using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormReceiptBiaya : System.Web.UI.Page
    {
        public enum Status
        {
            Open = 0,
            Parsial = 1,
            Receipt = 2,
            PrePayment = 3,
            CreateGiro = 4,
            Release = 5,

            //ReceiptType = 7 = BRS = KB
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Global.link = "../../Default.aspx";

                //iko
                LoadDept();
                LoadGroupPM();
                LoadItemBiaya();
                //iko

                if (Request.QueryString["ReceiptNo"] != null)
                {
                    LoadReceipt(Request.QueryString["ReceiptNo"].ToString());
                }
                else
                {
                    clearForm();
                }
                xref.Visible = (PurchnConfig("DOSupplierAktif") == 1) ? true : false;
                txtNoReff.Visible = (PurchnConfig("DOSupplierAktif") == 1) ? true : false;
                if (PurchnConfig("DOSupplierAktif") == 1 && PurchnConfig("InfoUpdateReceipt") == 1)
                {
                    ShowInfo("ReceiptDO");
                }
                string AutoSPB = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AutoSPBBiaya", "Receipt");
                chkAutoSPB.Checked = (AutoSPB == "1") ? true : false;
            }
            else
            {
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();

                string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "B";
                if (txtCariOP.Text != string.Empty && txtCariOP.Text.Substring(0, 2).ToUpper() == kd)
                {
                    POPurchnFacade poPurchnFacade = new POPurchnFacade();
                    Domain.POPurchn po = poPurchnFacade.RetrieveByNoCheckStatus(txtCariOP.Text);
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
                                ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName, purchnDetail.ID.ToString()));
                                //ddlItemName.Items.Add(new ListItem(purchnDetail.ItemName + " - " + purchnDetail.DocumentNo, purchnDetail.ID.ToString()));
                            }
                        }
                    }
                    else
                    {
                        if (txtCariOP.Text.Substring(0, 2).ToUpper() == kd)
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
                                else if (cekPo.Approval == 2)
                                {
                                    DisplayAJAXMessage(this, "Belum di-Approval Manager Plan");
                                    clearForm();
                                    return;
                                }

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
                            DisplayAJAXMessage(this, "Bukan No PO untuk BRS ...!");
                            clearForm();
                            return;
                        }
                    }

                    txtCariOP.Text = string.Empty;
                }
            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }
        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void LoadItemBiaya()
        {
            ddlJenisBiaya.Items.Clear();
            ArrayList arrBiaya = new ArrayList();
            BiayaFacade biayaFacade = new BiayaFacade();
            arrBiaya = biayaFacade.Retrieve3();

            ddlJenisBiaya.Items.Add(new ListItem("-- Pilih Biaya --", "0"));
            foreach (Biaya Bia in arrBiaya)
            {
                if (Bia.ItemCode.Length == 15)
                {
                    ddlJenisBiaya.Items.Add(new ListItem(Bia.ItemName + "  (" + Bia.ItemCode + ")", Bia.ID.ToString()));
                }
            }
        }
        private void LoadGroupPM()
        {
            ArrayList arrPM = new ArrayList();
            MTC_ZonaFacade PM = new MTC_ZonaFacade();
            arrPM = PM.RetrieveSpGroup();
            spGroup.Items.Clear();
            spGroup.Items.Add(new ListItem(" ", "0"));
            foreach (MTC_Zona uPm in arrPM)
            {
                spGroup.Items.Add(new ListItem(uPm.ZonaName, uPm.ID.ToString()));
            }
        }
        protected void ddlJenisBiaya_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJenisBiaya.SelectedIndex > 0)
            {
                //txtCariNamaBrg.Focus();
                if (ddlJenisBiaya.SelectedIndex == 3)
                {
                    if (PurchnConfig("InfoUpdateSPB") == 1)
                    {
                        ShowInfo("SPBBiaya");
                    }
                    x1.Visible = (PurchnConfig("SPBArmada") == 1) ? true : false;
                    ddlNoPolisi.Visible = (PurchnConfig("SPBArmada") == 1) ? true : false;
                    CheckBox1.Visible = (PurchnConfig("SPBArmada") == 1) ? true : false;
                    LoadNoPol(int.Parse(ddlDeptName.SelectedValue));
                }
                else
                {
                    LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
                }
            }
        }
        private void LoadNoPol(int DeptID)
        {
            try
            {
                DataSet NoPol = new DataSet();
                DataSet AlKend = new DataSet();
                ArrayList arrNopol = new ArrayList();
                ddlNoPolisi.Items.Clear();
                bpas_api.WebService1 api = new bpas_api.WebService1();
                string UnitKerja = string.Empty;
                string AliasKend = string.Empty;
                CheckBox1.Text = (((Users)Session["Users"]).UnitKerjaID == 7) ? "Citeureup" : "Karawang";
                if (CheckBox1.Checked == true)
                {
                    UnitKerja = (((Users)Session["Users"]).UnitKerjaID == 7) ? "1" : "7";
                    AliasKend = (((Users)Session["Users"]).UnitKerjaID == 7) ? "GRCBoardCtrp" : "GRCBoardKrwg";
                }
                else
                {
                    UnitKerja = ((Users)Session["Users"]).UnitKerjaID.ToString();
                    AliasKend = (((Users)Session["Users"]).UnitKerjaID == 7) ? "GRCBoardKrwg" : "GRCBoardCtrp";
                }
                NoPol = api.GetNoPolByPlant(UnitKerja);
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {
                    string Alias = string.Empty;
                    AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), AliasKend);
                    foreach (DataRow al in AlKend.Tables[0].Rows)
                    {
                        Alias = al["NamaKendaraan"].ToString();
                    }

                    arrNopol.Add(new MTC_Armada
                    {
                        ID = Convert.ToInt32(nR["ID"].ToString()),
                        NoPol = nR["KendaraanNo"].ToString() + " - " + Alias
                    });
                }
                ddlNoPolisi.DataSource = arrNopol;
                ddlNoPolisi.DataValueField = "ID";
                ddlNoPolisi.DataTextField = "NoPol";
                ddlNoPolisi.DataBind();
                ddlNoPolisi.Items.Add(new ListItem("Forklift - ", "1001"));
                ddlNoPolisi.Items.Add(new ListItem("Umum - ", "1000"));
                ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                ddlNoPolisi.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
                ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                ddlNoPolisi.SelectedValue = "0";
            }
        }
        private decimal GetLastPriceBiaya(int ItemID)
        {
            decimal result = 0;
            string strSQL = "Select Top 1 Price From ReceiptDetail where ItemTypeID=3 and ItemID=" + ItemID + " order by id desc";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = (sdr["Price"] != DBNull.Value) ? decimal.Parse(sdr["Price"].ToString()) : 0;
                }
            }
            return result;
        }
        protected void spGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spGroup.SelectedValue == "14")
            {
                LoadData("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                ddlForklif.Visible = true;
                frk.InnerHtml = "Forklift Name";
            }
            else
            {
                frk.InnerHtml = ""; ddlForklif.Visible = false;
            }
        }
        private void LoadData(string Section, string Key)
        {
            string[] arrData = new Inifiles(Server.MapPath("~/App_Data/GroupArmadaOnly.ini")).Read(Key, Section).Split(',');
            if (arrData.Count() > 0)
            {
                ddlForklif.Items.Clear();
                ddlForklif.Items.Add(new ListItem("--pilih--", "0"));
                for (int i = 0; i < arrData.Count(); i++)
                {
                    ddlForklif.Items.Add(new ListItem(arrData[i].ToString(), arrData[i].ToString()));
                }
            }
            else
            {
                ddlForklif.Items.Clear();
            }
        }
        protected void ddlNoPolisi_SelectedChange(object sender, EventArgs e)
        {
            try
            {
                DataSet NoPol = new DataSet();
                bpas_api.WebService1 api = new bpas_api.WebService1();
                NoPol = api.DetailKendaraan(ddlNoPolisi.SelectedItem.ToString());
                if (ddlNoPolisi.SelectedValue == "1001")
                {
                    frk.InnerHtml = "Nama " + ddlNoPolisi.SelectedItem.Text;
                    LoadData("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                    ddlForklif.Visible = true;
                }
                else if (ddlNoPolisi.SelectedValue == "1000")
                {
                    frk.InnerHtml = "Kendaraan";
                    LoadData("Umum", "U" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                    ddlForklif.Visible = true;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet error");
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
        }

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadReceipt(txtSearch.Text);
        }

        private void LoadReceipt(string strReceiptNo)
        {
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            string forReceipt = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "B";
            //string forReceipt = "KB";
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
                ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receipt.ID);
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
                    btnUpdate.Enabled = true;
                    //btnPrint.Enabled = false;
                }
                else
                {
                    if (receipt.Status == 0)
                    {
                        btnPrint.Enabled = true;
                    }

                }
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
            //arrList.Add(new ReceiptDetail());

            btnUpdate.Enabled = true;
            btnCancel.Enabled = false;
            btnPrint.Enabled = true;
            lbAddOP.Enabled = true;
            txtTanggal.Enabled = false;
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            //lstBiaya.DataSource = arrList;
            //lstBiaya.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("ReceiptNo");
            Session.Remove("ListOfReceiptDetail");
            Session.Remove("id");
            Session.Remove("Receipt");
            txtCariOP.Enabled = true;
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
                #region dinonaktifkan
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

            ReceiptDocNoFacade receiptDocNoFacade = new ReceiptDocNoFacade();
            ReceiptDocNo receiptDocNo = new ReceiptDocNo();

            if (strEvent == "Insert")
            {
                receiptDocNo = receiptDocNoFacade.RetrieveByReceiptCode(bln, thn, txtPONo.Text.Substring(0, 2));
                if (receiptDocNo.ID == 0)
                {
                    noBaru = 1;
                    receiptDocNo.ReceiptCode = txtPONo.Text.Substring(0, 2);
                    receiptDocNo.NoUrut = 1;
                    receiptDocNo.MonthPeriod = bln;
                    receiptDocNo.YearPeriod = thn;
                }
                else
                {
                    noBaru = receiptDocNo.NoUrut + 1;
                    receiptDocNo.ReceiptCode = txtPONo.Text.Substring(0, 2);
                    receiptDocNo.NoUrut = receiptDocNo.NoUrut + 1;
                }
            }

            POPurchnFacade poPurchnFacade = new POPurchnFacade();
            Domain.POPurchn poPurchn = poPurchnFacade.RetrieveByNo(txtPONo.Text);
            if (poPurchnFacade.Error == string.Empty && poPurchn.ID > 0)
            {
                receipt.SupplierType = 0;
                receipt.SupplierID = poPurchn.SupplierID;
                //receipt.PaymentDate = "";
                receipt.PoID = poPurchn.ID;
                receipt.PoNo = poPurchn.NoPO;
            }

            receipt.ReceiptNo = txtMrsNo.Text;
            receipt.ReceiptDate = DateTime.Parse(txtTanggal.Text);
            receipt.ReceiptType = 5;
            receipt.SupplierType = 0;
            receipt.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            receipt.Status = 0;
            receipt.CreatedBy = ((Users)Session["Users"]).UserName;
            receipt.ItemTypeID = 3;

            receipt.TipeAsset = 0;

            string strError = string.Empty;
            ArrayList arrReceiptDetail = new ArrayList();
            ArrayList arrListPakaiDetail = new ArrayList();
            string kd = string.Empty;
            Pakai pakai = new Pakai();
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            if (Session["ListOfReceiptDetail"] != null)
            {
                arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];

                //iko
                foreach (ReceiptDetail rD in arrReceiptDetail)
                {
                    //auto spbdetail

                    //new add
                    //new add
                    //new add
                    #region validasi data / gak perlu
                    //try
                    //{
                    //    decimal cekNumber = decimal.Parse(txtQtyPakai.Text);
                    //}
                    //catch
                    //{
                    //    DisplayAJAXMessage(this, "Quantity Pakai harus number");
                    //    return;
                    //}
                    //if (decimal.Parse(txtQtyPakai.Text) == 0)
                    //{
                    //    DisplayAJAXMessage(this, "Quantity Terima tidak boleh 0");
                    //    return;
                    //}

                    //try
                    //{
                    //    decimal cekNumber = decimal.Parse(txtStok.Text);
                    //}
                    //catch
                    //{
                    //    DisplayAJAXMessage(this, "Quantity Stok harus number");
                    //    return;
                    //}
                    //if (decimal.Parse(txtStok.Text) == 0)
                    //{
                    //    DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                    //    return;
                    //}

                    //if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text))
                    //{
                    //    DisplayAJAXMessage(this, "Quantity Pakai lebih besar dari pada Stok");
                    //    return;
                    //}
                    #endregion
                    #region multigiudang tidak di terapkan
                    //filter multigudang -- tidak berlaku untuk multigudang
                    //SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
                    //decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
                    //if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text) - StockOtherDept)
                    //{
                    //    DisplayAJAXMessage(this, "Sebagian Quantity Stock sudah dipesan oleh departemen lain, coba kurangi Qty pakainya");
                    //    return;
                    //}
                    //ddlDeptName.Enabled = false;
                    //end filter multigudang
                    //"K" utk Karawang
                    #endregion
                    #region validasi jika pilih biaya kendaraan
                    if (ddlJenisBiaya.SelectedIndex == 3 && ddlNoPolisi.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, "No. Kendaraan Belum dipilih");
                        //return;
                    }
                    #endregion
                    #region check last trans dan closing periode
                    //Company company = new Company();
                    //CompanyFacade companyFacade = new CompanyFacade();
                    kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "B";

                    string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
                    DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

                    PakaiFacade pakaiFacade = new PakaiFacade();
                    Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
                    DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);

                    /**
                     * check closing periode
                     */
                    AccClosingFacade cls = new AccClosingFacade();
                    AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
                    if (clsBln.Status == 1)
                    {
                        string mess = "Periode " + cls.nBulan(clsBln.Bulan) + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                        DisplayAJAXMessage(this, mess);
                        return;
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
                    PakaiDetail pakaiDetail = new PakaiDetail();
                    //ArrayList arrListPakaiDetail = new ArrayList();
                    #region validasi tidak terjadi double itemcode / gak pake
                    //if (Session["ListOfPakaiDetail"] != null)
                    //{
                    //    arrListPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                    //    if (arrListPakaiDetail.Count > 0)
                    //    {
                    //        int ada = 0;
                    //        foreach (PakaiDetail pki in arrListPakaiDetail)
                    //        {
                    //            if (CheckStatusBiaya() == 1)
                    //            {
                    //                if (txtCariNamaBrg.Text == pki.ItemName)
                    //                {
                    //                    DisplayAJAXMessage(this, "Sudah ada di-tabel untuk Barang tersebut");
                    //                    txtCariNamaBrg.Text = string.Empty;
                    //                    return;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (pki.ItemCode == txtItemCode.Text)
                    //                {
                    //                    DisplayAJAXMessage(this, "Sudah ada di-tabel untuk Barang tersebut");

                    //                    clearQty();
                    //                    return;
                    //                }
                    //            }
                    //            ada = ada + 1;
                    //        }
                    //    }
                    //}
                    #endregion
                    #region Validasi pilihan Zona untuk spb maintenance
                    if (Session["Zona"] != null)
                    {
                        if (Session["Zona"].ToString() == "1")
                        {
                            if (ddlZona.SelectedIndex == 0)
                            {
                                DisplayAJAXMessage(this, "Zona belum ditentukan");
                                return;
                            }
                        }
                    }
                    #endregion
                    #region reoder point tidak belaku
                    //cek ke re-order point--tidak berlaku untuk biaya
                    //InventoryFacade inventoryFacade = new InventoryFacade();
                    //Inventory inv = inventoryFacade.RetrieveById2(int.Parse(ddlItemName.SelectedValue));
                    //if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                    //{
                    //    if (inv.Jumlah - decimal.Parse(txtQtyPakai.Text) <= inv.ReOrder)
                    //    {
                    //        string aMess = "Jumlah Pemakaian Barang di stock sudah melampaui Reorder Point ( " + inv.ReOrder.ToString() + " )";
                    //        DisplayAJAXMessage(this, aMess);
                    //    }
                    //}
                    //
                    #endregion

                    //harus di-define pada klik Tambah dahulu

                    pakaiDetail.ItemID = rD.ItemID;             //(CheckStatusBiaya() == 1) ? int.Parse(txtItemID.Text) : int.Parse(ddlItemName.SelectedValue);
                    pakaiDetail.Quantity = rD.Quantity;         //decimal.Parse(txtQtyPakai.Text);
                    pakaiDetail.RowStatus = 0;
                    pakaiDetail.Keterangan = rD.Keterangan;     //txtKeterangan.Text;
                    pakaiDetail.GroupID = rD.GroupID;           //int.Parse(txtGroupID.Text);
                    pakaiDetail.UomID = rD.UomID;               //int.Parse(txtUomID.Text);
                    pakaiDetail.ItemCode = rD.ItemCode;         //txtItemCode.Text;
                    pakaiDetail.ItemName = rD.ItemName;         //txtCariNamaBrg.Text;
                    pakaiDetail.UOMCode = rD.UOMCode;           //txtUom.Text;
                    pakaiDetail.FlagTipe = rD.ItemID;           //(CheckStatusBiaya() == 1) ? int.Parse(txtItemID.Text) : 0; //id dari itemname field cari nama barang
                    pakaiDetail.SarmutID = rD.SarmutID;         //(spGroup.SelectedValue == string.Empty) ? 0 : int.Parse(spGroup.SelectedValue.ToString());
                    pakaiDetail.AvgPrice = rD.Price;         //(GetLastPriceBiaya((CheckStatusBiaya() == 1) ? int.Parse(txtItemID.Text) : int.Parse(ddlItemName.SelectedValue)));
                    pakaiDetail.DeptCode = rD.DeptCode;
                    //for biaya
                    pakaiDetail.ItemTypeID = 3;
                    pakaiDetail.IDJenisBiaya = rD.IDJenisBiaya; //(CheckStatusBiaya() == 1) ? int.Parse(ddlJenisBiaya.SelectedValue) : 0;
                    pakaiDetail.NoPol = rD.NoPol;

                    #region untuk MtcProject
                    int q = 0;
                    if (rD.DeptCode == "034") { q = rD.ProjectID; }
                    pakaiDetail.ProjectID = q;
                    #endregion

                    #region for perawartan kendaraan
                    /**
             * Added on 02-06-2014
             * Pemakain sparepart untuk kendaraan
             */
                    if (ddlNoPolisi.Items.Count > 0)
                    {
                        string npol = (ddlNoPolisi.SelectedValue == "0") ? string.Empty : ddlNoPolisi.SelectedItem.ToString();
                        pakaiDetail.IDKendaraan = (ddlNoPolisi.SelectedValue == "0" || ddlNoPolisi.SelectedValue == string.Empty) ? 0 : int.Parse(ddlNoPolisi.SelectedValue.ToString());
                        pakaiDetail.NoPol = (ddlNoPolisi.SelectedValue == "0") ? string.Empty : npol.Substring(0, npol.IndexOf(" -"));
                    }
                    #endregion
                    #region for zona maintenance
                    if (Session["Zona"] != null)
                    {
                        if (Session["Zona"].ToString() == "1")
                        {
                            pakaiDetail.Zona = ddlZona.SelectedValue.ToString();
                        }
                    }
                    #endregion
                    arrListPakaiDetail.Add(pakaiDetail);


                    //Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                    //GridView1.DataSource = arrListPakaiDetail;
                    //GridView1.DataBind();
                    //clearQty();

                    //new add
                    //new add
                    //new add






                }
                //iko
            }

            //iko
            //Utk auto SPB
            kd = string.Empty;
            pakai = new Pakai();

            company = new Company();
            companyFacade = new CompanyFacade();
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "B";
            PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade();
            PakaiDocNo pakaiDocNo = new PakaiDocNo();
            if (strEvent == "Insert")
            {
                //"K" for Karawang
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
            }
            pakai.PakaiNo = string.Empty;   //txtPakaiNo.Text;
            pakai.PakaiDate = DateTime.Parse(txtTanggal.Text);
            //5 for BIAYA
            pakai.PakaiTipe = 5;
            pakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            pakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            pakai.Status = 3;
            pakai.AlasanCancel = "";
            pakai.CreatedBy = ((Users)Session["Users"]).UserName;
            pakai.ItemTypeID = 3;
            //Utk auto SPB
            //iko


            ReceiptAssetProsessFacade receiptAssetProcessFacade = new ReceiptAssetProsessFacade(receipt, arrReceiptDetail, receiptDocNo, pakai, arrListPakaiDetail, pakaiDocNo);
            if (receipt.ID > 0)
            {
                #region dinonaktifkan
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
                strError = receiptAssetProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtMrsNo.Text = receiptAssetProcessFacade.ReceiptNo;
                    Session["id"] = receipt.ID;
                    Session["ReceiptNo"] = receiptAssetProcessFacade.ReceiptNo;
                }
            }

            //if (strError == string.Empty)
            //{

            //    ddlItemName.Items.Clear();
            //    //txtCariOP.ReadOnly = false;
            //    string AutoSPB = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AutoSPBBiaya", "Receipt");
            //    if (AutoSPB == "1" && chkAutoSPB.Checked == true)
            //    {
            //        //if (Session["AutoSPB"] != null)
            //        //{
            //            //ArrayList arrData = (ArrayList)Session["AutoSPB"];
            //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AutoSPB", "return Autospb();", true);
            //        //}
            //        Response.Redirect("FormPakaiBiaya.aspx?rcid=" + receiptAssetProcessFacade.ReceiptNo);
            //    }
            //    InsertLog(strEvent);
            //    btnUpdate.Enabled = true;
            //    btnPrint.Enabled = true;
            //    if (strEvent == "Edit")
            //        clearForm();

            //}
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                //if (btnCancel.Enabled == false)
                //    return;

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
                            ArrayList arrCurrentReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(rcp.ID);
                            if (receiptDetailFacade.Error == string.Empty)
                            {
                                ReceiptDetail receiptDetail1 = (ReceiptDetail)arrTransferDetail[index];
                                bool valid = false;
                                int poDetailID = 0;

                                foreach (ReceiptDetail rcpDetail in arrCurrentReceiptDetail)
                                {
                                    if (rcpDetail.PODetailID == receiptDetail1.PODetailID && rcpDetail.PODetailID == receiptDetail1.PODetailID)
                                    {
                                        //// cek stok 
                                        BiayaFacade biayaFacade = new BiayaFacade();
                                        Biaya inv = biayaFacade.RetrieveById(rcpDetail.ItemID);
                                        if (biayaFacade.Error == string.Empty)
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
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
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "B";

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
                        ArrayList arrCurrentReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(rcp.ID);
                        if (receiptDetailFacade.Error == string.Empty)
                        {
                            ArrayList arrReceiptDetail = new ArrayList();
                            foreach (ReceiptDetail rcpDetail in arrCurrentReceiptDetail)
                            {
                                if (rcp.ReceiptType != 7)
                                {
                                    InventoryFacade inventoryFacade = new InventoryFacade();
                                    Inventory inv = inventoryFacade.RetrieveByIdNew(rcpDetail.ItemID, rcpDetail.ItemTypeID);
                                    if (inventoryFacade.Error == string.Empty)
                                    {
                                        if (inv.Jumlah - rcpDetail.Quantity < 0)
                                        {
                                            DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok " + inv.ItemCode.ToString() +
                                              rcpDetail.ItemName.ToString().Substring(rcpDetail.ItemName.ToString().LastIndexOf("-")) + "  tidak mencukupi / sudah terpakai. Silahkan lakukan hapus per item");
                                            return;
                                        }
                                    }
                                }

                                arrReceiptDetail.Add(rcpDetail);
                            }

                            //ReceiptAssetProsessFacade receiptAssetProcessFacade = new ReceiptAssetProsessFacade(rcp, arrReceiptDetail, new ReceiptDocNo());
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
            eventLog.ModulName = "Entry Biaya Receipt";
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

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                SPPDetailFacade sppdetf = new SPPDetailFacade();
                SPPDetail sppdet = new SPPDetail();
                SPPFacade sppf = new SPPFacade();
                SPP spp = new SPP();
                Users userspp = new Users();
                UsersFacade usersppf = new UsersFacade();
                POPurchnDetailFacade poPurchnDetailFacade = new POPurchnDetailFacade();
                POPurchnDetail poPurchnDetail = poPurchnDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
                spp = sppf.RetrieveById(poPurchnDetail.SPPID);
                sppdet = sppdetf.RetrieveBySPPDetailID1(poPurchnDetail.SPPDetailID);
                userspp = usersppf.RetrieveByUserName(spp.CreatedBy);

                if (poPurchnDetailFacade.Error == string.Empty)
                {
                    decimal sisaPO = 0;
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    //decimal cekJumlah = receiptDetailFacade.SumPOReceiptDetail(poPurchnDetail.ItemID, txtPONo.Text);
                    decimal cekJumlah = receiptDetailFacade.SumPOReceiptDetail((poPurchnDetail.ItemID2 == 0) ? poPurchnDetail.ItemID : poPurchnDetail.ItemID2, txtPONo.Text, poPurchnDetail.ID);
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
                    txtKeterangan.Text = poPurchnDetail.CreatedBy.ToString();
                    ddlDeptName.SelectedValue = userspp.DeptID.ToString();
                    ddlJenisBiaya.SelectedValue = poPurchnDetail.ItemID.ToString();
                    int deptid = Int32.Parse(ddlDeptName.SelectedValue);

                    if (deptid == 5 || deptid == 4 || deptid == 18 || deptid == 19)
                    {
                        //ListItem li = spGroup.Items.FindByValue(sppdet.MaterialMTCGroupID.ToString());
                        ListItem li = spGroup.Items.FindByValue(sppdet.MTCGroupSarmutID.ToString());
                        if (li != null)
                            //spGroup.SelectedValue = sppdet.MaterialMTCGroupID.ToString();
                            spGroup.SelectedValue = sppdet.MTCGroupSarmutID.ToString();
                        else
                            spGroup.SelectedValue = "6";
                    }
                    #region untuk mtcProject
                    if (deptid == 19)
                    {
                        string item = ItemPo();
                        DdlMtcProject.Items.Clear();
                        ArrayList arrProject = new ArrayList();
                        MTC_ProjectFacade project = new MTC_ProjectFacade();
                        arrProject = project.RetrieveByItemDept(Int32.Parse(item));
                        foreach (MTC_Project Mtc1 in arrProject)
                        {
                            DdlMtcProject.Items.Add(new ListItem(Mtc1.Nomor, Mtc1.ID.ToString()));
                        }
                    }
                    else
                    {
                        DdlMtcProject.Items.Clear();
                    }
                    #endregion

                    bItemID.Text = poPurchnDetail.ItemID2.ToString();

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

        #region untuk mtcProject
        private string ItemPo()
        {
            string na = "";
            string strSQL = "WITH q AS (SELECT SppDetailID from POPurchnDetail where status>-1 and itemTypeID=3 and ID=" + Int32.Parse(ddlItemName.SelectedValue) + "),w AS (SELECT d.Keterangan FROM SPPDetail d, q WHERE d.ID=q.SppDetailID) SELECT b.id ItemId FROM Biaya b, w WHERE b.ItemName=w.Keterangan";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    na = sdr["ItemId"].ToString();
                }
            }
            return na;
        }
        #endregion

        private void clearQty()
        {
            txtItemCode.Text = string.Empty;
            txtQtyTerima.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtUom.Text = string.Empty;
            txtNoSpp.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            ddlItemName.SelectedIndex = 0;
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            #region validasi inputan

            try
            {
                decimal cekNumber = decimal.Parse(txtQtyTerima.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Terima harus number");
                return;
            }

            #region spGroup Tidak Boleh Kosong di dept mtn
            string dep = ddlDeptName.SelectedValue;
            if (dep == "4" || dep == "5" || dep == "18")
            {
                if (spGroup.SelectedValue == "")
                {
                    DisplayAJAXMessage(this, "spGroup Tidak Boleh Kosong");
                    return;
                }
            }
            #endregion

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
            #endregion
            //cek lastdate mrs & yang sudah yg diterima utk Item Brg Tersebut, NOT YET
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
                        if (CheckStatusBiaya() == 0)
                        {
                            /** update on 12-06-2014
                             * Receipt Biaya baru base on keterangan di spp
                             */

                            if (rcp.NoPO == txtPONo.Text && rcp.ItemCode == txtItemCode.Text &&
                                rcp.SppNo == txtNoSpp.Text && rcp.Price == decimal.Parse(txtPrice.Text))
                            {
                                DisplayAJAXMessage(this, "Sudah ada di-tabel untuk No SPP tersebut (with same price)");

                                clearQty();
                                return;
                            }
                        }
                        ada = ada + 1;
                    }
                }


            }

            #region untuk MtcProject
            int q = Int32.Parse(ddlDeptName.SelectedValue), w = 0;
            if (DdlMtcProject.SelectedValue != "") { w = Int32.Parse(DdlMtcProject.SelectedValue); }
            if (q == 19) { receiptDetail.ProjectID = w; } else { receiptDetail.ProjectID = 0; }
            #endregion

            receiptDetail.NoPO = txtPONo.Text;
            receiptDetail.ItemCode = txtItemCode.Text;
            receiptDetail.ItemName = ddlItemName.SelectedItem.ToString();
            receiptDetail.UOMCode = txtUom.Text;
            receiptDetail.Quantity = decimal.Parse(txtQtyTerima.Text);
            receiptDetail.RowStatus = 0;
            receiptDetail.Kadarair = 0;
            receiptDetail.Keterangan = txtKeterangan.Text;
            receiptDetail.ItemID2 = int.Parse(bItemID.Text);
            receiptDetail.ItemID = int.Parse(bItemID.Text);
            receiptDetail.DOSupplier = (PurchnConfig("DOSupplierAktif") == 1) ? txtNoReff.Text : string.Empty;
            if (receiptDetail.ItemID2 == 0)
            {
                DisplayAJAXMessage(this, "Kode barang belum terdaftar, hubungi IT Dept.");
                return;
            }
            POPurchnDetailFacade poPurchDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail poPurchnDetail = poPurchDetailFacade.RetrieveByDetailID(int.Parse(ddlItemName.SelectedValue));
            if (poPurchDetailFacade.Error == string.Empty && poPurchnDetail.ID > 0)
            {
                receiptDetail.Price = poPurchnDetail.Price;
                receiptDetail.ItemID = poPurchnDetail.ItemID2;
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
                receiptDetail.ItemID2 = poPurchnDetail.ItemID;

                receiptDetail.NoPol = poPurchnDetail.NoPol;

                receiptDetail.TipeAsset = 0;
            }

            //iko, for auto SPB
            receiptDetail.SarmutID = (spGroup.SelectedValue == string.Empty) ? 0 : int.Parse(spGroup.SelectedValue.ToString());

            //ikutin atau ambil itemID diatas
            receiptDetail.AvgPrice = GetLastPriceBiaya(receiptDetail.ItemID); //(GetLastPriceBiaya((CheckStatusBiaya() == 1) ? int.Parse(txtItemID.Text) : int.Parse(ddlItemName.SelectedValue)));
            receiptDetail.IDJenisBiaya = (CheckStatusBiaya() == 1) ? int.Parse(ddlJenisBiaya.SelectedValue) : 0;
            if (ddlDeptName.SelectedIndex > 0)
            {
                DeptFacade deptFacade = new DeptFacade();
                Dept dept = deptFacade.RetrieveById(int.Parse(ddlDeptName.SelectedValue));
                if (deptFacade.Error == string.Empty && dept.ID > 0)
                {
                    receiptDetail.DeptCode = dept.DeptCode;
                }
            }

            //iko, for auto SPB

            arrListReceiptDetail.Add(receiptDetail);
            Session["ListOfReceiptDetail"] = arrListReceiptDetail;

            GridView1.DataSource = arrListReceiptDetail;
            GridView1.DataBind();
            //lstBiaya.DataSource = arrListReceiptDetail;
            //lstBiaya.DataBind();
            clearQty();

        }
        private int CheckStatusBiaya()
        {
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing stat = cls.CheckBiayaAktif();

            return stat.Status;
        }
        public int PurchnConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            Domain.POPurchn status = purchn.PurchnTools(ModulName);
            return status.Status;
        }
        public void ShowInfo(string Info)
        {
            Session.Remove("Judul");
            Session.Remove("UserID");
            Session.Remove("Modul");
            SPPFacade info = new SPPFacade();
            int userid = ((Users)Session["Users"]).ID;
            int status = info.ShowInfoStatus(userid, Info);


            if (status == 0)
            {
                Session["Judul"] = "Info Update Receipt ";
                Session["UserID"] = userid.ToString();
                Session["ModulName"] = Info;
                Session["FileName"] = "UpdateReceipt.txt";
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "OpenWindows", "infoupdate()", true);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "OpenWindows", "infoupdate()", true);
            }

        }
    }
}