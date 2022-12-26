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
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

using DataAccessLayer;
using Factory;
using Cogs;
using System.Web.Services;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
//using DefectFacade;
using System.IO;

namespace GRCweb1.Modul.ListReport
{
    public partial class PemantauanOptMinmaxBBBP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();

            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 350, 100 , 20 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            LoadListPantau();
        }
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());

            }
        }
        private void LoadListPantau()
        {
            LblTgl1.Text = string.Empty;
            int Bulan = int.Parse(ddlBulan.SelectedValue);
            int Tahun = int.Parse(ddTahun.SelectedValue);
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string SaldoAwal = string.Empty;

            string strSQL = string.Empty;
            strSQL =
                "declare @bulan int,@tahun int,@tahun0 int,@SA NVARCHAR(MAX),@lencol int, @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX), " +
                "@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), " +
                "@cols4 AS NVARCHAR(MAX),@cols5 AS NVARCHAR(MAX),@cols6 AS NVARCHAR(MAX),@cols7 AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX);  " +
                "set @bulan=" + ddlBulan.SelectedValue + " set @tahun=" + ddTahun.SelectedItem.Text + "" +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB1]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB1 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB2]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB2 " +
                "if @bulan=1 begin set @tahun0=@tahun-1 set @SA ='DesQty' end " +
                "if @bulan=2 begin set @tahun0=@tahun set @SA ='janQty' end " +
                "if @bulan=3 begin set @tahun0=@tahun set @SA ='febQty' end " +
                "if @bulan=4 begin set @tahun0=@tahun set @SA ='marQty' end " +
                "if @bulan=5 begin set @tahun0=@tahun set @SA ='aprQty' end " +
                "if @bulan=6 begin set @tahun0=@tahun set @SA ='meiQty' end " +
                "if @bulan=7 begin set @tahun0=@tahun set @SA ='junQty' end " +
                "if @bulan=8 begin set @tahun0=@tahun set @SA ='julQty' end " +
                "if @bulan=9 begin set @tahun0=@tahun set @SA ='aguQty' end " +
                "if @bulan=10 begin set @tahun0=@tahun set @SA ='sepQty' end " +
                "if @bulan=11 begin set @tahun0=@tahun set @SA ='oktQty' end " +
                "if @bulan=12 begin set @tahun0=@tahun set @SA ='novQty' end " +
                "select * into tmppantauBB from ( " +
                "select  itemid,right(rtrim(convert(char,tanggal,112)),2) tgl,sum(quantity) Qty  from vw_StockPurchn S where bulan =@bulan and tahun=@tahun and ItemID in  " +
                "(select id from Inventory where MaxStock >0 and groupid in (1,2)) group by tanggal,itemid)s  order by tgl,itemid  " +
                "select ItemID,tgl, Qty + isnull((select sum(qty) from tmppantauBB where tgl<A.tgl and itemid=A.ItemID ),0) Qty into tmppantauBB1  from tmppantauBB A order by ItemID,tgl " +
                "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(Tgl)  FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols1 = STUFF((SELECT distinct ',case when isnull(' + QUOTENAME(Tgl) +',0)=0 then isnull((select top 1 qty from tmppantauBB1 where tgl < '+ Tgl+  " +
                "' and itemid=Q.itemid order by tgl desc),0) else ' + QUOTENAME(Tgl) + ' end ' + QUOTENAME(Tgl) FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols2 = STUFF((SELECT distinct ',isnull(' + QUOTENAME(Tgl) + ',0) ' + QUOTENAME(Tgl) FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols3 = STUFF((SELECT distinct ',SA+isnull(' + QUOTENAME(Tgl) + ',0) ' + QUOTENAME(Tgl) FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols4 = STUFF((SELECT distinct ',' + QUOTENAME(Tgl) + ',case when isnull(' + QUOTENAME(Tgl) + ',0) >=Min and isnull(' + QUOTENAME(Tgl) + ',0)<=Max then 1 else 0 end sta'  + Tgl  " +
                "    FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols5 = STUFF((SELECT distinct ',Null ' + QUOTENAME(Tgl) + ',sum(sta'  + Tgl +') sta'  + Tgl FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols6 = STUFF((SELECT distinct  ' sum(sta'  + Tgl +') +'  FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols7 = STUFF((SELECT distinct ',Null ' + QUOTENAME(Tgl) + ',Null sta'  + Tgl FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "set  @lencol=len(@cols6) " +
                "select @cols6 =left('('+@cols6+')',@lencol)+')'  " +
                "set @query =  " +
                "'select Itemcode,ItemName,UOMCode Sat, [Min], [Max],' + @cols4 + ' into tmppantauBB2 from ('+ " +
                "'select ID,itemcode,ItemName,UOMCode,MinStock Min,MaxStock Max,' + @cols3 + ' from ('+ " +
                "'select A.ID,A.itemcode,A.ItemName,U.UOMCode,A.MinStock,A.MaxStock, " +
                "(select '+@SA +' from SaldoInventory where YearPeriod='+cast(@tahun0 as char) +' and ItemID=A.ID ) SA,' + @cols2 + '  from Inventory A inner join UOM U on A.UOMID=U.ID   " +
                " left join' + " +
                "'(select itemid,' + @cols1 +' from ('+ " +
                "'SELECT itemid, ' + @cols +' from  (select itemid, tgl, isnull(Qty,0) + isnull((select sum(qty) from tmppantauBB where tgl<A.tgl and itemid=A.ItemID ),0) qty from tmppantauBB A) x  " +
                "pivot(sum(qty) for tgl in (' + @cols + ')) p)q) r on A.id=r.itemid where A.GroupID in (1,2) and A.RowStatus>-1 and A.Aktif=1 and A.MinStock>0 and A.MaxStock>0)S)T order by itemname   " +
                "select * from tmppantauBB2  " +
                "union all  " +
                "select ''Total'' itemcode,Null ItemName,Null UOMCode,Null [Min], Null [Max],' + @cols5 + ' from tmppantauBB2  " +
                "/*union all  " +
                "select ''Total Std MinMax'' itemcode,cast('+@cols6 +' as char) ItemName,Null UOMCode,Null [Min], Null [Max],' + @cols7 + ' from tmppantauBB2 */' " +
                "execute(@query)  " +
                "/*IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB1]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB1 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmppantauBB2]') AND type in (N'U')) DROP TABLE [dbo].tmppantauBB2*/";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            string col1 = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";

                if (bfield.HeaderText.Trim() == "Itemcode" || bfield.HeaderText.Trim() == "ItemName" || bfield.HeaderText.Trim() == "Sat")
                { bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left; }
                else
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                if (bfield.HeaderText.Trim().Length > 2)
                {
                    if (bfield.HeaderText.Trim().Substring(0, 3) == "sta")
                    {
                        bfield.HeaderText = "sta";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                }
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            LblTgl1.Text = ddlBulan.SelectedItem.Text + " " + ddTahun.SelectedItem.Text;

            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            int total = 0;
            int items = 0;
            zv.CustomQuery =
                "declare @query AS NVARCHAR(MAX),@cols6 AS NVARCHAR(MAX) " +
                "select @cols6 = STUFF((SELECT distinct  ' sum(sta'  + Tgl +') +'  FROM tmppantauBB c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
                "select @cols6 =left('('+@cols6+')',len(@cols6))+')' " +
                "set @query='select cast('+@cols6 +' as char) Total ,count(itemcode)items from tmppantauBB2' " +
                "execute(@query) ";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    total = Int32.Parse(dr["Total"].ToString());
                    items = Int32.Parse(dr["items"].ToString());
                }
            }
            Lbl_0.Text = total.ToString();
            string thnbln = ddTahun.SelectedItem.Text + ddlBulan.SelectedValue.PadLeft(2, '0');
            int aktif = Convert.ToInt32(thnbln);
            if (aktif < 202101)
                Lbl_1.Text = "930";
            else
            {
                int totalitem = items;
                Lbl_1.Text = (totalitem * System.DateTime.DaysInMonth(Convert.ToInt32(ddTahun.SelectedItem.Text), Convert.ToInt32(ddlBulan.SelectedValue))).ToString();
            }
            //Lbl_2.Text = (Convert.ToDecimal(Lbl_1.Text) / Convert.ToDecimal(Lbl_0.Text)).ToString("N1") + " %";
            Lbl_3.Text = ((Convert.ToDecimal(Lbl_0.Text) / Convert.ToDecimal(Lbl_1.Text) * 100)).ToString("N1") + " %";
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PemantauanSTDMinMax_" +
                ddlBulan.SelectedItem.Text + ddTahun.SelectedValue + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}