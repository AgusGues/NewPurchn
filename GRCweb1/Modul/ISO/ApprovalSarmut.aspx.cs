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
using DataAccessLayer;
using System.Collections.Generic;

namespace GRCweb1.Modul.ISO
{
    public partial class ApprovalSarmut : System.Web.UI.Page
    {
        public int ApprovalLevel { get; set; }
        public string Judul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.chkAll);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {
                string token = (Request.QueryString["token"] != null) ?
                    new EncryptPasswordFacade().DecryptString(Request.QueryString["token"].ToString()) : string.Empty;
                if (token != string.Empty)
                {
                    NameValueCollection url = HttpUtility.ParseQueryString(token);
                    UsersFacade usr = new UsersFacade();
                    Users users = usr.RetrieveByUserNameAndPassword(url[1].ToString(), url[2].ToString());
                    Session["Users"] = users;
                }
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                //string[] AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
                int Urutan = 0;
                Urutan = user.Apv;
                LoadBulan();
                LoadTahun();
                LoadDept();
                switch (Urutan)
                {
                    case 3:
                        //case 4:
                        this.ApprovalLevel = 3;
                        break;
                    case 2:
                        //case 4:
                        this.ApprovalLevel = 2;
                        break;
                    default:
                        this.ApprovalLevel = Urutan;
                        break;
                }
                if (ApprovalLevel == 0)
                {

                    this.StateView = 0;
                    appLevele.Value = ApprovalLevel.ToString();
                    btnApprove.Visible = false;
                    btnRefresh.Visible = true;
                    btnUnApprove.Visible = false;
                    btnUnApprove.Enabled = false;
                    btnExport.Visible = true;
                    //btnPreview.Visible = false;
                    chkAll.Visible = false;
                    criteria.Visible = true;
                    Judul = "INPUT";
                    ddlBulan.Visible = true;
                    ddlTahun.Visible = true;
                    lst.Attributes.Add("style", "height:500px");
                    PanelApv.Visible = false;
                    LoadTypeSarmut();
                }
                if (ApprovalLevel > 0)
                {
                    this.StateView = 0;

                    appLevele.Value = ApprovalLevel.ToString();
                    if (ApprovalLevel == 3)
                    {
                        PanelApv.Visible = false;
                        btnApprove.Visible = false;
                        btnRefresh.Visible = false;
                        btnUnApprove.Visible = false;
                        btnUnApprove.Enabled = false;
                    }
                    else
                    {
                        PanelApv.Visible = true;
                        btnApprove.Visible = true;
                        btnRefresh.Visible = true;
                        btnUnApprove.Visible = true;
                        btnUnApprove.Enabled = true;
                    }

                    btnExport.Visible = false;
                    //btnPreview.Visible = false;
                    chkAll.Visible = true;
                    criteria.Visible = true;
                    Judul = "APPROVAL";
                    ddlBulan.Visible = true;
                    ddlTahun.Visible = true;
                    lst.Attributes.Add("style", "height:500px");
                    LoadTypeSarmut();
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
        protected void txtKeterangan_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        Label lblTercapai = (Label)lstDetail.Items[i].FindControl("lblTercapai");
                        Label lblTarget = (Label)lstDetail.Items[i].FindControl("lblTarget");
                        TextBox txtActual = (TextBox)lstDetail.Items[i].FindControl("txtActual");
                        if (txtActual.ToolTip.Trim().ToUpper() == "MIN")
                        {
                            if (decimal.Parse(lblTarget.Text) > decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tidak tercapai";
                            else
                                lblTercapai.Text = "Tercapai";
                        }
                        else if (txtActual.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
                        {
                            if (decimal.Parse(lblTarget.Text) > decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tidak tercapai";
                            else
                                lblTercapai.Text = "Tercapai";
                        }
                        else
                        {
                            if (decimal.Parse(lblTarget.Text) <= decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tercapai";
                            else
                                lblTercapai.Text = "Tidak Tercapai";
                        }
                        i++;
                    }
                }
            }

        }
        protected void txtKeterangan1_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                int i = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    Label lblTercapai1 = (Label)lstPrs.Items[i].FindControl("lblTercapaiP");
                    Label lblTarget1 = (Label)lstPrs.Items[i].FindControl("lblTargetP");
                    TextBox txtActual1 = (TextBox)lstPrs.Items[i].FindControl("txtActualP");
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    if (txtActual1.ToolTip.Trim().ToUpper() == "MIN")
                    {
                        if (decimal.Parse(lblTarget1.Text) > decimal.Parse(txtActual1.Text))
                            lblTercapai1.Text = "Tidak tercapai";
                        else
                            lblTercapai1.Text = "Tercapai";
                    }
                    else if (txtActual1.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
                    {
                        if (decimal.Parse(lblTarget1.Text) > decimal.Parse(txtActual1.Text))
                            lblTercapai1.Text = "Tidak tercapai";
                        else
                            lblTercapai1.Text = "Tercapai";
                    }
                    else
                    {
                        if (decimal.Parse(lblTarget1.Text) >= decimal.Parse(txtActual1.Text))
                            lblTercapai1.Text = "Tercapai";
                        else
                            lblTercapai1.Text = "Tidak Tercapai";
                    }
                    i++;

                }
            }

        }
        protected void txtKeterangan2_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstDetail2;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        lstDetail2 = ((Repeater)(objDetail.FindControl("lstDetail2")));
                        int j = 0;
                        foreach (RepeaterItem objDetail2 in lstDetail2.Items)
                        {
                            Label lblTercapai2 = (Label)lstDetail2.Items[j].FindControl("lblTercapai2");
                            Label lblTarget2 = (Label)lstDetail2.Items[j].FindControl("lblTarget2");
                            TextBox txtActual2 = (TextBox)lstDetail2.Items[j].FindControl("txtActual2");
                            if (txtActual2.ToolTip.Trim().ToUpper() == "MIN")
                            {
                                if (decimal.Parse(lblTarget2.Text) > decimal.Parse(txtActual2.Text))
                                    lblTercapai2.Text = "Tidak tercapai";
                                else
                                    lblTercapai2.Text = "Tercapai";
                            }
                            else if (txtActual2.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
                            {
                                if (decimal.Parse(lblTarget2.Text) > decimal.Parse(txtActual2.Text))
                                    lblTercapai2.Text = "Tidak tercapai";
                                else
                                    lblTercapai2.Text = "Tercapai";
                            }
                            else
                            {
                                if (decimal.Parse(lblTarget2.Text) >= decimal.Parse(txtActual2.Text))
                                    lblTercapai2.Text = "Tercapai";
                                else
                                    lblTercapai2.Text = "Tidak Tercapai";
                            }
                            j++;
                        }
                        i++;
                    }
                }
            }

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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            LoadTypeSarmut();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.StateView = 2;
            criteria.Visible = true;
            Judul = "LIST";
            //LoadListSarmut(int.Parse(appLevele.Value));
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            lst2.Visible = true;
            int levelApv = getAllApv(ddlBulan.SelectedValue, ddlTahun.SelectedItem.Text);
            if (levelApv == 0)
                LoadSignNamePict("admin");
            if (levelApv == 1)
                LoadSignNamePict("head");
            if (levelApv == 2)
                LoadSignNamePict("mgr");

            if (SgnAdmName != string.Empty)
                LblAdmin.Text = SgnAdmName;
            else
                LblAdmin.Text = string.Empty;
            if (PictAdmName != string.Empty)
                Image3.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictAdmName);
            else
                Image3.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");

            if (SgnMgrName != string.Empty)
                LblMgr.Text = SgnMgrName;
            else
                LblMgr.Text = string.Empty;
            if (PictMgrName != string.Empty)
                Image2.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictMgrName);
            else
                Image2.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");

            if (SgnPMName != string.Empty)
                LblPM.Text = SgnPMName;
            else
                LblPM.Text = string.Empty;
            if (PictPMName != string.Empty)
                Image1.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictPMName);
            else
                Image1.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");
            LblPlant.Text = getAlamat() + "," + DateTime.Now.ToString("dd MMM yyyy");
            Image1.Width = 47;
            Image1.Height = 27;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            //FileInfo fi = new FileInfo(dirPath);
            //if (fi.Exists)
            //{
            Response.AddHeader("content-disposition", "attachment;filename=ListSarmut.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LIST SARMUT DAN PEMANTAUAN</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            lst2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
            lst2.Visible = false;
        }
        private string GetAbsoluteUrl(string relativeUrl)
        {
            relativeUrl = relativeUrl.Replace("~/", string.Empty);
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }

                return url + relativeUrl;
            }
            return relativeUrl;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        string SgnAdmName = string.Empty;
        string PictAdmName = string.Empty;
        string SgnMgrName = string.Empty;
        string PictMgrName = string.Empty;
        string SgnPMName = string.Empty;
        string PictPMName = string.Empty;
        private void LoadSignNamePict(string sign)
        {
            SgnAdmName = string.Empty;
            PictAdmName = string.Empty;
            SgnMgrName = string.Empty;
            PictMgrName = string.Empty;
            SgnPMName = string.Empty;
            PictPMName = string.Empty;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1  and ID in (" + ddlDept.SelectedValue + ")order by dept";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (sign == "admin")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();
                    }
                    if (sign == "head")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();
                        SgnMgrName = sdr["SgnMgrName"].ToString();
                        PictMgrName = sdr["PictMgrName"].ToString();
                    }
                    if (sign == "mgr")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();
                        SgnMgrName = sdr["SgnMgrName"].ToString();
                        PictMgrName = sdr["PictMgrName"].ToString();
                        SgnPMName = sdr["SgnPMName"].ToString();
                        PictPMName = sdr["PictPMName"].ToString();
                    }
                }
            }
        }
        private int getAllApv(string bulan, string tahun)
        {
            int apvAll = 0;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 Approval from SPD_TransPrs where RowStatus>-1 and tahun=" + tahun + " and bulan=" + bulan + " and SarmutPID in " +
                "(select id from SPD_Perusahaan where DeptID in(select id from spd_dept where id=" + ddlDept.SelectedValue + ")) order by Approval desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    apvAll = Int32.Parse(sdr["Approval"].ToString());
                }
            }
            return apvAll;
        }
        private string getAlamat()
        {
            string Alamat = string.Empty;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select lokasi from company where depoid=" + users.UnitKerjaID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Alamat = sdr["lokasi"].ToString();
                }
            }
            return Alamat;
        }
        private int StateView { get; set; }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypeSarmut();
        }
        private string checkUserdeptAuth(int userid)
        {
            string result = string.Empty;

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 deptid from UserDeptAuth where RowStatus>-1 and modulname='list sarmut'and userid=" + userid + " order by deptid";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["deptid"].ToString();
                }
            }
            return result;
        }
        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            string deptid = string.Empty;
            deptid = checkUserdeptAuth(users.ID);
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (deptid == string.Empty)
            {
                if (users.DeptID != 23)
                    zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (select deptid from users where id in (select distinct userid from ListUserHead where rowstatus>-1 and " +
                        "(managerid =" + users.ID + " or mgrid =" + users.ID + " or HeadID =" + users.ID + ") )) order by dept";
                else
                    zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            }
            else
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptid in (" + deptid + ") order by dept";
            }
            SqlDataReader sdr = zl.Retrieve();
            ddlDept.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDept.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
        }
        private void LoadTypeSarmut()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from spd_Type order by ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_TypeA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisSarmut = sdr["JenisSarmut"].ToString()
                    });
                }
            }
            lstType.DataSource = arrData;
            lstType.DataBind();
            if (btnExport.Visible == true)
                LoadTypexSarmut();
        }
        private void LoadTypexSarmut()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from spd_Type order by ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_TypeA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisSarmut = sdr["JenisSarmut"].ToString()
                    });
                }
            }
            lstTypex.DataSource = arrData;
            lstTypex.DataBind();
        }
        protected void lstType_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_TypeA ba = (SPD_TypeA)e.Item.DataItem;
            Repeater rps = (Repeater)e.Item.FindControl("lstPrs");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutP(ba.ID, rps);
        }
        protected void lstTypex_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_TypeA ba = (SPD_TypeA)e.Item.DataItem;
            Repeater rps = (Repeater)e.Item.FindControl("lstPrsx");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutPx(ba.ID, rps);
        }
        public int newdata = 0;
        private void LoadListSarmutP(int ID, Repeater rps)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            string sqlApv = string.Empty;
            if (users.Apv == 1)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=1 ";
            if (users.Apv == 3)
                sqlApv = " and ST.Approval=2 ";
            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string deptid = string.Empty;
            if (ddlDept.SelectedValue == string.Empty)
                deptid = "0";
            else
                deptid = ddlDept.SelectedValue;

            zl.CustomQuery = "select * from (select isnull(B.OnSystem,0)OnSystem, case isnull(ST.Approval,0) when 0 then 'Admin' when 2 " +
                "then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,isnull(ST.TglApv,'1/1/1900')TglApv,B.ID sdeptid,isnull(ST.ID,0) ID ,B.TypeSarmutID TypeID, " +
                "B.sarmutperusahaan  [Description],S.Satuan, " +
                "case when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 then isnull(T.[TargetV1],0) " +
                "else isnull((select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + "),0)end [target],pr.Param,isnull(ST.Actual,0)Actual , " +
                "isnull(ST.Approval,0) Approval,B.Urutan,isnull(ST.Tahun," + ddlTahun.SelectedValue + ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) +
                ")bulan,isnull(ST.checked,0)checked from  SPD_perusahaan B left join SPD_Satuan S on B.satuanID=S.id  " +
                "left join SPD_TargetV T on B.targetVID=T.id left join spd_parameter pr on B.paramid =PR.ID left join (select * from spd_transprs where Tahun=" +
                ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + "  )  st on B.ID=ST.sarmutPID " +
                "where B.RowStatus>-1 and   B.typesarmutid=" + ID + " and depoid=" + users.UnitKerjaID + " and B.deptid=" +
                deptid + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsA
                    {
                        OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        TypeID = Convert.ToInt32(sdr["TypeID"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["sdeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            rps.DataSource = arrData;
            rps.DataBind();
        }
        private void LoadListSarmutPx(int ID, Repeater rps)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            string sqlApv = string.Empty;
            if (users.Apv == 1)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=1 ";
            if (users.Apv == 3)
                sqlApv = " and ST.Approval=2 ";
            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string deptid = string.Empty;
            if (ddlDept.SelectedValue == string.Empty)
                deptid = "0";
            else
                deptid = ddlDept.SelectedValue;
            zl.CustomQuery = "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' " +
                "when 1 then 'Apv Head' end StatusApv,isnull(ST.TglApv,'1/1/1900')TglApv,B.ID sdeptid,isnull(ST.ID,0) ID ,B.TypeSarmutID TypeID, B.sarmutperusahaan  [Description],S.Satuan, " +
                "case when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 then isnull(T.[TargetV1],0) " +
                "else (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")end  [target],pr.Param,isnull(ST.Actual,0)Actual , " +
                "isnull(ST.Approval,0) Approval ,B.Urutan,isnull(ST.Tahun," + ddlTahun.SelectedValue + ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) +
                ")bulan,isnull(ST.checked,0)checked from  SPD_perusahaan B left join SPD_Satuan S on B.satuanID=S.id  " +
                "left join SPD_TargetV T on B.targetVID=T.id left join spd_parameter pr on B.paramid =PR.ID left join (select * from spd_transprs where Tahun=" +
                ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + "  )  st on B.ID=ST.sarmutPID " +
                "where B.RowStatus>-1 and   B.typesarmutid=" + ID + " and depoid=" + users.UnitKerjaID + " and B.deptid=" +
                deptid + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        TypeID = Convert.ToInt32(sdr["TypeID"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["sdeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            rps.DataSource = arrData;
            rps.DataBind();
        }
        private void LoadListSarmut(int typeID, int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string sqlApv = string.Empty;
            if (users.Apv == 1)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=1 ";
            if (users.Apv == 3)
                sqlApv = " and ST.Approval=2 ";
            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            zl.CustomQuery = "select * from (select isnull(B.OnSystem,0)OnSystem,case isnull(ST.Approval,0) when 0 then 'Admin' when 2 " +
                "then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,isnull(ST.TglApv,'1/1/1900')TglApv,A.ID sdeptID, isnull(ST.ID,0)ID , A.SarmutDepartemen [Description],S.Satuan, isnull(T.TargetV1,0) [Target],pr.Param," +
                "isnull(ST.Actual,0)Actual,B.Urutan,isnull(ST.Approval,0)Approval ,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan,isnull(ST.checked,0)checked  from SPD_Departemen A Right join SPD_perusahaan B on A.sarmutpid=B.ID " +
                "left join spd_parameter pr on A.paramID=pr.id left join SPD_TargetV T on A.TargetVID=T.ID left join SPD_Satuan S on A.satuanid=S.id " +
                "left join (select * from SPD_Trans where tahun=" + ddlTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + " ) ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 where B.typesarmutid=" + typeID + " and depoid= " + users.UnitKerjaID +
                "and B.deptid= " + ddlDept.SelectedValue + " and B.ID=" + prsID + " and drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptA
                    {
                        OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListSarmutx(int typeID, int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string sqlApv = string.Empty;
            if (users.Apv == 1)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                //sqlApv = " and ST.Approval=0 "; 
                sqlApv = " and ST.Approval=1 ";
            if (users.Apv == 3)
                sqlApv = " and ST.Approval=2 ";
            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            zl.CustomQuery = "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' " +
                "when 1 then 'Apv Head' end StatusApv,isnull(ST.TglApv,'1/1/1900')TglApv,A.ID sdeptID, isnull(ST.ID,0)ID , A.SarmutDepartemen [Description],S.Satuan, " +
                "isnull(T.TargetV1,0) [Target],pr.Param," +
                "isnull(ST.Actual,0)Actual,B.Urutan,isnull(ST.Approval,0)Approval ,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan,isnull(ST.checked,0)checked  from SPD_Departemen A Right join SPD_perusahaan B on A.sarmutpid=B.ID " +
                "left join spd_parameter pr on A.paramID=pr.id left join SPD_TargetV T on A.TargetVID=T.ID left join SPD_Satuan S on A.satuanid=S.id " +
                "left join (select * from SPD_Trans where tahun=" + ddlTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + " ) ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 where B.typesarmutid=" + typeID + " and depoid= " + users.UnitKerjaID +
                "and B.deptid= " + ddlDept.SelectedValue + " and B.ID=" + prsID + " and drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListSarmutDetail(int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from (select isnull(B.OnSystem,0)OnSystem,case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' " +
                "when 1 then 'Apv Head' end StatusApv,Isnull(ST.TglApv,'1/1/1900')TglApv, A.ID sdeptID, isnull(ST.ID,0)ID , A.SubDepartemen [Description],isnull(S.Satuan,'')Satuan, isnull(T.TargetV1,0) [Target], " +
                "pr.Param,isnull(ST.Actual,0)Actual,A.Urutan,isnull(ST.Approval,0)Approval,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan from " +
                "SPD_Departemendet A inner join SPD_Departemen B on A.sarmutdepId=B.ID  left join SPD_TargetV T on A.TargetVID=T.ID " +
                "left join SPD_Satuan S on A.satuanid=S.id left join (select * from SPD_TransDet where tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + ") ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 " +
                "left join spd_parameter pr on A.paramid=pr.ID " +
                "where A.sarmutdepID=" + prsID + " and A.drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and A.sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptDetA
                    {
                        OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString())
                    });
                }
            }

            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListSarmutDetailx(int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,isnull(ST.TglApv,'1/1/1900')TglApv, A.ID sdeptID, isnull(ST.ID,0)ID , A.SubDepartemen [Description],isnull(S.Satuan,'')Satuan, isnull(T.TargetV1,0) [Target], " +
                "pr.Param,isnull(ST.Actual,0)Actual,A.Urutan,isnull(ST.Approval,0)Approval,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan from " +
                "SPD_Departemendet A inner join SPD_Departemen B on A.sarmutdepId=B.ID  left join SPD_TargetV T on A.TargetVID=T.ID " +
                "left join SPD_Satuan S on A.satuanid=S.id left join (select * from SPD_TransDet where tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + ") ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 " +
                "left join spd_parameter pr on A.paramid=pr.ID " +
                "where A.sarmutdepID=" + prsID + " and A.drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and A.sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptDetA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        TglApv = Convert.ToDateTime(sdr["TglApv"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString())
                    });
                }
            }

            lst.DataSource = arrData;
            lst.DataBind();
        }
        protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_PrsA ba = (SPD_PrsA)e.Item.DataItem;
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapaiP");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkprs");
            TextBox txtActual = (TextBox)e.Item.FindControl("txtActualP");
            if (ba.Approval > 0)
                txtActual.ReadOnly = true;
            else
                txtActual.ReadOnly = false;
            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransPrs(Tahun, Bulan, SarmutPID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }
            chk.Visible = (users.Apv > 0) ? true : false;
            chk.Checked = (ba.Checked > 0) ? true : false;
            Image att = (Image)e.Item.FindControl("attPrs");
            att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog2('" + ba.ID.ToString() + "&tp=1')");

            lblTercapai.ToolTip = ba.ID.ToString();
            txtActual.ToolTip = ba.Param;
            if (txtActual.ToolTip.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (txtActual.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }
            if (ba.OnSystem == 1)
            {
                txtActual.ReadOnly = true;
                txtActual.ToolTip = "Actual value on System";
            }
            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
            LoadListAttachmentPrs(imgs.CssClass.ToString(), rpts);

            Repeater rps = (Repeater)e.Item.FindControl("lstDetail");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmut(ba.TypeID, ba.SDeptID, rps);
        }
        protected void lstPrsx_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_PrsA ba = (SPD_PrsA)e.Item.DataItem;

            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransPrs(Tahun, Bulan, SarmutPID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
            }
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapaiP");
            if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }
            Repeater rps = (Repeater)e.Item.FindControl("lstDetailx");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutx(ba.TypeID, ba.SDeptID, rps);
        }

        private void LoadListAttachment(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,(select approval from SPD_Trans where ID=SPD_AttachmentDep.A_SarmutTransID)Approval " +
                "from SPD_AttachmentDep where rowstatus>-1 and A_SarmutTransID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_AttachmentA
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SarmutransID = Convert.ToInt32(sdr["A_SarmutTransID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListAttachmentPrs(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,(select approval from SPD_TransPrs where ID=SPD_AttachmentPrs.A_SarmutTransID)Approval " +
                "from SPD_AttachmentPrs where rowstatus>-1 and A_SarmutTransID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_AttachmentPrsA
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SarmutransID = Convert.ToInt32(sdr["A_SarmutTransID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihat") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                SPD_AttachmentA att = (SPD_AttachmentA)e.Item.DataItem;
                //pre.Attributes.Add("onclick", "PDFPreviewSarmut('" + pre.CssClass.ToString() + "')");
                hps.Visible = (att.Approval < 1) ? true : false;
                //hps.Visible = (users.Apv < 1) ? true : false;

            }
        }
        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "pre":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\";
                        string ext = Path.GetExtension(Nama);
                        HttpResponse response = HttpContext.Current.Response;
                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            response.Clear();
                            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            response.AddHeader("Content-Length", file.Length.ToString());
                            response.ContentType = "application/octet-stream";
                            response.WriteFile(file.FullName);
                            response.End();
                        }
                        break;
                    case "hps":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update SPD_AttachmentDep set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }

        }
        protected void attachPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            string[] viewApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApprovalStatus", "BeritaAcara").Split(',');
            int idx = Array.IndexOf(viewApv, users.DeptID.ToString());
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
                Image hps = (Image)e.Item.FindControl("hapusprs");
                SPD_AttachmentPrsA att = (SPD_AttachmentPrsA)e.Item.DataItem;
                hps.Visible = (att.Approval < 1) ? true : false;
                hps.Visible = (users.Apv < 1) ? true : false;
            }
        }
        protected void attachPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "preprs":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\";
                        string ext = Path.GetExtension(Nama);

                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                        break;
                    case "hpsprs":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update SPD_AttachmentPrs set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        //LoadListAttachmentPrs(hps.AlternateText.ToString(), rpt);
                        LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void lstDetail_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_DeptA ba = (SPD_DeptA)e.Item.DataItem;
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapai");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            TextBox txtActual = (TextBox)e.Item.FindControl("txtActual");
            if (ba.Approval > 0)
                txtActual.ReadOnly = true;
            else
                txtActual.ReadOnly = false;
            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_Trans(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }

            lblTercapai.ToolTip = ba.ID.ToString();
            txtActual.ToolTip = ba.Param;
            if (txtActual.ToolTip.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (txtActual.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }
            if (ba.OnSystem == 1)
            {
                txtActual.ReadOnly = true;
                txtActual.ToolTip = "Actual value on System";
            }
            chk.Visible = (users.Apv > 0) ? true : false;
            chk.Checked = (ba.Checked > 0) ? true : false;
            Image att = (Image)e.Item.FindControl("att");
            att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() + "&tp=1')");
            Repeater rps = (Repeater)e.Item.FindControl("attachm");
            //HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListAttachment(ba.ID.ToString(), rps);

            Repeater rps1 = (Repeater)e.Item.FindControl("lstDetail2");
            HtmlGenericControl ps1 = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutDetail(ba.SDeptID, rps1);

        }
        protected void lstDetailx_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SPD_DeptA ba = (SPD_DeptA)e.Item.DataItem;
            Users users = (Users)Session["Users"];
            Label lblTercapai2 = (Label)e.Item.FindControl("lblTercapai2");
            Label lblTarget2 = (Label)e.Item.FindControl("lblTarget2");
            Label lblStatusApv = (Label)e.Item.FindControl("lblStatusApv");
            Label lblTglApv = (Label)e.Item.FindControl("lblTglApv");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk2");
            TextBox txtActual2 = (TextBox)e.Item.FindControl("txtActual2");

            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_Trans(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapai");
            if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }


            //if (ba.Satuan.Trim() == string.Empty)
            //{
            //    lblTercapai2.Text = string.Empty;
            //    lblTarget2.Text = string.Empty;
            //    txtActual2.Text = string.Empty;
            //    lblStatusApv.Text = string.Empty;
            //    txtActual2.Enabled = false;
            //}
            Repeater rps1 = (Repeater)e.Item.FindControl("lstDetail2x");
            HtmlGenericControl ps1 = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutDetailx(ba.SDeptID, rps1);

        }
        protected void lstDetail2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_DeptDetA ba = (SPD_DeptDetA)e.Item.DataItem;
            Label lblTercapai2 = (Label)e.Item.FindControl("lblTercapai2");
            Label lblTarget2 = (Label)e.Item.FindControl("lblTarget2");
            Label lblStatusApv = (Label)e.Item.FindControl("lblStatusApv");
            Label lblTglApv = (Label)e.Item.FindControl("lblTglApv");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk2");
            TextBox txtActual2 = (TextBox)e.Item.FindControl("txtActual2");
            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransDet(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }

            lblTercapai2.ToolTip = ba.ID.ToString();
            txtActual2.ToolTip = ba.Param;
            if (txtActual2.ToolTip.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai2.Text = "Tidak Tercapai";
                else
                    lblTercapai2.Text = "Tercapai";
            }
            else if (txtActual2.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai2.Text = "Tidak Tercapai";
                else
                    lblTercapai2.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai2.Text = "Tercapai";
                else
                    lblTercapai2.Text = "Tidak Tercapai";
            }
            if (ba.Satuan.Trim() == string.Empty)
            {
                lblTercapai2.Text = string.Empty;
                lblTarget2.Text = string.Empty;
                txtActual2.Text = string.Empty;
                lblStatusApv.Text = string.Empty;
                txtActual2.Enabled = false;
            }
            chk.Visible = (ba.Approval < ApprovalLevel) ? chk.Visible : false;
        }

        protected void lstDetail2_Command(object sender, RepeaterCommandEventArgs e)
        {

        }
        protected void lstPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attachPrs":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
                    LoadTypeSarmut();
                    break;
            }
        }
        protected void lstDetail_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attach":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachm");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("att");
                    LoadTypeSarmut();
                    break;
            }
        }
        protected void chk_CheckedChangeRpt(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (chk.Checked == true)
                zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
            else
                zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
            SqlDataReader sdr = zl.Retrieve();
            LoadTypeSarmut();
        }
        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (chk.Checked == true)
                zl.CustomQuery = "update spd_transPrs set checked=1 where id=" + transID + " " +
                "update SPD_Trans set checked=1 where tahun=(select Tahun from SPD_TransPrs where id=" + transID + ") and bulan= " +
                "(select bulan from SPD_TransPrs where id=" + transID + ") and  SarmutDeptID in ( " +
                "select id from SPD_Departemen where SarmutPID in (select sarmutpid from SPD_TransPrs where id=" + transID + ")) ";
            else
                zl.CustomQuery = "update spd_transPrs set checked=0 where id=" + transID + " " +
                "update SPD_Trans set checked=0 where tahun=(select Tahun from SPD_TransPrs where id=" + transID + ") and bulan= " +
                "(select bulan from SPD_TransPrs where id=" + transID + ") and  SarmutDeptID in ( " +
                "select id from SPD_Departemen where SarmutPID in (select sarmutpid from SPD_TransPrs where id=" + transID + ")) ";
            SqlDataReader sdr = zl.Retrieve();
            LoadTypeSarmut();
        }
        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            int i = 0;
            string transID = string.Empty;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        CheckBox chk = (CheckBox)lstDetail.Items[i].FindControl("chk");
                        chk.Checked = chkAll.Checked;
                        transID = chk.ToolTip;
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        if (chk.Checked == true)
                            zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
                        else
                            zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
                        SqlDataReader sdr = zl.Retrieve();
                        i++;
                    }
                }
            }
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                i = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    CheckBox chk = (CheckBox)lstPrs.Items[i].FindControl("chkprs");
                    chk.Checked = chkAll.Checked;
                    transID = chk.ToolTip;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (chk.Checked == true)
                        zl.CustomQuery = "update spd_transprs set checked=1 where id=" + transID;
                    else
                        zl.CustomQuery = "update spd_transprs set checked=0 where id=" + transID;
                    SqlDataReader sdr = zl.Retrieve();
                    i++;
                }
            }
            LoadTypeSarmut();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            Repeater lstDetail;
            Repeater lstPrs;

            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        Label lblTercapai = (Label)lstDetail.Items[i].FindControl("lblTercapai");
                        Label lblTarget = (Label)lstDetail.Items[i].FindControl("lblTarget");
                        CheckBox chk = (CheckBox)lstDetail.Items[i].FindControl("chk");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        if (chk.Checked == true)
                            zl.CustomQuery = "update SPD_Trans set approval =" + users.Apv + ", TglApv = GETDATE()  where ID=" + chk.ToolTip +
                                " update SPD_Transdet set approval =" + users.Apv + ", TglApv = GETDATE() where Tahun=" + ddlTahun.SelectedItem.Text +
                        " and bulan=" + ddlBulan.SelectedValue + " and sarmutdeptID in (select id from SPD_DepartemenDet " +
                        "where SarmutDepID in (select SarmutDeptID  from  SPD_Trans  where ID=" + chk.ToolTip + " ))";
                        SqlDataReader sdr = zl.Retrieve();
                        i++;
                    }
                }
            }

            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                int j = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    CheckBox chk = (CheckBox)lstPrs.Items[j].FindControl("chkprs");
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (chk.Checked == true)
                        zl.CustomQuery = "update SPD_TransPrs set approval =" + users.Apv + ", TglApv = GETDATE() where ID=" + chk.ToolTip;
                    SqlDataReader sdr = zl.Retrieve();

                    #region Update ke Buku Besar ISO
                    if (chk.Checked == true && users.Apv > 1)
                    {
                        string Tahun = string.Empty; string NilaiBulan = string.Empty; string NamaBulan = string.Empty;
                        NilaiBulan = ddlBulan.SelectedValue;
                        Tahun = ddlTahun.SelectedValue;

                        if (NilaiBulan == "1") { NamaBulan = "Jan"; }
                        else if (NilaiBulan == "2") { NamaBulan = "Feb"; }
                        else if (NilaiBulan == "3") { NamaBulan = "Mar"; }
                        else if (NilaiBulan == "4") { NamaBulan = "Apr"; }
                        else if (NilaiBulan == "5") { NamaBulan = "Mei"; }
                        else if (NilaiBulan == "6") { NamaBulan = "Jun"; }
                        else if (NilaiBulan == "7") { NamaBulan = "Jul"; }
                        else if (NilaiBulan == "8") { NamaBulan = "Agu"; }
                        else if (NilaiBulan == "9") { NamaBulan = "Sep"; }
                        else if (NilaiBulan == "10") { NamaBulan = "Okt"; }
                        else if (NilaiBulan == "11") { NamaBulan = "Nov"; }
                        else if (NilaiBulan == "12") { NamaBulan = "Des"; }

                        BBFin b = new BBFin();
                        FacadeBBFin f = new FacadeBBFin();
                        b = f.RetrieveItemSarmut(chk.ToolTip);

                        ArrayList arrBB = new ArrayList();
                        FacadeBBFin F = new FacadeBBFin();
                        arrBB = F.RetrieveNilaiMapping(b.SarMutPerusahaan, ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());
                        if (arrBB.Count > 0)
                        {
                            int i = 0;
                            foreach (BBFin List1 in arrBB)
                            {
                                if (List1.SubItemSarmut == null)
                                {
                                    ZetroView z = new ZetroView();
                                    z.QueryType = Operation.CUSTOM;
                                    if (chk.Checked == true)
                                        z.CustomQuery =
                                        "update SPD_PencapaianNilai set " + List1.InitialBulan + " = '" + List1.Nilai + "' where Tahun='" + ddlTahun.SelectedValue + "' " +
                                        "and ParameterTerukur='" + List1.ParameterTerukur + "' and spdsarmut='" + List1.ItemSarmut + "' and RowStatus>-1 ";
                                    SqlDataReader sd = z.Retrieve();
                                }
                                else
                                {
                                    ZetroView z = new ZetroView();
                                    z.QueryType = Operation.CUSTOM;
                                    if (chk.Checked == true)
                                        z.CustomQuery =
                                        "update SPD_PencapaianNilai set " + List1.InitialBulan + " = '" + List1.Nilai + "' where Tahun='" + ddlTahun.SelectedValue + "' " +
                                        "and ParameterTerukur='" + List1.ParameterTerukur + "' and spdsarmut='" + List1.ItemSarmut + "' and RowStatus>-1 " +
                                        "and subspdsarmut='" + List1.SubItemSarmut + "'";
                                    SqlDataReader sd = z.Retrieve();
                                }
                            }
                            i = i + 1;
                        }


                    }


                    #endregion

                    j++;
                }
            }
            LoadTypeSarmut();
        }

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
        //private void UploadFile(int BAID)
        //{
        //    if (Upload1.HasFile)
        //    {
        //        string FilePath = Upload1.PostedFile.FileName;
        //        string filename = Path.GetFileName(FilePath);
        //        string ext = Path.GetExtension(filename);
        //        if (ext.ToLower() == ".pdf")
        //        {
        //            Stream fs = Upload1.PostedFile.InputStream;
        //            BinaryReader br = new BinaryReader(fs);
        //            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //            ZetroLib zl = new ZetroLib();
        //            zl.Option = "Insert";
        //            zl.Criteria = "BAID,FileName,Attachment,RowStatus,Createdby,CreatedTime";
        //            zl.hlp = new AttachmentBA();
        //            zl.StoreProcedurName = "spBeritaAcaraAtt_Insert";
        //            string rst = zl.CreateProcedure();
        //            if (rst == string.Empty)
        //            {
        //                AttachmentBA ba = new AttachmentBA();
        //                ba.BAID = BAID;
        //                ba.FileName = filename.ToString();
        //                ba.Attachment = bytes;
        //                ba.RowStatus = 0;
        //                ba.CreatedBy = ((Users)Session["Users"]).UserName;
        //                zl.hlp = ba;
        //                int rs = zl.ProcessData();
        //                if (rs > 0)
        //                {
        //                    //LoadListSarmut(int.Parse(appLevele.Value));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
        //        }
        //    }
        //}
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

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypeSarmut();
        }
        protected void RbApv_CheckedChanged(object sender, EventArgs e)
        {
            if (RbApv.Checked == true)
            {
                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
                btnExport.Visible = false;
                LoadTypeSarmut();
            }
        }
        protected void RbList_CheckedChanged(object sender, EventArgs e)
        {
            if (RbList.Checked == true)
            {
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
                btnExport.Visible = true;
                LoadTypeSarmut();
            }
        }
        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypeSarmut();
        }
    }
}


