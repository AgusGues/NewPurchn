using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PCash : GRCBaseDomain
    {
        private string pettyCashCode = string.Empty;
        private string pettyCashName = string.Empty;
        private decimal saldo = 0;
        private int depoID = 0;
        private int rowStatus = 0;

        public string PettyCashCode
        {
            get
            {
                return pettyCashCode;
            }
            set
            {
                pettyCashCode = value;
            }
        }

        public string PettyCashName
        {
            get
            {
                return pettyCashName;
            }
            set
            {
                pettyCashName = value;
            }
        }

        public decimal Saldo
        {
            get
            {
                return saldo;
            }
            set
            {
                saldo = value;
            }
        }

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

        public int RowStatus
        {
            get
            {
                return rowStatus;
            }
            set
            {
                rowStatus = value;
            }
        }
    }
}
