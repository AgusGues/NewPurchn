using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class City : GRCBaseDomain
    {
        private int propinsiID = 0;
        private string namaPropinsi = string.Empty;
        private string cityName = string.Empty;
        private int areaDistribusiID = 0;
        private string areaDistribusiName = string.Empty;

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

        public int AreaDistribusiID
        {
            get
            {
                return areaDistribusiID;
            }
            set
            {
                areaDistribusiID = value;
            }
        }

        public string AreaDistribusiName
        {
            get
            {
                return areaDistribusiName;
            }
            set
            {
                areaDistribusiName = value;
            }
    
        }

    }
}
