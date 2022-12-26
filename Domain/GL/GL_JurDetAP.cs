using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GL_JurDetAP : GRCBaseDomain
    {
        private int jurDetNum = 0;
        private string invNo = string.Empty;
        private DateTime invDate = DateTime.Now;
        private DateTime dueDate = DateTime.Now;
        private string ourRef = string.Empty;
        private string supCode = string.Empty; //jurdetAP
        private string supName = string.Empty; //jurdetAP
        private string remark = string.Empty;
        private Decimal pPH= 0;
        private Decimal freight = 0;
        private Decimal pPnBM = 0;

        
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
       
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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
