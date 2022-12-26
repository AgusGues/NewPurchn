using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ZonaArea : GRCBaseDomain
    {
        private int propinsiId = 0;
        private string namaPropinsi = string.Empty;
        private int cityId = 0;
        private string cityName = string.Empty;
        private int kabupatenId = 0;
        private string namaKabupaten = string.Empty;
        private int zonaID = 0;
        private string zonaCode = string.Empty;


        public int PropinsiID
        {
            get
            {
                return propinsiId;
            }
            set
            {
                propinsiId = value;
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
                return cityId;
            }
            set
            {
                cityId = value;
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
                return kabupatenId;
            }
            set
            {
                kabupatenId = value;
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
    }
}
