using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace Domain
{
    public class POPurchnDetail:POPurchn
    {
        private int id = 0;
        private int pOID = 0;
        private int sPPID = 0;
        private int groupID = 0;
        private int itemID = 0;
        private int itemID2 = 0;
        private decimal price = 0;
        private decimal qty = 0;
        private int itemTypeID = 0;
        private int uOMID = 0;
        private int status = 0;
        private int noUrut = 0;
        private string itemName = string.Empty;
        private string itemCode = string.Empty;
        private string uomCode = string.Empty;
        private decimal stok = 0;
        private decimal jumQty = 0;
        private string noSPP = string.Empty;
        private string namaBarang = string.Empty;
        private string satuan = string.Empty;
        private string documentNo = string.Empty;
        private int sPPDetailID = 0;
        private decimal total = 0;
        private DateTime dlvDate = DateTime.Now.Date;
        public decimal Price2 { get; set; }
        private string nopol = string.Empty;

        public string NoPol
        {
            get { return nopol; }
            set { nopol = value; }
        }
        public DateTime DlvDate
        {
            get { return dlvDate; }
            set { dlvDate = value; }
        }

        public string NoSPP
        {
            get {return noSPP;}
            set{noSPP = value;}
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
        public decimal JumQty
        {
            get { return jumQty; }
            set { jumQty = value; }
        }
        public string DocumentNo
        {
            get { return documentNo; }
            set { documentNo = value; }
        }
        public decimal Stok
        {
            get { return stok;}
            set { stok = value; }
        }
        public string UOMCode
        {
            get { return uomCode; }
            set { uomCode = value; }
        }
        public string ItemCode
        {
            get
            { return itemCode; }
            set
            { itemCode = value; }
        }
        public string ItemName
        {
            get
            { return itemName; }
            set
            { itemName = value; }
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
        public int ItemID2
        {
            get { return itemID2; }
            set { itemID2 = value; }
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
        
    }
    public class KadarAir :POPurchn
    {
        public int PointStatus { get; set; }
        public string NoSJ { get; set; }
        public string IPAddress { get; set; }
    }
    public class DeliveryKertas : KadarAir
    {
        public int DepoID { get; set; }
        public string DepoName { get; set; }
        public string Checker { get; set; }
        public int PlantID { get; set; }
        public string NoSJ { get; set; }
        public DateTime TglKirim { get; set; }
        public DateTime TglETA { get; set; }
        public DateTime TglReceipt { get; set; }
        public int ReceiptID { get; set; }
        public string Expedisi { get; set; }
        public string NOPOL { get; set; }
        public decimal GrossDepo { get; set; }
        public decimal NettDepo { get; set; }
        public decimal KADepo { get; set; }
        public decimal JmlBAL { get; set; }
        public int POKAID { get; set; }
        public decimal GrossPlant { get; set; }
        public decimal NettPlant { get; set; }
        public decimal KAPlant { get; set; }
        public string DocNo { get; set; }
        public decimal SelisihKA { get; set; }
        public decimal SelisihBB { get; set; }
        public decimal Persen { get; set; }
        public int KAID { get; set; }
        public int Sesuai { get; set; }
        public int TdkSesuai { get; set; }
        public decimal SampahDepo { get; set; }
        public decimal Selisih { get; set; }
        public int IDBeli { get; set; }
        public int LMuatID { get; set; }
        public string LokasiMuat { get; set; }
        public string TujuanKirim { get; set; }
        public string JMobil { get; set; }
        public Int32 TypeByr { get; set; }
        public decimal NettPlant0 { get; set; }
        public decimal BeratPotong0 { get; set; }
        public string NoCash { get; set; }
        public string NoDP { get; set; }
        public string NoPelunasan { get; set; }
    }
    public class BayarKertas : KadarAir
    {
        public int DepoID { get; set; }
        public string DepoName { get; set; }
        public Int32 PlantID { get; set; }
        public Int32 IDBeli { get; set; }
        public string BGNo { get; set; }
        public string AnBGNo { get; set; }
        public DateTime TglBayar { get; set; }
        public DateTime JTempo { get; set; }
        public string Expedisi { get; set; }
        public string DocNo { get; set; }
        public decimal Harga { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalHarga { get; set; }
        public decimal TotalBayar { get; set; }
        public string Penerima { get; set; }
        public string CreatedBy { get; set; }
        public Int32 TypeByr { get; set; }
        public int LMuatID { get; set; }
        public int TKirim { get; set; }
        public int MinKirim { get; set; }
        public int HargaT { get; set; }
        public int DP { get; set; }
    }
    public class QAKadarAir : DeliveryKertas
    {
        public int DefaultStdKA { get; set; }
        public decimal Potongan2 { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public int ID { get; set; }

        public int NoBall { get; set; }
        public int NoBall1 { get; set; }
        public int BALKe { get; set; }
        public decimal Tusuk1 { get; set; }
        public decimal Tusuk2 { get; set; }
        public decimal AvgTusuk { get; set; }
        public decimal Tusuk11 { get; set; }
        public decimal Tusuk21 { get; set; }
        public decimal AvgTusuk1 { get; set; }
        public int BALKe1 { get; set; }
        public decimal JmlSample { get; set; }
        public decimal JmlSampleBasah { get; set; }
        public decimal ProsSampleBasah { get; set; }
        public decimal AvgKA { get; set; }
        public decimal AvgKA1 { get; set; }
        public decimal Potongan { get; set; }
        public decimal BeratPotong { get; set; }
        public int Keputusan { get; set; }
        public DateTime TglCheck { get; set; }
        public int DKKAID { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public decimal BeratSample { get; set; }
        public decimal BeratSampah { get; set; }
        public decimal Beli { get; set; }
        public decimal Bayar { get; set; }
        public decimal Harga { get; set; }
        public decimal Total { get; set; }
        public decimal StdKA { get; set; }
        public int Approval { get; set; }
        public int LMuatID { get; set; }
        public string NoSJ { get; set; }
        public string status { get; set; }
        public string alasan { get; set; }

        public int CompareTo(QAKadarAir Qa1, ObjCompare.ComparisonType comparisonType, QAKadarAir qa2)
        {
            int result = 0;
            if (comparisonType == ObjCompare.ComparisonType.BalKe)
            {
                result = BALKe.CompareTo(Qa1.BALKe);
            }
            return result;
        }
    }
    public class ObjCompare : IComparer
    {
        public enum ComparisonType
        {
            BalKe
        }
        public ComparisonType CompareMethod { get; set; }
        public int Compare(object x, object y)
        {
            QAKadarAir qa1 = (QAKadarAir)x;
            QAKadarAir qa2 = (QAKadarAir)y;
            return qa2.CompareTo(qa1, CompareMethod, qa2);
        }
    }
    
}
