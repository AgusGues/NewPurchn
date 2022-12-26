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
    public class RulesFacade : AbstractFacade
    {
        private Rules objRules = new Rules();
        private ArrayList arrRules;
        private List<SqlParameter> sqlListParam;


        public RulesFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objRules = (Rules)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RuleName", objRules.RuleName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRules.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertRules");

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
                objRules = (Rules)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRules.ID));
                sqlListParam.Add(new SqlParameter("@RuleName", objRules.RuleName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRules.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateRules");

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
                objRules = (Rules)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRules.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRules.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRules");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Rules where RowStatus = 0 order by rulename");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRules.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrRules.Add(new Rules());

            return arrRules;
        }

        public Rules RetrieveByCode(string ruleName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Rules where RowStatus = 0 and RuleName = '" + ruleName + "'");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Rules();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Rules where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRules.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrRules.Add(new Rules());

            return arrRules;
        }
        public ArrayList RetrieveByUserID2(int userID)
        {
            //masih ada Rulemenu yg double
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select a.UserName,c.RolesName,e.ID,e.RuleName,e.sort,e.Level,e.Href from Users as a, UserRoles as b,Roles as c,RoleRules as d, Rules as e where a.RowStatus = 0 and a.ID = "+userID+" and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0 and c.ID = d.RoleID and d.RuleID = e.ID and e.RowStatus = 0 and e.sort is not null and e.Level > 0 order by e.sort, e.Level, e.RuleName");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRules.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRules.Add(new Rules());

            return arrRules;
        }
        public ArrayList RetrieveByUserID(int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQ = "select aa.*,(select top 1 RolesName " +
                "from Users as a, UserRoles as b, Roles as c where a.RowStatus = 0 and a.ID = " + userID + " and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0) as RolesName " +
                "from(select distinct a.ID, a.UserName, d.RuleID, e.RuleName, e.IDname, e.sort, e.Level, e.Href from Users as a, UserRoles as b, Roles as c, RoleRules as d, rules as e " +
                "where a.RowStatus = 0 and a.ID = " + userID + " and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0 and c.ID = d.RoleID and d.RuleID = e.ID) as aa " +
                "where /*sort is not null and*/ level> 0 order by sort,Level";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQ);
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRules.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRules.Add(new Rules());

            return arrRules;
        }
        public ArrayList RetrieveByAllMenuActive()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  ID, e.RuleName, e.IDname, e.sort, e.Level, e.Href from  rules as e where e.rowstatus>-1  order by Level asc");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRules.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrRules.Add(new Rules());

            return arrRules;
        }

        public Rules RetrieveBySortAndLevel(string sort, int level)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from Rules where level="+level+" and Sort like '%"+sort+"%' and RowStatus = 0");
            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Rules();
        }
        public Rules RetrieveByIDname(string idname)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 1 * from Rules where IDname = '" + idname + "' and RowStatus=0";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrRules = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Rules();
        }

        public Rules GenerateObject(SqlDataReader sqlDataReader)
        {
            objRules = new Rules();
            objRules.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRules.RuleName = sqlDataReader["RuleName"].ToString();
            objRules.Level = Convert.ToInt32(sqlDataReader["Level"]);
            objRules.Href = sqlDataReader["Href"].ToString();
            objRules.Sort = sqlDataReader["Sort"].ToString();
            objRules.UserName = sqlDataReader["UserName"].ToString();
            objRules.RolesName = sqlDataReader["RolesName"].ToString();
            objRules.IDname = sqlDataReader["IDname"].ToString();

            return objRules;
        }
        public Rules GenerateObject2(SqlDataReader sqlDataReader)
        {
            objRules = new Rules();
            objRules.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRules.RuleName = sqlDataReader["RuleName"].ToString();
            objRules.Level = Convert.ToInt32(sqlDataReader["Level"]);
            objRules.Href = sqlDataReader["Href"].ToString();
            objRules.Sort = sqlDataReader["Sort"].ToString();
            objRules.IDname = sqlDataReader["IDname"].ToString();

            return objRules;
        }


    }
}
