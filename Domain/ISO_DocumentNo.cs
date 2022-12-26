using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_DocumentNo
    {
        private int id = 0;
        private int docNo = 0;
        private int pesType = 0;
        private int deptID = 0;
        private int tahun = 0;
        private string plant = string.Empty;

        public string Plant
        {
            get{return plant;}
            set{plant = value;}
        }
        public int ID
        {
            get{return id;}
            set{id = value;}
        }
        public int PesType
        {
            get { return pesType; }
            set { pesType = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }
        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }


    }
}
