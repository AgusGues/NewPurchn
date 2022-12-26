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

namespace GRCweb1.Modul.TPP
{
    public partial class Report : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        private ReportDocument objRpt2;
        private ReportDocument objRpt3;
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
                if (strID == "tpp")
                {
                    papername = "Legal";
                    ViewTPP();
                }
                if (strID == "pemantauantpp")
                {
                    papername = "Legal";
                    Viewpemantauantpp();
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }
        }


        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        { }
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
        public void ViewTPP()
        {
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
                objRpt3 = new ReportDocument();
                SqlDataAdapter da3 = new SqlDataAdapter(Session["Query2"].ToString(), sqlCon); da3.SelectCommand.CommandTimeout = 0;
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("TPP.rpt"));
                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                objRpt1.Subreports[1].SetDataSource(dt3);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                //SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
        public void Viewpemantauantpp()
        {
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
                objRpt3 = new ReportDocument();
                SqlDataAdapter da3 = new SqlDataAdapter(Session["Query2"].ToString(), sqlCon); da3.SelectCommand.CommandTimeout = 0;
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("pemantauantpp1.rpt"));
                objRpt1.SetDataSource(dt);
                objRpt1.Subreports[0].SetDataSource(dt2);
                objRpt1.Subreports[1].SetDataSource(dt3);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            }
            catch
            {

            }
        }
    }
}