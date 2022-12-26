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
    public partial class KartuStockBJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = DateTime.Now.Month;
                txtTahun.Text = DateTime.Now.Year.ToString();
                LoadItems();
                LoadGroupMarketing();
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
            arrItems = fcitemsfacade.RetrieveByStockBJ(ThBl);
            GridViewItems.DataSource = arrItems;
            GridViewItems.DataBind();
        }
        private void LoadGroupMarketing()
        {
            T3_Groups t3_Groups = new T3_Groups();
            T3_GroupsFacade t3_Groupsfacade = new T3_GroupsFacade();
            ArrayList arrt3_Groups = new ArrayList();
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            arrt3_Groups = t3_Groupsfacade.Retrieve();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (T3_Groups t3Group in arrt3_Groups)
            {
                ddlGroup.Items.Add(new ListItem(t3Group.Groups, t3Group.ID.ToString()));
            }
        }

        private void LoadGroupMarketingDetail(string GroupID)
        {
            FC_Items fcitems = new FC_Items();
            FC_ItemsFacade fcitemsfacade = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            string group = ddlGroup.SelectedItem.Text;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            arrItems = fcitemsfacade.RetrieveByGroupMarketing(group, ThBl);
            GridViewItemsGroup.DataSource = arrItems;
            GridViewItemsGroup.DataBind();
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

        private void LoadKStockGroup(string groups, decimal tebal, int lebar, int panjang)
        {
            KartuStock Kartu = new KartuStock();
            KartuStockFacade Kartufacade = new KartuStockFacade();
            ArrayList arrKartu = new ArrayList();
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;
            if (ddlBulan.SelectedIndex == 0)
                return;
            if (ddlBulan.SelectedIndex > 1)
            {
                ThBl0 = txtTahun.Text.ToString() + (ddlBulan.SelectedIndex - 1).ToString().PadLeft(2, '0');
                ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            }
            else
            {
                ThBl0 = (int.Parse(txtTahun.Text) - 1).ToString() + "12";
                ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            }
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
            arrKartu = Kartufacade.RetrieveByGroup(ThBl0, ThBl, strQtyLastMonth, groups, tebal.ToString("##0.00"), lebar.ToString(), panjang.ToString());
            GridViewKStockGroup.DataSource = arrKartu;
            GridViewKStockGroup.DataBind();
        }
        protected string convertTebal(string tebal)
        {
            string intebal = string.Empty;
            string k = string.Empty;
            for (int i = 0; i <= tebal.Length - 1; i++)
            {
                k = tebal.Substring(i, 1);
                if (tebal.Substring(i, 1) == ",")
                    k = ".";
                else
                    k = tebal.Substring(i, 1);
                intebal = intebal + k;
            }
            tebal = intebal;
            return tebal;
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

        protected void GridViewRekap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "rekap")
            //{
            //    PanelRekap.Visible = true; Panel4.Visible = false;

            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = GridViewItems.Rows[index];
            //    LPartno.Text = row.Cells[0].Text;
            //    LoadKStockRekap(row.Cells[0].Text.Trim());
            //}
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBPartno.Checked == true)
            {
                LoadItems();
                LoadKStock(LPartno.Text.Trim());
            }
            else
            {
                LoadGroupMarketingDetail(ddlGroup.SelectedValue);
                if (Session["tebal"].ToString() != string.Empty && Session["lebar"].ToString() != string.Empty && Session["panjang"].ToString() != string.Empty)
                    LoadKStockGroup(Session["group"].ToString().Trim(), decimal.Parse(Session["tebal"].ToString()), int.Parse(Session["lebar"].ToString()), int.Parse(Session["panjang"].ToString()));
            }
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
            //    string keterangan = string.Empty ;
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

        protected void RBPartno_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPartno.Checked == true)
            {
                PanelPartno.Visible = true;
                PanelGroup.Visible = false;
            }
        }

        protected void RBGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (RBGroup.Checked == true)
            {
                PanelPartno.Visible = false;
                PanelGroup.Visible = true;
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGroupMarketingDetail(ddlGroup.SelectedValue);
        }

        protected void GridViewItemsGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItemsGroup.PageIndex = e.NewPageIndex;
            LoadGroupMarketingDetail(ddlGroup.SelectedItem.Text);
        }

        protected void GridViewItemsGroup_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridViewItemsGroup.PageIndex = e.NewPageIndex;
            LoadGroupMarketingDetail(ddlGroup.SelectedValue);
        }

        protected void GridViewItemsGroup_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "pilih")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewItemsGroup.Rows[index];
                LGroups.Text = row.Cells[0].Text + " " + row.Cells[1].Text + " mm " + row.Cells[2].Text + " X " + row.Cells[3].Text;
                Session["group"] = row.Cells[0].Text;
                Session["tebal"] = row.Cells[1].Text;
                Session["lebar"] = row.Cells[2].Text;
                Session["panjang"] = row.Cells[3].Text;
                LoadKStockGroup(row.Cells[0].Text.Trim(), decimal.Parse(row.Cells[1].Text), int.Parse(row.Cells[2].Text), int.Parse(row.Cells[3].Text));
            }
        }
        protected void GridViewKStockGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewKStockGroup.Rows[rowindex].FindControl("GridView3");
            Label lbl = (Label)GridViewKStockGroup.Rows[rowindex].FindControl("Label2");
            GridViewKStockGroup.Rows[rowindex].FindControl("Cancel0").Visible = false;

            // 
            if (e.CommandName == "Details")
            {
                GridViewKStockGroup.Rows[rowindex].FindControl("Cancel0").Visible = true;
                GridViewKStockGroup.Rows[rowindex].FindControl("btn_Show0").Visible = false;
                ArrayList arrT3Rekap = new ArrayList();
                T3_RekapFacade T3Rekap = new T3_RekapFacade();
                string tglterima = DateTime.Parse(GridViewKStockGroup.Rows[rowindex].Cells[0].Text).ToString("yyyyMMdd");
                string keterangan = string.Empty;
                if (GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim() == "pengiriman")
                    arrT3Rekap = T3Rekap.RetrieveByTglKirimKS(tglterima, Session["group"].ToString().Trim(), convertTebal(Session["tebal"].ToString()), Session["lebar"].ToString(), Session["panjang"].ToString());
                if (GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim() == "Retur")
                    arrT3Rekap = T3Rekap.RetrieveByTglReturKS(tglterima, Session["group"].ToString().Trim(), convertTebal(Session["tebal"].ToString()), Session["lebar"].ToString(), Session["panjang"].ToString());

                if (GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim() == "Direct")
                {
                    keterangan = GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim();
                    arrT3Rekap = T3Rekap.RetrieveByTglTerimaKS(tglterima, Session["group"].ToString().Trim(), convertTebal(Session["tebal"].ToString()), Session["lebar"].ToString(), Session["panjang"].ToString(), keterangan.Trim());
                }
                if (GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim() == "Simetris" || GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim() == "Asimetris")
                {
                    keterangan = GridViewKStockGroup.Rows[rowindex].Cells[5].Text.Trim();
                    if (int.Parse(GridViewKStockGroup.Rows[rowindex].Cells[3].Text) > 0)
                        arrT3Rekap = T3Rekap.RetrieveByTglKeluarKS(tglterima, Session["group"].ToString().Trim(), convertTebal(Session["tebal"].ToString()), Session["lebar"].ToString(), Session["panjang"].ToString(), keterangan.Trim());
                    else
                        arrT3Rekap = T3Rekap.RetrieveByTglTerimaKS(tglterima, Session["group"].ToString().Trim(), convertTebal(Session["tebal"].ToString()), Session["lebar"].ToString(), Session["panjang"].ToString(), keterangan.Trim());
                }
                grv.DataSource = arrT3Rekap;
                grv.DataBind();
                grv.Visible = true;
            }
            else
            {
                //// child gridview  display false when cancel button raise event
                grv.Visible = false;
                GridViewKStockGroup.Rows[rowindex].FindControl("Cancel0").Visible = false;
                GridViewKStockGroup.Rows[rowindex].FindControl("btn_Show0").Visible = true;
            }
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

            string Html = "<b>KARTU STOCK BJ";
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