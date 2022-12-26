using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class AssetManagement
    {
        // 30 Juni 2019
        private decimal jumlah_asset = 0;
        public decimal JumlahAsset { get { return jumlah_asset; } set { jumlah_asset = value; } }
        // End

        private int Id =0;
        private int Idn = 0;
        private int rowstatus       = 0;
        private int groupid         = 0;
        private int classid         = 0;
        private string kodegroup    = string.Empty;
        private string namagroup    = string.Empty;
        private string kodeclass    = string.Empty;
        private string namaclass    = string.Empty;
        private string queryfld     = string.Empty;

        // 30 Juni 2019
        private string itemname = string.Empty;
        private int amlokasiid = 0;
      
        private int uomid = 0;
        private int ownerDeptID = 0;
        private int tipeAsset = 0;
        private int assetid = 0;

        public int AssetID { get { return assetid; } set { assetid = value; } }
        public int UomID { get { return uomid; } set { uomid = value; } }
        public int OwnerDeptID { get { return ownerDeptID; } set { ownerDeptID = value; } }
        public int TipeAsset { get { return tipeAsset; } set { tipeAsset = value; } }      

        public string ItemName { get { return itemname; } set { itemname = value; } }
        public int AMLokasiID { get { return amlokasiid; } set { amlokasiid = value; } }
        // End

        public int ID { get { return Id; } set { Id = value; } }
        public int IDn { get { return Idn; } set { Idn = value; } }
        public string KodeGroup { get { return kodegroup; } set { kodegroup = value; } }
        public string NamaGroup { get { return namagroup; } set { namagroup = value; } }
        public int RowStatus { get { return rowstatus; } set { rowstatus = value; } }
        public int GroupID { get { return groupid; } set { groupid = value; } }
        public int ClassID { get { return classid; } set { classid = value; } }
        public string KodeClass { get { return kodeclass; } set { kodeclass = value; } }
        public string NamaClass { get { return namaclass; } set { namaclass = value; } }
        public string QueryFld { get { return queryfld; } set { queryfld = value; } }
        public decimal NilaiPerolehan { get; set; }
        /**
         * for data asset
         */
        private int picdept         = 0;
        private string picperson = string.Empty;
        private string kodeasset=string.Empty;
        private string itemkode = string.Empty;
        private string deskripsi = string.Empty;
        private int methode = 0;
        private decimal nilai_asset = 0;
        private int umur_asset = 0;
        private int mfg_year = DateTime.Now.Year;
        private int subclassid = 0;
        private int lokasiid = 0;
        private int plantid = 0;
        private string kodelokasi = string.Empty;
        private string namalokasi = string.Empty;
        private string kodesubclass = string.Empty;
        private string namasubclass = string.Empty;
        private DateTime tgl_asset;// = DateTime.Now;
        private DateTime tgl_susut;// = DateTime.Now;
        private DateTime mfg_date ;//= DateTime.Now;
        private string createdby = string.Empty;

        public string CreatedBy { get { return createdby; } set { createdby = value; } }
        public string KodeAsset { get { return kodeasset; } set { kodeasset = value; } }
        public string ItemKode { get { return itemkode; } set { itemkode = value; } }
        public string Deskripsi { get { return deskripsi; } set { deskripsi = value; } }
        public string KodeSubClass { get { return kodesubclass; } set { kodesubclass = value; } }
        public string NamaSubClass { get { return namasubclass; } set { namasubclass = value; } }
        public string KodeLokasi { get { return kodelokasi; } set { kodelokasi = value; } }
        public string NamaLokasi { get { return namalokasi; } set { namalokasi = value; } }
        public int MethodDep { get { return methode; } set { methode = value; } }
        public decimal NilaiAsset { get { return nilai_asset; } set { nilai_asset = value; } }
        public int UmurAsset { get { return umur_asset; } set { umur_asset = value; } }
        public int MfgYear { get { return mfg_year; } set { mfg_year = value; } }
        public int LokasiID { get { return lokasiid; } set { lokasiid = value; } }
        public int SubClassID { get { return subclassid; } set { subclassid = value; } }
        public DateTime TglAsset { get { return tgl_asset; } set { tgl_asset = value; } }
        public DateTime TglSusut { get { return tgl_susut; } set { tgl_susut = value; } }
        public DateTime MfgDate { get { return mfg_date; } set { mfg_date = value; } }
        public int PicDept { get { return picdept; } set { picdept = value; } }
        public string PicPerson { get { return picperson; } set { picperson = value; } }
        public int PlantID { get { return plantid; } set { plantid = value; } }

        /**
         * variable for mutasi asset
         */
        private DateTime tglmutasi;
        private DateTime tglefektif;
        private string mutasiasal = string.Empty;
        private string mutasitujuan = string.Empty;
        private string mutasireson = string.Empty;
        private string jenismutasi = string.Empty;
        private string tanggal = string.Empty;
        private string tanggal2 = string.Empty;
        /**
         * variable kodeasset dan ID sudah di declare di for data asset
         */

        public DateTime TglMutasi { get { return tglmutasi; } set { tglmutasi = value; } }
        public DateTime TglEfektif { get { return tglefektif; } set { tglefektif = value; } }
        public string TglMutasi2 { get { return tanggal; } set { tanggal = value; } }
        public string TglEfektif2 { get { return tanggal2; } set { tanggal2 = value; } }
        public string MutasiAsal { get { return mutasiasal; } set { mutasiasal = value; } }
        public string MutasiTujuan { get { return mutasitujuan; } set { mutasitujuan = value; } }
        public string MutasiReson { get { return mutasireson; } set { mutasireson = value; } }
        public string JenisMutasi { get { return jenismutasi; } set { jenismutasi = value; } }

    }
    public class Disposal : GRCBaseDomain
    {
        public string BANumber { get; set; }
        public string AdjustNo { get; set; }
        public DateTime TanggalBA { get; set; }
        public string AlasanDispoal { get; set; }
        public string AlasanDisposal { get; set; }
        public int DeptID { get; set; }
        public int AssetID { get; set; }
        public int LokasiID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public int AdjustID { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public string DeptName { get; set; }
        public string Lokasi { get; set; }
        public string PIC { get; set; }
    }
}
