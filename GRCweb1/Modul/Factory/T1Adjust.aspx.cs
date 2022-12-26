using BusinessFacade;
using Dapper;
using DataAccessLayer;
using Domain;
using Factory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

namespace GRCweb1.Modul.Factory
{
    public partial class T1Adjust : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<FC_ItemsNF> GetPartonoT1(string partno)
        {
            List<FC_ItemsNF> Partno = new List<FC_ItemsNF>();
            Partno = FC_ItemsNFFacade.GetPartnoT1(partno);
            return Partno;
        }


        [WebMethod]
        public static List<FC_LokasiNF> GetLokasiT1(string lokasi)
        {
            List<FC_LokasiNF> Partno = new List<FC_LokasiNF>();
            Partno = FC_LokasiNFFacade.GetLokasiT1(lokasi);
            return Partno;
        }

        [WebMethod]
        public static List<FC_LokasiNF> GetStokLokasiT1(string partno, string lokasi, string thnbln, string thnbln0)
        {
            List<FC_LokasiNF> Partno = new List<FC_LokasiNF>();
            Partno = FC_LokasiNFFacade.GetLokasiStok(partno, lokasi, thnbln, thnbln0);
            return Partno;
        }


        [WebMethod]
        public static int SimpanAdjust(T1AdjustAll T1AdjustAll, T1_AdjustNF T1Adjust)
        {
            var msg = 1;
            T1_AdjustNF T1adjust = new T1_AdjustNF();
            T1_AdjustNFFacade T1adjustFacade = new T1_AdjustNFFacade();
            T1_AdjustDetailNF T1adjustDetail = new T1_AdjustDetailNF();
            T1_AdjustDetailFacade T1adjustDetailFacade = new T1_AdjustDetailFacade();

            Users users = (Users)HttpContext.Current.Session["Users"];
            string Username = users.UserName;
            int DepoID = users.DepoID;
            DateTime date = DateTime.Parse(T1Adjust.TglAdjust);
            int intdocno = T1adjustFacade.GetDocNo(date) + 1;
            string docno = "Adj" + date.ToString("yy") + date.ToString("MM") + intdocno.ToString().PadLeft(4, '0');

            T1Adjust.AdjustNo = docno;
            T1Adjust.AdjustDate = date;
            T1Adjust.CreatedBy = Username;
            T1Adjust.DepoID = DepoID;

            T1_AdjustProcessNFFacade AdjustProcessFacade = new T1_AdjustProcessNFFacade(T1Adjust, T1AdjustAll);
            string strError = AdjustProcessFacade.Insert();
            if (strError != " " || strError != null || strError != "")
            {
                msg = 0;
            }
            return msg;
        }

        [WebMethod]
        public static string ListT1Adjust(string tgl, int param)
        {
            List<T1_AdjustDetailNF> Adjust = new List<T1_AdjustDetailNF>();
            Adjust = T1_AdjustDetailNFFacade.ListT1Adjust(tgl, param);
            return JsonConvert.SerializeObject(Adjust);
        }


