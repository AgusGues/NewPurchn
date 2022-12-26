using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_AdjustDetail : GRCBaseDomain
    {
        int iD = 0;
        int adjustID = 0;
        int itemID = 0;
        int lokID = 0;
        int qtyIn = 0;
        int apv = 0;
        int qtyOut = 0;
        string partNo = string.Empty;
        string lokasi = string.Empty;
        string adjustNo = string.Empty;
        DateTime adjustDate = DateTime.Now;
        string adjustType = string.Empty;
        string noBA = string.Empty;
        string approval = string.Empty;
        string keterangan = string.Empty;
        private int sA = 0;
        private int destID = 0;
        private int paletID = 0;
        DateTime tglProduksi= DateTime.Now;
        string noPalet = string.Empty;

        public int DestID
        {
            get { return destID; }
            set { destID = value; }
        }
        public int PaletID
        {
            get { return paletID; }
            set { paletID = value; }
        }
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
        public DateTime AdjustDate
        {
            get { return adjustDate; }
            set { adjustDate = value; }
        }
        public string NoPalet
        {
            get { return noPalet; }
            set { noPalet = value; }
        }
        public DateTime TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
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

    public class T1AdjustAll
    {
        public List<T1AdjustDetail> T1AdjustDetail { get; set; }
    }

    public class T1AdjustDetail
    {
        public int ID { get; set; }
        public int AdjustID { get; set; }
        public string AdjustNo { get; set; }
        public string AdjustType { get; set; }
        public int ItemID { get; set; }
        public string LokID { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public int HPP { get; set; }
        public int RowStatus { get; set; }
        public string NoBA { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public int SA { get; set; }
        public int DestID { get; set; }
        public int PaletID { get; set; }
        public string DateProduksi { get; set; }
        public DateTime AdjustDate { get; set; }
        public string DateAdjust
        {
            get { return AdjustDate.ToShortDateString(); }
            set { AdjustDate.ToShortDateString(); }
        }
        public DateTime TglProduksi { get; set; }
        public string ProduksiDate
        {
            get { return TglProduksi.ToShortDateString(); }
            set { TglProduksi.ToShortDateString(); }
        }
        public int DepoID { get; set; }
        public string Partno { get; set; }
        public string Lokasi { get; set; }
        public string Keterangan { get; set; }
        public string Approval { get; set; }
        public int Apv { get; set; }
    }
}
