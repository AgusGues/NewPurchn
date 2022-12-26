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
    public class InputProjectNfFacade : AbstractTransactionFacade
    {
        private InputProjectNf.ParamHead obj = new InputProjectNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public InputProjectNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (InputProjectNf.ParamHead)objDomain;
        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectName", obj.NamaProject));
                sqlListParam.Add(new SqlParameter("@ProjectDate", obj.ProjectDate));
                sqlListParam.Add(new SqlParameter("@FinishDate", obj.FinishDate));
                sqlListParam.Add(new SqlParameter("@DeptID", obj.DeptID));
                sqlListParam.Add(new SqlParameter("@Sasaran", obj.Sasaran));
                sqlListParam.Add(new SqlParameter("@GroupID", obj.GroupID));
                sqlListParam.Add(new SqlParameter("@ProdLine", obj.ProdLine));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Qty", obj.Quantity));
                sqlListParam.Add(new SqlParameter("@Uom", obj.UOMID));
                sqlListParam.Add(new SqlParameter("@Nomor", obj.Nomor.ToUpper()));
                sqlListParam.Add(new SqlParameter("@Approval", obj.Approval));
                sqlListParam.Add(new SqlParameter("@DetailSasaran", obj.DetailSasaran));
                sqlListParam.Add(new SqlParameter("@Zona", obj.Zona));
                sqlListParam.Add(new SqlParameter("@ToDept", obj.ToDept));
                sqlListParam.Add(new SqlParameter("@NamaHead", obj.NamaHead));
                int intResult = transManager.DoTransaction(sqlListParam, "spMTCProjectInsert_Rev1");
                strError = transManager.Error;
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
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<InputProjectNf.ParamData> ListData(string status, int dept, string nomor)
        {
            List<InputProjectNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = "";
                if (status != "")
                {
                    where = " and mp.Status="+ status + "";
                    if (status == "21") { where = " and mp.Status=2 and mp.RowStatus >=1"; }
                    if (status == "2") { where = "and mp.Status=2 And mp.Approval=2 and mp.RowStatus!=1"; }
                    if (status == "0") { where = " and mp.Approval=0"; }

                }
                if (dept != 0) { where = " and mp.DeptID = " + dept; }
                if (nomor != "") { where = " and mp.Nomor = '" + nomor+"'"; }
                string query;
                try
                {
                    query = "SELECT top 300 " +
    "d.Alias DeptName, mp.ProjectName,mp.FromDate,mp.ToDate, " +
    "mp.Biaya,mp.Sasaran,mp.Nomor, " +
    "mp.Approval,mp.Status,mp.RowStatus, " +
    "(select isnull(cast(SUM(Harga) as decimal(11,2)), 0) from vw_mtcproject where ProjectID = mp.ID and RowStatus > -1 ) BiayaActual " +
    "FROM MTC_Project mp " +
    "LEFT JOIN Dept d on d.ID = mp.DeptID " +
    "where mp.RowStatus > -1 " + where;
                    AllData = connection.Query<InputProjectNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamDeptPemohon> ListDeptPemohon()
        {
            List<InputProjectNf.ParamDeptPemohon> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, DeptName FROM Dept WHERE RowStatus>-1 and Id in (2,3,4,5,9,6,10,7,14,18,19,26)";
                    AllData = connection.Query<InputProjectNf.ParamDeptPemohon>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamGroupProject> ListGroupProject()
        {
            List<InputProjectNf.ParamGroupProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select Id, NamaGroup FROM AM_Group where RowStatus>-1";
                    AllData = connection.Query<InputProjectNf.ParamGroupProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamDeptPemohon> GetDeptPemohon()
        {
            List<InputProjectNf.ParamDeptPemohon> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int DeptID = users.DeptID;
                string query;
                try
                {
                    query = "SELECT Id, DeptName FROM Dept WHERE RowStatus>-1 AND ID="+ DeptID;
                    AllData = connection.Query<InputProjectNf.ParamDeptPemohon>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamAreaProject> ListAreaProject()
        {
            List<InputProjectNf.ParamAreaProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, ZonaName from  MTC_Zona WHERE RowStatus>-1";
                    AllData = connection.Query<InputProjectNf.ParamAreaProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamSatuan> ListSatuan()
        {
            List<InputProjectNf.ParamSatuan> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, UomDesc from UOM where RowStatus >-1";
                    AllData = connection.Query<InputProjectNf.ParamSatuan>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamListHead> ListHeadName()
        {
            List<InputProjectNf.ParamListHead> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int DeptID = users.DeptID;
                string query;
                try
                {
                    query = "SELECT NamaHead FROM mtc_projectsubbagian WHERE RowStatus>-1 and DeptID="+ DeptID;
                    AllData = connection.Query<InputProjectNf.ParamListHead>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<InputProjectNf.ParamSubArea> ListSubArea(string AreaProject)
        {
            List<InputProjectNf.ParamSubArea> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string q = AreaProject.Trim();
                int DeptID = 0;
                if(q == "Zona 4") { DeptID = 3; }
                if(q == "Zona 1" || q == "Zona 2" || q == "Zona 3") { DeptID = 2; }
                string query;
                try
                {
                    query = "SELECT Id, AreaName FROM MTC_AreaImprovment WHERE RowStatus>-1 AND DeptID=" + DeptID;
                    AllData = connection.Query<InputProjectNf.ParamSubArea>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CountDocNo(int Bln, int Thn)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select Count(Id) from mtc_projectNo where Bulan=" + Bln + " and Tahun=" + Thn + "";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static InputProjectNf.ParamDocNo DocNo(int Bln,int Thn)
        {
            InputProjectNf.ParamDocNo AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select Id,Count from mtc_projectNo where Bulan=" + Bln + " and Tahun=" + Thn + "";
                    AllData = connection.QueryFirstOrDefault<InputProjectNf.ParamDocNo>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int UpdateDocNo(int Id, int Count)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "UPDATE mtc_projectNo set Count="+ Count + " where ID="+ Id;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int InsertDocNo(int Bln, int Thn, int Count)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "INSERT INTO mtc_projectNo values ("+ Bln + ","+ Thn + ","+ Count + ") ";
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
