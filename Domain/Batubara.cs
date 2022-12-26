using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Batubara : GRCBaseDomain
    {
        private string tanggal = string.Empty;
        private decimal lembar = 0;
        private decimal m3 = 0;
        private decimal kg = 0;
        private decimal kgm3 = 0;
        private decimal qtyBatubara = 0;
        private int effisiensi = 0;
        private int pemakaian = 0;
        private int nom = 0;

        private string keterangan = string.Empty;
        private decimal jmllebar = 0;
        private decimal jmlm3 = 0;
        private decimal jmlbatubara = 0;
        private decimal jmlkgm3 = 0;

        public string Tanggal
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

        public decimal Lembar
        {
            get
            {
                return lembar;
            }

            set
            {
                lembar = value;
            }
        }

        public decimal M3
        {
            get
            {
                return m3;
            }

            set
            {
                m3 = value;
            }
        }

        public decimal Kg
        {
            get
            {
                return kg;
            }

            set
            {
                kg = value;
            }
        }

        public decimal Kgm3
        {
            get
            {
                return kgm3;
            }

            set
            {
                kgm3 = value;
            }
        }

        public decimal QtyBatubara
        {
            get
            {
                return qtyBatubara;
            }

            set
            {
                qtyBatubara = value;
            }
        }

        public int Effisiensi
        {
            get
            {
                return effisiensi;
            }

            set
            {
                effisiensi = value;
            }
        }

        public int Pemakaian
        {
            get
            {
                return pemakaian;
            }

            set
            {
                pemakaian = value;
            }
        }

        public int Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
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

        public decimal Jmllebar
        {
            get
            {
                return jmllebar;
            }

            set
            {
                jmllebar = value;
            }
        }

        public decimal Jmlm3
        {
            get
            {
                return jmlm3;
            }

            set
            {
                jmlm3 = value;
            }
        }

        public decimal Jmlbatubara
        {
            get
            {
                return jmlbatubara;
            }

            set
            {
                jmlbatubara = value;
            }
        }

        public decimal Jmlkgm3
        {
            get
            {
                return jmlkgm3;
            }

            set
            {
                jmlkgm3 = value;
            }
        }
    }
}
