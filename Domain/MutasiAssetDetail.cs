using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MutasiAssetDetail
    {
        private int id = 0;
        private int mutasiAssetID = 0;
        private int itemID = 0;
        private decimal quantity = 0;
        private int uomID = 0;
        private int rowStatus = 0;
        private string itemName = string.Empty;
        private string uOMCode = string.Empty;
        private string itemCode = string.Empty;
        private int groupID = 0;
        private string keterangan = string.Empty;
        private int flagTipe = 0;

        public int FlagTipe
        {
            get { return flagTipe; }
            set { flagTipe = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string UOMCode
        {
            get { return uOMCode; }
            set { uOMCode = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int MutasiAssetID
        {
            get
            {
                return mutasiAssetID;
            }
            set
            {
                mutasiAssetID = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public int RowStatus
        {
            get
            {
                return rowStatus;
            }
            set
            {
                rowStatus = value;
            }
        }
        public int UomID
        {
            get
            {
                return uomID;
            }
            set
            {
                uomID = value;
            }
        }
    }
}
