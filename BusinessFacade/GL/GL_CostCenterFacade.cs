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
    public class GL_CostCenterFacade : AbstractFacade
    {
        private GL_CostCenter  objGL_CostCenter = new GL_CostCenter();
        private ArrayList arrGL_CostCenter;
        private List<SqlParameter> sqlListParam;

        public GL_CostCenterFacade()
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
            string strSQL = "Select isnull(ID,0) as ID, CostCenterCode, CostCenterName from GL_CostCenter where isnull(RowStatus,0) >-1 order by CostCentercode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_CostCenter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CostCenter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CostCenter.Add(new GL_CostCenter());

            return arrGL_CostCenter;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select isnull(ID,0) as ID, CostCenterCode, CostCenterName from GL_CostCenter where isnull(RowStatus,0) >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_CostCenter = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CostCenter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CostCenter.Add(new GL_CostCenter());
            return arrGL_CostCenter;
        }

        public GL_CostCenter GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_CostCenter = new GL_CostCenter();
            objGL_CostCenter.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_CostCenter.CostCenterCode = sqlDataReader["CostCenterCode"].ToString();
            objGL_CostCenter.CostCenterName = sqlDataReader["CostCenterName"].ToString();
            return objGL_CostCenter;
        }
    }
}
