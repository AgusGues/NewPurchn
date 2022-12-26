using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class UOM : GRCBaseDomain
    {
        private string uomCode = string.Empty;
        private string uomDesc = string.Empty;

        public string UOMCode
        {
            get
            {
                return uomCode;
            }
            set
            {
                uomCode = value;
            }
        }

        public string UOMDesc
        {
            get
            {
                return uomDesc;
            }
            set
            {
                uomDesc = value;
            }
        }
    }
}
