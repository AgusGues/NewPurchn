using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM_Lampiran : GRCBaseDomain
    {
        private int rMM_ID = 0;
        private string fileName = string.Empty;

        private string tanggalUpload = string.Empty;

        public string TanggalUpload
        {
            get { return tanggalUpload; }
            set { tanggalUpload = value; }
        }

        public int RMM_ID { get { return rMM_ID; } set { rMM_ID = value; } }
        public string FileName { get { return fileName; } set { fileName = value; } }
    }
}
