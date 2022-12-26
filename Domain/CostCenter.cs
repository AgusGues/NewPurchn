using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class CostCenter:Planning
    {
        public string GroupName { get; set; }
        public string DeptGroup { get; set; }
        public int MaterialCCID { get; set; }
        public int MaterialGroupID { get; set; }
        public int SortOrder { get; set; }
        public decimal CostBudget { get; set; }
        public decimal CostActual { get; set; }
        public decimal Prosen { get; set; }
        public int Kode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyBudget { get; set; }
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
    }
    public class CostPJ : CostCenter
    {
        public int Urutan { get; set; }
        public string Pengiriman { get; set; }
        public string JenisPacking { get; set; }
        public decimal PKA { get; set; }
        public decimal BKA { get; set; }
        public decimal BEY { get; set; }
        public decimal KPL { get; set; }
        public decimal KKR { get; set; }
        public decimal PPE { get; set; }
        public decimal Nilai { get; set; }
    }
    public class CostFIN : CostCenter
    {
        public int ID { get; set; }
        public int MatCCMatrixFinID { get; set; }
        public string PartNo { get; set; }
        public int PartNoID { get; set; }
        public string Lokasi { get; set; }
        public int LokasiID { get; set; }
        public decimal Barang { get; set; }
        public decimal Lembar { get; set; }
        public decimal RupiahPerBln { get; set; }
        public decimal Price { get; set; }
        public decimal ProdukOut { get; set; }
    }
}
