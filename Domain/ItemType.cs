using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ItemType : GRCBaseDomain
    {
       
        private string typeDescription = string.Empty;
       
        public string TypeDescription
        {
            get
            {
                return typeDescription;
            }
            set
            {
                typeDescription = value;
            }
        }
    }
}
