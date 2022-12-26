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
    public class RolesFacade : AbstractFacade
    {
        private Roles objRoles = new Roles();
        private ArrayList arrRoles;
        private List<SqlParameter> sqlListParam;


        public RolesFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objRoles = (Roles)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RolesName", objRoles.RolesName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRoles.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertRoles");

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
                objRoles = (Roles)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRoles.ID));
                sqlListParam.Add(new SqlParameter("@RolesName", objRoles.RolesName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRoles.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateRoles");

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
                objRoles = (Roles)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRoles.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRoles.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRoles");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select  * from Roles where RowStatus = 0 order by rolesname");
            strError = dataAccess.Error;
            arrRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRoles.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRoles.Add(new Roles());

            return arrRoles;
        }

        public Roles RetrieveByCode(string roleName)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Roles where RowStatus = 0 and RolesName = '" + roleName + "'");
            strError = dataAccess.Error;
            arrRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Roles();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Roles where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrRoles = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRoles.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRoles.Add(new Roles());

            return arrRoles;
        }

        public Roles GenerateObject(SqlDataReader sqlDatRolesder)
        {
            objRoles = new Roles();
            objRoles.ID = Convert.ToInt32(sqlDatRolesder["ID"]);
            objRoles.RolesName = sqlDatRolesder["RolesName"].ToString();
            objRoles.RowStatus = Convert.ToInt32(sqlDatRolesder["RowStatus"]);
            objRoles.CreatedBy = sqlDatRolesder["CreatedBy"].ToString();
            objRoles.CreatedTime = Convert.ToDateTime(sqlDatRolesder["CreatedTime"]);
            objRoles.LastModifiedBy = sqlDatRolesder["LastModifiedBy"].ToString();
            objRoles.LastModifiedTime = Convert.ToDateTime(sqlDatRolesder["LastModifiedTime"]);
            return objRoles;

        }
    }
}
