using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BD_Forklift:GRCBaseDomain
    {
        private int id;
        private string forklift = string.Empty;
        private DateTime tanggal = DateTime.Now;
        private string start = string.Empty;
        private string finish = string.Empty;
        private int total;
        private string kendala = string.Empty;
        private string perbaikan = string.Empty;
        private string keterangan = string.Empty;
        public string users = string.Empty;
        
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Forklift
        {
            get
            {
                return forklift;
            }
            set
            {
                forklift = value;
            }
        }

        public DateTime Tanggal
        {
            get
            {
                return tanggal;
            }
            set
            {
                tanggal = value;
            }
        }
        public string Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }
        public string Finish
        {
            get
            {
                return finish;
            }
            set
            {
                finish = value;
            }
        }
        public int Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }
        public string Kendala
        {
            get
            {
                return kendala;
            }
            set
            {
                kendala = value;
            }
        }
        public string Perbaikan
        {
            get
            {
                return perbaikan;
            }
            set
            {
                perbaikan = value;
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
        public string Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }

        public class DProdForklift
        {
            public int id { get; set; }
            public string prodForklift { get; set; }
            public int Total { get; set; }
        }

        
    }
}
