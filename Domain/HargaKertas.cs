using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HargaKertas : GRCBaseDomain
    {
        private int supplierID = 0;
        private decimal harga = 0;
        private int aktif = 0;
        private int itemID = 0;
        private string supplierCode = string.Empty;
        private string itemCode = string.Empty;
        private string supplierName = string.Empty;
        private string itemName = string.Empty;
        private decimal addPrice = 0;
        private decimal minPrice = 0;
        private decimal kadarair = 0;
        private decimal priceList = 0;
        private decimal priceBeli = 0;
        private int subComp = 0;

        public int SubComp
        {
            get { return subComp; }
            set { subComp = value; }
        }
        public decimal PriceBeli
        {
            get { return priceBeli; }
            set { priceBeli = value; }
        }
        public decimal PriceList
        {
            get { return priceList; }
            set { priceList = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public decimal AddPrice
        {
            get { return addPrice; }
            set { addPrice = value; }
        }
        public decimal MinPrice
        {
            get { return minPrice; }
            set { minPrice = value; }
        }

        public decimal Kadarair
        {
            get { return kadarair; }
            set { kadarair = value; }
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
        public string SupplierName
        {
            get
            {
                return supplierName;
            }
            set
            {
                supplierName = value;
            }
        }
        
        
        public int SupplierID
        {
            get
            {
                return supplierID;
            }
            set
            {
                supplierID = value;
            }
        }

        public int Aktif
        {
            get
            {
                return aktif;
            }
            set
            {
                aktif = value;
            }
        }

        public decimal Harga
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

    }
}
