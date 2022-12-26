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
    public class SuratJalanFacade : AbstractTransactionFacade
    {
        private SuratJalan objSuratJalan = new SuratJalan();
        private ArrayList arrSuratJalan;
        private List<SqlParameter> sqlListParam;

        public SuratJalanFacade(object objDomain)
            : base(objDomain)
        {
            objSuratJalan = (SuratJalan)objDomain;
        }

        public SuratJalanFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID", objSuratJalan.OPID));
                sqlListParam.Add(new SqlParameter("@ScheduleID", objSuratJalan.ScheduleID));
                sqlListParam.Add(new SqlParameter("@PoliceCarNo", objSuratJalan.PoliceCarNo));
                sqlListParam.Add(new SqlParameter("@DriverName", objSuratJalan.DriverName));
                sqlListParam.Add(new SqlParameter("@Status", objSuratJalan.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSuratJalan.Keterangan));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objSuratJalan.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSuratJalan.CreatedBy));
                sqlListParam.Add(new SqlParameter("@TglKirimActual", objSuratJalan.TglKirimActual));

                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objSuratJalan.ExpedisiDetailID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSuratJalan");

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
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@SuratJalanNo", objSuratJalan.SuratJalanNo));
                sqlListParam.Add(new SqlParameter("@OPID", objSuratJalan.OPID));
                sqlListParam.Add(new SqlParameter("@ScheduleID", objSuratJalan.ScheduleID));
                sqlListParam.Add(new SqlParameter("@PoliceCarNo", objSuratJalan.PoliceCarNo));
                sqlListParam.Add(new SqlParameter("@DriverName", objSuratJalan.DriverName));   
                sqlListParam.Add(new SqlParameter("@Status", objSuratJalan.Status));
                sqlListParam.Add(new SqlParameter("@Cetak", objSuratJalan.Cetak));           
                sqlListParam.Add(new SqlParameter("@Keterangan", objSuratJalan.Keterangan));
                sqlListParam.Add(new SqlParameter("@CountPrint", objSuratJalan.CountPrint));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objSuratJalan.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.CreatedBy));
                sqlListParam.Add(new SqlParameter("@TglKirimActual", objSuratJalan.TglKirimActual));

                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objSuratJalan.ExpedisiDetailID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSuratJalan");

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
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSuratJalan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelPostingReceive(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelPostingReceive");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int CancelSuratJalan(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objSuratJalan.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                //sqlListParam.Add(new SqlParameter("@DistID", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@SubDistID", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@EmailAddress", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@Pic", objSuratJalan.Pic));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelSuratJalan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdatePostingDate(TransactionManager transManager,int flag)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@Flag", flag));
                sqlListParam.Add(new SqlParameter("@ReceiveDate", objSuratJalan.ReceiveDate));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePostingDateSJ");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePostingStatus(TransactionManager transManager, int flag)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@Flag", flag));
                sqlListParam.Add(new SqlParameter("@ReceiveDate", objSuratJalan.ReceiveDate));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@StatusInvoice", objSuratJalan.StatusInvoice));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePostingStatusSJ");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateStatusSJ(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusSJ");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSJSendSMS(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratJalanNo", objSuratJalan.SuratJalanNo));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSJSendSMS");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateStatusCetakSuratJalan(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusCetakSuratJalan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateStatusCetakKwitansi(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusCetakKwitansi");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int TurunStatusSuratJalan(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@AlasanTurunStatus", objSuratJalan.AlasanTurunStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                //sqlListParam.Add(new SqlParameter("@DistID", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@SubDistID", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@EmailAddress", objSuratJalan.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@Pic", objSuratJalan.Pic));

                int intResult = transManager.DoTransaction(sqlListParam, "spTurunStatusSuratJalan");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveWithCN()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 500 A.ID,A.SuratJalanNo,A.OPID,A.OPNo,A.ScheduleID,A.ScheduleNo,A.PoliceCarNo,isnull(CN.NotaReturNo,'') as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate, "+
            "A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,A.ScheduleDate,OpCreatedTime from (select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate, " +
            "A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1) as A left join CN on CN.SuratJalanID=A.ID");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceived()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 2 order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        //tambahan

        public SuratJalan RetrieveByNo2(string SuratJalanNo, int ItemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID as RefOPID,OP.OPNo as RefOPNo,SuratJalan.ID as RefSJID,SuratJalan.SuratJalanNo as RefSJNo,SuratJalanDetail.ID as RefSJDetailID from SuratJalan,SuratJalanDetail,OP where OP.ID = SuratJalan.OPID and SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalanDetail.ItemID = " + ItemID + " and SuratJalan.SuratJalanNo = '" + SuratJalanNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        //
        public ArrayList RetrieveTokoPoint(string fromPeriode, string toPeriode)
        {
            ArrayList arrResult = new ArrayList();

            int[] intResult = new int[2];

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.CustomerId,SUM(OPDetail.Point) as TotalPoint from SuratJalan,OP,OPDetail where SuratJalan.Status > 0 and OPDetail.Quantity = OPDetail.QtyScheduled and convert(varchar,SuratJalan.createdtime,112) between '" + fromPeriode + "' and '" + toPeriode + "' and OP.ID = SuratJalan.OPID and OP.CustomerType = 1 and OP.ID = OPDetail.OPID group by CustomerId");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    intResult = new int[2];
                    intResult[0] = Convert.ToInt32(sqlDataReader["CustomerID"]);
                    intResult[1] = Convert.ToInt32(sqlDataReader["TotalPoint"]);
                    arrResult.Add(intResult);
                }
            }

            return arrResult;
        }


        public ArrayList RetrieveReceived(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 2 and C.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 2 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveNoReceivedByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1 and B.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveNoReceivedByDepoOnSchedule2(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID  from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1 and B.DepoID = " + depoID +
                " union all select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and (A.Status = 3 and A.PostingReceiveDate is null) and B.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedNoByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBatalKirim(string tglKirim)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 2 and  order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedByCriteria(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 2 and " + strField + " = '" + strValue + "' and B.DepoID="+depoID+" order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveReceivedByCriteriaForSJmemo(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,B.DepoID,A.ScheduleID,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,"+
                    "A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate "+
                    "from SuratJalanMemo as A, Memo as B where A.OPID=B.ID and B.Status >= 2 and A.Status>-1 and A.SuratJalanNo  like '%"+strValue+"%' order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObjectMemo(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveNoReceived()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveNoReceived(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1 and C.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveNoReceivedByCriteria(string strField,string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1 and " + strField + " = '" + strValue + "' and B.DepoID="+depoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveNoReceivedByCriteria2(string strField, string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status = 1 and " + strField + " = '" + strValue + "' and B.DepoID=" + depoID +
                " union all select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and (A.Status = 3 and A.PostingReceiveDate is null) and " + strField + " = '" + strValue + "' and B.DepoID=" + depoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedNo()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedNo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and C.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveReceivedNoByCriteria(string strField,string strValue, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and " + strField + " = '" + strValue + "' and B.DepoID="+depoID+" order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public int RetrieveJumOpenSJ(int opid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SuratJalan where Status = 0 and opid = " + opid);
            strError = dataAccess.Error;

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

        public int SumJumOPSJ(int opid, int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(SuratJalanDetail.Qty),0) as JumQty from SuratJalanDetail,SuratJalan where SuratJalan.OPID = " + opid + " and SuratJalanDetail.ItemID = " + itemID + " and SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.Status > -1");
            strError = dataAccess.Error;
           
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JumQty"]);
                }
            }

            return 0;
        }
        public int CekTypeKondisiTO(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select TypeKondisi from ScheduleDetail as a,TransferDetail as b where a.DocumentDetailID=b.ID and a.Status>-1 and a.ID=" + id);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["TypeKondisi"]);
                }
            }

            return 0;
        }


        public int RetrieveOutStandingOPById(int documentID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and C.Status in (3,5,6) and A.DocumentID = " + documentID + " group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jum = jum + 1;
                }
            }

            return jum;
        }


        public int GetOPById(int OpId,DateTime dtCreatedTime)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalan where Status > -1 and OPID = " + OpId);
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumID"]);
                }
            }

            return jum;
        }

        public int GetJumOPById(int OpId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalan where OPID = " + OpId + " and Status > -1");
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumID"]);
                }
            }

            return jum;
        }
        public string GetKodeToko(int OpId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case when OP.CustomerType=1 then (select top 1 TokoCode from Toko where Toko.ID=OP.CustomerId and Toko.RowStatus>-1) else (select top 1 CustomerCode from Customer where Customer.ID=OP.CustomerId and Customer.RowStatus>-1) end KodeToko from OP where ID="+OpId+" and Status>-1");
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["KodeToko"].ToString();
                }
            }

            return string.Empty;
        }
        public string ViewCekTagihanSuratJalan(string sjno)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 case when C.TypeDistSub=1 then (select TagihanKe from Distributor where Distributor.ID=C.DistSubID) else '' end TagihanKe from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '"+sjno+"' and A.Status>-1 ");
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["TagihanKe"].ToString();
                }
            }

            return string.Empty;
        }

        public int GetJumOPById(int OpId,int scheduleID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalan where OPID = " + OpId + " and ScheduleID = " + scheduleID + " and Status > -1");
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumID"]);
                }
            }

            return jum;
        }
        public int CekJumOPById(int OpId, int scheduleID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalan where OPID = " + OpId + " and ScheduleID = " + scheduleID);
            strError = dataAccess.Error;

            int jum = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumID"]);
                }
            }

            return jum;
        }
                
        public SuratJalan RetrieveByNo(string SuratJalanNo)
        {
            string strQuery = "select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status>-1 and A.SuratJalanNo = '" + SuratJalanNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public SuratJalan RetrieveByInvNo(string invNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
            "from SuratJalan as A,Schedule as B,OP as C,InvoiceDetail as D, Invoice as E where A.ScheduleID = B.ID and A.OPID = C.ID and A.Status>-1 and A.ID=D.SuratJalanID and D.InvoiceID=E.ID and E.Status>-1 and E.RowStatus>-1 and InvoiceNo='" + invNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalan();
        }

        public SuratJalan RetrieveByNoPost(string SuratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate from SuratJalan as A,Schedule as B,OP as C where A.Status > -1 and A.status < 2 and A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public SuratJalan RetrieveByNoPostDepoID(string SuratJalanNo, int DepoID)
        {
            string strDepoID = string.Empty;
            if (DepoID == 6)
                strDepoID = "' and B.DepoID in (6,8,7) ";
            else if (DepoID == 3)
                strDepoID = "' and B.DepoID in (3,7,1) ";
            else
                strDepoID = "' and B.DepoID=" + DepoID;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + strDepoID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalan();
        }

       
        public SuratJalan RetrieveByID(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.ID = " + ID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalan();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            //iko edit, 28mei14
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and " + strField + " = '" + strValue + "' order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }


        public ArrayList RetrieveByDistributor(string strDistributor)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status = 2 order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveByDistributor3(string strDistributor, string status)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            //Pa Pati

            //edit iko 2feb16 kecuali brg promosi
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and C.TypeDistSub = 1 and C.DistSubID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate,isnull((select top 1 ItemTypeID from SuratJalanDetail, Items, Groups where SuratJalanDetail.SuratJalanID=A.ID and SuratJalanDetail.ItemID=Items.ID and Items.GroupID=Groups.ID and ItemTypeID=6),0) as itemTypeID,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and C.TypeDistSub = 1 and C.DistSubID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " ) as A1 where itemTypeID<>6 order by ActualShipmentDate");
            //edit iko 2feb16 kecuali brg promosi
            
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveByDistributor(string strDistributor)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' and A.Status = 3 order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveByDistributor2(string strDistributor)
        {
            //old like : dist E.DistributorCode = '" + strDistributor + "' so bisa keluar semua. ini utk mitra inti
            string strDist = " E.DistributorCode in ('JT1','CPD1','JN1','JN2','JN3') ";
            
            string strQuery = "select * from ( " +
                "select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan, " +
                "A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 " +
                //"and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' " +
                //"and C.CustomerId = D.ID and D.DistributorID = E.ID and " +strDist+" and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' " +
                "and C.CustomerId = D.ID and D.DistributorID = E.ID and " + strDist + " and A.ActualShipmentDate >= '2017-03-01 00:00:00.000' " +
                "and A.Status = 3 and C.DistSubID=E.ID and C.Status>-1 ) as BB2 " +
                "where not exists (select * from ( " +
                "select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan, " +
                "A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E, CGI_Invoice as F, CGI_InvoiceDetail as G " +
                "where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 " +
                "and C.CustomerId = D.ID and D.DistributorID = E.ID and A.ID=G.SuratJalanID and G.InvoiceID=F.ID and " + strDist +
                //"E.DistributorCode = '" + strDistributor + "' and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' and A.Status = 3)  as BB1 " +
                //" and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' and A.Status = 3 and C.DistSubID=E.ID and C.Status>-1)  as BB1 " +
                " and A.ActualShipmentDate >= '2017-03-01 00:00:00.000' and A.Status = 3 and C.DistSubID=E.ID and C.Status>-1 and F.Status>-1)  as BB1 " +
                "where BB1.SuratJalanNo=BB2.SuratJalanNo) order by ActualShipmentDate";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveByDistributor(string strDistributor, int typeOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and D.SubDistributorID = 0 and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.SuratJalanNo");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.CreatedTime,A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByDistributor(string strDistributor, int typeOP,string suratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 2 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveByDistributor(string strDistributor, int typeOP, string suratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 3 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByDistributorByOP(string strDistributor, int typeOP, string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 2 and C.TypeOP = " + typeOP + " and C.OPNo = '" + opNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributor0(string strSubDistributor)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status = 2 order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveBySubDistributor3(string strSubDistributor, string status)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            //Pa Pati

            //edit iko 2feb16 kecuali brg promosi
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and C.TypeDistSub = 2 and C.DistSubID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " order by A.ActualShipmentDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate,isnull((select top 1 ItemTypeID from SuratJalanDetail, Items, Groups where SuratJalanDetail.SuratJalanID=A.ID and SuratJalanDetail.ItemID=Items.ID and Items.GroupID=Groups.ID and ItemTypeID=6),0) as itemTypeID,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and C.TypeDistSub = 2 and C.DistSubID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.CreatedTime >= '2014-01-01 00:00:00.000' and A.Status in " + status + " ) as A1 where itemTypeID<>6 order by ActualShipmentDate");
            //edit iko 2feb16 kecuali brg promosi

            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveBySubDistributor(string strSubDistributor)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and E.SubDistributorCode = '" + strSubDistributor + "' and A.ActualShipmentDate >= '2015-06-01 00:00:00.000' and A.Status = 3 order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveBySubDistributor2(string strSubDistributor)
        {
            string strQuery = "select * from ( select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan, A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID "+
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and  E.SubDistributorCode in ('" + strSubDistributor + "')  and A.ActualShipmentDate >= '2017-03-01 00:00:00.000' and A.Status = 3 and C.DistSubID=E.ID and C.Status>-1 ) as BB2 where not exists (select * from ( select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan, A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Toko as D,SubDistributor as E, CGI_Invoice as F, CGI_InvoiceDetail as G where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.SubDistributorID = E.ID and A.ID=G.SuratJalanID and G.InvoiceID=F.ID and  E.SubDistributorCode in ('" + strSubDistributor + "')  and A.ActualShipmentDate >= '2017-03-01 00:00:00.000' and A.Status = 3 and C.DistSubID=E.ID and C.Status>-1  and G.RowStatus>=0)  as BB1 where BB1.SuratJalanNo=BB2.SuratJalanNo) order by ActualShipmentDate";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributor2(string strSubDistributor,int typeOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.CreatedTime,A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributor(string strSubDistributor, int typeOP, string suratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 3 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList CGIRetrieveBySubDistributor(string strSubDistributor, int typeOP, string suratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 3 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributorByOP(string strSubDistributor, int typeOP, string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 2 and C.TypeOP = " + typeOP + " and C.OPNo = '" + opNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByCustomer(string strCustomer)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 2 order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveByCustomer3(string strCustomer, string status)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status in " + status + "  order by A.ActualShipmentDate");

            //edit iko 2feb16 kecuali brg promosi
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status in " + status + "  order by A.ActualShipmentDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate, isnull((select top 1 ItemTypeID from SuratJalanDetail, Items, Groups where SuratJalanDetail.SuratJalanID=A.ID and SuratJalanDetail.ItemID=Items.ID and Items.GroupID=Groups.ID and ItemTypeID=6),0) as itemTypeID,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID " +
                "from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status in " + status + " ) as A1 where itemTypeID<>6 order by ActualShipmentDate");
            //edit iko 2feb16 kecuali brg promosi
            
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveByCustomer3PRM(string status)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,isnull((select top 1 NotaReturNo from CN,CNDetail where CN.ID=CNDetail.CnID and CN.Status>-1 and CnType=1 and SuratJalanID=A.ID),A.DriverName) as DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,B.ScheduleDate as ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C,OPDetail as D, Items as E, Groups as G where A.ScheduleID = B.ID and A.OPID = C.ID and C.ID = D.OPID and D.ItemID=E.ID and E.GroupID=G.ID and G.ItemTypeID=6 and A.Status in " + status + "  order by A.ActualShipmentDate");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public ArrayList RetrieveByCustomer(string strCustomer,int typeOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.CreatedTime,A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                    //arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByCustomer2(int typeOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }


        public ArrayList RetrieveByCustomer(string strCustomer, int typeOP,string suratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 2 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByCustomerByOP(string strCustomer, int typeOP, string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 2 and C.TypeOP = " + typeOP + " and C.OPNo = '" + opNo + "' order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByDistributorPreInv(string strDistributor, int typeOP, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Toko as D,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.CustomerId = D.ID and D.DistributorID = E.ID and E.DistributorCode = '" + strDistributor + "' and D.SubDistributorID = 0 and A.Status = 2 and C.TypeOP = " + typeOP + " order by A.SuratJalanNo");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByDistributorPreInv(string strDistributor, int typeOP, string suratJalanNo, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Distributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 1 and E.DistributorCode = '" + strDistributor + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributorPreInv(string strSubDistributor, int typeOP, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveBySubDistributorPreInv(string strSubDistributor, int typeOP, string suratJalanNo, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,SubDistributor as E where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 1 and C.DistSubID = E.ID and C.TypeDistSub = 2 and E.SubDistributorCode = '" + strSubDistributor + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByCustomerPreInv(string strCustomer, int typeOP, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                    //arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public ArrayList RetrieveByCustomerPreInv(string strCustomer, int typeOP, string suratJalanNo, string asalSJ)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,B.ScheduleDate,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalan as A,Schedule as B,OP as C,Customer as D where A.ScheduleID = B.ID and A.OPID = C.ID and C.CustomerType = 2 and C.CustomerId = D.ID and D.CustomerCode = '" + strCustomer + "' and A.Status = 1 and C.TypeOP = " + typeOP + " and A.SuratJalanNo = '" + suratJalanNo + "' and C.DepoID " + asalSJ + " order by A.SuratJalanNo");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public SuratJalan RetrieveByNoToProforma(string SuratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo," +
                "case C.CustomerType " +
                "when 1 then (select TokoCode from Toko where ID = C.CustomerId) " +
                "when 2 then (select CustomerCode from Customer where ID = C.CustomerId) end KodeToko, " +
                "case C.CustomerType " +
                "when 1 then (select TokoName from Toko where ID = C.CustomerId) " +
                "when 2 then (select CustomerName from Customer where ID = C.CustomerId) end NamaToko, " +
                "A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate " +
                "from SuratJalan as A,Schedule as B,OP as C " +
                "where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read()) // Querynya blm diganti
                {
                    return GenerateObjectForProforma(sqlDataReader);
                }
            }

            return new SuratJalan();
        }

        public int UpdateTglKirimAktual(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalan.ID));
                sqlListParam.Add(new SqlParameter("@TglKirimActual", objSuratJalan.TglKirimActual));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTglKirimAktual");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public ArrayList RetrieveReceivedNoByDepoOnSchedule1(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and B.DepoID = " + depoID + " and A.Status=1 and A.StatusPengajuan is null order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public int RetrievePengajuanSJ(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(*) as count from SuratJalan,Depo,OP where SuratJalan.OPID = OP.ID and OP.DepoID = Depo.ID and Depo.ID = " + depoID + " and SuratJalan.Status > -1 and SuratJalan.StatusPengajuan =1");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            int count = 0;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["count"]);
                }
            }

            return count;
        }

        public ArrayList RetrieveListPengajuanSJ(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi,B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and B.DepoID = " + depoID + " and A.Status >-1 and A.StatusPengajuan =1 order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }

        public SuratJalan RetrieveByNo2(string SuratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalan.ID,SuratJalan.SuratJalanNo,SuratJalan.StatusPengajuan from SuratJalan where SuratJalan.SuratJalanNo = '" + SuratJalanNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject4(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public SuratJalan GenerateObject4(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["StatusPengajuan"].ToString()))
                objSuratJalan.StatusPengajuan = 0;
            else
                objSuratJalan.StatusPengajuan = Convert.ToInt32(sqlDataReader["StatusPengajuan"]);

            return objSuratJalan;

        }
        public SuratJalan RetrieveByNoTokoCodeName(string SuratJalanNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.CountPrint,A.Keterangan, " +
                                                                        "A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ActualShipmentDate, " +
                                                                         "case C.customertype " +
                                                                         "when 1 then (select TokoCode from Toko where ID = C.CustomerID) " +
                                                                         "when 2 then (select CustomerCode from Customer where ID = C.CustomerID) end KodeToko, " +
                                                                         "case C.customertype " +
                                                                         "when 1 then (select TokoName from Toko where ID = C.CustomerID) " +
                                                                         "when 2 then (select CustomerName from Customer where ID = C.CustomerID) end NamaToko " +
                                                                        "from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and A.SuratJalanNo = '" + SuratJalanNo + "'");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCodeName(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public string CekSJbySchduleDetailID(int scheID, int scheDetID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SuratJalanNo,'') as SJno from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.Status>-1 and ScheduleID=" + scheID + " and SuratJalanDetail.ScheduleDetailID=" + scheDetID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["SJno"].ToString();
                }
            }

            return string.Empty;
        }
        public string CekSJbySchduleDetailIDStatusMin1(int scheID, int scheDetID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SuratJalanNo,'') as SJno from SuratJalan,SuratJalanDetail where SuratJalan.ID=SuratJalanDetail.SuratJalanID and SuratJalan.Status=-1 and ScheduleID=" + scheID + " and SuratJalanDetail.ScheduleDetailID=" + scheDetID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["SJno"].ToString();
                }
            }

            return string.Empty;
        }
        public string GetAlamatKirim(string sjNO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select (select CityName from City where City.ID=A.CityID)+' | '+(select NamaKabupaten from Kabupaten where Kabupaten.ID=A.KabupatenID)+' | '+AlamatKirim as Alamat from (select top 1 OP.CustomerId,OP.CustomerType,case when OP.CustomerType=1 then (select CityID from Toko where Toko.ID=OP.CustomerId) else (select CityID from Customer where Customer.ID=OP.CustomerId) end CityID, case when OP.CustomerType=1 then (select KabupatenID from Toko where Toko.ID=OP.CustomerId) else (select KabupatenID from Customer where Customer.ID=OP.CustomerId) end KabupatenID,case when CustomerType=1 then (select Address from Toko where Toko.ID=OP.CustomerId) else (select Address from Customer where Customer.ID=OP.CustomerId) end AlamatKirim "+
                "from SuratJalan,OP where SuratJalanNo='"+sjNO+"' and SuratJalan.OPID=OP.ID) as A");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Alamat"].ToString();
                }
            }

            return string.Empty;
        }
        public ArrayList RetrieveTujuan(int depoID, string tujuan)
        {
            string strCariTujuan = string.Empty;
            if (tujuan.Length > 0)
                strCariTujuan = "  and KabupatenName like '%"+tujuan+"%'  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Ex_Vendor where RowStatus>-1 and DepoID="+depoID+strCariTujuan+" order by KabupatenName");
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalan.Add(GenerateObjectCost(sqlDataReader));
                }
            }
            else
                arrSuratJalan.Add(new SuratJalan());

            return arrSuratJalan;
        }
        public SuratJalan RetrieveTujuanWitdID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Ex_Vendor where RowStatus>-1 and ID=" + id);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCost(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public SuratJalan RetrieveForKabupaten(int sjID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 SuratJalan.ID,KabupatenID,NamaKabupaten as KabupatenName,0 as Cost from SuratJalan,OP,Kabupaten where SuratJalan.OPID=OP.ID and OP.Status>-1 and OP.KabupatenID=Kabupaten.ID and SuratJalan.ID=" + sjID);
            strError = dataAccess.Error;
            arrSuratJalan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCost(sqlDataReader);
                }
            }

            return new SuratJalan();
        }
        public SuratJalan GenerateObjectCodeName(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalan.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objSuratJalan.OPNo = sqlDataReader["OPNo"].ToString();
            objSuratJalan.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalan.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalan.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalan.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalan.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objSuratJalan.CountPrint = Convert.ToInt32(sqlDataReader["CountPrint"]);
            objSuratJalan.Keterangan = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalan.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);


            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalan.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalan.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalan.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objSuratJalan.KodeToko = sqlDataReader["KodeToko"].ToString();
            objSuratJalan.NamaToko = sqlDataReader["NamaToko"].ToString();
            //objSuratJalan.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))
                objSuratJalan.TglKirimActual = DateTime.MinValue;
            else
                objSuratJalan.TglKirimActual = Convert.ToDateTime(sqlDataReader["ActualShipmentDate"]);

            return objSuratJalan;

        }

        public SuratJalan GenerateObjectForProforma(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalan.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objSuratJalan.OPNo = sqlDataReader["OPNo"].ToString();
            objSuratJalan.KodeToko = sqlDataReader["KodeToko"].ToString();
            objSuratJalan.NamaToko = sqlDataReader["NamaToko"].ToString();
            objSuratJalan.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalan.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalan.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalan.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalan.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objSuratJalan.CountPrint = Convert.ToInt32(sqlDataReader["CountPrint"]);
            objSuratJalan.Keterangan = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalan.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalan.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalan.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalan.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))
                objSuratJalan.TglKirimActual = DateTime.MinValue;
            else
                objSuratJalan.TglKirimActual = Convert.ToDateTime(sqlDataReader["ActualShipmentDate"]);

            return objSuratJalan;
        }

        public SuratJalan GenerateObject(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalan.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objSuratJalan.OPNo = sqlDataReader["OPNo"].ToString();
            objSuratJalan.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalan.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalan.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalan.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalan.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objSuratJalan.CountPrint = Convert.ToInt32(sqlDataReader["CountPrint"]);
            objSuratJalan.Keterangan = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalan.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);
            
            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalan.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalan.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalan.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))
                objSuratJalan.TglKirimActual = DateTime.MinValue;
            else
                objSuratJalan.TglKirimActual = Convert.ToDateTime(sqlDataReader["ActualShipmentDate"]);
            if (string.IsNullOrEmpty(sqlDataReader["ScheduleDate"].ToString()))
                objSuratJalan.ScheduleDate = DateTime.MinValue;
            else
                objSuratJalan.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);

            objSuratJalan.OpCreatedTime = Convert.ToDateTime(sqlDataReader["OpCreatedTime"]);
            objSuratJalan.OpRetur = Convert.ToInt32(sqlDataReader["OpRetur"]);
            objSuratJalan.CetakKwitansi = Convert.ToInt32(sqlDataReader["CetakKwitansi"]);
            objSuratJalan.CountCetakKwitansi = Convert.ToInt32(sqlDataReader["CountCetakKwitansi"]);
            objSuratJalan.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);

            return objSuratJalan;
        }
        public SuratJalan GenerateObjectMemo(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalan.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            //objSuratJalan.OPNo = sqlDataReader["OPNo"].ToString();
            objSuratJalan.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            //objSuratJalan.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalan.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalan.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalan.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objSuratJalan.CountPrint = Convert.ToInt32(sqlDataReader["CountPrint"]);
            objSuratJalan.Keterangan = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalan.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalan.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalan.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalan.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))
                objSuratJalan.TglKirimActual = DateTime.MinValue;
            else
                objSuratJalan.TglKirimActual = Convert.ToDateTime(sqlDataReader["ActualShipmentDate"]);

            return objSuratJalan;
        }

        public SuratJalan GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalan.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objSuratJalan.OPNo = sqlDataReader["OPNo"].ToString();
            objSuratJalan.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalan.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalan.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalan.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalan.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objSuratJalan.CountPrint = Convert.ToInt32(sqlDataReader["CountPrint"]);
            objSuratJalan.Keterangan = sqlDataReader["Keterangan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalan.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalan.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalan.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalan.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalan.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalan.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);

            objSuratJalan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSuratJalan;
        }
        public SuratJalan GenerateObject3(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.RefOPID = Convert.ToInt32(sqlDataReader["RefOPID"]);
            objSuratJalan.RefOPNo = sqlDataReader["RefOPNo"].ToString();
            objSuratJalan.RefSJID = Convert.ToInt32(sqlDataReader["RefSJID"]);
            objSuratJalan.RefSJNo = sqlDataReader["RefSJNo"].ToString();
            objSuratJalan.RefSJDetailID = Convert.ToInt32(sqlDataReader["RefSJDetailID"]);

            return objSuratJalan;
        }
        public SuratJalan GenerateObjectCost(SqlDataReader sqlDataReader)
        {
            objSuratJalan = new SuratJalan();
            objSuratJalan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalan.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"].ToString());
            objSuratJalan.KabupatenName = sqlDataReader["KabupatenName"].ToString();
            objSuratJalan.Cost = Convert.ToDecimal(sqlDataReader["Cost"]);

            return objSuratJalan;
        }





    }
}

