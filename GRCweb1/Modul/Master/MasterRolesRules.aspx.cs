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
    public partial class MasterRolesRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadRoles();
                LoadRules();
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
            //if (txtSearch.Text != string.Empty)
            //{
            //    UsersFacade userFacade = new UsersFacade();
            //    Users users = new Users();
            //    if (ddlSearch.SelectedIndex == 0)
            //        users = userFacade.RetrieveByUserID(txtSearch.Text);
            //    else
            //        users = userFacade.RetrieveByUserName(txtSearch.Text);

            //    if (userFacade.Error == string.Empty)
            //    {
            //        txtUserID.Text = users.UserID;
            //        txtUserName.Text = users.UserName;

            //        UserRolesFacade userRolesFacade = new UserRolesFacade();
            //        ArrayList arrUserRoles = userRolesFacade.RetrieveByUserId(users.ID);
            //        if (arrUserRoles.Count > 0)
            //        {
            //            LoadUserRoles(arrUserRoles);
            //        }
            //    }
            //}
        }

        private void LoadRoleRules(ArrayList arrRoleRules)
        {
            ArrayList arrRules = (ArrayList)Session["ListOfRules"];
            foreach (RoleRules roleRules in arrRoleRules)
            {
                int i = 0;
                foreach (Rules rules in arrRules)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                    if (rules.ID == roleRules.RuleID)
                    {
                        cb.Checked = true;
                        break;
                    }

                    i = i + 1;
                }
            }
        }

        private void LoadRules()
        {
            RulesFacade rulesFacade = new RulesFacade();
            ArrayList arrRules = rulesFacade.Retrieve();
            if (rulesFacade.Error == string.Empty)
            {
                Session["ListOfRules"] = arrRules;
                GridView1.DataSource = arrRules;
                GridView1.DataBind();
            }
        }

        private void LoadRoles()
        {
            RolesFacade roleFacade = new RolesFacade();
            ArrayList arrRole = roleFacade.Retrieve();
            if (roleFacade.Error == string.Empty)
            {
                ddlNamaRole.Items.Clear();
                ddlNamaRole.Items.Add(new ListItem("-- Pilih Roles --", "0"));
                foreach (Roles roles in arrRole)
                {
                    ddlNamaRole.Items.Add(new ListItem(roles.RolesName, roles.ID.ToString()));
                }
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            if (ddlNamaRole.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih dulu Nama Rolenya");
                return;
            }

            RoleRulesFacade roleRulesFacade = new RoleRulesFacade();
            int intResult = 0;
            RoleRules roleRules = new RoleRules();
            roleRules.RoleID = int.Parse(ddlNamaRole.SelectedValue);
            intResult = roleRulesFacade.Delete(roleRules);
            if (roleRulesFacade.Error == string.Empty)
            {
                ArrayList arrRules = (ArrayList)Session["ListOfRules"];
                int i = 0;
                foreach (Rules rules in arrRules)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                    if (cb.Checked)
                    {
                        roleRules.RuleID = rules.ID;
                        intResult = roleRulesFacade.Insert(roleRules);
                        if (roleRulesFacade.Error != string.Empty)
                        {
                            break;
                        }
                    }

                    i = i + 1;
                }

                InsertLog("Edit");
                //clearForm();
            }
        }

        private void clearForm()
        {
            LoadRoles();
            LoadRules();
        }
        protected void ddlNamaRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNamaRole.SelectedIndex > 0)
            {
                LoadRules();
                RoleRulesFacade roleRulesFacade = new RoleRulesFacade();
                ArrayList arrRoleRules = roleRulesFacade.RetrieveByRoleId(int.Parse(ddlNamaRole.SelectedValue));
                if (arrRoleRules.Count > 0)
                {
                    LoadRoleRules(arrRoleRules);
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadRules();
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Role Rules";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = ddlNamaRole.SelectedItem.Text;
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

    }
}