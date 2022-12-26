using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ISO_UpdMasterDoc : GRCBaseDomain                       
    {
        private int iD = 0;
        private string docCategory = string.Empty;
        private string NamaDept = string.Empty;
        private int iddept = 0;
        private int iddoc = 0;

        public string DeptIDstring { get; set; }
       
        public int IDdoc
        {
            get { return iddoc; }
            set { iddoc = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int idDept
        {
            get { return iddept; }
            set { iddept = value; }
        }
        public string  DocCategory
        {
            get { return docCategory; }
            set { docCategory = value; }
        }
        public string namaDept
        {
            get { return NamaDept; }
            set { NamaDept = value; }
        }
        
    }
}
