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
    public partial class EstimasiMaterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static List<EstimasiMaterialNf.ParamDeptPemohon> ListDeptPemohon()
        {
            List<EstimasiMaterialNf.ParamDeptPemohon> list = new List<EstimasiMaterialNf.ParamDeptPemohon>();
            list = EstimasiMaterialNfFacade.ListDeptPemohon();
            return list;
        }

        [WebMethod]
        public static string ListData(string status, int dept, string nomor)
        {
            List<EstimasiMaterialNf.ParamData> list = new List<EstimasiMaterialNf.ParamData>();
            list = EstimasiMaterialNfFacade.GetListData(status, dept, nomor);
            return JsonConvert.SerializeObject(list);
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamDetail> ListDetail(int Id)
        {
            List<EstimasiMaterialNf.ParamDetail> list = new List<EstimasiMaterialNf.ParamDetail>();
            list = EstimasiMaterialNfFacade.GetListDetail(Id);
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamKodeProject> ListKodeProject()
        {
            List<EstimasiMaterialNf.ParamKodeProject> list = new List<EstimasiMaterialNf.ParamKodeProject>();
            list = EstimasiMaterialNfFacade.ListKodeProject();
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamTypeItem> ListTypeItem()
        {
            List<EstimasiMaterialNf.ParamTypeItem> list = new List<EstimasiMaterialNf.ParamTypeItem>();
            list = EstimasiMaterialNfFacade.ListTypeItem();
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamNameItem> ListNameItem(int type, string name)
        {
            List<EstimasiMaterialNf.ParamNameItem> list = new List<EstimasiMaterialNf.ParamNameItem>();
            list = EstimasiMaterialNfFacade.ListNameItem(type,name);
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamInfoItem> InfoNameItem(int item, int type)
        {
            List<EstimasiMaterialNf.ParamInfoItem> list = new List<EstimasiMaterialNf.ParamInfoItem>();
            list = EstimasiMaterialNfFacade.InfoNameItem(item, type);
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamListMaterial> ListMaterial(int Id)
        {
            List<EstimasiMaterialNf.ParamListMaterial> list = new List<EstimasiMaterialNf.ParamListMaterial>();
            list = EstimasiMaterialNfFacade.ListMaterial(Id);
            return list;
        }

        [WebMethod]
        public static List<EstimasiMaterialNf.ParamInfoKodeProject> InfoKodeProject(int Id)
        {
            List<EstimasiMaterialNf.ParamInfoKodeProject> list = new List<EstimasiMaterialNf.ParamInfoKodeProject>();
            list = EstimasiMaterialNfFacade.InfoKodeProject(Id);
            return list;
        }

        [WebMethod]
        public static string UpdateData(EstimasiMaterialNf.ParamHead isi)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            isi.Schedule = DateTime.Parse(isi.TanggalPakai);
            isi.RowStatus = 0;
            int intResul = 0;
            absTrans = new EstimasiMaterialNfFacade(isi);
            intResul = absTrans.Update(transManager);
            if (intResul != 0) { Msg = "1"; }
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return Msg;
        }

        [WebMethod]
        public static string AddData(EstimasiMaterialNf.ParamHead isi)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            isi.Schedule = DateTime.Parse(isi.TanggalPakai);
            isi.RowStatus = 0;
            int intResul = 0;
            absTrans = new EstimasiMaterialNfFacade(isi);
            intResul = absTrans.Insert(transManager);
            if (intResul != 0) { Msg = "1"; }
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return absTrans.Error;
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            return Msg;
        }

        [WebMethod]
        public static string DeleteData(int Id)
        {
            string Msg = "1";
            EstimasiMaterialNfFacade.DeleteData(Id);
            return Msg;
        }
    }
}