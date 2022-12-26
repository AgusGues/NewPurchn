using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TPP_Users : GRCBaseDomain
    {
        private int dept_ID = 0;
        private int approval = 0;
        private int user_ID = 0;
        private string email = string.Empty  ;

        public int Dept_ID { get { return dept_ID; } set { dept_ID = value; } }
        public int Approval { get { return approval; } set { approval = value; } }
        public int User_ID { get { return user_ID; } set { user_ID = value; } }
        public string Email { get { return email; } set { email = value; } }
    }
}
