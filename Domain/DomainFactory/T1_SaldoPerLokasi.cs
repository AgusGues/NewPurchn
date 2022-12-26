using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    
    public class T1_SaldoPerLokasi : GRCBaseDomain
    {
        private string  thnBln = string.Empty;
        private int lokasiID = 0;
        private string lokasi = string.Empty;
        private int itemID=0;
        private string partno = string.Empty;
        private int saldo = 0;

        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }
        
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
        
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public string Partno
        {
            get { return partno ; }
            set { partno = value; }
        }

        public string ThnBln
        {
            get { return thnBln ; }
            set { thnBln = value; }
        }

        public int Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

    }

    public class CopyOfT1_SaldoPerLokasi : GRCBaseDomain
    {
        private string thnBln = string.Empty;
        private int lokasiID = 0;
        private string lokasi = string.Empty;
        private int itemID = 0;
        private string partno = string.Empty;
        private int saldo = 0;

        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }

        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public string Partno
        {
            get { return partno; }
            set { partno = value; }
        }

        public string ThnBln
        {
            get { return thnBln; }
            set { thnBln = value; }
        }

        public int Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }

    }
}
