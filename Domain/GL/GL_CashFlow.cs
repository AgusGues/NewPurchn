using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_CashFlow : GRCBaseDomain
    {
        private string cashFlowCode = string.Empty;
        private string cashFlowName = string.Empty;

        public string CashFlowCode
        {
            get { return cashFlowCode; }
            set { cashFlowCode = value; }
        }

        public string CashFlowName
        {
            get { return cashFlowName; }
            set { cashFlowName = value; }
        }

    }
}
