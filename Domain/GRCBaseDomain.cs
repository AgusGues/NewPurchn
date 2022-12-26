using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class GRCBaseDomain
    {
        private int id = 0;
        private int rowStatus = 0;
        private string createdBy = string.Empty;
        private DateTime createdTime = DateTime.Now.Date;
        private string lastModifiedBy = string.Empty;
        private DateTime lastModifiedTime = DateTime.Now.Date;
        private string uomCode = string.Empty;
        private string uomDesc = string.Empty;
        private string satuan = string.Empty;

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

        public int RowStatus
        {
            get
            {
                return rowStatus;
            }
            set
            {
                rowStatus = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public DateTime CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
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

        public string UOMCode
        {
            get
            {
                return uomCode;
            }
            set
            {
                uomCode = value;
            }
        }


        public string UOMDesc
        {
            get
            {
                return uomDesc;
            }
            set
            {
                uomDesc = value;
            }
        }
        public string Satuan
        {
            get
            {
                return satuan;
            }
            set
            {
                satuan = value;
            }
        }
        public virtual string Unit { get; set; }




    }
}
