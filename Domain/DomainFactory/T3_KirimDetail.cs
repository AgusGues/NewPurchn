using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_KirimDetail : GRCBaseDomain
    {
        private int kirimID = 0;
        private int serahID = 0;
        private int groupID = 0;
        private string lokasiSer = string.Empty;
        private string sjno = string.Empty;
        private string opno = string.Empty;
        private string customer= string.Empty;
        private string groupDesc = string.Empty;
        private int qty = 0;
        private int itemIDSer = 0;
        private int itemIDSJ = 0;
        private string partnoKrm = string.Empty;
        private DateTime tgltrans = DateTime.Now.Date;
        private int lokasiID = 0;
        private string lokasiKrm= string.Empty;
        private int t3siapKirimID = 0;
        private int lokasiLoadingID = 0;
        decimal hPP = 0;
        private string pengiriman = string.Empty;
        private string jenisPalet = string.Empty;
        private int jmlPalet = 0;

        public string Pengiriman
        {
            get { return pengiriman; }
            set { pengiriman = value; }
        }
        public string JenisPalet
        {
            get { return jenisPalet; }
            set { jenisPalet = value; }
        }
        public int JmlPalet
        {
            get { return jmlPalet; }
            set { jmlPalet = value; }
        }
        public decimal  HPP
        {
            get { return hPP ; }
            set { hPP = value; }
        }
        public int KirimID
        {
            get { return kirimID; }
            set { kirimID = value; }
        }
        public int T3siapKirimID
        {
            get { return t3siapKirimID; }
            set { t3siapKirimID = value; }
        }
        public int LokasiLoadingID
        {
            get { return lokasiLoadingID; }
            set { lokasiLoadingID = value; }
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
        public string LokasiSer
        {
            get { return lokasiSer; }
            set { lokasiSer = value; }
        }
        public string SJNO
        {
            get { return sjno ; }
            set { sjno = value; }
        }
        public string OPNO
        {
            get { return opno ; }
            set { opno = value; }
        }
        public string Customer
        {
            get { return customer ; }
            set { customer = value; }
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
        public int ItemIDSJ
        {
            get { return itemIDSJ; }
            set { itemIDSJ = value; }
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