        [WebMethod]
        public static int Approve(T1AdjustAll T1AdjustAll)
        {
            var msg = 1;
            List<T1_AdjustDetailNF> Adjust = new List<T1_AdjustDetailNF>();
            T1_AdjustDetailNFFacade T1AdjustDetailFacade = new T1_AdjustDetailNFFacade();
            BM_DestackingNF destacking = new BM_DestackingNF();
            BM_DestackingNFFacade destackingF = new BM_DestackingNFFacade();
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Username = users.UserName;
            T1_SerahNF T1serah = new T1_SerahNF();
            T1_SerahNFFacade SerahFacade = new T1_SerahNFFacade();
            int destid = 0;
            int intResult = 0;

            FC_LokasiNF lokasi = new FC_LokasiNF();
            FC_LokasiNFFacade lokasiF = new FC_LokasiNFFacade();

            int lokAdjustID = 0;
            int adalokasi = lokasiF.check("AdjOut");
            if (adalokasi == 0)
            {
                lokasi.Lokasi = "AdjOut";
                lokasiF.Insert(lokasi);
            }
            else
            {
                lokasi = lokasiF.GetLokasi("AdjOut");
            }
            lokAdjustID = lokasi.ID;

            int lokAdjustID1 = 0;
            FC_LokasiNF lokasi1 = new FC_LokasiNF();
            FC_LokasiNFFacade lokasiF1 = new FC_LokasiNFFacade();
            int adalokasi1 = lokasiF1.check("AdjIn");
            if (adalokasi1 == 0)
            {
                lokasi1.Lokasi = "AdjIn";
                lokasiF1.Insert(lokasi1);
            }
            else
            {
                lokasi1 = lokasiF1.GetLokasi("AdjIn");
            }
            lokAdjustID1 = lokasi1.ID;

            foreach (T1_AdjustDetailNF t1AdjustDetail in T1AdjustAll.T1AdjustDetail)
            {
                t1AdjustDetail.Apv = users.Apv;
                t1AdjustDetail.CreatedBy = users.UserName;
                if (t1AdjustDetail.AdjustType.ToUpper().Trim() == "IN"){
                    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                    transManager = new TransactionManager(Global.ConnectionString());
                    transManager.BeginTransaction();
                    TransactionManager transManager1 = new TransactionManager(Global.ConnectionString());
                    transManager1 = new TransactionManager(Global.ConnectionString());
                    transManager1.BeginTransaction();
                    //siapkan data palet
                    int paletID = 0;
                    //siapkan data partno
                    FC_ItemsNF fcitems = new FC_ItemsNF();
                    fcitems = FC_ItemsNFFacade.CekPartno(t1AdjustDetail.Partno);
                    int itemID = fcitems.ID;

                    //siapkan data lokasi
                    int lokasiID = 0;
                    lokasiID = lokAdjustID1;

                    //insert data destacking
                    T1_JemurNF jemur = new T1_JemurNF();
                    destid = 0;
                    destacking.LokasiID = lokasiID;
                    destacking.PaletID = paletID;
                    destacking.ItemID = itemID;
                    destacking.CreatedBy = users.UserName;
                    destacking.TglProduksi = t1AdjustDetail.AdjustDate;
                    int qtyadjust = t1AdjustDetail.QtyIn + t1AdjustDetail.QtyOut;
                    destacking.Qty = qtyadjust;
                    destid = destackingF.Insert(destacking);
                    destacking.Status = "2";
                    destacking.ID = destid;
                    int destid1 = destackingF.UpdateForAdjust(destacking);
                    jemur.DestID = destid;
                    jemur.ItemID = itemID;
                    jemur.TglJemur = t1AdjustDetail.AdjustDate;
                    jemur.QtyIn = qtyadjust;
                    jemur.QtyOut = qtyadjust;
                    jemur.CreatedBy = users.UserName;

                    AbstractTransactionFacadeF absTrans = new T1_JemurNFFacade(jemur);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        msg = 0;
                        break;
                    }
                    jemur.ID = intResult;
                    int intResult2 = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        msg = 0;
                        break;
                    }
                    //insert data di tabel T1_serah dgn lokasi Adjust
                    int intResult1 = 0;
                    T1_SerahNF objSerah = new T1_SerahNF();
                    objSerah.ItemIDSer = t1AdjustDetail.ItemID;
                    objSerah.QtyIn = qtyadjust;
                    objSerah.LokasiID = lokasiID.ToString();
                    objSerah.DestID = destid;
                    objSerah.ItemIDDest = t1AdjustDetail.ItemID;
                    objSerah.HPP = 0;
                    objSerah.JemurID = intResult;
                    objSerah.CreatedBy = users.UserName;
                    objSerah.TglSerah = t1AdjustDetail.DateAdjust;
                    objSerah.SFrom = "Adjust";
                    objSerah.PartnoSer = t1AdjustDetail.Partno;
                    objSerah.LokasiSer = t1AdjustDetail.Lokasi;
                    AbstractTransactionFacadeF absTrans1 = new T1_SerahNFFacade(objSerah);
                    intResult1 = absTrans1.Insert(transManager1);

                    if (absTrans1.Error != string.Empty)
                    {
                        transManager1.RollbackTransaction();
                        msg = 0;
                        break;
                    }
                    transManager.CommitTransaction();
                    transManager.CloseConnection();
                    transManager1.CommitTransaction();
                    transManager1.CloseConnection();

                }
                else
                {
                    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                    transManager = new TransactionManager(Global.ConnectionString());
                    transManager.BeginTransaction();
                    TransactionManager transManager1 = new TransactionManager(Global.ConnectionString());
                    transManager1 = new TransactionManager(Global.ConnectionString());
                    transManager1.BeginTransaction();
                    //Adjust from count sheet
                    //update tabel destacking rowstatus=1
                    if (t1AdjustDetail.DestID == 0)
                    {
                        //siapkan data palet
                        int paletID = 0;

                        //siapkan data partno
                        FC_ItemsNF fcitems = new FC_ItemsNF();
                        FC_ItemsNFFacade fcitemsF = new FC_ItemsNFFacade();
                        fcitems = fcitems = FC_ItemsNFFacade.CekPartno(t1AdjustDetail.Partno);
                        int itemID = fcitems.ID;

                        //siapkan data lokasi
                        int lokasiID = 0;
                        lokasiID = lokAdjustID1;
                        destid = 0;
                        destacking.LokasiID = lokasiID;
                        destacking.PaletID = paletID;
                        destacking.ItemID = itemID;
                        destacking.CreatedBy = users.UserName;
                        destacking.TglProduksi = t1AdjustDetail.AdjustDate;
                        int qtyadjust = t1AdjustDetail.QtyIn + t1AdjustDetail.QtyOut;
                        destacking.Qty = qtyadjust * -1;
                        //destacking.Qty = 0;
                        destid = destackingF.Insert(destacking);
                        destacking.ID = destid;
                        int destid1 = destackingF.UpdateForAdjust(destacking);
                        break;
                    }
                    else
                    {
                        destacking.ID = t1AdjustDetail.DestID;
                        destacking.CreatedBy = users.UserName;
                        destid = destackingF.UpdateForAdjust(destacking);
                    }
                    T1_JemurNF jemur = new T1_JemurNF();
                    T1_JemurNFFacade jemurF = new T1_JemurNFFacade();
                    //cek tabel jemur jika tidak ada -> insert new record
                    int adajemur = 0;
                    jemur = jemurF.GetDataJemurTgl(t1AdjustDetail.TglProduksi.ToString("yyyyMMdd"));
                    //adajemur = jemurF.Check(T1AdjustDetail.DestID);
                    adajemur = jemur.ID;
                    intResult = 0;
                    jemur.DestID = t1AdjustDetail.DestID;
                    jemur.QtyIn = t1AdjustDetail.QtyOut;
                    jemur.QtyOut = t1AdjustDetail.QtyOut;
                    jemur.TglJemur = t1AdjustDetail.AdjustDate;
                    jemur.LokasiID = lokAdjustID.ToString();

                    if (adajemur == 0)
                    {
                        AbstractTransactionFacadeF absTrans = new T1_JemurNFFacade(jemur);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            msg = 0;
                            break;
                        }
                    }
                    else
                    {
                        //Update Jemur ulang jika lokasi adjust p99
                        if (t1AdjustDetail.Lokasi.Trim().ToUpper() == "P99")
                        {
                            int updLari = t1AdjustDetail.QtyOut;
                            T1_JemurLgNFFacade jemurlgF = new T1_JemurLgNFFacade();
                            ArrayList arrjemurlg = new ArrayList();
                            arrjemurlg = jemurlgF.RetrieveforJmrLg(t1AdjustDetail.TglProduksi.ToString("yyyyMMdd"), " A.ID=" + t1AdjustDetail.DestID +
                                " and ItemID0 in (select ID from FC_Items where PartNo='" + t1AdjustDetail.Partno + "') and ", "H.qtyin");
                            foreach (T1_Jemur jemurlg in arrjemurlg)
                            {
                                jemur.LokasiID = lokAdjustID.ToString();
                                jemur.ItemID = t1AdjustDetail.ItemID;
                                jemur.ID = jemurlg.ID;
                                if (jemurlg.QtyIn < updLari)
                                    jemur.QtyOut = jemurlg.QtyIn;
                                else
                                    jemur.QtyOut = updLari;
                                AbstractTransactionFacadeF absTrans = new T1_JemurNFFacade(jemur);
                                intResult = absTrans.Update1(transManager);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    break;
                                }
                                updLari = updLari - jemurlg.QtyIn;
                            }
                        }
                        else
                        {
                            AbstractTransactionFacadeF absTrans = new T1_JemurNFFacade(jemur);
                            //intResult = absTrans.Update(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                msg = 0;
                                break;
                            }
                        }
                        intResult = jemur.ID;
                    }

                    //insert data di tabel T1_serah dgn lokasi Adjust
                    int intResult1 = 0;
                    T1_SerahNF objSerah = new T1_SerahNF();
                    objSerah.ItemIDSer = t1AdjustDetail.ItemID;
                    objSerah.QtyIn = t1AdjustDetail.QtyOut;
                    objSerah.LokasiID = lokAdjustID.ToString();
                    objSerah.DestID = t1AdjustDetail.DestID;
                    objSerah.ItemIDDest = t1AdjustDetail.ItemID;
                    objSerah.HPP = 0;
                    objSerah.JemurID = intResult;
                    objSerah.CreatedBy = users.UserName;
                    objSerah.TglSerah = t1AdjustDetail.DateAdjust;
                    objSerah.SFrom = "Adjust";
                    AbstractTransactionFacadeF absTrans1 = new T1_SerahNFFacade(objSerah);
                    intResult1 = absTrans1.Insert(transManager1);

                    if (absTrans1.Error != string.Empty)
                    {
                        transManager1.RollbackTransaction();
                        msg = 0;
                        break;
                    }
                    T1_SerahNFFacade serahF = new T1_SerahNFFacade();

                    transManager.CommitTransaction();
                    transManager.CloseConnection();
                    transManager1.CommitTransaction();
                    transManager1.CloseConnection();
                    serahF.UpdateAdjustOut(intResult1);
                }
            }
            T1_AdjustNF T1adjust = new T1_AdjustNF();
            T1_AdjustProcessNFFacade AdjustProcessFacade = new T1_AdjustProcessNFFacade(T1adjust, T1AdjustAll);
            string strError = AdjustProcessFacade.Update();
            return msg;
        }
    }
}