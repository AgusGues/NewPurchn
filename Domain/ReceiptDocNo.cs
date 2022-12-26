using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    //class ReceiptDocNo
    //{
    //}

    public class ReceiptDocNo : GRCBaseDomain
    {
        private int noUrut = 0;
        private string receiptCode = string.Empty;
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

        public string ReceiptCode
        {
            get
            {
                return receiptCode;
            }
            set
            {
                receiptCode = value;
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
