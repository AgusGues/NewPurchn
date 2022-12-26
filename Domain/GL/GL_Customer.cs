using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Customer : GRCBaseDomain
    {
        private string custCode = string.Empty;
        private string custName = string.Empty;
        private string address = string.Empty;
        private string city = string.Empty;
        private string country = string.Empty;
        private string phone = string.Empty;
        private string fax = string.Empty;
        private string contactPerson = string.Empty;
        private string cCYCode = string.Empty;
        private string chartNo = string.Empty;
        private int vAT = 0;
        private string nPWP = string.Empty;
        //[Cust Code], [Cust Name], Address, City, Country, Phone, Fax, Contact, VAT, NPWP, [CCY Code], [Chart No]

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
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }
        public string CCYCode
        {
            get { return cCYCode; }
            set { cCYCode = value; }
        }
        public string ChartNo
        {
            get { return chartNo; }
            set { chartNo = value; }
        }
        public int VAT
        {
            get { return vAT; }
            set { vAT = value; }
        }
        public string NPWP
        {
            get { return nPWP; }
            set { nPWP = value; }
        }
    }
}
