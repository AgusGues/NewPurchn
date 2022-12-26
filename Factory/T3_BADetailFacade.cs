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
    public class T3_BADetailFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_BADetail objT3_BADetail = new T3_BADetail();
        private ArrayList arrT3_BADetail;
        private List<SqlParameter> sqlListParam;

        public T3_BADetailFacade(object objDomain)
            : base(objDomain)
        {
            objT3_BADetail = (T3_BADetail)objDomain;
        }
        public T3_BADetailFacade()
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
                objT3_BADetail = (T3_BADetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BAID", objT3_BADetail.BAID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_BADetail.ItemID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_BADetail.LokID));
                sqlListParam.Add(new SqlParameter("@AdjustType", objT3_BADetail.AdjustType));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_BADetail.QtyIn));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_BADetail.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_BADetail.CreatedBy));
                sqlListParam.Add(new SqlParameter("@keterangan", objT3_BADetail.Keterangan));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_BADetail");
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
                objT3_BADetail = (T3_BADetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT3_BADetail.ID));
                sqlListParam.Add(new SqlParameter("@Apv", objT3_BADetail.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objT3_BADetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT3_BAdetail");
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
                objT3_BADetail = (T3_BADetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT3_BADetail.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_BADetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteT3_BADetail");
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
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.BANo, A.BADate, A.NoBA, A.Keterangan,B.BAType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  "+
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_BA AS A INNER JOIN T3_BADetail AS B ON A.ID = B.BAID where B.rowstatus>-1 and convert(varchar,A.BAdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BADetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BADetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_BADetail.Add(new T3_BADetail());

            return arrT3_BADetail;
        }
        public ArrayList RetrieveByNoBA(string No)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.BANo, A.BADate, A.NoBA, A.Keterangan,B.BAType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_BA AS A INNER JOIN T3_BADetail AS B ON A.ID = B.BAID where  B.rowstatus>-1 and A.NoBA='" + No + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BADetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BADetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_BADetail.Add(new T3_BADetail());

            return arrT3_BADetail;
        }
        public ArrayList RetrieveByapv(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.BANo, A.BADate, A.NoBA, A.Keterangan,B.BAType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_BA AS A INNER JOIN T3_BADetail AS B ON A.ID = B.BAID where  B.rowstatus>-1 and B.apv=" + apv;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BADetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BADetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_BADetail.Add(new T3_BADetail());

            return arrT3_BADetail;
        }
       
        public ArrayList RetrieveByLokasi(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.BANo, A.BADate, A.NoBA, A.Keterangan,B.BAType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_BA AS A INNER JOIN T3_BADetail AS B ON A.ID = B.BAID where  B.rowstatus>-1 and B.lokid in (select id from fc_lokasi where lokasi='" + apv + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BADetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BADetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_BADetail.Add(new T3_BADetail());

            return arrT3_BADetail;
        }
        public ArrayList RetrieveByPartno(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid, A.BANo, A.BADate, A.NoBA, A.Keterangan,B.BAType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_BA AS A INNER JOIN T3_BADetail AS B ON A.ID = B.BAID where  B.rowstatus>-1 and B.itemid in (select id from fc_items where partno='" + apv + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BADetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BADetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_BADetail.Add(new T3_BADetail());

            return arrT3_BADetail;
        }
        public T3_BADetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_BADetail = new T3_BADetail();
            objT3_BADetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_BADetail.BAID = Convert.ToInt32(sqlDataReader["BAID"]);
            objT3_BADetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_BADetail.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);
            objT3_BADetail.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT3_BADetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            //objT3_BADetail.BANo = (sqlDataReader["BANo"]).ToString();
            //objT3_BADetail.BADate = Convert.ToDateTime(sqlDataReader["BADate"]);
            objT3_BADetail.BAType = (sqlDataReader["BAType"]).ToString();
            return objT3_BADetail;
        }
        public T3_BADetail GenerateObjectR(SqlDataReader sqlDataReader)
        {
            objT3_BADetail = new T3_BADetail();
            //objT3_BADetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objT3_BADetail.BAID = Convert.ToInt32(sqlDataReader["BAID"]);
            objT3_BADetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_BADetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_BADetail.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);
            objT3_BADetail.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT3_BADetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            //objT3_BADetail.BANo = (sqlDataReader["BANo"]).ToString();
            //objT3_BADetail.BADate = Convert.ToDateTime(sqlDataReader["BADate"]);
            objT3_BADetail.BAType = (sqlDataReader["BAType"]).ToString();
            return objT3_BADetail;
        }
    }
}
