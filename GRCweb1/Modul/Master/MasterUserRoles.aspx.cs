using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.Master
{
    public partial class MasterUserRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadRoles();
            }
            //else
            //{
            //    if (txtUserID.Text != string.Empty)
            //    {
            //        UsersFacade userFacade = new UsersFacade();
            //        Users users = userFacade.RetrieveByUserID(txtUserID.Text);
            //        if (userFacade.Error == string.Empty)
            //        {
            //            txtUserName.Text = users.UserName;

            //            UserRolesFacade userRolesFacade = new UserRolesFacade();
            //            ArrayList arrUserRoles = userRolesFacade.RetrieveByUserId(users.ID);
            //            if (arrUserRoles.Count > 0)
            //            {
            //                LoadUserRoles(arrUserRoles);
            //            }
            //        }
            //    }
            //}
        }


        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                UsersFacade userFacade = new UsersFacade();
                Users users = new Users();
                if (ddlSearch.SelectedIndex == 0)
                    users = userFacade.RetrieveByUserID( txtSearch.Text);
                else
                    users = userFacade.RetrieveByUserName(txtSearch.Text);

                if (userFacade.Error == string.Empty)
                {
                    txtUserID.Text = users.UserID;
                    txtUserName.Text = users.UserName;

                    UserRolesFacade userRolesFacade = new UserRolesFacade();
                    ArrayList arrUserRoles = userRolesFacade.RetrieveByUserId(users.ID);
                    if (arrUserRoles.Count > 0)
                    {
                        LoadUserRoles(arrUserRoles);

                        txtSearch.Text = string.Empty;
                    }
                }
            }
        }

        private void LoadUserRoles(ArrayList arrUserRoles)
        {
            ArrayList arrRoles = (ArrayList)Session["ListOfRoles"];
            foreach (UserRoles userRoles in arrUserRoles)
            {
                int i = 0;
                foreach (Roles roles in arrRoles)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                    if (roles.ID == userRoles.RoleID)
                    {
                        cb.Checked = true;
                        break;
                    }

                    i = i + 1;
                }
            }
        }

        private void LoadRoles()
        {
            RolesFacade rolesFacade = new RolesFacade();
            ArrayList arrRoles = rolesFacade.Retrieve();
            if (rolesFacade.Error == string.Empty)
            {
                Session["ListOfRoles"] = arrRoles;
                GridView1.DataSource = arrRoles;
                GridView1.DataBind();
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (ValidasiText() != string.Empty)
            {
                DisplayAJAXMessage(this, ValidasiText());
                return;
            }

            UsersFacade userFacade = new UsersFacade();
            Users users = userFacade.RetrieveByUserID( txtUserID.Text);
            if (userFacade.Error == string.Empty)
            {
                UserRolesFacade userRolesFacade = new UserRolesFacade();
                int intResult = 0;
                UserRoles userRoles = new UserRoles();
                userRoles.UserID = users.ID;
                intResult = userRolesFacade.Delete(userRoles);
                if (userRolesFacade.Error == string.Empty)
                {
                    ArrayList arrRoles = (ArrayList)Session["ListOfRoles"];
                    int i = 0;
                    foreach (Roles roles in arrRoles)
                    {
                        CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                        if (cb.Checked)
                        {
                            userRoles.RoleID = roles.ID;
                            intResult = userRolesFacade.Insert(userRoles);
                            if (userRolesFacade.Error != string.Empty)
                            {
                                break;
                            }
                        }

                        i = i + 1;
                    }

                    InsertLog("Edit");

                    //  clearForm();
                }
            }
        }

        private void clearForm()
        {
            txtUserID.Text = string.Empty;
            txtUserName.Text = string.Empty;
            LoadRoles();

            txtUserID.Focus();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadRoles();
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master User Roles";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtUserID.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidasiText()
        {
            if (txtUserID.Text == string.Empty)
                return "User tidak ada";

            return string.Empty;
        }

    }
}