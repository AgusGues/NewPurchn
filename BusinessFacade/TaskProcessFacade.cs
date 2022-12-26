using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Web;
using System.IO;

namespace BusinessFacade
{

    public class TaskProcessFacade
    {
        private Task objTask;
        private ArrayList arrImgLampiran;
        private string strError = string.Empty;
        private int intTaskID = 0;
        private ISO_DocumentNo objDocumentNo;


        public TaskProcessFacade(Task task, ISO_DocumentNo documentNo)
        {
            objTask = task;
            objDocumentNo = documentNo;
        }

        public ArrayList arrImgProcessFacade(ArrayList arrimgLampiran)
        {
            arrImgLampiran = arrimgLampiran;
            //arrTaskDetail = arrListDetail;
            return arrImgLampiran;
        }

        public string TaskNo
        {
            get
            {
                return objDocumentNo.Plant + objDocumentNo.DocNo.ToString().PadLeft(5, '0') + "/Task." + objTask.DeptName + "/" + objTask.TglMulai.Month.ToString().PadLeft(2, '0') + objTask.TglMulai.Year.ToString().Substring(2, 2); 
            }
        }


        public string Insert()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            if (objDocumentNo.ID == 0)
            {
                intResult = docNoFacade.Insert(objDocumentNo);
            }
            else
            {
                intResult = docNoFacade.Update(objDocumentNo);
            }

            if (intResult > 0)
            {
                intTaskID = 0;
                objTask.TaskNo = objDocumentNo.Plant + objDocumentNo.DocNo.ToString().PadLeft(5, '0') + "/Task." + objTask.DeptName + "/" + objTask.TglMulai.Month.ToString().PadLeft(2, '0') + objTask.TglMulai.Year.ToString().Substring(2, 2);

                AbstractTransactionFacade absTrans = new TaskFacade(objTask);
                intTaskID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intTaskID > 0)
                {
                    objTask.TaskID = intTaskID;

                    TaskFacade taskFacade = new TaskFacade(objTask);
                    //intResult = taskFacade.UpdateNoTaskNo(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}

                    absTrans = new TaskFacade(objTask);
                    taskFacade = new TaskFacade(objTask);
                    intResult = taskFacade.InsertTaskDetail(transManager);
                    if (taskFacade.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }

                    if (arrImgLampiran != null)
                    {
                        int z = 0;
                        Task task1 = new Task();
                        while (z < arrImgLampiran.Count)
                        {
                            task1.TaskID = intTaskID;
                            task1.Image = Convert.ToString(arrImgLampiran[z]);
                            TaskFacade taskFacade2 = new TaskFacade(task1);
                            taskFacade2.InsertImgLampiran(transManager);
                            z++;
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
            //int intResult = 0;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new BankOutFacade(objTask);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}
            //if (intResult > 0)
            //{
            //    absTrans = new BankOutDetailFacade((BankOutDetail)arrTaskDetail [0]);

            //    intResult = absTrans.Delete(transManager);
            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return absTrans.Error;
            //    }

            //    string testc = arrTaskDetail .Count.ToString();

            //    foreach (BankOutDetail bankOutDetail in arrTaskDetail )
            //    {
            //        bankOutDetail.BankOutID = objTask.ID;
            //        absTrans = new BankOutDetailFacade(bankOutDetail);
            //        intResult = absTrans.Insert(transManager);
            //        if (absTrans.Error != string.Empty)
            //        {
            //            transManager.RollbackTransaction();
            //            return absTrans.Error;
            //        }
            //    }

            //    //try jornalBank

            //    //until try jornalBank
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string InsertTaskDetail()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objTask);


            TaskFacade taskFacade = new TaskFacade(objTask);
            intResult = taskFacade.InsertTaskDetail(transManager);
            if (taskFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            //update status Task lama ke 1

            objTask.Status = 1;
            intResult = taskFacade.UpdateDetailStatus(transManager);
            if (taskFacade.Error != string.Empty)
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
            AbstractTransactionFacade absTrans = new TaskFacade(objTask);

            TaskFacade taskFacade = new TaskFacade(objTask);
            intResult = taskFacade.UpdateTaskDetailApproval(transManager);
            if (taskFacade.Error != string.Empty)
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
            AbstractTransactionFacade absTrans = new TaskFacade(objTask);

            TaskFacade taskFacade = new TaskFacade(objTask);
            intResult = taskFacade.UpdateStatusPosting(transManager);
            if (taskFacade.Error != string.Empty)
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
            AbstractTransactionFacade absTrans = new TaskFacade(objTask);

            TaskFacade taskFacade = new TaskFacade(objTask);
            intResult = taskFacade.UpdateDetailStatus(transManager);
            if (taskFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            intResult = taskFacade.UpdateStatusPosting(transManager);
            if (taskFacade.Error != string.Empty)
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
            //int intResult = 0;

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new BankOutFacade(objTask);
            //intResult = absTrans.Update(transManager);
            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return absTrans.Error;
            //}

            //transManager.CommitTransaction();
            //transManager.CloseConnection();

            return string.Empty;
        }

        public string Delete()
        {
            //int intResult = 0;
            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacade absTrans = new BankOutFacade(objTask);
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

        public string CancelTask()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objTask);

            TaskFacade taskFacade = new TaskFacade(objTask);
            intResult = taskFacade.CancelStatusTask(transManager);
            if (taskFacade.Error != string.Empty)
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
