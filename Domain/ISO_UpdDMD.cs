using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
   public class ISO_UpdDMD  : GRCBaseDomain
    {
       private int IdM = 0;
       private string Id = string.Empty;
       private string noDocument = string.Empty;
       private string docName = string.Empty;
       private string revisiNo = string.Empty;
       private string categoryUPD = string.Empty;
       private string aktif = string.Empty;
       private DateTime tglBerlaku = DateTime.Now.Date;
       private DateTime tglDistribusi = DateTime.Now.Date;
       private DateTime waktu = DateTime.Now.Date; 
       private string depta = string.Empty;
       private string dept = string.Empty;
       private string deptB = string.Empty;
       private string deptM = string.Empty;
       private string type = string.Empty;
       public string filename = string.Empty;
       public Byte[] attachfile { get; set; }

       private string tglBerlakus = string.Empty;
       private string tglBerlakuss = string.Empty;    
       private string deptname = string.Empty;
       private string urutan = string.Empty;
       private int userGroupID = 0;
       private int userid = 0;
       private int rowStatus = 0;
       private string deptCode = string.Empty;
       private int id = 0;
       private int idMaster = 0;
       private int kategory = 0;
       private int dept2 = 0;
       private int no = 0;
       private int categoryID = 0;
       public string namafile = string.Empty;
       private int plantid = 0;
       private int statusshare = 0;
       private int jenisupd = 0;
       public string alasan = string.Empty;
       private int linkID = 0;
       private int unitkerjaid = 0;

       public DateTime TglShare { get; set; }

       public int IDm
       {
           get { return IdM; }
           set { IdM = value; }
       }
       public int UnitKerjaID
       {
           get { return unitkerjaid; }
           set { unitkerjaid = value; }
       }
       public int LinkID
       {
           get { return linkID; }
           set { linkID = value; }
       }
       public string Alasan
       {
           get { return alasan; }
           set { alasan = value; }
       }
       public int JenisUPD
       {
           get { return jenisupd; }
           set { jenisupd = value; }
       }
       public int StatusShare
       {
           get { return statusshare; }
           set { statusshare = value; }
       }
       public int PlantID
       {
           get { return plantid; }
           set { plantid = value; }
       }
       public string TglBerlakuSS
       {
           get { return tglBerlakuss; }
           set { tglBerlakuss = value; }
       }
       public string TglBerlakuS
       {
           get { return tglBerlakus; }
           set { tglBerlakus = value; }
       }
       public string NamaFile
       {
           get { return namafile; }
           set { namafile = value; }
       }
       public int CategoryID
       {
           get { return categoryID; }
           set { categoryID = value; }
       }
       public int No
       {
           get { return no; }
           set { no = value; }
       }
       public int Dept2
       {
           get { return dept2; }
           set { dept2 = value; }
       }
       public int Kategory
       {
           get { return kategory; }
           set { kategory = value; }
       }
       public int IDmaster
       {
           get { return idMaster; }
           set { idMaster = value; }
       }
       public int ID
       {
           get { return id; }
           set { id = value; }
       }
       public string DeptName
       {
           get { return deptname; }
           set { deptname = value; }
       }
       public string DeptCode
       {
           get { return deptCode; }
           set { deptCode = value; }
       }
       public string Urutan
       {
           get { return urutan; }
           set { urutan = value; }
       }
       //public int DeptID
       //{
       //    get { return deptID; }
       //    set { deptID = value; }
       //}
       public int UserGroupID
       {
           get { return userGroupID; }
           set { userGroupID = value; }
       }
       public int UserID
       {
           get { return userid; }
           set { userid = value; }
       }
       public int RowStatus
       {
           get { return rowStatus; }
           set { rowStatus = value; }
       }


       public string FileName
       {
           get { return filename; }
           set { filename = value; }
       }
       public string Type
       {
           get { return type; }
           set { type = value; }
       }
       public string Deptm
       {
           get { return deptM; }
           set { deptM = value; }
       }
       public string DeptID
       {
           get { return dept; }
           set { dept = value; }
       }
       public string DeptA
       {
           get { return depta; }
           set { depta = value; }
       }
       public string Aktif
       {
           get { return aktif; }
           set { aktif = value; }
       }
       public string iD
       {
           get { return Id; }
           set { Id = value; }
       }
       public string CategoryUPD
       {
           get { return categoryUPD; }
           set { categoryUPD = value; }
       }
       public string NoDocument
       {
           get { return noDocument; }
           set { noDocument = value; }
       }
       public string DocName
       {
           get { return docName; }
           set { docName = value; }
       }
       public string RevisiNo
       {
           get { return revisiNo; }
           set { revisiNo = value; }
       }
       public DateTime TglBerlaku
       {
           get { return tglBerlaku; }
           set { tglBerlaku = value; }
       }
       public DateTime TglDistribusi
       {
           get { return tglDistribusi; }
           set { tglDistribusi = value; }
       }
       public DateTime Waktu
       {
           get { return waktu; }
           set { waktu = value; }
       }
       public string Dept
       {
           get { return deptB; }
           set { deptB = value; }
       }

    }
}
