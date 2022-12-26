using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class StockMovement
    {
        private int id = 0;
        private int typeDoc = 0;
        private string noDoc = string.Empty;
        private DateTime tglDoc = DateTime.Now.Date;
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int depoID = 0;
        private string depoName = string.Empty;
        private int quantity = 0;
        private int status = 0;
        private string createdBy = string.Empty;
        private int toDepoID = 0;

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

        public int TypeDoc
        {
            get
            {
                return typeDoc;
            }
            set
            {
                typeDoc = value;
            }
        }

        public string NoDoc
        {
            get
            {
                return noDoc;
            }
            set
            {
                noDoc = value;
            }
        }

        public DateTime TglDoc
        {
            get
            {
                return tglDoc;
            }
            set
            {
                tglDoc = value;
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

        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public int ToDepoID
        {
            get
            {
                return toDepoID;
            }
            set
            {
                toDepoID = value;
            }
        }

    }
}
