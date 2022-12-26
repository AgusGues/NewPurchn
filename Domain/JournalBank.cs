using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class JournalBank : GRCBaseDomain
    {
        private int coaID = 0;
        private int paymentHeaderID = 0;
        private int transactionID = 0;
        private string nourut = string.Empty;
        private DateTime transactionDate = DateTime.Now.Date;
        private string accountCode = string.Empty;
        private string description = string.Empty;
        private string dc = string.Empty;
        private decimal debet = 0;
        private decimal credit = 0;
        private int periodMonth = 0;
        private int periodYear = 0;
        private int posting = 0;
        private int status = 0;
        private int printStatus = 0;
        private int countPrint = 0;
        private int journalType = 0;    


        public int CoaID
        {
            get
            {
                return coaID;
            }
            set
            {
                coaID = value;
            }
        }

        public int PaymentHeaderID
        {
            get
            {
                return paymentHeaderID;
            }
            set
            {
                paymentHeaderID = value;
            }
        }

        public int TransactionID
        {
            get
            {
                return transactionID;
            }
            set
            {
                transactionID = value;
            }
        }

        public string Nourut
        {
            get
            {
                return nourut;
            }
            set
            {
                nourut = value;
            }
        }

        public DateTime TransactionDate
        {
            get
            {
                return transactionDate;
            }
            set
            {
                transactionDate = value;
            }
        }

        public string AccountCode
        {
            get
            {
                return accountCode;
            }
            set
            {
                accountCode = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

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

        public decimal Debet
        {
            get
            {
                return debet;
            }
            set
            {
                debet = value;
            }
        }

        public decimal Credit
        {
            get
            {
                return credit;
            }
            set
            {
                credit = value;
            }
        }

        public int PeriodMonth
        {
            get
            {
                return periodMonth;
            }
            set
            {
                periodMonth = value;
            }
        }

        public int PeriodYear
        {
            get
            {
                return periodYear;
            }
            set
            {
                periodYear = value;
            }
        }

        public int Posting
        {
            get
            {
                return posting;
            }
            set
            {
                posting = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public int PrintStatus
        {
            get
            {
                return printStatus;
            }
            set
            {
                printStatus = value;
            }
        }

        public int CountPrint
        {
            get
            {
                return countPrint;
            }
            set
            {
                countPrint = value;
            }
        
        }

        public int JournalType
        {
            get
            {
                return journalType;
            }
            set
            {
                journalType = value;
            }
        }
    }
}
