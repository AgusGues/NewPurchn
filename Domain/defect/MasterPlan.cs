using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterPlan : GRCBaseDomain
    {
        private string planCode = string.Empty;
        private string planName = string.Empty;
        //private int rowStatus = 

        public string PlanCode
        {
            get
            {
                return planCode;

            }
            set
            {
                planCode = value;
            }
        }
        public string PlanName
        {
            get
            {
                return planName;
            }
            set
            {
                planName = value;

            }

        }


    }

}
