using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
 
    public class ISO_Dept : GRCBaseDomain
    {
        private int deptID = 0;
        private string deptname = string.Empty;
        private string urutan = string.Empty;
        private int userGroupID = 0;
        private int userid = 0;
        private int rowStatus = 0;

        public string DeptName
        {
            get { return deptname; }
            set { deptname = value; }
        }
        public string Urutan
        {
            get { return urutan; }
            set { urutan = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int UserGroupID
        {
            get { return userGroupID; }
            set { userGroupID = value; }
        }
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }

    }

}
