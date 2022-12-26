using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MutasiAsset : GRCBaseDomain
    {
        private string mutasiNo = string.Empty;
        private DateTime mutasiDate = DateTime.Now.Date;
        private DateTime approvalDate = DateTime.Now.Date;
        private string approvalBy = string.Empty;
        private int status = 0;
        private string alasanCancel = string.Empty;
        private int pakaiTipe = 0;
        private int itemID = 0;
        private int uomID = 0;
        private string uomCode = string.Empty;
        private decimal quantity = 0;
        private string keterangan = string.Empty;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int dariDeptID = 0;
        private int dariDepoID = 0;
        private string dariDeptCode = string.Empty;
        private string dariDepoCode = string.Empty;
        private string deptName = string.Empty;
        private int keDeptID = 0;
        private int keDepoID = 0;
        private string keDeptCode = string.Empty;
        private string keDepoCode = string.Empty;

        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
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
        public string KeDeptCode
        {
            get { return keDeptCode; }
            set { keDeptCode = value; }
        }
        public string KeDepoCode
        {
            get { return keDepoCode ; }
            set { keDepoCode = value; }
        }
        public string DariDeptCode
        {
            get { return dariDeptCode; }
            set { dariDeptCode = value; }
        }
        public string DariDepoCode
        {
            get { return dariDepoCode; }
            set { dariDepoCode = value; }
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
        public string MutasiNo
        {
            get
            {
                return mutasiNo;
            }
            set
            {
                mutasiNo = value;
            }
        }

        public DateTime MutasiDate
        {
            get
            {
                return mutasiDate;
            }
            set
            {
                mutasiDate = value;
            }
        }

        public int KeDepoID
        {
            get { return keDepoID; }
            set { keDepoID = value; }
        }
        public int KeDeptID
        {
            get { return keDeptID; }
            set { keDeptID = value; }
        }
        public int DariDepoID
        {
            get { return dariDepoID; }
            set { dariDepoID = value; }
        }
        public int DariDeptID
        {
            get { return dariDeptID; }
            set { dariDeptID = value; }
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
