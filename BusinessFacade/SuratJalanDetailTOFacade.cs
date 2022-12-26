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
    public class SuratJalanDetailTOFacade : AbstractTransactionFacade
    {
        private SuratJalanDetailTO objSuratJalanDetailTO = new SuratJalanDetailTO();
        private ArrayList arrSuratJalanDetailTO;
        private List<SqlParameter> sqlListParam;
        private string scheduleNo = string.Empty;

        public SuratJalanDetailTOFacade(object objDomain)
            : base(objDomain)
        {
            objSuratJalanDetailTO = (SuratJalanDetailTO)objDomain;
        }

        public SuratJalanDetailTOFacade()
        {

        }

        public SuratJalanDetailTOFacade(object objDomain, string strScheduleNo)
        {
            objSuratJalanDetailTO = (SuratJalanDetailTO)objDomain;
            scheduleNo = strScheduleNo;
        }


        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratJalanTOID", objSuratJalanDetailTO.SuratJalanTOID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSuratJalanDetailTO.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objSuratJalanDetailTO.Qty));
                sqlListParam.Add(new SqlParameter("@ScheduleDetailID", objSuratJalanDetailTO.ScheduleDetailId));
                sqlListParam.Add(new SqlParameter("@Paket", objSuratJalanDetailTO.Paket));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSuratJalanDetailTO");

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
                objSuratJalanDetailTO = (SuratJalanDetailTO)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ScheduleDetailID", objSuratJalanDetailTO.ScheduleDetailId));
                sqlListParam.Add(new SqlParameter("@Qty", objSuratJalanDetailTO.Qty));
                sqlListParam.Add(new SqlParameter("@Flag", objSuratJalanDetailTO.Flag));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSuratJalanDetailTO");

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
                objSuratJalanDetailTO = (SuratJalanDetailTO)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratJalanTOID", objSuratJalanDetailTO.SuratJalanTOID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSuratJalanDetailTO");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanTOID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.Paket,(select top 1 TypeKondisi from TransferDetail,ScheduleDetail where TransferDetail.ID=ScheduleDetail.DocumentDetailID and ScheduleDetail.ItemID=A.ItemID and ScheduleDetail.ID=A.ScheduleDetailID ) as TypeKondisi,(select top 1 FromDepoID from TransferOrder,TransferDetail,ScheduleDetail where TransferDetail.ID=ScheduleDetail.DocumentDetailID and ScheduleDetail.ItemID=A.ItemID and TransferOrder.ID=TransferDetail.TransferOrderID and ScheduleDetail.ID=A.ScheduleDetailID) as fromDepoID from SuratJalanDetailTO as A,Items as B where A.ItemID = B.ID");
            strError = dataAccess.Error;
            arrSuratJalanDetailTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetailTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetailTO.Add(new SuratJalanDetailTO());

            return arrSuratJalanDetailTO;
        }


        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanTOID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket,(select top 1 TypeKondisi from TransferDetail,ScheduleDetail where TransferDetail.ID=ScheduleDetail.DocumentDetailID and ScheduleDetail.ItemID=A.ItemID and ScheduleDetail.ID=A.ScheduleDetailID ) as TypeKondisi from SuratJalanDetailTO as A,Items as B where A.ItemID = B.ID and A.SuratJalanTOID = " + Id);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanTOID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket,(select top 1 TypeKondisi from TransferDetail,ScheduleDetail where TransferDetail.ID=ScheduleDetail.DocumentDetailID and ScheduleDetail.ItemID=A.ItemID and ScheduleDetail.ID=A.ScheduleDetailID ) as TypeKondisi,(select top 1 FromDepoID from TransferOrder,TransferDetail,ScheduleDetail where TransferDetail.ID=ScheduleDetail.DocumentDetailID and ScheduleDetail.ItemID=A.ItemID and TransferOrder.ID=TransferDetail.TransferOrderID and "+
                "ScheduleDetail.ID=A.ScheduleDetailID) as fromDepoID from SuratJalanDetailTO as A,Items as B where A.ItemID = B.ID and A.SuratJalanTOID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetailTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetailTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetailTO.Add(new SuratJalanDetailTO());

            return arrSuratJalanDetailTO;
        }

        //public ArrayList RetrieveByCriteria(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.TransferOrderID,A.TransferDetailID,B.TransferOrderNo,A.ItemID,C.ItemCode,C.Description as ItemName,A.Qty,B.FromDepoName,B.ToDepoName,B.TotalKubikasi from SuratJalanDetailTO as A,TransferOrder as B where A.TransferOrderID = B.ID and A.ItemID = B.ID and " + strField + " like '%" + strValue + "%'");
        //    strError = dataAccess.Error;
        //    arrSuratJalanDetailTO = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSuratJalanDetailTO.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSuratJalanDetailTO.Add(new SuratJalanDetailTO());

        //    return arrSuratJalanDetailTO;
        //}

        public SuratJalanDetailTO GenerateObject(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetailTO = new SuratJalanDetailTO();
            objSuratJalanDetailTO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanDetailTO.SuratJalanTOID = Convert.ToInt32(sqlDataReader["SuratJalanTOID"]);
            objSuratJalanDetailTO.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSuratJalanDetailTO.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSuratJalanDetailTO.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetailTO.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetailTO.ScheduleDetailId = Convert.ToInt32(sqlDataReader["ScheduleDetailID"]);
            objSuratJalanDetailTO.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            objSuratJalanDetailTO.TypeKondisi = Convert.ToInt32(sqlDataReader["TypeKondisi"]);
            objSuratJalanDetailTO.FromDepoID = Convert.ToInt32(sqlDataReader["FromDepoID"]);

            return objSuratJalanDetailTO;
        }
    }
}

