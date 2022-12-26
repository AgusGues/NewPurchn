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
    public partial class Monitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count == 0)
                {
                    Response.Redirect("~/ISO_UPD/Monitoring.aspx");
                }
                else
                {
                    Global.link = "../../Default.aspx";
                    Users user = (Users)Session["Users"];

                    string BulanMaster = DateTime.Now.Month.ToString();
                    string TahunMaster = DateTime.Now.Year.ToString();

                    Session["Tahun"] = TahunMaster;
                    Session["Bulan"] = BulanMaster;
                    Session["UnitKerjaID"] = user.UnitKerjaID;
                    Session["NamaBulan"] = BulanMaster;
                    Session["FlagTahun"] = "";
                    Session["Flag"] = "";
                    Session["Filter"] = "";

                    UserID.Value = user.UnitKerjaID.ToString();

                    PanelBulanan.Visible = true; RBPeriodeTahunan.Enabled = false;
                    RBHideDetailDokumenBaru.Enabled = false; RBHideDetailShare.Enabled = false; RBHidePerubahan.Enabled = false; RBHidePemusnahan.Enabled = false;
                    LabelFilter.Visible = true; LabelFilter.Text = "Filter By :";
                    LabelFilter.Attributes["style"] = "color:black; font-weight:bold;";

                    //if (user.UnitKerjaID == 13)
                    //{

                    //    PanelShare.Visible = false;
                    //    PanelShareAntarPlant.Visible = false; PanelShareJombang.Visible = false;
                    //}

                    /** Distribusi Internal Plant **/
                    LoadDistribusi();

                    /** Share External Plant to Internal Plant **/
                    LoadDataShare();

                    /** UPD Baru **/
                    LoadDataDokumenBaru();

                    /** UPD Perubahan **/
                    LoadDataPerubahan();

                    /** UPD Pemusnahan **/
                    LoadDataPemusnahan();

                    LoadHeaderPeriodeBulan();
                    LoadFilter();
                }
            }
        }

        protected void BtnPreview_ServerClick(object sender, EventArgs e)
        {
            if (RBPeriodeBulanan.Checked)
            {
                RBPeriodeBulanan.Checked = true;
            }

            if (ddlBulan.SelectedValue == "0" && ddlTahun.SelectedValue == "0" && ddlFilter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, " Periode Bulan,Tahun & Filter harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue == "0" && ddlFilter.SelectedValue == "0" && ddlBulan.SelectedValue != "0")
            {
                DisplayAJAXMessage(this, " Periode Tahun & Filter harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue != "0" && ddlBulan.SelectedValue == "0" && ddlFilter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, " Periode Bulan & Filter harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue == "0" && ddlBulan.SelectedValue != "0" && ddlFilter.SelectedValue != "0")
            {
                DisplayAJAXMessage(this, " Periode Tahun harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue != "0" && ddlBulan.SelectedValue != "0" && ddlFilter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, " Filter harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue != "0" && ddlBulan.SelectedValue == "0" && ddlFilter.SelectedValue != "0")
            {
                DisplayAJAXMessage(this, " Periode Bulan harus di pilih !! ");
                return;
            }
            else if (ddlTahun.SelectedValue == "0" && ddlBulan.SelectedValue == "0" && ddlFilter.SelectedValue != "0")
            {
                DisplayAJAXMessage(this, " Periode Bulan & Tahun harus di pilih !! ");
                return;
            }

            RBDetailDokumenBaru.Enabled = true;
            RBDetailDokumenBaru.Checked = false;

            RBDetailShare.Enabled = true;
            RBDetailShare.Checked = false;

            RBPerubahan.Enabled = true;
            RBPerubahan.Checked = false;

            RBPemusnahan.Enabled = true;
            RBPemusnahan.Checked = false;

            RBHideDetailShare.Enabled = false;
            RBHidePerubahan.Enabled = false;
            RBDetailDokumenBaru.Enabled = false;

            PanelGridShare.Visible = false;
            PanelGridUPD.Visible = false;
            PanelGridUPDPerubahan.Visible = false;
            PanelGridPemusnahan.Visible = false;

            ddlFilter.Enabled = true;

            string Bulan = ddlBulan.SelectedValue;
            string Tahun = ddlTahun.SelectedItem.ToString();

            //if (ddlFilter.SelectedValue == "2")
            //{
            //    RBDetailDokumenBaru.Enabled = true;
            //}
            //else if (ddlFilter.SelectedValue == "3")
            //{
            //    RBPerubahan.Enabled = true;
            //}
            //else if (ddlFilter.SelectedValue == "4")
            //{
            //    RBPemusnahan.Enabled = true;
            //}

            Session["Bulan"] = Bulan;
            Session["Tahun"] = Tahun;
            Session["UnitKerjaID"] = UserID.Value;
            Session["FlagTahun"] = "";
            Session["Filter"] = ddlFilter.SelectedValue;

            LoadDataShare();
            LoadHeaderPeriodeBulan();
        }

        protected void BtnPreviewTahun_ServerClick(object sender, EventArgs e)
        {
            PanelGridShare.Visible = false;
            RBDetailShare.Enabled = true;
            RBHideDetailShare.Enabled = false;

            string Tahun = ddlPeriodeTahunAja.SelectedItem.ToString();

            Session["Bulan"] = Bulan;
            Session["Tahun"] = Tahun;
            Session["UnitKerjaID"] = UserID.Value;
            Session["FlagTahun"] = "PeriodeTahun";
            Session["Filter"] = ddlPeriodeTahunAja.SelectedValue;

            LoadDataShare();
            LoadHeaderPeriodeBulan();
        }

        private void LoadFilter()
        {
            ArrayList arrFilter = new ArrayList();
            //ISO_UPD2FacadeTemp masterDFacade = new ISO_UPD2FacadeTemp();
            //arrFilter = masterDFacade.RetrieveBulan();
            ddlFilter.Items.Clear();
            ddlFilter.Items.Add(new ListItem("--------- Pilih ---------", "0"));
            ddlFilter.Items.Add(new ListItem("Share ke Plant Lain", "11"));
            ddlFilter.Items.Add(new ListItem("Share dari Plant Lain", "1"));
            ddlFilter.Items.Add(new ListItem("Baru", "2"));
            ddlFilter.Items.Add(new ListItem("Perubahan", "3"));
            ddlFilter.Items.Add(new ListItem("Pemusnahan", "4"));

            foreach (ISO_UpdDMDTemp bln in arrFilter)
            {
                ddlBulan.Items.Add(new ListItem(bln.NamaBulan, bln.IDBulan.ToString()));
            }
        }

        private void LoadHeaderPeriodeBulan()
        {
            string PeriodeTahun = Session["FlagTahun"].ToString();
            string NamaBulan = Session["NamaBulan"].ToString();
            Users user = (Users)Session["Users"];

            string Bulan = Session["Bulan"].ToString();
            string Tahun = Session["Tahun"].ToString();
            if (Bulan == "1") { string JudulBulan = "JANUARI"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "2") { string JudulBulan = "FEBRUARI"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "3") { string JudulBulan = "MARET"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "4") { string JudulBulan = "APRIL"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "5") { string JudulBulan = "MEI"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "6") { string JudulBulan = "JUNI"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "7") { string JudulBulan = "JULI"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "8") { string JudulBulan = "AGUSTUS"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "9") { string JudulBulan = "SEPTEMBER"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "10") { string JudulBulan = "OKTOBER"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "11") { string JudulBulan = "NOVEMBER"; Session["JBulan"] = JudulBulan; }
            else if (Bulan == "12") { string JudulBulan = "DESEMBER"; Session["JBulan"] = JudulBulan; }


            string JudulNamaBulan = Session["JBulan"].ToString();
            LabelH1.Visible = true;

            if (PeriodeTahun == "PeriodeTahun")
            {
                LabelH1.Text = "PERIODE TAHUN : " + Tahun + " ";
            }
            else if (PeriodeTahun == "")
            {
                LabelH1.Text = "PERIODE : " + JudulNamaBulan + "  - " + Tahun + " ";
            }
        }

        //private void LoadDataShareJombang()
        //{
        //    string NamaBulan = Session["NamaBulan"].ToString();
        //    Users user = (Users)Session["Users"];
        //    string Bulan = Session["Bulan"].ToString(); string Tahun = Session["Tahun"].ToString();
        //    ISO_UPD2FacadeTemp FacadeMonitor21 = new ISO_UPD2FacadeTemp();
        //    ISO_UpdDMDTemp DomainMonitor21 = new ISO_UpdDMDTemp();
        //    int TotalDataShareAntarPlant = FacadeMonitor21.RetrieveTotalDataShareJombang(Bulan, Tahun);

        //    if (TotalDataShareAntarPlant > 0)
        //    {
        //        LabelShareJmb.Text = "TOTAL SHARE PLANT JOMBANG DI CTRP-KRWG";
        //        txtShareJmb.Text = "=" + " " + TotalDataShareAntarPlant.ToString();

        //        RBHideDetailJmb.Enabled = false; RBDetailJmb.Enabled = true;
        //    }
        //    else if (TotalDataShareAntarPlant == 0)
        //    {
        //        LabelShareJmb.Text = "TOTAL SHARE PLANT JOMBANG DI CTRP-KRWG";
        //        txtShareJmb.Text = "=" + " " + TotalDataShareAntarPlant.ToString();

        //        RBHideDetailJmb.Enabled = false; RBDetailJmb.Enabled = false;
        //    }
        //}

        private void LoadDistribusi()
        {
            string NamaBulan = Session["NamaBulan"].ToString();
            Users user = (Users)Session["Users"];
            string Bulan = Session["Bulan"].ToString(); string Tahun = Session["Tahun"].ToString();
            ISO_UPD2FacadeTemp FacadeMonitor21 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor21 = new ISO_UpdDMDTemp();
            int TotalDataShareAntarPlant = FacadeMonitor21.RetrieveTotalDataShareAntarPlant(Bulan, Tahun);

            if (TotalDataShareAntarPlant > 0)
            {
                if (user.UnitKerjaID == 7)
                {
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString();
                }
                else if (user.UnitKerjaID == 1)
                {
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString();
                }
                else if (user.UnitKerjaID == 13)
                {
                    PanelGridShareAntarPlant.Visible = true; PanelShare.Visible = true;
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString();
                }
                RBHideDetailShareAntarPlant.Enabled = false; RBDetailShareAntarPlant.Enabled = true;
            }
            else if (TotalDataShareAntarPlant == 0)
            {
                if (user.UnitKerjaID == 7)
                {
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString(); RBDetailShareAntarPlant.Enabled = false;
                }
                else if (user.UnitKerjaID == 1)
                {
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString(); RBDetailShareAntarPlant.Enabled = false;
                }
                else if (user.UnitKerjaID == 13)
                {
                    LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                    txtShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString(); RBDetailShareAntarPlant.Enabled = false;
                    //RBHideDetailShareAntarPlant.Enabled = false;
                }
                RBHideDetailShareAntarPlant.Enabled = false;
                //RBDetailShareAntarPlant.Enabled = false; RBHideDetailDokumenBaru.Enabled = false; RBHideDetailShare.Enabled = false;
                //LabelShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString(); RBDetailShareAntarPlant.Enabled = false;
            }
        }

        private void LoadDataShare()
        {
            Users user = (Users)Session["Users"];
            string PeriodeTahun = Session["FlagTahun"].ToString();
            string Bulan = Session["Bulan"].ToString();
            string Tahun = Session["Tahun"].ToString();
            string Filter = Session["Filter"].ToString();

            int UnitKerjaID = Convert.ToInt32(Session["UnitKerjaID"]);
            ISO_UPD2FacadeTemp FacadeMonitor1 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor1 = new ISO_UpdDMDTemp();

            if (PeriodeTahun == "PeriodeTahun")
            {
                string Flag = "PeriodeTahun"; Session["Flag"] = Flag;
            }
            else if (PeriodeTahun == "PeriodeBulan")
            {
                string Flag = "PeriodeBulan"; Session["Flag"] = Flag;
            }
            string FlagPeriode = Session["Flag"].ToString();

            int TotalShare = FacadeMonitor1.RetrieveDataShare(Bulan, Tahun, UnitKerjaID, FlagPeriode, Filter);


            if (Filter == "11")
            {
                PanelShare.Visible = false; PanelDokumenBaru.Visible = false; PanelDokumenPerubahan.Visible = false; PanelPemusnahan.Visible = false;
                PanelShareAntarPlant.Visible = true;
                //if (TotalShare > 0)
                //{
                //    LabelShareAntarPlant.Text = "TOTAL SHARE DOKUMEN DARI PLANT TERKAIT"; txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString();
                //    RBDetailShareAntarPlant.Enabled = true;
                //}
                //else
                //{
                //    LabelShareAntarPlant.Text = "TOTAL SHARE DOKUMEN DARI PLANT TERKAIT"; txtShareAntarPlant.Text = "=" + " " + "0"; RBDetailShareAntarPlant.Enabled = false;
                //    RBDetailShareAntarPlant.Enabled = false;
                //}
                if (TotalShare > 0)
                {
                    if (user.UnitKerjaID == 7)
                    {
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 1)
                    {
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        PanelGridShareAntarPlant.Visible = true; PanelShare.Visible = true;
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString();
                    }
                    RBHideDetailShareAntarPlant.Enabled = false; RBDetailShareAntarPlant.Enabled = true;
                }
                else if (TotalShare == 0)
                {
                    if (user.UnitKerjaID == 7)
                    {
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString(); RBDetailShareAntarPlant.Enabled = false;
                    }
                    else if (user.UnitKerjaID == 1)
                    {
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString(); RBDetailShareAntarPlant.Enabled = false;
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        LabelShareAntarPlant.Text = "TOTAL DOKUMEN YG SUDAH TER DISTRIBUSI KE ALL DEPT";
                        txtShareAntarPlant.Text = "=" + " " + TotalShare.ToString(); RBDetailShareAntarPlant.Enabled = false;
                        //RBHideDetailShareAntarPlant.Enabled = false;
                    }
                    RBHideDetailShareAntarPlant.Enabled = false;
                    //RBDetailShareAntarPlant.Enabled = false; RBHideDetailDokumenBaru.Enabled = false; RBHideDetailShare.Enabled = false;
                    //LabelShareAntarPlant.Text = "=" + " " + TotalDataShareAntarPlant.ToString(); RBDetailShareAntarPlant.Enabled = false;
                }

            }
            else if (Filter == "1")
            {
                PanelShare.Visible = true; PanelDokumenBaru.Visible = false; PanelDokumenPerubahan.Visible = false; PanelPemusnahan.Visible = false;
                PanelShareAntarPlant.Visible = false;
                //if (TotalShare > 0)
                //{
                //    LabelShare.Text = "TOTAL SHARE DOKUMEN"; txtShare.Text = "=" + " " + TotalShare.ToString();
                //    RBHideDetailShare.Visible = false;
                //}
                //else
                //{
                //    LabelShare.Text = "TOTAL SHARE DOKUMEN"; txtShare.Text = "=" + " " + "0"; 
                //    RBDetailShare.Enabled = false;
                //    RBHideDetailShare.Visible = false;
                //}
                if (TotalShare > 0)
                {
                    if (user.UnitKerjaID == 1)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-JOMBANG DI CITEUREUP"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 7)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT CTRP-JOMBANG DI KARAWANG"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-CTRP DI JOMBANG"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                }
                else
                {
                    //LabelShare.Text = "TOTAL SHARE PLANT CTRP DI KRWG-JMBG"; txtShare.Text = "=" + " " + "0"; RBDetailShare.Enabled = false;
                    //LabelShare.Visible = true; txtShare.Visible = true;
                    if (user.UnitKerjaID == 1)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-JOMBANG DI CITEUREUP"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                    else if (user.UnitKerjaID == 7)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT CTRP-JOMBANG DI KARAWANG"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-CTRP DI JOMBANG"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                }
            }
            else if (Filter == "2")
            {
                PanelShare.Visible = false; PanelDokumenBaru.Visible = true; PanelDokumenPerubahan.Visible = false; PanelPemusnahan.Visible = false;
                PanelShareAntarPlant.Visible = false;
                if (TotalShare > 0)
                {
                    LabelDokumenBaru.Text = "TOTAL PERMINTAAN DOKUMEN BARU"; txtDokumenBaru.Text = "=" + " " + TotalShare.ToString();
                    RBDetailDokumenBaru.Enabled = true;
                    RBHideDetailDokumenBaru.Visible = false;
                }
                else
                {
                    LabelDokumenBaru.Text = "TOTAL PERMINTAAN DOKUMEN BARU"; txtDokumenBaru.Text = "=" + " " + "0";
                    RBDetailDokumenBaru.Enabled = false;
                    RBHideDetailDokumenBaru.Visible = false;
                }
            }
            else if (Filter == "3")
            {
                PanelShare.Visible = false; PanelDokumenBaru.Visible = false; PanelDokumenPerubahan.Visible = true; PanelPemusnahan.Visible = false;
                PanelShareAntarPlant.Visible = false;
                if (TotalShare > 0)
                {
                    LabelPerubahan.Text = "TOTAL PERMINTAAN PERUBAHAN"; txtPerubahan.Text = "=" + " " + TotalShare.ToString();
                    RBHideDetailShareAntarPlant.Visible = false;
                    RBHidePerubahan.Visible = false;
                }
                else
                {
                    txtPerubahan.Text = "TOTAL PERMINTAAN PERUBAHAN"; txtPerubahan.Text = "=" + " " + "0";
                    RBPerubahan.Enabled = false;
                    RBHideDetailShareAntarPlant.Visible = false;
                }
            }
            else if (Filter == "4")
            {
                PanelShare.Visible = false; PanelDokumenBaru.Visible = false; PanelDokumenPerubahan.Visible = false; PanelPemusnahan.Visible = true;
                PanelShareAntarPlant.Visible = false;
                if (TotalShare > 0)
                {
                    LabelPemusnahan.Text = "TOTAL PERMINTAAN PEMUSNAHAN"; txtPemusnahan.Text = "=" + " " + TotalShare.ToString();
                    RBHidePemusnahan.Visible = false;
                }
                else
                {
                    txtPemusnahan.Text = "TOTAL PERMINTAAN PEMUSNAHAN"; txtPemusnahan.Text = "=" + " " + "0";
                    RBPemusnahan.Enabled = false;
                    RBHidePemusnahan.Visible = false;
                }
            }
            else if (Filter == "")
            {
                PanelShare.Visible = true; PanelDokumenBaru.Visible = true; PanelDokumenPerubahan.Visible = true; PanelPemusnahan.Visible = true;
                PanelShareAntarPlant.Visible = true;
                //PanelShareJombang.Visible = false;
                if (TotalShare > 0)
                {
                    if (user.UnitKerjaID == 1)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-JOMBANG DI CITEUREUP"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 7)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT CTRP-JOMBANG DI KARAWANG"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-CTRP DI JOMBANG"; txtShare.Text = "=" + " " + TotalShare.ToString();
                    }
                }
                else
                {
                    //LabelShare.Text = "TOTAL SHARE PLANT CTRP DI KRWG-JMBG"; txtShare.Text = "=" + " " + "0"; RBDetailShare.Enabled = false;
                    //LabelShare.Visible = true; txtShare.Visible = true;
                    if (user.UnitKerjaID == 1)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-JOMBANG DI CITEUREUP"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                    else if (user.UnitKerjaID == 7)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT CTRP-JOMBANG DI KARAWANG"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                    else if (user.UnitKerjaID == 13)
                    {
                        LabelShare.Text = "TOTAL SHARE DARI PLANT KRWG-CTRP DI JOMBANG"; txtShare.Text = "=" + " " + "0";
                        RBDetailShare.Enabled = false; LabelShare.Visible = true; txtShare.Visible = true;
                    }
                }
            }
        }

        private void LoadDataDokumenBaru()
        {
            string NamaBulan = Session["NamaBulan"].ToString();
            Users user = (Users)Session["Users"];
            string Bulan = Session["Bulan"].ToString(); string Tahun = Session["Tahun"].ToString();
            ISO_UPD2FacadeTemp FacadeMonitor2 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor2 = new ISO_UpdDMDTemp();
            int TotalDokumenBaru = FacadeMonitor2.RetrieveDataDokumenBaru(Bulan, Tahun);

            if (TotalDokumenBaru > 0)
            {
                LabelDokumenBaru.Text = "TOTAL PERMINTAAN DOKUMEN BARU";
                txtDokumenBaru.Text = "=" + " " + TotalDokumenBaru.ToString();
                RBDetailDokumenBaru.Enabled = true;
            }
            else if (TotalDokumenBaru == 0)
            {
                LabelDokumenBaru.Text = "TOTAL PERMINTAAN DOKUMEN BARU";
                txtDokumenBaru.Text = "=" + " " + TotalDokumenBaru.ToString();
                RBDetailDokumenBaru.Enabled = false;
            }
        }

        private void LoadDataPerubahan()
        {
            string NamaBulan = Session["NamaBulan"].ToString();
            Users user = (Users)Session["Users"];
            string Bulan = Session["Bulan"].ToString(); string Tahun = Session["Tahun"].ToString();
            ISO_UPD2FacadeTemp FacadeMonitor4 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor4 = new ISO_UpdDMDTemp();
            int TotalDokumenP = FacadeMonitor4.RetrieveDataDokumenP(Bulan, Tahun);

            if (TotalDokumenP > 0)
            {
                LabelPerubahan.Text = "TOTAL PERMINTAAN PERUBAHAN";
                txtPerubahan.Text = "=" + " " + TotalDokumenP.ToString();
                RBPerubahan.Enabled = true;
            }
            else if (TotalDokumenP == 0)
            {
                LabelPerubahan.Text = "TOTAL PERMINTAAN PERUBAHAN";
                txtPerubahan.Text = "=" + " " + TotalDokumenP.ToString(); RBPerubahan.Enabled = false;
                RBPerubahan.Enabled = false;
            }
        }

        private void LoadDataPemusnahan()
        {
            string NamaBulan = Session["NamaBulan"].ToString();
            Users user = (Users)Session["Users"];
            string Bulan = Session["Bulan"].ToString(); string Tahun = Session["Tahun"].ToString();
            ISO_UPD2FacadeTemp FacadeMonitor5 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor5 = new ISO_UpdDMDTemp();
            int TotalDokumenM = FacadeMonitor5.RetrieveDataDokumenM(Bulan, Tahun);

            if (TotalDokumenM > 0)
            {
                LabelPemusnahan.Text = "TOTAL PERMINTAAN PEMUSNAHAN";
                txtPemusnahan.Text = "=" + " " + TotalDokumenM.ToString();
            }
            else if (TotalDokumenM == 0)
            {
                LabelPemusnahan.Text = "TOTAL PERMINTAAN PEMUSNAHAN";
                txtPemusnahan.Text = "=" + " " + TotalDokumenM.ToString(); RBPemusnahan.Enabled = false;
            }
        }

        protected void RBDetailShareAntarPlant_CheckedChanged(object sender, EventArgs e)
        {
            //LoadGridShare();
            //LoadDataShare();
            LoadDistribusi();
            LoadGridDistribusi();

            PanelGridUPD.Visible = false;

            RBDetailShare.Checked = false;
            RBDetailShare.Enabled = false;

            RBHideDetailShare.Checked = false;
            RBHideDetailShare.Enabled = false;

            RBDetailDokumenBaru.Enabled = false;
            RBHideDetailDokumenBaru.Enabled = false;
            RBHideDetailDokumenBaru.Checked = false;
            RBPerubahan.Enabled = false;
            RBHidePerubahan.Enabled = false;

            RBDetailShareAntarPlant.Checked = true;
            RBDetailShareAntarPlant.Enabled = false;
            RBHideDetailShareAntarPlant.Checked = false;
            RBHideDetailShareAntarPlant.Enabled = true;
        }

        protected void RBHideDetailShareAntarPlant_CheckedChanged(object sender, EventArgs e)
        {
            //LoadDistribusi();
            //LoadGridDistribusi();
            //LoadDataPemusnahan();

            //PanelGridShare.Visible = false;
            //RBDetailShare.Checked = false;
            //RBDetailShare.Enabled = true;
            //RBDetailShareAntarPlant.Checked = false;
            //PanelGridShareAntarPlant.Visible = false;
            //RBDetailShareAntarPlant.Enabled = true;
            //RBHideDetailShareAntarPlant.Checked = false;

            //RBHideDetailShare.Checked = false;
            //RBHideDetailShare.Enabled = false;

            //RBDetailDokumenBaru.Enabled = true;
            //RBHideDetailDokumenBaru.Enabled = false;
            //RBHideDetailDokumenBaru.Checked = false;

            //RBPerubahan.Enabled = true;
            //RBPemusnahan.Enabled = true;
            Response.Redirect("Monitoring.aspx");
        }

        //protected void RBDetailJmb_CheckedChanged(object sender, EventArgs e)
        //{
        //   // LoadDataShareJombang();
        //    //PanelShareJombang.Visible = true;
        //}

        //protected void RBHideDetailJmb_CheckedChanged(object sender, EventArgs e)
        //{ }

        /** RB Detail Share Jombang ke Other Plant **/
        protected void RBDetailShare_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridShare();
            LoadDataShare();
            LoadDistribusi();
            LoadGridDistribusi();
            PanelGridShareAntarPlant.Visible = false;

            PanelGridUPD.Visible = false;
            //PanelShareJombang.Visible = true;

            RBDetailShare.Checked = true;
            RBDetailShare.Enabled = false;

            RBHideDetailShare.Checked = false;
            RBHideDetailShare.Enabled = true;

            RBDetailDokumenBaru.Enabled = false;
            RBHideDetailDokumenBaru.Enabled = false;
            RBHideDetailDokumenBaru.Checked = false;
            RBPerubahan.Enabled = false;
            RBHidePerubahan.Enabled = false;

            RBDetailShareAntarPlant.Checked = false;
            RBDetailShareAntarPlant.Enabled = false;
            RBHideDetailShareAntarPlant.Checked = false;
            RBHideDetailShareAntarPlant.Enabled = false;
        }

        protected void RBHideDetailShare_CheckedChanged(object sender, EventArgs e)
        {
            //PanelGridShare.Visible = false;
            //PanelGridShareAntarPlant.Visible = false;
            //RBDetailShare.Checked = false;
            //RBDetailShare.Enabled = true;

            //RBHideDetailShare.Checked = false;        
            //RBHideDetailShare.Enabled = false;

            //RBDetailDokumenBaru.Enabled = true;
            //RBHideDetailDokumenBaru.Enabled = false;
            //RBHideDetailDokumenBaru.Checked = false;

            //RBPerubahan.Enabled = true;

            //RBDetailShareAntarPlant.Checked = false;
            //RBDetailShareAntarPlant.Enabled = true;
            //RBHideDetailShareAntarPlant.Checked = false;
            //RBHideDetailShareAntarPlant.Enabled = false;
            Response.Redirect("Monitoring.aspx");
        }

        protected void RBDetailDokumenBaru_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridUPD();

            PanelGridShare.Visible = false;
            PanelDokumenBaru.Visible = true;
            PanelDokumenPerubahan.Visible = false;
            PanelPemusnahan.Visible = false;

            RBHideDetailDokumenBaru.Enabled = true;
            RBDetailDokumenBaru.Enabled = false;
            RBHideDetailDokumenBaru.Visible = true;
            RBDetailDokumenBaru.Visible = true;
            RBDetailShareAntarPlant.Enabled = false;
            RBDetailShare.Enabled = false;
            RBPerubahan.Enabled = false;
        }

        protected void RBHideDetailDokumenBaru_CheckedChanged(object sender, EventArgs e)
        {
            //LoadDataPemusnahan(); LoadDataPerubahan();

            //PanelGridUPD.Visible = false;

            //RBDetailDokumenBaru.Checked = false;
            //RBDetailDokumenBaru.Enabled = true;
            //RBHideDetailDokumenBaru.Enabled = false;
            //RBHideDetailDokumenBaru.Checked = false;

            //RBDetailShare.Enabled = true;
            //RBHideDetailShare.Enabled = false;
            //RBHideDetailShare.Checked = false;

            ////PanelDokumenPerubahan.Visible = true;
            //RBDetailShareAntarPlant.Enabled = true;
            //PanelDokumenPerubahan.Enabled = true;
            //RBPemusnahan.Enabled = true;
            //RBPerubahan.Enabled = true;
            Response.Redirect("Monitoring.aspx");
        }

        protected void RBPerubahan_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridPerubahan();

            RBPerubahan.Enabled = false;
            RBPerubahan.Checked = true;
            RBHidePerubahan.Enabled = true;
            RBHidePerubahan.Visible = true;

            RBHideDetailDokumenBaru.Enabled = false;
            RBDetailDokumenBaru.Enabled = false;
            RBDetailShare.Enabled = false;
            RBHideDetailShare.Enabled = false;
            RBDetailShareAntarPlant.Enabled = false;

        }

        protected void RBHidePerubahan_CheckedChanged(object sender, EventArgs e)
        {
            //LoadDataPerubahan();
            //PanelGridUPDPerubahan.Visible = false;

            //RBPerubahan.Enabled = true;
            //RBPerubahan.Checked = false;

            //RBHidePerubahan.Enabled = false;
            //RBHidePerubahan.Checked = false;

            //RBDetailDokumenBaru.Enabled = true;
            //RBDetailShare.Enabled = true;
            //RBHideDetailShare.Enabled = false;
            //RBHideDetailShare.Checked = false;
            //RBDetailShareAntarPlant.Enabled = true;
            Response.Redirect("Monitoring.aspx");

        }

        protected void RBPemusnahan_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridPemusnahan();

            PanelGridShare.Visible = false;
            PanelDokumenBaru.Visible = false;
            PanelDokumenPerubahan.Visible = false;
            PanelPemusnahan.Visible = true;
            RBHidePemusnahan.Visible = true;

            RBPemusnahan.Enabled = false;
            RBHidePemusnahan.Enabled = true;
            RBDetailShareAntarPlant.Enabled = false;
        }

        protected void RBHidePemusnahan_CheckedChanged(object sender, EventArgs e)
        {
            //PanelGridPemusnahan.Visible = false;

            //RBPemusnahan.Enabled = true;
            //RBHidePemusnahan.Enabled = false;
            //RBHidePemusnahan.Checked = false;
            //RBPemusnahan.Checked = false;
            Response.Redirect("Monitoring.aspx");
        }

        protected void RBPeriodeTahunan_CheckedChanged(object sender, EventArgs e)
        {
            PanelGridShare.Visible = false;
            BtnPreview.Visible = false;
            BtnPreviewTahun.Visible = true;
            RBPeriodeBulanan.Checked = false;
            RBPeriodeTahunan.Checked = true;

            RBHideDetailShare.Enabled = false;
            RBDetailShare.Enabled = false;

            PanelPilihanTahun.Visible = true;
            PanelPilihanBulan.Visible = false;
            LabelPTahun.Visible = true;

            LabelPTahun.Text = "Periode Tahun :";
            txtShare.Text = "=" + " " + "0";

            LoadTahunAja();
            //LoadBulan(); ddlBulan.Enabled = true;
        }

        protected void RBPeriodeBulanan_CheckedChanged(object sender, EventArgs e)
        {
            RBDetailShareAntarPlant.Enabled = false;
            RBPeriodeBulanan.Checked = true;
            RBDetailShare.Enabled = false;
            RBDetailDokumenBaru.Enabled = false;
            RBPerubahan.Enabled = false;
            RBPemusnahan.Enabled = false;
            RBHideDetailShare.Enabled = false;
            RBHideDetailDokumenBaru.Enabled = false;
            RBHidePemusnahan.Enabled = false;
            RBHidePemusnahan.Enabled = false;
            PanelDokumenBaru.Visible = true;
            PanelShare.Visible = true;
            PanelPemusnahan.Visible = true;
            PanelDokumenPerubahan.Visible = true;
            PanelGridShare.Visible = false;
            PanelPilihanBulan.Visible = true;
            PanelPilihanTahun.Visible = false;
            LabelPBulan.Visible = true;
            LabelDokumenBaru.Visible = true;
            LabelPerubahan.Visible = true;
            LabelPemusnahan.Visible = true;
            BtnPreview.Visible = true;
            PanelGridShareAntarPlant.Visible = false;

            LabelPBulan.Text = "Periode Bulan : ";
            txtShare.Text = "=" + " " + "0";
            txtDokumenBaru.Text = "=" + " " + "0";
            txtPerubahan.Text = "=" + " " + "0";
            txtPemusnahan.Text = "=" + " " + "0";
            txtShareAntarPlant.Text = "=" + " " + "0";

            ddlBulan.Enabled = false;
            LoadTahun();
        }

        private void LoadBulan(int Tahun)
        {
            ArrayList arrBulan = new ArrayList();
            ISO_UPD2FacadeTemp masterDFacade = new ISO_UPD2FacadeTemp();
            arrBulan = masterDFacade.RetrieveBulan(Tahun);
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("---------- Pilih ----------", "0"));
            foreach (ISO_UpdDMDTemp bln in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bln.NamaBulan, bln.IDBulan.ToString()));
            }
        }

        private void LoadBulanShare(int Tahun, int UnitKerjaID)
        {
            ArrayList arrBulan = new ArrayList();
            ISO_UPD2FacadeTemp masterDFacade = new ISO_UPD2FacadeTemp();
            arrBulan = masterDFacade.RetrieveBulanShare(Tahun, UnitKerjaID);
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("---------- Pilih ----------", "0"));
            foreach (ISO_UpdDMDTemp bln in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bln.NamaBulan, bln.IDBulan.ToString()));
            }
        }

        private void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            ISO_UPD2FacadeTemp masterDFacade = new ISO_UPD2FacadeTemp();
            arrTahun = masterDFacade.RetrieveTahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (ISO_UpdDMDTemp thn in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(thn.Tahun, thn.Tahun));
            }
        }

        private void LoadTahunAja()
        {
            ArrayList arrTahun = new ArrayList();
            ISO_UPD2FacadeTemp masterDFacade = new ISO_UPD2FacadeTemp();
            arrTahun = masterDFacade.RetrieveTahun();
            ddlPeriodeTahunAja.Items.Clear();
            ddlPeriodeTahunAja.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (ISO_UpdDMDTemp thn in arrTahun)
            {
                ddlPeriodeTahunAja.Items.Add(new ListItem(thn.Tahun, thn.Tahun));
            }
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        { ddlFilter.Enabled = true; }

        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            if (ddlFilter.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, " Pilihan Filter By harus di pilih terlebih dahulu !! ");
                return;
            }

            if (ddlFilter.SelectedItem.ToString().Trim() == "Share dari Plant Lain")
            {
                LoadBulanShare(Convert.ToInt32(ddlTahun.SelectedValue), users.UnitKerjaID);
                ddlBulan.Enabled = true;
            }
            else
            {
                LoadBulan(Convert.ToInt32(ddlTahun.SelectedValue));
                ddlBulan.Enabled = true;
            }
        }

        protected void ddlPeriodeTahunAja_Change(object sender, EventArgs e)
        { }
        protected void RBFilter_CheckedChanged(object sender, EventArgs e)
        { }
        protected void ddlFilter_Change(object sender, EventArgs e)
        { }

        private void LoadGridShare()
        {
            ArrayList arrGridShare = new ArrayList();
            string Bulan1 = Session["Bulan"].ToString();
            string Tahun1 = Session["Tahun"].ToString();
            string UnitKerjaID = Session["UnitKerjaID"].ToString();
            string FlagPeriode = Session["Flag"].ToString();

            ISO_UPD2FacadeTemp FacadeMonitor2 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor2 = new ISO_UpdDMDTemp();
            arrGridShare = FacadeMonitor2.RetrieveDataGridShare(Bulan1, Tahun1, UnitKerjaID, FlagPeriode);

            if (arrGridShare.Count > 0)
            {
                PanelGridShare.Visible = true;
                GridViewDataShare.DataSource = arrGridShare;
                GridViewDataShare.DataBind();
            }
            else
            {
                RBDetailShare.Enabled = false; RBHideDetailShare.Enabled = false;
            }
        }

        private void LoadGridDistribusi()
        {
            ArrayList arrGridShareAP = new ArrayList();
            string Bulan1 = Session["Bulan"].ToString();
            string Tahun1 = Session["Tahun"].ToString();
            string UnitKerjaID = Session["UnitKerjaID"].ToString();
            string FlagPeriode = Session["Flag"].ToString();

            ISO_UPD2FacadeTemp FacadeMonitor22 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor22 = new ISO_UpdDMDTemp();
            arrGridShareAP = FacadeMonitor22.RetrieveDataGridShareAntarPlant(Bulan1, Tahun1, UnitKerjaID, FlagPeriode);

            if (arrGridShareAP.Count > 0)
            {
                PanelGridShareAntarPlant.Visible = true;
                GridViewDataShareAntarPlant.DataSource = arrGridShareAP;
                GridViewDataShareAntarPlant.DataBind();
            }
            else
            {
                RBDetailShareAntarPlant.Enabled = false; RBHideDetailShareAntarPlant.Enabled = false;
            }
        }

        private void LoadGridUPD()
        {
            ArrayList arrGridUPD = new ArrayList();
            string Bulan1 = Session["Bulan"].ToString();
            string Tahun1 = Session["Tahun"].ToString();
            string UnitKerjaID = Session["UnitKerjaID"].ToString();
            string FlagPeriode = Session["Flag"].ToString();

            ISO_UPD2FacadeTemp FacadeMonitor3 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor3 = new ISO_UpdDMDTemp();
            arrGridUPD = FacadeMonitor3.RetrieveDataGridUPD(Bulan1, Tahun1, UnitKerjaID, FlagPeriode);

            if (arrGridUPD.Count > 0)
            {
                PanelGridUPD.Visible = true;
                GridViewUPD.DataSource = arrGridUPD;
                GridViewUPD.DataBind();
            }
            else
            {
                RBHideDetailDokumenBaru.Enabled = false; RBDetailDokumenBaru.Enabled = false;
            }
        }

        private void LoadGridPerubahan()
        {
            ArrayList arrGridP = new ArrayList();
            string Bulan1 = Session["Bulan"].ToString();
            string Tahun1 = Session["Tahun"].ToString();
            string UnitKerjaID = Session["UnitKerjaID"].ToString();
            string FlagPeriode = Session["Flag"].ToString();

            ISO_UPD2FacadeTemp FacadeMonitor4 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor4 = new ISO_UpdDMDTemp();
            arrGridP = FacadeMonitor4.RetrieveDataGridP(Bulan1, Tahun1, UnitKerjaID, FlagPeriode);

            if (arrGridP.Count > 0)
            {
                PanelGridUPDPerubahan.Visible = true;
                GridViewPerubahan.DataSource = arrGridP;
                GridViewPerubahan.DataBind();
            }
            else
            {
                RBHidePerubahan.Enabled = false; RBPerubahan.Enabled = false;
            }
        }

        private void LoadGridPemusnahan()
        {
            ArrayList arrGridPS = new ArrayList();
            string Bulan1 = Session["Bulan"].ToString();
            string Tahun1 = Session["Tahun"].ToString();
            string UnitKerjaID = Session["UnitKerjaID"].ToString();
            string FlagPeriode = Session["Flag"].ToString();

            ISO_UPD2FacadeTemp FacadeMonitor5 = new ISO_UPD2FacadeTemp();
            ISO_UpdDMDTemp DomainMonitor5 = new ISO_UpdDMDTemp();
            arrGridPS = FacadeMonitor5.RetrieveDataGridPS(Bulan1, Tahun1, UnitKerjaID, FlagPeriode);

            if (arrGridPS.Count > 0)
            {
                PanelGridPemusnahan.Visible = true;
                GridViewPemusnahan.DataSource = arrGridPS;
                GridViewPemusnahan.DataBind();
            }
            else
            {
                RBHidePemusnahan.Enabled = false; RBPemusnahan.Enabled = false;
            }
        }

        private void LoadOpenUPD()
        {
            //Users user = (Users)Session["Users"];        
            //ArrayList arrUPD = new ArrayList();
            //ISO_UpdFacade updF = new ISO_UpdFacade();
            //ISO_Upd upd = new ISO_Upd();
            //int deptID = user.DeptID;
            //int userID = user.ID;       

            //arrUPD = updF.RetrieveForApprovalHeader();       

            //foreach (ISO_Upd upd1 in arrUPD)
            //{
            //    if (upd1.updNo != "")
            //    {
            //        noSPP.Value += upd1.updNo + ",";
            //    }           
            //}

            //noSPP.Value = (noSPP.Value != string.Empty) ? noSPP.Value.Substring(0, (noSPP.Value.Length - 1)) : "0"; 
        }

        protected void GridViewDataShare_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string NamaFolder = "SoftCopy";
                string Nama = e.CommandArgument.ToString().Trim();
                //string Nama2 = @"\" + Nama;
                //string dirPath = @"D:\UPD_PDF_Temp\SoftCopy\";
                string dirPath = Path.Combine(@"D:\UPD_PDF_Temp\", NamaFolder + "/");
                //string ext = Path.GetExtension(Nama);

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
                    Response.Flush();
                }
            }
        }
        protected void GridViewDataShare_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ISO_UpdDMDTemp gd = (ISO_UpdDMDTemp)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("LinkButton1") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
                             
                if (gd.StatusApproval.Trim() == "Tidak Ikut")
                {                  
                    e.Row.Attributes.Add("style", "font-color:Red; font-weight:bold; color:Red;");
                }     
            }
        }
        protected void GridViewUPD_RowDataBound(object sender, GridViewRowEventArgs e)
        { }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        public class ISO_UPD2FacadeTemp
        {
            private ISO_UpdDMDTemp objUPD = new ISO_UpdDMDTemp();
            protected string strError = string.Empty;
            private ArrayList arrData = new ArrayList();
            private ISO_UpdDMDTemp upd = new ISO_UpdDMDTemp();
            public string Criteria { get; set; }
            public string Field { get; set; }
            public string Where { get; set; }

            public int RetrieveDataShareAntarPlant(string Bulan, string Tahun, int PlantID, string Flag, string Filter)
            {
                string QueryFilter = string.Empty; string queryantarplant = string.Empty;
                Users user = (Users)HttpContext.Current.Session["Users"];
                if (user.UnitKerjaID == 1)
                {
                    queryantarplant = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                }
                else if (user.UnitKerjaID == 7)
                {
                    queryantarplant = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                }
                else if (user.UnitKerjaID == 13)
                {
                    queryantarplant = "[sqljombang.grcboard.com].bpasctrp.dbo.";
                }

                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(CreatedTime)='" + Bulan + "' and YEAR(CreatedTime)='" + Tahun + "' " : " YEAR(CreatedTime)='" + Tahun + "' ";
                // Share
                if (Filter == "1" || Filter == "")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UpdDMD where PlantID<>" + PlantID + " " +
                                  " and RowStatus>-1 and " + Query + " and PlantID<>" + PlantID + " ";
                }

                if (Filter == "11" || Filter == "")
                {
                    QueryFilter = " select COUNT(ID)TotalData from " + queryantarplant + "ISO_UpdDMD where PlantID<>" + PlantID + " " +
                                  " and RowStatus>-1 and " + Query + " and PlantID<>" + PlantID + " ";
                }

                // Baru
                else if (Filter == "2")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=1 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }
                // Perubahan
                else if (Filter == "3")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=2 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }
                // Pemusnahan
                else if (Filter == "4")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=3 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }

                string strsql = QueryFilter;
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalData"]);
                    }
                }

                return 0;
            }

            public int RetrieveDataShare(string Bulan, string Tahun, int PlantID, string Flag, string Filter)
            {
                string QueryFilter = string.Empty; string queryantarplant = string.Empty;
                Users user = (Users)HttpContext.Current.Session["Users"];
                if (user.UnitKerjaID == 1)
                {
                    queryantarplant = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                }
                else if (user.UnitKerjaID == 7)
                {
                    queryantarplant = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                }
                else if (user.UnitKerjaID == 13)
                {
                    queryantarplant = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                }

                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "' " : " YEAR(lastmodifiedtime)='" + Tahun + "' ";

                // Share
                if (Filter == "1" || Filter == "")
                {
                    if (user.UnitKerjaID == 13)
                    {
                        QueryFilter =
                        " select sum(TotalData)TotalData from ( " +
                        " select COUNT(ID)TotalData from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where PlantID<>13  and RowStatus>-1 " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where PlantID<>13  and RowStatus>-1  " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +
                        " ) as x ";

                    }
                    else if (user.UnitKerjaID == 1)
                    {
                        QueryFilter =
                        " select sum(TotalData)TotalData from ( " +
                        " select COUNT(ID)TotalData from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where PlantID<>1  and RowStatus>-1 " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where PlantID<>1  and RowStatus>-1  " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +

                        " ) as x ";

                    }
                    else if (user.UnitKerjaID == 7)
                    {
                        QueryFilter =
                        " select sum(TotalData)TotalData from ( " +
                        " select COUNT(ID)TotalData from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where PlantID<>7  and RowStatus>-1 " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where PlantID<>7  and RowStatus>-1  " +
                        " and  MONTH(lastmodifiedtime)='" + Bulan + "' and YEAR(lastmodifiedtime)='" + Tahun + "'  and StatusShare=11 and aktif=2 " +

                        " union all " +

                        " select COUNT(ID)TotalData from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where MONTH(TglNonAktif)='" + Bulan + "' " +
                        " and YEAR(TglNonAktif)='" + Tahun + "' and aktif=-2 " +

                        " ) as x ";

                    }
                    //QueryFilter = " select COUNT(ID)TotalData from " + queryantarplant + "ISO_UpdDMD where PlantID<>" + PlantID + "  and RowStatus>-1  " +
                    //              " and " + Query + " and StatusShare=11 and aktif=2 " +
                    //              " union all " +
                    //              " select COUNT(ID)TotalData from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where PlantID<>13  and RowStatus>-1   and  MONTH(createdtime)='8' and YEAR(createdtime)='2021'  and StatusShare=11 and aktif=2  ) as x ";
                }

                if (Filter == "11")
                {
                    QueryFilter = " select COUNT(ID)TotalData from " + queryantarplant + "ISO_UpdDMD where PlantID=" + PlantID + " " +
                                  " and RowStatus>-1 and " + Query + " ";
                }

                // Baru
                else if (Filter == "2")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=1 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }
                // Perubahan
                else if (Filter == "3")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=2 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }
                // Pemusnahan
                else if (Filter == "4")
                {
                    QueryFilter = " select COUNT(ID)TotalData from ISO_UPD where JenisUPD=3 and MONTH(CreatedTime)=" + Bulan + " and " +
                                  " YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 and PlanID=" + PlantID + " ";
                }

                string strsql = QueryFilter;
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalData"]);
                    }
                }

                return 0;
            }

            public int RetrieveTotalDataShareAntarPlant(string Bulan, string Tahun)
            {
                Users user = (Users)HttpContext.Current.Session["Users"];
                string query = string.Empty;

                //if (user.UnitKerjaID == 1)
                //{
                //    //query = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                //    query =
                //   " select sum(A)TotalDataShareAntarPlant from ( " +
                //   " select COUNT(ID)A from  [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //   " union all " +
                //   " select COUNT(ID)A from  [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //   " ) as x ";
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    //query = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
                //    query =
                //    " select sum(A)TotalDataShareAntarPlant from ( " +
                //    " select COUNT(ID)A from  [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //    " union all " +
                //    " select COUNT(ID)A from  [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //    " ) as x ";
                //}
                //else if (user.UnitKerjaID == 13)
                //{
                //    //query = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
                //    query = 
                //    " select sum(A)TotalDataShareAntarPlant from ( "+
                //    " select COUNT(ID)A from  [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //    " union all "+
                //    " select COUNT(ID)A from  [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdDMD where PlantID=" + user.UnitKerjaID + "  and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from  [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UpdTemp where rowstatus>-1)  " +
                //    " ) as x ";
                //}

                string strsql =
                //" select COUNT(ID)TotalDataShareAntarPlant from " + query + "ISO_UpdDMD where PlantID=" + user.UnitKerjaID + " " +
                //" and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from " + query + "ISO_UpdTemp where rowstatus>-1)";
                //"" + query + "";
                " select count(ID)TotalDataShareAntarPlant from ( " +
                " select ID,Nomor,Nama,NamaDept,TanggalShare,Kategori,RevisiNo,Permintaan2 " +
                " from (select x.*,x2.DocTypeName Permintaan2 " +
                " from (select A.ID,NoDocument Nomor,DocName Nama,B.DeptName NamaDept,LEFT(convert(char,A.LastModifiedTime,106),11)TanggalShare," +
                " C.DocCategory Kategori,RevisiNo,idUPD,A.LastModifiedTime,A.PlantID from ISO_UpdDMD A " +
                " inner join Dept B ON A.Dept=B.ID  inner join ISO_UpdDocCategory C ON C.ID=A.CategoryUPD where aktif>1 and A.RowStatus>-1 " +
                " and StatusShare=11 ) as x  left join ISO_UPD x1 ON x1.ID=x.idUPD inner join ISO_UpdDocType x2 ON x2.ID=x1.JenisUPD " +
                " where x1.RowStatus>-1 ) as x3  where MONTH(LastModifiedTime)=" + Bulan + " and YEAR(LastModifiedTime)=" + Tahun + "  and PlantID='" + user.UnitKerjaID + "' ) as x ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalDataShareAntarPlant"]);
                    }
                }

                return 0;
            }

            public int RetrieveTotalDataShareJombang(string Bulan, string Tahun)
            {
                Users user = (Users)HttpContext.Current.Session["Users"];
                string query = string.Empty;

                if (user.UnitKerjaID == 1)
                {
                    query = " [sqljombang.grcboard.com].bpasjombang.dbo.";
                }
                else if (user.UnitKerjaID == 7)
                {
                    query = " [sqljombang.grcboard.com].bpasjombang.dbo.";
                }
                else if (user.UnitKerjaID == 13)
                {
                    query = " ";
                }

                string strsql =
                " select COUNT(ID)TotalDataShareAntarPlant from " + query + "ISO_UpdDMD where PlantID=13 " +
                " and RowStatus>-1  and MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " and ID in (select IDMasterDokumen from " + query + "ISO_UpdTemp where rowstatus>-1)";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalDataShareAntarPlant"]);
                    }
                }

                return 0;
            }

            public int RetrieveDataDokumenBaru(string Bulan, string Tahun)
            {
                //Users user = (Users)HttpContext.Current.Session["Users"];
                //string query = string.Empty;

                //if (user.UnitKerjaID == 1)
                //{
                //    query = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    query = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
                //}

                string strsql =
                " select COUNT(ID)TotalDataDokumenBaru from iso_upd where MONTH(TglPengajuan)=" + Bulan + " and " +
                " YEAR(TglPengajuan)=" + Tahun + " and RowStatus>-1 and JenisUPD=1";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalDataDokumenBaru"]);
                    }
                }

                return 0;
            }

            public int RetrieveDataDokumenP(string Bulan, string Tahun)
            {
                string strsql =
                " select COUNT(ID)TotalDataDokumenP from iso_upd where MONTH(TglPengajuan)=" + Bulan + " and " +
                " YEAR(TglPengajuan)=" + Tahun + " and RowStatus>-1 and JenisUPD=2";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalDataDokumenP"]);
                    }
                }

                return 0;
            }

            public int RetrieveDataDokumenM(string Bulan, string Tahun)
            {
                string strsql =
                " select COUNT(ID)TotalDataDokumenM from iso_upd where MONTH(TglPengajuan)=" + Bulan + " and " +
                " YEAR(TglPengajuan)=" + Tahun + " and RowStatus>-1 and JenisUPD=3";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["TotalDataDokumenM"]);
                    }
                }

                return 0;
            }

            public ArrayList RetrieveDataGridShare(string Bulan, string Tahun, string PlantID, string Flag)
            {
                arrData = new ArrayList();
                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                " select ROW_NUMBER() over (order by Data1.PlantID asc ) as No,*," +
                " case when Permintaan='Usulan Perubahan Dokumen' then 'Perubahan' " +
                " when Permintaan='Usulan Dokumen Baru' then 'Baru' " +
                " when Permintaan='Usulan Pemusnahan Dokumen' then 'Pemusnahan' end Permintaan2, " +
                " case when PlantID=1 then 'Citeureup' when PlantID=7 then 'Karawang' when PlantID=13 then 'Jombang' end Share_From " +
                " from (select AA.ID,NoDocument Nomor,DocName Nama,(select alias from Dept A where A.ID=Dept " +
                " and RowStatus>-1)NamaDept,LEFT(convert(char,AA.CreatedTime,106),11)TanggalShare," +
                " case when AA.StatusShare=2  then 'Apv Mgr -> Skretariat ISO' " +
                " when AA.StatusShare=0  then 'Tidak Ikut' " +
                " when AA.StatusShare=4 and AA.ID in (select idUPD from ISO_UpdDMD where RowStatus>-1 and Aktif=1) then 'Actived by ISO -> Distribusi ISO' " +
                " when AA.StatusShare=4 and AA.ID in (select idUPD from ISO_UpdDMD where RowStatus>-1 and Aktif in (0,2)) then 'TerDistribusi by ISO' " +
                " when AA.StatusShare=1 then 'Open -> Mgr'+' '+(select alias from Dept A where A.ID=Dept  and RowStatus>-1) end StatusApproval, " +
                " (select DocCategory from ISO_UpdDocCategory kat where kat.ID=AA.CategoryUPD and " +
                " RowStatus >-1)Kategori,(select DocTypeName from ISO_UpdDocType pr where pr.ID=BB.JenisUPD)Permintaan,AA.RevisiNo, "+
                " case when StatusShare=0 then AlasanTidak else '-' end Alasan, " +
                " AA.PlantID,isnull(BB.FileName2,'-') Lampiran from ISO_UpdDMD AA INNER JOIN ISO_UPDTemp BB ON AA.ID=BB.IDMasterDokumen " +
                " where AA.PlantID<>" + PlantID + " and AA.RowStatus>-1 and " +
                " " + Query + " " +
                " and AA.PlantID<>" + PlantID + " and BB.RowStatus > -1 ) as Data1 order by No,NamaDept,kategori,Nomor ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            TanggalShare = sdr["TanggalShare"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString(),
                            StatusApproval = sdr["StatusApproval"].ToString(),
                            Alasan = sdr["Alasan"].ToString(),
                            Share_From = sdr["Share_From"].ToString(),
                            Lampiran = sdr["Lampiran"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveDataGridShareAntarPlant(string Bulan, string Tahun, string PlantID, string Flag)
            {
                Users user = (Users)HttpContext.Current.Session["Users"]; string query = string.Empty;
                //if (user.UnitKerjaID == 1)
                //{
                //    query = " [sqlkrwg.grcboard.com].bpaskrwg.dbo. ";
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    query = " [sqlctrp.grcboard.com].bpasctrp.dbo. ";
                //}
                //else if (user.UnitKerjaID == 13)
                //{
                //    query = " [sqlkrwg.grcboard.com].bpaskrwg.dbo. ";
                //}

                arrData = new ArrayList();
                //string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                //" select ROW_NUMBER() over (order by Data1.ID asc ) as No,*, " +
                //" case when Permintaan='Usulan Perubahan Dokumen' then 'Perubahan' " +
                //" when Permintaan='Usulan Dokumen Baru' then 'Baru' " +
                //" when Permintaan='Usulan Pemusnahan Dokumen' then 'Pemusnahan' end Permintaan2 " +
                //" from (select AA.ID,NoDocument Nomor,DocName Nama,(select alias from "+query+"Dept A where A.ID=Dept " +
                //" and RowStatus>-1)NamaDept,LEFT(convert(char,AA.CreatedTime,106),11)TanggalShare, " +
                //" case when AA.StatusShare=2  then 'Apv Mgr -> Skretariat ISO' " +
                //" when AA.StatusShare=0  then 'Tidak Ikut' " +
                //" when AA.StatusShare=4 and AA.ID in (select idUPD from " + query + "ISO_UpdDMD where RowStatus>-1 and Aktif=1) then 'Actived by ISO -> Distribusi ISO' " +
                //" when AA.StatusShare=4 and AA.ID in (select idUPD from " + query + "ISO_UpdDMD where RowStatus>-1 and Aktif in (0,2)) then 'TerDistribusi by ISO' " +
                //" when AA.StatusShare=1 then 'Open -> Mgr'+' '+(select alias from " + query + "Dept A where A.ID=Dept  and RowStatus>-1) end StatusApproval, " +
                //" (select DocCategory from " + query + "ISO_UpdDocCategory kat where kat.ID=AA.CategoryUPD and " +
                //" RowStatus >-1)Kategori,(select DocTypeName from " + query + "ISO_UpdDocType pr where pr.ID=BB.JenisUPD)Permintaan,AA.RevisiNo,case when StatusShare=0 then AlasanTidak else '-' end Alasan " +
                //" from " + query + "ISO_UpdDMD AA INNER JOIN " + query + "ISO_UPDTemp BB ON AA.ID=BB.IDMasterDokumen " +
                //" where AA.PlantID=" + PlantID + " and AA.RowStatus>-1 and " +
                //" " + Query + " " +
                //" and AA.PlantID=" + PlantID + " and BB.RowStatus > -1 ) as Data1 order by No,NamaDept,kategori ";

                " select  ROW_NUMBER() over (order by ID asc ) as No,Nomor,Nama,NamaDept,TanggalShare,Kategori,RevisiNo,Permintaan2 from ( " +
                " select x.*,x2.DocTypeName Permintaan2 from ( " +
                " select A.ID,NoDocument Nomor,DocName Nama,B.DeptName NamaDept,LEFT(convert(char,A.LastModifiedTime,106),11)TanggalShare, " +
                " C.DocCategory Kategori,RevisiNo,idUPD,A.LastModifiedTime,A.PlantID from ISO_UpdDMD A inner join Dept B ON A.Dept=B.ID " +
                " inner join ISO_UpdDocCategory C ON C.ID=A.CategoryUPD where aktif>1 and A.RowStatus>-1  and StatusShare=11 ) as x " +
                " left join ISO_UPD x1 ON x1.ID=x.idUPD inner join ISO_UpdDocType x2 ON x2.ID=x1.JenisUPD  where x1.RowStatus>-1 ) as x3 " +
                " where MONTH(LastModifiedTime)='" + Bulan + "' and YEAR(LastModifiedTime)='" + Tahun + "'  and PlantID='" + user.UnitKerjaID + "' ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            TanggalShare = sdr["TanggalShare"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString()
                            //StatusApproval = sdr["StatusApproval"].ToString(),
                            //Alasan = sdr["Alasan"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveDataGridUPD(string Bulan, string Tahun, string PlantID, string Flag)
            {
                arrData = new ArrayList();
                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                " select ROW_NUMBER() over (order by Data1.ID asc ) as No,UpdNo Nomor,UpdName Nama,Tanggal,RevisiNo, " +
                " case when jenisUPD=1 then 'Baru' when JenisUPD=2 then 'Perubahan' when JenisUPD=3 then 'Pemusnahan' end Permintaan2," +
                " Kategori,DeptName NamaDept," +
                " case when Apv=0 then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID in (7,23,14,26,15,13,22,11,12) then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID not in (7,23,14,26,15,13,22,11,12) then 'Apv Mgr' + ' ' + DeptName + ' ' + '-> PM' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv PM -> Skretariat ISO' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv PM -> HRD Corp.' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy = 'Bastari' then 'Apv HRD Corp -> Skretariat ISO' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr Corp -> HRD Corp.' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv Mgr Corp -> Skretariat ISO'  " +
                " when Apv=3 then 'Verifikasi ISO -> Mgr ISO' " +
                //" when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'TerDistribusi -> Finish' " +
                //" when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'Apv Head ISO -> Distribusi' " +
                " when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'Apv Mgr ISO -> Skretariat ISO' " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (1)) then 'Actived -> Distribusi'  " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'TerDistribusi -> Finish' " +

                " end StatusApproval " +
                " from (select ID,DeptID,[Type],LastModifiedBy,(select Alias from Dept A where A.ID=DeptID)DeptName,UpdNo,UpdName," +
                " LEFT(convert(char,createdtime,106),11)Tanggal,(select DocCategory from ISO_UpdDocCategory B where B.ID=CategoryUPD)Kategori," +
                " JenisUPD,RevisiNo,Apv from ISO_UPD where JenisUPD=1 and " +
                " MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " " +
                " and RowStatus>-1 and PlanID=" + PlantID + " ) as Data1 ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            Tanggal = sdr["Tanggal"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            //Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString(),
                            StatusApproval = sdr["StatusApproval"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveDataGridP(string Bulan, string Tahun, string PlantID, string Flag)
            {
                arrData = new ArrayList();
                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                " select ROW_NUMBER() over (order by Data1.ID asc ) as No,UpdNo Nomor,UpdName Nama,Tanggal,RevisiNo, " +
                " case when jenisUPD=1 then 'Baru' when JenisUPD=2 then 'Perubahan' when JenisUPD=3 then 'Pemusnahan' end Permintaan2," +
                " Kategori,DeptName NamaDept," +
                " case when Apv=0 then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID in (7,23,14,26,15,13,22,11,12) then 'Open -> Mgr'+' '+DeptName " +
                //" when Apv=1 and DeptID not in (7,23,14,26,15,13,22,11,12) then 'Apv Mgr' + ' ' + DeptName + ' ' + '-> PM' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv Mgr' + ' ' + DeptName + ' ' + '-> Skretariat ISO' " +
                //" when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv PM -> Skretariat ISO' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr' + ' ' + DeptName + ' -> HRD Corp.' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy = 'Bastari' then 'Apv HRD Corp -> Skretariat ISO' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr Corp -> HRD Corp.' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv Mgr Corp -> Skretariat ISO'  " +
                " when Apv=3 then 'Verifikasi ISO -> Mgr ISO' " +
                " when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'Apv Mgr ISO -> Skretariat ISO' " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (1)) then 'Actived -> Distribusi'  " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'TerDistribusi -> Finish' " +
                " end StatusApproval " +
                " from (select ID,DeptID,[Type],LastModifiedBy,(select Alias from Dept A where A.ID=DeptID)DeptName,UpdNo,UpdName," +
                " LEFT(convert(char,createdtime,106),11)Tanggal,(select DocCategory from ISO_UpdDocCategory B where B.ID=CategoryUPD)Kategori," +
                " JenisUPD,RevisiNo,Apv from ISO_UPD where JenisUPD=2 and " +
                " MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " " +
                " and RowStatus>-1 and PlanID=" + PlantID + " ) as Data1 ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            Tanggal = sdr["Tanggal"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            //Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString(),
                            StatusApproval = sdr["StatusApproval"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveDataGridPS(string Bulan, string Tahun, string PlantID, string Flag)
            {
                arrData = new ArrayList();
                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                " select ROW_NUMBER() over (order by Data1.ID asc ) as No,UpdNo, " +
                "(select upd1.nodocument from iso_upddmd upd1 where upd1.ID=IDMaster and upd1.rowstatus>-1)Nomor,UpdName Nama,Tanggal,RevisiNo, " +
                " case when jenisUPD=1 then 'Baru' when JenisUPD=2 then 'Perubahan' when JenisUPD=3 then 'Pemusnahan' end Permintaan2," +
                " Kategori,DeptName NamaDept," +
                " case when Apv=0 then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID in (7,23,14,26,15,13,22,11,12) then 'Open -> Mgr'+' '+DeptName " +
                //" when Apv=1 and DeptID not in (7,23,14,26,15,13,22,11,12) then 'Apv Mgr' + ' ' + DeptName + ' ' + '-> PM' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv PM -> Mgr ISO' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr Dept -> HRD Corp.' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy = 'Bastari' then 'Apv HRD Corp -> Mgr ISO' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr Corp -> HRD Corp.' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv Mgr Corp -> Mgr ISO'  " +
                " when Apv=3 then 'Verifikasi ISO -> Skretariat ISO' " +

                " when Apv=4 and IDMaster in (select ID from ISO_UpdDMD where RowStatus >-1  and Aktif <> -2) then 'Apv Mgr ISO -> Skretariat ISO' " +
                " when Apv=5 and IDMaster in (select ID from ISO_UpdDMD where RowStatus >-1  and Aktif = -2) then 'Skretariat ISO -> Sdh Musnah' " +

                //" when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'Apv Mgr ISO -> Skretariat ISO' " +
                //" when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (1)) then 'Actived -> Distribusi'  " +
                //" when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'TerDistribusi -> Finish' " + 

                " end StatusApproval " +
                " from (select ID,DeptID,[Type],LastModifiedBy,(select Alias from Dept A where A.ID=DeptID)DeptName,UpdNo,UpdName," +
                " LEFT(convert(char,createdtime,106),11)Tanggal,(select DocCategory from ISO_UpdDocCategory B where B.ID=CategoryUPD)Kategori," +
                " JenisUPD,RevisiNo,Apv,IDMaster from ISO_UPD where JenisUPD=3 and " +
                " MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " " +
                " and RowStatus>-1 and PlanID=" + PlantID + " ) as Data1 ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            Tanggal = sdr["Tanggal"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            //Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString(),
                            StatusApproval = sdr["StatusApproval"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveDataGridM(string Bulan, string Tahun, string PlantID, string Flag)
            {
                arrData = new ArrayList();
                string Query = (Flag == "PeriodeBulan" || Flag == "") ? " MONTH(AA.CreatedTime)='" + Bulan + "' and YEAR(AA.CreatedTime)='" + Tahun + "' " : " YEAR(AA.CreatedTime)='" + Tahun + "' ";

                string strSQL =
                " select ROW_NUMBER() over (order by Data1.ID asc ) as No,UpdNo Nomor,UpdName Nama,Tanggal,RevisiNo, " +
                " case when jenisUPD=1 then 'Baru' when JenisUPD=2 then 'Perubahan' when JenisUPD=3 then 'Pemusnahan' end Permintaan2," +
                " Kategori,DeptName NamaDept," +
                " case when Apv=0 then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID in (7,23,14,26,15,13,22,11,12) then 'Open -> Mgr'+' '+DeptName " +
                " when Apv=1 and DeptID not in (7,23,14,26,15,13,22,11,12) then 'Apv Mgr' + ' ' + DeptName + ' ' + '-> PM' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv PM -> Skretariat ISO' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv PM -> HRD Corp.' " +
                " when Apv=2 and DeptID not in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy = 'Bastari' then 'Apv HRD Corp -> Skretariat ISO' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=2 and LastModifiedBy <> 'Bastari' then 'Apv Mgr Corp -> HRD Corp.' " +
                " when Apv=2 and DeptID in (7,23,14,26,15,13,22,11,12) and [Type]=1 then 'Apv Mgr Corp -> Skretariat ISO'  " +
                " when Apv=3 then 'Verifikasi ISO -> Mgr ISO' " +
                //" when Apv=4 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif = 2) then 'TerDistribusi -> Finish' " +
                //" when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif = 2) then 'Apv Head ISO -> Distribusi' " +

                " when Apv=4 and ID not in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'Apv Mgr ISO -> Skretariat ISO' " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (1)) then 'Actived -> Distribusi'  " +
                " when Apv=5 and ID in (select idUPD from ISO_UpdDMD where RowStatus >-1  and Aktif in (0,2)) then 'TerDistribusi -> Finish' " +

                " end StatusApproval " +
                " from (select ID,DeptID,[Type],LastModifiedBy,(select Alias from Dept A where A.ID=DeptID)DeptName,UpdNo,UpdName," +
                " LEFT(convert(char,createdtime,106),11)Tanggal,(select DocCategory from ISO_UpdDocCategory B where B.ID=CategoryUPD)Kategori," +
                " JenisUPD,RevisiNo,Apv from ISO_UPD where JenisUPD=3 and " +
                " MONTH(CreatedTime)=" + Bulan + " and  YEAR(CreatedTime)=" + Tahun + " " +
                " and RowStatus>-1 and PlanID=1 ) as Data1 ";

                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            No = sdr["No"].ToString(),
                            Nomor = sdr["Nomor"].ToString(),
                            Nama = sdr["Nama"].ToString(),
                            Tanggal = sdr["Tanggal"].ToString(),
                            RevisiNo = sdr["RevisiNo"].ToString(),
                            //Permintaan2 = sdr["Permintaan2"].ToString(),
                            Kategori = sdr["Kategori"].ToString(),
                            NamaDept = sdr["NamaDept"].ToString(),
                            StatusApproval = sdr["StatusApproval"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveBulan(int Tahun)
            {
                arrData = new ArrayList();
                string strSQL =
                "  select IDBulan,NamaBulan from (select DISTINCT(DATENAME(MONTH,CreatedTime))NamaBulan,MONTH(CreatedTime)IDBulan from ISO_Upd " +
                " where RowStatus>-1 and YEAR(createdtime)=" + Tahun + " ) as Data1 order by IDBulan ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            NamaBulan = sdr["NamaBulan"].ToString(),
                            IDBulan = Convert.ToInt32(sdr["IDBulan"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveBulanShare(int Tahun, int PlantID)
            {
                arrData = new ArrayList();
                string strSQL =
                "  select IDBulan,NamaBulan from (select DISTINCT(DATENAME(MONTH,CreatedTime))NamaBulan,MONTH(CreatedTime)IDBulan from ISO_UpdDMD " +
                " where RowStatus>-1 and YEAR(createdtime)=" + Tahun + "  and PlantID <> " + PlantID + ") as Data1 order by IDBulan ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            NamaBulan = sdr["NamaBulan"].ToString(),
                            IDBulan = Convert.ToInt32(sdr["IDBulan"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveTahun()
            {
                arrData = new ArrayList();
                string strSQL =
                //" select DISTINCT(YEAR(CreatedTime))Tahun from ISO_Upd  where RowStatus>-1 and YEAR(CreatedTime)>'2017' ";
               " select * from ( " +
               " select DISTINCT(YEAR(CreatedTime))Tahun from ISO_Upd  where RowStatus>-1 and YEAR(CreatedTime)>'2017' " +
               " union " +
               " select DISTINCT(YEAR(CreatedTime))Tahun from ISO_UpdDMD  where RowStatus>-1 and YEAR(CreatedTime)>'2017' " +
               " ) as x order by Tahun desc ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ISO_UpdDMDTemp
                        {
                            Tahun = sdr["Tahun"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ISO_UpdDMDTemp GenerateObjectDataShare(SqlDataReader sqlDataReader)
            {
                objUPD = new ISO_UpdDMDTemp();
                objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objUPD.StatusShare = sqlDataReader["StatusShare"].ToString();
                objUPD.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
                return objUPD;
            }




            //public ArrayList RetrieveBudgetDeliveryTahunan(string Tahun, string Periode, string PengKali, string NamaBulan)
            //{
            //    arrData = new ArrayList();
            //    if (Tahun == "2018")
            //    {
            //        if (NamaBulan == "Okt")
            //        {
            //            string PeriodeBulan = "(Okt)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Nov")
            //        {
            //            string PeriodeBulan = "(Okt+Nov)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Des")
            //        {
            //            string PeriodeBulan = "(Okt+Nov+Des)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //    }
            //    else if (Tahun != "2018")
            //    {
            //        if (NamaBulan == "Jan")
            //        {
            //            string PeriodeBulan = "(Jan)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Feb")
            //        {
            //            string PeriodeBulan = "(Jan+Feb)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Mrt")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Apr")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Mei")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Jun")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Jul")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Agst")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Sept")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Okt")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Nov")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt+Nov)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //        else if (NamaBulan == "Des")
            //        {
            //            string PeriodeBulan = "(Jan+Feb+Mrt+Apr+Mei+Jun+Jul+Agst+Sept+Okt+Nov+Des)";
            //            HttpContext context = HttpContext.Current; context.Session["Bulan"] = PeriodeBulan;
            //        }
            //    }

            //    HttpContext context1 = HttpContext.Current;
            //    string PeriodeBulan2 = (string)(context1.Session["Bulan"]);

            //    string Query1 = string.Empty;
            //    Query1 = "ISNULL(((NULLIF(" + PeriodeBulan2 + ",0)/(MaxBudget*" + PengKali + "))*100),0)Persen";
            //    string strSQL =
            //    "  select *," + PeriodeBulan2 + "Total, " +
            //    " " + Query1 + " from ( " +
            //    " select ROW_NUMBER() over (order by data1.urutan asc ) as NO,Data1.JenisUnit,Data1.NoPol,data1.MaxBudget, " +
            //    " case when Data1.UnitKerja=1 then 'CITEUREUP' when Data1.UnitKerja=7 then 'KARAWANG' end Plant, " +
            //    " ISNULL(A01,0)Jan,ISNULL(A02,0)Feb,ISNULL(A03,0)Mrt,ISNULL(A04,0)Apr, " +
            //    " ISNULL(A05,0)Mei,ISNULL(A06,0)Jun,ISNULL(A07,0)Jul,ISNULL(A08,0)Agst, " +
            //    " ISNULL(A09,0)Sept,ISNULL(A10,0)Okt,ISNULL(A11,0)Nov,ISNULL(A12,0)Des,Data1.urutan " +
            //    " from (select A.JenisUnit,A.NoPol,A.Urutan,A.MaxBudget,A.UnitKerja, " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "01' and GroupID in (8,9) and Status=2))'A01', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "02' and GroupID in (8,9) and Status=2))'A02', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "03' and GroupID in (8,9) and Status=2))'A03', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "04' and GroupID in (8,9) and Status=2))'A04', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "05' and GroupID in (8,9) and Status=2))'A05', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "06' and GroupID in (8,9) and Status=2))'A06', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "07' and GroupID in (8,9) and Status=2))'A07', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "08' and GroupID in (8,9) and Status=2))'A08', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "09' and GroupID in (8,9) and Status=2))'A09', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "10' and GroupID in (8,9) and Status=2))'A10', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "11' and GroupID in (8,9) and Status=2))'A11', " +
            //    " (select sum(Quantity*AvgPrice) from PakaiDetail pk where pk.NoPol=A.NoPol and PakaiID in (select ID from Pakai where LEFT(convert(char,pakaidate,112),6)='" + Tahun + "12' and GroupID in (8,9) and Status=2))'A12' " +
            //    " from MaterialBudgetArmada A where A.RowStatus > -1 ) as Data1 " +
            //    " ) as Data2 group by NoPol,NO,JenisUnit,MaxBudget,Jan,feb,Mrt,Apr,Mei,Jun,Jul,Agst,Sept,Okt,Nov,Des,urutan,plant order by Data2.urutan ";

            //    DataAccess da = new DataAccess(Global.ConnectionString());
            //    SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            //    if (da.Error == string.Empty && sdr.HasRows)
            //    {
            //        while (sdr.Read())
            //        {
            //            arrData.Add(new BudgetDelivery
            //            {
            //                No = sdr["No"].ToString(),
            //                Plant = sdr["Plant"].ToString(),
            //                JenisUnit = sdr["JenisUnit"].ToString(),
            //                NoPol = sdr["NoPol"].ToString(),
            //                MaxBudget = Convert.ToDecimal(sdr["MaxBudget"]),
            //                Jan = Convert.ToDecimal(sdr["Jan"]),
            //                Feb = Convert.ToDecimal(sdr["Feb"]),
            //                Mrt = Convert.ToDecimal(sdr["Mrt"]),
            //                Apr = Convert.ToDecimal(sdr["Apr"]),
            //                Mei = Convert.ToDecimal(sdr["Mei"]),
            //                Jun = Convert.ToDecimal(sdr["Jun"]),
            //                Jul = Convert.ToDecimal(sdr["Jul"]),
            //                Agst = Convert.ToDecimal(sdr["Agst"]),
            //                Sept = Convert.ToDecimal(sdr["Sept"]),
            //                Okt = Convert.ToDecimal(sdr["Okt"]),
            //                Nov = Convert.ToDecimal(sdr["Nov"]),
            //                Des = Convert.ToDecimal(sdr["Des"]),
            //                Total = Convert.ToDecimal(sdr["Total"]),
            //                Persen = Convert.ToDecimal(sdr["Persen"])
            //            });
            //        }
            //    }
            //    return arrData;
            //}

            //public ArrayList RetrieveBulan()
            //{
            //    arrData = new ArrayList();
            //    string strSQL =
            //    " select Bulan,case " +
            //    " when Bulan=1 then 'JANUARI' " +
            //    " when Bulan=2 then 'FEBRUARI' " +
            //    " when Bulan=3 then 'MARET' " +
            //    " when Bulan=4 then 'APRIL' " +
            //    " when Bulan=5 then 'MEI' " +
            //    " when Bulan=6 then 'JUNI' " +
            //    " when Bulan=7 then 'JULI' " +
            //    " when Bulan=8 then 'AGUSTUS' " +
            //    " when Bulan=9 then 'SEPTEMBER' " +
            //    " when Bulan=10 then 'OKTOBER' " +
            //    " when Bulan=11 then 'NOVEMBER' " +
            //    " when Bulan=12 then 'DESEMBER' " +
            //    " end BulanNama from " +
            //    " (select DISTINCT(MONTH(CreatedTime))Bulan from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as Data1 ";
            //    DataAccess da = new DataAccess(Global.ConnectionString());
            //    SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            //    if (da.Error == string.Empty && sdr.HasRows)
            //    {
            //        while (sdr.Read())
            //        {
            //            arrData.Add(new BudgetDelivery
            //            {
            //                Bulan = sdr["Bulan"].ToString(),
            //                BulanNama = sdr["BulanNama"].ToString()
            //            });
            //        }
            //    }
            //    return arrData;
            //}


        }

        public class ISO_UpdDMDTemp
        {
            public string Lampiran { get; set; }
            public string Share_From { get; set; }
            public string No { get; set; }
            public string Plant { get; set; }
            public string StatusShare { get; set; }
            public decimal MaxBudget { get; set; }
            public decimal Actual { get; set; }
            public decimal Persen { get; set; }
            public string Nomor { get; set; }
            public string Nama { get; set; }
            public string NamaDept { get; set; }
            public string TanggalShare { get; set; }
            public string Tanggal { get; set; }
            public string StatusApproval { get; set; }
            public string Kategori { get; set; }
            public string Permintaan { get; set; }
            public string RevisiNo { get; set; }
            public string Permintaan2 { get; set; }
            public string NamaBulan { get; set; }
            public string Bulan { get; set; }
            public string Tahun { get; set; }
            public string BulanNama { get; set; }
            public string Alasan { get; set; }

            public int IDBulan { get; set; }
            public int PlantID { get; set; }
            public int ID { get; set; }
            public decimal Mrt { get; set; }
            public decimal Apr { get; set; }
            public decimal Mei { get; set; }
            public decimal Jun { get; set; }
            public decimal Jul { get; set; }
            public decimal Agst { get; set; }
            public decimal Sept { get; set; }
            public decimal Okt { get; set; }
            public decimal Nov { get; set; }
            public decimal Des { get; set; }
            public decimal Total { get; set; }
        }

    }

}