using BusinessFacade;
using Domain;
using Factory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Maintenance
{
    public partial class Aproval_Project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ProjectLevelApp> LoadOpenImprovement()
        {
            
            List<List_Project> ListProject = new List<List_Project>();
            List<ProjectLevelApp> ProjectLevelApp = new List<ProjectLevelApp>();
            Users user = (Users)HttpContext.Current.Session["Users"];
            string[] App = new Inifiles(HostingEnvironment.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)HttpContext.Current.Session["Users"]).ID.ToString());

            string maxBiaya = new Inifiles(HostingEnvironment.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            string LastModifiedBy = new Inifiles(HostingEnvironment.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastModifiedBy", "EngineeringNew");
            int UserApv = user.Apv;
            int UserDept = user.DeptID;
            string UserDept1 = user.DeptID.ToString();

            if (UserDept == 10 || UserDept == 6)
            {
                UserDept1 = " in (6,10)";
            }
            else if (UserDept == 4)
            {
                UserDept1 = " in (4,5,18,19)";
            }
            else
            {
                UserDept1 = "in (" + UserDept.ToString() + ")";
            }

            string NoProject = "";
            string QueryAja = "";


            #region Manager Dept Pemohon
            if (LevelApp < 0) // Manager Dept Pemohon
            {
                string tambah = "";
                string tambah2 = "";
                string tambah3 = "(mp.Status=0 and mp.rowstatus=0 and mp.Approval in (1,2) and mp.LastModifiedBy is null and " +
                                 " mp.DeptID " + UserDept1 + " and mp.Rowstatus>-1 " + QueryAja + ") or " +
                                 "(mp.Status=1 and mp.rowstatus=1 and mp.Approval=1 " +
                                 " and (mp.VerDate is null or mp.VerDate = 0 or mp.VerDate = -2) and mp.LastModifiedBy in ('" + LastModifiedBy + "') " + QueryAja + ") " +
                                 " and mp.DeptID " + UserDept1 + " and mp.rowstatus>-1 ";
                string tambah4 = "";
                string tambah5 = " and mp.Nomor=" + NoProject + " ";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4);
            }
            #endregion
            else if (LevelApp == 3) //PM
            {
                string tambah = " ";
                string tambah2 = " ";
                string tambah3 = " ((mp.Status =0 and mp.rowstatus=1 and mp.Approval=1 and  (mp.ApvPM is null or mp.ApvPM = 0)) " +
                                 " or (mp.Status =1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=1) " +
                                 " or (mp.Status =2 and mp.rowstatus=2 and mp.Approval=3 and mp.ApvPM=2 and mp.release=1 and mp.DeptID in (4,5,18)) " +
                                 " or (mp.Status =2 and mp.rowstatus=2 and mp.Approval=4 and mp.ApvPM=2 and mp.release=1)) and mp.rowstatus>-1 order by mp.ApvPM";
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4);
            }
            #region Head Eng
            else if (LevelApp == 1) // Head Enginering
            {
                string nomorP = NoProject;
                string tambah2 = "";
                if (nomorP == "")
                {
                    tambah2 = " and mp.LastModifiedBy is not null ";
                }
                else
                {
                    tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                }
                string tambah = " ";
                string tambah22 = tambah2;


                string tambah31 =
                " ((mp.Status = 0 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.ToDate='2000-11-11' and mp.Biaya=0 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.VerDate=-2 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-1 and mp.VerDate=1 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-2 and mp.VerDate=1 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null))) ";

                string tambah3 = tambah31;
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4);
            }
            #endregion

            #region Head GA
            else if (LevelApp == 0) // Head GA
            {
                string nomorP = NoProject;
                string tambah2 = "";
                if (nomorP == "")
                {
                    tambah2 = " and mp.LastModifiedBy is not null ";

                }
                else
                {
                    tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                }
                string tambah = " ";
                string tambah22 = tambah2;

                string tambah31 =
                " ((mp.Status = 0 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.ToDate='2000-11-11' and mp.Biaya=0 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.VerDate=-2 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-1 and mp.VerDate=1 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-2 and mp.VerDate=1 and mp.ToDeptID=" + user.DeptID + ")) ";

                string tambah3 = tambah31;
                string tambah4 = "";


                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4);
            }
            #endregion

            #region MGR MTN
            else if (LevelApp == 2) // MGR MTN 
            {
                string nomorP = NoProject;
                string tambah2 = "";

                if (nomorP == "")
                {
                    tambah2 = " and mp.LastModifiedBy is not null ";
                }
                else
                {
                    tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                }
                //string tambah = " ";
                string tambah22 = tambah2;

                string tambah = " ";
                string tambah3 =
                " ((mp.Status =1 and mp.rowstatus=1 and mp.Approval=1 and mp.LastModifiedBy is not null and (mp.ToDeptID=19 or mp.ToDeptID is null)) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval in (1,2) and mp.DeptID in (4,5,18) and mp.LastModifiedBy is null and (mp.ToDeptID=19 or mp.ToDeptID is null)) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval=1 and mp.DeptID in (4,5,18,19) and mp.ToDeptID<>19 and mp.LastModifiedBy is null) ) ";
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4);
            }
            #endregion

            #region MGR GA
            else if (LevelApp == 5)  // MGR GA 
            {
                string tambah = " ";
                string tambah2 = "";
                string tambah3 =
                " ((mp.Status =1 and mp.rowstatus=1 and mp.Approval=1 and mp.LastModifiedBy is not null and mp.ToDeptID=7) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval=1 and mp.DeptID=7 and mp.ToDeptID<>7 and mp.LastModifiedBy is null) ) ";
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4);
            }
            #endregion

            #region Direksi
            else if (LevelApp == 4) //Direksi
            {
                string tambah = " ";
                string tambah2 = " and mp.LastModifiedBy <> 'Vero' ";
                string tambah3 = " mp.Status =2 and mp.rowstatus=2 and mp.Approval=2 and mp.ApvPM=2 and mp.Biaya > " + decimal.Parse(maxBiaya) + "  ";
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4);
            }
            #endregion

            #region Head MTN
            else if (LevelApp < 0 && user.Apv > 0 && user.DeptID == 5 || LevelApp < 0 && user.Apv > 0 && user.DeptID == 18 || LevelApp < 0 && user.Apv > 0 && user.DeptID == 4) // Head MTN 
            {
                string tambah = " DeptID=" + UserDept + " and ";
                string tambah2 = " and mp.LastModifiedBy is null ";
                string tambah3 = " and ((mp.Status =0 and mp.rowstatus=0 and mp.Approval in (1) or (mp.Status =1 and mp.rowstatus=1 and mp.Approval in (1))) ";
                string tambah4 = "";

                ListProject = MTC_ProjectFacade.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4);
            }
            #endregion


            ProjectLevelApp.Add(new ProjectLevelApp { LevelApp = LevelApp, ListProject= ListProject, MaxBiaya = Int32.Parse(maxBiaya), UserApv = user.Apv, UserDeptID = UserDept }) ;

            return ProjectLevelApp;
        }

        [WebMethod]
        public static string LoadMaterial (int improve)
        {
            List<MaterialEstimasi> ListMaterial = new List<MaterialEstimasi>();
            ListMaterial = MTC_ProjectFacade.RetrieveEstimasiMaterial(improve);
            return JsonConvert.SerializeObject(ListMaterial);

        }

        [WebMethod]
        public static int CekEstimasiMaterial(string improve)
        {
            int TotalMaterial;
            TotalMaterial = MTC_ProjectFacade.CekEstimasiMaterial(improve);
            return TotalMaterial;
        }

        [WebMethod]
        public static int ApproveProject(List_Project approve)
        {
            int status;
            Users user = (Users)HttpContext.Current.Session["Users"];
            string username = user.UserName;
            MTC_ProjectFacade MTC_Project = new MTC_ProjectFacade();
            status = MTC_Project.Approval(approve, username);
            return status;
        }

        [WebMethod]
        public static int Reschedule(int ID)
        {
            int status;
            Users user = (Users)HttpContext.Current.Session["Users"];
            string username = user.UserName;
            DateTime lastmodifiedtime = DateTime.Now;
            MTC_ProjectFacade MTC_Project = new MTC_ProjectFacade();
            status = MTC_Project.ReSchProject(ID, username, lastmodifiedtime);
            return status;
        }

        [WebMethod]
        public static int CancelProject(int ID)
        {
            int status;
            Users user = (Users)HttpContext.Current.Session["Users"];
            string[] App = new Inifiles(HostingEnvironment.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)HttpContext.Current.Session["Users"]).ID.ToString());
            string CancelBy = null;
            DateTime lastmodifiedtime = DateTime.Now;

            if (LevelApp < 0)
            {
                CancelBy = "CancelBy-" + user.UserName.Trim();
            }
            else if (LevelApp == 3)
            {
                CancelBy = "NotApprovedBy-" + user.UserName.Trim();
            }
            else if (LevelApp == 1)
            {
                CancelBy = "CancelBy-" + user.UserName.Trim();
            }

            MTC_ProjectFacade MTC_Project = new MTC_ProjectFacade();
            status = MTC_Project.CancelProject(ID, CancelBy, lastmodifiedtime);
            return status;
        }


        [WebMethod]
        public static int CancelProjectEst(int ID)
        {
            int status;
            Users user = (Users)HttpContext.Current.Session["Users"];
            string[] App = new Inifiles(HostingEnvironment.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)HttpContext.Current.Session["Users"]).ID.ToString());
            string CancelBy = null;
            DateTime lastmodifiedtime = DateTime.Now;

            if (LevelApp < 0)
            {
                CancelBy = "CancelBy-" + user.UserName.Trim();
            }
            else if (LevelApp == 3)
            {
                CancelBy = "NotApprovedBy-" + user.UserName.Trim();
            }
            else if (LevelApp == 1)
            {
                CancelBy = "CancelBy-" + user.UserName.Trim();
            }

            MTC_ProjectFacade MTC_Project = new MTC_ProjectFacade();
            status = MTC_Project.CancelProjectPM2(ID, CancelBy, lastmodifiedtime);
            return status;
        }

    }
}