using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Lampiran : GRCBaseDomain
    {
        private int tPP_ID = 0;
        private string fileName = string.Empty ;
        private string tanggalUpload = string.Empty;

        public string TanggalUpload
        {
            get { return tanggalUpload; }
            set { tanggalUpload = value; }
        }


        public int TPP_ID { get { return tPP_ID; } set { tPP_ID = value; } }
        public string FileName { get { return fileName; } set { fileName = value; } }
    }
}
