using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HelpDeskKeluhan : GRCBaseDomain
    {
        private string helpdeskno = string.Empty;
        private int id = 0;
        private DateTime helptgl = DateTime.Now.Date;
        private int deptid = 0;
        private string deptname = string.Empty;
        private int status = 0;
        private int helpdeskcategoryid = 0;
        private string keluhan = string.Empty;
        private string perbaikan = string.Empty;
        private DateTime tglperbaikan = DateTime.Now.Date;
        private string pic = string.Empty;
        private string analisa = string.Empty;
        private int kategoripenyelesaianID = 0;



        public string HelpDeskNo
        {
            get
            {
                return helpdeskno;
            }
            set
            {
                helpdeskno = value;
            }
        }

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

        public DateTime HelpTgl
        {
            get
            {
                return helptgl;
            }
            set
            {
                helptgl = value;
            }
        }

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

        public int HelpDeskCategoryID
        {
            get
            {
                return helpdeskcategoryid;
            }
            set
            {
                helpdeskcategoryid = value;
            }

        }

        public string Keluhan
        {
            get
            {
                return keluhan;
            }
            set
            {
                keluhan = value;
            }
        }

        public string Perbaikan
        {
            get
            {
                return perbaikan;
            }
            set
            {
                perbaikan = value;
            }
        }

        public DateTime TglPerbaikan
        {
            get
            {
                return tglperbaikan;
            }
            set
            {
                tglperbaikan = value;
            }
        }

        public string PIC
        {
            get
            {
                return pic;
            }
            set
            {
                pic = value;
            }
        }

        public string Analisa
        {
            get
            {
                return analisa;
            }
            set
            {
                analisa = value;
            }
        }

        public int KategoriPenyelesaianID
        {
            get
            {
                return kategoripenyelesaianID;
            }
            set
            {
                kategoripenyelesaianID = value;
            }
        }


    }
}
