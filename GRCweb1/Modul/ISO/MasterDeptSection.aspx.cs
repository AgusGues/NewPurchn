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

namespace GRCweb1.Modul.ISO
{
    public partial class MasterDeptSection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                LoadBulan();
                txtTahun.Text = DateTime.Now.Year.ToString();
            }
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadDataGrid(ArrayList arrBagian)
        {
            this.GridView1.DataSource = arrBagian;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridBagian(int DeptID)
        {
            ArrayList arrDepo = new ArrayList();
            ISO_BagianFacade depoFacade = new ISO_BagianFacade();
            arrDepo = depoFacade.RetrieveByDept(DeptID);
            if (arrDepo.Count > 0)
            {
                return arrDepo;
            }

            arrDepo.Add(new Depo());
            return arrDepo;
        }

        private void LoadDept()
        {
            ArrayList arrDept = new ArrayList();
            DeptFacade propinsiFacade = new DeptFacade();
            arrDept = propinsiFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- Pilih Dept --", string.Empty));
            foreach (Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
        }


        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtISOBagian.Text = string.Empty;
            BagianID.Text = string.Empty;
            LoadDataGrid(LoadGridBagian(int.Parse(ddlDept.SelectedValue)));
        }


        private void clearForm()
        {
            ViewState["id"] = null;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            ddlDept.SelectedIndex = 0;
            userGroup.SelectedIndex = 0;
            errMsg.InnerHtml = string.Empty;
            BagianID.Text = string.Empty;
            txtISOBagian.Text = string.Empty;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            if (txtISOBagian.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Section tidak boleh kosong");
                txtISOBagian.Focus();
                return;
            }

            if (txtKPI.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "KPI tidak boleh kosong");
                txtKPI.Focus();
                return;
            }
            if (txtSOP.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "SOP tidak boleh kosong");
                txtSOP.Focus();
                return;
            }
            if (txtTask.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Task tidak boleh kosong");
                txtTask.Focus();
                return;
            }
            if (txtDisiplin.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Disiplin tidak boleh kosong");
                txtDisiplin.Focus();
                return;
            }
            /*int q1 = int.Parse(txtKPI.Text.ToString()) + int.Parse(txtSOP.Text.ToString()) + int.Parse(txtSOP.Text.ToString()) + int.Parse(txtDisiplin.Text.ToString());
            if (q1 != 100)
            {
                DisplayAJAXMessage(this, "KPI+SOP+Task+Disiplin harus 100");
                txtDisiplin.Focus();
                return;
            }*/

            string strEvent = "Insert";
            ISO_BagianFacade Bagian = new ISO_BagianFacade();
            ISO_Bagian section = new ISO_Bagian();

            section.BagianName = txtISOBagian.Text;
            section.DeptID = int.Parse(ddlDept.SelectedValue);
            section.UserGroupID = int.Parse(userGroup.SelectedValue);

            section.Plant = ((Users)Session["Users"]).UnitKerjaID;
            section.UserName = ((Users)Session["Users"]).UserName;
            section.Bulan = int.Parse(ddlBulan.SelectedValue);
            section.Tahun = int.Parse(txtTahun.Text);
            section.BobotKpi = Convert.ToDecimal(txtKPI.Text) * Convert.ToDecimal(0.01);
            section.BobotSop = Convert.ToDecimal(txtSOP.Text) * Convert.ToDecimal(0.01);
            section.BobotTask = Convert.ToDecimal(txtTask.Text) * Convert.ToDecimal(0.01);
            section.BobotDisiplin = Convert.ToDecimal(txtDisiplin.Text) * Convert.ToDecimal(0.01);

            if (BagianID.Text == string.Empty)
            {
                Bagian.Insert(section);
            }
            else
            {
                section.ID = int.Parse(BagianID.Text);
                Bagian.Update(section);
            }
            if (Bagian.Error == string.Empty)
            {
                LoadDataGrid(LoadGridBagian(int.Parse(ddlDept.SelectedValue)));
                InsertLog(strEvent);
                txtISOBagian.Text = string.Empty;
                BagianID.Text = string.Empty;
                userGroup.Text = "200";
            }
            else
            {
                errMsg.InnerHtml = Bagian.Error;
            }

        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

            ISO_BagianFacade bagian = new ISO_BagianFacade();
            ISO_Bagian section = new ISO_Bagian();
            int Id = int.Parse(BagianID.Text);
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "update ISO_Bagian set RowStatus=-1 where id=" + Id +
                ";update ISO_BobotPES set RowStatus=-1 where BagianID=" + Id;
            SqlDataReader sdr = zl.Retrieve();
            LoadDataGrid(LoadGridBagian(int.Parse(ddlDept.SelectedValue)));
            InsertLog("Delete");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                BagianID.Text = row.Cells[0].Text;
                txtISOBagian.Text = row.Cells[1].Text;
                userGroup.Text = row.Cells[3].Text;

                int Id = int.Parse(BagianID.Text.ToString());
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "" +
    "WITH kpi AS ( " +
        "SELECT Bobot*100 BobotKpi FROM ISO_BobotPES WHERE RowStatus>-1 AND PesType=1 and BagianID=" + Id +
    ") " +
    ",task AS ( " +
        "SELECT Bobot*100 BobotTask FROM ISO_BobotPES WHERE RowStatus>-1 AND PesType=2 and BagianID=" + Id +
    ") " +
    ",sop AS ( " +
        "SELECT Bobot*100 BobotSop FROM ISO_BobotPES WHERE RowStatus>-1 AND PesType=3 and BagianID=" + Id +
    ") " +
    ",disiplin AS ( " +
        "SELECT Bobot*100 BobotDisiplin FROM ISO_BobotPES WHERE RowStatus>-1 AND PesType=4 and BagianID=" + Id +
    ") " +
    "SELECT*FROM kpi,task,sop,disiplin";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        txtKPI.Text = sdr["BobotKpi"].ToString();
                        txtTask.Text = sdr["BobotTask"].ToString();
                        txtSOP.Text = sdr["BobotSop"].ToString();
                        txtDisiplin.Text = sdr["BobotDisiplin"].ToString();
                    }
                }
            }
        }



        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrDepo = new ArrayList();
            ISO_BagianFacade depoFacade = new ISO_BagianFacade();
            if (txtSearch.Text == string.Empty)
                arrDepo = depoFacade.Retrieve();
            else
                arrDepo = depoFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrDepo.Count > 0)
            {
                return arrDepo;
            }

            arrDepo.Add(new Depo());
            return arrDepo;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGrid(LoadGridBagian(int.Parse(ddlDept.SelectedValue)));
            else
                LoadDataGrid(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master ISO Bagian";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = ddlDept.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            //if (eventLogFacade.Error == string.Empty)
            // clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        private string ValidateText()
        {
            string strDepo = string.Empty;


            return string.Empty;


        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGrid(LoadGridByCriteria());
        }
        protected void txtISOBagian_TextChanged(object sender, EventArgs e)
        {
            if (txtISOBagian.Text == "Manager" || txtISOBagian.Text == "manager")
            {
                userGroup.SelectedValue = "100";
            }
            else
            {
                userGroup.SelectedValue = "200";
            }

        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {

        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {

        }
    }
}