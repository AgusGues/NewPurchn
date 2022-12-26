using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SarMut_P_Departemen : GRCBaseDomain
    {
        private string sarMut_P_Department = string.Empty;
        private int deptID = 0;
        private int depoID = 0;
        private int urutan = 0;
        private int divHtmlNo = 0;


        public string SarMut_P_Department { get { return sarMut_P_Department; } set { sarMut_P_Department = value; } }
        public int DeptID { get { return deptID; } set { deptID = value; } }
        public int DepoID { get { return depoID; } set { depoID = value; } }
        public int Urutan { get { return urutan; } set { urutan = value; } }
        private int DivHtmlNo { get { return divHtmlNo; } set { divHtmlNo = value; } }
    }
}
