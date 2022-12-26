using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ISO_SOPScore : GRCBaseDomain
    {
        private int pesType = 0;
        private int categoryID = 0;
        private string targetKe = string.Empty;
        private decimal pointNilai = 0;
        private string target = string.Empty;

        private int idDetail = 0;
        private string sop = string.Empty;
        private int status = 0;
        private string pic = string.Empty;
        private string description = string.Empty;
        private decimal bobotNilai = 0;
        private string ketTargetKe = string.Empty;

        public string KetTargetKe
        {
            get { return ketTargetKe; }
            set { ketTargetKe = value; }
        }
        public decimal BobotNilai
        {
            get { return bobotNilai; }
            set { bobotNilai = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string PIC
        {
            get { return pic; }
            set { pic = value; }
        }
        public int IDDetail
        {
            get { return idDetail; }
            set { idDetail = value; }
        }
        public string SOP
        {
            get { return sop; }
            set { sop = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Target
        {
            get { return target; }
            set { target = value; }
        }
        public int PesType
        {
            get { return pesType; }
            set { pesType = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public string TargetKe
        {
            get { return targetKe; }
            set { targetKe = value; }
        }
        public decimal PointNilai
        {
            get { return pointNilai; }
            set { pointNilai = value; }
        }

    }
}
