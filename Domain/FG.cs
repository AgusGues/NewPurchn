using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class FG:GRCBaseDomain
    {
        
    }
    public class SP : GRCBaseDomain
    {
        public virtual int ItemID { get; set; }
        public virtual int RakID { get; set; }
        public virtual int DestID { get; set; }
        public virtual int PaletID { get; set; }
        public virtual decimal Tebal { get; set; }
        public virtual int Lebar { get; set; }
        public virtual int Panjang { get; set; }
        public virtual string PartNo { get; set; }
        public virtual string RakNo { get; set; }
        public virtual string PaletNo { get; set; }
        public virtual DateTime TglJemur { get; set; }
        public virtual DateTime TglProduksi { get; set; }
        public virtual int QtyIn { get; set; }
        public virtual int Qty { get; set; }
        public virtual int UmurJemur { get; set; }

    }
}
