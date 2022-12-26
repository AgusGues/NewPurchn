using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Penyebab : GRCBaseDomain
    {
        private string penyebab = string.Empty;

        public string Penyebab { get { return penyebab; } set { penyebab = value; } }
    }
}
