using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Asimetris : GRCBaseDomain
    {
        int serahID = 0;
        int rekapID = 0;
        int groupID = 0;
        int itemIDIn = 0;
        int lokIDIn = 0;
        int qtyIn = 0;
        DateTime  tglTrans = DateTime.Now ;
        int itemIDOut = 0;
        int lokIDOut = 0;
        int qtyOut =0;
        int sA = 0;
        decimal luas;
        decimal hPP;
        string groupName = string.Empty;
        string docNo = string.Empty;
        string partnoIn = string.Empty;
        string lokasiIn = string.Empty;
        string partnoOut = string.Empty;
        string lokasiOut = string.Empty;
        string mcutter = string.Empty;


        public String MCutter
        {
            get { return mcutter; }
            set { mcutter = value; }
        }
        public int SA
        {
            get { return sA; }
            set { sA = value; }
        }
        public int SerahID
        {
            get { return serahID ; }
            set { serahID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public decimal  Luas
        {
            get { return luas ; }
            set { luas = value; }
        }
        public decimal HPP
        {
            get { return hPP ; }
            set { hPP  = value; }
        }
        public int RekapID
        {
            get { return rekapID; }
            set { rekapID = value; }
        }
        public int ItemIDIn
        {
            get { return itemIDIn; }
            set { itemIDIn = value; }
        }
        public int LokIDIn
        {
            get { return lokIDIn ; }
            set { lokIDIn = value; }
        }
        public int QtyIn
        {
            get { return qtyIn ; }
            set { qtyIn = value; }
        }
        public DateTime  TglTrans
        {
            get { return tglTrans ; }
            set { tglTrans  = value; }
        }
        public int ItemIDOut
        {
            get { return itemIDOut ; }
            set { itemIDOut  = value; }
        }
        public int LokIDOut
        {
            get { return lokIDOut; }
            set { lokIDOut = value; }
        }
        public int QtyOut
        {
            get { return qtyOut; }
            set { qtyOut = value; }
        }
        public String  DocNo
        {
            get { return docNo ; }
            set { docNo  = value; }
        }
        public String PartnoIn
        {
            get { return partnoIn ; }
            set { partnoIn = value; }
        }
        public String LokasiIn
        {
            get { return lokasiIn; }
            set { lokasiIn = value; }
        }
        public String PartnoOut
        {
            get { return partnoOut ; }
            set { partnoOut = value; }
        }
        public String LokasiOut
        {
            get { return lokasiOut; }
            set { lokasiOut = value; }
        }
        public String GroupName
        {
            get { return groupName ; }
            set { groupName = value; }
        }
    }
}
