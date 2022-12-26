using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class WorkOrder : GRCBaseDomain
    {
        private string deptname = string.Empty;
        private string uraianpekerjaan = string.Empty;
        private string nowo = string.Empty;
        private string createdby = string.Empty;
        private string pelaksana = string.Empty;
        private string status = string.Empty;
        private string filename = string.Empty;
        private string statusapv = string.Empty;
        private string username = string.Empty;
        private string tgldibuat = string.Empty;
        private string tgldisetujui = string.Empty;
        private string tgltarget = string.Empty;
        private string finishdate = string.Empty;
        private string createdtime = string.Empty;
        private string createdtime1 = string.Empty;
        private string duedate = string.Empty;
        private string periode = string.Empty;
        private string area = string.Empty;
        private string hitungmundur = string.Empty;
        private string Elapsedweek = string.Empty;
        private string Elapsedday = string.Empty;
        private string uraianperbaikan = string.Empty;
        private string waktunow = string.Empty;
        private string waktudateline = string.Empty;
        private string statuswo = string.Empty;
        private string sisahari = string.Empty;
        private string alasancancel = string.Empty;

        private int bulan = 0;
        private int tahun = 0;
        private int nomorurut = 0;
        private int deptid = 0;
        private int woid = 0;
        private int idlampiran = 0;
        private int rowstatus = 0;
        private int apv = 0;
        private int userid = 0;
        private int id = 0;
        private int areaid = 0;
        private int total = 0;
        private string keterangan = string.Empty;

        //private DateTime createdtime = DateTime.Now.Date;
        //private DateTime finishdate = DateTime.Now.Date;
        //private DateTime duedate = DateTime.Now.Date;
        //private DateTime tgldibuat = DateTime.Now.Date;
        //private DateTime tgldisetujui = DateTime.Now.Date;

        public string AlasanCancel { get { return alasancancel; } set { alasancancel = value; } }
        public string SisaHari { get { return sisahari; } set { sisahari = value; } }
        public string StatusWO { get { return statuswo; } set { statuswo = value; } }
        public string DeptName { get { return deptname; } set { deptname = value; }}
        public string UraianPekerjaan { get { return uraianpekerjaan; } set { uraianpekerjaan = value; } }
        public string NoWO { get { return nowo; } set { nowo = value; } }
        public string CreatedBy { get { return createdby; } set { createdby = value; } }
        public string Pelaksana { get { return pelaksana; } set { pelaksana = value; } }
        public string Status { get { return status; } set { status = value; } }
        public string FileName { get { return filename; } set { filename = value; } }
        public string StatusApv { get { return statusapv; } set { statusapv = value; } }
        public string UserName { get { return username; } set { username = value; } }
        public string FinishDate { get { return finishdate; } set { finishdate = value; } }
        public string TglDibuat { get { return tgldibuat; } set { tgldibuat = value; } }
        public string TglTarget { get { return tgltarget; } set { tgltarget = value; } }
        public string TglDisetujui { get { return tgldisetujui; } set { tgldisetujui = value; } }
        public string DueDateWO { get { return duedate; } set { duedate = value; } }
        public string CreatedTime { get { return createdtime; } set { createdtime = value; } }
        public string CreatedTime1 { get { return createdtime1; } set { createdtime1 = value; } }
        public string Periode { get { return periode; } set { periode = value; } }
        public string AreaWO { get { return area; } set { area = value; } }
        public string HitungMundur { get { return hitungmundur; } set { hitungmundur = value; } }
        public string ElapsedWeek { get { return Elapsedweek; } set { Elapsedweek = value; } }
        public string ElapsedDay { get { return Elapsedday; } set { Elapsedday = value; } }
        public string UraianPerbaikan { get { return uraianperbaikan; } set { uraianperbaikan = value; } }
        public string WaktuNow { get { return waktunow; } set { waktunow = value; } }
        public string WaktuDateLine { get { return waktudateline; } set { waktudateline = value; } }
        //public string TglDisetujui { get { return tgldisetujui; } set { tgldisetujui = value; } }

        //public DateTime CreatedTime { get { return createdtime; } set { createdtime = value; } }
        //public DateTime FinishDate { get { return finishdate; } set { finishdate = value; } }
        //public DateTime DueDateWO { get { return duedate; } set { duedate = value; } }
        //public DateTime TglDibuat { get { return tgldibuat; } set { tgldibuat = value; } }
        //public DateTime TglDisetujui { get { return tgldisetujui; } set { tgldisetujui = value; } }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }

            set
            {
                keterangan = value;
            }
        }

        public int Bulan { get { return bulan; } set { bulan = value; } }
        public int Tahun { get { return tahun; } set { tahun = value; } }
        public int NomorUrut { get { return nomorurut; } set { nomorurut = value; } }
        public int DeptID_Users { get { return deptid; } set { deptid = value; } }
        public int WOID { get { return woid; } set { woid = value; } }
        public int IDLampiran { get { return idlampiran; } set { idlampiran = value; } }
        public int RowStatus { get { return rowstatus; } set { rowstatus = value; } }
        public int Apv { get { return apv; } set { apv = value; } }
        public int UserID { get { return userid; } set { userid = value; } }
        public int ID { get { return id; } set { id = value; } }
        public int AreaID { get { return areaid; } set { areaid = value; } }
        public int Total { get { return total; } set { total = value; } }

    }
}
