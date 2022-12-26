using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_Project : GRCBaseDomain
    {
        private string namaproject = string.Empty;
        private DateTime fromdate;
        private DateTime todatate;
        private int deptid = 0;
        private int progress = 0;
        private decimal biaya = 0;
        private string detailsasaran = string.Empty;
        private string zona = string.Empty;
        private int todept = 0;
        private string namahead = string.Empty;

        public string NamaProject { get { return namaproject; } set { namaproject = value; } }

        public DateTime FromDate { get { return fromdate; } set { fromdate = value; } }
        public DateTime ToDate { get { return todatate; } set { todatate = value; } }
        public int DeptID { get { return deptid; } set { deptid = value; } }
        public int ToDept { get { return todept; } set { todept = value; } }
        public int Progress { get { return progress; } set { progress = value; } }
        public decimal Biaya { get { return biaya; } set { biaya = value; } }
        public string NamaHead { get { return namahead; } set { namahead = value; } }

        //for project pakai detail
        public virtual int ItemID { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ItemName { get; set; }
        public virtual string UomCode { get; set; }
        public virtual decimal Jumlah { get; set; }
        public virtual decimal AvgPrice { get; set; }
        public virtual decimal Harga { get; set; }
        public virtual DateTime Tanggal { get; set; }
        //for project baru
        public virtual string GroupName { set; get; }
        public virtual int GroupID { get; set; }
        public virtual int ProjectID { get; set; }
        public virtual string SubProjectName { get; set; }
        public virtual int ProdLine { set; get; }
        public virtual DateTime ProjectDate { set; get; }
        public virtual DateTime FinishDate { set; get; }
        public virtual string DeptName { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int UOMID { get; set; }

        public int Approval { get; set; }
        public int Status { get; set; }
        public string Sasaran { get; set; }
        public string Nomor { get; set; }
        public string AreaImprove { get; set; }
        public string Statuse { get; set; }
        public string AktualFinish { get; set; }

        public string Zona { get { return zona; } set { zona = value; } }
        public string DetailSasaran { get { return detailsasaran; } set { detailsasaran = value; } }
    }
    public class EstimasiMaterial : MTC_Project
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UomCode { get; set; }
        public int ItemTypeID { get; set; }
        public DateTime Schedule { get; set; }
        public decimal QtyAktual { get; set; }
        public decimal QtyPlanning { get; set; }
        public decimal PriceAktual { get; set; }
        public decimal PricePlanning { get; set; }
        public decimal TotalHarga { get; set; }
        public decimal Selisih { get; set; }
    }

    public class ProjectLevelApp
    {
        public List<List_Project> ListProject { get; set; }
        public int LevelApp { get; set; }
        public int MaxBiaya { get; set; }
        public int UserApv { get; set; }
        public int UserDeptID { get; set; }
        public int MinApp { get; set; }
    }
    public class List_Project
    {
        public int ProjectID { get; set; }
        public int ID { get; set; }
        public string Nomor { get; set; }
        public string DeptName { get; set; }
        public string NamaGroup { get; set; }
        public string ProjectName { get; set; }
        public int Quantity { get; set; }
        public string UomCode { get; set; }
        public string Sasaran { get; set; }
        public string NamaHead { get; set; }
        public string AreaImprove { get; set; }
        public string DetailSasaran { get; set; }
        public int Biaya { get; set; }
        public string Zona { get; set; }
        public string LastModifiedBy2 { get; set; }
        public string LastModifiedBy {
            get
            {
                if (LastModifiedBy2 == null)
                {
                    LastModifiedBy2 = "";
                }
                return LastModifiedBy2;
            }}
        public int DeptID { get; set; }
        public int ToDeptID { get; set; }
        public int Approval { get; set; }
        public int Status { get; set; }
        public int RowStatus { get; set; }
        public int VerPM { get; set; }
        public int VerDate { get; set; }
        public int VerUser { get; set; }
        public int ProdLine { get; set; }
        public int Noted1 { get; set; }
        public int ApvDireksi { get; set; }
        public int Flag { get; set; }
        public DateTime FromDate2 { get; set; }
        public DateTime ToDate { get; set; }
        public string FinishDate { get; set; }
        public string Tanggal
        {
            get { return FromDate2.ToString(); }
            set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", FromDate2); } }
        }
        public string Target
        {
            get { return ToDate.ToString(); }
            set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", ToDate); } }
        }
        
    }

    public class MaterialEstimasi
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UomCode { get; set; }
        public int Jumlah { get; set; }
        public int Harga { get; set; }
        public DateTime Schedule { get; set; }
        public string SchedulePakai
        {
            get { return Schedule.ToString(); }
            set { { String.Format("{0:dd-MM-yyyy H:mm:ss}", Schedule); } }
        }
    }
}
