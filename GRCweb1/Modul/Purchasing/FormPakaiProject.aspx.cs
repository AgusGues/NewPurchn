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
    public partial class FormPakaiProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                //bisa diatur lewat users.DeptId
                //perlu tanya user utk per user bisa akses dept apa aja
                LoadDept();
                //CheckSPBPending();
                if (ProjectNewAktif("ProjectNewAktif") == 1) { LoadProject(); }
                LoadGroupPM();
                if (Request.QueryString["PakaiNo"] != null)
                {
                    LoadPakai(Request.QueryString["PakaiNo"].ToString());
                }
                else
                {
                    clearForm();
                }
            }
            else
            {
                if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                {
                    //base on grouppurchn: GroupID pada Inventory = 1 & GroupPurchn 

                    LoadItems(6);

                    txtCariNamaBrg.Text = string.Empty;
                }
            }
            //sarmut dropdown
            int deptID = ((Users)Session["Users"]).DeptID;
            ddlSpGroup.Enabled = (deptID == 4 || deptID == 5 || deptID == 18) ? true : false;
            projectNew.Visible = (ProjectNewAktif("ProjectNewAktif") == 1) ? true : false;
            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPakaiDetail"] = new ArrayList();
            Session["Pakai"] = null;
            Session["PakaiNo"] = null;
            Session["ListPakaiProject"] = new ArrayList();
            Session["ListSarmut"] = new ArrayList();

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
                if (ProjectNewAktif("ProjectByDept") == 1)
                {
                    if (dept.ID == ((Users)Session["Users"]).DeptID)
                    {
                        ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                    }
                }
                else
                {
                    ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
            }
        }

        private void LoadProject()
        {
            /**
              * Project Engineering
              * added on 01-07-2014
              */
            Session["OrderBy"] = "Order by ProjectName,SubProject,ID";
            ArrayList arrProject = new ArrayList();
            MTC_ProjectFacade project = new MTC_ProjectFacade();
            int Dep = (ProjectNewAktif("ProjectByDept") == 1) ? ((Users)Session["Users"]).DeptID : 0;
            arrProject = project.RetrieveByDept(Dep);
            ddlProjectName.Items.Add(new ListItem("--Pilih Project--", "0"));
            foreach (MTC_Project proj in arrProject)
            {

                string ProjectName = proj.SubProjectName;
                if (ProjectName == string.Empty)
                {
                    ListItem lst = new ListItem(proj.NamaProject, proj.ID.ToString());
                    ddlProjectName.Items.Add(lst);
                }
                else
                {
                    string img = HttpContext.Current.Request.ApplicationPath.ToString() + "/images/dots.gif";
                    ListItem lst = new ListItem("   " + proj.SubProjectName, proj.ID.ToString());
                    lst.Attributes.Add("style", "color:red");
                    ddlProjectName.Items.Add(lst);
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
                }

                if (int.Parse(ddlDeptName.SelectedValue) == 4 ||
                    int.Parse(ddlDeptName.SelectedValue) == 5 ||
                    int.Parse(ddlDeptName.SelectedValue) == 18)
                {
                    ddlSpGroup.Enabled = true;
                }
                else
                {
                    ddlSpGroup.Enabled = false;
                }
            }
        }

        private void LoadItems(int intGroupID)
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrInventory = inventoryFacade.RetrieveByCriteriaWithGroupID("A.ItemName", txtCariNamaBrg.Text, intGroupID);
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));

            }
        }

        private void LoadGroupPM()
        {
            ArrayList arrPM = new ArrayList();
            MTC_ZonaFacade PM = new MTC_ZonaFacade();
            arrPM = PM.RetrieveSpGroup();
            ddlSpGroup.Items.Add(new ListItem(" ", "0"));
            foreach (MTC_Zona uPm in arrPM)
            {
                ddlSpGroup.Items.Add(new ListItem(uPm.ZonaName, uPm.ID.ToString()));
            }
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
                        string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";
                        //string strPakaiTipe = "KO";
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
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";
            Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KO");
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
                string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";
                //string strPakaiTipe = "KO";
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
            #region proses validasi data
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
            //filter multigudang
            if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text))
            {
                DisplayAJAXMessage(this, "Sebagian Quantity Stock sudah dipesan oleh departemen lain, coba kurangi Qty pakainya");
                return;
            }
            if (ddlProjectName.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Nama Project Belum dipilih");
                return;
            }
            #endregion
            ddlDeptName.Enabled = false;
            //end filter multigudang
            //"K" utk Karawang
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project").Split(new string[] { "," }, StringSplitOptions.None);

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            #region Validasi tanggal input dan closing periode
            //cek tgl terakhir input
            //jika closing periode sudah aktif tgl akhir input tidak dipakai;
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl && ProjectNewAktif("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    return;

                }
            }
            #endregion
            //cek lastdate mrs
            #region Double data dalam satu penginputan
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
            #endregion
            // cek ke re-order point
            #region validasi Reorder Point
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
            #endregion
            pakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            pakaiDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            pakaiDetail.RowStatus = 0;
            pakaiDetail.Keterangan = txtKeterangan.Text;
            pakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            pakaiDetail.UomID = int.Parse(txtUomID.Text);
            pakaiDetail.ItemCode = txtItemCode.Text;
            pakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            pakaiDetail.UOMCode = txtUom.Text;
            //for inv
            pakaiDetail.ItemTypeID = 1;
            //pakaiDetail.ProjectID = (ProjectNewAktif("ProjectNewAktif") == 1) ? int.Parse(ddlProjectName.SelectedValue) : 0;
            //pakaiDetail.ProjectName =(ProjectNewAktif("ProjectNewAktif") == 1) ? ddlProjectName.SelectedItem.ToString():string.Empty;
            pakaiDetail.SarmutID = (ddlSpGroup.SelectedValue == string.Empty) ? 0 : int.Parse(ddlSpGroup.SelectedValue.ToString());
            #region for New project
            pakaiDetail.ProjectID = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDept.Contains(ddlDeptName.SelectedValue.ToString()) || arrDept.Contains("All"))) ? int.Parse(ddlProjectName.SelectedValue) : 0;
            pakaiDetail.ProjectName = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDept.Contains(ddlDeptName.SelectedValue.ToString()) || arrDept.Contains("All"))) ? ddlProjectName.SelectedItem.ToString() : string.Empty;

            #endregion
            arrListPakaiDetail.Add(pakaiDetail);
            Session["ListOfPakaiDetail"] = arrListPakaiDetail;
            #region Proses Data Sarmut
            MTC_Sarmut sarmut = new MTC_Sarmut();
            ArrayList arrListSarmut = new ArrayList();
            if (int.Parse(ddlDeptName.SelectedValue) == 4 ||
                int.Parse(ddlDeptName.SelectedValue) == 5 ||
                int.Parse(ddlDeptName.SelectedValue) == 18)
            {
                sarmut.ID = (ddlSpGroup.SelectedValue == string.Empty) ? 0 : int.Parse(ddlSpGroup.SelectedValue.ToString());
                sarmut.ItemID = int.Parse(ddlItemName.SelectedValue);
                sarmut.DeptCode = txtKodeDept.Text;
                sarmut.DeptID = int.Parse(ddlDeptName.SelectedValue.ToString());
                sarmut.Qty = decimal.Parse(txtQtyPakai.Text);
                sarmut.AvgPrice = 0;
                sarmut.SPBDate = Convert.ToDateTime(txtTanggal.Text);
                sarmut.SarmutID = int.Parse(ddlSpGroup.SelectedValue.ToString());
                arrListSarmut.Add(sarmut);


                Session["ListSarmut"] = arrListSarmut;
            }
            #endregion

            #region Proses Data Project
            if (ProjectNewAktif("ProjectNewAktif") == 1)
            {
                MTC_ProjectPakai objProject = new MTC_ProjectPakai();
                ArrayList arrProject = new ArrayList();
                objProject.ProjectID = int.Parse(ddlProjectName.SelectedValue);
                objProject.PakaiID = 0;
                objProject.ItemID = int.Parse(ddlItemName.SelectedValue);
                objProject.GroupID = int.Parse(txtGroupID.Text);
                objProject.ItemTypeID = int.Parse(txtItemTypeID.Text);
                objProject.Qty = Convert.ToDecimal(txtQtyPakai.Text);
                objProject.AvgPrice = decimal.Parse(txtAvgPrice.Text);
                arrProject.Add(objProject);
                Session["ListPakaiProject"] = arrProject;
            }
            #endregion
            #region CheckMaximalQty SPB - budget consummable
            PlanningFacade pd = new PlanningFacade();
            decimal checkCost = pd.MaterialBudgetBM(ddlItemName.SelectedValue, PlanningProd());
            if (ddlDeptName.SelectedValue == "2" && checkCost > 0)
            {
                if (MasihBolehSPB(decimal.Parse(txtQtyPakai.Text), true) == false)
                {
                    decimal sisabd = 0;
                    sisabd = (SisaSPB(true) <= 0) ? 0 : SisaSPB(true);
                    DisplayAJAXMessage(this, "Sisa SPB untuk bulan ini tinggal : " + sisabd.ToString("N2") + " lagi.\\n" +
                                             "Max SPB : " + MaxSPB(true).ToString("N2") + " Untuk Running " + PlanningProd().ToString() +
                                             " Line\\nSilahkan Hubungi Logistik Material");
                    return;
                }
            }
            else
            {
                if (MasihBolehSPB(decimal.Parse(txtQtyPakai.Text)) == false && isMaterialBudget(int.Parse(ddlItemName.SelectedValue)) == true)
                {
                    decimal sisa = (SisaSPB() <= 0) ? 0 : SisaSPB();
                    DisplayAJAXMessage(this, "Sisa SPB untuk bulan ini tinggal : " + sisa.ToString("###,###.#0") + " lagi.\\n" +
                                             "Max SPB : " + MaxSPB().ToString("N2") + "\\nSilahkan Hubungi Logistik Material");
                    return;
                }
            }
            #endregion
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
            ddlItemName.SelectedIndex = 0;

        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("ListOfPakaiDetail");
            Session.Remove("Pakai");
            Session.Remove("PakaiNo");
            Session.Remove("ListSarmut");
            Session.Remove("ListPakaiProject");
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
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            #region Validasi Closing Periode dan Tgl Akhir transaksi
            /**
             * jika peroide closing sudah aktif
             * cek tgl input terakhir tidak diperlukan lagi
             */
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    #region
                    //Level Approval pd table Users = level head dept or more up
                    //ada warning tapi bisa lewat so bisa entry tgl mundur
                    //int intApv = ((Users)Session["Users"]).Apv;
                    //if (intApv == 0)
                    #endregion
                    return;

                }
            }
            #endregion

            string strEvent = "Insert";
            pakai = new Pakai();

            if (Session["id"] != null)
            {
                #region baris dibawah ini sengaja di nonaktifkan
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
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";

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
            //6 for Project
            pakai.PakaiTipe = 6;
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
                #region line non aktif
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
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Pemakaian Project";
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
                SaldoInventoryFacade price = new SaldoInventoryFacade();
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));


                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    int AvgPrice = price.GetPrice(inv.ID, getMonthName(int.Parse(DateTime.Parse(txtTanggal.Text).Month.ToString())), DateTime.Parse(txtTanggal.Text).Year, inv.ItemTypeID);
                    txtGroupID.Text = inv.GroupID.ToString();
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    decimal jml = inv.Jumlah - StockPrivate();
                    txtStok.Text = jml.ToString("N2");
                    txtItemTypeID.Text = inv.ItemTypeID.ToString();
                    txtAvgPrice.Text = AvgPrice.ToString("N2");
                    txtQtyPakai.Focus();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadPakai(txtSearch.Text);
        }

        private void LoadPakai(string strPakaiNo)
        {
            // here too
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "O";
            //string strPakaiTipe = "KO";
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

                //sehingga tdk bisa add dept yg laen
                ddlDeptName.Enabled = false;

                if (pakai.Status == -1)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                //if (pakai.Status > 0)
                if (pakai.ID > 0 && pakai.Status > 1)
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
        private string DeptName(int DeptID)
        {
            DeptFacade dept = new DeptFacade();
            Dept nDept = dept.RetrieveById(DeptID);
            return nDept.DeptName;
        }

        private Decimal StockPrivate()
        {
            SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
            decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
            return StockOtherDept;
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCariNamaBrg.Focus();
            MTC_Project objP = new MTC_Project();
            MTC_ProjectFacade Project = new MTC_ProjectFacade();
            objP = Project.RetrieveByID(int.Parse(ddlProjectName.SelectedValue));
            txtProgres.Text = objP.Progress.ToString();
        }

        protected string getMonthName(int Bulan)
        {
            SaldoInventoryFacade Bln = new SaldoInventoryFacade();
            return Bln.GetStrMonth(Bulan);
        }
        protected int ProjectNewAktif(string Keterangan)
        {
            POPurchnFacade poTools = new POPurchnFacade();
            POPurchn objTools = poTools.PurchnTools(Keterangan);
            return objTools.Status;
        }

        public void ShowInfo(string Info, string FileName)
        {
            Session.Remove("Judul");
            Session.Remove("UserID");
            Session.Remove("Modul");
            SPPFacade info = new SPPFacade();
            int userid = ((Users)Session["Users"]).ID;
            int status = info.ShowInfoStatus(userid, Info);


            if (status == 0)
            {
                Session["Judul"] = "Info Update SPB ";
                Session["UserID"] = userid.ToString();
                Session["ModulName"] = Info;
                Session["FileName"] = FileName;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "OpenWindows", "infoupdate()", true);
            }

        }
        private void CheckSPBPending(bool baru)
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
        private decimal MaxSPB()
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            string periodeSPB = DateTime.Parse(txtTanggal.Text).ToString("yyyyMM");
            decimal TotalSPB = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            decimal MaxQtySPB = pkd.MaxQtySPB(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = MaxQtySPB;
            return result;

        }
        private decimal MaxSPB(bool BM)
        {
            //setingan jumlah max line di plant  yng digunakan untuk membuat bidgetsp
            Users user = (Users)Session["Users"];
            string JmlLine = "1";
            JmlLine = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LinePlant" + user.UnitKerjaID, "CostControl");
            decimal result = 0;
            PlanningFacade pp = new PlanningFacade();
            int RunningLine = this.PlanningProd();
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            result = pp.MaterialBudgetBM(ddlItemName.SelectedValue, RunningLine);
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = (MaxSPB() > 0) ? Math.Round(((MaxSPB() / decimal.Parse(JmlLine)) * RunningLine), 0, MidpointRounding.AwayFromZero) : result;
            result = (result == 0) ? MaxSPB() : (Rule > 1) ? MaxSPB() : result;
            return result;
        }

        private int PlanningProd()
        {
            int runLine = 2;
            PlanningFacade pp = new PlanningFacade();
            ArrayList arrPP = new ArrayList();
            string Criteria = "";
            arrPP = pp.Retrieve(Criteria, true);
            foreach (Planning p in arrPP)
            {
                runLine = int.Parse(p.RunningLine);
            }
            return runLine;
        }
        private decimal TotalSPB()
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            result = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            return result;
        }
        private bool MasihBolehSPB(decimal QtySkr)
        {
            bool result = false;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            decimal TotalSPB = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            decimal MaxQtySPB = pkd.MaxQtySPB(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = ((TotalSPB + QtySkr) <= MaxQtySPB) ? true : false;
            return result;
        }
        private bool MasihBolehSPB(decimal QtySkr, bool forBM)
        {
            bool result = false;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            decimal TotalSPB = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            decimal MaxQtySPB = (Rule == 12) ? this.MaxSPB() : this.MaxSPB(true);
            decimal addBudget = pkd.AddQtyBudget(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = ((TotalSPB + QtySkr) <= (MaxQtySPB + addBudget)) ? true : false;
            return result;
        }
        private string PeriodeRule(int Rule)
        {
            string result = string.Empty;
            switch (Rule)
            {
                case 6:
                    switch (DateTime.Parse(txtTanggal.Text).Month)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            result = DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "01' and '" + DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "06";
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                            result = DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "07' and '" + DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "12";
                            break;
                    }
                    break;
                case 12:
                    result = DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "01' and '" + DateTime.Parse(txtTanggal.Text).ToString("yyyy") + "12";
                    break;
                default:
                    result = DateTime.Parse(txtTanggal.Text).ToString("yyyyMM") + "' and '" + DateTime.Parse(txtTanggal.Text).ToString("yyyyMM");
                    break;
            }
            return result;
        }
        private decimal SisaSPB()
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            decimal TotalSPB = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            decimal MaxQtySPB = pkd.MaxQtySPB(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = MaxQtySPB - TotalSPB;
            return result;
        }
        private decimal SisaSPB(bool BM)
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            PlanningFacade pp = new PlanningFacade();

            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            decimal TotalSPB = pkd.TotalQtySPB(periodeSPB, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            decimal MaxQtySPB = MaxSPB(true);
            decimal addBudget = pkd.AddQtyBudget(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = MaxQtySPB - TotalSPB + addBudget;
            return result;
        }
        private bool isMaterialBudget(int ItemID)
        {
            bool result = false;
            string Aktife = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetAktif", "BudgetConsumable");
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            InventoryFacade inv = new InventoryFacade();
            Inventory pd = inv.RetrieveById(ItemID);
            result = (pd.Head == 5 && Aktife == "1") ? true : false;
            return result;

        }
        private string TipeBudget()
        {
            string result = string.Empty;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            string Periode = Tahun.ToString() + "01";
            int rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            switch (rule)
            {
                case 12:
                    result = "Tahunan";
                    break;
                case 6:
                    result = "Semesteran";
                    break;
                default:
                    result = "Bulanan";
                    break;
            }
            return result;
        }
        private decimal AddBudget()
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            decimal MaxQtySPB = pkd.AddQtyBudget(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = MaxQtySPB;
            return result;
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


        private void DisableAllControl()
        {
            foreach (DropDownList d in this.Page.Controls)
            {
                d.Enabled = false;
            }
            foreach (TextBox t in this.Page.Controls)
            {
                t.Enabled = false;
            }
            foreach (Button b in UpdatePanel1.Controls)
            {
                b.Enabled = false;
            }
        }
    }
}