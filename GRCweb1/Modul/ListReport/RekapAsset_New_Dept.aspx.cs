using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapAsset_New_Dept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadDept();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void LoadDept()
        {
            Users user = (Users)Session["Users"];
            AssetM as1 = new AssetM();
            FacadeAssetM asf = new FacadeAssetM();
            ArrayList arrData = asf.RetrieveDept(user.UnitKerjaID.ToString());
            if (arrData.Count > 0)
            {
                ddlDept.Items.Clear();
                ddlDept.Items.Add(new ListItem("---- Pilih Dept ----", "0"));
                foreach (AssetM head in arrData)
                {
                    ddlDept.Items.Add(new ListItem(head.Dept, head.KodeDept.ToString()));
                }
            }

        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            ReceiptFacade rf = new ReceiptFacade();
            ddlTahun.Items.Clear();
            ArrayList arrData = new ArrayList();
            arrData = rf.RetrieveTahun();
            foreach (BeritaAcara b in arrData)
            {
                ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadListAsset();
        }
        private decimal saAwal, saBeli, saAdjIn, saAdjOut, mIn, mOut, sAkhir, sSPB, n, sGudang = 0;
        private void LoadListAsset()
        {
            n = 0;
            saAwal = 0; saBeli = 0; saAdjIn = 0; saAdjOut = 0; mIn = 0; mOut = 0; sAkhir = 0; sSPB = 0; sGudang = 0;
            FacadeAssetM rf = new FacadeAssetM();
            ArrayList arrData = new ArrayList();
            string fieldSaldo = string.Empty;
            string yearPeriod = string.Empty;
            rf.Bulan = int.Parse(ddlBulan.SelectedValue);
            rf.Tahun = int.Parse(ddlTahun.SelectedValue);
            switch (ddlBulan.SelectedValue)
            {
                case "1":
                    fieldSaldo = "DesQty";
                    yearPeriod = ((int.Parse(ddlTahun.SelectedValue)) - 1).ToString();
                    break;
                default:
                    fieldSaldo = Global.nBulan(int.Parse(ddlBulan.SelectedValue), true) + "Qty";
                    yearPeriod = ddlTahun.SelectedValue.ToString();
                    break;
            }
            Users user = (Users)Session["Users"];
            rf.FieldSaldoAwal = fieldSaldo;
            rf.YearPeriode = yearPeriod;
            arrData = rf.RetrieveRekapAssetNew_Dept(ddlDept.SelectedValue, user.UnitKerjaID.ToString());
            lstAsset.DataSource = arrData;
            lstAsset.DataBind();
            //total.Cells[1].InnerHtml = n.ToString("N0");
            total.Cells[1].InnerHtml = saAwal.ToString("N0");
            total.Cells[2].InnerHtml = saBeli.ToString("N0");
            total.Cells[3].InnerHtml = saAdjIn.ToString("N0");
            total.Cells[4].InnerHtml = saAdjOut.ToString("N0");
            /*
            total.Cells[5].InnerHtml = mIn.ToString("N0");
            total.Cells[6].InnerHtml = mOut.ToString("N0");
            */
            total.Cells[5].InnerHtml = sAkhir.ToString("N0"); //index prev = 7
                                                              /*
                                                              total.Cells[8].InnerHtml = sSPB.ToString("N0");
                                                              total.Cells[9].InnerHtml = sGudang.ToString("N0");
                                                              */

        }
        protected void lstAsset_Databound(object sender, RepeaterItemEventArgs e)
        {
            AssetM rk = (AssetM)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            if (rk.StockGudang < 0)
            {
                //tr.Cells[12].Attributes.Add("style", "color:red");

            }
            saAwal = (saAwal + rk.SaldoAwal);
            saBeli = (saBeli + rk.Pembelian);
            saAdjIn = (saAdjIn + rk.AdjustIN);
            saAdjOut = (saAdjOut + rk.AdjustOut);
            mIn = (mIn + rk.MutasiIN);
            mOut = (mOut + rk.MutasiOut);
            sAkhir = (sAkhir + rk.SaldoAkhir);
            sSPB = (sSPB + rk.SPB);
            sGudang = (sGudang + rk.StockGudang);
            n = n + 1;

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapAsset.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }

    public class AssetM
    {
        public string PeriodeCostControl { get; set; }
        public string NamaPlant { get; set; }
        public string Dept { get; set; }
        public string KodeDept { get; set; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }

        public decimal AdjustIN { get; set; }
        public decimal AdjustOut { get; set; }
        public decimal SaldoAwal { get; set; }
        public decimal MutasiIN { get; set; }
        public decimal MutasiOut { get; set; }
        public decimal SaldoAkhir { get; set; }
        public decimal Pembelian { get; set; }
        public decimal SPB { get; set; }
        public decimal StockGudang { get; set; }
        public string Kategori { get; set; }
    }

    public class FacadeAssetM
    {
        public string strError = string.Empty;
        private ArrayList arrReceipt = new ArrayList();
        private AssetM pm = new AssetM();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public string FieldSaldoAwal { get; set; }
        public string YearPeriode { get; set; }

        public ArrayList RetrieveDept(string A)
        {
            string query = string.Empty;
            string q1 = string.Empty;

            if (A.Length == 1)
            {
                query = "13";
                q1 = "9";
            }
            else
            {
                query = "14";
                q1 = "10";
            }
            ArrayList arrData = new ArrayList();
            string strSQL =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) DROP TABLE [dbo].[temp1] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp2]') AND type in (N'U')) DROP TABLE [dbo].[temp2] " +

            " declare @temp table " +
            " ( " +
            " ItemCode varchar(255), " +
            " ItemName varchar(MAX), " +
            " Unit varchar(255), " +
            " Kategori varchar(100), " +
            " SaldoAwal decimal, " +
            " Pembelian decimal, " +
            " AdjustIN decimal, " +
            " AdjustOut decimal, " +
            " SaldoAkhir decimal, " +
            " SPB decimal, " +
            " StockGudang decimal " +
            
            " ); " +
            " INSERT @temp  Exec dbo.RekapAssetNew '6','2021','JunQty','2021' ; " +
            " select distinct ItemCode into temp1 from @temp   " +

            " select distinct SUBSTRING(ItemCode," + q1 + ",1)KodeDept into temp2 from temp1 where len(ItemCode)='" + query + "' " +
            " select A.*,B.NamaDept Dept from temp2 A inner join AM_Department B on A.KodeDept=B.ID where KodeDept > 0 order by KodeDept " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) DROP TABLE [dbo].[temp1] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp2]') AND type in (N'U')) DROP TABLE [dbo].[temp2] ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AssetM
                    {
                        KodeDept = sdr["KodeDept"].ToString(),
                        Dept = sdr["Dept"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveRekapAssetNew_Dept(string A, string UnitKerja)
        {
            string query = string.Empty; string query2 = string.Empty; string query3 = string.Empty;
            if (UnitKerja.Length == 1)
            {
                query2 = "9"; query3 = "13";
            }
            else
            {
                query2 = "10"; query3 = "14";
            }

            if (A != "0")
            {
                if (A == "1")
                {
                    query =
                    " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                    " union all " +
                    " select *  from temp1  where SUBSTRING(ItemCode,4,1)='B' and LEN(ItemCode)='11' ";
                }
                else if (A == "2")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='F' and LEN(ItemCode)='11' ";
                }
                else if (A == "3")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='M' and LEN(ItemCode)='11' ";
                }
                else if (A == "4")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='L' and LEN(ItemCode)='11' ";
                }
                else if (A == "5")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='G' and LEN(ItemCode)='11' ";
                }
                else if (A == "6")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='Q' and LEN(ItemCode)='11' ";
                }
                else if (A == "7")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
                else if (A == "8")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' ";
                    //" union all " +
                    //" select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
                else if (A == "9")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' ";
                    //" union all " +
                    //" select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
            }
            else
            { query = " select * from temp1 "; }

            arrReceipt = new ArrayList();
            string strSQL =
            //"EXEC dbo.RekapAssetNew " + this.Bulan + "," + this.Tahun + ",'" + this.FieldSaldoAwal + "','" + this.YearPeriode + "'";
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) DROP TABLE [dbo].[temp1] " +

            " declare @temp table " +
            " ( " +
            " ItemCode varchar(255), " +
            " ItemName varchar(MAX), " +
            " Unit varchar(255), " +
            " Kategori varchar(100), " +
            " SaldoAwal decimal, " +
            " Pembelian decimal, " +
            " AdjustIN decimal, " +
            " AdjustOut decimal, " +
            " SaldoAkhir decimal, " +
            " SPB decimal, " +
            " StockGudang decimal " +
            
            " ); " +
            " INSERT @temp  Exec dbo.RekapAssetNew '6','2021','JunQty','2021' ; " +
            " select * into temp1 from @temp  " +
            " " + query + " " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) DROP TABLE [dbo].[temp1] ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrReceipt.Add(GenerateObjectAsset(sdr));
                }
            }
            return arrReceipt;
        }
        private AssetM GenerateObjectAsset(SqlDataReader sdr)
        {
            AssetM rk = new AssetM();

            rk.ItemCode = sdr["ItemCode"].ToString();
            rk.ItemName = sdr["ItemName"].ToString();
            rk.Unit = sdr["Unit"].ToString();
            rk.SaldoAwal = decimal.Parse(sdr["SaldoAwal"].ToString());
            rk.SaldoAkhir = decimal.Parse(sdr["SaldoAkhir"].ToString());
            rk.Pembelian = decimal.Parse(sdr["Pembelian"].ToString());
            rk.AdjustIN = decimal.Parse(sdr["AdjustIN"].ToString());
            rk.AdjustOut = decimal.Parse(sdr["AdjustOut"].ToString());
            rk.SPB = decimal.Parse(sdr["SPB"].ToString());
            rk.StockGudang = decimal.Parse(sdr["StockGudang"].ToString());
            rk.Kategori = sdr["Kategori"].ToString();

            return rk;
        }
    }
}