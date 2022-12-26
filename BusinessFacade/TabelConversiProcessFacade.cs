using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

//from AdjustProcessFacade
namespace BusinessFacade
{
    public class TabelConversiProcessFacade
    {
        private TabelConversi objTabelConversi;
        //private ArrayList arrReceiptDetail;
        private string strError = string.Empty;
        private int intAdjustID = 0;

        public TabelConversiProcessFacade(TabelConversi adjust)
        //public TabelConversiProcessFacade(TabelConversi adjust, ArrayList arrListAdjustDetail)
        {
            objTabelConversi = adjust;
            //arrReceiptDetail = arrListAdjustDetail;
        }

        public string ConversiNo
        {
            get
            {
                return intAdjustID.ToString().PadLeft(5, '0');
            }
        }


        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new TabelConversiFacade(objTabelConversi);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                objTabelConversi.ConversiNo = intAdjustID.ToString().PadLeft(5, '0');
                objTabelConversi.ID = intAdjustID;

                absTrans = new TabelConversiFacade(objTabelConversi);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
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
