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
using Domain;
using System.Diagnostics;
using System.IO;



namespace GRCweb1.Modul.Mtc
{
    public partial class LapEffesiensiPMNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                var startdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                txtDari.Text = startdate.ToString("dd-MMM-yyyy");
                txtSampai.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
             ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (rbList.Items[0].Selected)
            {
                //tampilkan rekap nya dulu SortOrder=2
                lstRekap.Visible = true;
                lstDetail.Visible = false;
                MTC_SarmutFacade mtc = new MTC_SarmutFacade();
                mtc.Where = "WHERE sg.SortOrder=2 ";
                mtc.OrderBy = " ORDER BY mg.Kode,sg.GroupName,SortOrder ";
                mtc.Bulan = 9;
                mtc.Tahun = 2016;
                mtc.WhereDept = (ddlDept.SelectedIndex > 0) ? " AND p.DeptID=" + ddlDept.SelectedValue.ToString() : "";
                mtc.StartDates = DateTime.Parse(txtDari.Text).ToString("yyyyMMdd");
                mtc.EndDates = DateTime.Parse(txtSampai.Text).ToString("yyyyMMdd");
                ArrayList arrData = mtc.Retrieve(true);
                ListRekap.DataSource = arrData;
                ListRekap.DataBind();
            }
            else
            {
                DetailSarmut();
            }
            sw.Stop();
            txtTime.Text = string.Format("Total Execute Time :{0:hh\\:mm\\:ss}", sw.Elapsed);
        }
        private void DetailSarmut()
        {
            MTC_SarmutFacade mtc = new MTC_SarmutFacade();
            lstRekap.Visible = false;
            lstDetail.Visible = true;
            mtc.Where = "WHERE sg.SortOrder=2 ";
            mtc.OrderBy = " ORDER BY mg.Kode,sg.GroupName,SortOrder ";
            mtc.Bulan = 9;
            mtc.Tahun = 2016;
            mtc.WhereDept = (ddlDept.SelectedIndex > 0) ? " AND p.DeptID=" + ddlDept.SelectedValue.ToString() : "";
            mtc.StartDates = DateTime.Parse(txtDari.Text).ToString("yyyyMMdd");
            mtc.EndDates = DateTime.Parse(txtSampai.Text).ToString("yyyyMMdd");
            ArrayList arrData = mtc.Retrieve(true);
            lstSarmut.DataSource = arrData;
            lstSarmut.DataBind();
        }
        protected void lstSarmutDataBound(object sender, RepeaterItemEventArgs e)
        {
            MTC_SarmutFacade mtc = new MTC_SarmutFacade();
            SarmutMaintenance sm = (SarmutMaintenance)e.Item.DataItem;
            Repeater lstItem = (Repeater)e.Item.FindControl("ListDetail");
            ArrayList arrData = new ArrayList();
            mtc.Where = "WHERE sg.SortOrder=1 AND mg.ID=" + sm.ID.ToString();
            mtc.OrderBy = " ORDER BY PakaiNo,ItemName ";
            mtc.WhereDept = (ddlDept.SelectedIndex > 0) ? " AND p.DeptID=" + ddlDept.SelectedValue.ToString() : "";
            mtc.StartDates = DateTime.Parse(txtDari.Text).ToString("yyyyMMdd");
            mtc.EndDates = DateTime.Parse(txtSampai.Text).ToString("yyyyMMdd");
            //arrData = mtc.Retrieve(true, "Detail");
            lstItem.DataSource = arrData;
            lstItem.DataBind();
        }
        protected void lstSarmut_Command(object sender, RepeaterCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            Repeater lstItem = (Repeater)e.Item.FindControl("ListDetail");
            ArrayList arrData = new ArrayList();
            switch (e.CommandName)
            {
                case "show":
                    MTC_SarmutFacade mtc = new MTC_SarmutFacade();

                    mtc.Where = "WHERE sg.SortOrder=1 AND mg.ID=" + ID;
                    mtc.OrderBy = " ORDER BY PakaiNo,ItemName ";
                    mtc.WhereDept = (ddlDept.SelectedIndex > 0) ? " AND p.DeptID=" + ddlDept.SelectedValue.ToString() : "";
                    mtc.StartDates = DateTime.Parse(txtDari.Text).ToString("yyyyMMdd");
                    mtc.EndDates = DateTime.Parse(txtSampai.Text).ToString("yyyyMMdd");
                    arrData = mtc.Retrieve(true, "Detail");
                    lstItem.DataSource = arrData;
                    lstItem.DataBind();
                    break;

            }
        }
        protected void ListRekap_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SarmutMaintenance sm = (SarmutMaintenance)e.Item.DataItem;
            decimal Totals = sm.TotalPrice;
            txtTotals.Text = sm.TotalPrice.ToString("N2");
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LapEfesiensiOM.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();

            Response.Write("<table>");
            Response.Write(sw.ToString());

            Response.Write("</table>");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LAPORAN EFFESIENSI PM</b>";
            Html += "<br>Periode :  " ;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents.Trim() + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            //    if (GrdDynamic.Rows.Count == 0)
            //        return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Defect Group BM.xls"));
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            lstDetail.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rbList.SelectedIndex == 1)
            //    btnExport.Visible = true;
            //else
            //    btnExport.Visible = false;
        }
    }
}