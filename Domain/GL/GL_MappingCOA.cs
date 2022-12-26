using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GL_MappingCOA : GRCBaseDomain
    {
        private string pt = string.Empty;
        private string companyCode = string.Empty;
        private string tableName = string.Empty;
        private string typeTrx = string.Empty;
        private string groupPurchnCodeID = string.Empty;
        private string debetCredit = string.Empty;
        private int ptID = 0;
        private int itemTypeID = 0;
        private int depoID = 0;
        private int itemIDPlant = 0;
        private int itemIDPusat = 0;
        private int coaID = 0;
        private int coaIDDebit = 0;
        private int coaIDCredit = 0;

        public int PTID { get { return ptID; } set { ptID = value; } }
        public int ItemTypeID { get { return itemTypeID; } set { itemTypeID = value; } }
        public int DepoID { get { return depoID; } set { depoID = value; } }
        public int ItemIDPlant { get { return itemIDPlant; } set { itemIDPlant = value; } }
        public int ItemIDPusat { get { return itemIDPusat; } set { itemIDPusat = value; } }
        public int CoaID { get { return coaID; } set { coaID = value; } }
        public int CoaIDDebit { get { return coaIDDebit; } set { coaIDDebit = value; } }
        public int CoaIDCredit { get { return coaIDCredit; } set { coaIDCredit = value; } }
        public string DebetCredit { get { return debetCredit; } set { debetCredit = value; } }
        public string PT { get { return pt; } set { pt = value; } }
        public string CompanyCode { get { return companyCode; } set { companyCode = value; } }
        public string TableName { get { return tableName; } set { tableName = value; } }
        public string TypeTrx { get { return typeTrx; } set { typeTrx = value; } }
        public string GroupPurchnCodeID { get { return groupPurchnCodeID; } set { groupPurchnCodeID = value; } }


    }

}
