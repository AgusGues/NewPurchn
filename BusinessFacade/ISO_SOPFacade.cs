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
    public class ISO_SOPFacade : AbstractTransactionFacade
    {
        private ISO_SOP objSOP = new ISO_SOP();
        private ArrayList arrSOP;
        private List<SqlParameter> sqlListParam;

        public ISO_SOPFacade(object objDomain)
        : base(objDomain)
        {
            objSOP = (ISO_SOP)objDomain;
        }

        public ISO_SOPFacade()
        {

        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SopNo", objSOP.SopNo));
                sqlListParam.Add(new SqlParameter("@NewSop", objSOP.NewSop));
                sqlListParam.Add(new SqlParameter("@DeptID", objSOP.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSOP.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSOP.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSOP.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSOP.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSOP.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSOP.Ket));
                sqlListParam.Add(new SqlParameter("@Pic", objSOP.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSOP.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objSOP.UserGroupID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objSOP.TargetKe));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSOP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSOP.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSOP");

                strError = transManager.Error;


                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertKPI(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@KpiNo", objSOP.KpiNo));
                sqlListParam.Add(new SqlParameter("@NewKpi", objSOP.NewKpi));
                sqlListParam.Add(new SqlParameter("@DeptID", objSOP.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSOP.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSOP.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSOP.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSOP.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSOP.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSOP.Ket));
                sqlListParam.Add(new SqlParameter("@Pic", objSOP.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSOP.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objSOP.UserGroupID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objSOP.TargetKe));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSOP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSOP.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spPES_KPI_Insert");

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
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SopNo", objSOP.SopNo));
                sqlListParam.Add(new SqlParameter("@NewSop", objSOP.NewSop));
                sqlListParam.Add(new SqlParameter("@DeptID", objSOP.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSOP.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSOP.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSOP.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSOP.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSOP.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSOP.Ket));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objSOP.TglSelesai));
                sqlListParam.Add(new SqlParameter("@Pic", objSOP.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSOP.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objSOP.UserGroupID));
                sqlListParam.Add(new SqlParameter("@Status", objSOP.Status));
                sqlListParam.Add(new SqlParameter("@RowStatus", objSOP.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSOP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSOP.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSOP");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Delete(TransactionManager transManager)
        {

            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSOP.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSOP.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSOP");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateTaskDetailApproval(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objSOP.IdDetail));
                sqlListParam.Add(new SqlParameter("@Approval", objSOP.App));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSOPDetailApproval");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSopDetailApproval(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objSOP.IdDetail));
                sqlListParam.Add(new SqlParameter("@Approval", objSOP.App));
                sqlListParam.Add(new SqlParameter("@AlasanUnApprove", objSOP.AlasanUnApprove));

                int intResult = transManager.DoTransaction(sqlListParam, (objSOP.PesType == 3) ? "spUpdateSOPDetailApproval" : "spUpdateKPIDetailApproval");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateSopDetailApproval1(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objSOP.IdDetail));
                sqlListParam.Add(new SqlParameter("@Approval", objSOP.App));
                sqlListParam.Add(new SqlParameter("@AlasanUnApprove", objSOP.AlasanUnApprove));

                int intResult = transManager.DoTransaction(sqlListParam, (objSOP.PesType == 3) ? "spUpdateSOPDetailApproval1" : "spUpdateKPIDetailApproval1");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateStatusPosting(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objSOP.ID));
                sqlListParam.Add(new SqlParameter("@Status", objSOP.Status));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objSOP.TglSelesai));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSOPStatus");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateDetailStatus(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objSOP.IdDetail));
                sqlListParam.Add(new SqlParameter("@Status", objSOP.Status));
                sqlListParam.Add(new SqlParameter("@PointNilai", objSOP.PointNilai));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTaskDetailStatus");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int DeletePES(string ID,int PesType)
        {
            try
            {
                string Query = "";
                if (PesType == 3)
                {
                    Query = "Update ISO_SOP set RowStatus=-1, Status=-1 where ID=" + ID;
                    Query += "; Update ISO_SOPDetail set RowStatus=-1, Status=-1 where SOPID=" + ID;
                }
                else if (PesType == 1)
                {
                    Query = "Update ISO_KPI set RowStatus=-1, Status=-1 where ID=" + ID;
                    Query += "; Update ISO_KPIDetail set RowStatus=-1, Status=-1 where KPIID=" + ID;
                }
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(Query);
                
                    return 1;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        private string NewInputRebobot = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OtoRebobotAktif", "PES");
        public int InsertSOPDetail(TransactionManager transManager)
        {
            try
            {
                int intResult=0;
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@SopID", objSOP.SopID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objSOP.TargetKe));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSOP.TglTarget));
                sqlListParam.Add(new SqlParameter("@Status", objSOP.Status));
                sqlListParam.Add(new SqlParameter("@PointNilai", objSOP.PointNilai));
                sqlListParam.Add(new SqlParameter("@KetTargetKe", objSOP.KetTargetKe));
                sqlListParam.Add(new SqlParameter("@SopScoreID", objSOP.SopScoreID));
                if (NewInputRebobot == "1")
                {
                    sqlListParam.Add(new SqlParameter("@Rebobot", objSOP.Rebobot));
                    intResult = transManager.DoTransaction(sqlListParam, "spPES_SOP_DetailInsertNew");
                }
                else
                {
                    intResult = transManager.DoTransaction(sqlListParam, "spInsertSOPDetail");
                }

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertKPIDetail(TransactionManager transManager)
        {
            try
            {
                int intResult=0;
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@KpiID", objSOP.KpiID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objSOP.TargetKe));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSOP.TglTarget));
                sqlListParam.Add(new SqlParameter("@Status", objSOP.Status));
                sqlListParam.Add(new SqlParameter("@PointNilai", objSOP.PointNilai));
                sqlListParam.Add(new SqlParameter("@KetTargetKe", objSOP.KetTargetKe));
                sqlListParam.Add(new SqlParameter("@SopScoreID", objSOP.SopScoreID));
                if (NewInputRebobot == "1")
                {
                    sqlListParam.Add(new SqlParameter("@Rebobot", objSOP.Rebobot));
                    intResult = transManager.DoTransaction(sqlListParam, "spPES_KPI_DetailInsertNew");
                }
                else
                {
                    intResult = transManager.DoTransaction(sqlListParam, "spPES_KPI_DetailInsert");
                }
                strError = transManager.Error;

                return intResult;
                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertImgLampiran(TransactionManager transManager)
        {
            try
            {
                objSOP = (ISO_SOP)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@TaskID", objSOP.SopID));
                sqlListParam.Add(new SqlParameter("@Image", objSOP.Image));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSOPLampiran");

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
            string strSQL = "select A.ID,B.ID as idDetail, A.SopNo,A.SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip, " +
                            "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_SOP where RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public int GetLastTaskNo()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(left(SOPNo,5)),0) as Id from ISO_SOP");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Id"]);
                }
            }

            return 0;
        }
        public int GetLastSOPNo(int pesType, int deptID, int thn )
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select DocNo from ISO_DocumentNo where PesType=" + pesType + " and DeptID=" + deptID + " and Tahun=" + thn);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Id"]);
                }
            }

            return 0;
        }
        public decimal RetrieveByPointNilai(int pesType, string targetKe)
        {
            string strSQL = "select * from ISO_SOPScore where PesType=" + pesType + " and TargetKe='" + targetKe + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["PointNilai"]);
                }
            }

            return 0;
        }
        public ISO_SOP RetrieveByNo1(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.SopNo,A.SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID," +
                            "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot," +
                            "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,isnull(E.ID,0) as CatID,B.SopScoreID," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot " +
                            "from ISO_SOP as A "+
                            "LEFT JOIN ISO_SOPDetail as B ON " +
                            "A.ID=B.SOPID and B.RowStatus>-1 and B.Aktip=1 " +
                            "LEFT JOIN ISO_UserCategory as C " +
                            "ON C.ID=A.CategoryID and C.RowStatus >-1 " +
                            "LEFT JOIN ISO_SOPScore as D " +
                            "ON D.ID=B.SopScoreID and D.RowStatus>-1 " +
                            "LEFT JOIN ISO_Category as E ON " +
                            "E.ID=C.CategoryID " +
                            "where A.ID='" + no + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratObjectC(sqlDataReader);
                }
            }

            return new ISO_SOP();
        }
        public ISO_SOP RetrieveByNo2(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.KPINo as SopNo,A.KPIName as SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID," +
                            "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot," +
                            "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,isnull(E.ID,0) as CatID,B.SopScoreID," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot " +
                            "FROM ISO_KPI as A " +
                            "LEFT JOIN ISO_KPIDetail as B ON " +
                            "A.ID=B.KPIID and B.RowStatus>-1 and B.Aktip=1 " +
                            "LEFT JOIN ISO_UserCategory as C " +
                            "ON C.ID=A.CategoryID and C.RowStatus >-1 " +
                            "LEFT JOIN ISO_SOPScore as D " +
                            "ON D.ID=B.SopScoreID and D.RowStatus>-1 " +
                            "LEFT JOIN ISO_Category as E ON " +
                            "E.ID=C.CategoryID "+
                            "where  A.ID='" + no + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL); 
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratObjectC(sqlDataReader);
                }
            }

            return new ISO_SOP();
        }
        public string TipePes { get; set; }
        public ArrayList RetrieveList()
        {
            string strSQL = "";
            switch (TipePes)
            {
                case "KPI":
                    strSQL = "select A.ID,B.ID as idDetail, A.KPINo as SopNo,A.KPIName as SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                               "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID," +
                               "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot," +
                               "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,E.ID as CatID,B.SopScoreID," +
                               "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot " +
                               "from ISO_KPI as A,ISO_KPIDetail as B, ISO_UserCategory as C, ISO_SOPScore as D, " +
                               "ISO_Category as E " +
                               "where A.ID=B.KPIID and A.RowStatus>-1 and B.RowStatus>-1 and B.Aktip=1 " +
                               "and E.ID=C.CategoryID and E.RowStatus>-1 and D.ID=B.SopScoreID " +
                               "and C.ID=A.CategoryID " + this.Criteria;
                    break;
                case "SOP":
                    strSQL = "select A.ID,B.ID as idDetail, A.SopNo,A.SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                                    "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID," +
                                    "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot," +
                                    "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,E.ID as CatID,B.SopScoreID," +
                                    "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot " +
                                    "from ISO_SOP as A,ISO_SOPDetail as B, ISO_UserCategory as C, ISO_SOPScore as D, " +
                                    "ISO_Category as E " +
                                    "where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 and B.Aktip=1 " +
                                    "and E.ID=C.CategoryID and E.RowStatus>-1 and D.ID=B.SopScoreID " +
                                    "and C.ID=A.CategoryID " + this.Criteria;
                    break;
            }
            arrSOP = new ArrayList();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_SOP where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GeneratObjectC(sqlDataReader));
                }
            }

            return arrSOP;
        }
        public ArrayList RetrieveByDeptID(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and A.DeptID=" + deptID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_SOP where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ArrayList RetrieveByDeptIDSolved(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID=" + deptID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_SOP where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ArrayList RetrieveByDeptIDUnSolved(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip,"+
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai " +
                "from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and "+
                "A.DeptID=" + deptID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ArrayList RetrieveByDeptIDSolved2(int deptID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.SOPNo,A.SOPName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID=" + deptID + " and A.UserGroupID > 100  and A.UserID=" + userID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_SOP where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ArrayList RetrieveByDeptIDUnSolved2(int deptID, int userID)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.SOPNo,A.SOPName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai " +
                "from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and " +
                "A.DeptID=" + deptID + " and A.UserGroupID > 100 and A.UserID=" + userID + " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ArrayList RetrieveByPT(int ptID)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.SOPNo,A.SOPName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,"+
                            "'' as BagianName,B.Aktip,A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime " +
                            ",A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID "+
                            " and A.RowStatus>-1 and B.RowStatus>-1  and A.DepoID=" + ptID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_SOP where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }      
        public ArrayList RetrieveByPT2(string ptID)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.KpiNo as SopNo,A.KpiName as SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai, " +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID, " +
                            "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot, " +
                            "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,isnull(E.ID,0) as CatID,B.SopScoreID, " +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot  " +
                            "from ISO_KPI as A " +
                            "LEFT JOIN ISO_KPIDetail as B ON " +
                            "A.ID=B.KPIID and B.RowStatus>-1 and B.Aktip=1 " +
                            "LEFT JOIN ISO_UserCategory as C " +
                            "ON C.ID=A.CategoryID and C.RowStatus >-1 " +
                            "LEFT JOIN ISO_SOPScore as D " +
                            "ON D.ID=B.SopScoreID and D.RowStatus>-1 " +
                            "LEFT JOIN ISO_Category as E ON " +
                            "E.ID=C.CategoryID " + this.Criteria +
                            "where  A.RowStatus>-1 and B.Approval < 2 and A.DeptID in (" + ptID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GeneratObjectC(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public string Criteria { get; set; }
        public ArrayList RetrieveByPT3(string ptID)
        {
        #region oldQuery
                    //string strSQL = "select A.*,B.TargetKe " +
                    //                "from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 "+
                    //                "and A.Status =0 and A.DeptID=" + ptID + " and B.Approval=0 and B.Aktip=1 " +
                    //                "union " +
                    //                "select A.*,B.TargetKe " +
                    //                "from ISO_SOP as A,ISO_SOPDetail as B where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 "+
                    //                "and A.Status =2  and A.DeptID=" + ptID +
                    //                " and B.Approval=1 and B.Aktip=1  order by SOPNo";
        #endregion
            string strSQL = "select A.ID,B.ID as idDetail, A.SopNo,A.SopName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai, " +
                           "B.TglTargetSelesai,'' as BagianName,B.Aktip,D.TargetKe as KetTargetKe,A.CategoryID, " +
                           "CASE WHEN C.TypeBobot='%' THEN (A.NilaiBobot*100) ELSE A.NilaiBobot END NilaiBobot, " +
                           "A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,isnull(E.ID,0) as CatID,B.SopScoreID, " +
                           "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,E.Target,E.Checking,C.TypeBobot  " +
                           "from ISO_SOP as A " +
                           "LEFT JOIN ISO_SOPDetail as B ON " +
                           "A.ID=B.SOPID and B.RowStatus>-1 and B.Aktip=1 " +
                           "LEFT JOIN ISO_UserCategory as C " +
                           "ON C.ID=A.CategoryID and C.RowStatus >-1 " +
                           "LEFT JOIN ISO_SOPScore as D " +
                           "ON D.ID=B.SopScoreID and D.RowStatus>-1 " +
                           "LEFT JOIN ISO_Category as E ON " +
                           "E.ID=C.CategoryID " + this.Criteria +
                           "where  A.RowStatus>-1 and B.Approval < 2 and A.DeptID in(" + ptID+")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GeneratObjectC(sqlDataReader));
                }
            }
            
            return arrSOP;
        }
        public ArrayList RetrieveByNo(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.SOPNo,A.SOPName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                            "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai," +
                            "C.Description,B.KetTargetKe from ISO_SOP as A,ISO_SOPDetail as B,ISO_Category as C " +
                            "where A.ID=B.SOPID and A.RowStatus>-1 and B.RowStatus>-1 and A.CategoryID=C.ID and C.RowStatus>-1 " +
                            "and A.SOPNo='" + no + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectB(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
        public ISO_SOP RetrieveByJmlLampiran(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select COUNT(ID) as Jumlah from ISO_TaskLampiran where Status >=0 and "+
            "TaskID in (" + Id + ")");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }

            return new ISO_SOP();
        }
        public ArrayList RetrieveByArrLampiran(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Image from ISO_TaskLampiran where TaskID='" + Id + "' and Status>=0");
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
		
		// Ditambahkan tanggal 08 Oktober 2018
		
		public ArrayList RetrieveDept()
        {
            string strSQL = " select ID,Alias DeptName from Dept where ID in (select DeptID from ISO_Dept where RowStatus >-1) and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectRetrieveDept(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
		
		public ArrayList RetrievePIC(int DeptID)
        {
            string strSQL = " select UserID ID,UserName Pic from UserAccount where DeptID=" + DeptID + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectRetrievePIC(sqlDataReader));
                }
            }
            else
                arrSOP.Add(new ISO_SOP());

            return arrSOP;
        }
		
		public ArrayList RetrieveTahun(int UserID, string PES)
        {
            string strSQL = "  select DISTINCT(LEFT(convert(char,tglmulai,112),4))Tahun from ISO_" + PES + " where ISO_UserID=" + UserID + " and RowStatus > -1   ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectRetrieveTahun(sqlDataReader));
                }
            }

            return arrSOP;
        }
		
		public ArrayList RetrieveBulan(int UserID, int Tahun)
        {
            string strSQL =
            " select Bulan ID,case when Bulan=1 then 'JANUARI' when Bulan=2 then 'FEBRUARI' when Bulan=3 then 'MARET' " +
            " when Bulan=4 then 'APRIL' when Bulan=5 then 'MEI' when Bulan=6 then 'JUNI' when Bulan=7 then 'JULI' when Bulan=8 then 'AGUSTUS' " +
            " when Bulan=9 then 'SEPTEMBER' when Bulan=10 then 'OKTOBER' when Bulan=11 then 'NOVEMBER' when Bulan=12 then 'DESEMBER' end NamaBulan " +
            " from (select DISTINCT(MONTH(TglMulai))Bulan from ISO_SOP where ISO_UserID=" + UserID + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1 "+
            " union select DISTINCT(MONTH(TglMulai))Bulan from ISO_KPI where ISO_UserID=" + UserID + " and YEAR(TglMulai)=" + Tahun + 
            " and RowStatus > -1 ) as Data1  ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectRetrieveBulan(sqlDataReader));
                }
            }

            return arrSOP;
        }
		
		public ArrayList RetrieveDataKPISOP(int UserID, int Tahun, int Bulan, string KS)
        {
            string Query = string.Empty; string Query2 = string.Empty;
            if (KS == "SOP")
            {
                Query =
                " select ROW_NUMBER() over (order by data2.ID asc) as No,SUBSTRING(Periode,4,8)Periode2,Case when Data2.Status=2 then 'Approved' when Data2.Status=0 then 'Open' end StatusApproval,* from (select ID,left(convert(char,tglmulai,106),11)Periode,SOPName " +
                " [Description],CAST(NilaiBobot*100 AS INT) AS Bobot,Keterangan TargetPencapaian,(select CAST(pointnilai AS INT) AS PointNilai from ISO_SOPDetail where SOPID=A.id " +
                " and rowstatus>-1)PointNilai ,Status from ISO_SOP A ";
                Query2 = " and A.ID in (select SOPID from ISO_SOPDetail where RowStatus>-1) ";
            }
            else if (KS == "KPI")
            {
                Query =
                " select ROW_NUMBER() over (order by data2.ID asc) as No,SUBSTRING(Periode,4,8)Periode2,Case when Data2.Status=2 then 'Approved' when Data2.Status=0 then 'Open' end StatusApproval,* from (select ID,left(convert(char,tglmulai,106),11)Periode,KPIName " +
                " [Description],CAST(NilaiBobot*100 AS INT) AS Bobot,Keterangan TargetPencapaian,(select CAST(pointnilai AS INT) AS PointNilai from ISO_KPIDetail where KPIID=A.id " +
                " and rowstatus>-1)PointNilai ,Status from ISO_KPI A ";
                Query2 = " and A.ID in (select KPIID from ISO_KPIDetail where RowStatus>-1) ";
            }
            string strSQL =
            " " + Query + " " +

            " where ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1 "+Query2+") as Data2 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSOP.Add(GenerateObjectRetrievePES(sqlDataReader));
                }
            }

            return arrSOP;
        }
		
		public int Unlock(int ID, string KPISOP)
        {
            try
            {
                string Query = "";
                if (KPISOP == "SOP")
                {
                    Query = "Update ISO_SOP set status=0 where rowstatus>-1 and ID=" + ID;
                    Query += "; Update ISO_SOPDetail set status=0, approval=0 where rowstatus>-1 and SOPID=" + ID;
                }
                else if (KPISOP == "KPI")
                {
                    Query = "Update ISO_KPI set status=0 where rowstatus>-1 and ID=" + ID;
                    Query += "; Update ISO_KPIDetail set status=0, approval=0 where rowstatus>-1 and KPIID=" + ID;
                }
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(Query);

                return 1;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
		
		public int Lock(int ID, string KPISOP)
        {
            try
            {
                string Query = "";
                if (KPISOP == "SOP")
                {
                    Query = "Update ISO_SOP set status=2 where rowstatus>-1 and ID=" + ID;
                    Query += "; Update ISO_SOPDetail set status=2, approval=2 where rowstatus>-1 and SOPID=" + ID;
                }
                else if (KPISOP == "KPI")
                {
                    Query = "Update ISO_KPI set status=2 where rowstatus>-1 and ID=" + ID;
                    Query += "; Update ISO_KPIDetail set status=2, approval=2 where rowstatus>-1 and KPIID=" + ID;
                }
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(Query);

                return 1;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }	

		public int Cancel(string KPISOP, int UserID, int Tahun, int Bulan)
        {
            try
            {
                string Query = "";
                if (KPISOP == "SOP")
                {
                    Query = "update ISO_SOPDetail set RowStatus=-2 where SOPID in (select ID from ISO_SOP where  ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1) ";
                    Query += ";update ISO_SOP set rowstatus=-2 where ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1 ";

                    //Query = "update ISO_SOP set rowstatus=-2 where ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1 ";
                    //Query += ";update ISO_SOPDetail set RowStatus=-2 where SOPID in (select ID from ISO_SOP where  ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1) ";
                }
                else if (KPISOP == "KPI")
                {
                    //Query = "Update ISO_KPI set rowstatus=-2 where ID=" + ID;
                    //Query += "; Update ISO_KPIDetail set rowstatus=-2 where KPIID=" + ID;
                    Query = "update ISO_KPIDetail set RowStatus=-2 where KPIID in (select ID from ISO_KPI where  ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1) ";
                    Query += ";update ISO_KPI set rowstatus=-2 where ISO_UserID=" + UserID + " and MONTH(TglMulai)=" + Bulan + " and YEAR(TglMulai)=" + Tahun + " and RowStatus > -1 ";

                }
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(Query);

                return 1;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }		
		
		// End Tambahan
		
        public ISO_SOP GenerateObject(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOP.IdDetail = Convert.ToInt32(sqlDataReader["idDetail"]);
            objSOP.SopNo = sqlDataReader["SopNo"].ToString();
            objSOP.NewSop = sqlDataReader["SOPName"].ToString();
            objSOP.DeptID = Convert.ToInt16(sqlDataReader["DeptID"]);
            objSOP.BagianID = Convert.ToInt16(sqlDataReader["BagianID"]);
            objSOP.BagianName = sqlDataReader["BagianName"].ToString();
            objSOP.TargetKe = Convert.ToInt16(sqlDataReader["TargetKe"]);
            objSOP.TglMulai = Convert.ToDateTime(sqlDataReader["TglMulai"]);
            objSOP.TglTarget = Convert.ToDateTime(sqlDataReader["TglTargetSelesai"]);
            objSOP.CategoryID = Convert.ToInt16(sqlDataReader["CategoryID"]);
            objSOP.BobotNilai = Convert.ToInt16(sqlDataReader["NilaiBobot"]);
            objSOP.Ket = sqlDataReader["Keterangan"].ToString();
            //objSOP.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);
            if (string.IsNullOrEmpty(sqlDataReader["TglSelesai"].ToString()))
                objSOP.TglSelesai = DateTime.MinValue;
            else
                objSOP.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);

            objSOP.Pic = sqlDataReader["Pic"].ToString();
            objSOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objSOP.Aktip = Convert.ToInt32(sqlDataReader["Aktip"]);
            objSOP.App = Convert.ToInt32(sqlDataReader["Approval"]);
            objSOP.PointNilai = Convert.ToInt32(sqlDataReader["PointNilai"]);

            return objSOP;

        }
        public ISO_SOP GeneratObjectC(SqlDataReader sqlDataReader)
        {
            objSOP = GenerateObject(sqlDataReader);
            objSOP.KetTargetKe = sqlDataReader["KetTargetKe"].ToString();
            objSOP.Targete = sqlDataReader["Target"].ToString();
            objSOP.Checking = sqlDataReader["Checking"].ToString();
            objSOP.DepoID = Convert.ToInt32(sqlDataReader["CatID"].ToString());
            objSOP.TypeBobot = sqlDataReader["TypeBobot"].ToString();
            objSOP.SopScoreID = Convert.ToInt32(sqlDataReader["SopScoreID"].ToString());
            objSOP.Ket = sqlDataReader["Keterangan"].ToString();
            return objSOP;
        }
        public ISO_SOP GenerateObjectB(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOP.IdDetail = Convert.ToInt32(sqlDataReader["idDetail"]);
            objSOP.SopNo = sqlDataReader["SopNo"].ToString();
            objSOP.NewSop = sqlDataReader["SOPName"].ToString();
            objSOP.DeptID = Convert.ToInt16(sqlDataReader["DeptID"]);
            objSOP.BagianID = Convert.ToInt16(sqlDataReader["BagianID"]);
            objSOP.BagianName = sqlDataReader["BagianName"].ToString();
            objSOP.TargetKe = Convert.ToInt16(sqlDataReader["TargetKe"]);
            objSOP.TglMulai = Convert.ToDateTime(sqlDataReader["TglMulai"]);
            objSOP.TglTarget = Convert.ToDateTime(sqlDataReader["TglTargetSelesai"]);
            objSOP.CategoryID = Convert.ToInt16(sqlDataReader["CategoryID"]);
            objSOP.BobotNilai = Convert.ToDecimal(sqlDataReader["NilaiBobot"]);
            objSOP.Ket = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["TglSelesai"].ToString()))
                objSOP.TglSelesai = DateTime.MinValue;
            else
                objSOP.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);

            objSOP.Pic = sqlDataReader["Pic"].ToString();
            objSOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objSOP.Aktip = Convert.ToInt32(sqlDataReader["Aktip"]);
            objSOP.App = Convert.ToInt32(sqlDataReader["Approval"]);
            objSOP.PointNilai = Convert.ToInt32(sqlDataReader["PointNilai"]);
            objSOP.Description = sqlDataReader["Description"].ToString();
            objSOP.KetTargetKe = sqlDataReader["KetTargetKe"].ToString();

            return objSOP;

        }
        public ISO_SOP GenerateObjectHeader(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objSOP.IdDetail = Convert.ToInt32(sqlDataReader["idDetail"]);
            objSOP.SopNo = sqlDataReader["SOPNo"].ToString();
            objSOP.NewSop = sqlDataReader["SOPName"].ToString();
            objSOP.DeptID = Convert.ToInt16(sqlDataReader["DeptID"]);
            objSOP.BagianID = Convert.ToInt16(sqlDataReader["BagianID"]);
            objSOP.TargetKe = Convert.ToInt16(sqlDataReader["TargetKe"]);
            objSOP.TglMulai = Convert.ToDateTime(sqlDataReader["TglMulai"]);
            //objSOP.TglTarget = Convert.ToDateTime(sqlDataReader["TglTargetSelesai"]);
            objSOP.CategoryID = Convert.ToInt16(sqlDataReader["CategoryID"]);
            objSOP.BobotNilai = Convert.ToInt16(sqlDataReader["NilaiBobot"]);
            objSOP.Ket = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["TglSelesai"].ToString()))
                objSOP.TglSelesai = DateTime.MinValue;
            else
                objSOP.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);
            objSOP.Pic = sqlDataReader["Pic"].ToString();
            objSOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);


            return objSOP;

        }
        public ISO_SOP GenerateObject1(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.Jumlah = Convert.ToInt32(sqlDataReader["Jumlah"]);

            return objSOP;
        }
        public ISO_SOP GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.Image = Convert.ToString(sqlDataReader["Image"]);

            return objSOP;
        }
        public int getDeptID(int BagianID)
        {
            int result = 0;
            string strsql = "Select DeptID from ISO_Bagian where ID=" + BagianID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["DeptID"].ToString());
                }
            }
            return result;
        }
		
		// Tambahan Object , Added : 08 Oktober 2018 Oleh Beny
		public ISO_SOP GenerateObjectRetrieveDept(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOP.DeptName = sqlDataReader["DeptName"].ToString();

            return objSOP;
        }
		
		public ISO_SOP GenerateObjectRetrievePIC(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOP.Pic = sqlDataReader["Pic"].ToString();

            return objSOP;
        }
		
		public ISO_SOP GenerateObjectRetrieveTahun(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            //objSOP.DeptName = sqlDataReader["DeptName"].ToString();
            objSOP.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);

            return objSOP;
        }
		
		public ISO_SOP GenerateObjectRetrieveBulan(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            //objSOP.DeptName = sqlDataReader["DeptName"].ToString();
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOP.NamaBulan = sqlDataReader["NamaBulan"].ToString();

            return objSOP;
        }
		
		public ISO_SOP GenerateObjectRetrievePES(SqlDataReader sqlDataReader)
        {
            objSOP = new ISO_SOP();
            objSOP.No = sqlDataReader["No"].ToString();
            objSOP.Description = sqlDataReader["Description"].ToString();
            objSOP.Bobot = sqlDataReader["Bobot"].ToString();
            objSOP.TargetPencapaian = sqlDataReader["TargetPencapaian"].ToString();
            objSOP.PointNilai = Convert.ToDecimal(sqlDataReader["PointNilai"]);
            objSOP.StatusApproval = sqlDataReader["StatusApproval"].ToString();
            objSOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSOP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            return objSOP;
        }		
		// End tambahan Object
    }
}


