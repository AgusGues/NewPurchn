using System;

namespace Domain
{
    public class BM_Destacking : GRCBaseDomain
    {
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
        private DateTime drJam = DateTime.Now.Date;
        private DateTime sdJam = DateTime.Now.Date;
        private int qty = 0;
        private string status = string.Empty;
        private string id_dstk = string.Empty;
        private string plantName = string.Empty;
        private decimal hPP = 0;
        private int shift = 0;

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

        public string PlantName
        {
            get { return plantName; }
            set { plantName = value; }
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

        public DateTime DrJam
        {
            get { return drJam; }
            set { drJam = value; }
        }

        public DateTime SdJam
        {
            get { return sdJam; }
            set { sdJam = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Id_dstk
        {
            get { return id_dstk; }
            set { id_dstk = value; }
        }

        public decimal HPP
        {
            get { return hPP; }
            set { hPP = value; }
        }

        public class Destacking
        {
            public int ID { get; set; }
            public DateTime TglProduksi { get; set; }

            public string Produksi
            {
                get { return TglProduksi.ToString(); }
                set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", TglProduksi); } }
            }

            public string PartNo { get; set; }
            public string FormulaCode { get; set; }
            public int Qty { get; set; }
            public string Status { get; set; }
            public DateTime drJam { get; set; }
            public DateTime sdJam { get; set; }

            public string DariJam
            {
                get { return drJam.ToString(); }
                set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", drJam); } }
            }

            public string SampaiJam
            {
                get { return sdJam.ToString(); }
                set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", sdJam); } }
            }
        }


        public class BMDestacking
        {
            public int PlantID { get; set; }
            public int PlantGroupID { get; set; }
            public int FormulaID { get; set; }
            public int LokasiID { get; set; }
            public int Qty { get; set; }
            public int PaletID { get; set; }
            public int ItemID { get; set; }
            public string TglProduksi { get; set; }
            public string Id_dstk { get; set; }
            public string CreatedBy { get; set; }
            public int Shift { get; set; }
            public string DrJam { get; set; }
            public string SdJam { get; set; }
            public int DepoID { get; set; }
        }
    }
}