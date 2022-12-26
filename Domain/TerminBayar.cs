using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TerminBayar : GRCBaseDomain
    {
        private int iD=0;
        private int pOID = 0;
        private decimal  dP = 0;
        private int terminKe = 0;
        private int jmlTermin = 0;
        private decimal totalBayar = 0;
        private decimal persentase = 0;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int POID
        {
            get { return pOID; }
            set { pOID = value; }
        }
        public decimal DP
        {
            get { return dP; }
            set { dP = value; }
        }
        public int TerminKe
        {
            get { return terminKe; }
            set { terminKe = value; }
        }
        public int JmlTermin
        {
            get { return jmlTermin; }
            set { jmlTermin = value; }
        }
        public decimal TotalBayar
        {
            get { return totalBayar; }
            set { totalBayar = value; }
        }
        public decimal Persentase
        {
            get { return persentase; }
            set { persentase = value; }
        }
    }
}
