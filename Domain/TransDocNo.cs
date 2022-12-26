using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TransDocNo : GRCBaseDomain
    {
        private int noUrut = 0;
        private int bankAccountID = 0;
        private int transDocID = 0;
        private int monthPeriod = 0;
        private int yearPeriod = 0;
        private int iD = 0;
        private string dc = string.Empty;

        public string DC
        {
            get
            {
                return dc;
            }
            set
            {
                dc = value;
            }
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

        public int TransDocID
        {
            get
            {
                return transDocID;
            }
            set
            {
                transDocID = value;
            }
        }

        public int BankAccountID
        {
            get
            {
                return bankAccountID;
            }
            set
            {
                bankAccountID = value;
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
