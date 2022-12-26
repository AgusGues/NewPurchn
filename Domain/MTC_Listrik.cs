using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MTC_Listrik : GRCBaseDomain
    {
        private int id = 0;
        private DateTime tgl = DateTime.Now;
        private decimal kwhpln = 0;
        private decimal kvarhpln = 0;
        private decimal kwhpjt = 0;
        private decimal kvarhpjt = 0;
        private string keterangan = string.Empty;
        private int line = 0;
        private decimal result1 = 0;
        private decimal result2 = 0;
        private decimal result3 = 0;
        private decimal result4 = 0;
        private decimal totalkwh = 0;
        private decimal totalkvarh = 0;
        private decimal output = 0;
        private decimal kwhm3 = 0;
        private decimal prosentase = 0;


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

        public DateTime Tanggal
        {
            get
            {
                return tgl;
            }
            set
            {
                tgl = value;
            }
        }
        public decimal kWhPLN
        {
            get
            {
                return kwhpln;
            }
            set
            {
                kwhpln = value;
            }
        }
        public decimal kVarhPLN
        {
            get
            {
                return kvarhpln;
            }
            set
            {
                kvarhpln = value;
            }
        }
        public decimal kWhPJT
        {
            get
            {
                return kwhpjt;
            }
            set
            {
                kwhpjt = value;
            }
        }
        public decimal kVarhPJT
        {
            get
            {
                return kvarhpjt;
            }
            set
            {
                kvarhpjt = value;
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
        public int Line
        {
            get
            {
                return line;
            }
            set
            {
                line = value;
            }
        }
        public decimal Result1
        {
            get
            {
                return result1;
            }
            set
            {
                result1 = value;
            }
        }
        public decimal Result2
        {
            get
            {
                return result2;
            }
            set
            {
                result2 = value;
            }
        }
        public decimal Result3
        {
            get
            {
                return result3;
            }
            set
            {
                result3 = value;
            }
        }
        public decimal Result4
        {
            get
            {
                return result4;
            }
            set
            {
                result4 = value;
            }
        }

        public decimal TotalkWh
        {
            get
            {
                return totalkwh;
            }
            set
            {
                totalkwh = value;
            }
        }
        public decimal TotalkVarh
        {
            get
            {
                return totalkvarh;
            }
            set
            {
                totalkvarh = value;
            }
        }
        public decimal Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
            }
        }
        public decimal kWhM3
        {
            get
            {
                return kwhm3;
            }
            set
            {
                kwhm3 = value;
            }
        }
        public decimal Prosentase
        {
            get
            {
                return prosentase;
            }
            set
            {
                prosentase = value;
            }
        }
    }
}
