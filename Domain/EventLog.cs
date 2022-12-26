using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class EventLog
    {
        private int id = 0;
        private string modulName = string.Empty;
        private string eventName = string.Empty;
        private string documentNo = string.Empty;
        private string createdBy = string.Empty;
        private DateTime createdTime = DateTime.Now.Date;
        public int Bulan { get; set; }
        public int Tahun { get; set; }
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
        public string ModulName
        {
            get
            {
                return modulName;
            }
            set
            {
                modulName = value;
            }
        }
        public string EventName
        {
            get 
            {
                return eventName;
            }
            set
            {
                eventName = value;
            }
        }
        public string DocumentNo
        {
            get
            {
                return documentNo;
            }
            set
            {
                documentNo = value;
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
        //purpose for eventlog approval
        public int UserID {get;set;}
        public string DocNo { get; set; }
        public string DocType { get; set; }
        public int AppLevel {get;set;}
        public DateTime AppDate {get;set;}
        public string IPAddress { get; set; }
        public string Keterangan { get; set; }
    }
}
