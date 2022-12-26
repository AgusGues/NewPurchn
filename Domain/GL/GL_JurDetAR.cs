using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_JurDetAR : GRCBaseDomain
    {
        private int jurDetNum = 0;
        private string invNo = string.Empty;
        private DateTime invDate = DateTime.Now;
        private DateTime dueDate = DateTime.Now;
        private string ourRef = string.Empty;
        private string custCode = string.Empty; //jurdetAR
        private string custName = string.Empty; //jurdetAR
        private string remark = string.Empty;

        public int JurDetNum
        {
            get { return jurDetNum; }
            set { jurDetNum = value; }
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
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
