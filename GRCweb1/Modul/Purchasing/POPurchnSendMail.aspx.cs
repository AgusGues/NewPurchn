using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
namespace GRCweb1.Modul.Purchasing
{
    public partial class POPurchnSendMail : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                btnReport.Disabled = true;
                LoadBulan();
                LoadTahun();
                Bulan = ddlBulan.SelectedItem.Text;
                ViewState["seding"] = false;
                if (Request.QueryString["NoPo"] != null)
                {
                    //NoPo=KI1703-01715
                    LoadPO(Request.QueryString["NoPo"].ToString());
                }
                MailSendPO(true);
                string txtMsg = new Inifiles(Server.MapPath("~/App_Data/POPurchnConfig.ini")).Read("EmailText", "PO");
                txtMessage.Text = (txtMsg == string.Empty) ? txtMessage.Text : txtMsg;
                Panel1.Visible = true;
                peroideArea.Visible = false;
            }

        }
        protected void txtNoPO_Change(object sender, EventArgs e)
        {
            ViewState["seding"] = false;
            LoadPO(txtNoPO.Text);
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            PakaiFacade pk = new PakaiFacade();
            pk.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }



        protected void ReportMailPo_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        private void MailSendPO(bool All)
        {
            Bulan = ddlBulan.SelectedValue;
            POPurchnSendMailFacade psend = new POPurchnSendMailFacade();
            psend.KirimanHariIni = All;
            ArrayList arrData = new ArrayList();
            arrData = psend.RetrieveMailSend(ddlBulan.SelectedValue, ddlTahun.SelectedValue);
            ReportMailPo.DataSource = arrData;
            ReportMailPo.DataBind();
        }



        private void Clear()
        {
            txtNoPO.Text = "";
            txtEmail.Text = "";
            txtCariSupplier.Text = "";
            FileUpload1.Attributes.Clear();
            txtMessage.Text = "";
        }

        protected void btnFormPO_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormPOPurchn.aspx");
        }

        public string SendMail()
        {
            try
            {
                Users usr = (Users)Session["Users"];
                string plant = "ctrp";
                switch (usr.UnitKerjaID) { case 1: plant = "ctrp"; break; default: plant = "krwg"; break; }
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("purchasing_" + plant + "@grcboard.com");
                msg.To.Add(txtEmail.Text);
                msg.Subject = ("Purchase Order" + " - " + "No." + (txtNoPO.Text) + " - " + (txtCariSupplier.Text));
                msg.Body = txtMessage.Text;

                msg.IsBodyHtml = true;
                if (Session["Upload1"] != null)
                {
                    FileUpload1 = (FileUpload)Session["Upload1"];
                }
                if (FileUpload1.HasFile)
                {
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    Attachment myAttachment = new Attachment(FileUpload1.FileContent, fileName);
                    msg.Attachments.Add(myAttachment);
                    msg.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.FileName));
                }

                //attachment ke hapus saat btn kirim send karena postback 
                SmtpClient smt = new SmtpClient();
                smt.Host = "mail.grcboard.com";
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = "purchasing_" + plant + "@grcboard.com";
                ntwd.Password = "grc123!@#";
                smt.UseDefaultCredentials = true;
                smt.Credentials = new System.Net.NetworkCredential(ntwd.UserName, ntwd.Password);
                //smt.Credentials = ntwd;
                smt.Port = 587;
                smt.EnableSsl = true;
                smt.Send(msg);
                lbmsg.Text = "Email Sent Successfully";
                lbmsg.ForeColor = System.Drawing.Color.ForestGreen;
                ViewState["sending"] = true;
                return "Email terkirim";
            }
            catch (Exception ex)
            {
                ViewState["sending"] = false;
                return "Email gagal terkirim";
            }

        }

        protected void btnKirim_ServerClick(object sender, EventArgs e)
        {
            string strEvent = "Insert";
            PoPurchMail popurchmail = new PoPurchMail();
            POPurchnSendMailFacade posend = new POPurchnSendMailFacade();
            if (!FileUpload1.HasFile)
            {
                DisplayAJAXMessage(this, "Attachment file belum ada");
                return;
            }
            if (ViewState["id"] != null)
            {
                popurchmail.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            string message = SendMail();
            popurchmail.TglKirim = DateTime.Now;
            popurchmail.NoPO = txtNoPO.Text;
            popurchmail.SupplierName = txtCariSupplier.Text;
            popurchmail.Email = txtEmail.Text;
            popurchmail.Report = message;
            popurchmail.CreatedBy = ((Users)Session["Users"]).UserName;
            popurchmail.Keterangan = txtMessage.Text;
            int intResult = 0;
            intResult = posend.Insert(popurchmail);
            if (intResult > 0)
            {
                MailSendPO(true);
                DisplayAJAXMessage(this, message);
                ViewState["seding"] = false;
                Clear();
            }

        }

        private string ValidateText()
        {
            if (txtNoPO.Text == string.Empty)
                return "No.PO Harus diisi";
            else if (txtCariSupplier.Text == string.Empty)
                return "Supplier Harus diisi";
            if (txtEmail.Text == string.Empty)
                return "Email Harus diisi";
            return string.Empty;


        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        protected void btnReport_ServerClick(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            peroideArea.Visible = true;
            rmail.Style.Add(HtmlTextWriterStyle.Height, "380px");
            BtnFormMail.Visible = true;
        }

        public void btnMainForm_ServerClick(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            peroideArea.Visible = false;
            rmail.Style.Add(HtmlTextWriterStyle.Height, "180px");
            BtnFormMail.Visible = false;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReportMailPurchaseOrder.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            sr.Close();
            Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>REPORT MAIL PURCHASE ORDER</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
            rmail.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            MailSendPO(false);
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
        }

        private void LoadPO(string strPONo)
        {
            Users users = (Users)Session["Users"];
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByNoWithDepo2(strPONo, users.UnitKerjaID);

            if (pOPurchnFacade.Error == string.Empty && pOPurchn.ID > 0)
            {
                Session["id"] = pOPurchn.ID;
                txtNoPO.Text = pOPurchn.NoPO;
                Session["POPurchnNo"] = pOPurchn.NoPO;
                SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
                SuppPurch suppPurch = suppPurchFacade.RetrieveById(pOPurchn.SupplierID);
                if (suppPurchFacade.Error == string.Empty)
                {
                    txtCariSupplier.Text = suppPurch.SupplierName;
                    txtEmail.Text = suppPurch.EMail;
                }
            }
        }
    }
}