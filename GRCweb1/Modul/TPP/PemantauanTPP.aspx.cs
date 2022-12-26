using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.TPP
{
    public partial class PemantauanTPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                LoadMasalah();
                txtdrtanggal.Text = "01-Jan-" + DateTime.Now.ToString("yyyy");
                txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }

        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(BtnExport);
        }
        protected void BtnPreview2_Click(object sender, EventArgs e)
        {
            LoadTPP();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            //Response.AddHeader("content-disposition", "attachment;filename=PemantauanBudgetDelivery.xls");
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanTPP.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            if (ddlDepartemen.SelectedItem.ToString() == "ALL")
            {
                string Html = "PEMANTAUAN TPP SEMUA DEPARTMENT ";
                Html += "<br>Periode : " + txtdrtanggal.Text + " s/d " + txtsdtanggal.Text;
                Html += "<br>";
                string HtmlEnd = "";
                lst.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else
            {
                string Html = "PEMANTAUAN TPP DEPARTMENT : " + ddlDepartemen.SelectedItem.ToString();
                Html += "<br>Periode : " + txtdrtanggal.Text + " s/d " + txtsdtanggal.Text;
                Html += "<br>";
                string HtmlEnd = "";
                lst.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            //string Html = "PEMANTAUAN TPP " + ddlDepartemen.SelectedItem.ToString();
            //Html += "<br>Periode : " + txtdrtanggal.Text + " s/d " + txtsdtanggal.Text;
            //Html += "<br>";
            //string HtmlEnd = "";
            //lst.RenderControl(hw);
            //string Contents = sw.ToString();
            //Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            //Response.Write(Html + Contents + HtmlEnd);
            //Response.Flush();
            //Response.End();
        }

        private void LoadTPP()
        {
            string kriteria = string.Empty; string tgl1 = string.Empty; string tgl2 = string.Empty;
            //tgl1 = Convert.ToDateTime(txtdrtanggal.Text.Trim()).ToString("yyyyMMdd");
            //tgl2 = Convert.ToDateTime(txtsdtanggal.Text.Trim()).ToString("yyyyMMdd");
            DateTime t1 = DateTime.Now; DateTime t2 = DateTime.Now;

            if (ddlDepartemen.SelectedValue != "0")
                kriteria = kriteria + " and dept_ID=" + ddlDepartemen.SelectedValue;

            if (ddlMasalah.SelectedValue != "0")
                kriteria = kriteria + " and Asal_M_ID=" + ddlMasalah.SelectedValue;

            t1 = Convert.ToDateTime(txtdrtanggal.Text); t2 = Convert.ToDateTime(txtsdtanggal.Text);
            tgl1 = t1.ToString("yyyyMMdd");
            tgl2 = t2.ToString("yyyyMMdd");

            TPP_DeptFacade tpp_facade = new TPP_DeptFacade();
            ArrayList arrTPP = new ArrayList();
            arrTPP = tpp_facade.RetrieveData_TPP(kriteria, tgl1, tgl2);

            DataTPP.DataSource = arrTPP;
            DataTPP.DataBind();
        }

        protected void DataTPP_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            string kriteria = string.Empty;
            if (ddlDepartemen.SelectedValue != "0")
                kriteria = kriteria + " and dept_ID=" + ddlDepartemen.SelectedValue;
            if (ddlMasalah.SelectedValue != "0")
                kriteria = kriteria + " and Asal_M_ID=" + ddlMasalah.SelectedValue;

            string strQuery = "select A.ID,A.Laporan_No,A.CreatedTime,A.TPP_Date,B.Departemen, " +
                "isnull((select username from Users where ID in (select top 1 mgrid from TPP_ListApproval where UserID=A.User_ID )),'') PIC, " +
                "A.Keterangan Keterangan,A.Uraian ,A.Ketidaksesuaian,case when ISNULL(A.Closed,0)=0 then 'Open' else 'CLosed' end Status   " +
                "from tpp A inner join TPP_Dept B on A.Dept_ID=B.ID where convert(char,A.tpp_date,112)>='" +
                Convert.ToDateTime(txtdrtanggal.Text.Trim()).ToString("yyyyMMdd") + "' and convert(char,A.tpp_date,112)<='" +
                Convert.ToDateTime(txtsdtanggal.Text.Trim()).ToString("yyyyMMdd") + "' and A.RowStatus>-1 " + kriteria;

            string strQuery1 = "select C.Laporan_No,A.Penyebab,B.Uraian  from TPP_Penyebab A inner join TPP_Penyebab_Detail B on A.ID=B.Penyebab_ID inner join TPP C on C.ID=B.TPP_ID  " +
                    "where C.RowStatus>-1 and B.RowStatus>-1";
            string strQuery2 = "select B.Laporan_No,A.Jenis,A.Tindakan,A.TglVerifikasi tanggal  from TPP_Tindakan A inner join TPP B on A.TPP_ID=B.ID  where A.rowstatus>-1";
            if (txtdrtanggal.Text.Trim() != string.Empty)
            {
                Session["Query"] = strQuery;
                Session["Query1"] = strQuery1;
                Session["Query2"] = strQuery2;
                Session["periode"] = txtdrtanggal.Text.Trim() + " s/d " + txtsdtanggal.Text.Trim();
                Cetak(this);
            }
        }
        protected void LoadMasalah()
        {

            ddlMasalah.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.RetrieveMasalah();
            ddlMasalah.Items.Add(new ListItem("ALL", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlMasalah.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
        }
        private void LoadDept()
        {
            ddlDepartemen.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDepartemen.Items.Add(new ListItem("ALL", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlDepartemen.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDepartemen.SelectedValue = tppdept.ID.ToString();
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=pemantauantpp', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}