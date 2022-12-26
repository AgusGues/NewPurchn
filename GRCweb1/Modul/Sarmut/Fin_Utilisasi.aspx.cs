using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.SarMut
{
    public partial class Fin_Utilisasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                PanelListPlank.Visible = false;
                PanelBukanListPlank.Visible = true;
                LoadBulan();
                LoadTahun();
                Session["Nilai"] = null;
                RBUti.Checked = true;
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void RBUti_CheckedChanged(object sender, EventArgs e)
        {
            RBUtiListP.Checked = false; RBUti.Checked = true;
            PanelBukanListPlank.Visible = true; PanelListPlank.Visible = false;
            LoadPreview();
        }

        protected void RBUtiListP_CheckedChanged(object sender, EventArgs e)
        {
            RBUti.Checked = false; RBUtiListP.Checked = true; btnSimpan.Visible = false;
            PanelBukanListPlank.Visible = false; PanelListPlank.Visible = true;
            LoadPreviewListPlank();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            Session["Nilai"] = null;
            btnSimpan.Enabled = true;
            Response.Redirect("Fin_Utilisasi.aspx");
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            if (RBUti.Checked == true)
            {
                //Utiliasi I ( OK - KW dari BP )
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst1");
                    if (tr.Cells[0].InnerHtml != "")
                    {
                        DomainFinUtilisasi dSave = new DomainFinUtilisasi();
                        FacadeFinUtilisasi fSave = new FacadeFinUtilisasi();
                        int intResult = 0;

                        TextBox Keterangan = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                        dSave.Bulan = ddlBulan.SelectedValue;
                        dSave.Tahun = ddlTahun.SelectedValue;
                        dSave.Partno = tr.Cells[1].InnerHtml.ToString().Replace("&nbsp;", "").Trim();
                        dSave.Keterangan = Keterangan.Text.ToString() != "" ? Keterangan.Text.ToString() : "-";
                        dSave.QtyLembar = Convert.ToDecimal(tr.Cells[2].InnerHtml);
                        dSave.QtyM3 = Convert.ToDecimal(tr.Cells[3].InnerHtml.ToString().Replace(".", ",").Trim());
                        dSave.QtyAktualM3 = Convert.ToDecimal(tr.Cells[4].InnerHtml.ToString().Replace(".", ",").Trim());
                        dSave.CreatedBy = user.UserName.Trim();

                        intResult = fSave.SimpanUtilisasi_I(dSave);

                    }

                }

                //Utiliasi II ( BP dari Tahap I )
                for (int i = 0; i < lstMatrix2.Items.Count; i++)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)lstMatrix2.Items[i].FindControl("lst2");
                    if (tr2.Cells[0].InnerHtml != "")
                    {
                        DomainFinUtilisasi dSave2 = new DomainFinUtilisasi();
                        FacadeFinUtilisasi fSave2 = new FacadeFinUtilisasi();
                        int intResult2 = 0;

                        dSave2.Bulan = ddlBulan.SelectedValue;
                        dSave2.Tahun = ddlTahun.SelectedValue;
                        dSave2.Partno = tr2.Cells[1].InnerHtml.ToString().Replace("&nbsp;", "").Trim();
                        dSave2.QtyM3 = Convert.ToDecimal(tr2.Cells[2].InnerHtml.ToString().Replace(".", ",").Trim());
                        dSave2.CreatedBy = user.UserName.Trim();

                        intResult2 = fSave2.SimpanUtilisasi_II(dSave2);
                    }

                }

                //Utiliasi III ( BP dari BJ )
                for (int i = 0; i < lstMatrix3.Items.Count; i++)
                {
                    HtmlTableRow tr3 = (HtmlTableRow)lstMatrix3.Items[i].FindControl("lst3");
                    if (tr3.Cells[0].InnerHtml != "")
                    {
                        DomainFinUtilisasi dSave3 = new DomainFinUtilisasi();
                        FacadeFinUtilisasi fSave3 = new FacadeFinUtilisasi();
                        int intResult3 = 0;

                        dSave3.Bulan = ddlBulan.SelectedValue;
                        dSave3.Tahun = ddlTahun.SelectedValue;
                        dSave3.Partno = tr3.Cells[1].InnerHtml.ToString().Replace("&nbsp;", "").Trim();
                        dSave3.QtyM3 = Convert.ToDecimal(tr3.Cells[2].InnerHtml.ToString().Replace(".", ",").Trim());
                        dSave3.CreatedBy = user.UserName.Trim();

                        intResult3 = fSave3.SimpanUtilisasi_III(dSave3);
                    }

                }

                //Utiliasi ListPlank
                DomainFinUtilisasi dSave4 = new DomainFinUtilisasi();
                FacadeFinUtilisasi fSave4 = new FacadeFinUtilisasi();
                int intResult4 = 0;

                ArrayList arrDataLP = (ArrayList)Session["DataListPlank"];
                //int i2 = 0;
                foreach (DomainFinUtilisasi DataLP in arrDataLP)
                {
                    //dSave4.Partno = DataLP.Partno;
                    if (DataLP.Utilisasi == "")
                    {
                        dSave4.Bulan = ddlBulan.SelectedValue;
                        dSave4.Tahun = ddlTahun.SelectedValue;
                        dSave4.QtyBPM3 = Convert.ToDecimal(DataLP.QtyM3_LP.ToString().Replace(".", ",").Trim());
                        dSave4.QtyOKM3 = Convert.ToDecimal(DataLP.QtyM32.ToString().Replace(".", ",").Trim());
                        dSave4.CreatedBy = user.UserName.Trim();

                        intResult4 = fSave4.SimpanUtilisasiLP(dSave4);
                    }

                }
            }

            DisplayAJAXMessage(this, " Data berhasil disimpan !! "); return;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnCetak_Click(object sender, EventArgs e)
        {
            string strQuery0 = string.Empty;
            FacadeFinUtilisasi facadeLap0 = new FacadeFinUtilisasi();
            strQuery0 = facadeLap0.RetrieveJudul();

            string strQuery1 = string.Empty;
            FacadeFinUtilisasi facadeLap = new FacadeFinUtilisasi();
            strQuery1 = facadeLap.RetrieveLapUti(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));

            string strQuery2 = string.Empty;
            FacadeFinUtilisasi facadeLap2 = new FacadeFinUtilisasi();
            strQuery2 = facadeLap2.RetrieveLapUti2(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));

            Session["Query0"] = strQuery0;
            Session["Query1"] = strQuery1;
            Session["Query2"] = strQuery2;

            Cetak(this);
            //Cetak1(this);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst1");
                    TextBox Ket = (TextBox)lstMatrix.Items[i].FindControl("Keterangan");
                    Label Keterangan = (Label)lstMatrix.Items[i].FindControl("txtKeterangan");

                    //Ket.Visible = true;
                    //string tes = Ket.Text;

                    if (Ket != null) { Ket.Visible = false; }
                    Keterangan.Text = Ket.Text.ToString();
                    if (Keterangan != null) { Keterangan.Visible = true; }

                    tr.Cells[4].Attributes.Add("style", "background-color:White;");
                }
            }

            if (RBUtiListP.Checked == true)
            {
                Users user = (Users)Session["Users"];

                if (user.UnitKerjaID == 7)
                {
                    LabelNoFormLP.Visible = true; LabelNoFormLP.Text = "Form No. PRD/K/LUBT/100/18/R0";
                }
                else if (user.UnitKerjaID == 1)
                {
                    LabelNoFormLP.Visible = true; LabelNoFormLP.Text = "";
                }
                else if (user.UnitKerjaID == 13)
                {
                    LabelNoFormLP.Visible = true; LabelNoFormLP.Text = "";
                }

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                Response.AddHeader("content-disposition", "attachment;filename=LapUtilisasi.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                StringWriter sw2 = new StringWriter();
                HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                tbl2.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                string Contents2 = sw2.ToString();
                Contents2 = Contents2.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Contents);
                Response.Flush();
                Response.End();
            }
            else if (RBUti.Checked == true)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                Response.AddHeader("content-disposition", "attachment;filename=LapUtilisasi.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                StringWriter sw2 = new StringWriter();
                HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
                tbl.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                string Contents2 = sw2.ToString();
                Contents2 = Contents2.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Contents);
                Response.Flush();
                Response.End();
            }

            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Buffer = true;
            //Response.BufferOutput = true;
            //Response.AddHeader("content-disposition", "attachment;filename=LapUtilisasi.xls");
            //Response.Charset = "utf-8";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //StringWriter sw2 = new StringWriter();
            //HtmlTextWriter hw2 = new HtmlTextWriter(sw2);          
            //tbl.RenderControl(hw);       
            //string Contents = sw.ToString();
            //Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            //string Contents2 = sw2.ToString();
            //Contents2 = Contents2.Replace("border=\"0\"", "border=\"1\"");
            //Response.Write(Contents);
            //Response.Flush();
            //Response.End();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadPreview();

            Users user = (Users)Session["Users"];

            if (user.UnitKerjaID == 7)
            {
                LabelNoForm.Visible = true; LabelNoForm.Text = "Form No. PRD/K/LUBT/100/18/R0";
            }
            else if (user.UnitKerjaID == 1)
            {
                LabelNoForm.Visible = true; LabelNoForm.Text = "";
            }

            LabelPeriode.Visible = true;
            LabelPeriode.Text = "LAPORAN UTILISASI BULAN  " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text + "";

            LabelPeriodeLP.Visible = true;
            LabelPeriodeLP.Text = "LAPORAN UTILISASI BULAN  " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text + "";
            //update sarmut
            string actual = Session["nilaiakhir"].ToString();
            string sarmutPrs = "Utilisasi dari produk BP";
            int deptid = getDeptID("finishing");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                " and Bulan=" + ddlBulan.SelectedValue +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            SqlDataReader sdr1 = zl1.Retrieve();
        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected void LoadPreviewListPlank()
        {
            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10) { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }

            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;

            Users user = (Users)Session["Users"];

            //if (user.UnitKerjaID == 7)
            //{
            //    LabelNoForm.Visible = true; LabelNoForm.Text = "Form No. PRD/K/LUBT/100/18/R0";
            //}
            //else if (user.UnitKerjaID == 1)
            //{
            //    LabelNoForm.Visible = true; LabelNoForm.Text = "";
            //}

            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddlTahun.SelectedValue.ToString();
            if (Bulan1 < 10) { string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2; }
            else { string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2; }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;

            DomainFinUtilisasi DBMlp = new DomainFinUtilisasi();
            FacadeFinUtilisasi FBMlp = new FacadeFinUtilisasi();
            //int CekInputBJdariBP = FBM.RetrieveInputan(PeriodeBulanTahun);
            //Session["Cek"] = CekInputBJdariBP;
            int CekInput = 0;
            //#region Ada inputan
            //if (CekInput > 0)
            //{
            //    btnSimpan.Visible = false; btnCancel.Visible = true;       

            //    ArrayList arrData2 = new ArrayList();
            //    FacadeBMReportMMS Fbm2 = new FacadeBMReportMMS();

            //    if (user.UnitKerjaID == 1)
            //    {
            //        arrData2 = Fbm2.RetrieveReportMMS2(PeriodeBulanTahun); Session["Data2"] = arrData2;
            //        lstMatrix.DataSource = arrData2;
            //        lstMatrix.DataBind();
            //    }
            //    else if (user.UnitKerjaID == 7)
            //    {
            //        arrData2 = Fbm2.RetrieveReportFlo2Karawang(PeriodeBulanTahun); Session["Data2"] = arrData2;
            //        lstMatrixK2.DataSource = arrData2;
            //        lstMatrixK2.DataBind();
            //    }
            //}
            //#endregion
            #region Belum ada inputan
            if (CekInput == 0)
            {
                //btnSimpan.Visible = true; btnCancel.Visible = false;

                ArrayList arrData = new ArrayList(); ArrayList arrDataLp = new ArrayList();
                ArrayList arrData2 = new ArrayList();
                ArrayList arrData3 = new ArrayList();

                FacadeFinUtilisasi Fbm = new FacadeFinUtilisasi();
                FacadeFinUtilisasi Fbm2 = new FacadeFinUtilisasi();
                FacadeFinUtilisasi Fbm3 = new FacadeFinUtilisasi();

                FacadeFinUtilisasi Fbmlp = new FacadeFinUtilisasi();

                if (user.UnitKerjaID == 7 || user.UnitKerjaID == 1)
                {
                    arrDataLp = Fbm.RetrieveListLP(PeriodeBulanTahun);
                    lstListPlank.DataSource = arrDataLp;
                    lstListPlank.DataBind();
                }
            }
            #endregion
        }
        protected void LoadPreview()
        {
            //LoadPreviewListPlank();
            PanelListPlank.Visible = false; PanelBukanListPlank.Visible = true;

            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10) { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }

            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;

            Users user = (Users)Session["Users"];

            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddlTahun.SelectedValue.ToString();
            if (Bulan1 < 10) { string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2; }
            else { string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2; }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;

            DomainFinUtilisasi DBM = new DomainFinUtilisasi();
            FacadeFinUtilisasi FBM = new FacadeFinUtilisasi();
            int CekInput = FBM.RetrieveInputan(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));

            #region Ada inputan
            if (CekInput > 0)
            {
                Session["StatusInputan"] = "1";

                LabelStatus.Visible = true; LabelStatus.Text = "Status Laporan : Release";
                btnSimpan.Visible = false; btnCancel.Visible = true; btnCetak.Visible = true;

                ArrayList arrDataIsi1 = new ArrayList(); ArrayList arrDataIsi2 = new ArrayList(); ArrayList arrDataIsi3 = new ArrayList();
                FacadeFinUtilisasi FacadeFin = new FacadeFinUtilisasi();
                FacadeFinUtilisasi FacadeFin2 = new FacadeFinUtilisasi();
                FacadeFinUtilisasi FacadeFin3 = new FacadeFinUtilisasi();

                arrDataIsi1 = FacadeFin.RetrieveData1(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));
                lstMatrix.DataSource = arrDataIsi1;
                lstMatrix.DataBind();

                arrDataIsi2 = FacadeFin2.RetrieveData2(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));
                lstMatrix2.DataSource = arrDataIsi2;
                lstMatrix2.DataBind();

                arrDataIsi3 = FacadeFin3.RetrieveData3(Convert.ToInt32(ddlBulan.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));
                lstMatrix3.DataSource = arrDataIsi3;
                lstMatrix3.DataBind();

            }
            #endregion

            #region Belum ada inputan
            if (CekInput == 0)
            {
                Session["StatusInputan"] = "0";
                //btnSimpan.Visible = true; btnCancel.Visible = false;
                LabelStatus.Visible = true; LabelStatus.Text = "Status Laporan : Open";
                btnCetak.Visible = false;

                ArrayList arrData = new ArrayList();
                ArrayList arrData2 = new ArrayList();
                ArrayList arrData3 = new ArrayList();
                ArrayList arrDataLp = new ArrayList();

                FacadeFinUtilisasi Fbm = new FacadeFinUtilisasi();
                FacadeFinUtilisasi Fbm2 = new FacadeFinUtilisasi();
                FacadeFinUtilisasi Fbm3 = new FacadeFinUtilisasi();

                if (user.UnitKerjaID == 7 || user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    arrDataLp = Fbm.RetrieveListLP(PeriodeBulanTahun);
                    Session["DataListPlank"] = arrDataLp;

                    arrData = Fbm.RetrieveBJdariBP(PeriodeBulanTahun);
                    lstMatrix.DataSource = arrData;
                    lstMatrix.DataBind();

                    arrData2 = Fbm2.RetrieveBPdariT1(PeriodeBulanTahun);
                    lstMatrix2.DataSource = arrData2;
                    lstMatrix2.DataBind();

                    arrData3 = Fbm3.RetrieveBPdariBJ(PeriodeBulanTahun);
                    lstMatrix3.DataSource = arrData3;
                    lstMatrix3.DataBind();
                }
            }
            #endregion
        }
        private decimal TotalPakai = 0;
        private decimal TotalStd = 0;

        protected void lstMatrix_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //decimal TotalLembar = 0; decimal TotalM3 = 0; decimal TotalM3Aktual = 0;
            DomainFinUtilisasi UtiFin = (DomainFinUtilisasi)e.Item.DataItem;
            Users user = (Users)Session["Users"];

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst1");

                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    TextBox Ket = (TextBox)tr.Cells[i].FindControl("Keterangan");

                    Label LabelKet = (Label)tr.Cells[i].FindControl("txtKeterangan");
                    if (UtiFin.M3 != UtiFin.M3Other)
                    {
                        tr.Cells[i].Attributes.Add("style", "font-weight:bold; color:Blue;");
                        Ket.Visible = false; LabelKet.Visible = true;
                        //LabelKet.Text = "Terdapat produk dari Produk SR";  
                        LabelKet.Text = "Terdapat Produk SR";
                    }
                    HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                    trfooter.Cells[0].InnerHtml = "&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;" + "<b> TOTAL </b>";
                }
            }
        }
        protected void lstMatrix2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst2");

                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF2");
                    trfooter.Cells[0].InnerHtml = "&emsp;&emsp;&emsp;&emsp;&emsp;" + "<b> TOTAL </b>";
                }
            }
        }

        protected void lstMatrix3_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DomainFinUtilisasi d1 = new DomainFinUtilisasi();
            FacadeFinUtilisasi f1 = new FacadeFinUtilisasi();
            Decimal TotalM31 = f1.RetrieveTotalM3();

            DomainFinUtilisasi d2 = new DomainFinUtilisasi();
            FacadeFinUtilisasi f2 = new FacadeFinUtilisasi();
            Decimal TotalM32 = f2.RetrieveTotalM3_2();

            DomainFinUtilisasi d3 = new DomainFinUtilisasi();
            FacadeFinUtilisasi f3 = new FacadeFinUtilisasi();
            Decimal TotalM33 = f3.RetrieveTotalM3_3();

            DomainFinUtilisasi d4 = new DomainFinUtilisasi();
            FacadeFinUtilisasi f4 = new FacadeFinUtilisasi();
            Decimal TotalM34 = f4.RetrieveTotalM3_Aktual();

            Decimal TotalBJBP = 0; Decimal TotalBJBP2 = 0; Decimal TotalBPT1 = 0; Decimal TotalBPBJ = 0; Decimal TotalLP1 = 0; Decimal TotalLP2 = 0;
            if (TotalM31 > 0)
            {
                if (Session["StatusInputan"].ToString() == "1")
                {
                    DomainFinUtilisasi d01 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f01 = new FacadeFinUtilisasi();
                    TotalBJBP = f01.RetrieveTotalBJBPM3(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());

                    DomainFinUtilisasi d02 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f02 = new FacadeFinUtilisasi();
                    TotalBJBP2 = f01.RetrieveTotalBJBP2M3(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());

                    DomainFinUtilisasi d03 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f03 = new FacadeFinUtilisasi();
                    TotalBPT1 = f03.RetrieveTotalBPT1(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());

                    DomainFinUtilisasi d04 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f04 = new FacadeFinUtilisasi();
                    TotalBPBJ = f04.RetrieveTotalBPBJ(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());

                    DomainFinUtilisasi d05 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f05 = new FacadeFinUtilisasi();
                    TotalLP1 = f05.RetrieveTotalLP1(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());

                    DomainFinUtilisasi d06 = new DomainFinUtilisasi();
                    FacadeFinUtilisasi f06 = new FacadeFinUtilisasi();
                    TotalLP2 = f06.RetrieveTotalLP2(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString());
                }

                DomainFinUtilisasi DUti1 = new DomainFinUtilisasi();
                FacadeFinUtilisasi FUti1 = new FacadeFinUtilisasi();
                Decimal LPBawah = f1.RetrieveTotalLP2();

                DomainFinUtilisasi DUti2 = new DomainFinUtilisasi();
                FacadeFinUtilisasi FUti2 = new FacadeFinUtilisasi();
                Decimal LPAtas = f1.RetrieveTotalLP1();




                if (Session["StatusInputan"].ToString() == "0")
                {
                    LabelM31.Visible = true; LabelM31.Text = TotalM34.ToString("N2");
                    LabelM32.Visible = true; LabelM32.Text = TotalM32.ToString("N2") + "" + "+" + "" + TotalM33.ToString("N2");

                    LabelM31_Copy.Visible = true; LabelM31_Copy.Text = LabelM31.Text;
                    LabelM32_ttl.Visible = true; LabelM32_ttl.Text = (Convert.ToDecimal(TotalM32) + Convert.ToDecimal(TotalM33)).ToString("N2");

                    LabelNilai.Visible = true; LabelNilai.Text = ((Convert.ToDecimal(LabelM31_Copy.Text) / (Convert.ToDecimal(LabelM32_ttl.Text)) * 100).ToString("N0")) + "%";

                    LabelTotalP.Visible = true; LabelTotalP.Text = "TOTAL PENCAPAIAN =";
                    LabelTotalAtas.Visible = true; LabelTotalAtas.Text = (Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(LPAtas)).ToString();
                    LabelTotalBawah.Visible = true; LabelTotalBawah.Text = (Convert.ToDecimal(LabelM32_ttl.Text) + Convert.ToDecimal(LPBawah)).ToString();

                    LabelNilaiAkhir.Visible = true;
                    LabelNilaiAkhir.Text = ((Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(LPAtas)) / ((Convert.ToDecimal(LabelM32_ttl.Text) +
                        Convert.ToDecimal(LPBawah))) * 100).ToString("N0") + "%";
                    Session["nilaiakhir"] = null;
                    Session["nilaiakhir"] = ((Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(LPAtas)) / ((Convert.ToDecimal(LabelM32_ttl.Text) +
                        Convert.ToDecimal(LPBawah))) * 100).ToString("N0");

                    LTotal.Visible = true; LTotal.Text = TotalM31.ToString("N2");
                    LTotal2.Visible = true; LTotal2.Text = TotalM32.ToString("N2");
                    LTotal3.Visible = true; LTotal3.Text = TotalM33.ToString("N2");

                    LTotalAktual.Visible = true;
                    LTotalAktual.Text = TotalM34.ToString("N2");
                }
                else
                {
                    LabelM31.Visible = true; LabelM31.Text = TotalBJBP2.ToString("N2");
                    LabelM32.Visible = true; LabelM32.Text = TotalBPT1.ToString("N2") + "" + "+" + "" + TotalBPBJ.ToString("N2");

                    LabelM31_Copy.Visible = true; LabelM31_Copy.Text = LabelM31.Text;
                    LabelM32_ttl.Visible = true; LabelM32_ttl.Text = (Convert.ToDecimal(TotalBPT1) + Convert.ToDecimal(TotalBPBJ)).ToString("N2");

                    LabelNilai.Visible = true; LabelNilai.Text = ((Convert.ToDecimal(LabelM31_Copy.Text) / (Convert.ToDecimal(LabelM32_ttl.Text)) * 100).ToString("N0")) + "%";

                    LabelTotalP.Visible = true; LabelTotalP.Text = "TOTAL PENCAPAIAN =";
                    LabelTotalAtas.Visible = true; LabelTotalAtas.Text = (Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(TotalLP2)).ToString();
                    LabelTotalBawah.Visible = true; LabelTotalBawah.Text = (Convert.ToDecimal(LabelM32_ttl.Text) + Convert.ToDecimal(TotalLP1)).ToString();

                    LabelNilaiAkhir.Visible = true;
                    LabelNilaiAkhir.Text = ((Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(TotalLP2)) / ((Convert.ToDecimal(LabelM32_ttl.Text) +
                        Convert.ToDecimal(TotalLP1))) * 100).ToString("N0") + "%";
                    Session["nilaiakhir"] = null;
                    Session["nilaiakhir"] = ((Convert.ToDecimal(LabelM31_Copy.Text) + Convert.ToDecimal(TotalLP1)) / ((Convert.ToDecimal(LabelM32_ttl.Text) +
                        Convert.ToDecimal(TotalLP1))) * 100).ToString("N0");

                    LTotal.Visible = true; LTotal.Text = TotalBJBP.ToString("N2");
                    LTotalAktual.Visible = true; LTotalAktual.Text = TotalBJBP2.ToString("N2");

                    LTotal2.Visible = true; LTotal2.Text = TotalBPT1.ToString("N2");
                    LTotal3.Visible = true; LTotal3.Text = TotalBPBJ.ToString("N2");

                    //LabelM31.Visible = true; LabelM31.Text = TotalBJBP2.ToString("N2");
                    //LabelM32.Visible = true; LabelM32.Text = TotalBPT1.ToString("N2") + "" + "+" + "" + TotalBPBJ.ToString("N2");
                }

                //LTotalAktual.Visible = true; 
                //LTotalAktual.Text = TotalM34.ToString("N2");
            }

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst3");

                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF3");
                    trfooter.Cells[0].InnerHtml = "&emsp;&emsp;&emsp;&emsp;&emsp;" + "<b> TOTAL </b>";
                }
            }

        }
        protected void lstListPlank_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DomainFinUtilisasi d1LP = new DomainFinUtilisasi();
            FacadeFinUtilisasi f1LP = new FacadeFinUtilisasi();
            Decimal TotalLP1 = f1LP.RetrieveTotalLP1();

            DomainFinUtilisasi d2LP = new DomainFinUtilisasi();
            FacadeFinUtilisasi f2LP = new FacadeFinUtilisasi();
            Decimal TotalLP2 = f2LP.RetrieveTotalLP2();

            DomainFinUtilisasi d3 = new DomainFinUtilisasi();
            FacadeFinUtilisasi f3 = new FacadeFinUtilisasi();
            Decimal TotalM33 = f3.RetrieveTotalM3_3();

            if (TotalLP1 > 0 || TotalLP2 > 0)
            {
                LabelLP1.Visible = true; LabelLP1.Text = TotalLP1.ToString("N1");
                LabelLP2.Visible = true; LabelLP2.Text = TotalLP2.ToString("N1");
                LabelNilaiLP.Visible = true;
                LabelNilaiLP.Text = ((Convert.ToDecimal(LabelLP1.Text) / Convert.ToDecimal(LabelLP2.Text)) * 100).ToString("N0") + "%";
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LapUtilisasi', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void Cetak1(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LapUtilisasi1', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //protected void lstMatrixK2_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    decimal totalOutL1 = 0; decimal totalSPBL1 = 0; decimal totalEfesiensiL1 = 0; decimal pakaiBBL1 = 0;
        //    decimal totalOutL2 = 0; decimal totalSPBL2 = 0; decimal totalEfesiensiL2 = 0; decimal pakaiBBL2 = 0;
        //    decimal totalOutL3 = 0; decimal totalSPBL3 = 0; decimal totalEfesiensiL3 = 0; decimal pakaiBBL3 = 0;
        //    decimal totalOutL4 = 0; decimal totalSPBL4 = 0; decimal totalEfesiensiL4 = 0; decimal pakaiBBL4 = 0;
        //    decimal totalOutL5 = 0; decimal totalSPBL5 = 0; decimal totalEfesiensiL5 = 0; decimal pakaiBBL5 = 0;
        //    decimal totalOutL6 = 0; decimal totalSPBL6 = 0; decimal totalEfesiensiL6 = 0; decimal pakaiBBL6 = 0;
        //    Users user = (Users)Session["Users"];

        //    for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //    {
        //        Label txtjml = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL1");
        //        Label txtjml2 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL2");
        //        Label txtjml3 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL3");
        //        Label txtjml4 = (Label)lstMatrixK2.Items[i].FindControl("txtQtyPakaiL4");
        //        Label txtKet = (Label)lstMatrixK2.Items[i].FindControl("txtKeterangan");
        //        TextBox Ket = (TextBox)lstMatrixK2.Items[i].FindControl("Keterangan");

        //        HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //        string tgl = tr.Cells[0].InnerHtml;
        //        DomainBMReportMMS bm = new DomainBMReportMMS();
        //        FacadeBMReportMMS fbm = new FacadeBMReportMMS();
        //        //bm = fbm.Retrievetgl(tgl);

        //        if (bm.ID > 0)
        //        {

        //            //tr.Cells[5].InnerHtml = bm.PPML1.ToString();
        //            //tr.Cells[10].InnerHtml = bm.PPML2.ToString();
        //            //tr.Cells[15].InnerHtml = bm.PPML3.ToString();
        //            //tr.Cells[20].InnerHtml = bm.PPML4.ToString();

        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");                

        //            Ket.Text = bm.Keterangan.Trim();
        //            txtKet.Text = Ket.Text;

        //            txtjml.Text = jml.Text;
        //            txtjml2.Text = jmlL2.Text;
        //            txtjml3.Text = jmlL3.Text;
        //            txtjml4.Text = jmlL4.Text;

        //            //jml.Text = Convert.ToDecimal(bm.PakaiBBL1).ToString();
        //            //jmlL2.Text = Convert.ToDecimal(bm.PakaiBBL2).ToString();
        //            //jmlL3.Text = Convert.ToDecimal(bm.PakaiBBL3).ToString();
        //            //jmlL4.Text = Convert.ToDecimal(bm.PakaiBBL4).ToString();
        //            //jmlL5.Text = Convert.ToDecimal(bm.PakaiBBL5).ToString();
        //            //jmlL6.Text = Convert.ToDecimal(bm.PakaiBBL6).ToString();

        //            string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
        //            string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
        //            string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
        //            string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
        //            string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
        //            string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

        //            decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
        //            decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
        //            decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
        //            decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
        //            decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
        //            decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

        //            pakaiBBL1 += QtyPakaiBBD;
        //            pakaiBBL2 += QtyPakaiBBDL2;
        //            pakaiBBL3 += QtyPakaiBBDL3;
        //            pakaiBBL4 += QtyPakaiBBDL4;
        //            pakaiBBL5 += QtyPakaiBBDL5;
        //            pakaiBBL6 += QtyPakaiBBDL6;

        //            //string jumlahL1 = Convert.ToDecimal(bm.PakaiBBL1).ToString();

        //            LabelStatus.Visible = true; LabelStatus.Attributes["style"] = "color:blue; font-weight:bold;"; LabelStatus.Font.Name = "calibri";
        //            LabelStatus.Text = "Status Laporan : Release";
        //        }
        //        else if (bm.ID == 0)
        //        {
        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");

        //            string QtyPakaiBB = jml.Text.ToString() != "" ? jml.Text.ToString() : "0";
        //            string QtyPakaiBBL2 = jmlL2.Text.ToString() != "" ? jmlL2.Text.ToString() : "0";
        //            string QtyPakaiBBL3 = jmlL3.Text.ToString() != "" ? jmlL3.Text.ToString() : "0";
        //            string QtyPakaiBBL4 = jmlL4.Text.ToString() != "" ? jmlL4.Text.ToString() : "0";
        //            string QtyPakaiBBL5 = jmlL5.Text.ToString() != "" ? jmlL5.Text.ToString() : "0";
        //            string QtyPakaiBBL6 = jmlL6.Text.ToString() != "" ? jmlL6.Text.ToString() : "0";

        //            decimal QtyPakaiBBD = Convert.ToDecimal(QtyPakaiBB);
        //            decimal QtyPakaiBBDL2 = Convert.ToDecimal(QtyPakaiBBL2);
        //            decimal QtyPakaiBBDL3 = Convert.ToDecimal(QtyPakaiBBL3);
        //            decimal QtyPakaiBBDL4 = Convert.ToDecimal(QtyPakaiBBL4);
        //            decimal QtyPakaiBBDL5 = Convert.ToDecimal(QtyPakaiBBL5);
        //            decimal QtyPakaiBBDL6 = Convert.ToDecimal(QtyPakaiBBL6);

        //            pakaiBBL1 += QtyPakaiBBD;
        //            pakaiBBL2 += QtyPakaiBBDL2;
        //            pakaiBBL3 += QtyPakaiBBDL3;
        //            pakaiBBL4 += QtyPakaiBBDL4;
        //            pakaiBBL5 += QtyPakaiBBDL5;
        //            pakaiBBL6 += QtyPakaiBBDL6;

        //            LabelStatus.Visible = true;
        //            LabelStatus.Text = "Status Laporan : Open";
        //            LabelStatus.Attributes["style"] = "color:green; font-weight:bold;";
        //            LabelStatus.Font.Name = "calibri";
        //        }

        //        totalOutL1 += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
        //        totalSPBL1 += tr.Cells[3].InnerHtml != "" ? decimal.Parse(tr.Cells[3].InnerHtml) : 0;
        //        totalEfesiensiL1 += tr.Cells[4].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[4].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL2 += tr.Cells[6].InnerHtml != "" ? decimal.Parse(tr.Cells[6].InnerHtml) : 0;
        //        totalSPBL2 += tr.Cells[8].InnerHtml != "" ? decimal.Parse(tr.Cells[8].InnerHtml) : 0;
        //        totalEfesiensiL2 += tr.Cells[9].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[9].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL3 += tr.Cells[11].InnerHtml != "" ? decimal.Parse(tr.Cells[11].InnerHtml) : 0;
        //        totalSPBL3 += tr.Cells[13].InnerHtml != "" ? decimal.Parse(tr.Cells[13].InnerHtml) : 0;
        //        totalEfesiensiL3 += tr.Cells[14].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[14].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL4 += tr.Cells[16].InnerHtml != "" ? decimal.Parse(tr.Cells[16].InnerHtml) : 0;
        //        totalSPBL4 += tr.Cells[18].InnerHtml != "" ? decimal.Parse(tr.Cells[18].InnerHtml) : 0;
        //        totalEfesiensiL4 += tr.Cells[19].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[19].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL5 += tr.Cells[21].InnerHtml != "" ? decimal.Parse(tr.Cells[21].InnerHtml) : 0;
        //        totalSPBL5 += tr.Cells[23].InnerHtml != "" ? decimal.Parse(tr.Cells[23].InnerHtml) : 0;
        //        totalEfesiensiL5 += tr.Cells[24].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[24].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        totalOutL6 += tr.Cells[26].InnerHtml != "" ? decimal.Parse(tr.Cells[26].InnerHtml) : 0;
        //        totalSPBL6 += tr.Cells[28].InnerHtml != "" ? decimal.Parse(tr.Cells[28].InnerHtml) : 0;
        //        totalEfesiensiL6 += tr.Cells[29].InnerHtml != "&nbsp;&nbsp;0.00" ? decimal.Parse(tr.Cells[29].InnerHtml.ToString().Replace("&nbsp;&nbsp;", "")) : 0;

        //        HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //        trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL </b>";

        //        trfooter.Cells[1].InnerHtml = totalOutL1.ToString("N0");
        //        trfooter.Cells[2].InnerHtml = pakaiBBL1.ToString("N0");
        //        trfooter.Cells[3].InnerHtml = totalSPBL1.ToString("N0");
        //        trfooter.Cells[4].InnerHtml = totalEfesiensiL1.ToString("N2");

        //        trfooter.Cells[6].InnerHtml = totalOutL2.ToString("N0");
        //        trfooter.Cells[7].InnerHtml = pakaiBBL2.ToString("N0");
        //        trfooter.Cells[8].InnerHtml = totalSPBL2.ToString("N0");
        //        trfooter.Cells[9].InnerHtml = totalEfesiensiL2.ToString("N2");

        //        trfooter.Cells[11].InnerHtml = totalOutL3.ToString("N0");
        //        trfooter.Cells[12].InnerHtml = pakaiBBL3.ToString("N0");
        //        trfooter.Cells[13].InnerHtml = totalSPBL3.ToString("N0");
        //        trfooter.Cells[14].InnerHtml = totalEfesiensiL3.ToString("N2");

        //        trfooter.Cells[16].InnerHtml = totalOutL4.ToString("N0");
        //        trfooter.Cells[17].InnerHtml = pakaiBBL4.ToString("N0");
        //        trfooter.Cells[18].InnerHtml = totalSPBL4.ToString("N0");
        //        trfooter.Cells[19].InnerHtml = totalEfesiensiL4.ToString("N2");

        //        trfooter.Cells[21].InnerHtml = totalOutL5.ToString("N0");
        //        trfooter.Cells[22].InnerHtml = pakaiBBL5.ToString("N0");
        //        trfooter.Cells[23].InnerHtml = totalSPBL5.ToString("N0");
        //        trfooter.Cells[24].InnerHtml = totalEfesiensiL5.ToString("N2");

        //        trfooter.Cells[26].InnerHtml = totalOutL6.ToString("N0");
        //        trfooter.Cells[27].InnerHtml = pakaiBBL6.ToString("N0");
        //        trfooter.Cells[28].InnerHtml = totalSPBL6.ToString("N0");
        //        trfooter.Cells[29].InnerHtml = totalEfesiensiL6.ToString("N2");
        //    }

        //    string TglAlias = Session["Tgl"].ToString();
        //    DomainBMReportMMS DS = new DomainBMReportMMS();
        //    FacadeBMReportMMS FS = new FacadeBMReportMMS();
        //    //DS = FS.RetrieveSign(user.UnitKerjaID);

        //    //LabelTanggalK.Text = "Karawang, " + " " + TglAlias;
        //    //LabelAdminK.Text = "(______" + DS.AdminSign.Trim() + "______)";
        //    //LabelManagerK.Text = "(______" + DS.MgrSign.Trim() + "______)";
        //    //LabelPMK.Text = "(______" + DS.PMSign.Trim() + "______)";

        //    //Label lblImage = item.FindControl("lblImage") as Label;
        //}

        //protected void QtyPakaiLK1_Change(object sender, EventArgs e)
        //{
        //    decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;
        //    for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //    {
        //        HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //        DomainBMReportMMS pj = new DomainBMReportMMS();
        //        TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //        if (jml.Text != string.Empty)
        //        {
        //            string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //            decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //            tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //        }

        //        totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //        totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //        HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //        trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //        trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //    }
        //}

        //protected void QtyPakaiL1_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL1 = 0; decimal totalPPML1 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            DomainBMReportMMS pj = new DomainBMReportMMS();
        //            TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL1");
        //            if (jml.Text != string.Empty)
        //            {
        //                string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //                decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //                tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //            }

        //            totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //            totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //            trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            DomainBMReportMMS pj = new DomainBMReportMMS();
        //            TextBox jml = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL1");
        //            if (jml.Text != string.Empty)
        //            {
        //                string PakaiL1 = tr.Cells[3].InnerHtml != "" ? tr.Cells[3].InnerHtml : "0";
        //                decimal DPakaiL1 = Convert.ToDecimal(PakaiL1);
        //                tr.Cells[5].InnerHtml = ((Convert.ToDecimal(DPakaiL1) / decimal.Parse(jml.Text)) * 1000000).ToString("N0");
        //            }

        //            totalPakaiBBL1 += jml.Text != "" ? decimal.Parse(jml.Text) : 0;
        //            totalPPML1 += tr.Cells[5].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[5].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[2].InnerHtml = totalPakaiBBL1.ToString("N0");
        //            trfooter.Cells[5].InnerHtml = totalPPML1.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL2_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL2 = 0; decimal totalPPML2 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL2 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL2");
        //            if (jmlL2.Text != string.Empty)
        //            {
        //                string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
        //                decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
        //                tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
        //            totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[7].InnerHtml = totalPakaiBBL2.ToString("N0");
        //            trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL2 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL2");
        //            if (jmlL2.Text != string.Empty)
        //            {
        //                string PakaiL2 = tr.Cells[8].InnerHtml != "" ? tr.Cells[8].InnerHtml : "0";
        //                decimal DPakaiL2 = Convert.ToDecimal(PakaiL2);
        //                tr.Cells[10].InnerHtml = ((Convert.ToDecimal(DPakaiL2) / decimal.Parse(jmlL2.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL2 += jmlL2.Text != "" ? decimal.Parse(jmlL2.Text) : 0;
        //            totalPPML2 += tr.Cells[10].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[10].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[7].InnerHtml = totalPakaiBBL2.ToString("N0");
        //            trfooter.Cells[10].InnerHtml = totalPPML2.ToString("N0");
        //        }
        //    }

        //}
        //protected void QtyPakaiL3_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL3 = 0; decimal totalPPML3 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL3 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL3");
        //            if (jmlL3.Text != string.Empty)
        //            {
        //                string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
        //                decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
        //                tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
        //            totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N0");
        //            trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL3 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL3");
        //            if (jmlL3.Text != string.Empty)
        //            {
        //                string PakaiL3 = tr.Cells[13].InnerHtml != "" ? tr.Cells[13].InnerHtml : "0";
        //                decimal DPakaiL3 = Convert.ToDecimal(PakaiL3);
        //                tr.Cells[15].InnerHtml = ((Convert.ToDecimal(DPakaiL3) / decimal.Parse(jmlL3.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL3 += jmlL3.Text != "" ? decimal.Parse(jmlL3.Text) : 0;
        //            totalPPML3 += tr.Cells[15].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[15].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[12].InnerHtml = totalPakaiBBL3.ToString("N0");
        //            trfooter.Cells[15].InnerHtml = totalPPML3.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL4_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL4 = 0; decimal totalPPML4 = 0;

        //    if (user.UnitKerjaID == 1)
        //    {
        //        for (int i = 0; i < lstMatrix.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst2");
        //            TextBox jmlL4 = (TextBox)lstMatrix.Items[i].FindControl("QtyPakaiL4");
        //            if (jmlL4.Text != string.Empty)
        //            {
        //                string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
        //                decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
        //                tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
        //            totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
        //            trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N0");
        //            trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
        //        }
        //    }
        //    else if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL4 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL4");
        //            if (jmlL4.Text != string.Empty)
        //            {
        //                string PakaiL4 = tr.Cells[18].InnerHtml != "" ? tr.Cells[18].InnerHtml : "0";
        //                decimal DPakaiL4 = Convert.ToDecimal(PakaiL4);
        //                tr.Cells[20].InnerHtml = ((Convert.ToDecimal(DPakaiL4) / decimal.Parse(jmlL4.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL4 += jmlL4.Text != "" ? decimal.Parse(jmlL4.Text) : 0;
        //            totalPPML4 += tr.Cells[20].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[20].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[17].InnerHtml = totalPakaiBBL4.ToString("N0");
        //            trfooter.Cells[20].InnerHtml = totalPPML4.ToString("N0");
        //        }
        //    }
        //}
        //protected void QtyPakaiL5_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL5 = 0; decimal totalPPML5 = 0;

        //    if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL5 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL5");
        //            if (jmlL5.Text != string.Empty)
        //            {
        //                string PakaiL5 = tr.Cells[23].InnerHtml != "" ? tr.Cells[23].InnerHtml : "0";
        //                decimal DPakaiL5 = Convert.ToDecimal(PakaiL5);
        //                tr.Cells[25].InnerHtml = ((Convert.ToDecimal(DPakaiL5) / decimal.Parse(jmlL5.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL5 += jmlL5.Text != "" ? decimal.Parse(jmlL5.Text) : 0;
        //            totalPPML5 += tr.Cells[25].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[25].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[22].InnerHtml = totalPakaiBBL5.ToString("N0");
        //            trfooter.Cells[25].InnerHtml = totalPPML5.ToString("N0");
        //        }
        //    }        
        //}
        //protected void QtyPakaiL6_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"];
        //    decimal totalPakaiBBL6 = 0; decimal totalPPML6 = 0;

        //    if (user.UnitKerjaID == 7)
        //    {
        //        for (int i = 0; i < lstMatrixK2.Items.Count; i++)
        //        {
        //            HtmlTableRow tr = (HtmlTableRow)lstMatrixK2.Items[i].FindControl("lstK2");
        //            TextBox jmlL6 = (TextBox)lstMatrixK2.Items[i].FindControl("QtyPakaiL6");
        //            if (jmlL6.Text != string.Empty)
        //            {
        //                string PakaiL6 = tr.Cells[28].InnerHtml != "" ? tr.Cells[28].InnerHtml : "0";
        //                decimal DPakaiL6 = Convert.ToDecimal(PakaiL6);
        //                tr.Cells[30].InnerHtml = ((Convert.ToDecimal(DPakaiL6) / decimal.Parse(jmlL6.Text)) * 1000000).ToString("N0");
        //            }
        //            totalPakaiBBL6 += jmlL6.Text != "" ? decimal.Parse(jmlL6.Text) : 0;
        //            totalPPML6 += tr.Cells[30].InnerHtml != "&nbsp;" ? decimal.Parse(tr.Cells[30].InnerHtml.ToString().Replace("&nbsp;", "")) : 0;

        //            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstK2F");
        //            trfooter.Cells[27].InnerHtml = totalPakaiBBL6.ToString("N0");
        //            trfooter.Cells[30].InnerHtml = totalPPML6.ToString("N0");
        //        }
        //    }
        //}

        protected void Keterangan_Change(object sender, EventArgs e)
        { }
        protected void lstMatrix_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void lstMatrix2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void lstMatrix3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void lstMatrixK2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}

