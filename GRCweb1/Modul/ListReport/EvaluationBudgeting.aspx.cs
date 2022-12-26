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

namespace GRCweb1.Modul.ListReport
{
    public partial class EvaluationBudgeting : System.Web.UI.Page
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
                LoadDept();
                Bulan = ddlBulan.SelectedItem.Text;
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

        private void LoadTahun()
        {
            PakaiFacade pk = new PakaiFacade();
            pk.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void LoadDept()
        {
            ddlDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));

            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            EvBudget();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LapEvaluasiBudgeting.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            string PlanName = (((Users)Session["Users"]).UnitKerjaID == 7) ? "KARAWANG" : "CITEUREUP";
            sr.Close();
            Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>LAPORAN EVALUASI BUDGETING PLANT " + PlanName + "</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();

        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
        }

        private void EvBudget()
        {
            Bulan = ddlBulan.SelectedValue;
            //ddlDept = ddlDept.SelectedItem.Text;
            PakaiFacade pakaiFacade = new PakaiFacade();
            ArrayList arrData = new ArrayList();
            pakaiFacade.Where = (chkSls.Checked == true) ? " having (x.MaxQty-(sum(x.Quantity)))<0 " : "";
            pakaiFacade.Criteria += (chkNonB.Checked == true) ? " Where x.MaxQty<1 " : "";
            arrData = pakaiFacade.RetrieveEvBudget(ddlBulan.SelectedValue, ddlTahun.SelectedValue, ddlDept.SelectedValue);
            EvBudget1.DataSource = arrData;
            EvBudget1.DataBind();
        }

        protected void EvBudget1_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label selish = (Label)e.Item.FindControl("sls");
            if (selish.Text.Substring(0, 1) == "-")
            {
                selish.Attributes.Add("style", "color:red");
            }
        }

        protected void EvBudget1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }

    }
}