using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Expedisi : GRCBaseDomain
    {        
        private string expedisiName = string.Empty;
        private string address = string.Empty;
        private string telp = string.Empty;
        private string handphone = string.Empty;
        private string upName = string.Empty;
        private int depoID = 0;
        private string depoName = string.Empty;

        private int companyID = 0;
        public int CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }

        public string ExpedisiName
        {
            get
            {
                return expedisiName;
            }
            set
            {
                expedisiName = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string Telp
        {
            get
            {
                return telp;
            }
            set
            {
                telp = value;
            }
        }

        public string Handphone
        {
            get
            {
                return handphone;
            }
            set
            {
                handphone = value;
            }
        }
        public string UPName
        {
            get
            {
                return upName;
            }
            set
            {
                upName = value;
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

        public string DepoName
        {
            get
            {
                return depoName;
            }
            set
            {
                depoName = value;
            }
        }
        // added by Razib
        private string carType = string.Empty;
        private int expedisiID = 0;
        private DateTime scheduleDate = DateTime.Now;
        private string description = string.Empty;
        private decimal qty = 0;
        private int iDx = 0;
        //
        //added by Razib
        public int IDx { get { return iDx; } set { iDx = value; } }
        public string CarType { get { return carType; } set { carType = value; } }
        public int ExpedisiID { get { return expedisiID; } set { expedisiID = value; } }
        public DateTime ScheduleDate { get { return scheduleDate; } set { scheduleDate = value; } }
        public string Description { get { return description; } set { description = value; } }
        public decimal Qty { get { return qty; } set { qty = value; } }
        //
        public string ExScheduleNo { get; set; }

    }
}
