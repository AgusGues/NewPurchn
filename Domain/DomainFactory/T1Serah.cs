using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class T1Serah : GRCBaseDomain
    {
        private int iD = 0;
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
        private string lokasi = string.Empty;
        private int paletID = 0;
        private string palet = string.Empty;
        private int itemID = 0;
        private string partno = string.Empty;
        private DateTime tglProduksi = DateTime.Now.Date;
        private DateTime tglJemur = DateTime.Now.Date;
        private int qtyIn = 0;
        private int qtyOut = 0;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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
        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
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
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string Partno
        {
            get { return partno; }
            set { partno = value; }
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
}
