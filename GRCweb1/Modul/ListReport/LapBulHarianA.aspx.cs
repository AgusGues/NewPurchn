using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapBulHarianA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                LoadTipeSPP();
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            txtToPostingPeriod.Text = DateTime.Now.Date.ToString();
            if (ddlTipeSPP.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Tipe Laporan");
                return;
            }
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");

            DateTime tgl1 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);// DateTime.Parse(txtFromPostingPeriod.Text).Day - 1);
            DateTime tgl2 = new DateTime();
            if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
                tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);
            else
                tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Day - 1);

            string tglAwal = tgl1.ToString("yyyyMMdd");
            string tglAkhir = tgl2.ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtFromPostingPeriod.Text;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;

            int bikinID = 0;

            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["lapbul"] = null;
            Session["jenis"] = null;

            string ketBlnLalu = string.Empty;
            if (blnLalu - 1 == 0)
            {
                ketBlnLalu = "DesQty";
                thn = thn - 1;
            }
            else if (blnLalu - 1 == 1)
                ketBlnLalu = "JanQty";
            else if (blnLalu - 1 == 2)
                ketBlnLalu = "FebQty";
            else if (blnLalu - 1 == 3)
                ketBlnLalu = "MarQty";
            else if (blnLalu - 1 == 4)
                ketBlnLalu = "AprQty";
            else if (blnLalu - 1 == 5)
                ketBlnLalu = "MeiQty";
            else if (blnLalu - 1 == 6)
                ketBlnLalu = "JunQty";
            else if (blnLalu - 1 == 7)
                ketBlnLalu = "JulQty";
            else if (blnLalu - 1 == 8)
                ketBlnLalu = "AguQty";
            else if (blnLalu - 1 == 9)
                ketBlnLalu = "SepQty";
            else if (blnLalu - 1 == 10)
                ketBlnLalu = "OktQty";
            else if (blnLalu - 1 == 11)
                ketBlnLalu = "NovQty";


            LapHarian lapHarian = new LapHarian();

            int userID = ((Users)Session["Users"]).ID;
            //    int groupID = int.Parse(ddlTipeSPP.SelectedValue);
            int groupID = int.Parse(ddlTipeSPP.SelectedValue);
            int itemTypeID = 0;

            lapHarian.UserID = ((Users)Session["Users"]).ID;
            lapHarian.KodeLaporan = ddlTipeSPP.SelectedItem.ToString();
            lapHarian.DariTgl = DateTime.Parse(txtFromPostingPeriod.Text);
            lapHarian.SdTgl = DateTime.Parse(txtToPostingPeriod.Text);
            //lapHarian.TglCetak = DateTime.Now;
            lapHarian.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
            lapHarian.BikinID = 0;
            lapHarian.TglCetak = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss.fffff");

            //DateTime awalReport = DateTime.Now;
            string awalReport = lapHarian.TglCetak;

            //delete / kosongkan data dahulu
            //more spesifikasi / detail agar data orang laein tdk kehapus

            LapHarianProcessFacade kosongkanProcessFacade = new LapHarianProcessFacade(lapHarian, new ArrayList());
            strError = kosongkanProcessFacade.Delete();
            if (strError != string.Empty)
            {
                DisplayAJAXMessage(this, "Error kosongkan data report");
                return;
            }

            ArrayList arrInv = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            if (ddlTipeSPP.SelectedItem.ToString() == "Asset")
            {
                arrInv = inventoryFacade.RetrieveByGroupIDForAsset(int.Parse(ddlTipeSPP.SelectedValue), 2);
                itemTypeID = 2;
            }
            else if (ddlTipeSPP.SelectedItem.ToString() == "Biaya")
            {
                arrInv = inventoryFacade.RetrieveByGroupIDForBiaya(int.Parse(ddlTipeSPP.SelectedValue), 3);
                itemTypeID = 3;
            }
            else
            {
                arrInv = inventoryFacade.RetrieveByGroupID((ddlTipeSPP.SelectedValue), 1);
                itemTypeID = 1;
            }

            if (inventoryFacade.Error == string.Empty && arrInv.Count > 0)
            {
                //update no urut = 1 / saldo awal dulu            
                LapHarianProcessFacade invProcessFacade = new LapHarianProcessFacade(lapHarian, arrInv);

                //add here for repack
                if (groupID == 10)
                {
                    if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
                        strError = invProcessFacade.InsertSaldoInventoryRePack1(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
                    else
                        strError = invProcessFacade.InsertSaldoInventoryRePack(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
                }
                //until here
                else
                {

                    if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
                        strError = invProcessFacade.InsertSaldoInventory1(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
                    else
                        strError = invProcessFacade.InsertSaldoInventory(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
                }
                //
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Error transfer data inventori");
                    return;
                }
                else
                    bikinID = invProcessFacade.BikinanID;

            }

            if (groupID == 10 || groupID == 13)
            {
                //utk convertan
                ArrayList arrConvertan = new ArrayList();
                ConvertanFacade convertanFacade = new ConvertanFacade();
                lapHarian.BikinID = bikinID;

                arrConvertan = convertanFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
                if (convertanFacade.Error == string.Empty && arrConvertan.Count > 0)
                {
                    LapHarianProcessFacade receiptProcessFacade = new LapHarianProcessFacade(lapHarian, arrConvertan);
                    strError = receiptProcessFacade.InsertConvertan();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Error transfer data convertan");
                        return;
                    }
                    else
                        bikinID = receiptProcessFacade.BikinanID;
                }
                //until convertan

                //utk pemasukan / receipt
                ArrayList arrReceipt = new ArrayList();
                ReceiptFacade receiptFacade = new ReceiptFacade();
                lapHarian.BikinID = bikinID;

                //arrReceipt = receiptFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
                arrReceipt = receiptFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
                if (receiptFacade.Error == string.Empty && arrReceipt.Count > 0)
                {
                    LapHarianProcessFacade receiptProcessFacade = new LapHarianProcessFacade(lapHarian, arrReceipt);
                    strError = receiptProcessFacade.InsertReceipt();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Error transfer data receipt");
                        return;
                    }
                    else
                        bikinID = receiptProcessFacade.BikinanID;
                }


            }
            else
            {
                //utk pemasukan / receipt
                ArrayList arrReceipt = new ArrayList();
                ReceiptFacade receiptFacade = new ReceiptFacade();
                lapHarian.BikinID = bikinID;

                //arrReceipt = receiptFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
                arrReceipt = receiptFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
                if (receiptFacade.Error == string.Empty && arrReceipt.Count > 0)
                {
                    LapHarianProcessFacade receiptProcessFacade = new LapHarianProcessFacade(lapHarian, arrReceipt);
                    strError = receiptProcessFacade.InsertReceipt();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Error transfer data receipt");
                        return;
                    }
                    else
                        bikinID = receiptProcessFacade.BikinanID;
                }
            }

            //utk retur
            ArrayList arrReturPakai = new ArrayList();
            ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();

            lapHarian.BikinID = bikinID;

            //arrReturPakai = returPakaiFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
            arrReturPakai = returPakaiFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
            if (returPakaiFacade.Error == string.Empty && arrReturPakai.Count > 0)
            {
                LapHarianProcessFacade returPakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrReturPakai);
                strError = returPakaiProcessFacade.InsertReturPakai();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Error transfer data retur");
                    return;
                }
                else
                    bikinID = returPakaiProcessFacade.BikinanID;
            }

            //utk pemakaian
            ArrayList arrPakai = new ArrayList();
            PakaiFacade pakaiFacade = new PakaiFacade();


            lapHarian.BikinID = bikinID;
            arrPakai = pakaiFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
            if (pakaiFacade.Error == string.Empty && arrPakai.Count > 0)
            {
                LapHarianProcessFacade pakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrPakai);
                strError = pakaiProcessFacade.InsertPakai();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Error transfer data pemakaian");
                    return;
                }
                else
                    bikinID = pakaiProcessFacade.BikinanID;
            }



            //utk Adjustment
            ArrayList arrAdjust = new ArrayList();
            AdjustFacade adjustFacade = new AdjustFacade();
            lapHarian.BikinID = bikinID;
            arrAdjust = adjustFacade.RetrieveBySamaTgl2(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
            if (adjustFacade.Error == string.Empty && arrAdjust.Count > 0)
            {
                LapHarianProcessFacade adjustProcessFacade = new LapHarianProcessFacade(lapHarian, arrAdjust);
                strError = adjustProcessFacade.InsertAdjust();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Error transfer data adjust");
                    return;
                }
            }

            DateTime akhirReport = DateTime.Now;
            akhirReport = akhirReport + new TimeSpan(0, 0, 1);

            ReportFacade reportFacade = new ReportFacade();
            Session["formno"] = " ";
            string strQuery = reportFacade.ViewHarianA(userID, groupID, awalReport);
            if (groupID == 7)
            {
                if (users.UnitKerjaID == 1)
                    Session["formno"] = "Form No : LOG/LHAM/55/11/R1";
            }
            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["lapbul"] = ddlTipeSPP.SelectedItem;
            Session["group"] = ddlTipeSPP.SelectedValue;
            Session["blmApprove"] = CountNotApproval(DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd"), int.Parse(ddlTipeSPP.SelectedValue));
            Session["jenis"] = null;
            Cetak(this);
        }


        //protected void btnPrint_ServerClick(object sender, EventArgs e)
        //{
        //    txtToPostingPeriod.Text = DateTime.Now.Date.ToString();

        //    if (ddlTipeSPP.SelectedIndex == 0)
        //    {
        //        DisplayAJAXMessage(this, "Pilih Tipe Laporan");
        //        return;
        //    }
        //    if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
        //    {
        //        DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
        //        return;
        //    }
        //    string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
        //    string periodeAkhir = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");

        //    string txPeriodeAwal = txtFromPostingPeriod.Text;
        //    string txPeriodeAkhir = txtFromPostingPeriod.Text;
        //    DateTime tgl1 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);
        //    DateTime tgl2 = new DateTime();
        //    if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
        //        tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);
        //    else
        //        tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Day - 1);

        //    string tglAwal = tgl1.ToString("yyyyMMdd");
        //    string tglAkhir = tgl2.ToString("yyyyMMdd");

        //    string strError = string.Empty;
        //    int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
        //    int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;

        //    int bikinID = 0;

        //    Session["Query"] = null;
        //    Session["prdawal"] = null;
        //    Session["prdakhir"] = null;
        //    Session["lapbul"] = null;
        //    Session["jenis"] = null;

        //    string ketBlnLalu = string.Empty;
        //    if (blnLalu - 1 == 0)
        //    {
        //        ketBlnLalu = "DesQty";
        //        thn = thn - 1;
        //    }
        //    else if (blnLalu - 1 == 1)
        //        ketBlnLalu = "JanQty";
        //    else if (blnLalu - 1 == 2)
        //        ketBlnLalu = "FebQty";
        //    else if (blnLalu - 1 == 3)
        //        ketBlnLalu = "MarQty";
        //    else if (blnLalu - 1 == 4)
        //        ketBlnLalu = "AprQty";
        //    else if (blnLalu - 1 == 5)
        //        ketBlnLalu = "MeiQty";
        //    else if (blnLalu - 1 == 6)
        //        ketBlnLalu = "JunQty";
        //    else if (blnLalu - 1 == 7)
        //        ketBlnLalu = "JulQty";
        //    else if (blnLalu - 1 == 8)
        //        ketBlnLalu = "AguQty";
        //    else if (blnLalu - 1 == 9)
        //        ketBlnLalu = "SepQty";
        //    else if (blnLalu - 1 == 10)
        //        ketBlnLalu = "OktQty";
        //    else if (blnLalu - 1 == 11)
        //        ketBlnLalu = "NovQty";

        //    LapHarian lapHarian = new LapHarian();

        //    int userID = ((Users)Session["Users"]).ID;
        //    int groupID = int.Parse(ddlTipeSPP.SelectedValue);
        //    int itemTypeID = 0;

        //    lapHarian.UserID = ((Users)Session["Users"]).ID;
        //    lapHarian.KodeLaporan = ddlTipeSPP.SelectedItem.ToString();
        //    lapHarian.DariTgl = DateTime.Parse(txtFromPostingPeriod.Text);
        //    lapHarian.SdTgl = DateTime.Parse(txtToPostingPeriod.Text);

        //    //lapHarian.TglCetak = DateTime.Now;
        //    lapHarian.GroupID = int.Parse(ddlTipeSPP.SelectedValue);
        //    //lapHarian.BikinID = 0;
        //    //lapHarian.TglCetak = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss.fffff");

        //    ////DateTime awalReport = DateTime.Now;
        //    //string awalReport = lapHarian.TglCetak; 

        //    ////delete / kosongkan data dahulu
        //    ////more spesifikasi / detail agar data orang laein tdk kehapus

        //    //LapHarianProcessFacade kosongkanProcessFacade = new LapHarianProcessFacade(lapHarian, new ArrayList());
        //    //strError = kosongkanProcessFacade.Delete();
        //    //if (strError != string.Empty)
        //    //{
        //    //    DisplayAJAXMessage(this, "Error kosongkan data report");
        //    //    return;
        //    //}

        //    //ArrayList arrInv = new ArrayList();
        //    //InventoryFacade inventoryFacade = new InventoryFacade();
        //    //if (ddlTipeSPP.SelectedItem.ToString() == "Asset")
        //    //{
        //    //    arrInv = inventoryFacade.RetrieveByGroupIDForAsset(int.Parse(ddlTipeSPP.SelectedValue), 2);
        //    //    itemTypeID = 2;
        //    //}
        //    //else if (ddlTipeSPP.SelectedItem.ToString() == "Biaya")
        //    //{
        //    //    arrInv = inventoryFacade.RetrieveByGroupIDForBiaya(int.Parse(ddlTipeSPP.SelectedValue), 3);
        //    //    itemTypeID = 3;
        //    //}
        //    //else
        //    //{
        //    //    arrInv = inventoryFacade.RetrieveByGroupID(int.Parse(ddlTipeSPP.SelectedValue), 1);
        //    //    itemTypeID = 1;
        //    //}

        //    //if (inventoryFacade.Error == string.Empty && arrInv.Count > 0)
        //    //{
        //    //    //update no urut = 1
        //    //    LapHarianProcessFacade invProcessFacade = new LapHarianProcessFacade(lapHarian, arrInv);
        //    //    strError = invProcessFacade.InsertInventory();
        //    //    if (strError != string.Empty)
        //    //    {
        //    //        DisplayAJAXMessage(this, "Error transfer data inventori");
        //    //        return;
        //    //    }
        //    //    else
        //    //        bikinID = invProcessFacade.BikinanID;

        //    //}

        //    ////utk pemakaian
        //    //ArrayList arrPakai = new ArrayList();
        //    //PakaiFacade pakaiFacade = new PakaiFacade();
        //    //if (DateTime.Parse(txtFromPostingPeriod.Text).Day - 1 > 0)
        //    //{
        //    //    //update kurang ke stok awal & urutan
        //    //    arrPakai = pakaiFacade.RetrieveByKurangTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
        //    //    if (pakaiFacade.Error == string.Empty && arrPakai.Count > 0)
        //    //    {
        //    //        LapHarianProcessFacade pakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrPakai);
        //    //        strError = pakaiProcessFacade.InsertPakaiKeAwal();
        //    //        if (strError != string.Empty)
        //    //        {
        //    //            DisplayAJAXMessage(this, "Error transfer data pemakaian ke stok-awal");
        //    //            return;
        //    //        }
        //    //    }

        //    //}

        //    //lapHarian.BikinID = bikinID;

        //    //arrPakai = pakaiFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
        //    //if (pakaiFacade.Error == string.Empty && arrPakai.Count > 0)
        //    //{
        //    //    //insert ke pemakaian & urutan & NoDoc
        //    //    LapHarianProcessFacade pakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrPakai);
        //    //    strError = pakaiProcessFacade.InsertPakai();
        //    //    if (strError != string.Empty)
        //    //    {
        //    //        DisplayAJAXMessage(this, "Error transfer data pemakaian");
        //    //        return;
        //    //    }
        //    //    else
        //    //        bikinID = pakaiProcessFacade.BikinanID;
        //    //}

        //    ////utk pemasukan / receipt
        //    //ArrayList arrReceipt = new ArrayList();
        //    //ReceiptFacade receiptFacade = new ReceiptFacade();
        //    //if (DateTime.Parse(txtFromPostingPeriod.Text).Day - 1 > 0)
        //    //{
        //    //    arrReceipt = receiptFacade.RetrieveByKurangTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
        //    //    if (receiptFacade.Error == string.Empty && arrReceipt.Count > 0)
        //    //    {
        //    //        //update ke stok awal & update urutan
        //    //        LapHarianProcessFacade receiptProcessFacade = new LapHarianProcessFacade(lapHarian, arrReceipt);
        //    //        strError = receiptProcessFacade.InsertReceiptKeAwal();
        //    //        if (strError != string.Empty)
        //    //        {
        //    //            DisplayAJAXMessage(this, "Error transfer data receipt ke stok-awal");
        //    //            return;
        //    //        }
        //    //    }
        //    //}

        //    //lapHarian.BikinID = bikinID;

        //    //arrReceipt = receiptFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID, groupID);
        //    //if (receiptFacade.Error == string.Empty && arrReceipt.Count > 0)
        //    //{
        //    //    LapHarianProcessFacade receiptProcessFacade = new LapHarianProcessFacade(lapHarian, arrReceipt);
        //    //    strError = receiptProcessFacade.InsertReceipt();
        //    //    if (strError != string.Empty)
        //    //    {
        //    //        DisplayAJAXMessage(this, "Error transfer data receipt");
        //    //        return;
        //    //    }
        //    //    else
        //    //        bikinID = receiptProcessFacade.BikinanID;
        //    //}

        //    ////utk retur
        //    //ArrayList arrReturPakai = new ArrayList();
        //    //ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();
        //    //if (DateTime.Parse(txtFromPostingPeriod.Text).Day - 1 > 0)
        //    //{
        //    //    arrReturPakai = returPakaiFacade.RetrieveByKurangTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
        //    //    if (returPakaiFacade.Error == string.Empty && arrReturPakai.Count > 0)
        //    //    {
        //    //        LapHarianProcessFacade returPakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrReturPakai);
        //    //        strError = returPakaiProcessFacade.InsertReturPakaiKeAwal();
        //    //        if (strError != string.Empty)
        //    //        {
        //    //            DisplayAJAXMessage(this, "Error transfer data retur ke stok-awal");
        //    //            return;
        //    //        }
        //    //    }

        //    //}

        //    //lapHarian.BikinID = bikinID;

        //    //arrReturPakai = returPakaiFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
        //    //if (returPakaiFacade.Error == string.Empty && arrReturPakai.Count > 0)
        //    //{
        //    //    LapHarianProcessFacade returPakaiProcessFacade = new LapHarianProcessFacade(lapHarian, arrReturPakai);
        //    //    strError = returPakaiProcessFacade.InsertReturPakai();
        //    //    if (strError != string.Empty)
        //    //    {
        //    //        DisplayAJAXMessage(this, "Error transfer data retur");
        //    //        return;
        //    //    }
        //    //    else
        //    //        bikinID = returPakaiProcessFacade.BikinanID;
        //    //}

        //    ////utk Adjustment
        //    //ArrayList arrAdjust = new ArrayList();
        //    //AdjustFacade adjustFacade = new AdjustFacade();
        //    //if (DateTime.Parse(txtFromPostingPeriod.Text).Day - 1 > 0)
        //    //{
        //    //    arrAdjust = adjustFacade.RetrieveByKurangTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
        //    //    if (adjustFacade.Error == string.Empty && arrAdjust.Count > 0)
        //    //    {
        //    //        LapHarianProcessFacade adjustProcessFacade = new LapHarianProcessFacade(lapHarian, arrAdjust);
        //    //        strError = adjustProcessFacade.InsertAdjustKeAwal();
        //    //        if (strError != string.Empty)
        //    //        {
        //    //            DisplayAJAXMessage(this, "Error transfer data adjust ke stok-awal");
        //    //            return;
        //    //        }
        //    //    }

        //    //}

        //    //lapHarian.BikinID = bikinID;

        //    //arrAdjust = adjustFacade.RetrieveBySamaTgl(DateTime.Parse(txtFromPostingPeriod.Text).Day, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Year, itemTypeID);
        //    //if (adjustFacade.Error == string.Empty && arrAdjust.Count > 0)
        //    //{
        //    //    LapHarianProcessFacade adjustProcessFacade = new LapHarianProcessFacade(lapHarian, arrAdjust);
        //    //    strError = adjustProcessFacade.InsertAdjust();
        //    //    if (strError != string.Empty)
        //    //    {
        //    //        DisplayAJAXMessage(this, "Error transfer data adjust");
        //    //        return;
        //    //    }
        //    //}

        //    //DateTime akhirReport = DateTime.Now;
        //    //akhirReport = akhirReport + new TimeSpan(0, 0, 1);

        //    ReportFacade reportFacade = new ReportFacade();

        //    string strQuery = string.Empty;

        //    if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
        //        strQuery = reportFacade.ViewHarian5a(ketBlnLalu, thn,tglAwal,tglAkhir, periodeAwal, periodeAkhir, groupID);
        //    else
        //        strQuery = reportFacade.ViewHarian5(ketBlnLalu, thn,tglAwal,tglAkhir, periodeAwal, periodeAkhir, groupID);

        //    //string strQuery = reportFacade.ViewHarianA3(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);

        //    Session["Query"] = strQuery;
        //    Session["prdawal"] = txPeriodeAwal;
        //    Session["prdakhir"] = txPeriodeAkhir;
        //    Session["lapbul"] = ddlTipeSPP.SelectedItem;
        //    Session["jenis"] = null;

        //    Cetak(this);



        //}

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapLapHarian', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }

            ddlTipeSPP.SelectedIndex = 1;
        }
        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTipeSPP.SelectedIndex > 0)
            //{
            //    if (ddlTipeSPP.SelectedIndex == 5)
            //        ddlTipeBarang.SelectedIndex = 2;
            //    else if (ddlTipeSPP.SelectedIndex == 6)
            //        ddlTipeBarang.SelectedIndex = 3;
            //    else
            //        ddlTipeBarang.SelectedIndex = 1;
            //}
            //else
            //    ddlTipeBarang.SelectedIndex = 0;

        }
        private int CountNotApproval(string Tanggal, int GroupID)
        {
            int result = 0;
            string strSQl = "select COUNT(ID)Jml from Pakai where Status <2 and Status >-1 and CONVERT(char,PakaiDate,112) ='" + Tanggal + "' and PakaiTipe=" + GroupID;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQl);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Jml"].ToString());
                }
            }
            return result;
        }

    }
}