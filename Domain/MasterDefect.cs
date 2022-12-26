using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MasterDefect : GRCBaseDomain
    {
        private string defCode = string.Empty;
        private string defName = string.Empty;
        private int deptID = 0 ;
        private int urutan = 0;
        private int rowStatus = 0;

        //private int rowStatus = 

        public string DefCode
        {
            get{ return defCode;}          
            set{ defCode = value;}
            
        }
        public string DefName
        {
            get { return defName; }
            set { defName = value;}

        }

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }

        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }
        public int Urutan
        {
            get { return urutan; }
            set { urutan = value; }
        }
    }

    //public class Area : GRCBaseDomain
    //{
    //    private string areaName = string.Empty;

    //    public string AreaName
    //    {
    //        get
    //        {
    //            return areaName;
    //        }
    //        set
    //        {
    //            areaName = value;
    //        }
    //    }
    //}

}

