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
    public class SarmutPESProcessFacade
    {
        private SarmutPes objSarPes;
        //private ArrayList arrSOPDetail;
        private ArrayList arrImgLampiran;
        private string strError = string.Empty;
        private int intTaskID = 0;
        private ISO_DocumentNo objDocNo;

        public SarmutPESProcessFacade(SarmutPes sop, ISO_DocumentNo docNo)
        {
            objSarPes = sop;
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
                if (objSarPes.PesType == 1)//KPI
                    return objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/KPI." + objSarPes.DeptName + "/" + objSarPes.TglMulai.Month.ToString().PadLeft(2, '0') + objSarPes.TglMulai.Year.ToString().Substring(2, 2);
                else
                    return objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/SOP." + objSarPes.DeptName + "/" + objSarPes.TglMulai.Month.ToString().PadLeft(2, '0') + objSarPes.TglMulai.Year.ToString().Substring(2, 2);
            }
        }

        public string Insert()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans = new SarmutPESFacade(objSarPes);
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

                SarmutPESFacade sopFacade = new SarmutPESFacade();

                if (objSarPes.PesType == 1)//KPI
                {
                    sopFacade = new SarmutPESFacade(objSarPes);

                    objSarPes.KpiNo = objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/KPI." + objSarPes.DeptName + "/" + objSarPes.TglMulai.Month.ToString().PadLeft(2, '0') + objSarPes.TglMulai.Year.ToString().Substring(2, 2);

                    intTaskID = sopFacade.InsertKPI(transManager);

                    if (absTrans.Error != string.Empty || intTaskID < 0)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
                else //SOP
                {
                    objSarPes.SopNo = objDocNo.Plant + objDocNo.DocNo.ToString().PadLeft(5, '0') + "/SOP." + objSarPes.DeptName + "/" + objSarPes.TglMulai.Month.ToString().PadLeft(2, '0') + objSarPes.TglMulai.Year.ToString().Substring(2, 2);

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
                    absTrans = new SarmutPESFacade(objSarPes);
                    sopFacade = new SarmutPESFacade(objSarPes);

                    if (objSarPes.PesType == 1)//KPI
                    {
                        objSarPes.KpiID = intTaskID;

                        intResult = sopFacade.InsertKPIDetail(transManager);
                        if (sopFacade.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }

                    }
                    else //SOP
                    {
                        //objSarPes.SopID = intTaskID;

                        //intResult = sopFacade.InsertSOPDetail(transManager);
                        //if (sopFacade.Error != string.Empty)
                        //{
                        //    transManager.RollbackTransaction();
                        //    return absTrans.Error;
                        //}
                    }

                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();

            return string.Empty;
        }
        public string UpdateKpi()
        {
            int intResult = 0;

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new SarmutPESFacade(objSarPes);

            SarmutPESFacade kpiFacade = new SarmutPESFacade(objSarPes);
            intResult = kpiFacade.UpdateKpi(transManager);
            if (kpiFacade.Error != string.Empty)
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
