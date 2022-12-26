using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Web;
using System.Drawing;
using System.IO;

namespace BusinessFacade
{
    public class ISO_UpdProcessFacade
    {
        private ISO_Upd objUPD;
        private ArrayList arrImgLampiran;
        private ArrayList arrUPDDetail;
        private string strError = string.Empty;
        private int intUpdID = 0;
        private ISO_UpdDocNo objUpdNo;

        public ISO_UpdProcessFacade(ISO_Upd upd, ISO_UpdDocNo docno, ArrayList arrupdDetail)
        {
            objUPD = upd;
            objUpdNo = docno;
            arrUPDDetail = arrupdDetail;
        }

        public ArrayList arrImgProcessFacade(ArrayList arrimgLampiran)
        {
            arrImgLampiran = arrimgLampiran;            
            return arrImgLampiran;
        }

        public string UpdNo
        {
            
            get
            {               
                return objUpdNo.DeptCode + "/" + objUpdNo.DocTypE + "/" + objUpdNo.DocNo.ToString().PadLeft(2, '0') + "/" + objUpdNo.Tahun.ToString().Substring(2, 2);                  
            }
        }


        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            ISO_UpdDocNoFacade docNoFacade = new ISO_UpdDocNoFacade();
            if (objUpdNo.ID == 0)
            {
                intResult = docNoFacade.Insert(objUpdNo);
            }
            else
            {
                intResult = docNoFacade.Update(objUpdNo);
            }

            if (intResult > 0)
            {
                intUpdID = 0;
                
                objUPD.updNo = objUpdNo.DeptCode + "/" + objUpdNo.DocTypE + "/" + objUpdNo.DocNo.ToString().PadLeft(2, '0') + "/" + objUpdNo.Tahun.ToString().Substring(2, 2);

                AbstractTransactionFacade absTrans = new ISO_UpdFacade(objUPD);
                intUpdID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intUpdID > 0)
                {
                    objUPD.updID = intUpdID;

                    ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);

                    foreach (ISO_Upd updDetail in arrUPDDetail)
                    {
                        
                        updFacade = new ISO_UpdFacade(updDetail);
                        intResult = updFacade.InsertUPDDetail(transManager);
                        if (updFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                    }

                    if (arrImgLampiran != null)
                    {
                        int z = 0;
                        ISO_Upd task1 = new ISO_Upd();
                        while (z < arrImgLampiran.Count)
                        {
                            task1.UPDid = intUpdID;
                            task1.Image= Convert.ToString(arrImgLampiran[z]);
                            ISO_UpdFacade updFacade2 = new ISO_UpdFacade(task1);

                            updFacade2.InsertImgLampiran(transManager);
                            z++;

                            if (updFacade2.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return absTrans.Error;
                            }
                        }
                    }
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string Update()
        {
            
            return string.Empty;
        }

        public string InsertTaskDetail()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ISO_UpdFacade(objUPD);


            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.InsertUPDDetail(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            //update status Task lama ke 1

            objUPD.Status = 1;
            intResult = updFacade.UpdateDetailStatus(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateTaskDetailApproval()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ISO_UpdFacade(objUPD);

            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.UpdateTaskDetailApproval(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApv()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ISO_UpdFacade(objUPD);

            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.UpdateApv(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdatePosting()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objUPD);

            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.UpdateStatusPosting(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string UpdateTaskDetailPosting()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objUPD);

            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.UpdateDetailStatus(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            intResult = updFacade.UpdateStatusPosting(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            
            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateApprove()
        {
            
            return string.Empty;
        }

        public string Delete()
        {            

            return string.Empty;
        }

        public string CancelTask()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objUPD);

            ISO_UpdFacade updFacade = new ISO_UpdFacade(objUPD);
            intResult = updFacade.CancelStatusTask(transManager);
            if (updFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

    }
}

    //}
//}
