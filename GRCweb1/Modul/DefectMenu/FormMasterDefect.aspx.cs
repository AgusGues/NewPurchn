using BusinessFacade;
using DefectFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.DefectMenu
{
    public partial class FormMasterDefect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                clearForm();
                LoadDataGridMasterDefect(LoadGridMasterDefect());
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        private ArrayList LoadGridMasterDefect()
        {
            ArrayList arrMasterDefect = new ArrayList();
            MasterDefectFacade masterDefectFacade = new MasterDefectFacade();
            arrMasterDefect = masterDefectFacade.Retrieve();
            if (arrMasterDefect.Count > 0)
            {
                return arrMasterDefect;
            }

            arrMasterDefect.Add(new MasterDefect());
            return arrMasterDefect;
        }

        private void LoadDataGridMasterDefect(ArrayList arrMasterDefect)
        {
            this.GridView1.DataSource = arrMasterDefect;
            this.GridView1.DataBind();
        }
        
        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrMasterDef = new ArrayList();
            MasterDefectFacade masterDefectFacade = new MasterDefectFacade();
            if (txtSearch.Text == string.Empty)
                arrMasterDef = masterDefectFacade.Retrieve();
            else
                arrMasterDef = masterDefectFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrMasterDef.Count > 0)
            {
                return arrMasterDef;
            }

            arrMasterDef.Add(new Area());
            return arrMasterDef;
        }



        private void clearForm()
        {
            Session["id"] = null;
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;

            txtItemCode.Enabled = true;
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (txtItemCode.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Kode Defect Tidak boleh Kosong");
                return;
            }

            if (txtItemName.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nama Defect Tidak boleh Kosong");
                return;
            }

            string strEvent = "Insert";
            if (txtItemCode.Text == string.Empty)
            {
                //lblErrorName.Visible = true;
                return;
            }


            MasterDefect masterDefect = new MasterDefect();
            MasterDefectFacade masterDefectFacade = new MasterDefectFacade();

            if (ViewState["id"] != null)
            {
                masterDefect.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            masterDefect.DefCode = txtItemCode.Text;
            masterDefect.DefName = txtItemName.Text;
            masterDefect.CreatedBy = ((Users)Session["Users"]).UserName;
            masterDefect.LastModifiedBy = ((Users)Session["Users"]).UserName;

            //cek
            MasterDefect masterDef = new MasterDefect();

            masterDef = masterDefectFacade.RetrieveByCode(txtItemCode.Text);
            if (masterDefectFacade.Error != string.Empty || masterDef.ID > 0)
            {
                DisplayAJAXMessage(this, "Kode sudah ada ...!!");
                return;
            }

            //

            int intResult = 0;

            if (masterDefect.ID > 0)
            {
                intResult = masterDefectFacade.Update(masterDefect);
            }
            else
                intResult = masterDefectFacade.Insert(masterDefect);

            if (masterDefectFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridMasterDefect(LoadGridMasterDefect());
                clearForm();

                InsertLog(strEvent);

                txtItemCode.Enabled = true;
            }
        }


        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            MasterDefect defect = new MasterDefect();
            defect.ID = int.Parse(Session["id"].ToString());
            defect.LastModifiedBy = Global.UserLogin.UserName;

            string strError = string.Empty;
            MasterDefectFacade masterDefectFacade = new MasterDefectFacade();
            int intResult = 0;


            intResult = masterDefectFacade.Delete(defect);


            if (masterDefectFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridMasterDefect(LoadGridMasterDefect());
                clearForm();

                //InsertLog(strEvent);
            }


        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridMasterDefect(LoadGridByCriteria());
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Session["id"] = int.Parse(row.Cells[0].Text);
                MasterDefectFacade masterDefectFacade = new MasterDefectFacade();
                MasterDefect Defect = masterDefectFacade.RetrieveById(int.Parse(row.Cells[0].Text));
                if (masterDefectFacade.Error == string.Empty && Defect.ID > 0)
                {
                    ViewState["id"] = int.Parse(row.Cells[0].Text);
                    txtItemCode.Text = Defect.DefCode;
                    txtItemName.Text = Defect.DefName;

                    txtItemCode.Enabled = false;

                }
            }
        }
        
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Defect";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtItemCode.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }
    }
}