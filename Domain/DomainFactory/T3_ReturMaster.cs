using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_ReturMaster : GRCBaseDomain
    {
        int iD = 0;
        string returNo = string.Empty;
        DateTime returDate = DateTime.Now;
        string keterangan = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string ReturNo
        {
            get { return returNo; }
            set { returNo = value; }
        }
        public DateTime ReturDate
        {
            get { return returDate; }
            set { returDate = value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }

    }
}
