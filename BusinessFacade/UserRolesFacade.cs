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
    public class UserRolesFacade : AbstractFacade
    {
        private UserRoles objUserRoles = new UserRoles();
        private ArrayList arrUserRoles;
        private List<SqlParameter> sqlListParam;

        public UserRolesFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUserRoles = (UserRoles)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUserRoles.UserID));
                sqlListParam.Add(new SqlParameter("@RoleID", objUserRoles.RoleID));                

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertUserRoles");

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
            return 0;
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objUserRoles = (UserRoles)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUserRoles.UserID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteUserRoles");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UserRoles where RowStatus = 0");
            strError = dataAccess.Error;
            arrUserRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUserRoles.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUserRoles.Add(new UserRoles());

            return arrUserRoles;
        }

        public ArrayList RetrieveByUserId(int userID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UserRoles where UserID = " + userID);
            strError = dataAccess.Error;
            arrUserRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUserRoles.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUserRoles.Add(new UserRoles());

            return arrUserRoles;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UserRoles where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrUserRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUserRoles.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUserRoles.Add(new UserRoles());

            return arrUserRoles;
        }

        public UserRoles GenerateObject(SqlDataReader sqlDataReader)
        {
            objUserRoles = new UserRoles();
            objUserRoles.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUserRoles.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objUserRoles.RoleID = Convert.ToInt32(sqlDataReader["RoleID"]);            
            return objUserRoles;

        }
    }
}
