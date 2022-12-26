using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
////using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;
//using System.Text;

namespace GRCweb1.Modul.ISO
{
    public partial class RekapAnalisaResiko : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Request.QueryString["path"];
            if (path != null)
            {
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(path);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                LoadDept();
                LoadTahun();
                if (user.DeptID != 23)
                {
                    btnSolved.Visible = false;
                    btnCancel.Visible = false;
                }
                if (user.UnitKerjaID == 1)
                {
                    NoForm.Text = "ISO/RA/41/18/R5";
                }
                else if (user.UnitKerjaID == 7)
                {
                    NoForm.Text = "ISO/K/RA/41/18/R5 ";
                }
                else
                {
                    NoForm.Text = "ISO/J/RA/28/20/R1";
                }
            }
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (users.DeptID == 23)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            }
            else if (users.DeptID == 14)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            }
            else
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            }
            //if (users.DeptID !=23)
            //    zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            //else
            //zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptName.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
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

        protected void btnSolved_ServerClick(object sender, EventArgs e)
        {
            #region 1st
            for (int i = 0; i < lstPMX.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstPMX.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    int Result = 0;
                    Users user = (Users)Session["Users"];
                    AnaLisaResiko1 riskD = new AnaLisaResiko1();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();
                    int Status = 1;

                    riskD.Status = Status;
                    riskD.ID = int.Parse(chk.ToolTip.ToString());
                    //riskD.LastModifiedBy = user.UserName;
                    //riskD.DeptID = ((Users)Session["Users"]).DeptID;
                    string strSQL = "update ISO_AnalisaTrans set Status =" + Status + " where ID = " + chk.ToolTip.ToString() + " ";
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                        Result = -1;
                    //return Result;
                    DisplayAJAXMessage(this, "Solved !!!!");
                    chk.Checked = false;
                    btnSolved.Enabled = false;
                }
            }
            #endregion

            #region 2nd
            //for (int i = 0; i < lstPMX.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)lstPMX2.Items[i].FindControl("chkprs2");
            //    if (chk.Checked == true)
            //    {
            //        int Result = 0;
            //        Users user = (Users)Session["Users"];
            //        AnaLisaResiko1 riskD = new AnaLisaResiko1();
            //        string strError = string.Empty;
            //        string UserID = user.ID.ToString();
            //        int Status = 1;

            //        riskD.Status = Status;
            //        riskD.ID = int.Parse(chk.ToolTip.ToString());
            //        //riskD.LastModifiedBy = user.UserName;
            //        //riskD.DeptID = ((Users)Session["Users"]).DeptID;
            //        string strSQL = "update ISO_AnalisaTrans set Status =" + Status + " where ID = " + chk.ToolTip.ToString() + " ";
            //        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //        if (dataAccess.Error != string.Empty)
            //            Result = -1;
            //        //return Result;
            //        DisplayAJAXMessage(this, "Solved !!!!");
            //        chk.Checked = false;
            //        btnSolved.Enabled = false;
            //    }
            //}
            #endregion

        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            #region 1st
            for (int i = 0; i < lstPMX.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstPMX.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    int Result = 0;
                    Users user = (Users)Session["Users"];
                    AnaLisaResiko1 riskD = new AnaLisaResiko1();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();
                    int RowStatus = -1;

                    riskD.RowStatus = RowStatus;
                    riskD.ID = int.Parse(chk.ToolTip.ToString());
                    //riskD.LastModifiedBy = user.UserName;
                    //riskD.DeptID = ((Users)Session["Users"]).DeptID;
                    string strSQL = "update ISO_AnalisaTrans set RowStatus =" + RowStatus + " where ID = " + chk.ToolTip.ToString() + " and Apv=0 ";
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                        Result = -1;
                    DisplayAJAXMessage(this, "Cancel Berhasil!!!");
                    chk.Checked = false;
                    btnSolved.Enabled = false;
                    LoadAnalisaResiko();
                }
            }
            #endregion

        }

        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
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
                    case "hapus":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update ISO_AnalisaRAttachment set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        LoadAnalisaResiko();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }



        }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            //mgrHRD.Visible = false;
            //MgrHRDCorp.Visible = false;
            //Image2.Visible = false;
            //MgrBM.Visible = false;
            //HeadISOHrd.Visible = false;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //panel1.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Alamat.Text = getAlamat() + "," + DateTime.Now.ToString("dd MMM yyyy");
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;

            Response.AddHeader("content-disposition", "attachment;filename=AnalisaResiko.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            string Html = "<center><H2>RISK ASSESMENT</H2></center>";
            Html += "Departemen :" + ddlDeptName.SelectedItem.Text;
            string HtmlEnd = "";
            Panel2.RenderControl(hw);

            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void btnForm_serverClick(object sender, EventArgs e)
        {
            Response.Redirect("AnalisaResiko.aspx");
        }

        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                //ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
                ddlTahun.Items.Add(new System.Web.UI.WebControls.ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
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



        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void attachPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
            }
        }

        protected void attachPrs2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs2") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
            }
        }

        protected void attachPrs2_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "preprs2":
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
                    case "hpsprs2":
                        //Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                        //ZetroView zl = new ZetroView();
                        //zl.QueryType = Operation.CUSTOM;
                        //zl.CustomQuery = "Update SPD_AttachmentPrs set RowStatus=-1 where ID=" + hps.CssClass;
                        //SqlDataReader sdr = zl.Retrieve();
                        ////LoadListAttachmentPrs(hps.AlternateText.ToString(), rpt);
                        //LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }
        }

        protected void attachPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            #region old

            //Repeater rpt = (Repeater)sender;
            //try
            //{
            //    switch (e.CommandName)
            //    {
            //        case "preprs":
            //            string Nama = e.CommandArgument.ToString();
            //            string Nama2 = @"\" + Nama;
            //            string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\";
            //            string ext = Path.GetExtension(Nama);

            //            Response.Clear();
            //            string excelFilePath = dirPath + Nama;
            //            System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
            //            if (file.Exists)
            //            {
            //                Response.Clear();
            //                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //                Response.AddHeader("Content-Length", file.Length.ToString());
            //                Response.ContentType = "application/octet-stream";
            //                Response.WriteFile(file.FullName);
            //                Response.End();
            //            }
            //            break;
            //        case "hapus":
            //            Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
            //            ZetroView zl = new ZetroView();
            //            zl.QueryType = Operation.CUSTOM;
            //            zl.CustomQuery = "Update ISO_AnalisaRAttachment set RowStatus=-1 where ID=" + hps.CssClass;
            //            SqlDataReader sdr = zl.Retrieve();
            //            LoadAnalisaResiko();
            //            break;
            //    }
            //}
            //catch
            //{
            //    DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
            //    return;
            //}
            #endregion

        }

        protected void lstPMX2_Databound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            AnaLisaResiko1 ba = (AnaLisaResiko1)e.Item.DataItem;
            System.Web.UI.WebControls.Image att = (System.Web.UI.WebControls.Image)e.Item.FindControl("attPrs2");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkprs2");
            att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog2('" + ba.ID.ToString() + "&tp=1')");
            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs2");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs2");
            int Ada = Convert.ToInt32(Session["Ada"]);
            if (Ada == 1 || users.DeptID != 23)
            { chk.Visible = false; chkAll.Visible = false; }
            LoadListAttachmentPrs(imgs.CssClass.ToString(), rpts);
            if (ba.StatusX == "Solved")
            {
                chk.Checked = true;
                chk.Enabled = false;
            }
        }

        protected void lstPMX_Databound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            AnaLisaResiko1 ba = (AnaLisaResiko1)e.Item.DataItem;
            System.Web.UI.WebControls.Image att = (System.Web.UI.WebControls.Image)e.Item.FindControl("attPrs");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkprs");
            //att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog2('" + ba.ID.ToString() + "&tp=1')");
            Repeater rpts = (Repeater)e.Item.FindControl("attachmx");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
            Label lbl = (Label)e.Item.FindControl("idxx");
            LoadListAttachmentPrs(imgs.CssClass.ToString(), rpts);
            int Ada = Convert.ToInt32(Session["Ada"]);
            if (Ada == 1 || users.DeptID != 23)
            { chk.Visible = false; chkAll.Visible = false; }
            if (ba.StatusX == "Solved")
            {
                chk.Checked = true;
                chk.Enabled = false;
            }

            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label L2 = e.Item.FindControl("AnalisaRisk") as Label;
                if (L2.Text.Trim() == "Lain-lain")
                {
                    L2.Visible = false;
                }
                else
                {
                    L2.Visible = true;
                }
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (ddlSemester.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Semester");
                return;
            }
            string strQuery = string.Empty;
            strQuery = "  select ROW_NUMBER ()OVER(ORDER BY id) as No,* from  ( " +
                             " select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueInternal2,A.IssueEkternal1,A.IssueEkternal2, " +
                             " A.Peluang1,A.Peluang2,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'High' when A.LvlResiko >=6  then 'Medium' when A.LvlResiko >=1  then 'Low' end LvlResiko1, " +
                             " A.Treatment1,A.Treatment2, case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' " +
                             " when  DAY(A.DueDate1) between 15 and 21  then 'M3' else 'M4' end DueDate, " +
                             " Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv,case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager/Corp Manager' " +
                             " when A.Apv='2' then 'ISO' end Approval,Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus, case when MONTH(A.DueDate1) " +
                             " between 1 and 6 then 'Semester I' else 'Semester II' end Periode,D.FileName from ISO_AnalisaTrans A " +
                             " inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID " +
                             " inner join ISO_DeptRisk C on A.DeptID=C.ID left join ISO_AnalisaRAttachment D on D.A_SarmutTransID=A.ID ) " +
                             " as data1 where Periode='" + ddlSemester.SelectedItem + "' and data1.Tahun=" + ddlTahun.SelectedValue + " and data1.DeptID= " + Convert.ToInt32(ddlDeptName.SelectedValue) + " order by data1.ID asc ";

            Session["query"] = strQuery;
            Session["xjudul"] = "RISK ASSESMENT";
            Session["formno"] = "Form No. ISO/RA/41/18/R4";
            Session["namaPT"] = "PT.BANGUNPERKASA ADHITAMASENTRA";
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=AnalisaResiko', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1100px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadListAttachmentPrs(string BAID, Repeater attachmx)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select ID,docName,FileName,CreatedTime from ISO_AnalisaRAttachment where rowstatus>-1 and A_SarmutTransID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new AnaLisaResiko1
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            //SarmutransID = Convert.ToInt32(sdr["A_SarmutTransID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                            //Apv = Convert.ToInt32(sdr["Apv"].ToString())
                        });
                    }
                }
            }
            attachmx.DataSource = arrData;
            attachmx.DataBind();
        }

        protected void lstPMX2_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attachPrs2":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachPrs2");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs2");
                    //LoadTypeSarmut();
                    break;
            }
        }

        protected void lstPMX_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attachPrs":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
                    //LoadTypeSarmut();
                    break;
            }
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            int levelApv = getAllApv();
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID == 1)
            {

                if (levelApv == 1 && users.DeptID == 7)
                {
                    mgrHRD.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 7)
                {
                    mgrHRD.Visible = true;
                    MgrHRDCorp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 7)
                {
                    mgrHRD.Visible = true;
                    MgrHRDCorp.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 2)
                {
                    MgrBM.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 2)
                {
                    MgrBM.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 2)
                {
                    MgrBM.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 9)
                {
                    MgrQA.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 9)
                {
                    MgrQA.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 9)
                {
                    MgrQA.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 6)
                {
                    MgrLogBj.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 6)
                {
                    MgrLogBj.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 6)
                {
                    MgrLogBj.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 10)
                {
                    MgrLogBj.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 10)
                {
                    MgrLogBj.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 10)
                {
                    MgrLogBj.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 3)
                {
                    MgrFin.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 3)
                {
                    MgrFin.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 3)
                {
                    MgrFin.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                    MGRITCorp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                    MGRITCorp.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                    MgrPurch.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                    MgrPurch.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 19)
                {
                    MgrMtn.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 19)
                {
                    MgrMtn.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 19)
                {
                    MgrMtn.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 23)
                {
                    HeadISOCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 23)
                {
                    HeadISOCtrp.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 23)
                {
                    HeadISOCtrp.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 11)
                {
                    MgrPPICCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 11)
                {
                    MgrPPICCtrp.Visible = true;
                    PMbm.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 11)
                {
                    MgrPPICCtrp.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                Alamat.Text = getAlamat() + ", &nbsp;" + DateTime.Now.ToString("dd MMM yyyy");
                LoadAnalisaResiko();
            }
            else if (users.UnitKerjaID == 7)
            {
                if (levelApv == 1 && users.DeptID == 7)
                {
                    mgrHRDKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 7)
                {
                    mgrHRDKrwg.Visible = true;
                    MgrHRDCorp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 7)
                {
                    mgrHRDKrwg.Visible = true;
                    MgrHRDCorp.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 2)
                {
                    MgrBMKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 2)
                {
                    MgrBMKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 2)
                {
                    MgrBMKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 9)
                {
                    MgrQAKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 9)
                {
                    MgrQAKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 9)
                {
                    MgrQAKrwg.Visible = true;
                    PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 6)
                {
                    MgrLogBjKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 6)
                {
                    MgrLogBjKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 6)
                {
                    MgrLogBjKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 10)
                {
                    MgrLogBjKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 10)
                {
                    MgrLogBjKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 10)
                {
                    MgrLogBjKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 3)
                {
                    MgrFinKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 3)
                {
                    MgrFinKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 3)
                {
                    MgrFinKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                    MGRITCorp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 14)
                {
                    MgrIT.Visible = true;
                    MGRITCorp.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                    MgrPurch.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 15)
                {
                    spvPurch.Visible = true;
                    MgrPurch.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 19)
                {
                    MgtMtnKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 19)
                {
                    MgtMtnKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 19)
                {
                    MgtMtnKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 23)
                {
                    HeadISOKRwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 23)
                {
                    HeadISOKRwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 23)
                {
                    HeadISOKRwg.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 11)
                {
                    MgrPPICKrwg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 11)
                {
                    MgrPPICKrwg.Visible = true;
                    PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 11)
                {
                    MgrPPICCtrp.Visible = true;
                    PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                Alamat.Text = getAlamat() + ", &nbsp;" + DateTime.Now.ToString("dd MMM yyyy");
                LoadAnalisaResiko();
            }
            else
            {
                if (levelApv == 1 && users.DeptID == 7)
                {
                    mgrHRDJmbg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 7)
                {
                    mgrHRDJmbg.Visible = true;
                    MgrHRDCorp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 7)
                {
                    mgrHRDJmbg.Visible = true;
                    MgrHRDCorp.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 2)
                {
                    MgrBMJmbg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 2)
                {
                    MgrBMJmbg.Visible = true;
                    //PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 2)
                {
                    MgrBMJmbg.Visible = true;
                    //PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 9)
                {
                    MgrQAJmbg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 9)
                {
                    MgrQAJmbg.Visible = true;
                    //PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 9)
                {
                    MgrQAJmbg.Visible = true;
                    //PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 19)
                {
                    MgtMtnJmbg.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 19)
                {
                    MgtMtnJmbg.Visible = true;
                    //PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 19)
                {
                    MgtMtnJmbg.Visible = true;
                    //PMbm.Visible = true;
                    HeadISOHrd.Visible = true;
                }

                if (levelApv == 1 && users.DeptID == 23)
                {
                    HeadISOJombang.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 23)
                {
                    HeadISOJombang.Visible = true;
                    //PMbmKrwg.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 23)
                {
                    HeadISOJombang.Visible = true;
                    //PMbmKrwg.Visible = true;
                    HeadISOHrd.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 26)
                {
                    MgrDeliCtrp.Visible = true;
                    MgrDeliCtrp.Visible = true;
                    MgrCorpDelCtrp.Visible = true;
                }
                if (levelApv == 1 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                }
                if (levelApv == 2 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                if (levelApv == 3 && users.DeptID == 13)
                {
                    MgrMktCtrp.Visible = true;
                    MgrMktCtrp.Visible = true;
                    MgrCoprMkt.Visible = true;
                }
                Alamat.Text = getAlamat() + ", &nbsp;" + DateTime.Now.ToString("dd MMM yyyy");
                LoadAnalisaResiko();
            }
        }

        private int getAllApv()
        {
            int apvAll = 0;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select top 1 Apv from  ( " +
                             " select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueEkternal1," +
                             " A.Peluang1,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'High' when A.LvlResiko >=6  then 'Medium' when A.LvlResiko >=1  then 'Low' end LvlResiko1, " +
                             " A.Treatment1,case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' when  DAY(A.DueDate1) between 15 and 21  then 'M3' else 'M4' end DueDate, " +
                             " Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv,case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager' when A.Apv='2' then 'Corp.Plant Mgr/Corp.Mgr' else 'Corp.ISO Manager' end Approval,Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus, case when MONTH(A.DueDate1) between 1 and 6 then 'Semester I' else 'Semester II' end Periode,D.FileName from ISO_AnalisaTrans A " +
                             " inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID " +
                             " inner join ISO_DeptRisk C on A.DeptID=C.ID left join ISO_AnalisaRAttachment D on D.A_SarmutTransID=A.ID ) as data1 where Periode='" + ddlSemester.SelectedItem + "' and data1.Tahun=" + ddlTahun.SelectedValue + " and data1.DeptID= " + Convert.ToInt32(ddlDeptName.SelectedValue) + " and data1.RowStatus>-1 order by data1.ID asc ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    apvAll = Int32.Parse(sdr["Apv"].ToString());
                }
            }
            return apvAll;
        }

        public int newdata = 0;

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            int levelApv = getAllApv();
            Users users = (Users)Session["Users"];
            if (levelApv == 1 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
                MgrHRDCorp.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
                MgrHRDCorp.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }
            if (levelApv == 1 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }
            if (levelApv == 1 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
                MGRITCorp.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
                MGRITCorp.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
                MgrPurch.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
                MgrPurch.Visible = true;
                HeadISOHrd.Visible = true;
            }
            Alamat.Text = getAlamat() + ", &nbsp;" + DateTime.Now.ToString("dd MMM yyyy");
            LoadAnalisaResiko();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int levelApv = getAllApv();
            Users users = (Users)Session["Users"];
            if (levelApv == 1 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
                MgrHRDCorp.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 7)
            {
                mgrHRD.Visible = true;
                MgrHRDCorp.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 2)
            {
                MgrBM.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 9)
            {
                MgrQA.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }
            if (levelApv == 1 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 6)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }
            if (levelApv == 1 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 10)
            {
                MgrLogBj.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }
            if (levelApv == 1 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
                PMbm.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 3)
            {
                MgrFin.Visible = true;
                PMbm.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
                MGRITCorp.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 14)
            {
                MgrIT.Visible = true;
                MGRITCorp.Visible = true;
                HeadISOHrd.Visible = true;
            }

            if (levelApv == 1 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
            }
            if (levelApv == 2 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
                MgrPurch.Visible = true;
            }
            if (levelApv == 3 && users.DeptID == 15)
            {
                spvPurch.Visible = true;
                MgrPurch.Visible = true;
                HeadISOHrd.Visible = true;
            }
            Alamat.Text = getAlamat() + ", &nbsp;" + DateTime.Now.ToString("dd MMM yyyy");
            LoadAnalisaResiko();
        }

        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            if (chk.Checked == true)
            {
                btnSolved.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnSolved.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        protected void chk2_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            if (chk.Checked == true)
            {
                btnSolved.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnSolved.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void LoadAnalisaResiko()
        {
            Users users = (Users)Session["Users"];
            AnaLisaResiko1 dt = new AnaLisaResiko1();
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select * from  ( " +
                             " select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Ket,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueEkternal1," +
                             " A.Peluang1,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'High' when A.LvlResiko >=6  then 'Medium' when A.LvlResiko >=1  then 'Low' end LvlResiko1, " +
                             " A.Treatment1,case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' when  DAY(A.DueDate1) between 15 and 21  then 'M3' else 'M4' end DueDate, " +
                             " Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv,case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager' when A.Apv='2' then 'Corp.Plant Mgr/Corp.Mgr' else 'Corp.ISO Manager' end Approval,Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus, case when MONTH(A.DueDate1) between 1 and 6 then 'Semester I' else 'Semester II' end Periode from ISO_AnalisaTrans A " +
                             " inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID " +
                             " inner join ISO_DeptRisk C on A.DeptID=C.ID  ) as data1 where Periode='" + ddlSemester.SelectedItem + "' and data1.Tahun=" + ddlTahun.SelectedValue + " and data1.DeptID= " + Convert.ToInt32(ddlDeptName.SelectedValue) + " and data1.RowStatus>-1 order by data1.ID asc ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AnaLisaResiko1
                    {
                        //OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Dept = sdr["Dept"].ToString(),
                        AnalisaResiko = sdr["AnalisaResiko"].ToString(),
                        Ket = sdr["Ket"].ToString(),
                        Risk = sdr["Risk"].ToString(),
                        Aktivitas = sdr["Aktivitas"].ToString(),
                        IssueInternal1 = sdr["IssueInternal1"].ToString(),
                        //IssueInternal2 = sdr["IssueInternal2"].ToString(),
                        IssueEkternal1 = sdr["IssueEkternal1"].ToString(),
                        //IssueEkternal2 = sdr["IssueEkternal2"].ToString(),
                        Peluang1 = sdr["Peluang1"].ToString(),
                        //Peluang2 = sdr["Peluang2"].ToString(),
                        LvlKemungkinan = Convert.ToInt32(sdr["LvlKemungkinan"].ToString()),
                        LvlDampak = Convert.ToInt32(sdr["LvlDampak"].ToString()),
                        LvlResiko = Convert.ToInt32(sdr["LvlResiko"].ToString()),
                        LvlResiko1 = sdr["LvlResiko1"].ToString(),
                        Treatment1 = sdr["Treatment1"].ToString(),
                        //Treatment2 = sdr["Treatment2"].ToString(),
                        DueDate = sdr["DueDate"].ToString(),
                        Bulan = sdr["Bulan"].ToString(),
                        Tahun = sdr["Tahun"].ToString(),
                        Approval = sdr["Approval"].ToString(),
                        //FileName = sdr["FileName"].ToString(),
                        StatusX = sdr["StatusX"].ToString(),
                    });
                }
            }
            lstPMX.DataSource = arrData;
            lstPMX.DataBind();
            Repeater1.DataSource = arrData;
            Repeater1.DataBind();
            departemen.Text = ddlDeptName.SelectedItem.ToString();
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {

        }

        protected void chkIT_CheckedChange(object sender, EventArgs e)
        {

        }
    }
}

public class AnaLisaResiko1 : GRCBaseDomain
{
    public DateTime TglAnalisaTrans { get; set; }
    public int DeptID { get; set; }
    public string Dept { get; set; }
    public string AnalisaResiko { get; set; }
    public int AnalisaClassID { get; set; }
    public string Aktivitas { get; set; }
    public string Risk { get; set; }
    public string IssueInternal1 { get; set; }
    public string IssueInternal2 { get; set; }
    public string IssueEkternal1 { get; set; }
    public string IssueEkternal2 { get; set; }
    public string Peluang1 { get; set; }
    public string Peluang2 { get; set; }
    public int LvlKemungkinan { get; set; }
    public int LvlDampak { get; set; }
    public int LvlResiko { get; set; }
    public string LvlResiko1 { get; set; }
    public string Treatment1 { get; set; }
    public string Treatment2 { get; set; }
    public DateTime DueDate1 { get; set; }
    public DateTime DueDate2 { get; set; }
    public int Apv { get; set; }
    public int Approval1 { get; set; }
    public string Approval { get; set; }
    public int Status { get; set; }
    public string DueDate { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string FileName { get; set; }
    public int SarmutransID { get; set; }
    public string DocName { get; set; }
    public string StatusX { get; set; }
    public string Ket { get; set; }
}