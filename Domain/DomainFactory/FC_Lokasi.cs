using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FC_Lokasi : GRCBaseDomain
    {

        private int iD = 0;
        private int lokTypeID = 0;
        private string lokasi = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int LokTypeID
        {
            get { return lokTypeID; }
            set { lokTypeID = value; }
        }
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
    }

    public class LokasiStok
    {
        public int ItemID { get; set; }
        public int LokasiID { get; set; }
        public string Saldo { get; set; }
    }
}
