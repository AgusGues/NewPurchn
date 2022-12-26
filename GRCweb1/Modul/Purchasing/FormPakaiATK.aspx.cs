using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
/* komponen tambahan */
using DataAccessLayer;
using BusinessFacade;
using Domain;
using System.Data.SqlClient;

// PakaiTipe = 4 = ATK = KA

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPakaiATK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //disable button cancel untuk semua user, kecuali logistik
            DisableButtonCancel();
            //disable button cancel untuk semua user, kecuali logistik

            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                /**
                 * penambahan sistem budgeting di atk
                 * BudgetAktif=1
                 */
                string BudgetAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetAktif", "BudgetATK");
                string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AddItemAuth", "BudgetATK").Split(',');
                forbudget.Visible = (int.Parse(BudgetAktif) == 1) ? true : false;
                LoadDept();
                stk.Visible = (CheckConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                stk.Checked = (CheckConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                CheckSPBPending();
                if (Request.QueryString["PakaiNo"] != null)
                {
                    LoadPakai(Request.QueryString["PakaiNo"].ToString());
                }
                else
                {
                    clearForm();
                }
                GridView1.Visible = false;
                lbAddOP.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && BudgetAktif == "1") ? true : lbAddOP.Enabled;
            }
            else
            {
                if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                {
                    //base on grouppurchn: GroupID pada Inventory = 1 & GroupPurchn 

                    LoadItems(3);

                    txtCariNamaBrg.Text = string.Empty;
                }
            }


            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPakaiDetail"] = new ArrayList();
            Session["ListOfPakaiDetail2"] = new ArrayList();
            Session["Pakai"] = null;
            Session["PakaiNo"] = null;

            txtPakaiNo.Text = string.Empty;
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            txtUom.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtKodeDept.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtCariNamaBrg.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtUomID.Text = string.Empty;

            //ddlDeptName.Items.Clear();
            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            //else
            //    ddlDeptName.Items.Clear();

            ddlItemName.Items.Clear();

            ArrayList arrList = new ArrayList();
            lstATK.DataSource = arrList;
            lstATK.DataBind();
            ddlBulanBdg.Enabled = true;
            ddlTahunBdg.Enabled = true;

            arrList.Add(new PakaiDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = true;
            ddlDeptName.Enabled = true;

            GridView1.DataSource = arrList;
            GridView1.DataBind();

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
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
                if (CheckDeptFilter() == 0)
                {
                    ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
                else
                {
                    if (((Users)Session["Users"]).DeptID == dept.ID)
                    {
                        ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                    }
                }
            }
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDeptName.SelectedIndex > 0)
            {
                DeptFacade deptFacade = new DeptFacade();
                Dept dept = deptFacade.RetrieveById(int.Parse(ddlDeptName.SelectedValue));
                if (deptFacade.Error == string.Empty && dept.ID > 0)
                {
                    txtKodeDept.Text = dept.DeptCode;
                    txtCariNamaBrg.Focus();
                    LoadBudget(ddlBulanBdg);
                    LoadBudget(ddlTahunBdg);
                    Session["ListOfPakaiDetail2"] = null;
                    lstATK.DataSource = new ArrayList();
                    lstATK.DataBind();
                }
            }
        }

        private void LoadItems(int intGroupID)
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            string Stocked = (CheckConfig("ViewOnlyStockInSPB") == 1 && stk.Checked == true) ? " jumlah >0 and " : "";
            arrInventory = inventoryFacade.RetrieveByCriteriaWithGroupID(Stocked + "A.ItemName", txtCariNamaBrg.Text, intGroupID);
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            }
        }
        private int CheckConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn obj = purchn.PurchnTools(ModulName);
            return obj.Status;
        }
        private void LoadBudget(DropDownList ddl)
        {
            ArrayList arrData = new ArrayList();
            BudgetingFacade bdg = new BudgetingFacade();
            bdg.Pilihan = "ListBudget";
            bdg.Prefix = " distinct ";
            bdg.Field = (ddl.ID == "ddlBulanBdg") ? "Bulan" : "Tahun";
            bdg.Criteria = " and DeptID=" + ddlDeptName.SelectedValue;
            bdg.Criteria += " and Approval=2";
            arrData = bdg.Retrieve();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (Budget bg in arrData)
            {
                string val = (bdg.Field == "Bulan") ? Global.nBulan(bg.Bulan).ToString() : bg.Bulan.ToString();
                ddl.Items.Add(new ListItem(val, bg.Bulan.ToString()));
            }
            // ddl.SelectedValue = (bdg.Field == "Bulan") ? DateTime.Now.Month.ToString() : DateTime.Now.Year.ToString();
        }
        protected void ddlBulanBdg_Click(object sender, EventArgs e)
        {
            ddlTahunBdg.SelectedValue = (ddlTahunBdg.SelectedIndex == 0 && ddlTahunBdg.Items.Count > 1) ? DateTime.Now.Year.ToString() : ddlTahunBdg.SelectedValue;
            LoadItemBudget();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                // bisa cancel hanya bisa level head dept ke atas
                int intApv = ((Users)Session["Users"]).Apv;
                if (intApv > 0)
                {

                    if (btnCancel.Enabled == false)
                        return;

                    string strDocumentNo = string.Empty;
                    int intPakaiDetailID = 0;
                    string strEvent = string.Empty;

                    int index = Convert.ToInt32(e.CommandArgument);
                    ArrayList arrTransferDetail = new ArrayList();
                    arrTransferDetail = (ArrayList)Session["ListOfPakaiDetail"];

                    Pakai pakai = new Pakai();
                    if (Session["id"] != null)
                    {
                        // masuk sini krn sudah di save
                        // next job
                        // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                        int id = (int)Session["id"];

                        PakaiFacade pakaiFacade = new PakaiFacade();

                        // musti pake PakaiTipe agar bisa dibedakan No pemakaian-nya
                        // musti dipikirin utk gak bisa hapus jika accounting dah closing
                        // 3 = BAHAN BANTU= KM
                        Company company = new Company();
                        CompanyFacade companyFacade = new CompanyFacade();
                        string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";
                        //string strPakaiTipe = "KA";
                        Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                        if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                        {
                            {
                                int i = 0;
                                int x = 0;
                                PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                                ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                                if (pakaiDetailFacade.Error == string.Empty)
                                {
                                    PakaiDetail receiptDetail1 = (PakaiDetail)arrTransferDetail[index];
                                    bool valid = false;
                                    int pkiDetailID = 0;

                                    foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                                    {
                                        if (pkiDetail.ItemID == receiptDetail1.ItemID)
                                        {
                                            // cek stok
                                            InventoryFacade inventoryFacade = new InventoryFacade();
                                            Inventory inv = inventoryFacade.RetrieveById(pkiDetail.ItemID);
                                            if (inventoryFacade.Error == string.Empty)
                                            {
                                                if (inv.Jumlah - receiptDetail1.Quantity < 0)
                                                {
                                                    DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
                                                    return;
                                                }
                                            }

                                            pkiDetailID = pkiDetail.ID;
                                            i = x;
                                            valid = true;
                                            break;
                                        }

                                        x = x + 1;
                                    }

                                    if (valid == false)
                                    {
                                        arrTransferDetail.RemoveAt(index);
                                        Session["ListOfPakaiDetail"] = arrTransferDetail;
                                        GridView1.DataSource = arrTransferDetail;
                                        GridView1.DataBind();
                                    }
                                    else
                                    {
                                        PakaiDetail pakaiDetail = (PakaiDetail)arrTransferDetail[index];
                                        ArrayList arrPakaiDetail = new ArrayList();
                                        foreach (PakaiDetail pd in arrTransferDetail)
                                        {
                                            if (pd.ID == pkiDetailID)
                                            {
                                                //((ReceiptDetail)arrTransferDetail[i]).FlagPO = flagPO;
                                                arrPakaiDetail.Add(pd);
                                            }
                                        }

                                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(new Pakai(), arrPakaiDetail, new PakaiDocNo());
                                        intPakaiDetailID = pakaiDetail.ID;

                                        string strError = pakaiProcessFacade.CancelPakaiDetail();
                                        if (strError != string.Empty)
                                        {
                                            DisplayAJAXMessage(this, strError);
                                            return;
                                        }
                                        ArrayList arrTransfer = new ArrayList();

                                        foreach (PakaiDetail pd in arrTransferDetail)
                                        {
                                            //if (rd.DocumentNo != strDocumentNo)
                                            if (pd.ID != intPakaiDetailID)
                                            {
                                                arrTransfer.Add(pd);
                                            }
                                        }

                                        strEvent = "Hapus per Baris";
                                        InsertLog(strEvent);

                                        Session["ListOfPakaiDetail"] = arrTransfer;
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
                        Session["ListOfPakaiDetail"] = arrTransferDetail;

                        GridView1.DataSource = arrTransferDetail;
                        GridView1.DataBind();
                    }
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
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";
            Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KA");
        }

        private void DisableButtonCancel()
        {
            int intApv = ((Users)Session["Users"]).DeptID;
            if (intApv == 10)
            {
                btnCancel.Enabled = true;
                GridView1.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = false;
                GridView1.Enabled = false;
            }

        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            // bisa cancel hanya bisa level head dept ke atas
            int intApv = ((Users)Session["Users"]).Apv;

            if (Session["id"] != null && intApv > 0)
            {

                // masuk sini krn sudah di save
                // next job
                // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                int id = (int)Session["id"];
                string strEvent = string.Empty;

                PakaiFacade pakaiFacade = new PakaiFacade();
                // 1 = spare-part
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

                //string strPakaiTipe = "KA";
                Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                {
                    if (pki.Status >= 3)
                    { DisplayAJAXMessage(this, "Pemakaian tidak boleh di Cancel (harus di retur)"); return; }
                    if (Session["AlasanCancel"] != null)
                    {
                        pki.AlasanCancel = Session["AlasanCancel"].ToString();
                        Session["AlasanCancel"] = null;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong / blank");
                        return;
                    }

                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                    if (pakaiDetailFacade.Error == string.Empty)
                    {

                        ArrayList arrPakaiDetail = new ArrayList();
                        foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                        {

                            arrPakaiDetail.Add(pkiDetail);
                        }

                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pki, arrPakaiDetail, new PakaiDocNo());

                        string strError = pakaiProcessFacade.CancelPakai();
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

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
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

            if (txtKeterangan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Keterangan harus diisi...");
                return;
            }

            //"K" utk Karawang
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);


            #region cek closing dan tanggal akhir transaksi
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);
            string lastTrans = string.Empty;
            lastTrans = pakaiFacade.CekLastTrans(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else if (lastTrans != string.Empty || lastTrans != "")
            {
                if (nowTgl < lastTgl)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    int intApv = ((Users)Session["Users"]).Apv;
                    if (intApv == 0)
                        return;

                }
            }
            #endregion


            PakaiDetail pakaiDetail = new PakaiDetail();
            ArrayList arrListPakaiDetail = new ArrayList();

            if (Session["ListOfPakaiDetail"] != null)
            {
                arrListPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                if (arrListPakaiDetail.Count > 0)
                {
                    int ada = 0;
                    foreach (PakaiDetail pki in arrListPakaiDetail)
                    {
                        if (pki.ItemCode == txtItemCode.Text)
                        {
                            DisplayAJAXMessage(this, "Sudah ada di-tabel untuk Barang tersebut");

                            clearQty();
                            return;
                        }

                        ada = ada + 1;
                    }
                }
            }

            // cek ke re-order point
            InventoryFacade inventoryFacade = new InventoryFacade();
            Inventory inv = inventoryFacade.RetrieveById2(int.Parse(ddlItemName.SelectedValue));
            if (inventoryFacade.Error == string.Empty && inv.ID > 0)
            {
                if (inv.Jumlah - decimal.Parse(txtQtyPakai.Text) <= inv.ReOrder)
                {
                    string aMess = "Jumlah Pemakaian Barang di stock sudah melampaui Reorder Point ( " + inv.ReOrder.ToString() + " )";
                    DisplayAJAXMessage(this, aMess);
                }
            }
            //

            pakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            pakaiDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            pakaiDetail.RowStatus = 0;
            pakaiDetail.Keterangan = txtKeterangan.Text;
            pakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            pakaiDetail.UomID = int.Parse(txtUomID.Text);
            pakaiDetail.ItemCode = txtItemCode.Text;
            pakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            pakaiDetail.UOMCode = txtUom.Text;
            //pakaiDetail.ProjectID=
            //for inv
            pakaiDetail.ItemTypeID = 1;

            arrListPakaiDetail.Add(pakaiDetail);
            Session["ListOfPakaiDetail"] = arrListPakaiDetail;

            GridView1.DataSource = arrListPakaiDetail;
            GridView1.DataBind();
            lstATK.DataSource = arrListPakaiDetail;
            lstATK.DataBind();
            clearQty();

        }

        private void clearQty()
        {
            txtItemCode.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtUom.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtUomID.Text = string.Empty;
            txtGroupID.Text = string.Empty;
            //ddlItemName.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("ListOfPakaiDetail");
            Session.Remove("Pakai");
            Session.Remove("PakaiNo");

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
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            #region cek closing dan tanggal akhir transaksi 
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);
            string lastTrans = string.Empty; //untuj dept id yang blm pernah transaksi
            lastTrans = pakaiFacade.CekLastTrans(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else if (lastTrans != string.Empty || lastTrans != "")
            {
                if (nowTgl < lastTgl)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    int intApv = ((Users)Session["Users"]).Apv;
                    if (intApv == 0)
                        return;

                }
            }
            #endregion
            string strEvent = "Insert";
            pakai = new Pakai();

            if (Session["id"] != null)
            {
                #region
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
            company = new Company();
            companyFacade = new CompanyFacade();
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";

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

            pakai.PakaiNo = txtPakaiNo.Text;
            pakai.PakaiDate = DateTime.Parse(txtTanggal.Text);
            //3 for ATK
            pakai.PakaiTipe = 3;
            pakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            pakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            pakai.Status = 0;
            pakai.AlasanCancel = "";
            pakai.CreatedBy = ((Users)Session["Users"]).UserName;
            pakai.ItemTypeID = 1;

            string strError = string.Empty;
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                //cek stok ada gak 1x lagi
                foreach (PakaiDetail pkiDetail in arrPakaiDetail)
                {
                    InventoryFacade invFacade = new InventoryFacade();
                    Inventory inv = invFacade.RetrieveById(pkiDetail.ItemID);
                    if (invFacade.Error == string.Empty && inv.ID > 0)
                    {
                        if (inv.Jumlah - pkiDetail.Quantity < 0)
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
            // until here

            PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pakai, arrPakaiDetail, pakaiDocNo);
            if (pakai.ID > 0)
            {
                #region
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
                strError = pakaiProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtPakaiNo.Text = pakaiProcessFacade.PakaiNo;
                    Session["id"] = pakai.ID;
                    Session["PakaiNo"] = pakaiProcessFacade.PakaiNo;
                }
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                //txtCariOP.ReadOnly = false;

                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                btnPrint.Disabled = true;
                if (strEvent == "Edit")
                    clearForm();

            }
            LoadPakai(txtPakaiNo.Text);
            btnCancel.Enabled = false;
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Pemakaian ATK";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtPakaiNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                //nanti utk biaya & asset agak beda kayaknya

                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));
                string BudgetAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetAktif", "BudgetATK");
                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    if (inv.Head > 0 && BudgetAktif == "1" && ddlBulanBdg.SelectedValue == "0" && ddlTahunBdg.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, "Material " + inv.ItemName + " masuk di master budget\\n" +
                                                 "Pilih Periode Budget untuk SPB Material tersebut \\n\\nUntuk informasi lebih lanjut hubungi Logistik material");
                        return;
                    }
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    txtStok.Text = inv.Jumlah.ToString("N2");
                    txtGroupID.Text = inv.GroupID.ToString();

                    txtQtyPakai.Focus();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

            LoadPakai(txtSearch.Text);
            DisableButtonCancel();
        }

        private void LoadPakai(string strPakaiNo)
        {

            // here too
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "A";
            //string strPakaiTipe = "KA";
            clearForm();

            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai pakai = pakaiFacade.RetrieveByNoWithStatus(strPakaiNo, strPakaiTipe);
            if (pakaiFacade.Error == string.Empty && pakai.ID > 0)
            {
                Session["id"] = pakai.ID;

                txtPakaiNo.Text = pakai.PakaiNo;
                txtKodeDept.Text = pakai.DeptCode;
                //ddlDeptName.Text = pakai.DeptName;
                SelectDept(pakai.DeptName);
                txtTanggal.Text = pakai.PakaiDate.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = pakai.CreatedBy;

                ArrayList arrListPakaiDetail = new ArrayList();
                PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                if (pakaiDetailFacade.Error == string.Empty)
                {
                    foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                    {
                        if (pakaiDetail.ID > 0)
                            arrListPakaiDetail.Add(pakaiDetail);
                    }
                }

                Session["PakaiNo"] = pakai.PakaiNo;
                Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                GridView1.DataSource = arrListPakaiDetail;
                GridView1.DataBind();
                lstATK.DataSource = arrListPakaiDetail;
                lstATK.DataBind();
                //sehingga tdk bisa add dept yg laen
                ddlDeptName.Enabled = false;

                if (pakai.Status == -1)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                ddlBulanBdg.Enabled = false;
                ddlTahunBdg.Enabled = false;
                lbAddOP.Enabled = false;

                if (pakai.ID > 0 && pakai.Status >= 2)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                }
                else if (pakai.ID > 0 && pakai.Status == 0 || pakai.ID > 0 && pakai.Status == 1)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = true;
                }
            }
        }

        private void SelectDept(string strDeptname)
        {
            ddlDeptName.ClearSelection();
            foreach (ListItem item in ddlDeptName.Items)
            {
                if (item.Text == strDeptname)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private string ValidateText()
        {
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                if (arrPakaiDetail.Count == 0)
                    return "Tidak List Item yang di-input";
            }

            return string.Empty;
        }
        private int CheckDeptFilter()
        {
            POPurchnFacade poTools = new POPurchnFacade();
            POPurchn objTools = poTools.PurchnTools("ATKbyDept");
            return objTools.Status;
        }
        private void LoadItemBudget()
        {
            ArrayList arrListPakaiDetail = new ArrayList();
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "DetailBudget2";
            bg.Criteria = " and DeptID=" + ddlDeptName.SelectedValue.ToString();
            bg.Criteria += " and Tahun=" + ddlTahunBdg.SelectedValue.ToString();
            bg.Criteria += " and Bulan=" + ddlBulanBdg.SelectedValue.ToString();
            bg.Criteria += " and bad.Approval>=2";
            bg.Criteria2 = " and A.deptid=" + ddlDeptName.SelectedValue.ToString();
            bg.Criteria2 += " and year(A.pakaidate)=" + ddlTahunBdg.SelectedValue.ToString();
            bg.Criteria2 += " and month(A.pakaidate)=" + ddlBulanBdg.SelectedValue.ToString();
            //bg.Criteria += " and bad.PakaiDetailID is null";
            arrData = bg.Retrieve();
            decimal SPBQty = 0;
            txtCariNamaBrg.Enabled = (arrData.Count > 0) ? false : true;
            foreach (Budget bdg in arrData)
            {
                SPBQty = new PakaiDetailFacade().GetDateilSPB(bdg.PakaiDetailID).Quantity;
                if (bdg.AppvQty > SPBQty)
                {
                    PakaiDetail pakaiDetail = new PakaiDetail();
                    pakaiDetail.ItemID = bdg.ItemID;
                    pakaiDetail.Quantity = (bdg.AppvQty - SPBQty);
                    pakaiDetail.RowStatus = 0;
                    pakaiDetail.Keterangan = bdg.Keterangan;
                    pakaiDetail.GroupID = 3;
                    pakaiDetail.UomID = bdg.UomID;
                    pakaiDetail.ItemCode = bdg.ItemCode;
                    pakaiDetail.ItemName = bdg.ItemName;
                    pakaiDetail.UOMCode = bdg.UomCode;
                    pakaiDetail.BudgetID = bdg.ID;
                    pakaiDetail.Matikan = bdg.Matikan;
                    //for inv
                    pakaiDetail.ItemTypeID = 1;
                    arrListPakaiDetail.Add(pakaiDetail);
                }
            }
            //Session["ListOfPakaiDetail"] = arrListPakaiDetail;
            Session["ListOfPakaiDetail2"] = arrListPakaiDetail;
            GridView1.Visible = false;
            lstATK.DataSource = arrListPakaiDetail;
            lstATK.DataBind();
            notFound.Visible = (arrData.Count > 0) ? false : true;
        }

        protected void lstATK_DataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            chk.Checked = false;
            //chk.Enabled = false;
            string QtyEdited = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SPBPartialQty", "BudgetATK");
            TextBox txt = (TextBox)e.Item.FindControl("txtQty");
            txt.ReadOnly = (QtyEdited == "1") ? false : true;

            //ardi
            string txt2 = ((TextBox)e.Item.FindControl("txtmatikan")).Text;
            chk.Enabled = (txt2 == "yes") ? false : true;
            //ardi

        }

        protected void lstATK_Command(object sender, RepeaterCommandEventArgs e)
        {

        }
        protected void chk_change(object sender, EventArgs e)
        {

            string QtyEdited = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SPBPartialQty", "BudgetATK");
            if (Session["ListOfPakaiDetail2"] != null)
            {
                Session["Field"] = "Jumlah";
                ArrayList arrPilih = new ArrayList();
                ArrayList arrData = (ArrayList)Session["ListOfPakaiDetail2"];
                for (int i = 0; i < lstATK.Items.Count; i++)
                {
                    int Stock = 0;
                    CheckBox chk = (CheckBox)lstATK.Items[i].FindControl("chk");
                    TextBox txt = (TextBox)lstATK.Items[i].FindControl("txtQty");
                    if (chk.Checked == true)
                    {
                        Stock = new InventoryFacade().GetStock(((PakaiDetail)arrData[int.Parse(chk.ToolTip.ToString())]).ItemID, 1);

                        txtStok.Text = Stock.ToString();
                        txtItemCode.Text = ((PakaiDetail)arrData[int.Parse(chk.ToolTip.ToString())]).ItemCode.ToString();
                        decimal QtySPB = (QtyEdited == "1") ? decimal.Parse(txt.Text) : ((PakaiDetail)arrData[int.Parse(chk.ToolTip.ToString())]).Quantity;
                        if (QtySPB <= 0)
                        {
                            DisplayAJAXMessage(this, "Qty SPB tidak boleh nol (0)");
                            return;
                        }
                        if (Stock < QtySPB)
                        {
                            DisplayAJAXMessage(this, "Stock Tidak mencukupi");
                            chk.Checked = false;
                            //chk.Enabled = false;
                            return;
                        }
                        int n = 0;
                        foreach (PakaiDetail pd in arrData)
                        {
                            if (n == int.Parse(chk.ToolTip.ToString()))
                            {
                                PakaiDetail pdd = new PakaiDetail();
                                pdd = pd;
                                pdd.Quantity = QtySPB;
                                arrPilih.Add(pdd);
                            }
                            n = n + 1;
                        }

                    }

                }
                Session["ListOfPakaiDetail"] = arrPilih;
            }
        }
        private void CheckSPBPending()
        {
            PakaiFacade pf = new PakaiFacade();
            string message = string.Empty; string Statuse = "";
            ArrayList result = pf.RetrieveOpenStatus("", ((Users)Session["Users"]).DeptID.ToString());
            foreach (Pakai p in result)
            {
                Statuse = (p.Status == 0) ? "Open" : "Head";
                message += p.PakaiNo + " - " + p.Tanggal.TrimEnd() + " - Status : " + Statuse + "\\n";
            }
            if (result.Count > 0)
            {
                string msg = "alert('Untuk Sementara ini tidak bisa melakukan SPB, " +
                            "\\nMasih ada SPB yang belum di ambil sebagai berikut :\\n" + message +
                            "\\nSilahkan Hubungi Logistic Material');" +
                            "window.location.href='home.aspx'";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "SPB Alert", msg, true);
            }
        }
    }
}