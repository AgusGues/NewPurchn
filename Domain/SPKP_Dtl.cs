using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SPKP_Dtl : GRCBaseDomain
    {
        private string nospkp = string.Empty;
        private int no;

        public string NoSpkp {
            get { return nospkp; }
            set { nospkp = value; }
        }
        public int No
        {
            get { return no; }
            set { no = value; }
        }
        public class ddl1
        {
            public string Kategori { get; set; }
        }
        public class ddl2
        {
            public decimal Tebal { get; set; }
        }
        public class ddl3
        {
            public string Ukuran { get; set; }
        }
        public class insert_dtl
        {
            public int id { get; set; }
            public int spkpid { get; set; }
            public string NoSpkp { get; set; }
            public string Tanggal { get; set; }
            public int Shift { get; set; }
            public int Line { get; set; }
            public string Ukuran { get; set; }
            public string Kategori { get; set; }
            public string Tebal { get; set; }
            public int Target { get; set; }
            public string CreatedBy { get; set; }
            public string Keterangan { get; set; }
        }

        public class d_input
        {
            public List<insert_dtl> data { get; set; }
        }
    }
}
