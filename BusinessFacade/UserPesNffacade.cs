using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class UserPesNffacade : AbstractTransactionFacade
    {
        private UserPesNf.ParamHead obj = new UserPesNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public UserPesNffacade(object objDomain)
            : base(objDomain)
        {
            obj = (UserPesNf.ParamHead)objDomain;
        }

        public override int Delete(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", obj.LastModifiedBy));
                result = transManager.DoTransaction(sqlListParam, "spDeleteISOUsers");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override int Insert(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", obj.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", obj.UserName));
                sqlListParam.Add(new SqlParameter("@Password", obj.Password));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", obj.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", obj.UnitKerja));
                sqlListParam.Add(new SqlParameter("@CreatedBy",obj.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyID", obj.Company));
                sqlListParam.Add(new SqlParameter("@PlantID", obj.Company));
                sqlListParam.Add(new SqlParameter("@BagianID", obj.Jabatan));
                sqlListParam.Add(new SqlParameter("@DeptID", obj.Department));
                sqlListParam.Add(new SqlParameter("@NIK", obj.Nik));
                result = transManager.DoTransaction(sqlListParam, "spInsertISOUsers1");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override int Update(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", obj.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", obj.UserName));
                sqlListParam.Add(new SqlParameter("@Password", obj.Password));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", obj.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", obj.UnitKerja));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", obj.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@CompanyID", obj.Company));
                sqlListParam.Add(new SqlParameter("@PlantID", obj.Company));
                sqlListParam.Add(new SqlParameter("@BagianID", obj.Jabatan));
                sqlListParam.Add(new SqlParameter("@DeptID", obj.Department));
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                result = transManager.DoTransaction(sqlListParam, "spUpdateISOUsers");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static int MaxUserId()
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 1 id FROM Users order by id desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int GetUserPes(int user)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT Count(id) countId FROM iso_users where userid="+user;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int CekUser(string user)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT Count(id) countId FROM users where username='" + user +"'";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int UserGroupID(int Id)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 1 UserGroupID FROM ISO_Bagian WHERE RowStatus>-1 and ID="+ Id+" order by id desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int AddUserGroup(string DepartmentName, int UserID, int Department, int UserGroupID)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "INSERT INTO ISO_Dept values('"+ DepartmentName + "',"+ UserID + ","+ UserGroupID + ",0)";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int EditUserGroup(string DepartmentName, int UserID, int Department, int UserGroupID)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Update ISO_Dept set DeptName='"+ DepartmentName + "',DeptID="+ Department + ",UserGroupID="+ UserGroupID + "  where UserID=" + UserID;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<UserPesNf.ParamData> ListData()
        {
            List<UserPesNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query =
"SELECT top 5 "+
"u.Id, u.UserID, u.UserName, a.Nik, " +
"c.ID CompanyId, c.Nama CompanyName, " +
"m.DeptID, m.DeptName,  " +
"b.ID BagianId, b.BagianName, " +
"u.TypeUnitKerja TypeUnitKerjaId, " +
"CASE u.TypeUnitKerja WHEN 1 THEN 'Distibutor' ELSE 'Depo' END TypeUnitKerjaName, " +
"u.UnitKerjaID, " +
"CASE u.TypeUnitKerja " +
"WHEN 1 THEN(SELECT d.DistributorName FROM Distributor d WHERE u.UnitKerjaID = d.ID) " +
"ELSE(SELECT d.DepoName FROM Depo d WHERE u.UnitKerjaID = d.ID) " +
"END UnitKerjaName " +
"FROM ISO_Users u, UserAccount a, Company c, ISO_Dept_m m, ISO_Bagian b " +
"WHERE u.Id=a.UserId and u.CompanyID = c.ID AND u.DeptID = m.DeptID AND u.DeptJabatanID = b.ID " +
"AND u.RowStatus > -1 AND u.UserName != '' ORDER BY u.id desc /*u.UserName*/";
                    AllData = connection.Query<UserPesNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<UserPesNf.ParamUserName> ListUserName(string UserName)
        {
            List<UserPesNf.ParamUserName> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT top 10 Id, UserName from Users where RowStatus >-1 AND UserName!='' and UserName like '%"+ UserName + "%' order by id desc";
                    AllData = connection.Query<UserPesNf.ParamUserName>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<UserPesNf.ParamUnitKerja> ListUnitKerja(int type)
        {
            List<UserPesNf.ParamUnitKerja> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string q = "SELECT Id, DistributorName Name FROM Distributor WHERE RowStatus>-1";
                if (type == 2) { q = "SELECT Id, DepoName Name FROM Depo WHERE RowStatus>-1"; }
                try
                {
                    string query = q;
                    AllData = connection.Query<UserPesNf.ParamUnitKerja>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<UserPesNf.ParamCompany> ListCompany()
        {
            List<UserPesNf.ParamCompany> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id,Nama from Company WHERE RowStatus>-1";
                    AllData = connection.Query<UserPesNf.ParamCompany>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<UserPesNf.ParamDepartment> ListDepartment()
        {
            List<UserPesNf.ParamDepartment> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select DeptId Id,DeptName from ISO_Dept_m WHERE RowStatus>-1";
                    AllData = connection.Query<UserPesNf.ParamDepartment>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<UserPesNf.ParamJabatan> ListJabatan(int dept)
        {
            List<UserPesNf.ParamJabatan> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, BagianName FROM ISO_Bagian WHERE RowStatus>-1 and DeptID=" + dept;
                    AllData = connection.Query<UserPesNf.ParamJabatan>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

    }
}
