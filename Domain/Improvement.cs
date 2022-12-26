using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Improvement : GRCBaseDomain
    {
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int uOMID = 0;
        private int deptID = 0;
        private int unitKerjaID= 0;
        private string deptName = string.Empty;
        private string uOMDesc = string.Empty;
        private int groupID = 0;
        private int iD = 0;
        private int itemTypeID = 0;
        private string keterangan = string.Empty;
        private int itemID = 0;
        private string uomCode = string.Empty;
        private string nama = string.Empty;
        private string type = string.Empty;
        private string merk = string.Empty;
        private string ukuran = string.Empty;
        private string jenis = string.Empty;
        private string partnum = string.Empty;
        private int approval = 0;
        private string createdBy = string.Empty;
        private string approvalStatus= string.Empty;
        private int rowStatus = 0;
        private int stocknonstock = 0;
        private string unitKerja = string.Empty;
        private int leadtime=0;

        public int StockNonStock { get { return stocknonstock; } set { stocknonstock = value; } }

        public int LeadTime
        {
            get { return leadtime; }
            set { leadtime = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int UnitKerjaID
        {
            get { return unitKerjaID; }
            set { unitKerjaID = value; }
        }
        public string UnitKerja
        {
            get { return unitKerja; }
            set { unitKerja = value; }
        }
        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public string ApprovalStatus
        {
            get { return approvalStatus; }
            set { approvalStatus = value; }
        }

        public string Nama
        {
            get { return nama; }
            set { nama = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Merk
        {
            get { return merk; }
            set { merk = value; }
        }
        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }
        public string Jenis
        {
            get { return jenis; }
            set { jenis = value; }
        }
        public string Partnum
        {
            get { return partnum; }
            set { partnum = value; }
        }
       
        public string UomCode
        {
            get{return uomCode;}
            set{uomCode = value;}
        }
       
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string ItemCode
        {
            get{return itemCode;}
            set{itemCode = value;}
        }

        public string ItemName
        {
            get{ return itemName;}
            set { itemName = value; }
        }

       public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public int ItemTypeID
        {
            get { return itemTypeID; }
            set
            { itemTypeID = value; }
        }

        public int UOMID
        {
            get { return uOMID; }
            set { uOMID = value; }
        }

        public string UOMDesc
        {
            get { return uOMDesc; }
            set { uOMDesc = value; }
        }

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public int Approval
        {
            get { return approval; }
            set { approval = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        /**
         * Added on 10-06-2014
         * Object For Rekap Report
         */
        private int tahun = 0;
        private int bulan = 0;
        private int bb = 0;
        private int bp = 0;
        private int atk = 0;
        private int pyk = 0;
        private int mkt = 0;
        private int rkp = 0;
        private int mek = 0;
        private int elk = 0;
        private int total = 0;
        private string nbulan = string.Empty;
        public string BulanLong { get { return nbulan; } set { nbulan = value; } }
        public int Tahun { get { return tahun; } set { tahun = value; } }
        public int Bulan { get { return bulan; } set { bulan = value; } }
        public int BB { get { return bb; } set { bb = value; } }
        public int BP { get { return bp; } set { bp = value; } }
        public int ATK { get { return atk; } set { atk = value; } }
        public int PYK { get { return pyk; } set { pyk = value; } }
        public int MKT { get { return mkt; } set { mkt = value; } }
        public int RKP { get { return rkp; } set { rkp = value; } }
        public int MEK { get { return mek; } set { mek = value; } }
        public int ELK { get { return elk; } set { elk = value; } }
        public int Total { get { return total; } set { total = value; } }
    }
}
