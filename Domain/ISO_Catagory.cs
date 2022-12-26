using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_Catagory :GRCBaseDomain
    {
        private int nom = 0;
        private string task = string.Empty;
        private string desk = string.Empty;
        private int bobot = 0;
        private int pestype = 0;
        //private string typebobot = string.Empty;
        //private int deptid = 0;
        //private int bagianid = 0;
        //private int urutan = 0;

        public int Nom { get { return nom; } set { nom = value; } }
        public string Task { get { return task; } set { task = value; } }
        public string Desk { get { return desk; } set { desk = value; } }
        public int Bobots { get { return bobot; } set { bobot = value; } }
        public int PesType { get { return pestype; } set { pestype = value; } }
        public int DeptID { get; set; }
        public int PlantID { get; set; }
        public string Target { get; set; }
        public int CategoryID { get; set; }
        public int SectionID { get; set; }
        public decimal Bobot { get; set; }
        public string TypeBobot { get; set; }
        public int UserID { get; set; }
        public string BagianName { get; set; }
        public string Checking { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
    }
}
