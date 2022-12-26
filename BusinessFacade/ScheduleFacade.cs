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
    public class ScheduleFacade : AbstractTransactionFacade
    {
        private Schedule objSchedule = new Schedule();
        private ArrayList arrSchedule;
        private List<SqlParameter> sqlListParam;

        public ScheduleFacade(object objDomain)
            : base(objDomain)
        {
            objSchedule = (Schedule)objDomain;
        }

        public ScheduleFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ScheduleNo", objSchedule.ScheduleNo));
                sqlListParam.Add(new SqlParameter("@ScheduleDate", objSchedule.ScheduleDate));
                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objSchedule.ExpedisiDetailID));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objSchedule.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@Rate", objSchedule.Rate));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSchedule.Keterangan));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objSchedule.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@DepoID", objSchedule.DepoID));    
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSchedule.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SendSMS", objSchedule.SendSMS));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSchedule");

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
                sqlListParam.Add(new SqlParameter("@ID", objSchedule.ID));
                sqlListParam.Add(new SqlParameter("@ScheduleNo", objSchedule.ScheduleNo));
                sqlListParam.Add(new SqlParameter("@ScheduleDate", objSchedule.ScheduleDate));
                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objSchedule.ExpedisiDetailID));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objSchedule.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@Status", objSchedule.Status));
                sqlListParam.Add(new SqlParameter("@Rate", objSchedule.Rate));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSchedule.Keterangan));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objSchedule.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@DepoID", objSchedule.DepoID));    
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSchedule.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SendSMS", objSchedule.SendSMS));
                sqlListParam.Add(new SqlParameter("@ApprovalBy", objSchedule.ApprovalBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSchedule");

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
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSchedule.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSchedule.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSchedule");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 order by A.ID");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and B.DepoID = " + depoID + " order by A.ID desc ");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;

        }

        public ArrayList RetrieveByDepo2(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;

        }
        public ArrayList RetrieveScheduleGantung(string th)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select distinct A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,A.Status as ScheduleStatus, D.Status as ScheduleDetailStatus,isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=A.ID and SuratJalan.Status>-1 and SuratJalanDetail.ScheduleDetailID=D.ID),0) as SJaktip,"+
                "isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=A.ID and SuratJalan.Status=-1 and SuratJalanDetail.ScheduleDetailID=D.ID),0) as SJcancel,isnull(C.KodeRoute,'') as KodeRoute " +
                "from Schedule as A,Expedisi as B,ExpedisiDetail as C,ScheduleDetail as D where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus > -1 and A.DepoID in (1,7) and A.ID=D.ScheduleID and D.Status>-1 and LEFT(convert(varchar,ScheduleDate,112),4) >= '"+th+"' ) as aa where aa.SJcancel=-1 and SJaktip=0 and ScheduleDetailStatus>-1 order by ScheduleNo");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;

        }

        public ArrayList RetrieveByDepo(int depoID,string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and B.DepoID = " + depoID + " and A.ScheduleNo = '" + scheduleNo + "' order by A.ID");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;

        }

        public ArrayList RetrieveByDepoCriteria(int depoID, string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and B.DepoID = " + depoID + " and A.ScheduleNo like '%" + scheduleNo + "%' order by A.ID");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;

        }

        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 order by id desc ");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }


        public ArrayList RetrieveOpenStatus(string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and A.ScheduleNo = '" + scheduleNo + "'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }

        public ArrayList RetrieveOpenStatusCriteria(string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and A.ScheduleNo like '%" + scheduleNo + "%'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }

        public ArrayList RetrieveOpenStatusByDepo(int depoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and B.DepoID = " + depoId);
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }

        public ArrayList RetrieveOpenStatusByDepoPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and B.DepoID in (1,7) ");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }

        public Schedule RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Schedule();
        }
        public Schedule CekSchedule(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus > -1 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Schedule();
        }
        public Schedule RetrieveByNo(string strScheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.ScheduleNo = '" + strScheduleNo + "'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Schedule();
        }

        public Schedule RetrieveByLikeNo(string strScheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.ScheduleNo like '%" + strScheduleNo + "%'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Schedule();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Rate,A.Keterangan,A.DepoID,A.Status,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.JumlahMuatan,A.Approval,A.SendSMS,isnull(C.KodeRoute,'') as KodeRoute from Schedule as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }


        public ArrayList RetrieveByOP(string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(A.ScheduleNo) as ScheduleNo from Schedule as A,ScheduleDetail as B where B.TypeDoc = 0 and A.ID = B.ScheduleID and B.DocumentNo = '" + opNo + "'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(sqlDataReader["ScheduleNo"].ToString());
                }
            }
            
            return arrSchedule;
        }

        public ArrayList RetrieveByTO(string toNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(A.ScheduleNo) as ScheduleNo from Schedule as A,ScheduleDetail as B where B.TypeDoc = 1 and A.ID = B.ScheduleID and B.DocumentNo = '" + toNo + "'");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(sqlDataReader["ScheduleNo"].ToString());
                }
            }

            return arrSchedule;
        }

        public int RetrieveScheduleTOByDocument(int documentId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(ScheduleID) from Schedule as A,ScheduleDetail as B where A.ID = B.ScheduleID and B.DocumentID = " + documentId + " and A.Status = 0 and B.TypeDoc = 1");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            int i = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    i = i + 1;
                }
            }

            return i;
        }
        public DateTime GetServerDate()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select getdate() as ServerDate");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return DateTime.Parse(sqlDataReader["ServerDate"].ToString());
                }
            }

            return DateTime.MinValue;
        }

        public int CekJumlahGantungan(int documentId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(ScheduleID) from Schedule as A,ScheduleDetail as B where A.ID = B.ScheduleID and B.DocumentID = " + documentId + " and A.Status = 0 and B.TypeDoc = 1");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            int i = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    i = i + 1;
                }
            }

            return i;
        }
        public int CekJumlahGantungan(string Thn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(distinct ScheduleNo) as Jml from ( "+
                "select ScheduleNo,Schedule.Status as ScheduleStatus, ScheduleDetail.Status as ScheduleDetailStatus,isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=Schedule.ID and SuratJalan.Status>-1 and SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID),0) as SJaktip, "+
                "isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=Schedule.ID and SuratJalan.Status=-1 and SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID),0) as SJcancel " +
                "from Schedule,ScheduleDetail where Schedule.ID=ScheduleDetail.ScheduleID and Schedule.Status>-1 and ScheduleDetail.Status>-1 and LEFT(convert(varchar,ScheduleDate,112),4) >= '"+Thn+"' and Schedule.DepoID in (1,7)) as aa where aa.SJcancel=-1 and SJaktip=0 and ScheduleDetailStatus>-1");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["Jml"].ToString());
                }
            }

            return 0;
        }
        public int CekJumlahGantungan2()
        {
            string strQ = "select Jumlah1 as Jml from ScheduleInfo WHERE KodeInfo='A01'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQ);

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["Jml"].ToString());
                }
            }

            return 0;
        }
        public int CekGantunganPerDetailID(string scheNo, int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(distinct ScheduleNo) as Jml  from ("+
                "select ScheduleNo,Schedule.Status as ScheduleStatus, ScheduleDetail.Status as ScheduleDetailStatus,isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=Schedule.ID and SuratJalan.Status>-1 and SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID),0) as SJaktip, isnull((select top 1 Status from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.ScheduleID=Schedule.ID and SuratJalan.Status=-1 and SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID),0) as SJcancel,(select top 1 OPNo from OP where OP.ID=ScheduleDetail.DocumentID) as OPno,Description from Schedule,ScheduleDetail,Items where Schedule.ID=ScheduleDetail.ScheduleID and Schedule.Status>-1 and ScheduleDetail.Status>-1 and ScheduleDetail.ItemID=Items.ID and Schedule.DepoID in (1,7) and ScheduleNo='"+scheNo+"' and  ScheduleDetail.ID="+id+") as aa where SJcancel=-1 and SJaktip=0 and ScheduleDetailStatus>-1 ");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["Jml"].ToString());
                }
            }

            return 0;
        }
        public int RetrieveScheduleOPByDocument(int documentId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(ScheduleID) from Schedule as A,ScheduleDetail as B where A.ID = B.ScheduleID and B.DocumentID = " + documentId + " and A.Status = 0 and B.TypeDoc = 0");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            int i = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    i = i + 1;
                }
            }

            return i;
        }
        public int RetrieveScheduleSJTOByDocument(int documentId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(ScheduleID) from ScheduleDetail as A,TransferOrder as B where A.ScheduleID not in (select ScheduleID from SuratJalanTO where DocumentID = " + documentId + ") and A.TypeDoc = 1 and A.DocumentID = B.ID and B.Status in (3,5)");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            int i = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    i = i + 1;
                }
            }

            return i;
        }
        public int RetrieveScheduleSJOPByDocument(int documentId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(ScheduleID) from ScheduleDetail as A,OP as B where A.ScheduleID not in (select ScheduleID from SuratJalan where DocumentID = " + documentId + ") and A.TypeDoc = 0 and A.DocumentID = B.ID and B.Status in (3,5)");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            int i = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    i = i + 1;
                }
            }

            return i;
        }
        public int RetrieveSelisihHari(string tglAktualKirim, int SchedID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select datediff(day,ScheduleDate, '" + tglAktualKirim + "')-((datediff(WEEK,ScheduleDate,'" + tglAktualKirim + "'))*2) as SelisihHari from Schedule where ID = 210481");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["SelisihHari"].ToString());
                }
            }

            return 0;
        }
        public ArrayList CekJumlahOPbelumTerSchedule(int depoID, int th)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case when Status=2 then 'Total OP Baru' else 'Total OP-Parsial' end Keterangan,COUNT(UmurOP) as JumlahMuatan from ( "+
                "select distinct OPNo,OP.Status,DATEDIFF(DAY,OP.ApproveSCDate,GETDATE()) as UmurOP from OP,OPDetail where OP.ID=OPDetail.OPID and YEAR(OP.ApproveSCDate)="+th+" and Status in (2,3) and DepoID= "+depoID+
                " ) AS A where UmurOP>=4 group by Status");
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }
        public ArrayList CekJumlahOPbelumTerSchedule2(string kodeInfo)
        {
            string strQ = "select Keterangan,Jumlah1 as JumlahMuatan from ScheduleInfo WHERE KodeInfo='" + kodeInfo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQ);
            strError = dataAccess.Error;
            arrSchedule = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSchedule.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSchedule.Add(new Schedule());

            return arrSchedule;
        }
        
        public Schedule GenerateObject(SqlDataReader sqlDataReader)
        {
            objSchedule = new Schedule();
            objSchedule.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSchedule.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSchedule.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objSchedule.ExpedisiDetailID = Convert.ToInt32(sqlDataReader["ExpedisiDetailID"]);
            objSchedule.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objSchedule.CarType = sqlDataReader["CarType"].ToString();
            objSchedule.TotalKubikasi = Convert.ToDecimal(sqlDataReader["TotalKubikasi"]);
            objSchedule.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSchedule.Rate = sqlDataReader["Rate"].ToString();
            objSchedule.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSchedule.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSchedule.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objSchedule.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSchedule.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSchedule.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSchedule.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objSchedule.MinimalMuatan = Convert.ToDecimal(sqlDataReader["JumlahMuatan"]);
            objSchedule.ApprovalKubikasi = Convert.ToInt32(sqlDataReader["Approval"]);
            objSchedule.SendSMS = Convert.ToInt32(sqlDataReader["SendSMS"]);
            objSchedule.KodeRoute = sqlDataReader["KodeRoute"].ToString();

            return objSchedule;
        }
        public Schedule GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSchedule = new Schedule();
            objSchedule.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSchedule.MinimalMuatan = Convert.ToDecimal(sqlDataReader["JumlahMuatan"]);

            return objSchedule;
        }

    }
}

