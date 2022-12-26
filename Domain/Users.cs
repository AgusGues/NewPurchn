using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Users : GRCBaseDomain
    {
        private string userID = string.Empty;
        private string userName = string.Empty;
        private string password = string.Empty;
        private int typeUnitKerja = 0;
        private int unitKerjaID = 0;
        private int viewPrice = 0;
        private string tmpPeriode = string.Empty;
        private int deptID = 0;
        private int apv = 0;
        private int groupID = 0;
        private int headID = 0;
        private string usrMail = string.Empty;
        private int userLevel = 0;
        public int Flag { get; set; }
        private string useralias = string.Empty;

        public int CompanyID { get; set; }
        public string KodePT { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string PssMail { get; set; }
        public int DepoID { get; set; }


        public string UserAlias { get { return useralias; } set { useralias = value; } }
       
        public int UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }
        public int HeadID
        {
            get { return headID; }
            set { headID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        public int Apv
        {
            get { return apv; }
            set { apv = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public string TmpPeriode
        {
            get
            {
                return tmpPeriode;
            }
            set
            {
                tmpPeriode = value;
            }
        }
        public string UsrMail { get { return usrMail; } set { usrMail = value; } }
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public int TypeUnitKerja
        {
            get
            {
                return typeUnitKerja;
            }
            set
            {
                typeUnitKerja = value;
            }
        }
        public int UnitKerjaID
        {
            get
            {
                return unitKerjaID;
            }
            set
            {
                unitKerjaID = value;
            }
        }
        public int ViewPrice
        {
            get
            {
                return viewPrice;
            }
            set
            {
                viewPrice = value;
            }
        }
        public string KodeLokasi { get; set; }
        public int BagianID { get; set; }
        public string NIK { get; set; }
    }
}
