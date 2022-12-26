using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Groups : GRCBaseDomain
    {
        private int iD = 0;
        private string groups = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string  Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        public class Jenis
        {
            public int ID { get; set; }
            public string Groups { get; set; }

        }
        
    }
}
