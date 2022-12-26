using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace Factory
{
    public class T3_SJ_CMFacade : AbstractFacade
    {
        private T3_SJ_CM objT3_SJ_CM = new T3_SJ_CM();
        private ArrayList arrT3_SJ_CM;
        private List<SqlParameter> sqlListParam;

        public T3_SJ_CMFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objT3_SJ_CM = (T3_SJ_CM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratjalanNo", objT3_SJ_CM.SuratjalanNo));
                sqlListParam.Add(new SqlParameter("@TglKirim", objT3_SJ_CM.TglKirim));
                sqlListParam.Add(new SqlParameter("@Customer", objT3_SJ_CM.Customer));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_SJ_CM.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT3_SJ_CM");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public T3_SJ_CM RetrieveBySJno(string sjno)
        {
            string strSQL = "SELECT * from T3_SJ_CM where suratjalanno='" + sjno + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new T3_SJ_CM();
        }

        public T3_SJ_CM GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_SJ_CM = new T3_SJ_CM();
            objT3_SJ_CM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_SJ_CM.SuratjalanNo  = sqlDataReader["SuratJalanNo"].ToString();
            objT3_SJ_CM.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"]);
            objT3_SJ_CM.Customer = sqlDataReader["Customer"].ToString();
            return objT3_SJ_CM;
        }

    }
}
