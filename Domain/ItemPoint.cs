using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ItemPoint : GRCBaseDomain
    {
        private int itemID = 0;
        private string itemName = string.Empty;
        private int qty = 0;
        private int point = 0;
        private int pointType = 0;
        private int depoID = 0;
        private string depoName = string.Empty;

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

        public int Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }

        public int PointType
        {
            get
            {
                return pointType;
            }
            set
            {
                pointType = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public string DepoName
        {
            get
            {
                return depoName;
            }
            set
            {
                depoName = value;
            }
        }
    }
}
