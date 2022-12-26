using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SM_LoadingTime : GRCBaseDomain
    {
        private string noLoading = string.Empty;
        private DateTime tanggal = DateTime.Now.Date;
        private DateTime timeIn = DateTime.Now.Date;
        private DateTime timeOut = DateTime.Now.Date;
        private int kendaraanID = 0;
        private int status = 0;
        

        public string NoLoading
        {
            get { return noLoading; }
            set { noLoading = value; }
        }

        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }

        public DateTime TimeIn
        {
            get { return timeIn; }
            set { timeIn = value; }
        }

        public DateTime TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public int KendaraanID
        {
            get { return kendaraanID; }
            set { kendaraanID = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        
    }
}