public class FacadeFinUtilisasi
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private DomainFinUtilisasi objBM = new DomainFinUtilisasi();

    public FacadeFinUtilisasi()
        : base()
    {

    }
    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }

    public int SimpanUtilisasi_I(object objDomain)
    {
        try
        {
            objBM = (DomainFinUtilisasi)objDomain;
            sqlListParam = new List<SqlParameter>();

            sqlListParam.Add(new SqlParameter("@Bulan", objBM.Bulan));
            sqlListParam.Add(new SqlParameter("@Tahun", objBM.Tahun));
            sqlListParam.Add(new SqlParameter("@Partno", objBM.Partno));
            sqlListParam.Add(new SqlParameter("@Keterangan", objBM.Keterangan));
            sqlListParam.Add(new SqlParameter("@QtyLembar", objBM.QtyLembar));
            sqlListParam.Add(new SqlParameter("@QtyM3", objBM.QtyM3));
            sqlListParam.Add(new SqlParameter("@QtyAktualM3", objBM.QtyAktualM3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objBM.CreatedBy));

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "Fin_InsertUtilisasi1");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public int SimpanUtilisasi_II(object objDomain)
    {
        try
        {
            objBM = (DomainFinUtilisasi)objDomain;
            sqlListParam = new List<SqlParameter>();

            sqlListParam.Add(new SqlParameter("@Bulan", objBM.Bulan));
            sqlListParam.Add(new SqlParameter("@Tahun", objBM.Tahun));
            sqlListParam.Add(new SqlParameter("@Partno", objBM.Partno));
            sqlListParam.Add(new SqlParameter("@QtyM3", objBM.QtyM3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objBM.CreatedBy));

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "Fin_InsertUtilisasi2");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }
    public int SimpanUtilisasi_III(object objDomain)
    {
        try
        {
            objBM = (DomainFinUtilisasi)objDomain;
            sqlListParam = new List<SqlParameter>();

            sqlListParam.Add(new SqlParameter("@Bulan", objBM.Bulan));
            sqlListParam.Add(new SqlParameter("@Tahun", objBM.Tahun));
            sqlListParam.Add(new SqlParameter("@Partno", objBM.Partno));
            sqlListParam.Add(new SqlParameter("@QtyM3", objBM.QtyM3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objBM.CreatedBy));

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "Fin_InsertUtilisasi3");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }
    public int SimpanUtilisasiLP(object objDomain)
    {
        try
        {
            objBM = (DomainFinUtilisasi)objDomain;
            sqlListParam = new List<SqlParameter>();

            sqlListParam.Add(new SqlParameter("@Bulan", objBM.Bulan));
            sqlListParam.Add(new SqlParameter("@Tahun", objBM.Tahun));
            //sqlListParam.Add(new SqlParameter("@Partno", objBM.Partno));
            sqlListParam.Add(new SqlParameter("@QtyBPM3", objBM.QtyBPM3));
            sqlListParam.Add(new SqlParameter("@QtyOKM3", objBM.QtyOKM3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objBM.CreatedBy));


            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "Fin_InsertUtilisasiLP");
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
            objBM = (DomainFinUtilisasi)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Periode", objBM.Periode));
            //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBM.LastModifiedBy));           

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "BMAntiFoam_Cancel");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    private string QueryBJdariBP(string Periode)
    {
        #region QueryLama
        string result0 =
        " with data1 as (select * from vw_KartuStockBJNew where LEFT(convert(char,tanggal,112),6)='" + Periode + "'), " +

        " DataMasukDariBP as (select ItemID,PartNo PartNoOK,qty QtyBP,Keterangan PartnoAwal from data1 " +
        "                     where Process='simetris' and qty>0 and Keterangan like'%-P-%' and (PartNo like'%-3-%' or " +
        "                     PartNo like'%-M-%' or PartNo like'%-W-%') and ItemID in (select ID from FC_Items where Tebal in ('3','3.5','4','4.5') ) ), " +

        " DataMasukDariBP2 as (select ItemID,PartNoOK,QtyBP,(B.Volume*QtyBP)M3,case when PartnoAwal like'%SR%' then (A.QtyBP*B.Volume) else '0' end M31 " +
        "                      from DataMasukDariBP A INNER JOIN FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +

        " DataFinal as (select ItemID,PartNoOK Partno,sum(QtyBP) QtyLembar,sum(M3)M3,sum(M31)M3Other from DataMasukDariBP2 group by ItemID,PartNoOK) " +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,Partno,QtyLembar,cast (M3 as decimal(10,2))M3,cast ((M3-M3Other) as decimal(10,2))M3Other " +
        " from DataFinal  order by Partno  ";
        #endregion
        #region QueryBaru
        string result =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData1]') AND type in (N'U')) DROP TABLE [dbo].[tempData1] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData2]') AND type in (N'U')) DROP TABLE [dbo].[tempData2] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData3]') AND type in (N'U')) DROP TABLE [dbo].[tempData3] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataFinal]') AND type in (N'U')) DROP TABLE [dbo].[tempDataFinal] " +

        " declare @thnbln  varchar(6) " +
        " set @thnbln='" + Periode + "' " +

        " select  ItemID,PartNo,qty,Keterangan PartnoAwal into tempData1 from vw_KartuStockBJNew where Process='simetris' and qty>0 " +
        " and Keterangan like'%-P-%' and (PartNo like'%-3-%' or PartNo like'%-M-%' or PartNo like'%-W-%') and ItemID in (select ID from FC_Items " +
        " where Tebal in ('3','3.5','4','4.5') ) and  LEFT(convert(char,tanggal,112),6)='" + Periode + "' " +

        " select A.ItemID,A.PartNo,B.Tebal,B.Lebar,B.Panjang,sum(A.qty)qtylembar,case when A.PartnoAwal like'%SR%' then sum(A.Qty) else '0' " +
        " end qtylembar_sr  into tempData2 from tempData1 A INNER JOIN FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1  " +
        " group by A.ItemID,A.partno,A.partnoawal,B.tebal,B.lebar,B.panjang " +

        " select ItemID,PartNo,SUM(qtylembar)qtylembar,sum(qtylembar_sr)qtylembar_sr,tebal,lebar,panjang into tempData3 from tempData2 " +
        " group by ItemID,PartNo,tebal,lebar,panjang " +

        " select partno,qtylembar,cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as decimal(10,2)) m3,cast((((tebal*lebar*panjang)/1000000000)*" +
        " qtylembar_sr) as decimal(10,2)) m3_sr,cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as decimal(10,3)) m3_ttl into tempDataFinal  " +
        " from tempData3 " +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,Partno,QtyLembar,M3,(m3-m3_sr)M3Other from tempDataFinal order by partno ";

        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData1]') AND type in (N'U')) DROP TABLE [dbo].[tempData1] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData2]') AND type in (N'U')) DROP TABLE [dbo].[tempData2] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData3]') AND type in (N'U')) DROP TABLE [dbo].[tempData3] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataFinal]') AND type in (N'U')) DROP TABLE [dbo].[tempDataFinal] ";

        #endregion
        return result;
    }
    private string QueryBPdariT1(string Periode)
    {
        #region QueryLama
        string result0 =
        " with data1 as (select * from vw_KartuStockBJNew where LEFT(convert(char,tanggal,112),6)='" + Periode + "'), " +

        " DataMasukDariWIPT1 as (select ItemID,PartNo PartNoBP,qty QtyBP,Keterangan PartnoAwal from data1 " +
        "                        where ((process='direct') or (process  like '%simetris%' and substring(Keterangan,1,21) like '%-1-%')) " +
        "                        and ItemID in (select ID from FC_Items where Tebal in ('3','3.5','4','4.5') ) ), " +

        " DataMasukDariWIPT12 as (select ItemID,PartNoBP,QtyBP,(B.Volume*QtyBP)M3 " +
        "                         from DataMasukDariWIPT1 A INNER JOIN FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +

        " DataFinal as (select ItemID,PartNoBP Partno,sum(QtyBP) QtyLembar,sum(M3)M3  from DataMasukDariWIPT12 where PartNoBP like'%-P-%' and " +
        "               PartNoBP like'%SE%' group by ItemID,PartNoBP) " +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,Partno,round(cast (M3 as decimal(10,2)),1)M3 " +
        " from DataFinal  order by Partno  ";
        #endregion
        #region QueryBaru
        string result =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataA2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataA2] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataB2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataB2] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataAkhir]') AND type in (N'U')) DROP TABLE [dbo].[tempDataAkhir] " +

        " declare @thnbln  varchar(6) " +
        " set @thnbln='" + Periode + "' " +

        " select ItemID,PartNo,qty into tempDataA2 from vw_KartuStockBJNew where ((process='direct') or " +
        " (process  like '%simetris%' and substring(Keterangan,1,21) like '%-1-%'))  and ItemID in (select ID from FC_Items where Tebal in " +
        " ('3','3.5','4','4.5')) and LEFT(convert(char,tanggal,112),6)='" + Periode + "' " +

        " select A.Partno,B.Tebal,B.Lebar,B.Panjang,sum(A.qty) qtylembar into tempDataB2 from  tempDataA2 A inner join FC_Items B ON A.ItemID=B.ID " +
        " where A.PartNo like'%-P-%' and A.PartNo like'%SE%' group by A.partno,B.Tebal,B.Lebar,B.Panjang " +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,PartNo,cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as Decimal(10,2))M3, " +
        " cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as Decimal(10,3))M3_ttl into tempDataAkhir from tempDataB2 " +

        "  select No,PartNo,M3 from tempDataAkhir order by no ";

        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataA2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataA2] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataB2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataB2] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataAkhir]') AND type in (N'U')) DROP TABLE [dbo].[tempDataAkhir] ";


        #endregion
        return result;
    }
    private string QueryBPdariBJ(string Periode)
    {
        #region QueryLama
        string result0 =
        " with data1 as (select * from vw_KartuStockBJNew where LEFT(convert(char,tanggal,112),6)='" + Periode + "'), " +

        " DataMasukDariBJ as (select ItemID,PartNo PartNoBP,qty QtyBP,Keterangan PartnoAwal from data1 " +
        "                        where process  like '%simetris%' and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') and qty>0 " +
        "                        and ItemID in (select ID from FC_Items where Tebal in ('3','3.5','4','4.5') ) ), " +

        " DataMasukDariBJ2 as (select ItemID,PartNoBP,QtyBP,(B.Volume*QtyBP)M3 " +
        "                         from DataMasukDariBJ A INNER JOIN FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +

        " DataFinal as (select ItemID,PartNoBP Partno,sum(QtyBP) QtyLembar,sum(M3)M3  from DataMasukDariBJ2 where PartNoBP like'%-P-%' group by ItemID,PartNoBP)" +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,Partno,round(cast (M3 as decimal(10,2)),1)M3 " +
        " from DataFinal  order by Partno  ";
        #endregion
        #region QueryBaru
        string result =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataU1]') AND type in (N'U')) DROP TABLE [dbo].[tempDataU1] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataU2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataU2] " +
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataAkhir2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataAkhir2]  " +

        " declare @thnbln  varchar(6) " +
        " set @thnbln='" + Periode + "' " +

        " select ItemID,PartNo,qty into tempDataU1 from vw_KartuStockBJNew where process  like '%simetris%' and " +
        " (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') and qty>0 and " +
        " ItemID in (select ID from FC_Items where Tebal in ('3','3.5','4','4.5'))  and LEFT(convert(char,tanggal,112),6)='" + Periode + "' " +

        " select A.Partno,B.Tebal,B.Lebar,B.Panjang,sum(A.qty) qtylembar into tempDataU2 from tempDataU1 A INNER JOIN FC_Items B ON A.ItemID=B.ID " +
        " where B.RowStatus>-1 and A.Partno  like'%-P-%'  group by A.partno,B.Tebal,B.Lebar,B.Panjang " +

        " select ROW_NUMBER() OVER(ORDER BY Partno asc)No,PartNo,cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as Decimal(10,2))M3, " +
        " cast((((tebal*lebar*panjang)/1000000000)*qtylembar) as Decimal(10,3))M3_ttl into tempDataAkhir2 from tempDataU2 " +

        " select No,PartNo,M3 from  tempDataAkhir2 ";

        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataU1]') AND type in (N'U')) DROP TABLE [dbo].[tempDataU1] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataU2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataU2] " +
        //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataAkhir2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataAkhir2]  ";


        #endregion
        return result;
    }
    private string QueryUtiListPlank(string Periode)
    {
        #region QueryLama
        string result0 =
        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempUtiLP]') AND type in (N'U')) DROP TABLE [dbo].[tempUtiLP] " +

        " select Utilisasi,SUM(QtyM3_LP)QtyM3_LP,SUM(QtyM3_BP)QtyM3_BP,SUM(QtyM32)QtyM32,SUM(QtyM3_OK)QtyM3_OK,Process into tempUtiLP " +
        " from (select ''Utilisasi,QtyM3 QtyM3_LP,0'QtyM3_BP',QtyM32,QtyM3_OK,Process " +
        " from (select cast(sum(FC1.Volume*R1.Qty) as Decimal(10,2)) QtyM3,0'QtyM32',0'QtyM3_OK','R1'Process " +
        " from (select ItemID0,sum(Qty)Qty from vw_KartuStockListplankR1 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0 " +
        " and SUBSTRING(PartnoAsal,5,1)='P' group by ItemID0 ) R1 INNER JOIN FC_Items FC1 ON FC1.ID=R1.ItemID0 " +

        " union all " +

        " select cast(sum(FC2.Volume*R2.Qty) as Decimal(10,2)) QtyM3,0'QtyM32',0'QtyM3_OK','R2'Process  " +
        " from (select ItemID0,sum(Qty)Qty from vw_KartuStockListplankR2 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0 " +
        " and SUBSTRING(PartnoAsal,5,1)='P' group by ItemID0 ) R2 INNER JOIN FC_Items FC2 ON FC2.ID=R2.ItemID0 " +

        " union all " +

        " select cast(sum(FC3.Volume*R3.Qty) as Decimal(10,2)) QtyM3,0'QtyM32',0'QtyM3_OK','R3'Process  from (select ItemID0,sum(Qty)Qty " +
        " from vw_KartuStockListplankR3 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0 and SUBSTRING(PartnoAsal,5,1)='P' " +
        " group by ItemID0 ) R3 INNER JOIN FC_Items FC3 ON FC3.ID=R3.ItemID0  " +
        " union all " +

        //Lama
        //" select 0'QtyM3',cast(SUM(FC1.Volume*R1.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R1'Process from (select ItemID,qty from vw_KartuStockBJNew " +
        //" where LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Process='direct' and SUBSTRING(IDrec,9,7) in (select ID from T3_Rekap " +
        //" where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R1%' and ItemID in (select ID from FC_Items " +
        //" where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1) ) as R1 INNER JOIN FC_Items FC1 ON R1.ItemID=FC1.ID " +
        //End Lama

        //Baru
        " select 0'QtyM3',cast(SUM(FC1.Volume*R1.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R1'Process from (select QtyIn Qty,ItemID from T3_Rekap " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R1%' and ItemID in (select ID from FC_Items  " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1)  as R1 INNER JOIN FC_Items FC1 ON R1.ItemID=FC1.ID  " +
        //End Baru

        " union all " +

        //Lama
        //" select 0'QtyM3',cast(SUM(FC2.Volume*R2.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R2'Process from (select ItemID,qty " +
        //" from vw_KartuStockBJNew where LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Process='direct' and SUBSTRING(IDrec,9,7) in " +
        //" (select ID from T3_Rekap where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R2%' and " +
        //" ItemID in (select ID from FC_Items where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1) ) as R2 INNER JOIN FC_Items FC2 ON R2.ItemID=FC2.ID " +
        //End Lama

        //Baru
        " select 0'QtyM3',cast(SUM(FC2.Volume*R2.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R2'Process from (select QtyIn Qty,ItemID from T3_Rekap " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R2%' and ItemID in (select ID from FC_Items  " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1)  as R2 INNER JOIN FC_Items FC2 ON R2.ItemID=FC2.ID  " +
        //End Baru

        " union all " +

        //Lama
        //" select 0'QtyM3',cast(SUM(FC3.Volume*R3.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R3'Process from (select ItemID,qty from vw_KartuStockBJNew " +
        //" where LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Process='direct' and SUBSTRING(IDrec,9,7) in (select ID from T3_Rekap " +
        //" where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R3%' and ItemID in (select ID from FC_Items " +
        //" where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1) ) as R3 INNER JOIN FC_Items FC3 ON R3.ItemID=FC3.ID " +
        //End Lama

        //Baru
        " select 0'QtyM3',cast(SUM(FC3.Volume*R3.qty) as Decimal(10,2)) QtyM32,0'QtyM3_OK','R3'Process from (select QtyIn Qty,ItemID from T3_Rekap " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R3%' and ItemID in (select ID from FC_Items  " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1)  as R3 INNER JOIN FC_Items FC3 ON R3.ItemID=FC3.ID  " +
        //End Baru

        " ) as Data ) as DataFinal group by Utilisasi,Process " +

        " select * from tempUtiLP  " +
        " union all  " +
        " select 'Jumlah Produk'Utilisasi,SUM(QtyM3_LP)TQtyM3_LP,SUM(QtyM3_BP)TQtyM3_BP,SUM(QtyM32)TQtyM32,SUM(QtyM3_OK)TQtyM3_OK,''Process from tempUtiLP " +
        " union all  " +
        " select 'Jumlah Total Produk'Utilisasi,'0'TQtyM3_LP,(SUM(QtyM3_BP) + (SUM(QtyM3_LP)))TQtyM3_BP,'0'TQtyM32,(SUM(QtyM3_OK)+(SUM(QtyM32)))TQtyM3_OK,''Process from tempUtiLP " +
        " union all  " +
        " select 'Per Laporan ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,ROUND(((SUM(QtyM32)/SUM(QtyM3_LP))*100),0)TQtyM32,'0'TQtyM3_OK,''Process from tempUtiLP " +
        " union all  " +
        " select 'Utilisasi ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,'0'TQtyM32,ROUND(((SUM(QtyM3_OK)+SUM(QtyM32)) / (SUM(QtyM3_BP) + SUM(QtyM3_LP))*100),0)TQtyM3_OK,''Process from tempUtiLP ";

        #endregion
        #region Query Baru
        string result =
        "  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempUtiLP]') AND type in (N'U')) DROP TABLE [dbo].[tempUtiLP] " +

        " select Utilisasi,SUM(QtyM3_LP)QtyM3_LP,SUM(QtyM3_BP)QtyM3_BP,SUM(QtyM32)QtyM32,SUM(QtyM3_OK)QtyM3_OK,Process into tempUtiLP  " +
        " from (select ''Utilisasi,QtyM3 QtyM3_LP,0'QtyM3_BP',QtyM32,QtyM3_OK,Process  from ( " +

        " select isnull(cast(SUM((((Tebal*Lebar*Panjang)/1000000000)*Qty1)) as decimal(10,2)),0)QtyM3,0.00'QtyM32',0.00'QtyM3_OK','R1'Process " +
        " from (select SUM(Qty1)Qty1,Tebal,Lebar,panjang from (select ItemID0,isnull(sum(Qty),0)Qty1 from (select ItemID0,isnull(sum(Qty),0)Qty " +
        " from vw_KartuStockListplankR1 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0  and " +
        " SUBSTRING(PartnoAsal,5,1)='P' group by ItemID0 )  as xx2 group by ItemID0 ) as xx1 inner join FC_Items fc2 on xx1.ItemID0=fc2.ID " +
        " group by Tebal,Lebar,panjang ) as xx2 " +

        " union all " +

        " select isnull(cast(SUM((((Tebal*Lebar*Panjang)/1000000000)*Qty1)) as decimal(10,2)),0)QtyM3,0.00'QtyM32',0.00'QtyM3_OK','R2'Process " +
        " from (select SUM(Qty1)Qty1,Tebal,Lebar,panjang from (select ItemID0,isnull(sum(Qty),0)Qty1 from (select ItemID0,isnull(sum(Qty),0)Qty " +
        " from vw_KartuStockListplankR2 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0  and " +
        " SUBSTRING(PartnoAsal,5,1)='P' group by ItemID0 )  as xx2 group by ItemID0 ) as xx1 inner join FC_Items fc2 on xx1.ItemID0=fc2.ID " +
        " group by Tebal,Lebar,panjang ) as xx2 " +

        " union all " +

        " select isnull(cast(SUM((((Tebal*Lebar*Panjang)/1000000000)*Qty1)) as decimal(10,2)),0)QtyM3,0.00'QtyM32',0.00'QtyM3_OK','R3'Process " +
        " from (select SUM(Qty1)Qty1,Tebal,Lebar,panjang from (select ItemID0,isnull(sum(Qty),0)Qty1 from (select ItemID0,isnull(sum(Qty),0)Qty " +
        " from vw_KartuStockListplankR3 where process='i99' and LEFT(convert(char,tanggal,112),6)='" + Periode + "' and Qty>0  and " +
        " SUBSTRING(PartnoAsal,5,1)='P' group by ItemID0 )  as xx2 group by ItemID0 ) as xx1 inner join FC_Items fc2 on xx1.ItemID0=fc2.ID " +
        " group by Tebal,Lebar,panjang ) as xx2 " +

        " union all " +

        " select QtyM3,cast(ROUND(QtyM32,2) as decimal(10,2))QtyM32,QtyM3_OK,Process " +
        " from (select 0.00'QtyM3',isnull(cast(sum((((Tebal*Lebar*Panjang)/1000000000)*xx2.Qty)) as decimal(10,3)),0)QtyM32,0.00'QtyM3_OK','R1'Process " +
        " from (select xx.*,x1.Tebal,x1.Lebar,x1.Panjang from (select isnull(sum(QtyIn),0) Qty,ItemID from T3_Rekap  " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R1%' and ItemID in (select ID from FC_Items   " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1 group by itemid ) as xx inner join FC_Items x1 ON xx.ItemID=x1.ID ) as xx2 ) as xx3 " +

        " union all  " +

        " select QtyM3,cast(ROUND(QtyM32,2) as decimal(10,2))QtyM32,QtyM3_OK,Process " +
        " from (select 0.00'QtyM3',isnull(cast(sum((((Tebal*Lebar*Panjang)/1000000000)*xx2.Qty)) as decimal(10,2)),0)QtyM32,0.00'QtyM3_OK','R2'Process " +
        " from (select xx.*,x1.Tebal,x1.Lebar,x1.Panjang from (select isnull(sum(QtyIn),0) Qty,ItemID from T3_Rekap  " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R2%' and ItemID in (select ID from FC_Items  " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1 group by itemid ) as xx inner join FC_Items x1 ON xx.ItemID=x1.ID ) as xx2 ) as xx3 " +

        " union all  " +

        " select QtyM3,cast(ROUND(QtyM32,2) as decimal(10,2))QtyM32,QtyM3_OK,Process " +
        " from (select 0.00'QtyM3',isnull(cast(sum((((Tebal*Lebar*Panjang)/1000000000)*xx2.Qty)) as decimal(10,2)),0)QtyM32,0.00'QtyM3_OK','R3'Process " +
        " from (select xx.*,x1.Tebal,x1.Lebar,x1.Panjang from (select isnull(sum(QtyIn),0) Qty,ItemID from T3_Rekap " +
        " where LEFT(convert(char,TglTrans,112),6)='" + Periode + "' and T1SerahID>0 and Sfrom like'%R3%' and ItemID in (select ID from FC_Items " +
        " where SUBSTRING(PartNo,5,1)='3') and RowStatus>-1 group by itemid ) as xx inner join FC_Items x1 ON xx.ItemID=x1.ID ) as xx2 ) as xx3 " +

        "  ) as Data ) as DataFinal group by Utilisasi,Process  select * from tempUtiLP  " +

        " union all  " +

        " select 'Jumlah Produk'Utilisasi,SUM(QtyM3_LP)TQtyM3_LP,SUM(QtyM3_BP)TQtyM3_BP,SUM(QtyM32)TQtyM32,SUM(QtyM3_OK)TQtyM3_OK,''Process from tempUtiLP  " +

        " union all  " +

        " select 'Jumlah Total Produk'Utilisasi,'0'TQtyM3_LP,(SUM(QtyM3_BP) + (SUM(QtyM3_LP)))TQtyM3_BP,'0'TQtyM32,(SUM(QtyM3_OK)+(SUM(QtyM32)))TQtyM3_OK,''Process from tempUtiLP " +

        //revisi add nullif
        " union all " +
         "select 'Per Laporan ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,ROUND((isnull((nullif(SUM(QtyM32),0)/nullif(SUM(QtyM3_LP),0)),0)*100),0)TQtyM32,'0'TQtyM3_OK,''Process  from tempUtiLP  " +
        " union all  " +
        "select 'Utilisasi ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,'0'TQtyM32,ROUND(isnull(nullif(SUM(QtyM3_OK)+SUM(QtyM32),0) / (nullif(SUM(QtyM3_BP) + SUM(QtyM3_LP),0))*100,0),0)TQtyM3_OK,''Process from tempUtiLP  ";

        //revisi divide by zero error
        //" select 'Per Laporan ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,ROUND(((SUM(QtyM32)/SUM(QtyM3_LP))*100),0)TQtyM32,'0'TQtyM3_OK,''Process  from tempUtiLP " +
        //" union all " +
        //" select 'Utilisasi ( % )'Utilisasi,'0'TQtyM3_LP,'0'TQtyM3_BP,'0'TQtyM32,ROUND(((SUM(QtyM3_OK)+SUM(QtyM32)) / (SUM(QtyM3_BP) + SUM(QtyM3_LP))*100),0)TQtyM3_OK,''Process from tempUtiLP  ";
        //"  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempUtiLP]') AND type in (N'U')) DROP TABLE [dbo].[tempUtiLP] ";
        #endregion
        return result;
    }
    private string Data1(int Bulan, int Tahun)
    {
        string result =
        " select ROW_NUMBER() over (order by ID asc ) as No,PartNo,QtyLembar,QtyM3 M3,QtyAktualM3 M3Other,Keterangan " +
        " from Fin_UtilitasBJdariBP where Bulan=" + Bulan + " and Tahun=" + Tahun + " and RowStatus>-1 order by ID ";
        return result;
    }
    private string Data2(int Bulan, int Tahun)
    {
        string result =
        " select ROW_NUMBER() over (order by ID asc ) as No,PartNo,QtyM3 M3 from Fin_UtilitasBPdariT1 " +
        " where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " ";
        return result;
    }
    private string Data3(int Bulan, int Tahun)
    {
        string result =
        " select ROW_NUMBER() over (order by ID asc ) as No,PartNo,QtyM3 M3 from Fin_UtilitasBPdariBJ " +
        " where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " ";
        return result;
    }

    public string RetrieveJudul()
    {
        string strSQL;
        strSQL = " select 'Laporan Utilisasi'Judul  ";
        return strSQL;
    }

    public string RetrieveLapUti(int Bulan, int Tahun)
    {
        string strSQL;
        strSQL =
// " select ROW_NUMBER() over (order by ID asc ) as No,* from ( " +
// " select ID,PartNo PartNo1,QtyLembar,QtyM3 M3,QtyAktualM3, from Fin_UtilitasBJdariBP where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " " +
//" ) as data1 ";

//         " select ROW_NUMBER() over (order by keterangan,ID asc ) as No,* from (   " +
//"  select ID,PartNo ,QtyLembar,QtyM3 M3,QtyAktualM3,'PartNo1'Keterangan from Fin_UtilitasBJdariBP where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  " +
// " union all  " +
// " select ID,PartNo,0'QtyLembar',QtyM3 M3_2,0'QtyAktualM3','PartNo2'Keterangan from Fin_UtilitasBPdariBJ where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " " +
// " union all  " +
// " select ID,PartNo,0'QtyLembar',QtyM3 M3_3,0'QtyAktualM3','PartNo3'Keterangan from Fin_UtilitasBPdariT1 where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  ) as data1 ";

" select ROW_NUMBER() over (order by keterangan,ID asc ) as No,* from (   " +
"  select ID,PartNo ,QtyLembar,QtyM3 M3,QtyAktualM3,Keterangan from Fin_UtilitasBJdariBP where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  " +
"  ) as data1 ";
        return strSQL;
    }
    public string RetrieveLapUti2(int Bulan, int Tahun)
    {
        string strSQL2;
        strSQL2 =
        // " select ROW_NUMBER() over (order by ID asc ) as No,* from ( " +
        // " select ID,PartNo PartNo2,QtyM3 M3 from Fin_UtilitasBPdariBJ where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " " +
        //" ) as data1 ";

        " select ROW_NUMBER() over (order by ID asc ) as No,* from (   " +
"  select ID,PartNo PartNo2 ,QtyM3 M3 from Fin_UtilitasBPdariBJ where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  " +
"  ) as data1 ";

        //" select No,ID1,PartNo1,QtyLembar,M3,QtyAktualM3,No,ID2,partno2,M3_2,No,ID3,PartNo3,M3_3 from " +
        //" (select ROW_NUMBER() over (order by ID asc ) as No,ID ID1,PartNo PartNo1,QtyLembar,QtyM3 M3,QtyAktualM3,0'ID2',''PartNo2,0'M3_2',0'ID3',''PartNo3,0'M3_3' " +
        //" from Fin_UtilitasBJdariBP where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  " +
        //" union all " +
        //" select ROW_NUMBER() over (order by ID asc ) as No,0'ID1',''PartNo1,0'QtyLembar',0'M3',0'QtyAktualM3',ID ID2,PartNo PartNo2,QtyM3'M3_2',0'ID3',''PartNo3,0 " +
        //" 'M3_3' from Fin_UtilitasBPdariBJ where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + " " +
        //" union all " +
        //" select ROW_NUMBER() over (order by ID asc ) as No,0'ID1',''PartNo1,0'QtyLembar',0'M3',0'QtyAktualM3',0'ID2',''PartNo2,0'M3_2',ID ID3,PartNo PartNo3,QtyM3 " +
        //" 'M3_3' from Fin_UtilitasBPdariT1 where RowStatus>-1 and Bulan=" + Bulan + " and Tahun=" + Tahun + "  " +
        //" ) as xx group by ID1,PartNo1,ID2,partno2,QtyLembar,M3,QtyAktualM3,M3_2,No,ID3,PartNo3,M3_3,No ";
        return strSQL2;
    }

    public int RetrieveInputan(int Bulan, int Tahun)
    {
        string StrSql =
        " select sum(Total)Total from (select COUNT(ID)Total from Fin_UtilitasBJdariBP where Bulan=" + Bulan + " and Tahun=" + Tahun + " and RowStatus>-1 " +
        " union all " +
        " select 0'Total' ) as xx ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToInt32(sqlDataReader["Total"]);
            }
        }

        return 0;
    }

    public ArrayList RetrieveBJdariBP(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.QueryBJdariBP(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.QueryBJdariBP(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveBPdariT1(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.QueryBPdariT1(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.QueryBPdariT1(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject2(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveBPdariBJ(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.QueryBPdariBJ(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.QueryBPdariBJ(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject2(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveListLP(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.QueryUtiListPlank(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.QueryUtiListPlank(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectLP(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveData1(int Bulan, int Tahun)
    {
        arrData = new ArrayList();
        string strsql = this.Data1(Bulan, Tahun);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Data1(Bulan, Tahun));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectData1(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveData2(int Bulan, int Tahun)
    {
        arrData = new ArrayList();
        string strsql = this.Data2(Bulan, Tahun);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Data2(Bulan, Tahun));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectData23(sdr));
            }
        }
        return arrData;
    }
    public ArrayList RetrieveData3(int Bulan, int Tahun)
    {
        arrData = new ArrayList();
        string strsql = this.Data3(Bulan, Tahun);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Data3(Bulan, Tahun));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectData23(sdr));
            }
        }
        return arrData;
    }

    public Decimal RetrieveTotalM3()
    {
        string StrSql =
          " select isnull(cast(SUM(m3_ttl) as decimal(10,3)),0)m3 from tempDataFinal";
        //" select  cast(round(SUM(m3Other),2,1) as NUMERIC(18,2))M3 from tempDataFinal";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["m3"]);
            }
        }
        return 0;
    }
    public Decimal RetrieveTotalM3_2()
    {
        string StrSql =
        " select isnull(sum(m3_ttl),0)m3 from tempDataAkhir";
        //" select  cast(round(SUM(m3),2,1) as NUMERIC(18,2))M3 from tempDataFinalD ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["m3"]);
            }
        }
        return 0;
    }
    public Decimal RetrieveTotalM3_3()
    {
        string StrSql =
        " select isnull(sum(m3_ttl),0)m3 from tempDataAkhir2";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["m3"]);
            }
        }
        return 0;
    }
    public Decimal RetrieveTotalM3_Aktual()
    {
        string StrSql =
          " select SUM(m3aktual)m3aktul from (select partno,qtylembar,m3,m3_sr,m3_ttl,(m3-m3_sr)m3aktual from tempDataFinal ) xx ";
        //" select  cast(round(SUM(m3Other),2,1) as NUMERIC(18,2))M3 from tempDataFinal";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["m3aktul"]);
            }
        }
        return 0;
    }
    public Decimal RetrieveTotalLP1()
    {
        string StrSql =
        //" select isnull(cast(sum(QtyM32) as Decimal(10,2)),0)TotalLP1 from tempUtiLP ";
        "  select isnull(cast(sum(QtyM32) as Decimal(10,1)),0) TotalLP1 from tempUtiLP ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalLP1"]);
            }
        }

        return 0;
    }
    public Decimal RetrieveTotalLP2()
    {
        string StrSql =
        " select isnull(cast(sum(QtyM3_LP) as Decimal(10,1)),0)TotalLP2 from tempUtiLP ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalLP2"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalBJBPM3(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyM3)TotalBJBP from Fin_UtilitasBJdariBP where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalBJBP"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalBJBP2M3(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyAktualM3)TotalBJBP2 from Fin_UtilitasBJdariBP where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalBJBP2"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalBPT1(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyM3)TotalBPT1 from Fin_UtilitasBPdariT1 where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalBPT1"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalBPBJ(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyM3)TotalBPBJ from Fin_UtilitasBPdariBJ where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalBPBJ"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalLP1(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyBPM3)TotalLP1 from Fin_UtilisasiListPlank where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalLP1"]);
            }
        }

        return 0;
    }

    public Decimal RetrieveTotalLP2(string bln, string thn)
    {
        string StrSql =
        "  select sum(QtyOKM3)TotalLP2 from Fin_UtilisasiListPlank where bulan='" + bln + "' and tahun='" + thn + "' and rowstatus>-1 ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["TotalLP2"]);
            }
        }

        return 0;
    }

    private DomainFinUtilisasi GenerateObject_RetrieveSign(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();
        cpj.AdminSign = sdr["AdminSign"].ToString();
        cpj.MgrSign = sdr["MgrSign"].ToString();
        cpj.PMSign = sdr["PMSign"].ToString();
        return cpj;
    }
    private DomainFinUtilisasi GenerateObject(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();

        cpj.No = sdr["No"].ToString();
        cpj.Partno = sdr["Partno"].ToString();
        cpj.M3 = Convert.ToDecimal(sdr["M3"]);
        cpj.M3Other = Convert.ToDecimal(sdr["M3Other"]);
        cpj.QtyLembar = Convert.ToDecimal(sdr["QtyLembar"]);



        return cpj;
    }
    private DomainFinUtilisasi GenerateObject2(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();

        cpj.No = sdr["No"].ToString();
        cpj.Partno = sdr["Partno"].ToString();
        cpj.M3 = Convert.ToDecimal(sdr["M3"]);




        return cpj;
    }
    private DomainFinUtilisasi GenerateObjectLP(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();

        cpj.Utilisasi = sdr["Utilisasi"].ToString();
        cpj.QtyM3_BP = Convert.ToDecimal(sdr["QtyM3_BP"]);
        cpj.QtyM3_LP = Convert.ToDecimal(sdr["QtyM3_LP"]);
        cpj.QtyM32 = Convert.ToDecimal(sdr["QtyM32"]);
        cpj.QtyM3_OK = Convert.ToDecimal(sdr["QtyM3_OK"]);

        return cpj;
    }
    private DomainFinUtilisasi GenerateObject_RetrieveTgl(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();

        cpj.ID = Convert.ToInt32(sdr["ID"]);
        cpj.Keterangan = sdr["Keterangan"].ToString();

        return cpj;
    }
    private DomainFinUtilisasi GenerateObjectData1(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();
        cpj.No = sdr["No"].ToString();
        cpj.Partno = sdr["Partno"].ToString();
        cpj.QtyLembar = Convert.ToDecimal(sdr["QtyLembar"]);
        cpj.M3 = Convert.ToDecimal(sdr["M3"]);
        cpj.M3Other = Convert.ToDecimal(sdr["M3Other"]);
        cpj.Keterangan = sdr["Keterangan"].ToString();
        return cpj;
    }
    private DomainFinUtilisasi GenerateObjectData23(SqlDataReader sdr)
    {
        DomainFinUtilisasi cpj = new DomainFinUtilisasi();
        cpj.No = sdr["No"].ToString();
        cpj.Partno = sdr["Partno"].ToString();
        cpj.M3 = Convert.ToDecimal(sdr["M3"]);
        return cpj;
    }
}

public class DomainFinUtilisasi
{
    public int Status { get; set; }
    public int UnitKerjaID { get; set; }
    public int ID { get; set; }
    public string No { get; set; }
    public string Utilisasi { get; set; }

    public DateTime CreatedTime { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public DateTime Tanggal2 { get; set; }

    public string AdminSign { get; set; }
    public string MgrSign { get; set; }
    public string PMSign { get; set; }
    public string LastModifiedBy { get; set; }
    public string CreatedBy { get; set; }
    public string Periode { get; set; }
    public string Tanggal { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string Keterangan { get; set; }
    public string Partno { get; set; }

    public Decimal M3Other { get; set; }
    public Decimal M3 { get; set; }
    public Decimal QtyLembar { get; set; }

    public Decimal QtyM3_LP { get; set; }
    public Decimal QtyM3_BP { get; set; }
    public Decimal QtyM32 { get; set; }
    public Decimal QtyM3_OK { get; set; }

    public Decimal QtyM3 { get; set; }
    public Decimal QtyAktualM3 { get; set; }
    public Decimal QtyBPM3 { get; set; }
    public Decimal QtyOKM3 { get; set; }

}
