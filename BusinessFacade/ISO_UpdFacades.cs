using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using DataAccessLayer;
using Domain;

namespace BusinessFacade
{
    public class ISO_UpdFacade : AbstractTransactionFacade
    {
        private ISO_Upd objUPD = new ISO_Upd();
        //private ISO_UpdDMD objUPD2 = new ISO_UpdDMD();
        private ISO_DeptM1 objISO = new ISO_DeptM1();
        private ArrayList arrUPD;
        private List<SqlParameter> sqlListParam;

        public    ISO_UpdFacade(object objDomain)
            : base(objDomain)
        {
            objUPD = (ISO_Upd)objDomain;
        }

        public ISO_UpdFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UpdNo", objUPD.updNo));
                sqlListParam.Add(new SqlParameter("@JenisDoc", objUPD.JenisDoc));
                sqlListParam.Add(new SqlParameter("@CategoryUPD", objUPD.CategoryUPd));
                sqlListParam.Add(new SqlParameter("@UpdName", objUPD.UpdName));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@Pic", objUPD.Pic));
                sqlListParam.Add(new SqlParameter("@PlanID", objUPD.PlanID));
                sqlListParam.Add(new SqlParameter("@TglPengajuan", objUPD.TglPengajuan));
                //sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPD.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@RevisiNo", objUPD.RevisiNo));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objUPD.Iso_UserID));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));
                //sqlListParam.Add(new SqlParameter("@DeptA", objUPD.DeptA));
                sqlListParam.Add(new SqlParameter("@Type", objUPD.Type));
                sqlListParam.Add(new SqlParameter("@HeadID", objUPD.HeadID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertISO_UPD");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UpdNo", objUPD.updNo));
                sqlListParam.Add(new SqlParameter("@UpdAlasan", objUPD.updAlasan));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@UpdDok", objUPD.updDok));

                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "sp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApv(TransactionManager transManager)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Status", objUPD.Status));
                //sqlListParam.Add(new SqlParameter("@UpdAlasan", objUPD.updAlasan));
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                //sqlListParam.Add(new SqlParameter("@UpdDok", objUPD.updDok));

                //sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "sp_updateApvHead");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "sp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public int UpdateNoTaskNo(TransactionManager transManager)
        //{
        //    try
        //    {
        //        objUPD = (ISO_Upd)objDomain;
        //        sqlListParam = new List<SqlParameter>();

        //        sqlListParam.Add(new SqlParameter("@ID", objUPD.updID));
        //        sqlListParam.Add(new SqlParameter("@UpdNo", objUPD.updNo));

        //        int intResult = transManager.DoTransaction(sqlListParam, "sp");

        //        strError = transManager.Error;

        //        return intResult;

        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}
        public int UpdateTaskDetailApproval(TransactionManager transManager)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objUPD.idDetail));
                sqlListParam.Add(new SqlParameter("@Approval", objUPD.apv));

                int intResult = transManager.DoTransaction(sqlListParam, "sp");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@Status", objUPD.Status));
                //sqlListParam.Add(new SqlParameter("@TglSelesai", objUPD.TglSelesai));

                int intResult = transManager.DoTransaction(sqlListParam, "sp");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objUPD.idDetail));
                sqlListParam.Add(new SqlParameter("@Status", objUPD.Status));
                //sqlListParam.Add(new SqlParameter("@PointNilai", objUPD.PointNilai));

                int intResult = transManager.DoTransaction(sqlListParam, "sp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int CancelStatusTask(TransactionManager transManager)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@Status", objUPD.Status));

                int intResult = transManager.DoTransaction(sqlListParam, "sp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertUPDDetail(TransactionManager transManager)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;

                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UPDid", objUPD.updID));
                sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPD.TglBerlaku));
                sqlListParam.Add(new SqlParameter("UserID", objUPD.UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertISO_UPDdetail");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@UPDid", objUPD.UPDid));
                sqlListParam.Add(new SqlParameter("@UpdImage", objUPD.Image));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertISO_UpdUpload");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip, " +
            //    "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task where RowStatus>-1");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.UpdNo as NoDokumen,A.UpdName as NamaDokumen,A.TglPengajuan,A.RevisiNo,A.Status,A.TglBerlaku,A.JenisUPD,A.DeptID,A.PIC,A.PlanID from ISO_UPD as A where A.RowStatus>-1");


            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                //arrUPD.Add(new Task());
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public int CountNoUPD()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            ////SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(DestackingID,6,5)) as id from Destacking");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX(SUBSTRING(NoSPP,3,4)),0) as id from SPP where LEFT(SPP.NoSPP,2) = '" + preSPP + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX((UpdNo,2)),0) as id from SPP");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }
        public int GetLastTaskNo()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(TaskNo),0) as Id from ISO_Task");
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
        public decimal RetrieveByPointNilai(int pesType, int targetKe)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_TaskScore where PesType=" + pesType + " and TargetKe=" + targetKe);
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
        public ISO_Upd RetrieveByNo1(string no)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and B.Aktip=1 and A.TaskNo='" + no + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }
        public ISO_Upd RetrieveByNo2(string no)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and B.Aktip=1 and A.TaskNo='" + no + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }
        public ArrayList RetrieveByID(int UpdID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
            //    "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.ID=" + id + " order by B.ID desc");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  A.ID ,A.UpdNo,A.UpdName,A.JenisUPD,A.DeptID,A.PlanID,A.TglPengajuan,A.TglBerlaku,A.RevisiNo,A.PIC,A.Status,A.CreatedBy,A.CreatedTime," +
                "A.LastModifiedBy,A.LastModifiedTime from ISO_UPD as A where A.RowStatus>-1 and A.ID=" + UpdID + " order by A.ID desc");

            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByDeptID(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and A.DeptID=" + deptID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByDeptIDSolved(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID=" + deptID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByDeptIDUnSolved(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai " +
                "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and " +
                "A.DeptID=" + deptID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveByDeptIDSolved2(int deptID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID=" + deptID + " and A.UserGroupID > 100  and A.UserID=" + userID + " order by A.ID desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByDeptIDUnSolved2(int deptID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai " +
                "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0 and B.Aktip=1 and " +
                "A.DeptID=" + deptID + " and A.UserGroupID > 100 and A.UserID=" + userID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveByPT(int ptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DepoID=" + ptID);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByPT2(int ptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.* "+
            //     "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0  and A.DeptID="+ptID+" and B.Approval=0");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.*,B.TargetKe " +
                 "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.DeptID=" + ptID + " and B.Approval in (0,1) ");

            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveByPT3(int ptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.* "+
            //     "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0  and A.DeptID="+ptID+" and B.Approval=0");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.*,B.TargetKe " +
                 "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status =0 and A.DeptID=" + ptID + " and B.Approval=0 and B.Aktip=1 " +
                 "union " +
                "select A.*,B.TargetKe " +
                "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status =2  and A.DeptID=" + ptID +
                " and B.Approval=1 and B.Aktip=1  order by TaskNo");

            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        //public ISO_Upd RetrieveForApprovalHeader()
        //{
        //    Users user = (Users)HttpContext.Current.Session["Users"];
        //    //Users user = (Users)Session["Users"];   
        //    //arrUPD = new ArrayList();
        //    ISO_UpdFacade updF = new ISO_UpdFacade();
        //    ISO_Upd sp = new ISO_Upd();
        //    //int DeptID = user.DeptID;
        //    deptID = user.DeptID;
        //    string userid = string.Empty;
        //    userid = user.UserID;
        //    int IDUser = 0;
        //    IDUser = user.ID;

        //    sp = updF.RetrieveForApvKe(IDUser);
        //    if (sp.StatusApv == 1)
        //    {
        //        sp = updF.RetrieveForHeadApv1(deptID);

        //    }
        //}

        public ArrayList RetrieveForApprovalHeader()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            arrUPD = new ArrayList();
            ISO_Upd sp = new ISO_Upd();
            ISO_UpdFacade spF = new ISO_UpdFacade();
            string userid = users.UserID;
            int IDUser = users.ID;
            int IDUserN = users.ID;
            sp = spF.RetrieveTipeApv(IDUserN);

            if (sp.TipeApv != 0)
            {
                if (sp.TipeApv == 1) { arrUPD = RetrieveForHeadApv1(IDUser); } // Bukan Corporate
                else if (sp.TipeApv == 2) { arrUPD = RetrieveForHeadApv2(); } // Dokumen Khusus
                else if (sp.TipeApv == 3) { arrUPD = RetrieveForHeadApv3(IDUser); } // Optimum Stok Dulu, sekarang buat approval pak bastari               
                //else if (sp.TipeApv == 4) { arrUPD = RetrieveForHeadApv4(); } // Head ISO
                else if (sp.TipeApv == 5) { arrUPD = RetrieveForHeadApv5(); } // Manager ISO
                else if (sp.TipeApv == 6) { arrUPD = RetrieveForHeadApv6(); } // PM   
                else if (sp.TipeApv == 7) { arrUPD = RetrieveForHeadApv7(IDUser, users.DeptID); } // By Pass PM   
                else if (sp.TipeApv == 10) { arrUPD = RetrieveForHapus(); } // Sekretariat ISO  
            }
            return arrUPD;
        }

        public ArrayList RetrieveForApprovalHeaderShare()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            arrUPD = new ArrayList();
            ISO_Upd sp = new ISO_Upd();
            ISO_UpdFacade spF = new ISO_UpdFacade();
            string userid = users.UserID;
            int IDUser = users.ID;
            int IDUserN = users.ID;
            sp = spF.RetrieveTipeApv(IDUserN);

            if (sp.TipeApv != 0)
            {
                arrUPD = RetrieveForHeadApv1Share(users.DeptID,users.UnitKerjaID);
                //if (sp.TipeApv == 1) { arrUPD = RetrieveForHeadApv1(IDUser); } // Bukan Corporate
                //if (sp.TipeApv == 2) { arrUPD = RetrieveForHeadApv2(); } // Dokumen Khusus
                //if (sp.TipeApv == 3) { arrUPD = RetrieveForHeadApv3(IDUser); } // Optimum Stok Dulu, sekarang buat approval pak bastari               
                //if (sp.TipeApv == 4) { arrUPD = RetrieveForHeadApv4(); } // Head ISO
                //if (sp.TipeApv == 5) { arrUPD = RetrieveForHeadApv5(); } // Manager ISO
                //if (sp.TipeApv == 6) { arrUPD = RetrieveForHeadApv6(); } // PM   
                //if (sp.TipeApv == 7) { arrUPD = RetrieveForHeadApv7(IDUser, users.DeptID); } // By Pass PM   
            }
            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv1Share(int DeptID, int PlantID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string Query1 = string.Empty;
            if (DeptID == 10)
            { Query1 = " in (6,10) "; }
            else if (DeptID == 25 || DeptID == 19 || DeptID == 4 || DeptID == 5 || DeptID == 18)
            { Query1 = " in (4,19,5,18) "; }
            else
            { Query1 = "=" + DeptID; }
            string strSQL =
                //" select NoDokumen,NamaDokumen,(select DocCategory from ISO_UpdDocCategory A where A.ID=CategoryID)Kategori," +
                //" (select DocTypeName from ISO_UpdDocType B where B.ID=JenisUPD)Permintaan,case when plantid = 1 then 'Share Dokumen Citeureup' " +
                //" else 'Share Dokumen Karawang' end Keterangan,alasan from ISO_UPDTemp where RowStatus>-1 and Status=1 and DeptID=" + DeptID + " ";
            "  select A.ID,B.NoDokumen,B.NamaDokumen,(select DocCategory from ISO_UpdDocCategory A where A.ID=CategoryID)Kategori," +
            " (select DocTypeName from ISO_UpdDocType B where B.ID=JenisUPD)Permintaan,case when B.plantid = 1 then 'Share Dokumen Citeureup' " +
            " else 'Share Dokumen Karawang' end Keterangan,B.Alasan,A.Type  from ISO_UpdDMD A  INNER JOIN ISO_UPDTemp B ON A.ID=B.IDMasterDokumen " +
            " where B.RowStatus>-1 and A.RowStatus>-1 and A.Dept" + Query1 + " and A.StatusShare=1 and A.Aktif=1 and A.PlantID<>" + PlantID + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeaderShare(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        

        public ArrayList RetrieveForApprovalPM()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.deptID not in (14,7,26,23) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.DeptID=11 " +
                          "union all " +
                           "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.deptID not in (14,7,26,23) order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        //public string Criteria { get; set; }
        public ArrayList RetrieveForApprovalMGR()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            Users users = (Users)HttpContext.Current.Session["Users"];
            int deptID = users.DeptID;

            string userapvUPD = string.Empty;
            switch (deptID)
            {
                case 0:
                    userapvUPD = "in (11)";
                    break;
                case 2:
                    userapvUPD = "in (2,3)";
                    break;
                case 10:
                    userapvUPD = "in (6,10)";
                    break;
                default:
                    userapvUPD = "in (" + deptID + ")";
                    break;
            }

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadISO()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=23 " +
                          " union all " +
                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 2  and A.type=1 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6)" +
                          " union all " +
                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , A.apv," +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=3 and A.type=2 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadIT()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=14";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv4()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.DeptID=23 " +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0) and A.type in (1) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 " +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=3 and A.[Status]=0 and A.RowStatus=0) and A.type in (2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }
        public ArrayList RetrieveForHeadApv3(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          //" where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and B.UserID in (select UserID from ISO_UPDListApv where Apv2=" + IDUser + ")" +

                          " UNION ALL " +

                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0 and A.LastModifiedby <>'bastari') and A.type in (2) " +
                          " and B.RowStatus>-1  and A.RowStatus> -1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv1(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                         " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                         " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                         " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                         " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                         " isnull(G.FileName,'')FileLama " +
                         " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                         " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                         " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                         " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                         " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                         " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                         " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                         " and B.RowStatus>-1 and A.RowStatus> -1 and B.UserID in (select UserID from ISO_UPDListApv where Apv1=" + IDUser + ")";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv7(int IDUser, int DeptID)
        {
            string Query1 = string.Empty;
            if (DeptID == 7)
            {
                Query1 = " union all " +
                         " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                         " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                         " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                         " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                         " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                         " isnull(G.FileName,'')FileLama " +
                         " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                         " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                         " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                         " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                         " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                         " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                         " where A.Apv=2 and A.[Status]=0 and A.type in (2)  " +
                         " and B.RowStatus>-1 and A.RowStatus> -1  and A.DeptID <>7 ";
            }
            else
            { Query1 = ""; }

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                         " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                         " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                         " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                         " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                         " isnull(G.FileName,'')FileLama " +
                         " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                         " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                         " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                         " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                         " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                         " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                         " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) " +
                         " and B.RowStatus>-1 and A.RowStatus> -1 and B.UserID in (select UserID from ISO_UPDListApv where Apv2=" + IDUser + ") "+
                         " "+Query1+"";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHead2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.DeptID Not IN (14,23,7,26,13,15) ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHead4()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          "A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          "When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          "When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          "When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,A.Type,A.jenisUPD as JenisDoc,A.createdby,A.apv,case " +
                          "when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26)" +
                          "then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' " +
                          "when A.Apv = 4 then 'Head ISO' end StatusAPV,B.Alasan,A.PIC " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv in (2,3)  and A.type in (1,2,3) and A.status=0 " +
                          "and B.RowStatus=0" +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          "A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          "When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          "When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          "When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,A.Type,A.jenisUPD as JenisDoc,A.CreatedBy,A.apv,case " +
                          "when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) " +
                          "then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' " +
                          "when A.Apv = 4 then 'Head ISO' end StatusAPV,B.Alasan,A.PIC " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv in (0) and A.type in (1,2) and A.status=0 and A.DeptID=23 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader61(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv5()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.DeptID=23 " +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                //" where (A.Apv=4 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                //" where ((A.Apv=3 and A.[Status]=1 and A.RowStatus=0) or (A.Apv=3 and A.[Status]=0 and A.JenisUPD=3)) and A.type in (1,2,3) " +
                          " where ((A.Apv=2 and A.[Status]=0 and A.RowStatus=0) or " +
                          " (A.Apv=3 and A.[Status]=0 and A.JenisUPD=3)) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.CategoryUPD<>11 " +

                          " union all " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where A.CategoryUPD=11 and A.LastModifiedBy='bastari' and A.Apv=2 and " +
                          " left(convert(char,A.tglpengajuan,112),4)>='2022' ";


            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader61(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHapus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,ISNULL(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where A.Apv=2 and A.[Status]=0 and A.RowStatus=0 and A.JenisUPD=3 and B.RowStatus>-1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader61(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RForHeadApv22()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=23 " +
                          " union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV  " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv in (2,3)  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6)";
            //" union all " +
            //" select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
            //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            //" A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv " +
            //" from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //" and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
            //" and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=23 order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv6()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) and A.DeptID not in (14,23,15,26,7,13) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1" +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) and A.DeptID = 11 " +
                          " and B.RowStatus>-1 and A.RowStatus> -1";


            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForHeadApv2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1  and A.DeptID=7 ";

                          //" UNION ALL " +

                          //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          //" A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          //" jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          //" A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          //" when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          //" isnull(G.FileName,'')FileLama " +
                          //" from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          //" LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          //" LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          //" LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          //" LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          //" LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          //" where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0) and A.type in (2) " +
                          //" and B.RowStatus>-1 and A.RowStatus <> -1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApprovalCorpISOLevel2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=23 " +
                          " union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV  " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 4  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6)";
            //" union all " +
            //" select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
            //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            //" A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv " +
            //" from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //" and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
            //" and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=23 order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApprovalCorpPurchLevel2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=15 " +
                          " union all " +
                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=15 order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApprovalCorpPurchMGR()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=15 ";
            //" union all " +
            //" select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
            //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            //" A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv " +
            //" from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //" and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
            //" and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=15 order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvCorpDeliveryLvl2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (26) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (26) order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvMgrHRD()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=7 " +
                          "union all " +
                //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                //"A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby " +
                //"from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                //"and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type=2 and A.status=0 " +
                //"and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.DeptID in (7,11,13) "+                          
                //"union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvMgrLog()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (10,6) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.DeptID in (10,6) ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvMgrBM()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (2,3) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.DeptID in (2,3) ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvCorpITLvl2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=14 " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv in (1) and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=14 order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }


        public ArrayList RetrieveForApprovalCorpHRDLevel3()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=7 " +
                        "union all " +
                        "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                        " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                        " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                        " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                        " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                        "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                        "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                        "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                        "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID=7 order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvDireksi()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 1  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (26) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (1) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (13) " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type in (2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (13) " +
                           "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID in (26) order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApprovalISO()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            Users users = (Users)HttpContext.Current.Session["Users"];
            int deptID = users.DeptID;

            string userapvUPD = string.Empty;
            switch (deptID)
            {
                case 23:
                    userapvUPD = "in (23)";
                    break;
            }

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveForApvCorpISOLevel1()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            Users users = (Users)HttpContext.Current.Session["Users"];
            int deptID = users.DeptID;

            string userapvUPD = string.Empty;
            switch (deptID)
            {
                case 23:
                    userapvUPD = "in (23)";
                    break;
            }

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.apv = 0  and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV" +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.deptID " + userapvUPD + " order by A.ID desc ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }


        public ISO_Upd RetrieveByNoUPD(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.DeptID=11  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'  ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByNoUPDIT(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=14 " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv in (1) and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=14 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByMGR(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=" + deptID + "  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=" + deptID + "  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByCorpHRD(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=7  " +
                          " union all " +
                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV  " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=7  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByHeadIT(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=14  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveRowStatus(int UserID)
        {
            string strSQL = "select RowStatus RowStatus1 from ISO_UpdTipeApv where UserID=" + UserID + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRW(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }


        public ISO_Upd RetrieveByCorpISO(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV  " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=23  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV  " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=4 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' ";
            //"union all " +
            //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
            //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            //"A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv " +
            //"from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //"and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
            //"and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=23 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByCorpPurch(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=15  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=15 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByCorpPurchMGR(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=15  ";
            //"union all " +
            //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
            //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            //"A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv " +
            //"from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //"and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
            //"and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=15 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByMGRHRD(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type ,A.CreatedBy,A.pic,A.jenisupd as jenisdoc, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=" + deptID + "  " +
                //"union all " +
                //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                //"A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc " +
                //"from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                //"and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type=2 and A.status=0 " +
                //"and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.DeptID in (7,11,13)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc, A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByHeadDel(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type ,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID in (26)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.DeptID in (26)  ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByMgrLog(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type ,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID in (10,6)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.DeptID in (10,6)  ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByMgrBM(string NOSPP, int deptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type ,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID in (2,3)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.DeptID in (2,3)  ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        //public ISO_Upd RetrieveByApvHeadISO(string NOSPP)
        //{
        //    string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
        //                  " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
        //                  " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
        //                  " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
        //                  " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
        //                  "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
        //                  "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
        //                  "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) and A.status=0 " +
        //                  "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "' and A.deptID=23  " +
        //                  "union all " +
        //                  "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
        //                  " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
        //                  " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
        //                  " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
        //                  " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
        //                  "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
        //                  "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
        //                  "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type in (1) and A.status=0 " +
        //                  "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'" +
        //                  "union all " +
        //                  "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
        //                  " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
        //                  " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
        //                  " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
        //                  " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
        //                  "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
        //                  "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
        //                  "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=3 and A.type=2 and A.status=0 " +
        //                  "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrUPD = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectHeader1(sqlDataReader);
        //        }
        //    }

        //    return new ISO_Upd();
        //}


        public ISO_Upd RetrieveByApvHeadISO2(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type in (1) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'" +
                //"union all " +
                //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                //" A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                //" When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                //" When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                //" When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                //"A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                //"from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                //"and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type in (1) and A.status=0 " +
                //"and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'" +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=3 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,5,6) and A.UpdNo = '" + NOSPP + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByNoUPD4(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          "A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          "When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          "When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          "When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv in (2,3) and A.type in (1,2,3) and A.status=0 " +
                          "and B.RowStatus=0 and A.UpdNo = '" + NOSPP + "'" +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          "A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          "When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          "When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          "When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv in (0) and A.type in (1,2) and A.status=0 and A.DeptID=23" +
                          "and B.RowStatus=0  and A.UpdNo = '" + NOSPP + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }
        public ISO_Upd RetrieveByApv5(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "' and A.DeptID=23 " +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          //" where (A.Apv=4 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          //" where ((A.Apv=3 and A.[Status]=1 and A.RowStatus=0) or (A.Apv=3 and A.[status]=0 and A.JenisUPD=3) )and A.type in (1,2,3) " +
                          " where ((A.Apv=2 and A.[Status]=0 and A.RowStatus=0) or (A.Apv=3 and A.[status]=0 and A.JenisUPD=3) )and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "'";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveHapus(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                             " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                             " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                             " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                             " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                             " isnull(G.FileName,'')FileLama,ISNULL(A.RevisiNo,0)RevisiNo " +
                             " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                             " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                             " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                             " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                             " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                             " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                             " where A.Apv=2 and A.[Status]=0 and A.RowStatus=0 and A.JenisUPD=3 " +
                             " and B.RowStatus>-1 and A.UpdNo ='" + NOSPP + "'";                       

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv4(string NOSPP, int IDUser)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "' and A.DeptID=23 " +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0) and A.type in (1) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "'" +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=3 and A.[Status]=0 and A.RowStatus=0) and A.type in (2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "'";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv2(string NOSPP, int IDUser)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "' " +
                          " and B.UserID in (select UserID from ISO_UPDListApv where Apv3=" + IDUser + ")";

                          //" UNION ALL " +

                          //"select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          //" A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          //" jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          //" A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          //" when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          //" isnull(G.FileName,'')FileLama " +
                          //" from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          //" LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          //" LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          //" LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          //" LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          //" LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          //" where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0) and A.type in (2) " +
                          //" and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "'";

            //string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
            //              " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case "+
            //              " when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) "+
            //              " then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 "+
            //              " then 'Head ISO' end StatusAPV " +
            //              " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //              " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1,2,3) " +
            //              " and B.RowStatus>-1 and A.UpdNo ='" + NOSPP + "' and B.UserID in (select UserID from ISO_UPDListApv where Apv1=" + IDUser + ")" +

            //              " union all " +

            //              " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
            //              " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 "+
            //              " then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' "+
            //              " when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
            //              " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
            //              " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type in (3) " +
            //              " and B.RowStatus>-1 and A.UpdNo ='" + NOSPP + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv1(string NOSPP, int IDUser)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "' and B.UserID in (select UserID from ISO_UPDListApv where Apv1=" + IDUser + ")";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv1Share(string NOSPP)
        {
            
            string strSQL =            
            "  select B.NoDokumen,ISNULL(B.NamaDokumen,'')NamaDokumen,(select DocCategory from ISO_UpdDocCategory A where A.ID=CategoryID)Kategori," +
            " (select DocTypeName from ISO_UpdDocType B where B.ID=JenisUPD)Permintaan,case when B.plantid = 1 then 'Share Dokumen Citeureup' " +
            " else 'Share Dokumen Karawang' end Keterangan,B.Alasan,(select Alias from Dept dp where dp.ID=A.Dept and RowStatus>-1)DeptName,A.RevisiNo,A.[Type],A.PlantID PlanID " +
            " from ISO_UpdDMD A  INNER JOIN ISO_UPDTemp B ON A.ID=B.idMasterDokumen " +
            " where B.RowStatus>-1 and A.RowStatus>-1 and A.ID='" + NOSPP + "' and A.StatusShare=1 ";
           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1Share(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv1NotApv(string NOSPP)
        {
            string strSQL =
            //" select NoDocument NoDokumen,DocName NamaDokumen,(select Alias from Dept dp where dp.ID=Dept and RowStatus>-1)DeptName, "+
            //" alasanTidak alasan,(select DocCategory from ISO_UpdDocCategory doc where doc.ID=CategoryUPD)Kategori " +
            //" from ISO_UpdDMD where StatusShare=-3  and Aktif=1 ";          
            " select ID,NoDokumen,NamaDokumen,DeptName,Kategori,UPPER(REPLACE(Permintaan,'usulan ',''))Permintaan,RevisiNo,Alasan,DocName,[Type] from (" +
            " select ID,NoDocument NoDokumen,DocName NamaDokumen,(select Alias from Dept dp where dp.ID=Dept and RowStatus>-1)DeptName," +
            " (select DocCategory from ISO_UpdDocCategory doc where doc.ID=CategoryUPD)Kategori,"+
            " (select doctypename from ISO_UpdDocType upt where upt.id=data1.categoryid)Permintaan,RevisiNo,Alasan,DocName,[Type] " +
            "  from (select A.ID,A.NoDocument,A.DocName,A.CategoryUPD,B.CategoryID,A.AlasanTidak Alasan,A.RevisiNo" +
            " ,A.Dept,A.[Type] from ISO_UpdDMD A INNER JOIN ISO_UPDTemp B ON A.ID=B.idMasterDokumen where A.StatusShare=0 and " +
            " Aktif=1 and A.RowStatus>-1 and B.RowStatus>-1) as Data1 where ID='" + NOSPP + "') as Data2 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderNotShare(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv7(string NOSPP, int IDUser, int DeptID)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A " +
                          " INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                //" where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                //" and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "' and B.UserID in (select UserID from ISO_UPDListApv where Apv2=" + IDUser + ")";
                          " where (((A.Apv=1 and A.[Status]=0 and A.RowStatus>-1) and A.type in (1,2) ) or "+
                          " (A.Apv=2 and A.[Status]=0 and A.RowStatus>-1) and A.type in (2))  and B.RowStatus>-1 and A.UpdNo ='" + NOSPP + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }


        public ISO_Upd RetrieveByApv2(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          " and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type in (1,2) and A.status=0 " +
                          " and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.UpdNo = '" + NOSPP + "' and A.deptID NOT IN (23,14,7,13,26,15)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv6(string NOSPP, int IDUser)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) and A.DeptID not in (14,23,15,26,7,13) " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "'" +

                          " UNION ALL " +

                          " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) and A.DeptID=11 " +
                          " and B.RowStatus>-1 and A.RowStatus <> -1 and A.UpdNo ='" + NOSPP + "'";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByApv3(string NOSPP, int IDUser)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          //" where (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2,3) " +
                          " where (A.Apv=1 and A.[Status]=0 and A.RowStatus=0) and A.type in (1,2) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "' and B.UserID in (select UserID from ISO_UPDListApv where Apv2=" + IDUser + ")" +

                          " UNION ALL " +

                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD ,A.apv, " +
                          " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as " +
                          " jenisdoc,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and " +
                          " A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' " +
                          " when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV,ISNULL(E.NamaFile,'')NamaFile, " +
                          " isnull(G.FileName,'')FileLama,isnull(A.RevisiNo,0)RevisiNo " +
                          " from ISO_UPD as A INNER JOIN ISO_UpdDetail as B ON A.ID=B.UPDid " +
                          " LEFT JOIN Dept as C ON A.DeptID=C.ID " +
                          " LEFT JOIN ISO_UpdDocCategory as D ON A.CategoryUPD=D.ID " +
                          " LEFT JOIN ISO_UPDLampiran as E ON A.ID=E.IDupd  " +
                          " LEFT JOIN ISO_UpdDMD as F ON F.ID=A.IDmaster " +
                          " LEFT JOIN ISO_UPDdistribusiFile as G ON F.ID=G.idMaster " +
                          " where (A.Apv=2 and A.[Status]=0 and A.RowStatus=0 and A.LastModifiedby <>'bastari') and A.type in (2) " +
                          " and B.RowStatus>-1 and A.RowStatus> -1 and A.UpdNo ='" + NOSPP + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByCorpDrksLevel1(string NOSPP)
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.UpdNo = '" + NOSPP + "' and A.deptID in (26)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (2) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.UpdNo = '" + NOSPP + "' and A.deptID in (13)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=0 and A.type in (1) and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.UpdNo = '" + NOSPP + "' and A.deptID in (13)  " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.CreatedBy,A.pic,A.jenisupd as jenisdoc,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) and A.UpdNo = '" + NOSPP + "' and A.deptID in (26)  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveByPM()
        {
            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=1 and A.type=1 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) " +
                          "union all " +
                           "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
                          " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
                          " When A.CategoryUPD = 4 then 'Prosedure' When A.CategoryUPD = 6 Then 'Standar' " +
                          " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
                          " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,A.Pic as PIC,A.jenisUPD as JenisDoc,A.createdby,A.apv,case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' when A.Apv = 2 and A.DeptID in (14,15,23,13,26) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV " +
                          "from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=2 and A.type=2 and A.status=0 " +
                          "and B.RowStatus=0 and A.JenisUPD in (1,2,3,4,5,6,7,8,9,10,11,12,13) order by A.ID desc ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeader6(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        //public ArrayList RetrieveStatus0(string status , int DeptID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.tglpengajuan, " +
        //        " C.DeptName,case when A.CategoryUPD = 1 then 'Pedoman Mutu' when A.CategoryUPD = 2 then 'Instruksi Kerja' " +
        //        " when A.CategoryUPD = 3 then 'Form' when A.CategoryUPD = 4 then 'Prosedur' when A.CategoryUPD = 5 then 'Standar' " +
        //        " when A.CategoryUPD = 6 then 'Bagan Alir' when A.CategoryUPD = 7 then 'Struktur Org' " +
        //        " when A.CategoryUPD = 8 then 'JobDesc' end CategoryUPD " + 
        //        " ,A.IDmaster,B.alasan,A.type,A.deptid from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.categoryupd=D.id " +
        //        "and A.DeptID=C.ID and A.deptid=" + DeptID + " and A.ID=B.UPDid and A.Apv=0 " +
        //        "and B.RowStatus=0 order by A.ID desc ");

        //    strError = dataAccess.Error;
        //    arrUPD = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrUPD.Add(GenerateObjectHeader(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrUPD.Add(new ISO_Upd());

        //    return arrUPD;
        //}

        public ArrayList RetrieveStatus0(string DeptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.tglpengajuan, " +
                " C.DeptName,case when A.CategoryUPD = 1 then 'Pedoman Mutu' when A.CategoryUPD = 2 then 'Instruksi Kerja' " +
                " when A.CategoryUPD = 3 then 'Form' when A.CategoryUPD = 4 then 'Prosedur' when A.CategoryUPD = 5 then 'Standar' " +
                " when A.CategoryUPD = 6 then 'Bagan Alir' when A.CategoryUPD = 7 then 'Struktur Org' " +
                " when A.CategoryUPD = 8 then 'JobDesc' end CategoryUPD " +
                " ,A.IDmaster,B.alasan,A.type,A.deptid,ISNULL((select NamaFile from ISO_UPDLampiran as lp where lp.IDupd=A.ID),'')NamaFile  from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.categoryupd=D.id " +
                "and A.DeptID=C.ID and A.deptid=" + DeptID + " and A.ID=B.UPDid and (A.Apv=0 and A.[Status]=0 and A.RowStatus=0) " +
                "and B.RowStatus=0 order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveFile(int ID)
        {
            string strSQL = 
            " select NamaFile,FileLama from ( "+
            " select ISNULL(B.NamaFile,'-') NamaFile,ISNULL(D.[FileName],'-')FileLama,isnull(D.idMaster,'')idMaster,isnull(D.RowStatus,'')RowStatus " +
            " from ISO_UPD as A " +
            " LEFT JOIN ISO_UPDLampiran as B ON A.ID=B.IDupd and B.RowStatus>-1 " +
            " LEFT JOIN ISO_UpdDMD as C ON A.IDmaster=C.ID   and C.RowStatus>-1 " +
            " LEFT JOIN ISO_UPDdistribusiFile as D ON C.ID=D.idMaster and D.RowStatus>-1 " +
            " where A.ID in (" + ID + ")) as x where RowStatus>-1   ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectFile(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveListUPD(int DeptID)
        {
            string Query1 = string.Empty; string GroupDeptID = string.Empty;

            if (DeptID == 19)
            {
                GroupDeptID = " (4,5,18,19) ";
            }
            else
            {
                GroupDeptID = " (" + DeptID + ") ";
            }
            
            string strSQL = " select ROW_NUMBER() OVER(ORDER BY A.ID DESC)No,A.ID,updno as 'NoDokumen',updname as 'UpdName', " +
                                " (select DocCategory from ISO_UpdDocCategory as C where C.ID=A.CategoryUPD and C.RowStatus>-1)'CategoryUPD', " +
                                " (select substring(DocTypeName,8,30) from ISO_UpdDocType as D where D.ID=A.JenisUPD and D.RowStatus>-1)'JenisUPD', " +
                                " case  when (A.RowStatus=0 and A.Status=0 and A.apv=0 ) then 'Open' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv in (1,2) and A.DeptID not in (14,23,15,13,26))  then 'Apv Manager' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID in (14,23,15,13,26))  then 'Open' " +
                                //" when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID not in (14,23,15,13,26))  then 'Apv PM' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID in (14,23,15,13,26))  then 'Apv Mgr Corp' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=3 and A.Type=2)  then 'Apv Mgr HRD' " +                             
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=4) then 'Apv Mgr ISO'  "+
                                //" when (A.RowStatus=0 and A.Status=0 and A.Apv=5)  then 'Apv Mgr ISO' " +
                                " when (A.RowStatus=0 and A.Status=1 and A.Apv=4)  then 'Actived by ISO' " +
                                " when (A.RowStatus=0 and A.Status=1 and A.Apv=5)  then 'TerDistribusi' " +                                                             
                                //" when (A.RowStatus=0 and A.Status=1 and A.Apv=6)  then 'Aktif'  "+
                                " end 'Apv', " +
                                " LEFT(convert(char,tglpengajuan,113),12) as 'Tanggal', " +
                                " B.Alasan,"+
                                " (select ISNULL(NamaFile,'') from ISO_UPDLampiran lp where lp.IDupd=A.ID and lp.RowStatus>-1)NamaFile,D.Alias DeptName " +
                                " from ISO_UPD as A " +                               
                                " INNER JOIN ISO_UPDdetail as B ON A.ID=B.UPDid " +
                                " LEFT JOIN ISO_UPDLampiran as C ON C.IDupd=A.ID "+
                                " inner join Dept D ON D.ID=A.DeptID " +
                                " where A.DeptID in " + GroupDeptID + " and A.RowStatus>-1 and A.Apv<=3 " +
                                " order by A.ID desc ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader5(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveListUPD2()
        {
            string Query1 = string.Empty;

            string strSQL = " select ROW_NUMBER() OVER(ORDER BY A.ID DESC)No,A.ID,updno as 'NoDokumen',updname as 'UpdName', " +
                                " (select DocCategory from ISO_UpdDocCategory as C where C.ID=A.CategoryUPD and C.RowStatus>-1)'CategoryUPD', " +
                                " (select substring(DocTypeName,8,30) from ISO_UpdDocType as D where D.ID=A.JenisUPD and D.RowStatus>-1)'JenisUPD', " +
                                " case  when (A.RowStatus=0 and A.Status=0 and A.apv=0 ) then 'Open' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv in (1,2) and A.DeptID not in (14,23,15,13,26))  then 'Apv Manager' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=1 and A.DeptID in (14,23,15,13,26))  then 'Open' " +
                                //" when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID not in (14,23,15,13,26))  then 'Apv PM' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=2 and A.DeptID in (14,23,15,13,26))  then 'Apv Mgr Corp' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=3 and A.Type=2)  then 'Apv Mgr HRD' " +
                                " when (A.RowStatus=0 and A.Status=0 and A.Apv=4) then 'Apv Head ISO'  "+
                                //" when (A.RowStatus=0 and A.Status=0 and A.Apv=5)  then 'Apv Mgr ISO' " +
                                //" when (A.RowStatus=0 and A.Status=1 and A.Apv=6)  then 'Aktif'  "+
                                " when (A.RowStatus=0 and A.Status=1 and A.Apv=4)  then 'Actived by ISO' "+
                                " when (A.RowStatus=0 and A.Status=1 and A.Apv=5)  then 'TerDistribusi' "+
                                " end 'Apv', " +
                                " LEFT(convert(char,tglpengajuan,113),12) as 'Tanggal', " +
                                " B.Alasan," +
                                " (select ISNULL(NamaFile,'') from ISO_UPDLampiran lp where lp.IDupd=A.ID and lp.RowStatus>-1)NamaFile,D.Alias DeptName   " +
                                " from ISO_UPD as A " +
                                " INNER JOIN ISO_UPDdetail as B ON A.ID=B.UPDid " +
                                " LEFT JOIN ISO_UPDLampiran as C ON C.IDupd=A.ID " +
                                " inner join Dept D ON D.ID=A.DeptID "+
                                " where A.DeptID not in (23) and A.RowStatus>-1 and A.Apv<=3 " +
                                " order by A.ID desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader5(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }


        public ArrayList RetrieveISO()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select ROW_NUMBER() OVER(ORDER BY ID DESC)No,*,'-'DeptName,'-'Kategori from ( " +
            " select ID,UpdNo,NoDokumen,UpdName,CategoryUPD,TglPengajuan,DeptID,IDmaster,Alasan,Type,ISNULL(NamaFile,'')NamaFile,StatusUPD,IDCatUPD,PlanID PlantID " +
            " from (select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When " +
            " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form' " +
            " When A.CategoryUPD = 4 then 'Prosedur' When A.CategoryUPD = 6 Then 'Standar' " +
            " When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD , " +
            " A.tglpengajuan,A.deptid,A.IDmaster,B.alasan,A.Type,(select top 1 NamaFile from ISO_UPDLampiran as E "+
            " where E.IDupd=A.ID)NamaFile,''StatusUPD " +
            " ,A.CategoryUPD IDCatUPD,A.PlanID from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D "+
            " where  A.CategoryUPD=D.ID and A.DeptID=C.ID and A.ID=B.UPDid " +
            //" and ((A.Apv=2 and A.type=1 and A.status=0 and B.RowStatus=0) or "+
            " and ((A.Apv=3 and A.type=1 and A.status=0 and B.RowStatus=0) or " +
            //" (A.Apv=3 and A.type=2 and A.status=0 and B.RowStatus=0 and A.LastModifiedBy='bastari')) " +
            " (A.Apv=3 and A.type=2 and A.status=0 and B.RowStatus=0 and A.LastModifiedBy='head iso')) " +
            " and A.JenisUPD in (1,2)) as ZX "+
           
            " union all " +
           
            " select A.ID,'-'UpdNo,NoDocument NoDokumen,DocName UpdName,Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When  " +
            " A.CategoryUPD = 2 then 'Instruksi Kerja' When A.CategoryUPD = 3 Then 'Form'  When A.CategoryUPD = 4 then 'Prosedur' " +
            " When A.CategoryUPD = 6 Then 'Standar'  When A.CategoryUPD = 8 then 'Bagan Alir' When A.CategoryUPD = 10 Then 'Struktur Org' " +
            " When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD,A.CreatedTime TglPengajuan,A.Dept,A.ID IDmaster,B.alasan Alasan," +
            " B.Type,B.FileName NamaFile,case when A.plantID=1 then 'Share Ctrp' when A.PlantID=7 then 'Share Krwg' " +
            " end StatusUPD,A.CategoryUPD IDCatUPD,A.PlantID  from ISO_UpdDMD A INNER JOIN ISO_UPDTemp B ON A.ID=B.IDMasterDokumen "+ 
            " where A.StatusShare in (2,3,4) and A.aktif=1" +
            " and A.RowStatus>-1 and B.RowStatus>-1 and B.JenisUPD<>3 ) as Data1";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveISOMusnah()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select ROW_NUMBER() over (order by A.ID asc) as No,A.ID,A.UpdNo,B.NoDokumen,A.UpdName,A.CategoryUPD,(select doccategory from ISO_UpdDocCategory cat where cat.id=A.categoryupd and cat.RowStatus>-1)Kategori,A.tglpengajuan,A.deptid,(select top 1 DeptName from ISO_Dept dp where dp.DeptID=A.DeptID)DeptName,A.IDmaster,B.alasan," +
            " A.type,A.type,'-'NamaFile,'-'StatusUPD,'0'IDCatUPD,isnull(E.PlantID,'')PlantID " +
            " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory as D,ISO_Upddmd as E where A.CategoryUPD=D.ID " +
            " and A.idMaster=E.ID and A.DeptID=C.ID and A.ID=B.UPDid and A.Apv=3 and A.status=0 " +
            " and B.RowStatus=0 and E.aktif > 0 and A.JenisUPD=3  order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }




        public ArrayList RetrieveAPVALL(int DeptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string userapvUPD = string.Empty;
            switch (DeptID)
            {
                case 0:
                    userapvUPD = "in (11)";
                    break;
                case 2:
                    userapvUPD = "in (2,3)";
                    break;
                case 6:
                    userapvUPD = "in (6,10)";
                    break;
                default:
                    userapvUPD = "in (" + DeptID + ")";
                    break;
            }

            string strSQL = "select A.ID,A.UpdNo,B.nodokumen,A.UpdName,A.tglpengajuan, " +
                          " Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When A.CategoryUPD = 2 then 'Instruksi Kerja' " +
                          " When A.CategoryUPD = 3 then 'Form' When A.CategoryUPD = 4 then 'Prosedur' " +
                          " When A.CategoryUPD = 6 then 'Standar' When A.CategoryUPD = 8 then 'Bagan Alir' " +
                          " When A.CategoryUPD = 10 then 'Struktur Org' When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD, " +
                          "A.deptid,A.IDmaster,B.Alasan,A.type " +
                          "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and  A.DeptID=C.ID and A.DeptID " + userapvUPD + " and  A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) " +
                          "and B.RowStatus>-1 and A.jenisupd <> 3 order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveAPVmusnah(int DeptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string userapvUPD = string.Empty;
            switch (DeptID)
            {
                case 0:
                    userapvUPD = "in (11)";
                    break;
                case 2:
                    userapvUPD = "in (2,3)";
                    break;
                case 6:
                    userapvUPD = "in (6,10)";
                    break;
                default:
                    userapvUPD = "in (" + DeptID + ")";
                    break;
            }

            string strSQL = "select A.ID,A.UpdNo,B.nodokumen,A.UpdName,A.tglpengajuan, " +
                          " Case When A.CategoryUPD = 1 then 'Pedoman Mutu' When A.CategoryUPD = 2 then 'Instruksi Kerja' " +
                          " When A.CategoryUPD = 3 then 'Form' When A.CategoryUPD = 4 then 'Prosedur' " +
                          " When A.CategoryUPD = 6 then 'Standar' When A.CategoryUPD = 8 then 'Bagan Alir' " +
                          " When A.CategoryUPD = 10 then 'Struktur Org' When A.CategoryUPD = 11 then 'JobDesc' end CategoryUPD, " +
                          "A.deptid,A.IDmaster,B.Alasan,A.type " +
                          "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and  A.DeptID=C.ID and A.DeptID " + userapvUPD + " and  A.ID=B.UPDid and A.Apv=0 and A.type in (1,2) " +
                          "and B.RowStatus=0 and A.jenisupd=3 order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveAPVHRDkhusus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select A.ID,A.UpdNo,B.nodokumen,A.UpdName, " +
                          "Case When A.CategoryUPD = 1 Then 'Pedoman Mutu' when A.CategoryUPD = 2 Then 'Instruksi Kerja' " +
                          "When A.CategoryUPD = 3 Then 'Form' when A.CategoryUPD = 4 Then 'Prosedur' " +
                          "When A.CategoryUPD = 6 Then 'Standar' when A.CategoryUPD = 8 Then 'Bagan Alir' " +
                          "When A.CategoryUPD = 10 Then 'Struktur Org' when A.CategoryUPD = 11 Then 'JobDesc' end CategoryUPD,  " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.Alasan,A.type " +
                          "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and  A.DeptID=C.ID and  A.ID=B.UPDid and A.Apv=1 and A.Type=2 and B.RowStatus=0 " +
                          "union all " +
                          "select A.ID,A.UpdNo,B.nodokumen,A.UpdName, " +
                          "Case When A.CategoryUPD = 1 Then 'Pedoman Mutu' when A.CategoryUPD = 2 Then 'Instruksi Kerja' " +
                          "When A.CategoryUPD = 3 Then 'Form' when A.CategoryUPD = 4 Then 'Prosedur' " +
                          "When A.CategoryUPD = 6 Then 'Standar' when A.CategoryUPD = 8 Then 'Bagan Alir' " +
                          "When A.CategoryUPD = 10 Then 'Struktur Org' when A.CategoryUPD = 11 Then 'JobDesc' end CategoryUPD,  " +
                          "A.tglpengajuan,A.deptid,A.IDmaster,B.Alasan,A.type " +
                          "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.CategoryUPD=D.ID " +
                          "and  A.DeptID=C.ID and  A.ID=B.UPDid and A.Apv=0 and A.Type=2 and A.deptID=11 " +
                          "and B.RowStatus=0 order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveDeptM(string deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());


            string strSQL = "select namadept from iso_upddept where rowstatus=0 and deptid = " + deptID + " ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }


        public ISO_Upd RetrieveByGroup(int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Users.ID,ISO_Dept.UserGroupID from Users,ISO_Dept where Users.ID = ISO_Dept.UserID and ISO_Dept.UserGroupID = " + groupID);

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd CekStatusApv(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select apv  from iso_upd where rowstatus=0 and ID=" + ID + "";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCekStatusApv(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ArrayList RetrieveByNo(string no)
        {
            //string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
            //    "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.TaskNo='" + no + "'";   
            string strSQL = "select A.ID,B.ID as IdDetail,A.UpdNo,A.UpdName,A.TglPengajuan,,A.RevisiNo,A.TglBerlaku from ISO_UPD as A, ISO_UpdDetail as B where A.ID = B.UPDid and A.RowStatus>-1 and B.RowStatus>-1 and A.UpdNo='" + no + "' order by A.ID desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd RetrieveByJmlLampiran(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select COUNT(ID) as Jumlah from ISO_TaskLampiran where Status >=0 and " +
            "TaskID in (" + Id + ")");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForApvKe(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from ISO_UPDListApv1 where UserID in (select ID from Users where ID=" + IDUser + ") and RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderApvKe(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveTipeApv(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select TipeApv from ISO_UpdTipeApv where RowStatus > -1 and UserID=" + IDUser + "";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectTipeApv(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        //public ISO_Upd RetrieveForUPD(int IDUser)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    string strSQL = "select * from ISO_UPD where rowstatus > -1";
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectHeaderUPD(sqlDataReader);
        //        }
        //    }

        //    return new ISO_Upd();
        //}

        public ISO_Upd RetrieveType(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select Type from ISO_UPD where UserID in (select ID from Users where ID=" + IDUser + ") and RowStatus>-1 and Type=3";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectType(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForApv(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select apv from ISO_UPD where deptID=" + deptID + " and RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderApv(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForApvISO(int IDUser)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select apv as Apv1 from ISO_UPD where DeptID=23 and RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderApvKe1(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForNamaDept(int userDeptID)
        {
            string strSQL = "select DISTINCT(namaDept2) DeptName from ISO_UPDDept where deptIDalias=" + userDeptID + " and RowStatus > -1 and namadept2 is not null ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderNamaDept(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForType(int ID)
        {
            string strSQL = "select [TYPE] TypE from ISO_UpdType where rowstatus > -1 and ID=" + ID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderType(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForJD(int JD)
        {
            string strSQL = "select DocTypeName JenisDokumen from ISO_UpdDocType where RowStatus>-1 and ID=" + JD + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderJD(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd RetrieveForKategory(int ID)
        {
            string strSQL = "select DocCategory CategoryUPd from ISO_UpdDocCategory where RowStatus > -1 and ID=" + ID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderKate(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }


        public ArrayList RetrieveByArrLampiran(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Image from ISO_TaskLampiran where TaskID='" + Id + "' and Status>=0");
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveMaster()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select *,(left(convert(char,tanggal,106),12)) Tanggal2 from iso_updmasterupdate ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeaderM(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }




        public ISO_Upd GenerateObject(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.DeptName = sqlDataReader["deptName"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.CategoryUPD = Convert.ToInt32(sqlDataReader["CategoryUPD"]);
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objUPD.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objUPD.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();            
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            //objUPD.DeptA = sqlDataReader["DeptA"].ToString();          
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();

            return objUPD;
        }
        public ISO_Upd GenerateObjectHeader(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.No = Convert.ToInt32(sqlDataReader["No"]);
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["type"]);
            objUPD.TypE = sqlDataReader["TypE"].ToString();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.StatusUPD = sqlDataReader["StatusUPD"].ToString();
            objUPD.IDCatUPD = Convert.ToInt32(sqlDataReader["IDCatUPD"]);
            //objUPD.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPD.PlanID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD.Kategori = sqlDataReader["Kategori"].ToString();
            return objUPD;

        }

        public ISO_Upd GenerateObjectHeader1(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.Pic = sqlDataReader["PIC"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.JenisDoc = Convert.ToInt32(sqlDataReader["JenisDoc"]);
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.CategoryUPD = Convert.ToInt32(sqlDataReader["CategoryUPD"]);
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.TypE = sqlDataReader["TypE"].ToString();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            objUPD.StatusAPV = sqlDataReader["StatusAPV"].ToString();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.FileLama = sqlDataReader["FileLama"].ToString();
            objUPD.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            return objUPD;

        }

        public ISO_Upd GenerateObject1(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            //objUPD.Jumlah = Convert.ToInt32(sqlDataReader["Jumlah"]);
            return objUPD;
        }

        public ISO_Upd GenerateObject2(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Lampiran = Convert.ToString(sqlDataReader["Lampiran"]);
            return objUPD;
        }
        public ISO_Upd GenerateObjectHeaderM(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.idCategory = sqlDataReader["idCategory"].ToString();
            objUPD.RevNo = sqlDataReader["RevNo"].ToString();
            objUPD.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objUPD.FormNO = sqlDataReader["FormNO"].ToString();
            objUPD.Tanggal2 = sqlDataReader["Tanggal2"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeader5(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.No = Convert.ToInt32(sqlDataReader["No"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.apV = sqlDataReader["Apv"].ToString();
            objUPD.JenisUPd = sqlDataReader["JenisUPD"].ToString();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeader6(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.Pic = sqlDataReader["PIC"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.JenisDoc = Convert.ToInt32(sqlDataReader["JenisDoc"]);
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.TypE = sqlDataReader["TypE"].ToString();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            objUPD.ApV = sqlDataReader["ApV"].ToString();
            objUPD.StatusAPV = sqlDataReader["StatusAPV"].ToString();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.FileLama = sqlDataReader["FileLama"].ToString();

            return objUPD;
        }

        public ISO_Upd GenerateObjectHeader7(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.Pic = sqlDataReader["PIC"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.JenisDoc = Convert.ToInt32(sqlDataReader["JenisDoc"]);
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            return objUPD;

        }

        public ISO_Upd GenerateObjectHeaderApvKe(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.StatusApv = Convert.ToInt32(sqlDataReader["StatusApv"]);
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderUPD(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            return objUPD;
        }

        public ISO_Upd GenerateObjectType(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderApvKe1(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Apv1 = Convert.ToInt32(sqlDataReader["Apv1"]);
            return objUPD;
        }
        public ISO_Upd GenerateObjectHeaderApv(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            return objUPD;
        }
        public ISO_Upd GenerateObjectHeader61(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["UpdNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["updName"].ToString();
            objUPD.Pic = sqlDataReader["PIC"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["tglPengajuan"]);
            objUPD.JenisDoc = Convert.ToInt32(sqlDataReader["JenisDoc"]);
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.TypE = sqlDataReader["TypE"].ToString();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            objUPD.ApV = sqlDataReader["ApV"].ToString();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.FileLama = sqlDataReader["FileLama"].ToString();
            objUPD.StatusAPV = sqlDataReader["StatusAPV"].ToString();

            return objUPD;
        }
        public ISO_Upd GenerateObjectCekStatusApv(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            return objUPD;
        }

        public ISO_Upd GenerateObjectFile(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD.FileLama = sqlDataReader["FileLama"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderNamaDept(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderJD(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.JenisDokumen = sqlDataReader["JenisDokumen"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderKate(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPd"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeaderType(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.TypE = sqlDataReader["TypE"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectTipeApv(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.TipeApv = Convert.ToInt32(sqlDataReader["TipeApv"]);
            return objUPD;
        }

        public ISO_Upd GenerateObjectRW(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.RowStatus1 = Convert.ToInt32(sqlDataReader["RowStatus1"]);
            return objUPD;
        }

        public string CekTipe(string ID)
        {
            string result = "0";
            string StrSql = " select [Type]Type from ISO_UpdDocCategory where rowstatus > -1 and ID=" + ID + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["Type"].ToString();
                }
            }

            return result;
        }

        public string CekCDeptCode(string DeptID)
        {
            string result = "0";
            string StrSql = " select DeptCode from Dept where RowStatus > -1 and ID=" + DeptID + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["DeptCode"].ToString();
                }
            }

            return result;
        }

        public ISO_Upd RetrieveUPDBiasa(string Nomor)
        {
            string strSQL = 
            " select top 1 ID IDmaster,CategoryUPD,[Type]Type,NoDocument NoDokumen,RevisiNo from ISO_UpdDMD "+
            " where NoDocument='" + Nomor + "' and RowStatus > -1 and aktif=2 order by ID desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUPD(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }

        public ISO_Upd RetrieveUPDKhusus(string Nama)
        {
            string strSQL = "select ID IDmaster,CategoryUPD,[Type]Type,NoDocument NoDokumen,RevisiNo from ISO_UpdDMD where DocName='" + Nama + "' and RowStatus > -1 and type=2 and aktif=2 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUPD(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }

        public ISO_Upd GenerateObjectUPD(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPD"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            return objUPD;
        }

        public ArrayList Retrieve_ListUPD(int DeptID)
        {
            string Query1 = string.Empty;

            if (DeptID == 14 || DeptID == 15 || DeptID == 23 || DeptID == 26)
            { Query1 = " (A.Status=0 and A.RowStatus>-1 and A.Type in (1,2) and A.DeptID in (" + DeptID + ") and A.Apv=1)"; }
            else if (DeptID == 19 || DeptID == 18 || DeptID == 4 || DeptID == 5)
            {
                Query1 = "(A.Status=0 and A.RowStatus>-1 and A.Type in (1,2) and A.DeptID in (" + DeptID + ",4,5,18) and A.Apv=0)";
            }
            else
            { Query1 = "(A.Status=0 and A.RowStatus>-1 and A.Type in (1,2) and A.DeptID in (" + DeptID + ") and A.Apv=0)"; }

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select A.ID,A.UpdNo,B.NoDokumen,A.UpdName,(select DocCategory from ISO_UpdDocCategory UP where UP.ID=A.CategoryUPD and " +
            " UP.RowStatus>-1)CategoryUPD,(select DocTypeName from ISO_UpdDocType Doc where Doc.ID=A.JenisUPD and Doc.RowStatus>-1)JenisUPD" +
            " , A.tglpengajuan,B.alasan,A.createdby,"+
            //" case when A.Apv = 0 then 'Open' when A.Apv = 1 then 'Head' " +
            //" when (A.Apv = 2 and A.DeptID in (14,15,23,13,26)) then 'Manager Corporate' when A.Apv = 2 then 'Plant Manager' when A.Apv = 3 " +
            //" then 'Manager HRD' when A.Apv = 4 then 'Head ISO' end StatusAPV "+
            " case when A.Apv = 1 and A.DeptID in (14,15,23,26) then 'Open'  "+
            " when A.Apv = 0 and A.DeptID not in (14,15,23,26) then 'Open' end StatusAPV "+
            " from ISO_UPD as A,ISO_UpdDetail as B,Dept as C,ISO_UpdDocCategory " +
            " as D where A.CategoryUPD=D.ID and A.DeptID=C.ID and A.ID=B.UPDid  and "+Query1+" "+
            //"A.status=0 and B.RowStatus>-1 and A.rowstatus>-1 and A.apv=0  and A.deptID=" + DeptID + " " +
            " order by A.ID desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectListUPD(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd GenerateObjectListUPD(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.updNo = sqlDataReader["updNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["UpdName"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPd"].ToString();
            objUPD.TglPengajuan = Convert.ToDateTime(sqlDataReader["TglPengajuan"].ToString());
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD.StatusAPV = sqlDataReader["StatusAPV"].ToString();
            objUPD.JenisUPd = sqlDataReader["JenisUPd"].ToString();

            return objUPD;
        }

        public ArrayList RetrieveUPDKhusus(int UserDept)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string DeptID = string.Empty;
            if (UserDept == 4 || UserDept == 18 || UserDept == 5 || UserDept == 19)
            {
                DeptID = "4";
            }
            else
            {
                DeptID = UserDept.ToString();
            }

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select ID,NoDocument NoDokumen,DocName UpdName,RevisiNo,isnull(TglBerlaku,'')TglBerlaku from iso_upddmd where dept=" + DeptID + " and " +
            " Type=2 and RowStatus > -1 and Aktif=2 and PlantID=" + users.UnitKerjaID + " and id not in (select idmaster from iso_upd where rowstatus>-1) order by NoDocument desc ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectListUPDBiasa(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveUPDBiasa(int UserDept)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string DeptID = string.Empty;
            if (UserDept == 4 || UserDept == 18 || UserDept == 5 || UserDept == 19)
            {
                DeptID = "4";
            }
            else
            {
                DeptID = UserDept.ToString();
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            "select ID,NoDocument NoDokumen,DocName UpdName,RevisiNo,ISNULL(TglBerlaku,'')TglBerlaku from iso_upddmd where dept=" + DeptID + " and Type=1 and " +
            "RowStatus > -1 and Aktif=2 and PlantID="+ users.UnitKerjaID + " and id not in (select idmaster from iso_upd where rowstatus>-1) order by NoDocument desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectListUPDKhusus(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd GenerateObjectListUPDKhusus(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["UpdName"].ToString();
            objUPD.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD.TglBerlaku = Convert.ToDateTime(sqlDataReader["TglBerlaku"].ToString());
            return objUPD;
        }

        public ISO_Upd GenerateObjectListUPDBiasa(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["UpdName"].ToString();
            objUPD.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD.TglBerlaku = Convert.ToDateTime(sqlDataReader["TglBerlaku"].ToString());
            return objUPD;
        }

        public ISO_Upd RetrieveForID(string NoUPD)
        {
            string strSQL = "select ID from ISO_UPD where UpdNo='" + NoUPD + "' and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderID(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd GenerateObjectHeaderID(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objUPD;
        }


        public ISO_Upd RetrieveForIDMaster(string NoUPD)
        {
            string strSQL = " select RevisiNo from ISO_UpdDMD where ID in (select idmaster from ISO_UPD where RowStatus > -1 " +
                            " and UpdNo='" + NoUPD + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectMasterID(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd GenerateObjectMasterID(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.RevisiNo = Convert.ToString(sqlDataReader["RevisiNo"]);

            return objUPD;
        }

        public ArrayList RetrieveFileLama(string NoUPD)
        {
            string strSQL = " select ISNULL((select [FileName]FileLama from ISO_UPDdistribusiFile A where A.idMaster=idMaster " +
                            " and idMaster in (select idMaster from ISO_UPD B where  B.UpdNo=UpdNo and RowStatus > -1 and B.UpdNo='" + NoUPD + "' ) " +
                            " and A.RowStatus>-1),'-')FileLama from ISO_UpdDMD where ID in (select idmaster from ISO_UPD where RowStatus > -1 " +
                            " and UpdNo='" + NoUPD + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectMasterID1(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveFilePendukung(int NoUPD)
        {
            string strSQL = "  select FileName FileLama from ISO_UPDdistribusiFile where idMaster="+NoUPD+" and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectMasterID1(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveFileShare(string Data)
        {            
            string strSQL = " select FileName FileLama from ISO_UPDTemp where IDMasterDokumen="+Data+"";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectMasterID1(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd GenerateObjectMasterID1(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.FileLama = Convert.ToString(sqlDataReader["FileLama"]);
            //objUPD.FileLama = sqlDataReader["FileLama"].ToString();
            return objUPD;
        }

        public ISO_Upd RetrieveForData(string NoUPD)
        {
            //string strSQL = "select ID,Type,Apv,DeptID,JenisUPD from ISO_UPD where UpdNo='" + NoUPD + "' and RowStatus > -1";
            string strSQL =
            " select A.ID,A.Type,A.Apv,A.DeptID,A.JenisUPD,B.RowStatus,A.LastModifiedBy from ISO_UPD A "+
            " INNER JOIN ISO_UpdDetail B ON A.ID=B.UPDid where A.RowStatus>-1 and B.RowStatus>-1 and A.UpdNo='" + NoUPD + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData(sqlDataReader);
                }
            }

            return new ISO_Upd();
        }

        public ISO_Upd GenerateObjectData(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.apv = Convert.ToInt32(sqlDataReader["apv"]);
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.JenisUPD = Convert.ToInt32(sqlDataReader["JenisUPD"]);
            objUPD.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objUPD.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            return objUPD;
        }

        public int CekIDFile(int id)
        {

            string StrSql = " select ID from ISO_UPDdistribusiFile where idMaster=" + id + " and RowStatus > -1 ";
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

        public string RetrieveFileLama1(string NoUPD)
        {
            string result = "0";
            string StrSql = " select ISNULL((select [FileName]FileLama from ISO_UPDdistribusiFile A where A.idMaster=idMaster " +
                            " and idMaster in (select idMaster from ISO_UPD B where  B.UpdNo=UpdNo and RowStatus > -1 and B.UpdNo='" + NoUPD + "' ) " +
                            " and A.RowStatus>-1),'-')FileLama from ISO_UpdDMD where ID in (select idmaster from ISO_UPD where RowStatus > -1 " +
                            " and UpdNo='" + NoUPD + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    sqlDataReader["FileLama"].ToString();
                }
            }

            return result;
        }

        public ISO_Upd GenerateObjectHeaderShare(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
            objUPD.Kategori = sqlDataReader["Kategori"].ToString();
            objUPD.Permintaan = sqlDataReader["Permintaan"].ToString();
            objUPD.Keterangan = sqlDataReader["Keterangan"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            
            return objUPD;
        }

        //public ISO_Upd GenerateObjectHeaderShare(SqlDataReader sqlDataReader)
        //{
        //    objUPD = new ISO_Upd();
        //    //objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
        //    objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
        //    objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
        //    objUPD.Kategori = sqlDataReader["Kategori"].ToString();
        //    objUPD.Permintaan = sqlDataReader["Permintaan"].ToString();
        //    objUPD.Keterangan = sqlDataReader["Keterangan"].ToString();
        //    objUPD.Alasan = sqlDataReader["Alasan"].ToString();

        //    return objUPD;
        //}

        public ISO_Upd GenerateObjectHeader1Share(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            //objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
            objUPD.Kategori = sqlDataReader["Kategori"].ToString();
            objUPD.Permintaan = sqlDataReader["Permintaan"].ToString();
            objUPD.Keterangan = sqlDataReader["Keterangan"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.PlanID = Convert.ToInt32(sqlDataReader["PlanID"]);

            return objUPD;
        }

        public int cekShare(int DeptID, int PlantID)
        {
            string Query1 = string.Empty;
            if (DeptID == 10)
            { Query1 = "in (10,6)"; }
            else if (DeptID == 25 || DeptID == 19 || DeptID == 18 || DeptID == 5 || DeptID == 4)
            { Query1 = "in (4,19,18,5)"; }
            else { Query1 = " = " + DeptID; }

            string StrSql = " select sum(TotalShare)TotalShare from (select COUNT(ID)TotalShare from ISO_UpdDMD  " +
                " where Dept " + Query1 + " and RowStatus>-1 and StatusShare=1 and PlantID<>" + PlantID + " and aktif=1" +
                " union all "+
                " select '0'TotalShare from ISO_UpdDMD ) as Data1  ";
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

        public int cekShareNotApprove()
        {

            string StrSql = " select sum(TotalShare)TotalShare from (select COUNT(ID)TotalShare from ISO_UpdDMD  " +
                " where RowStatus>-1 and StatusShare=0 and aktif=1 and (ApvPM is null or ApvPM=0)" +
                " union all " +
                " select '0'TotalShare from ISO_UpdDMD ) as Data1  ";
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

        public ArrayList RetrieveForHeadApv1NotApv()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            //" select NoDocument NoDokumen,DocName NamaDokumen,(select Alias from Dept dp where dp.ID=Dept and RowStatus>-1)DeptName, "+
            //" alasantidak alasan,(select DocCategory from ISO_UpdDocCategory doc where doc.ID=CategoryUPD)Kategori " +
            //" from ISO_UpdDMD where StatusShare=-3  and Aktif=1  ";

            " select ID,NoDocument NoDokumen,(select Alias from Dept dp where dp.ID=Dept and RowStatus>-1)DeptName," +
            " (select DocCategory from ISO_UpdDocCategory doc where doc.ID=CategoryUPD)Kategori,"+
            " (select doctypename from ISO_UpdDocType upt where upt.id=data1.categoryid)Permintaan,RevisiNo,Alasan,DocName " +
            " NamaDokumen,Type from (select A.ID,A.NoDocument,A.DocName,A.CategoryUPD,B.CategoryID,A.AlasanTidak Alasan,A.RevisiNo" +
            " ,A.Dept,A.[Type] from ISO_UpdDMD A INNER JOIN ISO_UPDTemp B ON A.ID=B.IDMasterDokumen "+
            " where A.StatusShare=0 and (A.ApvPM is null or A.ApvPM=0) and " +
            " Aktif=1 and A.RowStatus>-1 and B.RowStatus>-1) as Data1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeaderNotShare(sqlDataReader));
                }
            }

            return arrUPD;
        }

        public ISO_Upd GenerateObjectHeaderNotShare(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();

            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
            objUPD.Kategori = sqlDataReader["Kategori"].ToString();
            objUPD.Permintaan = sqlDataReader["Permintaan"].ToString();
            //objUPD.Permintaan = sqlDataReader["Permintaan"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            

            return objUPD;
        }

        public ArrayList RetrieveData(int DeptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select ROW_NUMBER() OVER(ORDER BY A.ID DESC)No,A.ID,A.UpdNo,case when A.IDmaster>0 then (select ISNULL(A1.NoDocument,'') from iso_upddmd A1 where A1.ID=A.IDMaster) else '' end NoDokumen,A.UpdName,(select A.DocCategory from ISO_UpdDocCategory A where A.ID=CategoryUPD and A.RowStatus>-1)" +
            " CategoryUPD,LEFT(CONVERT(CHAR,A.tglpengajuan,106),11)TglPengajuanString,A.DeptID,A.IDmaster,(select E.Alias from dept E where E.ID=A.DeptID and E.RowStatus>-1)DeptName," +
            " B.Alasan2 Alasan,A.Type,(select C.NamaFile " +
            " from ISO_UPDLampiran C where C.IDupd=A.ID and C.RowStatus>-1)LampiranBaru,ISNULL((select [FileName] from ISO_UPDdistribusiFile D " +
            " where D.idMaster=A.IDmaster and D.RowStatus>-1),'')LampiranLama,('Not Apv By'+' '+A.LastModifiedBy)StatusUPD from ISO_UPD A INNER JOIN ISO_UpdDetail B ON A.ID=B.UPDid " +
            " where A.RowStatus=-2 and B.RowStatus=-2 and A.DeptID="+DeptID+" and A.Type=2";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectDataBalik(sqlDataReader));
                }
            }

            return arrUPD;
        }

        public ISO_Upd GenerateObjectDataBalik(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.No = Convert.ToInt32(sqlDataReader["No"]);
            objUPD.updNo = sqlDataReader["updNo"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.UpdName = sqlDataReader["UpdName"].ToString();
            objUPD.CategoryUPd = sqlDataReader["CategoryUPd"].ToString();
            objUPD.TglPengajuanString = sqlDataReader["TglPengajuanString"].ToString();            
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objUPD.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            objUPD.Alasan = sqlDataReader["Alasan"].ToString();            
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            objUPD.LampiranBaru = sqlDataReader["LampiranBaru"].ToString();
            objUPD.LampiranLama = sqlDataReader["LampiranLama"].ToString();
            objUPD.StatusUPD = sqlDataReader["StatusUPD"].ToString();

            return objUPD;
        }

        public ArrayList Retrieve_SubDept(int DeptID)
        {
            string strSQL = " select ID,subdept from iso_updsubdept where rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectSubDept(sqlDataReader));
                }
            }

            return arrUPD;
        }

        public ISO_Upd GenerateObjectSubDept(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.SubDept = sqlDataReader["SubDept"].ToString();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objUPD;
        }

        public int RetrieveHeadID(string NamaSub)
        {

            string StrSql = " select HeadID from iso_updsubdept where rowstatus>-1 and SubDept='" + NamaSub + "'  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["HeadID"]);
                }
            }

            return 0;
        }


    }
}
