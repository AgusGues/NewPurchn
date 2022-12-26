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
    public class WorkOrderFacade : AbstractFacade
    {
        private WorkOrder objWO = new WorkOrder();
        private ArrayList arrWO;
        private List<SqlParameter> sqlListParam;


        public WorkOrderFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public int insertWO(object objDomain)
        {
            int result = 0;
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoWO", objWO.NoWO));
                sqlListParam.Add(new SqlParameter("@DeptID_Users", objWO.DeptID_Users));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objWO.CreatedBy));
                sqlListParam.Add(new SqlParameter("@UraianPekerjaan", objWO.UraianPekerjaan));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_InsertWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
           
        }

        public int InsertLog_Apv(object objDomain)
        {
            int result = 0;
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@UserID", objWO.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                //sqlListParam.Add(new SqlParameter("@UraianPekerjaan", objWO.UraianPekerjaan));
                result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_InsertLogApproval");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int insertNo(object objDomain)
        {            
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));               
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_InsertNoWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
            
        }

        public int updateNo(object objDomain)
        {            
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NomorUrut", objWO.NomorUrut));
                sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));  
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateNoWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
            
        }

        public int UpdateWO_Closed(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objWO.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateClosedWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateCancel(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objWO.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_CancelWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int CancelWO_Lampiran(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objWO.ID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateWO_Lampiran");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int Cancel_WO(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objWO.ID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_Cancel_WO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateApvWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_DueDate(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                //sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateDueDateWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv_L3(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@Pelaksana", objWO.Pelaksana));
                sqlListParam.Add(new SqlParameter("@DueDateWO", objWO.DueDateWO));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateWOL3");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv_L4(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@FinishDate", objWO.FinishDate));
                sqlListParam.Add(new SqlParameter("@UraianPerbaikan", objWO.UraianPerbaikan));
               
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateWOL4");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv_L5(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_UpdateWOL5");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int insertLampiranWO(object objDomain)
        {
            try
            {
                objWO = (WorkOrder)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@FileName", objWO.FileName));                
                sqlListParam.Add(new SqlParameter("@CreatedBy", objWO.CreatedBy));               

                int intResult = dataAccess.ProcessData(sqlListParam, "WorkOrder_SP_InsertLapiranWO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public string RetrieveWOID(string WOID)
        {
            string result = string.Empty;
            string StrSql = " select FileLampiran FileName from WorkOrder_Lampiran where rowstatus > -1 and WOID=" + WOID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["FileName"].ToString();
                }
            }

            return result;
        }

        public string RetrieveWOID_int(string ID)
        {
            string result = string.Empty;
            string StrSql = " select ID WOID from WorkOrder where ID in (select WOID from WorkOrder_Lampiran " +
                            " where ID="+ID+" and RowStatus>-1) and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["WOID"].ToString();
                }
            }

            return result;
        }

       

        

        public int RetrieveUserLevel(int UserID)
        {            
            string StrSql = " select Status StatusApv from WorkOrder_ListApv where RowStatus > -1 and UserID="+UserID+" ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["StatusApv"]);
                }
            }

            return 0;
        }

        public int Retrieve_apv_wo(int ID)
        {
            string StrSql = " select Apv from WorkOrder where RowStatus > -1 and ID="+ ID +"";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Apv"]);
                }
            }

            return 0;
        }

        public int Retrieve_apv_wo_atch(int ID)
        {
            string StrSql = " select Apv from WorkOrder where ID in (select WOID from WorkOrder_Lampiran where ID="+ID+" and RowStatus>-1) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Apv"]);
                }
            }

            return 0;
        }


        public int RetrieveNoWO(string Bulan, string Tahun)
        {
            string StrSql = " select NoUrut from WorkOrder_Nomor where Bulan="+Bulan+" and  Tahun="+Tahun+" ";                            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["NoUrut"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveListWO(string DeptName, string Periode, int Tahun, string Tanda, string WaktuSkr)
        {
            string Query = string.Empty;
            //string Query2 = string.Empty;

            if (Tanda == "1")
            {
                Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 "+
                        " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)="+Tahun+")  " +
                        " and A.DeptID_Users in (select ID from dept where alias='"+DeptName+"' and rowstatus > -1)) as DataWO ) as DataWO2) as DataWO3) as DataWO4 order by WOID desc";
            }
            else if (Tanda == "2")
            {
                Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan>=3)  "+
                        " and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) and MONTH(A.DueDateWO)="+Periode+" and " +
                        " YEAR(DueDateWO)="+Tahun+") as DataWO ) as DataWO2) as DataWO3) as DataWO4 order by WOID desc";
                
            }


            string strSQL = " select case " +
                            " when FinishDate <> '' and FinishDate<=DueDateWO and Apv=4 then 'Next: Serah Terima' " +
                            " when FinishDate <> '' and FinishDate<=DueDateWO and Apv=5 then 'Closed' " +
                            " when FinishDate = '' and WaktuNow>WaktuDateLine  then 'Lewat' " +
                            " when FinishDate = '' and DueDateWO =''  then 'Next: Mgr MTN' " +
                            " when FinishDate = '' and  WaktuNow<WaktuDateLine then 'Progress' end 'StatusWO', " +
                            " case when  FinishDate <> '' and FinishDate<=DueDateWO then '0' else ElapsedDay end SisaHari,* from  (" +
                            " select WOID,NoWO,UraianPekerjaan,createdtime,DueDateWO,AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser," +
                            " ElapsedWeek, " +
                            " ElapsedDay,WaktuNow,WaktuDateLine,apv from" +
                            " (select WOID,NoWO,UraianPekerjaan,createdtime,case when Apv <3 then '' when Apv >=3 then DueDateWO end DueDateWO,AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser,ISNULL(ElapsedDay,0)ElapsedDay," +
                            " ISNULL(ElapsedWeek,0)ElapsedWeek,apv,WaktuNow ,WaktuDateLine from( " +
                            " select WOID,NoWO,UraianPekerjaan,createdtime,DueDateWO,AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser,apv," +

                            " case when Apv>2 then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) end ElapsedWeek, " +
                            " case when Apv>2 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) end ElapsedDay, " +
                            " LEFT(convert(char,'" + WaktuSkr + "',112),8) WaktuNow,   LEFT(convert(char,DueDateWO2,112),8)WaktuDateLine " +

                            //" case when Apv>2 then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) when Apv=4 then '' end ElapsedWeek,  " +
                //" case when Apv>2 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'"+WaktuSkr+"',112),8),DueDateWO) when Apv=4 then '' end ElapsedDay   " +

                            " from(select A.ID WOID,NoWO,UraianPekerjaan," +
                            " (select LEFT(convert(char,lp.CreatedTime,106),11) from WorkOrder_LogApproval as lp where lp.WOID=A.ID " +
                            " and lp.RowStatus>-1 and lp.Urutan=2)createdtime, " +
                            " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO,A.DueDateWO DueDateWO2,AreaWO," +
                            " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                            " ,ISNULL(Pelaksana,'')Pelaksana, " +
                            " case " +
                            " when A.apv=0 then 'Open' " +
                            " when A.apv=1 then 'Apv Mgr' " +
                            " when A.apv=2 then 'Apv PM'" +
                            " when A.apv=3 then 'Apv Mgr MTN'" +
                            " when A.apv=4 then 'Finish'" +
                            " when A.apv=5 then 'Closed'" +
                            " end StatusApv,C.Alias DeptName,(select wp.CreatedTime from WorkOrder_LogApproval wp where wp.WOID=A.ID and RowStatus > -1 and wp.Urutan=1)ApvMgrUser,apv from WorkOrder as A " +
                            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                //" where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval " +
                //" where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and rowstatus >-1 "+Query+") " +             
                //" and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) ) as DataWO" +
                //" ) as DataWO2) as DataWO3) as DataWO4 order by WOID desc";
                            " "+Query+"";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListWO_1(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

       
        public ArrayList RetrieveListWO_1(int DeptID, int Status)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            if (Status == 3) // Approval Mgr MTN
            {
                //Query1 = " and A.Apv = 2 order by A.DeptID_Users";
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan," +
                         " ISNULL(D.CreatedTime,'')CreatedTime,"+
                         " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO," +
                         " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                         " ,ISNULL(Pelaksana,'')Pelaksana, " +
                         " case " +
                         " when A.apv=0 then 'Open' " +
                         " when A.apv=1 then 'Aproved Mgr' " +
                         " when A.apv=2 then 'Approved PM'" +
                         " when A.apv=3 then 'Approved Mgr MTN'" +
                         " when A.apv=4 then 'Finish'" +
                         " when A.apv=5 then 'Closed'" +
                         " end StatusApv,C.Alias DeptName from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " and A.Apv = 2 order by A.DeptID_Users";
                Query3 = " LEFT JOIN WorkOrder_LogApproval as D on D.WOID=A.ID ";
            }
            else if (Status == 2) // Approval PM
            {
                //Query1 = " where A.RowStatus>-1 and A.Apv = 1 and D.Urutan=1 and D.RowStatus>-1 order by A.DeptID_Users";
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +                       
                         " LEFT(convert(char,D.CreatedTime,106),11)CreatedTime1,"+                         
                         " case when apv=0 then 'Open'" +
                         " when apv=1 then 'Mgr Dept.'" +
                         " when apv=2 then 'PM'" +
                         " when apv=3 then 'Mgr MTN' end StatusApv,C.Alias DeptName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID "+
                         " LEFT JOIN WorkOrder_LogApproval as D on D.WOID=A.ID ";
                Query2 = " where A.RowStatus>-1 and A.Apv = 1 and D.Urutan=1 and D.RowStatus>-1 order by A.DeptID_Users";
                Query3 = " ";
            }
            else if (Status == 9) // Head Kasie MTN ENG
            {
                //Query1 = " where A.RowStatus>-1 order by A.DeptID_Users";
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan," +
                         " (select LEFT(convert(char,lp.CreatedTime,106),11) from WorkOrder_LogApproval as lp where lp.WOID=A.ID and lp.RowStatus>-1 and lp.Urutan=1)createdtime,"+
                         " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO," +
                         " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                         " ,ISNULL(Pelaksana,'')Pelaksana, " +
                         " case " +
                         " when A.apv=0 then 'Open' " +
                         " when A.apv=1 then 'Aproved Mgr' " +
                         " when A.apv=2 then 'Approved PM'" +
                         " when A.apv=3 then 'Approved Mgr MTN'" +
                         " when A.apv=4 then 'Finish'" +
                         " when A.apv=5 then 'Closed'" +
                         " end StatusApv,C.Alias DeptName from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 order by A.DeptID_Users";
                Query3 = "";
            }
            else if (Status == 1 && DeptID== 10) // Approval Manager Logistik BB & BJ Dept
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv=1 then 'Mgr Dept.'" +
                         " when apv=2 then 'PM'" +
                         " when apv=3 then 'Mgr MTN' end StatusApv,DeptName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 and A.DeptID_Users in (10,6) and A.Apv = 0";
                Query3 = " ";
            }
            else if (Status == 1 && DeptID == 2) // Approval Manager Logistik BM & Finishing Dept
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv=1 then 'Mgr Dept.'" +
                         " when apv=2 then 'PM'" +
                         " when apv=3 then 'Mgr MTN' end StatusApv,DeptName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 and A.DeptID_Users in (2,3) and A.Apv = 0";
                Query3 = " ";
            }
            else if (Status == 1) // Approval Manager Dept
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 then  (select wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv=1 then 'Mgr Dept.'" +
                         " when apv=2 then 'PM'" +
                         " when apv=3 then 'Mgr MTN' end StatusApv,DeptName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 and A.DeptID_Users=" + DeptID + " and A.Apv = 0";
                Query3 = " ";
            }
            else
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when A.Apv=0 then  ''" +
                         " when A.Apv=1 then  (select LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when A.Apv=2 then  (select LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when A.Apv=3 then  (select LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when A.apv=0 then 'Open'" +
                         " when A.apv=1 then 'Mgr Dept.'" +
                         " when A.apv=2 then 'PM'" +
                         " when A.apv=3 then 'Mgr MTN' end StatusApv,DeptName" +
                         " from WorkOrder as A  " +
                         " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID ";
                Query2 = " where A.RowStatus>-1 and A.DeptID_Users=" + DeptID + "  order by A.ID asc";
                Query3 = " ";
            }

            string strSQL = " " + Query1 + "" +                                           
                            " " + Query2 + "" +
                            " " + Query3 + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListWO(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectListWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();            
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();   
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();            
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.CreatedTime1 = sqlDataReader["CreatedTime1"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();    

            return objWO;
        }

        public WorkOrder GenerateObjectListWO_1(SqlDataReader sqlDataReader)
        {           
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.SisaHari = sqlDataReader["SisaHari"].ToString();
            objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.DueDateWO = sqlDataReader["DueDateWO"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.ElapsedDay = sqlDataReader["ElapsedDay"].ToString();
            objWO.ElapsedWeek = sqlDataReader["ElapsedWeek"].ToString();
            return objWO;
        }

        public ArrayList RetrieveListLampiran(string WOID)
        {
            string strSQL = " select ID,FileLampiran FileName from WorkOrder_Lampiran where WOID="+WOID+" and rowstatus>-1";
                           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListLampiranWO(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectListLampiranWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();            
            objWO.FileName = sqlDataReader["FileName"].ToString();
            objWO.ID = Convert.ToInt32(sqlDataReader["ID"]);

            return objWO;
        }

        public ArrayList RetrieveListLampiranKosong()
        {
            string strSQL = " select top 1 '?'FileName from WorkOrder_Lampiran";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListLampiranWOKosong(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectListLampiranWOKosong(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();         
            objWO.FileName = sqlDataReader["FileName"].ToString();           
            return objWO;
        }

        public ArrayList RetrieveHeaderDept(string DeptID)
        {
            string strSQL = " select Alias DeptName from dept where ID=" + DeptID + " and rowstatus > -1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectHeaderDept(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectHeaderDept(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            return objWO;
        }

        public ArrayList RetrieveWOall(int StatusApv)
        {
            string strSQL = string.Empty;

            if (StatusApv == 3)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan from WorkOrder as A " +
                         " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=2 and A.RowStatus > -1  and wl.Urutan=1 ";
            }
            else if (StatusApv == 9)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan from WorkOrder as A " +
                         " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=3 and A.RowStatus > -1  and A.DueDateWO is not NULL ";
            }                           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectWOall(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectWOall(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();           
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();

            return objWO;
        }

        public WorkOrder RetrieveWOPerNoWO(string NoWO, int StatApv)
        {
            string strSQL = string.Empty;

            if (StatApv == 3)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan from WorkOrder as A " +
                         " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=2 and A.RowStatus > -1  and wl.Urutan=1 and A.NoWO='" + NoWO + "' ";
            }
            else if (StatApv == 9)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan from WorkOrder as A " +
                         " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=3 and A.RowStatus > -1 and wl.urutan=3  and A.DueDateWO is not NULL and A.NoWO='" + NoWO + "' ";
            }
                                   
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectWOPerNoWO(sqlDataReader);
                }
            }

            return new WorkOrder();
        }

        public WorkOrder GenerateObjectWOPerNoWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();    
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            return objWO;
        }

        public ArrayList Retrieve_ListWO_Lampiran(string NoWO)
        {
            string strSQL = " select B.ID IDlampiran,ISNULL(B.FileLampiran,'')FileName from WorkOrder as A " +
                            " LEFT JOIN WorkOrder_Lampiran as B ON A.ID=B.WOID where A.NoWO='"+NoWO+"' and B.RowStatus > -1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListWO_Lampiran(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectListWO_Lampiran(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.IDLampiran = Convert.ToInt32(sqlDataReader["IDLampiran"]);
            objWO.FileName = sqlDataReader["FileName"].ToString();           
            return objWO;
        }

        public ArrayList RetrieveLampiranWO(string ID)
        {
            string StrSql = " select FileLampiran FileName from WorkOrder_Lampiran where ID="+ID+" and RowStatus > -1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectList_RetrieveLampiranWO(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectList_RetrieveLampiranWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();          
            objWO.FileName = sqlDataReader["FileName"].ToString();
            return objWO;
        }

        public ArrayList RetrieveWO_Finish(string DeptID)
        {
            string strSQL = string.Empty;

         
            strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                     " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                     " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                     " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                     " UraianPekerjaan from WorkOrder as A " +
                     " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                     " where Apv=4 and A.RowStatus > -1 and wl.Urutan=4  and A.DueDateWO is not NULL and A.DeptID_Users=" + DeptID + "";           
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectList_RetrieveWO_Finish(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder());

            return arrWO;
        }

        public WorkOrder GenerateObjectList_RetrieveWO_Finish(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();

            return objWO;
        }

        public WorkOrder RetrieveWOFinishNoWO(string NoWO)
        {
            string strSQL = string.Empty;

            
            strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan from WorkOrder as A " +
                         " LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=4 and A.RowStatus > -1  and wl.Urutan=1 and A.NoWO='" + NoWO + "' ";  
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveWOFinishNoWO(sqlDataReader);
                }
            }

            return new WorkOrder();
        }

        public WorkOrder GenerateObject_RetrieveWOFinishNoWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            return objWO;
        }

        public ArrayList RetrieveNamaDept_1(string status,int DeptID, int Tahun, string Periode, string Flag)      
        {
            string Query1 = string.Empty;
            
            if (Flag == "1" && DeptID == 19
                || Flag == "1" &&  DeptID == 18
                || Flag == "1" && DeptID == 5
                || Flag == "1" && DeptID == 4
                || Flag == "1" && status == "2"
                || Flag == "1" && status == "3"
                || Flag == "1" && status == "9")
            { Query1 = "  "; }

            else if (Flag == "1" && DeptID != 19
                || Flag == "1" && DeptID != 18
                || Flag == "1" && DeptID != 5
                || Flag == "1" && DeptID != 4)
            {
                Query1 = "and A.DeptID_Users=" + DeptID + "";
            }
            else if (Flag == "2" && DeptID == 19
                || Flag == "2" && DeptID == 18
                || Flag == "2" && DeptID == 5
                || Flag == "2" && DeptID == 4
                || Flag == "2" && status == "2"
                || Flag == "2" && status == "3"
                || Flag == "2" && status == "9")
            { Query1 = " and A.DueDateWO is null  and A.Apv>=2 "; }
            else if (Flag == "2" && DeptID != 19
                || Flag == "2" && DeptID != 18
                || Flag == "2" && DeptID != 5
                || Flag == "2" && DeptID != 4)
            {
                Query1 = "and A.DeptID_Users=" + DeptID + "";
            }            

            ArrayList arrData = new ArrayList();
            string strSQL = " select DISTINCT(B.Alias) DeptName from workorder as A LEFT JOIN Dept as B ON A.DeptID_Users=B.ID "+
                            " where A.ID in (select WOID from WorkOrder_LogApproval where MONTH(CreatedTime)="+Periode+" "+
                            " and YEAR(CreatedTime)="+Tahun+" and RowStatus > -1 and urutan=2) and A.RowStatus > -1 "+Query1+" ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder
                    {                       
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_2(string status, int DeptID, int Tahun, string Periode, string Flag)
        {
            string Query1 = string.Empty;

            if (Flag == "1" && DeptID == 19
                || Flag == "1" && DeptID == 18
                || Flag == "1" && DeptID == 5
                || Flag == "1" && DeptID == 4
                || Flag == "1" && status == "2"
                || Flag == "1" && status == "3"
                || Flag == "1" && status == "9")
            { Query1 = "  "; }

            else if (Flag == "1" && DeptID != 19
                || Flag == "1" && DeptID != 18
                || Flag == "1" && DeptID != 5
                || Flag == "1" && DeptID != 4)
            {
                Query1 = "and A.DeptID_Users=" + DeptID + "";
            }
            else if (Flag == "2" && DeptID == 19
                || Flag == "2" && DeptID == 18
                || Flag == "2" && DeptID == 5
                || Flag == "2" && DeptID == 4
                || Flag == "2" && status == "2"
                || Flag == "2" && status == "3"
                || Flag == "2" && status == "9")
            { Query1 = " and YEAR(DueDateWO)=" + Tahun + " and MONTH(DueDateWO)=" + Periode + "   and A.Apv>=3 "; }
            else if (Flag == "2" && DeptID != 19
                || Flag == "2" && DeptID != 18
                || Flag == "2" && DeptID != 5
                || Flag == "2" && DeptID != 4)
            {
                Query1 = "and A.DeptID_Users=" + DeptID + "";
            }

            ArrayList arrData = new ArrayList();
            string strSQL = " select DISTINCT(B.Alias) DeptName from workorder as A LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
                            " where A.ID in (select WOID from WorkOrder_LogApproval where RowStatus > -1 and urutan>=3) "+
                            " and A.RowStatus > -1 " + Query1 + " ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder
                    {
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

       

        public string RetrieveNamaDept(string DeptID)
        {
            string result = "0";
            string StrSql = " select Alias DeptName from dept where rowstatus > -1 and ID=" + DeptID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["DeptName"].ToString();
                }
            }

            return result;
        }

        public ArrayList RetrieveArea(int DeptID)
        {            
            ArrayList arrData = new ArrayList();
            string strSQL = " select ID AreaID,AreaWO from WorkOrder_Area where RowStatus > -1 and DeptID="+DeptID+" ";
                           
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder
                    {
                        AreaID = Convert.ToInt32(sdr["AreaID"].ToString()),
                        AreaWO = sdr["AreaWO"].ToString()
                    });
                }
            }
            return arrData;
        }

        public int RetrieveTotalWO(string Bulan, string Tahun)
        {
            string StrSql = " select COUNT(DueDateWO)TotalWO from WorkOrder where MONTH(DueDateWO)="+Bulan+" and YEAR(DueDateWO)="+Tahun+" and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["TotalWO"]);
                }
            }

            return 0;
        }

        public WorkOrder RetrieveStatusWO(string Bulan, string Tahun)
        {
            string strSQL = string.Empty;


            strSQL = " select ISNULL(Total,0)Total,StatusWO from(select case when StatusWO='Tercapai' then COUNT(StatusWO) end Total,StatusWO from (select StatusWO from (select case when FinishDate<=DueDateWO and " +
                     " Apv=5  then 'Tercapai' else 'Tidak Tercapai' end StatusWO from WorkOrder where MONTH(DueDateWO)=" + Bulan + " and " +
                     " YEAR(DueDateWO)=" + Tahun + " and RowStatus > -1 ) as Data1) as Data2  group by StatusWO ) as Data3";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveStatusWO(sqlDataReader);
                }
            }

            return new WorkOrder();
        }

        public WorkOrder GenerateObject_RetrieveStatusWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder();
            objWO.Total = Convert.ToInt32(sqlDataReader["Total"]);
            objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            
            return objWO;
        }

    }
}
