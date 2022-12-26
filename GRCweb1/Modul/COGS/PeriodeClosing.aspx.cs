using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using BusinessFacade;
using Cogs;
using Factory;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using DataAccessLayer;

namespace GRCweb1.Modul.COGS
{
    public partial class PeriodeClosing : System.Web.UI.Page
    {
        private bool RepostingSA = bool.Parse(new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Reposting", "COGS").ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                LoadTahun(DateTime.Now.Year);
                string sts = (Purchn.Checked == true) ? "Purchn" : "Produksi";
                ddlBulan.SelectedValue = GetMonthLastClosing(sts).ToString();
                ClosingFacade c = new ClosingFacade();
                string stss = (Purchn.Checked == true) ? "Purchn" : "Produksi";
                int stat = c.ClosingStatus(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), stss);
                int dept = c.GetClosingStatus("SystemClosingByDept");
                int clsAktif = c.GetClosingStatus("SystemClosing");
                int user = ((Users)Session["Users"]).DeptID;
                btnCancel.Visible = (stat == 1 && user == dept && clsAktif == 1) ? true : false;
                btnUpdate.Visible = (stat == 0 && user == dept && clsAktif == 1) ? false : true;
                txtUser.Text = (((Users)Session["Users"]).UserName).ToString();
                btnPrint.Visible = (((Users)Session["Users"]).UserName == "admin") ? true : false;
                LoadData();

            }
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            // open periode
            ClosingFacade Cl = new ClosingFacade();
            Closper objCl = new Closper();
            string sts = (Purchn.Checked == true) ? "Purchn" : "Produksi";
            int ID = Cl.GetIDClosing(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), sts);
            objCl.ID = ID;
            objCl.RowStatus = 0;
            objCl.LastModifiedBy = ((Users)Session["Users"]).UserName.ToString();

            int result = (ID > 0) ? Cl.Update(objCl) : 0;
            if (result > 0 && Cl.Error == string.Empty)
            {
                LoadData();
            }
            else
            {
                DisplayAJAXMessage(this, Cl.Error);
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            //closing periode
            string sts = (Purchn.Checked == true) ? "Purchn" : "Produksi";
            #region Info proses closing
            if (sts == "Purchn")
            {
                infoclose.Visible = true; readsts.Visible = true;
                infoclose.InnerHtml = "Please wait.. posting in progress";
                readsts.InnerHtml = "";
                //ArrayList arrGroupsPurchn = new ArrayList();
                //GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
                //arrGroupsPurchn = groupsPurchnFacade.Retrieve();
                //foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
                //{
                //    if (groupsPurchn.ID > 0)
                //    {
                //        readsts.InnerHtml = "Sedang Proses Posting " + groupsPurchn.GroupDescription + " ....";
                //        PostingInventory(groupsPurchn.ID);
                //        DisplayAJAXMessage(this, "Posting " + groupsPurchn.GroupDescription + " complete");
                //    }
                //}
                //readsts.Visible = false;
                //readsts.InnerHtml = string.Empty;
                //infoclose.InnerHtml = "Posting complete...";
                //goto ps;
                Response.Redirect("periodeclosing.aspx?ID=1&t=purch");
            }
            else
            {
                infoclose.Visible = true; readsts.Visible = true;
                infoclose.InnerHtml = "Please wait.. posting in progress";
                PostingTahap1();
            }
            #endregion
            #region Execute Closing
            //ps:
            //ClosingFacade Cl = new ClosingFacade();
            //Closper objCl = new Closper();
            //int ID = Cl.GetIDClosing(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), sts);
            //if (ID > 0) { objCl.ID = ID; }
            //objCl.Tahun = int.Parse(ddlTahun.SelectedValue);
            //objCl.Bulan = int.Parse(ddlBulan.SelectedValue);
            //objCl.RowStatus = 1;
            //objCl.ModulName = sts;
            //if (ID > 0)
            //{
            //    objCl.LastModifiedBy = ((Users)Session["Users"]).UserName.ToString();
            //}
            //else
            //{
            //    objCl.CreatedBy = ((Users)Session["Users"]).UserName.ToString();
            //}
            //int result = (ID == 0) ? Cl.Insert(objCl) : Cl.Update(objCl);
            //if (result > 0 && Cl.Error == string.Empty)
            //{
            //    LoadData();
            //    readsts.InnerHtml = string.Empty;
            //}
            //else
            //{
            //    DisplayAJAXMessage(this, Cl.Error);
            //}
            #endregion
        }
        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "AddDelete")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        Fabrikasi objDel = new Fabrikasi();
        //        COGSFabrikasiFacade cogs = new COGSFabrikasiFacade();
        //        int Tahun = int.Parse(GridView1.Rows[index].Cells[0].Text.ToString());
        //        int Bulan = int.Parse(GridView1.Rows[index].Cells[1].Text.ToString());
        //        Fabrikasi objFab = cogs.RetrievebyBulan(Tahun, Bulan);
        //        ddlBulan.SelectedValue = objFab.Bulan.ToString();
        //        ddlTahun.SelectedValue = objFab.Tahun.ToString();
        //        txtID.Text = objFab.ID.ToString();

        //    }
        //}
        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadData();
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClosingFacade Cl = new ClosingFacade();
            string sts = (Purchn.Checked == true) ? "Purchn" : "Produksi";
            int stat = Cl.ClosingStatus(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), sts);
            int dept = Cl.GetClosingStatus("SystemClosingByDept");
            int user = ((Users)Session["Users"]).DeptID;
            int clsAktif = Cl.GetClosingStatus("SystemClosing");
            btnCancel.Visible = (stat == 1 && user == dept && clsAktif == 1) ? true : false;
            btnUpdate.Visible = (stat == 0 && user == dept && clsAktif == 1) ? false : true;
        }
        private void LoadTahun(int Tahun)
        {
            int prevThn = DateTime.Now.Year - 1;
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlTahun.Items.Add(new ListItem(prevThn.ToString(), prevThn.ToString()));
            ddlTahun.SelectedValue = Tahun.ToString();

        }
        private int GetMonthLastClosing(string Modul)
        {
            ClosingFacade Cl = new ClosingFacade();
            int Bln = Cl.LastMonthClosing(int.Parse(ddlTahun.SelectedValue), Modul);
            return (Bln == 0) ? DateTime.Now.Month : Bln;
        }
        public void LoadData()
        {
            ClosingFacade cl = new ClosingFacade();
            ArrayList arrCl = new ArrayList();
            string sts = (Purchn.Checked == true) ? "Purchn" : "Produksi";
            for (int i = 1; i <= 12; i++)
            {
                int id = cl.GetIDClosing(int.Parse(ddlTahun.SelectedValue), i, sts);
                arrCl.Add(new Closper
                {
                    ID = i,
                    nBulan = Global.nBulan(i),
                    Inventory = (cl.ClosingStatus(int.Parse(ddlTahun.SelectedValue), i, "Purchn") == 1) ? "Closed" : "Open",
                    Produksi = (cl.ClosingStatus(int.Parse(ddlTahun.SelectedValue), i, "Produksi") == 1) ? "Closed" : "Open",
                    CreatedBy = cl.GetCloseingBy(id),
                    LastModifiedBy = cl.GetCloseingBy(id)
                });
            }
            clsList.DataSource = arrCl;
            clsList.DataBind();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void Purchn_CheckedChanged(object sender, EventArgs e)
        {
            ClosingFacade Cl = new ClosingFacade();
            string sts = "Purchn";
            int stat = Cl.ClosingStatus(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), sts);
            int dept = Cl.GetClosingStatus("SystemClosingByDept");
            int user = ((Users)Session["Users"]).DeptID;
            int clsAktif = Cl.GetClosingStatus("SystemClosing");
            btnCancel.Visible = (stat == 1 && user == dept && clsAktif == 1) ? true : false;
            btnUpdate.Visible = (stat == 0 && user == dept && clsAktif == 1) ? false : true;
            infoclose.Visible = false;
            readsts.InnerHtml = string.Empty;
        }
        protected void Prod_CheckedChanged(object sender, EventArgs e)
        {
            ClosingFacade Cl = new ClosingFacade();
            string sts = "Produksi";
            int stat = Cl.ClosingStatus(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), sts);
            int dept = Cl.GetClosingStatus("SystemClosingByDept");
            int user = ((Users)Session["Users"]).DeptID;
            int clsAktif = Cl.GetClosingStatus("SystemClosing");
            btnCancel.Visible = (stat == 1 && user == dept && clsAktif == 1) ? true : false;
            btnUpdate.Visible = (stat == 0 && user == dept && clsAktif == 1) ? false : true;
            infoclose.Visible = false;
            readsts.InnerHtml = string.Empty;
        }
        protected void clsList_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dt = e.Item.DataItem as DataRowView;
                HtmlTableCell tdr = (HtmlTableCell)e.Item.FindControl("inv");
                HtmlTableCell tdp = (HtmlTableCell)e.Item.FindControl("prd");
                Closper cl = e.Item.DataItem as Closper;
                if (cl.Inventory == "Closed") { tdr.Attributes.Add("style", "color:Red"); }
                if (cl.Produksi == "Closed") { tdp.Attributes.Add("style", "color:Red"); }
            }
        }
        /**
         * Proses Posting inventory
         * */

        public void PostingInventory(int groupID)
        {
            //cek dulu 
            #region Preparation data
            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;

            }

            string ThBl = ddlTahun.SelectedItem.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            //int groupID = int.Parse(ddlTipeSPP.SelectedItem.ToString());
            int itemTypeid = 0;
            if (groupID == 4)
                itemTypeid = 2;
            else if (groupID == 5)
                itemTypeid = 3;
            else
                itemTypeid = 1;

            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(ddlTahun.SelectedValue) - 1;
            else
                tahun = int.Parse(ddlTahun.SelectedValue);

            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string strAvgPrice = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
                strAvgPrice = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
                strAvgPrice = "FebAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
                strAvgPrice = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
                strAvgPrice = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
                strAvgPrice = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
                strAvgPrice = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
                strAvgPrice = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
                strAvgPrice = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
                strAvgPrice = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
                strAvgPrice = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
                strAvgPrice = "NopAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
                strAvgPrice = "DesAvgPrice";
            }
            #endregion
            #region Kosongkan data bulan proses
            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventory saldoInvKosongkan = new SaldoInventory();
            saldoInvKosongkan.YearPeriod = int.Parse(ddlTahun.SelectedValue);
            saldoInvKosongkan.MonthPeriod = ddlBulan.SelectedIndex;

            saldoInvKosongkan.GroupID = groupID;
            //
            SaldoInventoryProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryProcessFacade(saldoInvKosongkan);

            //kosongkan per group
            strError1 = (RepostingSA == true) ? saldoInvKosongkanProcessFacade.Kosongkan() : string.Empty;
            if (strError1 != string.Empty)
            {
                DisplayAJAXMessage(this, "Posting Kosongkan Saldo Error ...!");
                return;
            }
            #endregion
            //new add update saldo bulan lalu
            #region Update Saldo Bulan Lalu
            if (strError1 == string.Empty)
            {
                SaldoInventory saldoInventory = new SaldoInventory();
                string strError = string.Empty;

                saldoInventory.YearPeriod = int.Parse(ddlTahun.SelectedValue);
                saldoInventory.GroupID = groupID;
                saldoInventory.ItemTypeID = itemTypeid;
                saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;

                SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
                strError = (RepostingSA == true) ? saldoInventoryProcessFacade.UpdateSaldoBlnLalu() : string.Empty;
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Posting Inventori Error utk saldo bulan lalu ...!");
                    return;
                }
            }

            #endregion
            #region Check ItemBaru di inventory
            InventoryFacade invFacade = new InventoryFacade();
            ArrayList arrItemIDBaru = invFacade.RetrieveByItemIDBaru(groupID, itemTypeid, ddlTahun.SelectedValue);
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
                        saldoInventory.YearPeriod = int.Parse(ddlTahun.SelectedValue);
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
            #endregion
            #region Posting proses
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
                        saldoInventory.YearPeriod = int.Parse(ddlTahun.SelectedValue);
                        saldoInventory.GroupID = groupID;
                        saldoInventory.MonthPeriod = ddlBulan.SelectedIndex;
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
                #endregion
                SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
                int intNull = saldoInventoryFacade.UpdateSaldoNull(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex);

                #region reset price
                RepostingFacade repostingfacade = new RepostingFacade();
                ArrayList arrReposting = new ArrayList();
                string strErrors = string.Empty;
                string ThBln = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
                //strErrors = repostingfacade.ResetPrice(groupID, ThBln);
                if (strErrors != string.Empty)
                {
                    DisplayAJAXMessage(this, "Reset price error");
                    return;
                }
                #endregion
                #region reposting price
                //RepostingFacade repostingfacade = new RepostingFacade();
                //ArrayList 
                arrReposting = new ArrayList();
                decimal price = 0;
                arrReposting = repostingfacade.RetrieveReposting(groupID, ThBln);

                foreach (Reposting reposting in arrReposting)
                {
                    price = 0;
                    if (reposting.ItemID > 0)
                    {
                        if (reposting.Process.Trim() == "ReceiptDetail")
                            price = repostingfacade.GetPriceForReceipt(ddlBulan.SelectedIndex.ToString().PadLeft(2, '0'), ddlTahun.SelectedValue.ToString(), reposting.ID, reposting.ItemTypeID);
                        strErrors = repostingfacade.PostingPrice(reposting.ID, reposting.Process, price);
                        if (price == 0)
                            //if (reposting.ItemID == 19192)
                            price = repostingfacade.GetPriceFromPrevious(ddlBulan.SelectedValue.PadLeft(2, '0'), ddlTahun.SelectedValue, reposting.ItemID, reposting.ItemTypeID);

                        strErrors = repostingfacade.PostingSaldoPrice(ddlTahun.SelectedValue, groupID, price, strAvgPrice, reposting.ItemID);

                    }
                }
                #endregion
                DisplayAJAXMessage(this, "Posting Inventory Average Price");
                #region proses average price
                string bln = string.Empty;
                string thn = string.Empty;
                string spp = string.Empty;
                string strQuery = string.Empty;

                bln = ddlBulan.SelectedIndex.ToString();//.SelectedItem.ToString();
                thn = ddlTahun.SelectedValue.ToString();
                string bulane = ddlBulan.SelectedItem.ToString();
                string FirstPeriod = string.Empty;
                string LastPeriod = string.Empty;
                string blne = bln.ToString();
                string s = new string('0', (2 - bln.Length));
                Int32 lastDay = DateTime.DaysInMonth(Convert.ToInt32(thn), Convert.ToInt32(blne));
                string d = new string('0', (2 - lastDay.ToString().Length));
                string jnStok = groupID.ToString();
                FirstPeriod = thn + s + bln + "01";
                LastPeriod = thn + s + bln + d + lastDay;
                ReportFacadeAcc reportFacade = new ReportFacadeAcc();
                strQuery = reportFacade.PostingAvgPrice(FirstPeriod, LastPeriod, jnStok, thn);
                int Nulle = process_avg(strQuery);
                #endregion
                DisplayAJAXMessage(this, "posting Inventory selesai");
                infoclose.InnerHtml = string.Empty;
            }
            //until new repack

        }
        public int process_avg(string Query)
        {
            string strError = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(Query);
            strError = dataAccess.Error;
            //bool xx=dataAccess.

            if (sqlDataReader.HasRows)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        

        public void PostingTahap1()
        {
            #region Prepared Data
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID == 1 && ddlBulan.SelectedValue.Trim() == "Mei" && ddlTahun.SelectedValue.Trim() == "2013")
            {
                DisplayAJAXMessage(this, "Tidak bisa posting saldo awal go live");
                return;
            }
            string ThBl = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');


            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(ddlTahun.SelectedValue) - 1;
            else
                tahun = int.Parse(ddlTahun.SelectedValue);

            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyMonth = "JanQty";
                strQtyLastMonth = "DesQty";
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
            #region kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoInventoryT1 saldoInvKosongkan = new SaldoInventoryT1();
            saldoInvKosongkan.YearPeriod = int.Parse(ddlTahun.SelectedValue);
            saldoInvKosongkan.MonthPeriod = ddlBulan.SelectedIndex;
            //new
            SaldoInventoryT1ProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryT1ProcessFacade(saldoInvKosongkan);

            //kosongkan per group
            strError1 = (RepostingSA == true) ? saldoInvKosongkanProcessFacade.Kosongkan() : string.Empty;
            if (strError1 != string.Empty)
            {
                DisplayAJAXMessage(this, "Posting Kosongkan Saldo Error ...!");
                return;
            }
            #endregion
            #region Update Saldo Bulan sebelumnya
            //new add update saldo bulan lalu
            if (strError1 == string.Empty)
            {
                SaldoInventoryT1 SaldoInventoryT1 = new SaldoInventoryT1();
                string strError = string.Empty;

                SaldoInventoryT1.YearPeriod = int.Parse(ddlTahun.SelectedValue);
                SaldoInventoryT1.MonthPeriod = ddlBulan.SelectedIndex;
                SaldoInventoryT1.ItemTypeID = 1;

                SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
                strError = (RepostingSA == true) ? SaldoInventoryT1ProcessFacade.UpdateSaldoBlnLalu() : string.Empty;
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Posting Inventori Error utk saldo bulan lalu ...!");
                    return;
                }
            }
            #endregion
            #region masukan itembaru ke dalam proses jika ada
            //cek itemID baru pada inventory
            FC_ItemsFacade FC_ItemsFacade = new FC_ItemsFacade();
            ArrayList arrItemIDBaru = FC_ItemsFacade.RetrieveByItemIDBaruT1(ddlTahun.SelectedValue);
            if (FC_ItemsFacade.Error == string.Empty && arrItemIDBaru.Count > 0)
            {
                SaldoInventoryT1 SaldoInventoryT1 = new SaldoInventoryT1();
                foreach (FC_Items inv in arrItemIDBaru)
                {
                    string strError = string.Empty;
                    SaldoInventoryT1 = new SaldoInventoryT1();

                    if (inv.ID > 0)
                    {
                        //insert ItemID baru Saja utk tahun tersebut (inventori, asset, biaya)
                        SaldoInventoryT1.ItemID = inv.ID;
                        SaldoInventoryT1.YearPeriod = int.Parse(ddlTahun.SelectedValue);
                        SaldoInventoryT1.GroupID = inv.GroupID;
                        SaldoInventoryT1.ItemTypeID = inv.ItemTypeID;
                        // 1 = inventory

                        SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
                        strError = SaldoInventoryT1ProcessFacade.Insert();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Inventori Error utk Item Baru...!");
                            return;
                        }
                    }
                }

            }
            #endregion
            #region Porting Proses
            FC_ItemsFacade FC_ItemsSaldo = new FC_ItemsFacade();
            ArrayList arrSaldo = FC_ItemsSaldo.RetrieveforSaldo(ThBl);
            if (FC_ItemsSaldo.Error == string.Empty && arrSaldo.Count > 0)
            {
                foreach (FC_Items fcItems in arrSaldo)
                {
                    if (fcItems.Stock != 0 || fcItems.Price != 0)
                    {
                        SaldoInventoryT1 SaldoInventoryT1 = new SaldoInventoryT1();
                        string strError = string.Empty;
                        SaldoInventoryT1.YearPeriod = int.Parse(ddlTahun.SelectedValue);
                        SaldoInventoryT1.MonthPeriod = ddlBulan.SelectedIndex;
                        SaldoInventoryT1.ItemID = fcItems.ItemID;
                        SaldoInventoryT1.Quantity = fcItems.Stock;
                        SaldoInventoryT1.GroupID = fcItems.GroupID;
                        SaldoInventoryT1.ItemTypeID = fcItems.ItemTypeID;
                        SaldoInventoryT1.Posting = 0;
                        SaldoInventoryT1.SaldoPrice = fcItems.Price;
                        SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
                        strError = SaldoInventoryT1ProcessFacade.Update();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Saldo Inventory Error ...!");
                            return;
                        }
                    }

                }
                #endregion
                #region Ganti dBNull dengan 0
                SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
                int intNull = SaldoInventoryT1Facade.UpdateSaldoNull(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex);
            }
            #endregion
            #region proses posting
            ArrayList arrt1saldo = new ArrayList();
            T1_SaldoPerLokasiFacade t1saldoF = new T1_SaldoPerLokasiFacade();
            tahun = 0;
            int bulan = 0;
            string thnbln0 = string.Empty;
            //if (Convert.ToInt32(ddlBulan.SelectedValue.Trim()) == 1)
            if (ddlBulan.SelectedIndex == 1)
            {
                tahun = Convert.ToInt32(ddlTahun.SelectedValue.Trim()) - 1;
                bulan = 12;
            }
            else
            {
                tahun = Convert.ToInt32(ddlTahun.SelectedValue.Trim());
                bulan = ddlBulan.SelectedIndex - 1;
            }
            thnbln0 = tahun.ToString() + bulan.ToString().PadLeft(2, '0');
            string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            t1saldoF.KosongkanSaldo(thnbln);
            arrt1saldo = t1saldoF.RetrieveByThnBln(thnbln0, thnbln);
            foreach (T1_SaldoPerLokasi t1saldo in arrt1saldo)
            {
                t1saldo.ThnBln = thnbln;
                t1saldo.CreatedBy = users.UserName;
                t1saldoF.Insert(t1saldo);
            }
            DisplayAJAXMessage(this, "Posting WIP selesai ...!");
            //ddlBulan.SelectedIndex = 0;
            #endregion
        }
    }
}