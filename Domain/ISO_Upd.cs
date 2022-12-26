using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_Upd : GRCBaseDomain
    {
        private int headid = 0;
        private string subdept = string.Empty;

        private string deptA = string.Empty;
        private string statusAPV = string.Empty;
        private string idMaster = string.Empty;
        private int id = 0;
        private string UpdNo = string.Empty;      
        private int UpdID = 0;
        private string updName = string.Empty;
        private int deptID = 0;
        private int status = 0;
        private int IdDetail = 0;
        private int Apv = 0;
        public string ApV { get; set; }
        private string pic = string.Empty;
        private string UpdDok = string.Empty;
        private string alasan = string.Empty;
        private string alasan2 = string.Empty;
        private int userID = 0;
        private DateTime tglPengajuan = DateTime.Now.Date;
        private DateTime tglBerlaku = DateTime.Now.Date;
        private string revisiNo = string.Empty;
        private int planID = 0;
        private string lampiran = string.Empty;
        private string lampiranlama = string.Empty;
        private string lampiranbaru = string.Empty;
        private int iso_UserID = 0;
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        private string nodokumen = string.Empty;
        private int jenisDoc = 0;
        private int jenisupd = 0;
        public string Jenisupd { get; set; }
        private int categoryDoc = 0;       
        private int categoryupd = 0;
        public string Categoryupd { get; set; }
        private string Lastmodifiedby = string.Empty;
        private string Lastmodifiedby2 = string.Empty;
        //private int Apv = 0;
        int no = 0;
        private string statusupd = string.Empty;
        private string alasanNo = string.Empty;
        private int idCatUPD = 0;
        private int plantid = 0;
        private int unitkerjaid = 0;
        private string namadokumen = string.Empty;
        private string tglpengajuanstring = string.Empty;
        private int iddmd = 0;

        public string Tanggal2 { get; set; }
        private string catdoc = string.Empty;
        public int UPDid { get; set; }
        public string Alasan { get; set; }
        public string StatusAPV { get; set; }
        public string Alasan2 { get; set; }
        public string IsiDokumen { get; set; }
        public string DokumenTerkainLain { get; set; }
        public string DokumenLama { get; set; }
        public string DokumenBaru { get; set; }       
        //public string NamaDokumen { get; set; }
        public string NoRevisi { get; set; }
        public string Image { get; set; }
        public string UPDImage = string.Empty;
        public string reVno = string.Empty;
        public DateTime tanggal = DateTime.Now.Date;
        public string IDcat = string.Empty;
        public string formNO = string.Empty;
        public int type = 0;
        public string tYpe = string.Empty;
        public int apv1 = 0;
        public int statusapv = 0;
        public int tipeApv = 0;
        public string namafile = string.Empty;
        public string filelama = string.Empty;
        public string jenisdokumen = string.Empty;
        public int rowstatus1 = 0;
        public string filenama = string.Empty;
        public string aktif { get; set; }
        public string permintaan = string.Empty;
        private string kategori = string.Empty;
        private string keterangan = string.Empty;
        private DateTime tglnonaktif = DateTime.Now.Date;




        public int HeadID
        {
            get { return headid; }
            set { headid = value; }
        }
        public string SubDept
        {
            get { return subdept; }
            set { subdept = value; }
        }
        public DateTime TglNonAktif
        {
            get { return tglnonaktif; }
            set { tglnonaktif = value; }
        }
        public int IDDmD
        {
            get { return iddmd; }
            set { iddmd = value; }
        }
        public string TglPengajuanString
        {
            get { return tglpengajuanstring; }
            set { tglpengajuanstring = value; }
        }
        public string NamaDokumen
        {
            get { return namadokumen; }
            set { namadokumen = value; }
        }
        public int UnitKerjaID
        {
            get { return unitkerjaid; }
            set { unitkerjaid = value; }
        }
        public int PlantID
        {
            get { return plantid; }
            set { plantid = value; }
        }
        public int IDCatUPD
        {
            get { return idCatUPD; }
            set { idCatUPD = value; }
        }
        public string AlasanNo
        {
            get { return alasanNo; }
            set { alasanNo = value; }
        }
        public string StatusUPD
        {
            get { return statusupd; }
            set { statusupd = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string Permintaan
        {
            get { return permintaan; }
            set { permintaan = value; }
        }
        public string Kategori
        {
            get { return kategori; }
            set { kategori = value; }
        }
        public string Aktif
        {
            get { return aktif; }
            set { aktif = value; }
        }
        public int RowStatus1
        {
            get { return rowstatus1; }
            set { rowstatus1 = value; }
        }
        public int TipeApv
        {
            get { return tipeApv; }
            set { tipeApv = value; }
        }
        public string JenisDokumen
        {
            get { return jenisdokumen; }
            set { jenisdokumen = value; }
        }
        public string FileLama
        {
            get { return filelama; }
            set { filelama = value; }
        }
        public string FileNama
        {
            get { return filenama; }
            set { filenama = value; }
        }
        public string NamaFile
        {
            get { return namafile; }
            set { namafile = value; }
        }
        public int No
        {
            get { return no; }
            set { no = value; }
        }
        public string LampiranLama
        {
            get { return lampiranlama; }
            set { lampiranlama = value; }
        }
        public string LampiranBaru
        {
            get { return lampiranbaru; }
            set { lampiranbaru = value; }
        }
        public int StatusApv
        {
            get { return statusapv; }
            set { statusapv = value; }
        }
        public int Apv1
        {
            get { return apv1; }
            set { apv1 = value; }
        }
        public string TypE
        {
            get { return tYpe; }
            set { tYpe = value; }
        }
        public string LastModifiedBy
        {
            get { return Lastmodifiedby; }
            set { Lastmodifiedby = value; }
        }
        public string LastModifiedBy2
        {
            get { return Lastmodifiedby2; }
            set { Lastmodifiedby2 = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        public string JenisUPd
        {
            get { return Jenisupd; }
            set { Jenisupd = value; }
        }
        public string FormNO
        {
            get { return formNO; }
            set { formNO = value; }
        }
        public string RevNo
        {
            get { return reVno; }
            set { reVno = value; }
        }
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }
        public string idCategory
        {
            get { return IDcat; }
            set { IDcat = value; }
        }

        public string DeptA
        {
            get { return deptA; }
            set { deptA = value; }
        }
        public string IDmaster
        {
            get { return idMaster; }
            set { idMaster = value; }
        }
        public string CatDoc
        {
            get { return catdoc; }
            set { catdoc = value; }
        }

        public string UpdImage
        {
            get { return UPDImage; }
            set { UPDImage = value; }
        }

        public int CategoryUPD
        {
            get { return categoryupd; }
            set {categoryupd = value; }
        }

        public string CategoryUPd
        {
            get { return Categoryupd; }
            set { Categoryupd = value; }
        }

        public string NoDokumen
        {
            get { return nodokumen; }
            set { nodokumen = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string updNo
        {   get { return UpdNo; }
            set { UpdNo = value; }
        }

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        public int CategoryDoc
        {
            get { return categoryDoc; }
            set { categoryDoc = value; }
        }
        public int JenisDoc
        {
            get { return jenisDoc; }
            set { jenisDoc = value; }
        }
        public int JenisUPD
        {
            get { return jenisupd; }
            set { jenisupd= value; }
        }
        public int updID
        {
            get { return UpdID; }
            set { UpdID = value; }
        }
        public string RevisiNo
        {
            get { return revisiNo; }
            set {revisiNo = value;}
        }
        
        public int Iso_UserID
        {   get { return iso_UserID; }
            set { iso_UserID = value; }
        }

        public int UserID
        {   get { return userID; }
            set { userID = value; }
        }

        public int apv
        {   get { return Apv; }
            set { Apv = value; }
        }
        public string apV
        {
            get { return ApV; }
            set { ApV = value; }
        }
        
        public int idDetail
        {   get { return IdDetail; }
            set { IdDetail = value; }
        }
        
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        public int DeptID
        {   get { return deptID; }
            set { deptID = value; }
        }
        
        public int Status
        {   get { return status; }
            set { status = value; }
        }        

        public string UpdName
        {
            get { return updName; }
            set { updName = value; }
        }
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        
        public string updDok
        {   get { return UpdDok; }
            set { UpdDok = value; }
        }
        public string updAlasan
        {   get { return alasan; }
            set { alasan = value; }
        }
        public DateTime TglPengajuan
        {
            get { return tglPengajuan; }
            set { tglPengajuan = value; }
        }
        public DateTime TglBerlaku
        {
            get { return tglBerlaku; }
            set { tglBerlaku = value; }
        }
        
        public string Lampiran
        {
            get { return lampiran; }
            set { lampiran = value; }
        }       

        public int PlanID
        {
            get { return planID; }
            set { planID = value; }
        }
       
    }
}
