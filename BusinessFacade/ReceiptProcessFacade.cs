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
    public class ReceiptProcessFacade
    {
        private Receipt objReceipt;
        private ReceiptDocNo objReceiptDocNo;
        private ArrayList arrReceiptDetail;
        private string strError = string.Empty;
        private int intReceiptID = 0;
        private int intReceiptDocNoID = 0;
        private string bl = string.Empty;
        private string th = string.Empty;

        public ReceiptProcessFacade(Receipt Receipt, ArrayList arrListReceipt, ReceiptDocNo receiptDocNo)
        {
            objReceipt = Receipt;
            arrReceiptDetail = arrListReceipt;
            objReceiptDocNo = receiptDocNo;
        }

        public string ReceiptNo
        {
            get
            {
                return objReceiptDocNo.ReceiptCode + th + bl + "-" + objReceiptDocNo.NoUrut.ToString().PadLeft(5, '0');
            }
        }



        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            #region
            //if (objReceiptDocNo.NoUrut == 1)
            //{
            //    ReceiptDocNoFacade receiptDocNoFacade = new ReceiptDocNoFacade();
            //    intReceiptDocNoID = receiptDocNoFacade.Insert(objReceiptDocNo);
            //    if (receiptDocNoFacade.Error != string.Empty)
            //    {
            //        return receiptDocNoFacade.Error;
            //    }
            //}
            //else
            //{
            //    ReceiptDocNoFacade receiptDocNoFacade = new ReceiptDocNoFacade();
            //    intReceiptDocNoID = receiptDocNoFacade.Update(objReceiptDocNo);
            //    if (receiptDocNoFacade.Error != string.Empty)
            //    {
            //        return receiptDocNoFacade.Error;
            //    }            
            //}
            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            #endregion
            if (objReceiptDocNo.NoUrut == 1)
            {
                AbstractTransactionFacade receiptDocNoFacade = new ReceiptDocNoFacade(objReceiptDocNo);
                intReceiptDocNoID = receiptDocNoFacade.Insert(transManager);
                if (receiptDocNoFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return receiptDocNoFacade.Error;
                }
            }
            else
            {
                AbstractTransactionFacade receiptDocNoFacade = new ReceiptDocNoFacade(objReceiptDocNo);
                intReceiptDocNoID = receiptDocNoFacade.Update(transManager);
                if (receiptDocNoFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return receiptDocNoFacade.Error;
                }
            }

            AbstractTransactionFacade absTrans = new ReceiptFacade(objReceipt);
            intReceiptID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intReceiptID > 0)
            {
                if (objReceiptDocNo.MonthPeriod < 10)
                    bl = "0" + objReceiptDocNo.MonthPeriod.ToString();
                else
                    bl = objReceiptDocNo.MonthPeriod.ToString();
                th = objReceiptDocNo.YearPeriod.ToString().Substring(2, 2);

                objReceipt.ReceiptNo = objReceiptDocNo.ReceiptCode + th + bl + "-" + objReceiptDocNo.NoUrut.ToString().PadLeft(5, '0');
                objReceipt.ID = intReceiptID;

                //create no
                absTrans = new ReceiptFacade(objReceipt);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }

                // utk update status pada PO
                POPurchnFacade poPurchnFacade = new POPurchnFacade();
                POPurchn cekPOPurchn = poPurchnFacade.CekSisaPO(objReceipt.PoNo);
                if (poPurchnFacade.Error == string.Empty)
                {
                    if (cekPOPurchn.ID > 0)
                    {
                        if (cekPOPurchn.QtyPO == cekPOPurchn.QtyReceipt)
                            cekPOPurchn.Status = 2;
                        else
                            cekPOPurchn.Status = 1;

                        POPurchn poPurchn = new POPurchn();
                        poPurchn.Status = cekPOPurchn.Status;
                        poPurchn.ID = objReceipt.PoID;

                        poPurchnFacade = new POPurchnFacade(poPurchn);
                        intResult = poPurchnFacade.UpdateStatusPO(transManager);
                        if (poPurchnFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return poPurchnFacade.Error;
                        }
                    }
                }
                //until here

                if (arrReceiptDetail.Count > 0)
                {
                    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    {
                        InventoryFacade inventoryFacade = new InventoryFacade();
                        Inventory inventory = new Inventory();
                        if (receiptDetail.TimbanganBPAS > 0)
                        {
                            receiptDetail.TimbanganBPAS = receiptDetail.TimbanganBPAS;
                        }
                        else
                        {
                            receiptDetail.TimbanganBPAS = 0;
                        }
                        receiptDetail.ReceiptID = intReceiptID;
                        absTrans = new ReceiptDetailFacade(receiptDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }



                        //update status PO by receipt
                        int podetailID = receiptDetail.PODetailID;
                        POPurchnDetailEditFacade podet = new POPurchnDetailEditFacade();
                        int intResulto = podet.UpdateByReceipt(podetailID);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                        // until here

                        //insert flag multigudang
                        SPP spp = new SPP();
                        SPPFacade sppf = new SPPFacade();
                        spp = sppf.RetrieveById(receiptDetail.SppID);
                        if (spp.SatuanID == 2)
                        {
                            Users users = new Users();
                            UsersFacade usersf = new UsersFacade();
                            users = usersf.RetrieveById(spp.UserID);
                            SPPMultiGudang sppm = new SPPMultiGudang();
                            SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
                            //ItemID, GroupID, ItemTypeID, QtyReceipt, QtyPakai, Aktif
                            sppm.SPPID = spp.ID;
                            sppm.GudangID = users.DeptID;
                            sppm.ItemID = receiptDetail.ItemID;
                            sppm.GroupID = receiptDetail.GroupID;
                            sppm.ItemTypeID = receiptDetail.ItemTypeID;
                            sppm.QtyReceipt = decimal.Parse(receiptDetail.Quantity.ToString());
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


                        inventory.ID = receiptDetail.ItemID;
                        inventory.Jumlah = receiptDetail.Quantity;
                        int intResulte = inventoryFacade.UpdateQty(inventory);
                        //insert to memoharian trans
                        if (receiptDetail.DOID > 0)
                        {
                            ReceiptDetailFacade rcpd = new ReceiptDetailFacade();
                            ReceiptDetail rcd = new ReceiptDetail();
                            rcd.ScheduleNo = receiptDetail.ScheduleNo;
                            rcd.ID = receiptDetail.DOID;
                            rcd.ReceiptID = receiptDetail.ReceiptID;
                            rcd.ItemID = receiptDetail.ItemID;
                            rcd.Quantity = receiptDetail.Quantity;
                            int result = rcpd.InsertMemoHarian(rcd);

                        }
                    }
                    //selesai
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
            {
                ReceiptDetailFacade rcdetail = new ReceiptDetailFacade();
                rcdetail.updateAveragePrice(receiptDetail, objReceipt.ReceiptDate.Month, objReceipt.ReceiptDate.Year);
                int res = rcdetail.update_timbangan(intResult, receiptDetail.TimbanganBPAS.ToString());
                /** Update memoharian untuk parsial delivery */
                if (receiptDetail.DOID > 0)
                {
                    intResult = rcdetail.UpdateParsialDelivert(receiptDetail.DOID, receiptDetail.ReceiptID);
                }
                /** proses buat BA otomatis */

            }
            return string.Empty;
        }


        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ReceiptFacade(objReceipt);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            //if (intResult > 0)
            //{
            //    if (arrSPPDetail.Count > 0)
            //    {
            //        absTrans = new SPPDetailFacade((SPPDetail)arrSPPDetail[0]);
            //        intResult = absTrans.Delete(transManager);
            //        if (absTrans.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return absTrans.Error;
            //        }

            //        foreach (SPPDetail sPPDetail in arrSPPDetail)
            //        {
            //            sPPDetail.SPPID = objSPP.ID;
            //            absTrans = new SPPDetailFacade(sPPDetail);
            //            intResult = absTrans.Insert(transManager);
            //            if (absTrans.Error != string.Empty)
            //            {
            //                transManager.RollbackTransaction();
            //                return absTrans.Error;
            //            }
            //        }
            //    }
            //}

            transManager.CommitTransaction();
            transManager.CloseConnection();

            //return string.Empty;
            //int intResult = 0;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new ScheduleFacade(objReceipt);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}
            //if (intResult > 0)
            //{
            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();

            //    transManager = new TransactionManager(Global.ConnectionString());
            //    transManager.BeginTransaction();

            //    if (arrReceiptDetail.Count > 0)
            //    {
            //        ScheduleDetailFacade scheduleDetailFacade = new ScheduleDetailFacade();
            //        ArrayList arrCurrentScheduleDetail = scheduleDetailFacade.RetrieveByNo(objReceipt.ReceiptNo);

            //        if (scheduleDetailFacade.Error == string.Empty)
            //        {
            //            ScheduleDetail scD = (ScheduleDetail)arrCurrentScheduleDetail[0];
            //            if (scD.ID == 0)
            //            {
            //                arrCurrentScheduleDetail = scheduleDetailFacade.RetrieveByNo2(objReceipt.ReceiptNo);
            //            }
            //            if (scheduleDetailFacade.Error == string.Empty)
            //            {
            //                foreach (ScheduleDetail scheduleDetail in arrReceiptDetail)
            //                {
            //                    int i = 0;
            //                    foreach (ScheduleDetail sc in arrCurrentScheduleDetail)
            //                    {
            //                        if (scheduleDetail.DocumentNo == sc.DocumentNo && scheduleDetail.ItemID == sc.ItemID)
            //                        {
            //                            i = 1;
            //                            break;
            //                        }
            //                    }

            //                    if (i == 0)
            //                    {
            //                        scheduleDetail.ScheduleID = objReceipt.ID;
            //                        absTrans = new ScheduleDetailFacade(scheduleDetail);
            //                        intResult = absTrans.Insert(transManager);
            //                        if (absTrans.Error != string.Empty)
            //                        {
            //                            transManager.RollbackTransaction();
            //                            return absTrans.Error;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //ScheduleDetailFacade sDetailFacade = new ScheduleDetailFacade();
            //ArrayList arrDistinctReceipt = sDetailFacade.RetrieveDistinctById(objReceipt.ID);

            //if (sDetailFacade.Error == string.Empty)
            //{
            //    if (arrDistinctReceipt.Count > 0)
            //    {
            //        foreach (int[] documentId in arrDistinctReceipt)
            //        {
            //            if (documentId[1] == 0)
            //            {
            //                OPDetailFacade opDetailFacade = new OPDetailFacade();
            //                ArrayList arrOpDetail = opDetailFacade.RetrieveById(documentId[0]);
            //                if (opDetailFacade.Error == string.Empty)
            //                {
            //                    int intFlag = 0;
            //                    foreach (OPDetail opDetail in arrOpDetail)
            //                    {
            //                        if (opDetail.QtyScheduled != opDetail.Quantity)
            //                            intFlag = 1;
            //                    }

            //                    OPFacade opFacade = new OPFacade();
            //                    OP op = opFacade.RetrieveByID(documentId[0]);
            //                    if (opFacade.Error == string.Empty)
            //                    {
            //                        if (intFlag == 0)
            //                            op.Status = 4;

            //                        else
            //                        {
            //                            if (op.Status == 2)
            //                                op.Status = 3;
            //                        }

            //                        opFacade = new OPFacade(op);
            //                        intResult = opFacade.Update(transManager);
            //                        if (opFacade.Error != string.Empty)
            //                        {
            //                            transManager.RollbackTransaction();
            //                            return opFacade.Error;
            //                        }

            //                    }
            //                }
            //                else
            //                {
            //                    transManager.RollbackTransaction();
            //                    return opDetailFacade.Error;
            //                }
            //            }
            //            else
            //            {
            //                TransferDetailFacade transferDetailFacade = new TransferDetailFacade();
            //                ArrayList arrTransferDetail = transferDetailFacade.RetrieveById(documentId[0]);
            //                if (transferDetailFacade.Error == string.Empty)
            //                {
            //                    int intFlag = 0;
            //                    foreach (TransferDetail transDetail in arrTransferDetail)
            //                    {
            //                        if (transDetail.QtyScheduled != transDetail.Qty)
            //                            intFlag = 1;
            //                    }

            //                    TransferOrderFacade transferOrderFacade = new TransferOrderFacade();
            //                    TransferOrder transferOrder = transferOrderFacade.RetrieveByID(documentId[0]);
            //                    if (transferOrderFacade.Error == string.Empty)
            //                    {
            //                        if (intFlag == 0)
            //                            transferOrder.Status = 4;

            //                        else
            //                        {
            //                            if (transferOrder.Status == 2)
            //                                transferOrder.Status = 3;
            //                        }

            //                        transferOrderFacade = new TransferOrderFacade(transferOrder);
            //                        intResult = transferOrderFacade.Update(transManager);
            //                        if (transferOrderFacade.Error != string.Empty)
            //                        {
            //                            transManager.RollbackTransaction();
            //                            return transferOrderFacade.Error;
            //                        }

            //                    }
            //                }
            //                else
            //                {
            //                    transManager.RollbackTransaction();
            //                    return transferDetailFacade.Error;
            //                }
            //            }

            //        }
            //    }

            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();

            //}
            //else
            //{
            //    transManager.RollbackTransaction();
            //    return sDetailFacade.Error;
            //}


            return string.Empty;
        }

        public string CancelReceiptDetail()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ReceiptDetail receiptDetail = new ReceiptDetail();
            int jBiaya = 0;
            foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            {
                receiptDetail.ID = rcpDetail.ID;
                receiptDetail.ItemID = rcpDetail.ItemID;
                receiptDetail.Quantity = rcpDetail.Quantity;
                receiptDetail.PoID = rcpDetail.PoID;
                receiptDetail.FlagPO = rcpDetail.FlagPO;
                //if (objReceipt.ReceiptType == 7)
                //    receiptDetail.FlagTipe = 3;
                //else
                //receiptDetail.FlagTipe = 2;
                receiptDetail.FlagTipe = rcpDetail.ItemTypeID;
                if (rcpDetail.ItemTypeID == 3)
                    receiptDetail.ItemID2 = rcpDetail.ItemID2;
                jBiaya = rcpDetail.ItemTypeID;
            }

            receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);

            intResult = receiptDetailFacade.CancelReceiptDetail(transManager);
            /**
             * Cancel qty keterangan biaya tabel biaya
             * */
            #region cancel biaya
            //if (jBiaya == 3)
            //{
            //    ReceiptDetail receiptDetailh = new ReceiptDetail();
            //    foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            //    {
            //        receiptDetailh.ID = rcpDetail.ID;
            //        receiptDetailh.ItemID = rcpDetail.ItemID2;
            //        receiptDetailh.Quantity = rcpDetail.Quantity;
            //        receiptDetailh.PoID = rcpDetail.PoID;
            //        receiptDetailh.FlagPO = rcpDetail.FlagPO;
            //        receiptDetailh.FlagTipe = rcpDetail.ItemTypeID;

            //    }
            //    receiptDetailFacade = new ReceiptDetailFacade(receiptDetailh);
            //    intResult = receiptDetailFacade.CancelReceiptDetail(transManager);
            //    if (receiptDetailFacade.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return receiptDetailFacade.Error;
            //    }
            //}
            #endregion
            if (receiptDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return receiptDetailFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelReceipt()
        {
            int intResult = 0;
            //string asString = string.Empty;
            objReceipt.Status = -1;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ReceiptFacade(objReceipt);
            ReceiptFacade receiptFacade = new ReceiptFacade();

            absTrans = new ReceiptFacade(objReceipt);
            //intResult = absTrans.Delete(transManager);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }


            TransactionManager transManager2 = new TransactionManager(Global.ConnectionString());
            transManager2.BeginTransaction();
            int jBiaya = 0;
            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ReceiptDetail receiptDetail = new ReceiptDetail();
            foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            {
                receiptDetail.ID = rcpDetail.ID;
                receiptDetail.ItemID = rcpDetail.ItemID;
                receiptDetail.Quantity = rcpDetail.Quantity;
                receiptDetail.PoID = rcpDetail.PoID;
                receiptDetail.FlagPO = rcpDetail.FlagPO;
                receiptDetail.FlagTipe = rcpDetail.ItemTypeID;
                jBiaya = rcpDetail.ItemTypeID;
                receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);
                //intResult = receiptDetailFacade.CancelInventoryByReceiptDetail(transManager2);
                intResult = receiptDetailFacade.CancelReceiptDetail(transManager2);
                if (receiptDetailFacade.Error != string.Empty)
                {
                    transManager2.RollbackTransaction();
                    return receiptDetailFacade.Error;
                }

            }
            /**
                * Cancel qty group biaya tabel biaya jika itemtypeid nya 3
                * */
            #region cancel biaya
            // TransactionManager transManager3 = new TransactionManager(Global.ConnectionString());
            //  transManager3.BeginTransaction();
            //if (jBiaya == 3)
            // {
            //     ReceiptDetail receiptDetailh = new ReceiptDetail();
            //     foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            //     {
            //         receiptDetailh.ID = rcpDetail.ID;
            //         receiptDetailh.ItemID = rcpDetail.ItemID2;
            //         receiptDetailh.Quantity = rcpDetail.Quantity;
            //         receiptDetailh.PoID = rcpDetail.PoID;
            //         receiptDetailh.FlagPO = rcpDetail.FlagPO;
            //         receiptDetailh.FlagTipe = rcpDetail.ItemTypeID;

            //     }
            //     receiptDetailFacade = new ReceiptDetailFacade(receiptDetailh);
            //     intResult = receiptDetailFacade.CancelReceiptDetail(transManager3);
            //     if (receiptDetailFacade.Error != string.Empty)
            //     {
            //         transManager3.RollbackTransaction();
            //         return receiptDetailFacade.Error;
            //     }
            // }

            // if (jBiaya == 3)
            // {
            //     transManager3.CommitTransaction();
            //     transManager3.CloseConnection();
            // } 
            #endregion
            //comit delete detail
            transManager2.CommitTransaction();
            transManager2.CloseConnection();
            //comit delet header
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Delete()
        {
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new ScheduleFacade(objReceipt);
            //intResult = absTrans.Delete(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprove()
        {
            int intResult = 0;
            objReceipt.Status = 1;
            objReceipt.ApprovalDate = DateTime.Now;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ReceiptFacade(objReceipt);
            ReceiptFacade receiptFacade = new ReceiptFacade();

            absTrans = new ReceiptFacade(objReceipt);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            if (arrReceiptDetail.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                {
                    #region mark
                    // for Biaya
                    //if (objPakai.PakaiTipe == 6)
                    //{
                    //    BiayaFacade biayaFacade = new BiayaFacade();
                    //    Biaya biaya = new Biaya();

                    //    biaya.ID = pakaiDetail.ItemID;
                    //    biaya.Jumlah = pakaiDetail.Quantity;

                    //    intResult = biayaFacade.MinusQty(biaya);
                    //    if (absTrans.Error != string.Empty)
                    //    {
                    //        transManager.RollbackTransaction();
                    //        return absTrans.Error;
                    //    }

                    //    //kok utk biaya simple ya............................
                    //    //apa karena gak maintenance Qty
                    //}
                    //for asset
                    //else if (objPakai.PakaiTipe == 5)
                    //{
                    //    AssetFacade assetFacade = new AssetFacade();
                    //    Asset asset = new Asset();

                    //    asset.ID = pakaiDetail.ItemID;
                    //    asset.Jumlah = pakaiDetail.Quantity;

                    //    intResult = assetFacade.MinusQty(asset);
                    //    if (absTrans.Error != string.Empty)
                    //    {
                    //        transManager.RollbackTransaction();
                    //        return absTrans.Error;
                    //    }
                    //    else
                    //    {
                    //        Asset cekInvAsset = assetFacade.InvAssetRetrieveByAssetID(pakaiDetail.ItemID, objPakai.DeptID);
                    //        if (cekInvAsset.ID > 0)
                    //            asset.Flag = 1;
                    //            //update by assetid+deptid
                    //        else
                    //            asset.Flag = 0;
                    //            //insert

                    //        asset.AssetID = pakaiDetail.ItemID;
                    //        asset.Jumlah = pakaiDetail.Quantity;
                    //        asset.DeptID = objPakai.DeptID;
                    //        asset.Gudang = objPakai.DepoID;
                    //        asset.RowStatus = 0;

                    //        intResult = assetFacade.InsertUpdateInvAsset(asset);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //    }
                    //}
                    //else

                    //if (objPakai.PakaiTipe != 5 || objPakai.PakaiTipe != 6)
                    //{
                    //    // utk minus ke table inventori
                    //    InventoryFacade inventoryFacade = new InventoryFacade();
                    //    Inventory inventory = new Inventory();

                    //    inventory.ID = pakaiDetail.ItemID;
                    //    inventory.Jumlah = pakaiDetail.Quantity;

                    //    intResult = inventoryFacade.MinusQtyTransit(inventory);
                    //    if (absTrans.Error != string.Empty)
                    //    {
                    //        transManager.RollbackTransaction();
                    //        return absTrans.Error;
                    //    }
                    //    // until here

                    //}

                    //if (pakaiDetail.ItemTypeID == 1)
                    //{
                    //    // utk minus ke table inventori
                    //    InventoryFacade inventoryFacade = new InventoryFacade();
                    //    Inventory inventory = new Inventory();

                    //    inventory.ID = pakaiDetail.ItemID;
                    //    inventory.Jumlah = pakaiDetail.Quantity;

                    //    intResult = inventoryFacade.MinusQtyTransit(inventory);
                    //    if (absTrans.Error != string.Empty)
                    //    {
                    //        transManager.RollbackTransaction();
                    //        return absTrans.Error;
                    //    }
                    //}
                    //else
                    //{
                    //    //if (pakaiDetail.ItemTypeID == 2)
                    //    //{
                    //    //    // utk minus ke table asset
                    //    //    AssetFacade assetFacade = new AssetFacade();
                    //    //    Asset asset = new Asset();

                    //    //    asset.ID = pakaiDetail.ItemID;
                    //    //    asset.Jumlah = pakaiDetail.Quantity;

                    //    //    intResult = assetFacade.MinusQtyTransit(asset);
                    //    //    if (absTrans.Error != string.Empty)
                    //    //    {
                    //    //        transManager.RollbackTransaction();
                    //    //        return absTrans.Error;
                    //    //    }
                    //    //}
                    //    else
                    //    {
                    //        // utk minus ke table Biaya
                    //        BiayaFacade biayaFacade = new BiayaFacade();
                    //        Biaya biaya = new Biaya();

                    //        biaya.ID = pakaiDetail.ItemID;
                    //        biaya.Jumlah = pakaiDetail.Quantity;

                    //        intResult = biayaFacade.MinusQtyTransit(biaya);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //    }
                    //}
                    #endregion

                }

            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

    }

}
