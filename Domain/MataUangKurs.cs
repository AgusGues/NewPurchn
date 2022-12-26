using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MataUangKurs : GRCBaseDomain 
    {
        private int id = 0;
        private int muid = 0;
        private int typeKurs = 0;
        private decimal kurs = 0;
        private DateTime drTgl = DateTime.Today;
        private DateTime sdTgl = DateTime.Today;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int TypeKurs
        {
            get { return typeKurs; }
            set { typeKurs = value; }
        }
        public decimal Kurs
        {
            get { return kurs; }
            set { kurs = value; }
        }
        public int MuId
        {
            get { return muid; }
            set { muid = value; }
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

    }
}
