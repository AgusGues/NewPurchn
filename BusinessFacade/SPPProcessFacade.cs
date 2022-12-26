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
    public class SPPProcessFacade : AbstractTransactionFacade
    {
        private SPP objSPP;
        private ArrayList arrSPPDetail;
        //private string strError = string.Empty;
        private int intSPPID = 0;
        private string th = string.Empty;
        private string bl = string.Empty;

        private SPPNumber objSPPNumber;
        //private Users objUser;
        //private Company objCompany;
        //private GroupsPurchn objGroupPurchn;

        public SPPProcessFacade(SPP sPP, ArrayList arrListSPP, SPPNumber sPPNumber)
        {

            objSPP = sPP;
            arrSPPDetail = arrListSPP;
            objSPPNumber = sPPNumber;
        }

        public string SPPNo
        {
            get
            {
                return objSPPNumber.KodeCompany + objSPPNumber.KodeSPP + th + bl + "-" + objSPPNumber.SPPCounter.ToString().PadLeft(4, '0');
            }
        }

        public string Insert()
        {
            int intResult = 0;

            //update no urut spp dulu
            SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            intResult = sPPNumberFacade.Update(objSPPNumber);
            if (intResult > 0)
            {
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();

                AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
                intSPPID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intSPPID > 0)
                {
                    objSPP.ID = intSPPID;
                    th = objSPP.TglBuat.Year.ToString().Substring(2, 2).PadLeft(2,'0');
                    bl = objSPP.TglBuat.Month.ToString().PadLeft(2, '0');
                    objSPP.NoSPP = objSPPNumber.KodeCompany + objSPPNumber.KodeSPP + th + bl+  "-" + objSPPNumber.SPPCounter.ToString().PadLeft(4, '0');

                    absTrans = new SPPFacade(objSPP);

                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (objSPP.SatuanID == 2)//flag untuk system multigudang
                    {
                        SPPPrivate sppprivate = new SPPPrivate();
                        sppprivate.SPPID = objSPP.ID;
                        absTrans = new SPPPrivateFacade(sppprivate);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                    if (arrSPPDetail.Count > 0)
                    {
                        foreach (SPPDetail sPPDetail in arrSPPDetail)
                        {
                            sPPDetail.SPPID = intSPPID;
                            absTrans = new SPPDetailFacade(sPPDetail);
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
            }

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }



        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intResult > 0)
            {
                if (arrSPPDetail.Count > 0)
                {
                    #region depreciated
                    //absTrans = new SPPDetailFacade((SPPDetail)arrSPPDetail[0]);
                    //intResult = absTrans.Delete(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}

                    //foreach (SPPDetail sPPDetail in arrSPPDetail)
                    //{
                    //    sPPDetail.SPPID = objSPP.ID;
                    //    sPPDetail.Status  =objSPP.Status;
                    //    absTrans = new SPPDetailFacade(sPPDetail);
                    //    intResult = absTrans.Insert(transManager);
                    //    if (absTrans.Error != string.Empty)
                    //    {
                    //        transManager.RollbackTransaction();
                    //        return absTrans.Error;
                    //    }
                    //}
                    #endregion
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        public string Delete()
        {
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new SuratJalanFacade(objSPP);
            //intResult = absTrans.Delete(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

           return string.Empty;
        }

        public string CancelSPPDetail()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
            SPPDetail sppDetail = new SPPDetail();
            foreach (SPPDetail sDetail in arrSPPDetail)
            {
                sppDetail.ID = sDetail.ID;
            }

            sppDetailFacade = new SPPDetailFacade(sppDetail);

            intResult = sppDetailFacade.CancelSPPDetail(transManager);
            if (sppDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return sppDetailFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusQtyPO()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
            SPPDetail sppDetail = new SPPDetail();
            foreach (SPPDetail sDetail in arrSPPDetail)
            {
                sppDetail.ID = sDetail.ID;
                sppDetail.QtyPO = sDetail.QtyPO;
            }

            sppDetailFacade = new SPPDetailFacade(sppDetail);

            intResult = sppDetailFacade.MinusQtyPOSPPDetail(transManager);
            if (sppDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return sppDetailFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        public string CancelSPP()
        {
            int intResult = 0;
            objSPP.Status = -1;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
            SPPFacade sppFacade = new SPPFacade();

            absTrans = new SPPFacade(objSPP);
            intResult = absTrans.Delete(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            CancelSPPDetail();

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CloseSPP()
        {
            int intResult = 0;
            objSPP.Status = -2;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
            SPPFacade sppFacade = new SPPFacade();

            absTrans = new SPPFacade(objSPP);
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


        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        
    }
}
