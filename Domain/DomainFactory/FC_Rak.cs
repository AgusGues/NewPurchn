using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class FC_Rak : GRCBaseDomain
    {
        private int iD = 0;
        private int status = 0;
        private string rak = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Rak
        {
            get { return rak; }
            set { rak = value; }
        }
    }
}
