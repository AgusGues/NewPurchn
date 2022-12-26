using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.Web.Script.Serialization;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapPOKertas : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadBulan();
                LoadTahun();
                //LoadSupplier();
                LoadCompany();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
        }

        private void LoadTahun()
        {
            POPurchnFacade ba = new POPurchnFacade();
            ba.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void LoadCompany()
        {
            CompanyFacade cp = new CompanyFacade();
            arrData = new ArrayList();
            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem("All", "0"));
            cp.Where = " AND DepoID in(1,7,13)";
            arrData = cp.Retrieve();
            foreach (Company cmp in arrData)
            {
                string lokasi = "";
                switch (cmp.DepoID)
                {
                    case 1: lokasi = "BPAS - CITEUREUP"; break;
                    case 7: lokasi = "BPAS - KARAWANG"; break;
                    case 13: lokasi = "BPAS - JOMBANG"; break;
                }
                ddlCompany.Items.Add(new ListItem(lokasi, cmp.DepoID.ToString()));
            }
            ddlCompany.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
        }
        public decimal qtyRMS = 0;
        protected void Rba_DataBound(object sender, RepeaterItemEventArgs e)
        {
            AgenLapak ag = (AgenLapak)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lstR");
            tr.Cells[1].InnerHtml = tr.Cells[1].InnerText.ToUpper();
            decimal tPotongan = (ag.KAPlant + ag.Sampah);
            qtyRMS = (qtyRMS + ag.QtyRMS);
            if (tPotongan < 25 && ag.ItemName != "KERTAS KRAFT")
            {
                tr.Cells[8].Style.Add("color", "red");
                tr.Cells[8].Attributes.Add("title", "Total Potongan (%)= " + tPotongan.ToString("N2"));
                tr.Cells[9].Style.Add("color", "red");
            }

            if (ag.KADepo == ag.KAPlant)
            {
                tr.Cells[1].Style.Add("color", "blue");
                tr.Cells[8].Attributes.Add("title", "Total Potongan (%)= " + tPotongan.ToString("N2"));
                tr.Cells[5].Style.Add("color", "blue");
            }
            if (ddlCompany.SelectedIndex == 0)
            {
                tr.Visible = true;
                nomor = nomor + 1;
                tr.Cells[0].InnerHtml = nomor.ToString();
            }
            else if (ag.PlantID == int.Parse(ddlCompany.SelectedValue))
            {
                tr.Visible = true;
                nomor = nomor + 1;
                tr.Cells[0].InnerHtml = nomor.ToString();
            }

            else
            {
                tr.Visible = false;
            }
        }
        protected void Rba_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }
        private int nomor = 0;
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            qtyRMS = 0;
            Bulan = ddlBulan.SelectedItem.Text;
            RekapPoKertas();
            if (ddlCompany.SelectedItem.Text.Trim().ToUpper() == "ALL")
            {
                decimal pembagi = 0;
                if (GetIedulFitri() >0)
                    pembagi = 1000;
                else
                    pembagi = 2000;
                LblPencapaian.Text = (qtyRMS / 1000).ToString("N2") + " Ton";
                LblPencapaian0.Text = (((qtyRMS / 1000)/ pembagi) *100).ToString("N2") + " %";
                #region update sarmut 
                string sarmutPrs = "Effisiensi Pemantauan Budget";
                string strJmlLine = string.Empty;
                string strDept = "PURCHASING";
                int SBulan = 0;
                int Tahun = 0;
                int deptid = getDeptID("PURCHASING");
                if (GetIedulFitri() > 0)
                    sarmutPrs = "Pemasukan Pulp Lokal  ( Target bulan libur lebaran )";
                else
                    sarmutPrs = "Pemasukan Pulp Lokal";

                strJmlLine = string.Empty;
                decimal actual = qtyRMS / 1000;
                //Pemasukan Pulp Lokal ( Target bulan libur lebaran ) 
                #endregion
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = 
                    "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                    "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                    "update SPD_TransPrs set actual=0 where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan like 'Pemasukan Pulp Lokal%') " +
                    "update SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') ";
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                    zl1.CustomQuery = zl1.CustomQuery +
                        "update [sqlctrp.grcboard.com].bpasctrp.dbo.SPD_TransPrs set actual=0 " +
                        " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                        "select ID from [sqlctrp.grcboard.com].bpasctrp.dbo.SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan like 'Pemasukan Pulp Lokal%') " +
                        "update [sqljombang.grcboard.com].bpasjombang.dbo.SPD_TransPrs set actual=0 " +
                        " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                        "select ID from [sqljombang.grcboard.com].bpasjombang.dbo.SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan like 'Pemasukan Pulp Lokal%') " +

                        "update [sqlctrp.grcboard.com].bpasctrp.dbo.SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") +
                        " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                        "select ID from [sqlctrp.grcboard.com].bpasctrp.dbo.SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') " +
                        "update [sqljombang.grcboard.com].bpasjombang.dbo.SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") +
                        " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                        "select ID from [sqljombang.grcboard.com].bpasjombang.dbo.SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
            else
                LblPencapaian.Text = "Pilihan harus ALL Plant";
        }
        protected int GetIedulFitri()
        {
            int ads = 0;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery =
                "select count(harilibur) total from CalenderOffDay where keterangan like '%fitri%' and year(harilibur)=" + ddlTahun.SelectedItem.Text +
                " and month(harilibur)=" + ddlBulan.SelectedValue;
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ads = Int32.Parse(dr["Total"].ToString());
                }
            }
            return ads;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanPOKertasDataPlant.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            //FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //StreamReader sr = fi.OpenText();
            //while (sr.Peek() >= 0)
            //{
            //    sb.Append(sr.ReadLine());
            //}
            //sr.Close();
            //Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>PEMANTAUAN PO KERTAS SESUAI DATA PLANT</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
            Html += "<br>Total Pencapaian Tonase: " + LblPencapaian.Text;
            Html += "<br>Total Pencapaian Persen: " + LblPencapaian0.Text;
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void RekapPoKertas()
        {
            Bulan = ddlBulan.SelectedValue;
            POPurchnFacade pOPurchFacade = new POPurchnFacade();
            ArrayList arrData = new ArrayList();
            bpas_api.WebService1 api = new bpas_api.WebService1();
            string data = api.DataKirimanKertasFilterBy(ddlBulan.SelectedValue, ddlTahun.SelectedValue, Convert.ToInt32(ddlCompany.SelectedValue));
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.Deserialize<AgenLapak[]>(data);
            foreach (AgenLapak ak in obj)
            {
                AgenLapak agk = new AgenLapak();
                agk = ak;
                agk.TglReceipt = ak.TglReceipt.ToLocalTime();
                arrData.Add(agk);
            }
            Rba.DataSource = arrData;
            Rba.DataBind();
        }
    }

    public class myClass
    {
        public List<AgenLapak> data { get; set; }
    }
    public class AgenLapak
    {
        public int DepoID { get; set; }
        public string DepoName { get; set; }
        public string Checker { get; set; }
        public int PlantID { get; set; }
        public string NoSJ { get; set; }
        public DateTime TglKirim { get; set; }
        public DateTime TglETA { get; set; }
        public DateTime TglReceipt { get; set; }
        public int ReceiptID { get; set; }
        public string Expedisi { get; set; }
        public string NOPOL { get; set; }
        public decimal GrossDepo { get; set; }
        public decimal NettDepo { get; set; }
        public decimal KADepo { get; set; }
        public decimal JmlBAL { get; set; }
        public int POKAID { get; set; }
        public decimal GrossPlant { get; set; }
        public decimal NettPlant { get; set; }
        public decimal KAPlant { get; set; }
        public decimal JmlBALPlant { get; set; }
        public string DocNo { get; set; }
        public decimal SelisihKA { get; set; }
        public int SupplierID { get; set; }
        public decimal StdKA { get; set; }
        public decimal Sampah { get; set; }
        public DateTime TglRMS { get; set; }
        public string NoRMS { get; set; }
        public string NoPO { get; set; }
        public string ItemName { get; set; }
        public string SupplierName { get; set; }
        public string PlantName { get; set; }
        public decimal Potongan { get; set; }
        public int AgenID { get; set; }
        public string AgenName { get; set; }
        public decimal QtyRMS { get; set; }
    }
}
