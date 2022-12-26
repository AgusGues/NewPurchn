using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class AreaDistribusi : GRCBaseDomain
    {
        private string areaDistribusiName = string.Empty;

        public string AreaDistribusiName
        {
            get
            {
                return areaDistribusiName;
            }
            set
            {
                areaDistribusiName = value;
            }
        }
    }
}
