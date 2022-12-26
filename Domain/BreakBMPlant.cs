using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BreakBMPlant : GRCBaseDomain
    {
        private int id = 0;
        private string plancode = string.Empty;
        private string planname = string.Empty;
        private int deptid = 0;
        private string deptname = string.Empty;

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

        public string PlanCode
        {
            get
            {
                return plancode;
            }
            set
            {
                plancode = value;
            }
        }

        public string PlanName
        {
            get
            {
                return planname;
            }
            set
            {
                planname = value;
            }
        }

        public int DeptID
        {
            get
            {
                return deptid;
            }
            set
            {
                deptid = value;
            }

        }

        public string DeptName
        {
            get
            {
                return deptname;
            }
            set
            {
                deptname = value;
            }
        }

        //public string MasterPlanID
        //{ get; set; }
    }
}
