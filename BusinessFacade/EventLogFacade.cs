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
    public class EventLogFacade : AbstractFacade
    {
        private EventLog objEventLog = new EventLog();
        private ArrayList arrEventLog;
        private List<SqlParameter> sqlListParam;

        public EventLogFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objEventLog = (EventLog)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ModulName", objEventLog.ModulName));
                sqlListParam.Add(new SqlParameter("@EventName", objEventLog.EventName));
                sqlListParam.Add(new SqlParameter("@DocumentNo", objEventLog.DocumentNo));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objEventLog.CreatedBy));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertEventLog");

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
            return 0;
        }

        public override int Delete(object objDomain)
        {

            return 0;
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from EventLog");
            strError = dataAccess.Error;
            arrEventLog = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrEventLog.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrEventLog.Add(new EventLog());

            return arrEventLog;
        }

        public EventLog RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from EventLog where Id = " + Id);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new EventLog();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from EventLog and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrEventLog = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrEventLog.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrEventLog.Add(new EventLog());

            return arrEventLog;
        }

        public EventLog GenerateObject(SqlDataReader sqlDataReader)
        {
            objEventLog = new EventLog();
            objEventLog.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objEventLog.ModulName = sqlDataReader["ModulName"].ToString();
            objEventLog.EventName = sqlDataReader["EventName"].ToString();
            objEventLog.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objEventLog.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objEventLog.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"].ToString());
            return objEventLog;
        }
        /**
         * Event Log Posting Accounting
         * added on 18-07-2016
         */
        public int InsertEvent(object objDomain)
        {
            int result = 0;
            objEventLog = (EventLog)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ModulName", objEventLog.ModulName));
            sqlListParam.Add(new SqlParameter("@UserName", objEventLog.CreatedBy));
            sqlListParam.Add(new SqlParameter("@Bulan", objEventLog.Bulan));
            sqlListParam.Add(new SqlParameter("@Tahun", objEventLog.Tahun));
            sqlListParam.Add(new SqlParameter("@Processe", objEventLog.EventName));
            sqlListParam.Add(new SqlParameter("@Keterangan", objEventLog.DocumentNo));
            sqlListParam.Add(new SqlParameter("@IPAddress", objEventLog.IPAddress));
            result = dataAccess.ProcessData(sqlListParam, "spEventLogPostingInsert");
            return result;
        }
        public ArrayList RetrievePostingLog(int Bulan,int Tahun)
        {
            arrEventLog = new ArrayList();
            string Bln = (Bulan == 0) ? "" : " and Bulan=" + Bulan;
            string strSQL = "select ModulName,UserID,PostingTime,PostingProcess,Keterangan,IPAddress,Bulan,Tahun from EventLogPosting " +
                             "where Tahun=" + Tahun + Bln + " order by PostingTime desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrEventLog.Add(GenerateEventObject(sdr));
                }
            }
            return arrEventLog;
        }
        private EventLog GenerateEventObject(SqlDataReader sdr)
        {
            objEventLog = new EventLog();
            objEventLog.ModulName = sdr["ModulName"].ToString();
            objEventLog.Keterangan = sdr["Keterangan"].ToString();
            objEventLog.IPAddress = sdr["IPAddress"].ToString();
            objEventLog.EventName = sdr["PostingProcess"].ToString();
            objEventLog.CreatedBy = sdr["UserID"].ToString();
            objEventLog.CreatedTime = DateTime.Parse(sdr["PostingTime"].ToString());
            return objEventLog;
        }
    }
}

