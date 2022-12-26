using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_SJ_CMDetail : GRCBaseDomain
    {
        private int iD = 0;
        private int sJID = 0;
        private int itemIDSJ = 0;
        private string itemName = string.Empty;
        private decimal tebal = 0;
        private decimal panjang = 0;
        private decimal lebar = 0;
        private int palet = 0;
        private int qty = 0;
        private int qtyP = 0;

        public int ID { get { return iD; } set { iD = value; } }
        public int SJID { get { return sJID; } set { sJID = value; } }
        public int ItemIDSJ { get { return itemIDSJ; } set { itemIDSJ = value; } }
        public string ItemName { get { return itemName; } set { itemName = value; } }
        public decimal Tebal { get { return tebal; } set { tebal = value; } }
        public decimal Panjang { get { return panjang; } set { panjang = value; } }
        public decimal Lebar { get { return lebar; } set { lebar = value; } }
        public int Palet { get { return palet; } set { palet = value; } }
        public int Qty { get { return qty; } set { qty = value; } }
        public int QtyP { get { return qtyP; } set { qtyP = value; } }
    }

    public class CopyOfT3_SJ_CMDetail : GRCBaseDomain
    {
        private int iD = 0;
        private int sJID = 0;
        private int itemIDSJ = 0;
        private string itemName = string.Empty;
        private decimal tebal = 0;
        private decimal panjang = 0;
        private decimal lebar = 0;
        private int palet = 0;
        private int qty = 0;
        private int qtyP = 0;

        public int ID { get { return iD; } set { iD = value; } }
        public int SJID { get { return sJID; } set { sJID = value; } }
        public int ItemIDSJ { get { return itemIDSJ; } set { itemIDSJ = value; } }
        public string ItemName { get { return itemName; } set { itemName = value; } }
        public decimal Tebal { get { return tebal; } set { tebal = value; } }
        public decimal Panjang { get { return panjang; } set { panjang = value; } }
        public decimal Lebar { get { return lebar; } set { lebar = value; } }
        public int Palet { get { return palet; } set { palet = value; } }
        public int Qty { get { return qty; } set { qty = value; } }
        public int QtyP { get { return qtyP; } set { qtyP = value; } }
    }
}
