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
    public class SPPNumberFacade : AbstractFacade
    {
        private SPPNumber objSPPNumber = new SPPNumber();
        private ArrayList arrSPPNumber;
        private List<SqlParameter> sqlListParam;


        public SPPNumberFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            //try
            //{
            //    objSJNumber = (SJNumber)objDomain;
            //    sqlListParam = new List<SqlParameter>();
            //    sqlListParam.Add(new SqlParameter("@PropinsiID", objSJNumber.PropinsiID));
            //    sqlListParam.Add(new SqlParameter("@SJNumberName", objSJNumber.SJNumberName));
            //    sqlListParam.Add(new SqlParameter("@CreatedBy", objSJNumber.CreatedBy));

            //    int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSJNumber");

            //    strError = dataAccess.Error;

            //    return intResult;

            //}
            //catch (Exception ex)
            //{
            //    strError = ex.Message;
            //    return -1;
            //}

            return 0;
        }

        public override int Update(object objDomain)
        {
            try
            {
                objSPPNumber = (SPPNumber)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupsPurchnID", objSPPNumber.GroupsPurchnID));
                sqlListParam.Add(new SqlParameter("@SPPCounter", objSPPNumber.SPPCounter));
                sqlListParam.Add(new SqlParameter("@POCounter", objSPPNumber.POCounter));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPPNumber.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Flag", objSPPNumber.Flag));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSPPNumber");

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
            return 0;
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SPPNumber");
            strError = dataAccess.Error;
            arrSPPNumber = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPNumber.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSPPNumber.Add(new SPPNumber());

            return arrSPPNumber;
        }

        public SPPNumber RetrieveByGroupsID(int groupsPurchnID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SPPNumber where GroupsPurchnID = " + groupsPurchnID);
            strError = dataAccess.Error;
            arrSPPNumber = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPPNumber();
        }


        public SPPNumber GenerateObject(SqlDataReader sqlDataReader)
        {
            objSPPNumber = new SPPNumber();
            objSPPNumber.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSPPNumber.GroupsPurchnID = Convert.ToInt32(sqlDataReader["GroupsPurchnID"]);
            objSPPNumber.SPPCounter = Convert.ToInt32(sqlDataReader["SPPCounter"]);
            objSPPNumber.POCounter = Convert.ToInt32(sqlDataReader["POCounter"]);
            objSPPNumber.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSPPNumber.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSPPNumber;

        }
    }
}
