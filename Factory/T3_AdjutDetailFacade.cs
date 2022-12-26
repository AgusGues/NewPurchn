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
    public class T3_AdjustDetailFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_AdjustDetail objT3_AdjustDetail = new T3_AdjustDetail();
        private ArrayList arrT3_AdjustDetail;
        private List<SqlParameter> sqlListParam;

        public T3_AdjustDetailFacade(object objDomain)
            : base(objDomain)
        {
            objT3_AdjustDetail = (T3_AdjustDetail)objDomain;
        }
        public T3_AdjustDetailFacade()
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
                objT3_AdjustDetail = (T3_AdjustDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustID", objT3_AdjustDetail.AdjustID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_AdjustDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_AdjustDetail.LokID ));
                sqlListParam.Add(new SqlParameter("@AdjustType", objT3_AdjustDetail.AdjustType));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_AdjustDetail.QtyIn));
                sqlListParam.Add(new SqlParameter("@Apv", objT3_AdjustDetail.Apv ));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_AdjustDetail.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_AdjustDetail.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SA", objT3_AdjustDetail.SA));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_AdjustDetail");
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
                objT3_AdjustDetail = (T3_AdjustDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT3_AdjustDetail.ID));
                sqlListParam.Add(new SqlParameter("@Apv", objT3_AdjustDetail.Apv));
                sqlListParam.Add(new SqlParameter("@Sa", objT3_AdjustDetail.SA));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objT3_AdjustDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT3_Adjustdetail");
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
                objT3_AdjustDetail = (T3_AdjustDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT3_AdjustDetail.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_AdjustDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteT3_AdjustDetail");
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
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.AdjustNo, A.AdjustDate, A.NoBA, A.Keterangan,B.AdjustType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  "+
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_Adjust AS A INNER JOIN T3_AdjustDetail AS B ON A.ID = B.AdjustID where B.rowstatus>-1 and convert(varchar,A.Adjustdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_AdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_AdjustDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_AdjustDetail.Add(new T3_AdjustDetail());

            return arrT3_AdjustDetail;
        }
        public ArrayList RetrieveByNoBA(string No)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.AdjustNo, A.AdjustDate, A.NoBA, A.Keterangan,B.AdjustType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_Adjust AS A INNER JOIN T3_AdjustDetail AS B ON A.ID = B.AdjustID where  B.rowstatus>-1 and A.NoBA='" + No + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_AdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_AdjustDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_AdjustDetail.Add(new T3_AdjustDetail());

            return arrT3_AdjustDetail;
        }
        public ArrayList RetrieveByapv(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.AdjustNo, A.AdjustDate, A.NoBA, A.Keterangan,B.AdjustType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_Adjust AS A INNER JOIN T3_AdjustDetail AS B ON A.ID = B.AdjustID where  B.rowstatus>-1 and B.apv=" + apv;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_AdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_AdjustDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_AdjustDetail.Add(new T3_AdjustDetail());

            return arrT3_AdjustDetail;
        }
       
        public ArrayList RetrieveByLokasi(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid,A.AdjustNo, A.AdjustDate, A.NoBA, A.Keterangan,B.AdjustType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_Adjust AS A INNER JOIN T3_AdjustDetail AS B ON A.ID = B.AdjustID where  B.rowstatus>-1 and B.lokid in (select id from fc_lokasi where lokasi='" + apv + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_AdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_AdjustDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_AdjustDetail.Add(new T3_AdjustDetail());

            return arrT3_AdjustDetail;
        }
        public ArrayList RetrieveByPartno(string apv)
        {
            string strSQL = "SELECT B.ID,B.itemid,B.lokid, A.AdjustNo, A.AdjustDate, A.NoBA, A.Keterangan,B.AdjustType, " +
                "case when B.ItemID>0 then(select partno from FC_Items where ID =B.ItemID) end partno,  " +
                "case when B.LokID>0 then (select lokasi from FC_Lokasi where ID=B.LokID ) end lokasi, B.QtyIn, B.QtyOut,case when B.Apv>0 then 'Accounting' else 'Admin' end Approval " +
                "FROM  T3_Adjust AS A INNER JOIN T3_AdjustDetail AS B ON A.ID = B.AdjustID where  B.rowstatus>-1 and B.itemid in (select id from fc_items where partno='" + apv + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_AdjustDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_AdjustDetail.Add(GenerateObjectR(sqlDataReader));
                }
            }
            else
                arrT3_AdjustDetail.Add(new T3_AdjustDetail());

            return arrT3_AdjustDetail;
        }
        public T3_AdjustDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_AdjustDetail = new T3_AdjustDetail();
            objT3_AdjustDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_AdjustDetail.AdjustID = Convert.ToInt32(sqlDataReader["AdjustID"]);
            objT3_AdjustDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_AdjustDetail.LokID = Convert.ToInt32(sqlDataReader["LokID"]);
            objT3_AdjustDetail.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);
            objT3_AdjustDetail.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT3_AdjustDetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            objT3_AdjustDetail.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT3_AdjustDetail.AdjustNo = (sqlDataReader["AdjustNo"]).ToString();
            objT3_AdjustDetail.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT3_AdjustDetail.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT3_AdjustDetail.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT3_AdjustDetail.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            return objT3_AdjustDetail;
        }
        public T3_AdjustDetail GenerateObjectR(SqlDataReader sqlDataReader)
        {
            objT3_AdjustDetail = new T3_AdjustDetail();
            //objT3_AdjustDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objT3_AdjustDetail.AdjustID = Convert.ToInt32(sqlDataReader["AdjustID"]);
            objT3_AdjustDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_AdjustDetail.LokID = Convert.ToInt32(sqlDataReader["LokID"]);
            objT3_AdjustDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_AdjustDetail.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);
            objT3_AdjustDetail.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT3_AdjustDetail.PartNo = (sqlDataReader["PartNo"]).ToString();
            objT3_AdjustDetail.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT3_AdjustDetail.AdjustNo = (sqlDataReader["AdjustNo"]).ToString();
            objT3_AdjustDetail.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT3_AdjustDetail.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT3_AdjustDetail.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT3_AdjustDetail.Approval = (sqlDataReader["approval"]).ToString();
            objT3_AdjustDetail.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            return objT3_AdjustDetail;
        }


    }
}
