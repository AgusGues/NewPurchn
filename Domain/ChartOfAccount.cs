using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ChartOfAccount : GRCBaseDomain
    {
        private string accountCode = string.Empty;
        private string accountName = string.Empty;
        private int accountJenis = 0;
        private int accountLevel = 0;
        private string dc = string.Empty;
        private string accountType = string.Empty;
        private string accountHead = string.Empty;

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

        public string AccountName
        {
            get
            {
                return accountName;
            }
            set
            {
                accountName = value;
            }
        }

        public int AccountJenis
        {
            get
            {
                return accountJenis;
            }
            set
            {
                accountJenis = value;
            }
        }

        public int AccountLevel
        {
            get
            {
                return accountLevel;
            }
            set
            {
                accountLevel = value;
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

        public string AccountType
        {
            get
            {
                return accountType;
            }
            set
            {
                accountType = value;
            }
        }

        public string AccountHead
        {
            get
            {
                return accountHead;
            }
            set
            {
                accountHead = value;
            }
        }
    }
}
