using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using System.Data.SqlClient;
using Domain;
using System.IO;
using System.Collections.Generic;

namespace GRCweb1.ModalDialog
{
    public partial class FromKadarAirQA : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string DocKA = (Request.QueryString["ka"] != null) ? Request.QueryString["ka"].ToString() : "";
            LoadDataKA(DocKA);
            string[] BisaPrint = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PrintKadarAir", "PO").Split(',');
            int pos = Array.IndexOf(BisaPrint, ((Users)Session["Users"]).DeptID.ToString());
            btnExport.Visible = (pos > -1) ? true : false;
            ((ScriptManager)Page.FindControl("ScriptManager2")).RegisterPostBackControl(btnExport);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string DocKA = (Request.QueryString["ka"] != null) ? Request.QueryString["ka"].ToString() : "";
        ExportToPDF(DocKA);
    }

    private void LoadDataKA(string DocKA)
    {       

        string DefaultStdKA = string.Empty; string stdNow = string.Empty; string stdNow2 = string.Empty;

        stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
        stdNow2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA2", "PO");
        DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");        

        /** A1 = [Perhitungan KA Aktif Saat Ini] **/
        ArrayList arrData = new ArrayList();
        DepoKertasKA dk = new DepoKertasKA();
        QAKadarAir ka = new QAKadarAir();
        string Criteria = " AND DocNo='" + DocKA + "'";
        ka = dk.Retrieve(Criteria, true);
        /** End A1 **/

        /** A2 = [Perhitungan Standart KA] **/
        ArrayList arrData2 = new ArrayList();
        DepoKertasKA dk2 = new DepoKertasKA();
        QAKadarAir ka2 = new QAKadarAir();
        string Criteria2 = " DocNo='" + DocKA + "' and rowstatus>-1 ) ";
        ka2 = dk2.Retrieve2(Criteria2, true);
        /** End A2 **/

        /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
        DepoKertasKA dk01 = new DepoKertasKA();
        QAKadarAir ka01 = new QAKadarAir();
        ka01 = dk01.RetrieveDataKertasImport(ka.ItemCode);
        /** End Syarat 1 **/

        /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
        DepoKertasKA dk02 = new DepoKertasKA();
        QAKadarAir ka02 = new QAKadarAir();
        ka02 = dk02.RetrieveDataSupplier(ka.SupplierID);
        /** End Syarat 2 **/

        /** Syarat 3 = [Verifikasi apakah ada Supplier Wijaya Limbah **/
        DepoKertasKA dk03 = new DepoKertasKA();
        QAKadarAir ka03 = new QAKadarAir();
        ka03 = dk03.RetrieveDataSupplier0(ka.SupplierID);
        /** End Syarat 3 **/

        string tglcek = ka.TglCheck.ToString("yyyyMMdd");
        string tglmulai = "20200206";
        #region Logika Baru Pengaturan Periode
        if (Convert.ToInt32(tglcek) >= Convert.ToInt32(tglmulai))
        {
            /** Jika Syarat 1,2 terpenuhi akan dimunculkan 2 perhitungan KA (KA yg aktif dan Standart KA) **/
            #region Rule 1
            //if (ka01.ID == 0 && ka02.ID == 0 || ka01.ID == 0 && ka02.ID > 0)
            if (ka01.ID > -1 && ka02.ID > -1)
            {                
                if (ka2.Brutto > 0 && ka2.Netto > 0)
                {
                    txtBeratBersih.Text = (ka.GrossPlant - ka2.Potongan).ToString("N2");
                    txtBeratBersih2.Text = (ka.GrossPlant - ka.Potongan).ToString("N2");
                    txtPotongan4.Text = ka2.Potongan.ToString("N2");
                    txtPotongan3.Text = ka2.Potongan.ToString("N2");
                    txtSelisihKA.Text = (ka.AvgKA - Convert.ToDecimal(ka2.StdKA)).ToString("N2");
                    txtstdKA4.Text = Convert.ToDecimal(ka2.StdKA).ToString("N0");
                    txtstdKA2.Text = Convert.ToDecimal(ka2.StdKA).ToString("N0");
                    txtBB.Text = Convert.ToDecimal(ka.NettPlant).ToString("N2");
                    txtBeratKotor3.Text = ka2.Brutto.ToString("N2");
                    txtBeratKotor4.Text = ka.GrossPlant.ToString("N2");
                    txtSelisihKA3.Text = (ka.AvgKA - Convert.ToDecimal(ka.StdKA)).ToString("N2");
                    txtSmph3.Text = ka.Sampah.ToString("N2");
                    txtPotongan.Text = ka.Potongan.ToString("N2");
                    txtPotongan2.Text = ka.Potongan.ToString("N2");
                    txtstdKA3.Text = ka.StdKA.ToString("N0");
                    txtstdKA5.Text = ka.StdKA.ToString("N0");

                    decimal net = Convert.ToDecimal((ka.GrossPlant - ka.Potongan).ToString("N2")) * (1 - (ka.StdKA/100));
                    lblBB0_Persen_Nilai.Text = Convert.ToDecimal((ka.GrossPlant - ka.Potongan).ToString("N0")) + " - " + " " + ka.StdKA.ToString("N0") + "% ";
                    lblBB0_Persen_Nilai1.Text = net.ToString("N2");
                    if (Convert.ToInt32(tglcek) < Convert.ToInt32("20210615"))
                    {

                        lblBB0_Persen.Visible = false;
                        tr1.Visible = true; tr2.Visible = true;
                        tr01.Visible = true; tr02.Visible = true;
                        tr11.Visible = false; tr22.Visible = false;
                        tr3.Visible = false; tr4.Visible = false; tr5.Visible = false; 
                    }
                    else
                    {
                        txtBeratKotor4.Visible = true; txtPotongan2.Visible = true; tr22.Visible = false;
                        tr01.Visible = false; tr02.Visible = false; tr1.Visible = true; tr11.Visible = false; tr2.Visible = true;
                        tr3.Visible = true; tr4.Visible = true; tr5.Visible = true;
                        lblBB0_Persen.Text = " Berat Bersih " + ka.StdKA.ToString("N0") + "% - KA " + ka.StdKA.ToString("N0") + "% ";
            
                    }
                }
                else
                {
                    txtBeratBersih.Text = (ka.GrossPlant - ka.Potongan).ToString("N2");
                    txtPotongan4.Text = ka.Potongan.ToString("N2");
                    txtPotongan3.Text = ka.Potongan.ToString("N2");
                    txtSelisihKA.Text = (ka.AvgKA - Convert.ToDecimal(ka.StdKA)).ToString("N2");
                    txtstdKA4.Text = Convert.ToDecimal(ka.StdKA).ToString("N0");
                    txtstdKA2.Text = Convert.ToDecimal(ka.StdKA).ToString("N0");
                    txtBB.Text = Convert.ToDecimal(ka.NettPlant).ToString("N2");
                   
                    tr1.Visible = false; tr11.Visible = false; 
                    tr2.Visible = false; tr22.Visible = false;
                }
                txtItemName.Text = ka.ItemName;
                txtTglKA.Text = ka.TglCheck.ToString("dd/MM/yyyy");
                txtSuppName.Text = ka.NOPOL.ToString() + "<br>" + ka.SupplierName;
                txtBK.Text = ka.GrossPlant.ToString("N2");
                txtKadarAir.Text = ka.AvgKA.ToString("N2");
                txtBeratKotor1.Text = ka.GrossPlant.ToString("N2");             
                txtSmph.Text = ka.Sampah.ToString("N2");
                txtBeratKotor.Text = ka.GrossPlant.ToString("N2");
                txtKadarAir.Text = ka.AvgKA.ToString("N2");
                txtstdKA.Text = ka.StdKA.ToString("N1");                
                txtBeratKotor2.Text = ka.GrossPlant.ToString("N2");
            }
            #endregion
            /** Jika Syarat 1,2 tidak terpenuhi akan dimunculkan 1 perhitungan KA (Standart KA) **/
        }
        #endregion
        /** Logika Lama **/
        #region Logika Lama
        else
        {
            decimal selisihka = 0;
            txtBeratKotor1.Text = ka.GrossPlant.ToString("N2");
            txtSelisihKA.Text = (ka.AvgKA - Convert.ToDecimal(DefaultStdKA)).ToString("N2");
            txtSmph.Text = ka.Sampah.ToString("N2");
            if (ka.StdKA == Convert.ToDecimal(DefaultStdKA))
            {
                txtPotongan.Text = "0"; txtPotongan2.Text = "0"; txtPotongan4.Text = ka.Potongan.ToString("N2");
            }
            txtItemName.Text = ka.ItemName;
            txtTglKA.Text = ka.TglCheck.ToString("dd/MM/yyyy");
            txtSuppName.Text = ka.NOPOL.ToString() + "<br>" + ka.SupplierName;
            txtBK.Text = ka.GrossPlant.ToString("N2");
            txtBB.Text = ka.NettPlant.ToString("N2");
            txtBeratKotor.Text = ka.GrossPlant.ToString("N2");
            txtKadarAir.Text = ka.AvgKA.ToString("N2");
            txtstdKA.Text = ka.StdKA.ToString("N1");
            txtSelisihKA3.Text = (ka.AvgKA - ka.StdKA).ToString("N2");
            txtBeratKotor2.Text = ka.GrossPlant.ToString("N2");
            selisihka = (ka.Sampah > 0) ? Math.Abs(ka.AvgKA - ka.StdKA) + ka.Sampah : (ka.AvgKA - ka.StdKA);
            txtBeratBersih.Text = (ka.GrossPlant - ka.Potongan).ToString("N2");
            txtSph2.Text = ka.BeratSampah.ToString("N2");
            txtstdKA3.Text = ka.StdKA.ToString("N0");
            txtstdKA4.Text = Convert.ToDecimal(DefaultStdKA).ToString("N0");
            txtstdKA5.Text = ka.StdKA.ToString("N0");
            txtstdKA2.Text = Convert.ToDecimal(DefaultStdKA).ToString("N0");
            txtPotongan3.Text = ka.Potongan.ToString("N2");

            tr22.Visible = false; tr11.Visible = false; tr2.Visible = false; tr1.Visible = false;
            tr1.Visible = true; tr2.Visible = true;
            tr01.Visible = false; tr02.Visible = false;
            txtBeratKotor4.Text = ka.GrossPlant.ToString("N2");
            txtBeratKotor3.Text = ka.GrossPlant.ToString("N2");
            txtPotongan2.Text = ka.Potongan.ToString("N2");
            txtBeratBersih2.Text = (ka.GrossPlant - ka.Potongan).ToString("N2");
            txtSelisihKA3.Text = (ka.AvgKA - ka.StdKA).ToString("N2");
            txtSmph3.Text = ka.Sampah.ToString("N2");
            txtPotongan.Text = ka.Potongan.ToString("N2");
        }
        #endregion

        switch (ka.RowStatus)
        {
            case 1: imgChk1.Visible = true; imgNo1.Visible = false; 
                imgChk2.Visible = false; imgNo2.Visible = true; 
                imgChk13.Visible = false; imgNo13.Visible = true; 
                break;
            case 2: imgChk1.Visible = false; imgNo1.Visible = true; 
                imgChk2.Visible = true; imgNo2.Visible = false; 
                imgChk13.Visible = false; imgNo13.Visible = true; 
                break;
            case -1: imgChk1.Visible = false; imgNo1.Visible = true; imgChk2.Visible = false; imgNo2.Visible = true; 
                imgChk13.Visible = true; imgNo13.Visible = false; 
                break;
        }
        if (ka.BeratSample > 0)
        {
            txtBeratSample.Text = ka.BeratSampah.ToString("N2") + " / " + ka.BeratSample.ToString("N2") + " x 100 ";                
            txtBeratSample0.Text = ka.Sampah.ToString("N2");                
        }
        else
        {
            txtBeratSample.Text = "0";
            txtBeratSample0.Text = "0";
        }
        aphead.InnerHtml = (ka.Approval <= 0) ? "" : "Approved";// "<img src=\"../images/Approved_16.png\" alt=\"Approved\" />";
        apmgr.InnerHtml = (ka.Approval <= 1) ? "" : "Approved";// "<img src=\"../images/Approved_16.png\" alt=\"Approved\" />";
        string dakid=" AND DKKAID="+ka.ID.ToString();
        arrData = dk.RetrieveKADetail(dakid,true);
        ArrayList arrT1 = new ArrayList();
        int n = 0;
        foreach (QAKadarAir kad in arrData)
        {
            n = n + 1;
            QAKadarAir k = new QAKadarAir();
                k.NoBall  = kad.NoBall ;
                k.BALKe = kad.BALKe;
                k.Tusuk1 = kad.Tusuk1;
                k.Tusuk2 = kad.Tusuk2;
                k.AvgKA = kad.AvgTusuk;
                k.NoBall1 = kad.NoBall1;
                k.BALKe1 = kad.BALKe1;
                k.Tusuk11 = kad.Tusuk11;
                k.Tusuk21 = kad.Tusuk21;
                k.AvgKA1 = kad.AvgTusuk1;
            arrT1.Add(k);
        }
        
        lstTusuk.DataSource = arrT1;
        lstTusuk.DataBind();
    }
    private void ExportToPDF(string DocKA)
    {
        
    }

    }
}