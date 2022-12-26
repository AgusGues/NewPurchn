using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapAsset_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            ReceiptFacade rf = new ReceiptFacade();
            ddlTahun.Items.Clear();
            ArrayList arrData = new ArrayList();
            arrData = rf.RetrieveTahun();
            foreach (BeritaAcara b in arrData)
            {
                ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadListAsset();
        }
        private decimal saAwal, saBeli, saAdjIn, saAdjOut, mIn, mOut, sAkhir, sSPB, n, sGudang = 0;
        private void LoadListAsset()
        {
            n = 0;
            saAwal = 0; saBeli = 0; saAdjIn = 0; saAdjOut = 0; mIn = 0; mOut = 0; sAkhir = 0; sSPB = 0; sGudang = 0;
            ReceiptFacade rf = new ReceiptFacade();
            ArrayList arrData = new ArrayList();
            string fieldSaldo = string.Empty;
            string yearPeriod = string.Empty;
            rf.Bulan = int.Parse(ddlBulan.SelectedValue);
            rf.Tahun = int.Parse(ddlTahun.SelectedValue);
            switch (ddlBulan.SelectedValue)
            {
                case "1":
                    fieldSaldo = "DesQty";
                    yearPeriod = ((int.Parse(ddlTahun.SelectedValue)) - 1).ToString();
                    break;
                default:
                    fieldSaldo = Global.nBulan(int.Parse(ddlBulan.SelectedValue), true) + "Qty";
                    yearPeriod = ddlTahun.SelectedValue.ToString();
                    break;
            }
            rf.FieldSaldoAwal = fieldSaldo;
            rf.YearPeriode = yearPeriod;
            arrData = rf.RetrieveRekapAssetNew();
            lstAsset.DataSource = arrData;
            lstAsset.DataBind();
            //total.Cells[1].InnerHtml = n.ToString("N0");
            total.Cells[1].InnerHtml = saAwal.ToString("N0");
            total.Cells[2].InnerHtml = saBeli.ToString("N0");
            total.Cells[3].InnerHtml = saAdjIn.ToString("N0");
            total.Cells[4].InnerHtml = saAdjOut.ToString("N0");
            /*
            total.Cells[5].InnerHtml = mIn.ToString("N0");
            total.Cells[6].InnerHtml = mOut.ToString("N0");
            */
            total.Cells[5].InnerHtml = sAkhir.ToString("N0"); //index prev = 7
                                                              /*
                                                              total.Cells[8].InnerHtml = sSPB.ToString("N0");
                                                              total.Cells[9].InnerHtml = sGudang.ToString("N0");
                                                              */

        }
        protected void lstAsset_Databound(object sender, RepeaterItemEventArgs e)
        {
            Domain.RekapAsset rk = (Domain.RekapAsset)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            if (rk.StockGudang < 0)
            {
                //tr.Cells[12].Attributes.Add("style", "color:red");

            }
            saAwal = (saAwal + rk.SaldoAwal);
            saBeli = (saBeli + rk.Pembelian);
            saAdjIn = (saAdjIn + rk.AdjustIN);
            saAdjOut = (saAdjOut + rk.AdjustOut);
            mIn = (mIn + rk.MutasiIN);
            mOut = (mOut + rk.MutasiOut);
            sAkhir = (sAkhir + rk.SaldoAkhir);
            sSPB = (sSPB + rk.SPB);
            sGudang = (sGudang + rk.StockGudang);
            n = n + 1;

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapAsset.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }
}