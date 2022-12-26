using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Tindakan : GRCBaseDomain
    {
        private int tPP_ID = 0;
        private string tindakan = string.Empty;
        private string pelaku = string.Empty;
        private DateTime jadwal_Selesai = DateTime.Now;
        private DateTime aktual_Selesai = DateTime.MinValue;
        private DateTime tglverifikasi = DateTime.Now;
        private int verifikasi = 0;
        private string jenis = string.Empty;
        private string target = string.Empty;

        public int TPP_ID { get { return tPP_ID; } set { tPP_ID = value; } }
        public string Tindakan { get { return tindakan; } set { tindakan = value; } }
        public string Pelaku { get { return pelaku; } set { pelaku = value; } }
        public DateTime Jadwal_Selesai { get { return jadwal_Selesai; } set { jadwal_Selesai = value; } }
        public DateTime Aktual_Selesai { get { return aktual_Selesai; } set { aktual_Selesai = value; } }
        public DateTime Tglverifikasi { get { return tglverifikasi; } set { tglverifikasi = value; } }
        public int Verifikasi { get { return verifikasi; } set { verifikasi = value; } }
        public string Jenis { get { return jenis; } set { jenis = value; } }
        public string Target { get { return target; } set { target = value; } }

        public string ApvMgr_Users { get; set; }
        public int Dept_ID_From { get; set; }
    }
}
