using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReport
{
    public partial class MonOutStandingPO : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadTipeSPP();
                LoadBulan();
                LoadTahun();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }




        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Group Purchn --", string.Empty));
            ddlTipeSPP.Items.Add(new ListItem("ALL", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadTahun()
        {
            POPurchnFacade ba = new POPurchnFacade();
            ba.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
        }

        private void MonOutPO()
        {
            Bulan = ddlBulan.SelectedValue;
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            ArrayList arrData = new ArrayList();
            arrData = pOPurchnFacade.RetrieveMonOutPO(ddlBulan.SelectedValue, ddlTahun.SelectedValue, ddlTipeSPP.SelectedValue);
            Mpo.DataSource = arrData;
            Mpo.DataBind();

        }

        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            MonOutPO();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=MonOutPO.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            //FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           // StreamReader sr = fi.OpenText();
            //while (sr.Peek() >= 0)
            //{
            //    sb.Append(sr.ReadLine());
            //}
            //sr.Close();
            //Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>MONITORING OUTSTANDING PO 3 DAYS AGO</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
            excel.RenderControl(hw);
            //Mpo.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();


            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Buffer = true;
            //Response.BufferOutput = true;
            //Response.AddHeader("content-disposition", "attachment;filename=PemantauanStatusSPP.xls");
            //Response.Charset = "utf-8";
            //Response.ContentType = "application/vnd.ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string Html = "<b>MONITORING OUTSTANDING PO 3 DAYS AGO</b>";
            //Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
            //Html += "<br><form id='frm1' runat='server' method='post'>";
            //string HtmlEnd = "</form>";
            ////lstForPrint.RenderControl(hw);
            //excel.RenderControl(hw);
            //string Contents = sw.ToString();
            ////Contents = Contents.Replace("", "");
            //Response.Write(Html + Contents + HtmlEnd);
            //Response.Flush();
            //Response.End();
        }

        protected void Mpo_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Mpo_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {

        }
    }
}