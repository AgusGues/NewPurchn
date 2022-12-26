using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BM_Destacking : GRCBaseDomain
    {
        private int plantID=0 ;
        private int plantGroupID=0 ;
        private string  plantGroup = string.Empty ;
        private int formulaID=0 ;
        private string formula = string.Empty;
        private int lokasiID=0 ;
        private string lokasi = string.Empty;
        private int paletID=0 ;
        private string palet = string.Empty;
        private int itemID=0 ;
        private string partno = string.Empty;
        private DateTime tglProduksi = DateTime.Now.Date;
        private int qty = 0;
        private string status = string.Empty;
        private string id_dstk = string.Empty;
        private decimal hPP = 0;
        private int shift= 0;
        
        public int Shift
        {
            get { return shift; }
            set { shift = value; }
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
            get { return partno ; }
            set { partno = value; }
        }
        public DateTime  TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
        }
        public int Qty
        {
            get { return qty ; }
            set { qty = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Id_dstk
        {
            get { return id_dstk ; }
            set { id_dstk = value; }
        }
        public decimal  HPP
        {
            get { return hPP ; }
            set { hPP = value; }
        }
    }
}
