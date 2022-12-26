using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Rak : GRCBaseDomain
    {
        private int id = 0;
        private string rakNo = string.Empty;
        private string lotNo = string.Empty;
        private string keterangan = string.Empty;

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

        public string RakNo
        {
            get
            {
                return rakNo;
            }
            set
            {
                rakNo = value;
            }
        }

        public string LotNo
        {
            get
            {
                return lotNo;
            }
            set
            {
                lotNo = value;
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
