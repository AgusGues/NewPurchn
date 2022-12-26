using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Reposting : GRCBaseDomain
    {
        int itemTypeID = 0;
        int groupID = 0;
        int tahun = 0;
        int bulan = 0;
        DateTime tanggal = DateTime.Now;
        string yMD= string.Empty;
        string yM = string.Empty;
        int itemID = 0;
        decimal quantity = 0;
        decimal price = 0;
        int iD = 0;
        string process = string.Empty;

        public int ItemTypeID
        { get { return itemTypeID; } set { itemTypeID = value; } }
        public int GroupID
        { get { return groupID; } set { groupID = value; } }
        public int Tahun
        { get { return tahun; } set { tahun = value; } }
        public int Bulan
        { get { return bulan; } set { bulan = value; } }
        public DateTime Tanggal
        { get { return tanggal; } set { tanggal = value; } }
        public string YMD
        { get { return yMD; } set { yMD = value; } }
        public string YM
        { get { return yM; } set { yM = value; } }
        public int ItemID
        { get { return itemID; } set { itemID = value; } }
        public decimal Quantity
        { get { return quantity; } set { quantity = value; } }
        public decimal Price
        { get { return price; } set { price = value; } }
        public int ID
        { get { return iD; } set { iD = value; } }
        public string Process
        { get { return process; } set { process = value; } }
    }
}
