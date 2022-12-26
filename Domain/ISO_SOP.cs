using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_SOP : GRCBaseDomain
    {
        private int usergroupID = 0;
        private int userID = 0;
        private int deptID = 0;
        private int bagianID = 0;
        private int categoryID = 0;
        private decimal bobotNilai = 0;
        private int status = 0;
        private int approval = 0;
        private int depoID = 0;
        private int targetKe = 0;
        private int sopID = 0;
        private int idDetail = 0;
        private int aktip = 0;
        private int app = 0;
        private decimal pointNilai = 0;
        private decimal point = 0;
        private decimal point2 = 0;
        private string pic = string.Empty;
        private string sopNo = string.Empty;
        private string newSop = string.Empty;
        private string ket = string.Empty;
        private string deptName = string.Empty;
        private string bagianName = string.Empty;
        private DateTime tglMulai = DateTime.Now.Date;
        private DateTime tglSelesai = DateTime.MinValue;
        private DateTime tglTarget = DateTime.Now.Date;
        private DateTime tglinput = DateTime.Now.Date;
        private DateTime tglapproved = DateTime.Now.Date;
        private string image = string.Empty;
        private int jumlah = 0;
        private int iso_UserID = 0;
        private int sopScoreID = 0;
        private string ketTargetKe = string.Empty;
        private string description = string.Empty;
        private string kpiNo = string.Empty;
        private int kpiID = 0;
        private int pesType = 0;
        private string newKpi = string.Empty;
        public string SOPName { get; set; }
        public int rowStatus = 0;
        //private string alasanUnApprove = string.Empty;
		
		//Ditambahkan tanggal 08 Oktober 2018 , Oleh Beny
		
        private string namabulan = string.Empty;
        private string no = string.Empty;
        private string bobot = string.Empty;
        private string pencapaiantarget = string.Empty;     
        private string statusapproval = string.Empty;

        public string StatusApproval {get { return statusapproval; } set { statusapproval = value; }}
        public string TargetPencapaian {get { return pencapaiantarget; } set { pencapaiantarget = value; }}
        public string Bobot {get { return bobot; } set { bobot = value; }}
        public string No {get { return no; } set { no = value; }}     
        public string NamaBulan {get { return namabulan; } set { namabulan = value; }}
		//End Tambahan

        public string Criteria { get; set; }
        public string Field { get; set; }
        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }

        public int PesType
        {
            get { return pesType; }
            set { pesType = value; }
        }
        public string KpiNo
        {
            get { return kpiNo; }
            set { kpiNo = value; }
        }
        public string NewKpi
        {
            get { return newKpi; }
            set { newKpi = value; }
        }
        public int KpiID
        {
            get { return kpiID; }
            set { kpiID = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string KetTargetKe
        {
            get { return ketTargetKe; }
            set { ketTargetKe = value; }
        }
        public int SopScoreID
        {
            get { return sopScoreID; }
            set { sopScoreID = value; }
        }
        public int Iso_UserID
        {
            get { return iso_UserID; }
            set { iso_UserID = value; }
        }
        public decimal PointNilai
        {
            get { return pointNilai; }
            set { pointNilai = value; }
        }
        public decimal Point
        {
            get { return point; }
            set { point = value; }
        }
        public decimal Point2
        {
            get { return point2; }
            set { point2 = value; }
        }
        public DateTime TglInput
        {
            get { return tglinput; }
            set { tglinput = value; }
        }
        public DateTime TglApproved
        {
            get { return tglapproved; }
            set { tglapproved = value; }
        }
        public int UserGroupID
        {
            get { return usergroupID; }
            set { usergroupID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public int App
        {
            get { return app; }
            set { app = value; }
        }
        public int Aktip
        {
            get { return aktip; }
            set { aktip = value; }
        }
        public int IdDetail
        {
            get { return idDetail; }
            set { idDetail = value; }
        }
        public int SopID
        {
            get { return sopID; }
            set { sopID = value; }
        }
        public int TargetKe
        {
            get { return targetKe; }
            set { targetKe = value; }
        }
        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int BagianID
        {
            get { return bagianID; }
            set { bagianID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public decimal BobotNilai
        {
            get { return bobotNilai; }
            set { bobotNilai = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Approval
        {
            get { return approval; }
            set { approval = value; }
        }
        public string BagianName
        {
            get { return bagianName; }
            set { bagianName = value; }
        }
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public string SopNo
        {
            get { return sopNo; }
            set { sopNo = value; }
        }
        public string NewSop
        {
            get { return newSop; }
            set { newSop = value; }
        }
        public string Ket
        {
            get { return ket; }
            set { ket = value; }
        }
        public DateTime TglMulai
        {
            get { return tglMulai; }
            set { tglMulai = value; }
        }
        public DateTime TglTarget
        {
            get { return tglTarget; }
            set { tglTarget = value; }
        }
        public DateTime TglSelesai
        {
            get { return tglSelesai; }
            set { tglSelesai = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public int Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }
        //public string AlasanUnApprovee { get; set; }
        public string Targete { get; set; }
        public string Checking { get; set; }
        public string TypeBobot { get; set; }
        public string Periode { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public decimal Score { get; set; }
        public decimal TotalBobot { get; set; }
        public decimal TotalNilai { get; set; }
        public string Pencapaian { get; set; }
        public decimal Nilai { get; set; }
        public int TypePes { get; set; }
        public string Target { get; set; }
        public string Keterangan { get; set; }
        public int SOPID { get; set; }
        public int KPIID { get; set; }
        public int Penilaian { get; set; }
        public int Rebobot { get; set; }
        public string AlasanUnApprove { get; set; }
        public string Penilaianx { get; set; }
        public string Penilaianx1 { get; set; }
        public int KPI { get; set; }
    }
}
