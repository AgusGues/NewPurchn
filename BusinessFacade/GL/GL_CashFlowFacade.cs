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
    public class GL_CashFlowFacade : AbstractFacade
    {
        private GL_CashFlow  objGL_CashFlow = new GL_CashFlow();
        private ArrayList arrGL_CashFlow;
        private List<SqlParameter> sqlListParam;

        public GL_CashFlowFacade()
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
            string strSQL = "Select isnull(ID,0) as ID, CashFlowCode, CashFlowName from GL_CashFlow where isnull(RowStatus,0) >-1 order by CashFlowName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_CashFlow = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CashFlow.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CashFlow.Add(new GL_CashFlow());

            return arrGL_CashFlow;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select isnull(ID,0) as ID, CashFlowCode, CashFlowName from GL_CashFlow where isnull(RowStatus,0) >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_CashFlow = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CashFlow.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CashFlow.Add(new GL_CashFlow());
            return arrGL_CashFlow;
        }

        public GL_CashFlow GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_CashFlow = new GL_CashFlow();
            objGL_CashFlow.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_CashFlow.CashFlowCode = sqlDataReader["CashFlowCode"].ToString();
            objGL_CashFlow.CashFlowName = sqlDataReader["CashFlowName"].ToString();
            return objGL_CashFlow;
        }
    }
}
