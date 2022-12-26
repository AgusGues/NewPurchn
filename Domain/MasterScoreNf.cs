using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MasterScoreNf
    {
        public class ParamDepartment
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
        }

        public class ParamSection
        {
            public int Id { get; set; }
            public string BagianName { get; set; }
        }

        public class ParamCategory
        {
            public int CategoryID { get; set; }
            public string Description { get; set; }
        }

        public class ParamData
        {
            public int Id { get; set; }
            public int PesType { get; set; }
            public string Category { get; set; }
            public int DeptId { get; set; }
            public string DeptName { get; set; }
            public int SectionId { get; set; }
            public string BagianName { get; set; }
            public int CategoryId { get; set; }
            public string Description { get; set; }
            public string TargetKe { get; set; }
            public int PointNilai { get; set; }
        }

        public class ParamHead
        {
            public int Id { get; set; }
            public int PesType { get; set; }
            public string Category { get; set; }
            public int DeptId { get; set; }
            public string DeptName { get; set; }
            public int SectionId { get; set; }
            public string BagianName { get; set; }
            public int CategoryId { get; set; }
            public string Description { get; set; }
            public string TargetKe { get; set; }
            public int PointNilai { get; set; }
        }

    }
}
