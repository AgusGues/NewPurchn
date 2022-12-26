using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Dept : GRCBaseDomain
    {
        private int iD = 0;
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        private string namaHead = string.Empty;
        private string alias = string.Empty;

        private string sectionName = string.Empty;
        private string bagianName = string.Empty;
        private int userID = 0;
        private int usergroupID = 0;
        private int deptID = 0;
        private int bagianID = 0;

        public string SectionName
        {
            get
            {
                return sectionName;
            }
            set
            {
                sectionName = value;
            }
        }

        public string BagianName
        {
            get
            {
                return bagianName;
            }
            set
            {
                bagianName = value;
            }
        }
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public int DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                deptID = value;
            }
        }
        public int BagianID
        {
            get
            {
                return bagianID;
            }
            set
            {
                bagianID = value;
            }
        }

        public int UserGroupID
        {
            get
            {
                return usergroupID;
            }
            set
            {
                usergroupID = value;
            }
        }


        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        public string NamaHead
        {
            get
            {
                return namaHead;
            }
            set
            {
                namaHead = value;
            }
        }
        
        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }
            set
            {
                deptName = value;
            }
        }

        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                alias = value;
            }
        }

        public string AlisName { get; set; } 
    }
}
