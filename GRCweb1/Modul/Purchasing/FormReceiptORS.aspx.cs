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
    public partial class FormReceiptORS : System.Web.UI.Page
    {
        public enum Status
        {
            Open = 0,
            Parsial = 1,
            Receipt = 2,
            PrePayment = 3,
            CreateGiro = 4,
            Release = 5,

            //ReceiptType = 3 = ORS = KA
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Global.link = "~/Default.aspx";

                if (Request.QueryString["ReceiptNo"] != null)
                {
                    LoadReceipt(Request.QueryString["ReceiptNo"].ToString());
                }
                //else if (Request.QueryString["receipt"] != null && Request.QueryString["receipt"].ToString() == "yes")
                //{
                //    LoadScheduleBySession();
                //}
                else
                {
                    clearForm();
                }
            }
            else
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();

                //string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

                //if (txtCariOP.Text != string.Empty && txtCariOP.Text.Substring(0, 2) == kd)
                if (txtCariOP.Text != string.Empty)
                {
                    POPurchnFacade poPurchnFacade = new POPurchnFacade();
                    POPurchn po = poPurchnFacade.RetrieveByNoCheckStatus(txtCariOP.Text);
                    SuppPurch supplier = new SuppPurch();
                    SuppPurchFacade supplierF = new SuppPurchFacade();
                    supplier = supplierF.RetrieveById(po.SupplierID);
                    txtSupplier.Text = supplier.SupplierName.Trim();
                    if (po.ID > 0)
                    {
                        if (po.POPurchnDate > DateTime.Parse(txtTanggal.Text))
                        {
                            DisplayAJAXMessage(this, "Tanggal PO tidak boleh lebih besar dari tanggal receipt");
                            return;
                        }
                        txtTanggal.Enabled = false;
                        txtQty.Text = string.Empty;
                        txtQtyTerima.Text = string.Empty;
                        txtPONo.Text = po.NoPO;
                        txtCreatedBy.Text = ((Users)Session["Users"]).UserName;

                        POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                        ArrayList arrPurchnDetail = poPurchnDetailFacade.RetrieveById(po.ID);
                        if (poPurchnDetailFacade.Error == string.Empty)
                        {
                            ddlItemName.Items.Clear();
                            ddlItemName.Items.Add(new ListItem("-- Pilih Item --", "0"));
                            foreach (POPurchnDetail purchnDetail in arrPurchnDetail)
                            {
                                if (purchnDetail.GroupID == 3)
                                    ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                                else
                                    DisplayAJAXMessage(this, "Barang yang pada No. PO tidak termasuk group barang ATK");
                                //ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName + " - " + purchnDetail.DocumentNo, purchnDetail.ID.ToString()));
                            }
                        }
                    }
                    else
                    {
                        //if (txtCariOP.Text.Substring(0, 2) == kd)
                        //{
                        POPurchn cekPo = poPurchnFacade.RetrieveByNo(txtCariOP.Text);
                        if (cekPo.ID > 0)
                        {
                            if (cekPo.Approval == 0)
                            {
                                DisplayAJAXMessage(this, "Belum di-Approval Head Dept");
                                clearForm();
                                return;
                            }
                            else if (cekPo.Approval == 1)
                            {
                                DisplayAJAXMessage(this, "Belum di-Approval Manager Purchasing");
                                clearForm();
                                return;
                            }
                            //else if (cekPo.Approval == 2)
                            //{
                            //    DisplayAJAXMessage(this, "Belum di-Approval Manager Plan");
                            //    clearForm();
                            //    return;
                            //}

                            txtCariOP.ReadOnly = true;
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "No PO tersebut tidak ada ...!");
                            clearForm();
                            return;
                        }
                        //}
                        //else
                        //{
                        //    DisplayAJAXMessage(this, "Bukan No PO untuk ORS ...!");
                        //    clearForm();
                        //    return;
                        //}
                    }

                    txtCariOP.Text = string.Empty;
                }
            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadReceipt(txtSearch.Text);
        }

        private void LoadReceipt(string strReceiptNo)
        {
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            string forReceipt = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";
            //string forReceipt = "KA";
            clearForm();

            ReceiptFacade receiptFacade = new ReceiptFacade();
            Receipt receipt = receiptFacade.RetrieveByNoWithStatus(strReceiptNo, forReceipt);
            if (receiptFacade.Error == string.Empty && receipt.ID > 0)
            {
                Session["id"] = receipt.ID;

                txtMrsNo.Text = receipt.ReceiptNo;
                txtPONo.Text = receipt.PoNo;
                txtTanggal.Text = receipt.ReceiptDate.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = receipt.CreatedBy;

                POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                ArrayList arrPurchnDetail = poPurchnDetailFacade.RetrieveById(receipt.PoID);
                if (poPurchnDetailFacade.Error == string.Empty)
                {
                    ddlItemName.Items.Clear();
                    ddlItemName.Items.Add(new ListItem("-- Pilih Item --", "0"));
                    foreach (POPurchnDetail purchnDetail in arrPurchnDetail)
                    {
                        ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                    }
                }

                ArrayList arrListScheduleDetail = new ArrayList();
                ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receipt.ID);
                if (receiptDetailFacade.Error == string.Empty)
                {
                    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    {
                        if (receiptDetail.ID > 0)
                            arrListScheduleDetail.Add(receiptDetail);
                    }
                }
                Session["ReceiptNo"] = receipt.ReceiptNo;
                Session["ListOfReceiptDetail"] = arrListScheduleDetail;
                GridView1.DataSource = arrListScheduleDetail;
                GridView1.DataBind();

                //sehinnga tdk bisa add po yg laen
                txtCariOP.Enabled = false;

                if (receipt.Status == -1)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                //if (receipt.Status == 0)
                if (receipt.Status > 0)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    //btnPrint.Disabled = false;
                }
                else
                {
                    if (receipt.Status == 0)
                    {
                        btnPrint.Disabled = false;
                    }

                }
            }
        }


        private void LoadScheduleBySession()
        {
            DisplayAJAXMessage(this, "Not yet");
            //if (Session["Schedule"] != null)
            //{
            //        Schedule schedule = (Schedule)Session["Schedule"];
            //        txtScheduleNo.Text = schedule.ScheduleNo;
            //        LoadExpedisi();
            //        SelectExpedisiName(schedule.ExpedisiName);
            //        LoadCarType(int.Parse(ddlExpedisiName.SelectedValue));
            //        SelectCarType(schedule.CarType);
            //        txtKubikasi.Text = GetKubikasi(int.Parse(ddlCarType.SelectedValue)).ToString("N0");
            //        txtScheduleDate.Text = schedule.ScheduleDate.ToString("dd-MMM-yyyy");
            //        txtRit.Text = schedule.Rate;
            //        txtKeterangan.Text = schedule.Keterangan;
            //        txtCreatedBy.Text = schedule.CreatedBy;
            //        JoinSessionToGrid();
            //        txtTotalKubikasi.Text = SumTotalKubikasi().ToString("N0");

            //}
        }


        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfReceiptDetail"] = new ArrayList();
            Session["Receipt"] = null;
            Session["ReceiptNo"] = null;

            //Session["ListOfScheduleDetail"] = new ArrayList();
            //Session["ListOfScheduleDetailOP"] = new ArrayList();
            //Session["ListOfScheduleDetailTO"] = new ArrayList();
            //Session["Schedule"] = null;
            //Session["TOId"] = null;
            //Session["OPId"] = null;
            //Session["ListTransferDetail"] = null;
            //Session["ListTOApprove"] = null;
            //Session["ListOPDetail"] = null;
            //Session["ListOPApprove"] = null;

            txtMrsNo.Text = string.Empty;
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            txtUom.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtNoSpp.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtCariOP.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtQtyTerima.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ddlItemName.Items.Clear();
            txtCariOP.ReadOnly = false;

            ArrayList arrList = new ArrayList();
            arrList.Add(new ReceiptDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = false;
            lbAddOP.Enabled = true;
            txtTanggal.Enabled = true;
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("ReceiptNo");
            Session.Remove("ListOfReceiptDetail");
            Session.Remove("id");
            Session.Remove("Receipt");

            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int bln = DateTime.Parse(txtTanggal.Text).Month;
            int thn = DateTime.Parse(txtTanggal.Text).Year;
            int noBaru = 0;

            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            //Schedule schedule = new Schedule();
            Receipt receipt = new Receipt();

            if (Session["id"] != null)
            {
                //    schedule.ID = int.Parse(Session["id"].ToString());

                //    ScheduleDetailFacade sDetailFacade = new ScheduleDetailFacade();
                //    ArrayList arrDistinctSchedule = sDetailFacade.RetrieveDistinctById(int.Parse(Session["id"].ToString()));

                //    SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
                //    SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();

                //    ScheduleFacade scheduleFacade = new ScheduleFacade();
                //    Schedule sch = scheduleFacade.RetrieveById(schedule.ID);
                //    if (scheduleFacade.Error == string.Empty)
                //    {
                //        if (sch.ScheduleDate.ToString("yyyyMMdd") != DateTime.Parse(txtScheduleDate.Text).ToString("yyyyMMdd"))
                //        {

                //            foreach (int[] documentId in arrDistinctSchedule)
                //            {
                //                if (documentId[1] == 0)
                //                {
                //                    int jumlahOP = suratJalanFacade.GetJumOPById(documentId[0], schedule.ID);
                //                    if (jumlahOP > 0)
                //                    {
                //                        DisplayAJAXMessage(this, "Cancel dulu surat Jalan OP");
                //                        return;
                //                    }
                //                }
                //                else
                //                {
                //                    int jumlahTO = suratJalanTOFacade.GetJumTOById(documentId[0], schedule.ID);
                //                    if (jumlahTO > 0)
                //                    {
                //                        DisplayAJAXMessage(this, "Cancel dulu surat Jalan TO");
                //                        return;
                //                    }
                //                }

                //            }
                //        }
                //    }

                strEvent = "Edit";
            }
            //here for document number

            ReceiptDocNoFacade receiptDocNoFacade = new ReceiptDocNoFacade();
            ReceiptDocNo receiptDocNo = new ReceiptDocNo();

            if (strEvent == "Insert")
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";
                receiptDocNo = receiptDocNoFacade.RetrieveByReceiptCode(bln, thn, kd);
                if (receiptDocNo.ID == 0)
                {
                    noBaru = 1;
                    //receiptDocNo.ReceiptCode = txtPONo.Text.Substring(0, 2);
                    receiptDocNo.ReceiptCode = kd;
                    receiptDocNo.NoUrut = 1;
                    receiptDocNo.MonthPeriod = bln;
                    receiptDocNo.YearPeriod = thn;
                }
                else
                {
                    noBaru = receiptDocNo.NoUrut + 1;
                    //receiptDocNo.ReceiptCode = txtPONo.Text.Substring(0, 2);
                    receiptDocNo.ReceiptCode = kd;
                    receiptDocNo.NoUrut = receiptDocNo.NoUrut + 1;
                }
            }

            POPurchnFacade poPurchnFacade = new POPurchnFacade();
            POPurchn poPurchn = poPurchnFacade.RetrieveByNo(txtPONo.Text);
            if (poPurchnFacade.Error == string.Empty && poPurchn.ID > 0)
            {
                receipt.SupplierType = 0;
                receipt.SupplierID = poPurchn.SupplierID;
                receipt.PoID = poPurchn.ID;
                receipt.PoNo = poPurchn.NoPO;
            }

            receipt.ReceiptNo = txtMrsNo.Text;
            receipt.ReceiptDate = DateTime.Parse(txtTanggal.Text);
            //2 for RMS
            receipt.ReceiptType = 3;
            receipt.SupplierType = 0;
            receipt.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            receipt.Status = 0;
            receipt.CreatedBy = ((Users)Session["Users"]).UserName;
            receipt.ItemTypeID = 1;

            receipt.tipeAsset = 0;

            string strError = string.Empty;
            ArrayList arrReceiptDetail = new ArrayList();
            if (Session["ListOfReceiptDetail"] != null)
                arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];

            ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(receipt, arrReceiptDetail, receiptDocNo);
            if (receipt.ID > 0)
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
                // update status pada po
                strError = receiptProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtMrsNo.Text = receiptProcessFacade.ReceiptNo;
                    Session["id"] = receipt.ID;
                    Session["ReceiptNo"] = receiptProcessFacade.ReceiptNo;
                }
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                //txtCariOP.ReadOnly = false;

                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                btnPrint.Disabled = false;
                if (strEvent == "Edit")
                    clearForm();

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                if (btnCancel.Enabled == false)
                    return;

                string strDocumentNo = string.Empty;
                int intReceiptDetailID = 0;
                string strEvent = string.Empty;

                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrTransferDetail = new ArrayList();
                arrTransferDetail = (ArrayList)Session["ListOfReceiptDetail"];

                Receipt receipt = new Receipt();
                if (Session["id"] != null)
                {
                    // masuk sini krn sudah di save
                    // next job
                    // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                    int id = (int)Session["id"];
                    int flagPO = 0;

                    ReceiptFacade receiptFacade = new ReceiptFacade();
                    Receipt rcp = receiptFacade.RetrieveByNo(txtMrsNo.Text);
                    if (receiptFacade.Error == string.Empty && rcp.ID > 0)
                    {
                        if (rcp.Status > 0)
                        {
                            DisplayAJAXMessage(this, "Tidak dapat dihapus karena dalam proses pembayaran");
                            return;
                        }
                        else
                        {
                            POPurchnFacade poPurchnFacade = new POPurchnFacade();
                            POPurchn poPurchn = poPurchnFacade.RetrieveByID(rcp.PoID);
                            if (poPurchnFacade.Error == string.Empty && poPurchn.ID > 0)
                            {
                                if (poPurchn.Status == -2)
                                {
                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Close");
                                    return;
                                }
                                else if (poPurchn.Status == -1)
                                {
                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Cancel");
                                    return;
                                }
                                else if (poPurchn.Status == 3)
                                {
                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO dalam pengajuan Pembayaran");
                                    return;
                                }
                                else if (poPurchn.Status == 4)
                                {
                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO dalam pembuatan Giro");
                                    return;
                                }
                                else if (poPurchn.Status == 5)
                                {
                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Bayar");
                                    return;
                                }
                                else
                                    flagPO = poPurchn.Status;

                            }

                            int i = 0;
                            int x = 0;
                            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                            ArrayList arrCurrentReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(rcp.ID);
                            if (receiptDetailFacade.Error == string.Empty)
                            {
                                ReceiptDetail receiptDetail1 = (ReceiptDetail)arrTransferDetail[index];
                                bool valid = false;
                                int poDetailID = 0;

                                foreach (ReceiptDetail rcpDetail in arrCurrentReceiptDetail)
                                {
                                    if (rcpDetail.PODetailID == receiptDetail1.PODetailID && rcpDetail.PODetailID == receiptDetail1.PODetailID)
                                    {
                                        // cek stok
                                        InventoryFacade inventoryFacade = new InventoryFacade();
                                        Inventory inv = inventoryFacade.RetrieveById(rcpDetail.ItemID);
                                        if (inventoryFacade.Error == string.Empty)
                                        {
                                            if (inv.Jumlah - receiptDetail1.Quantity < 0)
                                            {
                                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
                                                return;
                                            }
                                        }

                                        poDetailID = rcpDetail.ID;
                                        i = x;
                                        POPurchn cekSisaPO = poPurchnFacade.CekSisaPO(rcp.PoNo);
                                        if (cekSisaPO.ID > 0 && (cekSisaPO.QtyReceipt - rcpDetail.Quantity) == 0)
                                        {
                                            flagPO = 0;
                                        }

                                        valid = true;
                                        break;
                                    }

                                    x = x + 1;
                                }

                                if (valid == false)
                                {
                                    arrTransferDetail.RemoveAt(index);
                                    Session["ListOfReceiptDetail"] = arrTransferDetail;
                                    GridView1.DataSource = arrTransferDetail;
                                    GridView1.DataBind();
                                }
                                else
                                {
                                    ReceiptDetail receiptDetail = (ReceiptDetail)arrTransferDetail[index];
                                    ArrayList arrReceiptDetail = new ArrayList();
                                    foreach (ReceiptDetail rd in arrTransferDetail)
                                    {
                                        if (rd.ID == poDetailID)
                                        {
                                            //((OPDetail)arrOPDetail[i]).Point = pointX;
                                            ((ReceiptDetail)arrTransferDetail[i]).FlagPO = flagPO;
                                            arrReceiptDetail.Add(rd);
                                        }
                                    }

                                    ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(new Receipt(), arrReceiptDetail, new ReceiptDocNo());
                                    //intPODetailID = receiptDetail.PODetailID;
                                    intReceiptDetailID = receiptDetail.ID;

                                    string strError = receiptProcessFacade.CancelReceiptDetail();
                                    if (strError != string.Empty)
                                    {
                                        DisplayAJAXMessage(this, strError);
                                        return;
                                    }
                                    ArrayList arrTransfer = new ArrayList();

                                    foreach (ReceiptDetail rd in arrTransferDetail)
                                    {
                                        //if (rd.DocumentNo != strDocumentNo)
                                        if (rd.ID != intReceiptDetailID)
                                        {
                                            arrTransfer.Add(rd);
                                        }
                                    }

                                    strEvent = "Hapus per Baris";
                                    InsertLog(strEvent);

                                    Session["ListOfReceiptDetail"] = arrTransfer;

                                    GridView1.DataSource = arrTransfer;
                                    GridView1.DataBind();
                                }
                            }
                        }
                    }
                }
                else
                {
                    arrTransferDetail.RemoveAt(index);
                    Session["ListOfReceiptDetail"] = arrTransferDetail;

                    GridView1.DataSource = arrTransferDetail;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[3].Text = Convert.ToDecimal(e.Row.Cells[3].Text).ToString("N2");
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

            Response.Redirect("ListReceiptMRS.aspx?approve=" + kd);
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                // masuk sini krn sudah di save
                // next job
                // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                int id = (int)Session["id"];
                int flagPO = 0;
                string strEvent = string.Empty;

                ReceiptFacade receiptFacade = new ReceiptFacade();
                Receipt rcp = receiptFacade.RetrieveByNo(txtMrsNo.Text);
                if (receiptFacade.Error == string.Empty && rcp.ID > 0)
                {
                    if (rcp.Status > 0)
                    {
                        DisplayAJAXMessage(this, "Tidak dapat dihapus karena dalam proses pembayaran");
                        return;
                    }
                    else
                    {
                        if (Session["AlasanCancel"] != null)
                        {
                            rcp.AlasanCancel = Session["AlasanCancel"].ToString();
                            Session["AlasanCancel"] = null;
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong / blank");
                            return;
                        }

                        POPurchnFacade poPurchnFacade = new POPurchnFacade();
                        POPurchn poPurchn = poPurchnFacade.RetrieveByID(rcp.PoID);
                        if (poPurchnFacade.Error == string.Empty && poPurchn.ID > 0)
                        {
                            if (poPurchn.Status == -2)
                            {
                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Close");
                                return;
                            }
                            else if (poPurchn.Status == -1)
                            {
                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Cancel");
                                return;
                            }
                            else if (poPurchn.Status == 3)
                            {
                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO dalam pengajuan Pembayaran");
                                return;
                            }
                            else if (poPurchn.Status == 4)
                            {
                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO dalam pembuatan Giro");
                                return;
                            }
                            else if (poPurchn.Status == 5)
                            {
                                DisplayAJAXMessage(this, "Tidak dapat dihapus karena PO sudah di-Bayar");
                                return;
                            }
                            else
                            {
                                flagPO = 1;
                            }
                        }

                        ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                        ArrayList arrCurrentReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(rcp.ID);
                        if (receiptDetailFacade.Error == string.Empty)
                        {

                            ArrayList arrReceiptDetail = new ArrayList();
                            foreach (ReceiptDetail rcpDetail in arrCurrentReceiptDetail)
                            {
                                InventoryFacade inventoryFacade = new InventoryFacade();
                                Inventory inv = inventoryFacade.RetrieveById(rcpDetail.ItemID);
                                if (inventoryFacade.Error == string.Empty)
                                {
                                    if (inv.Jumlah - rcpDetail.Quantity < 0)
                                    {
                                        DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
                                        return;
                                    }
                                }

                                arrReceiptDetail.Add(rcpDetail);
                            }

                            ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(rcp, arrReceiptDetail, new ReceiptDocNo());

                            string strError = receiptProcessFacade.CancelReceipt();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, strError);
                                return;
                            }

                            else
                            {
                                strEvent = "Cancel All";
                                InsertLog(strEvent);

                                DisplayAJAXMessage(this, "Cancel berhasil .....");
                                clearForm();
                            }

                        }
                    }
                }
            }

        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Office Receipt";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtMrsNo.Text;
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
            //if (ValidateExpedisi() != string.Empty)
            //    return ValidateExpedisi();

            ArrayList arrReceiptDetail = new ArrayList();
            if (Session["ListOfReceiptDetail"] != null)
            {
                arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];
                if (arrReceiptDetail.Count == 0)
                    return "Tidak List Item yang di-input";
            }

            return string.Empty;
        }

        //private string ValidateExpedisi()
        //{
        //if (ddlExpedisiName.SelectedIndex == 0)
        //    return "Pilih Expedisi";
        //else if (ddlCarType.SelectedIndex == 0)
        //    return "Pilih Tipe Mobil";

        //    return string.Empty;
        //}

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                POPurchnDetail poPurchnDetail = poPurchnDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
                if (poPurchnDetailFacade.Error == string.Empty)
                {
                    decimal sisaPO = 0;

                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    //decimal cekJumlah = receiptDetailFacade.SumPOReceiptDetail(poPurchnDetail.ItemID, txtPONo.Text);
                    decimal cekJumlah = receiptDetailFacade.SumPOReceiptDetail(poPurchnDetail.ItemID, txtPONo.Text, poPurchnDetail.ID);
                    if (receiptDetailFacade.Error == string.Empty && cekJumlah > 0)
                    {
                        sisaPO = poPurchnDetail.Qty - cekJumlah;
                        txtQty.Text = sisaPO.ToString("N2");
                        txtQtyTerima.Text = sisaPO.ToString("N2");
                    }
                    else
                    {
                        txtQty.Text = poPurchnDetail.Qty.ToString("N2");
                        txtQtyTerima.Text = poPurchnDetail.Qty.ToString("N2");
                    }

                    txtItemCode.Text = poPurchnDetail.ItemCode;
                    txtUom.Text = poPurchnDetail.UOMCode;
                    txtStok.Text = poPurchnDetail.Stok.ToString("N2");
                    txtNoSpp.Text = poPurchnDetail.DocumentNo;
                    txtPrice.Text = poPurchnDetail.Price.ToString("N2");

                    //tunggu SPP utk ambil SPPNo
                }

                if (Session["ListOfReceiptDetail"] != null)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    if (Session["ListOfReceiptDetail"] != null)
                        arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];
                    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    {
                        if (receiptDetail.NoPO == txtPONo.Text && receiptDetail.ItemCode == txtItemCode.Text && receiptDetail.PODetailID == poPurchnDetail.ID)
                        {
                            txtQty.Text = (decimal.Parse(txtQty.Text) - receiptDetail.Quantity).ToString("N2");
                            txtQtyTerima.Text = (decimal.Parse(txtQtyTerima.Text) - receiptDetail.Quantity).ToString("N2");
                        }
                    }

                }
            }
        }

        private void clearQty()
        {
            txtItemCode.Text = string.Empty;
            txtQtyTerima.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtUom.Text = string.Empty;
            txtNoSpp.Text = string.Empty;
            txtPrice.Text = string.Empty;

            ddlItemName.SelectedIndex = 0;
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            try
            {
                decimal cekNumber = decimal.Parse(txtQtyTerima.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Terima harus number");
                return;
            }

            if (decimal.Parse(txtQtyTerima.Text) == 0)
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
            if (decimal.Parse(txtQty.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                return;
            }
            if (decimal.Parse(txtQtyTerima.Text) > decimal.Parse(txtQty.Text))
            {
                DisplayAJAXMessage(this, "Quantity Terima lebih besar");
                return;
            }

            string strReceiptCode = txtPONo.Text.Substring(0, 2) + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            #region check last date transaksi atau closing periode
            //int receiptType = 1 = MRS
            Receipt receipt = new Receipt();
            ReceiptFacade receiptFacade = new ReceiptFacade();
            Receipt lastEntry = receiptFacade.CekLastDate(strReceiptCode);
            DateTime lastTgl = new DateTime(lastEntry.ReceiptDate.Year, lastEntry.ReceiptDate.Month, lastEntry.ReceiptDate.Day);
            /**
             * check closing periode
             */
            AccClosingFacade Cls = new AccClosingFacade();
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            if (Cls.CheckClosing(Bulan, Tahun) != string.Empty)
            {
                DisplayAJAXMessage(this, "Periode " + DateTime.Parse(txtTanggal.Text).Month + "/" + DateTime.Parse(txtTanggal.Text).Year + " sudah di close by Accounting");
            }
            else
            {
                if (nowTgl < lastTgl && PurchnConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    return;
                }

            }

            //cek lastdate mrs
            #endregion

            ReceiptDetail receiptDetail = new ReceiptDetail();
            ArrayList arrListReceiptDetail = new ArrayList();

            if (Session["ListOfReceiptDetail"] != null)
            {
                arrListReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];
                if (arrListReceiptDetail.Count > 0)
                {
                    int ada = 0;
                    foreach (ReceiptDetail rcp in arrListReceiptDetail)
                    {

                        if (rcp.NoPO == txtPONo.Text && rcp.ItemCode == txtItemCode.Text && rcp.SppNo == txtNoSpp.Text && rcp.Price == decimal.Parse(txtPrice.Text))
                        {
                            DisplayAJAXMessage(this, "Sudah ada di-tabel untuk No SPP tersebut (with same price)");

                            clearQty();
                            return;
                        }

                        ada = ada + 1;
                    }
                }
            }

            receiptDetail.NoPO = txtPONo.Text;
            receiptDetail.ItemCode = txtItemCode.Text;
            receiptDetail.ItemName = ddlItemName.SelectedItem.ToString();
            receiptDetail.UOMCode = txtUom.Text;
            receiptDetail.Quantity = decimal.Parse(txtQtyTerima.Text);
            receiptDetail.RowStatus = 0;
            receiptDetail.Kadarair = 0;
            receiptDetail.Keterangan = txtKeterangan.Text;

            POPurchnDetailFacade poPurchDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail poPurchnDetail = poPurchDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
            if (poPurchDetailFacade.Error == string.Empty && poPurchnDetail.ID > 0)
            {
                receiptDetail.Price = poPurchnDetail.Price;
                receiptDetail.ItemID = poPurchnDetail.ItemID;
                receiptDetail.TotalPrice = poPurchnDetail.Price * decimal.Parse(txtQtyTerima.Text);
                receiptDetail.Disc = 0;
                receiptDetail.SppID = poPurchnDetail.SPPID;
                receiptDetail.SppNo = poPurchnDetail.DocumentNo;
                receiptDetail.PoID = poPurchnDetail.POID;
                receiptDetail.PoNo = txtPONo.Text;
                receiptDetail.UomID = poPurchnDetail.UOMID;
                receiptDetail.PODetailID = poPurchnDetail.ID;
                receiptDetail.GroupID = poPurchnDetail.GroupID;
                receiptDetail.ItemTypeID = poPurchnDetail.ItemTypeID;
                receiptDetail.ItemID2 = poPurchnDetail.ItemID2;

                receiptDetail.TipeAsset = 0;
            }

            arrListReceiptDetail.Add(receiptDetail);
            Session["ListOfReceiptDetail"] = arrListReceiptDetail;

            GridView1.DataSource = arrListReceiptDetail;
            GridView1.DataBind();

            clearQty();

        }
        public int PurchnConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn status = purchn.PurchnTools(ModulName);
            return status.Status;
        }
    }
}