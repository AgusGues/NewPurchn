using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Fabrikasi : GRCBaseDomain
    {
        private int tahun = 0;
        private int bulan = 0;
        private decimal jumlah = 0;
        private int rowstatus = 0;

        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }
        public int Bulan
        {
            get { return bulan; }
            set { bulan = value; }
        }
        public decimal Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }
        public int RowStatus
        {
            get { return rowstatus; }
            set { rowstatus = value; }
        }
    }
}
