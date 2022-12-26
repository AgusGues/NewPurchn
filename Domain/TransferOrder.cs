using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class TransferOrder : GRCBaseDomain
    {
        private string transferOrderNo = string.Empty;
        private DateTime transferOrderDate = DateTime.Now.Date;
        private int fromDepoID = 0;        
        private string fromDepoName = string.Empty;
        private string fromDepoAddress = string.Empty;
        private int toDepoID = 0;        
        private string toDepoName = string.Empty;
        private string toDepoAddress = string.Empty;
        private string kotaTujuan = string.Empty;
        private string areaTujuan = string.Empty;     
        private decimal totalKubikasi = 0;
        private int status = 0;
        private string keterangan = string.Empty;
        
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

        public int FromDepoID
        {
            get
            {
                return fromDepoID;
            }
            set
            {
                fromDepoID = value;
            }
        }

        public string FromDepoName
        {
            get
            {
                return fromDepoName;
            }
            set
            {
                fromDepoName = value;
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


        public int ToDepoID
        {
            get
            {
                return toDepoID;
            }
            set
            {
                toDepoID = value;
            }
        }

        public string ToDepoName
        {
            get
            {
                return toDepoName;
            }
            set
            {
                toDepoName = value;
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

    }
}
