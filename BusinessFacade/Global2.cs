using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Net;
using DataAccessLayer;
using System.Reflection;

using System.Web.Script.Serialization;


namespace BusinessFacade
{
    public class Global2
    {
        
        private string GetConnection(string strCon)
        {
            //string strCon1 = Global.ConnectionString();
            string strCon1 = string.Empty;
            string GRCBOARDPUSAT = "Initial Catalog=GRCBOARD;Data Source=sql1.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDLAPAK = "Initial Catalog=AgenLapak;Data Source=sql1.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDKRWG = "Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDCTRP = "Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDJMB = "Initial Catalog=bpasJombang;Data Source=sqlJombang.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDPURCH = Global.ConnectionString();
            if (strCon.Trim().ToUpper() == "GRCBOARDPUSAT") strCon1 = GRCBOARDPUSAT;
            if (strCon.Trim().ToUpper() == "GRCBOARDLAPAK") strCon1 = GRCBOARDLAPAK;
            if (strCon.Trim().ToUpper() == "GRCBOARDKRWG") strCon1 = GRCBOARDKRWG;
            if (strCon.Trim().ToUpper() == "GRCBOARDCTRP") strCon1 = GRCBOARDCTRP;
            if (strCon.Trim().ToUpper() == "GRCBOARDJMB") strCon1 = GRCBOARDJMB;
            if (strCon.Trim().ToUpper() == "GRCBOARDPURCH") strCon1 = GRCBOARDPURCH;
            return strCon1;
        }
        private string TaskMonitorQuery()
        {
            string strSQL = "WITH ListTaskExpire AS ( " +
                            "SELECT *,DATEADD(DAY,1,TglTargetSelesai)TglClose FROM ( " +
                            "SELECT ID,TglMulai,TaskName,x.DeptID,BagianID,PIC,Status,Max(TaskDetailID)TaskDetailID, " +
                            "Max(TargetKe)TargetKe,Max(TglTargetSelesai) TglTargetSelesai,x.UserID,ISNULL(HeadMail,'iso_krwg@grcboard.com')HeadMail " +
                            ",(select usrmail from users where users.ID=x.UserID)userMail,s.HeadID " +
                            " FROM ( " +
                            "SELECT t.ID,t.TglMulai,t.TaskName,t.DeptID,t.BagianID,t.PIC,t.Status,td.TargetKe,td.TglTargetSelesai, " +
                            "t.UserID,td.ID TaskDetailID " +
                            "FROM ISO_Task t " +
                            "LEFT JOIN ISO_TaskDetail td ON td.TaskID=t.ID " +
                            "LEFT JOIN ISO_Users iu ON iu.UserName=t.PIC " +

                            "WHERE t.Status between 0 AND 1 AND t.RowStatus>-1 AND iu.RowStatus>-1 " +
                            "AND YEAR(td.TglTargetSelesai)>year(getdate())-2 " +
                            ") as x " +
                            "LEFT JOIN vw_ISO_Usrmail s ON s.UserID=x.UserID AND s.DeptID=x.DeptID " +
                            "GROUP By ID,ID,TglMulai,TaskName,x.DeptID,BagianID,PIC,Status,x.UserID,HeadMail,s.HeadID " +
                            ") as xx " +
                            "WHERE DATEADD(DAY,1,(TglTargetSelesai)) <= GETDATE() " +
                            ") ";
            return strSQL;
        }
        private string ConvertDataTableTojSonString(DataTable dataTable)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return serializer.Serialize(tableRows);
        }
        public DataSet GetDataTable(string Tabel, string Field, string Criteria, string strCon)
        {
            string strSQL = "Select " + Field + " from " + Tabel + " " + Criteria;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sConn = new SqlConnection(GetConnection(strCon));
                sConn.Open();
                da = new SqlDataAdapter(strSQL, sConn);
                da.Fill(ds, "GetDataTable");
                sConn.Close();
            }
            catch
            {

            }
            return (ds);
        }
        public DataSet GetDataFromTable(string Tabel, string Criteria, string strCon)
        {
            SqlConnection sConn = new SqlConnection(GetConnection(strCon));
            sConn.Open();
            string strSQL = "Select * from " + Tabel + " " + Criteria;
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "GetDataFromTable");
            sConn.Close();
            return (ds);
        }
        public string TaskMonitoringFilter(string Criteria, string ConnectioName)
        {
            string result = string.Empty;
            DataTable d = new DataTable();
            SqlConnection sConn = new SqlConnection(GetConnection(ConnectioName));
            sConn.Open();
            string strSQL = this.TaskMonitorQuery() +
                            "SELECT * FROM ListTaskExpire " + Criteria +
                            "Order by ID,TargetKe";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "TaskMonitoring");
            sConn.Close();
            d = ds.Tables[0];
            result = ConvertDataTableTojSonString(d);
            return result;
        }
        public DataSet GetAliasKendaraan(string NoPol, string strCon)
        {
            DataAccess dataAccess = new DataAccess(GetConnection(strCon));
            SqlConnection sConn = new SqlConnection(GetConnection(strCon));
            sConn.Open();
            string strSQL = "Select NamaKendaraan From MTC_NamaArmada where NoPol='" + NoPol + "'";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "AliasKendaraan");
            sConn.Close();
            return (ds);
        }
        public DataSet GetNoPolByPlant(string Plant)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL = (Plant.Length >= 1) ?
                "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo, Merk, Tahun, Tonase, DepresiasiPerHari, Type, TypeKendaraanID, " +
                "DepoID, DepoName, RowStatus, DefaultNamaSupirID, DefaultNamaKenekID, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime  " +
                " from Ex_MasterKendaraan where rowstatus> -1 and DepoID=" + Plant + " order by KendaraanNo" :
                "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo, Merk, Tahun, Tonase, DepresiasiPerHari, Type, TypeKendaraanID, " +
                "DepoID, DepoName, RowStatus, DefaultNamaSupirID, DefaultNamaKenekID, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime  " +
                "from Ex_MasterKendaraan where rowstatus> -1 and  DepoID in(" + Plant + ") order by KendaraanNo";

            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "GetNoPolByPlant");
            sConn.Close();
            return (ds);

        }
        public DataSet Retrieve_SJ_OP_ByDate(string Plant,string tgl1,string tgl2)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL = 
                "select A.ID,A.SuratJalanNo,A.OPID,C.OPNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,"+
                "A.CountPrint,A.Keterangan,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy, "+
                "A.LastModifiedTime,A.ActualShipmentDate,B.ScheduleDate,C.CreatedTime as OpCreatedTime,OPretur,CetakKwitansi,CountCetakKwitansi, "+
                "B.DepoID from SuratJalan as A,Schedule as B,OP as C where A.ScheduleID = B.ID and A.OPID = C.ID and B.DepoID = " + Plant +
                " and convert(char,A.createdtime,112)>='" + tgl1 + "' and convert(char,A.createdtime,112)<='" + tgl2 + "' order by A.ID desc";

            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "SJ_OP_ByDate");
            sConn.Close();
            return (ds);

        }
        public DataSet Retrieve_SJ_TO_ByDate(string Plant, string tgl1, string tgl2)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL =
                "select  A.ID,A.SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID,B.ScheduleNo,A.PoliceCarNo,A.DriverName,"+
                "A.Status,A.Cetak,A.ReceiveDate,A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime "+
                "from SuratJalanTO as A,Schedule as B,TransferOrder as C where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and (C.FromDepoID = " +
                Plant + " or C.ToDepoID = " + Plant + ")  and convert(char,A.createdtime,112)>='" + tgl1 + "' and convert(char,A.createdtime,112)<='" + 
                tgl2 + "' order by A.ID desc";
           ;

            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "SJ_TO_ByDate");
            sConn.Close();
            return (ds);

        }
        public DataSet DetailKendaraan(string NoPol)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL = "SELECT * FROM Ex_MasterKendaraan where KendaraanNo='" + NoPol + "'";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DetailKendaraan");
            sConn.Close();
            return (ds);
        }


        public DataSet Retrieve_SJ_ByID(string ID)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL = "SELECT d.ScheduleNo, a.SuratJalanNo, c.Description, b.Qty FROM suratjalan a, suratjalandetail b, items c, schedule d WHERE a.id = b.SuratJalanid AND b.itemid = c.id AND a.Scheduleid = d.id AND a.status > -1  AND d.ScheduleNo = '" + ID + "' ORDER BY suratjalanno ASC";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "SJ_ByID");
            sConn.Close();
            return (ds);

        }


        public DataSet Retrieve_ScheduleItems(string drtgl, string sdtgl, int depoid)
        {
            SqlConnection sConn = new SqlConnection(GetConnection("GRCBOARDPUSAT"));
            sConn.Open();
            string strSQL = "SELECT a.ScheduleNo, b.SuratJalanNo, a.Scheduledate, c.itemid, d.Description item, c.Qty FROM schedule a, suratjalan b, suratjalandetail c, items d WHERE a.id = b.Scheduleid AND b.id = c.suratjalanid AND c.itemid = d.id AND b.status > -1 AND a.rowstatus > -1 AND depoid = "+ depoid +" AND left(CONVERT(char, scheduledate,112),8) BETWEEN " + drtgl + "AND " + sdtgl + "";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sConn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "SJ_Items");
            sConn.Close();
            return (ds);

        }
    }
}
