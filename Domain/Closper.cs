using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
   public class Closper:GRCBaseDomain
    {
       private int tahun;
       private int bulan;
       private string modul = string.Empty;
       private string nbulan = string.Empty;
       private string inventory = string.Empty;
       private string produksi = string.Empty;

       public int Tahun { set { tahun = value; } get { return tahun; } }
       public int Bulan { set { bulan = value; } get { return bulan; } }
       public string ModulName { set { modul = value; } get { return modul; } }
       public string nBulan { set { nbulan = value; } get { return nbulan; } }

       public string Inventory { set { inventory = value; } get { return inventory; } }
       public string Produksi { set { produksi = value; } get { return produksi; } }
    }
}
