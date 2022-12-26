using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SerahTerimaAssetNf
    {
        public class ParamAsset
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamGetAsset
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public int AMGroupID { get; set; }
            public int AMclassID { get; set; }
            public int AMsubClassID { get; set; }
            public int AMlokasiID { get; set; }
            public int AssetID { get; set; }
            public int UomID { get; set; }
            public int DeptID { get; set; }
        }

        public class ParamHead
        {
            //am_assetSerah
            public string ItemCode { get; set; }
            public string TanggalMulai { get; set; }
            public string TanggalSelesai { get; set; }
            public string NoAsset { get; set; }
            public DateTime TglMulaiPekerjaan { get; set; }
            public DateTime TglSelesaiPekerjaan { get; set; }
            public string UserName { get; set; }
            public int Upgrade { get; set; }
            public int Apv { get; set; }

            //am_assetSerahDetail
            public int AssetID { get; set; }
            public string KomponenCode { get; set; }
            public string KomponenItem { get; set; }
            public decimal QtyPakai { get; set; }
            public decimal Nilai { get; set; }

            //amAsset
            public int AMGroupID { get; set; }
            public int AMclassID { get; set; }
            public int AMsubClassID { get; set; }
            public int AMlokasiID { get; set; }
            public string ItemName { get; set; }
            public DateTime AssyDate { get; set; }
            public string NilaiAsset { get; set; }
            public DateTime MfgDate { get; set; }
            public string MfgYear { get; set; }
            public string LifeTime { get; set; }
            public int DepreciatID { get; set; }
            public DateTime StartDeprec { get; set; }
            public string ItemKode { get; set; }
            public string CreatedBy { get; set; }
            public int PicDept { get; set; }
            public int PlantID { get; set; }
            public int RowStatus { get; set; }
            public int TipeAsset { get; set; }
            public int UomID { get; set; }
            public int OwnerDeptID { get; set; }
            public int DeptID { get; set; }

            //Ba
            public string NoBA { get; set; }
            public string Tanggal { get; set; }
            public DateTime CreatedTime { get; set; }
            public int ItemID { get; set; }
        }

        public class ParamDtl
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public int QtyPakai { get; set; }
            public decimal Price { get; set; }
        }

        public class isiDetail
        {
            public List<ParamDtl> isiDtl { get; set; }
        }

        public class ParamInfoDetail
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public int QtyPakai { get; set; }
            public string UomCode { get; set; }
            public decimal Price { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class ParamInfoAsset
        {
            public string TglMulai { get; set; }
            public string TglSelesai { get; set; }
            public string DeptPemilikAsset { get; set; }
            public string DeptPembuatAsset { get; set; }
            public string StatusAsset { get; set; }
            public string BtnAprov { get; set; }
            public string BtnSerah { get; set; }
        }
    }
}
