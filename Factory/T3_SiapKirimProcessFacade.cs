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
    public class T3_SiapKirimProcessFacade
    {
        private T3_SiapKirim objSiapKirim;
        private T3_Serah objT3_SerahK;
        private T3_Serah objT3_SerahT;
        private int intSerah =0;
        private string strError = string.Empty;

        public T3_SiapKirimProcessFacade(T3_Serah T3_SerahK, T3_Serah T3_SerahT, T3_SiapKirim SiapKirim)
        {
            objSiapKirim = SiapKirim;
            objT3_SerahK = T3_SerahK;
            objT3_SerahT = T3_SerahT;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(objT3_SerahK);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            
            intSerah = 0;
            intResult = 0;
            absTrans = new T3_SerahFacade(objT3_SerahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            
            intResult = 0;
            absTrans = new T3_SiapKirimFacade(objSiapKirim);
            intResult = absTrans.Insert(transManager);
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
