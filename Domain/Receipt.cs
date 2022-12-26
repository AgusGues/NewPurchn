using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{

    public class Receipt : GRCBaseDomain
    {
        #region private variable
        private string receiptNo = string.Empty;
        private DateTime receiptDate = DateTime.Now.Date;
        private int receiptType = 0;
        private int supplierType = 0;
        private int supplierID = 0;
        private int depoID = 0;
        private DateTime paymentDate = DateTime.Now.Date;
        private string keterangan1 = string.Empty;
        private string keterangan2 = string.Empty;
        private DateTime approvalDate = DateTime.Now.Date;
        private DateTime tTagihanDate = DateTime.Now.Date;
        private DateTime jTempoDate = DateTime.Now.Date;
        private string approvalBy = string.Empty;
        private int status = 0;
        private int poID = 0;
        private string poNo = string.Empty;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private decimal quantity = 0;
        private string sppNo = string.Empty;
        private string alasanCancel = string.Empty;
        private int itemTypeID = 0;
        private string supplierName = string.Empty;
        private string fakturPajak = string.Empty;
        private string keteranganpay = string.Empty;
        private string supplierCode = string.Empty;
        private decimal qtyPO = 0;
        private DateTime? fakturPajakDate = null ;
        private string invoiceNo = string.Empty;
        private string currency = string.Empty;
        private string npwp = string.Empty;
        private decimal total = 0;
        private decimal pPN = 0;
        private decimal tagihan = 0;
        private decimal kursPajak = 0;
        #endregion

        public string Currency { get { return currency; } set { currency = value; } }
        public string NPWP { get { return npwp; } set { npwp = value; } }
        public decimal Total { get { return total; } set { total = value; } }
        public decimal PPN { get { return pPN; } set { pPN = value; } }
        public decimal Tagihan { get { return tagihan; } set { tagihan = value; } }
        public decimal KursPajak { get { return kursPajak; } set { kursPajak = value; } }
        public int QtyBPAS { get; set; }
        public decimal QtyTimbang { get; set; }
        public string keterangan { get; set; }
        public int tipeAsset { get; set; }

        public int TipeAsset
        {
            get { return tipeAsset; }
            set { tipeAsset = value; }
        }
        public DateTime? FakturPajakDate
        {
            get { return fakturPajakDate; }
            set { fakturPajakDate = value; }
        }
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        public decimal QtyPO
        {
            get { return qtyPO; }
            set { qtyPO = value; }
        }
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }
        public string SupplierCode
        {
            get { return supplierCode; }
            set { supplierCode = value; }
        }
        public string FakturPajak 
        {
            get { return fakturPajak ; }
            set { fakturPajak = value; }
        }
        public string Keteranganpay
        {
            get { return keteranganpay ; }
            set { keteranganpay = value; }
        }
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public string AlasanCancel
        {
            get { return alasanCancel; }
            set { alasanCancel = value; }
        }
        public string SppNo
        {
            get { return sppNo; }
            set { sppNo = value; }
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
        public int PoID
        {
            get
            {
                return poID;
            }
            set
            {
                poID = value;
            }
        }
        public string PoNo
        {
            get
            {
                return poNo;
            }
            set
            {
                poNo = value;
            }
        }
        public int ReceiptType
        {
            get { return receiptType; }
            set { receiptType = value; }
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
        public string ReceiptNo
        {
            get
            {
                return receiptNo;
            }
            set
            {
                receiptNo = value;
            }
        }
        public DateTime ReceiptDate
        {
            get { return receiptDate; }
            set { receiptDate = value; }
        }
        public DateTime TTagihanDate
        {
            get { return tTagihanDate ; }
            set { tTagihanDate = value; }
        }
        public DateTime JTempoDate 
        {
            get { return jTempoDate ; }
            set { jTempoDate = value; }
        }
        public int SupplierType
        {
            get
            {
                return supplierType;
            }
            set
            {
                supplierType = value;
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
        public DateTime PaymentDate
        {
            get
            {
                return paymentDate;
            }
            set
            {
                paymentDate = value;
            }
        }
        public string Keterangan1
        {
            get
            {
                return keterangan1;
            }
            set
            {
                keterangan1 = value;
            }
        }
        public string Keterangan2
        {
            get
            {
                return keterangan2;
            }
            set
            {
                keterangan2 = value;
            }
        }
        public DateTime ApprovalDate
        {
            get
            {
                return approvalDate;
            }
            set
            {
                approvalDate = value;
            }
        }
        public string ApprovalBy
        {
            get
            {
                return approvalBy;
            }
            set
            {
                approvalBy = value;
            }
        }
        /** AgenLapak
         */
        public int AgenID { get; set; }
        public string NoSJ { get; set; }
        public int KirimID { get; set; }
        public string NoPOL { get; set; }
        /** Kadar Air
         */
        public decimal Gross { get; set; }
        public decimal Netto { get; set; }
        public decimal KadarAir { get; set; }
        public int ItemID { get; set; }
        public decimal JmlBal { get; set; }
        public int BAID { get; set; }
        public int Approval { get; set; }
        public int SubCompanyID { get; set; }
        public decimal StdKadarAir { get; set; }
    }
    public class BeritaAcara : Receipt
    {
        public DateTime BADate { get; set; }
        public int FromDept { get; set; }
        public int ToDept { get; set; }
        public string BANum { get; set; }
        public string Subject { get; set; }
        public string JenisBarang { get; set; }
        public decimal TotalSup { get; set; }
        public decimal KadarAirBPAS { get; set; }
        public decimal TotalBPAS { get; set; }
        public decimal JmlBalBPAS { get; set; }
        public decimal Selisih { get; set; }
        public decimal ProsSelisih { get; set; }
        public string Keterangan { get; set; }
        public string BAAttn { get; set; }
        public decimal SelisihBal { get; set; }
        public int DepoKertasID { get; set; }
        public string DepoKertasName { get; set; }
        public string BANumber { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public decimal Sampah { get; set; }
    }
    public class RekapAsset : Receipt
    {
        public decimal AdjustIN { get; set; }
        public decimal AdjustOut { get; set; }
        public decimal SaldoAwal { get; set; }
        public decimal MutasiIN { get; set; }
        public decimal MutasiOut { get; set; }
        public decimal SaldoAkhir { get; set; }
        public decimal Pembelian { get; set; }
        public decimal SPB { get; set; }
        public decimal StockGudang { get; set; }
        public string Kategori { get; set; }
    }
    public class ApprovalBA : BeritaAcara
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string AppStatus { get; set; }
    }
    public class AttachmentBA : ApprovalBA
    {
        public string FileName { get; set; }
        public Byte[] Attachment { get; set; }
        public string DocName { get; set; }
    }
    public class PemantauanBA : GRCBaseDomain
    {
        public string BANum { get; set; }
        public string ItemName { get; set; }
        public string SupplierName { get; set; }
        public string RMSNo { get; set; }
        public string Nopol { get; set; }
        public string Keterangan { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime TglBABPAS { get; set; }
        public DateTime TglBADepo { get; set; }
        public int SelisihBA { get; set; }
        public decimal NetDepo { get; set; }
        public decimal NetBPAS { get; set; }
        public decimal NetSelisih { get; set; }
        public decimal NetProsen { get; set; }
        public decimal GrossDepo { get; set; }
        public decimal GrossBPAS { get; set; }
        public decimal GrossSelisih { get; set; }
        public decimal GrossProsen { get; set; }
        public decimal BalDepo { get; set; }
        public decimal BalBPAS { get; set; }
        public decimal BalSelisih { get; set; }
        public decimal BalProsen { get; set; }
        public decimal KADepo { get; set; }
        public decimal KABpas { get; set; }
        public decimal KASelisih { get; set; }
        public int BAID { get; set; }
        public string DepoName { get; set; }
        public decimal Persen { get; set; }
    }
    public class RekapPoKertas : Receipt
    {
        public string ReceiptNo { get; set; }
        public DateTime TglRMS { get; set; }
        public string PONo { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal BeratKotor { get; set; }
        public decimal StdKA { get; set; }
        public decimal AktualKA { get; set; }
        public decimal Quantity { get; set; }
        public decimal Sampah { get; set; }
        public decimal BeratBersih { get; set; }
    }
    public class PembuatanBA : PemantauanBA
    {
        public  DateTime PODate { get; set; }
        public  DateTime AttDate { get; set; }
        public string Tipe { get; set; }
    }
}
