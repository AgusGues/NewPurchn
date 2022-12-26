using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
    public class T3_BAProcessFacade
    {
        private T3_BA objBA;
        private T3_BADetail objBADetail;
        private ArrayList arrBADetail;
        private int intBAID =0;
        private string strError = string.Empty;

        public T3_BAProcessFacade(T3_BA BA, ArrayList BADetail)
        {
            objBA = BA;
            arrBADetail = BADetail;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T3_BAFacade(objBA);
            intBAID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intBAID > 0)
            {
                foreach (T3_BADetail t3BADetail in arrBADetail)
                {
                    t3BADetail.BAID = intBAID;
                    absTrans = new T3_BADetailFacade(t3BADetail);
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
            AbstractTransactionFacadeF absTrans = new T3_BADetailFacade(objBADetail);
            foreach (T3_BADetail t3BADetail in arrBADetail)
            {
                if (t3BADetail.Apv > 0)
                {
                    intResult = 0;
                    absTrans = new T3_BADetailFacade(t3BADetail);
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intResult > 0)
                    {
                        T3_Serah t3serah = new T3_Serah();
                        if (t3BADetail.QtyOut > 0)
                        {
                            t3serah.Flag = "kurang";
                            t3serah.Qty = t3BADetail.QtyOut;

                        }
                        else
                        {
                            t3serah.Flag = "tambah";
                            t3serah.Qty = t3BADetail.QtyIn;
                        }
                        t3serah.GroupID = 0;
                        t3serah.ItemID = t3BADetail.ItemID;
                        t3serah.CreatedBy = t3BADetail.CreatedBy;
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
