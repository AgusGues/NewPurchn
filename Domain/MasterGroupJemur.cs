using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    
    public class MasterGroupJemur : GRCBaseDomain
    {
        private string groupJemurCode = string.Empty;
        private string groupJemurName = string.Empty;
        


        public string GroupJemurCode
        {
            get { return groupJemurCode; }
            set { groupJemurCode = value; }
        }
        public string GroupJemurName
        {
            get { return groupJemurName; }
            set { groupJemurName = value; }
        }

       


    }
}
