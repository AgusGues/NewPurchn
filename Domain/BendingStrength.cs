using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BendingStrength : GRCBaseDomain
    {
        private decimal actual = 0;

        public decimal Actual
        {
            get { return actual; }
            set { actual = value; }
        }

        private string keterangan = string.Empty;

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        
        //Production testing report
        private int chek = 0;

        public int Chek
        {
            get { return chek; }
            set { chek = value; }
        }

        private string formulaName = string.Empty;

        public string FormulaName
        {
            get { return formulaName; }
            set { formulaName = value; }
        }

        private string jenis = string.Empty;

        public string Jenis
        {
            get { return jenis; }
            set { jenis = value; }
        }
        private string groupReport = string.Empty;

        public string GroupReport
        {
            get { return groupReport; }
            set { groupReport = value; }
        }


        private string pressing = string.Empty;

        public string Pressing
        {
            get { return pressing; }
            set { pressing = value; }
        }

        private int line = 0;

        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        private string kode = string.Empty;

        public string Kode
        {
            get { return kode; }
            set { kode = value; }
        }

        private string group = string.Empty;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        private int formulaID = 0;

        public int FormulaID
        {
            get { return formulaID; }
            set { formulaID = value; }
        }

        private DateTime prodDate = DateTime.Now.Date;

        public DateTime ProdDate
        {
            get { return prodDate; }
            set { prodDate = value; }
        }

        private string formula = string.Empty;

        public string Formula
        {
            get { return formula; }
            set { formula = value; }
        }


        private string groupProduksi = string.Empty;

        public string GroupProduksi
        {
            get { return groupProduksi; }
            set { groupProduksi = value; }
        }

        private string jenisProduksi = string.Empty;

        public string JenisProduksi
        {
            get { return jenisProduksi; }
            set { jenisProduksi = value; }
        }

        private decimal thicknessC = 0;

        public decimal ThicknessC
        {
            get { return thicknessC; }
            set { thicknessC = value; }
        }

        private decimal thicknessL = 0;

        public decimal ThicknessL
        {
            get { return thicknessL; }
            set { thicknessL = value; }
        }
        private decimal thicknessCL = 0;

        public decimal ThicknessCL
        {
            get { return thicknessCL; }
            set { thicknessCL = value; }
        }

        private decimal peakLoadC = 0;

        public decimal PeakLoadC
        {
            get { return peakLoadC; }
            set { peakLoadC = value; }
        }

        private decimal peakLoadL = 0;

        public decimal PeakLoadL
        {
            get { return peakLoadL; }
            set { peakLoadL = value; }
        }
        private decimal peakLoadCL = 0;

        public decimal PeakLoadCL
        {
            get { return peakLoadCL; }
            set { peakLoadCL = value; }
        }

        private decimal peakElongationC = 0;

        public decimal PeakElongationC
        {
            get { return peakElongationC; }
            set { peakElongationC = value; }
        }
        private decimal peakElongationL = 0;

        public decimal PeakElongationL
        {
            get { return peakElongationL; }
            set { peakElongationL = value; }
        }
        private decimal peakElongationCL = 0;

        public decimal PeakElongationCL
        {
            get { return peakElongationCL; }
            set { peakElongationCL = value; }
        }

        private decimal bendingStrengthC = 0;

        public decimal BendingStrengthC
        {
            get { return bendingStrengthC; }
            set { bendingStrengthC = value; }
        }
        private decimal bendingStrengthL = 0;

        public decimal BendingStrengthL
        {
            get { return bendingStrengthL; }
            set { bendingStrengthL = value; }
        }
        private decimal bendingStrengthCL = 0;

        public decimal BendingStrengthCL
        {
            get { return bendingStrengthCL; }
            set { bendingStrengthCL = value; }
        }

        private decimal areaUnderCurveC = 0;

        public decimal AreaUnderCurveC
        {
            get { return areaUnderCurveC; }
            set { areaUnderCurveC = value; }
        }
        private decimal areaUnderCurveL = 0;

        public decimal AreaUnderCurveL
        {
            get { return areaUnderCurveL; }
            set { areaUnderCurveL = value; }
        }
        private decimal areaUnderCurveCL = 0;

        public decimal AreaUnderCurveCL
        {
            get { return areaUnderCurveCL; }
            set { areaUnderCurveCL = value; }
        }

        //Rountine Tes Report
        private int idProd = 0;

        public int IdProd
        {
            get { return idProd; }
            set { idProd = value; }
        }

        private decimal bK = 0;

        public decimal BK
        {
            get { return bK; }
            set { bK = value; }
        }
        private decimal t = 0;

        public decimal T
        {
            get { return t; }
            set { t = value; }
        }
        private decimal l = 0;

        public decimal L
        {
            get { return l; }
            set { l = value; }
        }
        private decimal p = 0;

        public decimal P
        {
            get { return p; }
            set { p = value; }
        }

        private decimal v = 0;

        public decimal V
        {
            get { return v; }
            set { v = value; }
        }

        private decimal denisty = 0;

        public decimal Denisty
        {
            get { return denisty; }
            set { denisty = value; }
        }

        private decimal bA = 0;

        public decimal BA
        {
            get { return bA; }
            set { bA = value; }
        }
        private decimal bB = 0;

        public decimal BB
        {
            get { return bB; }
            set { bB = value; }
        }

        private decimal wC = 0;

        public decimal WC
        {
            get { return wC; }
            set { wC = value; }
        }

        private decimal wA = 0;

        public decimal WA
        {
            get { return wA; }
            set { wA = value; }
        }

        private decimal bK2 = 0;

        public decimal BK2
        {
            get { return bK2; }
            set { bK2 = value; }
        }
        private decimal lBC = 0;

        public decimal LBC
        {
            get { return lBC; }
            set { lBC = value; }
        }
        private decimal lBL = 0;

        public decimal LBL
        {
            get { return lBL; }
            set { lBL = value; }
        }
        private decimal lKC = 0;

        public decimal LKC
        {
            get { return lKC; }
            set { lKC = value; }
        }
        private decimal lKL = 0;

        public decimal LKL
        {
            get { return lKL; }
            set { lKL = value; }
        }

        private decimal dimentioC = 0;

        public decimal DimentioC
        {
            get { return dimentioC; }
            set { dimentioC = value; }
        }

        private decimal dimentioL = 0;

        public decimal DimentioL
        {
            get { return dimentioL; }
            set { dimentioL = value; }
        }

        private string createBy = string.Empty;

        public string CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        }


        private int nom = 0;

        public int Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private decimal bK3 = 0;

        public decimal BK3
        {
            get { return bK3; }
            set { bK3 = value; }
        }

        private string tanggal = string.Empty;

        public string Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }

    }
}
