using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class KartuStockNf
    {
        public class ParamItem
        {
            public int ID { get; set; }
            public int ItemTypeID { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
        }
        public class ParamData
        {
            public int IDn { get; set; }
            public int Type { get; set; }
            public string Urut { get; set; }
            public string Id { get; set; }

            public string Tanggal { get; set; }
            public string Faktur { get; set; }
            public string Masuk { get; set; }
            public string Keluar { get; set; }
            public string Keterangan { get; set; }
            public string Saldo { get; set; }

        }
        public class ParamInv
        {
            public int ID { get; set; }
            public int GroupID { get; set; }
            public int ItemTypeID { get; set; }
        }
        public class ParamInfoItem
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string UOMDesc { get; set; }
            public int MinStock { get; set; }
            public int MaxStock { get; set; }
            public int ReOrder { get; set; }
        }
    }
}
