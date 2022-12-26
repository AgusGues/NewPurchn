using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;

namespace BusinessFacade
{
    public class PakaiProcessFacade
    {
        private Pakai objPakai;
        private PakaiDocNo objPakaiDocNo;
        private ArrayList arrPakaiDetail;
        
        private string strError = string.Empty;
        private int intPakaiID = 0;
        private int intPakaiDocNoID = 0;
        private string bl = string.Empty;
        private string th = string.Empty;

        public PakaiProcessFacade(Pakai pakai, ArrayList arrListPakai, PakaiDocNo pakaiDocNo)
        {
            objPakai = pakai;
            arrPakaiDetail = arrListPakai;
            objPakaiDocNo = pakaiDocNo;
        }

 
        public string PakaiNo
        {
            get
            {
                return objPakaiDocNo.PakaiCode + th + bl + "-" + objPakaiDocNo.NoUrut.ToString().PadLeft(5, '0');
            }
        }


        public string Insert()
        {
            int intResult = 0;
            ArrayList arrMtcSarmut = new ArrayList();
            ArrayList arrProject = new ArrayList();
            ArrayList arrArmada = new ArrayList();
            ArrayList arrZona = new ArrayList();
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            #region Generate Nomor pakai
                if (objPakaiDocNo.NoUrut == 1)
                {
                    PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade(objPakaiDocNo);
                    intPakaiDocNoID = pakaiDocNoFacade.Insert(transManager);
                    if (pakaiDocNoFacade.Error != string.Empty)
                    {
                        return pakaiDocNoFacade.Error;
                    }
                }
                else
                {
                    PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade(objPakaiDocNo);
                    intPakaiDocNoID = pakaiDocNoFacade.Update(transManager);
                    if (pakaiDocNoFacade.Error != string.Empty)
                    {
                        return pakaiDocNoFacade.Error;
                    }
                }
                //transManager.CommitTransaction();
                //transManager.CloseConnection();
            #endregion
            #region Proses Header Pakai
                //transManager = new TransactionManager(Global.ConnectionString());
                //transManager.BeginTransaction();

                AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
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
            #endregion
            #region Proses Detail Pakai
                
                if (arrPakaiDetail.Count > 0)
                {
                    foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                    {                        
                        pakaiDetail.PakaiID = intPakaiID;
                        SaldoInventoryFacade saldoInventory = new SaldoInventoryFacade();
                        ReceiptFacade receiptFacade = new ReceiptFacade();
                        Receipt receipt = new Receipt();
                        receipt = receiptFacade.GetLastReceipt(pakaiDetail.ItemID);
                        DateTime lastReceiptDate = receipt.ReceiptDate ;

                        /** Beny : utk inputan SPB bahan bantu Flooculant **/
                        ReceiptFacade receiptFacade2 = new ReceiptFacade();
                        Receipt receipt2 = new Receipt();
                        receipt2 = receiptFacade2.CekItemFlooculant(pakaiDetail.ItemID);

                        if (pakaiDetail.GroupID == 1 || (pakaiDetail.GroupID == 2 && receipt2.ItemID == pakaiDetail.ItemID))
                        {
                            pakaiDetail.Press = pakaiDetail.Press;
                        }
                        else
                        {
                            pakaiDetail.Press = "0";
                        }

                        pakaiDetail.Kelompok = (pakaiDetail.Kelompok == null) ? "0" : pakaiDetail.Kelompok;    
                        /** End Beny **/

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
                       if (pakaiDetail.Zona != "Pilih Zona" || pakaiDetail.Zona!=string.Empty)
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
                        else if (objPakai.PakaiTipe == 4 || objPakai.PakaiTipe == 12)
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
                            #region Proses Repack Non GRC add by Razib - 2020-01-24
                            if (objPakai.PakaiTipe == 13)
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
                            int avgP=saldoInventory.GetPrice(pakaiDetail.ItemID, GetStrMonth(lastReceiptDate.Month), lastReceiptDate.Year, pakaiDetail.ItemTypeID);
                            arrMtc.SarmutID = pakaiDetail.SarmutID;
                            arrMtc.ItemID = pakaiDetail.ItemID;
                            arrMtc.Qty = pakaiDetail.Quantity;
                            arrMtc.DeptID = objPakai.DeptID;
                            arrMtc.DeptCode = pakaiDetail.DeptCode;
                            arrMtc.SPBDate = objPakai.CreatedTime;
                            arrMtc.SPBID = objPakai.PakaiNo;
                            arrMtc.ItemTypeID = objPakai.ItemTypeID;
                            arrMtc.AvgPrice = (avgP * pakaiDetail.Quantity);//langsung total price
                            /** Beny **/
                            //arrMtc.Kelompok = pakaiDetail.Kelompok;
                            /** end Beny **/
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
                            sarmut.ItemID       = pakaiDetail.ItemID;
                            sarmut.Quantity     = pakaiDetail.Quantity;
                            sarmut.AvgPrice     = pakaiDetail.AvgPrice;
                            sarmut.DeptID       = objPakai.DeptID;
                            sarmut.SPBDate      = objPakai.PakaiDate;
                            sarmut.SPBNo        = objPakai.PakaiNo;
                            sarmut.IDKendaraan  = pakaiDetail.IDKendaraan;
                            sarmut.NoPol        = pakaiDetail.NoPol;
                            sarmut.CreatedBy    = objPakai.CreatedBy;
                            sarmut.GroupID      = pakaiDetail.GroupID;
                            sarmut.ItemTypeID   = pakaiDetail.ItemTypeID;

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
               
            } 
           #endregion
            transManager.CommitTransaction();
            transManager.CloseConnection();
            if (arrZona.Count > 0)
            {
                foreach (PakaiDetail pk in arrZona)
                {
                    PakaiDetail pkd = new PakaiDetail();
                    pkd.ID = pk.ID;
                    pkd.Zona = pk.Zona;
                    int rst = new PakaiDetailFacade().UpdateZonaMTC(pkd);
                }
            }
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

        public string Update()
        {

            return string.Empty;
        }

        public string CancelPakaiDetail()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
            PakaiDetail pakaiDetail = new PakaiDetail();
            foreach (PakaiDetail pkiDetail in arrPakaiDetail)
            {
                pakaiDetail.ID = pkiDetail.ID;
                pakaiDetail.ItemID = pkiDetail.ItemID;
                pakaiDetail.Quantity = pkiDetail.Quantity;
                //receiptDetail.PoID = rcpDetail.PoID;
                //receiptDetail.FlagPO = rcpDetail.FlagPO;
                #region Tidak dipakai karena tidak sesuai
                //if (objPakai.PakaiTipe == 5)
                //    pakaiDetail.FlagTipe = 2;
                //else if (objPakai.PakaiTipe == 6)
                //    pakaiDetail.FlagTipe = 3;
                //else
                //    pakaiDetail.FlagTipe = 1;
                #endregion
                pakaiDetail.FlagTipe = pkiDetail.ItemTypeID;
                
                /**
                 * Sarmut MTC delete
                 * RowStatus=-1
                 */
                MTC_Sarmut objSarmut = new MTC_Sarmut();
                objSarmut.ID = pakaiDetail.SarmutID;
            }

            pakaiDetailFacade = new PakaiDetailFacade(pakaiDetail);

            intResult = pakaiDetailFacade.CancelPakaiDetail(transManager);
            if (pakaiDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return pakaiDetailFacade.Error;
            }
            #region dinonaktifkan
            //kurangi inventoryasset
            //AssetFacade assetFacade = new AssetFacade();
            //Asset asset = new Asset();

            //asset.ID = pakaiDetail.ItemID;
            //asset.DeptID = objPakai.DeptID;
            //asset.Jumlah = pakaiDetail.Quantity;

            //intResult = assetFacade.MinusQtyInvAsset(asset);
            //if (assetFacade.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return assetFacade.Error;
            //}
            //until here
            #endregion
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelPakai()
        {
            int intResult = 0;
            //string asString = string.Empty;
            objPakai.Status = -1;
            ArrayList arrDelSarmut = new ArrayList();

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
            PakaiFacade pakaiFacade = new PakaiFacade();

            absTrans = new PakaiFacade(objPakai);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
            PakaiDetail pakaiDetail = new PakaiDetail();
            foreach (PakaiDetail pkiDetail in arrPakaiDetail)
            {
                pakaiDetail.ID = pkiDetail.ID;
                pakaiDetail.ItemID = pkiDetail.ItemID;
                pakaiDetail.Quantity = pkiDetail.Quantity;
                
                if (pkiDetail.ItemTypeID == 2) //Asset
                    pakaiDetail.FlagTipe = 2;
                else if (pkiDetail.ItemTypeID == 3) //Biaya
                    pakaiDetail.FlagTipe = 3;
                else
                    pakaiDetail.FlagTipe = 1;

                //else if (objPakai.PakaiTipe == 6) //Project

                pakaiDetailFacade = new PakaiDetailFacade(pakaiDetail);
                intResult = pakaiDetailFacade.CancelInventoryByPakaiDetail(transManager);
                if (pakaiDetailFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return pakaiDetailFacade.Error;
                }
                #region dinonaktifkan
                //kurangin inventoryasset-nya
                //AssetFacade assetFacade = new AssetFacade();
                //Asset asset = new Asset();
                //asset.ID = pkiDetail.ItemID;
                //asset.DeptID = objPakai.DeptID;
                //asset.Jumlah = pkiDetail.Quantity;
                //intResult = assetFacade.MinusQtyInvAsset(asset);
                //if (assetFacade.Error != string.Empty)
                //{
                //    transManager.RollbackTransaction();
                //    return assetFacade.Error;
                //}
                //until here
                #endregion
                /**
                 * sarmut delete
                 */
                MTC_Sarmut objSmt = new MTC_Sarmut();
                objSmt.SarmutID = pakaiDetail.SarmutID;
                objSmt.RowStatus = -1;
                arrDelSarmut.Add(objSmt);
            }

            MTC_SarmutProcesFacade mtc = new MTC_SarmutProcesFacade(arrDelSarmut);
            string mt = mtc.Delete();
            if(mt!="ok")
            {
                return mt;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        
        public string CancelPakaiSarmut(ArrayList ArrSarmut)
        {
            int intResult = 0;
            //string asString = string.Empty;
            objPakai.Status = -1;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
            PakaiFacade pakaiFacade = new PakaiFacade();

            absTrans = new PakaiFacade(objPakai);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
            PakaiDetail pakaiDetail = new PakaiDetail();
            foreach (PakaiDetail pkiDetail in arrPakaiDetail)
            {
                pakaiDetail.ID = pkiDetail.ID;
                pakaiDetail.ItemID = pkiDetail.ItemID;
                pakaiDetail.Quantity = pkiDetail.Quantity;

                if (pkiDetail.ItemTypeID == 2) //Asset
                    pakaiDetail.FlagTipe = 2;
                else if (pkiDetail.ItemTypeID == 3) //Biaya
                    pakaiDetail.FlagTipe = 3;
                else
                    pakaiDetail.FlagTipe = 1;

                //else if (objPakai.PakaiTipe == 6) //Project

                pakaiDetailFacade = new PakaiDetailFacade(pakaiDetail);
                intResult = pakaiDetailFacade.CancelInventoryByPakaiDetail(transManager);
                if (pakaiDetailFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return pakaiDetailFacade.Error;
                }
                
            }
            MTC_SarmutProcesFacade mtc = new MTC_SarmutProcesFacade(ArrSarmut);
            string mt = mtc.Delete();
            if (mt !="ok")
            {
                transManager.RollbackTransaction();
                return mt;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        public string UpdateApprove(int apv)
        {
            Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
            //selai  user dept 10 (logistict) semua approval adalah head
            ArrayList arrDept = new DeptFacade().GetDeptFromHead(user.ID);
            string IDGudangs = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptIDGudang", "SPB");
            int IDGudang = 0;
            int.TryParse(IDGudangs, out IDGudang);
            
           
            apv = (user.DeptID == IDGudang && arrDept.Count == 0) ? 2 : 1;
            int intResult = 0;
            objPakai.Status = apv;// (user.DeptID == 10) ? apv : 1;
            objPakai.ApprovalDate = DateTime.Now;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
            PakaiFacade pakaiFacade = new PakaiFacade();

            absTrans = new PakaiFacade(objPakai);
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

            if (arrPakaiDetail.Count > 0 && apv==2)
            {
                foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                {
                    if (pakaiDetail.ItemTypeID == 1)
                    {
                        // utk minus ke table inventori
                        InventoryFacade inventoryFacade = new InventoryFacade();
                        Inventory inventory = new Inventory();

                        inventory.ID = pakaiDetail.ItemID;
                        inventory.Jumlah = pakaiDetail.Quantity;
                        intResult = inventoryFacade.MinusQtyTransit(inventory);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    else
                    {
                        if (pakaiDetail.ItemTypeID == 2)
                        {
                            // utk minus ke table asset
                            AssetFacade assetFacade = new AssetFacade();
                            Asset asset = new Asset();

                            asset.ID = pakaiDetail.ItemID;
                            asset.Jumlah = pakaiDetail.Quantity;

                            intResult = assetFacade.MinusQtyTransit(asset);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                        else 
                        {
                            // utk minus ke table Biaya
                            BiayaFacade biayaFacade = new BiayaFacade();
                            Biaya biaya = new Biaya();

                            biaya.ID = pakaiDetail.ItemID;
                            biaya.Jumlah = pakaiDetail.Quantity;

                            intResult = biayaFacade.MinusQtyTransit(biaya);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                    }
                }

            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApproveNew(int apv)
        {
            Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
            //selai  user dept 10 (logistict) semua approval adalah head
            ArrayList arrDept = new DeptFacade().GetDeptFromHead(user.ID);
            string IDGudangs = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptIDGudang", "SPB");
            int IDGudang = 0;
            int.TryParse(IDGudangs, out IDGudang);

            int Apv = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "declare @userid int set @userid=" + user.ID + " " +
                "select top 1 apv from (select 1 apv from dept where HeadID=@userid union all select 2 apv from dept where MgrID=@userid) a order by apv desc ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Apv = Convert.ToInt32(sdr["Apv"].ToString());
                }
            }

            if (Apv < 2)
                Apv = (user.DeptID == IDGudang && arrDept.Count == 0) ? 3 : 1;
            else
                Apv = 2;
            //apv = (user.DeptID == IDGudang && arrDept.Count == 0) ? 2 : 1;
            int intResult = 0;
            objPakai.Status = apv;// (user.DeptID == 10) ? apv : 1;
            objPakai.ApprovalDate = DateTime.Now;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
            PakaiFacade pakaiFacade = new PakaiFacade();

            absTrans = new PakaiFacade(objPakai);
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

            if (arrPakaiDetail.Count > 0 && apv == 2)
            {
                foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                {
                    if (pakaiDetail.ItemTypeID == 1)
                    {
                        // utk minus ke table inventori
                        InventoryFacade inventoryFacade = new InventoryFacade();
                        Inventory inventory = new Inventory();

                        inventory.ID = pakaiDetail.ItemID;
                        inventory.Jumlah = pakaiDetail.Quantity;
                        intResult = inventoryFacade.MinusQtyTransit(inventory);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    else
                    {
                        if (pakaiDetail.ItemTypeID == 2)
                        {
                            // utk minus ke table asset
                            AssetFacade assetFacade = new AssetFacade();
                            Asset asset = new Asset();

                            asset.ID = pakaiDetail.ItemID;
                            asset.Jumlah = pakaiDetail.Quantity;

                            intResult = assetFacade.MinusQtyTransit(asset);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                        else
                        {
                            // utk minus ke table Biaya
                            BiayaFacade biayaFacade = new BiayaFacade();
                            Biaya biaya = new Biaya();

                            biaya.ID = pakaiDetail.ItemID;
                            biaya.Jumlah = pakaiDetail.Quantity;

                            intResult = biayaFacade.MinusQtyTransit(biaya);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                    }
                }

            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateReady()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new PakaiFacade(objPakai);
            PakaiFacade pakaiFacade = new PakaiFacade();

            absTrans = new PakaiFacade(objPakai);
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

        public string Delete()
        {
            #region dinonaktifkan
            
            
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new ScheduleFacade(objPakai);
            //intResult = absTrans.Delete(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();
         #endregion
            return string.Empty;
        }
    }
}
