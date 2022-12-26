using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web;

namespace BusinessFacade
{
    public class WorkOrderFacade_New : AbstractFacade
    {
        private WorkOrder_New objWO = new WorkOrder_New();
        private ArrayList arrWO;
        private List<SqlParameter> sqlListParam;


        public WorkOrderFacade_New()
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoWO", objWO.NoWO));
                sqlListParam.Add(new SqlParameter("@DeptID_Users", objWO.DeptID_Users));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objWO.CreatedBy));
                sqlListParam.Add(new SqlParameter("@UraianPekerjaan", objWO.UraianPekerjaan));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@DeptIDP", objWO.DeptIDP));
                sqlListParam.Add(new SqlParameter("@PlantID", objWO.PlantID));
                sqlListParam.Add(new SqlParameter("@SubArea", objWO.SubArea));
                sqlListParam.Add(new SqlParameter("@Permintaan", objWO.Permintaan));
                sqlListParam.Add(new SqlParameter("@TipeWO", objWO.TipeWO));
                sqlListParam.Add(new SqlParameter("@Pelaksana", objWO.Pelaksana.Trim()));
                sqlListParam.Add(new SqlParameter("@NamaSubDept", objWO.NamaSubDept.Trim()));


                result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_InsertWO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objWO.AlasanCancel));
                //sqlListParam.Add(new SqlParameter("@AlasanNotApvOP", objWO.AlasanNotApvOP));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_CancelWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateNotOP(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objWO.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@AlasanNotApvOP", objWO.AlasanNotApvOP));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@StatusApv", objWO.StatusApv));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_NotApvOPWO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@UserID", objWO.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@StatusApv", objWO.StatusApv));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objWO.UnitKerjaID));
                sqlListParam.Add(new SqlParameter("@DeptID_Users", objWO.DeptID_Users));
                sqlListParam.Add(new SqlParameter("@VerifikasiSec", objWO.VerifikasiSec));

                result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_InsertLogApproval");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                sqlListParam.Add(new SqlParameter("@DeptIDP", objWO.DeptIDP));
                sqlListParam.Add(new SqlParameter("@Flag", objWO.Flag));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_InsertNoWO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NomorUrut", objWO.NomorUrut));
                sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                sqlListParam.Add(new SqlParameter("@DeptIDP", objWO.DeptIDP));
                sqlListParam.Add(new SqlParameter("@Flag", objWO.Flag));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateNoWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
            
        }

        public int  UpdateWO_Closed(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objWO.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@DeptID_Users", objWO.DeptID_Users));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateClosedWO");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public ArrayList WorkOrderApproveLewat(int deptidpenerima)
        {
            int DeptID1 = 0;

            if (deptidpenerima == 4 || deptidpenerima == 5 || deptidpenerima == 18 || deptidpenerima == 18)
            {
                DeptID1 = 19;
            }
            else
            {
                DeptID1 = 0;
            }
            arrWO = new ArrayList();
            string strSql =
                            "with runingTeks as ( " +
                            "select NoWO, UraianPekerjaan, CreatedTime, GETDATE()TglSekarang," +
                            "case when DATEDIFF(DAY, CreatedTime, GETDATE()) >= 3 then 'Lewat 3 hari' Else 'WO Perlu di Approve' end Keterangan " +
                            "from WorkOrder where " +
                            "DeptID_PenerimaWO = "+DeptID1+" " +
                            "and RowStatus > -1 " +
                            "and Apv = 2 " +
                            "and DueDateWO is null " +
                            "and Pelaksana = '' " +
                            "and UpdatePelaksanaTime is null " +
                            "and UpdateTargetTime is null) " +
                            "select * from runingTeks ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrWO.Add(new WorkOrder
                    {
                        NoWO = sdr["NoWO"].ToString(),
                        UraianPekerjaan = sdr["UraianPekerjaan"].ToString(),
                        Keterangan = sdr["Keterangan"].ToString()
                    });
                }
            }
            return arrWO;
        }

        public int CancelWO_Lampiran(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objWO.ID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWO_Lampiran");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objWO.ID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objWO.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_Cancel_WO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));                
                sqlListParam.Add(new SqlParameter("@StatusApv", objWO.StatusApv));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));
                sqlListParam.Add(new SqlParameter("@DeptIDHead", objWO.DeptIDHead));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@PlantID", objWO.PlantID));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objWO.UnitKerjaID));
                sqlListParam.Add(new SqlParameter("@DeptID_Users", objWO.DeptID_Users));
               
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateApvWO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                //sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                //sqlListParam.Add(new SqlParameter("@Tahun", objWO.Tahun));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateDueDateWO");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@Pelaksana", objWO.Pelaksana));
                sqlListParam.Add(new SqlParameter("@DueDateWO", objWO.DueDateWO));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));
                sqlListParam.Add(new SqlParameter("@FinishDate", objWO.FinishDate));
                sqlListParam.Add(new SqlParameter("@UraianPerbaikan", objWO.UraianPerbaikan));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWOL3");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv_L3HRD(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWOL3HRD");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_Apv_L3IT(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@Pelaksana", objWO.Pelaksana));
                sqlListParam.Add(new SqlParameter("@DueDateWO", objWO.DueDateWO));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWOL3IT");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Apv", objWO.Apv));
                sqlListParam.Add(new SqlParameter("@FinishDate", objWO.FinishDate));
                sqlListParam.Add(new SqlParameter("@UraianPerbaikan", objWO.UraianPerbaikan));
               
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWOL4");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdatePelaksana(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@Pelaksana", objWO.Pelaksana));
                

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdatePelaksana");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateTarget(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@DueDateWO", objWO.DueDateWO));


                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateTarget");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateFinish(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@FinishDate", objWO.FinishDate));


                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateFinish");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                //sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                //sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_UpdateWOL5");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateWO_ApvNaikTarget(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));

                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_TargetApv");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int CancelWO_T3(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@WoID", objWO.ID));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objWO.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_Cancel_WOT3");
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
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@FileName", objWO.FileName));                
                sqlListParam.Add(new SqlParameter("@CreatedBy", objWO.CreatedBy));
                sqlListParam.Add(new SqlParameter("@FileLampiranOP", objWO.FileLampiranOP));
                sqlListParam.Add(new SqlParameter("@PlantID", objWO.PlantID));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));

                int intResult = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_InsertLampiranWO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertClosedOto(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@WOID", objWO.WOID));
                sqlListParam.Add(new SqlParameter("@AreaWO", objWO.AreaWO));
                sqlListParam.Add(new SqlParameter("@ToDept", objWO.ToDept));

                int intResult = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_OtoCLose");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertTarget(object objDomain)
        {
            try
            {
                objWO = (WorkOrder_New)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoWO", objWO.NoWO));
                sqlListParam.Add(new SqlParameter("@TargetT1", objWO.TargetT1));
                sqlListParam.Add(new SqlParameter("@TargetT2", objWO.TargetT2));
                sqlListParam.Add(new SqlParameter("@UserName", objWO.UserName));
                sqlListParam.Add(new SqlParameter("@Target", objWO.Target));
                //sqlListParam.Add(new SqlParameter("@DeptIDP", objWO.DeptIDP));
                //sqlListParam.Add(new SqlParameter("@Flag", objWO.Flag));
                int result = dataAccess.ProcessData(sqlListParam, "WorkOrderNew_SP_NaikTarget");
                strError = dataAccess.Error;
                return result;
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
            //string StrSql = " select Status StatusApv from WorkOrder_ListApv where RowStatus > -1 and UserID="+UserID+" ";
            //string StrSql = " select ISNULL(StatusApv,0)StatusApv from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID=" + UserID + " ";
            string StrSql = " select SUM(StatusApv)StatusApv from (select '0'StatusApv from WorkOrder_ListApvUpdate union all " +
                            " select StatusApv from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID=" + UserID + " ) as Data1 ";
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

        public int Retrieve_Dept(int UserID)
        {
            string StrSql = " select SUM(DeptID)DeptID from (select '0'DeptID from WorkOrder_ListApvUpdate union all " +
                            " select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID=" + UserID + " ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["DeptID"]);
                }
            }

            return 0;
        }

        public int RetrieveUserDept(int DeptID)
        {
            //string StrSql = " select Status StatusApv from WorkOrder_ListApv where RowStatus > -1 and UserID="+UserID+" ";
            //string StrSql = " select ISNULL(StatusApv,0)StatusApv from WorkOrder_ListApvUpdate where RowStatus > -1 and UserID=" + UserID + " ";
            string StrSql = " select top 1 DeptID from WorkOrder_Dept where DeptID="+DeptID+" and rowstatus > -1 ";
                            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["DeptID"]);
                }
            }

            return 0;
        }

        public int RetrieveUserLevel1(int UserID)
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


        public int RetrieveNoWO(string Bulan, string Tahun, int DeptID, string Flag)
        {
            string StrSql = " select NoUrut from WorkOrder_Nomor where Bulan=" + Bulan + " and  Tahun=" + Tahun + " and DeptID=" + DeptID + " and Flag='"+Flag+"'";                            
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

        public ArrayList RetrieveListWO_0(string NamaSub ,string PenerimaWO, string UsersWO, string Periode, int Tahun, string Tanda, string WaktuSkr, int UnitKerjaID)
        {
            string Query = string.Empty; string QuerySelisihHK = string.Empty; string QueryApvMgr = string.Empty;
            string SubQuery = string.Empty;
            string SubQuery2 = string.Empty;
            string Pelaksana = "Pelaksana";
            Users users = (Users)HttpContext.Current.Session["Users"];

            #region Logika I
            if (Tanda == "5" || Tanda == "51" || Tanda == "41")
            {
                #region Penerima IT
                if (PenerimaWO == "IT")
                {
                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                            ") as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and "+
                            " ((AreaWO='SoftWare' and VerISO=1 and ApvOP in (2,-2) and Apv>=2)or(AreaWO='HardWare' and Apv>=2)) or "+

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1 " +

                            " ) as DataWO2) "+

                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                    //" as DataWO3) as DataWO4 where (Approval=-1) or (Approval>0 and Aktif=1) order by WOID desc ";

                    SubQuery2 =
                            " case when A.DeptID_PenerimaWo=14 and A.AreaWO='SoftWare' and A.Apv>=2 and A.VerISO=1  and A.ApvOP in (2,-2) then  "+
                            " (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) from WorkOrder_LogApproval as lp where lp.WOID=A.ID   "+
                            " and lp.RowStatus>-1 and lp.Urutan=3) "+ 
                            " when A.DeptID_PenerimaWo=14 and A.AreaWO='HardWare' and A.Apv>=2 then (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) "+
                            " from WorkOrder_LogApproval as lp  where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) "+ 
                            " else (select top 1 LEFT(convert(char,lp.CreatedTime,106),11)  from WorkOrder_LogApproval as lp "+
                            " where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) end createdtime,  ";

                    QueryApvMgr = " case "+
                                  " when A.AreaWO='SoftWare' then (A.DateApvOP) "+
                                  " when A.AreaWO='HardWare' then (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x "+
                                  " where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end ApvMgr, ";
                    
                    QuerySelisihHK = " case " +
                                     " when A.AreaWO='Software' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=4 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") " +
                                     " when  A.AreaWO='HardWare'  then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=3 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end Waktu2,  " +
                                     " case when A.AreaWO='Software' then A.DateApvOP when A.AreaWO='HardWare' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")  end Waktu1 ";
                }
                #endregion

                #region Penerima HRDGA
                else if (PenerimaWO == "HRD & GA")
                {
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";

                    if (PenerimaWO == "HRD & GA" && NamaSub != string.Empty)
                    {
                        Query =
                                " where "+
                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) " +
                                " and A.NamaSubDept='" + NamaSub.Replace("HRD - ", "") + "')" +

                                " or "+

                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) " +
                                " and A.NamaSubDept='" + NamaSub.Replace("HRD - ", "") + "')" +

                                " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and Apv>=2  or " +

                                " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                " ) as DataWO2) " +
                                " as DataWO3) as DataWO4 "+
                                " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                        SubQuery2 =
                                " case when A.DeptID_PenerimaWo=7  then  (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) " +
                                " from WorkOrder_LogApproval as lp where lp.WOID=A.ID   and lp.RowStatus>-1 and lp.Urutan=2) " +
                                " else (select top 1 LEFT(convert(char,lp.CreatedTime,106),11)  from WorkOrder_LogApproval as lp " +
                                " where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) end createdtime, ";
                    }
               
                    else if (PenerimaWO == "HRD & GA" && NamaSub == "")
                    {
                        Query =
                                   " where "+
                                   " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                   " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                   " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                   " or "+

                                   " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                   " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                   " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                   " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and Apv>=2 or " +

                                   " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                   " ) as DataWO2) " +
                                   " as DataWO3) as DataWO4 "+
                                   " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                        SubQuery2 =
                                " case when A.DeptID_PenerimaWo=7  then  (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) " +
                                " from WorkOrder_LogApproval as lp where lp.WOID=A.ID   and lp.RowStatus>-1 and lp.Urutan=2) " +
                                " else (select top 1 LEFT(convert(char,lp.CreatedTime,106),11)  from WorkOrder_LogApproval as lp " +
                                " where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) end createdtime, ";
                    }
                    else
                    {
                        Query =
                                       " where "+
                                       " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                       " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                       " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                       " or "+

                                       " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                       " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                       " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                       " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and Apv>=2  or " +

                                       " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                       " ) as DataWO2) " +
                                       " as DataWO3) as DataWO4 "+
                                       " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                        SubQuery2 =
                                " case when A.DeptID_PenerimaWo=7  then  (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) " +
                                " from WorkOrder_LogApproval as lp where lp.WOID=A.ID   and lp.RowStatus>-1 and lp.Urutan=2) " +
                                " else (select top 1 LEFT(convert(char,lp.CreatedTime,106),11)  from WorkOrder_LogApproval as lp " +
                                " where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) end createdtime, ";
                    }

                }
                #endregion
                #region Penerima MTC
                else if (PenerimaWO == "MAINTENANCE" || PenerimaWO == "MANAGER")
                {
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";

                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +
                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                            ///** Tambahan **/
                            //" or (D.WoID in (select ID from WorkOrder where DeptID_Users in (select ID from dept where alias='" + UsersWO + "' " +
                            //" and rowstatus > -1)and RowStatus>-1) and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)) " +
                            

                            " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " and " +
                            " ((AreaWO='Kendaraan' and VerSec=1 and Apv>=2) or (AreaWO <>'Kendaraan' and Apv>=2))  or " +

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +
                            
                            " ) as DataWO2) " +
                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                    SubQuery2 =
                            " case when A.DeptID_PenerimaWo=19 and A.AreaWO='Kendaraan' and A.VerSec=1  then  (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) "+
                            " from WorkOrder_LogApproval as lp where lp.WOID=A.ID   and lp.RowStatus>-1 and lp.Urutan=2) "+ 
                            " when A.DeptID_PenerimaWo=19 and A.AreaWO <>'Kendaraan' and A.Apv>=2 then (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) "+
                            " from WorkOrder_LogApproval as lp  where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) "+ 
                            " else (select top 1 LEFT(convert(char,lp.CreatedTime,106),11)  from WorkOrder_LogApproval as lp "+
                            " where lp.WOID=A.ID  and lp.RowStatus>-1 and lp.Urutan=2) end createdtime, ";
                }
                #endregion
            }
            #endregion

            #region Logika II
            else if (Tanda == "4")
            {
                #region Penerima HRDGA
                if (PenerimaWO == "HRD & GA" && NamaSub != string.Empty && NamaSub != "Ext")
                {
                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept in (select NamaSubDept from WorkOrder_SubDept where UserID=" + users.ID + " )) " +

                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept in (select NamaSubDept from WorkOrder_SubDept where UserID=" + users.ID + " )) " +

                            " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + "  or "+

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                            " ) as DataWO2) " +
                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 "; 
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                else if (PenerimaWO == "HRD & GA" && NamaSub == "Ext" )
                {
                    Query =
                                " where "+
                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1) and A.NamaSubDept<>'' )" +

                                " or "+

                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1) and A.NamaSubDept<>'' )" +

                                " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " or "+

                                " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                " ) as DataWO2) " +
                                " as DataWO3) as DataWO4 "+
                                " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                #endregion
                #region Dept Lainnya
                else
                {
                    if (PenerimaWO == "MAINTENANCE" || PenerimaWO == "MANAGER")
                    {
                        QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                        QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                    }
                    else
                    {
                        QuerySelisihHK = " case " +
                                         " when A.AreaWO='Software' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=4 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") " +
                                         " when  A.AreaWO='HardWare'  then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=3 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end Waktu2,  " +
                                         " case when A.AreaWO='Software' then A.DateApvOP when A.AreaWO='HardWare' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")  end Waktu1 ";
                        QueryApvMgr = " case " +
                                      " when A.AreaWO='SoftWare' then (A.DateApvOP) " +
                                      " when A.AreaWO='HardWare' then (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x " +
                                      " where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end ApvMgr, ";
                    }
                
                    Query =
                                    " where " +
                                    " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                    " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                    " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                    " or " +

                                    " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                    " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                    " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                    " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " or " +

                                    " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                    " ) as DataWO2) " +
                                    " as DataWO3) as DataWO4 "+
                                    " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                    SubQuery2 =     " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";                    
                }
                #endregion

            }
            #endregion

            #region Logika III : ISO           
            else if (Tanda == "3")
            {
                #region Penerima HRDGA
                if (PenerimaWO == "HRD & GA" && NamaSub != string.Empty)
                {
                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept='" + NamaSub.Replace("HRD - ", "") + "')" +

                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept='" + NamaSub.Replace("HRD - ", "") + "')" +

                            " ) as DataWO where MONTH(DueDateWO)=" + Periode + " and YEAR(DueDateWO)=" + Tahun + " or "+

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                            " ) as DataWO2) " +
                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                else if (PenerimaWO == "HRD & GA" && NamaSub == "")
                {
                    Query =
                               " where "+
                               " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                               " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                               " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                               " or "+

                               " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                               " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                               " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1))  or " +

                               " (D.WoID in (select ID from WorkOrder where DeptID_Users in (select ID from dept where alias='" + UsersWO + "'  "+
                               " and rowstatus > -1)and RowStatus>-1) and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "'  )) " +

                               " ) "+

                               " as DataWO where (MONTH(DueDateWO)=" + Periode + " and YEAR(DueDateWO)=" + Tahun + " ) or "+

                               " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  )  " +

                               " as DataWO2) " +
                               " as DataWO3) as DataWO4 "+
                               " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                #endregion
                #region Dept Lainnya
                else
                {
                    if (PenerimaWO == "MAINTENANCE" || PenerimaWO == "MANAGER")
                    {
                        QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                        QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                    }
                    else
                    {
                        QuerySelisihHK = " case " +
                                         " when A.AreaWO='Software' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=4 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") " +
                                         " when  A.AreaWO='HardWare'  then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=3 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end Waktu2,  " +
                                         " case when A.AreaWO='Software' then A.DateApvOP when A.AreaWO='HardWare' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")  end Waktu1 ";
                        QueryApvMgr = " case " +
                                      " when A.AreaWO='SoftWare' then (A.DateApvOP) " +
                                      " when A.AreaWO='HardWare' then (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x " +
                                      " where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end ApvMgr, ";
                    }
                    
                    Query =
                                 " where " +
                                 " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                 " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                 " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                 " or " +

                                 " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                 " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                 " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +
                                 " or (D.WoID in (select ID from WorkOrder where DeptID_Users in (select ID from dept where alias='" + UsersWO + "' " +
                                 " and rowstatus > -1)and RowStatus>-1) and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' " +
                                 " )) " +

                                 " ) as DataWO where (MONTH(DueDateWO)=" + Periode + " and YEAR(DueDateWO)=" + Tahun + ") " +
                                 " or (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  or" +

                                 " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                 " ) as DataWO2) " +
                                 " as DataWO3) as DataWO4 "+
                                 " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";

                    SubQuery2 =  " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";

                }
                #endregion
            }
            #endregion

            #region Logika IV
            else if (Tanda == "2")
            {
                #region HRDGA
                if (PenerimaWO == "HRD & GA" && NamaSub != string.Empty)
                {
                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept='" + NamaSub.Replace("HRD - ","") + "')" +

                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1) and A.NamaSubDept='" + NamaSub.Replace("HRD - ", "") + "')" +

                            " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " or "+

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                            " ) as DataWO2) " +
                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                  
                    
                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                else if (PenerimaWO == "HRD & GA" && NamaSub == "")
                {
                    Query =
                            " where "+
                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1))" +

                            " or "+

                            " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                            " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                            " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1))" +


                            " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " or "+

                            " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +
                            
                            " ) as DataWO2) " +
                            " as DataWO3) as DataWO4 "+
                            " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                    SubQuery2 =
                            " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";
                    QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                    QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                }
                #endregion
                #region Dept Lainnya
                else
                {
                    if (PenerimaWO == "MAINTENANCE" || PenerimaWO == "MANAGER")
                    {
                        QuerySelisihHK = "  (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")Waktu1, A.UpdateTargetTime Waktu2 ";
                        QueryApvMgr = " (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")ApvMgr, ";
                    }
                    else
                    {
                        QuerySelisihHK = " case " +
                                         " when A.AreaWO='Software' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=4 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") " +
                                         " when  A.AreaWO='HardWare'  then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=3 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end Waktu2,  " +
                                         " case when A.AreaWO='Software' then A.DateApvOP when A.AreaWO='HardWare' then (select x.CreatedTime from WorkOrder_LogApproval x where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ")  end Waktu1 ";

                        QueryApvMgr = " case " +
                                      " when A.AreaWO='SoftWare' then (A.DateApvOP) " +
                                      " when A.AreaWO='HardWare' then (select LEFT(convert(char,x.CreatedTime,106),11) from WorkOrder_LogApproval x " +
                                      " where x.WOID=A.ID and x.Urutan=1 and x.RowStatus>-1 and A.RowStatus>-1 and MONTH(A.DueDateWO)=" + Periode + " and YEAR(A.DueDateWO)=" + Tahun + ") end ApvMgr, ";
                    }
                    
                    Query =
                                " where " +
                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.WoID is null " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +

                                " or " +

                                " (A.RowStatus>-1 and A.PlantID=" + UnitKerjaID + " and D.Aktif=1 " +
                                " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + PenerimaWO + "' and rowstatus > -1)" +
                                " and A.DeptID_Users in (select ID from dept where alias='" + UsersWO + "' and rowstatus > -1)) " +


                                " ) as DataWO where MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + " or " +

                                " (MONTH(DataWO.TglTarget)=" + Periode + " and YEAR(DataWO.TglTarget)=" + Tahun + ") and DataWO.RowStatus>-1  " +

                                " ) as DataWO2 ) " +
                                " as DataWO3) as DataWO4 "+
                                " where (StsTarget=1 and Aktif=1) or (StsTarget is null and Aktif is null) or (StsTarget>1 and Aktif >=0) order by WOID desc";
                    SubQuery2 = " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime, ";                                          
                }
                #endregion
            }
            #endregion

            #region Query-nya
            string strSQL = " select case " +                            
                            " when DeptID_PenerimaWO=14 and Apv=1 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) then 'Next: PM' " +               
                            " when Apv=0 and DeptID_PenerimaWO=7 and DeptID_Users=7 then 'Next: Head'+' - '+NamaSubDept "+
                            " when Apv=0 then 'Next: Mgr Dept' " +
                            " when Cancel = 1 then 'Cancel - T3' " +                          
                            " when (StsTarget is null or StsTarget =1 ) and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat'  "+
                            " when  StsTarget =2  and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat - T2' "+
                            " when  StsTarget =3  and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat - T3' "+
                            " when FinishDate <> '' and FinishDate<=DueDateWO and Apv=4 then 'Finish'  " +
                            " when FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=4 then 'Finish'  " +                                                       
                            " when (StsTarget is null or StsTarget =1 ) and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo'  "+
                            " when StsTarget =2 and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T2' "+
                            " when StsTarget =3 and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T3' "+
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=7 and Pelaksana<>'' and DueDateWO<>''  then 'Next: Mgr HRD' " +                           
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=0 and AreaWO='Software'  then 'Next: Verifikasi ISO' " +
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO='Software'  then 'Next: Mgr IT' " +
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO='Software'  then 'Next: Mgr IT' " +
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=0 and AreaWO='Software'  then 'Next: Mgr Dept - Plant Terkait' " +
                            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=0 and AreaWO='Software' then 'Next: Verifikasi ISO' " +                           
                            " when Pelaksana <> '' and DueDateWO =''   and DeptID_PenerimaWO=19  then 'Next:' + "+Pelaksana+" " +
                            " when Pelaksana <> '' and DueDateWO <>''   and DeptID_PenerimaWO=19  and Apv=2  then 'Next: Mgr MTN' " +
                            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=19  then 'Next: Mgr MTN' "+  
                            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=14  and Apv=2 and AreaWO='HardWare'  then 'Next: Mgr IT' "+
                            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=7  and Apv=2 and VerSec=1  then 'Next: Mgr HRD' "+
                            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=7  and Apv=2 and AreaWO<>'Kendaraan'  then 'Next: Mgr HRD' "+
                            " when Pelaksana <> '' and DueDateWO <>''   and DeptID_PenerimaWO=7  and Apv=2  then 'Next: Mgr HRD' "+     
                            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO='' then 'Next : Head GA' "+
                            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO<>'' then 'Next: Mgr HRD' "+
                            " when StsTarget =3 and  FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T3' "+
                            " when StsTarget =2 and  FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T2' "+
                            " when (StsTarget is null or StsTarget =1 ) and FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai' "+
                            " when StsTarget=3 and WaktuNow<WaktuDateLine then 'Progress - T3' " +
                            " when StsTarget=2 and WaktuNow<WaktuDateLine then 'Progress - T2' " +
                            " when StsTarget=1 and WaktuNow<WaktuDateLine then 'Progress - T1'  "+
                            " when StsTarget is null then 'Progress' " +
                            " when StsTarget=3 and WaktuNow>WaktuDateLine then 'Lewat - T3' " +
                            " when StsTarget=2 and WaktuNow>WaktuDateLine then 'Lewat - T2' " +
                            " when StsTarget=1 and WaktuNow>WaktuDateLine then 'Lewat - T1' " +
                            " when StsTarget is null then 'Lewat' " +                         
                            " end 'StatusWO', "+  
                            "  case  "+
                            " when StsTarget is null and   YEAR(WaktuSelesai)>1900   then 0   " +
                            " when StsTarget is null and YEAR(WaktuSelesai)=1900 then ElapsedDay   " +
                            " when StsTarget is null and WaktuSelesai = '' then 0 " +
                            //" when StsTarget=1 and FinishDate>1900 then 0 "+
                            " when StsTarget=1 and MONTH(WaktuSelesai)>1900 then 0 " +
                            " when (StsTarget=1 or StsTarget=2 or StsTarget=3) and WaktuSelesai <> '' then 0 " +
                            " when (StsTarget=1 or StsTarget=2 or StsTarget=3) and WaktuSelesai = '' then ElapsedDay " +
                            " end SisaHari, "+

                            " DueDateWO DueDateWO2, "+
                            " case when YEAR(FinishDate)=1900 then '' when FinishDate = '' then '' else FinishDate end FinishDate2 " +                         
                            ",case when SubArea='' then AreaWO when SubArea <>'' then AreaWO+' '+'-'+' '+SubArea end SubArea2,* "+                            
                            "from  (" +
                            " select WOID,NoWO,UraianPekerjaan, "+     
                            " case "+
                            " when StsTarget is null or StsTarget = 1 then LEFT(convert(char,CreatedTime,106),11)  " +
                            " when StsTarget = 2 then LEFT(convert(char,CreatedTimeT2,106),11) " +
                            " when StsTarget = 3 then LEFT(convert(char,CreatedTimeT3,106),11) end createdtime,ApvMgr, " + 
                            " case  "+
                            " when DueDateWO is null then ''  "+
                            " when DueDateWO = '' then '' else DueDateWO end DueDateWO "+
                            ",AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser," +
                            " ElapsedWeek, " +
                            " ElapsedDay,WaktuNow,WaktuDateLine,apv,ISNULL(WaktuSelesai,'')WaktuSelesai,AliasMtn,DeptID_Users,DeptID_PenerimaWO, "+
                            " ISNULL(VerSec,'')VerSec,ISNULL(VerISO,'')VerISO,ISNULL(ApvOP,'')ApvOP,CreatedBy,SubArea,NamaSubDept,Approval,Aktif,Cancel, "+
                            " TglTarget,StsTarget, " +
                            " Waktu1,Waktu2,case when selisih1='0 HK' then '0' else selisih1 end selisih  "+                            
                            " from" +
                            " (select WOID,NoWO,UraianPekerjaan,createdtime,ApvMgr, " +                            
                            " DueDateWO " +
                            " ,AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser,ISNULL(ElapsedDay,0)ElapsedDay," +
                            " ISNULL(ElapsedWeek,0)ElapsedWeek,apv,WaktuNow ,WaktuDateLine,WaktuSelesai,DeptID_Users,DeptID_PenerimaWO,case when DeptID_Users=5 then 'Elk' when DeptID_Users=4 then 'Mkn' when DeptID_Users=18 then 'Uti' else '' end AliasMtn,VerSec,VerISO,ApvOP,CreatedBy,SubArea,NamaSubDept,Approval,Aktif,Cancel "+
                            " ,  case when StsTarget is null then TglTarget "+
                            " when StsTarget = 1 then TglTargetT1 "+
                            " when StsTarget = 2 then TglTargetT2  "+
                            " when StsTarget = 3 then TglTargetT3 end TglTarget,CreatedTimeT2,CreatedTimeT3,StsTarget, " +
                            " convert(varchar,(select SUM(Selisih)Selisih from  (  select datediff(dd, Waktu1, Waktu2) - case when DATEPART(dw,Waktu2)=7 "+
                            " then 1 when DATEPART(dw,Waktu2)=1 then 1 else 0  end  - (datediff(wk, Waktu1, Waktu2) * 2) -  "+
                            " case when datepart(dw, Waktu1) = 1 then 1 else 0 end +  case when datepart(dw, Waktu2) = 1  then 1 else 0 end Selisih  "+
                            " union all "+
                            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=Waktu1 and "+
                            " LEFT(convert(char,harilibur,112),8)<=Waktu2) as selisih))+' '+'HK' selisih1,LEFT(convert(char,Waktu1,106),11)Waktu1, "+
                            " LEFT(convert(char,Waktu2,106),11)Waktu2  "+ 
                            " from( " +
                            " select WOID,NoWO,UraianPekerjaan,createdtime,ApvMgr, " +                           
                            " case when StsTarget is null then DueDateWO "+
                            " when StsTarget = 1 then LEFT(convert(char,TglTargetT1,106),11) "+
                            " when StsTarget = 2 then LEFT(convert(char,TglTargetT2,106),11) "+
                            " when StsTarget = 3 then LEFT(convert(char,TglTargetT3,106),11) end DueDateWO, "+
                            " AreaWO,FinishDate,Pelaksana,StatusApv,DeptName,ApvMgrUser,apv," +                           
                            " case "+
                            " when StsTarget is null and Apv>=2 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) <0 then 0 " +
                            " when StsTarget is null and Apv>=2 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) >= 0  then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) " +
                            " when StsTarget = 1 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1) <0 then 0 " +
                            " when StsTarget = 1 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1) >=0 then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1) " +
                            " when StsTarget = 2 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) <0 then 0 " +
                            " when StsTarget = 2 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) >=0 then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) " +
                            " when StsTarget = 3 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) <0 then 0 " +
                            " when StsTarget = 3 and DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) >=0 then DATEDIFF(week,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) " +                            
                            " end ElapsedWeek, " +                            
                            " case "+
                            " when StsTarget is null and Apv>=2 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) <0 then 0 " +
                            " when StsTarget is null and Apv>=2 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO)  >= 0 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),DueDateWO) " +
                            " when StsTarget = 1 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1)  <0 then 0 " +
                            " when StsTarget = 1 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1) >= 0 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT1) " +
                            " when StsTarget = 2 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) <0 then 0 " +
                            " when StsTarget = 2 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) >= 0 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT2) " +
                            " when StsTarget = 3 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) <0 then 0 "+
                            " when StsTarget = 3 and DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) >=0 then DATEDIFF(DAY,LEFT(CONVERT(CHAR,'" + WaktuSkr + "',112),8),TglTargetT3) " +
                            " end ElapsedDay,  " + 
                            " LEFT(convert(char,'" + WaktuSkr + "',112),8) WaktuNow,   "+                           
                            "  case when StsTarget is null then LEFT(convert(char,DueDateWO2,112),8) "+
                            " when StsTarget = 1 then LEFT(convert(char,TglTargetT1,112),8) "+
                            " when StsTarget = 2 then LEFT(convert(char,TglTargetT2,112),8) "+
                            " when StsTarget = 3 then LEFT(convert(char,TglTargetT3,112),8) end WaktuDateLine, "+
                            " LEFT(convert(char,FinishDate2,112),8)WaktuSelesai,DeptID_Users,DeptID_PenerimaWO,VerSec,VerISO,ApvOP,CreatedBy,SubArea,ISNULL(NamaSubDept,'')NamaSubDept,Approval,Aktif,Cancel,TglTarget,CreatedTimeT2,CreatedTimeT3,TglTargetT1,TglTargetT2,TglTargetT3,StsTarget, " +
                            " Waktu1,Waktu2  "+                            
                            " from (select A.ID WOID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO," +     
                            " " + SubQuery2 + " "+
                            " " + QueryApvMgr + " " +                          
                            " case when D.ID>0 then (select top 1 LEFT(convert(char,AA.TglTarget,106),11) from WorkOrder_Target AA " +
                            " where AA.WoID=A.ID and AA.RowStatus>-1 order by AA.iD asc) else LEFT(convert(char,A.DueDateWO,106),11) end DueDateWO, " + 
                            " A.DueDateWO DueDateWO2,AreaWO," +
                            " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate,A.FinishDate FinishDate2, " +
                            " ISNULL(Pelaksana,'')Pelaksana, " +
                            " case " +
                            " when A.apv=0 then 'Open' " +  
                            " when A.Apv=2 and DeptID_PenerimaWO=19 and A.Pelaksana='' then 'Apv Mgr' "+
                            " when A.Apv=2 and ((A.Pelaksana is not null or A.Pelaksana <> '') and (A.DueDateWO is null or A.DueDateWO='')) and A.DeptID_PenerimaWO=19 then 'Apv Mgr MTN-1' "+
                            " when A.Apv=2 and ((A.Pelaksana is not null or A.Pelaksana <> '') and (A.DueDateWO is not null or A.DueDateWO<>'')) and A.DeptID_PenerimaWO=19 then 'Apv Head'+'-'+A.Pelaksana "+
                            " when A.Apv=2 and A.DeptID_PenerimaWO=7 and A.DeptID_Users<>7 and A.Pelaksana='SPV GA' and A.DueDateWO is null then 'Apv Mgr'  " +
                            " when A.Apv=2 and A.DeptID_PenerimaWO=7 and A.Pelaksana='SPV GA' and A.DueDateWO is not null then 'Apv Head GA' "+
                            " when A.Apv=2 and A.DeptID_PenerimaWO=7 and A.DeptID_Users=7 then 'Apv Head '+A.NamaSubDept "+
                            " when A.Apv=2 then 'Apv Mgr' "+
                            " when A.apv=3 and A.DeptID_PenerimaWO=19 then 'Apv Mgr MTN' " +
                            " when A.apv=3 and A.DeptID_PenerimaWO=7 then 'Apv Mgr HRD'  " +
                            " when A.apv=3 and A.DeptID_PenerimaWO=14 then 'Apv Mgr IT' " +
                            " when A.apv=4 then 'Finish'" +
                            " when A.apv=5 then 'Closed'" + 
                            " end StatusApv, "+
                            " C.Alias DeptName,(select top 1 wp.CreatedTime from WorkOrder_LogApproval wp where wp.WOID=A.ID and RowStatus > -1 and wp.Urutan=1)ApvMgrUser, "+
                            " apv,A.VerSec,A.VerISO,A.ApvOP,A.CreatedBy,ISNULL(SubArea,'')SubArea,NamaSubDept,isnull(D.Approval,-1)Approval,Aktif,A.Cancel ,A.StatusTarget,D.TglTarget,D.RowStatus,  "+  
                            " (select A1.Target from WorkOrder_Target A1 where A1.WoID=A.ID and A1.RowStatus>-1 and RowStatus>-1 and "+
                            " MONTH(A1.TglTarget)='" + Periode + "' and YEAR(A1.TglTarget)='" + Tahun + "' and A1.Aktif=1)StsTarget, " +
                            " (select A3.CreatedTime from WorkOrder_Target A3 where A3.WoID=A.ID and A3.RowStatus>-1 and target=2 and RowStatus>-1 and MONTH(A3.TglTarget)='" + Periode + "' and YEAR(A3.TglTarget)='" + Tahun + "')CreatedTimeT2,  " +
                            " (select A3.CreatedTime from WorkOrder_Target A3 where A3.WoID=A.ID and A3.RowStatus>-1 and target=3 and RowStatus>-1 and MONTH(A3.TglTarget)='" + Periode + "' and YEAR(A3.TglTarget)='" + Tahun + "')CreatedTimeT3,  " +
                            " (select A3.TglTarget from WorkOrder_Target A3 where A3.WoID=A.ID and A3.RowStatus>-1 and target=1 and RowStatus>-1 and MONTH(A3.TglTarget)='" + Periode + "' and YEAR(A3.TglTarget)='" + Tahun + "')TglTargetT1, " +
                            " (select A3.TglTarget from WorkOrder_Target A3 where A3.WoID=A.ID and A3.RowStatus>-1 and target=2 and RowStatus>-1 and MONTH(A3.TglTarget)='" + Periode + "' and YEAR(A3.TglTarget)='" + Tahun + "')TglTargetT2, " +
                            " (select A3.TglTarget from WorkOrder_Target A3 where A3.WoID=A.ID and A3.RowStatus>-1 and target=3 and RowStatus>-1 and MONTH(A3.TglTarget)='" + Periode + "' and YEAR(A3.TglTarget)='" + Tahun + "')TglTargetT3, " +
                            " " + QuerySelisihHK + ""+
                            " from WorkOrder as A " +
                            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                            " LEFT JOIN WorkOrder_Target D ON D.WoID=A.ID " +
                            " "+Query+"";
            #endregion

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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public ArrayList RetrieveListWO_1(string NamaSub, string PenerimaWO, string UsersWO, string Periode, int Tahun, string Tanda, string WaktuSkr, int UnitKerjaID)
        {
            string Query = string.Empty; string QuerySelisihHK = string.Empty; string QueryApvMgr = string.Empty;
            string SubQuery = string.Empty; string Logika = string.Empty;
            string SubQuery2 = string.Empty;
            //string Pelaksana = "Pelaksana";
            Users users = (Users)HttpContext.Current.Session["Users"];

            if (Tanda == "2")
            {
                Logika = " where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Periode + "' and UsersWO='" + UsersWO + "' and PenerimaWO='" + PenerimaWO + "' and DueDateWO is null order by NoWO,Target ";
            }
            else if (Tanda == "3")
            {
                Logika = " where YEAR(DueDateWO)='" + Tahun + "' and MONTH(DueDateWO)='" + Periode + "' and UsersWO='" + UsersWO + "' and PenerimaWO='" + PenerimaWO + "'  order by NoWO,Target ";
            }

            #region Query-nya
            string strSQL =
            " /** Break 7 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser ApvMgr,UpdateTargetTime Waktu2,DueDateWO,FinishDate FinishDate2,Pelaksana,CreatedBy,selisih_apv Selisih,case when SisaHari=0 then 0 else SisaHari end SisaHari,StatusWO,StatusApv,UsersWO,PenerimaWO,AreaWO SubArea2,AreaWO from ( " +
            " /** Break 6 **/ " +
            " select woID,StatusWO,StatusApv,NoWO,UraianPekerjaan,UsersWO,PenerimaWO,AreaWO,DeptID_Users,DeptID_PenerimaWO,Target, "+
            " left(convert(char,CreatedTime,106),12)CreatedTime,left(convert(char,ApvMgrUser,106),12)ApvMgrUser,left(convert(char,UpdateTargetTime,106),12)UpdateTargetTime,left(convert(char,DueDateWO,106),12)DueDateWO,left(convert(char,FinishDate,106),12)FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,selisih_apv,case when selisih_pekerjaan<0 then '0 HK' else selisih_pekerjaan + ' HK' end selisih_pekerjaan,WaktuSelesai,WaktuDateLine,  case   " +
            " when Target=1 and   YEAR(WaktuSelesai)>1900   then 0     " +
            " when Target=1 and YEAR(WaktuSelesai)=1900 then selisih_pekerjaan     " +
            " when Target=1 and WaktuSelesai = '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai <> '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai = '' then selisih_pekerjaan  end SisaHari from ( " +
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
            " when (Target is null or Target =1 ) and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat'  " +
            " when  Target =2  and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat - T2'  " +
            " when  Target =3  and FinishDate <> '' and WaktuSelesai>WaktuDateLine then 'Lewat - T3'  when FinishDate <> '' and FinishDate<=DueDateWO and Apv=4 then 'Finish'   " +
            " when FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=4 then 'Finish'  " +
            " when (Target is null or Target =1 ) and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo' " +
            " when Target =2 and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T2'   " +
            " when Target =3 and FinishDate = '' and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T3'  " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=7 and Pelaksana<>'' and DueDateWO<>''  then 'Next: Mgr HRD' " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=0 and AreaWO='Software'  then 'Next: Verifikasi ISO'  " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO='Software'  then 'Next: Mgr IT'   " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO='Software'  then 'Next: Mgr IT'   " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=0 and AreaWO='Software'  then 'Next: Mgr Dept - Plant Terkait'   " +
            " when FinishDate = '' and DueDateWO ='' and DeptID_PenerimaWO=14 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=0 and AreaWO='Software' then 'Next: Verifikasi ISO'   " +
            " when Pelaksana <> '' and DueDateWO =''   and DeptID_PenerimaWO=19  then 'Next:' + Pelaksana   " +
            " when Pelaksana <> '' and DueDateWO <>''   and DeptID_PenerimaWO=19  and Apv=2  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=19  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=14  and Apv=2 and AreaWO='HardWare'  then 'Next: Mgr IT'   " +
            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=7  and Apv=2 and VerSec=1  then 'Next: Mgr HRD'   " +
            " when Pelaksana = '' and DueDateWO =''   and DeptID_PenerimaWO=7  and Apv=2 and AreaWO<>'Kendaraan'  then 'Next: Mgr HRD'  " +
            " when Pelaksana <> '' and DueDateWO <>''   and DeptID_PenerimaWO=7  and Apv=2  then 'Next: Mgr HRD'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO='' then 'Next : Head GA'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO<>'' then 'Next: Mgr HRD'   " +
            " when Target =3 and  FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T3'   " +
            " when Target =2 and  FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T2'  " +
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
            " when apv=4 then 'Finish' when apv=5 then 'Closed' end StatusApv " +
            " from ( " +
            " /** Break 4 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser,UpdateTargetTime,DueDateWO,case when waktu0<0 then '' when waktu0 is null then '' else FinishDate end FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,NamaSubDept,VerISO,WaktuSelesai,WaktuDateLine,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO from ( " +
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
            " case when Target is null then CreatedTime when Target=1 then CreatedTime else CreatedTime2 end CreatedTime,UpdateTargetTime0,Pelaksana,CreatedBy,Apv,VerISO,NamaSubDept,WaktuSelesai,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO from ( " +
            " /** Break 1 **/ " +
            " select A.ID woID,A.NoWO,A.UraianPekerjaan,A.DeptID_Users,A.DeptID_PenerimaWO,A.StatusTarget,A.DueDateWO DueDateWO_awal,D.TglTarget,D.Target,A.FinishDate,A.CreatedTime,D.CreatedTime CreatedTime2,A.UpdateTargetTime UpdateTargetTime0,Pelaksana,A.CreatedBy,A.Apv,A.VerISO,A.NamaSubDept,isnull(LEFT(convert(char,FinishDate,112),8),'')WaktuSelesai,TRIM(A.AreaWO) +' - ' + TRIM(A.SubArea) AreaWO ,A.ApvOP,A.VerSec,A.Cancel,GETDATE()WaktuNow,C.Alias UsersWO,C1.Alias PenerimaWO " +
            " from WorkOrder as A  " +
            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
            " LEFT JOIN Dept as C1 ON A.DeptID_PenerimaWO=C1.ID  " +
            " LEFT JOIN WorkOrder_Target D ON D.WoID=A.ID  " +
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
            //" where YEAR(DueDateWO)='" + Tahun + "' and MONTH(DueDateWO)='" + Periode + "' and UsersWO='" + UsersWO + "' and PenerimaWO='" + PenerimaWO + "'  order by NoWO,Target " +
            "" + Logika + "" +
            " /** end Break 7 **/ ";    
           
            #endregion

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListWO_2(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public ArrayList RetrieveListWO(string NamaSub, string PenerimaWO, string UsersWO, string Periode, int Tahun, string Tanda, string WaktuSkr, int UnitKerjaID, string temp)
        {
            //if (FLag == "3")
            //{
            //    query =
            //    " where YEAR(DueDateWO)='" + ddlTahun.SelectedValue + "' and MONTH(DueDateWO)='" + ddlBulan.SelectedValue + "' " +
            //    " and DeptID_PenerimaWO='" + DeptID + "' ";
            //}
            ///** Pemantauan WO masuk status Open(apv mgr peminta) - Close **/
            //else if (FLag == "2")
            //{
            //    query =
            //    " where YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' " +
            //    " and DeptID_PenerimaWO='" + DeptID + "'  ";
            //}
            ///** Pencapaian WO Keluar **/
            //else if (FLag == "4")
            //{
            //    query =
            //    " where YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' " +
            //    " and DeptID_Users='" + DeptID + "'  ";
            //}
            ///** Pencapaian WO Masuk **/
            //else if (FLag == "5")
            //{
            //    query =
            //    " where YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' " +
            //    " and DeptID_PenerimaWO='" + DeptID + "'  ";
            //}

            string query = string.Empty;
            /** Pemantauan,Pencapaian dan WO Masuk **/
            if (Tanda == "2" || Tanda == "3" )
            {
                query = " UsersWO='" + UsersWO + "' and penerimaWO='" + PenerimaWO + "' order by NoWO desc ";
            }
            /** Pencapaian WO Keluar **/
            else if (Tanda == "4")
            {
                query = " UsersWO='" + UsersWO + "' and penerimaWO='" + PenerimaWO + "' and PlantID ='" + UnitKerjaID + "' "; 
            }
            /** Pencapaian WO Keluar **/
            else if (Tanda == "5" || Tanda == "51")
            {
                if (PenerimaWO == "IT")
                {
                    query = 
                    " UsersWO='" + UsersWO + "' and penerimaWO='" + PenerimaWO + "' and PlantID ='"+UnitKerjaID+"' "+
                    " and DateApvOP <>'' " +
                    " order by NoWO desc ";
                }
                else
                {
                    query = " UsersWO='" + UsersWO + "' and penerimaWO='" + PenerimaWO + "' order by NoWO desc ";
                }
            }

            string strSQL =
            "   select * from "+temp+" where " + query + " ";
            

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListWO_2(sqlDataReader));
                }
            }          

            return arrWO;
        }
       
        public ArrayList RetrieveListWO_1(int DeptID, int Status, int PlantID,int UserID,string ShareWO)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            string Query4 = string.Empty;
            string QueryShareWO = string.Empty; string QueryShareWOLog = string.Empty; string QueryShareWO1 = string.Empty;
            string QueryWO_HRD = string.Empty; 

            if (ShareWO == "1")
            {
                //QueryShareWO1 = " (A.DeptID_Users=" + DeptID + " and A.PlantID <> " + PlantID + " and A.Apv=2 and A.ApvOP=1) ";
                QueryShareWO = " (A.DeptID_Users=" + DeptID + " and A.PlantID <> " + PlantID + " and A.Apv=2 and A.ApvOP=1) ";                
                QueryShareWOLog = " (A.DeptID_Users=" + DeptID + " and A.PlantID <> " + PlantID + " and A.Apv=2 and A.ApvOP=1)  ";
            }
            else 
            {
                if (users.DeptID == 7)
                {
                    QueryShareWO = 
                    //" (A.DeptID_PenerimaWO=7 and A.RowStatus>-1 and A.apv=2 and A.pelaksana is not null and A.DueDateWO is not null ) or "+
                    " (A.DeptID_Users=7  and A.apv=0 and A.RowStatus>-1 and A.DeptID_PenerimaWO<>7) ";
                }
                else
                {
                    if (DeptID == 4 || DeptID == 5 || DeptID == 18)
                    {
                        DeptID = 19;
                    }

                    //QueryShareWO1 = " (A.DeptID_Users=" + DeptID + " and A.Apv=0 and A.rowstatus>-1)  ";
                    QueryShareWO = " (A.DeptID_Users=" + DeptID + " and A.Apv=0 and A.rowstatus>-1) ";
                    QueryShareWOLog = " ( A.DeptID_Users in (10,6) and A.Apv = 0 and A.PlantID=" + PlantID + ") ";                   
                }
            }

            string UnitKerjaID = string.Empty;
            if (PlantID == 7)
            { UnitKerjaID = "1"; }
            else if (PlantID == 1)
            { UnitKerjaID = "7"; }

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID = 19;
            }
           

            if (Status == 3) // Approval Mgr Penerima WO ( IT , HRD , MTC )
            {    
                Query1 = " select WOID,NoWO,UraianPekerjaan,AreaWO,ISNULL(CreatedTime,'')CreatedTime,ISNULL(CreatedTime1,'')CreatedTime1,DueDateWO,FinishDate,Pelaksana,StatusApv,FromDeptName,ToDeptName,PlantName from "+
                         " ( "+
                         " select A.ID WOID,NoWO,UraianPekerjaan,AreaWO," +                         
                         " case when A.PlantID=1 then LEFT(convert(char,A.CreatedTime,106),11) when A.PlantID=7 then LEFT(convert(char,A.CreatedTime,106),11) end CreatedTime, "+
                         //" case when A.PlantID=1 then LEFT(convert(char,D.CreatedTime,106),11) when A.PlantID=7 then LEFT(convert(char,D.CreatedTime,106),11) end CreatedTime1, "+
                         " (select LEFT(convert(char,a.CreatedTime,106),11) from WorkOrder_LogApproval a where a.WOID=A.ID and a.RowStatus>-1 and a.Urutan=1) CreatedTime1, "+
                         " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO," +
                         " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                         " ,ISNULL(Pelaksana,'')Pelaksana, " +
                         " case " +
                         " when A.apv=0 then 'Open' " +
                         //" when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         //" when Apv=1 and VerISO=1 then 'Verified ISO' " +
                         " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=2 and veriso=1 then 'Verifikasi ISO'" +
                         //" when apv=2 then 'PM'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         ",C.Alias FromDeptName,A.DeptID_Users," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                //Query2 = " LEFT JOIN WorkOrder_LogApproval as D on D.WOID=A.ID ";
                Query2 = " ";
                Query3 = " where A.RowStatus>-1  and " +                                             
                         " " + QueryShareWO + " " +
                         " ) as DataX " +
                         " order by DataX.DeptID_Users"; 
            }

            else if (Status == 11) // Approval Security Only For Karawang
            {
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan,AreaWO," +
                         " ISNULL(A.CreatedTime,'')CreatedTime," +
                         " (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) from WorkOrder_LogApproval as lp where lp.WOID=A.ID and lp.RowStatus>-1 and lp.Urutan=1)CreatedTime1," +
                         " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO," +
                         " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                         " ,ISNULL(Pelaksana,'')Pelaksana, " +
                         " case " +
                         " when A.apv=0 then 'Open' " +
                         " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         " when Apv=1 and VerISO=1 then 'Verifikasi ISO' " +
                         //" when apv=2 then 'PM'" + 
                         " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         ",C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = "  ";
                Query3 = " where ((A.DeptID_PenerimaWO=19 and A.Apv=2 and A.rowstatus>-1)) and (A.AreaWO='Kendaraan' and A.VerSec is null and A.rowstatus>-1) " +                        
                         " order by A.DeptID_Users";
            }

            else if (Status == 2) // Approval PM
            {
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +  
                         " case when A.ApvOP in (1,-1) then LEFT(convert(char,A.CreatedTime,106),11) else LEFT(convert(char,D.CreatedTime,106),11) end CreatedTime1, "+                        
                         " case when apv=0 then 'Open'" +

                         " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         //" when Apv=1 and VerISO=1 then 'Verifikasi ISO' " +   
                         
                         " when apv=2 and A.ApvOP=1 and A.PlantID<>"+PlantID+" then 'Mgr Dept.' "+
                         " when apv=2 and A.PlantID=" + PlantID + " then 'PM'" +                         
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' "+
                         " when ApvOP=-1 then 'Tidak Ikut' end StatusApv " +
                         " ,C.Alias FromDeptName," +
                         " case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName "+
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID "+
                         " LEFT JOIN WorkOrder_LogApproval as D on D.WOID=A.ID ";                

                // Revisi sesuai permintaan Pak Sodik 11 Juli 2018
                Query2 = " where A.RowStatus>-1  and " +
                         "((A.Apv = 1  and A.AreaWO='HardWare' and A.DeptID_PenerimaWO=14 and D.RowStatus>-1) or " +
                         "(A.Apv = 1  and A.AreaWO='SoftWare' and A.DeptID_PenerimaWO=14 and D.RowStatus>-1) or " +
                         //"(A.Apv = 1  and (A.DeptID_PenerimaWO <> 14 or A.DeptID_PenerimaWO is null) and D.RowStatus>-1) or" +
                         "(A.ApvOP in (-2,-1) and A.Apv=2)) " +
                         " order by A.DeptID_Users";
                Query3 = " ";
            }

            else if (Status == 9) // Head Kasie MTN ENG
            {
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan,AreaWO," +
                         " (select top 1 LEFT(convert(char,lp.CreatedTime,106),11) from WorkOrder_LogApproval as lp where lp.WOID=A.ID and lp.RowStatus>-1 and lp.Urutan=1)createdtime,"+
                         " LEFT(convert(char,D.CreatedTime,106),11)CreatedTime1," +    
                         " ISNULL(LEFT(convert(char,A.DueDateWO,106),11),'')DueDateWO," +
                         " ISNULL(LEFT(convert(char,A.FinishDate,106),11),'')FinishDate " +
                         " ,ISNULL(Pelaksana,'')Pelaksana, " +
                         " case " +
                         " when A.apv=0 then 'Open' " +
                         // " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         //" when Apv=1 and VerISO=1 then 'Verifikasi ISO' " +
                         " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=2 and veriso=1 then 'Verifikasi ISO'" +
                         //" when apv=2 then 'PM'" +  
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         " ,C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 where A.DeptID_PenerimaWO=19 order by A.DeptID_Users";
                Query3 = "";
            }

            else if (Status == 10) // Approval ISO Khusus buat WO IT SoftWare
            {
                Query1 = " select A.ID WOID,NoWO,UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " ISNULL((select top 1 LEFT(convert(char,wol.CreatedTime,106),11)  from WorkOrder_LogApproval wol where wol.WOID=A.ID and RowStatus>-1 and wol.Urutan=1),'')CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=2 and veriso=1 then 'Verifikasi ISO'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         ",C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                         //" LEFT JOIN WorkOrder_LogApproval as D on D.WOID=A.ID ";                
                Query2 = " where A.RowStatus>-1 and ((A.Apv = 2 and A.VerISO is null and "+
                         " A.AreaWO='SoftWare' and A.DeptID_PenerimaWO=14 and A.rowstatus>-1)) or  (A.Apv=0 and A.DeptID_Users=23 and A.rowstatus>-1) " +
                         " order by A.DeptID_Users ";
                Query3 = " ";            }

            else if (Status == 1 && DeptID == 10 || Status == 1 && DeptID == 6) // Approval Manager Logistik BB & BJ Dept
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 then  (select top 1 wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 then  (select top 1 wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 then  (select top 1 wp.CreatedTime from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=2 and veriso=1 then 'Verifikasi ISO'" +
                         //" when apv=2 then 'PM'" + 
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +                         
                         ",C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                Query2 = " where A.RowStatus>-1 and "+                                            
                         " " + QueryShareWOLog + "";
                         
                Query3 = " ";
            } 
           
            else if (Status == 1) // Approval Manager Dept
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3)" +
                         " when A.Apv=2 and A.PlantID <> " + PlantID + " then LEFT(convert(char,A.CreatedTime,106),11) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         " when Apv=1 and VerISO=1 then 'Verified ISO' " +
                         " when apv=2 and A.VerISO=1 and A.PlantID <> " + PlantID + " and A.PlantID=7 then 'Share Krwg'" +
                         " when apv=2 and A.VerISO=1 and A.PlantID <> " + PlantID + " and A.PlantID=1 then 'Share Ctrp'" +
                         //" when apv=2 then 'PM'"+  
                          " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +                         
                         ",C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +   
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName"+                      
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID ";
                
                if (DeptID == 15 || DeptID == 23 || DeptID == 26 || DeptID == 12 || DeptID == 13 || DeptID == 24)
                {
                    Query2 = " where A.RowStatus>-1 and (A.DeptID_Users=" + DeptID + " and A.Apv=0)";
                }
                else if (DeptID == 7)
                {
                    Query2 = " where A.RowStatus>-1 and ( "+ 
                             " (A.DeptID_Users=" + DeptID + " and A.NamaSubDept='' and A.Apv=0) or " +
                             /** Tambahan : rubah logika HRD yg corporate jadi under plant ( share plant ) **/
                             //" (A.DeptID_Users=" + DeptID + " and A.PlantID <> " + PlantID + " and A.Apv=2 and A.ApvOP=1) or "+
                             /** end tambahan **/                             
                             //" (A.DeptID_Users=" + DeptID + " and A.DeptID_PenerimaWO=7 and A.Apv=1) "+
                             " " + QueryShareWO + " " +
                             " ) ";
                }
                else
                {
                    Query2 = " where A.RowStatus>-1 and " +
                        //"(A.DeptID_Users=" + DeptID + " and A.Apv=0 " +
                        //" or (A.DeptID_Users=" + DeptID + " and A.PlantID <> " + PlantID + " and A.Apv=2 and A.ApvOP=1) "+
                             " " + QueryShareWO + " ";
                             //" ) ";
                }

                Query3 = " ";
            }
            else if (Status == 4) // Approval Head HRD - Internal WO
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when Apv=0 then  ''" +
                         " when Apv=1 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when Apv=2 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when Apv=3 and A.PlantID=" + PlantID + " then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=ID and wp.RowStatus>-1 and wp.Urutan=3)" +
                         " when A.Apv=2 and A.PlantID <> " + PlantID + " then LEFT(convert(char,A.CreatedTime,106),11) end CreatedTime1," +
                         " case when apv=0 then 'Open'" +
                         " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         " when Apv=1 and VerISO=1 then 'Verified ISO' " +
                         " when apv=2 and A.VerISO=1 and A.PlantID <> " + PlantID + " and A.PlantID=7 then 'Share Krwg'" +
                         " when apv=2 and A.VerISO=1 and A.PlantID <> " + PlantID + " and A.PlantID=1 then 'Share Ctrp'" +
                          " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         //" when apv=2 then 'PM'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         ",C.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A " +
                         " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                         " where A.RowStatus>-1 and A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 and A.Apv=0 " +
                         " and A.NamaSubDept in (select NamaSubDept from WorkOrder_SubDept where HeadID=" + UserID + " and RowStatus>-1) ";
                Query2 = " ";
                Query3 = " ";
                         

               
            }

            else if (Status == 0)
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan,AreaWO," +
                             " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                             " case when A.Apv=0 then  ''" +
                             " when A.Apv=1 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                             " when A.Apv=2 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                             " when A.Apv=3 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=3) "+
                             " when A.Apv=4 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=4) "+
                             " when A.Apv=5 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=5)  "+
                             " when A.Apv=6 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=6) "+
                             " end CreatedTime1," +
                             " case when A.apv=0 then 'Open'" +
                             " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                             " when Apv=1 and VerISO=1 then 'Verified ISO' " +
                             //" when apv=2 then 'PM'" +  
                             " when Apv=2 and A.DeptID_Users in (15,7,23,24,26,12,30,28) then 'Mgr Dept-Corp.'  "+
                             " when Apv=2 and A.DeptID_Users not in (15,7,23,24,26,12,30,28) then 'Mgr Dept'  " +
                             " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                             " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                             " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' "+
                             //" when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' "
                             " when apv=4 and DeptID_PenerimaWO<>14 then 'Finish' "+
                             " when apv=4 and DeptID_PenerimaWO=14 then 'Mgr IT' "+
                             " when apv=5 and DeptID_PenerimaWO<>14 then 'Closed' "+
                             " when apv=5 and DeptID_PenerimaWO=14 then 'Finish' "+
                             " when apv=6 and DeptID_PenerimaWO=14 then 'Closed' "+
                             " end StatusApv " +

                             ",B.Alias FromDeptName," +
                             " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                             " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                             " from WorkOrder as A  " +
                             " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID ";
                Query2 = " where A.RowStatus>-1 and ((A.DeptID_Users=" + DeptID + " and A.Apv>=0)) and A.PlantID=" + users.UnitKerjaID + "  order by A.ID asc ";
                Query3 = " ";
            }

            else
            {
                Query1 = " select A.ID WOID,A.NoWO,A.UraianPekerjaan,AreaWO," +
                         " LEFT(convert(char,A.CreatedTime,106),11)CreatedTime," +
                         " case when A.Apv=0 then  ''" +
                         " when A.Apv=1 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=1)" +
                         " when A.Apv=2 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=A.ID and wp.RowStatus>-1 and wp.Urutan=2)" +
                         " when A.Apv=3 then  (select top 1 LEFT(convert(char,wp.CreatedTime,106),11) from WorkOrder_LogApproval as wp where wp.WOID=a.ID and wp.RowStatus>-1 and wp.Urutan=3) end CreatedTime1," +
                         " case when A.apv=0 then 'Open'" +
                         " when apv=1 and (VerISO=0 or VerISO is NULL) then 'Mgr Dept.' " +
                         " when Apv=1 and VerISO=1 then 'Verified ISO' " +
                         //" when apv=2 then 'PM'" + 
                          " when apv in (1,2) and veriso is null then 'Mgr Dept.'" +
                         " when apv=3 and A.DeptID_PenerimaWO=19 then 'Mgr MTN' " +
                         " when Apv=3 and DeptID_PenerimaWO=7 then 'Mgr HRD' " +
                         " when Apv=3 and DeptID_PenerimaWO=14 then 'Mgr IT' end StatusApv " +
                         ",B.Alias FromDeptName," +
                         " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO and RowStatus > -1)ToDeptName " +
                         " , case when A.PlantID = 1 then 'Citeureup' when A.PlantiD=7 then 'Karawang' end PlantName" +
                         " from WorkOrder as A  " +
                         " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID ";           
                Query2 = " where A.RowStatus>-1 and ((A.DeptID_Users=" + DeptID + " and A.Apv=0))   order by A.ID asc ";
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
            //else
            //    arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectListWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();            
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();   
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();            
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.CreatedTime1 = sqlDataReader["CreatedTime1"].ToString();
            objWO.FromDeptName = sqlDataReader["FromDeptName"].ToString();
            objWO.ToDeptName = sqlDataReader["ToDeptName"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.PlantName = sqlDataReader["PlantName"].ToString();

            return objWO;
        }

        public WorkOrder_New GenerateObjectListWO_1(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
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
            objWO.FinishDate2 = sqlDataReader["FinishDate2"].ToString();
            objWO.ElapsedDay = sqlDataReader["ElapsedDay"].ToString();
            objWO.ElapsedWeek = sqlDataReader["ElapsedWeek"].ToString();
            objWO.AliasMtn = sqlDataReader["AliasMtn"].ToString();
            objWO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objWO.SubArea2 = sqlDataReader["SubArea2"].ToString();
            objWO.ApvMgr = sqlDataReader["ApvMgr"].ToString();
            objWO.Selisih = sqlDataReader["Selisih"].ToString();
            objWO.Waktu2 = sqlDataReader["Waktu2"].ToString();
            return objWO;
        }

        public WorkOrder_New GenerateObjectListWO_2(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.SisaHari = sqlDataReader["SisaHari"].ToString();
            objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();
            //objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.DueDateWO = sqlDataReader["DueDateWO"].ToString();
            //objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.FinishDate2 = sqlDataReader["FinishDate2"].ToString();
            //objWO.ElapsedDay = sqlDataReader["ElapsedDay"].ToString();
            //objWO.ElapsedWeek = sqlDataReader["ElapsedWeek"].ToString();
            //objWO.AliasMtn = sqlDataReader["AliasMtn"].ToString();
            objWO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objWO.SubArea2 = sqlDataReader["SubArea2"].ToString();
            objWO.ApvMgr = sqlDataReader["ApvMgr"].ToString();
            objWO.Selisih = sqlDataReader["Selisih"].ToString();
            objWO.Waktu2 = sqlDataReader["Waktu2"].ToString();
            objWO.UpdatePelaksanaTime = sqlDataReader["UpdatePelaksanaTime"].ToString();
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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectListLampiranWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();            
            objWO.FileName = sqlDataReader["FileName"].ToString();
            objWO.ID = Convert.ToInt32(sqlDataReader["ID"]);

            return objWO;
        }

        public ArrayList RetrieveListDept(string WOID)
        {
            string strSQL = " select case when PlantID=1 then 'CITEUREUP' when PlantID=7 then 'KARAWANG' end PlantName,"+
                            " case when DeptID_Users=7 and DeptID_PenerimaWO=7 then 'HRD - '+NamaSubDept else "+
                            " (select Alias from Dept A where A.ID=DeptID_Users and RowStatus>-1) end FromDeptName,(select Alias " +
                            " from Dept A where A.ID=DeptID_PenerimaWO and RowStatus>-1)ToDeptName,ISNULL(AlasanNotApvOP,'')AlasanNotApvOP "+
                            " from WorkOrder where RowStatus>-1 and ID=" + WOID + "";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObjectListDept(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectListDept(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.PlantName = sqlDataReader["PlantName"].ToString();
            objWO.ToDeptName = sqlDataReader["ToDeptName"].ToString();
            objWO.FromDeptName = sqlDataReader["FromDeptName"].ToString();
            objWO.AlasanNotApvOP = sqlDataReader["AlasanNotApvOP"].ToString();
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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectListLampiranWOKosong(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();         
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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectHeaderDept(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            return objWO;
        }

        public ArrayList RetrieveWOall(int StatusApv, int DeptID, string Pelaksana, int PlantID)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            

            if (DeptID == 14)
            {
                Query1 = " A.AreaWO='HardWare' and Apv=2 and DeptID_PenerimaWO=14 UNION ALL " +
                         " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget, " +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate, UraianPekerjaan,case when A.ApvOP in (-1,-2) then 'Tidak Ikut' " +
                         " when A.ApvOP in (1,2) then 'Ikut' else '' end StatusWO,ISNULL(PlantID,'')PlantID,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO  " +
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 " +
                         " from WorkOrder as A  " +
                         " where  A.RowStatus>-1 and (A.AreaWO='SoftWare' and ((Apv=2 and VerISO=1 and ApvOP in (2,-2)) and A.RowStatus > -1   and  DeptID_PenerimaWO=14  and A.PlantID = " + PlantID + " " +
                         " or " +
                         " A.StatusTarget is not null and A.ID in (select WoID from WorkOrder_Target where aktif=1 and Approval=0 and RowStatus>-1)) ) ";
                         //" and A.PlantID = "+PlantID+"";
            }
            else if (DeptID == 7)
            {
                Query1 = " (A.Pelaksana is not NULL and A.DueDateWO is not NULL and A.Apv=2 and A.DeptID_PenerimaWO=" + DeptID + " and A.RowStatus>-1 " +
                         " or A.rowstatus>-1 and A.DeptID_PenerimaWO=" + DeptID + " and A.StatusTarget is not null and A.ID in (select WoID from WorkOrder_Target where aktif=1 and Approval=0 and RowStatus>-1)) ";            
               
            }
            else if (DeptID == 19)
            {   
                Query1 = "(" +
                         " (A.Pelaksana is NULL and A.DueDateWO is NULL and A.Apv=2  and A.DeptID_PenerimaWO=19 and A.RowStatus>-1)" +
                         " or (A.Pelaksana = '' and A.DueDateWO is NULL and A.Apv=2  and A.DeptID_PenerimaWO=19 and A.RowStatus>-1)" +
                         " or (A.Pelaksana is not null and A.DueDateWO is not null and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.RowStatus>-1)" +                            
                         " or (A.AreaWO = 'General' and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is not null and A.DueDateWO is not null and A.RowStatus>-1)" +
                         " or A.DeptID_PenerimaWO=19 and A.StatusTarget is not null and A.ID in (select WoID from WorkOrder_Target where aktif=1 and Approval=0 and RowStatus>-1)) " +
                         " order by A.ID desc";

            }
            else 
            {   
                Query1 = "((A.DeptID_Users=" + DeptID + " and A.Apv=0)" +
                         " or (A.Pelaksana is NULL and A.DueDateWO is NULL and A.Apv=2  and A.DeptID_PenerimaWO=19 and A.DeptID_Users <> 26 and A.RowStatus>-1)" +
                         " or (A.Pelaksana is not null and A.DueDateWO is not null and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.DeptID_Users <> 26 and A.RowStatus>-1)" +
                         " or (A.AreaWO = 'Kendaraan' and A.VerSec = 1  and A.DeptID_Users=26 and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is not null and A.DueDateWO is not NULL and A.RowStatus>-1)" +
                         " or (A.AreaWO = 'Kendaraan' and A.VerSec = 1 and A.DeptID_Users=26 and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is null and A.DueDateWO is  NULL and A.RowStatus>-1) " +
                         " or (A.AreaWO = 'General' and A.VerSec is null  and A.DeptID_Users=26 and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is null and A.DueDateWO is NULL and A.RowStatus>-1)" +
                         " or (A.AreaWO = 'General' and A.VerSec is null and A.DeptID_Users=26 and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is not null and A.DueDateWO is not NULL and A.RowStatus>-1) " +
                         " or (A.AreaWO = 'General' and A.DeptID_Users<>26  and A.Apv=2 and A.DeptID_PenerimaWO=19 and A.Pelaksana is not null and A.DueDateWO is not null and A.RowStatus>-1))" +
                         " order by A.ID desc";

            }
           
            // Level Manager MTC
            if (StatusApv == 3 && DeptID == 19)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, "+                        
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,case when A.ApvOP in (-1,-2) then 'Tidak Ikut' when A.ApvOP in (1,2) then 'Ikut' else '' "+
                         " end StatusWO,ISNULL(PlantID,'')PlantID,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO  "+
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 from WorkOrder as A " +
                         //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where  A.RowStatus>-1 and " + Query1 + " ";   
            }

            // Level Manager HRDGA
            if (StatusApv == 3 && DeptID != 19 || StatusApv == 3 && DeptID != 14)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, "+                         
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,case when A.ApvOP in (-2,-1) then 'Tidak Ikut' when A.ApvOP in (1,2) then 'Ikut' else '' "+
                         " end StatusWO,ISNULL(PlantID,'')PlantID,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO "+
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 from WorkOrder as A " +
                         " where  A.RowStatus>-1 and " + Query1 + " ";

            }
            // Level Head selain HRD GA
            else if (StatusApv == 9 && DeptID != 7)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,case when A.ApvOP=0 then 'Tidak Ikut' when A.ApvOP=1 then 'Ikut' else '' " +
                         " end StatusWO,ISNULL(PlantID,'')PlantID,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO  " +
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 from WorkOrder as A " +
                    //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=3 and A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptID + " and (A.DueDateWO is not NULL or A.DueDateWO='') and  (A.FinishDate is null or A.FinishDate = '') ";
                         //
            }
            // Level Head HRD GA
            else if (StatusApv == 9 && DeptID == 7)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,case when A.ApvOP=0 then 'Tidak Ikut' when A.ApvOP=1 then 'Ikut' else '' " +
                         " end StatusWO,ISNULL(PlantID,'')PlantID,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO  " +
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 from WorkOrder as A " +
                         " where  A.RowStatus>-1 and " +
                         " (Apv=2 and A.DueDateWO is NULL and pelaksana is not null and A.RowStatus>-1 and A.DeptID_PenerimaWO=" + DeptID + ")" +
                         " or "+
                         " (Apv=3 and A.DueDateWO is not NULL and pelaksana is not null and A.RowStatus>-1 and A.DeptID_PenerimaWO=" + DeptID + ")";
            }
            else if (StatusApv == 8)
            {
                strSQL = " select A.ID WOID,NoWO,(select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1)DeptName, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wol.createdtime,106),11),'') from WorkOrder_LogApproval wol where wol.WOID=A.ID and wol.Urutan=2 and wol.rowstatus>-1)TglDisetujui,  " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,case when A.ApvOP=0 then 'Tidak Ikut' when A.ApvOP=1 then 'Ikut' else '' " +
                         " end StatusWO,ISNULL(PlantID,'')PlantID ,ISNULL(A.PermintaanWO,'') Permintaan,ISNULL(A.TipeWO,'') TipeWO " +
                         " ,case when A.TipeWO='Urgent' then 1 when A.tipeWO='Sesuai Schedulle' then 2 else 3 end Flag1 from WorkOrder as A " +                   
                         " where Apv=2 and A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptID + " and A.DueDateWO is NULL " +
                         " and A.Pelaksana='" + Pelaksana + "' and A.RowStatus > -1 ";                         
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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectWOall(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();           
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objWO.Permintaan = sqlDataReader["Permintaan"].ToString();
            objWO.TipeWO = sqlDataReader["TipeWO"].ToString();
            objWO.Flag1 = Convert.ToInt32(sqlDataReader["Flag1"]);

            return objWO;
        }

        public WorkOrder_New RetrieveWOPerNoWO(string NoWO, int StatApv, int DeptID)
        {
            string strSQL = string.Empty;
            

            if (StatApv == 3)
            {
                strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                         " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,A.Apv, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, "+                        
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, "+
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,case when A.ApvOP=-2 then 'TIDAK IKUT' when A.ApvOP=2 "+
                         " then 'IKUT' else '' end StatusWO,A.PlantID,ISNULL(A.SubArea,'')SubArea,ISNULL(A.PermintaanWO,'')Permintaan,"+
                         " ISNULL(A.TipeWO,'')TipeWO,A.DeptID_Users from WorkOrder as A " +
                         " where ((Apv=2) or (A.StatusTarget is not null and A.ID in (select WoID from WorkOrder_Target where aktif=1 and Approval=0 and RowStatus>-1))) and A.RowStatus > -1 and A.NoWO='" + NoWO + "' ";
                        
            }
            else if (StatApv == 9 && DeptID !=7)
            {
                strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                         " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,A.Apv, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,"+
                         //"ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,case when A.ApvOP=-2 then 'TIDAK IKUT' when A.ApvOP=2 " +
                         " then 'IKUT' else '' end StatusWO,A.PlantID,ISNULL(A.SubArea,'')SubArea,ISNULL(A.PermintaanWO,'')Permintaan,ISNULL(A.TipeWO,'')TipeWO,A.DeptID_Users from WorkOrder as A " +
                         //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=3 and A.RowStatus > -1 and A.DueDateWO is not NULL and A.NoWO='" + NoWO + "' ";
            }
            else if (StatApv == 9 && DeptID == 7)
            {
                strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                         " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,A.Apv, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat," +
                    //"ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,case when A.ApvOP=-2 then 'TIDAK IKUT' when A.ApvOP=2 " +
                         " then 'IKUT' else '' end StatusWO,A.PlantID,ISNULL(A.SubArea,'')SubArea,ISNULL(A.PermintaanWO,'')Permintaan,ISNULL(A.TipeWO,'')TipeWO,A.DeptID_Users from WorkOrder as A " +
                    //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where ((Apv=2 and A.DueDateWO is NULL) or (Apv=3 and  A.DueDateWO is not NULL)) and A.RowStatus > -1 and A.NoWO='" + NoWO + "' ";
            }

            else if (StatApv == 8)
            {
                strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                         " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,A.Apv, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,"+
                         //"ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,case when A.ApvOP=-2 then 'TIDAK IKUT' when A.ApvOP=2 " +
                         " then 'IKUT' else '' end StatusWO,A.PlantID,ISNULL(A.SubArea,'')SubArea,ISNULL(A.PermintaanWO,'')Permintaan,ISNULL(A.TipeWO,'')TipeWO,A.DeptID_Users from WorkOrder as A " +
                         //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=2 and A.RowStatus > -1 and A.DueDateWO is NULL and A.NoWO='" + NoWO + "' ";
            }
            else if (StatApv == 11)
            {
                strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                         " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,A.Apv, " +
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,"+
                         //"ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,case when A.ApvOP=-2 then 'TIDAK IKUT' when A.ApvOP=2 " +
                         " then 'IKUT' else '' end StatusWO,A.PlantID,ISNULL(A.SubArea,'')SubArea,ISNULL(A.PermintaanWO,'')Permintaan,ISNULL(A.TipeWO,'')TipeWO,A.DeptID_Users from WorkOrder as A " +
                         //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=2 and A.RowStatus > -1 and A.VerSec=1  and A.DueDateWO is not NULL and A.NoWO='" + NoWO + "' ";
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

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObjectWOPerNoWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();    
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objWO.SubArea = sqlDataReader["SubArea"].ToString();
            objWO.Permintaan = sqlDataReader["Permintaan"].ToString();
            objWO.TipeWO = sqlDataReader["TipeWO"].ToString();
            objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"]);
            objWO.Apv = Convert.ToInt32(sqlDataReader["Apv"]);

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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectListWO_Lampiran(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.IDLampiran = Convert.ToInt32(sqlDataReader["IDLampiran"]);
            objWO.FileName = sqlDataReader["FileName"].ToString();           
            return objWO;
        }

        public ArrayList RetrieveLampiranWO(string ID)
        {
            string StrSql = " select FileLampiran FileName,ISNULL(FileLampiranOP,0)FileLampiranOP from WorkOrder_Lampiran where ID=" + ID + " and RowStatus > -1";

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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectList_RetrieveLampiranWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();          
            objWO.FileName = sqlDataReader["FileName"].ToString();
            objWO.FileLampiranOP = (byte[])sqlDataReader["FileLampiranOP"];
            return objWO;
        }

        public ArrayList RetrieveWO_Finish(string DeptID)
        {
            string strSQL = string.Empty;
            string DeptID1 = string.Empty;

            if (DeptID == "4" || DeptID == "5" || DeptID == "18" || DeptID == "25")
            {
                DeptID1 = "19";
            }
            else if (DeptID == "10" || DeptID == "6")
            {
                DeptID1 = "6,10";
            }
            else if (DeptID == "7")
            {
                DeptID1 = "7,29";
            }
            else 
            {
                DeptID1 = DeptID;
            }
         
            strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                     " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName, "+
                     " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, "+
                     " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, "+
                     //" LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +

                     " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                     " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                     " UraianPekerjaan,A.AreaWO,ISNULL(A.DeptID_PenerimaWO,'') ToDept,A.DeptID_Users  from WorkOrder as A " +
                     //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                     " where Apv=4 and A.RowStatus > -1 and A.DueDateWO is not NULL and A.DeptID_Users in (" + DeptID1 + ")";           
            
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
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public WorkOrder_New GenerateObjectList_RetrieveWO_Finish(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);


            return objWO;
        }

        public WorkOrder_New RetrieveWOFinishNoWO(string NoWO)
        {
            string strSQL = string.Empty;


            strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else "+ 
                     " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName, "+
                         " LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat, " +
                //"ISNULL(LEFT(CONVERT(CHAR,wl.createdtime,106),11),'')TglDisetujui, " +
                         " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui, " +
                         " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget," +
                         " ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate," +
                         " UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept,(select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO "+
                         " and RowStatus > -1)ToDeptName,A.UraianPerbaikan,A.DeptID_Users from WorkOrder as A " +
                         //" LEFT JOIN WorkOrder_LogApproval as wl ON A.ID=wl.WOID " +
                         " where Apv=4 and A.RowStatus > -1  and A.NoWO='" + NoWO + "' ";  
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

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveWOFinishNoWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.ToDeptName = sqlDataReader["ToDeptName"].ToString();
            objWO.UraianPerbaikan = sqlDataReader["UraianPerbaikan"].ToString();
            objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"]);

            return objWO;
        }

        public ArrayList RetrieveNamaDept_Masuk(string status,int DeptID, int Tahun, string Periode, string Flag, int StatusReport, string Corp,int UnitKerjaID, string temp)      
        {
            string Query1 = string.Empty; string QueryA = string.Empty;

            if (DeptID == 19 || DeptID == 4 || DeptID == 18 || DeptID == 5 || DeptID == 25)
            {
                DeptID = 19;
               // Query1 =
               // " and YEAR(A.CreatedTime)=" + Tahun + " and MONTH(A.CreatedTime)=" + Periode + " and "+
               // " ("+
               // " (A.Apv>=2 and A.VerSec=1 and A.AreaWO='Kendaraan' and A.RowStatus > -1) or " +
               // " (A.Apv>=2 and A.AreaWO<>'Kendaraan' and A.RowStatus > -1 )" +
               // " )" +
               // " and A.DeptID_PenerimaWO=" + DeptID + " and A.RowStatus > -1";

               // QueryA =
               //" select DISTINCT(B.Alias) DeptName from workorder as A " +
               //" INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
               //" where A.PlantID=" + UnitKerjaID + " " +
               //" " + Query1 + " ";
                Query1 = "";
                QueryA = " select distinct UsersWO DeptName from " + temp + " where DeptID_PenerimaWO=" + DeptID + " order by UsersWO ";


            }
            else if (DeptID == 7)
            {
                //Query1 =
                //" and YEAR(A.CreatedTime)=" + Tahun + " and MONTH(A.CreatedTime)=" + Periode + " and A.Apv>=2" +
                //" and A.DeptID_PenerimaWO=" + DeptID + " and A.RowStatus > -1 ) as xx";

                //QueryA = 
                //" select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from " +
                //" (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                //" where A.PlantID=" + UnitKerjaID + "" +
                //" " + Query1 + " ";
                Query1 = "";
                QueryA = " select distinct UsersWO DeptName from " + temp + " where DeptID_PenerimaWO=" + DeptID + " order by UsersWO ";
            }
            else if (DeptID == 14)
            {
                //Query1 =
                //" and ("+
                //" (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='SoftWare' and A.ApvOP in (2) and A.Apv>=2 and A.DeptID_Users in (" + Corp + ") and A.ID in (select WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=3)) or  " +
                //" (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='SoftWare' and A.ApvOP in (2,-2) and A.Apv>=2 and A.DeptID_Users not in (" + Corp + ",11) and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=3)) or " +
                //" (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.DeptID_Users=11 and A.AreaWO='SoftWare' and A.ApvOP in (2,-2) and A.Apv>=2 and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)) or " +
                //" (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='HardWare' and A.Apv>=2 and A.DeptID_Users in (" + Corp + ")) and A.ID in (select  WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)) or " +
                //" (A.RowStatus > -1 and A.DeptID_PenerimaWO=14 and A.AreaWO='HardWare' and A.Apv>=2 and A.DeptID_Users not in (" + Corp + ") and A.ID in (select WOID from WorkOrder_LogApproval where YEAR(CreatedTime)=" + Tahun + " and MONTH(CreatedTime)=" + Periode + " and Urutan=2)" +
                //" )";

                //QueryA = 
                //" select DISTINCT(B.Alias) DeptName from workorder as A " + 
                //" INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                //" where A.PlantID=" + UnitKerjaID + " " +
                //" " + Query1 + " ";
                Query1 = "";
                QueryA =
                " select distinct UsersWO DeptName from " + temp + " " +
                " where DeptID_PenerimaWO=" + DeptID + " and PlantID='" + UnitKerjaID + "' "+
                " and DateApvOP <>'' "+  
                " order by UsersWO ";
            }



            ArrayList arrData = new ArrayList(); 
            //string strSQL = " select DISTINCT(B.Alias) DeptName from workorder as A " +
            //                " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
            //                " where A.PlantID=" + UnitKerjaID + " " +
            //                " " + Query1 + " ";

            //string strSQL = " select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from " +
            //                " (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
            //                " where A.PlantID=" + UnitKerjaID + "" +
            //                " " + Query1 + " ";
            string strSQL = QueryA;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {                       
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_Keluar(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID, int FLagUser, string NamaSub, string temp)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            string Query1 = string.Empty; string DeptiD2 = string.Empty; string Query2 = string.Empty; 

            if (users.DeptID == 2 || users.DeptID == 10 || users.DeptID == 6 || users.DeptID == 11 || users.DeptID == 3 || users.DeptID == 9)
            {
                Query2 = " where DeptID_Users=" + users.DeptID + "";
            }
            else
            {
                Query2 = "";
            }
            
            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25)
            { DeptiD2 = "19"; }
            else
            { DeptiD2 = DeptID.ToString(); }

            if (FLagUser > 0 && DeptiD2 != "23")
            {
                //Query1 = " A.DeptID_Users=" + DeptiD2 + " and A.NamaSubDept='"+NamaSub.Trim()+"' ";
                Query1 = " DeptID_Users=" + DeptiD2 + " and NamaSubDept='" + NamaSub.Trim() + "' ";
            }
            else if (DeptiD2 == "23")
            {
                //Query1 = " A.DeptID_Users=" + FLagUser + " and A.NamaSubDept='" + NamaSub.Trim() + "' ";
                Query1 = " DeptID_Users=" + FLagUser + " and NamaSubDept='" + NamaSub.Trim() + "' ";
            }
            else
            {                
                //Query1 = " A.DeptID_Users=" + DeptiD2 + "";
                Query1 = " DeptID_Users=" + DeptiD2 + "";
            }

            ArrayList arrData = new ArrayList();
            string strSQL =
                //" select DISTINCT(B.Alias) DeptName from workorder as A " +
                //" LEFT JOIN Dept as B ON A.DeptID_PenerimaWO=B.ID " +
                //" where A.RowStatus > -1 and A.Apv>=0 and " + Query1 + " and A.PlantID=" + UnitKerjaID + " " +
                //" and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "' ";   

            //" select distinct b.Alias DeptName from ( " +
                //" select * from ( "+
                //" select * from ( "+
                //" select isnull((select top 1 CreatedTime from WorkOrder_LogApproval B where B.WOID=A.ID and B.RowStatus>-1 and B.Urutan=1),0)ApvMgrUser, "+
                //" * from WorkOrder A where  " + Query1 + " and PlantID=" + UnitKerjaID + ") as x where left(convert(char,ApvMgrUser,112),4)<>'1900' " +
                //" ) as xx where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Periode + "' " +
                //" ) as xxx  "+
                //" inner join Dept b ON b.ID=xxx.DeptID_Users ";
            " select distinct PenerimaWO DeptName from " + temp + " " +
            " where YEAR(CreatedTime)='" + Tahun + "' and MONTH(CreatedTime)='" + Periode + "'  and "+
            " DeptID_Users='" + DeptiD2 + "' and PlantID=" + UnitKerjaID + " ";


            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_PencapaianWO(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID, string temp)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            string Query1 = string.Empty;
            string DeptiD2 = string.Empty;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25)
            {
                DeptiD2 = "19";
            }
            else
            {
                DeptiD2 = DeptID.ToString();
            }

            if (users.DeptID == 2 || users.DeptID == 10 || users.DeptID == 6 || users.DeptID == 11 || users.DeptID == 3 || users.DeptID == 9)
            {
                Query1 = " where DeptID_Users=" + users.DeptID + "";
            }
            else
            {
                Query1 = "";
            }

            ArrayList arrData = new ArrayList();
            string strSQL =                 
            //" select DISTINCT(B.Alias) DeptName from workorder as A " +
            //" LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
            //" where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.DueDateWO)='" + Tahun + "' " +
            //" and A.Apv>0 and MONTH(A.DueDateWO)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ";

            //" select DISTINCT(A.Alias) DeptName from ( " +
            //" select DeptID_Users from workorder where DeptID_PenerimaWO=" + DeptiD2 + " and RowStatus > -1 and  YEAR(DueDateWO)='" + Tahun + "'  "+
            //" and Apv>0 and MONTH(DueDateWO)='" + Periode + "' and PlantID=" + UnitKerjaID + "" +
            //" union " +
            //" select DeptID_Users from WorkOrder where DeptID_PenerimaWO=" + DeptiD2 + "" +
            //" and ID in (select WOID from WorkOrder_Target where RowStatus>-1 and MONTH(TglTarget)='" + Periode + "' and YEAR(TglTarget)='" + Tahun + "')  and RowStatus>-1) as x " +
            //" inner join Dept A ON A.ID=x.DeptID_Users ";
            " select distinct UsersWO DeptName from " + temp + " " + Query1 + " order by UsersWO ";

          
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_PemantauanWO(string status, int DeptID, int Tahun, string Periode, string Flag, int UnitKerjaID, string temp)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Query1 = string.Empty; string QueryA = string.Empty; string QueryB = string.Empty;
            string Query2 = string.Empty; string Query3 = string.Empty;

            if (users.DeptID == 2 || users.DeptID == 10 || users.DeptID == 6 || users.DeptID == 11 || users.DeptID == 3 || users.DeptID == 9)
            {
                Query2 = " where DeptID_Users=" + users.DeptID + "";
                Query3 = " and DeptID_Users=" + users.DeptID + "";
            }
            else
            {
                Query2 = ""; Query3 = "";
            }

            string DeptiD2 = string.Empty;
            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25)
            {
                DeptiD2 = "19";
            }
            else
            {
                DeptiD2 = DeptID.ToString();
            }
            if (DeptID != 7)
            {
                if (Flag != "2")
                {
                    QueryA = " select DISTINCT(B.Alias) DeptName from workorder as A " +
                             " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
                             " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "' " +
                             " and A.Apv>=0 and MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ";
                }
                else if (Flag == "2")
                {
                    QueryA =
                        //" select b.Alias DeptName from ( " +
                        //" select * from ( "+
                        //" select * from ( "+
                        //" select isnull((select top 1 CreatedTime from WorkOrder_LogApproval B where B.WOID=A.ID and B.RowStatus>-1 and B.Urutan=1),0) "+
                        //" ApvMgrUser,* from WorkOrder A where  DeptID_PenerimaWO=" + DeptiD2 + " and PlantID=" + UnitKerjaID + ") as x "+
                        //" where left(convert(char,ApvMgrUser,112),4)<>'1900' " +
                        ////" and DueDateWO is null "+
                        //" ) as xx where YEAR(ApvMgrUser)='" + Tahun + "' and MONTH(ApvMgrUser)='" + Periode + "' " +
                        //" ) as xxx "+
                        //" inner join Dept b ON b.ID=xxx.DeptID_Users";
                    " select distinct UsersWO DeptName from " + temp + " where DeptID_PenerimaWO='" + DeptiD2 + "' " + Query3 + " order by UsersWO ";
                }
            }

            if (DeptID == 7)
            {
                QueryA =
                    //" select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from " +
                    //     " (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
                    //     " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "'  and A.Apv>=0 and " +
                    //     " MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ) as xx ";
                " select distinct DeptName from ( " +
                " select case when UsersWO='HRD & GA' then 'HRD - '+NamaSubDept else UsersWO end DeptName,DeptID_Users from " + temp + " " +
                " where  DeptID_PenerimaWO='" + DeptiD2 + "' ) as x " + Query2 + "" +   
                " order by DeptName ";

            }
            ArrayList arrData = new ArrayList();
            //string strSQL = " select DISTINCT(B.Alias) DeptName from workorder as A " +
            //                " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +
            //                " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "' " +
            //                " and A.Apv>=0 and MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID="+UnitKerjaID+" ";
            //DataAccess da = new DataAccess(Global.ConnectionString());
            //string strSQL = 
            //" select case when DeptName='HRD & GA' then 'HRD - '+NamaSubDept else DeptName end DeptName from "+
            //" (select DISTINCT(B.Alias) DeptName,A.NamaSubDept from workorder as A  LEFT JOIN Dept as B ON A.DeptID_Users=B.ID "+
            //" where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.CreatedTime)='" + Tahun + "'  and A.Apv>=0 and " +
            //" MONTH(A.CreatedTime)='" + Periode + "' and A.PlantID=" + UnitKerjaID + " ) as xx ";
            string strSQL = QueryA;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
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
            string DeptiD2 = string.Empty;
            if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19 || DeptID == 25)
            {
                DeptiD2 = "19";
            }
            else
            {
                DeptiD2 = DeptID.ToString();
            } 

            ArrayList arrData = new ArrayList();           
            string strSQL = " select DISTINCT(B.Alias) DeptName from workorder as A " +
                            " LEFT JOIN Dept as B ON A.DeptID_Users=B.ID " +                            
                            " where A.RowStatus > -1 and A.DeptID_PenerimaWO=" + DeptiD2 + " and YEAR(A.DueDateWO)='" + Tahun + "' " +
                            " and A.Apv>0 and MONTH(A.DueDateWO)='" + Periode + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_3(string status, int DeptID, int Tahun, string Periode, string Flag)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string DeptID1 = string.Empty;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID1 = "19";
            }
            
            else
            {
                DeptID1 = DeptID.ToString();
            }

            Query1 = "and A.DeptID_Users=" + DeptID1 + "";

            if (Flag == "0")
            {
                Query2 = " and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "' ";
            }
            else { Query2 = " and YEAR(A.DueDateWO)='" + Tahun + "' and MONTH(A.DueDateWO)='" + Periode + "' "; }
           

            ArrayList arrData = new ArrayList();
            string strSQL = " select DISTINCT(B1.Alias)ToDeptName,(B.Alias) DeptName from workorder as A " +
                            " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                            " INNER JOIN Dept as B1 ON A.DeptID_PenerimaWO=B1.ID " +
                            " LEFT JOIN WorkOrder_LogApproval C ON C.WOID=A.ID " +
                            " where A.RowStatus > -1 " + Query1 + " " +                            
                            " " + Query2 + " " +               
                            " and ((C.Urutan>1 and C.rowstatus>-1) or (C.Urutan is null)) ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString(),
                        ToDeptName = sdr["ToDeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_5(string status, int DeptID, int Tahun, string Periode, string Flag, string Flag2)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            string DeptID1 = string.Empty;

            if (DeptID == 19)
            { Query3 = " and ((A.Apv>=2 and A.VerSec=1 and A.AreaWO='Kendaraan') or (A.Apv>=2 and A.AreaWO<>'Kendaraan'))"; }
            else if (DeptID == 7)
            { Query3 = " and A.Apv>=2 "; }
            else if (DeptID == 14)
            { Query3 = " and ((A.Apv>=2 and A.AreaWo='HardWare') or ( A.Apv>=2 and A.AreaWO='SoftWare' and A.ApvOP=2 and A.VerISO=1))"; }


            Query1 = " and A.DeptID_PenerimaWO="+DeptID+" ";

            if (Flag2 == "1")
            {
                Query2 = " and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "'  ";
            }
            else if (Flag2 == "2")
            {
                Query2 = " and YEAR(A.DueDateWO)='" + Tahun + "' and MONTH(A.DueDateWO)='" + Periode + "'  ";
            }
            else if (Flag2 == "3")
            {
                Query2 = " and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "'  ";
            }


            ArrayList arrData = new ArrayList();
            string strSQL = " select DISTINCT(B.Alias)DeptName from workorder as A " +
                            " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                            " INNER JOIN Dept as B1 ON A.DeptID_PenerimaWO=B1.ID " +              
                            " where A.RowStatus > -1 " + Query1 + " " +
                            " " + Query2 + " " +
                            " " + Query3 + " ";                           
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                        //ToDeptName = sdr["ToDeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_4(string status, int DeptID, int Tahun, string Periode, string Flag)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            string DeptID1 = string.Empty;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID1 = "19";
            }
            else
            {
                DeptID1 = DeptID.ToString();
            }

            Query1 = "and A.DeptID_Users=" + DeptID1 + "";
            //Query3 = "DISTINCT(B1.Alias) DeptName";

            if (Flag == "0")
            {
                Query2 = " and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "' ";
                Query3 = "DISTINCT(B1.Alias) DeptName";
            }
            else 
            {
                Query2 = " and YEAR(A.DueDateWO)='" + Tahun + "' and MONTH(A.DueDateWO)='" + Periode + "' ";
                Query3 = "DISTINCT(B.Alias) DeptName";
            }


            ArrayList arrData = new ArrayList();
            string strSQL = " select "+Query3+" from workorder as A " +
                            " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                            " INNER JOIN Dept as B1 ON A.DeptID_PenerimaWO=B1.ID " +
                            " where A.RowStatus > -1 " + Query1 + " and A.Apv>=0" +
                            " " + Query2 + " ";
                            //" and ((C.Urutan>1 and C.rowstatus>-1) or (C.Urutan is null)) ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                        //ToDeptName = sdr["ToDeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveNamaDept_12(string status, int DeptID, int Tahun, string Periode, string Flag)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            string DeptID1 = string.Empty;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID1 = "19";
            }
            else
            {
                DeptID1 = DeptID.ToString();
            }

            Query1 = "and A.DeptID_PenerimaWO=" + DeptID1 + "";            
            Query2 = " and YEAR(A.CreatedTime)='" + Tahun + "' and MONTH(A.CreatedTime)='" + Periode + "' ";
            Query3 = "DISTINCT(B1.Alias) DeptName";
            

            ArrayList arrData = new ArrayList();
            string strSQL = " select " + Query3 + " from workorder as A " +
                            " INNER JOIN Dept as B ON A.DeptID_Users=B.ID " +
                            " INNER JOIN Dept as B1 ON A.DeptID_PenerimaWO=B1.ID " +
                            " where A.RowStatus > -1 " + Query1 + " and A.Apv>=0" +
                            " " + Query2 + " ";
            //" and ((C.Urutan>1 and C.rowstatus>-1) or (C.Urutan is null)) ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        DeptName = sdr["DeptName"].ToString()
                        //ToDeptName = sdr["ToDeptName"].ToString()
                    });
                }
            }
            return arrData;
        }

       

        public string RetrieveNamaDept(string DeptID)
        {
            string result = "0";
            string StrSql = " select Alias DeptName from dept where rowstatus>-1 and ID=" + DeptID + " ";
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

        public ArrayList RetrieveArea(int DeptID, int ID)
        {            
            ArrayList arrData = new ArrayList();
            string strSQL = " select ID AreaID,AreaWO from WorkOrder_Area where RowStatus > -1 and DeptID=" + DeptID + " and DeptID_PenerimaWO " +
                            " in (select DeptID from WorkOrder_Dept where ID="+ID+" and RowStatus > -1) ";
                           
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        AreaID = Convert.ToInt32(sdr["AreaID"].ToString()),
                        AreaWO = sdr["AreaWO"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveSubArea(string Area)
        {
            ArrayList arrData = new ArrayList();
            string strSQL = " select ID SubAreaID,SubArea from WorkOrder_SubArea where AreaID in "+
                            "(select ID from WorkOrder_Area where AreaWO='" + Area + "' and RowStatus > -1) and RowStatus > -1 ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        SubAreaID = Convert.ToInt32(sdr["SubAreaID"].ToString()),
                        SubArea = sdr["SubArea"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveDept(int DeptID)
        {
            ArrayList arrData = new ArrayList();
            string strSQL = " select ID IDDept,NamaDept from WorkOrder_Dept where DeptID <> " + DeptID + " and RowStatus>-1 and flag=0 ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        IDDept = Convert.ToInt32(sdr["IDDept"].ToString()),
                        NamaDept = sdr["NamaDept"].ToString()
                    });
                }
            }
            return arrData;
        }

        public int RetrieveTotalWO(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport, string temp)
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

            //string StrSql = " select sum(TotalWO)TotalWO from " +
            //                " (select COUNT(DueDateWO)TotalWO from WorkOrder where MONTH(DueDateWO)=" + Bulan + " and " + Query1 + " and" +
            //                " YEAR(DueDateWO)=" + Tahun + " and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + "" +
            //                " ) as xx ";
            //                //" union all " +
            //                //" select COUNT(TglTarget)TotalWO from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + " and YEAR(TglTarget)=" + Tahun + " " +
            //                //" and RowStatus > -1 and WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " " +
            //                //" )) as xx ";

            string StrSql =
                //"  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDatattl]') AND type in (N'U')) DROP TABLE [dbo].[tempDatattl]   " +

            " select COUNT(WOID)TotalWO  from ( " +
            " select distinct(WOID)WOID from  " +
            " (select ID WOID from WorkOrder where MONTH(DueDateWO)=" + Bulan + "   and   " + Query1 + "   and  " +
            " YEAR(DueDateWO)=" + Tahun + "   and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + " group by ID " +

            " union all  " +

            " select WOID from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + "   and YEAR(TglTarget)=" + Tahun + "    " +
            " and RowStatus > -1 and WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID " +
            " ) as xx ) as xxx ";

            //" select * from tempDatattl ";


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

        public WorkOrder_New RetrieveTotalWO_PerHead(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport, string temp)
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
                //"  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDatattl]') AND type in (N'U')) DROP TABLE [dbo].[tempDatattl]   " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDatattl]') AND type in (N'U')) DROP TABLE [dbo].[tempDatattl]  " +

            " select * into tempDatattl from ( " +
            " select COUNT(Pelaksana)TotalWO,Pelaksana "+
            " from ( select WOID,Pelaksana " +
            " from (select ID WOID,Pelaksana from WorkOrder where MONTH(DueDateWO)=" + Bulan + "    and    apv>=2    and   YEAR(DueDateWO)=" + Tahun + " " +
            " and RowStatus > -1 and DeptID_PenerimaWO=" + DeptID2 + " group by ID ,Pelaksana " +
            " union all   " +
            " select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-1)Pelaksana " +
            " from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + "    and YEAR(TglTarget)=" + Tahun + "     and RowStatus > -1 and " +
            " WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID ) as x "+
            " group by woid,pelaksana ) as x1 group by pelaksana ) as x2 " +

            " select * from tempDatattl ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GRetrieveTotalWO_PerHead(sqlDataReader);
                }
            }

           return new WorkOrder_New();
        }

        public WorkOrder_New GRetrieveTotalWO_PerHead(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.TotalWO = Convert.ToInt32(sqlDataReader["TotalWO"]);
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();

            return objWO;
        }

        public WorkOrder_New RetrieveTtlWO_Break(string Bulan, string Tahun, int DeptID, string Tanda, string DeptID3, int StatusReport, string temp)
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
                //" select TotalWO,Pelaksana from ( " +
                //" select COUNT(WOID)TotalWO,Pelaksana from (  select distinct(WOID)WOID,Pelaksana from   ( " +
                //" select ID WOID,Pelaksana from WorkOrder where MONTH(DueDateWO)=" + Bulan + "   and    " + Query1 + "    and   YEAR(DueDateWO)=" + Tahun + "   " +
                //" and RowStatus > -1 and DeptID_PenerimaWO=19 group by ID,Pelaksana  " +
                //" union all   " +
                //" select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-2)Pelaksana " +
                //" from WorkOrder_Target where MONTH(TglTarget)=" + Bulan + "   and YEAR(TglTarget)=" + Tahun + "     and RowStatus > -1 and " +
                //" WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO=" + DeptID2 + " ) group by WOID " +
                //" ) as xx ) as xxx  group by Pelaksana ) as x1 ";
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
            " WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData02 x3 group by x3.Ket,Flag "+

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01]') AND type in (N'U')) DROP TABLE [dbo].[tempData01]  " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02]') AND type in (N'U')) DROP TABLE [dbo].[tempData02]  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GRetrieveTtlWO_Break(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GRetrieveTtlWO_Break(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            //objWO.TotalWO = Convert.ToInt32(sqlDataReader["TotalWO"]);
            objWO.Ket = sqlDataReader["Ket"].ToString();

            return objWO;
        }

        //public WorkOrder_New RetrieveStatusWO(string Bulan, string Tahun, int DeptID)
        //{
        //    string strSQL = string.Empty;
        //    string DeptID2 = string.Empty;
        //    if (DeptID == 4 || DeptID == 5 || DeptID == 18)
        //    {
        //        DeptID2 = "19";
        //    }
        //    else
        //    {
        //        DeptID2 = DeptID.ToString();
        //    }

        //    strSQL = " select ISNULL(Total,0)Total,StatusWO from(select case when StatusWO='Tercapai' then COUNT(StatusWO) end "+
        //             " Total,StatusWO from (select StatusWO from (select case when Apv=5  then 'Tercapai' else 'Tidak Tercapai' end "+
        //             " StatusWO from WorkOrder where MONTH(DueDateWO)=" + Bulan + " and  YEAR(DueDateWO)=" + Tahun + " and DeptID_PenerimaWO="+DeptID2+" " +
        //             " and  ID in (select woid from WorkOrder_LogApproval " +
        //             " where Urutan=5 and RowStatus > -1) and RowStatus > -1 ) as Data1) as Data2  group by StatusWO ) as Data3 ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrWO = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject_RetrieveStatusWO(sqlDataReader);
        //        }
        //    }

        //    return new WorkOrder_New();
        //}

        //public WorkOrder_New GenerateObject_RetrieveStatusWO(SqlDataReader sqlDataReader)
        //{
        //    objWO = new WorkOrder_New();
        //    objWO.Total = Convert.ToInt32(sqlDataReader["Total"]);
        //    objWO.StatusWO = sqlDataReader["StatusWO"].ToString();
            
        //    return objWO;
        //}

        public WorkOrder_New RetrieveStatusWO(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2, string temp)
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
                Query =

                //" select COUNT(WOID)Total  from ( "+
                    //" select woid  from (select ID WOID,Pelaksana from WorkOrder  "+
                    //" where MONTH(DueDateWO)=" + Periode + " and apv>4 and YEAR(DueDateWO)=" + Tahun + "  and RowStatus > -1 and " +
                    //" DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)  group by ID ,Pelaksana  " +
                    //" union all "+
                    //" select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-1)Pelaksana "+
                    //" from WorkOrder_Target where MONTH(TglTarget)=" + Periode + " and YEAR(TglTarget)=" + Tahun + " and RowStatus > -1 and  " +
                    //" WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO in "+
                    //" (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) and apv>4 ) group by WOID ) as x   group by woid,pelaksana  ) as x1 ";

                " select isnull(sum(Ttl),0)Total from ( " +
                " select isnull(COUNT(StatusApv),0)Ttl,StatusApv,Pelaksana  from "+temp+" group by StatusApv,Pelaksana " +
                " ) as x where StatusApv='Closed' ";
                
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
                        " and A.DeptID_Users in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) "+
                        " and MONTH(A.DueDateWO)=" + Periode + " and " +
                        " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " "+
                        " and A.apv>=4 ";
            }

            string strSQL = 
                //" select COUNT(NoWO)Total " +
                //            " from WorkOrder as A  "+
                //            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID "+                            
                            " " + Query + "";            
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

            return new WorkOrder_New();
        }

        public WorkOrder_New RetrieveStatusWO_break(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2, string temp)
        {
            string Query = string.Empty;

            if (Tanda == "1")
            {
                Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                        " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                        " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                        " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4"+

                        " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                        " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                        " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
                
            }
            else if (Tanda == "3")
            {
                Query =
                    //" select WOID,Pelaksana  from (select ID WOID,Pelaksana from WorkOrder "+
                    //" where MONTH(DueDateWO)=" + Periode + " and apv>4 and YEAR(DueDateWO)=" + Tahun + " " +
                    //" and RowStatus > -1 and DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) "+
                    //" group by ID ,Pelaksana  " +
                    //" union all "+
                    //" select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-1)Pelaksana "+
                    //" from WorkOrder_Target where MONTH(TglTarget)="+Periode+" and YEAR(TglTarget)=" + Tahun + " and RowStatus > -1 and  " +
                    //" WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO in "+
                    //" (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) and Apv>4 ) group by WOID ) as x  " +
                    //" group by woid,pelaksana   ) as xx group by xx.Pelaksana "+

                //" select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                    //" FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";

                 " select 'A'flag,TRIM(Pelaksana) +': '+cast(Ttl as varchar) Ket  into tempData01 from ( " +
                 " select isnull(COUNT(StatusApv),0)Ttl,StatusApv,Pelaksana  from "+temp+" group by StatusApv,Pelaksana " +
                 " ) as x where StatusApv='Closed' "+
                 " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                 " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";

            }
            else if (Tanda == "4")
            {
                Query = " where A.RowStatus>-1 and A.ID in (select WOID from WorkOrder_LogApproval  where rowstatus >-1 and Urutan=2 " +
                        " and MONTH(CreatedTime)=" + Periode + " and YEAR(CreatedTime)=" + Tahun + ")  " +
                        " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)" +
                        " and A.DeptID_Users in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) and A.apv>4 "+

                        " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                        " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                        " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
            }
            else if (Tanda == "7")
            {
                Query = " where A.RowStatus>-1 and A.Apv>=4  " +
                        " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptNama + "' and rowstatus > -1) " +
                        " and MONTH(A.DueDateWO)=" + Periode + " and " +
                        " YEAR(DueDateWO)=" + Tahun + " and MONTH(A.FinishDate)<=" + Periode + " and  YEAR(FinishDate)<=" + Tahun + " "+

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
                        " and A.apv>=4 "+

                        " group by Pelaksana,A.ID ) as xxx  group by Pelaksana " +
                        " select DISTINCT Ket = STUFF((SELECT DISTINCT '; ' + Ket " +
                        " FROM tempData01 AS x2  WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') from tempData01 x3 group by x3.Ket,Flag ";
            }

            string strSQL =
                //" select COUNT(NoWO)Total " +
                //            " from WorkOrder as A  " +
                //            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                //            " " + Query + "";

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01]') AND type in (N'U')) DROP TABLE [dbo].[tempData01] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02]') AND type in (N'U')) DROP TABLE [dbo].[tempData02] " +

            //" select 'A'Flag, TRIM(Pelaksana) + ' : ' + cast(COUNT(WOID) as varchar) Ket  into tempData01 from ( " +
            
            //" select A.ID WOID,Pelaksana  from WorkOrder as A   LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
            " " + Query + "";
           

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveStatusWO_brk(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New RetrieveStatusWO_breakPersen(string DeptName, string DeptNama, string Periode, int Tahun, string Tanda, string WaktuSkr, string Periode2, string temp)
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
                //Query = " where A.RowStatus>-1 " +
                //        " and A.DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) " +                    
                //        " and LEFT(convert(char,DueDateWO,112),8)>=LEFT(convert(char,FinishDate,112),8) and " +
                //        " LEFT(convert(char,DueDateWO,112),6)='" + Periode2 + "' and Apv>4 ";

                Query =  
                //" select WOID,Pelaksana  from (select ID WOID,Pelaksana from WorkOrder  where MONTH(DueDateWO)=" + Periode + " and apv>4 and YEAR(DueDateWO)=" + Tahun + " " +
                //" and RowStatus > -1 and DeptID_PenerimaWO in (select ID from dept where alias='" + DeptName + "' and rowstatus > -1)  group by ID ,Pelaksana " +
                //" union all "+
                //" select WOID,(select A.Pelaksana from WorkOrder A where A.ID=WoID and A.RowStatus>-1)Pelaksana "+
                //" from WorkOrder_Target where MONTH(TglTarget)=" + Periode + " and YEAR(TglTarget)=" + Tahun + " and RowStatus > -1 and  " +
                //" WoID in (select ID from WorkOrder where RowStatus>-1 and DeptID_PenerimaWO in "+
                //" (select ID from dept where alias='" + DeptName + "' and rowstatus > -1) and apv>4 ) group by WOID ) as x   group by woid,pelaksana " +
                //" ) as xx group by xx.Pelaksana "+

                " select Ttl Ket,Pelaksana into tempData01A from ( "+
                " select isnull(COUNT(StatusApv),0)Ttl,StatusApv,Pelaksana  from "+temp+" group by StatusApv,Pelaksana "+
                " ) as x where StatusApv='Closed' "+

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
                //" select COUNT(NoWO)Total " +
                //            " from WorkOrder as A  " +
                //            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID " +
                //            " " + Query + "";

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData01A]') AND type in (N'U')) DROP TABLE [dbo].[tempData01A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData02A]') AND type in (N'U')) DROP TABLE [dbo].[tempData02A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData03A]') AND type in (N'U')) DROP TABLE [dbo].[tempData03A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData04A]') AND type in (N'U')) DROP TABLE [dbo].[tempData04A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData05A]') AND type in (N'U')) DROP TABLE [dbo].[tempData05A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData06A]') AND type in (N'U')) DROP TABLE [dbo].[tempData06A] " +

            //"  select cast(COUNT(WOID) as varchar) Ket,Pelaksana  into tempData01A from ( " +

            //" select A.ID WOID,Pelaksana  from WorkOrder as A   LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +

            " " + Query + "";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveStatusWO_brk(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveStatusWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.Total = Convert.ToInt32(sqlDataReader["Total"]);
            //objWO.Ket = sqlDataReader["Ket"].ToString();
            ////objWO.StatusWO = sqlDataReader["StatusWO"].ToString();

            return objWO;
        }

        public WorkOrder_New GenerateObject_RetrieveStatusWO_brk(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();            
            objWO.Ket = sqlDataReader["Ket"].ToString();  
            return objWO;
        }

        public WorkOrder_New RetrieveDeptiD(int ID)
        {
            string StrSql = " select DeptID DeptIDP,Alias from WorkOrder_Dept where ID=" + ID + " and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDeptiD(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveDeptiD(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.DeptIDP = Convert.ToInt32(sqlDataReader["DeptIDP"]);
            objWO.Alias = sqlDataReader["Alias"].ToString();

            return objWO;
        }

        public WorkOrder_New RetrieveDataWO(int ID)
        {
            string StrSql = " select ID WOID,DeptID_PenerimaWO ToDept,(select top 1 DeptID from WorkOrder_ListApvUpdate wl where "+
                            " wl.DeptID=DeptID_PenerimaWO and RowStatus>-1 and StatusApv=3)DeptIDHead,ISNULL(VerISO,'')VerifikasiISO,ISNULL(VerSec,'') "+
                            " VerifikasiSec,ISNULL(ApvOP,'')ApvOP,* from WorkOrder where ID=" + ID + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataWO(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveDataWO(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"]);
            objWO.VerifikasiISO = Convert.ToInt32(sqlDataReader["VerifikasiISO"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objWO.VerifikasiSec = Convert.ToInt32(sqlDataReader["VerifikasiSec"]);
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.ApvOP = Convert.ToInt32(sqlDataReader["ApvOP"]);

            return objWO;
        }

        public int RetrieveDeptIDHead(int UserID)
        {
            string StrSql = " select DeptID from WorkOrder_ListApvUpdate where UserID="+UserID+" and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["DeptID"]);
                }
            }

            return 0;
        }

        public WorkOrder_New RetrieveSPM(int WOID)
        {
            string StrSql = " select ID WOID,DeptID_Users FromDeptName,DeptID_PenerimaWO ToDept,Apv,AreaWO,ISNULL(VerISO,0) VerifikasiISO,ISNULL(ApvOP,0)ApvOP,* from WorkOrder where ID=" + WOID + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveSPM(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveSPM(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objWO.FromDeptName = sqlDataReader["FromDeptName"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.VerifikasiISO = Convert.ToInt32(sqlDataReader["VerifikasiISO"]);
            objWO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objWO.CreatedTime = sqlDataReader["CreatedTime"].ToString();
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objWO.ApvOP = Convert.ToInt32(sqlDataReader["ApvOP"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();

            return objWO;
        }

        public string RetrieveDeptName(string DeptID1)
        {
            string DeptID = string.Empty;
            if (DeptID1 == "25")
            {
                DeptID = "19";
            }
            else
            {
                DeptID = DeptID1;
            }

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

        public string RetrieveDeptNameSub(string UserID)
        {
            string DeptID = string.Empty;
            //if (DeptID1 == "25")
            //{
            //    DeptID = "19";
            //}
            //else
            //{
            //    DeptID = DeptID1;
            //}

            string result = "0";
            string StrSql = " select 'HRD'+' '+'-'+' '+NamaSubDept DeptName from WorkOrder_SubDept where UserID=" + UserID + " or HeadID=" + UserID + " and RowStatus>-1 ";
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

        public WorkOrder_New RetrieveUserLevelNew(int ID)
        {
            //string StrSql = " select StatusApv, DeptID DeptIDHead from WorkOrder_ListApvUpdate where UserID=" + ID + " and RowStatus > -1 ";
            string StrSql = " select sum(statusapv)StatusApv,SUM(DeptIDHead)DeptIDHead from (select  '0'StatusApv,'0'DeptIDHead " +
                            " from WorkOrder_ListApvUpdate union all " +
                            " select StatusApv,DeptID DeptIDHead from WorkOrder_ListApvUpdate where UserID="+ID+" and RowStatus > -1  ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveUserLevelNew(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveUserLevelNew(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.DeptIDHead = Convert.ToInt32(sqlDataReader["DeptIDHead"]);
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();
            
            return objWO;
        }

        public ArrayList RetrieveHead()
        {
            ArrayList arrData = new ArrayList();
            string strSQL = " select ID HeadID,UserID,NamaHead HeadName,DeptID from WorkOrder_HeadName where RowStatus > -1 ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        HeadID = Convert.ToInt32(sdr["HeadID"].ToString()),
                        DeptID = Convert.ToInt32(sdr["DeptID"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        HeadName = sdr["HeadName"].ToString()
                    });
                }
            }
            return arrData;
        }

        public string RetrievePelaksana(int UserID)
        {
            string result = "0";
            string StrSql = " select NamaHead Pelaksana from WorkOrder_HeadName where RowStatus > -1 and UserID="+UserID+"";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["Pelaksana"].ToString();
                }
            }

            return result;
        }

        //public int RetrievePlantID(int ID)
        //{
        //    string StrSql = " select PlantID from WorkOrder where ID=" + ID + " and RowStatus > -1 ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return Convert.ToInt32(sqlDataReader["PlantID"]);
        //        }
        //    }

        //    return 0;
        //}

        public WorkOrder_New RetrievePlantID(int ID)
        {
            string strSQL = string.Empty;


            strSQL = " select ID WOID,PlantID,DeptID_PenerimaWO ToDept,AreaWO from WorkOrder where ID=" + ID + " and RowStatus > -1 ";                         
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrievePlantID(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrievePlantID(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();

            return objWO;
        }

        public WorkOrder_New RetrieveLampiran(int ID)
        {
            string strSQL = string.Empty;


            strSQL = " select FileLampiran FileName from WorkOrder_Lampiran where RowStatus>-1 and WOID=" + ID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveLampiran(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveLampiran(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();            
            objWO.FileName = sqlDataReader["FileName"].ToString();
            //objWO.FileLampiranOP = (byte[])sqlDataReader["FileLampiranOP"]; 

            return objWO;
        }

        //public ArrayList RetrieveOpenWO(int UserID, int DeptID) // WO Keluar ( IT - HRD - MTC )
        //{
        //    string Query1 = string.Empty;
        //    string Query2 = string.Empty;
        //    string Query3 = string.Empty;

        //    if (DeptID == 14)
        //    {
        //        Query1 = " and ((Apv in (0,4) and DeptID_Users=14)  or (Apv=4 and DeptID_PenerimaWO=14) or (Apv=2 and DeptID_PenerimaWO=14))) ";
        //        Query2 = " 14 ";
        //        Query3 = " case when DeptID_PenerimaWO = " + Query2 + " and Apv = 2 then 'Status Open - Last Approved PM' when DeptID_PenerimaWO <> " + Query2 + " and Apv = 0 then 'Status Open' " +
        //                 " when DeptID_PenerimaWO <> " + Query2 + " and Apv = 4 then 'Status Finish - Next Serah Terima' end StatusApv ";
        //    }
        //    else if (DeptID == 7)
        //    {
        //        Query1 = " and ((Apv in (0,4) and DeptID_Users=7)  or (Apv=4 and DeptID_PenerimaWO<>7 DeptID_Users=7) or (Apv=2 and DeptID_PenerimaWO=7))) ";
        //        Query2 = " 7 ";
        //        Query3 = " case when DeptID_PenerimaWO = " + Query2 + " and Apv = 2 then 'Status Open - Last Approved PM' when DeptID_PenerimaWO <> " + Query2 + " and Apv = 0 then 'Status Open' " +
        //                 " when DeptID_PenerimaWO <> " + Query2 + " and Apv = 4 then 'Status Finish - Next Serah Terima' end StatusApv ";

        //    }
        //    else if (DeptID == 25 || DeptID == 19)
        //    {
        //        Query1 = " and ((Apv=0 and DeptID_Users=19)  or (Apv=4 and DeptID_PenerimaWO<>19 and DeptID_Users=19) or (Apv=2 and DeptID_PenerimaWO=19))) ";
        //        Query2 = " 19 ";
        //        Query3 = " case when DeptID_PenerimaWO = " + Query2 + " and Apv = 2 and VerSec = 1 then 'Status Open - Last Approved Security' " +
        //                 " when DeptID_PenerimaWO = " + Query2 + " and Apv = 2 and VerSec IS NULL then 'Status Open - Last Approved PM' " +
        //                 " when DeptID_PenerimaWO <> " + Query2 + " and Apv = 0 then 'Status Open'  when DeptID_PenerimaWO <> " + Query2 + " and Apv = 4 then 'Status Finish - Next Serah Terima' end StatusApv ";
        //                        }

        //    string strQuery = " select ID,NoWO,CreatedBy,StatusWO,case when DeptID_PenerimaWO <> " + Query2 + " then DepTujuan else '' end ToDeptName, " +
        //                      " " + Query3 + " "+ 
        //                      " from (select ID,NoWO,CreatedBy,case when DeptID_PenerimaWO <> " + Query2 + " then 'WO Keluar' when DeptID_PenerimaWO = " + Query2 + " and Apv=2 then 'WO Masuk' end StatusWO, " +
        //                      " (select Alias from Dept D where D.ID=DeptID_PenerimaWO and RowStatus>-1)DepTujuan,DeptID_PenerimaWO,Apv,VerSec from WorkOrder " +
        //                      " where (DeptID_Users = " +
        //                      " (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") or  DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + "))  " +
        //                      " and RowStatus > -1 " + Query1 + " as Data1 order by StatusWO";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
        //    arrWO = new ArrayList();
        //    if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrWO.Add(new WorkOrder_New
        //            {
        //                ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
        //                NoWO = sqlDataReader["NoWO"].ToString(),
        //                CreatedBy = sqlDataReader["CreatedBy"].ToString(),
        //                ToDeptName = sqlDataReader["CreatedBy"].ToString(),
        //                StatusWO = sqlDataReader["StatusWO"].ToString(),
        //                StatusApv = sqlDataReader["StatusApv"].ToString()
        //            });
        //        }
        //    }
        //    return arrWO;
        //}

        public ArrayList RetrieveOpenWO(int UserID, int DeptID, int StsApv, int Apv)
        {
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Query3 = string.Empty;
            string Query4 = string.Empty;

            if (StsApv == 3)
            {
                if (DeptID == 19)
                {
                    Query1 =
                    // Hitung Total WO Keluar
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from (select case when StatusWO='WO Keluar' then COUNT(StatusWO) else 0  end Ket, 0 Ket2,0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Keluar' StatusWO from WorkOrder " +
                    " where DeptID_Users = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=0 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query2 =
                    // Hitung Total WO Masuk
                    " select 0 Ket,case when StatusWO='WO Masuk' then COUNT(StatusWO) else 0  end Ket2, 0 Ket3,0 Ket4  " +
                    " from (select ID,NoWO,CreatedBy,'WO Masuk' StatusWO from WorkOrder " +
                    " where " +
                    " (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") " +
                    " and Apv=2 and pelaksana is not NULL and DueDate is not NULL and AreaWO<>'Kendaraan' and RowStatus>-1) " +
                    " or " +
                    " (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") " +
                    " and Apv=2 and pelaksana is not NULL and DueDate is not NULL and DeptID_User=26 and VerSec=1 and AreaWO='Kendaraan' and RowStatus>-1) " +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query3 = 
                    // Hitung Total WO Finish
                    " select 0 Ket,0 Ket2, case when StatusWO='WO Finish' then COUNT(StatusWO) else 0  end Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Finish' StatusWO from WorkOrder " +
                    " where DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=4 and RowStatus>-1) " +
                    " as xx group by xx.StatusWO ) as Data1 ";
                    Query4 = "";
                    // Hitung Total WO Update
                       
                }
                else if (DeptID == 14)
                {
                    Query1 =
                    // Hitung Total WO Keluar
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from (select case when StatusWO='WO Keluar' then COUNT(StatusWO) else 0  end Ket, 0 Ket2,0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Keluar' StatusWO from WorkOrder " +
                    " where DeptID_Users = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=0 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query2 =
                    // Hitung Total WO Masuk
                    " select 0 Ket,case when StatusWO='WO Masuk' then COUNT(StatusWO) else 0  end Ket2, 0 Ket3,0 Ket4 "+
                    " from (select ID,NoWO,CreatedBy,'WO Masuk' StatusWO from WorkOrder "+
                    " where (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") " +
                    " and Apv=2 and AreaWO='HardWare' and RowStatus>-1) " +
                    " or " +
                    " (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") " +
                    " and Apv=2 and AreaWO='SoftWare' and RowStatus>-1) and VerISO=1 and ApvOP in (2,-1,-2) " +
                    " ) as xx group by xx.StatusWO UNION ALL ";                    
                    Query3 = 
                    // Hitung Total WO Finish
                    " select 0 Ket,0 Ket2, case when StatusWO='WO Finish' then COUNT(StatusWO) else 0  end Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Finish' StatusWO from WorkOrder " +
                    " where DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=4 and RowStatus>-1) " +
                    " as xx group by xx.StatusWO ) as Data1 ";
                    Query4 = "";
                    // Hitung Total WO Update
                    
                }
                else if (DeptID == 7)
                {
                    Query1 =
                    // Hitung Total WO Keluar
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from (select case when StatusWO='WO Keluar' then COUNT(StatusWO) else 0  end Ket, 0 Ket2,0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Keluar' StatusWO from WorkOrder " +
                    " where DeptID_Users = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=0 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query2 =
                    // Hitung Total WO Masuk
                    " select 0 Ket,case when StatusWO='WO Masuk' then COUNT(StatusWO) else 0  end Ket2, 0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Masuk' StatusWO from WorkOrder " +
                    " where (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") " +
                    " and Apv=2 and RowStatus>-1) " +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query3 =
                    // Hitung Total WO Finish
                    " select 0 Ket,0 Ket2, case when StatusWO='WO Finish' then COUNT(StatusWO) else 0  end Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Finish' StatusWO from WorkOrder " +
                    " where DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=3 and UserID=" + UserID + ") and Apv=4 and RowStatus>-1) " +
                    " as xx group by xx.StatusWO ) as Data1 ";
                    Query4 = "";
                    // Hitung Total WO Update
                        
                }
            }
             else if (StsApv == 8)
            {
                    Query1 = "";
                    // Hitung Total WO Keluar
                    Query2 = "";
                    // Hitung Total WO Masuk                    
                    Query3 = "";
                    // Hitung Total WO Finish
                    Query4 = 
                    // Hitung Total WO Update
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from ( " +
                    " select 0 Ket,0 Ket2, 0 Ket3,case when StatusWO='WO Update' then COUNT(StatusWO) else 0  end Ket4  " +
                    " from (select ID,NoWO,CreatedBy,'WO Update' StatusWO from WorkOrder " +
                    " where (DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=8 and UserID=" + UserID + ") " +
                    " and Apv=2 and pelaksana is not NULL and DueDate is NULL and RowStatus>-1) " +
                    " ) as xx group by xx.StatusWO) as Data1 ";

            }
             else if (StsApv == 9)
            {
                Query1 = "";
                // Hitung Total WO Keluar
                
                Query2 = "";

                Query3 =
                // Hitung Total WO Finish 
                " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                " from ( " +
                " select 0 Ket,0 Ket2,case when StatusWO='WO Finish' then COUNT(StatusWO) else 0  end Ket3,0 Ket4 " +
                " from (select ID,NoWO,CreatedBy,'WO Finish' StatusWO from WorkOrder " +
                " where DeptID_PenerimaWO = (select DeptID from WorkOrder_ListApvUpdate where RowStatus > -1 and  StatusApv=9 and UserID=" + UserID + ") " +
                " and Apv=3  and Pelaksana is not null and DueDateWO is not null and RowStatus>-1 " +
                " ) as xx group by xx.StatusWO) as Data1 ";
                    Query4 = "";
                    // Hitung Total WO Update
                    
            
            }
             else if (StsApv == 2)
            {
                    Query1 =
                    // Hitung Total WO Keluar
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from (select 0 Ket,case when StatusWO='WO Masuk' then COUNT(StatusWO) else 0  end Ket2,0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Masuk' StatusWO from WorkOrder " +
                    " where DeptID_PenerimaWO=14 and Apv=1 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO) as Data1";
                    Query2 = "";
                    // Hitung Total WO Masuk
                    Query3 = "";
                    // Hitung Total WO Finish
                    Query4 = "";
                    // Hitung Total WO Update

            }
             else if (StsApv == 0 && Apv > 0)
            {
                    Query1 =
                    // Hitung Total WO Keluar
                    " select ISNULL(SUM(ket),'')WOKeluar,ISNULL(SUM(Ket2),'')WOMasuk,ISNULL(SUM(ket3),'')WOFinish,ISNULL(SUM(ket4),'')WOUpdate " +
                    " from (select case when StatusWO='WO Keluar' then COUNT(StatusWO) else 0  end Ket, 0 Ket2,0 Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Keluar' StatusWO from WorkOrder " +
                    " where DeptID_Users = (select DeptID from Users where RowStatus > -1 and ID=" + UserID + ") and Apv=0 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO UNION ALL ";
                    Query2 = "";
                    // Hitung Total WO Masuk
                    Query3 = 
                    // Hitung Total WO Finish
                    " select 0 Ket,0 Ket2, case when StatusWO='WO Finish' then COUNT(StatusWO) else 0  end Ket3,0 Ket4 " +
                    " from (select ID,NoWO,CreatedBy,'WO Finish' StatusWO from WorkOrder " +
                    " where DeptID_Users = (select DeptID from Users where RowStatus > -1 and ID=" + UserID + ") and Apv=4 and RowStatus>-1" +
                    " ) as xx group by xx.StatusWO ) as Data1 ";
                    Query4 = "";
                    // Hitung Total WO Update
                    
            }
                           
            string strQuery = Query1 + Query2 + Query3 + Query4;
              
            

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrWO = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(new WorkOrder_New
                    {
                        WOKeluar = Convert.ToInt32(sqlDataReader["WOKeluar"].ToString()),
                        WOMasuk = Convert.ToInt32(sqlDataReader["WOMasuk"].ToString()),
                        WOFinish = Convert.ToInt32(sqlDataReader["WOFinish"].ToString()),
                        WOUpdate = Convert.ToInt32(sqlDataReader["WOUpdate"].ToString())

                    });
                }
            }
            return arrWO;
        }

        public ArrayList GetPermintaan(string Permintaan)
        {
            string strQuery = " select ID,permintaan from WorkOrder_permintaan where rowstatus>-1 and tipe='"+Permintaan+"' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrWO = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(new WorkOrder_New
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        Permintaan = sqlDataReader["Permintaan"].ToString(),                        
                    });
                }
            }
            return arrWO;
        }

        public WorkOrder_New RetrieveDataEmail(int DeptID, int UserID, int StsApv, int PlantID, int PlantIDOP, int VerISO, int ApvOP, int FromDeptID)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;

            if (VerISO == 0 || VerISO == 1 && ApvOP == 2)
            {
                if (DeptID == 7 && StsApv != 2 || DeptID == 14 && StsApv != 2 || DeptID == 19 && StsApv != 2)
                {
                    Query1 = " select Data1.DeptID,Data1.UserID,A.usrmail AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                           " where DeptID=" + DeptID + " and StatusApv=3) as Data1 LEFT JOIN Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
                }
                else if (DeptID != 7 && StsApv != 2 && StsApv != 10 || DeptID != 14 && StsApv != 2 && StsApv != 10 || DeptID != 19 && StsApv != 2 && StsApv != 10)
                {
                    Query1 = " select DISTINCT(MgrID)UserID,(select DeptID from Users A where A.ID=MgrID and RowStatus > -1)DeptID,(select B.usrmail " +
                             " from Users B where B.ID=MgrID and RowStatus>-1)AccountEmail from ListUserHead where RowStatus>-1 and UserID in " +
                             " (select ID from Users where DeptID=" + DeptID + " and RowStatus>-1) ";
                }
                else if (StsApv == 2 || StsApv == 10)
                {
                    Query1 = " select Data1.DeptID,Data1.UserID,A.usrmail AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                           " where DeptID=" + DeptID + " and StatusApv=" + StsApv + ") as Data1 LEFT JOIN Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
                }
            }
            else if (VerISO == 1 && ApvOP == 0)
            {
                string IP1_Ctrp = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string IP2_Krwg = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                if (PlantIDOP == 1)
                {
                    Query1 = " select Data1.UserID,A.usrmail AccountEmail from (select UserID from " + IP2_Krwg + "WorkOrder_ListApvUpdate " +
                             " where statusapv=3 and  userid in (select ID from " + IP2_Krwg + "Users where DeptID=" + FromDeptID + " )) as Data1 " +
                             " LEFT JOIN " + IP2_Krwg + "Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
                }
                else if (PlantIDOP == 7)
                {
                    Query1 = " select Data1.UserID,A.usrmail AccountEmail from (select UserID from " + IP1_Ctrp + "WorkOrder_ListApvUpdate " +
                             " where statusapv=3 and userid in (select ID from " + IP1_Ctrp + "Users where DeptID=" + DeptID + " )) as Data1 " +
                             " LEFT JOIN " + IP1_Ctrp + "Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
                }

                
            }

            strSQL = Query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataEmail(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public ArrayList RetrieveDataEmailArray(int DeptID, int UserID, int StsApv, int PlantID, int PlantIDOP, int VerISO, int ApvOP, int FromDeptID, string AreaWO, int ToDeptID)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;

            if (VerISO == 0 || VerISO == 1 && ApvOP == 2)
            {
                if (StsApv == 12)
                {
                    Query1 = " select distinct Data1.DeptID,A.AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                           " where DeptID=14 and StatusApv=3) as Data1 LEFT JOIN WorkOrder_AccountEmail as A ON Data1.DeptID=A.DeptID where A.RowStatus>-1 ";
                }
                //else if (StsApv == 13)
                //{
                //    Query1 = " select Data1.DeptID,Data1.UserID,A.usrmail AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                //           " where DeptID=14 and StatusApv=3) as Data1 LEFT JOIN Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
                //}
                else if (DeptID == 7 && StsApv != 2 || DeptID == 14 && StsApv != 2 || DeptID == 19 && StsApv != 2)
                {
                    Query1 = " select distinct Data1.DeptID,A.AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                           " where DeptID=" + DeptID + " and StatusApv=3) as Data1 LEFT JOIN WorkOrder_AccountEmail as A ON Data1.DeptID=A.DeptID where A.RowStatus>-1 ";
                }
                else if ( DeptID != 7 && StsApv != 2 && StsApv != 10 
                       || DeptID != 14 && StsApv != 2 && StsApv != 10 
                       || DeptID != 19 && StsApv != 2 && StsApv != 10)
                {
                    Query1 = " select DISTINCT(MgrID)UserID,(select DeptID from Users A where A.ID=MgrID and RowStatus > -1)DeptID,(select B.usrmail " +
                             " from Users B where B.ID=MgrID and RowStatus>-1)AccountEmail from ListUserHead where RowStatus>-1 and UserID in " +
                             " (select ID from Users where DeptID=" + DeptID + " and RowStatus>-1) ";
                }
                
               
                // 1.Approved WO IT - SoftWare oleh Manager Peminta dibawah PM  <<Next>> Kirim Email MgrISO
                else if (StsApv == 2 && VerISO == 0 && ApvOP == 0 && AreaWO == "SoftWare" && ToDeptID == 14)
                {
                    //Query1 = " select DISTINCT Data1.DeptID,A.AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                    //       " where StatusApv=" + StsApv + ") as Data1 LEFT JOIN WorkOrder_AccountEmail as A ON Data1.DeptID=A.DeptID where A.RowStatus>-1 ";
                    Query1 =
                    "  select AccountEmail from WorkOrder_AccountEmail where deptid=23 and RowStatus>-1 ";
                }

                // 1.Approved WO IT - SoftWare oleh PM <<Next>> Kirim Email ke Head ISO - Test OK
                //else if (StsApv == 10)
                //{
                //    Query1 = " select DISTINCT Data1.DeptID,A.AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                //           " where StatusApv=" + StsApv + ") as Data1 LEFT JOIN WorkOrder_AccountEmail as A ON Data1.DeptID=A.DeptID where A.RowStatus>-1 ";
                //}
            }

            // Approved WO IT - SoftWare oleh Head ISO Next >> Kirim Email ke Manager Peminta Lintas Plant
            else if (VerISO == 1 && ApvOP == 0)
            {
                string IP1_Ctrp = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string IP2_Krwg = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                if (PlantIDOP == 1)
                {                   
                    Query1 =
                    " select distinct AccountEmail from ( " +
                    " select x.*,x1.DeptID from ( " +
                    " select UserID from " + IP2_Krwg + "WorkOrder_ListApv  " +
                    " where status in (1,3) and  userid in (select ID from " + IP2_Krwg + "Users where DeptID='" + FromDeptID + "' )) as x " +
                    " inner join " + IP2_Krwg + "Users x1 ON x1.ID=x.UserID ) as a " +
                    " inner join  " + IP2_Krwg + "WorkOrder_AccountEmail b ON a.DeptID=b.DeptID ";
                }
                else if (PlantIDOP == 7)
                {                    
                    Query1 =
                    " select distinct AccountEmail from ( " +
                    " select x.*,x1.DeptID from ( " +
                    " select UserID from " + IP1_Ctrp + "WorkOrder_ListApv  " +
                    " where status in (1,3) and  userid in (select ID from " + IP1_Ctrp + "Users where DeptID='" + FromDeptID + "' )) as x " +
                    " inner join " + IP1_Ctrp + "Users x1 ON x1.ID=x.UserID ) as a " +
                    " inner join  " + IP1_Ctrp + "WorkOrder_AccountEmail b ON a.DeptID=b.DeptID ";
                }
            }
          
            // Approved WO IT - SoftWare oleh Manager Plant Terkait Next >> Kirim Email ke Manager IT (Pak Sodik)
            else if (VerISO == 1 && ApvOP == 1)
            {
                string IP1_Ctrp = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                string IP2_Krwg = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                if (PlantIDOP == 1)
                {
                    Query1 =
                    " select distinct AccountEmail from ( " +
                    " select x.*,x1.DeptID from ( " +
                    " select UserID from " + IP2_Krwg + "WorkOrder_ListApv  " +
                    " where status in (1,3) and  userid in (select ID from " + IP2_Krwg + "Users where DeptID='" + ToDeptID + "' )) as x " +
                    " inner join " + IP2_Krwg + "Users x1 ON x1.ID=x.UserID ) as a " +
                    " inner join  " + IP2_Krwg + "WorkOrder_AccountEmail b ON a.DeptID=b.DeptID ";
                }
                else if (PlantIDOP == 7)
                {
                    Query1 =
                    " select distinct AccountEmail from ( " +
                    " select x.*,x1.DeptID from ( " +
                    " select UserID from " + IP1_Ctrp + "WorkOrder_ListApv  " +
                    " where status in (1,3) and  userid in (select ID from " + IP1_Ctrp + "Users where DeptID='" + ToDeptID + "' )) as x " +
                    " inner join " + IP1_Ctrp + "Users x1 ON x1.ID=x.UserID ) as a " +
                    " inner join  " + IP1_Ctrp + "WorkOrder_AccountEmail b ON a.DeptID=b.DeptID ";
                }
            }

            strSQL = Query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObject_RetrieveDataEmail(sqlDataReader));
                }
            }
            else
                arrWO.Add(new WorkOrder_New());

            return arrWO;
        }

        public string RetrieveDataEmailUser(string NoWO)
        {
            string result = "0";
            string StrSql = 
            " select AccountEmail from WorkOrder_AccountEmail where DeptID in (select DeptID_Users from WorkOrder where NoWO='"+NoWO+"' and RowStatus=-2) "+
            " and Prioritas=1 and TipeKirim='TO' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["AccountEmail"].ToString();
                }
            }

            return result;
        }

        public string RetrieveDataEmailUserCC(string NoWO)
        {
            string result = "0";
            string StrSql =
            " select AccountEmail from ( " +
            " select AccountEmail,Prioritas,Urutan from ( " +
            " select * from WorkOrder_AccountEmail where DeptID in (select DeptID_Users from WorkOrder where NoWO='" + NoWO + "' and RowStatus=-2) " +
            " and RowStatus>-1 and TipeKirim='CC' " +
            " union all " +
            " select * from WorkOrder_AccountEmail where DeptID in (select DeptID_PenerimaWO from WorkOrder where NoWO='" + NoWO + "' and RowStatus=-2) " +
            " and RowStatus>-1 and TipeKirim='CC' " +
            " ) as x group by AccountEmail,Prioritas,Urutan  " +
            " ) as xx order by Prioritas,Urutan ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["AccountEmail"].ToString();
                }
            }

            return result;
        }

        public WorkOrder_New RetrieveDataEmailPasInput(int DeptID, int UserID, int StsApv)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;

            if (DeptID == 7 && StsApv != 2 && StsApv != 100 
                || DeptID == 14 && StsApv != 2 && StsApv != 100 
                || DeptID == 19 && StsApv != 2 && StsApv != 100)
            {
                Query1 = " select Data1.DeptID,Data1.UserID,A.usrmail AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                       " where DeptID=" + DeptID + " and StatusApv=3) as Data1 LEFT JOIN Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
            }
            else if (DeptID != 7 && StsApv != 2 && StsApv != 10 && StsApv != 100
                || DeptID != 14 && StsApv != 2 && StsApv != 10 && StsApv != 100
                || DeptID != 19 && StsApv != 2 && StsApv != 10 && StsApv != 100)
            {
                Query1 = " select DISTINCT(MgrID)UserID,(select DeptID from Users A where A.ID=MgrID and RowStatus > -1)DeptID,(select B.usrmail " +
                         " from Users B where B.ID=MgrID and RowStatus>-1)AccountEmail from ListUserHead where RowStatus>-1 and UserID in " +
                         " (select ID from Users where DeptID=" + DeptID + " and RowStatus>-1) ";
            }
            else if (StsApv == 2 || StsApv == 10)
            {
                Query1 = " select Data1.DeptID,Data1.UserID,A.usrmail AccountEmail from (select DeptID,UserID from WorkOrder_ListApvUpdate " +
                       " where DeptID=" + DeptID + " and StatusApv=" + StsApv + ") as Data1 LEFT JOIN Users as A ON Data1.UserID=A.ID where A.RowStatus>-1 ";
            }
            else if (StsApv ==100)
            {
                Query1 = " select usrmail AccountEmail from Users where ID=" + UserID + " and rowstatus>-1 ";
            } 

            strSQL = Query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataEmail(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveDataEmail(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.AccountEmail = sqlDataReader["AccountEmail"].ToString(); 
            return objWO;
        }

        public WorkOrder_New RetrieveData(string NoWO)
        {
            string strSQL = string.Empty;
            //strSQL = " select DeptID_PenerimaWO ToDept,DeptID_Users,ISNULL(ApvOP,0)ApvOP,ISNULL(VerISO,0)VerISO,ISNULL(VerSec,0)VerifikasiSec, "+
            //         " PlantID from workorder where nowo='" + NoWO + "' and rowstatus>-1";
            strSQL = " select DeptID_PenerimaWO ToDept,DeptID_Users,ISNULL(ApvOP,0)OP,ISNULL(ApvOP,0)ApvOP,ISNULL(VerISO,0)VerISO,ISNULL(VerSec,0)VerifikasiSec, " +
                     " PlantID,isnull(AlasanCancel,'')AlasanCancel from workorder where nowo='" + NoWO + "' and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveData(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveData(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"].ToString());
            objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"].ToString());
            objWO.ApvOP = Convert.ToInt32(sqlDataReader["ApvOP"].ToString());
            objWO.VerISO = Convert.ToInt32(sqlDataReader["VerISO"].ToString());
            objWO.VerifikasiSec = Convert.ToInt32(sqlDataReader["VerifikasiSec"].ToString());
            objWO.PlantID = Convert.ToInt32(sqlDataReader["PlantID"].ToString());
            objWO.AlasanCancel = sqlDataReader["AlasanCancel"].ToString();
            return objWO;
        }

        public WorkOrder_New CekNamaDeptPembuat(int ID)
        {
            string strSQL = string.Empty;
            strSQL = " select alias NamaDept from dept where id="+ID+" and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_CekNamaDeptPembuat(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_CekNamaDeptPembuat(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.NamaDept = sqlDataReader["NamaDept"].ToString();            
            return objWO;
        }

        public WorkOrder_New RetrieveDeptiDUsers(int ID)
        {
            string StrSql = " select DeptID DeptIDP,Alias from WorkOrder_Dept where DeptID=" + ID + " and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDeptiD(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        

        public string Retrieve_Corporate()
        {
            string result = string.Empty;
            string StrSql = " select deptiD Corporate from WorkOrder_DeptStatus where rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["Corporate"].ToString();
                }
            }

            return result;
        }

        public ArrayList RetrieveSerahTerimaWO()
        {
            string strQuery =
            " select A.ID WOID,A.DeptID_PenerimaWO ToDept,A.AreaWO,LEFT(convert(char,A.FinishDate,112),8)TglSelesaiWO " +
            " from WorkOrder A " +
            " inner join WorkOrder_LogApproval B ON A.ID=B.WOID " +
            " where ("+
            " (A.RowStatus>-1 and B.RowStatus>-1 and B.Urutan=4 and A.Apv=4 and A.DeptID_PenerimaWO<>14) or  " +
            " (A.RowStatus>-1 and B.RowStatus>-1 and B.Urutan=4 and A.Apv=4 and A.DeptID_PenerimaWO=14 and A.AreaWO='HardWare') or " +
            " (A.RowStatus>-1 and B.RowStatus>-1 and B.Urutan=5 and A.Apv=4 and A.DeptID_PenerimaWO=14 and A.AreaWO='SoftWare')"+
            " ) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrWO = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(new WorkOrder_New
                    {
                        WOID = Convert.ToInt32(sqlDataReader["WOID"].ToString()),
                        TglSelesaiWO = sqlDataReader["TglSelesaiWO"].ToString(),
                        ToDept = Convert.ToInt32(sqlDataReader["ToDept"].ToString()),
                        AreaWO = sqlDataReader["AreaWO"].ToString(),
                    });
                }
            }
            return arrWO;
        }

        public int CekRange(string Tgl1, string Tgl2)
        {
            string strSQL = string.Empty;
            strSQL =
            " declare @d1 datetime, @d2 datetime " +
            " select @d1 = '" + Tgl1 + "' ,  @d2 = '" + Tgl2 + "' " +
            " select SUM(Selisih)Selisih from  (select datediff(dd, @d1, @d2) - case when DATEPART(dw,@d2)=7 then 1 when DATEPART(dw,@d2)=1 then 1 else 0 end " +
            " - (datediff(wk, @d1, @d2) * 2) - " +
            " case when datepart(dw, @d1) = 1 then 1 else 0 end + " +
            " case when datepart(dw, @d2) = 1 then 1 else 0 end Selisih " +
            " union all " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=@d1 and " +
            " LEFT(convert(char,harilibur,112),8)<=@d2) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Selisih"]);
                }
            }

            return 0;
        }

        public int RetrieveUserIDMgr(int DeptID)
        {
            string strSQL = string.Empty;
            strSQL =
            " select Apv1 ApvMgr from ISO_UPDListApv where UserID in (select ID from Users where DeptID="+DeptID+" and RowStatus>-1) group by Apv1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ApvMgr"]);
                }
            }

            return 0;
        }

        public int cekShare(int DeptID, int UnitKerjaID)
        {

            string StrSql = " select sum(TotalShare)TotalShare from (select COUNT(ID)TotalShare from WorkOrder  " +
                            " where DeptID_Users=" + DeptID + " and RowStatus>-1 and ApvOP=0 and PlantID<>" + UnitKerjaID + " " +
                            " union all " +
                            " select '0'TotalShare from WorkOrder ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["TotalShare"]);
                }
            }

            return 0;
        }

        public int RetrieveSub(int ID)
        {

            string StrSql = " select count(ID)Total from WorkOrder_SubDept where RowStatus>-1 and UserID="+ID+" ";
                            //" where DeptID_Users=" + DeptID + " and RowStatus>-1 and ApvOP=0 and PlantID<>" + UnitKerjaID + " " +
                            //" union all " +
                            //" select '0'TotalShare from WorkOrder ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Total"]);
                }
            }

            return 0;
        }

        public string RetrieveNamaSub(int ID)
        {
            string result = string.Empty;
            string StrSql = " select NamaSubDept from WorkOrder_SubDept where RowStatus>-1 and (UserID="+ID+" or HeadID="+ID+") ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["NamaSubDept"].ToString();
                }
            }

            return result;
        }
        public int RetrieveStsUser(int UserID)
        {

            string StrSql =
            " select SUM(ID)ID from (select ID from WorkOrder_SubDept where UserID=" + UserID + "  and RowStatus>-1 " +
            " union all  " +
            " select ID from WorkOrder_SubDept where (HeadID=" + UserID + " or UserID=" + UserID + ") and RowStatus>-1 " +
            " union all  " +
            " select '0'ID ) as xx ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public WorkOrder_New RetrieveOutWO2Day(int UserID)
        {
            string StrSql =
            " select COUNT(Selisih)Total from ( "+
            " select xx.NoWO,xx.UraianPekerjaan,xx.DeptID_Users,xx.Pelaksana,LEFT(convert(char,xx.DueDateWO,112),11)WaktuDateLine," +
            " LEFT(convert(char,GETDATE(),112),11)WaktuNow,CountDown Selisih from (select DateDiff (DAY,GETDATE(),DueDateWO)CountDown,* " +
            " from WorkOrder where DeptID_PenerimaWO=19 and (FinishDate is null or FinishDate='')  and RowStatus>-1 and "+
            " (DueDateWO is not null or DueDateWO='')) as xx where xx.Pelaksana in (select NamaHead from WorkOrder_HeadName where "+
            " UserID=" + UserID + " and RowStatus>-1) and CountDown >=0 and CountDown <= 2 ) as xx1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_OutWO2Day(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_OutWO2Day(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            //objWO.NoWO = sqlDataReader["NoWO"].ToString();
            //objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            //objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"].ToString());
            //objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            //objWO.WaktuDateLine = sqlDataReader["WaktuDateLine"].ToString();
            //objWO.WaktuNow = sqlDataReader["WaktuNow"].ToString();
            //objWO.Selisih = Convert.ToInt32(sqlDataReader["Selisih"].ToString());
            objWO.Total = Convert.ToInt32(sqlDataReader["Total"].ToString());
            return objWO;
        }

        public ArrayList RetrieveOutWO(int DeptID, string Flagging)
        {
            string query = string.Empty;
            ArrayList arrData = new ArrayList();

            if (DeptID == 23 && Flagging == "5" || DeptID == 7 && Flagging == "5")
            { query = ""; }
            else if (DeptID == 25 || DeptID == 18 || DeptID == 4 || DeptID == 5)    
            { query = " DeptID_PenerimaWO=19 and "; }
            else
            { query = " DeptID_PenerimaWO=" + DeptID + " and "; }

           string strSQL =
            //" with DataAwal as  (select ROW_NUMBER() over (order by DeptID_Users,DueDateWO,pelaksana asc) as No1,ID,NoWO,UraianPekerjaan,DeptID_Users,Pelaksana,AreaWO," +
            //" DueDateWO,DATEDIFF(DAY,GETDATE(),DueDateWO)Lewat,case when NamaSubDept='' or NamaSubDept is null then (select A.Alias from Dept A where A.ID=DeptID_Users " +
            //" and A.RowStatus>-1) else 'HRD'+' '+'-'+' '+NamaSubDept end PemberiWO,StatusTarget [Target] from WorkOrder where " + query + " RowStatus>-1 and Apv>=0 " +
            //" and DueDateWO is not null and FinishDate is null), " +

            //" data2 as (select ROW_NUMBER() over (order by No1 asc) as NomorUrut,NoWO,UraianPekerjaan,PemberiWO FromDeptName,Pelaksana,AreaWO,LEFT(convert(char,DueDateWO,106),11)" +
            //" TglTarget,ID,isnull((select AA.Approval from WorkOrder_Target AA where AA.WoID=DataAwal.ID and AA.RowStatus>-1 and Aktif=1 ),-1)Apv  "+
            //" ,isnull(Target,1)Target from DataAwal " +
            //" where (Lewat<0)  or ID=( select WoID from WorkOrder_Target A where A.WoID=DataAwal.ID and RowStatus>-1 and Aktif=1 and Approval=0 )) " +
            //" select *,case when Apv=-1 then 'Lewat' when Apv=0 then 'Open' when Apv=1 then 'Lewat' end Ket from data2 ";

            " with DataAwal as  (select ROW_NUMBER() over (order by DeptID_Users,DueDateWO,pelaksana asc) as No1,ID,NoWO,UraianPekerjaan,DeptID_Users," +
            " Pelaksana,AreaWO, DueDateWO,DATEDIFF(DAY,GETDATE(),DueDateWO)Lewat,case when NamaSubDept='' or NamaSubDept is null " +
            " then (select A.Alias from Dept A where A.ID=DeptID_Users  and A.RowStatus>-1) else 'HRD'+' '+'-'+' '+NamaSubDept " +
            " end PemberiWO,isnull(StatusTarget,1) Target from WorkOrder where  " + query + "  RowStatus>-1 and Apv>=3  "+
            " and DueDateWO is not null and FinishDate is null and Cancel is null), " +

            " data2 as (select ROW_NUMBER() over (order by No1 asc) as NomorUrut1,NoWO,UraianPekerjaan,PemberiWO FromDeptName,Pelaksana,AreaWO," +
            " LEFT(convert(char,DueDateWO,106),11) TglTarget,ID,isnull((select AA.Approval from WorkOrder_Target AA where AA.WoID=DataAwal.ID " +
            " and AA.RowStatus>-1 and Aktif=1 ),-1)Apv,Target from DataAwal where (Lewat<0)  " +
            " or ID=( select WoID from WorkOrder_Target A where A.WoID=DataAwal.ID and RowStatus>-1 and Aktif=1 and Approval=0 and A.Cancel is null)), " +

            " data3 as (select *,case when Apv=-1  then 'Lewat' when Apv=1 then 'Lewat' when Apv=0 then 'Open' end Ket,isnull((select TglTarget from WorkOrder_Target A " +
            " where A.WoID=data2.ID and RowStatus>-1 and target=1),'')T1,isnull((select TglTarget from WorkOrder_Target A where A.WoID=data2.ID " +
            " and RowStatus>-1 and target=2),'')T2,isnull((select TglTarget from WorkOrder_Target A where A.WoID=data2.ID and RowStatus>-1 and target=3),'')T3 " +
            " from data2 ), " +

            " data4 as (select *,case when T1='1900-01-01 00:00:00.000' then TglTarget else T1 end Target1,T2 Target2,T3 Target3 from data3), " +

            " data5 as (select NomorUrut1,NoWO,UraianPekerjaan,FromDeptName,Pelaksana,AreaWO,case when TglTarget='01 Jan 1900' then '' else TglTarget end TglTarget,ID,Apv,ket,left(convert(char,Target1,106),11)" +
            " Target1,left(convert(char,Target2,106),11)Target2,left(convert(char,Target3,106),11)Target3,Target from data4), " +

            //" data6 as (select *,case when Target1='01 Jan 1900' then '' else Target1 end Target01,case when Target2='01 Jan 1900' then '-' else Target2 " +
            //" end Target02,case when Target3='01 Jan 1900' then '-' else Target3 end Target03 from data5) "+
            " data6 as (select NomorUrut1,NoWO,UraianPekerjaan,FromDeptName,Pelaksana,AreaWO,TglTarget,ID,Apv,Ket, "+
            " case when Target1='01 Jan 1900' then '' else Target1 end Target1,case when Target2='01 Jan 1900' then '' else Target2 "+
            " end Target2,case when Target3='01 Jan 1900' then '' else Target3 end Target3,Target from data5) "+

            " select ROW_NUMBER() over (order by data6.target2 desc) as NomorUrut,* from data6 order by data6.target2 desc ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new WorkOrder_New
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NomorUrut = Convert.ToInt32(sdr["NomorUrut"].ToString()),
                        NoWO = sdr["NoWO"].ToString(),
                        UraianPekerjaan = sdr["UraianPekerjaan"].ToString(),
                        FromDeptName = sdr["FromDeptName"].ToString(),
                        Pelaksana = sdr["Pelaksana"].ToString(),
                        AreaWO = sdr["AreaWO"].ToString(),
                        TglTarget = sdr["TglTarget"].ToString(),
                        Apv = Convert.ToInt32(sdr["Apv"].ToString()),
                        Ket = sdr["Ket"].ToString(),
                        Target01 = sdr["Target1"].ToString(),
                        Target02 = sdr["Target2"].ToString(),
                        Target03 = sdr["Target3"].ToString(),
                        Target = Convert.ToInt32(sdr["Target"].ToString())
                    });
                }
            }
            return arrData;
        }

        public WorkOrder_New RetrieveTarget(string NoWO)
        {
            string StrSql = 
            " select * from WorkOrder_Target where WoID in (select ID from WorkOrder where NoWO='"+NoWO+"' and RowStatus>-1) and RowStatus>-1 and Aktif=1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveTarget(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveTarget(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"].ToString());
            objWO.Target = Convert.ToInt32(sqlDataReader["Target"].ToString());
            //objWO.TargetT1 = sqlDataReader["TargetT1"].ToString();
            //objWO.TargetT2 = sqlDataReader["TargetT2"].ToString();
            //objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            //objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"].ToString());
            //objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            //objWO.WaktuDateLine = sqlDataReader["WaktuDateLine"].ToString();
            //objWO.WaktuNow = sqlDataReader["WaktuNow"].ToString();
            //objWO.Selisih = Convert.ToInt32(sqlDataReader["Selisih"].ToString());
            //objWO.Total = Convert.ToInt32(sqlDataReader["Total"].ToString());
            return objWO;
        }

        public int RetrieveStatusTarget(int ID)
        {
            string StrSql =
            " select * from WorkOrder_Target where Aktif=1 and RowStatus>-1 and WoID=" + ID + " and Approval=0 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public WorkOrder_New RetrieveDataUsers(int UserID)
        {
            string StrSql =
            " select * from WorkOrder_ListApvUpdate where  RowStatus>-1 and UserID="+UserID+" ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataUsers(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_RetrieveDataUsers(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();
            objWO.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());           
            return objWO;
        }

        public WorkOrder_New Retrieve_StatusReport(int UserID)
        {
            string StrSql =
                /** "select * from WorkOrder_ListApvUpdate where RowStatus>-1 and UserID=" + UserID + " "; **/

            " select sum(StatusReport)StatusReport,sum(StatusApv)StatusApv from (select StatusReport,StatusApv from WorkOrder_ListApvUpdate " +
            " where RowStatus>-1 and UserID=" + UserID + " " +
            " union all " +
            " select 0,0 ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_StatusReport(sqlDataReader);
                }
            }

            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_StatusReport(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.StatusApv = sqlDataReader["StatusApv"].ToString();
            objWO.StatusReport = sqlDataReader["StatusReport"].ToString();
            return objWO;
        }

        //Wo IT dari Maintenance -- razib
        public WorkOrder_New RetrieveByNoWithStatus(string strField, string strValue)
        {
            string strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else  " +
                            " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,  LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,  " +
                            " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui,  " +
                            " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget, ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate, UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept," +
                            " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO  and RowStatus > -1)ToDeptName,A.UraianPerbaikan,A.DeptID_Users from WorkOrder as A  where Apv=4 and A.RowStatus > -1 and A.DueDateWO is not NULL and " + strField + " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectX(sqlDataReader);
                }
            }
            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObjectX(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.WOID = Convert.ToInt32(sqlDataReader["WOID"]);
            objWO.NoWO = sqlDataReader["NoWO"].ToString();
            objWO.DeptName = sqlDataReader["DeptName"].ToString();
            objWO.TglDibuat = sqlDataReader["TglDibuat"].ToString();
            objWO.TglDisetujui = sqlDataReader["TglDisetujui"].ToString();
            objWO.UraianPekerjaan = sqlDataReader["UraianPekerjaan"].ToString();
            objWO.Pelaksana = sqlDataReader["Pelaksana"].ToString();
            objWO.FinishDate = sqlDataReader["FinishDate"].ToString();
            objWO.TglTarget = sqlDataReader["TglTarget"].ToString();
            objWO.AreaWO = sqlDataReader["AreaWO"].ToString();
            objWO.ToDept = Convert.ToInt32(sqlDataReader["ToDept"]);
            objWO.ToDeptName = sqlDataReader["ToDeptName"].ToString();
            objWO.UraianPerbaikan = sqlDataReader["UraianPerbaikan"].ToString();
            objWO.DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"]);
            return objWO;
        }

        public ArrayList AccountEmail_TO(string DeptID)
        {
            string strSQL = string.Empty;
            strSQL =
            " select AccountEmail,UserName from WorkOrder_AccountEmail where TipeKirim='TO' and DeptID='"+DeptID+"' and RowStatus > -1 order by Prioritas,Urutan ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObject_Email(sqlDataReader));
                }
            }           

            return arrWO;
        }

        public ArrayList AccountEmail_CC(string DeptID2)
        {
            string strSQL = string.Empty;
            strSQL =
            " select AccountEmail,UserName from WorkOrder_AccountEmail where TipeKirim='CC' and DeptID in (0,'" + DeptID2 + "') and RowStatus > -1 order by DeptID,Prioritas,Urutan ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(GenerateObject_Email(sqlDataReader));
                }
            }           

            return arrWO;
        }

        public WorkOrder_New GenerateObject_Email(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.AccountEmail = sqlDataReader["AccountEmail"].ToString();
            objWO.UserName = sqlDataReader["UserName"].ToString();            
            return objWO;
        }

        public string RetieveDeptID(string ID)
        {
            string result = "0"; 
            string strSQL = " select DeptID_PenerimaWO DeptID from WorkOrder where ID='" + ID + "' and RowStatus>-1 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //return sdr["DeptID"].ToString();
                    result = sdr["DeptID"].ToString();
                }
            }

            return result;
        }

        public WorkOrder_New DeptID_Nama(string ID)
        {
            string strSQL =
            " select A.Alias Dept_Pemberi,B.Alias Dept_Penerima from ( " +
            " select DeptID_Users,DeptID_PenerimaWO from WorkOrder where ID='"+ID+"' and RowStatus>-1  " +
            " ) as x inner join Dept A ON A.ID=x.DeptID_Users inner join Dept B ON B.ID=x.DeptID_PenerimaWO ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrWO = new ArrayList();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return GenerateObject_Name2(sdr);
                }
            }
            return new WorkOrder_New();
        }

        public WorkOrder_New GenerateObject_Name(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();           
            objWO.DeptName = sqlDataReader["DeptName"].ToString();           
            return objWO;
        }

        public WorkOrder_New GenerateObject_Name2(SqlDataReader sqlDataReader)
        {
            objWO = new WorkOrder_New();
            objWO.Dept_Pemberi = sqlDataReader["Dept_Pemberi"].ToString();
            objWO.Dept_Penerima = sqlDataReader["Dept_Penerima"].ToString();
            return objWO;
        }

        public ArrayList RetrieveWO(int DeptID)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            string query = string.Empty; string query0 = string.Empty; string query00 = string.Empty; int dept = 0;
            if (DeptID != 19 && DeptID != 25 && DeptID != 7 && DeptID != 14)
            {
                query = " where DeptID_Users=" + DeptID + "  ";
                query0 = " DeptID_Users ";
                query00 = " Dept_Penerima ";
            }
            else
            {
                //query = "";
                query = " where DeptID_PenerimaWO=" + DeptID + "  ";
                query0 = " DeptID_PenerimaWO ";
                query00 = " Dept_Pemberi ";
            }

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            { dept = 19; }
            else { dept = DeptID; }

            if (dept == 19 || dept == 14 || dept == 7)
            {
                query =
                    " select Noted+' : '+Informasi Informasi from ( " +
                    " select DeptID_Users,'WO Keluar blm ada Target'Noted,'( '+REPLACE(info,'&amp;','&')+' )' Informasi " +
                    " from tempInfoWO2 where DeptID_Users=" + dept + " group by DeptID_Users,info " +
                    " union " +
                    " select  DeptID_PenerimaWO ,'WO Masuk blm ada Target'Noted,'( '+REPLACE(info,'&amp;','&')+' )' Informasi " +
                    " from tempInfoWO22 where DeptID_PenerimaWO=" + dept + " group by DeptID_PenerimaWO,info " +
                    " union " +
                    " select DeptID_PenerimaWO,'WO sdh lewat Target'Noted,REPLACE(Informasi,'&amp;','&') Informasi from temp_dataWO4 " +
                    " ) as x ";
            }
            else
            {
                query =
                    " select Noted+' : '+Informasi Informasi from ( " +
                    " select DeptID_Users,'WO Keluar blm ada Target'Noted,REPLACE(info,'&amp;','&') Informasi " +
                    " from tempInfoWO2 where DeptID_Users=" + dept + " group by DeptID_Users,info ) as x ";
            }

            string strQuery =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO1]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO1] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO2]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO2] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO11]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO11] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO22]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO22] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWOO]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWOO] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO1]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO1] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO2]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO2] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO3]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO3] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO4]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO4] " +


            #region 1
            " ;with " +
            " data1 as ( " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (7,19) and RowStatus>-1 and apv=2 " +
            " union " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (14) and RowStatus>-1 and apv=2 and AreaWO='HardWare' " +
            " union " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (14) and RowStatus>-1 and apv=2 and ApvOP>1 " +
            " and VerISO=1 and AreaWO='Software' and PlantID=" + users.UnitKerjaID + " " +
            " ), " +

            " data2 as (select a.*,(select top 1 b.CreatedTime from WorkOrder_LogApproval b where b.WOID=a.ID and  b.RowStatus>-1 and Urutan=1 )TglApv " +
            " from data1 a), " +

            " data3 as (select *,GETDATE() TglNow from data2), " +

            " data4 as (select *,(select SUM(Selisih)Selisih from  (  " +
            " select datediff(dd, TglApv, getdate()) - case when DATEPART(dw,getdate())=7  then 1 when DATEPART(dw,getdate())=1 then 1 else 0 " +
            " end  - (datediff(wk, TglApv, getdate()) * 2) -   case when datepart(dw, TglApv) = 1 then 1 else 0 end +  " +
            " case when datepart(dw, getdate()) = 1  then 1 else 0 end Selisih    " +
            " union all " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=getdate() and " +
            " LEFT(convert(char,harilibur,112),8)<=TglApv   " +
            " ) as xx)Selisih from data3), " +

            " data5 as ( select a.*,b.Alias PemberiWO,c.Alias PenerimaWO from data4 a inner join Dept b ON a.DeptID_Users=b.ID " +
            " inner join Dept c ON a.DeptID_PenerimaWO=c.ID where Selisih>=7), " +

            " data6 as ( " +
            " select count(DeptID_Users)Ttl_ada,PemberiWO Dept_Pemberi,PenerimaWO Dept_Penerima,DeptID_Users,DeptID_PenerimaWO from data5 " +
                //" "+ query +" "+
            " group by PemberiWO,PenerimaWO,DeptID_Users,DeptID_PenerimaWO ) " +

            " select DeptID_Users,DeptID_PenerimaWO,trim(Dept_Penerima)+' : '+trim(cast(Ttl_ada as char)) info into tempInfoWO1 from data6 " +

            " select DISTINCT Info = STUFF((SELECT DISTINCT '; ' + Info FROM tempInfoWO1 AS x2  WHERE x2.DeptID_Users  = x3.DeptID_Users " +
            " FOR XML PATH('')), 1, 1, ''),DeptID_PenerimaWO,DeptID_Users into tempInfoWO2 from tempInfoWO1 x3 group by x3.Info,DeptID_Users,DeptID_PenerimaWO " +
            #endregion

            #region 2
            " ;with " +
            " data1 as ( " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (7,19) and RowStatus>-1 and apv=2 " +
            " union " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (14) and RowStatus>-1 and apv=2 and AreaWO='HardWare' " +
            " union " +
            " select DeptID_Users,DeptID_PenerimaWO,ID,Apv from WorkOrder where DeptID_PenerimaWO in (14) and RowStatus>-1 and apv=2 and ApvOP>1 " +
            " and VerISO=1 and AreaWO='Software' and PlantID=" + users.UnitKerjaID + " " +
            " ), " +

            " data2 as (select a.*,(select top 1 b.CreatedTime from WorkOrder_LogApproval b where b.WOID=a.ID and  b.RowStatus>-1 and Urutan=1 )TglApv " +
            " from data1 a), " +

            " data3 as (select *,GETDATE() TglNow from data2), " +

            " data4 as (select *,(select SUM(Selisih)Selisih from  (  " +
            " select datediff(dd, TglApv, getdate()) - case when DATEPART(dw,getdate())=7  then 1 when DATEPART(dw,getdate())=1 then 1 else 0 " +
            " end  - (datediff(wk, TglApv, getdate()) * 2) -   case when datepart(dw, TglApv) = 1 then 1 else 0 end +  " +
            " case when datepart(dw, getdate()) = 1  then 1 else 0 end Selisih    " +
            " union all " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=getdate() and " +
            " LEFT(convert(char,harilibur,112),8)<=TglApv   " +
            " ) as xx)Selisih from data3), " +

            " data5 as ( select a.*,b.Alias PemberiWO,c.Alias PenerimaWO from data4 a inner join Dept b ON a.DeptID_Users=b.ID " +
            " inner join Dept c ON a.DeptID_PenerimaWO=c.ID where Selisih>=7), " +

            " data6 as ( " +
            " select count(DeptID_Users)Ttl_ada,PemberiWO Dept_Pemberi,PenerimaWO Dept_Penerima,DeptID_Users,DeptID_PenerimaWO from data5 " +
            " group by PemberiWO,PenerimaWO,DeptID_Users,DeptID_PenerimaWO ) " +

            " select DeptID_Users,DeptID_PenerimaWO, trim(Dept_Pemberi)+' : '+trim(cast(Ttl_ada as char)) info into tempInfoWO11 from data6 " +

            " select DISTINCT Info = STUFF((SELECT DISTINCT '; ' + Info FROM tempInfoWO11 AS x2  WHERE x2.DeptID_PenerimaWO  = x3.DeptID_PenerimaWO " +
            " FOR XML PATH('')), 1, 1, ''),DeptID_PenerimaWO,DeptID_Users into tempInfoWO22 from tempInfoWO11 x3 group by x3.Info, DeptID_PenerimaWO,DeptID_Users " +
            #endregion

            #region 3
            " /** Break 7 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser ApvMgr,UpdateTargetTime Waktu2,DueDateWO,FinishDate FinishDate2,Pelaksana,CreatedBy,selisih_apv Selisih,case when SisaHari=0 then 0 else SisaHari end SisaHari,StatusWO,StatusApv,UsersWO,PenerimaWO,AreaWO SubArea2,AreaWO,PlantID,NamaSubDept,DateApvOP into temp_dataWOO from ( " +
            " /** Break 6 **/ " +
            " select woID,StatusWO,StatusApv,NoWO,UraianPekerjaan,UsersWO,PenerimaWO,AreaWO,DeptID_Users,DeptID_PenerimaWO,Target, " +
            " left(convert(char,CreatedTime,106),12)CreatedTime,left(convert(char,ApvMgrUser,106),12)ApvMgrUser,left(convert(char,UpdateTargetTime,106),12)UpdateTargetTime,left(convert(char,DueDateWO,106),12)DueDateWO,left(convert(char,FinishDate,106),12)FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,selisih_apv,case when selisih_pekerjaan<0 then '0 HK' else selisih_pekerjaan + ' HK' end selisih_pekerjaan,WaktuSelesai,WaktuDateLine,  case   " +
            " when Target=1 and   YEAR(WaktuSelesai)>1900   then 0     " +
            " when Target=1 and YEAR(WaktuSelesai)=1900 then selisih_pekerjaan     " +
            " when Target=1 and WaktuSelesai = '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai <> '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai = '' then selisih_pekerjaan  end SisaHari,PlantID,NamaSubDept,DateApvOP from ( " +
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
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser,UpdateTargetTime,DueDateWO,/**case when waktu0<0 then null when waktu0 is null then null else FinishDate end **/FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,NamaSubDept,VerISO,WaktuSelesai,WaktuDateLine,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID,waktu0 " +
            " , case when StatusTarget is null " +
            " then case when AreaWO='Hardware' then isnull(LEFT(convert(char,ApvMgrUser,106),12),'') when AreaWO='SoftWare' then  isnull(LEFT(convert(char,DateApvOP,106),12),'') end " +
            " when StatusTarget=2 then (select isnull(LEFT(convert(char,b1.CreatedTime,106),12),'') from WorkOrder_Target b1 where b1.WoID=x2.woID and b1.RowStatus>-1 and b1.Target=2) " +
            " when StatusTarget=3 then (select isnull(LEFT(convert(char,b2.CreatedTime,106),12),'') from WorkOrder_Target b2 where b2.WoID=x2.woID and b2.RowStatus>-1 and b2.Target=3) " +
            " end DateApvOP from ( " +
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
            " case when Target is null then CreatedTime when Target=1 then CreatedTime else CreatedTime2 end CreatedTime,UpdateTargetTime0,Pelaksana,CreatedBy,Apv,VerISO,NamaSubDept,WaktuSelesai,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID,DateApvOP from ( " +
            " /** Break 1 **/ " +
            " select A.ID woID,A.NoWO,A.UraianPekerjaan,A.DeptID_Users,A.DeptID_PenerimaWO,A.StatusTarget,A.DueDateWO DueDateWO_awal,D.TglTarget,D.Target,A.FinishDate,A.CreatedTime,D.CreatedTime CreatedTime2,A.UpdateTargetTime UpdateTargetTime0,Pelaksana,A.CreatedBy,A.Apv,A.VerISO,A.NamaSubDept,isnull(LEFT(convert(char,FinishDate,112),8),'')WaktuSelesai,case when A.SubArea ='' or A.SubArea is null then TRIM(A.AreaWO) else TRIM(A.AreaWO) +' - ' + TRIM(A.SubArea) end AreaWO,A.ApvOP,A.VerSec,A.Cancel,GETDATE()WaktuNow,C.Alias UsersWO,C1.Alias PenerimaWO,A.PlantID " +
            " ,case when A.ApvOP=2 and A.DeptID_Users in (7,23,24,23,26,12,13) then (select top 1 a1.CreatedTime from WorkOrder_LogApproval a1 where a1.WOID=A.ID and a1.RowStatus>-1 and a1.Urutan=3) else A.DateApvOP end DateApvOP " +
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
            " where DeptID_PenerimaWO='" + DeptID + "' " +
            " /** end Break 7 **/ " +

            " select count(NoWO)Ttl,DeptNama,DeptID_PenerimaWO into temp_dataWO1 from ( " +
            " select a.Alias DeptNama,DeptID_PenerimaWO,NoWO /**into temp_dataWO1**/ from ( " +
            " select cast(TargetWO as int)wk1,cast(WaktuNow as int)wk2,TargetWO,WaktuNow,DeptID_Users,DeptID_PenerimaWO,NoWO from ( " +
            " select cast(DueDateWO as datetime)TargetWO,GETDATE()WaktuNow,DeptID_Users,DeptID_PenerimaWO,NoWO from temp_dataWOO where FinishDate2 is null or FinishDate2='' " +
            " ) as x where TargetWO is not null " +
            " ) as xx inner join Dept a ON xx.DeptID_Users=a.ID where wk1<=wk2 group by DeptID_Users,a.Alias,xx.DeptID_PenerimaWO,NoWO " +
            " ) as xxx group by DeptNama,DeptID_PenerimaWO " +
            " select trim(DeptNama)+' : '+trim(cast(ttl as char)) Informasi,DeptID_PenerimaWO into temp_dataWO2 from temp_dataWO1 " +
            " select DISTINCT Informasi = STUFF((SELECT DISTINCT '; ' + Informasi FROM temp_dataWO2 AS x2 " +
            " WHERE x2.DeptID_PenerimaWO = x3.DeptID_PenerimaWO  FOR XML PATH('')), 1, 1, ''),DeptID_PenerimaWO into temp_dataWO3 " +
            " from temp_dataWO2 x3 group by x3.DeptID_PenerimaWO " +

            " select '( '+REPLACE(Informasi,'&amp;','&')+' )' Informasi,DeptID_PenerimaWO into temp_dataWO4 from temp_dataWO3 " +


            #endregion

 "" + query + "" +


            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO1]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO1] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO2]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO2] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO11]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO11] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempInfoWO22]') AND type in (N'U')) DROP TABLE [dbo].[tempInfoWO22] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWOO]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWOO] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO1]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO1] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO2]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO2] " +
            " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO3]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO3] ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrWO = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrWO.Add(new WorkOrder_New
                    {
                        //DeptID_Users = Convert.ToInt32(sqlDataReader["DeptID_Users"]),
                        Informasi = sqlDataReader["Informasi"].ToString()
                    });
                }
            }
            return arrWO;           
        }

        public int cekShareWO(int DeptID, int PlantID)
        {
            string Plant = string.Empty;
            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                Plant = "19";
            }
            else
            {
                Plant = DeptID.ToString();
            }

            string StrSql =

            " select sum(ttl)ttl from ( " +
            " select count(ID)ttl from WorkOrder where DeptID_Users=" + Plant + " and DeptID_PenerimaWO=14 and PlantID<>" + PlantID + " and Apv=2 and VerISO=1 and ApvOP=1 and RowStatus>-1 " +
            " union all " +
            " select 0 " +
            " ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ttl"]);
                }
            }

            return 0;
        }

        public int cekWO(int DeptID, int PlantID)
        {
            string Flag = string.Empty;
            if (DeptID == 19 || DeptID == 4 || DeptID == 25 || DeptID == 18)
            {
                Flag = " select count(ID)ttl from WorkOrder where RowStatus>-1 and apv=2 and DeptID_PenerimaWO=19 and UpdatePelaksanaTime is null ";
            }

            if (DeptID == 14)
            {
                Flag =

                " select count(ID)ttl from WorkOrder where RowStatus>-1 and apv=2 and VerISO=1 and ApvOP=2 and PlantID=" + PlantID + " and DeptID_PenerimaWO=" + DeptID + " " +
                " union all " +
                " select count(ID)ttl from WorkOrder where RowStatus>-1 and apv=2 and PlantID=" + PlantID + " and DeptID_PenerimaWO=" + DeptID + " and AreaWO='HardWare' ";

            }

            if (DeptID == 7)
            {
                Flag = " select count(ID)ttl from WorkOrder where RowStatus>-1 and apv=2 and DeptID_PenerimaWO=7 and Pelaksana is not null and UpdateTargetTime is not null ";
            }

            string StrSql =

            " select sum(ttl)ttl from ( " +
            " " + Flag + "" +
            " union all " +
            " select 0 " +
            " ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ttl"]);
                }
            }

            return 0;
        }
        //public WorkOrder_New RetrieveByNoWithStatus(string strField, string strValue)
        //{
        //    string strSQL = " select A.ID WOID,NoWO,case when A.DeptID_Users=7 and A.DeptID_PenerimaWO=7 then 'HRD'+' - '+A.NamaSubDept else  " +
        //                    " (select Alias from Dept as dpt where dpt.ID=DeptID_Users and dpt.RowStatus > -1) end DeptName,  LEFT(CONVERT(CHAR,A.createdtime,106),11)TglDibuat,  " +
        //                    " (select top 1 ISNULL(LEFT(CONVERT(CHAR,wl2.createdtime,106),11),'')TglDisetujui from WorkOrder_LogApproval wl2 where wl2.WOID=A.ID and RowStatus>-1 and wl2.Urutan=2) TglDisetujui,  " +
        //                    " ISNULL(A.Pelaksana,'')Pelaksana, ISNULL(LEFT(CONVERT(CHAR,A.DueDateWO,106),11),'')TglTarget, ISNULL(LEFT(CONVERT(CHAR,A.FinishDate,106),11),'')FinishDate, UraianPekerjaan,A.AreaWO,A.DeptID_PenerimaWO ToDept," +
        //                    " (select NamaDept from WorkOrder_Dept where DeptID=A.DeptID_PenerimaWO  and RowStatus > -1)ToDeptName,A.UraianPerbaikan,A.DeptID_Users from WorkOrder as A  where Apv=4 and A.RowStatus > -1 and A.DueDateWO is not NULL and " + strField + " like '%" + strValue + "%'";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrWO = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectX(sqlDataReader);
        //        }
        //    }
        //    return new WorkOrder_New();
        //}

    }
}

  //string result = string.Empty;
  //          string StrSql = " select NamaSubDept from WorkOrder_SubDept where RowStatus>-1 and (UserID="+ID+" or HeadID="+ID+") ";
  //          DataAccess dataAccess = new DataAccess(Global.ConnectionString());
  //          SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
  //          strError = dataAccess.Error;

  //          if (sqlDataReader.HasRows)
  //          {
  //              while (sqlDataReader.Read())
  //              {
  //                  result = sqlDataReader["NamaSubDept"].ToString();
  //              }
  //          }

  //          return result;
