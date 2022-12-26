using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuratJalankeluar : GRCBaseDomain
    {
        //private int nourut = 0;
        //private string nosj = string.Empty;
        //private DateTime tglsj = DateTime.Now.Date;
        //private string tujuan = string.Empty;
        //private string itemcode = string.Empty;
        //private string itemname = string.Empty;
        //private decimal jumlah = 0;
        //private string ket = string.Empty;
        //private string nopolisi = string.Empty;
        //private int rowsatus = 0;
        //private string createdby = string.Empty;
        //private DateTime createdtime = DateTime.Now.Date;
        //private string lastmodifiedby = string.Empty;
        //private DateTime lastmodifiedtime = DateTime.Now.Date;

        public int NoUrut { get; set; }
        public string NoSJ { get; set; }
        public DateTime TglSJ { get; set; }
        public string Tujuan { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public string Satuan { get; set; }
        public decimal Jumlah { get; set; }
        public string Ket { get; set; }
        public string NoPolisi { get; set; }
        public string TypeDescription { get; set; }
        //public int RowStatus { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedTime { get; set; }
        //public string LastModifiedBy { get; set; }
       // public DateTime LastModifiedTime { get; set; }

    }
}
