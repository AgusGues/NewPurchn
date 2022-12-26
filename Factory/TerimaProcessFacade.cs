using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using DataAccessLayer;
using System.Collections;
using BusinessFacade;
namespace Factory
{
    public class TerimaProcessFacade
    {
        private T3_Serah objT3_SerahT;
        private T3_Rekap objT3_RekapT;
        private int intSerah =0;
        private string strError = string.Empty;

        public TerimaProcessFacade(T3_Serah T3_SerahT,T3_Rekap RekapT)
        {
            objT3_SerahT = T3_SerahT;
            objT3_RekapT = RekapT;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            intSerah = 0;
            intResult = 0;
            AbstractTransactionFacadeF absTrans =  new T3_SerahFacade(objT3_SerahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intSerah > 0)
            {
                objT3_RekapT.SerahID = intSerah;
                absTrans = new T3_RekapFacade(objT3_RekapT);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            T3_RekapFacade rekapf = new T3_RekapFacade();
            return string.Empty;
        }

        public string Insert1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            intSerah = 0;
            intResult = 0;
            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(objT3_SerahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intSerah > 0)
            {
                objT3_RekapT.SerahID = intSerah;
                absTrans = new T3_RekapFacade(objT3_RekapT);
                intResult = absTrans.Insert1(transManager);
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
        public string Insertlistplank()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            intSerah = 0;
            intResult = 0;
            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(objT3_SerahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intSerah > 0)
            {
                objT3_RekapT.SerahID = intSerah;
                absTrans = new T3_RekapFacade(objT3_RekapT);
                intResult = absTrans.Update(transManager);
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
