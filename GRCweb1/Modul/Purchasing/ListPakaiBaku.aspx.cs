using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ListPakaiBaku : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(Request.QueryString["approve"].ToString());
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                LoadData(Request.QueryString["approve"].ToString());
            }
            else
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                ArrayList arrPakai = new ArrayList();

                if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                    arrPakai = pakaiFacade.BiayaRetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());
                if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                    arrPakai = pakaiFacade.AssetRetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());
                else
                    arrPakai = pakaiFacade.RetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());

                if (pakaiFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrPakai;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData(string strApprove)
        {
            try
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                ArrayList arrPakai = new ArrayList();

                if (strApprove.Substring(1, 1) == "C")
                    arrPakai = pakaiFacade.AssetRetrieveOpenStatus(strApprove);
                else if (strApprove.Substring(1, 1) == "B")
                    arrPakai = pakaiFacade.BiayaRetrieveOpenStatus(strApprove);
                else
                    arrPakai = pakaiFacade.RetrieveOpenStatus(strApprove);
                #region depreciated line
                //if (strApprove == "KP")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KP");
                //else if (strApprove == "KS")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KS");
                //else if (strApprove == "KM")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KM");
                //else if (strApprove == "KA")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KA");
                //else if (strApprove == "KO")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KO");
                //else if (strApprove == "KK")
                //    arrPakai = pakaiFacade.RetrieveOpenStatus("KK");
                //else if (strApprove == "KC")
                //    arrPakai = pakaiFacade.AssetRetrieveOpenStatus("KC");
                //else if (strApprove == "KB")
                //    arrPakai = pakaiFacade.BiayaRetrieveOpenStatus("KB");
                #endregion
                if (pakaiFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrPakai;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message.ToString());
            }

        }

        private void LoadDataReady(string strApprove)
        {
            try
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                ArrayList arrPakai = new ArrayList();

                if (strApprove.Substring(1, 1) == "C")
                    arrPakai = pakaiFacade.AssetRetrieveOpenStatus(strApprove);
                else if (strApprove.Substring(1, 1) == "B")
                    arrPakai = pakaiFacade.BiayaRetrieveOpenStatus(strApprove);
                else
                    arrPakai = pakaiFacade.RetrieveOpenStatusReady(strApprove);

                if (pakaiFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrPakai;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message.ToString());
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                if (Request.QueryString["approve"].ToString().Substring(1, 1) == "P")
                    Response.Redirect("FormPakaiBaku.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "S")
                    Response.Redirect("FormPakaiSparePart.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "M")
                    Response.Redirect("FormPakaiBantu.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "A")
                    Response.Redirect("FormPakaiATK.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "O")
                    Response.Redirect("FormPakaiProject.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "K")
                    Response.Redirect("FormPakaiMarketing.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                    Response.Redirect("FormPakaiAsset.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                    Response.Redirect("FormPakaiBiaya.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "R")
                    Response.Redirect("FormPakaiRepack.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "N")
                    Response.Redirect("FormPakaiNonGRC.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "E")
                    Response.Redirect("FormPakaiAsset.aspx?PakaiNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "G")
                    Response.Redirect("FormPakaiRePacknongrc.aspx?PakaiNo=" + row.Cells[0].Text);

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                //switch (e.Row.Cells[6].Text)
                //{
                //    case "0":
                //        e.Row.Cells[8].Text = "Open";
                //        break;
                //    case "1":
                //        e.Row.Cells[8].Text = "Head";
                //        break;
                //    case "2":
                //        e.Row.Cells[8].Text = "Gudang";
                //        break;
                //}
                //e.Row.Cells[6].Text = ((Status)int.Parse(e.Row.Cells[6].Text)).ToString();

                //e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["approve"].ToString().Substring(1, 1) == "P")
                Response.Redirect("FormPakaiBaku.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "S")
                Response.Redirect("FormPakaiSparePart.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "M")
                Response.Redirect("FormPakaiBantu.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "A")
                Response.Redirect("FormPakaiATK.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "O")
                Response.Redirect("FormPakaiProject.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "K")
                Response.Redirect("FormPakaiMarketing.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                Response.Redirect("FormPakaiAsset.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                Response.Redirect("FormPakaiBiaya.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "N")
                Response.Redirect("FormPakaiNonGRC.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "G")
                Response.Redirect("FormPakaiRePacknongrc.aspx");
        }

        protected void btnlist_ServerClick(object sender, EventArgs e)
        {
            Session["RadioBtn"] = "-";
            Response.Redirect("ListPakaiEx.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
        protected void RBAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadData(Request.QueryString["approve"].ToString());
        }
        protected void RBAll0_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataReady(Request.QueryString["approve"].ToString());
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}