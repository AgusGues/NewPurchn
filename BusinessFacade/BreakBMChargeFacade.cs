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
    public class BreakBMChargeFacade : AbstractFacade
    {
        private BreakBMCharge objBreakBMCharge = new BreakBMCharge();
        private ArrayList arrBreakBMCharge;
        private List<SqlParameter> sqlListParam;

        public BreakBMChargeFacade(object domain)
            :base()
        {

        }

        public BreakBMChargeFacade()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBreakBMCharge = (BreakBMCharge)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LokasiCharge", objBreakBMCharge.LokasiCharge));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBreakBMCharge.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBMCharge");

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
                objBreakBMCharge = (BreakBMCharge)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMCharge.ID));
                sqlListParam.Add(new SqlParameter("@LokasiCharge", objBreakBMCharge.LokasiCharge));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMCharge.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "@spUpdateBMCharge");

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
                objBreakBMCharge = (BreakBMCharge)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMCharge.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMCharge.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBMCharge");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMCharge where RowStatus = 0");
            strError = dataAccess.Error;
            arrBreakBMCharge = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMCharge.Add(GenerateObject(sqlDataReader));
                }
            }
            else

                arrBreakBMCharge.Add(new BreakBMCharge());

            return arrBreakBMCharge;

        }

        public BreakBMCharge RetrieveByCode(string lokasiCharge)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMCharge where RowStatus = 0 and LokasiCharge= '" + lokasiCharge + "'");
            strError = dataAccess.Error;
            arrBreakBMCharge = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new BreakBMCharge();

        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMCharge where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrBreakBMCharge = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMCharge.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMCharge.Add(new BreakBMCharge());

            return arrBreakBMCharge;
        }

        public BreakBMCharge GenerateObject(SqlDataReader sqlDataReader)
        {
            objBreakBMCharge = new BreakBMCharge();
            objBreakBMCharge.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMCharge.LokasiCharge = sqlDataReader["LokasiCharge"].ToString();
            objBreakBMCharge.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMCharge.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMCharge.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMCharge.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMCharge.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMCharge;
        }

    }
}
