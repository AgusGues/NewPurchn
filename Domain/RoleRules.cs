using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RoleRules : GRCBaseDomain
    {
        private int id = 0;
        private int roleID = 0;
        private string roleName = string.Empty;
        private int ruleID = 0;
        private string ruleName = string.Empty;

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

        public int RoleID
        {
            get
            {
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }

        public string RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public int RuleID
        {
            get
            {
                return ruleID;
            }
            set
            {
                ruleID = value;
            }
        }

        public string RuleName
        {
            get
            {
                return ruleName;
            }
            set
            {
                ruleName = value;
            }
        }


    }
}
