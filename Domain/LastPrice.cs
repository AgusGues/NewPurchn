using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LastPrice : GRCBaseDomain
    {
        private decimal price=0;
        private int crc=0;

        public decimal Price
        {
            get { return price; } 
            set { price = value; }
        }
        public int Crc
        {
            get { return crc; }
            set { crc = value; }
        }
    }
}
