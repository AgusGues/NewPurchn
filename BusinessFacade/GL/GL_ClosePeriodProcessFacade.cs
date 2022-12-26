using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace BusinessFacade.GL
{
    //class GL_ClosePeriodProcessFacade
    //{
    //}
    public class GL_ClosePeriodProcessFacade
    {
        private GL_ChartBal objChartBal;
        //private DocumentID objDocumentID;
        private ArrayList arrChartOfAccount;
        private ArrayList arrChartBal;
        private string strError = string.Empty;
        private int intInvoiceID = 0;
        private int flagKertas = 0;

        public GL_ClosePeriodProcessFacade(ArrayList myCoa, ArrayList MyChartBal, GL_ChartBal myChartBal)
        {
            objChartBal = myChartBal;
            arrChartOfAccount = myCoa;
            arrChartBal = MyChartBal;
        }

        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            //kosong saldoawal / begbal utk periode 2
            AbstractTransactionFacade absTrans = new GL_ChartBalFacade(objChartBal);
            intInvoiceID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            foreach (GL_ChartBal charBal1 in arrChartBal)
            {


            }

            return string.Empty;
        }
        



    }

}
