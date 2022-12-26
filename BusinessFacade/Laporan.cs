using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Domain;
namespace BusinessFacade
{
    public class Laporan
    {
        private ArrayList arrData = new ArrayList();
        private Lap ts = new Lap();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string SolveQuery { get; set; }
        public string Smt { get; set; }
        private decimal TotalBobot = 0;
        public string TglMulai { get; set; }
        public string TglSelesai { get; set; }
        public string DeptPilihan { get; set; }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "Solve":
                    query = "select (select COUNT(ID) from ISO_Task where (RowStatus>-1 AND RowStatus!=9) and Status>-1 and PIC=isot.PIC " + this.Criteria + ")Num, *," +
                            "(SELECT SUM(NilaiBobot)FROM ISO_Task where (RowStatus >-1 AND RowStatus!=9) and Status=2 and PIC=isot.PIC " + this.Criteria + ")TotalBobot " +
                            ",(select dbo.GetPointNilai(isot.ID))Point,(select top 1 isd.TglTargetSelesai from ISO_TaskDetail isd where TaskID=isot.ID and RowStatus>-1)Targete  " +
                            "from ISO_Task isot where isot.Status>-1 " + this.Criteria ;
                    break;
                case "PIC":
                    string querys = "SELECT ua.*,iu.UserName IsoName ,ib.BagianName " +
                            "from UserAccount ua LEFT JOIN ISO_Users as iu ON iu.ID=ua.UserID " +
                            "LEFT JOIN ISO_Bagian as ib ON ib.ID=ua.BagianID " +
                            "WHERE ua.RowStatus>-1" + this.Criteria;
                    query = "SELECT ISNULL(iu.ID,ua.ID)ID,ISNULL(iu.UserID,ua.ID)UserID,ISNULL(iu.UserName,UPPER(ua.UserName))UserName,iu.NIK, " +
                            "ua.DeptID,ua.DeptJabatanID BagianID,ua.UserName IsoName ,ib.BagianName  " +
                            "FROM ISO_Users ua " +
                            "LEFT JOIN ISO_Bagian AS ib ON ib.ID=ua.DeptJabatanID " +
                            "LEFT JOIN UserAccount AS iu ON iu.UserID=ua.ID and iu.BagianID=ua.DeptJabatanID " +
                            "WHERE ua.RowStatus>-1 AND ua.DeptJabatanID>0 " + this.Criteria;
                    query = "SELECT * FROM ( " +
                            "SELECT DISTINCT * FROM ( " +
                            "SELECT IU.ID UserID,ISNULL(IA.UserName,UPPER(IU.UserName))UserName,IB.BagianName,X.BagianID,IA.NIK," +
                            "ISNULL(IA.DeptID,IU.DeptID)DeptID,ISNULL(IB.Urutan,0)Urutan FROM (   " +
                            "SELECT BagianID,PIC,ISO_UserID from ISO_Task where  CONVERT(Char,TglMulai,112)  BETWEEN '" + this.TglMulai + "' and '" + this.TglSelesai + "'  " +
                            this.DeptPilihan.Replace("UA.", "") + "  GROUP BY BagianID,PIC,ISO_UserID   )as X    " +
                            
                            "LEFT JOIN ISO_Users as IU ON IU.DeptJabatanID=X.BagianID     " +
                            
                            //"LEFT JOIN ISO_Users as IU ON IU.UserName=X.PIC     " +
                            
                            "LEFT JOIN UserAccount as IA ON IA.UserID=IU.ID and IA.BagianID=IU.DeptJabatanID " +
                            
                            //"LEFT JOIN ISO_Bagian AS IB ON IB.ID=X.BagianID where IU.rowstatus>-1 " +
                            
                            "LEFT JOIN ISO_Bagian AS IB ON IB.ID=X.BagianID where IU.rowstatus>-1 and IA.RowStatus >-1 " +
                            
                            "UNION ALL " +
                            "SELECT UserID,UserName,IB.BagianName,BagianID,NIK,UA.DeptID,ISNULL(IB.Urutan,0)Urutan  " +
                            "FROM UserAccount UA " +
                            "LEFT JOIN ISO_Bagian AS IB ON IB.ID=UA.BagianID WHERE UA.RowStatus>-1 " + this.DeptPilihan +
                            ") XX " +
                            ") XXX " + this.Criteria;

