using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BreakBMCharge : GRCBaseDomain
    {
        private string lokasiCharge = string.Empty;

        public string LokasiCharge
        {
            get
            {
                return lokasiCharge;
            }
            set
            {
                lokasiCharge = value;
            }
        }
    }
}
