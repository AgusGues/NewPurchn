using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_CcyRate : GRCBaseDomain
    {
        private string ccyCode = string.Empty;
        private int ccyRate = 0;
        private DateTime efectiveDate = DateTime.MinValue;
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
        public int CcyRate
        {
            get { return ccyRate; }
            set { ccyRate = value; }
        }
        public DateTime EfectiveDate
        {
            get { return efectiveDate; }
            set { efectiveDate = value; }
        }
    }
}
