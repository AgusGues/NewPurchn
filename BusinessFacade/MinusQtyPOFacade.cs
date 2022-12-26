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
    public class MinusQtyPOFacade
    {
        //private SPP objSPP;
        private SPPDetail objSPPDetail;
        private POPurchnDetail objPOPurchnDetail;
        //private ArrayList arrSPPDetail;
        private string strError = string.Empty;
        
        //private SPPNumber objSPPNumber;
        //private Users objUser;
        //private Company objCompany;
        //private GroupsPurchn objGroupPurchn;

        public MinusQtyPOFacade(SPPDetail sPPDetail, POPurchnDetail pOPurchnDetail)
        {

            objSPPDetail = sPPDetail;
            objPOPurchnDetail = pOPurchnDetail;
        }

        
        public string Insert()
        {
            //int intResult = 0;

            ////update no urut spp dulu
            //SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            //intResult = sPPNumberFacade.Update(objSPPNumber);
            //if (intResult > 0)
            //{
            //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //    transManager.BeginTransaction();

            //    AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
            //    intSPPID = absTrans.Insert(transManager);
            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return absTrans.Error;
            //    }
            //    if (intSPPID > 0)
            //    {
            //        objSPP.ID = intSPPID;
            //        th = objSPP.TglBuat.Year.ToString().Substring(2, 2).PadLeft(2, '0');
            //        bl = objSPP.TglBuat.Month.ToString().PadLeft(2, '0');
            //        objSPP.NoSPP = objSPPNumber.KodeCompany + objSPPNumber.KodeSPP + th + bl + "-" + objSPPNumber.SPPCounter.ToString().PadLeft(4, '0');

            //        absTrans = new SPPFacade(objSPP);

            //        intResult = absTrans.Update(transManager);
            //        if (absTrans.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return absTrans.Error;
            //        }

            //        if (arrSPPDetail.Count > 0)
            //        {
            //            absTrans = new SPPDetailFacade((SPPDetail)arrSPPDetail[0]);
            //            intResult = absTrans.Delete(transManager);
            //            if (absTrans.Error != string.Empty)
            //            {
            //                transManager.RollbackTransaction();
            //                return absTrans.Error;
            //            }

            //            foreach (SPPDetail sPPDetail in arrSPPDetail)
            //            {
            //                sPPDetail.SPPID = intSPPID;
            //                absTrans = new SPPDetailFacade(sPPDetail);
            //                intResult = absTrans.Insert(transManager);
            //                if (absTrans.Error != string.Empty)
            //                {
            //                    transManager.RollbackTransaction();
            //                    return absTrans.Error;
            //                }
            //            }
            //        }
            //    }

            //    transManager.CommitTransaction();
            //    transManager.CloseConnection();
            //}

            ////transManager.CommitTransaction();
            ////transManager.CloseConnection();

            return string.Empty;
        }



        public string Update()
        {
            //int intResult = 0;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new SPPFacade(objSPP);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}
            //if (intResult > 0)
            //{
            //    if (arrSPPDetail.Count > 0)
            //    {
            //        absTrans = new SPPDetailFacade((SPPDetail)arrSPPDetail[0]);
            //        intResult = absTrans.Delete(transManager);
            //        if (absTrans.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return absTrans.Error;
            //        }

            //        foreach (SPPDetail sPPDetail in arrSPPDetail)
            //        {
            //            sPPDetail.SPPID = objSPP.ID;
            //            absTrans = new SPPDetailFacade(sPPDetail);
            //            intResult = absTrans.Insert(transManager);
            //            if (absTrans.Error != string.Empty)
            //            {
            //                transManager.RollbackTransaction();
            //                return absTrans.Error;
            //            }
            //        }
            //    }
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusQtyPO()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SPPDetailFacade sppDetailFacade = new SPPDetailFacade();
            sppDetailFacade = new SPPDetailFacade(objSPPDetail);

            intResult = sppDetailFacade.MinusQtyPOSPPDetail(transManager);
            if (sppDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return sppDetailFacade.Error;
            }

            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            pOPurchnDetailFacade = new POPurchnDetailFacade(objPOPurchnDetail);
            intResult = pOPurchnDetailFacade.UpdateStatusDetail(transManager);
            if (pOPurchnDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return pOPurchnDetailFacade.Error;
            }


            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        

       
    }
}
