using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.Boardmill
{
    public partial class ListProductionTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadLine();
                LoadKelompokProduk();
            }
        }

        private void LoadData()
        {
            ArrayList arrBS = new ArrayList();
            BendingStrengthFacade bsf = new BendingStrengthFacade();


            string line = ddlLine.SelectedItem.Text.ToString();
            string pressing = bsf.groupReportProduksi(ddlPressing.SelectedItem.Text.ToString());

            string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyy-MM-dd");
            string dTglSampai = DateTime.Parse(txtSdTgl.Text).ToString("yyyy-MM-dd");

            arrBS = bsf.RetrieveFormulaTipe2(line, pressing, dTglDari, dTglSampai);
            lstBendingStrength.DataSource = arrBS;
            lstBendingStrength.DataBind();
        }

        private void LoadLine()
        {
            ArrayList arrFormula = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrFormula = bendingSt.RetrieveLine();

            ddlLine.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (BendingStrength Bs in arrFormula)
            {
                ddlLine.Items.Add(new ListItem(Bs.ID.ToString(), Bs.Line.ToString()));
            }
        }

        private void LoadKelompokProduk()
        {
            ArrayList arrFormula = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrFormula = bendingSt.RetrieveKelompokProduk();

            ddlPressing.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (BendingStrength Bs in arrFormula)
            {

                ddlPressing.Items.Add(new ListItem(Bs.Jenis.ToString(), Bs.GroupReport.ToString()));
            }
        }

       

        protected void lstBendingStrength_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Modul/Boardmill/InputBendingStrength.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListProductionTest.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>PRODUCTION TEST REPORT</H2>";
            Html += "<br>Line : " + ddlLine.SelectedItem.Text.ToString();
            Html += "<br>Group Produk : " + ddlPressing.SelectedItem.Text.ToString();
            Html += "<br>Periode : " + txtDrTgl.Text.ToString() + " s/d " + txtSdTgl.Text.ToString();
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
}