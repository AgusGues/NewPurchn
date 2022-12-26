using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Lap : Task
    {
        public string IsoName { get; set; }
        public string UserName { get; set; }
        public decimal TotalBBT { get; set; }
        public decimal TotalSCR { get; set; }
        public string PemberiTask { get; set; }
        public decimal BobotPES { get; set; }
        public decimal MinBobot { get; set; }
    }
}
