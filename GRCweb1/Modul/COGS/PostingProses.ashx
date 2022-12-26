<%@ WebHandler Language="C#" Class="PostingProses" %>

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using Cogs;
using Factory;
using DataAccessLayer;
using System.Net;
using System.Net.Mail;

public class PostingProses : IHttpHandler,IReadOnlySessionState,IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int Bulan = (string.IsNullOrEmpty(context.Request["bln"]))?0:int.Parse(context.Request["bln"]);
        string Tahun = context.Request["Thn"];
        Users usr = (Users)context.Session["Users"];
        string user = usr.UserName;
        bool wpo = bool.Parse(context.Request["wpo"]);
        if (wpo == true)
        {
            int xx=0;
            string PurchnKode = (string.IsNullOrEmpty(context.Request["ID"])) ? string.Empty : context.Request["ID"].ToString();
            if (PurchnKode != string.Empty)
            {
                GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
                GroupsPurchn arrGroupsPurchn = groupsPurchnFacade.RetrieveById(int.Parse(PurchnKode.ToString()));
                
                if (int.Parse(PurchnKode.ToString()) > 0)
                {

                    if (int.Parse(PurchnKode.ToString()) <= 10)
                    {
                        PostingInventory(int.Parse(PurchnKode.ToString()), Bulan, Tahun, context, arrGroupsPurchn.GroupDescription);
                    }
                    else
                    {
                        context.Response.Write(InvClosing(int.Parse(Tahun), Bulan, user, "Purchn"));
                    }
                }
            }
        }
        else
        {
            if (context.Request["ID"] == "1")
            {
                PostingProduction(Bulan, Tahun, context, user);
            }
            else if(context.Request["ID"] == "2")
            {
                PostingBJ(Bulan, Tahun, context, user);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    public void PostingProduction(int Bulan, string Tahun, HttpContext context, string user)
    {
        #region Prepared Data
        if (user == "1" && Bulan == 5 && Tahun.Trim() == "2013")
        {
            context.Response.Write("Tidak bisa posting saldo awal go live");
            return;
        }
        string ThBl = Tahun.ToString() + Bulan.ToString().PadLeft(2, '0');


        int tahun = 0;
        if (Bulan == 1)
            tahun = int.Parse(Tahun) - 1;
        else
            tahun = int.Parse(Tahun);

        string strQtyLastMonth = string.Empty;
        string strQtyMonth = string.Empty;
        string strAvgPriceLastMonth = string.Empty;
        if (Bulan == 1)
        {
            strQtyMonth = "JanQty";
            strQtyLastMonth = "DesQty";
            strAvgPriceLastMonth = "DesAvgPrice";
        }
        else if (Bulan == 2)
        {
            strQtyMonth = "FebQty";
            strQtyLastMonth = "JanQty";
            strAvgPriceLastMonth = "JanAvgPrice";
        }
        else if (Bulan == 3)
        {
            strQtyMonth = "MarQty";
            strQtyLastMonth = "FebQty";
            strAvgPriceLastMonth = "febAvgPrice";
        }
        else if (Bulan == 4)
        {
            strQtyMonth = "AprQty";
            strQtyLastMonth = "MarQty";
            strAvgPriceLastMonth = "MarAvgPrice";
        }
        else if (Bulan == 5)
        {
            strQtyMonth = "MeiQty";
            strQtyLastMonth = "AprQty";
            strAvgPriceLastMonth = "AprAvgPrice";
        }
        else if (Bulan == 6)
        {
            strQtyMonth = "JunQty";
            strQtyLastMonth = "MeiQty";
            strAvgPriceLastMonth = "MeiAvgPrice";
        }
        else if (Bulan == 7)
        {
            strQtyMonth = "JulQty";
            strQtyLastMonth = "JunQty";
            strAvgPriceLastMonth = "JunAvgPrice";
        }
        else if (Bulan == 8)
        {
            strQtyMonth = "AguQty";
            strQtyLastMonth = "JulQty";
            strAvgPriceLastMonth = "JulAvgPrice";
        }
        else if (Bulan == 9)
        {
            strQtyMonth = "SepQty";
            strQtyLastMonth = "AguQty";
            strAvgPriceLastMonth = "AguAvgPrice";
        }
        else if (Bulan == 10)
        {
            strQtyMonth = "OktQty";
            strQtyLastMonth = "SepQty";
            strAvgPriceLastMonth = "SepAvgPrice";
        }
        else if (Bulan == 11)
        {
            strQtyMonth = "NovQty";
            strQtyLastMonth = "OktQty";
            strAvgPriceLastMonth = "OktAvgPrice";
        }
        else if (Bulan == 12)
        {
            strQtyMonth = "DesQty";
            strQtyLastMonth = "NovQty";
            strAvgPriceLastMonth = "NovAvgPrice";
        }

        #endregion
        #region kosongkan dahulu per bln proses
        string strError1 = string.Empty;
        SaldoInventoryT1 saldoInvKosongkan = new SaldoInventoryT1();
        saldoInvKosongkan.YearPeriod = int.Parse(Tahun);
        saldoInvKosongkan.MonthPeriod = Bulan;
        //new
        SaldoInventoryT1ProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryT1ProcessFacade(saldoInvKosongkan);

        //kosongkan per group
        strError1 = saldoInvKosongkanProcessFacade.Kosongkan();
        if (strError1 != string.Empty)
        {
           context.Response.Write("Posting Kosongkan Saldo Error ...!");
            return;
        }
        #endregion
        #region Update Saldo Bulan sebelumnya
        //new add update saldo bulan lalu
        if (strError1 == string.Empty)
        {
            SaldoInventoryT1 SaldoInventoryT1 = new SaldoInventoryT1();
            string strError = string.Empty;

            SaldoInventoryT1.YearPeriod = int.Parse(Tahun);
            SaldoInventoryT1.MonthPeriod = Bulan;
            SaldoInventoryT1.ItemTypeID = 1;

            SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
           // strError = SaldoInventoryT1ProcessFacade.UpdateSaldoBlnLalu();
            if (strError != string.Empty)
            {
                context.Response.Write( "Posting Inventori Error utk saldo bulan lalu ...!");
                return;
            }
        }
        #endregion
        #region masukan itembaru ke dalam proses jika ada
        //cek itemID baru pada inventory
        FC_ItemsFacade FC_ItemsFacade = new FC_ItemsFacade();
        ArrayList arrItemIDBaru = FC_ItemsFacade.RetrieveByItemIDBaruT1(Tahun);
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
                    SaldoInventoryT1.YearPeriod = int.Parse(Tahun);
                    SaldoInventoryT1.GroupID = inv.GroupID;
                    SaldoInventoryT1.ItemTypeID = inv.ItemTypeID;
                    // 1 = inventory

                    SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
                   // strError = SaldoInventoryT1ProcessFacade.Insert();
                    if (strError != string.Empty)
                    {
                        context.Response.Write( "Posting Inventori Error utk Item Baru...!");
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
                    SaldoInventoryT1.YearPeriod = int.Parse(Tahun);
                    SaldoInventoryT1.MonthPeriod = Bulan;
                    SaldoInventoryT1.ItemID = fcItems.ItemID;
                    SaldoInventoryT1.Quantity = fcItems.Stock;
                    SaldoInventoryT1.GroupID = fcItems.GroupID;
                    SaldoInventoryT1.ItemTypeID = fcItems.ItemTypeID;
                    SaldoInventoryT1.Posting = 0;
                    SaldoInventoryT1.SaldoPrice = fcItems.Price;
                    SaldoInventoryT1ProcessFacade SaldoInventoryT1ProcessFacade = new SaldoInventoryT1ProcessFacade(SaldoInventoryT1);
                  //  strError = SaldoInventoryT1ProcessFacade.Update();
                    if (strError != string.Empty)
                    {
                        context.Response.Write( "Posting Saldo Inventory Error ...!");
                        return;
                    }
                }

            }
        #endregion
            #region Ganti dBNull dengan 0
            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            int intNull = SaldoInventoryT1Facade.UpdateSaldoNull(int.Parse(Tahun), Bulan);
        }
            #endregion
        #region proses posting
        ArrayList arrt1saldo = new ArrayList();
        T1_SaldoPerLokasiFacade t1saldoF = new T1_SaldoPerLokasiFacade();
        tahun = 0;
        int bulan = 0;
        string thnbln0 = string.Empty;
        //if (Convert.ToInt32(Bulan.Trim()) == 1)
        if (Bulan == 1)
        {
            tahun = Convert.ToInt32(Tahun.Trim()) - 1;
            bulan = 12;
        }
        else
        {
            tahun = Convert.ToInt32(Tahun.Trim());
            bulan = Bulan - 1;
        }
        thnbln0 = tahun.ToString() + bulan.ToString().PadLeft(2, '0');
        string thnbln = Tahun.Trim() + Bulan.ToString().PadLeft(2, '0');
        t1saldoF.KosongkanSaldo(thnbln);
        arrt1saldo = t1saldoF.RetrieveByThnBln(thnbln0, thnbln);
        foreach (T1_SaldoPerLokasi t1saldo in arrt1saldo)
        {
            t1saldo.ThnBln = thnbln;
            t1saldo.CreatedBy =user;
           // t1saldoF.Insert(t1saldo);
        }
        context.Response.Write( "2:Posting WIP selesai ...");
        //Bulan = 0;
        #endregion
    }
    public void PostingBJ(int Bulan, string Tahun, HttpContext context, string user)
    {
        
        string ThBl = Tahun.ToString() + Bulan.ToString().PadLeft(2, '0');
        int tahun = 0;
        if (Bulan == 1)
            tahun = int.Parse(Tahun) - 1;
        else
            tahun = int.Parse(Tahun);

        string strQtyLastMonth = string.Empty;
        string strQtyMonth = string.Empty;
        string strAvgPriceLastMonth = string.Empty;
        if (Bulan == 1)
        {
            strQtyLastMonth = "DesQty";
            strQtyMonth = "JanQty";
            strAvgPriceLastMonth = "DesAvgPrice";
        }
        else if (Bulan == 2)
        {
            strQtyMonth = "FebQty";
            strQtyLastMonth = "JanQty";
            strAvgPriceLastMonth = "JanAvgPrice";
        }
        else if (Bulan == 3)
        {
            strQtyMonth = "MarQty";
            strQtyLastMonth = "FebQty";
            strAvgPriceLastMonth = "febAvgPrice";
        }
        else if (Bulan == 4)
        {
            strQtyMonth = "AprQty";
            strQtyLastMonth = "MarQty";
            strAvgPriceLastMonth = "MarAvgPrice";
        }
        else if (Bulan == 5)
        {
            strQtyMonth = "MeiQty";
            strQtyLastMonth = "AprQty";
            strAvgPriceLastMonth = "AprAvgPrice";
        }
        else if (Bulan == 6)
        {
            strQtyMonth = "JunQty";
            strQtyLastMonth = "MeiQty";
            strAvgPriceLastMonth = "MeiAvgPrice";
        }
        else if (Bulan == 7)
        {
            strQtyMonth = "JulQty";
            strQtyLastMonth = "JunQty";
            strAvgPriceLastMonth = "JunAvgPrice";
        }
        else if (Bulan == 8)
        {
            strQtyMonth = "AguQty";
            strQtyLastMonth = "JulQty";
            strAvgPriceLastMonth = "JulAvgPrice";
        }
        else if (Bulan == 9)
        {
            strQtyMonth = "SepQty";
            strQtyLastMonth = "AguQty";
            strAvgPriceLastMonth = "AguAvgPrice";
        }
        else if (Bulan == 10)
        {
            strQtyMonth = "OktQty";
            strQtyLastMonth = "SepQty";
            strAvgPriceLastMonth = "SepAvgPrice";
        }
        else if (Bulan == 11)
        {
            strQtyMonth = "NovQty";
            strQtyLastMonth = "OktQty";
            strAvgPriceLastMonth = "OktAvgPrice";
        }
        else if (Bulan == 12)
        {
            strQtyMonth = "DesQty";
            strQtyLastMonth = "NovQty";
            strAvgPriceLastMonth = "NovAvgPrice";
        }


        //kosongkan dahulu per bln proses
        string strError1 = string.Empty;
        SaldoInventoryBJ saldoInvKosongkan = new SaldoInventoryBJ();
        saldoInvKosongkan.YearPeriod = int.Parse(Tahun);
        saldoInvKosongkan.MonthPeriod = Bulan;
        //new
        SaldoInventoryBJProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryBJProcessFacade(saldoInvKosongkan);

        //kosongkan per group
        strError1 = saldoInvKosongkanProcessFacade.Kosongkan();
        if (strError1 != string.Empty)
        {
            context.Response.Write( "Posting Kosongkan Saldo Error ...!");
            return;
        }

        //new add update saldo bulan lalu
        if (strError1 == string.Empty)
        {
            SaldoInventoryBJ SaldoInventoryBJ = new SaldoInventoryBJ();
            string strError = string.Empty;

            SaldoInventoryBJ.YearPeriod = int.Parse(Tahun);
            SaldoInventoryBJ.MonthPeriod = Bulan;
            SaldoInventoryBJ.ItemTypeID = 3;

            SaldoInventoryBJProcessFacade SaldoInventoryBJProcessFacade = new SaldoInventoryBJProcessFacade(SaldoInventoryBJ);
            strError = SaldoInventoryBJProcessFacade.UpdateSaldoBlnLalu();
            if (strError != string.Empty)
            {
                context.Response.Write( "Posting InventoriBJ Error utk saldo bulan lalu ...!");
                return;
            }
        }

        // until new add update saldo bulan lalu
        //cek itemID baru pada inventory
        FC_ItemsFacade FC_ItemsFacade = new FC_ItemsFacade();
        ArrayList arrItemIDBaru = FC_ItemsFacade.RetrieveByItemIDBaruBJ(Tahun);
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
                    SaldoInventoryBJ.YearPeriod = int.Parse(Tahun);
                    SaldoInventoryBJ.GroupID = inv.GroupID;
                    SaldoInventoryBJ.ItemTypeID = inv.ItemTypeID;
                    // 1 = inventory

                    SaldoInventoryBJProcessFacade SaldoInventoryBJProcessFacade = new SaldoInventoryBJProcessFacade(SaldoInventoryBJ);
                    strError = SaldoInventoryBJProcessFacade.Insert();
                    if (strError != string.Empty)
                    {
                        context.Response.Write( "Posting InventoriBJ Error utk Item Baru...!");
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
                    SaldoInventoryBJ.YearPeriod = int.Parse(Tahun);
                    SaldoInventoryBJ.MonthPeriod = Bulan;
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
                        context.Response.Write( "Posting Saldo InventoryBJ Error ...!");
                        return;
                    }
                }
            }
            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            int intNull = SaldoInventoryBJFacade.UpdateSaldoNull(int.Parse(Tahun), Bulan);
        }
        context.Response.Write( "Posting selesai ...!");
        //Bulan = 0;
    }

    public void PostingInventory(int groupID, int Bulan, string Tahun, HttpContext context, string nmGroup)
    {
        //cek dulu 
        #region Preparation data

        string ThBl = Tahun.ToString() + Bulan.ToString().PadLeft(2, '0');
        //int groupID = int.Parse(ddlTipeSPP.SelectedItem.ToString());
        int itemTypeid = 0;
        if (groupID == 4)
            itemTypeid = 2;
        else if (groupID == 5)
            itemTypeid = 3;
        else
            itemTypeid = 1;

        int tahun = 0;
        if (Bulan == 1)
            tahun = int.Parse(Tahun) - 1;
        else
            tahun = int.Parse(Tahun);

        string strQtyLastMonth = string.Empty;
        string strQtyMonth = string.Empty;
        string strAvgPriceLastMonth = string.Empty;
        string strAvgPrice = string.Empty;
        if (Bulan == 1)
        {
            strQtyLastMonth = "DesQty";
            strQtyMonth = "JanQty";
            strAvgPriceLastMonth = "DesAvgPrice";
            strAvgPrice = "JanAvgPrice";
        }
        else if (Bulan == 2)
        {
            strQtyMonth = "FebQty";
            strQtyLastMonth = "JanQty";
            strAvgPriceLastMonth = "JanAvgPrice";
            strAvgPrice = "FebAvgPrice";
        }
        else if (Bulan == 3)
        {
            strQtyMonth = "MarQty";
            strQtyLastMonth = "FebQty";
            strAvgPriceLastMonth = "febAvgPrice";
            strAvgPrice = "MarAvgPrice";
        }
        else if (Bulan == 4)
        {
            strQtyMonth = "AprQty";
            strQtyLastMonth = "MarQty";
            strAvgPriceLastMonth = "AprAvgPrice";
        }
        else if (Bulan == 5)
        {
            strQtyMonth = "MeiQty";
            strQtyLastMonth = "AprQty";
            strAvgPriceLastMonth = "AprAvgPrice";
            strAvgPrice = "MeiAvgPrice";
        }
        else if (Bulan == 6)
        {
            strQtyMonth = "JunQty";
            strQtyLastMonth = "MeiQty";
            strAvgPriceLastMonth = "MeiAvgPrice";
            strAvgPrice = "JunAvgPrice";
        }
        else if (Bulan == 7)
        {
            strQtyMonth = "JulQty";
            strQtyLastMonth = "JunQty";
            strAvgPriceLastMonth = "JunAvgPrice";
            strAvgPrice = "JulAvgPrice";
        }
        else if (Bulan == 8)
        {
            strQtyMonth = "AguQty";
            strQtyLastMonth = "JulQty";
            strAvgPriceLastMonth = "JulAvgPrice";
            strAvgPrice = "AguAvgPrice";
        }
        else if (Bulan == 9)
        {
            strQtyMonth = "SepQty";
            strQtyLastMonth = "AguQty";
            strAvgPriceLastMonth = "AguAvgPrice";
            strAvgPrice = "SepAvgPrice";
        }
        else if (Bulan == 10)
        {
            strQtyMonth = "OktQty";
            strQtyLastMonth = "SepQty";
            strAvgPriceLastMonth = "SepAvgPrice";
            strAvgPrice = "OktAvgPrice";
        }
        else if (Bulan == 11)
        {
            strQtyMonth = "NovQty";
            strQtyLastMonth = "OktQty";
            strAvgPriceLastMonth = "OktAvgPrice";
            strAvgPrice = "NovAvgPrice";
        }
        else if (Bulan == 12)
        {
            strQtyMonth = "DesQty";
            strQtyLastMonth = "NovQty";
            strAvgPriceLastMonth = "NovAvgPrice";
            strAvgPrice = "DesAvgPrice";
        }
        #endregion

        SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
        //Update jika saldo qty is null
        int intNull = saldoInventoryFacade.UpdateSaldoNull(int.Parse(Tahun), Bulan);

        #region reposting price
        RepostingFacade repostingfacade = new RepostingFacade();
        ArrayList arrReposting = new ArrayList();
        string strErrors = string.Empty;
        string ThBln = Tahun.ToString() + Bulan.ToString().PadLeft(2, '0');
        //RepostingFacade repostingfacade = new RepostingFacade();
        //ArrayList 
        arrReposting = new ArrayList();
        decimal price = 0;
        //Get ItemID transaksi di bulan yng di pilih dan itemid yang sudah ada di table saldoinventory
        arrReposting = repostingfacade.RepostingItemID(groupID, ThBln);
        string totaldata = arrReposting.Count.ToString();
        int n = 0;
        foreach (Reposting reposting in arrReposting)
        {
            price = 0;
            n = n + 1;
            //if (reposting.Process.Trim() == "ReceiptDetail")
            //{
            if (reposting.ItemID > 0)
            {
                //get price from last receipt
                price = repostingfacade.GetPriceForReceipt(Bulan.ToString().PadLeft(2, '0'), Tahun.ToString(), reposting.ID, reposting.ItemTypeID);
                //jika masih blm dapet price juga (price==0) cari lagi di bulan sebelumnya
                if (price == 0)
                {
                    //price = repostingfacade.GetPriceFromPrevious(Bulan.ToString().PadLeft(2, '0'), Tahun.ToString(), reposting.ItemID, reposting.ItemTypeID);
                }
                strErrors = repostingfacade.PostingPrice(reposting.ID, reposting.Process, price);
                strErrors += repostingfacade.PostingSaldoPrice(Tahun.ToString(), groupID, price, strAvgPrice, reposting.ItemID);

            }
        }
        #endregion
        //context.Response.Write("Posting Inventory Average Price");
        #region proses average price
        string bln = string.Empty;
        string thn = string.Empty;
        string spp = string.Empty;
        string strQuery = string.Empty;

        bln = Bulan.ToString();//.SelectedItem.ToString();
        thn = Tahun.ToString();
        string bulane = Bulan.ToString();
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
        if (groupID < 11)
        {
            context.Response.Write((groupID + 1) + ": Posting Inventory " + nmGroup + " selesai");
        }
        else
        {
            context.Response.Write("0:Posting Inventory " + nmGroup + " selesai");

        }
        Users usere = (Users)context.Session["Users"];
        if ((groupID + 1) == 11)
        {
            context.Response.Write(InvClosing(int.Parse(thn), int.Parse(bln), usere.UserName.ToString(), "Purchn"));
        }
    }
        //until new repack

    
    public int process_avg(string Query)
    {
        string strError = string.Empty;
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(Query);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows && strError==string.Empty)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public string ProsesSaldoInventory()
    {
        //#region Kosongkan data bulan proses
        ////kosongkan dahulu per bln proses
        //string strError1 = string.Empty;
        //SaldoInventory saldoInvKosongkan = new SaldoInventory();
        //saldoInvKosongkan.YearPeriod = int.Parse(Tahun);
        //saldoInvKosongkan.MonthPeriod = Bulan;

        //saldoInvKosongkan.GroupID = groupID;
        ////
        //SaldoInventoryProcessFacade saldoInvKosongkanProcessFacade = new SaldoInventoryProcessFacade(saldoInvKosongkan);

        ////kosongkan per group
        //strError1 = saldoInvKosongkanProcessFacade.Kosongkan();
        //if (strError1 != string.Empty)
        //{
        //    context.Response.Write("Posting Kosongkan Saldo Error ...!");
        //    return;
        //}
        //#endregion
        ////new add update saldo bulan lalu
        //#region Update Saldo Bulan Lalu
        //if (strError1 == string.Empty)
        //{
        //    SaldoInventory saldoInventory = new SaldoInventory();
        //    string strError = string.Empty;

        //    saldoInventory.YearPeriod = int.Parse(Tahun);
        //    saldoInventory.GroupID = groupID;
        //    saldoInventory.ItemTypeID = itemTypeid;
        //    saldoInventory.MonthPeriod = Bulan;

        //    SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
        //    strError = saldoInventoryProcessFacade.UpdateSaldoBlnLalu();
        //    if (strError != string.Empty)
        //    {
        //        context.Response.Write("Posting Inventori Error utk saldo bulan lalu ...!");
        //        return;
        //    }
        //}

        //#endregion
        //#region Check ItemBaru di inventory

        //InventoryFacade invFacade = new InventoryFacade();
        //ArrayList arrItemIDBaru = invFacade.RetrieveByItemIDBaru(groupID, itemTypeid, Tahun);
        //if (invFacade.Error == string.Empty && arrItemIDBaru.Count > 0)
        //{
        //    SaldoInventory saldoInventory = new SaldoInventory();
        //    foreach (Inventory inv in arrItemIDBaru)
        //    {
        //        string strError = string.Empty;
        //        saldoInventory = new SaldoInventory();

        //        if (inv.ID > 0)
        //        {
        //            //insert ItemID baru Saja utk tahun tersebut (inventori, asset, biaya)
        //            saldoInventory.ItemID = inv.ID;
        //            saldoInventory.YearPeriod = int.Parse(Tahun);
        //            saldoInventory.GroupID = inv.GroupID;
        //            // 1 = inventory
        //            saldoInventory.ItemTypeID = itemTypeid;

        //            SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
        //            strError = saldoInventoryProcessFacade.Insert();
        //            if (strError != string.Empty)
        //            {
        //                context.Response.Write("Posting Inventori Error utk Item Baru...!");
        //                return;
        //            }
        //        }
        //    }

        //}
        //#endregion
        //#region Posting proses
        //ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
        //ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(0, 0, "0");
        //if (groupID == 1 || groupID == 2 || groupID == 3 || groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
        //{
        //    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2(groupID, itemTypeid, ThBl);
        //}
        //if (groupID == 4)
        //{
        //    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forAsset(groupID, itemTypeid, ThBl);
        //}
        //if (groupID == 5)
        //{
        //    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forBiaya(groupID, itemTypeid, ThBl);
        //}
        //if (groupID == 10)
        //{
        //    arrReceiptDetail = receiptDetailFacade.RetrieveByGroupIDwithAllSUM2forRepack(groupID, itemTypeid, ThBl);
        //}
        //if (receiptDetailFacade.Error == string.Empty && arrReceiptDetail.Count > 0)
        //{
        //    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
        //    {
        //        if (receiptDetail.Quantity != 0 || receiptDetail.Price != 0)
        //        {
        //            SaldoInventory saldoInventory = new SaldoInventory();
        //            string strError = string.Empty;
        //            saldoInventory.ItemTypeID = itemTypeid;
        //            saldoInventory.YearPeriod = int.Parse(Tahun);
        //            saldoInventory.GroupID = groupID;
        //            saldoInventory.MonthPeriod = Bulan;
        //            saldoInventory.ItemID = receiptDetail.ItemID;
        //            saldoInventory.Quantity = receiptDetail.Quantity;
        //            saldoInventory.Posting = 0;
        //            saldoInventory.SaldoPrice = receiptDetail.Price;
        //            SaldoInventoryProcessFacade saldoInventoryProcessFacade = new SaldoInventoryProcessFacade(saldoInventory);
        //            strError = saldoInventoryProcessFacade.Update();
        //            if (strError != string.Empty)
        //            {
        //                context.Response.Write("Posting Saldo Inventory Error ...!");
        //                return;
        //            }
        //        }

        //    }
        //#endregion
        return string.Empty;
    }
    public string InvClosing(int Tahun, int Bulan, string contexts,string Status)
    {
        #region Execute Closing
        ClosingFacade Cl = new ClosingFacade();
        Closper objCl = new Closper();
        int ID = Cl.GetIDClosing(Tahun, Bulan, Status);
        if (ID > 0) { objCl.ID = ID; }
        objCl.Tahun = Tahun;
        objCl.Bulan = Bulan;
        objCl.RowStatus = 1;
        objCl.ModulName = Status;
        if (ID > 0)
        {
            objCl.LastModifiedBy = contexts;
        }
        else
        {
            objCl.CreatedBy = contexts;
        }
        int result = (ID == 0) ? Cl.Insert(objCl) : Cl.Update(objCl);
        if (result > 0)
        {
            string email = KirimEmail(Status, Global.nBulan(Bulan).ToString() + " " + Tahun.ToString());
            return "0:Proses Closing " + Status + " complete..." + email;
        }
        else
        {
            return Cl.Error;
        }
                
        #endregion
    }
    private string KirimEmail(string DataClosing,string Periode)
    {
        string result = string.Empty;
        try
        {
            MailMessage msg = new MailMessage();
            EmailReportFacade emailFacade = new EmailReportFacade();
            msg.From = new MailAddress("system_support@grcboard.com");
            msg.To.Add(new MailAddress("sandra@grcboard.com"));
            msg.CC.Add(new MailAddress("luyiko@grcboard.com"));
            msg.Bcc.Add(new MailAddress("noreplay@grcboard.com"));
            msg.Subject = "Closing Data " + DataClosing + " Periode : " + Periode;
            msg.Body = "FYI\n\r";
            msg.Body += "Data " + DataClosing + " Periode " + Periode;
            msg.Body += "\n\r Telah dilakukan Closing per " + DateTime.Now.ToString("dd MMM yyyy");
            msg.Body += "Terima Kasih " + "\n\rAuto Remainder by System";
            SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
            smt.Host = emailFacade.mailSmtp();
            smt.Port = emailFacade.mailPort();
            smt.EnableSsl = true;
            smt.DeliveryMethod = SmtpDeliveryMethod.Network;
            smt.UseDefaultCredentials = false;
            smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
            //bypas certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            smt.Send(msg);
            result = "Email terkirim";
        }
        catch (Exception Ex)
        {
            result = Ex.Message;
        }
        return result;
    }
}