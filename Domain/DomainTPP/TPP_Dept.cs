using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Dept : GRCBaseDomain
    {
        private string kode = string.Empty;
        private string departemen = string.Empty;

        public string Kode { get { return kode; } set { kode = value; } }
        public string Departemen { get { return departemen; } set { departemen = value; } }

        //public CreatedTime TPP_Date { get; set; }
        public string CreatedTime { get; set; }
        public string TPP_Date { get; set; }
        public string Laporan_No { get; set; }
        public string PIC { get; set; }
        public string Keterangan { get; set; }
        public string Uraian { get; set; }
        public string Ketidaksesuaian { get; set; }
        public string Status { get; set; }
    }
}
