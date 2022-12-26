using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Ccy : GRCBaseDomain
    {
        private string ccyCode = string.Empty;
        private string ccyName = string.Empty;
        private int iD = 0;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string CcyCode
        {
            get { return ccyCode; }
            set { ccyCode = value; }
        }
        public string CcyName
        {
            get { return ccyName; }
            set { ccyName = value; }
        }
        
    }
}
