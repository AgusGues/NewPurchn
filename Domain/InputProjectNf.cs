using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InputProjectNf
    {
        public class ParamHead
        {
            public string Tanggal { get; set; }
            public string NamaProject { get; set; }
            public DateTime ProjectDate { get; set; }
            public DateTime FinishDate { get; set; }
            public int DeptID { get; set; }
            public string Sasaran { get; set; }
            public int GroupID { get; set; }
            public int ProdLine { get; set; }
            public string CreatedBy { get; set; }
            public int Quantity { get; set; }
            public int UOMID { get; set; }
            public string Nomor { get; set; }
            public int Approval { get; set; }
            public string DetailSasaran { get; set; }
            public string Zona { get; set; }
            public int ToDept { get; set; }
            public string NamaHead { get; set; }
        }

        public class ParamGroupProject
        {
            public int Id { get; set; }
            public string NamaGroup { get; set; }
        }

        public class ParamDocNo
        {
            public int Id { get; set; }
            public int Count { get; set; }
        }

        public class ParamData
        {
            public string DeptName { get; set; }
            public string ProjectName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Biaya { get; set; }
            public string Sasaran { get; set; }
            public string Nomor { get; set; }
            public string Approval { get; set; }
            public string Status { get; set; }
            public string RowStatus { get; set; }
            public string BiayaActual { get; set; }
        }

        public class ParamDeptPemohon
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
        }

        public class ParamAreaProject
        {
            public int Id { get; set; }
            public string ZonaName { get; set; }
        }

        public class ParamSatuan
        {
            public int Id { get; set; }
            public string UomDesc { get; set; }
        }

        public class ParamListHead
        {
            public string NamaHead { get; set; }
        }

        public class ParamSubArea
        {
            public int Id { get; set; }
            public string AreaName { get; set; }
        }
    }
}
