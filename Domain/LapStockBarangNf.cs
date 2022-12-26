using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LapStockBarangNf
    {
        public class ParamTypeItem
        {
            public int ID { get; set; }
            public string TypeDescription { get; set; }
        }
        public class ParamGroupItem
        {
            public int ID { get; set; }
            public string GroupDescription { get; set; }
        }

        public class ParamData
        {
            public string GroupDescription { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public string Jumlah { get; set; }
            public string StockGudang { get; set; }
            public string StockMax { get; set; }
            public string StockMin { get; set; }
            public string ReOrder { get; set; }
        }
    }
}
