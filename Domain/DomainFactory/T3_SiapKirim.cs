using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_SiapKirim : GRCBaseDomain
    {
        private int serahID = 0;
        private int groupID = 0;
        private string lokasiSer = string.Empty;
        private string groupDesc = string.Empty;
        private int qty = 0;
        private int itemIDSer = 0;
        private string partnoKrm = string.Empty;
        private DateTime tgltrans = DateTime.Now.Date;
        private int lokasiID = 0;
        private string lokasiKrm= string.Empty;
        decimal hPP = 0;
        private string sJNo = string.Empty;
        private int sA = 0;

        public int SA
        {
            get { return sA; }
            set { sA = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public decimal HPP
        {
            get { return hPP; }
            set { hPP  = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public string LokasiSer
        {
            get { return lokasiSer; }
            set { lokasiSer = value; }
        }
        public string SJNo
        {
            get { return sJNo; }
            set { sJNo = value; }
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
        public int ItemIDSer
        {
            get { return itemIDSer; }
            set { itemIDSer = value; }
        }
        public string  PartnoKrm
        {
            get { return partnoKrm; }
            set { partnoKrm = value; }
        }
        public DateTime Tgltrans
        {
            get { return tgltrans; }
            set { tgltrans = value; }
        }
        public int LokasiID
        {
            get { return lokasiID ; }
            set { lokasiID = value; }
        }
        public string LokasiKrm
        {
            get { return lokasiKrm ; }
            set { lokasiKrm = value; }
        }
    }
}
