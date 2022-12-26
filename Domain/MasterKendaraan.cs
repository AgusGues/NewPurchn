using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterKendaraan : GRCBaseDomain
    {
        private string noPolisi = string.Empty;
        private string jenisMobil = string.Empty;
        private int expedisiID = 0;
        private string expedisiName = string.Empty;
        private int expedisiDetailID = 0;
        private decimal target = 0;
        private int status = 0;

        public string NoPolisi
        {
            get { return noPolisi; }
            set { noPolisi = value; }
        }

        public string JenisMobil
        {
            get { return jenisMobil; }
            set { jenisMobil = value; }
        }

        public int ExpedisiID
        {
            get { return expedisiID; }
            set { expedisiID = value; }
        }

        public string ExpedisiName
        {
            get { return expedisiName; }
            set { expedisiName = value; }
        }
        public int ExpedisiDetailID
        {
            get { return expedisiDetailID; }
            set { expedisiDetailID = value; }
        }

        public decimal Target
        {
            get { return target; }
            set { target = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }



       


    }   
}
