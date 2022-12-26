using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    //class MasterJenisProduct
    //{
    //}
    public class MasterJenisProduct : GRCBaseDomain
    {
        private string jenisProductCode = string.Empty;
        private string jenisProductName = string.Empty;
        //private int rowStatus = 

        public string JenisProductCode
        {
            get
            {
                return jenisProductCode;

            }
            set
            {
                jenisProductCode = value;
            }
        }
        public string JenisProductName
        {
            get
            {
                return jenisProductName;
            }
            set
            {
                jenisProductName = value;

            }

        }


    }

}
