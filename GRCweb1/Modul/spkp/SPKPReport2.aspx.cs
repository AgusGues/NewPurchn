using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.SPKP
{
    public partial class SPKPReport2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btninput_Click(object sender, EventArgs e)
        {
            Response.Redirect("SPKP.aspx");
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            string a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=spkp.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<center><b>SURAT PERINTAH KERJA - PRODUKSI </center>";
            Html += "<center><b>(SPKP) </center> ";
            Html += "<center><b>JADWAL PRODUKSI </center>  ";
            Html += "<p align:'right'>   &ensp;" + nospkp.Text+ "  &ensp;&ensp;   " + a + "</p>";


            string HtmlEnd = "";

            div3.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void btnpreview_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();

            string strSQL = string.Empty;
            strSQL = "select top 1tanggal from spkp_detail where idspkp in(select id from spkp where nospkp='" + nospkp.Text + "')";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            //SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            //da.SelectCommand.CommandTimeout = 0;
            SqlCommand command = new SqlCommand(strSQL, sqlCon);
            //connection.Open();

            //SqlDataReader reader = command.ExecuteReader();

            SqlDataReader sdr = command.ExecuteReader();
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new ListType
                    {
                        tanggal = Convert.ToDateTime(sdr["tanggal"].ToString())
                    });
                }
            }

            lstspkp.DataSource = arrData;
            lstspkp.DataBind();
            
        }

        public class ListType : GRCBaseDomain
        {
            public DateTime tanggal { get; set; }
        }

        
        protected void lstspkp_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            ListType lt = (ListType)e.Item.DataItem;
            //Repeater rpt = (Repeater)e.Item.FindControl("lstpacking");
            //DataGrid dg = (DataGrid)e.Item.FindControl("GrdDynamic");
            GridView dg = (GridView)e.Item.FindControl("GrdDynamic");
            GridView dg2 = (GridView)e.Item.FindControl("GrdDynamic2");
            GridView dg3 = (GridView)e.Item.FindControl("GrdDynamic3");
            GridView dg4 = (GridView)e.Item.FindControl("GrdDynamic4");
            GridView dg5 = (GridView)e.Item.FindControl("GrdDynamic5");
            GridView dg6 = (GridView)e.Item.FindControl("GrdDynamic6");
            grid1(lt.tanggal, dg);
            grid2(lt.tanggal, dg2);
            grid3(lt.tanggal, dg3);
            grid4(lt.tanggal, dg4);
            grid5(lt.tanggal, dg5);
            grid6(lt.tanggal, dg6);
            
            gridtotal1();
            gridtotal2();
            gridtotal3();
            gridtotal4();
            gridtotal5();
            gridtotal6();

            GridView dgtgl = (GridView)e.Item.FindControl("GridTanggal");
            gridtanggal(dgtgl);

        }

        public void gridtanggal(GridView grv)
        {
             string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp " +

                    " select* into tmpspkp from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "'))tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT CONVERT(DATE,tanggal)Tanggal,shift from ( " +
                    " select tanggal, shift, target, partno from tmpspkp " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}
                if (bfield.HeaderText.Trim() == "Tanggal")
                {
                    bfield.DataFormatString = "{0:d}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }
        public void grid1(DateTime i,GridView grv)
        {
            //int rowindex = i;
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            //GridView grv = (GridView)GridViewspkp.Rows[rowindex].FindControl("GrdDynamic");
            //DataGrid dg = (DataGrid)e.Item.FindControl("myDataGrid");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp " +

                    " select* into tmpspkp from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=1)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}
                

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void grid2(DateTime i, GridView grv)
        {
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp2 " +

                    " select* into tmpspkp2 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=2)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp2 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp2 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void grid3(DateTime i, GridView grv)
        {
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp3 " +

                    " select* into tmpspkp3 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=3)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp3 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp3 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void grid4(DateTime i, GridView grv)
        {
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp4 " +

                    " select* into tmpspkp4 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "')and  line=4)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp4 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp4 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void grid5(DateTime i, GridView grv)
        {
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp5 " +

                    " select* into tmpspkp5 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=5)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp5 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp5 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void grid6(DateTime i, GridView grv)
        {
            DateTime a = i;
            string aa = a.ToString("yyyyMMdd");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp6 " +

                    " select* into tmpspkp6 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "')and line=6)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp6 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select tanggal, shift, target, partno from tmpspkp6 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p order by tanggal, shift' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            grv.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                bfield.HeaderText = col.ColumnName;
                //}

                grv.Columns.Add(bfield);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }

        public void gridtotal1()
        {
            //int rowindex = i;
            //GridView grv = (GridView)GridViewspkp.Rows[rowindex].FindControl("GrdDynamic");
            //DataGrid dg = (DataGrid)e.Item.FindControl("myDataGrid");
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp " +

                    " select* into tmpspkp from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=1)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select target, partno from tmpspkp " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p ' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal.Columns.Add(bfield);
            }
            GrdDyntotal.DataSource = dt;
            GrdDyntotal.DataBind();
        }

        public void gridtotal2()
        {
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp2 " +

                    " select* into tmpspkp2 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=2)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp2 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select  target, partno from tmpspkp2 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p ' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal2.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal2.Columns.Add(bfield);
            }
            GrdDyntotal2.DataSource = dt;
            GrdDyntotal2.DataBind();
        }

        public void gridtotal3()
        {
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp3 " +

                    " select* into tmpspkp3 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=3)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp3 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select  target, partno from tmpspkp3 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p ' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal3.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal3.Columns.Add(bfield);
            }
            GrdDyntotal3.DataSource = dt;
            GrdDyntotal3.DataBind();
        }

        public void gridtotal4()
        {
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp4 " +

                    " select* into tmpspkp4 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=4)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp4 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select  target, partno from tmpspkp4 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p ' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal4.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal4.Columns.Add(bfield);
            }
            GrdDyntotal4.DataSource = dt;
            GrdDyntotal4.DataBind();
        }

        public void gridtotal5()
        {
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp5 " +

                    " select* into tmpspkp5 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "') and line=5)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp5 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select  target, partno from tmpspkp5 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal5.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal5.Columns.Add(bfield);
            }
            GrdDyntotal5.DataSource = dt;
            GrdDyntotal5.DataBind();
        }

        public void gridtotal6()
        {
            string strSQL = string.Empty;
            strSQL = " DECLARE @cols AS NVARCHAR(MAX), " +
                    " @query AS NVARCHAR(MAX) " +

                    " IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpspkp]') AND type in (N'U')) DROP TABLE[dbo].tmpspkp6 " +

                    " select* into tmpspkp6 from ( " +
                    " select Tanggal, Shift, line, Tebal,  Kategori+' '+tebal+'MM '+ Ukuran +' ' + case when Keterangan='x' then 'x' else ' ' end as partno, Target from SPKP_Detail where IDSPKP in(select id from spkp where NoSPKP = '" + nospkp.Text + "')  and line=6)tmp " +

                    " select @cols = STUFF((SELECT ',' + QUOTENAME(partno) from tmpspkp6 group by partno order by partno " +
                    " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +

                    " set @query = 'SELECT ' + @cols + ' from ( " +
                    " select  target, partno from tmpspkp6 " +
                    " ) x pivot(sum(target)for partno in (' + @cols + '))p ' " +

                    " execute(@query); ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;

            GrdDyntotal6.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //bfield.HeaderText = col.ColumnName;
                //}

                GrdDyntotal6.Columns.Add(bfield);
            }
            GrdDyntotal6.DataSource = dt;
            GrdDyntotal6.DataBind();
        }

    }
}