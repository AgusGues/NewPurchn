using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace BusinessFacade
{
    public class SuratJalanProcessTOFacade
    {
        private SuratJalanTO objSuratJalanTO;
        private ArrayList arrSuratJalanDetailTO;
        private string strError = string.Empty;
        private int intSuratJalanTOID = 0;

        public SuratJalanProcessTOFacade(SuratJalanTO suratJalanTO, ArrayList arrListSuratJalanTO)
        {
            objSuratJalanTO = suratJalanTO;
            arrSuratJalanDetailTO = arrListSuratJalanTO;
        }

        public string SuratJalanTONo
        {
            get
            {
                return intSuratJalanTOID.ToString().PadLeft(9, '0') + "/TO/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }


        public string Insert()
        {
            int intResult = 0;


            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intSuratJalanTOID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intSuratJalanTOID > 0)
            {
                objSuratJalanTO.SuratJalanNo = intSuratJalanTOID.ToString().PadLeft(9, '0') + "/TO/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objSuratJalanTO.ID = intSuratJalanTOID;

                absTrans = new SuratJalanTOFacade(objSuratJalanTO);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }

                if (arrSuratJalanDetailTO.Count > 0)
                {
                    //absTrans = new SuratJalanDetailTOFacade((SuratJalanDetailTO)arrSuratJalanDetailTO[0]);
                    //intResult = absTrans.Delete(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}

                    foreach (SuratJalanDetailTO suratJalanDetailTO in arrSuratJalanDetailTO)
                    {
                        suratJalanDetailTO.SuratJalanTOID = intSuratJalanTOID;
                        absTrans = new SuratJalanDetailTOFacade(suratJalanDetailTO);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            else
            {
                transManager.RollbackTransaction();
                return "error";
            }
            

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //ScheduleFacade scheduleFacade = new ScheduleFacade();
            //int countSchedule = scheduleFacade.RetrieveScheduleSJTOByDocument(objSuratJalanTO.TransferOrderID);

            //TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
            //TransferOrder transferOrder = transferOrderFacade.RetrieveByID(objSuratJalanTO.TransferOrderID);
            //if (transferOrder.ID > 0)
            //{
            //    if (countSchedule == 0)
            //        transferOrder.Status = 7;
            //    else
            //        transferOrder.Status = 6;

            //    transferOrderFacade = new TransferOrderFacade(transferOrder);
            //    intResult = transferOrderFacade.Update(transManager);
            //    if (transferOrderFacade.Error != string.Empty)
            //        return transferOrderFacade.Error;


            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();
            //}


            return string.Empty;
        }

        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                if (arrSuratJalanDetailTO.Count > 0)
                {
                    absTrans = new SuratJalanDetailTOFacade((SuratJalanDetailTO)arrSuratJalanDetailTO[0], objSuratJalanTO.SuratJalanNo);
                    intResult = absTrans.Delete(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }

                    foreach (SuratJalanDetailTO suratJalanDetailTO in arrSuratJalanDetailTO)
                    {
                        suratJalanDetailTO.SuratJalanTOID = objSuratJalanTO.ID;
                        absTrans = new SuratJalanDetailTOFacade(suratJalanDetailTO);
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

        public string PostingShipment()
        {
            int intResult = 0;
            objSuratJalanTO.Status = 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }           
                if (intResult > 0)
                {
                     intResult = ((SuratJalanTOFacade)absTrans).UpdatePostingDate(transManager, 1);
                     if (intResult > 0)
                     {
                         foreach (SuratJalanDetailTO suratJalanDetailTO in arrSuratJalanDetailTO)
                         {
                             suratJalanDetailTO.Flag = 1;
                             absTrans = new SuratJalanDetailTOFacade(suratJalanDetailTO);
                             intResult = absTrans.Update(transManager);
                             if (absTrans.Error != string.Empty)
                             {
                                 transManager.RollbackTransaction();
                                 return absTrans.Error;
                             }
                         }

                         TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
                         TransferOrder transOrder = transferOrderFacade.RetrieveByID(objSuratJalanTO.TransferOrderID);
                         if (transferOrderFacade.Error == string.Empty)
                         {
                             if (transOrder.ID > 0)
                             {
                                 StockMovementFacade stockMovementFacade = new StockMovementFacade();
                                 foreach (SuratJalanDetail sjd in arrSuratJalanDetailTO)
                                 {
                                     ItemsFacade itemsFacade = new ItemsFacade();
                                     Items items = itemsFacade.RetrieveById(sjd.ItemID);
                                     if (itemsFacade.Error != string.Empty)
                                         return itemsFacade.Error;


                                     itemsFacade = new ItemsFacade();
                                     Items itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCategory);
                                     if (itemsFacade.Error != string.Empty)
                                         return itemsFacade.Error;


                                     StockMovement stockMovement = new StockMovement();
                                     stockMovementFacade = new StockMovementFacade();
                                     stockMovement.TypeDoc = 2;
                                     stockMovement.NoDoc = objSuratJalanTO.SuratJalanNo;
                                     stockMovement.TglDoc = objSuratJalanTO.CreatedTime;
                                     if (itemsHead.ID > 0)
                                         stockMovement.ItemID = itemsHead.ID;
                                     else
                                         stockMovement.ItemID = sjd.ItemID;

                                     stockMovement.DepoID = transOrder.FromDepoID;
                                     stockMovement.Quantity = sjd.Qty;
                                     stockMovement.Status = 1;
                                     stockMovement.CreatedBy = objSuratJalanTO.CreatedBy;

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

                         transManager.CommitTransaction();
                         transManager.CloseConnection();
                     }
                     else
                     {
                         transManager.RollbackTransaction();
                         return "Error";
                     }
                }
                else
                {
                    transManager.RollbackTransaction();
                    return "Error";
                }
            


            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();
            //int jumSJ = suratJalanTOFacade.RetrieveJumOpenSJ(objSuratJalanTO.TransferOrderID);

            //TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
            //TransferOrder transferOrder = transferOrderFacade.RetrieveByID(objSuratJalanTO.TransferOrderID);


            //if (jumSJ > 0)
            //    transferOrder.Status = 8;
            //else
            //    transferOrder.Status = 9;

            //transferOrderFacade = new TransferOrderFacade(transferOrder);
            //intResult = transferOrderFacade.Update(transManager);

            //if (transferOrderFacade.Error == string.Empty)
            //{
            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();            
            //}


            return string.Empty; 
        }
        public string TurunStatusSuratJalanTO()
        {
            int intResult = 0;
            objSuratJalanTO.Status = 0;
            //objSuratJalan.TglKirimActual = objSuratJalan.TglKirimActual;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            //intResult = absTrans.Update(transManager);
            intResult = ((SuratJalanTOFacade)absTrans).TurunStatusSuratJalanTO(transManager);
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
            objSuratJalanTO.Cetak = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
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

        public string PostingReceive()
        {
            int intResult = 0;
            objSuratJalanTO.Status = 2;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty && intResult>0)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                intResult = ((SuratJalanTOFacade)absTrans).UpdatePostingDate(transManager, 2);
                if (intResult > 0)
                {
                    foreach (SuratJalanDetailTO suratJalanDetailTO in arrSuratJalanDetailTO)
                    {
                        suratJalanDetailTO.Flag = 2;
                        absTrans = new SuratJalanDetailTOFacade(suratJalanDetailTO);
                        intResult = absTrans.Update(transManager);
                        if (absTrans.Error != string.Empty && intResult > 0)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                        ItemsFacade itemsFacade = new ItemsFacade();
                        Items items = itemsFacade.RetrieveById(suratJalanDetailTO.ItemID);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;

                        Items itemsHead = new Items();
                        itemsFacade = new ItemsFacade();
                        itemsHead = itemsFacade.RetrieveByGroupCode(items.GroupCategory);
                        if (itemsFacade.Error != string.Empty)
                            return itemsFacade.Error;

                        StockMovementFacade stockMovementFacade = new StockMovementFacade();

                        StockMovement stockMovement = new StockMovement();
                        stockMovementFacade = new StockMovementFacade();
                        stockMovement.TypeDoc = 2;
                        stockMovement.NoDoc = objSuratJalanTO.SuratJalanNo;
                        stockMovement.TglDoc = objSuratJalanTO.CreatedTime;
                        if (itemsHead.ID > 0)
                            stockMovement.ItemID = itemsHead.ID;
                        else
                            stockMovement.ItemID = suratJalanDetailTO.ItemID;

                        stockMovement.DepoID = objSuratJalanTO.DepoID;
                        stockMovement.ToDepoID = objSuratJalanTO.ToDepoID;
                        stockMovement.Quantity = suratJalanDetailTO.Qty;
                        stockMovement.Status = 2;
                        stockMovement.CreatedBy = objSuratJalanTO.CreatedBy;

                        stockMovementFacade = new StockMovementFacade();
                        intResult = stockMovementFacade.Insert(stockMovement);
                        if (stockMovementFacade.Error != string.Empty && intResult > 0)
                        {
                            transManager.RollbackTransaction();
                            return stockMovementFacade.Error;
                        }

                        InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

                        InventoryStock inventoryStock = new InventoryStock();

                        if (itemsHead.ID > 0)
                            inventoryStock.ItemID = itemsHead.ID;
                        else
                            inventoryStock.ItemID = suratJalanDetailTO.ItemID;

                        inventoryStock = inventoryStockFacade.RetrieveByItemCodeAndDepoAndKondisi(inventoryStock.ItemID, objSuratJalanTO.ToDepoID, suratJalanDetailTO.TypeKondisi);

                        if (itemsHead.ID > 0)
                            inventoryStock.ItemID = itemsHead.ID;
                        else
                            inventoryStock.ItemID = suratJalanDetailTO.ItemID;

                        string strData = string.Empty;
                        if (inventoryStock.ID>0)
                            strData = "Update";
                        else
                            strData = "Insert";


                        inventoryStock.Quantity = suratJalanDetailTO.Qty;
                        inventoryStock.DepoID = objSuratJalanTO.ToDepoID;
                        inventoryStock.Status = 4;
                        //inventoryStock.TypeKondisi = 0;
                        //9Juni2017
                        inventoryStock.TypeKondisi = suratJalanDetailTO.TypeKondisi;
                        inventoryStock.FromDepoID = suratJalanDetailTO.FromDepoID;

                        inventoryStockFacade = new InventoryStockFacade();
                        if (itemsHead.ID > 0 && strData=="Update")
                            intResult = inventoryStockFacade.Update(inventoryStock);
                        else
                            intResult = inventoryStockFacade.Insert(inventoryStock);
                        if (inventoryStockFacade.Error != string.Empty && intResult > 0)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
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
            }
            else
            {
                transManager.RollbackTransaction();
                return "Error";
            }




            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();
            //int jumSJ = suratJalanTOFacade.RetrieveJumOpenSJ(objSuratJalanTO.TransferOrderID);

            //TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
            //TransferOrder transferOrder = transferOrderFacade.RetrieveByID(objSuratJalanTO.TransferOrderID);


            //if (jumSJ > 0)
            //    transferOrder.Status = 8;
            //else
            //    transferOrder.Status = 9;

            //transferOrderFacade = new TransferOrderFacade(transferOrder);
            //intResult = transferOrderFacade.Update(transManager);

            //if (transferOrderFacade.Error == string.Empty)
            //{
            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();            
            //}


            return string.Empty;
        }

        public string CancelSuratJalan()
        {
            int intResult = 0;
            objSuratJalanTO.Status = -1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                 TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
                    TransferOrder transOrder = transferOrderFacade.RetrieveByID(objSuratJalanTO.TransferOrderID);
                    if (transferOrderFacade.Error == string.Empty)
                    {
                        if (transOrder.ID > 0)
                        {
                            foreach (SuratJalanDetailTO suratJalanDetailTO in arrSuratJalanDetailTO)
                            {
                                suratJalanDetailTO.Flag = 3;
                                absTrans = new SuratJalanDetailTOFacade(suratJalanDetailTO);
                                intResult = absTrans.Update(transManager);

                                InventoryStockFacade inventoryStockFacade = new InventoryStockFacade();

                                InventoryStock inventoryStock = new InventoryStock();
                                inventoryStock.Status = 5;
                                inventoryStock.ItemID = suratJalanDetailTO.ItemID;
                                inventoryStock.Quantity = suratJalanDetailTO.Qty;
                                inventoryStock.DepoID = transOrder.FromDepoID;

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

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
       
            return string.Empty;
        }

        public string UpdateCetakSuratJalan()
        {
            int intResult = 0;
            objSuratJalanTO.Cetak = 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
            intResult = absTrans.Update(transManager);
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

            return string.Empty;
        }

        public string Delete()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SuratJalanTOFacade(objSuratJalanTO);
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
    }
}
