using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DbConnection
    {
        public DbConnection(string strConnection)
        {
            sqlConnection = new SqlConnection(strConnection);           
            OpenConnection();
        }
        
        protected string _strError = string.Empty;
        protected SqlConnection sqlConnection;
        protected SqlCommand sqlCommand;

        public string Error
        {
            get
            {
                return _strError;
            }            
        }

        protected void BuildParameter(List<SqlParameter> sqlParam,string spName)
        {
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            foreach (SqlParameter param in sqlParam)
            {
                sqlCommand.Parameters.Add(param);               
            }
        }

        protected void BuildQueryParameter(string strQuery)
        {
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = strQuery;         
        }        

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        public void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();

            sqlConnection.Open();
        }
    }
}
