using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Kabupaten : GRCBaseDomain
    { 
        private string namaKabupaten = string.Empty;
        private int cityID = 0;
        private string cityName = string.Empty;
        private int propinsiID = 0;
        private string namaPropinsi = string.Empty;

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
    }
}
