using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_TargetBudget : GRCBaseDomain
    {
        private int tahun = 0;
        private string smt = string.Empty;
        private decimal jumlah = 0;
        private int nom = 0;

        public int Tahun
        {
            set { tahun = value; }
            get { return tahun; }
        }
        public string Smt
        {
            set { smt = value; }
            get { return smt; }
        }
        public decimal Jumlah
        {
            set { jumlah = value; }
            get { return jumlah; }
        }
        public int Nom
        {
            set { nom = value; }
            get { return nom; }
        }
    }
}
