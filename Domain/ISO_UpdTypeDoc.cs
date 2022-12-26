using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ISO_UpdTypeDoc : GRCBaseDomain                       
    {
        private int iD = 0;
        private string docTypeCode = string.Empty;
        private string docTypeName = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string  DocTypeCode
        {
            get { return docTypeCode; }
            set { docTypeCode = value; }
        }
        public string DocTypeName
        {
            get { return docTypeName; }
            set { docTypeName = value; }
        }
        
    }
}
