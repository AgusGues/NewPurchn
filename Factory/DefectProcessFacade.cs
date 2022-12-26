using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using Factory;
namespace DefectFacade
{
    public class DefectProcessFacade
    {
        private Defect objDefect;
        private ArrayList arrDefectDetail;
        private string strError = string.Empty;
        private int intDefectID = 0;
        private string bl = string.Empty;
        private string th = string.Empty;
        private string typedef = string.Empty;
        private string lpartno = string.Empty;
        public DefectProcessFacade(Defect Defect, ArrayList arrListDefect, string deftype, string partno)
        {
            objDefect = Defect;
            arrDefectDetail = arrListDefect;
            typedef = deftype;
            lpartno = partno;
        }
        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new DefectFacades(objDefect);
            T1_SerahFacade serahF = new T1_SerahFacade();
            intDefectID = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intDefectID > 0)
            {
                if (arrDefectDetail.Count > 0)
                {
                    foreach (DefectDetail defectDetail in arrDefectDetail)
                    {
                        defectDetail.DefectID = intDefectID;
                        if (typedef=="biasa")
                            absTrans = new DefectDetailFacade(defectDetail);
                        else
                            absTrans = new DefectLDetailFacade(defectDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }
            }
            if (typedef == "biasa")
                serahF.UpdateSerahByDefect(objDefect.SerahID);
            else
                serahF.UpdateT3RekapByDefect(objDefect.Tgl.ToString("yyyyMMdd"),lpartno);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string Insert1()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new DefectFacades(objDefect);
            T1_SerahFacade serahF = new T1_SerahFacade();
            intDefectID = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intDefectID > 0)
            {
                if (arrDefectDetail.Count > 0)
                {
                    foreach (DefectDetail defectDetail in arrDefectDetail)
                    {
                        defectDetail.DefectID = intDefectID;
                        if (typedef == "biasa")
                            absTrans = new DefectDetailFacade(defectDetail);
                        else
                            absTrans = new DefectLDetailFacade(defectDetail);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }
                }
            }
            if (typedef == "biasa")
                serahF.UpdateSerahByDefect(objDefect.SerahID);
            else
                serahF.UpdateT3RekapByDefect(objDefect.Tgl.ToString("yyyyMMdd"), lpartno);
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
        public string Update()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ReceiptFacade(objDefect);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
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
