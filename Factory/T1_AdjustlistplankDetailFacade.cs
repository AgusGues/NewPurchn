using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
namespace Factory
{
    public class T1_AdjustListplankDetailFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_AdjustListplankDetail objT1_AdjustListplankDetail = new T1_AdjustListplankDetail();
        private ArrayList arrT1_AdjustListplankDetail;
        private List<SqlParameter> sqlListParam;

        public T1_AdjustListplankDetailFacade(object objDomain)
            : base(objDomain)
        {
            objT1_AdjustListplankDetail = (T1_AdjustListplankDetail)objDomain;
        }
        public T1_AdjustListplankDetailFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update2(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT1_AdjustListplankDetail = (T1_AdjustListplankDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustID", objT1_AdjustListplankDetail.AdjustID));
                sqlListParam.Add(new SqlParameter("@ItemID0", objT1_AdjustListplankDetail.ItemID0));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_AdjustListplankDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@AdjustType", objT1_AdjustListplankDetail.AdjustType));
                sqlListParam.Add(new SqlParameter("@Qty", objT1_AdjustListplankDetail.Qty));
                sqlListParam.Add(new SqlParameter("@Apv", objT1_AdjustListplankDetail.Apv ));
                sqlListParam.Add(new SqlParameter("@Process", objT1_AdjustListplankDetail.Process));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_AdjustListplankDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_AdjustListplankDetail");
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
                objT1_AdjustListplankDetail = (T1_AdjustListplankDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT1_AdjustListplankDetail.ID));
                sqlListParam.Add(new SqlParameter("@Apv", objT1_AdjustListplankDetail.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objT1_AdjustListplankDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT1_AdjustListplankdetail");
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
                objT1_AdjustListplankDetail = (T1_AdjustListplankDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT1_AdjustListplankDetail.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_AdjustListplankDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteT1_AdjustListplankDetail");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveByTgl(string tgl)
        {
            string strSQL = "SELECT B.ID,B.itemid,A.AdjustNo, A.AdjustDate, A.NoBA, B.Process ,B.AdjustType, " +
                "I.partno,B.Qty,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval  " +
                "FROM  T1_AdjustListplank AS A INNER JOIN T1_AdjustListplankDetail AS B ON A.ID = B.AdjustID  " +
                "inner join FC_Items I on B.ItemID=I.ID where B.rowstatus>-1 and convert(varchar,A.Adjustdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplankDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_AdjustListplankDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT1_AdjustListplankDetail.Add(new T1_AdjustListplankDetail());

            return arrT1_AdjustListplankDetail;
        }
        public ArrayList RetrieveByNoBA(string No)
        {
            string strSQL = "SELECT B.ID,B.itemid,A.AdjustNo, A.AdjustDate, A.NoBA, B.Process ,B.AdjustType, " +
                "I.partno,B.Qty,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval  " +
                "FROM  T1_AdjustListplank AS A INNER JOIN T1_AdjustListplankDetail AS B ON A.ID = B.AdjustID  " +
                "inner join FC_Items I on B.ItemID=I.ID where B.rowstatus>-1 and A.NoBA='" + No + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplankDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_AdjustListplankDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT1_AdjustListplankDetail.Add(new T1_AdjustListplankDetail());

            return arrT1_AdjustListplankDetail;
        }
        public ArrayList RetrieveByapv(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,A.AdjustNo, A.AdjustDate, A.NoBA, B.Process ,B.AdjustType, " +
                "I.partno,B.Qty,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval  " +
                "FROM  T1_AdjustListplank AS A INNER JOIN T1_AdjustListplankDetail AS B ON A.ID = B.AdjustID  " +
                "inner join FC_Items I on B.ItemID=I.ID where B.rowstatus>-1 and B.apv=" + apv;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplankDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_AdjustListplankDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT1_AdjustListplankDetail.Add(new T1_AdjustListplankDetail());

            return arrT1_AdjustListplankDetail;
        }
       
        
        public ArrayList RetrieveByPartno(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,A.AdjustNo, A.AdjustDate, A.NoBA, B.Process ,B.AdjustType, " +
                "I.partno,B.Qty,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval  " +
                "FROM  T1_AdjustListplank AS A INNER JOIN T1_AdjustListplankDetail AS B ON A.ID = B.AdjustID  " +
                "inner join FC_Items I on B.ItemID=I.ID where B.rowstatus>-1 and B.itemid in (select id from fc_items where partno='" + apv + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplankDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_AdjustListplankDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT1_AdjustListplankDetail.Add(new T1_AdjustListplankDetail());

            return arrT1_AdjustListplankDetail;
        }
        public T1_AdjustListplankDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_AdjustListplankDetail = new T1_AdjustListplankDetail();
            objT1_AdjustListplankDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_AdjustListplankDetail.AdjustID = Convert.ToInt32(sqlDataReader["AdjustID"]);
            objT1_AdjustListplankDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT1_AdjustListplankDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT1_AdjustListplankDetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            objT1_AdjustListplankDetail.AdjustNo = (sqlDataReader["AdjustNo"]).ToString();
            objT1_AdjustListplankDetail.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT1_AdjustListplankDetail.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT1_AdjustListplankDetail.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT1_AdjustListplankDetail.Process = (sqlDataReader["Process"]).ToString();
            return objT1_AdjustListplankDetail;
        }
        public T1_AdjustListplankDetail GenerateObjectR(SqlDataReader sqlDataReader)
        {
            objT1_AdjustListplankDetail = new T1_AdjustListplankDetail();
            //objT1_AdjustListplankDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objT1_AdjustListplankDetail.AdjustID = Convert.ToInt32(sqlDataReader["AdjustID"]);
            objT1_AdjustListplankDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT1_AdjustListplankDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_AdjustListplankDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT1_AdjustListplankDetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            objT1_AdjustListplankDetail.AdjustNo = (sqlDataReader["AdjustNo"]).ToString();
            objT1_AdjustListplankDetail.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT1_AdjustListplankDetail.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT1_AdjustListplankDetail.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT1_AdjustListplankDetail.Approval = (sqlDataReader["approval"]).ToString();
            objT1_AdjustListplankDetail.Process = (sqlDataReader["Process"]).ToString();
            return objT1_AdjustListplankDetail;
        }

    }
}
