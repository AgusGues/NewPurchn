using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
   public class AccClosing : GRCBaseDomain
    {
        private int tahun = 0;
        private int bulan = 0;
        private int status = 0;
        private string modul = string.Empty;

        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }
        public int Bulan
        {
            get { return bulan; }
            set { bulan = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string ModulName
        {
            set { modul = value; }
            get { return modul; }
        }
    }
 
}
