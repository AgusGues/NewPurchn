using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class T3_BA : GRCBaseDomain
    {
        public DateTime BADate { get; set; }
        public string BANo { get; set; }
        public string Keterangan { get; set; }
        public int Approval { get; set; }
        public DateTime ApvDate1 { get; set; }
        public DateTime ApvDate2 { get; set; }
        public DateTime ApvDate3 { get; set; }
        public DateTime ApvDate4 { get; set; }
        public DateTime ApvDate5 { get; set; }
    }
}
