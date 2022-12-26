using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SPPPrivate : GRCBaseDomain
    {
        int iD = 0;
        int sPPID = 0;
        
        public int ID { get { return iD; } set { iD = value; } }
        public int SPPID { get { return sPPID; } set { sPPID = value; } }
    }
}
