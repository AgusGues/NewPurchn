using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LapWorkOrderNf
    {

        public class ParamHakAkses
        {
            public int StatusReport { get; set; }
            public int StatusApv { get; set; }
        }

        public class ParamTahun
        {
            public int Tahun { get; set; }
        }

        public class ParamData
        {
            public string DeptName { get; set; }
        }

        public class ParamDataWo
        {
            public string UsersWO { get; set; }
            public string NoWO { get; set; }
            public string UraianPekerjaan { get; set; }
            public string AreaWO { get; set; }
            public string CreatedTime { get; set; }
            public string ApvMgr { get; set; }
            public string Waktu2 { get; set; }
            public string DueDateWO { get; set; }
            public string FinishDate2 { get; set; }
            public string Pelaksana { get; set; }
            public string StatusApv { get; set; }
            public string SisaHari { get; set; }
            public string StatusWO { get; set; }
            public string CreatedBy { get; set; }
            public string Selisih { get; set; }
        }

        public class ParamPencapaianNilai
        {
            public string txtTotal { get; set; }
            public string txtTarget { get; set; }
            public string txtPersen { get; set; }
            public string KetTotal { get; set; }
            public string KetTarget { get; set; }
            public string KetPersen { get; set; }
            public string LabelTotalNilai { get; set; }
            public string LabelTargetNilai { get; set; }
            public string LabelPersenNilai { get; set; }
        }
    }
}
