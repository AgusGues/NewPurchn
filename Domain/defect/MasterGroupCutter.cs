using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterGroupCutter : GRCBaseDomain
    {
        private string groupCutCode = string.Empty;
        private string groupCutName = string.Empty;
        //private int rowStatus = 
       
        public string GroupCutCode
        {get{return groupCutCode;}
         set{groupCutCode = value;}
        }
        public string GroupCutName
        {get{return groupCutName;}
         set{groupCutName = value;}
        }

      
    }
}
