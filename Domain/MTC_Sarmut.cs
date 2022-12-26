using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_Sarmut : GRCBaseDomain
    {
        private int sarmutid = 0;
        private int itemid = 0;
        private string deptkode = string.Empty;
        private int deptid = 0;
        private decimal qty = 0;
        private decimal avgprice = 0;
        private DateTime spbdate = DateTime.Now.Date;
        private string spbid = string.Empty;
        private string sarmutname = string.Empty;
        private string sarmutcode = string.Empty;
        private decimal totalsm = 0;
        private int itemtypeid = 0;
        private int groupid = 0;
        private decimal budget = 0;
        private string groupsp = string.Empty;
        private string Keterangan = string.Empty;
        public string GroupReg { get; set; }
        public int Bulan { get; set; }
        public int Harga { get; set; }
        /**
         * added on 02-06-2014
         * perawatan kendaraan
         */
        private int idkendaraan = 0;
        private string nopol = string.Empty;

        private string kelompok = string.Empty;
        public string Kelompok
        {
            get { return kelompok; }
            set { kelompok = value; }
        }

        public string Groupsp
        {
            get { return groupsp; }
            set { groupsp = value; }
        }
        public decimal Budget
        {
            set { budget = value; }
            get { return budget; }
        }
        public int IDKendaraan
        {
            get { return idkendaraan; }
            set { idkendaraan = value; }
        }
        public string NoPol
        {
            get { return nopol; }
            set { nopol = value; }
        }

        public int ItemTypeID
        {
            get { return itemtypeid; }
            set { itemtypeid = value; }
        }

        public decimal TotalSM
        {
            get { return totalsm; }
            set { totalsm = value; }
        }
        public int SarmutID
        {
            get { return sarmutid; }
            set { sarmutid = value; }
        }

        public int ItemID
        {
            get { return itemid; }
            set { itemid = value; }
        }

        public string DeptCode
        {
            get { return deptkode; }
            set { deptkode = value; }
        }

        public int DeptID
        {
            get { return deptid; }
            set { deptid = value; }
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

        public DateTime SPBDate
        {
            get { return spbdate; }
            set { spbdate = value; }
        }

        public string SPBID
        {
            get { return spbid; }
            set { spbid = value; }
        }

        public string SarmutName
        {
            get { return sarmutname; }
            set { sarmutname = value; }
        }

        public string SarmutCode
        {
            get { return sarmutcode; }
            set { sarmutcode = value; }
        }
        public int GroupID
        {
            get { return groupid; }
            set { groupid = value; }
        }
        public string keterangan
        {
            get { return Keterangan; }
            set { Keterangan = value; }
        }
        /** for rekap sarmut
         * added on 30-06-2014
         */
        private int tahun = 0;
        private Decimal jan = 0;
        private Decimal feb = 0;
        private Decimal mar = 0;
        private Decimal apr = 0;
        private Decimal mei = 0;
        private Decimal jun = 0;
        private Decimal total = 0;
        private string smt = string.Empty;
        public int Tahun
        {
            set { tahun = value; }
            get { return tahun; }
        }
        public Decimal Jan
        {
            set { jan = value; }
            get { return jan; }
        }
        public Decimal Feb
        {
            set { feb = value; }
            get { return feb; }
        }
        public Decimal Mar
        {
            set { mar = value; }
            get { return mar; }
        }
        public Decimal Apr
        {
            set { apr = value; }
            get { return apr; }
        }
        public Decimal Mei
        {
            set { mei = value; }
            get { return mei; }
        }
        public Decimal Jun
        {
            set { jun = value; }
            get { return jun; }
        }
        public string Smt
        {
            set { smt = value; }
            get { return smt; }
        }
        public Decimal Total
        {
            set { total = value; }
            get { return total; }
        }
        public string ZonaMTC { get; set; }
    }
    public class SarmutMaintenance : GRCBaseDomain
    {
        public string Tanggal { get; set; }
        public int Kode { get; set; }
        public string PakaiDate { get; set; }
        public string PakaiNo { get; set; }
        public string ItemCode { get; set; }
        public int ItemID { get; set; }
        public int DeptID { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string GroupName { get; set; }
        public int GroupID { get; set; }
        public int SarmutID { get; set; }
        public string SarmutGroup { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Total { get; set; }
        public decimal Prosen { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
