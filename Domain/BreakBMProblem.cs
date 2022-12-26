using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class BreakBMProblem : GRCBaseDomain
    {
        private string lokasiProblem = string.Empty;

        public string LokasiProblem
        {
            get
            {
                return lokasiProblem;
            }
            set
            {
                lokasiProblem = value;
            }
        }
    }
}
