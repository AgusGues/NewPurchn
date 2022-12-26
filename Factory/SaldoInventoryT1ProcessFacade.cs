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
    public class SaldoInventoryT1ProcessFacade
    {
        private SaldoInventoryT1 objSaldoInventoryT1;
        private int intReceiptDocNoID = 0;

        public SaldoInventoryT1ProcessFacade(SaldoInventoryT1 saldoInv)
        {
            objSaldoInventoryT1 = saldoInv;

        }

        public string Insert()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.Insert(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
                {
                    return SaldoInventoryT1Facade.Error;
                }

            transManager.CommitTransaction();
            transManager.CloseConnection();


            return string.Empty;
        }

        public string Update()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.Update(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
            {
                return SaldoInventoryT1Facade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }

        public string UpdateSaldoBlnLalu()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.UpdateSaldoBlnLalu(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
            {
                return SaldoInventoryT1Facade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateSaldoAvgPriceBlnIni()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.UpdateSaldoAvgPriceBlnIni(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
            {
                return SaldoInventoryT1Facade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Kosongkan()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.KosongkanSaldo(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
            {
                return SaldoInventoryT1Facade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusSaldo()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoInventoryT1Facade SaldoInventoryT1Facade = new SaldoInventoryT1Facade();
            intReceiptDocNoID = SaldoInventoryT1Facade.MinusSaldo(objSaldoInventoryT1);
            if (SaldoInventoryT1Facade.Error != string.Empty)
            {
                return SaldoInventoryT1Facade.Error;
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
