using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Toko : GRCBaseDomain
    {
        private string tokoCode = string.Empty;
        private string tokoName = string.Empty;
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
        private int distributorID = 0;
        private string distributorCode = string.Empty;
        private string distributorName = string.Empty;
        private int subDistributorID = 0;
        private string subDistributorCode = string.Empty;
        private string subDistributorName = string.Empty;
        private string npwp = string.Empty;
        private string telepon = string.Empty;
        private decimal totalPoint = 0;
        private int salesmanID = 0;
        private string salesmanName = string.Empty;
        private string contactPerson = string.Empty;
        private string keterangan = string.Empty;
        private string noHp = string.Empty;
        //private int point = 0;
        private int block = 0;
        private int getPoint = 0;
        private string lokasiPajak = string.Empty;

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

        public int SubDistributorID
        {
            get
            {
                return subDistributorID;
            }
            set
            {
                subDistributorID = value;
            }
        }

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
                totalPoint = value;
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

        public string NoHP
        {
            get
            {
                return noHp;
            }
            set
            {
                noHp = value;
            }
        }

        public int Block
        {
            get
            {
                return block;
            }
            set
            {
                block = value;
            }
        }

        public int GetPoint
        {
            get
            {
                return getPoint;
            }
            set
            {
                getPoint = value;
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
