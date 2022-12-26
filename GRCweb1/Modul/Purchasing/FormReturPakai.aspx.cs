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
    public partial class FormReturPakai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                //bisa diatur lewat users.DeptId
                //perlu tanya user utk per user bisa akses dept apa aja
                //penambahan untuk dept log aja yang bisa return
                string[] returnOnly = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "ReturnSPB").Split(',');
                lbAddOP.Enabled = (returnOnly.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                LoadDept();
                txtCariNamaBrg.Enabled = false;
                if (Request.QueryString["ReturNo"] != null)
                {
                    LoadPakai(Request.QueryString["ReturNo"].ToString());
                }
                else
                {
                    clearForm();
                }
            }
            else
            {
                if (txtCariNamaBrg.Text != string.Empty)
                {
                    LoadItems(1);

                    txtCariNamaBrg.Text = string.Empty;
                }
            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfReturPakaiDetail"] = new ArrayList();
            Session["ReturPakai"] = null;
            Session["ReturNo"] = null;

            txtReturNo.Text = string.Empty;
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
            txtQtyRetur.Text = string.Empty;
            txtPakaiID.Text = string.Empty;

            //ddlDeptName.Items.Clear();
            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            //else
            //    ddlDeptName.Items.Clear();

            //ddlItemName.Items.Clear();

            ArrayList arrList = new ArrayList();
            arrList.Add(new ReturPakaiDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = lbAddOP.Enabled;
            //ddlDeptName.Enabled = false;

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
                ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
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
                }
            }
        }

        private void LoadItems(int intGroupID)
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            //arrInventory = inventoryFacade.RetrieveByCriteriaWithGroupID("A.ItemName", txtCariNamaBrg.Text, intGroupID);
            arrInventory = inventoryFacade.RetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName, Inv.ID.ToString()));
            }
        }
        private void LoadItemsByPakaiDetail(int intPakaiID)
        {
            // utk ddlitem selected nya blom bisa utk asset & biaya
            // cek ke pakai krn mau di retur kan

            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            PakaiDetailFacade inventoryFacade = new PakaiDetailFacade();
            arrInventory = inventoryFacade.RetrieveByPakaiIdForAll(intPakaiID);

            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (PakaiDetail Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName, Inv.ID.ToString()));
            }
        }

        private void LoadItemsByReturDetail(int intPakaiID)
        {
            ArrayList arrInventory = new ArrayList();
            ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
            arrInventory = returPakaiDetailFacade.RetrieveItemGrid(intPakaiID);
            Session["ListOfReturPakaiDetail"] = arrInventory;
            GridView1.DataSource = arrInventory;
            GridView1.DataBind();

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
                    arrTransferDetail = (ArrayList)Session["ListOfReturPakaiDetail"];

                    Pakai pakai = new Pakai();
                    if (Session["id"] != null)
                    {
                        // masuk sini krn sudah di save
                        // next job
                        // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                        int id = (int)Session["id"];

                        ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();

                        // musti pake PakaiTipe agar bisa dibedakan No pemakaian-nya
                        // musti dipikirin utk gak bisa hapus jika accounting dah closing
                        // 1 = spare-part= KS

                        Company company = new Company();
                        CompanyFacade companyFacade = new CompanyFacade();
                        string strPakaiTipe = txtPakaiNo.Text.Substring(0, 2);
                        //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
                        //string strPakaiTipe = "KS";
                        ReturPakai pki = returPakaiFacade.RetrieveByNoWithStatus2(txtReturNo.Text, strPakaiTipe);
                        if (returPakaiFacade.Error == string.Empty && pki.ID > 0)
                        {
                            {
                                int i = 0;
                                int x = 0;
                                ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                                ArrayList arrCurrentPakaiDetail = returPakaiDetailFacade.RetrieveByReturId(pki.ID);
                                if (returPakaiDetailFacade.Error == string.Empty)
                                {
                                    ReturPakaiDetail receiptDetail1 = (ReturPakaiDetail)arrTransferDetail[index];
                                    bool valid = false;
                                    int pkiDetailID = 0;

                                    foreach (ReturPakaiDetail pkiDetail in arrCurrentPakaiDetail)
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
                                        Session["ListOfReturPakaiDetail"] = arrTransferDetail;
                                        GridView1.DataSource = arrTransferDetail;
                                        GridView1.DataBind();
                                    }
                                    else
                                    {
                                        ReturPakaiDetail returPakaiDetail = (ReturPakaiDetail)arrTransferDetail[index];
                                        ArrayList arrPakaiDetail = new ArrayList();
                                        foreach (ReturPakaiDetail pd in arrTransferDetail)
                                        {
                                            if (pd.ID == pkiDetailID)
                                            {
                                                //((ReceiptDetail)arrTransferDetail[i]).FlagPO = flagPO;
                                                arrPakaiDetail.Add(pd);
                                            }
                                        }

                                        ReturPakaiProcessFacade returPakaiProcessFacade = new ReturPakaiProcessFacade(new ReturPakai(), arrPakaiDetail);
                                        intPakaiDetailID = returPakaiDetail.ID;

                                        string strError = returPakaiProcessFacade.CancelPakaiDetail();
                                        if (strError != string.Empty)
                                        {
                                            DisplayAJAXMessage(this, strError);
                                            return;
                                        }
                                        ArrayList arrTransfer = new ArrayList();

                                        foreach (ReturPakaiDetail pd in arrTransferDetail)
                                        {
                                            if (pd.ID != intPakaiDetailID)
                                            {
                                                arrTransfer.Add(pd);
                                            }
                                        }

                                        strEvent = "Hapus per Baris";
                                        InsertLog(strEvent);

                                        Session["ListOfReturPakaiDetail"] = arrTransfer;
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
                        Session["ListOfReturPakaiDetail"] = arrTransferDetail;

                        GridView1.DataSource = arrTransferDetail;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, "Hanya login head ke atas yang bisa hapus !!");
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
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

            //Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KS");
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

                ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();
                // 1 = spare-part
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string strPakaiTipe = txtPakaiNo.Text.Substring(0, 2);
                //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

                //string strPakaiTipe = "KS";
                ReturPakai pki = returPakaiFacade.RetrieveByNoWithStatus2(txtReturNo.Text, strPakaiTipe);
                if (returPakaiFacade.Error == string.Empty && pki.ID > 0)
                {
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

                    ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                    ArrayList arrCurrentPakaiDetail = returPakaiDetailFacade.RetrieveByReturId(pki.ID);
                    if (returPakaiDetailFacade.Error == string.Empty)
                    {

                        ArrayList arrPakaiDetail = new ArrayList();
                        foreach (ReturPakaiDetail pkiDetail in arrCurrentPakaiDetail)
                        {
                            //InventoryFacade inventoryFacade = new InventoryFacade();
                            //Inventory inv = inventoryFacade.RetrieveById(pkiDetail.ItemID);
                            //if (inventoryFacade.Error == string.Empty)
                            //{
                            //    if (inv.Jumlah - pkiDetail.Quantity < 0)
                            //    {
                            //        DisplayAJAXMessage(this, "Tidak dapat dihapus karena Stok tidak mencukupi / sudah terpakai");
                            //        return;
                            //    }
                            //}

                            arrPakaiDetail.Add(pkiDetail);
                        }

                        ReturPakaiProcessFacade returPakaiProcessFacade = new ReturPakaiProcessFacade(pki, arrPakaiDetail);

                        string strError = returPakaiProcessFacade.CancelPakai();
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
                decimal cekNumber = decimal.Parse(txtQtyRetur.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Pakai harus number");
                return;
            }
            if (decimal.Parse(txtQtyRetur.Text) == 0)
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
            if (decimal.Parse(txtQtyPakai.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                return;
            }

            if (decimal.Parse(txtQtyRetur.Text) > decimal.Parse(txtQtyPakai.Text))
            {
                DisplayAJAXMessage(this, "Quantity Retur lebih besar dari pada Quantity Pemakaian");
                return;
            }
            if (ddlItemName.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Barang dahulu ....!");
                return;
            }

            //"K" utk Karawang
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = txtPakaiNo.Text.Substring(0, 2);
            //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            //string strPakaiCode = "KS" + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            ReturPakai returPakai = new ReturPakai();
            ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();

            if (Session["ListOfReturPakaiDetail"] == null)
            {
                ReturPakai lastEntry = returPakaiFacade.CekLastDate(strPakaiCode);
                DateTime lastTgl = new DateTime(lastEntry.ReturDate.Year, lastEntry.ReturDate.Month, lastEntry.ReturDate.Day);

                if (nowTgl < lastTgl)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");

                    //Level Approval pd table Users = level head dept or more up
                    //ada warning tapi bisa lewat so bisa entry tgl mundur
                    //int intApv = ((Users)Session["Users"]).Apv;
                    //if (intApv == 0)
                    return;

                }
                //cek lastdate mrs
            }

            ReturPakaiDetail returPakaiDetail = new ReturPakaiDetail();
            ArrayList arrListPakaiDetail = new ArrayList();

            if (Session["ListOfReturPakaiDetail"] != null)
            {
                arrListPakaiDetail = (ArrayList)Session["ListOfReturPakaiDetail"];
                if (arrListPakaiDetail.Count > 0)
                {
                    int ada = 0;
                    foreach (ReturPakaiDetail pki in arrListPakaiDetail)
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

            returPakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            returPakaiDetail.Quantity = decimal.Parse(txtQtyRetur.Text);
            returPakaiDetail.RowStatus = 0;
            returPakaiDetail.Keterangan = txtKeterangan.Text;
            returPakaiDetail.UomID = int.Parse(txtUomID.Text);
            returPakaiDetail.ItemCode = txtItemCode.Text;
            returPakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            returPakaiDetail.UOMCode = txtUom.Text;
            returPakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            //for inv only = 1, next pick from pakai
            returPakaiDetail.ItemTypeID = 1;

            arrListPakaiDetail.Add(returPakaiDetail);
            Session["ListOfReturPakaiDetail"] = arrListPakaiDetail;

            GridView1.DataSource = arrListPakaiDetail;
            GridView1.DataBind();

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
            txtQtyRetur.Text = string.Empty;

            ddlItemName.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
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
            ReturPakai returPakai = new ReturPakai();

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

                //strEvent = "Edit";
                #endregion
            }
            //here for document number
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = txtPakaiNo.Text.Substring(0, 2);
            #region
            //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

            //PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade();
            //PakaiDocNo pakaiDocNo = new PakaiDocNo();
            //if (strEvent == "Insert")
            //{
            //    //"K" for Karawang
            //    pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, kd);
            //    //pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, "KS");
            //    if (pakaiDocNo.ID == 0)
            //    {
            //        noBaru = 1;
            //        pakaiDocNo.PakaiCode = kd;
            //        //pakaiDocNo.PakaiCode = "KS";
            //        pakaiDocNo.NoUrut = 1;
            //        pakaiDocNo.MonthPeriod = bln;
            //        pakaiDocNo.YearPeriod = thn;
            //    }
            //    else
            //    {
            //        noBaru = pakaiDocNo.NoUrut + 1;
            //        pakaiDocNo.PakaiCode = kd;
            //        //pakaiDocNo.PakaiCode = "KS";
            //        pakaiDocNo.NoUrut = pakaiDocNo.NoUrut + 1;
            //    }
            //}
            #endregion
            returPakai.PakaiNo = txtPakaiNo.Text;
            returPakai.PakaiID = int.Parse(txtPakaiID.Text);
            returPakai.ReturDate = DateTime.Parse(txtTanggal.Text);

            //1 for spare part
            // coba ke ddlDeptName utk pakaitipe nya
            returPakai.PakaiTipe = int.Parse(ddlDeptName.SelectedValue);

            returPakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            returPakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            returPakai.Status = 0;
            returPakai.AlasanCancel = "";
            returPakai.CreatedBy = ((Users)Session["Users"]).UserName;

            if (txtPakaiNo.Text.Substring(1, 1) == "C")
                returPakai.ItemTypeID = 2;
            else if (txtPakaiNo.Text.Substring(1, 1) == "B")
                returPakai.ItemTypeID = 3;
            else
                returPakai.ItemTypeID = 1;

            string strError = string.Empty;
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfReturPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfReturPakaiDetail"];
                #region
                //gak perlu cek stok
                //foreach (ReturPakaiDetail pkiDetail in arrPakaiDetail)
                //{
                //    InventoryFacade invFacade = new InventoryFacade();
                //    Inventory inv = invFacade.RetrieveById(pkiDetail.ItemID);
                //    if (invFacade.Error == string.Empty && inv.ID > 0)
                //    {
                //        if (inv.Jumlah - pkiDetail.Quantity < 0)
                //        {
                //            string strItemCode = inv.ItemCode;
                //            string strMessage = "Kode Barang " + strItemCode + " tidak mencukupi stok-nya...!";
                //            DisplayAJAXMessage(this, strMessage);

                //            clearQty();
                //            return;
                //        }
                //    }
                //}
                #endregion
            }
            // until here

            ReturPakaiProcessFacade returPakaiProcessFacade = new ReturPakaiProcessFacade(returPakai, arrPakaiDetail);
            if (returPakai.ID > 0)
            {
                #region depreciated line
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
                strError = returPakaiProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtReturNo.Text = returPakaiProcessFacade.ReturNo;
                    Session["id"] = returPakai.ID;
                    Session["ReturNo"] = returPakaiProcessFacade.ReturNo;
                }
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                btnPrint.Disabled = true;
                if (strEvent == "Edit")
                    clearForm();

            }
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Retur Pemakaian";
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

                // utk ambil qty pakai-nya
                PakaiFacade pakaiFacade = new PakaiFacade();
                Pakai pakai = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, txtPakaiNo.Text.Substring(0, 2));
                if (pakaiFacade.Error == string.Empty && pakai.ID > 0)
                {
                    //for inventori only
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                    foreach (PakaiDetail pkiDetail in arrPakaiDetail)
                    {
                        if (pkiDetail.ItemID == int.Parse(ddlItemName.SelectedValue))
                        {
                            txtQtyPakai.Text = pkiDetail.Quantity.ToString("N2");

                            break;
                        }
                    }
                }

                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));
                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    txtStok.Text = inv.Jumlah.ToString("N2");
                    txtGroupID.Text = inv.GroupID.ToString();

                    txtQtyRetur.Focus();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //string strQuery = string.Empty;
            if (txtSearch.Text != string.Empty)
            {
                if (ddlSearch.SelectedIndex == 0)
                {
                    //strQuery = "where PakaiNo = '" + txtSearch.Text + "'";
                    LoadPakai(txtSearch.Text);

                }
                else if (ddlSearch.SelectedIndex == 1)
                {
                    //strQuery = "where ReturNo = '" + txtSearch.Text + "'";
                    LoadRetur(txtSearch.Text);
                }

            }

            //LoadPakai(strQuery);
        }

        private void LoadPakai(string strPakaiNo)
        {
            // here too
            //Company company = new Company();
            //CompanyFacade companyFacade = new CompanyFacade();

            string strPakaiTipe = strPakaiNo.Substring(0, 2);
            //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            //string strPakaiTipe = "KS";

            clearForm();

            //utk panggil data pakai tuk di retur
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai pakai = pakaiFacade.RetrieveByNoWithStatus(strPakaiNo, strPakaiTipe);
            if (pakaiFacade.Error == string.Empty && pakai.ID > 0)
            {
                Session["id"] = pakai.ID;

                txtPakaiNo.Text = pakai.PakaiNo;
                txtKodeDept.Text = pakai.DeptCode;
                SelectDept(pakai.DeptName);
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = pakai.CreatedBy;
                txtPakaiID.Text = pakai.ID.ToString();

                LoadItemsByPakaiDetail(pakai.ID);
                #region
                //ArrayList arrListPakaiDetail = new ArrayList();
                //PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                //ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                //if (pakaiDetailFacade.Error == string.Empty)
                //{
                //    foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                //    {
                //        if (pakaiDetail.ID > 0)
                //            arrListPakaiDetail.Add(pakaiDetail);
                //    }
                //}
                //Session["ReturNo"] = pakai.PakaiNo;
                //Session["ListOfReturPakaiDetail"] = arrListPakaiDetail;
                //GridView1.DataSource = arrListPakaiDetail;
                //GridView1.DataBind();

                //sehingga tdk bisa add dept yg laen

                //ddlDeptName.Enabled = false;
                #endregion
                if (pakai.Status == -1)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                //if (pakai.Status > 0)
                if (pakai.ID > 0)
                {
                    //lbAddOP.Enabled = false;
                    //btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                }

            }
        }

        private void LoadRetur(string strReturNo)
        {
            // here too
            //Company company = new Company();
            //CompanyFacade companyFacade = new CompanyFacade();
            int intApv = ((Users)Session["Users"]).Apv;


            string strPakaiTipe = strReturNo.Substring(0, 2);
            //companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            //string strPakaiTipe = "KS";

            clearForm();

            //utk panggil data pakai tuk di retur
            ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();
            ReturPakai returPakai = returPakaiFacade.RetrieveByNoWithStatus(strReturNo, strPakaiTipe);
            if (returPakaiFacade.Error == string.Empty && returPakai.ID > 0)
            {
                Session["id"] = returPakai.ID;

                txtPakaiNo.Text = returPakai.PakaiNo;
                txtKodeDept.Text = returPakai.DeptCode;
                SelectDept(returPakai.DeptName);
                txtTanggal.Text = returPakai.ReturDate.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = returPakai.CreatedBy;
                txtPakaiID.Text = returPakai.ID.ToString();
                txtReturNo.Text = returPakai.ReturNo;
                Session["ReturNo"] = returPakai.ReturNo;
                // LoadItemsByPakaiDetail(returPakai.ID);
                LoadItemsByReturDetail(returPakai.ID);
                #region
                //ArrayList arrListPakaiDetail = new ArrayList();
                //PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                //ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                //if (pakaiDetailFacade.Error == string.Empty)
                //{
                //    foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                //    {
                //        if (pakaiDetail.ID > 0)
                //            arrListPakaiDetail.Add(pakaiDetail);
                //    }
                //}
                //Session["ReturNo"] = pakai.PakaiNo;
                //Session["ListOfReturPakaiDetail"] = arrListPakaiDetail;
                //GridView1.DataSource = arrListPakaiDetail;
                //GridView1.DataBind();

                //sehingga tdk bisa add dept yg laen

                //ddlDeptName.Enabled = false;
                #endregion
                if (returPakai.Status == -1)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                //if (pakai.Status > 0)
                if (returPakai.ID > 0)
                {
                    //lbAddOP.Enabled = false;
                    //btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                    if (intApv > 0)
                    {
                        btnCancel.Enabled = true;
                    }
                    else
                    {
                        btnCancel.Enabled = false;
                    }
                }

            }
            else
            {
                DisplayAJAXMessage(this, "Data tidak di temukan, kemungkinan sudah dicancel !!");
                return;
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
            if (Session["ListOfReturPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfReturPakaiDetail"];
                if (arrPakaiDetail.Count == 0)
                    return "Tidak List Item yang di-input";
            }

            return string.Empty;
        }


    }
}