using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class JobDesk : GRCBaseDomain
    {
        private int id = 0;
        private int user_ID = 0;
        private int deptid = 0;
        private int jabatan = 0;
        private string jobdesk_No = string.Empty;
        private string atasan = string.Empty;
        private string bawahan = string.Empty;
        private DateTime tglsusun = DateTime.Now.Date;
        private int revisi = 0;
        private int status = 0;
        private int approval = 0;
        private string approval2 = string.Empty;
        private string bagianname = string.Empty;
        private string deptname = string.Empty;
        private DateTime approveddate = DateTime.Today;

        private string tujabatan = string.Empty;
        private string tpjabatan = string.Empty;
        private string hubungankerja = string.Empty;
        private string tanggungjawab = string.Empty;
        private string wewenang = string.Empty;
        private string fisik = string.Empty;
        private string nonfisik = string.Empty;
        private string pendidikan = string.Empty;
        private string pengalaman = string.Empty;
        private string usia = string.Empty;
        private string pengetahuan = string.Empty;
        private string keterampilan = string.Empty;
        private string aliasdept = string.Empty;
        private string typejobdesk = string.Empty;
        private string alsancancel = string.Empty;
        private string alsantidakikutrevisi = string.Empty;

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
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
        public int DeptID
        {
            get
            {
                return deptid;
            }
            set
            {
                deptid = value;
            }
        }
        public string JOBDESK_No
        {
            get
            {
                return jobdesk_No;
            }
            set
            {
                jobdesk_No = value;
            }
        }
        public int Jabatan
        {
            get
            {
                return jabatan;
            }
            set
            {
                jabatan = value;
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
        public DateTime TglSusun
        {
            get { return tglsusun; }
            set { tglsusun = value; }
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
        public int Approval
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
        public string Approval2 { get { return approval2; } set { approval2 = value; } }
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
        public string DeptName
        {
            get
            {
                return deptname;
            }
            set
            {
                deptname = value;
            }
        }
        public DateTime ApprovedDate
        {
            get { return approveddate; }
            set { approveddate = value; }
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
        public string AliasDept
        {
            get
            {
                return aliasdept;
            }
            set
            {
                aliasdept = value;
            }
        }

        public int Tahun { get; set; }

        public string TypeJobDesk
        {
            get
            {
                return typejobdesk;
            }
            set
            {
                typejobdesk = value;
            }
        }

        public string AlasanCancel
        {
            get
            {
                return alsancancel;
            }
            set
            {
                alsancancel = value;
            }
        }

        public string AlasanTidakIkutRevisi
        {
            get
            {
                return alsantidakikutrevisi;
            }
            set
            {
                alsantidakikutrevisi = value;
            }
        }
    }
}
