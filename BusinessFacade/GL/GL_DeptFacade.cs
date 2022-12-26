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
    public class GL_DeptFacade : AbstractFacade
    {
        private GL_Dept  objGL_Dept = new GL_Dept();
        private ArrayList arrGL_Dept;
        private List<SqlParameter> sqlListParam;

        public GL_DeptFacade()
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
            string strSQL = "Select isnull(ID,0) as ID, DeptCode, DeptName from GL_Dept where isnull(RowStatus,0) >-1 order by Deptcode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Dept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Dept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Dept.Add(new GL_Dept());

            return arrGL_Dept;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select isnull(ID,0) as ID, DeptCode, DeptName from GL_Dept where isnull(RowStatus,0) >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Dept = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Dept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Dept.Add(new GL_Dept());
            return arrGL_Dept;
        }

        public GL_Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Dept = new GL_Dept();
            objGL_Dept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Dept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objGL_Dept.DeptName = sqlDataReader["DeptName"].ToString();
            return objGL_Dept;
        }
    }
}
