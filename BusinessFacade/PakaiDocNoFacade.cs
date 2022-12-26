using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{

    public class PakaiDocNoFacade : AbstractTransactionFacade
    {
        private PakaiDocNo objPakaiDocNo = new PakaiDocNo();
        private ArrayList arrPakaiDocNo;
        private List<SqlParameter> sqlListParam;

         public PakaiDocNoFacade(object objDomain)
            : base(objDomain)
        {
            objPakaiDocNo = (PakaiDocNo)objDomain;
        }
         public PakaiDocNoFacade()
        {
        }
         public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                objPakaiDocNo = (PakaiDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PakaiCode", objPakaiDocNo.PakaiCode));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objPakaiDocNo.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objPakaiDocNo.YearPeriod));
                sqlListParam.Add(new SqlParameter("@NoUrut", objPakaiDocNo.NoUrut));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPakaiDocNo");
                strError = transManager.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

         public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            //try
            //{
                objPakaiDocNo = (PakaiDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakaiDocNo.ID));
                sqlListParam.Add(new SqlParameter("@NoUrut", objPakaiDocNo.NoUrut));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePakaiDocNo");
                strError = transManager.Error;
                return intResult;
            //}
            //catch (Exception ex)
            //{
            //    strError = ex.Message;
            //    return -1;
            //}
        }



        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,DC,BankAccountID,TransDocID,MonthPeriod,YearPeriod,NoUrut from TransDocNo");
            strError = dataAccess.Error;
            arrPakaiDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDocNo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDocNo.Add(new PakaiDocNo());

            return arrPakaiDocNo;

        }

        public int RetrieveByNoUrut(string PakaiCode, int MonthPeriod, int YearPeriod)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select NoUrut from PakaiDocNo where PakaiCode='" + PakaiCode + "' and MonthPeriod=" + MonthPeriod + " and YearPeriod=" + YearPeriod;
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }

        public int RetrieveByPakaiCodeNf(int bln, int thn, string PakaiCode)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select * from PakaiDocNo where PakaiCode='" + PakaiCode + "' and MonthPeriod=" + bln + " and YearPeriod=" + thn;
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }

        public PakaiDocNo RetrieveByPakaiCode(int bln, int thn, string PakaiCode)
        {
            string strsql = "select * from PakaiDocNo where PakaiCode='" + PakaiCode + "' and MonthPeriod=" + bln + " and YearPeriod=" + thn;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPakaiDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new PakaiDocNo();
        }

        public PakaiDocNo GenerateObject(SqlDataReader sqlDataReader)
        {
            objPakaiDocNo = new PakaiDocNo();
            objPakaiDocNo.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPakaiDocNo.PakaiCode = sqlDataReader["PakaiCode"].ToString();
            objPakaiDocNo.MonthPeriod = Convert.ToInt32(sqlDataReader["MonthPeriod"]);
            objPakaiDocNo.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objPakaiDocNo.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);

            return objPakaiDocNo;
        }


        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }


    }
}
