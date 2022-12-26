using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class EditSPP : GRCBaseDomain
    {
        private int lokasiid = 0;
        private int id = 0;
        private string noSPP = string.Empty;
        private DateTime minta = DateTime.Today;
        private int itemID = 0;
        private int satuanID = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private decimal jumlah = 0;
        private decimal jumlahSisa = 0;
        private string keterangan = string.Empty;
        private int sudah = 0;
        private int fCetak = 0;
        private int userID = 0;
        private int pending = 0;
        private int inden = 0;
        private string alasanBatal = string.Empty;
        private string alasanCLS = string.Empty;
        private int status = 0;
        private int approval = 0;
        private int depoID = 0;
        private DateTime approveDate1 = DateTime.Today;
        private DateTime approveDate2 = DateTime.Today;
        private DateTime approveDate3 = DateTime.Today;
        private int aprv = 0;
        private DateTime createdTime = DateTime.Now.Date;
        private DateTime tglBuat = DateTime.Now.Date;
        private DateTime tanggal = DateTime.Now.Date;
        private int permintaanType = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private string uomCode = string.Empty;
        private string userName = string.Empty;
        private string headName = string.Empty;
        private int headID = 0;
        private string gudang = string.Empty;
        private string nopol = string.Empty;
        private string useradmin = string.Empty;
        private string userhead = string.Empty;

        //penambahan agus 2022-09-11
        private int sppDetailID = 0;
        private string jenisEdit = string.Empty;
        private string keteranganEditSpp = string.Empty;
        private int apvHeadPemohon = 0;
        private int apvMgrPemohon = 0;
        private int apvMgr = 0;
        private int apvPM = 0;
        private int apvPurch = 0;
        private int apvAccounting = 0;

        //penambahan agus 2022-09-11

        public string UserAdmin { get { return useradmin; } set { useradmin = value; } }
        public string UserHead { get { return userhead; } set { userhead = value; } }

        public string Gudang { get { return gudang; } set { gudang = value; } }

        //penambahan agus 2022-09-11
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
        //penambahan agus 2022-09-11
        public int LokasiID
        {
            get { return lokasiid; }
            set { lokasiid = value; }
        }

        public int HeadID
        {
            get { return headID; }
            set { headID = value; }
        }

        public string NoPol
        {
            get
            {
                return nopol;
            }
            set
            {
                nopol = value;
            }
        }

        public string HeadName
        {
            get
            {
                return headName;
            }
            set
            {
                headName = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string UomCode
        {
            get
            {
                return uomCode;
            }
            set
            {
                uomCode = value;
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

        public DateTime TglBuat
        {
            get { return tglBuat; }
            set { tglBuat = value; }
        }
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }
        public int PermintaanType
        {
            get
            {
                return permintaanType;
            }
            set
            {
                permintaanType = value;
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

        public string NoSPP
        {
            get
            {
                return noSPP;
            }
            set
            {
                noSPP = value;
            }
        }

        public DateTime Minta
        {
            get
            {
                return minta;
            }
            set
            {
                minta = value;
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

        public int SatuanID
        {
            get
            {
                return satuanID;
            }
            set
            {
                satuanID = value;
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

        public decimal Jumlah
        {
            get
            {
                return jumlah;
            }
            set
            {
                jumlah = value;
            }
        }

        public decimal JumlahSisa
        {
            get
            {
                return jumlahSisa;
            }
            set
            {
                jumlahSisa = value;
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

        public int Sudah
        {
            get
            {
                return sudah;
            }
            set
            {
                sudah = value;
            }
        }

        public int FCetak
        {
            get
            {
                return fCetak;
            }
            set
            {
                fCetak = value;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public int Pending
        {
            get
            {
                return pending;
            }
            set
            {
                pending = value;
            }
        }

        public int Inden
        {
            get
            {
                return inden;
            }
            set
            {
                inden = value;
            }
        }

        public string AlasanBatal
        {
            get
            {
                return alasanBatal;
            }
            set
            {
                alasanBatal = value;
            }
        }

        public string AlasanCLS
        {
            get
            {
                return alasanCLS;
            }
            set
            {
                alasanCLS = value;
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

        public int Approval
        {
            get
            {
                return approval;
            }
            set
            {
                approval = value;
            }
        }


        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public int Aprv
        {
            get
            {
                return aprv;
            }
            set
            {
                aprv = value;
            }
        }

        public DateTime ApproveDate1
        {
            get
            {
                return approveDate1;
            }
            set
            {
                approveDate1 = value;
            }
        }

        public DateTime ApproveDate2
        {
            get
            {
                return approveDate2;
            }
            set
            {
                approveDate2 = value;
            }
        }

        public DateTime ApproveDate3
        {
            get
            {
                return approveDate3;
            }
            set
            {
                approveDate3 = value;
            }
        }

        public string JenisEdit
        {
            get
            {
                return jenisEdit;
            }

            set
            {
                jenisEdit = value;
            }
        }

        public string KeteranganEditSpp
        {
            get
            {
                return keteranganEditSpp;
            }

            set
            {
                keteranganEditSpp = value;
            }
        }

        public int ApvHeadPemohon
        {
            get
            {
                return apvHeadPemohon;
            }

            set
            {
                apvHeadPemohon = value;
            }
        }

        public int ApvMgrPemohon
        {
            get
            {
                return apvMgrPemohon;
            }

            set
            {
                apvMgrPemohon = value;
            }
        }

        public int ApvMgr
        {
            get
            {
                return apvMgr;
            }

            set
            {
                apvMgr = value;
            }
        }

        public int ApvPM
        {
            get
            {
                return apvPM;
            }

            set
            {
                apvPM = value;
            }
        }

        public int ApvPurch
        {
            get
            {
                return apvPurch;
            }

            set
            {
                apvPurch = value;
            }
        }

        public int ApvAccounting
        {
            get
            {
                return apvAccounting;
            }

            set
            {
                apvAccounting = value;
            }
        }
    }

    public class ListSPPSPPEdit
    {
        public int ID { get; set; }
        public string NoSPP { get; set; }
        public DateTime Tanggal { get; set; }

        public string TanggalMinta
        {
            get { return Tanggal.ToString(); }
            set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", Tanggal); } }
        }
        public string GroupDescription { get; set; }
        public string TypeDescription { get; set; }
        public string ApprovalStatus { get; set; }
        public int Approval { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public int GroupID { get; set; }
        public string AlasanBatal { get; set; }
        public string HeadName { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime ApproveDate1 { get; set; }
        public DateTime ApproveDate2 { get; set; }
        public DateTime ApproveDate3 { get; set; }
        public DateTime LastModifiedTime { get; set; }

    }
    public class DetailSPPSPPEdit
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public DateTime TglKirim { get; set; }
        public string TanggalKirim
        {
            get { return TglKirim.ToString(); }
            set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", TglKirim); } }
        }
        public string ItemName { get; set; }
        public string Keterangan { get; set; }
        public int Quantity { get; set; }
        public string Satuan { get; set; }
        public int SPPID { get; set; }

    }
    public class SPPHeadSPPEdit : EditSPP
    {
        public int ManagerID { get; set; }
        public int PlantMgrID { get; set; }
        public int UserID { get; set; }

    }
}
