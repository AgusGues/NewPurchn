using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SaldoInventoryBJ : GRCBaseDomain
    {
        private int itemID = 0;
        private int monthPeriod = 0;
        private int yearPeriod = 0;
        private int posting = 0;
        private int groupID = 0;
        private int itemTypeID = 0;
        private decimal quantity = 0;

        private decimal janQty = 0;
        private decimal janAvgPrice = 0;
        private decimal febQty = 0;
        private decimal febAvgPrice = 0;
        private decimal marQty = 0;
        private decimal marAvgPrice = 0;
        private decimal aprQty = 0;
        private decimal aprAvgPrice = 0;
        private decimal meiQty = 0;
        private decimal meiAvgPrice = 0;
        private decimal junQty = 0;
        private decimal junAvgPrice = 0;
        private decimal julQty = 0;
        private decimal julAvgPrice = 0;
        private decimal aguQty = 0;
        private decimal aguAvgPrice = 0;
        private decimal sepQty = 0;
        private decimal sepAvgPrice = 0;
        private decimal oktQty = 0;
        private decimal oktAvgPrice = 0;
        private decimal novQty = 0;
        private decimal novAvgPrice = 0;
        private decimal desQty = 0;
        private decimal desAvgPrice = 0;
        private decimal saldoQty = 0;
        private decimal saldoPrice = 0;
        private string partNo = string.Empty;
        private string itemDesc = string.Empty;
        private decimal stokAwal = 0;
        private decimal stokAkhir = 0;
        private decimal avgPrice = 0;

        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }
        public decimal StokAkhir
        {
            get { return stokAkhir; }
            set { stokAkhir = value; }
        }
        public decimal StokAwal
        {
            get { return stokAwal; }
            set { stokAwal = value; }
        }
        
        public String ItemDesc
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }
        public String PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }
        public int ItemTypeID
        {
            get { return itemTypeID; }
            set { itemTypeID = value; }
        }
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int MonthPeriod
        {
            get { return monthPeriod; }
            set { monthPeriod = value; }
        }
        public int YearPeriod
        {
            get { return yearPeriod; }
            set { yearPeriod = value; }
        }
        public int Posting
        {
            get { return posting; }
            set { posting = value; }
        }
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public decimal SaldoQty
        {
            get { return saldoQty; }
            set { saldoQty = value; }
        }
        public decimal SaldoPrice
        {
            get { return saldoPrice; }
            set { saldoPrice = value; }
        }

        public decimal JanAvgPrice
        {
            get { return janAvgPrice; }
            set { janAvgPrice = value; }
        }
        public decimal JanQty
        {
            get { return janQty; }
            set { janQty = value; }
        }
        
        public decimal FebAvgPrice
        {
            get { return febAvgPrice; }
            set { febAvgPrice = value; }
        }
        public decimal FebQty
        {
            get { return febQty; }
            set { febQty = value; }
        }
        
        public decimal MarAvgPrice
        {
            get { return marAvgPrice; }
            set { marAvgPrice = value; }
        }
        public decimal MarQty
        {
            get { return marQty; }
            set { marQty = value; }
        }
        
        public decimal AprAvgPrice
        {
            get { return aprAvgPrice; }
            set { aprAvgPrice = value; }
        }
        public decimal AprQty
        {
            get { return aprQty; }
            set { aprQty = value; }
        }
        
        public decimal MeiAvgPrice
        {
            get { return meiAvgPrice; }
            set { meiAvgPrice = value; }
        }
        public decimal MeiQty
        {
            get { return meiQty; }
            set { meiQty = value; }
        }
        
        public decimal JunAvgPrice
        {
            get { return junAvgPrice; }
            set { junAvgPrice = value; }
        }
        public decimal JunQty
        {
            get { return junQty; }
            set { junQty = value; }
        }
        
        public decimal JulAvgPrice
        {
            get { return julAvgPrice; }
            set { julAvgPrice = value; }
        }
        public decimal JulQty
        {
            get { return julQty; }
            set { julQty = value; }
        }
        
        public decimal AguAvgPrice
        {
            get { return aguAvgPrice; }
            set { aguAvgPrice = value; }
        }
        public decimal AguQty
        {
            get { return aguQty; }
            set { aguQty = value; }
        }
        public decimal SepAvgPrice
        {
            get { return sepAvgPrice; }
            set { sepAvgPrice = value; }
        }
        public decimal SepQty
        {
            get { return sepQty; }
            set { sepQty = value; }
        }
        
        public decimal OktAvgPrice
        {
            get { return oktAvgPrice; }
            set { oktAvgPrice = value; }
        }
        public decimal OktQty
        {
            get { return oktQty; }
            set { oktQty = value; }
        }

        public decimal NovAvgPrice
        {
            get { return novAvgPrice; }
            set { novAvgPrice = value; }
        }
        public decimal NovQty
        {
            get { return novQty; }
            set { novQty = value; }
        }
        
        public decimal DesAvgPrice
        {
            get { return desAvgPrice; }
            set { desAvgPrice = value; }
        }
        public decimal DesQty
        {
            get { return desQty; }
            set { desQty = value; }
        }

    }
}
