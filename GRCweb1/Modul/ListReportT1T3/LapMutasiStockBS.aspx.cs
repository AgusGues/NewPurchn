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

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LapMutasiStockBS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtTahun.Text = DateTime.Now.Year.ToString();
                txtTanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtTanggal0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["Elapbul"] = "No";
            #region prepare parameter
            string strQtyLastMonth = string.Empty;
            string strHQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            DateTime tanggal = DateTime.Parse(txtTanggal.Text);
            DateTime tanggal0 = DateTime.Parse(txtTanggal0.Text);
            string paramTanggal = tanggal.ToString("dd MMM yyyy");
            string paramTanggal0 = tanggal0.ToString("dd MMM yyyy");
            string intTanggal = tanggal.ToString("yyyyMMdd");
            string intTanggal0 = tanggal0.ToString("yyyyMMdd");

            if (ddlBulan.SelectedValue == "1")
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "2")
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "3")
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "4")
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "5")
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "6")
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "7")
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "8")
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "9")
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "10")
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "11")
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedValue == "12")
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            #endregion
            if (RBDetail.Checked == true)
            {
                strQuery = reportFacade.ViewMutasiBJDet(txtPartno.Text, thnbln, strQtyLastMonth);
                Session["Query"] = strQuery;
                Session["periode"] = "BS Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                Session["nama"] = txtPartno.Text;
                Cetak(this);
            }
            else
            {
                if (RBNormal.Checked == true)
                {
                    if (Convert.ToInt32(thnbln) >= Convert.ToInt32("202006"))
                    {
                        strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "Normal", "baru");
                    }
                    else
                    {
                        strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "Normal", "lama");
                    }
                    //strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "Normal", "lama");
                    if (RBBulanan.Checked == true)
                    {
                        if (Convert.ToInt32(thnbln) < Convert.ToInt32("201708"))
                            Session["periode"] = "BS Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                        else
                            Session["periode"] = "BS Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                    }
                    else
                    {
                        #region define Bulan
                        int bulan = Convert.ToInt32(Convert.ToDateTime(txtTanggal0.Text).ToString("MM"));
                        if (bulan == 1)
                        {
                            strHQtyLastMonth = "DesQty";
                        }
                        else if (bulan == 2)
                        {
                            strHQtyLastMonth = "JanQty";
                        }
                        else if (bulan == 3)
                        {
                            strHQtyLastMonth = "FebQty";
                        }
                        else if (bulan == 4)
                        {
                            strHQtyLastMonth = "MarQty";
                        }
                        else if (bulan == 5)
                        {
                            strHQtyLastMonth = "AprQty";
                        }
                        else if (bulan == 6)
                        {
                            strHQtyLastMonth = "MeiQty";
                        }
                        else if (bulan == 7)
                        {
                            strHQtyLastMonth = "JunQty";
                        }
                        else if (bulan == 8)
                        {
                            strHQtyLastMonth = "JulQty";
                        }
                        else if (bulan == 9)
                        {
                            strHQtyLastMonth = "AguQty";
                        }
                        else if (bulan == 10)
                        {
                            strHQtyLastMonth = "SepQty";
                        }
                        else if (bulan == 11)
                        {
                            strHQtyLastMonth = "OktQty";
                        }
                        else if (bulan == 12)
                        {
                            strHQtyLastMonth = "NovQty";
                        }
                        #endregion

                        if (Convert.ToInt32(thnbln) >= Convert.ToInt32("202006"))
                        {
                            strQuery = reportFacade.ViewMutasiBSDetRekapHarian(intTanggal, intTanggal0, strHQtyLastMonth, strQtyMonth, "NORMAL", "baru");
                        }
                        else
                        {
                            strQuery = reportFacade.ViewMutasiBSDetRekapHarian(intTanggal, intTanggal0, strHQtyLastMonth, strQtyMonth, "NORMAL", "lama");
                        }

                        Session["periode"] = "BS Periode : " + paramTanggal0;
                    }
                }
                if (RBKonversi.Checked == true)
                {
                    //strQuery = reportFacade.ViewMutasiBSDetRekapKonversi(thnbln, strQtyLastMonth, strQtyMonth);
                    if (Convert.ToInt32(thnbln) >= Convert.ToInt32("202006"))
                    {
                        strQuery = reportFacade.ViewMutasiBSDetRekapKonversi(thnbln, strQtyLastMonth, strQtyMonth, "baru");
                    }
                    else
                    {
                        strQuery = reportFacade.ViewMutasiBSDetRekapKonversi(thnbln, strQtyLastMonth, strQtyMonth, "lama");
                    }
                    Session["periode"] = "BS Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text + " Konversi ke Ukuran Utuh(1220X2440)";
                }
                if (RBBSAuto.Checked == true)
                {
                    if (Convert.ToInt32(thnbln) >= Convert.ToInt32("201708"))
                    {
                        if (Convert.ToInt32(thnbln) >= Convert.ToInt32("202006"))
                        {
                            strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "BSAUTO", "baru");
                        }
                        else
                        {
                            strQuery = reportFacade.ViewMutasiBSDetRekap(thnbln, strQtyLastMonth, strQtyMonth, "BSAUTO", "lama");
                        }

                        //if (RBBulanan.Checked == true)
                        //{
                        //    Session["periode"] = "BS Auto Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                        //}
                        if (RBBulanan.Checked == true)
                        {
                            if (Convert.ToInt32(thnbln) < Convert.ToInt32("201708"))
                                Session["periode"] = "BS Auto Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                            else
                                Session["periode"] = "BS Auto Periode : " + ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
                        }
                        else
                        {
                            #region define Bulan
                            int bulan = Convert.ToInt32(Convert.ToDateTime(txtTanggal0.Text).ToString("MM"));
                            if (bulan == 1)
                            {
                                strHQtyLastMonth = "DesQty";
                            }
                            else if (bulan == 2)
                            {
                                strHQtyLastMonth = "JanQty";
                            }
                            else if (bulan == 3)
                            {
                                strHQtyLastMonth = "FebQty";
                            }
                            else if (bulan == 4)
                            {
                                strHQtyLastMonth = "MarQty";
                            }
                            else if (bulan == 5)
                            {
                                strHQtyLastMonth = "AprQty";
                            }
                            else if (bulan == 6)
                            {
                                strHQtyLastMonth = "MeiQty";
                            }
                            else if (bulan == 7)
                            {
                                strHQtyLastMonth = "JunQty";
                            }
                            else if (bulan == 8)
                            {
                                strHQtyLastMonth = "JulQty";
                            }
                            else if (bulan == 9)
                            {
                                strHQtyLastMonth = "AguQty";
                            }
                            else if (bulan == 10)
                            {
                                strHQtyLastMonth = "SepQty";
                            }
                            else if (bulan == 11)
                            {
                                strHQtyLastMonth = "OktQty";
                            }
                            else if (bulan == 12)
                            {
                                strHQtyLastMonth = "NovQty";
                            }
                            #endregion
                            //strQuery = reportFacade.ViewMutasiBSDetRekapHarian(intTanggal, intTanggal0, strHQtyLastMonth, strQtyMonth,"BSAUTO","");
                            if (Convert.ToInt32(thnbln) >= Convert.ToInt32("202006"))
                            {
                                strQuery = reportFacade.ViewMutasiBSDetRekapHarian(intTanggal, intTanggal0, strHQtyLastMonth, strQtyMonth, "BSAUTO", "baru");
                            }
                            else
                            {
                                strQuery = reportFacade.ViewMutasiBSDetRekapHarian(intTanggal, intTanggal0, strHQtyLastMonth, strQtyMonth, "BSAUTO", "lama");
                            }

                            Session["periode"] = "BS Auto Periode : " + paramTanggal0;
                        }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Pemisahan Laporan BSAuto mulai berlaku periode bulan Agustus 2017, untuk periode sebelumnya masih disatukan");
                        return;
                    }
                }
                Session["Query"] = strQuery;
                if (RBLembar.Checked == true)
                    Session["satuan"] = "lembar";
                else
                    Session["satuan"] = "m3";
                Cetak1(this);
            }
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBJ', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void Cetak1(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBSRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak1();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void RBRekap_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RBBulanan_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBulanan.Checked == true)
            {
                Panel3.Visible = true;
                Panel1.Visible = false;
            }
        }
        protected void RBHarian_CheckedChanged(object sender, EventArgs e)
        {
            if (RBHarian.Checked == true)
            {
                Panel3.Visible = false;
                Panel1.Visible = true;
            }
        }
    }
}