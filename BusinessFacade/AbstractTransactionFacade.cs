using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using System.Collections;


namespace BusinessFacade
{
    public abstract class AbstractTransactionFacade
    {
        protected object objDomain;
        protected string strError = string.Empty;

        public AbstractTransactionFacade(object _objDomain)
        {
            objDomain = _objDomain;
        }

        public AbstractTransactionFacade()
        {
        }

        public string Error
        {
            get
            {
                return strError;
            }
        }


        public abstract int Insert(TransactionManager transManager);
        public abstract int Update(TransactionManager transManager);
        public abstract int Delete(TransactionManager transManager);
        public abstract ArrayList Retrieve();
    }
}
