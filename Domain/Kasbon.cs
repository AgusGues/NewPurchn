using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Kasbon : GRCBaseDomain
    {
        private int id = 0;
        private int kdid = 0;
        private int deptid = 0;
        private string username = string.Empty;
        private string noSPP = string.Empty;
        private string noPO = string.Empty;
        private string namaBarang = string.Empty;
        private string kodeBarang = string.Empty;
        private decimal estimasikasbon = 0;
        private decimal total = 0;
        private decimal totalestimasi = 0;
        private decimal totalPO = 0;
        private decimal totalAllPO = 0;
        private decimal price = 0;
        private decimal danacadangan = 0;
        private decimal qty = 0;
        private decimal qtyPO = 0;
        private int itemID = 0;
        private string satuan = string.Empty;
        private int groupID = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private int sPPID = 0;
        private int pOID = 0;
        private int sPPDetailID = 0;
        private int status = 0;
        private int apv = 0;
        private string noKasbon = string.Empty;
        private string noPengajuan = string.Empty;
        private DateTime kasbonDate = DateTime.Now.Date;
        private string pic = string.Empty;
        private int itemFrom = 0;
        public int KasbonDetailID { get; set; }
        private string alasanNotApproval = string.Empty;
        private DateTime approveDate1 = DateTime.Now.Date;
        private DateTime approveDate2 = DateTime.Now.Date;
        private DateTime approveDate3 = DateTime.Now.Date;
        private DateTime approveDate4 = DateTime.Now.Date;
        private DateTime approvedFinance = DateTime.Now.Date;
        private DateTime approvedEnd = DateTime.Now.Date;
        private int kasbonCounter = 0;
        private int kasbonType = 0;

        private string Uomdesc = string.Empty;
        private string kasbonno = string.Empty;
        private DateTime tglkasbon = DateTime.Now.Date;
        private string itemname = string.Empty;


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
        public int DeptID
        {
            get
            {
                return deptid;
            }
            set
            {
                deptid = value;
            }
        }
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string NoSPP
        {
            get { return noSPP; }
            set { noSPP = value; }
        }
        public string NoPo
        {
            get { return noPO; }
            set { noPO = value; }
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
        public string KodeBarang
        {
            get
            {
                return kodeBarang;
            }
            set
            {
                kodeBarang = value;
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
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
            }
        }
        public decimal TotalEstimasi
        {
            get
            {
                return totalestimasi;
            }
            set
            {
                totalestimasi = value;
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
        public decimal TotalAllPO
        {
            get
            {
                return totalAllPO;
            }
            set
            {
                totalAllPO = value;
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
        public int POID
        {
            get
            {
                return pOID;
            }
            set
            {
                pOID = value;
            }
        }
        public int SPPDetailID
        {
            get
            {
                return sPPDetailID;
            }
            set
            {
                sPPDetailID = value;
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
        public int Apv
        {
            get
            {
                return apv;
            }
            set
            {
                apv = value;
            }
        }
        public string NoKasbon
        {
            get
            {
                return noKasbon;
            }
            set
            {
                noKasbon = value;
            }
        }
        public string NoPengajuan
        {
            get
            {
                return noPengajuan;
            }
            set
            {
                noPengajuan = value;
            }
        }
        public DateTime KasbonDate
        {
            get
            {
                return kasbonDate;
            }
            set
            {
                kasbonDate = value;
            }
        }
        public string PIC
        {
            get
            {
                return pic;
            }
            set
            {
                pic = value;
            }
        }

        public int ItemFrom
        {
            get { return itemFrom; }
            set { itemFrom = value; }
        }
        public string AlasanNotApproval
        {
            get { return alasanNotApproval; }
            set { alasanNotApproval = value; }
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
        public int KasbonCounter
        {
            get
            {
                return kasbonCounter;
            }
            set
            {
                kasbonCounter = value;
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
        public DateTime ApproveDate4
        {
            get
            {
                return approveDate4;
            }
            set
            {
                approveDate4 = value;
            }
        }
        public DateTime ApprovedFinance
        {
            get
            {
                return approvedFinance;
            }
            set
            {
                approvedFinance = value;
            }
        }
        public DateTime ApprovedEnd
        {
            get
            {
                return approvedEnd;
            }
            set
            {
                approvedEnd = value;
            }
        }
        public int KasbonType
        {
            get
            {
                return kasbonType;
            }
            set
            {
                kasbonType = value;
            }
        }

        public string UOMDesc
        {
            get
            {
                return Uomdesc;
            }
            set
            {
                Uomdesc = value;
            }
        }

        public string KasbonNo
        {
            get
            {
                return kasbonno;
            }
            set
            {
                kasbonno = value;
            }
        }
        public DateTime TglKasbon
        {
            get
            {
                return tglkasbon;
            }
            set
            {
                tglkasbon = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemname;
            }
            set
            {
                itemname = value;
            }
        }
    }
}
