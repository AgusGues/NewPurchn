using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SPPMultiGudang : GRCBaseDomain
    {
        int iD = 0;
        int sPPID = 0;
        int gudangID= 0; 
        int itemID= 0; 
        int groupID= 0;
        int itemTypeID = 0; 
        decimal qtyReceipt= 0;
        decimal qtyPakai= 0;
        int aktif = 0;

        public int ID { get { return iD; } set { iD = value; }}
        public int SPPID { get { return sPPID; } set { sPPID = value; } }
        public int GudangID { get { return gudangID; } set { gudangID = value; } }
        public int ItemID { get { return itemID; } set { itemID = value; } }
        public int GroupID { get { return groupID; } set { groupID = value; } }
        public int ItemTypeID { get { return itemTypeID; } set { itemTypeID = value; } }
        public decimal QtyReceipt { get { return qtyReceipt; } set { qtyReceipt = value; } }
        public decimal QtyPakai { get { return qtyPakai; } set { qtyPakai = value; } }
        public int Aktif { get { return aktif; } set { aktif = value; } }
    }
}
