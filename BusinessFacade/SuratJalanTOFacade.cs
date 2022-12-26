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
    public class SuratJalanTOFacade : AbstractTransactionFacade
    {
        private SuratJalanTO objSuratJalanTO = new SuratJalanTO();
        private ArrayList arrSuratJalanTO;
        private List<SqlParameter> sqlListParam;

        public SuratJalanTOFacade(object objDomain)
            : base(objDomain)
        {
            objSuratJalanTO = (SuratJalanTO)objDomain;
        }

        public SuratJalanTOFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TransferOrderID", objSuratJalanTO.TransferOrderID));
                sqlListParam.Add(new SqlParameter("@ScheduleID", objSuratJalanTO.ScheduleID));
                sqlListParam.Add(new SqlParameter("@PoliceCarNo", objSuratJalanTO.PoliceCarNo));
                sqlListParam.Add(new SqlParameter("@DriverName", objSuratJalanTO.DriverName));
                sqlListParam.Add(new SqlParameter("@Status", objSuratJalanTO.Status));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSuratJalanTO.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSuratJalanTO");

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
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalanTO.ID));
                sqlListParam.Add(new SqlParameter("@SuratJalanTONo", objSuratJalanTO.SuratJalanNo));
                sqlListParam.Add(new SqlParameter("@TransferOrderID", objSuratJalanTO.TransferOrderID));
                sqlListParam.Add(new SqlParameter("@ScheduleID", objSuratJalanTO.ScheduleID));
                sqlListParam.Add(new SqlParameter("@PoliceCarNo", objSuratJalanTO.PoliceCarNo));
                sqlListParam.Add(new SqlParameter("@DriverName", objSuratJalanTO.DriverName));
                sqlListParam.Add(new SqlParameter("@Status", objSuratJalanTO.Status));
                sqlListParam.Add(new SqlParameter("@Cetak", objSuratJalanTO.Cetak));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalanTO.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSuratJalanTO");

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
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalanTO.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalanTO.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSuratJalanTO");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdatePostingDate(TransactionManager transManager, int flag)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuratJalanTO.ID));
                sqlListParam.Add(new SqlParameter("@Flag", flag));
                sqlListParam.Add(new SqlParameter("@ReceiveDate", objSuratJalanTO.ReceiveDate));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePostingDateSJTO");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
	    public int TurunStatusSuratJalanTO(TransactionManager transManager)
	    {
		    try
		    {
			    sqlListParam = new List<SqlParameter>();
			    sqlListParam.Add(new SqlParameter("@ID", objSuratJalanTO.ID));
			    sqlListParam.Add(new SqlParameter("@AlasanTurunStatus", objSuratJalanTO.AlasanTurunStatus));
			    sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuratJalanTO.LastModifiedBy));

			    int intResult = transManager.DoTransaction(sqlListParam, "spTurunStatusSuratJalanTO");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveNoReceive()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public int SumJumTOSJ(int toid, int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(SuratJalanDetailTO.Qty),0) as JumQty from SuratJalanDetailTO,SuratJalanTO where SuratJalanTO.TransferOrderID = " + toid + " and SuratJalanDetailTO.ItemID = " + itemID + " and SuratJalanTO.ID = SuratJalanDetailTO.SuratJalanTOID and SuratJalanTO.Status > -1");
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

        public ArrayList RetrieveNoReceive(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1 and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ")");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1 and C.ToDepoID = " + depoID );
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveNoReceiveByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1 and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ")");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1 and B.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveNoReceiveByCriteria(string strField,string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 1 and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceiveNo()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceiveNo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ") order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceiveNoByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ") order by A.ID desc");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceiveNoByCriteria(string strField,string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceive()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 2 order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceive(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 2 and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ") order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceivedByDepoOnSchedule(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 2 and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ") order by A.ID desc");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 2 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }

        public ArrayList RetrieveReceiveByCriteria(string strField,string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.Status = 2 and " + strField + " = '" + strValue + "' order by A.ID desc");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }
        public SuratJalanTO RetrieveByNo(string SuratJalanTONo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and A.SuratJalanNo = '" + SuratJalanTONo + "'");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuratJalanTO();
        }

        public int RetrieveJumOpenSJ(int transferOrderId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SuratJalanTO where Status = 0 and TransferOrderID = " + transferOrderId);
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

        public int GetJumTOById(int ToId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalanTO where TransferOrderID = " + ToId + " and Status > -1");
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

        public int GetJumTOById(int ToId,int scheduleID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ID) as jumID from SuratJalanTO where TransferOrderID = " + ToId + " and ScheduleID = " + scheduleID + " and Status > -1");
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

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanNo,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from SuratJalanTO as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanTO.Add(new SuratJalanTO());

            return arrSuratJalanTO;
        }


        // Tambahan 17 Juli 2016 munculkan ExpedisiName di SJTO
        public SuratJalanTO RetrieveByNo2(string SuratJalanTONo)
        {
            string strSQL = "select A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime " +
                                                                        ", case when D.ExpedisiID > 0 then (select ExpedisiName from Expedisi where ID = D.ExpedisiID) end NamaExpedisi  " +
                                                                        "from SuratJalanTO as A,Schedule as B,TransferOrder as C, ExpedisiDetail as D where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and  B.ExpedisiDetailID = D.ID and A.SuratJalanNo = '" + SuratJalanTONo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuratJalanTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SuratJalanTO();
        }


        public SuratJalanTO GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSuratJalanTO = new SuratJalanTO();
            objSuratJalanTO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanTO.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalanTO.TransferOrderID = Convert.ToInt32(sqlDataReader["TransferOrderID"]);
            objSuratJalanTO.TransferOrderNo = sqlDataReader["TransferOrderNo"].ToString();
            objSuratJalanTO.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalanTO.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalanTO.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalanTO.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalanTO.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalanTO.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);

            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalanTO.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalanTO.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);


            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalanTO.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalanTO.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalanTO.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalanTO.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);

            objSuratJalanTO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalanTO.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalanTO.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalanTO.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            //Tambahan 17 Jul 16
            objSuratJalanTO.NamaExpedisi = sqlDataReader["NamaExpedisi"].ToString();

            return objSuratJalanTO;

        }

        public SuratJalanTO GenerateObject(SqlDataReader sqlDataReader)
        {
            objSuratJalanTO = new SuratJalanTO();
            objSuratJalanTO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanTO.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalanTO.TransferOrderID = Convert.ToInt32(sqlDataReader["TransferOrderID"]);
            objSuratJalanTO.TransferOrderNo = sqlDataReader["TransferOrderNo"].ToString();
            objSuratJalanTO.ScheduleID = Convert.ToInt32(sqlDataReader["ScheduleID"]);
            objSuratJalanTO.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objSuratJalanTO.PoliceCarNo = sqlDataReader["PoliceCarNo"].ToString();
            objSuratJalanTO.DriverName = sqlDataReader["DriverName"].ToString();
            objSuratJalanTO.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSuratJalanTO.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            if (string.IsNullOrEmpty(sqlDataReader["ReceiveDate"].ToString()))
                objSuratJalanTO.ReceiveDate = DateTime.MinValue;
            else
                objSuratJalanTO.ReceiveDate = Convert.ToDateTime(sqlDataReader["ReceiveDate"]);
            if (string.IsNullOrEmpty(sqlDataReader["PostingShipmentDate"].ToString()))
                objSuratJalanTO.PostingShipmentDate = DateTime.MinValue;
            else
                objSuratJalanTO.PostingShipmentDate = Convert.ToDateTime(sqlDataReader["PostingShipmentDate"]);
            if (string.IsNullOrEmpty(sqlDataReader["PostingReceiveDate"].ToString()))
                objSuratJalanTO.PostingReceiveDate = DateTime.MinValue;
            else
                objSuratJalanTO.PostingReceiveDate = Convert.ToDateTime(sqlDataReader["PostingReceiveDate"]);
            objSuratJalanTO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuratJalanTO.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuratJalanTO.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuratJalanTO.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSuratJalanTO;

        }


    }
}

