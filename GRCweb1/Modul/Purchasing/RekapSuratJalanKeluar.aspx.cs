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
//using DefectFacade;
using Factory;
using System.IO;

namespace GRCweb1.Modul.Purchasing
{
    public partial class RekapSuratJalanKeluar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string drTgl = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
            string sdTgl = DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd");
            LblDr.Text = txtdrtanggal.Text;
            LblSd.Text = txtsdtanggal.Text;
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempSJK]') AND type in (N'U')) DROP TABLE [dbo].tempSJK " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempSJK1]') AND type in (N'U')) DROP TABLE [dbo].tempSJK1 " +
                "select ROW_NUMBER() OVER (ORDER BY NoSJ,TglSJ,Tujuan desc) AS Row, A.NoSJ,A.TglSJ,A.Tujuan,A.Ket,cast(A.Jumlah as int)Jumlah,A.NoPolisi, " +
                "case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)  " +
                "    when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode,  " +
                "Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID)  " +
                "    when 2 then (select ItemName from Asset where Asset.ID=A.ItemID)  " +
                "else itemname end ItemName,UOM into tempSJK from INV_suratjalan_keluar as A  " +
                "where  A.RowStatus > -1 and Convert(char, TglSJ ,112)>='" + drTgl + "' and Convert(char, TglSJ ,112)<='" + sdTgl + "' " +
                "select row, NoSJ,TglSJ,Tujuan,Ket,NoPolisi,ItemCode,ItemName,Jumlah,UOM into tempSJK1 from (  " +
                "select Row,case when (select count(NoSJ) from tempSJK where row<A.row and nosj=A.nosj)=0 then NoSJ else '' end NoSJ ,  " +
                "case when (select count(NoSJ) from tempSJK where row<A.row and nosj=A.nosj)=0 then TglSJ else Null end TglSJ,  " +
                "case when (select count(NoSJ) from tempSJK where row<A.row and nosj=A.nosj)=0 then Tujuan else '' end Tujuan,  " +
                "case when (select count(NoSJ) from tempSJK where row<A.row and nosj=A.nosj)=0 then Ket else '' end Ket,  " +
                "case when (select count(NoSJ) from tempSJK where row<A.row and nosj=A.nosj)=0 then NoPolisi else '' end NoPolisi,ItemCode,ItemName,Jumlah,UOM  from tempSJK A)X  " +
                "select case when cast(Nomor as int)=0 then '' else Nomor end nomor,NoSJ,TglSJ,Tujuan,Ket,NoPolisi,ItemCode,ItemName,Jumlah,UOM from (  " +
                "select row, cast (case when nosj<>'' then (select count(nosj) from tempSJK1 where row<A.row and rtrim(nosj)<>'')+1 else '' end as char) Nomor, " +
                "NoSJ,TglSJ,Tujuan,Ket,NoPolisi,ItemCode,ItemName,Jumlah,UOM from tempSJK1 A)A1 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempSJK]') AND type in (N'U')) DROP TABLE [dbo].tempSJK " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempSJK1]') AND type in (N'U')) DROP TABLE [dbo].tempSJK1";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RekapSJ.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Panel3.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}