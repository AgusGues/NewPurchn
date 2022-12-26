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
    public class TermOfPayFacade : AbstractFacade
    {
        private TermOfPay objTermOfPay = new TermOfPay();
        private ArrayList arrTermOfPay;
        private List<SqlParameter> sqlListParam;


        public TermOfPayFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objTermOfPay = (TermOfPay)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TermPay", objTermOfPay.TermPay));
    
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertTermOfPay");

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
            try
            {
                objTermOfPay = (TermOfPay)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TermPay", objTermOfPay.TermPay));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateTermOfPay");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objTermOfPay = (TermOfPay)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTermOfPay.ID));
               

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteTermOfPay");

                strError = dataAccess.Error;

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TermPay from TermOfPay as A");
            strError = dataAccess.Error;
            arrTermOfPay = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTermOfPay.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTermOfPay.Add(new TermOfPay());

            return arrTermOfPay;
        }

        public TermOfPay RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.TermPay from TermOfPay as A where A.ID = " + Id);
            strError = dataAccess.Error;
            arrTermOfPay = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TermOfPay();
        }
        public int getterm(string term)
        {
            int result = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(A.ID) jml from TermOfPay as A where A.TermPay = '" + term +"'");
            strError = dataAccess.Error;
            arrTermOfPay = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = Convert.ToInt32(sqlDataReader["jml"]);
                }
            }

            return result;
        }
        public TermOfPay GenerateObject(SqlDataReader sqlDataReader)
        {
            objTermOfPay = new TermOfPay();
            objTermOfPay.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTermOfPay.TermPay = sqlDataReader["TermPay"].ToString();
            

            return objTermOfPay;
        }
    }
}
