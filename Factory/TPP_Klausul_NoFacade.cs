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
    public class TPP_Klausul_NoFacade : AbstractTransactionFacadeF
    {
        private TPP_Klausul_No objTPP_Klausul_No = new TPP_Klausul_No();
        private ArrayList arrTPP;
        private List<SqlParameter> sqlListParam;

        public TPP_Klausul_NoFacade(object objDomain)
            : base(objDomain)
        {
            objTPP_Klausul_No = (TPP_Klausul_No)objDomain;
        }
        public TPP_Klausul_NoFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                //sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@TPP_ID", objTPP_Klausul_No.TPP_ID));
                //sqlListParam.Add(new SqlParameter("@Tindakan", objTPP_Klausul_No.Tindakan));
                //sqlListParam.Add(new SqlParameter("@Pelaku", objTPP_Klausul_No.Pelaku));
                //sqlListParam.Add(new SqlParameter("@Jadwal_Selesai", objTPP_Klausul_No.Jadwal_Selesai));
                //sqlListParam.Add(new SqlParameter("@Aktual_Selesai", objTPP_Klausul_No.Aktual_Selesai));
                //sqlListParam.Add(new SqlParameter("@Jenis", objTPP_Klausul_No.Jenis));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP_Klausul_No.CreatedBy));
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
            string strSQL = "SELECT * from tpp_Klausul_No where rowstatus>-1  order by klausul_no ";
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
                arrTPP.Add(new TPP_Klausul_No());
            return arrTPP;
        }
        public ArrayList RetrieveByNo(string No, string jenis)
        {
            string strSQL = "SELECT * from tpp_Klausul_No order by klausul_no where rowstatus>-1 and Klausul_No='"+No+"'";
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
                arrTPP.Add(new TPP_Klausul_No());
            return arrTPP;
        }

        public TPP_Klausul_No GenerateObject(SqlDataReader sqlDataReader)
        {
            objTPP_Klausul_No = new TPP_Klausul_No();
            //objTPP_Klausul_No.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objTPP_Klausul_No = Convert.ToInt32(sqlDataReader["TPP_ID"]);
            objTPP_Klausul_No.Klausul_No = sqlDataReader["Klausul_No"].ToString();
            objTPP_Klausul_No.Deskripsi = sqlDataReader["Deskripsi"].ToString();
            //objTPP_Klausul_No.Jadwal_Selesai = Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"]);
            //objTPP_Klausul_No.Aktual_Selesai = Convert.ToDateTime(sqlDataReader["Aktual_Selesai"]);
            //objTPP_Klausul_No.Verifikasi = Convert.ToInt32(sqlDataReader["Verifikasi"]);
            return objTPP_Klausul_No;
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
