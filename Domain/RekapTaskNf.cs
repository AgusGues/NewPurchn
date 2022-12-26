using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RekapTaskNf
    {
        public class ParamDept
        {
            public int Id { get; set; }
            public string Alias { get; set; }
        }

        public class ParamPic
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
        }

        public class ParamData
        {
            public string UserName { get; set; }
            public int UserID { get; set; }
            public int BagianID { get; set; }
            public string BagianName { get; set; }
        }

        public class ParamDataDtl
        {
            public string UserName { get; set; }
            public string TaskNo { get; set; }
            public string TaskName { get; set; }
            public string TglMulai { get; set; }
            public string Target1 { get; set; }
            public string Target2 { get; set; }
            public string Target3 { get; set; }
            public string Target4 { get; set; }
            public string Target5 { get; set; }
            public string Target6 { get; set; }
            public string TglSelesai { get; set; }
            public string NilaiBobot { get; set; }
            public string Point { get; set; }
            public string Score { get; set; }
        }
    }
}
