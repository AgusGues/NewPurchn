using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class HelpDeskCategory : GRCBaseDomain
    {
        private string helpCategory = string.Empty;
        private string description = string.Empty;

        public string HelpCategory
        {
            get
            {
                return helpCategory;
            }
            set
            {
                helpCategory = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
    }

}
