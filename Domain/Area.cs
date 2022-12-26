using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Area : GRCBaseDomain
    {
        private string areaName = string.Empty;

        public string AreaName
        {
            get
            {
                return areaName;
            }
            set
            {
                areaName = value;
            }
        }
    }
}
