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
    public class T3_AsimetrisAutoProcessFacade
    {
        private T3_Serah objT3_SerahK;
        private T3_Rekap objT3_RekapK;
        private T3_Rekap objT3_RekapT;
        private ArrayList arrT3_RekapK;
        private ArrayList arrT3_RekapT;
        private ArrayList arrAsimetris;
        private T3_Asimetris  objSimetris;
        private int intSerah =0;
        private string strError = string.Empty;
        private string DocNo;
        private DateTime Tgl;
        private string Createdby;

        public T3_AsimetrisAutoProcessFacade(T3_Serah T3_SerahK, ArrayList arrListAsimetris, ArrayList RekapK, ArrayList rekapT, string docNo, DateTime tgl, string createdby)
        {
            objT3_SerahK = T3_SerahK;
            arrT3_RekapK = RekapK;
            arrT3_RekapT = rekapT;
            arrAsimetris = arrListAsimetris;
            DocNo=docNo;
            Tgl=tgl;
            Createdby = createdby;
        }

        public string Insert()
        {
            int intResult = 0;
            int IDsimetris = 0;
            int SAK = 0;
            int SAT = 0;
            int itemID = 0;
            int lokid = 0;
            int i = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(objT3_SerahK);
            foreach (T3_Asimetris asimetris in arrAsimetris)
            {
                asimetris.DocNo = DocNo;
                asimetris.CreatedBy = Createdby;
                absTrans = new T3_AsimetrisFacade(asimetris);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            IDsimetris = intResult;
            T3_SerahFacade t3serahKF = new T3_SerahFacade();
            T3_Serah t3serahK = new T3_Serah();
            t3serahK = t3serahKF.RetrieveByID(intResult);
            SAK = t3serahK.Qty;
            foreach (T3_Rekap rekapK in arrT3_RekapK)
            {
                /* kurangi stock item asal tabel serah*/
                t3serahK.Flag = "kurang";
                t3serahK.Qty = rekapK.QtyOutTrm;
                t3serahK.CreatedBy = rekapK.CreatedBy;
                absTrans = new T3_SerahFacade(t3serahK);
                intSerah = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
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

            SAT = 0;
            T3_SerahFacade t3serahTF = new T3_SerahFacade();
            T3_Serah t3serahT = new T3_Serah();
            
            intSerah = 0;
            i = 0;
            foreach (T3_Rekap rekapT in arrT3_RekapT)
            {
                intSerah = 0;
                if (i == 0)
                {
                    t3serahT = t3serahTF.RetrieveStockByLokIDnItemID(rekapT.ItemIDSer, rekapT.ItemIDSer );
                    SAT = t3serahT.Qty;
                }
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
                i++;
            }
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
