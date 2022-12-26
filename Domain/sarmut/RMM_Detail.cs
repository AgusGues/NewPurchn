using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM_Detail : GRCBaseDomain
    {
        private int rmm_ID = 0;
        private string pelaku = string.Empty;
        private string aktivitas = string.Empty;
        private int rMM_SumberDayaID = 0;
        private DateTime jadwal_Selesai = DateTime.Now;
        private DateTime aktual_Selesai = DateTime.MinValue;
        private DateTime tglverifikasi = DateTime.Now;
        private int verifikasi = 0;
        private string target = string.Empty;
        private int rMM_LocID = 0;
        private int rMM_Dept = 0;
        private string ket = string.Empty;
        private int solved = 0;


        public int Solved { get { return solved; } set { solved = value; } }
        public string Ket { get { return ket; } set { ket = value; } }
        public int RMM_Dept { get { return rMM_Dept; } set { rMM_Dept = value; } }
        public int RMM_LocID { get { return rMM_LocID; } set { rMM_LocID = value; } }
        public int RMM_ID { get { return rmm_ID; } set { rmm_ID = value; } }
        public string Pelaku { get { return pelaku; } set { pelaku = value; } }
        public string Aktivitas { get { return aktivitas; } set { aktivitas = value; } }
        public int RMM_SumberDayaID { get { return rMM_SumberDayaID; } set { rMM_SumberDayaID = value; } }
        public DateTime Jadwal_Selesai { get { return jadwal_Selesai; } set { jadwal_Selesai = value; } }
        public DateTime Aktual_Selesai { get { return aktual_Selesai; } set { aktual_Selesai = value; } }
        public DateTime TglVerifikasi { get { return tglverifikasi; } set { tglverifikasi = value; } }
        public int Verifikasi { get { return verifikasi; } set { verifikasi = value; } }
        public string Target { get { return target; } set { target = value; } }
    }
}
