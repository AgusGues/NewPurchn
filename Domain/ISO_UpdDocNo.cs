using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_UpdDocNo
    {
        private int id = 0;
        private int docno = 0;
        private int docType = 0;
        private int deptid = 0;
        private int tahun = 0;
        private int docCat = 0;
        private int revNo = 0;
        private string deptCode = string.Empty;
        private string plant = string.Empty;
        private string doCtype = string.Empty;
        private string docCategory = string.Empty;
        
    
        public string Plant
        {
            get{return plant;}
            set{plant = value;}
        }
        public string DocCategory
        {
            get { return docCategory; }
            set { docCategory = value; }
        }
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public string DocTypE
        {
            get { return doCtype; }
            set { doCtype = value; }
        }

        public int ID
        {
            get{return id;}
            set{id = value;}
        }
        public int DocType
        {
            get { return docType; }
            set { docType = value; }
        }
        public int DocCat
        {
            get { return docCat; }
            set { docCat = value; }
        }
        public int RevNo
        {
            get { return revNo; }
            set { revNo = value; }
        }
        public int DeptID
        {
            get { return deptid; }
            set { deptid = value; }
        }
        public int DocNo
        {
            get { return docno; }
            set { docno = value; }
        }
        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }


    }
}


  