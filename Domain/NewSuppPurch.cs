using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class NewSuppPurch : GRCBaseDomain
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
        private string keterangan = string.Empty;
        private string jenisusaha = string.Empty;
        private string kTP = string.Empty;
        private string nPWP_P = string.Empty;
        private string namaRekening = string.Empty;
        private string bankRekening = string.Empty;
        private string nomorRekening = string.Empty;

        public string NomorRekening
        {
            get { return nomorRekening; }
            set { nomorRekening = value; }
        }

        public string BankRekening
        {
            get { return bankRekening; }
            set { bankRekening = value; }
        }

        public string NamaRekening
        {
            get { return namaRekening; }
            set { namaRekening = value; }
        }

        public string KTP
        {
            get { return kTP ; }
            set { kTP = value; }
        }
        public string NPWP_P
        {
            get { return nPWP_P; }
            set { nPWP_P = value; }
        }
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
        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }
        public string JenisUsaha
        {
            get
            {
                return jenisusaha;
            }
            set
            {
                jenisusaha = value;
            }
        }
        public int Flag { get; set; }
        public int Aktif { get; set; }
        public int SubCompanyID { get; set; }
        public int CompanyID { get; set; }
        public int ForDK { get; set; }

    }
}
