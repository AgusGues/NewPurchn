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

namespace GRCweb1.Modul.Pes
{
    public partial class RekapKpi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapKpiNf.ParamDept> ListDept()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string getDept = RekapKpiNfFacade.GetDept(users.ID);
            List<RekapKpiNf.ParamDept> list = new List<RekapKpiNf.ParamDept>();
            list = RekapKpiNfFacade.ListDept(getDept);
            return list;
        }

        [WebMethod]
        public static List<RekapKpiNf.ParamTahun> ListTahun()
        {
            List<RekapKpiNf.ParamTahun> list = new List<RekapKpiNf.ParamTahun>();
            list = RekapKpiNfFacade.ListTahun();
            return list;
        }

        [WebMethod]
        public static List<RekapKpiNf.ParamPic> ListPic(int dept)
        {
            List<RekapKpiNf.ParamPic> list = new List<RekapKpiNf.ParamPic>();
            list = RekapKpiNfFacade.ListPic(dept);
            return list;
        }

        [WebMethod]
        public static List<RekapKpiNf.ParamData> ListData(int dept, int user)
        {
            List<RekapKpiNf.ParamData> list = new List<RekapKpiNf.ParamData>();
            list = RekapKpiNfFacade.ListData(dept, user);
            return list;
        }

        [WebMethod]
        public static List<RekapKpiNf.ParamDataDtl> ListDataDtl(int dept, int user,int bulan, int tahun)
        {
            List<RekapKpiNf.ParamDataDtl> list = new List<RekapKpiNf.ParamDataDtl>();
            list = RekapKpiNfFacade.ListDataDtl(dept,user, bulan, tahun);
            return list;
        }

    }
}