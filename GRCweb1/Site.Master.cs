using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Data.SqlClient;




namespace GRCweb1
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        //private int intUserID = 17; //for admin level
        private int intUserID = 0;
        [WebMethod]
        public static string getTime()
        {
            return DateTime.Now.ToString("hh:mm:ss tt");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                if (Session["Users"] == null)
                {
                    CekUsers();
                }
                else
                {
                   
                    intUserID = users.ID;
                }
                string[] Info = Global.ConnectionString().Split(new string[] { ";" }, StringSplitOptions.None);
                string[] Source = Info[1].Split('=');
                string[] Database = Info[0].Split('=');
                versions.ToolTip = Database[1] + "\n" + Source[1] + "\n" + ((Users)Session["Users"]).UserName + "\n" + HttpContext.Current.Request.UserHostAddress;
                CallVisiableMenu();

                //HrefClick();
                UpdateActiveClass();
                SayWelcome();
                SayInfoPath();
                JumlahNotif();
                updx();
                //Depo depo = new Depo();
                //UsersFacade usersFacade = new UsersFacade();
                //DepoFacade depoFacade = new DepoFacade();
                //depo = depoFacade.RetrieveById(users.UnitKerjaID);
                //if (depoFacade.Error == string.Empty)
                //{
                //    if (depo.ID > 0)
                //    {
                //        Label2.Text = depo.DepoName;
                //    }
                //}
                //SayCompanyCode();
            }

            SayCompanyCode();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CallVisiableMenu()
        {
            //Ini bisa
            //((Control)this.FindControl("li_02_MasterUsers")).Visible = false;
            //base.OnPreRender(e);

            //Init juga bisa
            //Control ctrl = (Control)this.FindControl("li_02_MasterUsers"); // or some other function that identifies children objects
            //                                                               // we change the status of the object
            //ctrl.Visible = false;

            //Progress Visiable Menu False
            if (Session["arrMenuVisiableFalse"] == null)
            {
                RulesFacade rulesFacade = new RulesFacade();
                ArrayList arrMenuVisiableFalse = new ArrayList();
                ArrayList arrRulesUser = rulesFacade.RetrieveByUserID(intUserID);
                rulesFacade = new RulesFacade();
                ArrayList arrRulesAll = rulesFacade.RetrieveByAllMenuActive();

                if (rulesFacade.Error == string.Empty && arrRulesAll.Count > 0)
                {
                    foreach (Rules rulesAll in arrRulesAll)
                    {
                        int flagRules = 0;
                        if (rulesAll.RuleName.ToUpper() == "PARSIAL DELIVERY ORDER")
                        {
                            string test = "";
                        }
                        string strRulesName = rulesAll.RuleName;
                        string strIDname = rulesAll.IDname;

                        if (arrRulesUser.Count > 0)
                            Session["UserMenuLevel"] = arrRulesUser;
                        else
                            Session["UserMenuLevel"] = null;

                        foreach (Rules rulesUser in arrRulesUser)
                        {
                            if (rulesUser.IDname == strIDname)
                            {
                                flagRules = 1;
                            }
                               
                        }
                        if (flagRules == 0)
                        {
                            arrMenuVisiableFalse.Add(rulesAll);
                        }
                    }

                    if (arrMenuVisiableFalse.Count > 0)
                    {
                        Session["arrMenuVisiableFalse"] = arrMenuVisiableFalse;

                        foreach (Rules rulesUser in arrMenuVisiableFalse)
                        {
                            string strLevel1 = string.Empty;
                            string strLevel2 = string.Empty;
                            string strLevel3 = string.Empty;
                            if (rulesUser.RuleName.ToUpper() == "PARSIAL DELIVERY ORDER")
                            {
                                string test = "";
                            }
                            if (rulesUser.IDname.Trim().ToUpper() == "MASTERROLERULES" || rulesUser.IDname.Trim().ToUpper() == "MASTERRULES")
                            {
                                String STRD = "";
                            }
                            if (rulesUser.Level == 1)
                                strLevel1 = "li_01_" + rulesUser.IDname.Trim();
                            else if (rulesUser.Level == 2)
                                strLevel1 = "li_02_" + rulesUser.IDname.Trim();
                            else if (rulesUser.Level == 3)
                                strLevel1 = "li_03_" + rulesUser.IDname.Trim();

                            Control ctrl = (Control)this.FindControl(strLevel1);
                            if (ctrl != null)
                            {
                                //utk visiable false di Master Page
                                ctrl.Visible = false;
                            }

                        }

                    }
                }
            }
            else
            {
                ArrayList arrMenuVisiableFalse = (ArrayList)Session["arrMenuVisiableFalse"];

                foreach (Rules rulesUser in arrMenuVisiableFalse)
                {
                    string strLevel1 = string.Empty;
                    string strLevel2 = string.Empty;
                    string strLevel3 = string.Empty;

                    if (rulesUser.Level == 1)
                        strLevel1 = "li_01_" + rulesUser.IDname.Trim();
                    else if (rulesUser.Level == 2)
                        strLevel1 = "li_02_" + rulesUser.IDname.Trim();
                    else if (rulesUser.Level == 3)
                        strLevel1 = "li_03_" + rulesUser.IDname.Trim();

                    Control ctrl = (Control)this.FindControl(strLevel1);
                    if (ctrl != null)
                    {
                        //utk yang udah dibuat di Master HTML utk di false
                        ctrl.Visible = false;
                    }
                }
            }

        }
        private void SayHeaderModal()
        {

            //StringBuilder sayHeaderModalHtml = new StringBuilder();
            //string str1 = string.Empty;
            //string str2 = string.Empty;
            //string str3 = string.Empty;
            //string strHeader = string.Empty;

            //str1 = @"<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""> ";
            //sayHeaderModalHtml.AppendLine(str1);
            //str2 = @"<span aria-hidden=""true"">&times;</span></button> ";
            //sayHeaderModalHtml.AppendLine(str2);

            //if (Session["sayHeaderModalFormChild"] != null)
            //{
            //    strHeader = Session["sayHeaderModalFormChild"].ToString();
            //    str3 = @"<h4 class=""modal-title"">" + strHeader + "</h4> ";
            //}
            //else
            //{
            //    strHeader = "Confirmation Header";
            //    str3 = @"<h4 class=""modal-title"">" + strHeader + "</h4> ";
            //}
            //sayHeaderModalHtml.AppendLine(str3);
            //Header_Modal.InnerHtml = sayHeaderModalHtml.ToString();
        }
        //Add By Razib 16-11-2022 
        private void JumlahNotif()
        {
            Users user = ((Users)Session["Users"]);
            int exID = 0;
            if (user.UnitKerjaID == 1)
            {
                exID = 1;
            }
            else if (user.UnitKerjaID == 7)
            {
                exID = 7;
            }
            else if (user.UnitKerjaID == 13)
            {
                exID = 13;
            }
            CompanyFacade JmlSharex = new CompanyFacade();
            int TotalShare = JmlSharex.RetrieveTotalSHareUPD(exID, user.DeptID);
            decimal TotalShare2 = Convert.ToDecimal(TotalShare);
            JmlhNotifShare.Text = "" + " " + TotalShare.ToString();
            updshareNot.Text = "UPD Share Notifikasi";
        }
        public int newdata = 0;
        private void updx()
        {
            Users user = ((Users)Session["Users"]);
            int exID = 0;
            if (user.UnitKerjaID == 1)
            {
                exID = 1;
            }
            else if (user.UnitKerjaID == 7)
            {
                exID = 7;
            }
            else if (user.UnitKerjaID == 13)
            {
                exID = 13;
            }
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string where = (user.DeptID == 23) ? "" : " and Dept=" + user.DeptID;
            zl.CustomQuery = "select top 5 * from ISO_UpdDMD where PlantID <>" + exID + " and StatusShare=1 and RowStatus>-1 " + where + "";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Company
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoDocument = sdr["NoDocument"].ToString(),
                        DocName = sdr["DocName"].ToString()
                    });
                }
            }
            TryShareMenu.DataSource = arrData;
            TryShareMenu.DataBind();
            //CompanyFacade JmlSharex = new CompanyFacade();
            //int TotalShare = JmlSharex.RetrieveTotalSHareUPD(exID, user.DeptID);
        }
        //end
        private void SayWelcome()
        {
            //nanti ambil dari ((Users)Session["Users"]).UserName
            StringBuilder sayWelcomeHtml = new StringBuilder();
            string sayWelcome = string.Empty;
            if (((Users)Session["Users"]).UserName == string.Empty)
                sayWelcome = @"<small>Welcome,</small> " + " Guys";
            else
                sayWelcome = @"<small>Welcome,</small> " + ((Users)Session["Users"]).UserName;

            sayWelcomeHtml.AppendLine(sayWelcome);
            WelcomeSpan.InnerHtml = sayWelcomeHtml.ToString();
        }
        private void SayCompanyCode()
        {
            //nanti ambil dari ((Users)Session["Users"]).UserName
            string strPeriod = string.Empty;
            string strCompanyCode = string.Empty;

            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(((Users)Session["Users"]).UnitKerjaID );
            if (company.ID > 0)
            {
                strCompanyCode = company.Lokasi  ;
            }


            //GL_ParameterFacade glparamf = new GL_ParameterFacade();
            //string strperiod = string.Empty;
            //strperiod = glparamf.retrieveByCode("period", ((Users)Session["Users"]).CompanyCode);

            //if (strperiod.Substring(4, 2) == "01")
            //    strperiod = "Januari-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "02")
            //    strperiod = "Februari-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "03")
            //    strperiod = "Maret-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "04")
            //    strperiod = "April-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "05")
            //    strperiod = "Mei-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "06")
            //    strperiod = "Juni-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "07")
            //    strperiod = "Juli-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "08")
            //    strperiod = "Agustus-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "09")
            //    strperiod = "September-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "10")
            //    strperiod = "Oktober-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "11")
            //    strperiod = "November-" + strperiod.Substring(0, 4);
            //else if (strperiod.Substring(4, 2) == "12")
            //    strperiod = "Desember-" + strperiod.Substring(0, 4);


            StringBuilder sayCompanyCodeHtml = new StringBuilder();
            //string sayWelcome = @"<span class=""label label-xlg label-success arrowed-in arrowed-in-right"">Company : " + strCompanyCode + "</span>";
            string sayWelcome = @"<span class=""label label-xlg label-success arrowed-in-right"">Plant : " + strCompanyCode + "</span>";
            sayCompanyCodeHtml.AppendLine(sayWelcome);

            //string sayPeriodBook = @"<span class=""label label-xlg label-purple arrowed"">Period : " + strperiod + "</span>";
            //sayCompanyCodeHtml.AppendLine(sayPeriodBook);
            
            pCompanyCode.InnerHtml = sayCompanyCodeHtml.ToString();
        }
        private void CekUsers()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users = usersFacade.RetrieveById(intUserID);
            if (users.ID > 0)
            {
                Session["Users"] = users;
                intUserID = users.ID;
            }
            else
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();

                Response.Redirect("~/Default.aspx");
                //Response.Redirect("~/Login.aspx");
            }
        }

        private void SayInfoPath()
        {
            string level1 = string.Empty;
            string level2 = string.Empty;
            string level3 = string.Empty;
            string level4 = string.Empty;
            string level5 = string.Empty;
            int maxLoop = 0;
            string strInfoModul = string.Empty;

            if (Session["RuleName1"] != null)
            {
                level1 = Session["RuleName1"].ToString();
                maxLoop = 1;
            }
            if (Session["RuleName2"] != null)
            {
                level2 = Session["RuleName2"].ToString();
                maxLoop = 2;
            }
            if (Session["RuleName3"] != null)
            {
                level3 = Session["RuleName3"].ToString();
                maxLoop = 3;
            }

            if (maxLoop > 0)
            {
                string path1 = string.Empty; string path2 = string.Empty; string path3 = string.Empty;
                string path4 = string.Empty; string path5 = string.Empty; string path0 = string.Empty; string path6 = string.Empty;

                StringBuilder sayInfoPathHtml = new StringBuilder();

                //path0 = @"<ul class=""breadcrumb"">  <li> <i class=""ace-icon fa fa-home home-icon""> </i> <a href = ""?HrefOnClick=Home.aspx"" > Home </a> </li> ";
                //sayInfoPathHtml.AppendLine(path0);

                for (int i = 1; i < maxLoop + 1; i++)
                {
                    if (level1 != string.Empty && i == 1)
                    {
                        //path1 = @"<li> <a href=""#""> Forms </a> </li>";
                        path1 = @"<a href=""#"">" + level1 + "</a>";
                        sayInfoPathHtml.AppendLine(path1);

                        strInfoModul = level1;
                    }
                    if (level2 != string.Empty && i == 2)
                    {
                        if (maxLoop > 2)
                            path2 = @"<li><a href=""#"">" + level2 + "</a></li> ";
                        else
                            path2 = @"<li class=""active""><strong>" + level2 + "</strong></li> ";
                        sayInfoPathHtml.AppendLine(path2);

                        strInfoModul = level2;
                    }
                    if (level3 != string.Empty && i == 3)
                    {
                        path3 = @"";
                        path3 = @"<li class=""active""><strong>" + level3 + "</strong></li> ";

                        sayInfoPathHtml.AppendLine(path3);

                        strInfoModul = level3;
                    }
                    if (level4 != string.Empty && i == 4)
                    {
                        path4 = @"";
                        sayInfoPathHtml.AppendLine(path4);

                        strInfoModul = level4;
                    }
                    if (level5 != string.Empty && i == 5)
                    {
                        path5 = @"";
                        sayInfoPathHtml.AppendLine(path5);

                        strInfoModul = level5;
                    }

                }

                InfoPath.InnerHtml = sayInfoPathHtml.ToString();

                //if (Session["RuleName1"] == null && Session["RuleName2"] == null && Session["RuleName3"] == null)
                //    strInfoModul = "Dashboard";

                //StringBuilder sayInfoModulHtml = new StringBuilder();
                //string sayInfoModul = strInfoModul + @"<small> <i class=""ace-icon fa fa-angle-double-right""></i>" + " Input / Update " + strInfoModul + "</small>";
                //sayInfoModulHtml.AppendLine(sayInfoModul);
                //InfoModul.InnerHtml = sayInfoModulHtml.ToString();
            }
            else
            {
                //if (Session["RuleName1"] == null && Session["RuleName2"] == null && Session["RuleName3"] == null)
                //    strInfoModul = "Dashboard";

                //StringBuilder sayInfoModulHtml = new StringBuilder();
                //string sayInfoModul = strInfoModul + @"<small> <i class=""ace-icon fa fa-angle-double-right""></i> " + " Overview & Stats" + "</small>";
                //sayInfoModulHtml.AppendLine(sayInfoModul);

                //InfoModul.InnerHtml = sayInfoModulHtml.ToString();
            }

        }
        private void HrefClick()
        {
            //awal buka ini session kosongkan aja / maybe

            string hrefOnClick = string.Empty;
            string strHrefOnClick = string.Empty;
            string test = string.Empty;
           int intLevel = 0;

            //check to see if the id in the querystring exists and that it parses as an int.
            if (Request.QueryString["HrefOnClick"] != null)
            {
                strHrefOnClick = Request.QueryString["HrefOnClick"].ToString();
                Session["PilihMenuDong"] = strHrefOnClick;
                
                RulesFacade rulesFacade = new RulesFacade();

                if (strHrefOnClick == "Home.aspx")
                //if (strHrefOnClick == "Default.aspx")
                {
                    hrefOnClick = "~/Home.aspx";
                    //hrefOnClick = "~/Default.aspx";

                    Session["MenuLevel1"] = null;
                    Session["MenuLevel2"] = null;
                    Session["MenuLevel3"] = null;

                    //utk SayInfoPath
                    Session["RuleName1"] = null;
                    Session["RuleName2"] = null;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_01_")
                {
                    Session["MenuLevel1"] = strHrefOnClick.Substring(6, strHrefOnClick.Length - 6);
                    Session["MenuLevel2"] = null;
                    Session["MenuLevel3"] = null;
                    intLevel = 1;

                    //cari level 1
                    string idNameLevel1 = strHrefOnClick.Substring(0, strHrefOnClick.Length);

                    Rules RuleName1 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 6));
                    hrefOnClick = RuleName1.Href;

                    //utk SayInfoPath
                    Session["RuleName1"] = RuleName1.RuleName;
                    Session["RuleName2"] = null;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_02_")
                {
                    intLevel = 2;
                    string idNameLevel2 = strHrefOnClick.Substring(0, strHrefOnClick.Length);

                    Rules RuleName2 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 6));
                    //string strSort = RuleName2.Sort.Substring(0, 1);
                    string strSort = RuleName2.Sort;
                    hrefOnClick = RuleName2.Href;
                    rulesFacade = new RulesFacade();

                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 1
                    Rules idNameLevel1 = rulesFacade.RetrieveBySortAndLevel(strSort, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    //menu active level 1
                    Session["MenuLevel1"] = "li_01_" + idNameLevel1.IDname;
                    Session["MenuLevel2"] = idNameLevel2;
                    Session["MenuLevel3"] = null;
                    //utk SayInfoPath
                    Session["RuleName1"] = idNameLevel1.RuleName;
                    Session["RuleName2"] = RuleName2.RuleName;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_03_")
                {
                    intLevel = 3;
                    string idNameLevel3 = strHrefOnClick.Substring(0, strHrefOnClick.Length);

                    Rules RuleName3 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 6));
                    //string strSort2 = RuleName3.Sort.Substring(0, 3);
                    string strSort2 = RuleName3.Sort;
                    hrefOnClick = RuleName3.Href;
                    rulesFacade = new RulesFacade();

                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 2
                    Rules idNameLevel2 = rulesFacade.RetrieveBySortAndLevel(strSort2, 2);
                    string strSort1 = idNameLevel2.Sort.Substring(0, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    rulesFacade = new RulesFacade();
                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 1
                    Rules idNameLevel1 = rulesFacade.RetrieveBySortAndLevel(strSort1, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    //menu active level 1
                    Session["MenuLevel1"] = "li_01_" + idNameLevel1.IDname;
                    Session["MenuLevel2"] = "li_02_" + idNameLevel2.IDname;
                    Session["MenuLevel3"] = idNameLevel3;

                    //utk SayInfoPath
                    Session["RuleName1"] = idNameLevel1.RuleName;
                    Session["RuleName2"] = idNameLevel2.RuleName;
                    Session["RuleName3"] = RuleName3.RuleName;
                }
                else
                {
                    //info messagebox gak ada on Rules / error

                }

                if (hrefOnClick == "~/Modul/Sales/InputOrderPenjualan.aspx")
                    hrefOnClick = "~/Modul/Sales/WebForm2.aspx";

                //Response.Redirect("Contact.aspx");
                Response.Redirect(hrefOnClick);
            }
            else
            {
            }

        }
        private void UpdateActiveClass()
        {
            string level1R = string.Empty; string level2R = string.Empty; string level3R = string.Empty;

            //krn masih awal & by default <li id="li_01_Dashboard" runat="server" class="active">
            //active duluan
            if (Session["MenuLama"] == null || Session["PilihMenuDong"] == null)
            {
                //yg awal = li_01_Dashboard yg active di-off
                li_01_Dashboard.Attributes.Remove("class");
                li_01_Dashboard.Attributes.Add("class", "");

                Session["MenuLama"] = "li_01_Dashboard";
            }
            else
            {
                if (Session["PilihMenuDong"].ToString() != "li_01_Dashboard")
                {
                    //yg awal = li_01_Dashboard yg active di-off
                    li_01_Dashboard.Attributes.Remove("class");
                    li_01_Dashboard.Attributes.Add("class", "");
                }
            }

            if (Session["MenuLevel1"] != null)
            {
                level1R = Session["MenuLevel1"].ToString();// + @"Attributes.Remove(""class"")";

                System.Web.UI.HtmlControls.HtmlGenericControl myliDashboard = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl(level1R);

                if (myliDashboard != null)
                {
                    //level 1, active open
                    if (Session["MenuLevel2"] == null)
                        myliDashboard.Attributes["class"] = "active";
                    else
                        myliDashboard.Attributes["class"] = "active open";

                }

                ////////
                Session["MenuLama"] = level1R;
            }
            if (Session["MenuLevel2"] != null)
            {
                level2R = Session["MenuLevel2"].ToString();// + @"Attributes.Remove(""class"")";
                System.Web.UI.HtmlControls.HtmlGenericControl myliDashboard2 = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl(level2R);

                if (myliDashboard2 != null)
                {
                    //level 2, active aja
                    //myliDashboard2.Attributes.Remove("class");
                    //myliDashboard2.Attributes.Add("class", "active");
                    if (Session["MenuLevel3"] == null)
                        myliDashboard2.Attributes["class"] = "active";
                    else
                        myliDashboard2.Attributes["class"] = "active open";
                }

                ////////
                Session["MenuLama"] = level2R;
            }
            if (Session["MenuLevel3"] != null)
            {
                level3R = Session["MenuLevel3"].ToString();// + @"Attributes.Remove(""class"")";

                System.Web.UI.HtmlControls.HtmlGenericControl myliDashboard3 = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Page.Master.FindControl(level3R);

                if (myliDashboard3 != null)
                {
                    //level 3, active aja
                    myliDashboard3.Attributes["class"] = "active";
                }

                ////////
                Session["MenuLama"] = level3R;
            }

            //if (Session["PilihMenuDong"] == null || Session["PilihMenuDong"].ToString() == "Default.aspx")
            if (Session["PilihMenuDong"] == null || Session["PilihMenuDong"].ToString() == "Home.aspx")
            {
                string aa = string.Empty;
                if (Session["PilihMenuDong"] != null)
                    aa = Session["PilihMenuDong"].ToString();

                li_01_Dashboard.Attributes.Remove("class");
                li_01_Dashboard.Attributes.Add("class", "active");

                //Session["MenuLama"] = "Default.aspx";
                Session["MenuLama"] = "Home.aspx";
            }
        }
        protected void cbJuornalStyle1_click(object sender, EventArgs e)
        {
            string test = string.Empty;
        }

        protected void cbJuornalStyle1_ServerChange(object sender, EventArgs e)
        {
            string test = string.Empty;

        }

        protected void btnLogout_ServerClick(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Default.aspx");
            //Response.Redirect("~/Login.aspx");

        }
        protected void MyKlik(object sender, EventArgs e)
        {
            var anchor = sender as HtmlAnchor;
            if (anchor == null)
                return;

            var href = anchor.HRef;
            var hrefID = anchor.ID;

            string test = string.Empty;
            //--do something
            string hrefOnClick = string.Empty;
            string strHrefOnClick = string.Empty;

            //check to see if the id in the querystring exists and that it parses as an int.
            if (hrefID != null)
            {
                strHrefOnClick = hrefID.ToString();

                string strHrefOnClickLama = string.Empty;
                if (Session["PilihMenuDong"] != null)
                    strHrefOnClickLama = Session["PilihMenuDong"].ToString();

                if (strHrefOnClickLama != strHrefOnClick)
                {
                    //untuk hapus session pindah form yg namanya mungkin sama
                    Session.Remove("ListJournal");
                    Session.Remove("JournalDialogNoLoop");
                    Session.Remove("JournalDialog");
                    Session.Remove("JournalHeader");
                    Session.Remove("JournalDialogDelete");
                    Session.Remove("ListJournalTemp");
                    Session.Remove("JournalDialogNoLoop");
                    Session.Remove("JournalDialog2");
                    Session.Remove("JournalHeader2");
                    Session.Remove("JournalDialogDelete");

                    //this.Page.Session.Clear();
                }


                Session["PilihMenuDong"] = strHrefOnClick;
                int intLevel = 0;
                RulesFacade rulesFacade = new RulesFacade();

                if (strHrefOnClick == "Home.aspx")
                //if (strHrefOnClick == "Default.aspx")
                {
                    hrefOnClick = "~/Home.aspx";
                    //hrefOnClick = "~/Default.aspx";

                    Session["MenuLevel1"] = null;
                    Session["MenuLevel2"] = null;
                    Session["MenuLevel3"] = null;

                    //utk SayInfoPath
                    Session["RuleName1"] = null;
                    Session["RuleName2"] = null;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_01_")
                {
                    Session["MenuLevel1"] = strHrefOnClick.Substring(6, strHrefOnClick.Length - 7);
                    Session["MenuLevel2"] = null;
                    Session["MenuLevel3"] = null;
                    intLevel = 1;

                    //cari level 1
                    string idNameLevel1 = strHrefOnClick.Substring(0, strHrefOnClick.Length - 1);

                    Rules RuleName1 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 7));

                    string strSort = RuleName1.Sort;
                    string kata = strSort.Replace(" ", "") + 1;

                    hrefOnClick = RuleName1.Href;

                    rulesFacade = new RulesFacade();

                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 1
                    Rules idNameLevel0 = rulesFacade.RetrieveBySortAndLevel(kata, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    //menu active level 1
                    Session["MenuLevel1"] = "li_01_" + idNameLevel0.IDname;
                    Session["MenuLevel2"] = idNameLevel1;
                    Session["MenuLevel3"] = null;

                    //utk SayInfoPath
                    Session["RuleName1"] = idNameLevel0.RuleName;
                    Session["RuleName2"] = RuleName1.RuleName;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_02_")
                {
                    intLevel = 2;

                    string idNameLevel2 = strHrefOnClick.Substring(0, strHrefOnClick.Length - 1);

                    Rules RuleName2 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 7));
                    //string strSort = RuleName2.Sort.Substring(0, 1);
                    string strSort = RuleName2.Sort;
                    hrefOnClick = RuleName2.Href;
                    rulesFacade = new RulesFacade();

                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 1
                    Rules idNameLevel1 = rulesFacade.RetrieveBySortAndLevel(strSort, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    //menu active level 1
                    Session["MenuLevel1"] = "li_01_" + idNameLevel1.IDname;
                    Session["MenuLevel2"] = idNameLevel2;
                    Session["MenuLevel3"] = null;

                    //utk SayInfoPath
                    Session["RuleName1"] = idNameLevel1.RuleName;
                    Session["RuleName2"] = RuleName2.RuleName;
                    Session["RuleName3"] = null;
                }
                else if (strHrefOnClick.Substring(0, 6) == "li_03_")
                {
                    intLevel = 3;
                    string idNameLevel3 = strHrefOnClick.Substring(0, strHrefOnClick.Length - 1);

                    Rules RuleName3 = rulesFacade.RetrieveByIDname(strHrefOnClick.Substring(6, strHrefOnClick.Length - 7));
                    //string strSort2 = RuleName3.Sort.Substring(0, 3);
                    string strSort2 = RuleName3.Sort;
                    hrefOnClick = RuleName3.Href;
                    rulesFacade = new RulesFacade();

                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 2
                    Rules idNameLevel2 = rulesFacade.RetrieveBySortAndLevel(strSort2, 3);
                    string strSort1 = idNameLevel2.Sort;
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    rulesFacade = new RulesFacade();
                    //cari headnya level1: dari IDname cari yg sama sort tapi level = 1
                    Rules idNameLevel1 = rulesFacade.RetrieveBySortAndLevel(strSort1, 1);
                    if (rulesFacade.Error != string.Empty)
                    {
                        //messagebox gak ketemu
                        return;
                    }

                    //menu active level 1
                    Session["MenuLevel1"] = "li_01_" + idNameLevel1.IDname;
                    Session["MenuLevel2"] = "li_02_" + idNameLevel2.IDname;
                    Session["MenuLevel3"] = idNameLevel3;

                    //utk SayInfoPath
                    Session["RuleName1"] = idNameLevel1.RuleName;
                    Session["RuleName2"] = idNameLevel2.RuleName;
                    Session["RuleName3"] = RuleName3.RuleName;
                }
                else
                {
                    //info messagebox gak ada on Rules / error

                }

                if (hrefOnClick == "~/Modul/Sales/InputOrderPenjualan.aspx")
                    hrefOnClick = "~/Modul/Sales/WebForm2.aspx";

                //Response.Redirect("Contact.aspx");
                Response.Redirect(hrefOnClick.Trim());
            }
            else
            {
            }
        }



    }

}