using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReturnBJ : GRCBaseDomain
    {
        public virtual DateTime Tanggal { get; set; }
        public virtual string SJNo { get; set; }
        public virtual string OPNo { get; set; }
        public virtual string Customer { get; set; }
        public virtual string PartNo { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string typeR { get; set; }
    }
}
