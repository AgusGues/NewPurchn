using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace Factory
{
    public class RMMProsesFacade
    {
        private RMM objRMM;
        private RMM objRMM11;
        private ArrayList arrRMM;
        private string strErorr = string.Empty;
        public RMMProsesFacade(RMM rmm)
        {
            objRMM = rmm;            
        }

        public string Update1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans;

            absTrans = new RMMFacade(objRMM);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

    }
}
