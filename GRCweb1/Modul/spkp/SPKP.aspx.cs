using BusinessFacade;
using DataAccessLayer;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.SPKP
{
    public partial class SPKP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetUser()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            string createdby = user.UserName;

            return createdby;
        }
        [WebMethod]
        public static List<SPKP_Dtl.ddl1> GetKategori()
        {
            List<SPKP_Dtl.ddl1> objkategori = new List<SPKP_Dtl.ddl1>();
            objkategori = SPKPFacade.Retrivekategori();
            //string hasil= JsonConvert.SerializeObject(objkategori);
            return objkategori;
        }
        [WebMethod]
        public static List<SPKP_Dtl.ddl2> GetTebal()
        {
            List<SPKP_Dtl.ddl2> objtebal = new List<SPKP_Dtl.ddl2>();
            objtebal = SPKPFacade.Retrivetebal();
            //return JsonConvert.SerializeObject(objdept);
            return objtebal;
        }

        [WebMethod]
        public static List<SPKP_Dtl.ddl3> GetUkuran()
        {
            List<SPKP_Dtl.ddl3> objukuran = new List<SPKP_Dtl.ddl3>();
            objukuran = SPKPFacade.Retriveukuran();
            return objukuran;
        }

        [WebMethod]
        public static string insert(SPKP_Dtl.d_input dinput, SPKP_Dtl obj)
        {
            int intResult = 0;
            int intspkp = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans;
            absTrans = new SPKPFacade(obj);
            intResult = absTrans.Insert(transManager);
            int spkpid = intResult;
            //rekapK.CutID = CutID;
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            /** rekam di table t3_rekap item asal*/
            else
            {
                intspkp = 0;
                foreach (SPKP_Dtl.insert_dtl spkpins in dinput.data)
                {
                    SPKP_Dtl.insert_dtl input;
                    input = spkpins;
                    spkpins.spkpid = spkpid;
                    absTrans = new SPKPFacadeDetail(input);
                    intspkp = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return absTrans.Error;
                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
    }
}