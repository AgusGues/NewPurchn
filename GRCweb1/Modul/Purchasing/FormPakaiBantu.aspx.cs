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
    public partial class FormPakaiBantu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";

                //bisa diatur lewat users.DeptId
                //perlu tanya user utk per user bisa akses dept apa aja
                LoadDept();
                stk.Visible = (PurchnConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                stk.Checked = (PurchnConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                if (Request.QueryString["PakaiNo"] != null)
                {
                    LoadPakai(Request.QueryString["PakaiNo"].ToString());
                }
                else
                {
                    clearForm();
                }
                LoadListLockSPP();
                CheckReOrderPoint();
                LoadGroupPM();
                //CheckSPBPending();
                spGroup.Enabled = false;
                x1.Visible = false;
                x2.Visible = false;
                CheckBox1.Text = (((Users)Session["Users"]).UnitKerjaID == 7) ? "Citeureup" : "Karawang";
            }
            else
            {
                if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                {
                    //base on grouppurchn: GroupID pada Inventory = 1 & GroupPurchn 

                    LoadItems(2);

                    txtCariNamaBrg.Text = string.Empty;
                }
            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
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
        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPakaiDetail"] = new ArrayList();
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
            txtTanggal.Enabled = true;
            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            //else
            //    ddlDeptName.Items.Clear();
            ddlNoPolisi.SelectedValue = "0";

            ddlItemName.Items.Clear();

            ArrayList arrList = new ArrayList();
            arrList.Add(new PakaiDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = true;
            ddlDeptName.Enabled = true;

            GridView1.Columns[5].Visible = true;
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        //private void LoadNoPol(int DeptID)
        //{
        //    /**
        //     * Load no polisi via webservice untuk dept 18[Mtc], 26[Armada],10[Material]
        //     */
        //    DataSet NoPol = new DataSet();
        //    if ((DeptID == 18 && spGroup.SelectedValue == "13") ||
        //        DeptID == 26 || DeptID == 14 || DeptID==10)
        //    {
        //        ddlNoPolisi.Items.Clear();
        //        //ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", ""));
        //        bpas_api.WebService1 api = new bpas_api.WebService1();
        //        NoPol = api.GetNoPolByPlant(((Users)Session["Users"]).UnitKerjaID.ToString());

        //        ddlNoPolisi.DataSource = NoPol;
        //        ddlNoPolisi.DataValueField = "ID";
        //        ddlNoPolisi.DataTextField = "KendaraanNo";
        //        ddlNoPolisi.DataBind();
        //        ddlNoPolisi.Items.Add(new ListItem("Forklift -", "1000"));
        //        ddlNoPolisi.Items.Add(new ListItem("Umum -", "1000"));
        //        ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
        //        ddlNoPolisi.SelectedValue = "0";
        //    }
        //    else
        //    {
        //        ddlNoPolisi.Items.Clear();
        //    }
        //}
        private void LoadNoPol(int DeptID)
        {
            /**
             * Load no polisi via webservice untuk dept 18[Mtc], 26[Armada],10[Material],14[edp],[4]mtc ctrp
             */
            DataSet NoPol = new DataSet();
            DataSet AlKend = new DataSet();
            ArrayList arrNopol = new ArrayList();
            if ((DeptID == PurchnConfig("MTCinArmada") && spGroup.SelectedValue == "13") ||
                DeptID == 26 || DeptID == 14 || DeptID == 10)
            {
                ddlNoPolisi.Items.Clear();
                MTC_ArmadaFacade arm = new MTC_ArmadaFacade();
                bpas_api.WebService1 api = new bpas_api.WebService1();
                string UnitKerja = string.Empty;
                string AliasKend = string.Empty;
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
                ddlNoPolisi.Items.Add(new ListItem("Forklift -", "1001"));
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
        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockDept", "SPB").Split(',');
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                if (PurchnConfig("MaterialByDept") == 0 || arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString()))
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

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Zona"] = null;
            if (ddlDeptName.SelectedIndex > 0)
            {
                DeptFacade deptFacade = new DeptFacade();
                Dept dept = deptFacade.RetrieveById(int.Parse(ddlDeptName.SelectedValue));
                if (deptFacade.Error == string.Empty && dept.ID > 0)
                {
                    txtKodeDept.Text = dept.DeptCode;

                    txtCariNamaBrg.Focus();
                    #region Proses Sarmut
                    /** aktifkan sarmut untuk dept mtc mek,el,ut*/
                    if (dept.ID == 4 || dept.ID == 5 || dept.ID == 18)
                    {
                        spGroup.Enabled = true;
                    }
                    else
                    {
                        spGroup.Enabled = false;
                    }
                    #endregion
                    #region Proses Perawatan kendaraan
                    if (ddlDeptName.SelectedValue == "26" && PurchnConfig("SPBArmada") == 1)
                    {
                        ShowInfo("SPArmada");
                    }
                    x1.Visible = (PurchnConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == "26") ? true : false;
                    x2.Visible = (PurchnConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == "26") ? true : false;
                    LoadNoPol(int.Parse(ddlDeptName.SelectedValue));
                    #endregion
                    #region Proses Engineering Project New
                    if ((ddlDeptName.SelectedValue == "19" ||
                        ddlDeptName.SelectedValue == "16" ||
                        ddlDeptName.SelectedValue == "21" ||
                        ddlDeptName.SelectedValue == "22") &&
                        ProjectNewAktif("ProjectNewAktif") == 1)
                    {
                        prj.Visible = true;
                        ddlProjectName.Visible = true;
                        LoadProject();
                        if (ProjectNewAktif("InfoUpdateSPB") == 1)
                        {
                            ShowInfo("SPBProject", "UpdateSPBProject.txt");
                        }
                    }
                    else
                    {
                        prj.Visible = false;
                        ddlProjectName.Visible = false;
                        ddlProjectName.SelectedValue = "0";
                    }
                    #endregion
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
                            Session["Zona"] = null;
                            break;
                    }
                    #endregion
                }
            }
        }
        protected void GetProdukLine(int DeptID)
        {
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "SPBProduksi").Split(new string[] { "," }, StringSplitOptions.None);

            if (arrDept.Contains(DeptID.ToString()) || arrDept.Contains("All"))
            {
                ddlProdLine.Items.Clear();
                ddlProdLine.Items.Add(new ListItem("--Pilih Line --", "0"));
                ArrayList arrDepts = new DeptFacade().GetLine();
                foreach (Plant pl in arrDepts)
                {
                    ddlProdLine.Items.Add(new ListItem(pl.PlantCode.ToString(), pl.ID.ToString()));
                }
                ddlProdLine.Enabled = true;

                /** Beny : 03 Agustus 2022 **/
                shift.Visible = false;
                /** End **/
            }
            else
            {
                ddlProdLine.Items.Clear();
                ddlProdLine.Enabled = false;
            }
        }

        /** Beny : 03 Agustus 2022 **/
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKeterangan.Text = ddlShift.SelectedValue;
        }
        /** End **/

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
        private void LoadItems(int intGroupID)
        {
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            string Stocked = (PurchnConfig("ViewOnlyStockInSPB") == 1 && stk.Checked == true) ? " jumlah >0 and " : "";
            arrInventory = inventoryFacade.RetrieveByCriteriaWithGroupID(Stocked + "A.ItemName", txtCariNamaBrg.Text, intGroupID);
            if (arrInventory.Count == 0)
            {
                arrInventory = inventoryFacade.RetrieveByCriteriaWithGroupID(Stocked + "A.ItemName", txtCariNamaBrg.Text, 11);
            }
            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["AlasanBatal"] = "";
            if (e.CommandName == "AddDelete")
            {
                // bisa cancel hanya bisa level head dept ke atas
                int intApv = ((Users)Session["Users"]).Apv;

                //string str = Session["AlasanBatal"].ToString();
                //if (Session["AlasanBatal"] != null)
                //{
                //    if (Session["AlasanBatal"].ToString() == string.Empty)
                //    {
                //        DisplayAJAXMessage(this, "Alasan Hapus tidak boleh kosong");
                //        return;
                //    }
                if (intApv > 0)
                {
                    #region proses delete

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
                        string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
                        //string strPakaiTipe = "KM";
                        Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                        if (pakaiFacade.Error == string.Empty && pki.ID > 0)
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
                                #region Baca data di session detail
                                foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                                {
                                    if (pkiDetail.ItemID == receiptDetail1.ItemID)
                                    {
                                        pkiDetailID = pkiDetail.ID;
                                        i = x;
                                        valid = true;
                                        break;
                                    }

                                    x = x + 1;
                                }
                                #endregion
                                #region Proses hapus grid dimulai

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
                                    else
                                    {
                                        DisplayAJAXMessage(this, "Data berhasil dihapus");
                                        EventLog evn = new EventLog();
                                        evn.CreatedBy = ((Users)Session["Users"]).UserName;
                                        evn.DocumentNo = txtPakaiNo.Text;
                                        evn.EventName = "Delete SPB Detail ID :" + ID.ToString() + " karena " + Session["AlasanBatal"].ToString();
                                        evn.ModulName = "Pemakaian Bahan Pembantu";
                                        int rst = new EventLogFacade().Insert(evn);
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


                                    Session["AlasanBatal"] = null;
                                    string pno = (Request.QueryString["PakaiNo"] != null) ? string.Empty : "?PakaiNo=" + txtPakaiNo.Text;
                                    Response.Redirect(Request.RawUrl + pno);
                                    Session["ListOfPakaiDetail"] = arrTransfer;
                                    GridView1.DataSource = arrTransfer;
                                    GridView1.DataBind();

                                }
                                #endregion

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
                    #endregion
                }
                //else
                //{
                //   string myScript = "confirm_hapus()";
                //   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", myScript, true);
                //     //ScriptManager.RegisterStartupScript(this, this.GetType(), "MyScript", myScript, true);
                //}//end of session
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
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
            Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KM");
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
                string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";

                //string strPakaiTipe = "KM";
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
            #region validasi Input data qty
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
            #region validasi pilih line untuk pemakaian BP (solar)
            string StatusPilihLine = GetSPBConfig("SPBProduksi", "Status").ToString();
            string[] arrDept = GetSPBConfig("SPBProduksi", "OnlyDept").Split(new string[] { "," }, StringSplitOptions.None);
            string[] arrItemID = GetSPBConfig("SPBProduksi", "ItemID").Split(new string[] { "," }, StringSplitOptions.None);
            if (ddlProdLine.SelectedIndex == 0 && StatusPilihLine == "1" &&
                (arrDept.Contains(ddlDeptName.SelectedValue.ToString()) || arrDept.Contains("All")) &&
                (arrItemID.Contains(ddlItemName.SelectedValue.ToString()) || arrItemID.Contains("All")))
            {
                DisplayAJAXMessage(this, "Production Line harus diisi jika melakukan SPB " + ddlItemName.SelectedItem.ToString());
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
            if (PurchnConfig("SPBArmada") == 1)
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
            #region Proses Material multi gudang
            SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
            decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
            if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text) - StockOtherDept)
            {
                DisplayAJAXMessage(this, "Sebagian Quantity Stock sudah dipesan oleh departemen lain, coba kurangi Qty pakainya");
                return;
            }
            ddlDeptName.Enabled = false;
            #endregion
            #region Cek Tanggal terakhir input dan periode closing
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);
            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);

            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl && PurchnConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    int intApv = ((Users)Session["Users"]).Apv;
                    if (intApv == 0)
                        return;

                }
            }
            #endregion
            #region Check Project name untuk dept maintenance engineering
            string[] arrDepts = GetSPBConfig("Project", "OnlyDept").Split(new string[] { "," }, StringSplitOptions.None);
            if (ddlProjectName.SelectedIndex == 0 &&
                arrDepts.Contains(ddlDeptName.SelectedValue.ToString()) &&
                ProjectNewAktif("ProjectNewAktif") == 1)
            {
                DisplayAJAXMessage(this, "Nama project belum ditentukan");
                return;
            }
            #endregion
            #region cek double itemcode
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
                        if (pki.ItemCode == txtItemCode.Text && !arrItemID.Contains(ddlItemName.SelectedValue.ToString()))
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
            #region CheckReorder Point
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
            #endregion
            #region Pengumpulan data
            decimal stoke = 0;
            decimal.TryParse(txtStok.Text, out stoke);
            pakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            pakaiDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            pakaiDetail.RowStatus = 0;
            pakaiDetail.Keterangan = txtKeterangan.Text;
            pakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            pakaiDetail.UomID = int.Parse(txtUomID.Text);
            pakaiDetail.ItemCode = txtItemCode.Text;
            pakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            pakaiDetail.UOMCode = txtUom.Text;
            pakaiDetail.KartuStock = stoke;
            //for inv
            pakaiDetail.ItemTypeID = 1;
            //fro prodline
            pakaiDetail.LineNo = (ddlProdLine.SelectedValue == string.Empty) ? 0 : int.Parse(ddlProdLine.SelectedValue.ToString());
            /** sarmut proses */

            /** Beny : 03 Agustus 2022**/
            pakaiDetail.Kelompok = "";
            pakaiDetail.Press = (ddlTebal.SelectedValue == DBNull.Value.ToString()) ? "0" : ddlTebal.SelectedItem.ToString().Trim();
            /** End **/

            #region for new project
            /** added on 01-10-2014 */
            pakaiDetail.SarmutID = (spGroup.SelectedValue == string.Empty) ? 0 : int.Parse(spGroup.SelectedValue.ToString());
            pakaiDetail.DeptCode = txtKodeDept.Text;
            //pakaiDetail.ProjectID = (ProjectNewAktif("ProjectNewAktif") == 1 && arrDepts.Contains(ddlDeptName.SelectedValue.ToString())) ? int.Parse(ddlProjectName.SelectedValue) : 0;
            //pakaiDetail.ProjectName = (ProjectNewAktif("ProjectNewAktif") == 1 && arrDepts.Contains(ddlDeptName.SelectedValue.ToString())) ? ddlProjectName.SelectedItem.ToString() : string.Empty;
            #endregion
            #region for New project
            pakaiDetail.ProjectID = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDepts.Contains(ddlDeptName.SelectedValue.ToString()) || arrDepts.Contains("All"))) ? int.Parse(ddlProjectName.SelectedValue) : 0;
            pakaiDetail.ProjectName = (ProjectNewAktif("ProjectNewAktif") == 1 && (arrDepts.Contains(ddlDeptName.SelectedValue.ToString()) || arrDepts.Contains("All"))) ? ddlProjectName.SelectedItem.ToString() : string.Empty;

            #endregion
            #region for perawartan kendaraan
            /**
             * Added on 02-06-2014
             * Pemakain sparepart untuk kendaraan
             */
            string npol = (ddlNoPolisi.SelectedValue.ToString() == "0") ? string.Empty : ddlNoPolisi.SelectedItem.ToString();
            pakaiDetail.IDKendaraan = (ddlNoPolisi.SelectedValue == "0" || ddlNoPolisi.SelectedValue == string.Empty) ? 0 : int.Parse(ddlNoPolisi.SelectedValue.ToString());
            pakaiDetail.NoPol = (ddlNoPolisi.SelectedValue == "0") ? string.Empty : npol.Substring(0, npol.IndexOf(" -"));
            if (int.Parse(ddlNoPolisi.SelectedValue) >= 1000)
            {
                pakaiDetail.NoPol = ddlForklif.SelectedValue.ToString();
            }
            else
            {
                pakaiDetail.NoPol = (ddlNoPolisi.SelectedValue == "0") ? string.Empty : npol.Substring(0, npol.IndexOf(" -"));
            }
            if (spGroup.SelectedValue == "14")
            {
                pakaiDetail.NoPol = ddlForklif.SelectedValue.ToString();
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
            Session["ListOfPakaiDetail"] = arrListPakaiDetail;
            #endregion
            #region proses sarmut maintenance
            /**
             * For sarmut maintenance
             * yang masuk sarmut hanya dept id : 4,5,18
             */
            MTC_Sarmut sarmut = new MTC_Sarmut();
            ArrayList arrListSarmut = new ArrayList();

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
            #region proses pakai bp for project
            if (ProjectNewAktif("ProjectNewAktif") == 1 && arrDepts.Contains(ddlDeptName.SelectedValue.ToString()))
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
            GridView1.Columns[6].Visible = true;
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
            ddlNoPolisi.SelectedValue = "0";
            ddlItemName.SelectedIndex = 0;
            ddlZona.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("ListOfPakaiDetail");
            Session.Remove("Pakai");
            Session.Remove("PakaiNo");
            x1.Visible = false;
            x2.Visible = false;
            ddlNoPolisi.SelectedValue = "0";

            /** Beny : 8 Agustus 2022 **/
            press.Visible = false;
            /** End **/

            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int bln = DateTime.Parse(txtTanggal.Text).Month;
            int thn = DateTime.Parse(txtTanggal.Text).Year;
            int noBaru = 0;
            #region input validation
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            #endregion
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
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
                if (nowTgl < lastTgl && PurchnConfig("SystemClosing") == 0)
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
                #region baris ini sengaja dinonaktifkan
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
            #region Proses Header Pakai Bahan Pembantu
            //here for document number
            company = new Company();
            companyFacade = new CompanyFacade();
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";

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
            //2 for BAHAN BANTU
            pakai.PakaiTipe = 2;
            pakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            pakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            pakai.Status = 0;
            pakai.AlasanCancel = "";
            pakai.CreatedBy = ((Users)Session["Users"]).UserName;
            pakai.ItemTypeID = 1;
            #endregion
            #region Proses Pakai Detail
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
                ddlProdLine.Items.Clear();
                ddlProdLine.Enabled = false;
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
            eventLog.ModulName = "Entry Pemakaian Bahan Bantu";
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
                #region Limit pemakaian solar
                /**
                 * Check Pembatasan pemakaian solar
                 * added on 16-10-2014
                 * base on wo logistic
                 */
                string LockStatus = GetSPBConfig("MaterialLock", "Status");
                if (GetSPBConfig("MaterialLock", "Status") == "1")
                {
                    string[] mat = { GetSPBConfig("MaterialLock", "MaterialID") };
                    string[] dept = GetSPBConfig("MaterialLock", "OnlyDeptID").Split(new string[] { "," }, StringSplitOptions.None);
                    if (mat.Contains(ddlItemName.SelectedValue.ToString()) &&
                      !dept.Contains(((Users)Session["Users"]).DeptID.ToString()))
                    {
                        DisplayAJAXMessage(this, "Material " + ddlItemName.SelectedItem + " tidak bisa di SPB, silahkan hub. Logistic BB");
                        ddlItemName.Items.Clear();
                        return;
                    }
                }
                #endregion

                ArrayList arrSpb = (Session["ListOfPakaiDetail"] != null) ? (ArrayList)Session["ListOfPakaiDetail"] : new ArrayList();
                SaldoInventoryFacade price = new SaldoInventoryFacade();
                InventoryFacade invFacade = new InventoryFacade();
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));
                decimal StockAkhir = invFacade.GetStockAkhir(ddlItemName.SelectedValue, 1, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());

                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    int AvgPrice = price.GetPrice(inv.ID, getMonthName(int.Parse(DateTime.Parse(txtTanggal.Text).Month.ToString())), DateTime.Parse(txtTanggal.Text).Year, inv.ItemTypeID);
                    txtAvgPrice.Text = AvgPrice.ToString();
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    txtStok.Text = StockAkhir.ToString("N2");
                    decimal totalSPB = 0;
                    foreach (PakaiDetail iv in arrSpb)
                    {
                        if (iv.ItemCode == inv.ItemCode) { totalSPB += iv.Quantity; }
                    }

                    txtStok.Text = (StockAkhir - totalSPB).ToString("N2");

                    //txtStok.Text = inv.Jumlah.ToString("N2");
                    txtGroupID.Text = inv.GroupID.ToString();
                    txtQtyPakai.Focus();
                    #region ProdukLine
                    string[] arrItemID = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "SPBProduksi").Split(new string[] { "," }, StringSplitOptions.None);
                    string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "SPBProduksi").Split(new string[] { "," }, StringSplitOptions.None);
                    string StatusPilihLine = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Status", "SPBProduksi");
                    if ((arrDept.Contains(ddlDeptName.SelectedValue) || arrDept.Contains("All")) &&
                        (arrItemID.Contains(ddlItemName.SelectedValue.ToString()) || arrItemID.Contains("All")) &&
                        StatusPilihLine == "1")
                    {
                        GetProdukLine(int.Parse(ddlDeptName.SelectedValue));
                        ddlProdLine.Enabled = true;
                    }
                    #endregion
                }
            }

            /** Beny : 8 Agustus 2022 **/
            PakaiDetailFacade pakaiFacade = new PakaiDetailFacade();
            PakaiDetail pakai = new PakaiDetail();
            pakai = pakaiFacade.CekItemFlooculant(ddlItemName.SelectedValue.ToString());

            if (ddlDeptName.SelectedValue == "2" && pakai.ItemID.ToString() == ddlItemName.SelectedValue.ToString())
            {
                press.Visible = true;
                LoadTebal();
            }
            /** End **/
        }

        /** Beny : 8 Agustus 2022 **/
        private void LoadTebal()
        {
            ddlTebal.Items.Clear();
            ArrayList arrTebal = new ArrayList();
            PakaiFacade pFacade = new PakaiFacade();
            arrTebal = pFacade.RetrieveTebal();
            ddlTebal.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (Pakai pk in arrTebal)
            {
                ddlTebal.Items.Add(new ListItem(pk.Keterangan.ToString(), pk.Keterangan.ToString()));
            }
        }
        /** End **/

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadPakai(txtSearch.Text);
        }

        private void LoadPakai(string strPakaiNo)
        {
            // here too
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "M";
            //string strPakaiTipe = "KM";
            clearForm();
            string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockDept", "SPB").Split(',');
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai pakai = pakaiFacade.RetrieveByNoWithStatus(strPakaiNo, strPakaiTipe);
            if (pakaiFacade.Error == string.Empty && pakai.ID > 0)
            {
                Session["id"] = pakai.ID;

                txtPakaiNo.Text = pakai.PakaiNo;
                txtKodeDept.Text = pakai.DeptCode;
                LoadDept();
                ddlDeptName.SelectedValue = pakai.DeptID.ToString();
                txtTanggal.Text = pakai.PakaiDate.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = pakai.CreatedBy;
                txtStatus.Text = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(pakai.Status.ToString(), "StatusSPB");
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
                GridView1.Columns[6].Visible = (pakai.Status > 1) ? false : true;
                Session["PakaiNo"] = pakai.PakaiNo;
                Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                GridView1.DataSource = arrListPakaiDetail;
                GridView1.DataBind();
                string arrLockField = "txtKodeDept,txtStatus,txtStok,txtTanggal,ddlDeptName,txtCreatedBy,txtPakaiNo";
                DisableField(false, arrLockField);

                if (pakai.Status > 2)
                    btnCancel.Enabled = false;
                else
                    btnCancel.Enabled = true;

                //if (pakai.Status > 0)
                if (pakai.ID > 0 && pakai.Status > 1)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                    btnUnlok.Visible = (arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                }
                else if (pakai.ID > 0 && pakai.Status == 0 || pakai.ID > 0 && pakai.Status == 1)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = true;
                }
                else
                {
                    btnUnlok.Visible = false;
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
                    return "Tidak ada List Item yang di-input";
            }

            return string.Empty;
        }
        private void CheckReOrderPoint()
        {
            int ROP = 0;
            InventoryFacade inventoryFacade = new InventoryFacade();
            ROP = inventoryFacade.CheckReorderPoint("2", ((Users)Session["Users"]).ID);

            if (ROP == 1 && ((Users)Session["Users"]).DeptID == 10)
            {
                Timer2.Enabled = true;
                Panel2.Visible = true;
            }
            else
            {
                Timer2.Enabled = false;
                Panel2.Visible = false;
                Timer1.Enabled = false;
            }

        }
        private void LoadListLockSPP()
        {
            //int intROP = 0;
            int strerror = 0;
            InventoryFacade inventoryFacade = new InventoryFacade();
            ROPFacade ropfacade = new ROPFacade();
            //intROP = inventoryFacade.CheckReorderPoint("2");
            ArrayList arrInventory = new ArrayList();
            arrInventory = inventoryFacade.ListReorderPoint(2);
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
            playsound();
            Timer2.Enabled = false;
            Panel2.Visible = false;
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
            if (Panel2.BackColor == System.Drawing.Color.LightSteelBlue)
            {
                Panel2.BackColor = System.Drawing.Color.Red;
                Console.Beep();
            }
            else
            {
                Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
                Console.Beep();
            }
        }

        public int PurchnConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn status = purchn.PurchnTools(ModulName);
            return status.Status;
        }
        protected void spGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlDeptName.SelectedValue == PurchnConfig("MTCinArmada").ToString() &&
                spGroup.SelectedValue == "13" && PurchnConfig("SPBArmada") == 1)
            {
                ShowInfo("SPArmada");
            }
            x1.Visible = (PurchnConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == PurchnConfig("MTCinArmada").ToString() && spGroup.SelectedValue == "13") ? true : false;
            x2.Visible = (PurchnConfig("SPBArmada") == 1 && ddlDeptName.SelectedValue == PurchnConfig("MTCinArmada").ToString() && spGroup.SelectedValue == "13") ? true : false;
            LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
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
        protected void ddlNoPolisi_SelectedIndexChanged(object sender, EventArgs e)
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
                Session["Judul"] = "Info Update SPB ";
                Session["UserID"] = userid.ToString();
                Session["ModulName"] = Info;
                Session["FileName"] = "UpdateSPBArmada.txt";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "OpenWindows", "infoupdate()", true);
            }

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadNoPol(int.Parse(ddlDeptName.SelectedValue.ToString()));
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
        protected string getMonthName(int Bulan)
        {
            SaldoInventoryFacade Bln = new SaldoInventoryFacade();
            return Bln.GetStrMonth(Bulan);
        }

        private string GetSPBConfig(string Section, string Key)
        {
            var cnf = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini"));
            return cnf.Read(Key, Section);
        }
        protected void btnUnlok_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(Session["id"].ToString());
            Pakai objPakai = new Pakai();
            PakaiFacade pakai = new PakaiFacade();
            /** check closing periode */
            int bln = Convert.ToDateTime(txtTanggal.Text).Month;
            int Year = Convert.ToDateTime(txtTanggal.Text).Year;
            int BlnSkr = DateTime.Now.Month;
            int ThnSkr = DateTime.Now.Year;
            if (bln == BlnSkr && Year == ThnSkr)
            {
                int Result = pakai.UpdateRelease(ID, 1);//unlock status
                if (Result == 1)
                {
                    EventLog evn = new EventLog();
                    evn.EventName = "Un Approve SPB " + txtPakaiNo.Text;
                    evn.ModulName = "SPB Bahan Pembantu";
                    evn.DocumentNo = txtPakaiNo.Text;
                    evn.CreatedBy = ((Users)Session["Users"]).UserName;
                    int rst = new EventLogFacade().Insert(evn);
                    DisplayAJAXMessage(this, "Un Aproved document berhasil");
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                DisplayAJAXMessage(this, "SPB Bulan " + Global.nBulan(bln) + " " + Year.ToString() + " tidak bisa di unlock");
            }
        }
        private void DisableField(bool status, string arrID)
        {
            string[] objID = arrID.Split(',');

            if (objID[0] == string.Empty)
            {
                foreach (object c in Div5.Controls)
                {
                    if (c is TextBox) { ((TextBox)c).Enabled = status; }
                    if (c is DropDownList) { ((DropDownList)c).Enabled = status; }
                }
            }
            else
            {
                for (int i = 0; i < objID.Count(); i++)
                {
                    if (Div5.FindControl(objID[i].ToString().TrimStart().TrimEnd()) is TextBox)
                    {
                        TextBox txt = (TextBox)Div5.FindControl(objID[i].ToString().TrimStart().TrimEnd());
                        if (txt != null) ((TextBox)txt).Enabled = status;
                    }

                }
                for (int x = 0; x < objID.Count(); x++)
                {
                    DropDownList ddl = new DropDownList();
                    if (Div5.FindControl(objID[x].ToString().TrimStart().TrimEnd()) is DropDownList)
                    {
                        ddl = (DropDownList)Div5.FindControl(objID[x].ToString().TrimStart().TrimEnd());
                        if (ddl != null) ((DropDownList)ddl).Enabled = status;
                    }
                }
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