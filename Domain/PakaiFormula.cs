using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PakaiFormula : GRCBaseDomain
    {
        private int iD = 0;
        private int pakaiID = 0;
        private int formulaID = 0;
        private int plantID = 0;
        private int plantGroupID = 0;
        private DateTime tglProduksi = DateTime.Now;
        private decimal jmlMix = 0;
        private string keterangan;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int PakaiID
        {
            get { return pakaiID; }
            set { pakaiID = value; }
        }
        public int FormulaID
        {
            get { return formulaID; }
            set { formulaID = value; }
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
        public DateTime  TglProduksi
        {
            get { return tglProduksi; }
            set { tglProduksi = value; }
        }
        public decimal JmlMix
        {
            get { return jmlMix; }
            set { jmlMix = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan  = value; }
        }
    }
}
