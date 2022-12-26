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
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;
using System.ComponentModel;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class JobDeskMonitoring : System.Web.UI.Page
    {
        public string Tahun = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in ListJobDesk.Items)
            {
                Repeater pic = (Repeater)rpt.FindControl("ListJobDesk");
                //((Image)rpt.FindControl("lstEdit")).Visible = false;
                //((Image)rpt.FindControl("lstDel")).Visible = false;
                foreach (RepeaterItem rp in pic.Items)
                {
                    ((Image)rp.FindControl("lstEdit")).Visible = false;
                    ((Image)rp.FindControl("lstDel")).Visible = false;
                }

            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LaporanJobDesk.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>MONITORING UPD JOBDESK</H2>";
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void LoadTahun()
        {
            JobDeskFacade p = new JobDeskFacade();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (JobDesk jobdesk in arrData)
            {
                ddlTahun.Items.Add(new ListItem(jobdesk.Tahun.ToString(), jobdesk.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //Repeater ListJobDesk = (Repeater)e.Item.FindControl("ListJobDesk");
            JobDesk jobdesk = new JobDesk();
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            ArrayList arrData = new ArrayList();
            if (rbShare.SelectedValue == "Share Karawang ke Citereup")
            {
                arrData = jobdeskFacade.RetrieveJOBDESKKrwgCtrp(ddlTahun.SelectedValue);
            }
            else if (rbShare.SelectedValue == "Share Citereup ke Karawang")
            {
                arrData = jobdeskFacade.RetrieveJOBDESKCtrpKrwg(ddlTahun.SelectedValue);
            }
            else if (rbShare.SelectedValue == "Share Karawang ke Jombang")
            {
                arrData = jobdeskFacade.RetrieveJOBDESKKrwgJombang(ddlTahun.SelectedValue);
            }
            else if (rbShare.SelectedValue == "Share Jombang ke Karawang")
            {
                arrData = jobdeskFacade.RetrieveJOBDESKJombangKrwg(ddlTahun.SelectedValue);
            }
            else if (rbShare.SelectedValue == "Share Citereup ke Jombang")
            {
                arrData = jobdeskFacade.RetrieveJOBDESKCtrpJombang(ddlTahun.SelectedValue);
            }
            else
            {
                arrData = jobdeskFacade.RetrieveJOBDESKJombangCtrp(ddlTahun.SelectedValue);
            }

            ListJobDesk.DataSource = arrData;
            ListJobDesk.DataBind();
        }
        protected void ListJobDesk_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            JobDesk jobDesk = (JobDesk)e.Item.DataItem;
            Image cetakJOBDESK = (Image)e.Item.FindControl("lstEdit");
            string querystr = jobDesk.ID.ToString();
            Label lbl = (Label)e.Item.FindControl("slvd");
            Label LblDept = (Label)e.Item.FindControl("LblDept");
            switch (jobDesk.Approval)
            {
                case 0:
                    lbl.Text = "Open";
                    break;
                case 1:
                    lbl.Text = "Approved Hrd >> Mgr Dept";
                    break;
                case 2:
                    lbl.Text = "Approved Manager";
                    break;
                case 3:
                    lbl.Text = "Approved Plant Manager/Corp Manager >> Corp Manager Hrd";
                    break;
                case 4:
                    lbl.Text = "Approved Corp Manager Hrd >> Iso";
                    break;
                case 5:
                    lbl.Text = "Approved Iso >> Distribusi Iso";
                    break;
                case 6:
                    lbl.Text = "Distributed By Iso";
                    break;
                case 7:
                    lbl.Text = "Finish";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }
            switch (jobDesk.DeptID)
            {
                case 2:
                    LblDept.Text = "Boardmill";
                    break;
                case 3:
                    LblDept.Text = "Finishing";
                    break;
                case 6:
                    LblDept.Text = "Logistik Produk Jadi";
                    break;
                case 7:
                    LblDept.Text = "HRD & GA";
                    break;
                case 9:
                    LblDept.Text = "QA";
                    break;
                case 10:
                    LblDept.Text = "Logistik Bahan Baku";
                    break;
                case 11:
                    LblDept.Text = "PPIC";
                    break;
                case 12:
                    LblDept.Text = "Finance";
                    break;
                case 13:
                    LblDept.Text = "Marketing";
                    break;
                case 14:
                    LblDept.Text = "IT";
                    break;
                case 15:
                    LblDept.Text = "Purchasing";
                    break;
                case 19:
                    LblDept.Text = "Maintenace";
                    break;
                case 22:
                    LblDept.Text = "Project";
                    break;
                case 23:
                    LblDept.Text = "Iso";
                    break;
                case 24:
                    LblDept.Text = "Accounting";
                    break;
                case 26:
                    LblDept.Text = "Armada";
                    break;
                case 30:
                    LblDept.Text = "Research & Development";
                    break;

            }
        }
        protected void ListJobDesk_Command(object sender, RepeaterCommandEventArgs e)
        {
            JobDesk jobDesk = (JobDesk)e.Item.DataItem;
            if (e.CommandName == "Add")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                Label IDstr = (Label)e.Item.FindControl("id");
                //TextBox ID = (TextBox)e.Item.FindControl("id");
                //GridViewRow row = GridView1.Rows[index];

                Response.Redirect("JobDeskInput.aspx?ID=" + IDstr.Text);

            }
        }
    }
}