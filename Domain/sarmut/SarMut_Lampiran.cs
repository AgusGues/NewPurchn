using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SarMut_Lampiran : GRCBaseDomain
    {
        private int sarmut_ID = 0;
        private string fileName = string.Empty;

        public int Sarmut_ID { get { return sarmut_ID; } set { sarmut_ID = value; } }
        public string FileName { get { return fileName; } set { fileName = value; } }
    }
}
