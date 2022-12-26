using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Promo : GRCBaseDomain 
    {
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        //private int distributorID = 0;
        //private string distributorName = string.Empty;
        private int agenID = 0;
        private decimal price = 0;
        private int zonaID = 0;
        private string zonaCode = string.Empty;
        private int typeAgen = 0;



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

        //public int DistributorID
        //{
        //    get
        //    {
        //        return distributorID;
        //    }
        //    set
        //    {
        //        distributorID = value;
        //    }
        //}

        //public string DistributorName
        //{
        //    get
        //    {
        //        return distributorName;
        //    }
        //    set
        //    {
        //        distributorName = value;
        //    }
        //}

        public int AgenID
        {
            get
            {
                return agenID;
            }
            set
            {
                agenID = value;
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

        public int ZonaID
        {
            get
            {
                return zonaID;
            }
            set
            {
                zonaID = value;
            }
        }

        public string ZonaCode
        {
            get
            {
                return zonaCode;
            }
            set
            {
                zonaCode = value;
            }
        }

        public int TypeAgen
        {
            get
            {
                return typeAgen;
            }
            set
            {
                typeAgen = value;
            }
        }


    }
}
