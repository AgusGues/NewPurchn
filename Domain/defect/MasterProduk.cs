using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterProduk : GRCBaseDomain
    {
        private string produkCode = string.Empty;
        private string produkName = string.Empty;
        //private int rowStatus = 

        public string ProdukCode
        {
            get
            {
                return produkCode;

            }
            set
            {
                produkCode = value;
            }
        }
        public string ProdukName
        {
            get
            {
                return produkName;
            }
            set
            {
                produkName = value;

            }

        }


    }

}
