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
    public class SerahTerimaNfFacade : AbstractTransactionFacade
    {
        private SerahTerimaNf.ParamHead obj = new SerahTerimaNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public SerahTerimaNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (SerahTerimaNf.ParamHead)objDomain;
        }
       
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectID", obj.Id));
                sqlListParam.Add(new SqlParameter("@Level", obj.Approval));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Statuse", obj.Statuse));
                sqlListParam.Add(new SqlParameter("@IPAddress", HttpContext.Current.Request.ServerVariables["remote_addr"].ToString()));
                int intResult = transManager.DoTransaction(sqlListParam, "spMTC_Project_log_Insert_Rev1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                sqlListParam.Add(new SqlParameter("@AktualFinish", obj.FinishDate));
                sqlListParam.Add(new SqlParameter("@Approval", obj.Approval));
                int intResult = transManager.DoTransaction(sqlListParam, "spMTC_Project_serah_Rev1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public static List<SerahTerimaNf.ParamProject> ListProject(string where)
        {
            List<SerahTerimaNf.ParamProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT mp.Id, mp.ProjectName,mp.Nomor ProjectKode FROM MTC_Project mp WHERE mp.RowStatus > -1 " + where;
                    AllData = connection.Query<SerahTerimaNf.ParamProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<SerahTerimaNf.ParamInfoProject> InfoProject(int Id)
        {
            List<SerahTerimaNf.ParamInfoProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = 
"SELECT mp.Id,mp.Nomor ProjectKode, mp.ProjectName,mp.Quantity, "+
"convert(varchar(10), mp.FromDate, 120) FromDate, " +
"convert(varchar(10), mp.ToDate, 120) ToDate, " +
"CASE " +
"WHEN mp.Status = 2 AND mp.RowStatus = 2 AND mp.Approval = 2 THEN 'Project Finished' " +
"WHEN mp.Status = 2 AND mp.RowStatus = 2 AND mp.Approval = 3 THEN 'Project Hand Over' " +
"ELSE '' END StatusProject, " +
"(select isnull(SUM(m.harga), 0) from vw_mtcproject m where m.ProjectID = mp.id and m.RowStatus > -1) BiayaActual, " +
"(SELECT DeptName FROM Dept d WHERE d.ID = mp.DeptID AND d.RowStatus > -1) DeptPemohon " +
"FROM MTC_Project mp WHERE mp.RowStatus > -1 AND mp.id = "+Id;
                    AllData = connection.Query<SerahTerimaNf.ParamInfoProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<SerahTerimaNf.ParamInfoDetail> InfoDetail(int Id)
        {
            List<SerahTerimaNf.ParamInfoDetail> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query =
"with q as ( "+
    "SELECT " +
    "CASE mpm.ItemTypeID WHEN 1 THEN inv.UOMID WHEN 2 THEN A.UOMID ELSE B.UOMID END UOMID, " +
    "CASE mpm.ItemTypeID WHEN 1 THEN inv.ItemCode WHEN 2 THEN A.ItemCode ELSE B.ItemCode END ItemCode, " +
    "CASE mpm.ItemTypeID WHEN 1 THEN Inv.ItemName WHEN 2 THEN A.ItemName ELSE B.ItemName END ItemName, " +
    "mpm.Jumlah, mpm.Schedule " +
    "FROM MTC_ProjectMaterial mpm " +
    "LEFT JOIN Inventory inv on inv.ID = mpm.ItemID " +
    "LEFT JOIN Biaya as B on B.ID = mpm.ItemID " +
    "LEFT JOIN Asset as A on A.ID = mpm.ItemID " +
    "WHERE mpm.RowStatus > -1 and mpm.ProjectID = " + Id +
") " +
"select " +
"(select UomCode from uom where id = q.UOMID) UomCode, " +
"*from q";
                    AllData = connection.Query<SerahTerimaNf.ParamInfoDetail>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int InfoApv(int Id)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = " select Approval from mtc_project where rowstatus > -1 and ID=" + Id + " ";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

    }
}
