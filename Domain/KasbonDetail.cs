using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class KasbonDetail : GRCBaseDomain
    {
        private int id = 0;
        private int kid = 0;
        private int kdid = 0;
        private int sppid = 0;
        private int poid = 0;
        private int sppdetailid = 0;
        private int groupID = 0;
        private int itemID = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private decimal quantity = 0;
        private string namaBarang = string.Empty;
        private string itemCode = string.Empty;
        private string satuan = string.Empty;
        private int status = 0;
        private decimal qtyPO = 0;
        private string cariItemName = string.Empty;
        private string keterangan = string.Empty;
        private decimal qtySisa = 0;
        private DateTime tglKirim = DateTime.Today;
        
        private int mTCGroupSarmutID = 0;
        private int materialMTCGroupID = 0;
        private int umurEkonomis = 0;
        private string nopo = string.Empty;
        private decimal estimasikasbon = 0;
        private decimal price = 0;
        private decimal ppn = 0;
        private decimal ongkosKirim = 0;
        private decimal estimasiharga = 0;
        private decimal totalPO = 0;
        private decimal hargapo = 0;
        private decimal qty = 0;
        private string noSPP = string.Empty;
        private decimal danacadangan = 0;


        public string NoSPP
        {
            get { return noSPP; }
            set { noSPP = value; }
        }

        public string NoPO
        {
            get { return nopo; }
            set { nopo = value; }
        }

        public int UmurEkonomis
        {
            get { return umurEkonomis; }
            set { umurEkonomis = value; }
        }
        public int MTCGroupSarmutID
        {
            get { return mTCGroupSarmutID; }
            set { mTCGroupSarmutID = value; }
        }
        public int MaterialMTCGroupID
        {
            get { return materialMTCGroupID; }
            set { materialMTCGroupID = value; }
        }
        

        public DateTime TglKirim
        {
            get { return tglKirim; }
            set { tglKirim = value; }
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
        public int KID
        {
            get
            {
                return kid;
            }
            set
            {
                kid = value;
            }
        }
        public int KDID
        {
            get
            {
                return kdid;
            }
            set
            {
                kdid = value;
            }
        }
        public int SPPID
        {
            get
            {
                return sppid;
            }
            set
            {
                sppid = value;
            }
        }
        public int POID
        {
            get
            {
                return poid;
            }
            set
            {
                poid = value;
            }
        }
        public int SPPDetailID
        {
            get
            {
                return sppdetailid;
            }
            set
            {
                sppdetailid = value;
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
        public string NamaBarang
        {
            get
            {
                return namaBarang;
            }
            set
            {
                namaBarang = value;
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
        public decimal Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }
        public decimal EstimasiKasbon
        {
            get
            {
                return estimasikasbon;
            }
            set
            {
                estimasikasbon = value;
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
        public decimal PPN
        {
            get
            {
                return ppn;
            }
            set
            {
                ppn = value;
            }
        }
        public decimal OngkosKirim
        {
            get
            {
                return ongkosKirim;
            }
            set
            {
                ongkosKirim = value;
            }
        }
        public decimal EstimasiHarga
        {
            get
            {
                return estimasiharga;
            }
            set
            {
                estimasiharga = value;
            }
        }

        public decimal TotalPO
        {
            get
            {
                return totalPO;
            }
            set
            {
                totalPO = value;
            }
        }
        public virtual int PendingPO { get; set; }
        public virtual string AlasanPending { get; set; }
        public string TypeBiaya { get; set; }
        public string Keterangan1 { get; set; }

        public decimal DanaCadangan
        {
            get
            {
                return danacadangan;
            }
            set
            {
                danacadangan = value;
            }
        }
    }


}
