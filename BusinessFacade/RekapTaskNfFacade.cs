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
    public class RekapTaskNfFacade : AbstractTransactionFacade
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

        public static List<RekapTaskNf.ParamData> ListData(int dept, int user)
        {
            List<RekapTaskNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = "";
                if (user != 0) { where = " and UserID = " + user + " "; }
                string query;
                try
                {
                    query = "select " +
"Distinct UserName, isnull(UserID,0) UserID,isnull(BagianID, 0)BagianID,  " +
"(select BagianName from ISO_Bagian where ID = BagianID)BagianName " +
"from UserAccount where RowStatus > -1  and DeptID =" + dept + where;
                    AllData = connection.Query<RekapTaskNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapTaskNf.ParamDataDtl> ListDataDtl(int user, string DariTanggal, string SampaiTanggal, int Status)
        {
            List<RekapTaskNf.ParamDataDtl> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                //all
                string where = " and (Convert(Char,TglSelesai,112) between '"+ DariTanggal + "' and '"+ SampaiTanggal + "' OR (Convert(Char,TglMulai,112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' and (RowStatus>-1 AND RowStatus!=9) and ISNULL(TglSelesai ,'1/1/1900')='1/1/1900')) ";

                if (Status == 1)//solve
                {
                    where = " and Convert(Char,TglSelesai,112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' ";
                }
                if (Status == 2)//unsolve
                {
                    where = " and ISNULL(TglSelesai ,'1/1/1900')='1/1/1900' and (RowStatus>-1 AND RowStatus!=9) ";
                }
                if (Status == 3)//cancel
                {
                    where = " and Convert(Char,TglMulai,112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' and Status=9 and RowStatus=9 ";
                }
                string query;
                try
                {
                    query =
"with w as ( "+
    "select " +
    "( " +
        "select COUNT(ID) from ISO_Task where (RowStatus > -1 AND RowStatus != 9) and Status> -1 " +
        "and PIC = isot.PIC  and ISO_UserID = "+ user + " " + where +
        /*"and ( " +
            "Convert(Char, TglSelesai, 112) between '" + DariTanggal + "' and '"+ SampaiTanggal + "' " +
            "OR (Convert(Char, TglMulai, 112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' " +
            "and (RowStatus > -1 AND RowStatus != 9) and ISNULL(TglSelesai, '1/1/1900') = '1/1/1900') " +
        ") " +*/
    ") Num, *, " +
    "( " +
        "SELECT SUM(NilaiBobot) FROM ISO_Task where(RowStatus > -1 AND RowStatus != 9) and Status = 2 " +
        "and PIC = isot.PIC and ISO_UserID = " + user + " " + where +
        /*"and ( " +
            "Convert(Char, TglSelesai, 112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' " +
            "OR (Convert(Char, TglMulai, 112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' " +
            "and (RowStatus > -1 AND RowStatus != 9) and ISNULL(TglSelesai, '1/1/1900') = '1/1/1900') " +
        ") " +*/
    ") TotalBobot , " +
    "(select dbo.GetPointNilai(isot.ID))Point, " +
    "(select top 1 isd.TglTargetSelesai from ISO_TaskDetail isd where TaskID = isot.ID and RowStatus> -1) Targete " +
    "from ISO_Task isot where isot.Status > -1 and ISO_UserID = " + user + " " + where +
    /*"and ( " +
        "Convert(Char, TglSelesai, 112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' " +
        "OR (Convert(Char, TglMulai, 112) between '" + DariTanggal + "' and '" + SampaiTanggal + "' " +
        "and (RowStatus > -1 AND RowStatus != 9) and ISNULL(TglSelesai, '1/1/1900') = '1/1/1900') " +
    ") " +*/
"), " +
"r as ( " +
    "select " +
    "pic,(select BagianName from iso_bagian b where b.ID = w.BagianID) BagianName, " +
    "( " +
        "Select top 1 cast((Bobot * 100) as decimal(11, 0)) bobot " +
        "from ISO_BobotPES where CAST( " +
        "RTRIM(LTRIM(cast(activetahun as char(4)))) + " +
        "'' + (Right('0' + RTRIM(LTRIM(CAST(activebulan as CHAR))), 2)) as int " +
        ") <= 202001 and BagianID = w.BagianID and PesType = 2 order by ID desc " +
    ") BobotMax,  " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 1) target1, " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 2) target2, " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 3) target3, " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 4) target4, " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 5) target5, " +
    "(Select TglTargetSelesai from ISO_TaskDetail d where d.RowStatus > -1 and d.TaskID = w.id and d.TargetKe = 6) target6, " +
    "TaskNo,TaskName,TglMulai,TglSelesai, Status, " +
    "cast(NilaiBobot as decimal(11, 2)) NilaiBobot, " +
    "cast(TotalBobot as decimal(11, 2)) TotalBobot, " +
    "cast(( " +
        "Select top 1 isnull(PointNilai, 0)PointNilai " +
        "from ISO_TaskDetail where RowStatus > -1 and TaskID = w.Id Order by ID Desc " +
    ") as decimal(11,2)) Point " +
    "from w " +
"), " +
"t as ( " +
    "select " +
    "pic + ' - ' + BagianName UserName, cast(BobotMax as char(11)) BobotMax, " +
    "cast( " +
        "case BobotMax " +
        "when 10 then 6 " +
        "when 15 then 8 " +
        "else BobotMax - 10 end as char(11) " +
    ") BobotMin, " +
    "TaskNo,TaskName, " +
    "cast(CONVERT(DATE, TglMulai) as char(10)) TglMulai, " +
    "isnull(cast(CONVERT(DATE, target1) as char(10)), '') target1, " +
    "isnull(cast(CONVERT(DATE, target2) as char(10)), '') target2, " +
    "isnull(cast(CONVERT(DATE, target3) as char(10)), '') target3, " +
    "isnull(cast(CONVERT(DATE, target4) as char(10)), '') target4, " +
    "isnull(cast(CONVERT(DATE, target5) as char(10)), '') target5, " +
    "isnull(cast(CONVERT(DATE, target6) as char(10)), '') target6, " +
    "case  " +
    "when TglSelesai is not null and Status!= 0 then cast(CONVERT(DATE, TglSelesai) as char(10)) " +
    "when target2 is null and GETDATE() > target1 then 'Naik Target' " +
    "when target2 is not null and target3 is null and GETDATE() > target2 and target2 > target1 then 'Naik Target' " +
    "when target3 is not null and target4 is null and GETDATE() > target3 and target3 > target2 then 'Naik Target' " +
    "when target4 is not null and target5 is null and GETDATE() > target4 and target4 > target3 then 'Naik Target' " +
    "when target5 is not null and target6 is null and GETDATE() > target5 and target5 > target4 then 'Naik Target' " +
    "when target6 is not null and GETDATE() > target6 and target6 > target5 then 'Naik Target'  " +
    "else 'Progress' " +
    "end TglSelesai, " +
    "NilaiBobot,Point,TotalBobot, " +
    "cast((NilaiBobot / TotalBobot) * Point as decimal(11, 2)) Score " +
    "from r " +
") " +
"select " +
"UserName + ' / BobotMax:' + BobotMax + ' / BobotMin:' + BobotMin UserName,TaskNo, TaskName,TglMulai, " +
"Target1,Target2,Target3,Target4,Target5,Target6,TglSelesai, " +
"NilaiBobot,Point,Score " +
"from t " +
"union all " +
"select ''UserName,''TaskNo,''TaskName, " +
"'TotalNilai'TglMulai,''Target1,''Target2,''Target3,''Target4,''Target5,''Target6,''TglSelesai,  " +
"sum(NilaiBobot) NilaiBobot,  " +
"cast(avg(Point) as decimal(11, 2)) Point, " +
"sum(cast((NilaiBobot / TotalBobot) * Point as decimal(11, 2))) Score " +
"from t";
                    AllData = connection.Query<RekapTaskNf.ParamDataDtl>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapTaskNf.ParamDept> ListDept(string dept)
        {
            List<RekapTaskNf.ParamDept> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "SELECT ID, Alias FROM Dept WHERE RowStatus>-1 and ID IN (" + dept + ")";
                    AllData = connection.Query<RekapTaskNf.ParamDept>(query).ToList();
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
                    string query = "Select top 1 DeptID from UserDeptAuth where RowStatus > -1  and UserID = " + user + " and ModulName = 'Rekap Task' ";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<RekapTaskNf.ParamPic> ListPic(int dept)
        {
            List<RekapTaskNf.ParamPic> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "Select UserID,UserName from UserAccount where RowStatus>-1  and DeptID=" + dept + " order by UserName";
                    AllData = connection.Query<RekapTaskNf.ParamPic>(query).ToList();
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
