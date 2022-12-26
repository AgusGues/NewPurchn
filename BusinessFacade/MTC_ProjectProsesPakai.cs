using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Runtime.InteropServices;

namespace BusinessFacade
{
    public class MTC_ProjectProsesPakai 
    {
        private ArrayList arrProject;
        private AbstractTransactionFacade absTrans;

        public MTC_ProjectProsesPakai(ArrayList ArrProject)
        {
            arrProject = ArrProject;
        }
        
        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            if (arrProject.Count > 0)
            {
                foreach (MTC_ProjectPakai objPro in arrProject)
                {
                    absTrans = new MTC_ProjectPakaiFacade(objPro);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();

            }
            return "ok";
        }
        public string InsertArmada()
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            if (arrProject.Count > 0)
            {
                foreach (MTC_Armada objPro in arrProject)
                {
                    absTrans = new MTC_ArmadaFacade(objPro);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
            }
            return "ok";
        }
        
    }
}
