using System;
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessFacade
{
    public class SuratJalanProcessFacade
    {
        private SuratJalan objSuratJalan;
        private ArrayList arrSuratJalanDetail;
        private string strError = string.Empty;
        private int intSuratJalanID = 0;
        //private DocumentID objDocumentID;
        private SJNumber objSJNumber;


        public SuratJalanProcessFacade(SuratJalan suratJalan, ArrayList arrListSuratJalan,SJNumber sjNumber)
        {
            objSuratJalan = suratJalan;
            arrSuratJalanDetail = arrListSuratJalan;
            objSJNumber = sjNumber;
        }

        public string SuratJalanNo
        {
            get
            {
                if (objSuratJalan.TypeOP == 1)
                {
                    return objSuratJalan.TypeOP.ToString() + objSuratJalan.TglKirimActual.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter1.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/" + Global.ConvertNumericToRomawi(objSuratJalan.TglKirimActual.Month) + "/" + objSuratJalan.TglKirimActual.Year.ToString();
                    //return objSuratJalan.TypeOP.ToString() + DateTime.Now.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter1.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                    // return objSuratJalan.TypeOP.ToString() + objDocumentID.SJID1.ToString().PadLeft(9, '0') + "/SJ/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                }
                else
                {
                    return objSuratJalan.TypeOP.ToString() + objSuratJalan.TglKirimActual.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter2.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/CD/" + Global.ConvertNumericToRomawi(objSuratJalan.TglKirimActual.Month) + "/" + objSuratJalan.TglKirimActual.Year.ToString();
                    //return objSuratJalan.TypeOP.ToString() + DateTime.Now.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter2.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/CD/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                    //return objSuratJalan.TypeOP.ToString() + objDocumentID.SJID2.ToString().PadLeft(9, '0') + "/SJ/CD/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                }
            }
        }

        public string CancelInvoice()
        {
            int intResult = 0;
            objSuratJalan.Status = 2;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;

        }

        public string BatalCancelInvoice()
        {
            int intResult = 0;
            objSuratJalan.Status = 3;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;

        }
        public string InsertAPI()
        {
            int intResult = 0;
            

            return string.Empty;
        }
        public string Insert()
        {
            int intResult = 0;

            DocumentIDFacade documentIDFacade = new DocumentIDFacade();
            //if (objDocumentID.ID == 0)
            //    intResult = documentIDFacade.Insert(objDocumentID);
            //else
            //    intResult = documentIDFacade.Update(objDocumentID);

            SJNumberFacade sjNumberFacade = new SJNumberFacade();
            intResult = sjNumberFacade.Update(objSJNumber);


            if (intResult > 0)
            {
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();

                AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
                intSuratJalanID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intSuratJalanID > 0)
                {
                    if (objSuratJalan.TypeOP == 1)
                        objSuratJalan.SuratJalanNo = objSuratJalan.TypeOP.ToString() + objSuratJalan.TglKirimActual.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter1.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/" + Global.ConvertNumericToRomawi(objSuratJalan.TglKirimActual.Month) + "/" + objSuratJalan.TglKirimActual.Year.ToString();
                    else
                        objSuratJalan.SuratJalanNo = objSuratJalan.TypeOP.ToString() + objSuratJalan.TglKirimActual.Year.ToString().Substring(3, 1) + objSJNumber.SJCounter2.ToString().PadLeft(8, '0') + SettingDepoSJ(objSJNumber.DepoID) + "/SJ/CD/" + Global.ConvertNumericToRomawi(objSuratJalan.TglKirimActual.Month) + "/" + objSuratJalan.TglKirimActual.Year.ToString();

                    objSuratJalan.ID = intSuratJalanID;

                    absTrans = new SuratJalanFacade(objSuratJalan);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }

                    if (arrSuratJalanDetail.Count > 0)
                    {
                        absTrans = new SuratJalanDetailFacade((SuratJalanDetail)arrSuratJalanDetail[0]);
                        intResult = absTrans.Delete(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                        foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                        {                           
                            suratJalanDetail.SuratJalanId = intSuratJalanID;
                            absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                            intResult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                    }
                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string UpdateTglKirimAktual()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = ((SuratJalanFacade)absTrans).UpdateTglKirimAktual(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            if (intResult > 0)
            {
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }

            else
            {
                transManager.RollbackTransaction();
                return "Error";
            }

            return string.Empty;
        }

        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                if (arrSuratJalanDetail.Count > 0)
                {
                    absTrans = new SuratJalanDetailFacade((SuratJalanDetail)arrSuratJalanDetail[0], objSuratJalan.SuratJalanNo);
                    intResult = absTrans.Delete(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }

                    foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                    {
                        suratJalanDetail.SuratJalanId = objSuratJalan.ID;
                        absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        public string Delete()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = absTrans.Delete(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelReceive()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            intResult = ((SuratJalanFacade)absTrans).CancelPostingReceive(transManager);

            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }


            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string UpdateStatusSJ()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            intResult = ((SuratJalanFacade)absTrans).UpdateStatusSJ(transManager);

            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }


            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        //public string PostingReceive()
        //{
        //    int intResult = 0;
        //    objSuratJalan.Status = 2;
        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();
        //    AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
        //    intResult = absTrans.Update(transManager);
        //    if (absTrans.Error != string.Empty)
        //    {
        //        transManager.RollbackTransaction();
        //        return absTrans.Error;
        //    }

        //    if (intResult > 0)
        //    {                 
        //         intResult = ((SuratJalanFacade)absTrans).UpdatePostingDate(transManager, 2);
        //         if (intResult > 0)
        //         {
        //             foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
        //             {
        //                 suratJalanDetail.Flag = 2;
        //                 absTrans = new SuratJalanDetailFacade(suratJalanDetail);
        //                 intResult = absTrans.Update(transManager);
        //                 if (absTrans.Error != string.Empty)
        //                 {
        //                     transManager.RollbackTransaction();
        //                     return absTrans.Error;
        //                 }

        //                 ItemsFacade itemsFacade = new ItemsFacade();
        //                 Items items = itemsFacade.RetrieveById(suratJalanDetail.ItemID);
        //                 if (itemsFacade.Error != string.Empty)
        //                     return itemsFacade.Error;


        //                 itemsFacade = new ItemsFacade();
        //                 Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCode);
        //                 if (itemsFacade.Error != string.Empty)
        //                     return itemsFacade.Error;

        //                 InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

        //                 InventoryStock inventoryStock = new InventoryStock();
        //                 inventoryStock.Status = 3;
        //                 if (itemsHead.ID > 0)
        //                     inventoryStock.ItemID = itemsHead.ID;
        //                 else
        //                     inventoryStock.ItemID = suratJalanDetail.ItemID;

        //                 inventoryStock.Quantity = suratJalanDetail.Qty;
        //                 inventoryStock.DepoID = objSuratJalan.DepoID;

        //                 inventoryStockFacade = new InventoryStockFacade();
        //                 intResult = inventoryStockFacade.Update(inventoryStock);
        //                 if (inventoryStockFacade.Error != string.Empty)
        //                 {
        //                     transManager.RollbackTransaction();
        //                     return absTrans.Error;
        //                 }
                       
        //             }

        //             transManager.CommitTransaction();
        //             transManager.CloseConnection();

        //             OPFacade opFacade = new OPFacade();
        //             OP op = opFacade.RetrieveByID(objSuratJalan.OPID);
        //             if (opFacade.Error == string.Empty)
        //             {
        //                 if (op.ID > 0)
        //                 {
        //                     if (op.CustomerType == 1)
        //                     {
        //                         TokoFacade tokoFacade = new TokoFacade();
        //                         Toko toko = tokoFacade.RetrieveById(op.CustomerID);
        //                         if (tokoFacade.Error == string.Empty)
        //                         {
        //                             if (toko.ID > 0)
        //                             {
        //                                 OPDetailFacade oPDetailFacade = new OPDetailFacade();
        //                                 ArrayList Arr = oPDetailFacade.RetrieveById(op.ID);
        //                                 if (oPDetailFacade.Error == string.Empty)
        //                                 {
        //                                     int TotPoint = 0;
        //                                     foreach (OPDetail opdetail in Arr)
        //                                     {
        //                                         TotPoint = TotPoint + (int)opdetail.Point;
        //                                     }
                                           
        //                                     tokoFacade = new TokoFacade();

        //                                     intResult = tokoFacade.UpdateTokoPoint(toko.ID,TotPoint);

        //                                     if (tokoFacade.Error != string.Empty)
        //                                     {
        //                                         transManager.RollbackTransaction();
        //                                         return tokoFacade.Error;
        //                                     }
        //                                 }
        //                             }
        //                         }
        //                     }
        //                 }
        //             }
        //             else
        //             {
        //                 return opFacade.Error;
        //             }

        //         }
        //         else
        //         {
        //             transManager.RollbackTransaction();
        //             return "Error";
        //         }                              
        //    }
        //    else
        //    {
        //        transManager.RollbackTransaction();
        //        return "Error";
        //    }

        //    //transManager = new TransactionManager(Global.ConnectionString());
        //    //transManager.BeginTransaction();

        //    //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
        //    //int jumSJ = suratJalanFacade.RetrieveOutStandingOPById(objSuratJalan.OPID);

        //    //OPFacade opFacade = new OPFacade();
        //    //OP op = opFacade.RetrieveByID(objSuratJalan.OPID);


        //    //if (jumSJ > 0)
        //    //    op.Status = 8;
        //    //else
        //    //    op.Status = 9;

        //    //opFacade = new OPFacade(op);
        //    //intResult = opFacade.Update(transManager);

        //    //if (opFacade.Error == string.Empty)
        //    //{
        //    //    transManager.CommitTransaction();
        //    //    transManager.CloseConnection();
        //    //}


        //    return string.Empty; 
        //}



        public string PostingReceive()
        {
            int intResult = 0;
            objSuratJalan.Status = 2;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);

                intResult = ((SuratJalanFacade)absTrans).UpdatePostingStatus(transManager, 2);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }


                if (intResult > 0)
                {
                    foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                    {
                        suratJalanDetail.Flag = 2;
                        absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                        intResult = absTrans.Update(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                        ItemsFacade itemsFacade = new ItemsFacade();
                        Items items = itemsFacade.RetrieveById(suratJalanDetail.ItemID);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;


                        itemsFacade = new ItemsFacade();
                        Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCategory);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;

                        if (items.Paket == 0)
                        {
                            if (suratJalanDetail.Paket == 0)
                            {
                                InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

                                InventoryStock inventoryStock = new InventoryStock();
                                inventoryStock.Status = 3;
                                if (itemsHead.ID > 0)
                                    inventoryStock.ItemID = itemsHead.ID;
                                else
                                    inventoryStock.ItemID = suratJalanDetail.ItemID;

                                inventoryStock.Quantity = suratJalanDetail.Qty;
                                inventoryStock.DepoID = objSuratJalan.DepoID;
                                inventoryStock.TypeKondisi = 1;

                                inventoryStockFacade = new InventoryStockFacade();
                                intResult = inventoryStockFacade.Update(inventoryStock);
                                if (inventoryStockFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                            }
                        }
                        else
                        {
                            int paketQty = 0;

                            PaketItemDetailFacade paketItemDetailFacade = new PaketItemDetailFacade();
                            ArrayList arrPaketItem = paketItemDetailFacade.RetrieveById(items.ID);

                            if (paketItemDetailFacade.Error == string.Empty)
                            {
                                foreach (PaketItemDetail paketItemDetail in arrPaketItem)
                                {
                                    if (paketQty == 0)
                                        paketQty = suratJalanDetail.Qty / paketItemDetail.Quantity;

                                    itemsFacade = new ItemsFacade();

                                    Items items1 = itemsFacade.RetrieveById(paketItemDetail.ItemID);

                                    InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();
                                    InventoryStock inventoryStock = new InventoryStock();

                                    itemsFacade = new ItemsFacade();
                                    Items itemsHead1 = itemsFacade.RetrieveByGroupCode(items1.GroupCategory);
                                    if (itemsFacade.Error != string.Empty)
                                        return itemsFacade.Error;

                                    if (itemsHead1.ID > 0)
                                        inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(itemsHead1.ID, objSuratJalan.DepoID);
                                    else
                                        inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(paketItemDetail.ItemID, objSuratJalan.DepoID);

                                    if (inventoryStock.ID > 0)
                                    {
                                        inventoryStock.Status = 3;
                                        if (itemsHead.ID > 0)
                                            inventoryStock.ItemID = itemsHead.ID;
                                        else
                                            inventoryStock.ItemID = paketItemDetail.ItemID;

                                        inventoryStock.Quantity = (paketItemDetail.Quantity * paketQty);
                                        inventoryStock.DepoID = objSuratJalan.DepoID;
                                        inventoryStock.TypeKondisi = 1;

                                        inventoryStockFacade = new InventoryStockFacade();
                                        intResult = inventoryStockFacade.Update(inventoryStock);

                                        if (inventoryStockFacade.Error != string.Empty)
                                        {
                                            return inventoryStockFacade.Error;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    transManager.CommitTransaction();
                    transManager.CloseConnection();

                    //OPFacade opFacade = new OPFacade();
                    //OP op = opFacade.RetrieveByID(objSuratJalan.OPID);
                    //if (opFacade.Error == string.Empty)
                    //{
                    //    if (op.ID > 0)
                    //    {
                    //        if (op.CustomerType == 1)
                    //        {
                    //            TokoFacade tokoFacade = new TokoFacade();
                    //            Toko toko = tokoFacade.RetrieveById(op.CustomerID);
                    //            if (tokoFacade.Error == string.Empty)
                    //            {
                    //                if (toko.ID > 0)
                    //                {
                    //                    OPDetailFacade oPDetailFacade = new OPDetailFacade();
                    //                    ArrayList Arr = oPDetailFacade.RetrieveById(op.ID);
                    //                    if (oPDetailFacade.Error == string.Empty)
                    //                    {
                    //                        int TotPoint = 0;
                    //                        foreach (OPDetail opdetail in Arr)
                    //                        {
                    //                            TotPoint = TotPoint + (int)opdetail.Point;
                    //                        }

                    //                        tokoFacade = new TokoFacade();

                    //                        intResult = tokoFacade.UpdateTokoPoint(toko.ID, TotPoint,1);

                    //                        if (tokoFacade.Error != string.Empty)
                    //                        {
                    //                            transManager.RollbackTransaction();
                    //                            return tokoFacade.Error;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    return opFacade.Error;
                    //}

                }
                else
                {
                    transManager.RollbackTransaction();
                    return "Error";
                }
            //}
            //else
            //{
            //    transManager.RollbackTransaction();
            //    return "Error";
            //}

            return string.Empty;
        }

        public string CancelSuratJalan()
        {
            int intResult = 0;
            objSuratJalan.Status = -1;
            //objSuratJalan.TglKirimActual = objSuratJalan.TglKirimActual;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            intResult = ((SuratJalanFacade)absTrans).CancelSuratJalan(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            //iko add, utk depo bisa kurangi stok
            if (intResult > 0 && objSuratJalan.DepoID != 1 && objSuratJalan.DepoID != 7)
            //if (intResult > 0)
            {
                foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                {
                    suratJalanDetail.Flag = 3;
                    absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }            



                OPFacade opFacade = new OPFacade();
                OP op = opFacade.RetrieveByID(objSuratJalan.OPID);
                if (opFacade.Error == string.Empty)
                {
                    if (op.ID > 0)
                    {
                        objSuratJalan.DepoID = op.DepoID;

                        foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                        {
                            ItemsFacade itemsFacade = new ItemsFacade();
                            Items items = itemsFacade.RetrieveById(suratJalanDetail.ItemID);
                            if (itemsFacade.Error != string.Empty)
                                return itemsFacade.Error;


                            itemsFacade = new ItemsFacade();
                            Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCategory);
                            if (itemsFacade.Error != string.Empty)
                                return itemsFacade.Error;

                            if (items.Paket == 0)
                            {
                                if (suratJalanDetail.Paket == 0)
                                {

                                    suratJalanDetail.Flag = 3;
                                    absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                                    intResult = absTrans.Update(transManager);

                                    InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

                                    InventoryStock inventoryStock = new InventoryStock();
                                    //inventoryStock.Status = 5;
                                    inventoryStock.Status = 4;
                                    if (itemsHead.ID > 0)
                                        inventoryStock.ItemID = itemsHead.ID;
                                    else
                                        inventoryStock.ItemID = suratJalanDetail.ItemID;
                                    inventoryStock.Quantity = suratJalanDetail.Qty;
                                    inventoryStock.DepoID = objSuratJalan.DepoID;
                                    inventoryStock.TypeKondisi = 0;

                                    inventoryStockFacade = new InventoryStockFacade();
                                    intResult = inventoryStockFacade.Update(inventoryStock);
                                    if (inventoryStockFacade.Error != string.Empty)
                                    {
                                        transManager.RollbackTransaction();
                                        return absTrans.Error;
                                    }

                                    if (absTrans.Error != string.Empty)
                                    {
                                        transManager.RollbackTransaction();
                                        return absTrans.Error;
                                    }
                                }
                            }
                            else
                            {
                                int paketQty = 0;

                                suratJalanDetail.Flag = 3;
                                absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                                intResult = absTrans.Update(transManager);

                                PaketItemDetailFacade paketItemDetailFacade = new PaketItemDetailFacade();
                                ArrayList arrPaketItem = paketItemDetailFacade.RetrieveById(items.ID);

                                if (paketItemDetailFacade.Error == string.Empty)
                                {
                                    foreach (PaketItemDetail paketItemDetail in arrPaketItem)
                                    {
                                        if (paketQty == 0)
                                            paketQty = suratJalanDetail.Qty / paketItemDetail.Quantity;

                                        itemsFacade = new ItemsFacade();

                                        Items items1 = itemsFacade.RetrieveById(paketItemDetail.ItemID);

                                        //inventoryStockFacade = new InventoryStockFacade();

                                        itemsFacade = new ItemsFacade();
                                        itemsHead = new Items();
                                        itemsHead = itemsFacade.RetrieveByGroupCode(items1.GroupCategory);
                                        if (itemsFacade.Error != string.Empty)
                                            return itemsFacade.Error;

                                        //InventoryStock inventoryStock = new InventoryStock();
                                        //inventoryStockFacade = new InventoryStockFacade();
                                        //if (itemsHead.ID > 0)
                                        //    inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(itemsHead.ID, objOP.DepoID);
                                        //else
                                        //    inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(paketItemDetail.ItemID, objOP.DepoID);

                                        //if (inventoryStock.ID > 0)
                                        //{
                                        //    inventoryStock.Status = 1;
                                        //    if (itemsHead.ID > 0)
                                        //        inventoryStock.ItemID = itemsHead.ID;
                                        //    else
                                        //        inventoryStock.ItemID = paketItemDetail.ItemID;

                                        //    inventoryStock.Quantity = (paketItemDetail.Quantity * opDetail.Quantity);
                                        //    inventoryStock.DepoID = objOP.DepoID;
                                        //    inventoryStock.TypeKondisi = 1;

                                        //    inventoryStockFacade = new InventoryStockFacade();
                                        //    intResult = inventoryStockFacade.Update(inventoryStock);

                                        //    if (inventoryStockFacade.Error != string.Empty)
                                        //    {
                                        //        return inventoryStockFacade.Error;
                                        //    }
                                        //}

                                        InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

                                        InventoryStock inventoryStock = new InventoryStock();
                                        //inventoryStock.Status = 5;
                                        inventoryStock.Status = 4;
                                        if (itemsHead.ID > 0)
                                            inventoryStock.ItemID = itemsHead.ID;
                                        else
                                            inventoryStock.ItemID = paketItemDetail.ItemID;
                                        inventoryStock.Quantity = (paketItemDetail.Quantity * paketQty);
                                        inventoryStock.DepoID = objSuratJalan.DepoID;
                                        inventoryStock.TypeKondisi = 0;

                                        inventoryStockFacade = new InventoryStockFacade();
                                        intResult = inventoryStockFacade.Update(inventoryStock);
                                        if (inventoryStockFacade.Error != string.Empty)
                                        {
                                            transManager.RollbackTransaction();
                                            return absTrans.Error;
                                        }

                                        if (absTrans.Error != string.Empty)
                                        {
                                            transManager.RollbackTransaction();
                                            return absTrans.Error;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    transManager.RollbackTransaction();
                    return opFacade.Error;
                }
            }

            else
            {
                foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                {
                    suratJalanDetail.Flag = 3;
                    absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }            
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateCetakSuratJalan()
        {
            int intResult = 0;
            objSuratJalan.Cetak = 1;
            objSuratJalan.CountPrint = objSuratJalan.CountPrint + 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            SuratJalanFacade suratJalanFacade = new SuratJalanFacade(objSuratJalan);
            intResult = suratJalanFacade.UpdateStatusCetakSuratJalan(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            if (intResult > 0)
            {
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            else
                transManager.RollbackTransaction();

            return string.Empty;
        }
        public string UpdateCetakKwitansi()
        {
            int intResult = 0;
            objSuratJalan.Cetak = 1;
            objSuratJalan.CountPrint = objSuratJalan.CountPrint + 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            SuratJalanFacade suratJalanFacade = new SuratJalanFacade(objSuratJalan);
            intResult = suratJalanFacade.UpdateStatusCetakKwitansi(transManager);

            if (intResult > 0)
            {
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            else
                transManager.RollbackTransaction();

            return string.Empty;
        }

        public string Reprint()
        {
            int intResult = 0;
            objSuratJalan.Cetak = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        //public string PostingShipment()
        //{
        //    int intResult = 0;
        //    objSuratJalan.Status = 1;
        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();
        //    AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
        //    intResult = absTrans.Update(transManager);
        //    if (absTrans.Error != string.Empty)
        //    {
        //        transManager.RollbackTransaction();
        //        return absTrans.Error;
        //    }

        //    if (intResult > 0)
        //    {
        //        intResult = ((SuratJalanFacade)absTrans).UpdatePostingDate(transManager, 1);
        //        if (intResult > 0)
        //        {
        //            OPFacade opFacade = new OPFacade();
        //            OP op = opFacade.RetrieveByID(objSuratJalan.OPID);
        //            if (opFacade.Error == string.Empty)
        //            {
        //                if (op.ID > 0)
        //                {
        //                    objSuratJalan.DepoID = op.DepoID;
        //                    foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
        //                    {
        //                        suratJalanDetail.Flag = 1;
        //                        absTrans = new SuratJalanDetailFacade(suratJalanDetail);
        //                        intResult = absTrans.Update(transManager);
        //                        if (absTrans.Error != string.Empty)
        //                        {
        //                            transManager.RollbackTransaction();
        //                            return absTrans.Error;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                transManager.RollbackTransaction();
        //                return opFacade.Error;
        //            }



        //            StockMovementFacade stockMovementFacade = new StockMovementFacade();
        //            foreach (SuratJalanDetail sjd in arrSuratJalanDetail)
        //            {
        //                ItemsFacade itemsFacade = new ItemsFacade();
        //                Items items = itemsFacade.RetrieveById(sjd.ItemID);
        //                if (itemsFacade.Error != string.Empty)
        //                    return itemsFacade.Error;


        //                itemsFacade = new ItemsFacade();
        //                Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCode);
        //                if (itemsFacade.Error != string.Empty)
        //                    return itemsFacade.Error;

        //                StockMovement stockMovement = new StockMovement();
        //                stockMovementFacade = new StockMovementFacade();
        //                stockMovement.TypeDoc = 1;
        //                stockMovement.NoDoc = objSuratJalan.SuratJalanNo;
        //                stockMovement.TglDoc = objSuratJalan.CreatedTime;
        //                if (itemsHead.ID > 0)
        //                    stockMovement.ItemID = itemsHead.ID;
        //                else
        //                    stockMovement.ItemID = sjd.ItemID;
        //                stockMovement.DepoID = objSuratJalan.DepoID;
        //                stockMovement.Quantity = sjd.Qty;
        //                stockMovement.Status = 1;
        //                stockMovement.CreatedBy = objSuratJalan.CreatedBy;

        //                stockMovementFacade = new StockMovementFacade();
        //                intResult = stockMovementFacade.Insert(stockMovement);
        //                if (stockMovementFacade.Error != string.Empty)
        //                {
        //                    transManager.RollbackTransaction();
        //                    return stockMovementFacade.Error;
        //                }
        //            }

        //            transManager.CommitTransaction();
        //            transManager.CloseConnection();

        //        }
        //        else
        //        {
        //            transManager.RollbackTransaction();
        //            return "Error";
        //        }
        //    }
        //    else
        //    {
        //        transManager.RollbackTransaction();
        //        return "Error";
        //    }

        //    //transManager = new TransactionManager(Global.ConnectionString());
        //    //transManager.BeginTransaction();

        //    //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
        //    //int jumSJ = suratJalanFacade.RetrieveOutStandingOPById(objSuratJalan.OPID);

        //    //OPFacade opFacade = new OPFacade();
        //    //OP op = opFacade.RetrieveByID(objSuratJalan.OPID);


        //    //if (jumSJ > 0)
        //    //    op.Status = 8;
        //    //else
        //    //    op.Status = 9;

        //    //opFacade = new OPFacade(op);
        //    //intResult = opFacade.Update(transManager);

        //    //if (opFacade.Error == string.Empty)
        //    //{
        //    //    transManager.CommitTransaction();
        //    //    transManager.CloseConnection();
        //    //}


        //    return string.Empty;
        //}

        public string PostingShipment()
        {
            int intResult = 0;
            objSuratJalan.Status = 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //if (intResult > 0)
            //{
                intResult = ((SuratJalanFacade)absTrans).UpdatePostingDate(transManager, 1);
                if (intResult > 0)
                {
                    OPFacade opFacade = new OPFacade();
                    OP op = opFacade.RetrieveByID(objSuratJalan.OPID);
                    if (opFacade.Error == string.Empty)
                    {
                        if (op.ID > 0)
                        {
                            objSuratJalan.DepoID = op.DepoID;
                            foreach (SuratJalanDetail suratJalanDetail in arrSuratJalanDetail)
                            {
                                suratJalanDetail.Flag = 1;
                                absTrans = new SuratJalanDetailFacade(suratJalanDetail);
                                intResult = absTrans.Update(transManager);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                            }
                        }
                    }
                    else
                    {
                        transManager.RollbackTransaction();
                        return opFacade.Error;
                    }



                    StockMovementFacade stockMovementFacade = new StockMovementFacade();
                    foreach (SuratJalanDetail sjd in arrSuratJalanDetail)
                    {
                        ItemsFacade itemsFacade = new ItemsFacade();
                        Items items = itemsFacade.RetrieveById(sjd.ItemID);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;


                        itemsFacade = new ItemsFacade();
                        Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCategory);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;

                        if (items.Paket == 0)
                        {
                            if (sjd.Paket == 0)
                            {
                                StockMovement stockMovement = new StockMovement();
                                stockMovementFacade = new StockMovementFacade();
                                stockMovement.TypeDoc = 1;
                                stockMovement.NoDoc = objSuratJalan.SuratJalanNo;
                                stockMovement.TglDoc = objSuratJalan.CreatedTime;
                                if (itemsHead.ID > 0)
                                    stockMovement.ItemID = itemsHead.ID;
                                else
                                    stockMovement.ItemID = sjd.ItemID;
                                stockMovement.DepoID = objSuratJalan.DepoID;
                                stockMovement.Quantity = sjd.Qty;
                                stockMovement.Status = 1;
                                stockMovement.CreatedBy = objSuratJalan.CreatedBy;

                                stockMovementFacade = new StockMovementFacade();
                                intResult = stockMovementFacade.Insert(stockMovement);
                                if (stockMovementFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return stockMovementFacade.Error;
                                }
                            }
                        }
                        else
                        {
                            int paketQty = 0;

                            PaketItemDetailFacade paketItemDetailFacade = new PaketItemDetailFacade();
                            ArrayList arrPaketItem = paketItemDetailFacade.RetrieveById(items.ID);

                            if (paketItemDetailFacade.Error == string.Empty)
                            {
                                foreach (PaketItemDetail paketItemDetail in arrPaketItem)
                                {
                                    if (paketQty == 0)
                                        paketQty = sjd.Qty / paketItemDetail.Quantity;

                                    itemsFacade = new ItemsFacade();

                                    Items items1 = itemsFacade.RetrieveById(paketItemDetail.ItemID);

                                    //inventoryStockFacade = new InventoryStockFacade();

                                    itemsFacade = new ItemsFacade();
                                    itemsHead = new Items();
                                    itemsHead = itemsFacade.RetrieveByGroupCode(items1.GroupCategory);
                                    if (itemsFacade.Error != string.Empty)
                                        return itemsFacade.Error;

                                    //InventoryStock inventoryStock = new InventoryStock();
                                    //inventoryStockFacade = new InventoryStockFacade();
                                    //if (itemsHead.ID > 0)
                                    //    inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(itemsHead.ID, objOP.DepoID);
                                    //else
                                    //    inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepo(paketItemDetail.ItemID, objOP.DepoID);

                                    //if (inventoryStock.ID > 0)
                                    //{
                                    //    inventoryStock.Status = 1;
                                    //    if (itemsHead.ID > 0)
                                    //        inventoryStock.ItemID = itemsHead.ID;
                                    //    else
                                    //        inventoryStock.ItemID = paketItemDetail.ItemID;

                                    //    inventoryStock.Quantity = (paketItemDetail.Quantity * opDetail.Quantity);
                                    //    inventoryStock.DepoID = objOP.DepoID;
                                    //    inventoryStock.TypeKondisi = 1;

                                    //    inventoryStockFacade = new InventoryStockFacade();
                                    //    intResult = inventoryStockFacade.Update(inventoryStock);

                                    //    if (inventoryStockFacade.Error != string.Empty)
                                    //    {
                                    //        return inventoryStockFacade.Error;
                                    //    }
                                    //}

                                    StockMovement stockMovement = new StockMovement();
                                    stockMovementFacade = new StockMovementFacade();
                                    stockMovement.TypeDoc = 1;
                                    stockMovement.NoDoc = objSuratJalan.SuratJalanNo;
                                    stockMovement.TglDoc = objSuratJalan.CreatedTime;
                                    if (itemsHead.ID > 0)
                                        stockMovement.ItemID = itemsHead.ID;
                                    else
                                        stockMovement.ItemID = paketItemDetail.ItemID;
                                    stockMovement.DepoID = objSuratJalan.DepoID;
                                    stockMovement.Quantity = (paketItemDetail.Quantity * paketQty);
                                    stockMovement.Status = 1;
                                    stockMovement.CreatedBy = objSuratJalan.CreatedBy;

                                    stockMovementFacade = new StockMovementFacade();
                                    intResult = stockMovementFacade.Insert(stockMovement);
                                    if (stockMovementFacade.Error != string.Empty)
                                    {
                                        transManager.RollbackTransaction();
                                        return stockMovementFacade.Error;
                                    }
                                }
                            }
                        }
                    }

                    transManager.CommitTransaction();
                    transManager.CloseConnection();

                }
                else
                {
                    transManager.RollbackTransaction();
                    return "Error";
                }

            //}
            //else
            //{
            //    transManager.RollbackTransaction();
            //    return "Error";
            //}

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
            //int jumSJ = suratJalanFacade.RetrieveOutStandingOPById(objSuratJalan.OPID);

            //OPFacade opFacade = new OPFacade();
            //OP op = opFacade.RetrieveByID(objSuratJalan.OPID);


            //if (jumSJ > 0)
            //    op.Status = 8;
            //else
            //    op.Status = 9;

            //opFacade = new OPFacade(op);
            //intResult = opFacade.Update(transManager);

            //if (opFacade.Error == string.Empty)
            //{
            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();
            //}


            return string.Empty;
        }
        public string TurunStatusSuratJalan()
        {
            int intResult = 0;
            objSuratJalan.Status = 0;
            //objSuratJalan.TglKirimActual = objSuratJalan.TglKirimActual;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanFacade(objSuratJalan);
            //intResult = absTrans.Update(transManager);
            intResult = ((SuratJalanFacade)absTrans).TurunStatusSuratJalan(transManager);
            if (intResult > 0)
            {
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            else
                transManager.RollbackTransaction();

            return string.Empty;
        }

        public string SettingDepoSJ(int depoID)
        {
            DepoFacade depoFacade = new DepoFacade();

            Depo depo = depoFacade.RetrieveById(depoID);

            if (depoFacade.Error == string.Empty)
            {
                if (depo.ID > 0)
                {
                    return "/" + depo.InitialToko;
                }
            }

            return string.Empty;
            
        }
    }
}
