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
    public class RekapKpiNfFacade : AbstractTransactionFacade
    {
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<RekapKpiNf.ParamData> ListData(int dept, int user)
        {
            List<RekapKpiNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = "";
                if (user != 0) { where = " and UserID = "+user+" "; }
                string query;
                try
                {
                    query = "select "+
"Distinct UserName, isnull(UserID,0) UserID,isnull(BagianID, 0)BagianID,  " +
"(select BagianName from ISO_Bagian where ID = BagianID)BagianName " +
"from UserAccount where RowStatus > -1  and DeptID ="+ dept + where;
                    AllData = connection.Query<RekapKpiNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapKpiNf.ParamDataDtl> ListDataDtl(int dept,int user, int bulan, int tahun)
        {
            List<RekapKpiNf.ParamDataDtl> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query =
"with q as ( "+
    "select Distinct UserName, isnull(UserID, 0) UserID,isnull(BagianID, 0)BagianID,  " +
    "(select BagianName from ISO_Bagian where ID = BagianID)BagianName " +
    "from UserAccount where RowStatus > -1 and DeptID = "+dept+" and UserID = "+ user + " " +
"), " +
"w as ( " +
    "select q.UserID PicUser, q.UserName + ' - ' + q.BagianName UserName,  KPIName,NilaiBobot, " +
    "Keterangan,ic.Target trg, sod.PointNilai,iuc.UserID,sod.Approval " +
    "FROM ISO_KPI as sop " +
    "LEFT JOIN ISO_KPIDetail as sod ON sod.KPIID = sop.ID " +
    "LEFT JOIN ISO_UserCategory as iuc ON iuc.ID = sop.CategoryID " +
    "LEFT JOIN ISO_Category as ic on ic.ID = iuc.CategoryID " +
    "inner join q on sop.ISO_UserID = q.UserID and sop.BagianID = q.BagianID " +
    "where sod.RowStatus > -1 and sop.RowStatus > -1 " +
    "and MONTH(TglMulai) = "+bulan+" AND YEAR(TglMulai) = "+tahun+" " +
"), " +
"r as ( " +
    "select cast(PicUser as char(10)) PicUser, UserName, KPIName Description, cast(NilaiBobot * 100 as decimal(11, 0)) Bobot, " +
    "trg Target, Keterangan Pencapaian, cast(PointNilai as char(11)) Score, " +
    "Case When Approval = 2 then cast((PointNilai * NilaiBobot) as decimal(11, 2)) else 0 end Point " +
    "from w " +
") " +
"select* from r " +
"union all " +
"select '' PicUser, '' UserName, 'Total'Description,sum(Bobot) Bobot,  " +
"'' Target, '' Pencapaian, '' Score , sum(Point) Point from r";
                    AllData = connection.Query<RekapKpiNf.ParamDataDtl>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapKpiNf.ParamDept> ListDept(string dept)
        {
            List<RekapKpiNf.ParamDept> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "SELECT ID, Alias FROM Dept WHERE RowStatus>-1 and ID IN (" + dept + ")";
                    AllData = connection.Query<RekapKpiNf.ParamDept>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string GetDept(int user)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select top 1 DeptID from UserDeptAuth where RowStatus > -1  and UserID = "+ user + " and ModulName = 'Rekap KPI' ";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<RekapKpiNf.ParamTahun> ListTahun()
        {
            List<RekapKpiNf.ParamTahun> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "select distinct YEAR(tglMulai) Tahun from ISO_Task order by year(tglMulai) desc";
                    AllData = connection.Query<RekapKpiNf.ParamTahun>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapKpiNf.ParamPic> ListPic(int dept)
        {
            List<RekapKpiNf.ParamPic> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "Select UserID,UserName from UserAccount where RowStatus>-1  and DeptID=" + dept + " order by UserName";
                    AllData = connection.Query<RekapKpiNf.ParamPic>(query).ToList();
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
