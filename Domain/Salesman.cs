using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Salesman : GRCBaseDomain
    {
        private int salesmanID = 0;
        private string salesmanCode = string.Empty;
        private string salesmanName = string.Empty;
        private int unitSalesman = 0;
	    private int unitID = 0;
        private string telp = string.Empty;

        public int SalesmanID
        {
            get
            {
                return salesmanID;
            }
            set
            {
                salesmanID = value;
            }
        }

        public string SalesmanCode
        {
            get
            {
                return salesmanCode;
            }
            set
            {
                salesmanCode = value;
            }
        }

        public string SalesmanName
        {
            get
            {
                return salesmanName;
            }
            set
            {
                salesmanName = value;
            }
        }

        public int UnitSalesman
        {
            get
            {
                return unitSalesman;
            }
            set
            {
                unitSalesman = value;
            }
        }

        public int UnitID
        {
            get
            {
                return unitID;
            }
            set
            {
                unitID = value;
            }
        }

        public string Telp
        {
            get
            {
                return telp;
            }
            set
            {
                telp = value;
            }
        }
    }
}
