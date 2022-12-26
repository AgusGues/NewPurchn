using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class AdjustDetail
    {
        private int id = 0;
        private int apv = 0;
        private int adjustID = 0;
        private int itemID = 0;
        private decimal quantity = 0;
        private int uomID = 0;
        private int rowStatus = 0;
        private int groupID = 0;
        private string itemName = string.Empty;
        private string uOMCode = string.Empty;
        private string itemCode = string.Empty;
        private string keterangan = string.Empty;
        private string adjustType = string.Empty;
        private int itemTypeID = 0;
        private string createdBy = string.Empty;
        private string adjustNo = string.Empty;
        private DateTime adjustDate = DateTime.Now.Date;
        private string nonStok = string.Empty;


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
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public string AdjustType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public string UOMCode
        {
            get { return uOMCode; }
            set { uOMCode = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public int Apv
        {
            get { return apv; }
            set { apv = value; }
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

        public int AdjustID
        {
            get
            {
                return adjustID;
            }
            set
            {
                adjustID = value;
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

        public string NonStok
        {
            get
            {
                return nonStok;
            }
            set
            {
                nonStok = value;
            }
        }
        public int AdjustDetailID { get; set; }
        public int ClosingStatus { get; set; }
       
    }
}
