using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ItemPrice : GRCBaseDomain
    {
        private int itemID = 0;
        private string itemName = string.Empty;
        private int zonaID = 0;
        private string zonaCode = string.Empty;
        private decimal hargaJual = 0;
        private decimal hargaDistributor = 0;
        private decimal hargaSubDistributor = 0;
        private decimal hargaRetail = 0;
        private decimal hargaToko = 0;                

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

        public decimal HargaJual
        {
            get
            {
                return hargaJual;
            }
            set
            {
                hargaJual = value;
            }
        }

        public decimal HargaDistributor
        {
            get
            {
                return hargaDistributor;
            }
            set
            {
                hargaDistributor = value;
            }
        }

        public decimal HargaSubDistributor
        {
            get
            {
                return hargaSubDistributor;
            }
            set
            {
                hargaSubDistributor = value;
            }
        }

        public decimal HargaRetail
        {
            get
            {
                return hargaRetail;
            }
            set
            {
                hargaRetail = value;
            }
        }

        public decimal HargaToko
        {
            get
            {
                return hargaToko;
            }
            set
            {
                hargaToko = value;
            }
        }      
    }
}
