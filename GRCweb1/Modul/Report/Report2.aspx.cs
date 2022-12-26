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
    public partial class Report21 : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        string STRPRINTERNAME;
        private string papername;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Check default printer on client machine

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
                    //btnExport.Visible = false;
                }
                catch
                { return; }
            }

            if (ViewState["report1"] == null)
            {
                Response.Expires = 0;
                string strID = "";
                strID = Request.QueryString["IdReport"].ToString();
                if (strID == "SuratJalan")
                {
                    //ViewSuratJalan();
                }
                if (strID == "RekapHistPO")
                {
                    papername = "PO";
                    ViewHistPO();
                }

                if (strID == "RekapHistPOExcel")
                {
                    papername = "PO";
                    ViewHistPOExcel();
                }
                if (strID == "SuratJalanTO")
                {
                    //ViewSuratJalanTO();
                }
                if (strID == "POPurchn")
                {
                    papername = "PO";
                    ViewPOPurchn2();
                }
                if (strID == "POPurchn2")
                {
                    papername = "PO";
                    ViewPOPurchnForFax();
                }

                if (strID == "Tawar")
                {
                    papername = "PO";
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
                    papername = "SJ";
                    print_DO();
                }
            }
            else
            {
                objRpt1 = new ReportDocument();
                objRpt1 = (ReportDocument)ViewState["report1"];
                crViewer.ReportSource = objRpt1;
            }
            // }
        }
        #region Report PO
        private string SubCompanyAktif = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SubCompanyAktif", "PO").ToString();
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
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
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
        public Company DetailCompany(int ID)
        {
            Company coi = new Company();
            try
            {
                string strSQL = "Select * From SuppPurch where CoID=" + ID;
                //DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString("ServerHO"));
                DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        coi.Nama = sdr["SupplierName"].ToString();
                        coi.Alamat1 = sdr["Telepon"].ToString();
                        coi.Alamat2 = sdr["Fax"].ToString();
                        coi.Manager = sdr["UP"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                coi.Nama = ex.Message;
            }
            return coi;
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
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
            int RevisiPO = pOPurchnFacade.GetPORevision(strPOid);
            int strCrc = pOPurchn.Crc;
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
            try
            {
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
            }
            catch
            {

            }
        }
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
           // btnExport.Visible = false;
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByID(strPOid);
            int RevisiPO = pOPurchnFacade.GetPORevision(strPOid);
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
        #endregion
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
            SetCurrentValuesForParameterField(objRpt1, Session["contact"].ToString(), "contact");
            SetCurrentValuesForParameterField(objRpt1, Session["Bongkar"].ToString(), "Bongkar");
            SetCurrentValuesForParameterField(objRpt1, Session["AtasNama"].ToString(), "AtasNama");
        }


    }
}