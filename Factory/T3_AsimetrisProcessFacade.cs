using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using DataAccessLayer;
using Domain;

namespace Factory
{
    public class T3_AsimetrisProcessFacade
    {
        private T3_Serah objT3_SerahK;
        private T3_Rekap objT3_RekapK;
        private ArrayList arrAsimetris;
        private int intSerah =0;
        private string strError = string.Empty;
        private string DocNo;
        private DateTime Tgl;
        private string Createdby;

        public T3_AsimetrisProcessFacade(T3_Serah T3_SerahK,  ArrayList arrListAsimetris, T3_Rekap RekapK, string docNo,DateTime tgl,string createdby)
        {
            objT3_SerahK = T3_SerahK;
            objT3_RekapK = RekapK;
            arrAsimetris = arrListAsimetris;
            DocNo=docNo;
            Tgl=tgl;
            Createdby = createdby;
        }

        public string Insert()
        {
            int intResult = 0;
            int cutID = 0;
            string keterangan = string.Empty;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(objT3_SerahK);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intSerah > 0)
            {
                foreach (T3_Asimetris asimetris in arrAsimetris)
                {
                    //proses lokasi akhir
                    T3_Serah objT3_SerahT = new T3_Serah();
                    T3_Rekap objT3_RekapT = new T3_Rekap();
                    intSerah = 0;
                    int intresult = 0;
                    objT3_SerahT.Flag = "tambah";
                    objT3_SerahT.GroupID = asimetris.GroupID;
                    objT3_SerahT.ItemID = asimetris.ItemIDOut;
                    objT3_SerahT.ID = asimetris.SerahID;
                    objT3_SerahT.LokID = asimetris.LokIDOut;
                    objT3_SerahT.Qty = asimetris.QtyOut;
                    objT3_SerahT.CreatedBy = Createdby;
                    objT3_SerahT.HPP = asimetris.HPP;
                    absTrans = new T3_SerahFacade(objT3_SerahT);
                    intSerah = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                    if (intSerah > 0)
                    {
                        //rekam table asimetris
                        cutID = 0;
                        asimetris.DocNo = DocNo;
                        asimetris.RekapID = intresult;
                        asimetris.CreatedBy = Createdby;

                        //asimetris.MCutter = HttpContext.Current.Session["MCutter"];

                        absTrans = new T3_AsimetrisFacade(asimetris);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        cutID = intResult;
                        objT3_RekapT.DestID = 0;
                        objT3_RekapT.SerahID = asimetris.SerahID;
                        objT3_RekapT.T1serahID = 0;
                        objT3_RekapT.LokasiID = asimetris.LokIDOut;
                        objT3_RekapT.ItemIDSer = asimetris.ItemIDOut;
                        objT3_RekapT.TglTrm = Tgl;
                        objT3_RekapT.QtyInTrm = asimetris.QtyOut;
                        objT3_RekapT.QtyOutTrm = 0;
                        objT3_RekapT.GroupID = asimetris.GroupID;
                        objT3_RekapT.CreatedBy = Createdby;
                        objT3_RekapT.Keterangan = asimetris.PartnoIn;
                        objT3_RekapT.HPP = asimetris.HPP;
                        objT3_RekapT.Process = "Asimetris";
                        objT3_RekapT.SA = asimetris.SA ;
                        objT3_RekapT.CutID = cutID;
                        absTrans = new T3_RekapFacade(objT3_RekapT);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return absTrans.Error;
                        }
                        keterangan = keterangan + "," + asimetris.PartnoOut;
                    }
                }
                objT3_RekapK.Keterangan = keterangan;
                objT3_RekapK.CutID = cutID;
                absTrans = new T3_RekapFacade(objT3_RekapK);
                intResult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
    }
}
