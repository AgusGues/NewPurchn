using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.Mtc
{
    public partial class InputWorkOrder_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                WorkOrder_New wor = new WorkOrder_New();
                WorkOrderFacade_New worf = new WorkOrderFacade_New();
                wor = worf.RetrieveUserLevelNew(user.ID);
                Session["StatusApv"] = wor.StatusApv;
                int StatusApv = Convert.ToInt32(wor.StatusApv);

                PanelAreaKerja.Visible = false; LabelAreaKerja.Visible = false; RBH.Visible = false; RBS.Visible = false;



                if (StatusApv > 0 && StatusApv != 10)
                {
                    LoadList();

                    if (user.DeptID == 4 || user.DeptID == 5 || user.DeptID == 18 || user.DeptID == 19)
                    {
                        LoadWOMTN();
                    }
                }

                else if (StatusApv == 0 || StatusApv == 10)
                {
                    LabelJudul.Visible = true;
                    LabelJudul.Text = "INPUTAN WORK ORDER";
                    LabelAreaKerja.Visible = true;
                    ddlArea.Visible = true;
                    LabelAreaKerja.Enabled = false;
                    ddlArea.Enabled = false;
                    txtCount.Visible = false;

                    PanelSatu.Visible = true;
                    PanelDua.Visible = false;
                    PanelTiga.Visible = true;
                    PanelEmpat.Visible = false;
                    btnNext.Visible = false;
                    btnPrev.Visible = false;
                    btnApprove.Visible = false;
                    btnUpdate.Visible = false;
                    btnUnApprove.Visible = false;
                    PanelPilihDept.Visible = true;
                    LabelPDept.Visible = true;
                    ddlPDept.Visible = true;
                    LabelTipe.Visible = false;
                    txtTipe.Visible = false;

                    RBS.Enabled = true;

                    btnUpdate.Visible = false;
                    btnUpdate1.Visible = false;
                    btnUpdateTarget.Visible = false;
                    btnFinish.Visible = false;



                    LoadDDL_Dept();
                    LoadDeptUsers();

                    //LoadWOMTN();
                }

            }
            btnUnApprove.Attributes.Add("onclick", "return confirm_batal();");
        }

        private void LoadWOMTN()
        {
            Users user = (Users)Session["Users"];

            WorkOrder_New domainMtn = new WorkOrder_New();
            WorkOrderFacade_New facadeMtn = new WorkOrderFacade_New();
            domainMtn = facadeMtn.RetrieveOutWO2Day(user.ID);
            if (domainMtn.Total > 0)
            {
                PaneLWarning.Visible = true; LabelWarning.Visible = true;
                LabelWarning.Text = "Total ada = " + domainMtn.Total + "" + " WO yang sudah mendekati waktu target ( Range 2 hari lagi mendekati target selesai )";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Blink", "CallBlink('" + this.LabelWarning.ClientID + "');", true);
            }


        }

        protected void ddlArea_Change(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            if (user.DeptID == 2)
            {
                if (ddlArea.SelectedItem.ToString().Trim() == "Zona 2" || ddlArea.SelectedItem.ToString().Trim() == "Zona 3")
                {
                    PanelSubArea.Visible = true; LabelSubArea.Visible = true; ddlSubArea.Visible = true;

                    WorkOrderFacade_New woFacade = new WorkOrderFacade_New();
                    ArrayList arrSubArea = woFacade.RetrieveSubArea(ddlArea.SelectedItem.ToString().Trim());
                    if (arrSubArea.Count > 0)
                    {
                        ddlSubArea.Items.Clear();
                        ddlSubArea.Items.Add(new ListItem("---- Pilih Line ----", "0"));
                        foreach (WorkOrder_New SubArea in arrSubArea)
                        {
                            ddlSubArea.Items.Add(new ListItem(SubArea.SubArea, SubArea.SubAreaID.ToString()));
                        }

                    }

                }
                else if (ddlArea.SelectedItem.ToString().Trim() == "Zona 1")
                {
                    PanelSubArea.Visible = false; LabelSubArea.Visible = false; ddlSubArea.Visible = false;
                }


            }

        }

        protected void ddlPDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPDept.SelectedItem.ToString().Trim() == "IT")
            {
                PanelIT.Visible = true; RBS.Visible = true; RBH.Visible = true; LabelJenis.Visible = true;
                LabelAreaKerja.Enabled = false; PanelAreaKerja.Visible = false; LabelTipeWO.Visible = true;
                ddlTipeWO.Visible = true; PanelTipe.Visible = true; RBH.Checked = false; RBS.Checked = false;
            }
            else
            {
                LoadDDL_Area(); LabelAreaKerja.Visible = true; ddlArea.Visible = true; LabelSubArea.Visible = true; ddlSubArea.Visible = true;
                LabelTipeWO.Visible = false; ddlTipeWO.Visible = false; PanelTipe.Visible = false;
                PanelHardware.Visible = false; PanelSoftware.Visible = false;
            }

        }

        private void LoadHead()
        {
            WorkOrder_New WOH = new WorkOrder_New();
            WorkOrderFacade_New FacadeWOH = new WorkOrderFacade_New();
            ArrayList arrHead = FacadeWOH.RetrieveHead();
            if (arrHead.Count > 0)
            {
                ddlPelaksana.Items.Clear();
                ddlPelaksana.Items.Add(new ListItem("---- Pilih Head ----", "0"));
                foreach (WorkOrder_New head in arrHead)
                {
                    ddlPelaksana.Items.Add(new ListItem(head.HeadName, head.HeadID.ToString()));
                }
            }

        }

        protected void LoadDDL_Area()
        {
            PanelIT.Visible = false; RBS.Visible = false; RBH.Visible = false; LabelJenis.Visible = false;
            LabelAreaKerja.Enabled = true; PanelAreaKerja.Visible = true;
            ddlArea.Enabled = true;

            Users user = ((Users)Session["Users"]);
            WorkOrderFacade_New woFacade = new WorkOrderFacade_New();
            ArrayList arrArea = woFacade.RetrieveArea(user.DeptID, Convert.ToInt32(ddlPDept.SelectedValue));
            if (arrArea.Count > 0)
            {
                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("---- Pilih Area WO ----", "0"));
                foreach (WorkOrder_New area in arrArea)
                {
                    ddlArea.Items.Add(new ListItem(area.AreaWO, area.AreaID.ToString()));
                }
            }
            else
            {
                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("General", "1"));
            }
        }

        protected void LoadDDL_Dept()
        {
            Users user = ((Users)Session["Users"]);

            WorkOrder_New domainSub = new WorkOrder_New();
            WorkOrderFacade_New facadeSub = new WorkOrderFacade_New();
            int CekSub = facadeSub.RetrieveSub(user.ID);

            if (CekSub == 0)
            {

                WorkOrderFacade_New woFacade = new WorkOrderFacade_New();
                ArrayList arrDept = woFacade.RetrieveDept(user.DeptID);
                if (arrDept.Count > 0)
                {
                    ddlPDept.Items.Clear();
                    ddlPDept.Items.Add(new ListItem("---- Pilih Dept ----", "0"));
                    foreach (WorkOrder_New dept in arrDept)
                    {
                        ddlPDept.Items.Add(new ListItem(dept.NamaDept, dept.IDDept.ToString()));
                    }
                    //ddlPDept.Items.Add(new ListItem("Umum", "1"));
                }
            }
            else if (CekSub > 0)
            {
                ddlPDept.Items.Clear();
                ddlPDept.Items.Add(new ListItem("HRD GA", "1"));
            }
            //else
            //{
            //    ddlPDept.Items.Clear();
            //    ddlPDept.Items.Add(new ListItem("Umum", "1"));           
            //}
        }


        protected void LoadList()
        {
            Users user = ((Users)Session["Users"]);
            int StApv = Convert.ToInt32(Session["StatusApv"]);

            WorkOrder_New WODomain = new WorkOrder_New();
            WorkOrderFacade_New WOFacade = new WorkOrderFacade_New();
            int DeptID = WOFacade.RetrieveDeptIDHead(user.ID);
            Session["DeptID"] = DeptID;

            LoadListApprovalWO();

            if (noWO.Value != "0")
            {
                string[] ListSPP = noWO.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                LoadListApprovalWO(ListOpenPO[0].ToString());
                btnNext.Enabled = (ListOpenPO.Count() > 1) ? true : false;
                btnPrev.Enabled = false;
                ViewState["index"] = idx;


                int CountWO = ListOpenPO.Count();
                int IndexWO = int.Parse(ViewState["index"].ToString()) + 1;

                txtCount.Text = IndexWO + "/" + CountWO;

            }
            else
            {
                btnNext.Visible = false;
                btnNext.Enabled = false;
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
                btnPrev.Enabled = false;

                PanelSatu.Visible = false; PanelDua.Visible = false; PanelTiga.Visible = false; PanelEmpat.Visible = false;
                LabelPeminta.Visible = false; txtPeminta.Visible = false; LabelTglBuat.Visible = false; txtTglBuat.Visible = false;
                LabelTglDisetujui.Visible = false; txtTglDisetujui.Visible = true; LabelDept.Visible = true; LabelTargetSelesai.Visible = false;
                txtTargetSelesai.Visible = false; btnSimpan.Visible = false; btnNew.Visible = false;
                LabelPelaksana.Visible = false; txtPelaksana.Visible = false; txtTargetSelesai.ReadOnly = false; txtPelaksana.ReadOnly = false;
                LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi2.Visible = false;
                btnList.Visible = false; LabelPerbaikan.Visible = false; txtPerbaikan.Visible = false; PanelPilihDept.Visible = false;
                LabelPDept.Visible = false; ddlPDept.Visible = false; ddlArea.Visible = true; LabelAreaKerja.Visible = false;
                PanelAreaKerja.Visible = false; PaneLinfo.Visible = true; LabelInfo.Visible = true;
                lstBA.Visible = false; btnUpdate.Visible = false; btnUpdate1.Visible = false; btnUpdateTarget.Visible = false; btnFinish.Visible = false;
                btnUnApprove.Visible = false; btnApprove.Visible = false; btnPrev.Visible = false;


            }
            string[] ListOpenPOx = noWO.Value.Split(',');
            string[] ListOpenPOd = ListOpenPOx.Distinct().ToArray();
            int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idxx - 1) >= ListOpenPOd.Count()) ? false : true;
            ViewState["index"] = idxx;
            //txtAlasan.Attributes.Add("onkeyup", "onKeyUp()");
            if (Request.QueryString["NoWO"] != null)
            {
                LoadListApprovalWO(Request.QueryString["NoWO"].ToString());
            }
        }

        protected void btnUpdate1_ServerClick(object sender, EventArgs e)
        {
            string Pelaksana = ddlPelaksana.SelectedItem.ToString().Trim();

            WorkOrder_New WorkOrder = new WorkOrder_New();
            WorkOrderFacade_New WorkOrderFacade = new WorkOrderFacade_New();

            int intResult = 0;
            WorkOrder.Pelaksana = Pelaksana;
            WorkOrder.WOID = Convert.ToInt32(LabelIDWO.Text);

            intResult = WorkOrderFacade.UpdatePelaksana(WorkOrder);

            if (intResult > 0)
            {
                Response.Redirect("InputWorkOrder_New.aspx");
            }
        }

        protected void btnUpdateTarget_ServerClick(object sender, EventArgs e)
        {
            WorkOrder_New WorkOrder = new WorkOrder_New();
            WorkOrderFacade_New WorkOrderFacade = new WorkOrderFacade_New();
            Users user = (Users)Session["Users"];

            if (user.DeptID == 4 || user.DeptID == 5 || user.DeptID == 18 || user.DeptID == 19 || user.DeptID == 25)
            {
                if (txtTargetSelesai.Text == "")
                {
                    DisplayAJAXMessage(this, "Tanggal target selesai belum ditentukan !! "); return;
                }

            }

            //if (txtFinishDate.Text == string.Empty)
            //{
            //    DisplayAJAXMessage(this, "Tanggal selesai belum ditentukan");
            //    return;
            //}

            int intResult = 0;
            WorkOrder.DueDateWO = txtTargetSelesai.Text;
            WorkOrder.WOID = Convert.ToInt32(LabelIDWO.Text);

            intResult = WorkOrderFacade.UpdateTarget(WorkOrder);

            if (intResult > 0)
            {
                Response.Redirect("InputWorkOrder_New.aspx");
            }
        }

        protected void btnFinish_ServerClick(object sender, EventArgs e)
        {
            WorkOrder_New WorkOrder = new WorkOrder_New();
            WorkOrderFacade_New WorkOrderFacade = new WorkOrderFacade_New();

            int intResult = 0;
            WorkOrder.FinishDate = txtFinishDate.Text;
            WorkOrder.WOID = Convert.ToInt32(LabelIDWO.Text);

            intResult = WorkOrderFacade.UpdateFinish(WorkOrder);

            if (intResult > 0)
            {
                Response.Redirect("InputWorkOrder_New.aspx");
            }


        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            if (txtFinishDate.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Tanggal selesai belum ditentukan");
                return;
            }

            int StatusAproval = Convert.ToInt32(Session["StatusApv"]);

            if (StatusAproval == 9)
            { int StApv1 = 4; Session["StApv1"] = StApv1; }

            int StApv = Convert.ToInt32(Session["StApv1"]);
            int ToDept = Convert.ToInt32(Session["ToDept"]);

            WorkOrder_New woR1 = new WorkOrder_New();
            WorkOrderFacade_New worF1 = new WorkOrderFacade_New();
            int intResult = 0;
            woR1.UserID = users.ID;
            woR1.FinishDate = txtFinishDate.Text;
            woR1.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR1.UserName = users.UserName;
            woR1.Apv = StApv;
            woR1.UraianPerbaikan = txtPerbaikan.Text;
            woR1.StatusApv = Convert.ToInt32(Session["StatusApv"]).ToString();
            woR1.ToDept = ToDept;
            woR1.AreaWO = ddlArea.SelectedItem.ToString();

            intResult = worF1.UpdateWO_Apv_L4(woR1);

            if (intResult > -1)
            {
                int intResult1 = 0;
                intResult1 = worF1.InsertLog_Apv(woR1);
                if (intResult1 > -1)
                {
                    Response.Redirect("InputWorkOrder_New.aspx");
                }
            }


        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            if (txtTargetSelesai.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Target selesai belum ditentukan");
                return;
            }

            if (txtPelaksana5.Text.Trim() == string.Empty || txtPelaksana5.Text.Trim() == "")
            {
                DisplayAJAXMessage(this, "Pelaksana pekerjaan belum ditentukan");
                return;
            }

            int ToDept = Convert.ToInt32(Session["ToDept"]);
            int StatusAproval = Convert.ToInt32(Session["StatusApv"]);


            /** Tambahan Beny **/
            Session["NoWO"] = txtNoWO.Text.Trim();
            string DeptID_PenerimaWO = this.DeptID(LabelIDWO.Text.ToString());

            WorkOrder_New worr = new WorkOrder_New();
            WorkOrderFacade_New worff = new WorkOrderFacade_New();
            worr = worff.DeptID_Nama(LabelIDWO.Text.ToString());
            /** end Tambahan **/

            if (StatusAproval == 9)
            {
                if (txtFinishDate.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Tanggal selesai belum ditentukan");
                    return;
                }
                else if (txtPelaksana5.Text == string.Empty || txtPelaksana5.Text == " ")
                {
                    DisplayAJAXMessage(this, "Pelaksana pekerjaan belum ditentukan");
                    return;
                }
            }

            Users users = (Users)Session["Users"];
            WorkOrder_New woR = new WorkOrder_New();
            WorkOrderFacade_New worF = new WorkOrderFacade_New();

            WorkOrder_New woR1 = new WorkOrder_New();
            WorkOrderFacade_New worF1 = new WorkOrderFacade_New();

            WorkOrder_New woR2 = new WorkOrder_New();
            WorkOrderFacade_New worF2 = new WorkOrderFacade_New();

            WorkOrder_New woR3 = new WorkOrder_New();
            WorkOrderFacade_New worF3 = new WorkOrderFacade_New();
            int NaikTarget = worF3.RetrieveStatusTarget(Convert.ToInt32(LabelIDWO.Text));

            int intResult = 0; int intResultT = 0;

            woR.DueDateWO = txtTargetSelesai.Text;
            woR2.DueDateWO = txtTargetSelesai.Text;
            woR.Pelaksana = txtPelaksana5.Text;
            woR2.Pelaksana = txtPelaksana5.Text;
            woR.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR1.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR2.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR2.Apv = StatusAproval;
            woR.FinishDate = txtFinishDate.Text;
            woR.UraianPerbaikan = txtPerbaikan.Text.Trim();
            woR.UserID = users.ID;
            woR.UserName = users.UserName;
            woR.ToDept = ToDept;
            woR.StatusApv = Convert.ToInt32(Session["StatusApv"]).ToString();
            woR.AreaWO = ddlArea.SelectedItem.ToString();
            woR.UnitKerjaID = users.UnitKerjaID;
            woR.DeptID_Users = Convert.ToInt32(Session["DeptID_Users"]);
            int DeptUser = Convert.ToInt32(Session["DeptID_Users"]);

            if (StatusAproval == 9)
            {
                woR.Apv = 4;
            }
            else { woR.Apv = StatusAproval; woR1.Apv = StatusAproval; }

            if (StatusAproval == 3 && users.DeptID == 7 && NaikTarget == 0
                || txtTargetSelesai.Text != string.Empty && StatusAproval == 3 && ToDept == 19 && NaikTarget == 0)
            {
                intResult = worF1.UpdateWO_Apv_L3HRD(woR1);
            }
            else if (StatusAproval == 3 && ToDept == 14 && NaikTarget == 0)
            {
                intResult = worF2.UpdateWO_Apv_L3IT(woR2);
            }
            else if (NaikTarget == 0)
            {
                intResult = worF.UpdateWO_Apv_L3(woR);
            }
            else if (NaikTarget > 0)
            {
                intResultT = worF.UpdateWO_ApvNaikTarget(woR);
                if (intResultT > 0)
                {
                    Response.Redirect("InputWorkOrder_New.aspx");
                }
            }

            if (intResult > -1)
            {
                int intResult2 = 0;

                if (StatusAproval == 3 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "SoftWare" && DeptUser != 23
                    || StatusAproval == 9 && users.DeptID == 7
                    || StatusAproval == 9 && users.DeptID == 19
                    || StatusAproval == 9 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "HardWare"
                    || StatusAproval == 9 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "SoftWare" && DeptUser == 23
                    || StatusAproval == 9 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "HardWare" && DeptUser == 23)
                { woR.Apv = 4; }
                else if (StatusAproval == 3 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "SoftWare" && DeptUser == 23
                    || StatusAproval == 3 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "HardWare" && DeptUser == 23)
                { woR.Apv = StatusAproval; }
                else if (StatusAproval == 9 && users.DeptID == 14 && ddlArea.SelectedItem.ToString() == "SoftWare" && DeptUser != 23)
                { woR.Apv = 5; }
                else { woR.Apv = StatusAproval; }

                /** Tambahan Beny **/
                if (txtFinishDate.Text != string.Empty)
                {
                    LoadKirimEmail_Selesai(Session["DeptID_Users"].ToString(), DeptID_PenerimaWO, worr.Dept_Pemberi, worr.Dept_Penerima);
                }

                intResult2 = worF.InsertLog_Apv(woR);

                Response.Redirect("InputWorkOrder_New.aspx");
            }

        }

        private string DeptID(string WOid)
        {
            WorkOrderFacade_New cn = new WorkOrderFacade_New();
            string result = "0";
            result = cn.RetieveDeptID(WOid);
            return result;
        }

        public void LoadKirimEmail_Selesai(string ToDept, string DeptID_PenerimaWO, string A, string B)
        {
            Users user = (Users)Session["users"];
            string NomorWO = Session["NoWO"].ToString();

            /** FROM **/
            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            mail.From = new MailAddress("system_support@grcboard.com", "WO -System Support-");

            /** TO **/
            WorkOrder_New WOd1 = new WorkOrder_New();
            WorkOrderFacade_New WOf1 = new WorkOrderFacade_New();
            ArrayList arrEmail1 = new ArrayList();
            arrEmail1 = WOf1.AccountEmail_TO(ToDept);

            foreach (WorkOrder_New ListEmail1 in arrEmail1)
            {
                mail.To.Add(new MailAddress(ListEmail1.AccountEmail.Trim(), ListEmail1.UserName.Trim()));
            }

            /** CC **/
            WorkOrder_New WOd2 = new WorkOrder_New();
            WorkOrderFacade_New WOf2 = new WorkOrderFacade_New();
            ArrayList arrEmail2 = new ArrayList();
            arrEmail2 = WOf2.AccountEmail_CC(DeptID_PenerimaWO);

            foreach (WorkOrder_New ListEmail2 in arrEmail2)
            {
                mail.CC.Add(new MailAddress(ListEmail2.AccountEmail.Trim(), ListEmail2.UserName.Trim()));
            }

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            mail.Subject = "Pemberitahuan Status WO " + ":" + NomorWO + " sudah Selesai di Kerjakan ";

            mail.Body += "Mohon untuk di lakukan Verifikasi hasil dari pekerjaan untuk Work Order di bawah ini : \n\r\n\r";
            mail.Body += "WO Nomor                  :   " + NomorWO.Trim() + "\n\r";
            mail.Body += "Department Pemberi WO     :   " + A.Trim() + "\n\r";
            mail.Body += "Department Pelaksana WO   :   " + B.Trim() + "\n\r";
            mail.Body += "Tgl Selesai WO            :   " + txtFinishDate.Text.ToString() + "\n\r";
            mail.Body += "Permintaan WO             :   " + txtUraian.Text.Trim() + "\n\r\n\r\n\r\n\r\n\r";
            //mail.Body += "Noted                     :\n\r";
            //mail.Body += "-------------------\n\r";
            //mail.Body += "- Mohon untuk di lakukan Serah Terima WO by System,  Max waktu untuk melakukan Serah Terima 3 HK. \n\r" +
            //             "- Jika Pada hari ke 4 belum juga melakukan Serah Terima WO, maka system akan melakukan auto closed utk WO tersebut. \n\r" +
            //             "- Penghitungan dimulai ketika email pemberitahuan ini diterima. \n\r\n\r";

            mail.Body += "Terima Kasih, \n\r";
            mail.Body += "PT. BANGUNPERKASA ADHITAMASENTRA \n\r";
            mail.Body += "- Ahlinya Papan Semen - \n\r";


            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "system_support@grcboard.com";
            NetworkCred.Password = "grc123!@#";
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            { }
        }

        protected void btnPrev_ServerClick(object sender, EventArgs e)
        {

            string[] ListSPP = noWO.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            int idx1 = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            int idx = int.Parse(ViewState["index"].ToString()) == 1 ? 1 : int.Parse(ViewState["index"].ToString()) - 1;
            int idx2 = int.Parse(ViewState["index"].ToString());
            btnPrev.Enabled = (idx > 1) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenPO.Count()) ? false : true;
            LoadListApprovalWO(ListOpenPO[idx - 1].ToString());

            //int CountWO = ListOpenPO.Count();
            //int IndexWO = int.Parse(ViewState["index"].ToString()) + 1;

            //txtCount.Text = IndexWO + "/" + CountWO; 

            ViewState["index"] = idx;

            int CountWO = ListOpenPO.Count();
            int IndexWO = int.Parse(ViewState["index"].ToString());

            txtCount.Text = IndexWO + "/" + CountWO;
        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string[] ListSPP = noWO.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();

                int CountWO = ListOpenPO.Count();
                int IndexWO = int.Parse(ViewState["index"].ToString()) + 1;
                txtCount.Text = IndexWO + "/" + CountWO;

                int idx5 = int.Parse(ViewState["index"].ToString());

                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;

                idx = (idx > ListOpenPO.Count() - 1) ? idx : idx;
                btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
                btnNext.Enabled = ((idx5 + 1) == ListOpenPO.Count()) ? false : true;

                try
                {
                    if (idx5 == 1)
                    { int idx2 = 1; ViewState["index2"] = idx2; }
                    else if (idx5 > 1)
                    { int idx2 = idx - 1; ViewState["index2"] = idx2; }

                    int idx3 = int.Parse(ViewState["index2"].ToString());

                    ViewState["index"] = idx;
                    LoadListApprovalWO(ListOpenPO[idx3].ToString());

                }
                catch
                {
                    LoadListApprovalWO(ListOpenPO[0].ToString());
                    ViewState["index"] = 0;
                }
                btnPrev.Enabled = (idx5 > 0) ? true : false;
            }
            catch { }
        }


        private void LoadListApprovalWO()
        {
            Users users = (Users)Session["Users"];
            int StApv = Convert.ToInt32(Session["StatusApv"]);
            int DeptIDHead = Convert.ToInt32(Session["DeptID"]);
            //string Pelaksana = ddlPelaksana.SelectedItem.ToString().Trim();

            WorkOrder_New WOP = new WorkOrder_New();
            WorkOrderFacade_New FacadeWOP = new WorkOrderFacade_New();
            string Pelaksana = FacadeWOP.RetrievePelaksana(users.ID);
            Session["Pelaksana"] = Pelaksana;

            WorkOrder_New WO = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();
            ArrayList arrWO1 = new ArrayList();
            arrWO1 = FacadeWO.RetrieveWOall(StApv, DeptIDHead, Pelaksana, users.UnitKerjaID);
            Session["arrWO1"] = arrWO1;

            foreach (WorkOrder_New WO1 in arrWO1)
            {
                if (WO1.NoWO != "")
                {
                    noWO.Value += WO1.NoWO + ",";
                }
            }

            noWO.Value = (noWO.Value != string.Empty) ? noWO.Value.Substring(0, (noWO.Value.Length - 1)) : "0";

        }

        private void LoadListApprovalWO(string NoWO)
        {
            int StApv = Convert.ToInt32(Session["StatusApv"]);
            int DeptIDHead = Convert.ToInt32(Session["DeptID"]);
            string pelaksana2 = Session["Pelaksana"].ToString();

            WorkOrder_New WO2 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO2 = new WorkOrderFacade_New();
            WO2 = FacadeWO2.RetrieveWOPerNoWO(NoWO, StApv, DeptIDHead);

            int DeptID_Users = WO2.DeptID_Users;
            Session["DeptID_Users"] = DeptID_Users;

            if (StApv == 3 && DeptIDHead == 19)
            {
                if (WO2.Pelaksana != string.Empty && WO2.TglTarget != string.Empty)
                {
                    LabelJudul.Visible = true;
                    LabelJudul.Text = "APPROVAL WORK ORDER";



                    LabelTargetSelesai.Visible = true; txtTargetSelesai.Visible = true;

                    PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true;
                    LabelPeminta.Visible = true; txtPeminta.Visible = true; LabelTglBuat.Visible = true; txtTglBuat.Visible = true;

                    LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true; LabelDept.Visible = true; btnSimpan.Visible = false; btnNew.Visible = false;
                    LabelPelaksana.Visible = false; txtPelaksana.Visible = false; txtTargetSelesai.ReadOnly = false; txtPelaksana.ReadOnly = false;

                    LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi2.Visible = false; btnUpdate.Visible = false;
                    btnList.Visible = false; LabelPerbaikan.Visible = false; txtPerbaikan.Visible = false; PanelPilihDept.Visible = false;
                    LabelPDept.Visible = false; ddlPDept.Visible = false; ddlArea.Visible = true; LabelAreaKerja.Visible = true;
                    PanelAreaKerja.Visible = true; PaneLinfo.Visible = false; LabelInfo.Visible = false;
                    lstBA.Visible = true;

                    btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = true; btnApprove.Visible = true;
                    btnUpdate.Visible = false; btnList.Visible = false; btnUpdate1.Visible = false; btnUpdateTarget.Visible = false;
                    btnFinish.Visible = false;


                    PanelSubArea.Visible = true; LabelSubArea.Visible = true; ddlSubArea.Visible = true;
                    LabelWajibisi4.Visible = false; LabelWajibisi3.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi1.Visible = false;

                    txtPelaksana.Visible = false; LabelPelaksana.Visible = false;

                    txtPermintaan.Visible = false; LabelTipe.Visible = false; txtTipe.Visible = false;
                    //ddlPelaksana.Items.Add(new ListItem(WO2.Pelaksana.Trim(), WO2.Pelaksana.Trim()));
                    LabelPelaksana.Visible = true; txtPelaksana5.Visible = true;
                    //ddlPelaksana.Visible = true; LabelPelaksana2.Visible = true; 
                    ddlPelaksana.Visible = false; LabelPelaksana2.Visible = false;
                    txtUraian.ReadOnly = true;

                    LoadShare(19);

                }
                else if (WO2.Pelaksana.Trim() == "" && WO2.DueDateWO.Trim() == "")
                {
                    LabelJudul.Visible = true;
                    LabelJudul.Text = "UPDATE PELAKSANA WORK ORDER";



                    PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true;
                    LabelPeminta.Visible = true; txtPeminta.Visible = true; LabelTglBuat.Visible = true; txtTglBuat.Visible = true;

                    LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true; LabelDept.Visible = true; LabelTargetSelesai.Visible = false;
                    txtTargetSelesai.Visible = false; btnSimpan.Visible = false; btnNew.Visible = false;
                    LabelPelaksana.Visible = false; txtPelaksana.Visible = false; txtTargetSelesai.ReadOnly = false; txtPelaksana.ReadOnly = false;

                    LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi2.Visible = false; btnUpdate.Visible = false;
                    btnList.Visible = false; LabelPerbaikan.Visible = false; txtPerbaikan.Visible = false; PanelPilihDept.Visible = false;
                    LabelPDept.Visible = false; ddlPDept.Visible = false; ddlArea.Visible = true; LabelAreaKerja.Visible = true;
                    PanelAreaKerja.Visible = true; PaneLinfo.Visible = false; LabelInfo.Visible = false;
                    lstBA.Visible = true;

                    btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = true; btnApprove.Visible = false;
                    btnUpdate.Visible = false; btnList.Visible = false; btnUpdate1.Visible = true; btnUpdateTarget.Visible = false;
                    btnFinish.Visible = false;


                    PanelSubArea.Visible = true; LabelSubArea.Visible = true; ddlSubArea.Visible = true;
                    LabelWajibisi4.Visible = true; LabelWajibisi3.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi1.Visible = false;
                    ddlPelaksana.Visible = true; LabelPelaksana2.Visible = true; LabelWajibisi4.Visible = true;
                    txtPelaksana.Visible = false;
                    txtPermintaan.Visible = false; LabelTipe.Visible = false; txtTipe.Visible = false;
                    LabelPelaksana.Visible = false; txtPelaksana5.Visible = false;
                    txtUraian.ReadOnly = true;

                    LoadShare(19);
                    LoadHead();

                    if (WO2.SubArea.Trim() == "NULL")
                    {
                        string SubArea1 = ""; Session["SubArea1"] = SubArea1;
                    }
                    else
                    {
                        string SubArea1 = WO2.SubArea.Trim(); Session["SubArea1"] = SubArea1;
                    }
                }
                else if (WO2.Pelaksana.Trim() != "" || WO2.DueDateWO.Trim() == "")
                {
                    LabelJudul.Visible = true;
                    LabelJudul.Text = "UPDATE TANGGAL TARGET WORK ORDER";
                    btnUpdate1.Visible = false; btnUpdateTarget.Visible = true;
                    LoadShare(19);
                }
                else
                { LoadHead(); LabelSubArea.Visible = true; ddlSubArea.Visible = true; LoadShare(19); }
            }
            else if (StApv == 9 && DeptIDHead == 19 || StApv == 9 && DeptIDHead == 14)
            {
                LabelJudul.Visible = true;
                LabelJudul.Text = "UPDATE TANGGAL SELESAI WORK ORDER";

                PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true; PanelSubArea.Visible = true;
                LabelSubArea.Visible = true; ddlSubArea.Visible = true;
                LabelPeminta.Visible = true; txtPeminta.Visible = true;
                LabelTglBuat.Visible = true; txtTglBuat.Visible = true;
                LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true;
                LabelDept.Visible = true;

                LabelFinishDate.Visible = true; txtFinishDate.Visible = true; LabelWajibisi3.Visible = true;

                btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = false; btnApprove.Visible = true;
                btnUpdate.Visible = false; btnList.Visible = false; btnUpdate1.Visible = false; btnUpdateTarget.Visible = false;
                btnFinish.Visible = false;

                txtPelaksana.ReadOnly = false; LabelWajibisiPerbaikan.Visible = true;
                LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false;
                ddlArea.Visible = true; LabelAreaKerja.Visible = true; PanelAreaKerja.Visible = true;
                PanelPilihDept.Visible = false; LabelPDept.Visible = false; ddlPDept.Visible = false;

                LabelTargetSelesai.Visible = true;
                txtTargetSelesai.Visible = true;

                LabelWajibisi1.Visible = false; LabelPelaksana.Focus();
                ddlPelaksana.Visible = false; LabelPelaksana2.Visible = false; LabelWajibisi2.Visible = false;
                txtPelaksana.Visible = false; LabelPelaksana.Visible = true; LabelWajibisi4.Visible = false;
                txtPelaksana5.Visible = true; txtPelaksana5.ReadOnly = false; txtUraian.ReadOnly = true;


                if (DeptIDHead == 14)
                {
                    txtPermintaan.Visible = true; LabelTipe.Visible = true; txtTipe.Visible = true;
                }
                else { txtPermintaan.Visible = false; LabelTipe.Visible = false; txtTipe.Visible = false; }

            }
            else if (StApv == 9 && DeptIDHead == 7)
            {
                LabelJudul.Visible = true;

                if (WO2.Apv == 2)
                {
                    LabelJudul.Text = "UPDATE TARGET SELESAI WORK ORDER";
                    txtPerbaikan.Visible = false; LabelPerbaikan.Visible = false;
                    LabelFinishDate.Visible = false; txtFinishDate.Visible = false;
                    btnUpdateTarget.Visible = true; btnUpdate.Visible = false;

                }
                else if (WO2.Apv == 3)
                {
                    LabelJudul.Text = "UPDATE TANGGAL SELESAI WORK ORDER";
                    txtPerbaikan.Visible = true; LabelPerbaikan.Visible = true;
                    LabelFinishDate.Visible = true; txtFinishDate.Visible = true;
                    btnUpdate.Visible = true; btnUpdateTarget.Visible = false;

                }

                PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true; PanelSubArea.Visible = true;
                LabelSubArea.Visible = true; ddlSubArea.Visible = true;
                LabelPeminta.Visible = true; txtPeminta.Visible = true;
                LabelTglBuat.Visible = true; txtTglBuat.Visible = true;
                LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true;
                LabelDept.Visible = true;

                LabelWajibisi3.Visible = false;

                btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = false; btnApprove.Visible = false;
                btnList.Visible = false; btnUpdate1.Visible = false; btnFinish.Visible = false;


                txtPelaksana.ReadOnly = false; LabelWajibisiPerbaikan.Visible = false;
                LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false;
                ddlArea.Visible = true; LabelAreaKerja.Visible = true; PanelAreaKerja.Visible = true;
                PanelPilihDept.Visible = false; LabelPDept.Visible = false; ddlPDept.Visible = false;

                LabelTargetSelesai.Visible = true;
                txtTargetSelesai.Visible = true;

                LabelWajibisi1.Visible = false; LabelPelaksana.Focus();
                ddlPelaksana.Visible = false; LabelPelaksana2.Visible = false; LabelWajibisi2.Visible = false;
                txtPelaksana.Visible = false; LabelPelaksana.Visible = true; LabelWajibisi4.Visible = false;
                txtPelaksana5.ReadOnly = true; txtUraian.ReadOnly = true; txtPelaksana5.Visible = true;

            }
            else if (StApv == 8 && DeptIDHead == 19)
            {
                LabelJudul.Visible = true;
                LabelJudul.Text = "UPDATE TANGGAL TARGET SELESAI WORK ORDER";

                PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true;
                PanelSubArea.Visible = true; LabelSubArea.Visible = true; ddlSubArea.Visible = true;

                LabelPeminta.Visible = true; txtPeminta.Visible = true;
                LabelTglBuat.Visible = true; txtTglBuat.Visible = true;
                LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true;
                LabelDept.Visible = true;
                LabelFinishDate.Visible = false; txtFinishDate.Visible = false; LabelWajibisi3.Visible = false;
                LabelPerbaikan.Visible = false; txtPerbaikan.Visible = false;

                btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = false;
                btnApprove.Visible = false; btnUpdate.Visible = false; btnList.Visible = false;
                btnUpdate1.Visible = false; btnUpdateTarget.Visible = true; btnFinish.Visible = false;

                txtPelaksana.Visible = false; LabelPelaksana.Visible = false; LabelWajibisi4.Visible = false;
                LabelWajibisi1.Visible = true; LabelWajibisi2.Visible = false;
                ddlArea.Visible = true; LabelAreaKerja.Visible = true; PanelAreaKerja.Visible = true;
                PanelPilihDept.Visible = false; LabelPDept.Visible = false; ddlPDept.Visible = false;

                LabelTargetSelesai.Visible = true; LabelWajibisiPerbaikan.Visible = false;
                txtTargetSelesai.Visible = true;

                ddlPelaksana.Visible = true; LabelPelaksana2.Visible = true; LabelWajibisi2.Visible = false;
                txtUraian.ReadOnly = true;
            }
            else if (StApv == 3 && DeptIDHead == 14 || StApv == 3 && DeptIDHead == 7)
            {
                LabelJudul.Visible = true;
                LabelJudul.Text = "APPROVAL WORK ORDER";



                LabelTargetSelesai.Visible = true; txtTargetSelesai.Visible = true;

                PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true;
                LabelPeminta.Visible = true; txtPeminta.Visible = true; LabelTglBuat.Visible = true; txtTglBuat.Visible = true;

                LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true; LabelDept.Visible = true; btnSimpan.Visible = false; btnNew.Visible = false;
                LabelPelaksana.Visible = false; txtPelaksana.Visible = false; txtTargetSelesai.ReadOnly = false; txtPelaksana.ReadOnly = false;

                LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false; LabelWajibisi2.Visible = false; btnUpdate.Visible = false;
                btnList.Visible = false; LabelPerbaikan.Visible = false; txtPerbaikan.Visible = false; PanelPilihDept.Visible = false;
                LabelPDept.Visible = false; ddlPDept.Visible = false; ddlArea.Visible = true; LabelAreaKerja.Visible = true;
                PanelAreaKerja.Visible = true; PaneLinfo.Visible = false; LabelInfo.Visible = false;
                lstBA.Visible = true;

                btnSimpan.Visible = false; btnNew.Visible = false; btnUnApprove.Visible = true; btnApprove.Visible = true;
                btnUpdate.Visible = false; btnList.Visible = false; btnUpdate1.Visible = false; btnUpdateTarget.Visible = false;
                btnFinish.Visible = false;

                PanelSubArea.Visible = false; LabelSubArea.Visible = false; ddlSubArea.Visible = false;
                LabelWajibisi4.Visible = false; LabelWajibisi3.Visible = false; LabelWajibisi2.Visible = true; LabelWajibisi1.Visible = true;
                ddlPelaksana.Visible = false; LabelPelaksana2.Visible = false;
                txtPelaksana.Visible = false; LabelPelaksana.Visible = true;
                txtPelaksana5.Visible = true; txtPelaksana5.ReadOnly = false; txtUraian.ReadOnly = true;
                //txtPermintaan.Visible = true; LabelTipe.Visible = true; txtTipe.Visible = true;
                //ddlPelaksana.Items.Add(new ListItem(WO2.Pelaksana.Trim(), WO2.Pelaksana.Trim()));
                txtPelaksana5.Focus();



                if (DeptIDHead == 14)
                {
                    txtPermintaan.Visible = true; LabelTipe.Visible = true; txtTipe.Visible = true;
                }
                else { txtPermintaan.Visible = false; LabelTipe.Visible = false; txtTipe.Visible = false; }

                if (WO2.AreaWO.Trim() == "SoftWare")
                {
                    PanelKonfirmasi.Visible = true;
                    if (WO2.PlantID == 1)
                    {
                        txtPlant.Text = "KARAWANG";
                    }
                    else if (WO2.PlantID == 7)
                    {
                        txtPlant.Text = "CITEUREUP";
                    }
                    txtKonfirmasi.Text = WO2.StatusWO;
                }


            }

            ddlArea.Items.Clear();
            ddlSubArea.Items.Clear();
            //ddlPelaksana.Items.Clear();

            if (WO2.SubArea.Trim() == "NULL")
            {
                string SubArea1 = ""; Session["SubArea1"] = SubArea1;
            }
            else
            {
                string SubArea1 = WO2.SubArea.Trim(); Session["SubArea1"] = SubArea1;
            }

            string SubAre = Session["SubArea1"].ToString();

            string Minta = WO2.Permintaan.Trim();

            LabelIDWO.Text = WO2.WOID.ToString();
            txtNoWO.Text = WO2.NoWO;
            txtPeminta.Text = WO2.DeptName;
            txtTglBuat.Text = WO2.TglDibuat;
            txtTglDisetujui.Text = WO2.TglDisetujui;
            txtUraian.Text = WO2.UraianPekerjaan;
            txtPelaksana5.Text = WO2.Pelaksana;
            ddlArea.Items.Add(new ListItem(WO2.AreaWO.Trim(), WO2.AreaWO.Trim()));
            txtKonfirmasi.Text = WO2.StatusWO; txtTargetSelesai.Text = WO2.TglTarget;
            ddlPelaksana.Items.Add(new ListItem(WO2.Pelaksana.Trim(), WO2.Pelaksana.Trim()));
            ddlSubArea.Items.Add(new ListItem(SubAre.Trim(), SubAre.Trim()));
            txtTipe.Text = WO2.TipeWO;
            txtPermintaan.Text = WO2.Permintaan;


            string Area = WO2.AreaWO.Trim();

            if (WO2.ToDept == 14 && Area == "SoftWare" && StApv == 3)
            {
                PanelKonfirmasi.Visible = true; LabelPlant.Visible = true; txtPlant.Visible = true;
                LabelKonfirmasi.Visible = true; txtKonfirmasi.Visible = true;

                if (WO2.PlantID == 1)
                {
                    txtPlant.Text = "KARAWANG";
                }
                else if (WO2.PlantID == 1)
                {
                    txtPlant.Text = "CITEUREUP";
                }

            }
            //if (WO2.PlantID == 1)
            //{
            //    txtPlant.Text = "KARAWANG";
            //}
            //else if (WO2.PlantID == 1)
            //{
            //    txtPlant.Text = "CITEUREUP";
            //}


            int ToDept = WO2.ToDept;
            Session["ToDept"] = ToDept;

            if (StApv != 0)
            {
                if (StApv == 3)
                {
                    //txtTargetSelesai.Text = "";
                }
                else if (StApv == 9)
                {
                    txtTargetSelesai.Text = WO2.TglTarget;
                }
            }

            WorkOrder_New WO3 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO3 = new WorkOrderFacade_New();
            ArrayList arrWO3 = new ArrayList();
            arrWO3 = FacadeWO3.Retrieve_ListWO_Lampiran(NoWO);
            Session["arrWO3"] = arrWO3;

            lstBA.DataSource = arrWO3;
            lstBA.DataBind();
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            WorkOrder_New wrk = (WorkOrder_New)e.Item.DataItem;
            System.Web.UI.WebControls.Image view = (System.Web.UI.WebControls.Image)e.Item.FindControl("view");
            view.Attributes.Add("onclick", "PreviewWO('" + wrk.IDLampiran.ToString() + "')");
        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        { }



        private void LoadDeptUsers()
        {
            Users users = (Users)Session["Users"];
            string DeptIDUsers = users.DeptID.ToString();
            string UserName = users.UserName;
            WorkOrder_New DomainWO = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();
            string NamaDept = FacadeWO.RetrieveNamaDept(DeptIDUsers);
            Session["DeptID"] = DeptIDUsers;
            Session["UserName"] = UserName;
        }

        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string StsApv = Session["StatusApv"].ToString();

            #region Department Penerima WO
            if (ddlPDept.SelectedItem.ToString().Trim() == "IT")
            {
                if (ddlTipeWO.SelectedValue != "0")
                {
                    string TipeWO1 = ddlTipeWO.SelectedItem.ToString().Trim(); Session["TipeWO"] = TipeWO1;
                }
                else
                {
                    string TipeWO1 = ""; Session["TipeWO"] = TipeWO1;
                }

                if (RBS.Checked == true)
                {
                    if (ddlSoftware.SelectedValue != "0")
                    {
                        string Permintaan1 = ddlSoftware.SelectedItem.ToString().Trim(); Session["Permintaan"] = Permintaan1;
                    }
                    else
                    {
                        string Permintaan1 = null; Session["Permintaan"] = Permintaan1;
                    }
                }


                if (RBH.Checked == true)
                {
                    if (ddlHardware.SelectedValue != "0")
                    {
                        string Permintaan1 = ddlHardware.SelectedItem.ToString().Trim(); Session["Permintaan"] = Permintaan1;
                    }
                    else
                    {
                        string Permintaan1 = ""; Session["Permintaan"] = Permintaan1;
                    }
                }
            }
            else if (ddlPDept.SelectedItem.ToString() != "IT")
            {
                string Permintaan1 = ""; Session["Permintaan"] = Permintaan1;
                string TipeWO1 = ""; Session["TipeWO"] = TipeWO1;
            }
            #endregion

            string Permintaan = Session["Permintaan"].ToString();
            string TipeWO = Session["TipeWO"].ToString();

            #region Filter Pilihan WO
            if (ddlPDept.SelectedItem.ToString().Trim() == "IT")
            {
                if (ddlTipeWO.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, " Tipe WO Wajib Di Pilih ");
                    return;
                }

                if (RBH.Checked == false && RBS.Checked == false)
                {
                    DisplayAJAXMessage(this, " Jenis Pekerjaan Wajib Di Pilih ");
                    return;
                }

                if (RBH.Checked == true)
                {
                    if (ddlHardware.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, " Permintaan Wajib Di Pilih ");
                        return;
                    }
                }

                if (RBS.Checked == true)
                {
                    if (ddlSoftware.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, " Permintaan Wajib Di Pilih ");
                        return;
                    }
                }


                string Alasan = txtUraian.Text;
                if (Alasan == string.Empty)
                {
                    DisplayAJAXMessage(this, " Uraian Pekerjaan Wajib Di Isi ");
                    return;
                }
            }
            else if (ddlPDept.SelectedItem.ToString().Trim() == "HRD GA" & users.DeptID == 7)
            {
                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("General", "0"));
            }
            else
            {
                if (ddlArea.SelectedItem.ToString().Trim() == "Zona 2" || ddlArea.SelectedItem.ToString().Trim() == "Zona 3")
                {
                    if (ddlSubArea.SelectedValue == "0")
                    {
                        DisplayAJAXMessage(this, " Sub Area Wajib Di Isi ");
                        return;
                    }
                }

                string NamaDept = ddlPDept.SelectedValue.ToString();
                if (NamaDept == "0")
                {
                    DisplayAJAXMessage(this, " Department Wajib Di Pilih ");
                    return;
                }

                string AreaWO = ddlArea.SelectedValue;
                if (AreaWO == "0")
                {
                    DisplayAJAXMessage(this, " Area Pekerjaan Wajib Di Pilih ");
                    return;
                }

                string Alasan = txtUraian.Text;
                if (Alasan == string.Empty)
                {
                    DisplayAJAXMessage(this, " Uraian Pekerjaan Wajib Di Isi ");
                    return;
                }
            }
            #endregion

            int Bln = DateTime.Now.Month;

            if (Bln < 10)
            { string Bulan = "0" + Bln; Session["Bulan1"] = Bulan; }
            else
            { string Bulan = Convert.ToInt32(Bln).ToString(); Session["Bulan1"] = Bulan; }

            string Bulan1 = Session["Bulan1"].ToString();
            int DeptIDUsers = users.DeptID;

            int IDDept = Convert.ToInt32(ddlPDept.SelectedValue);
            WorkOrder_New DomainWO1 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO1 = new WorkOrderFacade_New();
            DomainWO1 = FacadeWO1.RetrieveDeptiD(IDDept);

            WorkOrder_New DomainWO12 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO12 = new WorkOrderFacade_New();
            DomainWO12 = FacadeWO12.RetrieveDeptiDUsers(DeptIDUsers);

            int DeptIDPenerima = DomainWO1.DeptIDP;
            //int DeptIDUsers = users.DeptID;

            string Alias = DomainWO1.Alias.Trim();
            string Alias2 = DomainWO12.Alias.Trim();

            if (DeptIDPenerima == 14)
            {
                if (users.UnitKerjaID == 1)
                {
                    if (RBS.Checked == true)
                    { string Flag5 = "S"; Session["Flag5"] = Flag5; string Kode = "C"; Session["Kode"] = Kode; }
                    else if (RBH.Checked == true)
                    { string Flag5 = "H"; Session["Flag5"] = Flag5; string Kode = "C"; Session["Kode"] = Kode; }
                }
                else if (users.UnitKerjaID == 7)
                {
                    if (RBS.Checked == true)
                    { string Flag5 = "S"; Session["Flag5"] = Flag5; string Kode = "K"; Session["Kode"] = Kode; }
                    else if (RBH.Checked == true)
                    { string Flag5 = "H"; Session["Flag5"] = Flag5; string Kode = "K"; Session["Kode"] = Kode; }
                }
                else if (users.UnitKerjaID == 13)
                {
                    if (RBS.Checked == true)
                    { string Flag5 = "S"; Session["Flag5"] = Flag5; string Kode = "J"; Session["Kode"] = Kode; }
                    else if (RBH.Checked == true)
                    { string Flag5 = "H"; Session["Flag5"] = Flag5; string Kode = "J"; Session["Kode"] = Kode; }
                }
            }
            //else if (DeptIDPenerima == 7 & users.DeptID == 7)
            //{ string Flag5 = "10"; Session["Flag5"] = Flag5; }
            else
            { string Flag5 = "0"; Session["Flag5"] = Flag5; }

            string FlagKode = Session["Flag5"].ToString();
            int result = 0;
            WorkOrder_New DomainWO = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();

            string Thn = DateTime.Now.Year.ToString();
            string Tahun = Thn.Substring(2, 2);
            int NoUrut = FacadeWO.RetrieveNoWO(Bulan1, Thn, DeptIDPenerima, FlagKode);

            if (NoUrut > 0)
            {
                int Count1 = NoUrut;
                if (Count1 < 9)
                {
                    int CountNumber = 1 + Count1;
                    string CountNo = "00" + CountNumber;
                    Session["CountNo"] = CountNo;
                    Session["CountNumber"] = CountNumber;
                }
                else if (Count1 > 8 && Count1 < 100)
                {
                    int CountNumber = 1 + Count1;
                    Session["CountNo"] = "0" + CountNumber;
                    Session["CountNumber"] = CountNumber;
                }
                else if (Count1 >= 100)
                {
                    int CountNumber = 1 + Count1;
                    string CountNo = CountNumber.ToString();
                    Session["CountNo"] = CountNo;
                    Session["CountNumber"] = CountNumber;
                }

                int NomorUrut1 = Convert.ToInt32(Session["CountNumber"]);
                string NomorUrutan = Session["CountNo"].ToString();

                DomainWO.Bulan = Convert.ToInt32(Bulan1);
                DomainWO.Tahun = Convert.ToInt32(Thn);
                DomainWO.NomorUrut = NomorUrut1;
                DomainWO.Flag = FlagKode;

                if (DeptIDPenerima == 14)
                {
                    string Kode = Session["Kode"].ToString();
                    if (RBS.Checked == true)
                    {
                        string NomorWO3 = "WO" + "-" + Alias + "-" + Kode + NomorUrutan + Bulan1 + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }
                    else if (RBH.Checked == true)
                    {
                        string NomorWO3 = "WO" + "-" + Alias + "-" + NomorUrutan + Bulan1 + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }
                }
                else if (DeptIDPenerima == 19)
                {
                    //string NomorWO3 = "WO" + "-" + Alias + "-" + NomorUrutan + Bulan1 + Tahun;
                    string NomorWO3 = NomorUrutan + "/" + Bulan1 + "/" + Alias2 + "/" + Tahun;
                    Session["NomorWO3"] = NomorWO3;
                }
                else if (DeptIDPenerima == 7)
                {
                    string NomorWO3 = "WO" + "-" + Alias + "-" + NomorUrutan + Bulan1 + Tahun;
                    //string NomorWO3 = NomorUrutan + "/" + Bulan1 + "/" + Alias2 + "/" + Tahun;
                    Session["NomorWO3"] = NomorWO3;
                }

                string NomorWO1 = Session["NomorWO3"].ToString();
                Session["NomorWO1"] = NomorWO1;
                DomainWO.DeptIDP = DeptIDPenerima;


                result = FacadeWO.updateNo(DomainWO);
            }
            else
            {
                DomainWO.Bulan = int.Parse(Bulan1);
                DomainWO.Tahun = int.Parse(Thn);
                DomainWO.DeptIDP = DeptIDPenerima;

                if (DeptIDPenerima == 14 && RBS.Checked == true)
                {
                    string Flag = "S"; Session["Flag"] = Flag;
                    if (users.UnitKerjaID == 1)
                    {
                        string Kode = "C"; Session["Kode"] = Kode;
                    }
                    else if (users.UnitKerjaID == 7)
                    {
                        string Kode = "K"; Session["Kode"] = Kode;
                    }
                    else if (users.UnitKerjaID == 13)
                    {
                        string Kode = "J"; Session["Kode"] = Kode;
                    }
                }
                else if (DeptIDPenerima == 14 && RBH.Checked == true)
                {
                    string Flag = "H"; Session["Flag"] = Flag; string Kode = "-"; Session["Kode"] = Kode;
                }
                else
                {
                    string Flag = "0"; Session["Flag"] = Flag; string Kode = "-"; Session["Kode"] = Kode;
                }

                DomainWO.Flag = Session["Flag"].ToString();
                int result1 = 0;
                result1 = FacadeWO.insertNo(DomainWO);

                if (result1 > 0)
                {
                    string Kode = Session["Kode"].ToString();

                    if (DeptIDPenerima == 14 && RBS.Checked == true)
                    {
                        string NomorWO3 = "WO" + "-" + Alias + "-" + Kode + "001" + Bulan1 + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }
                    else if (DeptIDPenerima == 14 && RBH.Checked == true)
                    {
                        string NomorWO3 = "WO" + "-" + Alias + "-" + "001" + Bulan1 + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }
                    else if (DeptIDPenerima == 19)
                    {
                        //string NomorWO3 = "WO" + "-" + Alias + "-" + "001" + Bulan1 + Tahun;
                        string NomorWO3 = "001" + "/" + Bulan1 + "/" + Alias2 + "/" + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }
                    else if (DeptIDPenerima == 7)
                    {
                        string NomorWO3 = "WO" + "-" + Alias + "-" + "001" + Bulan1 + Tahun;
                        Session["NomorWO3"] = NomorWO3;
                    }

                    string NomorWO = Session["NomorWO3"].ToString();
                    Session["NomorWO1"] = NomorWO;
                }
            }

            if (DeptIDPenerima == 14)
            {
                if (RBH.Checked == true)
                { string AreaWO1 = "HardWare"; Session["AreaWO1"] = AreaWO1; }
                else if (RBS.Checked == true)
                { string AreaWO1 = "SoftWare"; Session["AreaWO1"] = AreaWO1; }
            }
            else
            { string AreaWO1 = ddlArea.SelectedItem.ToString(); Session["AreaWO1"] = AreaWO1; }

            string Area = Session["AreaWO1"].ToString();
            string NomorWO2 = Session["NomorWO1"].ToString();

            string Nama = Session["UserName"].ToString();
            int DeptID = Convert.ToInt32(Session["DeptiD"]);

            // Rubah 2 Plant Sama : 19 Agustus 2018
            if (DeptIDPenerima == 19 && DeptIDUsers == 2)
            {
                if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
                {
                    if (ddlArea.SelectedItem.ToString().Trim() == "Zona 1")
                    {
                        string SubArea = ""; Session["SubArea"] = SubArea;
                    }
                    else { string SubArea = ddlSubArea.SelectedItem.ToString().Trim(); Session["SubArea"] = SubArea; }
                }
                // Rubah dari 1 ke 11, tidak digunakan lagi
                else if (users.UnitKerjaID == 11)
                {
                    string SubArea = "NULL"; Session["SubArea"] = SubArea;
                }
            }
            else
            {
                string SubArea = ""; Session["SubArea"] = SubArea;
            }

            string SArea = Session["SubArea"].ToString();
            string PelaksanaTemp = "";
            DomainWO.SubArea = SArea;
            DomainWO.AreaWO = Area;
            DomainWO.UraianPekerjaan = txtUraian.Text;

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID = 19;
            }
            if (DeptIDPenerima == 7)
            {
                DomainWO.Pelaksana = "SPV GA";
            }
            else { DomainWO.Pelaksana = PelaksanaTemp.Trim(); }

            // Tambahan Baru
            if (DeptIDPenerima == 7 & users.DeptID == 7)
            {
                WorkOrder_New domain10 = new WorkOrder_New();
                WorkOrderFacade_New facade10 = new WorkOrderFacade_New();
                string NamaSubDept = facade10.RetrieveNamaSub(users.ID); Session["NamaSubDept"] = NamaSubDept;

                DomainWO.NamaSubDept = NamaSubDept.Trim();
            }
            else { DomainWO.NamaSubDept = ""; Session["NamaSubDept"] = ""; }

            string NamaSub = Session["NamaSubDept"].ToString().Trim();

            DomainWO.DeptID_Users = DeptID;
            DomainWO.NoWO = NomorWO2;
            DomainWO.CreatedBy = Nama;
            DomainWO.PlantID = users.UnitKerjaID;
            DomainWO.Permintaan = Permintaan;
            DomainWO.TipeWO = TipeWO;
            Session["Nomor"] = NomorWO2;
            DomainWO.WOID = 0;

            int result2 = 0;

            result2 = FacadeWO.insertWO(DomainWO);

            if (result2 > -1)
            {
                int result3 = 0;

                if (users.DeptID == 11)
                {
                    if (StsApv == "2")
                    {
                        DomainWO.Apv = 2;
                    }
                    else if (StsApv == "3") { DomainWO.Apv = 3; }

                    int WOID = result2;
                    DomainWO.WOID = WOID;
                    DomainWO.ToDept = DeptIDPenerima;
                    DomainWO.UserName = users.UserName;
                    DomainWO.UserID = users.ID;
                    result3 = FacadeWO.InsertLog_Apv(DomainWO);
                }
                else if (users.DeptID == 7 && NamaSub.Trim() == "HSE" || NamaSub == "Legal")
                {
                    int WOID = result2;
                    DomainWO.WOID = result2;
                    DomainWO.Apv = 2;
                    DomainWO.ToDept = DeptIDPenerima;
                    DomainWO.UserName = users.UserName;
                    DomainWO.UserID = users.ID;
                    result3 = FacadeWO.InsertLog_Apv(DomainWO);
                }

                #region Kirim Email
                KirimEmail(users.DeptID, users.ID, Convert.ToInt32(StsApv));
                #endregion

                txtNoWO.Text = NomorWO2;
                btnSimpan.Enabled = false;
                txtUraian.ReadOnly = true;
                txtNoWO.ReadOnly = true;

                DisplayAJAXMessage(this, " Proses Simpan Berhasil ");
                return;
            }

        }

        public void ClearForm()
        {

        }


        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            int StApv = Convert.ToInt32(Session["StatusApv"]);
            if (StApv > 0)
            {
                if (StApv == 3 || StApv == 9)
                {
                    Response.Redirect("LaporanWorkOrder_New.aspx");
                }
            }
            else
            {
                Response.Redirect("ListWorkOrder_New.aspx");
            }
            //Response.Redirect("ListWorkOrder.aspx");
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            txtNoWO.Text = string.Empty; LabelAreaKerja.Visible = false; ddlArea.Visible = false;
            txtUraian.Text = string.Empty; LabelSubArea.Visible = false; ddlSubArea.Visible = false;
            txtNoWO.Enabled = true;
            txtUraian.Enabled = true;
            txtUraian.ReadOnly = false;
            ddlArea.Enabled = false;
            btnSimpan.Enabled = true;
            //LoadDDL_Area();
            ddlPDept.Items.Clear();
            ddlPDept.Items.Add(new ListItem("---- Pilih Dept ----", "0"));
            LoadDDL_Dept();
            ddlArea.Items.Clear();
            ddlSubArea.Items.Clear();
            ddlPDept.Enabled = true;
            RBH.Visible = false; RBS.Visible = false;
            PanelIT.Visible = false; LabelJenis.Visible = false;
            LabelTipeWO.Visible = false; ddlTipeWO.Visible = false; PanelTipe.Visible = false;
            PanelHardware.Visible = false; PanelSoftware.Visible = false;

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadListApprovalWO(txtSearch.Text.Trim());
        }

        private void LoadDept()
        {

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        public void LoadGrid()
        {


        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        { }

        protected void RB2_CheckedChanged(object sender, EventArgs e)
        { }

        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_ServerClick(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_ServerClick(null, null);
            }
            else
            {
                Response.Redirect("ListWorkOrder_New.aspx");
            }
        }

        protected void btnUnApprove_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            WorkOrder_New woR = new WorkOrder_New();
            WorkOrderFacade_New worF = new WorkOrderFacade_New();
            int intResult = 0;
            woR.AlasanCancel = Session["AlasanCancel"].ToString();
            woR.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR.UserName = users.UserName;
            intResult = worF.UpdateCancel(woR);

            if (intResult > -1)
            {
                Response.Redirect("InputWorkOrder_New.aspx");
            }



        }

        protected void RBH_CheckedChanged(object sender, EventArgs e)
        {
            RBS.Checked = false; LoadPermintaan();
            PanelHardware.Visible = true; LabelHardware.Visible = true; ddlHardware.Visible = true;
            PanelSoftware.Visible = false; LabelSoftware.Visible = false; ddlSoftware.Visible = false;
        }

        protected void RBS_CheckedChanged(object sender, EventArgs e)
        {
            RBH.Checked = false; LoadPermintaan();
            PanelSoftware.Visible = true; LabelSoftware.Visible = true; ddlSoftware.Visible = true;
            PanelHardware.Visible = false; LabelHardware.Visible = false; ddlHardware.Visible = false;
        }

        protected void ddlHardware_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlSoftware_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void LoadPermintaan()
        {
            if (RBH.Checked == true)
            {
                string PermintaanH = "Hardware"; Session["PermintaanH"] = PermintaanH;
            }
            else if (RBS.Checked == true)
            {
                string PermintaanH = "Software"; Session["PermintaanH"] = PermintaanH;
            }

            string Permintaan = Session["PermintaanH"].ToString();

            ArrayList arrWOP = new ArrayList();
            WorkOrderFacade_New WOF1 = new WorkOrderFacade_New();
            arrWOP = WOF1.GetPermintaan(Permintaan.Trim());

            if (Permintaan == "Hardware")
            {
                ddlHardware.Items.Clear();
                ddlHardware.Items.Add(new ListItem("--Pilih --", "0"));
                foreach (WorkOrder_New pl in arrWOP)
                {
                    ddlHardware.Items.Add(new ListItem(pl.Permintaan, pl.ID.ToString()));
                }
            }
            else if (Permintaan == "Software")
            {
                ddlSoftware.Items.Clear();
                ddlSoftware.Items.Add(new ListItem("--Pilih --", "0"));
                foreach (WorkOrder_New pl in arrWOP)
                {
                    ddlSoftware.Items.Add(new ListItem(pl.Permintaan, pl.ID.ToString()));
                }
            }

        }
        public void KirimEmail(int DeptID, int UserID, int StsApv)
        {
            Users user = (Users)Session["users"];
            WorkOrder_New wn = new WorkOrder_New();
            WorkOrderFacade_New wnF = new WorkOrderFacade_New();
            wn = wnF.RetrieveDataEmailPasInput(DeptID, UserID, StsApv);
            string AlamatEmail = wn.AccountEmail.Trim();
            string NomorWO = Session["Nomor"].ToString();
            if (AlamatEmail == string.Empty)
                return;

            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            mail.From = new MailAddress("system_support@grcboard.com", "WO -System Support-");
            mail.To.Add(new MailAddress(AlamatEmail));
            //mail.To.Add(new MailAddress("beny@grcboard.com")); 
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            mail.Subject = "Permohonan Approval Work Order";

            mail.Body += "Mohon Approval untuk Work Order di bawah ini : \n\r\n\r";
            mail.Body += "WO Nomor         : " + NomorWO.Trim() + "\n\r";
            mail.Body += "Tanggal Buat     : " + DateTime.Now.ToString("dd-MM-yyyy") + "\n\r";
            mail.Body += "Di Buat oleh     : " + user.UserName.Trim() + "\n\r";
            mail.Body += "Dept. Tujuan     : " + ddlPDept.SelectedItem.ToString().Trim() + "\n\r";
            mail.Body += "Status Approval  : Open \n\r\n\r";
            mail.Body += "Permintaan WO    : " + txtUraian.Text.Trim() + "\n\r\n\r\n\r ";


            mail.Body += "Terima Kasih, \n\r\n\r\n\r";
            mail.Body += "PT. BANGUNPERKASA ADHITAMASENTRA \n\r";
            mail.Body += "- Ahlinya Papan Semen - \n\r";


            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "system_support@grcboard.com";
            NetworkCred.Password = "grc123!@#";
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            { }
        }
        private void clearForm()
        { }

        private void LoadShare(int DeptID)
        {
            Users user = (Users)Session["users"];
            WorkOrder_New DomainWork = new WorkOrder_New();
            WorkOrderFacade_New FacadeWork = new WorkOrderFacade_New();

            int Share = FacadeWork.cekShare(DeptID, user.UnitKerjaID);

            if (Share > 0)
            {
                if (user.UnitKerjaID == 1)
                {
                    string PlantNama = " Citeureup "; Session["Plant"] = PlantNama;
                }
                else if (user.UnitKerjaID == 7)
                {
                    string PlantNama = " Karawang "; Session["Plant"] = PlantNama;
                }
                else if (user.UnitKerjaID == 13)
                {
                    string PlantNama = " Jombang "; Session["Plant"] = PlantNama;
                }

                string NamaPlant = Session["Plant"].ToString();

                LabelShare.Visible = true; PaneLShare.Visible = true;
                LabelShare.Text = "Ada share WO dari " + NamaPlant + "sebanyak = " + " " + Share;
            }

        }

        protected void RBShare_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("ListWorkOrder_New.aspx");
        }

    }
}