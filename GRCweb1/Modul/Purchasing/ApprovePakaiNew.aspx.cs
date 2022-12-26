using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Globalization;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ApprovePakaiNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];

                LoadDept();
                Session["id"] = null;
                LoadOpenPakai();
                ViewState["counter"] = 0;
                LoadPakai((int)ViewState["counter"]);

                if (Request.QueryString["PakaiNo"] != null)
                {
                    ViewState["counter"] = FindPakai(Request.QueryString["PakaiNo"].ToString()); ;
                    LoadPakai((int)ViewState["counter"]);
                }
                info.Visible = (users.DeptID == 10) ? true : false;
            }

            Session["id"] = null;
        }

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
        }

        private void LoadPakai(int intRow)
        {
            ArrayList arrListPakai = new ArrayList();
            Users users = (Users)Session["Users"];

            if (users.Apv < 1)
            { return; }
            if (Session["ListOpenPakai"] != null)
                arrListPakai = (ArrayList)Session["ListOpenPakai"];

            if (intRow < arrListPakai.Count && intRow > -1)
            {
                Pakai pakai = new Pakai();
                pakai = (Pakai)arrListPakai[intRow];

                if (pakai.ID > 0)
                {
                    txtPakaiNo.Text = pakai.PakaiNo;
                    txtKodeDept.Text = pakai.DeptCode;
                    txtTanggal.Text = pakai.PakaiDate.ToString("dd-MMM-yyyy");
                    txtCreatedBy.Text = pakai.CreatedBy;
                    if (pakai.Ready == 0)
                        ChkReady.Checked = false;
                    else
                        ChkReady.Checked = true;
                    SelectDept(pakai.DeptName);

                    Session["id"] = pakai.ID;
                    Session["PakaiNo"] = pakai.PakaiNo;
                    //LoadSupplier();

                    ArrayList arrListPakaiDetail = new ArrayList();
                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    ArrayList arrPakaiDetail = new ArrayList();
                    if (pakai.ItemTypeID == 1)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                    if (pakai.ItemTypeID == 2)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForAsset(pakai.ID);
                    if (pakai.ItemTypeID == 3)
                        arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBiaya(pakai.ID);
                    if (pakaiDetailFacade.Error == string.Empty)
                    {
                        foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                        {
                            if (pakaiDetail.ID > 0)
                                arrListPakaiDetail.Add(pakaiDetail);
                        }
                    }
                    Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                    GridView1.DataSource = arrListPakaiDetail;
                    GridView1.DataBind();
                }
            }
            else
            {
                if (intRow == -1)
                {
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                }
                else
                {
                    ViewState["counter"] = (int)ViewState["counter"] - 1;
                }
                //POPurchn pOPurchn = new POPurchn();
                //pOPurchn = (POPurchn)arrListPakai[(int)ViewState["counter"]];
                if (arrListPakai.Count > 0)
                {
                    Pakai pakai = new Pakai();
                    pakai = (Pakai)arrListPakai[(int)ViewState["counter"]];
                }
            }

        }

        private void SelectDept(string strDeptname)
        {
            ddlDeptName.ClearSelection();
            foreach (ListItem item in ddlDeptName.Items)
            {
                if (item.Text == strDeptname)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private void LoadTermOfPay()
        {
            ArrayList arrTermOfPay = new ArrayList();
            TermOfPayFacade termOfPayFacade = new TermOfPayFacade();
            arrTermOfPay = termOfPayFacade.Retrieve();
            //ddlTermOfPay.Items.Add(new ListItem("-- Pilih Jenis TOP --", string.Empty));
            foreach (TermOfPay termOfPay in arrTermOfPay)
            {
                //ddlTermOfPay.Items.Add(new ListItem(termOfPay.TermPay, termOfPay.ID.ToString()));
            }
        }

        private void SelectTermOfPay(string strItemName)
        {
            //ddlTermOfPay.ClearSelection();
            //foreach (ListItem item in ddlTermOfPay.Items)
            //{
            //    if (item.Text == strItemName)
            //    {
            //        item.Selected = true;
            //        return;
            //    }
            //}
        }

        private void LoadOpenPakai()
        {
            Users users = (Users)Session["Users"];
            PakaiFacade pakaiFacade = new PakaiFacade();
            ArrayList arrPakai = new ArrayList();
            int Apv = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "declare @userid int set @userid=" + users.ID + " " +
                "select top 1 apv from (select 1 apv from dept where HeadID=@userid union all select 2 apv from dept where MgrID=@userid) a order by apv desc ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Apv = Convert.ToInt32(sdr["Apv"].ToString());
                }
            }
            //arrPakai = pakaiFacade.RetrieveOpenStatusForLogistik(users.ID, users.Apv, users.GroupID);
            arrPakai = pakaiFacade.RetrieveOpenStatusForLogistik(users.ID, Apv, users.GroupID);
            if (pakaiFacade.Error == string.Empty)
            {
                Session["ListOpenPakai"] = arrPakai;
            }
            if (users.Apv < 2 && users.DeptID == 10)
            {
                LblInfo.Visible = false;
                ChkReady.Visible = false;
                Timer1.Enabled = false;
                Timer2.Enabled = false;
                Panel2.Visible = false;
            }
            else
            {
                LblInfo.Visible = true;
                ChkReady.Visible = true;
            }
        }

        private int FindPakai(string strNoPakai)
        {
            ArrayList arrPakai = new ArrayList();
            int counter = 0;

            if (Session["ListOpenPakai"] != null)
                arrPakai = (ArrayList)Session["ListOpenPakai"];

            foreach (Pakai pakai in arrPakai)
            {
                if (pakai.PakaiNo == strNoPakai)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }

        private void LoadMataUang()
        {
            ArrayList arrMataUang = new ArrayList();
            MataUangFacade mataUangFacade = new MataUangFacade();
            arrMataUang = mataUangFacade.Retrieve();
            //ddlMataUang.Items.Add(new ListItem("-- Pilih Mata Uang --", string.Empty));
            foreach (MataUang mataUang in arrMataUang)
            {
                //ddlMataUang.Items.Add(new ListItem(mataUang.Lambang, mataUang.ID.ToString()));
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            int counter = FindPakai(txtSearch.Text);
            ViewState["counter"] = counter;
            //LoadPO(counter);
            LoadPakai(counter);
            txtSearch.Text = string.Empty;
        }

        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;
            //LoadPO((int)ViewState["counter"]);
            LoadPakai((int)ViewState["counter"]);
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string IDGudangs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptIDGudang", "SPB");
            int IDGudang = 0;
            int.TryParse(IDGudangs, out IDGudang);
            if (txtPakaiNo.Text != string.Empty)
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                Users user = (Users)Session["Users"];
                ArrayList arrDept = new DeptFacade().GetDeptFromHead(user.ID);
                int Apv = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @userid int set @userid=" + users.ID + " " +
                    "select top 1 apv from (select 1 apv from dept where HeadID=@userid union all select 2 apv from dept where MgrID=@userid) a order by apv desc ";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        Apv = Convert.ToInt32(sdr["Apv"].ToString());
                    }
                }

                if (Apv < 2)
                    Apv = (user.DeptID == IDGudang && arrDept.Count == 0) ? 3 : 1;
                else
                    Apv = 2;

                ArrayList arrPakai = pakaiFacade.AllRetrieveOpenStatusByNo(txtPakaiNo.Text, Apv);
                if (pakaiFacade.Error == string.Empty)
                {
                    foreach (Pakai pki in arrPakai)
                    {
                        pki.Status = Apv;// ((Users)Session["Users"]).Apv;
                        pki.ApprovalBy = ((Users)Session["Users"]).UserName;
                        pki.Ready = 0;
                        string strError = string.Empty;
                        ArrayList arrPakaiDetail = new ArrayList();

                        PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                        if (pki.ItemTypeID == 1)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                        else if (pki.ItemTypeID == 2)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForAsset(pki.ID);
                        else if (pki.ItemTypeID == 3)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBiaya(pki.ID);

                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pki, arrPakaiDetail, new PakaiDocNo());

                        if (pki.ID > 0)
                        {
                            /**
                             * informasikan jika periode sudah di closing
                             * tidak bisa di approve
                             * status ini akan mengirim email ke accounting
                             * added on 18-08-2016
                             */
                            AccClosingFacade cls = new AccClosingFacade();
                            AccClosing clsBln = cls.RetrieveByStatus(pki.PakaiDate.Month, pki.PakaiDate.Year);
                            if (clsBln.Status == 1)
                            {
                                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                                mess += "\\nHubungi Accounting Dept untuk solve masalah ini.";
                                DisplayAJAXMessage(this, mess);
                                //sKirimEmail(pki.PakaiNo, pki.PakaiDate.ToString("dd/MM/yyyy"));
                                return;
                            }
                            strError = pakaiProcessFacade.UpdateApproveNew(Apv);
                            EventLogProcess mp = new EventLogProcess();
                            EventLog evl = new EventLog();
                            mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                            mp.Pilihan = "Insert";
                            evl.UserID = ((Users)Session["Users"]).ID;
                            evl.AppLevel = (Apv - 1);// ((Users)Session["Users"]).Apv;
                            evl.DocNo = pki.PakaiNo.ToString();
                            evl.DocType = "SPB";
                            evl.AppDate = DateTime.Now;
                            evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                            mp.EventLogInsert(evl);
                        }

                        if (strError == string.Empty)
                        {

                            ArrayList arrPki = new ArrayList();
                            if (Session["ListOpenPakai"] != null)
                            {
                                arrPki = (ArrayList)Session["ListOpenPakai"];
                                ((Pakai)arrPki[(int)ViewState["counter"]]).Status = 0;
                                Session["ListOpenPakai"] = arrPki;
                            }

                            if (pki.ID > 0)
                            {
                                ViewState["counter"] = (int)ViewState["counter"] + 1;
                                LoadPakai((int)ViewState["counter"]);
                            }
                            Response.Redirect("ApprovePakaiNew.aspx");
                        }

                        else
                        {
                            //DisplayAJAXMessage(this, strError);
                        }
                    }
                }
            }
        }

        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            LoadPakai((int)ViewState["counter"]);
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfPakaiDetail"] = null;
            Session["PakaiNo"] = null;
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            //string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "S";
            string kd = users.DeptID.ToString();
            if (users.Apv < 1)
            { return; }
            Response.Redirect("ListPakaiApprove.aspx?approve=" + kd);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                SelectItem(row.Cells[1].Text);
            }
            else if (e.CommandName == "AddDelete")
            {
            }
        }

        private void SelectItem(string strItemName)
        {

        }

        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            //ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                //ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadSupplier()
        {
            ArrayList arrSuppPurch = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSuppPurch = suppPurchFacade.Retrieve();
            //ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier --", string.Empty));
            foreach (SuppPurch suppPurch in arrSuppPurch)
            {
                //ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
            }
        }

        protected void ChkReady_CheckedChanged(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            if (txtPakaiNo.Text != string.Empty)
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                //ubah ke array list
                string strpakai = txtPakaiNo.Text;
                ArrayList arrPakai = pakaiFacade.AllRetrieveOpenStatusByNo(txtPakaiNo.Text, ((Users)Session["Users"]).Apv);
                if (pakaiFacade.Error == string.Empty)
                {
                    foreach (Pakai pki in arrPakai)
                    {
                        //pki.Status = ((Users)Session["Users"]).Apv;
                        //pki.ApprovalBy = ((Users)Session["Users"]).UserName;
                        string strError = string.Empty;
                        ArrayList arrPakaiDetail = new ArrayList();

                        PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                        if (pki.ItemTypeID == 1)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                        else if (pki.ItemTypeID == 2)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForAsset(pki.ID);
                        else if (pki.ItemTypeID == 3)
                            arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiIdForBiaya(pki.ID);

                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pki, arrPakaiDetail, new PakaiDocNo());
                        int stsready = 0;
                        if (ChkReady.Checked == true)
                            stsready = 1;
                        else
                            stsready = 0;
                        pki.Ready = stsready;
                        if (pki.ID > 0)
                        {
                            strError = pakaiProcessFacade.UpdateReady();
                        }

                        if (strError == string.Empty)
                        {

                            ArrayList arrPki = new ArrayList();
                            if (Session["ListOpenPakai"] != null)
                            {
                                arrPki = (ArrayList)Session["ListOpenPakai"];
                                ((Pakai)arrPki[(int)ViewState["counter"]]).Status = 0;
                                Session["ListOpenPakai"] = arrPki;
                            }

                            if (pki.ID > 0)
                            {
                                ((Pakai)arrPki[(int)ViewState["counter"]]).Ready = 1;
                                Session["ListOpenPakai"] = arrPki;
                                ViewState["counter"] = (int)ViewState["counter"] + 1;
                                LoadPakai((int)ViewState["counter"]);
                            }
                        }

                        else
                        {
                            //DisplayAJAXMessage(this, strError);
                        }
                        break;
                    }
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int jumlah = 0;
            string strtanggal = Session["firstload"].ToString();
            PakaiFacade pakaiFacade = new PakaiFacade();
            jumlah = pakaiFacade.RetrieveNew(strtanggal);
            if (jumlah > 0)
            {
                Timer2.Enabled = true;
                Panel2.Visible = true;
            }
            else
            {
                Timer2.Enabled = false;
                Panel2.Visible = false;
            }
        }
        protected void Timer2_Tick(object sender, EventArgs e)
        {
            if (Panel2.BackColor == System.Drawing.Color.White)
            {
                Panel2.BackColor = System.Drawing.Color.Red;
                Console.Beep();
            }
            else
            {
                Panel2.BackColor = System.Drawing.Color.White;
                Console.Beep();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Timer2.Enabled = false;
            Panel2.Visible = false;
            LoadOpenPakai();
            ViewState["counter"] = 0;
            LoadPakai((int)ViewState["counter"]);
            Session["firstload"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"); ;
        }
        private void KirimEmail(string SPBNo, string Tanggal)
        {
            UsersFacade usf = new UsersFacade();
            Users users = usf.RetrieveById(246);
            MailMessage mail = new MailMessage();
            EmailReportFacade msg = new EmailReportFacade();
            mail.From = new MailAddress("system_support@grcboard.com");
            mail.To.Add(users.UsrMail.ToString());
            mail.Bcc.Add("noreplay@grcboard.com");
            mail.Subject = "Approval Pemakaian Error ";
            mail.Body = "Di informasikan ada status pemakaian yang blm di realease : \n\r";
            mail.Body += "SPB NO. :" + SPBNo;
            mail.Body += "\n\rSPB Date :" + Tanggal;
            //mail.Body += "\n\rItemName :" + Material;
            SmtpClient smt = new SmtpClient(msg.mailSmtp());
            smt.Host = msg.mailSmtp();
            smt.Port = msg.mailPort();
            smt.EnableSsl = true;
            smt.DeliveryMethod = SmtpDeliveryMethod.Network;
            smt.UseDefaultCredentials = false;
            smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
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
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}