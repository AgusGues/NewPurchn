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
 public class MasterTransaksiFacade : AbstractFacade
    {
        private MasterTransaksi objGrade = new MasterTransaksi();
        private ArrayList arrGrade;
        private MasterTransaksi objPelarian = new MasterTransaksi();
        private ArrayList arrPelarian;
        private List<SqlParameter> sqlListParam;

        public MasterTransaksiFacade()
            : base()
        {

        }

        public ArrayList GetYearTrans()
        {
            string strSQL = "select * from (select Year(TglTransaksi) as PelarianDate from Pel_Transaksi group by year(TglTransaksi) ) as T order by T.PelarianDate";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPelarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPelarian.Add(GenerateObjectYear(sqlDataReader));
                }
            }
            else
                arrPelarian.Add(new MasterTransaksi());

            return arrPelarian;
        }

        public MasterTransaksi GenerateObjectYear(SqlDataReader sqlDataReader)
        {
            objPelarian = new MasterTransaksi();
            objPelarian.Tahun = Convert.ToInt16(sqlDataReader["PelarianDate"]);
            return objPelarian;
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGrade = (MasterTransaksi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PelarianNo", objGrade.PelarianNo));
                sqlListParam.Add(new SqlParameter("@IDRegu", objGrade.IDRegu));
                sqlListParam.Add(new SqlParameter("@ReguCode", objGrade.ReguCode));
                sqlListParam.Add(new SqlParameter("@IDType", objGrade.IDType));
                sqlListParam.Add(new SqlParameter("@NamaType", objGrade.NamaType));
                sqlListParam.Add(new SqlParameter("@IDUkuran", objGrade.IDUkuran));
                sqlListParam.Add(new SqlParameter("@Ukuran", objGrade.Ukuran));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objGrade.TglProduksi));
                sqlListParam.Add(new SqlParameter("@TglTransaksi", objGrade.TglTransaksi));     // new
                sqlListParam.Add(new SqlParameter("@KodePelarian", objGrade.KodePelarian));
                sqlListParam.Add(new SqlParameter("@Jumlah", objGrade.Jumlah));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGrade.CreatedBy));
                sqlListParam.Add(new SqlParameter("@DestID", objGrade.DestID));
                sqlListParam.Add(new SqlParameter("@SerahID", objGrade.SerahID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertPel_Transaksi");

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
                objGrade = (MasterTransaksi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGrade.ID));
                sqlListParam.Add(new SqlParameter("@PelarianNo", objGrade.PelarianNo));
                sqlListParam.Add(new SqlParameter("@IDRegu", objGrade.IDRegu));
                sqlListParam.Add(new SqlParameter("@ReguCode", objGrade.ReguCode));
                sqlListParam.Add(new SqlParameter("@IDType", objGrade.IDType));
                sqlListParam.Add(new SqlParameter("@NamaType", objGrade.NamaType));
                sqlListParam.Add(new SqlParameter("@IDUkuran", objGrade.IDUkuran));
                sqlListParam.Add(new SqlParameter("@Ukuran", objGrade.Ukuran));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objGrade.TglProduksi));
                sqlListParam.Add(new SqlParameter("@TglTransaksi", objGrade.TglTransaksi));
                sqlListParam.Add(new SqlParameter("@KodePelarian", objGrade.KodePelarian));
                sqlListParam.Add(new SqlParameter("@Jumlah", objGrade.Jumlah));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGrade.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePel_Transaksi");

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
                objGrade = (MasterTransaksi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGrade.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGrade.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletePel_Transaksi");

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
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,PelarianNo,IDRegu,ReguCode,IDType,NamaType,IDUkuran,Ukuran,convert(varchar,TglProduksi,103) as TglProduksi,KodePelarian,convert(int, Jumlah ) as Jumlah,RowStatus,CreatedBy,CreatedTime,LastModifiedBy,LastModifiedTime from Pel_Transaksi where RowStatus > -1");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,PelarianNo,IDRegu,ReguCode,IDType,NamaType,IDUkuran,Ukuran,TglProduksi,TglTransaksi,KodePelarian,convert(int, Jumlah ) as Jumlah,RowStatus,CreatedBy,CreatedTime,LastModifiedBy,LastModifiedTime from Pel_Transaksi where RowStatus > -1 order by ID desc");
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new MasterTransaksi());

            return arrGrade;
        }
        public  ArrayList RetrieveByTglproduksi(string tgproduksi)
        {
            string strSQL = "select ID,PelarianNo,IDRegu,ReguCode,IDType,NamaType,IDUkuran,Ukuran,TglProduksi,TglTransaksi,KodePelarian, " + 
                "convert(int, Jumlah ) as Jumlah,RowStatus,CreatedBy,CreatedTime,LastModifiedBy,LastModifiedTime from Pel_Transaksi "+
                "where convert(char,tglproduksi,112)='" + tgproduksi  + "' and RowStatus > -1 order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new MasterTransaksi());

            return arrGrade;
        }
        public MasterTransaksi RetrieveMaxId()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(LEFT(PelarianNo,6)),0) as ID from Pel_Transaksi");
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject5(sqlDataReader);
                }
            }

            return new MasterTransaksi();
        }
     
        public MasterTransaksi RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_Transaksi where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MasterTransaksi();
        }

       
       

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_Transaksi where RowStatus = 0 and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new MasterTransaksi());

            return arrGrade;
        }

        public MasterTransaksi GenerateObject(SqlDataReader sqlDataReader)
        {
            objGrade = new MasterTransaksi();
            objGrade.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGrade.PelarianNo = sqlDataReader["PelarianNo"].ToString();
            objGrade.IDRegu = Convert.ToInt32(sqlDataReader["IDRegu"]);
            objGrade.ReguCode = sqlDataReader["ReguCode"].ToString();
            objGrade.IDType = Convert.ToInt32(sqlDataReader["IDType"]);
            objGrade.NamaType = sqlDataReader["NamaType"].ToString();
            objGrade.IDUkuran = Convert.ToInt32(sqlDataReader["IDUkuran"]);
            objGrade.Ukuran = sqlDataReader["Ukuran"].ToString();
            if (Convert.ToString(sqlDataReader["TglProduksi"]) != string.Empty && Convert.ToString(sqlDataReader["TglProduksi"]) != null)
            { objGrade.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]); }
            if (Convert.ToString(sqlDataReader["TglTransaksi"]) != string.Empty && Convert.ToString(sqlDataReader["TglTransaksi"]) != null)
            { objGrade.TglTransaksi = Convert.ToDateTime(sqlDataReader["TglTransaksi"]); }
            objGrade.KodePelarian = sqlDataReader["KodePelarian"].ToString();
            objGrade.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objGrade.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objGrade.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objGrade.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objGrade.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objGrade.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objGrade;
        }

       

        public MasterTransaksi GenerateObject5(SqlDataReader sqlDataReader)
        {
            objGrade = new MasterTransaksi();
            objGrade.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objGrade;
        }
    }
}
