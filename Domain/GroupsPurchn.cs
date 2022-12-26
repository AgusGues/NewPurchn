using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class GroupsPurchn : GRCBaseDomain
    {
        private int id = 0;
        private string codeID = string.Empty;
        private string groupCode = string.Empty;
        private string groupDescription = string.Empty;
        private int itemTypeID = 0;
        private int deptID = 0;
        private string typeDescription = string.Empty;
        private string deptName = string.Empty;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string CodeID
        {
            get
            {
                return codeID;
            }
            set
            {
                codeID = value;
            }
        }
        public string GroupCode
        {
            get
            {
                return groupCode;
            }
            set
            {
                groupCode = value;
            }
        }
        public string GroupDescription
        {
            get
            {
                return groupDescription;
            }
            set
            {
                groupDescription = value;
            }
        }

        public int ItemTypeID
        {
            get
            {
                return itemTypeID;
            }
            set
            {
                itemTypeID = value;
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

        public string TypeDescription
        {
            get
            {
                return typeDescription;
            }
            set
            {
                typeDescription = value;
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
    }
}
