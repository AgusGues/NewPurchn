using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HariLibur : GRCBaseDomain
    {
        private DateTime tglLibur = DateTime.Now.Date;
        private string keterangan = string.Empty;

        public DateTime TglLibur
        {
            get
            {
                return tglLibur;
            }
            set
            {
                tglLibur = value;
            }
        }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }
    }

        
}
