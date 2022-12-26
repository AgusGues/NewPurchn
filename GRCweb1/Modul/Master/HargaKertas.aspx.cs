using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Threading;
using System.Globalization;


namespace GRCweb1.Modul.Master
{
    public partial class FrmHargaKertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                clearForm();
            }
        }

        private ArrayList LoadGridHargaKertas()
        {
            ArrayList arrHargaKertas = new ArrayList();
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            //arrHargaKertas = hargaKertasFacade.Retrieve();
            arrHargaKertas = hargaKertasFacade.RetrieveByCompany(int.Parse(ddlSubCompany.SelectedValue.ToString()));
            if (arrHargaKertas.Count > 0)
            {
                return arrHargaKertas;
            }
            arrHargaKertas.Add(new HargaKertas());
            return arrHargaKertas;
        }

        private ArrayList LoadGridHargaKertasBySuplier()
        {
            ArrayList arrHargaKertas = new ArrayList();
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            arrHargaKertas = hargaKertasFacade.RetrieveByCriteria("SupplierName", txtCari.Text);
            if (arrHargaKertas.Count > 0)
            {
                return arrHargaKertas;
            }

            arrHargaKertas.Add(new HargaKertas());
            return arrHargaKertas;
        }

        private ArrayList LoadGridHargaKertasBySuplierID()
        {
            ArrayList arrHargaKertas = new ArrayList();
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            arrHargaKertas = hargaKertasFacade.RetrieveBySuppID1(Convert.ToInt32(ddlSupplier.SelectedValue), int.Parse(ddlSubCompany.SelectedValue.ToString()));
            if (arrHargaKertas.Count > 0)
            {
                return arrHargaKertas;
            }

            arrHargaKertas.Add(new HargaKertas());
            return arrHargaKertas;
        }

        private void LoadDataGridHargaKertas(ArrayList arrHargaKertas)
        {
            this.GridView1.DataSource = arrHargaKertas;
            this.GridView1.DataBind();
        }

        private void LoadSupplier(string strNaSupp)
        {
            ddlSupplier.Items.Clear();
            ArrayList arrItems = new ArrayList();

            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrItems = suppPurchFacade.RetrieveByCriteria("SupplierName", strNaSupp);

            if (suppPurchFacade.Error == string.Empty && arrItems.Count > 0)
            {
                ddlSupplier.Items.Add(new ListItem("-- Pilih Items --", "0"));

                foreach (SuppPurch suppPurch in arrItems)
                {
                    ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName + " (" + suppPurch.SupplierCode + ")", suppPurch.ID.ToString()));
                }
            }
        }

        private void LoadSupplierCode(string strNaSupp)
        {
            ddlSupplier.Items.Clear();
            ArrayList arrItems = new ArrayList();

            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrItems = suppPurchFacade.RetrieveByCriteria("Suppliercode", strNaSupp);

            if (suppPurchFacade.Error == string.Empty && arrItems.Count > 0)
            {
                foreach (SuppPurch suppPurch in arrItems)
                {
                    ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName + " (" + suppPurch.SupplierCode + ")", suppPurch.ID.ToString()));
                }
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridHargaKertas(LoadGridHargaKertasBySuplierID());
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            Session["id"] = null;
            txtCariSupplier.Text = string.Empty;
            txtHarga.Text = "0";
            txtAddPrice.Text = "0";
            txtMinPrice.Text = "0";
            txtKadarAir.Text = "0";
            txtPriceList.Text = "0";
            ddlSupplier.Items.Clear();
            txtCariNamaBrg.Text = string.Empty;
            ddlNamaBarang.Items.Clear();
            txtCariSupplier.Focus();
        }

        private void clearItem()
        {
            Session["id"] = null;
            txtHarga.Text = "0";
            txtAddPrice.Text = "0";
            txtMinPrice.Text = "0";
            txtKadarAir.Text = "0";
            txtPriceList.Text = "0";
            txtCariNamaBrg.Text = string.Empty;
            ddlNamaBarang.Items.Clear();
            txtCariNamaBrg.Focus();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            int strError = 0;
            if (ddlSupplier.Items.Count == 0 || ddlNamaBarang.Items.Count == 0)
                return;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            Users users = (Users)Session["Users"];
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            HargaKertas hrgKertas = new HargaKertas();
            hrgKertas.SubComp = int.Parse(ddlSubCompany.SelectedValue.ToString());
            hrgKertas.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            hrgKertas.ItemID = Convert.ToInt32(ddlNamaBarang.SelectedValue);
            hrgKertas.Harga = decimal.Parse(txtHarga.Text);
            hrgKertas.PriceBeli = decimal.Parse(txtPriceBeli.Text);
            hrgKertas.PriceList = decimal.Parse(txtPriceList.Text);
            hrgKertas.AddPrice = decimal.Parse(txtAddPrice.Text);
            hrgKertas.MinPrice = decimal.Parse(txtMinPrice.Text);
            hrgKertas.Kadarair = decimal.Parse(txtKadarAir.Text);
            hrgKertas.Aktif = 1;
            hrgKertas.CreatedBy = users.UserName;
            hrgKertas.LastModifiedBy = users.UserName;
            strError = hargaKertasFacade.Insert(hrgKertas);
            LoadDataGridHargaKertas(LoadGridHargaKertasBySuplierID());
            clearItem();
        }

        protected void btnHapus_ServerClick(object sender, EventArgs e)
        {
            int strError = 0;
            if (ddlSupplier.Items.Count == 0 || ddlNamaBarang.Items.Count == 0)
                return;
            Users users = (Users)Session["Users"];
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            HargaKertas hrgKertas = new HargaKertas();
            hrgKertas.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            hrgKertas.ItemID = Convert.ToInt32(ddlNamaBarang.SelectedValue);
            hrgKertas.LastModifiedBy = users.UserName;
            strError = hargaKertasFacade.Delete(hrgKertas);
            LoadDataGridHargaKertas(LoadGridHargaKertasBySuplierID());
            clearItem();
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridHargaKertas(LoadGridHargaKertasBySuplier());
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNamaBarang.SelectedIndex == 1)
            {
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inventory = inventoryFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                if (inventoryFacade.Error == string.Empty)
                {
                    if (inventory.ID > 0)
                    {

                    }
                }
            }
        }

        protected void txtCariSupplier_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier(txtCariSupplier.Text);
        }

        private void LoadItem(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrItems = inventoryFacade.RetrieveByCriteriaWithGroupID1("ItemName", strNabar, 1);
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));
            foreach (Inventory inventory in arrItems)
            {
                ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));
            }
        }

        private void LoadItemCode(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrItems = inventoryFacade.RetrieveByCriteriaWithGroupID("itemcode", strNabar, 1);
            foreach (Inventory inventory in arrItems)
            {
                ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));
            }
        }

        protected void txtCariNamaBrg_TextChanged(object sender, EventArgs e)
        {
            LoadItem(txtCariNamaBrg.Text);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataGridHargaKertas(LoadGridHargaKertas());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            try
            {
                GridViewRow row = GridView1.Rows[index];
                txtHarga.Text = row.Cells[4].Text;
                txtPriceBeli.Text = row.Cells[5].Text;
                txtPriceList.Text = row.Cells[6].Text;
                txtMinPrice.Text = row.Cells[7].Text;
                txtKadarAir.Text = row.Cells[8].Text;
                LoadItemCode(row.Cells[2].Text);
                LoadSupplierCode(row.Cells[0].Text);
            }
            catch
            {
                return;
            }
        }

        private string ValidateText()
        {
            if (ddlSupplier.Items.Count == 0 || ddlNamaBarang.Items.Count == 0)
            {
                return string.Empty;
            }
            if (Convert.ToInt32(ddlSupplier.SelectedValue) == 0)
                return "Nama Supplier tidakboleh kosong";
            if (Convert.ToInt32(ddlNamaBarang.SelectedValue) == 0)
                return "Nama Barang tidak boleh kosong";

            try
            {
                decimal dec = decimal.Parse(txtHarga.Text);
            }
            catch
            {
                return "Harga harus numeric";
            }
            try
            {
                decimal dec = decimal.Parse(txtKadarAir.Text);
            }
            catch
            {
                return "Kadar Air harus numeric";
            }
            try
            {
                decimal dec = decimal.Parse(txtAddPrice.Text);
            }
            catch
            {
                return "Add Price harus numeric";
            }
            try
            {
                decimal dec = decimal.Parse(txtMinPrice.Text);
            }
            catch
            {
                return "Min Price harus numeric";
            }
            return string.Empty;

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridHargaKertas(LoadGridHargaKertas());
        }

    }
}