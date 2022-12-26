using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Roles : GRCBaseDomain
    {
        private string rolesName = string.Empty;

        public string RolesName
        {
            get
            {
                return rolesName;
            }
            set
            {
                rolesName = value;
            }
        }
    }
}
