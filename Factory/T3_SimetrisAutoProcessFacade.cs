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
    public class T3_SimetrisAutoProcessFacade
    {
        private T3_Simetris objSimetris;
        private ArrayList arrT3_RekapK;
        private ArrayList arrT3_RekapT;
        private int intSerah = 0;
        private string strError = string.Empty;
        public T3_SimetrisAutoProcessFacade(ArrayList RekapK, ArrayList RekapT, T3_Simetris Simetris)
        {
            objSimetris = Simetris;
            arrT3_RekapK = RekapK;
            arrT3_RekapT = RekapT;
        }

        public string Insert1()
        {
            int intResult = 0;
            int SAK = 0;
            int SAT = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTranssimetris;
            AbstractTransactionFacadeF absTrans;
            AbstractTransactionFacadeF absTrans0;
            int IDsimetris = 0;
            /** rekam simetris*/
            absTranssimetris = new T3_SimetrisFacade(objSimetris);
            intResult = absTranssimetris.Insert(transManager);
            int CutID = intResult;
            if (absTranssimetris.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTranssimetris.Error;
            }
            IDsimetris = intResult;
            T3_SerahFacade t3serahKF = new T3_SerahFacade();
            T3_Serah t3serahK = new T3_Serah();
            t3serahK = t3serahKF.RetrieveByID(objSimetris.SerahID);
            SAK = t3serahK.Qty;

            T3_SerahFacade t3serahTF = new T3_SerahFacade();
            T3_Serah t3serahT = new T3_Serah();
            t3serahT = t3serahTF.RetrieveStockByLokIDnItemID(objSimetris.LokasiID, objSimetris.ItemID);
            SAT = t3serahT.Qty;
            foreach (T3_Rekap rekapK in arrT3_RekapK)
            {
                /* kurangi stock item asal tabel serah*/
                t3serahK.Flag = "kurang";
                t3serahK.Qty = rekapK.QtyOutTrm;
                t3serahK.CreatedBy = rekapK.CreatedBy;
                absTrans0 = new T3_SerahFacade(t3serahK);
                intSerah = absTrans0.Insert(transManager);
                if (absTrans0.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans0.Error;
                }
                rekapK.CutID = IDsimetris;
                rekapK.SA = SAK;
                /** rekam di table t3_rekap item asal*/
                if (IDsimetris > 0)
                {
                    intResult = 0;
                    absTrans = new T3_RekapFacade(rekapK);
                    intResult = absTrans.Insert1(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                SAK = SAK - t3serahK.Qty;
            }

            foreach (T3_Rekap rekapT in arrT3_RekapT)
            {
                intSerah = 0;
                //update stock item tujuan di t3_serah
                t3serahT.Flag = "tambah";
                t3serahT.Qty = rekapT.QtyInTrm;
                t3serahT.HPP = rekapT.HPP;
                t3serahT.CreatedBy = rekapT.CreatedBy;
                absTrans = new T3_SerahFacade(t3serahT);
                intSerah = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (IDsimetris > 0)
                {
                    rekapT.SA = SAT;
                    rekapT.CutID = IDsimetris;
                    absTrans = new T3_RekapFacade(rekapT);
                    intResult = absTrans.Insert1(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                SAT = SAT + t3serahT.Qty;
            }
            intResult = 0;
            transManager.CommitTransaction();
            transManager.CloseConnection();
            T3_RekapFacade t3rekapF = new T3_RekapFacade();
            foreach (T3_Rekap rekapK in arrT3_RekapK)
            {
                t3rekapF.UpdateCutLevel2(rekapK.ID, rekapK.QtyOutTrm);
            }
            return string.Empty;
        }
    }
}
