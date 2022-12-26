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
    public partial class UserPes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<UserPesNf.ParamData> ListData()
        {
            List<UserPesNf.ParamData> list = new List<UserPesNf.ParamData>();
            list = UserPesNffacade.ListData();
            return list;
        }

        [WebMethod]
        public static List<UserPesNf.ParamUserName> ListUserName(string UserName)
        {
            List<UserPesNf.ParamUserName> list = new List<UserPesNf.ParamUserName>();
            list = UserPesNffacade.ListUserName(UserName);
            return list;
        }

        [WebMethod]
        public static List<UserPesNf.ParamUnitKerja> ListUnitKerja(int type)
        {
            List<UserPesNf.ParamUnitKerja> list = new List<UserPesNf.ParamUnitKerja>();
            list = UserPesNffacade.ListUnitKerja(type);
            return list;
        }

        [WebMethod]
        public static List<UserPesNf.ParamCompany> ListCompany()
        {
            List<UserPesNf.ParamCompany> list = new List<UserPesNf.ParamCompany>();
            list = UserPesNffacade.ListCompany();
            return list;
        }

        [WebMethod]
        public static List<UserPesNf.ParamDepartment> ListDepartment()
        {
            List<UserPesNf.ParamDepartment> list = new List<UserPesNf.ParamDepartment>();
            list = UserPesNffacade.ListDepartment();
            return list;
        }

        [WebMethod]
        public static List<UserPesNf.ParamJabatan> ListJabatan(int dept)
        {
            List<UserPesNf.ParamJabatan> list = new List<UserPesNf.ParamJabatan>();
            list = UserPesNffacade.ListJabatan(dept);
            return list;
        }

        [WebMethod]
        public static string ProsesData(UserPesNf.ParamHead isi)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            Users users = (Users)HttpContext.Current.Session["Users"];
            int result = 0;
            isi.CreatedBy = users.UserName;
            isi.Password = "084183183007073185118172075177196105183108151150";
            int UserGroupID = UserPesNffacade.UserGroupID(isi.Jabatan);
            absTrans = new UserPesNffacade(isi);
            if (isi.Id == 0)
            {
                int CekUser = UserPesNffacade.CekUser(isi.UserName); if (CekUser == 0) { isi.UserID = -1; }
                int GetUserPes = UserPesNffacade.GetUserPes(isi.UserID); if (GetUserPes > 0) { return Msg; }
                int userid = isi.UserID; if (isi.UserID == -1) { userid = UserPesNffacade.MaxUserId()+1; }
                isi.UserID = userid;
                result = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); return absTrans.Error; }
                result = UserPesNffacade.AddUserGroup(isi.DepartmentName, isi.UserID, isi.Department, UserGroupID);
            }
            else
            {
                isi.LastModifiedBy= users.UserName;
                result = absTrans.Update(transManager);
                if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); return absTrans.Error; }
                result = UserPesNffacade.EditUserGroup(isi.DepartmentName, isi.UserID, isi.Department, UserGroupID);
            }
            Msg = "1";
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return Msg;
        }

        [WebMethod]
        public static string DeleteData(UserPesNf.ParamHead isi)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            Users users = (Users)HttpContext.Current.Session["Users"];
            int result = 0;
            isi.LastModifiedBy = users.UserName;
            absTrans = new UserPesNffacade(isi);
            result = absTrans.Delete(transManager);
            if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); return absTrans.Error; }
            Msg = "1";
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return Msg;
        }

    }
}