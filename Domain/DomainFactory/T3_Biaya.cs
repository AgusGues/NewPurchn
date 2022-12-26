using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Biaya : GRCBaseDomain
    {
        private int iD = 0;
        private int tahun = 0;
        private int bulan = 0;
        private string accName = string.Empty;
        private string cOA = string.Empty;
        private decimal biaya = 0;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
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
        public string AccName
        {
            get { return accName; }
            set { accName = value; }
        }

        public string COA
        {
            get { return cOA; }
            set { cOA = value; }
        }
        public decimal Biaya
        {
            get { return biaya; }
            set { biaya = value; }
        }
    }
}
