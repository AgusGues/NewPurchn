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

namespace GRCweb1.Modul.RMM
{
    public partial class RekapRMM : System.Web.UI.Page
    {
        public string Tahun = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadTahun2();
                LoadDept();
                LoadBulan();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstDept.Items)
            {
                Repeater pic = (Repeater)rpt.FindControl("ListRMM1");
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
            Response.AddHeader("content-disposition", "attachment;filename=ListRMM.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>LIST DATA RMM</H2>";
            Html += "<br>Periode : " + ddlSemester.SelectedItem.Text + " &nbsp; " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Departement : &nbsp;" + ddlDept.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        //protected void chkRbb_Checked(object sender, EventArgs e)
        //{
        //    CheckBox chk = (CheckBox)sender;
        //    int idx = int.Parse(chk.CssClass);
        //    string verf = string.Empty;
        //    if (chk.Checked == true)
        //        verf = "1";
        //    else
        //        verf = "0";
        //    RMMDetailFacade rmmdf = new RMMDetailFacade();
        //    string strerror = rmmdf.UpdateRMMDetailverf(chk.CssClass.Rows[idx.RowIndex].Cells[1].Text, verf);
        //    //LoadDetail(txtRMM_No.Text.Trim());
        //}

        protected void RBSemeteran_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSemesteran.Checked == true)
            {
                Panel3.Visible = true;
                Panel1.Visible = false;
            }
        }
        protected void RBBulanan_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBulan.Checked == true)
            {
                Panel3.Visible = false;
                Panel1.Visible = true;
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //Bulan = ddlBulan.SelectedItem.Text;
            ListRMM();
            //LoadDataRMM();
        }

        protected void btnPreview2_Click(object sender, EventArgs e)
        {
            //Bulan = ddlBulan.SelectedItem.Text;
            //ListRMM();
            //LoadDataRMM();
        }

        protected void ddlSemester_Change(object sender, EventArgs e)
        {
            // Bulan = ddlBulan.SelectedItem.Text;
        }

        protected void ddlDept_Change(object sender, EventArgs e)
        {

        }

        protected void ddlTahun_Change(object sender, EventArgs e)
        {

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

        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater ListRMM1 = (Repeater)e.Item.FindControl("ListRMM1");
            Domain.RMM rmm = (Domain.RMM)e.Item.DataItem;
            RMMFacade rmmFacade = new RMMFacade();
            ArrayList arrData = new ArrayList();
            if (RBSemesteran.Checked == true)
            {
                arrData = rmmFacade.RetrieveRecapRMM(ddlSemester.SelectedValue, ddlTahun.SelectedValue, rmm.RMM_DeptID.ToString());
            }
            else
            {
                arrData = rmmFacade.RetrieveRecapRMMMonth(ddlBulan.SelectedValue, ddlTahun.SelectedValue, rmm.RMM_DeptID.ToString());//(rmm.RMM_DeptID.ToString(),int.Parse(ddlBulan.SelectedValue.ToString()));
            }

            ListRMM1.DataSource = arrData;
            ListRMM1.DataBind();

        }

        private void ListRMM()
        {
            RMMFacade rf = new RMMFacade();
            arrData = new ArrayList();
            arrData = rf.RetrieveRecapRMM(ddlDept.SelectedValue.ToString());
            lstDept.DataSource = arrData;
            lstDept.DataBind();

        }

        protected void ListRMM1_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Domain.RMM rMM = (Domain.RMM)e.Item.DataItem;
            RMM_Detail rmdt = new RMM_Detail();
            Image edtRMM = (Image)e.Item.FindControl("lstEdit");
            Image delRMM = (Image)e.Item.FindControl("lstDel");
            string querystr = rMM.IDx.ToString();//rMM.ID.ToString();
                                                 // string querystr1 = rmdt.Aktivitas.ToString();
            edtRMM.Attributes.Add("onclick", "updateDO(" + querystr.ToString() + ")");
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "RMM").Split(',');
            delRMM.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            edtRMM.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            Label lbl = (Label)e.Item.FindControl("slvd");
            switch (rMM.Solved)
            {
                case 0:
                    lbl.Text = "UnSolved";
                    break;
                case 1:
                    lbl.Text = "Solved";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }

        }

        protected void ListRMM1_Command(object sender, RepeaterCommandEventArgs e)
        {

        }
        //private void LoadDataRMM()
        //{
        //    //Bulan = ddlBulan.SelectedValue;
        //    RMM rmmf1 = new RMM();
        //    RMMFacade rmmf = new RMMFacade();
        //    ArrayList arrData = new ArrayList();
        //    arrData = rmmf.RetrieveRecapRMM(ddlSemester.SelectedValue, ddlTahun.SelectedValue, rmmf1.RMM_DeptID.ToString());
        //    lstDept.DataSource = arrData;//Mpo.DataSource = arrData;
        //    lstDept.DataBind();//Mpo.DataBind();
        //}

        private void LoadTahun()
        {
            RMMFacade p = new RMMFacade();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (Domain.RMM rmm in arrData)
            {
                ddlTahun.Items.Add(new ListItem(rmm.Tahun.ToString(), rmm.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }

        private void LoadTahun2()
        {
            RMMFacade p = new RMMFacade();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun2.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun2.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (Domain.RMM rmm in arrData)
            {
                ddlTahun2.Items.Add(new ListItem(rmm.Tahun.ToString(), rmm.Tahun.ToString()));
            }
            ddlTahun2.SelectedValue = DateTime.Now.Year.ToString();

        }

        private void LoadDept()
        {
            ddlDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            RMM_Dept sarmutdept = new RMM_Dept();
            RMM_DeptFacade deptFacade = new RMM_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (RMM_Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            //sarmutdept = deptFacade.GetUserDept(user.ID);
            ddlDept.SelectedValue = sarmutdept.ID.ToString();
        }
    }
}