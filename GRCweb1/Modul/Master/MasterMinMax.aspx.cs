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
    public partial class MasterMinMax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                //LoadDataGridInventory(LoadGridInventory());

                clearForm();

            }
            else
            {
                string strPrefix = txtItemName.Text;
                if (strPrefix != string.Empty)
                {
                    //load itemname
                    LoadItem(strPrefix);
                }

                txtItemName.Text = string.Empty;
            }

        }

        private void LoadItem(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();

            InventoryFacade inventoryFacade = new InventoryFacade();
            arrItems = inventoryFacade.RetrieveByCriteria("ItemName", strNabar);

            if (inventoryFacade.Error == string.Empty && arrItems.Count > 0)
            {
                ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));

                foreach (Inventory inventory in arrItems)
                {
                    ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));
                }
            }
        }

        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlNamaBarang.SelectedIndex > 0)
            {
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inventory = inventoryFacade.RetrieveById2(int.Parse(ddlNamaBarang.SelectedValue));
                if (inventoryFacade.Error == string.Empty)
                {
                    if (inventory.ID > 0)
                    {
                        txtItemCode.Text = inventory.ItemCode;
                        txtMaxStock.Text = inventory.MaxStock.ToString("N2");
                        txtMinStock.Text = inventory.MinStock.ToString("N2");
                        txtReOrder.Text = inventory.ReOrder.ToString("N2");
                        txtUomCode.Text = inventory.UomCode;
                    }
                }

                txtMinStock.Focus();
            }
        }


        private void LoadDataGridInventory(ArrayList arrInventory)
        {
            this.GridView1.DataSource = arrInventory;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridInventory()
        {
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.Retrieve2();
            if (arrInventory.Count > 0)
            {
                return arrInventory;
            }

            arrInventory.Add(new Inventory());
            return arrInventory;
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            if (txtSearch.Text == string.Empty)
                arrInventory = inventoryFacade.Retrieve();
            else
                arrInventory = inventoryFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrInventory.Count > 0)
            {
                return arrInventory;
            }

            arrInventory.Add(new Inventory());
            return arrInventory;
        }

        private void clearForm()
        {
            Session["id"] = null;
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtUomCode.Text = string.Empty;
            txtMinStock.Text = string.Empty;
            txtMaxStock.Text = string.Empty;
            txtReOrder.Text = string.Empty;
            ddlNamaBarang.Items.Clear();

            txtItemName.Focus();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            Inventory inventory = new Inventory();
            InventoryFacade inventoryFacade = new InventoryFacade();
            if (ViewState["id"] != null)
            {
                inventory.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            inventory.ID = int.Parse(ddlNamaBarang.SelectedValue);
            inventory.MinStock = decimal.Parse(txtMinStock.Text);
            inventory.MaxStock = decimal.Parse(txtMaxStock.Text);
            inventory.ReOrder = decimal.Parse(txtReOrder.Text);
            //change domain ke decimal

            int intResult = 0;
            if (inventory.ID > 0)
            {
                intResult = inventoryFacade.UpdateMinMax(inventory);
                if (inventoryFacade.Error == string.Empty && intResult > 0)
                {
                    //LoadDataGridInventory(LoadGridInventory());
                    clearForm();

                    InsertLog(strEvent);
                }
            }
        }



        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //Items items = new Items();
            //items.ID = int.Parse(Session["id"].ToString());
            //items.LastModifiedBy = Global.UserLogin.UserName;

            //string strError = string.Empty;        
            //ItemsProcessFacade itemProsessFacade = new ItemsProcessFacade(items, new ArrayList());

            //strError = itemProsessFacade.Delete();

            //if (strError == string.Empty)
            //{
            //    LoadDataGridItems(LoadGridItems());
            //    //clearForm();
            //    InsertLog("Delete");
            //}
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Session["id"] = int.Parse(row.Cells[0].Text);
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inventory = inventoryFacade.RetrieveById(int.Parse(row.Cells[0].Text));
                if (inventoryFacade.Error == string.Empty && inventory.ID > 0)
                {
                    ViewState["id"] = int.Parse(row.Cells[0].Text);
                    txtItemCode.Text = inventory.ItemCode;
                    txtItemName.Text = inventory.ItemName;
                    txtMinStock.Text = Convert.ToString(inventory.MinStock);

                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //LoadDataGridInventory(LoadGridByCriteria());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //if (txtSearch.Text == string.Empty)
            //LoadDataGridInventory(LoadGridInventory());
            //else
            //LoadDataGridInventory(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Min-Max";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtItemCode.Text;
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


        private string ValidateText()
        {
            //if (txtItemName.Text == string.Empty)
            //    return "Nama Item tidak boleh kosong";
            if (ddlNamaBarang.SelectedIndex == 0)
                return "Nama Item tidak boleh kosong";


            return string.Empty;
        }
    }
}