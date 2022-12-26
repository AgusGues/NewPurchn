using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PakaiDetail:GRCBaseDomain
    {
        private int id = 0;
        private int pakaiID = 0;
        private int itemID = 0;
        private decimal quantity = 0;
        private decimal pakai = 0;
        private int uomID = 0;
        private int rowStatus = 0;
        private string itemName = string.Empty;
        private string uOMCode = string.Empty;
        private string matikan = string.Empty;
        private string itemCode = string.Empty;
        private int groupID = 0;
        private string keterangan = string.Empty;
        private int flagTipe = 0;
        private int itemTypeID = 0;
        private string deptCode = string.Empty;
        private decimal avgPrice = 0;
        /// <summary>
        /// added on 11-03-2014 for efesiensi perwatan mesin
        /// </summary>
        private int sarmutid = 0;
        private int projectid = 0;
        private string projectname = string.Empty;
        /** added on 02-06-2014 for perawatan kendaraan*/
        private int idkendaraan = 0;
        private string nopol = string.Empty;
        /** added on 20-09-2014 for SPB Biaya */
        private int  idjenisbiaya = 0;
        /** added on 29-10-2014 for SPB Bahan Baku produksi*/
        public int LineNo { get; set; }

        /** Beny : 8 Agustus 2022 **/
        public string Press { get; set; }
        public string Kelompok { get; set; }
        /** End **/

        public string ProjectName
        {
            get { return projectname; }
            set { projectname = value; }
        }
        public int ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }
        public int SarmutID
        {
            get { return sarmutid; }
            set { sarmutid = value; }
        }
        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public int FlagTipe
        {
            get { return flagTipe; }
            set { flagTipe = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
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
        public string Matikan
        {
            get { return matikan; }
            set { matikan = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        //public int ID
        //{
        //    get
        //    {
        //        return id;
        //    }
        //    set
        //    {
        //        id = value;
        //    }
        //}
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
        public decimal Pakai
        {
            get
            {
                return pakai;
            }
            set
            {
                pakai = value;
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
        public int IDJenisBiaya
        {
            set { idjenisbiaya = value; }
            get { return idjenisbiaya; }
        }
        public int BudgetID { get; set; }
        public decimal BudgetQty { get; set; }
        /// <summary>
        /// Object for Zona Maintenance
        /// Added on 18-01-2016
        /// </summary>
        public string Zona { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public int DeptID { get; set; }
        public decimal AddQty { get; set; }
        public decimal KartuStock { get; set; }
    }
}
