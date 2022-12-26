using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Supplier : GRCBaseDomain
    {
        private string supplierCode = string.Empty;
        private string supplierName = string.Empty;
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
        private string salesmanName = string.Empty;
        private string contactPerson = string.Empty;
        private string lokasiPajak = string.Empty;

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
