using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Task : GRCBaseDomain
    {
        private int usergroupID = 0;
        private int userID = 0;     
        private int deptID = 0;
        private int bagianID = 0;
        private int categoryID = 0;
        private int bobotNilai = 0;
        private int status = 0;
        private int depoID = 0;
        private int targetKe = 0;
        private int taskID = 0;
        private int idDetail = 0;
        private int aktip = 0;
        private int app = 0;        
        private decimal pointNilai = 0;
        private string pic = string.Empty;
        private string taskNo = string.Empty;
        private string newTask = string.Empty;
        private string ket = string.Empty;
        private string deptName = string.Empty;
        private string bagianName = string.Empty;
        private DateTime tglMulai = DateTime.Now.Date;
        private DateTime tglSelesai = DateTime.MinValue;
        private DateTime tglTarget = DateTime.Now.Date;
        private string image = string.Empty;
        private int jumlah = 0;
        private int iso_UserID = 0;
        private int taskType = 0;
        private int mailsent = 0;
        private string departemen = string.Empty;
        private string tglUpload = string.Empty;
                              
        public string Departemen
        {
            get { return departemen; }
            set { departemen = value; }
        }
        public int TaskType
        {
            get { return taskType; }
            set { taskType = value; }
        }
        public int Iso_UserID
        {
            get { return iso_UserID; }
            set { iso_UserID = value; }
        }
        public decimal PointNilai
        {
            get { return pointNilai; }
            set { pointNilai = value; }
        }

        public int UserGroupID
        {
            get { return usergroupID; }
            set { usergroupID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int App
        {
            get { return app; }
            set { app = value; }
        }
        public int Aktip
        {
            get { return aktip; }
            set { aktip = value; }
        }
        public int IdDetail
        {
            get { return idDetail; }
            set { idDetail = value; }
        }
        public int TaskID
        {
            get { return taskID; }
            set { taskID = value; }
        }
        public int TargetKe
        {
            get { return targetKe; }
            set { targetKe = value; }
        }
        public int DepoID
        {
            get { return depoID; }
            set { depoID = value; }
        }
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int BagianID
        {
            get { return bagianID; }
            set { bagianID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public int BobotNilai
        {
            get { return bobotNilai; }
            set { bobotNilai = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string BagianName
        {
            get { return bagianName; }
            set { bagianName = value; }
        }
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public string TaskNo
        {
            get { return taskNo; }
            set { taskNo = value; }
        }
        public string NewTask
        {
            get { return newTask; }
            set { newTask = value; }
        }
        public string Ket
        {
            get { return ket; }
            set { ket = value; }
        }
        public DateTime TglMulai
        {
            get { return tglMulai; }
            set { tglMulai = value; }
        }
        public DateTime TglTarget
        {
            get { return tglTarget; }
            set { tglTarget = value; }
        }
        public DateTime TglSelesai
        {
            get { return tglSelesai; }
            set { tglSelesai = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public int Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }
        public int MailSent
        {
            get { return mailsent; }
            set { mailsent = value; }
        }
        public string AlasanCancel { get; set; }

        public string TglUpload
        {
            get
            {
                return tglUpload;
            }

            set
            {
                tglUpload = value;
            }
        }
    }
}
