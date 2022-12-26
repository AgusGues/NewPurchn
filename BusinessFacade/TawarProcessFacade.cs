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
    public class TawarProcessFacade
    {
        private Tawar objTawar;
        private ArrayList arrPOPurchnDetail;
        private string strError = string.Empty;
        private int intAdjustID = 0;
        //private int intTawarID = 0;

        public TawarProcessFacade(Tawar tawar, ArrayList arrTawarDetail)
        {
            objTawar = tawar;
            arrPOPurchnDetail = arrTawarDetail;

        }

        public string RepackNo
        {
            get
            {
                return intAdjustID.ToString().PadLeft(4, '0') + "/BPAS-PP/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }


        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new TawarFacade(objTawar);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                objTawar.NoPO = intAdjustID.ToString().PadLeft(4, '0') + "/BPAS-PP/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objTawar.ID = intAdjustID;

                absTrans = new TawarFacade(objTawar);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (arrPOPurchnDetail.Count > 0)
                {
                    foreach (POPurchnDetail tawarDetail in arrPOPurchnDetail)
                    {
                        tawarDetail.POID = intAdjustID;
                        absTrans = new TawarDetailFacade(tawarDetail);
                        intResult = absTrans.Insert(transManager);
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

    }
}
