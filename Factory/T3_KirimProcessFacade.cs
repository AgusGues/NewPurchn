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
    public class T3_KirimProcessFacade
    {
        private T3_Kirim objKirim;
        //private T3_Serah objT3_SerahK;
        private ArrayList arrKirimDetail;
        private int intKirimID =0;
        private string strError = string.Empty;

        public T3_KirimProcessFacade(T3_Kirim Kirim,ArrayList KirimDetail)
        {
            objKirim = Kirim;
            objKirim = Kirim;
            arrKirimDetail = KirimDetail;
        }

        public string Insert()
        {
            int intResult = 0;
            
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T3_KirimFacade(objKirim);
            intKirimID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intKirimID > 0)
            {
                foreach (T3_KirimDetail t3KirimDetail in arrKirimDetail)
                {
                    //intResult = 0;
                    
                    //T3_SiapKirimFacade siapKFacade = new T3_SiapKirimFacade();
                    //siapK = siapKFacade.RetrieveByID(t3KirimDetail.T3siapKirimID);
                    T3_Serah objT3_SerahK = new T3_Serah();
                    objT3_SerahK.Flag = "kurang";
                    objT3_SerahK.ItemID = t3KirimDetail.ItemIDSer;
                    objT3_SerahK.GroupID = t3KirimDetail.GroupID;
                    objT3_SerahK.ID = t3KirimDetail.SerahID;
                    objT3_SerahK.LokID = t3KirimDetail.LokasiLoadingID ;
                    objT3_SerahK.Qty = t3KirimDetail.Qty;
                    objT3_SerahK.CreatedBy = t3KirimDetail.CreatedBy;
                    absTrans = new T3_SerahFacade(objT3_SerahK);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intResult > 0)
                    {
                        t3KirimDetail.KirimID = intKirimID;
                        absTrans = new T3_KirimDetailFacade(t3KirimDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        T3_SiapKirim siapK = new T3_SiapKirim();
                        absTrans = new T3_SiapKirimFacade(siapK);
                        siapK.ID = t3KirimDetail.T3siapKirimID;
                        siapK.Qty = t3KirimDetail.Qty;
                        siapK.LastModifiedBy = t3KirimDetail.CreatedBy;
                        intResult = absTrans.Update(transManager);
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
        public string Insert1()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new T3_KirimFacade(objKirim);
            intKirimID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intKirimID > 0)
            {
                foreach (T3_KirimDetail t3KirimDetail in arrKirimDetail)
                {
                    //intResult = 0;

                    //T3_SiapKirimFacade siapKFacade = new T3_SiapKirimFacade();
                    //siapK = siapKFacade.RetrieveByID(t3KirimDetail.T3siapKirimID);
                    T3_Serah objT3_SerahK = new T3_Serah();
                    objT3_SerahK.Flag = "kurang";
                    objT3_SerahK.ItemID = t3KirimDetail.ItemIDSer;
                    objT3_SerahK.GroupID = t3KirimDetail.GroupID;
                    objT3_SerahK.ID = t3KirimDetail.SerahID;
                    objT3_SerahK.LokID = t3KirimDetail.LokasiLoadingID;
                    objT3_SerahK.Qty = t3KirimDetail.Qty;
                    objT3_SerahK.CreatedBy = t3KirimDetail.CreatedBy;
                    absTrans = new T3_SerahFacade(objT3_SerahK);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intResult > 0)
                    {
                        t3KirimDetail.KirimID = intKirimID;
                        absTrans = new T3_KirimDetailFacade(t3KirimDetail);
                        intResult = absTrans.Insert1(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        T3_SiapKirim siapK = new T3_SiapKirim();
                        absTrans = new T3_SiapKirimFacade(siapK);
                        siapK.ID = t3KirimDetail.T3siapKirimID;
                        siapK.Qty = t3KirimDetail.Qty;
                        siapK.LastModifiedBy = t3KirimDetail.CreatedBy;
                        intResult = absTrans.Update(transManager);
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
