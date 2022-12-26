using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class HargaSupplier : GRCBaseDomain
    {
        private string planName = string.Empty;
        private string supplierCode = string.Empty;
        private string supplierName = string.Empty;
        private int supplierId = 0;

        private int idHarga = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private Decimal harga = 0;

        private int idHargaD = 0;
        private string itemCodeD = string.Empty;
        private string itemNameD = string.Empty;
        private Decimal hargaD = 0;

        private int plan = 0;

        public int Plan { get { return plan; } set { plan = value; } }

        public Decimal HargaD { get { return hargaD; } set { hargaD = value; } }
        public string ItemNameD { get { return itemNameD; } set { itemNameD = value; } }
        public string ItemCodeD { get { return itemCodeD; } set { itemCodeD = value; } }
        public int IdHargaD { get { return idHargaD; } set { idHargaD = value; } }

        public Decimal Harga { get { return harga; } set { harga = value; } }
        public string ItemName { get { return itemName; } set { itemName = value; } }
        public string ItemCode { get { return itemCode; } set { itemCode = value; } }
        public int IdHarga { get { return idHarga; } set { idHarga = value; } }

        public int SupplierId{get{return supplierId;}set{supplierId = value;}}
        public string SupplierName { get { return supplierName; } set { supplierName = value; } }
        public string SupplierCode { get { return supplierCode; } set { supplierCode = value; } }
        public string PlanName { get { return planName; } set { planName = value; } }

    }
}
