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
    public class SaldoInventoryBJProcessFacade
    {
        private SaldoInventoryBJ objSaldoInventoryBJ;
        private int intReceiptDocNoID = 0;

        public SaldoInventoryBJProcessFacade(SaldoInventoryBJ saldoInv)
        {
            objSaldoInventoryBJ = saldoInv;

        }

        public string Insert()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.Insert(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
                {
                    return SaldoInventoryBJFacade.Error;
                }

            transManager.CommitTransaction();
            transManager.CloseConnection();


            return string.Empty;
        }

        public string Update()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.Update(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
            {
                return SaldoInventoryBJFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        public string UpdateSaldoBlnLalu()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.UpdateSaldoBlnLalu(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
            {
                return SaldoInventoryBJFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateSaldoAvgPriceBlnIni()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.UpdateSaldoAvgPriceBlnIni(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
            {
                return SaldoInventoryBJFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Kosongkan()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.KosongkanSaldo(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
            {
                return SaldoInventoryBJFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusSaldo()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryBJFacade SaldoInventoryBJFacade = new SaldoInventoryBJFacade();
            intReceiptDocNoID = SaldoInventoryBJFacade.MinusSaldo(objSaldoInventoryBJ);
            if (SaldoInventoryBJFacade.Error != string.Empty)
            {
                return SaldoInventoryBJFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Delete()
        {
            return string.Empty;
        }
    }
}
