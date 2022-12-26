using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Analisa_Penyebab : GRCBaseDomain
    {
        private int tPP_ID = 0;
        private int penyebab_ID = 0;

        public int TPP_ID { get { return tPP_ID; } set { tPP_ID = value; } }
        public int Penyebab_ID { get { return penyebab_ID; } set { penyebab_ID = value; } }
    }
}
