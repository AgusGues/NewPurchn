using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SM_LoadingTimeDetail : GRCBaseDomain
    {
        private int loadingID = 0;
        private string noPolisi = string.Empty;
        private int ekspedisiID = 0;
        private string ekspedisiName = string.Empty;
        private DateTime waktuTunggu = DateTime.Now.Date;
        private string statusLoading = string.Empty;
        private string keterangan = string.Empty;
        private int rowStatus = 0;

        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }


        public int LoadingID
        {
            get { return loadingID; }
            set { loadingID = value; }
        }
        public string NoPolisi
        {
            get { return noPolisi; }
            set { noPolisi = value; }
        }

        public DateTime WaktuTunggu
        {
            get { return waktuTunggu; }
            set { waktuTunggu = value; }
        }
                
        public int EkspedisiID
        {
            get { return ekspedisiID; }
            set { ekspedisiID = value; }
        }
        public string EkspedisiName
        {
            get { return ekspedisiName; }
            set { ekspedisiName = value; }
        }

        public string StatusLoading
        {
            get { return statusLoading; }
            set { statusLoading= value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
    }
}

