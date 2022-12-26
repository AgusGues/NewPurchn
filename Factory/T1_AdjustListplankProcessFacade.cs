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
    public class T1_AdjustListplankProcessFacade
    {
        private T1_AdjustListplank objAdjust;
        private T1_AdjustListplankDetail objAdjustDetail;
        private ArrayList arrAdjustDetail;
        private int intAdjustID =0;
        private string strError = string.Empty;

        public T1_AdjustListplankProcessFacade(T1_AdjustListplank Adjust, ArrayList AdjustDetail)
        {
            objAdjust = Adjust;
            arrAdjustDetail = AdjustDetail;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T1_AdjustListplankFacade(objAdjust);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                foreach (T1_AdjustListplankDetail t1AdjustDetail in arrAdjustDetail)
                {
                    t1AdjustDetail.AdjustID = intAdjustID;
                    absTrans = new T1_AdjustListplankDetailFacade(t1AdjustDetail);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string Update()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_AdjustListplankDetailFacade(objAdjustDetail);
            foreach (T1_AdjustListplankDetail t1AdjustDetail in arrAdjustDetail)
            {
                if (t1AdjustDetail.Apv > 0)
                {
                    intResult = 0;
                    absTrans = new T1_AdjustListplankDetailFacade(t1AdjustDetail);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
    }
}
