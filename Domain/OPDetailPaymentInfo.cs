using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{

    public class OPDetailPaymentInfo : GRCBaseDomain
    {
        private int opid = 0;
        private string categoryName = string.Empty;
        private decimal totOP = 0;
        private string opno = string.Empty;
        private string categoryPayment = string.Empty;
        private string alamatPenagihan = string.Empty;
        private int categoryPaymentID = 0;
        private decimal uangMukaPersen = 0;
        private decimal uangMukaValue = 0;
        private int infoHari = 0;
        private int categoryProjectID = 0;
        private int categoryAplicationID = 0;
        private int categoryCompanyID = 0;
        private decimal creditLimit = 0;
        private DateTime tglDiterima = DateTime.MinValue;
        private int umurTandaTerima = 0;
        private string orderRef = string.Empty;
        private int lamaPembayaran = 0;

        public int LamaPembayaran
        {
            get { return lamaPembayaran; }
            set { lamaPembayaran = value; }
        }
        public string OrderRef
        {
            get { return orderRef; }
            set { orderRef = value; }
        }
        public DateTime TglDiterima
        {
            get { return tglDiterima; }
            set { tglDiterima = value; }
        }
        public int UmurTandaTerima
        {
            get { return umurTandaTerima; }
            set { umurTandaTerima = value; }
        }
        public decimal CreditLimit
        {
            get { return creditLimit; }
            set { creditLimit = value; }
        }
        public int CategoryPaymentID
        {
            get { return categoryPaymentID; }
            set { categoryPaymentID = value; }
        }
        public int InfoHari
        {
            get { return infoHari; }
            set { infoHari = value; }
        }
        public int CategoryProjectID
        {
            get { return categoryProjectID; }
            set { categoryProjectID = value; }
        }
        public int CategoryAplicationID
        {
            get { return categoryAplicationID; }
            set { categoryAplicationID = value; }
        }
        public int CategoryCompanyID
        {
            get { return categoryCompanyID; }
            set { categoryCompanyID = value; }
        }

        public decimal UangMukaPersen
        {
            get { return uangMukaPersen; }
            set { uangMukaPersen = value; }
        }
        public decimal UangMukaValue
        {
            get { return uangMukaValue; }
            set { uangMukaValue = value; }
        }
        public string AlamatPenagihan
        {
            get { return alamatPenagihan; }
            set { alamatPenagihan = value; }
        }
        public string CategoryPayment
        {
            get { return categoryPayment; }
            set { categoryPayment = value; }
        }
        public decimal TotOP
        {
            get { return totOP; }
            set { totOP = value; }
        }
        public string OPno
        {
            get { return opno; }
            set { opno = value; }
        }
        public int OPid
        {
            get { return opid; }
            set { opid = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }




    }

}
