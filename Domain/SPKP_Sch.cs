using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SPKP_Sch : GRCBaseDomain
    {
        private int iD = 0;
        private int sPKPID=0;
        private string plant = string.Empty; 
        private string formula=string.Empty;
        private int tebal = 0; 
        private string luas=string.Empty;
        private DateTime tanggal = DateTime.Now;
        private int shift = 0;
        private int lembar = 0;

        public int ID { get { return iD; } set { iD = value; } }
        public int SPKPID { get { return sPKPID; } set { sPKPID = value; } }
        public string Plant { get { return plant; } set { plant = value; } }
        public string Formula { get { return formula; } set { formula = value; } }
        public int Tebal { get { return tebal; } set { tebal = value; } }
        public string Luas { get { return luas; } set { luas = value; } }
        public DateTime Tanggal { get { return tanggal; } set { tanggal = value; } }
        public int Shift { get { return shift; } set { shift = value; } }
        public int Lembar { get { return lembar; } set { lembar = value; } }
    }

    public class CopyOfSPKP_Sch : GRCBaseDomain
    {
        private int iD = 0;
        private int sPKPID = 0;
        private string plant = string.Empty;
        private string formula = string.Empty;
        private int tebal = 0;
        private string luas = string.Empty;
        private DateTime tanggal = DateTime.Now;
        private int shift = 0;
        private int lembar = 0;

        public int ID { get { return iD; } set { iD = value; } }
        public int SPKPID { get { return sPKPID; } set { sPKPID = value; } }
        public string Plant { get { return plant; } set { plant = value; } }
        public string Formula { get { return formula; } set { formula = value; } }
        public int Tebal { get { return tebal; } set { tebal = value; } }
        public string Luas { get { return luas; } set { luas = value; } }
        public DateTime Tanggal { get { return tanggal; } set { tanggal = value; } }
        public int Shift { get { return shift; } set { shift = value; } }
        public int Lembar { get { return lembar; } set { lembar = value; } }
    }
}
