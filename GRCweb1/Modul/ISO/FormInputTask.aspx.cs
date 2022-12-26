using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GRCweb1.Modul.ISO
{
    public partial class FormInputTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session["TaskId"] = null;
                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;

                Global.link = "~/Default.aspx";

                LoadDept();
                LoadPIC();
                //ddlPIC_SelectedIndexChanged(null, null);
                //LoadDataGrid(LoadGrid());
                if (Session["ListOfReceipt"] != null)
                {
                    //LoadReceiptBySession();
                    lbUpdateTask.Enabled = true;

                }
                else if (Request.QueryString["TaskNo"] != null || Request.QueryString["id"] != null)
                {
                    lbUpdateTask.Enabled = true;

                    //LoadTask(Request.QueryString["TaskNo"].ToString());
                    LoadTask(Request.QueryString["id"].ToString());
                }
                else
                {
                    clearForm();
                    txtTahun.Text = DateTime.Now.Year.ToString();
                }


            }
            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
            //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(lbUpdateTask);
        }
        private void LoadTask(string taskNo)
        {
            TaskFacade taskFacade = new TaskFacade();
            int cekStatusDetail = 0;
            decimal pointNilai = 0;
            DateTime tglSelesai = DateTime.MaxValue;

            ArrayList arrTask = new ArrayList();
            arrTask = taskFacade.RetrieveByIDnew(taskNo);
            //with B.Status
            int staTask = 0;
            int appTask = 0;
            if (arrTask.Count > 0)
            {
                foreach (Task task in arrTask)
                {
                    Session["id"] = task.ID;
                    btnSave.Disabled = true;

                    ViewState["NoTask"] = task.TaskNo;

                    txtTaskNo.Text = task.TaskNo;
                    txtTglMulai.Text = task.TglMulai.ToString("dd-MMM-yyyy");
                    txtKeterangan.Text = task.NewTask;
                    //txtTglTarget.Text = task.TglTarget.ToString("dd-MMM-yyyy");
                    int tgl = task.TglTarget.Day;
                    int bln = task.TglTarget.Month;
                    int year = task.TglTarget.Year;

                    DateTime akhirBln = new DateTime(year, bln, DateTime.DaysInMonth(year, bln));

                    if (task.TglTarget.Day <= 7)
                        ddlTargetM.SelectedIndex = 1;
                    else if (task.TglTarget.Day <= 14)
                        ddlTargetM.SelectedIndex = 2;
                    else if (task.TglTarget.Day <= 21)
                        ddlTargetM.SelectedIndex = 3;
                    else if (akhirBln.Day == tgl)
                        ddlTargetM.SelectedIndex = 4;
                    ddlBulan.SelectedIndex = bln;
                    txtTahun.Text = year.ToString();
                    txtTglTarget.Text = task.TglTarget.ToString("dd-MMM-yyyy");
                    txtTglTargetAktip.Text = task.TglTarget.ToString("dd-MMM-yyyy");
                    ddlPIC.ClearSelection();
                    foreach (ListItem item in ddlPIC.Items)
                    {
                        // item.Selected = false;
                        if (item.Text == task.Pic.ToString())
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }

                    txtPic.Text = task.CreatedBy;
                    ddlDept.SelectedValue = task.DeptID.ToString();
                    //SelectDept(task.DeptID.ToString());

                    LoadSection();
                    //SelectSection(task.BagianID.ToString());
                    ddlBagian.SelectedValue = task.BagianID.ToString();
                    LoadCategory(2);
                    SelectCategory(task.CategoryID.ToString());
                    txtBobotNilai.Text = task.BobotNilai.ToString();

                    ddlStatus.SelectedIndex = (task.Status < 9) ? task.TargetKe : 8;
                    staTask = task.Status;
                    appTask = task.App;
                    if (task.Status == 2 && task.App == 2)
                    {
                        cekStatusDetail = 1;
                        pointNilai = task.PointNilai;
                        tglSelesai = task.TglSelesai;
                        ddlStatus.SelectedIndex = 7;
                        lbUpdateTask.Enabled = false;
                        ddlStatus.Enabled = false;
                    }
                    //ddlStatus.Enabled = (task.App == 2) ? true : false;
                    txtAlasanCancel.Visible = (task.Status == 9) ? true : false;
                    txtAlasanCancel.Text = task.AlasanCancel.ToString();
                    ifCancel.Visible = (task.Status == 9) ? true : false;
                    btnCancel.Enabled = (task.Status == 9) ? false : true;
                    txtTglSelesai.Text = (task.Status == 9 || task.Status == 2) ? task.TglSelesai.ToString("dd-MMM-yyyy") : string.Empty;
                    lbUpdateTask.Enabled = (task.Status == 9 || task.Status == 2) ? false : true;
                    if (task.App < 2)
                    {
                        txtApproval.Text = "Belum di-Approval";
                        lbUpdateTask.Enabled = false;
                    }
                    else
                    {
                        txtApproval.Text = "Sudah di-Approval";

                        ddlDept.Enabled = false;
                        ddlPIC.Enabled = false;
                        ddlBagian.Enabled = false;
                        txtTglMulai.ReadOnly = true;
                        txtTglMulai.Enabled = false;
                        ddlCategory.Enabled = false;
                        txtBobotNilai.ReadOnly = true;
                        txtKeterangan.ReadOnly = true;
                        lbUpdateTask.Enabled = (task.Status == 9 || task.Status == 2) ? false : true;
                    }
                    //btnCancel.Enabled = true;
                }
            }
            if (cekStatusDetail > 0)
            {
                lbUpdateTask.Enabled = (staTask == 9 || staTask == 2) ? false : true;
                txtPointNilai.Text = pointNilai.ToString();
                txtTglSelesai.Text = tglSelesai.ToString("dd-MMM-yyyy");
                btnCancel.Enabled = (staTask == 9 || staTask == 2) ? false : true;
            }
            else
            {
                lbUpdateTask.Enabled = (staTask == 9 || staTask == 2 || appTask == 0) ? false : true;
                btnCancel.Enabled = (staTask == 9 || staTask == 2) ? false : true;
            }



            this.GridView1.DataSource = arrTask;
            this.GridView1.DataBind();
            lstTask.DataSource = arrTask;
            lstTask.DataBind();

            Session["ListOfTask"] = arrTask;
        }
        private void clearForm()
        {
            Session["id"] = null;

            ddlTargetM.SelectedIndex = 0;
            ddlBulan.SelectedIndex = 0;
            txtTahun.Text = string.Empty;

            txtTglMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTglSelesai.Text = string.Empty;
            txtTglTarget.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTaskNo.Text = string.Empty;
            txtPic.Text = (((Users)Session["Users"]).UserName);
            txtKeterangan.Text = string.Empty;
            txtBobotNilai.Text = string.Empty;
            txtPointNilai.Text = string.Empty;
            txtTahun.Text = DateTime.Now.Year.ToString();
            ddlDept.SelectedIndex = 0;
            //ddlBagian.SelectedIndex = 0;
            ddlBagian.Items.Clear();
            //ddlCategory.SelectedIndex = 0;
            ddlCategory.Items.Clear();
            ddlStatus.SelectedIndex = 0;

            btnSave.Disabled = false;
            lbUpdateTask.Enabled = false;

            ddlDept.Enabled = true;
            ddlPIC.Enabled = true;
            ddlBagian.Enabled = true;
            txtTglMulai.ReadOnly = false;
            txtTglMulai.Enabled = true;
            ddlCategory.Enabled = true;
            txtBobotNilai.ReadOnly = false;
            txtKeterangan.ReadOnly = false;
            resultMailSucc.Visible = false;
            ddlStatus.Enabled = true;
            ddlStatus.SelectedIndex = 1;
            ArrayList arrList = new ArrayList();
            lstTask.DataSource = arrList;
            lstTask.DataBind();
            arrList.Add(new Task());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }
        protected void lstTask_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("sts");
            Task t = (Task)e.Item.DataItem;
            switch (t.App)
            {
                case 0:
                    lbl.Text = "Open";
                    break;
                case 1:
                    lbl.Text = "UnSolved";
                    break;
                case 2:
                    lbl.Text = (t.Status == 2) ? "Solved" : "Approved";
                    break;
                case 9:
                    lbl.Text = "Cancel";
                    break;
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "Open";
                else if (e.Row.Cells[4].Text == "1")
                    e.Row.Cells[4].Text = "UnSolved";
                else if (e.Row.Cells[4].Text == "2")
                    e.Row.Cells[4].Text = "Solved";
                else if (e.Row.Cells[4].Text == "-1")
                    e.Row.Cells[4].Text = "Cancel";
                else
                    e.Row.Cells[4].Text = "??";

                e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy");

                if (int.Parse(e.Row.Cells[2].Text) <= 6)
                    e.Row.Cells[2].Text = "T" + e.Row.Cells[2].Text;

                //e.Row.Cells[6].Text = Decimal.Parse(e.Row.Cells[6].Text).ToString("N2");
                //e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            //if (txtSearch.Text == string.Empty)
            //    LoadDataGridItems(LoadGridItems());
            //else
            //    LoadDataGridItems(LoadGridByCriteria());
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Session["id"] = int.Parse(row.Cells[0].Text);

            }
        }
        private void LoadPIC()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
            string inDeptid = usersFacade.GetDeptOto(users2.ID);
            string[] arrDept = inDeptid.Split(',');
            ISO_UserFacade isoUserFacade = new ISO_UserFacade();
            isoUserFacade.TypePES = (arrDept.Contains("25") || arrDept.Contains("27")) ? "" : "2";
            ArrayList arrIsoUser = isoUserFacade.RetrievePIC(users2.DeptID.ToString());
            // isoUserFacade.RetrieveByCriteria("DeptID", users2.DeptID.ToString());

            if (arrIsoUser.Count > 0)
            {
                ddlPIC.Items.Clear();
                ddlPIC.Items.Add(new ListItem("-- Choose PIC --", "0"));

                foreach (ISO_Users iso_User in arrIsoUser)
                {
                    ddlPIC.Items.Add(new ListItem(iso_User.UserName, iso_User.ID.ToString()));
                }
                //ddlPIC_SelectedIndexChanged(null, null);
                //ddlPIC.ClearSelection();
                //foreach (ListItem item in ddlPIC.Items)
                //{
                //    //ddlPIC.SelectedIndex = ddlPIC.SelectedIndex + 1;
                //    if (item.Text == ((Users)Session["Users"]).UserName)
                //    {

                //        item.Selected = true;
                //        //ddlDept.SelectedIndex = 0;

                //        return;
                //    }
                //    //else
                //    //{
                //    //    item.Selected = false;
                //    //}
                //}
                //ddlPIC.SelectedValue = ((Users)Session["Users"]).ID.ToString();
                ////ddlPIC_SelectedIndexChanged();
            }

        }

        private void LoadDept()
        {
            try
            {
                Users users = (Users)Session["Users"];

                UsersFacade usersFacade = new UsersFacade();
                DeptFacade deptFacade = new DeptFacade();
                Users users2 = usersFacade.RetrieveByUserName(users.UserName);
                Dept UserGrp = deptFacade.RetrieveDeptByUserID2(users2.ID);
                ArrayList arrDept = (UserGrp.UserGroupID == 100) ? deptFacade.RetrieveByUserID(users2.ID, true) : deptFacade.RetrieveByUserID(users2.ID);

                ddlDept.Items.Clear();
                if (deptFacade.Error == string.Empty)
                {
                    ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));

                    foreach (Dept dept in arrDept)
                    {
                        ddlDept.Items.Add(new ListItem(dept.DeptName, dept.DeptID.ToString()));
                    }
                }
                ddlDept.SelectedValue = users.DeptID.ToString();
            }
            catch { }
        }
        private void LoadSection()
        {
            DeptFacade deptFacade = new DeptFacade();
            Users usr = (Users)Session["Users"];
            ArrayList arrDept = deptFacade.RetrieveByUserID(((Users)Session["Users"]).ID);
            string inDept = new UsersFacade().GetDeptOto(((Users)Session["Users"]).ID);
            DeptFacade deptFacade2 = new DeptFacade();
            //ArrayList arrDept2 = deptFacade2.RetrieveSection(usr.ID,usr.DeptID.ToString());
            ArrayList arrDept2 = deptFacade2.RetrieveSection(int.Parse(ddlPIC.SelectedValue), ddlDept.SelectedValue);
            ddlBagian.Items.Clear();
            if (deptFacade2.Error == string.Empty)
            {
                ddlBagian.Items.Add(new ListItem("-- Choose Section --", "0"));

                foreach (Dept dept in arrDept2)
                {
                    ddlBagian.Items.Add(new ListItem(dept.BagianName, dept.BagianID.ToString()));
                }
            }
        }
        private void LoadCategory(int pestype)
        {
            CategoryFacade catFacade = new CategoryFacade();
            ArrayList arrCategory = catFacade.RetrieveByPesType(pestype);


            ddlCategory.Items.Clear();
            if (catFacade.Error == string.Empty)
            {
                ddlCategory.Items.Add(new ListItem("-- Choose Category --", "0"));

                foreach (Category cat in arrCategory)
                {
                    ddlCategory.Items.Add(new ListItem(cat.CategoryDescription, cat.ID.ToString()));
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            TaskFacade taskFacade = new TaskFacade();

            ArrayList arrTask = new ArrayList();
            arrTask = taskFacade.RetrieveByNo(txtSearch.Text);
            if (arrTask.Count > 0)
            {
                foreach (Task task in arrTask)
                {
                    Session["id"] = task.ID;
                    btnSave.Disabled = true;

                    ViewState["NoTask"] = task.TaskNo;

                    txtTaskNo.Text = task.TaskNo;
                    txtTglMulai.Text = task.TglMulai.ToString("dd-MMM-yyyy");
                    txtKeterangan.Text = task.NewTask;
                    //txtTglTarget.Text = task.TglTarget.ToString("dd-MMM-yyyy");
                    int tgl = task.TglTarget.Day;
                    int bln = task.TglTarget.Month;
                    int year = task.TglTarget.Year;

                    DateTime akhirBln = new DateTime(year, bln, DateTime.DaysInMonth(year, bln));

                    if (task.TglTarget.Day <= 7)
                        ddlTargetM.SelectedIndex = 1;
                    else if (task.TglTarget.Day <= 14)
                        ddlTargetM.SelectedIndex = 2;
                    else if (task.TglTarget.Day <= 21)
                        ddlTargetM.SelectedIndex = 3;
                    else if (akhirBln.Day == tgl)
                        ddlTargetM.SelectedIndex = 4;

                    ddlBulan.SelectedIndex = bln;
                    txtTahun.Text = year.ToString();

                    txtPic.Text = task.CreatedBy;
                    if (task.App == 0)
                    { txtApproval.Text = "Belum di-Approval"; }
                    else
                    { txtApproval.Text = "Sudah di-Approval"; }
                    int cekStatusDetail = 0;
                    decimal pointNilai = 0;
                    DateTime tglSelesai = DateTime.MaxValue;
                    if (task.Status == 2)
                    {
                        cekStatusDetail = 1;
                        pointNilai = task.PointNilai;
                        tglSelesai = task.TglSelesai;
                    }
                    if (cekStatusDetail > 0)
                    {
                        txtPointNilai.Text = pointNilai.ToString();
                        txtTglSelesai.Text = tglSelesai.ToString("dd-MMM-yyyy");
                    }

                    SelectDept(task.DeptID.ToString());

                    LoadSection();
                    SelectSection(task.BagianID.ToString());

                    LoadCategory(2);
                    SelectCategory(task.CategoryID.ToString());
                    txtBobotNilai.Text = task.BobotNilai.ToString();

                    ddlStatus.SelectedIndex = task.TargetKe;
                }
            }

            this.GridView1.DataSource = arrTask;
            this.GridView1.DataBind();
        }
        private void SelectDept(string strDepo)
        {
            ddlDept.ClearSelection();
            foreach (ListItem item in ddlDept.Items)
            {
                if (item.Value == strDepo)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        private void SelectSection(string strDepo)
        {

            ddlBagian.ClearSelection();
            foreach (ListItem item in ddlBagian.Items)
            {
                if (item.Value == strDepo)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        private void SelectCategory(string strDepo)
        {

            ddlCategory.ClearSelection();
            foreach (ListItem item in ddlCategory.Items)
            {
                if (item.Value == strDepo)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = ((Users)Session["Users"]).DeptID.ToString();// dept.DeptID.ToString();

            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=UnSolved" + "&FormTask=1");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {

            if (Session["ArrImgLampiran"] != null)
            {
                int z = 0;
                ArrayList arrImgLampiran = new ArrayList();
                arrImgLampiran = (ArrayList)Session["ArrImgLampiran"];
                while (z != arrImgLampiran.Count)
                {
                    File.Delete(this.Server.MapPath("~\\Resource_Web\\Lampiran_iso\\" + arrImgLampiran[z]));
                    z++;
                }

                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
            if (Session["id"] != null)
            {
                int usergroupid = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select usergroupid from iso_dept where userid in (select userid from iso_users where id=" + ddlPIC.SelectedValue + ")";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        usergroupid = Convert.ToInt32(sdr["usergroupid"].ToString());
                    }
                }
                Task task = new Task();
                if (Session["AlasanCancel"] != null)
                {
                    task.ID = (int)Session["id"];
                    task.Status = 9;
                    task.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    task.Ket = Session["AlasanCancel"].ToString();
                    TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());
                    string strError = taskProcessFacade.CancelTask();

                    if (strError == string.Empty)
                    {
                        UsersFacade usersFacade = new UsersFacade();
                        Users users3 = usersFacade.RetrieveForMgrTask(ddlDept.SelectedItem.Text, usergroupid);
                        string emailaddress = users3.UsrMail.Trim();
                        emailaddress += (((Users)Session["Users"]).UnitKerjaID == 7) ? ";iso_krwg@grcboard.com" : ";iso_ctrp@grcboard.com";
                        KirimEmail(emailaddress);
                        Session["AlasanCancel"] = null;
                        clearForm();
                        ArrayList arrData = new ArrayList();
                        lstTask.DataSource = arrData;
                        lstTask.DataBind();
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Task tidak dapat di cancel");
                    }

                }
                else
                {
                    DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong");
                }
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            btnSave.Disabled = false;
            btnCancel.Enabled = false;
            if (Session["ArrImgLampiran"] != null)
            {
                int z = 0;
                ArrayList arrImgLampiran = new ArrayList();
                arrImgLampiran = (ArrayList)Session["ArrImgLampiran"];
                while (z != arrImgLampiran.Count)
                {
                    File.Delete(this.Server.MapPath("~\\Resource_Web\\Lampiran_iso\\" + arrImgLampiran[z]));
                    z++;
                }

                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
            Session["AlasanCancel"] = null;
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;
            if (txtTaskNo.Text != string.Empty)
            {
                strQuery = reportFacade.ViewFormTask(txtTaskNo.Text, true);
                Session["Query"] = strQuery;
                Cetak(this);
            }
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=FormTask', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrTaskDetail = new ArrayList();
            ArrayList arrImgLampiran = new ArrayList();
            string strEvent = "Insert";

            int usergroupid = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select usergroupid from iso_dept where userid in (select userid from iso_users where id=" + ddlPIC.SelectedValue + ")";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    usergroupid = Convert.ToInt32(sdr["usergroupid"].ToString());
                }
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Status ");
                return;
            }
            if (ddlDept.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Departement ");
                return;
            }
            if (ddlPIC.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih PIC ");
                return;
            }
            if (ddlBagian.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Section ");
                return;
            }
            if (ddlCategory.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Category ");
                return;
            }
            if (txtTaskNo.Text == string.Empty && ddlStatus.SelectedIndex == 7)
            {
                DisplayAJAXMessage(this, "Pilih Status T1 dahulu ");
                return;
            }

            /**
             * Target di rubah jadi tanggal tidak perlu pilih minggu
             * Base on wo dari ISO
             * befor lebaran 2015
             * dibatalkan / di kembalikan lagi ke semula M1 dst on 28-08-2015
             **/
            if (ddlTargetM.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Target M");
                return;
            }
            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;
            }

            int tgl = 0;
            int bln = 0;
            int thn = int.Parse(txtTahun.Text);

            if (ddlTargetM.SelectedIndex == 1)
            {
                tgl = 7;
            }
            else if (ddlTargetM.SelectedIndex == 2)
            {
                tgl = 14;
            }
            else if (ddlTargetM.SelectedIndex == 3)
            {
                tgl = 21;
            }
            else if (ddlTargetM.SelectedIndex == 4)
            {
                DateTime final = new DateTime(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex));

                tgl = final.Day;
            }
            else
            {
                tgl = DateTime.Parse(txtTglTarget.Text).Day;
            }

            DateTime TglTarget = new DateTime(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex, tgl, 0, 0, 0);



            Task task = new Task();

            TaskFacade taskFacade = new TaskFacade();
            if (ViewState["id"] != null)
            {
                task.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            task.NewTask = txtKeterangan.Text;

            task.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            int pjgDept = ddlDept.SelectedItem.ToString().Length;
            if (pjgDept >= 4)
                task.DeptName = ddlDept.SelectedItem.ToString().Substring(0, 3);
            else
                task.DeptName = ddlDept.SelectedItem.ToString().Substring(0, pjgDept);


            if (rbTask0.Checked == true)
                task.TaskType = 0;
            else
                task.TaskType = 1;

            task.BagianID = Convert.ToInt32(ddlBagian.SelectedValue);
            task.TglMulai = DateTime.Parse(txtTglMulai.Text);
            task.TglTarget = TglTarget;//DateTime.Parse(txtTglTarget.Text);//
            task.CategoryID = int.Parse(ddlCategory.SelectedValue);
            //task.BobotNilai = decimal.Parse(txtBobotNilai.Text);
            task.BobotNilai = Convert.ToInt32(decimal.Parse(txtBobotNilai.Text));
            task.Ket = txtKeterangan.Text;
            //if (txtTglSelesai.Text != string.Empty)
            //    task.TglSelesai = DateTime.Parse(txtTglSelesai.Text);
            //else
            //    task.TglSelesai = new DateTime(DateTime.Now.Date.Year,1,1);

            //task.Pic = txtPic.Text;

            ISO_UserFacade iso_UserFacade = new ISO_UserFacade();
            ISO_Users isoUser = iso_UserFacade.RetrieveByUserID(((Users)Session["Users"]).ID.ToString());
            if (isoUser.ID > 0)
            {
                task.Iso_UserID = isoUser.ID;

            }
            else
            {
                DisplayAJAXMessage(this, "Belum terdaftar pada ISO-User ");

                return;
            }

            // start --add by Razib Wo WO-IT-K0040319 
            DateTime startTime = Convert.ToDateTime(txtTglMulai.Text);

            int mDate = DateTime.DaysInMonth(startTime.Year, startTime.AddMonths(2).Month);

            string tanggal = startTime.ToString("dd");

            if (Convert.ToInt32(tanggal) > mDate)
            {
                tanggal = mDate.ToString();
            }

            string bulan = startTime.AddMonths(2).ToString("MMM");
            int intbulan = Convert.ToInt32(startTime.ToString("MM"));
            string tahun = string.Empty;
            if (intbulan <= 10)
                tahun = startTime.ToString("yyyy");
            else
                tahun = (Convert.ToInt32(startTime.ToString("yyyy")) + 1).ToString();
            DateTime datelock = DateTime.Parse(tanggal + "-" + bulan + "-" + tahun);
            DateTime endTime = TglTarget;
            //DateTime finishTime = Convert.ToDateTime(txtTglSelesai.Text);
            //string tanggal2 = finishTime.ToString("dd");
            //string bulan2 = finishTime.AddMonths(2).ToString("MMM");
            //string tahun2 = finishTime.ToString("yyyy");
            //DateTime datelock2 = DateTime.Parse(tanggal2 + "-" + bulan2 + "-" + tahun2);
            //int month = endTime.Month - startTime.Month;
            if (startTime >= endTime)
            {
                DisplayAJAXMessage(this, "Tanggal Target harus melebihi Tanggal Mulai ");
                return;
            }
            if (endTime >= datelock)
            {
                DisplayAJAXMessage(this, "Tanggal Target Tidak Boleh Lebih dari 2 Bulan ");
                return;
            }
            //if (datelock2 <= datelock)
            //{
            //    DisplayAJAXMessage(this, "Tanggal Target Tidak Kurang dari Tanggal Mulai ");
            //    return;
            //}
            // end

            task.Pic = ddlPIC.SelectedItem.ToString();
            task.CreatedBy = (((Users)Session["Users"]).UserName);

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);

            DeptFacade deptFacade3 = new DeptFacade();
            Dept dept2 = deptFacade3.RetrieveDeptByUserGroup(users2.ID);

            task.UserID = users2.ID;
            task.UserGroupID = dept2.UserGroupID;
            task.TargetKe = ddlStatus.SelectedIndex;
            task.App = 0;

            //string NoTask = (taskFacade.GetLastTaskNo() + 1).ToString().PadLeft(5, '0');
            //task.TaskNo = NoTask;

            arrTaskDetail.Add(task);

            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(2, Convert.ToInt32(ddlDept.SelectedValue), DateTime.Parse(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                docNo.PesType = 2;
                docNo.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                docNo.Tahun = DateTime.Parse(txtTglMulai.Text).Year;

                //HO ikut C dulu
                if (((Users)Session["Users"]).UnitKerjaID == 1)
                    docNo.Plant = "C";
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                    docNo.Plant = "K";
            }
            else
            {
                docNo.DocNo = 1;
                docNo.PesType = 2;
                docNo.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                docNo.Tahun = DateTime.Parse(txtTglMulai.Text).Year;

                //HO ikut C dulu
                if (((Users)Session["Users"]).UnitKerjaID == 1)
                    docNo.Plant = "C";
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                    docNo.Plant = "K";
            }
            TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, docNo);
            taskProcessFacade.arrImgProcessFacade((ArrayList)Session["ArrImgLampiran"]);

            string strError = string.Empty;
            strError = taskProcessFacade.Insert();
            if (strError == string.Empty)
            {
                Users users3 = usersFacade.RetrieveForMgrTask(ddlDept.SelectedItem.Text, usergroupid);
                string emailaddress = users3.UsrMail.Trim();
                if (emailaddress.Trim() != string.Empty)
                    KirimEmail(emailaddress);
                    ////KirimEmail("noreplay@grcboard.com");
                    Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;

                txtTaskNo.Text = taskProcessFacade.TaskNo;

                LoadDataGrid(LoadGrid());
                InsertLog(strEvent);

                lbUpdateTask.Enabled = true;
                btnSave.Disabled = true;
            }

            //int intResult = 0;
            //if (task.ID > 0)
            //{
            //    intResult = taskFacade.Update(task);
            //}
            //else
            //{
            //    intResult = taskFacade.Insert(task);
            //}

            //if (taskFacade.Error == string.Empty && intResult > 0)
            //{
            //    txtTaskNo.Text = NoTask;

            //    LoadDataGrid(LoadGrid());
            //    InsertLog(strEvent);

            //    btnSave.Disabled = true;
            //}


        }
        protected void lbUpdateTask_Click(object sender, EventArgs e)
        {
            //pada header status 0 = unsolved, 2 = solved
            //pada detail sratus 0 = open, 1 = UnSolved, 2= Solved
            txtApproval.Text = string.Empty;

            if (txtTaskNo.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Pilih Task No ");
                return;
            }
            if (CekAttachment(txtTaskNo.Text) == 0 && ddlStatus.SelectedItem.Text.ToUpper() == "SELESAI")
            {
                UploadFile(this);
                return;
            }
            /**
             * Target di rubah jadi tanggal tidak perlu pilih minggu
             * Base on wo dari ISO
             * befor lebaran 2015
             **/

            Task tsk = new Task();

            // Target T1 dst Max 2 Bln
            //if ( tsk.TglTarget.Month > 2 )
            //{
            //    tsk.TglTarget.
            //    DisplayAJAXMessage(this, "Tanggal Target  tidak boleh lebih  dari 2 Bulan");
            //    return;
            //}

            if (ddlTargetM.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Target M");
                return;
            }
            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;
            }
            //DateTime startDate = Convert.ToDateTime(txtTglMulai.Text);
            //string tanggal = startDate.ToString("dd");
            //string bulan = startDate.AddMonths(2).ToString("MMM");
            //string tahun = startDate.ToString("yyyy");
            //DateTime datelock = DateTime.Parse(tanggal + "-" + bulan + "-" + tahun);
            //DateTime finishTime = Convert.ToDateTime(txtTglSelesai.Text);
            //string tanggal2 = finishTime.ToString("dd");
            //string bulan2 = finishTime.AddMonths(2).ToString("MMM");
            //string tahun2 = finishTime.ToString("yyyy");
            //DateTime datelock2 = DateTime.Parse(tanggal2 + "-" + bulan2 + "-" + tahun2);

            //if (datelock2 <= datelock)
            //{
            //    DisplayAJAXMessage(this, "Tanggal Target Tidak Kurang dari Tanggal Mulai ");
            //    return;
            //}

            decimal pointNilai = 0;
            string strError = string.Empty;
            string strEvent = "Edit Target";

            TaskFacade taskFacade = new TaskFacade();
            Task task = taskFacade.RetrieveByNo2(txtTaskNo.Text + "' and A.ID='" + Session["id"].ToString());

            if (ddlStatus.SelectedValue == "Selesai")//task solve
            {
                #region Proses Task status Selesai
                if (task.App == 0)
                {
                    DisplayAJAXMessage(this, "Task sebelumnya belum di-Approval ");
                    return;
                }
                if (txtTglSelesai.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Tgl Selesai harus di-Isi ");
                    return;
                }
                if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedIndex <= 6)
                {
                    if (DateTime.Parse(txtTglSelesai.Text) < DateTime.Parse(txtTglTargetAktip.Text))
                    {
                        DisplayAJAXMessage(this, "Tgl Selesai harus lebih besar dari Tgl Target ");
                        return;
                    }
                }

                //ArdiYoga (WO lock tgl selesai tdk boleh kurang dari tgl mulai)
                if (DateTime.Parse(txtTglSelesai.Text) <= DateTime.Parse(txtTglMulai.Text))
                {
                    DisplayAJAXMessage(this, "Tgl Selesai tidak boleh kurang dari Tgl Mulai ");
                    return;
                }
                //ArdiYoga

                //if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedIndex <= 6)
                //{
                //    if (DateTime.Parse(txtTglTargetAktip.Text) >
                //}
                if (ddlStatus.SelectedIndex == 7 && DateTime.Parse(txtTglSelesai.Text) > DateTime.Parse(txtTglTargetAktip.Text))
                {
                    DisplayAJAXMessage(this, "Tgl Selesai harus lebih besar dari Tgl Target yang ditentukan ");
                    return;

                }
                if (ddlStatus.SelectedIndex <= task.TargetKe)
                {
                    DisplayAJAXMessage(this, "Status Target harus Naik ");
                    return;
                }

                int intIdDetail = task.IdDetail;
                int id = task.ID;
                int intargetKe = task.TargetKe;
                task = new Task();

                task.ID = id;
                task.IdDetail = intIdDetail;
                task.Status = 2;//Solved            
                task.TargetKe = ddlStatus.SelectedIndex;
                task.TglSelesai = DateTime.Parse(txtTglSelesai.Text);

                pointNilai = taskFacade.RetrieveByPointNilai(2, intargetKe);
                task.PointNilai = pointNilai;

                TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());

                strError = taskProcessFacade.UpdateTaskDetailPosting();
                #endregion
            }
            else if (ddlStatus.SelectedValue == "Cancel")//task dibatalkan
            {
                #region Prose Task dibatalakan
                if (txtAlasanCancel.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Cancel harus di isi");
                    return;
                }

                int intIdDetail = task.IdDetail;
                int id = task.ID;
                task = new Task();

                task.ID = id;
                task.IdDetail = intIdDetail;
                task.TargetKe = ddlStatus.SelectedIndex;
                task.Status = 9;//Batal           
                task.PointNilai = 0;
                //task.TglSelesai = DateTime.Parse(txtTglSelesai.Text);
                //task.ID = (int)Session["id"];
                //task.Status = 9;
                task.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                task.Ket = txtAlasanCancel.Text;
                TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());
                strError = taskProcessFacade.CancelTask();

                //TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());

                //strError = taskProcessFacade.UpdateTaskDetailPosting();
                #endregion
            }
            else
            {
                // UNSLOVED
                #region Proses Task Baru
                int tgl = 0;
                int bln = 0;
                int thn = int.Parse(txtTahun.Text);

                if (ddlTargetM.SelectedIndex == 1)
                {
                    tgl = 7;
                }
                else if (ddlTargetM.SelectedIndex == 2)
                {
                    tgl = 14;
                }
                else if (ddlTargetM.SelectedIndex == 3)
                {
                    tgl = 21;
                }
                else if (ddlTargetM.SelectedIndex == 4)
                {
                    DateTime final = new DateTime(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex));

                    tgl = final.Day;
                }
                //DateTime.Parse(txtTglTarget.Text);//
                DateTime TglTarget = new DateTime(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex, tgl, 0, 0, 0);

                if (task.App == 0)
                {
                    DisplayAJAXMessage(this, "Task tersebut belum di-Approval ");
                    return;
                }
                if (ddlStatus.SelectedIndex <= task.TargetKe)
                {
                    DisplayAJAXMessage(this, "Status Target harus Naik ");
                    return;
                }

                ArrayList arrTask = new ArrayList();
                DateTime startTime = DateTime.Now;
                if (Session["ListOfTask"] != null)
                {
                    arrTask = (ArrayList)Session["ListOfTask"];
                    //Session["ListOfTask"]
                    foreach (Task task0 in arrTask)
                    {
                        if (ddlStatus.SelectedIndex >= 1 && ddlStatus.SelectedIndex <= 6 && TglTarget < task0.TglTarget)
                        {
                            DisplayAJAXMessage(this, "Tgl Target harus lebih besar dari Tgl Target sebelumnya ");
                            return;
                        }
                        if (ddlStatus.SelectedIndex == 7 && TglTarget > task0.TglTarget)
                        {
                            DisplayAJAXMessage(this, "Tgl Selesai harus  kurang atau sama dengan Tgl Target sebelumnya ");
                            return;
                        }
                        startTime = task0.TglTarget;

                    }
                }
                //startTime = startTime.AddDays(1);
                string tanngal = startTime.ToString("dd");
                string bulan = startTime.ToString("MM");
                string tahun = startTime.ToString("yyyy");

                //DateTime datelock = DateTime.Parse(tanggal + "-" + bulan + "-" + tahun);

                string endbulan = DateTime.DaysInMonth(Convert.ToInt32(tahun), Convert.ToInt32(bulan)).ToString();
                DateTime tglawal = startTime.AddMonths(2);
                DateTime endTime = TglTarget;
                string tahunn = TglTarget.ToString("yyyy");
                //int month = endTime.Month - startTime.Month;
                //if (ddlStatus.SelectedIndex >= 1 && ddlStatus.SelectedIndex <= 6 && (endTime.Month - startTime.Month) > 2)
                if (tanngal == endbulan)
                {
                    if (Convert.ToInt32(tahunn) > Convert.ToInt32(tahun))
                    {
                        int bulana = endTime.Month + 12;
                        if (ddlStatus.SelectedIndex >= 1 && ddlStatus.SelectedIndex <= 6 && (bulana - startTime.Month) > 2)
                        {
                            DisplayAJAXMessage(this, "Tanggal Target Tidak Boleh Lebih dari 2  Bulan ");
                            return;
                        }
                    }
                    if (ddlStatus.SelectedIndex >= 1 && ddlStatus.SelectedIndex <= 6 && (endTime.Month - startTime.Month) > 2)
                    {
                        DisplayAJAXMessage(this, "Tanggal Target Tidak Boleh Lebih dari 2  Bulan ");
                        return;
                    }
                }
                else
                {
                    if (ddlStatus.SelectedIndex >= 1 && ddlStatus.SelectedIndex <= 6 && (endTime > tglawal))
                    {
                        DisplayAJAXMessage(this, "Tanggal Target Tidak Boleh Lebih dari 2  Bulan ");
                        return;
                    }
                }
                int taskID = task.ID;
                int intIdDetail = task.IdDetail;
                task = new Task();

                task.TaskID = taskID;
                task.IdDetail = intIdDetail;
                task.TargetKe = ddlStatus.SelectedIndex;
                task.TglTarget = TglTarget;//DateTime.Parse(txtTglTarget.Text);//
                task.Status = 0;//unsolved

                TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());

                strError = taskProcessFacade.InsertTaskDetail();
                #endregion
            }

            if (strError == string.Empty)
            {
                LoadDataGrid(LoadGrid());
                InsertLog(strEvent);
                btnSave.Disabled = true;
                DisplayAJAXMessage(this, "Data tersimpan ");
                clearForm();
            }

        }
        protected void btnListSolved_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = ((Users)Session["Users"]).DeptID.ToString();// dept.DeptID.ToString();

            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=Solved" + "&FormTask=1");

            //Response.Redirect("ListAddInvoice.aspx?CustID=" + txtCustID.Text + "&TypeCust=" + ddlTypeCustomer.SelectedIndex + "&TypeForm=Tanda");

        }

        protected void btnLampiran_ServerClick(object sender, EventArgs e)
        {
            if (Session["TempIdLampiran"] == null || Session["ArrImgLampiran"] == null)
            {
                Session["TempIdLampiran"] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
        }

        private void LoadDataGrid(ArrayList arrTransDoc)
        {
            this.GridView1.DataSource = arrTransDoc;
            this.GridView1.DataBind();
            //IsiComboSearch();
        }

        private ArrayList LoadGrid()
        {
            ArrayList arrTask = new ArrayList();
            TaskFacade taskFacade = new TaskFacade();
            arrTask = taskFacade.RetrieveByNo(txtTaskNo.Text);
            if (arrTask.Count > 0)
            {
                return arrTask;
            }

            arrTask.Add(new TransDoc());
            return arrTask;
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input Task";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtTaskNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            //if (eventLogFacade.Error == string.Empty)
            //clearForm();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private string ValidateText()
        {
            //if (txtTransactionCode.Text == string.Empty)
            //    return "Isi dulu Transaction Code";
            //else if (txtTransactionName.Text == string.Empty)
            //    return "Isi dulu Transaction Name";
            //else if (ddlBankName.SelectedIndex == 0)
            //    return "Pilih dulu Bank Name";
            //else if (txtCOA.Text == string.Empty)
            //    return "Isi dulu COA";
            //else if (txtAccountReff.Text == string.Empty)
            //    return "Isi dulu Account Reff";
            //else if (txtTransactionGroup.Text == string.Empty)
            //    return "Isi dulu Transaction Group";
            //else if (rbDebet.Checked == false && rbKredit.Checked == false)
            //    return "Debet / Kredit harus di-isi Bank Masuk atau Bank Keluar";

            //ChartOfAccountFacade chartOfAccountFacade = new ChartOfAccountFacade();
            //ChartOfAccount chartOfAccount = chartOfAccountFacade.RetrieveByAccountCode(txtCOA.Text);
            //if (chartOfAccount.ID == 0)
            //{
            //    return "Chart Of Account tidak ada";
            //}


            return string.Empty;
        }

        protected void ddlPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlDept.SelectedIndex = 0;
            if (ddlDept.SelectedIndex > 0)
            {
                ISO_UserFacade iso = new ISO_UserFacade();
                ISO_Users ios = iso.RetrieveByISOuserID(ddlPIC.SelectedValue.ToString());
                ddlDept.SelectedValue = ios.DeptID.ToString();

                LoadSection();
                int bagID = new DeptFacade().GetBagianID(int.Parse(ddlPIC.SelectedValue.ToString()));
                ddlBagian.SelectedValue = bagID.ToString();
                ddlBagian_SelectedIndexChanged(null, null);
            }
            else if (ddlDept.SelectedIndex == 0)
            {
                ISO_UserFacade iso = new ISO_UserFacade();
                ISO_Users ios = iso.RetrieveByISOuserID(ddlPIC.SelectedValue.ToString());
                ddlDept.SelectedValue = ios.DeptID.ToString();
                LoadSection();
                if (ddlBagian.Items.Count > 0)
                {
                    int bagID = new DeptFacade().GetBagianID(int.Parse(ddlPIC.SelectedValue.ToString()));
                    ddlBagian.SelectedValue = bagID.ToString();
                    ddlBagian_SelectedIndexChanged(null, null);
                }
            }
            //LoadList();
            ISO_UserFacade isoUserFacade = new ISO_UserFacade();
            ISO_Users users = isoUserFacade.RetrieveById(int.Parse(ddlPIC.SelectedValue));

            DeptFacade deptFacade = new DeptFacade();
            if (users.UserID.ToString() != "0" && users.UserID.ToString() != "")
            {
                Dept dept = deptFacade.RetrieveDeptByUserGroup(int.Parse(users.UserID));
                if (dept.UserGroupID == 100)
                    rbTask1.Enabled = true;
                else
                    rbTask1.Enabled = false;
            }

        }
        protected void ddlBagian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(2);

            if (ddlBagian.SelectedIndex > 0)
            {
                if (ddlBagian.SelectedItem.ToString().Contains("Manager") == true)
                    rbTask1.Enabled = true;
                else
                    rbTask1.Enabled = false;
            }
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadCategory();
            if (ddlCategory.SelectedIndex > 0)
            {
                CategoryFacade catFacade = new CategoryFacade();
                Category cat = catFacade.RetrieveById(int.Parse(ddlCategory.SelectedValue));
                if (catFacade.Error == string.Empty)
                {
                    //txtKetCategory.Text = cat.CategoryDescription;
                    txtBobotNilai.Text = cat.Bobot.ToString();
                }
                //else
                //{
                //    txtKetCategory.Text = string.Empty;
                //    txtBobotNilai.Text = string.Empty;
                //}
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "Selesai")
            //if (ddlStatus.SelectedValue == "Selesai")
            {
                txtTglSelesai.Enabled = (txtApproval.Text == "Belum di-Approval") ? false : true;
                txtTglSelesai.Focus();
                ifCancel.Visible = false;
                txtAlasanCancel.Visible = false;
            }
            else if (ddlStatus.SelectedIndex == 8)
            {
                txtTglSelesai.Enabled = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "confirm_delete()", true);
                //btnCancel_ServerClick(this, new EventArgs());
                ifCancel.Visible = true;
                txtAlasanCancel.Visible = true;
            }
            else
            {
                txtTglSelesai.Enabled = false;
                ifCancel.Visible = false;
                txtAlasanCancel.Visible = false;
            }
            if (ddlStatus.SelectedItem.Text.ToUpper() == "SELESAI" && lbUpdateTask.Enabled == true)
            {
                if (CekAttachment(txtTaskNo.Text) == 0)
                    UploadFile(this);
            }
        }
        protected void UploadFile(Control page)
        {
            if (txtTaskNo.Text.Trim() != string.Empty)
            {
                int TaskID = GetTaskID(txtTaskNo.Text.Trim());
                string myScript = "var wn = window.showModalDialog('../../ModalDialog/UploadFileTask.aspx?ba=" + TaskID + "&tablename=Iso_TaskAttachment', 'UploadFile', 'resizable:yes;dialogHeight: 200px; dialogWidth: 820px;scrollbars=yes');";
                ScriptManager.RegisterStartupScript(page, page.GetType(),
                    "MyScript", myScript, true);
            }
        }
        protected int GetTaskID(string TaskNo)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ID from iso_task where RowStatus>-1 and TaskNo='" + TaskNo + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected int CekAttachment(string TaskNo)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select count(ID) ada from iso_taskAttachment where RowStatus>-1 and Taskid in (select id from iso_task where taskno ='" + TaskNo + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ada"].ToString());
                }
            }
            return result;
        }
        public void KirimEmail(string Email)
        {
            int tgl = 0;
            int bln = 0;
            int thn = int.Parse(txtTahun.Text);
            try
            {
                if (ddlTargetM.SelectedIndex == 1)
                {
                    tgl = 7;
                }
                else if (ddlTargetM.SelectedIndex == 2)
                {
                    tgl = 14;
                }
                else if (ddlTargetM.SelectedIndex == 3)
                {
                    tgl = 21;
                }
                else if (ddlTargetM.SelectedIndex == 4)
                {
                    DateTime final = new DateTime(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex));

                    tgl = final.Day;
                }
                else
                {
                    tgl = DateTime.Now.Day;
                }
                DateTime TglTarget = new DateTime(thn, ddlBulan.SelectedIndex, tgl, 0, 0, 0);
                string TglTargets = txtTglTarget.Text;
                Depo depo = new Depo();
                DepoFacade depof = new DepoFacade();
                depo = depof.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                msg.From = new MailAddress("system_support@grcboard.com");
                msg.To.Add(Email);
                //msg.To.Add("iso_krwg@grcboard.com");
                //msg.Bcc.Add("noreplay@grcboard.com");
                msg.Subject = emailFacade.mailSubjectTask(depo.DepoName);
                msg.Body += emailFacade.mailBody1Task() + "\n\r";
                msg.Body += "New Task    : " + txtKeterangan.Text + "\n\r";
                msg.Body += "Start Date  : " + txtTglMulai.Text + "\n\r";
                msg.Body += "Target Date : " + TglTarget.ToString("dd-MMM-yyyy") + "\n\r";
                msg.Body += "PIC         : " + ddlPIC.SelectedItem.Text + "\n\r";
                msg.Body += (Session["AlasanCancel"] != null) ? "Status : Task di Cancel -> " + Session["AlasanCancel"].ToString() + "\n\r" : "";
                string plant = ""; string plant1 = "";
                switch (depo.ID)
                {
                    case 1:
                        plant = "ctrp";
                        plant1 = "123.123.123.129";
                        break;
                    case 7:
                        plant = "krwg";
                        plant1 = "192.168.222.21";
                        break;
                    case 13:
                        plant = "Jombang";
                        plant1 = "192.168.252.3";
                        break;
                    default:
                        plant1 = "";
                        plant = "purchasing"; break;
                }
                msg.Body += (plant1 == "") ? "" : "Silahkan Klik : http://" + plant1 + ":212" + "\n\r";
                msg.Body += "atau Klik : http://new" + plant + ".grcboard.com" + "\n\r\n\r";
                msg.Body += "Terimakasih, " + "\n\r";
                msg.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
                msg.Body += "Regard's, " + "\n\r";
                msg.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                //SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                //smt.Host = emailFacade.mailSmtp();
                //smt.Port = emailFacade.mailPort();
                //smt.EnableSsl = true;
                //smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smt.UseDefaultCredentials = false;
                //smt.Credentials = new System.Net.NetworkCredential("edp_krwg@grcboard.com", "aakarim123");
                ////bypas certificate
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //        System.Security.Cryptography.X509Certificates.X509Chain chain,
                //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};
                //smt.Send(msg);
                SmtpClient Smtp = new SmtpClient();

                Smtp.Host = "mail.grcboard.com";
                Smtp.Port = 587;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "system_support@grcboard.com";
                NetworkCred.Password = "grc123!@#";
                Smtp.EnableSsl = true;
                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = NetworkCred;
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    Smtp.Send(msg);
                }
                catch (Exception ex)
                { }
                clearForm();
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email terkirim";
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
            catch (Exception ex)
            {
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email gagal terkirim " + ex.Message;
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            UsersFacade usersFacade = new UsersFacade();
            int usergroupid = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select usergroupid from iso_dept where userid in (select userid from iso_users where id=" + ddlPIC.SelectedValue + ")";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    usergroupid = Convert.ToInt32(sdr["usergroupid"].ToString());
                }
            }
            Users users3 = usersFacade.RetrieveForMgrTask(ddlDept.SelectedItem.Text, usergroupid);
            string emailaddress = users3.UsrMail.Trim();
        }
        protected void btnListCancel_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = ((Users)Session["Users"]).DeptID.ToString();

            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=Cancel" + "&FormTask=1");
        }

    }
}