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
    public class AdjustProcessFacade : AbstractTransactionFacade
    {
        private Adjust objAdjust;
        private ArrayList arrReceiptDetail;
        //private string strError = string.Empty;
        private int intAdjustID = 0;
        
        public AdjustProcessFacade(Adjust adjust, ArrayList arrListAdjustDetail)
        {
            objAdjust  = adjust;
            arrReceiptDetail = arrListAdjustDetail;
        }

        public string AdjustNo
        {
            get
            {
                return intAdjustID.ToString().PadLeft(5, '0') + "/ADJ/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                //return objReceiptDocNo.ReceiptCode + bl + th + "-" + objReceiptDocNo.NoUrut.ToString().PadLeft(5, '0');
            }
        }


        public string Insert()
        {
            int intResult = 0;


            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new AdjustFacade(objAdjust);
            intAdjustID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intAdjustID > 0)
            {
                objAdjust.AdjustNo = intAdjustID.ToString().PadLeft(5, '0') + "/ADJ/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objAdjust.ID = intAdjustID;

                absTrans = new AdjustFacade(objAdjust);
                intResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (arrReceiptDetail.Count > 0)
                {
                    foreach (AdjustDetail adjDetail in arrReceiptDetail)
                    {
                        adjDetail.AdjustID = intAdjustID;
                        absTrans = new AdjustDetailFacade(adjDetail);

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

        public string CancelReceiptDetail()
        {
            //int intResult = 0;
            //string asString = string.Empty;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            //ReceiptDetail receiptDetail = new ReceiptDetail();
            //foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            //{
            //    receiptDetail.ID = rcpDetail.ID;
            //    receiptDetail.ItemID = rcpDetail.ItemID;
            //    receiptDetail.Quantity = rcpDetail.Quantity;
            //    receiptDetail.PoID = rcpDetail.PoID;
            //    receiptDetail.FlagPO = rcpDetail.FlagPO;
            //    if (objReceipt.ReceiptType == 7)
            //        receiptDetail.FlagTipe = 3;
            //    else
            //        receiptDetail.FlagTipe = 2;

            //}

            //receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);

            //intResult = receiptDetailFacade.CancelReceiptDetail(transManager);
            //if (receiptDetailFacade.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return receiptDetailFacade.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelReceipt()
        {
            //int intResult = 0;
            //string asString = string.Empty;
            //objReceipt.Status = -1;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //AbstractTransactionFacade absTrans = new ReceiptFacade(objReceipt);
            //ReceiptFacade receiptFacade = new ReceiptFacade();

            //absTrans = new ReceiptFacade(objReceipt);
            ////intResult = absTrans.Delete(transManager);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            //transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();

            //ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            //ReceiptDetail receiptDetail = new ReceiptDetail();
            //foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
            //{
            //    receiptDetail.ID = rcpDetail.ID;
            //    receiptDetail.ItemID = rcpDetail.ItemID;
            //    receiptDetail.Quantity = rcpDetail.Quantity;
            //    receiptDetail.PoID = rcpDetail.PoID;
            //    receiptDetail.FlagPO = rcpDetail.FlagPO;
            //    receiptDetail.FlagTipe = 1;

            //    receiptDetailFacade = new ReceiptDetailFacade(receiptDetail);
            //    intResult = receiptDetailFacade.CancelInventoryByReceiptDetail(transManager);
            //    if (receiptDetailFacade.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return receiptDetailFacade.Error;
            //    }
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprove()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new AdjustDetailFacade(adjDetail);
            if (arrReceiptDetail.Count > 0)
            {
                foreach (AdjustDetail adjDetail in arrReceiptDetail)
                {
                    AbstractTransactionFacade absTrans = new AdjustDetailFacade(adjDetail);
                    if (adjDetail.Apv>0)
                    intResult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Delete()
        {
            int intResult = 0;


            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            if (arrReceiptDetail.Count >0)
            {
                foreach (AdjustDetail adjDetail in arrReceiptDetail)
                {
                    AbstractTransactionFacade absTrans = new AdjustDetailFacade(adjDetail);
                    if (adjDetail.Apv==0)
                    intResult = absTrans.Delete(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }


        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
    }
}
