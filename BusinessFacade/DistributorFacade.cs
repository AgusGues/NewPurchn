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
    public class DistributorFacade : AbstractFacade
    {
        private Distributor objDistributor = new Distributor();
        private ArrayList arrDistributor;
        private List<SqlParameter> sqlListParam;

        public DistributorFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDistributor = (Distributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DistributorCode", objDistributor.DistributorCode));
                sqlListParam.Add(new SqlParameter("@DistributorName", objDistributor.DistributorName));
                sqlListParam.Add(new SqlParameter("@Address", objDistributor.Address));
                sqlListParam.Add(new SqlParameter("@JoinDate", objDistributor.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objDistributor.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objDistributor.ZonaID));
                sqlListParam.Add(new SqlParameter("@NPWP", objDistributor.NPWP));                              
                sqlListParam.Add(new SqlParameter("@Telepon", objDistributor.Telepon));                
                sqlListParam.Add(new SqlParameter("@ContactPerson", objDistributor.ContactPerson));
                sqlListParam.Add(new SqlParameter("@DepoID", objDistributor.DepoID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDistributor.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDistributor");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            try
            {
                objDistributor = (Distributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDistributor.ID));
                sqlListParam.Add(new SqlParameter("@DistributorCode", objDistributor.DistributorCode));
                sqlListParam.Add(new SqlParameter("@DistributorName", objDistributor.DistributorName));
                sqlListParam.Add(new SqlParameter("@Address", objDistributor.Address));
                sqlListParam.Add(new SqlParameter("@JoinDate", objDistributor.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objDistributor.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objDistributor.ZonaID));                
                sqlListParam.Add(new SqlParameter("@Telepon", objDistributor.Telepon)); 
                sqlListParam.Add(new SqlParameter("@NPWP", objDistributor.NPWP));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objDistributor.ContactPerson));
                sqlListParam.Add(new SqlParameter("@DepoID", objDistributor.DepoID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDistributor.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDistributor");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objDistributor = (Distributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDistributor.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDistributor.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteDistributor");

                strError = dataAccess.Error;

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
            try
            {
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0");
                strError = dataAccess.Error;
                arrDistributor = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrDistributor.Add(GenerateObject(sqlDataReader));
                    }
                }
                else
                    arrDistributor.Add(new Distributor());
            }
            catch { }

            return arrDistributor;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }
        public ArrayList RetrieveByDepoCL(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.CreditLimit>0 and A.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }

        public ArrayList RetrieveByDepoPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.DepoID in (1,7)");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }

        public Distributor RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveDistLevel2ByTaxNo(string taxNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct d.DistLevel2Name,d.Address,d.NPWP from FakturPajak as a,FakturPajakDetail as b, Invoice as c,DistributorLevel2 as d "+
                "where a.ID=b.FakturPajakID and a.Status>-1 and b.RowStatus>-1 and b.InvoiceID=c.ID and c.Status>-1 and c.RowStatus>-1 and c.DistributorLevel2ID=d.ID "+
                "and a.TaxNo='" + taxNo +"'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSpesialIBG(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveDistLevel2ByTTINo(string tTINo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct d.DistLevel2Name,d.Address,d.NPWP from TandaTerima as a,TandaTerimaDetail as b, Invoice as c,DistributorLevel2 as d "+
                "where a.ID=b.TandaTerimaID and a.Status>-1 and b.RowStatus>-1 and b.InvoiceID=c.ID and c.Status>-1 and c.RowStatus>-1 and c.DistributorLevel2ID=d.ID "+
                "and a.TandaTerimaNo='" + tTINo + "'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSpesialIBG(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveDistLevel2ByInvoiceNo(string invNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct d.DistLevel2Name,d.Address,d.NPWP from Invoice as c,DistributorLevel2 as d "+
                "where c.Status>-1 and c.RowStatus>-1 and c.DistributorLevel2ID=d.ID and c.InvoiceNo='" + invNo + "'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSpesialIBG(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveDistLevel2ID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select d.DistLevel2Name,d.Address,d.NPWP from DistributorLevel2 as d where d.ID=" + id);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSpesialIBG(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveDistLevel2ByDistributorLevel2ID(int distributorLevel2ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct d.DistLevel2Name,d.Address,d.NPWP from Invoice as c,DistributorLevel2 as d " +
                "where c.Status>-1 and c.RowStatus>-1 and c.DistributorLevel2ID=d.ID and c.DistributorLevel2ID=" + distributorLevel2ID );
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSpesialIBG(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveByIdTax(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorNameTax as DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveByCodeByDate2(string Datenya, int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
            "case when (select top 1 DistributorHistory.ID from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") is  null " +
            "then (select top 1 Distributor.ID from Distributor where Distributor.ID = " + Id + ") " +
            "else (select top 1 DistributorHistory.ID from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") end ID, " +
            "case when (select top 1 DistributorHistory.DistributorCode from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") is  null " +
            "then (select top 1 Distributor.DistributorCode from Distributor where Distributor.ID = " + Id + ") " +
            "else (select top 1 DistributorHistory.DistributorCode from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") end DistributorCode, " +
            "case when (select top 1 DistributorHistory.DistributorName from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") is  null " +
            "then (select top 1 Distributor.DistributorNameTax as DistributorName from Distributor where Distributor.ID = " + Id + ") " +
            "else (select top 1 DistributorHistory.DistributorName from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") end DistributorName, " +
            "case when (select top 1 DistributorHistory.NPWP from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") is  null " +
            "then (select top 1 Distributor.NPWP from Distributor where Distributor.ID = " + Id + ") " +
            "else (select top 1 DistributorHistory.NPWP from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") end NPWP, " +
            "case when (select top 1 DistributorHistory.Address from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") is  null " +
            "then (select top 1 Distributor.Address from Distributor where Distributor.ID = " + Id + ") " +
            "else (select top 1 DistributorHistory.Address from Invoice,Distributor,DistributorHistory " +
            "where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.ID = " + Id + ") end Address");

            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveByIdPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.ID in (1,7)");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByCode(string strDistributorCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.DistributorCode = '" + strDistributorCode + "'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }

        public ArrayList RetrieveByChargeToPayment(int intNilai)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.NPWP,A.TotalPoint,A.ContactPerson,A.DepoID,C.DepoName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ChargeToPayment,A.PriceTypeID from Distributor as A,Zona as B,Depo as C where A.ZonaID = B.ID and A.DepoID = C.ID and A.RowStatus = 0 and A.ChargeToPayment = " + intNilai);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }
        public Distributor RetrieveByTokoID(int cTokoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Distributor.DepoID from Toko,Distributor where Toko.DistributorID = Distributor.ID and Toko.ID= " + cTokoId);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectTokoID(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByCheckGroupDist(int cDistId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select STUFF ((SELECT DISTINCT ',' + convert(varchar,IdDist) + '' FROM DistributorGroup where IdDistMaster= '" + cDistId + "' " +
            "and RowStatus >= 0 FOR XML PATH('')), 1, 1, '') as IdDistStr");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByIdCheckOrderOP(int cDistId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OrderOP from distributor where ID='" + cDistId + "' and RowStatus=0");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByIdSept15(string strNoTax)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case when " +
                        "(select DistributorHistory.DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID ) is null then (select Distributor.ID from Distributor where Distributor.ID=FakturPajak.CustomerID)  " +
                        "else (select ID from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID )  end ID, " +
                        "case when (select DistributorHistory.DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID ) is null then (select Distributor.DistributorNameTax as DistributorName from Distributor where Distributor.ID=FakturPajak.CustomerID)  " +
                        "else (select DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID )  end DistributorName, " +
                        "case when (select DistributorHistory.DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID ) is null then (select Distributor.DistributorCode from Distributor where Distributor.ID=FakturPajak.CustomerID)  " +
                        "else (select DistributorCode from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID )  end DistributorCode, " +
                        "case when (select DistributorHistory.DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID ) is null then (select Distributor.NPWP from Distributor where Distributor.ID=FakturPajak.CustomerID)  " +
                        "else (select NPWP from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID )  end NPWP, " +
                        "case when (select DistributorHistory.DistributorName from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID ) is null then (select Distributor.Address from Distributor where Distributor.ID=FakturPajak.CustomerID)  " +
                        "else (select Address from DistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                        "DistributorHistory.DistributorID )  end Address " +
                        "from FakturPajak where TaxNo= '" + strNoTax + "' ");


            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByCodeByTTNo(string TTNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select " +
"case when (select top 1 DistributorHistory.ID from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 Distributor.ID from Distributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = Distributor.ID and Distributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 DistributorHistory.ID from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end ID, " +
"case when (select top 1 DistributorHistory.DistributorCode from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 Distributor.DistributorCode from Distributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = Distributor.ID and Distributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 DistributorHistory.DistributorCode from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end DistributorCode, " +
"case when (select top 1 DistributorHistory.DistributorName from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 Distributor.DistributorNameTax as DistributorName from Distributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = Distributor.ID and Distributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 DistributorHistory.DistributorName from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end DistributorName, " +
"case when (select top 1 DistributorHistory.NPWP from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 Distributor.NPWP from Distributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = Distributor.ID and Distributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 DistributorHistory.NPWP from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end NPWP, " +
"case when (select top 1 DistributorHistory.Address from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 Distributor.Address from Distributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = Distributor.ID and Distributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 DistributorHistory.Address from TandaTerima,TandaTerimaDetail,Invoice,Distributor,DistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and Invoice.TglPenerimaan >=  DistributorHistory.FromPeriod and Invoice.TglPenerimaan <= DistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end Address";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public Distributor RetrieveByCodeByDate(string Datenya, string strDistributorCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select " +
"case when (select top 1 DistributorHistory.ID from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) is  null " +
"then (select top 1 Distributor.ID from Distributor where Distributor.DistributorCode = '" + strDistributorCode + "') " +
"else (select top 1 DistributorHistory.ID from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) end ID, " +
"case when (select top 1 DistributorHistory.DistributorCode from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) is  null " +
"then (select top 1 Distributor.DistributorCode from Distributor where Distributor.DistributorCode = '" + strDistributorCode + "') " +
"else (select top 1 DistributorHistory.DistributorCode from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) end DistributorCode, " +
"case when (select top 1 DistributorHistory.DistributorName from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) is  null " +
"then (select top 1 Distributor.DistributorNameTax as DistributorName from Distributor where Distributor.DistributorCode = '" + strDistributorCode + "') " +
"else (select top 1 DistributorHistory.DistributorName from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) end DistributorName, " +
"case when (select top 1 DistributorHistory.NPWP from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) is  null " +
"then (select top 1 Distributor.NPWP from Distributor where Distributor.DistributorCode = '" + strDistributorCode + "') " +
"else (select top 1 DistributorHistory.NPWP from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) end NPWP, " +
"case when (select top 1 DistributorHistory.Address from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) is  null " +
"then (select top 1 Distributor.Address from Distributor where Distributor.DistributorCode = '" + strDistributorCode + "') " +
"else (select top 1 DistributorHistory.Address from Invoice,Distributor,DistributorHistory " +
"where Invoice.CustomerID = Distributor.ID and Distributor.DistributorCode = DistributorHistory.DistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,DistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,DistributorHistory.ToPeriod,112) and Distributor.DistributorCode = '" + strDistributorCode + "' and CustomerType=1) end Address";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new Distributor();
        }
        public Distributor RetrieveByDistID(int ID)
        {
            string strQuery = "select A.ID,A.DistributorCode,A.DistributorName,A.Address,A.Telepon,A.JoinDate,A.CreditLimit,A.NPWP,A.ContactPerson,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from DistributorHistory as A where A.DistributorID =" + ID;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public ArrayList RetrieveVoucherByDist(string distCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.* from Distributor_PotonganCode as A,Distributor as B where A.Confirmation>0 and A.DistID=B.ID and B.DistributorCode='"+distCode+"'");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObjectVoucher(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }
        public Distributor GetEmailAddress(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(EmailAddress,'') as EmailAddress,isnull(Pic,'') as Pic from distributor where id = " + id);
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectEmail(sqlDataReader);
                }
            }

            return new Distributor();
        }

        public ArrayList RetrieveForIncentive()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select id,DistributorName from distributor where rowstatus=0 and id in (57, 58, 60, 61, 62)");
            strError = dataAccess.Error;
            arrDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDistributor.Add(GenerateObjectForIncentive(sqlDataReader));
                }
            }
            else
                arrDistributor.Add(new Distributor());

            return arrDistributor;
        }

        private object GenerateObjectForIncentive(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDistributor.DistributorName = sqlDataReader["DistributorName"].ToString();

            return objDistributor;
        }

        public Distributor GenerateObjectVoucher(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.CodeEncript = sqlDataReader["CodeEncrpt"].ToString();
            objDistributor.Nominal = Convert.ToDecimal(sqlDataReader["Nominal"]);
            if (string.IsNullOrEmpty(sqlDataReader["ConfirmationDate"].ToString()))
                objDistributor.ConfirmationDate = DateTime.MaxValue;
            else
                objDistributor.ConfirmationDate = Convert.ToDateTime(sqlDataReader["ConfirmationDate"]);

            return objDistributor;
        }

        public Distributor GenerateObjectSept15(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDistributor.DistributorCode = sqlDataReader["DistributorCode"].ToString();
            objDistributor.DistributorName = sqlDataReader["DistributorName"].ToString();
            objDistributor.Address = sqlDataReader["Address"].ToString();
            objDistributor.NPWP = sqlDataReader["NPWP"].ToString();
            return objDistributor;
        }
        public Distributor GenerateObjectTokoID(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            return objDistributor;
        }
        public Distributor GenerateObjectForSpesialIBG(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.DistributorName = sqlDataReader["DistLevel2Name"].ToString();
            objDistributor.Address = sqlDataReader["Address"].ToString();
            objDistributor.NPWP = sqlDataReader["NPWP"].ToString();

            return objDistributor;
        }
        public Distributor GenerateObject(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDistributor.DistributorCode = sqlDataReader["DistributorCode"].ToString();
            objDistributor.DistributorName = sqlDataReader["DistributorName"].ToString();
            objDistributor.Address = sqlDataReader["Address"].ToString();            
            objDistributor.Telepon = sqlDataReader["Telepon"].ToString();
            objDistributor.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objDistributor.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objDistributor.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objDistributor.ZonaCode  = sqlDataReader["ZonaCode"].ToString();
            objDistributor.NPWP = sqlDataReader["NPWP"].ToString();                 
            objDistributor.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objDistributor.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objDistributor.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objDistributor.DepoName = sqlDataReader["DepoName"].ToString();
            objDistributor.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDistributor.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDistributor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDistributor.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDistributor.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objDistributor.ChargeToPayment = Convert.ToInt32(sqlDataReader["ChargeToPayment"]);
            objDistributor.PriceTypeID = Convert.ToInt32(sqlDataReader["PriceTypeID"]);

            return objDistributor;
        }

        public Distributor GenerateObject1(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.IdDistStr = Convert.ToString(sqlDataReader["IdDistStr"]);
            return objDistributor;
        }
        public Distributor GenerateObject2(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.OrderOP = Convert.ToInt32(sqlDataReader["OrderOP"]);
            return objDistributor;
        }
        public Distributor GenerateObjectEmail(SqlDataReader sqlDataReader)
        {
            objDistributor = new Distributor();
            objDistributor.EmailAddress = sqlDataReader["EmailAddress"].ToString();
            objDistributor.Pic = sqlDataReader["Pic"].ToString();

            return objDistributor;
        }



    }
}

