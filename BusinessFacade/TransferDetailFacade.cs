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
    public class TransferDetailFacade : AbstractTransactionFacade
    {
        private TransferDetail objTransferDetail = new TransferDetail();
        private ArrayList arrTransferDetail;
        private List<SqlParameter> sqlListParam;

        public TransferDetailFacade(object objDomain)
            : base(objDomain)
        {
            objTransferDetail = (TransferDetail)objDomain;
        }

        public TransferDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TransferOrderID", objTransferDetail.TransferOrderID));
                sqlListParam.Add(new SqlParameter("@ItemID", objTransferDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objTransferDetail.Qty));
                sqlListParam.Add(new SqlParameter("@QtyScheduled", objTransferDetail.QtyScheduled));
                sqlListParam.Add(new SqlParameter("@QtyReceived", objTransferDetail.QtyReceived));
                sqlListParam.Add(new SqlParameter("@Tebal", objTransferDetail.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objTransferDetail.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objTransferDetail.Lebar));
                sqlListParam.Add(new SqlParameter("@Berat", objTransferDetail.Berat));
                sqlListParam.Add(new SqlParameter("@Paket", objTransferDetail.Paket));
                sqlListParam.Add(new SqlParameter("@TypeKondisi", objTransferDetail.TypeKondisi));
                sqlListParam.Add(new SqlParameter("@FromDepoID", objTransferDetail.FromDepoID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTransferDetail");

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
                sqlListParam.Add(new SqlParameter("@TransferOrderID", objTransferDetail.TransferOrderID));
                sqlListParam.Add(new SqlParameter("@ItemID", objTransferDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objTransferDetail.Qty));
                sqlListParam.Add(new SqlParameter("@QtyReceived", objTransferDetail.QtyReceived));
                sqlListParam.Add(new SqlParameter("@Status", objTransferDetail.Status));
                sqlListParam.Add(new SqlParameter("@Tebal", objTransferDetail.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objTransferDetail.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objTransferDetail.Lebar));
                sqlListParam.Add(new SqlParameter("@TypeKondisi", objTransferDetail.TypeKondisi));
                sqlListParam.Add(new SqlParameter("@FromDepoID", objTransferDetail.FromDepoID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTransferDetail");

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
                objTransferDetail = (TransferDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TransferOrderID", objTransferDetail.TransferOrderID));                

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteTransferDetail");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferDetail as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferDetail.Add(new TransferDetail());

            return arrTransferDetail;
        }

        public TransferDetail RetrieveById2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.QtyScheduled,A.QtyReceived,A.Status,A.Tebal,A.Panjang,A.Lebar,A.Berat,A.Paket from TransferDetail as A,Items as B where A.ItemID = B.ID and A.ID = " + Id);
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TransferDetail();
        }

        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.QtyScheduled,A.QtyReceived,A.Status,A.Tebal,A.Panjang,A.Lebar,A.Berat,A.Paket from TransferDetail as A,Items as B where A.ItemID = B.ID and A.TransferOrderID = " + Id);
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferDetail.Add(new TransferDetail());

            return arrTransferDetail;
        }

        public TransferDetail RetrieveByItemAndOP(int itemID, int toID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.QtyScheduled,A.QtyReceived,A.Status,A.Tebal,A.Panjang,A.Lebar,A.Berat,A.Paket from TransferDetail as A,Items as B where A.ItemID = B.ID and A.TransferOrderID = " + toID + " and A.ItemID = " + itemID);
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                   return GenerateObject(sqlDataReader);
                }
            }

            return new TransferDetail();
        }

        public int SumQtyByItemAndTO(int itemID, int transferOrderID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(SUM(Qty),0) as jumQty from TransferDetail where ItemID = " + itemID + " and TransferOrderID = " + transferOrderID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumQty"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveByIdNoScheduled(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TransferOrderID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.QtyScheduled,A.QtyReceived,A.Status,A.Tebal,A.Panjang,A.Lebar,A.Berat,A.Paket from TransferDetail as A,Items as B where A.ItemID = B.ID and A.QtyScheduled < A.Qty and A.TransferOrderID = " + Id);
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferDetail.Add(new TransferDetail());

            return arrTransferDetail;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from TransferDetail as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrTransferDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTransferDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTransferDetail.Add(new TransferDetail());

            return arrTransferDetail;
        }

        public TransferDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objTransferDetail = new TransferDetail();
            objTransferDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTransferDetail.TransferOrderID = Convert.ToInt32(sqlDataReader["TransferOrderID"]);
            objTransferDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objTransferDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objTransferDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objTransferDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objTransferDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objTransferDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objTransferDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTransferDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objTransferDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objTransferDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objTransferDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objTransferDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            return objTransferDetail;

        }
    }
}

