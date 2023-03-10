using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ReturPakai : GRCBaseDomain
    {
        private string returNo = string.Empty;
        private DateTime returDate = DateTime.Now.Date;
        private int pakaiID = 0;
        private string pakaiNo = string.Empty;
        private int depoID = 0;
        private int deptID = 0;
        private DateTime approvalDate = DateTime.Now.Date;
        private string approvalBy = string.Empty;
        private int status = 0;
        private string alasanCancel = string.Empty;
        private int pakaiTipe = 0;
        private int itemID = 0;
        private int uomID = 0;
        private string uomCode = string.Empty;
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        private decimal quantity = 0;
        private string keterangan = string.Empty;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int itemTypeID = 0;

        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public string UomCode
        {
            get { return uomCode; }
            set { uomCode = value; }
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
        public int UomID
        {
            get { return uomID; }
            set { uomID = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        public int PakaiTipe
        {
            get { return pakaiTipe; }
            set { pakaiTipe = value; }
        }
        public string AlasanCancel
        {
            get { return alasanCancel; }
            set { alasanCancel = value; }
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
        public string PakaiNo
        {
            get
            {
                return pakaiNo;
            }
            set
            {
                pakaiNo = value;
            }
        }
        public string ReturNo
        {
            get
            {
                return returNo;
            }
            set
            {
                returNo = value;
            }
        }

        public DateTime ReturDate
        {
            get
            {
                return returDate;
            }
            set
            {
                returDate = value;
            }
        }
        public int PakaiID
        {
            get
            {
                return pakaiID;
            }
            set
            {
                pakaiID = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }
        public int DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                deptID = value;
            }
        }
        public DateTime ApprovalDate
        {
            get
            {
                return approvalDate;
            }
            set
            {
                approvalDate = value;
            }
        }

        public string ApprovalBy
        {
            get
            {
                return approvalBy;
            }
            set
            {
                approvalBy = value;
            }
        }

    }

}
