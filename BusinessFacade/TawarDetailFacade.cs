using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class TawarDetailFacade : AbstractTransactionFacade
    {
        private POPurchnDetail objPOPurchnDetail = new POPurchnDetail();
        private ArrayList arrTawarDetail;
        private List<SqlParameter> sqlListParam;

        public TawarDetailFacade(object objDomain)
            : base(objDomain)
        {
            objPOPurchnDetail = (POPurchnDetail)objDomain;
        }

        public TawarDetailFacade()
        {


        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TawarID", objPOPurchnDetail.POID));
                sqlListParam.Add(new SqlParameter("@SPPID", objPOPurchnDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objPOPurchnDetail.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPOPurchnDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@GroupID", objPOPurchnDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@UOMID", objPOPurchnDetail.UOMID));
                
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTawarDetail");

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
                sqlListParam.Add(new SqlParameter("@TawarID", objPOPurchnDetail.POID));
                sqlListParam.Add(new SqlParameter("@SPPID", objPOPurchnDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objPOPurchnDetail.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPOPurchnDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@GroupID", objPOPurchnDetail.GroupID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTawarDetail");

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

                sqlListParam.Add(new SqlParameter("@TawarID", objPOPurchnDetail.POID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteTawarDetail");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.DocumentNo from TawarDetail as A where A.Status = 0");
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawarDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawarDetail.Add(new TawarDetail());

            return arrTawarDetail;
        }
        //new
        public POPurchnDetail RetrieveTotalById(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(A.Qty * A.Price),0) as Total from TawarDetail as A where A.POID = " + id);
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

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

        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut from TawarDetail as A where A.Status = 0 and A.ID = " + Id);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,B.UOMCode,A.DocumentNo, " +
                "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemCode from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemCode, " +
                "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemName, " +
                "(case A.ItemTypeID when 1 then (select Jumlah from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select Jumlah from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select Jumlah from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) Stok " +
                "from TawarDetail as A,UOM as B where A.Status = 0 and A.UOMID=B.ID and A.POID = " + Id);
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawarDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawarDetail.Add(new TawarDetail());

            return arrTawarDetail;
        }

        public POPurchnDetail RetrieveByItemAndPO(int itemID, int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.DocumentNo from TawarDetail as A where A.Status = 0 and A.ItemID = " + itemID + " and A.POID = " + poID);
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }

        public POPurchnDetail RetrieveByDetailID(int detailID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,B.UOMCode,A.DocumentNo, " +
                "(case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemCode from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemCode from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemCode, " +
                "(case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select ItemName from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) ItemName, " +
                "(case A.ItemTypeID when 1 then (select Jumlah from Inventory where ID=A.ItemID and Inventory.RowStatus>-1) " +
                "when 2 then (select Jumlah from asset where ID=A.ItemID and Asset.RowStatus>-1) " +
                "when 3 then (select Jumlah from biaya where ID=A.ItemID and biaya.RowStatus>-1) " +
                "else '' end) Stok " +
                "from TawarDetail as A,UOM as B where A.Status = 0 and A.UOMID=B.ID and A.ID = " + detailID);
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new POPurchnDetail();
        }
        //new 27 des
        public ArrayList RetrieveItemPOID(int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.UOMCode,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                                                                          "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                                                                          "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                                                                          "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end ItemName, " +
                                                                          "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                                                                          "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                                                                          "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode, " +
                                                                          "case A.ItemTypeID when 1  then (select Jumlah from Inventory where ID=A.ItemID and RowStatus > -1) " +
                                                                          "when 2 then (select Jumlah from Asset where ID=A.ItemID and RowStatus > -1) " +
                                                                          "else (select Jumlah from Biaya where ID=A.ItemID and RowStatus > -1) end Stok " +
                                                                          "from TawarDetail as A, UOM as B where A.UOMID=B.ID and A.POID = " + poID);
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawarDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawarDetail.Add(new TawarDetail());

            return arrTawarDetail;
        }
        //until new
        public decimal SumQtyByItemAndPO(int itemID, int poID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(SUM(Qty),0) as jumQty from TawarDetail where Status > -1 and ItemID = " + itemID + " and POID = " + poID);
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT poid,isnull(SUM(Qty),0) as JumQty from TawarDetail where Status>-1 and POID = " + poID + " group by POID");
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.DocumentNo from TawarDetail as A where A.Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrTawarDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawarDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawarDetail.Add(new TawarDetail());

            return arrTawarDetail;
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
            //objTawarDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objTawarDetail.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);

            objPOPurchnDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);

            return objPOPurchnDetail;

        }

        public POPurchnDetail GenerateObjectSum(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchnDetail.JumQty = Convert.ToDecimal(sqlDataReader["JumQty"]);

            return objPOPurchnDetail;

        }

        public POPurchnDetail GenerateObjectTotal(SqlDataReader sqlDataReader)
        {
            objPOPurchnDetail = new POPurchnDetail();
            objPOPurchnDetail.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            return objPOPurchnDetail;

        }
    }

}
