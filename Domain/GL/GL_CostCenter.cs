using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_CostCenter : GRCBaseDomain
    {
        private string costCenterCode = string.Empty;
        private string costCenterName = string.Empty;
        private int iD = 0;


        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string CostCenterCode
        {
            get { return costCenterCode; }
            set { costCenterCode = value; }
        }

        public string CostCenterName
        {
            get { return costCenterName; }
            set { costCenterName = value; }
        }
    }
}
