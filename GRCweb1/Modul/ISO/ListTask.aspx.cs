using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;


using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.ISO
{
    public partial class ListTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                if (Request.QueryString["p"] == null)
                {
                    dTask.Visible = true;
                    dSOP.Visible = false;
                    btnUpdate.Value = "Form Task";
                   
                    ddlSeacrhItem("TaskNo");
                    LoadDept();
                     LoadData(Request.QueryString["TaskDept"].ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
                    /** Beny **/
                    string strFirst = "1/1/" + DateTime.Now.Year.ToString();
                    DateTime dateFirst = DateTime.Parse(strFirst);
                    if (DateTime.Now.Month >= 6)
                    {
                        txtdrtanggal.Text = "01-Jul-" +  DateTime.Now.ToString("yyyy");
                    }
                    else
                    {
                        txtdrtanggal.Text = "01-Jan-" + DateTime.Now.ToString("yyyy");
                    }
                    txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else if (Request.QueryString["p"] == "SOP")
                {
                    dTask.Visible = false;
                    dSOP.Visible = true;
                    btnUpdate.Value = "Form SOP";
                    LoadSOP();
                    ddlSeacrhItem("SOPNo");
                }
                else if (Request.QueryString["p"] == "KPI")
                {
                    dTask.Visible = false;
                    dSOP.Visible = true;
                    btnUpdate.Value = "Form KPI";
                    LoadKPI();
                    ddlSeacrhItem("KPINo");
                }
                btnUpload_Click(null, null);
                ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnUpload);
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnUpload);
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            Task tppdept1 = new Task();
            TaskFacade tsFacade1 = new TaskFacade();
            int tanda = tsFacade1.RetrieveTanda(users.ID);
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            Task tppdept = new Task();
            TaskFacade tsFacade = new TaskFacade();
            arrDept = tsFacade.RetrieveDeptNama(tanda, users.DeptID);
            if (tanda == 0)
            {
                ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
                foreach (Task dept in arrDept)
                {
                    ddlDeptName.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.DeptID.ToString()));
                }
            }
            else
            {
                ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
                foreach (Task dept in arrDept)
                {
                    ddlDeptName.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.DeptID.ToString()));
                }
            }
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Users users = (Users)Session["Users"];

            //ArrayList arrTask0 = new ArrayList();
            //TaskFacade taskFacade0 = new TaskFacade();
            //arrTask0 = taskFacade0.RetrieveByDeptIDSolved_new(ddlDeptName.SelectedValue);


            ddlPIC.Items.Clear();
            ArrayList arrPIC = new ArrayList();
            Task tsk = new Task();
            TaskFacade tskFacade = new TaskFacade();



            if (ddlDeptName.SelectedValue.ToString() == "25")
            {
                arrPIC = tskFacade.RetrievePICMGR(ddlDeptName.SelectedValue.ToString());
            }
            else if (ddlDeptName.SelectedValue.ToString() == "27")
            {
                arrPIC = tskFacade.RetrievePICCMGR(ddlDeptName.SelectedValue.ToString());
            }
            else
            {
                arrPIC = tskFacade.RetrievePIC(ddlDeptName.SelectedValue.ToString());
            }


            ddlPIC.Items.Add(new ListItem("-- Pilih PIC --", "0"));
            foreach (Task dept in arrPIC)
            {
                ddlPIC.Items.Add(new ListItem(dept.Pic.ToUpper().Trim(), dept.Pic.ToUpper().Trim()));
            }
            LoadData(ddlDeptName.SelectedValue.ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
        }

        private void ddlSeacrhItem(string ItemName)
        {
            ddlSearch.Items.Clear();
            ddlSearch.Items.Add(new ListItem(ItemName, ItemName));
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }
        private void LoadDataSOP()
        {

        }

        private void LoadData(string deptID, string tipeTask, string formTask)
        {
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserGroup(users2.ID);

            ArrayList arrTask = new ArrayList(); ArrayList arrTask0 = new ArrayList();

            #region penambahan tanggal persemester
            string strFirst = "1/1/" + DateTime.Now.Year.ToString();
            DateTime dateFirst = DateTime.Parse(strFirst);
            if (DateTime.Now.Month >= 6)
            {
                txtdrtanggal.Text = "01-Jul-" + DateTime.Now.ToString("yyyy");
            }
            else
            {
                txtdrtanggal.Text = "01-Jan-" + DateTime.Now.ToString("yyyy");
            }

            txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            #endregion

            if (deptFacade.Error == string.Empty)
            {
                if (users2.ID == dept.UserID && dept.UserGroupID > 100)
                {
                    // ArrayList arrTask = new ArrayList();
                    TaskFacade taskFacade = new TaskFacade();
                    if (ddlDeptName.Items.Count > 1)
                        taskFacade.Criteria = " and deptid in (select xx.ID DeptID from (select distinct DeptID from ISO_Task where RowStatus>-1) as x " +
                            " inner join Dept xx ON x.DeptID=xx.ID )";
                    //taskFacade.Criteria = " and A.Createdby='" + ((Users)Session["Users"]).UserName.ToString() + "' ";
                    if (tipeTask == "Solved")
                    {
                        trtgl.Visible = true;
                        string tglawal = string.Empty; string tglakhir = string.Empty;
                        DateTime drTgl = DateTime.Now;
                        DateTime sdTgl = DateTime.Now;
                        //tglawal = drTgl.ToString("yyyyMMdd");
                        //tglakhir = sdTgl.ToString("yyyyMMdd");

                        tglawal = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
                        tglakhir = DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd");

                        arrTask = taskFacade.RetrieveByDeptIDSolved2(int.Parse(deptID), dept.UserID, tglawal, tglakhir);
                        arrTask0 = taskFacade.RetrieveByDeptIDSolved2(int.Parse(deptID));
                    }
                    else if (tipeTask == "Cancel")
                    {
                        trtgl.Visible = false;
                            arrTask = taskFacade.RetrieveByDeptIDCancel(int.Parse(deptID), dept.UserID);
                    }
                    else
                    {
                        trtgl.Visible = false;
                        if (ddlDeptName.Items.Count > 1)
                        {
                            taskFacade.Criteria = "";

                            //arrTask = taskFacade.RetrieveByDeptIDUnSolved2(int.Parse(deptID), dept.UserID);
                            arrTask = taskFacade.RetrieveByDeptIDUnSolved(int.Parse(deptID));
                        }
                        else
                        {
                            arrTask = taskFacade.RetrieveByDeptIDUnSolved2(int.Parse(deptID), dept.UserID);
                        }

                    }
                    if (taskFacade.Error == string.Empty)
                    {
                        //LoadPIC(arrTask);
                        GridView1.DataSource = arrTask;
                        GridView1.DataBind();
                        lstPes.DataSource = arrTask;
                        lstPes.DataBind();
                    }
                }
                else
                {
                    TaskFacade taskFacade = new TaskFacade();
                    //taskFacade.Criteria = " and CreatedBy='" + ((Users)Session["Users"]).UserName.ToString() + "' ";
                    if (ddlDeptName.Items.Count > 1)
                        //taskFacade.Criteria = " and deptid in (select xx.ID DeptID from (select distinct DeptID from ISO_Task where RowStatus>-1) as x " +
                        //" inner join Dept xx ON x.DeptID=xx.ID )";

                        //penambahan agus 11-11-2022
                        taskFacade.Criteria = " and deptid in (select xx.ID DeptID from (select distinct DeptID from ISO_Task where RowStatus>-1) as x " +
                        " inner join Dept xx ON x.DeptID=xx.ID and A.UserID in(select UserID from ISO_Users where DeptID = '"+deptID+"' and RowStatus >-1) )";

                        if (tipeTask == "Solved")
                    {
                        string tglawal = string.Empty; string tglakhir = string.Empty;
                        DateTime drTgl = DateTime.Now;
                        DateTime sdTgl = DateTime.Now;
                        tglawal = drTgl.ToString("yyyyMMdd");
                        tglakhir = sdTgl.ToString("yyyyMMdd");

                        //string Nama = users.UserName.ToString().Trim().ToUpper();

                        /** Beny **/
                        string Nama = string.Empty;
                        if (ddlPIC.Items.Count > 0)
                            Nama = ddlPIC.SelectedItem.Text;
                        arrTask = taskFacade.RetrieveByDeptIDSolved(int.Parse(deptID), Nama, tglawal, tglakhir);

                    }
                    else if (tipeTask == "Cancel")
                    {
                        trtgl.Visible = false;

                        arrTask = taskFacade.RetrieveByDeptIDCancel(int.Parse(deptID), dept.UserID);
                    }
                    else
                    {
                        trtgl.Visible = false;

                        arrTask = taskFacade.RetrieveByDeptIDUnSolved(int.Parse(deptID));
                    }
                    if (taskFacade.Error == string.Empty)
                    {

                        GridView1.DataSource = arrTask;
                        GridView1.DataBind();
                        lstPes.DataSource = arrTask;
                        lstPes.DataBind();
                    }

                }
                if (ddlDeptName.SelectedValue == "0")
                {
                    if (tipeTask == "Solved")
                    {
                        LoadPIC(arrTask0);
                    }
                    else
                    {
                        LoadPIC(arrTask);
                    }
                    int n = 0;
                    for (int i = 0; i < lstPes.Items.Count; i++)
                    {
                        HtmlTableRow tr = (HtmlTableRow)lstPes.Items[i].FindControl("rr");
                        Label td = (Label)lstPes.Items[i].FindControl("nom");
                        if (ddlPIC.SelectedIndex == 0)
                        {
                            n = (tr.Attributes["title"].ToString().ToUpper() == ddlPIC.SelectedValue) ? n + 1 : n;
                            tr.Visible = (tr.Attributes["title"].ToString().ToUpper() == ddlPIC.SelectedValue) ? true : false;

                        }

                    }
                }
            }
        }
        private void LoadPIC(ArrayList arrData)
        {
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--All--", "0"));
            ArrayList arrL = new ArrayList();
            ArrayList arrO = new ArrayList();
            foreach (Task tsk in arrData)
            {
                arrL.Add(tsk.Pic.ToUpper());
                //ddlPIC.Items.Add(new ListItem(tsk.Pic.ToUpper(), tsk.Pic.ToString()));
            }
            arrL.ToArray().Distinct().ToList().ForEach(a => arrO.Add(a));
            foreach (var ts in arrO)
            {
                ddlPIC.Items.Add(new ListItem(ts.ToString(), ts.ToString()));
            }
        }
        protected void ddlPIC_Change(object sender, EventArgs e)
        {
            if (Request.QueryString["TipeTask"].ToString() == "Solved")
            {
                string tglawal = string.Empty; string tglakhir = string.Empty;
                if (txtdrtanggal.Text == "" || txtsdtanggal.Text == "")
                {
                    DateTime drTgl = DateTime.Now;
                    DateTime sdTgl = DateTime.Now;
                    tglawal = drTgl.ToString("yyyyMMdd");
                    tglakhir = sdTgl.ToString("yyyyMMdd");
                }
                else
                {
                    DateTime drTgl = DateTime.Parse(txtdrtanggal.Text);
                    DateTime sdTgl = DateTime.Parse(txtsdtanggal.Text);
                    tglawal = drTgl.ToString("yyyyMMdd");
                    tglakhir = sdTgl.ToString("yyyyMMdd");
                }

                TaskFacade taskFacade0 = new TaskFacade();
                ArrayList arrTask0 = new ArrayList();
                arrTask0 = taskFacade0.RetrieveByDeptIDSolved(Convert.ToInt32(ddlDeptName.SelectedValue), ddlPIC.SelectedItem.ToString(), tglawal, tglakhir);

                GridView1.DataSource = arrTask0;
                GridView1.DataBind();

                lstPes.DataSource = arrTask0;
                lstPes.DataBind();
            }
            else
            {
                int n = 0;
                for (int i = 0; i < lstPes.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstPes.Items[i].FindControl("rr");
                    Label td = (Label)lstPes.Items[i].FindControl("nom");
                    if (ddlPIC.SelectedIndex > 0)
                    {
                        n = (tr.Attributes["title"].ToString().ToUpper() == ddlPIC.SelectedValue) ? n + 1 : n;
                        tr.Visible = (tr.Attributes["title"].ToString().ToUpper() == ddlPIC.SelectedValue) ? true : false;

                    }
                    else
                    {
                        n = n + 1;
                        tr.Visible = true;
                    }
                    td.Text = n.ToString();
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                if (Request.QueryString["FormTask"].ToString() == "1")
                    Response.Redirect("FormInputTask.aspx?TaskNo=" + row.Cells[0].Text);
                else
                    Response.Redirect("FormApprovalTask.aspx?TaskNo=" + row.Cells[0].Text);

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MM-yyyy");
                e.Row.Cells[6].Text = DateTime.Parse(e.Row.Cells[6].Text).ToString("dd-MM-yyyy");

                if (int.Parse(e.Row.Cells[5].Text) <= 6)
                    e.Row.Cells[5].Text = "T" + e.Row.Cells[5].Text;

                if (e.Row.Cells[8].Text == "0")
                    e.Row.Cells[8].Text = "Open";
                else if (e.Row.Cells[8].Text == "1")
                    e.Row.Cells[8].Text = "UnSolved";
                else if (e.Row.Cells[8].Text == "2")
                    e.Row.Cells[8].Text = "Solved";
                else if (e.Row.Cells[8].Text == "-1")
                    e.Row.Cells[8].Text = "Cancel";
                else
                    e.Row.Cells[8].Text = "??";

                //e.Row.Cells[6].Text = Decimal.Parse(e.Row.Cells[6].Text).ToString("N2");
                //e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["FormTask"].ToString() == "1")
                switch (Request.QueryString["p"])
                {
                    case "SOP":
                        Response.Redirect("FormInputSOP.aspx");
                        break;
                    case "KPI":
                        Response.Redirect("FormInputKPI.aspx");
                        break;
                    default:
                        Response.Redirect("FormInputTask.aspx");
                        break;
                }
            else
            {
                switch (Request.QueryString["p"])
                {
                    case "SOP":
                        Response.Redirect("FormApprovalSOP.aspx");
                        break;
                    case "KPI":
                        Response.Redirect("FormApprovalKPI.aspx");
                        break;
                    default:
                        Response.Redirect("FormApprovalTask.aspx");
                        break;
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            LoadData(Request.QueryString["TaskDept"].ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
        }
        protected void lstPes_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("lblSts");
            Label app = (Label)e.Item.FindControl("txtTarget");
            Task tsk = (Task)e.Item.DataItem;
            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
            ImageButton imgs = (ImageButton)e.Item.FindControl("edt");
            switch (tsk.App)
            {
                case 0: lbl.Text = "Open"; break;
                case 1: lbl.Text = "UnSolved"; break;
                case 2: lbl.Text = (tsk.Status == 2) ? "Solved" : (tsk.Status == 9) ? "Cancel" : "Approved"; break;
                case 8: case 9: case -1: lbl.Text = "Cancel"; break;
            }
            switch (tsk.TargetKe)
            {
                case 1: app.Text = "T1"; break;
                case 2: app.Text = "T2"; break;
                case 3: app.Text = "T3"; break;
                case 4: app.Text = "T4"; break;
                case 5: app.Text = "T5"; break;
                case 6: app.Text = "T6"; break;
                case 7: app.Text = "Selesai"; break;
                case 8: app.Text = "Cancel"; break;
            }
            lbl.ToolTip = (tsk.Status == 9) ? tsk.AlasanCancel : "";
            lbl.Attributes.Add("cursor", (tsk.Status == 9) ? "Pointer" : "");
            //filter

            LoadListAttachmentPrs(imgs.CommandArgument.ToString(), rpts);
        }
        private void LoadListAttachmentPrs(string TaskID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (TaskID == string.Empty)
                return;
            if (Convert.ToInt32(TaskID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from Iso_TaskAttachment where rowstatus>-1 and TaskID=" + TaskID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new TaskAttachment
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            TaskID = Convert.ToInt32(sdr["TaskID"].ToString()),
                            FileName = sdr["FileName"].ToString()
                        });
                    }
                    lst.DataSource = arrData;
                    lst.DataBind();
                }
            }

        }
        protected void lstPes_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "edit":
                    switch (Request.QueryString["p"])
                    {
                        case "SOP":
                            Response.Redirect("FormInputSOP.aspx?id=" + e.CommandArgument.ToString());
                            break;
                        case "KPI":
                            Response.Redirect("FormInputKPI.aspx?id=" + e.CommandArgument.ToString());
                            break;
                        default:
                            Response.Redirect("FormInputTask.aspx?id=" + e.CommandArgument.ToString());
                            break;
                    }
                    break;
                case "add":
                    //Uploadlisttask.PostedFile.SaveAs(Uploadlisttask.PostedFile.FileName) ;

                    Session["taskid"] = e.CommandArgument.ToString();
                    //PanelUpload.Visible = true;
                    //btnUpload_Click(null, null);
                    UploadFile(this, e.CommandArgument.ToString());
                    //LoadData(Request.QueryString["TaskDept"].ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
                    break;
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Uploadlisttask.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = Uploadlisttask.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                if (CheckAttachment(UploadedFileName) == true)
                {
                    DisplayAJAXMessage(this, "File Attacment pernah di upload, ganti nama file terlebih dahulu");
                    return;
                }
                Uploadlisttask.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\IsoTask\\" + UploadedFileName);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    string tablename = "Iso_TaskAttachment";
                    string taskid = Session["taskid"].ToString();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery =
                        "insert " + tablename + "( TaskID, FileName, RowStatus, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime)values(" +
                        int.Parse(taskid) + ",'" +
                        UploadedFileName + "',0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();
                    PanelUpload.Visible = false;
                    LoadData(Request.QueryString["TaskDept"].ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
                }
                catch (Exception ex)
                { Response.Write("An error occurred - " + ex.ToString()); lblMessage.Text = "Upload Data Gagal!"; }
            }
        }
        private bool CheckAttachment(string DocName)
        {
            string tablename = "Iso_TaskAttachment";
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select FileName from " + tablename + " where RowStatus >-1 and FileName='" + DocName + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    rst = true;
                }
            }
            return rst;
        }
        protected void UploadFile(Control page, string TaskID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/UploadFileTask.aspx?ba=" + TaskID +
                "&tablename=Iso_TaskAttachment&from=listtask', 'UploadFile', 'resizable:yes;dialogHeight: 200px; dialogWidth: 820px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
            //Response.Redirect("ListTask.aspx?TaskDept=" + Request.QueryString["TaskDept"].ToString() + "&TipeTask=Solved" + "&FormTask=1");
        }
        private ArrayList LoadSOP()
        {
            ArrayList arrData = new ArrayList();
            SOPList sp = new SOPList();
            sp.Criteria = " and DeptID=" + Request.QueryString["TaskDept"];
            sp.Criteria += (((Users)Session["Users"]).Apv == 0) ? " and UserID=" + ((Users)Session["Users"]).ID.ToString() : "";
            sp.OrderBy = " Order by A.PIC,Month(tglMulai),year(tglMulai),A.SOPNo,A.ID";
            arrData = sp.Retrieve();
            lstSOP.DataSource = arrData;
            lstSOP.DataBind();
            return arrData;
        }

        private ArrayList LoadKPI()
        {
            ArrayList arrData = new ArrayList();
            SOPList sp = new SOPList();
            sp.Criteria = " and A.DeptID=" + Request.QueryString["TaskDept"];
            sp.Criteria += (((Users)Session["Users"]).Apv == 0) ? " and A.UserID=" + ((Users)Session["Users"]).ID.ToString() : "";
            sp.OrderBy = " Order by A.PIC,Month(tglMulai),year(tglMulai),A.KPINo,A.ID";
            arrData = sp.RetrieveKPI();
            lstSOP.DataSource = arrData;
            lstSOP.DataBind(); return arrData;

        }


        protected void attachPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void attachPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "preprs":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\isotask\";
                        string ext = Path.GetExtension(Nama);

                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                        break;
                    case "hpsprs":
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update Iso_TaskAttachment set rowstatus=-1  where rowstatus>-1 and ID=" + e.CommandArgument.ToString();
                        SqlDataReader sdr = zl.Retrieve();

                        DisplayAJAXMessage(this, "Data sudah dihapus");
                        LoadData(Request.QueryString["TaskDept"].ToString(), Request.QueryString["TipeTask"].ToString(), Request.QueryString["FormTask"].ToString());
                        break;

                }
            }
            catch
            {
                return;
            }
        }
    }
}

