using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32;
using System.Management;
using System.Data.OleDb;
using System.IO;
using System.Web.SessionState;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using System.Xml.Serialization;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;


namespace GRCweb1.Modul.Purchasing
{
    public partial class HistoryPO : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                //WebReference_Ctrp.Service1 webserviceCtrp = new WebReference_Ctrp.Service1();
                //DataSet dsTest = webserviceCtrp.GetPOByNo();
                Users users = (Users)Session["Users"];
                if (users.UserName.Trim() == "Admin")
                {
                    btnExport.Visible = true;
                }
            }

            if (ViewState["report1"] == null)
            {
                Response.Expires = 0;
                LoadData();
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            if (RBReport.Checked == true)
            {
                Cetak(this);
            }
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            if (TxtValue.Text == string.Empty)
            {
                return;
            }
            HistPOFacade histPOFacade = new HistPOFacade();
            ArrayList arrHistPO = new ArrayList();
            Users users = (Users)Session["Users"];
            string strPODetail = string.Empty;
            if (ddlSearch.SelectedIndex == 2 || ddlSearch.SelectedIndex == 3 || ddlSearch.SelectedIndex == 4 || ddlSearch.SelectedIndex == 5)
            {

                if (users.ViewPrice > 0)
                {
                    if (users.ViewPrice == 1)
                    {
                        arrHistPO = histPOFacade.ViewHistPO(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                        strPODetail = histPOFacade.ViewHistPORpt(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                    }
                    if (users.ViewPrice == 2)
                    {
                        arrHistPO = histPOFacade.ViewHistPO2(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                        strPODetail = histPOFacade.ViewHistPORpt2(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                    }
                }
                else
                {
                    arrHistPO = histPOFacade.ViewHistPOByPrice0(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                    strPODetail = histPOFacade.ViewHistPOByPrice0Rpt(ddlSearch.SelectedValue, TxtValue.Text.Trim(), ")");
                }
            }
            else
                if (users.ViewPrice > 0)
            {
                if (users.ViewPrice == 1)
                {
                    arrHistPO = histPOFacade.ViewHistPO(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
                    strPODetail = histPOFacade.ViewHistPORpt(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
                }
                if (users.ViewPrice == 2)
                {
                    arrHistPO = histPOFacade.ViewHistPO2(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
                    strPODetail = histPOFacade.ViewHistPORpt2(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
                }
            }
            else
            {
                arrHistPO = histPOFacade.ViewHistPOByPrice0(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
                strPODetail = histPOFacade.ViewHistPOByPrice0Rpt(ddlSearch.SelectedValue, TxtValue.Text.Trim(), " ");
            }

            if (histPOFacade.Error == string.Empty)
            {
                Session["arrhistPO"] = null;
                GridView1.DataSource = arrHistPO;
                GridView1.DataBind();
                Session["arrhistPO"] = arrHistPO;
            }
            Session["Query"] = strPODetail;
            if (crViewer.Visible == true)
            {

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RBgrid_CheckedChanged(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            crViewer.Visible = false;
            if (TxtValue.Text != string.Empty)
            {
                LoadData();
            }
        }
        protected void RBReport_CheckedChanged(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            crViewer.Visible = true;
            if (TxtValue.Text != string.Empty)
            {
                if (RBReport.Checked == true)
                {
                    LoadData();
                    Cetak(this);
                }
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=RekapHistPO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void CetakExcel(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=RekapHistPOExcel', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (RBgrid.Checked == true)
            {
                ArrayList arrHistPO = new ArrayList();
                arrHistPO = (ArrayList)Session["arrhistPO"];
                try
                {

                    string Path = "D:\\ExportExcel\\myexcelfile_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls";
                    FileInfo FI = new FileInfo(Path);
                    StringWriter stringWriter = new StringWriter();
                    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
                    DataGrid DataGrd = new DataGrid();
                    DataGrd.DataSource = arrHistPO;
                    DataGrd.DataBind();


                    DataGrd.RenderControl(htmlWrite);
                    string directory = Path.Substring(0, Path.LastIndexOf("\\"));
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
                    stringWriter.ToString().Normalize();
                    vw.Write(stringWriter.ToString());
                    vw.Flush();
                    vw.Close();
                    WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
                }
                catch (Exception ex)
                {

                }
                finally
                {


                }
            }
            else
            {
                LoadData();
                CetakExcel(this);
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        public static void WriteAttachment(string FileName, string FileType, string content)
        {
            try
            {
                HttpResponse Response = System.Web.HttpContext.Current.Response;
                Response.ClearHeaders();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.ContentType = FileType;
                Response.Write(content);
                Response.End();
            }
            catch (Exception ex)
            {


            }


        }

    }
}