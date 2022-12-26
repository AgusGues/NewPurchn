using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class T3_Rekap : GRCBaseDomain
    {
        private int iD = 0;
        private int groupID = 0;
        private int destID = 0;
        private string plantGroup = string.Empty;
        private string formula = string.Empty;
        private string palet = string.Empty;
        private string partnoDest = string.Empty;
        private string partnoSer = string.Empty;
        private string partnoTrm = string.Empty;
        private DateTime tglProduksi = DateTime.Now.Date;
        private int serahID = 0;
        private int t1serahID = 0;
        private int t1sItemID = 0;
        private int t1sLokID = 0;
        private DateTime tglSerah = DateTime.Now.Date;
        private string lokasiSer = string.Empty;
        private int qtyInSer = 0;
        private int itemIDSer = 0;
        private DateTime tglTrm = DateTime.Now.Date;
        private int lokasiID = 0;
        private string lokasiTrm = string.Empty;
        private int qtyInTrm = 0;
        private int qtyOutTrm = 0;
        string keterangan = string.Empty;
        decimal hPP = 0;
        string process = string.Empty;
        string sfrom = string.Empty;
        string groups = string.Empty;
        private int sA = 0;
        private int cutID = 0;
        private int cutQty = 0;
        private int cutLevel = 0;


        public int CutID
        {
            get { return cutID; }
            set { cutID = value; }
        }
        public int CutQty
        {
            get { return cutQty; }
            set { cutQty = value; }
        }
        public int CutLevel
        {
            get { return cutLevel; }
            set { cutLevel = value; }
        }
        public int SA
        {
            get { return sA; }
            set { sA = value; }
        }
        public decimal HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public int T1SLokID
        {
            get { return t1sLokID ; }
            set { t1sLokID = value; }
        }
        public int DestID
        {
            get { return destID; }
            set { destID = value; }
        }
        public string PlantGroup
        {
            get { return plantGroup; }
            set { plantGroup = value; }
        }
        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }
        public string Sfrom
        {
            get { return sfrom; }
            set { sfrom = value; }
        }
        public string LokasiSer
        {
            get { return lokasiSer; }
            set { lokasiSer = value; }
        }
        public string Palet
        {
            get { return palet; }
            set { palet = value; }
        }
        public string PartnoDest
        {
            get { return partnoDest; }
            set { partnoDest = value; }
        }
        public string PartnoSer
        {
            get { return partnoSer; }
            set { partnoSer = value; }
        }
        public string PartnoTrm
        {
            get { return partnoTrm; }
            set { partnoTrm = value; }
        }
        public DateTime TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int T1serahID
        {
            get { return t1serahID; }
            set { t1serahID = value; }
        }
        public int T1sItemID
        {
            get { return t1sItemID; }
            set { t1sItemID = value; }
        }
        public DateTime TglSerah
        {
            get { return tglSerah; }
            set { tglSerah = value; }
        }
        public int QtyInSer
        {
            get { return qtyInSer; }
            set { qtyInSer = value; }
        }
        public int ItemIDSer
        {
            get { return itemIDSer; }
            set { itemIDSer = value; }
        }
        public DateTime TglTrm
        {
            get { return tglTrm; }
            set { tglTrm = value; }
        }
        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }
        public string LokasiTrm
        {
            get { return lokasiTrm; }
            set { lokasiTrm = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string Process
        {
            get { return process; }
            set { process = value; }
        }
        public string Groups
        {
            get { return groups ; }
            set { groups = value; }
        }
        public int QtyInTrm
        {
            get { return qtyInTrm; }
            set { qtyInTrm = value; }
        }
        public int QtyOutTrm
        {
            get { return qtyOutTrm; }
            set { qtyOutTrm = value; }
        }
    }
}
