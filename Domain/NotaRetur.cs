using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class NotaRetur : GRCBaseDomain 
    {
        private string notareturNo = string.Empty;
        private int suratJalanID = 0;
        private string suratJalanNo = string.Empty;
        private int invoiceID = 0;
        private string invoiceNo = string.Empty;
        private string fakturpajakNo = string.Empty;
        private string company = string.Empty;
        private string keterangan = string.Empty;
        private int status = 0;
        private int typeRetur = 0;
        private int opID = 0;
        private string opNo = string.Empty;
        private int itemID = 0;
        private string descryption = string.Empty;
        private int quantity = 0;
        private int price = 0;
        private DateTime tglLapor = DateTime.MinValue;
        private decimal nilaiNotaRetur = 0;
        private DateTime tglNotaRetur = DateTime.MinValue;
        private int customerType = 0;
        private int customerID = 0;
        private int depoID = 0;
        private DateTime tglKirim = DateTime.MinValue;
        private DateTime tglPenerimaan = DateTime.MinValue;
        private string refSJNo = string.Empty;
        private int detailID = 0;

        private int distSubID = 0;
        private int typeDistSub = 0;

        public int DistSubID
        {
            get { return distSubID; }
            set { distSubID = value; }
        }
        public int TypeDistSub
        {
            get { return typeDistSub; }
            set { typeDistSub = value; }
        }
        public int DetailID
        {
            get { return detailID; }
            set { detailID = value; }
        }
        public string RefSJNo
        {
            get { return refSJNo; }
            set { refSJNo = value; }
        }
        public DateTime TglPenerimaan
        {
            get { return tglPenerimaan; }
            set { tglPenerimaan = value; }
        }
        public int CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public DateTime TglKirim
        {
            get { return tglKirim; }
            set { tglKirim = value; }
        }

        public DateTime TglNotaRetur
        {
            get { return tglNotaRetur; }
            set { tglNotaRetur = value; }
        }
        public decimal NilaiNotaRetur
        {
            get { return nilaiNotaRetur;}
            set { nilaiNotaRetur=value;}
        }
        public DateTime TglLapor
        {
            get
            {
                return tglLapor;
            }
            set
            {
                tglLapor = value;
            }

        }
        public int OpID
        {
            get
            {
                return opID;
            }
            set
            {
                opID = value;
            }

        }

        public string OpNo
        {
            get
            {
                return opNo;
            }
            set
            {
                opNo = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }

        }

        public string Descryption
        {
            get
            {
                return descryption;
            }
            set
            {
                descryption = value;
            }
        }


        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }

        }

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }

        }

        public string NotaReturNo
        {
            get
            {
                return notareturNo;
            }
            set
            {
                notareturNo = value;
            }
        }

        public int SuratJalanID
        {
            get
            {
                return suratJalanID;
            }
            set
            {
                suratJalanID = value;
            }

        }

        public string SuratJalanNo
        {
            get
            {
                return suratJalanNo;
            }
            set
            {
                suratJalanNo = value;
            }
        }

        public int InvoiceID
        {
            get
            {
                return invoiceID;
            }
            set
            {
                invoiceID = value;
            }
        }

        public string InvoiceNo
        {
            get
            {
                return invoiceNo;
            }
            set
            {
                invoiceNo = value;
            }
        }

        public string FakturPajakNo
        {
            get
            {
                return fakturpajakNo;
            }
            set
            {
                fakturpajakNo = value;
            }
        }

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        public string Keterangan
        {
            get
            {
                return keterangan;
            }
            set
            {
                keterangan = value;
            }

        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public int TypeRetur
        {
            get
            {
                return typeRetur;
            }
            set
            {
                typeRetur = value;
            }

        }


    }
}
