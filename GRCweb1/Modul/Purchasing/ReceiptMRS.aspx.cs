using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using GRCweb1.Models;
using Newtonsoft.Json;
using Factory;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Web.Http;
using System.Reflection;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ReceiptMRS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamHeadData> HeadData(int id)
        {
            List<ReceiptMRSNf.ParamHeadData> list = new List<ReceiptMRSNf.ParamHeadData>();
            list = ReceiptMRSNfFacade.HeadData(id);
            return list;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamDtlData> DtlData(int id)
        {
            List<ReceiptMRSNf.ParamDtlData> list = new List<ReceiptMRSNf.ParamDtlData>();
            list = ReceiptMRSNfFacade.DtlData(id);
            return list;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamData> ListData(string KodeReceipt, string KodePo, string KodeSpp)
        {
            List<ReceiptMRSNf.ParamData> list = new List<ReceiptMRSNf.ParamData>();
            list = ReceiptMRSNfFacade.ListData(KodeReceipt, KodePo, KodeSpp);
            return list;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamPo> ListPo(string type)
        {
            List<ReceiptMRSNf.ParamPo> list = new List<ReceiptMRSNf.ParamPo>();
            list = ReceiptMRSNfFacade.GetListPo(type);
            return list;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamItem> ListItem(int Id)
        {
            List<ReceiptMRSNf.ParamItem> list = new List<ReceiptMRSNf.ParamItem>();
            list = ReceiptMRSNfFacade.GetListItem(Id);
            return list;
        }

        [WebMethod]
        public static string CekDate(int ID, string Tanggal)
        {
            string Msg = "";
            DateTime Tgl = DateTime.Parse(Tanggal);
            DateTime DatePo = ReceiptMRSNfFacade.CekDate(ID);
            if (DatePo > Tgl) { Msg = "1"; }
            return Msg;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamInfoItem> InfoItem(int Id)
        {
            List<ReceiptMRSNf.ParamInfoItem> list = new List<ReceiptMRSNf.ParamInfoItem>();
            list = ReceiptMRSNfFacade.GetInfoItem(Id);
            return list;
        }

        [WebMethod]
        public static string CekClosing(string NoPo, string Tanggal)
        {
            string Msg = "";
            string strReceiptCode = NoPo.Substring(0, 2) + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            DateTime nowTgl = new DateTime(DateTime.Parse(Tanggal).Year, DateTime.Parse(Tanggal).Month, DateTime.Parse(Tanggal).Day);
            DateTime LastEntry = ReceiptMRSNfFacade.CekLastDate(strReceiptCode);
            DateTime LastTgl = new DateTime(LastEntry.Year, LastEntry.Month, LastEntry.Day);
            int bulan = DateTime.Parse(Tanggal).Month;
            int tahun = DateTime.Parse(Tanggal).Year;
            int CountClosing = ReceiptMRSNfFacade.CekCountClose(bulan, tahun);
            if (CountClosing > 0)
            {
                ReceiptMRSNf.ParamCekClosing clsBln = ReceiptMRSNfFacade.RetrieveByStatus(bulan, tahun);
                if (clsBln.Status == 1)
                {
                    Msg = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                }
                else { if (nowTgl < LastTgl) { Msg = "Anda melewati Tanggal input terakhir"; } }
            }
            return Msg;
        }

        [WebMethod]
        public static List<ReceiptMRSNf.ParamInfoDetailPo> InfoDetailPo(int Id)
        {
            List<ReceiptMRSNf.ParamInfoDetailPo> list = new List<ReceiptMRSNf.ParamInfoDetailPo>();
            list = ReceiptMRSNfFacade.InfoDetailPo(Id);
            return list;
        }

        [WebMethod]
        public static string ClearArrayList()
        {
            string Msg = "";
            HttpContext.Current.Session["arrayListMrs"] = new ArrayList();
            return Msg;
        }

        [WebMethod]
        public static string AddItem(string item)
        {
            string Msg = "";
            ArrayList arrList = new ArrayList();
            if (HttpContext.Current.Session["arrayListMrs"] != null)
            {
                arrList = (ArrayList)HttpContext.Current.Session["arrayListMrs"];
                if (arrList.Count > 0)
                {
                    foreach (ReceiptMRSNf.ParamAddItem rcp in arrList.ToArray())
                    {
                        if (rcp.Item == item) { Msg = "Item Sudah Ada"; }
                        else
                        {
                            rcp.Item = item;
                            arrList.Add(rcp);
                            HttpContext.Current.Session["arrayListMrs"] = arrList;
                        }
                    }
                }
                else
                {
                    ReceiptMRSNf.ParamAddItem rcp = new ReceiptMRSNf.ParamAddItem();
                    rcp.Item = item;
                    arrList.Add(rcp);
                    HttpContext.Current.Session["arrayListMrs"] = arrList;
                }
            }
            else
            {
                ReceiptMRSNf.ParamAddItem rcp = new ReceiptMRSNf.ParamAddItem();
                rcp.Item = item;
                arrList.Add(rcp);
                HttpContext.Current.Session["arrayListMrs"] = arrList;
            }
            return Msg;
        }

        [WebMethod]
        public static string SaveData(ReceiptMRSNf.ParamHead isiHead, ReceiptMRSNf.isiDetail isiDetail)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            Users users = (Users)HttpContext.Current.Session["Users"];
            int bln = DateTime.Parse(isiHead.Tanggal).Month;
            int thn = DateTime.Parse(isiHead.Tanggal).Year;
            int noBaru = 0;
            string kd = "";
            string getKompany = ReceiptMRSNfFacade.GetKompany(users.UnitKerjaID);
            if (isiHead.TypeReceipt == 1) { kd = getKompany + "S"; }
            if (isiHead.TypeReceipt == 2) { kd = getKompany + "O"; }
            if (isiHead.TypeReceipt == 3) { kd = getKompany + "N"; }
            /*int cekNo = ReceiptMRSNfFacade.CekDocNo(bln, thn, kd);
            ReceiptMRSNf.ParamDocNo receiptDocNo = new ReceiptMRSNf.ParamDocNo();*/
            ReceiptDocNoFacade receiptDocNoFacad = new ReceiptDocNoFacade();
            ReceiptDocNo receiptDocNo = new ReceiptDocNo();
            receiptDocNo = receiptDocNoFacad.RetrieveByReceiptCode(bln, thn, kd);
            if (receiptDocNo.ID == 0)
            {
                noBaru = 1;
                receiptDocNo.ReceiptCode = kd;
                receiptDocNo.NoUrut = 1;
                receiptDocNo.MonthPeriod = bln;
                receiptDocNo.YearPeriod = thn;
            }
            else
            {
                noBaru = receiptDocNo.NoUrut + 1;
                receiptDocNo.ReceiptCode = kd;
                receiptDocNo.NoUrut = receiptDocNo.NoUrut + 1;
            }
            int supid = ReceiptMRSNfFacade.SupPo(isiHead.PoID);
            isiHead.SupplierType = 0;
            isiHead.SupplierID = supid;
            isiHead.PoID = isiHead.PoID;
            isiHead.PoNo = isiHead.NoPo;
            isiHead.ReceiptNo = "";
            isiHead.ReceiptDate = DateTime.Parse(isiHead.Tanggal);

            isiHead.TTagihanDate = DateTime.Parse(isiHead.Tanggal);
            isiHead.JTempoDate = DateTime.Parse(isiHead.Tanggal);
            isiHead.FakturPajakDate = DateTime.Parse(isiHead.Tanggal);

            isiHead.ReceiptType = 6;
            isiHead.SupplierType = 0;
            isiHead.DepoID = users.UnitKerjaID;
            isiHead.Status = 0;
            isiHead.CreatedBy = users.UserName;
            isiHead.ItemTypeID = 1;
            isiHead.tipeAsset = 0;
            int intReceiptDocNoID = 0;
            int intReceiptID = 0;
            int intResult = 0;
            string bl = "";
            string th = "";
            if (receiptDocNo.NoUrut == 1)
            {
                AbstractTransactionFacade receiptDocNoFacade = new ReceiptDocNoFacade(receiptDocNo);
                intReceiptDocNoID = receiptDocNoFacade.Insert(transManager);
                if (receiptDocNoFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return receiptDocNoFacade.Error;
                }
            }
            else
            {
                AbstractTransactionFacade receiptDocNoFacade = new ReceiptDocNoFacade(receiptDocNo);
                intReceiptDocNoID = receiptDocNoFacade.Update(transManager);
                if (receiptDocNoFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return receiptDocNoFacade.Error;
                }
            }
            absTrans = new ReceiptMRSNfFacade(isiHead);
            intReceiptID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intReceiptID > 0)
            {
                if (receiptDocNo.MonthPeriod < 10) { bl = "0" + receiptDocNo.MonthPeriod.ToString(); }
                else { bl = receiptDocNo.MonthPeriod.ToString(); }
                th = receiptDocNo.YearPeriod.ToString().Substring(2, 2);
                isiHead.ReceiptNo = receiptDocNo.ReceiptCode + th + bl + "-" + receiptDocNo.NoUrut.ToString().PadLeft(5, '0');
                isiHead.ID = intReceiptID;
                Msg = isiHead.ReceiptNo;

                //create no
                absTrans = new ReceiptMRSNfFacade(isiHead);
                absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }

                // utk update status pada PO
                ReceiptMRSNf.ParamSisaPo cekPOPurchn = ReceiptMRSNfFacade.GetSisaPo(isiHead.PoID);
                if (cekPOPurchn.ID > 0)
                {
                    if (cekPOPurchn.QtyPO == cekPOPurchn.QtyReceipt) { cekPOPurchn.Status = 2; }
                    else { cekPOPurchn.Status = 1; }
                    string updateStatusPo = ReceiptMRSNfFacade.UpdateStatusPo(cekPOPurchn.Status, isiHead.PoID);
                }
                foreach (ReceiptMRSNf.ParamDtl isiD in isiDetail.isiDtl)
                {
                    if (isiD.TimbanganBPAS > 0) { isiD.TimbanganBPAS = isiD.TimbanganBPAS; }
                    else{ isiD.TimbanganBPAS = 0; }
                    isiD.ReceiptID = intReceiptID;
                    absTrans = new ReceiptMRSDtlNfFacade(isiD);
                    intResult = absTrans.Insert(transManager);
                    int idDtl = intResult; isiD.ID = intResult;
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    //update status PO by receipt
                    int podetailID = isiD.PODetailID;
                    string updateStatusPoDtl= ReceiptMRSNfFacade.UpdateStatusPoDtl(podetailID);

                    //insert flag multigudang
                    ReceiptMRSNf.ParamSppIdDtl sppidtdl = ReceiptMRSNfFacade.GetSppIdDtl(isiD.SppID);
                    if (sppidtdl.SatuanID == 2)
                    {
                        int DeptID = ReceiptMRSNfFacade.GetDept(sppidtdl.UserID);
                        SPPMultiGudang sppm = new SPPMultiGudang();
                        sppm.SPPID = sppidtdl.ID;
                        sppm.GudangID = DeptID;
                        sppm.ItemID = isiD.ItemID;
                        sppm.GroupID = isiD.GroupID;
                        sppm.ItemTypeID = isiD.ItemTypeID;
                        sppm.QtyReceipt = decimal.Parse(isiD.Quantity.ToString());
                        sppm.QtyPakai = decimal.Parse("0");
                        sppm.Aktif = 1;
                        absTrans = new SPPMultiGudangFacade(sppm);
                        int intResultu = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    // utk add ke table inventori menambahkan jumlah receipt
                    //Proses update ke inventory ketika ada gagal tidak reverse lagi
                    //karena tidak ikut trans manager proses nya perlu dirubah
                    string updateJumInventory = ReceiptMRSNfFacade.UpdateJumInventory(isiD.Quantity, isiD.ItemID);
                    
                    //update average price
                    double LastPrice = ReceiptMRSNfFacade.LastPrice(isiD.ItemID, isiD.ItemTypeID);
                    double Price = Convert.ToDouble(isiD.Price);
                    double EndStock = ReceiptMRSNfFacade.GetStock(isiD.ItemID, isiD.ItemTypeID);
                    double QtyBeli = Convert.ToDouble(isiD.Quantity);
                    double AvgPrice = ((EndStock * LastPrice) + (QtyBeli * Price)) / (EndStock + QtyBeli);
                    SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
                    SaldoInventory objSaldoInventory = new SaldoInventory();
                    objSaldoInventory.ItemID = isiD.ItemID;
                    objSaldoInventory.GroupID = isiD.GroupID;
                    objSaldoInventory.ItemTypeID = isiD.ItemTypeID;
                    objSaldoInventory.AvgPrice = Convert.ToInt32(AvgPrice);
                    objSaldoInventory.MonthPeriod = isiHead.ReceiptDate.Month;
                    objSaldoInventory.YearPeriod = isiHead.ReceiptDate.Year;
                    //cek apakah data SaldoInventory tahun ini sudah ada
                    intResult = ReceiptMRSNfFacade.CekSaldoInvetory(isiD.ItemID, isiHead.ReceiptDate.Year, isiD.ItemTypeID);
                    if (intResult == 0)
                    {
                        intResult = saldoInventoryFacade.Insert(objSaldoInventory);
                    }
                    intResult = saldoInventoryFacade.UpdateSaldoAvgPriceBlnIni(objSaldoInventory);                    
                }
                //selesai
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            HttpContext.Current.Session["arrayListMrs"] = new ArrayList();
            return Msg;
        }
    }
}