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
    public partial class MasterRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadDataGridRules(LoadGridRules());
                clearForm();
            }
        }

        private void LoadDataGridRules(ArrayList arrRules)
        {
            this.GridView1.DataSource = arrRules;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridRules()
        {
            ArrayList arrRules = new ArrayList();
            RulesFacade rulesFacade = new RulesFacade();
            arrRules = rulesFacade.Retrieve();
            if (arrRules.Count > 0)
            {
                return arrRules;
            }

            arrRules.Add(new Rules());
            return arrRules;
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtRuleName.ReadOnly = false;
            txtRuleName.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            txtRuleName.Focus();
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrRules = new ArrayList();
            RulesFacade rulesFacade = new RulesFacade();
            if (txtSearch.Text == string.Empty)
                arrRules = rulesFacade.Retrieve();
            else
                arrRules = rulesFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrRules.Count > 0)
            {
                return arrRules;
            }

            arrRules.Add(new Rules());
            return arrRules;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidasiText();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            if (txtRuleName.Text == string.Empty)
                return;

            Rules rules = new Rules();
            RulesFacade rulesFacade = new RulesFacade();
            if (ViewState["id"] != null)
            {
                rules.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            rules.RuleName = txtRuleName.Text;
            rules.CreatedBy = ((Users)Session["Users"]).UserName;
            int intResult = 0;
            if (rules.ID > 0)
            {
                intResult = rulesFacade.Update(rules);
            }
            else
                intResult = rulesFacade.Insert(rules);

            if (rulesFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridRules(LoadGridRules());
                InsertLog(strEvent);
                //clearForm();
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            if (txtRuleName.Text == string.Empty)
                return;


            Rules rules = new Rules();
            rules.ID = int.Parse(ViewState["id"].ToString());
            rules.LastModifiedBy = Global.UserLogin.UserName;
            RulesFacade rulesFacade = new RulesFacade();
            int intResult = rulesFacade.Delete(rules);
            if (rulesFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridRules(LoadGridRules());
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
                txtRuleName.Text = row.Cells[1].Text;
                txtRuleName.ReadOnly = true;
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridRules(LoadGridByCriteria());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridRules(LoadGridRules());
            else
                LoadDataGridRules(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Rules";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtRuleName.Text;
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
            if (txtRuleName.Text == string.Empty)
            {
                return "Nama Rule tidak boleh kosong";
            }

            if (txtRuleName.Text != string.Empty && ViewState["id"] == null)
            {
                RulesFacade rulesFacade = new RulesFacade();
                Rules rules = rulesFacade.RetrieveByCode(txtRuleName.Text);
                if (rulesFacade.Error == string.Empty)
                {
                    if (rules.ID > 0)
                    {
                        return "Nama Rule sudah ada";
                    }
                }
            }

            return string.Empty;
        }


    }
}