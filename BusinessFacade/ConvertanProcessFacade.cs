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
    public class ConvertanProcessFacade
    {
        private Convertan objConvertan;
        private string strError = string.Empty;
        private int intAdjustID = 0;

        public ConvertanProcessFacade(Convertan convertan)
        {
            objConvertan = convertan;
        }

        public string RepackNo
        {
            get
            {
                return intAdjustID.ToString().PadLeft(5, '0') + "/RP/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }


        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ConvertanFacade(objConvertan);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                objConvertan.RepackNo = intAdjustID.ToString().PadLeft(5, '0') + "/RP/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objConvertan.ID = intAdjustID;

                absTrans = new ConvertanFacade(objConvertan);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                //potong stok inventoryRepack
                InventoryFacade inventoryFacade = new InventoryFacade();
                Inventory inventory = new Inventory();

                inventory.ID = objConvertan.FromItemID;
                inventory.Jumlah = objConvertan.FromQty;

                intResult = inventoryFacade.MinusQtyForRepack(inventory);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                // until here

                //add ke inventory
                // utk add ke table inventori
                inventory.ID = objConvertan.ToItemID;
                inventory.Jumlah = objConvertan.ToQty;

                intResult = inventoryFacade.UpdateQty(inventory);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                // until here

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
