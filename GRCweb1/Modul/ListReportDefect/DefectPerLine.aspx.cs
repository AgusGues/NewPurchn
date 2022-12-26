using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportDefect
{
    public partial class DefectPerLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 400, 100 , 60 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RBTglInput.Checked == true)
                LblPeriode.Text = "Tanggal Input";
            if (RBTglProduksi.Checked == true)
                LblPeriode.Text = "Tanggal Produksi";
            if (RBTglPotong.Checked == true)
                LblPeriode.Text = "Tanggal Potong";
            LblTgl1.Text = txtdrtanggal.Text;
            LblTgl2.Text = txtsdtanggal.Text;
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid(string tgl1, string tgl2)
        {
            string strtanggal = string.Empty;
            if (RBTglInput.Checked == true)
                strtanggal = "tglInput";
            if (RBTglProduksi.Checked == true)
                strtanggal = "TglProd";
            if (RBTglPotong.Checked == true)
                strtanggal = "TglPeriksa";
            string strSQL = "declare @tgl1 varchar(max)     " +
                "   declare @tgl2 varchar(max)     " +
                "   set @tgl1 ='" + tgl1 + "'     " +
                "   set @tgl2 ='" + tgl2 + "'     " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPL]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGPL]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTotL]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGPTotL]  " +
                "select * into tempdefectGPL from (   " +
                "select A.tgl TglPeriksa,cast( convert(char,E.tglproduksi,112) as date) TglProd,I.partno,TJ.TglJemur,rtrim(cast(CAST(I.Tebal as decimal(18,1)) as CHAR)) + ' X ' +       " +
                "   rtrim(cast(CAST(I.Lebar as decimal(18)) as CHAR)) + ' X ' + rtrim(cast(CAST(I.Panjang as decimal(18)) as CHAR)) Ukuran ,P.NoPAlet,       " +
                "   case when isnull(D.DefCode,'')='' then (select top 1 defcode from Def_MasterDefect where rowstatus>-1 order by id desc) else D.DefCode end DefCode,     " +
                "   BP.PlantName as Line, PG.[Group] as GP,J.GroupJemurCode as GJ,BF.FormulaCode as Jenis,      " +
                "   case when isnull(D.DefCode,'')='' then (select top 1 DefName from Def_MasterDefect where rowstatus>-1 order by id desc) else D.DefName end DefName,     " +
                "   case when isnull(D.DefCode,'')='' then (select top 1 Urutan from Def_MasterDefect where rowstatus>-1 order by id desc) else D.Urutan end Urutan,     " +
                "   isnull(D.DeptID,0) DeptID,ISNULL(B.ID,0) ID,A.ID DefectID,      " +
                "   case when isnull(D.DefCode,'')='' then (select top 1 ID from Def_MasterDefect where rowstatus>-1 order by id desc) else D.ID end MasterID,     " +
                "   isnull(B.qty,0) Qty,A.DestID,isnull((select sum(qty) from def_defectdetail where defectID=A.ID),0) QtyIn,A.TPotong as TotPotong     " +
                "   from def_defect A left join def_defectdetail B on A.ID=B.defectID left join bm_destacking E on A.destid=E.id      " +
                "   left join BM_PlantGroup PG on A.GroupProdID=PG.ID  left join fc_items I on E.itemid=I.ID left join BM_Palet P on E.PaletID=P.ID left join BM_Plant BP on BP.ID=PG.PlantID      " +
                "   left join Def_GroupJemur J on A.GroupJemurID = J.ID left join BM_Formula BF on BF.ID=E.FormulaID  Left join t1_jemur TJ on E.ID=TJ.destid      " +
                "   left join Def_MasterDefect D on B.MasterID=D.ID and D.rowstatus>-1 where I.tebal>=3 and I.tebal<=4  ) as Def where CONVERT(varchar, TglProd,112)>=@tgl1 and CONVERT(varchar,TglProd,112)<=@tgl2  " +
                " " +
                "select PlantName,tgl,sum(totpotong)totpotong,sum(totkubik)totkubik into tempdefectGPTotL from (select  B.PlantName ,cast( convert(char,A.tglproduksi,112) as date)  tgl,SUM(C.TPotong) as totpotong,SUM(C.TPotong *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) as totkubik   " +
                "from BM_Destacking A left join BM_Plant B on A.PlantID =B.ID inner join  fc_items I on I.ID=A.ItemID inner join Def_Defect C on A.ID=C.Destid " +
                "where I.tebal>=3 and I.tebal<=4  and CONVERT(char,A.tglproduksi,112)>=@tgl1 and  CONVERT(char,A.tglproduksi,112)<=@tgl2 and C.Status>-1 group by B.PlantName,A.tglproduksi) A  group by plantname,tgl" +
                "  " +
                "select tgl0,(isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0))TotPotong7,   " +
                "(isnull(TotBP1,0)+isnull(TotBP2,0)+isnull(TotBP3,0)+isnull(TotBP4,0)+isnull(TotBP5,0)+isnull(TotBP6,0)) TotBP7,   " +
                "case when (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0))>0 then   " +
                "((isnull(TotBP1,0)+isnull(TotBP2,0)+isnull(TotBP3,0)+isnull(TotBP4,0)+isnull(TotBP5,0)+isnull(TotBP6,0))/   " +
                "(isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0)))*100 end [%BP7],   " +
                "(isnull(TotBPR1,0)+isnull(TotBPR2,0)+isnull(TotBPR3,0)+isnull(TotBPR4,0)+isnull(TotBPR5,0)+isnull(TotBPR6,0))TotBPR7,   " +
                "case when (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0))>0 then   " +
                "((isnull(TotBPR1,0)+isnull(TotBPR2,0)+isnull(TotBPR3,0)+isnull(TotBPR4,0)+isnull(TotBPR5,0)+isnull(TotBPR6,0))/   " +
                " (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0)))*100 end [%Rtk7],  " +
                "(isnull(TotBPDL1,0)+isnull(TotBPDL2,0)+isnull(TotBPDL3,0)+isnull(TotBPDL4,0)+isnull(TotBPDL5,0)+isnull(TotBPDL6,0))TotBPDL7,   " +
                "case when (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0))>0 then   " +
                "((isnull(TotBPDL1,0)+isnull(TotBPDL2,0)+isnull(TotBPDL3,0)+isnull(TotBPDL4,0)+isnull(TotBPDL5,0)+isnull(TotBPDL6,0))/   " +
                " (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0)))*100 end [%DL7],  " +
                "(isnull(TotBPGF1,0)+isnull(TotBPGF2,0)+isnull(TotBPGF3,0)+isnull(TotBPGF4,0)+isnull(TotBPGF5,0)+isnull(TotBPGF6,0))TotBPGF7,   " +
                "case when (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0))>0 then   " +
                "((isnull(TotBPGF1,0)+isnull(TotBPGF2,0)+isnull(TotBPGF3,0)+isnull(TotBPGF4,0)+isnull(TotBPGF5,0)+isnull(TotBPGF6,0))/   " +
                " (isnull(TotPotong1,0)+isnull(TotPotong2,0)+isnull(TotPotong3,0)+isnull(TotPotong4,0)+isnull(TotPotong5,0)+isnull(TotPotong6,0)))*100 end [%GF7],  " +
                "TotPotong1,TotBP1,[%BP1],TotBPR1,[%Rtk1],TotBPDL1,[%DL1],TotBPGF1,[%GF1], " +
                "TotPotong2,TotBP2,[%BP2],TotBPR2,[%Rtk2],TotBPDL2,[%DL2],TotBPGF2,[%GF2], " +
                "TotPotong3,TotBP3,[%BP3],TotBPR3,[%Rtk3],TotBPDL3,[%DL3],TotBPGF3,[%GF3], " +
                "TotPotong4,TotBP4,[%BP4],TotBPR4,[%Rtk4],TotBPDL4,[%DL4],TotBPGF4,[%GF4], " +
                "TotPotong5,TotBP5,[%BP5],TotBPR5,[%Rtk5],TotBPDL5,[%DL5],TotBPGF5,[%GF5], " +
                "TotPotong6,TotBP6,[%BP6],TotBPR6,[%Rtk6],TotBPDL6,[%DL6],TotBPGF6,[%GF6]  " +
                " from (  " +
                "select distinct Tgl  tgl0 from Def_Defect where CONVERT(char,Tgl,112)>=@tgl1 and CONVERT(char,Tgl,112)<=@tgl2) D0 left join (  " +
                "select Tgl Tgl1,TotPotong TotPotong1,TotBP TotBP1,(TotBP/TotPotong)*100 [%BP1],TotBPR TotBPR1,(TotBPR/TotPotong)*100 [%Rtk1], " +
                "totBPDL totBPDL1,(TotBPDL/TotPotong)*100 [%DL1],totBPGF totBPGF1,(TotBPGF/TotPotong)*100 [%GF1] from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from   " +
                "tempdefectGPTotL A where plantname like '%1%' )D)D1 on D0.tgl0=D1.tgl1  " +
                "left join (  " +
                "select Tgl Tgl2,TotPotong TotPotong2,TotBP TotBP2,(TotBP/TotPotong)*100 [%BP2],TotBPR TotBPR2,(TotBPR/TotPotong)*100 [%Rtk2], " +
                "totBPDL totBPDL2,(TotBPDL/TotPotong)*100 [%DL2],totBPGF totBPGF2,(TotBPGF/TotPotong)*100 [%GF2] from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from   " +
                "tempdefectGPTotL A where plantname like '%2%' )D)D2 on D0.tgl0=D2.tgl2   " +
                "left join (  " +
                "select Tgl Tgl3,TotPotong TotPotong3,TotBP TotBP3,(TotBP/TotPotong)*100 [%BP3],TotBPR TotBPR3,(TotBPR/TotPotong)*100 [%Rtk3], " +
                "totBPDL totBPDL3,(TotBPDL/TotPotong)*100 [%DL3],totBPGF totBPGF3,(TotBPGF/TotPotong)*100 [%GF3] from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from   " +
                "tempdefectGPTotL A where plantname like '%3%' )D)D3 on D0.tgl0=D3.tgl3   " +
                "left join (  " +
                "select Tgl Tgl4,TotPotong TotPotong4,TotBP TotBP4,(TotBP/TotPotong)*100 [%BP4],TotBPR TotBPR4,(TotBPR/TotPotong)*100 [%Rtk4], " +
                "totBPDL totBPDL4,(TotBPDL/TotPotong)*100 [%DL4],totBPGF totBPGF4,(TotBPGF/TotPotong)*100 [%GF4] from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from   " +
                "tempdefectGPTotL A where plantname like '%4%' )D)D4 on D0.tgl0=D4.tgl4   " +
                "left join (  " +
                "select Tgl Tgl5,TotPotong TotPotong5,TotBP TotBP5,(TotBP/TotPotong)*100 [%BP5],TotBPR TotBPR5,(TotBPR/TotPotong)*100 [%Rtk5], " +
                "totBPDL totBPDL5,(TotBPDL/TotPotong)*100 [%DL5],totBPGF totBPGF5,(TotBPGF/TotPotong)*100 [%GF5] from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from   " +
                "tempdefectGPTotL A where plantname like '%5%')D)D5 on D0.tgl0=D5.tgl5   " +
                "left join (  " +
                "select Tgl Tgl6,TotPotong TotPotong6,TotBP TotBP6,(TotBP/TotPotong)*100 [%BP6],TotBPR TotBPR6,(TotBPR/TotPotong)*100 [%Rtk6], " +
                "totBPDL totBPDL6,(TotBPDL/TotPotong)*100 [%DL6],totBPGF totBPGF6,(TotBPGF/TotPotong)*100 [%GF6]  from (  " +
                "select plantname Line,tgl,totpotong,(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl)totBP,  " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='retak')totBPR, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='delaminasi')totBPDL, " +
                "(select SUM(qty) from tempdefectGPL where Line=A.plantname and TglProd=A.tgl and DefName='gompal finishing')totBPGF from  " +
                "tempdefectGPTotL A where plantname like '%6%')D)D6 on D0.tgl0=D6.tgl6 order by D0.tgl0 ";
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
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                if (col.ColumnName.Substring(0, 1) == "%")
                {
                    bfield.HeaderText = "%";
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = "Lbr";
                }
                if (col.ColumnName.Substring(0, 3) == "tgl")
                {
                    bfield.HeaderText = " ";
                    bfield.DataFormatString = "{0:d}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                if (col.ColumnName.Substring(0, 4) == "TotP")
                {
                    bfield.HeaderText = "Lbr";
                    bfield.DataFormatString = "{0:N0}";
                }
                if (col.ColumnName.Substring(0, 4) == "TotB")
                {
                    bfield.HeaderText = "Lbr";
                    bfield.DataFormatString = "{0:N0}";
                }
                if (col.ColumnName.Length > 5)
                {
                    string tx = col.ColumnName.Substring(3, 3);
                    if (col.ColumnName.Substring(3, 3) == "BPR")
                    {
                        // bfield.HeaderText = "Retak";
                        bfield.DataFormatString = "{0:N0}";
                    }
                    if (col.ColumnName.Substring(3, 3) == "BPG")
                    {
                        //bfield.HeaderText = "Gompal Finishing";
                        bfield.DataFormatString = "{0:N0}";
                    }
                    if (col.ColumnName.Substring(3, 3) == "BPD")
                    {
                        //bfield.HeaderText = "Delaminasi";
                        bfield.DataFormatString = "{0:N0}";
                    }
                }
                //if (col.ColumnName.Substring(0, 2) == "M3")
                //{
                //    bfield.HeaderText = "M3";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Defect Per Line.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
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
                HeaderCell.Text = "Total All Line";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 1";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 2";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 3";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 4";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 5";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Line 6";
                HeaderCell.ColumnSpan = 9;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);

                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Tanggal Produksi";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total Potong";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total BP";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP Retak";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "BP DL";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Gompal Finishing";
                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);
                GrdDynamic.Controls[0].Controls.AddAt(1, HeaderGridRow1);


            }
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
    }
}