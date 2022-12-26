using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_DeptM1 : GRCBaseDomain
    {
        private int iD = 0;
        private string DeptID = string.Empty;
        private string Namadept = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string deptID
        {
            get { return DeptID; }
            set { DeptID = value; }
        }

        public string namaDept
        {
            get { return Namadept; }
            set { Namadept = value; }
        }


    }
}
