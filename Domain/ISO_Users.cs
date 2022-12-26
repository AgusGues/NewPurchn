using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_Users : GRCBaseDomain
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
        private int companyID = 0;
        private int userLevel = 0;        
        private int plant = 0;
        private int pesType = 0;
        private string kodeUrutan = string.Empty;
        /** added on 17-02-2017*/
        private int nom = 0;
        private string CompanyName = string.Empty;
        private string DeptName = string.Empty;
        private int BagianID = 0;
        private string BagianName = string.Empty;
        private string unitkerjaname = string.Empty;
        private string typeunitkerjaname=string.Empty;
        /** end of added*/

        public int Nom { get { return nom; } set { nom = value; } }
        public string companyname { get { return CompanyName; } set { CompanyName = value; } }
        public string deptname { get { return DeptName; } set { DeptName = value; } }
        public int bagian { get { return BagianID; } set { BagianID = value; } }
        public string bagianname { get { return BagianName; } set { BagianName = value; } }
        public string UnitKerjaName { get { return unitkerjaname; } set { unitkerjaname = value; } }
        public string TypeUnitKerjaName { get { return typeunitkerjaname; } set { typeunitkerjaname = value; } }
        public string NIK { get;  set;  }

        public int Plant
        {
            get { return plant; }
            set { plant = value; }
        }
        public int PesType
        {
            get { return pesType; }
            set { pesType = value; }
        }
        public string KodeUrutan
        {
            get { return kodeUrutan; }
            set { kodeUrutan = value; }
        }

        public int UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }
        public int CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
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
    }
}
