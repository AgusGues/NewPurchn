using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterRegu : GRCBaseDomain
    {
        private int id = 0;
        private string regucode = string.Empty;
        private int planid = 0;
        private string planname = string.Empty;

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

        public string ReguCode
        {
            get
            {
                return regucode;
            }
            set
            {
                regucode = value;
            }
        }

        public int PlanID
        {
            get
            {
                return planid;
            }
            set
            {
                planid = value;
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
    }
}
