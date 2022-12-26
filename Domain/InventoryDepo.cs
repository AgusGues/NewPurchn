using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class InventoryDepo 
    {
        private int id = 0;
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int quantity = 0;
        private int depoID = 0;
        private string depoCode = string.Empty;
        private string depoName = string.Empty;
        private int status = 0;

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

        public string DepoCode
        {
            get
            {
                return depoCode;
            }
            set
            {
                depoCode = value;
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
