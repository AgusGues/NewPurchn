using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Retur : GRCBaseDomain
    {
        private int serahID = 0;
        private int groupID = 0;
        private string lokasiSer = string.Empty;
        private string sjno = string.Empty;
        private string opno = string.Empty;
        private string customer = string.Empty;
        private string groupDesc = string.Empty;
        private int qty = 0;
        private int itemIDSer = 0;
        private int itemIDSJ = 0;
        private string partnoRtr = string.Empty;
        private DateTime tgltrans = DateTime.Now.Date;
        private int lokasiID = 0;
        private string lokasiRtr = string.Empty;
        decimal hPP = 0;
        private int returID = 0;
        private string expedisi = string.Empty;
        private int apv = 0;
        private int sA = 0;
        private int typeR = 0;

        string jDefect = string.Empty;
        string groupProd = string.Empty;
        private DateTime tglProd = DateTime.Now;


        public DateTime TglProd
        {
            get { return tglProd; }
            set { tglProd = value; }
        }
        public string JDefect
        {
            get { return jDefect; }
            set { jDefect = value; }
        }
        public string GroupProd
        {
            get { return groupProd; }
            set { groupProd = value; }
        }

        public int TypeR
        {
            get { return typeR; }
            set { typeR = value; }
        }
        public int SA
        {
            get { return sA; }
            set { sA = value; }
        }
        public decimal HPP
        {
            get { return hPP ; }
            set { hPP = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int ReturID
        {
            get { return returID; }
            set { returID = value; }
        }
        public int Apv
        {
            get { return apv; }
            set { apv = value; }
        }
        public string Expedisi
        {
            get { return expedisi; }
            set { expedisi = value; }
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
            get { return sjno; }
            set { sjno = value; }
        }
        public string OPNO
        {
            get { return opno; }
            set { opno = value; }
        }
        public string Customer
        {
            get { return customer; }
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
        public string PartnoRtr
        {
            get { return partnoRtr; }
            set { partnoRtr = value; }
        }
        public DateTime Tgltrans
        {
            get { return tgltrans; }
            set { tgltrans = value; }
        }
        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }
        public string LokasiRtr
        {
            get { return lokasiRtr; }
            set { lokasiRtr = value; }
        }
    }


}
