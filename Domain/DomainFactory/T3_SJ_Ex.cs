using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class T3_SJ_Ex : GRCBaseDomain
    {
        private int iD = 0;
        private string suratjalanNo =string.Empty  ;
        private DateTime  tglKirim = DateTime.Now;
        private string customer = string.Empty;

        public int ID { get { return iD; } set { iD = value; } }
        public string SuratjalanNo { get { return suratjalanNo; } set { suratjalanNo = value; } }
        public DateTime TglKirim { get { return tglKirim; } set { tglKirim = value; } }
        public string Customer { get { return customer; } set { customer = value; } }
    }

    public class CopyOfT3_SJ_Ex : GRCBaseDomain
    {
        private int iD = 0;
        private string suratjalanNo = string.Empty;
        private DateTime tglKirim = DateTime.Now;
        private string customer = string.Empty;

        public int ID { get { return iD; } set { iD = value; } }
        public string SuratjalanNo { get { return suratjalanNo; } set { suratjalanNo = value; } }
        public DateTime TglKirim { get { return tglKirim; } set { tglKirim = value; } }
        public string Customer { get { return customer; } set { customer = value; } }
    }
}
