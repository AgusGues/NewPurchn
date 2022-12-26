using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterKursPIB : GRCBaseDomain 
    {
        private int id = 0;
        private int mkid = 0;
        private decimal kurs = 0;
        private DateTime drTgl = DateTime.Today;
        private DateTime sdTgl = DateTime.Today;
        private int typekurs = 0;
        private int rowstatus = 0;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public decimal Kurs
        {
            get { return kurs; }
            set { kurs = value; }
        }
        public int MKId
        {
            get { return mkid; }
            set { mkid = value; }
        }
        public DateTime DrTgl
        {
            get { return drTgl; }
            set { drTgl = value; }
        }
        public DateTime SdTgl
        {
            get { return sdTgl; }
            set { sdTgl = value; }
        }

        public int TypeKurs
        {
            get { return typekurs; }
            set { typekurs = value; }
        }

        public int Rowstatus
        {
            get { return rowstatus; }
            set { rowstatus = value; }
        }
    }
}
