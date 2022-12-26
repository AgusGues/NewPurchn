using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Domain;
using BusinessFacade;
using System.Collections;

using DataAccessLayer;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;


namespace GRCweb1.Modul.Purchasing
{
    public partial class FormAdjustment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                if (Request.QueryString["AdjustNo"] != null)
                {
                    clearForm();
                    LoadTipeBarang();
                    LoadAdjust(Request.QueryString["AdjustNo"].ToString());
                }
                else
                {
                    clearForm();
                    LoadTipeBarang();
                }
                //btnCancel.Attributes.Add("onclick", "return confirm_delete();");
            }
            else
            {
                if (txtCariNamaBrg.Text != string.Empty && (rbKurang.Checked == true || rbTambah.Checked == true || rbDisposal.Checked == true))
                {
                    LoadItems();
                    txtCariNamaBrg.Text = string.Empty;
                }
            }

        }

        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();
            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfAdjustDetail"] = new ArrayList();
            //Session["Pakai"] = null;
            Session["AdjustNo"] = null;

            RBKomponen.Checked = false;

            rbKurang.Checked = false;
            rbKurang.Checked = false;
            txtPakaiNo.Text = string.Empty;
            if (txtTanggal.Text == string.Empty)
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            txtUom.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtCariNamaBrg.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            //txtKeterangan.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtUomID.Text = string.Empty;
            rbTambah.Checked = false;
            rbKurang.Checked = false;
            txtGroupID.Text = string.Empty;
            ddlTipeBarang.Enabled = true;
            ddlTipeBarang.Items.Clear();
            ddlItemName.Items.Clear();

            ArrayList arrList = new ArrayList();
            arrList.Add(new AdjustDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            lbAddOP.Enabled = true;

            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadItems()
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            Users users = (Users)Session["Users"];
            string uk = string.Empty;

            //if (users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            //{
            //    if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
            //    {
            //        uk = "4";
            //    }
            //}
            //else if (users.UnitKerjaID == 7)
            //{
            //    if (int.Parse(ddlTipeBarang.SelectedValue) == 5)
            //    {
            //        uk = "4";
            //    }
            //}  
            //int GroupAsset = 0;
            //if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
            //{
            //    if (RBTunggal.Checked == true)
            //    {
            //        GroupAsset = 4;
            //    }
            //    else if (RBKomponen.Checked == true)
            //    {
            //        GroupAsset = 12;
            //    }
            //}

            if (int.Parse(ddlTipeBarang.SelectedValue) == 1)
            {
                arrInventory = inventoryFacade.RetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            }
            if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
            {
                if (RBTunggal.Checked == true)
                {
                    arrInventory = inventoryFacade.AssetRetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                    ddlItemName.Items.Add(new ListItem("-- Pilih Asset --", "0"));
                }
                else if (RBKomponen.Checked == true)
                {
                    arrInventory = inventoryFacade.BiayaRetrieveByCriteriaAssetKomponen("A.ItemName", txtCariNamaBrg.Text);
                    ddlItemName.Items.Add(new ListItem("-- Pilih Asset BerKomponen --", "0"));
                }
            }
            if (int.Parse(ddlTipeBarang.SelectedValue) == 3)
            {
                arrInventory = inventoryFacade.BiayaRetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                ddlItemName.Items.Add(new ListItem("-- Pilih Biaya --", "0"));
            }
            //if (int.Parse(ddlTipeBarang.SelectedValue) == 4 || int.Parse(ddlTipeBarang.SelectedValue) == 5)
            //if (uk == "4")
            //{
            //    arrInventory = inventoryFacade.BiayaRetrieveByCriteriaAssetKomponen("A.ItemName", txtCariNamaBrg.Text);
            //    ddlItemName.Items.Add(new ListItem("-- Pilih Asset BerKomponen --", "0"));
            //}

            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                string DetailID = row.Cells[8].Text;
                ArrayList arrListAdjustDetail = new ArrayList();
                arrListAdjustDetail = (ArrayList)Session["ListOfAdjustDetail"];
                AdjustDetail objAdjust = (AdjustDetail)arrListAdjustDetail[index];
                if (objAdjust.Apv == 0)
                {
                    /**
                     * lakukan edit row status
                     */
                    ArrayList adjDetailDel = new ArrayList();
                    AdjustDetail objAdj = new AdjustDetail();
                    objAdj.ID = objAdjust.ID;
                    adjDetailDel.Add(objAdj);

                    AdjustProcessFacade adjustFacade = new AdjustProcessFacade(new Adjust(), adjDetailDel);
                    string strError = adjustFacade.Delete();
                    if (strError == string.Empty)
                    {
                        arrListAdjustDetail.RemoveAt(index);
                        Session["ListOfAdjustDetail"] = arrListAdjustDetail;
                        GridView1.DataSource = arrListAdjustDetail;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, "Tidak bisa di hapus sudah di approved accounting");
                    return;
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd-MMM-yyyy");

                e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("N2");
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListAdjustment.aspx?approve=AD");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {

            #region validasi data
            try
            {
                decimal cekNumber = decimal.Parse(txtQtyPakai.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Pakai harus number");
                return;
            }
            if (decimal.Parse(txtQtyPakai.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Terima tidak boleh 0");
                return;
            }

            try
            {
                decimal cekNumber = decimal.Parse(txtStok.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Stok harus number");
                return;
            }
            if (rbKurang.Checked == true && rbNonStok.Checked == false)
            {
                if (decimal.Parse(txtStok.Text) == 0)
                {
                    DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                    return;
                }

                if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text))
                {
                    DisplayAJAXMessage(this, "Quantity Pakai lebih besar dari pada Stok");
                    return;
                }
            }
            #endregion
            #region validasi closing periode
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close tidak bisa untuk transaksi";
                DisplayAJAXMessage(this, mess);
                return;
            }
            #endregion
            AdjustDetail adjustDetail = new AdjustDetail();
            ArrayList arrListAdjustDetail = new ArrayList();

            if (Session["ListOfAdjustDetail"] != null)
            {
                arrListAdjustDetail = (ArrayList)Session["ListOfAdjustDetail"];
                if (arrListAdjustDetail.Count > 0)
                {
                    int ada = 0;
                    foreach (AdjustDetail adj in arrListAdjustDetail)
                    {
                        if (adj.ItemCode == txtItemCode.Text)
                        {
                            DisplayAJAXMessage(this, "Sudah ada di-tabel untuk Barang tersebut");

                            //clearQty();
                            //return;
                        }

                        ada = ada + 1;
                    }
                }
            }

            adjustDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            adjustDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            adjustDetail.RowStatus = 0;
            adjustDetail.Keterangan = txtKeterangan.Text;
            adjustDetail.UomID = int.Parse(txtUomID.Text);
            adjustDetail.ItemCode = txtItemCode.Text;
            adjustDetail.ItemName = ddlItemName.SelectedItem.ToString();
            adjustDetail.UOMCode = txtUom.Text;
            if (rbTambah.Checked == true) adjustDetail.AdjustType = "Tambah";
            if (rbKurang.Checked == true) adjustDetail.AdjustType = "Kurang";
            if (rbDisposal.Checked == true) adjustDetail.AdjustType = "Disposal";
            //adjustDetail.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
            if (ddlTipeBarang.SelectedItem.ToString() == "ASSET BERKOMPONEN")
            {
                adjustDetail.ItemTypeID = 4;
            }
            else
            {
                adjustDetail.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
            }
            //adjustDetail.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
            adjustDetail.GroupID = int.Parse(txtGroupID.Text);

            arrListAdjustDetail.Add(adjustDetail);
            Session["ListOfAdjustDetail"] = arrListAdjustDetail;

            GridView1.DataSource = arrListAdjustDetail;
            GridView1.DataBind();

            rbKurang.Enabled = false;
            rbTambah.Enabled = false;
            ddlTipeBarang.Enabled = false;
            clearQty();

        }

        private void clearQty()
        {
            txtItemCode.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtUom.Text = string.Empty;
            //txtKeterangan.Text = string.Empty;
            txtUomID.Text = string.Empty;
            txtGroupID.Text = string.Empty;

            ddlItemName.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            rbKurang.Enabled = true;
            rbTambah.Enabled = true;
            tr1.Visible = false; tr2.Visible = false;

            clearForm();
            LoadTipeBarang();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int bln = DateTime.Parse(txtTanggal.Text).Month;
            int thn = DateTime.Parse(txtTanggal.Text).Year;

            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";
            Adjust adjust = new Adjust();

            if (Session["id"] != null)
            {
                strEvent = "Edit";
            }

            adjust.AdjustNo = txtPakaiNo.Text;
            adjust.AdjustDate = DateTime.Parse(txtTanggal.Text);
            if (rbNonStok.Checked == true) adjust.NonStok = 1;
            if (rbTambah.Checked == true) adjust.AdjustType = "Tambah";
            if (rbKurang.Checked == true) adjust.AdjustType = "Kurang";
            if (rbDisposal.Checked == true) adjust.AdjustType = "Disposal";
            adjust.Status = 0;
            adjust.AlasanCancel = "";
            adjust.CreatedBy = ((Users)Session["Users"]).UserName;
            adjust.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);

            string strError = string.Empty;
            ArrayList arrAdjustDetail = new ArrayList();
            if (Session["ListOfAdjustDetail"] != null)
            {
                arrAdjustDetail = (ArrayList)Session["ListOfAdjustDetail"];
                //cek stok ada gak 1x lagi
                if (rbKurang.Checked == true)
                {
                    foreach (AdjustDetail adjDetail in arrAdjustDetail)
                    {
                        Inventory inv = new Inventory();
                        InventoryFacade invFacade = new InventoryFacade();
                        if (int.Parse(ddlTipeBarang.SelectedValue) == 1)
                            inv = invFacade.RetrieveById(adjDetail.ItemID);
                        if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
                            inv = invFacade.AssetRetrieveById(adjDetail.ItemID);
                        if (int.Parse(ddlTipeBarang.SelectedValue) == 3)
                            inv = invFacade.BiayaRetrieveById(adjDetail.ItemID);

                        if (invFacade.Error == string.Empty && inv.ID > 0)
                        {
                            if (rbNonStok.Checked == false && inv.Jumlah - adjDetail.Quantity < 0)
                            {
                                string strItemCode = inv.ItemCode;
                                string strMessage = "Kode Barang " + strItemCode + " tidak mencukupi stok-nya...!";
                                DisplayAJAXMessage(this, strMessage);

                                clearQty();
                                return;
                            }
                        }
                    }
                }
            }
            // until here

            AdjustProcessFacade pakaiProcessFacade = new AdjustProcessFacade(adjust, arrAdjustDetail);
            if (adjust.ID > 0)
            {
                //ScheduleFacade scheduleFacade = new ScheduleFacade();
                //Schedule sc = scheduleFacade.RetrieveByNo(txtPONo.Text);
                //if (scheduleFacade.Error == string.Empty)
                //{
                //    if (sc.ID > 0)
                //    {
                //        receipt.Status = sc.Status;
                //        strError = receiptProcessFacade.Update();
                //        // blom di cek
                //    }
                //}
            }
            else
            {
                strError = pakaiProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtPakaiNo.Text = pakaiProcessFacade.AdjustNo;
                    Session["id"] = adjust.ID;
                    Session["AdjustNo"] = pakaiProcessFacade.AdjustNo;
                }
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                //txtCariOP.ReadOnly = false;

                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                if (strEvent == "Edit")
                    clearForm();

            }
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Adjustment Barang";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtPakaiNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string uk = string.Empty; decimal StockAkhir = 0; int GroupAsset = 0;

            //if (users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            //{
            if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
            {
                if (RBTunggal.Checked == true)
                {
                    GroupAsset = 4;
                }
                else if (RBKomponen.Checked == true)
                {
                    GroupAsset = 12;
                }
            }
            //}
            //else if (users.UnitKerjaID == 7)
            //{
            //    if (int.Parse(ddlTipeBarang.SelectedValue) == 5)
            //    {
            //        uk = "4";
            //    }
            //}        

            if (ddlItemName.SelectedIndex > 0)
            {
                //nanti utk biaya & asset agak beda kayaknya
                Inventory inv = new Inventory();
                InventoryFacade invFacade = new InventoryFacade();


                if (int.Parse(ddlTipeBarang.SelectedValue) == 1)
                {
                    inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));

                    Inventory inv2 = new Inventory();
                    InventoryFacade invFacade2 = new InventoryFacade();
                    //decimal PendingSPB = invFacade2.GetPendingSPB(ddlItemName.SelectedValue, 1);
                    StockAkhir = invFacade2.GetStockAkhir(ddlItemName.SelectedValue, 1, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());
                }
                else if (int.Parse(ddlTipeBarang.SelectedValue) == 2)
                {
                    inv = invFacade.AssetRetrieveById(int.Parse(ddlItemName.SelectedValue));
                    Inventory inv3 = new Inventory();
                    InventoryFacade invFacade3 = new InventoryFacade();
                    //decimal PendingSPB = invFacade2.GetPendingSPB(ddlItemName.SelectedValue, 1);
                    StockAkhir = invFacade3.GetStockAkhirAsset(ddlItemName.SelectedValue, GroupAsset, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());

                }
                else if (int.Parse(ddlTipeBarang.SelectedValue) == 4)
                {
                    inv = invFacade.BiayaRetrieveById(int.Parse(ddlItemName.SelectedValue));
                    //if (int.Parse(ddlTipeBarang.SelectedValue) == 4)
                    if (uk == "4")
                        inv = invFacade.RetrieveByIdAssetBerKomponen(int.Parse(ddlItemName.SelectedValue));

                    Inventory inv4 = new Inventory();
                    InventoryFacade invFacade4 = new InventoryFacade();
                    //decimal PendingSPB = invFacade2.GetPendingSPB(ddlItemName.SelectedValue, 1);
                    StockAkhir = invFacade4.GetStockAkhirAsset(ddlItemName.SelectedValue, 12, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());
                }

                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    //txtStok.Text = inv.Jumlah.ToString("N2");
                    txtStok.Text = (StockAkhir).ToString("N2");
                    txtGroupID.Text = inv.GroupID.ToString();

                    txtQtyPakai.Focus();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadAdjust(txtSearch.Text);
        }

        private void LoadAdjust(string strAdjustNo)
        {
            // clearForm();

            AdjustFacade adjustFacade = new AdjustFacade();
            Adjust adjust = adjustFacade.RetrieveByNo(strAdjustNo);
            if (adjustFacade.Error == string.Empty && adjust.ID > 0)
            {
                Session["id"] = adjust.ID;

                txtPakaiNo.Text = adjust.AdjustNo;
                txtTanggal.Text = adjust.AdjustDate.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = adjust.CreatedBy;
                rbTambah.Checked = (adjust.AdjustType == "tambah") ? true : false;
                rbKurang.Checked = (adjust.AdjustType == "kurang") ? true : false;
                rbDisposal.Checked = (adjust.AdjustType == "Disposal") ? true : false;
                rbDisposal.Visible = (adjust.AdjustType == "Disposal") ? true : false;
                ddlTipeBarang.SelectedValue = adjust.ItemTypeID.ToString();

                ArrayList arrListAdjustDetail = new ArrayList();
                AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
                ArrayList arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustId(adjust.ID);
                if (adjustDetailFacade.Error == string.Empty)
                {
                    foreach (AdjustDetail adjDetail in arrAdjustDetail)
                    {
                        if (adjDetail.ID > 0)
                            arrListAdjustDetail.Add(adjDetail);
                    }
                }
                Session["AdjustNo"] = adjust.AdjustNo;
                Session["ListOfAdjustDetail"] = arrListAdjustDetail;
                GridView1.DataSource = arrListAdjustDetail;
                GridView1.DataBind();

                if (adjust.Status == -1)
                {
                    btnCancel.Enabled = false;
                }
                else if (adjust.Status == 0)
                {
                    btnCancel.Enabled = true;
                }
                else
                {
                    btnCancel.Enabled = false;
                }
                //if (pakai.Status > 0)
                if (adjust.ID > 0)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;

                }
            }
        }
        private void LoadAdjustApprov()
        {
            AdjustDetailFacade adjustFacade = new AdjustDetailFacade();
            ArrayList arrListAdjustDetail = new ArrayList();
            arrListAdjustDetail = adjustFacade.RetrieveByApproval(txtPakaiNo.Text);
            Session["ListOfAdjustApv"] = arrListAdjustDetail;
            GridApprove.DataSource = arrListAdjustDetail;
            GridApprove.DataBind();
        }

        private string ValidateText()
        {
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfAdjustDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfAdjustDetail"];
                if (arrPakaiDetail.Count == 0)
                    return "Tidak ada List Item yang di-input";
            }
            return string.Empty;
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrAdjustDetail = (ArrayList)Session["ListOfAdjustApv"];
            int i = 0;
            if (ChkAll.Checked == true) //& arrLembur.Count >0 
            {

                foreach (AdjustDetail Adjust in arrAdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = true;
                    i = i + 1;
                }
            }
            else
            {
                foreach (AdjustDetail Adjust in arrAdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = false;
                    i = i + 1;
                }
            }
        }
        protected void ChkAll0_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll0.Checked == true)
            {
                LoadAdjustApprov();
                PanelApprove.Visible = true;
                GridApprove.Visible = true;
                GridView1.Visible = false;

            }
            else
            {
                PanelApprove.Visible = false;
                GridApprove.Visible = false;
                GridView1.Visible = true;
            }
        }
        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AdjustDetailFacade T3AdjustDetailFacade = new AdjustDetailFacade();
            AdjustDetail adjdetail = new AdjustDetail();
            ArrayList arrAdjustDetail = (ArrayList)Session["ListOfAdjustApv"];
            Users users = (Users)Session["Users"];
            int i = 0; int j = 0;
            if (users.Apv == 0)
            {
                DisplayAJAXMessage(this, "Level Approval tidak mencukupi");
                return;
            }
            #region dim adjustIn asset otomatis insert to spb / wo 220202 by acc
            int GroupId = 0; string GroupCode = ""; string AdjustType = "";
            int DeptId = 0; int DepoId = 0; string UserName = "";
            #endregion
            foreach (AdjustDetail adjustdetail in arrAdjustDetail)
            {
                CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                if (chk.Checked)
                {
                    #region wo 09-04-2021 by acc / cek jika nonstok, maka qty = 0
                    if (adjustdetail.NonStok == "1")
                    {
                        adjustdetail.Quantity = 0;
                    }
                    #endregion
                    #region cek group adjustIn asset otomatis insert to spb / wo 220202 by acc
                    adjustdetail.Apv = 1;
                    adjustdetail.CreatedBy = users.UserName;
                    AssetFacade assetFacade = new AssetFacade();
                    Asset asset = assetFacade.RetrieveById(adjustdetail.ItemID);
                    if (asset.GroupID == 4)
                    {
                        GroupId = asset.GroupID;
                        AdjustFacade adjustFacade = new AdjustFacade();
                        Adjust adj = adjustFacade.RetrieveByNo(adjustdetail.AdjustNo);
                        UserName = adj.CreatedBy;
                        UsersFacade userFacade = new UsersFacade();
                        Users user = userFacade.RetrieveByUserName(adj.CreatedBy);
                        DeptId = user.DeptID;
                        DepoId = user.UnitKerjaID;
                    }
                    AdjustType = adjustdetail.AdjustType;
                    #endregion
                }
                i = i + 1;
            }
            Adjust adjust = new Adjust();
            AdjustProcessFacade AdjustProcessFacade = new AdjustProcessFacade(adjust, arrAdjustDetail);
            string strError = AdjustProcessFacade.UpdateApprove();
            #region proses adjustIn asset otomatis insert to spb / wo 220202 by acc
            int q1 = GroupId;
            string q2 = AdjustType;
            int q = 0;
            if (GroupId == 4 || GroupId == 12 && AdjustType == "Tambah")
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                if (GroupId == 4) { GroupCode = "C"; }
                if (GroupId == 12) { GroupCode = "E"; }
                int bln = DateTime.Parse(DateTime.Now.ToString()).Month;
                int thn = DateTime.Parse(DateTime.Now.ToString()).Year;
                int noBaru = 0;
                string kd = companyFacade.GetKodeCompany(users.UnitKerjaID) + GroupCode;
                PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade();
                PakaiDocNo pakaiDocNo = new PakaiDocNo();
                pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, kd);
                if (pakaiDocNo.ID == 0)
                {
                    noBaru = 1;
                    pakaiDocNo.PakaiCode = kd;
                    pakaiDocNo.NoUrut = 1;
                    pakaiDocNo.MonthPeriod = bln;
                    pakaiDocNo.YearPeriod = thn;
                }
                else
                {
                    noBaru = pakaiDocNo.NoUrut + 1;
                    pakaiDocNo.PakaiCode = kd;
                    pakaiDocNo.NoUrut = pakaiDocNo.NoUrut + 1;
                }
                string bl = ""; string th = "";
                if (pakaiDocNo.MonthPeriod < 10) { bl = "0" + pakaiDocNo.MonthPeriod.ToString(); }
                else { bl = pakaiDocNo.MonthPeriod.ToString(); }
                th = pakaiDocNo.YearPeriod.ToString().Substring(2, 2);
                int intPakaiDocNoID = 0;
                if (pakaiDocNo.NoUrut == 1)
                {
                    pakaiDocNoFacade = new PakaiDocNoFacade(pakaiDocNo);
                    intPakaiDocNoID = pakaiDocNoFacade.Insert(transManager);
                }
                else
                {
                    pakaiDocNoFacade = new PakaiDocNoFacade(pakaiDocNo);
                    intPakaiDocNoID = pakaiDocNoFacade.Update(transManager);
                }
                Pakai pakai = new Pakai();
                pakai.PakaiNo = pakaiDocNo.PakaiCode + th + bl + "-" + pakaiDocNo.NoUrut.ToString().PadLeft(5, '0');
                pakai.PakaiDate = DateTime.Parse(DateTime.Now.ToString());
                pakai.ApprovalDate = DateTime.Parse(DateTime.Now.ToString());
                pakai.PakaiTipe = GroupId;
                pakai.DeptID = DeptId;
                pakai.DepoID = DepoId;
                pakai.Status = 3;
                pakai.AlasanCancel = "";
                pakai.CreatedBy = UserName;
                pakai.ItemTypeID = 2;
                int intPakaiID = 0; int intResult = 0;
                AbstractTransactionFacade absTrans = new PakaiFacade(pakai);
                intPakaiID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); }
                if (intPakaiID > 0)
                {
                    pakai.ID = intPakaiID;
                    absTrans = new PakaiFacade(pakai);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); }
                }
                if (intPakaiID > 0)
                {
                    if (arrAdjustDetail.Count > 0)
                    {
                        foreach (AdjustDetail adjDetail in arrAdjustDetail)
                        {
                            CheckBox chks = (CheckBox)GridApprove.Rows[j].FindControl("chkapv");
                            if (chks.Checked)
                            {
                                AssetFacade assetFacade = new AssetFacade();
                                Asset asset = assetFacade.RetrieveById(adjDetail.ItemID);
                                if (asset.GroupID == 4 || asset.GroupID == 12)
                                {
                                    PakaiDetail pakaiDetail = new PakaiDetail();
                                    pakaiDetail.PakaiID = intPakaiID;
                                    pakaiDetail.ItemID = adjDetail.ItemID;
                                    pakaiDetail.Quantity = adjDetail.Quantity;
                                    pakaiDetail.Keterangan = adjDetail.Keterangan;
                                    pakaiDetail.RowStatus = 0;
                                    pakaiDetail.GroupID = GroupId;
                                    pakaiDetail.ItemTypeID = 2;
                                    pakaiDetail.AvgPrice = 0;
                                    pakaiDetail.IDJenisBiaya = 0;
                                    pakaiDetail.LineNo = 0;
                                    pakaiDetail.BudgetQty = 0;
                                    pakaiDetail.NoPol = "";
                                    pakaiDetail.UomID = asset.UOMID;
                                    absTrans = new PakaiDetailFacade(pakaiDetail);
                                    intResult = absTrans.Insert(transManager);
                                    if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); }
                                    #region Proces for asset
                                    if (intResult > 0)
                                    {
                                        asset.ID = pakaiDetail.ItemID;
                                        asset.Jumlah = pakaiDetail.Quantity;
                                        intResult = assetFacade.MinusQty(asset);
                                        if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); }
                                    }
                                    if (intResult > 0)
                                    {
                                        Asset cekInvAsset = assetFacade.InvAssetRetrieveByAssetID(
                                            pakaiDetail.ItemID, pakai.DeptID);
                                        if (cekInvAsset.ID > 0) { asset.Flag = 1; }
                                        else { asset.Flag = 0; }
                                        asset.AssetID = pakaiDetail.ItemID;
                                        asset.Jumlah = pakaiDetail.Quantity;
                                        asset.DeptID = DeptId;
                                        asset.Gudang = DepoId;
                                        asset.RowStatus = 0;
                                        intResult = assetFacade.InsertUpdateInvAsset(asset);
                                        if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); }
                                    }
                                    #endregion
                                }
                            }
                            j = j + 1;
                        }
                    }
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                #endregion
            }
            if (strError == string.Empty)
            {
                LoadAdjustApprov();
                EventLogProcess mp = new EventLogProcess();
                EventLog evl = new EventLog();
                mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                mp.Pilihan = "Insert";
                evl.UserID = ((Users)Session["Users"]).ID;
                evl.AppLevel = ((Users)Session["Users"]).Apv;
                evl.DocNo = adjdetail.AdjustNo.ToString();
                evl.DocType = "Adjustment";
                evl.AppDate = DateTime.Now;
                evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                mp.EventLogInsert(evl);
            }
        }

        protected void RBTunggal_CheckedChanged(object sender, EventArgs e)
        {
            rbDisposal.Enabled = true; rbKurang.Enabled = false; rbTambah.Enabled = true; rbTambah.Checked = true;
            rbDisposal.Checked = true; RBTunggal.Checked = true; RBKomponen.Checked = false;
        }

        protected void RBKomponen_CheckedChanged(object sender, EventArgs e)
        {
            rbDisposal.Enabled = false; rbKurang.Enabled = true; rbTambah.Enabled = true;
            RBTunggal.Checked = false; rbDisposal.Checked = false; rbTambah.Checked = true; rbKurang.Checked = false;
        }

        protected void GridApprove_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rbDisposal.Visible = (ddlTipeBarang.SelectedValue == "2") ? true : false;
            if (ddlTipeBarang.SelectedValue == "2")
            {
                tr1.Visible = true;
                tr2.Visible = true;
                tr3.Visible = true;
                rbDisposal.Checked = true; RBTunggal.Checked = true; rbDisposal.Visible = true;
                rbDisposal.Enabled = true;
                rbKurang.Enabled = false;
                rbTambah.Enabled = true;
            }
            else
            {
                tr1.Visible = false;
                tr2.Visible = true;
                //  rbDisposal.Enabled = true; 
                rbKurang.Enabled = true;
                rbTambah.Enabled = true;
                rbDisposal.Enabled = false;
            }


        }

    }
}