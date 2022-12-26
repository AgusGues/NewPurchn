using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Quota
    {
        private int id = 0;
        private int itemID = 0;
        private string itemName = string.Empty;
        private int tokoID = 0;
        private string tokoCode = string.Empty;
        private string tokoName = string.Empty;
        private string distributorCode = string.Empty;
        private int jumquota = 0;
        private int saldo = 0;
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

        public int TokoID
        {
            get
            {
                return tokoID;
            }
            set
            {
                tokoID = value;
            }
        }

        public string TokoCode
        {
            get
            {
                return tokoCode;
            }
            set
            {
                tokoCode = value;
            }
        }

        public string DistributorCode
        {
            get
            {
                return distributorCode;
            }
            set
            {
                distributorCode = value;
            }
        }

        public string TokoName
        {
            get
            {
                return tokoName;
            }            
            set
            {
                tokoName = value;
            }
        }

        public int JumQuota
        {
            get
            {
                return jumquota;
            }
            set
            {
                jumquota = value;
            }
        }

        public int Saldo
        {
            get
            {
                return saldo;
            }
            set
            {
                saldo = value;
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
