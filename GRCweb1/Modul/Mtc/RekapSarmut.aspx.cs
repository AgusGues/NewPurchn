using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Cogs;
/** for export to exec **/
using System.IO;
using System.Data;
/** for export to pdf**/
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace GRCweb1.Modul.Mtc
{
    public partial class RekapSarmut : System.Web.UI.Page
    {
        public decimal GrandTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/default.aspx";
                txtDariTgl.Text = DateTime.Now.AddDays(1 - (DateTime.Now.Day)).ToString("dd-MMM-yyyy");
                txtSampaiTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                btnExport.Enabled = false;
                btnExportpdf.Enabled = false;
                LoadZona();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExportpdf);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadSarmut();
        }

        private void LoadSarmut()
        {
            ArrayList arrSm = new ArrayList();
            arrSm = new MTC_ZonaFacade().RetrieveSpGroup();
            lstSarmut.DataSource = arrSm;
            lstSarmut.DataBind();
            btnExportpdf.Enabled = true;
            btnExport.Enabled = true;
        }
        protected void lstSarmut_DataBound(object source, RepeaterItemEventArgs e)
        {
            DataRowView sd = e.Item.DataItem as DataRowView;
            decimal totalSarmut = 0;
            var dtl = e.Item.FindControl("dtlSarmut") as Repeater;
            Label ttsarmut = (Label)e.Item.FindControl("txtTotalSarmut");
            if (dtl != null)
            {
                string dari = DateTime.Parse(txtDariTgl.Text).ToString("yyyyMMdd");
                string sampai = DateTime.Parse(txtSampaiTgl.Text).ToString("yyyyMMdd");
                string dept = (ddlDeptName.SelectedValue == "") ? string.Empty : "' and ls.DeptID='" + ddlDeptName.SelectedValue;
                string zona = (ddlZona.SelectedIndex == 0) ? string.Empty : " and xx.ZonaMTC='" + ddlZona.SelectedValue + "'";
                MTC_Zona sm = e.Item.DataItem as MTC_Zona;
                ArrayList arrDt = new ArrayList();
                //arrDt = new MTC_SarmutFacade().DetailSarmut(sm.ID, dari, sampai);
                arrDt = new MTC_SarmutFacade().DetailSarmut(sm.ID, dari, sampai + dept, zona);
                foreach (MTC_Sarmut m in arrDt)
                {
                    totalSarmut += m.Total;
                }
                GrandTotal += totalSarmut;
                ttsarmut.Text = totalSarmut.ToString("###,##0.#0");
                dtl.DataSource = arrDt;
                dtl.DataBind();
            }
        }
        private void LoadZona()
        {
            #region penambahan Zona
            /** Added on 12-01-2016 base on WO**/
            string ZonaAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ZonaAktif", "SPBMaintenance");
            string[] arrZona = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ZonaName", "SPBMaintenance").Split(',');
            ddlZona.Items.Clear();
            ddlZona.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih Zona--", "0"));
            for (int i = 0; i < arrZona.Count(); i++)
            {
                ddlZona.Items.Add(new System.Web.UI.WebControls.ListItem(arrZona[i].ToString(), arrZona[i].ToString()));
            }
            #region depreciated
            //switch (ddlDeptName.SelectedValue)
            //{
            //    case "4":
            //    case "5": 
            //    case "18":
            //    case "19":
            //        spZona.Visible = (ZonaAktif == "1") ? true : false;
            //        ddlZona.Visible = (ZonaAktif == "1") ? true : false;
            //        Session["Zona"] = ZonaAktif;
            //        break;
            //    default:
            //        spZona.Visible = false;
            //        Session["Zona"] = null;
            //        ddlZona.Visible = false;
            //        break;
            //}
            #endregion
            #endregion
        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapSarmut.xls");
            Response.Charset = "utf-8";
            // Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            lstSarmut.RenderControl(hw);
            Response.Write("<table border='1'>");
            Response.Write(sw.ToString());
            Response.Write("</table>");
            Response.Flush();
            Response.End();

        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            string dari = DateTime.Parse(txtDariTgl.Text).ToString("dd-MMM-yyyy");
            string sampai = DateTime.Parse(txtSampaiTgl.Text).ToString("dd-MMM-yyyy");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=RekapSarmut_" + ((Users)Session["Users"]).UserName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            lstSarmut.RenderControl(hw);
            /** 
             * CreateHeader PDF
             */
            string Header = "REKAP SARMUT MAINTENANCE";
            string Periode = "Periode :" + dari + " s/d " + sampai;
            string Tabel = "<table style='width:100%; font-size:small;border-collapse:collapse' border='1'>" +
                           "<tr><th colspan='8'>" + Header + "</th></tr>";
            Tabel += "<tr><th colspan='8'>" + Periode + "</th></tr>";
            //Tabel += "<tr align='center'>" +
            //          "<th style='width:5'>No.</th>" +
            //          "<th style='width:20'>Tgl SPB</th>" +
            //          "<th style='width:25'>Item Code</th>" +
            //          "<th style='width:200'>Item Name</th>" +
            //          "<th style='width:10'>Unit</th>" +
            //          "<th style='width:25'>Jumlah</th>" +
            //          "<th style='width:35'>Harga</th>" +
            //          "<th style='width:35'>Total</th>" +
            //          "</tr>";
            Tabel += sw.ToString().Replace("\r", "").Replace("\n", "").Replace("  ", "").Replace(" class=\"baris EvenRows\"", "").Replace(" class=\"tengah kotak\"", "").Replace(" class=\"kotak\"", "").Replace("%", "px");
            Tabel += "</table>";
            Document pdfDoc = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            StringReader sr = new StringReader(Tabel);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.Flush();
            Response.End();
        }

    }
}