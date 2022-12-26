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

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPenawaran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNoPO.Enabled = false;
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                Users users = (Users)Session["Users"];

                LoadSupplier();

                LoadTipeSPP();

                clearForm();
                Session["id"] = null;
                //ViewState["counter"] = 0;
                //LoadPO((int)ViewState["counter"]);
                if (Request.QueryString["PONo"] != null)
                {
                    LoadPO(Request.QueryString["PONo"].ToString());
                }
            }
            else
            {
                if (txtCariSupplier.Text != string.Empty)
                {
                    getSupplier();

                    txtCariSupplier.Text = string.Empty;
                    ddlSupplier.Focus();
                }
            }
        }

        private void getSupplier()
        {
            SuppPurchFacade supplierFacade = new SuppPurchFacade();
            ArrayList arrCustomer = supplierFacade.RetrieveByCriteria("A.SupplierName", txtCariSupplier.Text);
            if (supplierFacade.Error == string.Empty)
            {
                if (arrCustomer.Count > 0)
                {

                    ddlSupplier.Items.Clear();
                    ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier Name --", "0"));

                    if (arrCustomer.Count > 0)
                    {
                        foreach (SuppPurch supp in arrCustomer)
                        {
                            ddlSupplier.Items.Add(new ListItem(supp.SupplierName, supp.ID.ToString()));
                        }
                    }
                }
            }
        }

        private void LoadPO(string strPONo)
        {
            Users users = (Users)Session["Users"];
            TawarFacade tawarFacade = new TawarFacade();
            Tawar tawar = tawarFacade.RetrieveByNo(strPONo);

            if (tawarFacade.Error == string.Empty && tawar.ID > 0)
            {
                //    Session["id"] = pOPurchn.ID;
                txtNoPO.Text = tawar.NoPO;
                //    Session["POPurchnNo"] = pOPurchn.NoPO;
                txtDate.Text = tawar.POPurchnDate.ToString("dd-MMM-yyyy");
                //ddlSupplier.SelectedIndex = tawar.SupplierID;


                SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
                SuppPurch suppPurch = suppPurchFacade.RetrieveById(tawar.SupplierID);
                if (suppPurchFacade.Error == string.Empty)
                {
                    //ddlSupplier.ClearSelection();
                    //foreach (ListItem item in ddlSupplier.Items)
                    //{
                    //    if (item.Text == suppPurch.SupplierName)
                    //    {
                    //        item.Selected = true;

                    //    }
                    //}
                    ddlSupplier.SelectedValue = suppPurch.ID.ToString();
                    txtUp.Text = suppPurch.UP;
                    txtKodeSupplier.Text = suppPurch.SupplierCode;
                    txtTelepon.Text = suppPurch.Telepon;
                    txtFax.Text = suppPurch.Fax;
                }

                //    if (pOPurchn.Status > 0)
                //    {
                //        btnUpdate.Disabled = false;
                //        btnCancel.Enabled = true;
                //        btnClose.Disabled = false;
                //    }

                //    if (pOPurchn.Status < 0)
                //    {
                //        btnUpdate.Disabled = true;
                //        btnCancel.Enabled = false;
                //        btnClose.Disabled = true;
                //    }


                TawarFacade tawarFacade2 = new TawarFacade();
                //POPurchnFacade pOPurchnFacade3 = new POPurchnFacade();
                ArrayList arrItemList = tawarFacade2.ViewGridPO(tawar.ID);
                Session["NoPO"] = strPONo;
                Session["TawarID"] = tawar.ID;
                Session["JenisTransaksi"] = "PO";
                Session["ListOfPOPurchnDetail"] = arrItemList;
                GridView1.DataSource = arrItemList;
                GridView1.DataBind();
            }

            //POPurchnFacade pOPurchnFacade2 = new POPurchnFacade();
            //POPurchn pOPurchn2 = pOPurchnFacade2.ViewPO(pOPurchn.ID);
            //txtSPP.Text = pOPurchn2.NOSPP;
            //txtNamaBarang.Text = pOPurchn2.NamaBarang;
            //txtSatuan.Text = pOPurchn2.Satuan;
            ////txtHarga.Text = pOPurchn2.Price.ToString();
            ////txtQty.Text = pOPurchn2.Qty.ToString();


            //LoadTipeSPP();
            //ddlTipeSPP.SelectedIndex = pOPurchn2.GroupID;

        }

        private void LoadDataGridTO(POPurchn pOPurchn)
        {
            LoadPO(pOPurchn.NoPO);
        }

        private void LoadSupplier()
        {
            ArrayList arrSuppPurch = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSuppPurch = suppPurchFacade.Retrieve();
            ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier --", string.Empty));
            foreach (SuppPurch suppPurch in arrSuppPurch)
            {
                ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
            }
        }

        protected void txtSPP_TextChanged(object sender, EventArgs e)
        {
            ddlItemSPP.Items.Clear();

            TextBox txl = (TextBox)sender;

            if (txl.Text != string.Empty)
            {
                if (ddlItemSPP.SelectedIndex == 0)
                {
                    DisplayAJAXMessage(this, "Kode Barang tidak boleh kosong");
                    txl.Text = string.Empty;
                    return;
                }

                DropDownList ddlName = (DropDownList)ddlItemSPP;
                LoadItem(ddlName, txl.Text);
            }
        }

        private void LoadItem(DropDownList ddlName, string strNoSPP)
        {
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            strNoSPP = txtSPP.Text;

            SPPFacade sPPFacade = new SPPFacade();
            SPP sPP = new SPP();
            sPP = sPPFacade.RetrieveAppDepoByID(depoID, strNoSPP);
            //sPP = sPPFacade.RetrieveByNo(strNoSPP);
            if (sPPFacade.Error == string.Empty)
            {
                //if (sPP.Approval == 0)
                //{
                //    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Head Dept");
                //    txtSPP.Text = string.Empty;
                //    ddlItemSPP.Items.Clear();
                //    return;
                //}
                //if (sPP.Approval == 1)
                //{
                //    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Manager Dept");
                //    txtSPP.Text = string.Empty;
                //    ddlItemSPP.Items.Clear();
                //    return;
                //}

                string strNoSPP2 = sPP.NoSPP;

                ArrayList arrSPPDetail = new ArrayList();
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                //arrSPPDetail = sPPDetailFacade.RetrieveBySPPID(sPP.ID);
                arrSPPDetail = sPPDetailFacade.RetrieveBySPPIDNoSPP(txtSPP.Text);
                if (sPPDetailFacade.Error == string.Empty)
                {
                    ddlItemSPP.Items.Add(new ListItem("-- Pilih Item SPP --", string.Empty));
                    ddlItemSPP.Items.Add(new ListItem("-- ALL --", "0"));
                    foreach (SPPDetail sPPDetail in arrSPPDetail)
                    {
                        ddlItemSPP.Items.Add(new ListItem(sPPDetail.ItemName, sPPDetail.ID.ToString()));
                    }
                }
            }
        }

        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private ArrayList LoadGridPOPurchn()
        {
            ArrayList arrPOPurchn = new ArrayList();
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            arrPOPurchn = pOPurchnFacade.Retrieve();
            if (arrPOPurchn.Count > 0)
            {
                return arrPOPurchn;
            }

            arrPOPurchn.Add(new POPurchn());
            return arrPOPurchn;
        }

        private void clearQty()
        {
            txtQty.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            //txtHarga.Text = string.Empty;
            txtNamaBarang.Text = string.Empty;
            //txtDiscount.Text = "0";
            //txtPPN.Text = "0";
            //txtPPH.Text = "0";

        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPOPurchn"] = null;
            Session["id"] = null;
            Session["POPurchnNo"] = null;
            Session["NoPO"] = null;
            Session["JenisTransaksi"] = null;
            Session["ListOfPOPurchnDetail"] = null;
            Session["TotalPrice"] = null;
            Session["QuantitySPP"] = null;
            Session["ListOfPODetail"] = null;


            txtItemCode.Text = string.Empty;
            txtNoPO.Text = string.Empty;
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtSPP.Text = string.Empty;
            ddlTipeSPP.SelectedIndex = 0;
            ddlSupplier.SelectedIndex = 0;
            txtKodeSupplier.Text = string.Empty;
            txtUp.Text = string.Empty;
            txtTelepon.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtNamaBarang.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            ArrayList arrList = new ArrayList();
            arrList.Add(new POPurchnDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();

            btnUpdate.Disabled = false;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("Baris");
            Session.Remove("id");
            Session.Remove("ListOfPOPurchn");
            Session.Remove("id");
            Session.Remove("POPurchnNo");
            Session.Remove("NoPO");
            Session.Remove("JenisTransaksi");
            Session.Remove("ListOfPOPurchnDetail");
            Session.Remove("TotalPrice");
            Session.Remove("QuantitySPP");
            Session.Remove("ListOfPODetail");
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string strValidate = ValidateText();
            decimal totalPrice = 0;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            POPurchn pOPurchn = new POPurchn();
            Tawar tawar = new Tawar();

            if (Session["id"] != null)
            {
                tawar.ID = int.Parse(Session["id"].ToString());
                strEvent = "Edit";
            }

            //if (Session["TotalPrice"] != null)
            //{
            //    totalPrice = (decimal)Session["TotalPrice"];
            //}

            tawar.NoPO = txtNoPO.Text;
            tawar.POPurchnDate = DateTime.Parse(txtDate.Text);
            if (ddlSupplier.SelectedIndex > 0)
            {
                tawar.SupplierID = int.Parse(ddlSupplier.SelectedValue);

            }
            //pOPurchn.SupplierID = int.Parse(ddlSupplier.SelectedValue);
            tawar.Keterangan = string.Empty;
            //pOPurchn.Nopo = txtTerbilang.Text;
            //pOPurchn.PPN = ((decimal.Parse(txtPPN.Text)) / 100) * totalPrice;
            //pOPurchn.Disc = (decimal.Parse(txtDiscount.Text)) / 100 * totalPrice;
            //pOPurchn.PPH = ((decimal.Parse(txtPPH.Text)) / 100) * totalPrice;
            //pOPurchn.NilaiKurs = 0;
            //pOPurchn.Cetak = 0;
            //pOPurchn.CountPrt = 0;
            //pOPurchn.Status = 0;
            //pOPurchn.Approval = 0;
            tawar.CreatedBy = users.UserName;


            int intGroupID = 0;
            if (int.Parse(ddlTipeSPP.SelectedValue) == 9 || int.Parse(ddlTipeSPP.SelectedValue) == 8)
            {
                intGroupID = 8;
                //krn utk penomoran elektrik & mekanik = KS / sparepart
            }
            //else
            intGroupID = int.Parse(ddlTipeSPP.SelectedValue);

            //CompanyFacade companyFacade = new CompanyFacade();
            //GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //string kd = companyFacade.GetKodeCompany(users.UnitKerjaID) + groupsPurchnFacade.GetKodeSPP(intGroupID);

            //SPPNumber sPPNumber = new SPPNumber();
            //SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            //sPPNumber = sPPNumberFacade.RetrieveByGroupsID(intGroupID);
            //if (sPPNumberFacade.Error == string.Empty)
            //{
            //    if (sPPNumber.ID > 0)
            //    {
            //        sPPNumber.POCounter = sPPNumber.POCounter + 1;
            //        sPPNumber.KodeCompany = kd.Substring(0, 1);
            //        sPPNumber.KodeSPP = kd.Substring(1, 1);
            //        sPPNumber.LastModifiedBy = users.UserName;
            //    }
            //}

            // pOPurchn.NoPO = txtSPP.Text.Substring(0, 2) + DateTime.Parse(txtDate.Text).Year.ToString().Substring(2, 2) + DateTime.Parse(txtDate.Text).Month.ToString().PadLeft(2, '0');

            string strError = string.Empty;
            ArrayList arrPOPurchnDetail = new ArrayList();
            if (Session["ListOfPOPurchnDetail"] != null)
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];

            TawarProcessFacade tawarProcessFacade = new TawarProcessFacade(tawar, arrPOPurchnDetail);

            if (tawar.ID > 0)
            {
                strError = tawarProcessFacade.Update();

            }
            else
            {
                strError = tawarProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtNoPO.Text = tawarProcessFacade.RepackNo;

                    Session["id"] = pOPurchn.ID;
                    //totalPrice = totalPrice - Convert.ToDecimal(pOPurchn.Disc) + pOPurchn.PPN + pOPurchn.PPH;
                    ////txtTotalPrice.Text = totalPrice.ToString();

                    //TerbilangFacade terbilangFacade = new TerbilangFacade();
                    //txtTerbilang.Text = terbilangFacade.ConvertMoneyToWords(totalPrice);

                }
            }

            if (strError == string.Empty)
            {
                InsertLog(strEvent);
                btnUpdate.Disabled = true;
            }
            LoadPO(txtNoPO.Text);
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //TransferOrder transferOrder = new TransferOrder();
            //transferOrder.ID = int.Parse(Session["id"].ToString());
            //transferOrder.LastModifiedBy = txtUsers.Text;

            //string strError = string.Empty;
            //TransferOrderProcessFacade transferOrderProsessFacade = new TransferOrderProcessFacade(transferOrder, new ArrayList());

            //strError = transferOrderProsessFacade.Delete();

            //if (strError == string.Empty)
            //{            
            //    clearForm();
            //}
        }


        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //TransferOrder transferOrder = new TransferOrder();
            //transferOrder.ID = int.Parse(Session["id"].ToString());
            //transferOrder.LastModifiedBy = txtUsers.Text;

            //string strError = string.Empty;
            //TransferOrderProcessFacade transferOrderProsessFacade = new TransferOrderProcessFacade(transferOrder, new ArrayList());

            //strError = transferOrderProsessFacade.Delete();

            //if (strError == string.Empty)
            //{            
            //    clearForm();
            //}
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // blom ada pengecekan ke spp sdh full / blom


                GridViewRow row = GridView1.Rows[index];
                // LoadAllItems();            

                // in utk apa ??????????????????????????????????????????????????????????
                SelectItem(row.Cells[1].Text);

                //txtQuantity.Text = row.Cells[2].Text;
                //ddlItemName.Enabled = false;
                lbAddItem.Text = "Update Item";
                txtQty.Focus();
            }
            else if (e.CommandName == "AddDelete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //ArrayList arrTransferDetail = new ArrayList();
                //arrTransferDetail = (ArrayList)Session["ListOfTransferDetail"];
                //arrTransferDetail.RemoveAt(index);
                //Session["ListOfTransferDetail"] = arrTransferDetail;
                //GridView1.DataSource = arrTransferDetail;
                //GridView1.DataBind();

                //decimal decTonase = 0;
                //foreach (TransferDetail transferDetail in arrTransferDetail)
                //{
                //    decTonase = decTonase + (transferDetail.Berat * transferDetail.Qty);
                //}

                //txtKubikasi.Text = decTonase.ToString();
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadPO(txtSearch.Text);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            if (ddlItemSPP.SelectedIndex == 1)
            {
                for (int i = 1; i <= ddlItemSPP.Items.Count - 2; i++)
                {
                    ddlItemSPP.SelectedIndex = i + 1;
                    loadddlitemspp();
                    lbAddItemALL();
                }
                return;
            }
            int jmlItem = 0;

            if (ViewState["Baris"] != null)
            {
                jmlItem = (int)ViewState["Baris"];
                jmlItem = jmlItem + 1;
            }
            else
            {
                jmlItem = 1;
            }
            string strValidate = ValidateItem();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ArrayList arrPOPurchnDetail = new ArrayList();
            if (Session["ListOfPOPurchnDetail"] != null)
            {
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
            }
            SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
            SPPDetail sPPDetail = new SPPDetail();
            sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddlItemSPP.SelectedValue));
            if (sPPDetailFacade.Error == string.Empty)
            {
                if (sPPDetail.Quantity - sPPDetail.QtyPO - decimal.Parse(txtQty.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Qty PO melebihi Qty SPP ..!");
                    return;
                }
                if (sPPDetail.ItemTypeID < 1 && sPPDetail.ItemTypeID > 3)
                {
                    DisplayAJAXMessage(this, "Items tersebut tidak termasuk dalam Tipe Barang");
                    return;
                }
            }
            foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
            {
                if (pOPurchnDetail.NoSPP == txtSPP.Text)
                {

                    if (pOPurchnDetail.ItemCode == txtItemCode.Text)
                    {
                        DisplayAJAXMessage(this, "No. SPP: '" + txtSPP.Text + "' Item Code: '" + txtItemCode.Text + "' sudah di entry !!");
                        return;
                    }
                }

            }

            if (lbAddItem.Text == "Add Item")
            {
                POPurchnDetail pOPurchnDetail = new POPurchnDetail();
                InventoryFacade inventoryFacade = new InventoryFacade();

                int intItemsID = 0;
                if (sPPDetail.ItemTypeID == 1)
                {
                    Inventory inv = inventoryFacade.RetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                        intItemsID = inv.ID;
                }
                if (sPPDetail.ItemTypeID == 2)
                {
                    Inventory ase = inventoryFacade.AssetRetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && ase.ID > 0)
                        intItemsID = ase.ID;
                }
                if (sPPDetail.ItemTypeID == 3)
                {
                    Inventory bia = inventoryFacade.BiayaRetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && bia.ID > 0)
                        intItemsID = bia.ID;
                }

                if (intItemsID > 0)
                {
                    pOPurchnDetail.NoSPP = txtSPP.Text;
                    pOPurchnDetail.ItemCode = txtItemCode.Text;
                    pOPurchnDetail.NamaBarang = ddlItemSPP.SelectedItem.ToString();
                    pOPurchnDetail.Qty = decimal.Parse(txtQty.Text);
                    pOPurchnDetail.ItemID = sPPDetail.ItemID;
                    pOPurchnDetail.Satuan = sPPDetail.Satuan;
                    pOPurchnDetail.GroupID = sPPDetail.GroupID;
                    pOPurchnDetail.ItemTypeID = sPPDetail.ItemTypeID;
                    pOPurchnDetail.UOMID = sPPDetail.UOMID;
                    pOPurchnDetail.SPPID = sPPDetail.SPPID;
                    string intSppDetailID = int.Parse(ddlItemSPP.SelectedValue).ToString();
                    pOPurchnDetail.SPPDetailID = int.Parse(intSppDetailID);
                    pOPurchnDetail.DocumentNo = txtSPP.Text;
                    arrPOPurchnDetail.Add(pOPurchnDetail);
                }
            }
            ViewState["Baris"] = jmlItem;
            Session["ListOfPOPurchnDetail"] = arrPOPurchnDetail;
            GridView1.DataSource = arrPOPurchnDetail;
            GridView1.DataBind();
            clearQty();
        }

        private void lbAddItemALL()
        {
            int jmlItem = 0;

            if (ViewState["Baris"] != null)
            {
                jmlItem = (int)ViewState["Baris"];
                jmlItem = jmlItem + 1;
            }
            else
            {
                jmlItem = 1;
            }
            string strValidate = ValidateItem();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ArrayList arrPOPurchnDetail = new ArrayList();
            if (Session["ListOfPOPurchnDetail"] != null)
            {
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
            }
            SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
            SPPDetail sPPDetail = new SPPDetail();
            sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddlItemSPP.SelectedValue));
            if (sPPDetailFacade.Error == string.Empty)
            {
                if (sPPDetail.Quantity - sPPDetail.QtyPO - decimal.Parse(txtQty.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Qty PO melebihi Qty SPP ..!");
                    return;
                }
                if (sPPDetail.ItemTypeID < 1 && sPPDetail.ItemTypeID > 3)
                {
                    DisplayAJAXMessage(this, "Items tersebut tidak termasuk dalam Tipe Barang");
                    return;
                }
            }
            foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
            {
                if (pOPurchnDetail.NoSPP == txtSPP.Text)
                {

                    if (pOPurchnDetail.ItemCode == txtItemCode.Text)
                    {
                        DisplayAJAXMessage(this, "No. SPP: '" + txtSPP.Text + "' Item Code: '" + txtItemCode.Text + "' sudah di entry !!");
                        return;
                    }
                }

            }

            if (lbAddItem.Text == "Add Item")
            {
                POPurchnDetail pOPurchnDetail = new POPurchnDetail();
                InventoryFacade inventoryFacade = new InventoryFacade();

                int intItemsID = 0;
                if (sPPDetail.ItemTypeID == 1)
                {
                    Inventory inv = inventoryFacade.RetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                        intItemsID = inv.ID;
                }
                if (sPPDetail.ItemTypeID == 2)
                {
                    Inventory ase = inventoryFacade.AssetRetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && ase.ID > 0)
                        intItemsID = ase.ID;
                }
                if (sPPDetail.ItemTypeID == 3)
                {
                    Inventory bia = inventoryFacade.BiayaRetrieveById(sPPDetail.ItemID);
                    if (inventoryFacade.Error == string.Empty && bia.ID > 0)
                        intItemsID = bia.ID;
                }

                if (intItemsID > 0)
                {
                    pOPurchnDetail.NoSPP = txtSPP.Text;
                    pOPurchnDetail.ItemCode = txtItemCode.Text;
                    pOPurchnDetail.NamaBarang = ddlItemSPP.SelectedItem.ToString();
                    pOPurchnDetail.Qty = decimal.Parse(txtQty.Text);
                    pOPurchnDetail.ItemID = sPPDetail.ItemID;
                    pOPurchnDetail.Satuan = sPPDetail.Satuan;
                    pOPurchnDetail.GroupID = sPPDetail.GroupID;
                    pOPurchnDetail.ItemTypeID = sPPDetail.ItemTypeID;
                    pOPurchnDetail.UOMID = sPPDetail.UOMID;
                    pOPurchnDetail.SPPID = sPPDetail.SPPID;
                    string intSppDetailID = int.Parse(ddlItemSPP.SelectedValue).ToString();
                    pOPurchnDetail.SPPDetailID = int.Parse(intSppDetailID);
                    pOPurchnDetail.DocumentNo = txtSPP.Text;
                    arrPOPurchnDetail.Add(pOPurchnDetail);
                }
            }
            ViewState["Baris"] = jmlItem;
            Session["ListOfPOPurchnDetail"] = arrPOPurchnDetail;
            GridView1.DataSource = arrPOPurchnDetail;
            GridView1.DataBind();
        }
        private void loadddlitemspp()
        {
            //DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];

            if (ddlItemSPP.SelectedIndex > 0)
            {
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                SPPDetail sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddlItemSPP.SelectedValue));
                if (sPPDetailFacade.Error == string.Empty)
                {
                    if (sPPDetail.ID > 0)
                    {
                        ddlTipeSPP.SelectedIndex = sPPDetail.GroupID;

                        txtNamaBarang.Text = sPPDetail.ItemName;
                        //sehingga qty sisa spp yg terambil
                        txtQty.Text = (sPPDetail.Quantity - sPPDetail.QtyPO).ToString("N2");

                        Session["QuantitySPP"] = sPPDetail.Quantity - sPPDetail.QtyPO;
                        txtSatuan.Text = sPPDetail.Satuan;
                        txtItemCode.Text = sPPDetail.ItemCode;

                        // SelectTipeSPP(sPPDetail.GroupID);
                        if (sPPDetail.Quantity - sPPDetail.QtyPO <= 0)
                        {
                            DisplayAJAXMessage(this, "Qty <= Nol ..!");
                            return;
                        }
                    }
                }
            }
        }
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];

            if (ddl.SelectedIndex > 0)
            {
                SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
                SuppPurch suppPurch = suppPurchFacade.RetrieveById(int.Parse(ddl.SelectedValue));
                if (suppPurchFacade.Error == string.Empty)
                {
                    if (suppPurch.ID > 0)
                    {
                        txtKodeSupplier.Text = suppPurch.SupplierCode;
                        txtUp.Text = suppPurch.UP;
                        txtTelepon.Text = suppPurch.Telepon;
                        txtFax.Text = suppPurch.Fax;
                    }
                }
            }
        }

        protected void ddlItemSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];

            if (ddl.SelectedIndex > 0)
            {
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                SPPDetail sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddl.SelectedValue));
                if (sPPDetailFacade.Error == string.Empty)
                {
                    if (sPPDetail.ID > 0)
                    {
                        ddlTipeSPP.SelectedIndex = sPPDetail.GroupID;

                        txtNamaBarang.Text = sPPDetail.ItemName;
                        //sehingga qty sisa spp yg terambil
                        txtQty.Text = (sPPDetail.Quantity - sPPDetail.QtyPO).ToString("N2");

                        Session["QuantitySPP"] = sPPDetail.Quantity - sPPDetail.QtyPO;
                        txtSatuan.Text = sPPDetail.Satuan;
                        txtItemCode.Text = sPPDetail.ItemCode;

                        // SelectTipeSPP(sPPDetail.GroupID);
                        if (sPPDetail.Quantity - sPPDetail.QtyPO <= 0)
                        {
                            DisplayAJAXMessage(this, "Qty <= Nol ..!");
                            return;
                        }
                    }
                }
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != string.Empty)
            {
                //int xQtySPP = Convert.ToInt32(Session["QuantitySPP"]);
                //int xText = int.Parse(txtQty.Text);

                decimal xQtySPP = Convert.ToDecimal(Session["QuantitySPP"]);
                decimal xText = decimal.Parse(txtQty.Text);

                if (xText > xQtySPP)
                {
                    txtQty.Text = xQtySPP.ToString();

                }
            }

        }

        protected void showMessageBox(string message)
        {
            string sScript;
            message = message.Replace("'", "\'");
            sScript = String.Format("alert('{0}');", message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        }

        private void SelectItem(string strItemName)
        {
            //ddlItemName.ClearSelection();
            //foreach (ListItem item in ddlItemName.Items)
            //{
            //    if (item.Text == strItemName)
            //    {
            //        item.Selected = true;
            //        return;
            //    }
            //}
        }

        private void SelectTipeSPP(int groupPurchn)
        {

            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            GroupsPurchn groupsPurchn = groupsPurchnFacade.RetrieveById(groupPurchn);
            if (groupsPurchnFacade.Error == string.Empty)
            {

                ddlTipeSPP.ClearSelection();
                foreach (ListItem item in ddlTipeSPP.Items)
                {
                    if (item.Text == groupsPurchn.GroupDescription)
                    {
                        item.Selected = true;
                        return;
                    }
                }
            }
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {

            Session["ListOfPODetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("ListPenawaran.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }


        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input PO";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtNoPO.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            //if (ddlFromDepo.SelectedIndex == 0)
            //    return "Pilih Dari Depo";
            //else if (ddlToDepo.SelectedIndex == 0)
            //    return "Pilih Ke Depo";

            //ArrayList arrTransferDetail = new ArrayList();
            //if (Session["ListOfTransferDetail"] != null)
            //{
            //    arrTransferDetail = (ArrayList)Session["ListOfTransferDetail"];
            //    if (arrTransferDetail.Count == 0)
            //        return "Item Barang tidak ada yang di transfer";
            //}
            if (ddlSupplier.SelectedIndex <= 0)
            {
                return "Supplier tidak boleh kosong";
            }
            else
                return string.Empty;
        }

        private string ValidateItem()
        {
            //if (ddlSupplier.SelectedIndex == 0)
            //    return "Pilih Nama Supplier";
            //else 
            if (ddlItemSPP.SelectedIndex == 0)
                return "Pilih Item Barang dari SPP yang bersangkutan";
            else if (txtSPP.Text == string.Empty)
                return "Isi No. SPP";


            //try
            //{
            //    decimal dec = decimal.Parse(txtHarga.Text);
            //}
            //catch
            //{
            //    return "Harga harus numeric";
            //}

            try
            {
                decimal dec = decimal.Parse(txtQty.Text);
            }
            catch
            {
                return "Quantity harus numeric";
            }

            if (decimal.Parse(txtQty.Text) <= 0)
                return "Quantiry harus lebih dari Nol";


            return string.Empty;
        }
    }
}