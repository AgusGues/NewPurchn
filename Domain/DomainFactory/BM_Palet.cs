using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BM_Palet : GRCBaseDomain
    {
        private int iD = 0;
        private int status = 0;
        private string noPalet = string.Empty;

        public int ID
        {
            get { return iD ; }
            set { iD = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string NoPalet
        {
            get { return noPalet; }
            set { noPalet = value; }
        }


    }

    public class CopyOfBM_Palet : GRCBaseDomain
    {
        private int iD = 0;
        private int status = 0;
        private string noPalet = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string NoPalet
        {
            get { return noPalet; }
            set { noPalet = value; }
        }


    }
}
