using BusinessFacade;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class RasioPotProduk : System.Web.UI.Page
    {
        decimal TotalQty = 0; decimal TotalM3 = 0; decimal GrandTotal = 0; decimal GrandTotalM3 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                Panel1.Visible = true;
                LoadBulan(); LoadTahun();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void LoadBulan()
        {
            ArrayList arrBulan = new ArrayList();
            RasioFacade data = new RasioFacade();
            arrBulan = data.RetrieveBulan();
            ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem(Session["NamaBulanTemp"].ToString(), DateTime.Now.Month.ToString()));
            foreach (RasioDomain bulan in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bulan.BulanNama, bulan.Bulan));
            }
        }
        protected void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            RasioFacade data = new RasioFacade();
            arrTahun = data.RetrieveTahun();
            ddlTahun.Items.Clear();
            //ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            foreach (RasioDomain tahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(tahun.Tahun, tahun.Tahun));
            }
        }

        protected void ddlBulan_SelectedChange(object sender, EventArgs e)
        {

        }

        protected void ddlTahun_SelectedChange(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string Periode = ddlBulan.SelectedItem.ToString() + " " + ddlTahun.SelectedItem.ToString();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=Rasio.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string Html = "<center><b>PEMANTAUAN PEMOTONGAN PRODUK 1220 x 2440 ke 1200 x 2400";       
            //Html += "<br>";
            //string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            //Contents = Contents.Replace("border=\"0", "border=\"0");
            //Contents = Contents;
            Response.Write(Contents);
            Response.Flush();
            Response.End();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            string thn = ddlTahun.SelectedValue;
            string bln = ((ddlBulan.SelectedValue).ToString().Length == 1) ? "0" + ddlBulan.SelectedValue : ddlBulan.SelectedValue.ToString();
            string periode = thn + bln;

            ArrayList arrRasio = new ArrayList();
            RasioDomain rs = new RasioDomain(); RasioDomain rs1 = new RasioDomain(); RasioDomain rs2 = new RasioDomain(); RasioDomain rs3 = new RasioDomain();
            RasioFacade rf = new RasioFacade(); RasioFacade rf1 = new RasioFacade(); RasioFacade rf2 = new RasioFacade(); RasioFacade rf3 = new RasioFacade();

            if (user.UnitKerjaID == 1)
            {
                rs = rf.RetrieveData(periode, user.UnitKerjaID);
                rs1 = rf1.RetrieveData2(periode);
                rs2 = rf2.RetrieveData3(periode);
            }
            else if (user.UnitKerjaID == 7)
            {
                rs = rf.RetrieveData_Krwg(periode);
                rs1 = rf1.RetrieveData2_Krwg(periode);
                rs2 = rf2.RetrieveData3_Krwg(periode);
            }

            //tambahan jombang
            else if (user.UnitKerjaID == 13)
            {
                rs = rf.RetrieveData_Jmbg(periode);
                rs1 = rf1.RetrieveData2_Jmbg(periode);
                rs2 = rf2.RetrieveData3_Jmbg(periode);
            }


            decimal A = 0; decimal B = 0; decimal C = 0; decimal ttl1 = 0; decimal ttl2 = 0; decimal A1 = 0; decimal B1 = 0; decimal C1 = 0;
            decimal persenttl = 0;
            A = Convert.ToDecimal(rs.QtyOK_M3);
            B = Convert.ToDecimal(rs1.QtyNP1OK_M3);
            C = Convert.ToDecimal(rs2.QtyOKNP2_M3);
            A1 = Convert.ToDecimal(rs.QtyBP_M3);
            B1 = Convert.ToDecimal(rs1.QtyBPNP1_M3);
            C1 = Convert.ToDecimal(rs2.QtyBPNP2_M3);
            ttl1 = (A + B + C);
            ttl2 = (A1 + B1 + C1);
            //tambahan eror hendling jika data ttl1 dan ttl2 0 (0 tidak bisa dibagi 0)
            if (ttl1 == 0 && ttl2 == 0)
            {
                persenttl = 0; Session["actual"] = persenttl;
            }
            else
            {
                persenttl = (ttl1 / ttl2) * 100; Session["actual"] = persenttl;
            }


            lblperiode.Attributes.Add("Style", "font-size:14;"); lblperiode.Font.Name = "Calibri";
            lblheader.Attributes.Add("Style", "font-size:22; font-weight:bold;"); lblheader.Font.Name = "Calibri";
            lbl0.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lbl0.Font.Name = "Calibri";
            lbl00.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lbl00.Font.Name = "Calibri";
            lbl000.Attributes.Add("Style", "font-size:16; font-weight:bold;"); lbl000.Font.Name = "Calibri";
            lblP_BP.Attributes.Add("Style", "font-size:14;"); lblP_BP.Font.Name = "Calibri";
            lblP_OK.Attributes.Add("Style", "font-size:14;"); lblP_OK.Font.Name = "Calibri";
            lblPersen1.Attributes.Add("Style", "font-size:14;"); lblPersen1.Font.Name = "Calibri";
            lblNP.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lblNP.Font.Name = "Calibri";
            lblNP0.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lblNP0.Font.Name = "Calibri";

            lblNP_BP.Attributes.Add("Style", "font-size:14;"); lblNP_BP.Font.Name = "Calibri";
            lblNP_BP0.Attributes.Add("Style", "font-size:14;"); lblNP_BP0.Font.Name = "Calibri";
            txtNP_BPm3.Attributes.Add("Style", "font-size:14;"); txtNP_BPm3.Font.Name = "Calibri";
            txtNP_BPlb.Attributes.Add("Style", "font-size:14;"); txtNP_BPlb.Font.Name = "Calibri";

            lblNP_OK.Attributes.Add("Style", "font-size:14;"); lblNP_OK.Font.Name = "Calibri";
            lblNP_OK0.Attributes.Add("Style", "font-size:14;"); lblNP_OK0.Font.Name = "Calibri";
            txtNP_OKm3.Attributes.Add("Style", "font-size:14;"); txtNP_OKm3.Font.Name = "Calibri";
            txtNP_OKlb.Attributes.Add("Style", "font-size:14;"); txtNP_OKlb.Font.Name = "Calibri";
            lblPersenNP1.Attributes.Add("Style", "font-size:14;"); lblPersenNP1.Font.Name = "Calibri";

            lblNP01.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lblNP01.Font.Name = "Calibri";
            lblPersenTotal.Attributes.Add("Style", "font-size:14; font-weight:bold;"); lblPersenTotal.Font.Name = "Calibri";

            lblinfo.Attributes.Add("Style", "font-size:14;"); lblinfo.Font.Name = "Calibri";
            lblinfo1.Attributes.Add("Style", "font-size:14;"); lblinfo1.Font.Name = "Calibri";
            lblinfo2.Attributes.Add("Style", "font-size:14;"); lblinfo2.Font.Name = "Calibri";
            lblinfo3.Attributes.Add("Style", "font-size:14;"); lblinfo3.Font.Name = "Calibri";

            lblNP_BP2.Attributes.Add("Style", "font-size:14;"); lblNP_BP2.Font.Name = "Calibri";
            lblNP_BP20.Attributes.Add("Style", "font-size:14;"); lblNP_BP20.Font.Name = "Calibri";
            txtNP_BP2m3.Attributes.Add("Style", "font-size:14;"); txtNP_BP2m3.Font.Name = "Calibri";
            txtNP_BP2lb.Attributes.Add("Style", "font-size:14;"); txtNP_BP2lb.Font.Name = "Calibri";

            lblheader.Text = "PEMANTAUAN PEMOTONGAN PRODUK 1220 x 2440 ke 1200 x 2400";
            lblperiode.Text = "Periode " + " : " + ddlBulan.SelectedItem.ToString().Trim() + " " + ddlTahun.SelectedValue.ToString().Trim();
            lbl000.Text = "KETEBALAN : 3, 3.5, 4, & 4.5 mm";
            lbl00.Text = "1. Produk Pressing";

            lbl0.Text = "a. Ketebalan 4 mm";
            lblP_BP.Text = "Penerimaan BP 1220 x 2440";
            lblP_BP1.Text = " = ";
            txtP_BP1.Text = rs.QtyBP_M3.ToString("N0") + " m3";
            txtP_BP2.Text = " ( " + rs.QtyBP.ToString("N0") + " lembar )"; txtP_BP2.Attributes.Add("Style", "text-align: right;");

            lblP_OK.Text = "Hasil OK 1200 x 2400";
            lblP_OK1.Text = " = ";
            txtP_OK1.Text = rs.QtyOK_M3.ToString("N0") + " m3";
            txtP_OK2.Text = " ( " + rs.QtyOK.ToString("N0") + " lembar )"; txtP_OK2.Attributes.Add("Style", "text-align: right;");

            lblPersen1.Text = "Rasio Pemotongan Produk 1220 x 2440 ke 1200 x 2400 untuk produk Pressing ketebalan 4 mm yaitu : " + rs.QtyM3.ToString("N0") + " %";
            lblNP.Text = "2. Produk Non Pressing";
            lblNP0.Text = "a. Ketebalan 3.5 mm";

            lblNP_BP.Text = "Penerimaan BP 1220 x 2440";
            lblNP_BP0.Text = " = ";
            txtNP_BPm3.Text = rs1.QtyBPNP1_M3.ToString("N0") + " m3";
            txtNP_BPlb.Text = " ( " + rs1.QtyBPNP1.ToString("N0") + " lembar )"; txtNP_BPlb.Attributes.Add("Style", "text-align: right;");

            lblNP_OK.Text = "Hasil OK 1200 x 2400";
            lblNP_OK0.Text = " = ";
            txtNP_OKm3.Text = rs1.QtyNP1OK_M3.ToString("N0") + " m3";
            txtNP_OKlb.Text = " ( " + rs1.QtyNP1OK.ToString("N0") + " lembar )"; txtNP_OKlb.Attributes.Add("Style", "text-align: right;");
            lblPersenNP1.Text = "Rasio Pemotongan Produk 1220 x 2440 ke 1200 x 2400 untuk produk Non Pressing ketebalan 3.5 mm yaitu : " + rs1.QtyNP1M3.ToString("N0") + " %";

            lblNP01.Text = "b. Ketebalan 4 mm";
            lblNP_BP2.Text = "Penerimaan BP 1220 x 2440";
            lblNP_BP20.Text = " = ";
            txtNP_BP2m3.Text = rs2.QtyBPNP2_M3.ToString("N0") + " m3";
            txtNP_BP2lb.Text = " ( " + rs2.QtyBPNP2.ToString("N0") + " lembar )"; txtNP_BP2lb.Attributes.Add("Style", "text-align: right;");

            lblNP_OK2.Text = "Hasil OK 1200 x 2400";
            lblNP_OK20.Text = " = ";
            txtNP_OK2m3.Text = rs2.QtyOKNP2_M3.ToString("N0") + " m3";
            txtNP_OK2lb.Text = " ( " + rs2.QtyNP2OK.ToString("N0") + " lembar )"; txtNP_OK2lb.Attributes.Add("Style", "text-align: right;");

            lblPersenNP2.Text = "Rasio Pemotongan Produk 1220 x 2440 ke 1200 x 2400 untuk produk Non Pressing ketebalan 4 mm yaitu : " + rs2.QtyNP2M3.ToString("N0") + " %";
            lblPersenTotal.Text = "3. Total Rasio Pemotongan 1220 x 2440 ke 1200 x 2400 (Produk Pressing & Non Pressing) yaitu : " + persenttl.ToString("N0") + " %";

            lblinfo.Text = "Keterangan :";
            lblinfo1.Text = "1. Produk Pressing diantaranya GRC, PNK, STU, ERA, NNO";
            lblinfo2.Text = "2. Produk Non Pressing diantaranya CLB, SUB, DRG, TGR, JVB, HRE";
            lblinfo3.Text = "3. Untuk Persen menggunakan perhitungan dalam meter kubik";

            Link2Sarmut();
        }
        private void Link2Sarmut()
        {
            /** Link by Beny
             *  Added 31 Juli 2021
             * **/
            string sarmutPrs = "Pemotongan produk 1220 x 2440 menjadi 1200 x 2400";
            decimal actual = Math.Round(Convert.ToDecimal(Session["actual"]));
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
            "declare @tahun int,@bulan int,@thnbln nvarchar(6) " +
            "set @tahun=" + ddlTahun.SelectedValue + " " +
            "set @bulan=" + ddlBulan.SelectedValue + " " +
            "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +

            "update SPD_TransPrs set actual='" + actual + "' where RowStatus>-1 and Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
            "select ID from SPD_Perusahaan where deptid=2 and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";

            SqlDataReader sdr1 = zl1.Retrieve();
        }

        protected void lstData_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //    for (int i = 1; i == lstData.Items.Count+1; )
            //    {
            //        Label lbltxt = (Label)lstData.FindControl("lbltxt");


            //        lbltxt.Text = "OK"; 
            //    }
            //}

            ////Label lbltxt = (Label)lstData.Items[1].FindControl("lbltxt"); lbltxt.Text = "OK";
            ////string x = string.Empty;

            ////Label lbltxt = (Label)FindControl("lbltxt"); lbltxt.Text = "OK";
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}

