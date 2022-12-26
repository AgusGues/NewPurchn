using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Dept : GRCBaseDomain
    {
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        private int iD = 0;


        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        
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
