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

namespace GRCweb1.Modul.MTC
{
    public partial class SerahTerimaProject_rev1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
        }

        [WebMethod]
        public static List<SerahTerimaNf.ParamProject> ListProject()
        {
            List<SerahTerimaNf.ParamProject> list = new List<SerahTerimaNf.ParamProject>();
            Users user = (Users)HttpContext.Current.Session["Users"];
            string[] SerahTerima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvSerahTerima", "EngineeringNew").Split(',');
            int Level = Array.IndexOf(SerahTerima, user.ID.ToString());
            string Levele = string.Empty;
            switch (Level)
            {
                case 0: Levele = " and mp.Approval=2"; break;
                case 1: Levele = " and mp.Approval=2"; break;
                case 2: Levele = " and mp.RowStatus=2"; break;
                default:
                    Levele = " and mp.Approval=3";
                    if (user.DeptID == 6 || user.DeptID == 10) { Levele += " and mp.DeptID in (6,10)"; }
                    else { Levele += " and mp.DeptID=" + user.DeptID.ToString(); }
                    break;
            }
            string where = "";
            if (user.DeptID == 7)
            {
                where =
                " and (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2 and mp.Approval=2 and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='' and ToDeptID=7) or " +
                " (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2 and mp.Approval=3 and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='' and ToDeptID=19 and DeptID=7) ";
            }
            else if (user.DeptID == 19)
            {
                where =
                " and (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2 and mp.Approval=2 and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='' and ToDeptID=19) or " +
                " (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2 and mp.Approval=3 and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='' and ToDeptID=7 and DeptID=19) ";
            }
            else
            {
                where =
                " and (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2  " + Levele + " and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='') or " +
                " (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2  " + Levele + " and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='') ";
            }
            if (user.Apv > 0) { list = SerahTerimaNfFacade.ListProject(where); }
            return list;
        }

        [WebMethod]
        public static List<SerahTerimaNf.ParamInfoProject> InfoProject(int Id)
        {
            List<SerahTerimaNf.ParamInfoProject> list = new List<SerahTerimaNf.ParamInfoProject>();
            list = SerahTerimaNfFacade.InfoProject(Id);
            return list;
        }

        [WebMethod]
        public static List<SerahTerimaNf.ParamInfoDetail> InfoDetail(int Id)
        {
            List<SerahTerimaNf.ParamInfoDetail> list = new List<SerahTerimaNf.ParamInfoDetail>();
            list = SerahTerimaNfFacade.InfoDetail(Id);
            return list;
        }

        [WebMethod]
        public static string ApproveData(SerahTerimaNf.ParamHead isiHead)
        {
            string Msg = "";
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans;
            Users users = (Users)HttpContext.Current.Session["Users"];

            string[] App = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, (users.ID.ToString()));
            string[] SerahTerima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvSerahTerima", "EngineeringNew").Split(',');
            int Level = Array.IndexOf(SerahTerima, (users.ID.ToString()));
            string LevelStatus = "3";
            int apv = SerahTerimaNfFacade.InfoApv(isiHead.Id);
            if (Level == 0) { LevelStatus = "2"; }//di serahkan dari engineering project sudah finish
            if (Level == 2) { LevelStatus = "3"; }// diterima oleh dept pemohon

            isiHead.Id = isiHead.Id;
            /*if (LevelApp == 2) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 3; }
            else if (LevelApp < 0) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 4; }
            else { isiHead.RowStatus = int.Parse(LevelStatus); }*/

            if (LevelApp == 2 && apv == 2) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 3; }
            if (LevelApp == 2 && apv == 3) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 4; }
            if (LevelApp == 5 && apv == 3) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 4; }
            if (LevelApp == 5 && apv == 2) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 3; }
            else if (LevelApp < 0) { isiHead.RowStatus = 2; isiHead.Status = 2; isiHead.Approval = 4; }
            else{ isiHead.RowStatus = int.Parse(LevelStatus); }

            isiHead.Status = (int.Parse(LevelStatus) < 2) ? int.Parse(LevelStatus) + 1 : 2;
            isiHead.FinishDate = DateTime.Parse(isiHead.TanggalSelesai);
            int intResul = 0;
            absTrans = new SerahTerimaNfFacade(isiHead);
            intResul = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); return absTrans.Error; }
            if (intResul > 0)
            {
                isiHead.CreatedBy = users.UserID;
                if (isiHead.Approval > 0)
                {
                    if (isiHead.Approval == 3) { isiHead.Statuse = "Diserahkan [Finish on : " + isiHead.TanggalSelesai + " ]"; }
                    else if (isiHead.Approval == 4) { isiHead.Statuse = "Diterima"; }
                    else { isiHead.Statuse = "Diketahui"; }
                }
                absTrans = new SerahTerimaNfFacade(isiHead);
                intResul = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty) { transManager.RollbackTransaction(); return absTrans.Error; }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            return Msg;
        }
    }
}