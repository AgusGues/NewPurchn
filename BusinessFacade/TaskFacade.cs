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
    public class TaskFacade : AbstractTransactionFacade
    {
        private Task objTask = new Task();
        private ArrayList arrTask;
        private List<SqlParameter> sqlListParam;
        public string Criteria { get; set; }
        public TaskFacade(object objDomain)
        : base(objDomain)
        {
            objTask = (Task)objDomain;
        }
        public TaskFacade()
        {

        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TaskNo", objTask.TaskNo));
                sqlListParam.Add(new SqlParameter("@NewTask", objTask.NewTask));
                sqlListParam.Add(new SqlParameter("@DeptID", objTask.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objTask.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objTask.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objTask.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objTask.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objTask.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objTask.Ket));
                //sqlListParam.Add(new SqlParameter("@TglSelesai", objTask.TglSelesai));
                sqlListParam.Add(new SqlParameter("@Pic", objTask.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objTask.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objTask.UserGroupID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objTask.TargetKe));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTask.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objTask.Iso_UserID));

                sqlListParam.Add(new SqlParameter("@TaskType", objTask.TaskType));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTask");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TaskNo", objTask.TaskNo));
                sqlListParam.Add(new SqlParameter("@NewTask", objTask.NewTask));
                sqlListParam.Add(new SqlParameter("@DeptID", objTask.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objTask.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objTask.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objTask.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objTask.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objTask.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objTask.Ket));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objTask.TglSelesai));
                sqlListParam.Add(new SqlParameter("@Pic", objTask.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objTask.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objTask.UserGroupID));

                sqlListParam.Add(new SqlParameter("@Status", objTask.Status));
                sqlListParam.Add(new SqlParameter("@RowStatus", objTask.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTask.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objTask.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTask");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTask.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTask.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteTask");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateNoTaskNo(TransactionManager transManager)
        {
            try
            {
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objTask.TaskID));
                sqlListParam.Add(new SqlParameter("@TaskNo", objTask.TaskNo));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTaskNo");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objTask.IdDetail));
                sqlListParam.Add(new SqlParameter("@Approval", objTask.App));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTaskDetailApproval");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objTask.ID));
                sqlListParam.Add(new SqlParameter("@Status", objTask.Status));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objTask.TglSelesai));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTaskStatus");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objTask.IdDetail));
                sqlListParam.Add(new SqlParameter("@Status", objTask.Status));
                sqlListParam.Add(new SqlParameter("@PointNilai", objTask.PointNilai));

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
        public int CancelStatusTask(TransactionManager transManager)
        {
            try
            {
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", objTask.ID));
                sqlListParam.Add(new SqlParameter("@Status", objTask.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objTask.Ket));
                sqlListParam.Add(new SqlParameter("@CancelBy", objTask.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelStatusTask");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertTaskDetail(TransactionManager transManager)
        {
            try
            {
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@TaskID", objTask.TaskID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objTask.TargetKe));
                sqlListParam.Add(new SqlParameter("@TglTarget", objTask.TglTarget));
                sqlListParam.Add(new SqlParameter("@Status", objTask.Status));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTaskDetail");

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
                objTask = (Task)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@TaskID", objTask.TaskID));
                sqlListParam.Add(new SqlParameter("@Image", objTask.Image));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTaskLampiran");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip, " +
                "A.CategoryID,A.NilaiBobot,A.Keterangan,A.AlasanCancel,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai from ISO_Task where RowStatus>-1");

            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_TaskScore where PesType="+pesType+" and TargetKe="+targetKe);
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
        public Task RetrieveByNo1(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,"+
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                            "A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,"+
                            "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType "+
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 "+
                            "and B.Aktip=1 and A.TaskNo='" + no + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Task();
        }
        public Task RetrieveByNo2(string no)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and B.Aktip=1 and A.TaskNo='" + no + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Task();
        }
        public ArrayList RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan," +
                            "A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval," +
                            "B.PointNilai,A.TaskType from ISO_Task as A,ISO_TaskDetail as B " +
                            "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  " +
                            "and A.ID=" + id + " order by B.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        public ArrayList RetrieveByDeptID(int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai," +
                            "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                            "A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType " +
                            "from  ISO_Task as A inner join ISO_TaskDetail as B on A.ID=B.TaskID  where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 " +
                            "and A.Status=0 and B.Aktip=1 and A.DeptID=" + deptID + " order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        public ArrayList RetrieveByDeptIDSolved(int deptID, string Nama, string drTgl, string sdTgl)
        {
            string querii = string.Empty;
            if (Nama != "")
            {
                querii =
                " and A.PIC='" + Nama.Trim() + "' and A.DeptID=" + deptID + " and left(convert(char,A.TglSelesai,112),8)>='" + drTgl + "' " +
                " and left(convert(char,A.TglSelesai,112),8)<='" + sdTgl + "' ";
            }
            else
            {
                querii =
                " and A.DeptID=" + deptID + " and left(convert(char,A.TglSelesai,112),8)>='" +drTgl+"' " + " and left(convert(char,A.TglSelesai,112),8)<='" + sdTgl + "' ";
            }

            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai," +
                           "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                           "A.CategoryID,A.AlasanCancel,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy," +
                           "A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType,it.CreatedTime as TglUpload " +
                           "from  ISO_Task as A "+ 
                           "inner join ISO_TaskDetail as B on A.ID=B.TaskID "+
                           "left join Iso_TaskAttachment it on A.ID = it.TaskID "+
                           "where A.ID=B.TaskID " +
                           "and A.RowStatus>-1 and B.RowStatus>-1 and it.RowStatus >-1 and A.Status=2 and B.Aktip=1 " + this.Criteria + querii + 
                           " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }

        //Penambahan agus 11-11-2022
        public ArrayList RetrievePICMGR(string deptID)
        {
            string strSQL =
                "select UPPER(PIC)PIC from ( " +
                "select distinct A.PIC from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID " +

                
                "and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID in('" + deptID + "' ) " +

                " ) as x " +
                " order by PIC desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectPIC(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }


        public ArrayList RetrievePICCMGR(string deptID)
        {
            string strSQL =
                "select UPPER(PIC)PIC from ( " +
                "select distinct A.PIC from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID " +


                "and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID in('" + deptID + "' ) " +

                " ) as x " +
                " order by PIC desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectPIC(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }

        public ArrayList RetrievePIC(string deptID)
        {
            string strSQL =
                "select UPPER(PIC)PIC from ( " +
                "select distinct A.PIC from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID " +

                //"and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.DeptID='"+deptID+"'"+

                //penambahan agus 11-11-2022
                "and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=2 and B.Aktip=1 and A.UserID in(SELECT UserID FROM ISO_Users WHERE RowStatus >-1 AND DeptID ='" + deptID+"' ) " +

                " ) as x " +
                " order by PIC desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectPIC(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }

        public ArrayList RetrieveByDeptIDSolved2(int deptID)
        {
            string strSQL = "select top 1000 A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai," +
                           "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                           "A.CategoryID,A.AlasanCancel,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy," +
                           "A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType,C.CreatedTime as TglUpload " +
                           "from ISO_Task as A,ISO_TaskDetail as B, Iso_TaskAttachment C " +
                           "where A.ID=B.TaskID and C.TaskID = A.ID " +
                           "and A.RowStatus>-1 and B.RowStatus>-1 and C.RowStatus >-1 and A.Status=2 and B.Aktip=1 and A.DeptID=" + deptID+" " + this.Criteria +
                           " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }

        public ArrayList RetrieveByDeptIDUnSolved(int deptID)
        {
            string strdept = string.Empty;
            if (this.Criteria.Trim() == String.Empty)
                //strdept = "and A.DeptID=" + deptID;

            //penambahan agus 11-11-2022
            strdept = "and DeptID=" + deptID;
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,"+
                            "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip," +
                            "A.CategoryID,A.AlasanCancel,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy," +
                            "A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType,C.CreatedTime AS TglUpload " +
                            "from ISO_Task as A "+
                            "LEFT JOIN ISO_TaskDetail as B ON A.ID = B.TaskID " +
                            "LEFT JOIN Iso_TaskAttachment C ON A.ID = C.TaskID " +
                            "where A.RowStatus>-1 and B.RowStatus>-1 " +
                            "and (A.Status in(0)) and B.Aktip=1 and " +

            //" Year(b.TglTargetSelesai)>2016  "+ strdept + this.Criteria+ " order by A.ID desc";
            
            //penambahan agus 11-11-2022
            "Year(b.TglTargetSelesai) > 2016 and A.userid in(select UserID from ISO_Users where RowStatus >-1 " + strdept + this.Criteria + " )  order by A.ID desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrTask.Add(new Task());

            return arrTask;
        }

        public ArrayList RetrieveByDeptIDSolved2(int deptID, int userID, string drtgl, string sdtgl)
        {
            string Criteriane = (deptID == 25 || deptID == 27) ? " or A.PIC=(Select UserName from ISO_Users where ID=(" +
                "Select ISOUserID from ISO_BagianHead where DeptApp in(N'" + deptID + "'))) " : "";
            string withUserID = (GroupUserTask(userID, deptID) == "200") ? " and A.UserID=" + userID : string.Empty;
            string strSQL = "select top 1000 A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,"+
                            "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                            "A.CategoryID,A.AlasanCancel,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy," +
                            "A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType,C.CreatedTime as TglUpload " +
                            "from ISO_Task as A,ISO_TaskDetail as B, Iso_TaskAttachment C " +
                            "where A.ID=B.TaskID and C.TaskID = A.ID " +
                            "and A.RowStatus>-1 and B.RowStatus>-1 and C.RowStatus >-1 and A.Status=2 and B.Aktip=1 and (A.DeptID=" + deptID + Criteriane + this.Criteria + ")" +
                            "and A.UserGroupID > 100 and "+
                            "left(convert(char,A.TglSelesai,112),8)>='" + drtgl + "'  and left(convert(char,A.TglSelesai,112),8)<='" + sdtgl + "'  " +
                            " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrTask.Add(new Task());

            return arrTask;
        }

        public ArrayList RetrieveByDeptIDCancel(int deptID, int userID)
        {
            string Criteriane = (deptID == 25 || deptID == 27) ? " or A.PIC=(Select UserName from ISO_Users where ID=(" +
                "Select ISOUserID from ISO_BagianHead where DeptApp in(N'" + deptID + "'))) " : "";
            string withUserID = (GroupUserTask(userID, deptID) == "200") ? " and A.UserID=" + userID : string.Empty;
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai," +
                            "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip, " +
                            "A.CategoryID,A.AlasanCancel,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy," +
                            "A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID " +
                            "and A.RowStatus>-1 and B.RowStatus>-1 and ( A.Status=9) and B.Aktip=1 and (A.DeptID=" + deptID + Criteriane + this.Criteria + ")" +
                            "and A.UserGroupID > 100  " +
                            " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrTask;
        }
        public ArrayList RetrieveByDeptIDUnSolved2(int deptID, int userID)
        {
            string Criteriane = (deptID == 25 || deptID == 27) ? " or A.PIC=(Select UserName from ISO_Users where ID=(" +
                "Select ISOUserID from ISO_BagianHead where DeptApp in(N'" + deptID + "'))) " : "";
            string withUserID = (GroupUserTask(userID, deptID) == "200") ? " and A.UserID=" + userID : string.Empty;
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai," +
                            "case when A.BagianID>0 then (select BagianName from ISO_Bagian where ID=A.BagianID ) else '' end BagianName,B.Aktip," +
                            "A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType,C.CreatedTime as TglUpload " +
                            "from ISO_Task as A "+
                            "left join ISO_TaskDetail as B on A.ID = B.TaskID " +
                            "left join Iso_TaskAttachment C on A.ID = C.TaskID " +
                            "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and (A.Status between 0 and 1 and  B.Status< 9) and B.Aktip=1 and " +
                            //"(A.DeptID=" + deptID + Criteriane +
                            "(A.DeptID in (select DeptID from iso_dept where userid='" + userID + "' )" + Criteriane +
                            this.Criteria +") and A.UserGroupID > 100  order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrTask.Add(new Task());

            return arrTask;
        }
        public ArrayList RetrieveByPT(int ptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,B.TglTargetSelesai,'' as BagianName,B.Aktip," +
                "A.CategoryID,A.NilaiBobot,A.AlasanCancel,A.Keterangan,A.TglSelesai,A.PIC,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DepoID=" + ptID);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        public ArrayList RetrieveByPT2(int ptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.* "+
            //     "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.Status=0  and A.DeptID="+ptID+" and B.Approval=0");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.*,B.TargetKe " +
                 "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.DeptID=" + ptID + " and B.Approval in (0,1) ");

            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        public ArrayList RetrieveByPT3(string ptID)
        {
            string strSQL = "select A.*,B.TargetKe " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and "+
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)"+
                            "and A.DeptID in(" + ptID + ") and B.Approval=0 and B.Aktip=1 " +
                            "union all "+
                            "select A.*,B.TargetKe " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)" +
                            "and A.DeptID in(" + ptID + ") and B.Approval=1 and B.Aktip=1 " +
                            "union all " +
                            "select A.*,B.TargetKe " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and "+
                            "B.RowStatus>-1 and (A.Status =2 or A.Status=9) "+
                            "and A.DeptID in(" + ptID + ") and B.Approval=1 and B.Aktip=1  order by TaskNo";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            

            return arrTask;
        }
        public ArrayList RetrieveByPT3(string ptID,string UserGroupID)
        {
            string usergroup="";
            switch (UserGroupID)
            {
                case "50": usergroup = " and (A.UsergroupID=100)"; break;
                case "100": usergroup=" and A.UserGroupID=200 and A.DeptID <> 27 " ; break;
            }
            string strSQL = "SELECT distinct * FROM (select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)" +
                            "and A.DeptID in(" + ptID + ")" + usergroup + " and B.Approval=0 and B.Aktip=1 " +
                            "union all " +
                            "select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)" +
                            "and A.DeptID in(" + ptID + ")" + usergroup + " and B.Approval=1 and B.Aktip=1 " +
                            "union all " +
                            "select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =2 or A.Status=9) " +
                            "and A.DeptID in(" + ptID + ")" + usergroup + " and B.Approval=1 and B.Aktip=1 "+
                            "union all " +
                            "select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)" +
                            "and A.DeptID in(" + ptID + ") and pic in (select username from iso_Users where rowstatus>-1 and  userid in (select userid from iso_dept A where A.rowstatus>-1 " + usergroup + ")) and B.Approval=0 and B.Aktip=1 " +
                            "union all " +
                            "select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =0 or A.Status =9)" +
                            "and A.DeptID in(" + ptID + ")and pic in (select username from iso_Users where rowstatus>-1 and  userid in (select userid from iso_dept A where A.rowstatus>-1 " + usergroup + ")) and B.Approval=1 and B.Aktip=1 " +
                            "union all " +
                            "select A.*,B.TargetKe,B.TglTargetSelesai " +
                            "from ISO_Task as A,ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and " +
                            "B.RowStatus>-1 and (A.Status =2 or A.Status=9) " +
                            "and A.DeptID in(" + ptID + ")and pic in (select username from iso_Users where rowstatus>-1 and  userid in (select userid from iso_dept A where A.rowstatus>-1 " + usergroup + ")) and B.Approval=1 and B.Aktip=1 " +
                            ") as x WHERE Year(TglTargetSelesai)>2016 order by TaskNo";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectHeader(sqlDataReader));
                }
            }


            return arrTask;
        }
        public Task RetrieveByGroup(int groupID)
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

            return new Task();
        }
        public ArrayList RetrieveByIDnew(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai," +
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,A.AlasanCancel," +
                            "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime," +
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType, C.CreatedTime as TglUpload " +
                            "from ISO_Task as A left join ISO_TaskDetail B on B.TaskID = A.ID "+
                            "left Join Iso_TaskAttachment C on A.ID = C.TaskID "+
                            "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.ID=" + no;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrTask;
        }
        public ArrayList RetrieveByNo(string no)
        {
            string strSQL = "select A.ID,B.ID as idDetail, A.TaskNo,A.TaskName,A.DeptID,A.BagianID,B.TargetKe,A.TglMulai,"+
                            "B.TglTargetSelesai,'' as BagianName,B.Aktip,A.AlasanCancel," +
                            "A.CategoryID,A.NilaiBobot,A.Keterangan,A.TglSelesai,A.PIC,B.Status,A.CreatedBy,A.CreatedTime,"+
                            "A.LastModifiedBy,A.LastModifiedTime,B.Approval,B.PointNilai,A.TaskType from ISO_Task as A,"+
                            "ISO_TaskDetail as B where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.TaskNo='" + no + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Task where Status=0 and DepoID=" + ptID);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        public Task RetrieveByJmlLampiran(int Id)
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

            return new Task();
        }
        public ArrayList RetrieveByArrLampiran(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Image from ISO_TaskLampiran where TaskID='" + Id + "' and Status>=0");
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }
        //public ArrayList RetrieveByCriteria(int ptID, string strField, string strValue)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PT,A.TransactionCode,A.TransactionName,D.BanksID,B.BankName,A.BankAccountID,D.BankAccountNo,A.COAID,C.AccountCode,C.AccountName,A.AccountRef,A.TransactionGroup,A.NoUrut,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.DC from Task A,Banks as B,ChartOfAccount as C,BankAccount D where D.BanksID= B.ID and A.COAId = C.ID and A.BankAccountID = D.ID and D.BanksID = B.ID and A.RowStatus = 0 and A.PTiD=" + ptID + " and " + strField + " like '%" + strValue + "%'");
        //    strError = dataAccess.Error;
        //    arrTask = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrTask.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrTask.Add(new Task());

        //    return arrTask;
        //}
        public Task GenerateObject(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            objTask.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTask.IdDetail = Convert.ToInt32(sqlDataReader["idDetail"]);
            objTask.TaskNo = sqlDataReader["TaskNo"].ToString();
            objTask.NewTask = sqlDataReader["TaskName"].ToString();
            objTask.DeptID = Convert.ToInt16(sqlDataReader["DeptID"]);
            objTask.BagianID = Convert.ToInt16(sqlDataReader["BagianID"]);
            objTask.BagianName = sqlDataReader["BagianName"].ToString();
            objTask.TargetKe = Convert.ToInt16(sqlDataReader["TargetKe"]);
            objTask.TglMulai = Convert.ToDateTime(sqlDataReader["TglMulai"]);
            objTask.TglTarget = Convert.ToDateTime(sqlDataReader["TglTargetSelesai"]);
            objTask.CategoryID = Convert.ToInt16(sqlDataReader["CategoryID"]);
            objTask.BobotNilai = Convert.ToInt16(sqlDataReader["NilaiBobot"]);
            objTask.Ket = sqlDataReader["Keterangan"].ToString();
            //objTask.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);
            if (string.IsNullOrEmpty(sqlDataReader["TglSelesai"].ToString()))
                objTask.TglSelesai = DateTime.MinValue;
            else
                objTask.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);

            objTask.Pic = sqlDataReader["Pic"].ToString();
            objTask.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTask.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objTask.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTask.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objTask.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objTask.Aktip = Convert.ToInt32(sqlDataReader["Aktip"]);
            objTask.App = Convert.ToInt32(sqlDataReader["Approval"]);
            objTask.PointNilai = Convert.ToInt32(sqlDataReader["PointNilai"]);
            objTask.AlasanCancel = (sqlDataReader["AlasanCancel"] == DBNull.Value) ? string.Empty : sqlDataReader["AlasanCancel"].ToString();
            objTask.TaskType = Convert.ToInt32(sqlDataReader["TaskType"]);
            objTask.TglUpload = sqlDataReader["TglUpload"].ToString();
            return objTask;

        }
        public Task GenerateObjectHeader(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            objTask.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objTask.IdDetail = Convert.ToInt32(sqlDataReader["idDetail"]);
            objTask.TaskNo = sqlDataReader["TaskNo"].ToString();
            objTask.NewTask = sqlDataReader["TaskName"].ToString();
            objTask.DeptID = Convert.ToInt16(sqlDataReader["DeptID"]);
            objTask.BagianID = Convert.ToInt16(sqlDataReader["BagianID"]);
            objTask.TargetKe = Convert.ToInt16(sqlDataReader["TargetKe"]);
            objTask.TglMulai = Convert.ToDateTime(sqlDataReader["TglMulai"]);
            //objTask.TglTarget = Convert.ToDateTime(sqlDataReader["TglTargetSelesai"]);
            objTask.CategoryID = Convert.ToInt16(sqlDataReader["CategoryID"]);
            objTask.BobotNilai = Convert.ToInt16(sqlDataReader["NilaiBobot"]);
            objTask.Ket = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["TglSelesai"].ToString()))
                objTask.TglSelesai = DateTime.MinValue;
            else
                objTask.TglSelesai = Convert.ToDateTime(sqlDataReader["TglSelesai"]);
            objTask.Pic = sqlDataReader["Pic"].ToString();
            objTask.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTask.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objTask.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTask.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objTask.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objTask.AlasanCancel = (sqlDataReader["AlasanCancel"] == DBNull.Value) ? string.Empty : sqlDataReader["AlasanCancel"].ToString();

            return objTask;

        }
        public Task GenerateObject1(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            objTask.Jumlah = Convert.ToInt32(sqlDataReader["Jumlah"]);
            return objTask;
        }
        public Task GenerateObject2(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            objTask.Image = Convert.ToString(sqlDataReader["Image"]);
            return objTask;
        }
        public string GroupUserTask(int UserID,int DeptID)
        {
            string strSQL = "Select UserGroupID from ISO_Dept where UserID=" + UserID + " and DeptID=" + DeptID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["UserGroupID"].ToString();
                }
            }

            return string.Empty;
        }
     public DateTime GetDateStartTaskByDeptID(int deptID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select min(TglMulai) as TglMulai from ISO_Task where DeptID="+deptID+" and Status=0 and UserID="+userID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["TglMulai"]);
                }
            }

            return DateTime.MinValue;
        }
        public DateTime GetDateEndTaskByDeptID(int deptID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select min(TglMulai)+4 as TglMulai from ISO_Task where DeptID=" + deptID + " and Status=0 and UserID=" + userID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["TglMulai"]);
                }
            }

            return DateTime.MinValue;
        }

        public ArrayList RetrieveDeptNama(int tanda, int DeptID)
        {
            string A = string.Empty;
            if (tanda == 0)
            {
                A = " DeptID=" + DeptID + "";
            }
            else
            {                
                A = " DeptID not in (1,16,21,28) order by Departemen ";
            }

            string strSQL =
            " select * from ( "+
            " select xx.ID DeptID,xx.Alias Departemen from (select distinct DeptID from ISO_Task where RowStatus>-1) as x "+
            " inner join Dept xx ON x.DeptID=xx.ID ) as x1 "+
            " where "+A+" ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTask = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTask.Add(GenerateObjectDept(sqlDataReader));
                }
            }
            else
                arrTask.Add(new Task());

            return arrTask;
        }

        private Task GenerateObjectDept(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            objTask.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);            
            objTask.Departemen = sqlDataReader["Departemen"].ToString();
           
            return objTask;
        }

        private Task GenerateObjectPIC(SqlDataReader sqlDataReader)
        {
            objTask = new Task();
            //objTask.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objTask.Pic = sqlDataReader["Pic"].ToString();

            return objTask;
        }

        public int RetrieveTanda(int ID)
        {
            string StrSql =
            " select sum(ID)ID from ( " +
            " select ID from ISO_TaskExc where UserID="+ID+" and RowStatus>-1 " +
            " union all " +
            " select 0 ) as x ";
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
    }
}
