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
    public class SubDistributorFacade : AbstractFacade
    {
        private SubDistributor objSubDistributor = new SubDistributor();
        private ArrayList arrSubDistributor;
        private List<SqlParameter> sqlListParam;

        public SubDistributorFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSubDistributor = (SubDistributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SubDistributorCode", objSubDistributor.SubDistributorCode));
                sqlListParam.Add(new SqlParameter("@SubDistributorName", objSubDistributor.SubDistributorName));
                sqlListParam.Add(new SqlParameter("@Address", objSubDistributor.Address));
                sqlListParam.Add(new SqlParameter("@JoinDate", objSubDistributor.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objSubDistributor.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objSubDistributor.ZonaID));
                sqlListParam.Add(new SqlParameter("@DistributorID", objSubDistributor.DistributorID));
                sqlListParam.Add(new SqlParameter("@NPWP", objSubDistributor.NPWP));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objSubDistributor.ContactPerson)); 
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSubDistributor.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSubDistributor");

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
                objSubDistributor = (SubDistributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSubDistributor.ID));
                sqlListParam.Add(new SqlParameter("@SubDistributorCode", objSubDistributor.SubDistributorCode));
                sqlListParam.Add(new SqlParameter("@SubDistributorName", objSubDistributor.SubDistributorName));
                sqlListParam.Add(new SqlParameter("@Address", objSubDistributor.Address));
                sqlListParam.Add(new SqlParameter("@JoinDate", objSubDistributor.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreditLimit", objSubDistributor.CreditLimit));
                sqlListParam.Add(new SqlParameter("@ZonaID", objSubDistributor.ZonaID));
                sqlListParam.Add(new SqlParameter("@DistributorID", objSubDistributor.DistributorID));
                sqlListParam.Add(new SqlParameter("@NPWP", objSubDistributor.NPWP));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objSubDistributor.ContactPerson));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSubDistributor.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSubDistributor");

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
                objSubDistributor = (SubDistributor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSubDistributor.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSubDistributor.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteSubDistributor");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }
        public ArrayList RetrieveForDistLevel2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DistLevel2Code as SubDistributorCode,DistLevel2Name as SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,C.PriceTypeID from DistributorLevel2 as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and C.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public ArrayList RetrieveByDepoPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and C.DepoID in (1,7) ");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public  ArrayList RetrieveByDistributor(int distributorId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.DistributorID = " + distributorId);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public SubDistributor RetrieveByDistributor2(int distributorId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.DistributorID = " + distributorId);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSubDistributor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSubDistributor.Add(new SubDistributor());

            return arrSubDistributor;
        }

        public SubDistributor RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveByIdTax(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorNameTax as SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveByCodeByDate2(string Datenya, int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
            "case when (select top 1 SubDistributorHistory.ID from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") is  null " +
            "then (select top 1 SubDistributor.ID from SubDistributor where SubDistributor.ID = " + Id + ") " +
            "else (select top 1 SubDistributorHistory.ID from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") end ID, " +
            "case when (select top 1 SubDistributorHistory.SubDistributorCode from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") is  null " +
            "then (select top 1 SubDistributor.SubDistributorCode from SubDistributor where SubDistributor.ID = " + Id + ") " +
            "else (select top 1 SubDistributorHistory.SubDistributorCode from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") end SubDistributorCode, " +
            "case when (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") is  null " +
            "then (select top 1 SubDistributor.SubDistributorNameTax as SubDistributorName from SubDistributor where SubDistributor.ID = " + Id + ") " +
            "else (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") end SubDistributorName, " +
            "case when (select top 1 SubDistributorHistory.NPWP from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") is  null " +
            "then (select top 1 SubDistributor.NPWP from SubDistributor where SubDistributor.ID = " + Id + ") " +
            "else (select top 1 SubDistributorHistory.NPWP from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") end NPWP, " +
            "case when (select top 1 SubDistributorHistory.Address from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") is  null " +
            "then (select top 1 SubDistributor.Address from SubDistributor where SubDistributor.ID = " + Id + ") " +
            "else (select top 1 SubDistributorHistory.Address from Invoice,SubDistributor,SubDistributorHistory " +
            "where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
            "and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.ID = " + Id + ") end Address");

            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveById1(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.DistributorID = " + Id);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }

        public SubDistributor RetrieveByIdPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.ID in (1,7) ");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }

        public SubDistributor RetrieveByCode(string subDistributorCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and A.SubDistributorCode = '" + subDistributorCode + "'");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveByDistCode(string distributorCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SubDistributorCode,A.SubDistributorName,A.Address,A.JoinDate,A.CreditLimit,A.ZonaID,B.ZonaCode,A.DistributorID,C.DistributorName,A.NPWP,A.TotalPoint,A.ContactPerson,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.PriceTypeID from SubDistributor as A,Zona as B,Distributor as C where A.ZonaID = B.ID and A.DistributorID = C.ID and A.RowStatus = 0 and C.DistributorCode = '" + distributorCode + "'");
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveByNoSept15(string strNoTax)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case when " +
                                    "(select SubDistributorHistory.ID from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) is null  " +
                                    "then (select SubDistributor.ID from SubDistributor where SubDistributor.ID=FakturPajak.CustomerID)  " +
                                    "else (select SubDistributorHistory.ID from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) " +
                                    " end ID, " +
                                    "case when (select SubDistributorHistory.SubDistributorCode from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) is null  " +
                                    "then (select SubDistributor.SubDistributorCode from SubDistributor where SubDistributor.ID=FakturPajak.CustomerID)  " +
                                    "else (select SubDistributorHistory.SubDistributorCode from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) " +
                                    " end DistributorCode, " +
                                    "case when (select SubDistributorHistory.SubDistributorName from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) is null  " +
                                    "then (select SubDistributor.SubDistributorName from SubDistributor where SubDistributor.ID=FakturPajak.CustomerID)  " +
                                    "else (select SubDistributorHistory.SubDistributorName from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) " +
                                    " end DistributorName, " +
                                    " case when (select SubDistributorHistory.Address from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) is null  " +
                                    "then (select SubDistributor.Address from SubDistributor where SubDistributor.ID=FakturPajak.CustomerID)  " +
                                    "else (select SubDistributorHistory.Address from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and SubDistributorHistory.SubDistributorCode in(select SubDistributorCode from SubDistributor where ID = FakturPajak.CustomerID) ) " +
                                    " end Address, " +
                                    "case when (select SubDistributorHistory.SubDistributorName from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                                    "SubDistributorHistory.DistributorID ) is null then (select SubDistributor.NPWP from SubDistributor where SubDistributor.ID=FakturPajak.CustomerID)  " +
                                    "else (select NPWP from SubDistributorHistory where FakturPajak.TglPenerimaan>=FromPeriod and FakturPajak.TglPenerimaan<=ToPeriod and FakturPajak.CustomerID= " +
                                    "SubDistributorHistory.DistributorID )  end NPWP " +
                                    "from FakturPajak where TaxNo= '" + strNoTax + "'");

            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor RetrieveByCodeByTTNo(string TTNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
"case when (select top 1 SubDistributorHistory.ID from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 SubDistributor.ID from SubDistributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = SubDistributor.ID and SubDistributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 SubDistributorHistory.ID from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end ID, " +
"case when (select top 1 SubDistributorHistory.SubDistributorCode from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 SubDistributor.SubDistributorCode from SubDistributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = SubDistributor.ID and SubDistributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 SubDistributorHistory.SubDistributorCode from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end SubDistributorCode, " +
"case when (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName  from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 SubDistributor.SubDistributorNameTax as SubDistributorName from SubDistributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = SubDistributor.ID and SubDistributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end SubDistributorName, " +
"case when (select top 1 SubDistributorHistory.NPWP from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 SubDistributor.NPWP from SubDistributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = SubDistributor.ID and SubDistributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 SubDistributorHistory.NPWP from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end NPWP, " +
"case when (select top 1 SubDistributorHistory.Address from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') is  null " +
"then (select top 1 SubDistributor.Address from SubDistributor,Invoice,TandaTerima where TandaTerima.CustomerType = Invoice.CustomerType and TandaTerima.CustomerID =Invoice.CustomerID " +
"and Invoice.CustomerID = SubDistributor.ID and SubDistributor.ID = TandaTerima.CustomerID and TandaTerimaNo = '" + TTNo + "') " +
"else (select top 1 SubDistributorHistory.Address from TandaTerima,TandaTerimaDetail,Invoice,SubDistributor,SubDistributorHistory " +
"where TandaTerima.ID = TandaTerimaDetail.TandaTerimaID and TandaTerimaDetail.InvoiceID = Invoice.ID and TandaTerima.CustomerType = Invoice.CustomerType " +
"and TandaTerima.CustomerID =Invoice.CustomerID and Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and Invoice.TglPenerimaan >=  SubDistributorHistory.FromPeriod and Invoice.TglPenerimaan <= SubDistributorHistory.ToPeriod and TandaTerimaNo = '" + TTNo + "') end Address");

            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new SubDistributor();
        }

        public SubDistributor RetrieveByCodeByDate(string Datenya, string strSubDistributorCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select " +
"case when (select top 1 SubDistributorHistory.ID from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') is  null " +
"then (select top 1 SubDistributor.ID from SubDistributor where SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') " +
"else (select top 1 SubDistributorHistory.ID from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') end ID, " +
"case when (select top 1 SubDistributorHistory.SubDistributorCode from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') is  null " +
"then (select top 1 SubDistributor.SubDistributorCode from SubDistributor where SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') " +
"else (select top 1 SubDistributorHistory.SubDistributorCode from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') end SubDistributorCode, " +
"case when (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') is  null " +
"then (select top 1 SubDistributor.SubDistributorNameTax as SubDistributorName from SubDistributor where SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') " +
"else (select top 1 SubDistributorHistory.SubDistributorNameTax as SubDistributorName from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') end SubDistributorName, " +
"case when (select top 1 SubDistributorHistory.NPWP from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') is  null " +
"then (select top 1 SubDistributor.NPWP from SubDistributor where SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') " +
"else (select top 1 SubDistributorHistory.NPWP from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') end NPWP, " +
"case when (select top 1 SubDistributorHistory.Address from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') is  null " +
"then (select top 1 SubDistributor.Address from SubDistributor where SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') " +
"else (select top 1 SubDistributorHistory.Address from Invoice,SubDistributor,SubDistributorHistory " +
"where Invoice.CustomerID = SubDistributor.ID and SubDistributor.SubDistributorCode = SubDistributorHistory.SubDistributorCode " +
"and '" + Datenya + "' >=  convert(varchar,SubDistributorHistory.FromPeriod,112) and '" + Datenya + "' <= convert(varchar,SubDistributorHistory.ToPeriod,112) and SubDistributor.SubDistributorCode = '" + strSubDistributorCode + "') end Address";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSept15(sqlDataReader);
                }
            }

            return new SubDistributor();
        }
        public SubDistributor GetEmailAddress(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(EmailAddress,'') as EmailAddress,isnull(Pic,'') as Pic from SubDistributor where id = " + id);
            strError = dataAccess.Error;
            arrSubDistributor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectEmail(sqlDataReader);
                }
            }

            return new SubDistributor();
        }

        public SubDistributor GenerateObjectSept15(SqlDataReader sqlDataReader)
        {
            objSubDistributor = new SubDistributor();
            objSubDistributor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSubDistributor.SubDistributorCode = sqlDataReader["SubDistributorCode"].ToString();
            objSubDistributor.SubDistributorName = sqlDataReader["SubDistributorName"].ToString();
            objSubDistributor.Address = sqlDataReader["Address"].ToString();
            objSubDistributor.NPWP = sqlDataReader["NPWP"].ToString();
            return objSubDistributor;
        }

        public SubDistributor GenerateObject(SqlDataReader sqlDataReader)
        {
            objSubDistributor = new SubDistributor();
            objSubDistributor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSubDistributor.SubDistributorCode = sqlDataReader["SubDistributorCode"].ToString();
            objSubDistributor.SubDistributorName = sqlDataReader["SubDistributorName"].ToString();
            objSubDistributor.Address = sqlDataReader["Address"].ToString();
            objSubDistributor.JoinDate = Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objSubDistributor.CreditLimit = Convert.ToDecimal(sqlDataReader["CreditLimit"]);
            objSubDistributor.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objSubDistributor.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            objSubDistributor.DistributorID = Convert.ToInt32(sqlDataReader["DistributorID"]);
            objSubDistributor.DistributorName = sqlDataReader["DistributorName"].ToString();
            objSubDistributor.NPWP = sqlDataReader["NPWP"].ToString();
            objSubDistributor.TotalPoint = Convert.ToDecimal(sqlDataReader["TotalPoint"]);
            objSubDistributor.ContactPerson = sqlDataReader["ContactPerson"].ToString();    
            objSubDistributor.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objSubDistributor.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSubDistributor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSubDistributor.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSubDistributor.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objSubDistributor.PriceTypeID = Convert.ToInt32(sqlDataReader["PriceTypeID"]);

            return objSubDistributor;
        }
        public SubDistributor GenerateObjectEmail(SqlDataReader sqlDataReader)
        {
            objSubDistributor = new SubDistributor();
            objSubDistributor.EmailAddress = sqlDataReader["EmailAddress"].ToString();
            objSubDistributor.Pic = sqlDataReader["Pic"].ToString();

            return objSubDistributor;
        }



    }
}

