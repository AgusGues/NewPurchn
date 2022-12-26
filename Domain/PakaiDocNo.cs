using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PakaiDocNo : GRCBaseDomain
    {

        //Nf ________________________________________________________
        public class Param
        {
            public int ID { get; set; }
            public string PakaiCode { get; set; }
            public int MonthPeriod { get; set; }
            public int YearPeriod { get; set; }
            public string NoUrut { get; set; }
        }
        //end Nf_______________________________________________________
        private int noUrut = 0;
        private string pakaiCode = string.Empty;
        private int monthPeriod = 0;
        private int yearPeriod = 0;
        private int iD = 0;

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

        public int YearPeriod
        {
            get
            {
                return yearPeriod;
            }
            set
            {
                yearPeriod = value;
            }
        }

        public int MonthPeriod
        {
            get
            {
                return monthPeriod;
            }
            set
            {
                monthPeriod = value;
            }
        }

        public string PakaiCode
        {
            get
            {
                return pakaiCode;
            }
            set
            {
                pakaiCode = value;
            }
        }

        public int NoUrut
        {
            get
            {
                return noUrut;
            }
            set
            {
                noUrut = value;
            }
        }
    }
}
