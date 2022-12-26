using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Agen : GRCBaseDomain
    {
        private string nama = string.Empty;
        private string alamat = string.Empty;
        private string nohp = string.Empty;
        private string norekening = string.Empty;
        private int supppurchid = 0;
        private string code = string.Empty;
        private string kabupaten = string.Empty;
        private int areakirim = 0;

        private int agenid = 0;
        private string plantid = string.Empty;
        private int supplierid = 0;
        private string namaagen = string.Empty;
        private string namasupplier = string.Empty;

        public string Nama { 
            get { return nama; } 
            set { nama = value; } 
        }
        public string Alamat
        {
            get { return alamat; }
            set { alamat = value; }
        }
        public string NoHP
        {
            get { return nohp; }
            set { nohp = value; }
        }
        public string NoRekening
        {
            get { return norekening; }
            set { norekening = value; }
        }
        public int SuppPurchID
        {
            get { return supppurchid; }
            set { supppurchid = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Kabupaten
        {
            get { return kabupaten; }
            set { kabupaten = value; }
        }
        public int AreaKirim
        {
            get { return areakirim; }
            set { areakirim = value; }
        }
        
        public int AgenID
        {
            get { return agenid; }
            set { agenid = value; }
        }
        public string PlantID
        {
            get { return plantid; }
            set { plantid = value; }
        }
        public int SupplierID
        {
            get { return supplierid; }
            set { supplierid = value; }
        }
        public string NamaAgen
        {
            get { return namaagen; }
            set { namaagen = value; }
        }
        public string NamaSupplier
        {
            get { return namasupplier; }
            set { namasupplier = value; }
        }
    }
}
