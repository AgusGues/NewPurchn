using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PbahanBaku : GRCBaseDomain
    {
        private DateTime tgl_Prod;
        private string tgl_Prod2 = string.Empty;
        private decimal l1g1 = 0;
        private decimal l1g12 = 0;
        private decimal l2g1 = 0;
        private decimal l2g12 = 0;
        private decimal l3g1 = 0;
        private decimal l3g12 = 0;
        private decimal l4g1 = 0;
        private decimal l4g12 = 0;
        private decimal l5g1 = 0;
        private decimal l5g12 = 0;
        private decimal l6g1 = 0;
        private decimal l6g12 = 0;
        //=======================
        private decimal tmix = 0;
        private decimal formula = 0;
        //=========================
        private decimal actual = 0;
        private decimal skg = 0;
        private decimal skgmix = 0;
        //------------------------
        private decimal efis = 0;
        //--------------------------
        private string jkertas = string.Empty;

        //New Add Ver-2 by Razib -- 25-03-2019
        private decimal l1 = 0;
        private decimal l1x = 0;
        private decimal l2 = 0;
        private decimal l2x = 0;
        private decimal l3 = 0;
        private decimal l3x = 0;
        private decimal l4 = 0;
        private decimal l4x = 0;
        private decimal l5 = 0;
        private decimal l5x = 0;
        private decimal l6 = 0;
        private decimal l6x = 0;
        private decimal sdL = 0;
        private decimal sspB = 0;
        private decimal aT = 0;
        private decimal kL = 0;
        private string jKertasVirgin = string.Empty;
        private decimal kvKg = 0;
        private decimal kvKg2 = 0;
        private decimal kvEfis = 0;
        private decimal ksKgBima = 0;
        private decimal bkBimaKg = 0;
        private decimal sampahBima = 0;
        private decimal bbBimaKg = 0;
        private decimal ksKg0 = 0;
        private decimal ksKg = 0;
        private decimal ksEfis = 0;
        private decimal kuKg = 0;
        private decimal kuEfs = 0;
        private decimal kgrade2Kg = 0;
        private decimal kggrade2Eff = 0;
        private decimal kgrade3Kg = 0;
        private decimal kggrade3Eff = 0;
        private decimal kkKg = 0;
        private decimal kkEfis = 0;
        private decimal kdKg = 0;
        private decimal kdEfis = 0;
        private decimal kbtKg = 0;
        private decimal kbtKg2 = 0;
        private decimal kbtEfis = 0;
        private decimal kdLKaKg = 0;
        private decimal kdLkaKg2 = 0;
        private decimal kdLkaEfis = 0;
        private decimal tEfis = 0;
        private decimal kv = 0;
        private decimal kb = 0;
        private decimal ks = 0;
        private decimal kgradeUtama = 0;
        private decimal kgrade2 = 0;
        private decimal kgrade3 = 0;
        private decimal kk = 0;
        private decimal kbt = 0;
        private decimal kdlka = 0;
        private decimal fre = 0;
        private decimal pks = 0;
        private decimal bbD = 0;
        private decimal wcD = 0;
        private decimal bkD = 0;
        private decimal skK = 0;
        private decimal skKPerc = 0;
        private decimal tSpb = 0;
        private decimal tPemakaian = 0;

        public decimal L1 { set { l1 = value; } get { return l1; } }
        public decimal L1x { set { l1x = value; } get { return l1x; } }
        public decimal L2 { set { l2 = value; } get { return l2; } }
        public decimal L2x { set { l2x = value; } get { return l2x; } }
        public decimal L3 { set { l3 = value; } get { return l3; } }
        public decimal L3x { set { l3x = value; } get { return l3x; } }
        public decimal L4 { set { l4 = value; } get { return l4; } }
        public decimal L4x { set { l4x = value; } get { return l4x; } }
        public decimal L5 { set { l5 = value; } get { return l5; } }
        public decimal L5x { set { l5x = value; } get { return l5x; } }
        public decimal L6 { set { l6 = value; } get { return l6; } }
        public decimal L6x { set { l6x = value; } get { return l6x; } }
        public decimal SdL { set { sdL = value; } get { return sdL; } }
        public decimal SspB { set { sspB = value; } get { return sspB; } }
        public decimal AT { set { aT = value; } get { return aT; } }
        public decimal KL { set { kL = value; } get { return kL; } }
        public decimal TSpb { set { tSpb = value; } get { return tSpb; } }
        public decimal TPemakaian { set { tPemakaian = value; } get { return tPemakaian; } }
        public string JKertasVirgin { set { jKertasVirgin = value; } get { return jKertasVirgin; } }
        public decimal KvKg { set { kvKg = value; } get { return kvKg; } }
        public decimal KvKg2 { set { kvKg2 = value; } get { return kvKg2; } }
        public decimal KvEfis { set { kvEfis = value; } get { return kvEfis; } }
        public decimal KsKgBima { set { ksKgBima = value; } get { return ksKgBima; } }
        public decimal BkBimaKg { set { bkBimaKg = value; } get { return bkBimaKg; } }
        public decimal SampahBima { set { sampahBima = value; } get { return sampahBima; } }
        public decimal BbBimaKg { set { bbBimaKg = value; } get { return bbBimaKg; } }
        public decimal KsKg0 { set { ksKg0 = value; } get { return ksKg0; } }
        public decimal KsKg { set { ksKg = value; } get { return ksKg; } }
        public decimal KsEfis { set { ksEfis = value; } get { return ksEfis; } }
        public decimal KuKg { set { kuKg = value; } get { return kuKg; } }
        public decimal KuEfs { set { kuEfs = value; } get { return kuEfs; } }
        public decimal Kgrade2Kg { set { kgrade2Kg = value; } get { return kgrade2Kg; } }
        public decimal Kgrade2Eff { set { kggrade2Eff = value; } get { return kggrade2Eff; } }
        public decimal Kgrade3Kg { set { kgrade3Kg = value; } get { return kgrade3Kg; } }
        public decimal Kgrade3Eff { set { kggrade3Eff = value; } get { return kggrade3Eff; } }
        public decimal KkKg { set { kkKg = value; } get { return kkKg; } }
        public decimal KkEfis { set { kkEfis = value; } get { return kkEfis; } }
        public decimal KdKg { set { kdKg = value; } get { return kdKg; } }
        public decimal KdEfis { set { kdEfis = value; } get { return kdEfis; } }
        public decimal KbtKg { set { kbtKg = value; } get { return kbtKg; } }
        public decimal KbtKg2 { set { kbtKg2 = value; } get { return kbtKg2; } }
        public decimal KbtEfis { set { kbtEfis = value; } get { return kbtEfis; } }
        public decimal KdLKaKg { set { kdLKaKg = value; } get { return kdLKaKg; } }
        public decimal KdLKaKg2 { set { kdLkaKg2 = value; } get { return kdLkaKg2; } }
        public decimal KdLKaEfis { set { kdLkaEfis = value; } get { return kdLkaEfis; } }
        public decimal TEfis { set { tEfis = value; } get { return tEfis; } }
        public decimal KV { set { kv = value; } get { return kv; } }
        public decimal KB { set { kb = value; } get { return kb; } }
        public decimal KGradeUtama { set { kgradeUtama = value; } get { return kgradeUtama; } }
        public decimal Kgrade2 { set { kgrade2 = value; } get { return kgrade2; } }
        public decimal Kgrade3 { set { kgrade3 = value; } get { return kgrade3; } }
        public decimal KS { set { ks = value; } get { return ks; } }
        public decimal KK { set { kk = value; } get { return kk; } }
        public decimal Kbt { set { kbt = value; } get { return kbt; } }
        public decimal Kdlka { set { kdlka = value; } get { return kdlka; } }
        public decimal Fre { set { fre = value; } get { return fre; } }
        public decimal Pks { set { pks = value; } get { return pks; } }
        public decimal BbD { set { bbD = value; } get { return bbD; } }
        public decimal WcD { set { wcD = value; } get { return wcD; } }
        public decimal BkD { set { bkD = value; } get { return bkD; } }
        public decimal SkK { set { skK = value; } get { return skK; } }
        public decimal SkKPerc { set { skKPerc = value; } get { return skKPerc; } }

        //--------------------------------------------------------------------------

        public DateTime Tgl_Prod { set { tgl_Prod = value; } get { return tgl_Prod; } }
        public string Tgl_Prod2 { set { tgl_Prod2 = value; } get { return tgl_Prod2; } }
        public decimal L1G1 { set { l1g1 = value; } get { return l1g1; } }
        public decimal L1G12 { set { l1g12 = value; } get { return l1g12; } }
        public decimal L2G1 { set { l2g1 = value; } get { return l2g1; } }
        public decimal L2G12 { set { l2g12 = value; } get { return l2g12; } }
        public decimal L3G1 { set { l3g1 = value; } get { return l3g1; } }
        public decimal L3G12 { set { l3g12 = value; } get { return l3g12; } }
        public decimal L4G1 { set { l4g1 = value; } get { return l4g1; } }
        public decimal L4G12 { set { l4g12 = value; } get { return l4g12; } }
        public decimal L5G1 { set { l5g1 = value; } get { return l5g1; } }
        public decimal L5G12 { set { l5g12 = value; } get { return l5g12; } }
        public decimal L6G1 { set { l6g1 = value; } get { return l6g1; } }
        public decimal L6G12 { set { l6g12 = value; } get { return l6g12; } }
        public decimal TMix { set { tmix = value; } get { return tmix; } }
        public decimal Formula { set { formula = value; } get { return formula; } }
        public decimal Actual { set { actual = value; } get { return actual; } }
        public decimal sKg { set { skg = value; } get { return skg; } }
        public decimal sKgMix { set { skgmix = value; } get { return skgmix; } }
        public decimal Efis { set { efis = value; } get { return efis; } }
        public string JKertas { set { jkertas = value; } get { return jkertas; } }
    }
}
