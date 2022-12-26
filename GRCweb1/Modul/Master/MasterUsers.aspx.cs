using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Master
{
    public partial class MasterUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                txtPassword.Attributes.Add("value", string.Empty);
                LoadDataGridUsers(LoadGridUsers());
                clearForm();
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private ArrayList LoadGridUsers()
        {
            ArrayList arrUsers = new ArrayList();
            UsersFacade usersFacade = new UsersFacade();
            arrUsers = usersFacade.Retrieve();
            if (arrUsers.Count > 0)
            {
                return arrUsers;
            }

            arrUsers.Add(new Users());
            return arrUsers;
        }

        private void clearForm()
        {
            txtUserID.ReadOnly = false;
            txtUserName.ReadOnly = false;
            ViewState["id"] = null;
            txtUserID.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            txtPassword.Attributes["value"] = string.Empty;
            ddlTypeUnitKerja.SelectedIndex = 0;
            ddlUnitKerja.Items.Clear();
            txtUserID.Focus();
        }

        private void LoadDataGridUsers(ArrayList arrUsers)
        {
            this.GridView1.DataSource = arrUsers;
            this.GridView1.DataBind();
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

            Users users = new Users();
            UsersFacade usersFacade = new UsersFacade();
            if (ViewState["id"] != null)
            {
                users.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            users.UserID = txtUserID.Text;
            users.UserName = txtUserName.Text;
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            users.Password = encryptPasswordFacade.EncryptToString(txtPassword.Text);
            users.TypeUnitKerja = ddlTypeUnitKerja.SelectedIndex;
            users.UnitKerjaID = int.Parse(ddlUnitKerja.SelectedValue);
            users.CreatedBy = ((Users)Session["Users"]).UserName;
            int intResult = 0;
            if (users.ID > 0)
                intResult = usersFacade.Update(users);
            else
                intResult = usersFacade.Insert(users);
            if (usersFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridUsers(LoadGridUsers());
                //clearForm();
                InsertLog(strEvent);
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            Users users = new Users();
            UsersFacade usersFacade = new UsersFacade();

            users.ID = int.Parse(ViewState["id"].ToString());
            users.LastModifiedBy = Global.UserLogin.UserName;
            int intResult = usersFacade.Delete(users);
            if (usersFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridUsers(LoadGridUsers());
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
                txtUserID.Text = row.Cells[1].Text;
                txtUserName.Text = row.Cells[2].Text;
                txtUserID.ReadOnly = true;
                txtUserName.ReadOnly = true;

                UsersFacade userFacade = new UsersFacade();
                Users users = userFacade.RetrieveById(int.Parse(row.Cells[0].Text));
                if (userFacade.Error == string.Empty)
                {

                    //txtPassword.Text = users.Password;

                    EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();

                    txtPassword.Attributes["value"] = Request.Form[txtPassword.ClientID];
                    txtPassword.Attributes["value"] = encryptPasswordFacade.DecryptString(users.Password);
                }


                SelectTypeUnitKerja(row.Cells[3].Text);

                LoadInformation(ddlTypeUnitKerja.SelectedIndex);

                SelectUnitKerja(row.Cells[4].Text);
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridUsers(LoadGridByCriteria());
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrUsers = new ArrayList();
            UsersFacade usersFacade = new UsersFacade();
            if (txtSearch.Text == string.Empty)
                arrUsers = usersFacade.Retrieve();
            else
                arrUsers = usersFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrUsers.Count > 0)
            {
                return arrUsers;
            }

            arrUsers.Add(new Users());
            return arrUsers;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "1")
                {
                    e.Row.Cells[3].Text = "Distributor";

                    DistributorFacade distributorFacade = new DistributorFacade();
                    Distributor distributor = distributorFacade.RetrieveById(int.Parse(e.Row.Cells[4].Text));
                    if (distributorFacade.Error == string.Empty)
                    {
                        e.Row.Cells[4].Text = distributor.DistributorName;
                    }
                }
                else
                {
                    e.Row.Cells[3].Text = "Depo";

                    DepoFacade depoFacade = new DepoFacade();
                    Depo depo = depoFacade.RetrieveById(int.Parse(e.Row.Cells[4].Text));
                    if (depoFacade.Error == string.Empty)
                    {
                        e.Row.Cells[4].Text = depo.DepoName;
                    }
                }
            }
        }

        private void LoadInformation(int intTypeUnitKerja)
        {
            ddlUnitKerja.Items.Clear();

            if (intTypeUnitKerja == 0)
                return;

            ddlUnitKerja.Items.Add(new ListItem("Pilih Unit Kerja", "0"));

            if (intTypeUnitKerja == 100)
            {
                DistributorFacade distributorFacade = new DistributorFacade();
                ArrayList arrDistributor = distributorFacade.Retrieve();
                if (distributorFacade.Error == string.Empty)
                {
                    foreach (Distributor distributor in arrDistributor)
                    {
                        if (distributor.ID > 0)
                        {
                            ddlUnitKerja.Items.Add(new ListItem(distributor.DistributorName, distributor.ID.ToString()));
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
                        ddlUnitKerja.Items.Add(new ListItem(depo.DepoName, depo.ID.ToString()));
                    }
                }
            }
        }


        private void SelectUnitKerja(string strValue)
        {
            ddlUnitKerja.ClearSelection();
            foreach (ListItem item in ddlUnitKerja.Items)
            {
                if (item.Text == strValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private void SelectTypeUnitKerja(string strTypeUnitKerja)
        {
            ddlTypeUnitKerja.ClearSelection();
            foreach (ListItem item in ddlTypeUnitKerja.Items)
            {
                if (item.Text == strTypeUnitKerja)
                {
                    item.Selected = true;
                    return;
                }
            }
        }


        protected void ddlTypeUnitKerja_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInformation(ddlTypeUnitKerja.SelectedIndex);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            if (txtSearch.Text == string.Empty)
                LoadDataGridUsers(LoadGridUsers());
            else
                LoadDataGridUsers(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Users";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtUserID.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        public string ValidasiText()
        {
            if (txtUserID.Text == string.Empty)
                return "ID User tidak boleh kosong";
            else if (txtUserName.Text == string.Empty)
                return "User Name tidak boleh kosong";
            else if (txtPassword.Text == string.Empty)
                return "Password tidak boleh kosong";
            else if (ddlTypeUnitKerja.SelectedIndex == 0)
                return "Tipe Unit Kerja harus dipilih";
            else if (ddlUnitKerja.SelectedIndex == 0)
                return "Unit Kerja harus dipilih";

            UsersFacade userFacade = new UsersFacade();
            Users users = new Users();

            if (txtUserID.Text != string.Empty && ViewState["id"] == null)
            {
                users = userFacade.RetrieveByUserID(txtUserID.Text);
                if (userFacade.Error == string.Empty)
                {
                    if (users.ID > 0)
                    {
                        return "ID User sudah ada";
                    }
                }

                userFacade = new UsersFacade();
                users = userFacade.RetrieveByUserName(txtUserName.Text);
                if (userFacade.Error == string.Empty)
                {
                    if (users.ID > 0)
                    {
                        return "Nama User sudah ada";
                    }
                }
            }

            return string.Empty;
        }
    }
}