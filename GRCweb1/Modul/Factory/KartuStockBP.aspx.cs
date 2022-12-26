using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.Data;
using System.IO;

namespace GRCweb1.Modul.Factory
{
    public partial class KartuStockBP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = DateTime.Now.Month;
                txtTahun.Text = DateTime.Now.Year.ToString();
                LoadItems();
                LoadKStock(" ");
            }
               ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Linkbtn);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Linkbtn2);
        }

        private void LoadItems()
        {
            FC_Items fcitems = new FC_Items();
            FC_ItemsFacade fcitemsfacade = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            arrItems = fcitemsfacade.RetrieveByStockBP(ThBl);
            GridViewItems.DataSource = arrItems;
            GridViewItems.DataBind();
        }
        private void LoadItemsPartno(string partno)
        {
            FC_Items fcitems = new FC_Items();
            FC_ItemsFacade fcitemsfacade = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            arrItems = fcitemsfacade.RetrieveByStockBJpartno(ThBl, txtPartnoC.Text.Trim());
            GridViewItems.DataSource = arrItems;
            GridViewItems.DataBind();
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtPartnoC.Text != string.Empty)
            {
                LoadItemsPartno(txtPartnoC.Text.Trim());
            }
        }
        private void LoadKStock(string Partno)
        {
            KartuStock Kartu = new KartuStock();
            KartuStockFacade Kartufacade = new KartuStockFacade();
            ArrayList arrKartu = new ArrayList();
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            arrKartu = Kartufacade.RetrieveByPartNoBJ(Partno, ThBl, strQtyLastMonth);
            //GridViewKStock.DataSource = arrKartu;
            //GridViewKStock.DataBind();
            GridViewKStock1.DataSource = arrKartu;
            GridViewKStock1.DataBind();
        }

        protected void GridViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridViewItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "pilih")
            {
                PanelRekap.Visible = false; Panel4.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewItems.Rows[index];
                LPartno.Text = row.Cells[0].Text;
                LoadKStock(row.Cells[0].Text.Trim());
            }
            if (e.CommandName == "rekap")
            {
                PanelRekap.Visible = true; Panel4.Visible = false;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewItems.Rows[index];
                LPartno2.Text = row.Cells[0].Text;
                LoadKStockRekap(row.Cells[0].Text.Trim());
            }
        }

        private void LoadKStockRekap(string Partno)
        {
            KartuStock Kartu2 = new KartuStock();
            KartuStockFacade Kartufacade2 = new KartuStockFacade();
            ArrayList arrKartu2 = new ArrayList();
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            arrKartu2 = Kartufacade2.RetrieveByPartNoBJ2(Partno, ThBl, strQtyLastMonth);
            //GridViewKStock.DataSource = arrKartu;
            //GridViewKStock.DataBind();
            GridViewRekap.DataSource = arrKartu2;
            GridViewRekap.DataBind();
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadKStock(LPartno.Text.Trim());
        }

        protected void GridViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItems.PageIndex = e.NewPageIndex;
            if (txtPartnoC.Text.Trim() == string.Empty)
            {
                LoadItems();
            }
            else
            {
                LoadItemsPartno(txtPartnoC.Text.Trim());
            }
        }

        protected void txtPartnoC_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoC.Text != string.Empty)
            {
                LPartno.Text = txtPartnoC.Text.Trim();
                LoadKStock(txtPartnoC.Text.Trim());
            }
        }
        protected void btn_Show_Click(object sender, EventArgs e)
        {

        }
        protected void GridViewKStock1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridViewRekap_RowCommand(object sender, GridViewCommandEventArgs e)
        { }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            //GridView grv = (GridView)GridViewKStock1.Rows[rowindex].FindControl("GridView2");
            //Label lbl = (Label)GridViewKStock1.Rows[rowindex].FindControl("Label2");
            //GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = false;

            //// 
            //if (e.CommandName == "Details")
            //{
            //    GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = true;
            //    GridViewKStock1.Rows[rowindex].FindControl("btn_Show").Visible = false;
            //    ArrayList arrT3Rekap = new ArrayList();
            //    T3_RekapFacade T3Rekap = new T3_RekapFacade();
            //    string tglterima = DateTime.Parse(GridViewKStock1.Rows[rowindex].Cells[0].Text).ToString("yyyyMMdd");
            //    string keterangan = string.Empty;
            //    if (GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(9).Trim() == "pengiriman")
            //    {
            //        arrT3Rekap = T3Rekap.RetrieveByTglKirimKS(tglterima, LPartno.Text.Trim());
            //    }
            //    else
            //    {
            //        if (GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(7).Trim() == "retur")
            //        {
            //            arrT3Rekap = T3Rekap.RetrieveByTglReturKS(tglterima, LPartno.Text.Trim());
            //        }
            //        else
            //        {
            //            if (GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(0, 3).Trim() == "trm")
            //            {
            //                keterangan = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(7);
            //                arrT3Rekap = T3Rekap.RetrieveByTglTerimaKS(tglterima, LPartno.Text.Trim(), keterangan.Trim());
            //            }
            //            else
            //            {
            //                keterangan = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(9);
            //                arrT3Rekap = T3Rekap.RetrieveByTglKeluarKS(tglterima, LPartno.Text.Trim(), keterangan.Trim());
            //            }
            //        }
            //    }
            //    grv.DataSource = arrT3Rekap;
            //    grv.DataBind();
            //    grv.Visible = true;
            //}
            //else
            //{
            //    //// child gridview  display false when cancel button raise event
            //    grv.Visible = false;
            //    GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = false;
            //    GridViewKStock1.Rows[rowindex].FindControl("btn_Show").Visible = true;
            //}
        }

        protected void GridViewKStock1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void GridViewKStock1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            if (GridViewKStock1.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", LPartno.Text.Trim() + "-" + ddlBulan.SelectedValue + "-" + txtTahun.Text.Substring(2, 2) + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridViewKStock1.AllowPaging = false;
            GridViewKStock1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GridViewKStock1.HeaderRow.Cells.Count; i++)
            {
                GridViewKStock1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GridViewKStock1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void ExportToExcel2(object sender, EventArgs e)
        {
            if (GridViewRekap.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", LPartno2.Text.Trim() + "-" + ddlBulan.SelectedValue + "-" + txtTahun.Text.Substring(2, 2) + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            string Html = "<b>KARTU STOCK BP";
            Html += "<br><b>PERIODE : " + ddlBulan.SelectedValue + "-" + txtTahun.Text.Substring(2, 2);
            Html += "<br><b>PARTNO  : " + LPartno2.Text.ToString().Trim();
            Html += "<br>";
            string HtmlEnd = "";

            GridViewRekap.AllowPaging = false;
            GridViewRekap.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GridViewRekap.HeaderRow.Cells.Count; i++)
            {
                GridViewRekap.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GridViewRekap.RenderControl(htw);
            //string Contents = sw.ToString();
            //Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + HtmlEnd);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}