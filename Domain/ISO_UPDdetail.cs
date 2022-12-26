using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_UPDdetail : GRCBaseDomain
    {
        public int UPDid { get; set; }
        public string Alasan { get; set; }
        public string IsiDokumen { get; set; }
        public string DokumenTerkainLain { get; set; }
        public string DokumenLama { get; set; }
        public string DokumenBaru { get; set; }
        public string NoDokumen { get; set; }
        public string NamaDokumen { get; set; }
        public string NoRevisi { get; set; }
        public DateTime TglBerlaku { get; set; }

    }
}
