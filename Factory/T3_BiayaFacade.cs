using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;


namespace Factory
{
    public class T3_BiayaFacade : AbstractFacade
    {
        private T3_Biaya objT3_Biaya = new T3_Biaya();
        private ArrayList arrT3_Biaya;
        private List<SqlParameter> sqlListParam;


        public T3_BiayaFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objT3_Biaya = (T3_Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun", objT3_Biaya.Tahun ));
                sqlListParam.Add(new SqlParameter("@Bulan", objT3_Biaya.Bulan ));
                sqlListParam.Add(new SqlParameter("@COA", objT3_Biaya.COA ));
                sqlListParam.Add(new SqlParameter("@AccName", objT3_Biaya.AccName ));
                sqlListParam.Add(new SqlParameter("@Biaya", objT3_Biaya.Biaya ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Biaya.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT3_Biaya");

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
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objT3_Biaya = (T3_Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun", objT3_Biaya.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", objT3_Biaya.Bulan));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteT3_Biaya");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList RetrieveByPeriode(int tahun, int bulan)
        {
            string strSQL = "SELECT * from t3_biaya where tahun=" + tahun + " and bulan=" + bulan;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Biaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Biaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Biaya.Add(new T3_Biaya());
            return arrT3_Biaya;
        }

        public decimal  RetrieveTotalBiaya(int tahun, int bulan)
        {
            decimal biaya = 0;
            string strSQL = "SELECT isnull(Sum(biaya),0) as biaya from t3_biaya where tahun=" + tahun + " and bulan=" + bulan;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Biaya = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    biaya = Convert.ToDecimal(sqlDataReader["Biaya"].ToString());
                }
            }
            return biaya;
         }

        public T3_Biaya GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Biaya = new T3_Biaya();
            objT3_Biaya.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            objT3_Biaya.Bulan = Convert.ToInt32(sqlDataReader["Bulan"]);
            objT3_Biaya.COA = sqlDataReader["COA"].ToString();
            objT3_Biaya.AccName = sqlDataReader["AccName"].ToString();
            objT3_Biaya.Biaya = Convert.ToDecimal(sqlDataReader["Biaya"].ToString());
            return objT3_Biaya;
        }

    }
}
