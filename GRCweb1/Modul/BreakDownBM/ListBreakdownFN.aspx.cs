using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.BreakDownBM
{
    public partial class ListBreakdownFN : System.Web.UI.Page
    {
        string totalmenit = string.Empty;
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadNamaOven();
                LoadCategoriUraian();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void ddlPemotongOutputOven_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPemotongOutputOven.SelectedItem.Text == "--Pilih--")
            {
                ddlCategoriUraian.Visible = true;
            }
            else
            {
                ddlCategoriUraian.Visible = false;
            }

        }

        protected void ddlCategoriUraian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoriUraian.SelectedItem.Text == "-- Pilih Category Uraian --")
            {
                ddlPemotongOutputOven.Visible = true;
            }
            else
            {
                ddlPemotongOutputOven.Visible = false;
            }

        }

        static public void DisplayAJAXMessage(Control page, string msg, bool status)
        {

            string myScript = "confirm('" + msg + "');";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, status);
        }

        protected void lstListBdt2_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            Image upd = (Image)e.Item.FindControl("updSts");
            HtmlGenericControl spn = (HtmlGenericControl)e.Item.FindControl("ext");
            string cmd = e.CommandName.ToString();
            string ID = e.CommandArgument.ToString();

            if (cmd == "upd")
            {

                bool status = true;
                DisplayAJAXMessage(this, "Apakah akan menghapus data ini", status);

                if (status)
                {
                    BreakDownFN fn = new BreakDownFN();
                    fn.ID = int.Parse(ID.ToString());
                    int result = new BreakDownFNFacade().UpdateRowStatus(fn);
                }


            }

            ListBreakdownFN2();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Modul/BreakDownBM/InputBreakdownFN.aspx");
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        private void LoadTahun()
        {
            ArrayList arrD = this.ListTahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadCategoriUraian()
        {
            ArrayList arrNamaOven = new ArrayList();
            BreakDownFNFacade breakDFn = new BreakDownFNFacade();
            arrNamaOven = breakDFn.RetrieveCategoryUraian();

            ddlCategoriUraian.Items.Add(new ListItem("-- Pilih Category Uraian --", "0"));
            foreach (BreakDownFN naOven in arrNamaOven)
            {
                ddlCategoriUraian.Items.Add(new ListItem(naOven.UraianCat, naOven.ID.ToString()));
            }
        }

        private void LoadNamaOven()
        {
            ArrayList arrNamaOven = new ArrayList();
            BreakDownFNFacade breakDFn = new BreakDownFNFacade();
            arrNamaOven = breakDFn.RetrieveNamaOven();

            ddlNamaOven.Items.Add(new ListItem("-- Pilih Oven --", "0"));
            foreach (BreakDownFN naOven in arrNamaOven)
            {
                ddlNamaOven.Items.Add(new ListItem(naOven.NamaOven, naOven.ID.ToString()));
            }
        }

        private ArrayList ListTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT YEAR(Minta)Tahun From SPP Order By YEAR(Minta)Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new BeritaAcara
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new BeritaAcara { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //if (ddlNamaOven.SelectedIndex == 0)
            //{
            //    DisplayAJAXMessage(this, "Pilih Nama Oven!!!");
            //    return;
            //}
            ListBreakdownFN2();
        }

        private void ListBreakdownFN2()
        {
            BreakDownFNFacade bdtFN = new BreakDownFNFacade();
            arrData = new ArrayList();
            arrData = bdtFN.RetrieveListBdtFN(ddlNamaOven.SelectedValue.ToString());
            lstListBdt.DataSource = arrData;
            lstListBdt.DataBind();
        }

        protected void lstListBdt_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstListBdt_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater lstListBdt2 = (Repeater)e.Item.FindControl("lstListBdt2");
            Label Tgl = (Label)e.Item.FindControl("TglBreak");
            BreakDownFN bdtfnD = (BreakDownFN)e.Item.DataItem;
            BreakDownFNFacade bdtfnF = new BreakDownFNFacade();
            ArrayList arrData = new ArrayList();

            if (ddlPemotongOutputOven.SelectedItem.Text == "Oven Mati, PLN Mati, Batu Bara Habis, Produk yang akan di oven habis")
            {
                arrData = bdtfnF.RetrieveListBdtFNAllPemotongan(ddlTahun.SelectedValue, ddlBulan.SelectedValue.ToString(), bdtfnD.OvenID.ToString(), ddlCategoriUraian.SelectedValue.ToString());
                lstListBdt2.DataSource = arrData;
                lstListBdt2.DataBind();
            }
            else
            {
                arrData = bdtfnF.RetrieveListBdtFNAll(ddlTahun.SelectedValue, ddlBulan.SelectedValue.ToString(), bdtfnD.OvenID.ToString(), ddlCategoriUraian.SelectedValue.ToString());
                lstListBdt2.DataSource = arrData;
                lstListBdt2.DataBind();
            }


        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            foreach (RepeaterItem img in lstListBdt.Items)
            {
                Repeater rpt = (Repeater)img.FindControl("lstListBdt2");

                foreach (RepeaterItem rm in rpt.Items)
                {
                    //HtmlTableCell tc = (HtmlTableCell)rm.FindControl("hapusgrid");
                    Image im = (Image)rm.FindControl("UpdSts");

                    im.Visible = false;
                    //tc.Visible = false;


                }
            }


            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListBreakdownOven.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>REKAPITULASI BREAKDOWN</H2>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " &nbsp; " + ddlTahun.SelectedValue.ToString();
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void lstListBdt2_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstListBdt2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label TglBreak = (Label)e.Item.FindControl("TglBreak");

                if (TglBreak.Text.Trim() == "01/01/1900")
                {
                    TglBreak.Text = "Total Waktu";
                }







            }

        }

        protected void lstListBdt2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trr");

                tr.Cells[0].InnerText = "";

                Label L2 = e.Item.FindControl("NmMasterCatID") as Label;


                if (L2.Text.Trim() == "17")
                {
                    L2.Visible = false;
                }
                else
                {
                    L2.Visible = true;
                }
            }
        }
    }
}