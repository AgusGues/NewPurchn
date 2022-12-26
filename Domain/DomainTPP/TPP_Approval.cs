using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Approval : GRCBaseDomain
    {
        private int tPP_ID = 0;
        private DateTime approval_Date = DateTime.Now;
        private int user_ID = 0;

        public int TPP_ID { get { return tPP_ID; } set { tPP_ID = value; } }
        public DateTime Approval_Date { get { return approval_Date; } set { approval_Date = value; } }
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
    }
}
