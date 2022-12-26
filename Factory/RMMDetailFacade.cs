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
    public class RMMDetailFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM_Detail objRDetail= new RMM_Detail();
        private ArrayList arrRDetail;
        private List<SqlParameter> sqlListParam;

        public RMMDetailFacade(object objDomain)
            : base(objDomain)
        {
            objRDetail = (RMM_Detail)objDomain;
        }
        public RMMDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RMM_ID", objRDetail.RMM_ID));
                sqlListParam.Add(new SqlParameter("@Pelaku", objRDetail.Pelaku));
                sqlListParam.Add(new SqlParameter("@Jadwal_Selesai", objRDetail.Jadwal_Selesai));
                sqlListParam.Add(new SqlParameter("@RMM_SumberDayaID", objRDetail.RMM_SumberDayaID));
                sqlListParam.Add(new SqlParameter("@Aktivitas", objRDetail.Aktivitas));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objAkt.RowStatus));
                //sqlListParam.Add(new SqlParameter("@Aktual_Selesai", objAkt.Aktual_Selesai));
                sqlListParam.Add(new SqlParameter("@RMM_LocID", objRDetail.RMM_LocID));
                sqlListParam.Add(new SqlParameter("@Target", objRDetail.Target));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRDetail.CreatedBy));
                sqlListParam.Add(new SqlParameter("@RMM_Dept", objRDetail.RMM_Dept));
                int intResult = transManager.DoTransaction(sqlListParam, "[RMM_Detail_Insert]");
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
            arrRDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new RMM_Detail());
            return arrRDetail;
        }

        public ArrayList RetrieveByNo(string No)
        {
            string strSQL = " SELECT A.ID,A.RMM_ID, A.Aktivitas, A.Pelaku, A.Jadwal_Selesai, isnull(A.Aktual_Selesai,'17530101')Aktual_Selesai, A.Verifikasi,isnull(A.tglVerifikasi,getdate())tglVerifikasi, " +
                            " A.RMM_SumberdayaID,A.Target,A.RMM_LocID " +
                            " FROM RMM_Detail A inner join RMM C on A.RMM_ID=C.ID " +
                            " where A.rowstatus>-1 and C.RMM_No='" + No + "'";
            DataAccess dataAccess = new DataAccess (Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new RMM_Detail());
            return arrRDetail;
        }

        public string UpdateRMMDetailverf(string ID, string tgl, string verf)
        {
            string strSQL = "update RMM_Detail set verifikasi=" + verf + ",tglverifikasi='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateSolveRMM(string rmmid, string tglsolve, string ket, int value)
        {
            string strsql = "update rmm_detail set solved=" + value + 
                ",Solved_Date='" + tglsolve + "',Ket='" + ket + "' where id='" + rmmid + "'";
                
             //string strSQL = "update tpp set lastmodifiedtime=getdate(),lastmodifiedby='" + userby + "', Solved=" + value +
            //    ",Solve_Date='" + tglsolve + "',Due_Date='" + tgldue + "' where laporan_no='" + laporan + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateAktualSelesai(string ID, string tgl0, string tgl)
        {
            string strSQL = "update RMM_Detail set jadwal_Selesai='" + tgl0 + "',Aktual_Selesai='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }


        public string DeleteRMMDetail(string ID)
        {
            string strSQL = "Delete RMM_Detail where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public int getTarget(string rmm_no,string aktivitas)
        {
            int intresult = 0;
            string strSQL = "select count(aktivitas)targetke from RMM_Detail where rowstatus>-1 and Aktivitas='" + aktivitas +
                "'and RMM_ID in (select ID from RMM where RMM_No='" + rmm_no + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    intresult = Convert.ToInt32(sqlDataReader["targetke"]);
                }
            }
            return intresult;
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


        public RMM_Detail GenerateObject(SqlDataReader sqlDataReader)
        {
            objRDetail = new RMM_Detail();
            objRDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRDetail.RMM_ID = Convert.ToInt32(sqlDataReader["RMM_ID"]);
            objRDetail.Aktivitas = sqlDataReader["Aktivitas"].ToString();
            objRDetail.Pelaku = sqlDataReader["Pelaku"].ToString();
            objRDetail.Jadwal_Selesai = Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"]);
            objRDetail.Aktual_Selesai = Convert.ToDateTime(sqlDataReader["Aktual_Selesai"]);
            objRDetail.TglVerifikasi = Convert.ToDateTime(sqlDataReader["TglVerifikasi"]);
            objRDetail.Verifikasi = Convert.ToInt32(sqlDataReader["Verifikasi"]);
            objRDetail.RMM_LocID = Convert.ToInt32(sqlDataReader["RMM_LocID"]);
            objRDetail.RMM_SumberDayaID = Convert.ToInt32(sqlDataReader["RMM_SumberDayaID"]);
            objRDetail.Target = sqlDataReader["Target"].ToString();
            //objAkt.Target = sqlDataReader["Target"].ToString();
            return objRDetail;
        }

    }
}
