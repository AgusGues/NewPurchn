using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class JobdeskDetail : GRCBaseDomain
    {
        private int id = 0;
        private int bagianid = 0;
        private int jobdeskid = 0;
        private string bagianname = string.Empty;
        private string bawahan = string.Empty;
        private string tujabatan = string.Empty;
        private string tpjabatan = string.Empty;
        private string hubungankerja = string.Empty;
        private string tanggungjawab = string.Empty;
        private string wewenang = string.Empty;
        private string pendidikan = string.Empty;
        private string pengetahuan = string.Empty;
        private string pengalaman = string.Empty;
        private string usia = string.Empty;
        private string keterampilan = string.Empty;
        private string fisik = string.Empty;
        private string nonfisik = string.Empty;
        private string cariItemName = string.Empty;
        private DateTime tglSusun = DateTime.Today;
        private string atasan = string.Empty;
        private string approval = string.Empty;
        private int revisi = 0;
        private int status = 0;


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
        public int BagianID
        {
            get
            {
                return bagianid;
            }
            set
            {
                bagianid = value;
            }
        }
        public string BagianName
        {
            get
            {
                return bagianname;
            }
            set
            {
                bagianname = value;
            }
        }
        public int JOBDESKID
        {
            get
            {
                return jobdeskid;
            }
            set
            {
                jobdeskid = value;
            }
        }
        public string Bawahan
        {
            get
            {
                return bawahan;
            }
            set
            {
                bawahan = value;
            }
        }
        public string TUJabatan
        {
            get
            {
                return tujabatan;
            }
            set
            {
                tujabatan = value;
            }
        }
        public string TPJabatan
        {
            get
            {
                return tpjabatan;
            }
            set
            {
                tpjabatan = value;
            }
        }
        public string HubunganKerja
        {
            get
            {
                return hubungankerja;
            }
            set
            {
                hubungankerja = value;
            }
        }
        public string TanggungJawab
        {
            get
            {
                return tanggungjawab;
            }
            set
            {
                tanggungjawab = value;
            }
        }
        public string Wewenang
        {
            get
            {
                return wewenang;
            }
            set
            {
                wewenang = value;
            }
        }
        public string Pendidikan
        {
            get
            {
                return pendidikan;
            }
            set
            {
                pendidikan = value;
            }
        }
        public string Pengalaman
        {
            get
            {
                return pengalaman;
            }
            set
            {
                pengalaman = value;
            }
        }
        public string Usia
        {
            get
            {
                return usia;
            }
            set
            {
                usia = value;
            }
        }
        public string Pengetahuan
        {
            get
            {
                return pengetahuan;
            }
            set
            {
                pengetahuan = value;
            }
        }
        public string Keterampilan
        {
            get
            {
                return keterampilan;
            }
            set
            {
                keterampilan = value;
            }
        }
        public string Fisik
        {
            get
            {
                return fisik;
            }
            set
            {
                fisik = value;
            }
        }
        public string NonFisik
        {
            get
            {
                return nonfisik;
            }
            set
            {
                nonfisik = value;
            }
        }
        public DateTime TglSusun
        {
            get { return tglSusun; }
            set { tglSusun = value; }
        }

        public string CariItemName
        {
            get
            {
                return cariItemName;
            }
            set
            {
                cariItemName = value;
            }
        }
        public string Atasan
        {
            get
            {
                return atasan;
            }
            set
            {
                atasan = value;
            }
        }
        public string Approval
        {
            get
            {
                return approval;
            }
            set
            {
                approval = value;
            }
        }
        public int Revisi
        {
            get
            {
                return revisi;
            }
            set
            {
                revisi = value;
            }
        }
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
    }
}
