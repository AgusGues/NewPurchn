using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Serah : GRCBaseDomain
    {
        private int itemID = 0;
        private string partno = string.Empty;
        private string noPalet= string.Empty;
        private DateTime tglProduksi = DateTime.MinValue;
        private string partname = string.Empty;
        private int lokID = 0;
        private int groupID = 0;
        private string lokasi = string.Empty;
        private string flag = string.Empty;
        private decimal tebal = 0;
        private int lebar = 0;
        private int panjang = 0;
        private int qty = 0;
        decimal hPP = 0;
        private decimal volume = 0;
       
        string jDefect = string.Empty;
        string groupProd = string.Empty;
        private DateTime tglProd = DateTime.Now;


        public DateTime TglProd
        {
            get { return tglProd; }
            set { tglProd = value; }
        }
        public string JDefect
        {
            get { return jDefect; }
            set { jDefect = value; }
        }
        public string GroupProd
        {
            get { return groupProd; }
            set { groupProd = value; }
        }
        public decimal  HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public string Partno
        {
            get { return partno; }
            set { partno = value; }
        }
        public string Partname
        {
            get { return partname; }
            set { partname = value; }
        }
        public int LokID
        {
            get { return lokID; }
            set { lokID = value; }
        }
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public decimal  Tebal
        {
            get { return tebal; }
            set { tebal = value; }
        }
        public int Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }
        public int Panjang
        {
            get { return panjang; }
            set { panjang = value; }
        }
        public decimal  Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
       
    }
}
