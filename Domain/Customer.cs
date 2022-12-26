using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Customer : GRCBaseDomain
    {
        private string customerCode = string.Empty;
        private string customerName = string.Empty;
        private string address = string.Empty;
        private int propinsiID = 0;
        private string namaPropinsi = string.Empty;
        private int cityID = 0;
        private string cityName = string.Empty;
        private int kabupatenID = 0;
        private string namaKabupaten = string.Empty;
        private DateTime joinDate = DateTime.Now.Date;
        private decimal creditLimit = 0;
        private int zonaID = 0;
        private string zonaCode = string.Empty;
        private string npwp = string.Empty;
        private string telepon = string.Empty;
        private decimal totalPoint = 0;
        private int salesmanID = 0;
        private string salesmanName = string.Empty;
        private string contactPerson = string.Empty;
        private string lokasiPajak = string.Empty;
        private string distributorCode = string.Empty;
        private int block = 0;
        private int statusCreditLimit = 0;
        private int flagCL = 0;
        private int categoryPaymentID = 0;
        private string emailAddress = string.Empty;
        private int categoryCompanyID = 0;
        private int jmlHari = 0;
        private decimal uangMukaPersen = 0;
        private string distCode = string.Empty;
        private int itemPriceCategoryID = 0;
        private int lamaPembayaran = 0;
        private int jenisCustomer = 0;
        private int distAlmtID = 0;
        //
        private string asNamaTax = string.Empty;
        private string asNPWPTax = string.Empty;
        private string asAlamatTax = string.Empty;
        private string ktp_Nik = string.Empty;
        private string ktp_Nama = string.Empty;
        private string ktp_Alamat = string.Empty;

        public string Ktp_Nik { get { return ktp_Nik; } set { ktp_Nik = value; } }
        public string Ktp_Nama { get { return ktp_Nama; } set { ktp_Nama = value; } }
        public string Ktp_Alamat { get { return ktp_Alamat; } set { ktp_Alamat = value; } }

        public string AsNamaTax
        {
            get { return asNamaTax; }
            set { asNamaTax = value; }
        }
        public string AsNPWPTax
        {
            get { return asNPWPTax; }
            set { asNPWPTax = value; }
        }
        public string AsAlamatTax
        {
            get { return asAlamatTax; }
            set { asAlamatTax = value; }
        }
        //
        public int DistAlmtID
        {
            get { return distAlmtID; }
            set { distAlmtID = value; }
        }

        public int JenisCustomer
        {
            get { return jenisCustomer; }
            set { jenisCustomer = value; }
        }
        public int LamaPembayaran
        {
            get { return lamaPembayaran; }
            set { lamaPembayaran = value; }
        }
        public int ItemPriceCategoryID
        {
            get { return itemPriceCategoryID; }
            set { itemPriceCategoryID = value; }
        }
        public string DistCode
        {
            get { return distCode; }
            set { distCode = value; }
        }
        public decimal UangMukaPersen
        {
            get { return uangMukaPersen; }
            set { uangMukaPersen = value; }
        }
        public int JmlHari
        {
            get { return jmlHari; }
            set { jmlHari = value; }
        }
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }
        public int CategoryCompanyID
        {
            get { return categoryCompanyID; }
            set { categoryCompanyID = value; }
        }

        public int CategoryPaymentID
        {
            get { return categoryPaymentID; }
            set { categoryPaymentID = value; }
        }
        public int FlagCL
        {
            get { return flagCL; }
            set { flagCL = value; }
        }
        public int Block
        {
            get { return block; }
            set { block = value; }
        }
        public int StatusCreditLimit
        {
            get { return statusCreditLimit; }
            set { statusCreditLimit = value; }
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
        public string CustomerCode
        {
            get
            {
                return customerCode;
            }
            set
            {
                customerCode = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
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

        public int PropinsiID
        {
            get
            {
                return propinsiID;
            }
            set
            {
                propinsiID = value;
            }
        }

        public string NamaPropinsi
        {
            get
            {
                return namaPropinsi;
            }
            set
            {
                namaPropinsi = value;
            }
        }

        public int CityID
        {
            get
            {
                return cityID;
            }
            set
            {
                cityID = value;
            }
        }

        public string CityName
        {
            get
            {
                return cityName;
            }
            set
            {
                cityName = value;
            }
        }

        public int KabupatenID
        {
            get
            {
                return kabupatenID;
            }
            set
            {
                kabupatenID = value;
            }
        }

        public string NamaKabupaten
        {
            get
            {
                return namaKabupaten;
            }
            set
            {
                namaKabupaten = value;
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

        public int SalesmanID
        {
            get
            {
                return salesmanID;
            }
            set
            {
                salesmanID = value;
            }
        }

        public string SalesmanName
        {
            get
            {
                return salesmanName;
            }
            set
            {
                salesmanName = value;
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

        public string LokasiPajak
        {
            get
            {
                return lokasiPajak;
            }
            set
            {
                lokasiPajak = value;
            }
        }
    }
}
