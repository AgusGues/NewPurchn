using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Adjust : GRCBaseDomain
    {
        private string adjustNo = string.Empty;
        private DateTime adjustDate = DateTime.Now.Date;
        private string adjustType = string.Empty;
        private string keterangan1 = string.Empty;
        private int status = 0;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private decimal quantity = 0;
        private string alasanCancel = string.Empty;
        private int itemTypeID = 0;
        private int itemID = 0;
        private string uOMCode = string.Empty;
        private string keterangan = string.Empty;

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        //public string UOMCode
        //{
        //    get { return uOMCode; }
        //    set { uOMCode = value; }
        //}
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string AlasanCancel
        {
            get { return alasanCancel; }
            set { alasanCancel = value; }
        }
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
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
        public string AdjustType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string AdjustNo
        {
            get
            {
                return adjustNo;
            }
            set
            {
                adjustNo = value;
            }
        }

        public DateTime AdjustDate
        {
            get
            {
                return adjustDate;
            }
            set
            {
                adjustDate = value;
            }
        }
        public string Keterangan1
        {
            get
            {
                return keterangan1;
            }
            set
            {
                keterangan1 = value;
            }
        }
        //adjust asset new
        public int AssetID { get; set; }
        public string KodeAsset { get; set; }
        public string NamaAsset { get; set; }
        public string LokasiAsset { get; set; }
        public int LokasiID { get; set; }
        public int NonStok { get; set; }
    }
}
