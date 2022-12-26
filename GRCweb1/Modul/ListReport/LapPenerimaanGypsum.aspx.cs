using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPenerimaanGypsum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                GetBulan();
                GetTahun();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void GetBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void GetTahun()
        {
            LapGypsum lg = new LapGypsum();
            lg.Pilihan = "Tahun";
            ddlTahun.Items.Clear();
            foreach (Gypsum gp in lg.Retrieve())
            {
                ddlTahun.Items.Add(new ListItem(gp.Tahun.ToString(), gp.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadData()
        {
            ArrayList arrData = new ArrayList();
            LapGypsum lg = new LapGypsum();
            string GypsumCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt");
            lg.Pilihan = "Data";
            lg.Criteria = " and Month(ReceiptDate)=" + ddlBulan.SelectedValue.ToString();
            lg.Criteria += "and Year(ReceiptDate)=" + ddlTahun.SelectedValue.ToString();
            lg.Criteria += " and ItemID in(Select ID from Inventory where Itemcode in('" + GypsumCode + "')) ";
            lg.Criteria += " order by ReceiptDate,ID";
            arrData = lg.Retrieve();
            lstRMS.DataSource = arrData;
            lstRMS.DataBind();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LaporanPenerimaanGypsum.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "LAPORAN PENERIMAAN GYPSUM CURAH";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstRMS_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "Receipt");
            decimal stKA = (stdKA != string.Empty) ? Convert.ToDecimal(stdKA.ToString()) : 0;
            Gypsum gp = (Gypsum)e.Item.DataItem;
            Label sls = (Label)e.Item.FindControl("selisih");
            sls.Text = (gp.KadarAir > 28) ? (gp.KadarAir - stKA).ToString("###,##0.00") : "0";
        }
    }
    public class LapGypsum
    {
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string Field { get; set; }

        private ArrayList arrData = new ArrayList();
        private Gypsum gp = new Gypsum();

        private string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "Tahun":
                    query = "Select Distinct YEAR(ReceiptDate)Tahun from Receipt where ReceiptDate is not null order by Year(ReceiptDate) desc";
                    break;
                case "Data":
                    query = "select rd.ID,rs.ReceiptDate,rd.PONo,rd.Keterangan,QtyTimbang,isnull(ToAsset,0)toAsset,isnull(kadarair,0)KadarAir," +
                            " Round(Quantity,0)Quantity,rs.ReceiptNo, " +
                            "(select SupplierName from SuppPurch where ID=rs.SupplierId)SupplierName " +
                            "from ReceiptDetail as rd " +
                            "left join Receipt AS rs ON rs.ID=rd.ReceiptID " +
                            "where rs.Status>-1 and rd.RowStatus>-1 " + this.Criteria;
                    //"and YEAR(rs.ReceiptDate)=2015 and MONTH(rs.ReceiptDate)=9
                    //"and ItemID=31599";
                    break;
            }
            return query;
        }
        private Gypsum oGeneratObj(SqlDataReader sdr)
        {
            gp = new Gypsum();
            switch (this.Pilihan)
            {
                case "Tahun":
                    gp.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Data":
                    gp.ID = Convert.ToInt32(sdr["ID"].ToString());
                    gp.ReceiptDate = Convert.ToDateTime(sdr["ReceiptDate"].ToString());
                    gp.PoNo = sdr["PoNo"].ToString();
                    gp.SupplierName = sdr["SupplierName"].ToString();
                    gp.Keterangan1 = sdr["Keterangan"].ToString();
                    gp.QtyTimbang = Convert.ToDecimal(sdr["QtyTimbang"].ToString());
                    gp.QtyBPAS = Convert.ToInt32(sdr["toAsset"].ToString());
                    gp.Quantity = Convert.ToDecimal(sdr["Quantity"].ToString());
                    gp.KadarAir = Convert.ToDecimal(sdr["KadarAir"].ToString());
                    break;
            }
            return gp;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(oGeneratObj(sdr));
                }
            }
            return arrData;

        }
    }

    public class Gypsum : Receipt
    {
        public int Tahun { get; set; }
        public decimal KadarAir { get; set; }
    }
}