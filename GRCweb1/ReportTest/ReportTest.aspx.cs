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
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace GRCweb1.ReportTest
{
    public partial class ReportTest : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        private string papername = "Legal";
        string STRPRINTERNAME;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string subkey = @"Software\Microsoft\Windows NT\CurrentVersion\Windows";
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, false);
            //string defaultPrinter = ((string)key.GetValue("Device")).Split(',')[0];
            //key.Close();
            //string defaultprinter;
            //RegistryKey regKey = Registry.CurrentUser;
            //regKey = regKey.CreateSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows");
            //string regvalue = (string)regKey.GetValue("Device");
            //string charDP = "";
            //int TotalChar = regvalue.Length;
            //defaultprinter = "";
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
            //Session["defaultprinter"] = defaultPrinter;




            string p1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string p2 = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string p3 = this.Server.MapPath("");

            Text1.Value = p1;
            Text2.Value = p2;
            Text3.Value = p3;

            //Text1.Value = Directory.GetCurrentDirectory();
            //Text2.Value = AppDomain.CurrentDomain.BaseDirectory;
            //Text3.Value = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            #region parameter report
            if (ViewState["report1"] == null)
            {

                Response.Expires = 0;
                string strID = "";
                strID = Request.QueryString["IdReport"].ToString();

                if (strID == "JournalReport")
                {
                    papername = "Legal";
                    JournalReport();
                }

            }
            else
            {
                ReportDocument objRpt = new ReportDocument();
                objRpt = (ReportDocument)ViewState["report1"];
                CrystalReportViewer1.ReportSource = objRpt;

            }
            #endregion

        }
        public void JournalReport()
        {
            string pathRpt = AppDomain.CurrentDomain.BaseDirectory+ "ReportTest/cr1.rpt";

            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("cr1.rpt"));
            //objRpt1.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = objRpt1;
            CrystalReportViewer1.DisplayToolbar = true;

            //    //}
            //    //catch
            //    //{

            //    //}
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
        //private void SetPrintOptions(string paper)
        //{
        //    try
        //    {
        //        PrintOptions printOptions = objRpt1.PrintOptions;
        //        STRPRINTERNAME = ddlPrinter.SelectedValue;
        //        Users users = (Users)Session["Users"];
        //        printersFacade printerfacade = new printersFacade();
        //        Printers printer = new Printers();
        //        int rawKind = printerfacade.RetrievebyKind(STRPRINTERNAME, paper);


        //        printOptions.PrinterName = STRPRINTERNAME;
        //        if (paper == "a4")
        //        {
        //            objRpt1.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
        //            CrystalDecisions.Shared.PageMargins pMargin = new CrystalDecisions.Shared.PageMargins(0, 0, 0, 0);
        //            objRpt1.PrintOptions.ApplyPageMargins(pMargin);
        //        }
        //        else
        //        {
        //            if (printOptions.PrinterName != null)
        //            {
        //                System.Drawing.Printing.PrintDocument doctoprint = new System.Drawing.Printing.PrintDocument();
        //                doctoprint.PrinterSettings.PrinterName = printOptions.PrinterName;
        //                if (rawKind == 0)
        //                {
        //                    for (int i = 0; i <= doctoprint.PrinterSettings.PaperSizes.Count - 1; i++)
        //                    {
        //                        if (doctoprint.PrinterSettings.PaperSizes[i].PaperName.ToUpper() == paper)
        //                        {
        //                            rawKind = Convert.ToInt32(doctoprint.PrinterSettings.PaperSizes[i].GetType().GetField("kind",
        //                            System.Reflection.BindingFlags.Instance |
        //                            System.Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes[i]));
        //                            break;
        //                        }
        //                    }
        //                    printerfacade = new printersFacade();
        //                    int result = printerfacade.Insertraw(STRPRINTERNAME, papername, rawKind);
        //                }
        //                objRpt1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
        //                CrystalDecisions.Shared.PageMargins pMargin = new CrystalDecisions.Shared.PageMargins(0, 0, 0, 0);
        //                objRpt1.PrintOptions.ApplyPageMargins(pMargin);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }

        //}
        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }






    }
}