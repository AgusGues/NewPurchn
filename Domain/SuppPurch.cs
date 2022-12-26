using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuppPurch : GRCBaseDomain
    {
        private string supplierCode = string.Empty;
        private string supplierName = string.Empty;
        private string alamat = string.Empty;
        private string uP = string.Empty;
        private string telepon = string.Empty;
        private string fax = string.Empty;
        private string handphone = string.Empty;
        private DateTime joinDate = DateTime.Now.Date;
        private int rowStatus = 0;
        private string createdBy = string.Empty;
        private DateTime createdTime = DateTime.Now.Date;
        private string lastModifiedBy = string.Empty;
        private DateTime lastModifiedTime = DateTime.Now.Date;
        private string nPWP = string.Empty;
        private string eMail = string.Empty;
        private string pKP = string.Empty;
        public int CoID { get; set; }
        public string ChartNoType1 { get; set; }
        public string ChartNoType2 { get; set; }
        public string PKP
        {
            get { return pKP; }
            set { pKP = value; }
        }
        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }
        public string NPWP
        {
            get
            {
                return nPWP;
            }
            set
            {
                nPWP = value;
            }
        }
        public string SupplierCode
        {
            get
            {
                return supplierCode;
            }
            set
            {
                supplierCode = value;
            }
        }
        public string SupplierName
        {
            get
            {
                return supplierName;
            }
            set
            {
                supplierName = value;
            }
        }
        public string Alamat
        {
            get
            {
                return alamat;
            }
            set
            {
                alamat = value;
            }
        }
        public string UP
        {
            get
            {
                return uP;
            }
            set
            {
                uP = value;
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
        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }
        public string Handphone
        {
            get
            {
                return handphone;
            }
            set
            {
                handphone = value;
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
        public int RowStatus
        {
            get
            {
                return rowStatus;
            }
            set
            {
                rowStatus = value;
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
        public DateTime CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
            }
        }
        public string LastModifiedBy
        {
            get
            {
                return lastModifiedBy;
            }
            set
            {
                lastModifiedBy = value;
            }
        }
        public DateTime LastModifiedTime
        {
            get
            {
                return lastModifiedTime;
            }
            set
            {
                lastModifiedTime = value;
            }
        }
        public int Flag { get; set; }
        public int Aktif { get; set; }
        public int SubCompanyID { get; set; }
        public int CompanyID { get; set; }
        public int ForDK { get; set; }
        
    }
}
