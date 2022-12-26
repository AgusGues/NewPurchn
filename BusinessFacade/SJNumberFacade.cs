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
    public class SJNumberFacade : AbstractFacade
    {
        private SJNumber objSJNumber = new SJNumber();
        private ArrayList arrSJNumber;
        private List<SqlParameter> sqlListParam;


        public SJNumberFacade()
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
                objSJNumber = (SJNumber)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DepoID", objSJNumber.DepoID));
                sqlListParam.Add(new SqlParameter("@SJCounter1", objSJNumber.SJCounter1));
                sqlListParam.Add(new SqlParameter("@SJCounter2", objSJNumber.SJCounter2));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSJNumber.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Flag", objSJNumber.Flag));
                sqlListParam.Add(new SqlParameter("@Tahun", objSJNumber.Tahun)); 

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSJNumber");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SJNumber");
            strError = dataAccess.Error;
            arrSJNumber = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSJNumber.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSJNumber.Add(new SJNumber());

            return arrSJNumber;
        }

        public SJNumber RetrieveByDepoID(int depoID, int Th)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SJNumber where DepoID =" + depoID + " and Tahun=" + Th);
            strError = dataAccess.Error;
            arrSJNumber = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SJNumber();
        }
      
      
        public SJNumber GenerateObject(SqlDataReader sqlDataReader)
        {
            objSJNumber = new SJNumber();
            objSJNumber.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSJNumber.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSJNumber.SJCounter1 = Convert.ToInt32(sqlDataReader["SJCounter1"]);
            objSJNumber.SJCounter2 = Convert.ToInt32(sqlDataReader["SJCounter2"]);
            objSJNumber.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSJNumber.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objSJNumber.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            return objSJNumber;

        }
    }
}
