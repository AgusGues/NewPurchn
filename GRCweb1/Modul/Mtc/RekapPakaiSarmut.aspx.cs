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
using Factory;
using Cogs;
/** for export to exec **/
using System.IO;
using System.Data;
/** for export to pdf**/
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.DataVisualization.Charting;

namespace GRCweb1.Modul.Mtc
{
    public partial class RekapPakaiSarmut : System.Web.UI.Page
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
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExportpdf);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new System.Web.UI.WebControls.ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (RbBulan.Checked == true)
                LoadSarmut();
            else
                loadDynamicGrid();
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            btnExport.Enabled = true;
            Users users = (Users)Session["Users"];
            string thnBln = string.Empty;
            thnBln = ddTahun.SelectedItem.Text;
            string strSQL = string.Empty;
            strSQL = "declare @thn varchar(4) " +
                "set @thn='" + thnBln + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai1]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai1 " +
                "select Bulan,groupReg,sum(Harga)Harga into tempsrmtpakai from ( " +
                "select A.SPBDate, DATEPART(Month,A.spbdate)bulan, B.GroupNaME,B.GroupNaME+' ' + A.groupsp GroupReg,A.Harga    from ( " +
                "SELECT ls.SarmutID,ls.SPBDate,ls.PakaiNo,  " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,   " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName,  " +
                "   (select UomCode from UOM where ID=xx.UomID) as Unit,  " +
                "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp  " +
                "    FROM MTC_LapSarmut ls  " +
                "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID  " +
                "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID  " +
                "   where  xx.groupsp like '%regular%'  and  left(Convert(Char,ls.SPBDate,112),4) = @thn " +
                "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1   " +
                "UNION ALL  " +
                "  select E.SarmutID,ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1))  " +
                "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode,  " +
                "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2))  " +
                "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID)  " +
                "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC,  " +
                "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls  " +
                "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID  " +
                "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID  " +
                "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID  " +
                "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID  " +
                "  where xx.groupsp like '%regular%'  and left(Convert(Char,ls.ReturDate,112) ,4) = @thn)A inner join ( " +
                "  select vw.*,m.GroupNaME FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2) B on A.SarmutID =B.SarmutID ) C group by  Bulan,groupReg " +
                "select *,jan+feb+mar+mei+jun+jul+aug+sep+sep+okt+nov+[des] Total into tempsrmtpakai1 from ( " +
                "select distinct *, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=1) Jan, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=2) Feb, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=3) Mar, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=4) Apr, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=5) Mei, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=6) Jun, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=7) Jul, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=8) Aug, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=9) Sep, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=10) Okt, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=11) Nov, " +
                "(select isnull(sum(harga),0) from tempsrmtpakai where GroupReg=X.GroupReg and bulan=12) [Des] " +
                "from (select m.GroupNaME,m.GroupNaME +' ' + 'Regular' GroupReg,1 urut,'Regular' Reg  FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2 " +
                "union all " +
                "select m.GroupNaME,m.GroupNaME +' ' + 'Non Regular' GroupReg ,2 urut,'Non Regular' Reg FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2)X)Y order by GroupNaME ,urut,Reg " +
                "select GroupReg,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Aug,Sep,Okt,Nov,Des,Total from ( " +
                "select * from tempsrmtpakai1 " +
                "union all " +
                "select 'zx' GroupNaME,'Total Non Regular' GroupReg,0 urut,'' Reg,sum(Jan)Jan,sum(Feb)Feb,sum(Mar),sum(Apr)Apr,sum(Mei)Mei,sum(Jun)Jun,sum(Jul)Jul,sum(Aug)Aug, " +
                "sum(Sep)Sep,sum(Okt)Okt,sum(Nov)Nov,sum([Des])[Des],sum(Total)Total from tempsrmtpakai1 where reg='non regular'  " +
                "union all " +
                "select 'zy' GroupNaME,'Total Regular' GroupReg,0 urut,'' Reg,sum(Jan)Jan,sum(Feb)Feb,sum(Mar),sum(Apr)Apr,sum(Mei)Mei,sum(Jun)Jun,sum(Jul)Jul,sum(Aug)Aug, " +
                "sum(Sep)Sep,sum(Okt)Okt,sum(Nov)Nov,sum([Des])[Des],sum(Total)Total from tempsrmtpakai1 where reg='regular'  " +
                "union all " +
                "select 'zy' GroupNaME,'Grand Total' GroupReg,0 urut,'' Reg,sum(Jan)Jan,sum(Feb)Feb,sum(Mar),sum(Apr)Apr,sum(Mei)Mei,sum(Jun)Jun,sum(Jul)Jul,sum(Aug)Aug, " +
                "sum(Sep)Sep,sum(Okt)Okt,sum(Nov)Nov,sum([Des])[Des],sum(Total)Total from tempsrmtpakai1 )z " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai1]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai1";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            string formdeci = "{0:N1}";
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N2}";
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N2}";
                //    bfield.HeaderText = col.ColumnName;
                //}
                //if (col.ColumnName.Substring(0, 2) == "M3")
                //{
                //    bfield.HeaderText = "M3";
                //    bfield.DataFormatString = "{0:N4}";
                //}
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        protected void lstSarmut_DataBound(object source, RepeaterItemEventArgs e)
        {
            string ThnBln = ddTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            DataRowView sd = e.Item.DataItem as DataRowView;
            decimal totalSarmut = 0;
            decimal totalSarmutReg = 0;
            decimal totalSarmutNonReg = 0;
            decimal totalSarmutLain = 0;
            var dtlReg = e.Item.FindControl("dtlSarmutReg") as Repeater;
            var dtlNonReg = e.Item.FindControl("dtlSarmutNonReg") as Repeater;
            var dtlLain = e.Item.FindControl("dtlSarmutLain") as Repeater;
            Label ttsarmutReg = (Label)e.Item.FindControl("txtTotalSarmutReg");
            Label ttsarmutNonReg = (Label)e.Item.FindControl("txtTotalSarmutNonReg");
            Label ttsarmutLain = (Label)e.Item.FindControl("txtTotalSarmutLain");
            Label ttsarmut = (Label)e.Item.FindControl("txtTotalSarmut");

            if (dtlReg != null)
            {
                string dari = DateTime.Parse(txtDariTgl.Text).ToString("yyyyMMdd");
                string sampai = DateTime.Parse(txtSampaiTgl.Text).ToString("yyyyMMdd");
                string dept = (ddlDeptName.SelectedValue == "") ? string.Empty : "' and ls.DeptID='" + ddlDeptName.SelectedValue;
                string zona = (ddlZona.SelectedIndex == 0) ? string.Empty : " and xx.ZonaMTC='" + ddlZona.SelectedValue + "'";
                MTC_Zona sm = e.Item.DataItem as MTC_Zona;
                ArrayList arrDtReg = new ArrayList();
                arrDtReg = new MTC_SarmutFacade().DetailSarmutNewReg(sm.ID, ThnBln + dept, zona);
                foreach (MTC_Sarmut m in arrDtReg)
                {
                    totalSarmutReg += m.Total;
                }
                GrandTotal += totalSarmutReg;
                ttsarmutReg.Text = totalSarmutReg.ToString("###,##0.#0");
                totalSarmut += totalSarmutReg;
                dtlReg.DataSource = arrDtReg;
                dtlReg.DataBind();

                ArrayList arrDtNonReg = new ArrayList();
                arrDtNonReg = new MTC_SarmutFacade().DetailSarmutNewNonReg(sm.ID, ThnBln + dept, zona);
                foreach (MTC_Sarmut m in arrDtNonReg)
                {
                    totalSarmutNonReg += m.Total;
                }
                GrandTotal += totalSarmutNonReg;
                ttsarmutNonReg.Text = totalSarmutNonReg.ToString("###,##0.#0");
                totalSarmut += totalSarmutNonReg;
                ttsarmut.Text = totalSarmut.ToString("###,##0.#0");
                dtlNonReg.DataSource = arrDtNonReg;
                dtlNonReg.DataBind();

                ArrayList arrDtLain = new ArrayList();
                arrDtLain = new MTC_SarmutFacade().DetailSarmutNewLain(sm.ID, ThnBln + dept, zona);
                foreach (MTC_Sarmut m in arrDtLain)
                {
                    totalSarmutLain += m.Total;
                }
                GrandTotal += totalSarmutLain;
                ttsarmutLain.Text = totalSarmutLain.ToString("###,##0.#0");
                totalSarmut += totalSarmutLain;
                ttsarmut.Text = totalSarmut.ToString("###,##0.#0");
                dtlLain.DataSource = arrDtLain;
                dtlLain.DataBind();

                txtGtotal.Text = GrandTotal.ToString("###,##0.#0");
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
            decimal test = GrandTotal;
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
            if (RbBulan.Checked == true)
                lstSarmut.RenderControl(hw);
            else
                Panel2.RenderControl(hw);
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
            string Periode = "Periode : " + ddlBulan.SelectedItem.Text + " " + ddTahun.SelectedItem.Text;
            string Tabel = "<table style='width:100%; font-size:small;border-collapse:collapse' border='1'>" +
                           "<tr><th colspan='8'>" + Header + "</th></tr>";
            Tabel += "<tr><th colspan='8'>" + Periode + "</th></tr>";
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

        protected void RbBulan_CheckedChanged(object sender, EventArgs e)
        {
            if (RbBulan.Checked == true)
            {
                LBulan.Visible = true;
                ddlBulan.Visible = true;
                Panel1.Visible = true;
                Panel2.Visible = false;
            }
        }
        protected void RbTahun_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTahun.Checked == true)
            {
                LBulan.Visible = false;
                ddlBulan.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
            }
        }
        protected void RbTahun0_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTahun0.Checked == true)
            {
                LBulan.Visible = false;
                ddlBulan.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = true;
                MTC_SarmutFacade fml = new MTC_SarmutFacade();
                MTC_SarmutFacade fm = new MTC_SarmutFacade();
                ArrayList arrDataLegend = new ArrayList();
                ArrayList arrData = new ArrayList();

                arrDataLegend = fml.DetailSarmutChartLegend(ddTahun.SelectedItem.Text);
                int chki = 0;
                try
                {
                    ChkList.Items.Clear();
                    foreach (MTC_Sarmut Point in arrDataLegend)
                    {
                        ChkList.Items.Add(new System.Web.UI.WebControls.ListItem(Point.GroupReg, Point.GroupReg));
                        ChkList.Items[chki].Selected = true;
                        chki++;
                    }
                }
                catch { }
                int j = 0;
                Chart1.Titles.Add("REGULAR BUDGET MTN " + ddTahun.SelectedItem.Text);
                decimal TopPrice = 0;
                foreach (MTC_Sarmut s in arrDataLegend)
                {
                    Chart1.Legends.Add(s.GroupReg);
                    Chart1.Series.Add(s.GroupReg);
                    arrData = fm.DetailSarmutChartReg(ddTahun.SelectedItem.Text, s.GroupReg);
                    int[] x = new int[12];
                    int[] y = new int[12];
                    for (int i = 0; i < 12; i++)
                    {
                        y[i] = 0;
                        x[i] = i + 1;

                    }
                    foreach (MTC_Sarmut t in arrData)
                    {
                        y[t.Bulan - 1] = t.Harga;
                        x[t.Bulan - 1] = t.Bulan;
                        if (TopPrice < t.Harga)
                            TopPrice = t.Harga;

                    }
                    Chart1.Series[j].IsVisibleInLegend = true;
                    Chart1.Series[j].IsValueShownAsLabel = false;
                    Chart1.Series[j].Points.DataBindXY(x, y);
                    Chart1.Series[j].ChartType = SeriesChartType.Line;
                    Chart1.Series[j].XValueType = ChartValueType.Int32;
                    Chart1.Legends[j].Enabled = true;
                    Chart1.Legends[j].Docking = Docking.Right;
                    Chart1.Series[j].BorderWidth = 2;
                    Chart1.Series[j].ToolTip = s.GroupReg + "\nMonth = #VALX \nTotal Spending = #VALY{N}";
                    j++;
                }
                int YMax = 0;
                int interval = 0;
                if (TopPrice < 25000000) { YMax = 25000000; interval = 1000000; }
                if (TopPrice > 25000000 && TopPrice < 50000000) { YMax = 50000000; interval = 2000000; }
                if (TopPrice > 50000000 && TopPrice < 75000000) { YMax = 75000000; interval = 3000000; }
                if (TopPrice > 75000000 && TopPrice < 100000000) { YMax = 100000000; interval = 5000000; }
                if (TopPrice > 100000000 && TopPrice < 150000000) { YMax = 150000000; interval = 6000000; }
                if (TopPrice > 150000000 && TopPrice < 200000000) { YMax = 200000000; interval = 10000000; }
                if (TopPrice > 200000000 && TopPrice < 250000000) { YMax = 250000000; interval = 15000000; }
                if (TopPrice > 250000000) { YMax = Convert.ToInt32(TopPrice) + 1000000; interval = 20000000; }
                Chart1.ChartAreas[0].AxisX.Title = "Month";
                Chart1.ChartAreas[0].AxisY.Title = "Spending";
                Chart1.ChartAreas[0].AxisX.Interval = 1;
                Chart1.ChartAreas[0].AxisX.Maximum = 12;
                Chart1.ChartAreas[0].AxisY.Interval = interval;
                Chart1.ChartAreas[0].AxisY.Maximum = YMax;
                Chart1.ChartAreas[0].AxisY.Minimum = 0;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                Chart1.ChartAreas[0].Position = new ElementPosition(0, 10, 70, 90);
            }
        }
        protected void RbTahun1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void ChkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var series in Chart1.Series)
            {
                series.Points.Clear();
            }
            MTC_SarmutFacade fm = new MTC_SarmutFacade();
            ArrayList arrData = new ArrayList();
            Chart1.Titles.Add("REGULAR BUDGET MTN " + ddTahun.SelectedItem.Text);
            //for (int chki = 0; chki < ChkList.Items.Count; chki++)
            int chki = 0;
            int j = 0;
            decimal TopPrice = 0;
            foreach (System.Web.UI.WebControls.ListItem listItem in ChkList.Items)
            {
                if (listItem.Selected)
                {
                    string str = ChkList.Items[j].Text;
                    Chart1.Legends.Add(str);
                    Chart1.Series.Add(str);
                    arrData = fm.DetailSarmutChartReg(ddTahun.SelectedItem.Text, str);
                    int[] x = new int[12];
                    int[] y = new int[12];
                    for (int i = 0; i < 12; i++)
                    {
                        y[i] = 0;
                        x[i] = i + 1;

                    }
                    foreach (MTC_Sarmut t in arrData)
                    {
                        y[t.Bulan - 1] = t.Harga;
                        x[t.Bulan - 1] = t.Bulan;
                        if (TopPrice < t.Harga)
                            TopPrice = t.Harga;
                    }
                    Chart1.Series[chki].IsVisibleInLegend = true;
                    Chart1.Series[chki].IsValueShownAsLabel = false;
                    Chart1.Series[chki].Points.DataBindXY(x, y);
                    Chart1.Series[chki].ChartType = SeriesChartType.Line;
                    Chart1.Series[chki].XValueType = ChartValueType.Int32;
                    Chart1.Legends[chki].Enabled = true;
                    Chart1.Legends[chki].Docking = Docking.Right;
                    Chart1.Series[chki].BorderWidth = 2;
                    Chart1.Series[chki].ToolTip = str + "\nMonth = #VALX \nTotal Spending = #VALY{N}";
                    chki++;
                }
                else
                {
                    int test = 0;
                }
                j++;
            }
            int YMax = 0;
            int interval = 0;
            if (TopPrice < 25000000) { YMax = 25000000; interval = 1000000; }
            if (TopPrice > 25000000 && TopPrice < 50000000) { YMax = 50000000; interval = 2000000; }
            if (TopPrice > 50000000 && TopPrice < 75000000) { YMax = 75000000; interval = 3000000; }
            if (TopPrice > 75000000 && TopPrice < 100000000) { YMax = 100000000; interval = 5000000; }
            if (TopPrice > 100000000 && TopPrice < 150000000) { YMax = 150000000; interval = 6000000; }
            if (TopPrice > 150000000 && TopPrice < 200000000) { YMax = 200000000; interval = 10000000; }
            if (TopPrice > 200000000 && TopPrice < 250000000) { YMax = 250000000; interval = 15000000; }
            if (TopPrice > 250000000) { YMax = Convert.ToInt32(TopPrice) + 1000000; interval = 20000000; }
            Chart1.ChartAreas[0].AxisX.Title = "Month";
            Chart1.ChartAreas[0].AxisY.Title = "Spending";
            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisX.Maximum = 12;
            Chart1.ChartAreas[0].AxisY.Interval = interval;
            Chart1.ChartAreas[0].AxisY.Maximum = YMax;
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            Chart1.ChartAreas[0].Position = new ElementPosition(0, 10, 70, 90);

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}