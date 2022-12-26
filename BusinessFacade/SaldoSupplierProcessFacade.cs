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
    public class SaldoSupplierProcessFacade
    {
        private SaldoSupplier objSaldoSupplier;
        private int intReceiptDocNoID = 0;

        public SaldoSupplierProcessFacade(SaldoSupplier saldoInv)
        {
            objSaldoSupplier = saldoInv;

        }

        public string Insert()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoSupplierFacade saldoSupplierFacade = new SaldoSupplierFacade();
            intReceiptDocNoID = saldoSupplierFacade.Insert(objSaldoSupplier);
            if (saldoSupplierFacade.Error != string.Empty)
            {
                return saldoSupplierFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();


            return string.Empty;
        }

        public string Update()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoSupplierFacade SaldoSupplierFacade = new SaldoSupplierFacade();
            intReceiptDocNoID = SaldoSupplierFacade.Update(objSaldoSupplier);
            if (SaldoSupplierFacade.Error != string.Empty)
            {
                return SaldoSupplierFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();



            return string.Empty;
        }

        public string UpdateSaldoBlnLalu()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoSupplierFacade SaldoSupplierFacade = new SaldoSupplierFacade();
            intReceiptDocNoID = SaldoSupplierFacade.UpdateSaldoBlnLalu(objSaldoSupplier);
            if (SaldoSupplierFacade.Error != string.Empty)
            {
                return SaldoSupplierFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        //public string UpdateSaldoAvgPriceBlnIni()
        //{

        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();

        //    SaldoSupplierFacade SaldoSupplierFacade = new SaldoSupplierFacade();
        //    intReceiptDocNoID = SaldoSupplierFacade.UpdateSaldoAvgPriceBlnIni(objSaldoSupplier);
        //    if (SaldoSupplierFacade.Error != string.Empty)
        //    {
        //        return SaldoSupplierFacade.Error;
        //    }

        //    transManager.CommitTransaction();
        //    transManager.CloseConnection();

        //    return string.Empty;
        //}

        public string Kosongkan()
        {
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoSupplierFacade SaldoSupplierFacade = new SaldoSupplierFacade();
            intReceiptDocNoID = SaldoSupplierFacade.KosongkanSaldo(objSaldoSupplier);
            if (SaldoSupplierFacade.Error != string.Empty)
            {
                return SaldoSupplierFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string MinusSaldo()
        {

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            SaldoSupplierFacade SaldoSupplierFacade = new SaldoSupplierFacade();
            intReceiptDocNoID = SaldoSupplierFacade.MinusSaldo(objSaldoSupplier);
            if (SaldoSupplierFacade.Error != string.Empty)
            {
                return SaldoSupplierFacade.Error;
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

