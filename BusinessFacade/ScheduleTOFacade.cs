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
    public class ScheduleTOFacade : AbstractFacade
    {
        private ScheduleTO objScheduleTO = new ScheduleTO();
        private ArrayList arrScheduleTO;
        private List<SqlParameter> sqlListParam;

        public ScheduleTOFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            return 0;
        }

        public override int Update(object objDomain)
        {
            return 0;
        }

        public override int Delete(object objDomain)
        {

            return 0;
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and C.Status in (3,5) group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            strError = dataAccess.Error;
            arrScheduleTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleTO.Add(new ScheduleTO());

            return arrScheduleTO;
        }



        public ArrayList RetrieveOutStandingTO()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and B.ID not in (select ScheduleID from SuratJalanTO) group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            strError = dataAccess.Error;
            arrScheduleTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleTO.Add(new ScheduleTO());

            return arrScheduleTO;
        }

        public ArrayList RetrieveOutStandingTO(int depoID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 1 and (C.FromDepoID = " + depoID + " or C.ToDepoID = " + depoID + ") group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 1 and B.DepoID = " + depoID + " group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            strError = dataAccess.Error;
            arrScheduleTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleTO.Add(new ScheduleTO());

            return arrScheduleTO;
        }

        public ArrayList RetrieveOutStandingTO2(int depoID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 1 and B.DepoID = " + depoID + " group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as TransferOrderNo,C.TransferOrderDate,C.FromDepoName,C.ToDepoName,C.FromDepoAddress,C.ToDepoAddress from ScheduleDetail as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 1 and B.DepoID = " + depoID + " group by ScheduleNo,ScheduleDate,DocumentNo,TransferOrderDate,FromDepoName,ToDepoName,FromDepoAddress,ToDepoAddress");
            strError = dataAccess.Error;
            arrScheduleTO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleTO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleTO.Add(new ScheduleTO());

            return arrScheduleTO;
        }
       
        public ScheduleTO GenerateObject(SqlDataReader sqlDataReader)
        {
            objScheduleTO = new ScheduleTO();
            objScheduleTO.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objScheduleTO.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objScheduleTO.TransferOrderNo = sqlDataReader["TransferOrderNo"].ToString();
            objScheduleTO.TransferOrderDate = Convert.ToDateTime(sqlDataReader["TransferOrderDate"]);
            objScheduleTO.FromDepo = sqlDataReader["FromDepoName"].ToString();
            objScheduleTO.ToDepo = sqlDataReader["ToDepoName"].ToString();
            objScheduleTO.FromDepoAddress = sqlDataReader["FromDepoAddress"].ToString();
            objScheduleTO.ToDepoAddress = sqlDataReader["ToDepoAddress"].ToString();
            return objScheduleTO;
        }
    }
}

