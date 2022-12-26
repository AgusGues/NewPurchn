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


namespace BusinessFacade
{
    public class BreakBMProblemFacade : AbstractFacade
    {
        private BreakBMProblem objBreakBMProblem = new BreakBMProblem();
        private ArrayList arrBreakBMProblem;
        private List<SqlParameter> sqlListParam;

        public BreakBMProblemFacade(object Domain)
            :base()
        {

        }

        public BreakBMProblemFacade()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBreakBMProblem = (BreakBMProblem)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LokasiProblem", objBreakBMProblem.LokasiProblem));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBreakBMProblem.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBMProblem");

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
                objBreakBMProblem = (BreakBMProblem)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMProblem.ID));
                sqlListParam.Add(new SqlParameter("@LokasiProblem", objBreakBMProblem.LokasiProblem));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMProblem.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBMProblem");

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
                objBreakBMProblem = (BreakBMProblem)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMProblem.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMProblem.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBMProblem");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMProblem where RowStatus = 0");
            strError = dataAccess.Error;
            arrBreakBMProblem = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMProblem.Add(GenerateObject(sqlDataReader));
                }
            }
            else

                arrBreakBMProblem.Add(new BreakBMProblem());

            return arrBreakBMProblem;

        }

        public BreakBMProblem RetrieveByCode(string lokasiProblem)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMProblem where RowStatus = 0 and LokasiProblem= '" + lokasiProblem + "'");
            strError = dataAccess.Error;
            arrBreakBMProblem = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new BreakBMProblem();

        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMProblem where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrBreakBMProblem = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMProblem.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMProblem.Add(new BreakBMProblem());

            return arrBreakBMProblem;
        }

        public BreakBMProblem GenerateObject(SqlDataReader sqlDataReader)
        {
            objBreakBMProblem = new BreakBMProblem();
            objBreakBMProblem.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMProblem.LokasiProblem = sqlDataReader["LokasiProblem"].ToString();
            objBreakBMProblem.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMProblem.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMProblem.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMProblem.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMProblem.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMProblem;
        }



    }
}