public class SPD_TypeA : GRCBaseDomain
{
    public string JenisSarmut { get; set; }
}
public class SPD_PrsA : GRCBaseDomain
{
    public string Description { get; set; }
    public string Satuan { get; set; }
    public string Param { get; set; }
    public decimal Target { get; set; }
    public decimal Actual { get; set; }
    public int Approval { get; set; }
    public int Urutan { get; set; }
    public int TypeID { get; set; }
    public int SDeptID { get; set; }
    public int Checked { get; set; }
    public string StatusApv { get; set; }
    public int OnSystem { get; set; }
    public DateTime TglApv { get; set; }
}
public class SPD_DeptA : GRCBaseDomain
{
    public string Description { get; set; }
    public string Satuan { get; set; }
    public string Param { get; set; }
    public decimal Target { get; set; }
    public decimal Actual { get; set; }
    public int Approval { get; set; }
    public int SDeptID { get; set; }
    public int Urutan { get; set; }
    public int Checked { get; set; }
    public string StatusApv { get; set; }
    public int OnSystem { get; set; }
    public DateTime TglApv { get; set; }
}
public class SPD_DeptDetA : GRCBaseDomain
{
    public string Description { get; set; }
    public string Satuan { get; set; }
    public string Param { get; set; }
    public decimal Target { get; set; }
    public decimal Actual { get; set; }
    public int Approval { get; set; }
    public int SDeptID { get; set; }
    public int Urutan { get; set; }
    public int Checked { get; set; }
    public string StatusApv { get; set; }
    public DateTime TglApv { get; set; }
    public int OnSystem { get; set; }
}
public class SPD_AttachmentA : GRCBaseDomain
{
    public string FileName { get; set; }
    public string DocName { get; set; }
    public int SarmutransID { get; set; }
    public int Approval { get; set; }
}

