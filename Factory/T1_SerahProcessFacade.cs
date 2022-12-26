﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factory
{
    public class T1_SerahProcessFacade
    {
        private T1_Jemur objJemur;
        private T1_Serah objSerah;
        private ArrayList arrSerah;
        
        private string strError = string.Empty;
        public T1_SerahProcessFacade(T1_Jemur Jemur, ArrayList Serah)
        {
            objJemur = Jemur;
            arrSerah = Serah;
        }
        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                
                if (t1serah.SFrom =="lari")
                {
                    T1_Jemur Pelarian=new T1_Jemur();
                    Pelarian.ItemID0 = t1serah.ItemIDSer;
                    Pelarian.LokasiID0 = t1serah.LokasiID;
                    Pelarian.LokasiID = t1serah.LokasiID;
                    Pelarian.ID = objJemur.ID;
                    Pelarian.DestID = objJemur.DestID;
                    Pelarian.ItemID = t1serah.ItemIDSer;
                    Pelarian.RakID =objJemur.RakID ;
                    Pelarian.TglJemur = t1serah.TglSerah;
                    Pelarian.QtyIn = t1serah.QtyIn;
                    Pelarian.HPP = t1serah.HPP;
                    Pelarian.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_JemurLgFacade(Pelarian);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
                absTrans = new T1_SerahFacade(t1serah);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                objJemur.QtyOut = t1serah.QtyIn;
                AbstractTransactionFacadeF absTrans2 = new T1_JemurFacade(objJemur);
                intResult = absTrans2.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string Insert1020()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                if (t1serah.SFrom == "lari")
                {
                    T1_Jemur Pelarian = new T1_Jemur();
                    Pelarian.ItemID0 = t1serah.ItemIDSer;
                    Pelarian.LokasiID0 = t1serah.LokasiID;
                    Pelarian.LokasiID = t1serah.LokasiID;
                    Pelarian.ID = objJemur.ID;
                    Pelarian.DestID = objJemur.DestID;
                    Pelarian.ItemID = t1serah.ItemIDSer;
                    Pelarian.RakID = objJemur.RakID;
                    Pelarian.TglJemur = t1serah.TglSerah;
                    Pelarian.QtyIn = t1serah.QtyIn;
                    Pelarian.HPP = t1serah.HPP;
                    Pelarian.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_JemurLgFacade(Pelarian);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
                absTrans = new T1_SerahFacade(t1serah);
                intResult = absTrans.Insert(transManager);
                TextBox partnoTrm = new TextBox();
                TextBox lokasiTrm = new TextBox();
                TextBox QtyTrm = new TextBox();
                partnoTrm.Text = t1serah.PartnoSer;
                lokasiTrm.Text = t1serah.LokasiSer;
                QtyTrm.Text = t1serah.QtyIn.ToString();
                t1serah.ID = intResult;
                int intterima = TerimaBarang1020(partnoTrm, lokasiTrm, QtyTrm, t1serah);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                objJemur.QtyOut = t1serah.QtyIn;
                AbstractTransactionFacadeF absTrans2 = new T1_JemurFacade(objJemur);
                intResult = absTrans2.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                AbstractTransactionFacadeF abstrans = new T1_SerahFacade(t1serah);
                intResult = abstrans.Update(transManager);
                if (abstrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return abstrans.Error;
                }
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        protected int TerimaBarang1020(TextBox Partno, TextBox lokasi, TextBox qty, T1_Serah serah)
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
            maxtrans = serah.QtyIn - serah.QtyOut;
            checktrans = Convert.ToInt32(txtQtyTrm.Text);
            T3_Rekap rekap = new T3_Rekap();
            if (txtLokTrm.Text != string.Empty && txtQtyTrm.Text != string.Empty && maxtrans >= checktrans && checktrans > 0)
            {
                rekap.DestID = serah.DestID;
                rekap.SerahID = t3serah.ID;
                rekap.Keterangan = serah.PartnoDest;
                rekap.T1serahID = serah.ID;
                rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                if (rekap.LokasiID == 0)
                {
                    return 0;
                }
                FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
                rekap.ItemIDSer = items.ID;
                rekap.T1sItemID = serah.ItemIDSer;
                rekap.TglTrm = serah.TglSerah;
                rekap.QtyInTrm = int.Parse(txtQtyTrm.Text);
                rekap.T1SLokID = serah.LokasiID;
                rekap.QtyOutTrm =0;
                rekap.SA = t3serah.Qty;
                if (int.Parse(txtQtyTrm.Text) > 0 && serah.HPP > 0)
                    AvgHPP = ((t3serah.HPP * t3serah.Qty) + (int.Parse(txtQtyTrm.Text) * serah.HPP)) / (t3serah.Qty + int.Parse(txtQtyTrm.Text));
                else
                    AvgHPP = t3serah.HPP;
                rekap.HPP = AvgHPP;
                rekap.GroupID = items.GroupID;
                rekap.CreatedBy = users.UserName;
                rekap.Process = "Direct";
                rekap.CutQty = int.Parse(txtQtyTrm.Text);
                rekap.CutLevel = 1;
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
                string strError = TerimaProcessFacade.Insert1();
            }
            return serah.ID;
        }

        public string InsertLisplank()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                if (t1serah.SFrom == "lari")
                {
                    T1_Jemur Pelarian = new T1_Jemur();
                    Pelarian.ItemID0 = t1serah.ItemIDSer;
                    Pelarian.LokasiID0 = t1serah.LokasiID;
                    Pelarian.LokasiID = t1serah.LokasiID;
                    Pelarian.ID = objJemur.ID;
                    Pelarian.DestID = objJemur.DestID;
                    Pelarian.ItemID = t1serah.ItemIDSer;
                    Pelarian.RakID = objJemur.RakID;
                    Pelarian.TglJemur = t1serah.TglSerah;
                    Pelarian.QtyIn = t1serah.QtyIn;
                    Pelarian.HPP = t1serah.HPP;
                    Pelarian.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_JemurLgFacade(Pelarian);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
                absTrans = new T1_SerahFacade(t1serah);
                intResult = absTrans.Insert(transManager);

                TextBox partnoTrm = new TextBox();
                TextBox lokasiTrm = new TextBox();
                TextBox QtyTrm = new TextBox();
                partnoTrm.Text = t1serah.PartnoSer;
                lokasiTrm.Text = t1serah.LokasiSer;
                QtyTrm.Text = t1serah.QtyIn.ToString();
                t1serah.ID = intResult;
                if (t1serah.PartnoSer.IndexOf("-1-") == -1 && t1serah.SFrom != "lari" && lokasiTrm.Text.ToUpper()=="I99")
                {
                    int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, t1serah);
                    if (intterima <0)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                objJemur.QtyOut = t1serah.QtyIn;
                AbstractTransactionFacadeF absTrans2 = new T1_JemurFacade(objJemur);
                intResult = absTrans2.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (t1serah.SFrom != "lari" && (lokasiTrm.Text.ToUpper() == "I99") || (lokasiTrm.Text.ToUpper() == "B99"))
                {
                    AbstractTransactionFacadeF abstrans = new T1_SerahFacade(t1serah);
                    t1serah.QtyOut = t1serah.QtyIn;
                    intResult = abstrans.Update(transManager);
                    if (abstrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return abstrans.Error;
                    }
                }
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string InsertLisplank1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                if (t1serah.SFrom != "lari")
                {
                    T1_ListPlank ListPlank = new T1_ListPlank();
                    ListPlank.ItemID0 = t1serah.ItemIDSer;
                    ListPlank.LokasiID0 = t1serah.LokasiID;
                    ListPlank.LokasiID = t1serah.LokasiID;
                    ListPlank.ID = objJemur.ID;
                    ListPlank.DestID = objJemur.DestID;
                    ListPlank.ItemID = t1serah.ItemIDSer;
                    ListPlank.RakID = objJemur.RakID;
                    ListPlank.TglTrans = t1serah.TglSerah;
                    ListPlank.QtyIn = t1serah.QtyIn;
                    ListPlank.QtyOut = t1serah.QtyIn;
                    ListPlank.HPP = t1serah.HPP;
                    ListPlank.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_ListPlankFacade(ListPlank);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string InsertLisplankR1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                if (t1serah.SFrom != "lari")
                {
                    T1_LR1_ListPlank ListPlank = new T1_LR1_ListPlank();
                    ListPlank.ItemID0 = t1serah.ItemIDSer;
                    ListPlank.LokasiID0 = t1serah.LokasiID;
                    ListPlank.LokasiID = t1serah.LokasiID;
                    ListPlank.ID = objJemur.ID;
                    ListPlank.DestID = objJemur.DestID;
                    ListPlank.ItemID = t1serah.ItemIDSer;
                    ListPlank.RakID = objJemur.RakID;
                    ListPlank.TglTrans = t1serah.TglSerah;
                    ListPlank.QtyIn = t1serah.QtyIn;
                    ListPlank.QtyOut = t1serah.QtyIn;
                    ListPlank.HPP = t1serah.HPP;
                    ListPlank.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_LR1_ListPlankFacade(ListPlank);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
                
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string InsertLisplank2()
        {
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            //BM_DestackingFacade destF = new BM_DestackingFacade();
            //T1_JemurFacade t1jemurF = new T1_JemurFacade();
            //foreach (T1_Serah t1serah in arrSerah)
            //{
            //    intResult = 0;
            //    T1_ListPlank2 ListPlank2 = new T1_ListPlank2();
            //    ListPlank2.ItemID0 = t1serah.ItemIDSer;
            //    ListPlank2.LokasiID0 = t1serah.LokasiID;
            //    ListPlank2.LokasiID = t1serah.LokasiID;
            //    ListPlank2.ID = objJemur.ID;
            //    ListPlank2.DestID = objJemur.DestID;
            //    ListPlank2.ItemID = t1serah.ItemIDSer;
            //    ListPlank2.RakID = objJemur.RakID;
            //    ListPlank2.TglTrans = t1serah.TglSerah;
            //    ListPlank2.QtyIn = t1serah.QtyIn;
            //    ListPlank2.HPP = t1serah.HPP;
            //    ListPlank2.CreatedBy = t1serah.CreatedBy;
            //    AbstractTransactionFacadeF absTrans1 = new T1_ListPlankFacade(ListPlank2);
            //    intResult = absTrans1.Insert1(transManager);
            //    if (absTrans1.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return absTrans.Error;
            //    }
            //    t1serah.JemurID = intResult;

            //    absTrans = new T1_SerahFacade(t1serah);
            //    intResult = absTrans.Insert(transManager);
            //    TextBox partnoTrm = new TextBox();
            //    TextBox lokasiTrm = new TextBox();
            //    TextBox QtyTrm = new TextBox();
            //    partnoTrm.Text = t1serah.PartnoSer;
            //    lokasiTrm.Text = t1serah.LokasiSer;
            //    QtyTrm.Text = t1serah.QtyIn.ToString();
            //    t1serah.ID = intResult;
            //    if (t1serah.PartnoSer.IndexOf("-1-") == -1 && intResult > 0 && lokasiTrm.Text.Trim().ToUpper()!="H99")
            //    {
            //        int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, t1serah);
            //        if (absTrans.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return absTrans.Error;
            //        }
            //    }
            //    objJemur.QtyOut = t1serah.QtyIn;
            //    AbstractTransactionFacadeF absTrans2 = new T1_JemurFacade(objJemur);
            //    intResult = absTrans2.Update(transManager);
            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return absTrans.Error;
            //    }
            //    AbstractTransactionFacadeF abstrans = new T1_SerahFacade(t1serah);
            //    intResult = abstrans.Update(transManager);
            //    if (abstrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return abstrans.Error;
            //    }
            //}
            //intResult = destF.UpdateStatus(objJemur.DestID, 2);
            //transManager.CommitTransaction();
            //transManager.CloseConnection();
            return string.Empty;
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
            maxtrans = serah.QtyIn - serah.QtyOut;
            checktrans = Convert.ToInt32(txtQtyTrm.Text);
            T3_Rekap rekap = new T3_Rekap();
            if (txtLokTrm.Text != string.Empty && txtQtyTrm.Text != string.Empty && maxtrans >= checktrans && checktrans > 0)
            {
                rekap.DestID = serah.DestID;
                rekap.SerahID = t3serah.ID;
                rekap.Keterangan = serah.PartnoDest;
                rekap.T1serahID = serah.ID;
                rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                if (rekap.LokasiID == 0)
                {
                    return 0;
                }

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
                string strError = TerimaProcessFacade.Insert1();
                if (strError != string.Empty)
                    intResult= - 1;
                else
                    intResult= 0;
            }
            return intResult;
        }
        public string Insert4mili()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_SerahFacade(objSerah);
            BM_DestackingFacade destF = new BM_DestackingFacade();
            T1_JemurFacade t1jemurF = new T1_JemurFacade();
            int t1serahbsauto = 0;
            foreach (T1_Serah t1serah in arrSerah)
            {
                intResult = 0;
                if (t1serah.SFrom == "lari")
                {
                    T1_Jemur Pelarian = new T1_Jemur();
                    Pelarian.ItemID0 = t1serah.ItemIDSer;
                    Pelarian.LokasiID0 = t1serah.LokasiID;
                    Pelarian.LokasiID = t1serah.LokasiID;
                    Pelarian.ID = objJemur.ID;
                    Pelarian.DestID = objJemur.DestID;
                    Pelarian.ItemID = t1serah.ItemIDSer;
                    Pelarian.RakID = objJemur.RakID;
                    Pelarian.TglJemur = t1serah.TglSerah;
                    Pelarian.QtyIn = t1serah.QtyIn;
                    Pelarian.HPP = t1serah.HPP;
                    Pelarian.CreatedBy = t1serah.CreatedBy;
                    AbstractTransactionFacadeF absTrans1 = new T1_JemurLgFacade(Pelarian);
                    intResult = absTrans1.Insert1(transManager);
                    if (absTrans1.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    t1serah.JemurID = intResult;
                }
               
                if (t1serah.LokasiSer.Trim().ToUpper() != "BSAUTO")
                {
                    absTrans = new T1_SerahFacade(t1serah);
                    intResult = absTrans.Insert(transManager);
                    t1serahbsauto = intResult;
                }
                TextBox partnoTrm = new TextBox();
                TextBox lokasiTrm = new TextBox();
                TextBox QtyTrm = new TextBox();
                partnoTrm.Text = t1serah.PartnoSer;
                lokasiTrm.Text = t1serah.LokasiSer;
                QtyTrm.Text = t1serah.QtyIn.ToString();
                t1serah.ID = intResult;
                
                if (t1serah.LokasiSer.Trim().ToUpper() == "BSAUTO")
                    t1serah.ID = t1serahbsauto;
                if (lokasiTrm.Text.Trim().ToUpper() != "H99" && lokasiTrm.Text.Trim().ToUpper() != "I99" && lokasiTrm.Text.Trim().ToUpper() != "Q99")
                {
                    int intterima = TerimaBarang4mili(partnoTrm, lokasiTrm, QtyTrm, t1serah);
                    if (intterima < 0)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    AbstractTransactionFacadeF abstrans = new T1_SerahFacade(t1serah);
                    if (t1serah.LokasiSer.Trim().ToUpper() != "BSAUTO")
                        intResult = abstrans.Update(transManager);
                    if (abstrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return abstrans.Error;
                    }
                }
                objJemur.QtyOut = t1serah.QtyIn;
                AbstractTransactionFacadeF absTrans2 = new T1_JemurFacade(objJemur);
                if (t1serah.LokasiSer.Trim().ToUpper() != "BSAUTO")
                    intResult = absTrans2.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                
            }
            intResult = destF.UpdateStatus(objJemur.DestID, 2);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        protected int TerimaBarang4mili(TextBox Partno, TextBox lokasi, TextBox qty, T1_Serah serah)
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
            maxtrans = serah.QtyIn - serah.QtyOut;
            checktrans = Convert.ToInt32(txtQtyTrm.Text);
            T3_Rekap rekap = new T3_Rekap();
            if (txtLokTrm.Text != string.Empty && txtQtyTrm.Text != string.Empty && maxtrans >= checktrans && checktrans > 0)
            {
                rekap.DestID = serah.DestID;
                rekap.SerahID = t3serah.ID;
                rekap.Keterangan = serah.PartnoDest;
                rekap.T1serahID = serah.ID;
                rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                if (rekap.LokasiID == 0)
                {
                    return 0;
                }
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
                string strError = TerimaProcessFacade.Insert1();
                if (strError != string.Empty)
                    intResult= - 1;
                else
                    intResult= 0;
            }
            return intResult;
        }
    }
}
