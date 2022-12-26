using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class TransaksiSPP3 : System.Web.UI.Page
    {
        // private ArrayList arrSppDetail = new ArrayList(ItemID = 0;);
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSPP.Enabled = false;
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                Session["NewProject"] = "yes";
                Session["AlasanBatal"] = null;
                lblCheck.Text = (users.UnitKerjaID == 7) ? "Check stock Citeureup" : "Check stock Karawang";
                LoadTipeBarang();
                Session["id"] = null;
                clearForm();
                btnCancel.Enabled = (Request.QueryString["NoSPP"] == null) ? false : true;
                btnClose.Enabled = (Request.QueryString["NoSPP"] == null) ? false : true;
                ddlMinta.SelectedIndex = 1;
                Session["where"] = string.Empty;

                //iko
                FillItems("AM_Group", " where RowStatus >-1 Order By ID", GroupID);
                FillItems("AM_Lokasi", " where RowStatus >-1 Order By ID", LokasiID);
                LoadGroupPM();
                //FillItems("MTC_GroupSarmut", " where RowStatus >-1 Order By ID", ddlGroupSarmut);
                FillItems("MaterialMTCGroup", " where RowStatus >-1 Order By ID", ddlGroupEfesien);
                //iko

                // Tambahan for NewCode Asset 30 Juni 2019
                if (users.DeptID == 22 || users.DeptID == 30)
                {
                    trAsset1.Visible = false; trGroupSarMut1.Visible = false;
                }
                else
                {
                    trAsset1.Visible = true; trGroupSarMut1.Visible = true;
                }
                // End Tambahan

                if (Request.QueryString["NoSPP"] != null)
                {
                    clearForm();
                    LoadSPP(Request.QueryString["NoSPP"].ToString());
                }
                else
                {
                    LoadNamaHead();
                }
                lbAddItem.Enabled = false;
                Session["id"] = null;
                #region multigudang
                /** for multigudang
                 * per 04/07/2017 semua spp selain dept 10 (logistik bj) masuk ke private
                 * jika ada yng butuh kordinasi antar dept
                 */
                switch (users.DeptID)
                {
                    case 10:
                    case 13:
                        ddlMultiGudang.Enabled = true;
                        ddlMultiGudang.SelectedIndex = 1;
                        break;
                    default:
                        ddlMultiGudang.Enabled = false;
                        ddlMultiGudang.SelectedIndex = 2;
                        break;
                }
                #region multigudang
                //if(users.DeptID==19)
                //{
                //    /**
                //     * user maintenance
                //     * default public, private optional
                //     */
                //    ddlMultiGudang.Enabled = true;
                //    ddlMultiGudang.SelectedIndex = 2;
                //}
                //else if (users.DeptID == 16 || users.DeptID == 21 ||
                //        users.DeptID == 22)
                //{
                //    /** 
                //     * user project
                //     * only private
                //     */
                //    ddlMultiGudang.Enabled = false;
                //    ddlMultiGudang.SelectedIndex = 2;
                //}
                //else
                //{
                //    ddlMultiGudang.Enabled = true;
                //    ddlMultiGudang.SelectedIndex = 1;
                //}

                /** end of muti gudang**/
                #endregion
                #endregion
                if (Request.QueryString["fr"] != null)
                {
                    if (Session["SPP"] != null)
                    {
                        ArrayList arrBdg = (ArrayList)Session["SPP"];
                        LoadListSPPfromBudget(arrBdg);
                    }
                }
                //biaya
                rowBiaya.Visible = false;
            }
            else
            {
                if (ddlTipeSPP.SelectedValue == "5"
                    && CheckStatusBiaya() == 1
                    && PurchnConfig("InfoUpdateSPP") == 1)
                {
                    ShowInfo("Biaya");

                }
            }
            btnCancel.Attributes.Add("onclick", "return confirm_batal();");
            btnClose.Attributes.Add("onclick", "return confirm_close();");
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
                ddlNoPol.Items.Clear();
                MTC_ArmadaFacade arm = new MTC_ArmadaFacade();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                Global2 api = new Global2();
                string UnitKerja = string.Empty;
                string AliasKend = string.Empty;

                //UnitKerja = (((Users)Session["Users"]).UnitKerjaID == 1) ? "1" : "7";
                //AliasKend = (((Users)Session["Users"]).UnitKerjaID == 1) ? "GRCBoardCtrp" : "GRCBoardKrwg"; 

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

                NoPol = api.GetNoPolByPlant(UnitKerja);
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {
                    string Alias = string.Empty;
                    try
                    {
                        AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), AliasKend);
                        foreach (DataRow al in AlKend.Tables[0].Rows)
                        {
                            Alias = al["NamaKendaraan"].ToString();
                        }
                    }
                    catch { }

                    arrNopol.Add(new MTC_Armada
                    {
                        ID = Convert.ToInt32(nR["ID"].ToString()),
                        NoPol = nR["KendaraanNo"].ToString() + " - " + Alias
                        //NoPol = nR["KendaraanNo"].ToString()
                    });
                }
                ddlNoPol.DataSource = arrNopol;
                ddlNoPol.DataValueField = "ID";
                ddlNoPol.DataTextField = "NoPol";
                ddlNoPol.DataBind();
                //ddlNoPol.Items.Add(new ListItem("Froklift - ", "1001"));
                //ddlNoPol.Items.Add(new ListItem("Umum - ", "1000"));
                ddlNoPol.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                ddlNoPol.SelectedValue = "0";

                //else
                //{
                //    ddlNoPolisi.Items.Clear();
                //    ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
                //    ddlNoPolisi.SelectedValue = "0";
                //}
            }
            catch
            {
                DisplayAJAXMessage(this, "Data kendaraan tidak dapat di load, ada msalah koneksi ke HO, hubungi IT Dept");
            }
        }

        protected void ddlNoPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet NoPol = new DataSet();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                Global2 api = new Global2();
                NoPol = api.DetailKendaraan(ddlNoPol.SelectedItem.ToString());
                if (ddlNoPol.SelectedValue != "0")
                {
                    string NoPolL = ddlNoPol.SelectedItem.ToString();
                }
                //if (ddlNoPol.SelectedValue == "1001")
                //{
                //    frk.InnerHtml = "Nama " + ddlNoPol.SelectedItem.Text;
                //    LoadData("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                //    ddlForklif.Visible = true;
                //}
                //else if (ddlNoPolisi.SelectedValue == "1000")
                //{
                //    frk.InnerHtml = "Kendaraan";
                //    LoadData("Umum", "U" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                //    ddlForklif.Visible = true;
                //}
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet error");
            }

        }

        //iko
        protected void FillItems(string Tabel, string where, DropDownList Fld)
        {
            /**
            * Tampilkan data group asset ke dalam dropdown
            */

            Fld.Items.Clear();
            ArrayList arrData = new ArrayList();
            AssetManagementFacade dataFacade = new AssetManagementFacade();
            arrData = dataFacade.Retrieve(Tabel, where);
            Fld.Items.Add("");
            foreach (AssetManagement ListData in arrData)
            {
                switch (Tabel)
                {
                    case "AM_Group": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "AM_Class": Fld.Items.Add(new ListItem(ListData.NamaClass, ListData.ID.ToString())); break;
                    case "AM_SubClass": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "AM_Lokasi": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "Dept": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;

                    // 30 Juni 2019
                    case "asset": Fld.Items.Add(new ListItem(ListData.ItemName, ListData.AMLokasiID.ToString())); break;
                    // End

                    case "MTC_GroupSarmut": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "MaterialMTCGroup": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;

                }
            }

            Fld.SelectedIndex = 0;
        }
        protected void GroupID_OnTextChange(object sender, EventArgs e)
        {
            ClassID.Items.Clear();
            SbClassID.Items.Clear();
            FillItems("AM_Class", " where GroupID='" + GroupID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", ClassID);
            //GetKode("AM_Group", " Where ID='" + GroupID.SelectedValue + "' and RowStatus >-1", txtItemCode);
            //GetKode("AM_Group", " Where ID='" + GroupID.SelectedValue + "' and RowStatus >-1", umur_asset);
        }
        protected void GetKode(string Tabel, string where, TextBox Fld)
        {
            string Kode = string.Empty;
            ArrayList arrData = new ArrayList();
            AssetManagementFacade dataFacade = new AssetManagementFacade();
            arrData = dataFacade.Retrieve(Tabel, where);
            foreach (AssetManagement data in arrData)
            {
                switch (Tabel)
                {
                    case "AM_Group": Fld.Text = (Fld.ID == "umur_asset") ? data.UmurAsset.ToString() : data.KodeGroup.ToUpper(); break;
                    case "AM_Class": Fld.Text = Fld.Text.Substring(0, 1) + "." + data.KodeClass.ToUpper(); break;
                    case "AM_SubClass": Fld.Text = Fld.Text.Substring(0, 4) + "." + data.KodeGroup.ToUpper(); break;
                }
            }
        }
        protected void ClassID_OnTextChange(object sender, EventArgs e)
        {
            FillItems("AM_SubClass", " where ClassID='" + ClassID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", SbClassID);
            //GetKode("AM_Class", " Where ID='" + ClassID.SelectedValue + "'", txtItemCode);

        }
        protected void SbClassID_OnTextChange(object sender, EventArgs e)
        {
            //GetKode("AM_SubClass", " Where ID='" + SbClassID.SelectedValue + "'", txtItemCode);

        }
        private void SelectGroupAsset(string strTipename)
        {
            GroupID.ClearSelection();
            foreach (ListItem item in GroupID.Items)
            {
                if (item.Value == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        private void SelectClassAsset(string strTipename)
        {
            ClassID.ClearSelection();
            foreach (ListItem item in ClassID.Items)
            {
                if (item.Value == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        private void SelectSubClassAsset(string strTipename)
        {
            SbClassID.ClearSelection();
            foreach (ListItem item in SbClassID.Items)
            {
                if (item.Value == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        //iko

        // 30 Juni 2019
        private void SelectLokasiAsset(string strTipename)
        {
            LokasiID.ClearSelection();
            foreach (ListItem item in LokasiID.Items)
            {
                if (item.Value == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        // End 





        private void LoadNamaHead()
        {
            Users users = (Users)Session["Users"];

            //int DepartemenID = users.DeptID;

            //DeptFacade deptFacade = new DeptFacade();
            //Dept dept = new Dept();
            //dept = deptFacade.RetrieveById(DepartemenID);
            //txtNamaHead.Text = dept.NamaHead;

            UsersHeadFacade userHeadFacade = new UsersHeadFacade();
            UsersHead userHead = userHeadFacade.RetrieveByUserID(users.ID.ToString());
            if (userHeadFacade.Error == string.Empty && userHead.ID > 0)
            {
                UsersFacade usersFacade = new UsersFacade();
                Users users2 = usersFacade.RetrieveById(userHead.HeadID);

                txtNamaHead.Text = users2.UserName;
            }
            else
            {
                if (users.Apv == 0)
                {
                    DisplayAJAXMessage(this, "Head-ID tidak ada ..!");
                    return;
                }
            }

        }
        private void LoadKodeSPP()
        {
            Users users = (Users)Session["Users"];
            int depoid = users.UnitKerjaID;

            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            int noid = Convert.ToInt32(ddlTipeSPP.SelectedValue);
            GroupsPurchn groupsPurchn = new GroupsPurchn();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();

            //txtSPP.Text = groupsPurchnFacade.GetKodeSPP(noid);
            string NoAkhir = companyFacade.GetKodeCompany(depoid) + groupsPurchnFacade.GetKodeSPP(noid);

            SPP sPP = new SPP();
            SPPFacade sPPFacade = new SPPFacade();
            sPPFacade = new SPPFacade();

            int noUrut = sPPFacade.CountNoSPP(NoAkhir);
            noUrut = noUrut + 1;
            sPP.NoSPP = NoAkhir + noUrut.ToString().PadLeft(4, '0');
            txtSPP.Text = sPP.NoSPP;

        }
        private void LoadDataGridSPP(ArrayList arrSPPDetail)
        {
            GridView1.DataSource = arrSPPDetail;
            GridView1.DataBind();
            lstSPP.DataSource = arrSPPDetail;
            lstSPP.DataBind();
        }
        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }
        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();

            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }
        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            int itemtypeid = 0;

            if (ddlTipeBarang.SelectedIndex == 1) itemtypeid = 1;
            if (ddlTipeBarang.SelectedIndex == 2) itemtypeid = 2;
            if (ddlTipeBarang.SelectedIndex == 3) itemtypeid = 3;

            arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).DeptID, ((Users)Session["Users"]).GroupID, itemtypeid);

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                //if (groupsPurchn.ID != 6)
                //{
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
                //}
            }
        }
        private void LoadddlItems(DropDownList ddl)
        {
            ArrayList arrItems = new ArrayList();

            InventoryFacade inventoryFacade = new InventoryFacade();
            AssetFacade assetFacade = new AssetFacade();
            BiayaFacade biayaFacade = new BiayaFacade();

            arrItems = inventoryFacade.Retrieve();

            ddl.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrItems)
            {
                ddl.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrSPPDetail = new ArrayList();
            try
            {
                Session["SPPHeader"] = null;
                SPPFacade sPPFacade = new SPPFacade();
                SPP spp = sPPFacade.RetrieveByCriteriaSPP("A.NoSpp", txtSearch.Text);

                if (spp.ID > 0)
                {
                    Session["id"] = spp.ID;
                    Session["SPPHeader"] = spp;
                    txtSPP.Text = spp.NoSPP;
                    txtTglInput.Text = spp.Minta.ToString("dd-MMM-yyyy");
                    txtCreatedBy.Text = spp.CreatedBy;
                    string tCreatedBy = spp.CreatedBy;

                    UsersFacade usersFacade = new UsersFacade();
                    Users user = new Users();
                    user = usersFacade.RetrieveByUserName(tCreatedBy);
                    int DepartemenID = user.DeptID;

                    DeptFacade deptFacade = new DeptFacade();
                    user = new UsersFacade().RetrieveById(spp.HeadID);
                    txtNamaHead.Text = user.UserName;

                    btnCancel.Enabled = (spp.Status == 0 && spp.Approval <= user.Apv) ? true : false;
                    btnClose.Enabled = (spp.Status == 0 && spp.Approval <= user.Apv) ? true : false;

                    if (spp.Status == -2) txtStatus.Text = "Close";
                    if (spp.Status == -1) txtStatus.Text = "Batal";
                    if (spp.Status == 0) txtStatus.Text = "Open";
                    if (spp.Status == 1) txtStatus.Text = "Parsial";
                    if (spp.Status == 2) txtStatus.Text = "Full PO";

                    if (spp.PermintaanType == 1) ddlMinta.SelectedIndex = 0;
                    if (spp.PermintaanType == 2) ddlMinta.SelectedIndex = 1;
                    if (spp.PermintaanType == 3) ddlMinta.SelectedIndex = 2;

                    if (spp.ItemTypeID == 1) ddlTipeBarang.SelectedIndex = 1;
                    if (spp.ItemTypeID == 2) ddlTipeBarang.SelectedIndex = 2;
                    if (spp.ItemTypeID == 3) ddlTipeBarang.SelectedIndex = 3;

                    if (spp.Approval == 0) txtApproval.Text = "Admin";
                    if (spp.Approval == 1) txtApproval.Text = "Head";
                    if (spp.Approval == 2) txtApproval.Text = "Dept.Mgr";
                    if (spp.Approval > 2) txtApproval.Text = "Plant.Mgr";

                    /** control button */
                    if (spp.Approval == 0 && spp.Status == 0)
                    {
                        btnCancel.Enabled = true; btnClose.Enabled = true; btnUpdate.Disabled = true;
                    }
                    else if (spp.Approval <= ((Users)Session["Users"]).Apv && spp.Status == 0)
                    {
                        btnCancel.Enabled = true; btnClose.Enabled = true; btnUpdate.Disabled = true;
                    }
                    else
                    {
                        btnCancel.Enabled = false;
                        btnClose.Enabled = false;
                        btnUpdate.Disabled = true;
                        //btnCetak.Disabled = true;
                    }

                    GroupsPurchnFacade groupPurchnFacade = new GroupsPurchnFacade();
                    GroupsPurchn groupPurchn = groupPurchnFacade.RetrieveById(spp.GroupID);
                    if (groupPurchnFacade.Error == string.Empty) SelectTipeSPP(groupPurchn.GroupDescription);

                    SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                    arrSPPDetail = sPPDetailFacade.RetrieveBySPPID(spp.ID);

                    if (sPPDetailFacade.Error == string.Empty)
                    {
                        Session["NoSPP"] = spp.NoSPP;
                        Session["ListOfSPPDetail"] = arrSPPDetail;
                        btnUpdate.Disabled = true;
                    }
                    return arrSPPDetail;
                }
                else
                {
                    Session["id"] = null;
                    DisplayAJAXMessage(this, "No SPP tidak ditemukan");
                    //arrSPPDetail=new ArrayList();
                    //return arrSPPDetail;
                }

                arrSPPDetail = new ArrayList();
                return arrSPPDetail;
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
                return arrSPPDetail;
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            SPPFacade sPPFacade = new SPPFacade();

            Users users = (Users)Session["Users"];
            int depoid = users.UnitKerjaID;
            string strEvent = "Insert";

            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            int intGroupID = 0;
            if (int.Parse(ddlTipeSPP.SelectedValue) == 9 || int.Parse(ddlTipeSPP.SelectedValue) == 8)
            {
                intGroupID = 8;   //krn utk penomoran elektrik & mekanik = KS / sparepart
            }
            else
                intGroupID = int.Parse(ddlTipeSPP.SelectedValue);

            GroupsPurchn groupsPurchn = new GroupsPurchn();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            string kd = companyFacade.GetKodeCompany(depoid) + groupsPurchnFacade.GetKodeSPP(intGroupID);

            SPP sPP = new SPP();
            if (Session["id"] != null)
            {
                sPP.ID = int.Parse(Session["id"].ToString());
                strEvent = "Edit";
            }

            //new add
            UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
            UsersHead usersHead = usersHeadFacade.RetrieveByUserID(users.ID.ToString());
            if (usersHeadFacade.Error == string.Empty && usersHead.ID > 0)
            {
                sPP.HeadID = usersHead.HeadID;
            }
            else
            {
                DisplayAJAXMessage(this, "Head-ID tidak ada ..!");
                return;
            }
            //

            sPP.CreatedTime = DateTime.Parse(txtTglInput.Text);
            sPP.ItemTypeID = Convert.ToInt32(ddlTipeBarang.SelectedValue.ToString());
            sPP.GroupID = Convert.ToInt32(ddlTipeSPP.SelectedValue.ToString());
            sPP.NoSPP = txtSPP.Text;
            sPP.Minta = DateTime.Parse(txtTglInput.Text);
            #region deactive line
            // tanggal permintaan jangan di rubah
            //if (ddlMinta.SelectedIndex == 0)
            //{
            //    sPP.Minta = DateTime.Parse(txtTglInput.Text);  // Top Urgent
            //}
            //else
            //{
            //    if (ddlMinta.SelectedIndex == 1)
            //    {
            //        //leadtime di aktifkan, baca data leadtime di table inventory
            //        // jika lead time=0 or null 7 hari else sesuai leadtime
            //        // update leadtime ketika di approve plant manager
            //        int LeadTime = (ddlTipeSPP.SelectedValue.ToString() == "1") ? CheckLeadTime(int.Parse(ddlNamaBarang.SelectedValue)) : 7;

            //        sPP.Minta = DateTime.Parse(txtTglInput.Text).AddDays(LeadTime); // Biasa
            //    }
            //    else
            //    {
            //        //sesuai dengan tgl kirim yang ditulis saat spp
            //        sPP.Minta = DateTime.Parse(txtTglInput.Text).AddDays(14); // Sesuai Schedule
            //    }
            //}
            //br nyampe sini === ikuti prosesFacadenya OP
            //sPP.Jumlah = Convert.ToInt32(txtQty.Text);   hrsnya ada di SPPDetailProcessFacade
            //sPP.ItemID = Convert.ToInt32(ddlName.SelectedValue);

            //if (ddlTipeBarang.SelectedIndex == 1)
            //{
            //    Inventory inventory = new Inventory();
            //    InventoryFacade inventoryFacade = new InventoryFacade();
            //    inventory = inventoryFacade.RetrieveRecordByID(sPP.ItemID);
            //    sPP.SatuanID = inventory.UOMID;
            //}
            //else
            //{
            //    if (ddlTipeBarang.SelectedIndex == 2)
            //    {
            //        Asset asset = new Asset();
            //        AssetFacade assetFacade = new AssetFacade();
            //        asset = assetFacade.RetrieveRecordByID(sPP.ItemID);
            //        sPP.SatuanID = asset.UOMID;
            //    }
            //    else
            //    {
            //        Biaya biaya = new Biaya();
            //        BiayaFacade biayaFacade = new BiayaFacade();
            //        biaya = biayaFacade.RetrieveRecordByID(sPP.ItemID);
            //        sPP.SatuanID = biaya.UOMID;
            //    }
            //}
            #endregion
            sPP.SatuanID = (ddlMultiGudang.SelectedIndex > 0) ? int.Parse(ddlMultiGudang.SelectedValue) : 0;
            sPP.JumlahSisa = 0;
            sPP.Sudah = 0;
            sPP.FCetak = 0;
            sPP.UserID = users.ID;
            sPP.Pending = 0;
            sPP.Inden = 0;
            sPP.AlasanBatal = string.Empty;
            sPP.AlasanCLS = string.Empty;
            sPP.Status = 0;
            sPP.CreatedBy = users.UserName;
            sPP.CreatedTime = DateTime.Parse(txtTglInput.Text);
            sPP.LastModifiedBy = users.UserName;
            sPP.LastModifiedTime = DateTime.Parse(txtTglInput.Text);
            sPP.TglBuat = DateTime.Parse(txtTglInput.Text);
            sPP.PermintaanType = ddlMinta.SelectedIndex + 1;//type spp 1 urgent,2 biasa, 3 sesuai sch
            sPP.DepoID = users.UnitKerjaID;

            SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            SPPNumber sPPNumber = new SPPNumber();
            sPPNumber = sPPNumberFacade.RetrieveByGroupsID(intGroupID);
            if (sPPNumberFacade.Error == string.Empty)
            {
                if (sPPNumber.ID > 0)
                {
                    sPPNumber.SPPCounter = sPPNumber.SPPCounter + 1;
                    sPPNumber.KodeCompany = kd.Substring(0, 1);
                    sPPNumber.KodeSPP = kd.Substring(1, 1);
                    sPPNumber.LastModifiedBy = users.UserName;
                }
            }

            string strError = string.Empty;
            ArrayList arrSPPDetail = new ArrayList();
            if (Session["ListOfSPPDetail"] != null)
            {
                arrSPPDetail = (ArrayList)Session["ListOfSPPDetail"];
                if (arrSPPDetail.Count == 0)
                {
                    DisplayAJAXMessage(this, "Item barang tidak boleh kosong");
                    return;
                }
            }

            SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(sPP, arrSPPDetail, sPPNumber);

            if (sPP.ID > 0)
            {
                strError = sPPProsessFacade.Update();
                if (strError == string.Empty)
                {
                    btnUpdate.Disabled = true;

                    InsertLog(strEvent);
                    clearForm();


                }
            }
            else
            {
                strError = sPPProsessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtSPP.Text = sPPProsessFacade.SPPNo;
                    Session["NoSPP"] = sPPProsessFacade.SPPNo;
                    btnUpdate.Disabled = true;
                    InsertLog(strEvent);
                    //update Budgeting ATK Status jika spp ATK dari budget
                    #region Update BudgetATK
                    Budget bdg = new Budget();
                    if (Request.QueryString["fr"] != null)
                    {
                        if (Request.QueryString["fr"] == "budget")
                        {
                            BudgetingFacade b = new BudgetingFacade();
                            int Tahun = int.Parse(Request.QueryString["thn"].ToString());
                            int Bulan = int.Parse(Request.QueryString["bln"].ToString());
                            ArrayList arrSPP = (ArrayList)Session["SPP"];
                            SPP spp = new SPPFacade().RetrieveByNo(sPPProsessFacade.SPPNo);
                            foreach (Budget bd in arrSPP)
                            {
                                bdg.ItemID = bd.ItemID;
                                bdg.Tahun = Tahun;
                                bdg.Bulan = Bulan;
                                bdg.BudgetID = spp.ID;
                                bdg.Approval = 3;
                                //bdg.SPPNo = sPPProsessFacade.SPPNo;
                                int result = b.UpdateStatus(bdg);
                            }
                        }
                        Session["SPP"] = null;
                    }
                    #endregion
                    foreach (SPPDetail rop in arrSPPDetail)
                    {

                        ROPFacade ropfacade = new ROPFacade();
                        decimal SPPnoPO = ropfacade.JumlahSPPBlmPO(rop.ItemID, rop.ItemTypeID);
                        decimal POnoReceipt = ropfacade.JumlahPOblmReceipt(rop.ItemID, rop.ItemTypeID);
                        ropfacade.UpdateROPBySPP(rop.ItemID, rop.SPPID, (rop.Quantity + SPPnoPO + POnoReceipt));
                    }

                }
            }
            Session["ListOfSPPDetail"] = null;
        }
        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            // bisa cancel hanya bisa level head dept ke atas
            int intApv = ((Users)Session["Users"]).Apv;

            if (Session["id"] != null && intApv > 0)
            {


                int id = (int)Session["id"];
                string strEvent = string.Empty;

                SPPFacade sppFacade = new SPPFacade();
                SPP spp = sppFacade.RetrieveById(id);
                if (sppFacade.Error == string.Empty && spp.ID > 0 && spp.Status >= 0)
                {
                    if (spp.Approval == 1)
                    {
                        DisplayAJAXMessage(this, "Info : sudah di-approval Head Dept");
                        //return;                
                    }
                    if (spp.Approval == 2)
                    {
                        DisplayAJAXMessage(this, "Info : sudah di-approval Manager");
                        //return;
                    }

                    if (Session["AlasanBatal"] != null)
                    {
                        spp.AlasanBatal = Session["AlasanBatal"].ToString();
                        Session["AlasanBatal"] = null;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Batal tidak boleh kosong / blank");
                        return;
                    }

                    SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
                    ArrayList arrCurrentPakaiDetail = sppDetailFacade.RetrieveBySPPID(spp.ID);
                    if (sppDetailFacade.Error == string.Empty)
                    {
                        ArrayList arrSPPDetail = new ArrayList();
                        foreach (SPPDetail sppDetail in arrCurrentPakaiDetail)
                        {
                            if (sppDetail.QtyPO > 0)
                            {
                                string aMess = "Tidak dapat di-Cancel karena sudah buat PO utk Kode Barang = " + sppDetail.ItemCode;
                                DisplayAJAXMessage(this, aMess);
                                return;
                            }

                            arrSPPDetail.Add(sppDetail);
                        }

                        SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(spp, arrSPPDetail, new SPPNumber());

                        string strError = sPPProsessFacade.CancelSPP();
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
        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            // bisa close hanya bisa level head dept ke atas
            int intApv = ((Users)Session["Users"]).Apv;

            if (/*Session["id"] != null &&*/ intApv > 0)
            {
                int id = (int)Session["id"];
                string strEvent = string.Empty;

                SPPFacade sppFacade = new SPPFacade();
                SPP spp = sppFacade.RetrieveById(id);
                if (sppFacade.Error == string.Empty && spp.ID > 0 && spp.Status >= 0)
                {
                    if (spp.Approval == 1 && intApv < 1)
                    {
                        DisplayAJAXMessage(this, "Info : sudah di-approval Head Dept");
                        // return;                
                    }
                    if (spp.Approval == 2 && intApv < 2)
                    {
                        DisplayAJAXMessage(this, "Info : sudah di-approval Manager");
                        //return;
                    }

                    if (Session["AlasanClose"] != null)
                    {
                        spp.AlasanBatal = Session["AlasanClose"].ToString();

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Close tidak boleh kosong / blank");
                        return;
                    }

                    SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
                    ArrayList arrCurrentPakaiDetail = sppDetailFacade.RetrieveBySPPID(spp.ID);
                    if (sppDetailFacade.Error == string.Empty)
                    {

                        ArrayList arrSPPDetail = new ArrayList();
                        foreach (SPPDetail sppDetail in arrCurrentPakaiDetail)
                        {
                            if (sppDetail.QtyPO > 0)
                            {
                                string aMess = "Tidak dapat di-Close karena sudah buat PO utk Kode Barang = " + sppDetail.ItemCode;
                                DisplayAJAXMessage(this, aMess);
                                return;
                            }

                            arrSPPDetail.Add(sppDetail);
                        }

                        SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(spp, arrSPPDetail, new SPPNumber());

                        string strError = sPPProsessFacade.CloseSPP();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, strError);
                            return;
                        }

                        else
                        {
                            strEvent = "Close SPP All " + Session["AlasanClose"].ToString();
                            InsertLog(strEvent);
                            Session["AlasanClose"] = null;
                            DisplayAJAXMessage(this, "Close berhasil .....");
                            clearForm();
                        }
                    }
                }
            }
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input SPP";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtSPP.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Add")
            {

                GridViewRow row = GridView1.Rows[index];
                #region depreciated
                //SelectItem(row.Cells[1].Text);
                //string gCategory = string.Empty;
                //if (ddlTipeBarang.SelectedIndex == 0)
                //{
                //    DisplayAJAXMessage(this, "Pilih Tipe Barang ");
                //    return;
                //}

                //if (btnCancel.Enabled == false && btnUpdate.Disabled == true)
                //{
                //    DisplayAJAXMessage(this, "No SPP ini sudah dibatalkan.....");
                //    return;
                //}
                //if (btnClose.Enabled == false && btnUpdate.Disabled == true)
                //{
                //    DisplayAJAXMessage(this, "No SPP ini sudah di-close.....");
                //    return;
                //}

                //ArrayList arrSPPDetail = new ArrayList();
                //if (Session["ListOfSPPDetail"] != null)
                //    arrSPPDetail = (ArrayList)Session["ListOfSPPDetail"];

                //TextBox txtCariBarang = (TextBox)GridView1.Rows[index].FindControl("txtCariBarang");
                //DropDownList ddlItemCode = (DropDownList)GridView1.Rows[index].FindControl("ddlItemCode");
                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[index].FindControl("txtKodeBarang");
                //TextBox txtSatuan = (TextBox)GridView1.Rows[index].FindControl("txtSatuan");
                //TextBox txtQty = (TextBox)GridView1.Rows[index].FindControl("txtQty");
                //TextBox txtKeterangan = (TextBox)GridView1.Rows[index].FindControl("txtKeterangan");

                //// for min & max aja nanti
                ////if (decimal.Parse(txtStok.Text) - decimal.Parse(txtQty.Text) < 0)
                ////{
                ////    DisplayAJAXMessage(this, "Stok tidak mencukupi.....");
                ////    return;
                ////}

                //Users users = (Users)Session["Users"];

                //SPPDetail sPPDetail = new SPPDetail();

                //if (arrSPPDetail.Count > 0)
                //    sPPDetail = (SPPDetail)arrSPPDetail[index];

                ////sPPDetail.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                //sPPDetail.ItemID = int.Parse(ddlItemCode.SelectedValue);
                //sPPDetail.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
                //sPPDetail.Quantity = Convert.ToDecimal((txtQty.Text));
                //sPPDetail.ItemCode = txtKodeBarang.Text;
                //sPPDetail.Satuan = txtSatuan.Text;
                //sPPDetail.CariItemName = txtCariBarang.Text;
                //sPPDetail.Keterangan = txtKeterangan.Text;

                ////sPPDetail.ItemName = ddlItemCode.SelectedItem.ToString();

                //if (ddlTipeBarang.SelectedIndex == 1)
                //{
                //    Inventory inventory = new Inventory();
                //    InventoryFacade inventoryFacade = new InventoryFacade();
                //    inventory = inventoryFacade.RetrieveRecordByID2(sPPDetail.ItemID);

                //    sPPDetail.UOMID = inventory.UOMID;
                //    sPPDetail.GroupID = inventory.GroupID;
                //    sPPDetail.ItemName = inventory.ItemName;
                //    int xKodeBarang = Convert.ToInt16(inventory.ItemCode.Substring(6, 1).ToString());

                //    //string xStockSign = xStockValue.ToString();
                //    if (xKodeBarang == 9)
                //    {

                //        //add here for cek min / max / re=order
                //        if (sPPDetail.Quantity + inventory.Jumlah > inventory.MaxStock)
                //        {
                //            DisplayAJAXMessage(this, "Barang tersebut sudah melebihi batas maximum! Transfer Batal..");
                //            return;
                //        }
                //    }
                //    //until here
                //}
                //if (ddlTipeBarang.SelectedIndex == 2)
                //{
                //    Asset asset = new Asset();
                //    AssetFacade assetFacade = new AssetFacade();
                //    asset = assetFacade.RetrieveRecordByID(sPPDetail.ItemID);

                //    sPPDetail.GroupID = asset.GroupID;
                //    sPPDetail.UOMID = asset.UOMID;
                //    sPPDetail.ItemName = asset.ItemName;
                //}
                //if (ddlTipeBarang.SelectedIndex == 3)
                //{
                //    Biaya biaya = new Biaya();
                //    BiayaFacade biayaFacade = new BiayaFacade();
                //    biaya = biayaFacade.RetrieveRecordByID(sPPDetail.ItemID);

                //    sPPDetail.UOMID = biaya.UOMID;
                //    sPPDetail.GroupID = biaya.GroupID;
                //    sPPDetail.ItemName = biaya.ItemName;
                //}


                //if (arrSPPDetail.Count == 0)
                //    arrSPPDetail.Add(sPPDetail);

                ////Cek sebanyak 9 item dalam 1 SPP
                //int jumRecord = 0;
                //foreach (SPPDetail spd in arrSPPDetail)
                //{
                //    if (spd.ItemID > 0)
                //    {
                //        //jumRecord = jumRecord + arrSPPDetail.Count;
                //        jumRecord = jumRecord + 1;

                //    }
                //}

                //if (jumRecord > 9)
                //{
                //    arrSPPDetail.RemoveAt(arrSPPDetail.Count - 1);
                //    DisplayAJAXMessage(this, "Maximal 9 Item Barang");
                //}
                //else
                //{
                //    if (((SPPDetail)arrSPPDetail[arrSPPDetail.Count - 1]).ItemID > 0)
                //        arrSPPDetail.Add(new SPPDetail());

                //}
                //ddlTipeBarang.Enabled = false;
                //Session["ListOfSPPDetail"] = arrSPPDetail;

                //GridView1.DataSource = arrSPPDetail;
                //GridView1.DataBind();

                //Session["Edit"] = 1;
                #endregion
            }

            else if (e.CommandName == "AddDelete")
            {

                if (Session["ListOfSPPDetail"] != null)
                {
                    SPPDetail sPPDetail = (SPPDetail)((ArrayList)Session["ListOfSPPDetail"])[index];

                    ArrayList arrTransferDetail = new ArrayList();
                    arrTransferDetail = (ArrayList)Session["ListOfSPPDetail"];

                    //cek dulu dah pernah save atau blom
                    // kl blom boleh langsug hapus
                    if (Session["id"] != null)
                    {
                        // bisa cancel hanya bisa level head dept ke atas
                        int intApv = ((Users)Session["Users"]).Apv;
                        if (intApv > 0)
                        {
                            int intSPPDetailID = 0;
                            string strEvent = string.Empty;
                            int sppID = (int)Session["id"];

                            SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
                            ArrayList arrCurrentPakaiDetail = sppDetailFacade.RetrieveBySPPID(sppID);
                            if (sppDetailFacade.Error == string.Empty)
                            {
                                if (sPPDetail.QtyPO > 0)
                                {
                                    DisplayAJAXMessage(this, "SPP untuk Kode Barang tersebut sudah dibuat PO");
                                    return;
                                }

                                int i = 0;
                                int x = 0;

                                SPPDetail receiptDetail1 = (SPPDetail)arrTransferDetail[index];
                                bool valid = false;
                                int sppDetailID = 0;

                                foreach (SPPDetail sDetail in arrCurrentPakaiDetail)
                                {
                                    if (sDetail.ItemID == receiptDetail1.ItemID)
                                    {
                                        sppDetailID = sDetail.ID;

                                        i = x;
                                        valid = true;
                                        break;
                                    }

                                    x = x + 1;
                                }

                                if (valid == false)
                                {
                                    arrTransferDetail.RemoveAt(index);
                                    Session["ListOfSPPDetail"] = arrTransferDetail;
                                    GridView1.DataSource = arrTransferDetail;
                                    GridView1.DataBind();
                                }
                                else
                                {
                                    SPPDetail sppDetail = (SPPDetail)arrTransferDetail[index];
                                    ArrayList arrSPPDetail = new ArrayList();
                                    foreach (SPPDetail sd in arrTransferDetail)
                                    {
                                        if (sd.ID == sppDetailID)
                                        {
                                            arrSPPDetail.Add(sd);
                                        }
                                    }

                                    SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(new SPP(), arrSPPDetail, new SPPNumber());
                                    intSPPDetailID = sppDetail.ID;

                                    string strError = sPPProsessFacade.CancelSPPDetail();
                                    if (strError != string.Empty)
                                    {
                                        DisplayAJAXMessage(this, strError);
                                        return;
                                    }

                                    ArrayList arrTransfer = new ArrayList();
                                    foreach (SPPDetail sd in arrTransferDetail)
                                    {
                                        //if (rd.DocumentNo != strDocumentNo)
                                        if (sd.ID != intSPPDetailID)
                                        {
                                            arrTransfer.Add(sd);
                                        }
                                    }

                                    strEvent = "Hapus SPP per Baris";
                                    InsertLog(strEvent);

                                    Session["ListOfSPPDetail"] = arrTransfer;
                                    GridView1.DataSource = arrTransfer;
                                    GridView1.DataBind();
                                }
                            }
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Level tidak mencukupi untuk hapus");
                        }
                    }
                    else
                    {
                        //blom di save
                        arrTransferDetail.RemoveAt(index);

                        Session["ListOfSPPDetail"] = arrTransferDetail;

                        Session["Edit"] = 1;

                        GridView1.DataSource = arrTransferDetail;
                        GridView1.DataBind();
                    }

                }
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Session["cariSPP"] = 1;
            if (txtSearch.Text != string.Empty) LoadDataGridSPP(LoadGridByCriteria());

        }
        private void GetDataFromSession()
        {
            if (Session["SPPHeader"] != null)
            {
                SPP sPP = (SPP)Session["SPPHeader"];
                txtSPP.Text = sPP.NoSPP;
                ddlTipeSPP.SelectedIndex = sPP.GroupID;
                //txtKeterangan.Text = sPP.Keterangan;
                ddlTipeBarang.SelectedIndex = sPP.ItemTypeID;

                Users users = (Users)Session["Users"];

                if (Session["ListOfSPPDetail"] != null)
                {
                    ArrayList arrSPPDetail = (ArrayList)Session["ListOfSPPDetail"];
                    GridView1.DataSource = arrSPPDetail;
                    GridView1.DataBind();

                    //btnCancel.Enabled = true;
                    if (users.TypeUnitKerja == 1)
                    {

                    }

                    if (sPP.Status > 0 || sPP.Status == -1)
                        btnUpdate.Disabled = true;
                    else
                        btnUpdate.Disabled = false;


                    if (((Users)Session["Users"]).TypeUnitKerja == 2)
                    {
                        if (sPP.Status > 2)
                            btnUpdate.Disabled = true;
                        else
                            btnUpdate.Disabled = false;
                    }

                }
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //Session.Remove("NoSPP");
            Session.Remove("ListOfSPPDetail");
            Session.Remove("SPPHeader");
            Response.Redirect(Request.Url.AbsolutePath);
            LoadNamaHead();
            clearForm();
            GridView1.Columns[5].Visible = true;
            GridView3.Columns[5].Visible = true;

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnClose.Enabled = false;
        }
        protected void txtCari_TextChanged(object sender, EventArgs e)
        {
            //TextBox txl = (TextBox)sender;

        }
        private void LoadItem(string strNabar)
        {
            try
            {
                ddlNamaBarang.Items.Clear();
                string[] ItemSesuaiSCH = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SesuaiSch", "SPP").Split(',');
                //ddlMinta.SelectedIndex =(ddlNamaBarang.SelectedIndex 1;
                ArrayList arrItems = new ArrayList();
                Users users = (Users)Session["Users"];
                InventoryFacade inventoryFacade = new InventoryFacade();
                AssetFacade assetFacade = new AssetFacade();

                // 30 Juni 2019
                AssetManagementFacade aMF = new AssetManagementFacade();
                // End

                BiayaFacade biayaFacade = new BiayaFacade();

                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    arrItems = inventoryFacade.RetrieveByCriteriaWithGroupIDROP("ItemName", strNabar, int.Parse(ddlTipeSPP.SelectedValue), users.ID);
                }
                if (ddlTipeBarang.SelectedIndex == 2)
                {
                    arrItems = assetFacade.RetrieveByCriteriaWithGroupID("ItemName", strNabar, int.Parse(ddlTipeSPP.SelectedValue));
                }
                if (ddlTipeBarang.SelectedIndex == 3)
                {
                    /** new spp biaya tdk aktif**/

                    AccClosingFacade cls = new AccClosingFacade();
                    AccClosing stat = cls.CheckBiayaAktif();
                    if (stat.Status != 1)
                    {
                        arrItems = biayaFacade.RetrieveByCriteriaWithGroupID("ItemName", strNabar, int.Parse(ddlTipeSPP.SelectedValue));
                    }
                    else
                    {
                        /** new spp biaya aktif**/
                        arrItems = biayaFacade.RetrieveByCriteriaWithGroupID("len(itemcode)=15 and ItemTypeID", "3", int.Parse(ddlTipeSPP.SelectedValue));
                    }
                }
                ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));

                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    foreach (Inventory inventory in arrItems)
                    {

                        ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));

                    }
                }
                if (ddlTipeBarang.SelectedIndex == 2)
                {
                    foreach (Asset asset in arrItems)
                    {
                        ddlNamaBarang.Items.Add(new ListItem(asset.ItemName + " (" + asset.ItemCode + ")", asset.ID.ToString()));
                        // 30 Juni 2019
                        //ddlNamaBarang.Items.Add(new ListItem(asset.ItemName, asset.ID.ToString()));
                        // End
                    }
                }
                if (ddlTipeBarang.SelectedIndex == 3)
                {
                    if (arrItems.Count > 0)
                    {
                        foreach (Biaya biayane in arrItems)
                        {
                            ddlNamaBarang.Items.Add(new ListItem(biayane.ItemName + " (" + biayane.ItemCode + ")", biayane.ID.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DisplayAJAXMessage(this, error + " Item tidak ditemukan");
            }
        }
        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTipeSPP.Items.Clear();
            LoadTipeSPP();
            /**
             * For proses project new
             * Added on 14-11-2014
             */
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project").Split(new string[] { "," }, StringSplitOptions.None);
            string StatNewProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AddProject", "SPP");
            if (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && StatNewProject == "1" && ddlTipeBarang.SelectedValue == "1")
            {
                AutoCmplt.Enabled = true;
                biayaAutoComplete.Enabled = false;
                ddlMultiGudang.Enabled = true;
                // ddlMultiGudang.SelectedValue = "1";
                ddlTypeBiaya.Enabled = false;
                PaneLNoPol.Visible = false;
            }
            else
            {
                AutoCmplt.Enabled = false;
                ddlMultiGudang.Enabled = true;
                biayaAutoComplete.Enabled = (ddlTipeBarang.SelectedValue == "3") ? true : false;
                ddlTypeBiaya.Enabled = (ddlTipeBarang.SelectedValue == "3") ? true : false;
                PaneLNoPol.Visible = false;

            }

            if (ddlTipeBarang.SelectedIndex == 2)
            {
                //trAsset1.Visible = true;

                // 30 Juni 2019
                trGroupSarMut1.Visible = false;
                trAsset1.Visible = false;
                // End

                //trAsset2.Visible = true;

                // 30 Juni 2019
                trAsset2.Visible = false;
                // End

                //trAsset3.Visible = true;

                // 30 Juni 2019
                trAsset3.Visible = false;
                // End

                PaneLNoPol.Visible = false;
            }
            else
            {
                trAsset1.Visible = false;
                trAsset2.Visible = false;
                trAsset3.Visible = false;
                PaneLNoPol.Visible = false;
            }

            if (ddlTipeBarang.SelectedIndex == 3)
            {
                Users users = (Users)Session["Users"];

                if (users.DeptID == 26)
                {
                    PaneLNoPol.Visible = true;
                    LabelNoPol.Visible = true; ddlNoPol.Visible = true;
                    LoadNoPol(users.DeptID);
                }
                else
                {
                    PaneLNoPol.Visible = false;
                    LabelNoPol.Visible = false; ddlNoPol.Visible = false;
                }
            }


        }

        // 30 Juni 2019
        protected void ddlNamaAsset_OnTextChange(object sender, EventArgs e)
        {
            if (int.Parse(ddlTipeSPP.SelectedValue) == 12)
            {
                LoadAssetKomponen(); txtCari.Enabled = false;
            }
            else if (int.Parse(ddlTipeSPP.SelectedValue) == 4)
            {
                LoadAssetTunggal(); txtCari.Enabled = false;
            }

        }

        private void LoadAssetKomponen()
        {
            ArrayList arrAssetKomponen = new ArrayList();
            FacadeAsset facadeAssetKomponen = new FacadeAsset();
            arrAssetKomponen = facadeAssetKomponen.RetrieveAssetKomponen(ddlNamaAsset.SelectedValue);
            ddlNamaBarang.Items.Clear();
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Komponen Asset --", "0"));
            foreach (DomainAsset NewAssetKomponen in arrAssetKomponen)
            {
                ddlNamaBarang.Items.Add(new ListItem(NewAssetKomponen.NamaProjectAsset, NewAssetKomponen.ID.ToString()));
            }
        }

        private void LoadAssetTunggal()
        {
            ArrayList arrAssetKomponen = new ArrayList();
            FacadeAsset facadeAssetKomponen = new FacadeAsset();
            arrAssetKomponen = facadeAssetKomponen.RetrieveAssetTunggal(ddlNamaAsset.SelectedValue);
            ddlNamaBarang.Items.Clear();
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Asset Tunggal --", "0"));
            foreach (DomainAsset NewAssetKomponen in arrAssetKomponen)
            {
                ddlNamaBarang.Items.Add(new ListItem(NewAssetKomponen.NamaProjectAsset, NewAssetKomponen.ID.ToString()));
            }
        }
        // End


        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipeSPP.SelectedIndex > 0)
            {
                //if (int.Parse(ddlTipeSPP.SelectedValue) == 4)
                //    ddlTipeBarang.SelectedIndex = 2;

                //else if (int.Parse(ddlTipeSPP.SelectedValue) == 5)
                //    ddlTipeBarang.SelectedIndex = 3;
                //else
                //    ddlTipeBarang.SelectedIndex = 1;

                // 30 Juni 2019
                Users user = (Users)Session["Users"];

                if (int.Parse(ddlTipeSPP.SelectedValue) == 4 && user.DeptID == 22 || int.Parse(ddlTipeSPP.SelectedValue) == 4 && user.DeptID == 30)
                { ddlTipeBarang.SelectedIndex = 2; trNamaAsset.Visible = true; LoadDataAssetTunggal(); }
                else if (int.Parse(ddlTipeSPP.SelectedValue) == 4 && user.DeptID != 22 || int.Parse(ddlTipeSPP.SelectedValue) == 4 && user.DeptID != 30)
                { ddlTipeBarang.SelectedIndex = 2; }
                else if (int.Parse(ddlTipeSPP.SelectedValue) == 12)
                { ddlTipeBarang.SelectedIndex = 2; trNamaAsset.Visible = true; LoadDataAssetUtama(); }
                else if (int.Parse(ddlTipeSPP.SelectedValue) == 5)
                { ddlTipeBarang.SelectedIndex = 3; }
                else
                { ddlTipeBarang.SelectedIndex = 1; }
                // End
            }
            else
                ddlTipeBarang.SelectedIndex = 0;
            ddlTipeBarang.Enabled = false;

            txtCari.Text = "";

            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));
            /**
             * SPP Biaya pencarian nama barang di nonaktifkan
             * karena item biaya cuma ada 5
             * otomatis aktif jika rowstatus di table biayanew=1
             */

            //AccClosingFacade cls = new AccClosingFacade();
            //AccClosing stat = cls.CheckBiayaAktif();
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "Project").Split(new string[] { "," }, StringSplitOptions.None);
            string StatNewProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AddProject", "SPP");

            if (int.Parse(ddlTipeBarang.SelectedValue) == 3 && CheckStatusBiaya() == 1)
            {
                LoadItem("3");
                txtCari.ReadOnly = true;
                nonbiaya.Visible = false;
                isBiaya.Visible = true;
                ddlMultiGudang.Enabled = false;
                biayaAutoComplete.Enabled = true;
                AutoCmplt.Enabled = false;
                rowBiaya.Visible = true;
            }
            else
            {
                rowBiaya.Visible = false;
                txtKetBiaya.Text = "";
                txtCari.ReadOnly = false;
                nonbiaya.Visible = true;
                isBiaya.Visible = false;
                biayaAutoComplete.Enabled = false;
                AutoCmplt.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && ddlTipeBarang.SelectedValue == "1" && StatNewProject == "1") ? true : false;
            }
            /**
             * check type spp hanya 8,9 yang otomatis private
             */
            switch (int.Parse(ddlTipeSPP.SelectedValue))
            {
                case 8:
                case 9:
                    ddlMultiGudang.SelectedValue = "2";
                    ddlMultiGudang.Enabled = false;
                    break;
                default:
                    ddlMultiGudang.SelectedValue = "1";
                    ddlMultiGudang.Enabled = false;
                    break;
            }

        }

        // 30 Juni 2019
        private void LoadDataAssetUtama()
        {
            ArrayList arrAssetUtama = new ArrayList();
            FacadeAsset facadeAssetUtama = new FacadeAsset();
            arrAssetUtama = facadeAssetUtama.RetrieveAssetUtama();
            ddlNamaAsset.Items.Clear();
            ddlNamaAsset.Items.Add(new ListItem("-- Pilih Asset --", "0"));
            foreach (DomainAsset NewAssetUtama in arrAssetUtama)
            {
                ddlNamaAsset.Items.Add(new ListItem(NewAssetUtama.NamaProjectAsset, NewAssetUtama.KodeProjectAsset));
            }
        }

        private void LoadDataAssetTunggal()
        {
            ArrayList arrAssetTunggal = new ArrayList();
            FacadeAsset facadeAssetTunggal = new FacadeAsset();
            arrAssetTunggal = facadeAssetTunggal.RetrieveAssetTunggal();
            ddlNamaAsset.Items.Clear();
            ddlNamaAsset.Items.Add(new ListItem("-- Pilih Asset --", "0"));
            foreach (DomainAsset NewAssetUtama in arrAssetTunggal)
            {
                ddlNamaAsset.Items.Add(new ListItem(NewAssetUtama.NamaProjectAsset, NewAssetUtama.KodeProjectAsset));
            }
        }
        // End

        protected int checkLockATK(string itemid, int userid)
        {
            int locking = 0;
            SPPFacade sppf = new SPPFacade();
            locking = sppf.GetLockingATK(userid, itemid);
            return locking;
        }
        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];
            int blokNonStock = 0;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string BudgetBlock = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetBlock", "SPP");
            string[] ItemSesuaiSCH = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SesuaiSch", "SPP").Split(',');
            if (ddlNamaBarang.SelectedIndex > 0)
            {
                //GridViewRow row = (GridViewRow)ddl.NamingContainer;

                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    if (CheckLastReceipt(int.Parse(ddlNamaBarang.SelectedValue)) != string.Empty)
                    {
                        DisplayAJAXMessage(this, CheckLastReceipt(int.Parse(ddlNamaBarang.SelectedValue)));
                        return;
                    }
                    //if (users.UnitKerjaID == 1 || users.UnitKerjaID == 7 || users.UnitKerjaID == 13)
                    //{
                    if (ddlTipeSPP.SelectedItem.Text.Trim().ToUpper() == "ATK")
                    {
                        if (checkLockATK(ddlNamaBarang.SelectedValue, users.ID) > 0)
                        {
                            DisplayAJAXMessage(this, "Untuk barang ATK NonBudget hanya boleh diajukan oleh HRD / ISO Dept. dan untuk ATK Budget hanya boleh diajukan oleh Logistik Dept. ");
                            return;
                        }
                    }
                    //}
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    Inventory inventory = inventoryFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                    Panel2.Visible = false;
                    //penambahan verifikasi material yng di budgeting tidak bisa di input manual
                    //base on request PM on 11-02-2016
                    //pengaturan ada di PurchnConfig.ini [SPP] BudgetBlock=1 (1=aktif 0=tidak aktif
                    if (BudgetBlock == "1")
                    {
                        if (inventory.Head == 2 || inventory.Head == 1)
                        {
                            lbAddItem.Enabled = false;
                            DisplayAJAXMessage(this, inventory.ItemName + "\\n masuk di master budget. Tidak Bisa di SPP Manual");
                            return;
                        }
                    }
                    //cek multigudang
                    SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
                    decimal StockOtherDept = sppmf.RetrieveByStock(((Users)Session["Users"]).DeptID, int.Parse(ddlNamaBarang.SelectedValue), int.Parse(ddlTipeSPP.SelectedValue), 1);

                    string kd = inventory.ItemCode.Substring(6, 1);
                    if ((inventory.Stock) == 0 && inventory.GroupID > 2)
                    {
                        //if ((inventory.Jumlah-StockOtherDept) == 0)
                        if (inventory.Jumlah == 0 || StockOtherDept == 0)
                            blokNonStock = checkNonStock(txtCari.Text.Trim(), inventory.ItemTypeID);
                        else
                        {
                            DisplayAJAXMessage(this, "Stock barang masih ada ");
                            return;
                        }
                    }

                    if (inventoryFacade.Error == string.Empty)
                    {
                        if (inventory.ID > 0)
                        {
                            //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                            //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                            //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");
                            decimal jml = (inventory.Jumlah - StockOtherDept);
                            txtStok.Text = jml.ToString("N2");
                            txtJmlMax.Text = inventory.MaxStock.ToString("N2");
                            txtSatuan.Text = inventory.UomCode;
                            txtSatuan.ReadOnly = true;
                            txtKodeBarang.Text = inventory.ItemCode;
                            txtQty.Text = "0";
                            txtKeterangan.Focus();
                            StokNonStok.Text = (inventory.Stock == 0) ? "Non Stock" : "Stock";
                            switch (inventory.Stock)
                            {
                                case 0:
                                    switch (int.Parse(ddlTipeSPP.SelectedValue))
                                    {
                                        case 8:
                                        case 9: ddlMultiGudang.SelectedValue = "2"; ddlMultiGudang.Enabled = (inventory.Stock == 0) ? false : true; break;
                                        default: ddlMultiGudang.SelectedValue = "1"; ddlMultiGudang.Enabled = true; break;
                                    }
                                    break;
                                default: ddlMultiGudang.SelectedValue = "1"; ddlMultiGudang.Enabled = true; break;
                            }
                        }
                    }

                    ddlMinta.SelectedIndex = (ItemSesuaiSCH.Contains(inventory.ItemCode.ToString()) && ddlTipeBarang.SelectedValue == "1") ? 2 : ddlMinta.SelectedIndex;
                    int leadtime = inventory.LeadTime;
                    txtLeadTime.Text = leadtime.ToString();
                    ldTime.Text = leadtime.ToString() + " day(s)";
                    POPurchnFacade poTools = new POPurchnFacade();
                    POPurchn objTools = poTools.PurchnTools("LeadTime");
                    POPurchn wkDay = poTools.PurchnTools("WorkDay");

                    int WorkDay = 0;
                    if (objTools.Status == 1 && wkDay.Status == 5)
                    {
                        WorkDay = 2;
                    }
                    else if (objTools.Status == 1 && wkDay.Status == 6)
                    {
                        WorkDay = 1;
                    }
                    else
                    {
                        WorkDay = 0;
                    }

                    switch (ddlMinta.SelectedIndex)
                    {
                        case 0:
                            txtTglKirim.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                            break;
                        case 1:
                            if (leadtime > 0)
                            {
                                FromDate = DateTime.Parse(txtTglInput.Text).ToString("yyyy-MM-dd");
                                ToDate = DateTime.Parse(txtTglInput.Text).AddDays(leadtime + WorkDay).ToString("yyyy-MM-dd");
                                int HariLibur = GetHariLibur(FromDate, ToDate);

                                txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(leadtime + WorkDay + HariLibur).ToString("dd-MMM-yyyy");

                            }
                            else
                            {
                                txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(WorkDay).ToString("dd-MMM-yyyy");
                            }
                            break;
                        case 2:
                            txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(1).ToString("dd-MMM-yyyy");
                            break;
                    }
                }
                else
                {
                    if (ddlTipeBarang.SelectedIndex == 2)
                    {
                        CheckLastReceipt(int.Parse(ddlNamaBarang.SelectedValue));
                        AssetFacade assetFacade = new AssetFacade();
                        Asset asset = assetFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                        if (assetFacade.Error == string.Empty)
                        {
                            if (asset.ID > 0)
                            {
                                //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                                //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

                                txtStok.Text = asset.Jumlah.ToString("N2");
                                txtJmlMax.Text = asset.MaxStock.ToString("N2");
                                txtSatuan.Text = asset.UomCode;

                                // 30 Juni 2019
                                //if(asset.ItemCode.Length == 11)
                                //{
                                //    if (asset.AMKodeAsset_New.Trim() == "")
                                //    {
                                //        txtKodeBarang.Text = "Kode Asset Numeric belum ada !!";
                                //    }
                                //    else
                                //    {
                                //        txtKodeBarang.Text = asset.AMKodeAsset_New.Trim();
                                //    }
                                //}
                                //else if (asset.ItemCode.Length == 13)
                                //{
                                //    txtKodeBarang.Text = asset.ItemCode;
                                //}
                                txtKodeBarang.Text = asset.ItemCode;
                                //txtKodeBarang.Text = asset.AMKodeAsset_New.Trim();
                                txtQty.Text = "0";
                                txtKeterangan.Focus();
                                StokNonStok.Text = (asset.Stock == 0) ? "Non Stock" : "Stock";
                                txtSatuan.ReadOnly = true;

                                int leadtime = asset.LeadTime;
                                txtLeadTime.Text = leadtime.ToString();
                                ldTime.Text = leadtime.ToString() + " day(s)";
                                POPurchnFacade poTools = new POPurchnFacade();
                                POPurchn objTools = poTools.PurchnTools("LeadTime");
                                POPurchn wkDay = poTools.PurchnTools("WorkDay");
                                int WorkDay = 0;
                                if (objTools.Status == 1 && wkDay.Status == 5)
                                {
                                    WorkDay = 1;
                                }
                                else if (objTools.Status == 1 && wkDay.Status == 6)
                                {
                                    WorkDay = 2;
                                }
                                else
                                {
                                    WorkDay = 0;
                                }
                                switch (ddlMinta.SelectedIndex)
                                {
                                    case 0:
                                        txtTglKirim.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                                        break;
                                    case 1:
                                        //txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(7).ToString("dd-MMM-yyyy");
                                        //break;
                                        if (leadtime > 0)
                                        {
                                            FromDate = DateTime.Parse(txtTglInput.Text).ToString("yyyy-MM-dd");
                                            ToDate = DateTime.Parse(txtTglInput.Text).AddDays(leadtime + WorkDay).ToString("yyyy-MM-dd");
                                            int HariLibur = GetHariLibur(FromDate, ToDate);
                                            int WeekEnd = GetWeekEnd(FromDate, ToDate);

                                            txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(leadtime + WeekEnd + HariLibur).ToString("dd-MMM-yyyy");

                                        }
                                        else
                                        {
                                            txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(WorkDay).ToString("dd-MMM-yyyy");
                                        }
                                        break;
                                    case 2:
                                        txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(14).ToString("dd-MMM-yyyy");
                                        break;
                                }

                                //iko 30 Juni 2019
                                SelectGroupAsset(asset.AmGroupID.ToString());
                                ClassID.Items.Clear();
                                SbClassID.Items.Clear();
                                FillItems("AM_Class", " where GroupID='" + GroupID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", ClassID);
                                SelectClassAsset(asset.AmClassID.ToString());
                                FillItems("AM_SubClass", " where ClassID='" + ClassID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", SbClassID);
                                SelectSubClassAsset(asset.AmSubClassID.ToString());


                                if (ddlTipeSPP.SelectedItem.ToString().Trim() == "Asset")
                                {
                                    //FillItems("asset", " where ItemCode='" + txtKodeBarang.Text.Trim() + "' and RowStatus >-1 ", LokasiID);
                                    //FillItems("asset", " where AMKodeAsset_New='" + txtKodeBarang.Text.Trim() + "' and RowStatus >-1 ", LokasiID);
                                    FillItems("asset", " where ItemName='" + ddlNamaBarang.Text.Trim() + "' and RowStatus >-1 ", LokasiID);
                                    SelectLokasiAsset(asset.AmLokasiID.ToString());
                                }

                                //iko

                            }
                        }
                    }
                    else
                    {
                        BiayaFacade biayaFacade = new BiayaFacade();
                        Biaya biaya = biayaFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                        if (biayaFacade.Error == string.Empty)
                        {
                            if (biaya.ID > 0)
                            {
                                //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                                //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

                                txtStok.Text = biaya.Jumlah.ToString("N2");
                                txtJmlMax.Text = biaya.MaxStock.ToString("N2");
                                txtSatuan.Text = biaya.UomCode;
                                txtSatuan_OnTextChange(null, null);
                                txtQty.Text = string.Empty;
                                txtKeterangan.Focus();
                                StokNonStok.Text = "Non Stock";
                                txtSatuan.ReadOnly = false;
                                switch (ddlMinta.SelectedIndex)
                                {
                                    case 0:
                                        txtTglKirim.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                                        break;
                                    case 1:

                                        txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(7).ToString("dd-MMM-yyyy");

                                        break;
                                    case 2:
                                        txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(14).ToString("dd-MMM-yyyy");
                                        break;
                                }
                            }
                        }

                    }
                }
                getInfoBarang(int.Parse(ddlNamaBarang.SelectedValue), ddlTipeBarang.SelectedIndex);
                lbAddItem.Enabled = true;
                #region Cek Dead Stock 09-12-2021
                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    PaneLDeadstock.Visible = false;
                    InventoryFacade caribrgF = new InventoryFacade();
                    Inventory caribrg = new Inventory();
                    caribrg = caribrgF.RetrieveById(Convert.ToInt32(ddlNamaBarang.SelectedValue));
                    //loadDeadStockLocal(caribrg.ItemCode, DateTime.Parse(txtTglInput.Text).ToString("yyyyMM"));
                    //loadDeadStock1(caribrg.ItemCode, DateTime.Parse(txtTglInput.Text).ToString("yyyyMM"));
                    //loadDeadStock2(caribrg.ItemCode, DateTime.Parse(txtTglInput.Text).ToString("yyyyMM"));
                }
                #endregion
            }
        }

        private void GetDataItemBiaya(int ItemID)
        {
            BiayaFacade biayaFacade = new BiayaFacade();
            Biaya biaya = biayaFacade.RetrieveById(ItemID);
            if (biayaFacade.Error == string.Empty)
            {
                if (biaya.ID > 0)
                {
                    txtStok.Text = biaya.Jumlah.ToString("N2");
                    txtJmlMax.Text = biaya.MaxStock.ToString("N2");
                    txtSatuan.Text = biaya.UomCode;
                    txtSatuanID.Text = biaya.UOMID.ToString();
                    //txtKodeBarang.Text = biaya.ItemCode;
                    txtQty.Text = string.Empty;
                    txtQty.Focus();
                    switch (ddlMinta.SelectedIndex)
                    {
                        case 0:
                            txtTglKirim.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                            break;
                        case 1:

                            txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(7).ToString("dd-MMM-yyyy");

                            break;
                        case 2:
                            txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(14).ToString("dd-MMM-yyyy");
                            break;
                    }
                }
            }
        }
        protected int checkNonStock(string itemname, int itemtypeid)
        {
            int Stock = 0;
            Users users = (Users)Session["Users"];
            if (rbOn.Checked == true)
            {
                if (users.UnitKerjaID == 7)
                {
                    Panel2.Visible = true;
                    WebReference_Ctrp.Service1 webServiceCTP = new WebReference_Ctrp.Service1();
                    DataSet dsListReceipt = webServiceCTP.GetItemByName(itemname, itemtypeid);
                    GridView2.DataSource = dsListReceipt;
                    GridView2.DataBind();
                    LblStock.Visible = true;
                    LblStock.Text = "Stock Barang Citeureup";
                }
                else
                {
                    Panel2.Visible = true;
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    DataSet dsListReceipt = webServiceKRW.GetItemByName(itemname, itemtypeid);
                    GridView2.DataSource = dsListReceipt;
                    GridView2.DataBind();
                    LblStock.Visible = true;
                    LblStock.Text = "Stock Barang Karawang";
                }
            }
            return Stock;
        }
        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            POPurchnFacade poTools = new POPurchnFacade();
            POPurchn autoSave = poTools.PurchnTools("BiayaAutoSave");
            Users users = (Users)Session["Users"];
            try
            {
                int jmlItem = 0;
                Panel2.Visible = false;
                #region Validasi jumlah baris max 9 baris
                if (ViewState["Baris"] != null)
                {
                    jmlItem = (int)ViewState["Baris"];
                    jmlItem = jmlItem + 1;
                }
                else
                {
                    jmlItem = 1;
                }

                if (jmlItem > 9)
                {
                    DisplayAJAXMessage(this, "SPP Sudah melebihi 9 Item..!");
                    return;
                }
                #endregion
                #region Pilihan multigudang di check
                if (ddlMultiGudang.SelectedIndex == 0)
                {
                    DisplayAJAXMessage(this, "Tipe Multi Gudang belum ditentukan");
                    return;
                }
                #endregion
                #region Validasi Inputan field
                string strValidate = ValidateText();
                if (strValidate != string.Empty)
                {
                    DisplayAJAXMessage(this, strValidate);
                    return;
                }
                
                if (ddlGroupSarmut.SelectedItem.Text.Trim() != string.Empty)
                {
                    if (ddlGroupSarmut.SelectedItem.Text.Trim().ToUpper().Substring(0, 4) == "FORK")
                    {
                        //if (ddlForklif.SelectedIndex < 1)
                        if (ddlForklif.SelectedItem.ToString() == string.Empty)
                        {
                            DisplayAJAXMessage(this, "Forklift harus diisi...");
                            return;
                        }
                    }
                }
                #endregion
                //
                if (PaneLDeadstock.Visible == true)
                {
                    DisplayAJAXMessage(this, "Tidak bisa SPP item barang dead stock");
                    return;
                }
                if (StokNonStok.Text.ToUpper().Trim() == "STOCK")
                {
                    if (users.DeptID != 10 && users.DeptID != 19 && users.DeptID != 6)
                    {
                        DisplayAJAXMessage(this, "Keterangan stock hanya bisa di spp oleh departemen Logistik atau Mtc. Engineering");
                        return;
                    }
                }
                if (int.Parse(ddlTipeBarang.SelectedValue) == 3)
                {
                    if (txtKeterangan.Text == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Untuk SPP type Biaya keterangan harus di isi dengan uraian penggunaan biaya");
                        return;
                    }

                    if (txtKetBiaya.Text == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Keterangan Biaya harus di isi , \\ne.q : Untuk forklift 1");
                        return;
                    }

                    if (users.DeptID == 26 && ddlNoPol.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, "Nomor Polisi Belum DiPilih !");
                        return;
                    }
                }
                #region Validasi Autosave keterangan untuk sppbiaya

                if (BiayaID.Text == string.Empty && autoSave.Status == 0 && ddlTipeBarang.SelectedValue == "3")
                {
                    DisplayAJAXMessage(this, "Keterangan Biaya belum terdaftar di sistem");
                    return;
                }
               
                else if (int.Parse(ddlTipeBarang.SelectedValue) == 3 &&
                    BiayaID.Text == string.Empty)
                {
                    //jika nama biaya blm ada di tabel biaya
                    //buat otomatis ke table biaya


                    Biaya biaya = new Biaya();
                    BiayaFacade biayaFacade = new BiayaFacade();
                    string ItemKode = HitungNoAkhir("SP-JW-0");
                    biaya.ItemCode = ItemKode;
                    biaya.ItemName = txtKeterangan.Text.ToUpper();
                    biaya.Jumlah = 0;
                    biaya.Harga = 0;
                    //biaya.Gudang = int.Parse(ddlGudang.SelectedItem.Text);
                    biaya.ShortKey = "A";
                    biaya.SupplierCode = "-";
                    biaya.DeptID = ((Users)Session["Users"]).DeptID;
                    biaya.UOMID = 44;
                    biaya.MinStock = 0;
                    biaya.RakID = 1;
                    biaya.GroupID = 5;
                    biaya.ItemTypeID = 3;
                    biaya.Keterangan = txtKodeBarang.Text;
                    biaya.CreatedBy = ((Users)Session["Users"]).UserName;
                    biaya.LastModifiedBy = ((Users)Session["Users"]).UserName;

                    if (autoSave.Status == 1)
                    {
                        //jika field autosave di table purch_tools=1
                        //otomatis save aktif

                        int intResult = 0;
                        intResult = biayaFacade.Insert(biaya);

                        if (biayaFacade.Error != string.Empty && intResult <= 0)
                        {
                            DisplayAJAXMessage(this, "Ada Kesalahan proses. Silahkan coba lagi");
                            return;
                        }
                    }
                }
                #endregion
                // ddlItemName.Enabled = true;

                ArrayList arrSPPDetail = new ArrayList();

                if (Session["ListOfSPPDetail"] != null)
                {
                    arrSPPDetail = (ArrayList)Session["ListOfSPPDetail"];
                }
                #region check double item in spp
                //POPurchnDetail po = new POPurchnDetail();
                ArrayList arrListItem = new ArrayList();
                if (Session["ListOfSPPDetail"] != null)
                {
                    arrListItem = (ArrayList)Session["ListOfSPPDetail"];
                    if (arrListItem.Count > 0)
                    {
                        foreach (SPPDetail po in arrListItem)
                        {
                            if (CheckStatusBiaya() == 1 && ddlTipeBarang.SelectedIndex == 3)
                            {
                                if (po.Keterangan == txtKeterangan.Text)
                                {
                                    DisplayAJAXMessage(this, "Item Name " + txtKeterangan.Text + " sudah ada di List");
                                    return;
                                }
                            }
                            else
                            {
                                if (po.ItemID == int.Parse(ddlNamaBarang.SelectedValue))
                                {
                                    DisplayAJAXMessage(this, "Item Name " + ddlNamaBarang.SelectedItem.ToString() + " sudah ada di List");
                                    return;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (lbAddItem.Text == "AddItem")
                {
                    SPPDetail sPPDetail = new SPPDetail();
                    InventoryFacade inventoryFacade = new InventoryFacade();

                    int intItemsID = 0;
                    int intUOMID = 0;
                    int leadtime = 0;
                    decimal intJumlah = 0;
                    decimal intMaxStock = 0;
                    string strItemCode = string.Empty;
                    string strItemname = string.Empty;
                    string strSatuan = string.Empty;
                    #region for Inventory
                    if (ddlTipeBarang.SelectedIndex == 1)
                    {
                        Inventory inv = inventoryFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                            intItemsID = inv.ID;
                        intUOMID = inv.UOMID;
                        strItemCode = inv.ItemCode;
                        strItemname = inv.ItemName;
                        intJumlah = inv.Jumlah;
                        intMaxStock = inv.MaxStock;
                        leadtime = inv.LeadTime;

                        //Cek utk brg non stock ???????
                        //if (strItemCode.Substring(6, 1) == "9")
                        //if(StokNonStok.Text=="Stock")

                        int enggineeringDept = users.DeptID;
                        if (inv.Stock > 0 && enggineeringDept != 19)
                        {
                            if (CheckLastReceipt(inv.ID) != string.Empty)
                            {
                                DisplayAJAXMessage(this, CheckLastReceipt(inv.ID));
                                return;
                            }
                        }
                    }
                    #endregion
                    #region for Asset
                    if (ddlTipeBarang.SelectedIndex == 2)
                    {
                        Inventory ase = inventoryFacade.AssetRetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                        if (inventoryFacade.Error == string.Empty && ase.ID > 0)
                            intItemsID = ase.ID;
                        intUOMID = ase.UOMID;
                        strItemCode = ase.ItemCode;
                        strItemname = ase.ItemName;
                        intJumlah = ase.Jumlah;
                    }
                    #endregion
                    #region for Biaya
                    if (ddlTipeBarang.SelectedIndex == 3)
                    {
                        Inventory bia = inventoryFacade.BiayaRetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
                        if (inventoryFacade.Error == string.Empty && bia.ID > 0)
                            intItemsID = bia.ID;
                        intUOMID = int.Parse(txtSatuanID.Text);
                        strItemCode = bia.ItemCode;
                        strItemname = bia.ItemName;
                        intJumlah = bia.Jumlah;

                        // Nomor Polisi
                        if (users.DeptID == 26)
                        {
                            string npol = (ddlNoPol.SelectedValue.ToString() == "0") ? string.Empty : ddlNoPol.SelectedItem.ToString();
                            sPPDetail.NoPol = (ddlNoPol.SelectedValue == "0") ? string.Empty : npol.Substring(0, npol.IndexOf(" -"));
                        }
                        else
                        {
                            sPPDetail.NoPol = "0";
                        }
                        if (ddlForklif.Visible == true && ddlForklif.SelectedIndex > 0)
                            sPPDetail.NoPol = ddlForklif.SelectedValue;
                    }
                    #endregion
                    #region For Control pengajuan SPP sparepart (MaxStock)
                    //if (isSparePart() == true)
                    //{

                    //}
                    #endregion
                    UOMFacade uOMFacade = new UOMFacade();
                    UOM uOM = uOMFacade.RetrieveByID(intUOMID);
                    strSatuan = uOM.UOMCode;

                    int intGroupsID = 0;
                    intGroupsID = ddlTipeSPP.SelectedIndex;

                    sPPDetail.ItemID = intItemsID;
                    sPPDetail.GroupID = Convert.ToInt32(ddlTipeSPP.SelectedValue.ToString());
                    sPPDetail.Quantity = Convert.ToDecimal(txtQty.Text);
                    sPPDetail.ItemTypeID = ddlTipeBarang.SelectedIndex;
                    sPPDetail.UOMID = intUOMID;
                    sPPDetail.ItemCode = strItemCode;
                    sPPDetail.ItemName = strItemname;
                    sPPDetail.Satuan = strSatuan;
                    sPPDetail.Status = 0;
                    sPPDetail.QtyPO = 0;
                    sPPDetail.Keterangan = txtKeterangan.Text;
                    sPPDetail.TglKirim = DateTime.Parse(txtTglKirim.Text);
                    sPPDetail.TypeBiaya = ddlTypeBiaya.SelectedValue.ToString().ToUpper();
                    sPPDetail.Keterangan1 = txtKetBiaya.Text.ToUpper();
                    //iko

                    //if (GroupID.SelectedIndex > 0)
                    //    sPPDetail.AmGroupID = int.Parse(GroupID.SelectedValue);
                    //if (ClassID.SelectedIndex > 0)
                    //    sPPDetail.AmClassID = int.Parse(ClassID.SelectedValue);
                    //if (SbClassID.SelectedIndex > 0)
                    //    sPPDetail.AmSubClassID = int.Parse(SbClassID.SelectedValue);
                    //if (LokasiID.SelectedIndex > 0)
                    //    sPPDetail.AmLokasiID = int.Parse(LokasiID.SelectedValue);
                    //if (ddlGroupSarmut.SelectedIndex>0)
                    //    sPPDetail.MTCGroupSarmutID = int.Parse(ddlGroupSarmut.SelectedValue);
                    //if (ddlGroupEfesien.SelectedIndex > 0)   
                    //    sPPDetail.MaterialMTCGroupID = int.Parse(ddlGroupEfesien.SelectedValue);
                    //if (ddlUmurEko.SelectedIndex > 0)
                    //    sPPDetail.UmurEkonomis = int.Parse(ddlUmurEko.SelectedItem.ToString());
                    //iko

                    // 30 Juni 2019
                    if (users.DeptID == 22 || users.DeptID == 30)
                    {
                        //int GroupAsset = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(9, 1));

                        string query = string.Empty; string query2 = string.Empty;
                        if (users.UnitKerjaID.ToString().Length == 1)
                        {
                            query = "9"; query2 = "12";
                        }
                        else
                        {
                            query = "10"; query2 = "13";
                        }

                        if (ddlTipeSPP.SelectedValue == "4")
                        {
                            string KodeClass = txtKodeBarang.Text.Trim().Substring(5, 2); Session["KodeC"] = KodeClass;
                            int KodeBarang = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(2, 1)); Session["KodeB"] = KodeBarang;
                            int GroupID = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(2, 1)); Session["Group"] = KodeBarang;
                        }
                        else if (ddlTipeSPP.SelectedValue == "12")
                        {
                            //string KodeClass = txtKodeBarang.Text.Trim().Substring(12, 2); Session["KodeC"] = KodeClass;
                            //int KodeBarang = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(9, 1)); Session["KodeB"] = KodeBarang;
                            //int GroupID = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(9, 1)); Session["Group"] = KodeBarang;
                            string KodeClass = txtKodeBarang.Text.Trim().Substring(Convert.ToInt32(query2), 2); Session["KodeC"] = KodeClass;
                            int KodeBarang = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(Convert.ToInt32(query), 1)); Session["KodeB"] = KodeBarang;
                            int GroupID = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(Convert.ToInt32(query), 1)); Session["Group"] = KodeBarang;
                        }
                        //string KodeClass = txtKodeBarang.Text.Trim().Substring(12, 2);
                        if (ddlTipeBarang.SelectedIndex == 2)
                        {
                            string NamaSubClass = ddlNamaAsset.SelectedItem.ToString().Trim();

                            DomainAsset Dasset = new DomainAsset();
                            FacadeAsset Fasset = new FacadeAsset();
                            int IDclass = Fasset.RetrieveClassID(Session["KodeC"].ToString().Trim(), Convert.ToInt32(Session["KodeB"]));

                            DomainAsset DSubClassID = new DomainAsset();
                            FacadeAsset FSubClassID = new FacadeAsset();
                            int SubClassID = FSubClassID.RetrieveSubClassID(NamaSubClass, Convert.ToInt32(Session["KodeB"]));

                            //int GroupAsset = Convert.ToInt32(txtKodeBarang.Text.Trim().Substring(10, 1));

                            //sPPDetail.AmGroupID = Convert.ToInt32(ddlNamaAsset.SelectedItem.ToString().Substring(2, 1));
                            sPPDetail.AmGroupID = Convert.ToInt32(Session["Group"]);
                            //sPPDetail.AmClassID = Convert.ToInt32(ddlNamaAsset.SelectedItem.ToString().Substring(4, 3));
                            sPPDetail.AmClassID = IDclass;
                            //sPPDetail.AmSubClassID = 0;
                            sPPDetail.AmSubClassID = SubClassID;
                            sPPDetail.AmLokasiID = 0;
                            sPPDetail.MTCGroupSarmutID = 0;
                            sPPDetail.MaterialMTCGroupID = 0;
                            sPPDetail.UmurEkonomis = 0;
                        }
                    }
                    else
                    {
                        //FillItems("asset", " where RowStatus >-1 and itemcode='" + txtKodeBarang.Text.Trim() + "' Order By ID", AMLokasiID);
                        //SelectLokasiAsset(asset.AmSubClassID.ToString()); 

                        if (GroupID.SelectedIndex > 0)
                            sPPDetail.AmGroupID = int.Parse(GroupID.SelectedValue);
                        if (ClassID.SelectedIndex > 0)
                            sPPDetail.AmClassID = int.Parse(ClassID.SelectedValue);
                        if (SbClassID.SelectedIndex > 0)
                            sPPDetail.AmSubClassID = int.Parse(SbClassID.SelectedValue);
                        if (LokasiID.SelectedIndex > 0)
                            sPPDetail.AmLokasiID = int.Parse(LokasiID.SelectedValue);
                        if (ddlGroupSarmut.SelectedIndex > 0)
                            sPPDetail.MTCGroupSarmutID = int.Parse(ddlGroupSarmut.SelectedValue);
                        if (ddlGroupEfesien.SelectedIndex > 0)
                            sPPDetail.MaterialMTCGroupID = int.Parse(ddlGroupEfesien.SelectedValue);
                        if (ddlUmurEko.SelectedIndex > 0)
                            sPPDetail.UmurEkonomis = int.Parse(ddlUmurEko.SelectedItem.ToString());
                    }
                    // end


                    arrSPPDetail.Add(sPPDetail);
                }
                else
                {
                    #region
                    lbAddItem.Text = "AddItem";
                    //foreach (SPPDetail sPPDetail in arrSPPDetail)
                    //{
                    //    if (sPPDetail.ItemID == sPPDetail.ItemID)
                    //    {
                    //        pOPurchnDetail.Qty = int.Parse(txtQty.Text);
                    //        break;
                    //    }
                    //}
                    #endregion
                }
                ViewState["Baris"] = jmlItem;
                Session["ListOfSPPDetail"] = arrSPPDetail;
                GridView1.DataSource = arrSPPDetail;
                GridView1.DataBind();
                GridView3.DataSource = arrSPPDetail;
                GridView3.DataBind();
                ddlTipeSPP.Enabled = false;
                ddlMinta.Enabled = false;
                lstSPP.DataSource = arrSPPDetail;
                lstSPP.DataBind();
                PaneLDeadstock.Visible = false;
                clearForm2();
            }
            catch { }
        }
        private void clearForm2()
        {
            txtKeterangan.Text = string.Empty;
            BiayaID.Text = string.Empty;
            txtQty.Text = "0";
        }
        private void LoadGroupPM()
        {
            ArrayList arrPM = new ArrayList();
            MTC_ZonaFacade PM = new MTC_ZonaFacade();
            arrPM = PM.RetrieveSpGroup();
            ddlGroupSarmut.Items.Add(new ListItem(" ", "0"));
            foreach (MTC_Zona uPm in arrPM)
            {
                ddlGroupSarmut.Items.Add(new ListItem(uPm.ZonaName, uPm.ID.ToString()));
            }
        }
        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];

            if (ddl.SelectedIndex > 0)
            {
                GridViewRow row = (GridViewRow)ddl.NamingContainer;

                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    InventoryFacade inventoryFacade = new InventoryFacade();
                    Inventory inventory = inventoryFacade.RetrieveById(int.Parse(ddl.SelectedValue));
                    if (inventoryFacade.Error == string.Empty)
                    {
                        if (inventory.ID > 0)
                        {
                            //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                            //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                            //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

                            txtStok.Text = inventory.Jumlah.ToString("N2");
                            txtJmlMax.Text = inventory.MaxStock.ToString("N2");
                            txtSatuan.Text = inventory.UomCode;
                            txtKodeBarang.Text = inventory.ItemCode;
                            txtQty.Text = string.Empty;
                            txtQty.Focus();
                        }
                    }
                }
                else
                {
                    if (ddlTipeBarang.SelectedIndex == 2)
                    {
                        AssetFacade assetFacade = new AssetFacade();
                        Asset asset = assetFacade.RetrieveById(int.Parse(ddl.SelectedValue));
                        if (assetFacade.Error == string.Empty)
                        {
                            if (asset.ID > 0)
                            {
                                //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                                //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

                                txtStok.Text = asset.Jumlah.ToString("N2");
                                txtJmlMax.Text = asset.MaxStock.ToString("N2");
                                txtSatuan.Text = asset.UomCode;
                                txtKodeBarang.Text = asset.ItemCode;
                                txtQty.Text = string.Empty;
                                txtQty.Focus();
                            }
                        }
                    }
                    else
                    {
                        BiayaFacade biayaFacade = new BiayaFacade();
                        Biaya biaya = biayaFacade.RetrieveById(int.Parse(ddl.SelectedValue));
                        if (biayaFacade.Error == string.Empty)
                        {
                            if (biaya.ID > 0)
                            {
                                //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
                                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
                                //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

                                txtStok.Text = biaya.Jumlah.ToString("N2");
                                txtJmlMax.Text = biaya.MaxStock.ToString("N2");
                                txtSatuan.Text = biaya.UomCode;
                                txtKodeBarang.Text = biaya.ItemCode;
                                txtQty.Text = string.Empty;
                                txtQty.Focus();
                            }
                        }
                    }
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Users usr = (Users)Session["Users"];//user login data
            int apvSPP = (Session["SPPHeader"] != null) ? ((SPP)Session["SPPHeader"]).Approval : 0;//status aproval
            int stsSPP = (Session["SPPHeader"] != null) ? ((SPP)Session["SPPHeader"]).Status : 0;//status po
            if (txtSPP.Text != string.Empty)
            {
                /* jika user admin dan apv =0 dan status=0 / blm di buat po*/
                if (apvSPP == 0 && stsSPP == 0)
                {
                    GridView1.Columns[5].Visible = true;
                    GridView3.Columns[5].Visible = true;
                }
                else if (usr.Apv == 1 && apvSPP == 1 && stsSPP == 0)
                {
                    GridView1.Columns[5].Visible = true;
                    GridView3.Columns[5].Visible = true;
                }
                else if (apvSPP <= usr.Apv && stsSPP == 0)
                {
                    GridView1.Columns[5].Visible = true;
                    GridView3.Columns[5].Visible = true;
                }
                else
                {
                    GridView1.Columns[5].Visible = false;
                    GridView3.Columns[5].Visible = false;
                }

            }
            else
            {
                GridView1.Columns[5].Visible = true;
                GridView3.Columns[5].Visible = true;
            }
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //// Bind drop down to cities

                //DropDownList ddlItemCode = (DropDownList)e.Row.FindControl("ddlItemCode");
                //LoadddlItems(ddlItemCode);
            }
        }
        private void SelectItem(DropDownList ddl, string strItemID)
        {
            ddl.ClearSelection();
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == strItemID)
                {
                    item.Selected = true;
                    //item.Text = (Session["nabarselect"].ToString());
                    return;
                }

            }
        }
        private void SelectTipeSPP(string strTipename)
        {

            ddlTipeSPP.ClearSelection();
            foreach (ListItem item in ddlTipeSPP.Items)
            {
                if (item.Text == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        private void SelectTipeBarang(string strTipename)
        {
            ddlTipeBarang.ClearSelection();
            foreach (ListItem item in ddlTipeSPP.Items)
            {
                if (item.Text == strTipename)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
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
        private void clearForm()
        {
            ViewState["id"] = null;
            Session["NoSPP"] = null;
            Session.Remove("id");
            Session["ListOfSPPDetail"] = null;
            txtTglInput.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtSPP.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtKodeBarang.Text = string.Empty;
            btnUpdate.Disabled = false;
            txtQty.Text = "0";
            BiayaID.Text = string.Empty;
            //txtLeadTime.Text = string.Empty;
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            if (ddlTipeSPP.SelectedIndex > 0) ddlTipeSPP.SelectedIndex = 0;
            if (ddlTipeBarang.SelectedIndex > 0) ddlTipeBarang.SelectedIndex = 0;
            ddlTipeBarang.Enabled = true;
            ddlTipeSPP.Enabled = true;
            ddlMinta.Enabled = true;
            ddlNamaBarang.Items.Clear();
            ArrayList arrList = new ArrayList();
            //arrList.Add(new SPPDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            GridView3.DataSource = arrList;
            GridView3.DataBind();
            lstSPP.DataSource = arrList;
            lstSPP.DataBind();
        }
        private void LoadSPP(string strNOSPP)
        {
            Session["ListOfSPPDetail"] = null;

            Users users = (Users)Session["Users"];
            SPPFacade sPPFacade = new SPPFacade();
            SPP sPP = new SPP();
            sPP = sPPFacade.RetrieveByNo(strNOSPP);

            if (sPPFacade.Error == string.Empty && sPP.ID > 0)
            {
                Session["id"] = sPP.ID;
                txtSPP.Text = sPP.NoSPP;
                txtTglInput.Text = sPP.Tanggal.ToString("dd-MMM-yyyy");
                ddlTipeSPP.SelectedValue = sPP.GroupID.ToString();
                ddlTipeBarang.SelectedIndex = sPP.ItemTypeID;
                txtCreatedBy.Text = sPP.CreatedBy;
                ddlMinta.SelectedIndex = sPP.PermintaanType-1;

                UsersFacade usersFacade = new UsersFacade();
                Users user = new Users();
                user = usersFacade.RetrieveByUserName(sPP.CreatedBy);
                int DepartemenID = user.DeptID;

                Users usr = usersFacade.RetrieveById(sPP.HeadID);
                txtNamaHead.Text = usr.UserName;

                if (sPP.Status == -2) txtStatus.Text = "Close";
                if (sPP.Status == -1) txtStatus.Text = "Batal";
                if (sPP.Status == 0) txtStatus.Text = "Open";
                if (sPP.Status == 1) txtStatus.Text = "Parsial";
                if (sPP.Status == 2) txtStatus.Text = "Full PO";

                if (sPP.Approval == 0) { txtApproval.Text = "Admin"; }
                if (sPP.Approval == 1) { txtApproval.Text = "Head"; }
                if (sPP.Approval == 2) { txtApproval.Text = "Dept.Mgr"; }
                if (sPP.Approval == 3) { txtApproval.Text = "Plant.Mgr"; }

                Session["SPPHeader"] = sPP;

                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                ArrayList arrSPPDetail = sPPDetailFacade.RetrieveById(sPP.ID);
                if (sPPDetailFacade.Error == string.Empty)
                {
                    Session["NoSPP"] = sPP.NoSPP;
                    Session["ListOfSPPDetail"] = arrSPPDetail;
                    if (arrSPPDetail.Count <= 0) arrSPPDetail = new ArrayList();
                    GridView1.DataSource = arrSPPDetail;
                    GridView1.DataBind();
                    lstSPP.DataSource = arrSPPDetail;
                    lstSPP.DataBind();

                    if (sPP.Status <= -1) btnCancel.Enabled = false;

                    if (sPP.Status < 0 && users.Apv < 3)
                    {
                        btnUpdate.Disabled = true;
                        btnCancel.Enabled = false;
                        btnClose.Enabled = false;
                        //btnCetak.Disabled = false;
                    }
                    else if (sPP.Status == 0 && sPP.Approval <= users.Apv && users.Apv < 2)
                    {
                        btnUpdate.Disabled = false;
                        btnCancel.Enabled = true;
                        btnClose.Enabled = true;
                        // btnCetak.Disabled = true;
                    }
                    else if (sPP.Status == 0 && users.Apv > 1)
                    {
                        btnUpdate.Disabled = true;
                        btnCancel.Enabled = true;
                        btnClose.Enabled = true;
                        // btnCetak.Disabled = false;
                    }
                    else if (sPP.Status == 0 && sPP.Approval == 0)
                    {
                        btnUpdate.Disabled = false;
                        btnCancel.Enabled = true;
                        btnClose.Enabled = true;
                        // btnCetak.Disabled = true;
                    }
                    else
                    {
                        btnUpdate.Disabled = true;
                        btnCancel.Enabled = false;
                        btnClose.Enabled = false;
                        // btnCetak.Disabled = true;
                    }
                    //field disable
                    string LockField = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnEditableField", "SPP");
                    DisableField(false, LockField);
                }
            }
        }
        private string ValidateText()
        {
            if (ddlTipeSPP.SelectedIndex == 0)
                return "Tipe SPP tidak boleh kosong";
            if (ddlTipeBarang.SelectedIndex == 0)
                return "Tipe Barang tidak boleh kosong";

            try
            {
                decimal dec = decimal.Parse(txtQty.Text);
                if (dec < 1)
                    return "Jumlah tidak boleh kosong";
            }
            catch
            {
                return "Jumlah harus numeric";
            }

            //iko
            if (ddlTipeBarang.SelectedIndex == 2)
            {
                //if (GroupID.SelectedIndex == 0)
                //    return "Group Asset tidak boleh kosong";
                //else if (ClassID.SelectedIndex == 0)
                //    return "Class Asset tidak boleh kosong";
                //else if (SbClassID.SelectedIndex == 0)
                //    return "Sub-Class Asset tidak boleh kosong";
                //else if (LokasiID.SelectedIndex == 0)
                //    return "Lokasi Asset tidak boleh kosong";
            }
            //iko
            if (ddlTipeBarang.SelectedIndex == 3)
            {
                Users users = (Users)Session["Users"];
                SPPFacade sPPFacade = new SPPFacade();
                int usermtc = sPPFacade.GetUserMTC(users.UserName.Trim());
                if (usermtc > 0)
                {
                    if (ddlGroupEfesien.SelectedIndex == 0)
                        return "Group efesien mesin tidak boleh kosong";
                    if (ddlGroupSarmut.SelectedIndex == 0)
                        return "Group sarmut tidak boleh kosong";
                }
            }
            return string.Empty;
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            //Company company = new Company();
            //CompanyFacade companyFacade = new CompanyFacade();
            //string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
            Session["ListOfSPPDetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("ListSPP.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }
        protected void ddlMinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMinta.SelectedIndex == 2)
            {

                txtTglKirim.Enabled = true;
            }
            else if (ddlMinta.SelectedIndex == 1)
            {
                txtTglKirim.Enabled = false;
                txtTglKirim.Text = DateTime.Parse(txtTglInput.Text).AddDays(int.Parse(txtLeadTime.Text)).ToString("dd-MMM-yyyy");
            }
            else
            {
                txtTglKirim.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }

        }
        protected void txtCari_TextChanged2(object sender, EventArgs e)
        {
            if (txtCari.Text != string.Empty)
            {
                if (ddlTipeBarang.SelectedIndex == 0)
                {
                    DisplayAJAXMessage(this, "Tipe Barang tidak boleh kosong");
                    txtCari.Text = string.Empty;
                    return;
                }
                if (ddlTipeSPP.SelectedIndex == 0)
                {
                    DisplayAJAXMessage(this, "Tipe SPP tidak boleh kosong");
                    txtCari.Text = string.Empty;
                    return;
                }

                //GridViewRow row = (GridViewRow)txl.NamingContainer;

                //TextBox txtCari = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtCariBarang");
                //DropDownList ddlName = (DropDownList)GridView1.Rows[row.RowIndex].FindControl("ddlItemCode");

                LoadItem(txtCari.Text.Trim());

            }
        }
        private int CheckStatusBiaya()
        {
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing stat = cls.CheckBiayaAktif();
            return stat.Status;
        }
        private int CheckLeadTime(int ItemID)
        {
            Inventory objInv = new Inventory();
            InventoryFacade Inv = new InventoryFacade();
            objInv = Inv.RetrieveById(ItemID);
            return (Inv.Error == string.Empty) ? objInv.LeadTime : 0;
        }
        protected void txtKeterangan_TextChanged(object sender, EventArgs e)
        {
            if (CheckStatusBiaya() == 1 && ddlTipeBarang.SelectedIndex == 3)
            {
                CheckLastReceipt(int.Parse(ddlNamaBarang.SelectedValue));
                Biaya objBiaya = new Biaya();
                BiayaFacade onBiaya = new BiayaFacade();

                objBiaya = onBiaya.RetrieveByName(txtKeterangan.Text.TrimEnd());
                if (onBiaya.Error == string.Empty && objBiaya.ID > 0)
                {
                    BiayaID.Text = objBiaya.ID.ToString();
                    GetDataItemBiaya(objBiaya.ID);
                    txtQty.Focus();
                }
                else
                {
                    BiayaID.Text = string.Empty;
                    txtQty.Focus();
                    txtQty.ReadOnly = false;
                    return;
                }
            }
        }
        protected void txtSatuan_OnTextChange(object sender, EventArgs e)
        {
            txtSatuanID.Text = new BiayaFacade().GetSatuanID(txtSatuan.Text).ToString();
        }
        private string HitungNoAkhir(string strPrefix)
        {
            string NoAkhir = string.Empty;
            if (strPrefix.Length == 7)
            {

                BiayaFacade biayaFacade = new BiayaFacade();
                int noUrut = biayaFacade.CountItemCode(strPrefix);
                noUrut = noUrut + 1;
                //            supplier.SupplierCode = "C" + noUrut.ToString().PadLeft(4, '0');
                NoAkhir = strPrefix + noUrut.ToString().PadLeft(4, '0');

                return NoAkhir;
            }
            return NoAkhir;
        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "AddDelete")
            {
                if (Session["ListOfSPPDetail"] != null)
                {
                    SPPDetail sPPDetail = (SPPDetail)((ArrayList)Session["ListOfSPPDetail"])[index];

                    ArrayList arrTransferDetail = new ArrayList();
                    arrTransferDetail = (ArrayList)Session["ListOfSPPDetail"];

                    arrTransferDetail.RemoveAt(index);
                    GridView3.DataSource = arrTransferDetail;
                    GridView3.DataBind();
                }
            }
        }
        public void getInfoBarang(int ItemID, int ItemTypeID)
        {
            SPPDetailFacade spp = new SPPDetailFacade();
            SPPDetail objSpp = spp.GetLastPORMS(ItemID, ItemTypeID);
            LastPO.Text = objSpp.Satuan;
            LastRMS.Text = objSpp.Keterangan;
        }
        public int GetHariLibur(string frDate, string tDate)
        {
            POPurchnFacade DayOff = new POPurchnFacade();
            POPurchn JmlHari = DayOff.DayOffCalender(frDate, tDate);

            return JmlHari.Status;
        }
        public int GetWeekEnd(string frDate, string tDate)
        {
            POPurchnFacade DayOff = new POPurchnFacade();
            POPurchn JmlHari = DayOff.DayOffWeekEnd(frDate, tDate);

            return JmlHari.Status;
        }
        public int PurchnConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn info = purchn.PurchnTools(ModulName);
            return info.Status;
        }
        public void ShowInfo(string Info)
        {
            Session.Remove("Judul");
            Session.Remove("UserID");
            Session.Remove("Modul");
            //Session["Dibaca"]=null;
            SPPFacade info = new SPPFacade();
            int userid = ((Users)Session["Users"]).ID;
            int status = info.ShowInfoStatus(userid, Info);


            if (status == 0)
            {
                Session["Judul"] = "Info Update SPP Biaya";
                Session["UserID"] = userid.ToString();
                Session["ModulName"] = Info;
                Session["FileName"] = "UpdateBiaya.txt";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "OpenWindows", "ShowInfos()", true);
            }

        }
        public void UpdateInfoDibaca(string Info)
        {
            int userid = ((Users)Session["Users"]).ID;
            if (Session["Dibaca"] != null)
            {
                if (Session["Dibaca"].ToString() == "ok")
                {
                    AccClosingFacade dibaca = new AccClosingFacade();
                    int result = dibaca.UpdateInfoStatus(userid, Info);
                    if (result > 0 && dibaca.Error == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Terima kasih");
                        Session["Dibaca"] = null;
                        return;
                    }
                }
            }
        }
        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            lbUpdItem.Visible = false;
            SPPDetail arrSPP = e.Item.DataItem as SPPDetail;
            SPP SPPh = (SPP)Session["SPPHeader"];
            Image edit = (Image)e.Item.FindControl("lstEdit");
            Image delt = (Image)e.Item.FindControl("lstDel");
            Image pend = (Image)e.Item.FindControl("lstPnd");
            if (Session["SPPHeader"] != null)
            {
                if ((arrSPP.Status == 2 || ((Users)Session["Users"]).Apv < SPPh.Approval) && arrSPP.PendingPO == 0)
                {
                    edit.Visible = false;
                    delt.Visible = false;
                    pend.Visible = false;
                    lbAddItem.Enabled = false;
                    lbUpdItem.Visible = false;
                }
                else if (arrSPP.PendingPO == 1)//status di tolak Purchn
                {
                    int UserApv = ((Users)Session["Users"]).Apv;
                    edit.Visible = (UserApv > 0) ? true : false;
                    delt.Visible = (UserApv > 0) ? true : false;
                    pend.Visible = (UserApv > 0) ? false : true;
                    lbAddItem.Enabled = false;
                    lbUpdItem.Visible = (UserApv > 0) ? false : false;
                    int idx = e.Item.ItemIndex;

                }
                else
                {
                    edit.Visible = true;
                    delt.Visible = true;
                    pend.Visible = false;
                    lbAddItem.Enabled = false;
                    lbUpdItem.Visible = false;
                }
            }
            else
            {
                edit.Visible = false;
                delt.Visible = true;
                pend.Visible = false;
                lbAddItem.Enabled = true;
                lbUpdItem.Visible = false;
            }
        }
        protected void lstSPP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string cmd = e.CommandName.ToString();
            int ID = int.Parse(e.CommandArgument.ToString());
            SPPDetail spp = new SPPDetail();
            SPP SPPh = (SPP)Session["SPPHeader"];
            switch (cmd)
            {
                case "edit":
                    #region edit prosess
                    spp = new SPPDetailFacade().RetrieveBySPPDetailID(ID);
                    ddlTipeBarang.SelectedValue = spp.ItemTypeID.ToString();
                    LoadTipeSPP();
                    ddlTipeSPP.SelectedValue = spp.GroupID.ToString();
                    txtCari.Text = spp.ItemName.ToString();
                    LoadItem(spp.ItemName.ToString());
                    ddlNamaBarang.SelectedValue = spp.ItemID.ToString();
                    txtKodeBarang.Text = spp.ItemCode.ToString();
                    txtSatuanID.Text = spp.UOMID.ToString();
                    sppDetailID.Text = spp.ID.ToString();
                    ddlNamaBarang_SelectedIndexChanged(ddlNamaBarang, System.EventArgs.Empty);
                    txtQty.Text = spp.Quantity.ToString();
                    txtTglKirim.Text = spp.TglKirim.ToString();
                    txtKeterangan.Text = spp.Keterangan;
                    lbAddItem.Enabled = false;
                    lbUpdItem.Visible = (SPPh.HeadID == ((Users)Session["Users"]).ID || ((Users)Session["Users"]).Apv > 1) ? true : false;
                    string LockField = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnEditableField", "SPP");
                    DisableField(false, LockField);
                    int LdTime = new SPPDetailFacade().GetLeadTime(spp.ItemID, spp.ItemTypeID);
                    ldTime.Text = LdTime.ToString() + " day(s)";
                    ddlTypeBiaya.SelectedValue = spp.TypeBiaya.ToString();
                    rowBiaya.Visible = (spp.ItemTypeID == 3) ? true : false;
                    txtKetBiaya.Text = spp.Keterangan1.ToString();
                    #endregion
                    break;
                case "del":
                    #region hapus detail
                    SPPDetail spDetail = new SPPDetail();
                    if (Session["AlasanBatal"] != null)
                    {
                        if (Session["AlasanBatal"].ToString().Length < 1)
                        {
                            DisplayAJAXMessage(this, "Alasan batal tidak boleh kosong");
                            return;
                        }
                        spDetail.CariItemName = Session["AlasanBatal"].ToString();

                        if (Session["SPPHeader"] == null)
                        {
                            if (Session["ListOfSPPDetail"] != null)
                            {
                                ArrayList arrLst = (ArrayList)Session["ListOfSPPDetail"];
                                int Idx = e.Item.ItemIndex;
                                arrLst.RemoveAt(Idx);
                                Session["ListOfSPPDetail"] = arrLst;
                                lstSPP.DataSource = arrLst;
                                lstSPP.DataBind();
                            }
                        }
                        else
                        {
                            ArrayList arrSPPDetail = new ArrayList();
                            Session["where"] = " and A.ID=" + ID;
                            SPPDetailFacade dSpp = new SPPDetailFacade();
                            arrSPPDetail = dSpp.Retrieve();
                            SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(new SPP(), arrSPPDetail, new SPPNumber());

                            string strError = sPPProsessFacade.CancelSPPDetail();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, strError);
                                return;
                            }
                            else
                            {
                                EventLog even = new EventLog();
                                even.EventName = "Delete";
                                even.ModulName = "Delete Detail SPP [ID:" + ID + "] " + Session["AlasanBatal"].ToString();
                                even.DocumentNo = txtSPP.Text;
                                even.CreatedBy = ((Users)Session["Users"]).UserName;
                                even.CreatedBy += "-" + Request.ServerVariables["REMOTE_ADDR"].ToString();
                                EventLogFacade ev = new EventLogFacade();
                                int evn = ev.Insert(even);
                            }
                            string Query = (txtSearch.Text == string.Empty) ? "" : "?NoSPP=" + txtSearch.Text;
                            Response.Redirect(HttpContext.Current.Request.Url.ToString() + Query);
                        }
                    }

                    break;
                    #endregion
            }
        }
        protected void lbUpdItem_Click(object sender, EventArgs e)
        {
            if (sppDetailID.Text != string.Empty)
            {
                SPPDetailFacade spd = new SPPDetailFacade();
                SPPDetail obSpp = new SPPDetail();
                obSpp.ID = int.Parse(sppDetailID.Text);
                obSpp.Quantity = decimal.Parse(txtQty.Text);
                obSpp.Keterangan = txtKeterangan.Text;
                obSpp.Keterangan1 = txtKetBiaya.Text.ToUpper();

                int result = spd.UpdateSPPDetail(obSpp);
                if (result > 0)
                {
                    EventLog ev = new EventLog();
                    ev.EventName = "Update SPP Detail ID=" + sppDetailID.Text + ", Qty=" + obSpp.Quantity.ToString() + ",Ket=" + obSpp.Keterangan;
                    ev.ModulName = "SPP";
                    ev.DocumentNo = txtSPP.Text;
                    ev.CreatedBy = ((Users)Session["Users"]).UserName + "-" + Request.ServerVariables["REMOTE_ADDR"].ToString();
                    int rst = new EventLogFacade().Insert(ev);
                    if (rst > 0)
                    {
                        DisplayAJAXMessage(this, "Update Berhasil di lakukan");
                        string Query = (txtSearch.Text == string.Empty) ? "" : "?NoSPP=" + txtSearch.Text;
                        Response.Redirect(HttpContext.Current.Request.Url.ToString() + Query);
                    }
                }
            }
            else
            {
                return;
            }
        }
        private string CheckLastReceipt(int ItemID)
        {

            /**
             * Skenario blok material untuk di spp :
             * 1. Jika material yang akan di spp adalah Stock 
             * 2. Jika material tersebut sudah di buat spp dan totalnya (qty SPP + stock per item) melebihi maxstock
             * 3. Jika Total Stok per item dan jml PO yang blm di receipt sudah melebihi MaxStock
             * 4. Jika Material tersebut statusnya adalah Pending PO karena ada proses pending dari Purchasing
             * 5. Jika Material tersebut adalah material yng dibudgetkan (18/02/2016)
             */
            string Message = string.Empty;
            Inventory inv = new InventoryFacade().RetrieveByIdNew(ItemID, int.Parse(ddlTipeBarang.SelectedValue));
            #region Jika dikarenakan proses stock (Point 1,2,3)
            if (inv.Stock > 0)
            {
                //jika Stock Material
                #region di nonaktifkan
                /*
                SPPDetail oSPP = new SPPDetail();
                Session["where"] = (ddlTipeBarang.SelectedValue == "3") ?
                    " and A.Keterangan='" + txtKeterangan.Text + "'" :
                    " and A.ItemID=" + ItemID + " and A.ItemTypeID=" + ddlTipeBarang.SelectedValue;
                ArrayList arrSPP = new SPPDetailFacade().Retrieve();
                int SPPID = 0; int POID = 0; int rcpID = 0; decimal QtySPP = 0; decimal qtyPO = 0;
                foreach (SPPDetail objSPP in arrSPP)
                {
                    int ID = objSPP.ID;
                    qtyPO = objSPP.QtyPO;
                    QtySPP = objSPP.Quantity;
                    SPPID = objSPP.SPPID;
                }
                SPP oSP = new SPPFacade().RetrieveById(SPPID);
                Session["POCriteria"] = "and A.SPPID=" + SPPID;
                ArrayList arrPO = new POPurchnDetailFacade().Retrieve();
                foreach (POPurchnDetail po in arrPO)
                {
                    POID = po.ID;
                }
                Session["ReceiptCriteria"] = " and PODetailID=" + POID;
                ArrayList rcp = new ReceiptDetailFacade().Retrieve();
                foreach (ReceiptDetail rcpd in rcp)
                {
                    //rcpID = rcpd.ID;
                    //qtyPO = rcpd.Quantity;
                }*/
                #endregion
                decimal TotalStock = 0;
                decimal MaxStock = inv.MaxStock;
                Session["where"] = string.Empty;
                Session["POCriteria"] = string.Empty;
                Session["ReceiptCriteria"] = string.Empty;
                ROPFacade rop = new ROPFacade();
                decimal sppBlmPo = rop.JumlahSPPBlmPO(inv.ID, inv.ItemTypeID);
                decimal poBlmReceipt = rop.JumlahPOblmReceipt(inv.ID, inv.ItemTypeID);
                decimal jmlSPB = (Global.IsNumeric(txtQty.Text)) ? decimal.Parse(txtQty.Text) : 0;
                TotalStock = sppBlmPo + poBlmReceipt + jmlSPB + inv.Jumlah;

                if (MaxStock < TotalStock)
                {
                    //jika sudah melebihi max stock
                    //POPurchn hPO = new POPurchnFacade().RetrieveByID(POID);
                    Message = "Permintaan Sudah melebihi MaxStock (" + MaxStock.ToString("###,##0.#0") + ")\\n";
                    Message += "Stock Saat ini : " + inv.Jumlah.ToString("###,##0.#0");
                    if (poBlmReceipt > 0)
                    {
                        Message += "\\nOut Standing PO : " + poBlmReceipt.ToString("###,##0.#0");
                    }
                    if (sppBlmPo > 0)
                    {
                        Message += "\\nOut Standing SPP : " + sppBlmPo.ToString("###,##0.00");
                    }
                    Message += "\\nQuantity SPP Baru : " + jmlSPB.ToString("###,##0.00");
                    Message += "\\nTotal   : " + TotalStock.ToString("###,##0.00");
                    Message += "\\n";
                }

            }
            #endregion
            #region jika ada material yang spp nya di pending pembuatan PO nya oleh Purchasing
            if (LockUserSPP() == true)
            {
                Message += (ddlTipeBarang.SelectedValue == "3") ? txtKeterangan.Text : ddlNamaBarang.SelectedItem.ToString();
                Message += "Ini Statusanya Pending PO tidak bisa di SPP lagi sementara\\n";
            }
            #endregion
            #region Jika Material tersebut adalah material yng dibudgetkan
            string BudgetBlock = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetBlock", "SPP");
            if ((inv.Head == 1 || inv.Head == 2) && BudgetBlock == "1")
            {
                Message += inv.ItemName + "\\n masuk di master budget. Tidak Bisa di SPP Manual\\n";
            }
            #endregion
            return Message;
            #region Jika SPP biaya sebelumnya sudah di receipt dan blm di SPB
            //Message += inv.ItemName + "\\n SPP Biaya sebelumnya masih ada yng blm di SPB\\n";
            #endregion
        }
        private bool LockUserSPP()
        {
            Session["where"] = (ddlTipeBarang.SelectedValue == "3") ?
                " and A.Keterangan='" + txtKeterangan.Text + "'" :
                " and ItemID=" + ddlNamaBarang.SelectedValue.ToString();
            ArrayList arrSPP = new SPPDetailFacade().RetrievePending(((Users)Session["Users"]).ID);
            return (arrSPP.Count > 0) ? true : false;
        }
        private void LoadListSPPfromBudget(ArrayList arr)
        {
            /**
             * Proses transfer budget ATK menajadi SPP
             * Added on 26/08/2015
             */
            ArrayList arrSPPDetail = new ArrayList();

            ddlTipeBarang.SelectedValue = "1";
            ddlTipeBarang.Enabled = false;
            ddlTipeBarang_SelectedIndexChanged(null, null);
            ddlTipeSPP.SelectedValue = "3";
            ddlMinta.SelectedIndex = 1;
            ddlTipeSPP.Enabled = false;
            ddlMinta.Enabled = false;
            foreach (Budget bg in arr)
            {
                SPPDetail sPPDetail = new SPPDetail();
                sPPDetail.ItemID = bg.ItemID;
                sPPDetail.GroupID = 3;
                sPPDetail.Quantity = bg.Quantity;
                sPPDetail.ItemTypeID = 1;
                sPPDetail.UOMID = bg.UomID;
                sPPDetail.ItemCode = bg.ItemCode;
                sPPDetail.ItemName = bg.ItemName;
                sPPDetail.Satuan = bg.UomCode;
                sPPDetail.Status = 0;
                sPPDetail.QtyPO = 0;
                sPPDetail.Keterangan = "";
                sPPDetail.TglKirim = DateTime.Parse(txtTglInput.Text).AddDays(this.CheckLeadTime(bg.ItemID));
                sPPDetail.TypeBiaya = "";
                sPPDetail.Keterangan1 = "";
                arrSPPDetail.Add(sPPDetail);
            }
            lstSPP.DataSource = arrSPPDetail;
            lstSPP.DataBind();
            Session["ListOfSPPDetail"] = arrSPPDetail;
        }

        private bool isSparePart()
        {
            bool result = false;

            return result;
        }
        protected void ddlGroupSarmut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlGroupSarmut_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlGroupSarmut.SelectedValue == "14")
            {
                LoadData("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
                ddlForklif.Visible = true;
                frk.InnerHtml = "Forklift Name";
            }
            else
            {
                ddlForklif.Visible = false;
                frk.InnerHtml = " ";
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
            //        ddlForklif.Items.Add(new ListItem(arrData[i].ToString(), arrData[i].ToString()));
            //    }
            //}
            //else
            //{
            //    ddlForklif.Items.Clear();
            //}
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from masterforklift where RowStatus=0";
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDeadStockLocal(string itemcode, string tgl2)
        {
            string strtanggal = string.Empty;
            string plant = string.Empty;
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID == 1)
            {
                plant = "'Citeureup'";
            }
            if (users.UnitKerjaID == 7)
            {
                plant = "'Karawang'";
            }
            if (users.UnitKerjaID == 13)
            {
                plant = "'Jombang'";
            }
            string strSQL = "exec getdatadeadstock1  '" + tgl2 + "',24,0,'" + itemcode + "'" +
                "select top 1 " + plant + " Plant,Itemcode,Stock from tempdeadstock where itemcode='" + itemcode + "'";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                PaneLDeadstock.Visible = true;
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic1.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                GrdDynamic1.Columns.Add(bfield);
            }
            GrdDynamic1.DataSource = dt;
            GrdDynamic1.DataBind();
        }
        private void loadDeadStock1(string itemcode, string tgl2)
        {
            Users users = (Users)Session["Users"];
            string strtanggal = string.Empty;
            string Server1 = string.Empty;
            string TbServer1 = string.Empty;
            string plant = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                plant = "'Karawang'";
                Server1 = "sqlKrwg.grcboard.com";
                TbServer1 = "[sqlKrwg.grcboard.com].bpasKrwg.dbo.";
            }
            if (users.UnitKerjaID == 7)
            {
                plant = "'Citeureup'";
                Server1 = "sqlctrp.grcboard.com";
                TbServer1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
            }
            if (users.UnitKerjaID == 13)
            {
                plant = "'Citeureup'";
                Server1 = "sqlctrp.grcboard.com";
                TbServer1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
            }
            string strSQL =
                "declare @srvrctrp nvarchar(128), @retvalctrp int " +
                "set @srvrctrp = '" + Server1 + "'; " +
                "begin try " +
                "    exec @retvalctrp = sys.sp_testlinkedserver @srvrctrp; " +
                "end try " +
                "begin catch " +
                "    set @retvalctrp = sign(@@error); " +
                "end catch; " +
                "exec " + TbServer1 + "getdatadeadstock1  '" + tgl2 + "',24,0,'" + itemcode + "'" +
                "select top 1 " + plant + " Plant,Itemcode,Stock from " + TbServer1 + "tempdeadstock where itemcode='" + itemcode + "'";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                PaneLDeadstock.Visible = true;
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic2.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                GrdDynamic2.Columns.Add(bfield);
            }
            GrdDynamic2.DataSource = dt;
            GrdDynamic2.DataBind();
        }
        private void loadDeadStock2(string itemcode, string tgl2)
        {
            Users users = (Users)Session["Users"];
            string strtanggal = string.Empty;
            string Server1 = string.Empty;
            string TbServer1 = string.Empty;
            string plant = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                plant = "'Jombang'";
                Server1 = "sqlJombang.grcboard.com";
                TbServer1 = "[sqlJombang.grcboard.com].bpasJombang.dbo.";
            }
            if (users.UnitKerjaID == 7)

            {
                plant = "'Jombang'";
                Server1 = "sqlJombang.grcboard.com";
                TbServer1 = "[sqlJombang.grcboard.com].bpasJombang.dbo.";
            }
            if (users.UnitKerjaID == 13)

            {
                plant = "'Karawang'";
                Server1 = "sqlKrwg.grcboard.com";
                TbServer1 = "[sqlKrwg.grcboard.com].bpasKrwg.dbo.";
            }
            string strSQL =
                "declare @srvrctrp nvarchar(128), @retvalctrp int " +
                "set @srvrctrp = '" + Server1 + "'; " +
                "begin try " +
                "    exec @retvalctrp = sys.sp_testlinkedserver @srvrctrp; " +
                "end try " +
                "begin catch " +
                "    set @retvalctrp = sign(@@error); " +
                "end catch; " +
                "exec " + TbServer1 + "getdatadeadstock1  '" + tgl2 + "',24,0,'" + itemcode + "'" +
                "select top 1 " + plant + " Plant,Itemcode,Stock from " + TbServer1 + "tempdeadstock where itemcode='" + itemcode + "'";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                PaneLDeadstock.Visible = true;
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic3.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                GrdDynamic3.Columns.Add(bfield);
            }
            GrdDynamic3.DataSource = dt;
            GrdDynamic3.DataBind();
        }
    }

    public class FacadeAsset
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DomainAsset objAsset = new DomainAsset();

        public FacadeAsset()
            : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public ArrayList RetrieveAssetUtama()
        {
            //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
            //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
            //string strSQL =
            //" select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
            //" (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1,13)KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
            //" from Asset where Head=1 and RowStatus>-1  and LEN(ItemCode)>=13) as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
            //" where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where RowStatus>-1) ";
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //strError = dataAccess.Error;
            //arrData = new ArrayList();

            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
            //    }
            //}

            //return arrData;
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }

            string strSQL =
            " select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
            " (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1," + query + ")KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
            " from Asset where Head=1 and RowStatus>-1  and LEN(ItemCode)>=" + query + ") as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
            " where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where LEN(KodeAsset)>=13 and RowStatus>-1) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveAssetTunggal()
        {
            //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
            //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
            string strSQL =
            " select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
            " (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1,13)KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
            " from Asset where Head=2 and RowStatus>-1  and LEN(ItemCode)>=13) as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
            " where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where RowStatus>-1) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                }
            }

            return arrData;
        }

        private DomainAsset GenerateObjectAssetUtama(SqlDataReader sdr)
        {
            DomainAsset objAsset = new DomainAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
            objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

            return objAsset;
        }

        public ArrayList RetrieveAssetKomponen(string KodeAssetKomponen)
        {
            //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
            //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
            string strSQL =
            " select ID,ItemCode KodeProjectAsset,ItemName NamaProjectAsset from Asset where ItemCode like'%" + KodeAssetKomponen + "%' and RowStatus>-1 and Head=0 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetKomponen(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveAssetTunggal(string KodeAssetKomponen)
        {
            //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
            //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
            string strSQL =
            " select ID,ItemCode KodeProjectAsset,ItemName NamaProjectAsset from Asset where ItemCode like'%" + KodeAssetKomponen + "%' and RowStatus>-1 and Head=2 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetKomponen(sqlDataReader));
                }
            }

            return arrData;
        }

        private DomainAsset GenerateObjectAssetKomponen(SqlDataReader sdr)
        {
            DomainAsset objAsset = new DomainAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
            objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

            return objAsset;
        }

        public int RetrieveClassID(string KodeClass, int Group)
        {
            string StrSql =
            //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
            " select ID from AM_Class where GroupID=" + Group + " and RowStatus>-1 and KodeClass='" + KodeClass + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public int RetrieveSubClassID(string NamaClass, int Group)
        {
            string StrSql =
            //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
            " select ID from AM_SubClass where ClassID in (select ID from AM_Class where GroupID=" + Group + " and RowStatus>-1 ) " +
            " and RowStatus>-1 and NamaClass='" + NamaClass + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }


    }

    public class DomainAsset
    {
        public int KodeAssetKomponen { get; set; }
        public int ID { get; set; }
        public string NamaAsset { get; set; }
        public string KodeProjectAsset { get; set; }
        public string NamaProjectAsset { get; set; }
    }
}