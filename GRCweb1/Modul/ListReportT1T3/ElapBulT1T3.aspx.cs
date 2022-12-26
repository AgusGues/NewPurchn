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

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class ElapBulT1T3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                int Tahun0 = int.Parse(DateTime.Now.Year.ToString());
                int Bulan0 = int.Parse(DateTime.Now.Month.ToString());

                if (Bulan0 == 1)
                {
                    int Tahun = Tahun0 - 1;
                    int Bulan = 12;
                    Session["Tahun"] = Tahun;
                    Session["Bulan"] = Bulan;
                }
                else
                {
                    int Tahun = Tahun0;
                    int Bulan = Bulan0 - 1;
                    Session["Tahun"] = Tahun;
                    Session["Bulan"] = Bulan;
                }

                string Bulan1 = Session["Bulan"].ToString();
                string Tahun1 = Session["Tahun"].ToString();
                txtTahun.Text = Tahun1;

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

                Users users = (Users)Session["Users"];
                LaporanBulanan LPH = new LaporanBulanan();
                LaporanBulananFacade LPHF = new LaporanBulananFacade();
                LPH = LPHF.cekUserT13(users.ID);
                Session["Flag"] = LPH.Flag;
                Session["GroupID2"] = LPH.GroupID;

                string Ket = "Production";
                LaporanBulanan lp1 = new LaporanBulanan();
                LaporanBulananFacade lp2 = new LaporanBulananFacade();
                int Kunci = lp2.RetrieveKunci(Ket);

                if (Kunci == 0)
                { btnReleaseUlang.Enabled = false; }
                else if (Kunci == 1)
                { btnReleaseUlang.Enabled = true; }


                string UnitKerja1 = users.UnitKerjaID.ToString();
                Session["UnitKerja1"] = UnitKerja1;

                int TahunJDL = int.Parse(Session["Tahun"].ToString());
                int BulanJDL = int.Parse(Session["Bulan"].ToString());

                if (LPH.Flag == 1)
                {
                    PanelFormSentEmail.Visible = false;
                    PanelFormRelease.Visible = true;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormApprovalPM.Visible = false;
                    LoadLapbul(users.DeptID);
                }
                else if (LPH.Flag == 3) // Apv Manager Dept
                {
                    PanelFormSentEmail.Visible = false;
                    PanelFormRelease.Visible = false;
                    PanelFormApvMgrLog.Visible = true;
                    PanelFormApprovalPM.Visible = false;
                    LoadLapbul_Apv();
                }
                else if (LPH.Flag == 4) // Aproval PM
                {
                    PanelFormSentEmail.Visible = false;
                    PanelFormRelease.Visible = false;
                    PanelFormApvMgrLog.Visible = false;
                    PanelFormApprovalPM.Visible = true;
                    LoadLapbul_Apv2();
                }
            }
        }

        protected void LoadLapbul_Apv2()
        {

            Users users = (Users)Session["Users"];
            btnReleaseUlang.Enabled = false;
            btnEmail.Enabled = false;
            PanelPeriode.Visible = false;
            int Tahun = int.Parse(Session["Tahun"].ToString());
            int Bulan = int.Parse(Session["Bulan"].ToString());
            int Flag = int.Parse(Session["Flag"].ToString());

            Session["Tahun1"] = Tahun;
            Session["Bulan1"] = Bulan;

            LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
            LaporanBulanan group = new LaporanBulanan();
            ArrayList arrGroup = new ArrayList();
            arrGroup = groupsPurchnFacade.RetrieveGroupPurchn3T13(Tahun, Bulan);

            GridViewApv2.DataSource = arrGroup;
            GridViewApv2.DataBind();
            Session["ListOfGroup2"] = arrGroup;
            btnEmail.Enabled = false;

            string thn = Session["Tahun"].ToString();
            string bln = Session["Bulan"].ToString();

            LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
            LaporanBulanan group1 = new LaporanBulanan();
            int DeptID = groupsPurchnFacade1.retrieveDeptID(users.ID);

            LaporanBulanan Group1 = new LaporanBulanan();
            LaporanBulananFacade GroupF = new LaporanBulananFacade();
            ArrayList arrListFile = GroupF.cekFileAllT13(thn, bln, Flag, DeptID, users.Apv);

            lstBA1.DataSource = arrListFile;
            lstBA1.DataBind();
        }

        protected void LoadLapbul_Apv()
        {

            int Flag = int.Parse(Session["Flag"].ToString());
            btnReleaseUlang.Enabled = false;
            btnEmail.Enabled = false;
            PanelPeriode.Visible = false;
            Users users = (Users)Session["Users"];
            int Tahun = int.Parse(Session["Tahun"].ToString());
            int Bulan = int.Parse(Session["Bulan"].ToString());
            int Flag1 = int.Parse(Session["Flag"].ToString());
            Session["Tahun1"] = Tahun;
            Session["Bulan1"] = Bulan;

            LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
            LaporanBulanan group1 = new LaporanBulanan();
            int DeptID = groupsPurchnFacade1.retrieveDeptID(users.ID);

            LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
            LaporanBulanan group = new LaporanBulanan();
            ArrayList arrGroup = new ArrayList();
            arrGroup = groupsPurchnFacade.RetrieveGroupPurchn2T13(Tahun, Bulan, Flag1, DeptID);

            GridViewApv1.DataSource = arrGroup;
            GridViewApv1.DataBind();

            Session["ListOfGroup2"] = arrGroup;

            string thn = Session["Tahun"].ToString();
            string bln = Session["Bulan"].ToString();

            LaporanBulanan Group1 = new LaporanBulanan();
            LaporanBulananFacade GroupF = new LaporanBulananFacade();
            ArrayList arrListFile = GroupF.cekFileAllT13(thn, bln, Flag, DeptID, users.Apv);

            lstBA2.DataSource = arrListFile;
            lstBA2.DataBind();
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
                        //btnCetak.Enabled = false;
                        //PanelFormHeader.Visible = false;
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
                        //PanelFormHeader.Visible = true;
                        //PanelFormViewLapBul.Visible = false;
                        //PanelFormSentEmail.Visible = false;
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

                    intResult = GroupF.UpdateLapBulT13(Group1);

                    //LoadCetak();
                }
                i = i + 1;
            }
            LoadLapbul_Apv();
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

                    intResult = GroupF.UpdateLapBul2T13(Group1);

                    //LoadCetak();

                }
                i = i + 1;
            }

            LoadLapbul_Apv2();

        }

        protected void LoadLapbul(int DeptID)
        {
            if (DeptID > 0)
            {
                string BulanS = txtPeriode.Text.Trim();

                int Flag = int.Parse(Session["Flag"].ToString());

                int Tahun = int.Parse(Session["Tahun"].ToString());
                int Bulan = int.Parse(Session["Bulan"].ToString());

                Session["Tahun1"] = Tahun;
                Session["Bulan1"] = Bulan;
                string UnitKerja = Session["UnitKerja1"].ToString();

                Users users = (Users)Session["Users"];
                LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
                LaporanBulanan group = new LaporanBulanan();
                ArrayList arrGroup = new ArrayList();
                arrGroup = groupsPurchnFacade.RetrieveLapBulT13(BulanS, Tahun, Bulan, users.DeptID);


                GridView1.DataSource = arrGroup;
                GridView1.DataBind();
                Session["ListOfGroup"] = arrGroup;

                string thn = Session["Tahun"].ToString();
                string bln = Session["Bulan"].ToString();

                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                ArrayList arrListFile = GroupF.cekFileT13(thn, bln, Flag, users.DeptID);

                lstBA3.DataSource = arrListFile;
                lstBA3.DataBind();
            }
            else if (DeptID == 0)
            {
                string BulanS = ddlBulan.SelectedItem.ToString();

                int Flag = int.Parse(Session["Flag"].ToString());
                int Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
                int Bulan = Convert.ToInt32(ddlBulan.SelectedValue);
                Session["Tahun1"] = Tahun;
                Session["Bulan1"] = Bulan;
                string UnitKerja = Session["UnitKerja1"].ToString();

                Users users = (Users)Session["Users"];
                LaporanBulananFacade groupsPurchnFacade = new LaporanBulananFacade();
                LaporanBulanan group = new LaporanBulanan();
                ArrayList arrGroup = new ArrayList();
                arrGroup = groupsPurchnFacade.RetrieveLapBulT13(BulanS, Tahun, Bulan, users.DeptID);

                GridView1.DataSource = arrGroup;
                GridView1.DataBind();
                Session["ListOfGroup"] = arrGroup;

                string thn = Session["Tahun1"].ToString();
                string bln = Session["Bulan1"].ToString();

                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                ArrayList arrListFile = GroupF.cekFileT13(thn, bln, Flag, users.DeptID);

                lstBA3.DataSource = arrListFile;
                lstBA3.DataBind();
            }

        }

        protected void btnEmail_ServerClick(object sender, EventArgs e)
        {
            LoadDataEmail();
        }

        protected void btnReleaseUlang_ServerClick(object sender, EventArgs e)
        {
            string Ket = "Production";
            LaporanBulanan lp1 = new LaporanBulanan();
            LaporanBulananFacade lp2 = new LaporanBulananFacade();
            int Kunci = lp2.RetrieveKunci(Ket);
            Session["Release"] = Kunci;

            PanelFormRelease.Visible = false;
            PanelFormApvMgrLog.Visible = false;
            PanelFormApprovalPM.Visible = false;
            PanelFormSentEmail.Visible = false;
            btnCetak.Enabled = false;
            btnEmail.Enabled = true;
            PanelPeriode.Visible = false;
            PanelPeriodeBack.Visible = true;
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            PanelFormRelease.Visible = true;
            Session["Bulan"] = ddlBulan.SelectedValue;
            Session["Tahun"] = ddlTahun.SelectedValue;
            LoadLapbul(0);
        }

        protected void LoadDataEmail()
        {
            //PanelFormHeader.Visible = true;
            PanelFormRelease.Visible = false;
            PanelFormApvMgrLog.Visible = false;
            PanelFormApprovalPM.Visible = false;
            //PanelFormViewLapBul.Visible = false;
            PanelFormSentEmail.Visible = true;
            //btnR.Enabled = true;
            btnCetak.Enabled = false;
            btnEmail.Enabled = true;
            //btnLihat.Enabled = true;

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
            arrEmail = GroupF2.RetrieveEmailLapBulT13(thn1, bln1, FlagIDGroup, UnitKerja, users.ID);

            GridView3.DataSource = arrEmail;
            GridView3.DataBind();
            Session["ListOfEmail"] = arrEmail;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Users users = (Users)Session["Users"];

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                ViewState["GroupID"] = int.Parse(row.Cells[0].Text);

                if (row.Cells[5].Text.Contains("Next step Do Print"))
                {
                    btnCetak.Visible = true;
                    btnRelease.Visible = false;
                    btnCetak.Enabled = true;
                    bool chk = (row.FindControl("check") as CheckBox).Checked = true;

                    int count = 0;
                    foreach (GridViewRow row3 in GridView1.Rows)
                    {
                        CheckBox ch = (CheckBox)row3.FindControl("check");
                        if (ch.Checked == true)
                        {
                            count++;
                            if (count > 1)
                            {
                                bool chk2 = (row.FindControl("check") as CheckBox).Checked = false;

                                DisplayAJAXMessage(this, " Hanya boleh pilih 1 saja ");
                                //return;
                                LoadLapbul(users.DeptID);
                            }
                        }
                    }

                }
                else if (row.Cells[5].Text.Contains("Open"))
                {
                    btnCetak.Visible = false;
                    btnRelease.Visible = true;
                    bool chk = (row.FindControl("check") as CheckBox).Checked = true;

                    int count = 0;
                    foreach (GridViewRow row2 in GridView1.Rows)
                    {
                        CheckBox ch = (CheckBox)row2.FindControl("check");
                        if (ch.Checked == true)
                        {
                            count++;
                            if (count > 1)
                            {
                                bool chk2 = (row.FindControl("check") as CheckBox).Checked = false;

                                DisplayAJAXMessage(this, " Hanya boleh pilih 1 saja ");
                                //return;
                                LoadLapbul(users.DeptID);
                            }
                        }
                    }

                }

            }
            else if (e.CommandName == "Add2")
            {
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

                intResult = GroupFH.HapusT13(GroupH);
                if (intResult > 0)
                {
                    if (Gembok == 1)
                    {
                        LoadLapbul(0);
                    }
                    else if (Gembok == 0)
                    {
                        LoadLapbul(users.DeptID);
                    }
                }

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("check");


                    if (e.Row.Cells[5].Text.Contains("Printed : Next step SENT EMAIL") ||
                        e.Row.Cells[5].Text.Equals("Release : Next step Request Apv Mgr. Logistik") ||
                        e.Row.Cells[5].Text.Equals("Release : Next step Request Apv Mgr. Finishing") ||
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
                    else if (e.Row.Cells[5].Text.Contains("Approved PM : Next step Do Print"))
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

        protected void btnCetak_ServerClick(object sender, EventArgs e)
        {
            int GroupID2 = Convert.ToInt32(Session["GroupID2"]);
            ArrayList arrGroup = (ArrayList)Session["ListOfGroup"];

            int i = 0;
            foreach (LaporanBulanan ListGroup in arrGroup)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");
                if (cb.Checked)
                {
                    ListGroup.ID = int.Parse(GridView1.Rows[i].Cells[0].Text);
                    ListGroup.GroupName = GridView1.Rows[i].Cells[3].Text;
                    string GroupNama = ListGroup.GroupName.Trim();
                    Session["GroupNama"] = GroupNama;
                    Session["IDLap"] = ListGroup.ID;

                    if (GroupNama == "Mutasi BJ")
                    { LoadCetakBJ(); }
                    else if (GroupNama == "Mutasi BP Logistik" || GroupNama == "Mutasi BP Finishing" || GroupNama == "Mutasi BP")
                    { LoadCetakBP(); }
                    else if (GroupNama == "Mutasi BS")
                    { LoadCetakBS(); }
                    else if (GroupNama == "Mutasi BS Auto")
                    { LoadCetakBSAuto(); }
                    else if (GroupNama == "Mutasi WIP")
                    { LoadCetakWIP(); }
                    else if (GroupNama == "ListPlank")
                    { LoadListPlank(); }
                    else if (GroupNama == "Mutasi BJ Tahap 1")
                    { LoadCetakBJT1(); }
                }

                i = i + 1;
            }

            LoadLapbul(GroupID2);

        }

        protected void btnRelease_ServerClick(object sender, EventArgs e)
        {
            int Release = Convert.ToInt32(Session["Release"]);

            Users users = (Users)Session["Users"];
            int GroupID2 = Convert.ToInt32(Session["GroupID2"]);
            int GroupID = Convert.ToInt32(Session["IDLap"]);
            int IDElapBul = Convert.ToInt32(Session["ID"]);

            ArrayList arrGroup = (ArrayList)Session["ListOfGroup"];
            int i = 0;

            foreach (LaporanBulanan ListGroup in arrGroup)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");
                if (cb.Checked)
                {
                    ListGroup.ID = int.Parse(GridView1.Rows[i].Cells[0].Text);
                    ListGroup.GroupName = GridView1.Rows[i].Cells[3].Text;
                    string GroupNama = ListGroup.GroupName.Trim();
                    Session["GroupNama"] = GroupNama;
                    int Bulan = Convert.ToInt32(Session["BulanA1"]);
                    int Bulan0 = int.Parse(DateTime.Now.Month.ToString()) - 1;

                    LaporanBulanan Group1 = new LaporanBulanan();
                    LaporanBulananFacade GroupF = new LaporanBulananFacade();
                    int intResult = 0;

                    if (Release != 1 || Release == 1 && Bulan == Bulan0)
                    {
                        Group1.GroupID = ListGroup.GroupID;
                        Group1.Tahun = int.Parse(Session["Tahun"].ToString());
                        Group1.Bulan = int.Parse(Session["Bulan"].ToString());
                        Group1.Users = users.UserName;
                    }
                    else if (Release == 1 && Bulan != Bulan0)
                    {
                        Group1.GroupID = ListGroup.GroupID;
                        Group1.Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
                        Group1.Bulan = Convert.ToInt32(ddlBulan.SelectedValue);
                        Group1.Users = users.UserName;
                    }

                    intResult = GroupF.InsertLapBulT13(Group1);

                    int IDLap = intResult;
                    Session["IDLap"] = IDLap;

                    if (GroupNama == "Mutasi BJ")
                    { LoadCetakBJ(); }
                    else if (GroupNama == "Mutasi BP Logistik" || GroupNama == "Mutasi BP Finishing" || GroupNama == "Mutasi BP")
                    { LoadCetakBP(); }
                    else if (GroupNama == "Mutasi BS")
                    { LoadCetakBS(); }
                    else if (GroupNama == "Mutasi BS Auto")
                    { LoadCetakBSAuto(); }
                    else if (GroupNama == "Mutasi WIP")
                    { LoadCetakWIP(); }
                    else if (GroupNama == "ListPlank")
                    { LoadListPlank(); }
                    else if (GroupNama == "Mutasi BJ Tahap 1")
                    { LoadCetakBJT1(); }

                }

                i = i + 1;
            }
            if (Release != 1)
            {
                LoadLapbul(GroupID2);
            }
            else if (Release == 1)
            {
                LoadLapbul(0);
            }
        }

        protected void LoadCetakBJT1()
        {
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;
            string Bulan1 = Session["Bulan"].ToString();
            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;

            int Bulan2 = int.Parse(Bulan1);
            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();
            string thnbln = txtTahun.Text.Trim() + Bulanx1;
            string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
            string strHQtyLastMonth = string.Empty;

            #region
            if (Bulan1 == "1")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";

            }
            else if (Bulan1 == "2")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (Bulan1 == "3")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (Bulan1 == "4")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (Bulan1 == "5")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (Bulan1 == "6")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (Bulan1 == "7")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (Bulan1 == "8")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (Bulan1 == "9")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (Bulan1 == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (Bulan1 == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (Bulan1 == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            #endregion


            strQuery = reportFacade.ViewMutasiBJT1DetRekap(thnbln, strQtyLastMonth, strQtyMonth);
            Session["periode"] = txtPeriode.Text + " " + txtTahun.Text;

            Session["Query"] = strQuery;
            Session["Elapbul"] = "Elapbul";
            Session["satuan"] = "lembar1";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP;
            Session["IDLap1"] = Session["IDLap"];

            CetakBJT1(this);

        }

        static public void CetakBJT1(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBJT1Rekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void LoadListPlank()
        {
            int Release = Convert.ToInt32(Session["Release"]);

            if (Release != 1)
            {
                string periodeTahun = Session["Tahun"].ToString();
                int padbulan1 = Convert.ToInt32(Session["Bulan"]);
                Session["periodeTahun"] = periodeTahun; Session["padbulan1"] = padbulan1;
            }
            else if (Release == 1)
            {
                string periodeTahun = ddlTahun.SelectedItem.ToString();
                int padbulan1 = Convert.ToInt32(ddlBulan.SelectedValue);
                Session["periodeTahun"] = periodeTahun; Session["padbulan1"] = padbulan1;
            }

            int padbulan11 = Convert.ToInt32(Session["padbulan1"]);
            string periodeTahun1 = Session["periodeTahun"].ToString();

            if (padbulan11 < 10)
            {
                string padbulan2 = "0" + padbulan11;
                Session["padbulan2"] = padbulan2;
            }
            else if (padbulan11 >= 10)
            {
                string padbulan2 = padbulan11.ToString();
                Session["padbulan2"] = padbulan2;
            }


            string padbulan = Session["padbulan2"].ToString();
            string thnbln1 = periodeTahun1 + padbulan;
            string periodeBulan = txtPeriode.Text;
            string txtBulan = txtPeriode.Text;
            string frmtPrint = string.Empty;

            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string strQuery1 = string.Empty;
            string strQuery2 = string.Empty;
            string strQuery3 = string.Empty;

            if (Release != 1)
            {
                Session["periode"] = txtBulan + " " + periodeTahun1;
            }
            else if (Release == 1)
            {
                Session["periode"] = ddlBulan.SelectedItem + " " + periodeTahun1;
            }

            strQuery = reportFacade.ViewProsesListPlank(periodeTahun1 + padbulan);
            strQuery1 = reportFacade.ViewProsesListPlankR1(periodeTahun1 + padbulan);
            strQuery2 = reportFacade.ViewProsesListPlankR2(periodeTahun1 + padbulan);
            strQuery3 = reportFacade.ViewProsesListPlankR3(periodeTahun1 + padbulan);
            Session["Query"] = strQuery;
            Session["Query1"] = strQuery1;
            Session["Query2"] = strQuery2;
            Session["Query3"] = strQuery3;
            Session["Elapbul"] = "Elapbul";
            Session["IDLap1"] = Session["IDLap"];
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnbln1;
            Session["satuan"] = "lembar";

            CetakListPlank(this);
            Session["yearmonth"] = periodeTahun1 + padbulan;
        }

        static public void CetakListPlank(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LProsesListPlank', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void LoadCetakWIP()
        {
            int Release = Convert.ToInt32(Session["Release"]);

            if (Release != 1)
            {
                string BulanN = Session["Bulan"].ToString();
                Session["BulanN"] = BulanN;
            }
            else if (Release == 1)
            {
                string BulanN = ddlBulan.SelectedValue.ToString();
                Session["BulanN"] = BulanN;
            }


            string Bulan1 = Session["BulanN"].ToString();
            int Bulan2 = int.Parse(Bulan1);
            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();

            //string thnbln = txtTahun.Text.Trim() + Bulanx1;
            //string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();

            if (Release != 1)
            {
                string thnbln = txtTahun.Text.Trim() + Bulanx1;
                string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            else if (Release == 1)
            {
                string thnbln = ddlTahun.SelectedItem + Bulanx1;
                string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }

            string thnbln1 = Session["thnbln"].ToString();
            string thnblnP1 = Session["thnblnP"].ToString();

            string periodeBulan = Bulanx1;
            string periodeTahun = txtTahun.Text.Trim();
            string frmtPrint = string.Empty;
            //string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string noProduksi = string.Empty;

            if (Release != 1)
            {
                Session["periode"] = txtPeriode.Text.Trim() + " " + periodeTahun;
                strQuery = reportFacade.ViewMutasiWIPRekap(periodeBulan, periodeTahun, noProduksi);
                Session["yearmonth"] = periodeTahun + periodeBulan;
                Session["thnbln"] = thnblnP1;
            }
            else if (Release == 1)
            {
                Session["periode"] = ddlBulan.SelectedItem + " " + ddlTahun.SelectedItem;
                strQuery = reportFacade.ViewMutasiWIPRekap(ddlBulan.SelectedValue, ddlTahun.SelectedValue, noProduksi);
                Session["yearmonth"] = ddlTahun.SelectedItem + ddlBulan.SelectedValue.ToString();
                Session["thnbln"] = ddlBulan.SelectedValue.ToString() + "-" + ddlTahun.SelectedValue.ToString();

            }

            Session["Query"] = strQuery;
            Session["Satuan"] = "lembar1";
            Session["Elapbul"] = "Elapbul";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP1;
            Session["IDLap1"] = Session["IDLap"];

            LoadCetakWIP(this);
        }

        static public void LoadCetakWIP(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiWIPRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        protected void LoadCetakBJ()
        {
            int Release = Convert.ToInt32(Session["Release"]);

            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            if (Release != 1)
            {
                string BulanN = Session["Bulan"].ToString();
                Session["BulanN"] = BulanN;
            }
            else if (Release == 1)
            {
                string BulanN = ddlBulan.SelectedValue.ToString();
                Session["BulanN"] = BulanN;
            }

            string Bulan1 = Session["BulanN"].ToString();

            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;



            int Bulan2 = int.Parse(Bulan1);
            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();

            //string thnbln = txtTahun.Text.Trim() + Bulanx1;
            //string thnblnP = Bulanx1 +"-"+ txtTahun.Text.Trim();

            if (Release != 1)
            {
                string thnbln = txtTahun.Text.Trim() + Bulanx1;
                string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            else if (Release == 1)
            {
                string thnbln = ddlTahun.SelectedItem + Bulanx1;
                string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }


            string strHQtyLastMonth = string.Empty;

            #region
            if (Bulan1 == "1" || Bulan1 == "01")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";

            }
            else if (Bulan1 == "2" || Bulan1 == "02")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (Bulan1 == "3" || Bulan1 == "03")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (Bulan1 == "4" || Bulan1 == "04")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (Bulan1 == "5" || Bulan1 == "05")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (Bulan1 == "6" || Bulan1 == "06")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (Bulan1 == "7" || Bulan1 == "07")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (Bulan1 == "8" || Bulan1 == "08")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (Bulan1 == "9" || Bulan1 == "09")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (Bulan1 == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (Bulan1 == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (Bulan1 == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            #endregion

            string thnbln1 = Session["thnbln"].ToString();
            string thnblnP1 = Session["thnblnP"].ToString();

            strQuery = reportFacade.ViewMutasiBJDetRekap(thnbln1, strQtyLastMonth, strQtyMonth);

            //Session["periode"] = txtPeriode.Text + " " + txtTahun.Text;

            if (Release != 1)
            {
                Session["periode"] = txtPeriode.Text + " " + txtTahun.Text;
            }
            else if (Release == 1)
            {
                Session["periode"] = ddlBulan.SelectedItem + " " + ddlTahun.SelectedItem;
            }
            Session["Query"] = strQuery;
            Session["Elapbul"] = "Elapbul";
            Session["satuan"] = "lembar1";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP1;
            Session["IDLap1"] = Session["IDLap"];
            //Session["ID"] = Convert.ToInt32(Session["ID"]);

            CetakBJ(this);

        }

        static public void CetakBJ(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBJRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes') ; ";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);


        }

        protected void LoadCetakBP()
        {
            int Release = Convert.ToInt32(Session["Release"]);

            string strQtyLastMonth = string.Empty;
            string strHQtyLastMonth = string.Empty;
            string strQtyLastMonth0 = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            if (Release != 1)
            {
                string BulanN = Session["Bulan"].ToString();
                Session["BulanN"] = BulanN;
            }
            else if (Release == 1)
            {
                string BulanN = ddlBulan.SelectedValue.ToString();
                Session["BulanN"] = BulanN;
            }

            string Bulan1 = Session["BulanN"].ToString();
            int Bulan2 = int.Parse(Bulan1);

            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();
            //string thnbln = ddlTahun.SelectedItem + Bulanx1;
            //string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;

            if (Release != 1)
            {
                string thnbln = txtTahun.Text.Trim() + Bulanx1;
                string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            else if (Release == 1)
            {
                string thnbln = ddlTahun.SelectedItem + Bulanx1;
                string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }

            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;


            if (Bulan1 == "1" || Bulan1 == "01")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";

            }
            else if (Bulan1 == "2" || Bulan1 == "02")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (Bulan1 == "3" || Bulan1 == "03")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (Bulan1 == "4" || Bulan1 == "04")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (Bulan1 == "5" || Bulan1 == "05")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (Bulan1 == "6" || Bulan1 == "06")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (Bulan1 == "7" || Bulan1 == "07")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (Bulan1 == "8" || Bulan1 == "08")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (Bulan1 == "9" || Bulan1 == "09")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (Bulan1 == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (Bulan1 == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (Bulan1 == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }


            if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                if (users.DeptID == 3)
                {
                    Session["dept9"] = 1;
                    Session["dept8"] = "FINISHING";
                }
                else if (users.DeptID == 6)
                {
                    Session["dept9"] = 2;
                    Session["dept8"] = "LOGISTIK";
                }
                else { Session["dept9"] = 0; }
            }
            else if (users.UnitKerjaID == -1)
            {
                Session["dept8"] = "ALL";
                Session["dept9"] = 0;
            }
            //else { Session["dept9"] = 0;} 

            string thnbln1 = Session["thnbln"].ToString();
            string thnblnP1 = Session["thnblnP"].ToString();

            Session["dept7"] = "DEPARTEMEN " + Session["dept8"].ToString();
            string DeptJ = Session["dept7"].ToString();
            int deptID = Convert.ToInt32(Session["dept9"]);

            strQuery = reportFacade.ViewMutasiBPDetRekap(thnbln1, strQtyLastMonth0, strQtyLastMonth, deptID);

            if (Release != 1)
            {
                Session["periode"] = txtPeriode.Text.Trim() + " " + txtTahun.Text;
            }
            else if (Release == 1)
            {
                Session["periode"] = ddlBulan.SelectedItem + " " + ddlTahun.SelectedValue;
            }


            Session["Query"] = strQuery;
            Session["ELapbul"] = "Elapbul";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP1;
            Session["IDLap1"] = Session["IDLap"];
            Session["satuan"] = "lembar1";

            Session["dept"] = DeptJ;
            Session["userDeptID"] = users.ID;

            CetakBP(this);
        }

        static public void CetakBP(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBPRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void LoadCetakBS()
        {
            #region prepare parameter

            int Release = Convert.ToInt32(Session["Release"]);

            string strQtyLastMonth = string.Empty;
            string strHQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;

            if (Release != 1)
            {
                string BulanN = Session["Bulan"].ToString();
                Session["BulanN"] = BulanN;
            }
            else if (Release == 1)
            {
                string BulanN = ddlBulan.SelectedValue.ToString();
                Session["BulanN"] = BulanN;
            }

            string Bulan1 = Session["BulanN"].ToString();
            int Bulan2 = int.Parse(Bulan1);
            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();
            if (Release != 1)
            {
                string thnbln = txtTahun.Text.Trim() + Bulanx1;
                string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            else if (Release == 1)
            {
                string thnbln = ddlTahun.SelectedItem + Bulanx1;
                string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }


            if (Bulan1 == "1" || Bulan1 == "01")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";

            }
            else if (Bulan1 == "2" || Bulan1 == "02")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (Bulan1 == "3" || Bulan1 == "03")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (Bulan1 == "4" || Bulan1 == "04")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (Bulan1 == "5" || Bulan1 == "05")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (Bulan1 == "6" || Bulan1 == "06")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (Bulan1 == "7" || Bulan1 == "07")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (Bulan1 == "8" || Bulan1 == "08")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (Bulan1 == "9" || Bulan1 == "09")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (Bulan1 == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (Bulan1 == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (Bulan1 == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            #endregion
            string thnbln1 = Session["thnbln"].ToString();
            string thnblnP1 = Session["thnblnP"].ToString();

            if (Convert.ToInt32(thnbln1) >= Convert.ToInt32("202006"))
            {
                strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "Normal", "baru");
            }
            else
            {
                strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "Normal", "lama");
            }

            //strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "Normal");
            if (Release != 1)
            {
                Session["periode"] = "BS Periode : " + txtPeriode.Text.Trim() + " " + txtTahun.Text;
            }
            else if (Release == 1)
            {
                Session["periode"] = "BS Periode : " + ddlBulan.SelectedItem + " " + ddlTahun.SelectedItem;
            }

            Session["Query"] = strQuery;
            //Session["satuan"] = "lembar";
            Session["Elapbul"] = "Elapbul";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP1;
            Session["IDLap1"] = Session["IDLap"];
            Session["satuan"] = "lembar1";
            Cetak1(this);

        }

        static public void Cetak1(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBSRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void LoadCetakBSAuto()
        {
            #region prepare parameter

            int Release = Convert.ToInt32(Session["Release"]);

            string strQtyLastMonth = string.Empty;
            string strHQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            if (Release != 1)
            {
                string BulanN = Session["Bulan"].ToString();
                Session["BulanN"] = BulanN;
            }
            else if (Release == 1)
            {
                string BulanN = ddlBulan.SelectedValue.ToString();
                Session["BulanN"] = BulanN;
            }

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;

            string Bulan1 = Session["BulanN"].ToString();
            int Bulan2 = int.Parse(Bulan1);
            if (Bulan2 < 10)
            {
                string Bulan3 = "0" + Bulan2;
                Session["Bulanx"] = Bulan3;
            }
            else
            {
                string Bulan3 = Bulan2.ToString(); Session["Bulanx"] = Bulan3;
            }

            string Bulanx1 = Session["Bulanx"].ToString();

            if (Release != 1)
            {
                string thnbln = txtTahun.Text.Trim() + Bulanx1;
                string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            else if (Release == 1)
            {
                string thnbln = ddlTahun.SelectedItem + Bulanx1;
                string thnblnP = Bulanx1 + "-" + ddlTahun.SelectedItem;
                Session["thnbln"] = thnbln; Session["thnblnP"] = thnblnP;
            }
            //string thnbln = txtTahun.Text.Trim() + Bulanx1;
            //string thnblnP = Bulanx1 + "-" + txtTahun.Text.Trim();

            if (Bulan1 == "1" || Bulan1 == "01")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";

            }
            else if (Bulan1 == "2" || Bulan1 == "02")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (Bulan1 == "3" || Bulan1 == "03")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (Bulan1 == "4" || Bulan1 == "04")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (Bulan1 == "5" || Bulan1 == "05")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (Bulan1 == "6" || Bulan1 == "06")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (Bulan1 == "7" || Bulan1 == "07")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (Bulan1 == "8" || Bulan1 == "08")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (Bulan1 == "9" || Bulan1 == "09")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (Bulan1 == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (Bulan1 == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (Bulan1 == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            #endregion

            string thnbln1 = Session["thnbln"].ToString();
            string thnblnP1 = Session["thnblnP"].ToString();

            if (Convert.ToInt32(thnbln1) >= Convert.ToInt32("202006"))
            {
                strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "BSAUTO", "baru");
            }
            else
            {
                strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "BSAUTO", "lama");
            }

            //strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln1, strQtyLastMonth, strQtyMonth, "BSAUTO");
            //Session["periode"] = "BS Auto Periode : " + txtPeriode.Text.Trim() + " " + txtTahun.Text;

            if (Release != 1)
            {
                Session["periode"] = "BS Auto Periode : " + txtPeriode.Text.Trim() + " " + txtTahun.Text;
            }
            else if (Release == 1)
            {
                Session["periode"] = "BS Auto Periode : " + ddlBulan.SelectedItem + " " + ddlTahun.SelectedItem;
            }

            //strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "Normal");
            //Session["periode"] = "BS Periode : " + txtPeriode.Text.Trim() + " " + txtTahun.Text;
            Session["Query"] = strQuery;
            //Session["satuan"] = "lembar";
            Session["Elapbul"] = "Elapbul";
            Session["GroupNamaT13"] = Session["GroupNama"];
            Session["thnbln"] = thnblnP1;
            Session["IDLap1"] = Session["IDLap"];
            Session["satuan"] = "lembar1";
            CetakBSAuto(this);

        }

        static public void CetakBSAuto(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBSRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        //{ }

        protected void lstBA_Command(object sender, RepeaterItemEventArgs e)
        { }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            LaporanBulanan grp = (LaporanBulanan)e.Item.DataItem;
            System.Web.UI.WebControls.Image view = (System.Web.UI.WebControls.Image)e.Item.FindControl("view");
            view.Attributes.Add("onclick", "PreviewPDF('" + grp.ID.ToString() + "')");
        }

        protected void btnSent_ServerClick(object sender, EventArgs e)
        {
            string UnitKerja = Session["UnitKerja1"].ToString();
            string thn2 = Session["Tahun"].ToString();
            string bln2 = Session["Bulan"].ToString();

            #region kondisi lama
            /*if (bln2 == "01"){ string Bulan = "JAN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "02") { string Bulan = "FEB"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "03") { string Bulan = "MRT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "04") { string Bulan = "APR"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "05") { string Bulan = "MEI"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "06") { string Bulan = "JUN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "07") { string Bulan = "JUL"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "08") { string Bulan = "AGST"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "09") { string Bulan = "SEPT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "10") { string Bulan = "OKT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "11") { string Bulan = "NOV"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "12") { string Bulan = "DES"; Session["Bulan11"] = Bulan; }*/
            #endregion

            #region kondisi baru 
            if (bln2 == "01" || bln2 == "1") { string Bulan = "JAN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "02" || bln2 == "2") { string Bulan = "FEB"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "03" || bln2 == "3") { string Bulan = "MRT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "04" || bln2 == "4") { string Bulan = "APR"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "05" || bln2 == "5") { string Bulan = "MEI"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "06" || bln2 == "6") { string Bulan = "JUN"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "07" || bln2 == "7") { string Bulan = "JUL"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "08" || bln2 == "8") { string Bulan = "AGST"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "09" || bln2 == "9") { string Bulan = "SEPT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "10") { string Bulan = "OKT"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "11") { string Bulan = "NOV"; Session["Bulan11"] = Bulan; }
            else if (bln2 == "12") { string Bulan = "DES"; Session["Bulan11"] = Bulan; }
            #endregion
            string NamaBulan = Session["Bulan11"].ToString();

            Users users = (Users)Session["Users"];
            LaporanBulanan LPu = new LaporanBulanan();
            LaporanBulananFacade LPuF = new LaporanBulananFacade();
            LPu = LPuF.cekUserIDT13(users.ID);
            int GroupID = LPu.GroupID;
            string UserName = LPu.Noted.Trim();
            string GroupName = LPu.GroupName.Trim();

            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();

            //Pengirim : From
            LaporanBulanan LPu1 = new LaporanBulanan();
            LaporanBulananFacade LPuF1 = new LaporanBulananFacade();
            LPu1 = LPuF1.CekAccountEmailFrom(users.ID);

            mail.From = new MailAddress(LPu1.AccountEmail.Trim(), LPu1.UserName.Trim());

            //Tujuan : To
            LaporanBulanan LPu2 = new LaporanBulanan();
            LaporanBulananFacade LPuF2 = new LaporanBulananFacade();
            ArrayList arrEmaila = new ArrayList();
            arrEmaila = LPuF2.CekAccountEmailTo();

            foreach (LaporanBulanan ListEmaila in arrEmaila)
            {
                mail.To.Add(new MailAddress(ListEmaila.AccountEmail.Trim(), ListEmaila.UserName.Trim()));
            }

            //CC : Cc
            LaporanBulanan LPu3 = new LaporanBulanan();
            LaporanBulananFacade LPuF3 = new LaporanBulananFacade();
            ArrayList arrEmail2 = new ArrayList();
            arrEmail2 = LPuF3.CekAccountEmailCC(users.DeptID);

            foreach (LaporanBulanan ListEmailb in arrEmail2)
            {
                mail.CC.Add(new MailAddress(ListEmailb.AccountEmail.Trim(), ListEmailb.UserName.Trim()));
            }

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mail.Subject = "Laporan Bulanan Tahap I dan III" + " " + GroupName + " " + "Periode :" + " " + NamaBulan + " " + thn2;
            mail.Body += "Dengan Hormat, \n\r\n\r\n\r";
            mail.Body += "Dengan ini kami kirimkan Laporan Bulanan Tahap I dan III untuk Periode : " + " " + NamaBulan + "-" + thn2 + " \n\r\n\r\n\r";
            mail.Body += "Terimakasih, " + "\n\r";
            mail.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
            mail.Body += "Regard's, " + "\n\r";
            mail.Body += UserName + "\n\r";

            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

            //Account Email Admin
            LaporanBulanan LPu4 = new LaporanBulanan();
            LaporanBulananFacade LPuF4 = new LaporanBulananFacade();
            LPu4 = LPuF4.CekAccountEmailAdmin(users.ID);

            NetworkCred.UserName = LPu4.AccountEmail.Trim();
            NetworkCred.Password = LPu4.PassWord.Trim();


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

                intResult = LapBulFacade.UpdateLapbulEmailT13(LapBul);

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
            //PanelFormHeader.Visible = true;
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
            arrEmail = GroupF2.RetrieveEmailLapBulT13(thn1, bln1, FlagIDGroup, UnitKerja, users.ID);

            GridView3.DataSource = arrEmail;
            GridView3.DataBind();
            Session["ListOfEmail"] = arrEmail;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void chk_change(object sender, EventArgs e)
        {

        }

        protected void ChkAll3_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrEmail = (ArrayList)Session["ListOfEmail"];
            int i = 0;

            if (ChkAll3.Checked == true)
            {
                foreach (LaporanBulanan ListGroup in arrEmail)
                {
                    CheckBox chk = (CheckBox)GridView3.Rows[i].FindControl("check3");
                    chk.Checked = true;
                    i = i + 1;
                }

            }
            else
            {
                foreach (LaporanBulanan ListGroup in arrEmail)
                {
                    CheckBox chk = (CheckBox)GridView3.Rows[i].FindControl("check3");
                    chk.Checked = false;
                    i = i + 1;
                }
            }

        }

        protected void ChkAllApv2_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrApv2 = (ArrayList)Session["ListOfGroup2"];
            int i = 0;

            if (ChkAllApv2.Checked == true)
            {
                foreach (LaporanBulanan ListGroup in arrApv2)
                {
                    CheckBox chk = (CheckBox)GridViewApv2.Rows[i].FindControl("check4");
                    chk.Checked = true;
                    i = i + 1;
                }

            }
            else
            {
                foreach (LaporanBulanan ListGroup in arrApv2)
                {
                    CheckBox chk = (CheckBox)GridViewApv2.Rows[i].FindControl("check4");
                    chk.Checked = false;
                    i = i + 1;
                }
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
                        //PanelFormHeader.Visible = true;
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
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}