using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using BusinessFacade;
using DataAccessLayer;

namespace Cogs
{
    public abstract class AbstractFacade3 
    {
        protected string strError = string.Empty;
        protected DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public AbstractFacade3()
        {
            dataAccess.OpenConnection();
        }

        public abstract int Insert(object objDomain);
        public abstract int Update(object objDomain);
        public abstract int Delete(object objDomain);
        public abstract ArrayList Retrieve();                

        public string Error
        {
            get
            {
                return strError;
            }
            set
            {
                strError = value;
            }
        }

        public void CloseConnection()
        {
            dataAccess.CloseConnection(); 
        }

    }
}