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
    public class T3_BAFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_BA objT3_BA = new T3_BA();
        private ArrayList arrT3_BA;
        private List<SqlParameter> sqlListParam;

        public T3_BAFacade(object objDomain)
            : base(objDomain)
        {
            objT3_BA = (T3_BA)objDomain;
        }
        public T3_BAFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_BA = (T3_BA)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BANo", objT3_BA.BANo ));
                sqlListParam.Add(new SqlParameter("@BADate", objT3_BA.BADate ));
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_BA.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_BA.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_BA");
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
            string strSQL = "select * from t3_BA where convert(varchar,BAdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_BA.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_BA.Add(new T3_BA());

            return arrT3_BA;
        }
        public int GetDocNo(DateTime tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(BAno)as docnocount from ( select distinct BAno from t3_BA where month(BAdate) =" +
                tgltrans.Month + " and year(BAdate)=" + tgltrans.Year + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_BA = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T3_BA GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_BA = new T3_BA();
            objT3_BA.BANo = (sqlDataReader["SJNO"]).ToString();
            objT3_BA.BADate = Convert.ToDateTime(sqlDataReader["BADate"]);
            objT3_BA.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objT3_BA.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            return objT3_BA;
        }

    }
}
