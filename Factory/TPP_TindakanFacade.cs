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
    public class TPP_TindakanFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private TPP_Tindakan objTPP = new TPP_Tindakan();
        private ArrayList arrTPP;
        private List<SqlParameter> sqlListParam;

        public TPP_TindakanFacade(object objDomain)
            : base(objDomain)
        {
            objTPP = (TPP_Tindakan)objDomain;
        }
        public TPP_TindakanFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TPP_ID", objTPP.TPP_ID));
                sqlListParam.Add(new SqlParameter("@Tindakan", objTPP.Tindakan));
                sqlListParam.Add(new SqlParameter("@Pelaku", objTPP.Pelaku));
                sqlListParam.Add(new SqlParameter("@Jadwal_Selesai", objTPP.Jadwal_Selesai));
                //sqlListParam.Add(new SqlParameter("@Aktual_Selesai", objTPP.Aktual_Selesai));
                sqlListParam.Add(new SqlParameter("@Jenis", objTPP.Jenis));
                sqlListParam.Add(new SqlParameter("@Target", objTPP.Target));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "TPP_Insert_Tindakan1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string UpdateTindakan(string ID, string tgl, string verf )
        {
            string strSQL = "update tpp_tindakan set verifikasi="+ verf +",tglverifikasi='" +tgl + "' where ID="+ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateAktualSelesai(string ID, string tgl0, string tgl)
        {
            string strSQL = "update tpp_tindakan set jadwal_Selesai='" + tgl0 + "',Aktual_Selesai='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string DeleteTindakan(string ID)
        {
            string strSQL = "Delete tpp_tindakan where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
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
                arrTPP.Add(new TPP_Tindakan());
            return arrTPP;
        }
        public ArrayList RetrieveByNo(string No, string jenis)
        {
            //string strSQL = "SELECT A.ID,A.TPP_ID, A.Tindakan, A.Pelaku, A.Jadwal_Selesai, isnull(A.Aktual_Selesai,'17530101')Aktual_Selesai, A.Verifikasi,isnull(A.tglVerifikasi,getdate())tglVerifikasi " +
            //    ", A.Target FROM TPP_Tindakan A inner join TPP C on A.TPP_ID=C.ID " +
            //    "where A.rowstatus>-1 and C.Laporan_No='" + No + "' and A.jenis='" + jenis + "'";

            string strSQL =
            "SELECT A.ID,A.TPP_ID, A.Tindakan, A.Pelaku, A.Jadwal_Selesai, isnull(A.Aktual_Selesai,'17530101')Aktual_Selesai, " +
            "A.Verifikasi,isnull(A.tglVerifikasi,getdate())tglVerifikasi " +
            ", A.Target,isnull(C.ApvMgr_Users,'')ApvMgr_Users,isnull(C.Dept_ID_From,'')Dept_ID_From " +
            "FROM TPP_Tindakan A " +
            "inner join TPP C on A.TPP_ID=C.ID " +
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
                arrTPP.Add(new TPP_Tindakan());
            return arrTPP;
        }
        public int getTarget(string laporan_no, string jenis, string tindakan)
        {
            int intresult = 0;
            string strSQL = "select count(tindakan)targetke from TPP_Tindakan where rowstatus>-1 and Tindakan='"+tindakan+
                "' and jenis='"+jenis+"' and TPP_ID in (select ID from TPP where Laporan_No='"+laporan_no+"')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    intresult = Convert.ToInt32(sqlDataReader["targetke"]);
                }
            }
            return intresult;
        }
        public TPP_Tindakan GenerateObject(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP_Tindakan();
            objTPP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTPP.TPP_ID = Convert.ToInt32(sqlDataReader["TPP_ID"]);
            objTPP.Tindakan = sqlDataReader["Tindakan"].ToString();
            objTPP.Pelaku = sqlDataReader["Pelaku"].ToString();
            objTPP.Jadwal_Selesai = Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"]);
            objTPP.Aktual_Selesai = Convert.ToDateTime(sqlDataReader["Aktual_Selesai"]);
            objTPP.Tglverifikasi = Convert.ToDateTime(sqlDataReader["Tglverifikasi"]);
            objTPP.Verifikasi = Convert.ToInt32(sqlDataReader["Verifikasi"]);
            objTPP.Target = sqlDataReader["Target"].ToString();
            /** Beny **/
            objTPP.ApvMgr_Users = sqlDataReader["ApvMgr_Users"].ToString();
            objTPP.Dept_ID_From = Convert.ToInt32(sqlDataReader["Dept_ID_From"]);
            return objTPP;
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
