using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class NotaReturDetail
    {
        private int id = 0;
        private int notareturId = 0;
        private int itemId = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int qty = 0;
        private decimal harga = 0;
        private int typeKondisi = 0;
        private int nilaiRetur = 0;
        //tambahan
        private int refOPID = 0;
        private string refOPNo = string.Empty;
        private int refSJID = 0;
        private string refSJNo = string.Empty;
        private int refSJDetailID = 0;
        //
        private int asalBarang = 0;
        private int notaReturType = 0;
        private int cnDetailID = 0;
        private int status = 0;
        private int headSJID = 0;
        private string headSJNO = string.Empty;
        private DateTime tglKirim = DateTime.MinValue;
        private string cnNo = string.Empty;
        private int detailID = 0;
        private int depoID = 0;

        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public int DetailID
        {
            get { return detailID; }
            set { detailID = value; }
        }
        public string CnNo
        {
            get { return cnNo; }
            set { cnNo = value; }
        }
        public DateTime TglKirim
        {
            get { return tglKirim; }
            set { tglKirim = value; }
        }
        public string HeadSJNO
        {
            get { return headSJNO; }
            set { headSJNO = value; }
        }
        public int HeadSJID
        {
            get { return headSJID; }
            set { headSJID = value; }
        }

        public int Status
        { get { return status;}
          set { status = value;}
        }
        public int CNDetailID
        {
            get
            {
                return cnDetailID;
            }
            set
            {
                cnDetailID = value;
            }
        }
        public int NotaReturType
        {
            get
            {
                return notaReturType;
            }
            set
            {
                notaReturType = value;
            }
        }
        public int AsalBarang
        {
            get
            {
                return asalBarang;
            }
            set
            {
                asalBarang = value;
            }
        }

        //tambahan
        public int RefOPID
        {
            get
            {
                return refOPID;
            }
            set
            {
                refOPID = value;
            }
        }

        public string RefOPNo
        {
            get
            {
                return refOPNo;
            }
            set
            {
                refOPNo = value;
            }
        }

        public int RefSJID
        {
            get
            {
                return refSJID;
            }
            set
            {
                refSJID = value;
            }
        }

        public string RefSJNo
        {
            get
            {
                return refSJNo;
            }
            set
            {
                refSJNo = value;
            }
        }

        public int RefSJDetailID
        {
            get
            {
                return refSJDetailID;
            }
            set
            {
                refSJDetailID = value;
            }
        }
      
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }

        }

        public int NotaReturID
        {
            get
            {
                return notareturId;
            }
            set
            {
                notareturId = value;
            }
        }

        public int ItemID
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }

        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }

        public decimal Harga
        {
            get
            {
                return harga;
            }
            set
            {
                harga = value;
            }
        }

        public int TypeKondisi
        {
            get
            {
                return typeKondisi;
            }
            set
            {
                typeKondisi = value;
            }
        }

        public int NilaiRetur
        {
            get
            {
                return nilaiRetur;
            }
            set
            {
                nilaiRetur = value;
            }

        }

    }
}
