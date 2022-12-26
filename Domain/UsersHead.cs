using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class UsersHead : GRCBaseDomain
    {
        private int userID = 0;
        private string userName = string.Empty;
        private int headID = 0;
        private string headName = string.Empty;
        private int managerID = 0;
        private string managerName = string.Empty;
        private string keterangan = string.Empty;

        public int HeadID
        {
            get { return headID; }
            set { headID = value; }
        }
        public string HeadName
        {
            get { return headName; }
            set { headName = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
        }
        public string ManagerName
        {
            get { return ManagerName; }
            set { managerName = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }


    }

}
