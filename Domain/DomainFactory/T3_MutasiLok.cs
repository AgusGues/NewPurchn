using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_MutasiLok : GRCBaseDomain
    {
        private int iD = 0;
        private int groupID = 0;
        private int itemID = 0;
        private int lokID1 = 0;
        private int lokID2 = 0;
        private string partno = string.Empty;
        private string groupName = string.Empty;
        private int serahID = 0;
        private int rekapID = 0;
        private string lokasi1 = string.Empty;
        private string lokasi2 = string.Empty;
        private DateTime tglML = DateTime.Now.Date;
        private int qty = 0;
        private int sA1 = 0;
        private int sA2 = 0;

        public int SA2
        {
            get { return sA2; }
            set { sA2 = value; }
        }
        public int SA1
        {
            get { return sA1; }
            set { sA1 = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public string Partno
        {
            get { return partno; }
            set { partno = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int RekapID
        {
            get { return rekapID; }
            set { rekapID = value; }
        }
        public DateTime TglML
        {
            get { return tglML; }
            set { tglML = value; }
        }
        public int LokID1
        {
            get { return lokID1; }
            set { lokID1 = value; }
        }
        public int LokID2
        {
            get { return lokID2; }
            set { lokID2 = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string Lokasi1
        {
            get { return lokasi1; }
            set { lokasi1 = value; }
        }
        public string Lokasi2
        {
            get { return lokasi2; }
            set { lokasi2 = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
    }
}
