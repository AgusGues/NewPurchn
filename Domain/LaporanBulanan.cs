using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class LaporanBulanan : GRCBaseDomain
    {
        private string keterangan = string.Empty;
        private int bulan = 0;
        private int tahun = 0;
        private int status = 0;
        private string users = string.Empty;
        private string filename = string.Empty;
        private int lapid = 0;
        private int groupid = 0;
        private DateTime tglkrm = DateTime.Now.Date;
        private string groupDescription = string.Empty;
        private string pic = string.Empty;
        private int flag = 0;
        private int userid = 0;
        private string noted = string.Empty;
        private string groupname = string.Empty;
        private string accountemail = string.Empty;
        private string usernama = string.Empty;
        private string password = string.Empty;
        private string pic1 = string.Empty;
        private string pic2 = string.Empty;
        private string pic3 = string.Empty;
        private string sign2 = string.Empty;
        private string sign3 = string.Empty;
        private string periode = string.Empty;
        private string tglbuat = string.Empty;

        private string partno = string.Empty;
        private string sa = string.Empty;
        private decimal qtyAcc = 0;
        private decimal trans = 0;
        private decimal selisih = 0;
        private decimal qtylok = 0;

        public Decimal QtyLok
        {
            get { return qtylok; }
            set { qtylok = value; }
        }
        public Decimal Selisih
        {
            get { return selisih; }
            set { selisih = value; }
        }
        public Decimal Trans
        {
            get { return trans; }
            set { trans = value; }
        }
        public Decimal QtyAcc
        {
            get { return qtyAcc; }
            set { qtyAcc = value; }
        }
        public string Partno
        {
            get { return partno; }
            set { partno = value; }
        }
        public string SA
        {
            get { return sa; }
            set { sa = value; }
        }

        public string TanggalBuat
        {
            get { return tglbuat; }
            set { tglbuat = value; }
        }
        public string Periode
        {
            get { return periode; }
            set { periode = value; }
        }
        public string Sign3
        {
            get { return sign3; }
            set { sign3 = value; }
        }
        public string Sign2
        {
            get { return sign2; }
            set { sign2 = value; }
        }
        public string PIC1
        {
            get { return pic1; }
            set { pic1 = value; }
        }
        public string PIC2
        {
            get { return pic2; }
            set { pic2 = value; }
        }
        public string PIC3
        {
            get { return pic3; }
            set { pic3 = value; }
        }
        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }
        public string UserName
        {
            get { return usernama; }
            set { usernama = value; }
        }
        public string AccountEmail
        {
            get { return accountemail; }
            set { accountemail = value; }
        }
        public string GroupName
        {
            get { return groupname; }
            set { groupname = value; }
        }
        public string Noted
        {
            get { return noted; }
            set { noted = value; }
        }
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public string PIC
        {
            get { return pic; }
            set { pic = value; }
        }
        public DateTime TglKirim
        {
            get { return tglkrm; }
            set { tglkrm = value; }
        }
        public int GroupID
        {
            get { return groupid; }
            set { groupid = value; }
        }
        public int LapID
        {
            get { return lapid; }
            set { lapid = value; }
        }
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        public string Users
        {
            get { return users; }
            set { users = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
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
        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; }
        }
    }
}
