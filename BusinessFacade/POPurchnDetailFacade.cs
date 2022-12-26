using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class POPurchnDetailFacade : AbstractTransactionFacade
    {
        private POPurchnDetail objPOPurchnDetail = new POPurchnDetail();
        private ArrayList arrPOPurchnDetail;
        private List<SqlParameter> sqlListParam;

        public POPurchnDetailFacade(object objDomain)
            : base(objDomain)
        {
            objPOPurchnDetail = (POPurchnDetail)objDomain;
        }

        public POPurchnDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@POID", objPOPurchnDetail.POID));
                sqlListParam.Add(new SqlParameter("@SPPID", objPOPurchnDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objPOPurchnDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Price", objPOPurchnDetail.Price));
                sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPOPurchnDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objPOPurchnDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchnDetail.Status));
                sqlListParam.Add(new SqlParameter("@NoUrut", objPOPurchnDetail.NoUrut));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objPOPurchnDetail.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@DocumentNo", objPOPurchnDetail.DocumentNo));
                sqlListParam.Add(new SqlParameter("@DlvDate", objPOPurchnDetail.DlvDate));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPOPurchnDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchnDetail.ID ));
                //sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
                //sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
                //sqlListParam.Add(new SqlParameter("@QtyReceived", objPOPurchnDetail.QtyReceived));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchnDetail.Status));
                sqlListParam.Add(new SqlParameter("@DlvDate", objPOPurchnDetail.DlvDate));
                //sqlListParam.Add(new SqlParameter("@Tebal", objPOPurchnDetail.Tebal));
                //sqlListParam.Add(new SqlParameter("@Panjang", objPOPurchnDetail.Panjang));
                //sqlListParam.Add(new SqlParameter("@Lebar", objPOPurchnDetail.Lebar));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePOPurchnDetail");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objPOPurchnDetail = (POPurchnDetail)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@POID", objPOPurchnDetail.POID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeletePOPurchnDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateStatusDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchnDetail.ID));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchnDetail.Status ));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusPOPurchnDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string Where { get; set; }
        private string Criteria()
        {
            string str = (HttpContext.Current.Session["POCriteria"] != null) ? HttpContext.Current.Session["POCriteria"].ToString() : string.Empty;
            return str;
        }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut," +
                            "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                            "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                            "when 3 then (select ItemCode from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                            "else '' end) ItemCode, " +
                            "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                            "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                            "when 3 then " + ItemSPPBiayaNew() +
                            "else '' end) ItemName,A.UOMID,(select UOMCode from UOM where ID=A.UOMID)UOMCode," +
                            "(SELECT NoPO from POPurchn where ID=A.POID)DocumentNo,A.ItemTypeID,0 as Stok,"+
                            "A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate from POPurchnDetail as A where A.Status = 0" +
                            Criteria();
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }
        //new
        public POPurchnDetail RetrieveTotalById(int id,int viewPrice)
        {
            string strSQL=string.Empty;
            this.viewPrice = viewPrice;
            if (viewPrice < 1)
            {
                strSQL = "select isnull(SUM(total),0) as total from (select case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then isnull(SUM(A.Qty * A.Price),0) else 0 end Total from POPurchnDetail as A where A.status>-1 and A.POID = " + id + " group by A.itemid) as P";
            }
            else
            {
                /* di rubah untuk mengakomodasi sistem baru
                 * Jika Supplier harga dolar dan supplier luar maka tetep dolar
                 * Jika suplier harga dolar dan supplier lokal maka harga sudah di konversi dengan rupiah dari input nilai kurs
                 * added on 04-07-2015 base on Mgr task
                 */
                //strSQL = "select isnull(SUM(A.Qty * A.Price),0) as Total from POPurchnDetail as A where A.status>-1 and A.POID = " + id;
                strSQL = "select isnull(SUM(Total),0)Total,isnull((Select NilaiKurs From POPurchn where ID=" + id + "),0)NilaiKurs, " +
                        "isnull((Select isnull(flag,0) from SuppPurch where ID=(Select SupplierID From POPurchn where ID=" + id + ")),0)flag from( " +
                        "select Case When (S.flag=0 AND B.Crc>1 AND NilaiKurs>0) Then (NilaiKurs*Price*Qty) ELSE ((Price*Qty)) END Total   " +
                        "from POPurchnDetail as A LEFT JOIN POPurchn B ON B.ID=A.POID   " +
                        "LEFT JOIN SuppPurch S ON S.ID=B.SupplierID   " +
                        "where A.status>-1 and A.POID = " + id + ") as n ";
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectTotal(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }
        //new
        public LastPrice GetLastPrice(int SPPDetailId, string supplier,int viewprice)
        {
            LastPrice  lastPrice =new LastPrice();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strsql = "select A.ID,A.Price  from ReceiptDetail A, Receipt B  where B.ID=A.ReceiptID and A.ItemID in " +
            //    "(select ItemID from SPPDetail where ID=" + SPPDetailId + ") and B.SupplierId in " +
            //    "(select ID from SuppPurch where SupplierName='" + supplier + "') order by A.ID desc";
            string strsql=string.Empty;
            if (viewprice<2)
                strsql = "select top 1 A.ID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,B.Crc from POPurchnDetail A, POPurchn  B  where B.ID=A.POID and A.ItemID in " +
                "(select ItemID from SPPDetail where ID=" + SPPDetailId + ") and B.SupplierId in  " +
                "(select ID from SuppPurch where SupplierName='" + supplier + "') order by A.ID desc";
            else
                strsql = "select top 1 A.ID,A.Price,B.Crc from POPurchnDetail A, POPurchn  B  where B.ID=A.POID and A.ItemID in " +
                "(select ItemID from SPPDetail where ID=" + SPPDetailId + ") and B.SupplierId in  " +
                "(select ID from SuppPurch where SupplierName='" + supplier + "') order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lastPrice.Price = Convert.ToDecimal(sqlDataReader["Price"]);
                    lastPrice.Crc = Convert.ToInt32(sqlDataReader["crc"]);
                }
            }
            return lastPrice;
         }
       
        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,B.UOMCode,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate, " +
                "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemCode from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemCode, " +
                "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then "+ItemSPPBiayaNew()+
                " else '' end) ItemName, " +
                "(case A.ItemTypeID when 1 then (select isnull(sum(Jumlah),0) from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select isnull(sum(Jumlah),0) from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then "+StockBiayaNew() +
                " else '' end) Stok " +
                "from POPurchnDetail as A,UOM as B where A.Status >= 0 and A.UOMID=B.ID and A.POID = " + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }

        public ArrayList RetrieveByIdForReceipt(int Id)
        {
            /**
             * added on 19-06-2014
             * for get itemcode spp biaya new
             */
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,B.UOMCode,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate, " +
                "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then " + CodeSPPBiayaNew()+
                "else '' end) ItemCode, " +
                "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then " + ItemSPPBiayaNew() +
                " else '' end) ItemName, " +
                "(case A.ItemTypeID when 1 then (select isnull(sum(Jumlah),0) from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select isnull(sum(Jumlah),0) from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then " + StockBiayaNew() +
                " else '' end) Stok " +
                "from POPurchnDetail as A,UOM as B where A.Status >= 0 and A.UOMID=B.ID and A.POID = " + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }        
        public POPurchnDetail RetrieveByItemAndPO(int itemID, int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate from POPurchnDetail as A where A.Status = 0 and A.ItemID = " + itemID + " and A.POID = " + poID);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }

        public ArrayList RetrieveByPOID(int poID)
        {
            string strSql = "select A.POID,A.Qty,A.SppDetailID from POPurchnDetail as A where A.POID = " + poID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObjectPOID(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }
        //iko

        //public POPurchnDetail RetrieveByID2(int detailID)
        //{
        //    string strSQL = "select A.POID,A.Qty,A.SppDetailID from POPurchnDetail as A where A.ID = " + detailID;

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrPOPurchnDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectPOID(sqlDataReader);
        //        }
        //    }

        //    return new POPurchnDetail();
        //}

        public ArrayList RetrieveByID2(int detailID)
        {
            string strSql = " select A.POID,A.Qty,A.SppDetailID from POPurchnDetail as A where A.ID = " + detailID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObjectPOID(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }

        //iko

        public POPurchnDetail RetrieveByDetailID(int detailID)
        {
            string strSQL = "select A.ID,A.POID,A.SPPID,A.SppDetailID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,B.UOMCode,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate, " +
                "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemCode from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemCode, " +
                "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then "+ItemSPPBiayaNew() +
                "else '' end) ItemName, " +
                "(case A.ItemTypeID when 1 then (select isnull(sum(Jumlah),0) from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select isnull(sum(Jumlah),0) from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then "+StockBiayaNew() +
                " else '' end) Stok, " +
                "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=(Select Minta from SPP where SPP.ID=A.SPPID)) and A.ItemTypeID=3 THEN "+
                "Isnull((Select TOP 1 ID from Biaya Where ItemName=(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.SPPDetailID)),0) "+
                " ELSE isnull(A.ItemID,0) END ItemID2,(select keterangan1 from SPPDetail where ID=SPPDetailID)Keterangan "+
                ",(select ISNULL(NoPol,0)NoPol from SPPDetail where ID=SPPDetailID)NoPol " +
                "from POPurchnDetail as A,UOM as B where A.Status >= 0 and A.UOMID=B.ID and A.ID = " + detailID;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }
        //new 27 des
        public ArrayList RetrieveItemPOID(int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
                "A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.UOMCode,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode, " +
                "case A.ItemTypeID when 1  then (select isnull(sum(Jumlah),0) from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select isnull(sum(Jumlah),0) from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else " + StockBiayaNew() + " end Stok " +
                "from POPurchnDetail as A, UOM as B where A.UOMID=B.ID  and A.POID = " + poID + this.Where);
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }
        //until new
        public decimal SumQtyByItemAndPO(int itemID, int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(SUM(Qty),0) as jumQty from POPurchnDetail where Status > -1 and ItemID = " + itemID + " and POID = " + poID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["jumQty"]);
                }
            }

            return 0;
        }

        public POPurchnDetail SumQtyDetail(int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT poid,isnull(SUM(Qty),0) as JumQty from POPurchnDetail where Status>-1 and POID = "+poID+" group by POID");
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSum(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }

        //public ArrayList RetrieveByIdNoScheduled(int Id)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.QtyScheduled,A.QtyReceived,A.Status,A.Tebal,A.Panjang,A.Lebar,A.Berat,A.Paket from TransferDetail as A,Items as B where A.ItemID = B.ID and A.QtyScheduled < A.Qty and A.TransferOrderID = " + Id);
        //    strError = dataAccess.Error;
        //    arrTransferDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrTransferDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrTransferDetail.Add(new TransferDetail());

        //    return arrTransferDetail;
        //}

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate from POPurchnDetail as A where A.Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrPOPurchnDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchnDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchnDetail.Add(new POPurchnDetail());

            return arrPOPurchnDetail;
        }

        public POPurchnDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchnDetail.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchnDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objPOPurchnDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objPOPurchnDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPOPurchnDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objPOPurchnDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchnDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objPOPurchnDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPOPurchnDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objPOPurchnDetail.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objPOPurchnDetail.Stok = Convert.ToDecimal(sqlDataReader["Stok"]);
            objPOPurchnDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            //objPOPurchnDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objPOPurchnDetail.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objPOPurchnDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPOPurchnDetail.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchnDetail;
        }
        public POPurchnDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchnDetail.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchnDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objPOPurchnDetail.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            objPOPurchnDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objPOPurchnDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPOPurchnDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objPOPurchnDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchnDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objPOPurchnDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPOPurchnDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objPOPurchnDetail.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objPOPurchnDetail.Stok = Convert.ToDecimal(sqlDataReader["Stok"]);
            objPOPurchnDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            //objPOPurchnDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchnDetail.ItemID2 = Convert.ToInt32(sqlDataReader["ItemID2"]);
            objPOPurchnDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPOPurchnDetail.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            objPOPurchnDetail.CreatedBy = sqlDataReader["Keterangan"].ToString();
            objPOPurchnDetail.NoPol = sqlDataReader["NoPol"].ToString();
            return objPOPurchnDetail;
        }
        public POPurchnDetail GenerateObjectSum(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchnDetail.JumQty = Convert.ToDecimal(sqlDataReader["JumQty"]);
            return objPOPurchnDetail;
        }               
        public POPurchnDetail GenerateObjectPOID(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchnDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchnDetail.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            return objPOPurchnDetail;
        }
        public int viewPrice { get; set; }
        public POPurchnDetail GenerateObjectTotal(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            //flag untuk supplier bayar (0: Rp; 1: MU asing)
            if (this.viewPrice > 0)
            {
                objPOPurchnDetail.RowStatus = Convert.ToInt32(sqlDataReader["flag"]);
                objPOPurchnDetail.Price = Convert.ToDecimal(sqlDataReader["NilaiKurs"]);
            }
            return objPOPurchnDetail;

        }
        /**
         * added on 28-04-2014
         * untuk perubahan pada itemname table biaya
         * dan stock per itemnya
         * update on 11-06-2014
         */

        public string ItemSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< "+
                " (Select CreatedTime from SPP where SPP.ID=A.SPPID)) " +
                " THEN(select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.SPPDetailID) ELSE " +
                " (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string CodeSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=A.SPPID)) " +
                " THEN(select ItemCode from biaya where ItemName= "+
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.SPPDetailID) ELSE " +
                " (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string StockBiayaNew()
        {
            string strSQL = "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< "+
                " (Select CreatedTime from SPP where SPP.ID=A.SPPID)) THEN " +
                " (SELECT isnull(sum(jumlah),0) from Biaya where ItemName=(Select SPPDetail.Keterangan From SPPDetail "+
                " where SPPDetail.ID=A.SPPDetailID)) " +
                " ELSE (select isnull(sum(Jumlah),0) from Biaya where ID=A.ItemID and Biaya.RowStatus>-1) END";
            return strSQL;
        }

        public decimal CheckQtyPO(string SPPDetailID)
        {
            decimal result = 0;
            string query = "select isnull(SUM(Qty),0)Qty from POPurchnDetail where SppDetailID=" + SPPDetailID + " and Status >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Qty"].ToString());
                }
            }
            return result;
        }
        public decimal GetKadarAir(string POID)
        {
            decimal result = 0;
            Users user=(Users)System.Web.HttpContext.Current.Session["Users"];
            string strSQL = "select AktualKA from POPurchnKadarAir where RowStatus>-1 and POID=" + POID;
            strSQL = "WITH ItemIDKertas AS( " +
                     "       SELECT ID,ItemCode FROM Inventory WHERE ItemCode IN(SELECT ItemCode FROM DeliveryKertas Group By ItemCode) " +
                     "   ), " +
                     "   DataKiriman AS( " +
                     "       SELECT dk.TglKirim, ka.NoSJ,ka.NOPOL,ka.GrossPlant,ka.NettPlant,ka.AvgKA,ka.Sampah,ka.Potongan,dk.POKAID,ka.DocNo,idk.ID ItemID " +
                     "       FROM DeliveryKertasKA ka " +
                     "       LEFT JOIN DeliveryKertas dk On ka.NoSJ=dk.NoSJ " +
                     "       LEFT JOIN ItemIDKertas idk ON idk.ItemCode=ka.ItemCode " +
                     "       WHERE ka.PlantID in(" + user.UnitKerjaID + ") AND ka.RowStatus>-1 AND dk.RowStatus>-1  " +
                     "   ), " +
                     "   DataKirimanLokal AS( " +
                     "       SELECT ka.TglCheck TglKirim, ka.NoSJ,ka.NOPOL,ka.GrossPlant,ka.NettPlant,ka.AvgKA,ka.Sampah,ka.Potongan,ISNULL(ka.POKAID,0)POKAID,ka.DocNo,idk.ID ItemID " +
                     "       FROM DeliveryKertasKA ka  " +
                     "       LEFT JOIN ItemIDKertas idk ON idk.ItemCode=ka.ItemCode " +
                     "       WHERE ka.PlantID in(" + user.UnitKerjaID + ") AND Nosj='0' AND ka.RowStatus>-1 " +
                     "   ), " +
                     "   DataKirimanAll AS( " +
                     "       SELECT * FROM DataKiriman " +
                     "       UNION ALL " +
                     "       SELECT * FROM DataKirimanLokal " +
                     "   ) " +
                     "   SELECT * FROM DataKirimanAll WHERE POKAID= " + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr=da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["AvgKA"].ToString());
                }
            }
            else
            {
                result = GetKadarAir(POID, true);
            }
            return result;
        }
        public decimal GetKadarAir(string POID,bool FromKAQA)
        {
            decimal result = 0;
            string strSQL = "select AvgKA from DeliveryKertasKA where RowStatus>-1 and POKAID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["AvgKA"].ToString());
                }
            }
            return result;
        }
        public int GetKadarAirPOID(string POID)
        {
            int result = 0;
            string strSQL = "select ISNULL(ID,0)POID from POPurchnKadarAir where RowStatus>-1 and POID=(SELECT ID FROM POPurchn Where NOPO='" + POID + "')";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["POID"].ToString());
                }
            }
            return result;
        }
        public ArrayList RetrieveByNoPO(string NoPO)
        {
            arrPOPurchnDetail = new ArrayList();
            string strSQL = "SELECT pod.*, " +
                          "CASE pod.ItemTypeID WHEN 1 THEN G.ItemCode WHEN 2 THEN A.ItemCode WHEN 3 THEN B.ItemCode END ItemCode, " +
                          "CASE pod.ItemTypeID WHEN 1 THEN G.ItemName WHEN 2 THEN A.ItemName WHEN 3 THEN B.ItemName END ItemName, " +
                          "pd.DlvDate " +
                          "FROM vw_PObukanRP pod " +
                          "LEFt JOIN POPurchnDetail as Pd on pd.ID=pod.PODetailID " +
                          "LEFT JOIN Inventory as G on G.ID=pod.ItemID " +
                          "LEFT JOIN Asset as A on A.ID=pod.ItemID " +
                          "LEFT JOIN Biaya as B on B.ID=pod.ItemID " +
                          "WHERE NoPO='" + NoPO + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchnDetail.Add(GetObject(sdr));
                }
            }
            return arrPOPurchnDetail;
        }
        public ArrayList RetrieveByNoSPP(string NoPO)
        {
            arrPOPurchnDetail = new ArrayList();
            string strSQL = "SELECT pod.*, " +
                          "CASE pod.ItemTypeID WHEN 1 THEN G.ItemCode WHEN 2 THEN A.ItemCode WHEN 3 THEN B.ItemCode END ItemCode, " +
                          "CASE pod.ItemTypeID WHEN 1 THEN G.ItemName WHEN 2 THEN A.ItemName WHEN 3 THEN B.ItemName END ItemName, " +
                          "pd.DlvDate,pd.DocumentNo " +
                          "FROM vw_PObukanRP pod " +
                          "LEFt JOIN POPurchnDetail as Pd on pd.ID=pod.PODetailID " +
                          "LEFT JOIN Inventory as G on G.ID=pod.ItemID " +
                          "LEFT JOIN Asset as A on A.ID=pod.ItemID " +
                          "LEFT JOIN Biaya as B on B.ID=pod.ItemID " +
                          "WHERE pd.DocumentNo='" + NoPO + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchnDetail.Add(GetObject(sdr));
                }
            }
            return arrPOPurchnDetail;
        }
        private POPurchnDetail GetObject(SqlDataReader sdr)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.ID = (sdr["PODetailID"] != DBNull.Value) ? Convert.ToInt32(sdr["PODetailID"].ToString()) : 0;
            objPOPurchnDetail.ItemCode = sdr["ItemCode"].ToString();
            objPOPurchnDetail.ItemName = sdr["ItemName"].ToString();
            objPOPurchnDetail.NamaBarang = sdr["SupplierName"].ToString();
            objPOPurchnDetail.Qty = Convert.ToDecimal(sdr["Qty"].ToString());
            objPOPurchnDetail.DlvDate = Convert.ToDateTime(sdr["DlvDate"].ToString());
            return objPOPurchnDetail;
        }
        public int UpdatePrice(decimal Price, int ID)
        {
            int result = 0;
            string strSQL = "Update POPurchnDetail set Price=" + Price + " where ID=" + ID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            result = sdr.RecordsAffected;
            return result;
        }
        public ArrayList RetrieveKAData()
        {
            arrPOPurchnDetail = new ArrayList();
            string strSQL = "select * from POPurchnKadarAir where RowStatus>-1 Order by POID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchnDetail.Add(ObjectKA(sdr));
                }
            }
            return arrPOPurchnDetail;
        }
        public ArrayList RetrieveKAData(string POID)
        {
            arrPOPurchnDetail = new ArrayList();
            string strSQL = "select * from POPurchnKadarAir where RowStatus>-1 and POID=" + POID + " Order by POID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchnDetail.Add(ObjectKA(sdr));
                }
            }
            return arrPOPurchnDetail;
        }
        public ArrayList RetrieveKAData(string POID,string PODetailID)
        {
            arrPOPurchnDetail = new ArrayList();
            string strSQL = "select * from POPurchnKadarAir where RowStatus>-1 AND Gross>0 and PODetailID=" + PODetailID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchnDetail.Add(ObjectKA(sdr));
                }
            }
            return arrPOPurchnDetail;
        }
        public KadarAir RetrieveKAData(string POID, string PODetailID,bool Detail)
        {
            KadarAir ka = new KadarAir();
            string strSQL = "select * from POPurchnKadarAir where RowStatus>-1 and PODetailID=" + PODetailID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ka = (ObjectKA(sdr));
                }
            }
            return ka;
        }
        public Int32 GetNettPlant(string POID)
        {
            Int32 nettplant = 0;
            string strSQL = "select top 1 Netto from ( select NettDepo Netto from DeliveryKertas where RowStatus>-1 and POKAID=" + POID + " " +
                "union all " +
                "select NettPlant  Netto from DeliveryKertaska where RowStatus>-1 and POKAID=" + POID + ")a where netto>0 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    nettplant = int.Parse(sdr["Netto"].ToString());
                }
            }
            return nettplant;
        }
        private KadarAir ObjectKA(SqlDataReader sdr)
        {
            KadarAir ka = new KadarAir();
            ka.ItemID = int.Parse(sdr["ItemID"].ToString());
            ka.StdKA = decimal.Parse(sdr["StdKA"].ToString());
            ka.AktualKA = decimal.Parse(sdr["AktualKA"].ToString());
            ka.Gross = decimal.Parse(sdr["Gross"].ToString());
            ka.Netto = decimal.Parse(sdr["Netto"].ToString());
            ka.POID = int.Parse(sdr["POID"].ToString());
            ka.PODetailID = int.Parse(sdr["PODetailID"].ToString());
            ka.NoPol = sdr["NOPOL"].ToString();
            ka.ID = int.Parse(sdr["ID"].ToString());
            ka.NoSJ = sdr["SchNo"].ToString();
            ka.PointStatus = (sdr["PointStatus"] != DBNull.Value) ? int.Parse(sdr["PointStatus"].ToString()) : 0;
            ka.IPAddress = sdr["IPAddress"].ToString();
            return ka;
        }
        public int UpdateStatusPoint(int idKadarAir, KadarAir objKadarAir)
        {
            int result = 0;
            string strSQL = "Update POPurchnKadarAir Set NoSJ='" + objKadarAir.NoSJ + "',PointStatus=" + objKadarAir.PointStatus +
                   ",IPAddress='" + objKadarAir.IPAddress + "' where ID=" + idKadarAir;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            result = (sdr != null) ? sdr.RecordsAffected : 0;
            return result;
        }
        public POPurchnDetail GetLastPOPrice(int ItemID, int ItemTypeID)
        {
            objPOPurchnDetail = new POPurchnDetail();
            string strSQL = "SELECT Top 1 *,(SELECT POPurchnDate FROM POPurchn WHERE ID=POID)POPurchnDate "+
                            "FROM POPurchnDetail WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ItemTypeID + 
                            " AND Status>-1 ORDER BY ID DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return oGenerateObject(sdr);
                }
            }
            return objPOPurchnDetail;
        }
        private POPurchnDetail oGenerateObject(SqlDataReader sdr)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.ID = int.Parse(sdr["ID"].ToString());
            objPOPurchnDetail.Price = decimal.Parse(sdr["Price"].ToString());
            objPOPurchnDetail.POPurchnDate = DateTime.Parse(sdr["POPurchnDate"].ToString());
            return objPOPurchnDetail;
        }
    }
}
