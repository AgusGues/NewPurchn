using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MasterSectionNf
    {
        public class ParamHead
        {
            public int Id { get; set; }
            public int DeptId { get; set; }
            public string BagianName { get; set; }
            public int UserGroupId { get; set; }
            public int RowStatus { get; set; }
            public string LastModifiedBy { get; set; }
            public decimal BobotKpi { get; set; }
            public decimal BobotSop { get; set; }
            public decimal BobotTask { get; set; }
            public decimal BobotDisiplin { get; set; }
            public int BerlakuBulan { get; set; }
            public string BerlakuBulanName { get; set; }
            public int BerlakuTahun { get; set; }
        }
        public class ParamDepartment
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
        }
        public class ParamData
        {
            public int Id { get; set; }
            public string BagianName { get; set; }
            public int DeptId { get; set; }
            public string DeptName { get; set; }
            public int UserGroupId { get; set; }
            public string UserGroupName { get; set; }
            public int BobotKpi { get; set; }
            public int BobotSop { get; set; }
            public int BobotTask { get; set; }
            public int BobotDisiplin { get; set; }
            public int BerlakuBulan { get; set; }
            public string BerlakuBulanName { get; set; }
            public int BerlakuTahun { get; set; }
        }
    }
}
