using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class LapHarian : GRCBaseDomain
    {
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private string uomCode = string.Empty;
        private int uomID = 0;
        private decimal stokAwal = 0;
        private decimal pemasukan = 0;
        private decimal retur = 0;
        private decimal penyesuaian = 0;
        private decimal pemakaian = 0;
        private int deptID = 0;
        private string deptCode = string.Empty;
        private int userID = 0;
        private string tglCetak = string.Empty;
        private string kodeLaporan = string.Empty;
        private int groupID = 0;
        private DateTime dariTgl = DateTime.Now.Date;
        private DateTime sdTgl = DateTime.Now.Date;
        private decimal adjustTambah = 0;
        private decimal adjustKurang = 0;
        private string adjustType = string.Empty;
        private string noDoc = string.Empty;
        private int urutan = 0;
        private int bikinID = 0;
        private decimal stokAkhir = 0;

        public int BikinID
        {
            get { return bikinID; }
            set { bikinID = value; }
        }
        public decimal StokAkhir
        {
            get { return stokAkhir; }
            set { stokAkhir = value; }
        }

        public int Urutan
        {
            get { return urutan; }
            set { urutan = value; }
        }
        public string NoDoc
        {
            get { return noDoc; }
            set { noDoc = value; }
        }
        public string AdjustType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }
        public decimal AdjustTambah
        {
            get { return adjustTambah; }
            set { adjustTambah = value; }
        }
        public decimal AdjustKurang
        {
            get { return adjustKurang; }
            set { adjustKurang = value; }
        }
        public DateTime DariTgl
        {
            get { return dariTgl; }
            set { dariTgl = value; }
        }
        public DateTime SdTgl
        {
            get { return sdTgl; }
            set { sdTgl = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int UomID
        {
            get { return uomID; }
            set { uomID = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
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
        public string UomCode
        {
            get { return uomCode; }
            set { uomCode = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public string KodeLaporan
        {
            get { return kodeLaporan; }
            set { kodeLaporan = value; }
        }
        public decimal StokAwal
        {
            get { return stokAwal; }
            set { stokAwal = value; }
        }

        public decimal Pemasukan
        {
            get { return pemasukan; }
            set { pemasukan = value; }
        }
        public decimal Retur
        {
            get { return retur; }
            set { retur = value; }
        }
        public decimal Penyesuaian
        {
            get { return penyesuaian; }
            set { penyesuaian = value; }
        }
        public decimal Pemakaian
        {
            get { return pemakaian; }
            set { pemakaian = value; }
        }
        public string TglCetak
        {
            get { return tglCetak; }
            set { tglCetak = value; }
        }

    }
}
