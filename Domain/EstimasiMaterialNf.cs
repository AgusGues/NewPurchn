using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EstimasiMaterialNf
    {
        public class ParamDeptPemohon
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
        }

        public class ParamData
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
            public string ProjectName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int EstimasiBiaya { get; set; }
            public string Sasaran { get; set; }
            public string KodeProject { get; set; }
            public string Approval { get; set; }
            public string Status { get; set; }
            public string RowStatus { get; set; }
            public string BiayaActual { get; set; }
        }

        public class ParamDetail
        {
            public int ProjectID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public string Jumlah { get; set; }
            public string Harga { get; set; }
            public string TotalHarga { get; set; }
            public string QtyAktual { get; set; }
            public string HargaAktual { get; set; }
            public string TotalAktual { get; set; }
            public string Selisih { get; set; }
        }

        public class ParamKodeProject
        {
            public int Id { get; set; }
            public string Nomor { get; set; }
        }

        public class ParamTypeItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ParamNameItem
        {
            public int Id { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamInfoItem
        {
            public int Id { get; set; }
            public string ItemName { get; set; }
        }

        public class ParamInfoKodeProject
        {
            public string ProjectName { get; set; }
        }

        public class ParamListMaterial
        {
            public int Id { get; set; }
            public int ProjectId { get; set; }
            public int ItemId { get; set; }
            public int ItemTypeId { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public int Jumlah { get; set; }
            public int Harga { get; set; }
            public int TotalHarga { get; set; }
            public string Schedule { get; set; }
            public int Urutan { get; set; }
        }

        public class ParamHead
        {
            public int Id { get; set; }
            public int ItemID { get; set; }
            public int Jumlah { get; set; }
            public int Harga { get; set; }
            public int ItemTypeId { get; set; }
            public string TanggalPakai { get; set; }
            public DateTime Schedule { get; set; }
            public int RowStatus { get; set; }
            public int ProjectID { get; set; }
        }

    }
}
