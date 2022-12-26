using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Biaya : GRCBaseDomain
    {
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private string supplierCode = string.Empty;
        private string satuan = string.Empty;
        private int uOMID = 0;
        private string uOMDesc = string.Empty;
        private decimal jumlah = 0;
        private int harga = 0;
        private int maxStock = 0;
        private int minStock = 0;
        private int deptID = 0;
        private string deptName = string.Empty;
        private int rakID = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private int gudang = 0;
        private string shortKey = string.Empty;
        private string keterangan = string.Empty;
        private int head = 0;
        private string uomCode = string.Empty;
        private int aktif = 0;

        public int Aktif
        {
            get { return aktif; }
            set { aktif = value; }
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

        public string Satuan
        {
            get
            {
                return satuan;
            }
            set
            {
                satuan = value;
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

        public int MinStock
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

        public int MaxStock
        {
            get
            {
                return maxStock;
            }
            set
            {
                maxStock = value;
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
}
