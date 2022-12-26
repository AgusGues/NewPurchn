using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Grades : GRCBaseDomain
    {
        private int id = 0;
        private string gradeCode = string.Empty;
        private string gradeDescription = string.Empty;

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

        public string GradeCode
        {
            get
            {
                return gradeCode;
            }
            set
            {
                gradeCode = value;
            }
        }

        public string GradeDescription
        {
            get
            {
                return gradeDescription;
            }
            set
            {
                gradeDescription = value;
            }
        }
    }
}
