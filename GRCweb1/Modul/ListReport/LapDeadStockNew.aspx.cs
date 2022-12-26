using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapDeadStockNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadDept(true);

                Users users = (Users)Session["Users"];
                string UnitKerja1 = users.UnitKerjaID.ToString();
                Session["UnitKerja1"] = UnitKerja1;
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnSent);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 296, 99 , 50 ,false); </script>", false);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanDeathStock.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Departement :" + ddlDept.SelectedItem.Text;
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("class=\"kotak tengah xx\">", "class=\"kotak tengah\">'");
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void lstDS_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string periode0 = string.Empty;
            if (ddlBulan.SelectedValue.ToString().PadLeft(2, '0') == "01")
                periode0 = (Convert.ToInt32(ddlTahun.SelectedValue.ToString()) - 1).ToString() + "12";
            else
                periode0 = ddlTahun.SelectedValue.ToString() + (Convert.ToInt32(ddlBulan.SelectedValue.ToString().PadLeft(2, '0')) - 1).ToString().PadLeft(2, '0');

            string periode = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            DS ds0 = (DS)e.Item.DataItem;
            DS ds = (DS)e.Item.DataItem;
            Repeater lst = (Repeater)e.Item.FindControl("lstDetail");
            DeadStocked dst0 = new DeadStocked();
            dst0.Periode = periode0;
            switch (rbNS.SelectedIndex)
            {
                case 0:
                    dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() + ")attall ";
                    break;
                case 1://waring stock
                       //dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + 
                       //    periode0 + "01') + 1, 0))) )>3 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0 
                       //    + "01') + 1, 0))) ) >3))attall  where ltimecrnt>=3  ";
                    dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() +
                        " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0 + "01') + 1, 0))) )>3 and " +
                        " DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0 +
                        "01') + 1, 0))) ) <24))attall  where ltimecrnt>=3 and ltimecrnt<24  ";
                    break;
                case 2:
                    //dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + 
                    //periode0 + "01') + 1, 0))) )<=3 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0 + 
                    //"01') + 1, 0))) ) <=3))attall  ";
                    dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" +
                        periode0 + "01') + 1, 0))) )>24 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0 +
                        "01') + 1, 0))) ) >24))attall  where ltimecrnt>=24  ";
                    break;
                case 3:
                    dst0.Criteria = " WHERE DeptID=" + ds0.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" +
                periode + "01') + 1, 0))) )<=3 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode0
                + "01') + 1, 0))) ) <=3))attall  ";
                    break;
            }
            dst0.OrderBy = " Order by ItemName,ItemCode";
            dst0.Pilihan = "Detail";
            lst.DataSource = dst0.Retrieve();
            int jml0 = dst0.arrData.Count;

            DeadStocked dst = new DeadStocked();
            dst.Periode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0');
            switch (rbNS.SelectedValue)
            {
                case "0":
                    dst.Criteria = " WHERE DeptID=" + ds.ID.ToString() + ")attall ";
                    break;
                case "1"://deadstock
                    dst.Criteria = " WHERE DeptID=" + ds.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" +
                        periode + "01') + 1, 0))) )>24 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode +
                        "01') + 1, 0))) ) >24))attall  where ltimecrnt>=24  ";
                    break;
                case "2":
                    dst.Criteria = " WHERE DeptID=" + ds.ID.ToString() + " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" +
              periode + "01') + 1, 0))) )<=3 OR DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode
              + "01') + 1, 0))) ) <=3))attall  ";
                    break;
                case "3"://waring stock
                    dst.Criteria = " WHERE DeptID=" + ds.ID.ToString() +
                        " AND  (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode + "01') + 1, 0))) )>3 and " +
                        " DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + periode +
                        "01') + 1, 0))) ) <24))attall  where ltimecrnt>=3 and ltimecrnt<24  ";
                    break;
            }
            dst.OrderBy = " Order by ItemName,ItemCode";
            dst.Pilihan = "Detail";
            lst.DataSource = dst.Retrieve();
            int jml = dst.arrData.Count;
            if (jml - jml0 > 0)
            {
                LTambah.Text = (jml - jml0).ToString();
                LKurang.Text = "0";
            }
            else
            {
                LKurang.Text = ((jml - jml0) * -1).ToString();
                LTambah.Text = "0";
            }
            lst.DataBind();

        }
        protected void lstDetail_Databound(object sender, RepeaterItemEventArgs e)
        {
            DS ds = (DS)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            if (ds.Umur > 3 && rbNS.Items[0].Selected == true)
            {
                tr.Attributes.Add("style", "color:blue");
            }
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            DeadStocked ds = new DeadStocked();
            ds.Pilihan = "Tahun";
            ddlTahun.Items.Clear();
            foreach (DS d in ds.GetTahun())
            {
                ddlTahun.Items.Add(new ListItem(d.RowStatus.ToString(), d.RowStatus.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadDept()
        {
            ArrayList arrDept = new ArrayList();
            string[] dpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "DeathStock").Split(',');
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--All--", "0"));
            DeptFacade dept = new DeptFacade();
            arrDept = dept.Retrieve();
            foreach (Dept dp in arrDept)
            {
                if (dpt.Contains(dp.ID.ToString()))
                {
                    ddlDept.Items.Add(new ListItem(dp.DeptName, dp.ID.ToString()));
                }
                if (dpt.Contains("All"))
                {
                    ddlDept.Items.Add(new ListItem(dp.DeptName, dp.ID.ToString()));
                }
            }

        }
        private void LoadDept(bool trans)
        {
            ArrayList arrData = new ArrayList();
            DeadStocked ds = new DeadStocked();
            ds.Periode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0');
            arrData = ds.Retrieve(true);
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--ALL Dept--", "0"));
            foreach (DS d in arrData)
            {
                ddlDept.Items.Add(new ListItem(d.DeptName.ToString(), d.ID.ToString()));
            }

        }
        private void LoadList()
        {
            ArrayList arrData = new ArrayList();
            DeadStocked ds = new DeadStocked();
            ds.Periode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0');
            ds.Criteria = (ddlDept.SelectedIndex == 0) ? "" : " WHERE DeptID=" + ddlDept.SelectedValue.ToString();
            arrData = ds.Retrieve(true);
            lstDS.DataSource = arrData;
            lstDS.DataBind();
            ds.deletetable();
        }

        protected void btnSent_ServerClick(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("class=\"kotak tengah xx\">", "class=\"kotak tengah\">'");
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");

            string UnitKerja = Session["UnitKerja1"].ToString();
            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            string LapID = lst.ToString();

            //Pengirim   
            if (UnitKerja == "1")
            {
                mail.From = new MailAddress("log.bb.admin_ctrp@grcboard.com");
            }
            else if (UnitKerja == "7")
            {
                mail.From = new MailAddress("log.bb.admin_krwg@grcboard.com");
            }

            //Tujuan
            //mail.To.Add(new MailAddress("beny@grcboard.com", "Test"));
            if (UnitKerja == "1")
            {
                if (Convert.ToInt32(ddlDept.SelectedValue) == 19 || Convert.ToInt32(ddlDept.SelectedValue) == 5 || Convert.ToInt32(ddlDept.SelectedValue) == 4 || Convert.ToInt32(ddlDept.SelectedValue) == 18) //MTN
                {
                    mail.To.Add(new MailAddress("rofiq.susanto@grcboard.com", "rofik"));
                    mail.To.Add(new MailAddress("mtn.admin_ctrp@grcboard.com", "Citra"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 7) //HRD&GA
                {
                    mail.To.Add(new MailAddress("agung.hendro@grcboard.com", "Agung Hendro"));
                    mail.To.Add(new MailAddress("hrd.admin_ctrp@grcboard.com", "Karnel"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 26) //Armada
                {
                    mail.To.Add(new MailAddress("devian@grcboard.com", "Devian"));
                    mail.To.Add(new MailAddress("armada_ctrp@grcboard.com", "Admin Armada"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 10) //LogBB
                {
                    mail.To.Add(new MailAddress("fajar@grcboard.com ", "Fajar"));
                    mail.To.Add(new MailAddress("log.bb.admin_ctrp@grcboard.com", "Riki"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 6) //LogPJ
                {
                    mail.To.Add(new MailAddress("fajar@grcboard.com ", "Fajar"));
                    mail.To.Add(new MailAddress("log.bj.admin_ctrp@grcboard.com", "Sari"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 3) //Finishing
                {
                    mail.To.Add(new MailAddress("bachrodin@grcboard.com ", "Bachrodin"));
                    mail.To.Add(new MailAddress("finishing.admin_ctrp@grcboard.com", "Dita"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 2) //BM
                {
                    mail.To.Add(new MailAddress("bachrodin@grcboard.com", "Bachrodin"));
                    mail.To.Add(new MailAddress("bm.admin_ctrp@grcboard.com", "Nuh"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 16 || Convert.ToInt32(ddlDept.SelectedValue) == 21 || Convert.ToInt32(ddlDept.SelectedValue) == 22) //Project
                {
                    mail.To.Add(new MailAddress("sipil.project_krwg@grcboard.com", ""));
                    mail.To.Add(new MailAddress("log.bb_ctrp@grcboard.com", "Ervan"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 9) //QA
                {
                    mail.To.Add(new MailAddress("bahrul.ulum@grcboard.com", "Bahrul"));
                    mail.To.Add(new MailAddress("qa.admin_ctrp@grcboard.com", "Ahmad"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 30) //R&D
                {
                    mail.To.Add(new MailAddress("rnd.section@grcboard.com", "R&D"));
                    mail.To.Add(new MailAddress("log.bb_krwg@grcboard.com", "Ervan"));
                }
            }

            //edit tanggal 08-04- 2021, edit penambahan penerima dan cc oleh IT Karawang agus
            else if (UnitKerja == "7")
            {
                if (Convert.ToInt32(ddlDept.SelectedValue) == 19 || Convert.ToInt32(ddlDept.SelectedValue) == 5 || Convert.ToInt32(ddlDept.SelectedValue) == 4 || Convert.ToInt32(ddlDept.SelectedValue) == 18) //MTN
                {
                    //to mekanik elektrik
                    mail.To.Add(new MailAddress("zhakim@grcboard.com", "Zaenul Hakim"));
                    mail.To.Add(new MailAddress("mtn.admin_krwg@grcboard.com", "Nur"));
                    mail.To.Add(new MailAddress("elektrik_krwg@grcboard.com", "Gunawan"));
                    mail.To.Add(new MailAddress("zhakim@grcboard.com", "Hakim"));
                    mail.To.Add(new MailAddress("mekanik_krwg@grcboard.com", "Ehwanto"));
                    mail.To.Add(new MailAddress("utility_krwg@grcboard.com", "Doppy"));

                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }

                else if (Convert.ToInt32(ddlDept.SelectedValue) == 7) //HRD&GA
                {
                    //penerima
                    mail.To.Add(new MailAddress("anditaslim@grcboard.com", "Andi Taslim"));
                    mail.To.Add(new MailAddress("ga.admin_krwg@grcboard.com", "Evi"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 26) //Armada
                {
                    mail.To.Add(new MailAddress("devian@grcboard.com", "Devian"));
                    mail.To.Add(new MailAddress("armada_krwg@grcboard.com", "Ryant"));
                    mail.To.Add(new MailAddress("armada_ctrp@grcboard.com", "Sabir"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 10) //LogBB
                {
                    //penerima to
                    mail.To.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi"));
                    mail.To.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Nia"));
                    mail.To.Add(new MailAddress("log.bb_krwg@grcboard.com", "Wahyudi"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 6) //LogPJ
                {
                    //penerima to
                    mail.To.Add(new MailAddress("log.bj_krwg@grcboard.com", "Chabib"));
                    mail.To.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi"));
                    mail.To.Add(new MailAddress("log.bj.admin_krwg@grcboard.com", "Fidi"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));

                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 3) //Finishing
                {
                    //penerima to
                    mail.To.Add(new MailAddress("septiawan.dwijaya@grcboard.com", "Septiawan"));
                    mail.To.Add(new MailAddress("finishing.admin_krwg@grcboard.com", "Syafira"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 2) //BM
                {
                    mail.To.Add(new MailAddress("linda@grcboard.com", "Linda"));
                    mail.To.Add(new MailAddress("bm.admin_krwg@grcboard.com", "Yusuf"));

                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 16 || Convert.ToInt32(ddlDept.SelectedValue) == 21 || Convert.ToInt32(ddlDept.SelectedValue) == 22) //Project
                {
                    //mail.To.Add(new MailAddress("sipil.project_krwg@grcboard.com", "Andika"));
                    //mail.To.Add(new MailAddress("log.bb_krwg@grcboard.com", "Tedi"));

                    mail.To.Add(new MailAddress("zhakim@grcboard.com", "Hakim"));
                    mail.To.Add(new MailAddress("me.project_krwg@grcboard.com", "Yarman"));
                    mail.To.Add(new MailAddress("kasie_rnd_me_corp@grcboard.com", "Asep"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));

                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 9) //QA
                {
                    //penerima to
                    mail.To.Add(new MailAddress("cucun.wahyudi@grcboard.com", "Cucun"));
                    mail.To.Add(new MailAddress("qa.admin@yahoo.com", "Rossi"));

                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 30) //R&D
                {
                    //mail.To.Add(new MailAddress("rnd.section@grcboard.com", "R&D"));
                    //mail.To.Add(new MailAddress("log.bb_krwg@grcboard.com", "Tedi"));

                    //penerima to
                    mail.To.Add(new MailAddress("zhakim@grcboard.com", "Hakim"));
                    mail.To.Add(new MailAddress("me.project_krwg@grcboard.com", "Yarman"));
                    mail.To.Add(new MailAddress("kasie_rnd_me_corp@grcboard.com", "Asep"));


                    //penerima cc
                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));

                }
                else if (Convert.ToInt32(ddlDept.SelectedValue) == 14)//DeptIT
                {
                    mail.To.Add(new MailAddress("sodikin@grcboard.com", "Sodikin"));
                    mail.To.Add(new MailAddress("agus.it@grcboard.com", "Agus"));

                    mail.CC.Add(new MailAddress("zuhri@grcboard.com", "Zuhri"));
                    mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
                    mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Wahyudi"));
                    mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi.S"));
                }
            }
            //edit tanggal 08-04- 2021, edit penambahan penerima dan cc oleh IT Karawang agus

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            mail.Subject = "Reminder Barang Death Stock";
            mail.Body += "Dear All User, <br/><br/>";
            mail.Body += "Mohon bantuan dan kerjasamanya untuk barang Deathstock dan warning deathstock tersebut segera di SPB dan <br/>";
            mail.Body += "apabila masih Belum di lakukan SPB mohon di buatkan TPP. <br/>";
            mail.Body += " " + Contents + " <br/>";
            mail.IsBodyHtml = true;
            mail.Body += "Terimakasih, " + "<br/><br/><br/>";
            mail.Body += "Salam GRCBOARD " + "<br/><br/>";
            mail.Body += "Regard's, " + "<br/>";
            mail.Body += "Admin SP <br/>";
            //mail.Body = mail.Body.Replace(Environment.NewLine, "<br/>");

            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            if (UnitKerja == "1")
            {
                NetworkCred.UserName = "log.bb.admin_ctrp@grcboard.com";
                NetworkCred.Password = "grc123!@#";
            }
            else if (UnitKerja == "7")
            {
                NetworkCred.UserName = "log.bb.admin_krwg@grcboard.com";
                NetworkCred.Password = "DeptLogbb!@#";
            }

            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

    }

    public class DeadStocked
    {
        private DS ds = new DS();
        public ArrayList arrData = new ArrayList();
        public string Criteria { get; set; }
        public string Prefix { get; set; }
        public string Pilihan { get; set; }
        public string Field { get; set; }
        public string OrderBy { get; set; }
        public string Periode { get; set; }
        private string Query()
        {
            #region proses baca saldo awal
            string saldoQty = string.Empty;
            int Tahun = int.Parse(Periode.Substring(0, 4));
            switch (this.Periode.Substring(4, 2))
            {
                case "01": saldoQty = "desQty"; Tahun = (Tahun - 1); break;
                case "02": saldoQty = "janQty"; break;
                case "03": saldoQty = "febQty"; break;
                case "04": saldoQty = "marQty"; break;
                case "05": saldoQty = "aprQty"; break;
                case "06": saldoQty = "meiQty"; break;
                case "07": saldoQty = "junQty"; break;
                case "08": saldoQty = "julQty"; break;
                case "09": saldoQty = "aguQty"; break;
                case "10": saldoQty = "sepQty"; break;
                case "11": saldoQty = "oktQty"; break;
                case "12": saldoQty = "novQty"; break;
            }
            #endregion
            #region query lama
            string query = this.Prefix + "select  Uomdesc as satuan,ID,GroupID,ItemCode,itemname,isnull(badstock,0) as flag,lastreceipt,lastPakai,Jumlah as stock, " +
                           "DATEDIFF(MONTH,startcount,'" + this.Periode + "')/*/Convert(decimal(9,4),(Select DATEPART(D,(SELECT dbo.GetLastDay('" + this.Periode + "')))))*/ as ltimecrnt, " +
                           " (select top 1 UPPER((select UserName from Users where ID=HeadID)) from SPP where SPP.ID in( " +
                           " Select SPPID from ReceiptDetail where ReceiptID in(select receipt.ID from receipt where ReceiptDate=lreceipt)  " +
                           " and ItemID=mm.ID))Head, " +
                           "(select top 1 UPPER((select DeptID from Users where ID=HeadID)) from SPP where SPP.ID in( " +
                           " Select SPPID from ReceiptDetail where ReceiptID in(select receipt.ID from receipt where ReceiptDate=lreceipt)  " +
                           " and ItemID=mm.ID))DeptID " +
                           " from (select Uomdesc,ID,GroupID,ItemCode,itemname,badstock,lastreceipt,lastPakai,Jumlah,  " +
                           " case when isnull(M.lastPakai,'1/1/1900')= '1/1/1900' then lastreceipt else " +
                           " case when (lastreceipt>M.lastPakai )then lastreceipt else M.lastPakai end end startcount, " +
                           " lastreceipt as lreceipt  from (select Uomdesc,I.ID,GroupID,ItemCode,itemname,badstock, " +
                           " case when I.ID>0 then  " +
                           "     (select top 1 A.ReceiptDate from Receipt A, ReceiptDetail B where A.ID=B.ReceiptID and B.ItemID=I.ID  " +
                           "     and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastreceipt, " +
                           " case when I.ID>0 then  " +
                           "     (select top 1 A.PakaiDate from Pakai A, PakaiDetail B where A.ID=B.PakaiID and B.ItemID=I.ID  " +
                           "     and B.GroupID=I.GroupID and B.RowStatus>-1 order by A.PakaiDate desc ) end lastPakai,Jumlah    " +
                           " from Inventory I,UOM U where I.uomID=U.ID and  I.stock=0 and i.aktif=1 and I.Jumlah>0) as M ) as MM  " +
                           " where MM.startcount<DATEADD(month,(case when MM.BadStock>0 then (MM.BadStock*-1) else -3 end),'" + this.Periode + "')  " +
                           this.Criteria +
                           this.OrderBy;

            string strquery = "WITH NonStock AS( " +
                  "  SELECT ID,ItemCode,ItemName,UOMID,Jumlah,JmlTransit,ItemTypeID,GroupID,MinStock,MaxStock,Stock  " +
                  "  FROM inventory where Stock=0 and aktif=1  " +
                  "  and GroupID in(6,8,9) " +
                  "  ), " +
                  "  SaldoAwal AS( " +
                  "      SELECT ItemID,ItemTypeID," + saldoQty + " as Qty FROM SaldoInventory WHERE YearPeriod=" + Tahun.ToString() +
                  "      AND ItemiD IN(SELECT ID FROM NonStock) AND ItemTypeID in(SELECT ItemTypeID FROM NonStock) " +
                  "  ) " +
                  "  ,StockPurchn AS ( " +
                  "      SELECT ItemID,ItemTypeID,SUM(quantity)Qty " +
                  "      FROM  vw_StockPurchn WHERE YM=" + this.Periode + " AND  ItemiD IN(SELECT ID FROM NonStock) AND ItemTypeID in(SELECT ItemTypeID FROM NonStock) " +
                  "      GROUP By ItemID,ItemTypeID " +
                  "  ) " +
                  "  ,Lastrans AS( " +
                  "      SELECT ItemID,Max(Tanggal)Tgl,ID,process,ItemTypeID FROM vw_StockPurchn WHERE YM <=" + this.Periode + " AND  ItemiD IN(SELECT ID FROM NonStock)  " +
                  "      AND ItemTypeID in(SELECT ItemTypeID FROM NonStock) " +
                  "      GROUP BY ItemID,ID,ItemTypeID,process " +
                  "  ) " +
                  "  ,SaldoAkhir AS ( " +
                  "  SELECT ItemID,ItemTypeID,SUM(Qty)Qty FROM ( " +
                  "      SELECT * FROM SaldoAwal  " +
                  "      UNION  " +
                  "      SELECT * FROM StockPurchn " +
                  "  ) AS x " +
                  "  WHERE Qty>0 " +
                  "  GROUP By ItemID,ItemTypeID " +

                  "  ) " +
                  "  ,DataJadiDS AS ( " +
                  "  SELECT x.*,s.Qty SaldoAkhir,p.SppDetailID,p.SPPID,sp.Minta,sp.UserID,sp.HeadID  " +
                  "  FROM ( " +
                  "          SELECT MAX(rd.ID)ID,ItemID,rd.ItemTypeID,Quantity,AVG(AvgPrice)Price,MAX(PODetailID)PO,ReceiptDate " +
                  "          FROM ReceiptDetail rd " +
                  "          LEFT JOIN Receipt r ON r.ID=rd.ReceiptID " +
                  "          Where ItemID in(SELECT ItemID FROM SaldoAkhir) and rd.ItemTypeID=1 " +
                  "          Group By ItemID,Quantity,ReceiptDate,rd.ItemTypeID " +
                  "  ) AS X " +
                  "  LEFt JOIN SaldoAkhir as s on S.ItemID=x.ItemID " +
                  "  LEFT JOIN POPurchnDetail as P on p.ID=x.PO and p.ItemID=x.ItemID " +
                  "  LEFt JOIN SPP sp ON sp.ID=p.SPPID " +
                  "  WHERE CONVERT(CHAR,ReceiptDate,112)<='" + this.Periode + "' " +
                  "  ) " +
                  "  ,YngPrivate AS ( " +
                  "  SElECT GudangID DeptID,SPPID PrivateSPP,ItemID,(SUM(QtyReceipt)-SUM(QtyPakai))Saldo,CreatedTime " +
                  "  FROM SPPMultiGudang m where ItemiD in(SELECT ItemID FROM DataJadiDS) and aktif=1 " +
                  "  /*and DATEDIFF(MONTH,m.CreatedTime,GETDATE())>3 */" +
                  "  GROUP By GudangID,SPPID,ItemID,CreatedTime " +
                  "  ), " +
                  "  Lastrans2 AS ( " +
                  "  SELECT ROW_NUMBER() OVER(PARTITION BY d.ItemID ORDER BY ItemID,Minta Desc)n, d.*,u.DeptID " +
                  "  FROM DataJadiDS d " +
                  "  LEFT JOIN Users u On u.ID=d.UserID " +
                  "  ),Lastran3 AS( " +
                  "  Select d.ReceiptDate,d.ItemID,(SELECT dbo.ItemCodeInv(d.ItemID,d.ItemTypeID))ItemCode, " +
                  "  (SELECT dbo.ItemNameInv(d.ItemID,d.ItemTypeID))ItemName,ItemTypeID, " +
                  "  Quantity RMSQty,SaldoAkhir,Saldo SaldoPrivate,CASE WHEN d.DeptID=0 or yp.DeptID=0 or yp.DeptID is null " +
                  "  THEN 10 ELSE ISNULL(yp.DeptID,d.DeptID)END DeptID,CreatedTime " +
                  "  FROM Lastrans2 d " +
                  "  LEFT JOIN YngPrivate yp on yp.ItemID=d.ItemID and yp.PrivateSPP=d.SPPID " +
                  "  where n=1 ), " +
                  "  Lastran4 AS ( " +
                  "    Select l3.*, " +
                  "  (SELECT MAX(L2.Tgl) FROM Lastrans l2 WHERE l2.ItemID=l3.ItemID  GROUP BY ItemID,ItemTypeID)TglLastTrans, " +
                  "  (SELECT Top 1 l2.process FROM Lastrans l2 WHERE l2.ItemID=l3.ItemID   order by l2.ID desc)Process from Lastran3 l3)";
            #endregion
            string strquery1 = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NonStock]') AND type in (N'U')) DROP TABLE [dbo].NonStock   " +
                  "SELECT ID,ItemCode,ItemName,UOMID,Jumlah,JmlTransit,ItemTypeID,GroupID,MinStock,MaxStock,Stock  into NonStock   FROM inventory where Stock=0 and aktif=1  and GroupID in(6,8,9)      " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaldoAwal]') AND type in (N'U')) DROP TABLE [dbo].SaldoAwal  " +
                  "SELECT ItemID,S.ItemTypeID," + saldoQty + " as Qty into SaldoAwal FROM SaldoInventory S inner join NonStock ns on S.ItemID=ns.ID and S.ItemTypeID=ns.ItemTypeID  " +
                  "WHERE YearPeriod=" + Tahun.ToString() + "       " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockPurchn]') AND type in (N'U')) DROP TABLE [dbo].StockPurchn " +
                  "SELECT ItemID,ItemTypeID,SUM(quantity)Qty into StockPurchn FROM  vw_StockPurchn WHERE YM='" + this.Periode + "' AND  ItemiD IN(SELECT ID FROM NonStock) AND ItemTypeID  " +
                  "in(SELECT ItemTypeID FROM NonStock)       GROUP By ItemID,ItemTypeID  " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastrans]') AND type in (N'U')) DROP TABLE [dbo].Lastrans " +
                  "SELECT ItemID,Max(Tanggal)Tgl,S.ID,process,S.ItemTypeID into Lastrans FROM vw_StockPurchn  S inner join NonStock ns on S.ItemID=ns.ID and S.ItemTypeID=ns.ItemTypeID  " +
                  " WHERE YM <=" + this.Periode + " GROUP BY ItemID,S.ID,S.ItemTypeID,process   " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaldoAkhir]') AND type in (N'U')) DROP TABLE [dbo].SaldoAkhir " +
                  "SELECT ItemID,ItemTypeID,SUM(Qty)Qty into SaldoAkhir FROM (SELECT * FROM SaldoAwal UNION ALL SELECT * FROM StockPurchn   ) AS x   WHERE Qty>0    " +
                  "GROUP By ItemID,ItemTypeID " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataJadiDS]') AND type in (N'U')) DROP TABLE [dbo].DataJadiDS " +
                  "SELECT x.*,s.Qty SaldoAkhir,p.SppDetailID,p.SPPID,sp.Minta,sp.UserID,sp.HeadID into DataJadiDS   FROM (            " +
                  "SELECT MAX(rd.ID)ID,ItemID,rd.ItemTypeID,Quantity,AVG(AvgPrice)Price,MAX(PODetailID)PO,ReceiptDate FROM ReceiptDetail rd            " +
                  "LEFT JOIN Receipt r ON r.ID=rd.ReceiptID Where ItemID in(SELECT ItemID FROM SaldoAkhir) and rd.ItemTypeID=1            " +
                  "Group By ItemID,Quantity,ReceiptDate,rd.ItemTypeID   ) AS X   LEFt JOIN SaldoAkhir as s on S.ItemID=x.ItemID   LEFT JOIN POPurchnDetail as P on p.ID=x.PO and p.ItemID=x.ItemID    " +
                  "LEFt JOIN SPP sp ON sp.ID=p.SPPID   WHERE CONVERT(CHAR,ReceiptDate,112)<='" + this.Periode + "'    " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YngPrivate]') AND type in (N'U')) DROP TABLE [dbo].YngPrivate " +
                  "SElECT GudangID DeptID,SPPID PrivateSPP,ItemID,(SUM(QtyReceipt)-SUM(QtyPakai))Saldo,CreatedTime into YngPrivate  FROM SPPMultiGudang m where ItemiD  " +
                  "in(SELECT ItemID FROM DataJadiDS) " +//and aktif=1    
                  "GROUP By GudangID,SPPID,ItemID,CreatedTime    " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastrans2]') AND type in (N'U')) DROP TABLE [dbo].Lastrans2 " +
                  "SELECT ROW_NUMBER() OVER(PARTITION BY d.ItemID ORDER BY ItemID,Minta Desc)n, d.*,u.DeptID into Lastrans2  FROM DataJadiDS d   LEFT JOIN Users u On u.ID=d.UserID " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastran3]') AND type in (N'U')) DROP TABLE [dbo].Lastran3 " +
                  "Select d.ReceiptDate,d.ItemID,(SELECT dbo.ItemCodeInv(d.ItemID,d.ItemTypeID))ItemCode,   (SELECT dbo.ItemNameInv(d.ItemID,d.ItemTypeID))ItemName,ItemTypeID,    " +
                  "Quantity RMSQty,SaldoAkhir,Saldo SaldoPrivate,CASE WHEN d.DeptID=0 or yp.DeptID=0 or yp.DeptID is null   THEN 10 ELSE ISNULL(yp.DeptID,d.DeptID)END DeptID,CreatedTime    " +
                  "into Lastran3 FROM Lastrans2 d   LEFT JOIN YngPrivate yp on yp.ItemID=d.ItemID and yp.PrivateSPP=d.SPPID   where n=1     " +
                  " " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastran4]') AND type in (N'U')) DROP TABLE [dbo].Lastran4 " +
                  "Select l3.*,   (SELECT MAX(L2.Tgl) FROM Lastrans l2 WHERE l2.ItemID=l3.ItemID  GROUP BY ItemID,ItemTypeID)TglLastTrans,    " +
                  "(SELECT Top 1 l2.process FROM Lastrans l2 WHERE l2.ItemID=l3.ItemID   order by l2.ID desc)Process into Lastran4 from Lastran3 l3 " +
                  " ";
            return strquery1;
        }
        public string DeptQuery()
        {
            string strSQL = this.Query();
            strSQL += "SELECT DeptID,DeptName,DeptCode FROM Lastran4 l " +
                    "LEFT JOIN Dept d On d.ID=l.DeptID " + this.Criteria +
                    "GROUP By DeptID,DeptName,DeptCode " +
                    "ORDER BY d.DeptName IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataJadiDS]') AND type in (N'U')) DROP TABLE [dbo].DataJadiDS";

            return strSQL;
        }
        public string DataNonstock()
        {
            string strSQL = this.Query();
            strSQL +=
                  //"select * from (SELECT DeptID,ItemCode,ItemName,(Select dbo.SatuanInv(ItemID,ItemTypeID))Satuan,ItemID,SaldoAkhir Stock,ReceiptDate lastreceipt, " +
                  //      "  CASE WHEN ReceiptDate < TglLastTrans THEN TglLastTrans ELSE CASE WHEN RTRIM(Process)!='ReceiptDetail' THEN TglLastTrans  END END LastPakai,TglLastTrans, " +
                  //      "  CASE WHEN ReceiptDate > TglLastTrans THEN (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + this.Periode + "01') + 1, 0))) )) ELSE (DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + this.Periode + "01') + 1, 0))) )) END ltimecrnt,  " +
                  //      "  Process " +
                  //      "  FROM Lastran4 " +
                  "select * from (SELECT DeptID,ItemCode,ItemName,(Select dbo.SatuanInv(ItemID,ItemTypeID))Satuan,ItemID,SaldoAkhir Stock,ReceiptDate lastreceipt,    " +
                  "CASE WHEN ReceiptDate < TglLastTrans THEN TglLastTrans ELSE CASE WHEN RTRIM(Process)!='ReceiptDetail' THEN TglLastTrans  END END LastPakai,TglLastTrans,    " +
                  "CASE WHEN ReceiptDate > TglLastTrans  " +
                  "THEN (DATEDIFF(MONTH,ReceiptDate,(DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, '" + this.Periode + "01') + 1, 0))) )) ELSE (DATEDIFF(MONTH,TglLastTrans,(DATEADD(d, -1,  " +
                  "DATEADD(m, DATEDIFF(m, 0, '" + this.Periode + "01') + 1, 0))) )) END ltimecrnt,    Process   FROM Lastran4  " +
                  " " +
                  this.Criteria + this.OrderBy + " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataJadiDS]') AND type in (N'U')) DROP TABLE [dbo].DataJadiDS";



            return strSQL;
        }

        public void deletetable()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NonStock]') AND type in (N'U')) DROP TABLE [dbo].NonStock   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaldoAwal]') AND type in (N'U')) DROP TABLE [dbo].SaldoAwal  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockPurchn]') AND type in (N'U')) DROP TABLE [dbo].StockPurchn " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastrans]') AND type in (N'U')) DROP TABLE [dbo].Lastrans " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaldoAkhir]') AND type in (N'U')) DROP TABLE [dbo].SaldoAkhir " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataJadiDS]') AND type in (N'U')) DROP TABLE [dbo].DataJadiDS " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YngPrivate]') AND type in (N'U')) DROP TABLE [dbo].YngPrivate " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastrans2]') AND type in (N'U')) DROP TABLE [dbo].Lastrans2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastran3]') AND type in (N'U')) DROP TABLE [dbo].Lastran3 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lastran4]') AND type in (N'U')) DROP TABLE [dbo].Lastran4 ";
            //SqlDataReader sdr = da.RetrieveDataByString(strSQL);

        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = this.DataNonstock();
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(this.GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList Retrieve(bool Dept)
        {
            arrData = new ArrayList();
            string strQuery = this.DeptQuery();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strQuery);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(this.GenerateObject(sdr, Dept));
                }
            }
            return arrData;
        }
        public ArrayList GetTahun()
        {
            arrData = new ArrayList();
            string strSQL = "select Top 5 * from ( " +
                            "Select distinct Year(ReceiptDate)Tahun from Receipt " +
                            ") as x where x.Tahun is not null " +
                            " order by Tahun desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }

            return arrData;
        }
        private DS GenerateObject(SqlDataReader sdr)
        {
            ds = new DS();
            switch (this.Pilihan)
            {
                case "Dept":
                    ds.DeptName = sdr["DeptName"].ToString();
                    ds.CreatedBy = sdr["HeadName"].ToString();
                    ds.ID = Convert.ToInt32(sdr["DeptID"].ToString());
                    break;
                case "Tahun":
                    ds.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Detail":
                    ds.ItemCode = sdr["ItemCode"].ToString();
                    ds.ItemName = sdr["ItemName"].ToString();
                    ds.Satuan = sdr["Satuan"].ToString();
                    ds.ReceiptDate = (sdr["lastreceipt"] == DBNull.Value) ? "" : sdr["lastreceipt"].ToString().Substring(0, 10);
                    ds.PakaiDate = (sdr["lastPakai"] == DBNull.Value) ? "" : sdr["lastPakai"].ToString().Substring(0, 10);
                    ds.Umur = Convert.ToDecimal(sdr["ltimecrnt"].ToString());
                    ds.Stock = Convert.ToDecimal(sdr["stock"].ToString());
                    break;
            }
            return ds;
        }
        private DS GenerateObject(SqlDataReader sdr, bool Dept)
        {
            ds = new DS();
            ds.DeptName = sdr["DeptName"].ToString();
            //ds.CreatedBy = sdr["HeadName"].ToString();
            ds.ID = Convert.ToInt32(sdr["DeptID"].ToString());
            ds.DeptCode = sdr["DeptCode"].ToString();
            return ds;
        }
    }

    public class DS : GRCBaseDomain
    {
        public string Satuan { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ReceiptDate { get; set; }
        public string PakaiDate { get; set; }
        public decimal Umur { get; set; }
        public decimal Stock { get; set; }
        public int BadStock { get; set; }
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
    }

}