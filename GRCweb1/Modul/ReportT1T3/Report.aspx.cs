using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32;
using System.Management;
using Cogs;
using Factory;

namespace GRCweb1.Modul.ReportT1T3
{
    public partial class Report : System.Web.UI.Page
    {
        private ReportDocument hierarchicalGroupingReport;
        private string exportPath;
        private DiskFileDestinationOptions diskFileDestinationOptions;
        private ExportOptions exportOptions;
        private bool selectedNoFormat = false;


        private ReportDocument objRpt1;
        private ReportDocument objRpt2;
        private ReportDocument objRpt3;
        private ReportDocument objRpt4;
        string STRPRINTERNAME;
        private string papername = "SJ";
        protected void Page_Init(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    string subkey = @"Software\Microsoft\Windows NT\CurrentVersion\Windows";
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, false);
                    string defaultPrinter = ((string)key.GetValue("Device")).Split(',')[0];
                    key.Close();

                    string defaultprinter;
                    RegistryKey regKey = Registry.CurrentUser;
                    regKey = regKey.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows");
                    string regvalue = (string)regKey.GetValue("Device");
                    string charDP = "";
                    int TotalChar = regvalue.Length;
                    defaultprinter = "";
                    //for (int i = 0; i <= TotalChar; i++)
                    //{
                    //    charDP = regvalue.Substring(i, 1);
                    //    if (charDP != ",")
                    //    {
                    //        defaultprinter = defaultprinter + charDP;
                    //        charDP = "";
                    //    }
                    //    else break;
                    //}
                    Session["defaultprinter"] = defaultPrinter;
                    //Label1.Text = Session["defaultprinter"].ToString();

                    Users users = (Users)Session["Users"];
                    printersFacade printerfacade = new printersFacade();
                    Printers printer = new Printers();
                    ArrayList arrPrinters = printerfacade.Retrieve(users.UnitKerjaID);


