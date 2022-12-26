using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_Kirim : GRCBaseDomain
    {
        string customer=string.Empty ;
        string sJNo = string.Empty;
        string oPNo = string.Empty;
        DateTime  tglKirim=DateTime.Now ;
        int total=0;

        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public string SJNo
        {
            get { return sJNo; }
            set { sJNo = value; }
        }
        public string OPNo
        {
            get { return oPNo ; }
            set { oPNo = value; }
        }
       
        public int  Total
        {
            get { return total ; }
            set { total  = value; }
        }
        public DateTime  TglKirim
        {
            get { return tglKirim ; }
            set { tglKirim = value; }
        }
    }

    public class CopyOfT3_Kirim : GRCBaseDomain
    {
        string customer = string.Empty;
        string sJNo = string.Empty;
        string oPNo = string.Empty;
        DateTime tglKirim = DateTime.Now;
        int total = 0;

        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public string SJNo
        {
            get { return sJNo; }
            set { sJNo = value; }
        }
        public string OPNo
        {
            get { return oPNo; }
            set { oPNo = value; }
        }

        public int Total
        {
            get { return total; }
            set { total = value; }
        }
        public DateTime TglKirim
        {
            get { return tglKirim; }
            set { tglKirim = value; }
        }
    }
}
