using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Sisi : GRCBaseDomain
    {
        private string sisiDescription = string.Empty;

        public string SisiDescription
        {
            get
            {
                return sisiDescription;
            }
            set
            {
                sisiDescription = value;
            }
        }
    }
}
