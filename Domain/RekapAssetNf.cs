using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RekapAssetNf
    {
        public class ParamData
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Unit { get; set; }
            public decimal SaldoAwal { get; set; }
            public decimal Pembelian { get; set; }
            public decimal AdjustIn { get; set; }
            public decimal AdjustOut { get; set; }
            public decimal SaldoAkhir { get; set; }
            public decimal Spb { get; set; }
            public decimal StockGudang { get; set; }
            public string Kategori { get; set; }
        }
    }
}
