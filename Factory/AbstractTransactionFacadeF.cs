using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using System.Collections;


namespace BusinessFacade
{
    public abstract class AbstractTransactionFacadeF
    {
        protected object objDomain;
        protected string strError = string.Empty;

        public AbstractTransactionFacadeF(object _objDomain)
        {
            objDomain = _objDomain;
        }

        public AbstractTransactionFacadeF()
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
        public abstract int Insert1(TransactionManager transManager);
        public abstract int Update1(TransactionManager transManager);
        public abstract int Update2(TransactionManager transManager);
        public abstract int Delete(TransactionManager transManager);
        public abstract ArrayList Retrieve();
    }
}
