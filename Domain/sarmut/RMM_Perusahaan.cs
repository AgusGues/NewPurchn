using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM_Perusahaan : GRCBaseDomain
    {
        private string dimensi = string.Empty;
        private string sarMutPerusahaan = string.Empty;
        private int depoID = 0;
        private int urutan = 0;

        public string Dimensi { get { return dimensi; } set { dimensi = value; } }
        public string SarMutPerusahaan { get { return sarMutPerusahaan; } set { sarMutPerusahaan = value; } }
        public int DepoID { get { return depoID; } set { depoID = value; } }
        public int Urutan { get { return urutan; } set { urutan = value; } }
    }
}
