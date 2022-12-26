using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace BusinessFacade
{
    public class POPurchnProcessFacade
    {
        private POPurchn objPOPurchn;
        private SPPNumber objSPPNumber;
        private ArrayList arrPOPurchnDetail;
        private string strError = string.Empty;
        private int intPOPurchnID = 0;
        private string th = string.Empty;
        private string bl = string.Empty;

        public POPurchnProcessFacade(POPurchn pOPurchn, ArrayList arrPOPurchasingDetail,SPPNumber sppNumber)
        {
            objPOPurchn = pOPurchn;
            arrPOPurchnDetail = arrPOPurchasingDetail;
            objSPPNumber = sppNumber;
        }

        public string POPurchnNo
        {
            get
            {
                return objSPPNumber.KodeCompany + objSPPNumber.KodeSPP + th + bl + "-" + objSPPNumber.POCounter.ToString().PadLeft(5, '0');
            }
        }

        public string Insert()
        {
            int intResult = 0;
            ArrayList arrSubCom = new ArrayList();
            POPurchn subCOI = new POPurchn();            
            SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            intResult = sPPNumberFacade.Update(objSPPNumber);
            if (intResult > 0)
            {
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();

                AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
                string updstatus = string.Empty;
                if (objPOPurchn.NoPO != string.Empty)
                    updstatus = "edit";
                else
                    updstatus = "insert";
                if (updstatus == "insert")
                {
                    th = objPOPurchn.POPurchnDate.Year.ToString().Substring(2, 2).PadLeft(2, '0');
                    bl = objPOPurchn.POPurchnDate.Month.ToString().PadLeft(2, '0');

                    objPOPurchn.NoPO = objSPPNumber.KodeCompany + objSPPNumber.KodeSPP + th + bl + "-" + objSPPNumber.POCounter.ToString().PadLeft(5, '0');
                    intPOPurchnID = absTrans.Insert(transManager);
                    subCOI.ID = intPOPurchnID;
                    subCOI.SubCompanyID = objPOPurchn.SubCompanyID;
                    arrSubCom.Add(subCOI);
                }
                else
                {
                    intPOPurchnID = objPOPurchn.POID;
                }
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intPOPurchnID > 0)
                {
                    absTrans = new POPurchnDetailFacade((POPurchnDetail)arrPOPurchnDetail[0]);
                    #region proses delete dihilangkan
                        //intResult = absTrans.Delete(transManager);
                        //if (absTrans.Error != string.Empty)
                        //{
                        //    transManager.RollbackTransaction();
                        //    return absTrans.Error;
                        //}
                    #endregion
                    //until jika ada edit maka ini berlaku di delete dulu
                    int SppStat = 0;
                    int zSPPID = 0;
                        foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                        {
                            pOPurchnDetail.POID = intPOPurchnID;
                            absTrans = new POPurchnDetailFacade(pOPurchnDetail);

                            //ini mlsh
                            intResult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }

                            //Edit di SPPDetail bagian QtyPO
                            SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                            SPPDetail sPPDetail = new SPPDetail();

                            ArrayList arrSPPDetail = new ArrayList();
                            sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(pOPurchnDetail.SPPDetailID);
                            decimal qtyPOPurchn1 = sPPDetail.QtyPO;
                            
                            sPPDetail.QtyPO = qtyPOPurchn1 + Convert.ToDecimal(pOPurchnDetail.Qty);
                            sPPDetail.ID = pOPurchnDetail.SPPDetailID;
                            sPPDetail.ItemID = pOPurchnDetail.ItemID;
                            //untuk pemisahan spp (=2)dan pettycash (=1)
                            sPPDetail.Status = 2;
                            zSPPID = sPPDetail.SPPID;
                            decimal qtySPP = sPPDetail.Quantity;
                            decimal qtyPOPurchn2 = qtyPOPurchn1 + Convert.ToDecimal(pOPurchnDetail.Qty);
                            
                            if (qtySPP == qtyPOPurchn2)
                                SppStat = 2; // Full
                            if (qtySPP > qtyPOPurchn2)
                                SppStat = 1;
                            if (updstatus == "insert")
                            {
                                
                                absTrans = new SPPDetailFacade(sPPDetail);
                                intResult = absTrans.Update(transManager);
                               
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return absTrans.Error;
                                }
                            }
                        }

                        SPPFacade sPPFacade = new SPPFacade();
                        SPP sPP = new SPP();
                        sPP = sPPFacade.RetrieveById(zSPPID);
                        sPP.Status = SppStat;
                       
                        sPPFacade = new SPPFacade(sPP);
                        intResult = sPPFacade.UpdateStatusSPP(transManager);
                        if (sPPFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return sPPFacade.Error;
                        }
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                if (arrSubCom.Count > 0)
                {
                    POPurchn sub = new POPurchn();
                    foreach (POPurchn po in arrSubCom)
                    {
                        sub.ID = po.ID;
                        sub.SubCompanyID = po.SubCompanyID;
                        int rst = new POPurchnFacade().UpdateSubCompanyID(sub);
                    }
                }
            }
            return string.Empty;
        }

        public string Reprint()
        {
            int intResult = 0;
            objPOPurchn.Cetak = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
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
            AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                if (arrPOPurchnDetail.Count > 0)
                {
                    #region
                    //absTrans = new POPurchnDetailFacade((POPurchnDetail)arrPOPurchnDetail[0]);
                    //intResult = absTrans.Delete(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}
                    #endregion
                    foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                    {
                        pOPurchnDetail.POID = objPOPurchn.ID;
                        absTrans = new POPurchnDetailFacade(pOPurchnDetail);
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
        /** added for adopt spp biaya new*/
        public string Update2()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                if (arrPOPurchnDetail.Count > 0)
                {
                    //absTrans = new POPurchnDetailFacade((POPurchnDetail)arrPOPurchnDetail[0]);
                    //intResult = absTrans.Delete(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}

                    foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                    {
                        pOPurchnDetail.POID = objPOPurchn.ID;
                        absTrans = new POPurchnDetailFacade(pOPurchnDetail);
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

        public string UpdateCetakPOPurchn()
        {
            int intResult = 0;
            objPOPurchn.Cetak = 1;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new POPurchnFacade(objPOPurchn);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            if (intResult > 0)
            {
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            else
                transManager.RollbackTransaction();

            return string.Empty;
        }
        public int CheckSPPBiaya()
        {
            AccClosingFacade bn = new AccClosingFacade();
            AccClosing oBn = bn.CheckBiayaAktif();
            return oBn.RowStatus;
        }
        public int CheckActiveBiayaNew(DateTime spp)
        {
            AccClosingFacade bn = new AccClosingFacade();
            AccClosing oBn = bn.BiayaNewActive();
            
            return (oBn.CreatedTime<=spp && CheckSPPBiaya()==1)? 1:0;
        }
    }
}
