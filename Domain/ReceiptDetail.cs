using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ReceiptDetail:GRCBaseDomain
    {
        #region declare variable
        private int id = 0;
        private int receiptID = 0;
        private int itemID = 0;
        private int itemID2 = 0;
        private decimal quantity = 0;
        private int uomID = 0;
        private int rowStatus = 0;
        private decimal price = 0;
        private decimal disc = 0;
        private decimal totalPrice = 0;
        private string noPO = string.Empty;
        private string itemName = string.Empty;
        private string uOMCode = string.Empty;
        private string itemCode = string.Empty;
        private int poID = 0;
        private string poNo = string.Empty;
        private int sppID = 0;
        private string sppNo = string.Empty;
        private int poDetailID = 0;
        private int flagPO = 0;
        private int flagTipe = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private string keterangan = string.Empty;
        private decimal kadarair = 0;
        private int supplierID = 0;
        /** added on 20-06-2014 for DO Receipt*/
        private string dosupplier = string.Empty;
        #endregion

        //iko, for auto SPB
        private int sarmutID = 0;
        private decimal avgPrice = 0;
        private int iDJenisBiaya = 0;
        private string deptCode = string.Empty;
        private string nopol = string.Empty;
        private int tipeAsset = 0;
        private int ownerdeptid = 0;//OwnerDeptID

        #region untuk MtcProject
        private int projectId = 0;
        public int ProjectID
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }
        #endregion

        public int OwnerDeptID
        {
            get
            {
                return ownerdeptid;
            }
            set
            {
                ownerdeptid = value;
            }
        }

        public int TipeAsset
        {
            get { return tipeAsset; }
            set { tipeAsset = value; }
        }
       
        public string NoPol
        {
            get { return nopol; }
            set { nopol = value; }
        }

        public int SarmutID
        {
            get { return sarmutID; }
            set { sarmutID = value; }
        }
        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }
        public int IDJenisBiaya
        {
            get { return iDJenisBiaya; }
            set { iDJenisBiaya = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        //iko, for auto SPB
       
        
        public string DOSupplier
        {
            set { dosupplier = value; }
            get { return dosupplier; }
        }
        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public int FlagTipe
        {
            get { return flagTipe; }
            set { flagTipe = value; }
        }
        public int FlagPO
        {
            get { return flagPO; }
            set { flagPO = value; }
        }
        public int PODetailID
        {
            get { return poDetailID; }
            set { poDetailID = value; }
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
        public string NoPO
        {
            get { return noPO; }
            set { noPO = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
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
        public int ReceiptID
        {
            get
            {
                return receiptID;
            }
            set
            {
                receiptID = value;
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
        public int ItemID2
        {
            get { return itemID2; }
            set { itemID2 = value; }
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
        public decimal Kadarair
        {
            get
            {
                return kadarair;
            }
            set
            {
                kadarair = value;
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
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public decimal Disc
        {
            get
            {
                return disc;
            }
            set
            {
                disc = value;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
            }
        }
        public int PoID
        {
            get
            {
                return poID;
            }
            set
            {
                poID = value;
            }
        }
        public string PoNo
        {
            get
            {
                return poNo;
            }
            set
            {
                poNo = value;
            }
        }
        public int SppID
        {
            get
            {
                return sppID;
            }
            set
            {
                sppID = value;
            }
        }
        public string SppNo
        {
            get
            {
                return sppNo;
            }
            set
            {
                sppNo = value;
            }
        }
        /*added on 15-02-2015
         * Data timbangan receipt bahan baku
         */
        public decimal QtyTimbang { get; set; }
        public int KodeTimbang { get; set; }
        public string ScheduleNo { get; set; }
        public int DOID { get; set; }
        private decimal timbanganbpas = 0;
        public decimal TimbanganBPAS { get { return timbanganbpas; } set { timbanganbpas = value; } }
    }
}
