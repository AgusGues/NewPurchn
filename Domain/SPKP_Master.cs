using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SPKP_Master : GRCBaseDomain
    {
        private string noSPKP = string.Empty;
        private DateTime tglSPKP = DateTime.Now;
        private DateTime drtanggal= DateTime.Now;
        private DateTime sdtanggal = DateTime.Now;
        private int revisi = 0;
        private int tahun = 0;
        private int bulan = 0;
        private int minggu = 0;
        private int iD= 0;

        public string NoSPKP { get { return noSPKP; } set { noSPKP = value; } }
        public DateTime TglSPKP { get { return tglSPKP; } set { tglSPKP = value; } }
        public DateTime DrTanggal { get { return drtanggal; } set { drtanggal = value; } }
        public DateTime SdTanggal { get { return sdtanggal; } set { sdtanggal = value; } }
        public int Revisi { get { return revisi; } set { revisi = value; } }
        public int Tahun { get { return tahun; } set { tahun = value; } }
        public int Bulan { get { return bulan; } set { bulan = value; } }
        public int Minggu { get { return minggu; } set { minggu = value; } }
        public int ID { get { return iD; } set { iD = value; } }

        public virtual string PartNo { get; set; }
        public virtual string PaletNo { get; set; }
        public virtual string Rak { get; set; }
        public virtual DateTime TglProduksi { get; set; }
        public virtual DateTime TglJemur { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual int Status { get; set; }
        public virtual decimal Total { get; set; }
        public int Selisih { get; set; }

    }
}
