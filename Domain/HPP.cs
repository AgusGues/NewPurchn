using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HPP : GRCBaseDomain
    {
        private string jenisproduk = string.Empty;
        private string formula = string.Empty;
        private int lembar = 0;
        private decimal volume = 0;
        private decimal hargabb = 0;
        private decimal hargabp = 0;
        private decimal fabrikasi = 0;
        private decimal totalprod = 0;
        private int tahun = 0;
        private string bulan = string.Empty;
        private decimal prosentase = 0;
        private decimal sumtotalpros = 0;
        private decimal totalhpp=0;
        private decimal hpplbr=0;

        public string JenisProduk { set { jenisproduk = value; } get { return jenisproduk; } }
        public string Formula { set { formula = value; } get { return formula; } }
        public int Lembar { set { lembar = value; } get { return lembar; } }
        public decimal Volume { set { volume = value; } get { return volume; } }
        public decimal HargaBB { set { hargabb = value; } get { return hargabb; } }
        public decimal HargaBP { set { hargabp = value; } get { return hargabp; } }
        public decimal Fabrikasi { set { fabrikasi = value; } get { return fabrikasi; } }
        public decimal TotalProd { set { totalprod = value; } get { return totalprod; } }
        public int Tahun { set { tahun = value; } get { return tahun; } }
        public string Bulan { set { bulan = value; } get { return bulan; } }
        public decimal Prosen { set { prosentase = value; } get { return prosentase; } }
        public decimal TotalVolume { set { sumtotalpros = value; } get { return sumtotalpros; } }
        public decimal TotalHPP { set { totalhpp = value; } get { return totalhpp; } }
        public decimal Hpp { set { hpplbr = value; } get { return hpplbr; } }

    }
}
