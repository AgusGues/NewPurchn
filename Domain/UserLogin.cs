using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class UserLogin:GRCBaseDomain
    {
        private string ipaddress = string.Empty;
        private string userid = string.Empty;
        private int loginstat = 0;
        private DateTime logintime = DateTime.MinValue;

        public string IPAddress
        {
            set { ipaddress = value; }
            get { return ipaddress; }
        }
        public string UserID
        {
            set { userid = value; }
            get { return userid; }
        }

        public int LoginStat
        {
            set { loginstat = value; }
            get { return loginstat; }
        }
        public DateTime LoginTime
        {
            set { logintime = value; }
            get { return logintime; }
        }

    }
}
