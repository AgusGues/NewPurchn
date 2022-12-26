using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FC_ItemsNF
    {
        public int ID { get; set; }
        public string PartNo { get; set; }
        public int Tebal { get; set; }
        public int Panjang { get; set; }
        public int Lebar { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public float Volume { get; set; }
        public string Lokasi { get; set; }
        public int GroupID { get; set; }
        public int LokID { get; set; }
        public int ItemID { get; set; }
        public int HPP { get; set; }
        public int ItemTypeID { get; set; }
        public string Kode { get; set; }
        public string CreatedBy { get; set; }
    }
}
