using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SarMut_Tindakan : GRCBaseDomain
    {
        private int sarmut_ID = 0;
        private string tindakan = string.Empty;
        private string pelaku = string.Empty;
        private DateTime jadwal_Selesai = DateTime.Now;
        private DateTime aktual_Selesai = DateTime.Now;
        private DateTime tglVerifikasi = DateTime.Now;
        private int verifikasi = 0;
        private string jenis = string.Empty;
        private string target = string.Empty;


        public int Sarmut_ID { get { return sarmut_ID; } set { sarmut_ID = value; } }
        public string Tindakan { get { return tindakan; } set { tindakan = value; } }
        public string Pelaku { get { return pelaku; } set { pelaku = value; } }
        public DateTime Jadwal_Selesai { get { return jadwal_Selesai; } set { jadwal_Selesai = value; } }
        public DateTime Aktual_Selesai { get { return aktual_Selesai; } set { aktual_Selesai = value; } }
        public DateTime TglVerifikasi { get { return tglVerifikasi; } set { tglVerifikasi = value; } }
        public int Verifikasi { get { return verifikasi; } set { verifikasi = value; } }
        public string Jenis { get { return jenis; } set { jenis = value; } }
        public string Target { get { return target; } set { target = value; } }

    }
}
