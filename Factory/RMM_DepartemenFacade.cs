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
    public class RMM_DepartemenFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM_Departemen objDepartemen = new RMM_Departemen();
        private ArrayList arrDepartemen;
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
            string strSQL = "select * from RMM_Departemen as A where A.RowStatus >-1 order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDepartemen = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepartemen.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepartemen.Add(new RMM_Departemen());

            return arrDepartemen;

        }

        public ArrayList RetrieveByUserID(int userID)
        {
            string strSQL = "select A.*  from RMM_Departemen  A inner join RMM_Users B on A.DeptID=B.Dept_ID  where B.User_ID=" + userID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDepartemen = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepartemen.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepartemen.Add(new RMM_Departemen());
            return arrDepartemen;
        }

        public RMM_Departemen GetUserDepartemen(int userid)
        {
            string strSQL = "select A.*  from RMM_Departemen  A inner join RMM_Users B on A.DeptID=B.Dept_ID  where B.User_ID=" + userid;
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
            return new RMM_Departemen();
        }

        private RMM_Departemen GenerateObject(SqlDataReader sqlDataReader)
        {
            objDepartemen = new RMM_Departemen();
            objDepartemen.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDepartemen.SarMutDepartment = sqlDataReader["SarMutDepartment"].ToString();
            objDepartemen.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objDepartemen.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objDepartemen.Urutan = Convert.ToInt32(sqlDataReader["Urutan"]);
            return objDepartemen;
        }
    }
}
