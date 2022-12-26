using BusinessFacade;
using Cogs;
using DataAccessLayer;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Factory
{
    public partial class ProsesListplankR3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtdrtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtTglSerah.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadPartno();
                LoadDataTransit();
                T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
                int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));

                trCnC2.Visible = true; ChkCnC.Visible = true;
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 30 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            //loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        private void clearform()
        {
            //LoadDataTransit();
            txtQtyOut.Text = string.Empty;
            //txtPartnoOK.Text = string.Empty;
            //txtlokOK.Text = string.Empty;
            //txtPartnoBP.Text = string.Empty;
            //txtlokBP.Text = string.Empty;
            LoadDataTransit();
            //LoadDataGridViewMutasiLok();
        }
        private void LoadPartno()
        {
            FC_ItemsFacade Itemf = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            string tblname = string.Empty;

            if (RBI99.Checked == true)
                tblname = "T1_LR3_listplank";
            if (RBRuningSaw.Checked == true)
                tblname = "T1_LR3_RuningSaw";
            if (RBBevel.Checked == true)
                tblname = "T1_LR3_Bevel";
            if (RBPrint.Checked == true)
                tblname = "T1_LR3_Print";
            if (RBStraping.Checked == true)
                tblname = "T1_LR3_Straping";
            arrItems = Itemf.RetrieveByListPlank(tblname);
            //
            ddlPartno.Items.Clear();
            ddlPartno.Items.Add(new ListItem("", "0"));
            foreach (FC_Items items in arrItems)
            {
                ddlPartno.Items.Add(new ListItem(items.Partno, items.ID.ToString()));
            }
            ddlPartno.SelectedIndex = 1;
            LoadDataTransit();
            if (RBStraping.Checked == true)
            {
                if (RBOK.Checked == true)
                {
                    LabelOK.Visible = true;
                    txtlokOK.Visible = true;
                    LabelOK0.Visible = false;
                    txtlokBP.Visible = false;
                }
                else
                {
                    LabelOK0.Visible = true;
                    txtlokBP.Visible = true;
                    LabelOK.Visible = false;
                    txtlokOK.Visible = false;
                }
            }
            else
            {
                LabelOK.Visible = false;
                txtlokOK.Visible = false;
                LabelOK0.Visible = false;
                txtlokBP.Visible = false;
            }
        }
        private void LoadDataGridViewMutasiLok()
        {
            ArrayList arrT3MutasiLok = new ArrayList();
            T3_MutasiLokFacade T3MutasiLok = new T3_MutasiLokFacade();
            //arrT3MutasiLok = T3MutasiLok.RetrieveBytgl(TglMutasi.SelectedDate.ToString("yyyyMMdd"));
            GridViewMutasiLok.DataSource = arrT3MutasiLok;
            GridViewMutasiLok.DataBind();
        }
        private void LoadDataTransit()
        {
            ArrayList arrT1serah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();

            if (RBI99.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Listplank1(ddlPartno.SelectedItem.Text);
            if (RBRuningSaw.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3RuningSaw(ddlPartno.SelectedItem.Text);
            if (RBBevel.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Bevel(ddlPartno.SelectedItem.Text);
            if (RBStraping.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Straping(ddlPartno.SelectedItem.Text);
            GridViewtrans0.DataSource = arrT1serah;
            GridViewtrans0.DataBind();
            txtQtyOut.Text = string.Empty;
            int total = 0;
            T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
            int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                chkMutasi.Checked = true;
                txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
            }
            txtQtyOut.Text = total.ToString();
            txtQtyOut.Focus();
            if (RBI99.Checked == true || RBP99.Checked == true)
            {
                int kali = pengali();
                txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();

            }
            else
                txtQtyTransfer.Text = txtQtyOut.Text;
            txtPartnoTransfer.Text = string.Empty;
            txtPartnoOK.Text = string.Empty;
            txtPartnoBP.Text = string.Empty;
            if (ddlPartno.SelectedItem.Text == string.Empty)
                return;
            string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
            string lebar = ddlPartno.SelectedItem.Text.Substring(6, 3);
            string sisi = string.Empty;
            if (lebar == "090")
                sisi = "B1";
            else
                sisi = "SE";

            if (ddlPartno.SelectedItem.Text != string.Empty)
            {
                if (RBKali13.Checked == true)
                {
                    if (RBI99.Checked == true || RBP99.Checked == true)
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                    }
                    else
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                    }
                }
                if (RBKali14.Checked == true)
                {
                    if (RBI99.Checked == true || RBP99.Checked == true)
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                    }
                    else
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                    }
                }
                if (RBKali15.Checked == true)
                {
                    if (RBI99.Checked == true || RBP99.Checked == true)
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                    }
                    else
                    {
                        txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                        txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                        txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                    }
                }
            }
            else
                txtPartnoTransfer.Text = string.Empty;
            txtQtyOK.Text = txtQtyTransfer.Text;
            txtQtyBP.Text = "0";
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }
        protected int pengali()
        {
            int pengali0 = 0;

            try
            {
                string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
                string tebal = ddlPartno.SelectedItem.Text.Substring(6, 3);
                string lebar = ddlPartno.SelectedItem.Text.Substring(9, 4);
                string sisi = string.Empty;
                if (kode == "INT" && tebal == "090")
                    sisi = "B1";
                else
                    sisi = "SE";

                if (lebar == "0100")
                {
                    RBKali13.Visible = true;
                    RBKali14.Visible = false;
                    RBKali15.Visible = false;
                    RBKali13.Checked = true;
                    RBKali14.Checked = false;
                    RBKali15.Checked = false;
                    Panel9.Visible = false;
                }
                if (lebar == "0200")
                {
                    RBKali13.Visible = true;
                    RBKali14.Visible = true;
                    RBKali15.Visible = false;
                    Panel9.Visible = false;
                }
                if (lebar == "0300")
                {
                    RBKali13.Visible = true;
                    RBKali14.Visible = true;
                    RBKali15.Visible = true;
                    Panel9.Visible = true;
                    txtQtyTransfer2.Text = (Convert.ToInt32(txtQtyOut.Text)).ToString();
                    txtPartnoTransfer2.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                }


                if (RBKali13.Checked == true)
                {
                    if (lebar == "0100")
                        pengali0 = 1;
                    if (lebar == "0200")
                        pengali0 = 2;
                    if (lebar == "0300")
                        pengali0 = 2;
                }
                if (RBKali14.Checked == true)
                {
                    if (lebar == "0200")
                    {
                        pengali0 = 1;
                    }
                    if (lebar == "0300")
                    {
                        pengali0 = 1;
                    }
                }
                //if (RBKali15.Checked == true)
                //{
                //    if (lebar == "0300")
                //    {
                //        pengali0 = 1;
                //    }
                //}
                if (RBKaliNSTD.Checked == true)
                {

                    if (txtPanjangNStd.Text.Trim() != "")
                    {
                        if (Int32.Parse(txtPanjangNStd.Text) > 0)
                        {
                            pengali0 = (Convert.ToInt32(lebar) / Int32.Parse(txtPanjangNStd.Text));
                            pengali0 = Convert.ToInt32(pengali0);
                            if ((Convert.ToInt32(lebar) - (pengali0 * Int32.Parse(txtPanjangNStd.Text))) > 0)
                            {
                                txtPartnoTransfer2.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + (Convert.ToInt32(lebar) -
                                    (pengali0 * Int32.Parse(txtPanjangNStd.Text))).ToString().PadLeft(4, '0') + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                                txtQtyTransfer2.Text = txtQtyOut.Text;
                            }
                            else
                            {
                                txtPartnoTransfer2.Text = "";
                                txtQtyTransfer2.Text = "0";
                            }
                        }
                    }
                }
            }
            catch { }
            return pengali0;

        }
        protected void TransferData()
        {
            Users users = (Users)Session["Users"];
            string rekam = "gagal";
            string strError = string.Empty;
            int intresult = 0;
            BM_DestackingFacade dest = new BM_DestackingFacade();

            T1_LR3_ListPlank T1_LR3listplank = new T1_LR3_ListPlank();
            //T1_LR3_ListPlank2 T1_LR3listplank2 = new T1_LR3_ListPlank2();
            //T1_LR3_ListPlank2Facade T1_LR3listplank2F = new T1_LR3_ListPlank2Facade();
            T1_LR3_RuningSaw T1_LR3runingsaw = new T1_LR3_RuningSaw();
            T1_LR3_RuningSawFacade T1_LR3runingsawF = new T1_LR3_RuningSawFacade();
            T1_LR3_Bevel T1_LR3bevel = new T1_LR3_Bevel();
            T1_LR3_BevelFacade T1_LR3bevelF = new T1_LR3_BevelFacade();
            T1_LR3_Straping T1_LR3straping = new T1_LR3_Straping();
            T1_LR3_StrapingFacade T1_LR3strapingF = new T1_LR3_StrapingFacade();
            T1_SerahFacade serahF = new T1_SerahFacade();
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();

            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToInt32(DateTime.Parse(txtTanggal.Text).ToString("yyyy"));
            int Bulan = Convert.ToInt32(DateTime.Parse(txtTanggal.Text).ToString("MM"));
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            #region Verifikasi partno tujuan

            items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
            if (items.ID == 0)
            {
                items = new FC_Items();
                items.ItemTypeID = 1;
                items.Kode = txtPartnoTransfer.Text.Substring(0, 3);
                items.Tebal = decimal.Parse(txtPartnoTransfer.Text.Substring(6, 3)) / 10;
                items.Lebar = int.Parse(txtPartnoTransfer.Text.Substring(9, 4));
                items.Panjang = int.Parse(txtPartnoTransfer.Text.Substring(13, 4));
                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                items.Partno = txtPartnoTransfer.Text;
                items.GroupID = 0;
                itemsF.Insert(items);
                items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
            }
            items = itemsF.RetrieveByPartNo(txtPartnoOK.Text);
            if (items.ID == 0)
            {
                items = new FC_Items();
                items.ItemTypeID = 3;
                items.Kode = txtPartnoOK.Text.Substring(0, 3);
                items.Tebal = decimal.Parse(txtPartnoOK.Text.Substring(6, 3)) / 10;
                items.Lebar = int.Parse(txtPartnoOK.Text.Substring(9, 4));
                items.Panjang = int.Parse(txtPartnoOK.Text.Substring(13, 4));
                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                items.Partno = txtPartnoOK.Text;
                items.GroupID = 0;
                itemsF.Insert(items);
                items = itemsF.RetrieveByPartNo(txtPartnoOK.Text);
            }
            items = itemsF.RetrieveByPartNo(txtPartnoBP.Text);
            if (items.ID == 0)
            {
                items = new FC_Items();
                items.ItemTypeID = 3;
                items.Kode = txtPartnoBP.Text.Substring(0, 3);
                items.Tebal = decimal.Parse(txtPartnoBP.Text.Substring(6, 3)) / 10;
                items.Lebar = int.Parse(txtPartnoBP.Text.Substring(9, 4));
                items.Panjang = int.Parse(txtPartnoBP.Text.Substring(13, 4));
                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                items.Partno = txtPartnoBP.Text;
                items.GroupID = 0;
                itemsF.Insert(items);
                items = itemsF.RetrieveByPartNo(txtPartnoBP.Text);
            }
            #endregion
            //int QtyOK = Convert.ToInt32(txtQtyOK.Text);
            //int QtyBP = Convert.ToInt32(txtQtyBP.Text);
            int kali = pengali();
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_LR3_ListPlankFacade(T1_LR3listplank);
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                if (txtQtyMutasi.Text != string.Empty)
                {
                    #region I99
                    if (RBI99.Checked == true && ChkCnC.Checked == false)
                    {
                        items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
                        if (items.ID == 0)
                        {
                            items = new FC_Items();
                            items.ItemTypeID = 3;
                            items.Kode = txtPartnoTransfer.Text.Substring(0, 3);
                            items.Tebal = decimal.Parse(txtPartnoTransfer.Text.Substring(6, 3)) / 10;
                            items.Lebar = int.Parse(txtPartnoTransfer.Text.Substring(9, 4));
                            items.Panjang = int.Parse(txtPartnoTransfer.Text.Substring(13, 4));
                            items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                            items.Partno = txtPartnoTransfer.Text;
                            items.GroupID = 0;
                            itemsF.Insert(items);
                            items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
                        }
                        items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
                        T1_LR3runingsaw = new T1_LR3_RuningSaw();
                        T1_LR3runingsaw = T1_LR3runingsawF.RetrieveByID(GridViewtrans0.Rows[i].Cells[0].Text);
                        T1_LR3runingsaw.L1ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                        T1_LR3runingsaw.LokasiID0 = dest.GetLokID("I99");
                        T1_LR3runingsaw.ItemID0 = dest.GetPartnoID(GridViewtrans0.Rows[i].Cells[5].Text);
                        T1_LR3runingsaw.ItemID = items.ID;
                        T1_LR3runingsaw.TglTrans = DateTime.Parse(txtTanggal.Text);
                        T1_LR3runingsaw.QtyIn = kali * Convert.ToInt32(txtQtyMutasi.Text);
                        T1_LR3runingsaw.QtyAsal = Convert.ToInt32(txtQtyMutasi.Text);
                        T1_LR3runingsaw.LokasiID = T1_LR3runingsaw.LokasiID0;
                        T1_LR3runingsaw.CreatedBy = users.UserName;
                        absTrans = new T1_LR3_RuningSawFacade(T1_LR3runingsaw);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            DisplayAJAXMessage(this, "Rekam data potongan runing saw Error");
                            return;
                        }
                        if (Panel9.Visible == true && txtPartnoTransfer2.Text.Trim() != string.Empty)
                        {
                            items = itemsF.RetrieveByPartNo(txtPartnoTransfer2.Text);
                            if (items.ID == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoTransfer2.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoTransfer2.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoTransfer2.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoTransfer2.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoTransfer2.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsF.RetrieveByPartNo(txtPartnoTransfer2.Text);
                            }
                            items = itemsF.RetrieveByPartNo(txtPartnoTransfer2.Text);
                            T1_LR3runingsaw = new T1_LR3_RuningSaw();
                            T1_LR3runingsaw = T1_LR3runingsawF.RetrieveByID(GridViewtrans0.Rows[i].Cells[0].Text);
                            T1_LR3runingsaw.L1ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                            T1_LR3runingsaw.LokasiID0 = dest.GetLokID("I99");
                            T1_LR3runingsaw.ItemID0 = dest.GetPartnoID(GridViewtrans0.Rows[i].Cells[5].Text);
                            T1_LR3runingsaw.ItemID = items.ID;
                            T1_LR3runingsaw.TglTrans = DateTime.Parse(txtTanggal.Text);
                            T1_LR3runingsaw.QtyIn = Convert.ToInt32(txtQtyMutasi.Text);
                            T1_LR3runingsaw.QtyAsal = 0;
                            T1_LR3runingsaw.LokasiID = T1_LR3runingsaw.LokasiID0;
                            T1_LR3runingsaw.CreatedBy = users.UserName;
                            absTrans = new T1_LR3_RuningSawFacade(T1_LR3runingsaw);
                            intresult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                DisplayAJAXMessage(this, "Rekam data potongan runing saw Error");
                                return;
                            }
                        }
                    }
                    /** Added 19 November 2019 By Beny  **/
                    /** Proses Rekam dari lokasi i99 ke Tahap 3 ( Supply ke KAT - Produk CNC )  **/
                    else if (RBI99.Checked == true && ChkCnC.Checked == true && i == 0)
                    {
                        string Proses = "i99R3"; Session["Proses"] = Proses;
                        string lokasitujuan = txtLokasi.Text;
                        //string PartnoTujuan = Session["PartnoTujuan"].ToString();
                        items = itemsF.RetrieveByPartNo(ddlPartno.SelectedItem.ToString());
                        T1_Serah serah = new T1_Serah();

                        if (txtLokasi.Text == "")
                        { DisplayAJAXMessage(this, "Lokasi tahap 3 belum ditentukan."); return; }

                        serah = serahF.RetrieveByIDListP(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.PartnoSer = ddlPartno.SelectedItem.ToString();
                        serah.PartnoDest = GridViewtrans0.Rows[i].Cells[3].Text;
                        serah.ItemIDSer = items.ID;
                        serah.QtyIn = Convert.ToInt32(txtQtyOut.Text);
                        serah.LokasiID = dest.GetLokID("I99");
                        serah.LokasiSer = lokasitujuan;
                        serah.CreatedBy = users.UserName;
                        serah.TglSerah = Convert.ToDateTime(txtTanggal.Text);
                        serah.ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.SFrom = "i99R3";
                        TextBox partnoTrm = new TextBox();
                        TextBox lokasiTrm = new TextBox();
                        TextBox QtyTrm = new TextBox();
                        partnoTrm.Text = ddlPartno.SelectedItem.ToString();
                        lokasiTrm.Text = lokasitujuan;
                        QtyTrm.Text = serah.QtyIn.ToString();
                        int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, serah);
                        //txtPartnoOK.Text, txtLokTrm.Text
                    }
                    #endregion
                    #region RBRuningSaw
                    if (RBRuningSaw.Checked == true && ChkCnC.Checked == false)
                    {
                        items = itemsF.RetrieveByPartNo(txtPartnoTransfer.Text);
                        T1_LR3bevel = T1_LR3bevelF.RetrieveByID(GridViewtrans0.Rows[i].Cells[0].Text);
                        T1_LR3bevel.L1ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                        T1_LR3bevel.LokasiID0 = dest.GetLokID("I99");
                        T1_LR3bevel.ItemID0 = dest.GetPartnoID(GridViewtrans0.Rows[i].Cells[3].Text);
                        T1_LR3bevel.ItemID = items.ID;
                        T1_LR3bevel.TglTrans = DateTime.Parse(txtTanggal.Text);
                        T1_LR3bevel.QtyIn = Convert.ToInt32(txtQtyMutasi.Text);
                        T1_LR3bevel.LokasiID = T1_LR3bevel.LokasiID0;
                        T1_LR3bevel.CreatedBy = users.UserName;
                        absTrans = new T1_LR3_BevelFacade(T1_LR3bevel);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            DisplayAJAXMessage(this, "Rekam data Bevel Error");
                            return;
                        }

                    }
                    else if (RBRuningSaw.Checked == true && ChkCnC.Checked == true && i == 0)
                    {
                        string Proses = "RsR3"; Session["Proses"] = Proses;
                        items = itemsF.RetrieveByPartNo(ddlPartno.SelectedItem.ToString());
                        T1_Serah serah = new T1_Serah();

                        if (txtLokasi.Text == "")
                        { DisplayAJAXMessage(this, "Lokasi tahap 3 belum ditentukan."); return; }

                        serah = serahF.RetrieveByIDListP(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.PartnoSer = ddlPartno.SelectedItem.ToString();
                        serah.PartnoDest = GridViewtrans0.Rows[i].Cells[3].Text;
                        serah.ItemIDSer = items.ID;
                        serah.QtyIn = Convert.ToInt32(txtQtyOut.Text);
                        serah.LokasiID = dest.GetLokID("I99");
                        serah.LokasiSer = txtLokasi.Text; ;
                        serah.CreatedBy = users.UserName;
                        serah.TglSerah = Convert.ToDateTime(txtTanggal.Text);
                        serah.ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.SFrom = "RsR3";
                        TextBox partnoTrm = new TextBox();
                        TextBox lokasiTrm = new TextBox();
                        TextBox QtyTrm = new TextBox();
                        partnoTrm.Text = ddlPartno.SelectedItem.ToString();
                        lokasiTrm.Text = txtLokasi.Text; ;
                        QtyTrm.Text = serah.QtyIn.ToString();
                        int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, serah);
                    }
                    #endregion
                    #region Bevel
                    if (RBBevel.Checked == true)
                    {
                        string partno = string.Empty;
                        string lokasi = string.Empty;
                        if (Convert.ToInt32(txtQtyOK.Text) > 0)
                        {
                            partno = txtPartnoOK.Text;
                            lokasi = txtlokOK.Text;
                        }
                        else
                        {
                            partno = txtPartnoBP.Text;
                            lokasi = txtlokBP.Text;
                        }
                        if (RBLogistik.Checked == true && RBBP.Checked == true)
                        {
                            items = itemsF.RetrieveByPartNo(partno);
                            T1_Serah serah = new T1_Serah();
                            serah = serahF.RetrieveByIDLR3_Bevel(GridViewtrans0.Rows[i].Cells[0].Text);
                            serah.PartnoSer = partno;
                            serah.PartnoDest = GridViewtrans0.Rows[i].Cells[3].Text;
                            serah.ItemIDSer = items.ID;
                            serah.QtyIn = Convert.ToInt32(txtQtyMutasi.Text);
                            serah.LokasiID = dest.GetLokID("I99");
                            serah.LokasiSer = lokasi;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtTanggal.Text);
                            serah.ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                            serah.SFrom = "BevelR3";
                            TextBox partnoTrm = new TextBox();
                            TextBox lokasiTrm = new TextBox();
                            TextBox QtyTrm = new TextBox();
                            partnoTrm.Text = serah.PartnoSer;
                            lokasiTrm.Text = serah.LokasiSer;
                            QtyTrm.Text = serah.QtyIn.ToString();
                            int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, serah);
                            if (intterima < 0)
                            {
                                transManager.RollbackTransaction();
                                DisplayAJAXMessage(this, "Rekam data serah Error");
                                return;
                            }
                        }
                        else
                        {
                            items = itemsF.RetrieveByPartNo(partno);
                            T1_LR3straping = T1_LR3strapingF.RetrieveByID(GridViewtrans0.Rows[i].Cells[0].Text);
                            T1_LR3straping.L1ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                            T1_LR3straping.LokasiID0 = dest.GetLokID("I99");
                            T1_LR3straping.ItemID0 = dest.GetPartnoID(GridViewtrans0.Rows[i].Cells[3].Text);
                            T1_LR3straping.ItemID = items.ID;
                            T1_LR3straping.TglTrans = DateTime.Parse(txtTanggal.Text);
                            T1_LR3straping.QtyIn = Convert.ToInt32(txtQtyMutasi.Text);
                            T1_LR3straping.LokasiID = T1_LR3straping.LokasiID0;
                            T1_LR3straping.CreatedBy = users.UserName;
                            absTrans = new T1_LR3_StrapingFacade(T1_LR3straping);
                            intresult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                DisplayAJAXMessage(this, "Rekam data Straping Error");
                                return;
                            }
                        }
                    }
                    #endregion

                    #region Straping
                    if (RBStraping.Checked == true)
                    {
                        //QtyOK = QtyOK + Convert.ToInt32(txtQtyMutasi.Text);
                        //if (QtyOK <= Convert.ToInt32(txtQtyOK.Text) && Convert.ToInt32(txtQtyOK.Text) > 0)
                        //{
                        string partno = string.Empty;
                        string lokasi = string.Empty;
                        if (Convert.ToInt32(txtQtyOK.Text) > 0)
                        {
                            partno = txtPartnoOK.Text;
                            lokasi = txtlokOK.Text;
                        }
                        else
                        {
                            partno = txtPartnoBP.Text;
                            lokasi = txtlokBP.Text;
                        }
                        if (lokasi.Trim() == string.Empty || partno.Trim() == string.Empty)
                        {
                            DisplayAJAXMessage(this, "Partno / Lokasi tidak boleh kosong");
                            return;
                        }
                        items = itemsF.RetrieveByPartNo(partno);
                        T1_Serah serah = new T1_Serah();
                        serah = serahF.RetrieveByIDLR3_Print(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.PartnoSer = partno;
                        serah.PartnoDest = GridViewtrans0.Rows[i].Cells[3].Text;
                        serah.ItemIDSer = items.ID;
                        serah.QtyIn = Convert.ToInt32(txtQtyMutasi.Text);
                        serah.LokasiID = dest.GetLokID("I99");
                        serah.LokasiSer = lokasi;
                        serah.CreatedBy = users.UserName;
                        serah.TglSerah = Convert.ToDateTime(txtTanggal.Text);
                        serah.ID = Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text);
                        serah.SFrom = "strapingR3";
                        TextBox partnoTrm = new TextBox();
                        TextBox lokasiTrm = new TextBox();
                        TextBox QtyTrm = new TextBox();
                        partnoTrm.Text = serah.PartnoSer;
                        lokasiTrm.Text = serah.LokasiSer;
                        QtyTrm.Text = serah.QtyIn.ToString();
                        int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, serah);
                        if (intterima < 0)
                        {
                            transManager.RollbackTransaction();
                            DisplayAJAXMessage(this, "Rekam data serah Error");
                            return;
                        }
                    }
                    #endregion
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            rekam = "sukses";
            if (rekam == "sukses")
            {
                for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
                {
                    TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                    if (txtQtyMutasi.Text != string.Empty)
                    {
                        if (RBI99.Checked == true)
                        {
                            T1_LR3_ListPlankFacade T1_LR3listplankF = new T1_LR3_ListPlankFacade();
                            T1_LR3listplankF.UpdateFail(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text), Convert.ToInt32(txtQtyMutasi.Text));
                        }
                        if (RBRuningSaw.Checked == true)
                        {
                            T1_LR3_RuningSawFacade T1_LR3RuningSawF = new T1_LR3_RuningSawFacade();
                            T1_LR3RuningSawF.UpdateFail(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text), Convert.ToInt32(txtQtyMutasi.Text));
                        }
                        if (RBBevel.Checked == true)
                        {
                            T1_LR3bevelF.UpdateFail(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text), Convert.ToInt32(txtQtyMutasi.Text));
                        }
                        if (RBStraping.Checked == true)
                        {
                            T1_LR3strapingF = new T1_LR3_StrapingFacade();
                            T1_LR3strapingF.UpdateFail(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text), Convert.ToInt32(txtQtyMutasi.Text));
                        }
                    }
                }
            }
            DisplayAJAXMessage(this, "Data tersimpan");
            clearform();
            LoadPartno();
            LoadDataTransit();
        }
        private void loadDynamicGrid(string tgl1, string tgl2)
        {
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;

            string strSQL = "declare @tgl1 char(8) " +
            "declare @tgl2 char(8) " +
            "set @tgl1='" + tgl1 + "' " +
            "set @tgl2='" + tgl2 + "' " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlank]') AND type in (N'U')) DROP TABLE [dbo].tempListPlank  " +
            "select ROW_NUMBER() OVER (ORDER BY PartnoAsal, tanggal ) AS Row,* into tempListPlank from (   " +
            "select  * from ( select distinct tanggal tanggal0,partnoasal from  vw_KartuStockListplankR3 AI where CONVERT(char,tanggal,112)>=@tgl1 and CONVERT(char,tanggal,112)<=@tgl2) as P left join   " +
            "(  " +
            "select tanggal,PartNoAsal PartNoAsal0,PartNo,sum(qtyin)qtyin,sum(qtyout)qtyout from (   " +
            "select tanggal,PartNoAsal,PartNo,qty qtyin,0 qtyout from vw_KartuStockListplankR3  where qty>0 and process='i99'   " +
            "union all   " +
            "select tanggal,PartNoAsal,PartNo,0 qtyin,qty*-1 qtyout from vw_KartuStockListplankR3  where qty<0 and process='i99') as i99  " +
            "where  CONVERT(char,tanggal,112)>=@tgl1 and CONVERT(char,tanggal,112)<=@tgl2 group by tanggal,PartNoAsal,PartNo) as i99A on i99A.PartNoAsal0=P.PartnoAsal and P.tanggal0=i99A.tanggal   " +
            "left join   " +
            "(   " +
            "select tanggal tanggal1,PartNoAsal PartNoAsal1,PartNo PartNo1,sum(qtyin)qtyin1,sum(qtyout)qtyout1 from (  " +
            "select tanggal,PartNoAsal,PartNo,qty qtyin,0 qtyout from vw_KartuStockListplankR3  where qty>0 and process='runingsaw'  " +
            "union all   " +
            "select tanggal,PartNoAsal,PartNo,0 qtyin ,qty*-1 qtyout from vw_KartuStockListplankR3  where qty<0 and process='runingsaw') as runingsaw   " +
            "where  CONVERT(char,tanggal,112)>=@tgl1 and CONVERT(char,tanggal,112)<=@tgl2 group by tanggal,PartNoAsal,PartNo) as runingsawA    " +
            "on P.tanggal0=runingsawA.tanggal1 and P.PartNoAsal=runingsawA.PartNoAsal1    " +
            "left join    " +
            "(   " +
            "select tanggal tanggal2,PartNoAsal PartNoAsal2,PartNo PartNo2,sum(qtyin)qtyin2,sum(qtyout)qtyout2 from (   " +
            "select tanggal,PartNoAsal,PartNo,qty qtyin,0 qtyout from vw_KartuStockListplankR3  where qty>0 and process='Bevel'   " +
            "union all   " +
            "select tanggal,PartNoAsal,PartNo,0 qtyin,qty*-1 qtyout from vw_KartuStockListplankR3  where qty<0 and process='Bevel') as Bevel   " +
            "where  CONVERT(char,tanggal,112)>=@tgl1 and CONVERT(char,tanggal,112)<=@tgl2 group by tanggal,PartNoAsal,PartNo) as BevelA    " +
            "on P.tanggal0=BevelA.tanggal2 and P.PartNoAsal=BevelA.PartNoAsal2  /*and SUBSTRING(BevelA.PartNo2,7,11)=SUBSTRING(runingsawA.PartNo1,7,11) */  " +
            "left join    " +
            "(   " +
            "select tanggal tanggal4,PartNoAsal PartNoAsal4,PartNo PartNo4,sum(qtyin)qtyin4,sum(qtyout)qtyout4 from (   " +
            "select tanggal,PartNoAsal,PartNo,qty qtyin,0 qtyout from vw_KartuStockListplankR3  where qty>0 and process='StrapingR3'   " +
            "union all   " +
            "select tanggal,PartNoAsal,PartNo,0 qtyin,qty*-1 qtyout from vw_KartuStockListplankR3  where qty<0 and process='StrapingR3') as straping   " +
            "where  CONVERT(char,tanggal,112)>=@tgl1 and CONVERT(char,tanggal,112)<=@tgl2 group by tanggal,PartNoAsal,PartNo) as strapingA    " +
            "on P.tanggal0=strapingA.tanggal4 and P.PartNoAsal=strapingA.PartNoAsal4  /*and BevelA.PartNo2=strapingA.PartNo4 */ " +
            ") as d   " +
            "select  Tanggal0 Tanggal,Partnoasal I99_Partno,qtyin I99_In,qtyout I99_Out,Partno1 RuningSaw_Partno,qtyin1 RuningSaw_In, qtyout1 RuningSaw_Out,Partno2 Bevel_Partno,    " +
            "qtyin2 Bevel_In,qtyout2 Bevel_Out,Partno4 Straping_Partno, qtyin4 Straping_In, qtyout4 Straping_out from (   " +
            "select Tanggal0,Partnoasal,isnull(sum(qtyin),0)qtyin,isnull(sum(qtyout),0)qtyout,Partno1,isnull(sum(qtyin1),0)qtyin1,isnull(sum(qtyout1),0)qtyout1,Partno2, " +
            "isnull(sum(qtyin2),0)qtyin2,isnull(sum(qtyout2),0)qtyout2,Partno4,isnull(sum(qtyin4),0)qtyin4,isnull(sum(qtyout4),0)qtyout4 from (   " +
            "select Row, Tanggal0,Partnoasal,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno=L.partno and ROW<L.row)=0 then qtyin else null end qtyin,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno=L.partno and ROW<L.row)=0 then qtyout else null end qtyout,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno1=L.partno1 and ROW<L.row)=0 then Partno1 else null end Partno1,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno1=L.partno1 and ROW<L.row)=0 then qtyin1 else null end qtyin1,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno1=L.partno1 and ROW<L.row)=0 then qtyout1 else null end qtyout1,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno2=L.partno2 and ROW<L.row)=0 then Partno2 else null end Partno2,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno2=L.partno2 and ROW<L.row)=0 then qtyin2 else null end qtyin2,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno2=L.partno2 and ROW<L.row)=0 then qtyout2 else null end qtyout2,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno4=L.partno4 and ROW<L.row)=0 then Partno4 else null end Partno4,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno4=L.partno4 and ROW<L.row)=0 then qtyin4 else null end qtyin4,   " +
            "case when (select COUNT(Partnoasal) from templistplank where tanggal0=L.tanggal0  and partnoasal=L.partnoasal and partno4=L.partno4 and ROW<L.row)=0 then qtyout4 else null end qtyout4    " +
            "from tempListPlank L ) as L1  group by Tanggal0,Partnoasal,Partno1,Partno2,Partno4 ) as L2   " +
            "where qtyin+qtyout+qtyin1+qtyout1+qtyin2+qtyout2+qtyin4+qtyout4>0   " +
            "order by Partnoasal ,Tanggal0 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlank]') AND type in (N'U')) DROP TABLE [dbo].tempListPlank";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;

                if (col.ColumnName == "Tanggal")
                {
                    bfield.DataFormatString = "{0:d}";
                    bfield.HeaderText = "Tanggal";
                }
                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "NO")
                    bfield.HeaderText = "Partno";
                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "IN")
                    bfield.HeaderText = "Qty_In";
                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "UT")
                    bfield.HeaderText = "Qty_Out";
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 30 ,false); </script>", false);
        }
        protected int TerimaBarangLisplank(TextBox Partno, TextBox lokasi, TextBox qty, T1_Serah serah)
        {
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            Users users = (Users)HttpContext.Current.Session["Users"];
            int intResult = 0;
            int maxtrans = 0;
            int checktrans = 0;
            decimal AvgHPP = 0;

            TextBox txtQtyTrm = qty;
            TextBox txtLokTrm = lokasi;
            TextBox txtPartnoOK = Partno;
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();

            items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
            t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoOK.Text, txtLokTrm.Text);
            AvgHPP = 0;
            if (txtQtyTrm.Text == string.Empty)
                txtQtyTrm.Text = "0";
            //maxtrans = serah.QtyIn - serah.QtyOut;
            //checktrans = Convert.ToInt32(txtQtyTrm.Text);
            T3_Rekap rekap = new T3_Rekap();
            if (txtQtyTrm.Text != string.Empty)
            {
                rekap.DestID = serah.DestID;
                rekap.SerahID = t3serah.ID;
                rekap.Keterangan = serah.PartnoDest;
                rekap.T1serahID = serah.ID;
                rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                //if (rekap.LokasiID == 0)
                //{
                //    return 0;
                //}
                FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
                rekap.ItemIDSer = items.ID;
                rekap.T1sItemID = serah.ItemIDSer;
                rekap.TglTrm = serah.TglSerah;
                rekap.QtyInTrm = int.Parse(txtQtyTrm.Text);
                rekap.T1SLokID = serah.LokasiID;
                rekap.QtyOutTrm = 0;
                rekap.SA = t3serah.Qty;
                if (int.Parse(txtQtyTrm.Text) > 0 && serah.HPP > 0)
                    AvgHPP = ((t3serah.HPP * t3serah.Qty) + (int.Parse(txtQtyTrm.Text) * serah.HPP)) / (t3serah.Qty + int.Parse(txtQtyTrm.Text));
                else
                    AvgHPP = t3serah.HPP;
                rekap.HPP = AvgHPP;
                rekap.GroupID = items.GroupID;
                rekap.CreatedBy = users.UserName;
                rekap.Process = "Direct";
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                rekap.Sfrom = serah.SFrom;
                serah.QtyOut = int.Parse(txtQtyTrm.Text);

                //proses Update Stock
                t3serah.Flag = "tambah";
                t3serah.ItemID = items.ID;
                t3serah.ID = t3serah.ID;
                t3serah.GroupID = items.GroupID;
                t3serah.LokID = rekap.LokasiID;
                t3serah.Qty = int.Parse(txtQtyTrm.Text);
                t3serah.HPP = rekap.HPP;
                t3serah.CreatedBy = users.UserName;
                if (t3serah.ID == 0)
                    rekap.SerahID = intResult;
                else
                    rekap.SerahID = t3serah.ID;
                TerimaProcessFacade TerimaProcessFacade = new TerimaProcessFacade(t3serah, rekap);
                string strError = TerimaProcessFacade.Insertlistplank();
                if (strError != string.Empty)
                    intResult = -1;
                else
                    intResult = 0;
            }
            return intResult;
        }
        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            btnTansfer.Disabled = true;
            TransferData();
            btnTansfer.Disabled = false;
            clearform();
            Session["serahID"] = null;
            Session["lokid1"] = null;
            Session["lokid2"] = null;
            Session["lokasi1"] = null;
            Session["partno1"] = null;
            Session["itemid1"] = null;
            Session["GrouID"] = null;
            Session["hpp1"] = null;
            Session["stock1"] = null;
            Session["hpp2"] = null;
            Session["stock2"] = null;
        }
        private void clearlokasiawal()
        {
            txtQtyOut.Text = string.Empty;
            //txtPartnoA.Text = string.Empty;
            //txtPartnoA.Focus();
        }
        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["GroupID"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            int cekLoading = 0;
            if (cekLoading > 0)
            {
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            Session["lokasi1"] = t3serah.Lokasi;
            Session["partno1"] = t3serah.Partno;
            Session["itemid1"] = t3serah.ItemID;
            Session["hpp1"] = t3serah.HPP;
            Session["stock1"] = t3serah.Qty;
            Session["luas1"] = t3serah.Lebar * t3serah.Panjang;
            txtQtyOut.Text = string.Empty;
            txtQtyOut.Focus();
        }
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
        }
        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            #region viewgrid
            ArrayList arrT1serah = new ArrayList();
            ArrayList arrT1serah1 = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            int totalmutasi = Convert.ToInt32(txtQtyOut.Text);
            string palet = string.Empty;
            if (RBI99.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Listplank1(ddlPartno.SelectedItem.Text);
            if (RBRuningSaw.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3RuningSaw(ddlPartno.SelectedItem.Text);
            if (RBBevel.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Bevel(ddlPartno.SelectedItem.Text);
            if (RBStraping.Checked == true)
                arrT1serah = Serah.RetrieveStockLR3Straping(ddlPartno.SelectedItem.Text);

            int totalarray = 0;
            foreach (T1_Serah T1serah in arrT1serah)
            {
                totalarray = totalarray + T1serah.QtyIn;
                arrT1serah1.Add(T1serah);
                if (totalarray > totalmutasi)
                    break;
            }
            GridViewtrans0.DataSource = arrT1serah1;
            GridViewtrans0.DataBind();
            #endregion
            int jumlah = Convert.ToInt32(txtQtyOut.Text);
            int total = 0;

            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                if (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text) < jumlah)
                {
                    chkMutasi.Checked = true;
                    txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                    jumlah = jumlah - (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
                    total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
                }
                else
                {
                    chkMutasi.Checked = true;
                    txtQtyMutasi.Text = jumlah.ToString();
                    total = total + jumlah;
                    break;
                }
            }
            txtQtyOut.Text = total.ToString();
            if (total == 0)
            {
                txtQtyOut.Focus();
                return;
            }
            txtPartnoOK.Focus();

            /** Added 19 November 2019 By Beny  **/
            /** Proses keluar dari lokasi i99 ke Tahap III ( Supply ke KAT - Produk CNC ) **/
            if (RBI99.Checked == true && ChkCnC.Checked == true)
            {
                txtPartnoTCnC.Text = txtQtyOut.Text;
                txtPartnoCnC.Text = ddlPartno.SelectedItem.ToString();
            }
            else if (RBI99.Checked == true || RBP99.Checked == true)
            {
                int kali = pengali();
                txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();
            }
            else if (RBRuningSaw.Checked == true && ChkCnC.Checked == true)
            {
                txtPartnoTCnC.Text = txtQtyOut.Text;
                txtPartnoCnC.Text = ddlPartno.SelectedItem.ToString(); txtLokasi.Text = "AA13";
            }
            else
                txtQtyTransfer.Text = txtQtyOut.Text;
            if (RBOK.Checked == true)
                txtQtyOK.Text = txtQtyTransfer.Text;
            else
                txtQtyBP.Text = txtQtyTransfer.Text;
            //}
            //catch { }
        }
        private void Getfocus()
        {
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void txtLokasi1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }
        protected void txtQty1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }
        protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        {
            clearform();
            //txtPartnoA.Focus();
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void ChkMutasi_CheckedChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            int lastindex = 0;
            if (IsNumeric(txtQtyOut.Text) == true)
                jumlah = Convert.ToInt32(txtQtyOut.Text);
            else
                txtQtyOut.Text = "0";
            if (chkMutasi.Checked == true)
            {
                jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[7].Text;
                jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                lastindex = row.RowIndex;
            }
            else
            {
                //jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[7].Text;
                jumlah = jumlah - int.Parse(txtQtyMutasi.Text);
                txtQtyMutasi.Text = string.Empty;
                lastindex = row.RowIndex;
            }
            GridViewtrans0.SelectedIndex = lastindex;
            txtQtyMutasi.Focus();
            txtQtyOut.Text = jumlah.ToString();
        }
        protected void txtQtyOut_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                if (chkMutasi.Checked == false)
                    txtQtyMutasi.Text = string.Empty;
                if (txtQtyMutasi.Text != string.Empty)
                {
                    if (int.Parse(GridViewtrans0.Rows[i].Cells[7].Text) < int.Parse(txtQtyMutasi.Text))
                    {
                        txtQtyMutasi.Text = string.Empty;
                        chkMutasi.Checked = false;
                    }
                    else
                        jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                }
            }
            txtQtyMutasi.Focus();
            txtQtyOut.Text = jumlah.ToString();
        }
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {
        }
        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtPartnoOK_TextChanged(object sender, EventArgs e)
        {
            string tempPartno = string.Empty;
            string ukuran = string.Empty;
            string ukuran1 = string.Empty;
            if (txtPartnoTransfer.Text != string.Empty)
            {
                tempPartno = txtPartnoTransfer.Text;
                ukuran = ddlPartno.SelectedItem.Text.Substring(6, 11);
                ukuran1 = txtPartnoTransfer.Text.Substring(6, 11);
                //if (ukuran != ukuran1)
                //{
                //    txtPartnoTransfer.Focus();
                //    DisplayAJAXMessage(this, "Ukuran partno asal dan partno transfer harus sama");
                //    return;
                //}
            }
            txtlokOK.Focus();
        }
        protected void txtlokOK_TextChanged(object sender, EventArgs e)
        {
            txtPartnoBP.Focus();
        }
        protected void txtPartnoBP_TextChanged(object sender, EventArgs e)
        {
            txtlokBP.Focus();
        }
        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RBOK_CheckedChanged(object sender, EventArgs e)
        {
            string tempPartno = string.Empty;
            string ukuran = string.Empty;
            string ukuran1 = string.Empty;
            string kwalitas = string.Empty;
            txtQtyOK.Text = "0";
            txtQtyBP.Text = "0";
            if (txtPartnoTransfer.Text != string.Empty)
            {
                kwalitas = ddlPartno.SelectedItem.Text.Substring(3, 3);
                tempPartno = txtPartnoTransfer.Text;
                ukuran = ddlPartno.SelectedItem.Text.Substring(6, 11);
                ukuran1 = txtPartnoOK.Text.Substring(6, 11);
                if (ukuran != ukuran1)
                {

                    DisplayAJAXMessage(this, "ukuran dan kwalitas partno asal dan partno transfer harus sama");
                    return;
                }

            }
            txtPartnoOK.Visible = true;
            txtPartnoBP.Visible = false;
            txtQtyBP.Visible = false;
            LabelqtyBP.Visible = false;
            txtQtyOK.Visible = true;
            LabelqtyOK.Visible = true;
            txtQtyOK.Text = txtQtyTransfer.Text;
            txtQtyBP.Text = "0";
            RBStraping0.Checked = true;
            RBLogistik.Checked = false;
            if (RBOK.Checked == true) { LabelOK.Visible = true; txtlokOK.Visible = true; LabelOK0.Visible = false; txtlokBP.Visible = false; }
            else { LabelOK.Visible = false; txtlokOK.Visible = false; LabelOK0.Visible = true; txtlokBP.Visible = true; }
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string tempPartno = string.Empty;
            string ukuran = string.Empty;
            string ukuran1 = string.Empty;
            string kwalitas = string.Empty;
            txtQtyOK.Text = "0";
            txtQtyBP.Text = "0";
            if (txtPartnoTransfer.Text != string.Empty)
            {
                kwalitas = ddlPartno.SelectedItem.Text.Substring(3, 3);
                tempPartno = txtPartnoTransfer.Text;
                ukuran = ddlPartno.SelectedItem.Text.Substring(6, 11);
                ukuran1 = txtPartnoBP.Text.Substring(6, 11);
                if (ukuran != ukuran1)
                {
                    DisplayAJAXMessage(this, "ukuran dan kwalitas partno asal dan partno transfer harus sama");
                    return;
                }

            }
            txtPartnoOK.Visible = false;
            txtPartnoBP.Visible = true;
            txtQtyBP.Visible = true;
            LabelqtyBP.Visible = true;
            txtQtyOK.Visible = false;
            LabelqtyOK.Visible = false;
            txtQtyOK.Text = "0";
            txtQtyBP.Text = txtQtyTransfer.Text;
            RBStraping0.Checked = false;
            RBLogistik.Checked = true;
            if (RBOK.Checked == true) { LabelOK.Visible = true; txtlokOK.Visible = true; LabelOK0.Visible = false; txtlokBP.Visible = false; }
            else { LabelOK.Visible = false; txtlokOK.Visible = false; LabelOK0.Visible = true; txtlokBP.Visible = true; }
        }
        protected void RBP99_CheckedChanged(object sender, EventArgs e)
        {
            if (RBP99.Checked == true)
            {
                RBRuningSaw0.Checked = false;
                RBBevel0.Checked = false;
                RBPrint0.Checked = false;
                RBStraping0.Checked = false;
                RBLogistik.Checked = false;
                RBI990.Checked = true;
                LoadPartno();
                PanelSerah.Visible = false;
            }
        }
        protected void RBI99_CheckedChanged(object sender, EventArgs e)
        {
            if (RBI99.Checked == true)
            {
                ChkCnC.Checked = false; PanelCnC2.Visible = false; ChkCnC.Visible = true;
                RBRuningSaw0.Checked = true;
                RBBevel0.Checked = false;
                RBPrint0.Checked = false;
                RBStraping0.Checked = false;
                RBLogistik.Checked = false;
                RBI990.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                RBKali13.Visible = true;
                RBKali14.Visible = true;
                //RBKali15.Visible = true; 
                txtPartnoTransfer.Visible = true;
                RBOK.Checked = true;
            }
        }
        protected void RBRuningSaw_CheckedChanged(object sender, EventArgs e)
        {
            if (RBRuningSaw.Checked == true)
            {
                ChkCnC.Checked = false; PanelCnC2.Visible = false; ChkCnC.Visible = true;
                RBBevel0.Checked = true;
                RBRuningSaw0.Checked = false;
                RBPrint0.Checked = false;
                RBStraping0.Checked = false;
                RBLogistik.Checked = false;
                RBStraping0.Checked = false;
                RBI990.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;
                RBOK.Checked = true;

            }
        }
        protected void RBBevel_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBevel.Checked == true)
            {
                ChkCnC.Checked = false; PanelCnC2.Visible = false; ChkCnC.Visible = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                txtPartnoTransfer.Visible = false;
                RBLogistik.Checked = false;
                LoadPartno();
                //try
                //{
                RBOK.Enabled = false;
                RBBP.Enabled = false;
                RBStraping0.Checked = true;
                RBOK.Checked = false;
                RBBP.Checked = true;
                txtPartnoOK.Visible = false;
                txtPartnoBP.Visible = true;
                txtQtyBP.Visible = true;
                LabelqtyBP.Visible = true;
                txtQtyOK.Visible = false;
                LabelqtyOK.Visible = false;
                txtQtyOK.Text = "0";
                txtQtyBP.Text = txtQtyTransfer.Text;
                RBBevel0.Checked = false;
                RBRuningSaw0.Checked = false;
                RBPrint0.Checked = false;
                RBI990.Checked = false;

                if (ddlPartno.SelectedItem.Text.Trim() == string.Empty)
                {
                    txtQtyOK.Text = "0";
                    txtQtyOK.Text = "0";
                }
                PanelSerah.Visible = true;

                //}
                //catch { }
            }
        }
        protected void RBPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPrint.Checked == true)
            {
                RBLogistik.Checked = false;
                RBStraping0.Checked = true;
                RBBevel0.Checked = false;
                RBRuningSaw0.Checked = false;
                RBPrint0.Checked = false;
                RBI990.Checked = false;
                LoadPartno();
                PanelSerah.Visible = true;
                RBOK.Enabled = false;
                RBBP.Enabled = false;
            }
        }
        protected void RBStraping_CheckedChanged(object sender, EventArgs e)
        {
            if (RBStraping.Checked == true)
            {
                ChkCnC.Checked = false; PanelCnC2.Visible = false; ChkCnC.Visible = false;
                RBLogistik.Checked = true;
                RBStraping0.Checked = false;
                RBBevel0.Checked = false;
                RBRuningSaw0.Checked = false;
                RBPrint0.Checked = false;
                RBI990.Checked = false;
                LoadPartno();
                PanelSerah.Visible = true;
                RBOK.Enabled = true;
                RBBP.Enabled = true;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;
                RBOK.Checked = true;
                RBBP.Checked = false;
                txtPartnoOK.Visible = true;
                txtPartnoBP.Visible = false;
                txtQtyBP.Visible = false;
                LabelqtyBP.Visible = false;
                txtQtyOK.Visible = true;
                LabelqtyOK.Visible = true;
                txtQtyOK.Text = txtQtyTransfer.Text;
                LabelOK.Visible = true;
                txtlokOK.Visible = true;
                LabelOK0.Visible = false;
                txtlokBP.Visible = false;
            }
        }
        protected void RBRuningSaw0_CheckedChanged(object sender, EventArgs e)
        {
            if (RBRuningSaw0.Checked == true)
            {
                RBP99.Checked = false;
                RBI99.Checked = true;
                RBBevel.Checked = false;
                RBRuningSaw.Checked = false;
                RBPrint.Checked = false;
                RBStraping.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                RBKali13.Visible = true;
                RBKali14.Visible = true;
                //RBKali15.Visible = true; 
                txtPartnoTransfer.Visible = true;
            }
        }
        protected void RBBevel0_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBevel0.Checked == true)
            {
                RBP99.Checked = false;
                RBI99.Checked = false;
                RBBevel.Checked = false;
                RBRuningSaw.Checked = true;
                RBPrint.Checked = false;
                RBStraping.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;
            }
        }
        protected void RBPrint0_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPrint0.Checked == true)
            {
                RBP99.Checked = false;
                RBI99.Checked = false;
                //RBBevel.Checked = false;
                RBRuningSaw.Checked = true;
                RBPrint.Checked = false;
                RBStraping.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;
            }
        }
        protected void RBStraping0_CheckedChanged(object sender, EventArgs e)
        {
            if (RBStraping0.Checked == true)
            {
                RBP99.Checked = false;
                RBI99.Checked = false;
                RBBevel.Checked = true;
                RBRuningSaw.Checked = false;
                //RBPrint.Checked = true ;
                RBStraping.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
                PanelSerah.Visible = true;
                RBOK.Enabled = false;
                RBBP.Enabled = false;
                RBOK.Checked = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;
                RBBP.Checked = true;

                string tempPartno = string.Empty;
                string ukuran = string.Empty;
                string ukuran1 = string.Empty;
                string kwalitas = string.Empty;
                txtQtyOK.Text = "0";
                txtQtyBP.Text = "0";
                if (txtPartnoTransfer.Text != string.Empty)
                {
                    kwalitas = ddlPartno.SelectedItem.Text.Substring(3, 3);
                    tempPartno = txtPartnoTransfer.Text;
                    ukuran = ddlPartno.SelectedItem.Text.Substring(6, 11);
                    ukuran1 = txtPartnoOK.Text.Substring(6, 11);
                    if (ukuran != ukuran1)
                    {

                        DisplayAJAXMessage(this, "ukuran dan kwalitas partno asal dan partno transfer harus sama");
                        return;
                    }

                }
                txtPartnoOK.Visible = false;
                txtPartnoBP.Visible = true;
                txtQtyBP.Visible = true;
                LabelqtyBP.Visible = true;
                txtQtyOK.Visible = false;
                LabelqtyOK.Visible = false;
                txtQtyOK.Text = "0";
                txtQtyBP.Text = txtQtyTransfer.Text;
            }
        }
        protected void RBLogistik_CheckedChanged(object sender, EventArgs e)
        {
            if (RBLogistik.Checked == true && RBBevel.Checked == true)
            {
                string tempPartno = string.Empty;
                string ukuran = string.Empty;
                string ukuran1 = string.Empty;
                string kwalitas = string.Empty;
                txtQtyOK.Text = "0";
                txtQtyBP.Text = "0";
                if (txtPartnoTransfer.Text != string.Empty)
                {
                    kwalitas = ddlPartno.SelectedItem.Text.Substring(3, 3);
                    tempPartno = txtPartnoTransfer.Text;
                    ukuran = ddlPartno.SelectedItem.Text.Substring(6, 11);
                    ukuran1 = txtPartnoBP.Text.Substring(6, 11);
                    if (ukuran != ukuran1)
                    {
                        DisplayAJAXMessage(this, "ukuran dan kwalitas partno asal dan partno transfer harus sama");
                        return;
                    }

                }
                txtPartnoOK.Visible = false;
                txtPartnoBP.Visible = true;
                txtQtyBP.Visible = true;
                LabelqtyBP.Visible = true;
                txtQtyOK.Visible = false;
                LabelqtyOK.Visible = false;
                txtQtyOK.Text = "0";
                txtQtyBP.Text = txtQtyTransfer.Text;
                RBStraping0.Checked = false;
                RBLogistik.Checked = true;
                RBOK.Checked = false;
                RBBP.Checked = true;

                txtlokBP.Visible = true; txtlokBP.Enabled = true; txtlokBP.Text = string.Empty;
                LabelOK0.Visible = true; LabelOK0.Text = "Ke Lokasi : ";
            }
            if (RBLogistik.Checked == true && RBBevel.Checked == false && RBStraping.Checked == false)
            {
                RBStraping.Checked = true;
                RBStraping0.Checked = false;
                RBBevel0.Checked = false;
                RBRuningSaw0.Checked = false;
                RBPrint0.Checked = false;
                RBI990.Checked = false;
                LoadPartno();
                PanelSerah.Visible = true;
                RBOK.Enabled = false;
                RBBP.Enabled = false;
                RBKali13.Visible = false;
                RBKali14.Visible = false;
                //RBKali15.Visible = false; 
                txtPartnoTransfer.Visible = false;

            }
        }
        protected void RBI990_CheckedChanged(object sender, EventArgs e)
        {
            if (RBI990.Checked == true)
            {
                RBP99.Checked = true;
                RBI99.Checked = false;
                RBBevel.Checked = false;
                RBRuningSaw.Checked = false;
                RBPrint.Checked = false;
                RBStraping.Checked = false;
                LoadPartno();
                PanelSerah.Visible = false;
            }
        }
        protected void ddlPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataTransit();
            //try
            //{
            //    if (ddlPartno.Items.Count > 0)
            //    {
            //        string kwalitas = ddlPartno.SelectedItem.Text.Substring(3, 3);
            //        if (kwalitas == "-3-" && (RBPrint.Checked == true || RBStraping.Checked == true))
            //        {
            //            RBOK.Checked = true;
            //            RBBP.Checked = false;
            //            if (RBBevel.Checked == true)
            //            {
            //                RBRenovasi.Checked = false;
            //                RBStraping0.Checked = true;
            //            }
            //            txtPartnoOK.Visible = true;
            //            txtlokOK.Visible = true;
            //            LabelOK.Visible = true;
            //            txtPartnoBP.Visible = false;
            //            txtlokBP.Visible = false;
            //            LabelOK0.Visible = false;
            //            txtQtyBP.Visible = false;
            //            LabelqtyBP.Visible = false;
            //            txtQtyOK.Visible = true;
            //            LabelqtyOK.Visible = true;
            //            txtQtyOK.Text = txtQtyTransfer.Text;
            //            txtQtyBP.Text = "0";
            //        }
            //        else
            //        {
            //            RBBP.Checked = true;
            //            if (RBBevel.Checked == true)
            //            {
            //                RBRenovasi.Checked = true;
            //                RBStraping0.Checked = false;
            //            }
            //            RBOK.Checked = false;
            //            txtPartnoOK.Visible = false;
            //            txtlokOK.Visible = false;
            //            LabelOK.Visible = false;
            //            txtPartnoBP.Visible = true;
            //            txtlokBP.Visible = true;
            //            LabelOK0.Visible = true;
            //            txtQtyBP.Visible = true;
            //            LabelqtyBP.Visible = true;
            //            txtQtyOK.Visible = false;
            //            LabelqtyOK.Visible = false;
            //            txtQtyOK.Text = "0";
            //            txtQtyBP.Text = txtQtyTransfer.Text;
            //        }
            //    }
            //}
            //catch { }
        }
        protected void RBKali13_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlPartno.SelectedItem.Text.Trim() == string.Empty)
                return;
            string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
            string lebar = ddlPartno.SelectedItem.Text.Substring(6, 3);
            string sisi = string.Empty;

            if (kode == "INT" && lebar == "090")
                sisi = "B1";
            else
                sisi = "SE";
            if (RBI99.Checked == true || RBP99.Checked == true)
            {
                int kali = pengali();
                txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4);
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0100" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
            }
            else
            {
                txtQtyTransfer.Text = txtQtyOut.Text;
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
            }
        }
        protected void RBKali14_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlPartno.SelectedItem.Text.Trim() == string.Empty)
                return;
            string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
            string lebar = ddlPartno.SelectedItem.Text.Substring(6, 3);
            string sisi = string.Empty;

            if (kode == "INT" && lebar == "090")
                sisi = "B1";
            else
                sisi = "SE";
            if (RBI99.Checked == true || RBP99.Checked == true)
            {
                int kali = pengali();
                txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0200" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
            }
            else
            {
                txtQtyTransfer.Text = txtQtyOut.Text;
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
            }

        }
        protected void RBKali15_CheckedChanged(object sender, EventArgs e)
        {
            string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
            string lebar = ddlPartno.SelectedItem.Text.Substring(6, 3);
            string sisi = string.Empty;
            if (kode == "INT" && lebar == "090")
                sisi = "B1";
            else
                sisi = "SE";
            if (RBI99.Checked == true || RBP99.Checked == true)
            {
                int kali = pengali();
                txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4);
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + "0300" + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
            }
            else
            {
                txtQtyTransfer.Text = txtQtyOut.Text;
                txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
            }
        }
        protected void txtQtyOK_TextChanged(object sender, EventArgs e)
        {
            if (txtQtyOK.Text != string.Empty && Convert.ToInt32(txtQtyOK.Text) <= Convert.ToInt32(txtQtyTransfer.Text))
            {
                txtQtyBP.Text = (Convert.ToInt32(txtQtyTransfer.Text) - Convert.ToInt32(txtQtyOK.Text)).ToString();
                txtQtyOK.Text = (Convert.ToInt32(txtQtyTransfer.Text) - Convert.ToInt32(txtQtyBP.Text)).ToString();
            }
            else
            {
                txtQtyBP.Text = "0";
                txtQtyOK.Text = (Convert.ToInt32(txtQtyTransfer.Text)).ToString();
            }
        }
        protected void txtQtyBP_TextChanged(object sender, EventArgs e)
        {
            if (txtQtyBP.Text != string.Empty && Convert.ToInt32(txtQtyBP.Text) <= Convert.ToInt32(txtQtyTransfer.Text))
            {
                txtQtyOK.Text = (Convert.ToInt32(txtQtyTransfer.Text) - Convert.ToInt32(txtQtyBP.Text)).ToString();
                txtQtyBP.Text = (Convert.ToInt32(txtQtyTransfer.Text) - Convert.ToInt32(txtQtyOK.Text)).ToString();
            }
            else
            {
                txtQtyBP.Text = "0";
                txtQtyOK.Text = (Convert.ToInt32(txtQtyTransfer.Text)).ToString();
            }
        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Renovasi 3";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Runing Saw R3";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Bevel R3";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Straping R3";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Proses lispank " + DateTime.Now.ToString("ddMMyyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GrdDynamic.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void RBKaliNSTD_CheckedChanged(object sender, EventArgs e)
        {
            string kode = ddlPartno.SelectedItem.Text.Substring(0, 3);
            string tebal = ddlPartno.SelectedItem.Text.Substring(6, 3);
            string sisi = string.Empty;
            if (tebal == "090")
                sisi = "B1";
            else
                sisi = "SE";
            string lebar = string.Empty;
            RBKaliNSTD.Checked = true;
            if (txtPanjangNStd.Text.Trim() != string.Empty)
            {
                lebar = Int32.Parse(txtPanjangNStd.Text.Trim()).ToString().PadLeft(4, '0');
                if (RBI99.Checked == true || RBP99.Checked == true)
                {
                    int kali = pengali();
                    txtQtyTransfer.Text = (kali * Convert.ToInt32(txtQtyOut.Text)).ToString();
                    txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text.Substring(0, 9) + lebar + ddlPartno.SelectedItem.Text.Substring(13, 4) + sisi;
                    txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + lebar + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                    txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 3) + lebar + ddlPartno.SelectedItem.Text.Substring(13, 4)).Trim() + sisi;
                }
                else
                {
                    txtQtyTransfer.Text = txtQtyOut.Text;
                    txtPartnoTransfer.Text = ddlPartno.SelectedItem.Text;
                    txtPartnoOK.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-3-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                    txtPartnoBP.Text = (ddlPartno.SelectedItem.Text.Substring(0, 3) + "-P-" + ddlPartno.SelectedItem.Text.Substring(6, 11)).Trim() + sisi;
                }
            }
        }
        protected void txtQtyTransfer_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ChkCnC_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCnC.Checked == true)
            {
                PanelCnC2.Visible = true; Panel9.Visible = false; Panel1.Visible = false;
                txtPartnoTransfer.Visible = false; txtQtyTransfer.Visible = false;

                if (RBI99.Checked == true && ChkCnC.Checked == true)
                {
                    txtPartnoCnC.Text = ddlPartno.SelectedItem.ToString();
                    txtPartnoTCnC.Text = txtQtyOut.Text; txtLokasi.Text = "AA13";
                }
                else if (RBRuningSaw.Checked == true && ChkCnC.Checked == true)
                {
                    txtPartnoCnC.Text = ddlPartno.SelectedItem.ToString();
                    txtPartnoTCnC.Text = txtQtyOut.Text;
                    txtLokasi.Text = "AA13";
                }
                else if (RBBevel.Checked == true && ChkCnC.Checked == true)
                {
                    txtPartnoCnC.Text = ddlPartno.SelectedItem.ToString();
                    txtPartnoTCnC.Text = txtQtyOut.Text;
                    txtLokasi.Text = "AA13";
                }
            }
            else if (ChkCnC.Checked == false)
            {
                PanelCnC2.Visible = false; Panel1.Visible = true;
                txtPartnoTransfer.Visible = true; txtQtyTransfer.Visible = true;
            }
        }
    }
}