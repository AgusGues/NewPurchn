using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccClosingPeriode : GRCBaseDomain
    {
        private int tahun = 0;
        private int bulan = 0;
        private int status = 0;
        private string modul = string.Empty;
        private int id = 0;
        private DateTime dariTgl = DateTime.MinValue;
        private DateTime sampaiTgl = DateTime.MinValue;

        public DateTime DariTgl
        {
            get { return dariTgl; }
            set { dariTgl = value; }
        }

        public DateTime SampaiTgl
        {
            get { return sampaiTgl; }
            set { sampaiTgl = value; }
        }

        //public int ID
        //{
        //    get { return id; }
        //    set { id = value; }
        //}

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
