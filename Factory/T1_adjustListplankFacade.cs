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
    public class T1_AdjustListplankFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_AdjustListplank objT1_AdjustListplank = new T1_AdjustListplank();
        private ArrayList arrT1_AdjustListplank;
        private List<SqlParameter> sqlListParam;

        public T1_AdjustListplankFacade(object objDomain)
            : base(objDomain)
        {
            objT1_AdjustListplank = (T1_AdjustListplank)objDomain;
        }
        public T1_AdjustListplankFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT1_AdjustListplank = (T1_AdjustListplank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustNo", objT1_AdjustListplank.AdjustNo ));
                sqlListParam.Add(new SqlParameter("@AdjustDate", objT1_AdjustListplank.AdjustDate ));
                sqlListParam.Add(new SqlParameter("@NoBA", objT1_AdjustListplank.NoBA ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_AdjustListplank.CreatedBy  ));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_AdjustListplank");
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
            string strSQL = "select * from T1_AdjustListplank where convert(varchar,adjustdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_AdjustListplank.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_AdjustListplank.Add(new T1_AdjustListplank());

            return arrT1_AdjustListplank;
        }
        public int GetDocNo(DateTime tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(adjustno)as docnocount from ( select distinct adjustno from T1_AdjustListplank where month(adjustdate) =" +
                tgltrans.Month + " and year(adjustdate)=" + tgltrans.Year + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_AdjustListplank = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T1_AdjustListplank GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_AdjustListplank = new T1_AdjustListplank();
            objT1_AdjustListplank.AdjustNo = (sqlDataReader["SJNO"]).ToString();
            objT1_AdjustListplank.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT1_AdjustListplank.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT1_AdjustListplank.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT1_AdjustListplank.Process = (sqlDataReader["Process"]).ToString();
            objT1_AdjustListplank.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            return objT1_AdjustListplank;
        }
    }
}
