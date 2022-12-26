using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_Adjust : GRCBaseDomain
    {
        int iD = 0;
        string adjustNo = string.Empty;
        DateTime adjustDate = DateTime.Now;
        string adjustType = string.Empty;
        string noBA = string.Empty;
        string keterangan = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
    }

    public class T1Adjust
    {
        public int ID { get; set; }
        public string AdjustNo { get; set; }
        public string AdjustType { get; set; }
        public string NoBA { get; set; }
        public string TglAdjust { get; set; }
        public DateTime AdjustDate { get; set; }
        public string DateAdjust
        {
            get { return AdjustDate.ToShortDateString(); }
            set { AdjustDate.ToShortDateString(); }
        }
        public string Keterangan { get; set; }
        public string CreatedBy { get; set; }
        public int DepoID { get; set; }
    }
}
