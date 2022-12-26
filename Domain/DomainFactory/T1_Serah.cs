using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T1_Serah : GRCBaseDomain
    {
        //private int iD = 0;
        private int jemurID = 0;
        private int rakID = 0;
        private string rak = string.Empty;
        private int destID = 0;
        private int plantID = 0;
        private int plantGroupID = 0;
        private string plantGroup = string.Empty;
        private int formulaID = 0;
        private string formula = string.Empty;
        private int lokasiID = 0;
        private string lokasiDest = string.Empty;
        private string lokasiSer = string.Empty;
        private int paletID = 0;
        private string palet = string.Empty;
        private int itemIDDest = 0;
        private int itemIDSer = 0;
        private string partnoDest = string.Empty;
        private string partnoSer = string.Empty;
        private DateTime tglProduksi = DateTime.Now.Date;
        private DateTime tglJemur = DateTime.Now.Date;
        private DateTime tglSerah = DateTime.Now.Date;
        private int qtyIn = 0;
        private int qtyOut = 0;
        private decimal hPP = 0;
        private string sFrom= string.Empty;
        private decimal tebal = 0;
        private int lebar = 0;
        private int panjang = 0;
        private string ukuran = string.Empty;
        private string oven = string.Empty;

        public string Oven
        {
            get { return oven; }
            set { oven = value; }
        }
        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }
        public decimal HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }

        //public int ID
        //{
        //    get { return iD; }
        //    set { iD = value; }
        //}
        public decimal Tebal
        {
            get { return tebal; }
            set { tebal = value; }
        }
        public int Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }
        public int Panjang
        {
            get { return panjang; }
            set { panjang = value; }
        }
        public int JemurID
        {
            get { return jemurID; }
            set { jemurID = value; }
        }

        public int RakID
        {
            get { return rakID; }
            set { rakID = value; }
        }
        public string Rak
        {
            get { return rak; }
            set { rak = value; }
        }
        public string SFrom
        {
            get { return sFrom; }
            set { sFrom = value; }
        }
        public int DestID
        {
            get { return destID; }
            set { destID = value; }
        }
        public int PlantID
        {
            get { return plantID; }
            set { plantID = value; }
        }

        public int PlantGroupID
        {
            get { return plantGroupID; }
            set { plantGroupID = value; }
        }
        public string PlantGroup
        {
            get { return plantGroup; }
            set { plantGroup = value; }
        }
        public int FormulaID
        {
            get { return formulaID; }
            set { formulaID = value; }
        }
        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }

        public int LokasiID
        {
            get { return lokasiID; }
            set { lokasiID = value; }
        }

        public string LokasiDest
        {
            get { return lokasiDest; }
            set { lokasiDest = value; }
        }

        public string LokasiSer
        {
            get { return lokasiSer; }
            set { lokasiSer = value; }
        }

        public int PaletID
        {
            get { return paletID; }
            set { paletID = value; }
        }
        public string Palet
        {
            get { return palet; }
            set { palet = value; }
        }
        public int ItemIDDest
        {
            get { return itemIDDest; }
            set { itemIDDest = value; }
        }
        public int ItemIDSer
        {
            get { return itemIDSer; }
            set { itemIDSer = value; }
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

        public DateTime TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
        }
        public DateTime TglJemur
        {
            get { return tglJemur; }
            set { tglJemur = value; }
        }

        public DateTime TglSerah
        {
            get { return tglSerah; }
            set { tglSerah = value; }
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

    }

    public class RekapSerah
    {
        public List<t1Serah> tambah { get; set; }
    }

    public class RekapLari
    {
        public List<t1Serah> Lari { get; set; }
    }

    public class RekapTransit
    {
        public List<t1Serah> Transit { get; set; }
    }

    public class t1Serah
    {
        public int ID { get; set; }
        public int DestID { get; set; }
        public string PartnoSer { get; set; }
        public string PartnoDest { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public int Tebal { get; set; }
        public int Panjang { get; set; }
        public int Lebar { get; set; }
        public string LokasiSer { get; set; }
        public int ItemIDDest { get; set; }
        public int HPP { get; set; }
        public int JemurID { get; set; }
        public string TglSerah { get; set; }
        public string SFrom { get; set; }
        public int Oven { get; set; }
        public int ItemIDSer { get; set; }
        public string CreatedBy { get; set; }
        public string LokasiID { get; set; }
        public DateTime TglProduksi { get; set; }
        public string Produksi
        {
            get { return TglProduksi.ToShortDateString(); }
            set { TglProduksi.ToShortDateString(); }
        }
        public int Parameter { get; set; }
        public int DepoID { get; set; }
    }
}
