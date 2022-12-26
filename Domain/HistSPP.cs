using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HistSPP : GRCBaseDomain
    {
        DateTime pOPurchnDate = DateTime.Now.Date;
        DateTime tglSPP = DateTime.Now.Date;
        DateTime dlvDate = DateTime.Now.Date;
        string noPO = string.Empty;
        string noSPP = string.Empty;
        string itemCode = string.Empty;
        string itemName = string.Empty;
        string satuan = string.Empty;
        decimal price = 0;
        string cRC = string.Empty;
        decimal qty = 0;
        string supplierName = string.Empty;
        string telepon = string.Empty;
        string termin = string.Empty;
        string delivery = string.Empty;
        decimal disc = 0;
        decimal pPN = 0;
        string approval = string.Empty;

        public DateTime DlvDate
        {
            get { return dlvDate; }
            set { dlvDate = value; }
        }
        public DateTime POPurchnDate
        {
            get { return pOPurchnDate; }
            set { pOPurchnDate = value; }
        }
        public DateTime TglSPP
        {
            get { return tglSPP; }
            set { tglSPP = value; }
        }

        public string NoPO
        {
            get { return noPO; }
            set { noPO = value; }
        }
        public string NoSPP
        {
            get { return noSPP; }
            set { noSPP = value; }
        }
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public string Satuan
        {
            get { return satuan; }
            set { satuan = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public string CRC
        {
            get { return cRC; }
            set { cRC = value; }
        }
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }
        public string Telepon
        {
            get { return telepon; }
            set { telepon = value; }
        }
        public string Termin
        {
            get { return termin; }
            set { termin = value; }
        }
        public string Delivery
        {
            get { return delivery; }
            set { delivery = value; }
        }
        public decimal Disc
        {
            get { return disc; }
            set { disc = value; }
        }
        public decimal PPN
        {
            get { return pPN; }
            set { pPN = value; }
        }
        public string Approval
        {
            get { return approval; }
            set { approval = value; }
        }


    }
}
