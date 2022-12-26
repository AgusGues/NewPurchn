using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using DataAccessLayer;
using Domain;
using Factory;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web.Services;
using Factory.DataSets;
using System.Configuration;
using System.Data;
using System.Web.Hosting;
using Microsoft.Win32;

using CrystalDecisions.Shared;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
using System.ComponentModel;
using System.Management;
using System.IO;

namespace GRCweb1.Modul.Report
{
    
    public partial class Report2 : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        private ReportDocument objRpt2;
        string STRPRINTERNAME;
        private string papername;
        //DataTable dt = new DataTable();
        
        protected void Page_Init(object sender, EventArgs e)
        {
            //string Name = Request.QueryString["nopo"];
            if (!Page.IsPostBack)
            {
                #region detect printer
                string subkey = @"Software\Microsoft\Windows NT\CurrentVersion\Windows";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, false);
                string defaultPrinter = ((string)key.GetValue("Device")).Split(',')[0];
                key.Close();

                string defaultprinter;
                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows");
                string regvalue = (string)regKey.GetValue("Device");
                int TotalChar = regvalue.Length;
                defaultprinter = "";
                Session["defaultprinter"] = defaultPrinter;
                try
                {
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
            #endregion
            if (ViewState["report1"] == null)
            {
                Response.Expires = 0;
                string strID = "";
                strID = Request.QueryString["IdReport"].ToString();
                if (strID == "LapRekapKom")
                {
                    papername = "a4";
                    PrintLapRekapKom();

                }
                if (strID == "LoadingTime")
                {
                    ViewLapLoadingTime();
                }
                if (strID == "LapTask")
                {
                    ViewLapTask();
                }
                if (strID == "FormTask")
                {
                    ViewFormTask();
                }

                if (strID == "RekapPakaiDeptItem")
                {
                    papername = "a4";
                    ViewRekapPakaiDeptItem();
                }
                if (strID == "RekapRetur")
                {
                    papername = "a4";
                    ViewRekapRetur();
                }
                if (strID == "SlipMRS")
                {
                    papername = "SJ"; Panel1.Visible = true;//
                    ViewSlipMRS();
                }
                if (strID == "LapOpname")
                {
                    papername = "a4";
                    ViewLapOpname();
                }

                if (strID == "RekapAsset")
                {
                    papername = "legal";
                    ViewRekapAsset();
                }

                if (strID == "RekapReceiptExcel")
                {
                    ViewRekapReceiptExcel();
                }

                if (strID == "SlipRMS")//
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipRMS();
                }

                if (strID == "SlipORS")//
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipORS();
                }

                if (strID == "SlipARS")//
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipARS();
                }

