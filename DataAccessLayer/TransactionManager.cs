using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class TransactionManager : DbConnection
    {
        public TransactionManager(string strConnection)
            : base(strConnection)
        {
        }

        SqlTransaction sqlTrans;

        
        public void BeginTransaction()
        {          
            sqlTrans = sqlConnection.BeginTransaction();
        }


        public void CommitTransaction()
        {
            sqlTrans.Commit();
        }

        public void RollbackTransaction()
        {
            //sqlTrans.Rollback();
        }

        public void EndTransaction()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }

        public int DoTransaction(List<SqlParameter> sqlParam, string spName)
        {
            try
            {
                BuildParameter(sqlParam, spName);
                sqlCommand.Transaction = sqlTrans;
                return Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                _strError = ex.Message;
                EndTransaction();

                return -1;
            }
        }

    }
}
