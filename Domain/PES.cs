using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PES2016:GRCBaseDomain
    {
        public int DeptID { get; set; }
        public int BagianID { get; set; }
        public string DeptName { get; set; }
        public string BagianName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public decimal KPI { get; set; }
        public decimal BKPI { get; set; }
        public decimal PKPI { get; set; }
        public decimal Task { get; set; }
        public decimal BTask { get; set; }
        public decimal PTask { get; set; }
        public decimal SOP { get; set; }
        public decimal BSOP { get; set; }
        public decimal PSOP { get; set; }
        public decimal DIS { get; set; }
        public decimal BDIS { get; set; }
        public decimal PDIS { get; set; }
        public decimal Total { get; set; }
        public decimal Bobot { get; set; }
        public int PesType { get; set; }
        public int Mulai { get; set; }
        public int TahunMulai { get; set; }
        public int Pembagi { get; set; }
        public string PESName { get; set; }
        public string Nama { get; set; }
        public decimal Semester1 { get; set; }
        public decimal Semester2 { get; set; }
        public decimal BobotSmt1 { get; set; }
        public decimal BobotSmt2 { get; set; }
        #region object bulan
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal Mei { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ags { get; set; }
        public decimal Sep { get; set; }
        public decimal Okt { get; set; }
        public decimal Nop { get; set; }
        public decimal Des { get; set; }
        //Nilai Bulanan
        public decimal JanN { get; set; }
        public decimal FebN { get; set; }
        public decimal MarN { get; set; }
        public decimal AprN { get; set; }
        public decimal MeiN { get; set; }
        public decimal JunN { get; set; }
        public decimal JulN { get; set; }
        public decimal AgsN { get; set; }
        public decimal SepN { get; set; }
        public decimal OktN { get; set; }
        public decimal NopN { get; set; }
        public decimal DesN { get; set; }
        //Bobot pest tiap bulan
        public decimal JanB { get; set; }
        public decimal FebB { get; set; }
        public decimal MarB { get; set; }
        public decimal AprB { get; set; }
        public decimal MeiB { get; set; }
        public decimal JunB { get; set; }
        public decimal JulB { get; set; }
        public decimal AgsB { get; set; }
        public decimal SepB { get; set; }
        public decimal OktB { get; set; }
        public decimal NopB { get; set; }
        public decimal DesB { get; set; }
        public string Semester { get; set; }
        public decimal TotalBobot { get; set; }
        public int Tahun { get; set; }
        public int Penilaian { get; set; }
        public string Rebobot { get; set; }
        public int Urutan { get; set; }
        public int Approval { get; set; }
        public int JmlItemPES { get; set; }
        #endregion
   
    }
    public class PESOrg : PES2016
    {
        public int DeptID { get; set; }
        public int Revisi { get; set; }
        public DateTime MulaiBerlaku { get; set; }
        public string FileName { get; set; }
    }

    public class RekapPesDetail : PES2016
    {
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string Mar { get; set; }
        public string Apr { get; set; }
        public string Mei { get; set; }
        public string Jun { get; set; }
        public string Jul { get; set; }
        public string Ags { get; set; }
        public string Sep { get; set; }
        public string Okt { get; set; }
        public string Nop { get; set; }
        public string Des { get; set; }
        //Nilai Bulanan
        public string JanN { get; set; }
        public string FebN { get; set; }
        public string MarN { get; set; }
        public string AprN { get; set; }
        public string MeiN { get; set; }
        public string JunN { get; set; }
        public string JulN { get; set; }
        public string AgsN { get; set; }
        public string SepN { get; set; }
        public string OktN { get; set; }
        public string NopN { get; set; }
        public string DesN { get; set; }
        //Bobot pest tiap bulan
        public string JanB { get; set; }
        public string FebB { get; set; }
        public string MarB { get; set; }
        public string AprB { get; set; }
        public string MeiB { get; set; }
        public string JunB { get; set; }
        public string JulB { get; set; }
        public string AgsB { get; set; }
        public string SepB { get; set; }
        public string OktB { get; set; }
        public string NopB { get; set; }
        public string DesB { get; set; }
        public int DeptID { get; set; }
        public int BagianID { get; set; }
        public int Penilaian { get; set; }
        public string Rebobot { get; set; }
        public int Urutan { get; set; }
        public int Approval { get; set; }
        public string PESName { get; set; }
        public Decimal Bobot { get; set; }
        public int Tahun { get; set; }
    }
}
