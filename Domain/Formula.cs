using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Formula : GRCBaseDomain
    {
        private  int iD  = 0;
	    private string formulaName=string.Empty;
	    private string formulaCode=string.Empty;
        private string formulaDesc = string.Empty;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        public string FormulaName
        {
            get { return formulaName; }
            set { formulaName = value; }
        }
        public string FormulaCode
        {
            get { return formulaCode; }
            set { formulaCode = value; }
        }
        public string FormulaDesc
        {
            get { return formulaDesc; }
            set { formulaDesc = value; }
        }

        public class BmFormula
        {
            public int ID { get; set; }
            public string FormulaName { get; set; }
            public string FormulaCode { get; set; }
            public string FormulaDesc { get; set; }
        }


    }
}
