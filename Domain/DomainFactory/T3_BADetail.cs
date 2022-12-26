using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class T3_BADetail : GRCBaseDomain
    {
        public int BAID { get; set; }
        public string BANo { get; set; }
        public DateTime BADate { get; set; }
        public string BAType { get; set; }
        public int ItemID { get; set; }
        public string PartNo { get; set; }
        public string AdjustType { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public string Approval { get; set; }
        public int Apv { get; set; }
        public int LokID { get; set; }
        public string Keterangan { get; set; }
    }
}
