using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RekapWarningOrderNf
    {
        public class ParamTypeItem
        {
            public int ID { get; set; }
            public string GroupDescription { get; set; }
        }

        public class ParamData
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public decimal StockAwal { get; set; }
            public decimal Pemasukan { get; set; }
            public decimal Retur { get; set; }
            public decimal AdjustTambah { get; set; }
            public decimal AdjustKurang { get; set; }
            public decimal Pemakaian { get; set; }
            public decimal EndingStok { get; set; }
            public int GroupId { get; set; }
            public decimal MinStock { get; set; }
            public decimal ReOrder { get; set; }
        }
    }
}
