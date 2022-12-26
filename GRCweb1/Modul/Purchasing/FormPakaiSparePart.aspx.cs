using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPakaiSparePart : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LogActivity("SPB SparePart", true);
                Global.link = "~/Default.aspx";
                
                Session["AlasanCancel"] = null;
                Session["AlasanBatal"] = null;
                Session["AlasanTolak"] = null;

                Session["OrderBy"] = "";
                LoadDept();
                CheckSPBPending();
                LoadSatuan();
                LoadGroupPM();
                txtUomID.Enabled = false;
                stk.Visible = (CheckConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                stk.Checked = (CheckConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                if (Request.QueryString["PakaiNo"] != null)
                {
                    LoadPakai(Request.QueryString["PakaiNo"].ToString());
                }
                else
                {
                    clearForm();
                }
                spGroup.Enabled = false;
                x1.Visible = false;
                x2.Visible = false;
                spZona.Attributes.Add("display", "none");
                LoadListLockSPP();
                CheckReOrderPoint();
                CheckBox1.Text = (((Users)Session["Users"]).UnitKerjaID == 7) ? "Citeureup" : "Karawang";
                PanelPalet.Visible = false;
            }
            else
            {
                string[] OnProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project").Split(',');
                string CompareARB = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("CompareARP", "Project");
                int post = Array.IndexOf(OnProject, ddlDeptName.SelectedValue.ToString());
                if (post > -1 && CheckConfig("ProjectNewAktif") == 1 && CompareARB == "1")
                {
                    if (ddlProjectName.SelectedIndex == 0 && txtCariNamaBrg.Text != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Project harus di pilih terlebih dahulu");
                        return;
                    }
                    if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                    {
                        LoadItems();
                        txtCariNamaBrg.Text = string.Empty;
                    }
                }
                else
                {
                    if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                    {
                        //base on grouppurchn: GroupID pada Inventory = 1 & GroupPurchn 
                        // 6 = 7 = 8 = 9 = MRS
                        //LoadItems(6);
                        LoadItems(9);
                        txtCariNamaBrg.Text = string.Empty;
                    }
                }
            }
            DisableButtonCancel();
            //btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        protected void LogActivity(string activity, bool recordPageUrl)
        {
            if (Request.IsAuthenticated)
            {
                // Get information about the currently logged on user

                Users user = (Users)Session["Users"];
                if (user != null)
                {
                    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                    try
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@UserID", user.ID));
                        param.Add(new SqlParameter("@Activity", activity));
                        param.Add(new SqlParameter("@PageUrl", Request.RawUrl));
                        param.Add(new SqlParameter("@IPAddress", Global.GetIPAddress()));
                        param.Add(new SqlParameter("@Browser", Request.UserAgent));
                        int intResult = da.ProcessData(param, "sp_LogUserActivity");
                    }
                    catch
                    {
                    }
                    finally
                    {
                        da.CloseConnection();
                    }
                }
            }
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPakaiDetail"] = new ArrayList();
            Session["Pakai"] = null;
            Session["PakaiNo"] = null;
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
            txtUomID.SelectedIndex = 0;
            txtPakaiTipe.Text = string.Empty;
            txtGroupID.Text = string.Empty;
            ddlDeptName.Enabled = true;
            spGroup.SelectedIndex = 0;
            //ddlDeptName.Items.Clear();
            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            //else
            //    ddlDeptName.Items.Clear();
            ddlItemName.Items.Clear();
            ddlNoPolisi.SelectedValue = "0";
            ArrayList arrList = new ArrayList();
            arrList.Add(new PakaiDetail());
            ddlProjectName.Items.Clear();
            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = true;
            ddlDeptName.Enabled = true;
            ddlForklif.Items.Clear();
            ddlForklif.Visible = false;
            frk.InnerHtml = "";
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            PanelPalet.Visible = false;
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
                if (CheckConfig("SparePartByDept") == 0 || ((Users)Session["Users"]).DeptID == 10)
                {
                    ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
                else
                {
                    if (dept.ID == ((Users)Session["Users"]).DeptID && ((Users)Session["Users"]).DeptID != 10)
                    {
                        ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                    }
                }
            }
        }


        private void DisableButtonCancel()
        {
            PakaiFacade pki = new PakaiFacade();
            int status = 5;
            string pakaino = txtPakaiNo.Text;
            if (pakaino != "")
            {
                status = pki.Cekstatus(txtPakaiNo.Text);
            }

            int intApv = ((Users)Session["Users"]).DeptID;

            if (intApv == 10 || status == 0)
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

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Zona"] = null;
            txtCariNamaBrg.ReadOnly = false;
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
                    spGroup.Enabled = true;
                    spGroup.SelectedValue = "0";
                }
                else
                {
                    spGroup.SelectedValue = "0";
                    spGroup.Enabled = false;
                }

                #region Proses Armada
                if (ddlDeptName.SelectedValue == "26" && CheckConfig("SPBArmada") == 1 && CheckConfig("InfoUpdateSPB") == 1)
                {
                    ShowInfo("SPBArmada2", "UpdateSPBArmada.txt");
                }
                x1.Visible = (CheckConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == "26") ? true : false;
                x2.Visible = (CheckConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == "26") ? true : false;
                LoadNoPol(int.Parse(ddlDeptName.SelectedValue));
                #endregion
                #region Proses Engineering Project New
                string onlyDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project");
                string[] arrDept = onlyDept.Split(new string[] { "," }, StringSplitOptions.None);
                if ((arrDept.Contains(ddlDeptName.SelectedValue.ToString()) ||
                     arrDept.Contains("All")) && CheckConfig("ProjectNewAktif") == 1)
                {
                    txtCariNamaBrg.ReadOnly = true;
                    listProject.Visible = true;
                    prj.Visible = true;
                    ddlProjectName.Visible = true;
                    LoadProject();
                    if (CheckConfig("InfoUpdateSPB") == 1)
                    {
                        ShowInfo("SPBProject", "UpdateSPBProject.txt");
                    }
                }
                else
                {
                    listProject.Visible = false;
                    prj.Visible = false;
                    ddlProjectName.Visible = false;
                    ddlProjectName.Items.Clear();
                    ddlProjectName.Items.Add(new ListItem("", "0"));
                    ddlProjectName.SelectedValue = "0";
                }
                #endregion
            }
            #region penambahan Zona
            /** Added on 12-01-2016 base on WO**/
            string ZonaAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ZonaAktif", "SPBMaintenance");
            string[] arrZona = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ZonaName", "SPBMaintenance").Split(',');
            ddlZona.Items.Clear();
            ddlZona.Items.Add(new ListItem("--Pilih Zona--", "0"));
            for (int i = 0; i < arrZona.Count(); i++)
            {
                ddlZona.Items.Add(new ListItem(arrZona[i].ToString(), arrZona[i].ToString()));
            }
            switch (ddlDeptName.SelectedValue)
            {
                case "4":
                case "5":
                case "18":
                case "19":
                    spZona.Visible = (ZonaAktif == "1") ? true : false;
                    Session["Zona"] = ZonaAktif;
                    break;
                default:
                    spZona.Visible = false;
                    Session["Zona"] = ZonaAktif;
                    break;
            }
            #endregion
        }
        protected void ddlProjectName_Change(object sender, EventArgs e)
        {
            LoadItems();
        }
        private void LoadItems()
        {
            ddlItemName.Items.Clear();
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade m = new MTC_ProjectFacade();
            arrData = m.RetrieveEstimasiMaterialList(ddlProjectName.SelectedValue.ToString());
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            if (arrData.Count > 0)
            {

                foreach (EstimasiMaterial em in arrData)
                {
                    ddlItemName.Items.Add(new ListItem(em.ItemName + "( " + em.ItemCode + " )->" + em.Jumlah, em.ItemID.ToString()));
                }
            }
            else
            {
                //ddlItemName.Items.Add(new ListItem("Estimasi material belum ada untuk project tsb", "0"));
                //ddlItemName.SelectedIndex = 1;
            }
        }
        private void LoadItems(int intGroupID)
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade InventoryFacade = new InventoryFacade();
            Session["itemname"] = txtCariNamaBrg.Text.Trim();
            string Stocked = (CheckConfig("ViewOnlyStockInSPB") == 1 && stk.Checked == true) ? " jumlah >0 and " : "";
            arrInventory = InventoryFacade.RetrieveByCriteriaWithGroupID2(Stocked + "A.ItemName", txtCariNamaBrg.Text, intGroupID);
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                // bisa cancel hanya bisa level head dept ke atas
                int intApv = ((Users)Session["Users"]).Apv;
                string strDocumentNo = string.Empty;
                int intPakaiDetailID = 0;
                string strEvent = string.Empty;

                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrTransferDetail = new ArrayList();
                arrTransferDetail = (ArrayList)Session["ListOfPakaiDetail"];
                if (txtPakaiNo.Text != string.Empty)
                {
                    if (intApv > 0)
                    {

                        if (btnCancel.Enabled == false)
                            return;

                        #region check status closing
                        AccClosingFacade cls = new AccClosingFacade();
                        AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
                        if (clsBln.Status == 1)
                        {
                            string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                            DisplayAJAXMessage(this, mess);
                            return;
                        }
                        #endregion
                        #region old process di nonaktifkan
                        //Pakai pakai = new Pakai();
                        //if (Session["id"] != null)
                        //{

                        //    int id = (int)Session["id"];

                        //    PakaiFacade pakaiFacade = new PakaiFacade();

                        //    // musti pake PakaiTipe agar bisa dibedakan No pemakaian-nya
                        //    // musti dipikirin utk gak bisa hapus jika accounting dah closing
                        //    // ini dia jika accounting sudah closing
                        //    //cek periode closing

                        //    // 1 = spare-part= KS

                        //    Company company = new Company();
                        //    CompanyFacade companyFacade = new CompanyFacade();
                        //    string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
                        //    //string strPakaiTipe = "KS";
                        //    Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                        //    if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                        //    {
                        //        {
                        //            int i = 0;
                        //            int x = 0;
                        //            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                        //            ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                        //            if (pakaiDetailFacade.Error == string.Empty)
                        //            {
                        //                PakaiDetail receiptDetail1 = (PakaiDetail)arrTransferDetail[index];
                        //                bool valid = false;
                        //                int pkiDetailID = 0;

                        //                foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                        //                {
                        //                    if (pkiDetail.ItemTypeID == 1) // Inventory
                        //                    {
                        //                        // cek stok
                        //                        InventoryFacade InventoryFacade = new InventoryFacade();
                        //                        Inventory Inventory = new Inventory();

                        //                        Inventory.ID = pkiDetail.ItemID;
                        //                        Inventory.Jumlah = pkiDetail.Quantity;


                        //                        pkiDetailID = pkiDetail.ID;
                        //                        i = x;
                        //                        valid = true;
                        //                        break;
                        //                    }

                        //                    x = x + 1;
                        //                }
                        #endregion
                        #region proses delete per item
                        PakaiDetail pakaiDetail = (PakaiDetail)arrTransferDetail[index];
                        ArrayList arrPakaiDetail = new ArrayList();
                        foreach (PakaiDetail pd in arrTransferDetail)
                        {
                            if (pd.ID == pakaiDetail.ID)
                            {
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
                        else
                        {

                            strEvent = "Hapus per Baris";
                            InsertLog(strEvent);

                            LoadPakai(txtPakaiNo.Text);
                        }


                        #endregion

                    }
                }
                else
                {


                    arrTransferDetail.RemoveAt(index);
                    Session["ListOfPakaiDetail"] = arrTransferDetail;
                    GridView1.Columns[5].Visible = true;
                    GridView1.DataSource = arrTransferDetail;
                    GridView1.DataBind();

                }

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            int Status = (Session["PakaiStatus"] != null) ? (int)Session["PakaiStatus"] : 0;
            if (Status <= 1 && ((Users)Session["Users"]).Apv > 0)
            {
                GridView1.Columns[5].Visible = true;
            }
            else
            {
                GridView1.Columns[5].Visible = false;

            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KS");
        }

        protected void btnUpdateClose_ServerClick(object sender, EventArgs e)
        {
            
        }

        protected void btnUpdateAlasan_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel.Text;
            Session["AlasanBatal"] = txtAlasanCancel.Text;
            Session["AlasanTolak"] = txtAlasanCancel.Text;

            // bisa cancel hanya level head dept ke atas
            int intApv = ((Users)Session["Users"]).Apv;
            #region CheckClosingPeriode
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            #endregion
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
                string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

                //string strPakaiTipe = "KS";
                Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                {
                    if (pki.Status >= 3)
                    {
                        DisplayAJAXMessage(this, "Pemakaian tidak boleh di Cancel (harus di retur)");
                        return;
                    }
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

                        ArrayList arrSmt = new ArrayList();
                        MTC_SarmutFacade smt = new MTC_SarmutFacade();
                        arrSmt = smt.RetrieveTransID(pki.PakaiNo, 0);
                        string strError = pakaiProcessFacade.CancelPakaiSarmut(arrSmt);

                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, strError);
                            return;
                        }

                        else
                        {
                            strEvent = "Cancel All SPB SparePart";
                            InsertLog(strEvent);
                            //int rst=CancelSarmut(pki.PakaiNo, 0);
                            //if (rst > 0)
                            //{
                            DisplayAJAXMessage(this, "Cancel berhasil .....");
                            clearForm();
                            //}
                            //else
                            //{
                            //    DisplayAJAXMessage(this, "Sarmut Cancel Error....");
                            //}
                        }

                    }
                }
            }
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            panEdit.Visible = true;
            mpePopUp.Show();
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            PakaiDetail pakaiDetail = new PakaiDetail();
            ArrayList arrListPakaiDetail = new ArrayList();
            MTC_Sarmut sarmut = new MTC_Sarmut();
            ArrayList arrListSarmut = new ArrayList();
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project").Split(new string[] { "," }, StringSplitOptions.None);

            #region validasi input data
            #region check quantity harus number
            try
            {
                decimal cekNumber = decimal.Parse(txtQtyPakai.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity SPB harus number");
                return;
            }
            #endregion
            #region check quantity harus lebih dari nol
            if (decimal.Parse(txtQtyPakai.Text) <= 0)
            {
                DisplayAJAXMessage(this, "Quantity SPB harus lebih dari 0");
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
            #endregion
            #region Check Stock Barang
            if (decimal.Parse(txtStok.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                return;
            }
            #endregion
            #region Check Quantity spb tidak boleh melibihi stock
            if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text))
            {
                DisplayAJAXMessage(this, "Quantity Pakai lebih besar dari pada Stock");
                return;
            }
            #endregion
            #region check departemen yang dipilih untuk menentukan sarmut
            //cek sp group status
            if (spGroup.SelectedIndex == 0 &&
                (int.Parse(ddlDeptName.SelectedValue) == 4 ||
                 int.Parse(ddlDeptName.SelectedValue) == 5 ||
                 int.Parse(ddlDeptName.SelectedValue) == 18)
                )
            {
                DisplayAJAXMessage(this, "Spare Part Group Harus di pilih");
                return;
            }
            #endregion
            #region Check status no mobil untuk armada dan utility dept
            if (CheckConfig("SPBArmada") == 1)
            {
                if (((Users)Session["Users"]).DeptID == 26 || ddlDeptName.SelectedValue == "26" || spGroup.SelectedValue == "13")
                {
                    if (ddlNoPolisi.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, "Nomor Kendaraan harus di pilih!");
                        return;
                    }
                }
            }
            #endregion
            #region Check Project name untuk dept tertentu sesuai dengan variabl arrDept
            if (ddlProjectName.SelectedIndex == 0 && arrDept.Contains(ddlDeptName.SelectedValue.ToString()) &&
                CheckConfig("ProjectNewAktif") == 1)
            {
                DisplayAJAXMessage(this, "Nama project belum ditentukan");
                return;
            }
            #endregion
            #region Validasi pilihan Zona untuk spb maintenance
            if (Session["Zona"] != null)
            {
                if (Session["Zona"].ToString() == "1")
                {
                    if (ddlZona.SelectedIndex == 0)
                    {
                        switch (ddlDeptName.SelectedValue)
                        {
                            case "4":
                            case "5":
                            case "18":
                            case "19":
                                DisplayAJAXMessage(this, "Zona belum ditentukan");
                                return;
                        }
                    }
                }
            }
            #endregion
            if (txtKeterangan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Keterangan harus diisi...");
                return;
            }
            #region Validasi Keterangan dan forklift
            if (spGroup.SelectedItem.Text.Trim() != string.Empty)
            {
                if (spGroup.SelectedItem.Text.Trim().ToUpper().Substring(0, 4) == "FORK")
                {
                    if (ddlForklif.SelectedIndex < 1)
                    {
                        DisplayAJAXMessage(this, "Forklift harus diisi...");
                        return;
                    }
                }
            }
            #endregion
            #endregion
            #region Check Stok private (Multigudang)
            SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
            decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
            #endregion
            #region depreciated
            //if (decimal.Parse(txtQtyPakai.Text) > (decimal.Parse(txtStok.Text) - StockOtherDept))
            //{
            //    DisplayAJAXMessage(this, "Sebagian Quantity Stock sudah dipesan oleh departemen lain, coba kurangi Qty pakainya");
            //    return;
            //} sudah di split di ddlNamaBarang_Change();
            ddlDeptName.Enabled = false;
            //end filter multigudang
            #endregion
            #region cek closing dan tanggal akhir transaksi
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);

            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + nBulan(clsBln.Bulan) + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl && CheckConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");

                    //Level Approval pd table Users = level head dept or more up
                    //ada warning tapi bisa lewat so bisa entry tgl mundur
                    //int intApv = ((Users)Session["Users"]).Apv;
                    //if (intApv == 0) 
                    return;
                }
            }
            #endregion
            #region validasi dobel itemname
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
            #region CheckMaximalQty SPB - budget consummable
            PlanningFacade pd = new PlanningFacade();
            decimal checkCost = pd.MaterialBudgetBM(ddlItemName.SelectedValue, PlanningProd());
            decimal CheckKhusus = pd.MaterialBudgetKhusus(ddlItemName.SelectedValue, ddlDeptName.SelectedValue);
            if (ddlDeptName.SelectedValue == "2" && checkCost > 0)
            {
                if (pd.isMaterialBudgetKhusus(ddlItemName.SelectedValue, ddlDeptName.SelectedValue) == true)
                {
                    if (MasihBolehSPBKhusus(decimal.Parse(txtQtyPakai.Text), CheckKhusus) == false)
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
            }
            else
            {
                if (ddlDeptName.SelectedValue == "19")
                {
                    if (Convert.ToDecimal(lblSisa.Text) - Convert.ToDecimal(txtQtyPakai.Text) < 0)
                    {
                        //decimal sisa = (SisaSPB() <= 0) ? 0 : SisaSPB();
                        //decimal sisa = (Convert.ToDecimal(lblSisa.Text) <= 0) ? 0 : Convert.ToDecimal(lblSisa.Text);
                        DisplayAJAXMessage(this, "Sisa SPB untuk RAB ini tinggal : " + (Convert.ToDecimal(lblSisa.Text)).ToString("###,###.#0") + " lagi.\\n" +
                                                 "Max SPB : " + (Convert.ToDecimal(lblSPB.Text)).ToString("N2") + "\\nSilahkan review / update RAB");
                        return;
                    }
                }
                else
                {
                    if (MasihBolehSPB(decimal.Parse(txtQtyPakai.Text)) == false && isMaterialBudget(int.Parse(ddlItemName.SelectedValue)) == true)
                    {
                        decimal sisa = (SisaSPB() <= 0) ? 0 : SisaSPB();
                        //decimal sisa = (Convert.ToDecimal(lblSisa.Text) <= 0) ? 0 : Convert.ToDecimal(lblSisa.Text);
                        DisplayAJAXMessage(this, "Sisa SPB untuk bulan ini tinggal : " + sisa.ToString("###,###.#0") + " lagi.\\n" +
                                                 "Max SPB : " + MaxSPB().ToString("N2") + "\\nSilahkan Hubungi Logistik Material");
                        return;
                    }
                }
            }
            #endregion
            #region informasi reorder point
            // cek ke re-order point
            InventoryFacade InventoryFacade = new InventoryFacade();
            Inventory inv = InventoryFacade.RetrieveById2(int.Parse(ddlItemName.SelectedValue));
            if (InventoryFacade.Error == string.Empty && inv.ID > 0)
            {
                if (inv.Jumlah - decimal.Parse(txtQtyPakai.Text) <= inv.ReOrder)
                {
                    string aMess = "Jumlah Pemakaian Barang di stock sudah melampaui Reorder Point ( " + inv.ReOrder.ToString() + " )";
                    DisplayAJAXMessage(this, aMess);
                }
            }
            //
            #endregion
            decimal stoke = 0;
            decimal.TryParse(txtStok.Text, out stoke);
            #region Proses Pengumpulan data input
            pakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            pakaiDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            pakaiDetail.RowStatus = 0;
            if (PanelPalet.Visible == false)
                pakaiDetail.Keterangan = txtKeterangan.Text;
            else
            {
                if (RbPPacking.Checked == true)
                    pakaiDetail.Keterangan = "Palet Packing";
                else
                    pakaiDetail.Keterangan = "Palet Stock";
            }

            pakaiDetail.UomID = int.Parse(txtUomID.Text);
            pakaiDetail.ItemCode = txtItemCode.Text;
            pakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            pakaiDetail.UOMCode = txtUom.Text;
            pakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            pakaiDetail.SarmutID = (spGroup.SelectedValue == string.Empty) ? 0 : int.Parse(spGroup.SelectedValue.ToString());
            pakaiDetail.DeptCode = txtKodeDept.Text;
            pakaiDetail.KartuStock = stoke;
            pakaiDetail.ItemTypeID = 1;
            pakaiDetail.BudgetQty = (ddlDeptName.SelectedValue == "2") ? MaxSPB(true) : MaxSPB();
            bdgQty.Text = (ddlDeptName.SelectedValue == "2") ? MaxSPB(true).ToString() : MaxSPB().ToString();
            pakaiDetail.NoPol = ddlNoPolisi.SelectedItem.ToString();
            #region for perawartan kendaraan
            /**
             * Added on 02-06-2014
             * Pemakain sparepart untuk kendaraan
             */
            string npol = (ddlNoPolisi.SelectedValue.ToString() == "0") ? string.Empty : ddlNoPolisi.SelectedItem.ToString();
            pakaiDetail.IDKendaraan = (ddlNoPolisi.SelectedValue == "0" || ddlNoPolisi.SelectedValue == string.Empty) ? 0 : int.Parse(ddlNoPolisi.SelectedValue.ToString());
            if (int.Parse(ddlNoPolisi.SelectedValue) >= 1000)
            {
                pakaiDetail.NoPol = ddlForklif.SelectedValue.ToString();
            }
            else
            {
                pakaiDetail.NoPol = (ddlNoPolisi.SelectedValue == "0") ? string.Empty : npol.Substring(0, npol.IndexOf(" -"));
            }
            if (int.Parse(spGroup.SelectedValue) == 14)
            {
                pakaiDetail.NoPol = ddlForklif.SelectedValue.ToString();
                pakaiDetail.Keterangan += " [ " + ddlForklif.SelectedItem.Text + " ]";
            }
            #endregion
            #region for New project
            pakaiDetail.ProjectID = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDept.Contains(ddlDeptName.SelectedValue.ToString()) || arrDept.Contains("All"))) ? int.Parse(ddlProjectName.SelectedValue) : 0;
            pakaiDetail.ProjectName = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDept.Contains(ddlDeptName.SelectedValue.ToString()) || arrDept.Contains("All"))) ? ddlProjectName.SelectedItem.ToString() : string.Empty;

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
            Session["ListOfPakaiDetail"] = arrListPakaiDetail;
            #endregion
            #region Proses Sarmut
            /**
             * Sarmut Mtc Data
             * added on 13-03-2014
             * yang masuk sarmut hanya dept id : 4,5,18
             */

            if (int.Parse(ddlDeptName.SelectedValue) == 4 ||
                int.Parse(ddlDeptName.SelectedValue) == 5 ||
                int.Parse(ddlDeptName.SelectedValue) == 18)
            {
                sarmut.ID = (spGroup.SelectedValue == string.Empty) ? 0 : int.Parse(spGroup.SelectedValue.ToString());
                sarmut.ItemID = int.Parse(ddlItemName.SelectedValue);
                sarmut.DeptCode = txtKodeDept.Text;
                sarmut.DeptID = int.Parse(ddlDeptName.SelectedValue.ToString());
                sarmut.Qty = decimal.Parse(txtQtyPakai.Text);
                sarmut.AvgPrice = 0;
                sarmut.SPBDate = Convert.ToDateTime(txtTanggal.Text);
                sarmut.SarmutID = int.Parse(spGroup.SelectedValue.ToString());
                arrListSarmut.Add(sarmut);


                Session["ListSarmut"] = arrListSarmut;
            }
            #endregion
            #region proses pakai sp for project
            /**
             * pemakaian sparepart untuk project maintenance
             * rule : jika modul project sudah aktif dan dept telpilih adalah maintenance (19)
             */
            #region Proses Data Project
            if (ProjectNewAktif("ProjectNewAktif") == 1 && arrDept.Contains(ddlDeptName.SelectedValue.ToString()))
            {
                MTC_ProjectPakai objProject = new MTC_ProjectPakai();
                ArrayList arrProject = new ArrayList();
                objProject.ProjectID = int.Parse(ddlProjectName.SelectedValue);
                objProject.PakaiID = 0;
                objProject.ItemID = int.Parse(ddlItemName.SelectedValue);
                objProject.GroupID = int.Parse(txtGroupID.Text);
                objProject.ItemTypeID = 1;
                objProject.Qty = Convert.ToDecimal(txtQtyPakai.Text);
                objProject.AvgPrice = decimal.Parse(txtAvgPrice.Text);
                arrProject.Add(objProject);
                Session["ListPakaiProject"] = arrProject;
            }
            #endregion
            #endregion
            GridView1.Columns[5].Visible = true;
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
            txtUomID.SelectedIndex = 0;
            txtGroupID.Text = string.Empty;
            ddlItemName.SelectedIndex = 0;
            spGroup.SelectedIndex = 0;
            ddlNoPolisi.SelectedValue = "0";
            ddlZona.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("ListOfPakaiDetail");
            Session.Remove("Pakai");
            Session.Remove("PakaiNo");
            Session.Remove("ListSarmut");
            Session.Remove("ListKendaraan");
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int bln = DateTime.Parse(txtTanggal.Text).Month;
            int thn = DateTime.Parse(txtTanggal.Text).Year;
            int noBaru = 0;
            #region validasi inputan
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            #endregion
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            //string strPakaiCode = "KS" + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);
            #region check closing periode atau tanggl last entry
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);
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
                if (nowTgl < lastTgl && CheckConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    return;
                }

            }
            #endregion
            string strEvent = "Insert";
            pakai = new Pakai();

            if (Session["id"] != null)
            {
                #region baris dibawah dinonaktifkan
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
                #endregion
            }
            #region Proses Header Pakai Sparepart
            //here for document number
            company = new Company();
            companyFacade = new CompanyFacade();
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";

            PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade();
            PakaiDocNo pakaiDocNo = new PakaiDocNo();
            if (strEvent == "Insert")
            {
                //"K" for Karawang
                pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, kd);
                //pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, "KS");
                if (pakaiDocNo.ID == 0)
                {
                    noBaru = 1;
                    pakaiDocNo.PakaiCode = kd;
                    //pakaiDocNo.PakaiCode = "KS";
                    pakaiDocNo.NoUrut = 1;
                    pakaiDocNo.MonthPeriod = bln;
                    pakaiDocNo.YearPeriod = thn;
                }
                else
                {
                    noBaru = pakaiDocNo.NoUrut + 1;
                    pakaiDocNo.PakaiCode = kd;
                    //pakaiDocNo.PakaiCode = "KS";
                    pakaiDocNo.NoUrut = pakaiDocNo.NoUrut + 1;
                }
            }
            pakai.PakaiNo = txtPakaiNo.Text;
            pakai.PakaiDate = DateTime.Parse(txtTanggal.Text);
            //8 for electrik, 9 for mekanik 
            pakai.PakaiTipe = int.Parse(txtPakaiTipe.Text);
            pakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            pakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            pakai.Status = 0;
            pakai.AlasanCancel = "";
            pakai.CreatedBy = ((Users)Session["Users"]).UserName;
            pakai.ItemTypeID = 1;
            #endregion
            #region Proses Detail Pakai Sparepart
            string strError = string.Empty;
            decimal stok = 0;
            //ngambil data stock base on kartu stock
            decimal.TryParse(txtStok.Text, out stok);
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                foreach (PakaiDetail pkiDetail in arrPakaiDetail)
                {
                    InventoryFacade invFacade = new InventoryFacade();
                    Inventory inv = invFacade.RetrieveById(pkiDetail.ItemID);
                    if (invFacade.Error == string.Empty && inv.ID > 0)
                    {
                        if ((pkiDetail.KartuStock - pkiDetail.Quantity) < 0)
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
            #endregion
            PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pakai, arrPakaiDetail, pakaiDocNo);
            if (pakai.ID > 0)
            {
                #region di nonaktifkan
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
                    clearForm();
                    txtPakaiNo.Text = pakaiProcessFacade.PakaiNo;
                    Session["id"] = pakai.ID;
                    Session["PakaiNo"] = pakaiProcessFacade.PakaiNo;

                }
                else
                    DisplayAJAXMessage(this, strError);
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                //txtCariOP.ReadOnly = false;

                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                btnPrint.Disabled = true;
                //if (strEvent == "Edit")

            }
            LoadPakai(txtPakaiNo.Text);
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Pemakaian Spare-Part";
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
                //cek budget tambahan aktif
                string addbudget = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AddBudgetAktif", "BudgetConsumable");
                SaldoInventoryFacade price = new SaldoInventoryFacade();
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));
                string s1 = ddlItemName.SelectedItem.Text.Trim();
                bool b1 = s1.Contains("PAPAN");
                bool b2 = s1.Contains("BALOK");
                if ((ddlDeptName.SelectedValue == "6" || ddlDeptName.SelectedValue == "10") && (b1 == true || b2 == true))
                    PanelPalet.Visible = true;
                else
                    PanelPalet.Visible = false;
                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    txtGroupID.Text = inv.GroupID.ToString();
                    txtPakaiTipe.Text = inv.GroupID.ToString();
                    txtQtyPakai.Focus();
                    int AvgPrice = price.GetPrice(inv.ID, getMonthName(int.Parse(DateTime.Parse(txtTanggal.Text).Month.ToString())), DateTime.Parse(txtTanggal.Text).Year, inv.ItemTypeID);
                    txtAvgPrice.Text = AvgPrice.ToString();
                    /**
                    * cek private stok
                    * jika private stok bukan punya dept id
                    * tampilkan hanya public saja
                    */
                    SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
                    //decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
                    decimal StockOtherDept = sppmf.RetrieveByStock(((Users)Session["Users"]).DeptID, int.Parse(ddlItemName.SelectedValue),
                        int.Parse(txtGroupID.Text), 1);
                    decimal ShowStock = ((Convert.ToDecimal(inv.Jumlah) - StockOtherDept) > 0) ? (Convert.ToDecimal(inv.Jumlah) - StockOtherDept) : 0;
                    //stock diambil dari kartu stock
                    decimal PendingSPB = invFacade.GetPendingSPB(ddlItemName.SelectedValue, 1);
                    decimal StockAkhir = invFacade.GetStockAkhir(ddlItemName.SelectedValue, 1, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());
                    //txtStok.Text = ShowStock.ToString("N2");//
                    txtStok.Text = ((StockAkhir - StockOtherDept - PendingSPB) > 0) ? (StockAkhir - StockOtherDept - PendingSPB).ToString("N2") : "0";
                    txtPending.Text = (PendingSPB + StockOtherDept).ToString("N2");
                    txtPending.ToolTip = "Pending SPB :" + PendingSPB.ToString("N2") + "\n";
                    txtPending.ToolTip += "Private Stock :" + StockOtherDept.ToString("N2");
                    /**
                     * Tampilkan informasi budget material
                     * Jika material tersebut masuk di list budget (head=5 di table inventory)
                     * untuk bm check budget di cost control nya
                     */
                    PlanningFacade pd = new PlanningFacade();
                    decimal checkCost = pd.MaterialBudgetBM(ddlItemName.SelectedValue, PlanningProd());
                    addbdg.Visible = (addbudget == "1") ? true : false;
                    if (inv.Head == 5 || checkCost > 0)
                    {
                        switch (ddlDeptName.SelectedValue)
                        {
                            case "2":
                                showBudget.Visible = true;
                                lblPeriode.Text = DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
                                lblBudget.Text = (MaxSPB(true) > 0) ? (MaxSPB(true)).ToString("N2") : AddBudget().ToString("N2");//total budget
                                lblSPB.Text = TotalSPB().ToString("N2");//total spb
                                lblSisa.Text = SisaSPB(true).ToString("N2");//sisa budget
                                lbltBudget.Text = "RunLine : " + PlanningProd().ToString();// TipeBudget().ToString();//tipe tambahan
                                lbladdBudget.Text = (addbudget == "1") ? AddBudget().ToString("N2") : "";//budget tambahan
                                break;
                            case "19":
                                showBudget.Visible = true;
                                lblPeriode.Text = DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
                                lblBudget.Text = MaxSPBPrj(ddlProjectName.SelectedValue).ToString();//total budget
                                lblSPB.Text = TotalSPBPrj(ddlProjectName.SelectedValue).ToString();//total spb
                                lblSisa.Text = (Convert.ToDecimal(lblBudget.Text) - Convert.ToDecimal(lblSPB.Text)).ToString("N2");//sisa budget
                                lbltBudget.Text = "RAB";// TipeBudget().ToString();//tipe tambahan
                                lbladdBudget.Text = "";//budget tambahan
                                break;
                            default:
                                showBudget.Visible = true;
                                lblPeriode.Text = DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
                                lblBudget.Text = (MaxSPB() > 0) ? (MaxSPB() - AddBudget()).ToString("N2") : AddBudget().ToString("N2");//total budget
                                lblSPB.Text = TotalSPB().ToString("N2");//total spb
                                lblSisa.Text = SisaSPB().ToString("N2");//sisa budget
                                lbltBudget.Text = TipeBudget().ToString();//tipe tambahan
                                lbladdBudget.Text = (addbudget == "1") ? AddBudget().ToString("N2") : "";//budget tambahan
                                break;
                        }

                    }
                    else
                    {
                        if (ddlDeptName.SelectedValue == "19")
                        {
                            showBudget.Visible = true;
                            lblPeriode.Text = DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
                            lblBudget.Text = MaxSPBPrj(ddlProjectName.SelectedValue).ToString();//total budget
                            lblSPB.Text = TotalSPBPrj(ddlProjectName.SelectedValue).ToString();//total spb
                            lblSisa.Text = (Convert.ToDecimal(lblBudget.Text) - Convert.ToDecimal(lblSPB.Text)).ToString("N2");//sisa budget
                            lbltBudget.Text = "RAB";// TipeBudget().ToString();//tipe tambahan
                            lbladdBudget.Text = "";//budget tambahan
                        }
                        else
                        {
                            showBudget.Visible = false;
                            lblPeriode.Text = "";
                            lblBudget.Text = "";
                            lblSPB.Text = "";
                            lblSisa.Text = "";
                            lblBudget.Text = "";
                            lbladdBudget.Text = "";
                        }
                    }
                }
            }
        }
        //private string CheckPalet(string item,string itemname)
        //{
        //    string ada = "";
        //    bool b=

        //    return ada;
        //}
        protected string getMonthName(int Bulan)
        {
            SaldoInventoryFacade Bln = new SaldoInventoryFacade();
            return Bln.GetStrMonth(Bulan);
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
            string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            //string strPakaiTipe = "KS";
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
                Session["PakaiStatus"] = pakai.Status;
                Session["PakaiNo"] = pakai.PakaiNo;
                Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                GridView1.DataSource = arrListPakaiDetail;
                GridView1.DataBind();

                //sehingga tdk bisa add dept yg laen
                ddlDeptName.Enabled = false;

                btnCancel.Enabled = (pakai.Status == -1 || pakai.Status > ((Users)Session["Users"]).Apv) ? false : true;

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
        private void LoadSatuan()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            txtUomID.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                txtUomID.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }

        private void LoadGroupPM()
        {
            ArrayList arrPM = new ArrayList();
            MTC_ZonaFacade PM = new MTC_ZonaFacade();
            arrPM = PM.RetrieveSpGroup();
            spGroup.Items.Add(new ListItem(" ", "0"));
            foreach (MTC_Zona uPm in arrPM)
            {
                spGroup.Items.Add(new ListItem(uPm.ZonaName, uPm.ID.ToString()));
            }
        }
        /*
         * load Group material untuk budget baru
         */

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
                    return "Tidak ada List Item yang di-input";
            }
            return string.Empty;
        }

        private void CheckReOrderPoint()
        {
            int ROP = 0;
            InventoryFacade InventoryFacade = new InventoryFacade();
            ROP = InventoryFacade.CheckReorderPoint("6", ((Users)Session["Users"]).ID);
            ROP = InventoryFacade.CheckReorderPoint("8", ((Users)Session["Users"]).ID);
            ROP = InventoryFacade.CheckReorderPoint("9", ((Users)Session["Users"]).ID);
            //if (ROP == 1)
            //{
            //    Timer2.Enabled = true;
            //    Panel2.Visible = true;
            //}
            //else
            //{
            //    Timer2.Enabled = false;
            //    Panel2.Visible = false;
            //    Timer1.Enabled = false;
            //}
        }

        private void LoadListLockSPP()
        {
            //int intROP = 0;
            int strerror = 0;
            InventoryFacade InventoryFacade = new InventoryFacade();
            ROPFacade ropfacade = new ROPFacade();
            //intROP = InventoryFacade.CheckReorderPoint("6,8,9");
            ArrayList arrInventory = new ArrayList();
            arrInventory = InventoryFacade.ListReorderPoint(6);
            if (arrInventory.Count > 0)
            {
                foreach (Inventory Inv in arrInventory)
                {
                    strerror = ropfacade.InsertROP(Inv.ItemID, ((Users)Session["Users"]).ID);
                }
            }
            arrInventory = InventoryFacade.ListReorderPoint(8);

            if (arrInventory.Count > 0)
            {
                foreach (Inventory Inv in arrInventory)
                {
                    strerror = ropfacade.InsertROP(Inv.ItemID, ((Users)Session["Users"]).ID);
                }
            }
            arrInventory = InventoryFacade.ListReorderPoint(9);

            if (arrInventory.Count > 0)
            {
                foreach (Inventory Inv in arrInventory)
                {
                    strerror = ropfacade.InsertROP(Inv.ItemID, ((Users)Session["Users"]).ID);
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CheckReOrderPoint();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //playsound();
            //Timer2.Enabled = false;
            //Panel2.Visible = false;
        }

        public static void playsound()
        {
            // Declare the first few notes of the song, "Mary Had A Little Lamb".
            Note[] Mary =
            {
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.GbelowC, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.B, Duration.HALF),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.A, Duration.HALF),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.D, Duration.QUARTER),
        new Note(Tone.D, Duration.HALF)
        };
            // Play the song
            Play(Mary);
        }

        // Play the notes in a song.
        protected static void Play(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.REST)
                    Thread.Sleep((int)n.NoteDuration);
                else
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }

        // Define the frequencies of notes in an octave, as well as 
        // silence (rest).
        protected enum Tone
        {
            REST = 0,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        // Define the duration of a note in units of milliseconds.
        protected enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        // Define a note as a frequency (tone) and the amount of 
        // time (duration) the note plays.
        protected struct Note
        {
            Tone toneVal;
            Duration durVal;

            // Define a constructor to create a specific note.
            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }

            // Define properties to return the note's tone and duration.
            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            //if (Panel2.BackColor == System.Drawing.Color.White)
            //{
            //    Panel2.BackColor = System.Drawing.Color.Red;
            //    Console.Beep();
            //}
            //else
            //{
            //    Panel2.BackColor = System.Drawing.Color.White;
            //    Console.Beep();
            //}
        }

        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            if (clsBln.Status == 1)
            {
                string mess = "Periode " + nBulan(clsBln.Bulan) + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
        }

        private string nBulan(int Bulan)
        {
            string[] arrBln = new string[] { "", "January", "February", "Maret", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return arrBln[Bulan];
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception d)
            {
                string strError = d.Message;
                DisplayAJAXMessage(this, strError);
            }
        }
        private void LoadNoPol(int DeptID)
        {
            /**
             * Menampilkan daftar kendaraan pada dropdown
             * rule : tampilkan hanya nomor kendaraan sesuai plant
             *        tampilkan alias kendaraan sesuai plant
             * Database : GRCBBoard Table Ex_masterkendaraan dan bpaskrwg/bpasctrp table MTC_NamaArmada
             * Akses Data : Web Service BPAS_API - GetNoPolByPlant(int Plant)
             */
            try
            {
                DataSet NoPol = new DataSet();
                DataSet AlKend = new DataSet();
                ArrayList arrNopol = new ArrayList();
                if ((DeptID == ProjectNewAktif("MTCinArmada") && spGroup.SelectedValue == "13") ||
                    DeptID == 26 || DeptID == 14)
                {
                    ddlNoPolisi.Items.Clear();
                    MTC_ArmadaFacade arm = new MTC_ArmadaFacade();
                    //bpas_api.WebService1 api = new bpas_api.WebService1();
                    Global2 api = new Global2();
                    string UnitKerja = string.Empty;
                    string AliasKend = string.Empty;

                    if (CheckBox1.Checked == true)
                    {
                        UnitKerja = (((Users)Session["Users"]).UnitKerjaID == 7) ? "1" : "7";
                        AliasKend = (((Users)Session["Users"]).UnitKerjaID == 7) ? "GRCBoardCtrp" : "GRCBoardKrwg";
                    }
                    else
                    {
                        //UnitKerja = ((Users)Session["Users"]).UnitKerjaID.ToString();
                        //AliasKend = (((Users)Session["Users"]).UnitKerjaID == 7) ? "GRCBoardKrwg" : "GRCBoardCtrp";
                        UnitKerja = ((Users)Session["Users"]).UnitKerjaID.ToString();
                        if (UnitKerja == "1")
                        {
                            AliasKend = "GRCBoardCtrp";
                        }
                        else if (UnitKerja == "7")
                        {
                            AliasKend = "GRCBoardKrwg";
                        }
                        else if (UnitKerja == "13")
                        {
                            AliasKend = "GRCBoardJmb";
                        }
                    }

                    NoPol = api.GetNoPolByPlant(UnitKerja);
                    foreach (DataRow nR in NoPol.Tables[0].Rows)
                    {
                        string Alias = string.Empty;
                        string Nomor = nR["KendaraanNo"].ToString();
                        if (Nomor != string.Empty)
                        {
                            try
                            {
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
                            catch { }
                        }
                    }
                    ddlNoPolisi.DataSource = arrNopol;
                    ddlNoPolisi.DataValueField = "ID";
                    ddlNoPolisi.DataTextField = "NoPol";
                    ddlNoPolisi.DataBind();
                    //ddlNoPolisi.Items.Add(new ListItem("Froklift - ", "1001"));
                    ddlNoPolisi.Items.Add(new ListItem("Umum - ", "1000"));
                    ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                    ddlNoPolisi.SelectedValue = "0";
                }
                else
                {
                    ddlNoPolisi.Items.Clear();
                    ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                    ddlNoPolisi.SelectedValue = "0";
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data kendaraan tidak dapat di load, ada masalah koneksi ke HO, hubungi IT Dept");
            }
        }

        private int CheckConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn obj = purchn.PurchnTools(ModulName);
            return obj.Status;
        }

        protected void spGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            /**
             * Sarmut armada dengan muncul no kendaraan hanya untuk dept maintenance utility (18)
             */

            if (CheckConfig("SPBArmada") == 1 &&
                CheckConfig("InfoUpdateSPB") == 1 &&
                spGroup.SelectedValue == "13" &&
                ddlDeptName.SelectedValue == CheckConfig("MTCinArmada").ToString())
            {
                ShowInfo("SPBArmada", "UpdateSPBArmada.txt");
            }
            x1.Visible = (CheckConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == CheckConfig("MTCinArmada").ToString() && spGroup.SelectedValue == "13") ? true : false;
            x2.Visible = (CheckConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == CheckConfig("MTCinArmada").ToString() && spGroup.SelectedValue == "13") ? true : false;
            LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
            if (spGroup.SelectedValue == "14")
            {
                LoadData("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                ddlForklif.Visible = true; frk.Visible = true;
                frk.InnerHtml = "Forklift Name";
                spZona.Visible = (Session["Zona"].ToString() == "0") ? true : spZona.Visible;
                ddlZona.Visible = (Session["Zona"].ToString() == "0") ? false : ddlZona.Visible;
            }
            else
            {
                x1.Visible = false; ddlForklif.Visible = false; frk.Visible = false;
                spZona.Visible = (Session["Zona"].ToString() == "0") ? false : spZona.Visible;
                ddlZona.Visible = (Session["Zona"].ToString() == "0") ? true : ddlZona.Visible;
            }
        }
        protected void ddlNoPolisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet NoPol = new DataSet();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                Global2 api = new Global2();
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
        private void LoadData(string Section, string Key)
        {
            //string[] arrData = new Inifiles(Server.MapPath("~/App_Data/GroupArmadaOnly.ini")).Read(Key, Section).Split(',');
            //if (arrData.Count() > 0)
            //{
            //    ddlForklif.Items.Clear();
            //    ddlForklif.Items.Add(new ListItem("--pilih--", "0"));
            //    for (int i = 0; i < arrData.Count(); i++)
            //    {
            //        ddlForklif.Items.Add(new ListItem(arrData[i].ToString(),arrData[i].ToString()));
            //    }
            //}
            //else
            //{
            //    ddlForklif.Items.Clear();
            //}

            if (Section == "Forklift")
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                //zl.CustomQuery = "Select * from masterforklift where RowStatus=0";
                zl.CustomQuery = " Select * from masterforklift where RowStatus>-1 order by rowstatus desc ";
                SqlDataReader sdr = zl.Retrieve();
                ddlForklif.Items.Clear();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ddlForklif.Items.Add(new ListItem(sdr["forklift"].ToString(), sdr["ID"].ToString()));
                    }
                }
            }
            else if (Section == "Umum")
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "Select * from MasterNamaKendaraan where RowStatus=0";
                SqlDataReader sdr = zl.Retrieve();
                ddlForklif.Items.Clear();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ddlForklif.Items.Add(new ListItem(sdr["NamaKendaraan"].ToString(), sdr["ID"].ToString()));
                    }
                }
            }
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
            arrProject = project.RetrieveByDept(0, true);
            ddlProjectName.Items.Add(new ListItem("--Pilih Project--", "0"));
            foreach (MTC_Project proj in arrProject)
            {

                string ProjectName = proj.SubProjectName;
                //if (ProjectName == string.Empty)
                //{
                ListItem lst = new ListItem(proj.Nomor + " => " + proj.NamaProject, proj.ID.ToString());
                lst.Attributes.Add("title", proj.NamaProject);
                ddlProjectName.Items.Add(lst);
                //}
                //else
                //{
                //    string img = HttpContext.Current.Request.ApplicationPath.ToString() + "/images/dots.gif";
                //    ListItem lst = new ListItem("   "+proj.SubProjectName, proj.ID.ToString());
                //    lst.Attributes.Add("style", "color:red");
                //    ddlProjectName.Items.Add(lst);
                //}

            }
        }

        protected int ProjectNewAktif(string Keterangan)
        {
            POPurchnFacade poTools = new POPurchnFacade();
            POPurchn objTools = poTools.PurchnTools(Keterangan);
            return objTools.Status;
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
             * Menampilkan data kendaraan plant lain
             * rule : jika checkbok di centang maka munculkan data kendaraan plant lain
             */
            LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
        }

        private string GetDeptProject(string key)
        {
            var conf = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini"));
            return conf.Read(key, "Project");
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

            if (Rule == 12)
            {
                result = (MaxSPB() > 0) ? Math.Round(((MaxSPB() / decimal.Parse(JmlLine)) * RunningLine), 0, MidpointRounding.AwayFromZero) : result;
                result = (result == 0) ? MaxSPB() : (Rule > 1) ? MaxSPB() : result;
            }

            result = (pp.isMaterialBudgetKhusus(ddlItemName.SelectedValue, ddlDeptName.SelectedValue) == false) ?

            result :
                   (pp.MaterialBudgetKhusus(ddlItemName.SelectedValue, ddlDeptName.SelectedValue) > 0) ?
                   pp.MaterialBudgetKhusus(ddlItemName.SelectedValue, ddlDeptName.SelectedValue) : MaxSPB();
            return result;
        }

        private decimal MaxSPBPrj(string BM)
        {
            //setingan jumlah max line di plant  yng digunakan untuk membuat bidgetsp
            decimal result = 0;
            Users user = (Users)Session["Users"];
            PlanningFacade pp = new PlanningFacade();
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            result = pp.MaterialBudgetPrj(ddlItemName.SelectedValue, BM);
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
        private decimal TotalSPBPrj(string ID)
        {
            decimal result = 0;
            PakaiDetailFacade pkd = new PakaiDetailFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            string Periode = Tahun.ToString();
            Periode += (Bulan <= 6) ? "01" : "01";
            int Rule = pkd.RuleCalc(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            string periodeSPB = PeriodeRule(Rule);
            result = pkd.TotalQtySPBPrj(ID, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
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
        private bool MasihBolehSPBKhusus(decimal QtySkr, decimal QtyBudget)
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
            decimal MaxQtySPB = (Rule == 12) ? this.MaxSPB() : (QtyBudget == 0) ? this.MaxSPB() : QtyBudget;
            //decimal addBudget = pkd.AddQtyBudget(Periode, int.Parse(ddlItemName.SelectedValue), 1, int.Parse(ddlDeptName.SelectedValue));
            result = ((TotalSPB + QtySkr) <= MaxQtySPB) ? true : false;
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
                Statuse = (p.Status == 0) ? "Open" : (p.Status == 1) ? "Head" : "Manager";
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
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
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
        protected void txtNomor_Change(object sender, EventArgs e)
        {
            LoadItems1();
        }
        private void LoadItems1()
        {
            ddlItemName.Items.Clear();
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade m = new MTC_ProjectFacade();
            int ID = m.getID(txtNomor.Text);
            ddlProjectName.SelectedValue = ID.ToString();
            arrData = m.RetrieveEstimasiMaterialList(ID.ToString());
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            if (arrData.Count > 0)
            {

                foreach (EstimasiMaterial em in arrData)
                {
                    ddlItemName.Items.Add(new ListItem(em.ItemName + "( " + em.ItemCode + " )->" + em.Jumlah, em.ItemID.ToString()));
                }
            }
            else
            {
                //ddlItemName.Items.Add(new ListItem("Estimasi material belum ada untuk project tsb", "0"));
                //ddlItemName.SelectedIndex = 1;
            }
        }
    }
}