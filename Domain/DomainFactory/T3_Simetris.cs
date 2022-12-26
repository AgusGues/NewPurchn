using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Simetris : GRCBaseDomain
    {
        private int iD = 0;
        private int groupID = 0;
        private string partnoSer = string.Empty;
        private string groupName = string.Empty;
        private int serahID = 0;
        private int rekapID=0;
        private string lokasiSer = string.Empty;
        private int itemID = 0;
        private DateTime tglSm = DateTime.Now.Date;
        private int lokasiID = 0;
        private string partnoSm = string.Empty;
        private string lokasiSm = string.Empty;
        private int qtyInSm = 0;
        private int qtyOutSm = 0;
        private int nCH = 0;
        private int nCSS = 0;
        private int nCSE = 0;
        private string bS = string.Empty;
        private string defect = string.Empty;
        private string mcutter = string.Empty;

        private DateTime tglproduksi = DateTime.Now.Date;

        public DateTime TglProduksi
        {
            get { return tglproduksi; }
            set { tglproduksi = value; }
        }
        public string MCutter
        {
            get { return mcutter; }
            set { mcutter = value; }
        }
        public string Defect
        {
            get { return defect; }
            set { defect = value; }
        }
        public string BS
        {
            get { return bS; }
            set { bS = value; }
        }
        public int NCH
        {
            get { return nCH; }
            set { nCH = value; }
        }
        public int NCSS
        {
            get { return nCSS; }
            set { nCSS = value; }
        }
        public int NCSE
        {
            get { return nCSE; }
            set { nCSE = value; }
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
        public string PartnoSer
        {
            get { return partnoSer; }
            set { partnoSer = value; }
        }
        public string PartnoSm
        {
            get { return partnoSm; }
            set { partnoSm = value; }
        }
        public string GroupName
        {
            get { return groupName ; }
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
        public DateTime TglSm
        {
            get { return tglSm; }
            set { tglSm = value; }
        }
        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string LokasiSer
        {
            get { return lokasiSer; }
            set { lokasiSer = value; }
        }
        public string LokasiSm
        {
            get { return lokasiSm; }
            set { lokasiSm = value; }
        }

        public int QtyInSm
        {
            get { return qtyInSm; }
            set { qtyInSm = value; }
        }
        public int QtyOutSm
        {
            get { return qtyOutSm; }
            set { qtyOutSm = value; }
        }
    }
}
