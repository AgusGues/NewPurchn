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
    public class JobDeskProcessFacade : AbstractTransactionFacade
    {
        private JobDesk objJobDesk;
        private ArrayList arrJobDeskDetail;
        //private string strError = string.Empty;
        private int intJOBDESKID = 0;

        public JobDeskProcessFacade(JobDesk jobDesk, ArrayList arrListJobDesk)
        {

            objJobDesk = jobDesk;
            arrJobDeskDetail = arrListJobDesk;
        }

        public string JOBDESK_No
        {
            get
            {
                return intJOBDESKID.ToString().PadLeft(3, '0') + "/JOBDESK/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }

        public string Insert()
        {
            int intResult = 0;

                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();

                AbstractTransactionFacade absTrans = new JobDeskFacade(objJobDesk);
                intJOBDESKID = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (intJOBDESKID > 0)
                {
                    objJobDesk.JOBDESK_No = intJOBDESKID.ToString().PadLeft(3, '0') + "/JOBDESK/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                    objJobDesk.ID = intJOBDESKID;

                    absTrans = new JobDeskFacade(objJobDesk);
                    //intResult = absTrans.Update(transManager);
                    //if (absTrans.Error != string.Empty)
                    //{
                    //    transManager.RollbackTransaction();
                    //    return absTrans.Error;
                    //}
                    if (arrJobDeskDetail.Count > 0)
                    {
                        foreach (JobdeskDetail jobDeskDetail in arrJobDeskDetail)
                        {
                            jobDeskDetail.JOBDESKID = intJOBDESKID;
                            absTrans = new JobDeskDetailFacade(jobDeskDetail);

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

        //public string Update0()
        //{
        //    int intResult = 0;

        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();
        //    AbstractTransactionFacade absTrans = new JobDeskFacade(objJobDesk);
        //    intResult = absTrans.Update0(transManager);
        //    if (absTrans.Error != string.Empty)
        //    {
        //        transManager.RollbackTransaction();
        //        return absTrans.Error;
        //    }

        //    transManager.CommitTransaction();
        //    transManager.CloseConnection();

        //    return string.Empty;
        //}

        public string Update()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new JobDeskFacade(objJobDesk);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            //if (intResult > 0)
            //{
            //    if (arrJobDeskDetail.Count > 0)
            //    {

            //    }
            //}

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        public string CancelJOBDESKDetail()
        {
            int intResult = 0;
            string asString = string.Empty;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            JobDeskDetailFacade jobdeskDetailFacade = new JobDeskDetailFacade();
            SPPDetail jobdeskDetail = new SPPDetail();
            foreach (JobdeskDetail sDetail in arrJobDeskDetail)
            {
                jobdeskDetail.ID = sDetail.ID;
            }

            jobdeskDetailFacade = new JobDeskDetailFacade(jobdeskDetail);

            intResult = jobdeskDetailFacade.CancelJOBDESKDetail(transManager);
            if (jobdeskDetailFacade.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return jobdeskDetailFacade.Error;
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }

        //public string Update1()
        //{
        //    int intResult = 0;
        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();
        //    AbstractTransactionFacade absTrans = new JobDeskFacade(objJobDesk);

        //    intResult = absTrans.Update(transManager);
        //    if (absTrans.Error != string.Empty)
        //    {
        //        transManager.RollbackTransaction();
        //        return absTrans.Error;
        //    }

        //    transManager.CommitTransaction();
        //    transManager.CloseConnection();

        //    return string.Empty;
        //}

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }


        //public override int Update0(TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

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
