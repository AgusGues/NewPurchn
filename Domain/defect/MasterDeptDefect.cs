using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterDeptDefect : GRCBaseDomain
    {
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        //private int rowStatus = 

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
    }
}