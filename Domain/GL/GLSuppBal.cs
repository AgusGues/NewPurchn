using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class GL_SuppBal : Domain.GRCBaseDomain
    {
        private string period = string.Empty;
        private string supCode = string.Empty;
        private decimal begBal = 0;
        private decimal debitTrans = 0;
        private decimal creditTrans = 0;
        private decimal cCYBegBal = 0;
        private decimal cCYDebitTrans = 0;
        private decimal cCYCreditTrans = 0;

        public string CompanyCode { get; set; }
        public string Period
        {
            get { return period; }
            set { period = value; }
        }
        public string SupCode
        {
            get { return supCode; }
            set { supCode = value; }
        }
        public decimal BegBal
        {
            get { return begBal; }
            set { begBal = value; }
        }
        public decimal DebitTrans
        {
            get { return debitTrans; }
            set { debitTrans = value; }
        }
        public decimal CreditTrans
        {
            get { return creditTrans; }
            set { creditTrans = value; }
        }
        public decimal CCYBegBal
        {
            get { return cCYBegBal; }
            set { cCYBegBal = value; }
        }
        public decimal CCYDebitTrans
        {
            get { return cCYDebitTrans; }
            set { cCYDebitTrans = value; }
        }
        public decimal CCYCreditTrans
        {
            get { return cCYCreditTrans; }
            set { cCYCreditTrans = value; }
        }
    }
}