public class SOPList
{
    private ArrayList arrData = new ArrayList();
    public string Criteria { get; set; }
    public string OrderBy { get; set; }
    public string Query()
    {
        string query = "select A.ID,B.ID as idDetail, A.SOPNo,A.SOPName,A.DeptID,A.BagianID,B.KetTargetKe,B.TargetKe,A.TglMulai," +
                       "B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID  )" +
                       "else '' end BagianName,B.Aktip, A.CategoryID,'' as AlasanCancel,(A.NilaiBobot*100)NilaiBobot,A.Keterangan," +
                       "A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval," +
                       "B.PointNilai,CASE B.Approval WHEN 2 THEN (A.NilaiBobot*B.PointNilai) ELSE 0 END Nilai from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID  and A.RowStatus>-1 and " +
                       "B.RowStatus>-1 " + this.Criteria + " " + this.OrderBy;
        return query;
    }
    public string kpiQuery()
    {
        string query = "select A.ID,B.ID as idDetail, A.KPINo,A.KPIName,A.DeptID,A.BagianID,B.KetTargetKe,B.TargetKe,A.TglMulai," +
                       "B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID  )" +
                       "else '' end BagianName,B.Aktip, A.CategoryID,'' as AlasanCancel,(A.NilaiBobot*100)NilaiBobot,A.Keterangan," +
                       "A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval," +
                       "B.PointNilai,CASE B.Approval WHEN 2 THEN (A.NilaiBobot*B.PointNilai) ELSE 0 END Nilai from ISO_KPI as A,ISO_KPIDetail as B where A.ID=B.KPIID  and A.RowStatus>-1 and " +
                       "B.RowStatus>-1 " + this.Criteria + " " + this.OrderBy;
        return query;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sd = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sd.HasRows)
        {
            while (sd.Read())
            {
                arrData.Add(new SOP
                {
                    ID = Convert.ToInt32(sd["ID"].ToString()),
                    TaskID = Convert.ToInt32(sd["idDetail"].ToString()),
                    TaskNo = sd["SOPNo"].ToString(),
                    NewTask = sd["SOPName"].ToString(),
                    BagianName = sd["BagianName"].ToString(),
                    NilaiBobot = Convert.ToDecimal(sd["NilaiBobot"].ToString()),
                    Nilai = Convert.ToDecimal(sd["Nilai"].ToString()),
                    PointNilai = Convert.ToDecimal(sd["PointNilai"].ToString()),
                    TargetKe = Convert.ToInt32(Convert.ToDecimal(sd["PointNilai"].ToString())),
                    Ket = sd["Keterangan"].ToString(),
                    KetTargetKe = sd["KetTargetKe"].ToString(),
                    TglTarget = Convert.ToDateTime(sd["TglTargetSelesai"].ToString()),
                    Pic = sd["PIC"].ToString(),
                    Status = Convert.ToInt32(sd["Status"].ToString()),
                    TglMulai = Convert.ToDateTime(sd["tglMulai"].ToString())
                });
            }
        }
        return arrData;

    }
    public ArrayList RetrieveKPI()
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sd = da.RetrieveDataByString(this.kpiQuery());
        if (da.Error == string.Empty && sd.HasRows)
        {
            while (sd.Read())
            {
                arrData.Add(new SOP
                {
                    ID = Convert.ToInt32(sd["ID"].ToString()),
                    TaskID = Convert.ToInt32(sd["idDetail"].ToString()),
                    TaskNo = sd["KPINo"].ToString(),
                    NewTask = sd["KPIName"].ToString(),
                    BagianName = sd["BagianName"].ToString(),
                    NilaiBobot = Convert.ToDecimal(sd["NilaiBobot"].ToString()),
                    Nilai = Convert.ToDecimal(sd["Nilai"].ToString()),
                    PointNilai = Convert.ToDecimal(sd["PointNilai"].ToString()),
                    TargetKe = Convert.ToInt32(Convert.ToDecimal(sd["PointNilai"].ToString())),
                    Ket = sd["Keterangan"].ToString(),
                    KetTargetKe = sd["KetTargetKe"].ToString(),
                    TglTarget = Convert.ToDateTime(sd["TglTargetSelesai"].ToString()),
                    Pic = sd["PIC"].ToString(),
                    Status = Convert.ToInt32(sd["Status"].ToString()),
                    TglMulai = Convert.ToDateTime(sd["tglMulai"].ToString())
                });
            }
        }

        return arrData;
    }

}
public class SOP : Task
{
    public decimal Nilai { get; set; }
    public decimal Score { get; set; }
    public string KetTargetKe { get; set; }
    public decimal NilaiBobot { get; set; }
}
public class TaskAttachment : GRCBaseDomain
{
    public string FileName { get; set; }
    public int TaskID { get; set; }
}