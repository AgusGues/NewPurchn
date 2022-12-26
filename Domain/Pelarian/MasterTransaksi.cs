using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterTransaksi : GRCBaseDomain
    {
        private string pelarianno = string.Empty;
        private int id = 0;
        private int serahID = 0;
        private int destID = 0;
        private int idregu = 0;
        private string regucode = string.Empty;
        private int idtype = 0;
        private string namatype = string.Empty;
        private int idukuran = 0;
        private string ukuran = string.Empty;
        private DateTime tglproduksi = DateTime.Now.Date;
        private DateTime tgltransaksi = DateTime.Now.Date;
        private string kodepelarian = string.Empty;
        private decimal jumlah = 0;
        private int tahun = 0;

        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }
        public int SerahID
        {
            get { return serahID; }
            set { serahID = value; }
        }
        public int DestID
        {
            get { return destID; }
            set { destID = value; }
        }

        public string PelarianNo
        {
            get { return pelarianno; }
            set { pelarianno = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IDRegu
        {
            get { return idregu; }
            set { idregu = value; }

        }

        public string ReguCode
        {
            get { return regucode; }
            set { regucode = value; }
        }

        public int IDType
        {
            get { return idtype; }
            set { idtype = value; }

        }

        public string NamaType
        {
            get { return namatype; }
            set { namatype = value; }
        }

        public int IDUkuran
        {
            get { return idukuran; }
            set { idukuran = value; }

        }

        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }

        public DateTime TglProduksi
        {
            get{ return tglproduksi; }
            set{ tglproduksi = value; }
        }

        public DateTime TglTransaksi
        {
            get { return tgltransaksi; }
            set { tgltransaksi = value; }
        }
        public string KodePelarian
        {
            get{ return kodepelarian; }
            set{ kodepelarian = value; }
        }

        public decimal Jumlah
        {
            get{ return jumlah; }
            set{ jumlah = value; }
        }
    }
}
