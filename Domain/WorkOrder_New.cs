using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class WorkOrder_New : GRCBaseDomain
    {
        private string informasi = string.Empty;
        private string Dept_pemberi = string.Empty;
        private string Dept_penerima = string.Empty;
        private string waktu2 = string.Empty;
        private string selisih = string.Empty;
        private string apvmgr = string.Empty;
        private string aliasMtn = string.Empty;
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
        private string finishdate2 = string.Empty;
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
        private string namadept = string.Empty;
        private string alias = string.Empty;
        private string fDeptName = string.Empty;
        private string tDeptName = string.Empty;
        private string flag = string.Empty;
        private string plantname = string.Empty;
        private string subarea = string.Empty;
        private string headname = string.Empty;
        private string alasannotop = string.Empty;
        private string permintaan = string.Empty;
        private string tipewo = string.Empty;
        private string accountemail = string.Empty;
        private string subarea2 = string.Empty;
        private string deptname2 = string.Empty;
        private string statusreport = string.Empty;
        private string corporate = string.Empty;
        private string tglselesaiwo = string.Empty;
        private string namasubdept = string.Empty;

        public Byte[] FileLampiranOP { get; set; }
        public int Target { get; set; }
        public string TargetT1 { get; set; }
        public string TargetT2 { get; set; }
        public string TargetT3 { get; set; }
        public int Aktif { get; set; } public string Ket { get; set; }
        public string Target01 { get; set; } public string Target02 { get; set; } public string Target03 { get; set; }
        public int Ttl_ada { get; set; }
        //public int DeptID_Users { get; set; }
        public int DeptID_PenerimaWO { get; set; }
        public string UpdatePelaksanaTime { get; set; }


        private int flag1 = 0;
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
        private int IdDept = 0;
        private int Deptid_P = 0;
        private int verifikasiISO = 0;
        private int verifikasiSec = 0;
        private int todept = 0;
        private int plantid = 0;
        private int deptIDHead = 0;
        private int subareaid = 0;
        private int headid = 0;
        private int Deptid = 0;
        private int apvop = 0;
        private int unitkerjaid = 0;

        private int totalwo = 0;
        private int womasuk = 0;
        private int wokeluar = 0;
        private int wofinish = 0;
        private int woupdate = 0;
        private int veriso = 0;

        //private DateTime createdtime = DateTime.Now.Date;
        //private DateTime finishdate = DateTime.Now.Date;
        //private DateTime duedate = DateTime.Now.Date;
        //private DateTime tgldibuat = DateTime.Now.Date;
        //private DateTime tgldisetujui = DateTime.Now.Date;

        public string Informasi { get { return informasi; } set { informasi = value; } }
        public string Dept_Pemberi { get { return Dept_pemberi; } set { Dept_pemberi = value; } }
        public string Dept_Penerima { get { return Dept_penerima; } set { Dept_penerima = value; } }
        public string Waktu2 { get { return waktu2; } set { waktu2 = value; } }
       
        public string Selisih { get { return selisih; } set { selisih = value; } }
        public string ApvMgr { get { return apvmgr; } set { apvmgr = value; } }
        public string NamaSubDept { get { return namasubdept; } set { namasubdept = value; } }
        public string TglSelesaiWO { get { return tglselesaiwo; } set { tglselesaiwo = value; } }
        public string Corporate { get { return corporate; } set { corporate = value; } }
        public string StatusReport { get { return statusreport; } set { statusreport = value; } }
        public string DeptName2 { get { return deptname2; } set { deptname2 = value; } }
        public string SubArea2 { get { return subarea2; } set { subarea2 = value; } }
        public string AccountEmail { get { return accountemail; } set { accountemail = value; } }
        public string TipeWO { get { return tipewo; } set { tipewo = value; } }
        public string Permintaan { get { return permintaan; } set { permintaan = value; } }
        public string AliasMtn { get { return aliasMtn; } set { aliasMtn = value; } }
        public string AlasanNotApvOP { get { return alasannotop; } set { alasannotop = value; } }
        public string HeadName { get { return headname; } set { headname = value; } }
        public string SubArea { get { return subarea; } set { subarea = value; } }
        public string PlantName { get { return plantname; } set { plantname = value; } }
        public string Flag { get { return flag; } set { flag = value; } }
        public string FromDeptName { get { return fDeptName; } set { fDeptName = value; } }
        public string ToDeptName { get { return tDeptName; } set { tDeptName = value; } }
        public string Alias { get { return alias; } set { alias = value; } }
        public string NamaDept { get { return namadept; } set { namadept = value; } }
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
        public string FinishDate2 { get { return finishdate2; } set { finishdate2 = value; } }
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

       
        public int VerISO { get { return veriso; } set { veriso = value; } }
        public int WOUpdate { get { return woupdate; } set { woupdate = value; } }
        public int WOFinish { get { return wofinish; } set { wofinish = value; } }
        public int WOMasuk { get { return womasuk; } set { womasuk = value; } }
        public int WOKeluar { get { return wokeluar; } set { wokeluar = value; } }
        public int TotalWO { get { return totalwo; } set { totalwo = value; } }
        public int Flag1 { get { return flag1; } set { flag1 = value; } }
        public int DeptID { get { return Deptid; } set { Deptid = value; } }
        public int HeadID { get { return headid; } set { headid = value; } }
        public int SubAreaID { get { return subareaid; } set { subareaid = value; } }
        public int DeptIDHead { get { return deptIDHead; } set { deptIDHead = value; } }
        public int PlantID { get { return plantid; } set { plantid = value; } }
        public int ToDept { get { return todept; } set { todept = value; } }
        public int VerifikasiISO { get { return verifikasiISO; } set { verifikasiISO = value; } }
        public int VerifikasiSec { get { return verifikasiSec; } set { verifikasiSec = value; } }
        public int DeptIDP { get { return Deptid_P; } set { Deptid_P = value; } }
        public int IDDept { get { return IdDept; } set { IdDept = value; } }
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
        public int ApvOP { get { return apvop; } set { apvop = value; } }
        public int UnitKerjaID { get { return unitkerjaid; } set { unitkerjaid = value; } }

    }
}
