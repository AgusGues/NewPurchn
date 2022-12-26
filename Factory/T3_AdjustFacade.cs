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
    public class T3_AdjustFacade : BusinessFacade.AbstractTransactionFacadeF
    {
       private T3_Adjust objT3_Adjust = new T3_Adjust();
        private ArrayList arrT3_Adjust;
        private List<SqlParameter> sqlListParam;

        public T3_AdjustFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Adjust = (T3_Adjust)objDomain;
        }
        public T3_AdjustFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Adjust = (T3_Adjust)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustNo", objT3_Adjust.AdjustNo ));
                sqlListParam.Add(new SqlParameter("@AdjustDate", objT3_Adjust.AdjustDate ));
                sqlListParam.Add(new SqlParameter("@NoBA", objT3_Adjust.NoBA ));
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_Adjust.Keterangan ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Adjust.CreatedBy  ));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Adjust");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
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
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
          public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveByTgl(string tgl)
        {
            string strSQL = "select * from t3_adjust where convert(varchar,adjustdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Adjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Adjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Adjust.Add(new T3_Adjust());

            return arrT3_Adjust;
        }
        public int GetDocNo(DateTime tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(adjustno)as docnocount from ( select distinct adjustno from t3_adjust where month(adjustdate) =" +
                tgltrans.Month + " and year(adjustdate)=" + tgltrans.Year + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Adjust = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T3_Adjust GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Adjust = new T3_Adjust();
            objT3_Adjust.AdjustNo = (sqlDataReader["SJNO"]).ToString();
            objT3_Adjust.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT3_Adjust.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT3_Adjust.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT3_Adjust.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objT3_Adjust.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            return objT3_Adjust;
        }

    }
}
