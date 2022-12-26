using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM_Loc : GRCBaseDomain
    {
        private int sarDeptID = 0;
        private string loc = string.Empty;


        public int SarDeptID { get { return sarDeptID; } set { sarDeptID = value; } }
        public string Loc { get { return loc; } set { loc = value; } }
    }
}
