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

namespace GRCweb1.Modul.spkp
{
    public partial class ListSPKP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetList()
        {
            List<SPKP_Dtl> objlist = new List<SPKP_Dtl>();
            objlist = SPKPFacade.RetriveList();
            return JsonConvert.SerializeObject(objlist);
        }
        [WebMethod]
        public static string Getspkp(string nospkp,string line)
        {
            List<SPKP_Dtl.insert_dtl> objlist = new List<SPKP_Dtl.insert_dtl>();
            objlist = SPKPFacade.Retrivespkppilih(nospkp,line);
            return JsonConvert.SerializeObject(objlist);
        }
        
        [WebMethod]
        public static string Getnospkp(string id)
        {
            string nospkp = SPKPFacade.Retrivenospkp(id);
            return JsonConvert.SerializeObject(nospkp);
        }
        [WebMethod]
        public static string Getspkpdetail(string id)
        {
            List<SPKP_Dtl.insert_dtl> objlist = new List<SPKP_Dtl.insert_dtl>();
            objlist = SPKPFacade.Retrivespkpdetail(id);
            return JsonConvert.SerializeObject(objlist);
        }
        [WebMethod]
        public static string update(SPKP_Dtl.insert_dtl obj)
        {
            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();

            AbstractTransactionFacade absTrans;
            absTrans = new SPKPFacadeDetail(obj);
            intResult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            else
            {

            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return string.Empty;
        }
    }
}