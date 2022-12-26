using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormBAApproval : System.Web.UI.Page
    {
        public int ApprovalLevel { get; set; }
        /// <summary>
        /// The timbangan sup
        /// </summary>
        public decimal TimbanganSup = 0;
        /// <summary>
        /// The timbangan bpas
        /// </summary>
        public decimal TimbanganBpas = 0;
        /// <summary>
        /// The judul
        /// </summary>
        public string Judul = "";
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {
                string token = (Request.QueryString["token"] != null) ? new EncryptPasswordFacade().DecryptString(Request.QueryString["token"].ToString()) : string.Empty;
                if (token != string.Empty)
                {
                    NameValueCollection url = HttpUtility.ParseQueryString(token);
                    UsersFacade usr = new UsersFacade();
                    Users users = usr.RetrieveByUserNameAndPassword(url[1].ToString(), url[2].ToString());
                    Session["Users"] = users;
                }
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                string[] AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
                int Urutan = Array.IndexOf(AppUser, user.ID.ToString());
                Urutan = (Urutan < 0) ? 0 : Urutan;
                LoadBulan();
                LoadTahun();
                switch (Urutan)
                {
                    case 3:
                        //case 4:
                        this.ApprovalLevel = 3;
                        break;
                    default:
                        this.ApprovalLevel = Urutan;
                        break;
                }
                switch (Request.QueryString["tp"])
                {
                    case "0":
                        appLevele.Value = ApprovalLevel.ToString();
                        btnApprove.Visible = false;
                        btnUnApprove.Visible = false;
                        btnExport.Visible = false;
                        btnPreview.Visible = true;
                        btnBack.Visible = true;
                        this.StateView = 0;
                        criteria.Visible = false;
                        Judul = "LIST";
                        ddlBulan.Visible = true;
                        ddlTahun.Visible = true;
                        lst.Attributes.Add("style", "height:450px");
                        LoadListBA(ApprovalLevel);
                        break;
                    case "2":
                        appLevele.Value = ApprovalLevel.ToString();
                        btnApprove.Visible = false;
                        btnUnApprove.Visible = false;
                        btnExport.Visible = true;
                        btnPreview.Visible = true;
                        btnBack.Visible = false;
                        this.StateView = 2;
                        criteria.Visible = true;
                        Judul = "LIST";
                        ddlBulan.Visible = true;
                        ddlTahun.Visible = true;
                        lst.Attributes.Add("style", "height:450px");
                        LoadListBA(ApprovalLevel);
                        break;
                    default:
                        this.StateView = 1;
                        appLevele.Value = ApprovalLevel.ToString();
                        btnApprove.Visible = true;
                        btnBack.Visible = false;
                        btnUnApprove.Visible = true;
                        btnApprove.Enabled = false;
                        btnUnApprove.Enabled = false;
                        btnExport.Visible = false;
                        btnPreview.Visible = false;
                        criteria.Visible = false;
                        Judul = "APPROVAL";
                        ddlBulan.Visible = false;
                        ddlTahun.Visible = false;
                        lst.Attributes.Add("style", "height:500px");
                        LoadListBA(ApprovalLevel);
                        break;
                }
                LogActivity("Approve Berita Acara apv: " + ApprovalLevel.ToString(), true);

            }
        }

        protected void LogActivity(string activity, bool recordPageUrl)
        {
            if (Request.IsAuthenticated)
            {
                // Get information about the currently logged on user

                Users user = (Users)Session["Users"];
                if (user != null)
                {
                    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(BusinessFacade.Global.ConnectionString());
                    try
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@UserID", user.ID));
                        param.Add(new SqlParameter("@Activity", activity));
                        param.Add(new SqlParameter("@PageUrl", Request.RawUrl));
                        param.Add(new SqlParameter("@IPAddress", Global.GetIPAddress()));
                        param.Add(new SqlParameter("@Browser", Request.UserAgent));
                        int intResult = da.ProcessData(param, "sp_LogUserActivity");
                    }
                    catch
                    {
                    }
                    finally
                    {
                        da.CloseConnection();
                    }
                }
            }
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Modul/Purchasing/FormBeritaAcara.aspx");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.StateView = 2;
            criteria.Visible = true;
            Judul = "LIST";
            LoadListBA(int.Parse(appLevele.Value));
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstBA.Items)
            {
                ((Image)rpt.FindControl("attach")).Visible = false;
                ((Image)rpt.FindControl("view")).Visible = false;
                ((Image)rpt.FindControl("att")).Visible = false;
                ((Image)rpt.FindControl("info")).Visible = false;
                ((Image)rpt.FindControl("detail")).Visible = false;
                ((Image)rpt.FindControl("adjust")).Visible = false;
                Repeater att = (Repeater)rpt.FindControl("attachm");
                foreach (RepeaterItem rp in att.Items)
                {
                    ((Image)rp.FindControl("lihat")).Visible = false;
                    ((Image)rp.FindControl("hapus")).Visible = false;
                }


            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListBeritaAcara.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP PES</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            //Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// Gets or sets the state view.
        /// </summary>
        /// <value>The state view.</value>
        private int StateView { get; set; }
        /// <summary>
        /// Loads the list ba.
        /// </summary>
        /// <param name="appLevel">The application level.</param>
        private void LoadListBA(int appLevel)
        {
            string[] ViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAll", "BeritaAcara").Split(',');
            string StartDocument = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartDocument", "BeritaAcara");
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.TableName = "BeritaAcara";
            zw.Where = "where RowStatus>-1";
            zw.Where += " and Left(COnvert(Char,BADate,112),6)>=" + StartDocument;
            if (this.StateView == 1)
            {
                zw.Criteria = (appLevel >= 5) ? " and Approval=" + (appLevel - 0) : " and Approval= " + appLevel;
                zw.Criteria += (appLevel >= 2) ? "" : " and fromDept=" + ((Users)Session["Users"]).DeptID.ToString();
            }
            else
            {
                zw.Criteria = (ViewAll.Contains(((Users)Session["Users"]).DeptID.ToString()) || appLevel > 1) ? "" : " and Approval <=" + appLevel;
                zw.Criteria += (ViewAll.Contains(((Users)Session["Users"]).DeptID.ToString()) || appLevel > 1) ? "" : " and fromDept=" + ((Users)Session["Users"]).DeptID.ToString();
            }
            string ViewRecord = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewRecord", "BeritaAcara");
            ViewRecord = (int.Parse(ViewRecord) == 0) ? "" : "top " + ViewRecord;
            zw.Field = ViewRecord + " *,(select dbo.ItemNameInv(ItemID,1))ItemName ";
            zw.Field += ",(select dbo.SatuanInv(ItemID,1))Unit ";
            zw.Field += ",(Select ISNULL(SUM(JmlBal),0)JmlBal from BeritaAcaraDetail where BAID=BeritaAcara.ID)JmlBal";

            //zw.Field += ",(Select top 1 DepoKertas.DepoName from DepoKertas where DepoKertas.ID=BeritaAcara.DepoKertasID)DepoName";
            zw.Field += ",case when DepoKertasID=(select ID from DepoKertas where DepoName='Lain-Lain') " +
                        "then (select A1.SupplierName from BeritaAcaraDetail A1 where A1.BAID=BeritaAcara.ID and A1.RowStatus>-1) " +
                        "else (Select top 1 DepoKertas.DepoName from DepoKertas where DepoKertas.ID=BeritaAcara.DepoKertasID) end DepoName ";

            zw.Criteria += (this.StateView == 0) ? " and Approval < 6 Order by ID Desc" : "";
            zw.Criteria += (this.StateView == 1) ? " Order by ID desc" : "";
            zw.Criteria += (this.StateView == 2) ? " and Month(BADate)=" + int.Parse(ddlBulan.SelectedValue.ToString()) : "";
            zw.Criteria += (this.StateView == 2) ? " and Year(BADate)=" + ddlTahun.SelectedValue.ToString() : "";
            zw.Criteria += (this.StateView == 2) ? " Order by ID desc" : "";
            zw.QueryType = Operation.SELECT;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (CountAttachDoc(int.Parse(sdr["ID"].ToString())) > 0 && appLevel > 0)
                    {
                        #region jika level >0
                        arrData.Add(new BeritaAcara
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BANum = sdr["BANum"].ToString(),
                            ItemName = sdr["ItemName"].ToString(),
                            Unit = sdr["Unit"].ToString(),
                            JmlBalBPAS = Convert.ToDecimal(sdr["JmlBalBPAS"].ToString()),
                            JmlBal = Convert.ToDecimal(sdr["JmlBal"].ToString()),
                            TotalSup = Convert.ToDecimal(sdr["TotalSup"].ToString()),
                            TotalBPAS = Convert.ToDecimal(sdr["Netto"].ToString()),
                            Selisih = Convert.ToDecimal(sdr["Selisih"].ToString()),
                            ProsSelisih = Convert.ToDecimal(sdr["ProsSelisih"].ToString()),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString()),
                            DepoKertasName = sdr["DepoName"].ToString()
                        });
                        #endregion
                    }
                    else
                    {
                        #region jika level=0 maka munculkan semua
                        arrData.Add(new BeritaAcara
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            BANum = sdr["BANum"].ToString(),
                            ItemName = sdr["ItemName"].ToString(),
                            Unit = sdr["Unit"].ToString(),
                            JmlBalBPAS = Convert.ToDecimal(sdr["JmlBalBPAS"].ToString()),
                            JmlBal = Convert.ToDecimal(sdr["JmlBal"].ToString()),
                            TotalSup = Convert.ToDecimal(sdr["TotalSup"].ToString()),
                            TotalBPAS = Convert.ToDecimal(sdr["Netto"].ToString()),
                            Selisih = Convert.ToDecimal(sdr["Selisih"].ToString()),
                            ProsSelisih = Convert.ToDecimal(sdr["ProsSelisih"].ToString()),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString()),
                            DepoKertasName = sdr["DepoName"].ToString()
                        });
                        #endregion
                    }
                }
            }

            lstBA.DataSource = arrData;
            lstBA.DataBind();

        }
        /// <summary>
        /// Handles the DataBound event of the lstBA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            BeritaAcara ba = (BeritaAcara)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("slsBal");
            Label adj = (Label)e.Item.FindControl("adj");
            Image info = (Image)e.Item.FindControl("info");
            Image detail = (Image)e.Item.FindControl("detail");
            Image print = (Image)e.Item.FindControl("adjust");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            Repeater lstA = (Repeater)e.Item.FindControl("infoApp");
            decimal selisih = ba.JmlBal - ba.JmlBalBPAS;
            string[] AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
            UsersFacade usf = new UsersFacade();
            Users users = (AppUser.Count() > ba.Approval) ? usf.RetrieveById(int.Parse(AppUser[ba.Approval])) : new Users();
            int jmlAttachment = 0;
            lbl.Text = selisih.ToString("###,##0.00");
            adj.Text = (ba.Selisih < 0) ? "AdjOut" : "AdjIn";
            /** Adjust tools */

            string AutoAdjust = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AutoAdjustProses", "BeritaAcara");
            print.Visible = (ba.Approval >= 2 && ba.RowStatus == 0) ? true : false;
            print.Visible = (AutoAdjust == "1" && isAdjust(ba.BANum)) ? false : print.Visible;
            print.Visible = (isAdjust(ba.BANum)) ? false : print.Visible;
            chk.Visible = (ApprovalLevel > -1) ? true : false;
            chk.Visible = (ba.Approval == ApprovalLevel) ? chk.Visible : false;
            info.ToolTip = (ba.Approval >= AppUser.Count()) ? "Approval Completed " : "Status Approval : " + ba.Approval.ToString() + "\nNext Approver : " + users.UserName.ToString().ToUpper() + "    ";
            ApprovalPreview(ba.ID, lstA);
            /** attach confirmasi*/
            Image attach = (Image)e.Item.FindControl("attach");
            Image view = (Image)e.Item.FindControl("view");
            string MaxSelisih = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaxSelisih", "BeritaAcara");
            string[] Konfirmasi = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UploadDoc", "BeritaAcara").Split(',');
            string LastApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastApproval", "BeritaAcara");
            string LockApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Konfirmasi", "BeritaAcara");
            attach.Visible = (int.Parse(MaxSelisih) < ba.ProsSelisih && CheckAttach(ba.ID) == false) ? false : false;
            view.Visible = (int.Parse(MaxSelisih) < ba.ProsSelisih && CheckAttach(ba.ID) == true) ? false : false;
            int PosKonfirmasi = Array.IndexOf(Konfirmasi, (((Users)Session["Users"]).DeptID.ToString()));
            attach.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() + "')");
            //view.Attributes.Add("onclick", "PreviewPDF('" + ba.ID.ToString() + "')");
            attach.Visible = ((int.Parse(MaxSelisih) > ba.ProsSelisih) && PosKonfirmasi != -1 && CheckAttach(ba.ID) == false) ? false : attach.Visible;
            chk.Visible = (attach.Visible == true && (int.Parse(MaxSelisih) > ba.ProsSelisih) && ba.Approval >= int.Parse(LockApproval)) ? false : chk.Visible;
            //chk.Visible = (/*this.isAdjust(this.GetBANum(ba.ID)) == true &&*/ LastApproval == ((Users)Session["Users"]).ID.ToString()) ? false : chk.Visible;
            chk.Visible = (Judul == "LIST") ? false : chk.Visible;
            Image att = (Image)e.Item.FindControl("att");
            string[] UploadDoc = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UploadDoc", "BeritaAcara").Split(',');
            string[] jmlDoc = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Document", "BeritaAcara").Split(',');
            jmlAttachment = ((int.Parse(MaxSelisih) > ba.ProsSelisih) && ba.DepoID < 5) ? jmlDoc.Count() : jmlDoc.Count() - 1;
            chk.Visible = (ba.Approval >= 2 && ba.Approval == (ApprovalLevel - 1)) ? true : chk.Visible;
            att.Visible = ((ba.Approval < 5) && (UploadDoc.Contains(((Users)Session["Users"]).DeptID.ToString()) || ba.FromDept == ((Users)Session["Users"]).DeptID)) ? true : false;
            att.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() + "&tp=1')");
            Repeater rps = (Repeater)e.Item.FindControl("attachm");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            if ((int.Parse(MaxSelisih) > ba.ProsSelisih))
            {
                ps.Attributes.Add("style", "color:red");
                ps.Attributes.Add("title", "Perlu document konfirmasi");
            }
            chk.Visible = (CountAttachDoc(ba.ID) != jmlAttachment && /*(decimal.Parse(MaxSelisih) <= ba.ProsSelisih) &&*/ (ba.Approval >= int.Parse(LockApproval))) ? false : chk.Visible;
            LoadListAttachment(ba.ID.ToString(), rps);
        }
        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            string[] viewApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApprovalStatus", "BeritaAcara").Split(',');
            int idx = Array.IndexOf(viewApv, users.DeptID.ToString());
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
                ((Label)e.Item.FindControl("lblNextApp")).Text = (idx >= 0 && (this.StateView == 2)) ? info.ToolTip.Replace("\n", " - ") + " " : "";
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                AttachmentBA att = (AttachmentBA)e.Item.DataItem;
                pre.Attributes.Add("onclick", "PreviewPDF('" + pre.CssClass.ToString() + "')");
                //if (att.Attachment != null)
                //{
                //    pre.Attributes.Add("onclick", "PreviewPDF('" + pre.CssClass.ToString() + "')");
                //}
                //else
                //{
                //    pre.Attributes.Add("onclick", "PrevPDF('" + att.FileName + "')");
                //}
                hps.Visible = (att.FromDept == ((Users)Session["Users"]).DeptID && att.Approval < 5) ? true : false;
            }
        }
        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;

            Image pre = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
            switch (e.CommandName)
            {
                case "pre":
                    break;
                case "hps":
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update BeritaAcaraAttachment set RowStatus=-1 where ID=" + pre.CssClass;
                    SqlDataReader sdr = zl.Retrieve();
                    LoadListAttachment(pre.AlternateText.ToString(), rpt);
                    break;
            }
        }
        /// <summary>
        /// The tot aa
        /// </summary>
        private decimal totAa = 0; private decimal totCa = 0;
        /// <summary>
        /// Handles the DataBound event of the lstDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void lstDetail_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Receipt rcp = (Receipt)e.Item.DataItem;
            TimbanganSup += rcp.Netto;
            totAa += rcp.Gross;
            totCa += rcp.JmlBal;

        }
        /// <summary>
        /// Handles the Command event of the lstBA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "appInfo":
                    Repeater rpt = (Repeater)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("infoApp");
                    rpt.Visible = (rpt.Visible == true) ? false : true;
                    break;
                case "detail":
                    HtmlTableRow tr = (HtmlTableRow)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("dtl");
                    ImageButton img = (ImageButton)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("detail");
                    Repeater lst = (Repeater)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("lstDetail");
                    LoadDetailBA(img.CssClass.ToString(), lst);
                    tr.Visible = (tr.Visible == false) ? true : false;
                    break;
                case "proses":
                    ProcessAdjust(e.CommandArgument.ToString());
                    break;
                case "viewpdf":
                    break;
                case "upload":
                    if (CheckAttach(int.Parse(e.CommandArgument.ToString())) == true) { this.LoadListBA(int.Parse(appLevele.Value)); }
                    break;
                case "attach":
                    Repeater rpts = (Repeater)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("attachm");
                    ImageButton imgs = (ImageButton)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("att");
                    //rpts.Visible = (rpts.Visible == true) ? false : true;
                    LoadListAttachment(imgs.CssClass.ToString(), rpts);
                    break;
            }
        }
        /// <summary>
        /// Handles the CheckedChange event of the chk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            btnApprove.Enabled = (chk.Checked == true) ? true : false;
            btnUnApprove.Enabled = (chk.Checked == true) ? true : false;
        }
        /// <summary>
        /// Handles the Click event of the btnApprove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// 
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string LastApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastApproval", "BeritaAcara");
            string[] AprovalList = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
            int Urutan = Array.IndexOf(AprovalList, ((Users)Session["Users"]).ID.ToString());
            switch (Urutan)
            {
                case 3:
                    //case 4:
                    this.ApprovalLevel = 3;
                    break;
                default:
                    this.ApprovalLevel = Urutan;
                    break;
            }
            string BaNumber = string.Empty;
            string AppList = string.Empty;
            int nextApp = 0;
            int CheckClosing = 0;
            for (int i = 0; i < lstBA.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstBA.Items[i].FindControl("chk");
                if (chk.Checked == true)
                {
                    ZetroLib zlb = new ZetroLib();
                    zlb.TableName = "BeritaAcara";
                    zlb.hlp = new BeritaAcara();
                    zlb.Option = "Update";
                    zlb.Criteria = "ID,Approval";
                    zlb.StoreProcedurName = "spBeritaAcara_Approval";
                    string sp = zlb.CreateProcedure();

                    if (sp == string.Empty)
                    {
                        BeritaAcara ba = new BeritaAcara();
                        ba.ID = int.Parse(chk.CssClass.ToString());
                        ba.Approval = (ByPassLevel() > 0 && ByPassUser() == ((Users)Session["Users"]).ID) ? ByPassLevel() : int.Parse(appLevele.Value) + 1;
                        zlb.hlp = ba;
                        int rst = zlb.ProcessData();
                        chk.Enabled = (rst > 0) ? false : true;
                        btnApprove.Enabled = (rst > 0) ? false : true;
                        /** Log proses approval*/
                        ZetroLib zl = new ZetroLib();
                        zl.TableName = "BeritaAcaraApproval";
                        zl.hlp = new ApprovalBA();
                        zl.Option = "Insert";
                        zl.Criteria = "UserID,UserName,BAID,Approval,IPAddress,CreatedTime";
                        zl.StoreProcedurName = "spBeritaAcaraApproval_Insert";
                        string zet = zl.CreateProcedure();
                        Users usr = zl.UserAccount(((Users)Session["Users"]).ID);

                        if (zet == string.Empty)
                        {
                            if (LastApproval == ((Users)Session["Users"]).ID.ToString())
                            {
                                CheckClosing = UpdateAdjust(this.GetBANum(int.Parse(chk.CssClass.ToString())));
                                if (CheckClosing == 1) { return; }
                                this.ProcessPointLapak(int.Parse(chk.CssClass.ToString()));
                            }
                            ApprovalBA aba = new ApprovalBA();
                            aba.UserName = usr.UserName.ToString();
                            aba.UserID = ((Users)Session["Users"]).ID;
                            aba.IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                            aba.Approval = int.Parse(appLevele.Value) + 1;
                            aba.BAID = int.Parse(chk.CssClass.ToString());
                            zl.hlp = aba;
                            int rest = zl.ProcessData();
                            if (rest > 0)
                            {

                                if ((int.Parse(appLevele.Value) + 1) == 2)
                                {
                                    this.ProcessAdjust((chk.CssClass.ToString()));
                                }
                                BaNumber += ListBA(chk.CssClass.ToString());
                                AppList += ListBA(chk.CssClass.ToString());
                                AppList += ListApprover(chk.CssClass.ToString());
                                nextApp = (ByPassLevel() > 0 && ByPassUser() == ((Users)Session["Users"]).ID) ? ByPassLevel() : int.Parse(appLevele.Value) + 1;

                            }
                        }
                    }
                }
            }
            if (this.ApprovalLevel < AprovalList.Count())
            {
                if (LastApproval == ((Users)Session["Users"]).ID.ToString())
                {
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    KirimEmail(int.Parse(AprovalList[nextApp]), BaNumber, AppList);
                }
            }
            Response.Redirect(Request.RawUrl);
        }
        /// <summary>
        /// Approvals the preview.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        /// <param name="lstApp">The LST application.</param>
        private void ApprovalPreview(int BAID, Repeater lstApp)
        {
            ArrayList arrApp = new ArrayList();
            string AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara");
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "Select Distinct BAID as ID,UserID,UserName,IPAddress,Approval," +
                             "(Select Top 1 CreatedTime from BeritaAcaraApproval b where b.BAID=BeritaAcaraApproval.BAID and " +
                             " b.UserID=BeritaAcaraApproval.UserID)CreatedTime from BeritaAcaraApproval where BAID=" + BAID +
                             " order by BeritaAcaraApproval.Approval";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrApp.Add(new ApprovalBA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        UserName = sdr["UserName"].ToString(),
                        IPAddress = sdr["IPAddress"].ToString(),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        AppStatus = (Convert.ToInt32(sdr["Approval"].ToString()) > 0) ? "Approved" : "UnApproved",
                        CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                    });
                }
            }
            lstApp.DataSource = arrApp;
            lstApp.DataBind();
        }
        private ArrayList ApprovalPreview(int BAID)
        {
            ArrayList arrApp = new ArrayList();
            string AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara");
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "Select Distinct BAID as ID,UserID,UserName,IPAddress,Approval," +
                                     "(Select Top 1 CreatedTime from BeritaAcaraApproval b where b.BAID=BeritaAcaraApproval.BAID and " +
                                     " b.UserID=BeritaAcaraApproval.UserID)CreatedTime from BeritaAcaraApproval where BAID=" + BAID +
                                     " order by BeritaAcaraApproval.Approval";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrApp.Add(new ApprovalBA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        UserName = sdr["UserName"].ToString(),
                        IPAddress = sdr["IPAddress"].ToString(),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        AppStatus = (Convert.ToInt32(sdr["Approval"].ToString()) > 0) ? "Approved" : "UnApproved",
                        CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                    });
                }
            }
            return arrApp;
        }
        /// <summary>
        /// Bies the pass level.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int ByPassLevel()
        {
            int result = 0;
            string[] ByPass = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ByPass", "BeritaAcara").Split(',');
            if (ByPass.Count() > 1)
            {
                result = int.Parse(ByPass[1]);
            }
            return result;
        }
        /// <summary>
        /// Bies the pass user.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int ByPassUser()
        {
            int result = 0;
            string[] ByPass = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ByPass", "BeritaAcara").Split(',');
            if (ByPass.Count() > 1)
            {
                result = int.Parse(ByPass[0]);
            }
            return result;
        }
        /// <summary>
        /// Loads the detail ba.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        /// <param name="lst">The LST.</param>
        private void LoadDetailBA(string BAID, Repeater lst)
        {
            ArrayList arrDetailBA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select *,(select ReceiptDate from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptDate ";
            zw.CustomQuery += ",(select ReceiptNo from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptNo1 ";
            zw.CustomQuery += "from BeritaAcaraDetail where RowStatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrDetailBA.Add(new Receipt
                    {
                        ReceiptNo = sdr["ReceiptNo1"].ToString(),
                        ReceiptDate = (sdr["ReceiptDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["ReceiptDate"].ToString()),
                        SupplierName = sdr["SupplierName"].ToString(),
                        SupplierID = int.Parse(sdr["SupplierID"].ToString()),
                        Gross = decimal.Parse(sdr["Gross"].ToString()),
                        Netto = decimal.Parse(sdr["Netto"].ToString()),
                        Quantity = decimal.Parse(sdr["Quantity"].ToString()),
                        KadarAir = decimal.Parse(sdr["KadarAir"].ToString()),
                        JmlBal = decimal.Parse(sdr["jmlBal"].ToString()),
                        NoSJ = sdr["NoSJ"].ToString()
                    });
                }
            }
            lst.DataSource = arrDetailBA;
            lst.DataBind();
        }
        private void LoadListAttachment(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.SELECT;
            zv.TableName = "BeritaAcaraAttachment";
            zv.Field = "*,(Select Approval from BeritaAcara where ID=" + BAID + ")Approval";
            zv.Field += ",(Select FromDept from BeritaAcara where ID=" + BAID + ")FromDept ";
            zv.Where = " where rowstatus>-1";
            zv.Where += " and BAID=" + BAID;
            zv.Criteria = " Order by DocName";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new AttachmentBA
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            BAID = Convert.ToInt32(sdr["BAID"].ToString()),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            FromDept = Convert.ToInt32(sdr["FromDept"].ToString()),
                            CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();

        }
        /// <summary>
        /// Processes the adjust.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        private void ProcessAdjust(string BAID)
        {
            /**
             * Proces Input Otomatis ke table Adjust
             * Param *
             * Sesuai dengan spInsertAdjust
             */
            ZetroView zlb = new ZetroView();
            zlb.StoreProcedurName = "spInsertAdjust";
            zlb.QueryType = Operation.CUSTOM;
            zlb.CustomQuery = "SELECT ba.ID,ba.BANum,ba.BADate,ba.Selisih,ba.ItemID,rc.ReceiptDate,bad.NoSJ," +
                            "CASE WHEN ba.Selisih>0 THEN 'Tambah' ELSE 'Kurang' END AdjustType " +
                            ",(SELECT UomID FROM Inventory WHERE ID=ba.ItemID)UomID " +
                            ",(SELECT GroupID FROM Inventory WHERE ID=ba.ItemID)GroupID " +
                            "FROM BeritaAcara ba " +
                            "LEFT JOIN BeritaAcaraDetail bad ON bad.BAID=ba.ID " +
                            "LEFT JOIN Receipt rc ON rc.ID=bad.ReceiptNo " +
                            "WHERE BAID=" + BAID + "AND bad.RowStatus>-1 " +
                            " GROUP BY ba.ID,ba.BANum,ba.BADate,ba.Selisih,ba.ItemID,rc.ReceiptDate,bad.NoSJ";
            SqlDataReader sdr = zlb.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    DateTime dt = (sdr["ReceiptDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ReceiptDate"].ToString());
                    Adjust adj = new Adjust();
                    adj.AdjustNo = (this.LastAdjustID() + 1).ToString().PadLeft(5, '0') + "/ADJ/" + Global.BulanRomawi(dt.Month) + "/" + dt.Year.ToString();
                    adj.AdjustDate = dt;// Convert.ToDateTime(sdr["ReceiptDate"].ToString());
                    adj.AdjustType = sdr["AdjustType"].ToString();
                    adj.Keterangan1 = sdr["ID"].ToString().PadLeft(5, '0');
                    adj.Status = 0;
                    adj.ItemTypeID = 1;
                    adj.NonStok = 0;

                    adj.CreatedBy = ((Users)Session["Users"]).UserName;
                    zlb.Criteria = "AdjustNo,AdjustDate,AdjustType,Keterangan1,Status,ItemTypeID,CreatedBy,NonStok";
                    zlb.hlp = adj;
                    int rst = zlb.ProcessData();
                    if (rst > 0)
                    {
                        /**
                         * Proses Input Otomatis ke Table AdjustDetail
                         * Param *
                         * Sesuai dengn spInsertAdjustDetail
                         * added on 10-11-2015
                         */
                        ZetroLib zl = new ZetroLib();
                        zl.StoreProcedurName = "spInsertAdjustDetail";
                        AdjustDetail adjd = new AdjustDetail();
                        adjd.AdjustID = rst;
                        adjd.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                        adjd.Quantity = Math.Abs(Convert.ToDecimal(sdr["Selisih"].ToString()));
                        adjd.UomID = Convert.ToInt32(sdr["UomID"].ToString());
                        adjd.RowStatus = 0;
                        adjd.GroupID = Convert.ToInt32(sdr["GroupID"].ToString());
                        adjd.ItemTypeID = 1;
                        adjd.AdjustType = sdr["AdjustType"].ToString();
                        adjd.Keterangan = sdr["BANum"].ToString();
                        zl.Criteria = "AdjustID,ItemID,Quantity,UomID,RowStatus,GroupID,ItemTypeID,AdjustType,Keterangan";
                        zl.hlp = adjd;
                        int hsl = zl.ProcessData();
                        /** Update RowStatus beritaacara menjadi 1
                         * agar tombol proses adjust tidak muncul lagi
                         */
                        if (hsl > 0)
                        {
                            ZetroLib zb = new ZetroLib();
                            zb.TableName = "BeritaAcara";
                            zb.hlp = new BeritaAcara();
                            zb.Option = "Update";
                            zb.Criteria = "ID,RowStatus,Keterangan";
                            zb.StoreProcedurName = "spBeritaAcara_RowStatus";
                            string sp = zb.CreateProcedure();
                            if (sp == string.Empty)
                            {
                                BeritaAcara ba = new BeritaAcara();
                                ba.ID = int.Parse(BAID);
                                ba.RowStatus = 1;
                                ba.Keterangan = hsl.ToString().PadLeft(7, '0');
                                zb.hlp = ba;
                                int r = zb.ProcessData();
                                if (r > 0)
                                {
                                    LoadListBA(int.Parse(appLevele.Value));

                                }
                            }
                        }
                    }
                }
                // btnPreview_Click(null, null);
            }

        }
        /// <summary>
        /// Lasts the adjust identifier.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int LastAdjustID()
        {
            int result = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select Top 1 ID from Adjust Order By ID Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }
        /// <summary>
        /// Checks the attach.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckAttach(int BAID)
        {
            bool result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select ID from BeritaAcaraAttachment where RowStatus>-1 and BAID=" + BAID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                result = true;
            }
            return result;
        }
        private int CountAttachDoc(int BAID)
        {
            int result = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select COUNT(ID) as jml from BeritaAcaraAttachment where RowStatus>-1 and BAID=" + BAID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["jml"].ToString());
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        private void UploadFile(int BAID)
        {
            if (Upload1.HasFile)
            {
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    ZetroLib zl = new ZetroLib();
                    zl.Option = "Insert";
                    zl.Criteria = "BAID,FileName,Attachment,RowStatus,Createdby,CreatedTime";
                    zl.hlp = new AttachmentBA();
                    zl.StoreProcedurName = "spBeritaAcaraAtt_Insert";
                    string rst = zl.CreateProcedure();
                    if (rst == string.Empty)
                    {
                        AttachmentBA ba = new AttachmentBA();
                        ba.BAID = BAID;
                        ba.FileName = filename.ToString();
                        ba.Attachment = bytes;
                        ba.RowStatus = 0;
                        ba.CreatedBy = ((Users)Session["Users"]).UserName;
                        zl.hlp = ba;
                        int rs = zl.ProcessData();
                        if (rs > 0)
                        {
                            LoadListBA(int.Parse(appLevele.Value));
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
                }
            }
        }
        /**
         * Proses Kirim data ke table AgenLapak.Agen_dtKirimKeBPAS
         * dilakukan setelah / bersamaan dengan Approval yng dilakukan oleh Last Approvel
         * Skenario :
         * Update Approval execute spUpdateAdjustDetailApv
         * Insert via BPAS_API ke table Agen_DtKirimKeBPAS void ProcessPointLapak
         */
        /// <summary>
        /// Updates the adjust.
        /// </summary>
        /// <param name="BANum">The ba number.</param>
        private int UpdateAdjust(string BANum)
        {
            int rst = 0;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.UPDATE;
            zv.Criteria = "AdjustDetailID,ItemID,Quantity,AdjustType,CreatedBy,ItemTypeID";
            zv.TableName = "AdjustDetail";
            zv.StoreProcedurName = "spUpdateAdjustDetailApv";
            foreach (AdjustDetail adj in AdjustData(BANum))
            {
                AdjustDetail ad = new AdjustDetail();
                if (adj.ClosingStatus == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert", "Periode Tanggal BA sudah close tidak bisa Approve", true);
                    return rst;
                }
                ad = adj;
                ad.CreatedBy = ((Users)Session["Users"]).UserName;
                zv.hlp = ad;
                rst = zv.ProcessData();
            }
            return rst;

        }
        /// <summary>
        /// Gets the ba number.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        /// <returns>System.String.</returns>
        private string GetBANum(int BAID)
        {
            string result = string.Empty;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT BANum FROM BeritaAcara where ID=" + BAID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["BANum"].ToString();
                }
            }
            return result;
        }
        /// <summary>
        /// Adjusts the data.
        /// </summary>
        /// <param name="BANum">The ba number.</param>
        /// <returns>ArrayList.</returns>
        private ArrayList AdjustData(string BANum)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            // zv.CustomQuery = "Select *,(select AdjustType from Adjust where ID=AdjustID)AdjustType from AdjustDetail Where Keterangan='" + BANum + "'";
            zv.CustomQuery = "select ad.*,a.AdjustType,a.AdjustDate,isnull(ap.Status,0)Closing " +
                           " FROM AdjustDetail ad " +
                           " LEFT JOIN Adjust a ON ad.AdjustID=a.ID " +
                           " LEFT JOIN AccClosingPeriode ap on ap.Bulan=MONTH(a.AdjustDate) and ap.Tahun=Year(a.AdjustDate) and Modul='Purchn' " +
                           " WHERE ad.Keterangan='" + BANum + "'";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AdjustDetail
                    {
                        AdjustDetailID = Convert.ToInt32(sdr["ID"].ToString()),
                        AdjustID = Convert.ToInt32(sdr["AdjustID"].ToString()),
                        AdjustType = sdr["AdjustType"].ToString(),
                        ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                        Quantity = decimal.Parse(sdr["Quantity"].ToString()),
                        ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"].ToString()),
                        ClosingStatus = int.Parse(sdr["Closing"].ToString())
                    });
                }
            }
            return arrData;
        }
        /// <summary>
        /// Processes the point lapak.
        /// </summary>
        /// <param name="BAID">The baid.</param>
        private void ProcessPointLapak(int BAID)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT ISNULL(rc.ID,0) AS ReceiptID,ReceiptDate,bad.*,ba.ProsSelisih,((ba.ProsSelisih/100)*Quantity*-1)Qty ";
            zw.CustomQuery += "FROM BeritaAcaraDetail bad ";
            zw.CustomQuery += "LEFT JOIN Receipt rc ON rc.ID=bad.ReceiptNo ";
            zw.CustomQuery += "LEFT JOIN BeritaAcara ba ON ba.ID=bad.BAID ";
            zw.CustomQuery += "where bad.RowStatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    this.SupplierID = sdr["SupplierID"].ToString();
                    arrData.Add(new Receipt
                    {
                        KirimID = Convert.ToInt32(sdr["ReceiptNo"].ToString()),
                        NoPOL = this.GetNoPol(Convert.ToInt32(sdr["ReceiptID"].ToString())),
                        ReceiptDate = Convert.ToDateTime(sdr["ReceiptDate"].ToString()),
                        NoSJ = sdr["NoSJ"].ToString(),
                        Quantity = Convert.ToDecimal(sdr["Qty"].ToString()),
                        AgenID = GetIDLapak(),
                        CreatedBy = sdr["CreatedBy"].ToString()
                    });
                }
                int result = this.InsertDataKePointLapak(arrData);
            }
        }
        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>The supplier identifier.</value>
        private string SupplierID { get; set; }
        /// <summary>
        /// Gets the identifier lapak.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int GetIDLapak()
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where SupplierID=" + this.SupplierID;
                Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = bpas.GetDataFromTable("Agen_DtAgenIDtoSupID", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AgenID"].ToString());
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Gets the area kirim.
        /// </summary>
        /// <param name="AgenID">The agen identifier.</param>
        /// <returns>System.Int32.</returns>
        private int GetAreaKirim(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = bpas.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AreaKirim"].ToString());
                }
                return result;
            }
            catch (Exception) { return -1; }

        }
        /// <summary>
        /// Agens the code.
        /// </summary>
        /// <param name="AgenID">The agen identifier.</param>
        /// <returns>System.String.</returns>
        private string AgenCode(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = bpas.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["Code"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// Gets the numerator.
        /// </summary>
        /// <param name="AgenCode">The agen code.</param>
        /// <returns>System.String.</returns>
        private string GetNumerator(string AgenCode)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where Type='SJ Code " + AgenCode + "'";
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = bpas.GetDataFromTable("Agen_DtNumerator", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["Code"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// Inserts the data ke point lapak.
        /// </summary>
        /// <param name="arrLpk">The arr LPK.</param>
        /// <returns>System.Int32.</returns>
        private int InsertDataKePointLapak(ArrayList arrLpk)
        {
            int Resulted = 0;
            string rst = string.Empty;
            string SJNo = string.Empty;
            bpas_api.WebService1 api = new bpas_api.WebService1();
            foreach (Receipt rcd in arrLpk)
            {
                string Tanggal = rcd.ReceiptDate.ToString("yyyyMMdd");
                SJNo = rcd.NoSJ.ToString();
                Resulted = api.InsertKirimKeBPAS(
                      Tanggal,
                      rcd.NoPOL.ToString().ToUpper(),
                      rcd.NoSJ.ToString().ToUpper(),
                      rcd.Quantity,
                      rcd.AgenID.ToString(),
                      rcd.KirimID.ToString(),//ReceitID
                      ((Users)Session["Users"]).UnitKerjaID.ToString(),
                      ((Users)Session["Users"]).UserID.ToString());
                if (Resulted > 0)
                {
                    Receipt rcg = new Receipt();
                    rcg = new ReceiptFacade().RetrieveByNo(rcd.ReceiptNo);
                    rst = api.UpdateKiriman(
                        Resulted.ToString(),
                        rcg.ID.ToString(),
                        rcd.Quantity,
                        ((Users)Session["Users"]).UnitKerjaID.ToString()
                        );
                    //int rsult = new ReceiptFacade().UpdateSJNumber(rcg.ID, SJNo);
                    int rste = api.UpdateNoSJ(rcd.AgenID.ToString());
                }
            }
            return Resulted;
        }
        /// <summary>
        /// Gets the no pol.
        /// </summary>
        /// <param name="ReceiptID">The receipt identifier.</param>
        /// <returns>System.String.</returns>
        private string GetNoPol(int ReceiptID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where ConfirmReceipt=" + ReceiptID;
                ds = bpas.GetDataFromTable("Agen_DtKirimKeBpas", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["PlatNomor"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Determines whether the specified ba number is adjust.
        /// </summary>
        /// <param name="BANum">The ba number.</param>
        /// <returns><c>true</c> if the specified ba number is adjust; otherwise, <c>false</c>.</returns>
        private bool isAdjust(string BANum)
        {
            bool result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT ID FROM AdjustDetail WHERE RowStatus>-1 and Keterangan='" + BANum + "'";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }
        private ArrayList ListBATahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT YEAR(BADate)Tahun From BeritaAcara Order By YEAR(BADate)Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new BeritaAcara
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new BeritaAcara { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }
        /// <summary>
        /// Kirim Email ke Approvel berikutnya
        /// </summary>
        /// <param name="NextApproval"></param>
        /// <param name="NoBA"></param>
        private void KirimEmail(int NextApproval, string NoBA, string Approver)
        {
            string[] AprovalList = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
            UsersFacade usf = new UsersFacade();
            Users users = usf.RetrieveById(NextApproval);
            MailMessage mail = new MailMessage();
            EmailReportFacade msg = new EmailReportFacade();
            string token = new EncryptPasswordFacade().EncryptToString("id=2&UserID=" + users.UserID + "&pwd=" + users.Password.ToString());
            try
            {
                mail.From = new MailAddress("system_support@grcboard.com");
                mail.To.Add(users.UsrMail.ToString());
                // mail.Bcc.Add("noreplay@grcboard.com");
                mail.Subject = "Approval BA Kertas Kantong Semen ";
                mail.Body = "Mohon untuk di Approve BA Kertas Kantong Semen sebagai berikut : \n\r";
                mail.Body += NoBA;
                mail.Body += "Silahkan klik link berikut untuk Approval : \n\r";
                mail.Body += (users.UnitKerjaID == 7) ? "http://krwg.grcboard.com/?link=" ://Modul/Purchasing/FormBAApproval.aspx?token=" + token :
                            "http://ctrp.grcboard.com/?link=";//Modul/Purchasing/FormBAApproval.aspx?token=" + token;
                mail.Body += "\n\r";
                mail.Body += "Approver List :\n\r";
                //mail.Body += Approver;
                mail.Body += "Terimakasih, " + "\n\r";
                mail.Body += "Salam GRCBOARD " + "\n\r";
                mail.Body += "Regard's, " + "\n\r";
                mail.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                SmtpClient smt = new SmtpClient(msg.mailSmtp());
                smt.Host = msg.mailSmtp();
                smt.Port = msg.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("sodikin@grcbaord.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smt.Send(mail);
            }
            catch { }
        }
        private string ListBA(string BAID)
        {
            string result = string.Empty;
            BeritaAcara ba = new BeritaAcara();
            ba = this.GetDetailBA(BAID);
            result += " BA No : " + ba.BANum + " ";
            result += " Depo :" + ba.DepoKertasName + "\n\r";
            return result;
        }
        private string ListApprover(string BAID)
        {
            string result = string.Empty;
            ArrayList arrd = this.ApprovalPreview(int.Parse(BAID));
            foreach (ApprovalBA ap in arrd)
            {
                result += ap.Approval.ToString() + ". " + ap.UserName + " on :" + ap.CreatedTime + " [ " + ap.AppStatus.ToString() + " ]\n\r";
            }
            return result;
        }
        private BeritaAcara GetDetailBA(string BAID)
        {
            BeritaAcara ba = new BeritaAcara();
            string strSQL = "select *,(Select DepoKertas.DepoName From DepoKertas where ID=DepoKertasID)Depo " +
                            "from BeritaAcara where ID=" + BAID;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ba.ID = Convert.ToInt32(sdr["ID"].ToString());
                    ba.BANum = sdr["BaNum"].ToString();
                    ba.DepoKertasName = sdr["Depo"].ToString();
                }
            }
            return ba;
        }
    }
}