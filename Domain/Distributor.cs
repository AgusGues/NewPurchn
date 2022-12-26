using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Distributor : GRCBaseDomain
    {
        private string distributorCode = string.Empty;
        private string distributorName = string.Empty;
        private string address = string.Empty;        
        private DateTime joinDate = DateTime.Now.Date;
        private decimal creditLimit = 0;
        private int zonaID = 0;
        private string zonaCode = string.Empty;
        private string npwp = string.Empty;
        private string telepon = string.Empty;
        private decimal totalPoint = 0;
        private string contactPerson = string.Empty;
        private int depoID = 0;
        private string depoName = string.Empty;
        private int chargeToPayment = 0;
        private int idDistMaster = 0;
        private int idDist = 0;
        private string idDistStr = string.Empty;
        private int orderOP = 0;
        private int priceTypeID = 0;

        private string codeEncript = string.Empty;
        private decimal nominal = 0;
        private DateTime confirmationDate = DateTime.MinValue;
        private int voucherID = 0;

        public int PriceTypeID { get { return priceTypeID; } set { priceTypeID = value; } }
        public string CodeEncript { get { return codeEncript; } set { codeEncript = value; } }
        public decimal Nominal { get { return nominal; } set { nominal = value; } }
        public DateTime ConfirmationDate { get { return confirmationDate; } set { confirmationDate = value; } }
        public int VoucherID { get { return voucherID; } set { voucherID = value; } }

        public int OrderOP { get { return orderOP; } set { orderOP = value; } }
        public int IdDistMaster { get { return idDistMaster; } set { idDistMaster = value; } }
        public int IdDist { get { return idDist; } set { idDist = value; } }
        public string IdDistStr { get { return idDistStr; } set { idDistStr = value; } }
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
        public int ChargeToPayment
        {
            get
            {
                return chargeToPayment;
            }
            set
            {
                chargeToPayment = value;
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

        public string Telepon
        {
            get
            {
                return telepon;
            }
            set
            {
                telepon = value;
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
