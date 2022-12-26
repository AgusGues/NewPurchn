using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MTC_Project_Rev1 : GRCBaseDomain
    {
        private string lastmodifiedby0 = string.Empty;
        private string lastmodifiedby = string.Empty;
        private string namahead = string.Empty;
        private string namaproject = string.Empty;
        private DateTime fromdate;
        private DateTime todatate;
        private int deptid = 0;
        private int progress = 0;
        private decimal biaya = 0;
        private int idno = 0;
        private int bln = 0;
        private int thn = 0;
        private int count = 0;
        private string statusAproval = string.Empty;
        private int idarea = 0;
        private int verdate = 0;
        private string detailsasaran = string.Empty;
        private string zona = string.Empty;
        private string fromdate2 = string.Empty;
        private string todate2 = string.Empty;
        private int verpm = 0;
        private int veruser = 0;
        private int flag = 0;
        private int apvdireksi = 0;
        private int todept = 0;
        private int todeptid = 0;
        private int noted = 0;

        //penambahan agus 10-05-2022
        private string kondisiSebelum = string.Empty;

        public string KondisiSebelum
        {
            get { return kondisiSebelum; }
            set { kondisiSebelum = value; }
        }

        private string kondisiYangDiharapkan = string.Empty;

        public string KondisiYangDiharapkan
        {
            get { return kondisiYangDiharapkan; }
            set { kondisiYangDiharapkan = value; }
        }
        //penambahan agus 10-05-2022

        public string LastModifiedBy0 { get { return lastmodifiedby0; } set { lastmodifiedby0 = value; } }
        public string LastModifiedBy2 { get { return lastmodifiedby; } set { lastmodifiedby = value; } }
        public string NamaHead { get { return namahead; } set { namahead = value; } }
        public int Noted1 { get { return noted; } set { noted = value; } }
        public int ToDeptID { get { return todeptid; } set { todeptid = value; } }
        public int ToDept { get { return todept; } set { todept = value; } }
        public int ApvDir { get { return apvdireksi; } set { apvdireksi = value; } }
        public int Flag { get { return flag; } set { flag = value; } }
        public int VerUser { get { return veruser; } set { veruser = value; } }
        public int VerPM { get { return verpm; } set { verpm = value; } }
        public int VerDate { get { return verdate; } set { verdate = value; } }        
        public int Count { get { return count; } set { count = value; } }
        public int Tahun { get { return thn; } set { thn = value; } }
        public int Bulan { get { return bln; } set { bln = value; } }
        public int IDno { get { return idno; } set { idno = value; } }
        public int IDarea { get { return idarea; } set { idarea = value; } }
        public int DeptID { get { return deptid; } set { deptid = value; } }
        public int Progress { get { return progress; } set { progress = value; } }

        public string StatusAproval { get { return statusAproval; } set { statusAproval = value; } }
        public string FromDate2 { get { return fromdate2; } set { fromdate2 = value; } }
        public string ToDate2 { get { return todate2; } set { todate2 = value; } }
        public string DetailSasaran { get { return detailsasaran; } set { detailsasaran = value; } }
        public string NamaProject { get { return namaproject; } set { namaproject = value; } }
        public string Zona { get { return zona; } set { zona = value; } }

        public DateTime FromDate { get { return fromdate; } set { fromdate = value; } }
        public DateTime ToDate { get { return todatate; } set { todatate = value; } }        
        public decimal Biaya { get { return biaya; } set { biaya = value; } }

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
        public string AreaName { get; set; }
    }

    public class EstimasiMaterial_Rev1 : MTC_Project_Rev1
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
}
