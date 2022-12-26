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

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormApprovalPMShare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                int IDUser = user.ID;
                ISO_Upd sp = new ISO_Upd();
                ISO_UpdFacade spF = new ISO_UpdFacade();
                sp = spF.RetrieveForApvKe(IDUser);

                LoadOpenUPDShare();

                if (noSPP.Value != string.Empty)
                {
                    string[] ListSPP = noSPP.Value.Split(',');
                    string[] ListOpenPO = ListSPP.Distinct().ToArray();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    LoadOpenUPDShare(ListOpenPO[0].ToString());
                    btnApprove.Enabled = true;
                    btnNotApprove.Enabled = true;
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

                if (Request.QueryString["UpdNo"] != null)
                {
                    LoadOpenUPDShare(Request.QueryString["UpdNo"].ToString());
                }




                btnNotApprove.Attributes.Add("onclick", "return confirm_batal();");
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenPO.Count()) ? false : true;
            LoadOpenUPDShare(ListOpenPO[idx].ToString());
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
                LoadOpenUPDShare(ListOpenPO[idx].ToString());
            }
            catch
            {
                LoadOpenUPDShare(ListOpenPO[0].ToString());
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
                Response.Redirect("FormApprovalPMShare.aspx");
            }
        }

        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            ApproveUPD(true);
        }

        //protected void btnCari_Click(object sender, EventArgs e)
        //{
        //    LoadOpenUPDShare(txtCari.Text);
        //}

        private void LoadOpenUPDShare()
        {
            Users user = (Users)Session["Users"];
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd upd = new ISO_Upd();
            int deptID = user.DeptID;
            int userID = user.ID;

            arrUPD = updF.RetrieveForHeadApv1Share(user.DeptID, user.UnitKerjaID);

            foreach (ISO_Upd upd1 in arrUPD)
            {
                string Data2 = string.Empty;

                if (upd1.Type == 1)
                {
                    Data2 = upd1.NoDokumen.Trim();
                }
                else if (upd1.Type == 2)
                {
                    Data2 = upd1.NamaDokumen.Trim();
                }
                //Session["TipeUPD"] = upd1.Type;

                if (upd1.NoDokumen != "")
                {
                    noSPP.Value += upd1.ID + ",";
                }

                //if (upd1.NoDokumen != "")
                //{
                //    noSPP.Value += upd1.NoDokumen + ",";
                //}    

                //if (Data2 != "")
                //{
                //    noSPP.Value += Data2 + ",";
                //} 
            }

            noSPP.Value = (noSPP.Value != string.Empty) ? noSPP.Value.Substring(0, (noSPP.Value.Length - 1)) : "0";
        }

        private void LoadOpenUPDShare(string SPPNum)
        {
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd sp = new ISO_Upd();
            int ID = sp.ID;

            //int TipeUPD = Convert.ToInt32(Session["TipeUPD"]);

            Session["SPPNum"] = SPPNum;
            ISO_UpdFacade updF1 = new ISO_UpdFacade();
            ISO_Upd sp1 = new ISO_Upd();

            Users user = (Users)Session["Users"];
            int IDUser = ((Users)Session["Users"]).ID;
            sp1 = updF1.RetrieveTipeApv(IDUser);

            if (sp1.TipeApv != 0)
            {
                sp = updF.RetrieveByApv1Share(SPPNum);
            }

            if (SPPNum != "0")
            { txtTglPengajuan.Text = sp.TglPengajuan.ToString("dd-MMM-yyyy"); }
            else
            { txtTglPengajuan.Text = ""; }
            txtNoS.Text = " - ";
            txtNoS.Enabled = false;
            txtDibuatOleh.Enabled = false;

            if (sp.PlanID == 1)
            {
                txtDibuatOleh.Text = "Plant Citerueup";
            }
            else if (sp.PlanID == 7)
            {
                txtDibuatOleh.Text = "Plant Karawang";
            }
            else if (sp.PlanID == 13)
            {
                txtDibuatOleh.Text = "Plant Jombang";
            }
            else { txtDibuatOleh.Text = ""; }

            txtPermintaan.Text = sp.Permintaan;
            txtDept1.Text = sp.DeptName;
            txtNamDok1.Text = sp.NamaDokumen;
            txtAlasan.Text = sp.Alasan;
            txtNoDok.Text = sp.NoDokumen;
            txtKategori.Text = sp.Kategori;
            Session["NoDokumen"] = sp.NoDokumen.Trim();
            Session["NamaDokumen"] = sp.NamaDokumen.Trim();
            Session["Type"] = sp.Type;
            //int Type = sp.Type;

            if (sp.IDmaster != "")
            {
                ISO_UpdFacade updF2 = new ISO_UpdFacade();
                ISO_Upd sp2 = new ISO_Upd();
                int IDFile = updF2.CekIDFile(Convert.ToInt32(Convert.ToInt32(sp.IDmaster)));
                Session["IDFile"] = IDFile;
            }

            //string NamaFile = sp.NamaFile;    

            if (sp.RevisiNo != "")
            { txtRevisi.Text = sp.RevisiNo; }
            else if (sp.RevisiNo == "" && SPPNum == "0")
            { txtRevisi.Text = ""; }
            else { txtRevisi.Text = "0"; }

            //string Data = string.Empty;
            //if (sp.type == 1)
            //{
            //    Data = sp.NoDokumen.Trim();
            //}
            //else if (sp.Type == 2)
            //{
            //    Data = sp.NamaDokumen.Trim();
            //}

            //Session["FileLama"] = sp.FileLama.Trim();
            ISO_UpdFacade upd4 = new ISO_UpdFacade();
            ISO_Upd sp4 = new ISO_Upd();
            //ArrayList arrUPD4 = upd4.RetrieveFileShare(sp.NoDokumen.Trim());   
            ArrayList arrUPD4 = upd4.RetrieveFileShare(SPPNum);

            GridView4.DataSource = arrUPD4;
            GridView4.DataBind();

            foreach (ISO_Upd Dupd in arrUPD4)
            {
                if (Dupd.FileLama != "-" && Dupd.FileLama != "")
                {
                    Session["FileNama"] = Dupd.FileLama.Trim();
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
            string Nama = Session["FileNama"].ToString();
            string embed = "<object data=\"{0}{1}\"type=\"application/pdf\" width=\"100%\" height=\"550px\" >";
            embed += "</object>";
            pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerUPDShare.ashx?ba="), Nama);
        }

        protected void LoadPDF()
        { }


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
            ISO_DMDFacade FacadeUPD = new ISO_DMDFacade();
            ISO_Upd DomainUPD = new ISO_Upd();
            int Tipe = Convert.ToInt32(Session["Type"]);

            string IDmaster = Session["SPPNum"].ToString();

            string NamaDokumen = txtNamDok1.Text.Trim();
            string NoDokumen = Session["NoDokumen"].ToString();

            DomainUPD.Type = Tipe;
            DomainUPD.NamaDokumen = NamaDokumen;
            DomainUPD.NoDokumen = NoDokumen.Trim();
            DomainUPD.LastModifiedBy = user.UserName.Trim();
            DomainUPD.IDmaster = IDmaster;

            intResult = FacadeUPD.UpdateShare(DomainUPD);
            if (intResult > -1)
            { Response.Redirect("FormApprovalPMShare.aspx"); }
        }

        private void ApproveUPD(bool NotApprove)
        {
            int intResult = 0;
            //string tampung = string.Empty;

            
            Users user = (Users)Session["Users"];
            ISO_DMDFacade FacadeUPDns = new ISO_DMDFacade();
            ISO_Upd DomainUPDns = new ISO_Upd();

            string NoDokumen = Session["NoDokumen"].ToString();
            string NamaDokumen = Session["NamaDokumen"].ToString();
            int Tipe = Convert.ToInt32(Session["Type"]);
            DomainUPDns.AlasanNo = txtAlasanCancel.Text;
            DomainUPDns.Type = Tipe;
            DomainUPDns.NoDokumen = NoDokumen.Trim();
            DomainUPDns.NamaDokumen = NamaDokumen.Trim();
            DomainUPDns.LastModifiedBy = user.UserName.Trim();

            intResult = FacadeUPDns.UpdateNotShare(DomainUPDns);
            if (intResult > -1)
            { Response.Redirect("FormApprovalPMShare.aspx"); }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void clearForm()
        {
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

        protected void RBNotShare_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("FormApprovalPM.aspx");
        }
    }
}