using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{

    public class HargaBarang : GRCBaseDomain
    {
        private int itemID = 0;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private string supplierName = string.Empty;
        private string supplierCode = string.Empty;
        private decimal price = 0;

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

    }
}
