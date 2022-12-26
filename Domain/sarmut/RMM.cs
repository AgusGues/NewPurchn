using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM : GRCBaseDomain
    {
        private string old_Rmm_No = string.Empty;
        private string rmm_No = string.Empty;
        private DateTime tgl_RMM = DateTime.Now;
        private int rmm_DeptID = 0;
        private string rmm_DeptName = string.Empty;
        private int rmm_Dimensi = 0;
        private string rmm_DimensiName = string.Empty;
        private int rmm_Perusahaan = 0;
        private string sPerusahaan = string.Empty;
        private int rmm_Dept = 0;
        private string sDept = string.Empty;
        private int status = 0;
        private int user_ID = 0;
        private int apv = 0;
        private string aktivitas = string.Empty;
        private string pelaku = string.Empty;
        private int rmm_SumberDaya = 0;
        private string sumberDaya = string.Empty;
        private DateTime jadwal_selesai = DateTime.Now;
        private DateTime aktual_Selesai = DateTime.Now;
        private DateTime tglVerifikasi = DateTime.Now;
        private int solved = 0;
        private DateTime solved_Date = DateTime.Now;
        private int verified = 0;
        private int notverified = 0;
        private string approval = string.Empty;
        private string deptFrom = string.Empty;
        private int verifikasi = 0;
       // private int rMM_LocID = 0;
        private string lokasi = string.Empty;
        private int bulan = 0;
        private string bulan1 = string.Empty;
        private string smt = string.Empty;
        private string departemen = string.Empty;
        private string minggu = string.Empty;
        private string year = string.Empty;
        private int idx = 0;
        private string ket = string.Empty;

        public int DeptID { get; set; }
        public string Ket { get { return ket; } set { ket = value; } }
        public int IDx { get { return idx; } set { idx = value; } }
        public string Year { get { return year; } set { year = value; } }
        public string Bulan1 { get { return bulan1; } set { bulan1 = value; } }
        public string Minggu { get { return minggu; } set { minggu = value; } }
        public string Departemen { get { return departemen; } set { departemen = value; } }
        public string SMT { get { return smt; } set { smt = value; } }
        public int Bulan { get { return bulan; } set { bulan = value; } }
        public DateTime Week { get; set; }
        public int Tahun { get; set; }
        public string Lokasi { get { return lokasi; } set { lokasi = value; } }
        //public int RMM_LocID { get { return rMM_LocID; } set { rMM_LocID = value; } }
        public int Verifikasi { get { return verifikasi; } set { verifikasi = value; } }
        public string DeptFrom { get { return deptFrom; } set { deptFrom = value; } }
        public string Old_RMM_No { get { return old_Rmm_No; } set { old_Rmm_No = value; } }
        public string SumberDaya { get { return sumberDaya; } set { sumberDaya = value; } }
        public string SDept { get { return sDept; } set { sDept = value; } }
        public string DeptName { get { return rmm_DeptName; } set { rmm_DeptName = value; } }
        public string RMM_No { get { return rmm_No; } set { rmm_No = value; } }
        public DateTime Tgl_RMM { get { return tgl_RMM; } set { tgl_RMM = value; } }
        public int RMM_DeptID { get { return rmm_DeptID; } set { rmm_DeptID = value; } }
        public int RMM_Dimensi { get { return rmm_Dimensi; } set { rmm_Dimensi = value; } }
        public string DimensiName { get { return rmm_DimensiName; } set { rmm_DimensiName = value; } }
        public int RMM_Perusahaan { get { return rmm_Perusahaan; } set { rmm_Perusahaan = value; } }
        public string SMTPerusahaan { get { return sPerusahaan; } set { sPerusahaan = value; } }
        public int RMM_Dept { get { return rmm_Dept; } set { rmm_Dept = value; } }
        public int Status { get { return status; } set { status = value; } }
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
        public int Apv { get { return apv; } set { apv = value; } }
        public string Aktivitas { get { return aktivitas; } set { aktivitas = value; } }
        public string Pelaku { get { return pelaku; } set { pelaku = value; } }
        public int RMM_SumberDaya { get { return rmm_SumberDaya; } set { rmm_SumberDaya = value; } }
        public DateTime Jadwal_Selesai { get { return jadwal_selesai; } set { jadwal_selesai = value; } }
        public DateTime Aktual_Selesai { get { return aktual_Selesai; } set { aktual_Selesai = value; } }
        public DateTime TglVerifikasi { get { return tglVerifikasi; } set { tglVerifikasi = value; } }
        public int Solved { get { return solved; } set { solved = value; } }
        public DateTime Solved_Date { get { return solved_Date; } set { solved_Date = value; } }
        public int Verified { get { return verified; } set { verified = value; } }
        public int NotVerified { get { return notverified; } set { notverified = value; } }
        public string Approval { get { return approval; } set { approval = value; } }

    }
}
