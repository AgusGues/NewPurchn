using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PakaiSparePartNf
    {
        public class ParamData
        {
            public int ID { get; set; }
            public string PakaiNo { get; set; }
            public string CreatedTime { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Quantity { get; set; }
            public string UOMDesc { get; set; }
            public string Keterangan { get; set; }
            public int Status { get; set; }

        }
        public class ParamDept
        {
            public int ID { get; set; }
            public string DeptName { get; set; }
        }

        public class ParamDeptCode
        {
            public string DeptCode { get; set; }
        }

        public class ParamCekProject
        {
            public int ProjectID { get; set; }
            public string ProjectName { get; set; }
        }

        public class ParamEstimasiMaterial
        {
            public int ID { get; set; }
            public string ItemName { get; set; }
            public int Jumlah { get; set; }
        }

        public class ParamMaterialBudget
        {
            public int JumlahMaterial { get; set; }
        }

        public class ParamCekSPB
        {
            public string PakaiNo { get; set; }
            public string PakaiDate { get; set; }
            public int Status { get; set; }
        }

        public class ParamGroup
        {
            public int ID { get; set; }
            public string ZonaName { get; set; }
            public int ZonaCode { get; set; }
        }

        public class ParammBarang
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamInventoryFacade1
        {
            public int ID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string SupplierCode { get; set; }
            public int UOMID { get; set; }
            public string UOMCode { get; set; }
            public string UOMDesc { get; set; }
            public int Jumlah { get; set; }
            public int Harga { get; set; }
            public int MinStock { get; set; }
            public int MaxStock { get; set; }
            public int DeptID { get; set; }
            public int RakID { get; set; }
            public int Gudang { get; set; }
            public string ShortKey { get; set; }
            public string Keterangan { get; set; }
            public int Head { get; set; }
            public int RowStatus { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedTime { get; set; }
            public string LastModifiedBy { get; set; }
            public string LastModifiedTime { get; set; }
            public int GroupID { get; set; }
            public int ItemTypeID { get; set; }
            public int aktif { get; set; }
            public int LeadTime { get; set; }
            public int Stock { get; set; }
        }

        public class ParamMonthAvgPrice
        {
            public int MonthAvgPrice { get; set; }
        }

        public class ParamStockOtherDept
        {
            public int StockOtherDept { get; set; }
        }

        public class ParamPendingSPB
        {
            public int PendingSPB { get; set; }
        }

        public class ParamStockAkhir
        {
            public int StockAkhir { get; set; }
        }

        public class ParamPlanning
        {
            public int Planning { get; set; }
        }

        public class ParamCheckCost
        {
            public int CheckCost { get; set; }
        }

        public class ParamMaxSPB
        {
            public int MaxSPB { get; set; }
        }

        public class ParamAddBudget
        {
            public int AddBudget { get; set; }
        }

        public class ParamRuleBudget
        {
            public int RuleBudget { get; set; }
        }

        public class ParamTotalSPB
        {
            public int TotalSPB { get; set; }
        }

        public class ParamTotalSPBPrj
        {
            public int TotalSPBPrj { get; set; }
        }

        public class ParamItemIDKhusus
        {
            public int ItemIDKhusus { get; set; }
        }

        public class ParamForklift
        {
            public int ID { get; set; }
            public string Forklift { get; set; }
        }

        public class ParamStatus
        {
            public int Status { get; set; }
        }

        public class ParamCekClosing
        {
            public int nowTgl { get; set; }
            public int lastTgl { get; set; }
            public int clsBlnStatus { get; set; }
            public string clsBulan { get; set; }
            public int clsTahun { get; set; }
        }
    }
}
