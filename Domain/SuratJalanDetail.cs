using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class SuratJalanDetail
    {
        private int id = 0;
        private int suratJalanId = 0;
        private int itemID = 0;
        private string itemCode = string.Empty;
        private string itemName = string.Empty;
        private int qty = 0;
        private decimal totalPrice = 0;
        private int scheduleDetailId = 0;
        private int scheduleId = 0;
        private int itemId = 0;
        private string uomCode = string.Empty;
        private int flag = 0;
        private int paket = 0;
        private int pajak = 0;
        private int groupID = 0;
        private int depoID = 0;
        private int akangItem = 0;
        private string suratJalanNo = string.Empty;
        private string noPenerimaan = string.Empty;
        private int bankOutID = 0;
        private decimal hargaJual = 0;
        private string uom = string.Empty;
        private int typeKondisi = 0;

        public int TypeKondisi
        {
            get { return typeKondisi; }
            set { typeKondisi = value; }
        }
        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }
        public decimal HargaJual
        {
            get { return hargaJual; }
            set { hargaJual = value; }
        }
        public int BankOutID
        {
            get
            {
                return bankOutID;
            }
            set
            {
                bankOutID = value;
            }
        }
        public string NoPenerimaan
        {
            get
            {
                return noPenerimaan;
            }
            set
            {
                noPenerimaan = value;
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
        public int AkangItem
        {
            get
            {
                return akangItem;
            }
            set
            {
                akangItem = value;
            }
        }

        public int DepoID
        {
            get
            {
                return depoID;
            }
            set
            {
                depoID = value;
            }
        }
        public int GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
            }
        }
        public int Pajak
        {
            get
            {
                return pajak;
            }
            set
            {
                pajak = value;
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

        public int SuratJalanId
        {
            get
            {
                return suratJalanId;
            }
            set
            {
                suratJalanId = value;
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

        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
            }
        }

        public int ScheduleDetailId
        {
            get
            {
                return scheduleDetailId;
            }
            set
            {
                scheduleDetailId = value;
            }
        }

        public string UOMCode
        {
            get
            {
                return uomCode;
            }
            set
            {
                uomCode = value;
            }
        }

        public int Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        public int Paket
        {
            get
            {
                return paket;
            }
            set
            {
                paket = value;
            }
        }
    }
}
