using System;

namespace Domain
{
    public class LapMutasiWIP
    {
        public string NoDocument { get; set; }
        public int AwalQty { get; set; }
        public int InProdQty { get; set; }
        public int InP99 { get; set; }
        public int InAdjustQty { get; set; }
        public int InI99 { get; set; }
        public int OutAdjustQty { get; set; }
        public int H99 { get; set; }
        public int I99 { get; set; }
        public int B99 { get; set; }
        public int C99 { get; set; }
        public int Q99 { get; set; }
        public int OutP99 { get; set; }
        public decimal Volume { get; set; }
        public decimal Tebal { get; set; }
        public int AkhirQty { get; set; }
        public decimal M3 { get; set; }
        public decimal M3Produksi { get; set; }
    }

    public class LapMutasiBJ
    {
        public string Partno { get; set; }
        public int TWIP { get; set; }
        public int HTWIP { get; set; }
        public int TBP { get; set; }
        public int HTBP { get; set; }
        public int TBJ { get; set; }
        public int HTBJ { get; set; }
        public int TBS { get; set; }
        public int HTBS { get; set; }
        public int TRETUR { get; set; }
        public int HTRETUR { get; set; }
        public int TAdjust { get; set; }
        public int HTAdjust { get; set; }
        public int KKirim { get; set; }
        public int HKKirim { get; set; }
        public int KBP { get; set; }
        public int HKBP { get; set; }
        public int KBJ { get; set; }
        public int HKBJ { get; set; }
        public int KSample { get; set; }
        public int KBS { get; set; }
        public int HKBS { get; set; }
        public int KAdjust { get; set; }
        public int HKAdjust { get; set; }
        public int Awal { get; set; }
        public int Total { get; set; }
        public decimal Volume { get; set; }
    }

    public class LapAdjustment
    {
        public DateTime AdjustDate { get; set; }
        public string AdjustNo { get; set; }
        public string Keterangan { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int AdjustIn { get; set; }
        public int AdjustOut { get; set; }
        public string Unit { get; set; }
        public string DateAdjust
        {
            get { return AdjustDate.ToShortDateString(); }
            set { AdjustDate.ToShortDateString(); }
        }
    }

    public class LapMutasiBB
    {
        public string ItemID { get; set; }
        public decimal SaldoAwalQty { get; set; }
        public decimal SaldoAwalHS { get; set; }
        public decimal SaldoAwalAMT { get; set; }
        public decimal BeliQty { get; set; }
        public decimal BeliHS { get; set; }
        public decimal BeliAMT { get; set; }
        public decimal AdjustQty { get; set; }
        public decimal AdjustHS { get; set; }
        public decimal AdjustAMT { get; set; }
        public decimal ProdQty { get; set; }
        public decimal ProdHS { get; set; }
        public decimal ProdAMT { get; set; }
        public decimal AdjProdQty { get; set; }
        public decimal AdjProdHS { get; set; }
        public decimal AdjProdAMT { get; set; }
        public decimal ReturnQty { get; set; }
        public decimal ReturnHS { get; set; }
        public decimal ReturnAMT { get; set; }
        public decimal RetSupQty { get; set; }
        public decimal RetSupHS { get; set; }
        public decimal RetSupAMT { get; set; }
        public decimal SaldoAkhirQty { get; set; }
        public decimal SaldoAkhirHS { get; set; }
        public decimal SaldoAkhirAMT { get; set; }
        public string Tanggal { get; set; }
        public string DocNo { get; set; }
    }

    public class TipeSPP
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string GroupCOde { get; set; }
        public string GroupDescription { get; set; }
        public int ItemTypeID { get; set; }
    }
}