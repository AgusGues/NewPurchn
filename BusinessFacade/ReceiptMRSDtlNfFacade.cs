using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;


namespace BusinessFacade
{
    public class ReceiptMRSDtlNfFacade : AbstractTransactionFacade
    {
        private ReceiptMRSNf.ParamDtl obj = new ReceiptMRSNf.ParamDtl();
        private List<SqlParameter> sqlListParam;
        public ReceiptMRSDtlNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (ReceiptMRSNf.ParamDtl)objDomain;
        }

        public void updateAveragePrice(object objreceiptDetail, int bulan, int tahun)
        {
            int intResult = 0;
            ReceiptDetail receiptDetail = new ReceiptDetail();
            receiptDetail = (ReceiptDetail)objreceiptDetail;
            InventoryFacade inventoryFacade = new InventoryFacade();
            Inventory inventory = new Inventory();
            //update average price
            double LastPrice = inventoryFacade.GetLastPrice(receiptDetail.ItemID, receiptDetail.ItemTypeID);
            double Price = Convert.ToDouble(receiptDetail.Price);
            double EndStock = inventoryFacade.GetStock(receiptDetail.ItemID, receiptDetail.ItemTypeID);
            double QtyBeli = Convert.ToDouble(receiptDetail.Quantity);
            double AvgPrice = ((EndStock * LastPrice) + (QtyBeli * Price)) / (EndStock + QtyBeli);
            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            SaldoInventory objSaldoInventory = new SaldoInventory();
            objSaldoInventory.ItemID = receiptDetail.ItemID;
            objSaldoInventory.GroupID = receiptDetail.GroupID;
            objSaldoInventory.ItemTypeID = receiptDetail.ItemTypeID;
            objSaldoInventory.AvgPrice = Convert.ToInt32(AvgPrice);
            objSaldoInventory.MonthPeriod = bulan;
            objSaldoInventory.YearPeriod = tahun;
            //cek apakah data SaldoInventory tahun ini sudah ada
            intResult = saldoInventoryFacade.CekRow(receiptDetail.ItemID, tahun, receiptDetail.ItemTypeID);
            if (intResult == 0)
            {
                intResult = saldoInventoryFacade.Insert(objSaldoInventory);
            }
            intResult = saldoInventoryFacade.UpdateSaldoAvgPriceBlnIni(objSaldoInventory);
            //end update average price

        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReceiptID", obj.ReceiptID));
                sqlListParam.Add(new SqlParameter("@ItemID", obj.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", obj.Quantity));
                sqlListParam.Add(new SqlParameter("@Kadarair", obj.Kadarair));
                sqlListParam.Add(new SqlParameter("@Price", obj.Price));
                sqlListParam.Add(new SqlParameter("@Disc", obj.Disc));
                sqlListParam.Add(new SqlParameter("@TotalPrice", obj.TotalPrice));
                sqlListParam.Add(new SqlParameter("@PoID", obj.PoID));
                sqlListParam.Add(new SqlParameter("@PoNo", obj.PoNo));
                sqlListParam.Add(new SqlParameter("@SppID", obj.SppID));
                sqlListParam.Add(new SqlParameter("@SppNo", obj.SppNo));
                sqlListParam.Add(new SqlParameter("@UomID", obj.UomID));
                sqlListParam.Add(new SqlParameter("@PODetailID", obj.PODetailID));
                sqlListParam.Add(new SqlParameter("@RowStatus", obj.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", obj.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", obj.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Keterangan", obj.Keterangan));
                sqlListParam.Add(new SqlParameter("@ItemID2", obj.ItemID2));
                sqlListParam.Add(new SqlParameter("@QtyTimbang", obj.QtyTimbang));
                sqlListParam.Add(new SqlParameter("@KodeTimbang", obj.KodeTimbang));
                sqlListParam.Add(new SqlParameter("@TipeAsset", obj.TipeAsset));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReceiptDetail");

                /** updare Reorder Point */
                ROPFacade ropfacade = new ROPFacade();
                ropfacade.UpdateROPByReceipt(obj.ItemID, obj.Quantity);
                /** update qty timbangan untuk gypsum*/
                strError = transManager.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
