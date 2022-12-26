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
    public class T3_AdjustProcessFacade
    {
        private T3_Adjust objAdjust;
        private T3_AdjustDetail objAdjustDetail;
        private ArrayList arrAdjustDetail;
        private int intAdjustID =0;
        private string strError = string.Empty;

        public T3_AdjustProcessFacade(T3_Adjust Adjust, ArrayList AdjustDetail)
        {
            objAdjust = Adjust;
            arrAdjustDetail = AdjustDetail;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T3_AdjustFacade(objAdjust);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                foreach (T3_AdjustDetail t3AdjustDetail in arrAdjustDetail)
                {
                    t3AdjustDetail.AdjustID = intAdjustID;
                    absTrans = new T3_AdjustDetailFacade(t3AdjustDetail);
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
            AbstractTransactionFacadeF absTrans = new T3_AdjustDetailFacade(objAdjustDetail);
            foreach (T3_AdjustDetail t3AdjustDetail in arrAdjustDetail)
            {
                if (t3AdjustDetail.Apv > 0)
                {
                    intResult = 0;
                    absTrans = new T3_AdjustDetailFacade(t3AdjustDetail);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intResult > 0)
                    {
                        T3_Serah t3serah = new T3_Serah();
                        if (t3AdjustDetail.QtyOut > 0)
                        {
                            t3serah.Flag = "kurang";
                            t3serah.Qty = t3AdjustDetail.QtyOut;

                        }
                        else
                        {
                            t3serah.Flag = "tambah";
                            t3serah.Qty = t3AdjustDetail.QtyIn;
                        }
                        t3serah.GroupID = 0;
                        t3serah.ItemID = t3AdjustDetail.ItemID;
                        t3serah.LokID = t3AdjustDetail.LokID;
                        t3serah.CreatedBy = t3AdjustDetail.CreatedBy;
                        absTrans = new T3_SerahFacade(t3serah);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
    }
}
