using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ISO
{
    public partial class FormApprovalTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                clearForm();

                LoadDept();

                //LoadDataGrid(LoadGrid());
                ////////////////

                Session["id"] = null;

                LoadUserTask();

            }
        }

        private void LoadTask(int intRow)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrTask = new ArrayList();
            ArrayList arrTask2 = new ArrayList();

            //if (users.Apv < 1)
            //{ return; }

            if (Session["ListOpenTask"] != null)
                arrTask = (ArrayList)Session["ListOpenTask"];

            if (intRow < arrTask.Count && intRow > -1)
            {
                Task task = new Task();
                task = (Task)arrTask[intRow];
                if (task.ID > 0)
                {
                    Session["id"] = task.ID;
                    ViewState["NoTask"] = task.TaskNo;

                    txtTaskNo.Text = task.TaskNo;
                    txtTglMulai.Text = task.TglMulai.ToString("dd-MMM-yyyy");
                    txtKeterangan.Text = task.NewTask;
                    //txtTglTarget.Text = task.TglTarget.ToString("dd-MMM-yyyy");
                    txtPic.Text = task.Pic;
                    if (task.TglSelesai == DateTime.MinValue)
                        txtTglSelesai.Text = string.Empty;
                    else
                        txtTglSelesai.Text = task.TglSelesai.ToString("dd-MMM-yyyy");

                    SelectDept(task.DeptID.ToString());

                    LoadSection();
                    SelectSection(task.BagianID.ToString());

                    LoadCategory(2);
                    SelectCategory(task.CategoryID.ToString());
                    txtBobotNilai.Text = task.BobotNilai.ToString();

                    FindTask("T" + task.TargetKe.ToString());

                    ddlStatus.SelectedIndex = task.TargetKe;
                    txtCancel.Visible = (task.Status == 9) ? true : false;
                    txtAlasanCancel.Visible = (task.Status == 9) ? true : false;
                    txtAlasanCancel.Text = task.AlasanCancel.ToString();
                    //Session["OPHeader"] = op;
                    //txtOPNo.Text = op.OPNo;

                    TaskFacade taskFacade = new TaskFacade();
                    ArrayList arrTaskDetail = taskFacade.RetrieveByID(task.ID);

                    if (arrTaskDetail.Count > 0)
                    {
                        foreach (Task task2 in arrTaskDetail)
                        {
                            if (task2.Aktip == 1)
                            {
                                ddlStatus.SelectedIndex = task2.TargetKe;

                                txtTglTarget.Text = task2.TglTarget.ToString("dd-MMM-yyyy");
                            }

                            arrTask2.Add(task2);
                        }

                    }
                    else
                        arrTask2.Add((Task)arrTask[intRow]);


                    Session["ListOfTaskDetail"] = arrTask2;
                    GridView1.DataSource = arrTask2;
                    GridView1.DataBind();
                    lstTask.DataSource = arrTask2;
                    lstTask.DataBind();
                    Session["TaskId"] = task.ID;
                    Task task1 = new Task();
                    TaskFacade taskFacade1 = new TaskFacade();
                    task1 = taskFacade1.RetrieveByJmlLampiran(task.ID);
                    if (task1.Jumlah > 0) { btnLampiran.Visible = true; }
                    else { btnLampiran.Visible = false; }
                }

                /////////
            }
            else
            {
                if (intRow == -1)
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                else
                    ViewState["counter"] = (int)ViewState["counter"] - 1;

                Task task = new Task();
                // task = (Task)arrTask[(int)ViewState["counter"]];

            }
        }

        private void LoadUserTask()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);
            int uid = users.UnitKerjaID;
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserGroup(users2.ID);
            try
            {
                if (deptFacade.Error == string.Empty)
                {
                    if (users2.ID == dept.UserID && dept.UserGroupID > 100)
                    {
                        DisplayAJAXMessage(this, "User-ID anda Bukan DiLevel Approve" + users2.ID.ToString() + " " + dept.UserGroupID.ToString());
                        Response.Redirect("../../Home.aspx", false);
                        Response.End();
                    }
                    else
                    {
                        LoadOpenTask();

                        ViewState["counter"] = 0;
                        int counter = (int)ViewState["counter"];
                        if (Request.QueryString["TaskNo"] != null)
                        {
                            counter = FindTask(Request.QueryString["TaskNo"].ToString());
                            ViewState["counter"] = counter;
                        }

                        LoadTask(counter);
                    }
                }
            }
            catch { }

        }

        private void LoadOpenTask()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            ArrayList dept = deptFacade.RetrieveForISO(users2.ID);

            TaskFacade taskFacade = new TaskFacade();
            ArrayList arrTask = new ArrayList();
            string dep = string.Empty; string appLevel = "";
            foreach (Dept dp in dept)
            {
                dep = (dep == string.Empty) ? dp.DeptID.ToString() : dep + "," + dp.DeptID.ToString();
                appLevel = dp.UserGroupID.ToString();
            }
            //arrTask = taskFacade.RetrieveByPT(users.CompanyID);
            //arrTask = taskFacade.RetrieveByPT2(users.CompanyID);
            //if (users.UnitKerjaID == 1 || users.UnitKerjaID == 7)
            //    arrTask = taskFacade.RetrieveByPT3(dep);
            //else
                arrTask = taskFacade.RetrieveByPT3(dep, appLevel);

            if (taskFacade.Error == string.Empty)
            {
                Session["ListOpenTask"] = arrTask;
            }
        }
        private int FindTask(string strNoTask)
        {
            Users users = (Users)Session["Users"];

            ArrayList arrTask = new ArrayList();
            int counter = 0;

            if (Session["ListOpenTask"] != null)
                arrTask = (ArrayList)Session["ListOpenTask"];

            foreach (Task task in arrTask)
            {
                if (task.TaskNo == strNoTask)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }
        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOpenTask"] = null;


            //txtTglMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtTglSelesai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtTglTarget.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTglMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTglSelesai.Text = string.Empty;
            txtTglTarget.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTaskNo.Text = string.Empty;
            txtPic.Text = (((Users)Session["Users"]).UserName);
            ddlDept.SelectedIndex = 0;
            ddlBagian.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            txtBobotNilai.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            txtKeterangan.Text = string.Empty;

            ArrayList arrList = new ArrayList();
            arrList.Add(new Task());

            GridView1.DataSource = arrList;
            GridView1.DataBind();
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy");

                if (int.Parse(e.Row.Cells[2].Text) <= 6)
                    e.Row.Cells[2].Text = "T" + e.Row.Cells[2].Text;

                if (int.Parse(e.Row.Cells[4].Text) == 0)
                    e.Row.Cells[4].Text = "Open";
                else if (int.Parse(e.Row.Cells[4].Text) == 1)
                    e.Row.Cells[4].Text = "UnSolved";
                else if (int.Parse(e.Row.Cells[4].Text) == 2)
                    e.Row.Cells[4].Text = "Solved";
            }
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            try
            {
                ArrayList arrDept = deptFacade.RetrieveByUserID(users2.ID);

                ddlDept.Items.Clear();
                if (deptFacade.Error == string.Empty)
                {
                    ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));

                    foreach (Dept dept in arrDept)
                    {
                        ddlDept.Items.Add(new ListItem(dept.DeptName, dept.DeptID.ToString()));
                    }
                }
            }
            catch { }
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
        private void LoadSection()
        {
            DeptFacade deptFacade2 = new DeptFacade();
            ArrayList arrDept2 = deptFacade2.RetrieveSection3(ddlDept.Items[ddlDept.SelectedIndex].Text);

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

        private void LoadCategory(int pesType)
        {
            CategoryFacade catFacade = new CategoryFacade();
            //ArrayList arrCategory = catFacade.RetrieveBySectionIDByDeptID(ddlDept.SelectedIndex, ddlBagian.SelectedIndex);
            ArrayList arrCategory = catFacade.RetrieveByPesType(pesType);


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
            int i = 0;
            ArrayList arrTask = (Session["ListOpenTask"] != null) ? new ArrayList() : (ArrayList)Session["ListOpenTask"];
            foreach (Task t in arrTask)
            {

                if (t.TaskNo == txtSearch.Text)
                {

                    LoadTask(i);
                }

                i++;
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = dept.DeptID.ToString();

            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=UnSolved" + "&FormTask=2");
            //Response.Redirect("ListTask.aspx?receive=no");
        }


        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            /// kurangi yang di session
            /// kayak schedule

            Users users = (Users)Session["Users"];

            TaskFacade taskFacade = new TaskFacade();
            Task task = new Task();
            try
            {
                task = taskFacade.RetrieveByNo1(txtTaskNo.Text + "' and A.ID='" + Session["id"].ToString());

                int stsTask = task.Status;
                if (task.Status == 9)
                {
                    task.ID = (int)Session["id"];
                    task.Status = -1;
                    task.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    task.Ket = task.AlasanCancel.ToString();
                }
                //task.App = task.App + 1;
                task.App = 2;
                task.Status = stsTask + 1;

                string strError = string.Empty;
                ArrayList arrOPDetail = new ArrayList();

                TaskProcessFacade taskProcessFacade = new TaskProcessFacade(task, new ISO_DocumentNo());
                if (task.ID > 0)
                {
                    if (stsTask == 9)
                    {
                        strError = taskProcessFacade.CancelTask();
                        EventLogProcess mp = new EventLogProcess();
                        EventLog evl = new EventLog();
                        mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        mp.Pilihan = "Insert";
                        evl.UserID = ((Users)Session["Users"]).ID;
                        evl.AppLevel = ((Users)Session["Users"]).Apv;
                        evl.DocNo = task.TaskNo.ToString();
                        evl.DocType = "Cancel - Task";
                        evl.AppDate = DateTime.Now;
                        evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        mp.EventLogInsert(evl);
                    }
                    else
                    {
                        strError = taskProcessFacade.UpdateTaskDetailApproval();
                        EventLogProcess mp = new EventLogProcess();
                        EventLog evl = new EventLog();
                        mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        mp.Pilihan = "Insert";
                        evl.UserID = ((Users)Session["Users"]).ID;
                        evl.AppLevel = ((Users)Session["Users"]).Apv;
                        evl.DocNo = task.TaskNo.ToString();
                        evl.DocType = "Task";
                        evl.AppDate = DateTime.Now;
                        evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        mp.EventLogInsert(evl);
                    }
                }

                if (strError == string.Empty)
                {
                    InsertLog("Approve Task");

                    ArrayList arrTask = new ArrayList();
                    if (Session["ListOpenTask"] != null)
                    {
                        arrTask = (ArrayList)Session["ListOpenTask"];
                        //if (users.TypeUnitKerja == 1)
                        ((Task)arrTask[(int)ViewState["counter"]]).Status = stsTask;
                        //else
                        //    ((Task)arrTask[(int)ViewState["counter"]]).Status = 2;

                        Session["ListOpenTask"] = arrTask;
                    }

                    if (task.ID > 0)
                    {
                        ViewState["counter"] = (int)ViewState["counter"] + 1;

                        LoadTask((int)ViewState["counter"]);
                    }


                }
                else
                {
                    DisplayAJAXMessage(this, strError);
                }
            }
            catch { }
            Response.Redirect("FormApprovalTask.aspx");
        }
        protected void lstTask_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("sts");
            Task t = (Task)e.Item.DataItem;
            switch (t.Status)
            {
                case 0:
                    lbl.Text = "Open";
                    break;
                case 1:
                    lbl.Text = "UnSolved";
                    break;
                case 2:
                    lbl.Text = "Solved";
                    break;
                case 9:
                    lbl.Text = "Cancel";
                    break;
            }
        }
        private void LoadDataGrid(ArrayList arrTransDoc)
        {
            this.GridView1.DataSource = arrTransDoc;
            this.GridView1.DataBind();
            //IsiComboSearch();
            lstTask.DataSource = arrTransDoc;
            lstTask.DataBind();
        }

        private ArrayList LoadGrid()
        {
            ArrayList arrTask = new ArrayList();
            TaskFacade taskFacade = new TaskFacade();
            arrTask = taskFacade.Retrieve();
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
            return string.Empty;
        }

        protected void ddlBagian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(2);
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
                Category cat = catFacade.RetrieveById(ddlCategory.SelectedIndex);
                if (catFacade.Error == string.Empty)
                {
                    //txtKetCategory.Text = cat.CategoryDescription;
                    txtKeterangan.Text = cat.CategoryDescription;
                    txtBobotNilai.Text = cat.Bobot.ToString();
                }
                else
                {
                    //txtKetCategory.Text = string.Empty;
                    txtKeterangan.Text = string.Empty;
                    txtBobotNilai.Text = string.Empty;
                }
            }
        }
        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;

            LoadTask((int)ViewState["counter"]);
        }

        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;

            LoadTask((int)ViewState["counter"]);
        }
        private void LoadAccount()
        {

        }
    }
}