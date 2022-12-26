using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_AdjustListplank : GRCBaseDomain
    {
        int iD = 0;
        string adjustNo = string.Empty;
        DateTime adjustDate = DateTime.Now;
        string adjustType = string.Empty;
        string noBA = string.Empty;
        string process = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string AdjustNo
        {
            get { return adjustNo; }
            set { adjustNo = value; }
        }
        public DateTime AdjustDate
        {
            get { return adjustDate; }
            set { adjustDate = value; }
        }

        public string AdjustType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }

        public string NoBA
        {
            get { return noBA; }
            set { noBA = value; }
        }

        public string Process
        {
            get { return process; }
            set { process = value; }
        }
    }
}
