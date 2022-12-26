using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;

namespace BusinessFacade
{
    public class LapHarianProcessFacade
    {
        private LapHarian objLapHarian;
        private ArrayList arrLapBul;
        private string strError = string.Empty;
        //private int intAdjustID = 0;
        public int intBikinID = 0;

        public LapHarianProcessFacade(LapHarian lapBul, ArrayList arrLB)
        {
            objLapHarian = lapBul;
            arrLapBul = arrLB;
        }

        public int BikinanID
        {
            get
            {
                return intBikinID;
            }
        }

        public string Insert()
        {

            return string.Empty;
        }

        public string Update()
        {
            //int intResult = 0;

            return string.Empty;
        }

        public string Delete()
        {
            int intResult = 0;

            //LapHarian lapHarian = new LapHarian();
            //lapHarian.UserID = objLapHarian.UserID;
            //lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new LapHarianFacade(objLapHarian);
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

        public string InsertInventory()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            int thn = 0;
            string bln = string.Empty;
            thn = objLapHarian.DariTgl.Year;

            intBikinID = objLapHarian.BikinID;

            if (objLapHarian.DariTgl.Month - 1 == 0)
            {
                thn = objLapHarian.DariTgl.Year - 1;
                string tahun = thn.ToString("N0");
            }

            LapHarianFacade LapHarianFacade = new LapHarianFacade();
            if (arrLapBul.Count > 0)
            {
                foreach (Inventory inv in arrLapBul)
                {
                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = inv.ID;
                    lapHarian.ItemCode = inv.ItemCode;
                    lapHarian.ItemName = inv.ItemName;
                    lapHarian.UomID = inv.UOMID;
                    lapHarian.GroupID = inv.GroupID;
                    lapHarian.DeptID = inv.DeptID;

                    UOMFacade uomFacade = new UOMFacade();
                    UOM uom = uomFacade.RetrieveByID(inv.UOMID);
                    if (uomFacade.Error == string.Empty && uom.ID > 0)
                        lapHarian.UomCode = uom.UOMCode;
                    else
                        lapHarian.UomCode = "";

                    DeptFacade deptFacade = new DeptFacade();
                    Dept dept = deptFacade.RetrieveById(inv.DeptID);
                    if (deptFacade.Error == string.Empty && dept.ID > 0)
                        lapHarian.DeptCode = dept.DeptCode;
                    else
                        lapHarian.DeptCode = "";

                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
                    lapHarian.Urutan = 1;
                    lapHarian.NoDoc = "";
                    
                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
                    SaldoInventory saldoInventory = saldoInventoryFacade.RetrieveByItemID(inv.ID, thn, 1);
                    if (saldoInventoryFacade.Error == string.Empty && saldoInventory.ItemID > 0)
                    {
                        if (objLapHarian.DariTgl.Month - 1 == 0)
                            lapHarian.StokAwal = saldoInventory.DesQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 1)
                            lapHarian.StokAwal = saldoInventory.JanQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 2)
                            lapHarian.StokAwal = saldoInventory.FebQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 3)
                            lapHarian.StokAwal = saldoInventory.MarQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 4)
                            lapHarian.StokAwal = saldoInventory.AprQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 5)
                            lapHarian.StokAwal = saldoInventory.MeiQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 6)
                            lapHarian.StokAwal = saldoInventory.JunQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 7)
                            lapHarian.StokAwal = saldoInventory.JulQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 8)
                            lapHarian.StokAwal = saldoInventory.AguQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 9)
                            lapHarian.StokAwal = saldoInventory.SepQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 10)
                            lapHarian.StokAwal = saldoInventory.OktQty;
                        else if (objLapHarian.DariTgl.Month - 1 == 11)
                            lapHarian.StokAwal = saldoInventory.NovQty;

                    }

                    LapHarianFacade = new LapHarianFacade(lapHarian);
                    intResult = LapHarianFacade.Insert(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }
                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertReceipt()
        {
            int intResult = 0;
            string cek;
            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {

                foreach (Receipt receipt in arrLapBul)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    if (receipt.ItemTypeID == 2)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(receipt.ID);
                    else if (receipt.ItemTypeID == 3)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receipt.ID);
                    else
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receipt.ID);

                    intBikinID = objLapHarian.BikinID;

                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            //cek dulu StokAkhir berapa ?
                            //
                            if (receiptDetail.ItemCode.Trim() == "SP-GA-90137")
                            {
                                cek = "test";
                            }
                            lapHarian.ItemID = receiptDetail.ItemID;
                            lapHarian.ItemCode = receiptDetail.ItemCode;
                            lapHarian.ItemName = receiptDetail.ItemName;
                            lapHarian.UomID = receiptDetail.UomID;
                            lapHarian.UomCode = receiptDetail.UOMCode;
                            lapHarian.StokAwal = 0;
                            lapHarian.GroupID = receiptDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.Pemasukan = receiptDetail.Quantity;
                            lapHarian.Pemakaian = 0;
                            lapHarian.Retur = 0;
                            lapHarian.AdjustTambah = 0;
                            lapHarian.AdjustKurang = 0;
                            lapHarian.Urutan = 2;
                            lapHarian.NoDoc = receipt.ReceiptNo+"-M";
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

                            intBikinID = intBikinID + 1;
                            lapHarian.BikinID = intBikinID;

                            LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(receiptDetail.ItemID, objLapHarian.UserID,receiptDetail.GroupID);
                            if (cekStokAkhir.StokAkhir > 0)
                                lapHarian.StokAkhir = cekStokAkhir.StokAkhir;

                            lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.AdjustTambah + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.AdjustKurang);

                            if (receiptDetail.GroupID == objLapHarian.GroupID)
                            {
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();

                                LapHarianFacade = new LapHarianFacade(lapHarian);

                                intResult = LapHarianFacade.InsertReceipt(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                                transManager.CommitTransaction();
                                transManager.CloseConnection();

                            }
                        }
                    }

                }
            }
            return string.Empty;
        }

        public string InsertConvertan()
        {
            int intResult = 0;

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                //foreach (Receipt receipt in arrLapBul)
                foreach (Convertan convertan in arrLapBul)
                {

                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = convertan.ToItemID;
                    lapHarian.ItemCode = convertan.ToItemCode;
                    lapHarian.ItemName = convertan.ToItemName;
                    lapHarian.UomID = convertan.ToUomID;
                    lapHarian.UomCode = "";     // convertan.UOMCode;
                    lapHarian.StokAwal = 0;
                    lapHarian.GroupID = objLapHarian.GroupID;
                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.Pemasukan = convertan.ToQty;
                    lapHarian.Pemakaian = 0;
                    lapHarian.Retur = 0;
                    lapHarian.AdjustTambah = 0;
                    lapHarian.AdjustKurang = 0;
                    lapHarian.Urutan = 3;
                    lapHarian.NoDoc = convertan.RepackNo + "-M";
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(convertan.ToItemID, objLapHarian.UserID, objLapHarian.GroupID);
                    if (cekStokAkhir.StokAkhir > 0)
                        lapHarian.StokAkhir = cekStokAkhir.StokAkhir;

                    lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.AdjustTambah + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.AdjustKurang);

                    //if (receiptDetail.GroupID == objLapHarian.GroupID)
                    //{
                    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                    transManager.BeginTransaction();

                    LapHarianFacade = new LapHarianFacade(lapHarian);

                    intResult = LapHarianFacade.InsertConvertan(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }
                    transManager.CommitTransaction();
                    transManager.CloseConnection();

                    //}
                }

            }
            return string.Empty;
        }

        public string InsertPakai()
        {
            int intResult = 0;
            Users user = (Users)HttpContext.Current.Session["Users"];
            int CompanyID = user.UnitKerjaID;


            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Pakai pakai in arrLapBul)
                {
                    ArrayList arrPakaiDetail = new ArrayList();
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    if (pakai.ItemTypeID == 2)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForAsset(pakai.ID);
                    else if (pakai.ItemTypeID == 3)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBiaya(pakai.ID);
                    else
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);

                    intBikinID = objLapHarian.BikinID;

                    if (pakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = pakaiDetail.ItemID;
                            lapHarian.ItemCode = pakaiDetail.ItemCode;
                            lapHarian.ItemName = pakaiDetail.ItemName;
                            lapHarian.UomID = pakaiDetail.UomID;
                            lapHarian.UomCode = pakaiDetail.UOMCode;
                            lapHarian.StokAwal = 0;
                            lapHarian.GroupID = pakaiDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.Pemakaian = pakaiDetail.Quantity;
                            lapHarian.Pemasukan = 0;
                            lapHarian.Retur = 0;
                            lapHarian.AdjustTambah = 0;
                            lapHarian.AdjustKurang = 0;
                            lapHarian.DeptID = pakai.DeptID;
                            lapHarian.DeptCode = pakai.DeptCode;
                            lapHarian.NoDoc = pakai.PakaiNo;
                            lapHarian.Urutan = 5;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

                            intBikinID = intBikinID + 1;
                            lapHarian.BikinID = intBikinID;

                            LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(pakaiDetail.ItemID, objLapHarian.UserID, pakaiDetail.GroupID);
                            if (cekStokAkhir.StokAkhir > 0)
                                lapHarian.StokAkhir = cekStokAkhir.StokAkhir;

                            lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.AdjustTambah + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.AdjustKurang);

                            if (pakaiDetail.GroupID == objLapHarian.GroupID)
                            {
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();

                                LapHarianFacade = new LapHarianFacade(lapHarian);

                                intResult = LapHarianFacade.InsertPakai(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                                transManager.CommitTransaction();
                                transManager.CloseConnection();

                            }
                        }
                    }

                }

            }
            return string.Empty;
        }

        public string InsertReturPakai()
        {
            int intResult = 0;

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (ReturPakai returPakai in arrLapBul)
                {
                    ArrayList arrReturPakaiDetail = new ArrayList();
                    ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                    if (returPakai.ItemTypeID == 2)
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdForAsset(returPakai.ID);
                    else if (returPakai.ItemTypeID == 3)
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdForBiaya(returPakai.ID);
                    else
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturId(returPakai.ID);

                    intBikinID = objLapHarian.BikinID;

                    if (returPakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (ReturPakaiDetail returPakaiDetail in arrReturPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = returPakaiDetail.ItemID;
                            lapHarian.ItemCode = returPakaiDetail.ItemCode;
                            lapHarian.ItemName = returPakaiDetail.ItemName;
                            lapHarian.UomID = returPakaiDetail.UomID;
                            lapHarian.UomCode = returPakaiDetail.UOMCode;
                            lapHarian.StokAwal = 0;
                            lapHarian.GroupID = returPakaiDetail.GroupID;
                            lapHarian.Retur = returPakaiDetail.Quantity;
                            lapHarian.Pemasukan = 0;
                            lapHarian.Pemakaian = 0;
                            lapHarian.AdjustTambah = 0;
                            lapHarian.AdjustKurang = 0;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.DeptID = objLapHarian.DeptID;
                            lapHarian.DeptCode = objLapHarian.DeptCode;
                            lapHarian.Urutan = 4;
                            lapHarian.NoDoc = returPakai.ReturNo + "-R";
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

                            intBikinID = intBikinID + 1;
                            lapHarian.BikinID = intBikinID;

                            LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(returPakaiDetail.ItemID, objLapHarian.UserID,returPakaiDetail.GroupID);
                            if (cekStokAkhir.StokAkhir > 0)
                                lapHarian.StokAkhir = cekStokAkhir.StokAkhir;

                            lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.AdjustTambah + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.AdjustKurang);

                            if (returPakaiDetail.GroupID == objLapHarian.GroupID)
                            {
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();

                                LapHarianFacade = new LapHarianFacade(lapHarian);
                                intResult = LapHarianFacade.InsertReturPakai(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }

                                transManager.CommitTransaction();
                                transManager.CloseConnection();
                            }
                        }
                    }

                }
            }
            return string.Empty;
        }

        public string InsertAdjust()
        {
            int intResult = 0;

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Adjust adjust in arrLapBul)
                {
                    ArrayList arrAdjustDetail = new ArrayList();
                    AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
                    if (adjust.ItemTypeID == 2)
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdForAsset(adjust.ID);
                    else if (adjust.ItemTypeID == 3)
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdForBiaya(adjust.ID);
                    else
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustId(adjust.ID);

                    intBikinID = objLapHarian.BikinID;

                    if (adjustDetailFacade.Error == string.Empty)
                    {
                        foreach (AdjustDetail adjustDetail in arrAdjustDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = adjustDetail.ItemID;
                            lapHarian.ItemCode = adjustDetail.ItemCode;
                            lapHarian.ItemName = adjustDetail.ItemName;
                            lapHarian.UomID = adjustDetail.UomID;
                            lapHarian.UomCode = adjustDetail.UOMCode;
                            lapHarian.StokAwal = 0;
                            lapHarian.GroupID = adjustDetail.GroupID;
                            lapHarian.AdjustType = adjust.AdjustType.Trim();
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.DeptID = objLapHarian.DeptID;
                            lapHarian.DeptCode = objLapHarian.DeptCode;
                            lapHarian.Penyesuaian = adjustDetail.Quantity;
                            lapHarian.Retur = 0;
                            lapHarian.Pemasukan = 0;
                            lapHarian.Pemakaian = 0;
                            lapHarian.AdjustTambah = 0;
                            lapHarian.AdjustKurang = 0;
                            lapHarian.NoDoc = adjust.AdjustNo;
                            lapHarian.Urutan = 6;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.KodeLaporan = objLapHarian.KodeLaporan;

                            
                            intBikinID = intBikinID + 1;
                            lapHarian.BikinID = intBikinID;

                            LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(adjustDetail.ItemID, objLapHarian.UserID,adjustDetail.GroupID);
                            //LapHarian cekStokAkhir = LapHarianFacade.RetrieveForCekStokAkhir(adjustDetail.ItemID, 17, objLapHarian.TglCetak, adjustDetail.GroupID);
                            if (cekStokAkhir.StokAkhir > 0)
                                lapHarian.StokAkhir = cekStokAkhir.StokAkhir;
                            if (adjust.AdjustType == "Tambah")
                                lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.Penyesuaian + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.AdjustKurang);
                            else
                                lapHarian.StokAkhir = (lapHarian.StokAkhir + lapHarian.Pemasukan + lapHarian.AdjustTambah + lapHarian.Retur) - (lapHarian.Pemakaian + lapHarian.Penyesuaian);

                            if (adjustDetail.GroupID == objLapHarian.GroupID)
                            {
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();

                                LapHarianFacade = new LapHarianFacade(lapHarian);

                                intResult = LapHarianFacade.InsertAdjust(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }

                                transManager.CommitTransaction();
                                transManager.CloseConnection();
                            }
                        }
                    }

                }
            }
            return string.Empty;
        }

        public string InsertPakaiBakuBantu()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Pakai pakai in arrLapBul)
                {
                    ArrayList arrPakaiDetail = new ArrayList();
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBakuBantu(pakai.ID);

                    if (pakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = pakaiDetail.ItemID;
                            lapHarian.GroupID = pakaiDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.Pemakaian = pakaiDetail.Quantity;
                            lapHarian.DeptID = pakai.DeptID;
                            lapHarian.DeptCode = pakai.DeptCode;
                            lapHarian.NoDoc = pakai.PakaiNo;
                            lapHarian.Urutan = 1;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdatePakai(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertPakaiKeAwal()
        {
            int intResult = 0;
            Users user = (Users)HttpContext.Current.Session["Users"];
            int CompanyID = user.UnitKerjaID;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Pakai pakai in arrLapBul)
                {
                    ArrayList arrPakaiDetail = new ArrayList();
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    if (pakai.ItemTypeID == 2)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForAsset(pakai.ID);
                    else if (pakai.ItemTypeID == 3)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBiaya(pakai.ID);
                    else
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);

                    if (pakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = pakaiDetail.ItemID;
                            lapHarian.GroupID = pakaiDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.StokAwal = pakaiDetail.Quantity;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;
                            lapHarian.AdjustType = "Kurang";
                            lapHarian.NoDoc = "";

                            if (pakaiDetail.GroupID == objLapHarian.GroupID)
                            {
                                LapHarianFacade = new LapHarianFacade(lapHarian);
                                intResult = LapHarianFacade.UpdatePakaiKeAwal(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertPakaiBakuBantuKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Pakai pakai in arrLapBul)
                {
                    ArrayList arrPakaiDetail = new ArrayList();
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBakuBantu(pakai.ID);

                    if (pakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = pakaiDetail.ItemID;
                            lapHarian.GroupID = pakaiDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.StokAwal = pakaiDetail.Quantity;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdatePakaiKeAwal(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertReceiptBakuBantu()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Receipt receipt in arrLapBul)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdBakuBantu(receipt.ID);

                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = receiptDetail.ItemID;
                            lapHarian.GroupID = receiptDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.Pemasukan = receiptDetail.Quantity;
                            lapHarian.NoDoc = receipt.ReceiptNo;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateReceipt(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }
        public string InsertReceiptBakuBantuKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Receipt receipt in arrLapBul)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdBakuBantu(receipt.ID);

                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = receiptDetail.ItemID;
                            lapHarian.GroupID = receiptDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.StokAwal = receiptDetail.Quantity;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateReceiptKeAwal(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }
        public string InsertReceiptKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Receipt receipt in arrLapBul)
                {
                    ArrayList arrReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    if (receipt.ItemTypeID == 2)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(receipt.ID);
                    else if (receipt.ItemTypeID == 3)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receipt.ID);
                    else
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receipt.ID);

                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = receiptDetail.ItemID;
                            lapHarian.GroupID = receiptDetail.GroupID;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.StokAwal = receiptDetail.Quantity;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;
                            lapHarian.AdjustType = "Tambah";
                            lapHarian.NoDoc = "";

                            if (receiptDetail.GroupID == objLapHarian.GroupID)
                            {
                                LapHarianFacade = new LapHarianFacade(lapHarian);
                                intResult = LapHarianFacade.UpdateReceiptKeAwal(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertReturPakaiBakuBantu()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (ReturPakai returPakai in arrLapBul)
                {
                    ArrayList arrReturPakaiDetail = new ArrayList();
                    ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                    arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdBakuBantu(returPakai.ID);

                    if (returPakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (ReturPakaiDetail returPakaiDetail in arrReturPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = returPakaiDetail.ItemID;
                            lapHarian.GroupID = returPakaiDetail.GroupID;
                            lapHarian.Retur = returPakaiDetail.Quantity;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.DeptID = objLapHarian.DeptID;
                            lapHarian.DeptCode = objLapHarian.DeptCode;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateReturPakai(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }
        public string InsertReturPakaiBakuBantuKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (ReturPakai returPakai in arrLapBul)
                {
                    ArrayList arrReturPakaiDetail = new ArrayList();
                    ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                    arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdBakuBantu(returPakai.ID);

                    if (returPakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (ReturPakaiDetail returPakaiDetail in arrReturPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = returPakaiDetail.ItemID;
                            lapHarian.GroupID = returPakaiDetail.GroupID;
                            lapHarian.StokAwal = returPakaiDetail.Quantity;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.TglCetak = objLapHarian.TglCetak;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateReturPakaiKeAwal(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }
        public string InsertReturPakaiKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (ReturPakai returPakai in arrLapBul)
                {
                    ArrayList arrReturPakaiDetail = new ArrayList();
                    ReturPakaiDetailFacade returPakaiDetailFacade = new ReturPakaiDetailFacade();
                    if (returPakai.ItemTypeID == 2)
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdForAsset(returPakai.ID);
                    else if (returPakai.ItemTypeID == 3)
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturIdForBiaya(returPakai.ID);
                    else
                        arrReturPakaiDetail = returPakaiDetailFacade.RetrieveByReturId(returPakai.ID);

                    if (returPakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (ReturPakaiDetail returPakaiDetail in arrReturPakaiDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = returPakaiDetail.ItemID;
                            lapHarian.GroupID = returPakaiDetail.GroupID;
                            lapHarian.StokAwal = returPakaiDetail.Quantity;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;
                            lapHarian.AdjustType = "Tambah";
                            lapHarian.NoDoc = "";

                            if (returPakaiDetail.GroupID == objLapHarian.GroupID)
                            {
                                LapHarianFacade = new LapHarianFacade(lapHarian);
                                intResult = LapHarianFacade.UpdateReturPakaiKeAwal(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertAdjustBakuBantu()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Adjust adjust in arrLapBul)
                {
                    ArrayList arrAdjustDetail = new ArrayList();
                    AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
                    arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdBakuBantu(adjust.ID);

                    if (adjustDetailFacade.Error == string.Empty)
                    {
                        foreach (AdjustDetail adjustDetail in arrAdjustDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = adjustDetail.ItemID;
                            lapHarian.GroupID = adjustDetail.GroupID;
                            lapHarian.Penyesuaian = adjustDetail.Quantity;
                            lapHarian.AdjustType = adjust.AdjustType.Trim();
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.DeptID = objLapHarian.DeptID;
                            lapHarian.DeptCode = objLapHarian.DeptCode;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateAdjust(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }
        public string InsertAdjustBakuBantuKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Adjust adjust in arrLapBul)
                {
                    ArrayList arrAdjustDetail = new ArrayList();
                    AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
                    arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdBakuBantu(adjust.ID);

                    if (adjustDetailFacade.Error == string.Empty)
                    {
                        foreach (AdjustDetail adjustDetail in arrAdjustDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = adjustDetail.ItemID;
                            lapHarian.GroupID = adjustDetail.GroupID;
                            lapHarian.StokAwal = adjustDetail.Quantity;
                            lapHarian.AdjustType = adjust.AdjustType.Trim();
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.DeptID = objLapHarian.DeptID;
                            lapHarian.DeptCode = objLapHarian.DeptCode;

                            LapHarianFacade = new LapHarianFacade(lapHarian);
                            intResult = LapHarianFacade.UpdateAdjustKeAwal(transManager);
                            if (LapHarianFacade.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return LapHarianFacade.Error;
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

        public string InsertSaldoInventory1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            //
            SaldoInventoryFacade SIFacade = new SaldoInventoryFacade();
            ArrayList arrSaldoInventory = SIFacade.RetrieveSaldoInventory1(ketBlnLalu, yearPeriod, tglAwal, tglAkhir, tgl1, tgl2, groupID);

            int thn = 0;
            string bln = string.Empty;
            thn = objLapHarian.DariTgl.Year;

            intBikinID = objLapHarian.BikinID;

            if (objLapHarian.DariTgl.Month - 1 == 0)
            {
                thn = objLapHarian.DariTgl.Year - 1;
                string tahun = thn.ToString("N0");
            }

            LapHarianFacade LapHarianFacade = new LapHarianFacade();
            if (arrSaldoInventory.Count > 0)
            {
                foreach (SaldoInventory si in arrSaldoInventory)
                {
                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = si.ItemID ;
                    lapHarian.ItemCode = si.ItemCode;
                    lapHarian.ItemName = si.ItemName;
                    lapHarian.UomID = si.UomID;
                    lapHarian.UomCode = si.UOMCode;

                    lapHarian.GroupID = groupID;
                    lapHarian.DeptID = 0;
                    lapHarian.DeptCode = "";

                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    lapHarian.Urutan = 1;
                    lapHarian.NoDoc = "";
                    lapHarian.StokAwal = si.StokAwal;

                    LapHarianFacade = new LapHarianFacade(lapHarian);
                    intResult = LapHarianFacade.Insert(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }

                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string InsertSaldoInventory(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            //
            SaldoInventoryFacade SIFacade = new SaldoInventoryFacade();
            ArrayList arrSaldoInventory = SIFacade.RetrieveSaldoInventory(ketBlnLalu, yearPeriod, tglAwal, tglAkhir, tgl1, tgl2, groupID);

            int thn = 0;
            string bln = string.Empty;
            thn = objLapHarian.DariTgl.Year;

            intBikinID = objLapHarian.BikinID;

            if (objLapHarian.DariTgl.Month - 1 == 0)
            {
                thn = objLapHarian.DariTgl.Year - 1;
                string tahun = thn.ToString("N0");
            }

            LapHarianFacade LapHarianFacade = new LapHarianFacade();
            if (arrSaldoInventory.Count > 0)
            {
                foreach (SaldoInventory si in arrSaldoInventory)
                {
                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = si.ItemID;
                    lapHarian.ItemCode = si.ItemCode;
                    lapHarian.ItemName = si.ItemName;
                    lapHarian.UomID = si.UomID;
                    lapHarian.UomCode = si.UOMCode;

                    lapHarian.GroupID = groupID;
                    lapHarian.DeptID = 0;
                    lapHarian.DeptCode = "";

                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    lapHarian.Urutan = 1;
                    lapHarian.NoDoc = "";
                    lapHarian.StokAwal = si.StokAwal;

                    LapHarianFacade = new LapHarianFacade(lapHarian);
                    intResult = LapHarianFacade.Insert(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }

                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            //

            //if (arrLapBul.Count > 0)
            //{
            //    foreach (Inventory inv in arrLapBul)
            //    {
            //        LapHarian lapHarian = new LapHarian();

            //        lapHarian.ItemID = inv.ID;
            //        lapHarian.ItemCode = inv.ItemCode;
            //        lapHarian.ItemName = inv.ItemName;
            //        lapHarian.UomID = inv.UOMID;
            //        lapHarian.GroupID = inv.GroupID;
            //        lapHarian.DeptID = inv.DeptID;

            //        UOMFacade uomFacade = new UOMFacade();
            //        UOM uom = uomFacade.RetrieveByID(inv.UOMID);
            //        if (uomFacade.Error == string.Empty && uom.ID > 0)
            //            lapHarian.UomCode = uom.UOMCode;
            //        else
            //            lapHarian.UomCode = "";

            //        DeptFacade deptFacade = new DeptFacade();
            //        Dept dept = deptFacade.RetrieveById(inv.DeptID);
            //        if (deptFacade.Error == string.Empty && dept.ID > 0)
            //            lapHarian.DeptCode = dept.DeptCode;
            //        else
            //            lapHarian.DeptCode = "";

            //        lapHarian.UserID = objLapHarian.UserID;
            //        lapHarian.TglCetak = objLapHarian.TglCetak;
            //        lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
            //        intBikinID = intBikinID + 1;
            //        lapHarian.BikinID = intBikinID;

            //        lapHarian.Urutan = 1;
            //        lapHarian.NoDoc = "";


            //        SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            //        SaldoInventory saldoInventory = saldoInventoryFacade.RetrieveByItemID(inv.ID, thn, 1);
            //        if (saldoInventoryFacade.Error == string.Empty && saldoInventory.ItemID > 0)
            //        {
            //            if (objLapHarian.DariTgl.Month - 1 == 0)
            //                lapHarian.StokAwal = saldoInventory.DesQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 1)
            //                lapHarian.StokAwal = saldoInventory.JanQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 2)
            //                lapHarian.StokAwal = saldoInventory.FebQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 3)
            //                lapHarian.StokAwal = saldoInventory.MarQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 4)
            //                lapHarian.StokAwal = saldoInventory.AprQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 5)
            //                lapHarian.StokAwal = saldoInventory.MeiQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 6)
            //                lapHarian.StokAwal = saldoInventory.JunQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 7)
            //                lapHarian.StokAwal = saldoInventory.JulQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 8)
            //                lapHarian.StokAwal = saldoInventory.AguQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 9)
            //                lapHarian.StokAwal = saldoInventory.SepQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 10)
            //                lapHarian.StokAwal = saldoInventory.OktQty;
            //            else if (objLapHarian.DariTgl.Month - 1 == 11)
            //                lapHarian.StokAwal = saldoInventory.NovQty;

            //        }

            //        LapHarianFacade = new LapHarianFacade(lapHarian);
            //        intResult = LapHarianFacade.Insert(transManager);
            //        if (LapHarianFacade.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return LapHarianFacade.Error;
            //        }
            //    }


            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();
            //}

            return string.Empty;
        }

        public string InsertSaldoInventoryRePack1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            //
            SaldoInventoryFacade SIFacade = new SaldoInventoryFacade();
            ArrayList arrSaldoInventory = SIFacade.RetrieveSaldoInventoryRePack1(ketBlnLalu, yearPeriod, tglAwal, tglAkhir, tgl1, tgl2, groupID);

            int thn = 0;
            string bln = string.Empty;
            thn = objLapHarian.DariTgl.Year;

            intBikinID = objLapHarian.BikinID;

            if (objLapHarian.DariTgl.Month - 1 == 0)
            {
                thn = objLapHarian.DariTgl.Year - 1;
                string tahun = thn.ToString("N0");
            }

            LapHarianFacade LapHarianFacade = new LapHarianFacade();
            if (arrSaldoInventory.Count > 0)
            {
                foreach (SaldoInventory si in arrSaldoInventory)
                {
                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = si.ItemID ;
                    lapHarian.ItemCode = si.ItemCode;
                    lapHarian.ItemName = si.ItemName;
                    lapHarian.UomID = si.UomID;
                    lapHarian.UomCode = si.UOMCode;

                    lapHarian.GroupID = groupID;
                    lapHarian.DeptID = 0;
                    lapHarian.DeptCode = "";

                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    lapHarian.Urutan = 1;
                    lapHarian.NoDoc = "";
                    lapHarian.StokAwal = si.StokAwal;

                    LapHarianFacade = new LapHarianFacade(lapHarian);
                    intResult = LapHarianFacade.Insert(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }

                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string InsertSaldoInventoryRePack(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            //
            SaldoInventoryFacade SIFacade = new SaldoInventoryFacade();
            ArrayList arrSaldoInventory = SIFacade.RetrieveSaldoInventoryRePack(ketBlnLalu, yearPeriod, tglAwal, tglAkhir, tgl1, tgl2, groupID);

            int thn = 0;
            string bln = string.Empty;
            thn = objLapHarian.DariTgl.Year;

            intBikinID = objLapHarian.BikinID;

            if (objLapHarian.DariTgl.Month - 1 == 0)
            {
                thn = objLapHarian.DariTgl.Year - 1;
                string tahun = thn.ToString("N0");
            }

            LapHarianFacade LapHarianFacade = new LapHarianFacade();
            if (arrSaldoInventory.Count > 0)
            {
                foreach (SaldoInventory si in arrSaldoInventory)
                {
                    LapHarian lapHarian = new LapHarian();

                    lapHarian.ItemID = si.ItemID;
                    lapHarian.ItemCode = si.ItemCode;
                    lapHarian.ItemName = si.ItemName;
                    lapHarian.UomID = si.UomID;
                    lapHarian.UomCode = si.UOMCode;

                    lapHarian.GroupID = groupID;
                    lapHarian.DeptID = 0;
                    lapHarian.DeptCode = "";

                    lapHarian.UserID = objLapHarian.UserID;
                    lapHarian.TglCetak = objLapHarian.TglCetak;
                    lapHarian.KodeLaporan = objLapHarian.KodeLaporan;
                    intBikinID = intBikinID + 1;
                    lapHarian.BikinID = intBikinID;

                    lapHarian.Urutan = 1;
                    lapHarian.NoDoc = "";
                    lapHarian.StokAwal = si.StokAwal;

                    LapHarianFacade = new LapHarianFacade(lapHarian);
                    intResult = LapHarianFacade.Insert(transManager);
                    if (LapHarianFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return LapHarianFacade.Error;
                    }

                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string InsertAdjustKeAwal()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            LapHarianFacade LapHarianFacade = new LapHarianFacade();

            if (arrLapBul.Count > 0)
            {
                foreach (Adjust adjust in arrLapBul)
                {
                    ArrayList arrAdjustDetail = new ArrayList();
                    AdjustDetailFacade adjustDetailFacade = new AdjustDetailFacade();
                    if (adjust.ItemTypeID == 2)
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdForAsset(adjust.ID);
                    else if (adjust.ItemTypeID == 3)
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustIdForBiaya(adjust.ID);
                    else
                        arrAdjustDetail = adjustDetailFacade.RetrieveByAdjustId(adjust.ID);

                    if (adjustDetailFacade.Error == string.Empty)
                    {
                        foreach (AdjustDetail adjustDetail in arrAdjustDetail)
                        {
                            LapHarian lapHarian = new LapHarian();

                            lapHarian.ItemID = adjustDetail.ItemID;
                            lapHarian.GroupID = adjustDetail.GroupID;
                            lapHarian.StokAwal = adjustDetail.Quantity;
                            lapHarian.UserID = objLapHarian.UserID;
                            lapHarian.TglCetak = objLapHarian.TglCetak;
                            lapHarian.Urutan = 1;
                            lapHarian.AdjustType = adjust.AdjustType.Trim();
                            lapHarian.NoDoc = "";

                            if (adjustDetail.GroupID == objLapHarian.GroupID)
                            {
                                LapHarianFacade = new LapHarianFacade(lapHarian);
                                intResult = LapHarianFacade.UpdateAdjustKeAwal(transManager);
                                if (LapHarianFacade.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return LapHarianFacade.Error;
                                }
                            }
                        }
                    }

                }

                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return string.Empty;
        }

    }
}
