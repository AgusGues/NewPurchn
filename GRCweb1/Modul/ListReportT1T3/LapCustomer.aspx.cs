using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LapCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                if (Request.QueryString["t"] != null)
                {
                    ddlTahun.SelectedValue = Request.QueryString["t"].ToString();
                }
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 100 , 75 ,false); </script>", false);
        }
        private void LoadTahun()
        {
            //ISO_PES p = new ISO_PES();
            //ArrayList arrData = new ArrayList();
            //arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select YEAR(tglkirim)as Tahun from t3_kirim where YEAR(tglkirim)>year(GETDATE())-6  group by YEAR(tglkirim)";
            SqlDataReader sdr = zl.Retrieve();
            ddlTahun.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlTahun.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LaporanCustmer" + ddlBulan.SelectedItem.Text + ddlTahun.SelectedValue.ToString() + ".xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LAPORAN CUSTOMER</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstr.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;

            zl.CustomQuery = "declare @thnbln char(6) " +
                "set @thnbln='" + ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString() + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempCustKirim]') AND type in (N'U')) DROP TABLE [dbo].[tempCustKirim]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempCustPrd]') AND type in (N'U')) DROP TABLE [dbo].[tempCustPrd]  " +

                "SELECT AA.TglKirim,AA.Customer, C.PartNo,C.Panjang,C.Lebar, sum(AA.Qty) as Lembar, sum(AA.Qty*((C.tebal*C.lebar*C.Panjang)/1000000000))  M3, " +
                "sum(AA.Qty*((C.tebal*C.lebar*C.Panjang)/(4*1220*2440)))  X48  into tempCustKirim " +
                "FROM ( " +
                "select a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,   " +
                "case when  SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust   " +
                "from T3_Kirim as a, T3_KirimDetail as b inner join fc_items I on b.itemid=I.ID where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 " +
                "and b.RowStatus>-1 and b.Status>-1 and (I.partno like '%-3-%' or I.partno like '%-M-%') " +
                "union all " +
                "select distinct cast (convert(char,tglproduksi,112) as datetime)  CreatedTime,'' Customer, 0 KirimID, '' SJNo,'' OPNo,  " +
                "cast (convert(char,tglproduksi,112) as datetime) TglKirim,0 KirimDetailID,0 ItemIDSJ,ItemID,0 Qty,'' Cust  from BM_Destacking  " +
                "where rowstatus>-1 and left(convert(varchar,tglproduksi,112),6)=@thnbln " +
                ") as AA      " +
                "INNER JOIN FC_Items AS C ON AA.ItemID = C.ID     " +
                "WHERE  left(convert(varchar,AA.TglKirim,112),6)=@thnbln group by AA.TglKirim,AA.Customer, C.PartNo,AA.Cust,C.Panjang,C.Lebar " +
                " " +
                "select convert(char,A.tglproduksi,112)tglproduksi,sum(A.qty * ((I.Tebal * I.Panjang * I.Lebar )/(4*1220*2440))) X48,sum(A.qty * ((I.Tebal * I.Panjang * I.Lebar )/(1000000000))) M3 " +
                "into tempCustPrd from BM_Destacking A inner join fc_items I on A.ItemID = I.ID where A.RowStatus>-1 and left(convert(char,A.tglproduksi,112),6)=@thnbln  " +
                "group by convert(char,A.tglproduksi,112) order by convert(char,A.tglproduksi,112) " +

                "/*Retail*/ " +
                "select tglkirim,sum(K0) K0, sum(K00) K00, sum(K1) K1, sum(K2) K2, sum(K3) K3, sum(K4) K4, sum(K5) K5,sum(K6) K6, sum(K7) K7, sum(K8) K8, " +
                "sum(K9) K9, sum(K10) K10,sum(K09) K09,sum(K010) K010, sum(K11) K11, " +
                "sum(K12) K12, sum(K13) K13, sum(K14) K14, sum(K15) K15, sum(K16) K16, sum(K17) K17, sum(K18) K18, sum(K0+K1+K3+K5+K7+K9+K09+K11+K13+K15+K17) K19,  " +
                "(select isnull(sum(X48),0) from tempCustKirim where TglKirim=A.TglKirim and partno not like '%-p-%' and partno not like '%-s-%' )K20, SUM(K00+K2+K4+K6+K8+K10+K010+K12+K14+K16+K18) K21, isnull((select X48 from tempCustPrd where tglproduksi=convert(char,tglkirim,112)),0)K22,  " +
                "isnull((select M3 from tempCustPrd where tglproduksi=convert(char,tglkirim,112)),0)K23 from ( " +

                /* Retail - 4mm 4X8 */
                "/* Retail - 4mm 4X8 */" +
                "select tglkirim,sum(lembar) K0, sum(m3) K00, 0 K1, 0 K2, 0 K3, 0 K4, 0 K5,0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, " +
                "0 K11, 0 K12, 0 K13,0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='RETAIL' and kelompok='4mm 4X8' and rowstatus>-1) " +
                "and Customer not in (select depo from t3_customerdepo where rowstatus>-1) and Customer not in (select project from t3_customerproject where rowstatus>-1) group by tglkirim  " +
                "union all " +

                /* Retail - 4mm 1200x2400 */
                "/* Retail - 4mm 1200x2400 */" +
                "select tglkirim, 0 K0, 0 K00, sum(lembar) K1, sum(m3) K2, 0 K3, 0 K4, 0 K5,0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='RETAIL' and kelompok='4mm 1200x2400' and rowstatus>-1) " +
                "and Customer not in (select depo from t3_customerdepo where rowstatus>-1) and Customer not in (select project from t3_customerproject where rowstatus>-1) group by tglkirim  " +
                "union all " +

                /* Retail - 1000x1000 dan 1000x2000 */
                "/* Retail - 1000x1000 dan 1000x2000 */" +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2, sum(lembar) K3, sum(m3) K4, 0 K5,0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='RETAIL' and kelompok='1000x1000 dan 1000x2000' and rowstatus>-1) " +
                "and Customer not in (select depo from t3_customerdepo where rowstatus>-1) and Customer not in (select project from t3_customerproject where rowstatus>-1) group by tglkirim  " +
                "union all " +

                /*Retail - CLB, SUB, DRG, TGR dan JVB*/
                "/*Retail - CLB, SUB, DRG, TGR dan JVB*/" +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4,  sum(lembar) K5,sum(m3) K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='RETAIL' and kelompok='CLB, SUB, DRG, TGR dan JVB' and rowstatus>-1) " +
                "and Customer not in (select depo from t3_customerdepo where rowstatus>-1) and Customer not in (select project from t3_customerproject where rowstatus>-1) group by tglkirim  " +

                /* Poject */
                "/*Poject*/ " +
                "union all " +
                /* Belahan Lebar < 1200  , Panjang : >= 3 MTR */
                "/* Belahan Lebar <> 1200 / 2440  , Panjang : >= 3 MTR */" +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, sum(lembar) K7, sum(m3) K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where  Customer in (select project from t3_customerproject where rowstatus>-1) and Panjang>=3000 and lebar<1200 group by tglkirim  " +
                "union all " +

                /* Belahan Lebar < 1200  , Panjang : < 3 MTR */
                "/* Belahan Lebar <> 1200 / 2440  , Panjang : < 3 MTR */" +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, sum(lembar) K9, sum(m3) K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where  Customer in (select project from t3_customerproject where rowstatus>-1)  and Panjang<3000 and lebar<1200 group by tglkirim  " +
                "union all " +

                /* Utuhan Lebar : 1200 / 2440 */
                "/* Utuhan Lebar : 1200 / 2440 */" +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8,0 K9, 0 K10,sum(lembar) K09,sum(m3) K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where  Customer in (select project from t3_customerproject where rowstatus>-1) and lebar>=1200 and panjang>=2400 group by tglkirim  " +

                "/*DEPO*/ " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, sum(lembar) K11, sum(m3) K12, 0 K13, 0 K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='DEPO' and kelompok='4mm 4X8 dan 1200' and rowstatus>-1) " +
                "and Customer in (select depo from t3_customerdepo where rowstatus>-1) group by tglkirim  " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, sum(lembar) K13, sum(m3) K14, 0 K15, 0 K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='DEPO' and kelompok='1000x1000 dan LOTUS' and rowstatus>-1) " +
                "and Customer in (select depo from t3_customerdepo where rowstatus>-1) group by tglkirim " +

                "/*Listplank*/ " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, sum(lembar) K15, sum(m3) K16, 0 K17, 0 K18,sum(X48)X48 " +
                "from tempCustKirim where partno in (select partno from t3_CustomerPartno where kolom='listplank' and kelompok='' and rowstatus>-1) group by tglkirim " +

                "/*>4mm & lainnya*/ " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, sum(lembar) K17, sum(m3) K18,sum(X48)X48 " +
                "from tempCustKirim where partno not in (select partno from t3_CustomerPartno where rowstatus>-1)  " +
                "and Customer not in (select depo from t3_customerdepo where rowstatus>-1) and Customer not in (select project from t3_customerproject where rowstatus>-1) and (partno not like '%-p-%' and partno not like '%-s-%') group by tglkirim  " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, sum(lembar) K17, sum(m3) K18,sum(X48)X48 " +
                "from tempCustKirim where customer in (select depo from T3_CustomerDepo where rowstatus>-1) and partno not in (select partno from t3_CustomerPartno where kolom='listplank' and rowstatus>-1) group by tglkirim  " +
                "union all " +
                "select tglkirim, 0 K0, 0 K00, 0 K1, 0 K2,0 K3, 0 K4, 0 K5, 0 K6, 0 K7, 0 K8, 0 K9, 0 K10,0 K09,0 K010, 0 K11, 0 K12, 0 K13, 0 K14, 0 K15, 0 K16, sum(lembar *-1) K17, sum(m3 *-1) K18,sum(X48)X48 " +
                "from tempCustKirim where customer in (select depo from T3_CustomerDepo where rowstatus>-1) and partno in (select partno from t3_CustomerPartno where kolom='DEPO' and rowstatus>-1) group by tglkirim  " +
                ")A group by tglkirim  " +
                "order by tglkirim " +

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempCustKirim]') AND type in (N'U')) DROP TABLE [dbo].[tempCustKirim]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempCustPrd]') AND type in (N'U')) DROP TABLE [dbo].[tempCustPrd]  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new LCust
                    {
                        Tglkirim = DateTime.Parse(sdr["tglkirim"].ToString()),
                        K0 = Convert.ToDecimal(sdr["K0"].ToString()),
                        K00 = Convert.ToDecimal(sdr["K00"].ToString()),
                        K1 = Convert.ToDecimal(sdr["K1"].ToString()),
                        K2 = Convert.ToDecimal(sdr["K2"].ToString()),
                        K3 = Convert.ToDecimal(sdr["K3"].ToString()),
                        K4 = Convert.ToDecimal(sdr["K4"].ToString()),
                        K5 = Convert.ToDecimal(sdr["K5"].ToString()),
                        K6 = Convert.ToDecimal(sdr["K6"].ToString()),
                        K7 = Convert.ToDecimal(sdr["K7"].ToString()),
                        K8 = Convert.ToDecimal(sdr["K8"].ToString()),
                        K9 = Convert.ToDecimal(sdr["K9"].ToString()),
                        K10 = Convert.ToDecimal(sdr["K10"].ToString()),
                        K09 = Convert.ToDecimal(sdr["K09"].ToString()),
                        K010 = Convert.ToDecimal(sdr["K010"].ToString()),
                        K11 = Convert.ToDecimal(sdr["K11"].ToString()),
                        K12 = Convert.ToDecimal(sdr["K12"].ToString()),
                        K13 = Convert.ToDecimal(sdr["K13"].ToString()),
                        K14 = Convert.ToDecimal(sdr["K14"].ToString()),
                        K15 = Convert.ToDecimal(sdr["K15"].ToString()),
                        K16 = Convert.ToDecimal(sdr["K16"].ToString()),
                        K17 = Convert.ToDecimal(sdr["K17"].ToString()),
                        K18 = Convert.ToDecimal(sdr["K18"].ToString()),
                        K19 = Convert.ToDecimal(sdr["K19"].ToString()),
                        K20 = Convert.ToDecimal(sdr["K20"].ToString()),
                        K21 = Convert.ToDecimal(sdr["K21"].ToString()),
                        K22 = Convert.ToDecimal(sdr["K22"].ToString()),
                        K23 = Convert.ToDecimal(sdr["K23"].ToString())
                    });
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        public decimal Total0 = 0;
        public decimal Total00 = 0;
        public decimal Total1 = 0;
        public decimal Total2 = 0;
        public decimal Total3 = 0;
        public decimal Total4 = 0;
        public decimal Total5 = 0;
        public decimal Total6 = 0;
        public decimal Total7 = 0;
        public decimal Total8 = 0;
        public decimal Total9 = 0;
        public decimal Total10 = 0;
        public decimal Total09 = 0;
        public decimal Total010 = 0;
        public decimal Total11 = 0;
        public decimal Total12 = 0;
        public decimal Total13 = 0;
        public decimal Total14 = 0;
        public decimal Total15 = 0;
        public decimal Total16 = 0;
        public decimal Total17 = 0;
        public decimal Total18 = 0;
        public decimal Total19 = 0;
        public decimal Total20 = 0;
        public decimal Total21 = 0;
        public decimal Total22 = 0;
        public decimal Total23 = 0;
        protected void lst_DataBound(object sender, RepeaterItemEventArgs e)
        {
            LCust K = (LCust)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Total0 = Total0 + K.K0;
                    Total00 = Total00 + K.K00;
                    Total1 = Total1 + K.K1;
                    Total2 = Total2 + K.K2;
                    Total3 = Total3 + K.K3;
                    Total4 = Total4 + K.K4;
                    Total5 = Total5 + K.K5;
                    Total6 = Total6 + K.K6;
                    Total7 = Total7 + K.K7;
                    Total8 = Total8 + K.K8;
                    Total9 = Total9 + K.K9;
                    Total10 = Total10 + K.K10;
                    Total09 = Total09 + K.K09;
                    Total010 = Total010 + K.K010;
                    Total11 = Total11 + K.K11;
                    Total12 = Total12 + K.K12;
                    Total13 = Total13 + K.K13;
                    Total14 = Total14 + K.K14;
                    Total15 = Total15 + K.K15;
                    Total16 = Total16 + K.K16;
                    Total17 = Total17 + K.K17;
                    Total18 = Total18 + K.K18;
                    Total19 = Total19 + K.K19;
                    Total20 = Total20 + K.K20;
                    Total21 = Total21 + K.K21;
                    Total22 = Total22 + K.K22;
                    Total23 = Total23 + K.K23;
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr.Cells[1].InnerText = Total0.ToString("N0");
                    tr.Cells[2].InnerText = Total00.ToString("N1");
                    tr.Cells[3].InnerText = Total1.ToString("N0");
                    tr.Cells[4].InnerText = Total2.ToString("N1");
                    tr.Cells[5].InnerText = Total3.ToString("N0");
                    tr.Cells[6].InnerText = Total4.ToString("N1");
                    tr.Cells[7].InnerText = Total5.ToString("N0");
                    tr.Cells[8].InnerText = Total6.ToString("N1");
                    tr.Cells[9].InnerText = Total7.ToString("N0");
                    tr.Cells[10].InnerText = Total8.ToString("N1");
                    tr.Cells[11].InnerText = Total9.ToString("N0");
                    tr.Cells[12].InnerText = Total10.ToString("N1");
                    tr.Cells[13].InnerText = Total09.ToString("N0");
                    tr.Cells[14].InnerText = Total010.ToString("N1");
                    tr.Cells[15].InnerText = Total11.ToString("N0");
                    tr.Cells[16].InnerText = Total12.ToString("N1");
                    tr.Cells[17].InnerText = Total13.ToString("N0");
                    tr.Cells[18].InnerText = Total14.ToString("N1");
                    tr.Cells[19].InnerText = Total15.ToString("N0");
                    tr.Cells[20].InnerText = Total16.ToString("N1");
                    tr.Cells[21].InnerText = Total17.ToString("N1");
                    tr.Cells[22].InnerText = Total18.ToString("N1");
                    tr.Cells[23].InnerText = Total19.ToString("N1");
                    tr.Cells[24].InnerText = Total20.ToString("N1");
                    tr.Cells[25].InnerText = Total21.ToString("N1");
                    tr.Cells[26].InnerText = Total22.ToString("N1");
                    tr.Cells[27].InnerText = Total23.ToString("N1");

                }
            }
            catch { }
        }
    }
}

public class LCust : GRCBaseDomain
{
    public DateTime Tglkirim { get; set; }
    public Decimal K0 { get; set; }
    public Decimal K00 { get; set; }
    public Decimal K09 { get; set; }
    public Decimal K010 { get; set; }

    public Decimal K1 { get; set; }
    public Decimal K2 { get; set; }
    public Decimal K3 { get; set; }
    public Decimal K4 { get; set; }
    public Decimal K5 { get; set; }
    public Decimal K6 { get; set; }
    public Decimal K7 { get; set; }
    public Decimal K8 { get; set; }
    public Decimal K9 { get; set; }
    public Decimal K10 { get; set; }
    public Decimal K11 { get; set; }
    public Decimal K12 { get; set; }
    public Decimal K13 { get; set; }
    public Decimal K14 { get; set; }
    public Decimal K15 { get; set; }
    public Decimal K16 { get; set; }
    public Decimal K17 { get; set; }
    public Decimal K18 { get; set; }
    public Decimal K19 { get; set; }
    public Decimal K20 { get; set; }
    public Decimal K21 { get; set; }
    public Decimal K22 { get; set; }
    public Decimal K23 { get; set; }
}
