using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Printers : GRCBaseDomain
    {
        private int depoID =0;
        private string location=string.Empty ;
        private string printerNmae = string.Empty;
        private int rawKind = 0;
        public int UserID { get; set; }
        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public int RawKind
        {
            get { return rawKind; }
            set { rawKind = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string PrinterName
        {
           get { return printerNmae; }
            set { printerNmae = value; }
        }
    }
}
