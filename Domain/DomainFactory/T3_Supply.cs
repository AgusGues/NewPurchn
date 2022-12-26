using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Supply : GRCBaseDomain
    {
        private int serahID = 0;
        private int groupID = 0;
        private string groupDesc = string.Empty;
        private int qty = 0;
        private int itemID = 0;
        private string partno = string.Empty;
        private DateTime tgltrans = DateTime.Now.Date;
        private int lokasiID = 0;
        private string lokasi = string.Empty;
        decimal hPP = 0;

        public decimal HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
        public string GroupDesc
        {
            get { return groupDesc; }
            set { groupDesc = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string Partno
        {
            get { return partno; }
            set { partno = value; }
        }
        public DateTime TglTrans
        {
            get { return tgltrans; }
            set { tgltrans = value; }
        }
        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }
    }
}
