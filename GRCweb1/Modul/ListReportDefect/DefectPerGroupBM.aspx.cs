//using DefectFacade;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Factory;

namespace GRCweb1.Modul.ListReportDefect
{
    public partial class DefectPerGroupBM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                Global.link = "~/Default.aspx";
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
            }
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
            LbPer.Text = "Per Group";
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
            Users users = (Users)Session["Users"];
            #region Untuk Citeureup
            if (users.UnitKerjaID == 100)
            {
                string strtanggal = string.Empty;
                if (RBTglInput.Checked == true)
                    strtanggal = "tglInput";
                if (RBTglProduksi.Checked == true)
                    strtanggal = "TglProd";
                if (RBTglPotong.Checked == true)
                    strtanggal = "TglPeriksa";
                string strOven = string.Empty;
                if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
                {
                    strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
                }
                else
                {
                    strOven = "case when D.DeptID=0 then " +
                                "case when GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') then  " +
                                "    case when D.DefName='retak' then 2  " +
                                "        when D.DefName='gompal' then 3 " +
                                "    else D.DeptID end  " +
                                "else   " +
                                "    case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end  " +
                                "end " +
                            "else D.DeptID end deptID1";
                }
                string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGP]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGP] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTot]') AND type in (N'U')) DROP TABLE [dbo].tempdefectGPTot " +
                    "declare @tgl1 as NVARCHAR(MAX),@tgl2 as NVARCHAR(MAX) " +
                    "set @tgl1='" + tgl1 + "' " +
                    "set @tgl2='" + tgl2 + "' " +
                    "select * into tempdefectGP from ( " +
                    "select " + strOven + ", " +
                    "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  P.NoPAlet, D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,  " +
                    "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID,C.QtyIn,B.Qty*I.Volume as Kubik,E.Qty as TotPotong,C.sfrom " +
                    "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID left join T1_Serah C on A.SerahID=C.ID  " +
                    "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=C.DestID left join BM_Palet P on E.PaletID=P.ID " +
                    "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID  left join BM_Plant BP on BP.ID=E.PlantID left join FC_Items I on I.ID=C.ItemID0 inner join Def_GroupJemur GJ on GJ.ID=A.GroupJemurID " +
                    "where B.RowStatus>-1 ) as Def   " +
                    "where CONVERT(varchar, " + strtanggal + ",112)>=@tgl1 and CONVERT(varchar,  " + strtanggal + " ,112)<=@tgl2 and DeptID1=2 " +
                    "select  B.[Group],SUM(C.qtyin) as totpotong,SUM(kubik) as totkubik into tempdefectGPTot   " +
                    "from BM_Destacking A left join BM_PlantGroup B on A.PlantGroupID=B.ID inner join (  " +
                    "select C.destid,C.qtyin,C.QtyIn*I1.Volume as kubik from T1_Serah C Inner join fc_items I  " +
                    "on C.itemid=I.ID Inner join fc_items I1 on C.itemid0=I1.ID where C.Status>-1 and CONVERT(char,C.tglserah,112)>=@tgl1 and   " +
                    "CONVERT(char,C.tglserah,112)<=@tgl2 and (I.PartNo like '%-w-%' or I.PartNo like '%-3-%' ) " +
                    "union all  " +
                    "select C.DestID , B.Qty qtyin,B.Qty*I.Volume as kubik from Def_Defect A inner join Def_DefectDetail B on A.ID=B.DefectID  " +
                    "inner join T1_Serah C on A.SerahID=C.ID left join FC_Items I on I.ID=C.ItemID0  " +
                    "where CONVERT(char,A.tgl,112)>=@tgl1 and  CONVERT(char,A.tgl,112)<=@tgl2 and B.RowStatus>-1 " +
                    ") as C  on A.ID=C.DestID group by B.[Group] " +
                    "DECLARE @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX),@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), @cols4 AS NVARCHAR(MAX), " +
                    "@query0  AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX), " +
                    "    @query1  AS NVARCHAR(MAX), @query2  AS NVARCHAR(MAX), @query3  AS NVARCHAR(MAX); " +
                    "set @cols1= STUFF((SELECT distinct ',' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) +','+ c.[Group] + " +
                    "            ' /(select sum(qty) from tempdefectGP where [group]=''' + c.[Group] + ''')*100 as ' +  " +
                    "QUOTENAME(+ '%' + c.[Group] ) " +
                    "            FROM BM_PlantGroup c " +
                    "            FOR XML PATH(''), TYPE " +
                    "            ).value('.', 'NVARCHAR(MAX)')  " +
                    "        ,1,1,'') " +
                    "set @cols= STUFF((SELECT distinct ',' +  QUOTENAME(c.[Group] )+',' + QUOTENAME('M3' + c.[Group] )  " +
                    "            FROM BM_PlantGroup c " +
                    "            FOR XML PATH(''), TYPE " +
                    "            ).value('.', 'NVARCHAR(MAX)')  " +
                    "        ,1,1,'') " +
                    "set @cols2= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                    "QUOTENAME(+ '%' + c.[Group] ) " +
                    "            FROM BM_PlantGroup c " +
                    "            FOR XML PATH(''), TYPE " +
                    "            ).value('.', 'NVARCHAR(MAX)')  " +
                    "        ,1,1,'') " +
                    "set @cols3= STUFF((SELECT distinct + ',null as' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) + ',' + QUOTENAME(c.[Group] ) +' as ' +  " +
                    "QUOTENAME(+ '%' + c.[Group] ) " +
                    "            FROM BM_PlantGroup c " +
                    "            FOR XML PATH(''), TYPE " +
                    "            ).value('.', 'NVARCHAR(MAX)')  " +
                    "        ,1,1,'') " +
                    "set @cols4= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                    "QUOTENAME(+ '%' + c.[Group] ) " +
                    "            FROM BM_PlantGroup c " +
                    "            FOR XML PATH(''), TYPE " +
                    "            ).value('.', 'NVARCHAR(MAX)')  " +
                    "        ,1,1,'') " +
                    "set @query0 = ' " +
                    "select Defect,' + @cols1 +' from ( " +
                    "select * " +
                    "from ( " +
                    "    select C.defname as Defect,[Group],sum(QTY) as Qty from  " +
                    "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                    "    union all " +
                    "    select C.defname as Defect,''M3''+rtrim([Group]),isnull(sum(B.Kubik ),0) as Qty from  Def_Defect A inner join  " +
                    "    tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                    ") as X1  " +
                    "pivot  " +
                    "( " +
                    "sum(QTY) for [Group] in (' + @cols +') " +
                    ") as X)Y ' " +
                    "declare @defname as NVARCHAR(MAX) " +
                    "set @defname ='Total BP' " +
                    "set @query = ' union all  " +
                    "select Defect,' + @cols2 +' from ( " +
                    "select * " +
                    "from ( " +
                    "    select ''' + @defname +''' as Defect,[Group],sum(Qty) as Qty from  " +
                    "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group] " +
                    "    union all " +
                    "    select ''' + @defname +''' as Defect,''M3'' +[Group],sum(kubik) as Qty from  " +
                    "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group] " +
                    ") as X1  " +
                    "pivot  " +
                    "( " +
                    "sum(QTY) for [Group] in (' + @cols +') " +
                    ") as X)Y ' " +
                    "declare @defname1 as NVARCHAR(MAX) " +
                    "set @defname1 ='total Potong' " +
                    "set @query1 =  ' union all  " +
                    "select Defect,' + @cols2 +' from ( " +
                    "select * " +
                    "from ( " +
                    "    select ''' + @defname1 +''' as Defect,[Group],cast(sum(totpotong) as decimal(18,2)) as Qty from tempdefectGPTot group by [Group] " +
                    "    union all  " +
                    "    select ''' + @defname1 +''' as Defect,''M3''+[Group],sum(totkubik) as Qty from tempdefectGPTot group by [Group] " +
                    ") as X1  " +
                    "pivot  " +
                    "( " +
                    "sum(QTY) for [Group] in (' + @cols +') " +
                    ") as X)Y ' " +
                    "declare @defname2 as NVARCHAR(MAX) " +
                    "set @defname2 ='total Prosentase BP' " +
                    "set @query2 =  ' union all  " +
                    "select Defect,' + @cols3 +' from ( " +
                    "select * " +
                    "from ( " +
                    "    select Defect,X0.[group],qty/totpotong*100 as qty from (select ''' + @defname2 +''' as Defect,[Group],sum(qty) as qty from tempdefectGP  group by [Group] ) as X0  " +
                    "    inner join tempdefectGPTot Z on Z.[group]=X0.[group] " +
                    ") as X1  " +
                    "pivot  " +
                    "( " +
                    "sum(qty) for [Group] in (' + @cols +') " +
                    ") as X)Y ' " +
                    "declare @defname3 as NVARCHAR(MAX) " +
                    "set @defname3 ='Totall BP Pelarian' " +
                    "select @query3 =  ' union all  " +
                    "select Defect,' + @cols4 +' from ( " +
                    "select * " +
                    "from ( " +
                    "    select ''' + @defname3 +''' as Defect,[Group] , sum(qty) qty from tempdefectGP where sfrom=''lari'' group by [Group] " +
                    ") as X1  " +
                    "pivot  " +
                    "( " +
                    "sum(qty) for [Group] in (' + @cols +') " +
                    ") as X)Y order by Defect' " +
                    "execute(@query0 + @query + @query1 + @query2 + @query3)";
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
                string formdeci = "{0:N1}";
                if (txtDecimal.Text.Trim() == "2") formdeci = "{0:N2}";
                if (txtDecimal.Text.Trim() == "3") formdeci = "{0:N3}";
                if (txtDecimal.Text.Trim() == "4") formdeci = "{0:N4}";
                if (txtDecimal.Text.Trim() == "5") formdeci = "{0:N5}";
                if (txtDecimal.Text.Trim() == "6") formdeci = "{0:N6}";
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
                        bfield.HeaderText = col.ColumnName;
                    }
                    if (col.ColumnName.Substring(0, 2) == "M3")
                    {
                        bfield.HeaderText = "M3";
                        bfield.DataFormatString = formdeci;
                    }
                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();
                DefectFacades df = new DefectFacades();
                int TBP = df.GetTotalBP();
                int TPot = df.GetTotalPotong();
                Decimal TBPK = df.GetTotalBPKubik();
                Decimal TPotK = df.GetTotalPotongKubik();
                LBP.Text = TBP.ToString("N0");
                LPotong.Text = TPot.ToString("N0");
                LBP0.Text = TBPK.ToString("N1");
                LPotong0.Text = TPotK.ToString("N1");
            }
            #endregion
            #region Untuk Karawang
            else if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                string strtanggal = string.Empty;
                if (RBTglInput.Checked == true)
                    strtanggal = "tglInput";
                if (RBTglProduksi.Checked == true)
                    strtanggal = "TglProd";
                if (RBTglPotong.Checked == true)
                    strtanggal = "TglPeriksa";
                string strOven = string.Empty;
                if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
                {
                    strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
                }
                else
                {
                    strOven = "case when D.DeptID=0 then " +
                                "case when GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') then  " +
                                "    case when D.DefName='retak' then 2  " +
                                "        when D.DefName='gompal' then 3 " +
                                "    else D.DeptID end  " +
                                "else   " +
                                "    case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end  " +
                                "end " +
                            "else D.DeptID end deptID1";
                }
                string strSQL = string.Empty;
                if (Convert.ToInt32(tgl1.Substring(0, 6)) >= 201805)
                {
                    #region query Baru
                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGP]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGP] " +
                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTot]') AND type in (N'U')) DROP TABLE [dbo].tempdefectGPTot " +
                       "declare @tgl1 as NVARCHAR(MAX),@tgl2 as NVARCHAR(MAX) " +
                       "set @tgl1='" + tgl1 + "' " +
                       "set @tgl2='" + tgl2 + "' " +
                       "select * into tempdefectGP from ( " +
                       "select " + strOven + ", " +
                       "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,  " +
                       "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.ID  DestID,isnull(A.TPotong,0)  QtyIn,B.Qty*I.Volume as Kubik,isnull(A.TPotong,0) as TotPotong,isnull(A.TPotong,0)*I.volume as TPotongM3 ,'' sfrom " +
                       "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID  " +
                       "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=A.DestID  " +
                       "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID   " +
                       "left join BM_Plant BP on BP.ID=E.PlantID left join FC_Items I on I.ID=E.ItemID inner join Def_GroupJemur GJ on GJ.ID=A.GroupJemurID " +
                       "where B.RowStatus>-1  and D.RowStatus >-1) as Def   " +
                       "where CONVERT(varchar, " + strtanggal + ",112)>=@tgl1 and CONVERT(varchar,  " + strtanggal + " ,112)<=@tgl2 and DeptID1=2 " +

                       "select  B.[Group],SUM(C.qtyin) as totpotong,SUM(kubik) as totkubik,sum(TPotongM3) TotPotongM3 into tempdefectGPTot   " +
                       "from BM_Destacking A left join BM_PlantGroup B on A.PlantGroupID=B.ID inner join (  " +
                       "select C.ID DestID , A.TPotong qtyin,A.TPotong*I.Volume as kubik,A.TPotong*I.Volume TPotongM3 " +
                       "from Def_Defect A  " +
                       "inner join BM_Destacking C on A.DestID=C.ID left join FC_Items I on I.ID=C.ItemID " +
                       "where A.Status>-1 and CONVERT(char,A.tgl,112)>=@tgl1 and  CONVERT(char,A.tgl,112)<=@tgl2 " +
                       ") as C  on A.ID=C.DestID group by B.[Group]  " +

                       "DECLARE @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX),@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), @cols4 AS NVARCHAR(MAX), " +
                       "@query0  AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX), " +
                       "    @query1  AS NVARCHAR(MAX), @query2  AS NVARCHAR(MAX), @query3  AS NVARCHAR(MAX); " +
                       "set @cols1= STUFF((SELECT distinct ',' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) +','+ c.[Group] + " +
                       "            ' /(select sum(qty) from tempdefectGP where [group]=''' + c.[Group] + ''')*100 as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols= STUFF((SELECT distinct ',' +  QUOTENAME(c.[Group] )+',' + QUOTENAME('M3' + c.[Group] )  " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols2= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols3= STUFF((SELECT distinct + ',null as' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) + ',' + QUOTENAME(c.[Group] ) +' as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols4= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @query0 = ' " +
                       "select Defect,' + @cols1 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select C.defname as Defect,[Group],sum(QTY) as Qty from  " +
                       "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                       "    union all " +
                       "    select C.defname as Defect,''M3''+rtrim([Group]),isnull(sum(B.Kubik ),0) as Qty from  Def_Defect A inner join  " +
                       "    tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname as NVARCHAR(MAX) " +
                       "set @defname ='Total BP' " +
                       "set @query = ' union all  " +
                       "select Defect,' + @cols2 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname +''' as Defect,[Group],sum(Qty) as Qty from  " +
                       "    tempdefectGP B  group by [Group] " +
                       "    union all " +
                       "    select ''' + @defname +''' as Defect,''M3'' +[Group],sum(kubik) as Qty from  " +
                       "    tempdefectGP B  group by [Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname1 as NVARCHAR(MAX) " +
                       "set @defname1 ='total Potong' " +
                       "set @query1 =  ' union all  " +
                       "select Defect,' + @cols2 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname1 +''' as Defect,[Group],cast(sum(totpotong) as decimal(18,2)) as Qty from tempdefectGPTot group by [Group] " +
                       "    union all  " +
                       "    select ''' + @defname1 +''' as Defect,''M3''+[Group],sum(totkubik) as Qty from tempdefectGPTot group by [Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +

                       //"declare @defname2 as NVARCHAR(MAX) " +
                       //"set @defname2 ='total Prosentase BP' " +
                       //"set @query2 =  ' union all  " +
                       //"select Defect,' + @cols3 +' from ( " +
                       //"select * " +
                       //"from ( " +
                       //"    select Defect,X0.[group],Kubik/tpotongm3*100 as qty from (select ''' + @defname2 +''' as Defect,[Group],sum(kubik) as kubik,sum(tpotongm3) tpotongm3 from tempdefectGP  group by [Group] ) as X0  " +
                       //"    inner join tempdefectGPTot Z on Z.[group]=X0.[group] " +
                       //") as X1  " +
                       //"pivot  " +
                       //"( " +
                       //"sum(qty) for [Group] in (' + @cols +') " +
                       //") as X)Y ' " +

                       " declare @defname2 as NVARCHAR(MAX) " +
                       " set @defname2 ='total Prosentase BP' " +
                       " set @query2 =  ' union all  " +
                       " select Defect,' + @cols3 +' from ( select * from (     select Defect,X0.[group],cast((sum(Qty1)/ sum(Z.totkubik)) * 100 as decimal(10,1))qty " +
                       " from (select ''' + @defname2 +''' as Defect,[Group],sum(kubik)Qty1 from tempdefectGP  group by [Group] ) as X0  " +
                       " inner join tempdefectGPTot Z on Z.[group]=X0.[group] group by X0.Defect,X0.[Group]) as X1  pivot  ( sum(qty) for [Group] in (' + @cols +') ) as X)Y '  " +

                       "declare @defname3 as NVARCHAR(MAX) " +
                       "set @defname3 ='Totall BP Pelarian' " +
                       "select @query3 =  ' union all  " +
                       "select Defect,' + @cols4 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname3 +''' as Defect,[Group] , sum(qty) qty from tempdefectGP where sfrom=''lari'' group by [Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(qty) for [Group] in (' + @cols +') " +
                       ") as X)Y order by Defect' " +
                       "execute(@query0 + @query + @query1 + @query2 + @query3)";
                    #endregion
                }
                else
                {
                    #region query lama
                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGP]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGP] " +
                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTot]') AND type in (N'U')) DROP TABLE [dbo].tempdefectGPTot " +
                       "declare @tgl1 as NVARCHAR(MAX),@tgl2 as NVARCHAR(MAX) " +
                       "set @tgl1='" + tgl1 + "' " +
                       "set @tgl2='" + tgl2 + "' " +
                       "select * into tempdefectGP from ( " +
                       "select " + strOven + ", " +
                       "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  P.NoPAlet, D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,  " +
                       "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID,C.QtyIn,B.Qty*I.Volume as Kubik,E.Qty as TotPotong,C.sfrom " +
                       "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID left join T1_Serah C on A.SerahID=C.ID  " +
                       "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=C.DestID left join BM_Palet P on E.PaletID=P.ID " +
                       "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID  left join BM_Plant BP on BP.ID=E.PlantID left join FC_Items I on I.ID=C.ItemID0 inner join Def_GroupJemur GJ on GJ.ID=A.GroupJemurID " +
                       "where B.RowStatus>-1 ) as Def   " +
                       "where CONVERT(varchar, " + strtanggal + ",112)>=@tgl1 and CONVERT(varchar,  " + strtanggal + " ,112)<=@tgl2 and DeptID1=2 " +
                       "select  B.[Group],SUM(C.qtyin) as totpotong,SUM(kubik) as totkubik into tempdefectGPTot   " +
                       "from BM_Destacking A left join BM_PlantGroup B on A.PlantGroupID=B.ID inner join (  " +
                       "select C.destid,C.qtyin,C.QtyIn*I1.Volume as kubik from T1_Serah C Inner join fc_items I  " +
                       "on C.itemid=I.ID Inner join fc_items I1 on C.itemid0=I1.ID where C.Status>-1 and CONVERT(char,C.tglserah,112)>=@tgl1 and   " +
                       "CONVERT(char,C.tglserah,112)<=@tgl2 and (I.PartNo like '%-w-%' or I.PartNo like '%-3-%' ) " +
                       "union all  " +
                       "select C.DestID , B.Qty qtyin,B.Qty*I.Volume as kubik from Def_Defect A inner join Def_DefectDetail B on A.ID=B.DefectID  " +
                       "inner join T1_Serah C on A.SerahID=C.ID left join FC_Items I on I.ID=C.ItemID0  " +
                       "where CONVERT(char,A.tgl,112)>=@tgl1 and  CONVERT(char,A.tgl,112)<=@tgl2 and B.RowStatus>-1 " +
                       ") as C  on A.ID=C.DestID group by B.[Group] " +
                       "DECLARE @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX),@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), @cols4 AS NVARCHAR(MAX), " +
                       "@query0  AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX), " +
                       "    @query1  AS NVARCHAR(MAX), @query2  AS NVARCHAR(MAX), @query3  AS NVARCHAR(MAX); " +
                       "set @cols1= STUFF((SELECT distinct ',' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) +','+ c.[Group] + " +
                       "            ' /(select sum(qty) from tempdefectGP where [group]=''' + c.[Group] + ''')*100 as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols= STUFF((SELECT distinct ',' +  QUOTENAME(c.[Group] )+',' + QUOTENAME('M3' + c.[Group] )  " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols2= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols3= STUFF((SELECT distinct + ',null as' + QUOTENAME(c.[Group] ) +',' + QUOTENAME('M3' + c.[Group] ) + ',' + QUOTENAME(c.[Group] ) +' as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @cols4= STUFF((SELECT distinct ',' +  +QUOTENAME(c.[Group] ) +',' +QUOTENAME('M3' + c.[Group] )+ ',null as ' +  " +
                       "QUOTENAME(+ '%' + c.[Group] ) " +
                       "            FROM BM_PlantGroup c " +
                       "            FOR XML PATH(''), TYPE " +
                       "            ).value('.', 'NVARCHAR(MAX)')  " +
                       "        ,1,1,'') " +
                       "set @query0 = ' " +
                       "select Defect,' + @cols1 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select C.defname as Defect,[Group],sum(QTY) as Qty from  " +
                       "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                       "    union all " +
                       "    select C.defname as Defect,''M3''+rtrim([Group]),isnull(sum(B.Kubik ),0) as Qty from  Def_Defect A inner join  " +
                       "    tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group]  " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname as NVARCHAR(MAX) " +
                       "set @defname ='Total BP' " +
                       "set @query = ' union all  " +
                       "select Defect,' + @cols2 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname +''' as Defect,[Group],sum(Qty) as Qty from  " +
                       "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group] " +
                       "    union all " +
                       "    select ''' + @defname +''' as Defect,''M3'' +[Group],sum(kubik) as Qty from  " +
                       "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname1 as NVARCHAR(MAX) " +
                       "set @defname1 ='total Potong' " +
                       "set @query1 =  ' union all  " +
                       "select Defect,' + @cols2 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname1 +''' as Defect,[Group],cast(sum(totpotong) as decimal(18,2)) as Qty from tempdefectGPTot group by [Group] " +
                       "    union all  " +
                       "    select ''' + @defname1 +''' as Defect,''M3''+[Group],sum(totkubik) as Qty from tempdefectGPTot group by [Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(QTY) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname2 as NVARCHAR(MAX) " +
                       "set @defname2 ='total Prosentase BP' " +
                       "set @query2 =  ' union all  " +
                       "select Defect,' + @cols3 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select Defect,X0.[group],qty/totpotong*100 as qty from (select ''' + @defname2 +''' as Defect,[Group],sum(qty) as qty from tempdefectGP  group by [Group] ) as X0  " +
                       "    inner join tempdefectGPTot Z on Z.[group]=X0.[group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(qty) for [Group] in (' + @cols +') " +
                       ") as X)Y ' " +
                       "declare @defname3 as NVARCHAR(MAX) " +
                       "set @defname3 ='Totall BP Pelarian' " +
                       "select @query3 =  ' union all  " +
                       "select Defect,' + @cols4 +' from ( " +
                       "select * " +
                       "from ( " +
                       "    select ''' + @defname3 +''' as Defect,[Group] , sum(qty) qty from tempdefectGP where sfrom=''lari'' group by [Group] " +
                       ") as X1  " +
                       "pivot  " +
                       "( " +
                       "sum(qty) for [Group] in (' + @cols +') " +
                       ") as X)Y order by Defect' " +
                       "execute(@query0 + @query + @query1 + @query2 + @query3)";
                    #endregion
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
                GrdDynamic.Columns.Clear();
                string formdeci = "{0:N1}";
                if (txtDecimal.Text.Trim() == "2") formdeci = "{0:N2}";
                if (txtDecimal.Text.Trim() == "3") formdeci = "{0:N3}";
                if (txtDecimal.Text.Trim() == "4") formdeci = "{0:N4}";
                if (txtDecimal.Text.Trim() == "5") formdeci = "{0:N5}";
                if (txtDecimal.Text.Trim() == "6") formdeci = "{0:N6}";
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
                        bfield.HeaderText = col.ColumnName;
                    }
                    if (col.ColumnName.Substring(0, 2) == "M3")
                    {
                        bfield.HeaderText = "M3";
                        bfield.DataFormatString = formdeci;
                    }
                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();
                
                DefectFacades df = new DefectFacades();
                int TBP = df.GetTotalBP();
                int TPot = df.GetTotalPotong();
                Decimal TBPK = df.GetTotalBPKubik();
                Decimal TPotK = df.GetTotalPotongKubik();
                LBP.Text = TBP.ToString("N0");
                LPotong.Text = TPot.ToString("N0");
                LBP0.Text = TBPK.ToString("N1");
                LPotong0.Text = TPotK.ToString("N1");
            }
            #endregion
        }

        private void loadDynamicGridL(string tgl1, string tgl2)
        {
            string strtanggal = string.Empty;
            if (RBTglInput.Checked == true)
                strtanggal = "tglInput";
            if (RBTglProduksi.Checked == true)
                strtanggal = "TglProd";
            if (RBTglPotong.Checked == true)
                strtanggal = "TglPeriksa";
            string strOven = string.Empty;
            if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
            {
                strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
            }
            else
            {
                strOven = "case when D.DeptID=0 then " +
                            "case when GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') then  " +
                            "    case when D.DefName='retak' then 2  " +
                            "        when D.DefName='gompal' then 3 " +
                            "    else D.DeptID end  " +
                            "else   " +
                            "    case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end  " +
                            "end " +
                        "else D.DeptID end deptID1";
            }
            string strSQL = string.Empty;
            if (Convert.ToInt32(tgl1.Substring(0, 6)) >= 201805)
            {
                #region query Baru
                strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGP]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGP] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTot]') AND type in (N'U')) DROP TABLE [dbo].tempdefectGPTot " +
               "declare @tgl1 as NVARCHAR(MAX),@tgl2 as NVARCHAR(MAX) " +
               "set @tgl1='" + tgl1 + "' " +
               "set @tgl2='" + tgl2 + "' " +
               "select * into tempdefectGP from ( " +
               "select " + strOven + ",  " +
               "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  P.NoPAlet, D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,  " +
                   "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.ID  DestID,isnull(A.TPotong,0)  QtyIn,B.Qty*I.Volume as Kubik,isnull(A.TPotong,0) as TotPotong,'' sfrom " +
                   "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID  " +
                   "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=A.DestID left join BM_Palet P on E.PaletID=P.ID " +
                   "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID   " +
                   "left join BM_Plant BP on BP.ID=E.PlantID left join FC_Items I on I.ID=E.ItemID inner join Def_GroupJemur GJ on GJ.ID=A.GroupJemurID " +
                   "where B.RowStatus>-1  and D.RowStatus >-1) as Def   " +
                   "where CONVERT(varchar, " + strtanggal + ",112)>=@tgl1 and CONVERT(varchar,  " + strtanggal + " ,112)<=@tgl2 and DeptID1=2 and masterid>-1 " +

                   "select   B.PlantCode [Group],SUM(C.qtyin) as totpotong,SUM(kubik) as totkubik into tempdefectGPTot   " +
                   "from BM_Destacking A left join BM_Plant B on A.PlantID=B.ID inner join (  " +
                   "select C.ID DestID , A.TPotong qtyin,A.TPotong*I.Volume as kubik " +
                   "from Def_Defect A  " +
                   "inner join BM_Destacking C on A.DestID=C.ID left join FC_Items I on I.ID=C.ItemID " +
                   "where A.Status>-1 and CONVERT(char,A.tgl,112)>=@tgl1 and  CONVERT(char,A.tgl,112)<=@tgl2 " +
                   ") as C  on A.ID=C.DestID group by B.[PlantCode]  " +

               "DECLARE @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX),@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), @cols4 AS NVARCHAR(MAX),  " +
               "@query0  AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX),     @query1  AS NVARCHAR(MAX), @query2  AS NVARCHAR(MAX), @query3  AS NVARCHAR(MAX);  " +
               "set @cols1= STUFF((SELECT distinct ',' + QUOTENAME(c.PlantCode ) +',' + QUOTENAME('M3' + c.PlantCode ) +','+ QUOTENAME(c.PlantCode ) +  " +
               "                        ' /(select sum(qty) from tempdefectGP where [line]=''' + c.PlantCode + ''')*100 as ' +  " +
               "QUOTENAME(+ '%' + c.PlantCode )  " +
               "            FROM BM_Plant c  " +
               "            FOR XML PATH(''), TYPE  " +
               "            ).value('.', 'NVARCHAR(MAX)')   " +
               "        ,1,1,'')  " +
               "set @cols= STUFF((SELECT distinct ',' +  QUOTENAME(c.PlantCode  )+',' + QUOTENAME('M3' + c.PlantCode  )   " +
               "            FROM BM_Plant c  " +
               "            FOR XML PATH(''), TYPE  " +
               "            ).value('.', 'NVARCHAR(MAX)')   " +
               "        ,1,1,'')  " +
               "set @cols2= STUFF((SELECT distinct ',' +  +QUOTENAME(c.PlantCode ) +',' +QUOTENAME('M3' + c.PlantCode )+ ',null as ' +   " +
               "QUOTENAME(+ '%' + c.PlantCode )  " +
               "            FROM BM_Plant c  " +
               "            FOR XML PATH(''), TYPE  " +
               "            ).value('.', 'NVARCHAR(MAX)')   " +
               "        ,1,1,'')  " +
               "set @cols3= STUFF((SELECT distinct + ',null as' + QUOTENAME(c.PlantCode) +',' + QUOTENAME('M3' + c.PlantCode) + ',' + QUOTENAME(c.PlantCode) +' as ' +   " +
               "QUOTENAME(+ '%' + c.PlantCode)  " +
               "            FROM BM_Plant c  " +
               "            FOR XML PATH(''), TYPE  " +
               "            ).value('.', 'NVARCHAR(MAX)')   " +
               "        ,1,1,'')  " +
               "set @cols4= STUFF((SELECT distinct ',' +  +QUOTENAME(c.PlantCode ) +',' +QUOTENAME('M3' + c.PlantCode )+ ',null as ' +  " +
               "QUOTENAME(+ '%' + c.PlantCode ) " +
               "            FROM BM_Plant c " +
               "            FOR XML PATH(''), TYPE " +
               "            ).value('.', 'NVARCHAR(MAX)')  " +
               "        ,1,1,'') " +
               "set @query0 = '  " +
               "select Defect,' + @cols1 +' from (  " +
               "select *  " +
               "from (  " +
               "    select B.defname as Defect,[line],sum(QTY) as Qty from   " +
               "     tempdefectGP B  group by [line],B.defname   " +
               "    union all  " +
               "    select B.defname as Defect,''M3''+rtrim([line]),isnull(sum(B.Kubik ),0) as Qty from     " +
               "    tempdefectGP B  group by [line],B.defname   " +
               ") as X1   " +
               "pivot   " +
               "(  " +
               "sum(QTY) for [line] in (' + @cols +')  " +
               ") as X)Y '  " +
               "declare @defname as NVARCHAR(MAX)  " +
               "set @defname ='Total BP'  " +
               "set @query = ' union all   " +
               "select Defect,' + @cols2 +' from (  " +
               "select *  " +
               "from (  " +
               "    select ''' + @defname +''' as Defect,[line],sum(Qty) as Qty from   " +
               "     tempdefectGP B  group by [line]  " +
               "    union all  " +
               "    select ''' + @defname +''' as Defect,''M3'' +[line],sum(kubik) as Qty from   " +
               "    tempdefectGP B group by [line]  " +
               ") as X1   " +
               "pivot   " +
               "(  " +
               "sum(QTY) for [line] in (' + @cols +')  " +
               ") as X)Y '  " +
               "declare @defname1 as NVARCHAR(MAX)  " +
               "set @defname1 ='total Potong'  " +
               "set @query1 =  ' union all   " +
               "select Defect,' + @cols2 +' from (  " +
               "select *  " +
               "from (  " +
               "    select ''' + @defname1 +''' as Defect,[group],cast(sum(totpotong) as decimal(18,6)) as Qty from tempdefectGPTot group by [group]  " +
               "    union all   " +
               "    select ''' + @defname1 +''' as Defect,''M3''+[group][line],sum(totkubik) as Qty from tempdefectGPTot group by [group]   " +
               ") as X1   " +
               "pivot   " +
               "(  " +
               "sum(QTY) for [group] in (' + @cols +')  " +
               ") as X)Y '  " +
               "declare @defname2 as NVARCHAR(MAX)  " +
               "set @defname2 ='total Prosentase BP'  " +
               "set @query2 =  ' union all   " +
               "select Defect,' + @cols3 +' from (  " +
               "select *  " +
               "from (  " +
               "    select Defect,X0.[group],qty/totpotong*100 as qty from (select ''' + @defname2 +''' as Defect,[group],sum(qty) as qty from  " +
               "    tempdefectGP  group by [group] ) as X0   " +
               "    inner join tempdefectGPTot Z on Z.[group]=X0.[group]  " +
               ") as X1   " +
               "pivot   " +
               "(  " +
               "sum(qty) for [group] in (' + @cols +')  " +
               ") as X)Y '  " +
               "declare @defname3 as NVARCHAR(MAX) " +
               "set @defname3 ='Totall BP Pelarian' " +
               "select @query3 =  ' union all  " +
               "select Defect,' + @cols4 +' from ( " +
               "select * " +
               "from ( " +
               "    select ''' + @defname3 +''' as Defect,Line [Group] , sum(qty) qty from tempdefectGP where sfrom=''lari'' group by Line" +
               ") as X1  " +
               "pivot  " +
               "( " +
               "sum(qty) for [Group] in (' + @cols +') " +
               ") as X)Y order by Defect' " +
               "execute(@query0 + @query + @query1+@query2+@query3)";
                #endregion
            }
            else
            {
                #region query lama
                strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGP]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectGP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectGPTot]') AND type in (N'U')) DROP TABLE [dbo].tempdefectGPTot " +
                "declare @tgl1 as NVARCHAR(MAX),@tgl2 as NVARCHAR(MAX) " +
                "set @tgl1='" + tgl1 + "' " +
                "set @tgl2='" + tgl2 + "' " +
                "select * into tempdefectGP from ( " +
                "select " + strOven + ",  " +
                "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  P.NoPAlet, D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,   " +
                "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID,C.QtyIn,B.Qty*I.Volume as Kubik,E.Qty as TotPotong ,C.sfrom  " +
                "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID left join T1_Serah C on A.SerahID=C.ID   " +
                "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=C.DestID left join BM_Palet P on E.PaletID=P.ID  " +
                "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID  left join BM_Plant BP on BP.ID=E.PlantID left join FC_Items I on I.ID=C.ItemID0  inner join Def_GroupJemur GJ on GJ.ID=A.GroupJemurID   " +
                "where B.RowStatus>-1 ) as Def  where CONVERT(varchar, " + strtanggal + ",112)>=@tgl1 and CONVERT(varchar,  " + strtanggal + " ,112)<=@tgl2 and DeptID1=2  " +

                "select  B.PlantCode [group] ,SUM(C.qtyin) as totpotong,SUM(kubik) as totkubik into tempdefectGPTot    " +
                "from BM_Destacking A left join BM_Plant B on A.PlantID=B.ID inner join (   " +
                "select C.destid,C.qtyin,C.QtyIn*I1.Volume as kubik from T1_Serah C Inner join fc_items I   " +
                "on C.itemid=I.ID Inner join fc_items I1 on C.itemid0=I1.ID where C.Status>-1 and CONVERT(char,C.tglserah,112)>=@tgl1 and    " +
                "CONVERT(char,C.tglserah,112)<=@tgl2 and (I.PartNo like '%-w-%' or I.PartNo like '%-3-%' )  " +
                "union all   " +
                "select C.DestID , B.Qty qtyin,B.Qty*I.Volume as kubik from Def_Defect A inner join Def_DefectDetail B on A.ID=B.DefectID   " +
                "inner join T1_Serah C on A.SerahID=C.ID left join FC_Items I on I.ID=C.ItemID0   " +
                "where CONVERT(char,A.tgl,112)>=@tgl1 and  CONVERT(char,A.tgl,112)<=@tgl2 and B.RowStatus>-1  " +
                ") as C  on A.ID=C.DestID group by B.PlantCode " +

                "DECLARE @cols AS NVARCHAR(MAX),@cols1 AS NVARCHAR(MAX),@cols2 AS NVARCHAR(MAX),@cols3 AS NVARCHAR(MAX), @cols4 AS NVARCHAR(MAX),  " +
                "@query0  AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX),     @query1  AS NVARCHAR(MAX), @query2  AS NVARCHAR(MAX), @query3  AS NVARCHAR(MAX);  " +
                "set @cols1= STUFF((SELECT distinct ',' + QUOTENAME(c.PlantCode ) +',' + QUOTENAME('M3' + c.PlantCode ) +','+ QUOTENAME(c.PlantCode ) +  " +
                "                        ' /(select sum(qty) from tempdefectGP where [line]=''' + c.PlantCode + ''')*100 as ' +  " +
                "QUOTENAME(+ '%' + c.PlantCode )  " +
                "            FROM BM_Plant c  " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @cols= STUFF((SELECT distinct ',' +  QUOTENAME(c.PlantCode  )+',' + QUOTENAME('M3' + c.PlantCode  )   " +
                "            FROM BM_Plant c  " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @cols2= STUFF((SELECT distinct ',' +  +QUOTENAME(c.PlantCode ) +',' +QUOTENAME('M3' + c.PlantCode )+ ',null as ' +   " +
                "QUOTENAME(+ '%' + c.PlantCode )  " +
                "            FROM BM_Plant c  " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @cols3= STUFF((SELECT distinct + ',null as' + QUOTENAME(c.PlantCode) +',' + QUOTENAME('M3' + c.PlantCode) + ',' + QUOTENAME(c.PlantCode) +' as ' +   " +
                "QUOTENAME(+ '%' + c.PlantCode)  " +
                "            FROM BM_Plant c  " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @cols4= STUFF((SELECT distinct ',' +  +QUOTENAME(c.PlantCode ) +',' +QUOTENAME('M3' + c.PlantCode )+ ',null as ' +  " +
                "QUOTENAME(+ '%' + c.PlantCode ) " +
                "            FROM BM_Plant c " +
                "            FOR XML PATH(''), TYPE " +
                "            ).value('.', 'NVARCHAR(MAX)')  " +
                "        ,1,1,'') " +
                "set @query0 = '  " +
                "select Defect,' + @cols1 +' from (  " +
                "select *  " +
                "from (  " +
                "    select C.defname as Defect,[line],sum(QTY) as Qty from   " +
                "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[line]   " +
                "    union all  " +
                "    select C.defname as Defect,''M3''+rtrim([line]),isnull(sum(B.Kubik ),0) as Qty from  Def_Defect A inner join   " +
                "    tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[line]   " +
                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(QTY) for [line] in (' + @cols +')  " +
                ") as X)Y '  " +
                "declare @defname as NVARCHAR(MAX)  " +
                "set @defname ='Total BP'  " +
                "set @query = ' union all   " +
                "select Defect,' + @cols2 +' from (  " +
                "select *  " +
                "from (  " +
                "    select ''' + @defname +''' as Defect,[line],sum(Qty) as Qty from   " +
                "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[line]  " +
                "    union all  " +
                "    select ''' + @defname +''' as Defect,''M3'' +[line],sum(kubik) as Qty from   " +
                "    Def_Defect A inner join tempdefectGP B on A.ID=B.DefectID  inner join Def_MasterDefect C on B.MasterID =C.ID group by C.defname ,[line]  " +
                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(QTY) for [line] in (' + @cols +')  " +
                ") as X)Y '  " +
                "declare @defname1 as NVARCHAR(MAX)  " +
                "set @defname1 ='total Potong'  " +
                "set @query1 =  ' union all   " +
                "select Defect,' + @cols2 +' from (  " +
                "select *  " +
                "from (  " +
                "    select ''' + @defname1 +''' as Defect,[group],cast(sum(totpotong) as decimal(18,6)) as Qty from tempdefectGPTot group by [group]  " +
                "    union all   " +
                "    select ''' + @defname1 +''' as Defect,''M3''+[group][line],sum(totkubik) as Qty from tempdefectGPTot group by [group]   " +
                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(QTY) for [group] in (' + @cols +')  " +
                ") as X)Y '  " +
                "declare @defname2 as NVARCHAR(MAX)  " +
                "set @defname2 ='total Prosentase BP'  " +
                "set @query2 =  ' union all   " +
                "select Defect,' + @cols3 +' from (  " +
                "select *  " +
                "from (  " +
                "    select Defect,X0.[group],qty/totpotong*100 as qty from (select ''' + @defname2 +''' as Defect,[group],sum(qty) as qty from  " +
                "    tempdefectGP  group by [group] ) as X0   " +
                "    inner join tempdefectGPTot Z on Z.[group]=X0.[group]  " +
                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(qty) for [group] in (' + @cols +')  " +
                ") as X)Y '  " +
                "declare @defname3 as NVARCHAR(MAX) " +
                "set @defname3 ='Totall BP Pelarian' " +
                "select @query3 =  ' union all  " +
                "select Defect,' + @cols4 +' from ( " +
                "select * " +
                "from ( " +
                "    select ''' + @defname3 +''' as Defect,Line [Group] , sum(qty) qty from tempdefectGP where sfrom=''lari'' group by Line" +
                ") as X1  " +
                "pivot  " +
                "( " +
                "sum(qty) for [Group] in (' + @cols +') " +
                ") as X)Y order by Defect' " +
                "execute(@query0 + @query + @query1+@query2+@query3)";
                #endregion
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
            GrdDynamic.Columns.Clear();
            string formdeci = "{0:N1}";
            if (txtDecimal.Text.Trim() == "2") formdeci = "{0:N2}";
            if (txtDecimal.Text.Trim() == "3") formdeci = "{0:N3}";
            if (txtDecimal.Text.Trim() == "4") formdeci = "{0:N4}";
            if (txtDecimal.Text.Trim() == "5") formdeci = "{0:N5}";
            if (txtDecimal.Text.Trim() == "6") formdeci = "{0:N6}";

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
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName.Substring(0, 2) == "M3")
                {
                    bfield.HeaderText = "M3";
                    bfield.DataFormatString = formdeci;
                }
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            DefectFacades df = new DefectFacades();
            int TBP = df.GetTotalBP();
            int TPot = df.GetTotalPotong();
            Decimal TBPK = df.GetTotalBPKubik();
            Decimal TPotK = df.GetTotalPotongKubik();
            LBP.Text = TBP.ToString("N0");
            LPotong.Text = TPot.ToString("N0");
            LBP0.Text = TBPK.ToString("N1");
            LPotong0.Text = TPotK.ToString("N1");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Defect Group BM.xls"));
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (RBTglInput.Checked == true)
                LblPeriode.Text = "Tanggal Input";
            if (RBTglProduksi.Checked == true)
                LblPeriode.Text = "Tanggal Produksi";
            LblTgl1.Text = txtdrtanggal.Text;
            LblTgl2.Text = txtsdtanggal.Text;
            loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
    }
}