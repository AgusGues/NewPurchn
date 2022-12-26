using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class NewSPPDetail
    {
        private int id = 0;
        private int sPPID = 0;
        private int groupID = 0;
        private int itemID = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private decimal quantity = 0;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private string satuan = string.Empty;
        private int status = 0;
        private decimal qtyPO = 0;
        private string cariItemName = string.Empty;
        private string keterangan = string.Empty;
        private string laporan_no = string.Empty;
        private decimal qtySisa = 0;
        private DateTime tglKirim = DateTime.Today;
        private DateTime jadwal_selesai = DateTime.Now.Date;
        public DateTime TglKirim
        {
            get { return tglKirim; }
            set { tglKirim = value; }
        }
        public DateTime Jadwal_Selesai
        {
            get { return jadwal_selesai; }
            set { jadwal_selesai = value; }
        }
        public string Laporan_No
        {
            get
            {
                return laporan_no;
            }
            set
            {
                laporan_no = value;
            }
        }
        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }
        public string CariItemName
        {
            get
            {
                return cariItemName;
            }
            set
            {
                cariItemName = value;
            }
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
        public int SPPID
        {
            get
            {
                return sPPID;
            }
            set
            {
                sPPID = value;
            }
        }
        public int GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
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
        public int ItemTypeID
        {
            get
            {
                return itemTypeID;
            }
            set
            {
                itemTypeID = value;
            }
        }
        public int UOMID
        {
            get
            {
                return uOMID;
            }
            set
            {
                uOMID = value;
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
        public decimal QtySisa
        {
            get
            {
                return qtySisa;
            }
            set
            {
                qtySisa = value;
            }
        }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }
        public string Satuan
        {
            get
            {
                return satuan;
            }
            set
            {
                satuan = value;
            }
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
        public decimal QtyPO
        {
            get
            {
                return qtyPO;
            }
            set
            {
                qtyPO = value;
            }
        }
        public virtual int PendingPO { get; set; }
        public virtual string AlasanPending { get; set; }
        public string TypeBiaya { get; set; }
        public string Keterangan1 { get; set; }
    }
}
