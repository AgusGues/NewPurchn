using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_Supplier : GRCBaseDomain
    {
        private string supCode = string.Empty ;
        private string suppName = string.Empty;
        private string address = string.Empty;
        private string city = string.Empty;
        private string country = string.Empty;
        private string phone = string.Empty;
        private string fax = string.Empty;
        private string contactPerson = string.Empty;
        private string cCYCode = string.Empty;
        private string chartNo = string.Empty;

        public string CompanyCode { get; set; }
        public string SupCode
        {
            get { return supCode; }
            set { supCode = value; }
        }
        public string SuppName
        {
            get { return suppName; }
            set { suppName = value; }
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
    }
}
