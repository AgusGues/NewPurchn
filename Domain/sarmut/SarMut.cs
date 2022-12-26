using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SarMut : GRCBaseDomain
    {
        private string old_Sarmut_No = string.Empty;
        private string sarmut_No = string.Empty;
        private DateTime tgl_Sarmut = DateTime.Now;
        private int sarmut_DeptID = 0;
        private int sarmut_Perusahaan = 0;
        private int sarmut_Dept = 0;
        private int sarmut_PemantauanDept = 0;
        private int status = 0;
        private int bulan = 0;
        private int tahun = 0;
        private decimal hasil_Pencapaian;
        private int kesimpulan = 0;
        private int user_ID = 0;
        private int apv = 0;
        private int closed = 0;
        private DateTime close_Date = DateTime.Now;
        private string close_By = string.Empty;
        private int solved = 0;
        private DateTime solve_Date = DateTime.Now;
        private string approval = string.Empty;
        private int verified = 0;
        private int notverified = 0;
        private string departemen = string.Empty;
        private DateTime due_date = DateTime.Now;
        private string uraian = string.Empty;
        private string penyebab = string.Empty;
        private string deptFrom = string.Empty;

        public string DeptFrom { get { return deptFrom; } set { deptFrom = value; } }
        public string Penyebab { get { return penyebab; } set { penyebab = value; } }
        public string Uraian { get { return uraian; } set { uraian = value; } }
        public DateTime Due_Date { get { return due_date; } set { due_date = value; } }
        public string Departemen { get { return departemen; } set { departemen = value; } }
        public string Old_Sarmut_No { get { return old_Sarmut_No; } set { old_Sarmut_No = value; } }
        public string Sarmut_No { get { return sarmut_No; } set { sarmut_No = value; } }
        public DateTime Tgl_Sarmut { get { return tgl_Sarmut; } set { tgl_Sarmut = value; } }
        public int Sarmut_DeptID { get { return sarmut_DeptID; } set { sarmut_DeptID = value; } }
        public int Sarmut_Perusahaan { get { return sarmut_Perusahaan; } set { sarmut_Perusahaan = value; } }
        public int Sarmut_Dept { get { return sarmut_Dept; } set { sarmut_Dept = value; } }
        public int Sarmut_PemantauanDept { get { return sarmut_PemantauanDept; } set { sarmut_PemantauanDept = value; } }
        public int Status { get { return status; } set { status = value; } }
        public int Bulan { get { return bulan; } set { bulan = value; } }
        public int Tahun { get { return tahun; } set { tahun = value; } }
        public decimal Hasil_Pencapaian { get { return hasil_Pencapaian; } set { hasil_Pencapaian = value; } }
        public int Kesimpulan { get { return kesimpulan; } set { kesimpulan = value; } }
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
        public int Apv { get { return apv; } set { apv = value; } }
        public int Closed { get { return closed; } set { closed = value; } }
        public DateTime Close_Date { get { return close_Date; } set { close_Date = value; } }
        public string Close_By { get { return close_By; } set { close_By = value; } }
        public int Solved { get { return solved; } set { solved = value; } }
        public DateTime Solve_Date { get { return solve_Date; } set { solve_Date = value; } }
        public string Approval { get { return approval; } set { approval = value; } }
        public int Verified { get { return verified; } set { verified = value; } }
        public int NotVerified { get { return notverified; } set { notverified = value; } }


    }

    public class SPD_Sarmut
    {
        public int Urutan { get; set; }
        public string NO { get; set; }
        public string Dimensi { get; set; }
        public string SarMutPerusahaan { get; set; }
        public string Dept { get; set; }
        public string SarMutDepartemen { get; set; }
        public string ParameterTerukur { get; set; }
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string Mar { get; set; }
        public string Apr { get; set; }
        public string Mei { get; set; }
        public string Jun { get; set; }
        public string SMI { get; set; }
        public string Jul { get; set; }
        public string Agu { get; set; }
        public string Sep { get; set; }
        public string Okt { get; set; }
        public string Nov { get; set; }
        public string Des { get; set; }
        public string SMII { get; set; }
    }
}
