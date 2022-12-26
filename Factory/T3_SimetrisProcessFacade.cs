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
    public class T3_SimetrisProcessFacade
    {
        private T3_Simetris objSimetris;
        private T3_Serah objT3_SerahK;
        private T3_Serah objT3_SerahT;
        private T3_Rekap objT3_RekapK;
        private T3_Rekap objT3_RekapT;
        private int intSerah =0;
        private string strError = string.Empty;
        private ArrayList arrT3_SerahT;
        private ArrayList arrT3_RekapT;

        public T3_SimetrisProcessFacade(T3_Serah T3_SerahK, ArrayList  arrSerahT, T3_Rekap RekapK, ArrayList  arrRekapT, T3_Simetris Simetris)
        {
            objSimetris = Simetris;
            objT3_SerahK = T3_SerahK;
            objT3_RekapK = RekapK;
            arrT3_SerahT = arrSerahT;
            arrT3_RekapT = arrRekapT;
        }

        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans  ;
           
            /** rekam simetris*/
            absTrans = new T3_SimetrisFacade(objSimetris);
            intResult = absTrans.Insert(transManager);
            int CutID = intResult;
            objT3_RekapK.CutID = CutID;
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            /** rekam di table t3_rekap item asal*/
            else
            {
                intResult = 0;
                absTrans = new T3_RekapFacade(objT3_RekapK);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                else
                {
                    /* kurangi stock item asal tabel serah*/
                    absTrans = new T3_SerahFacade(objT3_SerahK);
                    intSerah = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    /**
                     * rekam ke table t3_rekap item tujuan
                     */

                    foreach (T3_Rekap T3_RekapT in arrT3_RekapT)
                    {
                        objT3_RekapT = T3_RekapT;
                        objT3_RekapT.CutID = CutID;
                        absTrans = new T3_RekapFacade(objT3_RekapT);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    /**
                    * update stock item tujuan di t3_serah
                    */
                    intSerah = 0;
                    foreach (T3_Serah T3_serahT in arrT3_SerahT)
                    {
                        objT3_SerahT = T3_serahT;
                        absTrans = new T3_SerahFacade(objT3_SerahT);
                        intSerah = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }
            }
            
            //intResult = 0;
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        public string Insert1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans;

            /** rekam simetris*/
            absTrans = new T3_SimetrisFacade(objSimetris);
            intResult = absTrans.Insert(transManager);
            int CutID = intResult;
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            objT3_RekapK.CutID = CutID;
            /** rekam di table t3_rekap item asal*/
            if (intResult > 0)
            {
                intResult = 0;
                absTrans = new T3_RekapFacade(objT3_RekapK);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            /* kurangi stock item asal tabel serah*/
            absTrans = new T3_SerahFacade(objT3_SerahK);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            /**
             * update stock item tujuan di t3_serah
             */
            intSerah = 0;
            //intResult = 0;
            absTrans = new T3_SerahFacade(objT3_SerahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            objT3_RekapT.CutID = CutID;
            /**
             * rekam ke table t3_rekap item tujuan
             */
            if (intSerah > 0)
            {
                absTrans = new T3_RekapFacade(objT3_RekapT);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            intResult = 0;
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

    }
}
