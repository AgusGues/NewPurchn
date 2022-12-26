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
    public partial class FormProcessConvert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";


                if (Request.QueryString["ConversiNo"] != null)
                {
                    LoadTableConversi(Request.QueryString["ConversiNo"].ToString());
                }
                else
                {
                    LoadFromUom();
                    LoadToUom();
                    clearForm();
                }
            }
            else
            {
                if (txtConversiNo.Text != string.Empty)
                {
                    LoadTableConversi(txtConversiNo.Text);
                    txtConversiNo.Text = string.Empty;
                }

            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        protected void txtFromJumlahProses_TextChanged(object sender, EventArgs e)
        {
            decimal FromJmlProsesAwal = Convert.ToDecimal(Session["FromJumlahProses"]);
            if (Convert.ToDecimal(txtFromJumlahProses.Text) > FromJmlProsesAwal)
            {
                DisplayAJAXMessage(this, "Harus lebih kecil atau sama dengan" + Session["FromJumlahProses"].ToString());
                txtFromJumlahProses.Text = Session["FromJumlahProses"].ToString();
                txtFromJumlahProses.Focus();
                return;
            }
            else
            {
                decimal txToQty = Convert.ToDecimal(txtToJumlah.Text);
                decimal txFrQty = Convert.ToDecimal(txtFromJumlah.Text);
                decimal txJml = Convert.ToDecimal(txtFromJumlahProses.Text);
                decimal hasilProses2 = (txToQty / txFrQty) * txJml;
                txtToJumlahProses.Text = hasilProses2.ToString("N2");
            }
            //txtFromJumlahProses.Text = invRepack.Jumlah.ToString("N2");
            //hasilProses = (tblConversi.ToQty / tblConversi.FromQty) * invRepack.Jumlah;
            //txtToJumlahProses.Text = hasilProses.ToString("N2");

        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfTabel"] = new ArrayList();
            //Session["Pakai"] = null;
            Session["RepackNo"] = null;
            Session["ConversiNo"] = null;

            txtFromJumlahProses.Text = string.Empty;
            txtToJumlahProses.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtFromItemName.Text = string.Empty;
            txtToItemName.Text = string.Empty;
            txtFromJumlah.Text = string.Empty;
            txtToJumlah.Text = string.Empty;
            txtConversiNo.Text = string.Empty;
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;

            if (ddlFromUomCode.SelectedIndex > 0)
                ddlFromUomCode.Items.Clear();
            if (ddlToUomCode.SelectedIndex > 0)
                ddlToUomCode.Items.Clear();
            if (ddlFromItemCode.SelectedIndex > 0)
                ddlFromItemCode.Items.Clear();
            if (ddlToItemCode.SelectedIndex > 0)
                ddlToItemCode.Items.Clear();

            ArrayList arrList = new ArrayList();
            arrList.Add(new Convertan());

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
            //from inv re-pack
            ArrayList arrInvRepack = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInvRepack = inventoryFacade.RepackRetrieveByCriteria("A.ItemName", txtFromJumlahProses.Text);
            if (inventoryFacade.Error == string.Empty)
            {
                ddlFromItemCode.Items.Clear();
                ddlFromItemCode.Items.Add(new ListItem("-- Pilih Inv. Re-Pack --", "0"));
                foreach (Inventory Inv in arrInvRepack)
                {
                    ddlFromItemCode.Items.Add(new ListItem(Inv.ItemName, Inv.ID.ToString()));
                }
            }
        }

        private void LoadToItems()
        {
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.RetrieveByCriteria("A.ItemName", txtFromJumlahProses.Text);
            if (inventoryFacade.Error == string.Empty)
            {
                ddlToItemCode.Items.Clear();
                ddlToItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
                foreach (Inventory Inv in arrInventory)
                {
                    ddlToItemCode.Items.Add(new ListItem(Inv.ItemName, Inv.ID.ToString()));
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
                //e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("N2");
                //e.Row.Cells[5].Text = Convert.ToDecimal(e.Row.Cells[5].Text).ToString("N2");
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListTabelConversi.aspx?approve=XP");
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

            string strError = string.Empty;
            string strEvent = "Insert";
            Convertan convertan = new Convertan();
            ConvertanFacade convertanfacade = new ConvertanFacade();

            if (Session["id"] != null)
            {
                //convertan.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }

            convertan.RepackNo = txtConversiNo.Text;
            convertan.CreatedBy = ((Users)Session["Users"]).UserName;

            convertan.FromItemID = int.Parse(ddlFromItemCode.SelectedValue);
            convertan.FromItemCode = ddlFromItemCode.SelectedItem.ToString();
            convertan.FromUomID = int.Parse(ddlFromUomCode.SelectedValue);

            convertan.FromUomCode = ddlFromUomCode.SelectedItem.ToString();
            convertan.ToItemID = int.Parse(ddlToItemCode.SelectedValue);
            convertan.ToItemCode = ddlToItemCode.SelectedItem.ToString();
            convertan.ToUomID = int.Parse(ddlToUomCode.SelectedValue);

            convertan.ToUomCode = ddlToUomCode.SelectedItem.ToString();
            convertan.RowStatus = 0;

            convertan.FromQty = decimal.Parse(txtFromJumlahProses.Text);
            convertan.ToQty = decimal.Parse(txtToJumlahProses.Text);

            ConvertanProcessFacade ConvertanProcessFacade = new ConvertanProcessFacade(convertan);
            if (convertan.ID > 0)
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
                strError = ConvertanProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtProsesNo.Text = ConvertanProcessFacade.RepackNo;
                    //Session["id"] = convertan.ID;
                    Session["RepackNo"] = ConvertanProcessFacade.RepackNo;

                    ArrayList arrConvertan = new ArrayList();
                    arrConvertan.Add(convertan);

                    GridView1.DataSource = arrConvertan;
                    GridView1.DataBind();
                }
            }

            //ArrayList arrConvertan2= new ArrayList();
            //arrConvertan2.Add(convertan);

            //GridView1.DataSource = arrConvertan2;
            //GridView1.DataBind();

            if (strError == string.Empty)
            {
                //ddlItemName.Items.Clear();
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
            eventLog.ModulName = "Entry Proses Re-Pack";
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
            // dari inventory utk ambil itemname & uomcode krn akan sama dgn yg di-inventory
            // dari inv re-part ambil qty * qty pd table convert utk itemID tsb
            if (ddlFromItemCode.SelectedIndex > 0)
            {
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlFromItemCode.SelectedValue));
                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtFromItemName.Text = inv.ItemName;
                    ddlFromUomCode.SelectedIndex = inv.UOMID;
                }
                ConvertanFacade convertanfacade = new ConvertanFacade();
                //TabelConversi tabelConversi = tabelConversiFacade.RetrieveByNo2("");

                Inventory invRepack = invFacade.RepackRetrieveById(int.Parse(ddlFromItemCode.SelectedValue));
                if (invFacade.Error == string.Empty && invRepack.ID > 0)
                {
                    txtFromJumlah.Text = invRepack.Jumlah.ToString("N2");
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
            //LoadTableConversi(txtSearch.Text);
            LoadConvertan(txtSearch.Text);
        }

        private void LoadConvertan(string strRepackNo)
        {

            ConvertanFacade convertanFacade = new ConvertanFacade();
            Convertan convertan = convertanFacade.RetrieveByNo3(strRepackNo);
            txtProsesNo.Text = convertan.RepackNo;

            ddlFromItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            ddlFromItemCode.Items.Add(new ListItem(convertan.FromItemCode, convertan.ID.ToString()));
            ddlFromItemCode.SelectedIndex = 1;
            ddlToItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            ddlToItemCode.Items.Add(new ListItem(convertan.ToItemCode, convertan.ID.ToString()));
            ddlToItemCode.SelectedIndex = 1;
            ddlFromUomCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            ddlFromUomCode.Items.Add(new ListItem(convertan.FromUomCode, convertan.ID.ToString()));
            ddlFromUomCode.SelectedIndex = 1;
            ddlToUomCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            ddlToUomCode.Items.Add(new ListItem(convertan.ToUomCode, convertan.ID.ToString()));
            ddlToUomCode.SelectedIndex = 1;

            txtFromJumlahProses.Text = convertan.FromQty.ToString();
            txtToJumlahProses.Text = convertan.ToQty.ToString();

            //txtFromJumlahProses.Text = convertan.FromQty.ToString("N2");
            //txtToJumlahProses.Text = convertan.ToQty.ToString("N2");
            //ddlFromUomCode.SelectedValue = convertan.FromUomCode;
            //ddlToUomCode.SelectedValue = convertan.ToUomCode;

        }

        private void LoadTableConversi(string strConversiNo)
        {
            clearForm();

            TabelConversiFacade tabelConversiFacade = new TabelConversiFacade();
            ArrayList ArrTabelConversi = tabelConversiFacade.RetrieveByNo2(strConversiNo);
            if (tabelConversiFacade.Error == string.Empty)
            {
                foreach (TabelConversi tblConversi in ArrTabelConversi)
                {
                    decimal hasilProses = 0;


                    InventoryFacade invFacade = new InventoryFacade();
                    Inventory invFrom = invFacade.RetrieveById(tblConversi.FromItemID);
                    if (invFacade.Error == string.Empty && invFrom.ID > 0)
                    {
                        txtFromItemName.Text = invFrom.ItemName;

                        ddlFromItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
                        ddlFromItemCode.Items.Add(new ListItem(invFrom.ItemCode, invFrom.ID.ToString()));
                        ddlFromItemCode.SelectedIndex = 1;

                        ddlFromUomCode.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
                        ddlFromUomCode.Items.Add(new ListItem(invFrom.UOMDesc, invFrom.UOMID.ToString()));
                        ddlFromUomCode.SelectedIndex = 1;
                    }
                    Inventory invTo = invFacade.RetrieveById(tblConversi.ToItemID);
                    if (invFacade.Error == string.Empty && invTo.ID > 0)
                    {
                        txtToItemName.Text = invTo.ItemName;

                        ddlToItemCode.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
                        ddlToItemCode.Items.Add(new ListItem(invTo.ItemCode, invTo.ID.ToString()));
                        ddlToItemCode.SelectedIndex = 1;

                        ddlToUomCode.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
                        ddlToUomCode.Items.Add(new ListItem(invTo.UOMDesc, invTo.UOMID.ToString()));
                        ddlToUomCode.SelectedIndex = 1;
                        txtStok.Text = invTo.Jumlah.ToString("N2");
                    }

                    Inventory invRepack = invFacade.RepackRetrieveById(tblConversi.FromItemID);
                    if (invFacade.Error == string.Empty && invRepack.ID > 0)
                    {
                        if (invRepack.Jumlah <= 0)
                        {
                            string aMess = "Jumlah Stok Re-Pack utk " + invFrom.ItemCode + " <= Nol (blom Input Pemakaian Re-Pack)";
                            DisplayAJAXMessage(this, aMess);
                            clearForm();
                            return;
                        }

                        txtFromJumlahProses.Text = invRepack.Jumlah.ToString("N2");
                        Session["FromJumlahProses"] = invRepack.Jumlah.ToString("N2");
                        hasilProses = (tblConversi.ToQty / tblConversi.FromQty) * invRepack.Jumlah;
                        txtToJumlahProses.Text = hasilProses.ToString("N2");
                    }


                    txtFromJumlah.Text = tblConversi.FromQty.ToString("N2");
                    txtToJumlah.Text = tblConversi.ToQty.ToString("N2");
                    txtConversiNo.Text = tblConversi.ConversiNo;
                    txtTanggal.Text = tblConversi.CreatedTime.ToString("dd-MMM-yyyy");
                    txtCreatedBy.Text = tblConversi.CreatedBy;

                    //Session["id"] = tblConversi.ID;
                    Session["RepackNo"] = tblConversi.ConversiNo;
                    if (tblConversi.RowStatus == -1)
                        btnCancel.Enabled = false;
                    else
                        btnCancel.Enabled = true;
                    if (tblConversi.ID > 0)
                    {
                        //lbAddOP.Enabled = false;
                        btnUpdate.Disabled = false;

                    }

                }

                //GridView1.DataSource = ArrTabelConversi;
                //GridView1.DataBind();

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
            if (decimal.Parse(txtFromJumlahProses.Text) == 0)
            {
                return "Dari Jumlah Proses tidak boleh 0";
            }
            if (decimal.Parse(txtToJumlahProses.Text) == 0)
            {
                return "Ke Jumlah Proses tidak boleh 0";
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
    }
}