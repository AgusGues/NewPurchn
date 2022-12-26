using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class LoadingTime : GRCBaseDomain
    {
        /** Tambahan mobil Luar dan Dalam Pulau **/
        public string Tujuan2 { get; set; }
        public string Tujuan { get; set; }
        public string TKirim { get; set; }
        public int Status22 { get; set; }
        private DateTime timedaftar = DateTime.Now.Date;
        public string noted { get; set; }
        //public decimal Target { get; set; }
        public string Target { get; set; }
        public int Status2 { get; set; }
        /** End Tambahan mobil Luar dan Dalam Pulau **/

        private string noPolisi = string.Empty;
        private string jenisMobil = string.Empty;
        private string expedisiName = string.Empty;
        private DateTime tanggal = DateTime.Now.Date;
        private DateTime timeIn = DateTime.Now.Date;
        private DateTime timeOut = DateTime.MinValue;   //.Now.Date;
        private int kendaraanID = 0;
        private int status = 0;
        private int ekspedisiID = 0;
        private string ekspedisiName = string.Empty;
        private string keterangan = string.Empty;
        private string loadingNo = string.Empty;
        private string noUrut = string.Empty;
        private DateTime tglIn = DateTime.MinValue;
        private DateTime tglOut = DateTime.MinValue;
        private int mobilSendiri = 0;
        private int ritase = 0;
        private string cardNo = "0";
        private string cardID = string.Empty;
        private int flag = 0;
        public virtual string TglKeluar { get; set; }

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public int Ritase
        {
            get { return ritase; }
            set { ritase = value; }
        }
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        public string CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }
        public int MobilSendiri
        {
            get { return mobilSendiri; }
            set { mobilSendiri = value; }
        }
        public DateTime TglIn
        {
            get { return tglIn; }
            set { tglIn = value; }
        }
        public DateTime TglOut
        {
            get { return tglOut; }
            set { tglOut = value; }
        }
        public string NoUrut
        {
            get { return noUrut; }
            set { noUrut = value; }
        }
        public string LoadingNo
        {
            get { return loadingNo; }
            set { loadingNo = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public string NoPolisi
        {
            get { return noPolisi; }
            set { noPolisi = value; }
        }
        public string JenisMobil
        {
            get { return jenisMobil; }
            set { jenisMobil = value; }
        }
        public string ExpedisiName
        {
            get { return expedisiName; }
            set { expedisiName = value; }
        }
        public DateTime Tanggal
        {
            get{return tanggal;}
            set{tanggal = value;}
        }
        public DateTime TimeIn
        {
            get{return timeIn;}
            set{timeIn = value;}
        }
        public DateTime TimeOut
        {
            get{return timeOut;}
            set{timeOut = value;}
        }
        public int KendaraanID
        {
            get{return kendaraanID;}
            set{kendaraanID = value;}
        }
        public int Status
        {
            get{return status;}
            set{status = value;}
        }
        public int EkspedisiID
        {
            get { return ekspedisiID; }
            set { ekspedisiID = value; }
        }
        public string EkspedisiName
        {
            get { return ekspedisiName; }
            set { ekspedisiName = value; }
        }
        public DateTime TimeDaftar
        {
            get { return timedaftar; }
            set { timedaftar = value; }
        }
        public string Noted
        {
            get { return noted; }
            set { noted = value; }
        }

        
        public decimal Targete { get; set; }
        public decimal Pencapaian { get; set; }
        public int JmlMobil { get; set; }
        public int JmlOK { get; set; }
        public int JmlLewat { get; set; }
        public int Luar { get; set; }
       
    }
}
