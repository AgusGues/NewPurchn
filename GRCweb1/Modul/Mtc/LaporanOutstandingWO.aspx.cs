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
using BasicFrame.WebControls;

namespace GRCweb1.Modul.MTC
{
    public partial class LaporanOutstandingWO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Flag2"] = ((Users)Session["Users"]).DeptID;
                //lstBA2.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(lstBA2_Command);
                LoadOutstandingWO();
                LoadDept();
                //Users user = (Users)Session["Users"];  
            }


        }

        private void LoadOutstandingWO()
        {
            string Flag = Session["FlagRB"].ToString();
            int DeptID1 = Convert.ToInt32(Session["Flag2"]);
            if (Flag == "RB")
            {
                Panel2.Visible = true; Panel1.Visible = false;
                RBGA.Visible = true; RBIT.Visible = true; RBMtc.Visible = true;
                Users user = ((Users)Session["Users"]);
                string TglSkr = DateTime.Now.ToString("yyyyMMdd");
                WorkOrder_New domainOut = new WorkOrder_New();
                WorkOrderFacade_New facadeOut = new WorkOrderFacade_New();
                ArrayList arrOut = new ArrayList();

                arrOut = facadeOut.RetrieveOutWO(DeptID1, Flag);

                lstBA2.DataSource = arrOut;
                lstBA2.DataBind();
            }
            else if (Flag == "NonRB")
            {
                Panel2.Visible = false; Panel1.Visible = true;
                RBGA.Visible = false; RBIT.Visible = false; RBMtc.Visible = false;
                Users user = ((Users)Session["Users"]);
                string TglSkr = DateTime.Now.ToString("yyyyMMdd");
                WorkOrder_New domainOut = new WorkOrder_New();
                WorkOrderFacade_New facadeOut = new WorkOrderFacade_New();
                ArrayList arrOut = new ArrayList();

                arrOut = facadeOut.RetrieveOutWO(DeptID1, Flag);

                lstBA.DataSource = arrOut;
                lstBA.DataBind();
            }

            //Users user = ((Users)Session["Users"]);
            //string TglSkr = DateTime.Now.ToString("yyyyMMdd");
            //WorkOrder_New domainOut = new WorkOrder_New();
            //WorkOrderFacade_New facadeOut = new WorkOrderFacade_New();
            //ArrayList arrOut = new ArrayList();

            //arrOut = facadeOut.RetrieveOutWO(DeptID1);

            //lstBA.DataSource = arrOut;
            //lstBA.DataBind();

        }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB1.Checked == true)
            {
                Session["Flag"] = 1;
                Response.Redirect("LaporanWorkOrder_New.aspx");
            }
        }

        protected void RBIT_CheckedChanged(object sender, EventArgs e)
        {
            if (RBIT.Checked == true)
            {
                Session["FlagRB"] = "RB"; Session["Flag2"] = "14";
                RBGA.Checked = false; RBMtc.Checked = false;
                LoadOutstandingWO();
                Panel1.Visible = false; Panel2.Visible = true;
            }
        }

        protected void RBGA_CheckedChanged(object sender, EventArgs e)
        {
            if (RBGA.Checked == true)
            {
                Session["FlagRB"] = "RB"; Session["Flag2"] = "7";
                RBMtc.Checked = false; RBIT.Checked = false;
                LoadOutstandingWO();
                Panel1.Visible = false; Panel2.Visible = true;
            }
        }

        protected void RBMtc_CheckedChanged(object sender, EventArgs e)
        {
            if (RBMtc.Checked == true)
            {
                Session["FlagRB"] = "RB"; Session["Flag2"] = "19";
                RBGA.Checked = false; RBIT.Checked = false;
                LoadOutstandingWO();
                Panel1.Visible = false; Panel2.Visible = true;
            }
        }

        protected void lstBA2_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            Repeater rpt = (Repeater)sender;
            Image hapus2 = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus2");
            Image hapus1 = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus1");
            Image Apv1 = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("Apv1");
            Image Apv2 = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("Apv2");
            hapus2.Attributes.Add("onclick", "confirm_batal();");

            switch (e.CommandName)
            {
                case "Apv1":
                    Apv1.Visible = false; Apv2.Visible = true;
                    break;

                case "Apv2":
                    WorkOrder_New DomainApvT = new WorkOrder_New();
                    WorkOrderFacade_New FacadeApvT = new WorkOrderFacade_New();

                    DomainApvT.WOID = Convert.ToInt32(hapus2.CssClass);
                    DomainApvT.UserName = user.UserName;

                    int intResult = 0;
                    intResult = FacadeApvT.UpdateWO_ApvNaikTarget(DomainApvT);
                    if (intResult > 0)
                    {
                        Response.Redirect("LaporanWorkOrder_New.aspx");
                    }
                    break;

                case "hps1":
                    hapus2.Visible = true; hapus1.Visible = false;
                    break;

                case "hps2":
                    hapus2.Visible = true; hapus1.Visible = false;
                    string Alasan = Session["AlasanCancel"].ToString();

                    WorkOrder_New DomainWO = new WorkOrder_New();
                    WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();

                    DomainWO.ID = Convert.ToInt32(hapus2.CssClass);
                    int ID = DomainWO.ID;
                    DomainWO.UserName = user.UserName;
                    DomainWO.AlasanCancel = Alasan;
                    int intResult2 = 0;
                    intResult2 = FacadeWO.CancelWO_T3(DomainWO);
                    if (intResult2 > 0)
                    {
                        Response.Redirect("LaporanWorkOrder_New.aspx");
                    }
                    break;

            }


        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            int RowNum = e.Item.ItemIndex;
            Image Simpan = (Image)lstBA.Items[RowNum].FindControl("simpan");
            Image img = (Image)lstBA.Items[RowNum].FindControl("edit");
            Label NoWO = (Label)e.Item.FindControl("txtNoWO");
            Label UraianK = (Label)e.Item.FindControl("lblUraianPekerjaan");
            Label DeptPemohon = (Label)e.Item.FindControl("lblFromDeptName");
            Label Pelaksana = (Label)e.Item.FindControl("lblPelaksana");
            Label AreaWo = (Label)e.Item.FindControl("lblAreaWO");
            Label TargetT1 = (Label)e.Item.FindControl("txtTarget");
            BDPLite dt = (BDPLite)e.Item.FindControl("txtUpdateTgl");
            ImageButton hapus = (ImageButton)e.Item.FindControl("hapus");


            switch (e.CommandName)
            {
                case "Edit":
                    Simpan.Visible = true;
                    img.Visible = false; dt.Enabled = true;
                    break;
                case "Save":
                    Users user = ((Users)Session["Users"]);
                    WorkOrder_New WoOut = new WorkOrder_New();
                    WorkOrderFacade_New WoOutFacade = new WorkOrderFacade_New();

                    WoOut = WoOutFacade.RetrieveTarget(NoWO.Text.ToString().Trim());

                    if (WoOut.ID >= 0)
                    {
                        WorkOrder_New WoOut1 = new WorkOrder_New();
                        WorkOrderFacade_New WoOutFacade1 = new WorkOrderFacade_New();

                        //if (WoOut.ID == 0)
                        //{
                        WoOut1.NoWO = NoWO.Text.ToString().Trim();
                        WoOut1.TargetT1 = Convert.ToDateTime(TargetT1.Text).ToString("yyyy-MM-dd");
                        WoOut1.TargetT2 = dt.SelectedDate.ToString("yyyy-MM-dd");
                        WoOut1.UserName = user.UserName;
                        WoOut1.Target = WoOut.Target;

                        int intResult = 0;
                        intResult = WoOutFacade1.InsertTarget(WoOut1);
                        //Add By Razib WO : WO-IT-K0030621 : 27-08-2021

                        #region Proses Mail#1
                        #region #1a
                        string Email = string.Empty;
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "select AccountEmail from WorkOrder_AccountEmail where DeptID='" + user.DeptID + "' and  RowStatus=2 ";
                        SqlDataReader sdr = zl.Retrieve();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                Email = sdr["AccountEmail"].ToString();
                            }
                        }
                        #endregion
                        #region #1b
                        int DeptIDUsr = 0;
                        ZetroView zlx = new ZetroView();
                        zlx.QueryType = Operation.CUSTOM;
                        zlx.CustomQuery = " select DeptID_Users from WorkOrder where NoWo ='" + NoWO.Text + "' and  RowStatus>-1 ";
                        SqlDataReader sdrx = zlx.Retrieve();
                        if (sdrx.HasRows)
                        {
                            while (sdrx.Read())
                            {
                                DeptIDUsr = Convert.ToInt32(sdrx["DeptID_Users"].ToString());
                            }
                        }
                        #endregion
                        #region #1c
                        string Email0 = string.Empty;
                        ZetroView zlz = new ZetroView();
                        zlz.QueryType = Operation.CUSTOM;
                        zlz.CustomQuery = "select AccountEmail from WorkOrder_AccountEmail where DeptID='" + DeptIDUsr + "' and  RowStatus=2 ";
                        SqlDataReader sdrz = zlz.Retrieve();
                        if (sdrz.HasRows)
                        {
                            while (sdrz.Read())
                            {
                                Email0 = sdrz["AccountEmail"].ToString();
                            }
                        }
                        #endregion
                        #endregion
                        #region Prosess Mail#2
                        try
                        {
                            Depo depo = new Depo();
                            DepoFacade depof = new DepoFacade();
                            depo = depof.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
                            MailMessage msg = new MailMessage();
                            EmailReportFacade emailFacade = new EmailReportFacade();
                            msg.From = new MailAddress("system_support@grcboard.com");
                            msg.To.Add(Email);
                            msg.CC.Add(Email0);
                            msg.Subject = emailFacade.mailSubjectWo(depo.DepoName);
                            msg.Body += emailFacade.mailBodyWO() + "\n\r";
                            msg.Body += "No WO    : " + NoWO.Text + "\n\r";
                            msg.Body += "Uraian Pekerjaan    : " + UraianK.Text + "\n\r";
                            msg.Body += "Dept Pemohon    : " + DeptPemohon.Text + "\n\r";
                            msg.Body += "Pelaksana    : " + Pelaksana.Text + "\n\r";
                            msg.Body += "Area Wo    : " + AreaWo.Text + "\n\r";
                            msg.Body += "Target Awal  : " + Convert.ToDateTime(TargetT1.Text).ToString("dd-MM-yyyy") + "\n\r";
                            msg.Body += "Target Baru  : " + dt.SelectedDate.ToString("dd-MM-yyyy") + "\n\r";
                            string plant = ""; string plant1 = "";
                            switch (depo.ID)
                            {
                                case 1:
                                    plant = "ctrp";
                                    plant1 = "123.123.123.129";
                                    break;
                                case 7:
                                    plant = "krwg";
                                    plant1 = "192.168.222.21";
                                    break;
                                case 13:
                                    plant = "Jombang";
                                    plant1 = "192.168.252.3";
                                    break;
                                default:
                                    plant1 = "";
                                    plant = "purchasing"; break;
                            }
                            msg.Body += (plant1 == "") ? "" : "Silahkan Klik : http://" + plant1 + "\n\r";
                            msg.Body += "atau Klik : http://" + plant + ".grcboard.com" + "\n\r\n\r";
                            msg.Body += "Terimakasih, " + "\n\r";
                            msg.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
                            msg.Body += "Regard's, " + "\n\r";
                            msg.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                            //msg.Body += emailFacade.mailFooter();
                            SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                            smt.Host = emailFacade.mailSmtp();
                            smt.Port = emailFacade.mailPort();
                            smt.EnableSsl = true;
                            smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smt.UseDefaultCredentials = false;
                            smt.Credentials = new System.Net.NetworkCredential("razib@grcboard.com", "10032011");
                            //bypas certificate
                            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };
                            smt.Send(msg);
                            resultMailSucc.Visible = true;
                            resultMailSucc.Text = "Email terkirim";
                            resultMailFail.Visible = false;
                            resultMailFail.Text = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            resultMailSucc.Visible = true;
                            resultMailSucc.Text = "Email gagal terkirim " + ex.Message;
                            resultMailFail.Visible = false;
                            resultMailFail.Text = string.Empty;
                        }
                        #endregion

                        if (intResult >= 0)
                        {
                            Response.Redirect("LaporanWorkOrder_New.aspx");
                        }
                        //}
                    }
                    //GMarketing gm2 = new GMarketing();
                    //GMarketingFacade fgm2 = new GMarketingFacade();

                    //gm2.Partno = partno.Text.Trim();
                    //gm2.GroupMarketing = ddlGM.SelectedValue.ToString();
                    //int intResult = 0;

                    //intResult = fgm2.Update(gm2);
                    //if (intResult > -1)
                    //{
                    //    LoadData();
                    //}
                    //else
                    //{
                    //    DisplayAJAXMessage(this, " Gagal simpan !! "); return;
                    //}

                    break;
            }
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptName.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDeptName.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
            IDDept.Text = users.DeptID.ToString();

        }


        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users Users = ((Users)Session["Users"]);
            WorkOrder_New domainUsers = new WorkOrder_New();
            WorkOrderFacade_New facadeUsers = new WorkOrderFacade_New();
            domainUsers = facadeUsers.RetrieveDataUsers(Users.ID);
            Session["StatusApv"] = domainUsers.StatusApv; Session["DeptID"] = domainUsers.DeptID;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                WorkOrder_New gm = (WorkOrder_New)e.Item.DataItem;
                int RowNum = e.Item.ItemIndex;

                ImageButton img = (ImageButton)e.Item.FindControl("edit");
                ImageButton simpan = (ImageButton)e.Item.FindControl("simpan");
                ImageButton hapus = (ImageButton)e.Item.FindControl("hapus");

                string StatusApv = Session["StatusApv"].ToString();
                string DeptID = Session["DeptID"].ToString();

                if (gm.Apv == 0 && gm.Ket == "Open" && StatusApv == "8" || gm.Apv == 0 && gm.Ket == "Open" && StatusApv == "9")
                {
                    img.Enabled = true; hapus.Visible = false;
                }
                /** Untuk Manager : Ketika sdh sampai Target 3 tapi masih belum selesai **/
                /** Untuk Manager : Harus di cancel **/
                else if (gm.Apv == 1 && gm.Ket == "Lewat" && StatusApv == "3" && gm.Target == 3)
                {
                    img.Visible = false; hapus.Visible = true; simpan.Visible = false;
                }
                else if (StatusApv == "3" && gm.Ket == "Lewat" && gm.Target != 3)
                {
                    img.Visible = false; hapus.Visible = false; simpan.Visible = false;
                }
                /** Untuk Head IT **/
                else if (gm.Apv == 1 && gm.Ket == "Lewat" && StatusApv == "9" && gm.Target == 3
                    || gm.Apv == 1 && gm.Ket == "Lewat" && StatusApv == "8" && gm.Target == 3)
                {
                    //img.Visible = false; hapus.Visible = false; simpan.Visible = false; hapus.Enabled = false;
                    img.Visible = true; hapus.Visible = false; simpan.Visible = false; hapus.Enabled = false;
                }
                else if (gm.Apv == 1 && gm.Ket == "Lewat" && StatusApv == "9" && gm.Target == 2
                    || gm.Apv == 1 && gm.Ket == "Lewat" && StatusApv == "8" && gm.Target == 2)
                {
                    img.Visible = true; hapus.Visible = false; simpan.Visible = false; hapus.Enabled = false;
                }
                else if (gm.Apv == -1 && gm.Ket == "Lewat" && StatusApv == "8"
                    || gm.Apv == -1 && gm.Ket == "Lewat" && StatusApv == "9"
                    || gm.Apv == 0 && gm.Ket == "Open" && StatusApv == "8"
                    || gm.Apv == 1 && gm.Ket == "Open" && StatusApv == "8")
                {
                    img.Visible = true; hapus.Visible = false; simpan.Visible = false; hapus.Enabled = false;
                }



                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst");
                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    if (gm.Ket == "Lewat")
                    {
                        tr.Cells[8].Attributes.Add("style", "background-color:Red; font-weight:bold; color:White;");
                    }
                    else if (gm.Ket == "Open")
                    {
                        tr.Cells[8].Attributes.Add("style", "background-color:Blue; font-weight:bold; color:White;");
                    }
                }
            }
        }

        protected void lstBA2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users Users = ((Users)Session["Users"]);
            WorkOrder_New domainUsers = new WorkOrder_New();
            WorkOrderFacade_New facadeUsers = new WorkOrderFacade_New();
            domainUsers = facadeUsers.RetrieveDataUsers(Users.ID);
            Session["StatusApv1"] = domainUsers.StatusApv; Session["DeptID1"] = domainUsers.DeptID;

            Repeater lstBA2;


            string StatusApv1 = Session["StatusApv1"].ToString();
            string DeptID1 = Session["DeptID1"].ToString();

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label Target1 = (Label)e.Item.FindControl("Target1");
                Label Target2 = (Label)e.Item.FindControl("Target2");
                Label Target3 = (Label)e.Item.FindControl("Target3");
                ImageButton hapus2 = (ImageButton)e.Item.FindControl("hapus2");
                ImageButton hapus1 = (ImageButton)e.Item.FindControl("hapus1");
                ImageButton Apv1 = (ImageButton)e.Item.FindControl("Apv1");
                ImageButton Apv2 = (ImageButton)e.Item.FindControl("Apv2");

                WorkOrder_New gm = (WorkOrder_New)e.Item.DataItem;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst2");
                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    if (gm.Ket == "Lewat")
                    {
                        tr.Cells[6].Attributes.Add("style", "background-color:Red; font-weight:bold; color:White;");
                    }
                    if (gm.Ket == "Lewat" && gm.Target == 3 && StatusApv1 == "3")
                    {
                        hapus2.Visible = false; hapus1.Visible = true; Apv1.Visible = false; Apv2.Visible = false;
                    }
                    else if (gm.Ket == "Lewat" && gm.Target == 2 && StatusApv1 == "3" ||
                             gm.Ket == "Lewat" && gm.Target == 1 && StatusApv1 == "3" ||
                             gm.Ket == "Lewat" && gm.Target == 3 && StatusApv1 == "3")
                    {
                        Apv1.Visible = false; Apv2.Visible = false; hapus2.Visible = false; hapus1.Visible = false;
                    }
                    else
                    {
                        hapus2.Visible = false; hapus1.Visible = false;
                    }

                    if (gm.Ket == "Open")
                    {
                        tr.Cells[6].Attributes.Add("style", "background-color:Blue; font-weight:bold; color:White;");
                        Apv1.Visible = true; Apv2.Visible = false;

                    }
                    if (Target1.Text.Contains("1900"))
                    {
                        Target1.Text = "-";
                    }
                    if (Target2.Text.Contains("1900"))
                    {
                        Target2.Text = "-";
                    }
                    if (Target3.Text.Contains("1900"))
                    {
                        Target3.Text = "-";
                    }

                    if (StatusApv1 == "3")
                    {
                        RBIT.Visible = false; RBMtc.Visible = false; RBGA.Visible = false;
                    }
                    if (StatusApv1 == "0")
                    {
                        Apv1.Visible = false; Apv2.Visible = false;
                    }
                }
            }
        }
    }
}