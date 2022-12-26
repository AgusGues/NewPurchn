using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Planning : GRCBaseDomain
    {
        public string Periode { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public string RunningLine { get; set; }
        public int Revision { get; set; }
        public int Approval { get; set; }
        public string Keterangan { get; set; }
        public int UnitKerjaID { get; set; }
        public string ApprovalBy { get; set; }
        public DateTime ApprovalTime { get; set; }
    }
}