public class SPD_AttachmentPrsA : GRCBaseDomain
{
    public string FileName { get; set; }
    public string DocName { get; set; }
    public int SarmutransID { get; set; }
    public int Approval { get; set; }
}

public class FacadeBBFin
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private BBFin objBB = new BBFin();
    private List<SqlParameter> sqlListParam;

    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }

    public int RetrieveApv(string ID, string bulan, string tahun)
    {
        string StrSql =
        " select Approval from SPD_TransPrs where ID=" + ID + " and SarmutPID in (select ID from SPD_Perusahaan where DeptID=2 " +
        " and SarMutPerusahaan='Efisiensi Budget Finishing' and RowStatus>-1) and RowStatus>-1 and bulan=" + bulan + " and tahun=" + tahun + " ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToInt32(sqlDataReader["Approval"]);
            }
        }

        return 0;
    }

    public BBFin RetrieveItemSarmut(string ID)
    {
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        string query = string.Empty;

        query =
        " select SarMutPerusahaan from SPD_Perusahaan where ID in (select SarmutPID from SPD_TransPrs where ID=" + ID + " and RowStatus>-1) and RowStatus>-1 ";

        string strSQL = query;


        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObj(sqlDataReader);
            }
        }

        return new BBFin();
    }

    public BBFin GenerateObj(SqlDataReader sqlDataReader)
    {
        objBB = new BBFin();
        objBB.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
        return objBB;
    }

    public ArrayList RetrieveNilaiMapping(string ItemSarmut, string bln, string thn)
    {
        arrData = new ArrayList();
        string strSQL =
        " select * from BukuBesar_Mapping where bulan=" + bln + " and tahun=" + thn + " and ItemSarMut='" + ItemSarmut + "'";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);

        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new BBFin
                {
                    ItemSarmut = sdr["ItemSarmut"].ToString(),
                    ParameterTerukur = sdr["ParameterTerukur"].ToString(),
                    Bulan = sdr["Bulan"].ToString(),
                    Nilai = Convert.ToDecimal(sdr["Nilai"].ToString()),
                    Tahun = sdr["Tahun"].ToString(),
                    InitialBulan = sdr["InitialBulan"].ToString(),
                    SubItemSarmut = sdr["SubItemSarmut"].ToString()
                });
            }
        }
        return arrData;
    }

}

public class BBFin
{
    public string SarMutPerusahaan { get; set; }
    public string ParameterTerukur { get; set; }
    public string InitialBulan { get; set; }
    public string ItemSarmut { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string SubItemSarmut { get; set; }

    public int Rowstatus { get; set; }
    public decimal TotalBudget { get; set; }
    public decimal Nilai { get; set; }
}