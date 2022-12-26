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
    //class GL_ChartBalProgressFacade
    //{
    //}
    public class GL_ChartBalProgressFacade
    {
        //private GL_JurHead objGL_JurHead;
        private ArrayList arrJurnalDetail;

        private string strError = string.Empty;
        private int intJurHeadID = 0;
        //private ArrayList arrChartBal;

        //public GL_ChartBalProgressFacade(ArrayList arrchartbal)
        public GL_ChartBalProgressFacade(ArrayList arrjurnaldetail)
        {
            arrJurnalDetail = arrjurnaldetail;
        }

        public string Insert()
        {
            //insert jurnal head
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new GL_JurDetFacade();
            //intJurHeadID = absTrans.Insert(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}
            //string users = objGL_JurHead.CreatedBy;
            ////insert jurnal detail
            //if (intJurHeadID > 0)
            //{



                foreach (GL_JurDet GLJournalDet in arrJurnalDetail)
                {
                    //gak save kesini lagi kan udah 
                    //proses ini untuk otomatis jurnal saja 

                    //GLJournalDet.JurHeadNum = intJurHeadID;
                    //GLJournalDet.CreatedBy = users;
                    //absTrans = new GL_JurDetFacade(GLJournalDet);
                    //intResult = absTrans.Insert(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}
                    //end jurnal detail 

                    //jurnal GL_ChartBal
                    int JurDetNum = intResult;
                    GL_ChartBal chartbal = new GL_ChartBal();

                    chartbal.Period = GLJournalDet.Period;
                    chartbal.ChartNo = GLJournalDet.ChartNo;
                    chartbal.BegBal = 0;
                    if (GLJournalDet.IDRAmount > 0)
                    {
                        //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                        chartbal.DebitTrans = GLJournalDet.DebitTrans;
                        chartbal.CCYDebitTrans = GLJournalDet.CCYDebitTrans;
                    }
                    else
                    {
                        //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                        chartbal.CreditTrans = GLJournalDet.CreditTrans;
                        chartbal.CCYCreditTrans = GLJournalDet.CCYCreditTrans;
                    }
                    chartbal.DeptCode = GLJournalDet.DeptCode;
                    chartbal.CostCode = GLJournalDet.CostCode;

                    absTrans = new GL_ChartBalFacade(chartbal);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    //end jurnal GL_ChartBal

                    //switch (GLJournalDet.JurType)
                    //{
                    //    //insert jurnal AP
                    //    case "AP":
                    //        GL_JurDetAP gljurdetAP = new GL_JurDetAP();
                    //        gljurdetAP.InvNo = GLJournalDet.InvNo;
                    //        gljurdetAP.JurDetNum = JurDetNum;
                    //        gljurdetAP.InvDate = GLJournalDet.InvDate;
                    //        gljurdetAP.OurRef = GLJournalDet.OurRef;
                    //        gljurdetAP.SupCode = GLJournalDet.SupCode;
                    //        gljurdetAP.DueDate = GLJournalDet.DueDate;
                    //        gljurdetAP.Remark = GLJournalDet.RemarkDet;
                    //        gljurdetAP.PPH = GLJournalDet.PPH;
                    //        gljurdetAP.Freight = GLJournalDet.Freight;
                    //        gljurdetAP.PPnBM = GLJournalDet.PPnBM;
                    //        gljurdetAP.CreatedBy = GLJournalDet.CreatedBy;
                    //        absTrans = new GL_JurDetAPFacade(gljurdetAP);
                    //        intResult = absTrans.Insert(transManager);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //        GL_SuppBal suppbal = new GL_SuppBal();
                    //        suppbal.Period = GLJournalDet.Period;
                    //        suppbal.SupCode = GLJournalDet.SupCode;
                    //        suppbal.BegBal = 0;
                    //        if (GLJournalDet.IDRAmount > 0)
                    //        {
                    //            //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                    //            suppbal.DebitTrans = GLJournalDet.DebitTrans;
                    //            suppbal.CCYDebitTrans = GLJournalDet.CCYDebitTrans;
                    //        }
                    //        else
                    //        {
                    //            //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                    //            suppbal.CreditTrans = GLJournalDet.CreditTrans;
                    //            suppbal.CCYCreditTrans = GLJournalDet.CCYCreditTrans;
                    //        }
                    //        absTrans = new GL_SuppBalFacade(suppbal);
                    //        intResult = absTrans.Insert(transManager);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //        break;

                    //    //insert jurnal AR
                    //    case "AR":
                    //        GL_JurDetAR gljurdetAR = new GL_JurDetAR();
                    //        gljurdetAR.InvNo = GLJournalDet.InvNo;
                    //        gljurdetAR.JurDetNum = JurDetNum;
                    //        gljurdetAR.InvDate = GLJournalDet.InvDate;
                    //        gljurdetAR.OurRef = GLJournalDet.OurRef;
                    //        gljurdetAR.CustCode = GLJournalDet.CustCode;
                    //        gljurdetAR.DueDate = GLJournalDet.DueDate;
                    //        gljurdetAR.Remark = GLJournalDet.RemarkDet;
                    //        gljurdetAR.CreatedBy = GLJournalDet.CreatedBy;
                    //        absTrans = new GL_JurDetARFacade(gljurdetAR);
                    //        intResult = absTrans.Insert(transManager);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //        GL_CustBal custbal = new GL_CustBal();
                    //        custbal.Period = GLJournalDet.Period;
                    //        custbal.CustCode = GLJournalDet.CustCode;
                    //        custbal.BegBal = 0;
                    //        if (GLJournalDet.IDRAmount > 0)
                    //        {
                    //            //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                    //            custbal.DebitTrans = GLJournalDet.DebitTrans;
                    //            custbal.CCYDebitTrans = GLJournalDet.CCYDebitTrans;
                    //        }
                    //        else
                    //        {
                    //            //lihat2x DebitTrans atau IDRAmount, CCYAmount atau CCYDebitTrans
                    //            custbal.CreditTrans = GLJournalDet.CreditTrans;
                    //            custbal.CCYCreditTrans = GLJournalDet.CCYCreditTrans;
                    //        }
                    //        absTrans = new GL_CustBalFacade(custbal);
                    //        intResult = absTrans.Insert(transManager);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //        break;
                    //    //insert jurnal Bank
                    //    case "BA":
                    //        GL_JurDetBA gljurdetBA = new GL_JurDetBA();
                    //        gljurdetBA.JurDetNum = JurDetNum;
                    //        gljurdetBA.BgNo = GLJournalDet.BgNo;
                    //        gljurdetBA.BankName = GLJournalDet.BankName;
                    //        gljurdetBA.DueDate = GLJournalDet.DueDate;
                    //        gljurdetBA.Remark = GLJournalDet.RemarkDet;
                    //        gljurdetBA.CreatedBy = GLJournalDet.CreatedBy;
                    //        absTrans = new GL_JurDetBAFacade(gljurdetBA);
                    //        intResult = absTrans.Insert(transManager);
                    //        if (absTrans.Error != string.Empty)
                    //        {
                    //            transManager.RollbackTransaction();
                    //            return absTrans.Error;
                    //        }
                    //        break;
                    //}


                }

            //}
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string Update()
        {
            return string.Empty;
        }
    }




}
