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

namespace BusinessFacade
{
    //class ReceiptAssetProsessFacade
    //{
    //}
    public class ReceiptAssetProsessFacade
    {
        private Receipt objReceipt;
        private ReceiptDocNo objReceiptDocNo;
        private ArrayList arrReceiptDetail;
        private string strError = string.Empty;
        private int intReceiptID = 0;
        private int intReceiptDocNoID = 0;
        private string bl = string.Empty;
        private string th = string.Empty;
        //iko
        private int intPakaiDocNoID = 0;
        private int intPakaiID = 0;

        private Pakai objPakai;
        private PakaiDocNo objPakaiDocNo;
        private ArrayList arrPakaiDetail;

        private Disposal objDisposal = new Disposal();
        private ArrayList arrObject;
        //iko

        //public ReceiptAssetProsessFacade(Receipt Receipt, ArrayList arrListReceipt, ReceiptDocNo receiptDocNo)
        public ReceiptAssetProsessFacade(Receipt Receipt, ArrayList arrListReceipt, ReceiptDocNo receiptDocNo,
            Pakai Pakai, ArrayList arrListPakaiDetail, PakaiDocNo pakaiDocNo)
        {
            objReceipt = Receipt;
            arrReceiptDetail = arrListReceipt;
            objReceiptDocNo = receiptDocNo;
            //Iko
            objPakai = Pakai;
            arrPakaiDetail = arrListPakaiDetail;
            objPakaiDocNo = pakaiDocNo;
            //Iko
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
            #region nonaktif line
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

                //create no transaksi
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

                //iko insert pakaidocNo
                if (objReceipt.TipeAsset == 4 || objReceipt.TipeAsset == 0) //asset master
                {
                    #region Generate Nomor pakai
                    if (objPakaiDocNo.NoUrut == 1)
                    {
                        PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade(objPakaiDocNo);
                        intPakaiDocNoID = pakaiDocNoFacade.Insert(transManager);
                        if (pakaiDocNoFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return pakaiDocNoFacade.Error;
                        }
                    }
                    else
                    {
                        PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade(objPakaiDocNo);
                        intPakaiDocNoID = pakaiDocNoFacade.Update(transManager);
                        if (pakaiDocNoFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return pakaiDocNoFacade.Error;
                        }
                    }
                    #endregion

                    #region Proses Header Pakai
                    if (objReceipt.TipeAsset == 4)
                    {
                        objPakai.Status = 2;
                    }
                    
                    absTrans = new PakaiFacade(objPakai);
                    intPakaiID = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intPakaiID > 0)
                    {
                        if (objPakaiDocNo.MonthPeriod < 10)
                            bl = "0" + objPakaiDocNo.MonthPeriod.ToString();
                        else
                            bl = objPakaiDocNo.MonthPeriod.ToString();
                        th = objPakaiDocNo.YearPeriod.ToString().Substring(2, 2);

                        objPakai.PakaiNo = objPakaiDocNo.PakaiCode + th + bl + "-" + objPakaiDocNo.NoUrut.ToString().PadLeft(5, '0');
                        objPakai.ID = intPakaiID;

                        //create no
                        absTrans = new PakaiFacade(objPakai);
                        intResult = absTrans.Update(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    #endregion

                    #region Proses Detail Pakai
                    ArrayList arrMtcSarmut = new ArrayList();
                    ArrayList arrProject = new ArrayList();
                    ArrayList arrArmada = new ArrayList();
                    ArrayList arrZona = new ArrayList();

                    if (arrPakaiDetail.Count > 0)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            pakaiDetail.PakaiID = intPakaiID;
                            SaldoInventoryFacade saldoInventory = new SaldoInventoryFacade();
                            ReceiptFacade receiptFacade = new ReceiptFacade();
                            Receipt receipt = new Receipt();

                            /** Ketika pakaiDetail.ItemID di Citeureup terjadi SusPend */
                            //receipt = receiptFacade.GetLastReceipt(pakaiDetail.ItemID);

                            if (pakaiDetail.GroupID == 4)
                            {
                                receipt = receiptFacade.GetLastReceiptAsset(pakaiDetail.ItemID);
                            }
                            else
                            {
                                receipt = receiptFacade.GetLastReceipt(pakaiDetail.IDJenisBiaya);
                            }

                            //receipt = receiptFacade.GetLastReceipt(pakaiDetail.IDJenisBiaya);

                            DateTime lastReceiptDate = receipt.ReceiptDate;
                            /** average price selain biaya di Nol kan 
                             * avgprice akan di isi saat posting average price proses
                             */
                            pakaiDetail.AvgPrice = (pakaiDetail.ItemTypeID == 3) ? pakaiDetail.AvgPrice : 0;
                            //saldoInventory.GetPrice(pakaiDetail.ItemID, GetStrMonth(lastReceiptDate.Month), lastReceiptDate.Year, pakaiDetail.ItemTypeID);

                            absTrans = new PakaiDetailFacade(pakaiDetail);
                            intResult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                            /**
                             * Update Zona maintenance jika sudah aktif
                             */
                            if (pakaiDetail.Zona != "Pilih Zona" || pakaiDetail.Zona != string.Empty)
                            {
                                PakaiDetailFacade pkz = new PakaiDetailFacade();
                                PakaiDetail pk = new PakaiDetail();
                                pk.ID = intResult;
                                pk.Zona = pakaiDetail.Zona;
                                arrZona.Add(pk);
                                //ikut trans manager ya
                                //absTrans = new PakaiDetailFacade(pk);
                                //int rstu = absTrans.UpdateZonaMTC(transManager);
                                //if (absTrans.Error != string.Empty)
                                //{
                                //    transManager.RollbackTransaction();
                                //    return absTrans.Error;
                                //}
                            }
                            //update table budgetatkdetail set aproval=4 and rowsatatus=id status
                            if (pakaiDetail.BudgetID > 0)
                            {
                                ArrayList arr = new ArrayList();
                                BudgetingFacade bg = new BudgetingFacade();
                                bg.Pilihan = "UpdateStatus";
                                bg.Field = "set PakaiDetailID=" + intResult.ToString();
                                bg.Field += ", Approval=4";
                                bg.Criteria = pakaiDetail.BudgetID.ToString();
                                arr = bg.Retrieve();
                            }
                            if (objPakai.PakaiTipe == 5)
                            {
                                #region Proses for Biaya
                                BiayaFacade biayaFacade = new BiayaFacade();
                                Biaya biaya = new Biaya();

                                biaya.ID = pakaiDetail.ItemID;
                                biaya.Jumlah = pakaiDetail.Quantity;

                                intResult = biayaFacade.MinusQty(biaya);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                /**
                                 * Added on 20-06-2014
                                 * Penguraan stock pada item group biaya
                                 */
                                Biaya GroupBiaya = new Biaya();
                                GroupBiaya.ID = pakaiDetail.IDJenisBiaya;
                                GroupBiaya.Jumlah = pakaiDetail.Quantity;
                                intResult = biayaFacade.MinusQty(GroupBiaya);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }

                                #endregion
                            }
                            else if (objPakai.PakaiTipe == 4)
                            {
                                #region Proces for asset
                                AssetFacade assetFacade = new AssetFacade();
                                Asset asset = new Asset();

                                asset.ID = pakaiDetail.ItemID;
                                asset.Jumlah = pakaiDetail.Quantity;

                                intResult = assetFacade.MinusQty(asset);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                else
                                {
                                    Asset cekInvAsset = assetFacade.InvAssetRetrieveByAssetID(pakaiDetail.ItemID, objPakai.DeptID);
                                    if (cekInvAsset.ID > 0)
                                        asset.Flag = 1;
                                    //update by assetid+deptid
                                    else
                                        asset.Flag = 0;
                                    //insert

                                    asset.AssetID = pakaiDetail.ItemID;
                                    asset.Jumlah = pakaiDetail.Quantity;
                                    asset.DeptID = objPakai.DeptID;
                                    asset.Gudang = objPakai.DepoID;
                                    asset.RowStatus = 0;

                                    intResult = assetFacade.InsertUpdateInvAsset(asset);
                                    if (absTrans.Error != string.Empty)
                                    {
                                        transManager.RollbackTransaction();
                                        return absTrans.Error;
                                    }
                                }
                                #endregion
                            }

                            else
                            {
                                #region Update Table Inventory
                                InventoryFacade inventoryFacade = new InventoryFacade();
                                Inventory inventory = new Inventory();

                                inventory.ID = pakaiDetail.ItemID;
                                inventory.Jumlah = pakaiDetail.Quantity;

                                intResult = inventoryFacade.MinusQty(inventory);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                #endregion
                                #region Proses Repack
                                if (objPakai.PakaiTipe == 10)
                                {
                                    Inventory invRePack = inventoryFacade.RepackRetrieveById(pakaiDetail.ItemID);
                                    if (invRePack.ID > 0)
                                    {
                                        // tambah jumlah aja
                                        inventory.ID = pakaiDetail.ItemID;
                                        inventory.Jumlah = pakaiDetail.Quantity;

                                        intResult = inventoryFacade.UpdateQtyForRepack(inventory);
                                        if (absTrans.Error != string.Empty)
                                        {
                                            transManager.RollbackTransaction();
                                            return absTrans.Error;
                                        }
                                    }
                                    else
                                    {
                                        // insert baru & jumlah
                                        inventory.ID = pakaiDetail.ItemID;
                                        inventory.Jumlah = pakaiDetail.Quantity;
                                        inventory.UOMID = pakaiDetail.UomID;
                                        inventory.RowStatus = 0;
                                        inventory.GroupID = pakaiDetail.GroupID;
                                        inventory.Jumlah = pakaiDetail.Quantity;

                                        intResult = inventoryFacade.InsertForRePack(inventory);
                                        if (absTrans.Error != string.Empty)
                                        {
                                            transManager.RollbackTransaction();
                                            return absTrans.Error;
                                        }
                                    }
                                }
                                #endregion
                            }

                            #region Proses SPP Multigudang (Khusus Private)
                            SPPMultiGudang sppm = new SPPMultiGudang();
                            sppm.ItemID = pakaiDetail.ItemID;
                            sppm.GroupID = pakaiDetail.GroupID;
                            sppm.ItemTypeID = pakaiDetail.ItemTypeID;
                            sppm.GudangID = objPakai.DeptID;
                            sppm.QtyPakai = pakaiDetail.Quantity;
                            sppm.CreatedBy = objPakai.CreatedBy;
                            SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade(sppm);
                            intResult = sppmf.Update(transManager);
                            //end update multigudang

                            #endregion
                            #region Collect Data Sarmut (Laporan efesiensi mesin maintenance) dan jadikan ArrayList
                            MTC_Sarmut arrMtc = new MTC_Sarmut();
                            int avgP = saldoInventory.GetPrice(pakaiDetail.ItemID, GetStrMonth(lastReceiptDate.Month), lastReceiptDate.Year, pakaiDetail.ItemTypeID);
                            arrMtc.SarmutID = pakaiDetail.SarmutID;
                            arrMtc.ItemID = pakaiDetail.ItemID;
                            arrMtc.Qty = pakaiDetail.Quantity;
                            arrMtc.DeptID = objPakai.DeptID;
                            arrMtc.DeptCode = pakaiDetail.DeptCode;
                            arrMtc.SPBDate = objPakai.CreatedTime;
                            arrMtc.SPBID = objPakai.PakaiNo;
                            arrMtc.ItemTypeID = objPakai.ItemTypeID;
                            arrMtc.Kelompok = "";
                            arrMtc.AvgPrice = (avgP * pakaiDetail.Quantity);//langsung total price
                            if (pakaiDetail.SarmutID != 0)
                            {
                                arrMtcSarmut.Add(arrMtc);
                            }
                            #endregion
                            #region Collect data project dan jadikan ArrayList
                            MTC_ProjectPakai objProject = new MTC_ProjectPakai();
                            if (pakaiDetail.ProjectID > 0)
                            {
                                objProject.ProjectID = pakaiDetail.ProjectID;
                                objProject.PakaiID = pakaiDetail.PakaiID;
                                objProject.ItemID = pakaiDetail.ItemID;
                                objProject.GroupID = pakaiDetail.GroupID;
                                objProject.ItemTypeID = pakaiDetail.ItemTypeID;
                                objProject.Qty = pakaiDetail.Quantity;
                                objProject.AvgPrice = avgP;
                                objProject.DeptID = objPakai.DeptID;
                                arrProject.Add(objProject);
                            }

                            #endregion
                            #region Collect Data for Perawatan kendaraan
                            MTC_Armada sarmut = new MTC_Armada();
                            sarmut.ItemID = pakaiDetail.ItemID;
                            sarmut.Quantity = pakaiDetail.Quantity;
                            sarmut.AvgPrice = pakaiDetail.AvgPrice;
                            sarmut.DeptID = objPakai.DeptID;
                            sarmut.SPBDate = objPakai.PakaiDate;
                            sarmut.SPBNo = objPakai.PakaiNo;
                            sarmut.IDKendaraan = pakaiDetail.IDKendaraan;
                            sarmut.NoPol = pakaiDetail.NoPol;
                            sarmut.CreatedBy = objPakai.CreatedBy;
                            sarmut.GroupID = pakaiDetail.GroupID;
                            sarmut.ItemTypeID = pakaiDetail.ItemTypeID;

                            if (pakaiDetail.IDKendaraan != 0)
                            {
                                arrArmada.Add(sarmut);
                            }

                            #endregion
                        }
                        //end loop
                        //input ke table sarmut
                        #region proses sarmut
                        MTC_SarmutProcesFacade mtc = new MTC_SarmutProcesFacade(arrMtcSarmut);
                        string mt = mtc.Insert();
                        if (mt != "ok")
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        #endregion
                        #region proses pemakaian project
                        if (arrProject.Count > 0)
                        {
                            MTC_ProjectProsesPakai pro = new MTC_ProjectProsesPakai(arrProject);
                            string proj = pro.Insert();
                            if (proj != "ok")
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                        #endregion
                        #region proses perawatan kendaraan
                        if (arrArmada.Count > 0)
                        {
                            MTC_ProjectProsesPakai pro = new MTC_ProjectProsesPakai(arrArmada);
                            string proj = pro.InsertArmada();
                            if (proj != "ok")
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                //iko


                System.Web.HttpContext.Current.Session["AutoSPB"] = null;
                ArrayList arrData = new ArrayList();

                if (arrReceiptDetail.Count > 0)
                {
                    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    {

                        receiptDetail.ReceiptID = intReceiptID;
                        receiptDetail.TipeAsset = objReceipt.TipeAsset;

                        absTrans = new ReceiptDetailFacade(receiptDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        #region Asset Tunggal
                        if (objReceipt.TipeAsset == 4) //asset master
                        {
                            #region Generate for Asset
                            // for Asset
                            if (objReceipt.ReceiptType == 4)
                            {
                                // utk add ke table asset
                                AssetFacade inventoryFacade = new AssetFacade();
                                Asset asset = new Asset();

                                asset.ID = receiptDetail.ItemID;
                                asset.Jumlah = receiptDetail.Quantity;

                                intResult = inventoryFacade.UpdateQty(asset);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                // until here

                                //iko utk ke AM
                                //cek ke SPPdetail utk data AM
                                POPurchnDetailFacade poDetailFacade = new POPurchnDetailFacade();
                                /** lama **/
                                //POPurchnDetail poDetail = poDetailFacade.RetrieveByID2(receiptDetail.PODetailID); //new add
                                ArrayList arrPODetail = poDetailFacade.RetrieveByID2(receiptDetail.PODetailID);
                                /** lama **/
                                //if (poDetail.SPPDetailID > 0)

                                if (arrPODetail.Count > 0)
                                {
                                    foreach (POPurchnDetail poDetail in arrPODetail)
                                    {
                                        int count = 0;
                                        for (int i2 = 0; i2 < poDetail.Qty; i2++)
                                        {
                                            AssetManagement data = new AssetManagement();
                                            SPPDetailFacade sppDetAMFacade = new SPPDetailFacade();
                                            SPPDetail sppDetAM = sppDetAMFacade.RetrieveBySPPDetailID(poDetail.SPPDetailID);
                                            if (sppDetAM.ID > 0)
                                            {
                                                data.GroupID = sppDetAM.AmGroupID;
                                                data.ClassID = sppDetAM.AmClassID;
                                                data.SubClassID = sppDetAM.AmSubClassID;
                                                data.LokasiID = sppDetAM.AmLokasiID;
                                                data.ItemKode = sppDetAM.ItemCode; //ntar ke asset table atau udah ke kesini juga
                                                data.Deskripsi = sppDetAM.ItemName; //string.Empty; //blank dulu
                                                data.KodeAsset = sppDetAM.ItemCode;
                                                data.TglAsset = objReceipt.ReceiptDate; //ngikut 

                                                POPurchnDetailFacade poDetF = new POPurchnDetailFacade();
                                                POPurchnDetail poDet = poDetF.RetrieveByDetailID(receiptDetail.PODetailID);
                                                if (poDet.ID > 0)
                                                    data.NilaiAsset = poDet.Qty * poDet.Price;

                                                data.UmurAsset = sppDetAM.UmurEkonomis;
                                                data.TglSusut = objReceipt.ReceiptDate; //ngikut 
                                                data.MethodDep = 1;
                                                //data.PicDept = objPakai.DeptID;
                                                //data.PicPerson = objPakai.CreatedBy;
                                                if (sppDetAM.GroupID == 5 || sppDetAM.GroupID == 1 || sppDetAM.GroupID == 2 || sppDetAM.GroupID == 4 || sppDetAM.GroupID == 6)
                                                {
                                                    data.PicDept = 5;
                                                    data.PicPerson = "Mgr-HRD";
                                                }
                                                else
                                                {
                                                    data.PicDept = 3;
                                                    data.PicPerson = "Mgr-MTC";
                                                }

                                                data.CreatedBy = ((Users)System.Web.HttpContext.Current.Session["Users"]).UserName;
                                                data.PlantID = ((Users)System.Web.HttpContext.Current.Session["Users"]).UnitKerjaID;
                                                //data.PlantID = ((Users)Session["Users"]).UnitKerjaID;
                                                data.AssetID = sppDetAM.ItemID;
                                                data.UomID = sppDetAM.UOMID;

                                                AssetManagementFacade dataFacade = new AssetManagementFacade();
                                                intResult = dataFacade.Insert(data, "spInsertAM_Asset");
                                                if (absTrans.Error != string.Empty || intResult < 0)
                                                {
                                                    transManager.RollbackTransaction();
                                                    return absTrans.Error;
                                                }
                                            }
                                            count = count + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    //karena utk data Aset tidak lengkap
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                //iko utk ke AM
                            }
                            #endregion
                            #region Generate for objReceipt
                            // for Biaya
                            if (objReceipt.ReceiptType == 5)
                            {
                                // utk add ke table asset
                                BiayaFacade biayaFacade = new BiayaFacade();
                                Biaya biaya = new Biaya();

                                biaya.ID = receiptDetail.ItemID;
                                biaya.Jumlah = receiptDetail.Quantity;

                                intResult = biayaFacade.UpdateQty(biaya);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                                // update jumlah untuk biaya jika biayanew aktif
                                if (receiptDetail.ItemID2 > 0)
                                {
                                    biaya.ID = receiptDetail.ItemID2;
                                    biaya.Jumlah = receiptDetail.Quantity;
                                    intResult = biayaFacade.UpdateQty(biaya);
                                    if (absTrans.Error != string.Empty)
                                    {
                                        transManager.RollbackTransaction();
                                        return absTrans.Error;
                                    }
                                }
                                /**
                                 * Lakukan proses spb secara otomatis jika :
                                 * Check Box AutoSPB di Check 
                                 * AutoSPBBiaya=1 di PurchnConfig.ini ->Receipt->AutoSPBBiaya
                                 * Parameter kunci : ItemID,Dept Yng SPP,Qty,Status SPB langsung Head Dept
                                 */

                                string AutoSPB = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AutoSPBBiaya", "Receipt");
                                if (AutoSPB == "1")
                                {

                                    ReceiptDetail rd = new ReceiptDetail();
                                    rd.SppID = getDeptFromSPP(receiptDetail.SppID);
                                    rd.ReceiptID = receiptDetail.ID;
                                    rd.ItemID2 = receiptDetail.ItemID2;
                                    rd.ItemID = receiptDetail.ItemID;
                                    rd.Quantity = receiptDetail.Quantity;
                                    arrData.Add(rd);

                                    System.Web.HttpContext.Current.Session["AutoSPB"] = arrData;
                                }
                            }
                            #endregion
                        }

                    }
                        #endregion
                    //selesai

                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        private int getDeptFromSPP(int p)
        {
            SPP spp = new SPPFacade().RetrieveByIdStatus(p);
            Users user = new UsersFacade().RetrieveById(spp.UserID);
            return user.DeptID;
        }

        public string Update()
        {
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
            foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            {
                receiptDetail.ID = rcpDetail.ID;
                receiptDetail.ItemID = rcpDetail.ItemID;
                receiptDetail.Quantity = rcpDetail.Quantity;
                receiptDetail.PoID = rcpDetail.PoID;
                receiptDetail.FlagPO = rcpDetail.FlagPO;
                //kecuali biaya, tdk ke table inventori / asset
                if (objReceipt.ReceiptType == 7)
                    receiptDetail.FlagTipe = 0;
                else
                    receiptDetail.FlagTipe = 2;
            }

            receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);

            intResult = receiptDetailFacade.CancelReceiptDetail(transManager);
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

            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ReceiptDetail receiptDetail = new ReceiptDetail();
            foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            {
                receiptDetail.ID = rcpDetail.ID;
                receiptDetail.ItemID = rcpDetail.ItemID;
                receiptDetail.Quantity = rcpDetail.Quantity;
                receiptDetail.PoID = rcpDetail.PoID;
                receiptDetail.FlagPO = rcpDetail.FlagPO;
                if (objReceipt.ReceiptType == 7)
                    receiptDetail.FlagTipe = 3;
                else
                    receiptDetail.FlagTipe = 2;

                receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);
                intResult = receiptDetailFacade.CancelInventoryByReceiptDetail(transManager);
                if (receiptDetailFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return receiptDetailFacade.Error;
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprove()
        {
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

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

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
        public string GetStrMonth(int monthAvgPrice)
        {
            string strAvgPrice = string.Empty;
            switch (monthAvgPrice)
            {
                case 1:
                    strAvgPrice = "janAvgPrice";
                    break;
                case 2:
                    strAvgPrice = "febAvgPrice";
                    break;
                case 3:
                    strAvgPrice = "marAvgPrice";
                    break;
                case 4:
                    strAvgPrice = "aprAvgPrice";
                    break;
                case 5:
                    strAvgPrice = "meiAvgPrice";
                    break;
                case 6:
                    strAvgPrice = "junAvgPrice";
                    break;
                case 7:
                    strAvgPrice = "julAvgPrice";
                    break;
                case 8:
                    strAvgPrice = "aguAvgPrice";
                    break;
                case 9:
                    strAvgPrice = "sepAvgPrice";
                    break;
                case 10:
                    strAvgPrice = "oktAvgPrice";
                    break;
                case 11:
                    strAvgPrice = "novAvgPrice";
                    break;
                case 12:
                    strAvgPrice = "desAvgPrice";
                    break;
            }
            return strAvgPrice;
        }



    }

}
