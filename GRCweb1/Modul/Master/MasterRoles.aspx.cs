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
    public partial class MasterRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadDataGridRoles(LoadGridRoles());
                clearForm();
            }
        }

        private void LoadDataGridRoles(ArrayList arrRoles)
        {
            this.GridView1.DataSource = arrRoles;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridRoles()
        {
            ArrayList arrRoles = new ArrayList();
            RolesFacade rolesFacade = new RolesFacade();
            arrRoles = rolesFacade.Retrieve();
            if (arrRoles.Count > 0)
            {
                return arrRoles;
            }

            arrRoles.Add(new Roles());
            return arrRoles;
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtRoleName.ReadOnly = false;
            txtRoleName.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            txtRoleName.Focus();
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrRoles = new ArrayList();
            RolesFacade rolesFacade = new RolesFacade();
            if (txtSearch.Text == string.Empty)
                arrRoles = rolesFacade.Retrieve();
            else
                arrRoles = rolesFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrRoles.Count > 0)
            {
                return arrRoles;
            }

            arrRoles.Add(new Roles());
            return arrRoles;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidasi = ValidasiText();
            if (strValidasi != string.Empty)
            {
                DisplayAJAXMessage(this, strValidasi);
                return;
            }

            string strEvent = "Insert";

            Roles roles = new Roles();
            RolesFacade rolesFacade = new RolesFacade();
            if (ViewState["id"] != null)
            {
                roles.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            roles.RolesName = txtRoleName.Text;
            roles.CreatedBy = ((Users)Session["Users"]).UserName;
            int intResult = 0;
            if (roles.ID > 0)
            {
                intResult = rolesFacade.Update(roles);
            }
            else
                intResult = rolesFacade.Insert(roles);

            if (rolesFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridRoles(LoadGridRoles());
                InsertLog(strEvent);
                //clearForm();
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

            Roles roles = new Roles();
            roles.ID = int.Parse(ViewState["id"].ToString());
            roles.LastModifiedBy = ((Users)Session["Users"]).UserName;
            RolesFacade rolesFacade = new RolesFacade();
            int intResult = rolesFacade.Delete(roles);
            if (rolesFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridRoles(LoadGridRoles());
                InsertLog("Delete");
                //clearForm();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtRoleName.Text = row.Cells[1].Text;
                txtRoleName.ReadOnly = true;
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridRoles(LoadGridByCriteria());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridRoles(LoadGridRoles());
            else
                LoadDataGridRoles(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Roles";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtRoleName.Text;
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

        public string ValidasiText()
        {
            if (txtRoleName.Text == string.Empty)
                return "Nama Role tidak boleh kosong";

            if (txtRoleName.Text != string.Empty && ViewState["id"] == null)
            {
                RolesFacade rolesFacade = new RolesFacade();
                Roles roles = rolesFacade.RetrieveByCode(txtRoleName.Text);
                if (rolesFacade.Error == string.Empty)
                {
                    if (roles.ID > 0)
                    {
                        return "Nama Role sudah ada";
                    }
                }
            }

            return string.Empty;

        }


    }
}