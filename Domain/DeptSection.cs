using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DeptSection : GRCBaseDomain
    {
        private int id = 0;
        private int deptID = 0;
        private string sectCode = string.Empty;
        private string sectName = string.Empty;
        private string deptName = string.Empty;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public string SectCode
        {
            get { return sectCode; }
            set { sectCode = value; }
        }
        public string SectName
        {
            get { return sectName; }
            set { sectName = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
    }
}
