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
    public class ReturPakaiProcessFacade
    {
        private ReturPakai objReturPakai;
        private ArrayList arrReturPakaiDetail;
        private string strError = string.Empty;
        private int intReturID = 0;
        private string bl = string.Empty;
        private string th = string.Empty;

        public ReturPakaiProcessFacade(ReturPakai pakai, ArrayList arrListPakai)
        {
            objReturPakai = pakai;
            arrReturPakaiDetail = arrListPakai;
        }

        public string ReturNo
        {
            get
            {
                return intReturID.ToString().PadLeft(5, '0') + "/RTR/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }


        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ReturPakaiFacade(objReturPakai);
            intReturID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intReturID > 0)
            {
                objReturPakai.ReturNo = intReturID.ToString().PadLeft(5, '0') + "/RTR/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objReturPakai.ID = intReturID;

                //create no
                absTrans = new ReturPakaiFacade(objReturPakai);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }

                if (arrReturPakaiDetail.Count > 0)
                {
                    foreach (ReturPakaiDetail returPakaiDetail in arrReturPakaiDetail)
                    {
                        returPakaiDetail.ReturID = intReturID;
                        absTrans = new ReturPakaiDetailFacade(returPakaiDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                        // for Biaya
                        if (objReturPakai.ItemTypeID==3)
                        {
                            BiayaFacade biayaFacade = new BiayaFacade();
                            Biaya biaya = new Biaya();

                            biaya.ID = returPakaiDetail.ItemID;
                            biaya.Jumlah = returPakaiDetail.Quantity;
                            //add jumlah
                            intResult = biayaFacade.UpdateQty(biaya);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                        //for asset
                        else if (objReturPakai.ItemTypeID == 2)
                        {
                            AssetFacade assetFacade = new AssetFacade();
                            Asset asset = new Asset();

                            asset.ID = returPakaiDetail.ItemID;
                            asset.Jumlah = returPakaiDetail.Quantity;

                            intResult = assetFacade.UpdateQty(asset);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                            else
                            {
                                Asset cekInvAsset = assetFacade.InvAssetRetrieveByAssetID(returPakaiDetail.ItemID, objReturPakai.DeptID);
                                if (cekInvAsset.ID > 0)
                                    asset.Flag = 1;
                                //update by assetid+deptid
                                else
                                    asset.Flag = 0;
                                //insert

                                asset.AssetID = returPakaiDetail.ItemID;
                                asset.Jumlah = returPakaiDetail.Quantity;
                                asset.DeptID = objReturPakai.DeptID;
                                asset.Gudang = objReturPakai.DepoID;
                                asset.RowStatus = 0;

                                intResult = assetFacade.InsertUpdateInvAsset(asset);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                            }
                        }
                        else
                        {
                            // utk minus ke table inventori
                            InventoryFacade inventoryFacade = new InventoryFacade();
                            Inventory inventory = new Inventory();

                            inventory.ID = returPakaiDetail.ItemID;
                            inventory.Jumlah = returPakaiDetail.Quantity;

                            intResult = inventoryFacade.UpdateQty(inventory);
                            //intResult = inventoryFacade.MinusQty(inventory);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                            // until here

                            //utk add ke elect_repack
                            //khusus utk PakaiRePack aja, gimana ya .....
                            //if (objReturPakai.PakaiTipe == 9)
                            //{
                            //    Inventory invRePack = inventoryFacade.RepackRetrieveById(returPakaiDetail.ItemID);
                            //    if (invRePack.ID > 0)
                            //    {
                            //        // tambah jumlah aja
                            //        inventory.ID = returPakaiDetail.ItemID;
                            //        inventory.Jumlah = returPakaiDetail.Quantity;

                            //        intResult = inventoryFacade.UpdateQtyForRepack(inventory);
                            //        if (absTrans.Error != string.Empty)
                            //        {
                            //            transManager.RollbackTransaction();
                            //            return absTrans.Error;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        // insert baru & jumlah
                            //        inventory.ID = returPakaiDetail.ItemID;
                            //        inventory.Jumlah = returPakaiDetail.Quantity;
                            //        inventory.UOMID = returPakaiDetail.UomID;
                            //        inventory.RowStatus = 0;
                            //        inventory.GroupID = returPakaiDetail.GroupID;
                            //        inventory.Jumlah = returPakaiDetail.Quantity;

                            //        intResult = inventoryFacade.InsertForRePack(inventory);
                            //        if (absTrans.Error != string.Empty)
                            //        {
                            //            transManager.RollbackTransaction();
                            //            return absTrans.Error;
                            //        }
                            //    }
                            //}
                            //until elect_repack
                        }
                    }
                    //selesai

                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Update()
        {
            //int intResult = 0;

            return string.Empty;
        }

        public string CancelPakaiDetail()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            ReturPakaiDetailFacade ReturPakaiDetailFacade = new ReturPakaiDetailFacade();
            ReturPakaiDetail returPakaiDetail = new ReturPakaiDetail();
            foreach (PakaiDetail pkiDetail in arrReturPakaiDetail)
            {
                returPakaiDetail.ID = pkiDetail.ID;
                returPakaiDetail.ItemID = pkiDetail.ItemID;
                returPakaiDetail.Quantity = pkiDetail.Quantity;
                //receiptDetail.PoID = rcpDetail.PoID;
                //receiptDetail.FlagPO = rcpDetail.FlagPO;

                if (objReturPakai.PakaiTipe == 5)
                    returPakaiDetail.FlagTipe = 2;
                else if (objReturPakai.PakaiTipe == 6)
                    returPakaiDetail.FlagTipe = 3;
                else
                    returPakaiDetail.FlagTipe = 1;
                //pakaiDetail.FlagTipe = 1;
            }

            ReturPakaiDetailFacade = new ReturPakaiDetailFacade(returPakaiDetail);

            intResult = ReturPakaiDetailFacade.CancelPakaiDetail(transManager);
            if (ReturPakaiDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return ReturPakaiDetailFacade.Error;
            }

            //kurangi inventoryasset
            //AssetFacade assetFacade = new AssetFacade();
            //Asset asset = new Asset();

            //asset.ID = returPakaiDetail.ItemID;
            //asset.DeptID = objReturPakai.DeptID;
            //asset.Jumlah = returPakaiDetail.Quantity;

            //intResult = assetFacade.MinusQtyInvAsset(asset);
            //if (assetFacade.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return assetFacade.Error;
            //}
            //until here

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelPakai()
        {
            int intResult = 0;
            //string asString = string.Empty;
            objReturPakai.Status = -1;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ReturPakaiFacade(objReturPakai);
            ReturPakaiFacade returPakaiFacade = new ReturPakaiFacade();

            absTrans = new ReturPakaiFacade(objReturPakai);
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

            ReturPakaiDetailFacade ReturPakaiDetailFacade = new ReturPakaiDetailFacade();
            ReturPakaiDetail returPakaiDetail = new ReturPakaiDetail();
            foreach (ReturPakaiDetail pkiDetail in arrReturPakaiDetail)
            {
                returPakaiDetail.ID = pkiDetail.ID;
                returPakaiDetail.ItemID = pkiDetail.ItemID;
                returPakaiDetail.Quantity = pkiDetail.Quantity;
                if (objReturPakai.PakaiTipe == 5)
                    returPakaiDetail.FlagTipe = 2;
                else if (objReturPakai.PakaiTipe == 6)
                    returPakaiDetail.FlagTipe = 3;
                else
                    returPakaiDetail.FlagTipe = 1;

                ReturPakaiDetailFacade = new ReturPakaiDetailFacade(returPakaiDetail);
                intResult = ReturPakaiDetailFacade.CancelInventoryByReturDetail(transManager);
                if (ReturPakaiDetailFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return ReturPakaiDetailFacade.Error;
                }

                //kurangin inventoryasset-nya
                //AssetFacade assetFacade = new AssetFacade();
                //Asset asset = new Asset();

                //asset.ID = pkiDetail.ItemID;
                //asset.DeptID = objReturPakai.DeptID;
                //asset.Jumlah = pkiDetail.Quantity;

                //intResult = assetFacade.MinusQtyInvAsset(asset);
                //if (assetFacade.Error != string.Empty)
                //{
                //    transManager.RollbackTransaction();
                //    return assetFacade.Error;
                //}
                //until here
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Delete()
        {
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new ScheduleFacade(objReturPakai);
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


    }

}
