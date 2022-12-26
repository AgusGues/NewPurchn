using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_JurDet : GRCBaseDomain
    {
        private string jurType = string.Empty;
        private string chartNo = string.Empty;
        private string chartName = string.Empty;
        private string description = string.Empty;
        private string deptCode = string.Empty;
        private string costCode = string.Empty;
        private string cCYCode = string.Empty;
        private Decimal cCYAmount = 0;
        private Decimal cCYRate = 0;
        private Decimal iDRAmount = 0;
        private string cFCode = string.Empty;
        private int jurHeadNum = 0;
        private int jurDetNum = 0;
        private int seqNum = 0;

        private string invNo = string.Empty;
        private DateTime invDate = DateTime.Now;
        private DateTime dueDate = DateTime.Now;
        private string ourRef = string.Empty;
        private string supCode = string.Empty; //jurdetAP
        private Decimal pPH = 0; //jurdetAP
        private Decimal freight = 0; //jurdetAP
        private Decimal pPnBM = 0; //jurdetAP
        private string supName = string.Empty; //jurdetAP
        private string custCode = string.Empty; //jurdetAR
        private string custName = string.Empty; //jurdetAR
        private string remarkDet = string.Empty;
        private string bgNo = string.Empty; //jurdetBA
        private string bankName = string.Empty; //jurdetBA
        //for import
        private string period = string.Empty;
        private string group = string.Empty;
        private decimal begBal = 0;
        private decimal debitTrans = 0;
        private decimal creditTrans = 0;
        private decimal cCYBegBal = 0;
        private decimal cCYDebitTrans = 0;
        private decimal cCYCreditTrans = 0;
        private int postable = 0;
        private string createdby = string.Empty;

        public int TrxMappingCOAid { get; set; }
        public string TypeTRX { get; set; }
        public string TableName { get; set; }
        public int IDTableName { get; set; }
        public string CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }
        public string Period
        {
            get { return period; }
            set { period = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
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
        public int Postable
        {
            get { return postable; }
            set { postable = value; }
        }
        //

        public string JurType
        {
            get { return jurType; }
            set { jurType = value; }
        }
        public string ChartNo
        {
            get { return chartNo; }
            set { chartNo = value; }
        }
        public string ChartName
        {
            get { return chartName; }
            set { chartName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public string CostCode
        {
            get { return costCode; }
            set { costCode = value; }
        }
        public string CCYCode
        {
            get { return cCYCode; }
            set { cCYCode = value; }
        }
        public Decimal CCYAmount
        {
            get { return cCYAmount; }
            set { cCYAmount = value; }
        }
        public Decimal CCYRate
        {
            get { return cCYRate; }
            set { cCYRate = value; }
        }
        public Decimal IDRAmount
        {
            get { return iDRAmount; }
            set { iDRAmount = value; }
        }
        public string CFCode
        {
            get { return cFCode; }
            set { cFCode = value; }
        }
        public int JurHeadNum
        {
            get { return jurHeadNum; }
            set { jurHeadNum = value; }
        }
        public int JurDetNum
        {
            get { return jurDetNum; }
            set { jurDetNum = value; }
        }
        public int SeqNum
        {
            get { return seqNum; }
            set { seqNum = value; }
        }
        public string InvNo
        {
            get { return invNo; }
            set { invNo = value; }
        }
        public DateTime InvDate
        {
            get { return invDate; }
            set { invDate = value; }
        }
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public string OurRef
        {
            get { return ourRef; }
            set { ourRef = value; }
        }
        public string SupCode
        {
            get { return supCode; }
            set { supCode = value; }
        }
        public string SupName
        {
            get { return supName; }
            set { supName = value; }
        }
        public string CustCode
        {
            get { return custCode; }
            set { custCode = value; }
        }
        public string CustName
        {
            get { return custName; }
            set { custName = value; }
        }
        public string RemarkDet
        {
            get { return remarkDet; }
            set { remarkDet = value; }
        }
        public string BgNo
        {
            get { return bgNo; }
            set { bgNo = value; }
        }
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        public Decimal PPH
        {
            get { return pPH; }
            set { pPH = value; }
        }
        public Decimal Freight
        {
            get { return freight; }
            set { freight = value; }
        }
        public Decimal PPnBM
        {
            get { return pPnBM; }
            set { pPnBM = value; }
        }
    }
}
