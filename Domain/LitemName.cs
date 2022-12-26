using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LitemName : GRCBaseDomain
    {
        private int iD = 0;
        private int groupID = 0;
        private string liNCode = string.Empty;
        private string liNName = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string LiNCode
        {
            get { return liNCode; }
            set { liNCode = value; }
        }

        public string LiNName
        {
            get { return liNName; }
            set { liNName = value; }
        }
    }
}
