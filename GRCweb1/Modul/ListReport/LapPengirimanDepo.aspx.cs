using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPengirimanDepo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadDepo();
                btnExport.Enabled = false;
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);

        }

        private void LoadDepo()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertas where RowStatus=0";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepo.Items.Clear();
            ddlDepo.Items.Add(new ListItem("--Pilih Depo--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepo.Items.Add(new ListItem(sdr["DepoName"].ToString(), sdr["Alamat"].ToString().TrimEnd()));
                }
            }
            ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();

        }
        private void LoadTahun()
        {
            POPurchnFacade pd = new POPurchnFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadRekapan()
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = dk.PemantauanKA(true);
            lstRekap.DataSource = arrData;
            lstRekap.DataBind();

        }
        private void LoadRekapan(bool Klarifikasi)
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = dk.PemantauanKA(false);
            lstKla.DataSource = arrData;
            lstKla.DataBind();
        }
        private void LoadDetail()
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = dk.DetailKonfirmasi();
            lstDepo.DataSource = arrData;
            lstDepo.DataBind();
        }
        private decimal totalDepo = 0;
        private decimal TotalPlant = 0;
        private decimal tSelisih = 0;
        private void LoadByChecker()
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = new ArrayList();
            //dk.SelisihByChecker();

            bpas_api.WebService1 api = new bpas_api.WebService1();
            string data = api.DataKirimanByChecker(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.Deserialize<DeliveryKertas[]>(data);
            foreach (DeliveryKertas ak in obj)
            {
                DeliveryKertas agk = new DeliveryKertas();
                agk = ak;
                arrData.Add(agk);
                totalDepo += ak.NettDepo;
                TotalPlant += ak.NettPlant;
                tSelisih += ak.Selisih;
            }
            lstChecker.DataSource = arrData;
            lstChecker.DataBind();
        }
        private void LoadByDepo()
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = new ArrayList();
            //dk.SelisihByChecker();

            bpas_api.WebService1 api = new bpas_api.WebService1();
            string data = api.DataKirimanByDepo(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.Deserialize<DeliveryKertas[]>(data);
            foreach (DeliveryKertas ak in obj)
            {
                DeliveryKertas agk = new DeliveryKertas();
                agk = ak;
                arrData.Add(agk);

            }
            lstRekap.DataSource = arrData;
            lstRekap.DataBind();
        }
        private void LoadBySupplier()
        {
            DepoKertasKA dk = new DepoKertasKA();
            dk.Bulans = ddlBulan.SelectedValue.ToString();
            dk.Tahuns = ddlTahun.SelectedValue.ToString();
            ArrayList arrData = new ArrayList();
            //dk.SelisihByChecker();

            bpas_api.WebService1 api = new bpas_api.WebService1();
            string data = api.DataKirimanBySuppiler(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.Deserialize<DeliveryKertas[]>(data);
            foreach (DeliveryKertas ak in obj)
            {
                DeliveryKertas agk = new DeliveryKertas();
                agk = ak;
                arrData.Add(agk);

            }
            lstSuppler.DataSource = arrData;
            lstSuppler.DataBind();
        }
        protected void lstRekap_Databound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("baris");
            switch (dk.RowStatus)
            {
                case 2:
                    tr.Attributes.Add("class", "Line3 bold");
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    break;
                case 3:
                    tr.Attributes.Add("class", "total bold");
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    tr.Cells[1].InnerHtml = "Total Depo";
                    break;
                default:
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    break;
            }
        }
        protected void lstKla_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("brs");
            switch (dk.RowStatus)
            {
                case 2:
                    tr.Attributes.Add("class", "Line3 bold");
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    break;
                case 3:
                    tr.Attributes.Add("class", "total bold");
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    tr.Cells[1].InnerHtml = "Total Depo";
                    break;
                default:
                    // tr.Cells[0].Attributes.Add("class", "transparant");
                    break;
            }
        }
        private int urutan = 0;
        protected void lstDepo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("dtl");
            switch (dk.RowStatus)
            {
                case 2:
                    urutan = 0;
                    tr.Attributes.Add("class", "Line3 bold");
                    tr.Cells[0].InnerHtml = "";//.Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    //tr.Cells[1].InnerHtml = tr.Cells[1].InnerHtml;
                    tr.Cells[2].InnerHtml = "";
                    tr.Cells[4].InnerHtml = "";
                    tr.Cells[5].InnerHtml = "";
                    break;
                case 3:
                    urutan = 0;
                    tr.Attributes.Add("class", "total bold");
                    tr.Cells[0].InnerHtml = "";//.Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    tr.Cells[1].InnerHtml = dk.SupplierName.ToUpper();
                    tr.Cells[2].InnerHtml = "";
                    tr.Cells[4].InnerHtml = "";
                    tr.Cells[5].InnerHtml = "";
                    break;
                case 4:
                    urutan = 0;
                    tr.Attributes.Add("class", "rebobot2 bold");
                    tr.Cells[0].InnerHtml = "";//.Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    //tr.Cells[1].InnerHtml = dk.SupplierName.ToUpper();
                    tr.Cells[2].InnerHtml = "";
                    tr.Cells[4].InnerHtml = "";
                    tr.Cells[5].InnerHtml = "";
                    break;
                default:
                    urutan = urutan + 1;
                    tr.Cells[0].InnerHtml = (urutan).ToString();
                    break;
            }
        }
        protected void lstChecker_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("brsCh");
            switch (dk.RowStatus)
            {
                case 2:
                    tr.Attributes.Add("class", "Line3 bold");
                    tr.Cells[0].Attributes.Add("class", "transparant");
                    tr.Cells[1].Attributes.Add("class", "kotak angka");
                    break;
            }
            //chTotal.Cells[2].InnerHtml = totalDepo.ToString("N2");
            //chTotal.Cells[3].InnerHtml = TotalPlant.ToString("N2");
            //chTotal.Cells[4].InnerHtml = tSelisih.ToString("N2");
            //chTotal.Cells[5].InnerHtml = (tSelisih / totalDepo).ToString("P2");
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //LoadRekapan();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //LoadRekapan();
            LoadByDepo();
            LoadRekapan(true);
            LoadByChecker();
            LoadDetail();
            LoadBySupplier();
            btnExport.Enabled = true;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanDepo.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>PEMANTAUAN DEPO</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "   " + ddlTahun.SelectedValue.ToString() + "</b>";
            string HtmlEnd = "";
            ctn.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }
}