using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormApprovalPM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count == 0)
                {
                    Response.Redirect("~/ISO_UPD/FormApprovalPM.aspx");
                }
                else
                {
                    Global.link = "~/Default.aspx";
                    Users user = (Users)Session["Users"];
                    int IDUser = user.ID;
                    ISO_Upd sp = new ISO_Upd();
                    ISO_UpdFacade spF = new ISO_UpdFacade();
                    sp = spF.RetrieveForApvKe(IDUser);
                    if (sp.StatusApv == 4)
                    {

                    }
                    else if (sp.StatusApv == 5)
                    {

                    }

                    LoadOpenUPD();

                    if (noSPP.Value != string.Empty)
                    {
                        string[] ListSPP = noSPP.Value.Split(',');
                        string[] ListOpenPO = ListSPP.Distinct().ToArray();
                        int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                        LoadOpenUPD(ListOpenPO[0].ToString());
                        btnApprove.Enabled = true;
                        btnNotApv.Enabled = true;
                        btnNext.Enabled = (ListOpenPO.Count() > 1) ? true : false;
                        ViewState["index"] = idx;
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                        btnNext.Enabled = false;
                    }
                    string[] ListOpenPOx = noSPP.Value.Split(',');
                    string[] ListOpenPOd = ListOpenPOx.Distinct().ToArray();
                    int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                    btnNext.Enabled = ((idxx - 1) >= ListOpenPOd.Count()) ? false : true;
                    ViewState["index"] = idxx;
                    //txtAlasan.Attributes.Add("onkeyup", "onKeyUp()");
                    if (Request.QueryString["UpdNo"] != null)
                    {
                        LoadOpenUPD(Request.QueryString["UpdNo"].ToString());
                    }
                }


            }
            btnNotApv.Attributes.Add("onclick", "return confirm_batal();");
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenPO.Count()) ? false : true;
            LoadOpenUPD(ListOpenPO[idx].ToString());
            ViewState["index"] = idx;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
            btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
            try
            {
                ViewState["index"] = idx;
                LoadOpenUPD(ListOpenPO[idx].ToString());
            }
            catch
            {
                LoadOpenUPD(ListOpenPO[0].ToString());
                ViewState["index"] = 0;
            }
            btnPrev.Enabled = (idx > 0) ? true : false;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveUPD();
        }
        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_Click(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_Click(null, null);
            }
            else
            {
                Response.Redirect("FormApprovalPM.aspx");
            }
        }

        protected void btnNotApv_Click(object sender, EventArgs e)
        {
            panEdit.Visible = true;
            mpePopUp.Show();
        }

        protected void btnUpdateAlasan_ServerClick(object sender, EventArgs e)
        {
            ApproveUPD(true);
        }

        //protected void btnCari_Click(object sender, EventArgs e)
        //{
        //    LoadOpenUPD(txtCari.Text);
        //}

        private void LoadOpenUPD()
        {
            Users user = (Users)Session["Users"];
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd upd = new ISO_Upd();
            int deptID = user.DeptID;
            int userID = user.ID;

            arrUPD = updF.RetrieveForApprovalHeader();

            foreach (ISO_Upd upd1 in arrUPD)
            {
                if (upd1.updNo != "")
                {
                    noSPP.Value += upd1.updNo + ",";
                }
            }

            noSPP.Value = (noSPP.Value != string.Empty) ? noSPP.Value.Substring(0, (noSPP.Value.Length - 1)) : "0";
        }

        private void LoadOpenUPD(string SPPNum)
        {
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd sp = new ISO_Upd();
            int ID = sp.ID;
            Session["SPPNum"] = SPPNum;

            ISO_UpdFacade updF1 = new ISO_UpdFacade();
            ISO_Upd sp1 = new ISO_Upd();

            Users user = (Users)Session["Users"];
            int IDUser = ((Users)Session["Users"]).ID;
            sp1 = updF1.RetrieveTipeApv(IDUser);

            ISO_UpdFacade updShareFacade = new ISO_UpdFacade();
            ISO_Upd spShare = new ISO_Upd();

            int Share = updShareFacade.cekShare(user.DeptID, user.UnitKerjaID);

            if (Share > 0)
            {
                if (sp1.TipeApv != 6)
                {
                    LabelInfoShare.Visible = true; LabelInfoShare.Text = "Ada " + " " + Share + " " + "  Share Dokumen yang harus di lihat";
                    LabelInfoNotApprove.Visible = false; LabelInfoNotApprove.Visible = false;
                }
                else
                {
                    LabelInfoShare.Visible = false; LabelInfoNotApprove.Visible = false;
                    LabelInfoNotApprove.Visible = false; RBShare.Visible = false;
                }
            }
            else
            {
                LabelInfoShare.Visible = false; RBShare.Visible = false;
                LabelInfoNotApprove.Visible = false; LabelInfoNotApprove.Visible = false;
            }

            ISO_UpdFacade updShareFacade2 = new ISO_UpdFacade();
            ISO_Upd spShare2 = new ISO_Upd();

            int Share2 = updShareFacade.cekShareNotApprove();

            if (user.UserName == "makrufspp")
            {
                if (Share2 > 0)
                {
                    LabelInfoNotApprove.Visible = true;
                    LabelInfoNotApprove.Text = "Ada " + " " + Share2 + " " + "  Share Dokumen yang tidak di approve Manager Dept";
                    RBNotApprove.Visible = true;
                }
                else
                {
                    LabelInfoNotApprove.Visible = false; LabelInfoNotApprove.Visible = false; RBNotApprove.Visible = false;
                }
            }

            if (sp1.TipeApv != 0)
            {
                if (sp1.TipeApv == 1) { sp = updF.RetrieveByApv1(SPPNum, IDUser); }    // Bukan Corporate            
                else if (sp1.TipeApv == 2) { sp = updF.RetrieveByApv2(SPPNum, IDUser); }    // Dokumen Khusus + HRD           
                else if (sp1.TipeApv == 3) { sp = updF.RetrieveByApv3(SPPNum, IDUser); }    // Optimum Stok + HRD
                                                                                            //else if (sp1.TipeApv == 4) { sp = updF.RetrieveByApv4(SPPNum, IDUser); }    // Head ISO          
                else if (sp1.TipeApv == 5) { sp = updF.RetrieveByApv5(SPPNum); }            // Manager ISO
                else if (sp1.TipeApv == 6) { sp = updF.RetrieveByApv6(SPPNum, IDUser); }    // PM 
                else if (sp1.TipeApv == 7) { sp = updF.RetrieveByApv7(SPPNum, IDUser, user.DeptID); }    // By Pass PM 
                else if (sp1.TipeApv == 10) { sp = updF.RetrieveHapus(SPPNum); } //SekretariatISO
            }

            //if (txtCari.Text != "Find by Nomor UPD" && sp.updNo == string.Empty)
            //{
            //    sp = new ISO_Upd();
            //    DisplayAJAXMessage(this, "UPD tidak di temukan");
            //    txtCari.Text = string.Empty;
            //    return;
            //}



            if (SPPNum != "0")
            { txtTglPengajuan.Text = sp.TglPengajuan.ToString("dd-MMM-yyyy"); }
            else
            { txtTglPengajuan.Text = ""; }
            //txtTglPengajuan.Text = sp.TglPengajuan.ToString("dd-MMM-yyyy");
            //txtTglPengajuan.Text = "";
            txtNamDok1.Text = sp.UpdName;
            txtNoS.Text = sp.updNo;
            int jenisDoc = sp.JenisDoc;
            int Kategory = sp.CategoryUPD;
            int userDeptID = sp.DeptID;
            Session["userDeptID"] = userDeptID;
            txtDibuatOleh.Text = sp.Pic;
            txtAlasan.Text = sp.Alasan;
            txtNoDok.Text = sp.NoDokumen;

            if (sp.IDmaster != "")
            {
                ISO_UpdFacade updF2 = new ISO_UpdFacade();
                ISO_Upd sp2 = new ISO_Upd();
                int IDFile = updF2.CekIDFile(Convert.ToInt32(Convert.ToInt32(sp.IDmaster)));
                Session["IDFile"] = IDFile;
            }

            string NamaFile = sp.NamaFile;

            ISO_UpdFacade updF5 = new ISO_UpdFacade();
            ISO_Upd sp5 = new ISO_Upd();
            sp5 = updF5.RetrieveForKategory(Kategory);
            if (Kategory != 0) { txtKategori.Text = sp5.CategoryUPd; }

            ISO_UpdFacade updF6 = new ISO_UpdFacade();
            ISO_Upd sp6 = new ISO_Upd();
            sp6 = updF6.RetrieveForJD(jenisDoc);
            if (jenisDoc != 0) { txtPermintaan.Text = sp6.JenisDokumen; }

            ISO_UpdFacade updF7 = new ISO_UpdFacade();
            ISO_Upd sp7 = new ISO_Upd();
            sp7 = updF7.RetrieveForNamaDept(userDeptID);
            if (userDeptID != 0) { txtDept1.Text = sp7.DeptName; }

            ISO_UpdFacade updF8 = new ISO_UpdFacade();
            ISO_Upd sp8 = new ISO_Upd();
            sp8 = updF8.RetrieveForID(SPPNum);

            ArrayList arrUPD = updF.RetrieveFile(sp.ID);
            GridView3.DataSource = arrUPD;
            GridView3.DataBind();

            ISO_UpdFacade updF9 = new ISO_UpdFacade();
            ISO_Upd sp9 = new ISO_Upd();
            sp9 = updF9.RetrieveForIDMaster(SPPNum);

            //if (sp.RevisiNo != string.Empty)
            //{ txtRevisi.Text = sp.RevisiNo; }
            //else if (sp.RevisiNo == "" && SPPNum == "0")
            //{ txtRevisi.Text = ""; }
            //else { txtRevisi.Text = "0"; }

            //if (sp.RevisiNo == "0" && jenisDoc != 3)  
            if (sp.RevisiNo == "0" && jenisDoc == 1)
            {
                LabelRevisi.Visible = true; LabelRevisi.Text = "REVISI : 0";
                txtRevisiKe.Visible = false;
                txtRevisi.Visible = false;
                LabelRevisiKe.Visible = false;
            }
            else if (sp.RevisiNo == "" && jenisDoc != 3)
            {
                LabelRevisi.Visible = false;
            }
            else if (sp.RevisiNo != "0" && jenisDoc != 3 || sp.RevisiNo == "0" && jenisDoc == 2)
            {
                int Revisi = 0;
                Revisi = Convert.ToInt32(sp.RevisiNo);
                if (Revisi >= 0)
                {
                    int RevisiIsi = Revisi + 1;
                    LabelRevisi.Visible = true; LabelRevisi.Text = "DARI REVISI"; txtRevisi.Visible = true; txtRevisi.Text = Revisi.ToString();
                    LabelRevisiKe.Visible = true; LabelRevisiKe.Text = "KE REVISI"; txtRevisiKe.Visible = true; txtRevisiKe.Text = RevisiIsi.ToString();
                }
            }
            else if (jenisDoc == 3)
            {
                LabelRevisi.Visible = true; LabelRevisi.Text = "REVISI"; txtRevisi.Visible = true; txtRevisi.Text = sp.RevisiNo;
            }


            ISO_UpdFacade upd4 = new ISO_UpdFacade();
            ISO_Upd sp4 = new ISO_Upd();
            ArrayList arrUPD4 = upd4.RetrieveFileLama(SPPNum);

            GridView4.DataSource = arrUPD4;
            GridView4.DataBind();

            foreach (ISO_Upd Dupd in arrUPD4)
            {
                if (Dupd.FileLama != "-" && Dupd.FileLama != "")
                {
                    PanelPreview.Visible = false;
                    //LoadFileLama();
                    GridView4.Enabled = true;
                }
                else if (Dupd.FileLama == "-" && Dupd.FileLama == "")
                {
                    PanelPreview.Visible = false;
                    GridView4.Enabled = false;
                }
            }
        }

        protected void LoadFileLama()
        {
            int IDFile = Convert.ToInt32(Session["IDFile"]);
            //string Nama = e.CommandArgument.ToString();
            string embed = "<object data=\"{0}{1}\"type=\"application/pdf\" width=\"100%\" height=\"550px\" >";
            embed += "</object>";
            pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerUPD.ashx?ba="), IDFile);

        }

        //protected void LoadFileBaru()
        //{
        //    int IDFile = Convert.ToInt32(Session["IDFile"]);
        //    //string Nama = e.CommandArgument.ToString();

        //    string embed = "<object data=\"{0}{1}\"type=\"application/vnd.ms-excel\" width=\"100%\" height=\"550px\" >";
        //    embed += "</object>";
        //    officeView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerUPDOffice.ashx?ba="), IDFile);

        //}

        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfSPPDetail"] = null;
            Session["NoSPP"] = null;
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            string spp = string.Join(",", ListOpenPO);
            string sp = new EncryptPasswordFacade().EncryptToString(spp);
            Response.Redirect("ListSPPApproval.aspx?approve=" + (((Users)Session["Users"]).GroupID) + "&sp=" + sp);
        }

        protected void txtAlasan_Change(object sender, EventArgs e)
        {
            //btnApprove.Enabled = (txtAlasan.Text.Length > 0) ? false : true;
            //btnNotApprove.Enabled = (txtAlasan.Text.Length > 0) ? true : false;
        }

        protected void txtAlasan2_Change(object sender, EventArgs e)
        {
            //btnApprove.Enabled = (txtAlasan.Text.Length > 0) ? false : true;
            //btnNotApprove.Enabled = (txtAlasan.Text.Length > 0) ? true : false;
        }

        private void ApproveUPD()
        {
            int intResult = 0;
            Users user = (Users)Session["Users"];
            ISO_UpdDMD upd1 = new ISO_UpdDMD();
            ISO_Upd upd2 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            string usernama = user.UserName;
            int deptID = user.DeptID;
            string NoUPD = Session["SPPNum"].ToString();
            int IDUser = ((Users)Session["Users"]).ID;
            upd2 = updF.RetrieveRowStatus(IDUser);
            int RW = upd2.RowStatus1;

            ISO_Upd upd3 = new ISO_Upd();
            ISO_UpdFacade updF3 = new ISO_UpdFacade();
            upd3 = updF.RetrieveForData(NoUPD);
            int Approval = upd3.apv; int lvlApv = 0;
            if (upd3.Type != 0)
            {
                if (upd3.Type == 1)
                {
                    if (upd3.apv == 0) { upd2.apv = upd3.apv + 2; lvlApv = upd3.apv + 2; }
                    //else if (upd3.apv == 0) { upd2.apv = upd3.apv + 2; }
                    else if (upd3.apv == 1) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 2) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 3) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 4) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 5) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                }
                else if (upd3.Type == 2 || upd3.Type == 3)
                {
                    if (RW == 1) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (RW == 3) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 0) { upd2.apv = upd3.apv + 2; lvlApv = upd3.apv + 2; }
                    else if (upd3.apv == 1) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 2 && upd2.RowStatus1 == 2) { upd2.apv = upd3.apv; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 2) { upd2.apv = upd3.apv + 0; lvlApv = upd3.apv + 0; }
                    else if (upd3.apv == 3) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 4) { upd2.apv = upd3.apv + 1; lvlApv = upd3.apv + 1; }
                    else if (upd3.apv == 5) { upd2.apv = upd3.apv + 0; lvlApv = upd3.apv + 0; }
                }
            }

            upd2.ID = upd3.ID;
            upd2.updNo = NoUPD;
            upd2.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd2.TypE = upd3.Type.ToString();
            upd2.RowStatus = upd3.RowStatus;
            upd2.DeptID = user.DeptID;
            upd2.type = upd3.Type;
            upd2.LastModifiedBy2 = upd3.LastModifiedBy;
            int tipe = upd3.Type;
            intResult = dmdF.UpdateApv1(upd2);
            if (intResult > -1)
            {
                ISO_UpdD2 upd = new ISO_UpdD2();
                ISO_UPDFacade2 updf = new ISO_UPDFacade2();
                int result = 0;

                upd.IPAddress = GetIPAddress();
                upd.UserID = user.ID.ToString();
                if (user.DeptID == 23 && user.Apv > 0)
                {
                    if (tipe == 2)
                    {
                        upd.Urutan = lvlApv;
                    }
                    else
                    {
                        upd.Urutan = lvlApv - 1;
                    }
                }
                else
                {
                    upd.Urutan = lvlApv - 1;
                }
                upd.IdUPD = upd3.ID;
                upd.DocShare = "-";

                result = updf.InsertLogApv(upd);

                Response.Redirect("FormApprovalPM.aspx");
            }

        }

        private void ApproveUPD(bool NotApprove)
        {
            Session["AlasanCancel"] = txtAlasanCancel.Text;
            int intResult = 0;
            Users users = new Users();
            ISO_UpdDMD upd1 = new ISO_UpdDMD();
            ISO_Upd upd2 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            string NoUPD = txtNoS.Text.Trim();

            upd2.Alasan2 = Session["AlasanCancel"].ToString();
            upd2.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd2.NoDokumen = NoUPD.Trim();

            intResult = dmdF.UpdateNotApvPM(upd2);
            AutoNext();
            clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void clearForm()
        {
            //txtAlasan2.Text = string.Empty;
            btnApprove.Enabled = true;
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd sp = new ISO_Upd();
            string NamaDept = string.Empty;

            int userDeptID = Convert.ToInt32(Session["userDeptID"]);
            sp = updF.RetrieveForNamaDept(userDeptID);

            if (userDeptID != 0)
            {
                NamaDept = sp.DeptName;    // Baru add 25 Maret 2017        
            }

            if (e.CommandName == "Download")
            {

                string Nama = e.CommandArgument.ToString();
                string Nama2 = @"\" + Nama;
                string dirPath = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept);
                string ext = Path.GetExtension(Nama);

                Response.Clear();

                if (ext == ".xlsx" || ext == ".docx")
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }
                else
                {
                    Response.ContentType = "application/octet-stream";
                }

                Response.AppendHeader("Content-Disposition", "filename="
                    + e.CommandArgument);
                Response.TransmitFile(Path.Combine(dirPath, Nama));
                Response.Flush();
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView4.Enabled = true;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbn1 = e.Row.FindControl("LinkButton1") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn1);


                }
            }
            catch (Exception ex)
            { }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                LoadFileLama();
                PanelPreview.Visible = true;
                //GridView4.Enabled = true;

                //int IDFile = Convert.ToInt32(Session["IDFile"]);
                //string Nama = e.CommandArgument.ToString();

                //string embed = "<object data=\"{0}{1}\"type=\"application/pdf\" width=\"100%\" height=\"550px\" >";
                //embed += "</object>";
                //pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerUPD.ashx?ba="), IDFile);
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbn2 = e.Row.FindControl("LinkButton2") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn2);
                }
            }
            catch (Exception ex)
            { }
        }

        protected void RBShare_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("FormApprovalPMShare.aspx");
        }

        protected void RBNotApprove_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("FormApprovalPMNoApprove.aspx");
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }

    public class ISO_UpdD2
    {
        public string DocShare { get; set; }
        public string IPAddress { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Rowstatus { get; set; }
        public int Urutan { get; set; }
        public int IdUPD { get; set; }
    }

    public class ISO_UPDFacade2
    {
        private ISO_UpdD2 objUPD2 = new ISO_UpdD2();
        protected string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        //private ISO_UpdD2 upd2 = new ISO_UpdD2();
        private List<SqlParameter> sqlListParam;

        public int InsertLogApv(object objDomain)
        {
            try
            {
                objUPD2 = (ISO_UpdD2)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUPD2.UserID));
                sqlListParam.Add(new SqlParameter("@IPAddress", objUPD2.IPAddress));
                sqlListParam.Add(new SqlParameter("@Urutan", objUPD2.Urutan));
                sqlListParam.Add(new SqlParameter("@IdUPD", objUPD2.IdUPD));
                sqlListParam.Add(new SqlParameter("@DocShare", objUPD2.DocShare));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "UPD_SP_LogApv");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

    }

}