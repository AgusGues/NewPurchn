using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ScheduleOP
    {
        private string scheduleNo = string.Empty;
        private DateTime scheduleDate = DateTime.Now.Date;
        private string oPNo = string.Empty;
        private DateTime oPDate = DateTime.Now.Date;
        private string address = string.Empty;
        private string alamatLain = string.Empty;
        private string customerType = string.Empty;
        private int customerID = 0;
        private string tokoCustName = string.Empty;

        public string TokoCustName
        {
            get
            {
                return tokoCustName;
            }
            set
            {
                tokoCustName = value;
            }
        }
        public string ScheduleNo
        {
            get
            {
                return scheduleNo;
            }
            set
            {
                scheduleNo = value;
            }
        }

        public DateTime ScheduleDate
        {
            get
            {
                return scheduleDate;
            }
            set
            {
                scheduleDate = value;
            }
        }

        public string OPNo
        {
            get
            {
                return oPNo;
            }
            set
            {
                oPNo = value;
            }
        }

        public DateTime OPDate
        {
            get
            {
                return oPDate;
            }
            set
            {
                oPDate = value;
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

        public string AlamatLain
        {
            get
            {
                return alamatLain;
            }
            set
            {
                alamatLain = value;
            }
        }

        public string CustomerType
        {
            get
            {
                return customerType;
            }
            set
            {
                customerType = value;
            }
        }

        public int CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
    }
}
