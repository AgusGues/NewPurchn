using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserPesNf
    {
        public class ParamHead
        {
            public int Id { get; set; }
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Nik { get; set; }
            public int Company { get; set; }
            public int TypeUnitKerja { get; set; }
            public int UnitKerja { get; set; }
            public int Department { get; set; }
            public string DepartmentName { get; set; }
            public int Jabatan { get; set; }
            public string Password { get; set; }
            public int RowStatus { get; set; }
            public string CreatedBy { get; set; }
            public string LastModifiedBy { get; set; }
        }

        public class ParamData
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Nik { get; set; }
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
            public int DeptId { get; set; }
            public string DeptName { get; set; }
            public int BagianId { get; set; }
            public string BagianName { get; set; }
            public int TypeUnitKerjaId { get; set; }
            public string TypeUnitKerjaName { get; set; }
            public int UnitKerjaId { get; set; }
            public string UnitKerjaName { get; set; }
        }

        public class ParamUserName
        {
            public int Id { get; set; }
            public string UserName { get; set; }
        }
       
        public class ParamUnitKerja
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ParamCompany
        {
            public int Id { get; set; }
            public string Nama { get; set; }
        }

        public class ParamDepartment
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
        }

        public class ParamJabatan
        {
            public int Id { get; set; }
            public string BagianName { get; set; }
        }
    }
}
