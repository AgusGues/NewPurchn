using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_Bagian : GRCBaseDomain
    {
        private int deptID = 0;
        private string bagianName = string.Empty;
        private string urutan = string.Empty;
        private int userGroupID = 0;
        private int plant = 0;
        private int rowStatus = 0;

        private int bulan = 0;
        private int tahun = 0;
        private decimal bobotKpi = 0;
        private decimal bobotSop = 0;
        private decimal bobotTask = 0;
        private decimal bobotDisiplin = 0;
        private string userName = string.Empty;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int Bulan
        {
            get { return bulan; }
            set { bulan = value; }
        }
        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }
        public decimal BobotKpi
        {
            get { return bobotKpi; }
            set { bobotKpi = value; }
        }
        public decimal BobotSop
        {
            get { return bobotSop; }
            set { bobotSop = value; }
        }
        public decimal BobotTask
        {
            get { return bobotTask; }
            set { bobotTask = value; }
        }
        public decimal BobotDisiplin
        {
            get { return bobotDisiplin; }
            set { bobotDisiplin = value; }
        }

        public string BagianName
        {
            get { return bagianName; }
            set { bagianName = value; }
        }
        public string Urutan
        {
            get { return urutan; }
            set { urutan = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int UserGroupID
        {
            get { return userGroupID; }
            set { userGroupID = value; }
        }
        public int Plant
        {
            get { return plant; }
            set { plant = value; }
        }
        public int RowStatus
        {
            get { return rowStatus; }
            set { rowStatus = value; }
        }

        /**
         * Added on 19-02-2014
         * By Iswan Putera
         */
        private string deptname = string.Empty;

        public string DeptName
        {
            get { return deptname; }
            set { deptname = value; }
        }
    }
}
