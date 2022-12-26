using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class OptimumStockData : GRCBaseDomain
    {
        private int iDn = 0;
        private int iD_stockProd = 0;
        private int group_IDStockProd = 0;
        private string groupName = string.Empty;
        private int min = 0;
        private int max = 0;
        private decimal tebal = 0;
        private decimal lebar = 0;
        
        private decimal panjang = 0;


        private string sisi = string.Empty;

        public string Sisi
        {
            get { return sisi; }
            set { sisi = value; }
        }


        private DateTime createDate = DateTime.Now.Date;

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        
        private int tahun = 0;

        
        private int bulan = 0;


        private int status = 0;

        

        public int IDn
        {
            get { return iDn; }
            set { iDn = value; }
        }

        public int ID_stockProd
        {
            get { return iD_stockProd; }
            set { iD_stockProd = value; }
        }

        public int Group_IDStockProd
        {
            get { return group_IDStockProd; }
            set { group_IDStockProd = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public decimal Tebal
        {
            get { return tebal; }
            set { tebal = value; }
        }

        public decimal Lebar
        {
            get { return lebar; }
            set { lebar = value; }
        }

        public decimal Panjang
        {
            get { return panjang; }
            set { panjang = value; }
        }

        

        

        public int Tahun
        {
            get { return tahun; }
            set { tahun = value; }
        }

        public int Bulan
        {
            get { return bulan; }
            set { bulan = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
