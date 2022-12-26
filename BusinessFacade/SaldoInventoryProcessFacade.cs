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

    public class SaldoInventoryProcessFacade
    {
        private SaldoInventory objSaldoInventory;
        //private ReceiptDocNo objReceiptDocNo;
        //private ArrayList arrSaldoInventory;
        //private string strError = string.Empty;
        //private int intReceiptID = 0;
        private int intReceiptDocNoID = 0;
        //private string bl = string.Empty;
        //private string th = string.Empty;

        public SaldoInventoryProcessFacade(SaldoInventory saldoInv)
        {
            objSaldoInventory = saldoInv;

        }

        public string Insert()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.Insert(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
                {
                    return saldoInventoryFacade.Error;
                }

            transManager.CommitTransaction();
            transManager.CloseConnection();


            return string.Empty;
        }

        public string Update()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.Update(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
            {
                return saldoInventoryFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        public string UpdateSaldoBlnLalu()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.UpdateSaldoBlnLalu(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
            {
                return saldoInventoryFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateSaldoAvgPriceBlnIni()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.UpdateSaldoAvgPriceBlnIni(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
            {
                return saldoInventoryFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Kosongkan()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.KosongkanSaldo(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
            {
                return saldoInventoryFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusSaldo()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            intReceiptDocNoID = saldoInventoryFacade.MinusSaldo(objSaldoInventory);
            if (saldoInventoryFacade.Error != string.Empty)
            {
                return saldoInventoryFacade.Error;
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
            //AbstractTransactionFacade absTrans = new ScheduleFacade(objReceipt);
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
    }
}
