using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MTC_Perawatan : GRCBaseDomain
    {
        int deptID  =0;
        string deptName=string.Empty  ;
        int plantID=0 ;
        string plantName=string.Empty  ;
        int zonaID=0;
        string zonaName=string.Empty  ;
        int macSysID=0 ;
        string macSysName = string.Empty;
        int macSysPartID=0 ;
        string macSysPartName=string.Empty  ;
        string perawatanName=string.Empty  ;
        string perawatanDesc = string.Empty;
        DateTime  tanggal=DateTime.Now ; 
        int jmlMenit=0 ;
        int pakaiDetailID=0 ;
        int itemID=0 ;
        string itemName = string.Empty;
        int quantity = 0;

        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        public int PlantID
        {
            get { return plantID; }
            set { plantID = value; }
        }
        public string PlantName
        {
            get { return plantName; }
            set { plantName = value; }
        }
        public int ZonaID
        {
            get { return zonaID; }
            set { zonaID = value; }
        }
        public string ZonaName
        {
            get { return zonaName; }
            set { zonaName = value; }
        }
        public int MacSysID
        {
            get { return macSysID; }
            set { macSysID = value; }
        }
        public string MacSysName
        {
            get { return macSysName; }
            set { macSysName = value; }
        }
        public int MacSysPartID
        {
            get { return macSysPartID; }
            set { macSysPartID = value; }
        }
        public string MacSysPartName
        {
            get { return macSysPartName; }
            set { macSysPartName = value; }
        }
        public string  PerawatanName
        {
            get { return perawatanName; }
            set { perawatanName = value; }
        }
        public string PerawatanDesc
        {
            get { return perawatanDesc; }
            set { perawatanDesc = value; }
        }
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; }
        }
        public int JmlMenit
        {
            get { return jmlMenit; }
            set { jmlMenit = value; }
        }
        public int PakaiDetailID
        {
            get { return pakaiDetailID; }
            set { pakaiDetailID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

    }
}
