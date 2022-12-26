using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Company : GRCBaseDomain
    {
        private int companyID = 0;
        private string kodeLokasi = string.Empty;
        private string kodePT = string.Empty;
        private int depoID = 0;
        private string lokasi = string.Empty;
        private string nama = string.Empty;
        private string alamat1 = string.Empty;
        private string alamat2 = string.Empty;
        private string manager = string.Empty;
        private string spv = string.Empty;


        public int CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }
        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public string KodeLokasi
        {
            get
            {
                return kodeLokasi;
            }
            set
            {
                kodeLokasi = value;
            }
        }
        public string KodePT
        {
            get
            {
                return kodePT;
            }
            set
            {
                kodePT = value;
            }
        }
        public string Lokasi
        {
            get
            {
                return lokasi;
            }
            set
            {
                lokasi = value;
            }
        }

        public string Nama
        {
            get
            {
                return nama;
            }
            set
            {
                nama = value;
            }
        }

        public string Alamat1
        {
            get
            {
                return alamat1;
            }
            set
            {
                alamat1 = value;
            }
        }

        public string Alamat2
        {
            get
            {
                return alamat2;
            }
            set
            {
                alamat2 = value;
            }
        }

        public string Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
            }
        }

        public string Spv
        {
            get
            {
                return spv;
            }
            set
            {
                spv = value;
            }
        }
        public string NoDocument { get; set; }
        public string DocName { get; set; }


    }
}
