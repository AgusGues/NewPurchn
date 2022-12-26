using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class LockSys:GRCBaseDomain
    {
        private DateTime daritgl = DateTime.MinValue;
        private DateTime sampaitgl = DateTime.MinValue;
        private string durasi = string.Empty;
        private string jamdari = string.Empty;
        private string jamsampai = string.Empty;
        private string keterangan = string.Empty;
        private string userlock = string.Empty;
        private string statuse = string.Empty;
        private string stat = string.Empty;

        public DateTime DariTgl
        {
            set { daritgl = value; }
            get { return daritgl; }
        }
        public DateTime SampaiTgl
        {
            set { sampaitgl = value; }
            get { return sampaitgl; }
        }
        public string Durasi
        {
            set { durasi = value; }
            get { return durasi; }
        }
        public string DariJam
        {
            set { jamdari = value; }
            get { return jamdari; }
        }
        public string SampaiJam
        {
            set { jamsampai = value; }
            get { return jamsampai; }
        }
        public string Keterangan
        {
            set { keterangan = value; }
            get { return keterangan; }
        }
        public string UserLock
        {
            set { userlock = value; }
            get { return userlock; }
        }
        public string StatusE
        {
            set { statuse = value; }
            get { return statuse; }
        }
        public string Stat
        {
            set { stat = value; }
            get { return stat; }
        }
    }
}
