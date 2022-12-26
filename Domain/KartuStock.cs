using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class KartuStock : GRCBaseDomain
    {
        private int awal = 0;
        private string keterangan = string.Empty;
        private DateTime tanggal = DateTime.Now;
        private int penerimaan = 0;
        private int pengeluaran = 0;
        private int saldo = 0;
        private decimal hPP = 0;
        private string process = string.Empty;
        public int Awal
        {
            get { return awal; }
            set { awal = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string Process
        {
            get { return process; }
            set { process = value; }
        }
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }
        public int Penerimaan
        {
            get { return penerimaan; }
            set { penerimaan = value; }
        }
        public int Pengeluaran
        {
            get { return pengeluaran; }
            set { pengeluaran = value; }
        }
        public int Saldo
        {
            get { return saldo ; }
            set { saldo = value; }
        }
        public Decimal HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }
    }
}
