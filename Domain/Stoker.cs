using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Stoker : GRCBaseDomain
    {
        private int id = 0;
        //private int idstoker = 0;
        private string nama = string.Empty;
        private string lokasi = string.Empty;
        private string partno = string.Empty;
        private int rowstatus = 0;
        private int idserah = 0;
        private int deptid = 0;
        private decimal qty = 0;

        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public int DeptID
        {
            get { return deptid; }
            set { deptid = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }   
        public int IDserah
        {
            get { return idserah; }
            set { idserah = value; }
        }

        public string Partno
        {
            get { return partno; }
            set { partno= value; }
        }

        public string Lokasi
        {
            get { return lokasi; }
            set { lokasi = value; }
        }

        //public string Alias
        //{
        //    get { return alias; }
        //    set { alias = value; }
        //}

        public string Nama
        {
            get { return nama; }
            set { nama = value; }
        }

        public int RowStatus
        {
            get { return rowstatus; }
            set { rowstatus = value; }
        }
       
    }
}
