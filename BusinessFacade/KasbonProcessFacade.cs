using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class KasbonProcessFacade
    {
        private Kasbon objKasbon;
        private ArrayList arrKasbonDetail;
        private string strError = string.Empty;
        private int intKasbonID = 0;

        public KasbonProcessFacade(Kasbon kasbon, ArrayList arrKKDetail)
        {
            objKasbon = kasbon;
            arrKasbonDetail = arrKKDetail;
        }

        public string NoPengajuan
        {
            get
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                CompanyFacade companyFacade = new CompanyFacade();
                string code = companyFacade.GetKodeCompany(users.UnitKerjaID);
                //return intKasbonID.ToString().PadLeft(4, '0') + "/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                return objKasbon.KasbonCounter.ToString().PadLeft(4, '0') + "/" + code + "/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            }
        }

        public string Insert()
        {
            int IntResult = 0;


            //KasbonNumberFacade kasbonNumberFacade = new KasbonNumberFacade();

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new KasbonFacade(objKasbon);
            intKasbonID = absTrans.Insert(transManager);
            absTrans = new KasbonNumberFacade(objKasbon);
            IntResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            if (intKasbonID > 0)
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                CompanyFacade companyFacade = new CompanyFacade();
                string code = companyFacade.GetKodeCompany(users.UnitKerjaID);
                //objKasbon.NoPengajuan = intKasbonID.ToString().PadLeft(4, '0') + "/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objKasbon.NoPengajuan = objKasbon.KasbonCounter.ToString().PadLeft(4, '0') + "/" + code + "/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                objKasbon.ID = intKasbonID;

                absTrans = new KasbonFacade(objKasbon);
                IntResult = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return absTrans.Error;
                }
                if (arrKasbonDetail.Count > 0)
                {
                    foreach (KasbonDetail kasbonDetail in arrKasbonDetail)
                    {
                        kasbonDetail.KID = intKasbonID;
                        absTrans = new KasbonDetailFacade(kasbonDetail);

                        IntResult = absTrans.Insert(transManager);
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

        

    }
}
