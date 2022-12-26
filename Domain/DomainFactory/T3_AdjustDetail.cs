using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_AdjustDetail : GRCBaseDomain
    {
        int iD = 0;
        int adjustID = 0;
        int itemID = 0;
        int lokID = 0;
        int qtyIn = 0;
        int apv= 0; 
        int qtyOut = 0;
        string partNo = string.Empty;
        string lokasi= string.Empty;
        string adjustNo = string.Empty;
        DateTime adjustDate = DateTime.Now;
        string adjustType = string.Empty;
        string noBA = string.Empty;
        string approval= string.Empty;
        string keterangan = string.Empty;
        private int sA = 0;

        public int SA
        {
            get { return sA; }
            set { sA = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int Apv
        {
            get { return apv; }
            set { apv = value; }
        }
        public int AdjustID
        {
            get { return adjustID; }
            set { adjustID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int LokID
        {
            get { return lokID; }
            set { lokID = value; }
        }
        public int QtyIn
        {
            get { return qtyIn; }
            set { qtyIn = value; }
        }
        public int QtyOut
        {
            get { return qtyOut; }
            set { qtyOut = value; }
        }
        public string PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }
        public string AdjustNo
        {
            get { return adjustNo; }
            set { adjustNo = value; }
        }
        public DateTime  AdjustDate
        {
            get { return adjustDate; }
            set { adjustDate = value; }
        }
        public string AdjustType
        {
            get { return adjustType; }
            set { adjustType = value; }
        }

        public string NoBA
        {
            get { return noBA; }
            set { noBA = value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string Approval
        {
            get { return approval; }
            set { approval = value; }
        }
    }

   
}
