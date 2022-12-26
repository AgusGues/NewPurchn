using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ScheduleDetail
    {
        private int id = 0;
        private int scheduleID = 0;        
        private int itemId = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int qty = 0;
        private int documentID = 0;
        private string documentNo = string.Empty;
        private int documentDetailID = 0;
        private int typeDoc = 0;
        private decimal totalKubikasi = 0;
        private string kotaTujuan = string.Empty;
        private string areaTujuan = string.Empty;
        private int status = 0;
        private int paket = 0;
        private string opNo2 = string.Empty;
        private string tokoCode = string.Empty;
        private string tokoName = string.Empty;
        private string depoName = string.Empty;
        private string customerCode = string.Empty;
        private string customerName = string.Empty;
        private string scheduleNo = string.Empty;
        private DateTime scheduleDate = DateTime.Now.Date;
        private int depoID = 0;
        private decimal berat = 0;
        private string alasanCancel = string.Empty;
        private int ptID = 0;
        private DateTime approvalSCDate = DateTime.MinValue;

        public DateTime ApprovalSCDate
        {
            get { return approvalSCDate; }
            set { approvalSCDate = value; }
        }
        public int PTID
        {
            get {return ptID;}
            set { ptID = value; }
        }
        public string AlasanCancel
        {
            get {return alasanCancel;}
            set { alasanCancel = value; }
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

        public int ScheduleID
        {
            get
            {
                return scheduleID;
            }
            set
            {
                scheduleID = value;
            }
        }

        public int DocumentID
        {
            get
            {
                return documentID;
            }
            set
            {
                documentID = value;
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

        public int DocumentDetailID
        {
            get
            {
                return documentDetailID;
            }
            set
            {
                documentDetailID = value;
            }
        }

        public int TypeDoc
        {
            get
            {
                return typeDoc;
            }
            set
            {
                typeDoc = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
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

        public string KotaTujuan
        {
            get
            {
                return kotaTujuan;
            }
            set
            {
                kotaTujuan = value;
            }
        }

        public string AreaTujuan
        {
            get
            {
                return areaTujuan;
            }
            set
            {
                areaTujuan = value;
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

        public int Paket
        {
            get
            {
                return paket;
            }
            set
            {
                paket = value;
            }
        }

        public string OpNo2
        {
            get
            {
                return opNo2;
            }
            set
            {
                opNo2 = value;
            }
        }

        public string TokoCode
        {
            get
            {
                return tokoCode;
            }
            set
            {
                tokoCode = value;
            }
        }

        public string TokoName
        {
            get
            {
                return tokoName;
            }
            set
            {
                tokoName = value;
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

        public string CustomerCode
        {
            get
            {
                return customerCode;
            }
            set
            {
                customerCode = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }

        public decimal Berat
        {
            get
            {
                return berat;
            }
            set
            {
                berat = value;
            }
        }
    }
}
