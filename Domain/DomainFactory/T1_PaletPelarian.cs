using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_PaletPelarian : GRCBaseDomain
    {
        private DateTime tglPotong = DateTime.Now.Date;
        private string noPalet = string.Empty;
        private string partnoAsal = string.Empty;
        private int qtyAsal = 0;
        private string partnoOK = string.Empty;
        private int qtyOK = 0;
        private string partnoBP = string.Empty;
        private int qtyBP = 0;

        public DateTime TglPotong { get { return tglPotong; } set { tglPotong = value; } }
        public string NoPalet { get { return noPalet; } set { noPalet = value; } }
        public string PartnoAsal { get { return partnoAsal; } set { partnoAsal = value; } }
        public int QtyAsal { get { return qtyAsal; } set { qtyAsal = value; } }
        public string PartnoOK { get { return partnoOK; } set { partnoOK = value; } }
        public int QtyOK { get { return qtyOK; } set { qtyOK = value; } }
        public string PartnoBP { get { return partnoBP; } set { partnoBP = value; } }
        public int QtyBP { get { return qtyBP; } set { qtyBP = value; } }
    }

    public class T1Pelarian
    {
        public int ID { get; set; }
        public string PartnoAsal { get; set; }
        public string PartnoTujuan { get; set; }
        public string LokasiTujuan { get; set; }
        public int QtyTujuan { get; set; }
        public string TglPotong { get; set; }
    }
}
