using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LMonitoringStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtdrtanggal.Text = "01" + DateTime.Now.ToString("-MMM-yyyy");
                txtsdtanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                //loadData();
                loadDynamicGridRekap();
                loadDynamicGrid();
            }
            if (RBJenis4.Checked == true)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 350, 100 , 45 ,false); </script>", false);
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 350, 100 , 30 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton4);
        }
        protected void loadData()
        {
            //loadDynamicGridEmpty();
            string strSQL = "declare @HMY0 varchar(10)  " +
                "declare @HMY1 varchar(10)  " +
                "declare @HMY2 varchar(10)  " +
                "declare @thnbln varchar(6) " +
                "declare @tahun int " +
                "declare @Bulan int " +
                "select @HMY1='" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd") + "' " +
                "select @HMY2='" + DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd") + "' " +
                "select @HMY0=SUBSTRING(@HMY1,1,6)+'01' " +
                "select @tahun=CAST( SUBSTRING(@HMY1,1,4) as int) " +
                "select @Bulan=CAST( SUBSTRING(@HMY1,5,2) as int) " +
                "if @Bulan='01'  " +
                "    begin " +
                "    select @tahun=@tahun-1 " +
                "    select @Bulan='12' " +
                "    end " +
                "else " +
                "    begin " +
                "    select @tahun=@tahun " +
                "    select @Bulan=@Bulan-1 " +
                "    end  " +
                "select @thnbln=cast(@tahun as varCHAR)+ REPLACE(STR(@Bulan, 2), SPACE(1), '0') " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempMonitoring]') AND type in (N'U')) DROP TABLE [dbo].TempMonitoring " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockWIP]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockWIP " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockWIP2]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockWIP2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockBJNew]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockBJNew " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempT3_ProdukStd]') AND type in (N'U')) DROP TABLE [dbo].TempT3_ProdukStd " +
                "select * into TempKartustockBJNew from vw_Kartustockbjnew  where CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<=@HMY2 " +
                "select * into TempKartustockWIP from vw_KartustockWIP where CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<=@HMY2 " +
                "select * into TempKartustockWIP2 from vw_KartustockWIP2 where CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<=@HMY2 " +
                "select * into TempT3_ProdukStd from (select rtrim(jenis) +'-3-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd " +
                "        union  " +
                "        select rtrim(jenis) +'-M-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd)std " +
                "select *,@HMY1 Tgl1,@HMY2 Tgl2  " +
                "into TempMonitoring  " +
                "from ( " +
                "    select 1 Urutan,'WIP' Jenis,Partno,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA, " +
                "    isnull(SUM(Awal),0) +SUM(QtyIn) - SUM(QtyOut) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,isnull(SUM(saldo),0) Awal,0 QtyIn,0 QtyOut from T1_SaldoPerLokasi A inner join FC_Items I on A.ItemID=I.ID   " +
                "    where  A.rowstatus>-1 and A.thnbln=@thnbln group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select  I.Partno,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal, 0 QtyIn,0 QtyOut from TempKartustockWIP A  inner join FC_Items I on A.ItemID0=I.ID   " +
                "    where CONVERT(char, tanggal ,112)>=@HMY0 and CONVERT(char, tanggal ,112)<@HMY1 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno,I.tebal,I.Lebar,I.Panjang,0 Awal, SUM(qty)QtyIn,0 QtyOut from TempKartustockWIP A inner join FC_Items I on A.ItemID0=I.ID  " +
                "    where qty>0 and CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno,I.tebal,I.Lebar,I.Panjang,0 Awal, 0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockWIP A inner join FC_Items I on A.ItemID0=I.ID  " +
                "    where qty<0 and CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal, 0 QtyIn,0 QtyOut from TempKartustockWIP2 A inner join FC_Items I on A.ItemID0=I.ID  " +
                "    where CONVERT(char, tanggal ,112)>=@HMY0 and CONVERT(char, tanggal ,112)<@HMY1 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno,I.tebal,I.Lebar,I.Panjang,0 Awal, SUM(qty)QtyIn,0 QtyOut from TempKartustockWIP2 A inner join FC_Items I on A.ItemID0=I.ID  " +
                "    where qty>0 and CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno,I.tebal,I.Lebar,I.Panjang,0 Awal, 0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockWIP2 A inner join FC_Items I on A.ItemID0=I.ID  " +
                "    where qty<0 and CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 group by I.Partno,I.tebal,I.Lebar,I.Panjang " +
                "    ) WIP where Awal<>0 or QtyIn<>0 or QtyOut<>0 group by Partno,tebal,Lebar,Panjang " +
                "    union all " +
                "    select 2 Urutan,'OK' Jenis,Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA,SUM(Saldo)Saldo  from ( " +
                "    select Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,isnull(SUM(Awal),0) +SUM(QtyIn) - SUM(QtyOut) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang, isnull(SUM(A.stock),0) Awal,0 QtyIn,0 QtyOut from vw_AwalStocknPriceBJ A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.YM=@thnbln and (I.partno in (select rtrim(jenis) +'-3-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd) ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal,0 QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<@HMY1 and (I.partno in ( " +
                "    select rtrim(jenis) +'-3-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd)) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,SUM(qty) QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno in ( " +
                "    select rtrim(jenis) +'-3-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd)) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno in ( " +
                "    select rtrim(jenis) +'-3-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd))group by  I.Partno ,I.tebal,I.Lebar,I.Panjang)OK  " +
                "    where Awal<>0 or QtyIn<>0 or QtyOut<>0 group by Partno,tebal,Lebar,Panjang )OK1 group by Partno,tebal,Lebar,Panjang " +
                "  " +
                "    union all " +
                "    select 3 Urutan,'TM' Jenis,Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA,SUM(Saldo)Saldo  from ( " +
                "    select Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,isnull(SUM(Awal),0) +SUM(QtyIn) - SUM(QtyOut) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,isnull(SUM(A.stock),0) Awal,0 QtyIn,0 QtyOut from vw_AwalStocknPriceBJ A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.YM=@thnbln and (I.partno in (select rtrim(jenis) +'-M-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd)) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal,0 QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<@HMY1 and (I.partno in ( " +
                "    select rtrim(jenis) +'-M-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd) ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,SUM(qty) QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno in ( " +
                "    select rtrim(jenis) +'-M-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd) ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno in ( " +
                "    select rtrim(jenis) +'-M-'+  REPLACE(STR(tebal*10, 3), SPACE(1), '0') +REPLACE(STR(lebar, 4), SPACE(1), '0')+ " +
                "    REPLACE(STR(panjang, 4), SPACE(1), '0')+RTRIM(sisi) partno from T3_ProdukStd)) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang)KW " +
                "    where Awal<>0 or QtyIn<>0 or QtyOut<>0 group by Partno,tebal,Lebar,Panjang )KW1 group by Partno,tebal,Lebar,Panjang " +
                "  " +
                "    union all " +
                "    select 4 Urutan,'BP' Jenis,Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA,SUM(Saldo)Saldo  from ( " +
                "    select Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,isnull(SUM(Awal),0) +SUM(QtyIn) - SUM(QtyOut) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,isnull(SUM(A.stock),0) Awal,0 QtyIn,0 QtyOut from vw_AwalStocknPriceBJ A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.YM=@thnbln and (I.partno Like '%-P-%' ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal,0 QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where   CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<@HMY1 and (I.partno Like '%-P-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,SUM(qty) QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-P-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-P-%' )  " +
                "    group by  I.Partno ,I.tebal,I.Lebar,I.Panjang)BP where Awal<>0 or QtyIn<>0 or QtyOut<>0 group by Partno,tebal,Lebar,Panjang )BP1 group by Partno,tebal,Lebar,Panjang " +
                " " +
                " " +
                "    union all " +
                " " +
                " " +
                "    select 6 Urutan,'EFO' Jenis,Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA,SUM(Saldo)Saldo  from ( " +
                "    select Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut,isnull(SUM(Awal),0) +SUM(QtyIn) - SUM(QtyOut) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,isnull(SUM(A.stock),0) Awal,0 QtyIn,0 QtyOut from vw_AwalStocknPriceBJ A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.YM=@thnbln and I.partno not in  " +
                "        (select * from TempT3_ProdukStd) and (I.PartNo like '%-3-%') group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal,0 QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<@HMY1 and I.partno not in  " +
                "        (select * from TempT3_ProdukStd) and (I.PartNo like '%-3-%' or I.PartNo  like '%-M-%') group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "        union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,SUM(qty) QtyIn,0 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and I.partno not in  " +
                "        (select * from TempT3_ProdukStd) and (I.PartNo like '%-3-%' or I.PartNo  like '%-M-%') group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,SUM(qty)*-1 QtyOut from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1 and CONVERT(char,tanggal,112)<=@HMY2 and I.partno not in  " +
                "        (select * from TempT3_ProdukStd) and (I.PartNo like '%-3-%' or I.PartNo  like '%-M-%')  " +
                "        group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    )EFO where Awal<>0 or QtyIn<>0 or QtyOut<>0 group by Partno,tebal,Lebar,Panjang )EFo1 group by Partno,tebal,Lebar,Panjang " +
                " " +
                "    union All " +
                "    select 5 urutan,'BS' Jenis,Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut, " +
                "    SUM(QtyInB)QtyInB,SUM(QtyOutB) QtyOutB,SUM(QtyInA)QtyInA,SUM(QtyOutA) QtyOutA,SUM(Saldo)Saldo  from ( " +
                "    select Partno ,tebal,Lebar,Panjang,isnull(SUM(Awal),0) Awal,SUM(QtyIn)QtyIn,SUM(QtyOut) QtyOut, " +
                "    SUM(QtyInB)QtyInB,SUM(QtyOutB) QtyOutB,SUM(QtyInA)QtyInA,SUM(QtyOutA) QtyOutA, " +
                "    isnull(SUM(Awal),0) +SUM(QtyIn+QtyInB+QtyInA) - SUM(QtyOut+QtyOutB+QtyOutA) Saldo  from ( " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,isnull(SUM(A.stock),0) Awal,0 QtyIn,0 QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA  " +
                "    from vw_AwalStocknPriceBJ A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.YM=@thnbln and (I.partno Like '%-S-%' ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,SUM(qty) Awal,0 QtyIn,0 QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where   CONVERT(char,tanggal,112)>=@HMY0 and CONVERT(char,tanggal,112)<@HMY1 and (I.partno Like '%-S-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,SUM(qty) QtyIn,0 QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA   " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.LokID not In (select ID from FC_Lokasi where (Lokasi='s99' or Lokasi='bsauto' )) and qty>0 and  CONVERT(char,tanggal,112)>=@HMY1  " +
                "    and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,SUM(qty)*-1 QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.LokID not In (select ID from FC_Lokasi where (Lokasi='s99' or Lokasi='bsauto' )) and qty<0 and  CONVERT(char,tanggal,112)>=@HMY1  " +
                "    and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%' )  " +
                "    group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,0 QtyOut,SUM(qty) QtyInB,0 QtyOutB,0 QtyInA,0 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.LokID  In (select ID from FC_Lokasi where (Lokasi='s99' )) and  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1 and  " +
                "    CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,0 QtyOut,0 QtyInB,SUM(qty)*-1 QtyOutB,0 QtyInA,0 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.LokID  In (select ID from FC_Lokasi where (Lokasi='s99' )) and  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1 and  " +
                "    CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%' )  " +
                "    group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,0 QtyOut,0 QtyInB,0 QtyOutB,SUM(qty) QtyInA,0 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on  " +
                "    A.itemid=I.ID where  A.LokID  In (select ID from FC_Lokasi where (Lokasi='bsauto' )) and  qty>0 and  CONVERT(char,tanggal,112)>=@HMY1  " +
                "    and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%'  ) group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    union all " +
                "    select I.Partno ,I.tebal,I.Lebar,I.Panjang,0 Awal,0 QtyIn,0 QtyOut,0 QtyInB,0 QtyOutB,0 QtyInA,SUM(qty)*-1 QtyOutA  " +
                "    from TempKartustockBJNew A inner join FC_Items I on A.itemid=I.ID  " +
                "    where  A.LokID  In (select ID from FC_Lokasi where (Lokasi='bsauto' )) and  qty<0 and  CONVERT(char,tanggal,112)>=@HMY1  " +
                "    and CONVERT(char,tanggal,112)<=@HMY2 and (I.partno Like '%-S-%' )  " +
                "    group by  I.Partno ,I.tebal,I.Lebar,I.Panjang " +
                "    )BS0 where Awal<>0 or QtyIn<>0 or QtyOut<>0 or QtyInB<>0 or QtyOutB<>0 or QtyInA<>0 or QtyOutA<>0 group by Partno ,tebal,Lebar,Panjang " +
                "    )BS1 group by Partno,tebal,Lebar,Panjang " +
                ")atAll order by Urutan,PartNo " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockWIP]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockWIP " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockWIP2]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockWIP2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempKartustockBJNew]') AND type in (N'U')) DROP TABLE [dbo].TempKartustockBJNew " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempT3_ProdukStd]') AND type in (N'U')) DROP TABLE [dbo].TempT3_ProdukStd ";
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
            if (RBJenis4.Checked == false)
            {
                loadDynamicGridRekap();
                loadDynamicGrid();
            }
            else
            {
                loadDynamicGridRekap();
                loadDynamicGridBS();
            }
            DisplayAJAXMessage(this, "Refresh data selesai");
        }
        private void loadDynamicGridRekap()
        {

            string strSQL = "with pemantauan as ( " +
                "select Urutan,Jenis,sum(Awal)Awal,sum(Awal*Volume) AwalM3, " +
                "sum(QtyIn+QtyInB+QtyInA) QtyIn,sum((QtyIn*Volume)+(QtyInB*Volume)+(QtyInA*Volume))QtyInM3, " +
                "sum(QtyOut+QtyOutB+QtyOutA)QtyOut,sum((QtyOut*Volume)+(QtyOutB*Volume)+(QtyOutA*Volume))QtyOutM3, " +
                "Sum(Saldo)Saldo,sum(Saldo*Volume) SaldoM3,Tgl_Awal,Tgl_Akhir from (  " +
                "select Urutan,Jenis,PartNo,Tebal,Lebar,Panjang,(Tebal*Lebar*Panjang)/1000000000 Volume,Awal, " +
                "QtyIn,QtyOut,QtyInB,QtyOutB,QtyInA,QtyOutA,Saldo,cast(tgl1 as datetime) Tgl_Awal,cast(tgl2 as datetime) Tgl_Akhir from TempMonitoring)A group by Tgl_Awal,Tgl_Akhir,Urutan,Jenis " +
                "),pemantauan1 as (  " +
                "select * from pemantauan   " +
                "union  " +
                "select 7 Urutan,'Total'Jenis,sum(Awal)Awal,sum(AwalM3) AwalM3, " +
                "sum(QtyIn),sum(QtyInM3) QtyInM3,sum(QtyOut),sum(QtyOutM3) QtyOutM3,  " +
                "sum(Saldo),sum(SaldoM3) SaldoM3,'' Tgl_Awal,'' Tgl_Akhir from pemantauan  " +
                ") select * from pemantauan1  order by Urutan";
            try
            {
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
                GrdDynamic0.Columns.Clear();
                foreach (DataColumn col in dt.Columns)
                {
                    BoundField bfield = new BoundField();
                    bfield.DataField = col.ColumnName;

                    //if (col.ColumnName == "Tanggal")
                    //{
                    //    bfield.DataFormatString = "{0:d}";
                    //    bfield.HeaderText = "Tanggal";
                    //}
                    if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "M3")
                    {
                        bfield.HeaderText = "M3";
                        bfield.DataFormatString = "{0:N1}";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else
                    {
                        bfield.HeaderText = "Lembar";
                        bfield.DataFormatString = "{0:N0}";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    if (col.ColumnName.Trim().ToUpper() == "JENIS")
                    {
                        bfield.HeaderText = "Jenis";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                    if (col.ColumnName.Trim().ToUpper() == "URUTAN")
                    {
                        bfield.HeaderText = "No.";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                    if (col.ColumnName.Trim().ToUpper() == "TGL_AWAL")
                    {
                        bfield.HeaderText = "Tgl Awal";
                        bfield.DataFormatString = "{0:D}";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                    if (col.ColumnName.Trim().ToUpper() == "TGL_AKHIR")
                    {
                        bfield.HeaderText = "Tgl Akhir";
                        bfield.DataFormatString = "{0:D}";
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                    //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "IN")
                    //    bfield.HeaderText = "Qty_In";
                    //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "UT")
                    //    bfield.HeaderText = "Qty_Out";
                    bfield.HeaderText = bfield.HeaderText.Trim();
                    GrdDynamic0.Columns.Add(bfield);
                }
                GrdDynamic0.DataSource = dt;
                GrdDynamic0.DataBind();
            }
            catch { }
            //if (RBJenis4.Checked == true)
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 45 ,false); </script>", false);
            //else
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 30 ,false); </script>", false);
        }
        private void loadDynamicGrid()
        {

            string jenis = string.Empty;
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;
            if (RBJenis0.Checked == true)
                jenis = "wip";
            if (RBJenis1.Checked == true)
                jenis = "ok";
            if (RBJenis2.Checked == true)
                jenis = "tm";
            if (RBJenis3.Checked == true)
                jenis = "bp";
            if (RBJenis4.Checked == true)
                jenis = "bs";
            if (RBJenis5.Checked == true)
                jenis = "efo";
            string strSQL = "with pemantauan as ( select ROW_NUMBER() OVER(ORDER BY tebal,lebar,panjang,partno)as ID,PartNo,Tebal,Lebar," +
                "Panjang,Awal,Awal*Volume AwalM3,QtyIn,QtyIn*Volume QtyInM3,QtyOut,QtyOut*Volume QtyOutM3,Saldo,Saldo*Volume SaldoM3 from ( " +
                "select Urutan,Jenis,PartNo,Tebal,Lebar,Panjang,(Tebal*Lebar*Panjang)/1000000000 Volume,Awal,QtyIn,QtyOut,Saldo from TempMonitoring)A " +
                "where jenis='" + jenis + "'),pemantauan1 as ( select * from pemantauan  " +
                "union " +
                "select 1000 ID,'Total'Partno,null Tebal,null Lebar,null Panjang,sum(Awal)Awal,sum(AwalM3) AwalM3,sum(QtyIn),sum(QtyInM3) QtyInM3," +
                "sum(QtyOut),sum(QtyOutM3) QtyOutM3,sum(Saldo),sum(SaldoM3) SaldoM3 from pemantauan " +
                ") select * from pemantauan1  order by ID";
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

                //if (col.ColumnName == "Tanggal")
                //{
                //    bfield.DataFormatString = "{0:d}";
                //    bfield.HeaderText = "Tanggal";
                //}
                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "M3")
                {
                    bfield.HeaderText = "M3";
                    bfield.DataFormatString = "{0:N1}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                else
                {
                    bfield.HeaderText = "Lembar";
                    bfield.DataFormatString = "{0:N0}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }

                if (col.ColumnName.Trim().ToUpper() == "TEBAL")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.DataFormatString = "{0:N1}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "LEBAR")
                {
                    bfield.HeaderText = "Lebar";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PANJANG")
                {
                    bfield.HeaderText = "Panjang";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PARTNO")
                {
                    bfield.HeaderText = "Partno";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "ID")
                {
                    bfield.HeaderText = "No.";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "IN")
                //    bfield.HeaderText = "Qty_In";
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "UT")
                //    bfield.HeaderText = "Qty_Out";
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            if (RBJenis4.Checked == true)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 45 ,false); </script>", false);
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 350, 100 , 30 ,false); </script>", false);
        }
        private void loadDynamicGridBS()
        {
            string jenis = string.Empty;
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;
            if (RBJenis0.Checked == true)
                jenis = "wip";
            if (RBJenis1.Checked == true)
                jenis = "ok";
            if (RBJenis2.Checked == true)
                jenis = "kw";
            if (RBJenis3.Checked == true)
                jenis = "bp";
            if (RBJenis4.Checked == true)
                jenis = "bs";
            if (RBJenis5.Checked == true)
                jenis = "efo";
            string strSQL = "with pemantauan as ( " +
                "select ROW_NUMBER() OVER(ORDER BY tebal,lebar,panjang,partno)as ID,PartNo,Tebal,Lebar,Panjang,Awal,Awal*Volume AwalM3, " +
                "QtyIn,QtyIn*Volume QtyInM3,QtyOut,QtyOut*Volume QtyOutM3, " +
                "QtyInB,QtyInB*Volume QtyInBM3,QtyOutB,QtyOutB*Volume QtyOutBM3, " +
                "QtyInA,QtyInA*Volume QtyInAM3,QtyOutA,QtyOutA*Volume QtyOutAM3,Saldo,Saldo*Volume SaldoM3 from (  " +
                "select Urutan,Jenis,PartNo,Tebal,Lebar,Panjang,(Tebal*Lebar*Panjang)/1000000000 Volume,Awal, " +
                "QtyIn,QtyOut,QtyInB,QtyOutB,QtyInA,QtyOutA,Saldo from TempMonitoring)A where jenis='BS'  " +
                "),pemantauan1 as (  " +
                "select * from pemantauan   " +
                "union  " +
                "select 1000 ID,'Total'Partno,null Tebal,null Lebar,null Panjang,sum(Awal)Awal,sum(AwalM3) AwalM3, " +
                "sum(QtyIn),sum(QtyInM3) QtyInM3,sum(QtyOut),sum(QtyOutM3) QtyOutM3,  " +
                "sum(QtyInB),sum(QtyInBM3) QtyInBM3,sum(QtyOutB),sum(QtyOutBM3) QtyOutBM3,  " +
                "sum(QtyInA),sum(QtyInAM3) QtyInAM3,sum(QtyOutA),sum(QtyOutAM3) QtyOutAM3,  " +
                "sum(Saldo),sum(SaldoM3) SaldoM3 from pemantauan  " +
                ") select * from pemantauan1  order by ID";
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

                //if (col.ColumnName == "Tanggal")
                //{
                //    bfield.DataFormatString = "{0:d}";
                //    bfield.HeaderText = "Tanggal";
                //}
                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "M3")
                {
                    bfield.HeaderText = "M3";
                    bfield.DataFormatString = "{0:N1}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                else
                {
                    bfield.HeaderText = "Lembar";
                    bfield.DataFormatString = "{0:N0}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }

                if (col.ColumnName.Trim().ToUpper() == "TEBAL")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "LEBAR")
                {
                    bfield.HeaderText = "Lebar";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PANJANG")
                {
                    bfield.HeaderText = "Panjang";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PARTNO")
                {
                    bfield.HeaderText = "Partno";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "ID")
                {
                    bfield.HeaderText = "No.";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "IN")
                //    bfield.HeaderText = "Qty_In";
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "UT")
                //    bfield.HeaderText = "Qty_Out";
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            if (RBJenis4.Checked == true)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 45 ,false); </script>", false);
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 350, 100 , 30 ,false); </script>", false);
        }
        private void loadDynamicGridEmpty()
        {
            string jenis = string.Empty;
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;
            if (RBJenis0.Checked == true)
                jenis = "wip";
            if (RBJenis1.Checked == true)
                jenis = "ok";
            if (RBJenis2.Checked == true)
                jenis = "kw";
            if (RBJenis3.Checked == true)
                jenis = "bp";
            if (RBJenis4.Checked == true)
                jenis = "bs";
            if (RBJenis5.Checked == true)
                jenis = "efo";
            string strSQL = "delete TempMonitoring";
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

                if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "M3")
                {
                    bfield.HeaderText = "M3";
                    bfield.DataFormatString = "{0:N1}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                else
                {
                    bfield.HeaderText = "Lembar";
                    bfield.DataFormatString = "{0:N0}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }

                if (col.ColumnName.Trim().ToUpper() == "TEBAL")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "LEBAR")
                {
                    bfield.HeaderText = "Lebar";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PANJANG")
                {
                    bfield.HeaderText = "Panjang";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName.Trim().ToUpper() == "PARTNO")
                {
                    bfield.HeaderText = "Partno";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "IN")
                //    bfield.HeaderText = "Qty_In";
                //if (col.ColumnName.Trim().Substring(col.ColumnName.Trim().Length - 2, 2).ToUpper() == "UT")
                //    bfield.HeaderText = "Qty_Out";
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            if (RBJenis4.Checked == true)
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 45 ,false); </script>", false);
            else
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 250, 100 , 30 ,false); </script>", false);
        }
        private void Getfocus()
        {
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                #region non BS
                if (RBJenis4.Checked == false)
                {
                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.ColumnSpan = 1;
                    //HeaderCell.RowSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = " ";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Dimensi";
                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Saldo Awal";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Penerimaan";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Pengeluaran";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "SaldoAkhir";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);

                }
                #endregion
                #region BS
                else
                {
                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = " ";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = " ";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    //HeaderCell = new TableCell();
                    //HeaderCell.Text = " ";
                    //HeaderCell.ColumnSpan = 1;
                    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Dimensi";
                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Saldo Awal";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Penerimaan";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Pengeluaran";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Penerimaan";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Pengeluaran";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Penerimaan";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Pengeluaran";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "SaldoAkhir";
                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);
                    if (RBJenis4.Checked == true)
                    {
                        HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = " ";
                        HeaderCell.ColumnSpan = 1;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = " ";
                        HeaderCell.ColumnSpan = 1;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = " ";
                        HeaderCell.ColumnSpan = 3;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = " ";
                        HeaderCell.ColumnSpan = 2;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "BS Simpan";
                        HeaderCell.ColumnSpan = 4;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "BS Buang";
                        HeaderCell.ColumnSpan = 4;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = "BS Auto";
                        HeaderCell.ColumnSpan = 4;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        HeaderCell = new TableCell();
                        HeaderCell.Text = " ";
                        HeaderCell.ColumnSpan = 2;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderGridRow.Cells.Add(HeaderCell);
                        GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);
                    }
                }
                #endregion

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            loadData();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Monitoring Stock Detail" + DateTime.Now.ToString("ddMMyyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            GrdDynamic.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void RBJenis0_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }

        protected void RBJenis1_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }

        protected void RBJenis2_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }

        protected void RBJenis3_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }

        protected void RBJenis4_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGridBS();
        }

        protected void RBJenis5_CheckedChanged(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }
        protected void GrdDynamic0_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Saldo Awal";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Penerimaan";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Pengeluaran";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "SaldoAkhir";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "Periode Penarikan Data";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic0.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

            if (GrdDynamic0.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Monitoring Stock Rekap " + DateTime.Now.ToString("ddMMyyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic0.AllowPaging = false;
            //GrdDynamic0.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic0.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic0.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            GrdDynamic0.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strvalue = string.Empty;

                for (int i = 0; i < e.Row.Cells[0].Text.Length; i++)
                {
                    if (e.Row.Cells[0].Text.Substring(i, 1) != "." && e.Row.Cells[0].Text.Substring(i, 1) != ",")
                        strvalue = strvalue + e.Row.Cells[0].Text.Substring(i, 1);
                }
                int value = Convert.ToInt32(strvalue);
                if (value >= 1000)
                {
                    e.Row.Cells[0].BackColor = Color.FromName("White");
                    e.Row.Cells[0].ForeColor = Color.FromName("White");

                }
            }
        }
        protected void GrdDynamic0_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strvalue = string.Empty;

                for (int i = 0; i < e.Row.Cells[0].Text.Length; i++)
                {
                    if (e.Row.Cells[0].Text.Substring(i, 1) != ".")
                        strvalue = strvalue + e.Row.Cells[0].Text.Substring(i, 1);
                }
                int value = Convert.ToInt32(strvalue);
                if (value >= 7)
                {
                    e.Row.Cells[0].BackColor = Color.FromName("White");
                    e.Row.Cells[10].BackColor = Color.FromName("White");
                    e.Row.Cells[11].BackColor = Color.FromName("White");

                    e.Row.Cells[0].ForeColor = e.Row.Cells[0].BackColor;
                    e.Row.Cells[10].ForeColor = e.Row.Cells[10].BackColor;
                    e.Row.Cells[11].ForeColor = e.Row.Cells[11].BackColor;

                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}