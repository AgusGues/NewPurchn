using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SubDistributor : GRCBaseDomain
    {
        private string subDistributorCode = string.Empty;
        private string subDistributorName = string.Empty;
        private string address = string.Empty;
        private DateTime joinDate = DateTime.Now.Date;
        private decimal creditLimit = 0;
        private int zonaID = 0;        
        private string zonaCode = string.Empty;
        private int distributorID = 0;
        private string distributorName = string.Empty;
        private string npwp = string.Empty;
        private decimal totalPoint = 0;
        private string contactPerson = string.Empty;
        private int priceTypeID = 0;
        private string emailAddress = string.Empty;
        private string pic = string.Empty;


        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }
        public int PriceTypeID { get { return priceTypeID; } set { priceTypeID = value; } }
        public string SubDistributorCode
        {
            get
            {
                return subDistributorCode;
            }
            set
            {
                subDistributorCode = value;
            }
        }

        public string SubDistributorName
        {
            get
            {
                return subDistributorName;
            }
            set
            {
                subDistributorName = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public DateTime JoinDate
        {
            get
            {
                return joinDate;
            }
            set
            {
                joinDate = value;
            }
        }

        public decimal CreditLimit
        {
            get
            {
                return creditLimit;
            }
            set
            {
                creditLimit = value;
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

        public int DistributorID
        {
            get
            {
                return distributorID;
            }
            set
            {
                distributorID = value;
            }
        }

        public string DistributorName
        {
            get
            {
                return distributorName;
            }
            set
            {
                distributorName = value;
            }
        }

        public string NPWP
        {
            get
            {
                return npwp;
            }
            set
            {
                npwp = value;
            }
        }

        public decimal TotalPoint
        {
            get
            {
                return totalPoint;
            }
            set
            {
                totalPoint = 0;
            }
        }

        public string ContactPerson
        {
            get
            {
                return contactPerson;
            }
            set
            {
                contactPerson = value;
            }

        }
 
    }
}
