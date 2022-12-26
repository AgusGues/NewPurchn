using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Otor : GRCBaseDomain
    {
        private int id = 0;
        private string nama1 = string.Empty;
        private string nama2 = string.Empty;
        private string nama3 = string.Empty;
        private string nPWP = string.Empty;
        private string sPPKP = string.Empty;
        private int bAPP = 0;
        private string sPV1 = string.Empty;
        private string aDM1 = string.Empty;


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

        public string Nama1
        {
            get
            {
                return nama1;
            }
            set
            {
                nama1 = value;
            }
        }

        public string Nama2
        {
            get
            {
                return nama2;
            }
            set
            {
                nama2 = value;
            }
        }

        public string Nama3
        {
            get
            {
                return nama3;
            }
            set
            {
                nama3 = value;
            }
        }

        public string NPWP
        {
            get
            {
                return nPWP;
            }
            set
            {
                nPWP = value;
            }
        }
        public string SPPKP
        {
            get
            {
                return sPPKP;
            }
            set
            {
                sPPKP = value;
            }
        }

        public int BAPP
        {
            get
            {
                return bAPP;
            }
            set
            {
                bAPP = value;
            }
        }

        public string SPV1
        {
            get
            {
                return sPV1;
            }
            set
            {
                sPV1 = value;
            }
        }

        public string ADM1
        {
            get
            {
                return aDM1;
            }
            set
            {
                aDM1 = value;
            }
        }


    }
}
