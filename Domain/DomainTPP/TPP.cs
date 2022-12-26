using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain
{
    public class TPP : GRCBaseDomain
    {
        private string old_Laporan_No = string.Empty;
        private string laporan_No = string.Empty;
        private DateTime tPP_Date = DateTime.Now;
        private DateTime createdTime = DateTime.Now;
        private string pIC = string.Empty;
        private string ketidaksesuaian = string.Empty;
        private string uraian = string.Empty;
        private string ketstatus = string.Empty;
        private string asal_Masalah = string.Empty;
        private string departemen = string.Empty;
        private int dept_ID = 0;
        private int asal_M_ID = 0;
        private int asal_M_ID1 = 0;
        private int user_ID = 0;
        private string keterangan = string.Empty;
        private int closed = 0;
        private DateTime close_Date = DateTime.Now;
        private int solved = 0;
        private DateTime solve_Date = DateTime.Now;
        private DateTime due_Date = DateTime.Now;
        private DateTime due_Datex = DateTime.Now;
        private int verified = 0;
        private int notverified = 0;
        private DateTime due_date = DateTime.Now;

        private string deptFrom = string.Empty;
        private int status = 0;
        private int apv = 0;
        private int no = 0;
        private string approval = string.Empty;
        private string bulan = string.Empty;
        private string tahun = string.Empty;
        private string noUrut = string.Empty;

        public string Old_Laporan_No { get { return old_Laporan_No; } set { old_Laporan_No = value; } }
        public string Laporan_No { get { return laporan_No; } set { laporan_No = value; } }
        public DateTime CreatedTime { get { return createdTime; } set { createdTime = value; } }
        public DateTime Due_date { get { return due_date; } set { due_date = value; } }
        public DateTime TPP_Date { get { return tPP_Date; } set { tPP_Date = value; } }
        public string PIC { get { return pIC; } set { pIC = value; } }
        public string Ketidaksesuaian { get { return ketidaksesuaian; } set { ketidaksesuaian = value; } }
        public string Uraian { get { return uraian; } set { uraian = value; } }
        public string KetStatus { get { return ketstatus; } set { ketstatus = value; } }
        public string Asal_Masalah { get { return asal_Masalah; } set { asal_Masalah = value; } }
        public string Departemen { get { return departemen; } set { departemen = value; } }
        public int Status { get { return status; } set { status = value; } }
        public int Dept_ID { get { return dept_ID; } set { dept_ID = value; } }
        public int Asal_M_ID { get { return asal_M_ID; } set { asal_M_ID = value; } }
        public int Asal_M_ID1 { get { return asal_M_ID1; } set { asal_M_ID1 = value; } }
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
        public string Approval { get { return approval; } set { approval = value; } }
        public string Keterangan { get { return keterangan; } set { keterangan = value; } }
        public int Closed { get { return closed; } set { closed = value; } }
        public DateTime Close_Date { get { return close_Date; } set { close_Date = value; } }
        public int Solved { get { return solved; } set { solved = value; } }
        public DateTime Solve_Date { get { return solve_Date; } set { solve_Date = value; } }
        public DateTime Due_Date { get { return due_Date; } set { due_Date = value; } }
        public DateTime Due_Datex { get { return due_Datex; } set { due_Datex = value; } }
        public int Verified { get { return verified; } set { verified = value; } }
        public int Notverified { get { return notverified; } set { notverified = value; } }

        public int Apv { get { return no; } set { no = value; } }
        public int No { get { return apv; } set { apv = value; } }
        public string Bulan { get { return bulan; } set { bulan = value; } }
        public string Tahun { get { return tahun; } set { tahun = value; } }
        public string NoUrut { get { return noUrut; } set { noUrut = value; } }
        public string DeptFrom { get { return deptFrom; } set { deptFrom = value; } }
        public int BagianID { get; set; }

        public int Ceklis { get; set; }
        public int RekomID { get; set; }
        public string rekomendasi = string.Empty;
        public string Rekomendasi { get { return rekomendasi; } set { rekomendasi = value; } }

    }
}
