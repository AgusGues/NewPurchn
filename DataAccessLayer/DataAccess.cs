using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DataAccess : DbConnection
    {
        public DataAccess(string strConnection)
            : base(strConnection)
        {

        }

        public int ProcessData(List<SqlParameter> sqlParam, string spName)
        {
            try
            {                
                BuildParameter(sqlParam, spName);
                return Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                return -1;
            }            
        }
        public int ProcessData(List<SqlParameter> sqlParam, string spName, bool ReturID)
        {
            try
            {
                BuildParameter(sqlParam, spName);
                return Convert.ToInt32(sqlCommand.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                return -1;
            }
        }
        public int ProcessDataReturnID(List<SqlParameter> sqlParam, string spName)
        {
            try
            {                
                BuildParameter(sqlParam, spName);
                return (int)sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                return -1;
            }
        }

        public object RetrieveDataReturnValue(List<SqlParameter> sqlParam, string spName)
        {
            try
            {                
                BuildParameter(sqlParam, spName);
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                return -1;
            }           
        }


        public SqlDataReader RetrieveDataByParameter(List<SqlParameter> sqlParam, string spName)
        {
            try
            {                
                BuildParameter(sqlParam, spName);
                return sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                SqlDataReader sqlDataReader = null;
                return sqlDataReader;
            }
        }

        public SqlDataReader RetrieveDataByString(string strQuery)
        {
            try
            {             
                sqlCommand = new SqlCommand(strQuery, sqlConnection);
                sqlCommand.CommandTimeout = 0;
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                SqlDataReader sqlDataReader = null;
                return sqlDataReader;
            }
        }

        public DataTable RetrieveDataTable(string strQuery)
        {
            try
            {
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(strQuery, sqlConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                DataTable dt = new DataTable();
                return dt;
            }         
        }
    }
}
