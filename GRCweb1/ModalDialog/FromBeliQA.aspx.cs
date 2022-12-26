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

namespace GRCweb1.ModalDialog
{
    public partial class FromBeliQA : System.Web.UI.Page
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
            ArrayList arrData = new ArrayList();
            DepoKertasKA dk = new DepoKertasKA();
            QAKadarAir ka = new QAKadarAir();
            decimal selisihka = 0;
            string Criteria = " AND DocNo='" + DocKA + "')beli";
            ka = dk.RetrieveBeli(Criteria, true);
            txtItemName.Text = ka.ItemName;
            txtTglKA.Text = ka.TglCheck.ToString("dd/MM/yyyy");
            txtSuppName.Text = ka.SupplierName;
            txtBK.Text = ka.GrossPlant.ToString("N2");
            txtBB.Text = ka.NettPlant.ToString("N2");
            txtBeratKotor.Text = ka.GrossPlant.ToString("N2");
            txtKadarAir.Text = ka.AvgKA.ToString("N2");
            txtstdKA.Text = ka.StdKA.ToString("N1");
            txtBeratKotor1.Text = ka.GrossPlant.ToString("N2");
            txtSelisihKA.Text = (ka.AvgKA - ka.StdKA).ToString("N2");
            txtBeratKotor2.Text = ka.GrossPlant.ToString("N2");
            txtSmph.Text = ka.Sampah.ToString("N2");
            selisihka = (ka.Sampah > 0) ? Math.Abs(ka.AvgKA - ka.StdKA) + ka.Sampah : (ka.AvgKA - ka.StdKA);
            txtPotongan.Text = ka.Potongan.ToString("N2");// (ka.GrossPlant * selisihka);
            txtPotongan2.Text = ka.Potongan.ToString("N2");// (ka.GrossPlant * selisihka);
            txtBeratBersih.Text = (ka.GrossPlant - ka.Potongan).ToString("N2");
            txtSph2.Text = ka.BeratSampah.ToString("N2");
            switch (ka.RowStatus)
            {
                case 1: imgChk1.Visible = true; imgNo1.Visible = false; imgChk2.Visible = false; imgNo2.Visible = true; imgChk13.Visible = false; imgNo13.Visible = true; break;
                case 2: imgChk1.Visible = false; imgNo1.Visible = true; imgChk2.Visible = true; imgNo2.Visible = false; imgChk13.Visible = false; imgNo13.Visible = true; break;
                case -1: imgChk1.Visible = false; imgNo1.Visible = true; imgChk2.Visible = false; imgNo2.Visible = true; imgChk13.Visible = true; imgNo13.Visible = false; break;
            }
            if (ka.BeratSample > 0)
            {
                txtBeratSample.Text = "Berat Sample : " + ka.BeratSample.ToString("N2");
                txtBeratSample.Text += "<br>Berat Sampah : " + ka.BeratSampah.ToString("N2");
                txtBeratSample.Text += "<hr>";
                txtBeratSample.Text += "Pros.Sampah(%) : " + ka.Sampah.ToString("N2");
            }
            aphead.InnerHtml = (ka.Approval <= 0) ? "" : "Approved";// "<img src=\"../images/Approved_16.png\" alt=\"Approved\" />";
            apmgr.InnerHtml = (ka.Approval <= 1) ? "" : "Approved";// "<img src=\"../images/Approved_16.png\" alt=\"Approved\" />";
            string dakid = " AND DKKAID=" + ka.ID.ToString();
            //dakid += " Order by BALKe";
            arrData = dk.RetrieveBeliKADetail(dakid, true);
            ArrayList arrT1 = new ArrayList();
            int n = 0;
            foreach (QAKadarAir kad in arrData)
            {
                n = n + 1;
                QAKadarAir k = new QAKadarAir();
                k.NoBall = kad.NoBall;
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