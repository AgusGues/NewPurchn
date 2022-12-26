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
    public class KasbonApprovalFacade
    {
        private Kasbon objKasbon;
        //private ArrayList arrPOPurchn;
        private SPP objSPP;
     
        private string strError = string.Empty;
        //private int intPOPurchnID = 0;

        public KasbonApprovalFacade(Kasbon kasbon, SPP sPP)
        {
            objKasbon = kasbon;
            objSPP = sPP;
        }

        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new KasbonFacade(objKasbon);

            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            //absTrans = new SPPFacade(objSPP);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string UpdateApprovalKasbon1()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new KasbonFacade(objKasbon);
            KasbonFacade kasbonFacade = new KasbonFacade(objKasbon);

            intResult = kasbonFacade.UpdateApprovKasbon1(transManager);
            if (kasbonFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return kasbonFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string UpdateApprovalKasbon2()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new KasbonFacade(objKasbon);
            KasbonFacade kasbonFacade = new KasbonFacade(objKasbon);

            intResult = kasbonFacade.UpdateApprovKasbon2(transManager);
            if (kasbonFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return kasbonFacade.Error;
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
            //AbstractTransactionFacade absTrans = new POPurchnFacade(objKasbon);
            //intResult = absTrans.Update(transManager);

            KasbonFacade kasbonFacade = new KasbonFacade(objKasbon);
            intResult = kasbonFacade.UpdateStatusNotApproval(transManager);

            if (kasbonFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return kasbonFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


    }
}
