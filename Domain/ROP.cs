using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ROP : GRCBaseDomain
    {
        private int iD=0 ;
	    private int itemID=0 ;
        private decimal  pakaiQty = 0;
	    private decimal sPPQty =0 ;
	    private int userID =0 ;
        private int sPPID = 0;
        private DateTime rOPDate = DateTime.Now.Date;
	    private int status =0 ;
        
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public int SPPID
        {
            get { return sPPID; }
            set { sPPID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public decimal PakaiQty
        {
            get { return pakaiQty; }
            set { pakaiQty = value; }
        }
        public decimal SPPQty
        {
            get { return sPPQty; }
            set { sPPQty = value; }
        }
        public DateTime ROPDate
        {
            get { return rOPDate; }
            set { rOPDate = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
