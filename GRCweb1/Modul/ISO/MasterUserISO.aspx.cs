using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class MasterUserISO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                LoadCompany();
                LoadDepartment();
                LoadDataGridUsers();
                TypeUnitKerjaID.SelectedValue = "2";
                TypeUnitKerjaID_SelectedIndexChanged(null, null);
                UnitKerjaID.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
                //CompanyID.SelectedValue=((Users)Session["Users"]).
                //((Users)Session["Users"]).UserName
            }
        }
        private void clearForm()
        {
            //userid.ReadOnly = false;
            username.ReadOnly = false;
            pwd.ReadOnly = false;
            ViewState["id"] = null;
            userid.Text = string.Empty;
            username.Text = string.Empty;
            pwd.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            pwd.Attributes["value"] = string.Empty;
            TypeUnitKerjaID.SelectedIndex = 0;
            //UnitKerjaID.Items.Clear();
            userid.Focus();
            CompanyID.SelectedIndex = 0;
            PlantID.SelectedIndex = 0;
            //PlantID.Items.Clear();
            DeptID.SelectedIndex = 0;
            //BagianID.Items.Clear();
            ID.Text = string.Empty;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (ID.Text == string.Empty)
            {
                simpan_baru();
            }
            else
            {
                simpan_update();
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            ISO_UserFacade usFacade = new ISO_UserFacade();
            ISO_Users users = new ISO_Users();
            //users.ID = int.Parse(ID.Text.ToString());

            //penambahan agus 25-10-2022
            users.ID = int.Parse( usFacade.GetNo(username.Text));
            //penambahan agus 25-10-2022

            users.LastModifiedBy = Global.UserLogin.UserName;
            
            int intResult = usFacade.Delete(users);
            if (usFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridUsers();
                InsertLog("Delete");
                //clearForm();
            }
            else
            {
                DisplayAJAXMessage(this, usFacade.Error);
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadGrid(LoadGridByCriteria());
        }
        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrUsers = new ArrayList();
            ISO_UserFacade usersFacade = new ISO_UserFacade();
            if (txtSearch.Text != string.Empty)
                arrUsers = usersFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrUsers.Count > 0)
            {
                return arrUsers;
            }

            arrUsers.Add(new ISO_Users());
            return arrUsers;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListUserISO.PageIndex = e.NewPageIndex;

            if (txtSearch.Text != string.Empty)
            {
                LoadGrid(LoadGridByCriteria());
            }
            else
            {
                LoadDataGridUsers();
            }
        }
        private void LoadInformation(int intTypeUnitKerja)
        {
            UnitKerjaID.Items.Clear();

            if (intTypeUnitKerja == 0)
                return;

            UnitKerjaID.Items.Add(new ListItem("Pilih Unit Kerja", "0"));

            if (intTypeUnitKerja == 1)
            {
                DistributorFacade distributorFacade = new DistributorFacade();
                ArrayList arrDistributor = distributorFacade.Retrieve();
                if (distributorFacade.Error == string.Empty)
                {
                    foreach (Distributor distributor in arrDistributor)
                    {
                        if (distributor.ID > 0)
                        {
                            UnitKerjaID.Items.Add(new ListItem(distributor.DistributorName, distributor.ID.ToString()));
                        }
                    }
                }
            }
            else
            {
                DepoFacade depoFacade = new DepoFacade();
                ArrayList arrDepo = depoFacade.Retrieve();
                if (depoFacade.Error == string.Empty)
                {
                    foreach (Depo depo in arrDepo)
                    {
                        UnitKerjaID.Items.Add(new ListItem(depo.DepoName, depo.ID.ToString()));
                    }
                }
            }
        }
        protected void TypeUnitKerjaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInformation(TypeUnitKerjaID.SelectedIndex);
        }

        private void LoadCompany()
        {
            CompanyID.Items.Clear();
            PlantID.Items.Clear();
            CompanyFacade cmp = new CompanyFacade();
            ArrayList arrCmp = cmp.Retrieve();
            CompanyID.Items.Add(new ListItem("--Pilih--", "0"));
            PlantID.Items.Add(new ListItem("--Pilih--", "0"));
            if (cmp.Error == string.Empty)
            {
                foreach (Company pabrik in arrCmp)
                {
                    CompanyID.Items.Add(new ListItem(pabrik.Nama, pabrik.ID.ToString()));
                    PlantID.Items.Add(new ListItem(pabrik.Nama, pabrik.ID.ToString()));
                }
            }
        }
        private void LoadDepartment()
        {
            DeptID.Items.Clear();
            ISO_DeptFacade Dept = new ISO_DeptFacade();
            ArrayList arrDept = Dept.RetieveByDeptID(0, string.Empty);
            DeptID.Items.Add(new ListItem("--Pilih Dept--", "0"));
            if (Dept.Error == string.Empty)
            {
                foreach (ISO_Dept dpt in arrDept)
                {
                    DeptID.Items.Add(new ListItem(dpt.DeptName, dpt.ID.ToString()));
                }
            }
        }

        private void LoadBagian(int intDept)
        {
            BagianID.Items.Clear();
            ISO_BagianFacade Bagian = new ISO_BagianFacade();
            ArrayList arrBagian = Bagian.RetrieveByDept(intDept);
            BagianID.Items.Add(new ListItem("--Pilih Bagian--", "0"));
            foreach (ISO_Bagian Bag in arrBagian)
            {
                BagianID.Items.Add(new ListItem(Bag.BagianName, Bag.ID.ToString()));
            }
        }
        protected void DeptID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDept = 0;
            ISO_DeptFacade Dept = new ISO_DeptFacade();
            ArrayList arrDept = Dept.RetieveByDeptID(int.Parse(DeptID.SelectedValue), "ID");
            foreach (ISO_Dept dpt in arrDept)
            {
                idDept = dpt.DeptID;
            }

            LoadBagian(idDept);
        }
        protected void userid_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LoadGrid(ArrayList arrUsr)
        {
            ListUserISO.DataSource = arrUsr;
            ListUserISO.DataBind();
        }

        private void LoadDataGridUsers()
        {
            ArrayList arrUsr = new ArrayList();
            ISO_UserFacade Usr = new ISO_UserFacade();
            if (Usr.Error == string.Empty)
            {
                arrUsr = Usr.RetrieveForGrid();
                ListUserISO.DataSource = arrUsr;
                ListUserISO.DataBind();
            }


        }

        protected void ListUserISO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = ListUserISO.Rows[index];
                userid.Text = row.Cells[1].Text;
                userid.ReadOnly = true;
                username.Text = row.Cells[2].Text;
                ISO_UserFacade Usr = new ISO_UserFacade();
                ISO_Users users = Usr.RetrieveByISOuserID(row.Cells[1].Text);//change on 13-03 (conflict with input task)
                ISO_DeptFacade Dp = new ISO_DeptFacade();
                ISO_Dept Dpt = Dp.RetrieveByUserID(users.UserID);
                if (Usr.Error == string.Empty)
                {
                    ISO_Dept Dp2 = Dp.RetrieveByDept(users.DeptID);
                    userid.Text = users.ID.ToString();
                    TypeUnitKerjaID.Text = users.TypeUnitKerja.ToString();
                    LoadInformation(int.Parse(users.TypeUnitKerja.ToString()));
                    UnitKerjaID.Text = users.UnitKerjaID.ToString();
                    CompanyID.Text = users.CompanyID.ToString();
                    PlantID.Text = users.Plant.ToString();
                    txtNIK.Text = users.NIK.ToString();
                    DeptID.Text = Dp2.ID.ToString();
                    LoadBagian(int.Parse(users.DeptID.ToString()));
                    BagianID.Text = users.bagian.ToString();
                    EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
                    ID.Text = users.UserID.ToString();
                }
                pwd.ReadOnly = true;
            }
        }

        protected void ListUserISO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ListUserISO.EditIndex = e.NewEditIndex;
            //ListUserISO.DataBind();
        }

        protected void simpan_baru()
        {
            ISO_UserFacade Usr = new ISO_UserFacade();
            ISO_Users U = new ISO_Users();
            ISO_DeptFacade Dpt = new ISO_DeptFacade();
            ISO_Dept dp = Dpt.RetriveByISODept(int.Parse(DeptID.Text));
            pwd.Text = "1234";
            U.UserID = userid.Text;
            U.UserName = username.Text;
            U.NIK = txtNIK.Text;
            U.TypeUnitKerja = int.Parse(TypeUnitKerjaID.Text);
            U.UnitKerjaID = int.Parse(UnitKerjaID.Text);
            U.CompanyID = int.Parse(CompanyID.Text);
            U.Plant = int.Parse(PlantID.Text);
            U.DeptID = dp.DeptID;  //int.Parse(DeptID.Text);
            U.bagian = int.Parse(BagianID.Text);
            U.CreatedBy = ((Users)Session["Users"]).UserName;
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            U.Password = encryptPasswordFacade.EncryptToString(pwd.Text);
            U.NIK = txtNIK.Text;
            int result = Usr.Insert(U);
            if (Usr.Error == string.Empty && result > 0)
            {
                simpan_ISO_dept();

            }
            else
            {
                DisplayAJAXMessage(this, Usr.Error);
                //message.InnerHtml = Usr.Error;
            }

        }
        protected void simpan_ISO_dept()
        {
            ISO_DeptFacade Dp = new ISO_DeptFacade();
            ISO_Dept Dt = new ISO_Dept();
            ISO_Users Usr = new ISO_Users();
            ISO_UserFacade Uid = new ISO_UserFacade();
            ISO_Users IdUs = Uid.RetrieveByUserID(userid.Text);
            ISO_BagianFacade iB = new ISO_BagianFacade();
            ISO_Bagian ibF = iB.RetrieveById(int.Parse(BagianID.Text));
            ISO_Dept Dtm = Dp.RetriveByISODept(int.Parse(DeptID.SelectedValue));
            Dt.DeptName = Dtm.DeptName;
            Dt.UserID = int.Parse(IdUs.UserID);
            Dt.DeptID = IdUs.DeptID;
            Dt.UserGroupID = ibF.UserGroupID;
            int result = Dp.Insert2(Dt);
            if (Dp.Error == string.Empty && result > 0)
            {
                LoadDataGridUsers();
                InsertLog("Insert");
            }
            else
            {
                //message.InnerHtml = Dp.Error;
                DisplayAJAXMessage(this, Dp.Error);
            }

        }
        protected void Update_ISO_dept()
        {
            ISO_DeptFacade Dp = new ISO_DeptFacade();
            ISO_Dept Dt = new ISO_Dept();
            ISO_Users Usr = new ISO_Users();
            ISO_UserFacade Uid = new ISO_UserFacade();
            ISO_Users IdUs = Uid.RetrieveByUserID(ID.Text);
            ISO_BagianFacade iB = new ISO_BagianFacade();
            ISO_Bagian ibF = iB.RetrieveById(int.Parse(BagianID.Text));
            ISO_Dept Dtm = Dp.RetriveByISODept(int.Parse(DeptID.SelectedValue));
            Dt.DeptName = Dtm.DeptName;
            Dt.UserID = int.Parse(IdUs.UserID);
            Dt.DeptID = IdUs.DeptID;
            Dt.UserGroupID = ibF.UserGroupID;
            Dt.RowStatus = 0;
            Dt.ID = Dtm.ID;
            int result = Dp.Update2(Dt);
            if (Dp.Error == string.Empty)//&& result > 0
            {
                LoadDataGridUsers();
                InsertLog("Update");
            }
            else
            {
                DisplayAJAXMessage(this, Dp.Error);
                //message.InnerHtml = Dp.Error;
            }

        }
        protected void simpan_update()
        {
            ISO_UserFacade Usr = new ISO_UserFacade();
            ISO_Users U = new ISO_Users();
            ISO_DeptFacade dP = new ISO_DeptFacade();
            ISO_Dept dPt = dP.RetriveByISODept(int.Parse(DeptID.Text));
            U.UserID = ID.Text;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from iso_users where userid="+ U.UserID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows){while (sdr.Read()){ U.ID = Convert.ToInt32(sdr["id"].ToString());}}
            //U.ID = int.Parse(userid.Text.ToString());
            U.UserName = username.Text;
            U.NIK = txtNIK.Text;
            U.TypeUnitKerja = int.Parse(TypeUnitKerjaID.Text);
            U.UnitKerjaID = int.Parse(UnitKerjaID.Text);
            U.CompanyID = int.Parse(CompanyID.Text);
            U.Plant = int.Parse(PlantID.Text);
            U.DeptID = dPt.DeptID;
            U.bagian = int.Parse(BagianID.Text);
            U.LastModifiedBy = ((Users)Session["Users"]).UserName;
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            U.Password = encryptPasswordFacade.EncryptToString(pwd.Text);
            int result = Usr.Update(U);
            if (Usr.Error == string.Empty && result > 0)
            {
                Update_ISO_dept();
                LoadDataGridUsers();
                InsertLog("Update");
            }
            else
            {
                DisplayAJAXMessage(this, Usr.Error);
                //message.InnerHtml = Usr.Error;
            }

        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Users";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = userid.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }
        protected void username_TextChanged(object sender, EventArgs e)
        {
            if (userid.Text.Trim() == string.Empty)
                return;
            UsersFacade userFacade = new UsersFacade();
            Users users = userFacade.RetrieveById(Convert.ToInt32(userid.Text.ToString()));

            if (userFacade.Error == string.Empty)
            {
                userid.Text = users.ID.ToString();
            }
            else
            {
                DisplayAJAXMessage(this, "User belum terdafatr di System Purchn");
                username.Text = string.Empty;
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}