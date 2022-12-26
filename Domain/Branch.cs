using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Branch : GRCBaseDomain
    {
        private string branchCode = string.Empty;
        private string branchName = string.Empty;

        public string BranchCode
        {
            get
            {
                return branchCode;
            }
            set
            {
                branchCode = value;
            }
            
        }

        public string BranchName
        {
            get
            {
                return branchName;
            }
            set
            {
                branchName = value;
            }
        }
    }
}
