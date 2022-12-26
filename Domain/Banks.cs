using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Banks : GRCBaseDomain
    {
        private string bankCode = string.Empty;
        private string bankName = string.Empty;
        private int depoID = 0;

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public string BankCode
        {
            get
            {
                return bankCode;
            }
            set
            {
                bankCode = value;
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
    }
}
