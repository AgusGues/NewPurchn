using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Asal_Masalah : GRCBaseDomain
    {
        private string asal_Masalah = string.Empty;
        public string Asal_Masalah { get { return asal_Masalah; } set { asal_Masalah = value; } }
    }
}
