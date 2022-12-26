using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using System.Collections;
using Cogs;
using DataAccessLayer;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Factory
{
    public partial class Asimetris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                DatePicker1.SelectedDate = DateTime.Now.AddDays(-1);
                LoadGroupM();
                clearform();

                LoadMesinCutter();
            }
        }

        private void LoadMesinCutter()
        {
            MesinCutter domainMCutter = new MesinCutter();
            FacadeMesinCutter facadeMCutter = new FacadeMesinCutter();
            ArrayList arrMCutter = facadeMCutter.RetrieveMesinCutter();
            if (arrMCutter.Count > 0)
            {
                ddlMCutter.Items.Clear();
                ddlMCutter.Items.Add(new ListItem("---- Pilih Mesin Cutter ----", "0"));
                foreach (MesinCutter msn in arrMCutter)
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
            ArrayList arrAsimetris = new ArrayList();
            Panel2.Enabled = true;
            Session["serahID"] = null;
            Session["lokid1"] = null;
            Session["lokasi1"] = null;
            Session["partno1"] = null;
            Session["ListofAsimetris"] = null;
            Session["itemid1"] = null;
            Session["luas1"] = null;
            Session["stock1"] = null;
            Session["hpp1"] = null;
            arrAsimetris = (ArrayList)Session["ListofAsimetris"];
            GridViewTemp.DataSource = arrAsimetris;
            GridViewTemp.DataBind();
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
            txtQty3.Text = string.Empty;
            LoadDataGridViewtrans();
            LoadDataGridViewASimetris();
        }
        private void clearlokasiakhir()
        {
            txtPartnoB.Text = txtPartnoB.Text.Substring(0, 9);
            txtTebal2.Text = string.Empty;
            txtLebar2.Text = string.Empty;
            txtPanjang2.Text = string.Empty;
            txtPartname2.Text = string.Empty;
            txtLokasi2.Text = string.Empty;
            txtStock2.Text = string.Empty;
            txtQty2.Text = string.Empty;
            txtQty3.Text = string.Empty;
            Session["lokid2"] = null;
            Session["luas2"] = null;
            Session["stock2"] = null;
            Session["hpp2"] = null;
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
            Session["arrT3Serah"] = arrT3Serah;
            GridViewtrans.DataSource = arrT3Serah;
            GridViewtrans.DataBind();
        }

        private void LoadDataGridViewASimetris()
        {
            ArrayList arrT3ASimetris = new ArrayList();
            T3_AsimetrisFacade T3ASAimetris = new T3_AsimetrisFacade();
            arrT3ASimetris = T3ASAimetris.RetrieveBytgl(DatePicker1.SelectedDate.ToString("yyyyMMdd"));
            GridViewAsimetris.DataSource = arrT3ASimetris;
            GridViewAsimetris.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            if (ddlMCutter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Anda belum menentukan pilihan Mesin Cutter !!");
                return;
            }

            ddlMCutter.Enabled = false;

            T3_Asimetris asimetris = new T3_Asimetris();
            ArrayList arrAsimetris = new ArrayList();

            Users users = (Users)Session["Users"];
            T3_Serah t3serah = new T3_Serah();
            T3_Rekap rekap = new T3_Rekap();
            FC_Items items = new FC_Items();

            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoB.Text, txtLokasi2.Text);
            int qty = t3serah.Qty;
            Session["hpp2"] = t3serah.HPP;
            Session["luas2"] = t3serah.Panjang * t3serah.Lebar;
            Session["stock2"] = t3serah.Qty;
            if (txtPartnoB.Text == string.Empty || txtQty3.Text == string.Empty || RBList.SelectedIndex == -1)
            {
                DisplayAJAXMessage(this, "Input Data belum lengkap");
                return;
            }
            if (txtPartnoA.Text.Substring(3, 3) == "-S-" && (txtPartnoB.Text.Substring(3, 3) == "-3-" || txtPartnoB.Text.Substring(3, 3) == "-P-"))
            {
                DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
                return;
            }
            if (txtPartnoB.Text.Substring(3, 3) == "-1-")
            {
                DisplayAJAXMessage(this, "Partno tahap 1 tidak diizinkan.");
                return;
            }
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DatePicker1.SelectedDate.Year;
            int Bulan = DatePicker1.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            if (Session["ListofAsimetris"] != null)
            {
                arrAsimetris = (ArrayList)Session["ListofAsimetris"];
                foreach (T3_Asimetris cekasimetris in arrAsimetris)
                {
                    if (txtPartnoB.Text.Trim() == cekasimetris.PartnoOut)
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoB.Text.Trim() + " sudah di input");
                        clearlokasiakhir();
                        return;
                    }
                }
            }
            decimal lastAvgHPP1 = decimal.Parse(Session["hpp1"].ToString());
            decimal laststock1 = Convert.ToDecimal(Session["stock1"].ToString());
            decimal lastAvgHPP2 = decimal.Parse(Session["hpp2"].ToString());
            decimal laststock2 = Convert.ToDecimal(Session["stock2"].ToString());
            decimal crntHPP2 = decimal.Parse(Session["hpp2"].ToString());
            decimal luas1 = decimal.Parse(Session["luas1"].ToString());
            decimal luas2 = decimal.Parse(Session["luas2"].ToString());

            int stock = SerahFacade.GetStock(int.Parse(Session["lokid2"].ToString()), int.Parse(Session["itemid2"].ToString()));
            asimetris.SA = stock;
            asimetris.SerahID = int.Parse(Session["serahid"].ToString());
            asimetris.ItemIDIn = int.Parse(Session["itemid1"].ToString());
            asimetris.LokIDIn = int.Parse(Session["lokid1"].ToString());
            asimetris.QtyIn = Convert.ToInt32(txtQty1.Text);
            asimetris.ItemIDOut = int.Parse(Session["itemid2"].ToString());
            asimetris.LokIDOut = int.Parse(Session["lokid2"].ToString());
            asimetris.QtyOut = Convert.ToInt32(txtQty3.Text); ;
            asimetris.PartnoIn = Session["partno1"].ToString();
            asimetris.LokasiIn = Session["lokasi1"].ToString();
            asimetris.QtyIn = Convert.ToInt16(txtQty1.Text);
            asimetris.GroupID = Convert.ToInt32(RBList.SelectedValue);
            asimetris.GroupName = RBList.SelectedItem.Text;
            asimetris.PartnoOut = txtPartnoB.Text;
            asimetris.LokasiOut = txtLokasi2.Text;
            asimetris.TglTrans = DatePicker1.SelectedDate.Date;
            asimetris.MCutter = ddlMCutter.SelectedItem.ToString().Trim();


            decimal AvgHPP = 0;
            decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
            if (lastAvgHPP2 > 0 || laststock2 > 0)
            {
                AvgHPP = ((lastAvgHPP2 * laststock2) + (HPPnewItem * Convert.ToInt32(txtQty3.Text))) / (laststock2 + Convert.ToInt32(txtQty3.Text));
            }
            else
            {
                AvgHPP = HPPnewItem;
            }
            asimetris.HPP = AvgHPP;
            double luas = 0;
            double sisaluas = 0;
            luas = Convert.ToInt16(txtQty3.Text) * (Convert.ToDouble(txtLebar2.Text) / 1000) * (Convert.ToDouble(txtPanjang2.Text) / 1000);
            asimetris.Luas = Convert.ToDecimal(luas);
            sisaluas = Convert.ToDouble(txtluas.Text) - Math.Round(luas, 4);

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

            #region Retrieve Partno bukan Listplank added 08 Agustus 2019 By Beny
            MesinCutter mc = new MesinCutter();
            FacadeMesinCutter fmc = new FacadeMesinCutter();
            mc = fmc.RetrieveData(Convert.ToInt32(tebal2), lebar2, panjang2);
            #endregion

            // Off 08 Agustus 2019 By Beny 
            //if (tebal1 >= 8 && tebal1 <= 9 && panjang1 >= 2200 && lebar1 > 1000) : Logic Lama

            // Added 08 Agustus 2019 By Beny
            if ((tebal1 >= 8 && tebal1 <= 9 && panjang1 >= 2200 && lebar1 > 1000 && mc.Lock > 0 && mc.ID > 0)
                 || (tebal1 == 9 && panjang1 >= 2200 && lebar1 > 1000 && mc.ID == 0))
            {
                if (lebar2 <= 300 && lebar2 >= 100)
                {
                    DisplayAJAXMessage(this, "Lakukan proses ini pada inputan proses listplank");
                    return;
                }
            }
            if (sisaluas < 0)
            {
                txtQty2.Text = string.Empty;
                txtQty3.Text = string.Empty;
                DisplayAJAXMessage(this, "Luas produk tidak mencukupi");
                return;
            }
            else
            {
                arrAsimetris.Add(asimetris);
                Session["ListofAsimetris"] = arrAsimetris;
                GridViewTemp.DataSource = arrAsimetris;
                GridViewTemp.DataBind();
                Panel2.Enabled = false;
            }
            txtluas.Text = (sisaluas).ToString();
            if (LCPartnoBS1.Text.Length < 15)
                AutoBS();
            clearlokasiakhir();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            #region Verifikasi Closing Periode
            /**
                 * check closing periode saat ini
                 * added on 13-08-2014
                 */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DatePicker1.SelectedDate.Year;
            int Bulan = DatePicker1.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            if (txtPartnoA.Text == string.Empty || txtPartnoB.Text == string.Empty)
            {
                clearform();
                ClearAutoBS();
                return;
            }
            T3_Asimetris asimetris = new T3_Asimetris();
            ArrayList arrAsimetris = new ArrayList();

            T3_Asimetris t3asimetris = new T3_Asimetris();
            T3_AsimetrisFacade t3asimetrisfacade = new T3_AsimetrisFacade();
            Users users = (Users)Session["Users"];
            T3_Serah t3serahK = new T3_Serah();
            T3_Rekap rekapK = new T3_Rekap();
            T3_Serah t3serahT = new T3_Serah();
            T3_Rekap rekapT = new T3_Rekap();
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            if (txtPartnoA.Text == string.Empty || txtPartnoB.Text == string.Empty)
                if (txtPartnoA.Text.Substring(3, 3) == "-S-" && (txtPartnoB.Text.Substring(3, 3) == "-3-" || txtPartnoB.Text.Substring(3, 3) == "-P-"))
                {
                    DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
                    return;
                }
            if (txtPartnoB.Text.Substring(3, 3) == "-1-")
            {
                DisplayAJAXMessage(this, "Partno tahap 1 tidak diizinkan.");
                return;
            }
            int intdocno = t3asimetrisfacade.GetDocNo(DatePicker1.SelectedDate.Date) + 1;
            string docno = "A" + DatePicker1.SelectedDate.ToString("yy") + DatePicker1.SelectedDate.ToString("MM") + intdocno.ToString().PadLeft(4, '0');
            if (Session["ListofAsimetris"] != null)
            {
                arrAsimetris = (ArrayList)Session["ListofAsimetris"];
                if (arrAsimetris.Count < 2)
                {
                    DisplayAJAXMessage(this, "Jumlah transaksi minimal 2 item");
                    return;
                }
                int awalID = 0;
                t3serahK.Flag = "kurang";
                t3serahK.ItemID = int.Parse(Session["itemid1"].ToString());
                t3serahK.GroupID = int.Parse(Session["groupid"].ToString());
                t3serahK.ID = int.Parse(Session["serahid"].ToString());
                t3serahK.LokID = int.Parse(Session["lokid1"].ToString());
                t3serahK.Qty = Convert.ToInt32(txtQty1.Text);
                t3serahK.CreatedBy = users.UserName;
                int stock = 0;
                //stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
                T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
                T3_Serah t3serah = new T3_Serah();
                t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
                stock = SerahFacade.GetStock(t3serah.LokID, t3serah.ItemID);

                if (stock - Convert.ToInt32(txtQty1.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                    return;
                }

                //intresult = SerahFacade.Insert(t3serah);
                awalID = t3serahK.ID;
                string keterangan = string.Empty;

                rekapK.DestID = 0;
                rekapK.GroupID = int.Parse(Session["groupid"].ToString());
                rekapK.SerahID = int.Parse(Session["serahid"].ToString());
                rekapK.T1serahID = 0;
                rekapK.LokasiID = int.Parse(Session["lokid1"].ToString());
                rekapK.ItemIDSer = int.Parse(Session["itemid1"].ToString());
                rekapK.TglTrm = DatePicker1.SelectedDate.Date;
                rekapK.QtyInTrm = 0;
                rekapK.QtyOutTrm = Convert.ToInt32(txtQty1.Text);
                rekapK.CreatedBy = users.UserName;
                rekapK.Keterangan = keterangan;
                rekapK.HPP = decimal.Parse(Session["hpp1"].ToString());
                //stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
                rekapK.SA = stock;
                rekapK.Process = "Asimetris";
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
                ada = LokasiF.check("S99");
                if (ada == 0)
                {
                    LokasiBSBuang.LokTypeID = 3;
                    LokasiBSBuang.Lokasi = "S99";
                    LokasiF.Insert(LokasiBSBuang);
                }
                LokasiBSBuang = LokasiF.RetrieveByLokasi("S99");
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
                            asimetris = new T3_Asimetris();
                            stock = 0;
                            stock = SerahFacade.GetStock(LokasiBSID, LokasiBSID);
                            asimetris.SA = stock;
                            asimetris.SerahID = int.Parse(Session["serahid"].ToString());
                            asimetris.ItemIDIn = int.Parse(Session["itemid1"].ToString());
                            asimetris.LokIDIn = int.Parse(Session["lokid1"].ToString());
                            asimetris.QtyIn = 0;
                            asimetris.ItemIDOut = newitemID;
                            asimetris.LokIDOut = LokasiBSID;
                            asimetris.QtyOut = Convert.ToInt32(LCQtyBS1.Text); ;
                            asimetris.PartnoIn = Session["partno1"].ToString();
                            asimetris.LokasiIn = Session["lokasi1"].ToString();
                            asimetris.GroupID = Convert.ToInt32(RBList.SelectedValue);
                            asimetris.GroupName = RBList.SelectedItem.Text;
                            asimetris.TglTrans = DatePicker1.SelectedDate.Date;
                            arrAsimetris.Add(asimetris);
                        }
                    }
                    #endregion
                    #region AutoBS2
                    if (LCPartnoBS2.Text != string.Empty && LCPartnoBS2.Text.Trim().Length > 15)
                    {
                        if (cekpartno(LCPartnoBS2.Text) == 1)
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
                            newitemID = ItemF.Insert(Item);
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
                            asimetris = new T3_Asimetris();
                            stock = 0;
                            stock = SerahFacade.GetStock(LokasiBSID, LokasiBSID);
                            asimetris.SA = stock;
                            asimetris.SerahID = int.Parse(Session["serahid"].ToString());
                            asimetris.ItemIDIn = int.Parse(Session["itemid1"].ToString());
                            asimetris.LokIDIn = int.Parse(Session["lokid1"].ToString());
                            asimetris.QtyIn = 0;
                            asimetris.ItemIDOut = newitemID;
                            asimetris.LokIDOut = LokasiBSID;
                            asimetris.QtyOut = Convert.ToInt32(LCQtyBS2.Text); ;
                            asimetris.PartnoIn = Session["partno1"].ToString();
                            asimetris.LokasiIn = Session["lokasi1"].ToString();
                            asimetris.GroupID = Convert.ToInt32(RBList.SelectedValue);
                            asimetris.GroupName = RBList.SelectedItem.Text;
                            asimetris.TglTrans = DatePicker1.SelectedDate.Date;
                            arrAsimetris.Add(asimetris);
                        }
                    }
                    #endregion
                    #region AutoBS3
                    if (LCPartnoBS3.Text != string.Empty && LCPartnoBS3.Text.Trim().Length > 15)
                    {
                        if (cekpartno(LCPartnoBS3.Text) == 1)
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
                            newitemID = ItemF.Insert(Item);
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
                            asimetris = new T3_Asimetris();
                            stock = 0;
                            stock = SerahFacade.GetStock(LokasiBSID, LokasiBSID);
                            asimetris.SA = stock;
                            asimetris.SerahID = int.Parse(Session["serahid"].ToString());
                            asimetris.ItemIDIn = int.Parse(Session["itemid1"].ToString());
                            asimetris.LokIDIn = int.Parse(Session["lokid1"].ToString());
                            asimetris.QtyIn = 0;
                            asimetris.ItemIDOut = newitemID;
                            asimetris.LokIDOut = LokasiBSID;
                            asimetris.QtyOut = Convert.ToInt32(LCQtyBS3.Text); ;
                            asimetris.PartnoIn = Session["partno1"].ToString();
                            asimetris.LokasiIn = Session["lokasi1"].ToString();
                            asimetris.GroupID = Convert.ToInt32(RBList.SelectedValue);
                            asimetris.GroupName = RBList.SelectedItem.Text;
                            asimetris.TglTrans = DatePicker1.SelectedDate.Date;
                            arrAsimetris.Add(asimetris);
                        }
                    }
                    #endregion
                    #region AutoBS4
                    if (LCPartnoBS4.Text != string.Empty && LCPartnoBS4.Text.Trim().Length > 15)
                    {
                        if (cekpartno(LCPartnoBS4.Text) == 1)
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
                            newitemID = ItemF.Insert(Item);
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
                            asimetris = new T3_Asimetris();
                            stock = 0;
                            stock = SerahFacade.GetStock(LokasiBSID, LokasiBSID);
                            asimetris.SA = stock;
                            asimetris.SerahID = int.Parse(Session["serahid"].ToString());
                            asimetris.ItemIDIn = int.Parse(Session["itemid1"].ToString());
                            asimetris.LokIDIn = int.Parse(Session["lokid1"].ToString());
                            asimetris.QtyIn = 0;
                            asimetris.ItemIDOut = newitemID;
                            asimetris.LokIDOut = LokasiBSID;
                            asimetris.QtyOut = Convert.ToInt32(LCQtyBS4.Text); ;
                            asimetris.PartnoIn = Session["partno1"].ToString();
                            asimetris.LokasiIn = Session["lokasi1"].ToString();
                            asimetris.GroupID = Convert.ToInt32(RBList.SelectedValue);
                            asimetris.GroupName = RBList.SelectedItem.Text;
                            asimetris.TglTrans = DatePicker1.SelectedDate.Date;
                            arrAsimetris.Add(asimetris);
                        }
                    }
                    #endregion
                }
                #endregion
                T3_AsimetrisProcessFacade AsimetrisProcessFacade = new T3_AsimetrisProcessFacade(t3serahK, arrAsimetris, rekapK, docno,
                    DatePicker1.SelectedDate, users.UserName.Trim());
                string strError = AsimetrisProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    DisplayAJAXMessage(this, "Data tersimpan");
                    clearform();
                    ClearAutoBS();

                    LoadMesinCutter();
                }
            }
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

            LoadDataGridViewtrans();
            if (fC_Items.Tebal == 0)
            {
                clearlokasiawal();
                return;
            }
            AutoCompleteExtender4.ContextKey = txtPartnoA.Text;
            txtLokasi1.Focus();
        }

        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoA.Text.Trim().Length < 10)
                return;
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            if (txtLokasi1.Text.Trim().ToUpper() == "S99")
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                txtLokasi1.Focus();
                return;
            }
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["groupid"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi1.Text);
            if (cekLoading > 0)
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            Session["lokasi1"] = t3serah.Lokasi;
            Session["partno1"] = t3serah.Partno;
            Session["itemid1"] = t3serah.ItemID;
            Session["hpp1"] = t3serah.HPP;
            Session["luas1"] = t3serah.Panjang * t3serah.Lebar;
            Session["stock1"] = t3serah.Qty;
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
                Session["luas1"] = t3serah.Panjang * t3serah.Lebar;
                Session["stock1"] = t3serah.Qty;
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
            LoadDataGridViewtrans();
        }
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridViewtrans();
        }
        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            //try
            //{
            if (txtStock1.Text != string.Empty && Convert.ToInt32(txtStock1.Text) > 0 && Convert.ToInt32(txtQty1.Text) <= Convert.ToInt32(txtStock1.Text))
            {
                txtPartnoB.Focus();
                txtluas.Text = (Convert.ToDouble(txtQty1.Text) * (Convert.ToDouble(txtLebar1.Text) / 1000) * (Convert.ToDouble(txtPanjang1.Text) / 1000)).ToString();
                txtPartnoB.Text = txtPartnoA.Text.Substring(0, 9);
            }
            else
            {
                txtQty1.Text = string.Empty;
                txtQty1.Focus();
            }
            //if (Convert.ToInt32(txtQty1.Text) > 250)
            //{
            //    DisplayAJAXMessage(this, "Jumlah pengeluaran tidak boleh lebih dari 250 lembar.");
            //    txtQty1.Text = string.Empty;
            //    txtQty1.Focus();
            //    return;
            //}
            //T3_SimetrisFacade simF = new T3_SimetrisFacade();
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            //int cutoption = simF.GetMaxCut250(users.UnitKerjaID);
            //if (cutoption == 1)
            //{
            //    if (Convert.ToInt32(txtQty1.Text) > 250)
            //    {
            //        DisplayAJAXMessage(this, "Jumlah pengeluaran tidak boleh lebih dari 250 lembar.");
            //        txtQty1.Text = string.Empty;
            //        txtQty1.Focus();
            //        return;
            //    }
            //}
            int cutoption = simF.GetMaxCut250(users.UnitKerjaID);
            //if (cutoption == 1)
            //{
            //    if (Convert.ToInt32(txtQty1.Text) > 250)
            //    {
            //        DisplayAJAXMessage(this, "Jumlah pengeluaran tidak boleh lebih dari 250 lembar.");
            //        txtQty1.Text = string.Empty;
            //        txtQty1.Focus();
            //        return;
            //    }
            //}
            //}
            //catch
            //{ }
        }

        protected void txtPartnoB_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoB.Text.Trim().Length < 10)
                return;
            if (txtPartnoA.Text.Substring(3, 3) == "-S-" && (txtPartnoB.Text.Substring(3, 3) == "-3-" || txtPartnoB.Text.Substring(3, 3) == "-P-"))
            {
                DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
                return;
            }
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
            if (txtPartnoA.Text != string.Empty && txtPartnoA.Text.Trim() != txtPartnoB.Text.Trim())
            {
                if (txtPartnoA.Text.Trim() != string.Empty)
                    fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoB.Text.Trim());
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
                    DisplayAJAXMessage(this, "Ketebalan produk tidak sama");
                    clearlokasiakhir();
                    return;
                }

                if (txtQty1.Text == string.Empty || Convert.ToInt32(txtQty1.Text) == 0)
                {
                    DisplayAJAXMessage(this, "Quantity awal tidak boleh kosong");
                    clearlokasiakhir();
                    txtQty1.Focus();
                    return;
                }
                if (txtQty1.Text == string.Empty || Convert.ToInt32(txtQty1.Text) == 0)
                {
                    DisplayAJAXMessage(this, "Luas produk tujuan tidak mencukupi");
                    clearlokasiakhir();
                    txtQty1.Focus();
                    return;
                }
                Session["itemid2"] = fC_Items.ID;
                txtLokasi2.Focus();
            }
            else
            {
                txtPartnoB.Text = txtPartnoA.Text.Substring(0, 9);
                txtPartnoB.Focus();
                DisplayAJAXMessage(this, "Partno tujuan tidak boleh sama dengan partno awal, gunakan menu proses mutasi lokasi untuk melakukan proses ini");
            }
            if (txtPartnoB.Text == string.Empty)
                txtPartnoB.Focus();
            int kali = 0;
            if (txtluas.Text != string.Empty)
            {
                //try
                //{
                Double luas2 = (Convert.ToDouble(txtLebar2.Text) / 1000) * (Convert.ToDouble(txtPanjang2.Text) / 1000);
                kali = Convert.ToInt32((Math.Truncate(Convert.ToDecimal(txtLebar1.Text) / Convert.ToDecimal(txtLebar2.Text))) *
                   (Math.Truncate(Convert.ToDecimal(txtPanjang1.Text) / Convert.ToDecimal(txtPanjang2.Text))));
                txtQty3.Text = (Convert.ToInt32(txtQty1.Text) * kali).ToString();
                //}
                //catch
                //{
                //    txtQty3.Text = "0";
                //}
            }
            else
            {
                txtQty3.Text = "0";
            }
            #region pengali
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

        protected void txtLokasi2_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoB.Text.Trim().Length < 10)
                return;
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
            Session["luas2"] = t3serah.Panjang * t3serah.Lebar;
            Session["stock2"] = t3serah.Qty;
            txtStock2.Text = qty.ToString();
            txtQty3.Focus();
        }

        protected void txtQty2_TextChanged(object sender, EventArgs e)
        {
            txtQty3.Text = (Convert.ToInt32(txtQty1.Text) * Convert.ToInt32(txtQty2.Text)).ToString();
            btnAdd.Focus();
            Users users = (Users)Session["Users"];
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            int cutoption = simF.GetMaxCut250(users.UnitKerjaID);
            int QtyMax = 0;
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
                else
                    QtyMax = 250;

                if (Convert.ToInt32(txtQty2.Text) > QtyMax)
                {
                    DisplayAJAXMessage(this, "Jumlah Partno Akhir tidak boleh lebih dari " + QtyMax.ToString().Trim() + " lembar.");
                    txtQty2.Text = string.Empty;
                    txtQty2.Focus();
                    return;
                }
            }
        }

        protected void txtQty3_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Focus();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void Getfocus()
        {
            if (txtPartnoA.Text == string.Empty)
                txtPartnoA.Focus();
            else
                if (txtLokasi1.Text == string.Empty)
                txtLokasi1.Focus();
            else
                    if (txtQty1.Text == string.Empty)
                txtQty1.Focus();
            else
                        if (txtPartnoB.Text == string.Empty || txtPartnoB.Text.Length < 10)
                txtPartnoB.Focus();
            else
                            if (txtLokasi2.Text == string.Empty)
                txtLokasi2.Focus();
            else
                                if (txtQty2.Text == string.Empty)
                txtQty2.Focus();
            else
                                    if (txtQty3.Text == string.Empty)
                txtQty3.Focus();
        }

        protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        {
            clearform();
            txtPartnoA.Focus();
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        protected void txtPartnoB_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }

        protected void ChkHide_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide.Checked == false)
                Panel3.Visible = false;
            else
                Panel3.Visible = true;
        }

        protected void GridViewAsimetris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void ChkHide0_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide0.Checked == false)
            {
                Panel5.Visible = false;
                btnTansfer.Visible = false;
            }
            else
            {
                Panel5.Visible = true;
                btnTansfer.Visible = true;
            }
        }

        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide1.Checked == false)
                Panel8.Visible = false;
            else
                Panel8.Visible = true;
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewASimetris();
        }

        protected void GridViewTemp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "batal")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewTemp.Rows[index];
                ArrayList arrTransfer = new ArrayList();
                if (Session["ListofAsimetris"] != null)
                {
                    arrTransfer = (ArrayList)Session["ListofAsimetris"];
                    arrTransfer.RemoveAt(index);
                    GridViewTemp.DataSource = arrTransfer;
                    GridViewTemp.DataBind();
                    txtluas.Text = (Convert.ToDecimal(txtluas.Text) + Convert.ToDecimal(row.Cells[6].Text)).ToString();
                }
            }
        }
        protected void RBList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btn_Show_Click(object sender, EventArgs e)
        {

        }
        protected void GridViewAsimetris_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewAsimetris.Rows[rowindex].FindControl("GridView2");
            Label lbl = (Label)GridViewAsimetris.Rows[rowindex].FindControl("Label2");
            GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = false;

            // 
            if (e.CommandName == "Details")
            {
                GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = true;
                GridViewAsimetris.Rows[rowindex].FindControl("btn_Show").Visible = false;

                ////// bind data with child gridview 
                ArrayList arrT3ASimetris = new ArrayList();
                T3_AsimetrisFacade T3ASAimetris = new T3_AsimetrisFacade();
                arrT3ASimetris = T3ASAimetris.RetrieveByDocNo(GridViewAsimetris.Rows[rowindex].Cells[0].Text);
                grv.DataSource = arrT3ASimetris;
                grv.DataBind();
                grv.Visible = true;
            }
            else
            {
                //// child gridview  display false when cancel button raise event
                grv.Visible = false;
                GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = false;
                GridViewAsimetris.Rows[rowindex].FindControl("btn_Show").Visible = true;
            }
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
            try
            {
                ClearAutoBS();
                FC_Items items = new FC_Items();
                FC_Items itemsAsal = new FC_Items();
                int kali = Convert.ToInt32(txtPengali.Text);
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
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
                if (txtPartnoB.Text == string.Empty)
                    return;

                if (ChkConvertBS.Checked == true)
                {
                    if (luassisa <= 0)
                        return;
                    if (panjangBS1 > 0 && lebarBS1 > 0)
                        partnoBS1 = txtPartnoB.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                            lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS1.Text = partnoBS1;

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
                            lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS2.Text = partnoBS2;
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
                            lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS3.Text = partnoBS3;
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
                            lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS4.Text = partnoBS4;
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
                //else
                //{
                //    LCQtyBS1.Text = "0";
                //    LCPartnoBS1.Text = string.Empty;
                //    kali = 0;
                //    //if (IsNumeric(txtPengali.Text) == false)
                //    txtPengali.Text = "1";
                //    FC_Items items0 = new FC_Items();
                //    FC_ItemsFacade items0F = new FC_ItemsFacade();
                //    items0 = items0F.RetrieveByPartNo(txtPartnoA.Text);
                //    items = itemsF.RetrieveByPartNo(txtPartnoB.Text);
                //    if (RBSimetris.Checked == true)
                //        txtPengali.Text = ((Math.Truncate(Convert.ToDecimal(items0.Lebar) / Convert.ToDecimal(items.Lebar))) *
                //        (Math.Truncate(Convert.ToDecimal(items0.Panjang) / Convert.ToDecimal(items.Panjang)))).ToString();
                //    else
                //        txtPengali.Text = "1";
                //    kali = Convert.ToInt32(txtPengali.Text);
                //    txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                //    LTotVolume.Text = (Convert.ToDecimal(LVolumeAsal.Text) * Convert.ToInt32(txtQtyAsal0.Text)).ToString();
                //    LVolumePotong.Text = (items.Volume).ToString();
                //    LTotVolume.Text = (Convert.ToDecimal(LTotVolume.Text) - (Convert.ToDecimal(txtQtyPOK0.Text) * Convert.ToDecimal(LVolumePotong.Text))).ToString();
                //}
            }
            catch { }
        }


        protected void ChkConvertBS_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConvertBS.Checked == true)
                AutoBS();
            else
                ClearAutoBS();
        }
        protected void GridViewAsimetris_DataBinding(object sender, EventArgs e)
        {
            //Users users = (Users)Session["Users"];
            //if (users.UserName.Trim().ToUpper() == "ADMIN")
            //{
            //    GridViewAsimetris.Columns[5].Visible = false;
            //}
        }
    }

    public class MesinCutter
    {
        public string NamaMesinCutter { get; set; }
        public int ID { get; set; }
        public int Lock { get; set; }
    }

    public class FacadeMesinCutter
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private MesinCutter mc = new MesinCutter();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }


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
                    arrData.Add(new MesinCutter
                    {
                        ID = int.Parse(sdr["ID"].ToString()),
                        NamaMesinCutter = sdr["NamaMesinCutter"].ToString()
                    });
                }
            }
            return arrData;
        }

        public MesinCutter RetrieveData(int Tebal, int Lebar, int Panjang)
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

            return new MesinCutter();
        }

        public MesinCutter GenerateObject_RetrieveData(SqlDataReader sqlDataReader)
        {
            mc = new MesinCutter();
            mc.ID = Convert.ToInt32(sqlDataReader["ID"]);
            mc.Lock = Convert.ToInt32(sqlDataReader["Lock"]);

            return mc;
        }
    }
}