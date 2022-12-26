using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.spkp
{
    public partial class LaporanAngkat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnPrint);
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTgljemur.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Session["tglJemur"] = null;
            LoadDataGrid();
            btnPrint.Enabled = true;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=LaporanWIPSiapPotong.xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                lst.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString().Replace("> ", ">"));
                Response.End();
                btnPrint.Enabled = false;
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message + " " + ex.Source.ToString());
            }
        }

        private void LoadDataGrid()
        {

            ArrayList arrAngkat = new ArrayList();
            ArrayList arrTotal = new ArrayList();
            SPKP_Master sp = new SPKP_Master();
            string tgl = Convert.ToDateTime(txtTgljemur.Text).ToString("yyyyMMdd");
            Session["tglJemur"] = tgl;
            arrAngkat = new ReportFacadeSPKP().RetrievePartNo();
            lstPartNo.DataSource = arrAngkat;
            lstPartNo.DataBind();
            foreach (SPKP_Master spk in arrAngkat)
            {
                sp.Total = spk.Total;
            }
            arrTotal.Add(sp);
        }
        protected void lstPartNo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (lstDetail.Checked == true)
            {
                ArrayList arrAngkatDatail = new ArrayList();
                var lst = e.Item.FindControl("lstAngkat") as Repeater;
                SPKP_Master spk = (SPKP_Master)e.Item.DataItem;
                if (lst != null)
                {
                    arrAngkatDatail = new ReportFacadeSPKP().RetrieveSiapPotong(spk.PartNo.ToString());
                    lst.DataSource = arrAngkatDatail;
                    lst.DataBind();
                }
            }
        }
        protected void lstDetail_Checked(object sender, EventArgs e)
        {
            if (lstDetail.Checked == true) LoadDataGrid();
        }
        protected void lstRekap_Checked(object sender, EventArgs e)
        {
            if (lstRekap.Checked == true) LoadDataGrid();
        }
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}