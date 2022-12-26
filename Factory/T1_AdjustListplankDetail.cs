using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_AdjustListplankDetail : GRCBaseDomain
    {
        int iD = 0;
        int adjustID = 0;
        int itemID = 0;
        int itemID0 = 0;
        int qty = 0;
        int apv = 0;
        string partNo = string.Empty;
        string adjustNo = string.Empty;
        DateTime adjustDate = DateTime.Now;
        string adjustType = string.Empty;
        string noBA = string.Empty;
        string approval = string.Empty;
        string process = string.Empty;
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
        public int ItemID0
        {
            get { return itemID0; }
            set { itemID0 = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        
        public string PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }
        public string AdjustNo
        {
            get { return adjustNo; }
            set { adjustNo = value; }
        }
        public DateTime AdjustDate
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

        public string Process
        {
            get { return process; }
            set { process = value; }
        }
        public string Approval
        {
            get { return approval; }
            set { approval = value; }
        }
    }
}
