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

namespace GRCweb1.Modul.Report
{
    public partial class ReportUPD : System.Web.UI.Page
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
                //Button1.Attributes["onclick"] = "window.print();";
                //Response.Cache.SetNoStore();
                //Response.AppendHeader("Pragma", "no-cache");

                string strID = "";
                strID = Request.QueryString["IdReport"].ToString();

                if (strID == "LapMasterDok")
                {
                    papername = "Legal";
                    ViewLapMasterDok();
                }
                if (strID == "LapDokISO")
                {
                    papername = "Legal";
                    ViewReportUPD();
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
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

        //public void ViewReportUPD()
        //{
        //    SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["GRCBoard"].ToString());
        //    try
        //    {
        //        sqlCon.Open();

        //        SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon); da.SelectCommand.CommandTimeout = 0;

        //        DataTable dt = new DataTable();



        //        da.Fill(dt);
        //        ReportDocument objRpt1 = new ReportDocument();
        //        objRpt1.Load(this.Server.MapPath("LapDokISO.rpt"));

        //        objRpt1.SetDataSource(dt);
        //        crViewer.ReportSource = objRpt1;
        //        crViewer.DisplayToolbar = true;
        //        SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "periodeAkhir");
        //        SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "periodeAwal");
        //    }
        //    catch
        //    {

        //    }
        //}

        public void ViewReportUPD()
        {
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            try
            {
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(Session["Query"].ToString(), sqlCon);
                DataTable dt = new DataTable();



                da.Fill(dt);
                ReportDocument objRpt1 = new ReportDocument();
                objRpt1.Load(this.Server.MapPath("LapDokISO.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["prdakhir"].ToString(), "periodeAkhir");
                SetCurrentValuesForParameterField(objRpt1, Session["prdawal"].ToString(), "periodeAwal");
            }
            catch
            {

            }
        }


        public void ViewLapMasterDok()
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


                if (Session["pilihMaster"].ToString() == "Pedoman Mutu")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokPM.rpt"));
                else
                    if (Session["pilihMaster"].ToString() == "Instruksi Kerja")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokIK.rpt"));
                else
                        if (Session["pilihMaster"].ToString() == "Form")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokForm.rpt"));
                else
                            if (Session["pilihMaster"].ToString() == "Prosedure")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokP.rpt"));
                else
                                if (Session["pilihMaster"].ToString() == "Rencana Mutu")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokRM.rpt"));
                else
                                    if (Session["pilihMaster"].ToString() == "Standar")
                    objRpt1.Load(this.Server.MapPath("LapMasterDokSTD.rpt"));

                objRpt1.SetDataSource(dt);
                crViewer.ReportSource = objRpt1;
                crViewer.DisplayToolbar = true;
                SetCurrentValuesForParameterField(objRpt1, Session["Deptm"].ToString(), "Deptm");
                SetCurrentValuesForParameterField(objRpt1, Session["pilihMaster"].ToString(), "pilihMaster");

            }
            catch
            {

            }
        }

        private void print_wo()
        {
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["GRCBOARD"].ToString());
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
    }
}