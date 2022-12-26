using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RekapKpiNf
    {
        public class ParamDept
        {
            public int Id { get; set; }
            public string Alias { get; set; }
        }

        public class ParamTahun
        {
            public int Tahun { get; set; }
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
            public string Description { get; set; }
            public string Bobot { get; set; }
            public string Target { get; set; }
            public string Pencapaian { get; set; }
            public string Score { get; set; }
            public string Point { get; set; }
        }
    }
}
