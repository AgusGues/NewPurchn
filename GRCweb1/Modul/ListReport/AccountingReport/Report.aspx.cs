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

namespace GRCweb1.Modul.ListReport.AccountingReport
{
    public partial class Report : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        string STRPRINTERNAME;
        private string papername = "SJ";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
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
                for (int i = 0; i <= TotalChar; i++)
                {
                    charDP = regvalue.Substring(i, 1);
                    if (charDP != ",")
                    {
                        defaultprinter = defaultprinter + charDP;
                        charDP = "";
                    }
                    else break;
                }
                Session["defaultprinter"] = defaultPrinter;
                //Label1.Text = Session["defaultprinter"].ToString();
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


            if (ViewState["report1"] == null)
            {
                Response.Expires = 0;
                string strID = ""; string strID2 = "";
                strID = Request.QueryString["IdReport"].ToString();
                strID2 = (Request.QueryString["tp"] == null) ? "" : Request.QueryString["tp"].ToString();

                if (strID == "LMutasiStock")
                {
                    papername = "Legal";
                    ViewLMutasiStock();
                }
                if (strID == "LMutasiTransStock")
                {
                    papername = "Legal";
                    ViewLMutasiTransStock();
                }
                if (strID == "LMutasiSP")
                {
                    papername = "Legal";
                    ViewLMutasiSP();
                }
                if (strID == "Adjustment")
                {
                    papername = "A4";
                    ViewLapAdjustment();
                }
                if (strID == "AdjustmentT1T3")
                {
                    papername = "A4";
                    ViewLapAjdustmentT1T3();
                }
                if (strID == "CountSheet")
                {
                    papername = "A4";
                    if (strID2 == "Curing")
                    {
                        CuringSheet();
                    }
                    else if (strID2 == "Jemur")
                    {
                        JemurSheet();
                    }
                    else if (strID2 == "Transit")
                    {
                        TransitSheet();
                    }
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }

        }

        protected void ViewLMutasiStock()
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
                objRpt1.Load(this.Server.MapPath("LapMutasiStock.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["judul"].ToString(), "judul");

            }
            catch
            {

            }
        }
        protected void ViewLMutasiTransStock()
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
                objRpt1.Load(this.Server.MapPath("LapMutasiStockByName.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["nm_barang"].ToString(), "nm_barang");
                SetCurrentValuesForParameterField(objRpt1, Session["judul"].ToString(), "judul");
            }
            catch
            {

            }
        }
        protected void ViewLMutasiSP()
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
                objRpt1.Load(this.Server.MapPath("LapDetailMutasiSP.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
                SetCurrentValuesForParameterField(objRpt1, Session["nm_barang"].ToString(), "nm_barang");

            }
            catch
            {

            }
        }
        protected void ViewLapAdjustment()
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
                objRpt1.Load(this.Server.MapPath("LapAdjustment.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
                SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");

            }
            catch
            {

            }

        }
        protected void CuringSheet()
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
                objRpt1.Load(this.Server.MapPath("CuringCountSheet.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                //SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
                //SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");

            }
            catch
            {

            }
        }
        protected void JemurSheet()
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
                objRpt1.Load(this.Server.MapPath("JemurCountSheet.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                //SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
                //SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");

            }
            catch
            {

            }
        }
        protected void TransitSheet()
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
                objRpt1.Load(this.Server.MapPath("TransitCountSheet.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                //SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
                //SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");

            }
            catch
            {

            }
        }
        protected void ViewLapAjdustmentT1T3()
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
                objRpt1.Load(this.Server.MapPath("LapAdjustmentT1T3.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["fromDate"].ToString(), "fromDate");
                SetCurrentValuesForParameterField(objRpt1, Session["toDate"].ToString(), "toDate");
                SetCurrentValuesForParameterField(objRpt1, Session["judul"].ToString(), "Judul");
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
        /**===============================================================*/
        protected void btnPrint_Click(object sender, EventArgs e)
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
        protected void crViewer_Init(object sender, EventArgs e)
        {

        }
    }
}