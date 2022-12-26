using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SaldoSupplier : GRCBaseDomain
    {
        private int monthPeriod = 0;
        private int yearPeriod = 0;
        private int posting = 0;
        private decimal jan = 0;
        private decimal feb = 0;
        private decimal mar = 0;
        private decimal apr = 0;
        private decimal mei = 0;
        private decimal jun = 0;
        private decimal jul = 0;
        private decimal agu = 0;
        private decimal sep = 0;
        private decimal okt = 0;
        private decimal nov = 0;
        private decimal des = 0;
        private decimal saldo = 0;
        private int supplierID = 0;
        private decimal price = 0;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        }
        public int MonthPeriod
        {
            get { return monthPeriod; }
            set { monthPeriod = value; }
        }
        public int YearPeriod
        {
            get { return yearPeriod; }
            set { yearPeriod = value; }
        }
        public int Posting
        {
            get { return posting; }
            set { posting = value; }
        }

        public decimal Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        public decimal Jan
        {
            get { return jan; }
            set { jan = value; }
        }
        public decimal Feb
        {
            get { return feb; }
            set { feb = value; }
        }
        public decimal Mar
        {
            get { return mar; }
            set { mar = value; }
        }
        public decimal Apr
        {
            get { return apr; }
            set { apr = value; }
        }
        public decimal Mei
        {
            get { return mei; }
            set { mei = value; }
        }
        public decimal Jun
        {
            get { return jun; }
            set { jun = value; }
        }
        public decimal Jul
        {
            get { return jul; }
            set { jul = value; }
        }
        public decimal Agu
        {
            get { return agu; }
            set { agu = value; }
        }
        public decimal Sep
        {
            get { return sep; }
            set { sep = value; }
        }
        public decimal Okt
        {
            get { return okt; }
            set { okt = value; }
        }
        public decimal Nov
        {
            get { return nov; }
            set { nov = value; }
        }
        public decimal Des
        {
            get { return des; }
            set { des = value; }
        }


    }
}
