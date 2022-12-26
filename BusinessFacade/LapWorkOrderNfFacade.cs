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
    public class LapWorkOrderNfFacade : AbstractTransactionFacade
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

        public static List<LapWorkOrderNf.ParamHakAkses> HakAkses()
        {
            List<LapWorkOrderNf.ParamHakAkses> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "select sum(StatusReport)StatusReport,sum(StatusApv)StatusApv " +
"from( " +
    "select StatusReport, StatusApv  " +
    "from WorkOrder_ListApvUpdate  " +
    "where RowStatus > -1 and UserID = "+ users .ID+ "  " +
    "union all  " +
    "select 0, 0 " +
") as xx";
                    AllData = connection.Query<LapWorkOrderNf.ParamHakAkses>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<LapWorkOrderNf.ParamTahun> ListTahun()
        {
            List<LapWorkOrderNf.ParamTahun> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "with "+
"tahun as ( "+
    "select top 1 Tahun + 1 Tahun " +
    "from( " +
        "select DISTINCT(LEFT(convert(char, Createdtime, 112), 4))Tahun  " +
        "from WorkOrder where RowStatus > -1 and(LEFT(convert(char, Createdtime, 112), 4) > 2016) " +
    ") as x  order by tahun desc " +
"), " +
"tahun1 as ( " +
    "select DISTINCT(LEFT(convert(char, Createdtime, 112), 4))Tahun  " +
    "from WorkOrder where RowStatus > -1 and(LEFT(convert(char, Createdtime, 112), 4) > 2016) " +
") " +
"select* from (" +
    "select * from tahun " +
    "union all " +
    "select *from tahun1 " +
") as x order by tahun desc";
                    AllData = connection.Query<LapWorkOrderNf.ParamTahun>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int StatusReport(int user)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select sum(StatusReport)StatusReport " +
"from( " +
    "select StatusReport " +
    "from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID = " + user + " "+
    "union all " +
    "select 0 " +
") as xx";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int StatusApv1(int user)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select SUM(StatusApv)StatusApv " +
"from( " +
    "select '0'StatusApv from WorkOrder_ListApvUpdate union all " +
    "select StatusApv from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID = " + user + " " +
") as Data1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string Corporate()
        {
            string val = "" ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select deptiD Corporate from WorkOrder_DeptStatus where rowstatus>-1";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int StsUser(int user)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select SUM(ID)ID  " +
"from( " +
    "select ID from WorkOrder_SubDept where UserID = 89  and RowStatus > -1 " +
    "union all " +
    "select ID from WorkOrder_SubDept where (HeadID = 89 or UserID = 89) and RowStatus> -1 " +
    "union all " +
    "select '0'ID " +
") as xx";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string NamaSub(int user)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select NamaSubDept from WorkOrder_SubDept where RowStatus>-1 and (UserID=" + user + " or HeadID=" + user + ")";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int DeptIDUser(int user)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select SUM(DeptID)DeptID "+
"from( " +
    "select '0'DeptID from WorkOrder_ListApvUpdate " +
    "union all " +
    "select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID = " + user + " " +
") as Data1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveNamaDept_PemantauanWO(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID)
        {
            string query = "";
            string Query1 = string.Empty; string QueryA = string.Empty; string QueryB = string.Empty;
            string DeptiD2 = string.Empty;
            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25) { DeptiD2 = "19"; }
            else { DeptiD2 = DeptID.ToString(); }
            if (DeptID != 7)
            {
                if (Flag != "2")
                {
                    QueryA = "select DISTINCT(B.Alias) DeptName from workorder as A " +
                             " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
                             " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "' " +
                             " and A.Apv>=0 and MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ";
                }
                else if (Flag == "2")
                {
                    QueryA ="select distinct UsersWO DeptName from temp_dataWO order by UsersWO ";
                }
            }

            if (DeptID == 7)
            {
                QueryA = "select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from " +
                         " (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
                         " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "'  and A.Apv>=0 and " +
                         " MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ) as xx ";
            }
            query = QueryA;
            return query;
        }

        public static string RetrieveNamaDept_PencapaianWO(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID)
        {
            string query = "";
            string Query1 = string.Empty;
            string DeptiD2 = string.Empty;
            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25) { DeptiD2 = "19"; }
            else { DeptiD2 = DeptID.ToString(); }
            string strSQL ="select distinct UsersWO DeptName from temp_dataWO order by UsersWO ";
            query = strSQL;
            return query;
        }

        public static string RetrieveNamaDept_Keluar(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID, int FLagUser, string NamaSub)
        {
            string query = "";
            string Query1 = string.Empty; string DeptiD2 = string.Empty;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25){ DeptiD2 = "19"; }
            else{ DeptiD2 = DeptID.ToString(); }

            if (FLagUser > 0 && DeptiD2 != "23") { Query1 = " DeptID_Users=" + DeptiD2 + " and NamaSubDept='" + NamaSub.Trim() + "' "; }
            else if (DeptiD2 == "23") { Query1 = " DeptID_Users=" + FLagUser + " and NamaSubDept='" + NamaSub.Trim() + "' "; }
            else{ Query1 = " DeptID_Users=" + DeptiD2 + ""; }

            string strSQL =
            " select distinct b.Alias DeptName from ( " +
            " select * from ( " +
            " select * from ( " +
            " select isnull((select top 1 CreatedTime from WorkOrder_LogApproval B where B.WOID=A.ID and B.RowStatus>-1 and B.Urutan=1),0)ApvMgrUser, " +
            " * from WorkOrder A where  " + Query1 + " and PlantID=" + UnitKerjaID + ") as x where left(convert(char,ApvMgrUser,112),4)<>'1900' " +
            " ) as xx where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Periode + "' " +
            " ) as xxx  " +
            " inner join Dept b ON b.ID=xxx.DeptID_Users ";
            query = strSQL;
            return query;
        }

        public static string RetrieveNamaDept_Masuk(string status, int DeptID, int Tahun, string Periode, string Flag, int StatusReport, string Corp, int UnitKerjaID)
        {
            string query = "";
            string Query1 = string.Empty; string QueryA = string.Empty;

            if (DeptID == 19 || DeptID == 4 || DeptID == 18 || DeptID == 5 || DeptID == 25)
            {
                DeptID = 19;
                Query1 =
                " and YEAR(A.CreatedTime)=" + Tahun + " and MONTH(A.CreatedTime)=" + Periode + " and " +
                " (" +
                " (A.Apv>=2 and A.VerSec=1 and A.AreaWO='Kendaraan' and A.RowStatus > -1) or " +
                " (A.Apv>=2 and A.AreaWO<>'Kendaraan' and A.RowStatus > -1 )" +
                " )" +
                " and A.DeptID_PenerimaWO=" + DeptID + " and A.RowStatus > -1";
                QueryA =
               " select DISTINCT(B.Alias) DeptName from workorder as A " +
               " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
               " where A.PlantID=" + UnitKerjaID + " " +
               " " + Query1 + " ";
            }
            else if (DeptID == 7)
            {
                Query1 =
                " and YEAR(A.CreatedTime)=" + Tahun + " and MONTH(A.CreatedTime)=" + Periode + " and A.Apv>=2" +
                " and A.DeptID_PenerimaWO=" + DeptID + " and A.RowStatus > -1 ) as xx";
                QueryA =
                " select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from " +
                " (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                " where A.PlantID=" + UnitKerjaID + "" +
                " " + Query1 + " ";
            }
            else if (DeptID == 14)
            {
                Query1 =
                " and (" +
                " (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='SoftWare' and A.ApvOP in (2) and A.Apv>=2 and A.DeptID_Users in (" + Corp + ") and A.ID in (select WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=3)) or  " +
                " (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='SoftWare' and A.ApvOP in (2,-2) and A.Apv>=2 and A.DeptID_Users not in (" + Corp + ",11) and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=3)) or " +
                " (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.DeptID_Users=11 and A.AreaWO='SoftWare' and A.ApvOP in (2,-2) and A.Apv>=2 and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)) or " +
                " (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='HardWare' and A.Apv>=2 and A.DeptID_Users in (" + Corp + ")) and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)) or " +
                " (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='HardWare' and A.Apv>=2 and A.DeptID_Users not in (" + Corp + ") and A.ID in (select WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)" +
                " )";
                QueryA =
                " select DISTINCT(B.Alias) DeptName from workorder as A " +
                " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                " where A.PlantID=" + UnitKerjaID + " " +
                " " + Query1 + " ";
            }
            query = QueryA;
            return query;
        }

        public static List<LapWorkOrderNf.ParamData> ListData(string qry)
        {
            List<LapWorkOrderNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = qry;
                    AllData = connection.Query<LapWorkOrderNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string RetrieveDeptName(string dept)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string DeptID = string.Empty;
                if (dept == "25")
                {
                    DeptID = "19";
                }
                else
                {
                    DeptID = dept;
                }
                try
                {
                    string query = " select Alias DeptName from dept where rowstatus > -1 and ID=" + DeptID + " ";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveDeptNameSub(string UserID)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = " select 'HRD'+' '+'-'+' '+NamaSubDept DeptName from WorkOrder_SubDept where UserID=" + UserID + " or HeadID=" + UserID + " and RowStatus>-1 ";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveListWO(string NamaSub, string PenerimaWO, string UsersWO, string Periode, int Tahun, string Tanda, string WaktuSkr, int UnitKerjaID)
        {
            string qry = "";
            string query = string.Empty;
            if (Tanda == "2" || Tanda == "3" || Tanda == "5")/** Pemantauan,Pencapaian dan WO Masuk **/
            {
                query = " UsersWO='" + UsersWO + "' and penerimaWO='" + PenerimaWO + "' order by NoWO desc ";
            }
            else if (Tanda == "4")/** Pencapaian WO Keluar **/
            {
                query = " UsersWO='" + UsersWO + "' order by NoWO desc ";
            }
            string strSQL = "select UsersWO, NoWO, UraianPekerjaan, AreaWO, CreatedTime, ApvMgr, Waktu2, DueDateWO, FinishDate2, Pelaksana, StatusApv, SisaHari, StatusWO, CreatedBy, Selisih from temp_dataWO where " + query + " ";
            qry = strSQL;
            return qry;
        }

        public static List<LapWorkOrderNf.ParamDataWo> ListDataWo(string qry)
        {
            List<LapWorkOrderNf.ParamDataWo> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = qry;
                    AllData = connection.Query<LapWorkOrderNf.ParamDataWo>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static void LoadDataMaster_WO(string FLag, int DeptID, int PlantID, int Tahun, int Bulan)
        {
            string query = string.Empty;
            /** Pencapaian WO Bulanan **/
            if (FLag == "3")
            {
                query =
                " where YEAR(DueDateWO)='" + Tahun + "' and MONTH(DueDateWO)='" + Bulan + "' " +
                " and DeptID_PenerimaWO='" + DeptID + "' ";
            }
            /** Pemantauan WO masuk status Open(apv mgr peminta) - Close **/
            else if (FLag == "2")
            {
                query =
                //" where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Bulan + "' "+
                //" and DeptID_PenerimaWO='" + DeptID + "'  ";
                " where (YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Bulan + "' and DeptID_PenerimaWO='" + DeptID + "'  and PlantID='" + PlantID + "') " +
                " or " +
                " (YEAR(CreatedTime)='" + Tahun + "' and MONTH(CreatedTime)='" + Bulan + "' and ApvMgrUser is null  and PlantID='" + PlantID + "') order by NoWO,Target ";
            }
            /** Pencapaian WO Keluar **/
            else if (FLag == "4")
            {
                query =
                " where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Bulan + "' " +
                " and DeptID_Users='" + DeptID + "'  ";
            }
            /** Pencapaian WO Masuk **/
            else if (FLag == "5")
            {
                query =
                " where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Bulan + "' " +
                " and DeptID_PenerimaWO='" + DeptID + "'  ";
            }

            ZetroView lst = new ZetroView();
            lst.QueryType = Operation.CUSTOM;
            lst.CustomQuery =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO] " +

            " /** Break 7 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser ApvMgr,UpdateTargetTime Waktu2,DueDateWO,FinishDate FinishDate2,Pelaksana,CreatedBy,selisih_apv Selisih,case when SisaHari=0 then 0 else SisaHari end SisaHari,StatusWO,StatusApv,UsersWO,PenerimaWO,AreaWO SubArea2,AreaWO,PlantID into temp_dataWO from ( " +
            " /** Break 6 **/ " +
            " select woID,StatusWO,StatusApv,NoWO,UraianPekerjaan,UsersWO,PenerimaWO,AreaWO,DeptID_Users,DeptID_PenerimaWO,Target, " +
            " left(convert(char,CreatedTime,106),12)CreatedTime,left(convert(char,ApvMgrUser,106),12)ApvMgrUser,left(convert(char,UpdateTargetTime,106),12)UpdateTargetTime,left(convert(char,DueDateWO,106),12)DueDateWO,left(convert(char,FinishDate,106),12)FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,selisih_apv,case when selisih_pekerjaan<0 then '0 HK' else selisih_pekerjaan + ' HK' end selisih_pekerjaan,WaktuSelesai,WaktuDateLine,  case   " +
            " when Target=1 and   YEAR(WaktuSelesai)>1900   then 0     " +
            " when Target=1 and YEAR(WaktuSelesai)=1900 then selisih_pekerjaan     " +
            " when Target=1 and WaktuSelesai = '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai <> '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai = '' then selisih_pekerjaan  end SisaHari,PlantID from ( " +
            " /** Break 5 **/ " +
            " select *, convert(varchar,(select SUM(Selisih)Selisih from  (  select datediff(dd, Waktu1, Waktu2) - case when DATEPART(dw,Waktu2)=7  then 1 when DATEPART(dw,Waktu2)=1 then 1 else 0  end  - (datediff(wk, Waktu1, Waktu2) * 2) -   case when datepart(dw, Waktu1) = 1 then 1 else 0 end +  case when datepart(dw, Waktu2) = 1  then 1 else 0 end Selisih   " +
            " union all  " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=Waktu1 and  LEFT(convert(char,harilibur,112),8)<=Waktu2) as selisih))+' '+'HK' selisih_apv, " +
            " convert(varchar,(select SUM(Selisih)Selisih from  (  select datediff(dd, WaktuNow, DueDateWO) - case when DATEPART(dw,DueDateWO)=7  then 1 when DATEPART(dw,DueDateWO)=1 then 1 else 0  end  - (datediff(wk, WaktuNow, DueDateWO) * 2) -   case when datepart(dw, WaktuNow) = 1 then 1 else 0 end +  case when datepart(dw, DueDateWO) = 1  then 1 else 0 end Selisih  " +
            " union all  " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=WaktuNow and  LEFT(convert(char,harilibur,112),8)<=DueDateWO) as selisih)) selisih_pekerjaan, " +
            " case  " +
            " when DeptID_PenerimaWO=14 and Apv=1 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) then 'Next: PM' " +
            " when Apv=0 and DeptID_PenerimaWO=7 and DeptID_Users=7 then 'Next: Head'+' - '+NamaSubDept   " +
            " when Apv=0 then 'Next: Mgr Dept'  " +
            " when Cancel = 1 then 'Cancel - T3'  " +
            " when (Target is null or Target =1 ) and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat'  " +
            " when  Target =2  and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat - T2'  " +
            " when  Target =3  and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat - T3'  when FinishDate is not null and FinishDate<=DueDateWO and Apv=4 then 'Finish'   " +
            " when FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=4 then 'Finish'  " +
            " when (Target is null or Target =1 ) and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo' " +
            " when Target =2 and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T2'   " +
            " when Target =3 and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T3'  " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=7 and Pelaksana<>'' and DueDateWO is not null  then 'Next: Mgr HRD' " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO is null and AreaWO like'%Software%'  then 'Next: Verifikasi ISO'  " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO like'%Software%'  then 'Next: Mgr IT'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO like'%Software%'  then 'Next: Mgr IT'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP is null and AreaWO like'%Software%'  then 'Next: Mgr Dept - Plant Terkait'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO is null and AreaWO like'%Software%' then 'Next: Verifikasi ISO'   " +
            " when Pelaksana is not null and DueDateWO is null   and DeptID_PenerimaWO=19  then 'Next:' + Pelaksana   " +
            " when Pelaksana is not null and DueDateWO is not null   and DeptID_PenerimaWO=19  and Apv=2  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=19  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=14  and Apv=2 and AreaWO='HardWare'  then 'Next: Mgr IT'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=7  and Apv=2 and VerSec=1  then 'Next: Mgr HRD'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=7  and Apv=2 and AreaWO<>'Kendaraan'  then 'Next: Mgr HRD'  " +
            " when Pelaksana <> '' and DueDateWO is not null   and DeptID_PenerimaWO=7  and Apv=2  then 'Next: Mgr HRD'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO is null then 'Next : Head GA'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO is not null then 'Next: Mgr HRD'   " +
            " when Target =3 and  FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T3'   " +
            " when Target =2 and  FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T2'  " +
            " when (Target is null or Target =1 ) and FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai' " +
            " when Target=3 and WaktuNow<WaktuDateLine then 'Progress - T3'  " +
            " when Target=2 and WaktuNow<WaktuDateLine then 'Progress - T2'   " +
            " when Target=1 and WaktuNow<WaktuDateLine then 'Progress - T1'  " +
            " when Target is null then 'Progress'   " +
            " when Target=3 and WaktuNow>WaktuDateLine then 'Lewat - T3'   " +
            " when Target=2 and WaktuNow>WaktuDateLine then 'Lewat - T2' " +
            " when Target=1 and WaktuNow>DueDateWO then 'Lewat - T1'   " +
            " when Target is null then 'Lewat'  end StatusWO,   " +
            " case  when apv=0 then 'Open'  " +
            " when Apv=2 and DeptID_PenerimaWO=19 and Pelaksana='' then 'Apv Mgr'   " +
            " when Apv=2 and ((Pelaksana is not null or Pelaksana <> '') and (DueDateWO is null or DueDateWO='')) and DeptID_PenerimaWO=19 then 'Apv Mgr MTN-1'   " +
            " when Apv=2 and ((Pelaksana is not null or Pelaksana <> '') and (DueDateWO is not null or DueDateWO<>'')) and DeptID_PenerimaWO=19 then 'Apv Head'+'-'+Pelaksana   " +
            " when Apv=2 and DeptID_PenerimaWO=7 and DeptID_Users<>7 and Pelaksana='SPV GA' and DueDateWO is null then 'Apv Mgr'    " +
            " when Apv=2 and DeptID_PenerimaWO=7 and Pelaksana='SPV GA' and DueDateWO is not null then 'Apv Head GA'   " +
            " when Apv=2 and DeptID_PenerimaWO=7 and DeptID_Users=7 then 'Apv Head '+NamaSubDept   " +
            " when Apv=2 then 'Apv Mgr'  " +
            " when apv=3 and DeptID_PenerimaWO=19 then 'Apv Mgr MTN'   " +
            " when apv=3 and DeptID_PenerimaWO=7 then 'Apv Mgr HRD'    " +
            " when apv=3 and DeptID_PenerimaWO=14 then 'Apv Mgr IT'   " +
            " when apv=4 then 'Finish' " +
            " when apv=5 and waktu0>=0 then 'Closed'  " +
            " when apv=5 and waktu0<0 then 'Lewat' " +
            " end StatusApv " +
            " from ( " +
            " /** Break 4 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser,UpdateTargetTime,DueDateWO,/**case when waktu0<0 then null when waktu0 is null then null else FinishDate end **/FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,NamaSubDept,VerISO,WaktuSelesai,WaktuDateLine,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID,waktu0 from ( " +
            " /** Break 3 **/ " +
            " select *, " +
            " case  " +
            " when Target =1 then (select top 1 a1.CreatedTime from WorkOrder_LogApproval a1 where a1.WOID=x1.woID  and a1.RowStatus>-1 and a1.Urutan=1)  " +
            " when Target=2 then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=2) " +
            " when Target=3 then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=3) " +
            " end ApvMgrUser, " +
            " case  " +
            " when target=1 then (select top 1 aa.UpdateTargetTime from WorkOrder aa where aa.ID=x1.woID and aa.RowStatus>-1)  " +
            " when target=2 then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=2)  " +
            " when target=3 then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=3)  " +
            " end UpdateTargetTime, " +
            " DATEDIFF(DAY,LEFT(CONVERT(CHAR,FinishDate,112),8),DueDateWO)waktu0,LEFT(convert(char,DueDateWO,112),8) WaktuDateLine from ( " +
            " /** Break 2 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,StatusTarget, " +
            " case when Target is null then '1' else Target end Target,case when TglTarget is null then DueDateWO_awal else TglTarget end DueDateWO,FinishDate, " +
            " case when Target is null then CreatedTime when Target=1 then CreatedTime else CreatedTime2 end CreatedTime,UpdateTargetTime0,Pelaksana,CreatedBy,Apv,VerISO,NamaSubDept,WaktuSelesai,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID from ( " +
            " /** Break 1 **/ " +
            " select A.ID woID,A.NoWO,A.UraianPekerjaan,A.DeptID_Users,A.DeptID_PenerimaWO,A.StatusTarget,A.DueDateWO DueDateWO_awal,D.TglTarget,D.Target,A.FinishDate,A.CreatedTime,D.CreatedTime CreatedTime2,A.UpdateTargetTime UpdateTargetTime0,Pelaksana,A.CreatedBy,A.Apv,A.VerISO,A.NamaSubDept,isnull(LEFT(convert(char,FinishDate,112),8),'')WaktuSelesai,case when A.SubArea ='' or A.SubArea is null then TRIM(A.AreaWO) else TRIM(A.AreaWO) +' - ' + TRIM(A.SubArea) end AreaWO,A.ApvOP,A.VerSec,A.Cancel,GETDATE()WaktuNow,C.Alias UsersWO,C1.Alias PenerimaWO,A.PlantID " +
            " from WorkOrder as A  " +
            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
            " LEFT JOIN Dept as C1 ON A.DeptID_PenerimaWO=C1.ID  " +
            " LEFT JOIN WorkOrder_Target D ON D.WoID=A.ID  where A.RowStatus>-1" +
            " /** end Break 1 **/ " +
            " ) as x    " +
            " /** end Break 2 **/ " +
            " ) as x1 " +
            " /** end Break 3 **/ " +
            " ) as x2  " +
            " /** end Break 4 **/ " +
            " ) as x3  " +
            " /** end Break 5 **/ " +
            " ) as x4  " +
            " /** end Break 6 **/ " +
            " ) as x5 " +
            "" + query + "" +
            " /** end Break 7 **/ ";
            SqlDataReader lst2 = lst.Retrieve();
        }

        public static int RetrieveTotalWO(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string DeptID2 = string.Empty; string Query1 = string.Empty;

                if (StatusReport == 5)
                {
                    DeptID2 = DeptID3;
                }
                else if (StatusReport == 0)
                {
                    if (DeptID == 4 || DeptID == 5 || DeptID == 18)
                    { DeptID2 = "19"; }
                    else
                    { DeptID2 = DeptID.ToString(); }
                }

                if (DeptID2 == "7")
                {
                    Query1 = " apv>=2 ";
                }
                else { Query1 = " apv>=2 "; }
                string StrSql =
                " select COUNT(WOID)TotalWO  from ( " +
                " select distinct(WOID)WOID from  " +
                " (select ID WOID from WorkOrder where MONTH(DueDateWO)=" + Bulan + "   and   " + Query1 + "   and  " +
                " YEAR(DueDateWO)=" + Tahun + "   and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + " group by ID " +
                " union all  " +
                " select WOID from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + "   and YEAR(TglTarget)=" + Tahun + "    " +
                " and RowStatus > -1 and WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID " +
                " ) as xx ) as xxx ";
                try
                {
                    string query = StrSql;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveTtlWO_Break(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string DeptID2 = string.Empty; string Query1 = string.Empty;

                if (StatusReport == 5)
                {
                    DeptID2 = DeptID3;
                }
                else if (StatusReport == 0)
                {
                    if (DeptID == 4 || DeptID == 5 || DeptID == 18)
                    { DeptID2 = "19"; }
                    else
                    { DeptID2 = DeptID.ToString(); }
                }

                if (DeptID2 == "7")
                {
                    Query1 = " apv>=2 ";
                }
                else { Query1 = " apv>=2 "; }

                string StrSql =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01]') AND type in (N'U')) DROP TABLE [dbo].[tempData01] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02]') AND type in (N'U')) DROP TABLE [dbo].[tempData02] " +

                " select * into tempData01 from ( " +

                " select 'A'Flag,TRIM(Pelaksana) +' : '+cast(COUNT(WOID) as varchar) Ket " +

                " from (  select distinct(WOID)WOID,Pelaksana from   ( " +
                " select ID WOID,Pelaksana " +
                " from WorkOrder " +
                " where MONTH(DueDateWO)=" + Bulan + "   and    " + Query1 + "    and   YEAR(DueDateWO)=" + Tahun + "   and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + "" +
                " group by ID,Pelaksana  " +
                " union all   " +
                " select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-2)Pelaksana " +
                " from WorkOrder_Target " +
                " where MONTH(TglTarget)=" + Bulan + "   and YEAR(TglTarget)=" + Tahun + "     and RowStatus > -1 and " +
                " WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID " +
                " ) as xx ) as xxx  group by Pelaksana ) as x1 " +
                " select Flag,Ket into tempData02 from tempData01 x " +
                " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket FROM tempData01 AS x2 " +
                " WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData02 x3 group by x3.Ket,Flag " +

                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01]') AND type in (N'U')) DROP TABLE [dbo].[tempData01]  " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02]') AND type in (N'U')) DROP TABLE [dbo].[tempData02]  ";
                try
                {
                    string query = StrSql;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveTotalWO_PerHead(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string DeptID2 = string.Empty; string Query1 = string.Empty;

                if (StatusReport == 5)
                {
                    DeptID2 = DeptID3;
                }
                else if (StatusReport == 0)
                {
                    if (DeptID == 4 || DeptID == 5 || DeptID == 18)
                    { DeptID2 = "19"; }
                    else
                    { DeptID2 = DeptID.ToString(); }
                }

                if (DeptID2 == "7")
                {
                    Query1 = " apv>=2 ";
                }
                else { Query1 = " apv>=2 "; }

                string StrSql =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDatattl]') AND type in (N'U')) DROP TABLE [dbo].[tempDatattl]  " +

                " select * into tempDatattl from ( " +
                " select COUNT(Pelaksana)TotalWO,Pelaksana " +
                " from ( select WOID,Pelaksana " +
                " from (select ID WOID,Pelaksana from WorkOrder where MONTH(DueDateWO)=" + Bulan + "    and    apv>=2    and   YEAR(DueDateWO)=" + Tahun + " " +
                " and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + " group by ID ,Pelaksana " +
                " union all   " +
                " select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-1)Pelaksana " +
                " from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + "    and YEAR(TglTarget)=" + Tahun + "     and RowStatus > -1 and " +
                " WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID ) as x " +
                " group by woid,pelaksana ) as x1 group by pelaksana ) as x2 " +

                " select * from tempDatattl ";
                try
                {
                    string query = StrSql;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveStatusWO(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Query = string.Empty;

                if (Tanda == "1")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4";
                }
                else if (Tanda == "3")
                {
                    Query = " where A.RowStatus>-1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                            " and LEFT(convert(char,DueDateWO,112),8)>=LEFT(convert(char,FinishDate,112),8) and " +
                            " LEFT(convert(char,DueDateWO,112),6)='" + Periode2 + "' and Apv>4 ";

                }
                else if (Tanda == "4")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4 ";
                }
                else if (Tanda == "7")
                {
                    Query = " where A.RowStatus>-1 and A.Apv>=4  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " ";
                }
                else if (Tanda == "0")
                {
                    Query = " where A.RowStatus>-1 " +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " " +
                            " and A.apv>=4 ";
                }

                string strSQL = " select COUNT(NoWO)Total " +
                                " from WorkOrder as A  " +
                                " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                                " " + Query + "";
                try
                {
                    string query = strSQL;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveStatusWO_break(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Query = string.Empty;

                if (Tanda == "1")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4" +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                            " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                            " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                }
                else if (Tanda == "3")
                {
                    Query =
                    " where A.RowStatus>-1  and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                    " and LEFT(convert(char,DueDateWO,112),8)>=LEFT(convert(char,FinishDate,112),8) and  LEFT(convert(char,DueDateWO,112),6)='" + Periode2 + "' and Apv>4 " +
                    " group by Pelaksana,A.ID )  as xxx  group by Pelaksana " +

                    " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                    " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                }
                else if (Tanda == "4")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4 " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                            " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                            " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                }
                else if (Tanda == "7")
                {
                    Query = " where A.RowStatus>-1 and A.Apv>=4  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                            " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                            " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                }
                else if (Tanda == "0")
                {
                    Query = " where A.RowStatus>-1 " +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " " +
                            " and A.apv>=4 " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                            " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                            " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                }

                string strSQL =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01]') AND type in (N'U')) DROP TABLE [dbo].[tempData01] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02]') AND type in (N'U')) DROP TABLE [dbo].[tempData02] " +

                " select 'A'Flag, TRIM(Pelaksana) + ' : ' + cast(COUNT(WOID) as varchar) Ket  into tempData01 from ( " +
                " select A.ID WOID,Pelaksana  from WorkOrder as A   LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
                " " + Query + "";

                try
                {
                    string query = strSQL;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string RetrieveStatusWO_breakPersen(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Query = string.Empty;
                if (Tanda == "1")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4" +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +

                            " select x3.Ket,x3.Pelaksana,A.TotalWO,A.Pelaksana Pelaksana2  into tempData02A " +
                            " from tempData01A x3 right join tempDatattl A ON A.Pelaksana=x3.Pelaksana group by x3.Ket,x3.Pelaksana,TotalWO,A.Pelaksana " +

                            " select isnull(cast(Ket as decimal(10,2)),0)ttl01,case when Pelaksana is null then Pelaksana2 else Pelaksana end Pelaksana, " +
                            " cast(TotalWO as decimal(10,2)) ttl02 into tempData03A from tempData02A " +

                            " select *,cast((ttl01/ttl02) as decimal(10,2))* 100 Ttl into tempData04A from tempData03A " +
                            " select cast(ttl as int)Persen,Pelaksana into tempData05A from tempData04A " +
                            " select 'A'Flag,Pelaksana +' : '+trim(cast(Persen as nchar))+' %' Ket into tempData06A from tempData05A " +
                            " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + trim(Ket)  FROM tempData06A AS x2  WHERE x2.Flag = x3.Flag  " +
                            " FOR XML PATH('')), 1, 1, '') from tempData06A x3 ";

                }
                else if (Tanda == "3")
                {
                    Query =
                    " where A.RowStatus>-1  and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                    " and LEFT(convert(char,DueDateWO,112),8)>=LEFT(convert(char,FinishDate,112),8) and  LEFT(convert(char,DueDateWO,112),6)='" + Periode2 + "' " +
                    " and Apv>4  group by Pelaksana,A.ID )  as xxx  group by Pelaksana  " +
                    " select x3.Ket,x3.Pelaksana,A.TotalWO,A.Pelaksana Pelaksana2  into tempData02A " +
                    " from tempData01A x3 right join tempDatattl A ON A.Pelaksana=x3.Pelaksana group by x3.Ket,x3.Pelaksana,TotalWO,A.Pelaksana " +

                    " select isnull(cast(Ket as decimal(10,2)),0)ttl01,case when Pelaksana is null then Pelaksana2 else Pelaksana end Pelaksana, " +
                    " cast(TotalWO as decimal(10,2)) ttl02 into tempData03A from tempData02A " +

                    " select *,cast((ttl01/ttl02) as decimal(10,2))* 100 Ttl into tempData04A from tempData03A " +
                    " select cast(ttl as int)Persen,Pelaksana into tempData05A from tempData04A " +
                    " select 'A'Flag,Pelaksana +' : '+trim(cast(Persen as nchar))+' %' Ket into tempData06A from tempData05A " +
                    " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + trim(Ket)  FROM tempData06A AS x2  WHERE x2.Flag = x3.Flag  " +
                    " FOR XML PATH('')), 1, 1, '') from tempData06A x3 ";
                }
                else if (Tanda == "4")
                {
                    Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                            " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4 " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +

                            " select x3.Ket,x3.Pelaksana,A.TotalWO,A.Pelaksana Pelaksana2  into tempData02A " +
                    " from tempData01A x3 right join tempDatattl A ON A.Pelaksana=x3.Pelaksana group by x3.Ket,x3.Pelaksana,TotalWO,A.Pelaksana " +

                    " select isnull(cast(Ket as decimal(10,2)),0)ttl01,case when Pelaksana is null then Pelaksana2 else Pelaksana end Pelaksana, " +
                    " cast(TotalWO as decimal(10,2)) ttl02 into tempData03A from tempData02A " +

                    " select *,cast((ttl01/ttl02) as decimal(10,2))* 100 Ttl into tempData04A from tempData03A " +
                    " select cast(ttl as int)Persen,Pelaksana into tempData05A from tempData04A " +
                    " select 'A'Flag,Pelaksana +' : '+trim(cast(Persen as nchar))+' %' Ket into tempData06A from tempData05A " +
                    " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + trim(Ket)  FROM tempData06A AS x2  WHERE x2.Flag = x3.Flag  " +
                    " FOR XML PATH('')), 1, 1, '') from tempData06A x3 ";
                }
                else if (Tanda == "7")
                {
                    Query = " where A.RowStatus>-1 and A.Apv>=4  " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +

                            " select x3.Ket,x3.Pelaksana,A.TotalWO,A.Pelaksana Pelaksana2  into tempData02A " +
                    " from tempData01A x3 right join tempDatattl A ON A.Pelaksana=x3.Pelaksana group by x3.Ket,x3.Pelaksana,TotalWO,A.Pelaksana " +

                    " select isnull(cast(Ket as decimal(10,2)),0)ttl01,case when Pelaksana is null then Pelaksana2 else Pelaksana end Pelaksana, " +
                    " cast(TotalWO as decimal(10,2)) ttl02 into tempData03A from tempData02A " +

                    " select *,cast((ttl01/ttl02) as decimal(10,2))* 100 Ttl into tempData04A from tempData03A " +
                    " select cast(ttl as int)Persen,Pelaksana into tempData05A from tempData04A " +
                    " select 'A'Flag,Pelaksana +' : '+trim(cast(Persen as nchar))+' %' Ket into tempData06A from tempData05A " +
                    " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + trim(Ket)  FROM tempData06A AS x2  WHERE x2.Flag = x3.Flag  " +
                    " FOR XML PATH('')), 1, 1, '') from tempData06A x3 ";
                }
                else if (Tanda == "0")
                {
                    Query = " where A.RowStatus>-1 " +
                            " and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +
                            " and MONTH(A.DueDateWO)=" + Periode + " and " +
                            " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " " +
                            " and A.apv>=4 " +

                            " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                            " select x3.Ket,x3.Pelaksana,A.TotalWO,A.Pelaksana Pelaksana2  into tempData02A " +
                    " from tempData01A x3 right join tempDatattl A ON A.Pelaksana=x3.Pelaksana group by x3.Ket,x3.Pelaksana,TotalWO,A.Pelaksana " +

                    " select isnull(cast(Ket as decimal(10,2)),0)ttl01,case when Pelaksana is null then Pelaksana2 else Pelaksana end Pelaksana, " +
                    " cast(TotalWO as decimal(10,2)) ttl02 into tempData03A from tempData02A " +

                    " select *,cast((ttl01/ttl02) as decimal(10,2))* 100 Ttl into tempData04A from tempData03A " +
                    " select cast(ttl as int)Persen,Pelaksana into tempData05A from tempData04A " +
                    " select 'A'Flag,Pelaksana +' : '+trim(cast(Persen as nchar))+' %' Ket into tempData06A from tempData05A " +
                    " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + trim(Ket)  FROM tempData06A AS x2  WHERE x2.Flag = x3.Flag  " +
                    " FOR XML PATH('')), 1, 1, '') from tempData06A x3 ";
                }

                string strSQL =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01A]') AND type in (N'U')) DROP TABLE [dbo].[tempData01A] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02A]') AND type in (N'U')) DROP TABLE [dbo].[tempData02A] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData03A]') AND type in (N'U')) DROP TABLE [dbo].[tempData03A] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData04A]') AND type in (N'U')) DROP TABLE [dbo].[tempData04A] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData05A]') AND type in (N'U')) DROP TABLE [dbo].[tempData05A] " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData06A]') AND type in (N'U')) DROP TABLE [dbo].[tempData06A] " +

                "  select cast(COUNT(WOID) as varchar) Ket,Pelaksana  into tempData01A from ( " +

                " select A.ID WOID,Pelaksana  from WorkOrder as A   LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
                " " + Query + "";
                try
                {
                    string query = strSQL;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<LapWorkOrderNf.ParamPencapaianNilai> PencapaianNilai(string txtTotal, string KetTotal, string LabelTotalNilai, string txtTarget, string KetTarget, string LabelTargetNilai, string txtPersen, string KetPersen, string LabelPersenNilai)
        {
            List<LapWorkOrderNf.ParamPencapaianNilai> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select '"+ txtTotal + "' txtTotal,'" + KetTotal + "' KetTotal, '" + LabelTotalNilai + "' LabelTotalNilai, '" + txtTarget + "' txtTarget, '" + KetTarget + "' KetTarget, '" + LabelTargetNilai + "' LabelTargetNilai, '" + txtPersen + "' txtPersen, '" + KetPersen + "' KetPersen, '" + LabelPersenNilai + "' LabelPersenNilai ";
                    AllData = connection.Query<LapWorkOrderNf.ParamPencapaianNilai>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int getDeptID_P(string deptname)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 id from dept where alias='" + deptname + "'";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int getDeptID(string ID)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select id from spd_dept where dptid =" + ID;
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
