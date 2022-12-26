using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BusinessFacade
{
    public class EditSPPDetail : GRCBaseDomain
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
        private decimal qtySisa = 0;
        private DateTime tglKirim = DateTime.Today;
        //iko
        private int amGroupID = 0;
        private int amClassID = 0;
        private int amSubClassID = 0;
        private int amLokasiID = 0;
        private string amKodeAsset = string.Empty;
        private int mTCGroupSarmutID = 0;
        private int materialMTCGroupID = 0;
        private int umurEkonomis = 0;
        private string nopol = string.Empty;
        private decimal price = 0;
        private string kelompok = string.Empty;

        //Agus
        private int iDEditSPP = 0;
        private string groupSP = string.Empty;
        private int pendingSPPDetail = 0;
        private string alasanPendingSPPdetail = string.Empty;
        private int sppDetailID = 0;

        public string Kelompok
        {
            get { return kelompok; }
            set { kelompok = value; }
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
        public string NoPol
        {
            get { return nopol; }
            set { nopol = value; }
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
        public int AmGroupID
        {
            get { return amGroupID; }
            set { amGroupID = value; }
        }
        public int AmClassID
        {
            get { return amClassID; }
            set { amClassID = value; }
        }
        public int AmSubClassID
        {
            get { return amSubClassID; }
            set { amSubClassID = value; }
        }
        public int AmLokasiID
        {
            get { return amLokasiID; }
            set { amLokasiID = value; }
        }
        public string AmKodeAsset
        {
            get { return amKodeAsset; }
            set { amKodeAsset = value; }
        }
        //iko

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

        public int IDEditSPP
        {
            get
            {
                return iDEditSPP;
            }

            set
            {
                iDEditSPP = value;
            }
        }

        //public int GroupSP
        //{
        //    get
        //    {
        //        return groupSP;
        //    }

        //    set
        //    {
        //        groupSP = value;
        //    }
        //}





        public string GroupSP
        {
            get
            {
                return groupSP;
            }

            set
            {
                groupSP = value;
            }
        }

        public int PendingSPPDetail
        {
            get
            {
                return pendingSPPDetail;
            }

            set
            {
                pendingSPPDetail = value;
            }
        }

        public string AlasanPendingSPPdetail
        {
            get
            {
                return alasanPendingSPPdetail;
            }

            set
            {
                alasanPendingSPPdetail = value;
            }
        }

        public int SppDetailID
        {
            get
            {
                return sppDetailID;
            }

            set
            {
                sppDetailID = value;
            }
        }
    }
}
