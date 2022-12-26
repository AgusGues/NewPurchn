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
using System.Web;

namespace Factory
{
    public class T1_AdjustFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private Domain.T1Adjust objT1_Adjust = new Domain.T1Adjust();
        private ArrayList arrT1_Adjust;
        private List<SqlParameter> sqlListParam;

        public T1_AdjustFacade(object objDomain)
            : base(objDomain)
        {
            objT1_Adjust = (Domain.T1Adjust)objDomain;
        }
        public T1_AdjustFacade()
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
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustNo", objT1_Adjust.AdjustNo ));
                sqlListParam.Add(new SqlParameter("@AdjustDate", objT1_Adjust.AdjustDate ));
                sqlListParam.Add(new SqlParameter("@NoBA", objT1_Adjust.NoBA ));
                sqlListParam.Add(new SqlParameter("@Keterangan", objT1_Adjust.Keterangan ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Adjust.CreatedBy  ));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_Adjust");
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
            string strSQL = "select * from T1_Adjust where convert(varchar,adjustdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Adjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Adjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Adjust.Add(new T1_Adjust());

            return arrT1_Adjust;
        }
        public int GetDocNo(DateTime tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(adjustno)as docnocount from ( select distinct adjustno from T1_Adjust where month(adjustdate) =" +
                tgltrans.Month + " and year(adjustdate)=" + tgltrans.Year + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Adjust = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T1Adjust GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_Adjust = new T1Adjust();
            objT1_Adjust.AdjustNo = (sqlDataReader["SJNO"]).ToString();
            objT1_Adjust.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objT1_Adjust.AdjustType = (sqlDataReader["AdjustType"]).ToString();
            objT1_Adjust.NoBA = (sqlDataReader["NoBA"]).ToString();
            objT1_Adjust.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objT1_Adjust.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            return objT1_Adjust;
        }

    }
}
