using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.spkp
{
    public partial class PemantauanWIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                masaCuring.Checked = true;
                viewRkp.Checked = true;
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                Response.AddHeader("content-disposition", "attachment;filename=PemantauanWIP.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "<b>REKAP DATA DISIPLIN</b>";
                Html += "<br>Periode : " + txtTanggal.Text;
                Html += (masaCuring.Checked == true) ? "<br>Pemantauan :" + masaCuring.Text + "</b>" : "<br>Pemantauan :" + masaJemur.Text + "</b>";
                string HtmlEnd = "";
                lste.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0", "border=\"1");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message + " " + ex.Source.ToString());
            }
        }
        protected void lst_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Repeater rpt = (Repeater)e.Item.FindControl("lstDetail");
            if (viewRdt.Checked == true)
            {
                SPKP_Master spkp = (SPKP_Master)e.Item.DataItem;
                ReportFacadeSPKP rkp = new ReportFacadeSPKP();
                rkp.Criteria = " and x.PartNo='" + spkp.PartNo + "'";
                arrData = (masaCuring.Checked == true) ? rkp.RetrieveMasaCuring() : rkp.RetrieveMasaJemur();
            }
            rpt.DataSource = arrData;
            rpt.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Al", "_copyHeader()", true);
        }

        private void LoadData()
        {
            ReportFacadeSPKP rkp = new ReportFacadeSPKP();
            ArrayList arrData = new ArrayList();
            arrData = (masaCuring.Checked == true) ? rkp.RetrieveMasaCuring(true) : rkp.RetrieveMasaJemur(true);
            lst.DataSource = arrData;
            lst.DataBind();
        }
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}