using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_Armada : GRCBaseDomain
    {
        private int idkendaraan = 0;
        private string nopol = string.Empty;
        private int itemid = 0;
        private decimal quantity = 0;
        private decimal avgprice = 0;
        private DateTime spbdate;
        private string spbno = string.Empty;
        private int deptid = 0;
        private int groupid = 0;
        private int itemtypeid = 0;
        private decimal totals = 0;
        private string itemcode = string.Empty;
        private string itemname=string.Empty;
        private string satuan = string.Empty;
        private decimal total = 0;
        private decimal totavg = 0;
        private string deptname = string.Empty;

        public int IDKendaraan
        {
            set { idkendaraan = value; }
            get { return idkendaraan; }
        }
        public string NoPol
        {
            set { nopol = value; }
            get { return nopol; }
        }
        public int ItemID
        {
            set { itemid = value; }
            get { return itemid; }
        }
        public decimal Quantity
        {
            set { quantity = value; }
            get { return quantity; }
        }
        public decimal AvgPrice
        {
            set { avgprice = value; }
            get { return avgprice; }
        }
        public DateTime SPBDate
        {
            set { spbdate = value; }
            get { return spbdate; }
        }
        public string SPBNo
        {
            set { spbno = value; }
            get { return spbno; }
        }
        public int DeptID
        {
            set { deptid = value; }
            get { return deptid; }
        }
        public int GroupID
        {
            set { groupid = value; }
            get { return groupid; }
        }
        public int ItemTypeID
        {
            set { itemtypeid = value; }
            get { return itemtypeid; }
        }
        public decimal TotalS
        {
            set { totals = value; }
            get { return totals; }
        }
        public string ItemCode
        {
            set { itemcode = value; }
            get { return itemcode; }
        }
        public string ItemName
        {
            set { itemname = value; }
            get { return itemname; }
        }
        public string Satuan
        {
            set { satuan = value; }
            get { return satuan; }
        }
        public Decimal Total
        {
            set { total = value; }
            get { return total; }
        }
        public string DeptName
        {
            set { deptname = value; }
            get { return deptname; }
        }
        public decimal TotAvg
        {
            set { totavg = value; }
            get { return totavg; }
        }

        public string DriverName { get; set; }
        public string HPDriver { get; set; }
        public string SIM { get; set; }
        public string SIMType { get; set; }
        public int DriverType { get; set; }
    }
}
