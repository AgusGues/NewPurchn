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
using System.Configuration;

namespace GRCweb1.Modul.Master
{
    public partial class MasterBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDepartemen();
                LoadSupplier();
                LoadRak();
                Ljudul.Text = "INVENTORY";
                LoadGroupI("1");
                //LoadItemType();
                LoadSatuan();
                LoadDataGridInventory(LoadGridInventory());
                txNama.ReadOnly = true;
                txtUkuran.ReadOnly = true;
                txtJenis.ReadOnly = true;
                txtType.ReadOnly = true;
                txtMerk.ReadOnly = true;
                txtPartNum.ReadOnly = true;
                //ddlItemType.Enabled = false;
                ddlGroup.Enabled = false;
                txtJumlah.ReadOnly = true;
                txtReorder.Text = "0";
                txtMaxStock.Text = "0";
                clearForm();
            }
            else
            {
                //if (txtItemCode.Text != string.Empty)
                string strPrefix = txtPrefix.Text;
                if (strPrefix != string.Empty)
                {
                    HitungNoAkhir(strPrefix);
                }
            }
        }

        private void LoadDataGridInventory(ArrayList arrInventory)
        {
            this.GridView1.DataSource = arrInventory;
            this.GridView1.DataBind();
        }

        private void HitungNoAkhir(string strPrefix)
        {
            if (strPrefix.Length == 7)
            {
                string NoAkhir = string.Empty;
                int noUrut = 0;
                if (ddlItemType.SelectedValue == "1")
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    noUrut = inventoryFacade.CountItemCode(strPrefix);
                }
                if (ddlItemType.SelectedValue == "2")
                {
                    AssetFacade assetFacade = new AssetFacade();
                    noUrut = assetFacade.CountItemCode(strPrefix);
                }
                //WebReference_Krwg.Service1 webServiceKRW_Local = new WebReference_Krwg.Service1();
                //int noUrutKrw = webServiceKRW_Local.GetItemCount(strPrefix);
                //WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                //int noUrutCtrp = webServiceCtrp.GetItemCount(strPrefix);
                //if (noUrutKrw > noUrutCtrp)
                //    noUrut = noUrutKrw;
                //else
                //    noUrut = noUrutCtrp; 
                noUrut = noUrut + 1;
                NoAkhir = strPrefix + noUrut.ToString().PadLeft(4, '0');
                txtItemCode.Text = string.Empty;
                txtItemCode.Text = NoAkhir;
                txNama.Focus();
            }
        }

        private void LoadDepartemen()
        {
            ArrayList arrDepartemen = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDepartemen = deptFacade.Retrieve();
            ddlDepartemen.Items.Add(new ListItem("-- Pilih Departemen --", "0"));
            foreach (Dept dept in arrDepartemen)
            {
                ddlDepartemen.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
        }

        private void LoadSatuan()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            ddlSatuan.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }

        private void LoadGroupI(string itemtype)
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            if (itemtype == "1")
            {
                Ljudul.Text = "INVENTORY";
                LContoh.Text = "SP-BR-9 (Barang Stock)  atau  SP-BR-0 (Barang Nonstock)";
            }
            else
            {
                Ljudul.Text = "ASSET";
                LContoh.Text = "CA-GA-0  atau  CA-BM-0";
            }
            arrGroupsPurchn = groupsPurchnFacade.RetrieveByItemTypeID(itemtype);
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadGroup()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadItemType()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();
            ddlItemType.Items.Add(new ListItem("-- Pilih Item Type --", "0"));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlItemType.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }

        private void LoadSupplier()
        {
            ArrayList arrSupplier = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSupplier = suppPurchFacade.Retrieve();
            ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier --", "0"));
            foreach (SuppPurch suppPurch in arrSupplier)
            {
                ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
            }
        }

        private void LoadRak()
        {
            ArrayList arrRak = new ArrayList();
            RakFacade rakFacade = new RakFacade();
            arrRak = rakFacade.Retrieve();
            ddlRak.Items.Add(new ListItem("-- Pilih Rak --", "0"));
            foreach (Rak rak in arrRak)
            {
                ddlRak.Items.Add(new ListItem(rak.RakNo, rak.ID.ToString()));
            }
        }

        private ArrayList LoadGridInventory()
        {
            ArrayList arrInventory = new ArrayList();
            if (ddlItemType.SelectedValue == "1")
            {
                InventoryFacade inventoryFacade = new InventoryFacade();
                arrInventory = inventoryFacade.Retrieve2();
            }
            else
            {
                AssetFacade inventoryFacade = new AssetFacade();
                arrInventory = inventoryFacade.Retrieve2();
            }
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
            ddlGroup.SelectedIndex = 0;
            ddlSupplier.SelectedIndex = 0;
            ddlDepartemen.SelectedIndex = 0;
            txtJumlah.Text = "0";
            ddlSatuan.SelectedIndex = 0;
            txtHarga.Text = "0";
            txtMinStock.Text = "0";
            ddlGudang.SelectedIndex = 0;
            ddlRak.SelectedIndex = 0;
            txtShortKey.Text = "0";
            txtLeadTime.Text = "0";
            txtKeterangan.Text = "-";
            txtItemCode.Focus();
            txNama.Text = string.Empty;
            txtType.Text = string.Empty;
            txtUkuran.Text = string.Empty;
            txtJenis.Text = string.Empty;
            txtMerk.Text = string.Empty;
            txtPartNum.Text = string.Empty;
            txtAlasanNonAktif.Text = string.Empty;
            PanelNama.Visible = true;
            int deptid = ((Users)Session["Users"]).DeptID;
            int simpan = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from dept where deptname like '%purch%' or deptname like '%log%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                   if (Convert.ToInt32( sdr["ID"].ToString())==deptid)
                    { simpan = 1; }
                }
            }
            if (simpan>0 )
            {
                btnUpdate.Visible = true;
                btnNew.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnNew.Visible = false;
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            CollectName();
            string strValidate = ValidateText();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            if (txtItemName.Text == string.Empty)
            {
                lblErrorName.Visible = true;
                return;
            }

            Inventory inventory = new Inventory();
            InventoryFacade inventoryFacade = new InventoryFacade();
            Asset asset = new Asset();
            AssetFacade assetFacade = new AssetFacade();
            //inventoryFacade
            if (ViewState["id"] != null)
            {
                inventory.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            inventory.ItemCode = txtItemCode.Text;
            inventory.ItemName = txtItemName.Text;
            inventory.Jumlah = (txtJumlah.Text == string.Empty) ? 0 : decimal.Parse(txtJumlah.Text);
            inventory.Harga = int.Parse(txtHarga.Text);
            inventory.Gudang = int.Parse(ddlGudang.SelectedItem.Text);
            inventory.ShortKey = txtShortKey.Text;
            inventory.SupplierCode = ddlSupplier.SelectedItem.Text;
            inventory.DeptID = int.Parse((ddlDepartemen.SelectedValue));
            inventory.UOMID = int.Parse(ddlSatuan.SelectedValue);
            inventory.MinStock = (txtMinStock.Text == string.Empty) ? 0 : Convert.ToDecimal(txtMinStock.Text);
            inventory.RakID = int.Parse(ddlRak.SelectedValue);
            inventory.GroupID = int.Parse(ddlGroup.SelectedValue);
            inventory.ItemTypeID = int.Parse(ddlItemType.SelectedValue);
            inventory.Keterangan = txtKeterangan.Text;
            inventory.CreatedBy = ((Users)Session["Users"]).UserName;
            inventory.LastModifiedBy = ((Users)Session["Users"]).UserName;
            inventory.Nama = txNama.Text;
            inventory.Type = txtType.Text;
            inventory.Merk = txtMerk.Text;
            inventory.Ukuran = txtUkuran.Text;
            inventory.Jenis = txtJenis.Text;
            inventory.Partnum = txtPartNum.Text;
            inventory.LeadTime = int.Parse(txtLeadTime.Text);
            inventory.Alasannonaktif = txtAlasanNonAktif.Text.Trim();
            inventory.MaxStock = (txtMaxStock.Text == string.Empty) ? 0 : Convert.ToDecimal(txtMaxStock.Text);
            inventory.ReOrder = (txtReorder.Text == string.Empty) ? 0 : Convert.ToDecimal(txtReorder.Text);
            inventory.LeadTime = int.Parse(txtLeadTime.Text);

            if (RBAktif.Checked == true)
                inventory.Aktif = 1;
            else
                inventory.Aktif = 0;
            if (RBStock.Checked == true)
                inventory.Stock = 1;
            else
                inventory.Stock = 0;

            int intResult = 0;
            if (inventory.ID > 0)
            {
                if (RBNonAktif.Checked == true && txtAlasanNonAktif.Text.Trim() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan non aktif barang harus di isi");
                    return;
                }
                intResult = inventoryFacade.Update(inventory);
            }
            else
                intResult = inventoryFacade.InsertNew(inventory);
            LoadDataGridInventory(LoadGridInventory());
            clearForm();
            if (inventoryFacade.Error == string.Empty && intResult > 0)
            {
                InsertLog(strEvent);
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
                if (ddlItemType.SelectedValue == "1")
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    Inventory inventory = inventoryFacade.RetrieveByIdNew(int.Parse(row.Cells[0].Text), 1);
                    if (inventoryFacade.Error == string.Empty && inventory.ID > 0)
                    {
                        ViewState["id"] = int.Parse(row.Cells[0].Text);
                        txtItemCode.Text = inventory.ItemCode;
                        txtItemName.Text = inventory.ItemName;
                        ddlSupplier.SelectedItem.Text = inventory.SupplierCode;
                        ddlDepartemen.SelectedValue = inventory.DeptID.ToString();
                        txtJumlah.Text = Convert.ToString(inventory.Jumlah);
                        ddlSatuan.SelectedValue = inventory.UOMID.ToString();
                        //SelectUOM(row.Cells[6].Text);
                        txtHarga.Text = Convert.ToString(inventory.Harga);
                        txtMinStock.Text = Convert.ToString(inventory.MinStock);
                        ddlGudang.SelectedItem.Text = Convert.ToString(inventory.Gudang);
                        ddlRak.SelectedValue = inventory.RakID.ToString();
                        txtShortKey.Text = inventory.ShortKey;
                        txtKeterangan.Text = inventory.Keterangan;
                        ddlGroup.SelectedValue = inventory.GroupID.ToString();
                        ddlItemType.SelectedValue = inventory.ItemTypeID.ToString();
                        txNama.Text = inventory.Nama;
                        txtType.Text = inventory.Type;
                        txtMerk.Text = inventory.Merk;
                        txtUkuran.Text = inventory.Ukuran;
                        txtJenis.Text = inventory.Jenis;
                        txtPartNum.Text = inventory.Partnum;
                        txtLeadTime.Text = Convert.ToString(inventory.LeadTime);
                        txtAlasanNonAktif.Text = inventory.Alasannonaktif;
                        txtMaxStock.Text = inventory.MaxStock.ToString();
                        txtReorder.Text = inventory.ReOrder.ToString();
                        if (inventory.Aktif == 1)
                        {
                            RBAktif.Checked = true;
                            RBNonAktif.Checked = false;
                            Panel3.Visible = false;
                        }
                        else
                        {
                            RBNonAktif.Checked = true;
                            RBAktif.Checked = false;
                            Panel3.Visible = true;
                        }
                        if (inventory.Stock == 1 || inventory.Stock == 9)
                        {
                            RBStock.Checked = true;
                            RBNonStock.Checked = false;
                            Panel3.Visible = true;
                        }
                        else
                        {
                            RBNonStock.Checked = true;
                            RBStock.Checked = false;
                            Panel3.Visible = true;
                        }
                    }
                    //PanelNama.Visible = false;
                }
                else
                {
                    //AssetFacade inventoryFacade = new AssetFacade();
                    //Asset  inventory = inventoryFacade.RetrieveById(int.Parse(row.Cells[0].Text));
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    Inventory inventory = inventoryFacade.RetrieveByIdNew(int.Parse(row.Cells[0].Text), 2);
                    if (inventoryFacade.Error == string.Empty && inventory.ID > 0)
                    {
                        ViewState["id"] = int.Parse(row.Cells[0].Text);
                        txtItemCode.Text = inventory.ItemCode;
                        txtItemName.Text = inventory.ItemName;
                        ddlSupplier.SelectedItem.Text = inventory.SupplierCode;
                        ddlDepartemen.SelectedValue = inventory.DeptID.ToString();
                        txtJumlah.Text = Convert.ToString(inventory.Jumlah);
                        ddlSatuan.SelectedValue = inventory.UOMID.ToString();
                        //SelectUOM(row.Cells[6].Text);
                        txtHarga.Text = Convert.ToString(inventory.Harga);
                        txtMinStock.Text = Convert.ToString(inventory.MinStock);
                        ddlGudang.SelectedItem.Text = Convert.ToString(inventory.Gudang);
                        ddlRak.SelectedValue = inventory.RakID.ToString();
                        txtShortKey.Text = inventory.ShortKey;
                        txtKeterangan.Text = inventory.Keterangan;
                        ddlGroup.SelectedValue = inventory.GroupID.ToString();
                        ddlItemType.SelectedValue = inventory.ItemTypeID.ToString();
                        txNama.Text = inventory.Nama;
                        txtType.Text = inventory.Type;
                        txtMerk.Text = inventory.Merk;
                        txtUkuran.Text = inventory.Ukuran;
                        txtJenis.Text = inventory.Jenis;
                        txtPartNum.Text = inventory.Partnum;
                        txtLeadTime.Text = Convert.ToString(inventory.LeadTime);
                        txtAlasanNonAktif.Text = inventory.Alasannonaktif;
                        txtMaxStock.Text = inventory.MaxStock.ToString();
                        txtReorder.Text = inventory.ReOrder.ToString();
                        if (inventory.Aktif == 1)
                        {
                            RBAktif.Checked = true;
                            RBNonAktif.Checked = false;
                            Panel3.Visible = true;
                        }
                        else
                        {
                            RBNonAktif.Checked = true;
                            RBAktif.Checked = false;
                            Panel3.Visible = true;
                        }

                        if (inventory.Stock == 1 || inventory.Stock == 9)
                        {
                            RBStock.Checked = true;
                            RBNonStock.Checked = false;
                            Panel3.Visible = true;
                        }
                        else
                        {
                            RBNonStock.Checked = true;
                            RBStock.Checked = false;
                            Panel3.Visible = true;
                        }
                    }
                    //PanelNama.Visible = false;
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridInventory(LoadGridByCriteria());
        }

        private void SelectUOM(string strUOMDesc)
        {
            //ddlSatuan.ClearSelection();
            //foreach (ListItem itemUOM in ddlSatuan.Items)
            //{
            //    if (itemUOM.Text == strUOMDesc)
            //    {
            //        itemUOM.Selected = true;
            //        return;
            //    }
            //}
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridInventory(LoadGridInventory());
            else
                LoadDataGridInventory(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Inventory";
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
            string strmessage = string.Empty;
            if (txtItemName.Text == string.Empty)
                return "Nama Item tidak boleh kosong";
            else if (ddlGroup.SelectedIndex == 0)
                return "Group barang tidak boleh kosong";
            else if (ddlSatuan.SelectedIndex == 0)
                return "Satuan tidak boleh kosong";

            return strmessage;
        }

        private void CollectName()
        {
            string strtxNama = string.Empty;
            string strtxtType = string.Empty;
            string strtxtUkuran = string.Empty;
            string strtxtMerk = string.Empty;
            string strtxtJenis = string.Empty;
            string strtxtPartNum = string.Empty;
            if (txNama.Text.Trim() == string.Empty)
                return;
            else
                strtxNama = txNama.Text.Trim();
            if (txtType.Text.Trim() != string.Empty)
                strtxtType = " " + txtType.Text.Trim();
            if (txtUkuran.Text.Trim() != string.Empty)
                strtxtUkuran = " " + txtUkuran.Text.Trim();
            if (txtMerk.Text.Trim() != string.Empty)
                strtxtMerk = " Merk " + txtMerk.Text.Trim();
            if (txtJenis.Text.Trim() != string.Empty)
                strtxtJenis = " " + txtJenis.Text.Trim();
            if (txtPartNum.Text.Trim() != string.Empty)
                strtxtPartNum = "-" + txtPartNum.Text.Trim();
            txtItemName.Text = strtxNama + strtxtType + strtxtUkuran + strtxtMerk + strtxtJenis + strtxtPartNum;

        }
        protected void txNama_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void txtType_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void txtUkuran_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void txtMerk_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void txtJenis_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void txtPartNum_TextChanged(object sender, EventArgs e)
        {
            CollectName();
        }

        protected void txNama_DataBinding(object sender, EventArgs e)
        {
            CollectName();
        }
        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearForm();
            contoh.Visible = (ddlItemType.SelectedIndex == 1) ? true : false;
            ddlGroup.Enabled = (ddlItemType.SelectedIndex == 1) ? true : false;
            //txtItemCode.ReadOnly = (ddlItemType.SelectedIndex == 1) ? false : true;
            //txtPrefix.Visible = (ddlItemType.SelectedIndex == 1) ? true : false;
            LoadGroupI(ddlItemType.SelectedValue);
            LoadDataGridInventory(LoadGridInventory());
            #region aktifkan kolom nama
            txNama.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            txtUkuran.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            txtJenis.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            txtType.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            txtMerk.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            txtPartNum.ReadOnly = (ddlItemType.SelectedIndex == 0) ? true : false;
            #endregion
        }
        protected void txtPrefix_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RBAktif_CheckedChanged(object sender, EventArgs e)
        {
            if (RBAktif.Checked == true)
            {
                Panel3.Visible = false;
            }

        }
        protected void RBNonAktif_CheckedChanged(object sender, EventArgs e)
        {
            if (RBNonAktif.Checked == true)
            {
                Panel3.Visible = true;
            }
        }
        protected void RBStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RBStock.Checked == true)
            {
                Panel3.Visible = false;
            }

        }
        protected void RBNonStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RBNonStock.Checked == true)
            {
                Panel3.Visible = true;
            }
        }
    }
}