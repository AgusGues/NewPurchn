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
    public partial class RekapTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapTaskNf.ParamDept> ListDept()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string getDept = RekapTaskNfFacade.GetDept(users.ID);
            List<RekapTaskNf.ParamDept> list = new List<RekapTaskNf.ParamDept>();
            list = RekapTaskNfFacade.ListDept(getDept);
            return list;
        }

        [WebMethod]
        public static List<RekapTaskNf.ParamPic> ListPic(int dept)
        {
            List<RekapTaskNf.ParamPic> list = new List<RekapTaskNf.ParamPic>();
            list = RekapTaskNfFacade.ListPic(dept);
            return list;
        }

        [WebMethod]
        public static List<RekapTaskNf.ParamData> ListData(int dept, int user)
        {
            List<RekapTaskNf.ParamData> list = new List<RekapTaskNf.ParamData>();
            list = RekapTaskNfFacade.ListData(dept, user);
            return list;
        }

        [WebMethod]
        public static List<RekapTaskNf.ParamDataDtl> ListDataDtl(int user, string DariTanggal, string SampaiTanggal, int Status)
        {
            string FromDate = DateTime.Parse(DariTanggal).ToString("yyyyMMdd");
            string ToDate = DateTime.Parse(SampaiTanggal).ToString("yyyyMMdd");
            List<RekapTaskNf.ParamDataDtl> list = new List<RekapTaskNf.ParamDataDtl>();
            list = RekapTaskNfFacade.ListDataDtl(user, FromDate, ToDate, Status);
            return list;
        }

    }
}