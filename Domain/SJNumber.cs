using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SJNumber
    {
        private int id = 0;
        private int depoID = 0;
        private int sjCounter1 = 0;
        private int sjCounter2 = 0;
        private string lastModifiedBy = string.Empty;
        private DateTime lastModifiedTime = DateTime.Now.Date;
        private int flag = 0;
        private string codeSJTO = string.Empty;
        private int tahun = 0;
        private int subCompanyID = 0;

        public int SubCompanyID
        {
            get
            {
                return subCompanyID;
            }
            set
            {
                subCompanyID = value;
            }
        }
        public int Tahun
        {
            get
            {
                return tahun;
            }
            set
            {
                tahun = value;
            }
        }

        public string CodeSJTO
        {
            get
            {
                return codeSJTO;
            }
            set
            {
                codeSJTO = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }

        public int SJCounter1
        {
            get
            {
                return sjCounter1;
            }
            set
            {
                sjCounter1 = value;
            }
        }

        public int SJCounter2
        {
            get
            {
                return sjCounter2;
            }
            set
            {
                sjCounter2 = value;
            }
        }
        
        public string LastModifiedBy
        {
            get
            {
                return lastModifiedBy;
            }
            set
            {
                lastModifiedBy = value;
            }
        }

        public DateTime LastModifiedTime
        {
            get
            {
                return lastModifiedTime;
            }
            set
            {
                lastModifiedTime = value;
            }
        }

        public int Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
    }
}
