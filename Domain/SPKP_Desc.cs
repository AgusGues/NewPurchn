using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SPKP_Desc : GRCBaseDomain
    {
        private int iD = 0;
        private int sPKPID = 0;
        private string nomor = string.Empty;
        private string description = string.Empty;

        public int ID { get { return iD; } set { iD = value; } }
        public int SPKPID { get { return sPKPID; } set { sPKPID = value; } }
        public string Nomor { get { return nomor; } set { nomor = value; } }
        public string Description { get { return description; } set { description = value; } }
    }

    public class CopyOfSPKP_Desc : GRCBaseDomain
    {
        private int iD = 0;
        private int sPKPID = 0;
        private string nomor = string.Empty;
        private string description = string.Empty;

        public int ID { get { return iD; } set { iD = value; } }
        public int SPKPID { get { return sPKPID; } set { sPKPID = value; } }
        public string Nomor { get { return nomor; } set { nomor = value; } }
        public string Description { get { return description; } set { description = value; } }
    }
}
