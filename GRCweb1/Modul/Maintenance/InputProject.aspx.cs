using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using GRCweb1.Models;
using Newtonsoft.Json;
using Factory;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Web.Http;
using System.Reflection;

namespace GRCweb1.Modul.Maintenance
{
    public partial class InputProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<InputProjectNf.ParamData> ListData(string status, int dept, string nomor)
        {
            List<InputProjectNf.ParamData> list = new List<InputProjectNf.ParamData>();
            list = InputProjectNfFacade.ListData(status, dept, nomor);
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamDeptPemohon> ListDeptPemohon()
        {
            List<InputProjectNf.ParamDeptPemohon> list = new List<InputProjectNf.ParamDeptPemohon>();
            list = InputProjectNfFacade.ListDeptPemohon();
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamGroupProject> ListGroupProject()
        {
            List<InputProjectNf.ParamGroupProject> list = new List<InputProjectNf.ParamGroupProject>();
            list = InputProjectNfFacade.ListGroupProject();
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamDeptPemohon> GetDeptPemohon()
        {
            List<InputProjectNf.ParamDeptPemohon> list = new List<InputProjectNf.ParamDeptPemohon>();
            list = InputProjectNfFacade.GetDeptPemohon();
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamAreaProject> ListAreaProject()
        {
            List<InputProjectNf.ParamAreaProject> list = new List<InputProjectNf.ParamAreaProject>();
            list = InputProjectNfFacade.ListAreaProject();
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamSatuan> ListSatuan()
        {
            List<InputProjectNf.ParamSatuan> list = new List<InputProjectNf.ParamSatuan>();
            list = InputProjectNfFacade.ListSatuan();
            return list;
        }

        [WebMethod]
        public static int GetBatasEstimasi()
        {
            int Msg = 0;
            string minBiaya = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurcnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            if (minBiaya !="") { Msg = int.Parse(minBiaya); }
            return Msg;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamListHead> ListHeadName()
        {
            List<InputProjectNf.ParamListHead> list = new List<InputProjectNf.ParamListHead>();
            list = InputProjectNfFacade.ListHeadName();
            return list;
        }

        [WebMethod]
        public static List<InputProjectNf.ParamSubArea> ListSubArea(string AreaProject)
        {
            List<InputProjectNf.ParamSubArea> list = new List<InputProjectNf.ParamSubArea>();
            list = InputProjectNfFacade.ListSubArea(AreaProject);
            return list;
        }

        [WebMethod]
        public static string SaveData(InputProjectNf.ParamHead isiHead)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            Users users = (Users)HttpContext.Current.Session["Users"];
            isiHead.CreatedBy = users.UserName;
            isiHead.FinishDate = DateTime.Parse(isiHead.Tanggal);
            isiHead.ProjectDate = DateTime.Parse(isiHead.Tanggal);
            string Bulan = "";
            int Bln = DateTime.Now.Month;
            if (Bln < 10) { Bulan = "0" + Bln; }
            else{ Bulan = Convert.ToInt32(Bln).ToString(); }
            string Bulan1 = Bulan.ToString();
            int Thn = DateTime.Now.Year;
            string Tahun = Thn.ToString().Substring(2, 2);
            int CountNumber = 1;
            string CountNo = "";
            string CountNo1 = "";
            string No = "";
            int resultDocNo = 0;
            int countDocNo = InputProjectNfFacade.CountDocNo(Bln, Thn);
            if (countDocNo > 0)
            {
                InputProjectNf.ParamDocNo docno = InputProjectNfFacade.DocNo(Bln, Thn);
                int Count1 = docno.Count;
                if (Count1 < 10) { CountNumber = 1 + Count1; CountNo = Convert.ToInt32(CountNumber).ToString(); }
                else { CountNumber = 1 + Count1; CountNo = Convert.ToInt32(CountNumber).ToString(); }
                if (CountNumber < 10) { CountNo1 = "0" + Convert.ToInt32(CountNo).ToString(); }
                else { CountNo1 = Convert.ToInt32(CountNo).ToString(); }
                No = "I" + CountNo1 + Bulan1 + Tahun;
                resultDocNo=InputProjectNfFacade.UpdateDocNo(docno.Id, CountNumber);
            }
            else
            {
                No = "I" + "01" + Bulan1 + Tahun;
                resultDocNo=InputProjectNfFacade.InsertDocNo(Bln, Thn, CountNumber);
            }
            isiHead.Nomor = No;
            Msg = No;
            int intResul = 0;
            absTrans = new InputProjectNfFacade(isiHead);
            intResul = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            return Msg;
        }
    }
}