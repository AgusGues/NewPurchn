using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;

namespace GRCweb1.Modul.Factory
{
    public partial class PostingBJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = 0;
                txtTahun.Text = DateTime.Now.Year.ToString();
            }

        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            #region validasi data
            try
            {
                int cekNumber = int.Parse(txtTahun.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Tahun harus angka");
                return;
            }

            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;

            }
            #endregion
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = int.Parse(txtTahun.Text);
            int Bulan = int.Parse(ddlBulan.SelectedIndex.ToString());
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            #region Prepare data
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID == 7 && ddlBulan.SelectedValue.Trim() == "November" && txtTahun.Text.Trim() == "2013")
            {
                DisplayAJAXMessage(this, "Tidak bisa posting saldo awal go live");
                return;
            }
            if (users.UnitKerjaID == 1 && ddlBulan.SelectedValue.Trim() == "Mei" && txtTahun.Text.Trim() == "2013")
            {
                DisplayAJAXMessage(this, "Tidak bisa posting saldo awal go live");
                return;
            }
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(txtTahun.Text) - 1;
            else
                tahun = int.Parse(txtTahun.Text);

            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            #endregion

            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventoryBJ saldoInvKosongkan = new SaldoInventoryBJ();
            saldoInvKosongkan.YearPeriod = int.Parse(txtTahun.Text);
            saldoInvKosongkan.MonthPeriod = ddlBulan.SelectedIndex;
            //new
            SaldoInventoryBJProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryBJProcessFacade(saldoInvKosongkan);

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
                SaldoInventoryBJ SaldoInventoryBJ = new SaldoInventoryBJ();
                string strError = string.Empty;

                SaldoInventoryBJ.YearPeriod = int.Parse(txtTahun.Text);
                SaldoInventoryBJ.MonthPeriod = ddlBulan.SelectedIndex;
                SaldoInventoryBJ.ItemTypeID = 3;

                SaldoInventoryBJProcessFacade SaldoInventoryBJProcessFacade = new SaldoInventoryBJProcessFacade(SaldoInventoryBJ);
                strError = SaldoInventoryBJProcessFacade.UpdateSaldoBlnLalu();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Posting Inventori Error utk saldo bulan lalu ...!");
                    return;
                }
            }

            // until new add update saldo bulan lalu
            //cek itemID baru pada inventory
            FC_ItemsFacade FC_ItemsFacade = new FC_ItemsFacade();
            ArrayList arrItemIDBaru = FC_ItemsFacade.RetrieveByItemIDBaruBJ(txtTahun.Text);
            if (FC_ItemsFacade.Error == string.Empty && arrItemIDBaru.Count > 0)
            {
                SaldoInventoryBJ SaldoInventoryBJ = new SaldoInventoryBJ();
                foreach (FC_Items inv in arrItemIDBaru)
                {
                    string strError = string.Empty;
                    SaldoInventoryBJ = new SaldoInventoryBJ();

                    if (inv.ID > 0)
                    {
                        //insert ItemID baru Saja utk tahun tersebut (inventori, asset, biaya)
                        SaldoInventoryBJ.ItemID = inv.ID;
                        SaldoInventoryBJ.YearPeriod = int.Parse(txtTahun.Text);
                        SaldoInventoryBJ.GroupID = inv.GroupID;
                        SaldoInventoryBJ.ItemTypeID = inv.ItemTypeID;
                        // 1 = inventory

                        SaldoInventoryBJProcessFacade SaldoInventoryBJProcessFacade = new SaldoInventoryBJProcessFacade(SaldoInventoryBJ);
                        strError = SaldoInventoryBJProcessFacade.Insert();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Inventori Error utk Item Baru...!");
                            return;
                        }
                    }
                }

            }
            // until new add update saldo bulan lalu
            FC_ItemsFacade FC_ItemsSaldo = new FC_ItemsFacade();
            ArrayList arrSaldo = FC_ItemsSaldo.RetrieveforSaldoBJ(ThBl);
            if (FC_ItemsSaldo.Error == string.Empty && arrSaldo.Count > 0)
            {
                foreach (FC_Items fcItems in arrSaldo)
                {
                    if (fcItems.Stock != 0 || fcItems.Price != 0)
                    {
                        SaldoInventoryBJ SaldoInventoryBJ = new SaldoInventoryBJ();
                        string strError = string.Empty;
                        SaldoInventoryBJ.YearPeriod = int.Parse(txtTahun.Text);
                        SaldoInventoryBJ.MonthPeriod = ddlBulan.SelectedIndex;
                        SaldoInventoryBJ.ItemID = fcItems.ItemID;
                        SaldoInventoryBJ.Quantity = fcItems.Stock;
                        SaldoInventoryBJ.GroupID = fcItems.GroupID;
                        SaldoInventoryBJ.ItemTypeID = fcItems.ItemTypeID;
                        SaldoInventoryBJ.Posting = 0;
                        SaldoInventoryBJ.SaldoPrice = fcItems.Price;
                        SaldoInventoryBJProcessFacade SaldoInventoryBJProcessFacade = new SaldoInventoryBJProcessFacade(SaldoInventoryBJ);
                        strError = SaldoInventoryBJProcessFacade.Update();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Saldo Inventory Error ...!");
                            return;
                        }
                    }
                }

                SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
                int intNull = SaldoInventoryBJFacade.UpdateSaldoNull(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex);
            }
            DisplayAJAXMessage(this, "Posting selesai ...!");
            //ddlBulan.SelectedIndex = 0;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}