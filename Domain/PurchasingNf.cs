using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PurchasingNf
    {
        public class ParamCekAddItem
        {
            public int Item { get; set; }
        }

        public class ParamDeadStock
        {
            public string Plant { get; set; }
            public string ItemCode { get; set; }
            public decimal Stock { get; set; }
        }

        public class ParamHeadData
        {
            public int ID { get; set; }
            public string TypeItem { get; set; }
            public string CreateUser { get; set; }
            public string HeadUser { get; set; }
            public string StatusSpp { get; set; }
            public string ApprovalSpp { get; set; }
            public string MultiGudangName { get; set; }
            public string TypeMinta { get; set; }
            public string NoSpp { get; set; }
            public string Tanggal { get; set; }
        }

        public class ParamDtlData
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Satuan { get; set; }
            public string Keterangan { get; set; }
            public decimal Quantity { get; set; }
            public string TglKirim { get; set; }
        }

        public class ParamDataSPP
        {
            public int ID { get; set; }
            public string NoSPP { get; set; }
            public string CreatedTime { get; set; }
            public string Minta { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Quantity { get; set; }
            public string UOMDesc { get; set; }
            public string Keterangan { get; set; }
            public int Approval { get; set; }
            public int Status { get; set; }
        }

        public class ParamDeptUser
        {
            public int ID { get; set; }
            public string DeptName { get; set; }

        }

        public class ParamListTypeItem
        {
            public int ID { get; set; }
            public string TypeDescription { get; set; }

        }

        public class ParamListTypeSpp
        {
            public int ID { get; set; }
            public string GroupDescription { get; set; }

        }

        public class ParamListAsset
        {
            public int ID { get; set; }
            public string KodeProjectAsset { get; set; }
            public string NamaProjectAsset { get; set; }

        }

        public class ParamListKomponen
        {
            public int ID { get; set; }
            public string ItemName { get; set; }

        }

        public class ParamListItem
        {
            public int ID { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }

        }

        public class ParamListGroupSarmut
        {
            public int ID { get; set; }
            public string ZonaName { get; set; }

        }

        public class ParamListForklif
        {
            public string ID { get; set; }
            public string Forklift { get; set; }
        }

        public class ParamListGroupAsset
        {
            public int ID { get; set; }
            public string NamaGroup { get; set; }

        }

        public class ParamListKelasAsset
        {
            public int ID { get; set; }
            public string NamaClass { get; set; }

        }

        public class ParamNoPol
        {
            public int ID { get; set; }
            public string KendaraanNo { get; set; }
            public string Type { get; set; }

        }

        public class ParamListGroupEfisien
        {
            public int ID { get; set; }
            public string GroupName { get; set; }

        }

        public class ParamInfoItem
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public decimal StockItem { get; set; }
            public decimal MaxStock { get; set; }
            public string Satuan { get; set; }
            public int UOMID { get; set; }
            public string JenisStock { get; set; }
            public int MultiGudang { get; set; }
            public string LastPo { get; set; }
            public string LastRms { get; set; }
            public int LeadTime { get; set; }
            public string TanggalKirim { get; set; }
            public int Minta { get; set; }
            public string Msg { get; set; }

        }

        public class ParamPurchnTools
        {
            public int Status { get; set; }
        }

        public class ParamInsertBiaya
        {
            public string Msg { get; set; }
        }

        public class ParamAddItem
        {
            public string Msg { get; set; }
            public string NoPol { get; set; }
            public int ItemID { get; set; }
            public int GroupID { get; set; }
            public decimal Quantity { get; set; }
            public int ItemTypeID { get; set; }
            public int UOMID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Satuan { get; set; }
            public decimal QtyPO { get; set; }
            public string Keterangan { get; set; }
            public string TglKirim1 { get; set; }
            public string TypeBiaya { get; set; }
            public string Keterangan1 { get; set; }
            public int AmGroupID { get; set; }
            public int AmClassID { get; set; }
            public int AmSubClassID { get; set; }
            public int AmLokasiID { get; set; }
            public int MTCGroupSarmutID { get; set; }
            public int MaterialMTCGroupID { get; set; }
            public int UmurEkonomis { get; set; }
        }

        public class DomainAsset
        {
            public int KodeAssetKomponen { get; set; }
            public int ID { get; set; }
            public string NamaAsset { get; set; }
            public string KodeProjectAsset { get; set; }
            public string NamaProjectAsset { get; set; }
        }


        public class ParamListBiaya
        {
            public string ID { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamDataBiaya
        {
            public string ID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamItemBiaya
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public decimal StockItem { get; set; }
            public decimal MaxStock { get; set; }
            public string Satuan { get; set; }
            public int SatuanID { get; set; }
            public string TanggalKirim { get; set; }
            public string Msg { get; set; }
        }

        public class ParamHead
        {
            public int ID { get; set; }
            public string NoSPP { get; set; }
            public DateTime Minta { get; set; }
            public int ItemID { get; set; }
            public int SatuanID { get; set; }
            public int GroupID { get; set; }
            public int ItemTypeID { get; set; }
            public decimal Jumlah { get; set; }
            public decimal JumlahSisa { get; set; }
            public string Keterangan { get; set; }
            public int Sudah { get; set; }
            public int FCetak { get; set; }
            public int UserID { get; set; }
            public int Pending { get; set; }
            public int Inden { get; set; }
            public string AlasanBatal { get; set; }
            public string AlasanCLS { get; set; }
            public int Status { get; set; }
            public int Approval { get; set; }
            public int DepoID { get; set; }
            public DateTime ApproveDate1 { get; set; }
            public DateTime ApproveDate2 { get; set; }
            public DateTime ApproveDate3 { get; set; }
            public int Aprv { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedTime { get; set; }
            public string LastModifiedBy { get; set; }
            public DateTime LastModifiedTime { get; set; }
            public DateTime TglBuat { get; set; }
            public DateTime Tanggal { get; set; }
            public int PermintaanType { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public string UserName { get; set; }
            public string HeadName { get; set; }
            public int HeadID { get; set; }
            public string Gudang { get; set; }
            public string Nopol { get; set; }
            public string Useradmin { get; set; }
            public string Userhead { get; set; }

            public int TypeSpp { get; set; }
            public string TanggalInput { get; set; }
            public int TypeItem { get; set; }
            public int MultiGudang { get; set; }
            public int TypeMinta { get; set; }

        }
       
        public class ParamDtl
        {
            public int ID { get; set; }
            public int SPPID { get; set; }
            public int GroupID { get; set; }
            public int ItemID { get; set; }
            public int ItemTypeID { get; set; }
            public int UOMID { get; set; }
            public decimal Quantity { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public string Satuan { get; set; }
            public int Status { get; set; }
            public decimal QtyPO { get; set; }
            public string CariItemName { get; set; }
            public string Keterangan { get; set; }
            public string Keterangan1 { get; set; }
            public decimal QtySisa { get; set; }
            public DateTime TglKirim { get; set; }
            public int TglKirim1 { get; set; }
            public string TypeBiaya { get; set; }
            public int AmGroupID { get; set; }
            public int AmClassID { get; set; }
            public int AmSubClassID { get; set; }
            public int AmLokasiID { get; set; }
            public string AmKodeAsset { get; set; }
            public int MTCGroupSarmutID { get; set; }
            public int MaterialMTCGroupID { get; set; }
            public int UmurEkonomis { get; set; }
            public string NoPol { get; set; }
        }
        public class isiDetail
        {
            public List<ParamDtl> isiDtl { get; set; }
        }
    }
}
