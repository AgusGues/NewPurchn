using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Scrub : GRCBaseDomain
    {
        public string filename { get; set; }
        public string Lampiran { get; set; }
        public int ScrubID { get; set; }

        private int typescrab = 0;
        private int typeinput = 0;
        private decimal jumlah = 0;
        private decimal m3 = 0;
        private decimal kg = 0;
        private string createdby = string.Empty;
        private string keterangan = string.Empty;
        private decimal totalBerat = 0;
        private decimal totalM3 = 0;
        private string tanggal = string.Empty;
        private string jenisScrab = string.Empty;
        private string satuanBerat = string.Empty;
        private int id = 0;
        private int nom = 0;
        private DateTime tglInput = DateTime.Now.Date;
        private int totalJumlah = 0;
        private string tangglKedua = string.Empty;
        private int unitKerjaID = 0;
        private int tahun = 0;
        private string statusApprovalSemua = string.Empty;

        public string StatusApprovalSemua
        {
            get { return statusApprovalSemua; }
            set { statusApprovalSemua = value; }
        }


        private int level = 0;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }


        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }
        private int bulan = 0;

        public int Bulan
        {
            get { return bulan; }
            set { bulan = value; }
        }

        

        public int UnitKerjaID
        {
            get { return unitKerjaID; }
            set { unitKerjaID = value; }
        }

        public string TangglKedua
        {
            get { return tangglKedua; }
            set { tangglKedua = value; }
        }
        private string jenisScrabkedua = string.Empty;

        public string JenisScrabkedua
        {
            get { return jenisScrabkedua; }
            set { jenisScrabkedua = value; }
        }
        

        private string palet = string.Empty;

        public string Palet
        {
            get { return palet; }
            set { palet = value; }
        }
        private string bin = string.Empty;

        public string Bin
        {
            get { return bin; }
            set { bin = value; }
        }
        private int paletJumlah = 0;

        public int PaletJumlah
        {
            get { return paletJumlah; }
            set { paletJumlah = value; }
        }
        private int binJumlah = 0;

        public int BinJumlah
        {
            get { return binJumlah; }
            set { binJumlah = value; }
        }
        private decimal m3Palet = 0;

        public decimal M3Palet
        {
            get { return m3Palet; }
            set { m3Palet = value; }
        }
        private decimal m3Bin = 0;

        public decimal M3Bin
        {
            get { return m3Bin; }
            set { m3Bin = value; }
        }
        private decimal beratPalet = 0;

        public decimal BeratPalet
        {
            get { return beratPalet; }
            set { beratPalet = value; }
        }
        private decimal beratBin = 0;

        public decimal BeratBin
        {
            get { return beratBin; }
            set { beratBin = value; }
        }

        private string keteranganPalet = string.Empty;

        public string KeteranganPalet
        {
            get { return keteranganPalet; }
            set { keteranganPalet = value; }
        }
        private string keteranganBin = string.Empty;

        public string KeteranganBin
        {
            get { return keteranganBin; }
            set { keteranganBin = value; }
        }

        public int TotalJumlah
        {
            get { return totalJumlah; }
            set { totalJumlah = value; }
        }

        public DateTime TglInput
        {
            get { return tglInput; }
            set { tglInput = value; }
        }


        public int Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string SatuanBerat
        {
            get { return satuanBerat; }
            set { satuanBerat = value; }
        }

        public string JenisScrab
        {
            get { return jenisScrab; }
            set { jenisScrab = value; }
        }

        public string Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }

        public decimal TotalBerat
        {
            get { return totalBerat; }
            set { totalBerat = value; }
        }
       

        public decimal TotalM3
        {
            get { return totalM3; }
            set { totalM3 = value; }
        }

        public int Typescrab
        {
            get { return typescrab; }
            set { typescrab = value; }
        }

        
        public int Typeinput
        {
            get { return typeinput; }
            set { typeinput = value; }
        }
        
        public decimal Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }

        public decimal M3
        {
            get { return m3; }
            set { m3 = value; }
        }

        public decimal Kg
        {
            get { return kg; }
            set { kg = value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }

        public string Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }

    }
}
