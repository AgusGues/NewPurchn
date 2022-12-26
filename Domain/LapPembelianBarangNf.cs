using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LapPembelianBarangNf
    {
        public class ParamLaporanItem
        {
            public int ID { get; set; }
            public string GroupDescription { get; set; }
        }

        public class ParamData
        {
            public string ItemID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UOMCode { get; set; }
            public string QtySaldo { get; set; }
            public string HppSaldo { get; set; }
            public string TotSaldo { get; set; }
            public string QtyMasuk { get; set; }
            public string AvgHargaBeli { get; set; }
            public string AvgTotBeli { get; set; }
            public string QtyPakai { get; set; }
            public string HppSaldoPakai { get; set; }
            public string TotHppSaldoPakai { get; set; }
            public string QtyAdjustTambah { get; set; }
            public string HppSaldoAdjustTambah { get; set; }
            public string TotHppSaldoQtyAdjustTambah { get; set; }
            public string QtyAdjsutKurang { get; set; }
            public string QtyAdjustKurang { get; set; }
            public string HppSaldoAdjustKurang { get; set; }
            public string TotHppSaldoQtyAdjustKurang { get; set; }
            public string QtyRetur { get; set; }
            public string TotHppSaldoQtyRetur { get; set; }
            public string EndStock { get; set; }
            public string AvgPrice { get; set; }
            public string TotAvgPrice { get; set; }
            public string FakturPajak { get; set; }
        }
    }
}
