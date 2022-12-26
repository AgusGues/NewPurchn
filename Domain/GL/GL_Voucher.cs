using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Voucher : GRCBaseDomain
    {
        private string voucherCode = string.Empty;
        private string voucherName = string.Empty;
        private string chartNo = string.Empty;
        private string chartname = string.Empty;
        private string dc = string.Empty;
        private string companycode = "0";
        private int ptid = 0;
        private int bankaccountid = 0;
        private string bankaccountno = string.Empty;
        private string keteranganbank = string.Empty;
        private int transdocid = 0;
        private string transactionname = string.Empty;
        private string typetrx = string.Empty;
        private string signedPerson = string.Empty;
        private int printMode = 0;

        public string VoucherCode
        {
            get { return voucherCode; }
            set { voucherCode = value; }
        }
        public string VoucherName
        {
            get { return voucherName; }
            set { voucherName = value; }
        }
        public string ChartNo
        {
            get { return chartNo; }
            set { chartNo = value; }
        }
        public string ChartName
        {
            get { return chartname; }
            set { chartname = value; }
        }
        public string DC
        {
            get { return dc; }
            set { dc = value; }
        }
        public string CompanyCode
        {
            get { return companycode; }
            set { companycode = value; }
        }
        public int PTID
        {
            get { return ptid; }
            set { ptid = value; }
        }
        public int BankAccountID
        {
            get { return bankaccountid; }
            set { bankaccountid = value; }
        }
        public string BankAccountNo
        {
            get { return bankaccountno; }
            set { bankaccountno = value; }
        }
        public string KeteranganBank
        {
            get { return keteranganbank; }
            set { keteranganbank = value; }
        }
        public int TransDocID
        {
            get { return transdocid; }
            set { transdocid = value; }
        }
        public string TransactionName
        {
            get { return transactionname; }
            set { transactionname = value; }
        }
        public string TypeTRX
        {
            get { return typetrx; }
            set { typetrx = value; }
        }
        public int PrintMode
        {
            get { return printMode; }
            set { printMode = value; }
        }
        public string SignedPerson
        {
            get { return signedPerson; }
            set { signedPerson = value; }
        }
    }
}