                    string printertext = string.Empty;
                    string printervalue = string.Empty;
                    ddlPrinter.Items.Clear();
                    ddlPrinter.Items.Add(new ListItem("Pilih Printer", defaultPrinter));
                    foreach (Printers printers in arrPrinters)
                    {
                        if (printers.PrinterName.Trim() != "")
                        {
                            if (printers.Location.Trim() == "")
                            {
                                printertext = printers.PrinterName.Trim();
                                printervalue = @printers.PrinterName.Trim();
                            }
                            else
                            {
                                printertext = printers.PrinterName.Trim() + " on " + printers.Location.Trim();
                                printervalue = @"\\" + printers.Location.Trim() + @"\" + printers.PrinterName.Trim();
                            }
                            ddlPrinter.Items.Add(new ListItem(printertext, printervalue));
                        }
                    }
                }
                catch
                { return; }
            }


            if (ViewState["report1"] == null)
            {
                Response.Expires = 0;
                //Button1.Attributes["onclick"] = "window.print();";
                //Response.Cache.SetNoStore();
                //Response.AppendHeader("Pragma", "no-cache");

                string strID = "";
                strID = Request.QueryString["IdReport"].ToString();

                if (strID == "LTransitH")
                {
                    papername = "Legal";
                    ViewLTransit();
                }
                if (strID == "LTransitHPel")
                {
                    papername = "Legal";
                    ViewLTransitPel();
                }
                if (strID == "LSimetris")
                {
                    papername = "Legal";
                    ViewLSimetris();
                }
                if (strID == "LPenyerahan")
                {
                    papername = "Legal";
                    ViewLPenyerahan();
                }
                if (strID == "LASimetris")
                {
                    papername = "Legal";
                    ViewLASimetris();
                }
                if (strID == "LMutasiLokasi")
                {
                    papername = "Legal";
                    ViewLMutasiLokasi();
                }
                if (strID == "LSaldoLokasi")
                {
                    papername = "Legal";
                    ViewLSaldoLokasi();
                }
                if (strID == "LSaldoItemB")
                {
                    papername = "Legal";
                    ViewLSaldoItemB();
                }
                if (strID == "LTransitIn")
                {
                    papername = "Legal";
                    ViewLTransitIn();
                }
                if (strID == "LTransitInPel")
                {
                    papername = "Legal";
                    ViewLTransitInPel();
                }
                if (strID == "LTransitInPelarian")
                {
                    papername = "Legal";
                    ViewLTransitInPelarian();
                }
                if (strID == "LPemantauanT1")
                {
                    papername = "Legal";
                    ViewLPemantauanT1();
                }
                if (strID == "LPemetaanT1")
                {
                    papername = "Legal";
                    ViewLPemetaanT1();
                }
                if (strID == "LT1SaldoPerLokasi")
                {
                    papername = "Legal";
                    ViewLT1SaldoPerLokasi();
                }
                if (strID == "LT1SaldoPerLokasiPeta")
                {
                    papername = "Legal";
                    ViewLT1SaldoPerLokasiPeta();
                }

                if (strID == "LSaldoTransit_T1")
                {
                    papername = "Legal";
                    ViewLSaldoTransit_T1();
                }
                if (strID == "LT1SaldoPerLokasiDet")
                {
                    papername = "Legal";
                    ViewLT1SaldoPerLokasiDet();
                }
                if (strID == "LPemantauanPel")
                {
                    papername = "Legal";
                    ViewLPemantauanPel();
                }
                if (strID == "LKirim")
                {
                    papername = "Legal";
                    ViewLPengiriman();
                }
                if (strID == "LMutasiBJ")
                {
                    papername = "Legal";
                    ViewLMutasiBJ();
                }
                if (strID == "LMutasiBJT1")
                {
                    papername = "Legal";
                    ViewLMutasiBJT1();
                }
                if (strID == "LMutasiBJRekap")
                {
                    papername = "Legal";
                    ViewLMutasiBJRekap();
                }
                if (strID == "LMutasiBJT1Rekap")
                {
                    papername = "Legal";
                    ViewLMutasiBJT1Rekap();
                }
                if (strID == "LMutasiBSRekap")
                {
                    papername = "Legal";
                    ViewLMutasiBSRekap();
                }
                if (strID == "LMutasiBP")
                {
                    papername = "Legal";
                    ViewLMutasiBP();
                }
                if (strID == "LMutasiBPRekap")
                {
                    papername = "Legal";
                    ViewLMutasiBPRekap();
                }
                if (strID == "LMutasiWIP")
                {
                    papername = "Legal";
                    ViewLMutasiWIP();
                }
                if (strID == "LMutasiWIPRekap")
                {
                    papername = "Legal";
                    ViewLMutasiWIPRekap();
                }
                if (strID == "LSaldoPerUkuran")
                {
                    papername = "Legal";
                    ViewLSaldoPerUkuran();
                }
                if (strID == "LKartuStockPerUkuran")
                {
                    papername = "Legal";
                    ViewLKartuStockPerUkuran();
                }
                if (strID == "RekapStockTriwulan")
                {
                    papername = "Legal";
                    ViewRekapStockTriwulan();
                }
                if (strID == "RekapRetur")
                {
                    papername = "a4";
                    ViewRekapRetur();
                }
                if (strID == "PemantauanRetur")
                {
                    papername = "a4";
                    ViewPemantauanRetur();
                }
                if (strID == "LProsesListPlank")
                {
                    papername = "a3";
                    ViewProsesListPlank();
                }
                if (strID == "LNCHandling")
                {
                    papername = "a3";
                    ViewNCHandling();
                }
                if (strID == "LNCSortir")
                {
                    papername = "a3";
                    ViewNCSortir();
                }
                if (strID == "LNCHandlingB")
                {
                    papername = "a3";
                    ViewNCHandlingB();
                }
                if (strID == "LNCSortirB")
                {
                    papername = "a3";
                    ViewNCSortirB();
                }
                if (strID == "LJurnalT3")
                {
                    papername = "a2";
                    ViewJurnalT3();
                }
                if (strID == "ParetoDefect")
                {
                    papername = "a2";
                    ViewParetoDefect();
                }
                if (strID == "LReadyStock")
                {
                    papername = "a2";
                    ViewLReadyStock();
                }
                if (strID == "LPemantauan4mm")
                {
                    papername = "a2";
                    ViewLPemantauan4mm();
                }
                if (strID == "LReject")
                {
                    papername = "a2";
                    ViewLReject();
                }
                if (strID == "LRekapKirimM3")
                {
                    papername = "legal";
                    ViewLRekapKirimM3();
                }
                if (strID == "LapOpnameT3T")
                {
                    papername = "a4";
                    ViewLaporanT3();
                }

                if (strID == "LapOpnameT3B")
                {
                    papername = "a4";
                    ViewLaporanT3opname();
                }
                if (strID == "PenurunanReject")
                {
                    papername = "a4";
                    ViewPenurunanReject();
                }
                if (strID == "smtpelarian")
                {
                    papername = "a4";
                    ViewSmtPelarian();
                }
                if (strID == "RekapDefect")
                {
                    papername = "Legal";
                    ViewRekapDefect();
                }
                if (strID == "RekapDefectJemur")
                {
                    papername = "Legal";
                    ViewRekapDefectGroupJemur();
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }

        }

        public void ViewRekapDefectGroupJemur()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("RptRekapDefect2.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                //SetCurrentValuesForParameterField(objRpt1, Session["bln"].ToString(), "bln");
                //SetCurrentValuesForParameterField(objRpt1, Session["tahun"].ToString(), "tahun");
                //SetCurrentValuesForParameterField(objRpt1, Session["dept"].ToString(), "dept");
            }
            catch
            {

            }
        }
        public void ViewRekapDefect()
        {
            Users users = (Users)Session["Users"];
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();

            if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                objRpt1.Load(this.Server.MapPath("RptRekapDefect.rpt"));
            }
            else if (users.UnitKerjaID == 11)
            {
                objRpt1.Load(this.Server.MapPath("RptRekapDefect_ctrp.rpt"));
            }

            //objRpt1.Load(this.Server.MapPath("RptRekapDefect.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            SetCurrentValuesForParameterField(objRpt1, Session["prd1"].ToString(), "prd1");
            SetCurrentValuesForParameterField(objRpt1, Session["prd2"].ToString(), "prd2");
            SetCurrentValuesForParameterField(objRpt1, Session["dept"].ToString(), "dept");
            SetCurrentValuesForParameterField(objRpt1, Session["proses"].ToString(), "proses");
            string pressing = string.Empty;
            if (Session["proses"].ToString().Trim() == string.Empty)
                pressing = Session["pressing"].ToString();
            else
                pressing = " , " + Session["pressing"].ToString();
            SetCurrentValuesForParameterField(objRpt1, pressing, "pressing");
            //}
            //catch
            //{

            //}
        }
        public void ViewLPemantauanPel()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LPeantauanPel.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }

        public void ViewLTransit()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LTransit.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLTransitPel()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LTransitPel.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLSimetris()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
            sqlCon.Close();
            DataTable dt = new DataTable();

            da.Fill(dt);

            ReportDocument objRpt1 = new ReportDocument();
            if (Session["report"].ToString() == "biasa")
                objRpt1.Load(this.Server.MapPath("LSimetris.rpt"));
            else
                objRpt1.Load(this.Server.MapPath("LSimetrisWithBS.rpt"));

            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            //}
            //catch
            //{

            //}
        }
        public void ViewLPenyerahan()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
            sqlCon.Close();
            DataTable dt = new DataTable();

            da.Fill(dt);

            ReportDocument objRpt1 = new ReportDocument();

            objRpt1.Load(this.Server.MapPath("LPenyerahan.rpt"));

            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            SetCurrentValuesForParameterField(objRpt1, Session["StsApv"].ToString(), "StsApv");
            SetCurrentValuesForParameterField(objRpt1, Session["CreatedBy"].ToString(), "CreatedBy");
            SetCurrentValuesForParameterField(objRpt1, Session["ApproveBy"].ToString(), "ApproveBy");
            SetCurrentValuesForParameterField(objRpt1, Session["AcceptedBy"].ToString(), "AcceptedBy");
            //}
            //catch
            //{

            //}
        }
        public void ViewLASimetris()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LASimetris.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLMutasiLokasi()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LMutasiLokasi.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLSaldoLokasi()
        {
            //string strSQL= SqlDataAdapter(Session["Query"].ToString();
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);
                ReportDocument objRpt1 = new ReportDocument();
                if (Session["bylokasi"].ToString() == "yes")
                    objRpt1.Load(this.Server.MapPath("LSaldoLokasi.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LSaldoLokasi1.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
            }
            catch
            {

            }
        }
        public void ViewLSaldoItemB()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);
                ReportDocument objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LSaldoItemB.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLTransitIn()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LTransitIn.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLTransitInPel()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LTransitInPel.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLTransitInPelarian()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LTransitInPelarian.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLPemantauanT1()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LPemantauanT1.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch (Exception e)
            {

            }
        }
        public void ViewLPemetaanT1()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                ReportDocument objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LPemetaanT1.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLSaldoTransit_T1()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

            //try
            //{
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            sqlCon.Close();
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("LSaldoLokasiTransitT1.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            //    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            //}
            //catch
            //{

            //}
        }
        public void ViewLT1SaldoPerLokasi()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                if (Session["report"].ToString() == "peta")
                    objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiPeta.rpt"));
                else
                {
                    //objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasi.rpt"));
                    objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiP99.rpt"));
                }
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLT1SaldoPerLokasiPeta()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                if (Session["report"].ToString() == "peta")
                {
                    objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiPantau.rpt"));
                    //objRpt2.Load(this.Server.MapPath("LT1SaldoPerLokasiDet.rpt"));
                }
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        //public void ViewLT1SaldoPerLokasiDet()
        //{

        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

        //    //try
        //    //{
        //        sqlCon.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        objRpt2 = new ReportDocument();
        //        SqlDataAdapter da2 = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon); da2.SelectCommand.CommandTimeout = 0;
        //        DataTable dt2 = new DataTable();
        //        da2.Fill(dt2);
        //        objRpt1 = new ReportDocument();
        //        if (Session["report"].ToString() == "peta")
        //            objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiPeta.rpt"));
        //        else
        //        {
        //            objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiMaster.rpt"));
        //            //objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiDet.rpt"));
        //        }
        //        objRpt1.SetDataSource(dt);
        //        objRpt1.Subreports[0].SetDataSource(dt2);
        //        crViewer.ReportSource = objRpt1;
        //        crViewer.DisplayToolbar = true;
        //        SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
        //    //}
        //    //catch
        //    //{

        //    //}
        //}
        public void ViewLT1SaldoPerLokasiDet()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                if (Session["periode"].ToString().Substring(0, 5) == "CURIN")
                {
                    objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiDet.rpt"));
                }
                else
                {
                    if (Session["plant"].ToString() == "KRWG")
                        objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiDetJ.rpt"));
                    else
                        objRpt1.Load(this.Server.MapPath("LT1SaldoPerLokasiDetJctrp.rpt"));
                }
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {
            }
        }

        public void ViewLPengiriman()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LKirim.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void ViewLMutasiBJ()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LMutasiBJ.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["nama"].ToString(), "nama");
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {
                DisplayAJAXMessage(this, "Connection Time Expired");
            }
        }
        public void ViewLMutasiBJT1()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LMutasiBJT1.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["nama"].ToString(), "nama");
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {
                DisplayAJAXMessage(this, "Connection Time Expired");
            }
        }

        public void ViewLMutasiBJRekap()
        {
            // Tambahan
            string ElapBul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                if (ElapBul != string.Empty)
                {
                    if (ElapBul == "Elapbul")
                    {
                        LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                        LaporanBulanan group1 = new LaporanBulanan();
                        group1 = groupsPurchnFacade1.cekStatus1T13(ID);
                        int StatusLap = group1.Status;
                        Session["StatusLap"] = StatusLap;
                        int Plant = users.UnitKerjaID;
                        Session["Plant"] = Plant;

                        LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                        LaporanBulanan group2 = new LaporanBulanan();
                        group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                        Session["PIC"] = group2.PIC;
                        Session["Sign2"] = group2.Sign2;

                    }
                    else if (ElapBul == "No")
                    {
                        int StatusLap = -1;
                        Session["StatusLap"] = StatusLap;
                        int Plant = 0;
                        Session["Plant"] = Plant;

                        string PIC1 = "";
                        Session["PIC1"] = PIC1;

                        string PIC2 = "";
                        Session["PIC2"] = PIC2;

                        string PIC3 = "";
                        Session["PIC3"] = PIC3;

                        Session["PIC"] = 0;
                        Session["Sign2"] = "";

                    }
                }

                if (Session["satuan"].ToString() == "lembar" || Session["satuan"].ToString() == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                    objRpt1.Load(this.Server.MapPath("LMutasiBJRekapNoPrice.rpt"));

                //else if ( Session["satuan"] == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                //    objRpt1.Load(this.Server.MapPath("LMutasiBJRekapNoPrice_sign.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LMutasiBJRekapNoPriceM3.rpt"));

                if (ElapBul == "Elapbul")
                {
                    string StatusLapT13 = Session["StatusLap"].ToString();
                    LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                    LaporanBulanan group2T13 = new LaporanBulanan();
                    group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");

                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");


                    LoadConvertPDFBJ();
                }
                else
                {
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");

                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");
                }
                //if (ElapBul == "Elapbul")
                //{
                //    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                //    LoadConvertPDFBJ();
                //}
                //else
                //{
                //    SetCurrentValuesForParameterField(objRpt1, "-1", "StatusElapBul");
                //}

                //SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                //LoadConvertPDFBJ();

            }
            catch
            {
                //DisplayAJAXMessage(this, "Connection Time Expired");
            }
        }
        public void ViewLMutasiBJT1Rekap()
        {
            // Tambahan
            string ElapBul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            string xLokasi = string.Empty; string managerN = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                if (ElapBul != string.Empty)
                {
                    if (ElapBul == "Elapbul")
                    {
                        LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                        LaporanBulanan group1 = new LaporanBulanan();
                        group1 = groupsPurchnFacade1.cekStatus1T13(ID);
                        int StatusLap = group1.Status;
                        Session["StatusLap"] = StatusLap;
                        int Plant = users.UnitKerjaID;
                        Session["Plant"] = Plant;

                        LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                        LaporanBulanan group2 = new LaporanBulanan();
                        group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                        Session["PIC"] = group2.PIC;
                        Session["Sign2"] = group2.Sign2;

                    }
                    else if (ElapBul == "No")
                    {
                        int StatusLap = -1;
                        Session["StatusLap"] = StatusLap;
                        int Plant = 0;
                        Session["Plant"] = Plant;

                        string PIC1 = "";
                        Session["PIC1"] = PIC1;

                        string PIC2 = "";
                        Session["PIC2"] = PIC2;

                        string PIC3 = "";
                        Session["PIC3"] = PIC3;

                        Session["PIC"] = 0;
                        Session["Sign2"] = "";

                        string manager = "";
                        Session["manager"] = manager;


                    }
                }

                if (Session["satuan"].ToString() == "lembar" || Session["satuan"].ToString() == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                    objRpt1.Load(this.Server.MapPath("LMutasiBJT1RekapNoPrice.rpt"));

                //else if ( Session["satuan"] == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                //    objRpt1.Load(this.Server.MapPath("LMutasiBJRekapNoPrice_sign.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LMutasiBJT1RekapNoPriceM3.rpt"));

                if (ElapBul == "Elapbul")
                {
                    string StatusLapT13 = Session["StatusLap"].ToString();
                    LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                    LaporanBulanan group2T13 = new LaporanBulanan();
                    group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");

                    if (users.UnitKerjaID == 7)
                    {
                        managerN = "Mgr Finishing";
                    }
                    else
                    {
                        managerN = "Manager";
                    }
                    SetCurrentValuesForParameterField(objRpt1, managerN, "manager");

                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");


                    LoadConvertPDFBJ();
                }
                else
                {
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");

                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");

                    SetCurrentValuesForParameterField(objRpt1, managerN, "manager");
                }
                //if (ElapBul == "Elapbul")
                //{
                //    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                //    LoadConvertPDFBJ();
                //}
                //else
                //{
                //    SetCurrentValuesForParameterField(objRpt1, "-1", "StatusElapBul");
                //}

                //SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                //LoadConvertPDFBJ();

            }
            catch
            {
                //DisplayAJAXMessage(this, "Connection Time Expired");
            }
        }
        public void ViewLMutasiBSRekap()
        {
            // Tambahan
            string Elapbul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                if (Elapbul != string.Empty)
                {
                    if (Elapbul == "Elapbul")
                    {
                        LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                        LaporanBulanan group1 = new LaporanBulanan();
                        group1 = groupsPurchnFacade1.cekStatus1T13(ID);

                        int StatusLap = group1.Status;
                        Session["StatusLap"] = StatusLap;

                        int Plant = users.UnitKerjaID;
                        Session["Plant"] = Plant;

                        LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                        LaporanBulanan group2 = new LaporanBulanan();
                        group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                        Session["PIC"] = group2.PIC;
                        Session["Sign2"] = group2.Sign2;


                    }
                    else if (Elapbul == "No")
                    {
                        int StatusLap = -1;
                        Session["StatusLap"] = StatusLap;
                        int Plant = 0;
                        Session["Plant"] = Plant;

                        string PIC1 = "";
                        Session["PIC1"] = PIC1;

                        string PIC2 = "";
                        Session["PIC2"] = PIC2;

                        string PIC3 = "";
                        Session["PIC3"] = PIC3;

                        Session["PIC"] = 0;
                        Session["Sign2"] = "";
                    }
                }

                //// Tambahan
                //string StatusLapT13 = Session["StatusLap"].ToString();
                //// End Tambahan

                if (Session["satuan"].ToString() == "lembar" || Session["satuan"].ToString() == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                    objRpt1.Load(this.Server.MapPath("LMutasiBSRekapNoPrice.rpt"));
                //else if (Session["satuan"] == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                //    objRpt1.Load(this.Server.MapPath("LMutasiBSRekapNoPrice_sign.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LMutasiBSRekapNoPriceM3.rpt"));

                if (Elapbul == "Elapbul")
                {
                    // Tambahan
                    string StatusLapT13 = Session["StatusLap"].ToString();
                    // End Tambahan

                    LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                    LaporanBulanan group2T13 = new LaporanBulanan();
                    group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);

                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");

                    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");


                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");

                    LoadConvertPDFBS();
                }
                else
                {
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");

                    SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");


                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");

                }

                //if (ElapBul == "Elapbul")
                //{
                //    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                //    LoadConvertPDFBS();
                //}
                //else
                //{                
                //    SetCurrentValuesForParameterField(objRpt1, "0", "StatusElapBul");
                //}
                //SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                //LoadConvertPDFBS();
                sqlCon.Close();
            }
            catch
            {
                //DisplayAJAXMessage(this, "Connection Time Expired");
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        public void ViewLMutasiBP()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LMutasiBP.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["nama"].ToString(), "nama");

            }
            catch
            {

            }
        }

        public void ViewLMutasiBPRekap()
        {
            // Tambahan
            string ElapBul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            string xLokasi = string.Empty; string managerN = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            string sql = Session["Query"].ToString();
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                if (ElapBul != string.Empty)
                {
                    if (ElapBul == "Elapbul")
                    {
                        LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                        LaporanBulanan group1 = new LaporanBulanan();
                        group1 = groupsPurchnFacade1.cekStatus1T13(ID);

                        int StatusLap = group1.Status;
                        Session["StatusLap"] = StatusLap;

                        int Plant = users.UnitKerjaID;
                        Session["Plant"] = Plant;

                        LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                        LaporanBulanan group2 = new LaporanBulanan();
                        group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                        Session["PIC"] = group2.PIC;
                        Session["Sign2"] = group2.Sign2;
                        Session["Sign3"] = group2.Sign3;

                    }
                    else if (ElapBul == "No")
                    {
                        int StatusLap = -1;
                        Session["StatusLap"] = StatusLap;
                        int Plant = 0;
                        Session["Plant"] = Plant;

                        string PIC1 = "";
                        Session["PIC1"] = PIC1;

                        string PIC2 = "";
                        Session["PIC2"] = PIC2;

                        string PIC3 = "";
                        Session["PIC3"] = PIC3;

                        string manager = "";
                        Session["manager"] = manager;

                        Session["PIC"] = 0;
                        Session["Sign2"] = "";
                        Session["Sign3"] = "";
                    }
                }

                //// Tambahan
                //string StatusLapT13 = Session["StatusLap"].ToString();
                //// End Tambahan

                if (Session["satuan"].ToString() == "lembar" || Session["satuan"].ToString() == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                    objRpt1.Load(this.Server.MapPath("LMutasiBPRekapNoPrice.rpt"));
                //else if (Session["satuan"] == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                //    objRpt1.Load(this.Server.MapPath("LMutasiBPRekapNoPrice_sign.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LMutasiBPRekapNoPriceM3.rpt"));

                if (ElapBul == "Elapbul")
                {
                    // Tambahan
                    string StatusLapT13 = Session["StatusLap"].ToString();
                    // End Tambahan

                    LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                    LaporanBulanan group2T13 = new LaporanBulanan();
                    group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);

                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, Session["dept"].ToString(), "dept");

                    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign3"].ToString(), "Sign3");

                    if (users.UnitKerjaID == 7)
                    {
                        managerN = "Mgr Finishing";
                    }
                    else
                    {
                        managerN = "Manager";
                    }
                    SetCurrentValuesForParameterField(objRpt1, managerN, "Manager");
                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");

                    LoadConvertPDFBP();

                }
                else
                {
                    objRpt1.SetDataSource(dt);
                    crViewer.ReportSource = objRpt1;
                    crViewer.DisplayToolbar = true;
                    SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                    SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                    SetCurrentValuesForParameterField(objRpt1, Session["dept"].ToString(), "dept");

                    SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");

                    SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                    SetCurrentValuesForParameterField(objRpt1, Session["PIC"].ToString(), "PIC");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");
                    SetCurrentValuesForParameterField(objRpt1, Session["Sign3"].ToString(), "Sign3");

                    SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");

                    SetCurrentValuesForParameterField(objRpt1, managerN, "Manager");
                }

                //if (ElapBul == "Elapbul")
                //{
                //    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                //    LoadConvertPDFBP();
                //}
                //else
                //{
                //    SetCurrentValuesForParameterField(objRpt1, "0", "StatusElapBul");
                //}

                //SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                //LoadConvertPDFBP();
                sqlCon.Close();
            }
            catch
            {

            }
        }

        public void ViewLMutasiWIP()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LMutasiWIP.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["barang"].ToString(), "barang");

            }
            catch
            {

            }
        }

        public void ViewLMutasiWIPRekap()
        {
            // Tambahan
            string ElapBul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            string xLokasi = string.Empty; string ManagerN = string.Empty;
            Users users = (Users)Session["Users"];

            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            string test = Session["yearmonth"].ToString();
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();
            string cek = Session["Query"].ToString();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();

            if (ElapBul != string.Empty)
            {
                if (ElapBul == "Elapbul")
                {
                    LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                    LaporanBulanan group1 = new LaporanBulanan();
                    group1 = groupsPurchnFacade1.cekStatus1T13(ID);
                    int StatusLap = group1.Status;
                    Session["StatusLap"] = StatusLap;
                    int Plant = users.UnitKerjaID;
                    Session["Plant"] = Plant;

                    LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                    LaporanBulanan group2 = new LaporanBulanan();
                    group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                    Session["PIC"] = group2.PIC;
                    Session["Sign2"] = group2.Sign2;

                }
                else if (ElapBul == "No")
                {
                    int StatusLap = -1;
                    Session["StatusLap"] = StatusLap;
                    int Plant = 0;
                    Session["Plant"] = Plant;

                    string PIC1 = "";
                    Session["PIC1"] = PIC1;

                    string PIC2 = "";
                    Session["PIC2"] = PIC2;

                    string PIC3 = "";
                    Session["PIC3"] = PIC3;

                    Session["PIC"] = 0;
                    Session["Sign2"] = "";

                    string manager = "";
                    Session["manager"] = manager;

                }

            }


            //// Tambahan
            //string StatusLapT13 = Session["StatusLap"].ToString();
            //// End Tambahan

            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + users.UnitKerjaID.ToString(), "Report");

            if (Convert.ToInt32(Session["yearmonth"].ToString()) <= Convert.ToInt32(periode))
                objRpt1.Load(this.Server.MapPath("LMutasiWIPRekapNoPrice.rpt"));
            //else if (Session["Elapbul"].ToString() == "Elapbul")
            //    objRpt1.Load(this.Server.MapPath("LMutasiWIPRekapNoPrice_sign.rpt"));
            else
            {
                if (Session["satuan"].ToString() == "lembar" || Session["satuan"].ToString() == "lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                    objRpt1.Load(this.Server.MapPath("LMutasiWIPrekapNoPricep99Listplank.rpt"));
                //else if (Session["satuan"]=="lembar1" && Session["Elapbul"].ToString() == "Elapbul")
                //    objRpt1.Load(this.Server.MapPath("LMutasiWIPrekapNoPricep99Listplank_sign.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LMutasiWIPrekapNoPricep99ListplankM3.rpt"));
            }

            if (ElapBul == "Elapbul")
            {
                // Tambahan
                string StatusLapT13 = Session["StatusLap"].ToString();
                // End Tambahan

                LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                LaporanBulanan group2T13 = new LaporanBulanan();
                group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");

                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");

                if (users.UnitKerjaID == 7)
                {
                    ManagerN = "Mgr Finishing";
                }
                else
                {
                    ManagerN = "Manager";
                }

                SetCurrentValuesForParameterField(objRpt1, ManagerN, "manager");

                LoadConvertPDFWiP();
            }
            else
            {

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");

                SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                SetCurrentValuesForParameterField(objRpt1, Session["Sign2"].ToString(), "Sign2");

                SetCurrentValuesForParameterField(objRpt1, ManagerN, "manager");
            }

            //if (ElapBul == "Elapbul")
            //{
            //    SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
            //    LoadConvertPDFWiP();
            //}
            //else
            //{
            //    SetCurrentValuesForParameterField(objRpt1, "0", "StatusElapBul");
            //}
            sqlCon.Close();
        }

        public void ViewLSaldoPerUkuran()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LSaldoPerUkuran.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["saldoawalbp"].ToString(), "saldoawalbp");
                SetCurrentValuesForParameterField(objRpt1, Session["saldoawalbpkubik"].ToString(), "saldoawalbpkubik");
                SetCurrentValuesForParameterField(objRpt1, Session["saldobp"].ToString(), "saldobp");
                SetCurrentValuesForParameterField(objRpt1, Session["saldobpkubik"].ToString(), "saldobpkubik");
                SetCurrentValuesForParameterField(objRpt1, Session["saldoawalok"].ToString(), "saldoawalok");
                SetCurrentValuesForParameterField(objRpt1, Session["saldoawalokkubik"].ToString(), "saldoawalokkubik");
                SetCurrentValuesForParameterField(objRpt1, Session["saldook"].ToString(), "saldook");
                SetCurrentValuesForParameterField(objRpt1, Session["saldookkubik"].ToString(), "saldookkubik");
                SetCurrentValuesForParameterField(objRpt1, Session["tglawal"].ToString(), "tglawal");
                SetCurrentValuesForParameterField(objRpt1, Session["tglakhir"].ToString(), "tglakhir");
                //SetCurrentValuesForParameterField(objRpt1, Session["partno"].ToString(), "partno");

            }
            catch
            {

            }
        }

        public void ViewLKartuStockPerUkuran()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("KSByUkuran.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["tglawal"].ToString(), "tglawal");
                SetCurrentValuesForParameterField(objRpt1, Session["tglakhir"].ToString(), "tglakhir");
                SetCurrentValuesForParameterField(objRpt1, Session["kwalitas"].ToString(), "kwalitas");
            }
            catch
            {

            }
        }

        public void ViewRekapStockTriwulan()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("RekapStockTriwulan.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["Bulan1"].ToString(), "Bulan1");
                SetCurrentValuesForParameterField(objRpt1, Session["Bulan2"].ToString(), "Bulan2");
                SetCurrentValuesForParameterField(objRpt1, Session["Bulan3"].ToString(), "Bulan3");
                SetCurrentValuesForParameterField(objRpt1, Session["plant"].ToString(), "plant");
            }
            catch
            {

            }
        }

        private void SetCurrentValuesForParameterField(ReportDocument reportDocument, string value, string PARAMETER_FIELD_NAME)
        {
            try
            {
                ParameterValues currentParameterValues = new ParameterValues();
                ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
                parameterDiscreteValue.Value = value;
                currentParameterValues.Add(parameterDiscreteValue);
                ParameterFieldDefinitions parameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields;
                ParameterFieldDefinition parameterFieldDefinition = parameterFieldDefinitions[PARAMETER_FIELD_NAME];
                parameterFieldDefinition.ApplyCurrentValues(currentParameterValues);
            }
            catch
            {
            }
        }

        private void SetPrintOptions(string paper)
        {
            try
            {
                PrintOptions printOptions = objRpt1.PrintOptions;
                STRPRINTERNAME = ddlPrinter.SelectedValue;
                Users users = (Users)Session["Users"];
                printersFacade printerfacade = new printersFacade();
                Printers printer = new Printers();
                int rawKind = printerfacade.RetrievebyKind(STRPRINTERNAME, paper);
                printOptions.PrinterName = STRPRINTERNAME;
                if (paper == "a4")
                {
                    objRpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                    CrystalDecisions.Shared.PageMargins pMargin = new CrystalDecisions.Shared.PageMargins(0, 0, 0, 0);
                    objRpt1.PrintOptions.ApplyPageMargins(pMargin);
                }
                else
                {
                    if (printOptions.PrinterName != null)
                    {
                        System.Drawing.Printing.PrintDocument doctoprint = new System.Drawing.Printing.PrintDocument();
                        doctoprint.PrinterSettings.PrinterName = printOptions.PrinterName;
                        if (rawKind == 0)
                        {
                            for (int i = 0; i <= doctoprint.PrinterSettings.PaperSizes.Count - 1; i++)
                            {
                                if (doctoprint.PrinterSettings.PaperSizes[i].PaperName.ToUpper() == paper)
                                {
                                    rawKind = Convert.ToInt32(doctoprint.PrinterSettings.PaperSizes[i].GetType().GetField("kind",
                                    System.Reflection.BindingFlags.Instance |
                                    System.Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes[i]));
                                    break;
                                }
                            }
                            printerfacade = new printersFacade();
                            int result = printerfacade.Insertraw(STRPRINTERNAME, papername, rawKind);
                        }
                        objRpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
                        CrystalDecisions.Shared.PageMargins pMargin = new CrystalDecisions.Shared.PageMargins(0, 0, 0, 0);
                        objRpt1.PrintOptions.ApplyPageMargins(pMargin);
                    }
                }
            }
            catch
            {
            }
        }
        public void ViewRekapRetur()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("RekapRetur.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTanggal");
                SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTanggal");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewPemantauanRetur()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];
                if (users.UnitKerjaID == 13)
                {
                    objRpt1.Load(this.Server.MapPath("PemantauanReturJmbng.rpt"));
                }
                else
                {
                    objRpt1.Load(this.Server.MapPath("PemantauanRetur.rpt"));
                }
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["plant"].ToString(), "plant");
                SetCurrentValuesForParameterField(objRpt1, Session["totalkirim"].ToString(), "totalkirim");
                SetCurrentValuesForParameterField(objRpt1, Session["totalkiriml"].ToString(), "totalkiriml");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }

        public void ViewProsesListPlank()
        {
            // Tambahan
            string ElapBul = Session["Elapbul"].ToString();
            int ID = Convert.ToInt32(Session["IDLap1"]);
            // End Tambahan

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty; string ManagerN = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            //try
            //{

            //sqlCon.Open();
            //SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            //da.SelectCommand.CommandTimeout = 0;
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //crViewer.DisplayToolbar = true;
            //objRpt1 = new ReportDocument();
            //objRpt1.Load(this.Server.MapPath("LProsesLisPlank.rpt"));
            //objRpt1.SetDataSource(dt);
            //crViewer.ReportSource = objRpt1;

            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt2 = new ReportDocument();

            SqlDataAdapter da2 = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon); da2.SelectCommand.CommandTimeout = 0;
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            objRpt3 = new ReportDocument();
            SqlDataAdapter da3 = new SqlDataAdapter(Session["Query2"].ToString(), sqlCon); da3.SelectCommand.CommandTimeout = 0;
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            objRpt4 = new ReportDocument();
            SqlDataAdapter da4 = new SqlDataAdapter(Session["Query3"].ToString(), sqlCon); da3.SelectCommand.CommandTimeout = 0;
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);

            objRpt1 = new ReportDocument();


            //Tambahan
            if (ElapBul != string.Empty)
            {
                if (ElapBul == "Elapbul")
                {
                    LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                    LaporanBulanan group1 = new LaporanBulanan();
                    group1 = groupsPurchnFacade1.cekStatus1T13(ID);
                    int StatusLap = group1.Status;
                    Session["StatusLap"] = StatusLap;
                    int Plant = users.UnitKerjaID;
                    Session["Plant"] = Plant;

                    LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                    LaporanBulanan group2 = new LaporanBulanan();
                    group2 = groupsPurchnFacade2.cekPICT13(users.ID);
                    Session["PIC"] = group2.PIC;
                    Session["Sign2"] = group2.Sign2;

                }
                else if (ElapBul == "No")
                {
                    int StatusLap = -1;
                    Session["StatusLap"] = StatusLap;
                    int Plant = 0;
                    Session["Plant"] = Plant;

                    string PIC1 = "";
                    Session["PIC1"] = PIC1;

                    string PIC2 = "";
                    Session["PIC2"] = PIC2;

                    string PIC3 = "";
                    Session["PIC3"] = PIC3;

                    Session["PIC"] = 0;
                    Session["Sign2"] = "";

                    string manager = "";
                    Session["manager"] = manager;

                }

            }
            //End Tambahan
            if (Session["satuan"].ToString().Trim() == "lembar")
                objRpt1.Load(this.Server.MapPath("LProsesLisPlank.rpt"));
            else
                objRpt1.Load(this.Server.MapPath("LProsesLisPlankM3.rpt"));

            if (ElapBul == "Elapbul")
            {
                // Tambahan
                string StatusLapT13 = Session["StatusLap"].ToString();
                // End Tambahan

                LaporanBulananFacade groupsPurchnFacade2T13 = new LaporanBulananFacade();
                LaporanBulanan group2T13 = new LaporanBulanan();
                group2T13 = groupsPurchnFacade2T13.RetrieveTTDT13(ID);

                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                objRpt1.Subreports[1].SetDataSource(dt3);
                objRpt1.Subreports[2].SetDataSource(dt4);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC1, "PIC1");
                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC2, "PIC2");
                SetCurrentValuesForParameterField(objRpt1, group2T13.PIC3, "PIC3");
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");
                SetCurrentValuesForParameterField(objRpt1, StatusLapT13, "StatusElapBul");
                if (users.UnitKerjaID == 7)
                {
                    ManagerN = "Mgr Finishing";
                }
                else
                {
                    ManagerN = "Manager";
                }

                SetCurrentValuesForParameterField(objRpt1, ManagerN, "manager");

                crViewer.DisplayToolbar = true;

                LoadConvertListPlank();
                //LoadConvertPDFWiP();

            }

            else

            {
                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                objRpt1.Subreports[1].SetDataSource(dt3);
                objRpt1.Subreports[2].SetDataSource(dt4);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, Session["StatusLap"].ToString(), "StatusElapBul");
                SetCurrentValuesForParameterField(objRpt1, Session["PIC1"].ToString(), "PIC1");
                SetCurrentValuesForParameterField(objRpt1, Session["PIC2"].ToString(), "PIC2");
                SetCurrentValuesForParameterField(objRpt1, Session["PIC3"].ToString(), "PIC3");
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString(), "Plant");

                SetCurrentValuesForParameterField(objRpt1, ManagerN, "manager");

                crViewer.DisplayToolbar = true;
            }

            //objRpt1.SetDataSource(dt);
            //objRpt1.Subreports[0].SetDataSource(dt2);
            //objRpt1.Subreports[1].SetDataSource(dt3);
            //objRpt1.Subreports[2].SetDataSource(dt4);
            //crViewer.ReportSource = objRpt1;
            //SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            //SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
            //crViewer.DisplayToolbar = true;
            sqlCon.Close();

        }

        public void ViewNCHandling()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            decimal lembar = simF.GetTotalLembarOK(Session["thnbln"].ToString());
            decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("LNCHandling.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, lembar.ToString(), "totalok");
                SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewNCSortir()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            decimal lembar = simF.GetTotalLembarOK(Session["thnbln"].ToString());
            decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("LNCSortir.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, lembar.ToString(), "totalok");
                SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }

        public void ViewNCHandlingB()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            decimal lembar = simF.GetTotalLembarOK(Session["thnbln"].ToString());
            decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("LNCHandlingB.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, lembar.ToString(), "totalok");
                SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewNCSortirB()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            T3_SimetrisFacade simF = new T3_SimetrisFacade();
            decimal lembar = simF.GetTotalLembarOK(Session["thnbln"].ToString());
            decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("LNCSortirB.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                SetCurrentValuesForParameterField(objRpt1, lembar.ToString(), "totalok");
                SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewJurnalT3()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            //T3_SimetrisFacade simF = new T3_SimetrisFacade();
            //decimal lembar = simF.GetTotalLembarOK(Session["thnbln"].ToString());
            //decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
            }
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("LJurnalT3.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["jurnal"].ToString(), "jurnal");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
                //SetCurrentValuesForParameterField(objRpt1, lembar.ToString(), "totalok");
                //SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewParetoDefect()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;

                objRpt1.Load(this.Server.MapPath("ParetoDefect.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                //SetCurrentValuesForParameterField(objRpt1, kubik.ToString(), "totalkubikok");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewLReadyStock()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                objRpt1.Load(this.Server.MapPath("LReadyStock1.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, company.Lokasi.Trim().ToUpper(), "plant");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewLPemantauan4mm()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                objRpt1.Load(this.Server.MapPath("Pemantauan4mm.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                //SetCurrentValuesForParameterField(objRpt1, company.Lokasi.Trim().ToUpper(), "plant");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewLReject()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                sqlCon.Close();
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                if (Session["dept"].ToString() == "Logistik")
                {
                    if (Session["jenis"].ToString() == "Bulanan")
                        objRpt1.Load(this.Server.MapPath("LReject.rpt"));
                    else
                        objRpt1.Load(this.Server.MapPath("LRejectLH.rpt"));
                }
                else
                {
                    if (Session["jenis"].ToString() == "Bulanan")
                        objRpt1.Load(this.Server.MapPath("LRejectF.rpt"));
                    else
                        objRpt1.Load(this.Server.MapPath("LRejectFH.rpt"));
                }
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
        }
        public void ViewLRekapKirimM3()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            string xLokasi = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            //try
            //{

            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            sqlCon.Close();
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();
            crViewer.DisplayToolbar = true;
            objRpt1.Load(this.Server.MapPath("LRekapKirimM3.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            //}
            //catch (Exception e)
            //{
            //    Response.Write(e.Message);
            //}

            //setup
            exportPath = "C:\\rekappengiriman\\";

            if (!System.IO.Directory.Exists(exportPath))
            {
                System.IO.Directory.CreateDirectory(exportPath);
            }
            hierarchicalGroupingReport = objRpt1;
            diskFileDestinationOptions = new DiskFileDestinationOptions();
            exportOptions = hierarchicalGroupingReport.ExportOptions;
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOptions.FormatOptions = null;
            //configure selection
            exportOptions.ExportFormatType = ExportFormatType.Excel;
            diskFileDestinationOptions.DiskFileName = exportPath + "Pengiriman2plant" + DateTime.Now.ToString("ddMMMyyyy") + ".xls";
            exportOptions.DestinationOptions = diskFileDestinationOptions;

            hierarchicalGroupingReport.Export();

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            SetPrintOptions(papername);
            objRpt1.PrintToPrinter(1, false, 1, 99);
            CloseWindow(this);

        }
        protected void ddlPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetPrintOptions(papername);
        }
        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        public void ViewLaporanT3()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("LapOpnameT3T.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["NamaPlant"].ToString(), "NamaPabrik");
                SetCurrentValuesForParameterField(objRpt1, Session["CutOff"].ToString(), "CutOff");
                SetCurrentValuesForParameterField(objRpt1, Session["tgl"].ToString(), "TglOpname");
                SetCurrentValuesForParameterField(objRpt1, Session["Tahun"].ToString(), "tahun");
            }
            catch
            {

            }
        }


        public void ViewLaporanT3opname()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapOpnameT3B.rpt"));


                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["NamaPlant"].ToString(), "NamaPabrik");
                SetCurrentValuesForParameterField(objRpt1, Session["blnT"].ToString(), "BulanTahun");
                SetCurrentValuesForParameterField(objRpt1, Session["tgl"].ToString(), "TglOpname");
                SetCurrentValuesForParameterField(objRpt1, Session["CutOff"].ToString(), "CutOff");
            }
            catch
            {

            }
        }
        public void ViewPenurunanReject()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                string Target = string.Empty;
                string TargetBP = string.Empty;
                if (Convert.ToInt32(Session["yearmonth"].ToString()) <= 201906)
                    objRpt1.Load(this.Server.MapPath("LPenurunanReject.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LPenurunanReject_A.rpt"));

                if (Session["plant"].ToString().ToUpper() == "CITEUREUP")
                {
                    Target = "550 M3"; Session["Target"] = Target;
                    TargetBP = "100 M3"; Session["TargetBP"] = TargetBP;
                }
                else if (Session["plant"].ToString().ToUpper() == "KARAWANG")
                {
                    Target = "1.000 M3"; Session["Target"] = Target;
                    TargetBP = "100 M3"; Session["TargetBP"] = TargetBP;
                }
                else if (Session["plant"].ToString().ToUpper() == "JOMBANG")
                {
                    Target = "200 M3"; Session["Target"] = Target;
                    TargetBP = "25 M3"; Session["TargetBP"] = TargetBP;
                }

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["plant"].ToString(), "plant");
                SetCurrentValuesForParameterField(objRpt1, Session["Target"].ToString(), "Target");
                SetCurrentValuesForParameterField(objRpt1, Session["TargetBP"].ToString(), "TargetBP");

                SetCurrentValuesForParameterField(objRpt1, Session["Judul"].ToString(), "Judul");
                SetCurrentValuesForParameterField(objRpt1, Session["Info1"].ToString(), "Info1");
            }
            catch
            {

            }
        }
        public void ViewSmtPelarian()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                sqlCon.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("SmtPelarian.rpt"));


                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["line"].ToString(), "line");

            }
            catch
            {

            }
        }

        protected void LoadConvertPDFBJ()
        {
            string LapBul = Session["GroupNamaT13"].ToString();
            string ThnBln = Session["thnbln"].ToString();
            int IDElapBul = Convert.ToInt32(Session["IDLap1"]);

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + LapBul + "_" + ThnBln + ".pdf";

            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan GroupT3 = new LaporanBulanan();
            LaporanBulananFacade GroupFT3 = new LaporanBulananFacade();
            int Sts = GroupFT3.RetrieveFileIDT13(IDElapBul);

            LaporanBulanan GroupT2 = new LaporanBulanan();
            LaporanBulananFacade GroupFT2 = new LaporanBulananFacade();
            GroupT2 = GroupFT2.RetrieveFileIDT13(FileName, IDElapBul);
            int ID = GroupT2.ID;

            if (ID == 0)
            {
                Users users = (Users)Session["Users"];
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDElapBul;
                intResult = GroupF.InsertLapBulFileNameT13(Group1);

                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();

            }
            else
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.LapID = IDElapBul;

                int stsApv = GroupF.cekStatusapvT13(IDElapBul);

                if (stsApv == 3)
                {
                    intResult = GroupF.UpdateDataCetakT13(Group1);

                }


                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();
            }
        }

        protected void LoadConvertPDFBS()
        {
            int IDElapBul = Convert.ToInt32(Session["IDLap1"]);
            string LapBul = Session["GroupNamaT13"].ToString();
            string ThnBln = Session["thnbln"].ToString();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + LapBul + "_" + ThnBln + ".pdf";

            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan GroupT3 = new LaporanBulanan();
            LaporanBulananFacade GroupFT3 = new LaporanBulananFacade();
            int Sts = GroupFT3.RetrieveFileIDT13(IDElapBul);

            LaporanBulanan GroupT2 = new LaporanBulanan();
            LaporanBulananFacade GroupFT2 = new LaporanBulananFacade();
            GroupT2 = GroupFT2.RetrieveFileIDT13(FileName, IDElapBul);
            int ID = GroupT2.ID;

            if (ID == 0)
            {
                Users users = (Users)Session["Users"];
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDElapBul;
                intResult = GroupF.InsertLapBulFileNameT13(Group1);

                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();

            }
            else
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.LapID = IDElapBul;

                int stsApv = GroupF.cekStatusapvT13(IDElapBul);

                if (stsApv == 3)
                {
                    intResult = GroupF.UpdateDataCetakT13(Group1);

                }


                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();
            }
        }

        protected void LoadConvertPDFBP()
        {
            int IDElapBul = Convert.ToInt32(Session["IDLap1"]);
            string LapBul = Session["GroupNamaT13"].ToString();
            string ThnBln = Session["thnbln"].ToString();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + LapBul + "_" + ThnBln + ".pdf";

            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan GroupT3 = new LaporanBulanan();
            LaporanBulananFacade GroupFT3 = new LaporanBulananFacade();
            int Sts = GroupFT3.RetrieveFileIDT13(IDElapBul);

            LaporanBulanan GroupT2 = new LaporanBulanan();
            LaporanBulananFacade GroupFT2 = new LaporanBulananFacade();
            GroupT2 = GroupFT2.RetrieveFileIDT13(FileName, IDElapBul);
            int ID = GroupT2.ID;

            if (ID == 0)
            {
                Users users = (Users)Session["Users"];
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDElapBul;
                intResult = GroupF.InsertLapBulFileNameT13(Group1);

                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();

            }
            else
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.LapID = IDElapBul;

                int stsApv = GroupF.cekStatusapvT13(IDElapBul);

                if (stsApv == 3)
                {
                    intResult = GroupF.UpdateDataCetakT13(Group1);

                }


                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();
            }
        }

        protected void LoadConvertPDFWiP()
        {
            int IDElapBul = Convert.ToInt32(Session["IDLap1"]);
            string LapBul = Session["GroupNamaT13"].ToString();
            string ThnBln = Session["thnbln"].ToString();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + LapBul + "_" + ThnBln + ".pdf";

            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan GroupT3 = new LaporanBulanan();
            LaporanBulananFacade GroupFT3 = new LaporanBulananFacade();
            int Sts = GroupFT3.RetrieveFileIDT13(IDElapBul);

            LaporanBulanan GroupT2 = new LaporanBulanan();
            LaporanBulananFacade GroupFT2 = new LaporanBulananFacade();
            GroupT2 = GroupFT2.RetrieveFileIDT13(FileName, IDElapBul);
            int ID = GroupT2.ID;

            if (ID == 0)
            {
                Users users = (Users)Session["Users"];
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDElapBul;
                intResult = GroupF.InsertLapBulFileNameT13(Group1);

                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();

            }
            else
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.LapID = IDElapBul;

                int stsApv = GroupF.cekStatusapvT13(IDElapBul);

                if (stsApv == 3)
                {
                    intResult = GroupF.UpdateDataCetakT13(Group1);

                }


                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();
            }
        }

        protected void LoadConvertListPlank()
        {
            int IDElapBul = Convert.ToInt32(Session["IDLap1"]);
            string LapBul = Session["GroupNamaT13"].ToString();
            string ThnBln = Session["thnbln"].ToString();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + LapBul + "_" + ThnBln + ".pdf";

            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan GroupT3 = new LaporanBulanan();
            LaporanBulananFacade GroupFT3 = new LaporanBulananFacade();
            int Sts = GroupFT3.RetrieveFileIDT13(IDElapBul);

            LaporanBulanan GroupT2 = new LaporanBulanan();
            LaporanBulananFacade GroupFT2 = new LaporanBulananFacade();
            GroupT2 = GroupFT2.RetrieveFileIDT13(FileName, IDElapBul);
            int ID = GroupT2.ID;

            if (ID == 0)
            {
                Users users = (Users)Session["Users"];
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDElapBul;
                intResult = GroupF.InsertLapBulFileNameT13(Group1);

                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();

            }
            else
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.LapID = IDElapBul;

                int stsApv = GroupF.cekStatusapvT13(IDElapBul);

                if (stsApv == 3)
                {
                    intResult = GroupF.UpdateDataCetakT13(Group1);

                }


                CrExportOptions = objRpt1.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                objRpt1.Export();
            }
        }
    }
}