using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterUkuran : GRCBaseDomain
    {
        //private int itemID = 0;
        private decimal lebar = 0;
        private decimal panjang = 0;
        private int bagi = 0;
        private string description = string.Empty;

        public decimal Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }
        public decimal Panjang
        {
            get { return panjang; }
            set { panjang = value; }
        }
        public int Bagi
        {
            get { return bagi; }
            set { bagi = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
