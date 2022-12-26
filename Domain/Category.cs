using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Category : GRCBaseDomain
    {
        private int iD = 0;
        private string kategory = string.Empty;
        private string categoryDescription = string.Empty;
        private decimal bobot = 0;
        private int deptID = 0;
        private int sectionID = 0;
        private string typeBobot = string.Empty;
        private string target = string.Empty;

        public string Target
        {
            get { return target; }
            set { target = value; }
        }
        public string TypeBobot
        {
            get { return typeBobot; }
            set { typeBobot = value; }
        }
        public decimal Bobot
        {
            get { return bobot; }
            set { bobot = value; }
        }
        public int DeptID
        {
            get { return deptID; }
            set { deptID = value; }
        }
        public int SectionID
        {
            get { return sectionID; }
            set { sectionID = value; }
        }
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string CategoryDescription
        {
            get { return categoryDescription; }
            set { categoryDescription = value; }
        }
        public string Kategory
        {
            get { return kategory; }
            set { kategory = value; }
        }

        public int CategoryID { get; set; }
    }
}
