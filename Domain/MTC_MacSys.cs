using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MTC_MacSys : GRCBaseDomain
    {
        private int iD = 0;
        //private int zonaID = 0;
        private int plantID = 0;
        private string macSysName = string.Empty;
        private string macSysCode = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public int PlantID
        {
            get { return plantID; }
            set { plantID = value; }
        }

        public string MacSysName
        {
            get { return macSysName; }
            set { macSysName  = value; }
        }
        public string MacSysCode
        {
            get { return macSysCode; }
            set { macSysCode  = value; }
        }
    }
}
