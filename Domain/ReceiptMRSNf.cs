using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReceiptMRSNf
    {
        public class ParamAddItem
        {
            public string Item { get; set; }
        }
        public class ParamHeadData
        {
            public string KodeReceipt { get; set; }
            public string Tanggal { get; set; }
        }
        public class ParamDtlData
        {
            public int ID { get; set; }
            public string KodePO { get; set; }
            public string KodeSpp { get; set; }
            public string Quantity { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Satuan { get; set; }
            public string Keterangan { get; set; }
        }
        public class ParamData
        {
            public int ID { get; set; }
            public string KodeReceipt { get; set; }
            public string TanggalReceipt { get; set; }
            public string KodePO { get; set; }
            public string KodeSpp { get; set; }
            public string Quantity { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Satuan { get; set; }
            public string Status { get; set; }
        }
        public class ParamPo
        {
            public int PoId { get; set; }
            public string NoPo { get; set; }
        }
        public class ParamItem
        {
            public int ID { get; set; }
            public string ItemName { get; set; }
        }
        public class ParamInfoItem
        {
            public string ItemCode { get; set; }
            public decimal Stock { get; set; }
            public string Satuan { get; set; }
            public decimal QtyPo { get; set; }
            public string NoSpp { get; set; }
            public int Status { get; set; }
            public string Suplier { get; set; }
        }

        public class ParamCekClosing
        {
            public int Bulan { get; set; }
            public int Tahun { get; set; }
            public int Status { get; set; }
        }

        public class ParamInfoDetailPo
        {
            public decimal Price { get; set; }
            public int ItemID { get; set; }
            public int SppID { get; set; }
            public string DocumentNo { get; set; }
            public int PoID { get; set; }
            public int UomID { get; set; }
            public int GroupID { get; set; }
            public int ItemTypeID { get; set; }
        }
        public class ParamDocNo
        {
            public int ID { get; set; }
            public int NoUrut { get; set; }
            public int MonthPeriod { get; set; }
            public int YearPeriod { get; set; }
            public string ReceiptCode { get; set; }
        }
        public class ParamPoPurchn
        {
            public int SupplierID { get; set; }
            public int ID { get; set; }
            public string NoPO { get; set; }
            public int TypeReceipt { get; set; }
        }
        public class ParamSisaPo
        {
            public int ID { get; set; }
            public decimal QtyPO { get; set; }
            public decimal QtyReceipt { get; set; }
            public int Status { get; set; }
            public int PoID { get; set; }
        }
        public class ParamSppIdDtl
        {
            public int ID { get; set; }
            public int SatuanID { get; set; }
            public int UserID { get; set; }
        }
        public class ParamHead
        {
            public int ID { get; set; }
            public string Tanggal { get; set; }
            public int TypeReceipt { get; set; }
            public int SupplierType { get; set; }
            public int SupplierID { get; set; }
            public int PoID { get; set; }
            public string PoNo { get; set; }
            public string NoPo { get; set; }
            public string ReceiptNo { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int ReceiptType { get; set; }
            public int DepoID { get; set; }
            public int Status { get; set; }
            public int ItemTypeID { get; set; }
            public int tipeAsset { get; set; }
            public string CreatedBy { get; set; }
            public string AlasanCancel { get; set; }
            public string LastModifiedBy { get; set; }
            public DateTime TTagihanDate { get; set; }
            public DateTime JTempoDate { get; set; }
            public string FakturPajak { get; set; }
            public string Keteranganpay { get; set; }
            public DateTime FakturPajakDate { get; set; }
            public string InvoiceNo { get; set; }
            public decimal KursPajak { get; set; }
        }
        public class ParamDtl
        {
            public int ID { get; set; }
            public int Bulan { get; set; }
            public int Tahun { get; set; }
            public int Status { get; set; }
            public int TimbanganBPAS { get; set; }
            public decimal ReceiptID { get; set; }
            public int ItemID { get; set; }
            public decimal Quantity { get; set; }
            public decimal Kadarair { get; set; }
            public decimal Price { get; set; }
            public decimal Disc { get; set; }
            public decimal TotalPrice { get; set; }
            public int PoID { get; set; }
            public string PoNo { get; set; }
            public int SppID { get; set; }
            public string SppNo { get; set; }
            public int UomID { get; set; }
            public int PODetailID { get; set; }
            public int RowStatus { get; set; }
            public int GroupID { get; set; }
            public int ItemTypeID { get; set; }
            public string Keterangan { get; set; }
            public string ItemID2 { get; set; }
            public decimal QtyTimbang { get; set; }
            public int KodeTimbang { get; set; }
            public int TipeAsset { get; set; }
            public int DOID { get; set; }
            public string ScheduleNo { get; set; }
        }
        public class isiDetail
        {
            public List<ParamDtl> isiDtl { get; set; }
        }
    }
}