                if (strID == "SlipMKT")//
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipMKT();
                }

                if (strID == "SlipBiaya")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipBiaya();
                }

                if (strID == "SlipPakaiAsset")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiAsset();
                }

                if (strID == "SlipPakaiATK")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiATK();
                }

                if (strID == "SlipPakaiBaku")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiBaku();
                }

                if (strID == "SlipPakaiBantu")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiBantu();
                }

                if (strID == "SlipPakaiBiaya")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiBiaya();
                }

                if (strID == "SlipPakaiMKT")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiMKT();
                }

                if (strID == "SlipPakaiNonGRC")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiNonGRC();
                }

                if (strID == "SlipPakaiProject")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiProject();
                }

                if (strID == "SlipPakaiRepack")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiRepack();
                }

                if (strID == "SlipPakaiPart")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipPakaiPart();
                }

                if (strID == "BankOut")
                {
                    ViewBankOut();
                }
                if (strID == "SlipSPP")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipSPP();
                }
                if (strID == "RekapLapBul")

                {
                    papername = "a4";
                    ViewRekapLapBul();
                }
                if (strID == "RekapLapHarian")
                {
                    papername = "a4";
                    ViewRekapLapHarian();
                }
                if (strID == "RekapOutstandingSPP")
                {
                    papername = "legal";
                    ViewRekapOutstandingSPP();
                }

                if (strID == "RekapSPP")
                {
                    papername = "a4";
                    ViewRekapSPP();
                }

                if (strID == "RekapPO")
                {
                    papername = "a4";
                    ViewRekapPO();
                }

                if (strID == "RekapReceipt")
                {
                    papername = "a4";
                    ViewRekapReceipt();
                }

                if (strID == "LapPemantauanPurchn")
                {
                    papername = "a4";
                    ViewRekapLapPemantauanPurchn();
                }

                if (strID == "RekapPakai")
                {
                    papername = "a4";
                    ViewRekapPakai();
                }

                if (strID == "SlipRetur")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSlipRetur();
                }
                if (strID == "RekapPembelianBarang")
                {
                    ViewPembelianBarang();
                }
                if (strID == "LapBarang")
                {
                    papername = "a4";
                    ViewLapBarang();
                }
                if (strID == "kartustock")
                {
                    papername = "a4";
                    ViewKartuStock();
                }
                if (strID == "kartustockharga")
                {
                    papername = "a4";
                    ViewKartuStockHarga();
                }
                if (strID == "RekapDeathStock")
                {
                    papername = "a4";
                    ViewRekapDeathStock();
                }
                if (strID == "LJTempoBayar")
                {
                    papername = "a4";
                    ViewLJTempoBayar();
                }
                if (strID == "LPPnBM")
                {
                    papername = "Legal";
                    ViewLPPnBM();
                }
                if (strID == "lapWarnOrder")
                {
                    papername = "a4";
                    print_wo();
                }
                if (strID == "LapPantau")
                {
                    papername = "a4";
                    PrintLapPantauSPP();
                }
                if (strID == "SlipSJK")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSJK();
                }
                if (strID == "RekapLapBul")
                {
                    papername = "a4";
                    ViewRekapLapBul();
                }
                if (strID == "LapBreakBM")
                {
                    papername = "Legal";
                    PrintLapBreakBM();

                }
                if (strID == "SuratJalan")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSuratJalanWithLampiranMultiPage();
                }
                if (strID == "SuratJalanTO")
                {
                    papername = "SJ"; Panel1.Visible = true;
                    ViewSuratJalanTO();
                }
                if (strID == "bayarkertas")
                {
                    papername = "a4";
                    ViewBayarKertas();
                }
                if (strID == "bayarExp")
                {
                    papername = "a4";
                    ViewBayarExp();
                }
                if (strID == "bayarExp2")
                {
                    papername = "a4";
                    ViewBayarExp2();
                }
                //from report2
                if (strID == "SuratJalan")
                {
                    //ViewSuratJalan();
                }
                if (strID == "RekapHistPO")
                {
                    papername = "PO"; ; Panel1.Visible = true;
                    ViewHistPO();
                }

                if (strID == "RekapHistPOExcel")
                {
                    papername = "PO"; ; Panel1.Visible = true;
                    ViewHistPOExcel();
                }
                if (strID == "SuratJalanTO")
                {
                    ViewSuratJalanTO();
                }
                if (strID == "POPurchn")
                {
                    papername = "PO"; ; Panel1.Visible = true;
                    ViewPOPurchn2();
                }
                if (strID == "POPurchn2")
                {
                    papername = "PO"; ; Panel1.Visible = true;
                    ViewPOPurchnForFax();
                }

                if (strID == "Tawar")
                {
                    papername = "PO"; ; Panel1.Visible = true;
                    ViewTawar();
                }
                if (strID == "ImproveDetail")
                {
                    papername = "A4";
                    ViewLapImpDetail();
                }
                if (strID == "ImproveRekap")
                {
                    papername = "A4";
                    ViewLapImpRekap();
                }
                if (strID == "rkpReport")
                {
                    papername = "A4";
                    print_kendaraan();
                }
                if (strID == "dtlReport")
                {
                    papername = "A4";
                    print_dtlKendaraan();
                }
                if (strID == "lapSarmut")
                {
                    papername = "A4";
                    print_sarmut();
                    //print_rekap_sarmut();
                }
                if (strID == "DO")
                {
                    papername = "SJ"; ; Panel1.Visible = true;
                    print_DO();
                }
                if (strID == "kasbon")
                {
                    papername = "A4";
                    ViewKASBON();
                }
                if (strID == "kasbonRealisasi")
                {
                    papername = "A4";
                    ViewKasbonRealisasi();
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }

        }
        private void PrintLapRekapKom()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                objRpt1.Load(this.Server.MapPath("RekapHelp.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["xjudul"].ToString(), "xjudul");
                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");
                SetCurrentValuesForParameterField(objRpt1, Session["formno"].ToString(), "formno");

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        private void PrintLapBreakBM()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                int depo = ((Users)Session["Users"]).UnitKerjaID;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                if (depo == 1)
                {
                    objRpt1.Load(this.Server.MapPath("BreakBmReport.rpt"));
                }
                else if (depo == 7)
                {
                    objRpt1.Load(this.Server.MapPath("BreakBmReport1.rpt"));
                }
                else
                {
                    objRpt1.Load(this.Server.MapPath("BreakBmReport2.rpt"));
                }
                //objRpt1.Load(this.Server.MapPath("BreakBmReport.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["xjudul"].ToString(), "xjudul");
                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");
                SetCurrentValuesForParameterField(objRpt1, Session["formno"].ToString(), "formno");
                SetCurrentValuesForParameterField(objRpt1, Session["GP1"].ToString(), "GP1");
                SetCurrentValuesForParameterField(objRpt1, Session["GP2"].ToString(), "GP2");
                SetCurrentValuesForParameterField(objRpt1, Session["GP3"].ToString(), "GP3");
                SetCurrentValuesForParameterField(objRpt1, Session["GP4"].ToString(), "GP4");
                SetCurrentValuesForParameterField(objRpt1, Session["TargetBM"].ToString(), "TargetBM");
                SetCurrentValuesForParameterField(objRpt1, Session["TargetMTN"].ToString(), "TargetMTN");
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }

        public void ViewLapLoadingTime()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;

                objRpt1.Load(this.Server.MapPath("LapLoadingTime.rpt"));


                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                //SetCurrentValuesForParameterField(objRpt1, Session["TglTask"].ToString(), "drTgl");
                //SetCurrentValuesForParameterField(objRpt1, Session["sdTglTask"].ToString(), "sdTgl");

            }
            catch
            {

            }
        }
        public void ViewLapTask()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;

                //objRpt1.Load(this.Server.MapPath("LapTask.rpt"));
                if (Session["TglTask"].ToString() == "1")
                    objRpt1.Load(this.Server.MapPath("RekapTask.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("RekapTaskbyDept.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["TglTask"].ToString(), "drTgl");
                SetCurrentValuesForParameterField(objRpt1, Session["sdTglTask"].ToString(), "sdTgl");

            }
            catch
            {

            }
        }
        public void ViewFormTask()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                objRpt1.Load(this.Server.MapPath("FormTask.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
            }
            catch
            {

            }
        }

        public void ViewPembelianBarang()
        {
            ParameterField crParameterField;
            ParameterFields crParameterFields;
            ParameterDiscreteValue value = new ParameterDiscreteValue();
            ParameterValues currentParameterValues;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();

                da.Fill(dt);

                ReportDocument objRpt = new ReportDocument();

                string xJudul = "LAPORAN BULANAN GUDANG " + Session["lapbul"].ToString().ToUpper();

                objRpt.Load(this.Server.MapPath("RekapPembelianBarang.rpt"));

                objRpt.SetDataSource(dt);
                crViewer.ReportSource = objRpt;
                crViewer.DisplayToolbar = true;

                crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                value = new ParameterDiscreteValue();
                value.Value = Session["bulan"].ToString();
                currentParameterValues = new ParameterValues();
                currentParameterValues.Add(value);
                crParameterField = new ParameterField();
                crParameterField = crParameterFields["bulan"];
                crParameterField.CurrentValues = currentParameterValues;

                crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                value = new ParameterDiscreteValue();
                value.Value = Session["tahun"].ToString();
                currentParameterValues = new ParameterValues();
                currentParameterValues.Add(value);
                crParameterField = new ParameterField();
                crParameterField = crParameterFields["tahun"];
                crParameterField.CurrentValues = currentParameterValues;

                crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                value = new ParameterDiscreteValue();
                value.Value = xJudul;
                currentParameterValues = new ParameterValues();
                currentParameterValues.Add(value);
                crParameterField = new ParameterField();
                crParameterField = crParameterFields["xJudul"];
                crParameterField.CurrentValues = currentParameterValues;

            }
            catch
            {

            }
        }

        public void ViewRekapReceiptExcel()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                ParameterField crParameterField;
                ParameterFields crParameterFields;
                ParameterDiscreteValue value = new ParameterDiscreteValue();
                ParameterValues currentParameterValues;

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                ReportDocument objRpt = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt.Load(this.Server.MapPath("RekapReceiptExcel.rpt"));
                objRpt.SetDataSource(dt);
                crViewer.ReportSource = objRpt;

                crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                value = new ParameterDiscreteValue();
                value.Value = Session["drTgl"].ToString();
                currentParameterValues = new ParameterValues();
                currentParameterValues.Add(value);
                crParameterField = new ParameterField();
                crParameterField = crParameterFields["drTanggal"];
                crParameterField.CurrentValues = currentParameterValues;

                crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                value = new ParameterDiscreteValue();
                value.Value = Session["sdTgl"].ToString();
                currentParameterValues = new ParameterValues();
                currentParameterValues.Add(value);
                crParameterField = new ParameterField();
                crParameterField = crParameterFields["sdTanggal"];
                crParameterField.CurrentValues = currentParameterValues;
            }
            catch
            {

            }
        }

        public void ViewSlipRetur()
        {

            string strReceiptNo = string.Empty;
            if (Session["ReturNo"] != null)
            {
                strReceiptNo = Session["ReturNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipRetur(strReceiptNo);

            int depo = ((Users)Session["Users"]).UnitKerjaID;
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;

                SetCurrentValuesForParameterField(objRpt1, "RETUR BARANG / SPARE PART", "JudulPemakaian");
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/RB/27/10/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/SPB/03/99/R2", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewRekapPakai()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{

            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            string cek = Session["Query"].ToString();
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);

            objRpt1 = new ReportDocument();
            crViewer.DisplayToolbar = true;
            //crViewer.SeparatePages = false;
            Users users = (Users)Session["Users"];
            if (Session["type"].ToString() == "detail")
                objRpt1.Load(this.Server.MapPath("RekapPakai2.rpt"));
            else
                objRpt1.Load(this.Server.MapPath("RekapPakai3.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;

            SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "tgl1");
            SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "tgl2");
            SetCurrentValuesForParameterField(objRpt1, Session["deptName"].ToString(), "DeptName");
            SetCurrentValuesForParameterField(objRpt1, users.ViewPrice.ToString(), "viewprice");
            //}
            //catch(Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }

        public void ViewRekapLapPemantauanPurchn()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                string strJudul = Session["xjudul"].ToString();
                if (strJudul == "Laporan Pemantauan Purchasing")
                    objRpt1.Load(this.Server.MapPath("LapPemantauanPurchn.rpt"));
                else
                    objRpt1.Load(this.Server.MapPath("LapPemantauanPurchn.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");

                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");

                SetCurrentValuesForParameterField(objRpt1, strJudul, "xJudul");
            }
            catch
            {

            }
        }

        public void ViewSlipPakaiPart()
        {

            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            //int strRakID = 0;
            //if (Session["RakID"] != null)
            //{
            //    strRakID = Session["RakID"].ToString();
            //}

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);

            int depo = ((Users)Session["Users"]).UnitKerjaID;
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN BARANG / SPARE PART", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/SPB/03/99/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/SPB/03/99/R2", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiRepack()
        {

            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN RE-PACK", "JudulPemakaian");

                SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiProject()
        {

            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN PROJECT", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/SPB/03/10/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiNonGRC()
        {
            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }
            Company company1 = new Company();
            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("PemakaianNonGRC.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();

                Company company = companyFacade.RetrieveByDepoIdNonGRC(8);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN NON GRC", "JudulPemakaian");
                SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }
        }

        public void ViewSlipPakaiMKT()
        {
            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN MARKETING", "JudulPemakaian");

                SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiBiaya()
        {
            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN BIAYA", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/SPB/03/10/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/PBP/02/99/R2", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiBantu()
        {
            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN BAHAN PENUNJANG / BAKAR", "JudulPemakaian");
                SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiBaku()
        {

            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PENGAJUAN PERMINTAAN BAHAN BAKU", "JudulPemakaian");
                SetCurrentValuesForParameterField(objRpt1, " ", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSJK()
        {
            string strNoSJ = string.Empty;
            if (Session["NoSJ"] != null)
            {
                strNoSJ = Session["NoSJ"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSJK = reportFacade.ViewSuratKeluar(strNoSJ);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSJK.ToString(), sqlCon);

                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipSJK.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "Graha GRC board Lt.3", "address");
                SetCurrentValuesForParameterField(objRpt1, "Jl.S.Parman Kav 64, Slipi Palmerah", "address1");
                SetCurrentValuesForParameterField(objRpt1, "Jakarta Barat 11410 - Indonesia", "address2");
                SetCurrentValuesForParameterField(objRpt1, "Telp. : (62-21)53666800, Fax : (62-21)53666720", "phone");
                SetCurrentValuesForParameterField(objRpt1, "SURAT JALAN", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. BPAS05/SJ/15/01/R2", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/SJ/15/01/R1", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiATK()
        {

            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PERMINTAAN OFFICE EQUIPMENT", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/POE/28/04/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/POE/28/04/R0", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipPakaiAsset()
        {
            string strReceiptNo = string.Empty;
            if (Session["PakaiNo"] != null)
            {
                strReceiptNo = Session["PakaiNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipPakai(strReceiptNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("Pemakaian.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "PENGAJUAN PERMINTAAN ASSET", "JudulPemakaian");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/K/SPB/03/10/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "FORM NO. LOG/PBP/02/99/R2", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipBiaya()
        {

            string strReceiptNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strReceiptNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strReceiptNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");

                SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                SetCurrentValuesForParameterField(objRpt1, "", "CoAddress");
                SetCurrentValuesForParameterField(objRpt1, "BIAYA RECEIPT SLIP ( MRS )", "JudulSlip");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/MRS/30/99/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/LPB/13/99/R1", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipMKT()
        {

            string strSPPNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strSPPNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strSPPNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //            crViewer.DisplayToolbar = true;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                SetCurrentValuesForParameterField(objRpt1, "", "CoAddress");
                SetCurrentValuesForParameterField(objRpt1, "MARKETING RECEIPT SLIP ( MRS )", "JudulSlip");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/MRS/30/99/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/LPB/13/99/R1", "FormIso");
            }
            catch
            {

            }

        }

        public void ViewSlipARS()
        {
            Panel1.Visible = true;
            string strSPPNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strSPPNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strSPPNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                SetCurrentValuesForParameterField(objRpt1, "", "CoAddress");
                SetCurrentValuesForParameterField(objRpt1, "ASSET RECEIPT SLIP ( ARS )", "JudulSlip");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/ARS/50/10/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/LPB/13/99/R1", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipORS()
        {

            Panel1.Visible = true;
            string strSPPNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strSPPNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strSPPNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                SetCurrentValuesForParameterField(objRpt1, "", "CoAddress");
                SetCurrentValuesForParameterField(objRpt1, "OFFICE SUPPLIES RECEIPT SLIP ( OSRS )", "JudulSlip");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/OSRS/19/04/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/OSRS/19/04/R0", "FormIso");

            }
            catch
            {

            }

        }

        public void ViewSlipRMS()
        {
            Panel1.Visible = true;
            string strSPPNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strSPPNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strSPPNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;
            Receipt rcp = new ReceiptFacade().RetrieveByNo(strSPPNo);
            Domain.POPurchn scID = new POPurchnFacade().RetrieveByID(rcp.PoID);
            SuppPurchFacade spn = new SuppPurchFacade();
            Company spp = new Company();
            if (scID.SubCompanyID > 0)
            {
                spp = DetailCompany(scID.SubCompanyID);
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                string pt = company.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, spp.Nama, "CoName");
                SetCurrentValuesForParameterField(objRpt1, spp.Lokasi, "CoAddress");
                SetCurrentValuesForParameterField(objRpt1, "RAW MATERIAL RECEIPT SLIP ( RMS )", "JudulSlip");
                if (depo == 7)

                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/LPB/13/99/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/LPB/13/99/R2", "FormIso");
            }
            catch
            {

            }

        }

        public void ViewSlipMRS()
        {
            Panel1.Visible = true;
            string strSPPNo = string.Empty;
            if (Session["ReceiptNo"] != null)
            {
                strSPPNo = Session["ReceiptNo"].ToString();
            }

            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipReceipt(strSPPNo);
            int depo = ((Users)Session["Users"]).UnitKerjaID;
            Receipt rcp = new ReceiptFacade().RetrieveByNo(strSPPNo);
            SuppPurchFacade spn = new SuppPurchFacade();
            SuppPurch spp = spn.RetrieveById(rcp.SupplierID);

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("SlipReceiptMRS.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;
                CompanyFacade companyFacade = new CompanyFacade();

                if (spp.SubCompanyID == 8)
                {
                    Company company = companyFacade.RetrieveByDepoIdNonGrc(spp.SubCompanyID); string pt0 = company.Nama; Session["pt"] = pt0;
                }
                else
                {
                    Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID); string pt0 = company.Nama; Session["pt"] = pt0;
                }

                //Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                //string pt = company.Nama;
                string pt = Session["pt"].ToString();
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                SetCurrentValuesForParameterField(objRpt1, "", "CoAddress");

                SetCurrentValuesForParameterField(objRpt1, "MATERIAL RECEIPT SLIP ( MRS )", "JudulSlip");
                if (depo == 7)
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/K/MRS/30/99/R0", "FormIso");
                else
                    SetCurrentValuesForParameterField(objRpt1, "Form no. LOG/MRS/30/99/R2", "FormIso");

                //crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
                //value = new ParameterDiscreteValue();
                //value.Value = string.Format("{0:0,0.00}", vTotal);
                //currentParameterValues = new ParameterValues();
                //currentParameterValues.Add(value);
                //crParameterField = new ParameterField();
                //crParameterField = crParameterFields["Total"];
                //crParameterField.CurrentValues = currentParameterValues;
            }
            catch
            {

            }

        }

        public void ViewRekapOutstandingSPP()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();

                string strJudul = Session["xjudul"].ToString();

                if (strJudul == "Outstanding PO")
                {
                    string strprint = Session["print"].ToString();
                    if (strprint == "Portrait")
                        objRpt1.Load(this.Server.MapPath("LapOutstandingPO.rpt"));
                    else
                        objRpt1.Load(this.Server.MapPath("LapOutstandingPOLS.rpt"));
                }
                else
                    objRpt1.Load(this.Server.MapPath("LapOutstandingSPP.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");
                SetCurrentValuesForParameterField(objRpt1, strJudul, "xJudul");
            }
            catch
            {

            }
        }

        public void ViewRekapLapHarian()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                string xMgr = string.Empty;
                string xKsie = string.Empty;
                string xLokasi = string.Empty;
                string xAlamat1 = string.Empty;
                string xAlamat2 = string.Empty;
                string xJudul = string.Empty;

                Users users = (Users)Session["Users"];
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveById(users.UnitKerjaID);
                if (companyFacade.Error == string.Empty && company.ID > 0)
                {
                    xMgr = company.Manager;
                    xKsie = company.Spv;
                    xLokasi = company.Lokasi;
                    xAlamat1 = company.Alamat1;
                    xAlamat2 = company.Alamat2;
                    //if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
                    //    xJudul = "LAPORAN HARIAN BAHAN BAKU & BAHAN " + Session["lapbul"].ToString().ToUpper();
                    //else
                    xJudul = "Laporan Transaksi Warehouse " + Session["lapbul"].ToString();
                }

                //if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
                //    objRpt1.Load(this.Server.MapPath("LapHarianBakuBantu.rpt"));
                //else
                objRpt1.Load(this.Server.MapPath("LapHarianA.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                //crViewer.DisplayToolbar = true;
                /**
                 * Lock print button
                 */
                int grp = (Session["group"] != null) ? int.Parse(Session["group"].ToString()) : 0;
                int disablePrint = (Session["blmApprove"] != null) ? int.Parse(Session["blmApprove"].ToString()) : 0;
                switch (grp)
                {
                    case 3:
                    case 4:
                    case 5:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        crViewer.DisplayToolbar = (disablePrint > 0) ? false : true;
                        BtnPrint.Enabled = (disablePrint > 0) ? false : true;
                        break;
                    default:
                        crViewer.DisplayToolbar = true;
                        BtnPrint.Enabled = true;
                        break;
                }

                SetCurrentValuesForParameterField(objRpt1, Session["formno"].ToString(), "formno");
                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");


                SetCurrentValuesForParameterField(objRpt1, xJudul, "xjudul");

                SetCurrentValuesForParameterField(objRpt1, xMgr, "xMgr");

                SetCurrentValuesForParameterField(objRpt1, xKsie, "xKsie");

            }
            catch
            {

            }
        }

        public void ViewRekapAsset()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();

                da.Fill(dt);

                objRpt1 = new ReportDocument();

                string xMgr = string.Empty;
                string xKsie = string.Empty;
                string xLokasi = string.Empty;
                string xAlamat1 = string.Empty;
                string xAlamat2 = string.Empty;
                string xJudul = string.Empty;

                Users users = (Users)Session["Users"];
                CompanyFacade companyFacade = new CompanyFacade();
                Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
                if (companyFacade.Error == string.Empty && company.ID > 0)
                {
                    xMgr = company.Manager;
                    xKsie = company.Spv;
                    //xMgr = "ERVAN";
                    //xKsie = "RIDWAN";
                    xLokasi = company.Lokasi;
                    xAlamat1 = company.Alamat1;
                    xAlamat2 = company.Alamat2;
                    xJudul = "LAPORAN REKAP ASSET";
                }

                objRpt1.Load(this.Server.MapPath("RekapAsset.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");

                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");

                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");

                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");



                SetCurrentValuesForParameterField(objRpt1, xJudul, "xjudul");

                SetCurrentValuesForParameterField(objRpt1, xMgr, "xMgr");

                SetCurrentValuesForParameterField(objRpt1, xKsie, "xKsie");
            }
            catch
            {

            }
        }

        //public void ViewRekapLapBul()
        //{
        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //    //try
        //    //{
        //        sqlCon.Open();

        //        SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); 
        //        da.SelectCommand.CommandTimeout = 0;

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);

        //        objRpt1 = new ReportDocument();

        //        string xMgr = string.Empty;
        //        string xKsie = string.Empty;
        //        string xLokasi = string.Empty;
        //        string xAlamat1 = string.Empty;
        //        string xAlamat2 = string.Empty;
        //        string xJudul = string.Empty;
        //        string formno = " ";
        //        Users users = (Users)Session["Users"];
        //        CompanyFacade companyFacade = new CompanyFacade();
        //        Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
        //        if (users.UnitKerjaID==7)
        //            formno = "Form No. LOG/K/LSSP/39/08/R1";
        //        else
        //            formno = "Form No. LOG/LSSP/39/08/R1";    

        //        if (companyFacade.Error == string.Empty && company.ID > 0)
        //        {
        //            xMgr = company.Manager;
        //            xKsie = company.Spv;
        //            //xMgr = "ERVAN";
        //            //xKsie = "RIDWAN";
        //            xLokasi = company.Lokasi;
        //            xAlamat1 = company.Alamat1;
        //            xAlamat2 = company.Alamat2;
        //           // string test = Session["jenis"].ToString().ToUpper();
        //            if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
        //            {

        //                if (Session["jenis"].ToString().ToUpper() == "BAHAN BAKU" || Session["jenis"].ToString().ToUpper() == "BAHAN PEMBANTU")
        //                {
        //                    xJudul = "LAPORAN HARIAN BAHAN BAKU & BAHAN " + Session["lapbul"].ToString().ToUpper();
        //                    formno = "LOG/LMH/04/99/R4";

        //                }
        //                else
        //                {
        //                    xJudul = "LAPORAN HARIAN STOCK BARANG " + Session["jenis"].ToString().ToUpper();
        //                    formno = "LOG/K/LAM/52/11/R0";
        //                }
        //                if (Session["jenis"].ToString().ToUpper() == "MARKETING" || Session["jenis"].ToString().ToUpper() == "REPACK")
        //                {
        //                    formno = "LOG/LHAM/55/11/R1";
        //                }
        //            }
        //            else
        //            {

        //                if (Session["lapbul"].ToString().ToUpper() == "ORDER")
        //                    xJudul = "LAPORAN WARNING STOCK ORDER " + Session["jenis"].ToString().ToUpper();
        //                else
        //                {
        //                    string stock=string.Empty ;
        //                    if (Session["stock"].ToString() == "1")
        //                        stock = "STOCK";
        //                    if (Session["stock"].ToString() == "0")
        //                        stock = "NONSTOCK";
        //                    if (Session["stock"].ToString() == " ")
        //                        stock = " ";
        //                    xJudul = "LAPORAN " + stock + " BULANAN GUDANG " + Session["lapbul"].ToString().ToUpper();
        //                }
        //            }
        //        }
        //        if (Session["lapbul"].ToString().ToUpper() == "MARKETING")
        //        {
        //            if (((Users)Session["Users"]).UnitKerjaID == 1)
        //            {
        //                formno = "LOG/LBGM/54/11/R1";
        //            }
        //        }

        //        if (Session["lapbul"].ToString().ToUpper() == "ELECTRIK" || Session["lapbul"].ToString().ToUpper() == "MEKANIK")
        //        {
        //            if (((Users)Session["Users"]).UnitKerjaID == 1)
        //            {
        //                formno = "LOG/LSSP/39/08/R1";
        //            }
        //        }
        //        if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
        //            objRpt1.Load(this.Server.MapPath("LapHarianBakuBantu.rpt"));
        //        else
        //        {
        //            if (Session["lapbul"].ToString().ToUpper() == "ORDER")
        //                objRpt1.Load(this.Server.MapPath("LapWarnOrder.rpt"));
        //            else
        //            {
        //                if (Session["FormatCetak"].ToString() == "LandScape")
        //                {
        //                    if (Session["asset"].ToString().Trim() == "4")
        //                        objRpt1.Load(this.Server.MapPath("LapBul-S-Asset.rpt"));
        //                    else
        //                        objRpt1.Load(this.Server.MapPath("LapBul-S.rpt"));
        //                }
        //                else
        //                {
        //                    if (Session["asset"].ToString().Trim() == "4")
        //                        objRpt1.Load(this.Server.MapPath("LapBul-P-Asset.rpt"));
        //                    else
        //                    {
        //                        if (Session["price"].ToString()=="0" || Session["groupid"].ToString()=="10")
        //                            objRpt1.Load(this.Server.MapPath("LapBul-P.rpt"));
        //                        else
        //                            objRpt1.Load(this.Server.MapPath("LapBul-P-with-price1.rpt"));
        //                    }
        //                }
        //            }
        //        }
        //        objRpt1.SetDataSource(dt);
        //        crViewer.ReportSource = objRpt1;
        //        crViewer.DisplayToolbar = true;

        //        SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
        //        if (Session["lapbul"].ToString().ToUpper() != "PENUNJANG")
        //        {

        //            SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");

        //            SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");

        //            SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");

        //            SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");

        //        }

        //        SetCurrentValuesForParameterField(objRpt1, users.UserName.ToUpper(), "xusers");
        //        SetCurrentValuesForParameterField(objRpt1, xJudul, "xjudul");

        //        SetCurrentValuesForParameterField(objRpt1, xMgr, "xMgr");

        //        SetCurrentValuesForParameterField(objRpt1, xKsie, "xKsie");
        //        SetCurrentValuesForParameterField(objRpt1, formno, "formno");
        //    //}
        //    //catch
        //    //{

        //    //}
        //}

        public void ViewRekapLapBul()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();
            string strsql = Session["Query"].ToString();
            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();

            da.Fill(dt);

            objRpt1 = new ReportDocument();

            string xMgr = string.Empty;
            string xKsie = string.Empty;
            string xLokasi = string.Empty;
            string xAlamat1 = string.Empty;
            string xAlamat2 = string.Empty;
            string xJudul = string.Empty;

            string xPT = string.Empty;

            //string PIC2 = string.Empty;
            string formno = " ";
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);

            int GroupID2 = int.Parse(Session["groupid"].ToString());

            // Tambahan Beny         
            string StatusLapBul = Session["Elap"].ToString();
            string UnitKerja_Plant = Session["UnitKerja"].ToString();

            if (StatusLapBul != string.Empty)
            {
                if (StatusLapBul == "ELapBul")
                {
                    int GroupID = int.Parse(Session["groupid"].ToString());
                    int PeriodeBulan = int.Parse(Session["bulan1"].ToString());
                    int PeriodeTahun = int.Parse(Session["tahun1"].ToString());
                    LaporanBulananFacade groupsPurchnFacade1 = new LaporanBulananFacade();
                    LaporanBulanan group1 = new LaporanBulanan();
                    group1 = groupsPurchnFacade1.cekStatus1(PeriodeTahun, PeriodeBulan, GroupID);
                    int StatusLap = group1.Status;
                    Session["StatusLap"] = StatusLap;
                    Session["PeriodeBulan"] = PeriodeBulan;
                    Session["PeriodeTahun"] = PeriodeTahun;
                    Session["GroupID"] = GroupID;
                }
            }
            // End Tambahan

            if (users.UnitKerjaID == 7)
                formno = "Form No. LOG/K/LSSP/39/08/R1";
            else
                formno = "Form No. LOG/LSSP/39/08/R1";

            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xMgr = company.Manager;
                xKsie = company.Spv;
                //xMgr = "ERVAN";
                //xKsie = "RIDWAN";
                //string StatusLapBul = Session["Elap"].ToString();

                if (StatusLapBul == "ELapBul")
                {
                    int PeriodeBulan1 = Convert.ToInt32(Session["PeriodeBulan"]);
                    int PeriodeTahun1 = Convert.ToInt32(Session["PeriodeTahun"]);
                    int GroupID1 = Convert.ToInt32(Session["GroupID"]);
                    LaporanBulananFacade groupsPurchnFacade2 = new LaporanBulananFacade();
                    LaporanBulanan group2 = new LaporanBulanan();
                    group2 = groupsPurchnFacade2.RetrieveTTD(PeriodeTahun1, PeriodeBulan1, GroupID1);
                    int StatusElap = group2.Status;
                    string PIC = group2.PIC;
                    string PIC2 = PIC.Trim();
                    int GroupIDL = group2.GroupID;
                    Session["StatusElap"] = StatusElap;
                    Session["PIC2"] = PIC2;
                    Session["GroupIDL"] = GroupIDL;
                }



                //xLokasi = company.Lokasi;
                //xAlamat1 = company.Alamat1;
                //xAlamat2 = company.Alamat2;

                if (GroupID2 == 13)
                {
                    xPT = "PT. CIPTA PAPAN DINAMIKA (NON GRC)";
                    xLokasi = "Jakarta";
                    xAlamat1 = "GRAHA GRCBOARD Lt.3, Jl.S Parman kav. 64";
                    xAlamat2 = "Slipi Palmerah, Jakarta Barat";
                }
                else
                {
                    xPT = "PT. BANGUNPERKASA ADHITAMASENTRA";
                    xLokasi = company.Lokasi;
                    xAlamat1 = company.Alamat1;
                    xAlamat2 = company.Alamat2;
                }

                if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
                {

                    if (Session["jenis"].ToString().ToUpper() == "BAHAN BAKU" || Session["jenis"].ToString().ToUpper() == "BAHAN PEMBANTU")
                    {
                        xJudul = "LAPORAN HARIAN BAHAN BAKU & BAHAN " + Session["lapbul"].ToString().ToUpper();
                        formno = "LOG/LMH/04/99/R4";

                    }
                    else
                    {
                        xJudul = "LAPORAN HARIAN STOCK BARANG " + Session["jenis"].ToString().ToUpper();
                        formno = "LOG/K/LAM/52/11/R0";
                    }
                    if (Session["jenis"].ToString().ToUpper() == "MARKETING" || Session["jenis"].ToString().ToUpper() == "REPACK")
                    {
                        formno = "LOG/LHAM/55/11/R1";
                    }
                }
                else
                {

                    if (Session["lapbul"].ToString().ToUpper() == "ORDER")
                        xJudul = "LAPORAN WARNING STOCK ORDER " + Session["jenis"].ToString().ToUpper();
                    else
                    {
                        string stock = string.Empty;
                        if (Session["stock"].ToString() == "1")
                            stock = "STOCK";
                        if (Session["stock"].ToString() == "0")
                            stock = "NONSTOCK";
                        if (Session["stock"].ToString() == " ")
                            stock = " ";
                        xJudul = "LAPORAN " + stock + " BULANAN GUDANG " + Session["lapbul"].ToString().ToUpper();
                    }
                }
            }
            if (Session["lapbul"].ToString().ToUpper() == "MARKETING")
            {
                if (((Users)Session["Users"]).UnitKerjaID == 1)
                {
                    formno = "LOG/LBGM/54/11/R1";
                }
            }

            if (Session["lapbul"].ToString().ToUpper() == "ELECTRIK" || Session["lapbul"].ToString().ToUpper() == "MEKANIK")
            {
                if (((Users)Session["Users"]).UnitKerjaID == 1)
                {
                    formno = "LOG/LSSP/39/08/R1";
                }
            }

            if (Session["lapbul"].ToString().ToUpper() == "PENUNJANG")
                objRpt1.Load(this.Server.MapPath("LapHarianBakuBantu.rpt"));
            else
            {
                if (Session["lapbul"].ToString().ToUpper() == "ORDER")
                    objRpt1.Load(this.Server.MapPath("LapWarnOrder.rpt"));
                else
                {
                    if (Session["FormatCetak"].ToString() == "LandScape")
                    {
                        if (Session["asset"].ToString().Trim() == "4")
                            objRpt1.Load(this.Server.MapPath("LapBul-S-Asset.rpt"));

                        else if (StatusLapBul == "ELapBul")
                        {
                            if (UnitKerja_Plant == "1")
                            {

                                objRpt1.Load(this.Server.MapPath("LapBul-S_Sign.rpt")); // Laporan Bulanan Landscape with Sign    
                            }
                            else if (UnitKerja_Plant == "7")
                            {
                                objRpt1.Load(this.Server.MapPath("LapBul-S_Sign_krwg.rpt")); // Laporan Bulanan Landscape with Sign    
                            }
                            else if (UnitKerja_Plant == "13")
                            {
                                objRpt1.Load(this.Server.MapPath("LapBul-S_Sign_Jmbg.rpt")); // Laporan Bulanan Landscape with Sign    
                            }
                        }
                        else if (StatusLapBul == "LapBul")
                        {
                            objRpt1.Load(this.Server.MapPath("LapBul-S.rpt"));
                        }
                    }
                    else
                    {
                        if (Session["asset"].ToString().Trim() == "4")
                            objRpt1.Load(this.Server.MapPath("LapBul-P-Asset.rpt"));
                        else
                        {
                            if (Session["price"].ToString() == "0" || Session["groupid"].ToString() == "10")
                                objRpt1.Load(this.Server.MapPath("LapBul-P.rpt"));
                            else
                                objRpt1.Load(this.Server.MapPath("LapBul-P-with-price1.rpt"));
                        }
                    }
                }
            }

            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;

            SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "prdawal");
            if (Session["lapbul"].ToString().ToUpper() != "PENUNJANG")
            {
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");

                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");

                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");

                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");

                SetCurrentValuesForParameterField(objRpt1, xPT, "alamat3");
            }

            if (StatusLapBul == "ELapBul")
            {
                string PICL = Session["PIC2"].ToString();
                string GroupIDL1 = Session["GroupIDL"].ToString();
                string StatusElap1 = Session["StatusElap"].ToString();

                SetCurrentValuesForParameterField(objRpt1, users.UserName.ToUpper(), "xusers");
                SetCurrentValuesForParameterField(objRpt1, xJudul, "xjudul");

                SetCurrentValuesForParameterField(objRpt1, xMgr, "xMgr");

                SetCurrentValuesForParameterField(objRpt1, xKsie, "xKsie");
                SetCurrentValuesForParameterField(objRpt1, formno, "formno");

                SetCurrentValuesForParameterField(objRpt1, PICL, "PIC");
                SetCurrentValuesForParameterField(objRpt1, StatusElap1.ToString(), "StatusElap1");
                SetCurrentValuesForParameterField(objRpt1, GroupIDL1.ToString(), "GroupIDL");

                LoadConvertPDF();
            }
            else
            {
                SetCurrentValuesForParameterField(objRpt1, users.UserName.ToUpper(), "xusers");
                SetCurrentValuesForParameterField(objRpt1, xJudul, "xjudul");

                SetCurrentValuesForParameterField(objRpt1, xMgr, "xMgr");

                SetCurrentValuesForParameterField(objRpt1, xKsie, "xKsie");
                SetCurrentValuesForParameterField(objRpt1, formno, "formno");
            }

        }

        protected void LoadConvertPDF()
        {
            Users users = (Users)Session["Users"];
            int GroupID = int.Parse(Session["GroupID"].ToString());
            int PeriodeBulan = int.Parse(Session["bulan1"].ToString());
            int PeriodeTahun = int.Parse(Session["tahun1"].ToString());
            int IDLap = Convert.ToInt32(Session["IDLap"]);

            if (GroupID > 0)
            {
                if (GroupID == 1)
                { string GroupPurc = "BB"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 2)
                { string GroupPurc = "BP"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 3)
                { string GroupPurc = "ATK"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 4)
                { string GroupPurc = "Asset"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 5)
                { string GroupPurc = "Biaya"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 6)
                { string GroupPurc = "Project"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 7)
                { string GroupPurc = "Marketing"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 8)
                { string GroupPurc = "Elektrik"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 9)
                { string GroupPurc = "Mekanik"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 10)
                { string GroupPurc = "Repack"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 11)
                { string GroupPurc = "Bahan Bakar"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 12)
                { string GroupPurc = "Asset Berkomponen"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 13)
                { string GroupPurc = "Barang Non GRC (Barang Marketing)"; Session["GroupPurc"] = GroupPurc; }
                else if (GroupID == 14)
                { string GroupPurc = "Repack Barang Non GRC (Barang Marketing)"; Session["GroupPurc"] = GroupPurc; }
                //else if (GroupID == 5)
                //{ string GroupPurc = "Biaya"; Session["GroupPurc"] = GroupPurc; }


            }

            string GroupPurchasing = Session["GroupPurc"].ToString();
            string TahunLB = Session["tahun1"].ToString();
            string BulanLB = Session["bulan1"].ToString();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "D:\\Laporan\\" + "LapBul_" + GroupPurchasing + "_" + BulanLB + "_" + TahunLB + ".pdf";
            string FileName1 = CrDiskFileDestinationOptions.DiskFileName;
            string FileName = FileName1.Substring(11);

            LaporanBulanan Group3 = new LaporanBulanan();
            LaporanBulananFacade GroupF3 = new LaporanBulananFacade();
            int Sts = GroupF3.RetrieveFileID(IDLap);
            //int ID = Group3.ID;


            LaporanBulanan Group2 = new LaporanBulanan();
            LaporanBulananFacade GroupF2 = new LaporanBulananFacade();
            Group2 = GroupF2.RetrieveFileID(FileName, IDLap);
            int ID = Group2.ID;

            if (ID == 0)
            {
                LaporanBulanan Group1 = new LaporanBulanan();
                LaporanBulananFacade GroupF = new LaporanBulananFacade();
                int intResult = 0;
                Group1.FileName = FileName;
                Group1.CreatedBy = users.UserName;
                Group1.LapID = IDLap;
                intResult = GroupF.InsertLapBulFileName(Group1);

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
                //Group1.FileName = FileName;
                Group1.Status = Sts;
                Group1.LapID = IDLap;
                intResult = GroupF.UpdateDataCetak(Group1);

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

        public void ViewRekapSPP()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("RekapSPP.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTanggal");

                SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTanggal");

            }
            catch
            {

            }
        }

        public void ViewRekapPO()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("RekapPO.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTanggal");

                SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTanggal");
            }
            catch
            {

            }
        }

        public void ViewRekapReceipt()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("RekapReceipt.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTanggal");

                SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTanggal");
            }
            catch
            {

            }
        }

        public void ViewBankOut()
        {
            //        ParameterField crParameterField;
            //        ParameterFields crParameterFields;
            //        ParameterDiscreteValue value = new ParameterDiscreteValue();
            //        ParameterValues currentParameterValues;

            //        string strBanOutNo = string.Empty;
            //        if (Session["BankOutNo"] != null)
            //        {
            //            strBanOutNo = Session["BankOutNo"].ToString();
            //        }

            //        TerbilangFacade terbilangFacade = new TerbilangFacade();

            //        BankOutDetailFacade bankOutDetailFacade = new BankOutDetailFacade();
            //        BankOutDetail bankOut = bankOutDetailFacade.RetrieveTotalByNo(strBanOutNo);
            ////        int vTotal = Convert.ToInt32(bankOut.Total);
            //        decimal vTotal = bankOut.Total;
            //        string terSay = terbilangFacade.ConvertMoneyToWords(bankOut.Total);

            //        ReportFacade reportFacade = new ReportFacade();
            //        string strBankCard = reportFacade.ViewBankOut(strBanOutNo);

            //        SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //        try
            //        {
            //            sqlCon.Open();

            //            SqlDataAdapter da = new SqlDataAdapter(strBankCard.ToString(), sqlCon);

            //            DataTable dt = new DataTable();

            //            da.Fill(dt);

            //            objRpt1 = new ReportDocument();

            //            objRpt1.Load(this.Server.MapPath("BankOut.rpt"));
            //            objRpt1.SetDataSource(dt);
            //            crViewer.ReportSource = objRpt1;
            //            //            crViewer.DisplayToolbar = true;

            //            //crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
            //            //value = new ParameterDiscreteValue();
            //            //value.Value = "BUKTI BANK MASUK";
            //            //currentParameterValues = new ParameterValues();
            //            //currentParameterValues.Add(value);
            //            //crParameterField = new ParameterField();
            //            //crParameterField = crParameterFields["vDebet"];
            //            //crParameterField.CurrentValues = currentParameterValues;

            ////            string tersay = Terbilang(Convert.ToInt32(vTotal));
            //            crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
            //            value = new ParameterDiscreteValue();
            //            value.Value = "Terbilang : # " + terSay + " #";
            //            currentParameterValues = new ParameterValues();
            //            currentParameterValues.Add(value);
            //            crParameterField = new ParameterField();
            //            crParameterField = crParameterFields["Terbilang"];
            //            crParameterField.CurrentValues = currentParameterValues;


            //            //strVolume = String.Format(“{0:#,###}”, intVolume);
            //            //string.Format(System.Globalization.CultureInfo.CreateSpecificCulture(“hi”), “{0:#,0}”, temp1);
            //            crParameterFields = new ParameterFields(crViewer.ParameterFieldInfo);
            //            value = new ParameterDiscreteValue();
            //            value.Value = string.Format("{0:0,0.00}", vTotal);
            //                //string.Format("{0:###,###,###,###.00}",vTotal);
            //            currentParameterValues = new ParameterValues();
            //            currentParameterValues.Add(value);
            //            crParameterField = new ParameterField();
            //            crParameterField = crParameterFields["Total"];
            //            crParameterField.CurrentValues = currentParameterValues;
            //        }
            //        catch
            //        {

            //        }

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

        private int  SetPrintOptions(string paper)
        {
            int result = -1;
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
                            int resultp = printerfacade.Insertraw(STRPRINTERNAME, papername, rawKind);
                        }
                        objRpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
                        CrystalDecisions.Shared.PageMargins pMargin = new CrystalDecisions.Shared.PageMargins(0, 0, 0, 0);
                        objRpt1.PrintOptions.ApplyPageMargins(pMargin);
                        result = 0;
                    }
                }
            }
            catch
            {
                return result;
            }
            return result;
        }

        public void ViewSlipSPP()
        {
            string strSPPNo = string.Empty;
            if (Session["NoSPP"] != null)
            {
                strSPPNo = Session["NoSPP"].ToString();
            }
            ReportFacade reportFacade = new ReportFacade();
            string strSlipMRS = reportFacade.ViewSlipSPP(strSPPNo);
            //CompanyFacade comFacade = new CompanyFacade();
            //Company com = comFacade.RetrieveByCriteria("DepoID",);
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSlipMRS.ToString(), sqlCon);
            DataTable dt = new DataTable();

            da.Fill(dt);

            Users users = (Users)Session["Users"];
            int DepartID = users.DeptID;
            int zDepoID = users.UnitKerjaID;

            objRpt1 = new ReportDocument();

            objRpt1.Load(this.Server.MapPath("SlipSPP2.rpt"));
            //if (zDepoID == 1)
            //{
            //    if (DepartID == 16 || DepartID == 13 || DepartID == 10)
            //    {
            //        objRpt1.Load(this.Server.MapPath("SlipSPP.rpt"));
            //    }
            //    if (DepartID == 6)
            //    {
            //        objRpt1.Load(this.Server.MapPath("SlipSPP2.rpt"));
            //    }
            //    else
            //    {
            //        objRpt1.Load(this.Server.MapPath("SlipSPP.rpt"));
            //    }
            //}
            //else
            //{
            //    if (DepartID == 1 || DepartID == 7 || DepartID == 8)
            //    {
            //        objRpt1.Load(this.Server.MapPath("SlipSPPLQ2180.rpt"));
            //    }
            //    if (DepartID == 4 || DepartID == 5 || DepartID == 18 || DepartID == 19)
            //    {
            //        objRpt1.Load(this.Server.MapPath("SlipSPPLX300.rpt"));
            //    }
            //}
            objRpt1.SetDataSource(dt);
            //SetPrintOptions(papername);
            crViewer.ReportSource = objRpt1;
            //crViewer.DisplayToolbar = true;

            SetCurrentValuesForParameterField(objRpt1, "SURAT PERMINTAAN PEMBELIAN ( SPP )", "JudulSlip");
            //Tampilkan Nama head
            SPPFacade sPPFacade = new SPPFacade();
            SPP spp = sPPFacade.RetrieveByCriteriaSPP("A.NoSpp", strSPPNo);
            string rCreatedBy = spp.CreatedBy;

            UsersFacade usersFacade = new UsersFacade();
            Users user = new Users();
            user = usersFacade.RetrieveByUserName(rCreatedBy);
            int DepartemenID = user.DeptID;

            DeptFacade deptFacade = new DeptFacade();
            Dept dept = new Dept();
            dept = deptFacade.RetrieveById(DepartemenID);
            string rNamaHead = dept.NamaHead;
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
            string pt = company.Nama;
            SetCurrentValuesForParameterField(objRpt1, pt, "pt");
            SetCurrentValuesForParameterField(objRpt1, rNamaHead, "NamaHead");
            //}
            //catch
            //{
            //}
        }

        public void ViewRekapPakaiDeptItem()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("RekapPakaiDeptItem.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTgl");

                SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTgl");

                SetCurrentValuesForParameterField(objRpt1, Session["deptName"].ToString(), "DeptName");
            }
            catch
            {

            }
        }

        public void ViewLapBarang()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {

                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("LapBarang.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["TipeBarang"].ToString(), "tipeBarang");
                SetCurrentValuesForParameterField(objRpt1, Session["rGroup"].ToString(), "group");
                SetCurrentValuesForParameterField(objRpt1, Session["rStock"].ToString(), "stock");
                SetCurrentValuesForParameterField(objRpt1, Session["rAktif"].ToString(), "aktif");
            }
            catch
            {

            }
        }
        public void ViewKartuStock()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("KartuStock.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["itemcode"].ToString(), "itemcode");
                SetCurrentValuesForParameterField(objRpt1, Session["itemname"].ToString(), "itemname");
                SetCurrentValuesForParameterField(objRpt1, Session["satuan"].ToString(), "satuan");
                SetCurrentValuesForParameterField(objRpt1, Session["type"].ToString(), "type");
                SetCurrentValuesForParameterField(objRpt1, Session["minstock"].ToString(), "minstock");
                SetCurrentValuesForParameterField(objRpt1, Session["maxstock"].ToString(), "maxstock");
                SetCurrentValuesForParameterField(objRpt1, Session["reorder"].ToString(), "reorder");

            }
            catch
            {

            }
        }

        public void ViewKartuStockHarga()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("KartuStockHarga.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["itemcode"].ToString(), "itemcode");
                SetCurrentValuesForParameterField(objRpt1, Session["itemname"].ToString(), "itemname");
                SetCurrentValuesForParameterField(objRpt1, Session["satuan"].ToString(), "satuan");
                SetCurrentValuesForParameterField(objRpt1, Session["type"].ToString(), "type");
                SetCurrentValuesForParameterField(objRpt1, Session["minstock"].ToString(), "minstock");
                SetCurrentValuesForParameterField(objRpt1, Session["maxstock"].ToString(), "maxstock");
                SetCurrentValuesForParameterField(objRpt1, Session["reorder"].ToString(), "reorder");
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");

            }
            catch
            {

            }
        }

        public void ViewRekapDeathStock()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("DeathStock.rpt"));
                objRpt1.SetDataSource(dt);
                //SetPrintOptions(papername);
                crViewer.ReportSource = objRpt1;

                //SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
                SetCurrentValuesForParameterField(objRpt1, Session["Group"].ToString(), "Group");
                SetCurrentValuesForParameterField(objRpt1, Session["termasuk"].ToString(), "termasuk");
            }
            catch
            {

            }
        }

        public void ViewLJTempoBayar()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                //crViewer.SeparatePages = false;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("LJTempoBayar.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
                SetCurrentValuesForParameterField(objRpt1, Session["typetgl"].ToString(), "typetgl");

                //SetCurrentValuesForParameterField(objRpt1, Session["termasuk"].ToString(), "termasuk");
            }
            catch
            {

            }
        }
        public void ViewLPPnBM()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;
                Users users = (Users)Session["Users"];

                objRpt1.Load(this.Server.MapPath("LPPnBM.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
            }
            catch
            {

            }
        }

        private void print_wo()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            crViewer.DisplayToolbar = true;
            crViewer.SeparatePages = true;
            crViewer.ShowAllPageIds = true;

            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("RekapWarningOrder.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
            SetCurrentValuesForParameterField(objRpt1, Session["spGroup"].ToString(), "spGroup");
        }
        private void PrintLapPantauSPP()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;

                objRpt1.Load(this.Server.MapPath("LapPemantauanSPP.rpt"));


                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;

                SetCurrentValuesForParameterField(objRpt1, Session["Tahun"].ToString(), "Tahun");
                SetCurrentValuesForParameterField(objRpt1, Session["Bulan"].ToString(), "Bulan");
                SetCurrentValuesForParameterField(objRpt1, Session["Group"].ToString(), "Group");

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
        /* added on 02-03-2015
         * author : beny
         */
        public void ViewLapOpname()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                da.SelectCommand.CommandTimeout = 0;

                string PT = string.Empty;
                string GroupID = Session["groupid"].ToString();

                if (GroupID == "13")
                {
                    PT = "PT CIPTA PAPAN DINAMIKA (NON GRC)";
                }
                else
                {
                    PT = "PT BANGUNPERKASA ADHITAMASENTRA";
                }

                DataTable dt = new DataTable();

                da.Fill(dt);
                ReportDocument objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapOpname1.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["lokasi"].ToString(), "lokasi");
                SetCurrentValuesForParameterField(objRpt1, Session["group"].ToString(), "group");
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "prdakhir");
                SetCurrentValuesForParameterField(objRpt1, Session["periodeopname"].ToString(), "periodeopname");
                SetCurrentValuesForParameterField(objRpt1, PT, "PT");
            }
            catch
            {

            }
        }
        /*=============================================================================*/

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["IdReport"].ToString() == "SlipMRS" ||
                Request.QueryString["IdReport"].ToString() == "SlipRMS" ||
                Request.QueryString["IdReport"].ToString() == "SlipARS" ||
                Request.QueryString["IdReport"].ToString() == "SlipMKT" ||
                Request.QueryString["IdReport"].ToString() == "SlipBiaya" ||
                Request.QueryString["IdReport"].ToString() == "SlipORS")
            {
                CetakReceipt cr = new CetakReceipt();
                cr.Field = "isnull(Cetak,0) as Cetak";
                cr.Criteria = " where ReceiptNo='" + Session["ReceiptNo"].ToString() + "'";
                int Cetak = cr.StatusCetak();
                if (Cetak == 0)
                {
                   if ( SetPrintOptions(papername) >-1) 
                    {
                        objRpt1.PrintToPrinter(1, false, 1, 99);
                        CetakReceipt crk = new CetakReceipt();
                        crk.Field = "Cetak=1,CetakTime='" + ((Users)Session["Users"]).UserID.ToString() + " on :" + DateTime.Now.ToString() + "'";
                        crk.Criteria = " ReceiptNo='" + Session["ReceiptNo"].ToString() + "'";
                        int rst = crk.UpdateStatusCetak();
                        if (rst < 0)
                        {
                            DisplayAJAXMessage(this, "Database Error");
                            CloseWindow(this);
                            return;
                        }
                        else
                        {
                            CloseWindow(this);
                        }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Printer Error !");
                        return;
                    }

                }
                else
                {
                    DisplayAJAXMessage(this, "Receipt no " + Session["ReceiptNo"].ToString() + " sudah pernah di cetak");
                    return;
                }
            }
            if (Session["CetakSJ"] != null)
            {
                int cetak = int.Parse(Session["CetakSJ"].ToString());
                int cetakto = int.Parse(Session["CetakSJto"].ToString());
                if (Session["CetakSJ"] != null)
                {
                    if ((int)Session["CetakSJ"] == 0 || (int)Session["CetakSJto"] == 1)
                    {
                        BtnPrint.Enabled = false;


                        if (Session["CSuratJalan"] != null)
                        {
                            SuratJalan suratJalan = (SuratJalan)Session["CSuratJalan"];
                            if (suratJalan.DepoID != ((Users)Session["Users"]).UnitKerjaID)
                            {
                                DisplayAJAXMessage(this, "Bukan Surat Jalan Depo / Pabrik anda ");
                                return;
                            }

                            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
                            int intResult = cpdWebService.sj_UpdateCetakSuratJalan(suratJalan.ID, 1);
                            if (intResult == 0)
                                DisplayAJAXMessage(this, "Error update status Cetak Surat Jalan ");
                            else
                            {
                                Session["CSuratJalan"] = null;
                                //CloseWindow(this);
                            }
                        }
                        if (SetPrintOptions(papername) > -1)
                        {
                            objRpt1.PrintToPrinter(1, false, 1, 99);
                            Session["CSuratJalan"] = null;
                            CloseWindow(this);
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Printer Error !");
                            return;
                        }
                    }

                    string cprint = Session["CetakSJ"].ToString();
                    string cto = Session["CetakSJto"].ToString();
                    if ((int)Session["CetakSJ"] == 1 || (int)Session["CetakSJto"] == 0)
                    {
                        BtnPrint.Enabled = false;
                       
                        if (Session["CSuratJalan"] != null)
                        {
                            Domain.SuratJalanTO suratJalan = (Domain.SuratJalanTO)Session["CSuratJalan"];
                            if (suratJalan.DepoID != ((Users)Session["Users"]).UnitKerjaID)
                            {
                                DisplayAJAXMessage(this, "Bukan Surat Jalan Depo / Pabrik anda ");
                                return;
                            }

                            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
                            int intResult = cpdWebService.sj_UpdateCetakSuratJalan(suratJalan.ID, 2);
                            if (intResult == 0)
                            {
                                DisplayAJAXMessage(this, "Error update status Cetak Surat Jalan Depo");
                                //return;
                            }
                            else
                            {
                                Session["CSuratJalan"] = null;
                                //CloseWindow(this);
                            }
                        }
                        if (SetPrintOptions(papername) > -1)
                        {
                            objRpt1.PrintToPrinter(1, false, 1, 99);
                            Session["CSuratJalan"] = null;
                            CloseWindow(this);
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Printer Error !");
                            return;
                        }

                    }
                    if ((int)Session["CetakSJ"] == 1 || (int)Session["CetakSJto"] == 1)
                    {
                        BtnPrint.Enabled = false;
                        DisplayAJAXMessage(this, "Surat Jalan ini sudah pernah di cetak");
                        return;
                    }

                }
            }

            if (Request.QueryString["IdReport"].ToString() == "POPurchn" || Request.QueryString["IdReport"].ToString() == "POPurchn2")
            {
                if ((int)Session["CetakPO"] == 0)
                {
                    if (Session["CPOPurchn"] != null)
                    {
                        if (SetPrintOptions(papername) > -1)
                        {
                            objRpt1.PrintToPrinter(1, false, 1, 99);
                            Domain.POPurchn pOPurchn = (Domain.POPurchn)Session["CPOPurchn"];
                            pOPurchn.Cetak = 1;
                            ArrayList arrPOPurchn = new ArrayList();
                            arrPOPurchn.Add(new POPurchnDetail());
                            POPurchnProcessFacade pOPurchnProcessFacade = new POPurchnProcessFacade(pOPurchn, arrPOPurchn, new SPPNumber());
                            string strError = pOPurchnProcessFacade.UpdateCetakPOPurchn();
                            if (strError != string.Empty)
                                DisplayAJAXMessage(this, strError);
                            else
                            {
                                Session["CPOPurchn"] = null;
                                CloseWindow(this);
                            }
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Printer Error !");
                            return;
                        }
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, "Report ini sudah pernah di cetak");
                    return;
                }
            }
            else if (Request.QueryString["IdReport"].ToString() == "DO")
            {
                if (Session["DONum"] != null)
                {
                    if (SetPrintOptions(papername) > -1)
                    {
                        objRpt1.PrintToPrinter(1, false, 1, 99);
                        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                        SqlDataReader sdr = da.RetrieveDataByString("Update MemoHarian_Armada set Cetak=1 where DoNum='" + Session["DONum"].ToString() + "'");
                        if (da.Error == string.Empty)
                        {
                            Session["DONum"] = null;
                            CloseWindow(this);
                        }
                        else
                        {
                            DisplayAJAXMessage(this, da.Error.ToString());
                        }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Printer Error !");
                        return;
                    }

                }
            }
            else if (SetPrintOptions(papername) > -1)
            {
                SetPrintOptions(papername);
                objRpt1.PrintToPrinter(1, false, 1, 99);
                Session["ReceiptNo"] = string.Empty;
                CloseWindow(this);
            }
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
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private string SubCompanyAktif = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SubCompanyAktif", "PO").ToString();
        public Company DetailCompany(int ID)
        {
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            Company coi = new Company();
            try
            {
                string strSQL = "Select * From SuppPurch where CoID=" + ID;
                //DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString("ServerHO"));
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        coi.Nama = sdr["SupplierName"].ToString();
                        coi.Alamat1 = sdr["Telepon"].ToString();
                        coi.Alamat2 = sdr["Fax"].ToString();
                        coi.Manager = sdr["UP"].ToString();
                        coi.Lokasi = sdr["Alamat"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                coi.Nama = ex.Message;
            }
            finally { da.CloseConnection(); }
            return coi;
        }

        public void ViewSuratJalanWithLampiranMultiPage()
        {
            Session["CSuratJalan"] = null;
            if (Session["dtCetakSJ"] != null)
            {
                DataTable dtCtk = (DataTable)Session["dtCetakSJ"];

                Session["CetakSJto"] = 1; //suratJalan.Cetak;
                Session["CetakSJ"] = 0; //suratJalan.Cetak;
                SuratJalan sj = new SuratJalan();
                sj.DepoID = int.Parse(dtCtk.Rows[0]["DepoID"].ToString());
                sj.ID = int.Parse(dtCtk.Rows[0]["ID"].ToString());

                Session["CSuratJalan"] = sj;

                DataTable dt = dtCtk;
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = false;
                string TypeOP = string.Empty;
                if (int.Parse(dtCtk.Rows[0]["TypeOP"].ToString()) == 4)
                    TypeOP = "PT. BANGUNPERKASA ADHITAMASENTRA";
                else
                    TypeOP = "PT. CIPTAPAPAN DINAMIKA";
                if (int.Parse(dtCtk.Rows[0]["MssOpType"].ToString()) == 4)
                    TypeOP = "PT. CIPTA GRAHA INDAH";

                objRpt1.Load(this.Server.MapPath("SuratJalanCTRP_NewMultiPage.rpt"));


                objRpt1.SetDataSource(dt);

                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, TypeOP, "TypeOP");
            }
        }
        public void ViewSuratJalanTO()
        {
            Session["CSuratJalan"] = null;
            if (Session["dtCetakSJto"] != null)
            {
                DataTable dtCtk = (DataTable)Session["dtCetakSJto"];

                Session["CetakSJ"] = 1; //suratJalan.Cetak;
                Session["CetakSJto"] = 0; //suratJalan.Cetak;
                Domain.SuratJalanTO sj = new Domain.SuratJalanTO();

                sj.DepoID = int.Parse(dtCtk.Rows[0]["DepoID"].ToString());
                sj.ID = int.Parse(dtCtk.Rows[0]["ID"].ToString());
                Session["CSuratJalan"] = sj;

                DataTable dt = dtCtk;
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = false;

                objRpt1.Load(this.Server.MapPath("SuratJalanTO.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
            }
        }
        public void ViewBayarKertas()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            if (Session["docno"] == null)
                return;

            int x = 0;
            string docno = Session["docno"].ToString();
            TerbilangFacade terbilangFacade = new TerbilangFacade();
            int total = Int32.Parse(Session["total"].ToString());
            string terSay = terbilangFacade.ConvertMoneyToWords(total);

            ArrayList arrBayar = new ArrayList();
            //PettyCashOutDetailFacade PettyCashOutDetailFacade = new PettyCashOutDetailFacade();
            //PettyCashOutDetail PettyCashOut = PettyCashOutDetailFacade.RetrieveTotalByNo(pettycashNo);

            decimal vTotal = decimal.Parse(Session["total"].ToString());
            DepoKertasKA dk = new DepoKertasKA();
            DepoKertasKA pettyCashOutFacade = new DepoKertasKA();
            string Criteria = " And DocNo = '" + docno + "'";
            BayarKertas pettyCashOut = pettyCashOutFacade.RetrieveBayar1(Criteria);
            string bgno = pettyCashOutFacade.RetrieveBayar2(Criteria);
            //int pettyCashOutID = pettyCashOut.ID;
            ArrayList arrBayarkertas = new ArrayList();
            if (Session["pelunasan"].ToString().Trim() == "2")
                arrBayarkertas = pettyCashOutFacade.RetrieveBayarCetak(Criteria);
            else if (Session["pelunasan"].ToString().Trim() == "0")
                arrBayarkertas = pettyCashOutFacade.RetrieveBayar(Criteria);
            else if (Session["pelunasan"].ToString().Trim() == "1")
                arrBayarkertas = pettyCashOutFacade.RetrieveBayarCetakDP(Criteria);

            if (pettyCashOutFacade.Error == string.Empty)
            {
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;

                objRpt1.Load(this.Server.MapPath("fakturbayarkertas.rpt"));

                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.DocNo, "PettyCashNo");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.TglBayar.ToString("dd/MM/yyyy"), "PettyCashDate");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.Penerima, "Kepada");
                SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(), "Total");
                SetCurrentValuesForParameterField(objRpt1, terSay + " Rupiah", "terbilang");
                crViewer.ReportSource = objRpt1;
                string nosj = string.Empty;
                foreach (BayarKertas BayarKertas in arrBayarkertas)
                {
                    x = x + 1;
                    if (BayarKertas.NoSJ.Trim() == string.Empty)
                        nosj = string.Empty;
                    else
                        nosj = "(" + BayarKertas.NoSJ.Trim() + ")";
                    SetCurrentValuesForParameterField(objRpt1, x.ToString(), "No" + x.ToString());
                    if (BayarKertas.ItemName.Trim().ToUpper() != "PEMBAYARAN DP")
                        SetCurrentValuesForParameterField(objRpt1, nosj + BayarKertas.ItemName.Trim() + " (" + BayarKertas.Qty.ToString("N0") + ") @" +
                            BayarKertas.Harga.ToString("N0"), "uraian" + x.ToString());
                    else
                        SetCurrentValuesForParameterField(objRpt1, nosj + BayarKertas.ItemName.Trim(), "uraian" + x.ToString());

                    SetCurrentValuesForParameterField(objRpt1, BayarKertas.TotalHarga.ToString("N0"), "nilai" + x.ToString());
                }
            }
            if (x < 10)
            {
                x = x + 1;

                for (int i = x; i <= 10; i++)
                {
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "No" + i.ToString());
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "uraian" + i.ToString());
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "nilai" + i.ToString());
                }
            }
            SetCurrentValuesForParameterField(objRpt1, bgno, "uraian10");
        }
        public void ViewBayarExp()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            if (Session["docno"] == null)
                return;

            int x = 0;
            string docno = Session["docno"].ToString();
            TerbilangFacade terbilangFacade = new TerbilangFacade();
            int total = Int32.Parse(Session["total"].ToString());
            string terSay = terbilangFacade.ConvertMoneyToWords(total);

            ArrayList arrBayar = new ArrayList();
            //PettyCashOutDetailFacade PettyCashOutDetailFacade = new PettyCashOutDetailFacade();
            //PettyCashOutDetail PettyCashOut = PettyCashOutDetailFacade.RetrieveTotalByNo(pettycashNo);

            decimal vTotal = decimal.Parse(Session["total"].ToString());
            DepoKertasKA dk = new DepoKertasKA();
            DepoKertasKA pettyCashOutFacade = new DepoKertasKA();
            string Criteria = " And DocNo = '" + docno + "'";
            BayarKertas pettyCashOut = pettyCashOutFacade.RetrieveBayar1(Criteria);
            string bgno = pettyCashOutFacade.RetrieveBayar2(Criteria);
            //int pettyCashOutID = pettyCashOut.ID;
            ArrayList arrBayarkertas = new ArrayList();
            arrBayarkertas = pettyCashOutFacade.RetrieveBayarExp(Criteria);
            if (pettyCashOutFacade.Error == string.Empty)
            {
                objRpt1 = new ReportDocument();
                crViewer.DisplayToolbar = true;

                objRpt1.Load(this.Server.MapPath("fakturbayarkertas.rpt"));

                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.DocNo, "PettyCashNo");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.TglBayar.ToString("dd/MM/yyyy"), "PettyCashDate");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.Penerima, "Kepada");
                SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(), "Total");
                SetCurrentValuesForParameterField(objRpt1, terSay + " Rupiah", "terbilang");
                crViewer.ReportSource = objRpt1;
                string nosj = string.Empty;
                foreach (BayarKertas BayarKertas in arrBayarkertas)
                {
                    x = x + 1;
                    if (BayarKertas.NoSJ.Trim() == string.Empty)
                        nosj = string.Empty;
                    else
                        nosj = "(" + BayarKertas.NoSJ.Trim() + ")";
                    SetCurrentValuesForParameterField(objRpt1, x.ToString(), "No" + x.ToString());
                    SetCurrentValuesForParameterField(objRpt1, nosj + BayarKertas.ItemName.Trim() + " (" + BayarKertas.Qty.ToString("N0") + ") @" + BayarKertas.Harga.ToString("N0"), "uraian" + x.ToString());
                    SetCurrentValuesForParameterField(objRpt1, BayarKertas.TotalHarga.ToString("N0"), "nilai" + x.ToString());
                }
            }
            if (x < 10)
            {
                x = x + 1;

                for (int i = x; i <= 10; i++)
                {
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "No" + i.ToString());
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "uraian" + i.ToString());
                    SetCurrentValuesForParameterField(objRpt1, string.Empty, "nilai" + i.ToString());
                }
            }
            SetCurrentValuesForParameterField(objRpt1, bgno, "uraian10");
        }
        public void ViewBayarExp2()
        {


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

            sqlCon.Open();
            

            if (Session["docno"] == null)
                return;

            int x = 0;
            string docno = Session["docno"].ToString();
            TerbilangFacade terbilangFacade = new TerbilangFacade();
            int total = Int32.Parse(Session["total"].ToString());
            string terSay = terbilangFacade.ConvertMoneyToWords(total);

            ArrayList arrBayar = new ArrayList();
            //PettyCashOutDetailFacade PettyCashOutDetailFacade = new PettyCashOutDetailFacade();
            //PettyCashOutDetail PettyCashOut = PettyCashOutDetailFacade.RetrieveTotalByNo(pettycashNo);

            decimal vTotal = decimal.Parse(Session["total"].ToString());
            DepoKertasKA dk = new DepoKertasKA();
            DepoKertasKA pettyCashOutFacade = new DepoKertasKA();
            string Criteria = " And DocNo = '" + docno + "'";
            BayarKertas pettyCashOut = pettyCashOutFacade.RetrieveBayar1(Criteria);
            string bgno = pettyCashOutFacade.RetrieveBayar2(Criteria);
            //int pettyCashOutID = pettyCashOut.ID;
            //ArrayList arrBayarkertas = new ArrayList();
            //arrBayarkertas = pettyCashOutFacade.RetrieveBayarExp(Criteria);
           string  strSQL = "SELECT isnull((select NoSJ from Deliverykertas where IDbeli=DelivBayarKertas.IDbeli and rowstatus>-1),'')nosj,itemname,qty,harga FROM DelivBayarKertas " +
                "where RowStatus>-1" + Criteria;
            if (pettyCashOutFacade.Error == string.Empty)
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("fakturbayarkertas2.rpt"));
                crViewer.DisplayToolbar = true;
                objRpt1.SetDataSource(dt);
                crViewer.DisplayToolbar = true;

                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.DocNo, "PettyCashNo");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.TglBayar.ToString("dd/MM/yyyy"), "PettyCashDate");
                SetCurrentValuesForParameterField(objRpt1, pettyCashOut.Penerima, "Kepada");
                SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(), "Total");
                SetCurrentValuesForParameterField(objRpt1, terSay + " Rupiah", "terbilang");
                crViewer.ReportSource = objRpt1;
                string nosj = string.Empty;
                //foreach (BayarKertas BayarKertas in arrBayarkertas)
                //{
                //    x = x + 1;
                //    if (BayarKertas.NoSJ.Trim() == string.Empty)
                //        nosj = string.Empty;
                //    else
                //        nosj = "(" + BayarKertas.NoSJ.Trim() + ")";
                //    SetCurrentValuesForParameterField(objRpt1, x.ToString(), "No" + x.ToString());
                //    SetCurrentValuesForParameterField(objRpt1, nosj + BayarKertas.ItemName.Trim() + " (" + BayarKertas.Qty.ToString("N0") + ") @" + BayarKertas.Harga.ToString("N0"), "uraian" + x.ToString());
                //    SetCurrentValuesForParameterField(objRpt1, BayarKertas.TotalHarga.ToString("N0"), "nilai" + x.ToString());
                //}
            }
            //if (x < 10)
            //{
            //    x = x + 1;

            //    for (int i = x; i <= 10; i++)
            //    {
            //        SetCurrentValuesForParameterField(objRpt1, string.Empty, "No" + i.ToString());
            //        SetCurrentValuesForParameterField(objRpt1, string.Empty, "uraian" + i.ToString());
            //        SetCurrentValuesForParameterField(objRpt1, string.Empty, "nilai" + i.ToString());
            //    }
            //}
            SetCurrentValuesForParameterField(objRpt1, bgno, "bgno");
            sqlCon.Close();
        }
        public void ViewPOPurchn3()
        {
            int strPOid = 0;

            if (Session["ID"] != null)
            {
                strPOid = Convert.ToInt32(Session["ID"]);
            }
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            string tempat = string.Empty;
            string formno = string.Empty;
            if (depoID == 7)
            {
                formno = "Form no. PUR/K/PO/07/02/R3";
                tempat = "Karawang, ";
            }
            else
            {
                formno = "Form no. PUR/PO/07/02/R3";
                tempat = "Citeureup, ";
            }
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
            int strCrc = pOPurchn.Crc;
            decimal strPPN = pOPurchn.PPN;
            decimal strPPH = pOPurchn.PPH;
            decimal strDiscount = pOPurchn.Disc;
            decimal totDiscount = 0;
            decimal subDiscount = 0;
            decimal subPPN = 0;
            decimal totPPN = 0;
            decimal subPPH = 0;
            decimal totPPH = 0;
            decimal grandTotal = 0;
            decimal grandTotal0 = 0;
            decimal grandTotal1 = 0;
            decimal grandTotal2 = 0;
            decimal grandTotal3 = 0;
            int strApproval = pOPurchn.Approval;

            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail pOPurchnDetail = pOPurchnDetailFacade.RetrieveTotalById(strPOid, ((Users)Session["Users"]).ViewPrice);
            string xAlamat1 = string.Empty;
            string xAlamat2 = string.Empty;
            CompanyFacade companyFacade = new CompanyFacade();
            //Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
            //xAlamat1 = company.Alamat1;
            //xAlamat2 = company.Alamat2;
            Company company = new Company();
            company = RetrieveCompanyByDepoID(users.UnitKerjaID);

            Company company1 = new Company();
            company1 = RetrieveCompany(pOPurchn.SubCompanyID);

            Company company0 = new Company();
            if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
                company0 = RetrieveCompany(company1.ID);
            else
                company0 = RetrieveCompanyByDepoID(users.UnitKerjaID);
            tempat = company.Lokasi.ToString();

            Company company2 = new Company();
            if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
                company2 = RetrieveCompany(company1.ID);
            else
                company2 = RetrieveCompanyByDepoID(users.UnitKerjaID);
            tempat = company.Lokasi.ToString();

            switch (depoID)
            {
                case 1:
                case 7:
                    xAlamat1 = "GRAHA GRC BOARD Lt. 3\n\r" +
                             "Jl. S. Parman kav. 64 Slipi Palmerah\n\r" +
                             "Jakarta 11410 - Indonesia\n\r" +
                             "Telp. (62-21) 53666800 (hunting)\n\r" +
                             "Fax. (62-21) 53666720\n\r";
                    xAlamat2 = ((Company)companyFacade.RetrieveByDepoId(depoID)).Alamat2.Replace(", ", "\n\r");

                    break;
                default:
                    xAlamat1 = company.Alamat1.Replace(", ", "\n\r"); ;
                    xAlamat2 = company.Alamat2.Replace(", ", "\n\r"); ;
                    break;
            }
            ReportFacade reportFacade = new ReportFacade();
            string strPODetail = reportFacade.ViewPOPurchn(strPOid, users.ViewPrice);

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strPODetail.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                Session["CetakPO"] = pOPurchn.Cetak;
                Session["CPOPurchn"] = pOPurchn;

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("POPurchnFax.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;


                //test loading image
                //            objRpt1.Database.Tables["Images"].SetDataSource(rptTest.ImageTable(System.Web.HttpContext.
                //Current.Request.MapPath("manager.bmp")));


                decimal vTotal = pOPurchnDetail.Total;

                MataUangFacade mataUangFacade = new MataUangFacade();
                MataUang mataUang = mataUangFacade.RetrieveById(strCrc);
                string nmUang = mataUang.Nama;
                string zLambang = mataUang.Lambang;
                string NamaRekening = string.Empty; string BankRekening = string.Empty; string NomorRekening = string.Empty;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select isnull(NamaRekening,'-')NamaRekening, isnull(BankRekening,'-')BankRekening, " +
                    "isnull(NomorRekening,'-')NomorRekening FROM SuppPurch where id=" + pOPurchn.SupplierID;
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        NamaRekening = sdr["NamaRekening"].ToString();
                        BankRekening = sdr["BankRekening"].ToString();
                        NomorRekening = sdr["NomorRekening"].ToString();
                    }
                }
                SetCurrentValuesForParameterField(objRpt1, BankRekening, "NamaBank");
                SetCurrentValuesForParameterField(objRpt1, NamaRekening, "NamaRekening");
                SetCurrentValuesForParameterField(objRpt1, NomorRekening, "NomorRekening");
                SetCurrentValuesForParameterField(objRpt1, formno, "formno");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang1");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang2");
                //SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang3");
                //SetCurrentValuesForParameterField(objRpt1, vTotal.ToString("N2"), "SubTotal");
                subDiscount = vTotal * strDiscount / 100;
                totDiscount = vTotal - subDiscount;
                grandTotal0 = totDiscount;
                //if (strDiscount == 0)
                if (grandTotal0 == vTotal)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "Discount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisDiscount");
                }
                else
                {
                    //subDiscount = vTotal * strDiscount / 100;
                    //totDiscount = vTotal - subDiscount;
                    SetCurrentValuesForParameterField(objRpt1, "Discount  ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, strDiscount.ToString("N2") + "%", "Discount");
                    SetCurrentValuesForParameterField(objRpt1, subDiscount.ToString("N3"), "SubDiscount");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, totDiscount.ToString("N3"), "TotalDiscount");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, "-----------------------------", "GarisDiscount");
                }
                subPPN = (grandTotal0 * strPPN / 100);
                totPPN = (grandTotal0 * strPPN / 100) + grandTotal0;
                grandTotal1 = totPPN;
                //if (strPPN == 0)
                if (grandTotal1 == grandTotal0)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                }
                else
                {
                    //if (strDiscount == 0)
                    //{
                    //    totPPN = vTotal * strPPN / 100;
                    //}
                    //else
                    //{
                    //    totPPN = totDiscount * strPPN / 100;
                    //}
                    SetCurrentValuesForParameterField(objRpt1, "PPN", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, strPPN.ToString("N0") + "%", "PPN");
                    SetCurrentValuesForParameterField(objRpt1, subPPN.ToString("N3"), "SubPPN");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5");
                    SetCurrentValuesForParameterField(objRpt1, totPPN.ToString("N3"), "totalPPN");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPN");
                }

                subPPH = (grandTotal0 * strPPH / 100);
                totPPH = (grandTotal0 * strPPH / 100) + grandTotal1;
                grandTotal2 = totPPH;
                //if (strPPN == 0)
                if (grandTotal2 == grandTotal1)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
                    //SetCurrentValuesForParameterField(objRpt1, " ", "totalPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN");
                }
                else
                {
                    //if (strDiscount == 0)
                    //{
                    //    totPPN = vTotal * strPPN / 100;
                    //}
                    //else
                    //{
                    //    totPPN = totDiscount * strPPN / 100;
                    //}
                    SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
                    SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
                    //SetCurrentValuesForParameterField(objRpt1, totPPN.ToString("N2"), "totalPPH");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");

                }

                // Perhitungan Grand Total
                grandTotal = grandTotal2;
                if (grandTotal == vTotal)
                {
                    //grandTotal = vTotal;
                    SetCurrentValuesForParameterField(objRpt1, "", "lambang3");
                    SetCurrentValuesForParameterField(objRpt1, "", "SubTotal");
                    SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang3");
                    SetCurrentValuesForParameterField(objRpt1, vTotal.ToString("{0:N3}"), "SubTotal");
                    SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                }

                //else
                //{
                //    if (strDiscount != 0 & strPPN == 0)
                //    {
                //        grandTotal = totDiscount;
                //         SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N2"), "GrandTotal");
                //         SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                //         SetCurrentValuesForParameterField(objRpt1, "================", "GarisAkhir");
                //         SetCurrentValuesForParameterField(objRpt1, "-----------------------------", "GarisPPN");
                //    }
                //    else
                //    {
                //        if (strDiscount == 0 & strPPN != 0)
                //        {
                //            grandTotal = vTotal + totPPN;
                //             SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N2"), "GrandTotal");
                //             SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                //             SetCurrentValuesForParameterField(objRpt1, "===================", "GarisAkhir");
                //             SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                //        }
                //        else
                //        {
                //            grandTotal = totDiscount + totPPN;
                //             SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N2"), "GrandTotal");
                //             SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                //             SetCurrentValuesForParameterField(objRpt1, "===================", "GarisAkhir");
                //             SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                //        }
                //    }
                //}

                TerbilangFacade terbilangFacade = new TerbilangFacade();
                string terSay = terbilangFacade.ConvertMoneyToWords(grandTotal);
                string Say = terbilangFacade.changeNumericToWords(Convert.ToDouble(grandTotal));
                SetCurrentValuesForParameterField(objRpt1, terSay + " " + nmUang, "Terbilang");
                SetCurrentValuesForParameterField(objRpt1, Say + " " + nmUang, "say");
                SetCurrentValuesForParameterField(objRpt1, strApproval.ToString("N0"), "cApproval");
                SetCurrentValuesForParameterField(objRpt1, depoID.ToString().Trim(), "depo");
                SetCurrentValuesForParameterField(objRpt1, tempat, "tempat");
                string pt = company0.Nama;
                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, company.Nama, "pt2");
                OtorFacade otorFacade = new OtorFacade();
                Otor otor = otorFacade.RetrieveByCompanyID(company.ID);
                if (otorFacade.Error == string.Empty)
                {
                    //foreach (Otor otor in arrOtor)
                    //{
                    SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
                    SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
                    SetCurrentValuesForParameterField(objRpt1, otor.NPWP, "NPWP");
                    SetCurrentValuesForParameterField(objRpt1, otor.SPPKP, "sppkp");

                    //}
                }
                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");
                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");
                if (pOPurchn.Remark.Trim().Length > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "Remark :", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, pOPurchn.Remark.Trim(), "Remark");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, "", "Remark");
                }

            }
            catch
            {

            }

        }
        private Company RetrieveCompany(int PlantID)
        {
            //webservice port 85
            Company cp = new Company();
            try
            {
                Global2 api = new Global2();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                DataSet da = api.GetDataTable("Company", "*", "Where ID=" + PlantID, "GRCBOARDPURCH");

                foreach (DataRow dr in da.Tables[0].Rows)
                {
                    cp.ID = int.Parse(dr["ID"].ToString());
                    cp.Nama = dr["Nama"].ToString().ToUpper();
                    cp.Alamat1 = dr["Alamat1"].ToString();
                    cp.Alamat2 = dr["Alamat2"].ToString();
                    cp.DepoID = int.Parse(dr["DepoID"].ToString());
                    cp.KodeLokasi = dr["KodeLokasi"].ToString();
                    cp.Lokasi = dr["Lokasi"].ToString();
                }

            }
            catch { }
            return cp;
        }
        private Company RetrieveCompanyByDepoID(int PlantID)
        {
            //webservice port 85
            Company cp = new Company();
            try
            {
                Global2 api = new Global2();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                DataSet da = api.GetDataTable("Company", "*", "Where DepoID=" + PlantID, "GRCBOARDPURCH");

                foreach (DataRow dr in da.Tables[0].Rows)
                {
                    cp.ID = int.Parse(dr["ID"].ToString());
                    cp.Nama = dr["Nama"].ToString().ToUpper();
                    cp.Alamat1 = dr["Alamat1"].ToString();
                    cp.Alamat2 = dr["Alamat2"].ToString();
                    cp.DepoID = int.Parse(dr["DepoID"].ToString());
                    cp.KodeLokasi = dr["KodeLokasi"].ToString();
                    cp.Lokasi = dr["Lokasi"].ToString();
                }

            }
            catch { }
            return cp;
        }

        public void ViewPOPurchn2()
        {
            int strPOid = 0;
            string JumlahDigit = DecimalFormat("PO");
            string JumlahDigit1 = DecimalFormat1("PO");
            if (Session["ID"] != null)
            {
                strPOid = Convert.ToInt32(Session["ID"]);
            }
            else
                return;
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            string tempat = string.Empty;
            string formno = string.Empty;
            if (depoID == 7)
            {
                formno = "Form no. PUR/K/PO/07/02/R3";
                tempat = "Karawang, ";
            }
            else
            {
                formno = "Form no. PUR/PO/07/02/R3";
                tempat = "Citeureup, ";
            }

            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
            int RevisiPO = pOPurchnFacade.GetPORevision(strPOid);
            int strCrc = pOPurchn.Crc;
            string createdby = pOPurchn.CreatedBy;
            //add by Razib
            decimal JumlahX = pOPurchnFacade.GetJumlah(strPOid);
            decimal PriceX = pOPurchnFacade.GetPriceX(strPOid);
            //end
            decimal strPPN = pOPurchn.PPN;
            decimal strPPH = pOPurchn.PPH;
            decimal strOngkos = pOPurchn.Ongkos;
            decimal strDiscount = pOPurchn.Disc;
            decimal totDiscount = 0;
            decimal subDiscount = 0;
            decimal subPPN = 0;
            decimal totPPN = 0;
            decimal subPPH = 0;
            decimal totPPH = 0;
            decimal totOngkos = 0;
            decimal grandTotal = 0;
            decimal grandTotal0 = 0;
            decimal grandTotal1 = 0;
            decimal grandTotal2 = 0;
            decimal grandTotal3 = 0;
            int subcompanyID = pOPurchn.SubCompanyID;
            //decimal NilaiKurs = 0;
            int strApproval = pOPurchn.Approval;

            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail pOPurchnDetail = pOPurchnDetailFacade.RetrieveTotalById(strPOid, ((Users)Session["Users"]).ViewPrice);

            ReportFacade reportFacade = new ReportFacade();
            string strPODetail = reportFacade.ViewPOPurchn(strPOid, users.ViewPrice);
            string xAlamat1 = string.Empty;
            string xAlamat2 = string.Empty;
            CompanyFacade companyFacade = new CompanyFacade();
            //Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
            /*
             * Data Kop nama perusahaan di printout sesuai dengan login masing2
             * diambil dari data server ho
             * added on 17-03-2016
             */
            Company company = new Company();
            company = RetrieveCompanyByDepoID(users.UnitKerjaID);

            Company company1 = new Company();
            company1 = RetrieveCompany(pOPurchn.SubCompanyID);

            //Company company2 = new Company();
            //company2 = RetrieveCompany(pOPurchn.SubCompanyID);

            Company company0 = new Company();

            if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
                company0 = RetrieveCompany(company1.ID);
            //else if (pOPurchn.SubCompanyID == company2.ID && pOPurchn.SubCompanyID > 0)
            //    company0 = RetrieveCompany(company2.ID);
            else
                company0 = RetrieveCompanyByDepoID(users.UnitKerjaID);

            tempat = company.Lokasi.ToString();
            switch (depoID)
            {
                case 13:
                case 1:
                case 7:
                    xAlamat1 = "GRAHA GRC BOARD Lt. 3\n\r" +
                             "Jl. S. Parman kav. 64 Slipi Palmerah\n\r" +
                             "Jakarta 11410 - Indonesia\n\r" +
                             "Telp. (62-21) 53666800 (hunting)\n\r" +
                             "Fax. (62-21) 53666720\n\r";
                    xAlamat2 = Global.UppercaseWords(((Company)companyFacade.RetrieveByDepoId(depoID)).Alamat2.Replace(", ", "\n\r"));

                    break;
                default:
                    xAlamat1 = company.Alamat1.Replace(", ", "\n\r"); ;
                    xAlamat2 = Global.UppercaseWords(company.Alamat2.Replace(", ", "\n\r"));
                    break;
            }
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strPODetail.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                Session["CetakPO"] = pOPurchn.Cetak;
                Session["CPOPurchn"] = pOPurchn;

                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("POPurchn.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = false;

                //test loading image
                //            objRpt1.Database.Tables["Images"].SetDataSource(rptTest.ImageTable(System.Web.HttpContext.
                //Current.Request.MapPath("manager.bmp")));

                string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
                int MataUang = pOPurchnDetail.RowStatus;
                int MU = (InputKurs == "Aktif" && MataUang == 0 && pOPurchnDetail.Price > 0) ? 1 : strCrc;
                decimal vTotal = pOPurchnDetail.Total;
                MataUangFacade mataUangFacade = new MataUangFacade();
                MataUang mataUang = mataUangFacade.RetrieveById(MU/*strCrc*/);
                string nmUang = mataUang.Nama;
                string zLambang = mataUang.Lambang;
                if (zLambang.ToUpper().Trim() == "RP")
                {
                    vTotal = GetSubtotal2Digit(strPOid);
                }
                string NamaRekening = string.Empty; string BankRekening = string.Empty; string NomorRekening = string.Empty;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select isnull(NamaRekening,'-')NamaRekening, isnull(BankRekening,'-')BankRekening, " +
                    "isnull(NomorRekening,'-')NomorRekening FROM SuppPurch where id=" + pOPurchn.SupplierID;
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        NamaRekening = sdr["NamaRekening"].ToString();
                        BankRekening = sdr["BankRekening"].ToString();
                        NomorRekening = sdr["NomorRekening"].ToString();
                    }
                }
                SetCurrentValuesForParameterField(objRpt1, BankRekening, "NamaBank");
                SetCurrentValuesForParameterField(objRpt1, NamaRekening, "NamaRekening");
                SetCurrentValuesForParameterField(objRpt1, NomorRekening, "NomorRekening");
                SetCurrentValuesForParameterField(objRpt1, formno, "formno");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang1");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang2");
                subDiscount = vTotal * strDiscount / 100;
                totDiscount = vTotal - subDiscount;
                grandTotal0 = totDiscount;
                if (zLambang.ToUpper().Trim() != "RP")
                {
                    SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString("N3"), "xJumlahx");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString(JumlahDigit), "xJumlahx");
                }
                if (grandTotal0 == vTotal)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "Discount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisDiscount");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "Discount  ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, strDiscount.ToString("N2") + "%", "Discount");
                    SetCurrentValuesForParameterField(objRpt1, subDiscount.ToString("N2"), "SubDiscount");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, totDiscount.ToString("N2"), "TotalDiscount");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, "-----------------------------", "GarisDiscount");
                }
                subPPN = (grandTotal0 * strPPN / 100);
                totPPN = (grandTotal0 * strPPN / 100) + grandTotal0;
                grandTotal1 = totPPN;
                if (grandTotal1 == grandTotal0)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    if (totDiscount == 0)
                        SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "PPN", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, strPPN.ToString("N0") + "%", "PPN");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPN.ToString("N3"), "SubPPN");
                        SetCurrentValuesForParameterField(objRpt1, totPPN.ToString("N3"), "totalPPN");

                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPN.ToString(JumlahDigit), "SubPPN");
                        SetCurrentValuesForParameterField(objRpt1, totPPN.ToString(JumlahDigit), "totalPPN");

                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPN");
                }

                //if (strPPH == 2)
                //{
                subPPH = (grandTotal0 * strPPH / 100);
                totPPH = grandTotal1 - (grandTotal0 * strPPH / 100);// +grandTotal1;
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    //    totPPH = grandTotal1;// +grandTotal1;
                                                                    //}
                grandTotal2 = totPPH;
                if (grandTotal2 == grandTotal1)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    if (totPPN == 0)
                    { SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN"); }
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");
                }
                if (strPPH > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
                    }
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");
                }
                if (RevisiPO > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "Revisi Ke : " + RevisiPO.ToString(), "Revision");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "Revision");
                }
                totOngkos = strOngkos + grandTotal2;
                grandTotal3 = totOngkos;
                if (grandTotal3 == grandTotal2)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblOngkos");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubOngkos");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang8");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotOngkos");
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "Ongkos kirim", "lblOngkos");
                    SetCurrentValuesForParameterField(objRpt1, strOngkos.ToString(JumlahDigit), "SubOngkos");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang8");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotOngkos");

                }

                // Perhitungan Grand Total
                grandTotal = grandTotal3;
                if (grandTotal == vTotal)
                {
                    //grandTotal = vTotal;
                    SetCurrentValuesForParameterField(objRpt1, "", "lambang3");
                    SetCurrentValuesForParameterField(objRpt1, "", "SubTotal");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang3");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, vTotal.ToString("N3"), "SubTotal");
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    }
                    else
                    {
                        vTotal = GetSubtotal2Digit(strPOid);
                        SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(JumlahDigit), "SubTotal");
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
                    }
                    //SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(JumlahDigit), "SubTotal");
                    //SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                }
                TerbilangFacade terbilangFacade = new TerbilangFacade();
                string terSay = terbilangFacade.ConvertMoneyToWords(grandTotal);
                string Say = terbilangFacade.changeNumericToWords(Convert.ToDouble(grandTotal));
                SetCurrentValuesForParameterField(objRpt1, terSay + " " + nmUang, "Terbilang");
                SetCurrentValuesForParameterField(objRpt1, Say + " " + nmUang, "say");
                SetCurrentValuesForParameterField(objRpt1, strApproval.ToString("N0"), "cApproval");
                SetCurrentValuesForParameterField(objRpt1, depoID.ToString().Trim(), "depo");
                SetCurrentValuesForParameterField(objRpt1, tempat, "tempat");

                if (pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "H" || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "M"
                     || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "G")
                {
                    if (company0.Nama.Length >= 23)
                    {
                        string pt1 = company0.Nama.Substring(0, 23); Session["pt1"] = pt1;
                    }

                    else if (company0.Nama.Length >= 20)
                    {
                        string pt1 = company0.Nama.Substring(0, 20); Session["pt1"] = pt1;
                    }
                }
                else if (pOPurchn.SubCompanyID == 0)
                {
                    if (company0.Nama.Length >= 31)
                    {
                        string pt1 = company0.Nama.Substring(0, 31); Session["pt1"] = pt1;
                    }
                }

                else if (pOPurchn.SubCompanyID == 8)
                {
                    string pt1 = company0.Nama.Trim(); Session["pt1"] = pt1;
                }
                else
                {
                    string pt1 = "PT BANGUNPERKASA ADHITAMASENTRA"; Session["pt1"] = pt1;
                }


                string pt = Session["pt1"].ToString().Trim();

                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, company.Nama, "pt2");
                /**
                 * data otorisasi / tanda tangan di PO
                 */
                OtorFacade otorFacade = new OtorFacade();
                //Otor otor = otorFacade.RetrieveByCompanyID(company1.ID);
                //Otor otor = otorFacade.RetrieveByCompanyID(company.ID);

                Otor otor2 = new Otor(); Otor otor = new Otor();
                if (pOPurchn.SubCompanyID == 8)
                {
                    //Otor otor = otorFacade.RetrieveByCompanyID(8);
                    otor2 = otorFacade.RetrieveByCompanyID(8);

                    if (otorFacade.Error == string.Empty)
                    {
                        //foreach (Otor otor in arrOtor)
                        //{
                        SetCurrentValuesForParameterField(objRpt1, otor2.Nama1, "Nama1");
                        SetCurrentValuesForParameterField(objRpt1, otor2.Nama3, "Nama3");
                        SetCurrentValuesForParameterField(objRpt1, otor2.NPWP, "NPWP");
                        SetCurrentValuesForParameterField(objRpt1, otor2.SPPKP, "sppkp");

                        //}
                    }
                }
                else
                {
                    otor = otorFacade.RetrieveByCompanyID(company.ID);
                    if (otorFacade.Error == string.Empty)
                    {
                        //foreach (Otor otor in arrOtor)
                        //{
                        SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
                        SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
                        SetCurrentValuesForParameterField(objRpt1, otor.NPWP, "NPWP");
                        SetCurrentValuesForParameterField(objRpt1, otor.SPPKP, "sppkp");

                        //}
                    }
                }

                //if (otorFacade.Error == string.Empty)
                //{
                //    //foreach (Otor otor in arrOtor)
                //    //{
                //    SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
                //    SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
                //    SetCurrentValuesForParameterField(objRpt1, otor2.NPWP, "NPWP");
                //    SetCurrentValuesForParameterField(objRpt1, otor2.SPPKP, "sppkp");

                //    //}
                //}
                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");
                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");
                if (pOPurchn.Remark.Trim().Length > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "Remark :", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, pOPurchn.Remark.Trim(), "Remark");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, "", "Remark");
                }
                /** Khusus untuk PO Kertas nama vendor
                 * mengambil nama perusahaan dari data HO
                 * added on 26-01-2016
                 */
                if (pOPurchn.SubCompanyID > 0 && SubCompanyAktif == "1")
                {
                    Company cmp = (Company)this.DetailCompany(pOPurchn.SubCompanyID);
                    SetCurrentValuesForParameterField(objRpt1, cmp.Nama.ToString().ToUpper(), "CoName");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Alamat1.ToString(), "CoAddress1");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Alamat2.ToString(), "CoAddress2");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Manager.ToString(), "CoUP");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoAddress1");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoAddress2");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoUP");
                }
            SetCurrentValuesForParameterField(objRpt1, createdby, "createdby");
            //}
            //catch
            //{

            //}
        }

        //public void ViewPOPurchn2()
        //{
        //    int strPOid = Convert.ToInt32(Request.QueryString["ID"].ToString());
        //    string JumlahDigit = DecimalFormat("PO");
        //    string JumlahDigit1 = DecimalFormat1("PO");
        //    //if (Session["ID"] != null)
        //    //{
        //    //    strPOid = Convert.ToInt32(Session["ID"]);
        //    //}
        //    Users users = (Users)Session["Users"];
        //    int depoID = users.UnitKerjaID;
        //    string tempat = string.Empty;
        //    string formno = string.Empty;
        //    if (depoID == 7)
        //    {
        //        formno = "Form no. PUR/K/PO/07/02/R3";
        //        tempat = "Karawang, ";
        //    }
        //    else
        //    {
        //        formno = "Form no. PUR/PO/07/02/R3";
        //        tempat = "Citeureup, ";
        //    }

        //    POPurchnFacade pOPurchnFacade = new POPurchnFacade();
        //    Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
        //    int RevisiPO = pOPurchnFacade.GetPORevision(strPOid);
        //    int strCrc = pOPurchn.Crc;
        //    //add by Razib
        //    decimal JumlahX = pOPurchnFacade.GetJumlah(strPOid);
        //    decimal PriceX = pOPurchnFacade.GetPriceX(strPOid);
        //    //end
        //    decimal strPPN = pOPurchn.PPN;
        //    decimal strPPH = pOPurchn.PPH;
        //    decimal strOngkos = pOPurchn.Ongkos;
        //    decimal strDiscount = pOPurchn.Disc;
        //    decimal totDiscount = 0;
        //    decimal subDiscount = 0;
        //    decimal subPPN = 0;
        //    decimal totPPN = 0;
        //    decimal subPPH = 0;
        //    decimal totPPH = 0;
        //    decimal totOngkos = 0;
        //    decimal grandTotal = 0;
        //    decimal grandTotal0 = 0;
        //    decimal grandTotal1 = 0;
        //    decimal grandTotal2 = 0;
        //    decimal grandTotal3 = 0;
        //    int subcompanyID = pOPurchn.SubCompanyID;
        //    //decimal NilaiKurs = 0;
        //    int strApproval = pOPurchn.Approval;

        //    POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
        //    POPurchnDetail pOPurchnDetail = pOPurchnDetailFacade.RetrieveTotalById(strPOid, ((Users)Session["Users"]).ViewPrice);

        //    ReportFacade reportFacade = new ReportFacade();
        //    string strPODetail = reportFacade.ViewPOPurchn(strPOid, users.ViewPrice);
        //    string xAlamat1 = string.Empty;
        //    string xAlamat2 = string.Empty;
        //    CompanyFacade companyFacade = new CompanyFacade();
        //    Company company = new Company();
        //    company = RetrieveCompanyByDepoID(users.UnitKerjaID);

        //    Company company1 = new Company();
        //    company1 = RetrieveCompany(pOPurchn.SubCompanyID);

        //    Company company0 = new Company();

        //    if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
        //        company0 = RetrieveCompany(company1.ID);
        //    else
        //        company0 = RetrieveCompanyByDepoID(users.UnitKerjaID);

        //    tempat = company.Lokasi.ToString();
        //    switch (depoID)
        //    {
        //        case 13:
        //        case 1:
        //        case 7:
        //            xAlamat1 = "GRAHA GRC BOARD Lt. 3\n\r" +
        //                     "Jl. S. Parman kav. 64 Slipi Palmerah\n\r" +
        //                     "Jakarta 11410 - Indonesia\n\r" +
        //                     "Telp. (62-21) 53666800 (hunting)\n\r" +
        //                     "Fax. (62-21) 53666720\n\r";
        //            xAlamat2 = Global.UppercaseWords(((Company)companyFacade.RetrieveByDepoId(depoID)).Alamat2.Replace(", ", "\n\r"));

        //            break;
        //        default:
        //            xAlamat1 = company.Alamat1.Replace(", ", "\n\r"); ;
        //            xAlamat2 = Global.UppercaseWords(company.Alamat2.Replace(", ", "\n\r"));
        //            break;
        //    }
        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //    try
        //    {
        //        sqlCon.Open();

        //        SqlDataAdapter da = new SqlDataAdapter(strPODetail.ToString(), sqlCon);

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);

        //        Session["CetakPO"] = pOPurchn.Cetak;
        //        Session["CPOPurchn"] = pOPurchn;

        //        objRpt1 = new ReportDocument();

        //        objRpt1.Load(this.Server.MapPath("RptPOPurchn.rpt"));
        //        objRpt1.SetDataSource(dt);
        //        crViewer.ReportSource = objRpt1;
        //        Panel1.Visible = true; crViewer.DisplayToolbar = false;

        //        string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
        //        int MataUang = pOPurchnDetail.RowStatus;
        //        int MU = (InputKurs == "Aktif" && MataUang == 0 && pOPurchnDetail.Price > 0) ? 1 : strCrc;
        //        decimal vTotal = pOPurchnDetail.Total;
        //        MataUangFacade mataUangFacade = new MataUangFacade();
        //        MataUang mataUang = mataUangFacade.RetrieveById(MU/*strCrc*/);
        //        string nmUang = mataUang.Nama;
        //        string zLambang = mataUang.Lambang;
        //        if (zLambang.ToUpper().Trim() == "RP")
        //        {
        //            vTotal = GetSubtotal2Digit(strPOid);
        //        }
        //        string NamaRekening = string.Empty; string BankRekening = string.Empty; string NomorRekening = string.Empty;
        //        ZetroView zl = new ZetroView();
        //        zl.QueryType = Operation.CUSTOM;
        //        zl.CustomQuery = "select isnull(NamaRekening,'-')NamaRekening, isnull(BankRekening,'-')BankRekening, " +
        //            "isnull(NomorRekening,'-')NomorRekening FROM SuppPurch where id=" + pOPurchn.SupplierID;
        //        SqlDataReader sdr = zl.Retrieve();
        //        if (sdr.HasRows)
        //        {
        //            while (sdr.Read())
        //            {
        //                NamaRekening = sdr["NamaRekening"].ToString();
        //                BankRekening = sdr["BankRekening"].ToString();
        //                NomorRekening = sdr["NomorRekening"].ToString();
        //            }
        //        }
        //        SetCurrentValuesForParameterField(objRpt1, BankRekening, "NamaBank");
        //        SetCurrentValuesForParameterField(objRpt1, NamaRekening, "NamaRekening");
        //        SetCurrentValuesForParameterField(objRpt1, NomorRekening, "NomorRekening");
        //        SetCurrentValuesForParameterField(objRpt1, formno, "formno");
        //        SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang1");
        //        SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang2");
        //        subDiscount = vTotal * strDiscount / 100;
        //        totDiscount = vTotal - subDiscount;
        //        grandTotal0 = totDiscount;
        //        if (zLambang.ToUpper().Trim() != "RP")
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString("N3"), "xJumlahx");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString(JumlahDigit), "xJumlahx");
        //        }
        //        if (grandTotal0 == vTotal)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lblDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "Discount");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "SubDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang4");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "dotDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "GarisDiscount");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "Discount  ", "lblDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, strDiscount.ToString("N2") + "%", "Discount");
        //            SetCurrentValuesForParameterField(objRpt1, subDiscount.ToString("N2"), "SubDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4a");
        //            SetCurrentValuesForParameterField(objRpt1, totDiscount.ToString("N2"), "TotalDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotDiscount");
        //            SetCurrentValuesForParameterField(objRpt1, "-----------------------------", "GarisDiscount");
        //        }
        //        subPPN = (grandTotal0 * strPPN / 100);
        //        totPPN = (grandTotal0 * strPPN / 100) + grandTotal0;
        //        grandTotal1 = totPPN;
        //        if (grandTotal1 == grandTotal0)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lblPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "PPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "SubPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang5");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "dotPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
        //            if (totDiscount == 0)
        //                SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
        //        }
        //        else
        //        {

        //            SetCurrentValuesForParameterField(objRpt1, "PPN", "lblPPN");
        //            SetCurrentValuesForParameterField(objRpt1, strPPN.ToString("N0") + "%", "PPN");
        //            if (zLambang.ToUpper().Trim() != "RP")
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPN.ToString("N3"), "SubPPN");
        //                SetCurrentValuesForParameterField(objRpt1, totPPN.ToString("N3"), "totalPPN");

        //            }
        //            else
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPN.ToString(JumlahDigit), "SubPPN");
        //                SetCurrentValuesForParameterField(objRpt1, totPPN.ToString(JumlahDigit), "totalPPN");

        //            }

        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5a");
        //            SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPN");
        //        }

        //        subPPH = (grandTotal0 * strPPH / 100);
        //        totPPH = grandTotal1 - (grandTotal0 * strPPH / 100);// +grandTotal1;
        //        grandTotal2 = totPPH;
        //        if (grandTotal2 == grandTotal1)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lblPPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "PPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "totalPPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang7");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "dotPPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPH");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
        //            if (totPPN == 0)
        //            { SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN"); }
        //        }
        //        else
        //        {

        //            SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
        //            SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
        //            if (zLambang.ToUpper().Trim() != "RP")
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
        //                SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
        //            }
        //            else
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
        //                SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
        //            }

        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
        //            SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
        //            SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");
        //        }
        //        if (strPPH > 0)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
        //            SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
        //            if (zLambang.ToUpper().Trim() != "RP")
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
        //                SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
        //            }
        //            else
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
        //                SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
        //            }
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
        //            SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
        //            SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");
        //        }
        //        if (RevisiPO > 0)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "Revisi Ke : " + RevisiPO.ToString(), "Revision");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "", "Revision");
        //        }
        //        totOngkos = strOngkos + grandTotal2;
        //        grandTotal3 = totOngkos;
        //        if (grandTotal3 == grandTotal2)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lblOngkos");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "SubOngkos");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang8");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "dotOngkos");
        //        }
        //        else
        //        {

        //            SetCurrentValuesForParameterField(objRpt1, "Ongkos kirim", "lblOngkos");
        //            SetCurrentValuesForParameterField(objRpt1, strOngkos.ToString(JumlahDigit), "SubOngkos");
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang8");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
        //            SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotOngkos");

        //        }

        //        // Perhitungan Grand Total
        //        grandTotal = grandTotal3;
        //        if (grandTotal == vTotal)
        //        {
        //            //grandTotal = vTotal;
        //            SetCurrentValuesForParameterField(objRpt1, "", "lambang3");
        //            SetCurrentValuesForParameterField(objRpt1, "", "SubTotal");
        //            if (zLambang.ToUpper().Trim() != "RP")
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
        //            }
        //            else
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
        //            }

        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
        //            SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
        //            SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
        //            SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang3");
        //            if (zLambang.ToUpper().Trim() != "RP")
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, vTotal.ToString("N3"), "SubTotal");
        //                SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
        //            }
        //            else
        //            {
        //                vTotal = GetSubtotal2Digit(strPOid);
        //                SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(JumlahDigit), "SubTotal");
        //                SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
        //            }
        //            SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
        //            SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
        //            SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
        //        }
        //        TerbilangFacade terbilangFacade = new TerbilangFacade();
        //        string terSay = terbilangFacade.ConvertMoneyToWords(grandTotal);
        //        string Say = terbilangFacade.changeNumericToWords(Convert.ToDouble(grandTotal));
        //        SetCurrentValuesForParameterField(objRpt1, terSay + " " + nmUang, "Terbilang");
        //        SetCurrentValuesForParameterField(objRpt1, Say + " " + nmUang, "say");
        //        SetCurrentValuesForParameterField(objRpt1, strApproval.ToString("N0"), "cApproval");
        //        SetCurrentValuesForParameterField(objRpt1, depoID.ToString().Trim(), "depo");
        //        SetCurrentValuesForParameterField(objRpt1, tempat, "tempat");

        //        if (pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "H" || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "M"
        //             || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "G")
        //        {
        //            if (company0.Nama.Length >= 23)
        //            {
        //                string pt1 = company0.Nama.Substring(0, 23); Session["pt1"] = pt1;
        //            }

        //            else if (company0.Nama.Length >= 20)
        //            {
        //                string pt1 = company0.Nama.Substring(0, 20); Session["pt1"] = pt1;
        //            }
        //        }
        //        else if (pOPurchn.SubCompanyID == 0)
        //        {
        //            if (company0.Nama.Length >= 31)
        //            {
        //                string pt1 = company0.Nama.Substring(0, 31); Session["pt1"] = pt1;
        //            }
        //        }

        //        else if (pOPurchn.SubCompanyID == 8)
        //        {
        //            string pt1 = company0.Nama.Trim(); Session["pt1"] = pt1;
        //        }
        //        else
        //        {
        //            string pt1 = "PT BANGUNPERKASA ADHITAMASENTRA"; Session["pt1"] = pt1;
        //        }


        //        string pt = Session["pt1"].ToString().Trim();

        //        SetCurrentValuesForParameterField(objRpt1, pt, "pt");
        //        SetCurrentValuesForParameterField(objRpt1, company.Nama, "pt2");
        //        /**
        //         * data otorisasi / tanda tangan di PO
        //         */
        //        OtorFacade otorFacade = new OtorFacade();

        //        Otor otor2 = new Otor(); Otor otor = new Otor();
        //        if (pOPurchn.SubCompanyID == 8)
        //        {
        //            otor2 = otorFacade.RetrieveByCompanyID(8);

        //            if (otorFacade.Error == string.Empty)
        //            {
        //                SetCurrentValuesForParameterField(objRpt1, otor2.Nama1, "Nama1");
        //                SetCurrentValuesForParameterField(objRpt1, otor2.Nama3, "Nama3");
        //                SetCurrentValuesForParameterField(objRpt1, otor2.NPWP, "NPWP");
        //                SetCurrentValuesForParameterField(objRpt1, otor2.SPPKP, "sppkp");
        //            }
        //        }
        //        else
        //        {
        //            otor = otorFacade.RetrieveByCompanyID(company.ID);
        //            if (otorFacade.Error == string.Empty)
        //            {
        //                //foreach (Otor otor in arrOtor)
        //                //{
        //                SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
        //                SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
        //                SetCurrentValuesForParameterField(objRpt1, otor.NPWP, "NPWP");
        //                SetCurrentValuesForParameterField(objRpt1, otor.SPPKP, "sppkp");

        //                //}
        //            }
        //        }

        //        SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");
        //        SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");
        //        if (pOPurchn.Remark.Trim().Length > 0)
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "Remark :", "LblRemark");
        //            SetCurrentValuesForParameterField(objRpt1, pOPurchn.Remark.Trim(), "Remark");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "", "LblRemark");
        //            SetCurrentValuesForParameterField(objRpt1, "", "Remark");
        //        }
        //        /** Khusus untuk PO Kertas nama vendor
        //         * mengambil nama perusahaan dari data HO
        //         * added on 26-01-2016
        //         */
        //        if (pOPurchn.SubCompanyID > 0 && SubCompanyAktif == "1")
        //        {
        //            Company cmp = (Company)this.DetailCompany(pOPurchn.SubCompanyID);
        //            SetCurrentValuesForParameterField(objRpt1, cmp.Nama.ToString().ToUpper(), "CoName");
        //            SetCurrentValuesForParameterField(objRpt1, cmp.Alamat1.ToString(), "CoAddress1");
        //            SetCurrentValuesForParameterField(objRpt1, cmp.Alamat2.ToString(), "CoAddress2");
        //            SetCurrentValuesForParameterField(objRpt1, cmp.Manager.ToString(), "CoUP");
        //        }
        //        else
        //        {
        //            SetCurrentValuesForParameterField(objRpt1, "", "CoName");
        //            SetCurrentValuesForParameterField(objRpt1, "", "CoAddress1");
        //            SetCurrentValuesForParameterField(objRpt1, "", "CoAddress2");
        //            SetCurrentValuesForParameterField(objRpt1, "", "CoUP");
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        public decimal GetSubtotal2Digit(int id)
        {
            decimal result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select  sum(cast(price as decimal(18,2)) * cast (qty as decimal(18,2)) )  * (select case when isnull(nilaikurs,0)=0 then 1 " +
                "else nilaikurs end from POPurchn where id=" + id + ") jumlah from POPurchnDetail where status>-1 and POID =" + id;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["jumlah"].ToString());
                }
            }
            return result;
        }
        public void ViewPOPurchnForFax()
        {
            int strPOid = 0;
            string JumlahDigit = DecimalFormat("PO");
            //Add by Razib
            string JumlahDigit1 = DecimalFormat1("PO");
            if (Session["ID"] != null)
            {
                strPOid = Convert.ToInt32(Session["ID"]);
            }
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            string tempat = string.Empty;
            string formno = string.Empty;
            if (depoID == 7)
            {
                formno = "Form no. PUR/K/PO/07/02/R3";
                tempat = "Karawang, ";
            }
            else
            {
                formno = "Form no. PUR/PO/07/02/R3";
                tempat = "Citeureup, ";
            }
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
            int RevisiPO = pOPurchnFacade.GetPORevision(strPOid);
            string createdby = pOPurchn.CreatedBy;
            //add by Razib
            decimal JumlahX = pOPurchnFacade.GetJumlah(strPOid);
            decimal PriceX = pOPurchnFacade.GetPriceX(strPOid);
            //end
            int strCrc = pOPurchn.Crc;
            decimal strPPN = pOPurchn.PPN;
            decimal strPPH = pOPurchn.PPH;
            decimal strDiscount = pOPurchn.Disc;
            decimal totDiscount = 0;
            decimal subDiscount = 0;
            decimal subPPN = 0;
            decimal totPPN = 0;
            decimal subPPH = 0;
            decimal totPPH = 0;
            decimal grandTotal = 0;
            decimal grandTotal0 = 0;
            decimal grandTotal1 = 0;
            decimal grandTotal2 = 0;
            decimal grandTotal3 = 0;
            decimal strOngkos = pOPurchn.Ongkos;
            decimal totOngkos = 0;
            int strApproval = pOPurchn.Approval;

            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail pOPurchnDetail = pOPurchnDetailFacade.RetrieveTotalById(strPOid, ((Users)Session["Users"]).ViewPrice);

            string xAlamat1 = string.Empty;
            string xAlamat2 = string.Empty;
            CompanyFacade companyFacade = new CompanyFacade();
            //Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
            Company company = new Company();
            company = RetrieveCompanyByDepoID(users.UnitKerjaID);

            Company company1 = new Company();
            company1 = RetrieveCompany(pOPurchn.SubCompanyID);

            Company company0 = new Company();
            if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
                company0 = RetrieveCompany(company1.ID);
            else
                company0 = RetrieveCompanyByDepoID(users.UnitKerjaID);

            Company company2 = new Company();
            if (pOPurchn.SubCompanyID == company1.ID && pOPurchn.SubCompanyID > 0)
                company2 = RetrieveCompany(company1.ID);

            else
                company2 = RetrieveCompanyByDepoID(users.UnitKerjaID);

            tempat = company.Lokasi.ToString();
            tempat = company.Lokasi.ToString();
            tempat = company.Lokasi.ToString();
            switch (depoID)
            {
                case 13:
                case 1:
                case 7:
                    xAlamat1 = "GRAHA GRC BOARD Lt. 3\n\r" +
                             "Jl. S. Parman kav. 64 Slipi Palmerah\n\r" +
                             "Jakarta 11410 - Indonesia\n\r" +
                             "Telp. (62-21) 53666800 (hunting)\n\r" +
                             "Fax. (62-21) 53666720\n\r";
                    xAlamat2 = Global.UppercaseWords(((Company)companyFacade.RetrieveByDepoId(depoID)).Alamat2.Replace(", ", "\n\r"));

                    break;
                default:
                    xAlamat1 = company.Alamat1.Replace(", ", "\n\r");
                    xAlamat2 = Global.UppercaseWords(company.Alamat2.Replace(", ", "\n\r"));
                    break;
            }

            ReportFacade reportFacade = new ReportFacade();
            string strPODetail = reportFacade.ViewPOPurchn(strPOid, users.ViewPrice);

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strPODetail.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);

                Session["CetakPO"] = pOPurchn.Cetak;
                Session["CPOPurchn"] = pOPurchn;
                Session["NoPO"] = pOPurchn.NoPO.ToString();
                objRpt1 = new ReportDocument();

                objRpt1.Load(this.Server.MapPath("POPurchnFax.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                crViewer.HasPrintButton = false;
                crViewer.HasToggleGroupTreeButton = false;
                crViewer.HasCrystalLogo = false;
                //test loading image
                //            objRpt1.Database.Tables["Images"].SetDataSource(rptTest.ImageTable(System.Web.HttpContext.
                //Current.Request.MapPath("manager.bmp")));
                /*
                 * Added for acomadate e-spt online
                 */
                string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
                int MataUang = pOPurchnDetail.RowStatus;
                int MU = (InputKurs == "Aktif" && MataUang == 0 && pOPurchnDetail.Price > 0) ? 1 : strCrc;
                decimal vTotal = pOPurchnDetail.Total;

                MataUangFacade mataUangFacade = new MataUangFacade();
                MataUang mataUang = mataUangFacade.RetrieveById(MU/*strCrc*/);


                //decimal vTotal = pOPurchnDetail.Total;

                //MataUangFacade mataUangFacade = new MataUangFacade();
                //MataUang mataUang = mataUangFacade.RetrieveById(strCrc);
                string nmUang = mataUang.Nama;
                string zLambang = mataUang.Lambang;
                string NamaRekening = string.Empty; string BankRekening = string.Empty; string NomorRekening = string.Empty;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select isnull(NamaRekening,'-')NamaRekening, isnull(BankRekening,'-')BankRekening, " +
                    "isnull(NomorRekening,'-')NomorRekening FROM SuppPurch where id=" + pOPurchn.SupplierID;
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        NamaRekening = sdr["NamaRekening"].ToString();
                        BankRekening = sdr["BankRekening"].ToString();
                        NomorRekening = sdr["NomorRekening"].ToString();
                    }
                }
                SetCurrentValuesForParameterField(objRpt1, BankRekening, "NamaBank");
                SetCurrentValuesForParameterField(objRpt1, NamaRekening, "NamaRekening");
                SetCurrentValuesForParameterField(objRpt1, NomorRekening, "NomorRekening");

                SetCurrentValuesForParameterField(objRpt1, formno, "formno");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang1");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang2");
                SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang3");
                //start add By Razib
                //SetCurrentValuesForParameterField(objRpt1, pOPurchnDetail.Price.ToString("N2"), "strPrice");
                if (zLambang.ToUpper().Trim() == "RP")
                {
                    vTotal = GetSubtotal2Digit(strPOid);
                }
                if (zLambang.ToUpper().Trim() != "RP")
                {
                    SetCurrentValuesForParameterField(objRpt1, vTotal.ToString("N3"), "SubTotal");
                    SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString("N3"), "Jumlahx");
                    //SetCurrentValuesForParameterField(objRpt1, PriceX.ToString(JumlahDigit1), "PriceX");
                }
                else
                {
                    vTotal = GetSubtotal2Digit(strPOid);
                    SetCurrentValuesForParameterField(objRpt1, vTotal.ToString(JumlahDigit), "SubTotal");
                    SetCurrentValuesForParameterField(objRpt1, JumlahX.ToString(JumlahDigit), "Jumlahx");
                    //SetCurrentValuesForParameterField(objRpt1, PriceX.ToString(JumlahDigit), "PriceX");
                }
                //end
                if (RevisiPO > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "Revisi Ke : " + RevisiPO.ToString(), "Revision");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "Revision");
                }
                subDiscount = vTotal * strDiscount / 100;
                totDiscount = vTotal - subDiscount;
                grandTotal0 = totDiscount;
                if (grandTotal0 == vTotal)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "Discount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisDiscount");
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "Discount  ", "lblDiscount");
                    SetCurrentValuesForParameterField(objRpt1, strDiscount.ToString("N2") + "%", "Discount");
                    //Start -- Add By Razib
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subDiscount.ToString("N3"), "SubDiscount");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subDiscount.ToString(JumlahDigit), "SubDiscount");
                    }
                    //End
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang4a");
                    //Start Add By Razib
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, totDiscount.ToString("N3"), "TotalDiscount");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, totDiscount.ToString(JumlahDigit), "TotalDiscount");
                    }
                    //End
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotDiscount");
                    SetCurrentValuesForParameterField(objRpt1, "-----------------------------", "GarisDiscount");
                }
                if (strPPH > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, strPPH.ToString("N2") + "%", "PPH");
                    //Start Add By Razib
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
                    }

                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");
                }
                subPPN = (grandTotal0 * strPPN / 100);
                totPPN = (grandTotal0 * strPPN / 100) + grandTotal0;
                grandTotal1 = totPPN;
                if (grandTotal1 == grandTotal0)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPN");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang4a");
                    if (totDiscount == 0)
                        SetCurrentValuesForParameterField(objRpt1, " ", "TotalDiscount");
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "PPN", "lblPPN");
                    SetCurrentValuesForParameterField(objRpt1, strPPN.ToString("N0") + "%", "PPN");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPN.ToString("N3"), "SubPPN");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPN.ToString(JumlahDigit), "SubPPN");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, totPPN.ToString("N3"), "totalPPN");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, totPPN.ToString(JumlahDigit), "totalPPN");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang5a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPN");
                }
                //if (strPPH == 2)
                //{
                subPPH = (grandTotal0 * strPPH / 100);
                totPPH = grandTotal1 - (grandTotal0 * strPPH / 100);// +grandTotal1;
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    //    totPPH = grandTotal1 ;// +grandTotal1;
                                                                    //}

                grandTotal2 = totPPH;
                if (grandTotal2 == grandTotal1)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "PPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "totalPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPH");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang5a");
                    if (totPPN == 0)
                    { SetCurrentValuesForParameterField(objRpt1, " ", "totalPPN"); }
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "PPH", "lblPPH");
                    SetCurrentValuesForParameterField(objRpt1, strPPH.ToString(JumlahDigit) + "%", "PPH");
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString("N3"), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString("N3"), "totalPPH");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, subPPH.ToString(JumlahDigit), "SubPPH");
                        SetCurrentValuesForParameterField(objRpt1, totPPH.ToString(JumlahDigit), "totalPPH");
                    }

                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotPPH");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPH");

                }
                totOngkos = strOngkos + grandTotal2;
                grandTotal3 = totOngkos;
                if (grandTotal3 == grandTotal2)
                {
                    SetCurrentValuesForParameterField(objRpt1, " ", "lblOngkos");
                    SetCurrentValuesForParameterField(objRpt1, " ", "SubOngkos");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang8");
                    SetCurrentValuesForParameterField(objRpt1, " ", "dotOngkos");
                }
                else
                {

                    SetCurrentValuesForParameterField(objRpt1, "Ongkos kirim", "lblOngkos");
                    SetCurrentValuesForParameterField(objRpt1, strOngkos.ToString(JumlahDigit), "SubOngkos");
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang8");
                    SetCurrentValuesForParameterField(objRpt1, " ", "lambang7a");
                    SetCurrentValuesForParameterField(objRpt1, "...............................................................", "dotOngkos");

                }

                // Perhitungan Grand Total
                grandTotal = grandTotal3;
                if (grandTotal == vTotal)
                {
                    //grandTotal = vTotal;
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
                    }
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, " ", "GarisPPN");
                }
                else
                {
                    if (zLambang.ToUpper().Trim() != "RP")
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString("N3"), "GrandTotal");
                    }
                    else
                    {
                        SetCurrentValuesForParameterField(objRpt1, grandTotal.ToString(JumlahDigit), "GrandTotal");
                    }
                    SetCurrentValuesForParameterField(objRpt1, zLambang, "lambang6");
                    SetCurrentValuesForParameterField(objRpt1, "====================", "GarisAkhir");
                    SetCurrentValuesForParameterField(objRpt1, "----------------------------", "GarisPPN");
                }
                TerbilangFacade terbilangFacade = new TerbilangFacade();
                string terSay = terbilangFacade.ConvertMoneyToWords(grandTotal);
                string Say = terbilangFacade.changeNumericToWords(Convert.ToDouble(grandTotal));
                SetCurrentValuesForParameterField(objRpt1, terSay + " " + nmUang, "Terbilang");
                SetCurrentValuesForParameterField(objRpt1, Say + " " + nmUang, "say");
                SetCurrentValuesForParameterField(objRpt1, strApproval.ToString("N0"), "cApproval");
                SetCurrentValuesForParameterField(objRpt1, depoID.ToString().Trim(), "depo");
                SetCurrentValuesForParameterField(objRpt1, tempat, "tempat");

                //string pt = company0.Nama.Substring(0,31);


                if (pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "H" || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "M"
                     || pOPurchn.SubCompanyID == 0 && pOPurchn.NoPO.Substring(0, 1) == "G")
                {
                    if (company0.Nama.Length >= 23)
                    {
                        string pt1 = company0.Nama.Substring(0, 23); Session["pt1"] = pt1;
                    }
                    else if (company0.Nama.Length >= 20)
                    {
                        string pt1 = company0.Nama.Substring(0, 20); Session["pt1"] = pt1;
                    }
                }
                else if (pOPurchn.SubCompanyID == 0)
                {
                    if (company0.Nama.Length >= 31)
                    {
                        string pt1 = company0.Nama.Substring(0, 31); Session["pt1"] = pt1;
                    }
                }
                else if (pOPurchn.SubCompanyID == 8)
                {
                    string pt1 = company0.Nama.Trim(); Session["pt1"] = pt1;
                }
                else
                {
                    string pt1 = "PT BANGUNPERKASA ADHITAMASENTRA"; Session["pt1"] = pt1;
                }


                string pt = Session["pt1"].ToString().Trim();


                SetCurrentValuesForParameterField(objRpt1, pt, "pt");
                SetCurrentValuesForParameterField(objRpt1, company.Nama, "pt2");
                OtorFacade otorFacade = new OtorFacade();
                Otor otor2 = new Otor(); Otor otor = new Otor();

                //if (pOPurchn.SubCompanyID == 8)
                //{
                //    //Otor otor = otorFacade.RetrieveByCompanyID(8);
                //    otor2 = otorFacade.RetrieveByCompanyID(8);
                //}
                //else
                //{
                //    otor = otorFacade.RetrieveByCompanyID(company.ID);
                //}

                //if (otorFacade.Error == string.Empty)
                //{
                //    //foreach (Otor otor in arrOtor)
                //    //{
                //    SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
                //    SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
                //    SetCurrentValuesForParameterField(objRpt1, otor2.NPWP, "NPWP");
                //    SetCurrentValuesForParameterField(objRpt1, otor2.SPPKP, "sppkp");

                //    //}
                //}

                if (pOPurchn.SubCompanyID == 8)
                {
                    //Otor otor = otorFacade.RetrieveByCompanyID(8);
                    otor2 = otorFacade.RetrieveByCompanyID(8);

                    if (otorFacade.Error == string.Empty)
                    {
                        //foreach (Otor otor in arrOtor)
                        //{
                        SetCurrentValuesForParameterField(objRpt1, otor2.Nama1, "Nama1");
                        SetCurrentValuesForParameterField(objRpt1, otor2.Nama3, "Nama3");
                        SetCurrentValuesForParameterField(objRpt1, otor2.NPWP, "NPWP");
                        SetCurrentValuesForParameterField(objRpt1, otor2.SPPKP, "sppkp");

                        //}
                    }
                }
                else
                {
                    otor = otorFacade.RetrieveByCompanyID(company.ID);
                    if (otorFacade.Error == string.Empty)
                    {
                        //foreach (Otor otor in arrOtor)
                        //{
                        SetCurrentValuesForParameterField(objRpt1, otor.Nama1, "Nama1");
                        SetCurrentValuesForParameterField(objRpt1, otor.Nama3, "Nama3");
                        SetCurrentValuesForParameterField(objRpt1, otor.NPWP, "NPWP");
                        SetCurrentValuesForParameterField(objRpt1, otor.SPPKP, "sppkp");

                        //}
                    }
                }

                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");
                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");
                if (pOPurchn.Remark.Trim().Length > 0)
                {
                    SetCurrentValuesForParameterField(objRpt1, "Remark :", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, pOPurchn.Remark.Trim(), "Remark");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "LblRemark");
                    SetCurrentValuesForParameterField(objRpt1, "", "Remark");
                }
                /** Khusus untuk PO Kertas
                 * mengambil nama perusahaan dari data HO
                 * added on 26-01-2016
                 */
                if (pOPurchn.SubCompanyID > 0 && SubCompanyAktif == "1")
                {
                    Company cmp = (Company)this.DetailCompany(pOPurchn.SubCompanyID);
                    SetCurrentValuesForParameterField(objRpt1, cmp.Nama.ToString().ToUpper(), "CoName");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Alamat1.ToString(), "CoAddress1");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Alamat2.ToString(), "CoAddress2");
                    SetCurrentValuesForParameterField(objRpt1, cmp.Manager.ToString(), "CoUP");
                }
                else
                {
                    SetCurrentValuesForParameterField(objRpt1, "", "CoName");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoAddress1");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoAddress2");
                    SetCurrentValuesForParameterField(objRpt1, "", "CoUP");
                }
                SetCurrentValuesForParameterField(objRpt1, createdby , "createdby");
            }
            catch
            {

            }

        }
        private string DecimalFormat(string DocTipe)
        {
            string digit = "N3";
            var cfg = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini"));
            digit = cfg.Read("JumlahDigit", DocTipe).ToString();
            return digit;
        }
        //add By Razib -
        private string DecimalFormat1(string DocTipe)
        {
            string digit = "N3";
            var cfg = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini"));
            digit = cfg.Read("JumlahDigit1", DocTipe).ToString();
            return digit;
        }

        public void ViewTawar()
        {
            int strTawarId = 0;
            if (Session["TawarID"] != null)
            {
                strTawarId = Convert.ToInt32(Session["TawarID"]);
            }
            string xKsie = string.Empty;
            string xLokasi = string.Empty;
            string xAlamat1 = string.Empty;
            string xAlamat2 = string.Empty;
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveByDepoId(users.UnitKerjaID);
            if (companyFacade.Error == string.Empty && company.ID > 0)
            {
                xLokasi = company.Lokasi;
                xAlamat1 = company.Alamat1;
                xAlamat2 = company.Alamat2;
            }
            ReportFacade reportFacade = new ReportFacade();
            string strTawarDetail = reportFacade.ViewTawarReport(strTawarId);

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();

                SqlDataAdapter da = new SqlDataAdapter(strTawarDetail.ToString(), sqlCon);

                DataTable dt = new DataTable();

                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("Inquiry.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, xAlamat1, "alamat1");
                SetCurrentValuesForParameterField(objRpt1, xAlamat2, "alamat2");
                SetCurrentValuesForParameterField(objRpt1, xLokasi, "Lokasi");
            }
            catch
            {

            }
        }

        public void ViewHistPO()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("HistPO.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                crViewer.DisplayGroupTree = false;
            }
            catch
            {
            }
        }
        public void ViewLapImpDetail()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapImprovementDetail.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                crViewer.DisplayGroupTree = false;
                SetCurrentValuesForParameterField(objRpt1, Session["Tahun"].ToString(), "Tahun");
                SetCurrentValuesForParameterField(objRpt1, Session["Bulan"].ToString(), "Bulan");
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString().ToUpper(), "Plant");
            }
            catch
            {
            }
        }
        public void ViewLapImpRekap()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapImprovementRekap.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                crViewer.DisplayGroupTree = false;
                SetCurrentValuesForParameterField(objRpt1, Session["Plant"].ToString().ToUpper(), "Plant");
            }
            catch
            {
            }
        }

        public void ViewHistPOExcel()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                //string Path = "D:\\ExportExcel\\reportEcel" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls";
                MemoryStream oStream;
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("HistPO.rpt"));
                objRpt1.SetDataSource(dt);
                //crViewer.ReportSource = objRpt1;
                //crViewer.RefreshReport();
                objRpt1.Refresh();

                oStream = (MemoryStream)objRpt1.ExportToStream(ExportFormatType.Excel);
                //Response.ContentType = "application/vnd.ms-excel";
                //try
                //{
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                try
                {
                    Response.BinaryWrite(oStream.ToArray());
                    //Response.Redirect("HistPO.aspx", false);
                }
                catch
                {

                }

                // objRpt1.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
            }
        }
        private void print_kendaraan()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            ArrayList arrArmada = new ArrayList();
            crViewer.DisplayToolbar = true;
            crViewer.SeparatePages = true;
            crViewer.ShowAllPageIds = true;
            arrArmada = (ArrayList)Session["Data"];
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("NoPol", typeof(string)));
            dt.Columns.Add(new DataColumn("SPBNo", typeof(string)));
            dt.Columns.Add(new DataColumn("AvgPrice", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
            if (arrArmada.Count > 0)
            {
                foreach (MTC_Armada da in arrArmada)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = da.ID;
                    dr["NoPol"] = da.NoPol;
                    dr["SPBNo"] = da.SPBNo;
                    dr["AvgPrice"] = Convert.ToDecimal(da.AvgPrice);
                    dr["Quantity"] = Convert.ToDecimal(da.Quantity);
                    dt.Rows.Add(dr);
                }
            }

            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("LapRekapKendaraan.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            string test = Session["Periode"].ToString();
            //SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
        }
        private void print_dtlKendaraan()
        {
            try
            {
                DataTable dt = new DataTable();
                ArrayList arrArmada = new ArrayList();
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("NoPol", typeof(string)));
                dt.Columns.Add(new DataColumn("Tanggal", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Value", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Total", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Keterangan", typeof(string)));
                crViewer.DisplayToolbar = true;
                crViewer.SeparatePages = true;
                crViewer.ShowAllPageIds = true;
                arrArmada = (ArrayList)Session["Data"];
                if (arrArmada.Count > 0)
                {
                    foreach (MTC_Armada da in arrArmada)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = Convert.ToInt32(da.ID);
                        dr["NoPol"] = da.NoPol;
                        dr["Tanggal"] = da.SPBDate.ToString("dd-MM-yyyy");
                        dr["ItemCode"] = da.ItemCode.ToString();
                        dr["ItemName"] = da.ItemName.ToString();
                        dr["Unit"] = da.Satuan;
                        dr["Qty"] = Convert.ToDecimal(da.Quantity);
                        dr["Value"] = Convert.ToDecimal(da.AvgPrice);
                        dr["Total"] = Convert.ToDecimal(da.Total);
                        dr["Keterangan"] = da.DeptName.ToString();
                        dt.Rows.Add(dr);
                    }
                }
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapDetailKendaraan.rpt"));
                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                SetCurrentValuesForParameterField(objRpt1, Session["Periode"].ToString(), "Periode");
            }
            catch
            {
            }
        }
        #region Report Sarmut Maintenance
        private void print_sarmut()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            sqlCon.Open();

            SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("LapEfesiensiPM.rpt"));


            objRpt1.SetDataSource(dt);

            crViewer.ReportSource = objRpt1;

            print_rekap_sarmut();
        }
        private void print_rekap_sarmut()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());

            sqlCon.Open();

            SqlDataAdapter da1 = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon);
            da1.SelectCommand.CommandTimeout = 0;
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            objRpt1.Subreports[0].SetDataSource(dt1);

            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
            SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");
            SetCurrentValuesForParameterField(objRpt1, Session["smt"].ToString(), "smt");
            SetCurrentValuesForParameterField(objRpt1, Session["smt1"].ToString(), "smt1");
            SetCurrentValuesForParameterField(objRpt1, Session["smt2"].ToString(), "smt2");
            SetCurrentValuesForParameterField(objRpt1, Session["smt3"].ToString(), "smt3");
            SetCurrentValuesForParameterField(objRpt1, Session["smt4"].ToString(), "smt4");
            SetCurrentValuesForParameterField(objRpt1, Session["smt5"].ToString(), "smt5");
            SetCurrentValuesForParameterField(objRpt1, Session["smt6"].ToString(), "smt6");
            SetCurrentValuesForParameterField(objRpt1, Session["bulan"].ToString(), "bulan");
            SetCurrentValuesForParameterField(objRpt1, Session["Dept"].ToString(), "Dept");
        }
        #endregion
        /*Print DO Parsial delivery */
        private void print_DO()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter sda = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
            sda.SelectCommand.CommandTimeout = 0;
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("DoForm.rpt"));
            objRpt1.SetDataSource(dt1);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = false;
            //BtnPrint.Visible = true;

            //penambahan agus 20-10-2022
            SetCurrentValuesForParameterField(objRpt1, Session["NomorDokumen"].ToString(), "NomorDokumen");
            //penambahan agus 20-10-2022

            SetCurrentValuesForParameterField(objRpt1, Session["contact"].ToString(), "contact");
            SetCurrentValuesForParameterField(objRpt1, Session["Bongkar"].ToString(), "Bongkar");
            SetCurrentValuesForParameterField(objRpt1, Session["AtasNama"].ToString(), "AtasNama");
        }
        public void ViewKasbonRealisasi()
        {
            int Dept = ((Users)Session["Users"]).DeptID;
            string UnitKerja = ((Users)Session["Users"]).ToString();

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt2 = new ReportDocument();
                SqlDataAdapter da2 = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon); da2.SelectCommand.CommandTimeout = 0;
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                Users users = (Users)Session["Users"];
                Kasbon kasbon = new Kasbon();
                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("KasbonRealisasi.rpt"));
                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
            }
            catch
            {

            }
        }
        public void ViewKASBON()
        {
            int Dept = ((Users)Session["Users"]).DeptID;
            decimal totalAll = 0;
            string JumlahDigit = DecimalFormat("PO");
            string UnitKerja = ((Users)Session["Users"]).ToString();

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;
                DataTable dt = new DataTable();
                da.Fill(dt);

                objRpt2 = new ReportDocument();
                SqlDataAdapter da2 = new SqlDataAdapter(Session["Query1"].ToString(), sqlCon); da2.SelectCommand.CommandTimeout = 0;
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                //MataUangFacade mataUangFacade = new MataUangFacade();
                //MataUang mataUang = mataUangFacade.RetrieveById(strCrc);
                //string nmUang = mataUang.Nama;

                Users users = (Users)Session["Users"];
                Kasbon kasbon = new Kasbon();
                string nmUang = "Rupiah";

                objRpt1 = new ReportDocument();
                objRpt2 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("Kasbon.rpt"));
                SetCurrentValuesForParameterField(objRpt1, totalAll.ToString(JumlahDigit), "TotalALL");
                TerbilangFacade terbilangFacade = new TerbilangFacade();
                string terSay = terbilangFacade.ConvertMoneyToWords(totalAll);
                string Say = terbilangFacade.changeNumericToWords(Convert.ToDouble(totalAll));
                SetCurrentValuesForParameterField(objRpt1, terSay + " " + nmUang, "Terbilang");
                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
            }
            catch
            {

            }
        }

       
    }
    public class CetakReceipt
    {
        private string field = "*";
        public string Field { get { return field; } set { field = value; } }
        public string Criteria { get; set; }
        public string Oleh { get; set; }
        public string QuerySelect()
        {
            return "Select " + this.Field + " from Receipt" + this.Criteria;
        }
        public string QueryUpdate()
        {
            return "Update Receipt set " + this.Field + " where " + this.Criteria;
        }
        public int StatusCetak()
        {
            int result = 0;
            DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.QuerySelect());
            string[] fld = this.Field.Split(new string[] { " as " }, StringSplitOptions.None);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr[((fld.Count() > 0) ? fld[1] : this.Field)].ToString());
                }
            }
            return result;
        }

        public int UpdateStatusCetak()
        {
            int result = 0;
            try
            {
                DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(this.QueryUpdate());
                if (da.Error == string.Empty)
                {
                    result = 1;
                }
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        

    }
}