using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Factory;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using System.IO;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class PemantauanOven : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                getYear();
            }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 30 ,false); </script>", false);
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
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
        protected void Button1_Click(object sender, EventArgs e)
        {

            LblPeriode.Text = ddlOven.SelectedItem.Text;
            LblTgl1.Text = ddlBulan.SelectedItem.Text; ;
            LblTgl2.Text = ddTahun.SelectedItem.Text; ;
            loadDynamicGrid();
            updatesarmut();
            //loadDynamicGridwet(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        //private void loadDynamicGrid(string tgl1, string tgl2)
        //{
        //    string strtanggal = string.Empty;
        //    if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
        //    {
        //        return;
        //    }
        //    DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
        //    DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
        //    string tglakhir = string.Empty;
        //    int tambahtgl = 0;
        //    long jmlHari = DateDiff(DateInterval.Day, intgl1, intgl2) + 1;
        //    if (jmlHari > 31)
        //    {
        //        string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
        //            DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("dd MMMM yyyy");
        //        tglakhir = DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("MM/dd/yyyy");
        //        Session["periode"] = periode;
        //    }
        //    else
        //    {
        //        string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
        //            DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
        //        tglakhir = DateTime.Parse(txtsdtanggal.Text).ToString("MM/dd/yyyy");
        //        Session["periode"] = periode;
        //    }
        //    string tglproses = string.Empty;
        //    string sqlselect = string.Empty;
        //    string sqlinpivot = string.Empty;
        //    string sqlselect1 = string.Empty;
        //    string sqlinpivot1 = string.Empty;
        //    for (int i = 0; i < 31; i++)
        //    {
        //        if (i == 0)
        //            tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
        //        else
        //            tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
        //        if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
        //        {
        //            if (i < 30)
        //            {
        //                sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
        //                    "[" + (i + 1).ToString() + "],";
        //                sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],";
        //            }
        //            else
        //            {
        //                sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                   DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
        //                   "[" + (i + 1).ToString() + "]";
        //                sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "]";
        //            }

        //        }
        //        else
        //        {
        //            if (i < 30)
        //                sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //                "[" + (i + 1).ToString() + "],";
        //            else
        //                sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //                "[" + (i + 1).ToString() + "]";
        //            if (i < 30)
        //                sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
        //            else
        //                sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
        //        }
        //        #region remark
        //        //}
        //        //else
        //        //{
        //        //    sqltanggal[i] = DateTime.Parse("01/01/1900").ToString("MM/dd/yyyy");
        //        //    if (i < 30)
        //        //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //        //        "[" + (i + 1).ToString() + "],";
        //        //    else
        //        //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //        //        "[" + (i + 1).ToString() + "]";
        //        //    if (i < 30)
        //        //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
        //        //    else
        //        //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
        //        //}
        //        //string tempstr = sqltanggal[i];
        //        #endregion

        //    }
        //    if (RBTglInput.Checked == true)
        //        strtanggal = "tglInput";
        //    if (RBTglProduksi.Checked == true)
        //        strtanggal = "TglProd";
        //    if (RBTglPotong.Checked == true)
        //        strtanggal = "TglPeriksa";
        //    string strSQL = "declare @tgl1 varchar(max) " +
        //        "declare @tgl2 varchar(max) " +
        //        "set @tgl1 ='" + tgl1 + "' " +
        //        "set @tgl2 ='" + tgl2 + "' " +
        //        "select  oven Oven,"  + sqlselect +
        //        " from (select Oven,CONVERT(char,tgljemur,112)hari, sum(QtyIn)QtyIn from T1_Jemur " +
        //        "where destid in (select destid from t1_serah where itemid0 in(select ID from fc_items " + 
        //        "where partno in (select partno from t1_partnooven where rowstatus>-1 and proses='dry cut')) and convert(char,tglserah,112)>=@tgl1  " +
        //        "and convert(char,tglserah,112)<=@tgl2  ) and  ISNULL(oven,'')<>'' and convert(char,tgljemur,112)>=@tgl1  " +
        //        "and convert(char,tgljemur,112)<=@tgl2 group by Oven,TglJemur " +
        //        "union all " +
        //        "select 'Total' Oven,CONVERT(char,tgljemur,112)hari, sum(QtyIn)QtyIn from T1_Jemur " +
        //        "where destid in (select destid from t1_serah where itemid0 in(select ID from fc_items " +
        //        "where partno in (select partno from t1_partnooven where rowstatus>-1 and proses='dry cut')) and convert(char,tglserah,112)>=@tgl1 " +
        //        "and convert(char,tglserah,112)<=@tgl2  ) and  ISNULL(oven,'')<>'' and convert(char,tgljemur,112)>=@tgl1 " +
        //        "and convert(char,tgljemur,112)<=@tgl2 group by TglJemur ) up pivot (sum(QTYin)for hari in (" + sqlinpivot + ")) as A1 ";

        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //    sqlCon.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
        //    da.SelectCommand.CommandTimeout = 0;
        //    #region Code for preparing the DataTable
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
        //    dcol.AutoIncrement = true;
        //    #endregion
        //    GrdDynamic.Columns.Clear();
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        BoundField bfield = new BoundField();

        //        bfield.DataField = col.ColumnName;
        //        bfield.HeaderText = col.ColumnName;

        //        GrdDynamic.Columns.Add(bfield);
        //    }
        //    GrdDynamic.DataSource = dt;
        //    GrdDynamic.DataBind();
        //}
        private void loadDynamicGrid()
        {
            string oven = string.Empty;
            string strSQL = string.Empty;
            Users users = (Users)HttpContext.Current.Session["Users"];

            if (users.UnitKerjaID == 13)
            {
                if (ddlOven.SelectedIndex == 5)
                {
                    oven = "1,2,3,4";
                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpoutputoven]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpoutputoven " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpbreakovenforlap]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpbreakovenforlap " +
                            "select *  into tmpoutputoven from ( " +
                            "SELECT A.Tanggal, A.Partno, A.Qty, B.Volume FROM (SELECT A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo, sum(A.QtyIn) Qty " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and  A.status>-1  and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven in(" + oven + ") " +
                            "GROUP BY A.TglSerah, partno) a, FC_Items b WHERE a.partno = b.partno " +
                            "union all " +
                            "SELECT  A.Tanggal, A.Partno, A.Qty, B.Volume FROM (select tanggal,partno, sum(qty) qty from (select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven from " +
                            "(select lari.DestID, TglJemur, PartNo2, Lokasi2, case when(select COUNT(destid) from T1_Serah where JemurID = s.JemurID and  ID < s.serahID and[status] > -1 and itemid0 = s.itemID0 and DestID = s.DestID) > 0  then 0 else Qty1 end Qty1 from" +
                            "(SELECT B.ID, B.DestID, B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin AS Qty1 FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID= D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > -1)) as lari left join(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah, C.CreatedTime as tglinputserah,C.ID as serahID, C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > -1) as s on lari.DestID = s.DestID and lari.ID = s.JemurID) as lagi " +
                            "where Qty1> 0)lagi2 where left(CONVERT(char, tanggal, 112), 6) = '" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and Oven in(" + oven + ")  GROUP BY tanggal, partno) a, FC_Items b WHERE a.partno = b.partno" +
                            ")tmp " +

                            "select * into tmpbreakovenforlap from ( " +
                            "select ((select   " +
                            "(CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)   " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM ( " +
                            "    select  " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)  " +
                            "    and A.OvenID=2 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select " +
                            "(CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END ) Waktu FROM ( " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)" +
                            "    and A.OvenID=3 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select    " +
                            "(CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM (  " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)   " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A   " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)   " +
                            "    and A.OvenID=4 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select    " +
                            "(CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM (  " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)   " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A   " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)   " +
                            "    and A.OvenID=1 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            "))waktu)cc " +

                            "DECLARE @cols AS NVARCHAR(MAX),@colsp AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX),@nullcols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX);  " +
                            "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(rtrim(c.Partno))   " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @colsp = STUFF((SELECT distinct '+ isnull(' + QUOTENAME(rtrim(c.Partno))  +',0)' " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @sumcols = STUFF((SELECT distinct ',sum(' + QUOTENAME(rtrim(c.Partno)) + ') as '+  " +
                            "            QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "select @nullcols =STUFF((SELECT distinct ',null ' + QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "set @query = ' " +
                            "select * from(" +
                            "select left(convert(char,tanggal,13),11) Tanggal,'+ @sumcols +',sum(lembar) Lembar, sum(lembar * volume)M3,(sum(lembar * volume)/195)*100 Persen from( " +
                            "SELECT Tanggal, volume, ' + @cols + ',' + @colsp +' Lembar from  (select Tanggal, partno,volume,qty from tmpoutputoven) x  " +
                            "pivot(sum(qty) for partno in (' + @cols + ')) p ) as q  group by tanggal) as t  " +
                            "union all select ''Total'' Total,'+@nullcols + ',sum(qty)lembar,sum(qty*volume)m3,null persen from  tmpoutputoven " +
                            "union all select null Tanggal,'+@nullcols + ', sum(qty/(DAY(EOMONTH(''" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01''))*3))lembar, " +
                            "((sum(qty*volume))/((select waktu from tmpbreakovenforlap))) m3,(((sum(qty*volume))/((select waktu from tmpbreakovenforlap)))/65*100) persen from  tmpoutputoven' " +
                            "execute(@query)  ";
                }
                else
                {
                    oven = ddlOven.SelectedIndex.ToString();

                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpoutputoven]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpoutputoven " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpbreakovenforlap]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpbreakovenforlap " +
                            "select *  into tmpoutputoven from ( " +
                            "SELECT A.Tanggal, A.Partno, A.Qty, B.Volume FROM (SELECT A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo, sum(A.QtyIn) Qty " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and  A.status>-1  and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven in(" + oven + ") " +
                            "GROUP BY A.TglSerah, partno) a, FC_Items b WHERE a.partno = b.partno " +
                            "union all " +
                            "SELECT  A.Tanggal, A.Partno, A.Qty, B.Volume FROM (select tanggal,partno, sum(qty) qty from (select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven from " +
                            "(select lari.DestID, TglJemur, PartNo2, Lokasi2, case when(select COUNT(destid) from T1_Serah where JemurID = s.JemurID and  ID < s.serahID and[status] > -1 and itemid0 = s.itemID0 and DestID = s.DestID) > 0  then 0 else Qty1 end Qty1 from" +
                            "(SELECT B.ID, B.DestID, B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin AS Qty1 FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID= D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > -1)) as lari left join(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah, C.CreatedTime as tglinputserah,C.ID as serahID, C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > -1) as s on lari.DestID = s.DestID and lari.ID = s.JemurID) as lagi " +
                            "where Qty1> 0)lagi2 where left(CONVERT(char, tanggal, 112), 6) = '" +ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and Oven in(" + oven + ")  GROUP BY tanggal, partno) a, FC_Items b WHERE a.partno = b.partno" +
                            ")tmp " +

                            "select * into tmpbreakovenforlap from ( " +
                            "select " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "select  " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "and A.OvenID= " + oven + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            ")r)cc " +

                            "DECLARE @cols AS NVARCHAR(MAX),@colsp AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX),@nullcols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX);  " +
                            "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(rtrim(c.Partno))   " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @colsp = STUFF((SELECT distinct '+ isnull(' + QUOTENAME(rtrim(c.Partno))  +',0)' " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @sumcols = STUFF((SELECT distinct ',sum(' + QUOTENAME(rtrim(c.Partno)) + ') as '+  " +
                            "            QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "select @nullcols =STUFF((SELECT distinct ',null ' + QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "set @query = ' " +
                            "select * from(" +
                            "select left(convert(char,tanggal,13),11) Tanggal,'+ @sumcols +',sum(lembar) Lembar, sum(lembar * volume)M3,(sum(lembar * volume)/195)*100 Persen from( " +
                            "SELECT Tanggal, volume, ' + @cols + ',' + @colsp +' Lembar from  (select Tanggal, partno,volume,qty from tmpoutputoven) x  " +
                            "pivot(sum(qty) for partno in (' + @cols + ')) p ) as q  group by tanggal) as t  " +
                            "union all select ''Total'' Total,'+@nullcols + ',sum(qty)lembar,sum(qty*volume)m3,null persen from  tmpoutputoven " +
                            "union all select null Tanggal,'+@nullcols + ', sum(qty/(DAY(EOMONTH(''" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01''))*3))lembar, " +
                            "((sum(qty*volume))/((select waktu from tmpbreakovenforlap))) m3,(((sum(qty*volume))/((select waktu from tmpbreakovenforlap)))/65*100) persen from  tmpoutputoven' " +
                            "execute(@query)  ";
                }
            }
            else
            {
                if (ddlOven.SelectedIndex == 5)
                {
                    oven = "1,2,3,4";
                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpoutputoven]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpoutputoven " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpbreakovenforlap]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpbreakovenforlap " +
                            "select *  into tmpoutputoven from ( " +
                            "SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty,case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  " +
                            "left join T1_Jemur J on J.DestID=A.DestID where   A.sfrom='jemur' and    A.status>-1  and left(convert(char,A.tglserah,112),6)='" +
                            ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven in(" + oven + ") " +
                            "union all " +
                            "select tanggal,partno,qty,volume from ( " +
                            "select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven , volume  " +
                            "from (  " +
                            "select lari.DestID,TglJemur,PartNo2,Lokasi2, " +
                            "case when (select COUNT(destid) from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID)>0  " +
                            "then 0 else Qty1 end Qty1,Volume  " +
                            "    from (  " +
                            "    SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume   " +
                            "    FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID=D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)  " +
                            "    ) as lari left join   " +
                            "    (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID,  " +
                            "    C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1 " +
                            "    ) as s on lari.DestID=s.DestID and lari.ID=s.JemurID   " +
                            ") as lagi where  Qty1>0)lagi2  where left(CONVERT(char,tanggal, 112),6)='" +
                            ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and Oven in(" + oven + ") " +
                            ")tmp " +

                            "select *  into tmpbreakovenforlap from ( " +
                            "select ((select   " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "    select  " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)  " +
                            "    and A.OvenID=2 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select    " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)" +
                            "    and A.OvenID=3 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select    " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM (  " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)   " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A   " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)   " +
                            "    and A.OvenID=4 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            ")+ " +
                            "(select    " +
                            "CASE DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM (  " +
                            "    select   " +
                            "    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)   " +
                            "    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A   " +
                            "    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID   " +
                            "    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)   " +
                            "    and A.OvenID=1 group by A.NmMasterCatID,A.OvenID,D.UraianCat  " +
                            ")r " +
                            "))waktu)cc " +

                            "DECLARE @cols AS NVARCHAR(MAX),@colsp AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX),@nullcols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX);  " +
                            "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(rtrim(c.Partno))   " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @colsp = STUFF((SELECT distinct '+ isnull(' + QUOTENAME(rtrim(c.Partno))  +',0)' " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @sumcols = STUFF((SELECT distinct ',sum(' + QUOTENAME(rtrim(c.Partno)) + ') as '+  " +
                            "            QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "select @nullcols =STUFF((SELECT distinct ',null ' + QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "set @query = ' " +
                            "select * from(" +
                            "select left(convert(char,tanggal,13),11) Tanggal,'+ @sumcols +',sum(lembar) Lembar, sum(lembar * volume)M3,(sum(lembar * volume)/195)*100 Persen from( " +
                            "SELECT Tanggal, volume, ' + @cols + ',' + @colsp +' Lembar from  (select Tanggal, partno,volume,qty from tmpoutputoven) x  " +
                            "pivot(sum(qty) for partno in (' + @cols + ')) p ) as q  group by tanggal) as t  " +
                            "union all select ''Total'' Total,'+@nullcols + ',sum(qty)lembar,sum(qty*volume)m3,null persen from  tmpoutputoven " +
                            "union all select null Tanggal,'+@nullcols + ', sum(qty/(DAY(EOMONTH(''" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01''))*3))lembar, " +
                            "((sum(qty*volume))/((select waktu from tmpbreakovenforlap))) m3,(((sum(qty*volume))/((select waktu from tmpbreakovenforlap)))/65*100) persen from  tmpoutputoven' " +
                            "execute(@query)  ";
                }
                else
                {
                    oven = ddlOven.SelectedIndex.ToString();

                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpoutputoven]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpoutputoven " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpbreakovenforlap]') AND type in (N'U')) " +
                            "DROP TABLE [dbo].tmpbreakovenforlap " +
                            "select *  into tmpoutputoven from ( " +
                            "SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty,case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  " +
                            "left join T1_Jemur J on J.DestID=A.DestID where   A.sfrom='jemur' and    A.status>-1  and left(convert(char,A.tglserah,112),6)='" +
                            ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven in(" + oven + ") " +
                            "union all " +
                            "select tanggal,partno,qty,volume from ( " +
                            "select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where status>-1 and destid=lagi.DestID and oven>0 order by id  )Oven , volume  " +
                            "from (  " +
                            "select lari.DestID,TglJemur,PartNo2,Lokasi2, " +
                            "case when (select COUNT(destid) from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID)>0  " +
                            "then 0 else Qty1 end Qty1,Volume  " +
                            "    from (  " +
                            "    SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume   " +
                            "    FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID=D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)  " +
                            "    ) as lari left join   " +
                            "    (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID,  " +
                            "    C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1 " +
                            "    ) as s on lari.DestID=s.DestID and lari.ID=s.JemurID   " +
                            ") as lagi where  Qty1>0)lagi2  where left(CONVERT(char,tanggal, 112),6)='" +
                            ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and Oven in(" + oven + ") " +
                            ")tmp " +

                            "select *  into tmpbreakovenforlap from ( " +
                            "select " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                            "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "select  " +
                            "CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "and A.OvenID= " + oven + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            ")r)cc " +

                            "DECLARE @cols AS NVARCHAR(MAX),@colsp AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX),@nullcols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX);  " +
                            "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(rtrim(c.Partno))   " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @colsp = STUFF((SELECT distinct '+ isnull(' + QUOTENAME(rtrim(c.Partno))  +',0)' " +
                            "            FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')   " +
                            "select @sumcols = STUFF((SELECT distinct ',sum(' + QUOTENAME(rtrim(c.Partno)) + ') as '+  " +
                            "            QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "select @nullcols =STUFF((SELECT distinct ',null ' + QUOTENAME(rtrim(c.Partno)) FROM tmpoutputoven c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                            "set @query = ' " +
                            "select * from(" +
                            "select left(convert(char,tanggal,13),11) Tanggal,'+ @sumcols +',sum(lembar) Lembar, sum(lembar * volume)M3,(sum(lembar * volume)/195)*100 Persen from( " +
                            "SELECT Tanggal, volume, ' + @cols + ',' + @colsp +' Lembar from  (select Tanggal, partno,volume,qty from tmpoutputoven) x  " +
                            "pivot(sum(qty) for partno in (' + @cols + ')) p ) as q  group by tanggal) as t  " +
                            "union all select ''Total'' Total,'+@nullcols + ',sum(qty)lembar,sum(qty*volume)m3,null persen from  tmpoutputoven " +
                            "union all select null Tanggal,'+@nullcols + ', sum(qty/(DAY(EOMONTH(''" + ddTahun.Text.Trim() + " - " + ddlBulan.SelectedValue.ToString() + "-01''))*3))lembar, " +
                            "((sum(qty*volume))/((select waktu from tmpbreakovenforlap))) m3,(((sum(qty*volume))/((select waktu from tmpbreakovenforlap)))/65*100) persen from  tmpoutputoven' " +
                            "execute(@query)  ";
                }
            }

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
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select count(partno) colcount from (select distinct partno from tmpoutputoven )a " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpoutputoven]') AND type in (N'U')) DROP TABLE [dbo].tmpoutputoven";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    int colcount = Convert.ToInt32(sdr["colcount"].ToString());
                    Session["colcount"] = colcount;
                }
            }
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                if (bfield.HeaderText.Trim() == "M3" || bfield.HeaderText.Trim() == "Persen")
                { bfield.DataFormatString = "{0:N2}"; }
                //else
                //    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                if (bfield.HeaderText.Trim() == "Tanggal")
                {
                    bfield.DataFormatString = "{0:d}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }

        public void updatesarmut()
        {
            string query = string.Empty;
            string sarmutPrs = string.Empty;
            decimal aktual = 0;
            string thn = ddTahun.Text.Trim();
            string bln = (ddlBulan.SelectedValue.Length == 1) ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
            string periode = thn + bln;
            Users users = (Users)HttpContext.Current.Session["Users"];

            if (users.UnitKerjaID == 13)
            {
                if (ddlOven.SelectedIndex == 5)
                {
                    sarmutPrs = "Akumulasi Oven Drying";
                    query = "select isnull(hasil,0) hasil from( " +
                            "select round( " +
                            "    ( " +
                            "        ( " +
                            "            ( " +
                            "                select  sum(qty*volume) from( " +
                            "SELECT A.Tanggal, A.Partno, A.Qty, B.Volume FROM (SELECT A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo, sum(A.QtyIn) Qty " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and  A.status>-1  and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' " +
                            "GROUP BY A.TglSerah, partno) a, FC_Items b WHERE a.partno = b.partno " +
                            "                    union all  " +
                                "SELECT  A.Tanggal, A.Partno, A.Qty, B.Volume FROM (select tanggal,partno, sum(qty) qty from (select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven from " +
                                "(select lari.DestID, TglJemur, PartNo2, Lokasi2, case when(select COUNT(destid) from T1_Serah where JemurID = s.JemurID and  ID < s.serahID and[status] > -1 and itemid0 = s.itemID0 and DestID = s.DestID) > 0  then 0 else Qty1 end Qty1 from" +
                                "(SELECT B.ID, B.DestID, B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin AS Qty1 FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID= D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > -1)) as lari left join(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah, C.CreatedTime as tglinputserah,C.ID as serahID, C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > -1) as s on lari.DestID = s.DestID and lari.ID = s.JemurID) as lagi " +
                                "where Qty1> 0)lagi2 where left(CONVERT(char, tanggal, 112), 6) = '" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "'  GROUP BY tanggal, partno) a, FC_Items b WHERE a.partno = b.partno" +
                            "                )xx " +
                            "            )/ " +
                            "            ( " +
                            "                ( " +
                            "                    select   " +
                            "                    (CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=1 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    (CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=2 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    (CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=3 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    (CASE WHEN COUNT(1) = 0 THEN 0 ELSE CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END END) Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)  " +
                            "	                    and A.OvenID=4 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                )  " +
                            "            ) " +
                            "        ) " +
                            "    ),2 " +
                            ") hasil)a";
                }
                else
                {
                    sarmutPrs = "Output Oven Drying " + ddlOven.SelectedIndex.ToString();
                    query = "select isnull(hasil,0) hasil from( " +
                            "select round( " +
                            "( " +
                            "   ( " +
                            "        ( " +
                            "            select  sum(qty*volume) from( " +
                            "SELECT A.Tanggal, A.Partno, A.Qty, B.Volume FROM (SELECT A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo, sum(A.QtyIn) Qty " +
                            "FROM T1_Serah A INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and  A.status>-1  and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven in(" + ddlOven.SelectedIndex.ToString() + ") " +
                            "GROUP BY A.TglSerah, partno) a, FC_Items b WHERE a.partno = b.partno " +
                            "                union all  " +
                                "SELECT  A.Tanggal, A.Partno, A.Qty, B.Volume FROM (select tanggal,partno, sum(qty) qty from (select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven from " +
                                "(select lari.DestID, TglJemur, PartNo2, Lokasi2, case when(select COUNT(destid) from T1_Serah where JemurID = s.JemurID and  ID < s.serahID and[status] > -1 and itemid0 = s.itemID0 and DestID = s.DestID) > 0  then 0 else Qty1 end Qty1 from" +
                                "(SELECT B.ID, B.DestID, B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin AS Qty1 FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID= D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > -1)) as lari left join(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah, C.CreatedTime as tglinputserah,C.ID as serahID, C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > -1) as s on lari.DestID = s.DestID and lari.ID = s.JemurID) as lagi " +
                                "where Qty1> 0)lagi2 where left(CONVERT(char, tanggal, 112), 6) = '" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and Oven in(" + ddlOven.SelectedIndex.ToString() + ")  GROUP BY tanggal, partno) a, FC_Items b WHERE a.partno = b.partno" +
                            "            )xx " +
                            "        )/ " +
                            "        ( " +
                            "            select   " +
                            "            CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "            WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "                select  " +
                            "                CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + "- 01')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "                WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "                inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "                inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "                and A.OvenID=" + ddlOven.SelectedIndex + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "            )r " +
                            "        ) " +
                            "    ) " +
                            "),2 " +
                            ") hasil)a";
                }
            }
            else
            {
                if (ddlOven.SelectedIndex == 5)
                {
                    sarmutPrs = "Akumulasi Oven Drying";
                    query = "select isnull(hasil,0) hasil from( " +
                            "select round( " +
                            "    ( " +
                            "        ( " +
                            "            ( " +
                            "                select  sum(qty*volume) from( " +
                            "                SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty,case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  FROM T1_Serah A  " +
                            "                    INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where   A.sfrom='jemur' and    A.status>-1   " +
                            "                    and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "'  " +
                            "                    union all  " +
                            "                    select tanggal,partno,qty,volume from (  " +
                            "		                    select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where status>-1 and destid=lagi.DestID and oven>0 order by id )Oven , volume  from ( " +
                            "		                    select lari.DestID,TglJemur,PartNo2,Lokasi2,  " +
                            "		                    case when (select COUNT(destid) from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID)>0   " +
                            "		                    then 0 else Qty1 end Qty1,Volume      from (       " +
                            "			                    SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume  " +
                            "			                    FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID=D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)  " +
                            "		                    ) as lari left join       (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID,      C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1 ) as s  " +
                            "		                    on lari.DestID=s.DestID and lari.ID=s.JemurID    " +
                            "	                    ) as lagi where  Qty1>0 " +
                            "                   )lagi2  where left(CONVERT(char,tanggal, 112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' " +
                            "                )xx " +
                            "            )/ " +
                            "            ( " +
                            "                ( " +
                            "                    select   " +
                            "                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=1 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=2 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "	                    and A.OvenID=3 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                ) + " +
                            "                ( " +
                            "                    select   " +
                            "                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "                    WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "		                    select  " +
                            "	                    CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "	                    WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "	                    inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "	                    inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22)  " +
                            "	                    and A.OvenID=4 group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "                   )r " +
                            "                )  " +
                            "            ) " +
                            "        ) " +
                            "    ),2 " +
                            ") hasil)a";
                }
                else
                {
                    sarmutPrs = "Output Oven Drying " + ddlOven.SelectedIndex.ToString();
                    query = "select isnull(hasil,0) hasil from( " +
                            "select round( " +
                            "( " +
                            "   ( " +
                            "        ( " +
                            "            select  sum(qty*volume) from( " +
                            "            SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty,case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  FROM T1_Serah A  " +
                            "                INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID  left join T1_Jemur J on J.DestID=A.DestID where   A.sfrom='jemur' and    A.status>-1   " +
                            "                and left(convert(char,A.tglserah,112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' and A.Oven=" + ddlOven.SelectedIndex + "  " +
                            "                union all  " +
                            "                select tanggal,partno,qty,volume from (  " +
                            "	                select TglJemur tanggal,PartNo2 partno,Qty1 qty,(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id )Oven , volume  from ( " +
                            "		                select lari.DestID,TglJemur,PartNo2,Lokasi2,  " +
                            "		                case when (select COUNT(destid) from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID)>0  " +
                            "		                then 0 else Qty1 end Qty1,Volume      from (       " +
                            "			                SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume     " +
                            "			                FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 inner join BM_Destacking D on B.DestID=D.ID inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)  " +
                            "		                ) as lari left join       (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID,      C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1 ) as s  " +
                            "		                on lari.DestID=s.DestID and lari.ID=s.JemurID    " +
                            "	                ) as lagi where  Qty1>0 " +
                            "               )lagi2  where left(CONVERT(char,tanggal, 112),6)='" + ddTahun.Text.Trim() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "'and Oven=" + ddlOven.SelectedIndex + "  " +
                            "            )xx " +
                            "        )/ " +
                            "        ( " +
                            "            select   " +
                            "            CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + " - 01 ')) WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0)  " +
                            "            WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu FROM ( " +
                            "                select  " +
                            "                CASE  DAY(EOMONTH('" + ddTahun.Text.Trim() + " -  " + ddlBulan.SelectedValue.ToString() + "- 01')) WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00)  " +
                            "                WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu from  FnBreakDown A  " +
                            "                inner join FnBreakDown_MasterOven B on A.OvenID=B.ID inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID  " +
                            "                inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + ddlBulan.SelectedValue.ToString() + "' and YEAR(TanggalBreak)='" + ddTahun.Text.Trim() + "' and A.NmMasterCatID in(18,20,21,22) " +
                            "                and A.OvenID=" + ddlOven.SelectedIndex + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +
                            "            )r " +
                            "        ) " +
                            "    ) " +
                            "),2 " +
                            ") hasil)a";
                }
            }

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = query;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["hasil"].ToString());
                }
            }

            if (aktual <= 0)
                aktual = 0;
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + thn +
                " and Bulan=" + ddlBulan.SelectedValue +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            SqlDataReader sdr1 = zl1.Retrieve();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Pemantauan oven " + DateTime.Now.ToString("ddMMyyyy") + ".xls"));
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
        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) || 
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy")!=DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtdrtanggal.Text = "01-" + DateTime.Parse(txtsdtanggal.Text).ToString("MMM-yyyy");
        }
        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) ||
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy") != DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Parse(txtdrtanggal.Text).AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd")
            //        + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Partno";
                HeaderCell.ColumnSpan = Convert.ToInt32(Session["colcount"].ToString());
                HeaderCell.HorizontalAlign = HorizontalAlign.Center; ;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Output Total";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center; ;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center; ;
                HeaderGridRow.Cells.Add(HeaderCell);

                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        //protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        GridView HeaderGrid = (GridView)sender;
        //        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //        TableCell HeaderCell = new TableCell();
        //        HeaderCell.Text = " ";
        //        HeaderCell.ColumnSpan = 1;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Total All Line";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 1";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 2";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 3";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 4";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 5";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = "Line 6";
        //        HeaderCell.ColumnSpan = 9;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderGridRow.Cells.Add(HeaderCell);
        //        GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);

        //        //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //        //TableCell HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Tanggal Produksi";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);

        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);

        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);

        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);

        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);

        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total Potong";
        //        //HeaderCell1.ColumnSpan = 1;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Total BP";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP Retak";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "BP DL";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //HeaderCell1 = new TableCell();
        //        //HeaderCell1.Text = "Gompal Finishing";
        //        //HeaderCell1.ColumnSpan = 2;
        //        //HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
        //        //HeaderGridRow1.Cells.Add(HeaderCell1);
        //        //GrdDynamic.Controls[0].Controls.AddAt(1, HeaderGridRow1);


        //    }
        //}
        public enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
        {
            return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }
        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }
        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
        {
            if (interval == DateInterval.Year)
                return dt2.Year - dt1.Year;

            if (interval == DateInterval.Month)
                return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

            TimeSpan ts = dt2 - dt1;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
                return Round(ts.TotalDays);

            if (interval == DateInterval.Hour)
                return Round(ts.TotalHours);

            if (interval == DateInterval.Minute)
                return Round(ts.TotalMinutes);

            if (interval == DateInterval.Second)
                return Round(ts.TotalSeconds);

            if (interval == DateInterval.Weekday)
            {
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.WeekOfYear)
            {
                while (dt2.DayOfWeek != eFirstDayOfWeek)
                    dt2 = dt2.AddDays(-1);
                while (dt1.DayOfWeek != eFirstDayOfWeek)
                    dt1 = dt1.AddDays(-1);
                ts = dt2 - dt1;
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.Quarter)
            {
                double d1Quarter = GetQuarter(dt1.Month);
                double d2Quarter = GetQuarter(dt2.Month);
                double d1 = d2Quarter - d1Quarter;
                double d2 = (4 * (dt2.Year - dt1.Year));
                return Round(d1 + d2);
            }

            return 0;
        }
        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
    }
}