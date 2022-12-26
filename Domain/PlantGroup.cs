using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PlantGroup : GRCBaseDomain
    {
        private int iD = 0;
        private int plantID = 0;
        private string group = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public int PlantID
        {
            get { return plantID; }
            set { plantID = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public class BM_PlantGroup
        {
            public int ID { get; set; }
            public int PlantID { get; set; }
            public string Group { get; set; }
        }

    }
}
