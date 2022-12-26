using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DataAccessLayer;

namespace BusinessFacade
{
    public abstract class AbstractFacade2
    {
        protected string strError = string.Empty;
        protected DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public AbstractFacade2()
        {
            dataAccess.OpenConnection();
        }

        public abstract int Insert(object objDomain,string TableName);
        public abstract int Update(object objDomain,string TableName);
        public abstract int Delete(object objDomain,string TableName);
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
