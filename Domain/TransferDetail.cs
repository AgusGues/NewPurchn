using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TransferDetail 
    {
        private int id = 0;
        private int transferOrderID = 0;
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int qty = 0;
        private int qtyScheduled = 0;
        private int qtyReceived = 0;
        private int status = 0;
        private decimal tebal = 0;
        private decimal panjang = 0;
        private decimal lebar = 0;
        private decimal berat = 0;
        private int paket = 0;
        public int  TypeKondisi { get; set; }
        public int FromDepoID { get; set; }

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

        public int TransferOrderID
        {
            get
            {
                return transferOrderID;
            }
            set
            {
                transferOrderID = value;
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

        public int Qty
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

        public int QtyScheduled
        {
            get
            {
                return qtyScheduled;
            }
            set
            {
                qtyScheduled = value;
            }
        }

        public int QtyReceived
        {
            get
            {
                return qtyReceived;
            }
            set
            {
                qtyReceived = value;
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

        public decimal Tebal
        {
            get
            {
                return tebal;
            }
            set
            {
                tebal = value;
            }
        }

        public decimal Panjang
        {
            get
            {
                return panjang;
            }
            set
            {
                panjang = value;
            }
        }

        public decimal Lebar
        {
            get
            {
                return lebar;
            }
            set
            {
                lebar = value;
            }
        }

        public decimal Berat
        {
            get
            {
                return berat;
            }
            set
            {
                berat = value;
            }
        }

        public int Paket
        {
            get
            {
                return paket;
            }
            set
            {
                paket = value;
            }

        }
    }
}
