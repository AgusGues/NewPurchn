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
    
    public class ISO_SOPProcessFacade
    {
        private ISO_SOP objSOP;
        //private ArrayList arrSOPDetail;
        private ArrayList arrImgLampiran;
        private string strError = string.Empty;
        private int intTaskID = 0;
        private ISO_DocumentNo objDocNo;

        public ISO_SOPProcessFacade(ISO_SOP sop, ISO_DocumentNo docNo)
        {
            objSOP = sop;
            objDocNo = docNo;
        }

        public ArrayList arrImgProcessFacade(ArrayList arrimgLampiran)
        {
            arrImgLampiran = arrimgLampiran;
            return arrImgLampiran;
        }

        public string sopNonya
        {
            get
            {
                if (objSOP.PesType == 1)//KPI
                    return objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/KPI." + objSOP.DeptName + "/" + objSOP.TglMulai.Month.ToString().PadLeft(2, '0') + objSOP.TglMulai.Year.ToString().Substring(2, 2);
                else
                    return objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/SOP." + objSOP.DeptName + "/" + objSOP.TglMulai.Month.ToString().PadLeft(2, '0') + objSOP.TglMulai.Year.ToString().Substring(2, 2);
            }
        }


        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new ISO_SOPFacade(objSOP);
            #region Proces Number document
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            if (objDocNo.ID == 0)
            {
                intResult = docNoFacade.Insert(objDocNo);
            }
            else
            {
                intResult = docNoFacade.Update(objDocNo);
            }

            if (intResult > 0)
            {

                ISO_SOPFacade sopFacade = new ISO_SOPFacade();

                if (objSOP.PesType == 1)//KPI
                {
                    sopFacade = new ISO_SOPFacade(objSOP);

                    objSOP.KpiNo = objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/KPI." + objSOP.DeptName + "/" + objSOP.TglMulai.Month.ToString().PadLeft(2, '0') + objSOP.TglMulai.Year.ToString().Substring(2, 2);

                    intTaskID = sopFacade.InsertKPI(transManager);

                    if (absTrans.Error != string.Empty || intTaskID < 0)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                else //SOP
                {
                    objSOP.SopNo = objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/SOP." + objSOP.DeptName + "/" + objSOP.TglMulai.Month.ToString().PadLeft(2, '0') + objSOP.TglMulai.Year.ToString().Substring(2, 2);

                    intTaskID = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            #endregion
                if (intTaskID > 0)
                {
                    absTrans = new ISO_SOPFacade(objSOP);
                    sopFacade = new ISO_SOPFacade(objSOP);

                    if (objSOP.PesType == 1)//KPI
                    {
                        objSOP.KpiID = intTaskID;

                        intResult = sopFacade.InsertKPIDetail(transManager);
                        if (sopFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                    }
                    else //SOP
                    {
                        objSOP.SopID = intTaskID;

                        intResult = sopFacade.InsertSOPDetail(transManager);
                        if (sopFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
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
            AbstractTransactionFacade absTrans = new TaskFacade(objSOP);


            TaskFacade taskFacade = new TaskFacade(objSOP);
            intResult = taskFacade.InsertTaskDetail(transManager);
            if (taskFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            //update status ISO_SOP lama ke 1

            objSOP.Status = 1;
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

        public string UpdateSOPDetailApproval()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ISO_SOPFacade(objSOP);

            ISO_SOPFacade sopFacade = new ISO_SOPFacade(objSOP);
            intResult = sopFacade.UpdateSopDetailApproval(transManager);
            if (sopFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string UpdateSOPDetailApproval1()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new ISO_SOPFacade(objSOP);

            ISO_SOPFacade sopFacade = new ISO_SOPFacade(objSOP);
            intResult = sopFacade.UpdateSopDetailApproval1(transManager);
            if (sopFacade.Error != string.Empty)
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
            AbstractTransactionFacade absTrans = new TaskFacade(objSOP);

            TaskFacade taskFacade = new TaskFacade(objSOP);
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
            AbstractTransactionFacade absTrans = new TaskFacade(objSOP);

            TaskFacade taskFacade = new TaskFacade(objSOP);
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

            return string.Empty;
        }

        public string Delete()
        {

            return string.Empty;
        }

        public string CancelISO_SOP()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new TaskFacade(objSOP);

            TaskFacade taskFacade = new TaskFacade(objSOP);
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
