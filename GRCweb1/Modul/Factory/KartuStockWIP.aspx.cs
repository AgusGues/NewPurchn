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
using System.IO;
using System.Data;

namespace GRCweb1.Modul.Factory
{
    public partial class KartuStockWIP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = DateTime.Now.Month;
                txtTahun.Text = DateTime.Now.Year.ToString();
                getYear();
                LoadItems();
                LoadKStockBM(" ");
            }
       ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Linkbtn);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddlTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            txtTahun.Text = ddlTahun.SelectedValue;
            LoadItems();
            LoadKStockBM(LPartno.Text.Trim());
        }
        private void LoadItems()
        {
            FC_Items fcitems = new FC_Items();
            FC_ItemsFacade fcitemsfacade = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            ArrayList arrItems2 = new ArrayList();
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
            if (RadioButton1.Checked == true)
            {
                arrItems = fcitemsfacade.RetrieveByKSWIP(ThBl, strQtyLastMonth);
            }
            else
            {
                arrItems = fcitemsfacade.RetrieveByKSLari(ThBl, strQtyLastMonth);
            }
            GridViewItems.DataSource = arrItems;
            GridViewItems.DataBind();

        }
        private void LoadKStockBM(string Partno)
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
                strQtyLastMonth = "12";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "01";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "02";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "03";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "04";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "05";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "06";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "07";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "sepQty";
                strQtyLastMonth = "08";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "09";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "10";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "11";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            if (Partno.Trim().Length > 0)
            {
                //string subpartno = Partno.Trim().Substring(3, 3);
                //if (LPartno.Text.Substring(3, 3).Trim() != "-1-" )
                arrKartu = Kartufacade.RetrieveByPartNoBM(Partno, ThBl, strQtyLastMonth);
                //else
                //    arrKartu = Kartufacade.RetrieveByPartNoP(Partno, ThBl, strQtyLastMonth);
            }
            GridViewKStock1.DataSource = arrKartu;
            GridViewKStock1.DataBind();
        }
        private void LoadKStockLari(string Partno)
        {
            KartuStock Kartu = new KartuStock();
            KartuStockFacade Kartufacade = new KartuStockFacade();
            ArrayList arrKartu = new ArrayList();
            string strQtyLastMonth = string.Empty;
            string strQtyLastQty = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "12";
                strQtyLastQty = "desqty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastQty = "janqty";
                strQtyLastMonth = "01";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastQty = "febqty";
                strQtyLastMonth = "02";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastQty = "marqty";
                strQtyLastMonth = "03";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastQty = "AprQty";
                strQtyLastMonth = "04";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastQty = "MeiQty";
                strQtyLastMonth = "05";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastQty = "JunQty";
                strQtyLastMonth = "06";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastQty = "JulQty";
                strQtyLastMonth = "07";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyMonth = "sepQty";
                strQtyLastQty = "AguQty";
                strQtyLastMonth = "08";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastQty = "sepQty";
                strQtyLastMonth = "09";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastQty = "OktQty";
                strQtyLastMonth = "10";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastQty = "NovQty";
                strQtyLastMonth = "11";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            if (Partno.Trim().Length > 0)
            {
                //string subpartno = Partno.Trim().Substring(3, 3);
                //if (LPartno.Text.Substring(3, 3).Trim() != "-1-" )
                arrKartu = Kartufacade.RetrieveByPartNoP(Partno, ThBl, strQtyLastMonth);
                //else
                //    arrKartu = Kartufacade.RetrieveByPartNoP(Partno, ThBl, strQtyLastMonth);
            }
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
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewItems.Rows[index];
                LPartno.Text = row.Cells[0].Text;
                if (RadioButton1.Checked == true)
                    LoadKStockBM(row.Cells[0].Text.Trim());
                else
                    LoadKStockLari(row.Cells[0].Text.Trim());
            }
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadKStockBM(LPartno.Text.Trim());
        }

        protected void GridViewKStock1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewKStock1.Rows[rowindex].FindControl("GridView2");
            Label lbl = (Label)GridViewKStock1.Rows[rowindex].FindControl("Label2");
            GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = false;
            try
            {
                // 
                if (e.CommandName == "Details")
                {
                    GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = true;
                    GridViewKStock1.Rows[rowindex].FindControl("btn_Show").Visible = false;
                    ArrayList arrT1_Serah = new ArrayList();
                    T1_SerahFacade t1_Serah = new T1_SerahFacade();
                    string tglterima = DateTime.Parse(GridViewKStock1.Rows[rowindex].Cells[0].Text).ToString("yyyyMMdd");
                    string keterangan = string.Empty;
                    string partno = string.Empty;
                    string spartno = LPartno.Text.Substring(3, 3).Trim();
                    //if (GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(0, 1).Trim().ToUpper() == "T")
                    //{
                    //    if (LPartno.Text.Substring(3, 3).Trim() != "-1-" )//|| LPartno.Text.Substring(4, 3).Trim() != "-1-" || LPartno.Text.Substring(5, 3).Trim() != "-1-")
                    //    {
                    //        partno = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(9).Trim();
                    //        arrT1_Serah = t1_Serah.RetrieveByTglSerahWIP(tglterima, partno, LPartno.Text);
                    //    }
                    //    else
                    //    {
                    //        partno = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(9).Trim();
                    //        arrT1_Serah = t1_Serah.RetrieveByTglSerahWIP2(tglterima, partno, LPartno.Text);
                    //    }
                    //}
                    //else
                    //{
                    //    if (LPartno.Text.Substring(3, 3).Trim() != "-1-" )//|| LPartno.Text.Substring(4, 3).Trim() != "-1-" || LPartno.Text.Substring(4, 3).Trim() != "-1-")
                    //    {
                    //        partno = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(12, 17).Trim();
                    //        arrT1_Serah = t1_Serah.RetrieveByTglProduksiWIP(tglterima, partno);
                    //    }
                    //    else
                    //    {
                    //        partno = LPartno.Text;
                    //        arrT1_Serah = t1_Serah.RetrieveByTglProduksiWIP2(tglterima, partno, GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(12).Trim());
                    //    }
                    //}
                    partno = GridViewKStock1.Rows[rowindex].Cells[1].Text.Substring(9).Trim();
                    arrT1_Serah = t1_Serah.RetrieveByTglSerahWIP(tglterima, partno, LPartno.Text);
                    grv.DataSource = arrT1_Serah;
                    grv.DataBind();
                    grv.Visible = true;
                }
                else
                {
                    //// child gridview  display false when cancel button raise event
                    grv.Visible = false;
                    GridViewKStock1.Rows[rowindex].FindControl("Cancel").Visible = false;
                    GridViewKStock1.Rows[rowindex].FindControl("btn_Show").Visible = true;
                }
            }
            catch
            { }
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
        protected void btn_Show_Click(object sender, EventArgs e)
        {

        }
        protected void txtPartnoC_TextChanged(object sender, EventArgs e)
        {
            LPartno.Text = txtPartnoC.Text.Trim();
            LoadKStockBM(txtPartnoC.Text.Trim());
        }

        protected void LinkButton3_Click1(object sender, EventArgs e)
        {
            ExportToExcel(sender, e);
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
                LoadItems();
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
                LoadItems();
        }
        protected void txtTahun_TextChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadKStockBM(LPartno.Text.Trim());
        }

    }
}