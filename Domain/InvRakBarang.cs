using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class InvRakBarang : Inventory
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int Status { get; set; }
        public int UOMID { get; set; }
        public int RakID { get; set; }
        public string RName { get; set; }
        public decimal Qty { get; set; }
       
        

    }

    public class InvR : InvRakBarang
    {
        public string RakNo { get; set; }
        public string UOMDesc { get; set; }
        //public string criteria { get; set; }
        //public string pilihan { get; set; }
       // public string RName { get; set; }
        //public string ItemCode {get; set;}
        //public string ItemName { get; set; }
        //public string UOMCode { get; set; }
        //public int Status { get; set; }
        //public string RName { get; set; }
        //public string RakNo { get; set; }

    }
    
    
}
