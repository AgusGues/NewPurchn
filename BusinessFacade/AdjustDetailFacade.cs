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
    public class AdjustDetailFacade : AbstractTransactionFacade
    {
        private AdjustDetail objAdjustDetail = new AdjustDetail();
        private ArrayList arrAdjustDetail;
        private List<SqlParameter> sqlListParam;
        private string adjustNo = string.Empty;

        public AdjustDetailFacade(object objDomain)
            : base(objDomain)
        {
            objAdjustDetail = (AdjustDetail)objDomain;
        }

        public AdjustDetailFacade()
        {

        }

        public AdjustDetailFacade(object objDomain, string strReceiptNo)
        {
            objAdjustDetail = (AdjustDetail)objDomain;
            adjustNo = strReceiptNo;
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustID", objAdjustDetail.AdjustID));
                sqlListParam.Add(new SqlParameter("@ItemID", objAdjustDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objAdjustDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@UomID", objAdjustDetail.UomID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objAdjustDetail.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", objAdjustDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@AdjustType", objAdjustDetail.AdjustType));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objAdjustDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objAdjustDetail.Keterangan));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertAdjustDetail1");

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
                sqlListParam.Add(new SqlParameter("@AdjustDetailID", objAdjustDetail.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objAdjustDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objAdjustDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@AdjustType", objAdjustDetail.AdjustType));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objAdjustDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAdjustDetail.CreatedBy ));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateAdjustDetailApv");

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
                objAdjustDetail = (AdjustDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objAdjustDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelAdjustDetail");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from AdjustDetail where RowStatus>-1");
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByAdjustIdBakuBantu(int Id)
        {
            string strSQL = "select A.ID,A.AdjustID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName "+
                            "from AdjustDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 and aktif=1 and A.UomID=B.ID and A.ItemID=C.ID "+
                            "and A.GroupID in (1,2) and A.AdjustID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByAdjustId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from AdjustDetail where RowStatus>-1 and AdjustID=" + Id);
            string strSQL="select A.ID,A.AdjustID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode, "+
                          " CASE A.ItemTypeID  "+
	                      "     WHEN 1 Then C.ItemCode "+
	                      "     WHEN 2 Then (select ItemCode from Asset where ID=ItemID and Aktif=1) "+
                          "     WHEN 3 THEN (select ItemCode from Biaya where ID=ItemID and Aktif=1) "+
                          "     WHEN 4 THEN (select ItemCode from Asset where ID=ItemID and Aktif=1 and groupid=12) " +
                          " END ItemCode, " +
                          " CASE A.ItemTypeID  "+
	                      "     WHEN 1 Then C.ItemName "+
                          "     WHEN 2 Then (select ItemName from Asset where ID=ItemID and Aktif=1) " +
                          "     WHEN 3 THEN (select ItemName from Biaya where ID=ItemID and Aktif=1) "+
                          "     WHEN 4 THEN (select ItemName from Asset where ID=ItemID and Aktif=1 and groupid=12) " +
                          "END ItemName," +
                          "(select Adjust.AdjustType from Adjust  where ID=A.AdjustID) AdjustType " +
                          " from AdjustDetail as A, UOM as B, Inventory as C  " +
                          "  where A.RowStatus>-1 and A.UomID=B.ID and A.ItemID=C.ID and A.AdjustID="+Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByAdjustIdwithGroupID(int Id, int groupID)
        {
            string strSQL = "select A.ID,A.AdjustID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode, "+
                            " CASE A.ItemTypeID  "+
	                        "     WHEN 1 Then C.ItemCode "+
                            "     WHEN 2 Then (select ItemCode from Asset where ID=ItemID and Aktif=1) " +
                            "     WHEN 3 THEN (select ItemCode from Biaya where ID=ItemID and Aktif=1) END ItemCode, " +
                            " CASE A.ItemTypeID  "+
	                        "     WHEN 1 Then C.ItemName "+
                            "     WHEN 2 Then (select ItemName from Asset where ID=ItemID and Aktif=1) " +
                            "     WHEN 3 THEN (select ItemName from Biaya where ID=ItemID and Aktif=1) END ItemName " +
                            " from AdjustDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 and A.UomID=B.ID "+
                            " and A.ItemID=C.ID and A.AdjustID=" + Id + " and A.GroupID=" + groupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByAdjustIdForAsset(int Id)
        {
            string strSQL = "select A.ID,A.AdjustID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,"+
                            "C.ItemCode,C.ItemName from AdjustDetail as A, UOM as B, Asset as C where A.RowStatus>-1 and A.UomID=B.ID "+
                            "and A.ItemID=C.ID and A.AdjustID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }
        public ArrayList RetrieveByAdjustIdForBiaya(int Id)
        {
            string strSQL = "select A.ID,A.AdjustID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName "+
                            "from AdjustDetail as A, UOM as B, Biaya as C where A.RowStatus>-1 and A.UomID=B.ID and A.ItemID=C.ID and A.AdjustID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByGroupIDwithSUM(int groupID, int itemType, string thbl)
        {
            string strSQL = "select ItemID,SUM(Quantity) as Quantity,GroupID,AdjustType from ( " +
                            "select B.ItemID,B.Quantity,B.GroupID,A.AdjustType from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  "+
                            "and B.RowStatus>-1 and B.GroupID=" + groupID + " and B.ItemTypeID=" + itemType + 
                            " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thbl + "') as P group by GroupID,AdjustType,ItemID "+
                            " order by ItemID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObjectSUM(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new AdjustDetail());

            return arrAdjustDetail;
        }

        public ArrayList RetrieveByApproval(string AdjNo)
        {
            string strSQL = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            strSQL = (AdjNo == string.Empty) ?
                    "select distinct top 200 B.ID,B.itemid,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.ItemTypeID, " +
                    "case when B.ItemTypeID =1 then (select itemcode from Inventory where ID=B.ItemID and Aktif=1 ) " +
                    "when B.ItemTypeID =2 then (select itemcode from Asset  where ID=B.ItemID and Aktif=1 ) " +
                    "when B.ItemTypeID =3 then (select itemcode from Biaya  where ID=B.ItemID  and Aktif=1) "+
                    "when B.ItemTypeID =4 then (select itemcode from Asset  where ID=B.ItemID  and Aktif=1 and groupID=12) " +
                    "end itemcode, " +
                    
                    "case when B.ItemTypeID =1 then (select rtrim(itemName) from Inventory where ID=B.ItemID ) " +
                    "when B.ItemTypeID =2 then (select rtrim(itemName) from Asset  where ID=B.ItemID and Aktif=1 ) " +
                    "when B.ItemTypeID =3 then (select rtrim(itemName) from Biaya  where ID=B.ItemID  and Aktif=1) "+
                    "when B.ItemTypeID =4 then (select rtrim(itemName) from Asset  where ID=B.ItemID  and Aktif=1 and groupID=12) " +
                    "end itemName,B.Quantity,C.UOMCode,B.Keterangan " +
                    "from Adjust as A, AdjustDetail as B,UOM C where  A.ID=B.AdjustID and B.UomID=C.ID and isnull(B.apv,0)=0 and B.RowStatus>-1 order by B.ID desc" :

                    "select distinct top 10 B.ID,B.itemid,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.ItemTypeID, " +
                    "case when B.ItemTypeID =1 then (select itemcode from Inventory where ID=B.ItemID ) " +
                    "when B.ItemTypeID =2 then (select itemcode from Asset  where ID=B.ItemID  and Aktif=1) " +
                    "when B.ItemTypeID =3 then (select itemcode from Biaya  where ID=B.ItemID  and Aktif=1) "+
                    "when B.ItemTypeID =4 then (select itemcode from Asset  where ID=B.ItemID  and Aktif=1 and groupID=12) " +
                    "end itemcode, " +
                    "case when B.ItemTypeID =1 then (select rtrim(itemName) from Inventory where ID=B.ItemID ) " +
                    "when B.ItemTypeID =2 then (select rtrim(itemName) from Asset  where ID=B.ItemID  and Aktif=1) " +
                    "when B.ItemTypeID =3 then (select rtrim(itemName) from Biaya  where ID=B.ItemID  and Aktif=1) "+
                    "when B.ItemTypeID =4 then (select rtrim(itemName) from Asset  where ID=B.ItemID  and Aktif=1 and groupID=12) " +
                    "end itemName,B.Quantity,C.UOMCode,B.Keterangan " +
                    "from Adjust as A, AdjustDetail as B,UOM C where  A.ID=B.AdjustID and B.UomID=C.ID and isnull(B.apv,0)=0 and B.RowStatus>-1 " +
                    "and A.AdjustNo='" + AdjNo + "' order by B.ID desc";
            
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjustDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjustDetail.Add(GenerateObjectApproval(sqlDataReader));
                }
            }
            else
                arrAdjustDetail.Add(new Adjust());
            return arrAdjustDetail;
        }


        public AdjustDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objAdjustDetail = new AdjustDetail();
            objAdjustDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objAdjustDetail.AdjustID = Convert.ToInt32(sqlDataReader["AdjustID"]);
            objAdjustDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objAdjustDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objAdjustDetail.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objAdjustDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objAdjustDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objAdjustDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objAdjustDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objAdjustDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objAdjustDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            return objAdjustDetail;
        }

        public AdjustDetail GenerateObjectSUM(SqlDataReader sqlDataReader)
        {
            objAdjustDetail = new AdjustDetail();
            objAdjustDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objAdjustDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objAdjustDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objAdjustDetail.AdjustType = sqlDataReader["AdjustType"].ToString();
            return objAdjustDetail;
        }

        public AdjustDetail GenerateObjectApproval(SqlDataReader sqlDataReader)
        {
            objAdjustDetail = new AdjustDetail();
            objAdjustDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objAdjustDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objAdjustDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objAdjustDetail.AdjustNo = sqlDataReader["AdjustNo"].ToString();
            objAdjustDetail.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objAdjustDetail.AdjustType = sqlDataReader["AdjustType"].ToString();
            objAdjustDetail.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objAdjustDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objAdjustDetail.ItemName = sqlDataReader["ItemName"].ToString().Trim();
            objAdjustDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objAdjustDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objAdjustDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            return objAdjustDetail;
        }

    }
}
