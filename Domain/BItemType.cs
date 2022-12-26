using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BItemType : GRCBaseDomain
    {
        string inCode = string.Empty;
        string inType = string.Empty;
        string kode = string.Empty;

        public string Kode
        { get { return kode; } set { kode = value; } }
        public string InType
        { get { return inType; } set { inType = value; } }
        public string InCode
        { get { return inCode; } set { inCode = value; } }
    }
}
