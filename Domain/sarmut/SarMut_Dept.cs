using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SarMut_Dept : GRCBaseDomain
    {
        private string kode = string.Empty;
        private string departemen = string.Empty;

        public string Kode { get { return kode; } set { kode = value; } }
        public string Departemen { get { return departemen; } set { departemen = value; } }
    }
}
