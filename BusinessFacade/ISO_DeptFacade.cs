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
    public class ISO_DeptFacade : AbstractFacade
    {
        private ISO_Dept objDept = new ISO_Dept();
        private ArrayList arrUsers;
        private List<SqlParameter> sqlListParam;
        

        public ISO_DeptFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDept = (ISO_Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@DeptID", objDept.DeptID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISODeptM");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int Insert2(object objDomain)
        {
            try
            {
                objDept = (ISO_Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@UserID", objDept.UserID));
                sqlListParam.Add(new SqlParameter("@DeptID", objDept.DeptID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objDept.UserGroupID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISODept");
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
                objDept = (ISO_Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDept.ID));
                sqlListParam.Add(new SqlParameter("@DeptID", objDept.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDept.RowStatus));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISODeptM");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int Update2(object objDomain)
        {
            try
            {
                objDept = (ISO_Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDept.ID));
                sqlListParam.Add(new SqlParameter("@DeptID", objDept.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@UserID", objDept.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objDept.UserGroupID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDept.RowStatus));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISODept");

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
                objDept = (ISO_Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDept.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDept.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteISODeptM");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ISO_Dept where RowStatus = 0");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Dept());

            return arrUsers;
        }

        public ISO_Dept RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Dept where RowStatus = 0 and Id = " + Id);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Dept();
        }

        public ISO_Dept RetrieveByUserID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Dept where RowStatus = 0 and UserId = '" + userId + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Dept();
        }

        public ISO_Dept RetrieveByUserName(string userName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Dept where RowStatus = 0 and UserName = '" + userName + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Dept();
        }

        public ISO_Dept RetrieveByDept(int DeptIDE)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL="Select * from ISO_Dept_m as m where m.RowStatus = 0 and DeptID= '" + DeptIDE + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject2(sqlDataReader);
                    }
                }
            
            return new ISO_Dept();
        }

        public ISO_Dept RetriveByISODept(int ISODept)
        {
            string strSQL = "Select * from ISO_Dept_m as m where m.RowStatus = 0 and ID= '" + ISODept + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new ISO_Dept();
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Dept where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Dept());

            return arrUsers;
        }
        public ArrayList RetieveByDeptID(int DeptIDE,string Field)
        {
            string fld=(Field==string.Empty)?"m.DeptID":Field;
            string strSQL = (DeptIDE != 0) ? "Select * from ISO_Dept_m as m where m.RowStatus = 0 and "+fld+" = '" + DeptIDE + "'" :
                           "Select * from ISO_Dept_m as m where m.RowStatus = 0" ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Dept());

            return arrUsers;
        }

        public ISO_Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objDept = new ISO_Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.UserGroupID = Convert.ToInt32(sqlDataReader["UserGroupID"]);
            objDept.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);

            return objDept;
        }
        public ISO_Dept GenerateObject2(SqlDataReader sqlDataReader)
        {

            objDept = new ISO_Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objDept;
        }
    }

}
