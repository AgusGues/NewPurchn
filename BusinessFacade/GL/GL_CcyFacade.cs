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
    public class GL_CcyFacade : AbstractFacade
    {
        private GL_Ccy  objGL_Ccy = new GL_Ccy();
        private ArrayList arrGL_Ccy;
        private List<SqlParameter> sqlListParam;

        public GL_CcyFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Ccy where isnull(RowStatus,0) >-1 order by Ccycode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Ccy = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Ccy.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Ccy.Add(new GL_Ccy());

            return arrGL_Ccy;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Ccy where RowStatus >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Ccy = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Ccy.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Ccy.Add(new GL_Ccy());
            return arrGL_Ccy;
        }

        public GL_Ccy GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Ccy = new GL_Ccy();
            objGL_Ccy.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Ccy.CcyCode = sqlDataReader["CcyCode"].ToString();
            objGL_Ccy.CcyName = sqlDataReader["CcyName"].ToString();
            return objGL_Ccy;
        }
    }
}
