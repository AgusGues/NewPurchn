using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class FormApprovalPES5R : System.Web.UI.Page
    {
        int kpi = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadBulan();
                clearForm();
                LoadDept();
                Session["id"] = null;
                LoadUserSOP();

            }
        }

        private void LoadSOP(int intRow)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrSOP = new ArrayList();
            ArrayList arrSOP2 = new ArrayList();

            if (Session["ListOpenSOP"] != null)
                arrSOP = (ArrayList)Session["ListOpenSOP"];

            if (intRow < arrSOP.Count && intRow > -1)
            {
                ISO_SOP sop = new ISO_SOP();
                sop = (ISO_SOP)arrSOP[intRow];

                //if (sop.UserID > 0)
                if (sop.DeptID > 0)

                {
                    string sopname = "";
                    if (sop.KPI == 1)
                    {
                        sopname = "kpiname";
                    }
                    else
                    {
                        sopname = "sopname";
                    }

                    //txtPICname.Text = sop.Pic.ToUpper();
                    //txtUserID.Text = sop.UserID.ToString();
                    //txtJabatan.Text = sop.BagianName.ToUpper();
                    //txtJabatanID.Value = sop.BagianID.ToString();

                    //Penambahan agus 24-06-2022
                    txtDepartemen.Text = sop.DeptName.ToString();
                    txtDepartemenID.Value = sop.DeptID.ToString();
                    //Penambahan agus 24-06-2022

                    /* show item sop/kpi on list */
                    Approval5R app = new Approval5R();

                    //app.Criteria = sop.UserID.ToString();

                    app.Criteria = sop.DeptID.ToString();

                    app.Field = "Periode";
                    app.Criteria2 = " and " + sopname + " like 'pelaksanaan 5r%' ";
                    if (sop.KPI == 1)
                    {
                        app.Table = "ISO_KPI";
                        kpi = 1;
                        Session["kpi"] = 1;
                    }
                    else
                    {
                        app.Table = "ISO_SOP";
                        kpi = 2;
                        Session["kpi"] = 2;
                    }
                    arrSOP2 = app.Retrieve();
                    lstH.DataSource = arrSOP2;
                    lstH.DataBind();
                }
            }
            else
            {
                if (intRow == -1)
                {
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                }
                else
                {
                    ViewState["counter"] = (intRow == 0) ? 0 : (int)ViewState["counter"] - 1;
                }
                ISO_SOP sop = new ISO_SOP();
                sop = (arrSOP.Count == 0) ? new ISO_SOP() : (ISO_SOP)arrSOP[(int)ViewState["counter"]];

            }
        }
        protected void lstRkp_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            CheckBox cx = (CheckBox)e.Item.FindControl("chk");
            TextBox txt = (TextBox)e.Item.FindControl("txtAlasanUnApprove");
            Label x1 = (Label)e.Item.FindControl("xxx");
            //Label x2 = (Label)e.Item.FindControl("xxx1");
            ISO_SOP iso_sop = (ISO_SOP)e.Item.DataItem;
            cx.ToolTip = iso_sop.SopID.ToString();
            txt.ToolTip = "Tulisan Alasan UnApprove disini..";
            // x2.ToolTip = iso_sop.Penilaianx1.ToString();
            switch (iso_sop.Approval)
            {
                case 0:
                    x1.Text = "Open";
                    break;
                case 1:
                    x1.Text = "UnApproved";
                    break;
                default:
                    x1.Text = "";
                    break;
            }
            //Image editx = (Image)e.Item.FindControl("edit");
            //Image simpanx = (Image)e.Item.FindControl("simpan");
            //string querystr = iso_sop.SopID.ToString();
            ////editx.Attributes.Add("onclick", "updatePES(" + querystr.ToString() + ")");
        }
        protected void lstH_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrTot = new ArrayList();
            Repeater lst = (Repeater)e.Item.FindControl("lstRkp");
            ISO_SOP p = (ISO_SOP)e.Item.DataItem;
            Approval5R ps = new Approval5R();
            ArrayList arrSOP = new ArrayList();

            ps.Criteria = " where xx.Description like 'Pelaksanaan 5r%' and xx.Approval< 2 and xx.DeptID=" + txtDepartemenID.Value;
            //ps.Criteria += " and xx.BagianID=" + txtJabatanID.Value;
            ps.Bulan = p.Bulan.ToString();
            ps.Tahun = p.Tahun.ToString();
            if (kpi == 1)
            {
                ps.Field = "ItemKPI";
            }
            else
            {
                ps.Field = "ItemSOP";
            }
            arrData = ps.Retrieve();
            lst.DataSource = arrData;
            lst.DataBind();
        }

        
        protected void lstH_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

            CheckBox cb = (CheckBox)e.Item.FindControl("chkAll");
            Repeater lstDetail = (Repeater)e.Item.FindControl("lstRkp");
            CheckBox cbi = (CheckBox)lstDetail.FindControl("chk");
            cbi.Checked = (cb.Checked == true) ? true : false;

        }
        protected void chkAll_Check(object sender, EventArgs e)
        {
            var idx = (CheckBox)sender;
            int ids = int.Parse(idx.CssClass.ToString());
            CheckBox cxb = (CheckBox)lstH.Items[ids].FindControl("chkAll2");
            Repeater lstDetail = (Repeater)lstH.Items[ids].FindControl("lstRkp");
            for (int i = 0; i < lstDetail.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstDetail.Items[i].FindControl("chk");
                chk.Checked = (cxb.Checked == true) ? true : false;
                chk.Enabled = (cxb.Checked == true) ? false : true;
            }
            btnUpdate.Disabled = (cxb.Checked == true) ? false : true;
            btnUnUpdate.Disabled = (cxb.Checked == true) ? false : true;
        }
        protected void chk_Checked(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            btnUpdate.Disabled = (cb.Checked == true) ? false : true;
            btnUnUpdate.Disabled = (cb.Checked == true) ? false : true;




        }
        private void LoadCategoriNew(int isoUserID)
        {
            ArrayList arrData = new ArrayList();
            CategoryFacade cf = new CategoryFacade();
            cf.Criteria = " and ic.DeptID=" + ddlDept.SelectedValue;
            cf.Criteria += "and uc.SectionID=" + ddlBagian.SelectedValue.ToString();
            cf.Criteria += "and uc.UserID=" + isoUserID;
            cf.Criteria += "and uc.PesType=3 order by ic.ID";
            arrData = cf.RetrieveNewCat();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("-- Pilih Category --", "0"));
            foreach (Category ct in arrData)
            {
                ddlCategory.Items.Add(new ListItem(ct.CategoryDescription, ct.ID.ToString()));
            }
        }
        private void LoadUserSOP()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserGroup(users2.ID);

            if (deptFacade.Error == string.Empty)
            {
                if (users2.ID == dept.UserID && dept.UserGroupID > 200)
                {
                    DisplayAJAXMessage(this, "User-ID anda Bukan DiLevel Approve");
                    Response.Redirect("../../Home.aspx", false);
                    Response.End();
                }
                else
                {


                    LoadOpenSOP();

                    ViewState["counter"] = 0;
                    int counter = (int)ViewState["counter"];
                    if (Request.QueryString["SOPNo"] != null)
                    {
                        counter = FindSOP(Request.QueryString["SOPNo"].ToString());
                        ViewState["counter"] = counter;
                    }

                    LoadSOP(counter);
                }
            }

        }
        private void LoadOpenSOP()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            //Dept dept = deptFacade.RetrieveDeptByUserID2(users2.ID);
            ArrayList arrDep = deptFacade.RetrieveForISO(users2.ID);
            string dep = string.Empty;
            string LevelApp = "";
            int grpID = 0;
            foreach (Dept de in arrDep)
            {
                dep += de.DeptID.ToString() + ",";
                grpID = de.UserGroupID;
            }
            Approval5R app = new Approval5R();
            ArrayList arrSOP = new ArrayList();
            switch (grpID)
            {
                case 50:
                    LevelApp = " and ISO_Dept.UserGroupID=100";
                    break;
                case 100:
                    LevelApp = " and ISO_Dept.UserGroupID=200 ";
                    break;
                case 200:
                    LevelApp = " and ISO_Dept.UserGroupID=200 ";
                    break;
            }
            /* Load PIC*/
            string tabel = (Request.QueryString["p"] == null) ? "ISO_SOP" : "ISO_KPI";
            string sopname = (Request.QueryString["p"] == null) ? "sopname" : "kpiname";
            string[] tab = tabel.Split('_');
            //app.Criteria = " where rowstatus >-1 and DeptID in(select id from dept) ";
            //app.Criteria += " and UserID in(SELECT ISO_Users.ID FROM ISO_Users  inner join iso_dept on ISO_Users.UserID=iso_dept.userid  WHERE iso_dept.RowStatus>-1 and ISO_Users.RowStatus>-1 " + LevelApp + " and  UserName in(";
            //app.Criteria += " (SELECT PIC FROM " + tabel + " as K LEFT JOIN " + tabel + "Detail as kd on kd." + tab[1].ToString() + "ID =k.ID " +
            //                " Where k."+sopname+" like 'pelaksanaan 5r%' and ((k.Status Between 0 and 1) AND kd.Approval between 0 and 1) and k.RowStatus >-1 and DeptID in(select id from dept)) ))";
            //revisi karena pelaksanaan 5r masuk kedalam KPI
            app.Criteria = " where rowstatus >-1 and DeptID in(select id from dept) ";
            app.Criteria += " and UserID in(SELECT ISO_Users.ID FROM ISO_Users  inner join iso_dept on ISO_Users.UserID=iso_dept.userid  WHERE iso_dept.RowStatus>-1 and ISO_Users.RowStatus>-1 " + LevelApp + " and  UserName in(";
            app.Criteria += " (SELECT PIC FROM ISO_SOP as K LEFT JOIN ISO_SOPDetail as kd on kd.SopID =k.ID " +
                            " Where k.sopname like 'pelaksanaan 5r%' and ((k.Status Between 0 and 1) AND kd.Approval between 0 and 1) and k.RowStatus >-1 and DeptID in(select id from dept) UNION SELECT PIC FROM ISO_KPI K LEFT JOIN ISO_KPIDetail as kd on kd.kpiID =k.ID  Where k.kpiname like 'pelaksanaan 5r%' and ((k.Status Between 0 and 1) AND kd.Approval between 0 and 1) and k.RowStatus >-1 and DeptID in(select id from dept)) ))";
            app.Field = "PIC";
            arrSOP = app.Retrieve();

            if (arrSOP.Count > 0)
            {
                Session["ListOpenSOP"] = arrSOP;
            }
        }
        private int FindSOP(string strNoSOP)
        {
            Users users = (Users)Session["Users"];

            ArrayList arrSOP = new ArrayList();
            int counter = 0;

            if (Session["ListOpenSOP"] != null)
                arrSOP = (ArrayList)Session["ListOpenSOP"];

            foreach (ISO_SOP sop in arrSOP)
            {
                if (sop.Pic == strNoSOP)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }
        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOpenSOP"] = null;
            ViewState["TypeBobotSOP"] = null;
            txtTglMulai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTaskNo.Text = string.Empty;
            txtPic.Text = "";// (((Users)Session["Users"]).UserName);
            ddlDept.SelectedIndex = 0;
            ddlBagian.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            txtBobotNilai.Text = string.Empty;
            txtKeterangan.Text = string.Empty;

            ArrayList arrList = new ArrayList();
            arrList.Add(new ISO_SOP());

            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
           
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
                if (ViewState["TypeBobotSOP"] != null)
                {
                    if (ViewState["TypeBobotSOP"].ToString() == "%")
                        e.Row.Cells[2].Text = (decimal.Parse(e.Row.Cells[2].Text) * 100).ToString("N0") + "%";
                    else
                        e.Row.Cells[2].Text = e.Row.Cells[2].Text;
                }

                
            }
        }
        private void LoadDept()
        {
            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
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
        public ArrayList LoadDept(string Criteria)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBoardPurch");
            foreach (DataRow d in da.Tables[0].Rows)
            {
                arrData.Add(new Dept
                {
                    ID = Convert.ToInt32(d["ID"].ToString()),
                    AlisName = d["DeptName"].ToString()
                });
            }
            return arrData;
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
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = dept.DeptID.ToString();

        }
        protected void btnUnUpdate_ServerClick(object sender, EventArgs e)
        {
            /// kurangi yang di session
            /// kayak schedule

            Users users = (Users)Session["Users"];
            string strError = string.Empty;
            //TaskFacade taskFacade = new TaskFacade();
            //Task task = new Task();
            ISO_SOPFacade sopFacade = new ISO_SOPFacade();
            ISO_SOP sop = new ISO_SOP();
            int JmlBaris = lstH.Items.Count;
            for (int i = 0; i < JmlBaris; i++)
            {
                Repeater rpt = (Repeater)lstH.Items[i].FindControl("lstRkp");
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    CheckBox chk = (CheckBox)rpt.Items[n].FindControl("chk");
                    if (chk.Checked == true)
                    {
                        TextBox txtAlasanUnApprove = (TextBox)rpt.Items[n].FindControl("txtAlasanUnApprove");
                        sop = (Request.QueryString["p"] == null) ?
                            sopFacade.RetrieveByNo1(chk.ToolTip.ToString().TrimEnd()) :
                            sopFacade.RetrieveByNo2(chk.ToolTip.ToString().TrimEnd());
                        sop.PesType = (Request.QueryString["p"] == null) ? 3 : 1;
                        sop.App = (sop.App < 2) ? 1 : sop.App;   //users.Apv;
                        sop.AlasanUnApprove = txtAlasanUnApprove.Text;
                        ArrayList arrOPDetail = new ArrayList();

                        ISO_SOPProcessFacade sopProcessFacade = new ISO_SOPProcessFacade(sop, new ISO_DocumentNo());


                        if (sop.ID > 0)
                        {
                            strError = sopProcessFacade.UpdateSOPDetailApproval1();
                            EventLogProcess mp = new EventLogProcess();
                            EventLog evl = new EventLog();
                            mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                            mp.Pilihan = "Insert";
                            evl.UserID = ((Users)Session["Users"]).ID;
                            evl.AppLevel = ((Users)Session["Users"]).Apv;
                            evl.DocNo = sop.SopNo.ToString();
                            evl.DocType = (Request.QueryString["p"] == null) ? "PES-SOP" : "PES-KPI";
                            evl.AppDate = DateTime.Now;
                            evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                            mp.EventLogInsert(evl);
                        }

                    }
                }
            }

            if (strError == string.Empty)
            {
                string judul = (Request.QueryString["p"] == null) ? "SOP" : Request.QueryString["p"].ToString();
                InsertLog("Approve " + judul);

                ArrayList arrSOP = new ArrayList();
                if (Session["ListOpenSOP"] != null)
                {
                    arrSOP = (ArrayList)Session["ListOpenSOP"];
                    ((ISO_SOP)arrSOP[(int)ViewState["counter"]]).Status = 1;

                    Session["ListOpenSOP"] = arrSOP;
                }

                if (sop.ID > 0)
                {
                    ViewState["counter"] = (int)ViewState["counter"] + 1;

                    LoadSOP((int)ViewState["counter"]);
                }
                if (Request.QueryString["p"] == null)
                {
                    Response.Redirect("FormApprovalPES5R.aspx");
                }
                else
                {
                    Response.Redirect("FormApprovalPES5R.aspx?p=KPI");
                }
            }
            else
            {
                DisplayAJAXMessage(this, strError);
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            /// kurangi yang di session
            /// kayak schedule

            Users users = (Users)Session["Users"];
            string strError = string.Empty;
            //TaskFacade taskFacade = new TaskFacade();
            //Task task = new Task();
            ISO_SOPFacade sopFacade = new ISO_SOPFacade();
            ISO_SOP sop = new ISO_SOP();
            int JmlBaris = lstH.Items.Count;
            kpi = Convert.ToInt32(Session["kpi"]);
            for (int i = 0; i < JmlBaris; i++)
            {
                Repeater rpt = (Repeater)lstH.Items[i].FindControl("lstRkp");
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    CheckBox chk = (CheckBox)rpt.Items[n].FindControl("chk");
                    if (chk.Checked == true)
                    {
                        TextBox txtAlasanUnApprove = (TextBox)rpt.Items[n].FindControl("txtAlasanUnApprove");
                        if (kpi == 1)
                        {
                            sop = sopFacade.RetrieveByNo2(chk.ToolTip.ToString().TrimEnd());
                            sop.PesType = 1;
                        }
                        else
                        {
                            sop = sopFacade.RetrieveByNo1(chk.ToolTip.ToString().TrimEnd());
                            sop.PesType = 3;
                        }
                        sop.App = (sop.App < 2) ? 2 : sop.App;   //users.Apv;
                        sop.AlasanUnApprove = txtAlasanUnApprove.Text;
                        ArrayList arrOPDetail = new ArrayList();

                        ISO_SOPProcessFacade sopProcessFacade = new ISO_SOPProcessFacade(sop, new ISO_DocumentNo());


                        if (sop.ID > 0)
                        {
                            strError = sopProcessFacade.UpdateSOPDetailApproval();
                            EventLogProcess mp = new EventLogProcess();
                            EventLog evl = new EventLog();
                            mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                            mp.Pilihan = "Insert";
                            evl.UserID = ((Users)Session["Users"]).ID;
                            evl.AppLevel = ((Users)Session["Users"]).Apv;
                            evl.DocNo = sop.SopNo.ToString();
                            evl.DocType = (Request.QueryString["p"] == null) ? "PES-SOP" : "PES-KPI";
                            evl.AppDate = DateTime.Now;
                            evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                            mp.EventLogInsert(evl);
                        }

                    }
                }
            }

            if (strError == string.Empty)
            {
                string judul = (Request.QueryString["p"] == null) ? "SOP" : Request.QueryString["p"].ToString();
                InsertLog("Approve " + judul);

                ArrayList arrSOP = new ArrayList();
                if (Session["ListOpenSOP"] != null)
                {
                    arrSOP = (ArrayList)Session["ListOpenSOP"];
                    ((ISO_SOP)arrSOP[(int)ViewState["counter"]]).Status = 1;

                    Session["ListOpenSOP"] = arrSOP;
                }

                if (sop.ID > 0)
                {
                    ViewState["counter"] = (int)ViewState["counter"] + 1;

                    LoadSOP((int)ViewState["counter"]);
                }
                if (Request.QueryString["p"] == null)
                {
                    Response.Redirect("FormApprovalPES5R.aspx");
                }
                else
                {
                    Response.Redirect("FormApprovalPES5R.aspx?p=KPI");
                }
            }
            else
            {
                DisplayAJAXMessage(this, strError);
            }
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Approval SOP";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtTaskNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlBagian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(2);
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection();
        }
        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;

            LoadSOP((int)ViewState["counter"]);
        }
        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;

            LoadSOP((int)ViewState["counter"]);
        }
        protected void lstTask_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("sts");
            ISO_SOP tsk = (ISO_SOP)e.Item.DataItem;
            switch (tsk.Status)
            {
                case 0: lbl.Text = "Open"; break;
                case 1: lbl.Text = "UnSolved"; break;
                case 2: lbl.Text = "Solved"; break;
                case 9: lbl.Text = "Cancel"; break;
            }
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();

        }
        private void LoadTahun()
        {
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--pilih bulan--", "0"));
            int n = 0;
            for (int i = 0; i < 12; i++)
            {
                n = i + 1;
                ddlBulan.Items.Add(new ListItem(Global.nBulan(n).ToString(), n.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }


    }

    public class Approval5R

    {
        private ArrayList arrData = new ArrayList();
        private ISO_SOP sp = new ISO_SOP();
        public string Field { get; set; }
        public string Criteria { get; set; }
        public string Criteria2 { get; set; }
        public string Tahun { get; set; }
        public string Bulan { get; set; }
        public string Table { get; set; }
        public string Query()
        {

            string query = string.Empty;
            switch (this.Field)
            {
                case "PIC":
                    query = "select Distinct " +
                            //"UserName,isnull(UserID,0)UserID,isnull(BagianID,0)BagianID ," +
                            //"(Select BagianName from ISO_Bagian Where ID=BagianID)BagianName, "+

                            //penambahan agus 24-06-2022
                            "isnull(DeptID,0)DeptID,(select DeptName from Dept where ID in(DeptID))DeptName, " +
                            //penambahan agus 24-06-2022

                            "(CASE WHEN username IN (SELECT PIC FROM ISO_KPI WHERE kpiname like 'pelaksanaan 5r%') THEN 1 ELSE 2 end) KPI" +
                             " from UserAccount " + this.Criteria + " ";
                    //"order by userName ";
                    break;
                case "ItemSOP":
                    query = "select *,case when Approval=0 then xx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                            "select x.*, " +

                            //tambahan agus 24-06-2022
                            "sp.PIC, " +
                            "sp.DeptID, " +
                            //tambahan agus 24-06-2022

                            "sp.BagianID,sp.ID as SOPID,sp.Keterangan,sp.AlasanUnApprove,sd.PointNilai,sd.KetTargetKe,sd.Approval from( " +
                            "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,case when iu.Penilaian<6 then 'Target Bulan' when iu.Penilaian=6 then 'Target Semesteran' else 'Target Tahunan' end Penilaianx," +
                            " case when iu.Penilaian2<6 then 'Target Bulan' when iu.Penilaian2=6 then 'Target Semesteran' else 'Target Tahunan' end Penilaianx1,iu.PesType, " +
                            "Case when iu.TypeBobot='%' then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,ic.KodeUrutan " +
                            "from ISO_UserCategory iu   " +
                            "Left Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                            "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=3  " +
                            ") as x " +
                            "LEFT JOIN ISO_SOP as sp " +
                            "on sp.PIC=(Select Top 1 UserName from ISO_Users where ID=x.UserID and RowStatus>-1) and sp.RowStatus>-1 " +
                            "left JOIN ISO_SOPDetail as sd " +
                            "on sd.SOPID=sp.ID and sd.RowStatus>-1 " +
                            "where sp.CategoryID=x.ID and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                            ") as xx " + this.Criteria +
                            " order by xx.KodeUrutan";
                    break;
                case "Periode":

                    query = "select Distinct (CAST(DATENAME(MONTH,TglMulai)as Varchar) +' '+Cast(YEAR(TglMulai) as varchar)) AS Periode, " +
                            "YEAR(TglMulai)Tahun,MONTH(TglMulai)Bulan " +
                            "from " + this.Table + " " +
                            "left join " + this.Table + "Detail ikp ON ikp." + (this.Table.Split('_'))[1].ToString() + "ID =" + this.Table + ".ID " +

                            //"where PIC=(Select Username from ISO_Users where ID=" + this.Criteria + " ) " + this.Criteria2 + "  " +

                            "where DeptID=(Select Distinct DeptID from ISO_Users where DeptID=" + this.Criteria + " ) " + this.Criteria2 + "  " +

                            "and ((" + this.Table + ".Status between 0 and 1)AND ikp.Approval BETWEEN 0 AND 1) and " + this.Table + ".RowStatus>-1 " +
                            "AND ikp.RowStatus>-1 " +
                            "order by YEAR(TglMulai),MONTH(TglMulai)";
                    break;
                case "ItemKPI":
                    query = "select *,case when Approval=0 then xx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                            "select x.*, " +

                            //tambahan agus 24-06-2022
                            "sp.PIC, " +
                            "sp.DeptID, " +
                            //tambahan agus 24-06-2022

                            "sp.BagianID, " +
                            "sp.ID as SOPID, " +
                            "sp.Keterangan, " +
                            "sp.AlasanUnApprove, " +
                            "sd.PointNilai, " +
                            "sd.KetTargetKe,sd.Approval from( " +
                            "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,case when iu.Penilaian<6 then 'Target Bulan' when iu.Penilaian=6 then 'Target Semesteran' else 'Target Tahunan' end Penilaianx," +
                            "case when iu.Penilaian2<6 then 'Target Bulan' when iu.Penilaian2=6 then 'Target Semesteran' else 'Target Tahunan' end Penilaianx1,iu.PesType, " +
                            "Case when iu.TypeBobot='%' then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,ic.KodeUrutan " +
                            "from ISO_UserCategory iu   " +
                            "Left Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                            "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=1  " +
                            ") as x " +
                            "LEFT JOIN ISO_KPI as sp " +
                            "on sp.PIC=(Select Top 1 UserName from ISO_Users where ID=x.UserID and RowStatus>-1) and sp.RowStatus>-1 " +
                            "left JOIN ISO_KPIDetail as sd " +
                            "on sd.KPIID=sp.ID and sd.RowStatus>-1 " +
                            "where sp.CategoryID=x.ID and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                            ") as xx " + this.Criteria +
                            " order by xx.KodeUrutan";
                    break;
            }
            return query;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        private ISO_SOP GenerateObject(SqlDataReader sdr)
        {
            sp = new ISO_SOP();
            switch (this.Field)
            {
                case "PIC":

                    //sp.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    //sp.Pic = sdr["UserName"].ToString();
                    //sp.BagianName = sdr["BagianName"].ToString();
                    //sp.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());


                    sp.KPI = Convert.ToInt32(sdr["KPI"].ToString());
                    sp.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    sp.DeptName = sdr["DeptName"].ToString();

                    break;
                case "Periode":
                    sp.Periode = sdr["Periode"].ToString();
                    sp.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    sp.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    break;
                case "ItemSOP":
                    sp.ID = Convert.ToInt32(sdr["ID"].ToString());
                    sp.UserID = Convert.ToInt32(sdr["UserID"].ToString());

                    //Penambahan Agus 24-06-2022
                    sp.SOPName = sdr["PIC"].ToString();
                    //Penambahan Agus 24-06-2022

                    //sp.SOPName = sdr["Description"].ToString();

                    sp.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString());
                    sp.TypePes = Convert.ToInt32(sdr["PesType"].ToString());
                    sp.Target = sdr["Target"].ToString();
                    sp.Pencapaian = sdr["Keterangan"].ToString();
                    sp.Score = Convert.ToDecimal(sdr["PointNilai"].ToString());
                    sp.Nilai = Convert.ToDecimal(sdr["Nilai"].ToString());
                    sp.SopID = Convert.ToInt32(sdr["SOPID"].ToString());
                    sp.AlasanUnApprove = sdr["AlasanUnApprove"].ToString();
                    sp.Approval = Convert.ToInt32(sdr["Approval"].ToString());
                    sp.Penilaianx = sdr["Penilaianx"].ToString();
                    sp.Penilaianx1 = sdr["Penilaianx1"].ToString();

                    break;
                case "ItemKPI":
                    sp.ID = Convert.ToInt32(sdr["ID"].ToString());
                    sp.UserID = Convert.ToInt32(sdr["UserID"].ToString());

                    //Penambahan Agus 24-06-2022
                    sp.SOPName = sdr["PIC"].ToString();
                    //Penambahan Agus 24-06-2022

                    //sp.SOPName = sdr["Description"].ToString();

                    sp.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString());
                    sp.TypePes = Convert.ToInt32(sdr["PesType"].ToString());
                    sp.Target = sdr["Target"].ToString();
                    sp.Pencapaian = sdr["Keterangan"].ToString();
                    sp.Score = Convert.ToDecimal(sdr["PointNilai"].ToString());
                    sp.Nilai = Convert.ToDecimal(sdr["Nilai"].ToString());
                    sp.SopID = Convert.ToInt32(sdr["SOPID"].ToString());
                    sp.AlasanUnApprove = sdr["AlasanUnApprove"].ToString();
                    sp.Penilaianx = sdr["Penilaianx"].ToString();
                    sp.Penilaianx1 = sdr["Penilaianx1"].ToString();
                    break;
            }
            return sp;
        }

    }
}