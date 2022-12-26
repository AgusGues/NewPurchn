using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_SJDetail : GRCBaseDomain
    {
        int sjID = 0;
        int sjdID = 0;
        string itemName = string.Empty;
        int itemIDSJ = 0;
        int tebal = 0;
        int panjang = 0;
        int lebar = 0;
        int qty = 0;
        int qtyP = 0;

        public int SJID
        {
            get { return sjID; }
            set { sjID  = value; }
        }
        public int SJDID
        {
            get { return sjdID; }
            set { sjdID = value; }
        }
        public string  ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int ItemIDSJ
        {
            get { return itemIDSJ; }
            set { itemIDSJ = value; }
        }
        public int Tebal
        {
            get { return tebal ; }
            set { tebal = value; }
        }
        public int Panjang
        {
            get { return panjang ; }
            set { panjang = value; }
        }
        public int Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }
        public int Qty
        {
            get { return qty ; }
            set { qty = value; }
        }
        public int QtyP
        {
            get { return qtyP; }
            set { qtyP = value; }
        }
    }

    public class CopyOfT3_SJDetail : GRCBaseDomain
    {
        int sjID = 0;
        int sjdID = 0;
        string itemName = string.Empty;
        int itemIDSJ = 0;
        int tebal = 0;
        int panjang = 0;
        int lebar = 0;
        int qty = 0;
        int qtyP = 0;

        public int SJID
        {
            get { return sjID; }
            set { sjID = value; }
        }
        public int SJDID
        {
            get { return sjdID; }
            set { sjdID = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int ItemIDSJ
        {
            get { return itemIDSJ; }
            set { itemIDSJ = value; }
        }
        public int Tebal
        {
            get { return tebal; }
            set { tebal = value; }
        }
        public int Panjang
        {
            get { return panjang; }
            set { panjang = value; }
        }
        public int Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public int QtyP
        {
            get { return qtyP; }
            set { qtyP = value; }
        }
    }
}
