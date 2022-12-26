using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormTableConversi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                clearForm();
                LoadFromUom();
                LoadToUom();

            }
            else
            {
                if (txtFromCariItemCode.Text != string.Empty)
                {
                    LoadFromItems();
                    txtFromCariItemCode.Text = string.Empty;
                }
                if (txtToCariItemCode.Text != string.Empty)
                {
                    LoadToItems();
                    txtToCariItemCode.Text = string.Empty;
                }

            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfTabel"] = new ArrayList();
            //Session["Pakai"] = null;
            Session["ConversiNo"] = null;

            txtFromCariItemCode.Text = string.Empty;
            txtToCariItemCode.Text = string.Empty;
            txtFromItemName.Text = string.Empty;
            txtToItemName.Text = string.Empty;
            txtFromJumlah.Text = string.Empty;
            txtToJumlah.Text = string.Empty;
            txtConversiNo.Text = string.Empty;
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;

            //ddlFromUomCode.Items.Clear();
            //ddlToUomCode.Items.Clear();
            //ddlFromItemCode.Items.Clear();
            //ddlToItemCode.Items.Clear();

            ArrayList arrList = new ArrayList();
            arrList.Add(new TabelConversi());

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

        private void LoadFromUom()
        {
            ArrayList arrUomCode = new ArrayList();
            UOMFacade uomFacade = new UOMFacade();
            arrUomCode = uomFacade.Retrieve();

            ddlFromUomCode.Items.Add(new ListItem("-- Pilih Satuan --", string.Empty));
            foreach (UOM uom in arrUomCode)
            {
                ddlFromUomCode.Items.Add(new ListItem(uom.UOMCode, uom.ID.ToString()));
            }
        }
        private void LoadToUom()
        {
            ArrayList arrUomCode = new ArrayList();
            UOMFacade uomFacade = new UOMFacade();
            arrUomCode = uomFacade.Retrieve();

            ddlToUomCode.Items.Add(new ListItem("-- Pilih Satuan --", string.Empty));
            foreach (UOM uom in arrUomCode)
            {
                ddlToUomCode.Items.Add(new ListItem(uom.UOMCode, uom.ID.ToString()));
            }
        }

        private void LoadFromItems()
        {
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.RetrieveByCriteria("A.ItemName", txtFromCariItemCode.Text);
            if (inventoryFacade.Error == string.Empty)
            {
                ddlFromItemCode.Items.Clear();
                ddlFromItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
                foreach (Inventory Inv in arrInventory)
                {
                    ddlFromItemCode.Items.Add(new ListItem(Inv.ItemName + " (" + Inv.ItemCode + ")", Inv.ID.ToString()));
                }
            }
        }

        private void LoadToItems()
        {
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.RetrieveByCriteria("A.ItemName", txtToCariItemCode.Text);
            if (inventoryFacade.Error == string.Empty)
            {
                ddlToItemCode.Items.Clear();
                ddlToItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
                foreach (Inventory Inv in arrInventory)
                {
                    ddlToItemCode.Items.Add(new ListItem(Inv.ItemName + " (" + Inv.ItemCode + ")", Inv.ID.ToString()));
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                //// bisa cancel hanya bisa level head dept ke atas
                //int intApv = ((Users)Session["Users"]).Apv;
                //if (intApv > 0)
                //{

                //    if (btnCancel.Enabled == false)
                //        return;

                //    string strDocumentNo = string.Empty;
                //    int intPakaiDetailID = 0;
                //    string strEvent = string.Empty;

                //    int index = Convert.ToInt32(e.CommandArgument);
                //    ArrayList arrTransferDetail = new ArrayList();
                //    arrTransferDetail = (ArrayList)Session["ListOfAdjustDetail"];

                //    Pakai pakai = new Pakai();
                //    if (Session["id"] != null)
                //    {
                //        // masuk sini krn sudah di save
                //        // next job
                //        // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                //        int id = (int)Session["id"];

                //        PakaiFacade pakaiFacade = new PakaiFacade();

                //        // musti pake PakaiTipe agar bisa dibedakan No pemakaian-nya
                //        // musti dipikirin utk gak bisa hapus jika accounting dah closing
                //        // 2 = bahan baku
                //        string strPakaiTipe = "KP";
                //        Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                //        if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                //        {
                //            {
                //                int i = 0;
                //                int x = 0;
                //                PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                //                ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                //                if (pakaiDetailFacade.Error == string.Empty)
                //                {
                //                    PakaiDetail receiptDetail1 = (PakaiDetail)arrTransferDetail[index];
                //                    bool valid = false;
                //                    int pkiDetailID = 0;

                //                    foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                //                    {
                //                        if (pkiDetail.ItemID == receiptDetail1.ItemID)
                //                        {
                //                            // cek stok
                //                            InventoryFacade inventoryFacade = new InventoryFacade();
                //                            Inventory inv = inventoryFacade.RetrieveById(pkiDetail.ItemID);
                //                            if (inventoryFacade.Error == string.Empty)
                //                            {
                //                                if (inv.Jumlah - receiptDetail1.Quantity < 0)
                //                                {
                //                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
                //                                    return;
                //                                }
                //                            }

                //                            pkiDetailID = pkiDetail.ID;
                //                            i = x;
                //                            valid = true;
                //                            break;
                //                        }

                //                        x = x + 1;
                //                    }

                //                    if (valid == false)
                //                    {
                //                        arrTransferDetail.RemoveAt(index);
                //                        Session["ListOfAdjustDetail"] = arrTransferDetail;
                //                        GridView1.DataSource = arrTransferDetail;
                //                        GridView1.DataBind();
                //                    }
                //                    else
                //                    {
                //                        PakaiDetail pakaiDetail = (PakaiDetail)arrTransferDetail[index];
                //                        ArrayList arrPakaiDetail = new ArrayList();
                //                        foreach (PakaiDetail pd in arrTransferDetail)
                //                        {
                //                            if (pd.ID == pkiDetailID)
                //                            {
                //                                //((ReceiptDetail)arrTransferDetail[i]).FlagPO = flagPO;
                //                                arrPakaiDetail.Add(pd);
                //                            }
                //                        }

                //                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(new Pakai(), arrPakaiDetail, new PakaiDocNo());
                //                        intPakaiDetailID = pakaiDetail.ID;

                //                        string strError = pakaiProcessFacade.CancelPakaiDetail();
                //                        if (strError != string.Empty)
                //                        {
                //                            DisplayAJAXMessage(this, strError);
                //                            return;
                //                        }
                //                        ArrayList arrTransfer = new ArrayList();

                //                        foreach (PakaiDetail pd in arrTransferDetail)
                //                        {
                //                            //if (rd.DocumentNo != strDocumentNo)
                //                            if (pd.ID != intPakaiDetailID)
                //                            {
                //                                arrTransfer.Add(pd);
                //                            }
                //                        }

                //                        strEvent = "Hapus per Baris";
                //                        InsertLog(strEvent);

                //                        Session["ListOfAdjustDetail"] = arrTransfer;
                //                        GridView1.DataSource = arrTransfer;
                //                        GridView1.DataBind();
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        arrTransferDetail.RemoveAt(index);
                //        Session["ListOfAdjustDetail"] = arrTransferDetail;

                //        GridView1.DataSource = arrTransferDetail;
                //        GridView1.DataBind();
                //    }
                //}
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("N2");
                e.Row.Cells[5].Text = Convert.ToDecimal(e.Row.Cells[5].Text).ToString("N2");
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListTabelConversi.aspx?approve=ZZ");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            //// bisa cancel hanya bisa level head dept ke atas
            //int intApv = ((Users)Session["Users"]).Apv;

            //if (Session["id"] != null && intApv > 0)
            //{

            //    // masuk sini krn sudah di save
            //    // next job
            //    // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

            //    int id = (int)Session["id"];
            //    string strEvent = string.Empty;

            //    PakaiFacade pakaiFacade = new PakaiFacade();
            //    // 2 = bahan baku
            //    string strPakaiTipe = "KP";
            //    Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
            //    if (pakaiFacade.Error == string.Empty && pki.ID > 0)
            //    {
            //        if (Session["AlasanCancel"] != null)
            //        {
            //            pki.AlasanCancel = Session["AlasanCancel"].ToString();
            //            Session["AlasanCancel"] = null;
            //        }
            //        else
            //        {
            //            DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong / blank");
            //            return;
            //        }

            //        PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
            //        ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
            //        if (pakaiDetailFacade.Error == string.Empty)
            //        {

            //            ArrayList arrPakaiDetail = new ArrayList();
            //            foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
            //            {
            //                InventoryFacade inventoryFacade = new InventoryFacade();
            //                Inventory inv = inventoryFacade.RetrieveById(pkiDetail.ItemID);
            //                if (inventoryFacade.Error == string.Empty)
            //                {
            //                    if (inv.Jumlah - pkiDetail.Quantity < 0)
            //                    {
            //                        DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
            //                        return;
            //                    }
            //                }

            //                arrPakaiDetail.Add(pkiDetail);
            //            }

            //            PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pki, arrPakaiDetail, new PakaiDocNo());

            //            string strError = pakaiProcessFacade.CancelPakai();
            //            if (strError != string.Empty)
            //            {
            //                DisplayAJAXMessage(this, strError);
            //                return;
            //            }

            //            else
            //            {
            //                strEvent = "Cancel All";
            //                InsertLog(strEvent);

            //                DisplayAJAXMessage(this, "Cancel berhasil .....");
            //                clearForm();
            //            }

            //        }
            //    }
            //}
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
        }

        //private void clearQty()
        //{
        //    txtItemCode.Text = string.Empty;
        //    txtQtyPakai.Text = string.Empty;
        //    txtStok.Text = string.Empty;
        //    txtUom.Text = string.Empty;
        //    txtKeterangan.Text = string.Empty;
        //    txtUomID.Text = string.Empty;

        //    ddlItemName.SelectedIndex = 0;
        //}

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            //int bln = DateTime.Parse(txtTanggal.Text).Month;
            //int thn = DateTime.Parse(txtTanggal.Text).Year;
            //int noBaru = 0;

            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strError = string.Empty;
            string strEvent = "Insert";
            TabelConversi tabelConversi = new TabelConversi();
            TabelConversiFacade tabelConversiFacade = new TabelConversiFacade();

            TabelConversi cekTabel = tabelConversiFacade.CekTabel(int.Parse(ddlFromItemCode.SelectedValue), int.Parse(ddlFromUomCode.SelectedValue), int.Parse(ddlToItemCode.SelectedValue), int.Parse(ddlToUomCode.SelectedValue));
            if (tabelConversiFacade.Error == string.Empty && cekTabel.ID > 0)
            {
                DisplayAJAXMessage(this, "Tabel Conversi tidak boleh sama");
                return;
            }

            if (Session["id"] != null)
            {
                tabelConversi.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            tabelConversi.ConversiNo = txtConversiNo.Text;
            tabelConversi.CreatedBy = ((Users)Session["Users"]).UserName;

            tabelConversi.FromItemID = int.Parse(ddlFromItemCode.SelectedValue);
            tabelConversi.FromQty = decimal.Parse(txtFromJumlah.Text);
            tabelConversi.FromUomID = int.Parse(ddlFromUomCode.SelectedValue);
            tabelConversi.ToItemID = int.Parse(ddlToItemCode.SelectedValue);
            tabelConversi.ToQty = decimal.Parse(txtToJumlah.Text);
            tabelConversi.ToUomID = int.Parse(ddlToUomCode.SelectedValue);
            tabelConversi.RowStatus = 0;
            tabelConversi.FromItemCode = ddlFromItemCode.SelectedValue;
            tabelConversi.ToItemCode = ddlToItemCode.SelectedValue;
            tabelConversi.FromUomCode = ddlFromUomCode.SelectedValue;
            tabelConversi.ToUomCode = ddlToUomCode.SelectedValue;

            TabelConversiProcessFacade tabelConversiProcessFacade = new TabelConversiProcessFacade(tabelConversi);
            if (tabelConversi.ID > 0)
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
                strError = tabelConversiProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtConversiNo.Text = tabelConversiProcessFacade.ConversiNo;
                    Session["id"] = tabelConversi.ID;
                    Session["ConversiNo"] = tabelConversiProcessFacade.ConversiNo;

                    ArrayList ArrTabelConversi = new ArrayList();
                    ArrTabelConversi.Add(tabelConversi);

                    GridView1.DataSource = ArrTabelConversi;
                    GridView1.DataBind();
                }
            }

            //if (strError == string.Empty)
            //{
            //    ddlItemName.Items.Clear();
            //    //txtCariOP.ReadOnly = false;

            //    InsertLog(strEvent);
            //    btnUpdate.Disabled = true;
            //    if (strEvent == "Edit")
            //        clearForm();

            //}
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Tabel Conversi";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtConversiNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        protected void ddlToUomCode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlFromUomCode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlFromItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFromItemCode.SelectedIndex > 0)
            {
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlFromItemCode.SelectedValue));
                if (invFacade.Error == string.Empty)
                {
                    txtFromItemName.Text = inv.ItemName;
                    ddlFromUomCode.SelectedIndex = inv.UOMID;
                }
            }
        }
        protected void ddlToItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToItemCode.SelectedIndex > 0)
            {
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlToItemCode.SelectedValue));
                if (invFacade.Error == string.Empty)
                {
                    txtToItemName.Text = inv.ItemName;
                    ddlToUomCode.SelectedIndex = inv.UOMID;
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadTableConversi(txtSearch.Text);
        }

        private void LoadTableConversi(string strAdjustNo)
        {
            clearForm();

            TabelConversiFacade tabelConversiFacade = new TabelConversiFacade();
            ArrayList ArrTabelConversi = tabelConversiFacade.RetrieveByNo2(strAdjustNo);
            if (tabelConversiFacade.Error == string.Empty)
            {
                foreach (TabelConversi tblConversi in ArrTabelConversi)
                {
                    InventoryFacade invFacade = new InventoryFacade();
                    Inventory invFrom = invFacade.RetrieveById(tblConversi.FromItemID);
                    if (invFacade.Error == string.Empty && invFrom.ID > 0)
                    {
                        txtFromItemName.Text = invFrom.ItemName;
                    }
                    Inventory invTo = invFacade.RetrieveById(tblConversi.ToItemID);
                    if (invFacade.Error == string.Empty && invTo.ID > 0)
                    {
                        txtToItemName.Text = invTo.ItemName;
                    }

                    ddlFromUomCode.SelectedIndex = tblConversi.FromUomID;
                    ddlToUomCode.SelectedIndex = tblConversi.ToUomID;
                    txtFromJumlah.Text = tblConversi.FromQty.ToString("N2");
                    txtToJumlah.Text = tblConversi.ToQty.ToString("N2");
                    txtConversiNo.Text = tblConversi.ConversiNo;
                    txtTanggal.Text = tblConversi.CreatedTime.ToString("dd-MMM-yyyy");
                    txtCreatedBy.Text = tblConversi.CreatedBy;

                    Session["id"] = tblConversi.ID;
                    Session["ConversiNo"] = tblConversi.ConversiNo;
                    if (tblConversi.RowStatus == -1)
                        btnCancel.Enabled = false;
                    else
                        btnCancel.Enabled = true;
                    if (tblConversi.ID > 0)
                    {
                        lbAddOP.Enabled = false;
                        btnUpdate.Disabled = true;
                    }

                }

                GridView1.DataSource = ArrTabelConversi;
                GridView1.DataBind();

            }
        }

        private string ValidateText()
        {
            try
            {
                decimal cekNumber = decimal.Parse(txtFromJumlah.Text);
            }
            catch
            {
                return "Quantity Pakai harus number";
            }
            try
            {
                decimal cekNumber = decimal.Parse(txtToJumlah.Text);
            }
            catch
            {
                return "Quantity Pakai harus number";
            }
            if (decimal.Parse(txtFromJumlah.Text) == 0)
            {
                return "Dari Quantity tidak boleh 0";
            }
            if (decimal.Parse(txtToJumlah.Text) == 0)
            {
                return "Ke Quantity tidak boleh 0";
            }
            if (ddlFromItemCode.SelectedIndex == 0)
            {
                return "Dari Kode Barang tidak boleh kosong";
            }
            if (ddlToItemCode.SelectedIndex == 0)
            {
                return "Ke Kode Barang tidak boleh kosong";
            }
            if (ddlFromUomCode.SelectedIndex == 0)
            {
                return "Dari Satuan tidak boleh kosong";
            }
            if (ddlToUomCode.SelectedIndex == 0)
            {
                return "Ke Satuan tidak boleh kosong";
            }
            if (ddlFromItemCode.SelectedValue == ddlToItemCode.SelectedValue)
            {
                return "Kode Barang tidak boleh sama";
            }

            return string.Empty;
        }

        protected void txtFromCariItemCode_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtToCariItemCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}