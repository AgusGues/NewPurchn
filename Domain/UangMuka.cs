using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class UangMuka : GRCBaseDomain
    {
        private string uMNo = string.Empty;
        private int customerType = 0;
        private int customerID = 0;
        private int status = 0;
        private decimal totalOP = 0;
        private decimal totalUM = 0;
        private int oPID = 0;
        private string opNo = string.Empty;
        private DateTime transactionDate = DateTime.MinValue;
        private DateTime tglPenerimaan = DateTime.MinValue;

        private DateTime createdTime = DateTime.MinValue;
        private string suratJalanNo = string.Empty;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;

        private decimal qty = 0;
        private decimal hargaRetail = 0;
        private string typeInvoice = string.Empty;
        private int bankInID = 0;
        private int invoiceID = 0;
        private decimal total = 0;
        private string invoiceNo = string.Empty;
        private decimal sisaUM = 0;

        public decimal SisaUM
        {
            get { return sisaUM; }
            set { sisaUM = value; }
        }
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        public DateTime TglPenerimaan
        {
            get { return tglPenerimaan; }
            set { tglPenerimaan = value; }
        }
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        public int InvoiceID
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }
        public int BankInID
        {
            get { return bankInID; }
            set { bankInID = value; }
        }
        public string TypeInvoice
        {
            get
            {
                return typeInvoice;
            }
            set
            {
                typeInvoice = value;
            }
        }
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public decimal HargaRetail
        {
            get { return hargaRetail; }
            set { hargaRetail = value; }
        }
        public string SuratJalanNo
        {
            get
            {
                return suratJalanNo;
            }
            set
            {
                suratJalanNo = value;
            }
        }
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }
        public DateTime CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
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
        public string OPNo
        {
            get
            {
                return opNo;
            }
            set
            {
                opNo = value;
            }
        }
        public int OPID
        {
            get { return oPID; }
            set { oPID = value; }
        }

        public decimal TotalUM
        {
            get { return totalUM; }
            set { totalUM = value; }
        }

        public decimal TotalOP
        {
            get { return totalOP; }
            set { totalOP = value; }
        }

        public int CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public string UMNo
        {
            get
            {
                return uMNo;
            }
            set
            {
                uMNo = value;
            }
        }

    }
}
