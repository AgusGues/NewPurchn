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
    public class SaldoInventoryBJFacade : AbstractFacade
    {
        private SaldoInventoryBJ objSaldoInventoryBJ = new SaldoInventoryBJ();
        private TotalSaldoUkuran objSaldoUkuran = new TotalSaldoUkuran();
        private ArrayList arrSaldoInventoryBJ;
        private List<SqlParameter> sqlListParam;

        public SaldoInventoryBJFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryBJ.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryBJ.ItemTypeID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSaldoInventoryBJ");
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
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                //if (objSaldoInventoryBJ.Quantity > 0)
                //{
                //    string c = "ada";
                //}
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryBJ.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryBJ.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryBJ.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryBJ.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryBJ.Posting));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventoryBJ.SaldoPrice ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoInventoryBJ");
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

            return -1;
        }

        public int KosongkanSaldo(object objDomain)
        {

            try
            {
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryBJ.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spKosongkanSaldoInventoryBJ");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int MinusSaldo(object objDomain)
        {

            try
            {
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryBJ.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryBJ.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryBJ.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryBJ.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryBJ.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusSaldoInventoryBJ");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateSaldoAvgPriceBlnIni(object objDomain)
        {

            try
            {
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryBJ.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryBJ.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventoryBJ.AvgPrice));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryBJ.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoAvgPriceBlnIniBJ");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateSaldoBlnLalu(object objDomain)
        {

            try
            {
                objSaldoInventoryBJ = (SaldoInventoryBJ)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryBJ.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryBJ.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryBJ.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryBJ.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryBJ.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryBJ.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryBJ.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoBlnLaluBJ");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateSaldoNull(int tahun, int bulan)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@YearPeriod", tahun));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", bulan));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoBJNull");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryBJ");
            strError = dataAccess.Error;
            arrSaldoInventoryBJ = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryBJ.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryBJ.Add(new SaldoInventoryBJ());

            return arrSaldoInventoryBJ;

        }

        public SaldoInventoryBJ RetrieveByItemID(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryBJ where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID="+itemtypeID);
            strError = dataAccess.Error;
            arrSaldoInventoryBJ = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SaldoInventoryBJ();
        }

        public int GetPrice(int itemID, string MonthAvgPrice, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " + MonthAvgPrice + 
                " from SaldoInventoryBJ where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
            strError = dataAccess.Error;
            arrSaldoInventoryBJ = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader[MonthAvgPrice]);
                }
            }

            return 0;
        }
        public int CekRow(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryBJ where ItemId=" + itemID + 
                " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                return 1;
            }

            return 0;
        }
        public TotalSaldoUkuran GetBP(string strSQL)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSaldoInventoryBJ = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUkuran(sqlDataReader);
                }
            }

            return new TotalSaldoUkuran();
        }

        public TotalSaldoUkuran GetOK(string strSQL)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSaldoInventoryBJ = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUkuran(sqlDataReader);
                }
            }

            return new TotalSaldoUkuran();
        }

        public TotalSaldoUkuran GenerateObjectUkuran(SqlDataReader sqlDataReader)
        {
            objSaldoUkuran = new TotalSaldoUkuran();
            objSaldoUkuran.Saldoawalbp = Convert.ToInt32(sqlDataReader["Saldoawalbp"]);
            objSaldoUkuran.Saldoawalbpkubik = Convert.ToDecimal(sqlDataReader["Saldoawalbpkubik"]);
            objSaldoUkuran.Saldobp = Convert.ToInt32(sqlDataReader["Saldobp"]);
            objSaldoUkuran.Saldobpkubik = Convert.ToDecimal(sqlDataReader["Saldobpkubik"]);
            objSaldoUkuran.Saldoawalok = Convert.ToInt32(sqlDataReader["Saldoawalok"]);
            objSaldoUkuran.Saldoawalokkubik = Convert.ToDecimal(sqlDataReader["Saldoawalokkubik"]);
            objSaldoUkuran.Saldook = Convert.ToInt32(sqlDataReader["Saldook"]);
            objSaldoUkuran.Saldookkubik = Convert.ToDecimal(sqlDataReader["Saldookkubik"]);
            return objSaldoUkuran;
        }

        public SaldoInventoryBJ GenerateObject3(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryBJ = new SaldoInventoryBJ();
            objSaldoInventoryBJ.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryBJ.AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"]);
            return objSaldoInventoryBJ;
        }

        public SaldoInventoryBJ GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryBJ = new SaldoInventoryBJ();
            objSaldoInventoryBJ.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryBJ.PartNo  = sqlDataReader["ItemCode"].ToString();
            objSaldoInventoryBJ.ItemDesc  = sqlDataReader["ItemName"].ToString();
            objSaldoInventoryBJ.StokAwal = Convert.ToDecimal(sqlDataReader["StokAwal"]);
            objSaldoInventoryBJ.StokAkhir = Convert.ToDecimal(sqlDataReader["StokAkhir"]);
            return objSaldoInventoryBJ;
        }

        public SaldoInventoryBJ GenerateObject(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryBJ = new SaldoInventoryBJ();
            objSaldoInventoryBJ.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryBJ.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objSaldoInventoryBJ.SaldoQty = Convert.ToDecimal(sqlDataReader["SaldoQty"]);
            objSaldoInventoryBJ.SaldoPrice = Convert.ToDecimal(sqlDataReader["SaldoPrice"]);
            objSaldoInventoryBJ.JanQty = Convert.ToDecimal(sqlDataReader["JanQty"]);
            objSaldoInventoryBJ.JanAvgPrice = Convert.ToDecimal(sqlDataReader["JanAvgPrice"]);
            objSaldoInventoryBJ.FebQty = Convert.ToDecimal(sqlDataReader["FebQty"]);
            objSaldoInventoryBJ.FebAvgPrice = Convert.ToDecimal(sqlDataReader["FebAvgPrice"]);
            objSaldoInventoryBJ.MarQty = Convert.ToDecimal(sqlDataReader["MarQty"]);
            objSaldoInventoryBJ.MarAvgPrice = Convert.ToDecimal(sqlDataReader["MarAvgPrice"]);
            objSaldoInventoryBJ.AprQty = Convert.ToDecimal(sqlDataReader["AprQty"]);
            objSaldoInventoryBJ.AprAvgPrice = Convert.ToDecimal(sqlDataReader["AprAvgPrice"]);
            objSaldoInventoryBJ.MeiQty = Convert.ToDecimal(sqlDataReader["MeiQty"]);
            objSaldoInventoryBJ.MeiAvgPrice = Convert.ToDecimal(sqlDataReader["MeiAvgPrice"]);
            objSaldoInventoryBJ.JunQty = Convert.ToDecimal(sqlDataReader["JunQty"]);
            objSaldoInventoryBJ.JunAvgPrice = Convert.ToDecimal(sqlDataReader["JunAvgPrice"]);
            objSaldoInventoryBJ.JulQty = Convert.ToDecimal(sqlDataReader["JulQty"]);
            objSaldoInventoryBJ.JulAvgPrice = Convert.ToDecimal(sqlDataReader["JulAvgPrice"]);
            objSaldoInventoryBJ.AguQty = Convert.ToDecimal(sqlDataReader["AguQty"]);
            objSaldoInventoryBJ.AguAvgPrice = Convert.ToDecimal(sqlDataReader["AguAvgPrice"]);
            objSaldoInventoryBJ.SepQty = Convert.ToDecimal(sqlDataReader["SepQty"]);
            objSaldoInventoryBJ.SepAvgPrice = Convert.ToDecimal(sqlDataReader["SepAvgPrice"]);
            objSaldoInventoryBJ.OktQty = Convert.ToDecimal(sqlDataReader["OktQty"]);
            objSaldoInventoryBJ.OktAvgPrice = Convert.ToDecimal(sqlDataReader["OktAvgPrice"]);
            objSaldoInventoryBJ.NovQty = Convert.ToDecimal(sqlDataReader["NovQty"]);
            objSaldoInventoryBJ.NovAvgPrice = Convert.ToDecimal(sqlDataReader["NovAvgPrice"]);
            objSaldoInventoryBJ.DesQty = Convert.ToDecimal(sqlDataReader["DesQty"]);
            objSaldoInventoryBJ.DesAvgPrice = Convert.ToDecimal(sqlDataReader["DesAvgPrice"]);

            return objSaldoInventoryBJ;
        }
    }
}
