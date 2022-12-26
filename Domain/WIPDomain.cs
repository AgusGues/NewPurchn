using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class WIPDomain : GRCBaseDomain
    {
        private string partno = string.Empty;
        private string topartno = string.Empty;
        private string lokasi = string.Empty;
        private string rak = string.Empty;
        private string paletno = string.Empty;
        private string keterangan = string.Empty;
        private int destckid = 0;
        private int qty = 0;
        private string nopart = string.Empty;
        private int saldo = 0;
        private decimal qtyin = 0;
        private decimal qtyout = 0;
        private decimal sqtyin = 0;
        private decimal sqtyout = 0;
        private string slokasi = string.Empty;
        private DateTime tglserah;
        private DateTime tanggal;
        private int nom = 0;

        public int Nom
        {
            set { nom = value; }
            get { return nom; }
        }
        public DateTime Tanggal
        {
            set { tanggal = value; }
            get { return tanggal; }
        }
        public string PartNo
        {
            get { return partno; }
            set { partno = value; }
        }
        public string ToPartNo
        {
            get { return topartno; }
            set { topartno = value; }
        }
        public string Lokasi
        {
            set { lokasi = value; }
            get { return lokasi; }
        }
        public string Rak
        {
            set { rak = value; }
            get { return rak; }
        }
        public string PaletNo
        {
            set { paletno = value; }
            get { return paletno; }
        }
        public string Keterangan
        {
            set { keterangan = value; }
            get { return keterangan; }
        }
        public int DestackID
        {
            set { destckid = value; }
            get { return destckid; }
        }
        public int Qty
        {
            set { qty = value; }
            get { return qty; }
        }
        public string NoPart
        {
            set { nopart = value; }
            get { return nopart; }
        }
        public int Saldo
        {
            set { saldo = value; }
            get { return saldo; }
        }
        public decimal QtyIn
        {
            set { qtyin = value; }
            get { return qtyin; }
        }
        public decimal QtyOut
        {
            set { qtyout = value; }
            get { return qtyout; }
        }
        public decimal sQtyIn
        {
            set { sqtyin = value; }
            get { return sqtyin; }
        }
        public decimal sQtyOut
        {
            set { sqtyout = value; }
            get { return sqtyout; }
        }
        public DateTime TglSerah
        {
            set { tglserah = value; }
            get { return tglserah; }
        }
    }
}
