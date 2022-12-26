using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Schedule : GRCBaseDomain
    {
        private string scheduleNo = string.Empty;
        private DateTime scheduleDate = DateTime.Now.Date;
        private int expedisiDetailID = 0;
        private string expedisiName = string.Empty;
        private string carType = string.Empty;        
        private decimal totalKubikasi = 0;
        private int status = 0;
        private string rate = string.Empty;
        private string keterangan = string.Empty;
        private string alasanCancel = string.Empty;
        private int depoID = 0;
        private decimal minimalMuatan = 0;
        private int approvalKubikasi = 0;
        private int sendSMS = 0;
        private string kodeRoute = string.Empty;
        private string approvalBy = string.Empty;
        private DateTime approvalDate = DateTime.Now.Date;

        public DateTime ApprovalDate
        {
            get { return approvalDate; }
            set { approvalDate = value; }
        }
        public string ApprovalBy
        {
            get { return kodeRoute; }
            set { kodeRoute = value; }
        }
        public string KodeRoute
        {
            get { return kodeRoute; }
            set { kodeRoute = value; }
        }
        public int SendSMS
        {
            get { return sendSMS; }
            set { sendSMS = value; }
        }
        public int ApprovalKubikasi
        {
            get
            {
                return approvalKubikasi;
            }
            set
            {
                approvalKubikasi = value;
            }
        }
        public decimal MinimalMuatan
        {
            get
            {
                return minimalMuatan;
            }
            set
            {
                minimalMuatan = value;
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

        public int ExpedisiDetailID
        {
            get
            {
                return expedisiDetailID;
            }
            set
            {
                expedisiDetailID = value;
            }
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

        public string CarType
        {
            get
            {
                return carType;
            }
            set
            {
                carType = value;
            }
        }
      
        public decimal TotalKubikasi
        {
            get
            {
                return totalKubikasi;
            }
            set
            {
                totalKubikasi = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public string Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
            }
        }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }
        }

        public string AlasanCancel
        {
            get
            {
                return alasanCancel;
            }
            set
            {
                alasanCancel = value;
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
    }
}
