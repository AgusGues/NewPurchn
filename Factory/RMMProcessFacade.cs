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
    public class RMMProcessFacade //: BusinessFacade.AbstractTransactionFacadeF//AbstractTransactionFacadeF
    {
        private RMM objRMM;
        private ArrayList arrRMMDetail;
        private string strError = string.Empty;
        private int intRMM_ID = 0;

        public RMMProcessFacade(RMM rmm, ArrayList arrListRMMDetail)
        {
            objRMM = rmm;
            arrRMMDetail = arrListRMMDetail;
        }


        public string RMM_No
        {
            get
            {
                return intRMM_ID.ToString().PadLeft(3, '0') + "/RMM/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }

        public string Insert()
        {
            int IntResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacadeF absTrans = new RMMFacade(objRMM);
            intRMM_ID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intRMM_ID > 0 )
            {
                objRMM.RMM_No = intRMM_ID.ToString().PadLeft(3 , '0') + "/RMM/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objRMM.ID = intRMM_ID;

                absTrans = new RMMFacade(objRMM);
                IntResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (arrRMMDetail.Count > 0)
                {
                    foreach (RMM_Detail rmm_Detail in arrRMMDetail)
                    {
                        rmm_Detail.RMM_ID = intRMM_ID;
                        absTrans = new RMMDetailFacade(rmm_Detail);

                        IntResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }

            }
            else
            {
                transManager.RollbackTransaction();
                return "error";
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Update()
        {
            //int intResult = 0;

            return string.Empty;
        }

        public string CancelRMMDetail()
        {
            return string.Empty;
        }

        public string CancelRMM()
        {
            return string.Empty;
        }

        //public override int Insert(TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Update(TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Delete(TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override ArrayList Retrieve()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
