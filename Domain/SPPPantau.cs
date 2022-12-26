using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SPPPantau : GRCBaseDomain
    {
        #region Pemantauan SPP
        private string tglspp;
        private string nospp = string.Empty;
        private string apv = string.Empty;
        private string tipespp = string.Empty;
        private string apvdate = string.Empty;
        private string itemcode = string.Empty;
        private string itemname = string.Empty;
        private string uom = string.Empty;
        private decimal qty = 0;
        private int nom = 0;
        private string selisih = string.Empty;
        private string leadTime = string.Empty;
        private decimal quantiy = 0;
        private decimal qtyPO = 0;
        private decimal qtySisa = 0;
        private string namaHead = string.Empty;
        private string prioritas = string.Empty;
        private string userAlias = string.Empty;
        private string deptName = string.Empty;
        private decimal hargaPo = 0;
        private decimal qtySPP = 0;
        private string purchnDate = string.Empty;
        private string nopemantauanPO = string.Empty;
        private string approvalPo = string.Empty;
        private string tglReceipt = string.Empty;
        private string noReceipt = string.Empty;
        private decimal qtyReceipt = 0;
        private decimal qtyPO2 = 0;
        public string UserAlias { get; set; }
        public string DeptName { get; set; }
        public decimal HargaPo { get; set; }
        public decimal QtySPP { get; set; }
        public string PurchnDate { get; set; }
        public string NopemantauanPO { get; set; }
        public string ApprovalPo { get; set; }
        public string TglReceipt { get; set; }
        public string NoReceipt { get; set; }
        public decimal QtyReceipt { get; set; }
        public decimal QtyPO2 { get; set; }

        //private DateTime createdTime = DateTime.Now;

        //public DateTime CreatedTime {set{cre
        public string Prioritas { set { prioritas = value; } get { return prioritas; } }
        public string NamaHead { set { namaHead = value; } get { return namaHead; } }
        public decimal QtySisa { set { qtySisa = value; } get { return qtySisa; } }
        public decimal QtyPO { set { qtyPO = value; } get { return qtyPO; } }
        public decimal Quantity { set { quantiy = value; } get { return quantiy; } }
        public string LeadTime { set { leadTime = value; } get { return leadTime; } }
        public string Selisih { set { selisih = value; } get { return selisih; } }
        public int Nom { set { nom = value; } get { return nom; } }
        public string TglSPP{set { tglspp = value; }get { return tglspp; }}
        public string NoSPP{set { nospp = value;}get { return nospp; }}
        public string ApvStatus{ set { apv = value; } get { return apv; }}
        public string TipeSPP{ set { tipespp = value; } get { return tipespp; }}
        public string ApvDate { set { apvdate = value; } get { return apvdate; } }
        public string ItemCode { set { itemcode = value; } get { return itemcode; } }
        public string ItemName { set { itemname = value; } get { return itemname; } }
        public string Uom { set { uom = value; } get { return uom; } }
        public decimal Qty { set { qty = value; } get { return qty; } }
        #endregion
        #region Pemantauan PO
            private string tglpo = string.Empty;
            private string nopo = string.Empty;
            private string dlvdate = string.Empty;

            public string TglPO { set { tglpo = value; } get { return tglpo; } }
            public string NoPO { set { nopo = value; } get { return nopo; } }
            public string DlvDate { set { dlvdate = value; } get { return dlvdate; } }

        #endregion
    }
}
