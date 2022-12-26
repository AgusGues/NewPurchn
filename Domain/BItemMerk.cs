using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BItemMerk : GRCBaseDomain
    {
        string inCode = string.Empty;
        string inMerk = string.Empty;
        string kode = string.Empty;

        public string Kode
        { get { return kode; } set { kode = value; } }
        public string InMerk
        { get { return inMerk; } set { inMerk = value; } }
        public string InCode
        { get { return inCode; } set { inCode = value; } }
    }
}
