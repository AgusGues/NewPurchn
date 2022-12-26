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
    public class TPP_ProcessFacade
    {
        private TPP objTPP;
        private TPP objTPP1;
        private ArrayList arrTPP;
        private string strError = string.Empty;

        public TPP_ProcessFacade(TPP tpp)
        {
            objTPP = tpp;            
        }

        public string Update1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans;

            absTrans = new TPP_Facade(objTPP);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            //if (intResult > 0)
            //{
            //    intResult = 0;
            //    absTrans = new TPP_Facade(objTPP);
            //    intResult = absTrans.Insert(transManager);
            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return absTrans.Error;
            //    }
            //}

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Update2()
        {
            int intResult = 0;
            int rekom = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans;
            absTrans = new TPP_Facade(objTPP);
            intResult = absTrans.Insert1(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            rekom = intResult;
            if (intResult > 0)
            {
                //TPP tppdd = new TPP();

                objTPP.RekomID = rekom;
                intResult = 0;

                absTrans = new TPP_Facade(objTPP);
                intResult = absTrans.Update1(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
    }
}
