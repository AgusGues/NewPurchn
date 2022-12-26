using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_JurDetBA : GRCBaseDomain
    {
        private int jurDetNum = 0;
        private DateTime dueDate = DateTime.Now;
        private string remark = string.Empty;
        private string bgNo = string.Empty; //jurdetBA
        private string bankName = string.Empty; //jurdetBA

        public int JurDetNum
        {
            get { return jurDetNum; }
            set { jurDetNum = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
       
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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
    }
}
