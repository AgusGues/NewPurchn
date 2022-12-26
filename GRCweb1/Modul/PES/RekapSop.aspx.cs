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
    public partial class RekapSop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapSopNf.ParamDept> ListDept()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string getDept = RekapSopNfFacade.GetDept(users.ID);
            List<RekapSopNf.ParamDept> list = new List<RekapSopNf.ParamDept>();
            list = RekapSopNfFacade.ListDept(getDept);
            return list;
        }

        [WebMethod]
        public static List<RekapSopNf.ParamTahun> ListTahun()
        {
            List<RekapSopNf.ParamTahun> list = new List<RekapSopNf.ParamTahun>();
            list = RekapSopNfFacade.ListTahun();
            return list;
        }

        [WebMethod]
        public static List<RekapSopNf.ParamPic> ListPic(int dept)
        {
            List<RekapSopNf.ParamPic> list = new List<RekapSopNf.ParamPic>();
            list = RekapSopNfFacade.ListPic(dept);
            return list;
        }

        [WebMethod]
        public static List<RekapSopNf.ParamData> ListData(int dept, int user)
        {
            List<RekapSopNf.ParamData> list = new List<RekapSopNf.ParamData>();
            list = RekapSopNfFacade.ListData(dept, user);
            return list;
        }

        [WebMethod]
        public static List<RekapSopNf.ParamDataDtl> ListDataDtl(int dept, int user, int bulan, int tahun)
        {
            List<RekapSopNf.ParamDataDtl> list = new List<RekapSopNf.ParamDataDtl>();
            list = RekapSopNfFacade.ListDataDtl(dept, user, bulan, tahun);
            return list;
        }
    }
}