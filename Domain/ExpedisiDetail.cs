using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ExpedisiDetail
    {
        private int id = 0;
        private int expedisiID = 0;
        private string carType = string.Empty;
        private decimal kubikasi = 0;
        private string noPolisi = string.Empty;
        private decimal minimalMuatan = 0;
        private int expedisiDetailID = 0;
        private int depoID = 0;

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }
        public int ExpedisiDetailID
        {
            get
            {
                return expedisiDetailID;
            }
            set
            {
                expedisiDetailID = value;
            }
        }
        public decimal MinimalMuatan
        {
            get
            {
                return minimalMuatan;
            }
            set
            {
                minimalMuatan = value;
            }
        }
        public string NoPolisi
        {
            get
            {
                return noPolisi;
            }
            set
            {
                noPolisi = value;
            }
        }

        public int ID
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

        public int ExpedisiID
        {
            get
            {
                return expedisiID;
            }
            set
            {
                expedisiID = value;
            }

        }

        public string CarType
        {
            get
            {
                return carType;
            }
            set
            {
                carType = value;
            }
        }

        public decimal Kubikasi
        {
            get
            {
                return kubikasi;
            }
            set
            {
                kubikasi = value;
            }
        }
    }
}
