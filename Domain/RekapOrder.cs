using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RekapOrder:GRCBaseDomain
    {
        public virtual string ItemCode { get; set; }
        public virtual string ItemName { get; set; }
        public virtual string UOMName { get; set; }
        public virtual decimal MinStock { get; set; }
        public virtual decimal ReOrder { get; set; }
        public virtual decimal Stock { get; set; }
        public virtual decimal OtSPP { get; set; }
        public virtual decimal OtPO { get; set; }
        public virtual string Remark { get; set; }
    }
}
