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
using static System.Net.Mime.MediaTypeNames;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormEditSPP : System.Web.UI.Page
    {
        EditSPP sh = new EditSPP();
        EditSPPDetail shd = new EditSPPDetail();
        EditSPPFacade sp = new EditSPPFacade();
        EditSPPDetailFacade spd = new EditSPPDetailFacade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    LoadSPP(Request.QueryString["ID"].ToString());
                    ddlMinta.SelectedIndex = GetPermintaanType(Request.QueryString["ID"].ToString());
                    ddlTipeBarang.SelectedIndex = GetTipeBarang(Request.QueryString["ID"].ToString());
                    ddlTipeSPP.SelectedIndex = GetTipeSPP(Request.QueryString["ID"].ToString());

                    
                }
                else
                {
                    //LoadNamaHead();
                }
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
        protected void SbClassID_OnTextChange(object sender, EventArgs e)
        {
            //GetKode("AM_SubClass", " Where ID='" + SbClassID.SelectedValue + "'", txtItemCode);

        }
        protected void ClassID_OnTextChange(object sender, EventArgs e)
        {
            FillItems("AM_SubClass", " where ClassID='" + ClassID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", SbClassID);
            //GetKode("AM_Class", " Where ID='" + ClassID.SelectedValue + "'", txtItemCode);

        }
        protected void GroupID_OnTextChange(object sender, EventArgs e)
        {
            ClassID.Items.Clear();
            SbClassID.Items.Clear();
            FillItems("AM_Class", " where GroupID='" + GroupID.SelectedValue + "' and RowStatus >-1 order by KodeClass,ID", ClassID);
            //GetKode("AM_Group", " Where ID='" + GroupID.SelectedValue + "' and RowStatus >-1", txtItemCode);
            //GetKode("AM_Group", " Where ID='" + GroupID.SelectedValue + "' and RowStatus >-1", umur_asset);
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
        public void getInfoBarang(int ItemID, int ItemTypeID)
        {
            SPPDetailFacade spp = new SPPDetailFacade();
            SPPDetail objSpp = spp.GetLastPORMS(ItemID, ItemTypeID);
            LastPO.Text = objSpp.Satuan;
            LastRMS.Text = objSpp.Keterangan;
        }
        protected void txtSatuan_OnTextChange(object sender, EventArgs e)
        {
            txtSatuanID.Text = new BiayaFacade().GetSatuanID(txtSatuan.Text).ToString();
        }
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
        public int GetWeekEnd(string frDate, string tDate)
        {
            POPurchnFacade DayOff = new POPurchnFacade();
            POPurchn JmlHari = DayOff.DayOffWeekEnd(frDate, tDate);

            return JmlHari.Status;
        }
        public int GetHariLibur(string frDate, string tDate)
        {
            POPurchnFacade DayOff = new POPurchnFacade();
            POPurchn JmlHari = DayOff.DayOffCalender(frDate, tDate);

            return JmlHari.Status;
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
        protected int checkLockATK(string itemid, int userid)
        {
            int locking = 0;
            SPPFacade sppf = new SPPFacade();
            locking = sppf.GetLockingATK(userid, itemid);
            return locking;
        }
        private bool LockUserSPP()
        {
            Session["where"] = (ddlTipeBarang.SelectedValue == "3") ?
                " and A.Keterangan='" + txtKeterangan.Text + "'" :
                " and ItemID=" + ddlNamaBarang.SelectedValue.ToString();
            ArrayList arrSPP = new SPPDetailFacade().RetrievePending(((Users)Session["Users"]).ID);
            return (arrSPP.Count > 0) ? true : false;
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
        

        #region tambah item baru
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
                            //lbAddItem.Enabled = false;

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

            
            
            txtItemIDPengganti.Text = ddlNamaBarang.SelectedValue;
        }
        #endregion
        private int GetTipeBarang(string sppDetailTipeBarang)
        {
            int intresult = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select A.ItemTypeID from SPP as A, SPPDetail B where B.ID = "+ sppDetailTipeBarang + " and A.ID = B.SPPID  and A.Status > -1 and B.Status > -1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    intresult = Int32.Parse(sdr["ItemTypeID"].ToString());
                }
            }
            return intresult;
        }

        private void LoadItem(string strNabar)
        {
            try
            {
                ddlNamaBarang.Items.Clear();
                string[] ItemSesuaiSCH = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SesuaiSch", "SPP").Split(',');
                
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
                        //ddlNamaBarang.Items.Add(new ListItem(asset.ItemName + " (" + asset.ItemCode + ")", asset.ID.ToString()));
                        // 30 Juni 2019
                        ddlNamaBarang.Items.Add(new ListItem(asset.ItemName, asset.ID.ToString()));
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
        
        private void clear()
        {
            txtTglInput.Text = string.Empty;
            ddlTipeBarang.SelectedValue = int.Parse("0").ToString();
            ddlTipeSPP.SelectedValue = int.Parse("0").ToString();
            ddlMinta.SelectedValue = int.Parse("0").ToString();
            txtCreatedBy.Text = string.Empty;
            txtCari.Text = string.Empty;
            //ddlNamaBarang.SelectedIndex = 0;
            txtItemID.Text = string.Empty;
            txtKodeBarang.Text = string.Empty;
            txtNamaBarang.Text = string.Empty;
            txtKeteranganEditSPP.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtKetBiaya.Text = string.Empty;
            txtSPP.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtNamaHead.Text = string.Empty;
            txtApproval.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            txtTglKirim.Text = string.Empty;
            txtItemID.Text = string.Empty;
            txtItemIDPengganti.Text = string.Empty;
        }

        private void validasiKeteranganEdit()
        {
            DisplayAJAXMessage(this, "Keterangan Edit Harus di isi");
            txtKeteranganEditSPP.Focus();
            
        }

        private int cekApproval(string nospp)
        {
            int intresult = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select Approval from SPP where NoSPP = '"+nospp+"' and Status >-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    intresult = Int32.Parse(sdr["Approval"].ToString());
                }
            }
            return intresult;
        }
        
        private void insertSPPEdit()
        {
            ArrayList arrDataIsertEditApproval0 = new ArrayList();
            ZetroView zwinsertEditApproval0 = new ZetroView();
            zwinsertEditApproval0.QueryType = Operation.CUSTOM;
            zwinsertEditApproval0.CustomQuery = "select s.ID,s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                       "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                       "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                       "from spp s where s.id = " + Session["id"] + " and s.[Status] >-1";

            SqlDataReader sdrEditSPPApproval0 = zwinsertEditApproval0.Retrieve();
            if (sdrEditSPPApproval0.HasRows)
            {
                while (sdrEditSPPApproval0.Read())
                {
                    sh.ID = Convert.ToInt32(sdrEditSPPApproval0["ID"]);
                    sh.NoSPP = sdrEditSPPApproval0["NoSPP"].ToString();
                    sh.Minta = Convert.ToDateTime(sdrEditSPPApproval0["Minta"]);
                    sh.PermintaanType = Convert.ToInt32(sdrEditSPPApproval0["PermintaanType"]);
                    sh.SatuanID = Convert.ToInt32(sdrEditSPPApproval0["SatuanID"]);
                    sh.GroupID = Convert.ToInt32(sdrEditSPPApproval0["GroupID"]);
                    sh.ItemTypeID = Convert.ToInt32(sdrEditSPPApproval0["ItemTypeID"]);
                    sh.Jumlah = Convert.ToDecimal(sdrEditSPPApproval0["Jumlah"]);
                    sh.JumlahSisa = Convert.ToDecimal(sdrEditSPPApproval0["JumlahSisa"]);
                    sh.Keterangan = sdrEditSPPApproval0["Keterangan"].ToString();
                    sh.Sudah = Convert.ToInt32(sdrEditSPPApproval0["Sudah"]);
                    sh.FCetak = Convert.ToInt32(sdrEditSPPApproval0["FCetak"]);
                    sh.UserID = Convert.ToInt32(sdrEditSPPApproval0["UserID"]);
                    sh.HeadID = Convert.ToInt32(sdrEditSPPApproval0["HeadID"]);
                    sh.Pending = Convert.ToInt32(sdrEditSPPApproval0["Pending"]);
                    sh.Inden = Convert.ToInt32(sdrEditSPPApproval0["Inden"]);
                    sh.AlasanBatal = sdrEditSPPApproval0["AlasanBatal"].ToString();
                    sh.AlasanCLS = sdrEditSPPApproval0["AlasanCLS"].ToString();
                    sh.Status = Convert.ToInt32(sdrEditSPPApproval0["Status"]);
                    sh.Approval = Convert.ToInt32(sdrEditSPPApproval0["Approval"]);
                    sh.CreatedBy = sdrEditSPPApproval0["CreatedBy"].ToString();
                    sh.CreatedTime = Convert.ToDateTime(sdrEditSPPApproval0["CreatedTime"]);
                    sh.LastModifiedBy = sdrEditSPPApproval0["LastModifiedBy"].ToString();
                    sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPPApproval0["LastModifiedTime"]);
                    sh.DepoID = Convert.ToInt32(sdrEditSPPApproval0["DepoID"]);
                    sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate1"]);
                    sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate2"]);
                    sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate3"]);
                    sh.JenisEdit = "SPP";
                    sh.KeteranganEditSpp = txtKeteranganEditSPP.Text;
                    //sh.ApvHeadPemohon = 0;
                    //sh.ApvMgrPemohon = 0;
                    //sh.ApvPM = 0;
                    sh.ApvPurch = 0;
                    sh.ApvAccounting = 0;
                }

            }

            int cekInputanApproval0 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
            int result0 = 0;

            if (cekInputanApproval0 == 0)
            {
                result0 = sp.InsertEditSPP(sh);
            }
            else
            {

            }
        }

        private void insertSPPdetailEdit()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                            "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                            "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                            "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

            SqlDataReader sdr = zw.Retrieve();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                    shd.SPPID = Convert.ToInt32(sdr["SPPID"]);
                    shd.GroupID = Convert.ToInt32(sdr["GroupID"]);
                    shd.ItemID = Convert.ToInt32(sdr["ItemID"]);
                    shd.Quantity = Decimal.Parse(txtQty.Text);
                    shd.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"]);
                    shd.UOMID = Convert.ToInt32(sdr["UOMID"]);
                    shd.Status = Convert.ToInt32(sdr["Status"]);
                    shd.QtyPO = Convert.ToInt32(sdr["QtyPO"]);
                    shd.Keterangan = sdr["Keterangan"].ToString();
                    shd.TglKirim = Convert.ToDateTime(sdr["TglKirim"]);
                    shd.PendingPO = Convert.ToInt32(sdr["PendingPO"]);
                    shd.AlasanPending = sdr["AlasanPending"].ToString();
                    shd.TypeBiaya = sdr["TypeBiaya"].ToString();
                    shd.Keterangan1 = sdr["Keterangan1"].ToString();
                    shd.AmGroupID = Convert.ToInt32(sdr["AmGroupID"]);
                    shd.AmClassID = Convert.ToInt32(sdr["AmClassID"]);
                    shd.AmSubClassID = Convert.ToInt32(sdr["AmSubClassID"]);
                    shd.AmLokasiID = Convert.ToInt32(sdr["AmLokasiID"]);
                    shd.MTCGroupSarmutID = Convert.ToInt32(sdr["MTCSarmutGroupID"]);
                    shd.MaterialMTCGroupID = Convert.ToInt32(sdr["MaterialMTCGroupID"]);
                    shd.UmurEkonomis = Convert.ToInt32(sdr["UmurEkonomis"]);
                    shd.NoPol = sdr["NoPol"].ToString();
                    shd.GroupSP = sdr["GroupSP"].ToString();
                    shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());

                }
            }

            int result1 = 0;

            result1 = spd.InsertEditSPPDetail(shd);
        }

        private void updateSpp()
        {
            ArrayList arrDataIsertEditApproval0 = new ArrayList();
            ZetroView zwinsertEditApproval0 = new ZetroView();
            zwinsertEditApproval0.QueryType = Operation.CUSTOM;
            zwinsertEditApproval0.CustomQuery = "select s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                       "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                       "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                       "from spp s where s.NoSPP = '" +txtSPP.Text+ "' and s.[Status] >-1";

            SqlDataReader sdrEditSPPApproval0 = zwinsertEditApproval0.Retrieve();
            if (sdrEditSPPApproval0.HasRows)
            {
                while (sdrEditSPPApproval0.Read())
                {
                    //sh.ID = Convert.ToInt32(sdrEditSPPApproval0["ID"]);
                    sh.NoSPP = sdrEditSPPApproval0["NoSPP"].ToString();
                    sh.Minta = Convert.ToDateTime(sdrEditSPPApproval0["Minta"]);
                    sh.PermintaanType = Convert.ToInt32(sdrEditSPPApproval0["PermintaanType"]);
                    sh.SatuanID = Convert.ToInt32(sdrEditSPPApproval0["SatuanID"]);
                    sh.GroupID = Convert.ToInt32(sdrEditSPPApproval0["GroupID"]);
                    sh.ItemTypeID = Convert.ToInt32(sdrEditSPPApproval0["ItemTypeID"]);
                    sh.Jumlah = Convert.ToDecimal(sdrEditSPPApproval0["Jumlah"]);
                    sh.JumlahSisa = Convert.ToDecimal(sdrEditSPPApproval0["JumlahSisa"]);
                    sh.Keterangan = sdrEditSPPApproval0["Keterangan"].ToString();
                    sh.Sudah = Convert.ToInt32(sdrEditSPPApproval0["Sudah"]);
                    sh.FCetak = Convert.ToInt32(sdrEditSPPApproval0["FCetak"]);
                    sh.UserID = Convert.ToInt32(sdrEditSPPApproval0["UserID"]);
                    sh.HeadID = Convert.ToInt32(sdrEditSPPApproval0["HeadID"]);
                    sh.Pending = Convert.ToInt32(sdrEditSPPApproval0["Pending"]);
                    sh.Inden = Convert.ToInt32(sdrEditSPPApproval0["Inden"]);
                    sh.AlasanBatal = sdrEditSPPApproval0["AlasanBatal"].ToString();
                    sh.AlasanCLS = sdrEditSPPApproval0["AlasanCLS"].ToString();
                    sh.Status = Convert.ToInt32(sdrEditSPPApproval0["Status"]);
                    sh.Approval = Convert.ToInt32(sdrEditSPPApproval0["Approval"]);
                    sh.CreatedBy = sdrEditSPPApproval0["CreatedBy"].ToString();
                    sh.CreatedTime = Convert.ToDateTime(sdrEditSPPApproval0["CreatedTime"]);
                    sh.LastModifiedBy = sdrEditSPPApproval0["LastModifiedBy"].ToString();
                    sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPPApproval0["LastModifiedTime"]);
                    sh.DepoID = Convert.ToInt32(sdrEditSPPApproval0["DepoID"]);
                    sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate1"]);
                    sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate2"]);
                    sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate3"]);

                }

            }

            
            int result0 = 0;
            result0 = sp.UpdateEditSPP(sh);

            
        }

        private void updateSPPdetail()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                            "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                            "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                            "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

            SqlDataReader sdr = zw.Retrieve();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                    shd.SPPID = Convert.ToInt32(sdr["SPPID"]);
                    shd.GroupID = Convert.ToInt32(sdr["GroupID"]);
                    shd.ItemID = Convert.ToInt32(sdr["ItemID"]);
                    shd.Quantity = Decimal.Parse(txtQty.Text);
                    shd.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"]);
                    shd.UOMID = Convert.ToInt32(sdr["UOMID"]);
                    shd.Status = Convert.ToInt32(sdr["Status"]);
                    shd.QtyPO = Convert.ToInt32(sdr["QtyPO"]);
                    shd.Keterangan = sdr["Keterangan"].ToString();
                    shd.TglKirim = Convert.ToDateTime(sdr["TglKirim"]);
                    shd.PendingPO = Convert.ToInt32(sdr["PendingPO"]);
                    shd.AlasanPending = sdr["AlasanPending"].ToString();
                    shd.TypeBiaya = sdr["TypeBiaya"].ToString();
                    shd.Keterangan1 = sdr["Keterangan1"].ToString();
                    shd.AmGroupID = Convert.ToInt32(sdr["AmGroupID"]);
                    shd.AmClassID = Convert.ToInt32(sdr["AmClassID"]);
                    shd.AmSubClassID = Convert.ToInt32(sdr["AmSubClassID"]);
                    shd.AmLokasiID = Convert.ToInt32(sdr["AmLokasiID"]);
                    shd.MTCGroupSarmutID = Convert.ToInt32(sdr["MTCSarmutGroupID"]);
                    shd.MaterialMTCGroupID = Convert.ToInt32(sdr["MaterialMTCGroupID"]);
                    shd.UmurEkonomis = Convert.ToInt32(sdr["UmurEkonomis"]);
                    shd.NoPol = sdr["NoPol"].ToString();
                    shd.GroupSP = sdr["GroupSP"].ToString();
                    shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());

                }
            }

            int result1 = 0;

            result1 = spd.UpdateSPPDetail(shd);
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {

            int nilaiApproval = 0;
            nilaiApproval = cekApproval(txtSPP.Text);
            if (nilaiApproval == 0)
            {

                if (cekDataSPP(txtSPP.Text) == 0)
                {
                    //Cancel SPP Header
                    if (txtKeteranganEditSPP.Text == "")
                    {
                        validasiKeteranganEdit();
                    }
                    else
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update spp set Status =-1 where NoSPP='" + txtSPP.Text + "' and Status >-1";
                        SqlDataReader sl = zl.Retrieve();

                        clear();
                        DisplayAJAXMessage(this, "Permintaan Cancel SPP Berhasil");
                    }

                }
                else if (cekDataSPP(txtSPP.Text) > 1)
                {
                    if (txtKeteranganEditSPP.Text == "")
                    {
                        validasiKeteranganEdit();
                    }
                    else
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update SPPDetail set Status=-1 where SPPID=" + Session["id"] + " and ID=" + Session["sppdetailid"] + " and Status >-1";
                        SqlDataReader sl = zl.Retrieve();

                        clear();
                        DisplayAJAXMessage(this, "Permintaan Cancel SPP Berhasil");
                    }

                    
                }
                else if (cekDataSPP(txtSPP.Text) == 1)
                {
                    if (txtKeteranganEditSPP.Text == "")
                    {
                        validasiKeteranganEdit();
                    }
                    else
                    {
                        //spp header
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update spp set Status =-1 where NoSPP='" + txtSPP.Text + "' and Status >-1";
                        SqlDataReader sl = zl.Retrieve();

                        //sppdetail
                        ZetroView zk = new ZetroView();
                        zk.QueryType = Operation.CUSTOM;
                        zk.CustomQuery = "update SPPDetail set Status=-1 where SPPID=" + Session["id"] + " and ID=" + Session["sppdetailid"] + " and Status >-1";
                        SqlDataReader sk = zk.Retrieve();

                        clear();
                        DisplayAJAXMessage(this, "Permintaan Cancel SPP Berhasil");
                    }

                    

                }
                

            }
            else
            {


                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region insert EditSPP
                    ArrayList arrDataIsertEdit = new ArrayList();
                    ZetroView zwinsertEdit = new ZetroView();
                    zwinsertEdit.QueryType = Operation.CUSTOM;
                    zwinsertEdit.CustomQuery = "select s.ID,s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                               "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                               "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                               "from spp s where s.id = " + Session["id"] + " and s.[Status] >-1";

                    SqlDataReader sdrEditSPP = zwinsertEdit.Retrieve();
                    if (sdrEditSPP.HasRows)
                    {
                        while (sdrEditSPP.Read())
                        {
                            sh.ID = Convert.ToInt32(sdrEditSPP["ID"]);
                            sh.NoSPP = sdrEditSPP["NoSPP"].ToString();
                            sh.Minta = Convert.ToDateTime(sdrEditSPP["Minta"]);
                            sh.PermintaanType = Convert.ToInt32(sdrEditSPP["PermintaanType"]);
                            sh.SatuanID = Convert.ToInt32(sdrEditSPP["SatuanID"]);
                            sh.GroupID = Convert.ToInt32(sdrEditSPP["GroupID"]);
                            sh.ItemTypeID = Convert.ToInt32(sdrEditSPP["ItemTypeID"]);
                            sh.Jumlah = Convert.ToDecimal(sdrEditSPP["Jumlah"]);
                            sh.JumlahSisa = Convert.ToDecimal(sdrEditSPP["JumlahSisa"]);
                            sh.Keterangan = sdrEditSPP["Keterangan"].ToString();
                            sh.Sudah = Convert.ToInt32(sdrEditSPP["Sudah"]);
                            sh.FCetak = Convert.ToInt32(sdrEditSPP["FCetak"]);
                            sh.UserID = Convert.ToInt32(sdrEditSPP["UserID"]);
                            sh.HeadID = Convert.ToInt32(sdrEditSPP["HeadID"]);
                            sh.Pending = Convert.ToInt32(sdrEditSPP["Pending"]);
                            sh.Inden = Convert.ToInt32(sdrEditSPP["Inden"]);
                            sh.AlasanBatal = sdrEditSPP["AlasanBatal"].ToString();
                            sh.AlasanCLS = sdrEditSPP["AlasanCLS"].ToString();
                            sh.Status = Convert.ToInt32(sdrEditSPP["Status"]);
                            sh.Approval = Convert.ToInt32(sdrEditSPP["Approval"]);
                            sh.CreatedBy = sdrEditSPP["CreatedBy"].ToString();
                            sh.CreatedTime = Convert.ToDateTime(sdrEditSPP["CreatedTime"]);
                            sh.LastModifiedBy = sdrEditSPP["LastModifiedBy"].ToString();
                            sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPP["LastModifiedTime"]);
                            sh.DepoID = Convert.ToInt32(sdrEditSPP["DepoID"]);
                            sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPP["ApproveDate1"]);
                            sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPP["ApproveDate2"]);
                            sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPP["ApproveDate3"]);
                            sh.JenisEdit = "SPP";
                            sh.KeteranganEditSpp = txtKeteranganEditSPP.Text;
                            //sh.ApvHeadPemohon = 0;
                            //sh.ApvMgrPemohon = 0;
                            //sh.ApvPM = 0;
                            sh.ApvPurch = 0;
                            sh.ApvAccounting = 0;
                        }

                    }

                    int cek = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    int result = 0;

                    if (cek == 0)
                    {
                        result = sp.InsertEditSPP(sh);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert SPPEdit");
                    }


                    #endregion

                    #region insertSPPEditDetail 
                    ArrayList arrData = new ArrayList();
                    ZetroView zw = new ZetroView();
                    zw.QueryType = Operation.CUSTOM;
                    zw.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP,sd.ID sppdetailid " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdr = zw.Retrieve();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                            shd.SPPID = Convert.ToInt32(sdr["SPPID"]);
                            shd.GroupID = Convert.ToInt32(sdr["GroupID"]);
                            shd.ItemID = Convert.ToInt32(sdr["ItemID"]);
                            shd.Quantity = Decimal.Parse(txtQty.Text);
                            shd.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"]);
                            shd.UOMID = Convert.ToInt32(sdr["UOMID"]);
                            shd.Status = Convert.ToInt32(sdr["Status"]);
                            shd.QtyPO = Convert.ToInt32(sdr["QtyPO"]);
                            shd.Keterangan = sdr["Keterangan"].ToString();
                            shd.TglKirim = Convert.ToDateTime(sdr["TglKirim"]);
                            shd.PendingPO = Convert.ToInt32(sdr["PendingPO"]);
                            shd.AlasanPending = sdr["AlasanPending"].ToString();
                            shd.TypeBiaya = sdr["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdr["Keterangan1"].ToString();
                            shd.AmGroupID = Convert.ToInt32(sdr["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdr["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdr["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdr["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdr["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdr["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdr["UmurEkonomis"]);
                            shd.NoPol = sdr["NoPol"].ToString();
                            shd.GroupSP = sdr["GroupSP"].ToString();
                            shd.SppDetailID = Convert.ToInt32(sdr["sppdetailid"]);

                        }
                    }

                    int result1 = 0;
                    int cek1 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    if (cek1 == 1)
                    {
                        result1 = spd.InsertEditSPPDetailCancel(shd);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert SPPEditDetail");
                    }

                    #endregion

                    #region cancel SPP
                    int cek2 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    if (cek2 == 1)
                    {
                        ZetroView zwse = new ZetroView();
                        zwse.QueryType = Operation.CUSTOM;
                        zwse.CustomQuery = "update SPP set Status = -1 where id=" + Session["id"] + "";
                        SqlDataReader sdrt = zwse.Retrieve();
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Cancel SPP");
                    }
                    
                    #endregion

                    #region Cancel SPPDetail
                    //ZetroView zwc = new ZetroView();
                    //zw.QueryType = Operation.CUSTOM;
                    //zw.CustomQuery = "update SPPDetail set Status=-1 where ID=" + int.Parse(Session["sppdetailid"].ToString()) + "";
                    //SqlDataReader sdrd = zw.Retrieve();
                    #endregion

                    clear();
                    DisplayAJAXMessage(this, "Permintaan Cancel SPP Berhasil");
                }

            }
            
        }

        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            int nilaiApproval = cekApproval(txtSPP.Text);
            if (nilaiApproval == 0)
            {
                //insertSPPEdit();
                //insertSPPdetailEdit();

                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region update ke table asli SPP, SPPDetail
                    updateSpp();
                    updateSPPdetail();

                    clear();
                    DisplayAJAXMessage(this, "Permintaan Edit SPP Berhasil di Simpan");
                    #endregion

                }

            }

            else
            {
                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region insert EditSPP
                    ArrayList arrDataIsertEdit = new ArrayList();
                    ZetroView zwinsertEdit = new ZetroView();
                    zwinsertEdit.QueryType = Operation.CUSTOM;
                    zwinsertEdit.CustomQuery = "select s.ID,s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                               "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                               "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                               "from spp s where s.id = " + Session["id"] + " and s.[Status] >-1";

                    SqlDataReader sdrEditSPP = zwinsertEdit.Retrieve();
                    if (sdrEditSPP.HasRows)
                    {
                        while (sdrEditSPP.Read())
                        {
                            sh.ID = Convert.ToInt32(sdrEditSPP["ID"]);
                            sh.NoSPP = sdrEditSPP["NoSPP"].ToString();
                            sh.Minta = Convert.ToDateTime(sdrEditSPP["Minta"]);
                            sh.PermintaanType = Convert.ToInt32(sdrEditSPP["PermintaanType"]);
                            sh.SatuanID = Convert.ToInt32(sdrEditSPP["SatuanID"]);
                            sh.GroupID = Convert.ToInt32(sdrEditSPP["GroupID"]);
                            sh.ItemTypeID = Convert.ToInt32(sdrEditSPP["ItemTypeID"]);
                            sh.Jumlah = Convert.ToDecimal(sdrEditSPP["Jumlah"]);
                            sh.JumlahSisa = Convert.ToDecimal(sdrEditSPP["JumlahSisa"]);
                            sh.Keterangan = sdrEditSPP["Keterangan"].ToString();
                            sh.Sudah = Convert.ToInt32(sdrEditSPP["Sudah"]);
                            sh.FCetak = Convert.ToInt32(sdrEditSPP["FCetak"]);
                            sh.UserID = Convert.ToInt32(sdrEditSPP["UserID"]);
                            sh.HeadID = Convert.ToInt32(sdrEditSPP["HeadID"]);
                            sh.Pending = Convert.ToInt32(sdrEditSPP["Pending"]);
                            sh.Inden = Convert.ToInt32(sdrEditSPP["Inden"]);
                            sh.AlasanBatal = sdrEditSPP["AlasanBatal"].ToString();
                            sh.AlasanCLS = sdrEditSPP["AlasanCLS"].ToString();
                            sh.Status = Convert.ToInt32(sdrEditSPP["Status"]);
                            sh.Approval = Convert.ToInt32(sdrEditSPP["Approval"]);
                            sh.CreatedBy = sdrEditSPP["CreatedBy"].ToString();
                            sh.CreatedTime = Convert.ToDateTime(sdrEditSPP["CreatedTime"]);
                            sh.LastModifiedBy = sdrEditSPP["LastModifiedBy"].ToString();
                            sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPP["LastModifiedTime"]);
                            sh.DepoID = Convert.ToInt32(sdrEditSPP["DepoID"]);
                            sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPP["ApproveDate1"]);
                            sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPP["ApproveDate2"]);
                            sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPP["ApproveDate3"]);
                            sh.JenisEdit = "SPP";
                            sh.KeteranganEditSpp = txtKeteranganEditSPP.Text;
                            //sh.ApvHeadPemohon = 0;
                            //sh.ApvMgrPemohon = 0;
                            //sh.ApvPM = 0;
                            sh.ApvPurch = 0;
                            sh.ApvAccounting = 0;
                        }

                    }

                    int cek = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    int result = 0;

                    if (cek == 0)
                    {
                        result = sp.InsertEditSPP(sh);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert ke SPPEdit");
                    }


                    #endregion

                    #region insertSPPEditDetail 
                    ArrayList arrData = new ArrayList();
                    ZetroView zw = new ZetroView();
                    zw.QueryType = Operation.CUSTOM;
                    zw.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdr = zw.Retrieve();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                            shd.SPPID = Convert.ToInt32(sdr["SPPID"]);
                            shd.GroupID = Convert.ToInt32(sdr["GroupID"]);
                            shd.ItemID = Convert.ToInt32(sdr["ItemID"]);
                            shd.Quantity = Decimal.Parse(txtQty.Text);
                            shd.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"]);
                            shd.UOMID = Convert.ToInt32(sdr["UOMID"]);
                            shd.Status = Convert.ToInt32(sdr["Status"]);
                            shd.QtyPO = Convert.ToInt32(sdr["QtyPO"]);
                            shd.Keterangan = sdr["Keterangan"].ToString();
                            shd.TglKirim = Convert.ToDateTime(sdr["TglKirim"]);
                            shd.PendingPO = Convert.ToInt32(sdr["PendingPO"]);
                            shd.AlasanPending = sdr["AlasanPending"].ToString();
                            shd.TypeBiaya = sdr["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdr["Keterangan1"].ToString();
                            shd.AmGroupID = Convert.ToInt32(sdr["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdr["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdr["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdr["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdr["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdr["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdr["UmurEkonomis"]);
                            shd.NoPol = sdr["NoPol"].ToString();
                            shd.GroupSP = sdr["GroupSP"].ToString();
                            shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());

                        }
                    }

                    int cek1 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    int result1 = 0;

                    if (cek1 == 1)
                    {
                        result1 = spd.InsertEditSPPDetail(shd);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert ke SPPDetail");
                    }


                    #endregion

                    #region cancel SPP

                    int cek2 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));

                    if (cek2 == 1)
                    {
                        ZetroView zws = new ZetroView();
                        zws.QueryType = Operation.CUSTOM;
                        zws.CustomQuery = "update SPP set Status = -1 where id=" + Session["id"] + "";
                        SqlDataReader sdrf = zws.Retrieve();

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Update ke table SPP");
                    }
                    #endregion

                    #region Cancel SPPDetail
                    //ZetroView zwc = new ZetroView();
                    //zw.QueryType = Operation.CUSTOM;
                    //zw.CustomQuery = "update SPPDetail set Status=-1 where ID=" + int.Parse(Session["sppdetailid"].ToString()) + "";
                    //SqlDataReader sdrd = zw.Retrieve();
                    #endregion

                    clear();
                    DisplayAJAXMessage(this, "Permintaan Edit SPP Berhasil di Simpan");
                }
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

                
                LoadItem(txtCari.Text.Trim());

            }
        }

        private int cekDataSPP(string nospp)
        {
            int intresult = 0;
            ZetroView z = new ZetroView();
            z.QueryType = Operation.CUSTOM;
            z.CustomQuery = "select Count(ID) dataspp from SPPDetail where SPPID in(select ID from SPP where NoSPP='" + txtSPP.Text + "') and Status >-1";

            SqlDataReader sd = z.Retrieve();
            if (sd.HasRows)
            {
                while (sd.Read())
                {
                    intresult = Int32.Parse(sd["dataspp"].ToString());
                }
            }

            return intresult;
        }
        private int GetPermintaanType(string sppdetailIDType)
        {
            int intresult = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select A.PermintaanType from SPP as A, SPPDetail B where B.ID = "+ sppdetailIDType + " and A.ID = B.SPPID  and A.Status > -1 and B.Status > -1 ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    intresult = Int32.Parse(sdr["PermintaanType"].ToString());
                }
            }
            return intresult;
        }

        private int GetTipeSPP(string sppDetailGroupID)
        {
            int intresult = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select ID from GroupsPurchn where ID in (select A.GroupID from SPP as A, SPPDetail B where B.ID = "+ sppDetailGroupID + " and A.ID = B.SPPID  and A.Status > -1 and B.Status > -1) ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    intresult = Int32.Parse(sdr["ID"].ToString());
                }
            }

            return intresult;
        }

        private void LoadSPP(string sppdetailid)
        {
            string a = Request.QueryString["ID"].ToString();
            string b = sp.GetNamaBarang(a);

            Session["ListOfSPPDetail"] = null;

            Users users = (Users)Session["Users"];
            
            EditSPPFacade sPPFacade = new EditSPPFacade();

            EditSPP sPP = new EditSPP();

            EditSPPDetail sPPDetail = new EditSPPDetail();

            sPP = sPPFacade.RetrieveByNoDetailID(sppdetailid);
            Session["sppdetailid"] = sppdetailid;

            if (sPPFacade.Error == string.Empty && sPP.ID > 0)
            {
                Session["id"] = sPP.ID;
                txtSPP.Text = sPP.NoSPP;
                txtTglInput.Text = sPP.Tanggal.ToString("dd-MMM-yyyy");
                ddlTipeSPP.SelectedValue = sPP.GroupID.ToString();
                ddlTipeBarang.SelectedIndex = sPP.ItemTypeID;
                txtCreatedBy.Text = sPP.CreatedBy;
                
                

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

                EditSPPDetailFacade sPPDetailFacade = new EditSPPDetailFacade();

                sPPDetail = sPPDetailFacade.RetrieveByIdDetail(int.Parse(sppdetailid), sPP.ID);

                if (sPPDetailFacade.Error == string.Empty && int.Parse(sppdetailid) > 0)
                {
                    Session["NoSPP"] = sPP.NoSPP;
                   

                    txtKodeBarang.Text = sPPDetail.ItemCode;
                    txtQty.Text = sPPDetail.Quantity.ToString();
                    txtSatuan.Text = sPPDetail.Satuan;
                    txtKeterangan.Text = sPPDetail.Keterangan.ToString();
                    txtTglKirim.Text = sPPDetail.TglKirim.ToString("dd-MM-yyyy");
                    txtItemID.Text = sPPDetail.ItemID.ToString();
                    txtNamaBarang.Text = b;
               
                    if (sPP.Status < 0 && users.Apv < 3)
                    {
                        //btnUpdate.Disabled = true;
                        //btnCancel.Enabled = false;
                        //btnClose.Enabled = false;
                        
                    }
                    else if (sPP.Status == 0 && sPP.Approval <= users.Apv && users.Apv < 2)
                    {
                        //btnUpdate.Disabled = false;
                        //btnCancel.Enabled = true;
                        //btnClose.Enabled = true;
                        
                    }
                    else if (sPP.Status == 0 && users.Apv > 1)
                    {
                        //btnUpdate.Disabled = true;
                        //btnCancel.Enabled = true;
                        //btnClose.Enabled = true;
                        
                    }
                    else if (sPP.Status == 0 && sPP.Approval == 0)
                    {
                        //btnUpdate.Disabled = false;
                        //btnCancel.Enabled = true;
                        //btnClose.Enabled = true;
                        
                    }
                    else
                    {
                        //btnUpdate.Disabled = true;
                        //btnCancel.Enabled = false;
                        //btnClose.Enabled = false;
                        
                    }
                    //field disable
                    string LockField = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnEditableField", "SPP");
                    //DisableField(false, LockField);
                }
            }
        }
        
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        
        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            int nilaiApproval = 0;

            nilaiApproval = cekApproval(txtSPP.Text);


            if (nilaiApproval == 0)
            {
                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region ambah item belum Approval
                    //Update ke SPP Asli
                    ZetroView zwinsertEditApproval = new ZetroView();
                    zwinsertEditApproval.QueryType = Operation.CUSTOM;
                    zwinsertEditApproval.CustomQuery = "select s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                               "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                               "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                               "from spp s where s.NoSPP = '" + txtSPP.Text + "' and s.[Status] >-1";

                    SqlDataReader sdrEditSPPApproval = zwinsertEditApproval.Retrieve();
                    if (sdrEditSPPApproval.HasRows)
                    {
                        while (sdrEditSPPApproval.Read())
                        {
                            //sh.ID = Convert.ToInt32(sdrEditSPPApproval0["ID"]);
                            sh.NoSPP = sdrEditSPPApproval["NoSPP"].ToString();
                            sh.Minta = Convert.ToDateTime(sdrEditSPPApproval["Minta"]);

                            sh.PermintaanType = int.Parse(ddlMinta.SelectedValue);
                            sh.SatuanID = int.Parse(ddlMultiGudang.SelectedValue);
                            sh.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            sh.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);

                            sh.Jumlah = Convert.ToDecimal(sdrEditSPPApproval["Jumlah"]);
                            sh.JumlahSisa = Convert.ToDecimal(sdrEditSPPApproval["JumlahSisa"]);

                            sh.Keterangan = sdrEditSPPApproval["Keterangan"].ToString();
                            sh.Sudah = Convert.ToInt32(sdrEditSPPApproval["Sudah"]);
                            sh.FCetak = Convert.ToInt32(sdrEditSPPApproval["FCetak"]);
                            sh.UserID = Convert.ToInt32(sdrEditSPPApproval["UserID"]);
                            sh.HeadID = Convert.ToInt32(sdrEditSPPApproval["HeadID"]);
                            sh.Pending = Convert.ToInt32(sdrEditSPPApproval["Pending"]);
                            sh.Inden = Convert.ToInt32(sdrEditSPPApproval["Inden"]);
                            sh.AlasanBatal = sdrEditSPPApproval["AlasanBatal"].ToString();
                            sh.AlasanCLS = sdrEditSPPApproval["AlasanCLS"].ToString();
                            sh.Status = Convert.ToInt32(sdrEditSPPApproval["Status"]);
                            sh.Approval = Convert.ToInt32(sdrEditSPPApproval["Approval"]);
                            sh.CreatedBy = sdrEditSPPApproval["CreatedBy"].ToString();
                            sh.CreatedTime = Convert.ToDateTime(sdrEditSPPApproval["CreatedTime"]);
                            sh.LastModifiedBy = sdrEditSPPApproval["LastModifiedBy"].ToString();
                            sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPPApproval["LastModifiedTime"]);
                            sh.DepoID = Convert.ToInt32(sdrEditSPPApproval["DepoID"]);
                            sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPPApproval["ApproveDate1"]);
                            sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPPApproval["ApproveDate2"]);
                            sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPPApproval["ApproveDate3"]);

                        }

                    }


                    int resultB = 0;
                    resultB = sp.UpdateEditSPP(sh);


                    //update ke SPP Detail Asli
                    ZetroView zwR = new ZetroView();
                    zwR.QueryType = Operation.CUSTOM;
                    zwR.CustomQuery = "select sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdrR = zwR.Retrieve();

                    if (sdrR.HasRows)
                    {
                        while (sdrR.Read())
                        {
                            //shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                            shd.SPPID = Convert.ToInt32(sdrR["SPPID"]);

                            shd.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            shd.ItemID = int.Parse(txtItemIDPengganti.Text);
                            shd.Quantity = Decimal.Parse(txtQty.Text);
                            shd.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
                            shd.UOMID = Convert.ToInt32(sdrR["UOMID"]);

                            shd.Status = Convert.ToInt32(sdrR["Status"]);
                            shd.QtyPO = Convert.ToInt32(sdrR["QtyPO"]);
                            shd.Keterangan = sdrR["Keterangan"].ToString();
                            shd.TglKirim = Convert.ToDateTime(sdrR["TglKirim"]);
                            shd.PendingPO = Convert.ToInt32(sdrR["PendingPO"]);
                            shd.AlasanPending = sdrR["AlasanPending"].ToString();
                            shd.TypeBiaya = sdrR["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdrR["Keterangan1"].ToString();
                            shd.AmGroupID = Convert.ToInt32(sdrR["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdrR["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdrR["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdrR["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdrR["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdrR["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdrR["UmurEkonomis"]);
                            shd.NoPol = sdrR["NoPol"].ToString();
                            shd.GroupSP = sdrR["GroupSP"].ToString();
                            shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());

                        }
                    }

                    int resultR = 0;

                    resultR = spd.UpdateSPPDetail(shd);

                    DisplayAJAXMessage(this, "Permintaan Edit SPP Berhasil di Simpan");
                    clear();
                    #endregion
                }
            }
            else
            {
                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region insert ke table EditSPP
                    ArrayList arrDataIsertEditApproval0 = new ArrayList();
                    ZetroView zwinsertEditApproval0 = new ZetroView();
                    zwinsertEditApproval0.QueryType = Operation.CUSTOM;
                    zwinsertEditApproval0.CustomQuery = "select s.ID,s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                               "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                               "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                               "from spp s where s.id = " + Session["id"] + " and s.[Status] >-1";

                    SqlDataReader sdrEditSPPApproval0 = zwinsertEditApproval0.Retrieve();
                    if (sdrEditSPPApproval0.HasRows)
                    {
                        while (sdrEditSPPApproval0.Read())
                        {
                            sh.ID = Convert.ToInt32(sdrEditSPPApproval0["ID"]);
                            sh.NoSPP = sdrEditSPPApproval0["NoSPP"].ToString();
                            sh.Minta = Convert.ToDateTime(sdrEditSPPApproval0["Minta"]);

                            sh.PermintaanType = int.Parse(ddlMinta.SelectedValue);
                            sh.SatuanID = int.Parse(ddlMultiGudang.SelectedValue);
                            sh.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            sh.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);

                            sh.Jumlah = Convert.ToDecimal(sdrEditSPPApproval0["Jumlah"]);
                            sh.JumlahSisa = Convert.ToDecimal(sdrEditSPPApproval0["JumlahSisa"]);
                            sh.Keterangan = sdrEditSPPApproval0["Keterangan"].ToString();
                            sh.Sudah = Convert.ToInt32(sdrEditSPPApproval0["Sudah"]);
                            sh.FCetak = Convert.ToInt32(sdrEditSPPApproval0["FCetak"]);
                            sh.UserID = Convert.ToInt32(sdrEditSPPApproval0["UserID"]);
                            sh.HeadID = Convert.ToInt32(sdrEditSPPApproval0["HeadID"]);
                            sh.Pending = Convert.ToInt32(sdrEditSPPApproval0["Pending"]);
                            sh.Inden = Convert.ToInt32(sdrEditSPPApproval0["Inden"]);
                            sh.AlasanBatal = sdrEditSPPApproval0["AlasanBatal"].ToString();
                            sh.AlasanCLS = sdrEditSPPApproval0["AlasanCLS"].ToString();
                            sh.Status = Convert.ToInt32(sdrEditSPPApproval0["Status"]);
                            sh.Approval = Convert.ToInt32(sdrEditSPPApproval0["Approval"]);
                            sh.CreatedBy = sdrEditSPPApproval0["CreatedBy"].ToString();
                            sh.CreatedTime = Convert.ToDateTime(sdrEditSPPApproval0["CreatedTime"]);
                            sh.LastModifiedBy = sdrEditSPPApproval0["LastModifiedBy"].ToString();
                            sh.LastModifiedTime = Convert.ToDateTime(sdrEditSPPApproval0["LastModifiedTime"]);
                            sh.DepoID = Convert.ToInt32(sdrEditSPPApproval0["DepoID"]);
                            sh.ApproveDate1 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate1"]);
                            sh.ApproveDate2 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate2"]);
                            sh.ApproveDate3 = Convert.ToDateTime(sdrEditSPPApproval0["ApproveDate3"]);
                            sh.JenisEdit = "SPP";
                            sh.KeteranganEditSpp = txtKeteranganEditSPP.Text;
                            sh.ApvPurch = 0;
                            sh.ApvAccounting = 0;
                        }

                    }

                    int cekInputanApproval0 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    int result0 = 0;

                    if (cekInputanApproval0 == 0)
                    {
                        result0 = sp.InsertEditSPP(sh);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert ke EditSPP");
                    }
                    #endregion

                    #region insert ke table EditSPPDetaiil
                    ArrayList arrData = new ArrayList();
                    ZetroView zw = new ZetroView();
                    zw.QueryType = Operation.CUSTOM;
                    zw.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdr = zw.Retrieve();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            shd.IDEditSPP = Convert.ToInt32(sdr["IDEditSPP"]);
                            shd.SPPID = Convert.ToInt32(sdr["SPPID"]);

                            shd.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            shd.ItemID = int.Parse(txtItemIDPengganti.Text);
                            shd.Quantity = Decimal.Parse(txtQty.Text);
                            shd.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);
                            shd.UOMID = Convert.ToInt32(sdr["UOMID"]);

                            shd.Status = Convert.ToInt32(sdr["Status"]);
                            shd.QtyPO = Convert.ToInt32(sdr["QtyPO"]);
                            shd.Keterangan = sdr["Keterangan"].ToString();
                            shd.TglKirim = Convert.ToDateTime(sdr["TglKirim"]);
                            shd.PendingPO = Convert.ToInt32(sdr["PendingPO"]);
                            shd.AlasanPending = sdr["AlasanPending"].ToString();
                            shd.TypeBiaya = sdr["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdr["Keterangan1"].ToString();
                            shd.AmGroupID = Convert.ToInt32(sdr["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdr["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdr["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdr["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdr["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdr["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdr["UmurEkonomis"]);
                            shd.NoPol = sdr["NoPol"].ToString();
                            shd.GroupSP = sdr["GroupSP"].ToString();
                            shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());

                        }
                    }

                    int result1 = 0;
                    int cekInputan = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    if (cekInputan == 1)
                    {
                        result1 = spd.InsertEditSPPDetail(shd);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Insert ke EditSppDetail");
                    }


                    #endregion

                    #region update ke table SPP Menjadi Non Aktif
                    int cekInputan1 = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    if (cekInputan1 == 1)
                    {
                        ZetroView zws = new ZetroView();
                        zws.QueryType = Operation.CUSTOM;
                        zws.CustomQuery = "update SPP set Status = -1 where id=" + Session["id"] + "";
                        SqlDataReader sdrf = zws.Retrieve();
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Cancel SPP");
                    }
                    
                    #endregion

                    DisplayAJAXMessage(this, "Permintaan Ubah Item SPP Berhasil di Simpan");
                    clear();
                }

            }

            
           
        }

        protected void lbTambah_Click(object sender, EventArgs e)
        {
            SPPDetailFacade sdf = new SPPDetailFacade();
            int sppdetail = 0;
            int nilaiApproval = 0;

            nilaiApproval = cekApproval(txtSPP.Text);


            if (nilaiApproval == 0)
            {
                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    
                    #region insertSPPDetail 
                    
                    ZetroView zisd = new ZetroView();
                    zisd.QueryType = Operation.CUSTOM;
                    zisd.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdisd = zisd.Retrieve();

                    if (sdisd.HasRows)
                    {
                        while (sdisd.Read())
                        {
                            
                            shd.SPPID = Convert.ToInt32(sdisd["SPPID"]);
                            shd.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            shd.ItemID = int.Parse(txtItemIDPengganti.Text);
                            shd.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);

                            shd.UOMID = Convert.ToInt32(sdisd["UOMID"]);
                            shd.Quantity = Decimal.Parse(txtQty.Text);

                            shd.Status = Convert.ToInt32(sdisd["Status"]);
                            shd.QtyPO = sppdetail;
                            shd.Keterangan = txtKeteranganEditSPP.Text;
                            shd.TglKirim = Convert.ToDateTime(sdisd["TglKirim"]);
                            shd.TypeBiaya = sdisd["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdisd["Keterangan1"].ToString();

                            shd.AmGroupID = Convert.ToInt32(sdisd["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdisd["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdisd["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdisd["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdisd["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdisd["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdisd["UmurEkonomis"]);
                            shd.NoPol = sdisd["NoPol"].ToString();
                            shd.GroupSP = sdisd["GroupSP"].ToString();
                            

                        }
                    }

                    

                    int result1 = 0;
                    result1 = spd.InsertSPPDetail(shd);
                    #endregion
                    
                }

                DisplayAJAXMessage(this, "Penambahan Item SPP Baru Berhasil");
                clear();
            }
            else
            {
                if (txtKeteranganEditSPP.Text == "")
                {
                    validasiKeteranganEdit();
                }
                else
                {
                    #region tambah item baru ke table editspp, editsppdetail
                    #region insert EditSPP

                    ZetroView ztt = new ZetroView();
                    ztt.QueryType = Operation.CUSTOM;
                    ztt.CustomQuery = "select s.ID,s.NoSPP,s.Minta,s.PermintaanType,s.SatuanID,s.GroupID,s.ItemTypeID,s.Jumlah,s.JumlahSisa,s.Keterangan, " +
                                               "s.Sudah,s.FCetak,s.UserID,s.HeadID,s.Pending,s.Inden,s.AlasanBatal,s.AlasanCLS,s.[Status],s.Approval,s.CreatedBy, " +
                                               "s.CreatedTime,s.LastModifiedBy,s.LastModifiedTime,s.DepoID,s.ApproveDate1,s.ApproveDate2,s.ApproveDate3 " +
                                               "from spp s where s.id = " + Session["id"] + " and s.[Status] >-1";

                    SqlDataReader sdtt = ztt.Retrieve();
                    if (sdtt.HasRows)
                    {
                        while (sdtt.Read())
                        {
                            sh.ID = Convert.ToInt32(sdtt["ID"]);
                            sh.NoSPP = sdtt["NoSPP"].ToString();
                            sh.Minta = Convert.ToDateTime(sdtt["Minta"]);

                            sh.PermintaanType = int.Parse(ddlMinta.SelectedValue);
                            sh.SatuanID = int.Parse(ddlMultiGudang.SelectedValue);
                            sh.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            sh.ItemTypeID = int.Parse(ddlTipeBarang.SelectedValue);

                            sh.Jumlah = Convert.ToDecimal(sdtt["Jumlah"]);
                            sh.JumlahSisa = Convert.ToDecimal(sdtt["JumlahSisa"]);
                            sh.Keterangan = sdtt["Keterangan"].ToString();
                            sh.Sudah = Convert.ToInt32(sdtt["Sudah"]);
                            sh.FCetak = Convert.ToInt32(sdtt["FCetak"]);
                            sh.UserID = Convert.ToInt32(sdtt["UserID"]);
                            sh.HeadID = Convert.ToInt32(sdtt["HeadID"]);
                            sh.Pending = Convert.ToInt32(sdtt["Pending"]);
                            sh.Inden = Convert.ToInt32(sdtt["Inden"]);
                            sh.AlasanBatal = sdtt["AlasanBatal"].ToString();
                            sh.AlasanCLS = sdtt["AlasanCLS"].ToString();
                            sh.Status = Convert.ToInt32(sdtt["Status"]);
                            sh.Approval = Convert.ToInt32(sdtt["Approval"]);
                            sh.CreatedBy = sdtt["CreatedBy"].ToString();
                            sh.CreatedTime = Convert.ToDateTime(sdtt["CreatedTime"]);
                            sh.LastModifiedBy = sdtt["LastModifiedBy"].ToString();
                            sh.LastModifiedTime = Convert.ToDateTime(sdtt["LastModifiedTime"]);
                            sh.DepoID = Convert.ToInt32(sdtt["DepoID"]);
                            sh.ApproveDate1 = Convert.ToDateTime(sdtt["ApproveDate1"]);
                            sh.ApproveDate2 = Convert.ToDateTime(sdtt["ApproveDate2"]);
                            sh.ApproveDate3 = Convert.ToDateTime(sdtt["ApproveDate3"]);
                            sh.JenisEdit = "SPP";
                            sh.KeteranganEditSpp = txtKeteranganEditSPP.Text;
                            //sh.ApvHeadPemohon = 0;
                            //sh.ApvMgrPemohon = 0;
                            //sh.ApvPM = 0;
                            sh.ApvPurch = 0;
                            sh.ApvAccounting = 0;
                        }

                    }

                    int cek = int.Parse(sp.CekInputanEditSPP(sh.NoSPP));
                    int result = 0;

                    if (cek == 0)
                    {
                        result = sp.InsertEditSPP(sh);
                    }
                    else
                    {

                    }


                    #endregion

                    #region insertSPPEditDetail 



                    ZetroView zttd = new ZetroView();
                    zttd.QueryType = Operation.CUSTOM;
                    zttd.CustomQuery = "select (select IDEditSPP from EditSPP where NoSPP='" + txtSPP.Text + "' and [Status] >-1)IDEditSPP,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemTypeID,sd.UOMID,sd.[Status],sd.QtyPO,sd.Keterangan,sd.tglkirim,isnull(sd.PendingPO,0)PendingPO," +
                                    "sd.AlasanPending,sd.TypeBiaya,sd.Keterangan1,sd.AmGroupID,sd.AmClassID,sd.AmSubClassID,sd.AmLokasiID,sd.MTCSarmutGroupID,sd.MaterialMTCGroupID," +
                                    "sd.UmurEkonomis,sd.NoPol,sd.GroupSP " +
                                    "from SPPDetail sd where sd.SPPID = " + int.Parse(Session["id"].ToString()) + "  and sd.ID = " + int.Parse(Session["sppdetailid"].ToString()) + " and sd.[Status] > -1";

                    SqlDataReader sdttd = zttd.Retrieve();

                    if (sdttd.HasRows)
                    {
                        while (sdttd.Read())
                        {
                            shd.IDEditSPP = Convert.ToInt32(sdttd["IDEditSPP"]);
                            shd.SPPID = Convert.ToInt32(sdttd["SPPID"]);

                            shd.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
                            shd.ItemID = int.Parse(txtItemIDPengganti.Text);
                            shd.Quantity = Decimal.Parse(txtQty.Text);
                            shd.ItemTypeID = Convert.ToInt32(sdttd["ItemTypeID"]);

                            shd.UOMID = Convert.ToInt32(sdttd["UOMID"]);

                            shd.Status = Convert.ToInt32(sdttd["Status"]);
                            shd.QtyPO = Convert.ToInt32(sdttd["QtyPO"]);
                            shd.Keterangan = sdttd["Keterangan"].ToString();
                            shd.TglKirim = Convert.ToDateTime(sdttd["TglKirim"]);
                            shd.PendingPO = Convert.ToInt32(sdttd["PendingPO"]);
                            shd.AlasanPending = sdttd["AlasanPending"].ToString();
                            shd.TypeBiaya = sdttd["TypeBiaya"].ToString();
                            shd.Keterangan1 = sdttd["Keterangan1"].ToString();
                            shd.AmGroupID = Convert.ToInt32(sdttd["AmGroupID"]);
                            shd.AmClassID = Convert.ToInt32(sdttd["AmClassID"]);
                            shd.AmSubClassID = Convert.ToInt32(sdttd["AmSubClassID"]);
                            shd.AmLokasiID = Convert.ToInt32(sdttd["AmLokasiID"]);
                            shd.MTCGroupSarmutID = Convert.ToInt32(sdttd["MTCSarmutGroupID"]);
                            shd.MaterialMTCGroupID = Convert.ToInt32(sdttd["MaterialMTCGroupID"]);
                            shd.UmurEkonomis = Convert.ToInt32(sdttd["UmurEkonomis"]);
                            shd.NoPol = sdttd["NoPol"].ToString();
                            shd.GroupSP = sdttd["GroupSP"].ToString();
                            //shd.SppDetailID = int.Parse(Session["sppdetailid"].ToString());
                            shd.SppDetailID = sppdetail;

                        }
                    }

                    int result1 = 0;

                    result1 = spd.InsertEditSPPDetail(shd);
                    #endregion

                    #region cancel SPP
                    ZetroView zwst = new ZetroView();
                    zwst.QueryType = Operation.CUSTOM;
                    zwst.CustomQuery = "update SPP set Status = -1 where id=" + Session["id"] + "";
                    SqlDataReader sdrf = zwst.Retrieve();
                    #endregion

                    #region Cancel SPPDetail
                    //ZetroView zwc = new ZetroView();
                    //zw.QueryType = Operation.CUSTOM;
                    //zw.CustomQuery = "update SPPDetail set Status=-1 where ID=" + int.Parse(Session["sppdetailid"].ToString()) + "";
                    //SqlDataReader sdrd = zw.Retrieve();
                    #endregion

                    
                    #endregion
                }

                clear();
                DisplayAJAXMessage(this, "Permintaan Penambahan Item SPP Berhasil di Simpan");
            }
        }
    }
    
}

