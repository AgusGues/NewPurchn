using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using System.Collections;
using Cogs;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormReceiptRMS : System.Web.UI.Page
    {
        public enum Status
        {
            Open = 0,
            Parsial = 1,
            Receipt = 2,
            PrePayment = 3,
            CreateGiro = 4,
            Release = 5,
            HiblowCapacity = 25000,
            //ReceiptType = 2 = RMS = KP & KM
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string[] InputDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputReceipt", "Receipt").Split(',');
            string ParsialDeliveryAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ScheduleAktif", "MemoHarian");
            string[] UserRePrint = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Reprint", "Receipt").Split(',');
            int usrPrint = Array.IndexOf(UserRePrint, ((Users)Session["Users"]).UserID.ToString());
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                Session["AlasanCancel"] = null;
                Session["AlasanBatal"] = null;
                Session["AlasanTolak"] = null;

                if (Request.QueryString["ReceiptNo"] != null)
                {
                    LoadReceipt(Request.QueryString["ReceiptNo"].ToString());
                }
                else
                {
                    clearForm();
                    txtCariOP.Enabled = true;
                }
                //btnCancel.Attributes.Add("onclick", "return confirm_delete();");
                int pos = Array.IndexOf(InputDept, ((Users)Session["Users"]).DeptID.ToString());
                lbAddOP.Enabled = (pos != -1) ? true : false;
                txtCariOP.Enabled = (pos != -1) ? true : false;
            }
            else
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string kd;
                if (RadioBB.Checked == true)
                {
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
                }
                else if (RadioBS.Checked == true)
                {
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "X";
                }
                else
                {
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
                }

                if (txtCariOP.Text != string.Empty && (txtCariOP.Text.Substring(1, 1).ToUpper() == "I" || txtCariOP.Text.Substring(1, 1).ToUpper() == "P" || txtCariOP.Text.Substring(1, 1).ToUpper() == "M"))
                {
                    Session["SupID"] = null;
                    POPurchnFacade poPurchnFacade = new POPurchnFacade();
                    Domain.POPurchn po = poPurchnFacade.RetrieveByNoCheckStatus(txtCariOP.Text);
                    SuppPurch supplier = new SuppPurch();
                    SuppPurchFacade supplierF = new SuppPurchFacade();
                    supplier = supplierF.RetrieveById(po.SupplierID);
                    txtPOTipe.Value = po.Delivery.ToUpper();
                    string[] arrSupplier = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
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
                        txtSupplier.Text = supplier.SupplierName.Trim();
                        Session["SupID"] = supplier.ID.ToString();
                        txtSubCompanyID.Value = po.SubCompanyID.ToString();
                        POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                        ArrayList arrPurchnDetail = poPurchnDetailFacade.RetrieveById(po.ID);
                        if (poPurchnDetailFacade.Error == string.Empty)
                        {
                            RadioBB.Enabled = false;
                            RadioBP.Enabled = false;
                            ddlItemName.Items.Clear();
                            ddlItemName.Items.Add(new ListItem("-- Pilih Item --", "0"));
                            foreach (POPurchnDetail purchnDetail in arrPurchnDetail)
                            {
                                if (RadioBB.Checked == true)
                                {
                                    if (purchnDetail.GroupID == 1)
                                        ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                                }
                                else if (RadioBS.Checked == true)
                                {
                                    if (purchnDetail.GroupID == 11)
                                    {
                                        ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                                    }
                                }
                                else
                                {
                                    if (purchnDetail.GroupID == 2)
                                        ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                                }

                                //ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName + " - " + purchnDetail.DocumentNo, purchnDetail.ID.ToString()));
                            }
                        }
                    }
                    else
                    {
                        if (txtCariOP.Text.Substring(1, 1).ToUpper() == "I" ||
                            txtCariOP.Text.Substring(1, 1).ToUpper() == "M" ||
                            txtCariOP.Text.Substring(1, 1).ToUpper() == "P")
                        {
                            Domain.POPurchn cekPo = poPurchnFacade.RetrieveByNo(txtCariOP.Text);
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
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Bukan No PO untuk RMS ...!");
                            clearForm();
                            return;
                        }
                    }
                    //tambahan untuk aktifkan qty timbang BPAS
                    txtCariOP.Text = string.Empty;
                    txtQtyTimbang.Text = "0";
                    txtQtyTimbang.Visible = (arrSupplier.Contains(txtSupplier.Text)) ? true : false;
                    spl.Visible = (arrSupplier.Contains(txtSupplier.Text)) ? true : false;

                }
            }
        }

        protected void ddlToPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlToPlan.SelectedIndex > 0)
            {
                #region depreciated line
                //utk CTRP            
                //if (((Users)Session["Users"]).UnitKerjaID == 1 || ((Users)Session["Users"]).UnitKerjaID == 7)
                //{
                //    if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 1)
                //    {
                //        InventoryFacade inventoryFacade = new InventoryFacade();
                //        Inventory inv = inventoryFacade.RetrieveByCode("012019004030000");
                //        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                //        {
                //            txtStok.Text = inv.Jumlah.ToString("N2");
                //        }
                //    }
                //    else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 2)
                //    {
                //        InventoryFacade inventoryFacade = new InventoryFacade();
                //        Inventory inv = inventoryFacade.RetrieveByCode("012019005040000");
                //        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                //        {
                //            txtStok.Text = inv.Jumlah.ToString("N2");
                //        }
                //    }
                //    else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 3)
                //    {

                //        InventoryFacade inventoryFacade = new InventoryFacade();
                //        Inventory inv = inventoryFacade.RetrieveByCode("012019006050000");
                //        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                //        {
                //            txtStok.Text = inv.Jumlah.ToString("N2");
                //        }
                //    }

                //    //utk KALSIUM KARBONATE 150 MESH CURAH
                //    else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 1)
                //    {
                //        InventoryFacade inventoryFacade = new InventoryFacade();
                //        Inventory inv = inventoryFacade.RetrieveByCode("011047004000200");
                //        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                //        {
                //            txtStok.Text = inv.Jumlah.ToString("N2");
                //        }
                //    }
                //    else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 2)
                //    {
                //        InventoryFacade inventoryFacade = new InventoryFacade();
                //        Inventory inv = inventoryFacade.RetrieveByCode("011047007000200");
                //        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                //        {
                //            txtStok.Text = inv.Jumlah.ToString("N2");
                //        }
                //    }
                //    else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 3)
                //    {
                #endregion
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inv = inventoryFacade.RetrieveByCode(ddlToPlan.SelectedValue.ToString());
                if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtStok.Text = inv.Jumlah.ToString("N2");
                    txtKadarAir.Text = "0";
                }
                //}
                // }

                //DeptFacade deptFacade = new DeptFacade();
                //Dept dept = deptFacade.RetrieveById(int.Parse(ddlToPlan.SelectedValue));
                //if (deptFacade.Error == string.Empty && dept.ID > 0)
                //{
                //    txtKodeDept.Text = dept.DeptCode;

                //    txtCariNamaBrg.Focus();
                // }
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?ReceiptNo=" + txtSearch.Text);
            //LoadReceipt(txtSearch.Text);
        }
        private void LoadReceipt(string strReceiptNo)
        {
            try
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();

                string forReceipt = string.Empty;
                forReceipt = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
                clearForm();
                string[] UserRePrint = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Reprint", "Receipt").Split(',');
                int usrPrint = Array.IndexOf(UserRePrint, ((Users)Session["Users"]).UserID.ToString());

                ReceiptFacade receiptFacade = new ReceiptFacade();
                Receipt receipt = receiptFacade.RetrieveByNoWithStatus(strReceiptNo, forReceipt);
                if (receiptFacade.Error == string.Empty && receipt.ID > 0)
                {
                    Session["id"] = receipt.ID;
                    Session["ReceiptID"] = "";
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
                    Session["ReceiptID"] = " and A.RowStatus >-3 and A.ReceiptID=" + receipt.ID;
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

                    lstRMS.DataSource = arrListScheduleDetail;
                    lstRMS.DataBind();
                    lbAddOP.Enabled = false;
                    //DisableForm();
                    string[] arrDeptDelete = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeleteReceipt", "Receipt").Split(',');
                    txtCariOP.Enabled = false;
                    btnCancel.Enabled = (arrDeptDelete.Contains(((Users)Session["Users"]).DeptID.ToString()) && receipt.Status == 0 && ((Users)Session["Users"]).Apv > 0) ? true : false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = (receipt.Status == 0) ? false : true;
                    btnReprint.Visible = (receipt.Status == 0 && CheckStatusCetak(receipt.ID) == 0 && usrPrint > -1) ? true : false;

                    lbAddOP.Enabled = false;
                    Session["ReceiptID"] = "";
                    //parsial delivery

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DisplayAJAXMessage(this, error);
                return;
            }
        }
        private void LoadScheduleBySession()
        {
            DisplayAJAXMessage(this, "Not yet");
        }
        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfReceiptDetail"] = new ArrayList();
            Session["Receipt"] = null;
            Session["ReceiptNo"] = null;
            Session["SupID"] = null;
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
            txtCariOP.Enabled = true;
            RadioBB.Enabled = true;
            RadioBP.Enabled = true;
            ddlSjLapak.Items.Clear();
            forLapak.Visible = false;
            forLine.Visible = true;
            forAgenJabar.Visible = false;
            txtNOPOL.Text = string.Empty;
            txtNoSJ.Text = string.Empty;
            ArrayList arrList = new ArrayList();
            lstRMS.DataSource = arrList;
            lstRMS.DataBind();
            txtTanggal.Enabled = true;
            txtKadarAir.Text = "0";
            txtSupplier.Text = string.Empty;
            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = true;
            arrList.Add(new ReceiptDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            GrdDynamic.Visible = false;
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {

            Session.Remove("ReceiptNo");
            Session.Remove("ListOfReceiptDetail");
            Session.Remove("id");
            Session.Remove("Receipt");
            Session["Lapak"] = null;
            Session["SupID"] = null;
            Session["aLapak"] = null;
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
                #region hidden line
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
                #endregion
                strEvent = "Edit";
            }
            //here for document number
            #region process Normal
            ReceiptDocNoFacade receiptDocNoFacade = new ReceiptDocNoFacade();
            ReceiptDocNo receiptDocNo = new ReceiptDocNo();

            if (strEvent == "Insert")
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string kd = string.Empty;
                if (RadioBB.Checked == true)
                {
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
                }
                else
                {
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
                }

                receiptDocNo = receiptDocNoFacade.RetrieveByReceiptCode(bln, thn, kd);
                if (receiptDocNo.ID == 0)
                {
                    noBaru = 1;
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
            Domain.POPurchn poPurchn = poPurchnFacade.RetrieveByNo(txtPONo.Text);
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
            receipt.ReceiptType = 2;
            receipt.SupplierType = 0;
            receipt.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            receipt.Status = 0;
            receipt.CreatedBy = ((Users)Session["Users"]).UserName;
            receipt.ItemTypeID = 1;
            //receipt.SubCompanyID = int.Parse(txtSubCompanyID.Value);
            string strError = string.Empty;
            ArrayList arrReceiptDetail = new ArrayList();
            if (Session["ListOfReceiptDetail"] != null)
                arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];
            #endregion
            ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(receipt, arrReceiptDetail, receiptDocNo);
            if (receipt.ID > 0)
            {
                #region hidden line
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
                #endregion
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
                    btnPrint.Disabled = false;
                    #region Proses Poin Lapak
                    /**
                     * Process Kirim Data ke agen Lapak Jika Material Kertas Kantong Semen
                     * added on 22-09-2015
                     */
                    POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
                    string AgenLapakAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AgenLapakAktif", "Receipt");
                    string[] arrKertasCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KertasItemCode", "Receipt").Split(',');
                    if (arrKertasCode.Contains(txtItemCode2.Text) && AgenLapakAktif == "1")
                    {
                        string rst = string.Empty;
                        string SJNo = "";
                        int Resulted = 0;
                        //jika status sudah terkirim dari Proses Approval PO level manager
                        // jangan di lakukan kirim
                        foreach (POPurchnDetail ppd in pOPurchnDetailFacade.RetrieveItemPOID(poPurchn.ID))
                        {
                            KadarAir ka = pOPurchnDetailFacade.RetrieveKAData(poPurchn.ID.ToString(), ppd.ID.ToString(), true);
                            if (ka.PointStatus > 0) { goto PosisiUpdate; }
                            if (forAgenJabar.Visible == true)
                            {
                                if (Session["aLapak"] != null)
                                {
                                    ArrayList arrLpk = (ArrayList)Session["aLapak"];
                                    bpas_api.WebService1 api = new bpas_api.WebService1();
                                    foreach (Receipt rcd in arrLpk)
                                    {
                                        string Tanggal = rcd.ReceiptDate.ToString("yyyyMMdd");
                                        SJNo = rcd.NoSJ.ToString();
                                        /**
                                         * data kirim ke table Agen_DtKirimKeBPAS - server ho
                                         * Tanggal,NoMobil,No.SJ,Qty,AgenID,ID for Output,UserUnit Kerja,CreatedBy
                                         */
                                        Resulted = api.InsertKirimKeBPAS(
                                              Tanggal,
                                              rcd.NoPOL.ToString().ToUpper(),
                                              rcd.NoSJ.ToString().ToUpper(),
                                              rcd.Quantity,
                                              rcd.AgenID.ToString(),
                                              receipt.ID.ToString(),
                                              ((Users)Session["Users"]).UnitKerjaID.ToString(),
                                              rcd.CreatedBy.ToString());
                                        if (Resulted > 0)
                                        {
                                            Receipt rcg = new Receipt();
                                            rcg = new ReceiptFacade().RetrieveByNo(receiptProcessFacade.ReceiptNo);
                                            rst = api.UpdateKiriman(
                                                Resulted.ToString(),
                                                rcg.ID.ToString(),
                                                rcd.Quantity,
                                                ((Users)Session["Users"]).UnitKerjaID.ToString()
                                                );
                                            int rsult = new ReceiptFacade().UpdateSJNumber(rcg.ID, SJNo);
                                            int rste = api.UpdateNoSJ(rcd.AgenID.ToString());
                                            //update data point status di table POPurchnKadarAir
                                            KadarAir kaUPD = new KadarAir();
                                            kaUPD.NoSJ = ka.NoSJ = SJNo;// sjNumber.ToString().PadLeft(6, '0') + "/" + AgenCode(txtLapakID.Text) + "/SJ/" + bln + "/" + thn;
                                            kaUPD.PointStatus = Resulted;
                                            kaUPD.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                            int rs = pOPurchnDetailFacade.UpdateStatusPoint(ka.ID, kaUPD);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (Session["Lapak"] != null)
                                {

                                    ArrayList arrData = (ArrayList)Session["Lapak"];
                                    foreach (Receipt r in arrData)
                                    {
                                        this.IDLapak = r.KirimID.ToString();
                                        string AgenID = r.AgenID.ToString();
                                        string NoPol = r.NoPOL;
                                        SJNo = r.NoSJ;
                                        this.Qty = r.Quantity.ToString("###,##0.00");
                                    }
                                    //this.IDLapak = txtLapakID.Text;
                                    Receipt rc = new Receipt();
                                    rc = new ReceiptFacade().RetrieveByNo(receiptProcessFacade.ReceiptNo);
                                    bpas_api.WebService1 bpas = new bpas_api.WebService1();
                                    this.PlanID = ((Users)Session["Users"]).UnitKerjaID.ToString();
                                    //this.Qty = rc.Quantity.ToString();
                                    this.ReceiptNo = rc.ID.ToString();
                                    string ID = this.IDLapak;
                                    decimal Jumlah = decimal.Parse(this.Qty);
                                    string Plant = decimal.Parse(this.PlanID).ToString();
                                    string ReceiptID = this.ReceiptNo;
                                    rst = bpas.UpdateKiriman(ID, ReceiptID, Jumlah, PlanID);
                                    SJNo = (SJNo == string.Empty) ? this.GetSJfromPointLapak(int.Parse(ReceiptID)) : SJNo;
                                    int rsult = new ReceiptFacade().UpdateSJNumber(int.Parse(ReceiptID), SJNo);
                                }
                            }
                        }
                        if (rst == "Record Updated")
                        {
                            //string rs = bpas.InsertSMS(AgenID, Jumlah, "SmsPengirimanAgen");
                            txtLapakID.Text = string.Empty;
                            txtItemCode2.Text = string.Empty;
                            /** 
                             * Catat Log event
                             * Untuk melakukan deteksi data terupdate atau tidak
                             */
                            Session["Lapak"] = null;
                            Session["aLapak"] = null;
                        }
                    #endregion
                    PosisiUpdate:
                        #region
                        EventLog eventLog = new EventLog();
                        eventLog.ModulName = "Receipt Kantong Semen";
                        eventLog.EventName = "Update Lapak " + SJNo + " :" + rst;
                        eventLog.DocumentNo = txtMrsNo.Text;
                        eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

                        EventLogFacade eventLogFacade = new EventLogFacade();
                        int intResult = eventLogFacade.Insert(eventLog);
                        #endregion

                    }

                    //Tracking process
                    ddlItemName.Items.Clear();
                    //txtCariOP.ReadOnly = false;

                    InsertLog(strEvent);
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                    if (strEvent == "Edit")
                        clearForm();
                }
            }

        }
        protected void txtNOPOL_Change(object sender, EventArgs e)
        {
            lbAddOP.Enabled = (txtNOPOL.Text.Length > 5) ? true : false;
            if (txtNOPOL.Text.Length < 6)
            {
                DisplayAJAXMessage(this, "Nomor Kendaraan tidak lengkap");
                return;
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
                            Domain.POPurchn poPurchn = poPurchnFacade.RetrieveByID(rcp.PoID);
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
                                        Domain.POPurchn cekSisaPO = poPurchnFacade.CekSisaPO(rcp.PoNo);
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
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";

            Response.Redirect("ListReceiptMRS.aspx?approve=" + kd);
            //Response.Redirect("ListReceiptMRS.aspx?approve=KP");
        }


        protected void btnUpdateClose_ServerClick(object sender, EventArgs e)
        {

        }
        protected void btnUpdateAlasan_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel.Text;
            Session["AlasanBatal"] = txtAlasanCancel.Text;
            Session["AlasanTolak"] = txtAlasanCancel.Text;

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
                #region Check Status Closing
                /**
                         * check closing periode saat ini
                         * added on 13-08-2014
                         */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtTanggal.Text).Year;
                int Bulan = DateTime.Parse(txtTanggal.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Purchn");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing.Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion

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
                        Domain.POPurchn poPurchn = poPurchnFacade.RetrieveByID(rcp.PoID);
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
                                strEvent = "Cancel All by " + ((Users)Session["Users"]).UserName;
                                InsertLog(strEvent);

                                DisplayAJAXMessage(this, "Cancel berhasil .....");
                                clearForm();
                            }

                        }
                    }
                }
            }
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            mpePopUp.Show();
            panEdit.Visible = true;

        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry RAW Receipt";
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
                    return "Tidak ada List Item yang di-input";
            }

            return string.Empty;
        }
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Lapak"] = null;
            if (ddlItemName.SelectedIndex > 0)
            {
                decimal xJml = 0;
                decimal xQRaw = 0;
                Session["JmlQtyReceipt"] = null;
                Session["JmlQtyPO"] = null;
                string[] arrItemTimbang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');

                POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                POPurchnDetail poPurchnDetail = poPurchnDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
                if (poPurchnDetailFacade.Error == string.Empty)
                {
                    decimal sisaPO = 0;
                    decimal cekJumlah = 0;
                    xJml = poPurchnDetail.Qty;
                    Session["JmlQtyPO"] = poPurchnDetail.Qty; ;

                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    string[] arrItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCode", "Receipt").Split(',');
                    int inItemCode = Array.IndexOf(arrItemCode, poPurchnDetail.ItemCode.ToString());
                    if ((((Users)Session["Users"]).UnitKerjaID == 1 || ((Users)Session["Users"]).UnitKerjaID == 7) && inItemCode > -1)
                        cekJumlah = receiptDetailFacade.SumPOReceiptDetailB(txtPONo.Text, poPurchnDetail.ID);
                    else
                        cekJumlah = receiptDetailFacade.SumPOReceiptDetail(poPurchnDetail.ItemID, txtPONo.Text, poPurchnDetail.ID);

                    if (receiptDetailFacade.Error == string.Empty && cekJumlah > 0)
                    {
                        decimal jml = (Session["JmlQtyReceipt"] != null) ? decimal.Parse(Session["JmlQtyReceipt"].ToString()) : 0;
                        sisaPO = poPurchnDetail.Qty - cekJumlah;
                        txtQty.Text = sisaPO.ToString("N2");
                        txtQtyTerima.Text = sisaPO.ToString("N2");
                    }
                    else
                    {
                        txtQty.Text = poPurchnDetail.Qty.ToString("N2");
                        txtQtyTerima.Text = poPurchnDetail.Qty.ToString("N2");
                    }

                    //lock po
                    string strSQL =
                        "select *, qtyPO-qtyreceipt sisa from ( " +
                        "select A.POPurchnDate, A.NoPO , B.ID,B.POID,B.Qty qtyPO,A.Delivery,(select SUM(quantity) from ReceiptDetail where  RowStatus>-1 and PODetailID=B.ID) qtyreceipt " +
                        "from POPurchnDetail B inner join POPurchn A on B.POID=A.ID where B.ItemID in (select ID from Inventory where ItemCode='" + poPurchnDetail.ItemCode + "')  " +
                        "and  B.Status>-1 )pr where qtyPO-qtyreceipt >0  and NoPO<'" + txtPONo.Text + "' and Delivery in (select Delivery from POPurchn where NoPO='" + txtPONo.Text + "') order by POPurchnDate ";
                    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
                    sqlCon.Open();
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
                    da.SelectCommand.CommandTimeout = 0;
                    #region Code for preparing the DataTable
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
                    dcol.AutoIncrement = true;
                    #endregion
                    //GrdDynamic.Visible = true;
                    //GrdDynamic.Columns.Clear();
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    //    BoundField bfield = new BoundField();
                    //    bfield.DataField = col.ColumnName;
                    //    bfield.HeaderText = col.ColumnName;
                    //    GrdDynamic.Columns.Add(bfield);
                    //}
                    //GrdDynamic.DataSource = dt;
                    //GrdDynamic.DataBind();
                    ////end lock PO
                    //if (dt.Rows.Count > 0)
                    //{
                    //    DisplayAJAXMessage(this, "masih ada PO lama yang belum terpenuhi");
                    //    txtPONo.Text = string.Empty;
                    //    ddlItemName.Items.Clear();
                    //    return;
                    //}
                    xQRaw = cekJumlah;
                    Session["JmlQtyReceipt"] = cekJumlah;
                    txtItemCode2.Text = poPurchnDetail.ItemCode;
                    txtItemCode.Text = poPurchnDetail.ItemCode;
                    txtUom.Text = poPurchnDetail.UOMCode;
                    txtStok.Text = poPurchnDetail.Stok.ToString("N2");
                    txtNoSpp.Text = poPurchnDetail.DocumentNo;
                    txtPrice.Text = poPurchnDetail.Price.ToString("N2");
                    txtItemID.Text = poPurchnDetail.ItemID.ToString();
                    #region Depreciated code
                    //utk CTRP            
                    //if (((Users)Session["Users"]).UnitKerjaID == 1 || ((Users)Session["Users"]).UnitKerjaID == 7)
                    //{
                    //    //utk semen curah
                    //    //if (txtItemCode.Text == "MT-BB-00040")
                    //    if (txtItemCode.Text == "012019003020000")
                    //    {
                    //        ddlToPlan.Items.Clear();
                    //        ddlToPlan.Items.Add(new ListItem("-- Pilih Plan --", "0"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 1 (MT-BB-00004)", "1"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 2 (MT-BB-00018)", "2"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 3 (MT-BB-00025)", "3"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 4 (MT-BB-00042)", "4"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 5 (012019001010100)", "5"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 1 (012019004030000)", "1"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 2 (012019005040000)", "2"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 3 (012019006050000)", "3"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 4 (012019007060000)", "4"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 5 (012019001010100)", "5"));
                    //    }
                    //    //utk kalsium
                    //    //else if (txtItemCode.Text == "MT-BB-00041")
                    //    else if (txtItemCode.Text == "011047003000200")
                    //    {
                    //        ddlToPlan.Items.Clear();
                    //        ddlToPlan.Items.Add(new ListItem("-- Pilih Plan --", "0"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 1 (MT-BB-00028)", "1"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 2 (MT-BB-00024)", "2"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 3 (MT-BB-00026)", "3"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 4 (MT-BB-00043)", "4"));
                    //        //ddlToPlan.Items.Add(new ListItem("Plan 5 (011045001010100)", "5"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 1 (011047004000200)", "1"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 2 (011047007000200)", "2"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 3 (011047008000200)", "3"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 4 (011047006000200)", "4"));
                    //        ddlToPlan.Items.Add(new ListItem("Plan 5 (011045001010100)", "5"));
                    //    }
                    //}
                    //until CTRP
                    #endregion
                    PilihPlan();
                    int inItemTimbang = Array.IndexOf(arrItemTimbang, poPurchnDetail.ItemCode);
                    spl.Visible = (inItemTimbang > -1) ? true : spl.Visible;
                    txtQtyTimbang.Visible = (inItemTimbang > -1) ? true : txtQtyTimbang.Visible;

                }

                if (Session["ListOfReceiptDetail"] != null)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    if (Session["ListOfReceiptDetail"] != null)
                        arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];

                    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    {
                        if (receiptDetail.NoPO == txtPONo.Text && receiptDetail.ItemCode == txtItemCode.Text &&
                            receiptDetail.PODetailID == poPurchnDetail.ID)
                        {
                            txtQty.Text = (decimal.Parse(txtQty.Text) - receiptDetail.Quantity).ToString("N2");
                            txtQtyTerima.Text = (decimal.Parse(txtQtyTerima.Text) - receiptDetail.Quantity).ToString("N2");
                        }
                    }

                }

                decimal toleransi20 = 0.2m;
                decimal toleransi50 = 0.5m;
                decimal x20 = xJml + (xJml * toleransi20);
                decimal x50 = xJml + (xJml * toleransi50);

                //011102037020000 kantong semen
                //Contoh dr foxpro
                //if (txtItemCode.Text == "MT-BB-00003" && x50 < xQRaw)
                if (txtItemCode.Text == "011102037020000" && x50 < xQRaw)
                {
                    DisplayAJAXMessage(this, "Jumlah di PO sudah habis");
                    return;
                }
                //else if (txtItemCode.Text != "MT-BB-00003" && x20 < xQRaw)
                else if (txtItemCode.Text != "011102037020000" && x20 < xQRaw)
                {
                    DisplayAJAXMessage(this, "Jumlah di PO sudah habis");
                    return;
                }
                //Load Schedule data
                //Check status schedule sudah diaktifkan atau belum
                string SchAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ScheduleAktif", "MemoHarian");
                string[] arrItemID = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)Session["Users"]).UnitKerjaID.ToString()).Split(',');
                int posx = Array.IndexOf(arrItemID, poPurchnDetail.ItemID.ToString());
                if (int.Parse(SchAktif) == 1 && posx > -1)// arrItemID.Contains(poPurchnDetail.ItemID.ToString()))
                {
                    MemoHarian.Visible = true;
                    LoadSchedule(poPurchnDetail.POID, int.Parse(poPurchnDetail.ItemID.ToString()));
                    ddlMemoHarian.Visible = true;
                }
                else
                {
                    ddlMemoHarian.Items.Clear();
                    ddlMemoHarian.Items.Add(new ListItem("--pilih--", "0"));
                }
                /**
                 * Agen Lapak Data input
                 */
                string AgenLapakAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AgenLapakAktif", "Receipt");
                string[] CodeKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KertasItemCode", "Receipt").Split(',');
                string Locked = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Locked", "Receipt");
                string AreaJabar = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AreaJabar", "Receipt");
                int pos = Array.IndexOf(CodeKertas, poPurchnDetail.ItemCode);
                if (pos != -1 && AgenLapakAktif == "1")
                {
                    /** check Agen Area Kirim
                     * Jika 2 (area Jabar ) lakukan proses lain
                     * bukan hanya area jabar aja tetapi semua on 07-01-2016
                     */
                    this.SupplierID = (Session["SupID"] != null) ? Session["SupID"].ToString() : "";
                    int AgenID = this.GetIDLapak();
                    txtLapakID.Text = this.GetIDLapak().ToString();
                    int AreaKirim = this.GetAreaKirim(AgenID.ToString());


                    forAgenJabar.Visible = true;
                    forLine.Visible = false;
                    forLapak.Visible = false;
                    lbAddOP.Enabled = (/*AreaJabar == "0" ||*/ Locked == "0") ? true : false;


                }
                else
                {
                    lbAddOP.Enabled = true;
                    forLine.Visible = true;
                    forLapak.Visible = false;
                    forAgenJabar.Visible = false;
                }
                /**
                 * GetKadarAir From PO
                 */
                POPurchnDetailFacade pd = new POPurchnDetailFacade();
                decimal KA = pd.GetKadarAir(poPurchnDetail.POID.ToString());
                txtKadarAir.Text = KA.ToString();
                ArrayList arrKA = pd.RetrieveKAData(poPurchnDetail.POID.ToString(), poPurchnDetail.ID.ToString());
                foreach (KadarAir kad in arrKA)
                {
                    txtNOPOL.Text = kad.NoPol.ToString().ToUpper();
                    txtNoSJ.Text = kad.NoSJ.ToString().ToUpper();
                    lbAddOP.Enabled = (kad.NoPol.Length > 5) ? true : false;
                }
            }
        }
        private void PilihPlan()
        {
            /**
                     * Update Dropdown plan
                     * 08-04-2014
                     * get invetory code where shortkey 'L'
                     */
            InventoryFacade inv = new InventoryFacade();
            ArrayList arrInv = new ArrayList();
            Inventory objInv = inv.RetrieveByCode(txtItemCode.Text);
            if (objInv.ShortKey == "S") //txtItemCode.Text == "012019003020000" semen curah
            {
                arrInv = inv.RetrieveByCriteria("ShortKey", "L");
            }
            else if (objInv.ShortKey == "K")
            {
                arrInv = inv.RetrieveByCriteria("ShortKey", "M"); //kalsium karbonat
            }
            ddlToPlan.Items.Clear();
            ddlToPlan.Items.Add(new ListItem("-- Pilih Plan --", "0"));
            int n = 0;
            foreach (Inventory nInv in arrInv)
            {
                n = n + 1;
                ddlToPlan.Items.Add(new ListItem(nInv.ItemCode + " " + nInv.ItemName, nInv.ItemCode.ToString()));
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
            ddlToPlan.SelectedIndex = 0;
            ddlItemName.SelectedIndex = 0;
            txtQtyTimbang.Text = string.Empty;
            ddlMemoHarian.Items.Clear();
            txtKadarAir.Text = "0";
            txtTimbAsli.Text = "0";
        }
        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            #region validasi data 
            try
            {
                decimal cekNumber = decimal.Parse(txtKadarAir.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Kadar Air harus number");
                return;
            }
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
                DisplayAJAXMessage(this, "Quantity harus number");
                return;
            }
            if (decimal.Parse(txtQty.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity PO tidak boleh 0");
                return;
            }
            string SchAktifE = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ScheduleAktif", "MemoHarian");
            string PODO = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POType", "MemoHarian");
            string[] arrItemCode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCode", "Receipt").Split(',');
            // string[] arrItemCode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)Session["Users"]).UnitKerjaID.ToString()).Split(',');
            int posx = Array.IndexOf(arrItemCode, txtItemCode.Text);
            if (int.Parse(SchAktifE) == 1 && posx > -1 && ddlMemoHarian.SelectedValue == "0")
            {
                //if (txtPOTipe.Value.Substring(0, 4) == PODO.ToUpper())
                //{
                DisplayAJAXMessage(this, "DO harus di pilih untuk material " + ddlItemName.SelectedItem);
                return;
                //}
            }

            if (ddlToPlan.SelectedIndex == 0 && (CheckShortKey(txtItemCode.Text) == "S" || CheckShortKey(txtItemCode.Text) == "K"))
            {
                DisplayAJAXMessage(this, "Untuk Kode Barang " + txtItemCode.Text + " (" + ddlItemName.SelectedItem.ToString() + "), Untuk Line, tidak boleh kosong");
                return;
            }
            #endregion
            string strReceiptCode = string.Empty;
            if (RadioBB.Checked == true)
                strReceiptCode = txtPONo.Text.Substring(0, 1) + "P" + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            else
                strReceiptCode = txtPONo.Text.Substring(0, 1) + "M" + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            //DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            #region check last date transaksi atau closing periode
            //int receiptType = 1 = MRS
            Receipt receipt = new Receipt();
            ReceiptFacade receiptFacade = new ReceiptFacade();
            Receipt lastEntry = receiptFacade.CekLastDate(strReceiptCode);
            //DateTime lastTgl = new DateTime(lastEntry.ReceiptDate.Year, lastEntry.ReceiptDate.Month, lastEntry.ReceiptDate.Day);
            DateTime lastTgl = lastEntry.ReceiptDate.AddDays(-4);
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
                if (nowTgl < lastTgl)
                {
                    //contoh dr foxpro
                    //if (txtItemCode.Text != "MT-BB-0003")
                    if (txtItemCode.Text != "011102037020000")
                    {
                        DisplayAJAXMessage(this, "Tgl. Raw Material Tidak Boleh Lebih Kecil dari Tgl. Raw Material Sebelumnya");
                        return;
                    }
                }

            }

            //cek lastdate mrs
            #endregion

            #region Toleransi Setting
            decimal t20 = 0.2M;
            decimal toleransi20 = 0.2m;
            decimal toleransi50 = 0.5m;

            //decimal toleransi20 = 20 / 100;
            //decimal toleransi50 = 50 / 100;

            decimal xPlus20 = 0;
            decimal xPlus50 = 0;

            decimal xTPlus20 = 0;
            decimal xTPlus50 = 0;
            /* WO no WO-IT-K0120121 menghilangkan toleransi penerimaan*/
            if (Session["JmlQtyReceipt"] != null)
            {
                if (Session["JmlQtyPO"] != null)
                {
                    //xPlus20 = decimal.Parse(Session["JmlQtyPO"].ToString()) * decimal.Parse(toleransi20.ToString());
                    //xPlus50 = decimal.Parse(Session["JmlQtyPO"].ToString()) * decimal.Parse(toleransi50.ToString());

                    xTPlus20 = decimal.Parse(Session["JmlQtyPO"].ToString()) + xPlus20;
                    xTPlus50 = decimal.Parse(Session["JmlQtyPO"].ToString()) + xPlus50;
                }
            }

            ///xPlus20 = 0;
            //xPlus50 = 0;

            ////xTPlus20 = 0;
            //xTPlus50 = 0;
            #endregion
            #region Toleransi Kelebihan Terima
            //011102037020000 kantong semen
            if (((Users)Session["Users"]).UnitKerjaID == 1 || ((Users)Session["Users"]).UnitKerjaID == 7 || ((Users)Session["Users"]).UnitKerjaID == 13)
            {
                //citerep
                //if (txtItemCode.Text == "MT-BB-00003" && xTPlus50 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                if (txtItemCode.Text == "011102037020000" && xTPlus50 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                {
                    //DisplayAJAXMessage(this, "Jumlah tidak boleh > 50% dari SPP");
                    DisplayAJAXMessage(this, "Jumlah tidak boleh > dari SPP");
                    return;
                }
                //if (txtItemCode.Text != "MT-BB-00003" && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                if (txtItemCode.Text != "011102037020000" && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                {
                    //DisplayAJAXMessage(this, "Jumlah tidak boleh > 20% dari SPP");
                    DisplayAJAXMessage(this, "Jumlah tidak boleh >  dari SPP");
                    return;
                }
            }
            else
            {
                //karawang
                //if (txtItemCode.Text == "MT-BB-00003" && xTPlus50 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                if (txtItemCode.Text == "011102037020000" && xTPlus50 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                {
                    DisplayAJAXMessage(this, "Jumlah tidak boleh > 50% dari SPP");
                    return;
                }
                //if (txtItemCode.Text != "MT-BB-00003" && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                if (txtItemCode.Text != "011102037020000" && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                {
                    //DisplayAJAXMessage(this, "Jumlah tidak boleh > 20% dari SPP");
                    DisplayAJAXMessage(this, "Jumlah tidak boleh > dari SPP");
                    return;
                }
                //kalsium dan semen curah untuk plan /line 1 s/d 6
                //if ((txtItemCode.Text == "MT-BB-00002" || txtItemCode.Text == "MT-BB-00004" || txtItemCode.Text == "MT-BB-00028") && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                if ((txtItemCode.Text == "011047002000200" || txtItemCode.Text == "012019004030000" || txtItemCode.Text == "011047004000200") && xTPlus20 < (decimal.Parse(txtQtyTerima.Text) + decimal.Parse(Session["JmlQtyReceipt"].ToString())))
                {
                    //DisplayAJAXMessage(this, "Jumlah tidak boleh > 20% dari SPP");
                    DisplayAJAXMessage(this, "Jumlah tidak boleh > dari SPP");
                    return;
                }

            }
            //
            #endregion
            ReceiptDetail receiptDetail = new ReceiptDetail();
            ArrayList arrListReceiptDetail = new ArrayList();
            #region Check Doubel Receipt Item dan Harga yang sama
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
            #endregion
            #region Proses Data
            receiptDetail.NoPO = txtPONo.Text;
            receiptDetail.ItemCode = txtItemCode.Text;
            receiptDetail.ItemName = ddlItemName.SelectedItem.ToString();
            receiptDetail.UOMCode = txtUom.Text;
            //receiptDetail.Quantity = decimal.Parse(txtQtyTerima.Text);
            receiptDetail.Kadarair = decimal.Parse(txtKadarAir.Text);
            receiptDetail.RowStatus = 0;
            receiptDetail.Keterangan = txtKeterangan.Text;

            POPurchnDetailFacade poPurchDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail poPurchnDetail = poPurchDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
            if (poPurchDetailFacade.Error == string.Empty && poPurchnDetail.ID > 0)
            {
                #region CheckData Timbangan untuk item tertentu
                string[] arrDataTimbang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');
                string[] arrSupplier = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
                string YgDiakui = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DataTimbangan", "Receipt").ToString();
                if ((txtQtyTimbang.Text == string.Empty || decimal.Parse(txtQtyTimbang.Text) < 1)
                    && (arrSupplier.Contains(txtSupplier.Text) || arrDataTimbang.Contains(txtItemCode.Text)))
                {
                    DisplayAJAXMessage(this, "Qty BPAS wajib di isi untuk material " + ddlItemName.SelectedItem);
                    return;
                }

                #endregion
                //receiptDetail.Quantity = (arrDataTimbang.Contains(poPurchnDetail.ItemID.ToString())&& YgDiakui=="1") ? decimal.Parse(txtQtyTerima.Text) : decimal.Parse(txtQtyTimbang.Text);
                receiptDetail.Quantity = (arrDataTimbang.Contains(txtItemCode.Text)) ? Math.Round(decimal.Parse(txtQtyTimbang.Text), 0) : decimal.Parse(txtQtyTerima.Text);
                receiptDetail.QtyTimbang = (arrSupplier.Contains(txtSupplier.Text)) ? decimal.Parse(txtQtyTimbang.Text) : (arrDataTimbang.Contains(txtItemCode.Text)) ? Math.Round(decimal.Parse(txtQtyTerima.Text), 0) : 0;
                receiptDetail.TimbanganBPAS = (arrDataTimbang.Contains(txtItemCode.Text)) ? decimal.Parse(txtTimbAsli.Text) : 0;
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
                receiptDetail.KodeTimbang = (arrSupplier.Contains(txtSupplier.Text) || arrDataTimbang.Contains(txtItemCode.Text)) ? int.Parse(YgDiakui.ToString()) : 0;
                receiptDetail.ScheduleNo = ddlMemoHarian.SelectedValue.ToString();
            }
            #endregion
            #region hidden line
            //here add
            //utk CTRP    
            //dpreciated on 10-04-2014 menggunakan cara baru
            //mulai dari sini
            /*
            if (((Users)Session["Users"]).UnitKerjaID == 1 || ((Users)Session["Users"]).UnitKerjaID == 7)
            {
                //if (txtItemCode.Text == "MT-BB-00040" && ddlToPlan.SelectedIndex == 1)
                if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 1)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00004");
                    Inventory inv = inventoryFacade.RetrieveByCode("012019004030000");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
               // else if (txtItemCode.Text == "MT-BB-00040" && ddlToPlan.SelectedIndex == 2)
                else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 2)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00018");
                    Inventory inv = inventoryFacade.RetrieveByCode("012019005040000");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
               // else if (txtItemCode.Text == "MT-BB-00040" && ddlToPlan.SelectedIndex == 3)
                else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 3)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00025");
                    Inventory inv = inventoryFacade.RetrieveByCode("012019006050000");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
                //else if (txtItemCode.Text == "MT-BB-00040" && ddlToPlan.SelectedIndex == 4)
                else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 4)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00042");    
                    Inventory inv = inventoryFacade.RetrieveByCode("012019007060000");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
                //else if (txtItemCode.Text == "MT-BB-00040" && ddlToPlan.SelectedIndex == 5)
                else if (txtItemCode.Text == "012019003020000" && ddlToPlan.SelectedIndex == 5)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("012019001010100");
                    Inventory inv = inventoryFacade.RetrieveByCode("012019001010100");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }

                //if (txtItemCode.Text == "MT-BB-00041" && ddlToPlan.SelectedIndex == 1)
                if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 1)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00028");
                    Inventory inv = inventoryFacade.RetrieveByCode("011047004000200");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
                //else if (txtItemCode.Text == "MT-BB-00041" && ddlToPlan.SelectedIndex == 2)
                else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 2)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00024");
                    Inventory inv = inventoryFacade.RetrieveByCode("011047007000200");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
               // else if (txtItemCode.Text == "MT-BB-00041" && ddlToPlan.SelectedIndex == 3)
                else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 3)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00026");
                    Inventory inv = inventoryFacade.RetrieveByCode("011047008000200");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
                //else if (txtItemCode.Text == "MT-BB-00041" && ddlToPlan.SelectedIndex == 4)
                else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 4)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("MT-BB-00043");
                    Inventory inv = inventoryFacade.RetrieveByCode("011047006000200");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }
                //else if (txtItemCode.Text == "MT-BB-00041" && ddlToPlan.SelectedIndex == 5)
                else if (txtItemCode.Text == "011047003000200" && ddlToPlan.SelectedIndex == 5)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveByCode("011045001010100");
                    Inventory inv = inventoryFacade.RetrieveByCode("011045001010100");
                    if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    {
                        receiptDetail.ItemID = inv.ID;
                        receiptDetail.UomID = inv.UOMID;
                        receiptDetail.ItemCode = inv.ItemCode;
                        receiptDetail.ItemName = inv.ItemName;
                    }
                }

            }*/
            //sampai sini
            //until here add
            #endregion
            #region Proses Semen dan kalsium
            if (CheckShortKey(txtItemCode.Text) == "S" || CheckShortKey(txtItemCode.Text) == "K")
            {
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inv = inventoryFacade.RetrieveByCode(ddlToPlan.SelectedValue.ToString());
                if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                {
                    receiptDetail.ItemID = inv.ID;
                    receiptDetail.UomID = inv.UOMID;
                    receiptDetail.ItemCode = inv.ItemCode;
                    receiptDetail.ItemName = inv.ItemName;
                }

            }
            #endregion
            #region Parsial Delivery
            string SchAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ScheduleAktif", "MemoHarian");
            string[] arrItemID = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)Session["Users"]).UnitKerjaID.ToString()).Split(',');
            int ItemParsial = Array.IndexOf(arrItemID, poPurchnDetail.ItemID.ToString());
            if (int.Parse(SchAktif) == 1 && ItemParsial > -1)
            {
                if (ddlMemoHarian.SelectedIndex > 0)
                {
                    receiptDetail.ScheduleNo = ddlMemoHarian.SelectedItem.Text;
                    receiptDetail.DOID = int.Parse(ddlMemoHarian.SelectedValue.ToString());
                }
                else
                {
                    DisplayAJAXMessage(this, "Nomor DO Harus di pilih");
                    return;
                }
            }
            #endregion
            arrListReceiptDetail.Add(receiptDetail);
            Session["ListOfReceiptDetail"] = arrListReceiptDetail;
            /**
             * AgenLapakprocess
             * Added on 28-09-2015
             */
            #region Colect Surat Jalan AgenLapak
            string AgenLapakAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AgenLapakAktif", "Receipt");
            string[] arrKertasCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KertasItemCode", "Receipt").Split(',');
            string Locked = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Locked", "Receipt");
            if (arrKertasCode.Contains(txtItemCode2.Text) && AgenLapakAktif == "1")
            {
                ArrayList arrData = new ArrayList();
                Receipt rc = new Receipt();
                if (forAgenJabar.Visible == true)
                {
                    string bln = Global.BulanRomawi(DateTime.Parse(txtTanggal.Text).Month).ToString();
                    string thn = DateTime.Parse(txtTanggal.Text).Year.ToString();
                    int sjNumber = this.GetNumerator(AgenCode(txtLapakID.Text)) + 1;
                    ArrayList arrDatax = new ArrayList();
                    Receipt rc1 = new Receipt();
                    rc1.NoPOL = txtNOPOL.Text;
                    rc1.NoSJ = sjNumber.ToString().PadLeft(6, '0') + "/" + AgenCode(txtLapakID.Text) + "/SJ/" + bln + "/" + thn;
                    rc1.Quantity = decimal.Parse(txtQtyTerima.Text);
                    rc1.AgenID = int.Parse(txtLapakID.Text);
                    rc1.CreatedBy = ((Users)Session["Users"]).UserID;
                    rc1.ReceiptDate = DateTime.Parse(txtTanggal.Text);
                    arrDatax.Add(rc1);
                    Session["aLapak"] = (arrDatax.Count > 0) ? arrDatax : null;

                }
                else
                {
                    rc.NoSJ = (ddlSjLapak.SelectedValue == string.Empty) ? txtNoSJ.Text : ddlSjLapak.SelectedValue;
                    rc.KirimID = (txtKirimanID.Text == string.Empty) ? 0 : Convert.ToInt32(txtKirimanID.Text);
                    rc.Quantity = decimal.Parse(txtQtyTerima.Text);
                    arrData.Add(rc);
                    Session["Lapak"] = (arrData.Count > 0) ? arrData : null;
                    ddlSjLapak.SelectedIndex = 0;
                }
            }
            #endregion
            GridView1.DataSource = arrListReceiptDetail;
            GridView1.DataBind();

            lstRMS.DataSource = arrListReceiptDetail;
            lstRMS.DataBind();
            clearQty();
            Session["JmlQtyReceipt"] = null;
            Session["JmlQtyPO"] = null;

        }
        protected void txtQtyTimbang_Change(object sender, EventArgs e)
        {
            txtTimbAsli.Text = txtQtyTimbang.Text;
        }
        protected void ddlMemoHarian_Change(object sender, EventArgs e)
        {
            //txtQtyTerima.Text = (ddlMemoHarian.SelectedIndex > 0) ? new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("HiblowCapacity", "MemoHarian").ToString() : "0";
        }
        protected void lstRMS_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string[] arrDataTimbang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');
            string[] arrSupplier = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockReceipt", "Receipt").Split(new string[] { "," }, StringSplitOptions.None);
            Image lok = (Image)e.Item.FindControl("lstLock");
            Image unlok = (Image)e.Item.FindControl("LstUnLock");
            Image edt = (Image)e.Item.FindControl("lstEdit");
            Image del = (Image)e.Item.FindControl("lstDel");
            Image deln = (Image)e.Item.FindControl("DelNew");
            Label info = (Label)e.Item.FindControl("txtQty");
            ReceiptDetail rcp = e.Item.DataItem as ReceiptDetail;
            Receipt rdp = new ReceiptFacade().RetrieveByID(rcp.ReceiptID);
            int inv = new InventoryFacade().GetStock(rcp.ItemID, rcp.ItemTypeID);
            deln.Visible = (rcp.ID == 0) ? true : false;
            edt.Visible = ((rdp.Status > 0 || arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) && rcp.ID > 0) ? true : false;
            del.Visible = (rdp.Status > 0 || rcp.RowStatus < 0 || inv < rcp.Quantity) ? false : false;
            lok.Visible = (rdp.Status > 0 && arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            unlok.Visible = (rdp.LastModifiedBy != "" && rdp.Status == 0 && arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            /** Tampilkan data timbangan jika ada */
            Repeater dTim = (Repeater)e.Item.FindControl("Timbangan");
            ArrayList arrDtim = (ArrayList)Session["ListOfReceiptDetail"];

            info.ToolTip = (arrDataTimbang.Contains(rcp.ItemCode)) ? "Timbangan Supplier : " + rcp.QtyTimbang.ToString("###,##0.00") + "\n" : "";
            info.ToolTip += (arrDataTimbang.Contains(rcp.ItemCode)) ? "Timbangan BPAS : " + rcp.TimbanganBPAS.ToString("###,##0.00") : "";
        }
        protected void lstRMS_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            ArrayList arrDetail = new ArrayList();
            switch (e.CommandName)
            {
                case "edit":
                    #region Edit Proses
                    string[] arrDataTimbang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');
                    string[] arrEdit = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UpdateReceipt", "Receipt").Split(new string[] { "," }, StringSplitOptions.None);
                    if (arrEdit.Contains(((Users)Session["Users"]).DeptID.ToString()))
                    {
                        int ID = int.Parse(e.CommandArgument.ToString());
                        Receipt rcp = new ReceiptDetailFacade().RetrieveByDetailID(ID);
                        txtMrsNo.Text = rcp.ReceiptNo;
                        txtPONo.Text = rcp.PoNo;
                        txtNoSpp.Text = rcp.SppNo;
                        txtSupplier.Text = rcp.SupplierName;
                        txtTanggal.Text = rcp.ReceiptDate.ToString("dd-MMM-yyyy");
                        txtItemCode.Text = rcp.ItemCode;
                        txtUom.Text = rcp.ApprovalBy;
                        ddlItemName.SelectedValue = rcp.PoID.ToString();
                        txtQty.Text = rcp.QtyPO.ToString("###,##0.00");
                        txtQtyTerima.Text = (arrDataTimbang.Contains(rcp.ItemCode)) ? rcp.QtyTimbang.ToString("###,##0.00") : rcp.Quantity.ToString("###,##0.00");
                        txtQtyTimbang.Text = (arrDataTimbang.Contains(rcp.ItemCode)) ? rcp.Quantity.ToString("###,##0.00") : rcp.QtyTimbang.ToString("###,##0.00");
                        txtTimbAsli.Text = rcp.QtyBPAS.ToString();
                        txtKadarAir.Text = rcp.KursPajak.ToString();
                        txtKeterangan.Text = rcp.Keterangan1;
                        txtCreatedBy.Text = rcp.CreatedBy;
                        int jml = new InventoryFacade().GetStock(rcp.ID, rcp.ItemTypeID);
                        txtStok.Text = jml.ToString("###,##0.00");
                        txtReceiptID.Text = ID.ToString();
                        PilihPlan();
                        if (ddlToPlan.Items.Count > 1)
                        {
                            ddlToPlan.SelectedValue = rcp.InvoiceNo.ToString();
                        }
                        btnEdit.Visible = (((Users)Session["Users"]).Apv >= 1) ? true : false;
                        string EditSta = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EditStatus", "Receipt");
                        if (EditSta == "0") { DisableForm(); } else { EnableForm(); }
                    }
                    #endregion
                    break;
                case "del":
                    #region Delete detail receipt
                    try
                    {
                        string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeleteReceipt", "Receipt").Split(',');
                        if (arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString()) && ((Users)Session["Users"]).Apv >= 1)
                        {
                            #region verifikasi data
                            int flagPO = 0;
                            Session["Field"] = " Jumlah ";
                            int ID = int.Parse(e.CommandArgument.ToString());
                            Receipt rcp = new ReceiptDetailFacade().RetrieveByDetailID(ID);
                            //check stock per item
                            int jml = new InventoryFacade().GetStock(rcp.ID, rcp.ItemTypeID);
                            if (jml < rcp.Quantity)
                            {
                                DisplayAJAXMessage(this, "Tidak bisa di cancel Stock Tidak mencukupi");
                                return;
                            }
                            //check status pembayaran receipt
                            if (rcp.Status > 0)
                            {
                                DisplayAJAXMessage(this, "Tidak bisa di hapus karena dalam proses pembayaran");
                                return;
                            }
                            else
                            {
                                string msg = string.Empty;
                                POPurchnDetail po = new POPurchnDetailFacade().RetrieveByDetailID(rcp.PoID);
                                if (po.Status == -2) { msg = "Tidak bisa dihapus PO Sudah di close"; }
                                else if (po.Status == -1) { msg = "Tidak bisa dihapus PO sudah di delete"; }
                                else if (po.Status == 1) { msg = "Tidak bisa dihapus PO dalam proses pembayaran"; }
                                else if (po.Status == 3) { msg = "Tidak dapat dihapus karena PO dalam pengajuan Pembayaran"; }
                                else if (po.Status == 4) { msg = "Tidak dapat dihapus karena PO dalam pembuatan Giro"; }
                                else if (po.Status == 5) { msg = "Tidak dapat dihapus karena PO sudah di-Bayar"; }
                                else if (msg != string.Empty) { DisplayAJAXMessage(this, msg); return; }
                                else
                                {
                                    flagPO = po.Status;
                                }

                            }
                            #endregion
                            #region Check Status Closing
                            /**
                         * check closing periode saat ini
                         * added on 13-08-2014
                         */
                            ClosingFacade Closing = new ClosingFacade();
                            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
                            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
                            int status = Closing.GetMonthStatus(Tahun, Bulan, "Purchn");
                            int clsStat = Closing.GetClosingStatus("SystemClosing");
                            if (status == 1 && clsStat == 1)
                            {
                                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing.\nTransaksi Tidak bisa dilakukan");
                                return;
                            }
                            #endregion
                            Session["ReceiptID"] = " and A.ID=" + ID;
                            ArrayList arrTransferDetail = new ReceiptDetailFacade().RetrieveByReceiptId(ID);
                            ArrayList arrReceiptDetail = new ArrayList();
                            foreach (ReceiptDetail rd in arrTransferDetail)
                            {

                                ((ReceiptDetail)arrTransferDetail[0]).FlagPO = flagPO;
                                arrReceiptDetail.Add(rd);

                            }

                            ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(new Receipt(), arrReceiptDetail, new ReceiptDocNo());
                            string strError = receiptProcessFacade.CancelReceiptDetail();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, strError);
                                return;
                            }
                            ArrayList arrTransfer = new ArrayList();

                            foreach (ReceiptDetail rd in arrTransferDetail)
                            {
                                arrTransfer.Add(rd);
                            }

                            string strEvent = "Hapus per Baris by " + ((Users)Session["Users"]).UserName;
                            InsertLog(strEvent);

                            Session["ListOfReceiptDetail"] = arrTransfer;

                            GridView1.DataSource = arrTransfer;
                            GridView1.DataBind();
                            lstRMS.DataSource = arrTransfer;
                            lstRMS.DataBind();
                        }
                        else
                        {
                            Session["Field"] = "";
                            DisplayAJAXMessage(this, "Level Aproval tidak mencukupi");
                            return;
                        }
                        Session["Field"] = "";
                        Session["ReceiptID"] = "";
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Data Tidak bisa di hapus");
                    }
                    #endregion
                    break;
                case "unlock":
                    #region Buka kunci untuk di edit/delete
                    string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockReceipt", "Receipt").Split(new string[] { "," }, StringSplitOptions.None);
                    if (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()))
                    {
                        Session["Lock"] = 0;
                        int ID = int.Parse(e.CommandArgument.ToString());
                        int rst = new ReceiptDetailFacade().UnLockReceipt(ID);
                        if (rst > 0) { Response.Redirect(HttpContext.Current.Request.Url.ToString()); }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Hubungi Accounting untuk unlock RMS ini");
                    }
                    #endregion
                    break;
                case "lock":
                    #region lock untuk edit data
                    string[] arrDepts = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockReceipt", "Receipt").Split(new string[] { "," }, StringSplitOptions.None);
                    if (arrDepts.Contains(((Users)Session["Users"]).DeptID.ToString()))
                    {
                        Session["Lock"] = 1;
                        int ID = int.Parse(e.CommandArgument.ToString());
                        int rst = new ReceiptDetailFacade().UnLockReceipt(ID);
                        if (rst > 0) { Response.Redirect(HttpContext.Current.Request.Url.ToString()); }
                        Session["Lock"] = "";
                    }
                    #endregion
                    break;
                case "deln":
                    arrDetail = (ArrayList)Session["ListOfReceiptDetail"];
                    if (arrDetail.Count > index) arrDetail.RemoveAt(index);
                    Session["ListOfReceiptDetail"] = arrDetail;
                    lstRMS.DataSource = arrDetail;
                    lstRMS.DataBind();
                    break;
            }
        }
        private string CheckShortKey(string ItemCode)
        {
            InventoryFacade inv = new InventoryFacade();
            ArrayList arrInv = new ArrayList();
            Inventory objInv = inv.RetrieveByCode(ItemCode);
            return objInv.ShortKey.ToString();
        }
        private void DisableForm()
        {
            foreach (object c in frm10.Controls)
            {
                if (c is TextBox) { ((TextBox)c).Enabled = false; }
                if (c is DropDownList) { ((DropDownList)c).Enabled = false; }
            }
        }
        private void EnableForm()
        {
            DisableForm();
            string[] txtField = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EditTableField", "Receipt").Split(',');
            for (int i = 0; i < txtField.Count(); i++)
            {
                TextBox d = Div1.FindControl(txtField[i].ToString()) as TextBox;
                d.Enabled = true;
            }
        }
        protected void EditData(object sender, EventArgs e)
        {
            ReceiptDetail objRcp = new ReceiptDetail();
            ReceiptDetailFacade rcp = new ReceiptDetailFacade();
            string[] arrDataTimbang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');
            string[] field = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EditTableField", "Receipt").Split(',');
            string[] domain = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DomainName", "Receipt").Split(',');
            objRcp.ID = int.Parse(txtReceiptID.Text);
            objRcp.Quantity = (arrDataTimbang.Contains(txtItemCode.Text)) ? decimal.Parse(txtQtyTimbang.Text) : decimal.Parse(txtQtyTerima.Text);
            objRcp.QtyTimbang = (txtQtyTimbang.Text != string.Empty) ? ((arrDataTimbang.Contains(txtItemCode.Text)) ? decimal.Parse(txtQtyTerima.Text) : decimal.Parse(txtQtyTimbang.Text)) : 0;
            objRcp.Keterangan = txtKeterangan.Text;
            int result = rcp.UpdateRMS(objRcp);
            if (result > 0)
            {
                Response.Redirect(HttpContext.Current.Request.Url.ToString());
            }
            else
            {
                DisplayAJAXMessage(this, "Update Gagal");
            }
        }
        private void LoadSchedule(int PoID, int ItemID)
        {
            ArrayList arrSch = new ArrayList();
            ddlMemoHarian.Items.Clear();
            arrSch = new ReceiptFacade().GetSchedule(PoID, ItemID);
            ddlMemoHarian.Items.Add(new ListItem("--Pilih DO--", "0"));
            foreach (Receipt rcp in arrSch)
            {
                ListItem ls = new ListItem(rcp.InvoiceNo.ToString(), rcp.ID.ToString());
                ls.Attributes.Add("title", "Sch Date :" + rcp.ReceiptDate.ToString());
                ddlMemoHarian.Items.Add(ls);
            }
        }
        protected void txtKadarAir_Change(object sender, EventArgs e)
        {
            string[] MatCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialCode", "Receipt").Split(',');
            string stdKadarAir = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "Receipt");
            string CheckKA_Aktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KadarAirAktif", "Receipt");
            if (MatCode.Contains(txtItemCode.Text) && CheckKA_Aktif == "1")
            {
                /** hitung kadar air jika material gypsum)*/
                decimal Netto = 0; decimal Nett = 0;
                decimal AktualKA = Convert.ToDecimal(txtKadarAir.Text);
                decimal StandarKA = Convert.ToDecimal(stdKadarAir);
                decimal dataTimbangan = (txtQtyTimbang.Text != string.Empty) ? Convert.ToDecimal(txtQtyTimbang.Text) : 0;
                decimal dataTerima = (txtQtyTerima.Text != string.Empty) ? Convert.ToDecimal(txtQtyTerima.Text) : 0;
                if (AktualKA > StandarKA)
                {
                    decimal SelisihKA = AktualKA - StandarKA;
                    Netto = dataTimbangan - ((SelisihKA * dataTimbangan) / 100);
                    Nett = dataTerima - ((SelisihKA * dataTerima) / 100);
                    txtQtyTimbang.Text = Math.Round(Netto).ToString("###,##0.00");
                    //txtQtyTerima.Text = Nett.ToString("###,##0.00");
                }
                else
                {
                    txtQtyTimbang.Text = txtQtyTimbang.Text;
                    txtQtyTerima.Text = txtQtyTerima.Text;
                }
            }
        }
        /**
         * Posting receipt material kertas kantong semen
         * untuk proses point ke lapak
         */
        private string ReceiptNo { get; set; }
        private string Qty { get; set; }
        private string PlanID { get; set; }
        private string IDLapak { get; set; }
        private string AgenID { get; set; }
        private string SupplierID { get; set; }
        private bpas_api.WebService1 api = new bpas_api.WebService1();
        private Global2 api2 = new Global2();

        private void LoadSJLapak()
        {
            txtLapakID.Text = this.GetIDLapak().ToString();
            if (txtLapakID.Text != "-1" || txtLapakID.Text != string.Empty)
            {
                LoadSJ();
            }
            else
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error, Silahkan hubungi IT Dept.");
                return;
            }
        }

        private void LoadAgen()
        {
            try
            {
                //string IDAgen = "0";
                DataSet ds = new DataSet();
                ds = api2.GetDataFromTable("Agen_DtSupplier", " where SuppPurchID=" + this.SupplierID, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    this.AgenID = d["AgenID"].ToString();
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LoadSJ();
                    //LoadNopol();
                }
            }
            catch (Exception)
            {
                DisplayAJAXMessage(this, "Koneksi internet error ");
                return;
            }
        }
        //not used

        private void LoadSJ()
        {

            DataSet ds = new DataSet();
            ddlSjLapak.Items.Clear();
            ddlSjLapak.Items.Add(new ListItem("--Pilih No SJ--", ""));
            string Criteria = " where AgenID=" + txtLapakID.Text;
            Criteria += " and ConfirmReceipt=0";
            ds = api2.GetDataTable("Agen_DtKirimKeBpas", "NoSJ,PlatNomor,ID", Criteria, "GRCBoardLapak");
            foreach (DataRow d in ds.Tables[0].Rows)
            {
                ddlSjLapak.Items.Add(new ListItem(d["NoSJ"].ToString() + " - " + d["PlatNomor"].ToString().ToUpper(), d["NoSJ"].ToString()));
            }


        }
        protected void ddlSjLapak_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds2 = new DataSet();
            txtKirimanID.Text = "0";
            string Criteria2 = " where NoSJ='" + ddlSjLapak.SelectedValue.ToString() + "'";
            ds2 = api2.GetDataTable("Agen_DtKirimKeBpas", "ID", Criteria2, "GRCBoardLapak");
            string kirimanID = "0";
            foreach (DataRow dd in ds2.Tables[0].Rows)
            {
                kirimanID = dd["ID"].ToString();
            }
            txtKirimanID.Text = kirimanID;
            lbAddOP.Enabled = (ddlSjLapak.SelectedIndex > 0) ? true : false;
        }
        private void UpdateReceiptKS()
        {
            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            string ID = this.IDLapak;
            decimal Jumlah = decimal.Parse(this.Qty);
            string Plant = this.PlanID;
            string ReceiptID = this.ReceiptNo;
            string rst = bpas.UpdateKiriman(ID, ReceiptID, Jumlah, PlanID);
        }
        private int GetIDLapak()
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();

                string Criteria = " where SupplierID=" + this.SupplierID;
                Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = api2.GetDataFromTable("Agen_DtAgenIDtoSupID", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AgenID"].ToString());
                }
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        private int GetAreaKirim(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = api2.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AreaKirim"].ToString());
                }
                return result;
            }
            catch (Exception) { return -1; }

        }
        private string AgenCode(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = api2.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["Code"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private int GetNumerator(string AgenCode)
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where Type='SJ Code " + AgenCode + "'";
                ds = api2.GetDataFromTable("Agen_DtNumerator", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = Convert.ToInt32(d["LastNumber"].ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                return 0; ;
            }

        }
        private string GetSJfromPointLapak(int ReceiptID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ConfirmReceipt=" + ReceiptID;
                ds = api2.GetDataFromTable("Agen_DtKirimKeBpas", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["NoSJ"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        protected void btnReprint_ServerClick(object sender, EventArgs e)
        {
            ReceiptFacade rcp = new ReceiptFacade();
            string ReceiptID = (Session["id"] != null) ? Session["id"].ToString() : "0";
            int result = rcp.UpdateStatusReprint(ReceiptID);
            btnPrint.Disabled = (result > 0) ? false : true;
        }
        private int CheckStatusCetak(int receiptID)
        {
            int result = 0;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString("Select Cetak from receipt where ID=" + receiptID);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Cetak"].ToString());
                }
            }
            return result;
        }
    }
}