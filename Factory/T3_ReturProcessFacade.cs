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
    public class T3_ReturProcessFacade 
    {
        private T3_ReturMaster objReturMaster;
        private ArrayList arrRetur;
        private int intReturMasterID =0;
        private string strError = string.Empty;

        public T3_ReturProcessFacade(T3_ReturMaster returMaster, ArrayList ListRetur)
        {
            objReturMaster = returMaster;
            arrRetur = ListRetur;
        }

        public string Insert()
        {
            int intResult = 0;
            int intSerah = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T3_ReturMasterFacade(objReturMaster);
            intReturMasterID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intReturMasterID > 0)
            {
                foreach (T3_Retur t3Retur in arrRetur)
                {
                    intResult = 0;
                    t3Retur.ReturID = intReturMasterID;
                    absTrans = new T3_ReturFacade(t3Retur);
                    if (t3Retur.SJNO.Trim()==string.Empty)
                        return absTrans.Error;
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }

                    absTrans = new T3_ReturFacade(t3Retur);
                    T3_Serah objT3_SerahT = new T3_Serah();
                    objT3_SerahT.Flag = "tambah";
                    objT3_SerahT.Qty = t3Retur.Qty;
                    objT3_SerahT.GroupID = t3Retur.GroupID;
                    objT3_SerahT.ItemID = t3Retur.ItemIDSer;
                    objT3_SerahT.LokID = t3Retur.LokasiID;
                    objT3_SerahT.CreatedBy = t3Retur.CreatedBy;
                    intSerah = 0;
                    intResult = 0;
                    absTrans = new T3_SerahFacade(objT3_SerahT);
                    intSerah = absTrans.Insert(transManager);
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
