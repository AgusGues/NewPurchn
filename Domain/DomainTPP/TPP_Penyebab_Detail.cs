using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TPP_Penyebab_Detail : GRCBaseDomain
    {
        private string uraian = string.Empty;
        private int tPP_ID = 0;
        private int penyebab_ID = 0;
        private int rowstatus = 0;
        private string penyebab = string.Empty;

        public string Penyebab { get { return penyebab; } set { penyebab = value; } }
        public string Uraian { get { return uraian; } set { uraian = value; } }
        public int TPP_ID { get { return tPP_ID; } set { tPP_ID = value; } }
        public int Rowstatus { get { return rowstatus; } set { rowstatus = value; } }
        public int Penyebab_ID { get { return penyebab_ID; } set { penyebab_ID = value; } }
    }
}
