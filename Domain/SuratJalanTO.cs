using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuratJalanTO : SuratJalan
    {
        private int transferOrderID = 0;
        private string transferOrderNo = string.Empty;
        private string alasanTurunStatus = string.Empty;

        public string AlasanTurunStatus
        {
            get { return alasanTurunStatus; }
            set { alasanTurunStatus = value; }
        }
        public int TransferOrderID
        {
            get
            {
                return transferOrderID;
            }
            set
            {
                transferOrderID = value;
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
    }
}
