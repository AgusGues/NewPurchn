using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class DefectDetail
    {
        private int id = 0;
        private int masterID = 0;
        private int defectID = 0;
        private int tPot = 0;
        private decimal qty = 0;
        private int rowStatus = 0;
        private string defectName = string.Empty ;
        private string ukuran = string.Empty;
        public int MasterID
        {
            get { return masterID; }
            set { masterID = value; }
        }
        public int TPot
        {
            get { return tPot; }
            set { tPot = value; }
        }
        public string Ukuran
        {
            get { return ukuran; }
            set { ukuran = value; }
        }
        public string DefectName
        {
            get { return defectName; }
            set { defectName = value; }
        }
        public int DefectID
        {
            get { return defectID; }
            set { defectID = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        


        public decimal Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }

        public int RowStatus
        {
            get
            {
                return rowStatus;
            }
            set
            {
                rowStatus = value;
            }
        }

        
    }
}
