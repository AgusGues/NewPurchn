using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TransDoc : GRCBaseDomain
    {
                
        private string transactionCode = string.Empty;
        private string transactionName = string.Empty;
        private int coaID = 0;
        private string accountCode = string.Empty;        
        private int bankID = 0;
        private string bankName = string.Empty;
        private int bankAccountID = 0;
        private string bankAccountNo = string.Empty;
        private string accountRef = string.Empty;
        private string transactionGroup = string.Empty;
        private string noUrut = string.Empty;
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

        public string TransactionCode
        {
            get
            {
                return transactionCode;
            }
            set
            {
                transactionCode = value;
            }
        }

        public string TransactionName
        {
            get
            {
                return transactionName;
            }
            set
            {
                transactionName = value;
            }
        }

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

        public int BankID
        {
            get
            {
                return bankID;
            }
            set
            {
                bankID = value;
            }
        }

        public string BankName
        {
            get
            {
                return bankName;
            }
            set
            {
                bankName = value;
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

        public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                bankAccountNo = value;
            }
        }

        public string AccountRef
        {
            get
            {
                return accountRef;
            }
            set
            {
                accountRef = value;
            }
        }

        public string TransactionGroup
        {
            get
            {
                return transactionGroup;
            }
            set
            {
                transactionGroup = value;
            }
        }

        public string NoUrut
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
