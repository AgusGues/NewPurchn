using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace BusinessFacade
{
    public class POApprovalStatusFacade
    {
        private POPurchn objPOPurchn;
        //private ArrayList arrPOPurchn;
        private SPP objSPP;
     
        private string strError = string.Empty;
        //private int intPOPurchnID = 0;

        public POApprovalStatusFacade(POPurchn pOPurchn, SPP sPP)
        {
            objPOPurchn = pOPurchn;
            objSPP = sPP;
        }

        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);

            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            
                absTrans = new SPPFacade(objSPP);
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

        public string UpdateNotApproval()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            //intResult = absTrans.Update(transManager);

            POPurchnFacade poPurchnFacade = new POPurchnFacade(objPOPurchn);
            intResult = poPurchnFacade.UpdateStatusNotApproval(transManager);

            if (poPurchnFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return poPurchnFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprovalPO1()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            //intResult = absTrans.Update(transManager);

            POPurchnFacade poPurchnFacade = new POPurchnFacade(objPOPurchn);
            intResult = poPurchnFacade.UpdateApprovePO1(transManager);

            if (poPurchnFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return poPurchnFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprovalPO2()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            //intResult = absTrans.Update(transManager);

            POPurchnFacade poPurchnFacade = new POPurchnFacade(objPOPurchn);
            intResult = poPurchnFacade.UpdateApprovePO2(transManager);

            if (poPurchnFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return poPurchnFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprovalPO3()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            //intResult = absTrans.Update(transManager);

            POPurchnFacade poPurchnFacade = new POPurchnFacade(objPOPurchn);
            intResult = poPurchnFacade.UpdateApprovePO3(transManager);

            if (poPurchnFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return poPurchnFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprovalPO4()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            //intResult = absTrans.Update(transManager);

            POPurchnFacade poPurchnFacade = new POPurchnFacade(objPOPurchn);
            intResult = poPurchnFacade.UpdateApprovePO4(transManager);

            if (poPurchnFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return poPurchnFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


    }
}
