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
    public class ReturPakaiDetailFacade : AbstractTransactionFacade
    {
        private ReturPakaiDetail objReturPakaiDetail = new ReturPakaiDetail();
        private ArrayList arrReturPakaiDetail;
        private List<SqlParameter> sqlListParam;
        //private string receiptNo = string.Empty;
        private string returNo = string.Empty;

        public ReturPakaiDetailFacade(object objDomain)
            : base(objDomain)
        {
            objReturPakaiDetail = (ReturPakaiDetail)objDomain;
        }

        public ReturPakaiDetailFacade()
        {

        }

        public ReturPakaiDetailFacade(object objDomain, string strPakaiNo)
        {
            objReturPakaiDetail = (ReturPakaiDetail)objDomain;
            returNo = strPakaiNo;
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReturID", objReturPakaiDetail.ReturID));
                sqlListParam.Add(new SqlParameter("@ItemID", objReturPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objReturPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@UomID", objReturPakaiDetail.UomID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objReturPakaiDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@RowStatus", objReturPakaiDetail.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", objReturPakaiDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReturPakaiDetail.ItemTypeID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReturPakaiDetail");

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
            // perlu di test dulu

            int intResult = Delete(transManager);
            if (strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objReturPakaiDetail = (ReturPakaiDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReturPakaiDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteReturPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelPakaiDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReturPakaiDetail.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objReturPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objReturPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objReturPakaiDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelReturPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelInventoryByPakaiDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objReturPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objReturPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objReturPakaiDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelInventoryByPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelInventoryByReturDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objReturPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objReturPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objReturPakaiDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelInventoryByReturDetail");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ReturPakaiDetail and RowStatus>-1");
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveItemGrid(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
                        "case A.ItemTypeID " +
                        "when 1 then(select ItemCode from Inventory where ID = B.ItemID) " +
                        "when 2 then(select ItemCode from Asset where ID = B.ItemID) " +
                        "when 3 then(select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                        "case A.ItemTypeID " +
                        "when 1 then(select ItemName from Inventory where ID = B.ItemID) " +
                        "when 2 then(select ItemName from Asset where ID = B.ItemID) " +
                        "when 3 then(select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                        "B.Quantity,C.UOMCode,B.Keterangan " +
                        "from ReturPakai as A,ReturPakaiDetail as B,UOM as C " +
                        "where A.ID = B.ReturID and B.UomID = C.ID and A.ID = " + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObjectGrid(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }


        public ArrayList RetrieveByReturIdBakuBantu(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName from ReturPakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.GroupID in (1,2) and A.ReturID=" + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveByReturId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName from ReturPakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReturID=" + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveByReturIdwithGroupID(int Id, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName from ReturPakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReturID=" + Id + " and A.GroupID=" + groupID);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveByReturIdForAsset(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName from ReturPakaiDetail as A, UOM as B, Asset as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReturID=" + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveByReturIdForBiaya(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName from ReturPakaiDetail as A, UOM as B, Biaya as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReturID=" + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }
        public ArrayList RetrieveByPakaiIdForAll(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case PakaiDetail.ItemTypeID when 1 then (select Inventory.ItemName from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ItemName from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ItemName from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ItemName," +
                "case PakaiDetail.ItemTypeID when 1 then (select Inventory.ID from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ID from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ID from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ID from ReturPakaiDetail where PakaiDetail.RowStatus>-1 and PakaiDetail.PakaiID=" + Id);
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ArrayList RetrieveByGroupIDwithSUM(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,SUM(Quantity) as Quantity,GroupID from ( " +
                    "select B.ItemID,B.Quantity,B.GroupID from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1  and B.RowStatus>-1 and " +
                    "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemType + " and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thbl + "') as P group by GroupID,ItemID order by ItemID");
            strError = dataAccess.Error;
            arrReturPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakaiDetail.Add(GenerateObjectSUM(sqlDataReader));
                }
            }
            else
                arrReturPakaiDetail.Add(new ReturPakaiDetail());

            return arrReturPakaiDetail;
        }

        public ReturPakaiDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objReturPakaiDetail = new ReturPakaiDetail();
            objReturPakaiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReturPakaiDetail.ReturID = Convert.ToInt32(sqlDataReader["ReturID"]);
            objReturPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReturPakaiDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReturPakaiDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            objReturPakaiDetail.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objReturPakaiDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objReturPakaiDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objReturPakaiDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReturPakaiDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objReturPakaiDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            return objReturPakaiDetail;
        }
        public ReturPakaiDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objReturPakaiDetail = new ReturPakaiDetail();
            objReturPakaiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReturPakaiDetail.ItemName = sqlDataReader["ItemName"].ToString();

            return objReturPakaiDetail;
        }

        public ReturPakaiDetail GenerateObjectGrid(SqlDataReader sqlDataReader)
        {
            objReturPakaiDetail = new ReturPakaiDetail();
            objReturPakaiDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReturPakaiDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            objReturPakaiDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objReturPakaiDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReturPakaiDetail.ItemName = sqlDataReader["ItemName"].ToString();
            
            return objReturPakaiDetail;
        }
        public ReturPakaiDetail GenerateObjectSUM(SqlDataReader sqlDataReader)
        {
            objReturPakaiDetail = new ReturPakaiDetail();
            objReturPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReturPakaiDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReturPakaiDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            return objReturPakaiDetail;
        }

    }

}
