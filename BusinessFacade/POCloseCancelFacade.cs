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
    public class POCloseCancelFacade
    {
        private POPurchn objPOPurchn;
        private string strError = string.Empty;

        public POCloseCancelFacade(POPurchn pOPurchn)
        {
            objPOPurchn = pOPurchn;
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

            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            ArrayList arrPOPurchnDetail = pOPurchnDetailFacade.RetrieveByPOID(objPOPurchn.ID);

            foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
            {
                SPPDetail sppdetail = new SPPDetail();
                sppdetail.ID = pOPurchnDetail.SPPDetailID;
                sppdetail.QtyPO = pOPurchnDetail.Qty;
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade(sppdetail);
                intResult = sPPDetailFacade.MinusQtyPOSPPDetail(transManager);
                if (sPPDetailFacade.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return sPPDetailFacade.Error;
                }
                //update status detail po
                POPurchnDetail pod = new POPurchnDetail();
                pod.ID = pOPurchnDetail.ID;
                pod.Status = -2;
                POPurchnDetailFacade podd = new POPurchnDetailFacade(pod);
                intResult = podd.UpdateStatusDetail(transManager);
                if (podd.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return podd.Error;
                }
            }
            
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

    }
}
