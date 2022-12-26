using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MTC_Zona : GRCBaseDomain
    {
        private int iD = 0;
        private int deptID = 0;
        private int plantID = 0;
        private string zonaName = string.Empty;
        private string zonaCode = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public int PlantID
        {
            get { return plantID; }
            set { plantID = value; }
        }

        public string ZonaName
        {
            get { return zonaName; }
            set { zonaName = value; }
        }
        public string ZonaCode
        {
            get { return zonaCode; }
            set { zonaCode = value; }
        }
    }
}
