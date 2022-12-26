using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //class GL_JournalAutoGrid
    //{
    //}
    public class GL_JournalAutoGrid : GRCBaseDomain
    {
        private string journalDebetCredit = string.Empty;
        private string chartNo = string.Empty;
        public string JournalDebetCredit
        {
            get { return journalDebetCredit; }
            set { journalDebetCredit = value; }
        }
        public string ChartNo
        {
            get { return chartNo; }
            set { chartNo = value; }
        }

    }


}
