using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Runtime.InteropServices;

namespace BusinessFacade
{
    public class MTC_SarmutProcesFacade
    {
        private ArrayList arrSarmut;
        private AbstractTransactionFacade absTrans;

        public MTC_SarmutProcesFacade(ArrayList ArrSarmut)
        {
            arrSarmut = ArrSarmut;
        }

        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            if (arrSarmut.Count > 0)
            {
                foreach (MTC_Sarmut mtcSarmut in arrSarmut)
                {
                    absTrans = new MTC_SarmutFacade(mtcSarmut);
                     intResult = absTrans.Insert(transManager);
                     if (absTrans.Error != string.Empty)
                     {
                         transManager.RollbackTransaction();
                         return absTrans.Error;
                     }
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                
            }
            return "ok";
        }
        public string Delete()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            foreach (MTC_Sarmut arSmt in arrSarmut)
            {
                if (arSmt.SarmutID > 0)
                {
                    absTrans = new MTC_SarmutFacade(arSmt);
                    intResult = absTrans.Delete(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return "ok";
        }
    }
}
