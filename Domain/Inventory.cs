using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Domain
{
    public class Inventory : GRCBaseDomain
    {
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private string supplierCode = string.Empty;
        private int uOMID = 0;
        private decimal jumlah = 0;
        private int harga = 0;
        private decimal minStock = 0;
        private int deptID = 0;
        private string deptName = string.Empty;
        private string uOMDesc = string.Empty;
        private string groupDescription = string.Empty;
        private int rakID = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private int gudang = 0;
        private string shortKey = string.Empty;
        private string keterangan = string.Empty;
        private int head = 0;
        private int itemID = 0;
        private string uomCode = string.Empty;
        private decimal maxStock = 0;
        private decimal reOrder = 0;
        private decimal jmlTransit = 0;
        private int pakaiID = 0;
        private int pakaiDetailID = 0;
        private int aktif = 0;
        private int stock = 0;
        private string nama = string.Empty;
        private string type = string.Empty;
        private string merk = string.Empty;
        private string ukuran = string.Empty;
        private string jenis = string.Empty;
        private string partnum = string.Empty;
        private int leadTime = 0;
        private string alasannonaktif = string.Empty;
        //iko
        private int amGroupID = 0;
        private int amClassID = 0;
        private int amSubClassID = 0;
        private int amLokasiID = 0;
        private string amKodeAsset = string.Empty;
        private DateTime tanggal;
        public int AmGroupID
        {
            get { return amGroupID; }
            set { amGroupID = value; }
        }
        public int AmClassID
        {
            get { return amClassID; }
            set { amClassID = value; }
        }
        public int AmSubClassID
        {
            get { return amSubClassID; }
            set { amSubClassID = value; }
        }
        public int AmLokasiID
        {
            get { return amLokasiID; }
            set { amLokasiID = value; }
        }
        public string AmKodeAsset
        {
            get { return amKodeAsset; }
            set { amKodeAsset = value; }
        }
        //iko

        public int LeadTime
        {
            get { return leadTime; }
            set { leadTime = value; }
        }

        public string Alasannonaktif
        {
            get { return alasannonaktif; }
            set { alasannonaktif = value; }
        }
        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; }
        }
        public string Nama
        {
            get { return nama; }
            set { nama = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Merk
        {
            get { return merk; }
            set { merk = value; }
        }
        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }
        public string Jenis
        {
            get { return jenis; }
            set { jenis = value; }
        }
        public string Partnum
        {
            get { return partnum; }
            set { partnum = value; }
        }
        public int Aktif
        {
            get { return aktif; }
            set { aktif = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public int PakaiDetailID
        {
            get { return pakaiDetailID; }
            set { pakaiDetailID = value; }
        }
        public int PakaiID
        {
            get { return pakaiID; }
            set { pakaiID = value; }
        }

        public decimal JmlTransit
        {
            get { return jmlTransit; }
            set { jmlTransit = value; }
        }
        public decimal MaxStock
        {
            get { return maxStock; }
            set { maxStock = value; }
        }
        public decimal ReOrder
        {
            get { return reOrder; }
            set { reOrder = value; }
        }

        public string UomCode
        {
            get
            {
                return uomCode;
            }
            set
            {
                uomCode = value;
            }
        }

        public DateTime Tanggal
        {
            get
            {
                return tanggal;
            }
            set
            {
                tanggal = value;
            }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                return supplierCode;
            }
            set
            {
                supplierCode = value;
            }
        }

        public int GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
            }
        }

        public int ItemTypeID
        {
            get
            {
                return itemTypeID;
            }
            set
            {
                itemTypeID = value;
            }
        }

        public int UOMID
        {
            get
            {
                return uOMID;
            }
            set
            {
                uOMID = value;
            }
        }

        public string UOMDesc
        {
            get
            {
                return uOMDesc;
            }
            set
            {
                uOMDesc = value;
            }
        }
        
        public decimal Jumlah
        {
            get
            {
                return jumlah;
            }
            set
            {
                jumlah = value;
            }
        }

        public int Harga
        {
            get
            {
                return harga;
            }
            set
            {
                harga = value;
            }
        }

        public decimal MinStock
        {
            get
            {
                return minStock;
            }
            set
            {
                minStock = value;
            }
        }

        public int DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                deptID = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }
            set
            {
                deptName = value;
            }
        }
        

        public int RakID
        {
            get
            {
                return rakID;
            }
            set
            {
                rakID = value;
            }
        }

        
        public int Gudang
        {
            get
            {
                return gudang;
            }
            set
            {
                gudang = value;
            }
        }

        public string ShortKey
        {
            get
            {
                return shortKey;
            }
            set
            {
                shortKey = value;
            }
        }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }

        public int Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }

       

    }
    public class KartStock : Inventory
    {
        public DateTime Tanggal { get; set; }
        public string Faktur { get; set; }
        public string Uraian { get; set; }
        public decimal Masuk { get; set; }
        public decimal Keluar { get; set; }
        public decimal Saldo { get; set; }
        public string BonNo { get; set; }
        public int Tipe { get; set; }
    }
}
