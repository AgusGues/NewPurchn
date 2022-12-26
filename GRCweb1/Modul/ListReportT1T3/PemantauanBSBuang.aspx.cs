using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class PemantauanBSBuang : System.Web.UI.Page
    {
        decimal TotalQty = 0; decimal TotalM3 = 0; decimal GrandTotal = 0; decimal GrandTotalM3 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                Session["result"] = "none"; Session["resultapv"] = "none"; Session["DeptIDapv"] = 0; Session["isi"] = "-";
                Session["Next"] = "-"; Session["Prev"] = "-"; Session["Apv"] = "-"; Session["Liat"] = "-"; Session["LiatDeptID"] = 0;
                Session["awal"] = "-";
                labeldept.Text = "Department";
                if (users.DeptID == 3)
                {
                    labelJudul.Text = "PEMANTAUAN BS BUANG FINISHING";
                }
                else if (users.DeptID == 6 || users.DeptID == 10)
                {
                    labelJudul.Text = "PEMANTAUAN BS BUANG LOGISTIK";
                }
                txtdrtanggal.Text = DateTime.Now.AddDays(-1).ToString("01-MMM-yyyy");
                txtsdtanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");

                DateTime drTgl = DateTime.Parse(txtdrtanggal.Text); DateTime sdTgl = DateTime.Parse(txtsdtanggal.Text);
                string tglawal = drTgl.ToString("yyyyMMdd"); string tglakhir = sdTgl.ToString("yyyyMMdd");
                Session["tglawal"] = tglawal; Session["tglakhir"] = tglakhir;

                if (users.Apv > 0 || users.DeptID == 24)
                {
                    LoadDataBS(); btnSave.Enabled = false; btnCancel.Enabled = false; btnApv.Enabled = true; Panel1.Visible = false;
                    //RBLiat.Visible = true;
                }
                else if (users.DeptID == 3 && users.Apv == 0 || users.DeptID == 6 && users.Apv == 0)
                {
                    btnSave.Enabled = true; btnCancel.Enabled = true; btnApv.Enabled = false; Panel1.Visible = true;
                    RBLiat.Visible = false; RBFinishing.Visible = false; RBLogistik.Visible = false;
                }
                else if (users.DeptID == 23)
                {
                    RBFinishing.Visible = true; RBLogistik.Visible = true;
                }
                else
                {
                    btnApv.Enabled = false; btnSave.Enabled = false; btnCancel.Enabled = false; Panel1.Visible = true;
                }

            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void RBLiat_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Visible = true; labelsave.Visible = false; RBLogistik.Visible = true; RBFinishing.Visible = true;
            Users user = (Users)Session["Users"];
            if (user.DeptID == 3)
            {
                RBFinishing.Checked = true; RBLogistik.Checked = false;
            }
            else if (user.DeptID == 10 || user.DeptID == 6)
            {
                RBLogistik.Checked = true; RBFinishing.Checked = false;
            }
        }

        protected void RBLogistik_CheckedChanged(object sender, EventArgs e)
        {
            RBFinishing.Checked = false;
        }

        protected void RBFinishing_CheckedChanged(object sender, EventArgs e)
        {
            RBLogistik.Checked = false;
        }

        private void LoadDataBS()
        {
            string hasil = Session["result"].ToString();
            string stsapv = Session["resultapv"].ToString();
            string Apv = Session["Apv"].ToString();

            Session["Next"] = "-"; Session["Prev"] = "-";

            if (hasil == "sukses")
            {
                btnSave.Enabled = false;
                labelsave.Visible = true; labelsave.Text = "Release laporan berhasil ... !!!";
            }
            else
            {
                btnSave.Enabled = true; ;
            }

            if (stsapv == "sukses")
            {
                btnSave.Enabled = false; btnApv.Visible = false;
                labelsave.Visible = false; labelsave.Text = "Laporan berhasil di approved !!!";
                RBLogistik.Visible = false; RBFinishing.Visible = false;
            }
            else
            {
                labelsave.Visible = false; btnApv.Enabled = true;
            }

            Users user = (Users)Session["Users"];
            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();

            int DeptID01 = 0;

            if (user.DeptID == 9 || user.DeptID == 24)
            {
                PantauBSBuang BS01 = new PantauBSBuang();
                PantauBSBuangFacade FacadeBS01 = new PantauBSBuangFacade();
                BS01 = FacadeBS01.RetrieveDeptApv(Session["Next"].ToString(), user.DeptID);

                PantauBSBuang BS02 = new PantauBSBuang();
                PantauBSBuangFacade FacadeBS02 = new PantauBSBuangFacade();
                BS02 = FacadeBS02.Retrievettl(user.DeptID);

                DeptID01 = BS01.DeptID;

                if (DeptID01 > 0)
                {
                    Session["DeptIDapv"] = DeptID01; btnApv.Visible = true;

                    if (BS02.ttl > 1)
                    {
                        btnNext.Visible = true; btnPrev.Visible = true; btnPrev.Enabled = false; btnNext.Enabled = true; RBLiat.Visible = false;
                        LoadBreakDownTglRelease1(DeptID01);
                    }
                    else
                    {
                        btnNext.Visible = false; btnPrev.Visible = false; btnApv.Enabled = true; RBLiat.Visible = true;
                        LoadBreakDownTglRelease1(DeptID01);
                    }
                }
                else
                {
                    btnApv.Visible = false; lstTglP.DataBind(); lblGrandTotal.Text = ""; lblGrandTotalM3.Text = ""; labelJudul.Text = "";
                    if (Apv == "Apv")
                    {
                        labelJudul.Text = "";
                        DisplayAJAXMessage(this, "Approval Berhasil !! "); return;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Tidak ada laporan yg harus di apv !! "); return;
                    }
                }
            }
            else if (user.DeptID == 3 || user.DeptID == 6 || user.DeptID == 10)
            {
                if (user.DeptID > 0)
                {
                    LoadBreakDownTglRelease(user.DeptID);
                }
                //else
                //{
                //    btnApv.Enabled = false;
                //    DisplayAJAXMessage(this, "Tidak ada laporan yg harus di apv !! "); return;
                //}

                //if (hasil == "sukses")
                //{
                //    btnSave.Enabled = false;
                //    labelsave.Visible = true; labelsave.Text = "Release laporan berhasil ... !!!";
                //}
                //else
                //{
                //    btnSave.Enabled = true; ;
                //}
            }
        }
        private void LoadBreakDownTglRelease1(int DeptID)
        {
            Users user = (Users)Session["Users"];
            string Query = string.Empty; string Query2 = string.Empty;
            //int Dept = Convert.ToInt32(Session["DeptIDapv"]);

            if (user.DeptID == 9)
            {
                Query = " and ApvDept=1 and ApvQA=0 and ApvAcc=0 ";
            }
            else if (user.DeptID == 24)
            {
                Query = " and ApvDept=1 and ApvQA=1 and ApvAcc=0 ";
            }


            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeBS.RetrieveDataBSTglRelease1(DeptID, Query);
            lstTglP.DataSource = arrData;
            lstTglP.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            string Periode = Session["Periode"].ToString();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanBSBuang.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<center><b>LEMBAR PEMANTAUAN BS BUANG";
            if (user.DeptID == 3)
            {
                Html += "<br><b><i>Departement : Finishing ";

            }
            else if (user.DeptID == 6)
            {
                Html += "<br><b><i>Departement : Logistik FinishGood ";
            }
            Html += "<br><b><i>Periode : " + Periode;
            Html += "<br>";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int hasil = 0;
            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();

            for (int i = 0; i < lstTglP.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstTglP.Items[i].FindControl("tglpotong");
                if (tr.Cells[1].InnerHtml != "")
                {
                    DateTime TglPotong = Convert.ToDateTime(tr.Cells[1].InnerHtml.Replace("&nbsp;", ""));
                    string Tgl = TglPotong.ToString("yyyyMMdd");
                    BS.TglPotong = Tgl;
                    hasil = FacadeBS.Cancel(BS);

                    if (hasil > 0)
                    {
                        LoadDataBS(); labelsave.Visible = true; labelsave.Text = "Cancel laporan berhasil ... !!!";
                        //DisplayAJAXMessage(this, "Proses cancel berhasil !! "); return;
                    }
                }
            }
        }

        //protected void Chk_CheckedChanged(object sender, EventArgs e)
        //{ }

        protected void btnApv_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
            int intResult = 0; int DeptIDBs = 0;
            int i = 0;
            HtmlTableRow tr = (HtmlTableRow)lstTglP.Items[i].FindControl("tglpotong");
            DateTime TglPotong = Convert.ToDateTime(tr.Cells[1].InnerHtml.Replace("&nbsp;", ""));
            string Tgl = TglPotong.ToString("yyyyMM");

            if (labeldept.Text == "Logistik")
            {
                DeptIDBs = 6;
            }
            else if (labeldept.Text == "Finishing")
            {
                DeptIDBs = 3;
            }

            string Dept = string.Empty;
            if (users.DeptID == 10 && users.Apv > 1)
            {
                Dept = "6";
            }
            else
            {
                Dept = users.DeptID.ToString();
            }

            BS.TglPotong = Tgl;
            //BS.DeptID = users.DeptID;
            BS.DeptID = Convert.ToInt32(Dept);
            BS.DeptIDBs = DeptIDBs;
            BS.LastModifiedBy = users.UserName;

            intResult = FacadeBS.UpdateApv(BS);

            if (intResult > -1)
            {
                Session["resultapv"] = "sukses";
                Session["Apv"] = "Apv";
                btnNext.Visible = false; btnPrev.Visible = false;
                if (users.DeptID == 3 || users.DeptID == 10 || users.DeptID == 6)
                {
                    lblGrandTotalM3.Text = ""; lblGrandTotal.Text = ""; btnApv.Visible = false;
                }
                LoadDataBS();
                //labelsave.Visible = true; labelsave.Text = "laporan berhasil di approved !!!";
                DisplayAJAXMessage(this, "Proses Approval berhasil !! "); return;
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Session["Next"] = "Next"; btnNext.Enabled = false; btnPrev.Enabled = true;
            Users user = (Users)Session["Users"];
            PantauBSBuang BS03 = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS03 = new PantauBSBuangFacade();
            BS03 = FacadeBS03.RetrieveDeptApv(Session["Next"].ToString(), user.DeptID);
            Session["DeptID00"] = BS03.DeptID;
            if (BS03.DeptID > 0)
            {
                if (BS03.DeptID == 6)
                {
                    labelJudul.Text = "Logistik"; labeldept.Text = "Logistik";
                }
                else if (BS03.DeptID == 3)
                {
                    labelJudul.Text = "Finishing"; labeldept.Text = "Finishing";
                }
                LoadBreakDownTglRelease1(BS03.DeptID);
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Session["Prev"] = "Prev"; btnNext.Enabled = true; btnPrev.Enabled = false;
            Users user = (Users)Session["Users"];
            PantauBSBuang BS01 = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS01 = new PantauBSBuangFacade();
            BS01 = FacadeBS01.RetrieveDeptApv(Session["Prev"].ToString(), user.DeptID);
            Session["DeptID00"] = BS01.DeptID;
            if (BS01.DeptID > 0)
            {
                LoadBreakDownTglRelease1(BS01.DeptID);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int hasil = 0; Repeater lstData;
            Users users = (Users)Session["Users"];
            PantauBSBuang BS02 = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS02 = new PantauBSBuangFacade();

            PantauBSBuang BS03 = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS03 = new PantauBSBuangFacade();


            #region asli
            //for (int i = 0; i < lstTglP.Items.Count; i++)
            //{
            //    #region I
            //    HtmlTableRow tr = (HtmlTableRow)lstTglP.Items[i].FindControl("tglpotong");
            //    lstPrs = ((Repeater)(lstTglP.FindControl("ListBS")));

            //    if (tr.Cells[1].InnerHtml != "")
            //    {               
            //        DateTime TglPotong = Convert.ToDateTime(tr.Cells[1].InnerHtml.Replace("&nbsp;", ""));                
            //        string Tgl = TglPotong.ToString("yyyyMMdd");              
            //        ArrayList arrData = new ArrayList();                
            //        arrData = FacadeBS02.RetrieveDataBS(Tgl,users.DeptID.ToString());
            //        foreach (PantauBSBuang bs in arrData)
            //        {
            //            DateTime TglPotong1 = Convert.ToDateTime(bs.TglPotong);
            //            string Tgl1 = TglPotong.ToString("yyyyMMdd");
            //            BS02.TglPotong = Tgl1;
            //            BS02.Partno = bs.Partno;
            //            BS02.Qty = bs.Qty;
            //            BS02.M3 = bs.M3;
            //            BS02.PIC = bs.PIC;
            //            BS02.CreatedBy = users.UserName;
            //            BS02.DeptID = users.DeptID;
            //            BS02.Ket = "-";

            //            hasil = FacadeBS03.Release(BS02);
            //        }
            //    }
            //    #endregion
            //}
            //if (hasil > 0)
            //{
            //    Session["result"] = "sukses";
            //    LoadDataBS(); //labelsave.Visible = false; labelsave.Text = "Release laporan berhasil ... !!!";           
            //    //DisplayAJAXMessage(this, "Proses Release berhasil !! "); return;
            //}
            #endregion

            #region Modif
            int i = 0;
            foreach (RepeaterItem objItem0 in lstTglP.Items)
            {
                HtmlTableRow tr = (HtmlTableRow)lstTglP.Items[i].FindControl("tglpotong");
                lstData = ((Repeater)(objItem0.FindControl("ListBS")));
                int ii = 0;
                foreach (RepeaterItem objItem in lstData.Items)
                {
                    HtmlTableRow tr1 = (HtmlTableRow)lstData.Items[ii].FindControl("ps1");
                    DateTime Tgl = Convert.ToDateTime(tr.Cells[1].InnerHtml.Replace("&nbsp;", "").Trim());
                    string TglP = Tgl.ToString("yyyy-MM-dd");
                    TextBox Keterangan = (TextBox)lstData.Items[ii].FindControl("Keterangan");

                    BS02.TglPotong = TglP;
                    BS02.Partno = tr1.Cells[0].InnerHtml.Replace("&nbsp;", "").Trim();
                    BS02.Qty = Convert.ToDecimal(tr1.Cells[1].InnerHtml);
                    BS02.M3 = Convert.ToDecimal(tr1.Cells[2].InnerHtml);
                    BS02.PIC = tr1.Cells[3].InnerHtml.Replace("&nbsp;", "").Trim();
                    BS02.CreatedBy = users.UserName;
                    BS02.DeptID = users.DeptID;
                    BS02.Ket = Keterangan.Text.ToString().Trim();

                    hasil = FacadeBS03.Release(BS02);

                    ii++;
                }
                i++;
            }
            if (hasil > 0)
            {
                Session["result"] = "sukses"; Session["isi"] = "1"; Session["Liat"] = "Liat";
                LoadDataBS();
            }
            #endregion
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            labelsave.Visible = false; labelsave.Text = "";
            Users user = (Users)Session["Users"]; int DeptID = 0;

            if (user.DeptID == 9 || user.DeptID == 24)
            {
                Session["Liat"] = "Liat";

                if (RBLogistik.Checked == false && RBFinishing.Checked == false)
                {
                    DisplayAJAXMessage(this, "Pilihan Department belum di pilih !! "); return;
                }

                if (RBLogistik.Checked)
                {
                    DeptID = 6; Session["LiatDeptID"] = 6;
                }
                else if (RBFinishing.Checked)
                {
                    DeptID = 3; Session["LiatDeptID"] = 3;
                }
            }
            else
            {
                Session["Liat"] = "Liat";

                if (RBLiat.Checked)
                {
                    if (RBLogistik.Checked == false && RBFinishing.Checked == false)
                    {
                        DisplayAJAXMessage(this, "Pilihan Department belum di pilih !! "); return;
                    }

                    if (RBLogistik.Checked)
                    {
                        DeptID = 6; Session["LiatDeptID"] = 6;
                    }
                    else if (RBFinishing.Checked)
                    {
                        DeptID = 3; Session["LiatDeptID"] = 3;
                    }
                }
                else
                {
                    DeptID = user.DeptID;
                }

                //DeptID = user.DeptID;
            }

            DateTime drTgl = DateTime.Parse(txtdrtanggal.Text);
            DateTime sdTgl = DateTime.Parse(txtsdtanggal.Text);
            string tglawal = drTgl.ToString("yyyyMMdd");
            string tglakhir = sdTgl.ToString("yyyyMMdd");
            string bln = drTgl.ToString("MM"); string thn = drTgl.ToString("yyyy");
            string Periode = string.Empty;
            if (bln != "0")
            {
                if (bln == "01") { Periode = "Januari " + thn; Session["Periode"] = Periode; }
                else if (bln == "02") { Periode = "Februari " + thn; Session["Periode"] = Periode; }
                else if (bln == "03") { Periode = "Maret " + thn; Session["Periode"] = Periode; }
                else if (bln == "04") { Periode = "April " + thn; Session["Periode"] = Periode; }
                else if (bln == "05") { Periode = "Mei " + thn; Session["Periode"] = Periode; }
                else if (bln == "06") { Periode = "Juni " + thn; Session["Periode"] = Periode; }
                else if (bln == "07") { Periode = "Juli " + thn; Session["Periode"] = Periode; }
                else if (bln == "08") { Periode = "Agustus " + thn; Session["Periode"] = Periode; }
                else if (bln == "09") { Periode = "September " + thn; Session["Periode"] = Periode; }
                else if (bln == "10") { Periode = "Oktober " + thn; Session["Periode"] = Periode; }
                else if (bln == "11") { Periode = "November " + thn; Session["Periode"] = Periode; }
                else if (bln == "12") { Periode = "Desember " + thn; Session["Periode"] = Periode; }
            }

            PantauBSBuang BS01 = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS01 = new PantauBSBuangFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeBS01.RetrieveDataRelease(tglawal, tglakhir, DeptID);

            if (arrData.Count > 0)
            {
                Session["isi"] = "1"; btnExport.Enabled = true;

                if (user.Apv > 0 && RBLiat.Checked == false)
                {
                    btnSave.Enabled = false; Panel1.Visible = false;
                }
                else if (user.Apv > 0 && RBLiat.Checked == true)
                {
                    btnSave.Enabled = false; Panel1.Visible = true; labelJudul.Visible = true;
                }
                else
                {
                    btnSave.Enabled = false; Panel1.Visible = true; RBLiat.Enabled = false;
                }

                LoadBreakDownTglRelease2(tglawal, tglakhir, DeptID);
            }
            else
            {
                Session["isi"] = "0"; btnExport.Enabled = false;
                if (user.DeptID == 9 || user.DeptID == 24 || user.DeptID == 6 && user.Apv > 0 || user.DeptID == 3 && user.Apv > 0 || user.DeptID == 10 && user.Apv > 0)
                {
                    lstTglP.DataBind(); lblGrandTotal.Text = ""; lblGrandTotalM3.Text = ""; labelJudul.Visible = false; labeldept.Text = "Manager";

                    DisplayAJAXMessage(this, "Data tidak ada !! "); return;

                }
                else
                {
                    LoadBreakDownTgl(tglawal, tglakhir);
                    btnSave.Enabled = true; Panel1.Visible = true; Session["awal"] = "awal";

                }
            }
        }

        private void LoadBreakDownTgl(string tgl1, string tgl2)
        {
            Users users = (Users)Session["Users"];
            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeBS.RetrieveDataBSTgl(tgl1, tgl2, users.DeptID, users.UnitKerjaID.ToString());
            if (arrData.Count == 0)
            {
                lstTglP.DataBind(); lblGrandTotal.Text = ""; lblGrandTotalM3.Text = "";
                DisplayAJAXMessage(this, "Data tidak ada !! "); return;
            }
            else
            {
                btnSave.Visible = true; btnCancel.Visible = true;
                lstTglP.DataSource = arrData;
                lstTglP.DataBind();
            }
        }

        private void LoadBreakDownTglRelease(int DeptID)
        {
            Users users = (Users)Session["Users"]; int DeptID00 = 0;
            if (users.DeptID == 9 || users.DeptID == 24)
            {
                DeptID00 = DeptID;
            }
            else
            {
                DeptID00 = users.DeptID;
            }

            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeBS.RetrieveDataBSTglRelease(DeptID00);
            if (arrData.Count > 0)
            {
                lstTglP.DataSource = arrData;
                lstTglP.DataBind();
            }
            else if (arrData.Count == 0 && users.DeptID > 0)
            {
                lstTglP.DataBind();
                //DisplayAJAXMessage(this, "Tidak ada laporan yg harus di apv !! "); return;
            }
            //lstTglP.DataSource = arrData;
            //lstTglP.DataBind();
        }

        private void LoadBreakDownTglRelease2(string tgl1, string tgl2, int DeptID)
        {
            Users users = (Users)Session["Users"]; int DeptID00 = 0;
            if (users.DeptID == 9 || users.DeptID == 24)
            {
                DeptID00 = DeptID;
            }
            else
            {
                if (RBLogistik.Checked || RBFinishing.Checked)
                {
                    DeptID00 = DeptID;
                }
                else
                {
                    DeptID00 = users.DeptID;
                }
            }

            PantauBSBuang BS = new PantauBSBuang();
            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeBS.RetrieveDataBSTglRelease2(tgl1, tgl2, DeptID00);
            lstTglP.DataSource = arrData;
            lstTglP.DataBind();
        }

        private void LoadBSBuang(string tgl1, string tgl2)
        {
        }

        protected void lstTglP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            PantauBSBuang p = (PantauBSBuang)e.Item.DataItem;
            DateTime tgl = DateTime.Parse(p.TglPotong);
            string tglP = tgl.ToString("yyyyMMdd");

            if (RBLiat.Checked == false)
            {
                if (users.DeptID == 3 && p.ApvDept == "0" && users.Apv > 0
                    || users.DeptID == 6 && p.ApvDept == "0" && users.Apv > 0
                    || users.DeptID == 10 && p.ApvDept == "0" && users.Apv > 0)
                {
                    btnApv.Enabled = true; btnApv.Visible = true;
                }
                if (users.DeptID == 3 && p.ApvDept == "0" && users.Apv == 0 && p.ApvAcc == "0"
                    || users.DeptID == 6 && p.ApvDept == "0" && users.Apv == 0 && p.ApvAcc == "0"
                    || users.DeptID == 10 && p.ApvDept == "0" && users.Apv == 0 && p.ApvAcc == "0")
                {
                    btnApv.Enabled = false; btnApv.Visible = false; btnCancel.Visible = true;
                }

                if (users.DeptID == 3 && p.ApvDept == "1" || users.DeptID == 6 && p.ApvDept == "1" || users.DeptID == 10 && p.ApvDept == "1")
                {
                    btnApv.Enabled = false;
                }
                if (users.DeptID == 9 && p.ApvQA == "0" && p.ApvDept == "1" && p.ApvAcc == "0")
                {
                    btnApv.Enabled = true;
                }
                if (users.DeptID == 9 && p.ApvQA == "1")
                {
                    btnApv.Enabled = false;
                }
                if (users.DeptID == 24 && p.ApvAcc == "0" && p.ApvQA == "1" && p.ApvDept == "1")
                {
                    btnApv.Enabled = true;
                }
                if (users.DeptID == 24 && p.ApvAcc == "1")
                {
                    btnApv.Enabled = false;
                }
            }
            else
            {
                if (RBFinishing.Checked && users.DeptID == 3)
                {
                    btnApv.Enabled = true;
                }
                else if (RBFinishing.Checked && users.DeptID != 3)
                {
                    btnApv.Enabled = false;
                }
                else if (RBLogistik.Checked && users.DeptID == 6 || RBLogistik.Checked && users.DeptID == 10)
                {
                    btnApv.Enabled = true;
                }
                else if (RBLogistik.Checked && users.DeptID != 6 || RBLogistik.Checked && users.DeptID != 10)
                {
                    btnApv.Enabled = false;
                }

                if (users.DeptID == 9 && p.ApvQA != "1" || users.DeptID == 24 && p.ApvAcc != "1")
                {
                    btnApv.Enabled = true;
                }
                else
                {
                    btnApv.Enabled = false;
                }
            }

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tglpotong");
                    if (tr != null)
                    {
                        Repeater lstBs = (Repeater)e.Item.FindControl("ListBS");
                        PantauBSBuang BS01 = new PantauBSBuang();
                        PantauBSBuangFacade FacadeBS01 = new PantauBSBuangFacade();
                        ArrayList arrData01 = new ArrayList();
                        string Flag = Session["Next"].ToString();
                        int DeptID00 = Convert.ToInt32(Session["DeptID00"]);
                        int DeptID01 = 0;
                        int LiatDeptID = Convert.ToInt32(Session["LiatDeptID"]);
                        string Lihat = Session["Liat"].ToString();

                        if (Flag == "Next")
                        {
                            DeptID01 = DeptID00;
                        }
                        else
                        {
                            DeptID01 = Convert.ToInt32(Session["DeptIDapv"]);
                        }

                        if (LiatDeptID > 0)
                        {
                            DeptID01 = LiatDeptID;
                        }

                        arrData01 = FacadeBS01.RetrieveDataBSRelease(tglP, users.DeptID, DeptID01, Lihat);

                        if (arrData01.Count > 0)
                        {
                            lstBs.DataSource = arrData01;
                            lstBs.DataBind();
                        }
                        else
                        {
                            PantauBSBuang BS = new PantauBSBuang();
                            PantauBSBuangFacade FacadeBS = new PantauBSBuangFacade();
                            ArrayList arrData = new ArrayList();
                            arrData = FacadeBS.RetrieveDataBS(tglP, users.DeptID.ToString());
                            lstBs.DataSource = arrData;
                            lstBs.DataBind();
                        }
                    }
                }
            }
            catch { }
        }

        protected void ListBS_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"]; int DeptID = Convert.ToInt32(Session["DeptIDapv"]);

            if (users.DeptID == 3 && Session["Liat"].ToString() != "Liat")
            {
                labeldept.Text = "Finishing";
            }
            else if (users.DeptID == 6 || users.DeptID == 10 && Session["Liat"].ToString() != "Liat")
            {
                labeldept.Text = "Logistik";
            }
            else if (users.DeptID == 9 || users.DeptID == 24 || users.DeptID == 10 || users.DeptID == 6 || users.DeptID == 3)
            {
                if (Session["Next"].ToString() == "Next")
                {
                    if (Convert.ToInt32(Session["DeptID00"]) == 3)
                    {
                        labeldept.Text = "Finishing"; labelJudul.Text = "PEMANTAUAN BS BUANG FINISHING";
                    }
                    else if (Convert.ToInt32(Session["DeptID00"]) == 6)
                    {
                        labeldept.Text = "Logistik"; labelJudul.Text = "PEMANTAUAN BS BUANG LOGISTIK";
                    }
                }
                if (Session["Liat"].ToString() == "Liat")
                {
                    if (Convert.ToInt32(Session["LiatDeptID"]) == 3)
                    {
                        labeldept.Text = "Finishing"; labelJudul.Text = "PEMANTAUAN BS BUANG FINISHING";
                    }
                    else if (Convert.ToInt32(Session["LiatDeptID"]) == 6)
                    {
                        labeldept.Text = "Logistik"; labelJudul.Text = "PEMANTAUAN BS BUANG LOGISTIK";
                    }
                    else if (users.DeptID == 6)
                    {
                        labeldept.Text = "Logistik"; labelJudul.Text = "PEMANTAUAN BS BUANG LOGISTIK";
                    }
                    else if (users.DeptID == 3)
                    {
                        labeldept.Text = "Finishing"; labelJudul.Text = "PEMANTAUAN BS BUANG FINISHING";
                    }
                }
                else if (Session["Next"].ToString() != "Next" && Session["Liat"].ToString() != "Liat")
                {
                    if (DeptID == 3)
                    {
                        labeldept.Text = "Finishing"; labelJudul.Text = "PEMANTAUAN BS BUANG FINISHING";
                    }
                    else if (DeptID == 6)
                    {
                        labeldept.Text = "Logistik"; labelJudul.Text = "PEMANTAUAN BS BUANG LOGISTIK";
                    }
                }
            }

            Repeater lstData;
            PantauBSBuang p = (PantauBSBuang)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                    if (tr != null)
                    {
                        string isi = Session["isi"].ToString();
                        TextBox Keterangan = (TextBox)e.Item.FindControl("Keterangan");
                        Label Keterangan1 = (Label)e.Item.FindControl("txtKeterangan");

                        if (isi == "0")
                        {
                            Keterangan.Visible = true; Keterangan1.Visible = false;
                            Keterangan1.Text = Keterangan.Text;
                        }
                        else
                        {
                            Keterangan.Visible = false; Keterangan1.Visible = true;
                            Keterangan1.Text = p.Ket;
                        }
                        //lstData = ((Repeater)(lstTglP.FindControl("ListBS")));

                        //Keterangan.Visible = false; Keterangan1.Visible = true;
                        //Keterangan1.Text = p.Ket;

                        TotalQty += p.Qty; TotalM3 += p.M3; GrandTotal += p.Qty; GrandTotalM3 += p.M3;

                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = TotalQty.ToString("N0");
                    tr2.Cells[2].InnerText = TotalM3.ToString("N2");

                    TotalQty = 0; TotalM3 = 0;
                }

                lblGrandTotal.Text = GrandTotal.ToString("N0"); lblGrandTotalM3.Text = GrandTotalM3.ToString("N2");
            }
            catch { }

            //Repeater lstData;
            //int i = 0;
            //foreach (RepeaterItem objItem0 in lstTglP.Items)
            //{
            //    HtmlTableRow tr = (HtmlTableRow)lstTglP.Items[i].FindControl("tglpotong");
            //    lstData = ((Repeater)(objItem0.FindControl("ListBS")));
            //    int ii = 0;
            //    foreach (RepeaterItem objItem in lstData.Items)
            //    {
            //        HtmlTableRow tr1 = (HtmlTableRow)lstData.Items[ii].FindControl("ps1");                
            //        TextBox Keterangan = (TextBox)lstData.Items[ii].FindControl("Keterangan");
            //        Keterangan.Text = p.Ket;

            //        ii++;
            //    }
            //    i++;
            //}
            //for (int i = 0; i < lstTglP.Items.Count; i++)
            //{
            //    lstData = ((Repeater)(lstTglP.FindControl("ListBS")));
            //    TextBox Keterangan = (TextBox)lstData.Items[i].FindControl("Keterangan");
            //    Keterangan.Text = p.Ket; 
            //}

        }

        protected void ListBS_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }

    public class PantauBSBuangFacade
    {
        public string strError = string.Empty;
        private ArrayList arrDataBS = new ArrayList();
        private PantauBSBuang BS = new PantauBSBuang();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int Release(object objDomain)
        {
            try
            {
                BS = (PantauBSBuang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglPotong", BS.TglPotong));
                sqlListParam.Add(new SqlParameter("@Partno", BS.Partno));
                sqlListParam.Add(new SqlParameter("@Qty", BS.Qty));
                sqlListParam.Add(new SqlParameter("@M3", BS.M3));
                sqlListParam.Add(new SqlParameter("@CreatedBy", BS.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PIC", BS.PIC));
                sqlListParam.Add(new SqlParameter("@DeptID", BS.DeptID));
                sqlListParam.Add(new SqlParameter("@Keterangan", BS.Ket));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "PantauBS_Release");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateApv(object objDomain)
        {
            try
            {
                BS = (PantauBSBuang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglPotong", BS.TglPotong));
                sqlListParam.Add(new SqlParameter("@DeptID", BS.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptIDBs", BS.DeptIDBs));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", BS.LastModifiedBy));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "PantauBS_Approval");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int Cancel(object objDomain)
        {
            try
            {
                BS = (PantauBSBuang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglPotong", BS.TglPotong));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "PantauBS_Cancel");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public ArrayList RetrieveDataBSRelease(string tgl, int DeptID, int DeptID01, string Tanda)
        {
            arrDataBS = new ArrayList(); int DeptID2 = 0;

            if (DeptID == 9 || DeptID == 24)
            {
                DeptID2 = DeptID01;
            }
            else if (DeptID == 10 || DeptID == 6)
            {
                if (Tanda == "Liat" && DeptID01 > 0)
                {
                    DeptID2 = DeptID01;
                }
                else
                {
                    DeptID2 = 6;
                }

            }
            else if (DeptID == 3)
            {
                if (Tanda == "Liat" && DeptID01 > 0)
                {
                    DeptID2 = DeptID01;
                }
                else
                {
                    DeptID2 = 3;
                }
            }
            else
            {
                DeptID2 = DeptID;
            }

            string strSQL =
            " select Partno,qty,M3,isnull(PIC,'-')PIC, " +
            " case when ApvDept=1 then 'Approved' else 'Open' end ApvDept, " +
            " case when ApvQA=1 then 'Approved' else 'Open' end ApvQA, " +
            " case when ApvAcc=1 then 'Approved' else 'Open' end ApvAcc,Keterangan Ket " +
            " from PantauBSBuang where rowstatus>-1 and tglpotong='" + tgl + "' and DeptID=" + DeptID2 + "";
            //" union all " +
            //" select '-'Partno,'0'Qty,'0'M3,'-'PIC,'0'ApvFin,'0'ApvQA,'0'ApvAcc,'-'Ket ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        //No = sdr["No"].ToString(),
                        Partno = sdr["Partno"].ToString(),
                        //TglPotong = sdr["TglPotong"].ToString(),
                        Qty = decimal.Parse(sdr["Qty"].ToString()),
                        M3 = decimal.Parse(sdr["M3"].ToString()),
                        PIC = sdr["PIC"].ToString(),
                        ApvQA = sdr["ApvQA"].ToString(),
                        ApvDept = sdr["ApvDept"].ToString(),
                        ApvAcc = sdr["ApvAcc"].ToString(),
                        Ket = sdr["Ket"].ToString()
                    });
                }
            }
            return arrDataBS;
        }

        //public ArrayList CekOpenLaporan()
        //{
        //    arrDataBS = new ArrayList();

        //    string strSQL =
        //    " select count(ID)ttl from PantauBSBuang where rowstatus>-1 and ApvFin<>1 and ApvQA<>1 and ApvAcc<>1 ";

        //    DataAccess da = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sdr = da.RetrieveDataByString(strSQL);

        //    if (da.Error == string.Empty && sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            arrDataBS.Add(new PantauBSBuang
        //            {
        //                ttl = Convert.ToInt32(sdr["ttl"].ToString())
        //            });
        //        }
        //    }
        //    return arrDataBS;
        //}

        public ArrayList RetrieveDataBS(string tgl, string DeptID)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string PicLogistik = string.Empty;
            string PicFinishing = string.Empty; string Lokasi1 = string.Empty; string Lokasi2 = string.Empty; string kueri = string.Empty;
            string SubQuery1 = string.Empty;

            if (users.UnitKerjaID == 1)
            {
                if (users.DeptID == 6)
                {
                    PicLogistik = " 'Ardi'PIC ";
                    Lokasi1 = "";
                    Lokasi2 = "";
                    kueri = "";
                }
                else
                {
                    PicFinishing = "Dita";
                    Lokasi1 = " ('H99') ";
                    Lokasi2 = " ('','') ";
                    kueri = "";
                }
            }
            else if (users.UnitKerjaID == 7)
            {
                if (users.DeptID == 6)
                {
                    //PicLogistik = "Pandu";
                    PicLogistik = " case when A.LokID in (select ID from FC_Lokasi where Lokasi in ('SORTIR','R99')) then 'QA Sortir' when A.LokID in (select ID from FC_Lokasi where Lokasi in ('L99')) then 'QA Inspect' end PIC ";
                    Lokasi1 = " ('') ";
                    Lokasi2 = " ('') ";
                    kueri = "";
                }
                else
                {
                    PicFinishing = "Galih";
                    Lokasi1 = " ('F99','F41','E19','F40','A99','E14') ";
                    Lokasi2 = " ('F38') ";
                    kueri = "";
                }
            }
            else if (users.UnitKerjaID == 13)
            {
                if (users.DeptID == 6)
                {
                    PicLogistik = "'Fariz'PIC";
                    Lokasi1 = "('R99','P01A','P01B','Z99','Z10','Z89','Z11','L99')";
                    Lokasi2 = "('')";
                    kueri = " and DeptID=6 ";
                }
                else
                {
                    PicFinishing = "Nanik";
                    Lokasi1 = "('F40', 'F38', 'A99', 'E19')";
                    Lokasi2 = "('')";
                    kueri = " and DeptID=3 ";
                }
            }

            arrDataBS = new ArrayList(); string Query = string.Empty;
            if (DeptID == "3")
            {
                Query =    
                " with data_Simetris1 as ( " +
                " select SerahID,TglTrans,QtyIn,ID IDSimetris,ItemID  from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99') " +
                " and ItemID in (select ID from FC_Items where PartNo like'%-S-%')  and CreatedBy in (select username from users where DeptID in (6,0)) " +
                " and SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%' or PartNo like'%-S-%' or PartNo like'%-1-%') " +
                //" and LokID in (select ID from FC_Lokasi where Lokasi in " + Lokasi1 + ") and RowStatus>-1) " +
                " and LokID in (select ID from FC_Lokasi where ID in  (select LokID from PantauBSBuang_Lokasi where Rowstatus>-1 and Keterangan='Simetris' "+ kueri + ") ) and RowStatus>-1) " +
                " and RowStatus>-1 and left(convert(char,tgltrans,112),8)='" + tgl + "'), " +

                " data_MutasiLok as (  select A1.PartNo,left(convert(char,TglPotong,103),10)TglPotong,Qty,M3 from ( " +
                " select TglTrans TglPotong,ItemID,sum(Qty)Qty,sum(M3)M3 from ( " +
                " select x.*,((A.Tebal*A.Lebar*A.Panjang)/1000000000)*x.Qty M3 from ( " +
                " select TglTrans,Qty,ItemID from T3_MutasiLok A where ItemID in (select ID from FC_Items where PartNo like'%-S-%') " +
                //" and LokID1 in (select ID from FC_Lokasi where Lokasi in " + Lokasi2 + " " +
                " and LokID1 in (select ID from FC_Lokasi where ID in  (select LokID from PantauBSBuang_Lokasi where Rowstatus>-1 and Keterangan='Mutasi Lokasi' " + kueri + ")  " +
                " and LokID2 in (select ID from FC_Lokasi where Lokasi in ('S99') and Rowstatus>-1) and " +
                " left(convert(char,tgltrans,112),8)='" + tgl + "')) as x " +
                " inner join FC_Items A ON A.ID=x.ItemID ) as xx group by TglTrans,ItemID ) as x1 inner join FC_Items A1 ON A1.ID=x1.ItemID), " +

                " data_Simetris2 as (select (RIGHT(trim(D.PartNo),2))Kode,A.ItemID,D.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.QtyIn Qty, " +
                " cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.QtyIn) as decimal(10,2))M3  from data_Simetris1 A  " +
                " left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID inner join FC_Items D ON D.ID=A.ItemID ),  " +

                " data1 as (select PartNo,TglPotong,sum(Qty)Qty,sum(M3)M3 from data_Simetris2  where kode not in ('CE','CR','CB') group by TglPotong,PartNo), " +
                " data2 as (select PartNo,TglPotong,Qty,M3 from data1 union all select PartNo,TglPotong,Qty,M3 from data_MutasiLok), " +
                " data3 as (select PartNo,TglPotong,sum(Qty)Qty,sum(M3)M3  from data2 group by TglPotong,PartNo) " +

                " select ROW_NUMBER() over (order by TglPotong asc ) as No,*,'" + PicFinishing + "'PIC,'-'ApvQA,'-'ApvDept,'-'ApvAcc,'Open'Ket from data3  ";
            }
            else if (DeptID == "6")
            {
                if (users.UnitKerjaID == 7)
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,Qty,ItemID,Noted,LokID2 LokID " +
                    " from (  select x.*,case when AA.Dept is null then '-' else AA.Dept end DeptLokasi from (  " +
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted,(select A.LokID from T3_Serah A where A.ID=SerahID)LokID2  " +
                    " from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99')  and ItemID in (select ID from FC_Items " +
                    " where PartNo like'%-S-%')  and CreatedBy in (select username from users where DeptID=6) and SerahID in (select ID from T3_Serah " +
                    " where LokID in (select ID from FC_Lokasi where Lokasi in ('SORTIR','R99','L99')))  and  " +
                    " left(convert(char,tgltrans,112),8)='" + tgl + "' and BS='LOG' and rowstatus>-1   ) as x  " +
                    " inner join FC_Lokasi AA ON AA.ID=x.LokID2 ) as x1 where DeptLokasi<>'FINISHING' " +
                    " union all " +
                    " select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted,LokID1 LokID " +
                    " from T3_MutasiLok where left(convert(char,tgltrans,112),8)='" + tgl + "' and LokID2 in (select ID from FC_Lokasi where Lokasi='S99') " +
                    " and CreatedBy in (select username from users where DeptID=6) and CreatedBy not in ('lenysj') and RowStatus>-1 ";
                }
                else if (users.UnitKerjaID == 1)
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,Qty,ItemID,Noted " +
                    " from (  select x.*,case when AA.Dept is null then '-' else AA.Dept end DeptLokasi from (  " +
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted,(select A.LokID from T3_Serah A where A.ID=SerahID)LokID2  " +
                    " from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99')  and ItemID in (select ID from FC_Items " +
                    " where PartNo like'%-S-%')  and CreatedBy in (select username from users where DeptID=6) and " +
                    " left(convert(char,tgltrans,112),8)='" + tgl + "' and BS='LOG' and rowstatus>-1   ) as x  " +
                    " inner join FC_Lokasi AA ON AA.ID=x.LokID2 ) as x1 where DeptLokasi<>'FINISHING' " +
                    " union all " +
                    " select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted " +
                    " from T3_MutasiLok where left(convert(char,tgltrans,112),8)='" + tgl + "' and LokID2 in (select ID from FC_Lokasi where Lokasi='S99') " +
                    " and CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ";
                }
                else if (users.UnitKerjaID == 13)
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,Qty,ItemID,Noted " +
                    " from (  select x.*,case when AA.Dept is null then '-' else AA.Dept end DeptLokasi from (  " +
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted,(select A.LokID from T3_Serah A where A.ID=SerahID)LokID2  " +
                    " from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99')  and ItemID in (select ID from FC_Items " +
                    " where PartNo like'%-S-%')  and CreatedBy in (select username from users where DeptID=6) and " +
                    " left(convert(char,tgltrans,112),8)='" + tgl + "' and BS='LOG' and rowstatus>-1   ) as x  " +
                    " inner join FC_Lokasi AA ON AA.ID=x.LokID2 ) as x1 where DeptLokasi<>'FINISHING' " +
                    " union all " +
                    " select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted " +
                    " from T3_MutasiLok where left(convert(char,tgltrans,112),8)='" + tgl + "' and LokID2 in (select ID from FC_Lokasi where Lokasi='S99') " +
                    " and CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ";
                }


                Query =
                " with data1 as ( " + SubQuery1 + ")," +
                " data2 as ( " +
                " select ROW_NUMBER() over (order by A.tgltrans asc ) as No,C.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.Qty, " +
                " cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.Qty) as decimal(10,4))M3," + PicLogistik + ",'-'ApvQA,'-'ApvDept,'-'ApvAcc, " +
                " 'Open'Ket,A.ItemID,Noted " +
                " from data1 A  left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1  inner join FC_Items C ON C.iD=A.ItemID ) " +

                " select No,PartNo,TglPotong,Qty,cast(M3 as decimal(10,2))M3,PIC,ApvQA,ApvDept,ApvAcc,Ket from data2 order by No ";
            }

            string strSQL = Query;

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        //No = sdr["No"].ToString(),
                        Partno = sdr["Partno"].ToString(),
                        TglPotong = sdr["TglPotong"].ToString(),
                        Qty = decimal.Parse(sdr["Qty"].ToString()),
                        M3 = decimal.Parse(sdr["M3"].ToString()),
                        PIC = sdr["PIC"].ToString(),
                        ApvQA = sdr["ApvQA"].ToString(),
                        ApvDept = sdr["ApvDept"].ToString(),
                        ApvAcc = sdr["ApvAcc"].ToString(),
                        Ket = sdr["Ket"].ToString()
                    });
                }
            }
            return arrDataBS;
        }


        public ArrayList RetrieveDataRelease(string tgl1, string tgl2, int DeptID)
        {
            arrDataBS = new ArrayList();

            string strSQL =
            " select distinct tglpotong from PantauBSBuang where rowstatus>-1 and tglpotong>='" + tgl1 + "' and tglpotong<='" + tgl2 + "'" +
            " and DeptID=" + DeptID + "";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        TglPotong = sdr["TglPotong"].ToString()
                    });
                }
            }
            return arrDataBS;
        }

        public ArrayList RetrieveDataBSTglRelease1(int DeptID, string Q2)
        {
            arrDataBS = new ArrayList();
            //if (DeptID == 0)
            //{

            //}
            //else
            //{ 

            //}
            string strSQL =
            " select ROW_NUMBER() over (order by TglPotong asc ) as No,TglPotong,isnull(ApvDept,0)ApvDept from ( " +
            " select distinct left(convert(char,tglpotong,103),10)TglPotong,ApvDept from PantauBSBuang " +
            " where Rowstatus>-1 and DeptID=" + DeptID + "  " + Q2 + ") as x order by TglPotong";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        TglPotong = sdr["TglPotong"].ToString(),
                        No = sdr["No"].ToString(),
                        ApvDept = sdr["ApvDept"].ToString()
                    });
                }
            }
            return arrDataBS;
        }

        public ArrayList RetrieveDataBSTglRelease(int DeptID)
        {
            arrDataBS = new ArrayList(); int Query = 0;
            if (DeptID == 10)
            {
                Query = 6;
            }
            else
            {
                Query = DeptID;
            }
            string strSQL =
            " select ROW_NUMBER() over (order by TglPotong asc ) as No,TglPotong,isnull(ApvDept,0)ApvDept from ( " +
            " select distinct left(convert(char,tglpotong,103),10)TglPotong,ApvDept from PantauBSBuang " +
            //" where DeptID="+DeptID+" and left(convert(char,tglpotong,112),8)>='" + tgl1 + "' and left(convert(char,tglpotong,112),8)<='" + tgl2 + "' and Rowstatus>-1 ) as x order by TglPotong";
            " where DeptID=" + Query + " and Rowstatus>-1 and apvdept=0 and apvqa=0 and apvacc=0) as x order by TglPotong";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        TglPotong = sdr["TglPotong"].ToString(),
                        No = sdr["No"].ToString(),
                        ApvDept = sdr["ApvDept"].ToString()
                    });
                }
            }
            return arrDataBS;
        }

        public ArrayList RetrieveDataBSTglRelease2(string tgl1, string tgl2, int DeptID)
        {
            arrDataBS = new ArrayList();
            string strSQL =
            " select ROW_NUMBER() over (order by TglPotong asc ) as No,TglPotong,isnull(ApvDept,0)ApvDept,isnull(ApvQA,0)ApvQA,isnull(ApvAcc,0)ApvAcc " +
            " from (select distinct left(convert(char,tglpotong,103),10)TglPotong,ApvDept,ApvQA,ApvAcc from PantauBSBuang " +
              " where DeptID=" + DeptID + " and left(convert(char,tglpotong,112),8)>='" + tgl1 + "' and left(convert(char,tglpotong,112),8)<='" + tgl2 + "' and Rowstatus>-1 ) as x order by TglPotong";
            //" where DeptID=" + DeptID + " and Rowstatus>-1 ) as x order by TglPotong";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        TglPotong = sdr["TglPotong"].ToString(),
                        No = sdr["No"].ToString(),
                        ApvDept = sdr["ApvDept"].ToString(),
                        ApvQA = sdr["ApvQA"].ToString(),
                        ApvAcc = sdr["ApvAcc"].ToString()
                    });
                }
            }
            return arrDataBS;
        }

        public ArrayList RetrieveDataBSTgl(string tgl1, string tgl2, int DeptID, string PlantID)
        {
            arrDataBS = new ArrayList(); string Query = string.Empty; string Lokasi1 = string.Empty; string Lokasi2 = string.Empty; string kueri = string.Empty;
            string SubQuery1 = string.Empty; string SubQuery2 = string.Empty;

            if (DeptID == 3)
            {
                if (PlantID == "7")
                {
                    Lokasi1 = " ('F99','F41','E19','F40','E14','A99') "; Lokasi2 = " ('F38','Z99') "; kueri = " ";
                }
                else if (PlantID == "1")
                {
                    Lokasi1 = " ('H99') "; Lokasi2 = " ('','') "; kueri = " ";
                }
                else if (PlantID == "13")
                {
                    Lokasi1 = " ('F40','F38','A99','E19') "; Lokasi2 = " ('','') "; kueri = " and DeptID=3 ";
                }

                Query =
                    //" with data1 as (select SerahID,TglTrans,QtyIn,ID IDSimetris " +
                    //" from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99') " +
                    //" and ItemID in (select ID from FC_Items where PartNo like'%-S-%') " +
                    //" and CreatedBy in (select username from users where DeptID=6) " +
                    //" and SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%') " +
                    //" and LokID in (select ID from FC_Lokasi where Lokasi='H99') and RowStatus>-1)  " +
                    //" and RowStatus>-1 and left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'), " +
                    //" data2 as (select C.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.QtyIn Qty,cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.QtyIn) as decimal(10,2))M3 " +
                    //" from data1 A   left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID ),  " +
                    //" data3 as (select TglPotong,sum(Qty)Qty,sum(M3)M3 from data2 group by TglPotong) " +
                    //" select ROW_NUMBER() over (order by TglPotong asc ) as No,* from data3 ";
                    " with data_Simetris1 as ( " +
                    " select SerahID,TglTrans,QtyIn,ID IDSimetris  from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99') " +
                    " and ItemID in (select ID from FC_Items where PartNo like'%-S-%')  and CreatedBy in (select username from users where DeptID in (6,0)) " +
                    " and SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%' or PartNo like'%-S-%' or PartNo like'%-1-%') " +
                     //" and LokID in (select ID from FC_Lokasi where Lokasi in " + Lokasi1 + ") and RowStatus>-1) " +
                     " and LokID in (select ID from FC_Lokasi where ID in  (select LokID from PantauBSBuang_Lokasi where Rowstatus>-1 and Keterangan='Simetris' "+kueri+")) and RowStatus>-1) " +
                    " and RowStatus>-1 and left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'), " +

                    " data_MutasiLok as (  select A1.PartNo,left(convert(char,TglPotong,103),10)TglPotong,Qty,M3 from ( " +
                    " select TglTrans TglPotong,ItemID,sum(Qty)Qty,sum(M3)M3 from ( " +
                    " select x.*,((A.Tebal*A.Lebar*A.Panjang)/1000000000)*x.Qty M3 from ( " +
                    " select TglTrans,Qty,ItemID from T3_MutasiLok A where ItemID in (select ID from FC_Items where PartNo like'%-S-%') " +
                    //" and LokID1 in (select ID from FC_Lokasi where Lokasi in " + Lokasi2 + " " +
                    " and LokID1 in (select ID from FC_Lokasi where ID in  (select LokID from PantauBSBuang_Lokasi where Rowstatus>-1 and Keterangan='Mutasi Lokasi' " + kueri + ")  " +
                    " and LokID2 in (select ID from FC_Lokasi where Lokasi in ('S99') and Rowstatus>-1) and " +
                    " left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "')) as x " +
                    " inner join FC_Items A ON A.ID=x.ItemID ) as xx group by TglTrans,ItemID ) as x1 inner join FC_Items A1 ON A1.ID=x1.ItemID), " +
                    " data_Simetris2 as (select C.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.QtyIn Qty, " +
                    " cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.QtyIn) as decimal(10,2))M3  from data_Simetris1 A  " +
                    " left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID ),  " +
                    " data1 as (select TglPotong,sum(Qty)Qty,sum(M3)M3 from data_Simetris2  group by TglPotong), " +
                    " data2 as (select TglPotong,Qty,M3 from data1 union all select TglPotong,Qty,M3 from data_MutasiLok), " +
                    " data3 as (select TglPotong,sum(Qty)Qty,sum(M3)M3  from data2 group by TglPotong) " +
                    " select ROW_NUMBER() over (order by TglPotong asc ) as No,* from data3  ";

            }

            else if (DeptID == 6)
            {
                if (PlantID == "7")
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted from T3_Simetris " +
                    " where LokID in (select ID from FC_Lokasi where Lokasi='S99') and ItemID in (select ID from FC_Items where PartNo like'%-S-%')" +
                    " and SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi " +
                    " in ('SORTIR','R99','L99'))) and CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 and " +
                    " left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'  " +
                    " union all  " +
                    " select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted from T3_MutasiLok where left(convert(char,tgltrans,112),8)>='" + tgl1 + "' " +
                    " and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'and  LokID2 in (select ID from FC_Lokasi where Lokasi='S99') and " +
                    " CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ";
                }
                else if (PlantID == "1")
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted from T3_Simetris " +
                    " where LokID in (select ID from FC_Lokasi where Lokasi='S99') and ItemID in (select ID from FC_Items where PartNo like'%-S-%' " +
                    " and PartNo not like'%SO%') and CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 and " +
                    " left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'  " +
                    " union all  " +
                    " select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted from T3_MutasiLok where left(convert(char,tgltrans,112),8)>='" + tgl1 + "' " +
                    " and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'and  LokID2 in (select ID from FC_Lokasi where Lokasi='S99') and " +
                    " CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ";
                }
                else if (PlantID == "13")
                {
                    SubQuery1 =
                    " select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted from T3_Simetris " +
                    " where LokID in (select ID from FC_Lokasi where Lokasi='S99') and ItemID in (select ID from FC_Items where PartNo like'%-S-%' " +
                    " and PartNo not like'%SO%') and CreatedBy in (select username from users where DeptID=6) and " +
                    " SerahID in (select ID from T3_Serah where LokID in (select ID from FC_Lokasi where Lokasi in ('R99','P01A','P01B','Z99','Z10','Z89','Z11','E19'))) and RowStatus>-1 and " +
                    " left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'  ";
                    //" union all  " +
                    //" select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted from T3_MutasiLok where left(convert(char,tgltrans,112),8)>='" + tgl1 + "' " +
                    //" and left(convert(char,tgltrans,112),8)<='" + tgl2 + "'and  LokID2 in (select ID from FC_Lokasi where Lokasi='S99') and " +
                    //" CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ";
                }

                Query =
                //" with data1 as ( "+
                //" select ID,SerahID,TglTrans,QtyIn Qty,ItemID,'Simetris'Noted  "+
                //" from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99')  "+
                //" and ItemID in (select ID from FC_Items where PartNo like'%-S-%' and PartNo not like'%SO%')  and CreatedBy in (select username from users where DeptID=6) " +
                //" and /** SerahID in (select ID from T3_Serah where  LokID in (select ID from FC_Lokasi where Lokasi='R99') and RowStatus>-1) " +
                //" and **/ RowStatus>-1 and left(convert(char,tgltrans,112),8)>='" + tgl1 + "' and left(convert(char,tgltrans,112),8)<='" + tgl2 + "' " +
                //" union all "+
                //" select ID,SerahID,TglTrans,Qty,ItemID,'Mutasian'Noted from T3_MutasiLok where left(convert(char,tgltrans,112),8)>='" + tgl1 + "' "+
                //" and left(convert(char,tgltrans,112),8)<='" + tgl2 + "' and "+
                //" LokID2 in (select ID from FC_Lokasi where Lokasi='S99') and CreatedBy in (select username from users where DeptID=6)  and RowStatus>-1 ), "+
                //" data2 as ( "+
                //" select C.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.Qty Qty,"+
                //" cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.Qty) as decimal(10,2))M3  "+
                //" from data1 A   left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID), "+
                //" data3 as (select TglPotong,sum(Qty)Qty,sum(M3)M3 from data2 group by TglPotong)   "+
                //" select ROW_NUMBER() over (order by TglPotong asc ) as No,* from data3 ";

                " with data1 as ( " + SubQuery1 + " )," +
                " data2 as (  select C.PartNo,left(convert(char,A.tgltrans,103),10)TglPotong,A.Qty Qty, " +
                " cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.Qty) as decimal(10,2))M3   from data1 A  " +
                " left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID),  " +
                " data3 as (select TglPotong,sum(Qty)Qty,sum(M3)M3 from data2 group by TglPotong) " +
                " select ROW_NUMBER() over (order by TglPotong asc ) as No,* from data3  ";
            }

            string strSQL = Query;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        TglPotong = sdr["TglPotong"].ToString(),
                        No = sdr["No"].ToString(),
                        M3 = decimal.Parse(sdr["M3"].ToString()),
                        Qty = decimal.Parse(sdr["Qty"].ToString())
                    });
                }
            }
            return arrDataBS;
        }

        public ArrayList RetrieveDataBSTtl(string tgl)
        {
            arrDataBS = new ArrayList();

            string strSQL =
            " with data1 as (select SerahID,TglTrans,QtyIn,ID IDSimetris " +
            " from T3_Simetris where LokID in (select ID from FC_Lokasi where Lokasi='S99') " +
            " and ItemID in (select ID from FC_Items where PartNo like'%-S-%') " +
            " and CreatedBy in (select username from users where DeptID=6) " +
            " and SerahID in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%-P-%') " +
            " and LokID in (select ID from FC_Lokasi where Lokasi='H99') and RowStatus>-1)  " +
            " and RowStatus>-1 and left(convert(char,tgltrans,112),8)='" + tgl + "'), " +

            " data2 as (select C.PartNo,left(convert(char,A.tgltrans,112),10)TglPotong,A.QtyIn Qty,cast((((C.Tebal*C.Lebar*C.Panjang)/1000000000)*A.QtyIn) as decimal(10,2))M3 " +
            " from data1 A   left join t3_serah B ON A.SerahID=B.ID and B.RowStatus>-1   inner join FC_Items C ON C.iD=B.ItemID ),  " +

            " data3 as (select TglPotong,sum(Qty)Qty,sum(M3)M3 from data2 group by TglPotong) " +

            " select ROW_NUMBER() over (order by TglPotong asc ) as No,* from data3 ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataBS.Add(new PantauBSBuang
                    {
                        //TglPotong = sdr["TglPotong"].ToString(),
                        //No = sdr["No"].ToString(),
                        M3 = decimal.Parse(sdr["M3"].ToString()),
                        Qty = decimal.Parse(sdr["Qty"].ToString())
                    });
                }
            }
            return arrDataBS;
        }

        public PantauBSBuang CekOpenLaporan(int DeptID)
        {
            int DeptID1 = 0;
            if (DeptID == 10)
            {
                DeptID1 = 6;
            }
            else
            {
                DeptID1 = DeptID;
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select * from PantauBSBuang where rowstatus>-1 and (ApvDept<>1 or ApvQA<>1 or ApvAcc<>1) and DeptID=" + DeptID1 + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveOpenLaporan(sqlDataReader);
                }
            }

            return new PantauBSBuang();
        }

        public PantauBSBuang RetrieveOpenLaporan(SqlDataReader sqlDataReader)
        {
            BS = new PantauBSBuang();
            BS.TglPotong = sqlDataReader["TglPotong"].ToString();
            BS.Partno = sqlDataReader["Partno"].ToString();
            return BS;
        }

        public PantauBSBuang RetrieveDeptApv(string x, int DeptID)
        {
            string Query = string.Empty; string Query1 = string.Empty;
            if (x == "Next")
            {
                Query = " Desc ";
            }
            else
            {
                Query = "";
            }

            if (DeptID == 9)
            {
                Query1 = " ApvDept=1 and ApvQA=0 and ApvAcc=0 ";
            }
            else if (DeptID == 24)
            {
                Query1 = " ApvDept=1 and ApvQA=1 and ApvAcc=0 ";
            }

            string StrSql =
            " select distinct top 1 DeptID from PantauBSBuang where " + Query1 + " and Rowstatus>-1 order by DeptID " + Query + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveTgl(sqlDataReader);
                }
            }

            return new PantauBSBuang();
        }

        public PantauBSBuang Retrievettl(int DeptID)
        {
            string Query = string.Empty;
            if (DeptID == 9)
            {
                Query = " and apvDept=1 and apvqa=0 and ";
            }
            else if (DeptID == 24)
            {
                Query = " and apvDept=1 and apvqa=1 and apvacc=0 and ";
            }
            string StrSql =
            " select count(Ttl)ttl from (select distinct(DeptID)Ttl from PantauBSBuang where rowstatus>-1 " + Query + " Rowstatus>-1) as x ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveTtl(sqlDataReader);
                }
            }

            return new PantauBSBuang();
        }

        public PantauBSBuang GenerateObject_RetrieveTtl(SqlDataReader sqlDataReader)
        {
            BS = new PantauBSBuang();
            BS.ttl = Convert.ToInt32(sqlDataReader["ttl"].ToString());

            return BS;
        }

        public PantauBSBuang GenerateObject_RetrieveTgl(SqlDataReader sqlDataReader)
        {
            BS = new PantauBSBuang();
            BS.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());

            return BS;
        }

    }

    public class PantauBSBuang
    {
        public int DeptIDBs { get; set; }
        public int DeptID { get; set; }
        public int ttl { get; set; }
        public string Partno { get; set; }
        public decimal Qty { get; set; }
        public decimal M3 { get; set; }
        public string TglPotong { get; set; }
        public DateTime Createdtime { get; set; }
        public string No { get; set; }
        public string PIC { get; set; }
        public string ApvQA { get; set; }
        public string ApvFin { get; set; }
        public string ApvAcc { get; set; }
        public string Ket { get; set; }
        public string CreatedBy { get; set; }
        public string ApvDept { get; set; }
        public string Keterangan { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }

}