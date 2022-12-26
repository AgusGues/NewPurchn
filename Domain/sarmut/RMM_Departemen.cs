using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RMM_Departemen : GRCBaseDomain
    {
        private string sarMutDepartment = string.Empty;
        private int deptID = 0;
        private int depoID = 0;
        private int urutan = 0;
        private int divHtmlNo = 0;


        public string SarMutDepartment { get { return sarMutDepartment; } set { sarMutDepartment = value; } }
        public int DeptID { get { return deptID; } set { deptID = value; } }
        public int DepoID { get { return depoID; } set { depoID = value; } }
        public int Urutan { get { return urutan; } set { urutan = value; } }
        private int DivHtmlNo { get { return divHtmlNo; } set { divHtmlNo = value; } }
    }
}
