using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
    public class TPP_Asal_MasalahFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private TPP_Asal_Masalah objTPP_Asal_Masalah = new TPP_Asal_Masalah();
        private ArrayList arrTPP;
        private List<SqlParameter> sqlListParam;

        public TPP_Asal_MasalahFacade(object objDomain)
            : base(objDomain)
        {
            objTPP_Asal_Masalah = (TPP_Asal_Masalah)objDomain;
        }
        public TPP_Asal_MasalahFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                //sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@TPP_ID", objTPP_Asal_Masalah.TPP_ID));
                //sqlListParam.Add(new SqlParameter("@Tindakan", objTPP_Asal_Masalah.Tindakan));
                //sqlListParam.Add(new SqlParameter("@Pelaku", objTPP_Asal_Masalah.Pelaku));
                //sqlListParam.Add(new SqlParameter("@Jadwal_Selesai", objTPP_Asal_Masalah.Jadwal_Selesai));
                //sqlListParam.Add(new SqlParameter("@Aktual_Selesai", objTPP_Asal_Masalah.Aktual_Selesai));
                //sqlListParam.Add(new SqlParameter("@Jenis", objTPP_Asal_Masalah.Jenis));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP_Asal_Masalah.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "TPP_Insert_Tindakan");
                strError = transManager.Error;
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
            string strSQL = "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP_Asal_Masalah());
            return arrTPP;
        }
        public ArrayList RetrieveByNo(string No, string jenis)
        {
            string strSQL = "SELECT A.ID,A.TPP_ID, A.Tindakan, A.Pelaku, A.Jadwal_Selesai, A.Aktual_Selesai, A.Verifikasi " +
                "FROM TPP_Asal_Masalah A inner join TPP C on A.TPP_ID=C.ID " +
                "where A.rowstatus>-1 and C.Laporan_No='" + No + "' and A.jenis='" + jenis + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP_Asal_Masalah());
            return arrTPP;
        }

        public TPP_Asal_Masalah GenerateObject(SqlDataReader sqlDataReader)
        {
            objTPP_Asal_Masalah = new TPP_Asal_Masalah();
            //objTPP_Asal_Masalah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objTPP_Asal_Masalah = Convert.ToInt32(sqlDataReader["TPP_ID"]);
            //objTPP_Asal_Masalah.Tindakan = sqlDataReader["Tindakan"].ToString();
            //objTPP_Asal_Masalah.Pelaku = sqlDataReader["Pelaku"].ToString();
            //objTPP_Asal_Masalah.Jadwal_Selesai = Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"]);
            //objTPP_Asal_Masalah.Aktual_Selesai = Convert.ToDateTime(sqlDataReader["Aktual_Selesai"]);
            //objTPP_Asal_Masalah.Verifikasi = Convert.ToInt32(sqlDataReader["Verifikasi"]);
            return objTPP_Asal_Masalah;
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
