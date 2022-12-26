using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Diagnostics;

namespace GRCweb1.Modul.ListReport
{
    public partial class FormPostingInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = DateTime.Now.Month;
                txtTahun.Text = DateTime.Now.Year.ToString();
                GetTahun();
                LoadTipeSPP();
                //Button1.Visible = false;
                string[] viewEvent = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EventPostingLog", "COGS").Split(',');
                int pos = Array.IndexOf(viewEvent, ((Users)Session["Users"]).DeptID.ToString());
                if (pos >= 0)
                {
                    lst.Visible = true;
                    ListEventLog();
                }
                else
                {
                    lst.Visible = false;
                }
                //check closing periode status if 1 btn posting saldo disabled
                Button4.Enabled = (ClosingStatus() > 0) ? false : true;
            }

        }

        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        protected void btnRePosting_ServerClick(object sender, EventArgs e)
        {
            //Proses Reposting Price Saldo Inventory
            //try
            //{
            btnReset_ServerClick(null, null);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            #region prepared data
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPrice = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPrice = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPrice = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPrice = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPrice = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPrice = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPrice = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPrice = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPrice = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPrice = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPrice = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPrice = "NovAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPrice = "DesAvgPrice";
            }
            int ItmTypeID = 0;
            switch (ddlTipeSPP.SelectedValue)
            {
                case "4":
                    ItmTypeID = 2;
                    break;
                case "5":
                    ItmTypeID = 3;
                    break;
                default:
                    ItmTypeID = 1;
                    break;
            }
            #endregion
            RepostingFacade repostingfacade = new RepostingFacade();
            ArrayList arrReposting = new ArrayList();
            decimal price = 0;
            int totalerror = 0;
            int TotalItem = 0;
            string strError = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            string ItemIDAktif = repostingfacade.ItemWithTrans(int.Parse(ddlTipeSPP.SelectedValue), ThBl, ItmTypeID);
            //Posting Item Yang tidak ada pergerakan (avg bulan dipilih = avg bulan lalu)
            strError = repostingfacade.PostingPriceSameWithPrevious((txtTahun.Text), int.Parse(ddlTipeSPP.SelectedValue), strQtyLastMonth, strAvgPrice, ItemIDAktif, ItmTypeID.ToString());

            //Posting Average price item yang bergerak di periode terpilih
            ReportFacadeAcc postingPrice = new ReportFacadeAcc();
            string Result = postingPrice.PostingPriceSaldoAwal(ThBl, ddlTipeSPP.SelectedValue);
            sw.Stop();
            /** create log event **/
            EventLogFacade ev = new EventLogFacade();
            Domain.EventLog l = new Domain.EventLog();
            l.ModulName = ddlTipeSPP.SelectedItem.Text;
            l.Bulan = (ddlBulan.SelectedIndex);
            l.Tahun = int.Parse(ddlTahun.SelectedValue);
            l.EventName = "Proses Posting Price as Saldo Awal [ " + sw.Elapsed + " ]";
            l.CreatedBy = ((Users)Session["Users"]).UserName;
            l.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            l.DocumentNo = "Reposting " + Result;// TotalItem.ToString("###,##0") + " items succeed with " + totalerror.ToString("###,##0") + " updated";
            int res = ev.InsertEvent(l);
            ListEventLog();
            DisplayAJAXMessage(this, "Reposting " + Result + " Total time execute :" + sw.Elapsed.ToString());//TotalItem.ToString("###,##0") + " items succeed with " + totalerror.ToString("###,##0") + " updated");

        }

        protected void btnReset_ServerClick(object sender, EventArgs e)
        {
            //Reset Price on SaldoInventory Table
            RepostingFacade repostingfacade = new RepostingFacade();
            ArrayList arrReposting = new ArrayList();
            string strError = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            strError = repostingfacade.ResetPrice(int.Parse(ddlTipeSPP.SelectedValue), ThBl);
            if (strError == string.Empty)
            {

                // DisplayAJAXMessage(this, "Reset price succeed");
            }
        }

        protected void btnPrint_ServerClick_old(object sender, EventArgs e)
        {
            //cek dulu 
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

            txtPersiapan.Text = "Sedang Proses ...";
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            //int groupID = int.Parse(ddlTipeSPP.SelectedItem.ToString());
            int groupID = 0;
            groupID = ddlTipeSPP.SelectedIndex;

            int itemTypeid = 0;
            if (groupID == 4)
                itemTypeid = 2;
            else if (groupID == 5)
                itemTypeid = 3;
            else
                itemTypeid = 1;

            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(txtTahun.Text) - 1;
            else
                tahun = int.Parse(txtTahun.Text);

            string strQtyLastMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventory saldoInvKosongkan = new SaldoInventory();
            saldoInvKosongkan.YearPeriod = int.Parse(txtTahun.Text);
            saldoInvKosongkan.MonthPeriod = ddlBulan.SelectedIndex;
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

                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                saldoInventory.GroupID = groupID;
                saldoInventory.ItemTypeID = itemTypeid;
                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

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
            ArrayList arrItemIDBaru = invFacade.RetrieveByItemIDBaru(groupID, itemTypeid, txtTahun.Text);
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
                        saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
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
            #region depreciated code
            // until new add update saldo bulan lalu


            //add ItemID dulu Inventori
            //add groupID
            //ArrayList arrInv = invFacade.Retrieve();
            //ArrayList arrInv = invFacade.RetrieveByGroupID(groupID, 1);
            //if (invFacade.Error == string.Empty)
            //{
            //    foreach (Inventory inv in arrInv)
            //    {
            //        string strError = string.Empty;

            //        //jika januari ambil saldo des thn lalu

            //        SaldoInventory saldoInventory = new SaldoInventory();
            //        SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            //        SaldoInventory saldoInv = saldoInventoryFacade.RetrieveByItemID(inv.ID, tahun, 1);
            //        //SaldoInventory saldoInv = saldoInventoryFacade.RetrieveByItemID(inv.ID, int.Parse(txtTahun.Text), 1);
            //        if (saldoInventoryFacade.Error == string.Empty)
            //        {
            //            if (saldoInv.ItemID == inv.ID && saldoInv.ItemID > 0)
            //            {
            //                // cek dulu apakah ke last year juga & last month
            //                saldoInventory = new SaldoInventory();

            //                //for update qty aja utk ItemId yg sdh ada
            //                saldoInventory.ItemID = inv.ID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = inv.GroupID;
            //                // 1 = inventory
            //                saldoInventory.ItemTypeID = 1;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                //saldoInventory.Quantity = 0;
            //                saldoInventory.Posting = 0;

            //                if (ddlBulan.SelectedIndex == 1)
            //                    saldoInventory.Quantity = saldoInv.DesQty;
            //                else if (ddlBulan.SelectedIndex == 2)
            //                    saldoInventory.Quantity = saldoInv.JanQty;
            //                else if (ddlBulan.SelectedIndex == 3)
            //                    saldoInventory.Quantity = saldoInv.FebQty;
            //                else if (ddlBulan.SelectedIndex == 4)
            //                    saldoInventory.Quantity = saldoInv.MarQty;
            //                else if (ddlBulan.SelectedIndex == 5)
            //                    saldoInventory.Quantity = saldoInv.AprQty;
            //                else if (ddlBulan.SelectedIndex == 6)
            //                    saldoInventory.Quantity = saldoInv.MeiQty;
            //                else if (ddlBulan.SelectedIndex == 7)
            //                    saldoInventory.Quantity = saldoInv.JunQty;
            //                else if (ddlBulan.SelectedIndex == 8)
            //                    saldoInventory.Quantity = saldoInv.JulQty;
            //                else if (ddlBulan.SelectedIndex == 9)
            //                    saldoInventory.Quantity = saldoInv.AguQty;
            //                else if (ddlBulan.SelectedIndex == 10)
            //                    saldoInventory.Quantity = saldoInv.SepQty;
            //                else if (ddlBulan.SelectedIndex == 11)
            //                    saldoInventory.Quantity = saldoInv.OktQty;
            //                else if (ddlBulan.SelectedIndex == 12)
            //                    saldoInventory.Quantity = saldoInv.NovQty;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Update();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Inventori Error ...!");
            //                    return;
            //                }

            //            }
            //            else
            //            {
            //                if (saldoInv.ItemID > 0)
            //                {
            //                    saldoInventory = new SaldoInventory();

            //                    //insert ItemID baru Saja utk tahun tersebut (inventori, asset, biaya)
            //                    saldoInventory.ItemID = inv.ID;
            //                    saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                    saldoInventory.GroupID = inv.GroupID;
            //                    // 1 = inventory
            //                    saldoInventory.ItemTypeID = 1;

            //                    SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                    strError = saldoInventoryProcessFacade.Insert();
            //                    if (strError != string.Empty)
            //                    {
            //                        DisplayAJAXMessage(this, "Posting Inventori Error ...!");
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            DisplayAJAXMessage(this, "Posting Inventori Error ...!");
            //        }

            //    }
            //}



            //add ItemID dulu Asset
            //add groupID
            //ArrayList arrInvForAsset = invFacade.RetrieveForAsset();

            //ArrayList arrInvForAsset = invFacade.RetrieveByGroupIDForAsset(groupID, itemTypeid);
            //if (invFacade.Error == string.Empty)
            //{
            //    foreach (Inventory invAset in arrInvForAsset)
            //    {
            //        string strError = string.Empty;
            //        SaldoInventory saldoInventory = new SaldoInventory();

            //        SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            //        SaldoInventory saldoInv = saldoInventoryFacade.RetrieveByItemID(invAset.ID, int.Parse(txtTahun.Text), 2);
            //        if (saldoInventoryFacade.Error == string.Empty)
            //        {
            //            if (saldoInv.ItemID == invAset.ID)
            //            {
            //                //for update qty aja utk ItemId yg sdh ada
            //                saldoInventory.ItemID = invAset.ID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = invAset.GroupID;
            //                // 2 = asset
            //                saldoInventory.ItemTypeID = itemTypeid;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                //saldoInventory.MonthPeriod = 0;
            //                //saldoInventory.Quantity = 0;
            //                saldoInventory.Posting = 0;
            //                if (ddlBulan.SelectedIndex == 1)
            //                    saldoInventory.Quantity = saldoInv.DesQty;
            //                else if (ddlBulan.SelectedIndex == 2)
            //                    saldoInventory.Quantity = saldoInv.JanQty;
            //                else if (ddlBulan.SelectedIndex == 3)
            //                    saldoInventory.Quantity = saldoInv.FebQty;
            //                else if (ddlBulan.SelectedIndex == 4)
            //                    saldoInventory.Quantity = saldoInv.MarQty;
            //                else if (ddlBulan.SelectedIndex == 5)
            //                    saldoInventory.Quantity = saldoInv.AprQty;
            //                else if (ddlBulan.SelectedIndex == 6)
            //                    saldoInventory.Quantity = saldoInv.MeiQty;
            //                else if (ddlBulan.SelectedIndex == 7)
            //                    saldoInventory.Quantity = saldoInv.JunQty;
            //                else if (ddlBulan.SelectedIndex == 8)
            //                    saldoInventory.Quantity = saldoInv.JulQty;
            //                else if (ddlBulan.SelectedIndex == 9)
            //                    saldoInventory.Quantity = saldoInv.AguQty;
            //                else if (ddlBulan.SelectedIndex == 10)
            //                    saldoInventory.Quantity = saldoInv.SepQty;
            //                else if (ddlBulan.SelectedIndex == 11)
            //                    saldoInventory.Quantity = saldoInv.OktQty;
            //                else if (ddlBulan.SelectedIndex == 12)
            //                    saldoInventory.Quantity = saldoInv.NovQty;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Update();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Asset Error ...!");
            //                    return;
            //                }

            //            }
            //            else
            //            {
            //                saldoInventory.ItemID = invAset.ID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = invAset.GroupID;
            //                // 2 = asset
            //                saldoInventory.ItemTypeID = itemTypeid;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Insert();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Asset Error ...!");
            //                    return;
            //                }

            //            }
            //        }

            //    }

            //}
            //add ItemID dulu Biaya
            //add groupID
            //ArrayList arrInvForBiaya = invFacade.RetrieveForBiaya();
            //ArrayList arrInvForBiaya = invFacade.RetrieveByGroupIDForBiaya(groupID, itemTypeid );
            //if (invFacade.Error == string.Empty)
            //{
            //    foreach (Inventory invBiaya in arrInvForBiaya)
            //    {
            //        string strError = string.Empty;

            //        SaldoInventory saldoInventory = new SaldoInventory();

            //        SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            //        SaldoInventory saldoInv = saldoInventoryFacade.RetrieveByItemID(invBiaya.ID, int.Parse(txtTahun.Text), itemTypeid);
            //        if (saldoInventoryFacade.Error == string.Empty)
            //        {
            //            if (saldoInv.ItemID == invBiaya.ID)
            //            {
            //                //for update qty aja utk ItemId yg sdh ada
            //                saldoInventory.ItemID = invBiaya.ID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = invBiaya.GroupID;
            //                // 3 = biaya
            //                saldoInventory.ItemTypeID = itemTypeid;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                saldoInventory.MonthPeriod = 0;
            //                //saldoInventory.Quantity = 0;
            //                saldoInventory.Posting = 0;
            //                if (ddlBulan.SelectedIndex == 1)
            //                    saldoInventory.Quantity = saldoInv.DesQty;
            //                else if (ddlBulan.SelectedIndex == 2)
            //                    saldoInventory.Quantity = saldoInv.JanQty;
            //                else if (ddlBulan.SelectedIndex == 3)
            //                    saldoInventory.Quantity = saldoInv.FebQty;
            //                else if (ddlBulan.SelectedIndex == 4)
            //                    saldoInventory.Quantity = saldoInv.MarQty;
            //                else if (ddlBulan.SelectedIndex == 5)
            //                    saldoInventory.Quantity = saldoInv.AprQty;
            //                else if (ddlBulan.SelectedIndex == 6)
            //                    saldoInventory.Quantity = saldoInv.MeiQty;
            //                else if (ddlBulan.SelectedIndex == 7)
            //                    saldoInventory.Quantity = saldoInv.JunQty;
            //                else if (ddlBulan.SelectedIndex == 8)
            //                    saldoInventory.Quantity = saldoInv.JulQty;
            //                else if (ddlBulan.SelectedIndex == 9)
            //                    saldoInventory.Quantity = saldoInv.AguQty;
            //                else if (ddlBulan.SelectedIndex == 10)
            //                    saldoInventory.Quantity = saldoInv.SepQty;
            //                else if (ddlBulan.SelectedIndex == 11)
            //                    saldoInventory.Quantity = saldoInv.OktQty;
            //                else if (ddlBulan.SelectedIndex == 12)
            //                    saldoInventory.Quantity = saldoInv.NovQty;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Update();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Biaya Error ...!");
            //                    return;
            //                }

            //            }
            //            else
            //            {
            //                saldoInventory.ItemID = invBiaya.ID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = invBiaya.GroupID;
            //                // 3 = biaya
            //                saldoInventory.ItemTypeID = itemTypeid;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Insert();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Biaya Error ...!");
            //                    return;
            //                }

            //            }
            //        }

            //    }

            //}
            #endregion
            txtPersiapan.Text = "Selesai !";
            txtReceipt.Text = "Sedang Proses ...";

            //new repack
            if (groupID == 10 || groupID == 13)
            {
                ConvertanFacade convertanFacade = new ConvertanFacade();
                ArrayList arrConvertan = convertanFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl); ;

                //ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                //ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl);
                if (convertanFacade.Error == string.Empty && arrConvertan.Count > 0)
                {
                    foreach (Convertan convertan in arrConvertan)
                    {
                        if (convertan.ToItemID > 0)
                        {
                            SaldoInventory saldoInventory = new SaldoInventory();
                            string strError = string.Empty;

                            // 1 = inventory
                            saldoInventory.ItemTypeID = itemTypeid;
                            saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                            saldoInventory.GroupID = groupID;
                            saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

                            saldoInventory.ItemID = convertan.ToItemID;
                            saldoInventory.Quantity = convertan.ToQty;
                            saldoInventory.Posting = 0;

                            SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                            strError = saldoInventoryProcessFacade.Update();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, "Posting convertan Error ...!");
                                return;
                            }
                        }
                    }
                }

            }
            //until new repack

            else
            {
                //add data receipt
                //ReceiptFacade receiptFacade = new ReceiptFacade();
                //new logik
                ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl);
                if (receiptDetailFacade.Error == string.Empty && arrReceiptDetail.Count > 0)
                {
                    foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
                    {
                        if (rcpDetail.ItemID > 0)
                        {
                            SaldoInventory saldoInventory = new SaldoInventory();
                            string strError = string.Empty;

                            // 1 = inventory
                            saldoInventory.ItemTypeID = itemTypeid;
                            saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                            saldoInventory.GroupID = rcpDetail.GroupID;
                            saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

                            saldoInventory.ItemID = rcpDetail.ItemID;
                            saldoInventory.Quantity = rcpDetail.Quantity;
                            saldoInventory.Posting = 0;

                            SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                            strError = saldoInventoryProcessFacade.Update();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, "Posting Receipt Error ...!");
                                return;
                            }
                        }
                    }
                }
                //until new logik
            }
            #region depreciated line
            //ArrayList arrReceipt = receiptFacade.RetrieveOpenStatusForAll(ThBl, itemTypeid, groupID);
            //if (receiptFacade.Error == string.Empty && arrReceipt.Count > 0)
            //{
            //    foreach (Receipt rcp in arrReceipt)
            //    {
            //        string strError = string.Empty;

            //        ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            //        //add groupID
            //        //ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(rcp.ID);
            //        ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdwithGroupID(rcp.ID, groupID);
            //        if (receiptDetailFacade.Error == string.Empty && arrReceiptDetail.Count > 0)
            //        {
            //            foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            //            {
            //                SaldoInventory saldoInventory = new SaldoInventory();

            //                // 1 = inventory
            //                saldoInventory.ItemTypeID = 1;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = rcpDetail.GroupID;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

            //                saldoInventory.ItemID = rcpDetail.ItemID;
            //                saldoInventory.Quantity = rcpDetail.Quantity;
            //                saldoInventory.Posting = 0;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Update();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Receipt Error ...!");
            //                    return;
            //                }
            //            }
            //        }

            //    }

            //}
            //until add data receipt
            txtReceipt.Text = "Selesai !";
            txtPakai.Text = "Sedang Proses ...";
            #endregion
            //new logik
            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();

            ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl);
            if (pakaiDetailFacade.Error == string.Empty && arrPakaiDetail.Count > 0)
            {
                foreach (PakaiDetail pkiDetail in arrPakaiDetail)
                {
                    if (pkiDetail.ItemID > 0)
                    {
                        if (pkiDetail.ItemID > 0)
                        {
                            string strError = string.Empty;
                            SaldoInventory saldoInventory = new SaldoInventory();

                            saldoInventory.ItemTypeID = itemTypeid;
                            saldoInventory.ItemID = pkiDetail.ItemID;
                            saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                            saldoInventory.GroupID = pkiDetail.GroupID;
                            saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
                            saldoInventory.Quantity = pkiDetail.Quantity;
                            saldoInventory.Posting = 0;

                            SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                            strError = saldoInventoryProcessFacade.MinusSaldo();
                            if (strError != string.Empty)
                            {
                                DisplayAJAXMessage(this, "Posting Pakai Error ...!");
                                return;
                            }
                        }
                    }
                }
            }
            #region depreciated line
            //until new logik

            //add data pakai
            //PakaiFacade pakaiFacade = new PakaiFacade();
            ////here
            ////ArrayList arrPakai = pakaiFacade.RetrieveOpenStatusForAll(ThBl);
            //ArrayList arrPakai = pakaiFacade.RetrieveOpenStatusForAll(ThBl, itemTypeid, groupID);
            //if (pakaiFacade.Error == string.Empty && arrPakai.Count > 0)
            //{
            //    foreach (Pakai pki in arrPakai)
            //    {
            //        string strError = string.Empty;

            //        PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
            //        //add groupID
            //        //ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
            //        ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdwithGroupID(pki.ID, groupID);
            //        if (pakaiDetailFacade.Error == string.Empty && arrPakaiDetail.Count > 0)
            //        {
            //            foreach (PakaiDetail pkiDetail in arrPakaiDetail)
            //            {
            //                SaldoInventory saldoInventory = new SaldoInventory();
            //                saldoInventory.ItemTypeID = 1;
            //                saldoInventory.ItemID = pkiDetail.ItemID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = pkiDetail.GroupID;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                saldoInventory.Quantity = pkiDetail.Quantity;
            //                saldoInventory.Posting = 0;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.MinusSaldo();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Pakai Error ...!");
            //                    return;
            //                }

            //            }
            //        }
            //    }
            //}
            //until add data pakai
            txtPakai.Text = "Selesai !";
            #endregion
            //new logik
            AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();

            ArrayList arrAdjustDetail = adjustDetailFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl);
            if (adjustDetailFacade.Error == string.Empty && arrAdjustDetail.Count > 0)
            {
                foreach (AdjustDetail adjDetail in arrAdjustDetail)
                {
                    if (adjDetail.ItemID > 0)
                    {
                        SaldoInventory saldoInventory = new SaldoInventory();
                        string strError = string.Empty;

                        saldoInventory.ItemTypeID = itemTypeid;
                        saldoInventory.ItemID = adjDetail.ItemID;
                        saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                        saldoInventory.GroupID = adjDetail.GroupID;
                        saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
                        saldoInventory.Quantity = adjDetail.Quantity;
                        saldoInventory.Posting = 0;

                        SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                        if (adjDetail.AdjustType == "Tambah")
                        {
                            if (adjDetail.Quantity > 0)
                            {
                                strError = saldoInventoryProcessFacade.Update();
                                if (strError != string.Empty)
                                {
                                    DisplayAJAXMessage(this, "Posting Adjust Tambah Error ...!");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (adjDetail.Quantity > 0)
                            {
                                strError = saldoInventoryProcessFacade.MinusSaldo();
                                if (strError != string.Empty)
                                {
                                    DisplayAJAXMessage(this, "Posting Adjust Kurang Error ...!");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            //until new logik



            txtAdjust.Text = "Sedang Proses ...";
            ////add data adjustment
            //AdjustFacade adjustFacade = new AdjustFacade();
            ////ArrayList arrAdjust = adjustFacade.RetrieveOpenStatusForAll(ThBl);
            ////here
            //ArrayList arrAdjust = adjustFacade.RetrieveOpenStatusForAll(ThBl, itemTypeid, groupID);
            //if (adjustFacade.Error == string.Empty && arrAdjust.Count > 0)
            //{
            //    foreach (Adjust adj in arrAdjust)
            //    {
            //        string strError = string.Empty;

            //        AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
            //        //add groupID
            //        ArrayList arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdwithGroupID(adj.ID, groupID);
            //        //ArrayList arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustId(adj.ID);
            //        if (adjustDetailFacade.Error == string.Empty && arrAdjustDetail.Count > 0)
            //        {
            //            foreach (AdjustDetail adjDetail in arrAdjustDetail)
            //            {
            //                SaldoInventory saldoInventory = new SaldoInventory();
            //                saldoInventory.ItemTypeID = 1;
            //                saldoInventory.ItemID = adjDetail.ItemID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = adjDetail.GroupID;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                saldoInventory.Quantity = adjDetail.Quantity;
            //                saldoInventory.Posting = 0;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                if (adj.AdjustType == "Tambah")
            //                {
            //                    if (adjDetail.Quantity > 0)
            //                    {
            //                        strError = saldoInventoryProcessFacade.Update();
            //                        if (strError != string.Empty)
            //                        {
            //                            DisplayAJAXMessage(this, "Posting Adjust Tambah Error ...!");
            //                            return;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if (adjDetail.Quantity > 0)
            //                    {
            //                        strError = saldoInventoryProcessFacade.MinusSaldo();
            //                        if (strError != string.Empty)
            //                        {
            //                            DisplayAJAXMessage(this, "Posting Adjust Kurang Error ...!");
            //                            return;
            //                        }
            //                    }
            //                }

            //            }
            //        }

            //    }

            //}
            //until add data adjustment
            txtAdjust.Text = "Selesai !";
            txtRetur.Text = "Sedang Proses ...";

            //new logik
            ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
            ArrayList arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByGroupIDwithSUM(groupID, itemTypeid, ThBl);
            if (returPakaiDetailFacade.Error == string.Empty && arrReturPakaiDetail.Count > 0)
            {
                foreach (ReturPakaiDetail rtrDetail in arrReturPakaiDetail)
                {
                    if (rtrDetail.ItemID > 0)
                    {
                        string strError = string.Empty;
                        SaldoInventory saldoInventory = new SaldoInventory();

                        saldoInventory.ItemTypeID = itemTypeid;
                        saldoInventory.ItemID = rtrDetail.ItemID;
                        saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                        saldoInventory.GroupID = rtrDetail.GroupID;
                        saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
                        saldoInventory.Quantity = rtrDetail.Quantity;
                        saldoInventory.Posting = 0;

                        SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                        strError = saldoInventoryProcessFacade.Update();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Retur Error ...!");
                            return;
                        }
                    }
                }
            }

            //until new logik

            //add data retur
            //ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();
            ////here
            //ArrayList arrReturFacade = returPakaiFacade.RetrieveOpenStatusForAll(ThBl, itemTypeid);
            //if (returPakaiFacade.Error == string.Empty && arrReturFacade.Count > 0)
            //{
            //    foreach (ReturPakai rtr in arrReturFacade)
            //    {
            //        string strError = string.Empty;

            //        ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
            //        //add groupID
            //        //ArrayList arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturId(rtr.ID);
            //        ArrayList arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdwithGroupID(rtr.ID, groupID);
            //        if (returPakaiDetailFacade.Error == string.Empty && arrReturPakaiDetail.Count > 0)
            //        {
            //            foreach (ReturPakaiDetail rtrDetail in arrReturPakaiDetail)
            //            {
            //                SaldoInventory saldoInventory = new SaldoInventory();
            //                saldoInventory.ItemTypeID = 1;
            //                saldoInventory.ItemID = rtrDetail.ItemID;
            //                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
            //                saldoInventory.GroupID = rtrDetail.GroupID;
            //                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
            //                saldoInventory.Quantity = rtrDetail.Quantity;
            //                saldoInventory.Posting = 0;

            //                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
            //                strError = saldoInventoryProcessFacade.Update();
            //                if (strError != string.Empty)
            //                {
            //                    DisplayAJAXMessage(this, "Posting Retur Error ...!");
            //                    return;
            //                }

            //            }
            //        }

            //    }

            //}
            txtRetur.Text = "Selesai !";

            //until add data retur


            //Update Average Harga Pembelian
            SaldoInventoryFacade priceSaldoInventoryFacade = new SaldoInventoryFacade();
            ArrayList arrUpdateAvgPrice = priceSaldoInventoryFacade.RetrieveAllTableForPriceSaldoInventory(strQtyLastMonth, strAvgPriceLastMonth, tahun, ThBl, itemTypeid, groupID);
            if (priceSaldoInventoryFacade.Error == string.Empty)
            {
                foreach (SaldoInventory saldoInv in arrUpdateAvgPrice)
                {
                    string strError = string.Empty;
                    SaldoInventory saldoInventory = new SaldoInventory();

                    saldoInventory.ItemTypeID = itemTypeid;
                    saldoInventory.GroupID = groupID;
                    saldoInventory.ItemID = saldoInv.ItemID;
                    saldoInventory.AvgPrice = saldoInv.AvgPrice;
                    saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                    saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

                    SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                    strError = saldoInventoryProcessFacade.UpdateSaldoAvgPriceBlnIni();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Posting Average Harga Error ...!");
                        return;
                    }


                }
            }
            //until : Update Average Harga Pembelian

            DisplayAJAXMessage(this, "Posting selesai ...!");
            ddlBulan.SelectedIndex = 0;
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //Proses Posting Saldo Inventory
            //cek dulu 
            #region validasi input
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
            txtPersiapan.Text = "Sedang Proses ...";
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            //int groupID = int.Parse(ddlTipeSPP.SelectedItem.ToString());
            int groupID = 0;
            groupID = Convert.ToInt32(ddlTipeSPP.SelectedValue);

            int itemTypeid = 0;
            if (groupID == 4 || groupID == 12)
                itemTypeid = 2;
            else if (groupID == 5)
                itemTypeid = 3;
            else
                itemTypeid = 1;

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


            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventory saldoInvKosongkan = new SaldoInventory();
            saldoInvKosongkan.YearPeriod = int.Parse(txtTahun.Text);
            saldoInvKosongkan.MonthPeriod = ddlBulan.SelectedIndex;
            //new
            //saldoInvKosongkan.GroupID = ddlTipeSPP.SelectedIndex;
            saldoInvKosongkan.GroupID = groupID;
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

                saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                saldoInventory.GroupID = groupID;
                saldoInventory.ItemTypeID = itemTypeid;
                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

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
            ArrayList arrItemIDBaru = invFacade.RetrieveByItemIDBaru(groupID, itemTypeid, txtTahun.Text);
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
                        saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
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
            // until new add update saldo bulan lalu

            txtPersiapan.Text = "Selesai !";
            txtReceipt.Text = "Sedang Proses ...";


            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ArrayList arrReceiptDetail = new ArrayList();// receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(0, 0, "0");

            switch (groupID)
            {
                case 4:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forAsset(groupID, itemTypeid, ThBl);
                    break;
                case 12:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forAsset(groupID, itemTypeid, ThBl);
                    break;
                case 5:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forBiaya(groupID, itemTypeid, ThBl);
                    break;
                case 10:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forRepack(groupID, itemTypeid, ThBl);
                    break;
                case 13:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forNonGrc(groupID, itemTypeid, ThBl);
                    break;
                case 14:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forRepack(groupID, itemTypeid, ThBl);
                    break;
                default:
                    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(groupID, itemTypeid, ThBl);
                    break;
            }
            if (receiptDetailFacade.Error == string.Empty && arrReceiptDetail.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                {
                    ////if (receiptDetail.Quantity >= 0)
                    //    if (receiptDetail.ItemID == 35359)
                    //{

                    SaldoInventory saldoInventory = new SaldoInventory();
                    string strError = string.Empty;
                    saldoInventory.ItemTypeID = itemTypeid;
                    saldoInventory.YearPeriod = int.Parse(txtTahun.Text);
                    saldoInventory.GroupID = groupID;
                    saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
                    saldoInventory.ItemID = receiptDetail.ItemID;

                    saldoInventory.Quantity = receiptDetail.Quantity;
                    saldoInventory.Posting = 0;
                    saldoInventory.SaldoPrice = receiptDetail.Price;
                    if (saldoInventory.ItemID == 42712)
                    { string test = "test"; }
                    SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                    strError = saldoInventoryProcessFacade.Update();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Posting Saldo Inventory Error ...!");
                        return;
                    }
                    //}
                    //else
                    //{
                    //    InventoryFacade infad = new InventoryFacade();
                    //    Inventory inv = infad.RetrieveByIdNew(receiptDetail.ItemID, groupID);
                    //    DisplayAJAXMessage(this, "Error :\\nItemName :" + inv.ItemName + "\\nQuantity :" + receiptDetail.Quantity);
                    //    return;
                    //}


                }

                SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
                int intNull = saldoInventoryFacade.UpdateSaldoNull(int.Parse(txtTahun.Text), ddlBulan.SelectedIndex);
            }
            //until new repack

            //btnRePosting_ServerClick(null, null);
            /** create log even **/
            EventLogFacade ev = new EventLogFacade();
            Domain.EventLog l = new Domain.EventLog();
            l.ModulName = ddlTipeSPP.SelectedItem.Text;
            l.Bulan = ddlBulan.SelectedIndex;
            l.Tahun = int.Parse(ddlTahun.SelectedValue);
            l.EventName = "Proses Saldo Inventory";
            l.CreatedBy = ((Users)Session["Users"]).UserName;
            l.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            l.DocumentNo = (strError1 == string.Empty) ? "Success" : strError1;
            int res = ev.InsertEvent(l);
            DisplayAJAXMessage(this, "Posting selesai ...!");
            //ddlBulan.SelectedIndex = 0;
            ListEventLog();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListEventLog();
            Button4.Enabled = (ClosingStatus() > 0) ? false : true;
        }
        protected void ddlTipeSPP_OnTextChanged(object sender, EventArgs e)
        {
            /** 
             * tampilkan Posting average price jika tipe spp nya bahan baku atau bahan pembantu saja
             */
            //int spp = Convert.ToInt16(ddlTipeSPP.SelectedValue);
            //if (spp < 3)
            //{
            //    Button1.Visible = true;
            //}
            //else
            //{
            //    Button1.Visible = false;
            //}
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            /**
             * Button Posting Average Price
             */
            /**
             * cek bulan,tahun dan tipe spp
             * harus terisi semua
             */
            string bln = string.Empty;
            string thn = string.Empty;
            string spp = string.Empty;
            string strQuery = string.Empty;
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            if (ddlBulan.SelectedIndex <= 0 ||
               ddlTipeSPP.SelectedIndex <= 0 ||
               txtTahun.Text == "")
            {
                DisplayAJAXMessage(this, "Lengkapi pengisian form ...!");
            }
            bln = ddlBulan.SelectedIndex.ToString();//.SelectedItem.ToString();
            thn = txtTahun.Text;
            string bulane = ddlBulan.SelectedItem.ToString();
            string FirstPeriod = string.Empty;
            string LastPeriod = string.Empty;
            string blne = bln.ToString();
            string s = new string('0', (2 - bln.Length));
            Int32 lastDay = DateTime.DaysInMonth(Convert.ToInt32(thn), Convert.ToInt32(blne));
            string d = new string('0', (2 - lastDay.ToString().Length));
            string jnStok = ddlTipeSPP.SelectedValue;
            FirstPeriod = thn + s + bln + "01";
            LastPeriod = thn + s + bln + d + lastDay;
            ReportFacadeAcc reportFacade = new ReportFacadeAcc();
            strQuery = reportFacade.PostingAvgPrice(FirstPeriod, LastPeriod, jnStok, thn);
            string Nulle = this.process_avg(strQuery);
            sw.Stop();
            /**
             * catat proses posting
             */
            EventLogFacade ev = new EventLogFacade();
            Domain.EventLog l = new Domain.EventLog();
            l.ModulName = ddlTipeSPP.SelectedItem.Text;
            l.Bulan = ddlBulan.SelectedIndex;
            l.Tahun = int.Parse(ddlTahun.SelectedValue);
            l.EventName = "Posting Average Price [ " + sw.Elapsed.ToString() + " ]";
            l.CreatedBy = ((Users)Session["Users"]).UserName;
            l.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            l.DocumentNo = Nulle;
            int res = ev.InsertEvent(l);
            ListEventLog();
            DisplayAJAXMessage(this, Nulle + " Total Execute Time :" + sw.Elapsed.ToString());

        }
        public string process_avg(string Query)
        {
            string strError = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(Query);
            strError = dataAccess.Error;

            if (strError == string.Empty)
            {
                strError = "Posting Success with " + sqlDataReader.RecordsAffected.ToString("###,###") + " Records Updated";

            }
            else
            {
                strError += "Error ";
            }

            return strError;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            /**
             * Buton Reset Price
             */
            btnReset_ServerClick(sender, e);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            /**
             * Button Reposting Price
             */

            btnRePosting_ServerClick(sender, e);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            /**
             * Button Posting Saldo
             */

            btnPrint_ServerClick(sender, e);
        }
        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void GetTahun()
        {
            string strSQL = "Select Tahun from vw_StockPurchn group by tahun order by tahun";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            ddlTahun.Items.Clear();
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlTahun.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void ddlTahun_Click(object sender, EventArgs e)
        {
            txtTahun.Text = ddlTahun.SelectedValue;
            Button2.Visible = (int.Parse(ddlTahun.SelectedValue) < 2016) ? false : true;
            Button4.Enabled = (ClosingStatus() > 0) ? false : true;
        }
        private void ListEventLog()
        {
            EventLogFacade ev = new EventLogFacade();
            ArrayList arrData = new ArrayList();
            int Bulan = ddlBulan.SelectedIndex;
            int Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
            arrData = ev.RetrievePostingLog(Bulan, Tahun);
            lstEvent.DataSource = arrData;
            lstEvent.DataBind();
        }
        private int ClosingStatus()
        {
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus(ddlBulan.SelectedIndex, int.Parse(ddlTahun.SelectedValue.ToString()));
            return clsBln.Status;
        }

    }
}