                    break;
                case "Dept":
                    query = "Select * " + pemberyTask + " from Dept where RowStatus>-1 " + this.Criteria;
                    break;
                case "Bobot":
                    query = BobotPES();
                    break;
            }
            return query;
        }
        private string pemberyTask = ",(Select Top 1 UserName from UserAccount where UserID in( " +
                                   " select ID from ISO_Users where UserID in( " +
                                   " select top 1 Userid from ISO_Dept where (UserGroupID=100 or UserGroupID=50 ) and DeptID=Dept.ID and RowStatus>-1 " +
                                   " and UserID!=0)))TaskBy ";
        private string BobotPES()
        {
            return "Select ISNULL((Bobot*100),0)Bobot from ISO_BobotPES where PesType=2 " +
                     " AND CAST(RTRIM(LTRIM(cast(activetahun as char(4))))+''+" +
                     "(RIGHT('0'+ RTRIM(LTRIM(CAST(activebulan as CHAR))),2)) as int)<=" + this.Smt + this.Criteria;
        }
        public ArrayList Retrieve()
        {
            string strSQL = this.Query();
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(this.generateObject(sdr));
                }
            }
            return arrData;
        }
        public Lap Retrieve(bool Detail)
        {
            ts = new Lap();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ts=(this.generateObject(sdr));
                }
            }
            return ts;
        }
        public Lap generateObject(SqlDataReader sdr)
        {
            ts = new Lap();
            try
            {

                switch (this.Pilihan)
                {
                    case "Solve":
                        decimal bbt = 0; decimal pp = 0; decimal tbb = 0;
                        bbt = Convert.ToDecimal(sdr["NilaiBobot"].ToString());
                        tbb = (sdr["TotalBobot"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["TotalBobot"].ToString());
                        pp = (sdr["Point"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Point"].ToString());
                        TotalBobot += (pp > 0) ? (bbt / tbb) * pp : TotalBobot;
                        ts.ID = Convert.ToInt32(sdr["ID"].ToString());
                        ts.TaskNo = sdr["TaskNo"].ToString();
                        ts.NewTask = sdr["TaskName"].ToString();
                        ts.TglMulai = Convert.ToDateTime(sdr["TglMulai"].ToString());
                        ts.App = Convert.ToInt32(sdr["Status"].ToString());
                        ts.BobotNilai = Convert.ToInt32(sdr["NilaiBobot"].ToString());
                        ts.TglSelesai = (sdr["TglSelesai"] == DBNull.Value) ? Convert.ToDateTime(sdr["Targete"].ToString()) : Convert.ToDateTime(sdr["TglSelesai"].ToString());
                        ts.TotalBBT = (sdr["TotalBobot"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["TotalBobot"].ToString());
                        ts.TaskID = Convert.ToInt32(sdr["Num"].ToString());
                        ts.PointNilai = TotalBobot;
                        ts.MailSent = Convert.ToInt32(sdr["MailSent"].ToString());
                        //ts.PemberiTask = sdr["TaskBy"].ToString();
                        break;
                    case "PIC":
                        //ts.IsoName = sdr["IsoName"].ToString();
                        ts.UserName = sdr["UserName"].ToString();
                        ts.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                        ts.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                        ts.BagianName = sdr["BagianName"].ToString();
                        //ts.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                        ts.UserID = (sdr["UserID"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["UserID"].ToString());
                        //ts.BobotPES = (sdr["BobotPES"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["BobotPES"].ToString());

                        break;
                    case "Dept":
                        ts.DeptID = Convert.ToInt32(sdr["ID"].ToString());
                        ts.DeptName = sdr["Alias"].ToString();
                        ts.PemberiTask = sdr["TaskBy"].ToString();
                        break;
                    case "Bobot":
                        ts.BobotPES = (sdr["Bobot"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Bobot"].ToString());
                        break;
                }
            }
            catch { }
            return ts;
        }

    }
}
