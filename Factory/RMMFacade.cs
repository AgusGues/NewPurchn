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
    public class RMMFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM objRMM = new RMM();
        private ArrayList arrRMM;
        private List<SqlParameter> sqlListParam;
        private ArrayList arrData = new ArrayList();
        public RMMFacade(object objDomain)
            : base(objDomain)
        {
            objRMM = (RMM)objDomain;
        }

        public RMMFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Old_RMM_No", objRMM.Old_RMM_No));
                sqlListParam.Add(new SqlParameter("@RMM_No", objRMM.RMM_No));
                sqlListParam.Add(new SqlParameter("@Tgl_RMM", objRMM.Tgl_RMM));
                sqlListParam.Add(new SqlParameter("@RMM_DeptID", objRMM.RMM_DeptID));
                sqlListParam.Add(new SqlParameter("@RMM_Dimensi", objRMM.RMM_Dimensi));
                sqlListParam.Add(new SqlParameter("@RMM_Perusahaan", objRMM.RMM_Perusahaan));
                //sqlListParam.Add(new SqlParameter("@RMM_Dept", objRMM.RMM_Dept));
                //sqlListParam.Add(new SqlParameter("@RMM_LocID", objRMM.RMM_LocID));
                sqlListParam.Add(new SqlParameter("@Status", objRMM.Status));
                sqlListParam.Add(new SqlParameter("@User_ID", objRMM.User_ID));
                sqlListParam.Add(new SqlParameter("@Apv", objRMM.Apv));
                //sqlListParam.Add(new SqlParameter("@RMM_Aktivitas", objRMM.RMM_Aktivitas));
                //sqlListParam.Add(new SqlParameter("@RMM_Pelaku", objRMM.RMM_Pelaku));
                //sqlListParam.Add(new SqlParameter("@RMM_Sumberdaya", objRMM.RMM_SumberDaya));
                //sqlListParam.Add(new SqlParameter("@RMM_Jadwal", objRMM.RMM_Jadwal));
                //sqlListParam.Add(new SqlParameter("@RMM_Aktual", objRMM.RMM_Aktual));
                //sqlListParam.Add(new SqlParameter("@RMM_TglVerfikasi", objRMM.TglVerifikasi));
                //sqlListParam.Add(new SqlParameter("@Solved", objRMM.Solved));
                //sqlListParam.Add(new SqlParameter("@Solved_Date", objRMM.Solved_Date));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRMM.CreatedBy));

                int IntResult = transManager.DoTransaction(sqlListParam,"RMM_Insert12");
                strError = transManager.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,C.RowStatus,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai," +
                            "  isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                            " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
					        " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
					        " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
					        " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
					        " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
					        " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 and C.RowStatus>-1 order by A.ID Desc,DeptName ";
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    arrRMM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());
            return arrRMM;
        }

        public RMM RetrieveByNo(string No)
        {
            //string strSQL = " select A.Tgl_RMM, A.RMM_DeptID,A.CreatedTime,A.ID,ISNULL(C.Departemen,'') DeptName,A.RMM_No,A.RMM_Jadwal,ISNULL(E.SarMutPerusahaan,'')SMTPerusahaan,ISNULL(E.Dimensi,'')DimensiName," +
            //              " ISNULL(Z.SarMutDepartment,'')SDept,ISNULL(A.Status,0)Status,ISNULL(D.Penyebab,'') SumberDaya,A.RMM_Jadwal,A.RMM_Aktual,A.RMM_Aktivitas,A.RMM_Pelaku,A.User_ID," +
            //              " A.Apv,case	when Apv=2 then 'Open' when Apv is null then 'MGr Dept' when Apv=3 then 'PM/Corp Mgr' end Approval,ISNULL(A.Verifikasi,0)Verifikasi, " +
            //              " isnull(A.Solved,0)Solved,isnull(A.Solved_Date,'1/1/1900')Solve_Date ," +
            //              " (select COUNT(ID)verified from RMM where Verifikasi=1 )verified, " +
            //              " (select COUNT(ID)verified from RMM where Verifikasi=0)notverified ,A.RMM_TglVerifikasi " +
            //              " from RMM A left join SarMut_Dept C on A.RMM_DeptID=C.ID inner join SarMut_Penyebab D on D.ID=A.RMM_SumberDaya inner join SarMut_Perusahaan E on E.ID=A.RMM_Perusahaan " +
            //              " inner join SarMut_Departemen Z on Z.ID=A.RMM_Dept and A.RowStatus>-1 and  A.RMM_No = '" + No + "' " + 
            //              " order by A.ID Desc,Departemen ";
            string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai," +
                            "  isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan," +
                            " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
                            " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
                            "(select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku," +
                            "(select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status," +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
                            " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 and  A.RMM_No = '" + No + "' " + 
                            " order by A.ID Desc,DeptName ";

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
            return new RMM();
        }

        public ArrayList GetYearTPP()
        {
            string strSQL = "select distinct year(Tgl_RMM) year from rmm order by year(Tgl_RMM) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    objRMM = new RMM();
                    objRMM.Year = (sqlDataReader["Year"].ToString());
                    arrRMM.Add(objRMM);
                }
            }
            else
                arrRMM.Add(new RMM());
            return arrRMM;
        }

        public ArrayList RetrieveByKriteria(string kriteria)
        {
            //string strSQL = " select A.Tgl_RMM, A.RMM_DeptID,A.CreatedTime,A.ID,ISNULL(C.Departemen,'') DeptName,A.RMM_No,A.RMM_Jadwal,ISNULL(E.SarMutPerusahaan,'')SMTPerusahaan,ISNULL(E.Dimensi,'')DimensiName," +
            //                " ISNULL(Z.SarMutDepartment,'')SDept,ISNULL(A.Status,0)Status,ISNULL(D.Penyebab,'') SumberDaya,A.RMM_Jadwal,A.RMM_Aktual,A.RMM_Aktivitas,A.RMM_Pelaku,A.User_ID," +
            //                " A.Apv,case	when Apv=2 then 'Open' when Apv is null then 'MGr Dept' when Apv=3 then 'PM/Corp Mgr' end Approval,ISNULL(A.Verifikasi,0)Verifikasi, " +
            //                " isnull(A.Solved,0)Solved,isnull(A.Solved_Date,'1/1/1900')Solve_Date ," +
            //                " (select COUNT(ID)verified from RMM where Verifikasi=1 )verified, " +
            //                " (select COUNT(ID)verified from RMM where Verifikasi=0)notverified ,A.RMM_TglVerifikasi " +						 
            //                " from RMM A left join SarMut_Dept C on A.RMM_DeptID=C.ID inner join SarMut_Penyebab D on D.ID=A.RMM_SumberDaya inner join SarMut_Perusahaan E on E.ID=A.RMM_Perusahaan " +
            //                " inner join SarMut_Departemen Z on Z.ID=A.RMM_Dept and A.RowStatus>-1 " + kriteria + " " +
            //                " order by A.ID Desc,Departemen ";
            string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,C.RowStatus,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai," +
                            "  isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                            " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
                            "  case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            "  when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
                            " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                            " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
                            " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 and C.RowStatus>-1 " + kriteria + " " +
                            " order by A.ID Desc,DeptName ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    arrRMM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());
            return arrRMM;
        }

        public ArrayList RetrieveRecapRMM(string Dept)
        {
            arrData = new ArrayList();
            string where = (Dept == "0") ? "" : " where RMM_DeptID=" + Dept;
            string strSql = " select s.RMM_DeptID,d.Departemen from RMM s left join RMM_Dept d on d.ID=s.RMM_DeptID " + where + " Group by s.RMM_DeptID,d.Departemen ";
                           //where + " order by d.Departemen"; 
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenObject(sdr));
                }
            }
            return arrData;
        }
        private RMM GenObject(SqlDataReader sdr)
        {
            RMM b = new RMM();
            b.RMM_DeptID = int.Parse(sdr["RMM_DeptID"].ToString());
            b.Departemen = sdr["Departemen"].ToString();
            //b.DeptName = sdr["DeptName"].ToString();
            //b.DeptCode = sdr["DeptCode"].ToString();
            return b;
        }

        public ArrayList RetrieveRecapRMM(string Smt, string Tahun, string RMM_DeptID)
        {
            #region 
            //query lama
            
            //string where = (DeptID == "0") ? "" : " where A.RMM_DeptID=" + DeptID;
            //string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,(select Departemen from SarMut_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,convert(char,C.Jadwal_Selesai,103)Jadwal_Selesai,isnull(C.Aktual_Selesai,'1/1/1900')Aktual_Selesai," +
            //                " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
            //                " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
            //                " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
            //                " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
            //                " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
            //                "(select SarMutPerusahaan from SarMut_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
            //                "(select Dimensi from SarMut_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
            //                "(select SarMutDepartment from SarMut_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " + 
            //                "(select Penyebab from  SarMut_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
            //                " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'PM/Corp Mgr' when Apv=2 then 'ISO-2' when Apv=3 then 'ISO-1' end Approval, " +
            //                " isnull(C.TglVerifikasi,'1/1/1900')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(A.Solved,0)Solved,isnull(A.Solved_Date,'1/1/1900')Solve_Date " +
            //                " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1  where A.RMM_DeptID= '" + DeptID + "' and Year(C.Jadwal_Selesai)='" + Tahun + "' order by A.ID Desc,DeptName";
            #endregion
            string kriteria = string.Empty;
            if (Smt !="0")
                kriteria = kriteria + " and SMT=" + Smt;
            if (RMM_DeptID !="0")
                kriteria = kriteria + " and RMM_DeptID=" + RMM_DeptID;
            string strSQL = " with R As " +
                            " ( " +
                            " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,C.RowStatus,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai, " +
                            " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                            " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu], " +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year], " +
                            " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName, " +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then '1' else '2' end SMT, " +
                            " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                            " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
                            " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 " +
                            " ) " +
                            " select RowStatus, Tgl_RMM,RMM_DeptID,CreatedTime,ID,IDx,DeptName,[User_ID],RMM_No,Jadwal_Selesai,Aktual_Selesai,Lokasi,SmtPerusahaan,Minggu,Bulan1,Year,DimensiName,SMT, " +
                            " SDept,Aktivitas,Pelaku,SumberDaya,[Status],Apv,Approval,TglVerifikasi,Verifikasi,Solved,Solve_Date,Ket from R where Year(Jadwal_Selesai)='" + Tahun + "' and RowStatus>-1 " + kriteria ;
                            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL); 
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());

                return arrRMM;
            
        }

        public ArrayList RetrieveRecapRMMMonth(string bulan,string tahun, string RMM_DeptID)
        {
            #region
            //query lama

            //string where = (DeptID == "0") ? "" : " where A.RMM_DeptID=" + DeptID;
            //string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,(select Departemen from SarMut_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,convert(char,C.Jadwal_Selesai,103)Jadwal_Selesai,isnull(C.Aktual_Selesai,'1/1/1900')Aktual_Selesai," +
            //                " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
            //                " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
            //                " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
            //                " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
            //                " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
            //                "(select SarMutPerusahaan from SarMut_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
            //                "(select Dimensi from SarMut_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
            //                "(select SarMutDepartment from SarMut_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " + 
            //                "(select Penyebab from  SarMut_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
            //                " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'PM/Corp Mgr' when Apv=2 then 'ISO-2' when Apv=3 then 'ISO-1' end Approval, " +
            //                " isnull(C.TglVerifikasi,'1/1/1900')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(A.Solved,0)Solved,isnull(A.Solved_Date,'1/1/1900')Solve_Date " +
            //                " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1  where A.RMM_DeptID= '" + DeptID + "' and Year(C.Jadwal_Selesai)='" + Tahun + "' order by A.ID Desc,DeptName";
            #endregion
            string kriteria = string.Empty;
            //if (Smt != "0")
            //    kriteria = kriteria + " and SMT=" + Smt;
            if (RMM_DeptID != "0")
                kriteria = kriteria + " and RMM_DeptID=" + RMM_DeptID;
            string strSQL = " with R As " +
                            " ( " +
                            " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai, " +
                            " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                            " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu], " +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year], " +
                            " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName, " +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then '1' else '2' end SMT, " +
                            " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                            " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
                            " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 " +
                            " ) " +
                            " select Tgl_RMM,RMM_DeptID,CreatedTime,ID,IDx,DeptName,[User_ID],RMM_No,Jadwal_Selesai,Aktual_Selesai,Lokasi,SmtPerusahaan,Minggu,Bulan1,Year,DimensiName,SMT, " +
                            " SDept,Aktivitas,Pelaku,SumberDaya,[Status],Apv,Approval,TglVerifikasi,Verifikasi,Solved,Solve_Date,Ket from R where Month(Jadwal_Selesai)='" + bulan + "' and year(Jadwal_Selesai)='" + tahun + "' " + kriteria;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());

            return arrRMM;

        }


        public ArrayList RetrieveRecapRMMMonthx(string bulan, string tahun, string RMM_DeptID)
        {
            #region
            //query lama

            //string where = (DeptID == "0") ? "" : " where A.RMM_DeptID=" + DeptID;
            //string strSQL = " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,(select Departemen from SarMut_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,convert(char,C.Jadwal_Selesai,103)Jadwal_Selesai,isnull(C.Aktual_Selesai,'1/1/1900')Aktual_Selesai," +
            //                " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
            //                " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
            //                " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu]," +
            //                " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year],	" +
            //                " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
            //                "(select SarMutPerusahaan from SarMut_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
            //                "(select Dimensi from SarMut_Perusahaan where ID=A.RMM_Dimensi) as DimensiName," +
            //                "(select SarMutDepartment from SarMut_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " + 
            //                "(select Penyebab from  SarMut_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
            //                " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'PM/Corp Mgr' when Apv=2 then 'ISO-2' when Apv=3 then 'ISO-1' end Approval, " +
            //                " isnull(C.TglVerifikasi,'1/1/1900')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(A.Solved,0)Solved,isnull(A.Solved_Date,'1/1/1900')Solve_Date " +
            //                " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1  where A.RMM_DeptID= '" + DeptID + "' and Year(C.Jadwal_Selesai)='" + Tahun + "' order by A.ID Desc,DeptName";
            #endregion
            string kriteria = string.Empty;
            //if (Smt != "0")
            //    kriteria = kriteria + " and SMT=" + Smt;
            if (RMM_DeptID != "0")
                kriteria = kriteria + "where Month(Jadwal_Selesai)='" + bulan + "' and year(Jadwal_Selesai)='" + tahun + "' and RMM_DeptID=" + RMM_DeptID;
            string strSQL = " with R As " +
                            " ( " +
                            " select A.Tgl_RMM,A.RMM_DeptID,A.CreatedTime,A.ID,C.ID as IDx,(select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,A.User_ID,A.RMM_No,C.Jadwal_Selesai,isnull(C.Aktual_Selesai,' ')Aktual_Selesai, " +
                            " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                            " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                            " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 'M1' when DAY(C.Jadwal_Selesai) between 8 and 14 then 'M2' " +
                            " when DAY(C.Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end [Minggu], " +
                            " MONTH(C.Jadwal_Selesai)[Bulan1], YEAR(C.Jadwal_Selesai)[Year], " +
                            " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName, " +
                            " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then '1' else '2' end SMT, " +
                            " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                            " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                            " A.Apv, case when Apv=0 then 'Open' when Apv IS NULL then 'Open' when Apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' end Approval, " +
                            " isnull(C.TglVerifikasi,' ')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved,isnull(C.Solved_Date,' ')Solve_Date,C.Ket " +
                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 " +
                            " ) " +
                            " select Tgl_RMM,RMM_DeptID,CreatedTime,ID,IDx,DeptName,[User_ID],RMM_No,Jadwal_Selesai,Aktual_Selesai,Lokasi,SmtPerusahaan,Minggu,Bulan1,Year,DimensiName,SMT, " +
                            " SDept,Aktivitas,Pelaku,SumberDaya,[Status],Apv,Approval,TglVerifikasi,Verifikasi,Solved,Solve_Date,Ket from R " + kriteria;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());

            return arrRMM;

        }

        public string UpdateSolved(string ID, string tgl, string solved)
        {
            string strSQL = "update RMM set Solved=" + solved+ ",Solved_Date='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string updateSolved2(string Idx, string tgl,int value)
        {
            string strSQL = "update rmm_detail set solved=" + value +
                ",Solved_Date='" + tgl + "' where RMM_ID='" + Idx + "'" ;
            //string strSQL = "update RMM_detail set Solved=" + value + 
             //               ",Solve_Date='" + tgl + "' where ID='" + Idx + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string updateCncl(string Idx,int value)
        {
            string strSQL = "update rmm set RowStatus=" + value + " where ID='" + Idx + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRMM.ID));
                sqlListParam.Add(new SqlParameter("@DeptID", objRMM.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objRMM.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRMM.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "[RMM_UpdateApv]");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
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


        public string GetApv(string UserID)
        {
            string strsql = "select Approval as Apv from RMM_Users where User_ID=" + UserID + " and rowstatus>-1";
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

        public string GetUserType(string userID)
        {
            string strSQL = "select keterangan from RMM_Users where user_id=" + userID;
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

        public int GetLastUrutan(int tahun)
        {
            int urutan = 0;
            string strSQL = "select top 1 urutan from (select CAST(SUBSTRING(RMM_No,1,3) as int) urutan from RMM where YEAR(Tgl_RMM)=" + tahun + ")A order by urutan desc";
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

        public string UpdateRMM(string ID, string tgl, string verf)
        {
            string strSQL = "update RMM set Verifikasi=" + verf + ",RMM_TglVerifikasi='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public ArrayList LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select Distinct(Year(Tgl_RMM))Tahun From RMM WHERE Year(Tgl_RMM) IS NOT NULL Order By Year(Tgl_RMM)";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectThn(sdr));
                }
            }
            return arrData;
        }

        public string GetUserI(string UserID)
        {
            string strsql = "select User_ID from RMM where RMM_DeptID in (select Dept_ID from RMM_Users where User_ID=" + UserID + ") and apv=0 and RowStatus>-1";
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
            string strsql = "select User_ID from RMM where RMM_DeptID in (select Dept_ID from RMM_Users where User_ID=" + UserID + ") and RowStatus>-1";
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

        public ArrayList RetrieveOpenRMMHeader(string UserInput, string Apv)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];

            arrRMM = new ArrayList();
            int UserID = users.ID;

            arrRMM = RetrieveForOpenRMM(UserID, UserInput, Apv);
            return arrRMM;
        }

        public ArrayList RetrieveForOpenRMM(int UserID, string UserInput, string Apv)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = " select A.ID ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open' when apv=2 then 'Open' " +
            //                            " when apv=3 then 'Plant Mgr/Corp Mgr' when Status=1 then 'Close' end Approval , A.RMM_Aktivitas,case when RMM_SumberDaya=1 then 'Mesin' " +
            //                            " when RMM_SumberDaya=2 then 'Material' when RMM_SumberDaya=3 then 'Metode' when RMM_SumberDaya=4 then 'Lingkungan' when RMM_SumberDaya=5 then 'Manusia' end SumberDaya " +
            //                            " from RMM A   where Apv =" + Apv + "-1 and A.ID not in  (select TPP_ID from SarMut_Approval where RowStatus >-1) and A.RowStatus > -1 " +
            //                            " and A.RMM_DeptID in  (select Dept_ID from SarMut_Users where [User_ID]='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ")";

            string strSQL = "select A.ID ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open' " + 
                                         " when apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO'  when Status=1 then 'Close' end Approval , C.Aktivitas," +
                                         " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya " +
                                         " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID   where Apv =" + Apv + "-1 and A.ID not in  (select RMM_ID from RMM_Approval where RowStatus >-1) and A.RowStatus > -1 " +
                                         " and A.RMM_DeptID in  (select Dept_ID from RMM_Users where [User_ID]='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ")";		
           
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateObjectHeaderRMM(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());
            return arrRMM;
        }


        public RMM RetrieveRMMNum(string NoRMM, string UserInput)
        {
            //string strSQL = " select A.ID ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open' when apv=2 then 'Open' " +
            //                             " when apv=3 then 'Plant Mgr/Corp Mgr' when Status=1 then 'Close' end Approval , A.RMM_Aktivitas,case when RMM_SumberDaya=1 then 'Mesin' " +
            //                             " when RMM_SumberDaya=2 then 'Material' when RMM_SumberDaya=3 then 'Metode' when RMM_SumberDaya=4 then 'Lingkungan' when RMM_SumberDaya=5 then 'Manusia' end SumberDaya " +
            //                             " from RMM A   where Apv =3-1 and A.ID not in  (select TPP_ID from SarMut_Approval where RowStatus >-1) and A.RowStatus > -1 and RMM_No='" + NoRMM + " '";
            string strSQL = "   select A.ID ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when A.Apv=0 then 'Open' when apv is null then 'Open' " +
                                         "  when apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' when Status=1 then 'Close' end Approval ," +
                                         "  (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,C.Aktivitas " +
                                         "  from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.ID not in (select RMM_ID from RMM_Approval where RowStatus >-1) and A.RowStatus > -1  " +
                                         " and A.RMM_No='" + NoRMM + " '";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderRMM(sqlDataReader);
                }
            }

            return new RMM();
        }

        public ArrayList RetrieveRMM(string UserHead, string UserInput1)
        {
            string strsql = " select A.ID as idRMM ,A.RMM_No, A.Tgl_RMM,X.Departemen DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open'" +
                                        " when apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr' when Apv=3 then 'Corp ISO' when Status=1 then 'Close' end Approval , " +
                                        "  (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya, " +
                                        " C.Aktivitas " +
                                        " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID " +
                                        " inner join RMM_Dept X on A.RMM_DeptID=X.ID " +
                                        " where A.RowStatus > -1 and A.RMM_DeptID in (select Dept_ID from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
                                        " and Apv=(select top 1 Approval  from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";
            //string strSQL = " select A.ID ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open' when apv=2 then 'Open' " +
            //                            " when apv=3 then 'Plant Mgr/Corp Mgr' when Status=1 then 'Close' end Approval , A.RMM_Aktivitas,case when RMM_SumberDaya=1 then 'Mesin' " +
            //                            " when RMM_SumberDaya=2 then 'Material' when RMM_SumberDaya=3 then 'Metode' when RMM_SumberDaya=4 then 'Lingkungan' when RMM_SumberDaya=5 then 'Manusia' end SumberDaya " +
            //                            " from RMM A  where Apv =3-1 and A.ID not in  (select TPP_ID from SarMut_Approval where RowStatus >-1) and A.RowStatus > -1 and RMM_No='" + NoRMM + " '";
            ////string strsql = " select A.ID as idSMT,A.Sarmut_No,'' DeptFrom, " +
            ////                " A.Tgl_Sarmut,B.Uraian,case when Apv=1 then 'Open' when Apv=2 then 'Approve Manager' when Apv=3 then 'Approve Mgr' when (Apv=4)" +
            ////                " then 'Plant Mgr' end 'Approval' from SarMut A inner join SarMut_Penyebab_Detail B on A.ID=B.Sarmut_ID where A.RowStatus > -1 and B.RowStatus>-1 " +
            ////                " and A.Sarmut_DeptID in (select Dept_ID from SarMut_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
            ////                " and Apv=(select top 1 Approval  from SarMut_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrRMM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateGridRMM(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());
            //}
            //catch { }
            return arrRMM;
        }

        public ArrayList RetrieveDept(string Dept_ID, string UserHead )
        {
            string kriteria = string.Empty;
            if (Dept_ID != "0")
                kriteria = kriteria + "and A.RMM_DeptID=" + Dept_ID;
            string strSQL = " select A.ID as idRMM ,A.RMM_No, A.Tgl_RMM,X.Departemen DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open'" +
                                        " when apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' when Status=1 then 'Close' end Approval , " +
                                        "  (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya, " +
                                        " C.Aktivitas " +
                                        " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID " +
                                        " inner join RMM_Dept X on A.RMM_DeptID=X.ID " +
                                        " where A.RowStatus > -1 and A.RMM_DeptID in (select Dept_ID from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
                                        " and Apv=(select top 1 Approval  from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1 " + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            arrRMM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMM.Add(GenerateGridRMM(sqlDataReader));
                }
            }
            else
                arrRMM.Add(new RMM());
            return arrRMM;
        }

        public string GetStatusApv(string UserID)
        {
            string strsql = "select Apv from RMM where RowStatus > -1 and RMM_DeptID in (select Dept_ID from RMM_Users where RowStatus > -1 " +
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


        public RMM GenerateObjectHeaderRMM(SqlDataReader sqlDataReader)
        {
            objRMM = new RMM();
            string test = sqlDataReader["Tgl_RMM"].ToString();
            objRMM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRMM.RMM_No = sqlDataReader["RMM_No"].ToString();
            objRMM.Aktivitas = sqlDataReader["Aktivitas"].ToString();
            ////objTPP.PIC = sqlDataReader["PIC"].ToString();
            objRMM.Tgl_RMM = Convert.ToDateTime(sqlDataReader["Tgl_RMM"]);
            ////objTPP.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            ////objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objRMM.Approval = sqlDataReader["Approval"].ToString();
            return objRMM;
        }

        public RMM GenerateGridRMM(SqlDataReader sqlDataReader)
        {
            objRMM = new RMM();
            objRMM.ID = Convert.ToInt32(sqlDataReader["idRMM"]);
            objRMM.RMM_No = sqlDataReader["RMM_No"].ToString();
            objRMM.Aktivitas = sqlDataReader["Aktivitas"].ToString();
            ////objTPP.PIC = sqlDataReader["PIC"].ToString();
            objRMM.Tgl_RMM = Convert.ToDateTime(sqlDataReader["Tgl_RMM"]);
            objRMM.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            ////objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objRMM.Approval = sqlDataReader["Approval"].ToString();
            return objRMM;
        }

        public RMM GenerateObject(SqlDataReader sqlDataReader)
        {
            objRMM = new RMM();
            objRMM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRMM.IDx = Convert.ToInt32(sqlDataReader["IDx"]);
            objRMM.Tgl_RMM = Convert.ToDateTime(sqlDataReader["Tgl_RMM"]);
            objRMM.DeptName = sqlDataReader["DeptName"].ToString();
            objRMM.RMM_No = sqlDataReader["RMM_No"].ToString();
            objRMM.SMTPerusahaan = sqlDataReader["SMTPerusahaan"].ToString();
            objRMM.DimensiName = sqlDataReader["DimensiName"].ToString();
            objRMM.SDept = sqlDataReader["SDept"].ToString();
            objRMM.Aktivitas = sqlDataReader["Aktivitas"].ToString();
            objRMM.Pelaku = sqlDataReader["Pelaku"].ToString();
            objRMM.SumberDaya = sqlDataReader["SumberDaya"].ToString();
            objRMM.Jadwal_Selesai=Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"]);
            objRMM.Aktual_Selesai = Convert.ToDateTime(sqlDataReader["Aktual_Selesai"]);
            objRMM.Approval = sqlDataReader["Approval"].ToString();
            //objRMM.Verified = Convert.ToInt32(sqlDataReader["Verified"]);
            objRMM.Verifikasi = Convert.ToInt32(sqlDataReader["Verifikasi"]);
            objRMM.Lokasi = sqlDataReader["Lokasi"].ToString();
            objRMM.TglVerifikasi = Convert.ToDateTime(sqlDataReader["TglVerifikasi"]);
            //objRMM.RMM_DeptID = Convert.ToInt32(sqlDataReader["RMM_DeptID"]);
            //objRMM.RMM_Dimensi = Convert.ToInt32(sqlDataReader["RMM_Dimensi"]);
            //objRMM.RMM_Perusahaan = Convert.ToInt32(sqlDataReader["RMM_Perusahaan"]);
            //objRMM.RMM_Dept = Convert.ToInt32(sqlDataReader["RMM_Dept"]);
            //objRMM.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objRMM.User_ID = Convert.ToInt32(sqlDataReader["User_ID"]);
            //objRMM.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objRMM.SMT = sqlDataReader["SMT"].ToString();
            objRMM.Minggu = sqlDataReader["Minggu"].ToString();
            objRMM.Bulan1 = sqlDataReader["Bulan1"].ToString();
            objRMM.Year = sqlDataReader["Year"].ToString();
            objRMM.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objRMM.Ket = sqlDataReader["Ket"].ToString();
            objRMM.Solved = Convert.ToInt32(sqlDataReader["Solved"]);
            return objRMM;
        }

        private RMM GenerateObjectThn(SqlDataReader sdr)
        {
            objRMM = new RMM();
            objRMM.Tahun = int.Parse(sdr["Tahun"].ToString());
            return objRMM;
        }
    }
}
