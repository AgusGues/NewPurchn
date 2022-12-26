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
    public class RoleRulesFacade : AbstractFacade
    {
        private RoleRules objRoleRules = new RoleRules();
        private ArrayList arrRoleRules;
        private List<SqlParameter> sqlListParam;

        public RoleRulesFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objRoleRules = (RoleRules)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RoleID", objRoleRules.RoleID));
                sqlListParam.Add(new SqlParameter("@RuleID", objRoleRules.RuleID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertRoleRules");

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
                objRoleRules = (RoleRules)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RoleID", objRoleRules.RoleID));


                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRoleRules");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select A.ID,A.RoleID,B.RolesName,A.RuleID,C.RuleName from RoleRules as A,Roles as B,Rules as C where A.RoleID = B.ID and A.RuleID = C.ID");
            strError = dataAccess.Error;
            arrRoleRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRoleRules.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRoleRules.Add(new RoleRules());

            return arrRoleRules;
        }

        public ArrayList RetrieveByRoleId(int roleID)
        {
            string strSQL = "Select A.ID,A.RoleID,B.RolesName,A.RuleID,C.RuleName from RoleRules as A,Roles as B,Rules as C where A.RoleID = B.ID and A.RuleID = C.ID and A.RoleID = " + roleID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRoleRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRoleRules.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRoleRules.Add(new RoleRules());

            return arrRoleRules;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select A.ID,A.RoleID,B.RolesName,A.RuleID,C.RuleName from RoleRules as A,Roles as B,Rules as C where A.RoleID = B.ID and A.RuleID = C.ID and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrRoleRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRoleRules.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRoleRules.Add(new RoleRules());

            return arrRoleRules;
        }

        public RoleRules GenerateObject(SqlDataReader sqlDataReader)
        {
            objRoleRules = new RoleRules();
            objRoleRules.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRoleRules.RoleID = Convert.ToInt32(sqlDataReader["RoleID"]);
            objRoleRules.RoleName = sqlDataReader["RolesName"].ToString();            
            objRoleRules.RuleID = Convert.ToInt32(sqlDataReader["RuleID"]);
            objRoleRules.RuleName = sqlDataReader["RuleName"].ToString();
            return objRoleRules;

        }
    }
}
