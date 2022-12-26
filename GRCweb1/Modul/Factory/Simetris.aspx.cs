using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Collections;
using DataAccessLayer;

namespace GRCweb1.Modul.Factory
{
    public partial class Simetris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                DatePicker1.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                LoadGroupM();
                clearform();

                Users users = (Users)Session["Users"];

                if (users.UnitKerjaID != 1)
                {                   
                    RBPotong3.Visible = false;
                }

                if (users.DeptID == 3)
                {
                    LabelMCutter.Visible = true; LoadMesinCutter(); ddlMCutter.Visible = true;
                }
            }
        }

        private void LoadMesinCutter()
        {
            MasterDefect domainMCutter = new MasterDefect();
            FacadeMasterDefect facadeMCutter = new FacadeMasterDefect();
            ArrayList arrMCutter = facadeMCutter.RetrieveMesinCutter();
            if (arrMCutter.Count > 0)
            {
                ddlMCutter.Items.Clear();
                ddlMCutter.Items.Add(new ListItem("---- Pilih Mesin Cutter ----", "0"));
                foreach (MasterDefect msn in arrMCutter)
                {
                    ddlMCutter.Items.Add(new ListItem(msn.NamaMesinCutter, msn.ID.ToString()));
                }
            }

        }

        private void LoadGroupM()
        {
            ArrayList arrGroupM = new ArrayList();
            T3_GroupsFacade groupFacade = new T3_GroupsFacade();
            arrGroupM = groupFacade.Retrieve();
            try
            {
                foreach (T3_Groups groups in arrGroupM)
                {
                    RBList.Items.Add(new ListItem(groups.Groups, groups.ID.ToString()));
                }
            }
            catch { }
        }
        private void clearform()
        {
            Session["serahID"] = null;
            Session["lokid1"] = null;
            Session["lokid2"] = null;
            Session["lokasi1"] = null;
            Session["partno1"] = null;
            Session["itemid1"] = null;
            Session["itemid2"] = null;
            Session["hpp1"] = null;
            Session["stock1"] = null;
            Session["hpp2"] = null;
            Session["stock2"] = null;
            Session["hpp2"] = null;
            Session["luas1"] = null;
            Session["luas2"] = null;

            txtPartnoA.Text = string.Empty;
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtPartname1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtStock1.Text = string.Empty;
            txtQty1.Text = string.Empty;

            txtPartnoB.Text = string.Empty;
            txtTebal2.Text = string.Empty;
            txtLebar2.Text = string.Empty;
            txtPanjang2.Text = string.Empty;
            txtPartname2.Text = string.Empty;
            txtLokasi2.Text = string.Empty;
            txtStock2.Text = string.Empty;
            txtQty2.Text = string.Empty;
            LoadDataGridViewtrans();
            LoadDataGridViewSimetris();
        }

        private void LoadDataGridViewtrans()
        {
            ArrayList arrT3Serah = new ArrayList();
            T3_SerahFacade T3Serah = new T3_SerahFacade();
            string criteria = string.Empty;
            if (txtLokasiC.Visible = true && txtLokasiC.Text != string.Empty)
                criteria = " and B.Lokasi='" + txtLokasiC.Text.Trim() + "' ";
            else
                if (txtPartnoC.Text != string.Empty)
                criteria = " and C.PartNo='" + txtPartnoC.Text.Trim() + "' ";
            //if (criteria != string.Empty)
            arrT3Serah = T3Serah.RetrieveStock(criteria);
            //Session["arrT3Serah"] = arrT3Serah;
            GridViewtrans.DataSource = arrT3Serah;
            GridViewtrans.DataBind();
        }

        private void LoadDataGridViewSimetris()
        {
            ArrayList arrT3Simetris = new ArrayList();
            T3_SimetrisFacade T3Simetris = new T3_SimetrisFacade();
            arrT3Simetris = T3Simetris.RetrieveBytgl(DateTime.Parse(DatePicker1.Text).ToString("yyyyMMdd"));
            GridViewSimetris.DataSource = arrT3Simetris;
            GridViewSimetris.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            //BACA data di table simetris
            //1. keluar dari serahID sejumlah QTYIN (di tabel t3_rekap ada di qtyout)
            //2. masuk ke lokid dan itemID sejumlah qtyout (di tabel  t3_rekap ada di qtyin)

            //if (txtQty1.Text == string.Empty || txtQty2.Text == string.Empty || RBList.SelectedIndex==-1)
            Users users = (Users)Session["Users"];

            if (users.DeptID == 3 && ddlMCutter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Anda belum menentukan pilihan Mesin Cutter !!");
                return;
            }

            if ((txtPartnoA.Text.Substring(3, 3) == "-3-" || txtPartnoA.Text.Substring(3, 3) == "-M-") && txtPartnoB.Text.Substring(3, 3) == "-P-" && ddlDefect.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Anda belum menentukan Jenis Defect !!");
                return;

            }
            if (txtQty1.Text == string.Empty || txtQty2.Text == string.Empty || RBList.SelectedIndex < 0)
            {
                DisplayAJAXMessage(this, "Data belum lengkap");
                return;
            }
            if (txtPartnoB.Text.Substring(3, 3) == "-1-")
            {
                DisplayAJAXMessage(this, "Partno tahap 1 tidak diizinkan.");
                return;
            }
            //if (txtPartnoA.Text.Substring(3, 3) == "-S-" && (txtPartnoB.Text.Substring(3, 3) == "-3-" || txtPartnoB.Text.Substring(3, 3) == "-P-"))
            //{
            //    DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
            //    return;
            //}

            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(DatePicker1.Text).Year;
            int Bulan = DateTime.Parse(DatePicker1.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            LabelDefect.Visible = false; ddlDefect.Visible = false;

            ArrayList arrt3_serahT = new ArrayList();
            ArrayList arrt3_rekapT = new ArrayList();
            string strError = string.Empty;
            int intresult = 0;
            decimal lastAvgHPP1 = decimal.Parse(Session["hpp1"].ToString());
            decimal laststock1 = Convert.ToDecimal(Session["stock1"].ToString());
            decimal lastAvgHPP2 = decimal.Parse(Session["hpp2"].ToString());
            decimal laststock2 = Convert.ToDecimal(Session["stock2"].ToString());
            //decimal crntHPP2 = decimal.Parse(Session["hpp2"].ToString());
            decimal luas1 = decimal.Parse(Session["luas1"].ToString());
            decimal luas2 = decimal.Parse(Session["luas2"].ToString());
            if (luas1 < luas2)
            {
                //if (txtPartnoA.Text.Contains("-W-") == true && txtPartnoB.Text.Contains("-W-") == true &&
                //        Convert.ToDouble(txtTebal1.Text) == 3.5 && Convert.ToDouble(txtTebal2.Text) == 4)
                //{
                //    DisplayAJAXMessage(this, "Mutasi Pink Board");
                //}
                //else
                //{
                DisplayAJAXMessage(this, "Ukuran tidak mencukupi");
                return;
                //}
            }

            //Users users = (Users)Session["Users"];
            T3_Serah t3serahK = new T3_Serah();//Asal Partno
            T3_Serah t3serahT = new T3_Serah();//terima Partno
            T3_Rekap rekapK = new T3_Rekap();//asal partno qtyout
            T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
            FC_Items items = new FC_Items();

            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_RekapFacade rekapFacade = new T3_RekapFacade();//t3_rekap
            T3_SerahFacade SerahFacade = new T3_SerahFacade();//t3_serah
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();

            int panjang1 = 0;
            int panjang2 = 0;
            items = new FC_Items();
            items = ItemsFacade.RetrieveByPartNo(txtPartnoA.Text.Trim());
            int itemid1 = items.ID;
            decimal tebal1 = items.Tebal;
            int lebar1 = items.Lebar;
            panjang1 = items.Panjang;
            items = new FC_Items();
            items = ItemsFacade.RetrieveByPartNo(txtPartnoB.Text.Trim());
            decimal tebal2 = items.Tebal;
            int lebar2 = items.Lebar;
            panjang2 = items.Panjang;
            int itemid2 = items.ID;

            //Cek apakah barang std atau efo WO-IT-J0010521 (fajri)
            FC_Items std = new FC_Items();
            std = new FC_Items();
            std = ItemsFacade.RetrievePartnoStandar(txtPartnoA.Text.Substring(0, 3), items.Tebal, items.Lebar, items.Panjang);

            #region Retrieve Partno bukan Listplank added 08 Agustus 2019 By Beny
            MasterDefect mc = new MasterDefect();
            FacadeMasterDefect fmc = new FacadeMasterDefect();
            mc = fmc.RetrieveData(Convert.ToInt32(tebal2), lebar2, panjang2);
            #endregion

            if (panjang1 < panjang2)
            {
                DisplayAJAXMessage(this, "Panjang partno tujuan tidak mencukupi");
                return;
            }
            // Off 08 Agustus 2019 By Beny 
            //if (tebal1 >= 8 && tebal1 <= 9 && panjang1 >= 2200 && lebar1 > 1000)

            // Added 08 Agustus 2019 By Beny
            if ((tebal1 >= 8 && tebal1 <= 9 && panjang1 >= 2200 && lebar1 > 1000 && mc.Lock > 0 && mc.ID > 0)
                || (tebal1 >= 8 && tebal1 <= 9 && panjang1 >= 2200 && lebar1 > 1000 && mc.ID == 0))
            {
                if (lebar2 <= 300 && lebar2 >= 100)
                {
                    DisplayAJAXMessage(this, "Lakukan proses ini pada inputan proses listplank");
                    return;
                }
            }
            int awalID = 0;
            t3serahK.Flag = "kurang";
            t3serahK.ItemID = itemid1;
            t3serahK.GroupID = int.Parse(Session["groupid"].ToString());
            t3serahK.ID = int.Parse(Session["serahid"].ToString());
            t3serahK.LokID = int.Parse(Session["lokid1"].ToString());
            t3serahK.Qty = Convert.ToInt32(txtQty1.Text);
            t3serahK.CreatedBy = users.UserName;
            int stock = 0; int stock2 = 0;
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            stock = SerahFacade.GetStock(t3serah.LokID, t3serah.ItemID);
            if (stock - Convert.ToInt32(txtQty1.Text) < 0)
            {
                DisplayAJAXMessage(this, "Stock " + txtLokasi1.Text + " tidak mencukupi, proses dibatalkan !");
                return;
            }
            //intresult = SerahFacade.Insert(t3serah);
            awalID = t3serahK.ID;
            rekapK.DestID = 0;
            rekapK.SerahID = int.Parse(Session["serahid"].ToString());
            rekapK.T1serahID = 0;
            rekapK.GroupID = int.Parse(Session["groupid"].ToString());
            rekapK.LokasiID = int.Parse(Session["lokid1"].ToString());
            rekapK.ItemIDSer = itemid1;
            rekapK.TglTrm = DateTime.Parse(DatePicker1.Text);
            rekapK.QtyInTrm = 0;
            rekapK.QtyOutTrm = Convert.ToInt32(txtQty1.Text);
            rekapK.HPP = lastAvgHPP1;
            rekapK.CreatedBy = users.UserName;
            rekapK.Keterangan = txtPartnoB.Text;
            rekapK.SA = stock;
            rekapK.Process = "Simetris";
            //intresult = rekapFacade.Insert(rekap);

            //proses lokasi akhir
            t3serahT.Flag = "tambah";
            t3serahT.ItemID = itemid2;
            t3serahT.GroupID = Convert.ToInt32(RBList.SelectedValue);
            t3serahT.ID = int.Parse(Session["serahid"].ToString());
            t3serahT.LokID = int.Parse(Session["lokid2"].ToString());
            t3serahT.Qty = Convert.ToInt32(txtQty2.Text);
            arrt3_serahT.Add(t3serahT);

            decimal AvgHPP = 0;
            decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
            t3serahT.HPP = AvgHPP;
            t3serahT.CreatedBy = users.UserName;
            //intresult = SerahFacade.Insert(t3serah);
            /*dapatkan stock partno tujuan*/
            T3_Serah t3serah2 = new T3_Serah();
            t3serah2 = t3_SerahFacade.RetrieveStockByPartno(txtPartnoB.Text, txtLokasi2.Text);
            stock2 = SerahFacade.GetStock(t3serah2.LokID, t3serah2.ItemID);
            rekapT.DestID = 0;
            rekapT.SerahID = int.Parse(Session["serahid"].ToString());
            rekapT.GroupID = Convert.ToInt32(RBList.SelectedValue);
            rekapT.T1serahID = 0;
            rekapT.LokasiID = int.Parse(Session["lokid2"].ToString());
            rekapT.ItemIDSer = itemid2;
            rekapT.TglTrm = DateTime.Parse(DatePicker1.Text);
            rekapT.QtyInTrm = Convert.ToInt32(txtQty2.Text);
            rekapT.QtyOutTrm = 0;
            rekapT.HPP = HPPnewItem;
            rekapT.GroupID = 0;
            rekapT.CreatedBy = users.UserName;
            rekapT.Keterangan = txtPartnoA.Text;
            rekapT.Process = "Simetris";
            rekapT.SA = stock2;
            arrt3_rekapT.Add(rekapT);
            //intresult = rekapFacade.Insert(rekap);
            #region AutoBS
            //proses lokasi akhir autoBP
            int newitemID = 0;
            FC_Lokasi LokasiBSSimpan = new FC_Lokasi();
            FC_LokasiFacade LokasiF = new FC_LokasiFacade();
            int ada = LokasiF.check("Z99");
            if (ada == 0)
            {
                LokasiBSSimpan.LokTypeID = 3;
                LokasiBSSimpan.Lokasi = "Z99";
                LokasiF.Insert(LokasiBSSimpan);
            }
            LokasiBSSimpan = LokasiF.RetrieveByLokasi("Z99");
            FC_Lokasi LokasiBSBuang = new FC_Lokasi();
            ada = 0;
            ada = LokasiF.check("BSAUTO");
            if (ada == 0)
            {
                LokasiBSBuang.LokTypeID = 3;
                LokasiBSBuang.Lokasi = "BSAUTO";
                LokasiF.Insert(LokasiBSBuang);
            }
            LokasiBSBuang = LokasiF.RetrieveByLokasi("BSAUTO");
            int LokasiBSID = 0;
            FC_Items Item = new FC_Items();
            FC_ItemsFacade ItemF = new FC_ItemsFacade();
            if (ChkConvertBS.Checked == true)
            {
                #region AutoBS1
                if (LCPartnoBS1.Text != string.Empty && LCPartnoBS1.Text.Trim().Length > 15)
                {


                    if (cekpartno(LCPartnoBS1.Text) == 1)
                    {

                        Item.ItemTypeID = 3;
                        Item.Kode = LCPartnoBS1.Text.Substring(0, 3);
                        Item.Tebal = decimal.Parse(LCPartnoBS1.Text.Substring(6, 3)) / 10;
                        Item.Lebar = int.Parse(LCPartnoBS1.Text.Substring(9, 4));
                        Item.Panjang = int.Parse(LCPartnoBS1.Text.Substring(13, 4));
                        Item.Volume = decimal.Parse(LCPartnoBS1.Text.Substring(6, 3)) / 1000 * decimal.Parse(LCPartnoBS1.Text.Substring(9, 4)) / 1000 *
                            decimal.Parse(LCPartnoBS1.Text.Substring(13, 4)) / 1000;
                        Item.Partno = LCPartnoBS1.Text;
                        Item.ItemDesc = "Sisa Potong";
                        Item.GroupID = 0;
                        newitemID = ItemF.Insert(Item);
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS1.Text);
                        newitemID = Item.ID;
                    }
                    else
                    {
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS1.Text);
                        newitemID = Item.ID;
                    }
                    LokasiBSID = 0;
                    //if (Item.Lebar < 400)
                    LokasiBSID = LokasiBSBuang.ID;
                    //else
                    //    LokasiBSID = LokasiBSSimpan.ID;
                    if (newitemID > 0)
                    {
                        t3serahT = new T3_Serah();
                        t3serahT.Flag = "tambah";
                        t3serahT.ItemID = newitemID;
                        t3serahT.GroupID = Item.GroupID;
                        t3serahT.LokID = LokasiBSID;
                        t3serahT.Qty = Convert.ToInt32(LCQtyBS1.Text);
                        arrt3_serahT.Add(t3serahT);
                    }
                    stock2 = 0;
                    T3_Serah t3serah3 = new T3_Serah();
                    t3serah3 = t3_SerahFacade.RetrieveStockByPartno(LCPartnoBS1.Text, LCLokBS1.Text);
                    stock2 = SerahFacade.GetStock(t3serah3.LokID, t3serah3.ItemID);
                    rekapT = new T3_Rekap();
                    rekapT.DestID = 0;
                    rekapT.SerahID = int.Parse(Session["serahid"].ToString());
                    rekapT.GroupID = Convert.ToInt32(RBList.SelectedValue);
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = LokasiBSID;
                    rekapT.ItemIDSer = newitemID;
                    rekapT.TglTrm = DateTime.Parse(DatePicker1.Text);
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS1.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoA.Text;
                    rekapT.Process = "Simetris";
                    rekapT.SA = stock2;
                    arrt3_rekapT.Add(rekapT);
                }
                #endregion
                #region AutoBS2
                if (LCPartnoBS2.Text != string.Empty)
                {
                    newitemID = 0;
                    if (cekpartno(LCPartnoBS2.Text) == 1 && LCPartnoBS2.Text.Trim().Length > 15)
                    {
                        Item.ItemTypeID = 3;
                        Item.Kode = LCPartnoBS2.Text.Substring(0, 3);
                        Item.Tebal = decimal.Parse(LCPartnoBS2.Text.Substring(6, 3)) / 10;
                        Item.Lebar = int.Parse(LCPartnoBS2.Text.Substring(9, 4));
                        Item.Panjang = int.Parse(LCPartnoBS2.Text.Substring(13, 4));
                        Item.Volume = decimal.Parse(LCPartnoBS2.Text.Substring(6, 3)) / 1000 * decimal.Parse(LCPartnoBS2.Text.Substring(9, 4)) / 1000 *
                            decimal.Parse(LCPartnoBS2.Text.Substring(13, 4)) / 1000;
                        Item.Partno = LCPartnoBS2.Text;
                        Item.ItemDesc = "Sisa Potong";
                        Item.GroupID = 0;
                        ItemF.Insert(Item);
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS2.Text);
                        newitemID = Item.ID;
                    }
                    else
                    {
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS2.Text);
                        newitemID = Item.ID;
                    }
                    LokasiBSID = 0;
                    //if (Item.Lebar < 400)
                    LokasiBSID = LokasiBSBuang.ID;
                    //else
                    //    LokasiBSID = LokasiBSSimpan.ID;
                    if (newitemID > 0)
                    {
                        t3serahT = new T3_Serah();
                        t3serahT.Flag = "tambah";
                        t3serahT.ItemID = newitemID;
                        t3serahT.GroupID = Item.GroupID;
                        t3serahT.LokID = LokasiBSID;
                        t3serahT.Qty = Convert.ToInt32(LCQtyBS2.Text);
                        arrt3_serahT.Add(t3serahT);
                    }
                    stock2 = 0;
                    T3_Serah t3serah4 = new T3_Serah();
                    t3serah4 = t3_SerahFacade.RetrieveStockByPartno(LCPartnoBS2.Text, LCLokBS2.Text);
                    stock2 = SerahFacade.GetStock(t3serah4.LokID, t3serah4.ItemID);
                    rekapT = new T3_Rekap();
                    rekapT.DestID = 0;
                    rekapT.SerahID = int.Parse(Session["serahid"].ToString());
                    rekapT.GroupID = Convert.ToInt32(RBList.SelectedValue);
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = LokasiBSID;
                    rekapT.ItemIDSer = newitemID;
                    rekapT.TglTrm = DateTime.Parse(DatePicker1.Text);
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS2.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoA.Text;
                    rekapT.Process = "Simetris";
                    rekapT.SA = stock2;
                    arrt3_rekapT.Add(rekapT);
                }
                #endregion
                #region AutoBS3
                if (LCPartnoBS3.Text != string.Empty)
                {
                    newitemID = 0;
                    if (cekpartno(LCPartnoBS3.Text) == 1 && LCPartnoBS3.Text.Trim().Length > 15)
                    {
                        Item.ItemTypeID = 3;
                        Item.Kode = LCPartnoBS3.Text.Substring(0, 3);
                        Item.Tebal = decimal.Parse(LCPartnoBS3.Text.Substring(6, 3)) / 10;
                        Item.Lebar = int.Parse(LCPartnoBS3.Text.Substring(9, 4));
                        Item.Panjang = int.Parse(LCPartnoBS3.Text.Substring(13, 4));
                        Item.Volume = decimal.Parse(LCPartnoBS3.Text.Substring(6, 3)) / 1000 * decimal.Parse(LCPartnoBS3.Text.Substring(9, 4)) / 1000 *
                            decimal.Parse(LCPartnoBS3.Text.Substring(13, 4)) / 1000;
                        Item.Partno = LCPartnoBS3.Text;
                        Item.ItemDesc = "Sisa Potong";
                        Item.GroupID = 0;
                        ItemF.Insert(Item);
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS3.Text);
                        newitemID = Item.ID;
                    }
                    else
                    {
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS3.Text);
                        newitemID = Item.ID;
                    }
                    LokasiBSID = 0;
                    //if (Item.Lebar < 400)
                    LokasiBSID = LokasiBSBuang.ID;
                    //else
                    //    LokasiBSID = LokasiBSSimpan.ID;
                    if (newitemID > 0)
                    {
                        t3serahT = new T3_Serah();
                        t3serahT.Flag = "tambah";
                        t3serahT.ItemID = newitemID;
                        t3serahT.GroupID = Item.GroupID;
                        t3serahT.LokID = LokasiBSID;
                        t3serahT.Qty = Convert.ToInt32(LCQtyBS3.Text);
                        arrt3_serahT.Add(t3serahT);
                    }
                    stock2 = 0;
                    T3_Serah t3serah4 = new T3_Serah();
                    t3serah4 = t3_SerahFacade.RetrieveStockByPartno(LCPartnoBS3.Text, LCLokBS3.Text);
                    stock2 = SerahFacade.GetStock(t3serah4.LokID, t3serah4.ItemID);
                    rekapT = new T3_Rekap();
                    rekapT.DestID = 0;
                    rekapT.SerahID = int.Parse(Session["serahid"].ToString());
                    rekapT.GroupID = Convert.ToInt32(RBList.SelectedValue);
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = LokasiBSID;
                    rekapT.ItemIDSer = newitemID;
                    rekapT.TglTrm = DateTime.Parse(DatePicker1.Text);
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS3.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoA.Text;
                    rekapT.Process = "Simetris";
                    rekapT.SA = stock2;
                    arrt3_rekapT.Add(rekapT);
                }
                #endregion
                #region AutoBS4
                if (LCPartnoBS4.Text != string.Empty)
                {
                    newitemID = 0;
                    if (cekpartno(LCPartnoBS4.Text) == 1 && LCPartnoBS4.Text.Trim().Length > 15)
                    {
                        Item.ItemTypeID = 3;
                        Item.Kode = LCPartnoBS4.Text.Substring(0, 3);
                        Item.Tebal = decimal.Parse(LCPartnoBS4.Text.Substring(6, 3)) / 10;
                        Item.Lebar = int.Parse(LCPartnoBS4.Text.Substring(9, 4));
                        Item.Panjang = int.Parse(LCPartnoBS4.Text.Substring(13, 4));
                        Item.Volume = decimal.Parse(LCPartnoBS4.Text.Substring(6, 3)) / 1000 * decimal.Parse(LCPartnoBS4.Text.Substring(9, 4)) / 1000 *
                            decimal.Parse(LCPartnoBS4.Text.Substring(13, 4)) / 1000;
                        Item.Partno = LCPartnoBS4.Text;
                        Item.ItemDesc = "Sisa Potong";
                        Item.GroupID = 0;
                        ItemF.Insert(Item);
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS4.Text);
                        newitemID = Item.ID;
                    }
                    else
                    {
                        Item = ItemF.RetrieveByPartNo(LCPartnoBS4.Text);
                        newitemID = Item.ID;
                    }
                    LokasiBSID = 0;
                    //if (Item.Lebar < 400)
                    LokasiBSID = LokasiBSBuang.ID;
                    //else
                    //    LokasiBSID = LokasiBSSimpan.ID;
                    if (newitemID > 0)
                    {
                        t3serahT = new T3_Serah();
                        t3serahT.Flag = "tambah";
                        t3serahT.ItemID = newitemID;
                        t3serahT.GroupID = Item.GroupID;
                        t3serahT.LokID = LokasiBSID;
                        t3serahT.Qty = Convert.ToInt32(LCQtyBS4.Text);
                        arrt3_serahT.Add(t3serahT);
                    }
                    stock2 = 0;
                    T3_Serah t3serah4 = new T3_Serah();
                    t3serah4 = t3_SerahFacade.RetrieveStockByPartno(LCPartnoBS4.Text, LCLokBS4.Text);
                    stock2 = SerahFacade.GetStock(t3serah4.LokID, t3serah4.ItemID);
                    rekapT = new T3_Rekap();
                    rekapT.DestID = 0;
                    rekapT.SerahID = int.Parse(Session["serahid"].ToString());
                    rekapT.GroupID = Convert.ToInt32(RBList.SelectedValue);
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = LokasiBSID;
                    rekapT.ItemIDSer = newitemID;
                    rekapT.TglTrm = DateTime.Parse(DatePicker1.Text);
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS4.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoA.Text;
                    rekapT.Process = "Simetris";
                    rekapT.SA = stock2;
                    arrt3_rekapT.Add(rekapT);
                }
                #endregion
            }
            #endregion
            //rekam table simetris
            T3_Serah t3serahCek = new T3_Serah();
            t3serahCek = SerahFacade.RetrieveByID(int.Parse(Session["serahid"].ToString()));
            T3_Simetris simetris = new T3_Simetris();
            T3_SimetrisFacade SimetrisFacade = new T3_SimetrisFacade();
            simetris.RekapID = intresult;
            simetris.SerahID = int.Parse(Session["serahid"].ToString());
            simetris.LokasiID = int.Parse(Session["lokid2"].ToString());
            simetris.TglSm = DateTime.Parse(DatePicker1.Text);
            simetris.ItemID = itemid2;
            simetris.QtyInSm = Convert.ToInt32(txtQty1.Text);
            simetris.QtyOutSm = Convert.ToInt32(txtQty2.Text);
            simetris.GroupID = Convert.ToInt32(RBList.SelectedValue);

            if (users.DeptID == 3)
            {
                simetris.MCutter = ddlMCutter.SelectedItem.ToString().Trim();
            }
            else
            {
                simetris.MCutter = "-";
            }
            if (RBNonNC.Checked == true)
            {
                simetris.NCH = 0;
                simetris.NCSS = 0;
                simetris.NCSE = 0;
            }
            if (RBNCHandling.Checked == true)
            {
                simetris.NCH = 1;
                simetris.NCSS = 0;
                simetris.NCSE = 0;
            }

            //Jika barang std
            if (RBNCSortir.Checked == true && std.Tebal != 0)
            {
                simetris.NCH = 0;
                simetris.NCSS = 1;
                simetris.NCSE = 0;
            }

            //if (RBNCSortir.Checked==true && RBStd.Checked == true)
            //{
            //    simetris.NCH = 0;
            //    simetris.NCSS = 1;
            //    simetris.NCSE = 0;
            //}

            //Jika barang efo
            if (RBNCSortir.Checked == true && std.Tebal == 0)
            {
                simetris.NCH = 0;
                simetris.NCSS = 0;
                simetris.NCSE = 1;
            }
            //if (RBNCSortir.Checked == true && RBEfo.Checked == true)
            //{
            //    simetris.NCH = 0;
            //    simetris.NCSS = 0;
            //    simetris.NCSE = 1;
            //}
            if (PanelNC.Visible == false)
            {
                simetris.NCH = 0;
                simetris.NCSS = 0;
                simetris.NCSE = 0;
            }
            if (PanelBS.Visible == true)
            {
                //if (RBFin.Checked == true)
                //    simetris.BS = "FIN";
                //else
                //    simetris.BS = "LOG";

                if (RBFin.Checked == true)
                {
                    simetris.BS = "FIN";
                }
                else if (RBLog.Checked == true)
                {
                    simetris.BS = "LOG";
                }
                else
                {
                    simetris.BS = "KAT";
                }
            }
            simetris.CreatedBy = users.UserName;

            if (ddlDefect.SelectedValue == "")
            {
                simetris.Defect = "0";
            }
            else
            {
                simetris.Defect = ddlDefect.SelectedItem.ToString().Trim();
            }
            if (tglprod.Text == "")
            {
                simetris.TglProduksi = Convert.ToDateTime("01-01-1900");
            }
            else
            {
                simetris.TglProduksi = Convert.ToDateTime(tglprod.Text);
            }
            if (t3serahCek.ItemID == simetris.ItemID)
            {
                DisplayAJAXMessage(this, "Kesalahan pendefinisian PartNo, tekan F5 untuk lanjut");
                return;
            }
            //intresult = SimetrisFacade.Insert(simetris);
            T3_SimetrisProcessFacade SimetrisProcessFacade = new T3_SimetrisProcessFacade(t3serahK, arrt3_serahT, rekapK, arrt3_rekapT, simetris);
            strError = SimetrisProcessFacade.Insert();
            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Data tersimpan");
                clearform();
                txtPartnoA.Focus();

                ddlDefect.Items.Clear();
                //ddlDefect.Items.Add(new ListItem("---- Pilih Jenis Defect ----", "0"));

                LoadMesinCutter();
            }
            else
                DisplayAJAXMessage(this, "Simpan data error");
            ClearAutoBS();
        }

        protected int cekpartno(string partno)
        {
            int cek = 0;
            FC_Items itemsT = new FC_Items();
            FC_ItemsFacade itemsTF = new FC_ItemsFacade();
            itemsT = itemsTF.RetrieveByPartNo(partno);
            if (itemsT.ID == 0)
            {
                return 1;
            }

            return cek;
        }
        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewSimetris();
        }

        private void clearlokasiawal()
        {
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtQty1.Text = string.Empty;
            txtPartnoA.Text = string.Empty;
            txtPartnoA.Focus();
        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoA.Text.Trim().Length < 10)
                return;
            FC_Items fC_Items = new FC_Items();
            FC_ItemsFacade fC_ItemsFacade = new FC_ItemsFacade();
            if (txtPartnoA.Text.Trim() != string.Empty)
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoA.Text.Trim());
            txtTebal1.Text = fC_Items.Tebal.ToString();
            txtLebar1.Text = fC_Items.Lebar.ToString();
            txtPanjang1.Text = fC_Items.Panjang.ToString();
            txtPartname1.Text = fC_Items.ItemDesc;
            txtPartnoC.Text = txtPartnoA.Text;
            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            string deptname = string.Empty;
            dept = deptfacade.RetrieveById(users.DeptID);
            if (dept.DeptName.Length >= 8)
                deptname = dept.DeptName.Substring(0, 8).ToUpper();
            else
                deptname = dept.DeptName;
            if ((txtPartnoB.Text.IndexOf("-P-") != -1 || txtPartnoB.Text.IndexOf("-S-") != -1) &&
               (txtPartnoA.Text.IndexOf("-3-") != -1 || txtPartnoA.Text.IndexOf("-W-") != -1 || txtPartnoA.Text.IndexOf("-M-") != -1))
                if (deptname.Trim() == "LOGISTIK")
                {
                    PanelNC.Visible = true;
                    tglprod.Visible = true;
                    lbltglprod.Visible = true;
                }
                else
                {
                    PanelNC.Visible = false;
                }
            LoadDataGridViewtrans();
            if (fC_Items.Tebal == 0)
            {
                clearlokasiawal();
                return;
            }
            AutoCompleteExtender4.ContextKey = txtPartnoA.Text;
            if (txtPartnoB.Text.Trim().Length > 15)
                AutoBS();
            txtLokasi1.Focus();
        }

        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["groupid"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            if (txtLokasi1.Text.Trim().ToUpper() == "S99")
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                txtLokasi1.Focus();
                return;
            }
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi2.Text);
            if (cekLoading > 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            Session["lokasi1"] = t3serah.Lokasi;
            Session["partno1"] = t3serah.Partno;
            Session["itemid1"] = t3serah.ItemID;
            Session["hpp1"] = t3serah.HPP;
            Session["stock1"] = t3serah.Qty;
            Session["luas1"] = t3serah.Lebar * t3serah.Panjang * t3serah.Tebal;
            txtStock1.Text = qty.ToString();
            txtQty1.Text = string.Empty;
            txtQty1.Focus();
        }

        protected void GridViewtrans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pilih")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewtrans.Rows[index];
                clearlokasiawal();
                txtPartnoA.Text = row.Cells[4].Text;
                txtTebal1.Text = row.Cells[6].Text;
                txtLebar1.Text = row.Cells[7].Text;
                txtPanjang1.Text = row.Cells[8].Text;
                txtPartname1.Text = row.Cells[5].Text;
                txtLokasi1.Text = row.Cells[10].Text;
                txtStock1.Text = row.Cells[11].Text;
                txtQty1.Text = string.Empty;
                T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
                T3_Serah t3serah = new T3_Serah();
                t3serah = t3_SerahFacade.RetrieveStockByPartno(row.Cells[4].Text, row.Cells[10].Text);
                int qty = t3serah.Qty;
                Session["serahID"] = t3serah.ID;
                Session["groupid"] = t3serah.GroupID;
                Session["lokid1"] = t3serah.LokID;
                Session["lokasi1"] = t3serah.Lokasi;
                Session["partno1"] = t3serah.Partno;
                Session["itemid1"] = t3serah.ItemID;
                Session["hpp1"] = t3serah.HPP;
                Session["stock1"] = t3serah.Qty;
                Session["luas1"] = t3serah.Tebal * t3serah.Lebar * t3serah.Panjang;

                txtQty1.Focus();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCari.SelectedIndex == 0)
            {
                txtPartnoC.Visible = true;
                txtLokasiC.Visible = false;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
            else
            {
                txtPartnoC.Visible = false;
                txtLokasiC.Visible = true;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
        }
        protected void txtPartnoC_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoC.Text.Trim().Length < 10)
                return;
            LoadDataGridViewtrans();
            AutoCompleteExtender4.ContextKey = txtPartnoC.Text;
        }
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridViewtrans();
        }

        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            string kode = string.Empty;
            Users users = (Users)Session["Users"];
            try
            {
                if (IsNumeric(txtQty1.Text) == false)
                {
                    txtQty1.Text = string.Empty;
                    return;
                }
                T3_SimetrisFacade simF = new T3_SimetrisFacade();


               
               kode = txtPartnoA.Text.Substring(0, 3);
               

                if (txtStock1.Text != string.Empty && Convert.ToInt32(txtStock1.Text) > 0 &&
                    Convert.ToInt32(txtQty1.Text) <= Convert.ToInt32(txtStock1.Text))
                {
                    txtPartnoB.Focus();
                    txtPartnoB.Text = kode + txtPartnoA.Text.Substring(3, 6);
                    txtPartnoB.Focus();
                }
                else
                {
                    txtQty1.Text = string.Empty;
                    txtQty1.Focus();
                }
            }
            catch { }
        }

        private void clearlokasiakhir()
        {
            txtPartnoB.Text = string.Empty;
            txtTebal2.Text = string.Empty;
            txtLebar2.Text = string.Empty;
            txtPanjang2.Text = string.Empty;
            txtPartname2.Text = string.Empty;
            txtPartnoB.Focus();
        }

        protected void txtPartnoB_TextChanged(object sender, EventArgs e)
        {

            if (txtPartnoB.Text.Trim().Length < 10)
                return;
            //if (txtPartnoA.Text.Substring(3, 3) == "-S-" && (txtPartnoB.Text.Substring(3, 3) == "-3-" || txtPartnoB.Text.Substring(3, 3) == "-P-"))
            //{
            //    DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
            //    return;
            //}
            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            string deptname = string.Empty;
            dept = deptfacade.RetrieveById(users.DeptID);
            if (dept.DeptName.Length >= 8)
                deptname = dept.DeptName.Substring(0, 8).ToUpper();
            else
                deptname = dept.DeptName;

            // Tambahan munculin jenis defect
            if (txtPartnoB.Text.IndexOf("-P-") != -1)
            {
                LabelDefect.Visible = true; ddlDefect.Visible = true;
                LoadMasterDefect();
            }

            if ((txtPartnoB.Text.IndexOf("-P-") != -1 || txtPartnoB.Text.IndexOf("-S-") != -1) &&
               (txtPartnoA.Text.IndexOf("-3-") != -1 || txtPartnoA.Text.IndexOf("-W-") != -1 || txtPartnoA.Text.IndexOf("-M-") != -1))
                if (deptname.Trim() == "LOGISTIK")
                {
                    PanelNC.Visible = true;
                    tglprod.Visible = true;
                    lbltglprod.Visible = true;
                }
                else
                {
                    PanelNC.Visible = false;
                }
            if (txtPartnoB.Text.IndexOf("-S-") != -1)
            {
                PanelBS.Visible = true;
            }
            else
            {
                PanelBS.Visible = false;
            }
            if (txtPartnoA.Text != string.Empty && txtPartnoA.Text.Trim() != txtPartnoB.Text.Trim())
            {
                double V1 = 0;
                double V2 = 0;
                int qty = 0;
                FC_Items fC_Items = new FC_Items();
                FC_ItemsFacade fC_ItemsFacade = new FC_ItemsFacade();
                int panjang1 = 0;
                int panjang2 = 0;
                fC_Items = new FC_Items();
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoA.Text.Trim());
                panjang1 = fC_Items.Panjang;
                fC_Items = new FC_Items();
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoB.Text.Trim());
                panjang2 = fC_Items.Panjang;
                if (panjang1 < panjang2)
                {
                    DisplayAJAXMessage(this, "Panjang partno tujuan tidak mencukupi");
                    return;
                }
                if (txtPartnoA.Text.Trim() != string.Empty)
                    fC_Items = fC_ItemsFacade.RetrieveByPartNoIn(txtPartnoB.Text.Trim());
                txtTebal2.Text = fC_Items.Tebal.ToString();
                txtLebar2.Text = fC_Items.Lebar.ToString();
                txtPanjang2.Text = fC_Items.Panjang.ToString();
                txtPartname2.Text = fC_Items.ItemDesc;
                if (fC_Items.GroupID > 0)
                    RBList.SelectedValue = fC_Items.GroupID.ToString();
                //if (txtPartnoA.Text.Substring(1, 3) != txtPartnoB.Text.Substring(1, 3))
                //{
                //    DisplayAJAXMessage(this, "Jenis produk tidak sama");
                //    clearlokasiakhir();
                //    return;
                //}
                if (txtTebal1.Text != txtTebal2.Text)
                {
                    if (txtPartnoA.Text.Contains("-M-") == true && txtPartnoB.Text.Contains("-M-") == true &&
                        Convert.ToDouble(txtTebal1.Text) == 3.5 && Convert.ToDouble(txtTebal2.Text) == 4)
                    {
                        //DisplayAJAXMessage(this, "KW");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Ketebalan produk tidak sama");
                        clearlokasiakhir();
                        return;
                    }
                }
                //V2 = (Convert.ToDouble(txtLebar2.Text) / 1000) * (Convert.ToDouble(txtPanjang2.Text) / 1000);
                //V1 = (Convert.ToDouble(txtLebar1.Text) / 1000) * (Convert.ToDouble(txtPanjang1.Text) / 1000);
                //if (V1 == 0 || V2 == 0)
                //    return;
                //qty = Convert.ToInt32(V1 / V2);
                //qty = Convert.ToInt32 ((Math.Truncate(Convert.ToDecimal(txtLebar1.Text) / Convert.ToDecimal(txtLebar2.Text))) * 
                //    (Math.Truncate(Convert.ToDecimal(txtPanjang1.Text) / Convert.ToDecimal(txtPanjang2.Text))));
                qty = Convert.ToInt32((Math.Round(Convert.ToDecimal(txtLebar1.Text) / Convert.ToDecimal(txtLebar2.Text))) *
                    (Math.Round(Convert.ToDecimal(txtPanjang1.Text) / Convert.ToDecimal(txtPanjang2.Text))));
                if (txtQty1.Text == string.Empty || Convert.ToInt32(txtQty1.Text) == 0)
                {
                    DisplayAJAXMessage(this, "Luas produk tujuan tidak mencukupi");
                    clearlokasiakhir();
                    txtQty1.Focus();
                    return;
                }
                txtQty2.Text = (qty * Convert.ToInt32(txtQty1.Text)).ToString();
                Session["itemid2"] = fC_Items.ID;
                Session["luas2"] = fC_Items.Lebar * fC_Items.Panjang * fC_Items.Tebal;
                if (txtPartnoB.Text.Length < 15)
                    txtPartnoB.Focus();
                else
                    txtLokasi2.Focus();
            }
            else
            {
                //txtPartnoB.Text = txtPartnoA.Text.Substring(0, 9);
                txtPartnoB.Focus();
                DisplayAJAXMessage(this, "Partno tujuan tidak boleh sama dengan partno awal, gunakan menu proses mutasi lokasi untuk melakukan proses ini");
            }

            if (txtPartnoB.Text == string.Empty)
                txtPartnoB.Focus();
            #region pengali
            int kali = 0;
            txtPengali.Text = "1";
            FC_Items items0 = new FC_Items();
            FC_ItemsFacade items0F = new FC_ItemsFacade();
            items0 = items0F.RetrieveByPartNo(txtPartnoA.Text);
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            items = itemsF.RetrieveByPartNo(txtPartnoB.Text);
            if (items.Tebal != items0.Tebal)
            {
                DisplayAJAXMessage(this, "Ketebalan harus sama");
                txtPartnoB.Text = string.Empty;
                txtPartnoB.Focus();
                return;
            }
            txtPengali.Text = ((Math.Truncate(Convert.ToDecimal(items0.Lebar) / Convert.ToDecimal(items.Lebar))) *
                (Math.Truncate(Convert.ToDecimal(items0.Panjang) / Convert.ToDecimal(items.Panjang)))).ToString();
            kali = Convert.ToInt32(txtPengali.Text);
            AutoBS();
            #endregion

        }

        private void LoadMasterDefect()
        {
            MasterDefect domainDefect = new MasterDefect();
            FacadeMasterDefect facadeDefect = new FacadeMasterDefect();
            ArrayList arrDefect = facadeDefect.RetrieveMasterDefect();
            if (arrDefect.Count > 0)
            {
                ddlDefect.Items.Clear();
                ddlDefect.Items.Add(new ListItem("---- Pilih Jenis Defect ----", "0"));
                foreach (MasterDefect def in arrDefect)
                {
                    ddlDefect.Items.Add(new ListItem(def.JenisDefect, def.JenisDefect));
                }
            }

        }

        protected void txtLokasi2_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_Lokasi lokasi = new FC_Lokasi();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            lokasi = lokasifacde.RetrieveByLokasi(txtLokasi2.Text);
            int lokid = lokasi.ID;
            if (lokid == 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi belum terdaftar");
                return;
            }
            else
                txtLokasi2.Text = lokasi.Lokasi;
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi2.Text);
            if (cekLoading > 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoB.Text, txtLokasi2.Text);
            int qty = t3serah.Qty;
            Session["lokid2"] = lokid;
            Session["hpp2"] = t3serah.HPP;
            Session["stock2"] = t3serah.Qty;
            txtStock2.Text = qty.ToString();
            txtQty2.Focus();
        }

        protected void txtQty2_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(txtQty2.Text) == false || txtQty2.Text.Trim() == "0")
            {
                txtQty2.Text = string.Empty;
                return;
            }
            else
                btnTansfer.Focus();

            Users users = (Users)Session["Users"];
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            int cutoption = simF.GetMaxCut250(users.UnitKerjaID);
            int QtyMax = 10000;
            if (cutoption == 1)
            {
                items = itemsF.RetrieveByPartNo(txtPartnoB.Text.Trim());
                if ((items.Tebal == 8 || items.Tebal == 9) && (items.Lebar == 100 || items.Lebar == 200 || items.Lebar == 300) && items.Panjang == 2440)
                {
                    QtyMax = 5000;
                }
                else
                    if ((items.Tebal == 4) && (items.Lebar == 1000) && items.Panjang == 1000)
                {
                    QtyMax = 3000;
                }
                else
                        if ((items.Tebal == 4) && (items.Lebar == 500) && items.Panjang == 1000)
                {
                    QtyMax = 3000;
                }
                if ((items.Tebal == 8 || items.Tebal == 9) && (items.Lebar == 100 || items.Lebar == 200 || items.Lebar == 300) && items.Panjang <= 400)
                {
                    QtyMax = 10000;
                }

                if (Convert.ToInt32(txtQty2.Text) > QtyMax)
                {
                    DisplayAJAXMessage(this, "Jumlah Partno Akhir tidak boleh lebih dari " + QtyMax.ToString().Trim() + " lembar.");
                    txtQty2.Text = string.Empty;
                    txtQty2.Focus();
                    return;
                }
            }
            //if (Convert.ToInt32(txtQty2.Text) > 250)
            //{
            //    DisplayAJAXMessage(this, "Jumlah Potongan tidak boleh lebih dari 250 lembar.");
            //    return;
            //}
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtPartnoB_PreRender(object sender, EventArgs e)
        {
            if (txtPartnoB.Text.Trim().Length < 10)
                return;
            if (txtPartnoA.Text == string.Empty && txtPartnoB.Text == string.Empty)
                txtPartnoA.Focus();
            else
                if (txtStock1.Text != string.Empty)
            {
                if (Convert.ToInt32(txtStock1.Text) == 0)
                    txtLokasi1.Focus();
                else
                    if (txtQty1.Text == string.Empty || Convert.ToInt32(txtQty1.Text) == 0)
                    txtQty1.Focus();
                else
                    txtPartnoB.Focus();
            }
        }

        protected void txtLokasi1_PreRender(object sender, EventArgs e)
        {
            if (txtPartnoA.Text.Trim().Length < 15)
                return;
            if (txtLokasi1.Text == string.Empty)
                txtLokasi1.Focus();
        }
        protected void txtQty1_PreRender(object sender, EventArgs e)
        {

            if (txtQty1.Text == string.Empty && txtLokasi1.Text != string.Empty)
                txtQty1.Focus();

        }

        protected void txtLokasi2_PreRender(object sender, EventArgs e)
        {
            if (txtPartnoB.Text.Trim().Length < 15)
                return;
            if (txtLokasi2.Text == string.Empty && txtPartnoA.Text != string.Empty &&
                txtPartnoB.Text.Length > 10 && txtLokasi1.Text != string.Empty && txtQty1.Text != string.Empty)
                txtLokasi2.Focus();
        }

        protected void txtQty2_PreRender(object sender, EventArgs e)
        {
            if (txtPartnoB.Text.Trim().Length < 15)
                return;
            if (txtQty2.Text == string.Empty && txtLokasi2.Text != string.Empty &&
                txtLokasi1.Text != string.Empty && txtQty1.Text != string.Empty)
                txtQty2.Focus();
            else
                if (txtPartnoB.Text != string.Empty && txtLokasi2.Text != string.Empty)
                btnTansfer.Focus();
        }

        //protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        //{
        //    clearform();
        //    txtPartnoA.Focus();
        //}

       
        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == false)
                Panel3.Visible = false;
            else
                Panel3.Visible = true;
        }
        protected void ClearAutoBS()
        {
            LCQtyBS1.Text = string.Empty;
            LCPartnoBS1.Text = string.Empty;
            LCQtyBS2.Text = string.Empty;
            LCPartnoBS2.Text = string.Empty;
            LCQtyBS3.Text = string.Empty;
            LCPartnoBS3.Text = string.Empty;
            LCQtyBS4.Text = string.Empty;
            LCPartnoBS4.Text = string.Empty;
            LCLokBS1.Text = string.Empty;
            LCLokBS2.Text = string.Empty;
            LCLokBS3.Text = string.Empty;
            LCLokBS4.Text = string.Empty;
        }

        protected void AutoBS()
        {
            ClearAutoBS(); Users users = (Users)Session["Users"];
            try
            {
                FC_Items items = new FC_Items();
                FC_Items itemsAsal = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
                int kali = Convert.ToInt32(txtPengali.Text);
                items = itemsF.RetrieveByPartNo(txtPartnoB.Text);
                itemsAsal = itemsF.RetrieveByPartNo(txtPartnoA.Text);
                string partnoBS1 = string.Empty;
                string partnoBS2 = string.Empty;
                string partnoBS3 = string.Empty;
                string partnoBS4 = string.Empty;
                int panjangBS1 = 0;
                int panjangBS2 = 0;
                int panjangBS3 = 0;
                int panjangBS4 = 0;
                int lebarBS1 = 0;
                int lebarBS2 = 0;
                int lebarBS3 = 0;
                int lebarBS4 = 0;
                int luassisa = 0;

                #region Cara Potong I
                if (RBPotong1.Checked == true)
                {
                    luassisa = (itemsAsal.Lebar * itemsAsal.Panjang) - (items.Lebar * items.Panjang * kali);
                    lebarBS1 = 20;
                    panjangBS1 = itemsAsal.Panjang;
                    lebarBS2 = itemsAsal.Lebar - (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar)))) - lebarBS1;
                    panjangBS2 = itemsAsal.Panjang;
                    lebarBS3 = 40;
                    panjangBS3 = (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar))));
                    lebarBS4 = itemsAsal.Panjang - 40 - (items.Panjang *
                        Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                    if (lebarBS4 > 400)
                    {
                        lebarBS4 = 400;
                        lebarBS3 = itemsAsal.Panjang - lebarBS4 - (items.Panjang *
                        Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                    }
                    panjangBS4 = panjangBS3;
                    //decimal total = 0;
                    if (txtPartnoB.Text == string.Empty)
                        return;

                    if (ChkConvertBS.Checked == true)
                    {
                        if (luassisa <= 0)
                            return;
                        if (panjangBS1 > 0 && lebarBS1 > 0)
                            partnoBS1 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);
                        //LCPartnoBS1.Text = partnoBS1;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS1.Text = partnoBS1.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS1.Text = partnoBS1; }

                        if (LCPartnoBS1.Text != string.Empty)
                        {
                            LCQtyBS1.Text = txtQty1.Text;
                            if (lebarBS1 > 0)
                                if (lebarBS1 < 400)
                                    LCLokBS1.Text = "BSAUTO";
                                else
                                    LCLokBS1.Text = "BSAUTO";
                        }

                        luassisa = luassisa - (lebarBS1 * panjangBS1);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS2 > 0 && lebarBS2 > 0)
                            partnoBS2 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS2.Text = partnoBS2;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS2.Text = partnoBS2.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS2.Text = partnoBS2; }


                        if (LCPartnoBS2.Text != string.Empty)
                        {
                            LCQtyBS2.Text = txtQty1.Text;
                            if (lebarBS2 > 0)
                                if (lebarBS2 < 400)
                                    LCLokBS2.Text = "BSAUTO";
                                else
                                    LCLokBS2.Text = "BSAUTO";
                        }
                        luassisa = luassisa - (lebarBS2 * panjangBS2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS3 > 0 && lebarBS3 > 0)
                            partnoBS3 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS3.Text = partnoBS3;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS3.Text = partnoBS3.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS3.Text = partnoBS3; }

                        if (LCPartnoBS3.Text != string.Empty)
                        {
                            LCQtyBS3.Text = txtQty1.Text;
                            if (lebarBS3 > 0)
                                if (lebarBS3 < 400)
                                    LCLokBS3.Text = "BSAUTO";
                                else
                                    LCLokBS3.Text = "BSAUTO";
                        }
                        luassisa = luassisa - (lebarBS3 * panjangBS3);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS4 > 0 && lebarBS4 > 0)
                            partnoBS4 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS4.Text = partnoBS4;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS4.Text = partnoBS4.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS4.Text = partnoBS4; }

                        if (LCPartnoBS4.Text != string.Empty)
                        {
                            LCQtyBS4.Text = txtQty1.Text;
                            if (lebarBS4 > 0)
                                if (lebarBS4 < 400)
                                    LCLokBS4.Text = "BSAUTO";
                                else
                                    LCLokBS4.Text = "BSAUTO";
                        }
                    }
                }
                #endregion
                #region Cara Potong II
                else if (RBPotong2.Checked == true)
                {
                    luassisa = (itemsAsal.Lebar * itemsAsal.Panjang) - (items.Lebar * items.Panjang * kali);
                    lebarBS1 = 20;
                    panjangBS1 = itemsAsal.Panjang;
                    lebarBS2 = itemsAsal.Lebar - (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar)))) - lebarBS1;
                    panjangBS2 = itemsAsal.Panjang;
                    lebarBS3 = 200;
                    panjangBS3 = items.Lebar;
                    lebarBS4 = itemsAsal.Panjang - (items.Panjang * kali) - lebarBS3;
                    panjangBS4 = panjangBS3;

                    if (txtPartnoB.Text == string.Empty)
                        return;

                    if (ChkConvertBS.Checked == true)
                    {
                        if (luassisa <= 0)
                            return;
                        if (panjangBS1 > 0 && lebarBS1 > 0)
                            partnoBS1 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS1.Text = partnoBS1;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS1.Text = partnoBS1.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS1.Text = partnoBS1; }

                        if (LCPartnoBS1.Text != string.Empty)
                        {
                            LCQtyBS1.Text = txtQty1.Text;
                            if (lebarBS1 > 0)
                                if (lebarBS1 < 400)
                                    LCLokBS1.Text = "BSAUTO";
                                else
                                    LCLokBS1.Text = "BSAUTO";
                        }
                        luassisa = luassisa - ((lebarBS1 * panjangBS1) * 2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS2 > 0 && lebarBS2 > 0)
                            partnoBS2 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS2.Text = partnoBS2;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS2.Text = partnoBS2.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS2.Text = partnoBS2; }

                        if (LCPartnoBS2.Text != string.Empty)
                        {
                            LCQtyBS2.Text = txtQty1.Text;
                            if (lebarBS2 > 0)
                                if (lebarBS2 < 400)
                                    LCLokBS2.Text = "BSAUTO";
                                else
                                    LCLokBS2.Text = "BSAUTO";
                        }
                        luassisa = luassisa - (lebarBS2 * panjangBS2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS3 > 0 && lebarBS3 > 0)
                            partnoBS3 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS3.Text = partnoBS3;

                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS3.Text = partnoBS3.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS3.Text = partnoBS3; }

                        if (LCPartnoBS3.Text != string.Empty)
                        {
                            LCQtyBS3.Text = txtQty1.Text;
                            if (lebarBS3 > 0)
                                if (lebarBS3 < 400)
                                    LCLokBS3.Text = "BSAUTO";
                                else
                                    LCLokBS3.Text = "BSAUTO";
                        }
                        luassisa = luassisa - (lebarBS3 * panjangBS3);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS4 > 0 && lebarBS4 > 0)
                            partnoBS4 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);

                        //LCPartnoBS4.Text = partnoBS4;
                        /** Ditambahkan Base On WO Accounting **/
                        if (users.DeptID == 28) { LCPartnoBS4.Text = partnoBS4.Trim() + "-" + "KAT"; }
                        else { LCPartnoBS4.Text = partnoBS4; }

                        if (LCPartnoBS4.Text != string.Empty)
                        {
                            LCQtyBS4.Text = txtQty1.Text;
                            if (lebarBS4 > 0)
                                if (lebarBS4 < 400)
                                    LCLokBS4.Text = "BSAUTO";
                                else
                                    LCLokBS4.Text = "BSAUTO";
                        }
                    }                  

                   
                }
                #endregion
                #region Cara Potong III                  
                /** WO - Finishing Target End Januari 2022 :  WO-IT-C0050821
                 *  added by : Beny **/
                else if (RBPotong3.Checked == true)
                {
                    string PartnoAwal = string.Empty;
                    string PartnoAkhir = string.Empty;
                    string PartnoBSAuto = string.Empty;
                    string PartnoBSAuto1 = string.Empty;
                    string PartnoBSAuto2 = string.Empty;
                    string KodeAwal = string.Empty;

                    int LebarAwal = 0; int PanjangAwal = 0; int PanjangAkhir = 0; int Tebal1 = 0; int LebarAkhir = 0;


                    PartnoAwal = txtPartnoA.Text; PartnoAkhir = txtPartnoB.Text;

                    LebarAwal = int.Parse(txtPartnoA.Text.Substring(9, 4));
                    LebarAkhir = int.Parse(txtPartnoB.Text.Substring(9, 4));

                    PanjangAwal = int.Parse(txtPartnoA.Text.Substring(13, 4));
                    PanjangAkhir = int.Parse(txtPartnoB.Text.Substring(13, 4));

                    Tebal1 = int.Parse(PartnoAwal.Substring(6, 3)) / 10;

                    KodeAwal = PartnoAwal.Substring(0, 4).Trim() + "S-";

                    string kode = string.Empty;
                    if (txtLokasi2.Text.Contains("KAT"))
                    {
                        kode = txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17) + "-KAT";
                    }
                    else
                    {
                        kode = txtPartnoB.Text.Substring(17, txtPartnoB.Text.Length - 17);
                    }


                    int SisaLebar1 = 0;
                    int SisaLebar2 = 0; int SisaLebar3 = 0;
                    int LebarBS1 = 0; int PanjangBS1 = 0; int LebarBS2 = 0; int PanjangBS2 = 0; int PanjangBS3 = 0;
                    int LebarBaru1 = 0;
                    int PanjangBaru1 = 0;
                    int SisaPanjangBS2 = 0; int SisaPanjangBS3 = 0;
                    string LebarBSAuto1 = string.Empty; string PanjangBSAuto1 = string.Empty; string TebalBSAuto1 = string.Empty;
                    string LebarBSAuto2 = string.Empty; string PanjangBSAuto2 = string.Empty;
                    SisaLebar1 = LebarAwal - LebarAkhir;

                    /** Lebar Max 400 yg di hitung sebagai BS Auto **/
                    if (LebarAwal != LebarAkhir || PanjangAwal != PanjangAkhir)
                    {
                        if (SisaLebar1 > 0 && SisaLebar1 <= 400)
                        {
                            /** BS Auto I **/
                            LebarBS1 = SisaLebar1; PanjangBS1 = PanjangAwal;

                            /** Tebal I **/
                            if (Tebal1.ToString().Length == 1) { TebalBSAuto1 = "0" + Tebal1.ToString().Trim() + "0"; }
                            else if (Tebal1.ToString().Length == 2) { TebalBSAuto1 = "0" + Tebal1.ToString().Trim(); }
                            else if (Tebal1.ToString().Length == 3) { TebalBSAuto1 = Tebal1.ToString().Trim(); }

                            /** Lebar I **/
                            if (LebarBS1.ToString().Length == 4) { LebarBSAuto1 = LebarBS1.ToString().Trim(); }
                            else if (LebarBS1.ToString().Length == 3) { LebarBSAuto1 = "0" + LebarBS1.ToString().Trim(); }
                            else if (LebarBS1.ToString().Length == 2) { LebarBSAuto1 = "00" + LebarBS1.ToString().Trim(); }
                            else if (LebarBS1.ToString().Length == 1) { LebarBSAuto1 = "000" + LebarBS1.ToString().Trim(); }

                            /** Panjang I **/
                            if (PanjangBS1.ToString().Length == 4) { PanjangBSAuto1 = PanjangBS1.ToString().Trim(); }
                            else if (PanjangBS1.ToString().Length == 3) { PanjangBSAuto1 = "0" + PanjangBS1.ToString().Trim(); }
                            else if (PanjangBS1.ToString().Length == 2) { PanjangBSAuto1 = "00" + PanjangBS1.ToString().Trim(); }
                            else if (PanjangBS1.ToString().Length == 1) { PanjangBSAuto1 = "000" + PanjangBS1.ToString().Trim(); }

                            /** Partno BS Auto I **/
                            PartnoBSAuto1 = KodeAwal + TebalBSAuto1 + LebarBSAuto1 + PanjangBSAuto1 + kode;
                            LCQtyBS1.Text = txtQty1.Text; ;

                        }

                        LebarBaru1 = LebarAwal - SisaLebar1;
                        if (LebarBaru1 > LebarAkhir)
                        {

                        }
                        else
                        {
                            PanjangBS2 = Convert.ToInt32(PanjangAwal) - Convert.ToInt32(PanjangAkhir);
                            SisaPanjangBS2 = Convert.ToInt32(PanjangBS2) - Convert.ToInt32(PanjangAkhir);
                            if (Convert.ToInt32(PanjangBS2) > Convert.ToInt32(PanjangAkhir))
                            {
                                SisaPanjangBS3 = Convert.ToInt32(PanjangBS2) - Convert.ToInt32(PanjangAkhir);
                                if (Convert.ToInt32(SisaPanjangBS3) > Convert.ToInt32(PanjangAkhir))
                                {

                                }
                                else
                                {
                                    /** BS Auto II **/
                                    LebarBS2 = SisaPanjangBS2; PanjangBS2 = LebarBaru1;

                                    /** Lebar II **/
                                    if (LebarBS2.ToString().Length == 4) { LebarBSAuto2 = LebarBS2.ToString().Trim(); }
                                    else if (LebarBS2.ToString().Length == 3) { LebarBSAuto2 = "0" + LebarBS2.ToString().Trim(); }
                                    else if (LebarBS2.ToString().Length == 2) { LebarBSAuto2 = "00" + LebarBS2.ToString().Trim(); }
                                    else if (LebarBS2.ToString().Length == 1) { LebarBSAuto2 = "000" + LebarBS2.ToString().Trim(); }

                                    /** Panjang II **/
                                    if (PanjangBS2.ToString().Length == 4) { PanjangBSAuto2 = PanjangBS2.ToString().Trim(); }
                                    else if (PanjangBS2.ToString().Length == 3) { PanjangBSAuto2 = "0" + PanjangBS2.ToString().Trim(); }
                                    else if (PanjangBS2.ToString().Length == 2) { PanjangBSAuto2 = "00" + PanjangBS2.ToString().Trim(); }
                                    else if (PanjangBS2.ToString().Length == 1) { PanjangBSAuto2 = "000" + PanjangBS2.ToString().Trim(); }

                                    /** Partno BS Auto II **/
                                    PartnoBSAuto2 = KodeAwal + TebalBSAuto1 + LebarBSAuto2 + PanjangBSAuto2 + kode;
                                    LCQtyBS2.Text = txtQty1.Text; ;
                                }
                            }
                            else
                            {
                                /** BS Auto II **/
                                LebarBS2 = PanjangBS2; PanjangBS2 = LebarBaru1;

                                /** Lebar II **/
                                if (LebarBS2.ToString().Length == 4) { LebarBSAuto2 = LebarBS2.ToString().Trim(); }
                                else if (LebarBS2.ToString().Length == 3) { LebarBSAuto2 = "0" + LebarBS2.ToString().Trim(); }
                                else if (LebarBS2.ToString().Length == 2) { LebarBSAuto2 = "00" + LebarBS2.ToString().Trim(); }
                                else if (LebarBS2.ToString().Length == 1) { LebarBSAuto2 = "000" + LebarBS2.ToString().Trim(); }

                                /** Panjang II **/
                                if (PanjangBS2.ToString().Length == 4) { PanjangBSAuto2 = PanjangBS2.ToString().Trim(); }
                                else if (PanjangBS2.ToString().Length == 3) { PanjangBSAuto2 = "0" + PanjangBS2.ToString().Trim(); }
                                else if (PanjangBS2.ToString().Length == 2) { PanjangBSAuto2 = "00" + PanjangBS2.ToString().Trim(); }
                                else if (PanjangBS2.ToString().Length == 1) { PanjangBSAuto2 = "000" + PanjangBS2.ToString().Trim(); }

                                /** Partno BS Auto II **/
                                PartnoBSAuto2 = KodeAwal + TebalBSAuto1 + LebarBSAuto2 + PanjangBSAuto2 + kode;
                                LCQtyBS2.Text = txtQty1.Text; ;
                            }
                        }
                    }

                    string PartnoBSAutoo1 = LebarBS1.ToString() + "" + PanjangBS1.ToString();
                    string PartnoBSAutoo2 = PanjangBS2.ToString() + "" + PanjangBS3.ToString();

                    LCPartnoBS1.Text = PartnoBSAuto1;
                    LCPartnoBS2.Text = PartnoBSAuto2;

                    LCLokBS1.Text = "BSAUTO";
                    LCLokBS2.Text = "BSAUTO";
                }
                #endregion
            }
            catch
            {
            }
        }

        protected void DatePicker1_PreRender(object sender, EventArgs e)
        {

        }

        protected void RBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void ChkConvertBS_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConvertBS.Checked == true)
            {
                PanelPotong.Enabled = true;
                AutoBS();
            }
            else
            {
                ClearAutoBS();
                PanelPotong.Enabled = false;
            }
        }
        protected void RBPotong1_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPotong1.Checked == true)
                AutoBS();
        }
        protected void RBPotong2_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPotong2.Checked == true)
                AutoBS();
        }
        /** added by : Beny u/ Citerueup saja **/
        protected void RBPotong3_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPotong3.Checked == true)
                AutoBS();
        }
        protected void RBNCSortir_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBNCSortir.Checked == true)
            //{
            //    PanelNCSortir.Visible = true;
            //}
        }
        protected void RBNCHandling_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBNCHandling.Checked == true)
            //{
            //    PanelNCSortir.Visible = false ;
            //}
        }
        protected void RBNonNC_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBNonNC.Checked == true)
            //{
            //    PanelNCSortir.Visible = false;
            //}
        }
        protected void GridViewSimetris_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            if (e.CommandName == "hapus")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = gvr.RowIndex;
                GridViewRow row = GridViewSimetris.Rows[index];
                ID = Int32.Parse(row.Cells[0].Text);
                if (Int32.Parse(row.Cells[7].Text) > GetStockLokasi(row.Cells[5].Text, row.Cells[6].Text))
                {
                    DisplayAJAXMessage(this, "Cancel simetris tidak bisa dilakukan, karena stock partno : " + row.Cells[5].Text +
                        " di lokasi : " + row.Cells[6].Text + " tidak mencukupi (stock =" + GetStockLokasi(row.Cells[5].Text, row.Cells[6].Text).ToString() + ")");
                    return;
                }
                CancelSimetris(ID);
                LoadDataGridViewSimetris();
            }
        }
        private int GetStockLokasi(string partno, string lokasi)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select qty from t3_serah where ItemID in (select ID from fc_items " +
                "where partno='" + partno + "') and LokID in (select ID from fc_lokasi where lokasi='" + lokasi + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["qty"].ToString());
                }
            }
            return result;
        }
        private void CancelSimetris(int ID)
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "declare @cutID int " +
                "set @cutid=" + ID + " " +
                "update T3_Simetris set rowstatus=-1 where ID=@cutid " +
                "update T3_Rekap set rowstatus=-1 where CutID=@cutid " +
                "create table #tempSerah(lokid int,itemid int,qtyin int,qtyout int)  " +
                " declare @lokid int " +
                " declare @itemid int " +
                " declare @qtyin int " +
                " declare @qtyout int " +
                " declare kursor cursor for  " +
                " select lokid,itemid,qtyin,qtyout from T3_Rekap  where  CutID=@cutid " +
                " open kursor  " +
                " FETCH NEXT FROM kursor  " +
                " INTO  @lokid ,@itemid ,@qtyin,@qtyout " +
                " WHILE @@FETCH_STATUS = 0  " +
                " begin  " +
                " if @qtyin >0  " +
                "begin " +
                "update t3_serah set qty=qty-@qtyin where lokid=@lokid and itemid=@itemid " +
                "end " +
                "else " +
                "begin " +
                "update t3_serah set qty=qty+@qtyout where lokid=@lokid and itemid=@itemid " +
                "end  " +
                " FETCH NEXT FROM kursor  " +
                " INTO @lokid ,@itemid ,@qtyin,@qtyout " +
                " END  " +
                " CLOSE kursor  " +
                " DEALLOCATE kursor " +
                " drop table #tempSerah";
            SqlDataReader sdr = zl.Retrieve();

        }

    }

    public class MasterDefect
    {
        public string JenisDefect { get; set; }
        public string NamaMesinCutter { get; set; }
        public int ID { get; set; }
        public int Lock { get; set; }
    }

    public class FacadeMasterDefect
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private MasterDefect pm = new MasterDefect();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public ArrayList RetrieveMasterDefect()
        {
            arrData = new ArrayList();
            string strSQL =
            " select ID,DefName JenisDefect from Def_MasterDefect where RowStatus >-1 order by Urutan ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new MasterDefect
                    {
                        ID = int.Parse(sdr["ID"].ToString()),
                        JenisDefect = sdr["JenisDefect"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveMesinCutter()
        {
            arrData = new ArrayList();
            string strSQL =
            " select ID,NamaMesin NamaMesinCutter from T3_MesinCutter where RowStatus>-1 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new MasterDefect
                    {
                        ID = int.Parse(sdr["ID"].ToString()),
                        NamaMesinCutter = sdr["NamaMesinCutter"].ToString()
                    });
                }
            }
            return arrData;
        }

        public MasterDefect RetrieveData(int Tebal, int Lebar, int Panjang)
        {
            string StrSql = " select ID,Lock from No_PartnoListPlank where Tebal='" + Tebal + "' and Lebar='" + Lebar + "' and Panjang='" + Panjang + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveData(sqlDataReader);
                }
            }

            return new MasterDefect();
        }

        public MasterDefect GenerateObject_RetrieveData(SqlDataReader sqlDataReader)
        {
            pm = new MasterDefect();
            pm.ID = Convert.ToInt32(sqlDataReader["ID"]);
            pm.Lock = Convert.ToInt32(sqlDataReader["Lock"]);

            return pm;
        }
    }
}