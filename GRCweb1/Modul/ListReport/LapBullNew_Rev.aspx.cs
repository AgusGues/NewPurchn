using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Xml.Linq;
using System.Net;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapBullNew_Rev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                int Tahun0 = int.Parse(DateTime.Now.Year.ToString());
                int Bulan0 = int.Parse(DateTime.Now.Month.ToString());

                if (Bulan0 == 1)
                { int Tahun = Tahun0 - 1; int Bulan = 12; Session["Tahun"] = Tahun; Session["Bulan"] = Bulan; }
                else
                { int Tahun = Tahun0; int Bulan = Bulan0 - 1; Session["Tahun"] = Tahun; Session["Bulan"] = Bulan; }

                string Bulan1 = Session["Bulan"].ToString();
                if (Bulan1 == "1") { txtPeriode.Text = "JANUARI"; }
                else if (Bulan1 == "2") { txtPeriode.Text = "FEBRUARI"; }
                else if (Bulan1 == "3") { txtPeriode.Text = "MARET"; }
                else if (Bulan1 == "4") { txtPeriode.Text = "APRIL"; }
                else if (Bulan1 == "5") { txtPeriode.Text = "MEI"; }
                else if (Bulan1 == "6") { txtPeriode.Text = "JUNI"; }
                else if (Bulan1 == "7") { txtPeriode.Text = "JULI"; }
                else if (Bulan1 == "8") { txtPeriode.Text = "AGUSTUS"; }
                else if (Bulan1 == "9") { txtPeriode.Text = "SEPTEMBER"; }
                else if (Bulan1 == "10") { txtPeriode.Text = "OKTOBER"; }
                else if (Bulan1 == "11") { txtPeriode.Text = "NOVEMBER"; }
                else if (Bulan1 == "12") { txtPeriode.Text = "DESEMBER"; }

                txtTahun.Text = Session["Tahun"].ToString();
                Users users = (Users)Session["Users"];
                string UnitKerja1 = users.UnitKerjaID.ToString();
                Session["UnitKerja1"] = UnitKerja1;

                LaporanBulanan LPH = new LaporanBulanan();
                LaporanBulananFacade LPHF = new LaporanBulananFacade();
                LPH = LPHF.cekUser(users.ID);
                Session["Flag"] = LPH.Flag;
                Session["GroupID2"] = LPH.GroupID;

                string Ket = "Inventory";
                LaporanBulanan lp1 = new LaporanBulanan();
                LaporanBulananFacade lp2 = new LaporanBulananFacade();
                int Kunci = lp2.RetrieveKunci(Ket);

                if (Kunci == 0)
                { btnReleaseUlang.Enabled = false; }
                else if (Kunci == 1)
                { btnReleaseUlang.Enabled = true; }

                int TahunJDL = int.Parse(Session["Tahun"].ToString());
                int BulanJDL = int.Parse(Session["Bulan"].ToString());


                int days = DateTime.DaysInMonth(TahunJDL, BulanJDL);
                if (BulanJDL > 0)
                {
                    if (BulanJDL == 1) { string BulanL = "Jan"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 2) { string BulanL = "Feb"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 3) { string BulanL = "Mrt"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 4) { string BulanL = "Apr"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 5) { string BulanL = "Mei"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 6) { string BulanL = "Jun"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 7) { string BulanL = "Jul"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 8) { string BulanL = "Agst"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 9) { string BulanL = "Sep"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 10) { string BulanL = "Okt"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 11) { string BulanL = "Nov"; Session["BulanL"] = BulanL; }
                    if (BulanJDL == 12) { string BulanL = "Des"; Session["BulanL"] = BulanL; }
                }

                if (BulanJDL < 10)
                {
                    string PeriodeAwal = TahunJDL + "0" + BulanJDL + "01";
                    string PeriodeAkhir = TahunJDL + "0" + BulanJDL + days;
                    string Periode1 = "01" + "-" + "0" + BulanJDL + "-" + TahunJDL;
                    string Periode2 = days + "-" + "0" + BulanJDL + "-" + TahunJDL;
                    Session["PeriodeAwal"] = PeriodeAwal;
                    Session["PeriodeAkhir"] = PeriodeAkhir;
                    Session["PeriodeAwal"] = PeriodeAwal;
                    Session["PeriodeAkhir"] = PeriodeAkhir;
                    Session["Periode1"] = Periode1;
                    Session["Periode2"] = Periode2;
                }
                else
                {
                    string PeriodeAwal = TahunJDL.ToString() + BulanJDL + "01";
                    string PeriodeAkhir = TahunJDL.ToString() + BulanJDL + days;
                    string Periode1 = "01" + "-" + BulanJDL + "-" + TahunJDL;
                    string Periode2 = days + "-" + BulanJDL + "-" + TahunJDL;
                    Session["PeriodeAwal"] = PeriodeAwal;
                    Session["PeriodeAkhir"] = PeriodeAkhir;
                    Session["Periode1"] = Periode1;
                    Session["Periode2"] = Periode2;
                }

                string JudulBulan = Session["BulanL"].ToString();
                string JudulL1 = "01" + "-" + JudulBulan + "-" + TahunJDL;
                string JudulL2 = days + "-" + JudulBulan + "-" + TahunJDL;
                Session["JudulL1"] = JudulL1;
                Session["JudulL2"] = JudulL2;
                txtFromPostingPeriod.Text = Session["PeriodeAwal"].ToString();
                txtToPostingPeriod.Text = Session["PeriodeAkhir"].ToString();
                string PeriodeLap1 = JudulL1;
                string PeriodeLap2 = JudulL2;
                string PeriodeLaporan = JudulL1 + " " + "s/d" + " " + JudulL2;
                Session["PeriodeLaporan"] = PeriodeLaporan;
                txtLap.Text = PeriodeLaporan;


                if (LPH.UserID > 0)
                {
                    if (LPH.UserID == users.ID && LPH.Flag == 1)
                    {
                        LoadLapbul(LPH.GroupID, 0);
                    }
                    else if (LPH.UserID == users.ID && LPH.Flag == 2)
                    {
                        LoadLapbul(LPH.GroupID, 0);
                    }
                    else if (LPH.UserID == users.ID && LPH.Flag == 3)
                    {
                        LoadLapbul_Apv1();
                    }
                    else if (LPH.UserID == users.ID && LPH.Flag == 4)
                    {
                        LoadLapbul_Apv2();
                    }
                }
            }
        }

        protected void LoadLapbul(int GroupID, int FlagLagi)
        {
            if (FlagLagi == 5)
            {
                string BulanS1 = ddlBulan.SelectedItem.ToString().Trim(); Session["BulanS1"] = BulanS1;
                int Bulan1 = int.Parse(ddlBulan.SelectedValue); Session["Bulan01"] = Bulan1;
                int Tahun1 = int.Parse(ddlTahun.SelectedValue); Session["Tahun01"] = Tahun1;

            }
            else
            {
                string BulanS1 = txtPeriode.Text.Trim(); Session["BulanS1"] = BulanS1;
                int Tahun1 = int.Parse(Session["Tahun"].ToString()); Session["Tahun01"] = Tahun1;
                int Bulan1 = int.Parse(Session["Bulan"].ToString()); Session["Bulan01"] = Bulan1;
            }

            string BulanS = Session["BulanS1"].ToString();
            int Tahun = Convert.ToInt32(Session["Tahun01"]);
            int Bulan = Convert.ToInt32(Session["Bulan01"]);

            Session["Tahun1"] = Tahun;
            Session["Bulan1"] = Bulan;
            string UnitKerja = Session["UnitKerja1"].ToString();

            LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
            LaporanBulanan group = new LaporanBulanan();
            ArrayList arrGroup = new ArrayList();
            arrGroup = groupsPurchnFacade.RetrieveGroupPurchn(BulanS, Tahun, Bulan, GroupID, UnitKerja);

            GridView1.DataSource = arrGroup;
            GridView1.DataBind();
            Session["ListOfGroup"] = arrGroup;

            PanelFormHeader.Visible = true;
            PanelFormApvMgrLog.Visible = false;
            PanelFormApprovalPM.Visible = false;
            //btnLihat.Enabled = false;
            btnEmail.Enabled = false;

            string thn = Session["Tahun01"].ToString();
            string bln = Session["Bulan01"].ToString();

            string Flag1 = Session["Flag"].ToString();

            if (Flag1 != "0")
            {
                if (Flag1 == "2")
                {
                    btnCetak.Enabled = false; btnEmail.Enabled = false; PanelFormRelease.Visible = false;
                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    ArrayList arrListFile = GroupF.cekFile(thn, bln, GroupID, UnitKerja);
                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();

                }
                else if (Flag1 == "1")
                {
                    PanelFormHeader.Visible = true; PanelFormRelease.Visible = true; PanelFormApvMgrLog.Visible = false; PanelFormSentEmail.Visible = false;
                    btnCetak.Enabled = true; btnEmail.Enabled = true; PanelFormRelease.Visible = true;
                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    ArrayList arrListFile = GroupF.cekFile(thn, bln, GroupID, UnitKerja);
                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();

                }
            }
        }

        protected void LoadLapbul_Apv1()
        {
            int Flag = int.Parse(Session["Flag"].ToString());
            string Query1 = string.Empty;
            if (Flag == 3)
            {
                Query1 = "and B.Status=1";
            }
            else if (Flag == 4)
            {
                Query1 = "and B.Status=2";
            }

            int Tahun = int.Parse(Session["Tahun"].ToString());
            int Bulan = int.Parse(Session["Bulan"].ToString());
            //Session["Tahun1"] = Tahun;
            //Session["Bulan1"] = Bulan;
            //int days = DateTime.DaysInMonth(Tahun, Bulan);

            //string JudulBulan = Session["BulanL"].ToString();
            //string JudulL1 = "01" + "-" + JudulBulan + "-" + Tahun;
            //string JudulL2 = days + "-" + JudulBulan + "-" + Tahun;
            //Session["JudulL1"] = JudulL1;
            //Session["JudulL2"] = JudulL2;

            //if (Bulan < 10)
            //{
            //    string PeriodeAwal = Tahun + "0" + Bulan + "01";
            //    string PeriodeAkhir = Tahun + "0" + Bulan + days;
            //    string Periode1 = "01" + "-" + "0" + Bulan + "-" + Tahun;
            //    string Periode2 = days + "-" + "0" + Bulan + "-" + Tahun;
            //    Session["PeriodeAwal"] = PeriodeAwal;
            //    Session["PeriodeAkhir"] = PeriodeAkhir;
            //    Session["Periode1"] = Periode1;
            //    Session["Periode2"] = Periode2;
            //}
            //else
            //{
            //    string PeriodeAwal = Tahun + Bulan + "01";
            //    string PeriodeAkhir = Tahun.ToString() + Bulan + days;
            //    string Periode1 = "01" + "-" + Bulan + "-" + Tahun;
            //    string Periode2 = days + "-" + Bulan + "-" + Tahun;
            //    Session["PeriodeAwal"] = PeriodeAwal;
            //    Session["PeriodeAkhir"] = PeriodeAkhir;
            //    Session["Periode1"] = Periode1;
            //    Session["Periode2"] = Periode2;
            //}

            //txtFromPostingPeriod.Text = Session["PeriodeAwal"].ToString();
            //txtToPostingPeriod.Text = Session["PeriodeAkhir"].ToString();
            //string PeriodeLap1 = Session["Periode1"].ToString();
            //string PeriodeLap2 = Session["Periode2"].ToString();
            //string PeriodeLaporan = PeriodeLap1 + " " + "s/d" + " " + PeriodeLap2;
            //txtLap.Text = PeriodeLaporan;       


            LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
            LaporanBulanan group = new LaporanBulanan();
            ArrayList arrGroup = new ArrayList();
            arrGroup = groupsPurchnFacade.RetrieveGroupPurchn2(Tahun, Bulan);

            GridViewApv1.DataSource = arrGroup;
            GridViewApv1.DataBind();

            Session["ListOfGroup2"] = arrGroup;

            PanelFormHeader.Visible = false;
            btnEmail.Enabled = false;
            PanelFormApvMgrLog.Visible = true;
            btnReleaseUlang.Enabled = false;

            PanelFormApprovalPM.Visible = false;
            string thn = Session["Tahun"].ToString();
            string bln = Session["Bulan"].ToString();

            LaporanBulanan Group1 = new LaporanBulanan();
            LaporanBulananFacade GroupF = new LaporanBulananFacade();
            ArrayList arrListFile = GroupF.cekFileAll(thn, bln, Query1);

            lstBA2.DataSource = arrListFile;
            lstBA2.DataBind();
        }

        protected void LoadLapbul_Apv2()
        {
            int Flag = int.Parse(Session["Flag"].ToString());
            string Query1 = string.Empty;
            if (Flag == 3)
            {
                Query1 = "and B.Status=1";
            }
            else if (Flag == 4)
            {
                Query1 = "and B.Status=2";
            }

            int Tahun = int.Parse(Session["Tahun"].ToString());
            int Bulan = int.Parse(Session["Bulan"].ToString());
            Session["Tahun1"] = Tahun;
            Session["Bulan1"] = Bulan;
            int days = DateTime.DaysInMonth(Tahun, Bulan);

            string JudulBulan = Session["BulanL"].ToString();
            string JudulL1 = "01" + "-" + JudulBulan + "-" + Tahun;
            string JudulL2 = days + "-" + JudulBulan + "-" + Tahun;
            Session["JudulL1"] = JudulL1;
            Session["JudulL2"] = JudulL2;


            if (Bulan < 10)
            {
                string PeriodeAwal = Tahun + "0" + Bulan + "01";
                string PeriodeAkhir = Tahun + "0" + Bulan + days;
                string Periode1 = "01" + "-" + "0" + Bulan + "-" + Tahun;
                string Periode2 = days + "-" + "0" + Bulan + "-" + Tahun;
                Session["PeriodeAwal"] = PeriodeAwal;
                Session["PeriodeAkhir"] = PeriodeAkhir;
                Session["Periode1"] = Periode1;
                Session["Periode2"] = Periode2;
            }
            else
            {
                string PeriodeAwal = Tahun + Bulan + "01";
                string PeriodeAkhir = Tahun.ToString() + Bulan + days;
                string Periode1 = "01" + "-" + Bulan + "-" + Tahun;
                string Periode2 = days + "-" + Bulan + "-" + Tahun;
                Session["PeriodeAwal"] = PeriodeAwal;
                Session["PeriodeAkhir"] = PeriodeAkhir;
                Session["Periode1"] = Periode1;
                Session["Periode2"] = Periode2;
            }

            txtFromPostingPeriod.Text = Session["PeriodeAwal"].ToString();
            txtToPostingPeriod.Text = Session["PeriodeAkhir"].ToString();
            string PeriodeLap1 = Session["Periode1"].ToString();
            string PeriodeLap2 = Session["Periode2"].ToString();
            string PeriodeLaporan = PeriodeLap1 + " " + "s/d" + " " + PeriodeLap2;
            txtLap.Text = PeriodeLaporan;

            LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
            LaporanBulanan group = new LaporanBulanan();
            ArrayList arrGroup = new ArrayList();
            arrGroup = groupsPurchnFacade.RetrieveGroupPurchn3(Tahun, Bulan);

            GridViewApv2.DataSource = arrGroup;
            GridViewApv2.DataBind();
            Session["ListOfGroup2"] = arrGroup;

            PanelFormHeader.Visible = true;
            btnEmail.Enabled = false;
            PanelFormApvMgrLog.Visible = false;
            PanelFormApprovalPM.Visible = true;
            btnCetak.Enabled = false;
            PanelFormHeader.Visible = false;
            btnReleaseUlang.Enabled = false;
            //btnL.Enabled = false;
            //btnL.Enabled = false;
            //btnR.Enabled = false;
            //btnLihat.Enabled = false;

            string thn = Session["Tahun"].ToString();
            string bln = Session["Bulan"].ToString();

            LaporanBulanan Group1 = new LaporanBulanan();
            LaporanBulananFacade GroupF = new LaporanBulananFacade();
            ArrayList arrListFile = GroupF.cekFileAll(thn, bln, Query1);

            lstBA1.DataSource = arrListFile;
            lstBA1.DataBind();
        }

        //protected void ChkAll1_CheckedChanged(object sender, EventArgs e)
        //{
        //    ArrayList arrGroup = (ArrayList)Session["ListOfGroup"];
        //    int i = 0;
        //    if (ChkAll1.Checked == true)
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
        //            chk.Checked = true;
        //            i = i + 1;
        //        }

        //    }
        //    else
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
        //            chk.Checked = false;
        //            i = i + 1;
        //        }
        //    }

        //}

        //protected void ChkAll2_CheckedChanged(object sender, EventArgs e)
        //{
        //    ArrayList arrGroup = (ArrayList)Session["ListOfGroup2"];
        //    int i = 0;
        //    if (ChkAll2.Checked == true)
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridViewApv1.Rows[i].FindControl("check2");
        //            chk.Checked = true;
        //            i = i + 1;
        //        }

        //    }
        //    else
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridViewApv1.Rows[i].FindControl("check2");
        //            chk.Checked = false;
        //            i = i + 1;
        //        }
        //    }

        //}

        protected void ChkAll3_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrGroup = (ArrayList)Session["ListOfEmail"];
            int i = 0;
            if (ChkAll3.Checked == true)
            {
                foreach (LaporanBulanan ListGroup in arrGroup)
                {
                    CheckBox chk = (CheckBox)GridView3.Rows[i].FindControl("check3");
                    chk.Checked = true;
                    i = i + 1;
                }

            }
            else
            {
                foreach (LaporanBulanan ListGroup in arrGroup)
                {
                    CheckBox chk = (CheckBox)GridView3.Rows[i].FindControl("check3");
                    chk.Checked = false;
                    i = i + 1;
                }
            }

        }

        //protected void ChkAll4_CheckedChanged(object sender, EventArgs e)
        //{
        //    ArrayList arrGroup = (ArrayList)Session["ListOfGroup2"];
        //    int i = 0;
        //    if (ChkAll4.Checked == true)
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridViewApv2.Rows[i].FindControl("check4");
        //            chk.Checked = true;
        //            i = i + 1;
        //        }

        //    }
        //    else
        //    {
        //        foreach (LaporanBulanan ListGroup in arrGroup)
        //        {
        //            CheckBox chk = (CheckBox)GridViewApv2.Rows[i].FindControl("check4");
        //            chk.Checked = false;
        //            i = i + 1;
        //        }
        //    }

        //}


        protected void btnApv1_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrGroup = (ArrayList)Session["ListOfGroup2"];

            int i = 0;
            foreach (LaporanBulanan ListGroup in arrGroup)
            {
                CheckBox cb = (CheckBox)GridViewApv1.Rows[i].Cells[1].FindControl("check2");
                if (cb.Checked)
                {
                    Users users = (Users)Session["Users"];
                    ListGroup.ID = int.Parse(GridViewApv1.Rows[i].Cells[0].Text);
                    Session["IDLap"] = ListGroup.ID;
                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    int intResult = 0;

                    Group1.ID = ListGroup.ID;
                    Group1.Tahun = int.Parse(Session["Tahun"].ToString());
                    Group1.Bulan = int.Parse(Session["Bulan"].ToString());
                    Group1.Status = ListGroup.Status;
                    Group1.Users = users.UserName;

                    intResult = GroupF.UpdateLapBul(Group1);

                    //LoadCetak();
                }
                i = i + 1;
            }
            LoadLapbul_Apv1();
        }

        protected void btnApv2_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrGroup = (ArrayList)Session["ListOfGroup2"];

            int i = 0;
            foreach (LaporanBulanan ListGroup in arrGroup)
            {
                CheckBox cb = (CheckBox)GridViewApv2.Rows[i].Cells[1].FindControl("check4");
                if (cb.Checked)
                {
                    Users users = (Users)Session["Users"];
                    ListGroup.ID = int.Parse(GridViewApv2.Rows[i].Cells[0].Text);
                    Session["IDLap"] = ListGroup.ID;
                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    int intResult = 0;

                    Group1.ID = ListGroup.ID;
                    Group1.Tahun = int.Parse(Session["Tahun"].ToString());
                    Group1.Bulan = int.Parse(Session["Bulan"].ToString());
                    Group1.Status = ListGroup.Status;
                    Group1.Users = users.UserName;

                    intResult = GroupF.UpdateLapBul2(Group1);

                    //LoadCetak();

                }
                i = i + 1;
            }

            LoadLapbul_Apv2();

        }

        protected void LoadLihatPDF()
        {
            Users users = (Users)Session["Users"];
            LaporanBulanan LPu = new LaporanBulanan();
            LaporanBulananFacade LPuF = new LaporanBulananFacade();
            LPu = LPuF.cekUserID(users.ID);
            string UnitKerja = Session["UnitKerja1"].ToString();

            if (LPu.ID > 0)
            {
                if (users.ID == LPu.ID || LPu.Flag == 1)
                {
                    PanelFormApprovalPM.Visible = false;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormRelease.Visible = false;
                    PanelFormSentEmail.Visible = false;
                    //PanelFormViewLapBul.Visible = true;
                    int FlagID = LPu.Flag;

                    string thn = Session["Tahun"].ToString();
                    string bln = Session["Bulan"].ToString();

                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();

                    ArrayList arrListFile = GroupF.cekFile(thn, bln, FlagID, UnitKerja);

                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();
                }
                else if (users.ID == LPu.ID || LPu.Flag == 2)
                {
                    PanelFormApprovalPM.Visible = false;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormRelease.Visible = false;
                    PanelFormSentEmail.Visible = false;
                    //PanelFormViewLapBul.Visible = true;
                    PanelFormHeader.Visible = true;
                    int FlagID = LPu.Flag;

                    btnCetak.Visible = false;
                    btnEmail.Visible = false;
                    btnApv2.Visible = false;
                    //btnR.Visible = false;
                    //btnL.Visible = false;
                    string thn = Session["Tahun"].ToString();
                    string bln = Session["Bulan"].ToString();

                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    ArrayList arrListFile = GroupF.cekFile(thn, bln, FlagID, UnitKerja);

                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();
                }
            }
        }

        protected void btnLihat_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            LaporanBulanan LPu = new LaporanBulanan();
            LaporanBulananFacade LPuF = new LaporanBulananFacade();
            string UnitKerja = Session["UnitKerja1"].ToString();

            LPu = LPuF.cekUserID(users.ID);

            if (LPu.ID > 0)
            {
                if (users.ID == LPu.ID || LPu.Flag == 1)
                {
                    PanelFormApprovalPM.Visible = false;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormRelease.Visible = false;
                    PanelFormSentEmail.Visible = false;
                    //PanelFormViewLapBul.Visible = true;
                    int FlagID = LPu.Flag;
                    string thn = Session["Tahun"].ToString();
                    string bln = Session["Bulan"].ToString();

                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    ArrayList arrListFile = GroupF.cekFile(thn, bln, FlagID, UnitKerja);

                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();
                }
                else if (users.ID == LPu.ID || LPu.Flag == 2)
                {
                    PanelFormApprovalPM.Visible = false;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormRelease.Visible = false;
                    int FlagID = LPu.Flag;
                    PanelFormSentEmail.Visible = false;
                    //PanelFormViewLapBul.Visible = true;

                    PanelFormHeader.Visible = true;

                    btnCetak.Visible = false;
                    btnEmail.Visible = false;
                    btnApv2.Visible = false;
                    //btnR.Visible = true;
                    //btnL.Visible = false;


                    string thn = Session["Tahun"].ToString();
                    string bln = Session["Bulan"].ToString();

                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    ArrayList arrListFile = GroupF.cekFile(thn, bln, FlagID, UnitKerja);

                    lstBA3.DataSource = arrListFile;
                    lstBA3.DataBind();
                }
            }
        }

        protected void btnEmail_ServerClick(object sender, EventArgs e)
        {
            LoadDataEmail();
            PanelFormHeader.Visible = false; PanelPeriode.Visible = false;
        }

        protected void LoadDataEmail()
        {
            PanelPeriode.Visible = false;
            PanelFormHeader.Visible = false;
            PanelFormRelease.Visible = false;
            PanelFormApvMgrLog.Visible = false;
            PanelFormSentEmail.Visible = true;
            btnCetak.Enabled = false;
            btnEmail.Enabled = false;

            Users users = (Users)Session["Users"];
            LaporanBulanan LPu = new LaporanBulanan();
            LaporanBulananFacade LPuF = new LaporanBulananFacade();
            LPu = LPuF.cekUserID(users.ID);
            int FlagIDGroup = LPu.GroupID;
            string UnitKerja = users.UnitKerjaID.ToString();

            string thn1 = Session["Tahun"].ToString();
            string bln1 = Session["Bulan"].ToString();
            LaporanBulanan Group2 = new LaporanBulanan();
            LaporanBulananFacade GroupF2 = new LaporanBulananFacade();
            ArrayList arrEmail = new ArrayList();
            arrEmail = GroupF2.RetrieveEmailLapBul(thn1, bln1, FlagIDGroup, UnitKerja);

            GridView3.DataSource = arrEmail;
            GridView3.DataBind();
            Session["ListOfEmail"] = arrEmail;
        }

        protected void btnSent_ServerClick(object sender, EventArgs e)
        {
            PanelFormHeader.Visible = false;

            string UnitKerja = Session["UnitKerja1"].ToString();
            string thn2 = Session["Tahun"].ToString();
            string bln2 = Session["Bulan"].ToString();

            if (bln2 == "1")
            { string Bulan = "JAN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "2") { string Bulan = "FEB"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "3") { string Bulan = "MRT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "4") { string Bulan = "APR"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "5") { string Bulan = "MEI"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "6") { string Bulan = "JUN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "7") { string Bulan = "JUL"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "8") { string Bulan = "AGST"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "9") { string Bulan = "SEPT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "10") { string Bulan = "OKT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "11") { string Bulan = "NOV"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "12") { string Bulan = "DES"; Session["Bulan11"] = Bulan; }
            string NamaBulan = Session["Bulan11"].ToString();

            Users users = (Users)Session["Users"];
            LaporanBulanan LPu = new LaporanBulanan();
            LaporanBulananFacade LPuF = new LaporanBulananFacade();
            LPu = LPuF.cekUserID(users.ID);
            int GroupID = LPu.GroupID;
            string UserName = LPu.Noted;
            string GroupName = LPu.GroupName;

            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();

            //Pengirim   
            if (UnitKerja == "1")
            {
                mail.From = new MailAddress("log.bb.admin_ctrp@grcboard.com", UserName);
            }
            else if (UnitKerja == "7")
            {
                mail.From = new MailAddress("log.bb.admin_krwg@grcboard.com", UserName);
            }
            else if (UnitKerja == "13")
            {
                mail.From = new MailAddress("log.bb.admin_jmb@grcboard.com", UserName);
            }


            //Tujuan
            //mail.To.Add(new MailAddress("beny@grcboard.com", "Test"));
            if (UnitKerja == "1")
            {
                mail.To.Add(new MailAddress("acc.spv_ctrp@grcboard.com", "Harlina"));
                mail.To.Add(new MailAddress("fin_ctrp@grcboard.com", "Syahirun"));
            }
            else if (UnitKerja == "7")
            {
                mail.To.Add(new MailAddress("acc_krwg@grcboard.com", "Reslin"));
                //mail.To.Add(new MailAddress("fin_ctrp@grcboard.com", "Herlan"));
            }
            else if (UnitKerja == "13")
            {
                mail.To.Add(new MailAddress("acc_jmb@grcboard.com", "Dhila"));
                //mail.To.Add(new MailAddress("fin_ctrp@grcboard.com", "Herlan"));
            }

            //CC
            if (UnitKerja == "1")
            {
                //mail.CC.Add(new MailAddress("devian@grcboard.com", "Devian"));
                mail.CC.Add(new MailAddress("log.bb_ctrp@grcboard.com", "R. Rizky"));
                mail.CC.Add(new MailAddress("iso_ctrp@grcboard.com", "Ceicilia"));
                mail.CC.Add(new MailAddress("log.bb.admin_ctrp@grcboard.com", UserName));
            }
            else if (UnitKerja == "7")
            {
                mail.CC.Add(new MailAddress("adi.sufiandi@grcboard.com", "Adi"));
                mail.CC.Add(new MailAddress("log.bb.admin_krwg@grcboard.com", "Tedy"));
                mail.CC.Add(new MailAddress("iso_krwg@grcboard.com", "Waty"));
            }
            else if (UnitKerja == "13")
            {
                //mail.CC.Add(new MailAddress("putut.cahyono@grcboard.com", "Putut"));
                mail.CC.Add(new MailAddress("Kasie.logbb_jombang@grcboard.com", "Roni"));
                mail.CC.Add(new MailAddress("iso_jombang@grcboard.com", "Febrina"));
            }

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            mail.Subject = "Laporan Bulanan Inventory" + " " + GroupName + " " + "Periode :" + " " + NamaBulan + " " + thn2;
            mail.Body += "Dengan Hormat, \n\r\n\r\n\r";
            mail.Body += "Dengan ini kami kirimkan Laporan Bulanan Inventory untuk Periode : " + " " + NamaBulan + "-" + thn2 + " \n\r\n\r\n\r";
            mail.Body += "Terimakasih, " + "\n\r";
            mail.Body += "Salam GRCBOARD " + "\n\r";
            mail.Body += "Regard's, " + "\n\r";
            mail.Body += UserName + "\n\r";

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
            else if (UnitKerja == "13")
            {
                NetworkCred.UserName = "log.bb.admin_jmb@grcboard.com";
                NetworkCred.Password = "Lo91stIk*JMB";
            }

            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            ArrayList arrEmail1 = (ArrayList)Session["ListOfEmail"];
            int i = 0;
            foreach (LaporanBulanan ListEmail1 in arrEmail1)
            {
                CheckBox cb = (CheckBox)GridView3.Rows[i].Cells[1].FindControl("check3");
                if (cb.Checked)
                {
                    string NamaFile1 = GridView3.Rows[i].Cells[3].Text;
                    string NamaFile = NamaFile1.Trim();
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"D:\Laporan\" + NamaFile);
                    mail.Attachments.Add(attachment);

                    string LapID = GridView3.Rows[i].Cells[0].Text;
                    Session["LapID"] = LapID;
                }

                i = i + 1;

                int LapID1 = Convert.ToInt32(Session["LapID"]);
                LaporanBulanan LapBul = new LaporanBulanan();
                LaporanBulananFacade LapBulFacade = new LaporanBulananFacade();

                int intResult = 0;

                LapBul.LastModifiedBy = users.UserName;
                LapBul.ID = LapID1;

                intResult = LapBulFacade.UpdateLapbulEmail(LapBul);

            }

            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            LoadGridListEmail(GroupID);
        }

        protected void LoadGridListEmail(int GroupID)
        {
            PanelFormHeader.Visible = true;
            PanelFormRelease.Visible = false;
            PanelFormApvMgrLog.Visible = false;
            //PanelFormViewLapBul.Visible = false;
            PanelFormSentEmail.Visible = true;
            //btnR.Enabled = true;
            btnCetak.Enabled = false;
            btnEmail.Enabled = false;
            //btnLihat.Enabled = true;

            Users users = (Users)Session["Users"];
            string UnitKerja = users.UnitKerjaID.ToString();

            int FlagIDGroup = GroupID;

            string thn1 = Session["Tahun"].ToString();
            string bln1 = Session["Bulan"].ToString();
            LaporanBulanan Group2 = new LaporanBulanan();
            LaporanBulananFacade GroupF2 = new LaporanBulananFacade();
            ArrayList arrEmail = new ArrayList();
            arrEmail = GroupF2.RetrieveEmailLapBul(thn1, bln1, FlagIDGroup, UnitKerja);

            GridView3.DataSource = arrEmail;
            GridView3.DataBind();
            Session["ListOfEmail"] = arrEmail;
        }




        protected void btnSentORI_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrEmail1 = (ArrayList)Session["ListOfEmail"];
            int i = 0;
            foreach (LaporanBulanan ListEmail1 in arrEmail1)
            {
                CheckBox cb = (CheckBox)GridView3.Rows[i].Cells[1].FindControl("check3");
                if (cb.Checked)
                {
                    ListEmail1.ID = int.Parse(GridView3.Rows[i].Cells[0].Text);
                    int ID = int.Parse(GridView3.Rows[i].Cells[0].Text);
                    LaporanBulanan Group2 = new LaporanBulanan();
                    LaporanBulananFacade GroupF2 = new LaporanBulananFacade();
                    ListEmail1.FileName = GridView3.Rows[i].Cells[3].Text;
                    string NamaFile1 = GridView3.Rows[i].Cells[3].Text;
                    string NamaFile = NamaFile1.Trim();


                    string GroupInventory = GridView3.Rows[i].Cells[2].Text;
                    string thn2 = Session["Tahun"].ToString();
                    string bln2 = Session["Bulan"].ToString();

                    if (bln2 == "1")
                    { string Bulan = "JAN"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "2") { string Bulan = "FEB"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "3") { string Bulan = "MRT"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "4") { string Bulan = "APR"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "5") { string Bulan = "MEI"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "6") { string Bulan = "JUN"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "7") { string Bulan = "JUL"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "8") { string Bulan = "AGST"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "9") { string Bulan = "SEPT"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "10") { string Bulan = "OKT"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "11") { string Bulan = "NOV"; Session["Bulan11"] = Bulan; }
                    else if (bln2 == "12") { string Bulan = "DES"; Session["Bulan11"] = Bulan; }
                    string NamaBulan = Session["Bulan11"].ToString();




                    MailMessage mail = new MailMessage();
                    SmtpClient Smtp = new SmtpClient();
                    mail.From = new MailAddress("log.bb.admin_ctrp@grcboard.com");
                    mail.To.Add(new MailAddress("acc.spv_ctrp@grcboard.com", "Harlina"));
                    mail.To.Add(new MailAddress("fin_ctrp@grcboard.com", "Herlan"));
                    mail.CC.Add(new MailAddress("devian@grcboard.com", "Devian"));
                    mail.CC.Add(new MailAddress("log.bb_ctrp@grcboard.com", "Ervan"));
                    mail.CC.Add(new MailAddress("iso_ctrp@grcboard.com", "Ceicilia"));


                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                    mail.Subject = "Laporan Bulanan" + " " + GroupInventory + "Periode :" + "" + NamaBulan + "-" + thn2;
                    mail.Body += "Dengan Hormat, \n\r\n\r\n\r";
                    mail.Body += "Dengan ini kami kirimkan Laporan Bulanan Inventory : " + GroupInventory + " " + NamaBulan + " - " + thn2 + " \n\r\n\r\n\r";
                    mail.Body += "Terimakasih, " + "\n\r";
                    mail.Body += "Salam GRCBOARD " + "\n\r";
                    mail.Body += "Regard's, " + "\n\r";
                    mail.Body += ((Users)Session["Users"]).UserName + "\n\r";

                    Smtp.Host = "mail.grcboard.com";
                    Smtp.Port = 587;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "log.bb.admin_ctrp@grcboard.com";
                    NetworkCred.Password = "grc123!@#";


                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"D:\Laporan\" + NamaFile);
                    Smtp.EnableSsl = true;
                    Smtp.UseDefaultCredentials = false;
                    Smtp.Credentials = NetworkCred;
                    Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mail.Attachments.Add(attachment);

                    try
                    { Smtp.Send(mail); }

                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;
                    }

                    Users users = (Users)Session["Users"];
                    LaporanBulanan LapBul = new LaporanBulanan();
                    LaporanBulananFacade LapBulFacade = new LaporanBulananFacade();
                    int intResult = 0;

                    LapBul.LastModifiedBy = users.UserName;
                    LapBul.ID = ID;

                    intResult = LapBulFacade.UpdateLapbulEmail(LapBul);
                }

                i = i + 1;
            }

            LoadDataEmail();

        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            LaporanBulanan grp = (LaporanBulanan)e.Item.DataItem;
            System.Web.UI.WebControls.Image view = (System.Web.UI.WebControls.Image)e.Item.FindControl("view");
            view.Attributes.Add("onclick", "PreviewPDF('" + grp.ID.ToString() + "')");
        }

        //Tombol Release buat user admin : Jika laporan sudah OK, user harus melakukan klik tombol release
        protected void btnRelease_ServerClick(object sender, EventArgs e)
        {
            int Flag1 = Convert.ToInt32(Session["Flag1"]);

            if (Flag1 == 5)
            {
                int TahunA = Convert.ToInt32(ddlTahun.SelectedValue); Session["TahunA1"] = TahunA;
                int BulanA = Convert.ToInt32(ddlBulan.SelectedValue); Session["BulanA1"] = BulanA;
            }
            else
            {
                int TahunA = int.Parse(Session["Tahun"].ToString()); Session["TahunA1"] = TahunA;
                int BulanA = int.Parse(Session["Bulan"].ToString()); Session["BulanA1"] = BulanA;
            }

            int Tahun = Convert.ToInt32(Session["TahunA1"]);
            int Bulan = Convert.ToInt32(Session["BulanA1"]);

            Users users = (Users)Session["Users"];
            int GroupID2 = Convert.ToInt32(Session["GroupID2"]);
            int GroupID = Convert.ToInt32(Session["IDLap"]);
            ArrayList arrGroup = (ArrayList)Session["ListOfGroup"];
            int i = 0;

            foreach (LaporanBulanan ListGroup in arrGroup)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");
                if (cb.Checked)
                {
                    ListGroup.ID = int.Parse(GridView1.Rows[i].Cells[0].Text);
                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    int intResult = 0;

                    Group1.GroupID = ListGroup.GroupID;
                    Group1.Tahun = Tahun;
                    Group1.Bulan = Bulan;
                    Group1.Users = users.UserName;

                    intResult = GroupF.InsertLapBul(Group1);

                    int IDLap = intResult;
                    Session["IDLap"] = IDLap;

                    LoadCetak();
                }

                i = i + 1;
            }

            if (Flag1 == 5)
            {
                LoadLapbul(GroupID2, Flag1);
            }
            else
            {
                LoadLapbul(GroupID2, 0);
            }
        }

        //protected void btnPrintOut_ServerClick(object sender, EventArgs e)
        //{ }

        protected void btnL_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("LapBulAll.aspx");
        }

        protected void btnR_ServerClick(object sender, EventArgs e)
        {

            PanelFormHeader.Visible = true;
            PanelFormRelease.Visible = true;
            PanelFormApvMgrLog.Visible = false;
            PanelFormSentEmail.Visible = false;
            //PanelFormViewLapBul.Visible = false;

            btnCetak.Enabled = true;
            //btnR.Enabled = false;
            //btnLihat.Enabled = true;
            btnEmail.Visible = true;

        }

        protected void btnCetak_ServerClick(object sender, EventArgs e)
        {
            LoadCetak();
        }
        protected void LoadCetak()
        //protected void btnCetak_ServerClick(object sender, EventArgs e)
        {
            //int GroupID2 = Convert.ToInt32(Session["GroupID2"]);
            //int GroupID = Convert.ToInt32(Session["IDLap"]);

            int Flag1 = Convert.ToInt32(Session["Flag1"]);

            string GroupInvventory1 = Session["GroupInventory1"].ToString();
            int IDLap = Convert.ToInt32(Session["IDLap"]);

            if (Flag1 == 5)
            {
                int Tahun1 = Convert.ToInt32(ddlTahun.SelectedValue); Session["TahunA1"] = Tahun1;
                int Bulan1 = Convert.ToInt32(ddlBulan.SelectedValue); Session["BulanA1"] = Bulan1;
            }
            else
            {
                int Tahun1 = int.Parse(Session["Tahun"].ToString()); Session["TahunA1"] = Tahun1;
                int Bulan1 = int.Parse(Session["Bulan"].ToString()); Session["BulanA1"] = Bulan1;
            }

            int Tahun = Convert.ToInt32(Session["TahunA1"]);
            int Bulan = Convert.ToInt32(Session["BulanA1"]);

            //int GroupID = int.Parse(IDLap.Text);      
            int GroupID = IDLap;
            LaporanBulanan LP = new LaporanBulanan();
            LaporanBulananFacade LPF = new LaporanBulananFacade();
            //LP = LPF.RetrieveGroupIDPurchn(int.Parse(IDLap.Text));
            LP = LPF.RetrieveGroupIDPurchn(GroupID);
            int GroupIDP = LP.GroupID;
            string GroupPurchn = LP.GroupDescription;

            string periodeAwal = txtFromPostingPeriod.Text;
            string periodeAkhir = txtToPostingPeriod.Text;

            string frmtPrint = "LandScape";

            string strError = string.Empty;
            int thn = Tahun;
            int blnLalu = Bulan;

            int intPilihLapbul = GroupIDP;
            string pilihLapbul = GroupPurchn;


            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            string deptname = (dept.DeptName.Length > 3) ? dept.DeptName.Substring(0, 3).ToUpper() : dept.DeptName.ToString().ToUpper();

            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["lapbul"] = null;
            Session["jenis"] = null;
            Session["price"] = 0;
            Session["FormatCetak"] = frmtPrint;

            if (GroupID == 0)
            {
                DisplayAJAXMessage(this, "Pilih Tipe Laporan");
                return;
            }

            int userID = ((Users)Session["Users"]).ID;
            int groupID = GroupID;

            string ketBlnLalu = string.Empty;
            string PriceBlnLalu = string.Empty;

            if (blnLalu - 1 == 0)
            {
                ketBlnLalu = "DesQty";
                PriceBlnLalu = "DesAvgPrice";
                thn = thn - 1;
            }
            else if (blnLalu - 1 == 1)
            {
                ketBlnLalu = "JanQty";
                PriceBlnLalu = "JanAvgPrice";
            }
            else if (blnLalu - 1 == 2)
            {
                ketBlnLalu = "FebQty"; PriceBlnLalu = "FebAvgPrice";
            }
            else if (blnLalu - 1 == 3)
            {
                ketBlnLalu = "MarQty"; PriceBlnLalu = "MarAvgPrice";
            }
            else if (blnLalu - 1 == 4)
            { ketBlnLalu = "AprQty"; PriceBlnLalu = "AprAvgPrice"; }
            else if (blnLalu - 1 == 5)
            { ketBlnLalu = "MeiQty"; PriceBlnLalu = "MeiAvgPrice"; }
            else if (blnLalu - 1 == 6)
            { ketBlnLalu = "JunQty"; PriceBlnLalu = "JunAvgPrice"; }
            else if (blnLalu - 1 == 7)
            { ketBlnLalu = "JulQty"; PriceBlnLalu = "JulAvgPrice"; }
            else if (blnLalu - 1 == 8)
            { ketBlnLalu = "AguQty"; PriceBlnLalu = "AguAvgPrice"; }
            else if (blnLalu - 1 == 9)
            { ketBlnLalu = "SepQty"; PriceBlnLalu = "SepAvgPrice"; }
            else if (blnLalu - 1 == 10)
            { ketBlnLalu = "OktQty"; PriceBlnLalu = "OktAvgPrice"; }
            else if (blnLalu - 1 == 11)
            { ketBlnLalu = "NovQty"; PriceBlnLalu = "NovAvgPrice"; }


            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;
            string stock = string.Empty;
            stock = " ";


            if (intPilihLapbul == 10 || intPilihLapbul == 14)
            {
                strQuery = reportFacade.ViewLapBul2ForRepackOnly(ketBlnLalu, thn, periodeAwal, periodeAkhir, GroupIDP, stock);
            }
            else if (intPilihLapbul == 3)
            {
                strQuery = reportFacade.ViewLapBul2ForAtkOnly(ketBlnLalu, thn, periodeAwal, periodeAkhir, GroupIDP, stock);
            }
            else
            {
                if (dept.ID == 14 && users.ViewPrice > 2 || dept.ID == 24 && users.ViewPrice > 2)
                {
                    Session["price"] = users.ViewPrice;
                    ReportFacadeAcc reportFacadeacc = new ReportFacadeAcc();
                    #region Prepared Data
                    string bln = Bulan.ToString();
                    string FirstPeriod = string.Empty;
                    string LastPeriod = string.Empty;
                    string blne = bln.ToString();
                    string s = new string('0', (2 - bln.Length));
                    Int32 lastDay = DateTime.DaysInMonth(Convert.ToInt32(thn), Convert.ToInt32(blne));
                    string d = new string('0', (2 - lastDay.ToString().Length));
                    string jnStok = groupID.ToString();


                    FirstPeriod = thn + s + bln + "01";
                    LastPeriod = thn + s + bln + d + lastDay;
                    string Dari = FirstPeriod;
                    //string Tahun = Tahun.ToString();
                    string GroupPurch = jnStok;
                    string GrpID = GroupPurch;
                    string strSQL = string.Empty;
                    string SaldoLaluQty = string.Empty;
                    string SaldoLaluPrice = string.Empty;
                    string periodeTahun = string.Empty;
                    int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
                    string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
                    string periodeBlnThn = Dari.Substring(0, 6).ToString();
                    switch (fldBln)
                    {
                        case 1:
                            SaldoLaluQty = "DesQty";
                            SaldoLaluPrice = "DesAvgPrice";
                            periodeTahun = (Convert.ToInt16(Tahun) - 1).ToString();
                            break;
                        case 2:
                            SaldoLaluQty = "JanQty";
                            SaldoLaluPrice = "JanAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 3:
                            SaldoLaluQty = "FebQty";
                            SaldoLaluPrice = "FebAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 4:
                            SaldoLaluQty = "MarQty";
                            SaldoLaluPrice = "MarAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 5:
                            SaldoLaluQty = "AprQty";
                            SaldoLaluPrice = "AprAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 6:
                            SaldoLaluQty = "MeiQty";
                            SaldoLaluPrice = "MeiAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 7:
                            SaldoLaluQty = "JunQty";
                            SaldoLaluPrice = "JunAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 8:
                            SaldoLaluQty = "JulQty";
                            SaldoLaluPrice = "JulAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 9:
                            SaldoLaluQty = "AguQty";
                            SaldoLaluPrice = "AguAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 10:
                            SaldoLaluQty = "SepQty";
                            SaldoLaluPrice = "SepAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 11:
                            SaldoLaluQty = "OktQty";
                            SaldoLaluPrice = "OktAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                        case 12:
                            SaldoLaluQty = "NovQty";
                            SaldoLaluPrice = "NovAvgPrice";
                            periodeTahun = Tahun.ToString();
                            break;
                    }
                    #endregion
                    string strquery0 = reportFacadeacc.ViewMutasiStockLapbul(FirstPeriod, LastPeriod, jnStok, thn.ToString());
                    strQuery = strquery0 + reportFacade.ViewLapBul2VP(PriceBlnLalu, ketBlnLalu, thn, periodeAwal, periodeAkhir, GroupIDP, stock);
                    //strQuery = reportFacade.ViewLapBul2(ketBlnLalu, thn, periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue), stock);
                }
                else
                {
                    Session["price"] = 0;
                    strQuery = reportFacade.ViewLapBul2(ketBlnLalu, thn, periodeAwal, periodeAkhir, GroupIDP, stock);
                }

            }
            string UnitKerja = Session["UnitKerja1"].ToString();
            Session["UnitKerja"] = UnitKerja;
            Session["Query"] = strQuery;
            string TanggalA = Session["JudulL1"].ToString();
            string TanggalB = Session["JudulL2"].ToString();
            Session["prdawal"] = TanggalA;
            Session["prdakhir"] = TanggalB;
            Session["lapbul"] = pilihLapbul;
            Session["pilihlapbul"] = pilihLapbul;
            Session["jenis"] = null; ;
            Session["stock"] = stock;
            Session["groupid"] = GroupIDP;
            string PeriodeLap = Session["PeriodeLaporan"].ToString();
            Session["bulan1"] = Bulan;
            Session["tahun1"] = Tahun;
            string Elap = "ELapBul";
            Session["Elap"] = Elap;
            Session["asset"] = 0;

            Cetak11(this);
        }

        static public void Cetak11(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapLapBul', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{        
        //    if (e.CommandName == "Add")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = GridView1.Rows[index];
        //        ViewState["GroupID"] = int.Parse(row.Cells[0].Text);            
        //        txtGroup.Text = CekString(row.Cells[3].Text);
        //        IDLap.Text = CekString(row.Cells[0].Text);
        //        //IDGroup.Text = CekString(row.Cells[2].Text);
        //        Session["IDLap"] = IDLap.Text;


        //    }
        //}


        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("check");


                    if (e.Row.Cells[5].Text.Contains("Printed : Next step Kirim Email") ||
                        e.Row.Cells[5].Text.Equals("Release : Next step Request Apv Mgr. Logistik") ||
                        e.Row.Cells[5].Text.Equals("Approved Mgr Log : Next step Request Apv PM"))
                    {
                        string GroupInventory = CekString(e.Row.Cells[3].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        chk.Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        e.Row.Cells[8].Enabled = true;
                    }

                    else if (e.Row.Cells[5].Text.Equals("Open : Next step Release"))
                    {
                        //int Release1 = Convert.ToInt32(Session["Release"]);
                        string GroupInventory = CekString(e.Row.Cells[3].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        chk.Enabled = false;
                        e.Row.Cells[7].Enabled = true;
                        e.Row.Cells[8].Enabled = false;

                    }

                    else if (e.Row.Cells[5].Text.Contains("Email Sent : Finished"))
                    {
                        int Release1 = Convert.ToInt32(Session["Release"]);

                        string GroupInventory = CekString(e.Row.Cells[3].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        chk.Enabled = false;
                        e.Row.Cells[7].Enabled = false;
                        //e.Row.Cells[7].Enabled = false;

                        if (Release1 == 1)
                        {
                            e.Row.Cells[8].Enabled = true;
                        }
                        else
                        {
                            e.Row.Cells[7].Enabled = false;
                        }

                    }
                    else if (e.Row.Cells[5].Text.Contains("Approved Manager Dept. : Next step Do Print"))
                    {
                        string GroupInventory = CekString(e.Row.Cells[3].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        chk.Enabled = false;
                        e.Row.Cells[7].Enabled = true;
                        e.Row.Cells[8].Enabled = true;
                    }
                }
            }
            catch
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                //Session["IDLap"] = IDLap.Text;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                ViewState["GroupID"] = int.Parse(row.Cells[0].Text);
                if (row.Cells[5].Text.Contains("Approved Manager Dept. : Next step Do Print"))
                {
                    Session["IDLap"] = int.Parse(row.Cells[0].Text);
                    btnCetak.Visible = true;
                    btnRelease.Visible = false;
                    bool chk = (row.FindControl("check") as CheckBox).Checked = true;

                }
                else if (row.Cells[5].Text.Contains("Open"))
                {
                    btnCetak.Visible = false;
                    btnRelease.Visible = true;
                    bool chk = (row.FindControl("check") as CheckBox).Checked = true;

                }

            }
            else if (e.CommandName == "Add2")
            {
                int Flag = Convert.ToInt32(Session["Flag"]);
                int GroupP = Convert.ToInt32(Session["GroupID2"]);
                int Flag1 = Convert.ToInt32(Session["Flag1"]);

                Users users = (Users)Session["Users"];
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                ViewState["GroupID"] = int.Parse(row.Cells[0].Text);
                int GroupIDD = int.Parse(row.Cells[0].Text);
                int Gembok = Convert.ToInt32(Session["Release"]);

                LaporanBulanan GroupH = new LaporanBulanan();
                LaporanBulananFacade GroupFH = new LaporanBulananFacade();
                int intResult = 0;
                GroupH.ID = int.Parse(row.Cells[0].Text);
                GroupH.Users = users.UserName;

                intResult = GroupFH.Hapus(GroupH);

                if (intResult > 0)
                {
                    if (Gembok == 1)
                    {
                        LoadLapbul(GroupP, Flag1);
                    }
                    else if (Gembok == 0)
                    {
                        LoadLapbul(GroupP, 0);
                    }
                }

            }
        }


        protected void GridViewApv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("check2");

                    if (e.Row.Cells[3].Text.Contains("Sent") || e.Row.Cells[3].Text.Contains("Approved Mgr Log") || e.Row.Cells[3].Text.Contains("Open") || e.Row.Cells[3].Text.Contains("Approved PM"))
                    {
                        chk.Enabled = false;
                        btnCetak.Enabled = false;
                        PanelFormHeader.Visible = false;
                    }
                    else if (e.Row.Cells[3].Text.Contains("Release"))
                    {
                        string GroupInventory = CekString(e.Row.Cells[2].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        Session["IDLap1"] = CekString(e.Row.Cells[0].Text);
                        chk.Enabled = true; ;
                    }

                }
            }
            catch
            {

            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("check3");

                    if (e.Row.Cells[4].Text.Contains("Printed"))
                    {
                        chk.Enabled = true;
                        PanelFormHeader.Visible = true;
                        //PanelFormViewLapBul.Visible = false;
                        PanelFormSentEmail.Visible = true;
                    }
                    else
                    {
                        chk.Enabled = true;
                    }

                }
            }
            catch
            {

            }
        }

        protected void GridViewApv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("check4");
                    if (e.Row.Cells[3].Text.Contains("Approved Mgr Log"))
                    {
                        chk.Enabled = true;
                        PanelFormHeader.Visible = true;
                        //PanelFormViewLapBul.Visible = false;
                        PanelFormSentEmail.Visible = false;
                        PanelFormApprovalPM.Visible = true;

                        string GroupInventory = CekString(e.Row.Cells[2].Text);
                        Session["GroupInventory1"] = GroupInventory;
                        Session["IDLap"] = CekString(e.Row.Cells[0].Text);
                    }
                    else if (e.Row.Cells[3].Text.Contains("Sent") || e.Row.Cells[3].Text.Contains("Approved PM") || e.Row.Cells[3].Text.Contains("Open") || e.Row.Cells[3].Text.Contains("Release"))
                    {
                        chk.Enabled = false;

                    }
                }
            }
            catch
            {

            }
        }

        protected void lstBA_Command(object sender, RepeaterItemEventArgs e)
        { }

        protected void btnReleaseUlang_ServerClick(object sender, EventArgs e)
        {
            string Ket = "Inventory";
            LaporanBulanan lp1 = new LaporanBulanan();
            LaporanBulananFacade lp2 = new LaporanBulananFacade();
            int Kunci = lp2.RetrieveKunci(Ket);

            // int Release = 1;
            Session["Release"] = Kunci;

            PanelFormRelease.Visible = false;
            PanelFormApvMgrLog.Visible = false;
            PanelFormApprovalPM.Visible = false;
            PanelFormSentEmail.Visible = false;
            btnCetak.Enabled = false;
            btnEmail.Enabled = false;
            PanelPeriode.Visible = false;
            PanelPeriodeBack.Visible = true;
            PanelFormHeader.Visible = false;
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            PanelFormRelease.Visible = true;
            PanelFormHeader.Visible = false;


            int Flag1 = 5;
            Session["Flag1"] = Flag1;
            int GroupP = Convert.ToInt32(Session["GroupID2"]);

            int Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
            int Bulan = Convert.ToInt32(ddlBulan.SelectedValue);

            int days = DateTime.DaysInMonth(Tahun, Bulan);

            if (Bulan > 0)
            {
                if (Bulan == 1) { string BulanL = "Jan"; Session["BulanL"] = BulanL; }
                if (Bulan == 2) { string BulanL = "Feb"; Session["BulanL"] = BulanL; }
                if (Bulan == 3) { string BulanL = "Mrt"; Session["BulanL"] = BulanL; }
                if (Bulan == 4) { string BulanL = "Apr"; Session["BulanL"] = BulanL; }
                if (Bulan == 5) { string BulanL = "Mei"; Session["BulanL"] = BulanL; }
                if (Bulan == 6) { string BulanL = "Jun"; Session["BulanL"] = BulanL; }
                if (Bulan == 7) { string BulanL = "Jul"; Session["BulanL"] = BulanL; }
                if (Bulan == 8) { string BulanL = "Agst"; Session["BulanL"] = BulanL; }
                if (Bulan == 9) { string BulanL = "Sep"; Session["BulanL"] = BulanL; }
                if (Bulan == 10) { string BulanL = "Okt"; Session["BulanL"] = BulanL; }
                if (Bulan == 11) { string BulanL = "Nov"; Session["BulanL"] = BulanL; }
                if (Bulan == 12) { string BulanL = "Des"; Session["BulanL"] = BulanL; }
            }

            if (Bulan < 10)
            {
                string PeriodeAwal = Tahun + "0" + Bulan + "01";
                string PeriodeAkhir = Tahun + "0" + Bulan + days;
                string Periode1 = "01" + "-" + "0" + Bulan + "-" + Tahun;
                string Periode2 = days + "-" + "0" + Bulan + "-" + Tahun;
                Session["PeriodeAwal"] = PeriodeAwal;
                Session["PeriodeAkhir"] = PeriodeAkhir;
                Session["Periode1"] = Periode1;
                Session["Periode2"] = Periode2;
            }
            else
            {
                //revisi periode awal convert to string
                string PeriodeAwal = Tahun.ToString() + Bulan + "01";
                string PeriodeAkhir = Tahun.ToString() + Bulan + days;
                string Periode1 = "01" + "-" + Bulan + "-" + Tahun;
                string Periode2 = days + "-" + Bulan + "-" + Tahun;
                Session["PeriodeAwal"] = PeriodeAwal;
                Session["PeriodeAkhir"] = PeriodeAkhir;
                Session["Periode1"] = Periode1;
                Session["Periode2"] = Periode2;
            }

            string JudulBulan = Session["BulanL"].ToString();
            string JudulL1 = "01" + "-" + JudulBulan + "-" + Tahun;
            string JudulL2 = days + "-" + JudulBulan + "-" + Tahun;
            Session["JudulL1"] = JudulL1;
            Session["JudulL2"] = JudulL2;
            txtFromPostingPeriod.Text = Session["PeriodeAwal"].ToString();
            txtToPostingPeriod.Text = Session["PeriodeAkhir"].ToString();
            string PeriodeLap1 = JudulL1;
            string PeriodeLap2 = JudulL2;
            string PeriodeLaporan = JudulL1 + " " + "s/d" + " " + JudulL2;
            Session["PeriodeLaporan"] = PeriodeLaporan;
            txtLap.Text = PeriodeLaporan;


            LoadLapbul(GroupP, Flag1);
        }

    }
}