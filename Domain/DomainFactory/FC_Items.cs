using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FC_Items : GRCBaseDomain
    {
        private int itemID = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private string partno = string.Empty;
        private string kode = string.Empty;
        private string sisi = string.Empty;
        private string itemDesc = string.Empty;
        private string lokasi = string.Empty;
        private decimal tebal = 0;
        private int lebar = 0;
        private int qID = 0;
        private int sisiID = 0;
        private int panjang = 0;
        private int stock = 0;
        private int status = 0;
        private decimal volume = 0;
        private decimal price = 0;
        private string ukuran = string.Empty;
        private string items = string.Empty;

        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int Status
        {
            get { return status ; }
            set { status = value; }
        }
        public int QID
        {
            get { return qID; }
            set { qID = value; }
        }
        public int SisiID
        {
            get { return sisiID; }
            set { sisiID = value; }
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
        public string Kode
        {
            get { return kode ; }
            set { kode = value; }
        }
        public string Sisi
        {
            get { return sisi; }
            set { sisi = value; }
        }
        public string ItemDesc
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
        public decimal Tebal
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
        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public decimal Price
        {
            get { return price ; }
            set { price = value; }
        }
        public string Ukuran
        {
            get { return ukuran ; }
            set { ukuran  = value; }
        }
        public string Items
        {
            get { return ukuran ; }
            set { ukuran = value; }
        }

        public class PartnoItems
        {
            public int ID { get; set; }
            public string PartNo { get; set; }
            public int Tebal { get; set; }
            public int Panjang { get; set; }
            public int Lebar { get; set; }
            public float Volume { get; set; }
            public int GroupID { get; set; }
            public string Kode { get; set; }
            public string Ukuran { get; set; }
        }
    }
}
