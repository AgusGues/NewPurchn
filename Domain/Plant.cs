using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain

{

    public class Plant : GRCBaseDomain
    {
        private int iD  =0;
	    private string  plantName=string.Empty;
	    private string  plantCode=string.Empty;
	    private string  kodeSemen=string.Empty;
        private string kodeKalsium = string.Empty;
        private string zonaName = string.Empty;


        public string ZonaName
        {
            get { return zonaName; }
            set { zonaName = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string PlantName
        {
            get { return plantName; }
            set { plantName = value; }
        }
        public string PlantCode
        {
            get { return plantCode; }
            set { plantCode = value; }
        }
        public string KodeSemen
        {
            get { return kodeSemen; }
            set { kodeSemen = value; }
        }
       
    }
}
