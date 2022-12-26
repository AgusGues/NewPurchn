using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_JurHead : GRCBaseDomain
    {
        private string period = string.Empty;
        private string voucherCode = string.Empty;
        private string  jurNo  = string.Empty;
        private DateTime jurDate = DateTime.Now;
        private string jurDesc  = string.Empty;
        private int jurHeadNum = 0;
        private string createdBy = string.Empty;
        private string companyCode = string.Empty;
        private int jurHeadNumID = 0;
        public int flagTipe { get; set; }
        public int PTID { get; set; }
        public string VoucherCode0 { get; set; }
        public int JurHeadNumID
        {
            get { return jurHeadNumID; }
            set { jurHeadNumID = value; }
        }
        public string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        }
        public string Period
        {
            get { return period; }
            set { period = value; }
        }
        public string VoucherCode
        {
            get { return voucherCode; }
            set { voucherCode = value; }
        }
        public string JurNo
        {
            get { return jurNo; }
            set { jurNo = value; }
        }
        public DateTime JurDate
        {
            get { return jurDate; }
            set { jurDate = value; }
        }
        public string JurDesc
        {
            get { return jurDesc; }
            set { jurDesc = value; }
        }
        public int JurHeadNum
        {
            get { return jurHeadNum; }
            set { jurHeadNum = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
    }
}
