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
    public class ScheduleOPFacade : AbstractFacade
    {
        private ScheduleOP objScheduleOP = new ScheduleOP();
        private ArrayList arrScheduleOP;
        private List<SqlParameter> sqlListParam;

        public ScheduleOPFacade()
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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }


        public ArrayList RetrieveOutStandingOP()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }


        public ArrayList RetrieveOutStandingOP(int DepoID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and C.DepoID = " + DepoID + " group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }


        public ArrayList RetrieveOutStandingOP2(int DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and B.DepoID = " + DepoID + " group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }

        public ArrayList RetrieveOutStandingOPByOPNo(int DepoID,string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and C.DepoID = " + DepoID + " and A.DocumentNo = '" + opNo + "' group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }

        public ArrayList RetrieveOutStandingOPByOPNo2(int DepoID, string opNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and B.DepoID = " + DepoID + " and A.DocumentNo = '" + opNo + "' group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }

        public ArrayList RetrieveOutStandingOPByScheduleNo(int DepoID, string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and C.DepoID = " + DepoID + " and B.ScheduleNo = '" + scheduleNo + "' group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }

        public ArrayList RetrieveOutStandingOPByScheduleNo2(int DepoID, string scheduleNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.ScheduleNo,B.ScheduleDate,A.DocumentNo as OPNo,C.CreatedTime as OPDate,C.AlamatLain,C.CustomerType,C.CustomerID,case when C.CustomerType=1 then (select top 1 Address from Toko where Toko.ID=C.CustomerId) else (select top 1 Address from Customer where Customer.ID=C.CustomerId) end Address,case when C.CustomerType=1 then (select top 1 TokoName from Toko where Toko.ID=C.CustomerId) else (select top 1 CustomerName from Customer where Customer.ID=C.CustomerId) end TokoCustName from ScheduleDetail as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.DocumentID = C.ID and B.Status = 1 and A.Status = 0 and A.TypeDoc = 0 and B.DepoID = " + DepoID + " and B.ScheduleNo = '" + scheduleNo + "' group by ScheduleNo,ScheduleDate,DocumentNo,C.CreatedTime,AlamatLain,CustomerType,CustomerID");
            strError = dataAccess.Error;
            arrScheduleOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScheduleOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrScheduleOP.Add(new ScheduleOP());

            return arrScheduleOP;
        }

        public ScheduleOP GenerateObject(SqlDataReader sqlDataReader)
        {
            objScheduleOP = new ScheduleOP();
            objScheduleOP.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            objScheduleOP.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objScheduleOP.OPNo = sqlDataReader["OPNo"].ToString();
            objScheduleOP.OPDate = Convert.ToDateTime(sqlDataReader["OPDate"]);
            objScheduleOP.AlamatLain = sqlDataReader["AlamatLain"].ToString();
            if (sqlDataReader["CustomerType"].ToString() == "1")
                objScheduleOP.CustomerType = "Toko";
            else
                objScheduleOP.CustomerType = "Individual";

            objScheduleOP.CustomerID = Convert.ToInt32(sqlDataReader["CustomerID"].ToString());
            objScheduleOP.Address = sqlDataReader["Address"].ToString();
            objScheduleOP.TokoCustName = sqlDataReader["TokoCustName"].ToString();

            return objScheduleOP;
        }
    }
}

