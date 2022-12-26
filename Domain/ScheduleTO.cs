using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ScheduleTO
    {
        private string scheduleNo = string.Empty;
        private DateTime scheduleDate = DateTime.Now.Date;
        private string transferOrderNo = string.Empty;
        private DateTime transferOrderDate = DateTime.Now.Date;
        private string fromDepo = string.Empty;
        private string toDepo = string.Empty;
        private string fromDepoAddress = string.Empty;
        private string toDepoAddress = string.Empty;

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

        public string TransferOrderNo
        {
            get
            {
                return transferOrderNo;
            }
            set
            {
                transferOrderNo = value;
            }
        }

        public DateTime TransferOrderDate
        {
            get
            {
                return transferOrderDate;
            }
            set
            {
                transferOrderDate = value;
            }
        }

        public string FromDepo
        {
            get
            {
                return fromDepo;
            }
            set
            {
                fromDepo = value;
            }
        }

        public string ToDepo
        {
            get
            {
                return toDepo;
            }
            set
            {
                toDepo = value;
            }
        }

        public string FromDepoAddress
        {
            get
            {
                return fromDepoAddress;
            }
            set
            {
                fromDepoAddress = value;
            }
        }

        public string ToDepoAddress
        {
            get
            {
                return toDepoAddress;
            }
            set
            {
                toDepoAddress = value;
            }
        }
    }
}
