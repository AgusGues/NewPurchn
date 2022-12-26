using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BranchDepo : GRCBaseDomain
    {
        private int branchID = 0;
        private string branchName = string.Empty;
        private int depoID = 0;
        private string depoName = string.Empty;

        public int BranchID
        {
            get
            {
                return branchID;
            }
            set
            {
                branchID = value;
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

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public string DepoName
        {
            get
            {
                return depoName;
            }
            set
            {
                depoName = value;
            }
        }
    }
}
