using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Parameter : GRCBaseDomain
    {
        private string paramCode = string.Empty;
        private string paramName = string.Empty;
        private string charValue = string.Empty;
        private int intValue = 0;
        private int iD = 0;
        private string companyCode = string.Empty;
        private string info = string.Empty;

        public string Info
        {
            get { return info; }
            set { info = value; }
        }
        public string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string ParamCode
        {
            get { return paramCode; }
            set { paramCode = value; }
        }
        public string ParamName
        {
            get { return paramName; }
            set { paramName = value; }
        }
        public string CharValue
        {
            get { return charValue; }
            set { charValue = value; }
        }
        public int IntValue
        {
            get { return intValue; }
            set { intValue = value; }
        }
    }
}
