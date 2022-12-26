using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Ex_Route : GRCBaseDomain
    {      
        private int depoID = 0;
        private int cityID = 0;
        private int kabupatenID = 0;
        private int typeRoute = 0;
        private int kilometer = 0;
        private int rasio = 0;
        private int literBBM = 0;
        private int totalBBM = 0;
        private int tol = 0;
        private int mel = 0;
        private int parkir = 0;
        private int other = 0;
        private int uangJalan = 0;

        private string namaPropinsi = string.Empty;
        private string depoName = string.Empty;
        private string cityName = string.Empty;
        private string kabupatenName = string.Empty;
        private string ketRoute = string.Empty;

        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }

        public int CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        public int KabupatenID
        {
            get { return kabupatenID; }
            set { kabupatenID = value; }
        }

        public int TypeRoute
        {
            get { return typeRoute; }
            set { typeRoute = value; }
        }

        public int Kilometer
        {
            get { return kilometer; }
            set { kilometer = value; }
        }

        public int Rasio
        {
            get { return rasio; }
            set { rasio = value; }
        }

        public int LiterBBM
        {
            get { return literBBM; }
            set { literBBM = value; }
        }

        public int TotalBBM
        {
            get { return totalBBM; }
            set { totalBBM = value; }
        }

        public int Tol
        {
            get { return tol; }
            set { tol = value; }
        }

        public int Mel
        {
            get { return mel; }
            set { mel = value; }
        }

        public int Parkir
        {
            get { return parkir; }
            set { parkir = value; }
        }

        public int Other
        {
            get { return other; }
            set { other = value; }
        }

        public int UangJalan
        {
            get { return uangJalan; }
            set { uangJalan = value; }
        }

        public string DepoName
        {
            get { return depoName; }
            set { depoName = value; }
        }

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public string KabupatenName
        {
            get { return kabupatenName; }
            set { kabupatenName = value; }
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

        public string KetRoute
        {
            get
            {
                return ketRoute;
            }
            set
            {
                ketRoute = value;
            }
        }

    }
}
