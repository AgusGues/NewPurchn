using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SPPNumber
    {
        private int iD = 0;
        private int groupsPurchnID = 0;
        private int sPPCounter = 0;
        private int pOCounter = 0;
        private string lastModifiedBy = string.Empty;
        private DateTime lastModifiedTime = DateTime.Now.Date;
        private int flag = 0;
        private string kodeCompany = string.Empty;
        private string kodeSPP = string.Empty;

        public string KodeSPP
        {
            get { return kodeSPP; }
            set { kodeSPP = value; }
        }
        public string KodeCompany
        {
            get { return kodeCompany; }
            set { kodeCompany = value; } 
        }
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        public int GroupsPurchnID
        {
            get
            {
                return groupsPurchnID;
            }
            set
            {
                groupsPurchnID = value;
            }
        }

        public int SPPCounter
        {
            get
            {
                return sPPCounter;
            }
            set
            {
                sPPCounter = value;
            }
        }
        public int POCounter
        {
            get
            {
                return pOCounter;
            }
            set
            {
                pOCounter = value;
            }
        }
        
        public string LastModifiedBy
        {
            get
            {
                return lastModifiedBy;
            }
            set
            {
                lastModifiedBy = value;
            }
        }

        public DateTime LastModifiedTime
        {
            get
            {
                return lastModifiedTime;
            }
            set
            {
                lastModifiedTime = value;
            }
        }

        public int Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
    }
}
