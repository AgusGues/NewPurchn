using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Depo :GRCBaseDomain
    {
        private string depoCode = string.Empty;
        private string depoName = string.Empty;
        private string address = string.Empty;        
        private string namaPropinsi = string.Empty;
        private int cityID = 0;
        private string cityName = string.Empty;
        private int kabupatenID = 0;
        private string namaKabupaten = string.Empty;
        private string initialToko = string.Empty;
        private string initialToko2 = string.Empty;


        public string DepoCode
        {
            get
            {
                return depoCode;
            }
            set
            {
                depoCode = value;
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

        public string InitialToko
        {
            get
            {
                return initialToko;
            }
            set
            {
                initialToko = value;
            }
        }

        public string InitialToko2
        {
            get
            {
                return initialToko2;
            }
            set
            {
                initialToko2 = value;
            }

        }
    }
}
