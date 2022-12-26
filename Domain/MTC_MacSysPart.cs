using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MTC_MacSysPart : GRCBaseDomain
    {
        private int iD = 0;
        private int macSysID = 0;
        private int zonaID = 0;
        private string macSysPartName = string.Empty;
        private string macSysPartCode = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public int MacSysID
        {
            get { return macSysID; }
            set { macSysID = value; }
        }

        public int ZonaID
        {
            get { return zonaID; }
            set { zonaID = value; }
        }

        public string MacSysPartName
        {
            get { return macSysPartName; }
            set { macSysPartName = value; }
        }
        public string MacSysPartCode
        {
            get { return macSysPartCode; }
            set { macSysPartCode = value; }
        }
    }
}
