using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RekapPakaiNf
    {
        public class ParamGroupItem
        {
            public int ID { get; set; }
            public string GroupDescription { get; set; }
        }

        public class ParamDept
        {
            public string Alias { get; set; }
        }

        public class ParamDataDetail
        {
            public string DeptName { get; set; }
            public string GroupDescription { get; set; }
            public string PakaiNo { get; set; }
            public string PakaiDate { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public string Jumlah { get; set; }
            public string Harga { get; set; }
            public string Total { get; set; }
            public string Keterangan { get; set; }
            public string Status { get; set; }
            public string NoPol { get; set; }
        }
        public class ParamDataRekap
        {
            public string DeptName { get; set; }
            public string GroupDescription { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UomCode { get; set; }
            public string Jumlah { get; set; }
            public string Harga { get; set; }
            public string Total { get; set; }
        }
    }
}
