using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PaketItemDetail : GRCBaseDomain
    {
        private int paketItemID = 0;
        private int itemID = 0;
        private string itemName = string.Empty;
        private int quantity = 0;
        private int flagReport = 0;

        public int FlagReport
        {
            get
            {
                return flagReport;
            }
            set
            {
                flagReport = value;
            }
        }

        public int PaketItemID
        {
            get
            {
                return paketItemID;
            }
            set
            {
                paketItemID = value;
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

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

    }
}
