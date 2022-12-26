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
    public class UsersHeadFacade : AbstractFacade
    {
        private UsersHead objUsersHead = new UsersHead();
        private ArrayList arrUsersHead;
        private List<SqlParameter> sqlListParam;

        public UsersHeadFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUsersHead = (UsersHead)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUsersHead.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsersHead.UserName));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertUsersHead");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            try
            {
                objUsersHead = (UsersHead)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUsersHead.ID));
                sqlListParam.Add(new SqlParameter("@UserID", objUsersHead.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsersHead.UserName));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateUsersHead");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objUsersHead = (UsersHead)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUsersHead.ID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteUsersHead");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ListUserHead where RowStatus = 0");
            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsersHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUsersHead.Add(new UsersHead());

            return arrUsersHead;
        }

        public UsersHead RetrieveByUserID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from ListUserHead where RowStatus = 0 and UserId = '" + userId + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new UsersHead();
        }
        public ArrayList RetrieveByHeadID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from ListUserHead where RowStatus = 0 and HeadID = '" + userId + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsersHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUsersHead.Add(new UsersHead());
            return arrUsersHead;
        }
        public UsersHead RetrieveByUserIDimprov(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from ListUserHead where RowStatus = 0 and UserId = '" + userId + "' and keterangan='improvement'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new UsersHead();
        }
        public UsersHead RetrieveByImprovementHead(string HeadID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(managerID) as HeadID  from ListUserHead where keterangan='improvement' and RowStatus>-1 and HeadID=" + HeadID);

            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new UsersHead();
        }
        public UsersHead RetrieveByImprovementManager(string HeadID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(managerID) as HeadID  from ListUserHead where keterangan='improvement pusat' and RowStatus>-1 " );

            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new UsersHead();
        }
        public ArrayList RetrieveByManagerID(int managerID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(HeadID) as HeadID from ListUserHead where RowStatus>-1 and ManagerID="+managerID);

            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsersHead.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsersHead.Add(new UsersHead());
            return arrUsersHead;
        }
        public ArrayList RetrieveByManagerID(int managerID,string UserID)
        {
            string strSQL = "select distinct(HeadID) as HeadID from ListUserHead where RowStatus>-1 and ManagerID=" + managerID + " and UserID not in(" + UserID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrUsersHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsersHead.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsersHead.Add(new UsersHead());
            return arrUsersHead;
        }

        public UsersHead GenerateObject(SqlDataReader sqlDataReader)
        {
            objUsersHead = new UsersHead();
            objUsersHead.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUsersHead.UserID = Convert.ToInt32(sqlDataReader["UserID"].ToString());
            objUsersHead.HeadID = Convert.ToInt32(sqlDataReader["HeadID"].ToString());
            objUsersHead.ManagerID = Convert.ToInt32(sqlDataReader["ManagerID"].ToString());
            return objUsersHead;

        }

        public UsersHead GenerateObject2(SqlDataReader sqlDataReader)
        {
            objUsersHead = new UsersHead();
            objUsersHead.HeadID = Convert.ToInt32(sqlDataReader["HeadID"].ToString());
            return objUsersHead;

        }

    }

}
