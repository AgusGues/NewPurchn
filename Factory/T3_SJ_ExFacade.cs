using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
namespace Factory
{
    public class T3_SJ_ExFacade : AbstractFacade
    {
        private T3_SJ_Ex objT3_SJ_Ex = new T3_SJ_Ex();
        private ArrayList arrT3_SJ_Ex;
        private List<SqlParameter> sqlListParam;

        public T3_SJ_ExFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objT3_SJ_Ex = (T3_SJ_Ex)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratjalanNo", objT3_SJ_Ex.SuratjalanNo));
                sqlListParam.Add(new SqlParameter("@TglKirim", objT3_SJ_Ex.TglKirim));
                sqlListParam.Add(new SqlParameter("@Customer", objT3_SJ_Ex.Customer));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_SJ_Ex.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT3_SJ_Ex");
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

        public T3_SJ_Ex RetrieveBySJno(string sjno)
        {
            string strSQL = "SELECT * from T3_SJ_Ex where suratjalanno='" + sjno + "'";
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
            return new T3_SJ_Ex();
        }

        public T3_SJ_Ex GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_SJ_Ex = new T3_SJ_Ex();
            objT3_SJ_Ex.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_SJ_Ex.SuratjalanNo  = sqlDataReader["SuratJalanNo"].ToString();
            objT3_SJ_Ex.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"]);
            objT3_SJ_Ex.Customer = sqlDataReader["Customer"].ToString();
            return objT3_SJ_Ex;
        }

    }
}