public class RasioFacade
{
    public string strError = string.Empty;
    private ArrayList arrDataRS = new ArrayList();
    private RasioDomain RS = new RasioDomain();
    private List<SqlParameter> sqlListParam;

    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }

    public ArrayList RetrieveBulan()
    {
        arrDataRS = new ArrayList();
        string strSQL =
        " select Bulan,case " +
        " when Bulan=1 then 'JANUARI' " +
        " when Bulan=2 then 'FEBRUARI' " +
        " when Bulan=3 then 'MARET' " +
        " when Bulan=4 then 'APRIL' " +
        " when Bulan=5 then 'MEI' " +
        " when Bulan=6 then 'JUNI' " +
        " when Bulan=7 then 'JULI' " +
        " when Bulan=8 then 'AGUSTUS' " +
        " when Bulan=9 then 'SEPTEMBER' " +
        " when Bulan=10 then 'OKTOBER' " +
        " when Bulan=11 then 'NOVEMBER' " +
        " when Bulan=12 then 'DESEMBER' " +
        " end BulanNama from " +
        " (select DISTINCT(MONTH(CreatedTime))Bulan from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as Data1 order by Bulan";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);

        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrDataRS.Add(new RasioDomain
                {
                    Bulan = sdr["Bulan"].ToString(),
                    BulanNama = sdr["BulanNama"].ToString()
                });
            }
        }
        return arrDataRS;
    }

    public ArrayList RetrieveTahun()
    {
        arrDataRS = new ArrayList();
        string strSQL =
        " select * from ( " +
        " select DISTINCT(YEAR(CreatedTime))Tahun from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as x order by Tahun desc";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);

        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrDataRS.Add(new RasioDomain
                {
                    Tahun = sdr["Tahun"].ToString()
                });
            }
        }
        return arrDataRS;
    }

    public RasioDomain RetrieveData(string periode, int PlantID)
    {
        string queryTujuan = string.Empty; string querySumber = string.Empty;
        if (PlantID == 1)
        {
            queryTujuan = "'H99','FN02'"; querySumber = "'G99','FN01'";
        }
        else if (PlantID == 7)
        {
            queryTujuan = "'H99','A98'"; querySumber = "'A99'";
        }
        string strSQL =

        " /** Pressing 1 BP **/ " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing] " +

        " select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempPressing_BP from ( " +
        " select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        " select x2.*,A.ItemID from ( " +
        " select sum(Qty)Qty,SerahID from ( " +
        " select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID,SerahID from ( " +
        " select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
        " /** Partno Tujuan **/ " +
        " LokID in (select ID from FC_Lokasi where Lokasi in (" + queryTujuan + ")) and ItemID " +
        " in (select ID from FC_Items where Tebal in ('4','4.5') and lebar='1200' and panjang='2400' and Kode in ('GRC','PNK','STU','ERA','NNO')  and " +
        " /** End Partno Tujuan **/ " +
        " /** Partno Sumber **/  " +
        " SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%' and Tebal in ('4','4.5') and " +
        " Lebar='1220' and Panjang='2440') and LokID in (select ID from FC_Lokasi where Lokasi in (" + querySumber + "))))   " +
        " /** End Partno Sumber **/ " +
        " )  as x group by ItemID,TglTrans,LokID,serahID ) as x1  group by SerahID  " +
        " ) as x2 inner join T3_Serah A ON A.ID=x2.SerahID " +
        " ) as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        " ) as x4 " +

        " /** Pressing 2 OK **/ " +
        " select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempPressing_OK from ( " +
        " select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        " select sum(Qty)Qty,ItemID from ( " +
        " select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID from ( " +
        " select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
        " /** Akhir **/ " +
        " LokID in (select ID from FC_Lokasi where Lokasi in (" + queryTujuan + ")) and ItemID in (select ID from FC_Items where Kode in ('GRC','PNK','STU','ERA','NNO') and PartNo like'%-3-04012002400%' or PartNo like'%-M-04012002400%' or PartNo like'%-3-04512002400%' or PartNo like'%-M-04512002400%') and " +
        " /** End Akhir **/ " +

        " /** Sumber **/  " +
        " SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi in (" + querySumber + ")))   " +
        " /** End Sumber **/ " +
        " )  as x group by ItemID,TglTrans,LokID) as x1  group by ItemID " +
        " ) as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        " ) as x4 " +

        //" select sum(QtyOK)QtyOK,sum(QtyBP)QtyBP,sum(QtyM3)QtyM3 into tempPressing from ( " +
        //" select 0'QtyOK',QtyLembar QtyBP,0'QtyM3' from tempPressing_BP " +
        //" union all " +
        //" select QtyLembar'QtyOK',0'QtyBP',0'QtyM3' from tempPressing_OK " +
        //" union all " +
        //" select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempPressing_BP)*100,0)PersenM3 from tempPressing_OK ) as x " +
        " select sum(QtyOK)QtyOK,sum(QtyOK_M3)QtyOK_M3,sum(QtyBP)QtyBP,sum(QtyBP_M3)QtyBP_M3,sum(QtyM3)QtyM3 into tempPressing from (  " +
        " select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempPressing_BP  " +
        " union all  " +
        " select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempPressing_OK  " +
        " union all  " +
        " select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempPressing_OK ) as x  " +
        " select ISNULL(qtyok, 0) qtyok, ISNULL(qtyok_m3, 0) qtyok_m3, ISNULL(qtybp, 0) qtybp, ISNULL(qtybp_m3, 0) qtybp_m3, ISNULL(qtym3, 0) qtym3 from tempPressing " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing] ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain RetrieveData_Krwg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('GRC','PNK','STU','ERA','NNO') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempPressing_BP from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('GRC','PNK','STU','ERA','NNO') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempPressing_OK from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyOK,sum(QtyOK_M3)QtyOK_M3,sum(QtyBP)QtyBP,sum(QtyBP_M3)QtyBP_M3,sum(QtyM3)QtyM3 into tempPressing from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempPressing_BP   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempPressing_OK  " +
        "  union all " +
        "  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempPressing_OK ) as x  " +

        "  select ISNULL(qtyok, 0) qtyok, ISNULL(qtyok_m3, 0) qtyok_m3, ISNULL(qtybp, 0) qtybp, ISNULL(qtybp_m3, 0) qtybp_m3, ISNULL(qtym3, 0) qtym3 from tempPressing " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData(sqlDataReader);
            }
        }

        return new RasioDomain();
    }


    public RasioDomain RetrieveData_Jmbg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('GRC','PNK','STU','ERA','NNO') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempPressing_BP from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('GRC','PNK','STU','ERA','NNO') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempPressing_OK from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyOK,sum(QtyOK_M3)QtyOK_M3,sum(QtyBP)QtyBP,sum(QtyBP_M3)QtyBP_M3,sum(QtyM3)QtyM3 into tempPressing from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempPressing_BP   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempPressing_OK  " +
        "  union all " +
        "  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempPressing_OK ) as x  " +

        "  select ISNULL(qtyok, 0) qtyok, ISNULL(qtyok_m3, 0) qtyok_m3, ISNULL(qtybp, 0) qtybp, ISNULL(qtybp_m3, 0) qtybp_m3, ISNULL(qtym3, 0) qtym3 from tempPressing " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempPressing]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    //public RasioDomain RetrieveData2(string periode, int PlantID)
    //{
    //    string querySumber = string.Empty; string queryTujuanOK = string.Empty; string queryTujuanBP = string.Empty;

    //    if (PlantID == 1)
    //    {
    //        querySumber = "'G99','FN01'"; queryTujuanOK = "'G99','FN02'"; queryTujuanBP = "'G99','FN02'";
    //    }
    //    else if (PlantID == 7)
    //    {
    //        querySumber = "'A99'"; queryTujuanOK = "'H99'"; queryTujuanBP = "'A98'";
    //    }

    //    string strSQL =

    //    "/** NonPressing 1 BP Tebal 3.5 **/" +
    //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP] " +
    //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK] " +
    //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing] " +

    //    "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_BP from ( " +
    //    "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
    //    "select x2.*,A.ItemID from ( " +
    //    "select sum(Qty)Qty,SerahID from ( " +
    //    "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID,SerahID from ( " +
    //    "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
    //    "/** Partno Tujuan **/ " +
    //    //"LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID in (select ID from FC_Items where Pressing is null) and " +
    //    "LokID in (select ID from FC_Lokasi where Lokasi in (" + queryTujuanBP + ")) and ItemID " +
    //    "in (select ID from FC_Items where Tebal in ('3.5') and lebar='1200' and panjang='2400' and Kode in ('CLB','SUB','DRG','TGR','JVB','HRE')  and " +
    //    "/** End Partno Tujuan **/ " +
    //    "/** Partno Sumber **/  " +
    //    "SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-03512202440%') and LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01'))))  " +
    //    "/** End Partno Sumber **/ " +
    //    ")  as x group by ItemID,TglTrans,LokID,serahID ) as x1  group by SerahID  " +
    //    ") as x2 inner join T3_Serah A ON A.ID=x2.SerahID " +
    //    ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
    //    ") as x4 " +

    //    "/** NonPressing 2 OK Tebal 3.5 **/ " +
    //    "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_OK from ( " +
    //    "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
    //    "select sum(Qty)Qty,ItemID from ( " +
    //    "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID from ( " +
    //    "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
    //    "/** Akhir **/ " +
    //    //"LokID in (select ID from FC_Lokasi where Lokasi in ('H99')) and ItemID in (select ID from FC_Items where Pressing is null and PartNo like'%-3-03512002400%' or PartNo like'%-M-03512002400%') and " +
    //    " LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID in (select ID from FC_Items where tebal='3.5' and SUBSTRING(PartNo,5,1) in ('3','M') and lebar='1200' and Panjang='2400' and Kode in ('CLB', 'SUB','DRG','TGR','JVB','HRE')) and " +
    //    "/** End Akhir **/ " +
    //    "/** Sumber **/  " +
    //    //"SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01')))   " +
    //    " SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440') "+
    //    " and LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01'))) "+
    //    "/** End Sumber **/ " +
    //    ")  as x group by ItemID,TglTrans,LokID) as x1  group by ItemID " +
    //    ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
    //    ") as x4 " +

    //    "select sum(QtyOK)QtyNP1OK,sum(QtyOK_M3)QtyNP1OK_M3,sum(QtyBP)QtyBPNP1,sum(QtyBP_M3)QtyBPNP1_M3,sum(QtyM3)QtyNP1M3 into tempNonPressing from (   " +
    //    "select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP  " +
    //    "union all   " +
    //    "select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK   " +
    //    "union all   " +
    //    "select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK ) as x   " +

    //    "select * from tempNonPressing  ";

    //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
    //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
    //    strError = dataAccess.Error;
    //    arrDataRS = new ArrayList();

    //    if (sqlDataReader.HasRows)
    //    {
    //        while (sqlDataReader.Read())
    //        {
    //            return GenerateObject_RetrieveData2(sqlDataReader);
    //        }
    //    }

    //    return new RasioDomain();
    //}

    public RasioDomain RetrieveData2(string periode)
    {
        string strSQL =

        " /** NonPressing 1 BP Tebal 3.5 **/ " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing] " +

        "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_BP from ( " +
        "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        "select x2.*,A.ItemID from ( " +
        "select sum(Qty)Qty,SerahID from ( " +
        "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID,SerahID from ( " +
        "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
        "/** Partno Tujuan **/ " +
        //"LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID in (select ID from FC_Items where Pressing is null) and " +
        "LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID " +
        "in (select ID from FC_Items where Tebal in ('3.5') and lebar='1200' and panjang='2400' and Kode in ('CLB','SUB','DRG','TGR','JVB','HRE')  and " +
        "/** End Partno Tujuan **/ " +
        "/** Partno Sumber **/  " +
        "SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-03512202440%') and LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01'))))  " +
        "/** End Partno Sumber **/ " +
        ")  as x group by ItemID,TglTrans,LokID,serahID ) as x1  group by SerahID  " +
        ") as x2 inner join T3_Serah A ON A.ID=x2.SerahID " +
        ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        ") as x4 " +

        "/** NonPressing 2 OK Tebal 3.5 **/ " +
        "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_OK from ( " +
        "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        "select sum(Qty)Qty,ItemID from ( " +
        "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID from ( " +
        "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
        "/** Akhir **/ " +
        //"LokID in (select ID from FC_Lokasi where Lokasi in ('H99')) and ItemID in (select ID from FC_Items where Pressing is null and PartNo like'%-3-03512002400%' or PartNo like'%-M-03512002400%') and " +
        " LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID in (select ID from FC_Items where tebal='3.5' and SUBSTRING(PartNo,5,1) in ('3','M') and lebar='1200' and Panjang='2400' and Kode in ('CLB', 'SUB','DRG','TGR','JVB','HRE')) and " +
        "/** End Akhir **/ " +
        "/** Sumber **/  " +
        //"SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01')))   " +
        " SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440') and LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01'))) " +
        "/** End Sumber **/ " +
        ")  as x group by ItemID,TglTrans,LokID) as x1  group by ItemID " +
        ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        ") as x4 " +

        "select sum(QtyOK)QtyNP1OK,sum(QtyOK_M3)QtyNP1OK_M3,sum(QtyBP)QtyBPNP1,sum(QtyBP_M3)QtyBPNP1_M3,sum(QtyM3)QtyNP1M3 into tempNonPressing from (   " +
        "select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP  " +
        "union all   " +
        "select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK   " +
        "union all   " +
        "select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK ) as x   " +

        "select ISNULL(qtynp1ok, 0) qtynp1ok, ISNULL(qtynp1ok_m3, 0) qtynp1ok_m3, ISNULL(qtybpnp1, 0) qtybpnp1, ISNULL(qtybpnp1_m3, 0) qtybpnp1_m3, ISNULL(qtybpnp1_m3, 0) qtynp1m3 from tempNonPressing  " +

        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing] ";


        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData2(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain RetrieveData2_Krwg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='3.5' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_BP from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='3.5' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_OK from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyNP1OK,sum(QtyOK_M3)QtyNP1OK_M3,sum(QtyBP)QtyBPNP1,sum(QtyBP_M3)QtyBPNP1_M3,sum(QtyM3)QtyNP1M3 into tempNonPressing from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK  " +
        "  union all " +
        //"  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK ) as x  " +
        " select 0'QtyOK',0'QtyBP',cast((cast(QtyM3 as decimal(18,0))/(select cast(QtyM3 as decimal(18,0)) " +
        " from tempNonPressing_BP)*100) as decimal(18,1))PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK ) as x  " +

        "  select ISNULL(qtynp1ok, 0) qtynp1ok, ISNULL(qtynp1ok_m3, 0) qtynp1ok_m3, ISNULL(qtybpnp1, 0) qtybpnp1, ISNULL(qtybpnp1_m3, 0) qtybpnp1_m3, ISNULL(QtyNP1M3, 0) qtynp1m3 from tempNonPressing " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData2(sqlDataReader);
            }
        }

        return new RasioDomain();
    }


    public RasioDomain RetrieveData2_Jmbg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='3.5' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_BP from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='3.5' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='3.5' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_OK from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyNP1OK,sum(QtyOK_M3)QtyNP1OK_M3,sum(QtyBP)QtyBPNP1,sum(QtyBP_M3)QtyBPNP1_M3,sum(QtyM3)QtyNP1M3 into tempNonPressing from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK  " +
        "  union all " +
        "  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK ) as x  " +

        "  select ISNULL(qtynp1ok, 0) qtynp1ok, ISNULL(qtynp1ok_m3, 0) qtynp1ok_m3, ISNULL(qtybpnp1, 0) qtybpnp1, ISNULL(qtybpnp1_m3, 0) qtybpnp1_m3, ISNULL(qtybpnp1_m3, 0) qtynp1m3 from tempNonPressing " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData2(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain RetrieveData3(string periode)
    {
        string strSQL =

        " /** NonPressing 1 BP Tebal 4 **/ " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2] " +

        "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_BP4 from ( " +
        "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        "select x2.*,A.ItemID from ( " +
        "select sum(Qty)Qty,SerahID from ( " +
        "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID,SerahID from ( " +
        "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'  and " +
        "/** Partno Tujuan **/ " +
        "LokID in (select ID from FC_Lokasi where Lokasi in ('H99','FN02')) and ItemID in " +
        "(select ID from FC_Items where Tebal=4 and Lebar='1200' and Panjang='2400' and Kode in ('CLB','SUB','DRG','TGR','JVB','HRE')) and " +
        "/** End Partno Tujuan **/ " +
        "/** Partno Sumber **/  " +
        "SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%' and Lebar='1220' and Panjang='2440') and LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01')))   " +
        "/** End Partno Sumber **/ " +
        ")  as x group by ItemID,TglTrans,LokID,serahID ) as x1  group by SerahID  " +
        ") as x2 inner join T3_Serah A ON A.ID=x2.SerahID " +
        ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        ") as x4 " +

        "/** NonPressing 2 OK Tebal 3.5 **/ " +
        "select sum(Qty)QtyLembar,round(sum(Volume),0)QtyM3 into tempNonPressing_OK4 from ( " +
        "select Qty,(((B.Tebal*B.Lebar*B.Panjang)/1000000000)*x3.Qty)Volume from ( " +
        "select sum(Qty)Qty,ItemID from ( " +
        "select distinct ItemID,sum(QtyIn)Qty,TglTrans,LokID from ( " +
        "select * from T3_Simetris where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "' and " +
        "/** Akhir **/ " +
        "LokID in (select ID from FC_Lokasi where Lokasi in ('H99')) and ItemID in (select ID from FC_Items where Pressing='NO' and PartNo like'%-3-04012002400SE%' or PartNo like'%-M-04512002400SE%' and RowStatus>-1)and " +
        "/** End Akhir **/ " +
        "/** Sumber **/  " +
        "SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi in ('G99','FN01')))   " +
        "/** End Sumber **/ " +
        ")  as x group by ItemID,TglTrans,LokID) as x1  group by ItemID " +
        ") as x3 inner join FC_Items B ON B.ID=x3.ItemID " +
        ") as x4 " +

        "select sum(QtyOK)QtyNP2OK,sum(QtyOK_M3)QtyOKNP2_M3,sum(QtyBP)QtyBPNP2,sum(QtyBP_M3)QtyBPNP2_M3,sum(QtyM3)QtyNP2M3 into tempNonPressing2 from (  " +
        "select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP4   " +
        "union all   " +
        "select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK4   " +
        "union all   " +
        "select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP4)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK4 ) as x   " +

        "select ISNULL(qtynp2ok, 0) qtynp2ok, ISNULL(qtyoknp2_m3, 0) qtyoknp2_m3, ISNULL(qtybpnp2, 0) qtybpnp2, ISNULL(qtybpnp2_m3, 0) qtybpnp2_m3, ISNULL(qtynp2m3, 0) qtynp2m3 from tempNonPressing2  " +

        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4] " +
        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2] ";



        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData3(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain RetrieveData3_Krwg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_BP4 from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_OK4 from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyNP2OK,sum(QtyOK_M3)QtyOKNP2_M3,sum(QtyBP)QtyBPNP2,sum(QtyBP_M3)QtyBPNP2_M3,sum(QtyM3)QtyNP2M3 into tempNonPressing2 from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP4   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK4  " +
        "  union all " +
        "  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP4)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK4 ) as x  " +

        "  select ISNULL(qtynp2ok, 0) qtynp2ok, ISNULL(qtyoknp2_m3, 0) qtyoknp2_m3, ISNULL(qtybpnp2, 0) qtybpnp2, ISNULL(qtybpnp2_m3, 0) qtybpnp2_m3, ISNULL(qtynp2m3, 0) qtynp2m3 from tempNonPressing2 " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData3(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain RetrieveData3_Jmbg(string periode)
    {
        string strSQL =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2]  " +

        " /** Part 1 **/ " +
        " /** Data Awal Simetris **/ " +
        " ;with dataSimetrisBP01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisBP02 as ( " +
        " select A.*,A1.ItemID ItemID0 from dataSimetrisBP01 A inner join T3_Serah A1 ON A.SerahID=A1.ID where A1.RowStatus>-1 and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisBP03 as ( " +
        " select SUBSTRING(B.PartNo,5,1)Jenis,SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,C.Tebal,C.Lebar,C.Panjang from dataSimetrisBP02 A inner join FC_Items B ON A.ItemID=B.ID inner join FC_Items C ON C.ID=A.ItemID0 where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400'  and LokID in (select ID from FC_Lokasi where Lokasi in ('A98','H99'))) , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisBP03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') and Jenis in ('P','3','M') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_BP4 from dataSimetrisBP04 " +
        "  /** End Part 1 **/ " +

        "  /** Part 2 **/ " +
        "  /** Data Awal Simetris **/ " +
        " ;with dataSimetrisOK01 as (  " +
        " select SerahID,LokID,ItemID,QtyIn from T3_Simetris A where RowStatus>-1 and left(convert(char,tgltrans,112),6)='" + periode + "'), " +

        " /** Sumber **/ " +
        " dataSimetrisOK02 as ( " +
        " select * from dataSimetrisOK01  where SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi='A99') and ItemID in (select ID from FC_Items where Tebal='4' and Lebar='1220' and Panjang='2440' and PartNo like'%-P-%'))), " +

        " /** Tujuan **/ " +
        " dataSimetrisOK03 as ( " +
        " select SUBSTRING(B.PartNo,1,3)Kode,B.PartNo,A.*,B.Tebal,B.Lebar,B.Panjang from dataSimetrisOK02 A inner join FC_Items B ON A.ItemID=B.ID where B.Tebal='4' and B.Lebar='1200' and B.Panjang='2400' and LokID in (select ID from FC_Lokasi where Lokasi='H99') and PartNo like'%-3-%' or PartNo like'%-M-%') , " +

        " dataSimetrisBP04 as ( " +
        " select sum(QtyIn)QtyLembar,((Tebal*Lebar*Panjang)/1000000000)Volume from dataSimetrisOK03 where Kode in ('CLB','SUB','DRG','TGR','JVB','HRE') group by Tebal,Lebar,Panjang) " +

        " select QtyLembar,cast(QtyLembar*Volume as decimal(18,2))QtyM3 into tempNonPressing_OK4 from dataSimetrisBP04 " +
        "   /** End Part 2 **/ " +

        "  select sum(QtyOK)QtyNP2OK,sum(QtyOK_M3)QtyOKNP2_M3,sum(QtyBP)QtyBPNP2,sum(QtyBP_M3)QtyBPNP2_M3,sum(QtyM3)QtyNP2M3 into tempNonPressing2 from ( " +
        "  select 0'QtyOK',QtyLembar QtyBP,0'QtyM3',QtyM3 QtyBP_M3,0'QtyOK_M3' from tempNonPressing_BP4   " +
        "  union all " +
        "   select QtyLembar'QtyOK',0'QtyBP',0'QtyM3',0'QtyBP_M3',QtyM3 QtyOK_M3 from tempNonPressing_OK4  " +
        "  union all " +
        "  select 0'QtyOK',0'QtyBP',round(QtyM3/(select QtyM3 from tempNonPressing_BP4)*100,0)PersenM3,0'QtyBP_M3',0'QtyOK_M3' from tempNonPressing_OK4 ) as x  " +

        "  select ISNULL(qtynp2ok, 0) qtynp2ok, ISNULL(qtyoknp2_m3, 0) qtyoknp2_m3, ISNULL(qtybpnp2, 0) qtybpnp2, ISNULL(qtybpnp2_m3, 0) qtybpnp2_m3, ISNULL(qtynp2m3, 0) qtynp2m3 from tempNonPressing2 " +

        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_BP4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_BP4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing_OK4]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing_OK4]  " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNonPressing2]') AND type in (N'U')) DROP TABLE [dbo].[tempNonPressing2]  ";




        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrDataRS = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_RetrieveData3(sqlDataReader);
            }
        }

        return new RasioDomain();
    }

    public RasioDomain GenerateObject_RetrieveData(SqlDataReader sqlDataReader)
    {
        RS = new RasioDomain();
        RS.QtyBP = Convert.ToDecimal(sqlDataReader["QtyBP"].ToString());
        RS.QtyOK = Convert.ToDecimal(sqlDataReader["QtyOK"].ToString());
        RS.QtyM3 = Convert.ToDecimal(sqlDataReader["QtyM3"].ToString());
        RS.QtyBP_M3 = Convert.ToDecimal(sqlDataReader["QtyBP_M3"].ToString());
        RS.QtyOK_M3 = Convert.ToDecimal(sqlDataReader["QtyOK_M3"].ToString());

        return RS;
    }

    public RasioDomain GenerateObject_RetrieveData2(SqlDataReader sqlDataReader)
    {
        RS = new RasioDomain();
        RS.QtyNP1OK = Convert.ToDecimal(sqlDataReader["QtyNP1OK"].ToString());
        RS.QtyNP1OK_M3 = Convert.ToDecimal(sqlDataReader["QtyNP1OK_M3"].ToString());
        RS.QtyBPNP1 = Convert.ToDecimal(sqlDataReader["QtyBPNP1"].ToString());
        RS.QtyBPNP1_M3 = Convert.ToDecimal(sqlDataReader["QtyBPNP1_M3"].ToString());
        RS.QtyNP1M3 = Convert.ToDecimal(sqlDataReader["QtyNP1M3"].ToString());

        return RS;
    }

    public RasioDomain GenerateObject_RetrieveData3(SqlDataReader sqlDataReader)
    {
        RS = new RasioDomain();
        RS.QtyNP2OK = Convert.ToDecimal(sqlDataReader["QtyNP2OK"].ToString());
        RS.QtyOKNP2_M3 = Convert.ToDecimal(sqlDataReader["QtyOKNP2_M3"].ToString());
        RS.QtyBPNP2 = Convert.ToDecimal(sqlDataReader["QtyBPNP2"].ToString());
        RS.QtyBPNP2_M3 = Convert.ToDecimal(sqlDataReader["QtyBPNP2_M3"].ToString());
        RS.QtyNP2M3 = Convert.ToDecimal(sqlDataReader["QtyNP2M3"].ToString());

        return RS;
    }

}

public class RasioDomain
{
    public decimal QtyNP2OK { get; set; }
    public decimal QtyOKNP2_M3 { get; set; }
    public decimal QtyBPNP2 { get; set; }
    public decimal QtyBPNP2_M3 { get; set; }
    public decimal QtyNP2M3 { get; set; }

    public decimal QtyNP1OK { get; set; }
    public decimal QtyNP1OK_M3 { get; set; }
    public decimal QtyBPNP1 { get; set; }
    public decimal QtyBPNP1_M3 { get; set; }
    public decimal QtyNP1M3 { get; set; }

    public int DeptIDBs { get; set; }
    public decimal M3 { get; set; }
    public string Bulan { get; set; }
    public string BulanNama { get; set; }
    public string Tahun { get; set; }
    public string QtyLembar { get; set; }
    public decimal QtyOK { get; set; }
    public decimal QtyBP { get; set; }
    public decimal QtyM3 { get; set; }
    public decimal QtyOK_M3 { get; set; }
    public decimal QtyBP_M3 { get; set; }

    public DateTime Createdtime { get; set; }

}
