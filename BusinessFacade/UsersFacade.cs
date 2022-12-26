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
    public class UsersFacade : AbstractFacade
    {
        private Users objUsers = new Users();
        private ArrayList arrUsers;        
        private List<SqlParameter> sqlListParam;
        

        public UsersFacade() : base()
        {
         
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUsers = (Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUsers.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsers.UserName));
                sqlListParam.Add(new SqlParameter("@Password", objUsers.Password));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objUsers.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objUsers.UnitKerjaID));   
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUsers.CreatedBy));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertUsers");
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
                objUsers = (Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUsers.ID));
                sqlListParam.Add(new SqlParameter("@UserID", objUsers.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsers.UserName));
                sqlListParam.Add(new SqlParameter("@Password", objUsers.Password));
                sqlListParam.Add(new SqlParameter("@usrmail", objUsers.UsrMail ));
                sqlListParam.Add(new SqlParameter("@pssmail", objUsers.PssMail ));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objUsers.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objUsers.UnitKerjaID));   
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUsers.CreatedBy));
                sqlListParam.Add(new SqlParameter("@TmpPeriode", objUsers.TmpPeriode));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateUsers");

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
                objUsers = (Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUsers.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUsers.LastModifiedBy));
                               
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteUsers");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from Users where RowStatus >-1");
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
                arrUsers.Add(new Users());
            
            return arrUsers;
        }

        public Users RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Users where RowStatus >-1 and Id = " + Id);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
           return new Users();     
        }

        public Users RetrieveByUserID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Users where RowStatus >-1 and UserId = '" + userId + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Users();
        }

        public string GetDeptOto(int userid)
        {
            string dept = string.Empty;
            string strSQL = "Select distinct DeptID from ISO_Dept where RowStatus >-1 and UserID=" + userid;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dept += sdr["DeptID"].ToString() + ",";
                }
            }
            return (dept.Length > 1) ? dept.Substring(0, dept.Length - 1) : dept;
        }
        public Users RetrieveByUserName(string userName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Users where RowStatus >-1 and UserName = '" + userName + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Users();
        }

        public Users RetrieveByUserNameAndPassword(string userName,string password)
        {
            try
            {
                string strSQL = "Select * from Users where RowStatus >-1 and UserName = '" + userName.Replace("'", string.Empty) + "' and Password = '" + password.Replace("'", string.Empty) + "'";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }
                return new Users();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return new Users();
            }
               
        }
        public Users RetrieveForMgrTask(string DeptName,int usergroupid)
        {
            string strSQL = string.Empty;
            if (usergroupid==200)
                strSQL = "select * from Users where ID in(select UserID from ISO_Users where RowStatus>-1 and UserID  " +
               "in (select  top 1 userID from ISO_Dept where rowstatus>-1 and  DeptName='" + DeptName + "' and UserGroupID=100 order by UserGroupID))";
            else
                strSQL = "select * from Users where ID in(select UserID from ISO_Users where RowStatus>-1 and UserID  " +
               "in (select  top 1 userID from ISO_Dept where rowstatus>-1 and  DeptName='" + DeptName + "' and UserGroupID=50 order by UserGroupID))";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            using (SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL))
            {
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }
            }
            return new Users();
        }

        public Users RetrieveTppNoteApproved(int Id)
        {
            string strSQL = "select * from Users where ID in(select UserID from ISO_Users where RowStatus>-1 and UserID  " +
                "in (select user_ID from TPPEmail where Dept_ID " + "in (select Dept_ID from tpp where id='" + Id + "'))) order by id desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            using (SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL))
            {
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }
            }
            return new Users();
        }

        public Users RetrieveTppApproved(int Id)
        {
            string strSQL = "select * from Users where ID in(select UserID from ISO_Users where RowStatus>-1 and UserID  " +
                "in (select userID from ISO_Dept where UserGroupID=100 and TppDept " + "in (select Dept_ID from tpp where id='" + Id + "'))) order by id desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            using (SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL))
            {
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }
            }
            return new Users();
        }

        public Users GenerateObject(SqlDataReader sqlDataReader)
        {
            objUsers = new Users();
            objUsers.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUsers.UserID = sqlDataReader["UserID"].ToString();
            objUsers.UserName = sqlDataReader["UserName"].ToString();
            objUsers.Password = sqlDataReader["Password"].ToString();
            objUsers.TypeUnitKerja = Convert.ToInt32(sqlDataReader["TypeUnitKerja"]);
            objUsers.UnitKerjaID = Convert.ToInt32(sqlDataReader["UnitKerjaID"]);
            objUsers.ViewPrice = Convert.ToInt32(sqlDataReader["ViewPrice"]);
            objUsers.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objUsers.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUsers.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objUsers.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objUsers.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objUsers.UsrMail = sqlDataReader["usrmail"].ToString();
            objUsers.PssMail = (string.IsNullOrEmpty(sqlDataReader["pssmail"].ToString())) ? "" : sqlDataReader["pssmail"].ToString();
            objUsers.TmpPeriode = sqlDataReader["TmpPeriode"].ToString();
            objUsers.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUsers.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objUsers.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objUsers.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
            objUsers.UserAlias = sqlDataReader["UserAlias"].ToString();
            //objUsers.HeadID = (string.IsNullOrEmpty(sqlDataReader["HeadID"].ToString())) ? 0 : Convert.ToInt32(sqlDataReader["HeadID"]);

            if (string.IsNullOrEmpty(sqlDataReader["UserLevel"].ToString()))
                objUsers.UserLevel = 0;
            else
                objUsers.UserLevel = Convert.ToInt32(sqlDataReader["UserLevel"].ToString());
            objUsers.KodeLokasi = PlantCode(sqlDataReader["UnitKerjaID"].ToString());

            //objUsers.UserAlias = sqlDataReader["UserAlias"].ToString();
            return objUsers;

        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Users where RowStatus >-1 and " + strField + " like '%" + strValue + "%'");
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
                arrUsers.Add(new Users());

            return arrUsers;
        }
        /** Lock System **/
        public ArrayList RetrieveAllUser()
        {
            string strSQL = "Select U.* from Users as U where RowStatus >-1 Order by username";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(new Users
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        UserID = sqlDataReader["UserID"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                        DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString())
                    });
                }
            }
            else
                arrUsers.Add(new Users());
            return arrUsers;
        }
        public string PlantCode(string DepoID)
        {
            string result = string.Empty;
            string strSQL = "Select KodeLokasi from Company where DepoID=" + DepoID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["KodeLokasi"].ToString().ToUpper();
                }
            }
            return result;
        }
        public string Criteria { get; set; }
        public ArrayList RetriveUserAccount()
        {
            arrUsers = new ArrayList();
            string strSQL = "Select * from UserAccount where RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrUsers.Add(objectUser(sdr));
                }
            }
            return arrUsers;
        }
        private Users objectUser(SqlDataReader sdr)
        {
            objUsers = new Users();
            objUsers.ID=Convert.ToInt32(sdr["UserID"].ToString());
            objUsers.UserName=sdr["UserName"].ToString();
            objUsers.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
            objUsers.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
            objUsers.NIK = sdr["NIK"].ToString();
            return objUsers;
        }
    }
}
