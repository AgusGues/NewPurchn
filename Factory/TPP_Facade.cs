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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factory
{
    public class TPP_Facade : BusinessFacade.AbstractTransactionFacadeF
    {
        private TPP objTPP = new TPP();
        private ArrayList arrTPP;
        private List<SqlParameter> sqlListParam;

        public TPP_Facade(object objDomain)
            : base(objDomain)
        {
            objTPP = (TPP)objDomain;
        }
        public TPP_Facade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Old_Laporan_No", objTPP.Old_Laporan_No));
                sqlListParam.Add(new SqlParameter("@Laporan_No", objTPP.Laporan_No));
                sqlListParam.Add(new SqlParameter("@TPP_Date", objTPP.TPP_Date));
                sqlListParam.Add(new SqlParameter("@PIC", objTPP.PIC));
                sqlListParam.Add(new SqlParameter("@Ketidaksesuaian", objTPP.Ketidaksesuaian));
                sqlListParam.Add(new SqlParameter("@Uraian", objTPP.Uraian));
                sqlListParam.Add(new SqlParameter("@Status", objTPP.Status));
                sqlListParam.Add(new SqlParameter("@Dept_ID", objTPP.Dept_ID));
                sqlListParam.Add(new SqlParameter("@Asal_M_ID", objTPP.Asal_M_ID));
                sqlListParam.Add(new SqlParameter("@Asal_M_ID1", objTPP.Asal_M_ID1));
                sqlListParam.Add(new SqlParameter("@User_ID", objTPP.User_ID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objTPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@BagianID", objTPP.BagianID));
                int intResult = transManager.DoTransaction(sqlListParam, "TPP_Insert1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        //public int Update1(TransactionManager transManager)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objTPP.ID));
        //        sqlListParam.Add(new SqlParameter("@Apv", objTPP.Apv));

        //        int intResult = transManager.DoTransaction(sqlListParam, "[TPP_UpdateNotApv]");
        //        strError = transManager.Error;
        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}
        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = "select A.Dept_ID,A.CreatedTime,A.ID,isnull(C.Departemen,' ') Departemen,A.Laporan_No,A.TPP_Date," +
                "A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv,case when (apv=0 and apvHSE=1) then 'HSE' when Apv=5 then 'Open' when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                "when apv=1 then 'Head/Kasie' when apv=2 then 'Manager' when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan," +
                "isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                "isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date,isnull(A.Due_Date,'1/1/1900')Due_Datex, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID)verified, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID)notverified, " +
                "isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 " +
                "order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(bagianid,0) bagianid, " +
                "ISNULL((select ceklis from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1),0) Ceklis,(select Rekomendasi from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1) Rekomendasi " +
                "from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID  left join TPP_Dept C on A.Dept_ID=C.ID  " +
                " where A.RowStatus>-1 order by TPP_Date Desc,Departemen ";
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
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public ArrayList RetrieveByKriteria(string kriteria)
        {
            string strSQL = "select A.Dept_ID,A.CreatedTime,A.ID,isnull(C.Departemen,'')Departemen,A.Laporan_No,A.TPP_Date," +
                "A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv,case when (apv=0 and apvHSE=1) then 'HSE' when Apv=5 then 'Open' when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                "when apv=1 then 'Head/Kasie'  when apv=2 then 'Manager'  when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan," +
                "isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                "isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date,isnull(A.Due_Date,'1/1/1900')Due_Datex, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID)verified, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID)notverified, " +
                "isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(bagianid,0) bagianid, " +
                "ISNULL((select ceklis from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1),0) Ceklis,(select Rekomendasi from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1) Rekomendasi " +
                "from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID " +
                "left join TPP_Dept C on A.Dept_ID=C.ID where A.rowstatus>-1 " + kriteria + " order by TPP_Date,Departemen ";
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
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public TPP RetrieveByNo(string No)
        {
            string strSQL = "select A.Dept_ID,A.CreatedTime,A.ID,isnull(C.Departemen,'')Departemen,A.Laporan_No,A.TPP_Date," +
                "A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv,case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                "when apv=1 then 'Head/Kasie'  when apv=2 then 'Manager'  when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan," +
                "isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                "isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date,isnull(A.Due_Date,'1/1/1900')Due_Datex, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID and RowStatus>-1)verified, " +
                "(select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID and RowStatus>-1)notverified, " +
                "isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(bagianid,0) bagianid, " +
                "ISNULL((select ceklis from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1),0) Ceklis,(select Rekomendasi from TPP_RekomHSE where id=ISNULL(RekomID,0) and Rowstatus>-1) Rekomendasi " +
                "from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID " +
                "left join TPP_Dept C on A.Dept_ID=C.ID where Laporan_No = '" + No + "' order by TPP_Date,Departemen ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new TPP();
        }
        public ArrayList GetYearTPP()
        {
            string strSQL = "select distinct year(tpp_date) tahun from tpp order by year(tpp_date) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    objTPP = new TPP();
                    objTPP.Tahun = (sqlDataReader["Tahun"].ToString());
                    arrTPP.Add(objTPP);
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public int GetLastUrutan(int tahun)
        {
            int urutan = 0;
            string strSQL = "select top 1 urutan from (select CAST(SUBSTRING(Laporan_no,1,3) as int) urutan from tpp where YEAR(tpp_date)=" + tahun + ")A order by urutan desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    urutan = Convert.ToInt32(sqlDataReader["urutan"]);
                }
            }
            return urutan;
        }
        public string getbagian(int id){
            string bagian=" - ";
            string strSQL="select top 1 bagian from tpp_bagian where id=" + id ;
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    bagian=sqlDataReader["bagian"].ToString();
                }
            }
            return bagian;
        }

        public string GetKodeMasalah(string masalah)
        {
            string strmasalah = string.Empty;
            string strSQL = "select kode from tpp_asal_masalah where asal_masalah='" + masalah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    strmasalah = sqlDataReader["kode"].ToString();
                }
            }
            return strmasalah;
        }
        public string UpdateCloseTPP(string laporan, string tgl, string userby, int value)
        {
            string strSQL = "update tpp set lastmodifiedtime=getdate(),Closeby='" + userby + "',lastmodifiedby='" + userby + "', closed=" + value + ",CLose_Date='" + tgl +
                "' where laporan_no='" + laporan + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateSolveTPP(string laporan, string tglsolve, string tgldue, string userby, int value)
        {
            string strSQL = "update tpp set lastmodifiedtime=getdate(),lastmodifiedby='" + userby + "', Solved=" + value +
                ",Solve_Date='" + tglsolve + "',Due_Date='" + tgldue + "' where laporan_no='" + laporan + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        //public string UpdateNotApprove(string ID)
        //{
        //    string strSQL = "update tpp set Apv = 0 where ID='" + ID + "'";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    return strError;
        //}
        public string GetUserType(string userID)
        {
            string strSQL = "select keterangan from tpp_users where user_id=" + userID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            string usertype = string.Empty;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    usertype = sqlDataReader["keterangan"].ToString();
                }
            }
            return usertype;
        }

        public TPP GenerateObject1(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            objTPP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTPP.Dept_ID = Convert.ToInt32(sqlDataReader["Dept_ID"]);
            objTPP.Asal_M_ID = Convert.ToInt32(sqlDataReader["Asal_M_ID"]);
            objTPP.Asal_M_ID1 = Convert.ToInt32(sqlDataReader["Asal_M_ID1"]);
            objTPP.Departemen = sqlDataReader["Departemen"].ToString();
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTPP.Due_date = Convert.ToDateTime(sqlDataReader["Due_date"]);
            //objTPP.Due_Datex = Convert.ToDateTime(sqlDataReader["Due_datex"]);
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.Ketidaksesuaian = (sqlDataReader["Ketidaksesuaian"]).ToString();
            objTPP.Uraian = (sqlDataReader["Uraian"]).ToString();
            objTPP.KetStatus = (sqlDataReader["KetStatus"]).ToString();
            objTPP.Asal_Masalah = (sqlDataReader["Asal_Masalah"]).ToString();
            objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objTPP.Approval = sqlDataReader["Approval"].ToString();
            objTPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objTPP.Closed = Convert.ToInt32(sqlDataReader["closed"]);
            objTPP.Close_Date = Convert.ToDateTime(sqlDataReader["Close_Date"]);
            objTPP.Solved = Convert.ToInt32(sqlDataReader["Solved"]);
            objTPP.Solve_Date = Convert.ToDateTime(sqlDataReader["Solve_Date"]);
            objTPP.Solve_Date = Convert.ToDateTime(sqlDataReader["Solve_Date"]);
            objTPP.Verified = Convert.ToInt32(sqlDataReader["Verified"]);
            objTPP.Notverified = Convert.ToInt32(sqlDataReader["Notverified"]);
            return objTPP;
        }

        public TPP GenerateObject(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            objTPP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTPP.Dept_ID = Convert.ToInt32(sqlDataReader["Dept_ID"]);
            objTPP.Asal_M_ID = Convert.ToInt32(sqlDataReader["Asal_M_ID"]);
            objTPP.Asal_M_ID1 = Convert.ToInt32(sqlDataReader["Asal_M_ID1"]);
            objTPP.Departemen = sqlDataReader["Departemen"].ToString();
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTPP.Due_date = Convert.ToDateTime(sqlDataReader["Due_date"]);
            objTPP.Due_Datex = Convert.ToDateTime(sqlDataReader["Due_datex"]);
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.Ketidaksesuaian = (sqlDataReader["Ketidaksesuaian"]).ToString();
            objTPP.Uraian = (sqlDataReader["Uraian"]).ToString();
            objTPP.KetStatus = (sqlDataReader["KetStatus"]).ToString();
            objTPP.Asal_Masalah = (sqlDataReader["Asal_Masalah"]).ToString();
            objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objTPP.Approval = sqlDataReader["Approval"].ToString();
            objTPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objTPP.Closed = Convert.ToInt32(sqlDataReader["closed"]);
            objTPP.Close_Date = Convert.ToDateTime(sqlDataReader["Close_Date"]);
            objTPP.Solved = Convert.ToInt32(sqlDataReader["Solved"]);
            objTPP.Solve_Date = Convert.ToDateTime(sqlDataReader["Solve_Date"]);
            objTPP.Solve_Date = Convert.ToDateTime(sqlDataReader["Solve_Date"]);
            objTPP.Verified = Convert.ToInt32(sqlDataReader["Verified"]);
            objTPP.Notverified = Convert.ToInt32(sqlDataReader["Notverified"]);
            objTPP.BagianID = Convert.ToInt32(sqlDataReader["BagianID"]);
            objTPP.Rekomendasi = sqlDataReader["Rekomendasi"].ToString();
            objTPP.Ceklis = Convert.ToInt32(sqlDataReader["Ceklis"]);
            return objTPP;
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTPP.ID));
                sqlListParam.Add(new SqlParameter("@Apv", objTPP.Apv));

                int intResult = transManager.DoTransaction(sqlListParam, "[TPP_UpdateApv]");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public override int UpdateNot(DataAccessLayer.TransactionManager transManager)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objTPP.ID));

        //        int intResult = transManager.DoTransaction(sqlListParam, "[TPP_UpdateNotApv]");
        //        strError = transManager.Error;
        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            //throw new NotImplementedException();
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Ceklis", objTPP.Ceklis));
                sqlListParam.Add(new SqlParameter("@Rekomendasi", objTPP.rekomendasi));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "TPP_Insert_RekomHSE");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTPP.ID));
                sqlListParam.Add(new SqlParameter("@Apv", objTPP.Apv));
                sqlListParam.Add(new SqlParameter("@RekomID", objTPP.RekomID));

                int intResult = transManager.DoTransaction(sqlListParam, "TPP_Update_HSE");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
            //throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveOpenTPPHeader(string UserInput, string Apv)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            arrTPP = new ArrayList();
            int UserID = users.ID;

            arrTPP = RetrieveForOpenTPP(UserID, UserInput, Apv);
            return arrTPP;
        }
        public ArrayList RetrieveForOpenTPP(int UserID, string UserInput, string Apv)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ID ,Laporan_No, TPP_Date, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open' when apv=1 then 'Head/Kasie' " +
                           "when apv=2 then 'Manager' when Status=1 then 'Close' end Approval,Uraian,PIC,isnull(bagianid,0) bagianid from TPP where Apv = " + Apv + "-1 and ID not in " +
                           "(select TPP_ID from TPP_Approval where RowStatus >-1) and RowStatus > -1 and Dept_ID in " +
                           "(select Dept_ID from TPP_Users where [User_ID]='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ")";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObjectHeaderTPP(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public TPP RetrieveTPPupdate(string ID)
        {
            string strSQL = "select uraian,tpp_date,pic,laporan_no,asal_m_id from TPP where ID=" + ID + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectTes(sqlDataReader);
                }
            }

            return new TPP();
        }
        public TPP RetrieveTPPNum(string NoTPP, string UserInput)
        {
            string strSQL = "select ID ,Laporan_No, TPP_Date,'' DeptFrom,case when (apv=0 and apvHSE=1) then 'HSE' when Apv=5 then 'Open' when Apv=0 then 'Open' when apv is null then 'Open' when apv=1 then 'Head/Kasie' " +
                           "when apv=2 then 'Manager' when Status=1 then 'Close' end Approval,Uraian,PIC from TPP where ID not in " +
                           "(select TPP_ID from TPP_Approval where RowStatus >-1) and RowStatus > -1 and Laporan_No='" + NoTPP + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderTPP(sqlDataReader);
                }
            }

            return new TPP();
        }
        public ArrayList RetrieveTPP(string UserHead, string UserInput1)
        {
            //try
            //{
            string strsql = "select ID as idTPP,Laporan_No,'' DeptFrom, " +
            "TPP_Date,Uraian,case when Apv=0 then 'Open' when Apv=1 then 'Approve Head' when Apv=2 then 'Approve Mgr' when (Apv=3) " +
            "then 'Plant Mgr' end 'Approval' from TPP where RowStatus > -1 and Dept_ID in (select Dept_ID from TPP_Users where RowStatus > -1 " +
            "and User_ID=" + UserHead + ") and Apv=(select top 1 Approval  from TPP_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrTPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateGridTPP(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            //}
            //catch { }
            return arrTPP;
        }

        //add hasan april 2022
        
        private ArrayList arrSPPDetail;
        public ArrayList RetrieveTPPPopup(int UserID)
        {
            string total = "0";
            int apv = 0;
            string query = "select approval from tpp_users where user_id=" + UserID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(query);
            arrSPPDetail = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    apv = Convert.ToInt32(sqlDataReader["approval"].ToString());
                }
            }
            if (apv == 5)
            {

                string strQuery = "select distinct * from tpp where Apv=5 order by id desc ";
                DataAccess dataAccess2 = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader2 = dataAccess2.RetrieveDataByString(strQuery);
                arrSPPDetail = new ArrayList();
                if (dataAccess2.Error == string.Empty && sqlDataReader2.HasRows)
                {
                    while (sqlDataReader2.Read())
                    {
                        arrSPPDetail.Add(new NewSPPDetail
                        {
                            Laporan_No = sqlDataReader2["Laporan_No"].ToString()
                        });
                    }
                }

            }
            else
            {

            }
            return arrSPPDetail;
        }
        //end add hasan

        public string GetStatusApv(string UserID)
        {
            string strsql = "select Apv from TPP where RowStatus > -1 and Dept_ID in (select Dept_ID from TPP_Users where RowStatus > -1 " +
            "and User_ID=" + UserID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Apv"].ToString();
                }
            }

            return string.Empty;
        }
        public string GetApv(string UserID)
        {
            string strsql = "select Approval as Apv from TPP_Users where User_ID=" + UserID + " and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Apv"].ToString();
                }
            }

            return string.Empty;
        }
        public string GetUserI(string UserID)
        {
            string strsql = "select User_ID from TPP where Dept_ID in (select Dept_ID from TPP_Users where User_ID=" + UserID + ") and apv=0 and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["User_ID"].ToString();
                }
            }

            return string.Empty;
        }
        public string GetUserIGrid(string UserID)
        {
            string strsql = "select User_ID from TPP where Dept_ID in (select Dept_ID from TPP_Users where User_ID=" + UserID + ") and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["User_ID"].ToString();
                }
            }

            return string.Empty;
        }
        public ArrayList RetrieveListTPP(string UserID, string UserInput)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ID as idTPP,ROW_NUMBER()OVER(ORDER BY ID ASC)No,Laporan_No,LEFT(convert(char,TPP_Date,110),10) TPP_Date,(select A.Departemen " +
                           "from TPP_Dept as A,Dept as B where A.deptID=B.ID and A.DeptID in (select DeptID from Users where ID='" + UserInput + "')) " +
                           "DeptFrom,LEFT(convert(char,TPP_Date,110),10) TPP_Date,Uraian,case when apv=0 then 'Open' when apv=1 then 'Approved Head' " +
                           "when apv=2 then 'Approved Mgr' else 'Close' end Approval from TPP where ID not in " +
                           "(select TPP_ID from TPP_Approval where RowStatus >-1) and RowStatus > -1 and Dept_ID in " +
                           "(select Dept_ID from TPP_Users where [User_ID]='" + UserID + "' and RowStatus>-1)";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObjectListTPP(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());

            return arrTPP;
        }
        public TPP GenerateObjectHeaderTPP(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            string test = sqlDataReader["TPP_Date"].ToString();
            objTPP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.Uraian = sqlDataReader["Uraian"].ToString();
            objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            //objTPP.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            //objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objTPP.Approval = sqlDataReader["Approval"].ToString();
            return objTPP;
        }

        public TPP GenerateGridTPP(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            objTPP.ID = Convert.ToInt32(sqlDataReader["IDtpp"]);
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.Uraian = sqlDataReader["Uraian"].ToString();
            //objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            objTPP.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            //objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objTPP.Approval = sqlDataReader["Approval"].ToString();
            return objTPP;
        }

        public TPP GenerateObjectTes(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            objTPP.Uraian = sqlDataReader["Uraian"].ToString();
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.Asal_M_ID = Convert.ToInt32(sqlDataReader["Asal_M_ID"]);
            return objTPP;
        }
        public TPP GenerateObjectListTPP(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP();
            objTPP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTPP.Laporan_No = sqlDataReader["Laporan_No"].ToString();
            objTPP.Uraian = sqlDataReader["Uraian"].ToString();
            //objTPP.PIC = sqlDataReader["PIC"].ToString();
            objTPP.TPP_Date = Convert.ToDateTime(sqlDataReader["TPP_Date"]);
            objTPP.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            objTPP.Approval = sqlDataReader["Approval"].ToString();
            objTPP.No = Convert.ToInt32(sqlDataReader["No"]);

            return objTPP;
        }

        //baru created by Razib
        public ArrayList RetrieveRecapTPPMonthAll(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(Due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and Dept_ID=" + Dept_ID;
            string strSQL = " with TPPx as ( " +
                                           " select A.Dept_ID,A.CreatedTime,A.ID,Upper(isnull(C.Departemen,' ')) Departemen,A.Laporan_No,A.TPP_Date,A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv," +
                                           " case when Apv=0 then 'Admin' " +
                                           " when apv is null then 'Admin' " +
                                           " when apv=1 then 'Head/Kasie' " +
                                           " when apv=2 then 'Manager' " +
                                           " when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan, " +
                                           " isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                                           " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
                                           " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID)verified, " +
                                           " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID)notverified, " +
                                           " isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
                                           " from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID  left join TPP_Dept C on A.Dept_ID=C.ID where A.RowStatus>-1 " +
                                " ) select KetStatus,Verified,NotVerified,Solve_Date,Close_Date,Keterangan,Approval,PIC,CreatedTime,ID,Dept_ID,Asal_M_ID1,Asal_M_ID, Departemen,TPP_Date,Laporan_No,Ketidaksesuaian,Uraian,Asal_Masalah,Closed,Apv,due_date,Solved from TPPx where Year(Due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    arrTPP.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }

        //baru created by Razib
        public ArrayList RetrieveRecapTPPMonthOpen(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and Dept_ID=" + Dept_ID;
            string strSQL = " with TPPx as ( " +
                                           " select A.Dept_ID,A.CreatedTime,A.ID,Upper(isnull(C.Departemen,' ')) Departemen,A.Laporan_No,A.TPP_Date,A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv," +
                                           " case when Apv=0 then 'Admin' " +
                                           " when apv is null then 'Admin' " +
                                           " when apv=1 then 'Head/Kasie' " +
                                           " when apv=2 then 'Manager' " +
                                           " when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan, " +
                                           " isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                                           " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
                                           " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID)verified, " +
                                           " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID)notverified, " +
                                           " isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
                                           " from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID  left join TPP_Dept C on A.Dept_ID=C.ID where A.RowStatus>-1 " +
                                " ) select KetStatus,Verified,NotVerified,Solve_Date,Close_Date,Keterangan,Approval,PIC,CreatedTime,ID,Dept_ID,Asal_M_ID1,Asal_M_ID, Departemen,TPP_Date,Laporan_No,Ketidaksesuaian,Uraian,Asal_Masalah,Closed,Apv,due_date,Solved from TPPx where Closed=0 and Year(Due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    arrTPP.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public ArrayList RetrieveRecapTPPMonthClosed(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and Dept_ID=" + Dept_ID;
            string strSQL = " with TPPx as ( " +
                                          " select A.Dept_ID,A.CreatedTime,A.ID,Upper(isnull(C.Departemen,' ')) Departemen,A.Laporan_No,A.TPP_Date,A.PIC,A.Ketidaksesuaian,A.Uraian,A.KetStatus,B.Asal_Masalah,A.Apv," +
                                          " case when Apv=0 then 'Admin' " +
                                          " when apv is null then 'Admin' " +
                                          " when apv=1 then 'Head/Kasie' " +
                                          " when apv=2 then 'Manager' " +
                                          " when apv=3 then 'PM / Corp. Mgr' end Approval,A.Keterangan, " +
                                          " isnull(A.Asal_M_ID,0)Asal_M_ID,isnull(A.Asal_M_ID1,0)Asal_M_ID1, isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                                          " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
                                          " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=1 and TPP_ID=A.ID)verified, " +
                                          " (select COUNT(ID)verified from TPP_Tindakan where Verifikasi=0 and TPP_ID=A.ID)notverified, " +
                                          " isnull((select top 1 Jadwal_Selesai from TPP_Tindakan where TPP_ID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
                                          " from TPP A inner join TPP_Asal_Masalah B on A.Asal_M_ID=B.ID  left join TPP_Dept C on A.Dept_ID=C.ID where A.RowStatus>-1 " +
                               " ) select KetStatus,Verified,NotVerified,Solve_Date,Close_Date,Keterangan,Approval,PIC,CreatedTime,ID,Dept_ID,Asal_M_ID1,Asal_M_ID, Departemen,TPP_Date,Laporan_No,Ketidaksesuaian,Uraian,Asal_Masalah,Closed,Apv,due_date,Solved from TPPx where Closed=1 and Year(due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    arrTPP.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }

        public ArrayList RetrieveForOpenTPPHSE(int UserID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ID ,Laporan_No, TPP_Date, '' DeptFrom, " +
                            "case when (apv=0 and apvHSE=1) then 'HSE' when Apv =5 then 'Open' when Apv=0 then 'Open' when apv is null then 'Open' when apv=1 then 'Head/Kasie' " +
                            "when apv=2 then 'Manager' when Status=1 then 'Close' end Approval,Uraian,PIC,isnull(bagianid,0) bagianid from TPP where Apv in(select Approval from tpp_users where user_id=" + UserID + " and Keterangan='hse') " +
                            "and ID not in (select TPP_ID from TPP_Approval where RowStatus >-1) and RowStatus > -1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObjectHeaderTPP(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public ArrayList RetrieveTPPHSE(int UserID)
        {
            //try
            //{
            string strsql = "select ID as idTPP,Laporan_No,'' DeptFrom, " +
            "TPP_Date,Uraian,case when (apv=0 and apvHSE=1) then 'HSE' when Apv=5 then 'Open' when Apv=0 then 'Open' when Apv=1 then 'Approve Head' when Apv=2 then 'Approve Mgr' when (Apv=3) " +
            "then 'Plant Mgr' end 'Approval' from TPP where RowStatus > -1 and Asal_M_ID in (select ID from TPP_Asal_Masalah where RowStatus > -1 and Asal_Masalah='Kecelakaan kerja')" +
            "and Apv=(select top 1 Approval  from TPP_Users where RowStatus > -1 and User_ID=" + UserID + ")";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrTPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateGridTPP(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            //}
            //catch { }
            return arrTPP;
        }
    }
}
