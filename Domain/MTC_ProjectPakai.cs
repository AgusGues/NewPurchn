using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_ProjectPakai : GRCBaseDomain
    {
        private int projectid = 0;
        private int pakaiid = 0;
        private int itemid = 0;
        private int groupid = 0;
        private int itemtypeid = 0;
        private decimal qty = 0;
        private int rowstatus = 0;
        private decimal avgprice = 0;
        private int deptid = 0;

        public int ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }
        public int PakaiID
        {
            get { return pakaiid; }
            set { pakaiid = value; }
        }
        public int ItemID
        {
            get { return itemid; }
            set { itemid = value; }
        }
        public int GroupID
        {
            get { return groupid; }
            set { groupid = value; }
        }
        public int ItemTypeID
        {
            get { return itemtypeid; }
            set { itemtypeid = value; }
        }
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public decimal AvgPrice
        {
            get { return avgprice; }
            set { avgprice = value; }
        }
        public int RowStatus
        {
            get { return rowstatus; }
            set { rowstatus = value; }
        }
        public int DeptID
        {
            get { return deptid; }
            set { deptid = value; }
        }
    }
}
