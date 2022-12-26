using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PoPurchnNf
    {
        public class ParamCekAddItem
        {
            public string Item { get; set; }
        }

        public class ParamHeadData
        {
            public int ID { get; set; }
            public string NoPo { get; set; }
            public string MataUang { get; set; }
            public string TermOfPay { get; set; }
            public string PPN { get; set; }
            public string PPH { get; set; }
            public string Discount { get; set; }
            public string TermOfDelivery { get; set; }
            public string Bayar { get; set; }
            public string SupplierName { get; set; }
            public string UPSupplier { get; set; }
            public string TelpSupplier { get; set; }
            public string FaxSupplier { get; set; }
            public string TanggalPo { get; set; }
            public string OngkosKirim { get; set; }
        }

        public class ParamDtlData
        {
            public int ID { get; set; }
            public string NoSpp { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Quantity { get; set; }
            public string Satuan { get; set; }
            public string Harga { get; set; }
            public string DlvDate { get; set; }
        }

        public class ParamDataPo
        {
            public int ID { get; set; }
            public string NoPo { get; set; }
            public string NoSpp { get; set; }
            public string TanggalPo { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Qty { get; set; }
            public string UomDesc { get; set; }
            public string Price { get; set; }
        }
        public class ParamListNoSpp
        {
            public int ID { get; set; }
            public string NoSpp { get; set; }
        }

        public class ParamListSppDetailBySppId
        {
            public int ID { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamInfoItem
        {
            public int ID { get; set; }
            public int UOMID { get; set; }
            public int GroupID { get; set; }
            public int TypeSpp { get; set; }
            public string ItemName { get; set; }
            public int ItemID { get; set; }
            public int ItemTypeID { get; set; }
            public decimal Qty { get; set; }
            public string SatuanName { get; set; }
            public string DeliveryDate { get; set; }
            public string ItemCode { get; set; }
            public string IdBiayaName { get; set; }
            public decimal Harga { get; set; }
            public string LeadTime { get; set; }
            public int MataUang { get; set; }
            public string Msg { get; set; }
        }

        public class ParamSppDetailById
        {
            public int ID { get; set; }
            public int SPPID { get; set; }
            public int GroupID { get; set; }
            public int Quantity { get; set; }
            public decimal QtyPO { get; set; }
            public int ItemTypeID { get; set; }
            public int ItemID { get; set; }
            public int UOMID { get; set; }
            public string Satuan { get; set; }
            public string ItemCode { get; set; }
            public string Keterangan { get; set; }
            public int Status { get; set; }

        }

        public class ParamlastPrice
        {
            public int Price { get; set; }
            public int Crc { get; set; }
            public string Lambang { get; set; }
        }

        public class ParamHargaRendah
        {
            public string Price { get; set; }
            public string SupplierName { get; set; }
        }

        public class ParamListSup
        {
            public int ID { get; set; }
            public string SupplierName { get; set; }
        }

        public class ParamTermOfPayment
        {
            public int ID { get; set; }
            public string Termin { get; set; }
        }

        public class ParamListDlvKertas
        {
            public int ID { get; set; }
            public string NoSj { get; set; }
        }

        public class ParamInfoKadarAir
        {
            public int Gross { get; set; }
            public int KadarAir { get; set; }
            public int Netto { get; set; }
            public int Sampah { get; set; }
            public string NoPol { get; set; }
        }

        public class ParamInfoSup
        {
            public string UPSupplier { get; set; }
            public string TelpSupplier { get; set; }
            public string FaxSupplier { get; set; }
            public decimal Harga { get; set; }
            public int MataUang { get; set; }
            public string MataUangName { get; set; }
            public int PPN { get; set; }
            public string TermOfPay { get; set; }
            public int TermOfPayID { get; set; }
            public int SubCompanyID { get; set; }
            public int ForDK { get; set; }
            public int PosKertas { get; set; }
        }

        public class ParamCekQtyPo
        {
            public int Quantity { get; set; }
            public decimal QtyPo { get; set; }
            public int QtySudahPo { get; set; }
        }

        public class ParamInfoSupById
        {
            public string Npwp { get; set; }
            public string UPSupplier { get; set; }
            public string TelpSupplier { get; set; }
            public string FaxSupplier { get; set; }
            public int SubCompanyID { get; set; }
            public int ForDK { get; set; }
        }

        public class ParamListMataUang
        {
            public int ID { get; set; }
            public string Lambang { get; set; }
        }

        public class ParamListTermOfPay
        {
            public int ID { get; set; }
            public string TermPay { get; set; }
        }

        public class ParamListIndent
        {
            public int ID { get; set; }
            public string Tenggang { get; set; }
        }

        public class ParamAddItem
        {
            public decimal HargaKertas { get; set; }
        }

        public class ParamSppNumber
        {
            public decimal HargaKertas { get; set; }
            public DateTime LastModifiedTime { get; set; }
            public int POCounter { get; set; }
            public string KodeCompany { get; set; }
            public string KodeSPP { get; set; }
            public string LastModifiedBy { get; set; }
            public int ID { get; set; }
            public int Flag { get; set; }
        }

        public class ParamHead
        {
            public int ID { get; set; }
            public string NoPO { get; set; }
            public int SupplierID { get; set; }
            public DateTime CreatedTime { get; set; }
            public DateTime ApproveDate1 { get; set; }
            public DateTime ApproveDate2 { get; set; }
            public string TanggalPo { get; set; }
            public string Terbilang { get; set; }
            public int TermOpPay { get; set; }
            public string Termin { get; set; }
            public string AlasanBatal { get; set; }
            public string AlasanClose { get; set; }
            public string AlasanNotApproval { get; set; }
            public string TermOpPayName { get; set; }
            public string KetTermOfPay { get; set; }
            public string Indent { get; set; }
            public string Delivery { get; set; }
            public int Crc { get; set; }
            public int MataUang { get; set; }
            public string Keterangan { get; set; }
            public decimal PPN { get; set; }
            public decimal PPH { get; set; }
            public decimal Disc { get; set; }
            public decimal Discount { get; set; }
            public decimal Ongkos { get; set; }
            public decimal OngkosKirim { get; set; }
            public decimal UangMuka { get; set; }
            public int CoaID { get; set; }
            public int Cetak { get; set; }
            public int CountPrt { get; set; }
            public int Status { get; set; }
            public int Approval { get; set; }
            public string CreatedBy { get; set; }
            public string Remark { get; set; }
            public decimal NilaiKurs { get; set; }
            public int PaymentType { get; set; }
            public int ItemFrom { get; set; }
            public int Barang { get; set; }
            public int GroupID { get; set; }
            public int SubCompanyID { get; set; }
            public decimal TotalPrice { get; set; }
            public string ItemCode { get; set; }
            public decimal StdKA { get; set; }
            public decimal Gross { get; set; }
            public decimal KadarAir { get; set; }
            public decimal Netto { get; set; }
            public decimal Sampah { get; set; }
            public string NoMobil { get; set; }
        }

        public class ParamDtl
        {
            public int POID { get; set; }
            public int SPPID { get; set; }
            public int GroupID { get; set; }
            public int ItemID { get; set; }
            public int ItemTypeID { get; set; }
            public int UOMID { get; set; }
            public int Status { get; set; }
            public int NoUrut { get; set; }
            public DateTime DlvDate { get; set; }
            public string DeliveryDate { get; set; }
            public int SPPDetailID { get; set; }
            public string DocumentNo { get; set; }
            public string ItemCode { get; set; }
            public decimal Price { get; set; }
            public decimal Qty { get; set; }
        }
        public class isiDetail
        {
            public List<ParamDtl> isiDtl { get; set; }
        }

        public class ParamKdr
        {
            public decimal StdKA { get; set; }
            public decimal Gross { get; set; }
            public decimal AktualKA { get; set; }
            public decimal Netto { get; set; }
            public string NoPol { get; set; }
            public decimal Sampah { get; set; }
            public string SchNo { get; set; }
            public int SchID { get; set; }
        }

        public class isiKadar
        {
            public List<ParamKdr> isiKdr { get; set; }
        }

        public class DeliveryKertas 
        {
            public int ID { get; set; }
            public int POKAID { get; set; }
            public decimal GrossPlant { get; set; }
            public decimal NettPlant { get; set; }
            public decimal KAPlant { get; set; }
            public string LastModifiedBy { get; set; }
            public DateTime LastModifiedTime { get; set; }
            public DateTime TglReceipt { get; set; }
        }

        public class QAKadarAir
        {
            public int ID { get; set; }
            public int POKAID { get; set; }
            public string LastModifiedBy { get; set; }
            public DateTime LastModifiedTime { get; set; }
        }

        public class ByNo
        {
            public int PODetailID { get; set; }
            public int ID { get; set; }
        }

        public class ByID
        {
            public int ItemID { get; set; }
        }

        public class Um
        {
            public int UangMuka { get; set; }
            public int CoaID { get; set; }
        }
        public class TerminBayar
        {
            public decimal DP { get; set; }
            public int TerminKe { get; set; }
            public int JmlTermin { get; set; }
            public decimal Persentase { get; set; }
            public decimal TotalBayar { get; set; }
        }

        public class ParamDataSpp
        {
            public int ID { get; set; }
            public string KodeSpp { get; set; }
            public string TanggalSpp { get; set; }
            public string TypeSpp { get; set; }
            public string UserHead { get; set; }
            public string ApvDate { get; set; }
            public int Detail { get; set; }
        }

        public class ParamDetailSpp
        {
            public int ID { get; set; }
            public string KodeSpp { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Satuan { get; set; }
            public decimal QtySpp { get; set; }
            public decimal QtyPo { get; set; }
            public string DeliveryDate { get; set; }
            public int LeadTime { get; set; }
            public string AlasanPending { get; set; }
        }
    }//_________________
}
