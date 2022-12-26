using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapBulAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            string strFirst = "1/1/" + DateTime.Now.Year.ToString();
            DateTime dateFirst = DateTime.Parse(strFirst);
            if (!Page.IsPostBack)
            {
                LoadTipeSPP();
                LoadTahun();
                //txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                //txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtTahun.SelectedValue = DateTime.Now.Year.ToString();
                rbOff.Checked = true;
                //txtTahun.Text = DateTime.Now.Year.ToString();
                if (ddlBulan.SelectedIndex > 0)
                {
                    ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex - 1;
                    txtTahun0.Text = txtTahun.Text;
                }
                else
                {
                    ddlBulan0.SelectedIndex = 11;
                    txtTahun0.Text = (Convert.ToInt16(txtTahun.Text) - 1).ToString();
                }
                txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
                CekPeriodPosting();
            }
        }
        private void LoadTahun()
        {
            ArrayList arrSI = new ArrayList();
            PakaiFacade pakai = new PakaiFacade();
            arrSI = pakai.GetYearTrans();
            txtTahun.Items.Clear();
            foreach (Pakai objSI in arrSI)
            {
                txtTahun.Items.Add(new ListItem(objSI.Tahun.ToString(), objSI.Tahun.ToString()));
            }

        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //Users users = (Users)Session["Users"];
            //string UnitKerja = users.UnitKerjaID.ToString();
            //Session["UnitKerja"] = UnitKerja;

            if (ddlTipeSPP.SelectedIndex == 0) return;
            //check periode closing accounting jika sudah di close tidak bisa posting ulang
            AccClosingFacade Closing = new AccClosingFacade();
            string objClosing = Closing.CheckClosing(ddlBulan.SelectedIndex, int.Parse(txtTahun.SelectedValue));
            PanelPost.Visible = (objClosing == ddlBulan.SelectedValue + txtTahun.SelectedValue) ? false : true;

            //if (DateTime.Now < Convert.ToDateTime(txtToPostingPeriod.Text).AddDays(29))
            //    PanelPost.Visible = true;
            //else
            //    PanelPost.Visible = false;

            if (PanelPost.Visible == true && rbOn.Checked == true) Posting();

            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            string frmtPrint = string.Empty;
            if (ddlFormatPrint.SelectedIndex == 0)    // Potrait
            {
                frmtPrint = "Potrait";
            }
            else
            {
                frmtPrint = "LandScape";
            }
            int intPilihLapbul = Convert.ToUInt16(ddlTipeSPP.SelectedValue);
            string pilihLapbul = ddlJenisLapBul.SelectedValue.ToString();
            Users users = (Users)Session["Users"];
            string UnitKerja = users.UnitKerjaID.ToString();
            Session["UnitKerja"] = UnitKerja;

            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            string deptname = (dept.DeptName.Length > 3) ? dept.DeptName.Substring(0, 3).ToUpper() : dept.DeptName.ToString().ToUpper();

            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["lapbul"] = null;
            Session["jenis"] = null;
            Session["price"] = 0;// users.ViewPrice;
            Session["FormatCetak"] = frmtPrint;
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
            int userID = ((Users)Session["Users"]).ID;
            int groupID = int.Parse(ddlTipeSPP.SelectedValue);
            //int itemTypeID = 0;

            string ketBlnLalu = string.Empty;
            string PriceBlnLalu = string.Empty;
            if (blnLalu - 1 == 0)
            {
                ketBlnLalu = "DesQty";
                PriceBlnLalu = "DesAvgPrice";
                thn = thn - 1;
            }
            else if (blnLalu - 1 == 1)
            {
                ketBlnLalu = "JanQty";
                PriceBlnLalu = "JanAvgPrice";
            }
            else if (blnLalu - 1 == 2)
            {
                ketBlnLalu = "FebQty"; PriceBlnLalu = "FebAvgPrice";
            }
            else if (blnLalu - 1 == 3)
            {
                ketBlnLalu = "MarQty"; PriceBlnLalu = "MarAvgPrice";
            }
            else if (blnLalu - 1 == 4)
            { ketBlnLalu = "AprQty"; PriceBlnLalu = "AprAvgPrice"; }
            else if (blnLalu - 1 == 5)
            { ketBlnLalu = "MeiQty"; PriceBlnLalu = "MeiAvgPrice"; }
            else if (blnLalu - 1 == 6)
            { ketBlnLalu = "JunQty"; PriceBlnLalu = "JunAvgPrice"; }
            else if (blnLalu - 1 == 7)
            { ketBlnLalu = "JulQty"; PriceBlnLalu = "JulAvgPrice"; }
            else if (blnLalu - 1 == 8)
            { ketBlnLalu = "AguQty"; PriceBlnLalu = "AguAvgPrice"; }
            else if (blnLalu - 1 == 9)
            { ketBlnLalu = "SepQty"; PriceBlnLalu = "SepAvgPrice"; }
            else if (blnLalu - 1 == 10)
            { ketBlnLalu = "OktQty"; PriceBlnLalu = "OktAvgPrice"; }
            else if (blnLalu - 1 == 11)
            { ketBlnLalu = "NovQty"; PriceBlnLalu = "NovAvgPrice"; }


            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;
            string stock = string.Empty;
            if (pilihLapbul == "All")
            {
                stock = " ";
            }
            if (pilihLapbul == "Stock")
            {
                stock = "1";
                //strQuery = reportFacade.ViewLapBul2stock(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue));
            }
            if (pilihLapbul == "Non Stock")
            {
                stock = "0";
                //strQuery = reportFacade.ViewLapBul2nonstock(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue));
            }

            if (intPilihLapbul == 10)
            {
                strQuery = reportFacade.ViewLapBul2ForRepackOnly(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
            }
            else if (intPilihLapbul == 3)
            {
                strQuery = reportFacade.ViewLapBul2ForAtkOnly(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
            }
            else
            {
                //if (deptname == "FINA" || deptname == "EDP" || deptname == "ACC")
                if (dept.ID == 14 || dept.ID == 24 && users.ViewPrice > 0)
                {
                    Session["price"] = users.ViewPrice;
                    ReportFacadeAcc reportFacadeacc = new ReportFacadeAcc();
                    #region Prepared Data
                    string bln = ddlBulan.SelectedValue;
                    string FirstPeriod = string.Empty;
                    string LastPeriod = string.Empty;
                    string blne = bln.ToString();
                    string s = new string('0', (2 - bln.Length));
                    Int32 lastDay = DateTime.DaysInMonth(Convert.ToInt32(thn), Convert.ToInt32(blne));
                    string d = new string('0', (2 - lastDay.ToString().Length));
                    string jnStok = ddlTipeSPP.SelectedValue;


                    FirstPeriod = thn + s + bln + "01";
                    LastPeriod = thn + s + bln + d + lastDay;
                    string Dari = FirstPeriod;
                    string Tahun = thn.ToString();
                    string GroupPurch = jnStok;
                    string GrpID = GroupPurch;
                    string strSQL = string.Empty;
                    string SaldoLaluQty = string.Empty;
                    string SaldoLaluPrice = string.Empty;
                    string periodeTahun = string.Empty;
                    int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
                    string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
                    string periodeBlnThn = Dari.Substring(0, 6).ToString();
                    switch (fldBln)
                    {
                        case 1:
                            SaldoLaluQty = "DesQty";
                            SaldoLaluPrice = "DesAvgPrice";
                            periodeTahun = (Convert.ToInt16(Tahun) - 1).ToString();
                            break;
                        case 2:
                            SaldoLaluQty = "JanQty";
                            SaldoLaluPrice = "JanAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 3:
                            SaldoLaluQty = "FebQty";
                            SaldoLaluPrice = "FebAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 4:
                            SaldoLaluQty = "MarQty";
                            SaldoLaluPrice = "MarAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 5:
                            SaldoLaluQty = "AprQty";
                            SaldoLaluPrice = "AprAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 6:
                            SaldoLaluQty = "MeiQty";
                            SaldoLaluPrice = "MeiAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 7:
                            SaldoLaluQty = "JunQty";
                            SaldoLaluPrice = "JunAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 8:
                            SaldoLaluQty = "JulQty";
                            SaldoLaluPrice = "JulAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 9:
                            SaldoLaluQty = "AguQty";
                            SaldoLaluPrice = "AguAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 10:
                            SaldoLaluQty = "SepQty";
                            SaldoLaluPrice = "SepAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 11:
                            SaldoLaluQty = "OktQty";
                            SaldoLaluPrice = "OktAvgPrice";
                            periodeTahun = Tahun;
                            break;
                        case 12:
                            SaldoLaluQty = "NovQty";
                            SaldoLaluPrice = "NovAvgPrice";
                            periodeTahun = Tahun;
                            break;
                    }
                    #endregion
                    string strquery0 = reportFacadeacc.ViewMutasiStockLapbul(FirstPeriod, LastPeriod, jnStok, thn.ToString());
                    strQuery = strquery0 + reportFacade.ViewLapBul2VP(PriceBlnLalu, ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
                    //strQuery = reportFacade.ViewLapBul2(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
                }
                else
                {
                    Session["price"] = 0;
                    strQuery = reportFacade.ViewLapBul2(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
                }
            }

            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["lapbul"] = JudulLapBul(ddlTipeSPP.SelectedItem.ToString());
            Session["pilihlapbul"] = pilihLapbul;
            Session["jenis"] = null;
            Session["stock"] = stock;
            Session["groupid"] = groupID;

            string LapBul = "LapBul";
            Session["Elap"] = LapBul;

            //new
            //Session["asset"] = intPilihLapbul;
            Session["asset"] = 0;
            //jika tidak jadi ganti report untuk asset ganti jadi : Session["asset"] =0
            Cetak(this);
        }
        public string JudulLapBul(string Jenis)
        {
            string judul = string.Empty;
            switch (Jenis)
            {
                case "Mekanik":
                    judul = "Sparepart";
                    break;
                default:
                    judul = Jenis;
                    break;
            }
            return judul;
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapLapBul', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
            //ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string jenisLap = ddlTipeSPP.SelectedValue;
            //int xjenisLap2 = ddlTipeSPP.SelectedIndex;


        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
            txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
            if (ddlBulan.SelectedIndex > 0)
            {
                ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex;
                txtTahun0.Text = txtTahun.Text;
            }
            else
            {
                ddlBulan0.SelectedIndex = 12;
                txtTahun0.Text = (Convert.ToInt16(txtTahun.Text) - 1).ToString();
            }
            CekPeriodPosting();
        }
        protected void TxtTahun_TextChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
            txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
        }

        private void Posting()
        {
            string ThBl = txtTahun0.Text.ToString() + ddlBulan0.SelectedIndex.ToString().PadLeft(2, '0');
            //int groupID = int.Parse(ddlTipeSPP.SelectedItem.ToString());
            int groupID = 0;
            groupID = Convert.ToInt32(ddlTipeSPP.SelectedValue);

            int itemTypeid = 0;
            if (groupID == 4)
                itemTypeid = 2;
            else if (groupID == 5)
                itemTypeid = 3;
            else
                itemTypeid = 1;

            int tahun = 0;
            if (ddlBulan0.SelectedIndex == 1)
                tahun = int.Parse(txtTahun0.Text) - 1;
            else
                tahun = int.Parse(txtTahun0.Text);

            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            if (ddlBulan0.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan0.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }


            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventory saldoInvKosongkan = new SaldoInventory();
            saldoInvKosongkan.YearPeriod = int.Parse(txtTahun0.Text);
            saldoInvKosongkan.MonthPeriod = ddlBulan0.SelectedIndex;
            //new
            saldoInvKosongkan.GroupID = ddlTipeSPP.SelectedIndex;
            //
            SaldoInventoryProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryProcessFacade(saldoInvKosongkan);

            //kosongkan per group
            strError1 = saldoInvKosongkanProcessFacade.Kosongkan();
            if (strError1 != string.Empty)
            {
                DisplayAJAXMessage(this, "Posting Kosongkan Saldo Error ...!");
                return;
            }

            //new add update saldo bulan lalu
            if (strError1 == string.Empty)
            {
                SaldoInventory saldoInventory = new SaldoInventory();
                string strError = string.Empty;

                saldoInventory.YearPeriod = int.Parse(txtTahun0.Text);
                saldoInventory.GroupID = groupID;
                saldoInventory.ItemTypeID = itemTypeid;
                saldoInventory.MonthPeriod = ddlBulan0.SelectedIndex;

                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                strError = saldoInventoryProcessFacade.UpdateSaldoBlnLalu();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Posting Inventori Error utk saldo bulan lalu ...!");
                    return;
                }
            }

            // until new add update saldo bulan lalu
            //cek itemID baru pada inventory
            InventoryFacade invFacade = new InventoryFacade();
            ArrayList arrItemIDBaru = invFacade.RetrieveByItemIDBaru(groupID, itemTypeid, txtTahun0.Text);
            if (invFacade.Error == string.Empty && arrItemIDBaru.Count > 0)
            {
                SaldoInventory saldoInventory = new SaldoInventory();
                foreach (Inventory inv in arrItemIDBaru)
                {
                    string strError = string.Empty;
                    saldoInventory = new SaldoInventory();

                    if (inv.ID > 0)
                    {
                        //insert ItemID baru Saja utk tahun tersebut (inventori, asset, biaya)
                        saldoInventory.ItemID = inv.ID;
                        saldoInventory.YearPeriod = int.Parse(txtTahun0.Text);
                        saldoInventory.GroupID = inv.GroupID;
                        // 1 = inventory
                        saldoInventory.ItemTypeID = itemTypeid;

                        SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                        strError = saldoInventoryProcessFacade.Insert();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Inventori Error utk Item Baru...!");
                            return;
                        }
                    }
                }

            }

            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(0, 0, "0");
            if (groupID == 1 || groupID == 2 || groupID == 3 || groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            {
                arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(groupID, itemTypeid, ThBl);
            }
            if (groupID == 4)
            {
                arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forAsset(groupID, itemTypeid, ThBl);
            }
            if (groupID == 5)
            {
                arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forBiaya(groupID, itemTypeid, ThBl);
            }
            if (groupID == 10)
            {
                arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forRepack(groupID, itemTypeid, ThBl);
            }
            if (receiptDetailFacade.Error == string.Empty && arrReceiptDetail.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                {
                    if (receiptDetail.Quantity != 0 || receiptDetail.Price != 0)
                    {
                        SaldoInventory saldoInventory = new SaldoInventory();
                        string strError = string.Empty;
                        saldoInventory.ItemTypeID = itemTypeid;
                        saldoInventory.YearPeriod = int.Parse(txtTahun0.Text);
                        saldoInventory.GroupID = groupID;
                        saldoInventory.MonthPeriod = int.Parse(ddlBulan0.SelectedValue);
                        saldoInventory.ItemID = receiptDetail.ItemID;
                        saldoInventory.Quantity = receiptDetail.Quantity;
                        saldoInventory.Posting = 0;
                        saldoInventory.SaldoPrice = receiptDetail.Price;
                        SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                        strError = saldoInventoryProcessFacade.Update();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Saldo Inventory Error ...!");
                            return;
                        }
                    }
                }

                SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
                int intNull = saldoInventoryFacade.UpdateSaldoNull(int.Parse(txtTahun0.Text), ddlBulan0.SelectedIndex);
            }

            //DisplayAJAXMessage(this, "Posting selesai ...!");
        }
        private void CekPeriodPosting()
        {
            AccClosingFacade Closing = new AccClosingFacade();
            string objClosing = Closing.CheckClosing(ddlBulan.SelectedIndex, int.Parse(txtTahun.SelectedValue));
            if (objClosing != string.Empty)
            {
                PanelPost.Visible = (objClosing == ddlBulan.SelectedValue + txtTahun.SelectedValue) ? false : true;
            }
            else
            {
                if (DateTime.Now < Convert.ToDateTime(txtToPostingPeriod.Text).AddDays(29))
                    PanelPost.Visible = true;
                else
                    PanelPost.Visible = false;
            }
        }

    }
}