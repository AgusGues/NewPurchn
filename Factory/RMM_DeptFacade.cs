using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
    public class RMM_DeptFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM_Dept objDept = new RMM_Dept();
        private ArrayList arrDept;
        private List<SqlParameter> sqlListParam;
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            string strSQL = "select * from RMM_Dept as A where A.RowStatus >-1 order By A.Departemen";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }

        public RMM_Dept GetUserDept(int userid)
        {
            string strSQL = "select A.*  from RMM_Dept  A inner join RMM_Users B on A.ID=B.Dept_ID  where B.User_ID=" + userid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new RMM_Dept();
        }

        private RMM_Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objDept = new RMM_Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.Kode = sqlDataReader["Kode"].ToString();
            objDept.Departemen = sqlDataReader["Departemen"].ToString();
            return objDept;
        }
    }
}
