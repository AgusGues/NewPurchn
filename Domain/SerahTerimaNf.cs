using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SerahTerimaNf
    {
        public class ParamHead
        {
            public int Id { get; set; }
            public int RowStatus { get; set; }
            public int Status { get; set; }
            public int Approval { get; set; }
            public string TanggalSelesai { get; set; }
            public DateTime FinishDate { get; set; }
            public string CreatedBy { get; set; }
            public string Statuse { get; set; }

        }

        public class ParamProject
        {
            public int Id { get; set; }
            public string ProjectKode { get; set; }
            public string ProjectName { get; set; }
        }

        public class ParamInfoProject
        {
            public int Id { get; set; }
            public string ProjectKode { get; set; }
            public string ProjectName { get; set; }
            public int Quantity { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string StatusProject { get; set; }
            public decimal BiayaActual { get; set; }
            public string DeptPemohon { get; set; }
        }

        public class ParamInfoDetail
        {
            public string UomCode { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public decimal Jumlah { get; set; }
            public string Schedule { get; set; }
        }

    }
}
