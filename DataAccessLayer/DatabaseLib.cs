using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace DataAccessLayer
{
    public class DatabaseLib
    {

        public DatabaseLib()
        {

        }
        public string strConn { get; set; }
        public SqlDataReader DataReader(string Query)
        {
            DataAccess dta = new DataAccess(this.strConn);
            SqlDataReader sdr = dta.RetrieveDataByString(Query);
            return sdr;
        }

    }
}
