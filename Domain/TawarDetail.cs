using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TawarDetail
    {
        private int id = 0;
        private int pOID = 0;
        private int sPPID = 0;
        private int groupID = 0;
        private int itemID = 0;
        private decimal price = 0;
        private decimal qty = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private int status = 0;
        private int noUrut = 0;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private string uomCode = string.Empty;
        private decimal stok = 0;
        private decimal jumQty = 0;
        private string noSPP = string.Empty;
        private string namaBarang = string.Empty;
        private string satuan = string.Empty;
        private string documentNo = string.Empty;
        private int sPPDetailID = 0;
        private decimal total = 0;

        public string NoSPP
        {
            get
            {
                return noSPP;
            }
            set
            {
                noSPP = value;
            }
        }

        public string NamaBarang
        {
            get
            {
                return namaBarang;
            }
            set
            {
                namaBarang = value;
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

        public int SPPDetailID
        {
            get
            {
                return sPPDetailID;
            }
            set
            {
                sPPDetailID = value;
            }
        }

        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }
        public decimal JumQty
        {
            get { return jumQty; }
            set { jumQty = value; }
        }
        public string DocumentNo
        {
            get { return documentNo; }
            set { documentNo = value; }
        }
        public decimal Stok
        {
            get { return stok; }
            set { stok = value; }
        }
        public string UOMCode
        {
            get { return uomCode; }
            set { uomCode = value; }
        }
        public string ItemCode
        {
            get
            { return itemCode; }
            set
            { itemCode = value; }
        }
        public string ItemName
        {
            get
            { return itemName; }
            set
            { itemName = value; }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int POID
        {
            get
            {
                return pOID;
            }
            set
            {
                pOID = value;
            }
        }

        public int SPPID
        {
            get
            {
                return sPPID;
            }
            set
            {
                sPPID = value;
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

        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public decimal Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
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

        public int NoUrut
        {
            get
            {
                return noUrut;
            }
            set
            {
                noUrut = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

    }

}
