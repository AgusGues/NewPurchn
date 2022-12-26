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
    public class SisiFacade : AbstractFacade
    {
        private Sisi objSisi = new Sisi();
        private ArrayList arrSisi;
        private List<SqlParameter> sqlListParam;


        public SisiFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSisi = (Sisi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SisiDescription", objSisi.SisiDescription));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSisi.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSisi");

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
                objSisi = (Sisi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSisi.ID));
                sqlListParam.Add(new SqlParameter("@SisiDescription", objSisi.SisiDescription));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSisi.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSisi");

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
                objSisi = (Sisi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSisi.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSisi.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteSisi");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from Sisi where RowStatus = 0");
            strError = dataAccess.Error;
            arrSisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSisi.Add(new Sisi());

            return arrSisi;
        }

        public Sisi RetrieveByCode(string sisiDescription)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Sisi where RowStatus = 0 and SisiDescription = '" + sisiDescription + "'");
            strError = dataAccess.Error;
            arrSisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Sisi();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Sisi where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSisi.Add(new Sisi());

            return arrSisi;
        }

        public Sisi GenerateObject(SqlDataReader sqlDataReader)
        {
            objSisi = new Sisi();
            objSisi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSisi.SisiDescription = sqlDataReader["SisiDescription"].ToString();
            objSisi.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objSisi.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSisi.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSisi.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSisi.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSisi;

        }
    }
}
