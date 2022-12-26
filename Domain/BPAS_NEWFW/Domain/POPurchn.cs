using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class POPurchn : GRCBaseDomain
    {
        private string noPO = string.Empty;
        private DateTime pOPurchnDate = DateTime.Now.Date;
        private int supplierID = 0;
        private int pOID = 0;
        private string termin = string.Empty;
        private decimal pPN = 0;
        private string delivery = string.Empty;
        private int crc = 0;
        private string keterangan = string.Empty;
        private string terbilang = string.Empty;
        private decimal disc = 0;
        private decimal pPH = 0;
        private decimal nilaiKurs = 0;
        private int cetak = 0;
        private int countPrt = 0;
        private int status = 0;
        private int approval = 0;
        private DateTime approveDate1 = DateTime.Now.Date;
        private DateTime approveDate2 = DateTime.Now.Date;
        private string alasanBatal = string.Empty;
        private string alasanClose = string.Empty;
        private string nOSPP = string.Empty;
        private string uOMCode = string.Empty;
        private string supplierName = string.Empty;
        private string supplierCode = string.Empty;
        private string uP = string.Empty;
        private string telepon = string.Empty;
        private decimal jumlah = 0;
        private string fax = string.Empty;
        private int sPPID = 0;
        private int groupID = 0;
        private int itemID = 0;
        private decimal price = 0;
        private decimal qty = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private int noUrut = 0;
        private int sPPDetailID = 0;
        private string documentNo = string.Empty;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private string namaBarang = string.Empty;
        private string satuan = string.Empty;
        private decimal qtyPO = 0;
        private decimal qtyReceipt = 0;
        private int paymentType = 0;
        private int itemFrom = 0;
        private string indent = string.Empty;
        private string alasanNotApproval = string.Empty;
        private DateTime dlvDate = DateTime.Now.Date;
        private decimal ongkos = 0;
        private decimal uangMuka = 0;
        private int coaID = 0;
        private string remark = string.Empty;
        private string sPRNo = string.Empty;
        public int PODetailID { get; set; }
        public int DeptID { get; set; }
        public string MasterAssetKomponen { get; set; }
        private string alasanReprint = string.Empty;

        public string AlasanReprint
        {
            get { return alasanReprint; }
            set { alasanReprint = value; }
        }
        public string SPRNo
        {
            get { return sPRNo; }
            set { sPRNo = value; }
        }
        public int CoaID
        {
            get { return coaID; }
            set { coaID = value; }
        }
        public decimal UangMuka
        {
            get { return uangMuka; }
            set { uangMuka = value; }
        }
        public decimal Ongkos
        {
            get { return ongkos; }
            set { ongkos = value; }
        }
        public DateTime DlvDate
        {
            get { return dlvDate; }
            set { dlvDate = value; }
        }
        public string AlasanNotApproval
        {
            get { return alasanNotApproval; }
            set { alasanNotApproval = value; }
        }
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        public string Indent
        {
            get
            {
                return indent;
            }
            set
            {
                indent = value;
            }
        }
        public int PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }
        public int ItemFrom
        {
            get { return itemFrom; }
            set { itemFrom = value; }
        }
        public decimal QtyReceipt
        {
            get { return qtyReceipt; }
            set { qtyReceipt = value; }
        }
        public decimal QtyPO
        {
            get { return qtyPO; }
            set { qtyPO = value; }
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
        public string DocumentNo
        {
            get
            {
                return documentNo;
            }
            set
            {
                documentNo = value;
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
        public int NoUrut
        {
            get
            {
                return noUrut;
            }
            set
            {
                noUrut = value;
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
        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
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
        public string Telepon
        {
            get
            {
                return telepon;
            }
            set
            {
                telepon = value;
            }
        }
        public string UP
        {
            get
            {
                return uP;
            }
            set
            {
                uP = value;
            }
        }
        public string SupplierName
        {
            get
            {
                return supplierName;
            }
            set
            {
                supplierName = value;
            }
        }
        public string SupplierCode
        {
            get
            {
                return supplierCode;
            }
            set
            {
                supplierCode = value;
            }
        }
        public string UOMCode
        {
            get
            {
                return uOMCode;
            }
            set
            {
                uOMCode = value;
            }
        }
        public string NOSPP
        {
            get
            {
                return nOSPP;
            }
            set
            {
                nOSPP = value;
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
        public string NoPO
        {
            get
            {
                return noPO;
            }
            set
            {
                noPO = value;
            }
        }
        public DateTime POPurchnDate
        {
            get
            {
                return pOPurchnDate;
            }
            set
            {
                pOPurchnDate = value;
            }
        }
        public int SupplierID
        {
            get
            {
                return supplierID;
            }
            set
            {
                supplierID = value;
            }
        }
        public string Termin
        {
            get
            {
                return termin;
            }
            set
            {
                termin = value;
            }
        }
        public decimal PPN
        {
            get
            {
                return pPN;
            }
            set
            {
                pPN = value;
            }
        }
        public string Delivery
        {
            get
            {
                return delivery;
            }
            set
            {
                delivery = value;
            }
        }
        public int Crc
        {
            get
            {
                return crc;
            }
            set
            {
                crc = value;
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
        public string Terbilang
        {
            get
            {
                return terbilang;
            }
            set
            {
                terbilang = value;
            }
        }
        public decimal Disc
        {
            get
            {
                return disc;
            }
            set
            {
                disc = value;
            }
        }
        public decimal PPH
        {
            get
            {
                return pPH;
            }
            set
            {
                pPH = value;
            }
        }
        public decimal NilaiKurs
        {
            get
            {
                return nilaiKurs;
            }
            set
            {
                nilaiKurs = value;
            }
        }
        public int Cetak
        {
            get
            {
                return cetak;
            }
            set
            {
                cetak = value;
            }
        }
        public int CountPrt
        {
            get
            {
                return countPrt;
            }
            set
            {
                countPrt = value;
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
        public string AlasanClose
        {
            get
            {
                return alasanClose;
            }
            set
            {
                alasanClose = value;
            }
        }
        public int Tahun { get; set; }
        public decimal TotalHarga { get; set; }
        public string KirimVia { get; set; }
        public DateTime KirimDate { get; set; }
        public string TerimaBy { get; set; }
        public DateTime TerimaDate { get; set; }
        public DateTime EstDelivery { get; set; }
        public DateTime ActDelivery { get; set; }
        public DateTime SchDate { get; set; }
        public string NoPol { get; set; }
        public string Armada { get; set; }
        public string SchNo { get; set; }
        public decimal EstQty { get; set; }
        public int SchID { get; set; }
        public string DoNum { get; set; }
        public int SchPOID { get; set; }
        public int ArmadaID { get; set; }
        public int Flag { get; set; }
        public int Unloading { get; set; }
        public int Ritase { get; set; }
        public string Driver { get; set; }
        public string CoDriver { get; set; }
        public string Jam { get; set; }
        /**
         * Kadar Air
         * added on 02-10-2015
         */
        public decimal StdKA { get; set; }
        public decimal Gross { get; set; }
        public decimal AktualKA { get; set; }
        public decimal Netto { get; set; }
        public int SubCompanyID { get; set; }

        public string ApproveDate1S { get; set; }
        public string ApproveDate2S { get; set; }
        public string ApproveMgrS { get; set; }
        public string JamAppv1 { get; set; }
        public string JamAppv2 { get; set; }
        public string ReceiptNo { get; set; }
        public decimal Sampah { get; set; }
        public string DocNo { get; set; }

    }

    public class PoPurchMail : POPurchn
    {
        public DateTime TglKirim { get; set; }
        public string Email { get; set; }
        public string Report { get; set; }
        public string SupplierName { get; set; }
        public string CreatedBy { get; set; }
    }
    public class MonOutPO : POPurchn
    {
        public decimal RMSQty { get; set; }

    }


    public class POHead
    {
        public int ID { get; set; }
        public string NoPO { get; set; }
        public string Termin { get; set; }
        public int PPN { get; set; }
        public string MataUang { get; set; }
        public string Delivery { get; set; }
        public string Keterangan { get; set; }
        public string SupplierName { get; set; }
        public string UP { get; set; }
        public int UangMuka { get; set; }
        public int PPH { get; set; }
        public int Disc { get; set; }
        public string Remark { get; set; }
        public int Approval { get; set; }
        public string LastModifiedBy { get; set; }
        public int UserApv { get; set; }
        public string AlasanNotApproval { get; set; }
        public string DocumentNo { get; set; }
    }

    public class PODetail
    {
        public string DocumentNo { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string ItemCode { get; set; }
        public string UOMCode { get; set; }
        public int Qty { get; set; }
        public DateTime Dlvdate { get; set; }
        public string DelivDate
        {
            get { return Dlvdate.ToShortDateString(); }
            set { Dlvdate.ToShortDateString(); }
        }
    }

    public class RekapPO
    {
        public int ID { get; set; }
        public string NoPO { get; set; }
        public string SupplierName { get; set; }
        public string Approval { get; set; }
        public decimal PPN { get; set; }
        public decimal PPH { get; set; }
        public string M_Uang { get; set; }
        public string ItemName { get; set; }
        public string NoSPP { get; set; }
        public int Qty { get; set; }
        public string Satuan { get; set; }
        public decimal Disc { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }
        public float Tot2 { get; set; }
        public int Apv { get; set; }
        public DateTime PoPurchnDate { get; set; }
        public string PoDate
        {
            get { return PoPurchnDate.ToShortDateString(); }
            set { PoPurchnDate.ToShortDateString(); }
        }
        public DateTime Dlvdate { get; set; }
        public string DeliveryDate
        {
            get { return Dlvdate.ToShortDateString(); }
            set { Dlvdate.ToShortDateString(); }
        }
        public string groupdesc { get; set; }
        public int Cetak { get; set; }
        public string Remark { get; set; }
        public string MataUang { get; set; }
        public string Lambang { get; set; }
        public string ItemCode { get; set; }
        public string Alamat { get; set; }
        public string UP { get; set; }
        public string Telepon { get; set; }
        public string Fax { get; set; }
        public string Termin { get; set; }
        public string Delivery { get; set; }
        public int DepoID { get; set; }
    }
}
